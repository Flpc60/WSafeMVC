using System;
using System.Collections.Generic;
using System.Linq;
using WSafe.Domain.Data.Entities;
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
        public int AccidentesTrabajo(DateTime fechaInicial, DateTime fechaFinal)
        {
            return _empresaContext.Incidentes
                .Where(i => i.FechaIncidente >= fechaInicial && i.FechaIncidente <= fechaFinal)
                .Count();
        }
        public int AccidentesTrabajoInvestigados(DateTime fechaInicial, DateTime fechaFinal)
        {
            return _empresaContext.Incidentes
                .Where(i => i.FechaIncidente >= fechaInicial && i.FechaIncidente <= fechaFinal && i.RequiereInvestigacion == true)
                .Count();
        }

        public int AccidentesTrabajoMortales(DateTime fechaInicial, DateTime fechaFinal)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Indicador> AusentismoCausaMedica(DateTime fechaInicial, DateTime fechaFinal)
        {
            throw new NotImplementedException();
        }

        public int DiasIncapacidadAccidentesTrabajo(DateTime fechaInicial, DateTime fechaFinal)
        {
            throw new System.NotImplementedException();
        }

        public int DiasCargadosAccidentesTrabajo(DateTime fechaInicial, DateTime fechaFinal)
        {
            throw new System.NotImplementedException();
        }

        public int DíasAusenciaIncapacidadLaboral(DateTime fechaInicial, DateTime fechaFinal)
        {
            throw new NotImplementedException();
        }

        public int EnfermedadesIncidentesAusentismos(DateTime fechaInicial, DateTime fechaFinal)
        {
            throw new System.NotImplementedException();
        }

        public decimal FrecuenciaAccidentalidad(DateTime fechaInicial, DateTime fechaFinal)
        {
            return Convert.ToDecimal(AccidentesTrabajo(fechaInicial, fechaFinal))
                / Convert.ToDecimal(NumeroTrabajadoresMes()) * 100;
        }

        public decimal IncidenciaEnfermedad(DateTime fechaInicial, DateTime fechaFinal)
        {

            return Convert.ToDecimal(NumeroCasosNuevosEnfermedadLaboral(fechaInicial, fechaFinal))
                / Convert.ToDecimal(PromedioTrabajadores(fechaInicial, fechaFinal)) * 100000;
        }

        public int NumeroCasosEnfermedadLaboral(DateTime fechaInicial, DateTime fechaFinal)
        {
            throw new System.NotImplementedException();
        }

        public int NumeroCasosNuevosEnfermedadLaboral(DateTime fechaInicial, DateTime fechaFinal)
        {
            throw new System.NotImplementedException();
        }

        public int NumeroDiasTrabajadosMes()
        {
            throw new NotImplementedException();
        }

        public int NumeroTrabajadoresMes()
        {
            throw new NotImplementedException();
        }

        public decimal PrevalenciaEnfermedad(DateTime fechaInicial, DateTime fechaFinal)
        {

            return Convert.ToDecimal(NumeroCasosEnfermedadLaboral(fechaInicial, fechaFinal))
                / Convert.ToDecimal(PromedioTrabajadores(fechaInicial, fechaFinal)) * 100000;
        }
        public int PromedioTrabajadores(DateTime fechaInicial, DateTime fechaFinal)
        {
            throw new NotImplementedException();
        }

        public decimal ProporcionAccidentesMortales(DateTime fechaInicial, DateTime fechaFinal)
        {
            return Convert.ToDecimal(AccidentesTrabajoMortales(fechaInicial, fechaFinal))
                / Convert.ToDecimal(AccidentesTrabajo(fechaInicial, fechaFinal)) * 100;
        }
        public decimal SeveridadAccidentalidad(DateTime fechaInicial, DateTime fechaFinal)
        {
            return Convert.ToDecimal(DiasIncapacidadAccidentesTrabajo(fechaInicial, fechaFinal))
                + Convert.ToDecimal(DiasCargadosAccidentesTrabajo(fechaInicial, fechaFinal))
                 / Convert.ToDecimal(NumeroTrabajadoresMes()) * 100; ;
        }

        public int PromedioTrabajadores()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Indicador> IIndicadorHelper.FrecuenciaAccidentalidad(DateTime fechaInicial, DateTime fechaFinal)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Indicador> IIndicadorHelper.SeveridadAccidentalidad(DateTime fechaInicial, DateTime fechaFinal)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Indicador> IIndicadorHelper.ProporcionAccidentesMortales(DateTime fechaInicial, DateTime fechaFinal)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Indicador> IIndicadorHelper.PrevalenciaEnfermedad(DateTime fechaInicial, DateTime fechaFinal)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Indicador> IIndicadorHelper.IncidenciaEnfermedad(DateTime fechaInicial, DateTime fechaFinal)
        {
            throw new NotImplementedException();
        }
    }
}