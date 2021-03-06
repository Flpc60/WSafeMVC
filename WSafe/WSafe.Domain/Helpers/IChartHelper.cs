using System;
using System.Collections.Generic;
using WSafe.Web.Models;

namespace WSafe.Domain.Helpers
{
    public interface IChartHelper
    {
        void DrawImagen(string arcchivo, string tipo, string nombre, IEnumerable<IndicadorDetallesViewModel> lista);
        void SaveIndicadores(string nombre, string ejeX, string ejeY, List<IndicadorDetallesViewModel> lista);
        void PrintIndicadores(string nombre, string ejeX, string ejeY, List<IndicadorDetallesViewModel> lista);
        IEnumerable<IndicadorDetallesViewModel> GetFrecuenciaAccidentes(int[] periodo, int year);
        IEnumerable<IndicadorDetallesViewModel> GetSeveridadAccidentalidad(int[] periodo, int year);
        IEnumerable<IndicadorDetallesViewModel> GetAccidentesTrabajoMortales(int[] periodo, int year);
        IEnumerable<IndicadorDetallesViewModel> GetAccidentesTrabajoInvestigados(int[] periodo, int year);
        IEnumerable<IndicadorDetallesViewModel> GetAllIncidentes(int[] periodo, int year);
        IEnumerable<IndicadorDetallesViewModel> GetIncidentesInvestigados(int[] periodo, int year);
        IEnumerable<IndicadorDetallesViewModel> GetAusentismoCausaMedica(int[] periodo, int year);
        IEnumerable<IndicadorDetallesViewModel> GetFatorRiesgoOcupacional();
    }
}