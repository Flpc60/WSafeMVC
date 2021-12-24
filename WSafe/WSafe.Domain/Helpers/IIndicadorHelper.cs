using System;
using System.Collections.Generic;
using WSafe.Domain.Data.Entities;

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
        int DíasAusenciaIncapacidadLaboral(DateTime fechaInicial, DateTime fechaFinal);
        int EnfermedadesIncidentesAusentismos(DateTime fechaInicial, DateTime fechaFinal);
        int NumeroTrabajadoresMes();
        int PromedioTrabajadores();
        int NumeroDiasTrabajadosMes();
        IEnumerable<Indicador> FrecuenciaAccidentalidad(DateTime fechaInicial, DateTime fechaFinal);
        IEnumerable<Indicador> SeveridadAccidentalidad(DateTime fechaInicial, DateTime fechaFinal);
        IEnumerable<Indicador> ProporcionAccidentesMortales(DateTime fechaInicial, DateTime fechaFinal);
        IEnumerable<Indicador> PrevalenciaEnfermedad(DateTime fechaInicial, DateTime fechaFinal);
        IEnumerable<Indicador> IncidenciaEnfermedad(DateTime fechaInicial, DateTime fechaFinal);
        IEnumerable<Indicador> AusentismoCausaMedica(DateTime fechaInicial, DateTime fechaFinal);

    }
}