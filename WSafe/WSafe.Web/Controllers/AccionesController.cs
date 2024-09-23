using Rotativa;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
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
    // Gestionar las acciones correctvas, preventivas y de mejora para el SG-SST...
    public class AccionesController : Controller
    {
        private int _clientID;
        private int _orgID;
        private string _year;
        private string _path;
        private readonly EmpresaContext _empresaContext;
        private readonly IComboHelper _comboHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IGestorHelper _gestorHelper;
        private readonly IChartHelper _chartHelper;
        public AccionesController
            (EmpresaContext empresaContext,
            IComboHelper comboHelper,
            IConverterHelper converterHelper,
            IGestorHelper gestorHelper, IChartHelper chartHelper)
        {
            _empresaContext = empresaContext;
            _comboHelper = comboHelper;
            _converterHelper = converterHelper;
            _gestorHelper = gestorHelper;
            _chartHelper = chartHelper;
        }

        [AuthorizeUser(operation: 1, component: 4)]
        public async Task<ActionResult> Index()
        {
            _orgID = (int)Session["orgID"];
            var list = await _empresaContext.Acciones
                    .Where(a => a.OrganizationID == _orgID)
                .OrderByDescending(a => a.FechaSolicitud)
                .ToListAsync();
            var modelo = _converterHelper.ToAccionVMList(list, _orgID);
            return View(modelo);
        }

        [AuthorizeUser(operation: 2, component: 4)]
        public ActionResult Create()
        {
            _orgID = (int)Session["orgID"];
            var model = _converterHelper.ToAccionViewModelNew(_orgID);
            ViewBag.Categorias = _comboHelper.GetAllCausas();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(AccionViewModel model)
        {
            return RedirectToAction("Index", model);
        }

        [AuthorizeUser(operation: 3, component: 4)]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _orgID = (int)Session["orgID"];
            var result = await _empresaContext.Acciones.FirstOrDefaultAsync(i => i.ID == id.Value);
            var model = _converterHelper.ToAccionViewModel(result, _orgID);
            if (model == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccionID = id;

            ViewBag.fechaSolicitud = model.FechaSolicitud;
            ViewBag.fechaCierre = model.FechaCierre;

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateAccion([Bind(Include="ID, ZonaID, ProcesoID, ActividadID, TareaID, FechaSolicitud, Categoria, TrabajadorID, " +
            "FuenteAccion, Descripcion, EficaciaAntes, EficaciaDespues, FechaCierre, Efectiva, ActionCategory, UserID, MainCause1ID, MainCause2ID, MainCause3ID, MainCause4ID, MainCause5ID")] Accion model)
        {
            var message = "";
            try
            {
                model.ClientID = (int)Session["clientID"];
                model.OrganizationID = (int)Session["orgID"];
                if (ModelState.IsValid)
                {
                    var consulta = new AccionService(new AccionRepository(_empresaContext));
                    var result = await _converterHelper.ToAccionAsync(model, false);
                    await consulta.Update(result);
                    message = "La actualización se ha realizado exitosamente !!";
                    var idAccion = model.ID;
                    return Json(new { data = idAccion, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                message = "La actualización NO se ha realizado exitosamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                message = "La actualización NO se ha realizado exitosamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [AuthorizeUser(operation: 4, component: 4)]
        public async Task<ActionResult> DeleteAccion(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var message = "";
            Accion accion = await _empresaContext.Acciones.FindAsync(id);
            var planes = _empresaContext.PlanActions.Where(p => p.AccionID == id).Count();
            if (planes != 0)
            {
                message = "Esta acción tiene planes de acción pendientes por eliminar!!";
                return Json(new { data = false, error = message }, JsonRequestBehavior.AllowGet);
            }
            var sigue = _empresaContext.Seguimientos.Where(s => s.AccionID == id).Count();
            if (sigue != 0)
            {
                message = "Esta acción tiene seguimientos pendientes por eliminar!!";
                return Json(new { data = false, error = message }, JsonRequestBehavior.AllowGet);
            }

            _orgID = (int)Session["orgID"];
            var model = _converterHelper.ToAccionViewModel(accion, _orgID);
            return Json(new { data = model, error = message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteAccion(int id)
        {
            var consulta = new AccionService(new AccionRepository(_empresaContext));
            try
            {
                await consulta.Delete(id);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Acciones", "Delete"));
            }

            return Json(new { data = true, message = "El registro ha sido eliminado exitosamente" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CreatePlanAccion(int idAccion)
        {
            if (idAccion == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = new PlanAccionVM
            {
                AccionID = idAccion
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> CreatePlanAccion(PlanAction model)
        {
            var message = "";
            try
            {
                model.FechaActivity = DateTime.Now;
                model.ClientID = (int)Session["clientID"];
                model.OrganizationID = (int)Session["orgID"];
                model.UserID = (int)Session["userID"];

                if (ModelState.IsValid)
                {
                    PlanAction result = await _converterHelper.ToPlanAccionAsync(model);
                    _empresaContext.PlanActions.Add(result);
                    var saved = await _empresaContext.SaveChangesAsync();
                    message = "El registro ha sido ingresado correctamente !!";
                    return Json(new { data = result, mensaj = message }, JsonRequestBehavior.AllowGet);
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
        public ActionResult CreateSeguimientoPlan(int idAccion)
        {
            if (idAccion == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            var model = new SeguimientoAccionVM
            {
                AccionID = idAccion,
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> CreateSeguimientoPlan(SeguimientoAccion seguimientoAccion)
        {
            if (seguimientoAccion.AccionID == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var message = "";
            try
            {
                if (ModelState.IsValid)
                {
                    _empresaContext.SeguimientosAccion.Add(seguimientoAccion);
                    await _empresaContext.SaveChangesAsync();
                    message = "El registro ha sido ingresado correctamente !!";
                    return Json(new { data = seguimientoAccion, mensaj = message }, JsonRequestBehavior.AllowGet);
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

        [HttpPost]
        public async Task<ActionResult> CreateAccion(Accion model)
        {
            var message = "";
            try
            {
                model.ClientID = (int)Session["clientID"];
                model.OrganizationID = (int)Session["orgID"];
                model.UserID = (int)Session["userID"];

                if (ModelState.IsValid)
                {
                    var result = await _converterHelper.ToAccionAsync(model, true);
                    _empresaContext.Acciones.Add(result);
                    var saved = await _empresaContext.SaveChangesAsync();
                    message = "El registro ha sido ingresado correctamente !!";
                    var idAccion = _empresaContext.Acciones.OrderByDescending(x => x.ID).First().ID;
                    return Json(new { data = idAccion, mensaj = message }, JsonRequestBehavior.AllowGet);
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

        // GET: Plan acción/ListarPlanAction
        [HttpGet]
        public ActionResult ListarPlanAccion(int idAccion)
        {
            if (idAccion == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //TODO
            var planes = _empresaContext.PlanActions.Where(pa => pa.AccionID == idAccion).ToList();
            var result = _converterHelper.ToPlanAccionVMList(planes);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ListarSeguimientoAccion(int idAccion)
        {
            if (idAccion == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //TODO
            var seguimientos = _empresaContext.SeguimientosAccion
                .Where(sa => sa.AccionID == idAccion).ToList();
            var result = _converterHelper.ToSeguimientoAccionVMList(seguimientos);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult UpdatePlanAccion(int ID)
        {
            var plan = _empresaContext.PlanActions.FirstOrDefault(pa => pa.ID == ID);
            var model = _converterHelper.ToPlanAccionVM(plan);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UpdatePlanAccion(PlanAction planAction)
        {
            var message = "";
            try
            {
                planAction.ClientID = (int)Session["clientID"];
                planAction.OrganizationID = (int)Session["orgID"];
                planAction.UserID = (int)Session["userID"];

                if (ModelState.IsValid)
                {
                    PlanAction plan = await _converterHelper.ToPlanAccionAsync(planAction);
                    _empresaContext.Entry(plan).State = EntityState.Modified;
                    await _empresaContext.SaveChangesAsync();
                    message = "La actualización se ha realizado exitosamente !!";
                    return Json(new { data = true, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                message = "La actualización NO se ha realizado exitosamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                message = "La actualización NO se ha realizado exitosamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<ActionResult> DeletePlanAccion(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanAction plan = await _empresaContext.PlanActions.FindAsync(id);
            var model = _converterHelper.ToPlanAccionVM(plan);
            if (model == null)
            {
                return HttpNotFound();
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> DeletePlanAccion(int id)
        {
            if (ModelState.IsValid)
            {
                PlanAction accion = await _empresaContext.PlanActions.FindAsync(id);
                _empresaContext.PlanActions.Remove(accion);
                await _empresaContext.SaveChangesAsync();
                return Json(accion, JsonRequestBehavior.AllowGet);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public JsonResult UpdateSeguimientoAccion(int ID)
        {
            var seguimiento = _empresaContext.SeguimientosAccion.FirstOrDefault(sa => sa.ID == ID);
            var model = _converterHelper.ToSeguimientoAccionVM(seguimiento);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateSeguimientoAccion([Bind(Include = "ID, AccionID, FechaSeguimiento, Resultado, TrabajadorID")] SeguimientoAccion model)
        {
            var message = "";
            try
            {
                if (ModelState.IsValid)
                {
                    _empresaContext.Entry(model).State = EntityState.Modified;
                    await _empresaContext.SaveChangesAsync();
                    message = "El registro ha sido actualizado correctamente !!";
                    return Json(new { data = true, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                message = "El registro NO ha sido actualizado correctamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                message = "El registro NO ha sido actualizado correctamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }

        }

        // GET: Acciones/Delete/5
        public async Task<ActionResult> DeleteSeguimientoAccion(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeguimientoAccion sigue = await _empresaContext.SeguimientosAccion.FindAsync(id);
            var model = _converterHelper.ToSeguimientoAccionVM(sigue);
            if (model == null)
            {
                return HttpNotFound();
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteSeguimientoAccion(int id)
        {
            if (ModelState.IsValid)
            {
                SeguimientoAccion accion = await _empresaContext.SeguimientosAccion.FindAsync(id);
                _empresaContext.SeguimientosAccion.Remove(accion);
                await _empresaContext.SaveChangesAsync();
                return Json(accion, JsonRequestBehavior.AllowGet);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> PrintAccionesToPdf(int id)
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
                var item = _empresaContext.Normas.Find(organization.StandardActions).Item;
                var fullPath = $"{_path}/4. ACTUAR/{item}/";
                var path = Server.MapPath(fullPath);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                Random random = new Random();
                var filename = "ReporteAccion" + random.Next(1, 100) + ".Pdf";
                var filePathName = path + filename;
                var result = await _empresaContext.Acciones.FirstOrDefaultAsync(i => i.ID == id);

                // Capturar firma electrónica
                var responsable = _empresaContext.Users.Where(u => u.ID == result.UserID).FirstOrDefault();

                string signatureBase64;

                if (responsable.Firma != null)
                {
                    signatureBase64 = Convert.ToBase64String(responsable.Firma);
                }
                else
                {
                    signatureBase64 = Convert.ToBase64String(responsable.FirmaElectronica);
                }

                Session["signatureUser"] = signatureBase64;

                var model = _converterHelper.ToAccionVMFull(result, 1);
                var document = _empresaContext.Documents.FirstOrDefault(d => d.ID == 1);
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
                var descript = "Reporte Acciones CPM";
                var userID = (int)Session["userID"];
                Movimient movimient = new Movimient()
                {
                    ID = 0,
                    OrganizationID = _orgID,
                    NormaID = organization.StandardActions,
                    UserID = userID,
                    Descripcion = descript,
                    Document = fullName,
                    Year = year,
                    Item = item,
                    Ciclo = "A",
                    Type = type,
                    Path = path,
                    ClientID = _clientID
                };
                _empresaContext.Movimientos.Add(movimient);
                // Generar trazabilidad 
                var model1 = _converterHelper.Traceability(organization.StandardActions, year, _orgID, fullName);
                if (model1 != null)
                {
                    _empresaContext.SigueAnnualPlans.Add(model1);
                }

                _empresaContext.SaveChanges();
                return report;
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Acciones", "Index"));
            }
        }

        [HttpGet]
        public ActionResult GetAllPlans()
        {
            _clientID = (int)Session["clientID"];
            _orgID = (int)Session["orgID"];
            _year = (string)Session["year"];
            var noConformance = _empresaContext.Acciones
                .Where(a => a.OrganizationID == _orgID)
                .Count();
            var numPlans = (from a in _empresaContext.Acciones
                            where (a.OrganizationID == _orgID)
                            join p in _empresaContext.PlanActions on a.ID equals p.AccionID
                            select p).Count();

            var numCorrective = _empresaContext.Acciones
                .Where(ac => ac.Categoria == CategoriasAccion.Correctiva).Count();
            var numEfectives = _empresaContext.Acciones
                .Where(ac => ac.Efectiva == true).Count();

            return Json(new { noConformance = noConformance, numPlans = numPlans, numCorrective = numCorrective, numEfectives = numEfectives }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetNoConformance()
        {
            try
            {
                _clientID = (int)Session["clientID"];
                _orgID = (int)Session["orgID"];
                _year = (string)Session["year"];
                Random random = new Random();
                var filename = "chart" + random.Next(1, 100) + ".jpg";
                var filePathName = "~/Images/" + filename;
                var datos = _chartHelper.GetAllNoConformance(_orgID);
                var image = "/Images/" + filename;
                return Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Riesgos", "Index"));
            }
        }

        [HttpGet]
        public ActionResult GetAllActions()
        {
            try
            {
                _clientID = (int)Session["clientID"];
                _orgID = (int)Session["orgID"];
                _year = (string)Session["year"];
                Random random = new Random();
                var filename = "chart" + random.Next(1, 100) + ".jpg";
                var filePathName = "~/Images/" + filename;
                var datos = _chartHelper.GetAllValueActions(_orgID);
                var image = "/Images/" + filename;
                return Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Riesgos", "Index"));
            }
        }

        [HttpGet]
        public ActionResult GetAllCorrectiveActions()
        {
            try
            {
                _clientID = (int)Session["clientID"];
                _orgID = (int)Session["orgID"];
                _year = (string)Session["year"];
                int year = Convert.ToInt32(_year);
                Random random = new Random();
                var filename = "chart" + random.Next(1, 100) + ".jpg";
                var filePathName = "~/Images/" + filename;
                var datos = _chartHelper.GetAllValueCorrectiveActions(year, _orgID);
                var image = "/Images/" + filename;
                return Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Riesgos", "Index"));
            }
        }
        [HttpGet]
        public ActionResult GetEfectiveActions()
        {
            try
            {
                _clientID = (int)Session["clientID"];
                _orgID = (int)Session["orgID"];
                _year = (string)Session["year"];
                int year = Convert.ToInt32(_year);
                Random random = new Random();
                var filename = "chart" + random.Next(1, 100) + ".jpg";
                var filePathName = "~/Images/" + filename;
                var datos = _chartHelper.GetAllEfectiveActions(year, _orgID);
                var image = "/Images/" + filename;
                return Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Riesgos", "Index"));
            }
        }

        [HttpGet]
        public async Task<ActionResult> ActionsMatrixPdf()
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
                var item = _empresaContext.Normas.Find(organization.StandardActions).Item;
                var fullPath = $"{_path}/4. ACTUAR/{item}/";
                var path = Server.MapPath(fullPath);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                Random random = new Random();
                var filename = "ActionsMatrix" + random.Next(1, 100) + ".Pdf";
                var filePathName = path + filename;
                var list = await _empresaContext.Acciones
                    .Where(a => a.OrganizationID == _orgID)
                    .Include(pa => pa.Planes)
                    .OrderByDescending(a => a.FechaSolicitud)
                    .ToListAsync();
                var modelo = _converterHelper.ToActionsMatrixVM(list);
                var document = _empresaContext.Documents.FirstOrDefault(d => d.ID == 10);
                ViewBag.formato = document.Formato;
                ViewBag.estandar = document.Estandar;
                ViewBag.titulo = document.Titulo;
                ViewBag.version = document.Version;
                ViewBag.fecha = DateTime.Now;
                var report = new ViewAsPdf("CreateActionsMatrix");
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
                var descript = "Matriz de acciones correctivas, preventivas y de mejora";
                var userID = (int)Session["userID"];
                Movimient movimient = new Movimient()
                {
                    ID = 0,
                    OrganizationID = _orgID,
                    NormaID = organization.StandardActions,
                    UserID = userID,
                    Descripcion = descript,
                    Document = fullName,
                    Year = year,
                    Item = item,
                    Ciclo = "A",
                    Type = type,
                    Path = path,
                    ClientID = _clientID
                };
                _empresaContext.Movimientos.Add(movimient);
                // Generar trazabilidad 
                var model1 = _converterHelper.Traceability(organization.StandardActions, year, _orgID, fullName);
                if (model1 != null)
                {
                    _empresaContext.SigueAnnualPlans.Add(model1);
                }

                _empresaContext.SaveChanges();
                return report;
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Acciones", "Index"));
            }
        }
    }
}