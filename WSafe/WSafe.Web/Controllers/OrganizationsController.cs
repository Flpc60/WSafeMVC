using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using WSafe.Domain.Data.Entities;
using WSafe.Domain.Helpers;
using WSafe.Web.Models;

namespace WSafe.Web.Controllers
{
    public class OrganizationsController : Controller
    {
        private readonly EmpresaContext _empresaContext;
        private readonly IComboHelper _comboHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IChartHelper _chartHelper;
        public OrganizationsController(EmpresaContext empresaContext, IComboHelper comboHelper, IConverterHelper converterHelper)
        {
            _empresaContext = empresaContext;
            _comboHelper = comboHelper;
            _converterHelper = converterHelper;
        }
        public async Task<ActionResult> Index()
        {
            var id = _empresaContext.Organizations.OrderByDescending(x => x.ID).First().ID;
            ViewBag.id = id;
            Organization organization = await _empresaContext.Organizations.FindAsync(id);
            return View(organization);
        }
        public ActionResult GetAllRoles()
        {
            var roles = _comboHelper.GetAllRoles();
            return View(roles);
        }

        [HttpPost]
        public async Task<ActionResult> CreateRole(Role model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _empresaContext.Roles.Add(model);
                    await _empresaContext.SaveChangesAsync();
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
                return Json(new { data = model, error = "El registro no se ha ingresado correctamente" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { data = model, error = "El registro no se ha ingresado correctamente" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteRole(int id)
        {
            try
            {
                Role role = await _empresaContext.Roles.FindAsync(id);

                // Validar retiro de rol
                var message = "";
                var users = _empresaContext.Users.Where(t => t.RoleID == id).Count();
                if (users != 0)
                {
                    message = "Este rol no se puede eliminar por estar asignado  a un usuario!!";
                    return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
                }

                message = "Este rol fué eliminado correctamente!!";
                _empresaContext.Roles.Remove(role);
                await _empresaContext.SaveChangesAsync();
                return Json(new { data = true, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { error = "El registro no se ha ingresado correctamente" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetAllCargos()
        {
            var cargos = _comboHelper.GetAllCargos();
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
                    message = "El cargo fué ingresado correctamente!!";
                    _empresaContext.Cargos.Add(model);
                    await _empresaContext.SaveChangesAsync();
                    return Json(new { data = true, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                message = "El cargo no fué ingresado correctamente!!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { data = model, error = "El registro no se ha ingresado correctamente" }, JsonRequestBehavior.AllowGet);
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
                    message = "Este cargo no se puede eliminar por estar asignado  a un trabajador!!";
                    return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
                }

                message = "El cargo fué eliminado correctamente!!";
                _empresaContext.Cargos.Remove(cargo);
                await _empresaContext.SaveChangesAsync();
                return Json(new { data = true, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { error = "El registro no se ha eliminado correctamente" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetAllZonas()
        {
            var zonas = _comboHelper.GetAllZonas();
            return Json(zonas, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> CreateZone(Zona model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _empresaContext.Zonas.Add(model);
                    await _empresaContext.SaveChangesAsync();
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
                return Json(new { data = model, error = "El registro no se ha ingresado correctamente" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { data = model, error = "El registro NO se ha ingresado correctamente" }, JsonRequestBehavior.AllowGet);
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
            var process = _comboHelper.GetAllProcess();
            return Json(process, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> CreateProcess(Proceso model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _empresaContext.Procesos.Add(model);
                    await _empresaContext.SaveChangesAsync();
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
                return Json(new { data = model, error = "El registro no se ha ingresado correctamente" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { data = model, error = "El registro no se ha ingresado correctamente" }, JsonRequestBehavior.AllowGet);
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
            var activitys = _comboHelper.GetAllActivitys();
            return Json(activitys, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> CreateActivity(Actividad model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _empresaContext.Actividades.Add(model);
                    await _empresaContext.SaveChangesAsync();
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
                return Json(new { data = model, error = "El registro no se ha ingresado correctamente" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { data = model, error = "El registro no se ha ingresado correctamente" }, JsonRequestBehavior.AllowGet);
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
            var tasks = _comboHelper.GetAllTareas();
            return Json(tasks, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTask(Tarea model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _empresaContext.Tareas.Add(model);
                    await _empresaContext.SaveChangesAsync();
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
                return Json(new { data = model, error = "El registro no se ha ingresado correctamente" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { data = model, error = "El registro no se ha ingresado correctamente" }, JsonRequestBehavior.AllowGet);
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,NIT,RazonSocial,Direccion,Municip,Department,Telefono,ARL,ClaseRiesgo,DocumentRepresent,NameRepresent,EconomicActivity,NumeroTrabajadores,Products,Mision,Vision,Objetivos,Procesos,Organigrama,TurnosAdministrativo,TurnosOperativo")] Organization organization)
        {
            var message = "";
            try
            {
                if (ModelState.IsValid)
                {
                    _empresaContext.Organizations.Add(organization);
                    await _empresaContext.SaveChangesAsync();
                    message = "La Organización ha sido actualizada correctamente!!";
                    return Json(new { data = true, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                message = "La Organización no se ha podido actualizar correctamente!!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                message = "La Organización no se ha podido actualizar correctamente!!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateOrganization(Organization organization)
        {
            var message = "";
            try
            {
                if (ModelState.IsValid)
                {
                    _empresaContext.Entry(organization).State = EntityState.Modified;
                    await _empresaContext.SaveChangesAsync();
                    message = "La Organización ha sido actualizada correctamente!!";
                    return Json(new { data = true, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                message = "La Organización NO se ha podido actualizar correctamente!!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                message = "La Organización NO se ha podido actualizar correctamente!!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }
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