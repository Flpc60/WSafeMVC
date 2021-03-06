using System;

namespace WSafe.Domain.Helpers.Implements
{
    public interface IIndicadorHelper
    {
        int AccidentesTrabajo(int year);
        int AccidentesTrabajoMortales(int year);
        decimal ProporcionAccidentesInvestigados(int year);
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
        decimal PromedioTrabajadores(DateTime fechaInicial, DateTime fechaFinal);
        int NumeroDiasTrabajadosMes();
        decimal FrecuenciaAccidentalidad(DateTime fechaInicial, DateTime fechaFinal);
        decimal SeveridadAccidentalidad(DateTime fechaInicial, DateTime fechaFinal);
        decimal ProporcionAccidentesMortales(DateTime fechaInicial, DateTime fechaFinal);
        decimal PrevalenciaEnfermedad(DateTime fechaInicial, DateTime fechaFinal);
        decimal IncidenciaEnfermedad(DateTime fechaInicial, DateTime fechaFinal);
        int NumeroACPAccidentes(DateTime fechaInicial, DateTime fechaFinal);
        int NumeroACP(DateTime fechaInicial, DateTime fechaFinal);
        decimal ProporcionACPAccidentes(DateTime fechaInicial, DateTime fechaFinal);
    }
}