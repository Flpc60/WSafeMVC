using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using WSafe.Web.Models;

namespace WSafe.Domain.Helpers.Implements
{
    public class ChartHelper : IChartHelper
    {
        private readonly EmpresaContext _empresaContext;
        private readonly IndicadorHelper _indicadorHelper;
        public ChartHelper(EmpresaContext empresaContext, IndicadorHelper indicadorHelper)
        {
            _empresaContext = empresaContext;
            _indicadorHelper = indicadorHelper;
        }
        public void DrawIndicadores(string tipo, string nombre, string ejeX, string ejeY, List<IndicadorDetallesViewModel> lista)
        {
            new Chart(width: 500, height: 300)
                 .AddTitle(nombre)
                      .AddSeries(chartType: tipo,
                        xValue: lista.MesAnn,
                          yValues: lista.Resultado)
                        .Write("bmp");
            return null;
        }
        public IEnumerable<IndicadorDetallesViewModel> GetAccidentesTrabajo(DateTime fechaInicial, DateTime fechaFinal)
        {
            var denominador = _indicadorHelper.NumeroTrabajadoresMes(fechaInicial, fechaFinal);

            var resultado = _empresaContext.Incidentes
                    .Where(at => at.FechaIncidente >= fechaInicial && at.FechaIncidente <= fechaFinal && at.CategoriaIncidente == Data.Entities.Incidentes.CategoriasIncidente.Accidente)
                    .OrderByDescending(at => at.FechaIncidente)
                    .GroupBy(at => new { at.FechaIncidente.Month, at.FechaIncidente.Year });

            var viewModel = new List<IndicadorDetallesViewModel>();
            foreach (var item in resultado)
            {
                viewModel.Add(new IndicadorDetallesViewModel
                {
                    MesAnn = ("{0}-{1}", item.key, item.FechaIncidente.Year),
                    Numerador = item.Count(),
                    Denominador = denominador,
                    Resultado = (item.Count() / denominador * 100),
                });
            }
            return viewModel;
        }

        public IEnumerable<IndicadorDetallesViewModel> GetAccidentesTrabajoInvestigados(DateTime fechaInicial, DateTime fechaFinal)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IndicadorDetallesViewModel> GetAccidentesTrabajoMortales(DateTime fechaInicial, DateTime fechaFinal)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IndicadorDetallesViewModel> GetAllIncidentes(DateTime fechaInicial, DateTime fechaFinal)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IndicadorDetallesViewModel> GetIncidentesInvestigados(DateTime fechaInicial, DateTime fechaFinal)
        {
            throw new NotImplementedException();
        }

        public void PrintIndicadores(string nombre, string ejeX, string ejeY, List<IndicadorDetallesViewModel> lista)
        {
            throw new System.NotImplementedException();
        }

        public void SaveIndicadores(string nombre, string ejeX, string ejeY, List<IndicadorDetallesViewModel> lista)
        {
            throw new System.NotImplementedException();
        }
    }
}