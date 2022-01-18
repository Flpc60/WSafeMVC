using System;

namespace WSafe.Domain.Helpers.Implements
{
    public interface IIndicadorHelper
    {
        int AccidentesTrabajo(DateTime fechaInicial, DateTime fechaFinal);
        int AccidentesTrabajoMortales(DateTime fechaInicial, DateTime fechaFinal);
        decimal ProporcionAccidentesInvestigados(DateTime fechaInicial, DateTime fechaFinal);
        int IncidentesInvestigados(DateTime fechaInicial, DateTime fechaFinal);
        int TotalIncidentes(DateTime fechaInicial, DateTime fechaFinal);
        decimal ProporcionIncidentesInvestigados(DateTime fechaInicial, DateTime fechaFinal);
        int DiasIncapacidadAccidentesTrabajo(DateTime fechaInicial, DateTime fechaFinal);
        int DiasCargadosAccidentesTrabajo(DateTime fechaInicial, DateTime fechaFinal);
        int NumeroCasosEnfermedadLaboral(DateTime fechaInicial, DateTime fechaFinal);
        int NumeroCasosNuevosEnfermedadLaboral(DateTime fechaInicial, DateTime fechaFinal);
        int DíasAusenciaIncapacidadLaboral(DateTime fechaInicial, DateTime fechaFinal);
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