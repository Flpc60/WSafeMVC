using Rotativa;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
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
        private readonly IGestorHelper _gestorHelper;
        public AnnualPlansController(EmpresaContext empresaContext, IComboHelper comboHelper, IConverterHelper converterHelper, IChartHelper chartHelper, IGestorHelper gestorHelper)
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

        // GET: AnnualPlans/Create
        public ActionResult Create()
        {
            _orgID = (int)Session["orgID"];
            var model = _converterHelper.ToCreatePlanActivityVM(_orgID);
            ViewBag.guardar = true;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,NormaID,Activity,Entregables,Financieros,Administrativos,Tecnicos,Humanos,TrabajadorID,Observation,InitialDate,FechaFinal,Programed,ActivityFrequency,StateActivity,ActionCategory,OrganizationID,ClientID,UserID,Executed")] CreatePlanActivityVM model)
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
                model.DateSigue = model.InitialDate;
                model.StateCronogram = StatesCronogram.Programada;
                model.Executed = model.Programed;
                model.Normas = _comboHelper.GetNormasAll();
                model.Workers = _comboHelper.GetWorkersFull(_orgID);

                if (ModelState.IsValid && model.NormaID != 0 && model.TrabajadorID != 0)
                {
                    var consulta = new AnnualPlanService(new AnnualPlanRepository(_empresaContext));
                    var planActivity = await _converterHelper.ToPlanActivityAsync(model, true);
                    var saved = await consulta.Insert(planActivity);
                    if (saved != null)
                    {
                        // Generar registros de seguimiento del plan anual acorde con la programación de actividades

                        var id = _empresaContext.PlanActivities.OrderByDescending(pa => pa.ID)
                            .Select(pa => pa.ID).First();

                        // Calcular la diferencia en días
                        TimeSpan diferencia = Convert.ToDateTime(model.FechaFinal) - Convert.ToDateTime(model.InitialDate);
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
                            while (Convert.ToDateTime(model.FechaFinal) >= fecha)
                            {
                                var siguePlan = new SiguePlanAnual
                                {
                                    ID = 0,
                                    DateSigue = fecha,
                                    TrabajadorID = model.TrabajadorID,
                                    StateActivity = model.StateActivity,
                                    StateCronogram = (StatesCronogram)1,
                                    Executed = 0,
                                    Programed = numActivities,
                                    Observation = model.Observation,
                                    ActionCategory = model.ActionCategory,
                                    PlanActivityID = id,
                                    FileName = "",
                                    OrganizationID = model.OrganizationID,
                                    ClientID = model.ClientID,
                                    UserID = model.UserID
                                };
                                _empresaContext.SigueAnnualPlans.Add(siguePlan);

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
                return View("Error", new HandleErrorInfo(ex, "AnnualPlans", "Index"));
            }

            ViewBag.message = "Faltan campos por diligenciar del formulario !!";
            ViewBag.guardar = true;

            return View(model);
        }

        // GET: AnnualPlans/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            try
            {
                _orgID = (int)Session["orgID"];
                _year = (string)Session["year"];
                PlanActivity planActivity = await _empresaContext.PlanActivities.FindAsync(id);
                var model = _converterHelper.ToUpdatePlanActivityVM(planActivity, _orgID);
                ViewBag.guardar = true;

                return View(model);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "AnnualPlans", "Index"));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,NormaID,Activity,Entregables,Financieros,Administrativos,Tecnicos,Humanos,TrabajadorID,Observation,InitialDate,FechaFinal,Programed,ActivityFrequency,StateActivity,ActionCategory,OrganizationID,ClientID,UserID,Executed")] CreatePlanActivityVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PlanActivity planActivity = await _converterHelper.ToPlanActivityAsync(model, false);
                    _empresaContext.Entry(planActivity).State = EntityState.Modified;
                    await _empresaContext.SaveChangesAsync();
                    _orgID = (int)Session["orgID"];
                    model.Normas = _comboHelper.GetNormasAll();
                    model.Workers = _comboHelper.GetWorkersFull(_orgID);

                    ViewBag.guardar = false;
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "AnnualPlans", "Index"));
            }

            _orgID = model.OrganizationID;
            model.Workers = _comboHelper.GetWorkersFull(_orgID);
            model.Normas = _comboHelper.GetNormasAll();
            ViewBag.message = "Faltan campos por diligenciar del formulario !!";
            ViewBag.guardar = true;

            return View(model);
        }

        /*
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

        [HttpGet]
        public ActionResult GetPlanActivities(int id)
        {
            try
            {
                var list =
                    from sp in _empresaContext.SigueAnnualPlans
                    where (sp.PlanActivityID == id)
                    orderby sp.DateSigue
                    select new
                    {
                        ID = sp.ID,
                        DateSigue = sp.DateSigue,
                        TrabajadorID = sp.TrabajadorID,
                        TxtActionCategory = sp.ActionCategory,
                        TxtStateActivity = sp.StateActivity,
                        TxtStateCronogram = sp.StateCronogram,
                        Programed = sp.Programed,
                        Executed = sp.Executed,
                        FileName = sp.FileName,
                        Observation = sp.Observation,
                        Responsable = "",
                        PlanActivityID = sp.PlanActivityID
                    };

                var model = new List<SiguePlanAnualVM>();
                foreach (var item in list)
                {
                    model.Add(new SiguePlanAnualVM
                    {
                        ID = item.ID,
                        TrabajadorID = item.TrabajadorID,
                        TextDateSigue = item.DateSigue.ToString("yyyy-MM-dd"),
                        TxtActionCategory = _gestorHelper.GetActionCategory((int)item.TxtActionCategory),
                        TxtStateActivity = _gestorHelper.GetStateActivity(item.TxtStateActivity),
                        TxtStateCronogram = _gestorHelper.GetStateCronogram(item.TxtStateCronogram),
                        Observation = item.Observation,
                        Programed=item.Programed,
                        Executed = item.Executed,
                        FileName = item.FileName,
                        PlanActivityID=item.PlanActivityID
                    });
                }

                foreach (var item in model)
                {
                    item.Responsable = _empresaContext.Trabajadores.Find(item.TrabajadorID).NombreCompleto.ToUpper();
                }

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch
            {

                var message = "La conslta NO ser ha realizado correctamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult CreateTraceability(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _orgID = (int)Session["orgID"];
            _year = (string)Session["year"];

            var model = new SiguePlanAnualVM
            {
                DateSigue = DateTime.Now,
                Workers = _comboHelper.GetWorkersFull(_orgID),
                PlanActivityID = id,

            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTraceability(SiguePlanAnual model)
        {
            if (model.PlanActivityID == 0)
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
                    _empresaContext.SigueAnnualPlans.Add(model);
                    await _empresaContext.SaveChangesAsync();
                    message = "El registro ha sido ingresado correctamente !!";
                    return Json(new { data = model, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                message = "El registro ha sido ingresado correctamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                message = "El registro ha sido ingresado correctamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<ActionResult> UpdateSiguePlanAnual(int id)
        {
            _orgID = (int)Session["orgID"];
            _year = (string)Session["year"];

            SiguePlanAnual siquePlan = await _empresaContext.SigueAnnualPlans.FindAsync(id);
            var model = _converterHelper.ToUpdateSiguePlanAnual(siquePlan, _orgID);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateSiguePlanAnual(SiguePlanAnual model)
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
                return Json(new { data = model.PlanActivityID, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                message = "La actualización NO se ha realizado exitosamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<ActionResult> DeleteSiguePlanAnual(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _orgID = (int)Session["orgID"];
            _year = (string)Session["year"];

            SiguePlanAnual siquePlan = await _empresaContext.SigueAnnualPlans.FindAsync(id);
            var model = _converterHelper.ToUpdateSiguePlanAnual(siquePlan, _orgID);
            if (model == null)
            {
                return HttpNotFound();
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteSiguePlanAnual(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    SiguePlanAnual siquePlan = await _empresaContext.SigueAnnualPlans.FindAsync(id);
                    _empresaContext.SigueAnnualPlans.Remove(siquePlan);
                    await _empresaContext.SaveChangesAsync();
                    var message = "La actualización se ha realizado exitosamente !!";
                    return Json(new { data = siquePlan.PlanActivityID, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                var message = "La actualización NO se ha realizado exitosamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> AnnualPlansPdf()
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
                var item = _empresaContext.Normas.Find(organization.StandardAnnualPlan).Item;
                var fullPath = $"{_path}/1. PLANEAR/{item}/";
                var path = Server.MapPath(fullPath);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                Random random = new Random();
                var filename = "Plan de trabajo anual" + random.Next(1, 100) + ".Pdf";
                var filePathName = path + filename;
                var list = await _empresaContext.PlanActivities
                    .Where(pa => pa.OrganizationID == _orgID && pa.FechaFinal.Year.ToString() == _year)
                    .OrderByDescending(pa => pa.InitialDate)
                    .Include(s => s.SiguePlanAnual)
                    .ToListAsync();
                var model = _converterHelper.ToAnnualPlanMatriz(list);
                var document = _empresaContext.Documents.FirstOrDefault(d => d.ID == 14);
                ViewBag.formato = document.Formato;
                ViewBag.estandar = document.Estandar;
                ViewBag.titulo = document.Titulo;
                ViewBag.version = document.Version;
                ViewBag.fecha = DateTime.Now;
                var report = new ViewAsPdf("CreateAnnualPlan");
                report.Model = model;
                report.FileName = filePathName;
                report.PageSize = Rotativa.Options.Size.A4;
                report.PageOrientation = Rotativa.Options.Orientation.Landscape;
                report.PageWidth = 399;
                report.PageHeight = 399;
                report.SaveOnServerPath = filePathName;

                //Generar archivo de movimiento
                var fullName = filename;
                var type = Path.GetExtension(filename).ToUpper();
                var descript = "PLAN DE TRABAJO ANUAL";
                var userID = (int)Session["userID"];
                Movimient movimient = new Movimient()
                {
                    ID = 0,
                    OrganizationID = _orgID,
                    NormaID = organization.StandardAnnualPlan,
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
                var model1 = _converterHelper.Traceability(organization.StandardAnnualPlan, year, _orgID, fullName);
                if (model1 != null)
                {
                    _empresaContext.SigueAnnualPlans.Add(model1);
                }

                _empresaContext.SaveChanges();
                return report;
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "AnnualPlans", "Index"));
            }
        }

        [HttpGet]
        public ActionResult AnnualPlanGraphic(int year)
        {
            try
            {
                _clientID = (int)Session["clientID"];
                _orgID = (int)Session["orgID"];
                _year = (string)Session["year"];
                _path = (string)Session["path"];

                Random random = new Random();
                var filename = "chart" + random.Next(1, 100) + ".jpg";
                var filePathName = "~/Images/" + filename;
                var datos = _chartHelper.GetAnnualPlanActivitiesAll(year, _orgID);
                var image = "/Images/" + filename;
                return Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Riesgos", "Index"));
            }
        }

        [HttpGet]
        public async Task<ActionResult> DeleteActivityPlan(int? id)
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
                PlanActivity actyvityPlan = await _empresaContext.PlanActivities
                    .Include(sp => sp.SiguePlanAnual)  // Incluir la propiedad relacionada
                    .FirstOrDefaultAsync(p => p.ID == id);

                if (actyvityPlan != null)
                {
                    var executed = actyvityPlan.SiguePlanAnual
                        .Where(sp => sp.PlanActivityID == id && sp.StateCronogram == StatesCronogram.Ejecutada)
                        .Count();
                    if (executed != 0)
                    {
                        message = "Esta actividad ya se está ejecutando !!";
                        return Json(new { data = false, error = message }, JsonRequestBehavior.AllowGet);
                    }
                    var model = _converterHelper.ToUpdatePlanActivityVM(actyvityPlan, _orgID);

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
        public async Task<ActionResult> DeleteActivityPlan(int id)
        {
            var consulta = new AnnualPlanService(new AnnualPlanRepository(_empresaContext));
            try
            {
                await consulta.Delete(id);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "AnnualPlans", "Delete"));
            }

            return Json(new { data = true, message = "El registro ha sido eliminado exitosamente" }, JsonRequestBehavior.AllowGet);
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