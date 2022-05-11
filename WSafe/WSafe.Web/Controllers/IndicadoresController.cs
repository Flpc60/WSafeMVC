﻿using Rotativa;
using System;
using System.Data.Entity;
using System.Linq;
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
        public ActionResult CreateNuevaConsulta()
        {
            var result = new CreateIndicatorsViewModel();
            result.Indicadores = _comboHelper.GetComboIndicadores();
            return View(result);
        }

        [HttpPost]
        public ActionResult GetFrecuenciaAccidentes(int[] periodo, int year) 
        {
            if (periodo != null)
            {
                var indicador = _empresaContext.Indicadores.FirstOrDefault(i =>i.ID == 1);
                var result = _converterHelper.ToIndicadorViewModelNew(indicador, periodo, year);
                return View("Index", result);
            }
            return View("Index");
        }
        public ActionResult FrecuenciaAccidentes(string periodo, int year)
        {
            var result = new CreateIndicatorsViewModel();
            result.Indicadores = _comboHelper.GetComboIndicadores();
            ViewBag.Image = "~/Images/chart05.jpg";
            ViewBag.Titulo = "FICHA TÉCNICA INDICADOR FRECUENCIA DE ACCIDENTALIDAD";
            ViewBag.Periodo = periodo;
            ViewBag.Year = year;
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
        public ActionResult Details(string periodo, int year, string image, string titulo)
        {
            var result = new CreateIndicatorsViewModel();
            result.Indicadores = _comboHelper.GetComboIndicadores();
            ViewBag.Image = image;
            ViewBag.Titulo = titulo;
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

        [HttpGet]
        public ActionResult PrintIndicadorToPdf(string periodo, int year, string image, string titulo, int id, string pdf)
        {
            var report = new ActionAsPdf("DetailsPDF", new { Id = id, Periodo = periodo, Year = year, Image = image, Titulo = titulo });
            report.FileName = pdf;
            report.PageSize = Rotativa.Options.Size.A4;
            report.PageOrientation = Rotativa.Options.Orientation.Landscape;
            report.PageWidth = 199;
            report.PageHeight = 199;
            return report;
        }
    }
}