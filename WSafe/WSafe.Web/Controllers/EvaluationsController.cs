using Rotativa;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
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
            try
            {
                var list = ( from c in _empresaContext.Califications
                    join n in _empresaContext.Normas on c.NormaID equals n.ID
                    where c.EvaluationID == id && n.Ciclo == ciclo
                    orderby n.Item
                    select new
                    {
                        ID = c.ID,
                        EvaluationID = c.EvaluationID,
                        NormaID = c.NormaID,
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
                    }
                ).ToList();
                    
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                var message = "La consulta NO se ha realizado exitosamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult UpdateCalification(int id)
        {
            try
            {
                var calification =
                        from c in _empresaContext.Califications
                        join n in _empresaContext.Normas on c.NormaID equals n.ID
                        where c.ID == id
                        orderby n.Item
                        select new
                        {
                            ID = c.ID,
                            EvaluationID = c.EvaluationID,
                            NormaID = c.NormaID,
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
                return Json(calification, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                var message = "La consulta NO se ha realizado exitosamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
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
                calification.Valoration = model.Valoration / 100;
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
                var list1 = ( from c in _empresaContext.Califications
                        join n in _empresaContext.Normas on c.NormaID equals n.ID
                        where c.EvaluationID == id
                        select new
                        {
                            ID = c.ID,
                            EvaluationID = c.EvaluationID,
                            NormaID = c.NormaID,
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
                        }).ToList();

                var list2 = new List<CalificationVM>();
                foreach (var item in list1)
                {
                    list2.Add(new CalificationVM
                    {
                        ID = item.ID,
                        NormaID = item.NormaID,
                        Ciclo = _gestorHelper.GetCiclo(item.Ciclo),
                        Standard = _gestorHelper.GetStandard(item.Standard),
                        Item = item.Item,
                        Name = item.Name,
                        Valor = item.Valor,
                        Cumple = item.Cumple,
                        NoCumple = item.NoCumple,
                        Justify = item.Justify,
                        NoJustify = item.NoJustify,
                        Valoration = item.Valoration,
                        Verification = item.Verification.Trim(),
                    });
                }

                var total = list2.Count();
                foreach (var item in list2)
                {
                    if (item.Cumple) { cumple++; }
                    if (item.NoCumple) { noCumple++; }
                    if (item.Justify) { noAplica++; }
                    if (item.NoJustify) { noAplica++; }
                }
                var aplica = total - noAplica;
                if (total > 0) { standarsResult = cumple / total; }
                if (aplica > 0) { aplicationsResult = cumple / aplica; }
                ValorationCategory category = ValorationCategory.ACEPTABLE;

                switch (standarsResult)
                {
                    case decimal v when (v > 85):
                        category = ValorationCategory.ACEPTABLE;
                        break;

                    case decimal v when (v >= 61 && v <= 85):
                        category = ValorationCategory.MODERADAMENTE_ACEPTABLE;
                        break;

                    case decimal v when (v < 60):
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
                return Json(list2, JsonRequestBehavior.AllowGet);
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
                    NormaID = model.NormaID,
                    FechaFinal = DateTime.Now,
                    Activity = model.Activity,
                    TrabajadorID = model.TrabajadorID,
                    Recurso = model.Recurso,
                    ActionCategory = model.ActionCategory,
                    Observation = model.Observation,
                    Fundamentos = model.Fundamentos
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
            try
            {
                var list =
                    from p in _empresaContext.PlanActivities
                    join n in _empresaContext.Normas on p.NormaID equals n.ID
                    where (p.EvaluationID == id)
                    orderby n.Item
                    select new
                    {
                        ID = p.ID,
                        TrabajadorID = p.TrabajadorID,
                        FechaFinal = p.FechaFinal,
                        Activity = p.Activity,
                        Recurso = p.Recurso,
                        ActionCategory = p.ActionCategory,
                        Observation = p.Observation,
                        Ciclo = n.Ciclo,
                        Item = n.Item,
                        Name = n.Name,
                        Fundamentos = p.Fundamentos
                    };

                var model = new List<PlanActivityVM>();
                foreach (var item in list)
                {
                    model.Add(new PlanActivityVM
                    {
                        ID = item.ID,
                        TrabajadorID = item.TrabajadorID,
                        FechaFinal = item.FechaFinal,
                        FechaCumplimiento = item.FechaFinal.ToString("yyyy-MM-dd"),
                        Activity = item.Activity,
                        TxtRecurso = _gestorHelper.GetRecurso(item.Recurso),
                        TxtActionCategory = _gestorHelper.GetActionCategory((int)item.ActionCategory),
                        Observation = item.Observation,
                        Ciclo = _gestorHelper.GetCiclo(item.Ciclo),
                        Item = item.Item,
                        Name = item.Name.Trim(),
                        Fundamentos = item.Fundamentos.Trim()
                    });
                }

                foreach (var item in model)
                {
                    item.Responsable = _empresaContext.Trabajadores.Find(item.TrabajadorID).NombreCompleto.ToUpper();
                }

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch
            {

                var message = "La conslta NO ser ha realizado correctamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<ActionResult> DeletePlanActivity(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanActivity plan = await _empresaContext.PlanActivities.FindAsync(id);
            var model = _converterHelper.ToPlanActivityVM(plan);
            if (model == null)
            {
                return HttpNotFound();
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> DeletePlanActivity(int id)
        {
            var message = "";
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _empresaContext.PlanActivities.FindAsync(id);
                    _empresaContext.PlanActivities.Remove(result);
                    await _empresaContext.SaveChangesAsync();
                    message = "El registro fué borrado correctamente !!";
                    return Json(new { data = true, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    message = "El registro NO fué borrado correctamente !!";
                    return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                message = "El registro NO fué borrado correctamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<ActionResult> UpdatePlanActivity(int id)
        {
            PlanActivity plan = await _empresaContext.PlanActivities.FindAsync(id);
            var model = _converterHelper.ToPlanActivityVM(plan);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UpdatePlanActivity(PlanActivity model)
        {
            var message = "";
            try
            {
                // Actualizar la BD
                _empresaContext.Entry(model).State = EntityState.Modified;
                await _empresaContext.SaveChangesAsync();
                message = "La actualización se ha realizado exitosamente !!";
                return Json(new { data = model.EvaluationID, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                message = "La actualización NO se ha realizado exitosamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<ActionResult>  GeneratePDF(int id)
        {
            Random random = new Random();
            var filename = "EstándaresMinimos" + random.Next(1, 100) + ".Pdf";
            var filePathName = "~/Documents/" + filename;
            var evaluation = await _empresaContext.Evaluations.FindAsync(id);
            var model = _converterHelper.ToMinimalsStandardsVM(evaluation);
            var report = new ViewAsPdf("MinimalsStandards", new { id = id });
            report.Model = model;
            report.FileName = filePathName;
            report.PageSize = Rotativa.Options.Size.Letter;
            report.Copies = 1;
            report.PageOrientation.GetValueOrDefault();
            report.FormsAuthenticationCookieName = FormsAuthentication.FormsCookieName;
            return report;
        }
    }
}