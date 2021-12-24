using System;

namespace WSafe.Domain.Helpers.Implements
{
    interface IIndicadorHelper
    {
        int AccientesTrabajo(DateTime fechaInicial, DateTime fechaFinal);
        int AccientesTrabajoMortales(DateTime fechaInicial, DateTime fechaFinal);
        int AccientesTrabajoInvestigados(DateTime fechaInicial, DateTime fechaFinal);
        int DiasIncapacidadAccientesTrabajo(DateTime fechaInicial, DateTime fechaFinal);
        int NumeroCasosEnfermedadLabora(DateTime fechaInicial, DateTime fechaFinal);
        int NumeroCasosNuevosEnfermedadLabora(DateTime fechaInicial, DateTime fechaFinal);
        int DíasAusenciaIncapacidadLaboralComun(DateTime fechaInicial, DateTime fechaFinal);
        int EnfermedadesIncidentesAusentismos(DateTime fechaInicial, DateTime fechaFinal);
    }
}