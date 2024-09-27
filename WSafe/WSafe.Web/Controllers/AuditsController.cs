using Rotativa;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using WSafe.Domain.Data;
using WSafe.Domain.Data.Entities;
using WSafe.Domain.Helpers;
using WSafe.Domain.Models;
using WSafe.Domain.Repositories.Implements;
using WSafe.Domain.Services.Implements;
using WSafe.Web.Filters;

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
                ViewBag.organization = $"GESTIÓN DE AUDITORIAS: {Session["organization"].ToString().Trim()}";
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
            ViewBag.AuditID = model.ID;
            ViewBag.trabajadores = _comboHelper.GetComboTrabajadores(_orgID);
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
                model.Workers = _comboHelper.GetWorkersFull(_orgID);
                model.Auditers = _comboHelper.GetComboAuditers(_orgID);

                if (ModelState.IsValid)
                {
                    if (model.ID == 0)
                    {
                        var consulta = new AuditService(new AuditRepository(_empresaContext));
                        var recomendation = await _converterHelper.ToAuditAsync(model, true);
                        var saved = await consulta.Insert(recomendation);
                        if (saved != null)
                        {
                            ViewBag.AuditID = _empresaContext.Audits.OrderByDescending(a => a.ID).First().ID;
                            return View(model);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Audits", "Index"));
            }

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
                var pdfBytes = report.BuildFile(this.ControllerContext);
                System.IO.File.WriteAllBytes(filePathName, pdfBytes);

                //Generar archivo de movimiento
                var fullName = filename;
                var type = System.IO.Path.GetExtension(filename).ToUpper();
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
                var pdfBytes = report.BuildFile(this.ControllerContext);
                System.IO.File.WriteAllBytes(filePathName, pdfBytes);

                //Generar archivo de movimiento
                var fullName = filename;
                var type = System.IO.Path.GetExtension(filename).ToUpper();
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
                var pdfBytes = report.BuildFile(this.ControllerContext);
                System.IO.File.WriteAllBytes(filePathName, pdfBytes);

                //Generar archivo de movimiento
                var fullName = filename;
                var type = System.IO.Path.GetExtension(filename).ToUpper();
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

        [HttpGet]
        public JsonResult GetQuestionsChapter(int chapter)
        {
            try
            {
                var list = (from a in _empresaContext.AuditItems
                            join n in _empresaContext.Normas on a.NormaID equals n.ID
                            where (int)a.AuditChapter == chapter
                            select new
                            {
                                ID = a.ID,
                                Name = a.Name,
                                Standard = n.Name,
                                Chapter = a.AuditChapter
                            }
                ).ToList();

                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                var message = "La consulta NO se ha realizado exitosamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }
        /*

        [HttpPost]
        public async Task<ActionResult> CreateAuditedResult(List<AuditedResult> model)
        {
            try
            {

                // Actualizar la BD
                if (ModelState.IsValid)
                {
                    var id = model[0].AuditID;
                    var chapter = model[0].AuditChapter;
                    var existingRecordsCount = await _empresaContext.AuditedResults
                        .Where(a => a.AuditID == id && a.AuditChapter == chapter)
                        .CountAsync();

                    if (existingRecordsCount == 0)
                    {
                        Audit audit = await _empresaContext.Audits.FindAsync(model[0].AuditID);
                        foreach (var item in model)
                        {
                            AuditItem auditItem = await _empresaContext.AuditItems.FindAsync(item.AuditItemID);
                            AuditedResult audited = new AuditedResult()
                            {
                                ID = 0,
                                AuditID = item.AuditID,
                                AuditItemID = item.AuditItemID,
                                AuditItem = auditItem,
                                Result = item.Result,
                                Process = audit.Process,
                                AuditChapter = auditItem.AuditChapter
                            };
                            _empresaContext.AuditedResults.Add(audited);
                        }
                        await _empresaContext.SaveChangesAsync();
                        return Json(new { data = model, mensaj = "La actualización se ha realizado exitosamente !!" }, JsonRequestBehavior.AllowGet);
                    }
                    return Json(new { data =false, mensaj = "La actualización No se ha realizado exitosamente !!" }, JsonRequestBehavior.AllowGet);
                }
                var message = "La actualización NO se ha realizado exitosamente !!";
                return Json(new { data = model, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                var message = "La actualización NO se ha realizado exitosamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }
        */
        [HttpPost]
        public async Task<ActionResult> CreateAuditedResult(List<AuditedResult> model)
        {
            try
            {
                // Actualizar la BD
                if (ModelState.IsValid)
                {
                    var id = model[0].AuditID;
                    var chapter = model[0].AuditChapter;
                    var existingRecordsCount = await _empresaContext.AuditedResults
                        .Where(a => a.AuditID == id && a.AuditChapter == chapter)
                        .CountAsync();

                    if (existingRecordsCount == 0)
                    {
                        Audit audit = await _empresaContext.Audits.FindAsync(model[0].AuditID);
                        foreach (var item in model)
                        {
                            AuditItem auditItem = await _empresaContext.AuditItems.FindAsync(item.AuditItemID);
                            AuditedResult audited = new AuditedResult()
                            {
                                ID = 0,
                                AuditID = item.AuditID,
                                AuditItemID = item.AuditItemID,
                                AuditItem = auditItem,
                                Result = item.Result,
                                Process = audit.Process,
                                AuditChapter = auditItem.AuditChapter
                            };
                            _empresaContext.AuditedResults.Add(audited);
                        }
                        await _empresaContext.SaveChangesAsync();
                        return Json(new { success = true, message = "La actualización se ha realizado exitosamente !!" });
                    }

                    return Json(new { success = false, message = "La actualización No se ha realizado exitosamente !!" });
                }

                var message = "La actualización NO se ha realizado exitosamente !!";
                return Json(new { success = false, message = message });
            }
            catch (Exception ex)
            {
                var message = "La actualización NO se ha realizado exitosamente !!";
                return Json(new { success = false, message = message, error = ex.Message });
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
        public JsonResult GetAsistenceAuditedResult(int chapter, int process)
        {
            try
            {
                var list = (from a in _empresaContext.AuditedResults
                            join ai in _empresaContext.AuditItems on a.AuditItemID equals ai.ID
                            where (int)a.Process == (int)process && (int)a.AuditChapter == (int)chapter
                            group ai by new { a.AuditItemID, a.Result } into audited
                            select new
                            {
                                ID = audited.Key.AuditItemID,
                                Name = audited.OrderByDescending(x => x.ID).FirstOrDefault().Name,
                                Result = audited.Key.Result,
                                Count = audited.Count()
                            })
                            .GroupBy(x => x.ID)
                            .Select(group => group.OrderByDescending(x => x.Count).FirstOrDefault())
                            .ToList();

                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                var message = "La consulta NO se ha realizado exitosamente !!";
                ViewBag.ErrorMessage = message; // Usar ViewBag para pasar mensajes a la vista
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}