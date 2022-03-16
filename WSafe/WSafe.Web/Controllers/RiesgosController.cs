using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using WSafe.Domain.Helpers;
using WSafe.Domain.Repositories.Implements;
using WSafe.Domain.Services.Implements;
using WSafe.Web.Filters;
using WSafe.Web.Models;

namespace WSafe.Web.Controllers
{
    public class RiesgosController : Controller
    {
        private readonly EmpresaContext _empresaContext;
        private readonly IComboHelper _comboHelper;
        private readonly IConverterHelper _converterHelper;
        public RiesgosController(EmpresaContext empresaContext, IComboHelper comboHelper, IConverterHelper converterHelper)
        {
            _empresaContext = empresaContext;
            _comboHelper = comboHelper;
            _converterHelper = converterHelper;
        }

        // GET: Riesgos
        [AuthorizeUser(1,2)]
        public async Task<ActionResult> Index()
        {
            var list = await _empresaContext.Riesgos.Include(z => z.Zona)
                .Include(p => p.Proceso)
                .Include(a => a.Actividad)
                .Include(t => t.Tarea)
                .Include(cp => cp.Peligro)
                .OrderByDescending(cr => cr.NivelRiesgo)
                .ToListAsync();
            var modelo = _converterHelper.ToRiesgoViewModelList(list);
            return View(modelo);
        }

        // GET: Riesgos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var result = await _empresaContext.Riesgos.Include(z => z.Zona)
                .Include(p => p.Proceso)
                .Include(a => a.Actividad)
                .Include(t => t.Tarea)
                .Include(cp => cp.Peligro)
                .FirstOrDefaultAsync(i => i.ID == id.Value);

            if (result == null)
            {
                return HttpNotFound();
            }

            return View(result);
        }

        // GET: Riesgos/Create
        public ActionResult Create()
        {
            var riesgoView = _converterHelper.ToRiesgoViewModelNew();
            return View(riesgoView);
        }

        // POST: Riesgos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RiesgoViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var consulta = new RiesgoService(new RiesgoRepository(_empresaContext));
                    var riesgo = await _converterHelper.ToRiesgoAsync(model, true);
                    var saved = await consulta.Insert(riesgo);
                    if (saved != null)
                    {
                        return RedirectToAction("Index");
                    }
                }
                return View(model);
            }
            catch
            {
                return View(model);
            }
        }

        // GET: Riesgos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var result = await _empresaContext.Riesgos.Include(z => z.Zona)
                .Include(p => p.Proceso)
                .Include(a => a.Actividad)
                .Include(t => t.Tarea)
                .Include(cp => cp.Peligro)
                .Include(i => i.MedidasIntervencion)
                .FirstOrDefaultAsync(i => i.ID == id.Value);

            var riesgoViewModel = _converterHelper.ToRiesgoViewModel(result);

            if (riesgoViewModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.RiesgoID = id;

            return View(riesgoViewModel);
        }

        // POST: Riesgos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(RiesgoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var consulta = new RiesgoService(new RiesgoRepository(_empresaContext));
                    var result = await _converterHelper.ToRiesgoAsync(model, false);
                    await consulta.Update(result);
                    return RedirectToAction("Index");
                }
            }
            catch (DbEntityValidationException ex)
            {
                //return View("Error", new HandleErrorInfo(ex, "Riesgos", "Create"));
                throw ex;
            }

            return View(model);
        }

        // GET: Riesgos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var result = await _empresaContext.Riesgos.Include(z => z.Zona)
                .Include(p => p.Proceso)
                .Include(a => a.Actividad)
                .Include(t => t.Tarea)
                .Include(cp => cp.Peligro)
                .Include(i => i.MedidasIntervencion)
                .FirstOrDefaultAsync(i => i.ID == id.Value);

            try
            {
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Riesgos", "Create"));
            }

            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }


        // POST: Riesgos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var consulta = new RiesgoService(new RiesgoRepository(_empresaContext));
            try
            {
                await consulta.Delete(id);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Riesgos", "Create"));
            }

            return RedirectToAction("Index");
        }

        // GET: Controles
        public async Task<ActionResult> GetControles(int id)
        {
            // TODO
            var list = await _empresaContext.Controles
                .Where(c => c.RiesgoID == id)
                .ToListAsync();

            var result = await _empresaContext.Riesgos.Include(z => z.Zona)
                .Include(p => p.Proceso)
                .Include(a => a.Actividad)
                .Include(t => t.Tarea)
                .Include(cp => cp.Peligro)
                .FirstOrDefaultAsync(r => r.ID == id);

            ViewBag.Zona = result.Zona.Descripcion;
            ViewBag.Proceso = result.Proceso.Descripcion;
            ViewBag.Actividad = result.Actividad.Descripcion;
            ViewBag.Tarea = result.Tarea.Descripcion;
            ViewBag.Riesgo = result.Peligro.Descripcion;
            ViewBag.Nivel = result.CategoriaRiesgo;
            ViewBag.Expuestos = result.NroExpuestos;
            ViewBag.RiesgoID = result.ID;

            return View(list);
        }

        // POST: Riesgos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAccionConfirmed(int id)
        {
            var consulta = new AccionService(new AccionRepository(_empresaContext));
            try
            {
                await consulta.Delete(id);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Riesgos", "Create"));
            }

            return RedirectToAction("GetAllAcciones");
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
            if (idRiesgo == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var result = await _empresaContext.Aplicaciones
                .Where(a => a.ID == idRiesgo)
                .Include(t => t.Trabajador).ToListAsync();

            var intervenciones = _converterHelper.ToIntervencionesViewModel(result);

            if (intervenciones == null)
            {
                //return HttpNotFound();
            }
            return Json(intervenciones, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> AgregarIntervenciones(AplicacionVM model)
        {
            if (model == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _converterHelper.ToAplicacionAsync(model, true);
                    _empresaContext.Aplicaciones.Add(result);
                    var saved = await _empresaContext.SaveChangesAsync();
                }
            }
            catch
            {
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}