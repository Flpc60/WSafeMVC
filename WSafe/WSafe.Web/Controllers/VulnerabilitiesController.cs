using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WSafe.Domain.Data;
using WSafe.Domain.Data.Entities.Ppre;
using WSafe.Domain.Helpers;
using WSafe.Domain.Models;
using WSafe.Domain.Repositories.Implements;
using WSafe.Domain.Services.Implements;
using WSafe.Web.Filters;

namespace WSafe.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class VulnerabilitiesController : Controller
    {
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
        private readonly IEmergencyConverter _emergencyConverter;
        public VulnerabilitiesController(EmpresaContext empresaContext, IComboHelper comboHelper, IConverterHelper converterHelper, IChartHelper chartHelper, IGestorHelper gestorHelper, IEmergencyConverter emergencyConverter)
        {
            _empresaContext = empresaContext;
            _comboHelper = comboHelper;
            _converterHelper = converterHelper;
            _chartHelper = chartHelper;
            _gestorHelper = gestorHelper;
            _emergencyConverter = emergencyConverter;
        }

        [AuthorizeUser(operation: 1, component: 2)]
        public async Task<ActionResult> Index()
        {
            _orgID = (int)Session["orgID"];
            var vulnerabilities = await _empresaContext.Vulnerabilities
                .Include(v => v.EvaluationConcept)
                .Include(a => a.Amenaza)
                .ToListAsync();
            var model = _emergencyConverter.ToListVulnerabilityVM(vulnerabilities, _orgID);
            ViewBag.organization = $"GESTIÓN DE VULNERABILIDADES: {Session["organization"].ToString().Trim()}";
            return View(model);
        }

        [AuthorizeUser(operation: 1, component: 2)]
        public async Task<ActionResult> IndexById(int id)
        {
            _orgID = (int)Session["orgID"];

            var vulnerabilities = await _empresaContext.Vulnerabilities
                .Where(v => (int)v.VulnerabilityType == id)
                .Include(v => v.EvaluationConcept)
                .Include(v => v.Amenaza)
                .ToListAsync();

            var model = _emergencyConverter.ToListVulnerabilityVM(vulnerabilities, _orgID);
            ViewBag.organization = $"GESTIÓN DE VULNERABILIDADES: {Session["organization"].ToString().Trim()}";
            return View("Index", model);
        }

        [AuthorizeUser(operation: 2, component: 2)]
        public ActionResult Create()
        {
            try
            {
                _orgID = (int)Session["orgID"];
                var model = _emergencyConverter.ToCrtVulnerabilityVMNew(_orgID);
                return View(model);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Vulnerabilities", "Index"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "ID,Types,CategoryAmenaza,AmenazaID,EvaluationConceptID,Response,ClientID,OrganizationID,UserID")] CrtVulnerabilityVM model)
        {
            try
            {
                var message = "";
                model.ClientID = (int)Session["clientID"];
                model.OrganizationID = (int)Session["orgID"];
                model.UserID = (int)Session["userID"];

                if (ModelState.IsValid)
                {
                    if (model.ID == 0)
                    {
                        var consulta = new VulnerabilityService(new VulnerabilityRepository(_empresaContext));
                        var vulnerability = await _emergencyConverter.ToVulnerabilityAsync(model, true);
                        var saved = await consulta.Insert(vulnerability);
                        if (saved == null)
                        {
                            message = "El registro NO ha sido ingresado correctamente !!";
                            return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            message = "El registro ha sido ingresado correctamente !!";
                            var id = _empresaContext.Vulnerabilities.OrderByDescending(x => x.ID).First().ID;
                            return Json(new { data = id, mensaj = message }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        message = "El registro NO ha sido ingresado correctamente !!";
                        return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    message = "El registro NO ha sido ingresado correctamente !!";
                    return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Vulnerabilities", "Index"));
            }
        }

        [HttpGet]
        public ActionResult GetIntervencionesAll(int id)
        {
            var model = _emergencyConverter.GetInterventionsAll(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> CreateIntervention(Intervention model)
        {
            var message = "";
            try
            {
                model.ClientID = (int)Session["clientID"];
                model.OrganizationID = (int)Session["orgID"];
                model.UserID = (int)Session["userID"];

                if (ModelState.IsValid)
                {
                    _empresaContext.Interventions.Add(model);
                    await _empresaContext.SaveChangesAsync();
                    var id = _empresaContext.Interventions.OrderByDescending(x => x.ID).First().ID;
                    message = "El registro ha sido ingresado correctamente !!";
                    return Json(new { data = id, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                message = "El registro NO ha sido ingresado correctamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                message = "La transacción NO ha sido realizada correctamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateEvaluation([Bind(Include = "ID,Name,VulnerabilitiType,EvaluationPerson,EvaluationRecurso,EvaluationSystem,ClientID,OrganizationID,UserID")] EvaluationConcept model)
        {
            try
            {
                var message = "";
                model.ClientID = (int)Session["clientID"];
                model.OrganizationID = (int)Session["orgID"];
                model.UserID = (int)Session["userID"];

                if (ModelState.IsValid)
                {
                    if (model.ID == 0)
                    {
                        _empresaContext.EvaluationConcepts.Add(model);
                        await _empresaContext.SaveChangesAsync();
                        var id = _empresaContext.EvaluationConcepts.OrderByDescending(x => x.ID).First().ID;
                        message = "El registro ha sido ingresado correctamente !!";
                        return Json(new { data = id, mensaj = message }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        message = "El registro NO ha sido ingresado correctamente !!";
                        return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    message = "El registro NO ha sido ingresado correctamente !!";
                    return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Vulnerabilities", "Index"));
            }
        }

        /*
                // GET: Vulnerabilities/Edit/5
                public async Task<ActionResult> Edit(int? id)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    VulnerabilityVM vulnerabilityVM = await db.VulnerabilityVMs.FindAsync(id);
                    if (vulnerabilityVM == null)
                    {
                        return HttpNotFound();
                    }
                    return View(vulnerabilityVM);
                }

                // POST: Vulnerabilities/Edit/5
                // To protect from overposting attacks, enable the specific properties you want to bind to, for 
                // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
                [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<ActionResult> Edit([Bind(Include = "ID,CategoryAmenaza,Amenaza,Type,EvaluationConcept,Item,Response,Observation")] VulnerabilityVM vulnerabilityVM)
                {
                    if (ModelState.IsValid)
                    {
                        db.Entry(vulnerabilityVM).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                    return View(vulnerabilityVM);
                }

                // GET: Vulnerabilities/Delete/5
                public async Task<ActionResult> Delete(int? id)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    VulnerabilityVM vulnerabilityVM = await db.VulnerabilityVMs.FindAsync(id);
                    if (vulnerabilityVM == null)
                    {
                        return HttpNotFound();
                    }
                    return View(vulnerabilityVM);
                }

                // POST: Vulnerabilities/Delete/5
                [HttpPost, ActionName("Delete")]
                [ValidateAntiForgeryToken]
                public async Task<ActionResult> DeleteConfirmed(int id)
                {
                    VulnerabilityVM vulnerabilityVM = await db.VulnerabilityVMs.FindAsync(id);
                    db.VulnerabilityVMs.Remove(vulnerabilityVM);
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
