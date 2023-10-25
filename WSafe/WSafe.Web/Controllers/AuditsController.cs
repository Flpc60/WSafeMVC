using Rotativa;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Diagnostics;
using System.IdentityModel.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using WSafe.Domain.Data.Entities;
using WSafe.Domain.Helpers;
using WSafe.Domain.Repositories.Implements;
using WSafe.Domain.Services.Implements;
using WSafe.Web.Filters;
using WSafe.Web.Models;
using static Antlr4.Runtime.Atn.SemanticContext;

namespace WSafe.Web.Controllers
{
    public class AuditsController : Controller
    {
        // auditorías internas en el SG-SST

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
        public AuditsController(EmpresaContext empresaContext, IComboHelper comboHelper, IConverterHelper converterHelper, IChartHelper chartHelper, IGestorHelper gestorHelper)
        {
            _empresaContext = empresaContext;
            _comboHelper = comboHelper;
            _converterHelper = converterHelper;
            _chartHelper = chartHelper;
            _gestorHelper = gestorHelper;
        }

        [AuthorizeUser(operation: 1, component: 2)]
        public async Task<ActionResult> Index()
        {
            try
            {
                _orgID = (int)Session["orgID"];
                var list = await _empresaContext.Audits
                    .Where(a => a.OrganizationID == _orgID)
                    .OrderByDescending(a => a.AuditDate)
                    .ToListAsync();

                var model = _converterHelper.ToAuditListVM(list);
                return View(model);
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Error en la acción Index: {ex.Message}");
                return View("Error", new HandleErrorInfo(ex, "Audits", "Index"));
            }
        }

