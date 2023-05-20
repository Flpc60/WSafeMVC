using Rotativa;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using WSafe.Domain.Data.Entities;
using WSafe.Domain.Helpers;
using WSafe.Domain.Repositories.Implements;
using WSafe.Domain.Services.Implements;
using WSafe.Web.Filters;
using WSafe.Web.Models;

namespace WSafe.Web.Controllers
{
    // Gestión de riesgos en el SG-SST
    public class RiesgosController : Controller
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
        public RiesgosController(EmpresaContext empresaContext, IComboHelper comboHelper, IConverterHelper converterHelper, IChartHelper chartHelper)
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
                var list = await _empresaContext.Riesgos
                    .Where(r => r.OrganizationID == _orgID)
                    .OrderByDescending(cr => cr.NivelRiesgo)
                    .ToListAsync();
                var modelo = _converterHelper.ToRiesgoViewModelList(list);
                return View(modelo);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Riesgos", "Index"));
            }
        }

        [HttpGet]
        public async Task<ActionResult> PrintRiesgosToPdf()
        {
            try
            {
                // Configuración nombre archivo pdf
                _clientID = (int)Session["clientID"];
                _orgID = (int)Session["orgID"];
                _year = (string)Session["year"];
                var organization = _empresaContext.Organizations.Find(_orgID);
                var year = _year;
                var item = _empresaContext.Normas.Find(organization.StandardMatrixRisk).Item;
                var fullPath = $"~/SG-SST/{year}/2. HACER/{item}/";
                var path = Server.MapPath(fullPath);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                Random random = new Random();
                var filename = "MatrixRisk" + random.Next(1, 100) + ".Pdf";
                var filePathName = path + filename;
                var list = await _empresaContext.Riesgos
                    .Where(r => r.OrganizationID == _orgID)
                    .Include(mi => mi.MedidasIntervencion)
                    .OrderByDescending(cr => cr.NivelRiesgo)
                    .ToListAsync();
                var modelo = _converterHelper.ToRiesgoViewModelFul(list);
                var document = _empresaContext.Documents.FirstOrDefault(d => d.ID == 8);
                ViewBag.formato = document.Formato;
                ViewBag.estandar = document.Estandar;
                ViewBag.titulo = document.Titulo;
                ViewBag.version = document.Version;
                ViewBag.fecha = DateTime.Now;
                var report = new ViewAsPdf("GetAll");
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
                var descript = "Matriz de riesgos";
                var userID = (int)Session["userID"];
                Movimient movimient = new Movimient()
                {
                    ID = 0,
                    OrganizationID = organization.ID,
                    NormaID = organization.StandardMatrixRisk,
                    UserID = userID,
                    Descripcion = descript,
                    Document = fullName,
                    Year = year,
                    Item = item,
                    Ciclo = "H",
                    Type = type,
                    Path = path
                };
                _empresaContext.Movimientos.Add(movimient);
                _empresaContext.SaveChanges();
                return report;
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Riesgos", "Index"));
            }
        }

        [AuthorizeUser(operation: 2, component: 2)]
        public ActionResult Create()
        {
            var model = _converterHelper.ToRiesgoViewModelNew();
            ViewBag.trabajadores = _comboHelper.GetComboTrabajadores();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> CreateRiesgo(RiesgoViewModel model)
        {
            try
            {
                var message = "";
                model.ClientID = (int)Session["clientID"];
                model.OrganizationID = (int)Session["orgID"];
                if (ModelState.IsValid)
                {
                    if (model.ID == 0)
                    {
                        var consulta = new RiesgoService(new RiesgoRepository(_empresaContext));
                        var riesgo = await _converterHelper.ToRiesgoAsync(model, true);
                        var saved = await consulta.Insert(riesgo);
                        if (saved == null)
                        {
                            message = "El registro NO ha sido ingresado correctamente !!";
                            return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            message = "El registro ha sido ingresado correctamente !!";
                            var idRiesgo = _empresaContext.Riesgos.OrderByDescending(x => x.ID).First().ID;
                            return Json(new { data = idRiesgo, mensaj = message }, JsonRequestBehavior.AllowGet);
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
                return View("Error", new HandleErrorInfo(ex, "Riesgos", "Index"));
            }
        }

        [AuthorizeUser(operation: 3, component: 2)]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var result = await _empresaContext.Riesgos.FirstOrDefaultAsync(i => i.ID == id.Value);
            var model = _converterHelper.ToRiesgoViewModel(result);
            if (model == null)
            {
                return HttpNotFound();
            }
            ViewBag.trabajadores = _comboHelper.GetComboTrabajadores();
            ViewBag.riesgoID = id;
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateRiesgo(RiesgoViewModel model)
        {
            try
            {
                var message = "";
                model.ClientID = (int)Session["clientID"];
                model.OrganizationID = (int)Session["orgID"];
                if (ModelState.IsValid)
                {
                    var consulta = new RiesgoService(new RiesgoRepository(_empresaContext));
                    var result = await _converterHelper.ToRiesgoAsync(model, false);
                    await consulta.Update(result);
                    var idRiesgo = model.ID;
                    message = "La actualización se ha realizado exitosamente !!";
                    return Json(new { data = idRiesgo, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    message = "La actualización NO se ha realizado exitosamente !!";
                    return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Riesgos", "Index"));
            }
        }
        public IEnumerable<SelectListItem> GetPeligros()
        {
            List<SelectListItem> peligros = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Value = null,
                    Text = " "
                }
            };
            return peligros;
        }

        [HttpGet]
        public ActionResult GetPeligros(int id)
        {
            if (id != null)
            {
                var peligros = _comboHelper.GetComboPeligros(id);
                return Json(peligros, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        [HttpGet]
        public ActionResult GetZonas()
        {
            var zonas = _comboHelper.GetComboZonas();
            return Json(zonas, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetProcesos()
        {
            var procesos = _comboHelper.GetComboProcesos();
            return Json(procesos, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetActivities()
        {
            var activities = _comboHelper.GetComboActividades();
            return Json(activities, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetTareas()
        {
            var tareas = _comboHelper.GetComboTareas();
            return Json(tareas, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetTrabajadores()
        {
            var works = _comboHelper.GetComboTrabajadores();
            return Json(works, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> GetIntervenciones(int idRiesgo)
        {
            try
            {
                var result = await _empresaContext.Aplicaciones
                    .Where(a => a.RiesgoID == idRiesgo).ToListAsync();

                var intervenciones = _converterHelper.ToIntervencionesViewModel(result);

                return Json(intervenciones, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Riesgos", "Index"));
            }
        }

        [HttpGet]
        public async Task<ActionResult> UpdateIntervencion(int id)
        {
            try
            {
                var result = _empresaContext.Aplicaciones.Find(id);
                var intervencion = _converterHelper.ToAplicationVM(result);
                return Json(intervencion, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Riesgos", "Index"));
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateIntervencion(Aplicacion model)
        {
            var message = "";
            try
            {
                if (ModelState.IsValid)
                {
                    _empresaContext.Entry(model).State = EntityState.Modified;
                    var saved = await _empresaContext.SaveChangesAsync();
                    message = "El registro ha sido actualizado correctamente !!";
                    return Json(new { data = true, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                message = "El registro NO ha sido actualizado correctamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                message = "El registro NO ha sido actualizado correctamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddIntervenciones(AplicacionVM model)
        {
            var message = "";
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _converterHelper.ToAplicacionAsync(model, true);
                    _empresaContext.Aplicaciones.Add(result);
                    var saved = await _empresaContext.SaveChangesAsync();
                    var idAplica = _empresaContext.Aplicaciones.OrderByDescending(x => x.ID).First().ID;
                    message = "El registro ha sido ingresado correctamente !!";
                    return Json(new { data = idAplica, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                message = "El registro NO ha sido ingresado correctamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                message = "La transacción NO ha sido realizada correctamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<ActionResult> DeleteIntervencion(int? id)
        {
            var message = "";
            try
            {
                var result = _empresaContext.Aplicaciones.Find(id);
                var model = _converterHelper.ToAplicationVM(result);
                return Json(new { data = model, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                message = "No fué posible realizar esta transacción !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteIntervencion(int id)
        {
            var message = "";
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _empresaContext.Aplicaciones.FindAsync(id);
                    _empresaContext.Aplicaciones.Remove(result);
                    await _empresaContext.SaveChangesAsync();
                    message = "El registro fué borrado correctamente !!";
                    return Json(new { data = true, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                message = "El registro NO fué borrado correctamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                message = "El registro NO fué borrado correctamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [AuthorizeUser(operation: 4, component: 2)]
        public async Task<ActionResult> DeleteRiesgo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Riesgo riesgo = await _empresaContext.Riesgos.FindAsync(id);

            var message = "";
            var result = _empresaContext.Aplicaciones.Where(a => a.RiesgoID == id).Count();
            if (result != 0)
            {
                message = "Este riesgo tiene medidas de intervención pendientes por eliminar!!";
                return Json(new { data = false, error = message }, JsonRequestBehavior.AllowGet);
            }

            var model = _converterHelper.ToRiesgoVMUnit(riesgo);
            return Json(new { data = model, error = message }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> DeleteRiesgo(int id)
        {
            var consulta = new RiesgoService(new RiesgoRepository(_empresaContext));
            try
            {
                await consulta.Delete(id);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Riesgos", "Delete"));
            }

            return Json(new { data = true, message = "El registro ha sido eliminado exitosamente" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllIncidents(int idZona, int idProceso, int idActivity)
        {
            try
            {
                var result = await _empresaContext.Incidentes
                    .Where(i => i.ZonaID == idZona && i.ProcesoID == idProceso && i.ActividadID == idActivity).ToListAsync();

                foreach (var item in result)
                {
                    item.TxtFechaIncident = item.FechaIncidente.ToString("yyyy-MM-dd");
                }

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Riesgos", "Index"));
            }
        }

        [HttpGet]
        public ActionResult GetAllRisks()
        {
            try
            {
                _orgID = (int)Session["orgID"];
                Random random = new Random();
                var filename = "chart" + random.Next(1, 100) + ".jpg";
                var filePathName = "~/Images/" + filename;
                var datos = _chartHelper.GetAllValueRisks(_orgID);
                var image = "/Images/" + filename;
                return Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Riesgos", "Index"));
            }
        }

        [HttpGet]
        public ActionResult GetAllActivitys()
        {
            try
            {
                _orgID = (int)Session["orgID"];
                Random random = new Random();
                var filename = "chart" + random.Next(1, 100) + ".jpg";
                var filePathName = "~/Images/" + filename;
                var datos = _chartHelper.GetAllValueActivitys(_orgID);
                var image = "/Images/" + filename;
                return Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Riesgos", "Index"));
            }
        }

        [HttpGet]
        public ActionResult GetAllDangers()
        {
            try
            {
                _orgID = (int)Session["orgID"];
                _year = (string)Session["year"];
                var year = DateTime.Now.Year;
                Random random = new Random();
                var filename = "chart" + random.Next(1, 100) + ".jpg";
                var filePathName = "~/Images/" + filename;
                var datos = _chartHelper.GetFatorRiesgoOcupacional(year, _orgID);
                var image = "/Images/" + filename;
                return Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Riesgos", "Index"));
            }
        }

        [HttpGet]
        public ActionResult GetAllEfects()
        {
            try
            {
                _orgID = (int)Session["orgID"];
                Random random = new Random();
                var filename = "chart" + random.Next(1, 100) + ".jpg";
                var filePathName = "~/Images/" + filename;
                var datos = _chartHelper.GetAllValueEfects(_orgID);
                var image = "/Images/" + filename;
                return Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Riesgos", "Index"));
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
    }
}