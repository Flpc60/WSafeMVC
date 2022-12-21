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
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateEvaluation()
        {
            var message = "";
            try
            {
                var idEmpresa = _empresaContext.Organizations.OrderByDescending(x => x.ID).First().ID;
                var evaluation = new Evaluation()
                {
                    ID = 0,
                    OrganizationID = idEmpresa,
                    FechaEvaluation = DateTime.Now
                };
                _empresaContext.Evaluations.Add(evaluation);
                await _empresaContext.SaveChangesAsync();
                var evaluationID = _empresaContext.Evaluations.OrderByDescending(x => x.ID).First().ID;

                var normas = _empresaContext.Normas.ToList();

                foreach (var item in normas)
                {
                    var calification = new Calification()
                    {
                        ID = 0,
                        EvaluationID = evaluationID,
                        NormaID = item.ID,
                        Cumple = false,
                        Justifica = false,
                        Valoration = 0,
                        Observation = ""
                    };
                    _empresaContext.Califications.Add(calification);
                }

                await _empresaContext.SaveChangesAsync();
                message = "El registro ha sido ingresado correctamente !!";
                return Json(new { data = evaluationID, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                message = "El registro NO ha sido ingresado correctamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
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

        [HttpGet]
        public ActionResult GetCalifications(int id)
        {
            if (id != null)
            {
                var list =
                    from c in _empresaContext.Califications
                    join n in _empresaContext.Normas on c.NormaID equals n.ID
                    where c.EvaluationID == id
                    select new CalificationVM
                    {
                        ID = c.ID,
                        Ciclo = n.Ciclo,
                        Standard = n.Standard,
                        Item = n.Item,
                        Name = n.Name,
                        Valor = n.Valor,
                        Cumple = c.Cumple,
                        Justify = c.Justifica,
                        Valoration = c.Valoration,
                        Observation = c.Observation
                    };

                var model = _converterHelper.ToCalificationVMList(list);
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }
    }
}