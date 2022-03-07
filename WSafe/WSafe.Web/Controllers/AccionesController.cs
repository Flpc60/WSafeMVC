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
    public class AccionesController : Controller
    {
        private readonly EmpresaContext _empresaContext;
        private readonly IComboHelper _comboHelper;
        private readonly IConverterHelper _converterHelper;
        public AccionesController(EmpresaContext empresaContext, IComboHelper comboHelper, IConverterHelper converterHelper)
        {
            _empresaContext = empresaContext;
            _comboHelper = comboHelper;
            _converterHelper = converterHelper;
        }

        // GET: Acciones
        public async Task<ActionResult> Index()
        {
            var consulta = new AccionService(new AccionRepository(_empresaContext));
            return View(await consulta.GetALL());
        }

        // GET: Acciones/Create
        public ActionResult Create()
        {
            var model = _converterHelper.ToAccionViewModelNew();
            return View(model);
        }

        // POST: Acciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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
                Trabajadores = _comboHelper.GetComboTrabajadores()
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> CreatePlanAccion(PlanAccion planAccion)
        {
            if (ModelState.IsValid)
            {
                _empresaContext.PlanesAccion.Add(planAccion);
                await _empresaContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return Json(planAccion, JsonRequestBehavior.AllowGet);
        }

        // GET: Seguimineto Plan acción/Create
        [HttpGet]
        public ActionResult CreateSeguiminetoPlan(int idAccion)
        {
            var model = new SeguimientoAccionVM
            {
                AccionID = idAccion,
                Trabajadores = _comboHelper.GetComboTrabajadores()
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> CreateSeguimientoPlan(SeguimientoAccion seguimientoAccion)
        {
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
        public async Task<ActionResult> ListarPlanAccion(int? idAccion)
        {
            if (idAccion == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //var result =  _empresaContext.PlanesAccion.Where(pa => pa.AccionID == 43).ToList();
            var data = (from pa in _empresaContext.PlanesAccion.Where(pa => pa.AccionID == 43).AsEnumerable()
                        select new
                        {
                            FechaInicial = pa.FechaInicial.ToString("dd/MM/yyyy"),
                            FechaFinal   = pa.FechaFinal.ToString("dd/MM/yyyy"),
                            Causa = pa.Causa,
                            Accion = pa.Accion,
                            Prioritaria = pa.Prioritaria,
                            Costos = pa.Costos
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}