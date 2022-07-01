using Rotativa;
using System;
using System.Linq;
using System.Web.Mvc;
using WSafe.Domain.Helpers;
using WSafe.Domain.Helpers.Implements;
using WSafe.Web.Filters;
using WSafe.Web.Models;

namespace WSafe.Web.Controllers
{
    public class IndicadoresController : Controller
    {
        private readonly EmpresaContext _empresaContext;
        private readonly IComboHelper _comboHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IChartHelper _chartHelper;
        private readonly IIndicadorHelper _indicadorHelper;
        private readonly IGestorHelper _gestorHelper;

        public IndicadoresController
            (
                EmpresaContext empresaContext,
                IComboHelper comboHelper,
                IConverterHelper converterHelper,
                IChartHelper chartHelper,
                IIndicadorHelper indicadorHelper,
                IGestorHelper gestorHelper
            )
        {
            _empresaContext = empresaContext;
            _comboHelper = comboHelper;
            _converterHelper = converterHelper;
            _chartHelper = chartHelper;
            _indicadorHelper = indicadorHelper;
            _gestorHelper = gestorHelper;

        }
        [AuthorizeUser(operation: 1, component: 5)]
        public ActionResult Index()
        {
            try
            {
                var year = DateTime.Now.Year;
                var mortales = _indicadorHelper.AccidentesTrabajoMortales(year);
                var accidentes = _indicadorHelper.AccidentesTrabajo(year);
                decimal proporcion = Convert.ToDecimal((double)mortales / (double)accidentes * 100);
                ViewBag.txtProporcion = Math.Round(proporcion, 2);
                ViewBag.txtIncidentes = _indicadorHelper.GetIncidentes(year);
                ViewBag.txtAusentismos = _indicadorHelper.DiasIncapacidadAccidentesTrabajo(year);
                ViewBag.txtMortales = mortales;
                ViewBag.txtAccidentes = accidentes;
                ViewBag.txtYear = year;
                return View();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult Details(int id, int year)
        {
            //TODO
            try
            {
                if (year != null)
                {
                    var periodo = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
                    var indicador = _empresaContext.Indicadores.FirstOrDefault(i => i.ID == id);
                    var result = _converterHelper.ToIndicadorViewModelNew(indicador, periodo, year);
                    ViewBag.Titulo = result.Titulo;
                    ViewBag.txtPeriodo = _gestorHelper.GetPeriodo(periodo);
                    ViewBag.Year = year.ToString();
                    ViewBag.id = result.ID;
                    return View(result);
                }
                return Json(new { error = "No hay información" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult FrecuenciaAccidentalidad(string periodo, int year)
        {
            return View("Details");
        }
        public ActionResult SeveridadAccidentes(string periodo, int year)
        {
            var result = new CreateIndicatorsViewModel();
            result.Indicadores = _comboHelper.GetComboIndicadores();
            ViewBag.Image = "~/Images/chart05.jpg";
            ViewBag.Titulo = "FICHA TÉCNICA INDICADOR SEVERIDAD ACCIDENTALIDAD";
            ViewBag.Periodo = periodo;
            ViewBag.Year = year;
            return View("Details");
        }
        public ActionResult ProrcionAccidentesMortales(string periodo, int year)
        {
            var result = new CreateIndicatorsViewModel();
            result.Indicadores = _comboHelper.GetComboIndicadores();
            ViewBag.Image = "~/Images/chart05.jpg";
            ViewBag.Titulo = "FICHA TÉCNICA INDICADOR PROPORCION DE ACCIDENTES MORTALES";
            ViewBag.Periodo = periodo;
            ViewBag.Year = year;
            return View("Details");
        }
        public ActionResult Accidentabilidad(string periodo, int year)
        {
            var result = new CreateIndicatorsViewModel();
            result.Indicadores = _comboHelper.GetComboIndicadores();
            ViewBag.Image = "~/Images/chart05.jpg";
            ViewBag.Titulo = "FICHA TÉCNICA INDICADOR ACCIDENTABILIDAD";
            ViewBag.Periodo = periodo;
            ViewBag.Year = year;
            return View("Details");
        }
        public ActionResult ProporcionAccionesEjecutadas(string periodo, int year)
        {
            var result = new CreateIndicatorsViewModel();
            result.Indicadores = _comboHelper.GetComboIndicadores();
            ViewBag.Image = "~/Images/chart05.jpg";
            ViewBag.Titulo = "FICHA TÉCNICA INDICADOR PROPORCION DE ACCIONES EJECUTADAS";
            ViewBag.Periodo = periodo;
            ViewBag.Year = year;
            return View("Details");
        }
        public ActionResult ProporcionAccionesEfectivas(string periodo, int year)
        {
            var result = new CreateIndicatorsViewModel();
            result.Indicadores = _comboHelper.GetComboIndicadores();
            ViewBag.Image = "~/Images/chart05.jpg";
            ViewBag.Titulo = "FICHA TÉCNICA INDICADOR PROPORCION DE ACCIONES EFECTIVAS";
            ViewBag.Periodo = periodo;
            ViewBag.Year = year;
            return View("Details");
        }
        public ActionResult DetailsPDF(string periodo, int year, string image, string titulo, int id)
        {
            var result = _empresaContext.Incidentes.FirstOrDefault(i => i.ID == id);
            var modelo = _converterHelper.ToIncidenteViewModel(result);
            var document = _empresaContext.Documents.FirstOrDefault(d => d.ID == id);
            ViewBag.formato = document.Formato;
            ViewBag.estandar = document.Estandar;
            ViewBag.titulo = document.Titulo;
            ViewBag.version = document.Version;
            ViewBag.fecha = DateTime.Now;
            ViewBag.Image = image;
            ViewBag.Titulo = titulo;
            ViewBag.Periodo = periodo;
            ViewBag.Year = year;
            return View(modelo);
        }
        public ActionResult GenerateIndicadorToPdf(int id, int year, string pdf)
        {
            var report = new ActionAsPdf("Details", new {id = id, year = year });
            report.FileName = pdf;
            report.PageSize = Rotativa.Options.Size.A4;
            report.PageOrientation = Rotativa.Options.Orientation.Landscape;
            report.PageWidth = 199;
            report.PageHeight = 199;
            return report;
        }
    }
}