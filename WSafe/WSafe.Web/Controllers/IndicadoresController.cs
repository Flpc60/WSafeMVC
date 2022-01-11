using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using WSafe.Domain.Helpers;
using WSafe.Web.Models;

namespace WSafe.Web.Controllers
{
    public class IndicadoresController : Controller
    {
        private readonly EmpresaContext _empresaContext;
        private readonly IComboHelper _comboHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IChartHelper _chartHelper;
        public IndicadoresController
            (
                EmpresaContext empresaContext,
                IComboHelper comboHelper,
                IConverterHelper converterHelper,
                IChartHelper chartHelper
            )
        {
            _empresaContext = empresaContext;
            _comboHelper = comboHelper;
            _converterHelper = converterHelper;
            _chartHelper = chartHelper;

        }

        // GET: Indicadores
        public async Task<ActionResult> Index()
        {
            var indicador = _empresaContext.Indicadores.FirstOrDefault(i => i.ID == 1);
            var result = _converterHelper.ToIndicadorViewModel(indicador);
            return View(result);
        }

        // GET: Indicadores/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IndicadorViewModel indicadorViewModel = await _empresaContext.IndicadorViewModels.FindAsync(id);
            if (indicadorViewModel == null)
            {
                return HttpNotFound();
            }
            return View(indicadorViewModel);
        }

        // GET: Indicadores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Indicadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,FechaInicial,FechaFinal,IndicadorID")] IndicadorViewModel indicadorViewModel)
        {
            if (ModelState.IsValid)
            {
                _empresaContext.IndicadorViewModels.Add(indicadorViewModel);
                await _empresaContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(indicadorViewModel);
        }

        // GET: Indicadores/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IndicadorViewModel indicadorViewModel = await _empresaContext.IndicadorViewModels.FindAsync(id);
            if (indicadorViewModel == null)
            {
                return HttpNotFound();
            }
            return View(indicadorViewModel);
        }

        // POST: Indicadores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,FechaInicial,FechaFinal,IndicadorID")] IndicadorViewModel indicadorViewModel)
        {
            if (ModelState.IsValid)
            {
                _empresaContext.Entry(indicadorViewModel).State = EntityState.Modified;
                await _empresaContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(indicadorViewModel);
        }

        // GET: Indicadores/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IndicadorViewModel indicadorViewModel = await _empresaContext.IndicadorViewModels.FindAsync(id);
            if (indicadorViewModel == null)
            {
                return HttpNotFound();
            }
            return View(indicadorViewModel);
        }

        // POST: Indicadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            IndicadorViewModel indicadorViewModel = await _empresaContext.IndicadorViewModels.FindAsync(id);
            _empresaContext.IndicadorViewModels.Remove(indicadorViewModel);
            await _empresaContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _empresaContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
