using System.Collections.Generic;
using WSafe.Domain.Models;

namespace WSafe.Domain.Helpers
{
    public interface IChartHelper
    {
        void DrawImagen(string arcchivo, string tipo, string nombre, IEnumerable<IndicadorDetallesViewModel> lista);
        void SaveIndicadores(string nombre, string ejeX, string ejeY, List<IndicadorDetallesViewModel> lista);
        void PrintIndicadores(string nombre, string ejeX, string ejeY, List<IndicadorDetallesViewModel> lista);
        IEnumerable<IndicadorDetallesViewModel> GetFrecuenciaAccidentes(int[] periodo, int year, int _orgID);
        IEnumerable<IndicadorDetallesViewModel> GetSeveridadAccidentalidad(int[] periodo, int year, int _orgID);
        IEnumerable<IndicadorDetallesViewModel> GetAccidentesTrabajoMortales(int[] periodo, int year, int _orgID);
        IEnumerable<IndicadorDetallesViewModel> GetAccidentesTrabajoInvestigados(int[] periodo, int year);
        IEnumerable<IndicadorDetallesViewModel> GetAllIncidentes(int[] periodo, int year);
        IEnumerable<IndicadorDetallesViewModel> GetIncidentesInvestigados(int[] periodo, int year);
        IEnumerable<IndicadorDetallesViewModel> GetAusentismoCausaMedica(int[] periodo, int year);
        IEnumerable<IndicadorDetallesViewModel> GetFatorRiesgoOcupacional(int year, int _orgID);
        IEnumerable<IndicadorDetallesViewModel> GetAllValueRisks(int _orgID);
        IEnumerable<IndicadorDetallesViewModel> GetAllValueActivitys(int _orgID);
        IEnumerable<IndicadorDetallesViewModel> GetAllValueDangers(int _orgID);
        IEnumerable<IndicadorDetallesViewModel> GetAllValueEfects(int _orgID);
        IEnumerable<IndicadorDetallesViewModel> GetAllNoConformance(int _orgID);
        IEnumerable<IndicadorDetallesViewModel> GetAllValueActions(int _orgID);
        IEnumerable<IndicadorDetallesViewModel> GetAllValueCorrectiveActions(int year, int _orgID);
        IEnumerable<IndicadorDetallesViewModel> GetAllCalifications(int id);
        IEnumerable<IndicadorDetallesViewModel> GetAllCalificationsStandard(int id);
        IEnumerable<IndicadorDetallesViewModel> GetAllEfectiveActions(int year, int _orgID);
        IEnumerable<IndicadorDetallesViewModel> GetAllScholarship(int year, int _orgID);
        IEnumerable<IndicadorDetallesViewModel> GetAllWorkAreas(int year, int _orgID);
        IEnumerable<IndicadorDetallesViewModel> GetAllOcupaciones(int year, int _orgID);
        IEnumerable<IndicadorDetallesViewModel> GetAllTiposVinculacion(int year, int _orgID);
        IEnumerable<IndicadorDetallesViewModel> GetAllEstadosCivil(int year, int _orgID);
        IEnumerable<IndicadorDetallesViewModel> GetAllTiposVivienda(int year, int _orgID);
        IEnumerable<IndicadorDetallesViewModel> GetAllTiposJornada(int year, int _orgID);
        IEnumerable<IndicadorDetallesViewModel> GetAllNumeroHijos(int year, int _orgID);
        IEnumerable<IndicadorDetallesViewModel> GetAnnualPlanActivitiesAll(int year, int _orgID);
    }
}