using System;
using WSafe.Domain.Models;

namespace WSafe.Domain.Helpers.Implements
{
    public interface IIndicadorHelper
    {
        int AccidentesTrabajo(int year, int month);
        int AccidentesTrabajoMortales(int year);
        int IncidentesInvestigados(int year);
        int GetIncidentes(int year);
        decimal ProporcionIncidentesInvestigados(int year);
        int DiasIncapacidadAccidentesTrabajo(int year);
        int DiasCargadosAccidentesTrabajo(int year);
        int NumeroCasosEnfermedadLaboral(DateTime fechaInicial, DateTime fechaFinal);
        int NumeroCasosNuevosEnfermedadLaboral(DateTime fechaInicial, DateTime fechaFinal);
        int DíasAusenciaIncapacidadLaboral(int year);
        int EnfermedadesIncidentesAusentismos(DateTime fechaInicial, DateTime fechaFinal);
        int NumeroTrabajadoresMes(DateTime fechaInicial, DateTime fechaFinal);
        decimal PromedioTrabajadores(int year, int _orgID);
        int NumeroDiasTrabajadosMes(int month, int year);
        decimal SeveridadAccidentalidad(DateTime fechaInicial, DateTime fechaFinal);
        decimal PrevalenciaEnfermedad(DateTime fechaInicial, DateTime fechaFinal);
        decimal IncidenciaEnfermedad(DateTime fechaInicial, DateTime fechaFinal);
        int NumeroACPAccidentes(DateTime fechaInicial, DateTime fechaFinal);
        int NumeroACP(DateTime fechaInicial, DateTime fechaFinal);
        decimal ProporcionACPAccidentes(DateTime fechaInicial, DateTime fechaFinal);
        DashboardVM GetIndicators(int year, int month, int _orgID);
        int NumeroHorasTrabajadasMes(int year, int month, int orgID);
    }
}