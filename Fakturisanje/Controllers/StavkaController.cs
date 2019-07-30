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

namespace Fakturisanje.Controllers
{
    public class StavkaController : Controller
    {
        private InitModel db = new InitModel();

        // GET: Stavka
        public async Task<ActionResult> Index()
        {
            return View(await db.Stavka.OrderBy(s => s.Obrisana).ToListAsync());
        }

        // GET: Stavka/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stavka stavka = await db.Stavka.FindAsync(id);
            if (stavka == null)
            {
                return HttpNotFound();
            }
            return View(stavka);
        }

        // GET: Stavka/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stavka/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdStavke,NazivStavke,CenaStavke,Obrisana")] Stavka stavka)
        {
            if (ModelState.IsValid)
            {
                db.Stavka.Add(stavka);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(stavka);
        }

        // GET: Stavka/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stavka stavka = await db.Stavka.FindAsync(id);
            if (stavka == null)
            {
                return HttpNotFound();
            }
            return View(stavka);
        }

        // POST: Stavka/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdStavke,NazivStavke,CenaStavke,Obrisana")] Stavka stavka)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stavka).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(stavka);
        }

        // GET: Stavka/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stavka stavka = await db.Stavka.FindAsync(id);
            if (stavka == null)
            {
                return HttpNotFound();
            }
            return View(stavka);
        }

        // POST: Stavka/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Stavka stavka = await db.Stavka.FindAsync(id);
            db.Stavka.Remove(stavka);
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
