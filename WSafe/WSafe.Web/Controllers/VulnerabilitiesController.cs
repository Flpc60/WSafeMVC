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
                .Include(a => a.Amenaza)
                .Include(v => v.EvaluationConcept)
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
                var model = _emergencyConverter.ToCrtVulnerabilityVMNew(_orgID, 1);
                ViewBag.trabajadores = _comboHelper.GetWorkersFull(_orgID);

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
                    message = "El registro ha sido ingresado correctamente !!";
                    return Json(new { data = model.ID, mensaj = message }, JsonRequestBehavior.AllowGet);
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
                model.ClientID = (int)Session["clientID"];
                model.OrganizationID = (int)Session["orgID"];
                model.UserID = (int)Session["userID"];

                if (!ModelState.IsValid)
                {
                    return Json(new { data = false, mensaj = "El registro NO ha sido ingresado correctamente!!" }, JsonRequestBehavior.AllowGet);
                }

                if (model.ID != 0)
                {
                    return Json(new { data = false, mensaj = "El registro NO ha sido ingresado correctamente!!" }, JsonRequestBehavior.AllowGet);
                }

                ResetEvaluationFieldsByType(model);
                _empresaContext.EvaluationConcepts.Add(model);
                await _empresaContext.SaveChangesAsync();
                var id = model.ID;
                return Json(new { data = id, mensaj = "El registro ha sido ingresado correctamente!!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Vulnerabilities", "Index"));
            }
        }

        private void ResetEvaluationFieldsByType(EvaluationConcept model)
        {
            switch (model.VulnerabilitiType)
            {
                case VulnerabilityTypes.Personas:
                    model.EvaluationRecurso = 0;
                    model.EvaluationSystem = 0;
                    break;
                case VulnerabilityTypes.Recursos:
                    model.EvaluationPerson = 0;
                    model.EvaluationSystem = 0;
                    break;
                case VulnerabilityTypes.Sistemas:
                    model.EvaluationPerson = 0;
                    model.EvaluationRecurso = 0;
                    break;
            }
        }
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                _orgID = (int)Session["orgID"];

                var model = await _emergencyConverter.ToCrtVulnerabilityVM(id, _orgID);
                ViewBag.trabajadores = _comboHelper.GetWorkersFull(_orgID);
                return View(model);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Vulnerabilities", "Index"));
            }
        }
        [HttpPost]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Types,CategoryAmenaza,AmenazaID,EvaluationConceptID,Response,ClientID,OrganizationID,UserID")] CrtVulnerabilityVM model)
        {
            try
            {
                var message = "";
                model.ClientID = (int)Session["clientID"];
                model.OrganizationID = (int)Session["orgID"];
                model.UserID = (int)Session["userID"];

                if (ModelState.IsValid)
                {
                    if (model.ID != 0)
                    {
                        var consulta = new VulnerabilityService(new VulnerabilityRepository(_empresaContext));
                        var vulnerability = await _emergencyConverter.ToVulnerabilityAsync(model, false);
                        var saved = await consulta.Update(vulnerability);
                        if (saved == null)
                        {
                            message = "El registro NO ha sido actualizado correctamente !!";
                            return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            message = "El registro ha sido actualizado correctamente !!";
                            return Json(new { data = vulnerability.ID, mensaj = message }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        message = "El registro NO ha sido actualizado correctamente !!";
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
        public ActionResult GetEvaluations(int id)
        {
            if (id != 0)
            {
                _orgID = (int)Session["orgID"];
                return Json(_comboHelper.GetAllEvaluationConcepts(_orgID, id), JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        [HttpGet]
        public ActionResult GetAmenazas(int id)
        {
            if (id != 0)
            {
                _orgID = (int)Session["orgID"];
                return Json(_comboHelper.GetAllAmenazas(_orgID, id), JsonRequestBehavior.AllowGet);
            }
            return null;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _empresaContext.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        public async Task<ActionResult> GetVulnerabilitiesByID(int id)
        {
            _orgID = (int)Session["orgID"];
            var model = await _converterHelper.GetConsolidateVulnerability(id, _orgID);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}
