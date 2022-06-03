using Rotativa;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using WSafe.Domain.Data.Entities;
using WSafe.Domain.Helpers;
using WSafe.Domain.Repositories.Implements;
using WSafe.Domain.Services.Implements;
using WSafe.Web.Models;

namespace WSafe.Web.Controllers
{
    // Getionar todas las acciones correctvas, preventivas y de mejora
    public class AccionesController : Controller
    {
        // Inyecciones
        private readonly EmpresaContext _empresaContext;
        private readonly IComboHelper _comboHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IGestorHelper _gestorHelper;
        public AccionesController
            (EmpresaContext empresaContext,
            IComboHelper comboHelper,
            IConverterHelper converterHelper,
            IGestorHelper gestorHelper)
        {
            _empresaContext = empresaContext;
            _comboHelper = comboHelper;
            _converterHelper = converterHelper;
            _gestorHelper = gestorHelper;
        }

        // Listar todas las acciones abiertas en orden de fecha de solicitud
        public async Task<ActionResult> Index()
        {
            var list = await _empresaContext.Acciones
                .Where(a => a.Estado == false)
                .OrderByDescending(a => a.FechaSolicitud)
                .ToListAsync();
            var modelo = _converterHelper.ToAccionVMList(list);
            return View(modelo);
        }

        // GET: Acciones/Create
        public ActionResult Create()
        {
            var model = _converterHelper.ToAccionViewModelNew();
            ViewBag.Categorias = _comboHelper.GetAllCausas();
            return View(model);
        }

        // POST: Acciones/Create
        [HttpPost]
        public ActionResult Create(AccionViewModel model)
        {
            return RedirectToAction("Index", model);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            //TODO
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var result = await _empresaContext.Acciones.FirstOrDefaultAsync(i => i.ID == id.Value);
            var model = _converterHelper.ToAccionViewModel(result);
            if (model == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccionID = id;

            ViewBag.Categorias = _comboHelper.GetAllCausas();

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateAccion(Accion model)
        {
            if (ModelState.IsValid)
            {
                var consulta = new AccionService(new AccionRepository(_empresaContext));
                var result = await _converterHelper.ToAccionAsync(model, false);
                await consulta.Update(result);
            }
            var idAccion = model.ID;
            return Json(idAccion, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
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
            var sigue = _empresaContext.SeguimientosAccion.Where(s => s.AccionID == id).Count();
            if (sigue != 0)
            {
                message = "Esta acción tiene seguimientos pendientes por eliminar!!";
                return Json(new { data = false, error = message }, JsonRequestBehavior.AllowGet);
            }

            var model = _converterHelper.ToAccionViewModel(accion);
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

        // GET: Plan acción/Create
        [HttpGet]
        public ActionResult CreatePlanAccion(int idAccion)
        {
            if (idAccion == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = new PlanAccionVM
            {
                AccionID = idAccion,
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> CreatePlanAccion(PlanAction model)
        {
            if (model.AccionID == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                PlanAction result = await _converterHelper.ToPlanAccionAsync(model);
                _empresaContext.PlanActions.Add(result);
                var saved = await _empresaContext.SaveChangesAsync();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        // GET: Seguimineto Plan acción/Create
        [HttpGet]
        public ActionResult CreateSeguimientoPlan(int idAccion)
        {
            if (idAccion == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
            SeguimientoAccion model = await _converterHelper.ToSeguimientoAccionAsync(seguimientoAccion);

            if (ModelState.IsValid)
            {
                _empresaContext.SeguimientosAccion.Add(seguimientoAccion);
                await _empresaContext.SaveChangesAsync();
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            return Json(seguimientoAccion, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAccion([Bind(Include="ID, ZonaID, ProcesoID, ActividadID, TareaID, FechaSolicitud, Categoria, TrabajadorID, " +
            "FuenteAccion, Descripcion, EficaciaAntes, EficaciaDespues, FechaCierre, Efectiva, Estado")] Accion model)
        {
            if (model == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _converterHelper.ToAccionAsync(model, true);
                    _empresaContext.Acciones.Add(result);
                    var saved = await _empresaContext.SaveChangesAsync();
                }
            }
            catch
            {
            }

            var idAccion = _empresaContext.Acciones.OrderByDescending(x => x.ID).First().ID;
            return Json(idAccion, JsonRequestBehavior.AllowGet);
        }
        // GET: Plan acción/ListarPlanAction
        [HttpGet]
        public async Task<ActionResult> ListarPlanAccion(int idAccion)
        {
            if (idAccion == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //TODO
            var planes = _empresaContext.PlanActions.Where(pa => pa.AccionID == idAccion).ToList();
            var result = _converterHelper.ToPlanAccionVMList(planes);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: Seguimiento acción/ListarSeguimientoAccion
        [HttpGet]
        public async Task<ActionResult> ListarSeguimientoAccion(int idAccion)
        {
            if (idAccion == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //TODO
            var seguimientos = _empresaContext.SeguimientosAccion.Where(sa => sa.AccionID == idAccion).ToList();
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
        public async Task<ActionResult> UpdatePlanAccion([Bind(Include = "ID, AccionID, FechaInicial, FechaFinal, Causa, Accion, TrabajadorID, Prioritaria, Costos")] PlanAction planAccion)
        {
            if (ModelState.IsValid)
            {
                _empresaContext.Entry(planAccion).State = EntityState.Modified;
                await _empresaContext.SaveChangesAsync();
            }

            return Json(planAccion, JsonRequestBehavior.AllowGet);
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
            if (ModelState.IsValid)
            {
                _empresaContext.Entry(model).State = EntityState.Modified;
                await _empresaContext.SaveChangesAsync();
            }

            return Json(model, JsonRequestBehavior.AllowGet);
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
        public async Task<ActionResult> Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var result = await _empresaContext.Acciones.FirstOrDefaultAsync(i => i.ID == id);

            var model = _converterHelper.ToAccionVMFull(result, 1);
            ViewBag.planes = model.Planes.Count();
            ViewBag.sigue = model.Seguimientos.Count();
            return View(model);
        }

        [HttpGet]
        public ActionResult PrintAccionesToPdf(int id)
        {
            var report = new ActionAsPdf("Details", new { id = id });
            report.FileName = "ReporteAcciones.Pdf";
            report.PageSize = Rotativa.Options.Size.A4;
            report.Copies = 1;
            report.PageOrientation.GetValueOrDefault();

            return report;
        }
    }
}