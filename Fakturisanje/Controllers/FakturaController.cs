using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Fakturisanje.Models;
using Fakturisanje.ViewModels;

namespace Fakturisanje.Controllers
{
    public class FakturaController : Controller
    {
        private InitModel db = new InitModel();

        // GET: Faktura
        public async Task<ActionResult> Index()
        {
            return View(await db.Faktura.Where(f => f.Obrisana == false).OrderBy(f => f.Datum).ToListAsync());
        }

        // GET: Faktura (arhivirane)
        public async Task<ActionResult> ArhiviraneFakture()
        {
            var arhiviraneFakture = await db.Faktura.Where(f => f.Obrisana == true).OrderBy(f => f.Datum).ToListAsync();
            if (arhiviraneFakture == null)
            {
                ViewBag.nemaArhiviranih = "Nema arhiviranih faktura";
                return ViewBag.nemaArhiviranih;
            }
            else
            {
                return View(arhiviraneFakture);
            }
            
        }
        
        // GET: Faktura/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Faktura faktura = await db.Faktura.FindAsync(id);
            if (faktura == null)
            {
                return HttpNotFound();
            }

            List<Stavka> stavke = new List<Stavka>();

            var unosi = db.Unosi.Where(u => u.IdFakture == id).Select(u => u).ToList();
            foreach (var it in unosi)
            {
                var stavka = db.Stavka.Where(s => s.IdStavke == it.IdStavke).Select(s => s).FirstOrDefault();
                var kolicina = db.Unosi.Where(u => u.IdStavke == it.IdStavke && u.IdFakture == id).Select(u => u.Kolicina).FirstOrDefault();
                stavka.kolicina = kolicina;
                stavke.Add(stavka);
            }

            EditFakturaViewModel efvm = new EditFakturaViewModel()
            {
                Id = id,
                Faktura = faktura,
                Unosi = unosi,
                Stavka = stavke
            };

            return View(efvm);
        }

        // GET: Faktura/Create
        public ActionResult Create()
        {
            ViewBag.Stavke = new SelectList(db.Stavka.Where(s => s.Obrisana == false), "IdStavke", "NazivStavke");

            return View();
        }

        public double CenaStavke(int id)
        {
            var cena = db.Stavka.Where(s => s.IdStavke == id).Select(s => s.CenaStavke).FirstOrDefault();

            return cena;
        }

        // POST: Faktura/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "skupStavkizaUnos,skupKolicinazaUnos,Faktura,Unosi")] FakturaViewModel fvm)
        {
            if (ModelState.IsValid)
            {
                FakturaViewModel favimo = new FakturaViewModel()
                {
                    Id = fvm.Faktura.IdFakture
                };

                //Faktura
                Faktura faktura = new Faktura()
                {
                    IdFakture = fvm.Faktura.IdFakture,
                    Datum = fvm.Faktura.Datum,
                    Ukupno = Math.Round(fvm.Faktura.Ukupno, 2, MidpointRounding.ToEven),
                    Obrisana = false
                };

                if (ModelState.IsValid)
                {
                    db.Faktura.Add(faktura);
                }

                var kolicineZaUnos = fvm.skupKolicinazaUnos.Split(',');
                var stavkeZaUnos = fvm.skupStavkizaUnos.Split(',');

                for (var i = 0; i < stavkeZaUnos.Length; i++)
                {
                    Unosi unos = new Unosi()
                    {
                        IdFakture = fvm.Faktura.IdFakture,
                        IdStavke = Convert.ToInt32(stavkeZaUnos[i]),
                        Kolicina = Convert.ToInt32(kolicineZaUnos[i])
                    };

                    if (ModelState.IsValid)
                    {
                        db.Unosi.Add(unos);
                    }
                }
                if (faktura.Datum <= DateTime.Now)
                {
                    db.FakturaViewModels.Add(favimo);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.GreskaDatum = "Datum ne sme biti u buducnosti";
                    ViewBag.Stavke = new SelectList(db.Stavka.Where(s => s.Obrisana == false), "IdStavke", "NazivStavke");
                }
            }

            return View(fvm);
        }

        // GET: Faktura/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faktura faktura = await db.Faktura.FindAsync(id);
            if (faktura == null)
            {
                return HttpNotFound();
            }

            List<Stavka> stavke = new List<Stavka>();

            var unosi = db.Unosi.Where(u => u.IdFakture == id).Select(u => u).ToList();
            foreach (var it in unosi)
            {
                var stavka = db.Stavka.Where(s => s.IdStavke == it.IdStavke).Select(s => s).FirstOrDefault();
                var kolicina = db.Unosi.Where(u => u.IdStavke == it.IdStavke && u.IdFakture == id).Select(u => u.Kolicina).FirstOrDefault();
                stavka.kolicina = kolicina;
                stavke.Add(stavka);
            }

            EditFakturaViewModel efvm = new EditFakturaViewModel()
            {
                Id = id,
                Faktura = faktura,
                Unosi = unosi,
                Stavka = stavke
            };

            ViewBag.Stavke = new SelectList(db.Stavka.Where(s => s.Obrisana == false), "IdStavke", "NazivStavke");

            return View(efvm);
        }

        // POST: Faktura/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "stavkeZaBrisanje,skupStavkizaUnos,skupKolicinazaUnos,Faktura")] EditFakturaViewModel efvm)
        {
            if (ModelState.IsValid)
            {
                Faktura faktura = new Faktura()
                {
                    IdFakture = efvm.Faktura.IdFakture,
                    Datum = efvm.Faktura.Datum,
                    Ukupno = efvm.Faktura.Ukupno,
                    Obrisana = efvm.Faktura.Obrisana
                };

                try
                {
                    if (efvm.stavkeZaBrisanje.Length > 0)
                    {
                        var unosiBrisanje = efvm.stavkeZaBrisanje.Split(',');
                        foreach (var it in unosiBrisanje)
                        {
                            var idSt = Convert.ToInt32(it);
                            var unos = db.Unosi.Where(u => u.IdFakture == efvm.Faktura.IdFakture && u.IdStavke == idSt)
                                                .Select(u => u).FirstOrDefault();

                            db.Unosi.Remove(unos);
                        }
                    }

                    if (efvm.skupStavkizaUnos.Length > 0)
                    {
                        var stavkeZaUnos = efvm.skupStavkizaUnos.Split(',');
                        var kolicineZaunos = efvm.skupKolicinazaUnos.Split(',');

                        for (var i = 0; i < stavkeZaUnos.Length; i++)
                        {
                            Unosi unos = new Unosi()
                            {
                                IdFakture = efvm.Faktura.IdFakture,
                                IdStavke = Convert.ToInt32(stavkeZaUnos[i]),
                                Kolicina = Convert.ToInt32(kolicineZaunos[i])
                            };

                            if (ModelState.IsValid)
                            {
                                db.Unosi.Add(unos);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    db.Entry(faktura).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }

                if (faktura.Obrisana == false)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("ArhiviraneFakture");
                }
            }

            return View(efvm);
        }

        // GET: Faktura/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faktura faktura = await db.Faktura.FindAsync(id);
            if (faktura == null)
            {
                return HttpNotFound();
            }
            return View(faktura);
        }

        // POST: Faktura/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Faktura faktura = await db.Faktura.FindAsync(id);
            //db.Faktura.Remove(faktura);
            faktura.Obrisana = true;
            await db.SaveChangesAsync();
            if (faktura.Obrisana == false)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("ArhiviraneFakture");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
