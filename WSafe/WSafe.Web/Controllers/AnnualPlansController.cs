using Rotativa;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using WSafe.Domain.Data.Entities;
using WSafe.Domain.Helpers;
using WSafe.Domain.Repositories.Implements;
using WSafe.Domain.Services.Implements;
using WSafe.Web.Filters;
using WSafe.Web.Models;

namespace WSafe.Web.Controllers
{
    public class AnnualPlansController : Controller
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
        public AnnualPlansController(EmpresaContext empresaContext, IComboHelper comboHelper, IConverterHelper converterHelper, IChartHelper chartHelper)
        {
            _empresaContext = empresaContext;
            _comboHelper = comboHelper;
            _converterHelper = converterHelper;
            _chartHelper = chartHelper;
        }

        [AuthorizeUser(operation: 1, component: 2)]
        public async Task<ActionResult> Index()
        {
            try
            {
                _orgID = (int)Session["orgID"];
                _year = (string)Session["year"];
                var list = await _empresaContext.PlanActivities
                    .Where(pa => pa.OrganizationID == _orgID && pa.FechaFinal.Year.ToString() == _year)
                    .OrderByDescending(pa => pa.InitialDate)
                    .Include(s => s.SiguePlanAnual)
                    .ToListAsync();
                var model = _converterHelper.ToAnnualPlanVM(list);
                return View(model);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "AnnualPlans", "Index"));
            }
        }
        /*
        // GET: AnnualPlans/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnnualPlanVM annualPlanVM = await db.AnnualPlanVMs.FindAsync(id);
            if (annualPlanVM == null)
            {
                return HttpNotFound();
            }
            return View(annualPlanVM);
        }

        // GET: AnnualPlans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AnnualPlans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Cycle,Activity,Entregables,Recursos,Responsable,Observation,StateActivity,Programed,Executed")] AnnualPlanVM annualPlanVM)
        {
            if (ModelState.IsValid)
            {
                db.AnnualPlanVMs.Add(annualPlanVM);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(annualPlanVM);
        }

        // GET: AnnualPlans/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnnualPlanVM annualPlanVM = await db.AnnualPlanVMs.FindAsync(id);
            if (annualPlanVM == null)
            {
                return HttpNotFound();
            }
            return View(annualPlanVM);
        }

        // POST: AnnualPlans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Cycle,Activity,Entregables,Recursos,Responsable,Observation,StateActivity,Programed,Executed")] AnnualPlanVM annualPlanVM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(annualPlanVM).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(annualPlanVM);
        }

        // GET: AnnualPlans/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnnualPlanVM annualPlanVM = await db.AnnualPlanVMs.FindAsync(id);
            if (annualPlanVM == null)
            {
                return HttpNotFound();
            }
            return View(annualPlanVM);
        }

        // POST: AnnualPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            AnnualPlanVM annualPlanVM = await db.AnnualPlanVMs.FindAsync(id);
            db.AnnualPlanVMs.Remove(annualPlanVM);
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