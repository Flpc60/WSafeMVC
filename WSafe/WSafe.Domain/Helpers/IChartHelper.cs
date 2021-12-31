using System;
using System.Collections.Generic;
using WSafe.Web.Models;

namespace WSafe.Domain.Helpers
{
    interface IChartHelper
    {
        void DrawIndicadores(string nombre, string ejeX, string ejeY, List<IndicadorDetallesViewModel> lista);
        void SaveIndicadores(string nombre, string ejeX, string ejeY, List<IndicadorDetallesViewModel> lista);
        void PrintIndicadores(string nombre, string ejeX, string ejeY, List<IndicadorDetallesViewModel> lista);
        IEnumerable<IndicadorDetallesViewModel> CreateList(DateTime fechaInicial, DateTime fechaFinal);
    }
}