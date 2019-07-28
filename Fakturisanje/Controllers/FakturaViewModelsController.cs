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
    public class FakturaViewModelsController : Controller
    {
        private InitModel db = new InitModel();

        // GET: FakturaViewModels
        public async Task<ActionResult> Index()
        {   

            return View(await db.FakturaViewModels.ToListAsync());
        }

        // GET: FakturaViewModels/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FakturaViewModel fakturaViewModel = await db.FakturaViewModels.FindAsync(id);
            if (fakturaViewModel == null)
            {
                return HttpNotFound();
            }
            return View(fakturaViewModel);
        }

        // GET: FakturaViewModels/Create
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

        // POST: FakturaViewModels/Create
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

        // GET: FakturaViewModels/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //FakturaViewModel fakturaViewModel = await db.FakturaViewModels.FindAsync(id);
            //if (fakturaViewModel == null)
            //{
            //    return HttpNotFound();
            //}

            Faktura faktura = db.Faktura.Where(f => f.IdFakture == id).Select(f => f).FirstOrDefault();
            List<Unosi> unosi = db.Unosi.Where(u => u.IdFakture == id).Select(u => u).ToList();
            List<Stavka> stavke = new List<Stavka>();

            foreach(var it in unosi)
            {
                var stavka = db.Stavka.Where(s => s.IdStavke == it.IdStavke).Select(s => s).FirstOrDefault();
                stavke.Add(stavka);
            }

            EditFakturaViewModel editFakt = new EditFakturaViewModel()
            {
                Faktura = faktura,
                Unosi = unosi,
                Stavka = stavke
            };
            
            return View(editFakt);
        }

        // POST: FakturaViewModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id")] FakturaViewModel fakturaViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fakturaViewModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(fakturaViewModel);
        }

        // GET: FakturaViewModels/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FakturaViewModel fakturaViewModel = await db.FakturaViewModels.FindAsync(id);
            if (fakturaViewModel == null)
            {
                return HttpNotFound();
            }
            return View(fakturaViewModel);
        }

        // POST: FakturaViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            FakturaViewModel fakturaViewModel = await db.FakturaViewModels.FindAsync(id);
            db.FakturaViewModels.Remove(fakturaViewModel);
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