        [AuthorizeUser(operation: 2, component: 2)]
        public ActionResult Create()
        {
            _orgID = (int)Session["orgID"];
            var model = _converterHelper.ToAuditedCreateVMNew(_orgID);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,AuditDate,AuditerID,AuditProcess,WorkerID,")] AuditedCreateVM model)
        {
            try
            {
                _clientID = (int)Session["clientID"];
                _orgID = (int)Session["orgID"];
                _year = (string)Session["year"];
                _path = (string)Session["path"];
                model.ClientID = (int)Session["clientID"];
                model.OrganizationID = (int)Session["orgID"];
                model.UserID = (int)Session["userID"];

                if (ModelState.IsValid)
                {
                    if (model.ID == 0)
                    {
                        var consulta = new AuditService(new AuditRepository(_empresaContext));
                        var recomendation = await _converterHelper.ToAuditAsync(model, true);
                        var saved = await consulta.Insert(recomendation);
                        if (saved != null)
                        {
                            return View("Create");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Audits", "Index"));
            }

            model.Workers = _comboHelper.GetWorkersFull(_orgID);
            model.Auditers = _comboHelper.GetComboAuditers(_orgID);
            ViewBag.message = "Faltan campos por diligenciar del formulario !!";
            return View(model);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            try
            {
                Recomendation recomendation = await _empresaContext.Recomendations.FindAsync(id);
                _orgID = (int)Session["orgID"];
                var model = _converterHelper.ToRecomendationVM(recomendation, _orgID);

                return View(model);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Recomendations", "Edit"));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,WorkerID,Contingencia,TipoReintegro,CargoID,PatologyID,EmisionDate,Emision,Entity,ReceptionDate,Description,Type,Duration,InitialDate,FinalDate,Compromise,Controls,Investigation,EPP,Tasks,WorkerCompromise,Observation,CoordinadorID,OrganizationID,ClientID,UserID")] RecomendationVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.CoordinadorID != 0)
                    {
                        Recomendation recomendation = await _converterHelper.ToRecomendationAsync(model, false);
                        _empresaContext.Entry(recomendation).State = EntityState.Modified;
                        await _empresaContext.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Recomendations", "Edit"));
            }

            _orgID = model.OrganizationID;
            model.Workers = _comboHelper.GetWorkersFull(_orgID);
            model.Cargos = _comboHelper.GetCargosAll(_orgID);
            model.Patologies = _comboHelper.GetPatologiesAll();
            ViewBag.message = "Faltan campos por diligenciar del formulario !!";

            return View(model);
        }

        // GET: Recomendations/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            try
            {
                Recomendation recomendation = await _empresaContext.Recomendations.FindAsync(id);
                _orgID = (int)Session["orgID"];
                var model = _converterHelper.ToRecomendationVM(recomendation, _orgID);
                ViewBag.trabajador = _empresaContext.Trabajadores.Find(model.WorkerID).NombreCompleto;

                return View(model);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Recomendations", "Edit"));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                Recomendation recomendation = await _empresaContext.Recomendations.FindAsync(id);
                _empresaContext.Recomendations.Remove(recomendation);
                await _empresaContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Recomendations", "Delete"));
            }
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
        public async Task<ActionResult> RecomendationsListPdf()
        {
            try
            {
                // Configuración nombre archivo pdf
                _clientID = (int)Session["clientID"];
                _orgID = (int)Session["orgID"];
                _year = (string)Session["year"];
                _path = (string)Session["path"];
                var organization = _empresaContext.Organizations.Find(_orgID);
                var year = _year;
                var item = _empresaContext.Normas.Find(organization.StandardRecomendations).Item;
                var fullPath = $"{_path}/2. HACER/{item}/";
                var path = Server.MapPath(fullPath);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                Random random = new Random();
                var filename = "Recomendaciones médicas" + random.Next(1, 100) + ".Pdf";
                var filePathName = path + filename;
                var list = await _empresaContext.Recomendations
                    .Where(r => r.OrganizationID == _orgID)
                    .Include(s => s.Seguimients)
                    .OrderByDescending(a => a.ReceptionDate)
                    .ToListAsync();
                var modelo = _converterHelper.ToRecomendationMatrixVM(list);
                var document = _empresaContext.Documents.FirstOrDefault(d => d.ID == 11);
                ViewBag.formato = document.Formato;
                ViewBag.estandar = document.Estandar;
                ViewBag.titulo = document.Titulo;
                ViewBag.version = document.Version;
                ViewBag.fecha = DateTime.Now;
                var report = new ViewAsPdf("CreateRecomendationsList");
                report.Model = modelo;
                report.FileName = filePathName;
                report.PageSize = Rotativa.Options.Size.A4;
                report.PageOrientation = Rotativa.Options.Orientation.Landscape;
                report.PageWidth = 399;
                report.PageHeight = 399;
                report.SaveOnServerPath = filePathName;

                //Generar archivo de movimiento
                var fullName = filename;
                var type = Path.GetExtension(filename).ToUpper();
                var descript = "RESTRICCIÓNES Y RECOMENDACIONES MÉDICO-LABORALES";
                var userID = (int)Session["userID"];
                Movimient movimient = new Movimient()
                {
                    ID = 0,
                    OrganizationID = _orgID,
                    NormaID = organization.StandardRecomendations,
                    UserID = userID,
                    Descripcion = descript,
                    Document = fullName,
                    Year = year,
                    Item = item,
                    Ciclo = "H",
                    Type = type,
                    Path = path,
                    ClientID = _clientID
                };
                _empresaContext.Movimientos.Add(movimient);
                _empresaContext.SaveChanges();
                return report;
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Recomendations", "Index"));
            }
        }

        [HttpGet]
        public async Task<ActionResult> PrintRecomendationToPdf(int id)
        {
            try
            {
                // Configuración nombre archivo pdf
                _clientID = (int)Session["clientID"];
                _orgID = (int)Session["orgID"];
                _year = (string)Session["year"];
                _path = (string)Session["path"];
                var organization = _empresaContext.Organizations.Find(_orgID);
                var year = _year;
                var item = _empresaContext.Normas.Find(organization.StandardRecomendations).Item;
                var fullPath = $"{_path}/2. HACER/{item}/";
                var path = Server.MapPath(fullPath);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                Random random = new Random();
                var filename = "Reporte recomendaciones médicas" + random.Next(1, 100) + ".Pdf";
                var filePathName = path + filename;
                var result = await _empresaContext.Recomendations.Include(sr => sr.Seguimients).FirstOrDefaultAsync(i => i.ID == id);
                var model = _converterHelper.ToRecomendationVMFull(result, 12);
                var document = _empresaContext.Documents.FirstOrDefault(d => d.ID == 12);
                ViewBag.formato = document.Formato;
                ViewBag.estandar = document.Estandar;
                ViewBag.titulo = document.Titulo;
                ViewBag.version = document.Version;
                ViewBag.fecha = DateTime.Now;
                var report = new ViewAsPdf("Details");
                report.Model = model;
                report.FileName = filePathName;
                report.PageSize = Rotativa.Options.Size.A4;
                report.Copies = 1;
                report.PageOrientation.GetValueOrDefault();
                report.FormsAuthenticationCookieName = FormsAuthentication.FormsCookieName;
                report.SaveOnServerPath = filePathName;

                //Generar archivo de movimiento
                var fullName = filename;
                var type = Path.GetExtension(filename).ToUpper();
                var descript = "Reporte recomendaciones médico laborales";
                var userID = (int)Session["userID"];
                Movimient movimient = new Movimient()
                {
                    ID = 0,
                    OrganizationID = _orgID,
                    NormaID = organization.StandardRecomendations,
                    UserID = userID,
                    Descripcion = descript,
                    Document = fullName,
                    Year = year,
                    Item = item,
                    Ciclo = "H",
                    Type = type,
                    Path = path,
                    ClientID = _clientID
                };
                _empresaContext.Movimientos.Add(movimient);
                _empresaContext.SaveChanges();
                return report;
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Recomendations", "Index"));
            }
        }

        [HttpGet]
        public async Task<ActionResult> PrintAuditResultToPdf(int id)
        {
            try
            {
                // Configuración nombre archivo pdf
                _clientID = (int)Session["clientID"];
                _orgID = (int)Session["orgID"];
                _year = (string)Session["year"];
                _path = (string)Session["path"];
                var organization = _empresaContext.Organizations.Find(_orgID);
                var year = _year;
                var item = _empresaContext.Normas.Find(organization.StandardAudits).Item;
                var fullPath = $"{_path}/3. VERIFICAR/{item}/";
                var path = Server.MapPath(fullPath);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                Random random = new Random();
                var filename = "Reporte auditoría interna" + random.Next(1, 100) + ".Pdf";
                var filePathName = path + filename;
                var list = await _empresaContext.AuditedResults
                    .Where(a => a.AuditID == id)
                    .Include(a => a.AuditItem)
                    .OrderBy(a => a.AuditItem.AuditChapter)
                    .ToListAsync();
                IEnumerable<AuditedResultVM> model = _converterHelper.ToAuditedResultVM(list);

                var document = _empresaContext.Documents.FirstOrDefault(d => d.ID == 13);
                ViewBag.formato = document.Formato;
                ViewBag.estandar = document.Estandar;
                ViewBag.titulo = document.Titulo;
                ViewBag.version = document.Version;
                ViewBag.fecha = DateTime.Now;
                var audit = await _empresaContext.Audits.FindAsync(id);
                ViewBag.auditFecha = audit.AuditDate;
                ViewBag.process = _gestorHelper.GetWorkArea(audit.Process);
                ViewBag.responsable = _empresaContext.Trabajadores.Find(audit.WorkerID).NombreCompleto;
                var auditer = _empresaContext.Auditers.Find(audit.AuditerID);
                ViewBag.auditer = auditer.FirstName.ToString() + " " + auditer.LastName.ToString();
                var report = new ViewAsPdf("Details");
                report.Model = model;
                report.FileName = filePathName;
                report.PageSize = Rotativa.Options.Size.A4;
                report.Copies = 1;
                report.PageOrientation.GetValueOrDefault();
                report.FormsAuthenticationCookieName = FormsAuthentication.FormsCookieName;
                report.SaveOnServerPath = filePathName;

                //Generar archivo de movimiento
                var fullName = filename;
                var type = Path.GetExtension(filename).ToUpper();
                var descript = "Reporte auditoría interna";
                var userID = (int)Session["userID"];
                Movimient movimient = new Movimient()
                {
                    ID = 0,
                    OrganizationID = _orgID,
                    NormaID = organization.StandardAudits,
                    UserID = userID,
                    Descripcion = descript,
                    Document = fullName,
                    Year = year,
                    Item = item,
                    Ciclo = "V",
                    Type = type,
                    Path = path,
                    ClientID = _clientID
                };
                _empresaContext.Movimientos.Add(movimient);
                _empresaContext.SaveChanges();
                return report;
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Audits", "Index"));
            }
        }
    }
}