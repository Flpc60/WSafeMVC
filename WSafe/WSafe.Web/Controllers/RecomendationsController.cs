using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WSafe.Web.Models;

namespace WSafe.Web.Controllers
{
    public class RecomendationsController : Controller
    {
        private EmpresaContext db = new EmpresaContext();

        // GET: Recomendations
        public async Task<ActionResult> Index()
        {
            return View(await db.RecomendationListVMs.ToListAsync());
        }

        // GET: Recomendations/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecomendationListVM recomendationListVM = await db.RecomendationListVMs.FindAsync(id);
            if (recomendationListVM == null)
            {
                return HttpNotFound();
            }
            return View(recomendationListVM);
        }

        // GET: Recomendations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Recomendations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Trabajador,Contingencia,TipoReintegro,Cargo,Patology,EmisionDate,Emision,Entity,ReceptionDate,InitialDate,FinalDate,Duration,Compromise,Controls,EPP,Tasks,WorkerCompromise,Observation,Coordinador")] RecomendationListVM recomendationListVM)
        {
            if (ModelState.IsValid)
            {
                db.RecomendationListVMs.Add(recomendationListVM);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(recomendationListVM);
        }

        // GET: Recomendations/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecomendationListVM recomendationListVM = await db.RecomendationListVMs.FindAsync(id);
            if (recomendationListVM == null)
            {
                return HttpNotFound();
            }
            return View(recomendationListVM);
        }

        // POST: Recomendations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Trabajador,Contingencia,TipoReintegro,Cargo,Patology,EmisionDate,Emision,Entity,ReceptionDate,InitialDate,FinalDate,Duration,Compromise,Controls,EPP,Tasks,WorkerCompromise,Observation,Coordinador")] RecomendationListVM recomendationListVM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recomendationListVM).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(recomendationListVM);
        }

        // GET: Recomendations/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecomendationListVM recomendationListVM = await db.RecomendationListVMs.FindAsync(id);
            if (recomendationListVM == null)
            {
                return HttpNotFound();
            }
            return View(recomendationListVM);
        }

        // POST: Recomendations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RecomendationListVM recomendationListVM = await db.RecomendationListVMs.FindAsync(id);
            db.RecomendationListVMs.Remove(recomendationListVM);
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
