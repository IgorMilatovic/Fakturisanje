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
    public class UnosiController : Controller
    {
        private InitModel db = new InitModel();

        // GET: Unosi
        public async Task<ActionResult> Index()
        {
            var unosi = db.Unosi.Include(u => u.Faktura).Include(u => u.Stavka);
            return View(await unosi.ToListAsync());
        }

        // GET: Unosi/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unosi unosi = await db.Unosi.FindAsync(id);
            if (unosi == null)
            {
                return HttpNotFound();
            }
            return View(unosi);
        }

        // GET: Unosi/Create
        public ActionResult Create()
        {
            ViewBag.IdFakture = new SelectList(db.Faktura, "IdFakture", "IdFakture");
            ViewBag.IdStavke = new SelectList(db.Stavka, "IdStavke", "NazivStavke");
            return View();
        }

        // POST: Unosi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdFakture,IdStavke,Kolicina")] Unosi unosi)
        {
            if (ModelState.IsValid)
            {
                db.Unosi.Add(unosi);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdFakture = new SelectList(db.Faktura, "IdFakture", "IdFakture", unosi.IdFakture);
            ViewBag.IdStavke = new SelectList(db.Stavka, "IdStavke", "NazivStavke", unosi.IdStavke);
            return View(unosi);
        }

        // GET: Unosi/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unosi unosi = await db.Unosi.FindAsync(id);
            if (unosi == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdFakture = new SelectList(db.Faktura, "IdFakture", "IdFakture", unosi.IdFakture);
            ViewBag.IdStavke = new SelectList(db.Stavka, "IdStavke", "NazivStavke", unosi.IdStavke);
            return View(unosi);
        }

        // POST: Unosi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdFakture,IdStavke,Kolicina")] Unosi unosi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(unosi).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdFakture = new SelectList(db.Faktura, "IdFakture", "IdFakture", unosi.IdFakture);
            ViewBag.IdStavke = new SelectList(db.Stavka, "IdStavke", "NazivStavke", unosi.IdStavke);
            return View(unosi);
        }

        // GET: Unosi/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unosi unosi = await db.Unosi.FindAsync(id);
            if (unosi == null)
            {
                return HttpNotFound();
            }
            return View(unosi);
        }

        // POST: Unosi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Unosi unosi = await db.Unosi.FindAsync(id);
            db.Unosi.Remove(unosi);
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
