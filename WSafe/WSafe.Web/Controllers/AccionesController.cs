using Rotativa;
using System;
using System.Data.Entity;
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

namespace WSafe.Web.Controllers
{
    // Getionar todas las acciones correctvas, preventivas y de mejora de la organización
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

        [AuthorizeUser(operation: 1, component: 4)]
        public async Task<ActionResult> Index()
        {
            var list = await _empresaContext.Acciones
                .OrderByDescending(a => a.FechaSolicitud)
                .ToListAsync();
            var modelo = _converterHelper.ToAccionVMList(list);
            return View(modelo);
        }

        [AuthorizeUser(operation: 2, component: 4)]
        public ActionResult Create()
        {
            var model = _converterHelper.ToAccionViewModelNew();
            ViewBag.Categorias = _comboHelper.GetAllCausas();
            ViewBag.fechaSolicitud = DateTime.Now;
            ViewBag.fechaCierre = DateTime.Now;

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
            ViewBag.fechaSolicitud = model.FechaSolicitud;
            ViewBag.fechaCierre = model.FechaCierre;

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateAccion([Bind(Include="ID, ZonaID, ProcesoID, ActividadID, TareaID, FechaSolicitud, Categoria, TrabajadorID, " +
            "FuenteAccion, Descripcion, EficaciaAntes, EficaciaDespues, FechaCierre, Efectiva, ActionCategory")] Accion model)
        {
            var message = "";
            try
            {
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
            var message = "";
            try
            {
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
            if (idAccion == null)
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
        public async Task<ActionResult> CreateSeguimientoPlan(Seguimiento seguimientoAccion)
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
                    Seguimiento model = await _converterHelper.ToSeguimientoAccionAsync(seguimientoAccion);
                    _empresaContext.Seguimientos.Add(seguimientoAccion);
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

        [HttpPost]
        public async Task<ActionResult> CreateAccion(Accion model)
        {
            if (model == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var message = "";

            try
            {
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
        public async Task<ActionResult> ListarPlanAccion(int idAccion)
        {
            if (idAccion == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //TODO
            var planes = _empresaContext.PlanActions.Where(pa => pa.AccionID == idAccion).ToList();
            var result = _converterHelper.ToPlanAccionVMList(planes);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> ListarSeguimientoAccion(int idAccion)
        {
            if (idAccion == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //TODO
            var seguimientos = _empresaContext.Seguimientos.Where(sa => sa.AccionID == idAccion).ToList();
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
        public async Task<ActionResult> UpdatePlanAccion([Bind(Include = "ID, AccionID, FechaInicial, FechaFinal, Causa, Accion, TrabajadorID, Prioritaria, Costos, ActionCategory")] PlanAction planAccion)
        {
            var message = "";
            try
            {
                if (ModelState.IsValid)
                {
                    _empresaContext.Entry(planAccion).State = EntityState.Modified;
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
            var seguimiento = _empresaContext.Seguimientos.FirstOrDefault(sa => sa.ID == ID);
            var model = _converterHelper.ToSeguimientoAccionVM(seguimiento);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateSeguimientoAccion([Bind(Include = "ID, AccionID, FechaSeguimiento, Resultado, TrabajadorID")] Seguimiento model)
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
            Seguimiento sigue = await _empresaContext.Seguimientos.FindAsync(id);
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
                Seguimiento accion = await _empresaContext.Seguimientos.FindAsync(id);
                _empresaContext.Seguimientos.Remove(accion);
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
        public async Task<ActionResult> PrintAccionesToPdf(int id)
        {
            Random random = new Random();
            var filename = "ReporteAccion" + random.Next(1, 100) + ".Pdf";
            var filePathName = "~/Documents/" + filename;

            var result = await _empresaContext.Acciones.FirstOrDefaultAsync(i => i.ID == id);
            var model = _converterHelper.ToAccionVMFull(result, 1);
            ViewBag.planes = model.Planes.Count();
            ViewBag.sigue = model.Seguimientos.Count();
            var report = new ViewAsPdf("Details", new { id = id });
            report.Model = model;
            report.FileName = filePathName;
            report.PageSize = Rotativa.Options.Size.A4;
            report.Copies = 1;
            report.PageOrientation.GetValueOrDefault();
            report.FormsAuthenticationCookieName = FormsAuthentication.FormsCookieName;
            return report;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllPlans()
        {

            var noConformance = _empresaContext.Acciones.Count();
            var numPlans = _empresaContext.PlanActions.Count();

            return Json(new { noConformance = noConformance, numPlans = numPlans },JsonRequestBehavior.AllowGet);
        }
    }
}