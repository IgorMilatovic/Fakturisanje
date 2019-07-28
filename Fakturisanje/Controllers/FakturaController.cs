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
            return View(await db.Faktura.Where(f => f.Obrisana == false).ToListAsync());
        }

        // GET: Faktura (arhivirane)
        public async Task<ActionResult> ArhiviraneFakture()
        {
            var arhiviraneFakture = await db.Faktura.Where(f => f.Obrisana == true).ToListAsync();
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
                var kolicina = db.Unosi.Where(u => u.IdStavke == it.IdStavke).Select(u => u.Kolicina).FirstOrDefault();
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
        public async Task<ActionResult> Create([Bind(Include = "skupStavkizaUnos,skupKolicinazaUnos,Faktura,Unos")] FakturaViewModel fvm)
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

                db.FakturaViewModels.Add(favimo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(fvm);
        }


        //public async Task<ActionResult> Create([Bind(Include = "IdFakture,Datum,Ukupno,Obrisana")] Faktura faktura)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Faktura.Add(faktura);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    return View(faktura);
        //}

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
            return View(faktura);
        }

        // POST: Faktura/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdFakture,Datum,Ukupno,Obrisana")] Faktura faktura)
        {
            if (ModelState.IsValid)
            {
                db.Entry(faktura).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(faktura);
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
            db.Faktura.Remove(faktura);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
