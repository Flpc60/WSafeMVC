namespace WSafe.Domain.Helpers.Implements
{
    interface IIndicadorHelper
    {
        int AccientesTrabajo(string fechaInicial, string fechaFinal);
        int AccientesTrabajoMortales(string fechaInicial, string fechaFinal);
        int AccientesTrabajoInvestigados(string fechaInicial, string fechaFinal);
        int DiasIncapacidadAccientesTrabajo(string fechaInicial, string fechaFinal);
        int NumeroCasosEnfermedadLabora(string fechaInicial, string fechaFinal);
        int NumeroCasosNuevosEnfermedadLabora(string fechaInicial, string fechaFinal);
        int DíasAusenciaIncapacidadLaboralComun(string fechaInicial, string fechaFinal);
        int EnfermedadesIncidentesAusentismos(string fechaInicial, string fechaFinal);
    }
}