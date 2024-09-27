using Rotativa;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WSafe.Domain.Data;
using WSafe.Domain.Data.Entities;
using WSafe.Domain.Helpers;
using WSafe.Domain.Models;
using WSafe.Web.Filters;

namespace WSafe.Web.Controllers
{
    public class EvaluationsController : Controller
    {
        // Evaluación inicial del SG-SST
        private int _clientID;
        private int _orgID;
        private string _year;
        private string _path;
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
            _orgID = (int)Session["orgID"];
            var list = await _empresaContext.Evaluations
                .Where(r => r.OrganizationID == _orgID)
                .OrderBy(e => e.FechaEvaluation)
                .ToListAsync();
            var modelo = _converterHelper.ToEvaluationVMList(list);
            ViewBag.organization = $"GESTIÓN DE EVALUACIÓN DE DESEMPEÑO: {Session["organization"].ToString().Trim()}";
            return View(modelo);
        }

        // GET: Evaluations/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateEvaluation(bool indicador)
        {
            var message = "";
            try
            {
                _clientID = (int)Session["clientID"];
                _orgID = (int)Session["orgID"];
                var organization = await _empresaContext.Organizations.FindAsync(_orgID);
                var range = Int32.Parse(organization.Range.Trim());
                var evaluation = new Evaluation()
                {
                    ID = 0,
                    OrganizationID = _orgID,
                    FechaEvaluation = DateTime.Now,
                    ClientID = _clientID
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
            var result = await _empresaContext.Evaluations.FindAsync(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = result.ID;
            return View();
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
            Evaluation evaluation = await _empresaContext.Evaluations.FindAsync(id);
            if (evaluation == null)
            {
                return HttpNotFound();
            }
            return View(evaluation);
        }

        // POST: Evaluations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Evaluation evaluation = await _empresaContext.Evaluations.FindAsync(id);
            _empresaContext.Evaluations.Remove(evaluation);
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
                var list = (from c in _empresaContext.Califications
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
                decimal standardResult = 0;
                decimal aplicationResult = 0;
                decimal noAplicaResult = 0;
                decimal standardValor = 0;

                var list1 = (from c in _empresaContext.Califications
                             join n in _empresaContext.Normas on c.NormaID equals n.ID
                             where c.EvaluationID == id
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
                             }).ToList();

                var list2 = new List<CalificationVM>();
                foreach (var item in list1)
                {
                    standardResult += item.Valoration;
                    standardValor += item.Valor;
                    if (item.Justify) { noAplicaResult += item.Valoration; }
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
                aplicationResult = standardResult - noAplicaResult;
                ValorationCategory category = ValorationCategory.ACEPTABLE;
                decimal standardValoration = standardResult / standardValor * 100;
                decimal aplicationValoration = aplicationResult / standardValor * 100;

                switch (standardValoration)
                {
                    case decimal v when (v > 85):
                        category = ValorationCategory.ACEPTABLE;
                        break;

                    case decimal v when (v >= 61 && v <= 85):
                        category = ValorationCategory.MODERADAMENTE_ACEPTABLE;
                        break;

                    case decimal v when (v <= 60):
                        category = ValorationCategory.CRITICO;
                        break;
                }

                // Actualizar la BD
                Evaluation model = await _empresaContext.Evaluations.FindAsync(id);
                model.Cumple = cumple;
                model.NoCumple = noCumple;
                model.NoAplica = noAplica;
                model.StandarsResult = standardValoration;
                model.AplicationsResult = aplicationValoration;
                model.Category = category;
                model.ClientID = (int)Session["clientID"];
                model.OrganizationID = (int)Session["orgID"];
                _empresaContext.Entry(model).State = EntityState.Modified;
                await _empresaContext.SaveChangesAsync();
                return Json(new { data = list2, totales = standardResult, mensaj = message }, JsonRequestBehavior.AllowGet);
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
                    ID = model.ID,
                    EvaluationID = model.EvaluationID,
                    NormaID = model.NormaID,
                    FechaFinal = model.FechaFinal,
                    Activity = model.Activity,
                    TrabajadorID = model.TrabajadorID,
                    Financieros = model.Financieros,
                    Administrativos = model.Administrativos,
                    Tecnicos = model.Tecnicos,
                    Humanos = model.Humanos,
                    ActionCategory = model.ActionCategory,
                    Observation = model.Observation,
                    Fundamentos = model.Fundamentos,
                    AuditID = model.AuditID
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
                        Financieros = p.Financieros,
                        Administrativos = p.Administrativos,
                        Tecnicos = p.Tecnicos,
                        Humanos = p.Humanos,
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
                        Financieros = item.Financieros,
                        Administrativos = item.Administrativos,
                        Tecnicos = item.Tecnicos,
                        Humanos = item.Humanos,
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

        [HttpGet]
        public async Task<ActionResult> DeletePlanActivity(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                PlanActivity plan = await _empresaContext.PlanActivities.FindAsync(id);
                var model = _converterHelper.ToPlanActivityVM(plan);
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                var message = "El registro NO fué borrado correctamente !!";
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
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
                //PlanActivity plan = await _empresaContext.PlanActivities.FindAsync(model.ID);
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
        public async Task<ActionResult> GeneratePDF(int id)
        {
            // Configuración nombre archivo pdf
            _clientID = (int)Session["clientID"];
            _orgID = (int)Session["orgID"];
            _year = (string)Session["year"];
            var organization = _empresaContext.Organizations.Find(_orgID);
            var year = _year;
            var item = _empresaContext.Normas.Find(organization.StandardEvaluation).Item;
            var fullPath = "~/SG-SST/" + year + "/1. PLANEAR/" + item + "/";
            var path = Server.MapPath(fullPath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            Random random = new Random();
            var filename = "EstándaresMinimos" + random.Next(1, 100) + ".Pdf";
            var filePathName = path + filename;
            var evaluation = await _empresaContext.Evaluations.FindAsync(id);
            var model = _converterHelper.ToMinimalsStandardsVM(evaluation);
            var report = new ViewAsPdf("MinimalsStandards");
            report.Model = model;
            report.FileName = filePathName;
            report.PageSize = Rotativa.Options.Size.Letter;
            report.PageMargins = new Rotativa.Options.Margins(5, 5, 5, 5);
            report.Copies = 1;
            report.PageOrientation.GetValueOrDefault();
            report.FormsAuthenticationCookieName = FormsAuthentication.FormsCookieName;
            var pdfBytes = report.BuildFile(this.ControllerContext);
            System.IO.File.WriteAllBytes(filePathName, pdfBytes);

            //Generar archivo de movimiento
            var fullName = filename;
            var type = Path.GetExtension(filename).ToUpper();
            var descript = "Evaluación estándares";
            var userID = (int)Session["userID"];
            Movimient movimient = new Movimient()
            {
                ID = 0,
                OrganizationID = _orgID,
                NormaID = organization.StandardEvaluation,
                UserID = userID,
                Descripcion = descript,
                Document = fullName,
                Year = year,
                Item = item,
                Ciclo = "P",
                Type = type,
                Path = path,
                ClientID = _clientID
            };
            _empresaContext.Movimientos.Add(movimient);

            // Generar trazabilidad 
            var model1 = _converterHelper.Traceability(organization.StandardEvaluation, year, _orgID, fullName);
            if (model1 != null)
            {
                _empresaContext.SigueAnnualPlans.Add(model1);
            }

            _empresaContext.SaveChanges();

            return report;
        }

        [HttpGet]
        public async Task<ActionResult> DeleteEvaluation(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var message = "El registro NO ha sido eliminado exitosamente";
            Evaluation evaluation = await _empresaContext.Evaluations.FindAsync(id);
            var fecha = evaluation.FechaEvaluation.ToString("yyyy-MM-dd");
            return Json(new { data = evaluation, dateEvaluation = fecha, error = message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteEvaluation(int id)
        {
            Evaluation evaluation = await _empresaContext.Evaluations.FindAsync(id);
            _empresaContext.Evaluations.Remove(evaluation);
            await _empresaContext.SaveChangesAsync();
            return Json(new { data = true, message = "El registro ha sido eliminado exitosamente" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateMovimient(string Descripcion, int NormaID, HttpPostedFileBase fileLoad)
        {
            var message = "";
            try
            {
                var organizatión = _empresaContext.Organizations.OrderByDescending(x => x.ID).First();
                var year = organizatión.Year.ToString();
                var normaID = NormaID.ToString();
                var userID = (int)Session["userID"];
                var descript = Descripcion.ToString();
                var norma = _empresaContext.Normas.Find(NormaID);
                var cycle = norma.Ciclo.ToString();
                var ruta = norma.Ciclo.ToString();

                switch (cycle)
                {
                    case "P":
                        ruta = "1. PLANEAR/";
                        break;

                    case "H":
                        ruta = "2. HACER/";
                        break;
                    case "V":
                        ruta = "3. VERIFICAR/";
                        break;
                    case "A":
                        ruta = "4. ACTUAR/";
                        break;
                }

                var item = norma.Item.ToString();
                var fullPath = "~/SG-SST/" + ruta + year + "/" + item + "/";
                var path = Server.MapPath(fullPath);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                fileLoad.SaveAs(path + Path.GetFileName(fileLoad.FileName));
                var fullName = fileLoad.FileName;
                var type = Path.GetExtension(fileLoad.FileName).ToUpper();

                // Crear movimiento de documentos
                Movimient model = new Movimient()
                {
                    ID = 0,
                    OrganizationID = organizatión.ID,
                    NormaID = NormaID,
                    UserID = userID,
                    Descripcion = descript,
                    Document = fullName,
                    Year = year,
                    Item = item,
                    Ciclo = cycle,
                    Type = type,
                    Path = path
                };

                _empresaContext.Movimientos.Add(model);
                // Generar trazabilidad 
                var model1 = _converterHelper.Traceability(NormaID, year, _orgID, fullName);
                _empresaContext.SigueAnnualPlans.Add(model1);

                _empresaContext.SaveChanges();
                message = "El archivo ha sido subido correctamente !!";
                return Json(new { data = true, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                message += ex.Message;
                return Json(new { data = false, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetAllCalifications(int id)
        {
            try
            {
                Random random = new Random();
                var filename = "chart" + random.Next(1, 100) + ".jpg";
                var filePathName = "~/Images/" + filename;
                var datos = _chartHelper.GetAllCalifications(id);
                var image = "/Images/" + filename;
                return Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Riesgos", "Index"));
            }
        }

        [HttpGet]
        public ActionResult GetAllCalificationsStandard(int id)
        {
            try
            {
                Random random = new Random();
                var filename = "chart" + random.Next(1, 100) + ".jpg";
                var filePathName = "~/Images/" + filename;
                var datos = _chartHelper.GetAllCalificationsStandard(id);
                var image = "/Images/" + filename;
                return Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Riesgos", "Index"));
            }
        }
    }
}