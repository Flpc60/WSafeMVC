using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using WSafe.Domain.Helpers;
using WSafe.Web.Filters;
using WSafe.Web.Models;

namespace WSafe.Web.Controllers
{
    public class RecomendationsController : Controller
    {
        //  Recomendaciones médicas en el SG-SST
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
        public RecomendationsController(EmpresaContext empresaContext, IComboHelper comboHelper, IConverterHelper converterHelper, IChartHelper chartHelper)
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
                var list = await _empresaContext.Recomendations
                    .Where(r => r.OrganizationID == _orgID)
                    .OrderByDescending(r => r.InitialDate)
                    .ToListAsync();
                var model = _converterHelper.ToRecomendationListVM(list);
                return View(model);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Recomnedation", "Index"));
            }
        }

        // GET: Recomendations/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecomendationListVM recomendationListVM = await _empresaContext.RecomendationListVMs.FindAsync(id);
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
                _empresaContext.RecomendationListVMs.Add(recomendationListVM);
                await _empresaContext.SaveChangesAsync();
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
            RecomendationListVM recomendationListVM = await _empresaContext.RecomendationListVMs.FindAsync(id);
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
                _empresaContext.Entry(recomendationListVM).State = EntityState.Modified;
                await _empresaContext.SaveChangesAsync();
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
            RecomendationListVM recomendationListVM = await _empresaContext.RecomendationListVMs.FindAsync(id);
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
            RecomendationListVM recomendationListVM = await _empresaContext.RecomendationListVMs.FindAsync(id);
            _empresaContext.RecomendationListVMs.Remove(recomendationListVM);
            await _empresaContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

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
