using System;
using System.Collections.Generic;
using System.Linq;
using WSafe.Domain.Data.Entities.Incidentes;
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
        public int AccidentesTrabajo(int[] periodo)
        {
            return (from at in _empresaContext.Incidentes
                    where (periodo.Contains(at.FechaIncidente.Month)) && at.CategoriaIncidente == CategoriasIncidente.Accidente
                    select at).Count();
        }
        public int AccidentesTrabajoInvestigados(DateTime fechaInicial, DateTime fechaFinal)
        {
            return _empresaContext.Incidentes
                .Where(i => i.FechaIncidente >= fechaInicial && i.FechaIncidente <= fechaFinal && i.CategoriaIncidente == Data.Entities.Incidentes.CategoriasIncidente.Accidente && i.RequiereInvestigacion == true)
                .Count();
        }

        public int AccidentesTrabajoMortales(int[] periodo)
        {
            return (from at in _empresaContext.Incidentes
                    where(periodo.Contains(at.FechaIncidente.Month)) && at.CategoriaIncidente == CategoriasIncidente.Accidente
                    && at.ConsecuenciasLesion == ConsecuenciasLesion.fatalidadMultiple select at).Count();
        }
        public decimal AusentismoCausaMedica(DateTime fechaInicial, DateTime fechaFinal)
        {
            return Convert.ToDecimal(IncidentesInvestigados(fechaInicial, fechaFinal))
                / Convert.ToDecimal(TotalIncidentes(fechaInicial, fechaFinal)) * 100;
        }

        public int DiasIncapacidadAccidentesTrabajo(int[] periodo)
        {
            return (from at in _empresaContext.Incidentes
                    where (periodo.Contains(at.FechaIncidente.Month)) && at.IncapacidadMedica == true select at).Sum(i => i.DiasIncapacidad);
        }
        public int DiasCargadosAccidentesTrabajo(int[] periodo)
        {
            return 6000;
        }

        public int DíasAusenciaIncapacidadLaboral(DateTime fechaInicial, DateTime fechaFinal)
        {
            return _empresaContext.Incidentes
                .Where(i => i.FechaIncidente >= fechaInicial && i.FechaIncidente <= fechaFinal && i.IncapacidadMedica == true)
                .Sum(i => i.DiasIncapacidad);
        }

        public int EnfermedadesIncidentesAusentismos(DateTime fechaInicial, DateTime fechaFinal)
        {
            throw new System.NotImplementedException();
        }

        public decimal FrecuenciaAccidentalidad(int[] periodo, int year)
        {
            return Convert.ToDecimal(AccidentesTrabajo(periodo))
                / Convert.ToDecimal(NumeroTrabajadoresMes(periodo, year)) * 100;
        }
        public decimal IncidenciaEnfermedad(int[] periodo)
        {

            return Convert.ToDecimal(NumeroCasosNuevosEnfermedadLaboral(periodo))
                / Convert.ToDecimal(PromedioTrabajadores(periodo)) * 100000;
        }

        public int NumeroCasosEnfermedadLaboral(int[] periodo)
        {
            throw new System.NotImplementedException();
        }

        public int NumeroCasosNuevosEnfermedadLaboral(int[] periodo) 
        {
            throw new System.NotImplementedException();
        }
        public decimal PrevalenciaEnfermedad(int[] periodo)
        {
            return Convert.ToDecimal(NumeroCasosEnfermedadLaboral(periodo))
                / Convert.ToDecimal(PromedioTrabajadores(periodo)) * 100000;
        }
        public decimal PromedioTrabajadores(int[] periodo) 
        {
            return (from t in _empresaContext.Trabajadores where (periodo.Contains(t.FechaNomina.Month)) select t).Count();
        }

        public decimal ProporcionAccidentesMortales(int[] periodo)
        {
            return Convert.ToDecimal(AccidentesTrabajoMortales(periodo))
                / Convert.ToDecimal(AccidentesTrabajo(periodo)) * 100;
        }
        public decimal SeveridadAccidentalidad(int[] periodo, int year)
        {
            return Convert.ToDecimal(DiasIncapacidadAccidentesTrabajo(periodo))
                + Convert.ToDecimal(DiasCargadosAccidentesTrabajo(periodo))
                 / Convert.ToDecimal(NumeroTrabajadoresMes(periodo, year)) * 100; ;
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

        public int NumeroTrabajadoresMes(int[] periodo, int year)
        {
            return (from t in _empresaContext.Trabajadores where(periodo.Contains(t.FechaNomina.Month) && t.FechaNomina.Year == year) select t).Count();
        }

        public int NumeroDiasTrabajadosMes()
        {
            return _empresaContext.Trabajadores.Sum(dp => dp.DiasPago);
        }
        public int NumeroCasosEnfermedadLaboral(DateTime fechaInicial, DateTime fechaFinal)
        {
            throw new NotImplementedException();
        }

        public int NumeroCasosNuevosEnfermedadLaboral(DateTime fechaInicial, DateTime fechaFinal)
        {
            throw new NotImplementedException();
        }

        public int NumeroTrabajadoresMes(DateTime fechaInicial, DateTime fechaFinal)
        {
            throw new NotImplementedException();
        }

        public decimal PromedioTrabajadores(DateTime fechaInicial, DateTime fechaFinal)
        {
            throw new NotImplementedException();
        }

        public decimal FrecuenciaAccidentalidad(DateTime fechaInicial, DateTime fechaFinal)
        {
            throw new NotImplementedException();
        }

        public decimal SeveridadAccidentalidad(DateTime fechaInicial, DateTime fechaFinal)
        {
            throw new NotImplementedException();
        }

        public decimal ProporcionAccidentesMortales(DateTime fechaInicial, DateTime fechaFinal)
        {
            throw new NotImplementedException();
        }

        public decimal PrevalenciaEnfermedad(DateTime fechaInicial, DateTime fechaFinal)
        {
            throw new NotImplementedException();
        }

        public decimal IncidenciaEnfermedad(DateTime fechaInicial, DateTime fechaFinal)
        {
            throw new NotImplementedException();
        }

        public int DiasIncapacidadAccidentesTrabajo(DateTime fechaInicial, DateTime fechaFinal)
        {
            throw new NotImplementedException();
        }

        public int AccidentesTrabajo(DateTime fechaInicial, DateTime fechaFinal)
        {
            throw new NotImplementedException();
        }

        public int DiasCargadosAccidentesTrabajo(DateTime fechaInicial, DateTime fechaFinal)
        {
            throw new NotImplementedException();
        }

        public int AccidentesTrabajoMortales(DateTime fechaInicial, DateTime fechaFinal)
        {
            throw new NotImplementedException();
        }
    }
}