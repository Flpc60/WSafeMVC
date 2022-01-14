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
        public int AccidentesTrabajo(DateTime fechaInicial, DateTime fechaFinal)
        {
            return _empresaContext.Incidentes
                .Where(i => i.FechaIncidente >= fechaInicial && i.FechaIncidente <= fechaFinal && i.CategoriaIncidente == Data.Entities.Incidentes.CategoriasIncidente.Accidente)
                .Count();
        }
        public int AccidentesTrabajoInvestigados(DateTime fechaInicial, DateTime fechaFinal)
        {
            return _empresaContext.Incidentes
                .Where(i => i.FechaIncidente >= fechaInicial && i.FechaIncidente <= fechaFinal && i.CategoriaIncidente == Data.Entities.Incidentes.CategoriasIncidente.Accidente && i.RequiereInvestigacion == true)
                .Count();
        }

        public int AccidentesTrabajoMortales(DateTime fechaInicial, DateTime fechaFinal)
        {
            return _empresaContext.Incidentes
                .Where(i => i.FechaIncidente >= fechaInicial && i.FechaIncidente <= fechaFinal && i.CategoriaIncidente == Data.Entities.Incidentes.CategoriasIncidente.Accidente && i.ConsecuenciasLesion == Data.Entities.Incidentes.ConsecuenciasLesion.Mortal)
                .Count();
        }

        public int AusentismoCausaMedica(DateTime fechaInicial, DateTime fechaFinal)
        {
            throw new NotImplementedException();
        }

        public int DiasIncapacidadAccidentesTrabajo(DateTime fechaInicial, DateTime fechaFinal)
        {
            return _empresaContext.Incidentes
                .Where(i => i.FechaIncidente >= fechaInicial && i.FechaIncidente <= fechaFinal && i.IncapacidadMedica == true)
                .Sum(i => i.DiasIncapacidad);
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
                / Convert.ToDecimal(NumeroTrabajadoresMes(fechaInicial, fechaFinal)) * 100;
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
        public decimal PrevalenciaEnfermedad(DateTime fechaInicial, DateTime fechaFinal)
        {
            return Convert.ToDecimal(NumeroCasosEnfermedadLaboral(fechaInicial, fechaFinal))
                / Convert.ToDecimal(PromedioTrabajadores(fechaInicial, fechaFinal)) * 100000;
        }
        public decimal PromedioTrabajadores(DateTime fechaInicial, DateTime fechaFinal)
        {
            return _empresaContext.Trabajadores
                .Where(i => i.FechaNomina >= fechaInicial && i.FechaNomina <= fechaFinal)
                .Count();
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
                 / Convert.ToDecimal(NumeroTrabajadoresMes(fechaInicial, fechaFinal)) * 100; ;
        }
        public int NumeroACPAccidentes(DateTime fechaInicial, DateTime fechaFinal)
        {
            return _empresaContext.Acciones
                .Where(i => i.FechaSolicitud >= fechaInicial && i.FechaSolicitud <= fechaFinal && i.FuenteAccion == Data.Entities.FuentesAccion.Incidente)
                .Count();
        }
        public int NumeroACP(DateTime fechaInicial, DateTime fechaFinal)
        {
            return _empresaContext.Acciones
                .Where(i => i.FechaSolicitud >= fechaInicial && i.FechaSolicitud <= fechaFinal)
                .Count();
        }
        public decimal ProporcionACPAccidentes(DateTime fechaInicial, DateTime fechaFinal)
        {
            return Convert.ToDecimal(NumeroACPAccidentes(fechaInicial, fechaFinal))
                / Convert.ToDecimal(NumeroACP(fechaInicial, fechaFinal)) * 100;
        }
        public decimal ProporcionAccidentesInvestigados(DateTime fechaInicial, DateTime fechaFinal)
        {
            return Convert.ToDecimal(AccidentesTrabajoInvestigados(fechaInicial, fechaFinal))
                / Convert.ToDecimal(AccidentesTrabajo(fechaInicial, fechaFinal)) * 100;
        }

        public int IncidentesInvestigados(DateTime fechaInicial, DateTime fechaFinal)
        {
            return _empresaContext.Incidentes
                .Where(i => i.FechaIncidente >= fechaInicial && i.FechaIncidente <= fechaFinal && i.CategoriaIncidente == Data.Entities.Incidentes.CategoriasIncidente.Incidente && i.RequiereInvestigacion == true)
                .Count();
        }

        public int TotalIncidentes(DateTime fechaInicial, DateTime fechaFinal)
        {
            return _empresaContext.Incidentes
                .Where(i => i.FechaIncidente >= fechaInicial && i.FechaIncidente <= fechaFinal && i.CategoriaIncidente == Data.Entities.Incidentes.CategoriasIncidente.Incidente)
                .Count();
        }

        public decimal ProporcionIncidentesInvestigados(DateTime fechaInicial, DateTime fechaFinal)
        {
            return Convert.ToDecimal(IncidentesInvestigados(fechaInicial, fechaFinal))
                / Convert.ToDecimal(TotalIncidentes(fechaInicial, fechaFinal)) * 100;
        }

        public int NumeroTrabajadoresMes(DateTime fechaInicial, DateTime fechaFinal)
        {
            return _empresaContext.Trabajadores
                .Where(t => t.FechaNomina >= fechaInicial && t.FechaNomina <= fechaFinal)
                .Count();
        }

        public decimal NumeroDiasTrabajadosMes(DateTime fechaInicial, DateTime fechaFinal)
        {
            return Convert.ToDecimal(_empresaContext.Trabajadores.Average(dp => dp.DiasPago));
        }
    }
}