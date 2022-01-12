using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
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
        public void DrawImagen(string tipo, string nombre, IEnumerable<IndicadorDetallesViewModel> lista)
        {
            var filePathName = "D:/Projects/WSafe/WSafe.Web/Images/chart01.jpg";
            var chartImage = new Chart(width: 500, height: 300, theme: ChartTheme.Green);
            chartImage.AddTitle(nombre);
            chartImage.AddSeries(chartType: tipo,
                        xValue: lista, xField: "MesAnn",
                          yValues: lista, yFields: "Resultado");
            chartImage.Save(path: filePathName);
        }
        public IEnumerable<IndicadorDetallesViewModel> GetFrecuenciaAccidentes(DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                var denominador = _indicadorHelper.NumeroTrabajadoresMes(fechaInicial, fechaFinal);

                var result = from at in _empresaContext.Incidentes
                                where at.FechaIncidente >= fechaInicial && at.FechaIncidente <= fechaFinal && at.CategoriaIncidente == Data.Entities.Incidentes.CategoriasIncidente.Accidente
                                group at by new { at.FechaIncidente.Month, at.FechaIncidente.Year } into datosAgrupados
                                orderby datosAgrupados.Key
                                select new { Clave = datosAgrupados.Key, Datos = datosAgrupados };

                var viewModel = new List<IndicadorDetallesViewModel>();
                foreach (var grupo in result)
                {
                    viewModel.Add(new IndicadorDetallesViewModel
                    {
                        MesAnn = (grupo.Clave.Month + "-" + grupo.Clave.Year).ToString(),
                        Numerador = grupo.Datos.Count(),
                        Denominador = denominador,
                        Resultado = Convert.ToDecimal((double)grupo.Datos.Count() / (double)denominador * 100),
                    });
                }
                return viewModel;
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
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