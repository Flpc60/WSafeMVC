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
        private readonly IGestorHelper _gestorHelper;
        public EvaluationsController(EmpresaContext empresaContext, IComboHelper comboHelper, IConverterHelper converterHelper, IChartHelper chartHelper, IGestorHelper gestorHelper)
        {
            _empresaContext = empresaContext;
            _comboHelper = comboHelper;
            _converterHelper = converterHelper;
            _chartHelper = chartHelper;
            _gestorHelper = gestorHelper;
        }

        [AuthorizeUser(operation: 1, component: 2)]
        // GET: Evaluations
        public async Task<ActionResult> Index()
        {
            var list = await _empresaContext.Evaluations
                .OrderBy(e => e.FechaEvaluation)
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
            ViewBag.trabajadores = _comboHelper.GetComboTrabajadores();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateEvaluation(bool indicador)
        {
            var message = "";
            try
            {
                var idEmpresa = _empresaContext.Organizations.OrderByDescending(x => x.ID).First().ID;
                var organization = await _empresaContext.Organizations.FindAsync(idEmpresa);
                var range = Int32.Parse(organization.Range.Trim());
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

                if (indicador)
                {
                    normas = _empresaContext.Normas
                            .Where(n => n.Range <= range)
                            .ToList();
                }

                foreach (var item in normas)
                {
                    var calification = new Calification()
                    {
                        ID = 0,
                        EvaluationID = evaluationID,
                        NormaID = item.ID,
                        Cumple = false,
                        NoCumple = false,
                        Justify = false,
                        NoJustify = false,
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
        public ActionResult GetCalifications(int id, string ciclo)
        {
            if (id != null)
            {
                var list =
                    from c in _empresaContext.Califications
                    join n in _empresaContext.Normas on c.NormaID equals n.ID
                    where (c.EvaluationID == id && n.Ciclo == ciclo)
                    orderby n.Item
                    select new CalificationVM
                    {
                        ID = c.ID,
                        Ciclo = n.Ciclo,
                        Standard = n.Standard,
                        Item = n.Item,
                        Name = n.Name,
                        Valor = n.Valor,
                        Cumple = c.Cumple,
                        NoCumple = c.NoCumple,
                        Justify = c.Justify,
                        NoJustify = c.NoJustify,
                        Valoration = c.Valoration,
                        Verification = n.Verification.Trim()
                    };

                var model = _converterHelper.ToCalificationVMList(list);
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult UpdateCalification(int id)
        {
            var list =
                    from c in _empresaContext.Califications
                    join n in _empresaContext.Normas on c.NormaID equals n.ID
                    where c.ID == id
                    orderby n.Item
                    select new CalificationVM
                    {
                        ID = c.ID,
                        EvaluationID = c.EvaluationID,
                        Ciclo = n.Ciclo,
                        Standard = n.Standard,
                        Item = n.Item,
                        Name = n.Name,
                        Valor = n.Valor,
                        Cumple = c.Cumple,
                        NoCumple = c.NoCumple,
                        Justify = c.Justify,
                        NoJustify = c.NoJustify,
                        Valoration = c.Valoration,
                        Observation = c.Observation,
                        Verification = n.Verification
                    };
            var model = _converterHelper.ToCalificationVMList(list);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> EditCalification(CalificationVM model)
        {
            var message = "";
            try
            {
                Calification calification = await _empresaContext.Califications.FindAsync(model.ID);
                calification.Cumple = model.Cumple;
                calification.NoCumple = model.NoCumple;
                calification.Justify = model.Justify;
                calification.NoJustify = model.NoJustify;
                calification.Valoration = model.Valoration/100;
                calification.Observation = model.Observation;
                // Actualizar la BD
                _empresaContext.Entry(calification).State = EntityState.Modified;
                await _empresaContext.SaveChangesAsync();
                message = "La actualización se ha realizado exitosamente !!";
                return Json(new { data = calification.EvaluationID, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                message = "La actualización NO se ha realizado exitosamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<ActionResult> UpdateEvaluation(int id)
        {
            var message = "";
            try
            {
                var cumple = 0;
                var noCumple = 0;
                var noAplica = 0;
                decimal standarsResult = 0;
                decimal aplicationsResult = 0;
                var result =
                        from c in _empresaContext.Califications
                        join n in _empresaContext.Normas on c.NormaID equals n.ID
                        where c.EvaluationID == id
                        select new CalificationVM
                        {
                            ID = c.ID,
                            EvaluationID = c.EvaluationID,
                            Ciclo = n.Ciclo,
                            Standard = n.Standard,
                            Item = n.Item,
                            Name = n.Name,
                            Valor = n.Valor,
                            Cumple = c.Cumple,
                            NoCumple = c.NoCumple,
                            Justify = c.Justify,
                            NoJustify = c.NoJustify,
                            Valoration = c.Valoration,
                            Observation = c.Observation
                        };
                var list = _converterHelper.ToCalificationVMList(result);
                var total = list.Count();

                foreach (var item in list)
                {
                    if (item.Cumple) { cumple++; }
                    if (item.NoCumple) { noCumple++; }
                    if (item.Justify) { noAplica++; }
                    if (item.NoJustify) { noAplica++; }
                }
                var aplica = total - noAplica;
                if (total>0) { standarsResult = cumple / total; }
                if (aplica > 0) { aplicationsResult = cumple / aplica; }
                ValorationCategory category = ValorationCategory.ACEPTABLE;

                switch (total)
                {
                    case int v when(v > 85):
                        category = ValorationCategory.ACEPTABLE;
                        break;

                    case int v when (v >= 61 && v <= 85):
                        category = ValorationCategory.MODERADAMENTE_ACEPTABLE;
                        break;

                    case int v when (v < 60):
                        category = ValorationCategory.CRITICO;
                        break;
                }

                // Actualizar la BD
                Evaluation model = await _empresaContext.Evaluations.FindAsync(id);
                model.Cumple = cumple;
                model.NoCumple = noCumple;
                model.NoAplica = noAplica;
                model.StandarsResult = standarsResult;
                model.AplicationsResult = aplicationsResult;
                model.Category = category;
                _empresaContext.Entry(model).State = EntityState.Modified;
                await _empresaContext.SaveChangesAsync();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                message = "La actualización NO se ha realizado exitosamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public async Task<ActionResult> CreatePlanActivity(PlanActivity model)
        {
            var message = "";
            try
            {
                var planActivity = new PlanActivity()
                {
                    ID = 0,
                    EvaluationID = model.EvaluationID,
                    FechaFinal = DateTime.Now,
                    Activity = model.Activity,
                    TrabajadorID = model.TrabajadorID,
                    Presupuesto = model.Presupuesto,
                    ActionCategory = ActionCategories.Sin_Iniciar,
                    Observation = model.Observation,
                    Ciclo = model.Ciclo,
                    Item = model.Item,
                    Name = model.Name
                };
                _empresaContext.PlanActivities.Add(planActivity);
                await _empresaContext.SaveChangesAsync();
                return Json(new { data = true, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                message = "El registro NO ha sido ingresado correctamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetPlanActivities(int id)
        {
            if (id != null)
            {
                var list = _empresaContext.PlanActivities
                    .Where(p => p.EvaluationID == id)
                    .ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }
    }
}