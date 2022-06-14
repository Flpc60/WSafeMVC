using System.Data.Entity;
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
            return View(await _empresaContext.Organizations.ToListAsync());
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
                _empresaContext.Roles.Remove(role);
                await _empresaContext.SaveChangesAsync();
                return Json(role, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { error = "El registro no se ha ingresado correctamente" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetAllCargos()
        {
            var cargos = _comboHelper.GetAllCargos();
            return View(cargos);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCargo(Cargo model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _empresaContext.Cargos.Add(model);
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
        public async Task<ActionResult> DeleteCargo(int id)
        {
            try
            {
                Cargo cargo = await _empresaContext.Cargos.FindAsync(id);
                _empresaContext.Cargos.Remove(cargo);
                await _empresaContext.SaveChangesAsync();
                return Json(cargo, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { error = "El registro no se ha ingresado correctamente" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetAllZonas()
        {
            var zonas = _comboHelper.GetAllZonas();
            return View(zonas);
        }

        [HttpPost]
        public async Task<ActionResult> CreateZona(Zona model)
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
                return Json(new { data = model, error = "El registro no se ha ingresado correctamente" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteZona(int id)
        {
            try
            {
                Zona zona = await _empresaContext.Zonas.FindAsync(id);
                _empresaContext.Zonas.Remove(zona);
                await _empresaContext.SaveChangesAsync();
                return Json(zona, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { error = "El registro no se ha ingresado correctamente" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetAllProcess()
        {
            var process = _comboHelper.GetAllProcess();
            return View(process);
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
                _empresaContext.Procesos.Remove(process);
                await _empresaContext.SaveChangesAsync();
                return Json(process, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { error = "El registro no se ha ingresado correctamente" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetAllActivitys()
        {
            var activitys = _comboHelper.GetAllActivitys();
            return View(activitys);
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
                _empresaContext.Actividades.Remove(activity);
                await _empresaContext.SaveChangesAsync();
                return Json(activity, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { error = "El registro no se ha ingresado correctamente" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetAllTareas()
        {
            var tareas = _comboHelper.GetAllTareas();
            return View(tareas);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTarea(Tarea model)
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
        public async Task<ActionResult> DeleteTarea(int id)
        {
            try
            {
                Tarea tarea = await _empresaContext.Tareas.FindAsync(id);
                _empresaContext.Tareas.Remove(tarea);
                await _empresaContext.SaveChangesAsync();
                return Json(tarea, JsonRequestBehavior.AllowGet);
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
            if (ModelState.IsValid)
            {
                _empresaContext.Organizations.Add(organization);
                await _empresaContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(organization);
        }

        // GET: Organizations/Edit/5
        public async Task<ActionResult> Edit(int? id)
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

        // POST: Organizations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,NIT,RazonSocial,Direccion,Municip,Department,Telefono,ARL,ClaseRiesgo,DocumentRepresent,NameRepresent,EconomicActivity,NumeroTrabajadores,Products,Mision,Vision,Objetivos,Procesos,Organigrama,TurnosAdministrativo,TurnosOperativo")] Organization organization)
        {
            if (ModelState.IsValid)
            {
                _empresaContext.Entry(organization).State = EntityState.Modified;
                await _empresaContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(organization);
        }

        // GET: Organizations/Delete/5
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