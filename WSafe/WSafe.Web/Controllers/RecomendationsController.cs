using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using WSafe.Domain.Helpers;
using WSafe.Domain.Repositories.Implements;
using WSafe.Domain.Services.Implements;
using WSafe.Web.Filters;
using WSafe.Web.Models;
using static Antlr4.Runtime.Atn.SemanticContext;

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

        [AuthorizeUser(operation: 2, component: 2)]
        public ActionResult Create()
        {
            _orgID = (int)Session["orgID"];
            var model = _converterHelper.ToRecomendationVMNew(_orgID);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,WorkerID,Contingencia,TipoReintegro,CargoID,PatologyID,EmisionDate,Emision,Entity,ReceptionDate,Description,Type,Duration,InitialDate,FinalDate,Compromise,Controls,Investigation,EPP,Tasks,WorkerCompromise,Observation,CoordinadorID")] RecomendationVM model)
        {
            try
            {
                model.ClientID = (int)Session["clientID"];
                model.OrganizationID = (int)Session["orgID"];
                model.UserID = (int)Session["userID"];

                if (ModelState.IsValid)
                {
                    if (model.ID == 0)
                    {
                        var consulta = new RecomendationService(new RecomendationRepository(_empresaContext));
                        var recomendation = await _converterHelper.ToRecomendationAsync(model, true);
                        var saved = await consulta.Insert(recomendation);
                        if (saved != null)
                        {
                            return RedirectToAction("Index");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Recomendation", "Index"));
            }

            model.Workers = _comboHelper.GetWorkersFull(_orgID);
            model.Cargos = _comboHelper.GetCargosAll(_orgID);
            model.Patologies = _comboHelper.GetPatologiesAll();
            ViewBag.message = "Faltan campos por diligenciar del formulario !!";
            return View(model);
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
