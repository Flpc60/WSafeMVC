﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
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
        public int AccidentesTrabajo(int year)
        {
            return (from at in _empresaContext.Incidentes
                    where at.FechaIncidente.Year == year && at.CategoriasIncidente == CategoriasIncidente.Accidente
                    select at).Count();
        }
        public int AccidentesTrabajoInvestigados(int year)
        {
            return _empresaContext.Incidentes
                .Where(i => i.FechaIncidente.Year == year && i.CategoriasIncidente == Data.Entities.Incidentes.CategoriasIncidente.Accidente && i.RequiereInvestigacion == true)
                .Count();
        }

        public int AccidentesTrabajoMortales(int year)
        {
            return (from at in _empresaContext.Incidentes
                    where at.FechaIncidente.Year == year && at.CategoriasIncidente == CategoriasIncidente.Accidente
                    && at.ConsecuenciasLesion == ConsecuenciasLesion.fatalidadMultiple select at).Count();
        }
        public decimal AusentismoCausaMedica(int year)
        {
            return Convert.ToDecimal(IncidentesInvestigados(year))
                / Convert.ToDecimal(GetIncidentes(year)) * 100;
        }

        public int DiasIncapacidadAccidentesTrabajo(int year)
        {
            return (from at in _empresaContext.Incidentes
                    where at.FechaIncidente.Year == year && at.IncapacidadMedica == true select at).Sum(i => i.DiasIncapacidad);
        }
        public int DiasCargadosAccidentesTrabajo(int year)
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
            return Convert.ToDecimal(AccidentesTrabajo(year))
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

        public decimal ProporcionAccidentesMortales(int year)
        {
            return Convert.ToDecimal(AccidentesTrabajoMortales(year))
                / Convert.ToDecimal(AccidentesTrabajo(year)) * 100;
        }
        public decimal SeveridadAccidentalidad(int[] periodo, int year)
        {
            return Convert.ToDecimal(DiasIncapacidadAccidentesTrabajo(year))
                + Convert.ToDecimal(DiasCargadosAccidentesTrabajo(year))
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
        public decimal ProporcionAccidentesInvestigados(int year)
        {
            return Convert.ToDecimal(AccidentesTrabajoInvestigados(year))
                / Convert.ToDecimal(AccidentesTrabajo(year)) * 100;
        }

        public int IncidentesInvestigados(int year)
        {
            return _empresaContext.Incidentes
                .Where(i => i.FechaIncidente.Year == year && i.CategoriasIncidente == Data.Entities.Incidentes.CategoriasIncidente.Incidente && i.RequiereInvestigacion == true)
                .Count();
        }

        public int GetIncidentes(int year)
        {
            return (from at in _empresaContext.Incidentes
                    where at.FechaIncidente.Year == year && at.CategoriasIncidente == CategoriasIncidente.Incidente select at).Count();
        }

        public decimal ProporcionIncidentesInvestigados(int year)
        {
            return Convert.ToDecimal(IncidentesInvestigados(year))
                / Convert.ToDecimal(GetIncidentes(year)) * 100;
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
        public decimal ProporcionIncidentesInvestigados(DateTime fechaInicial, DateTime fechaFinal)
        {
            throw new NotImplementedException();
        }

        public int IncidentesInvestigados(DateTime fechaInicial, DateTime fechaFinal)
        {
            throw new NotImplementedException();
        }
        public int DíasAusenciaIncapacidadLaboral(int year)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<DashboardVM> GetIndicators()
        {
            try
            {
                var fecha = DateTime.Now;
                var year = fecha.Month;
                int[] periodo = {1,2,3,4,5,6,7,8,9,10,11,12 };
                var denominador = NumeroTrabajadoresMes(periodo, year);

                var result = from at in _empresaContext.Incidentes
                             where (periodo.Contains(at.FechaIncidente.Month) && at.FechaIncidente.Year == year && at.CategoriasIncidente == CategoriasIncidente.Accidente)
                             group at by new { at.FechaIncidente.Year, at.FechaIncidente.Month } into datosAgrupados
                             orderby datosAgrupados.Key
                             select new { Clave = datosAgrupados.Key, Datos = datosAgrupados };

                var viewModel = new List<DashboardVM>();
                foreach (var grupo in result)
                {
                    viewModel.Add(new DashboardVM
                    {
                        //MesAnn = (grupo.Clave.Month + "-" + grupo.Clave.Year).ToString(),
                        //Numerador = grupo.Datos.Count(),
                        //Denominador = denominador,
                        //Resultado = Convert.ToDecimal((double)grupo.Datos.Count() / (double)denominador * 100),
                    });
                }
                return viewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}