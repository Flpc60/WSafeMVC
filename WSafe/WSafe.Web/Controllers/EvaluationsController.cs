using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using WSafe.Domain.Helpers;
using WSafe.Web.Filters;
using WSafe.Web.Models;

namespace WSafe.Web.Controllers
{
    public class EvaluationsController : Controller
    {
        private int _operation;
        private int _roleID;
        private readonly EmpresaContext _empresaContext;
        private readonly IComboHelper _comboHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IChartHelper _chartHelper;
        public EvaluationsController(EmpresaContext empresaContext, IComboHelper comboHelper, IConverterHelper converterHelper, IChartHelper chartHelper)
        {
            _empresaContext = empresaContext;
            _comboHelper = comboHelper;
            _converterHelper = converterHelper;
            _chartHelper = chartHelper;
        }

        [AuthorizeUser(operation: 1, component: 2)]
        // GET: Evaluations
        public async Task<ActionResult> Index()
        {
            var list = await _empresaContext.Evaluations
                .OrderByDescending(e => e.FechaEvaluation)
                .ToListAsync();
            var modelo = _converterHelper.ToEvaluationVMList(list);
            return View(modelo);
        }

        // GET: Evaluations/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvaluationVM evaluationVM = await _empresaContext.EvaluationVMs.FindAsync(id);
            if (evaluationVM == null)
            {
                return HttpNotFound();
            }
            return View(evaluationVM);
        }

        // GET: Evaluations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Evaluations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,OrganizationID,FechaEvaluation,Cumple,NoCumple,NoAplica,StandarsResult,AplicationsResult,Activitys,Ejecutadas,Avance,Category,Color")] EvaluationVM evaluationVM)
        {
            if (ModelState.IsValid)
            {
                _empresaContext.EvaluationVMs.Add(evaluationVM);
                await _empresaContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(evaluationVM);
        }

        // GET: Evaluations/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvaluationVM evaluationVM = await _empresaContext.EvaluationVMs.FindAsync(id);
            if (evaluationVM == null)
            {
                return HttpNotFound();
            }
            return View(evaluationVM);
        }

        // POST: Evaluations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,OrganizationID,FechaEvaluation,Cumple,NoCumple,NoAplica,StandarsResult,AplicationsResult,Activitys,Ejecutadas,Avance,Category,Color")] EvaluationVM evaluationVM)
        {
            if (ModelState.IsValid)
            {
                _empresaContext.Entry(evaluationVM).State = EntityState.Modified;
                await _empresaContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(evaluationVM);
        }

        // GET: Evaluations/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvaluationVM evaluationVM = await _empresaContext.EvaluationVMs.FindAsync(id);
            if (evaluationVM == null)
            {
                return HttpNotFound();
            }
            return View(evaluationVM);
        }

        // POST: Evaluations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            EvaluationVM evaluationVM = await _empresaContext.EvaluationVMs.FindAsync(id);
            _empresaContext.EvaluationVMs.Remove(evaluationVM);
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