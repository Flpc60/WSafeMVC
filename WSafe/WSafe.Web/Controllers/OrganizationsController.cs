using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using WSafe.Domain.Data.Entities;
using WSafe.Domain.Helpers;
using WSafe.Web.Filters;
using WSafe.Web.Models;

namespace WSafe.Web.Controllers
{
    // Datos básicos de la organización
    public class OrganizationsController : Controller
    {
        private int _clientID;
        private int _orgID;
        private string _year;
        private string _path;
        private readonly EmpresaContext _empresaContext;
        private readonly IComboHelper _comboHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IChartHelper _chartHelper;
        private readonly IGestorHelper _gestorHelper;
        public OrganizationsController(EmpresaContext empresaContext, IComboHelper comboHelper, IConverterHelper converterHelper, IGestorHelper gestorHelper)
        {
            _empresaContext = empresaContext;
            _comboHelper = comboHelper;
            _converterHelper = converterHelper;
            _gestorHelper = gestorHelper;
        }
        [AuthorizeUser(operation: 1, component: 1)]
        public async Task<ActionResult> Index()
        {
            _clientID = (int)Session["clientID"];
            _orgID = (int)Session["orgID"];
            _year = (string)Session["year"];
            _path = (string)Session["path"];
            var folders = _empresaContext.Clients.Find(_clientID).Folders;
            var cantidad = _empresaContext.Organizations
                .Where(o => o.ClientID == _clientID).Count();
            bool create = false;
            if(folders > cantidad) { create = true; }
            Organization organization = await _empresaContext.Organizations.FindAsync(_orgID);
            ViewBag.id = organization.ID;
            ViewBag.create = create;
            return View(organization);
        }
        public ActionResult GetAllCargos()
        {
            _orgID = (int)Session["orgID"];
            var cargos = _comboHelper.GetAllCargos().Where(c => c.OrganizationID ==_orgID);
            return Json(cargos, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> CreateNewCargo(Cargo model)
        {
            try
            {
                var message = "";
                if (ModelState.IsValid)
                {
                    _orgID = (int)Session["orgID"];
                    _clientID = (int)Session["clientID"];
                    model.OrganizationID = _orgID;
                    model.ClientID = _clientID;
                    _empresaContext.Cargos.Add(model);
                    await _empresaContext.SaveChangesAsync();
                    message = "El cargo fué ingresado correctamente!!";
                    return Json(new { data = true, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                message = "El cargo NO fué ingresado correctamente!!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { data = model, error = "El registro NO se ha ingresado correctamente" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteCargo(int id)
        {
            try
            {
                Cargo cargo = await _empresaContext.Cargos.FindAsync(id);

                // Validar retiro de cargo
                var message = "";
                var workers = _empresaContext.Trabajadores.Where(t => t.CargoID == id).Count();
                if (workers != 0)
                {
                    message = "Este cargo NO se puede eliminar por estar asignado  a un trabajador!!";
                    return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
                }

                message = "El cargo fué eliminado correctamente!!";
                _empresaContext.Cargos.Remove(cargo);
                await _empresaContext.SaveChangesAsync();
                return Json(new { data = true, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { error = "El registro NO se ha eliminado correctamente" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetAllZonas()
        {
            _orgID = (int)Session["orgID"];
            var zonas = _comboHelper.GetAllZonas().Where(z =>z.OrganizationID ==_orgID );
            return Json(zonas, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> CreateZone(Zona model)
        {
            var message = "";
            try
            {
                if (ModelState.IsValid)
                {
                    _orgID = (int)Session["orgID"];
                    _clientID = (int)Session["clientID"];
                    model.OrganizationID = _orgID;
                    model.ClientID = _clientID;
                    _empresaContext.Zonas.Add(model);
                    await _empresaContext.SaveChangesAsync();
                    message = "La zona fue ingresada correctamente !!";
                    return Json(new { data = true, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                message = "La zona NO fue ingresada correctamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                message = "La zona NO fue ingresada correctamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public async Task<ActionResult> DeleteZone(int id)
        {
            try
            {
                Zona zona = await _empresaContext.Zonas.FindAsync(id);

                // Validar retiro de zone
                var risks = _empresaContext.Riesgos.Where(r => r.ZonaID == id).Count();
                var message = "";
                if (risks != 0)
                {
                    message = "Esta zona no se puede eliminar por estar asignada a un riesgo!!";
                    return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                var incidents = _empresaContext.Incidentes.Where(i => i.ZonaID == id).Count();
                if (incidents != 0)
                {
                    message = "Esta zona no se puede eliminar por estar asignada a un incidente!!";
                    return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                var actions = _empresaContext.Acciones.Where(a => a.ZonaID == id).Count();
                if (actions != 0)
                {
                    message = "Esta zona no se puede eliminar por estar asignada a una acción!!";
                    return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                message = "Esta zona fué eliminada correctamente!!";

                _empresaContext.Zonas.Remove(zona);
                await _empresaContext.SaveChangesAsync();
                return Json(new { data = true, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { error = "El registro no se ha ingresado correctamente" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetAllProcess()
        {
            _orgID = (int)Session["orgID"];
            var process = _comboHelper.GetAllProcess().Where(p => p.OrganizationID == _orgID);
            return Json(process, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> CreateProcess(Proceso model)
        {
            var message = "";
            try
            {
                if (ModelState.IsValid)
                {
                    _orgID = (int)Session["orgID"];
                    _clientID = (int)Session["clientID"];
                    model.OrganizationID = _orgID;
                    model.ClientID = _clientID;
                    _empresaContext.Procesos.Add(model);
                    await _empresaContext.SaveChangesAsync();
                    message = "El proceso fué ingresado correctamente !!";
                    return Json(new { data = true, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                message = "El proceso NO fué ingresado correctamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                message = "El proceso NO fué ingresado correctamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteProcess(int id)
        {
            try
            {
                Proceso process = await _empresaContext.Procesos.FindAsync(id);

                // Validar retiro de process
                var risks = _empresaContext.Riesgos.Where(r => r.ProcesoID == id).Count();
                var message = "";
                if (risks != 0)
                {
                    message = "Este proceso no se puede eliminar por estar asignado a un riesgo!!";
                    return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                var incidents = _empresaContext.Incidentes.Where(i => i.ProcesoID == id).Count();
                if (incidents != 0)
                {
                    message = "Este proceso no se puede eliminar por estar asignado a un incidente!!";
                    return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                var actions = _empresaContext.Acciones.Where(a => a.ProcesoID == id).Count();
                if (actions != 0)
                {
                    message = "Este proceso no se puede eliminar por estar asignado a una acción!!";
                    return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                message = "Este proceso fué eliminado correctamente!!";

                _empresaContext.Procesos.Remove(process);
                await _empresaContext.SaveChangesAsync();
                return Json(new { data = true, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { error = "El registro no se ha ingresado correctamente" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetAllActivitys()
        {
            _orgID = (int)Session["orgID"];
            var activitys = _comboHelper.GetAllActivitys().Where(a => a.OrganizationID == _orgID);
            return Json(activitys, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> CreateActivity(Actividad model)
        {
            var message = "";
            try
            {
                if (ModelState.IsValid)
                {
                    _orgID = (int)Session["orgID"];
                    _clientID = (int)Session["clientID"];
                    model.OrganizationID = _orgID;
                    model.ClientID = _clientID;
                    _empresaContext.Actividades.Add(model);
                    await _empresaContext.SaveChangesAsync();
                    message = "La actividad fué imgresada correctamente !!";
                    return Json(new { data = true, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                message = "La actividad NO fué imgresada correctamente !!";
                return Json(new { data = true, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                message = "La actividad NO fué imgresada correctamente !!";
                return Json(new { data = true, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteActivity(int id)
        {
            try
            {
                Actividad activity = await _empresaContext.Actividades.FindAsync(id);

                // Validar retiro de activity
                var risks = _empresaContext.Riesgos.Where(r => r.ActividadID == id).Count();
                var message = "";
                if (risks != 0)
                {
                    message = "Esta actividad no se puede eliminar por estar asignada a un riesgo!!";
                    return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                var incidents = _empresaContext.Incidentes.Where(i => i.ActividadID == id).Count();
                if (incidents != 0)
                {
                    message = "Esta actividad no se puede eliminar por estar asignada a un incidente!!";
                    return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                var actions = _empresaContext.Acciones.Where(a => a.ActividadID == id).Count();
                if (actions != 0)
                {
                    message = "Esta actividad no se puede eliminar por estar asignada a una acción!!";
                    return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                message = "Esta actividad fué eliminada correctamente!!";

                _empresaContext.Actividades.Remove(activity);
                await _empresaContext.SaveChangesAsync();
                return Json(new { data = true, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { error = "El registro no se ha ingresado correctamente" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetAllTasks()
        {
            _orgID = (int)Session["orgID"];
            var tasks = _comboHelper.GetAllTareas().Where(t => t.OrganizationID == _orgID);
            return Json(tasks, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTask(Tarea model)
        {
            var message = "";
            try
            {
                if (ModelState.IsValid)
                {
                    _orgID = (int)Session["orgID"];
                    _clientID = (int)Session["clientID"];
                    model.OrganizationID = _orgID;
                    model.ClientID = _clientID;
                    _empresaContext.Tareas.Add(model);
                    await _empresaContext.SaveChangesAsync();
                    message = "La tarea fué ingresada correctamente !!";
                    return Json(new { data = true, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                message = "La tarea NO fué ingresada correctamente !!";
                return Json(new { data = true, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                message = "La tarea NO fué ingresada correctamente !!";
                return Json(new { data = true, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteTask(int id)
        {
            try
            {
                Tarea tarea = await _empresaContext.Tareas.FindAsync(id);

                // Validar retiro de tarea
                var risks = _empresaContext.Riesgos.Where(r => r.TareaID == id).Count();
                var message = "";
                if (risks != 0)
                {
                    message = "Esta tarea no se puede eliminar por estar asignada a un riesgo!!";
                    return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                var incidents = _empresaContext.Incidentes.Where(i => i.TareaID == id).Count();
                if (incidents != 0)
                {
                    message = "Esta tarea no se puede eliminar por estar asignada a un incidente!!";
                    return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                var actions = _empresaContext.Acciones.Where(a => a.TareaID == id).Count();
                if (actions != 0)
                {
                    message = "Esta tarea no se puede eliminar por estar asignada a una acción!!";
                    return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                message = "Esta tarea fué eliminada correctamente!!";

                _empresaContext.Tareas.Remove(tarea);
                await _empresaContext.SaveChangesAsync();
                return Json(new { data = true, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { error = "El registro no se ha ingresado correctamente" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Create()
        {
            _clientID = (int)Session["clientID"];
            var model = new Organization()
            {
                ClientID = _clientID,
                ControlDate = DateTime.Now,
                StandardActions = 43,
                StandardEvaluation = 52,
                StandardIncidents = 18,
                StandardMatrixRisk = 28,
                StandardSocioDemographic = 9,
                StandardUnsafeacts = 57
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Organization model)
        {
            try
            {
                _clientID = (int)Session["clientID"];
                _orgID = (int)Session["orgID"];
                _year = (string)Session["year"];
                _path = (string)Session["path"];

                model.ControlDate = DateTime.Now;
                model.RenovacionCurso = DateTime.Now;
                model.ResolucionLicencia = DateTime.Now;
                model.RenovacionLicencia = DateTime.Now;

                if (ModelState.IsValid)
                {
                    _empresaContext.Organizations.Add(model);
                    await _empresaContext.SaveChangesAsync();
                    var id = _empresaContext.Organizations.OrderByDescending(o => o.ID)
                        .Select(o => o.ID).First();

                    var path = $"~/ORG{id}/SG-SST/{_year}";
                    var directory = Server.MapPath(path);
                    _gestorHelper.CreateFolder(directory);
                    return RedirectToAction("Index","Home");
                }
                return View(model);
            }
            catch
            {
                return View(model);
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateOrganization(Organization model)
        {
            try
            {
                var message = "";
                if (ModelState.IsValid)
                {
                    var path = $"~/ORG{model.ID}/SG-SST/{model.Year}";
                    var directory = Server.MapPath(path);
                    _gestorHelper.CreateFolder(directory);

                    _empresaContext.Entry(model).State = EntityState.Modified;
                    await _empresaContext.SaveChangesAsync();
                    message = "La Organización ha sido actualizada correctamente!!";
                    return Json(new { data = true, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    message = "La Organización NO se ha podido actualizar correctamente!!";
                    return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Organizations", "Index"));
            }
        }

        [AuthorizeUser(operation: 4, component: 1)]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organization organization = await _empresaContext.Organizations.FindAsync(id);
            if (organization == null)
            {
                return HttpNotFound();
            }
            return View(organization);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Organization organization = await _empresaContext.Organizations.FindAsync(id);
            _empresaContext.Organizations.Remove(organization);
            await _empresaContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}