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
    public class MovimientosController : Controller
    {
        private int _operation;
        private int _roleID;
        private readonly EmpresaContext _empresaContext;
        private readonly IComboHelper _comboHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IChartHelper _chartHelper;
        public MovimientosController(EmpresaContext empresaContext, IComboHelper comboHelper, IConverterHelper converterHelper, IChartHelper chartHelper)
        {
            _empresaContext = empresaContext;
            _comboHelper = comboHelper;
            _converterHelper = converterHelper;
            _chartHelper = chartHelper;
        }

        [AuthorizeUser(operation: 1, component: 2)]


        // GET: Movimientos
        public async Task<ActionResult> Index()
        {
            return View();
        }

        // GET: Movimientos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimient movimientVM = await _empresaContext.Movimientos.FindAsync(id);
            if (movimientVM == null)
            {
                return HttpNotFound();
            }
            return View(movimientVM);
        }

        // GET: Movimientos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movimientos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,OrganizationID,Ciclo,Item,Name,NormaID,Descripcion,Document,Year")] MovimientVM movimientVM)
        {
            if (ModelState.IsValid)
            {
                _empresaContext.MovimientVMs.Add(movimientVM);
                await _empresaContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(movimientVM);
        }

        // GET: Movimientos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovimientVM movimientVM = await _empresaContext.MovimientVMs.FindAsync(id);
            if (movimientVM == null)
            {
                return HttpNotFound();
            }
            return View(movimientVM);
        }

        // POST: Movimientos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,OrganizationID,Ciclo,Item,Name,NormaID,Descripcion,Document,Year")] MovimientVM movimientVM)
        {
            if (ModelState.IsValid)
            {
                _empresaContext.Entry(movimientVM).State = EntityState.Modified;
                await _empresaContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(movimientVM);
        }

        // GET: Movimientos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovimientVM movimientVM = await _empresaContext.MovimientVMs.FindAsync(id);
            if (movimientVM == null)
            {
                return HttpNotFound();
            }
            return View(movimientVM);
        }

        // POST: Movimientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MovimientVM movimientVM = await _empresaContext.MovimientVMs.FindAsync(id);
            _empresaContext.MovimientVMs.Remove(movimientVM);
            await _empresaContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult GetNormas(string ciclo)
        {
            if (ciclo != null)
            {
                var result =
                    from n in _empresaContext.Normas
                    where n.Ciclo == ciclo
                    select new
                    {
                        ID = n.ID,
                        Name = n.Item + " " + n.Name
                    };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetMovimientos(string ciclo)
        {
            if (ciclo != null)
            {
                var data = _empresaContext.Movimientos.Where(m => m.Ciclo == ciclo).ToList();
                var result = _converterHelper.ToListMovimientos(data);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }
    }
}
