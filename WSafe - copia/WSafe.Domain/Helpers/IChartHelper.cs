using System;
using System.Collections.Generic;
using WSafe.Web.Models;

namespace WSafe.Domain.Helpers
{
    interface IChartHelper
    {
        void DrawImagen(string tipo, string nombre, List<IndicadorDetallesViewModel> lista);
        void SaveIndicadores(string nombre, string ejeX, string ejeY, List<IndicadorDetallesViewModel> lista);
        void PrintIndicadores(string nombre, string ejeX, string ejeY, List<IndicadorDetallesViewModel> lista);
        IEnumerable<IndicadorDetallesViewModel> GetFrecuenciaAccidentes(DateTime fechaInicial, DateTime fechaFinal);
        IEnumerable<IndicadorDetallesViewModel> GetAccidentesTrabajoMortales(DateTime fechaInicial, DateTime fechaFinal);
        IEnumerable<IndicadorDetallesViewModel> GetAccidentesTrabajoInvestigados(DateTime fechaInicial, DateTime fechaFinal);
        IEnumerable<IndicadorDetallesViewModel> GetAllIncidentes(DateTime fechaInicial, DateTime fechaFinal);
        IEnumerable<IndicadorDetallesViewModel> GetIncidentesInvestigados(DateTime fechaInicial, DateTime fechaFinal);
    }
}