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
using WSafe.Domain.Helpers;

namespace WSafe.Web.Controllers
{
    public class OccupationalsController : Controller
    {
        //  Plan de trabajo anual en el SG-SST
        private int _clientID;
        private int _orgID;
        private string _year;
        private string _path;
        private int _operation;
        private int _roleID;
        private readonly EmpresaContext _empresaContext;
        private readonly IComboHelper _comboHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IChartHelper _chartHelper;
        private readonly IGestorHelper _gestorHelper;
        public OccupationalsController(EmpresaContext empresaContext, IComboHelper comboHelper, IConverterHelper converterHelper, IChartHelper chartHelper, IGestorHelper gestorHelper)
        {
            _empresaContext = empresaContext;
            _comboHelper = comboHelper;
            _converterHelper = converterHelper;
            _chartHelper = chartHelper;
            _gestorHelper = gestorHelper;
        }

        // GET: Occupationals
        public async Task<ActionResult> Index()
        {
            try
            {
                _orgID = (int)Session["orgID"];
                _year = (string)Session["year"];
                var list = await _empresaContext.Occupationals
                    .Where(o => o.OrganizationID == _orgID && o.ExaminationDate.Year.ToString() == _year)
                    .OrderByDescending(o => o.ExaminationDate)
                    .Include(s => s.SigueOccupational)
                    .ToListAsync();
                var model = _converterHelper.ToMedicalRecomendationVM(list);
                return View(model);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Occupationals", "Index"));
            }
        }

/*
        // GET: Occupationals/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalRecomendationVM medicalRecomendationVM = await db.MedicalRecomendationVMs.FindAsync(id);
            if (medicalRecomendationVM == null)
            {
                return HttpNotFound();
            }
            return View(medicalRecomendationVM);
        }

        // GET: Occupationals/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Occupationals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,ExaminationDate,Trabajador,Talla,Peso,ExaminationType,Recomendations,MedicalRecomendation")] MedicalRecomendationVM medicalRecomendationVM)
        {
            if (ModelState.IsValid)
            {
                db.MedicalRecomendationVMs.Add(medicalRecomendationVM);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(medicalRecomendationVM);
        }

        // GET: Occupationals/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalRecomendationVM medicalRecomendationVM = await db.MedicalRecomendationVMs.FindAsync(id);
            if (medicalRecomendationVM == null)
            {
                return HttpNotFound();
            }
            return View(medicalRecomendationVM);
        }

        // POST: Occupationals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,ExaminationDate,Trabajador,Talla,Peso,ExaminationType,Recomendations,MedicalRecomendation")] MedicalRecomendationVM medicalRecomendationVM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicalRecomendationVM).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(medicalRecomendationVM);
        }

        // GET: Occupationals/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalRecomendationVM medicalRecomendationVM = await db.MedicalRecomendationVMs.FindAsync(id);
            if (medicalRecomendationVM == null)
            {
                return HttpNotFound();
            }
            return View(medicalRecomendationVM);
        }

        // POST: Occupationals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MedicalRecomendationVM medicalRecomendationVM = await db.MedicalRecomendationVMs.FindAsync(id);
            db.MedicalRecomendationVMs.Remove(medicalRecomendationVM);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
*/
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _empresaContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
