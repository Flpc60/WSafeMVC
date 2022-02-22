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
            return View();
        }

        // POST: Acciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Accion accion)
        {
            if (ModelState.IsValid)
            {
                _empresaContext.Acciones.Add(accion);
                await _empresaContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(accion);
        }

        // GET: Acciones/Edit/5
        public async Task<ActionResult> Edit(int? id)
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

        // POST: Plan acción
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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

        // POST: Seguimiento Plan acción
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<ActionResult> SeguiminentoPlan(SeguimientoAccion seguimientoAccion)
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
        public async Task<ActionResult> CreateAccion(AccionViewModel model)
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

            var idAccion = from acc in _empresaContext.Acciones orderby acc.ID ascending
                           select new { ID = acc.ID };

            return Json(idAccion, JsonRequestBehavior.AllowGet);
        }
    }
}