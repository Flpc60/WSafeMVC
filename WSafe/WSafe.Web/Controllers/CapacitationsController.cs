﻿using Rotativa;
using System;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WSafe.Domain.Data;
using WSafe.Domain.Data.Entities;
using WSafe.Domain.Helpers;
using WSafe.Domain.Models;
using WSafe.Domain.Repositories.Implements;
using WSafe.Domain.Services.Implements;

namespace WSafe.Web.Controllers
{
    public class CapacitationsController : Controller
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
        public CapacitationsController(EmpresaContext empresaContext, IComboHelper comboHelper, IConverterHelper converterHelper, IChartHelper chartHelper, IGestorHelper gestorHelper)
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
                var list = await _empresaContext.Capacitations
                    .Where(o => o.OrganizationID == _orgID && o.EndDate.Year.ToString() == _year)
                    .OrderByDescending(o => o.InitialDate)
                    .Include(s => s.Schedule)
                    .ToListAsync();
                var model = _converterHelper.ToListCapacitationVM(list);
                ViewBag.organization = $"GESTIÓN DE CAPACITACIONES: {Session["organization"].ToString().Trim()}";
                return View(model.ToList());
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Occupationals", "Index"));
            }
        }
        public ActionResult Create()
        {
            _orgID = (int)Session["orgID"];
            var model = _converterHelper.ToCreateCapacitationVM(_orgID);
            ViewBag.guardar = true;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,TrainingTopicID,TrabajadorID,StateCronogram,Programed,Executed,Citados,Capacitados,Evaluados,InitialDate,EndDate,ActivityFrequency,OrganizationID,ClientID,UserID")] CreateCapacitationVM model)
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
                model.TrainingTopics = _comboHelper.GetTrainingTopicsAll(_orgID);
                model.Workers = _comboHelper.GetWorkersFull(_orgID);

                if (model.TrabajadorID != 0)
                {
                    var consulta = new CapacitationService(new CapacitationRepository(_empresaContext));
                    var capacitation = _converterHelper.ToCapacitationAsync(model, true);
                    var saved = await consulta.Insert(capacitation);
                    if (saved != null)
                    {
                        // Generar registros de seguimiento del plan anual de capacitaciones acorde con la programación de actividades

                        var id = _empresaContext.Capacitations.OrderByDescending(c => c.ID)
                            .Select(c => c.ID).First();

                        // Calcular la diferencia en días
                        TimeSpan diferencia = Convert.ToDateTime(model.EndDate) - Convert.ToDateTime(model.InitialDate);
                        int factor = diferencia.Days;
                        var sumar = 1;
                        short numActivities = 0;
                        var denominador = 1;
                        switch (model.ActivityFrequency)
                        {
                            case ActivitiesFrequency.Diaria:
                                sumar = 1;
                                denominador = (factor / sumar) <= 0 ? 1 : (factor / sumar);
                                numActivities = Convert.ToInt16(model.Programed / denominador);
                                break;
                            case ActivitiesFrequency.Semanal:
                                sumar = 7;
                                denominador = (factor / sumar) <= 0 ? 1 : (factor / sumar);
                                numActivities = Convert.ToInt16(model.Programed / denominador);
                                break;
                            case ActivitiesFrequency.Quincenal:
                                sumar = 15;
                                denominador = (factor / sumar) <= 0 ? 1 : (factor / sumar);
                                numActivities = Convert.ToInt16(model.Programed / denominador);
                                break;
                            case ActivitiesFrequency.Mensual:
                                sumar = 30;
                                denominador = (factor / sumar) <= 0 ? 1 : (factor / sumar);
                                numActivities = Convert.ToInt16(model.Programed / denominador);
                                break;
                            case ActivitiesFrequency.Bimensual:
                                sumar = 60;
                                denominador = (factor / sumar) <= 0 ? 1 : (factor / sumar);
                                numActivities = Convert.ToInt16(model.Programed / denominador);
                                break;
                            case ActivitiesFrequency.Trimestral:
                                sumar = 90;
                                denominador = (factor / sumar) <= 0 ? 1 : (factor / sumar);
                                numActivities = Convert.ToInt16(model.Programed / denominador);
                                break;
                            case ActivitiesFrequency.Cuatrimestral:
                                sumar = 120;
                                denominador = (factor / sumar) <= 0 ? 1 : (factor / sumar);
                                numActivities = Convert.ToInt16(model.Programed / denominador);
                                break;
                            case ActivitiesFrequency.Semestral:
                                sumar = 180;
                                denominador = (factor / sumar) <= 0 ? 1 : (factor / sumar);
                                numActivities = Convert.ToInt16(model.Programed / denominador);
                                break;
                            case ActivitiesFrequency.Anual:
                                sumar = 365;
                                denominador = (factor / sumar) <= 0 ? 1 : (factor / sumar);
                                numActivities = Convert.ToInt16(model.Programed / denominador);
                                break;
                            default:
                                break;
                        }

                        if (numActivities > 0)
                        {

                            DateTime fecha = Convert.ToDateTime(model.InitialDate);
                            while (Convert.ToDateTime(model.EndDate) >= fecha)
                            {
                                var siguePlan = new Schedule
                                {
                                    ID = 0,
                                    DateSigue = fecha,
                                    TrabajadorID = model.TrabajadorID,
                                    StateCronogram = (StatesCronogram)1,
                                    Programed = numActivities,
                                    Executed = numActivities,
                                    Citados = model.Citados,
                                    Capacitados = model.Capacitados,
                                    Evaluados = model.Evaluados,
                                    CapacitationID = id,
                                    OrganizationID = model.OrganizationID,
                                    ClientID = model.ClientID,
                                    UserID = model.UserID
                                };
                                _empresaContext.Schedules.Add(siguePlan);

                                fecha = fecha.AddDays(sumar);
                            }
                            await _empresaContext.SaveChangesAsync();
                        }

                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Capacitations", "Index"));
            }

            ViewBag.message = "Faltan campos por diligenciar del formulario !!";
            ViewBag.guardar = true;

            return View(model);
        }
        public async Task<ActionResult> Edit(int? id)
        {
            try
            {
                _orgID = (int)Session["orgID"];
                _year = (string)Session["year"];
                Capacitation capacitation = await _empresaContext.Capacitations.FindAsync(id);
                var model = _converterHelper.ToUpdateCapacitationVM(capacitation, _orgID);
                ViewBag.guardar = true;

                return View(model);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Capacitations", "Index"));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID, TrainingTopicID, TrabajadorID, StateCronogram, Programed, Executed, Citados, Capacitados, Evaluados, InitialDate, EndDate, ActivityFrequency, OrganizationID, ClientID, UserID")] CreateCapacitationVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Capacitation capacitation = _converterHelper.ToCapacitationAsync(model, false);
                    _empresaContext.Entry(capacitation).State = EntityState.Modified;
                    await _empresaContext.SaveChangesAsync();
                    _orgID = (int)Session["orgID"];
                    model.Workers = _comboHelper.GetWorkersFull(_orgID);
                    model.TrainingTopics = _comboHelper.GetTrainingTopicsAll(_orgID);
                    ViewBag.guardar = false;
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Capacitations", "Index"));
            }

            _orgID = model.OrganizationID;
            model.TrainingTopics = _comboHelper.GetTrainingTopicsAll(_orgID);
            model.Workers = _comboHelper.GetWorkersFull(_orgID);
            ViewBag.message = "Faltan campos por diligenciar del formulario !!";
            ViewBag.guardar = true;

            return View(model);
        }

        [HttpGet]
        public ActionResult GetSigueOccupationals(int id)
        {
            try
            {
                var list =
                    from s in _empresaContext.SigueOccupationals
                    where (s.OccupationalID == id)
                    orderby s.SigueDate
                    select new
                    {
                        ID = s.ID,
                        OccupationalID = s.OccupationalID,
                        DateSigue = s.SigueDate,
                        Resultado = s.Resultado,
                        Recomendations = s.Recomendations,
                        TrabajadorID = s.TrabajadorID
                    };

                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch
            {

                var message = "La conslta NO se ha realizado correctamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<ActionResult> UpdateSigueCapacitation(int id)
        {
            _orgID = (int)Session["orgID"];
            _year = (string)Session["year"];

            Schedule model = await _empresaContext.Schedules.FindAsync(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateSigueCapacitation(Schedule model)
        {
            var message = "";
            try
            {
                model.ClientID = (int)Session["clientID"];
                model.OrganizationID = (int)Session["orgID"];
                model.UserID = (int)Session["userID"];

                _empresaContext.Entry(model).State = EntityState.Modified;
                await _empresaContext.SaveChangesAsync();
                message = "La actualización se ha realizado exitosamente !!";
                return Json(new { data = model.CapacitationID, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                message = "La actualización NO se ha realizado exitosamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<ActionResult> DeleteSigueCapacitation(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _orgID = (int)Session["orgID"];
            _year = (string)Session["year"];

            Schedule model = await _empresaContext.Schedules.FindAsync(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteSigueCapacitation(int id)
        {
            try
            {
                Schedule sique = await _empresaContext.Schedules.FindAsync(id);
                _empresaContext.Schedules.Remove(sique);
                await _empresaContext.SaveChangesAsync();
                var message = "El registro se ha borrado exitosamente !!";
                return Json(new { data = sique.CapacitationID, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                var message = "El registro NO se ha borrado exitosamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateSigueCapacitation(Schedule model)
        {
            if (model.CapacitationID == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var message = "";
            try
            {
                model.ClientID = (int)Session["clientID"];
                model.OrganizationID = (int)Session["orgID"];
                model.UserID = (int)Session["userID"];

                if (ModelState.IsValid)
                {
                    _empresaContext.Schedules.Add(model);
                    await _empresaContext.SaveChangesAsync();
                    message = "El registro ha sido ingresado correctamente !!";
                    return Json(new { data = model, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                message = "El registro NO ha sido ingresado correctamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                message = "El registro NO ha sido ingresado correctamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<ActionResult> DeleteCapacitation(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                _clientID = (int)Session["clientID"];
                _orgID = (int)Session["orgID"];
                _year = (string)Session["year"];
                _path = (string)Session["path"];

                var message = "";
                Capacitation result = await _empresaContext.Capacitations
                    .Include(c => c.Schedule)
                    .FirstOrDefaultAsync(c => c.ID == id);

                if (result != null)
                {
                    var sigue = result.Schedule
                        .Where(s => s.CapacitationID == id)
                        .Count();
                    if (sigue != 0)
                    {
                        message = "Esta capaciatción tiene seguimientos !!";
                        return Json(new { data = false, error = message }, JsonRequestBehavior.AllowGet);
                    }
                    var model = _converterHelper.ToUpdateCapacitationVM(result, _orgID);
                    return Json(new { data = model, error = "" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    message = "No se encontró la actividad con el ID proporcionado.";
                    return Json(new { data = false, error = message }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "AnnualPlans", "Index"));
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteCapacitation(int id)
        {
            var consulta = new CapacitationService(new CapacitationRepository(_empresaContext));
            try
            {
                await consulta.Delete(id);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Capacitations", "Delete"));
            }

            return Json(new { data = true, message = "El registro ha sido eliminado exitosamente" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> CapacitationsPdf(string cronograma, string eficacia, string eficiencia, string efectividad)
        {
            try
            {
                // Verificar y obtener datos de sesión
                if (Session["clientID"] == null || Session["orgID"] == null || Session["year"] == null || Session["path"] == null || Session["userID"] == null)
                {
                    return View("Error", new HandleErrorInfo(new Exception("No ha iniciado sesión correctamente."), "Capacitations", "Index"));
                }

                // Configuración nombre archivo pdf
                int _clientID = (int)Session["clientID"];
                int _orgID = (int)Session["orgID"];
                string _year = (string)Session["year"];
                string _path = (string)Session["path"];

                var organization = _empresaContext.Organizations.Find(_orgID);
                var year = _year;
                var item = _empresaContext.Normas.Find(organization.StandardCapacitation).Item;
                var fullPath = $"{_path}/1. PLANEAR/{item}/";
                var path = Server.MapPath(fullPath);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                Random random = new Random();
                var filename = "Cronograma de capacitaciones y entrenamiento" + random.Next(1, 100) + ".Pdf";
                var filePathName = path + filename;

                // Obtener datos de capacitación
                var list = await _empresaContext.Capacitations
                    .Where(o => o.OrganizationID == _orgID && o.InitialDate.Year.ToString() == _year)
                    .OrderByDescending(o => o.InitialDate)
                    .Include(s => s.Schedule)
                    .ToListAsync();
                var model = _converterHelper.ToListCapacitationVM(list);

                var document = _empresaContext.Documents.FirstOrDefault(d => d.ID == 16);
                var decodedCronograma = HttpUtility.UrlDecode(cronograma);
                var decodedEficacia = HttpUtility.UrlDecode(eficacia);
                ViewBag.cronograma = decodedCronograma;
                ViewBag.eficacia = eficacia;
                ViewBag.eficiencia = eficiencia;
                ViewBag.efectividad = efectividad;

                ViewBag.formato = document.Formato;
                ViewBag.estandar = document.Estandar;
                ViewBag.titulo = document.Titulo;
                ViewBag.version = document.Version;
                ViewBag.fecha = DateTime.Now;

                var report = new ViewAsPdf("CreateCapacitations", model)
                {
                    FileName = filePathName,
                    PageSize = Rotativa.Options.Size.A4,
                    PageOrientation = Rotativa.Options.Orientation.Landscape,
                    PageMargins = new Rotativa.Options.Margins(10, 10, 10, 10),
                    CustomSwitches = "--enable-local-file-access"
                };
                var pdfBytes = report.BuildFile(this.ControllerContext);
                System.IO.File.WriteAllBytes(filePathName, pdfBytes);

                //Generar archivo de movimiento
                var fullName = filename;
                var type = Path.GetExtension(filename).ToUpper();
                var descript = "MATRIZ DE CRONOGRAMA DE CAPACITACIONES Y ENTRENAMIENTO";
                var userID = (int)Session["userID"];
                Movimient movimient = new Movimient()
                {
                    ID = 0,
                    OrganizationID = _orgID,
                    NormaID = organization.StandardCapacitation,
                    UserID = userID,
                    Descripcion = descript,
                    Document = fullName,
                    Year = year,
                    Item = item,
                    Ciclo = "P",
                    Type = type,
                    Path = path,
                    ClientID = _clientID
                };
                _empresaContext.Movimientos.Add(movimient);
                // Generar trazabilidad 
                var model1 = _converterHelper.Traceability(organization.StandardCapacitation, year, _orgID, fullName);
                if (model1 != null)
                {
                    _empresaContext.SigueAnnualPlans.Add(model1);
                }

                _empresaContext.SaveChanges();
                var pdfBytesFile = report.BuildFile(this.ControllerContext);

                // Configurar la respuesta HTTP para el tipo MIME y el encabezado del archivo PDF
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "inline;filename=" + filename);
                Response.BinaryWrite(pdfBytesFile);

                var pdfUrl = $"{Request.Url.GetLeftPart(UriPartial.Authority)}/{fullPath}/{filename}";
                return Content(pdfUrl);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Capacitations", "Index"));
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
        public async Task<ActionResult> GetScheduleAll()
        {
            try
            {
                _orgID = (int)Session["orgID"];
                _year = (string)Session["year"];
                var list = await _empresaContext.Capacitations
                    .Where(o => o.OrganizationID == _orgID && o.EndDate.Year.ToString() == _year)
                    .OrderByDescending(o => o.InitialDate)
                    .Include(s => s.Schedule)
                    .ToListAsync();
                var model = _converterHelper.ToCapacitationVM(list);
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Capacitations", "Index"));
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetTrainingTopicName(int id)
        {
            try
            {
                _orgID = (int)Session["orgID"];
                var model = await _empresaContext.TrainingTopics
                    .Where(c => c.OrganizationID == _orgID)
                    .FirstOrDefaultAsync(c => c.ID == id);
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Capacitations", "Index"));
            }
        }

        [HttpGet]
        public ActionResult GetSigueCapacitations(int id)
        {
            try
            {
                var list =
                    from s in _empresaContext.Schedules
                    where (s.CapacitationID == id)
                    orderby s.DateSigue
                    select new
                    {
                        ID = s.ID,
                        CapacitationID = s.CapacitationID,
                        DateSigue = s.DateSigue,
                        TrabajadorID = s.TrabajadorID,
                        Responsable = " ",
                        StateCronogram = s.StateCronogram,
                        Executed = s.Executed,
                        Programed = s.Programed,
                        Citados = s.Citados,
                        Capacitados = s.Capacitados,
                        Evaluados = s.Evaluados
                    };

                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch
            {

                var message = "La conslta NO se ha realizado correctamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddTrainingTopic(TrainingTopic model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _orgID = (int)Session["orgID"];
                    _year = (string)Session["year"];
                    model.OrganizationID = _orgID;
                    _empresaContext.TrainingTopics.Add(model);
                    await _empresaContext.SaveChangesAsync();

                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, message = "Faltan datos por ingresar !!" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
