using System;
using System.Collections.Generic;
using WSafe.Domain.Data.Entities;

namespace WSafe.Domain.Helpers.Implements
{
    interface IIndicadorHelper
    {
        int AccidentesTrabajo(DateTime fechaInicial, DateTime fechaFinal);
        int AccidentesTrabajoMortales(DateTime fechaInicial, DateTime fechaFinal);
        int AccidentesTrabajoInvestigados(DateTime fechaInicial, DateTime fechaFinal);
        int DiasIncapacidadAccidentesTrabajo(DateTime fechaInicial, DateTime fechaFinal);
        int DiasCargadosAccidentesTrabajo(DateTime fechaInicial, DateTime fechaFinal);
        int NumeroCasosEnfermedadLaboral(DateTime fechaInicial, DateTime fechaFinal);
        int NumeroCasosNuevosEnfermedadLaboral(DateTime fechaInicial, DateTime fechaFinal);
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