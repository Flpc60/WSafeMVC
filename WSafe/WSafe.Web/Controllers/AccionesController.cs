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
            return View(model);
        }

        // POST: Acciones/Create
        [HttpPost]
        public ActionResult Create(AccionViewModel model)
        {
            return RedirectToAction("Index", model);
        }

        // GET: Acciones/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
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

            return View(model);
        }

        // POST: Acciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Accion accion)
        {
            if (ModelState.IsValid)
            {
                _empresaContext.Entry(accion).State = EntityState.Modified;
                await _empresaContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(accion);
        }

        // GET: Acciones/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accion accion = await _empresaContext.Acciones.FindAsync(id);
            if (accion == null)
            {
                return HttpNotFound();
            }
            return View(accion);
        }

        // POST: Acciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Accion accion = await _empresaContext.Acciones.FindAsync(id);
            _empresaContext.Acciones.Remove(accion);
            await _empresaContext.SaveChangesAsync();
            return RedirectToAction("Index");
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
        public async Task<ActionResult> CreatePlanAccion([Bind(Include = "ID, AccionID, FechaInicial, FechaFinal, Causa, Accion, TrabajadorID, Prioritaria, Costos")] PlanAccion planAccion)
        {
            if (planAccion.AccionID == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanAccion model = await _converterHelper.ToPlanAccionAsync(planAccion);


            if (ModelState.IsValid)
            {
                _empresaContext.PlanesAccion.Add(model);
                await _empresaContext.SaveChangesAsync();
                return RedirectToAction("Index");
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
                return RedirectToAction("Index");
            }
            return Json(seguimientoAccion, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAccion(Accion model)
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
        // GET: Plan acción/ListarPlanAccion
        [HttpGet]
        public async Task<ActionResult> ListarPlanAccion(int idAccion)
        {
            if (idAccion == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //TODO
            var planes = _empresaContext.PlanesAccion.Where(pa => pa.AccionID == idAccion).ToList();
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
            var plan = _empresaContext.PlanesAccion.FirstOrDefault(pa => pa.ID == ID);
            var model = _converterHelper.ToPlanAccionVM(plan);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UpdatePlanAccion([Bind(Include = "ID, AccionID, FechaInicial, FechaFinal, Causa, Accion, TrabajadorID, Prioritaria, Costos")] PlanAccion planAccion)
        {
            if (ModelState.IsValid)
            {
                _empresaContext.Entry(planAccion).State = EntityState.Modified;
                await _empresaContext.SaveChangesAsync();
            }

            return Json(planAccion, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> DeletePlanAccion(int id)
        {
            if (ModelState.IsValid)
            {
                PlanAccion accion = await _empresaContext.PlanesAccion.FindAsync(id);
                _empresaContext.PlanesAccion.Remove(accion);
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
        public async Task<ActionResult> UpdateSeguimientoAccion([Bind(Include = "ID, AccionID, FechaSeguimiento, Resultado, TrabajadorID")] SeguimientoAccion seguimientoAccion)
        {
            if (ModelState.IsValid)
            {
                _empresaContext.Entry(seguimientoAccion).State = EntityState.Modified;
                await _empresaContext.SaveChangesAsync();
            }

            return Json(seguimientoAccion, JsonRequestBehavior.AllowGet);
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
    }
}