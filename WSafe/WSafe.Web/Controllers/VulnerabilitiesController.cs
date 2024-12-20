﻿using Rotativa;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WSafe.Domain.Data;
using WSafe.Domain.Data.Entities;
using WSafe.Domain.Data.Entities.Ppre;
using WSafe.Domain.Helpers;
using WSafe.Domain.Models;
using WSafe.Domain.Repositories.Implements;
using WSafe.Domain.Services.Implements;
using WSafe.Web.Filters;

namespace WSafe.Web.Controllers
{
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
            var model = await _emergencyConverter.ToListVulnerabilityVM(_orgID, 1);
            ViewBag.organization = $"GESTIÓN DE VULNERABILIDADES: {Session["organization"].ToString().Trim()}";
            return View(model);
        }

        [AuthorizeUser(operation: 1, component: 2)]
        public async Task<ActionResult> IndexById(int id)
        {
            _orgID = (int)Session["orgID"];
            var model = await _emergencyConverter.ToListVulnerabilityVM(_orgID, id);
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
            switch (model.VulnerabilityType)
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
            var model = await _emergencyConverter.GetConsolidateVulnerability(id, _orgID);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> CalificationAmenazas()
        {
            _orgID = (int)Session["orgID"];
            var califications = await _empresaContext.CalificationAmenazas
                .Include(c => c.Amenaza)
                .OrderBy(c => c.CategoryAmenaza)
                .OrderBy(c => c.AmenzaID)
                .ToListAsync();
            var model = _emergencyConverter.ToListCalificationAmenazaVM(califications, _orgID);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> LevelRiskAmenazas()
        {
            _orgID = (int)Session["orgID"];
            var model = await _emergencyConverter.ToListRiskLevelVM(_orgID);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult PrintVulnerabilitiesToPdf(string calification, string vulnera1, string vulnera2, string vulnera3, string riskLevel)
        {
            try
            {
                if (Session["clientID"] == null || Session["orgID"] == null || Session["year"] == null || Session["path"] == null)
                {
                    throw new InvalidOperationException("Error: Información de sesión incompleta.");
                }

                string Calification = HttpUtility.UrlDecode(calification);
                string Vulnera1 = HttpUtility.UrlDecode(vulnera1);
                string Vulnera2 = HttpUtility.UrlDecode(vulnera2);
                string Vulnera3 = HttpUtility.UrlDecode(vulnera3);
                string RiskLevel = HttpUtility.UrlDecode(riskLevel);

                _clientID = (int)Session["clientID"];
                _orgID = (int)Session["orgID"];
                _year = (string)Session["year"];
                _path = (string)Session["path"];

                var organization = _empresaContext.Organizations.Find(_orgID);
                if (organization == null) throw new Exception("Organización no encontrada.");

                var item = _empresaContext.Normas.Find(organization.StandardEmergenciesPLan)?.Item;
                if (item == null) throw new Exception("Norma no encontrada.");

                var fullPath = $"{_path}/2. HACER/{item}/";
                var path = Server.MapPath(fullPath);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                // Generar el nombre del archivo PDF
                var filename = $"Matriz_analisis_vulnerabilidades_{Guid.NewGuid()}.pdf";
                var filePathName = Path.Combine(path, filename);

                // Crear el modelo para el PDF
                var model = new VulnerabilitiesPdfVM
                {
                    Calification = Calification,
                    Vulnera1 = Vulnera1,
                    Vulnera2 = Vulnera2,
                    Vulnera3 = Vulnera3,
                    RiskLevel = RiskLevel
                };

                // Crear el PDF utilizando Rotativa
                var document = _empresaContext.Documents.FirstOrDefault(d => d.ID == 17);
                ViewBag.estandar = document.Estandar;
                ViewBag.titulo = document.Titulo;
                ViewBag.fecha = DateTime.Now;

                var report = new ViewAsPdf("Details", model)
                {
                    FileName = filePathName,
                    PageSize = Rotativa.Options.Size.A4,
                    PageOrientation = Rotativa.Options.Orientation.Landscape,
                    PageMargins = new Rotativa.Options.Margins(10, 10, 10, 10),
                    CustomSwitches = "--enable-local-file-access"
                };

                // Guardar el PDF en el servidor
                var pdfBytes = report.BuildFile(this.ControllerContext);
                System.IO.File.WriteAllBytes(filePathName, pdfBytes);

                // Registrar movimiento del archivo
                var userID = (int)Session["userID"];
                var movimient = new Movimient
                {
                    OrganizationID = _orgID,
                    NormaID = organization.StandardEmergenciesPLan,
                    UserID = userID,
                    Descripcion = "Matriz de análisis de vulnerabilidades",
                    Document = filename,
                    Year = _year,
                    Item = item,
                    Ciclo = "H",
                    Type = Path.GetExtension(filename).ToUpper(),
                    Path = path,
                    ClientID = _clientID
                };
                _empresaContext.Movimientos.Add(movimient);

                // Registrar trazabilidad
                var traceabilityModel = _converterHelper.Traceability(organization.StandardEmergenciesPLan, _year, _orgID, filename);
                if (traceabilityModel != null)
                {
                    _empresaContext.SigueAnnualPlans.Add(traceabilityModel);
                }

                _empresaContext.SaveChanges();

                return Json(new { fileName = filename, fileUrl = Url.Content(Path.Combine(fullPath, filename)) });
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Vulnerabilities", "PrintVulnerabilitiesToPdf"));
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetVulnerabilitiesDetail(int id)
        {
            _orgID = (int)Session["orgID"];
            var model = await _emergencyConverter.ToVulnerabilitiesVM(_orgID, id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}
