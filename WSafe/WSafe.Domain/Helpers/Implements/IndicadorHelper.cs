using System;
using System.Linq;
using WSafe.Domain.Data.Entities;
using WSafe.Domain.Data.Entities.Incidentes;
using WSafe.Web.Models;

namespace WSafe.Domain.Helpers.Implements
{
    public class IndicadorHelper : IIndicadorHelper
    {
        // Construcción indicadores para SG-SST
        private readonly EmpresaContext _empresaContext;
        public IndicadorHelper(EmpresaContext empresaContext)
        {
            _empresaContext = empresaContext;
        }
        public int AccidentesTrabajo(int year, int month)
        {
            return (from at in _empresaContext.Incidentes
                    where at.FechaIncidente.Year == year && at.FechaIncidente.Month == month
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
                    && at.ConsecuenciasLesion == ConsecuenciasLesion.fatalidadMultiple
                    select at).Count();
        }
        public decimal AusentismoCausaMedica(int year)
        {
            return Convert.ToDecimal(IncidentesInvestigados(year))
                / Convert.ToDecimal(GetIncidentes(year)) * 100;
        }

        public int DiasIncapacidadAccidentesTrabajo(int year)
        {
            return (from at in _empresaContext.Incidentes
                    where at.FechaIncidente.Year == year && at.IncapacidadMedica == true
                    select at).Sum(i => i.DiasIncapacidad);
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

        public decimal IncidenciaEnfermedad(int[] periodo)
        {
            //TODO
            return Convert.ToDecimal(NumeroCasosNuevosEnfermedadLaboral(periodo))
                / 100000;
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
            //TODO
            return Convert.ToDecimal(NumeroCasosEnfermedadLaboral(periodo))
                / 100000;
        }
        public decimal PromedioTrabajadores(int year, int _orgID)
        {
            return (from t in _empresaContext.Trabajadores
                    where (t.FechaRetiro.Year != year && t.OrganizationID == _orgID)
                    select t).Count();
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
        public int IncidentesInvestigados(int year)
        {
            return _empresaContext.Incidentes
                .Where(i => i.FechaIncidente.Year == year && i.CategoriasIncidente == Data.Entities.Incidentes.CategoriasIncidente.Incidente && i.RequiereInvestigacion == true)
                .Count();
        }

        public int GetIncidentes(int year)
        {
            return (from at in _empresaContext.Incidentes
                    where at.FechaIncidente.Year == year && at.CategoriasIncidente == CategoriasIncidente.Incidente
                    select at).Count();
        }

        public decimal ProporcionIncidentesInvestigados(int year)
        {
            return Convert.ToDecimal(IncidentesInvestigados(year))
                / Convert.ToDecimal(GetIncidentes(year)) * 100;
        }

        public int NumeroTrabajadoresMes(int[] periodo, int year)
        {
            return (from t in _empresaContext.Trabajadores where (t.FechaIngreso.Year == year && periodo.Contains(t.FechaIngreso.Month)) select t).Count();
        }

        public int NumeroDiasTrabajadosMes(int month, int year)
        {
            var diasMes = DateTime.DaysInMonth(year, month);
            var diasPago = 0;
            var result = from t in _empresaContext.Trabajadores where t.Activo == true && t.FechaIngreso.Year == year && t.FechaIngreso.Month == month select t;
            if (result.Count() > 0)
            {
                diasPago = result.Sum(dp => diasMes - dp.FechaIngreso.Day);
            }
            return diasPago;
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
        public DashboardVM GetIndicators(int year, int month, int _orgID)
        {
            try
            {
                var activitiesQuery = from sap in _empresaContext.SigueAnnualPlans
                                      where (sap.OrganizationID == _orgID && sap.DateSigue.Year == year)
                                      select sap;
                decimal activities = activitiesQuery.Where(spa => spa.StateCronogram == StatesCronogram.Programada).Sum(spa => (short?)(spa.Programed)) ?? 0;
                decimal executed = activitiesQuery.Where(spa => spa.StateCronogram == StatesCronogram.Ejecutada).Sum(spa => (short?)(spa.Executed)) ?? 0;
                decimal proporcion = 0;
                if (activities > 0)
                {
                    proporcion = Convert.ToDecimal(executed) / activities * 100;
                }

                decimal proporcionSM = 0;
                var evaluation = _empresaContext.Evaluations
                    .Where(r => r.OrganizationID == _orgID)
                    .OrderByDescending(o => o.FechaEvaluation.Year + o.FechaEvaluation.Month)
                    .FirstOrDefault(e => e.FechaEvaluation.Year == year);

                if (evaluation != null)
                {
                    proporcionSM = evaluation.StandarsResult;
                }
                decimal denominador = PromedioTrabajadores(year, _orgID);

                var result = (from at in _empresaContext.Incidentes
                              where (at.FechaIncidente.Year == year && at.OrganizationID == _orgID)
                              group at by new { at.FechaIncidente.Year } into data
                              select new
                              {
                                  AccidentsProportion = ((data.Where(x => x.CategoriasIncidente == CategoriasIncidente.Accidente).Count() / month) / denominador * 100),
                                  Accidents = data.Where(x => x.CategoriasIncidente == CategoriasIncidente.Accidente).Count(),
                                  Incidents = data.Where(x => x.CategoriasIncidente == CategoriasIncidente.Incidente).Count(),
                                  Mortality = data.Where(x => x.CategoriasIncidente == CategoriasIncidente.Accidente && x.ConsecuenciasLesion == ConsecuenciasLesion.fatalidadMultiple).Count(),
                                  MortalityProportion = 100,
                                  Ausentisms = data.Where(y => y.IncapacidadMedica == true).Sum(i => i.DiasIncapacidad),
                                  MinimalStandardsProportion = proporcionSM,
                                  ActivitiesPlanProportion = proporcion
                              }).ToList();

                var model = new DashboardVM();
                foreach (var item in result)
                {
                    model.AccidentsProportion = Math.Round(Convert.ToDecimal(item.AccidentsProportion), 2);
                    model.Accidents = item.Accidents;
                    model.Incidents = item.Incidents;
                    model.Ausentisms = item.Ausentisms;
                    model.Mortality = item.Mortality;
                    if (item.Accidents == 0)
                    {
                        model.MortalityProportion = 0;
                    }
                    else
                    {
                        model.MortalityProportion = Math.Round(Convert.ToDecimal(((decimal)item.Mortality / (decimal)item.Accidents) * 100), 2);
                    }
                    model.MinimalStandardsProportion = item.MinimalStandardsProportion;
                    model.ActivitiesPlanProportion = Math.Round(item.ActivitiesPlanProportion, 1);
                }
                model.ActivitiesPlanProportion = Math.Round(proporcion, 1);

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int NumeroHorasTrabajadasMes(int year, int month, int orgID)
        {
            return (from t in _empresaContext.Trabajadores where (t.FechaIngreso.Year == year && t.FechaIngreso.Month == month && t.OrganizationID == orgID) select t).Count() * 8 * 23;
        }
    }
}