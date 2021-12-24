using System;
using System.Linq;
using WSafe.Web.Models;

namespace WSafe.Domain.Helpers.Implements
{
    public class IndicadorHelper : IIndicadorHelper
    {
        private readonly EmpresaContext _empresaContext;
        public IndicadorHelper(EmpresaContext empresaContext)
        {
            _empresaContext = empresaContext;
        }
        public int AccientesTrabajo(DateTime fechaInicial, DateTime fechaFinal)
        {
            return  _empresaContext.Incidentes
                .Where(i => i.FechaIncidente >= fechaInicial && i.FechaIncidente <= fechaFinal)
                .Count();
        }
        public int AccientesTrabajoInvestigados(DateTime fechaInicial, DateTime fechaFinal)
        {
            return _empresaContext.Incidentes
                .Where(i => i.FechaIncidente >= fechaInicial && i.FechaIncidente <= fechaFinal && i.RequiereInvestigacion == true)
                .Count();
        }

        public int AccientesTrabajoMortales(DateTime fechaInicial, DateTime fechaFinal)
        {
            throw new System.NotImplementedException();
        }

        public int DiasIncapacidadAccientesTrabajo(DateTime fechaInicial, DateTime fechaFinal)
        {
            throw new System.NotImplementedException();
        }

        public int DíasAusenciaIncapacidadLaboralComun(DateTime fechaInicial, DateTime fechaFinal)
        {
            throw new System.NotImplementedException();
        }

        public int EnfermedadesIncidentesAusentismos(DateTime fechaInicial, DateTime fechaFinal)
        {
            throw new System.NotImplementedException();
        }

        public int NumeroCasosEnfermedadLabora(DateTime fechaInicial, DateTime fechaFinal)
        {
            throw new System.NotImplementedException();
        }

        public int NumeroCasosNuevosEnfermedadLabora(DateTime fechaInicial, DateTime fechaFinal)
        {
            throw new System.NotImplementedException();
        }
    }
}