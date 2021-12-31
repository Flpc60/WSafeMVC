using System.Collections.Generic;
using System.Web.Helpers;
using WSafe.Web.Models;

namespace WSafe.Domain.Helpers.Implements
{
    public class ChartHelper : IChartHelper
    {
        public void DrawIndicadores(string tipo, string titulo, string ejeX, string ejeY, List<IndicadorDetallesViewModel> lista)
        {
            new Chart(width: 500, height: 300)
                 .AddTitle(titulo)
                      .AddSeries(chartType: tipo,
                        xValue: new[] { "ASP.NET", "HTML5", "C Language", "C++" },
                          yValues: new[] { "90", "100", "80", "70" })
                        .Write("bmp");
            return null;
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