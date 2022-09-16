using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Helpers;
using WSafe.Domain.Data.Entities.Incidentes;
using WSafe.Web.Models;

namespace WSafe.Domain.Helpers.Implements
{
    public class ChartHelper : IChartHelper
    {
        // Construir gráficas
        private readonly EmpresaContext _empresaContext;
        private readonly IndicadorHelper _indicadorHelper;
        public ChartHelper(EmpresaContext empresaContext, IndicadorHelper indicadorHelper)
        {
            _empresaContext = empresaContext;
            _indicadorHelper = indicadorHelper;
        }
        public void DrawImagen(string archivo, string tipo, string nombre, IEnumerable<IndicadorDetallesViewModel> lista)
        {
            var chartImage = new Chart(width: 500, height: 300, theme: ChartTheme.Green);
            chartImage.AddTitle(nombre);
            chartImage.AddSeries("Default", chartType: tipo,
                        xValue: lista, xField: "MesAnn",
                          yValues: lista, yFields: "Resultado");
            chartImage.Save(path: archivo);
        }
        public IEnumerable<IndicadorDetallesViewModel> GetFrecuenciaAccidentes(int[] periodo, int year)
        {
            try
            {
                var denominador = _indicadorHelper.NumeroTrabajadoresMes(periodo, year);

                var result = from at in _empresaContext.Incidentes
                             where (periodo.Contains(at.FechaIncidente.Month) && at.FechaIncidente.Year == year && at.CategoriasIncidente == CategoriasIncidente.Accidente)
                             group at by new { at.FechaIncidente.Year, at.FechaIncidente.Month } into datosAgrupados
                             orderby datosAgrupados.Key
                             select new { Clave = datosAgrupados.Key, Datos = datosAgrupados };

                var viewModel = new List<IndicadorDetallesViewModel>();
                foreach (var grupo in result)
                {
                    viewModel.Add(new IndicadorDetallesViewModel
                    {
                        MesAnn = (grupo.Clave.Month + "-" + grupo.Clave.Year).ToString(),
                        Numerador = grupo.Datos.Count(),
                        Denominador = denominador,
                        Resultado = Convert.ToDecimal((double)grupo.Datos.Count() / (double)denominador * 100),
                    });
                }
                return viewModel;
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
        }
        public IEnumerable<IndicadorDetallesViewModel> GetAccidentesTrabajoInvestigados(DateTime fechaInicial, DateTime fechaFinal)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IndicadorDetallesViewModel> GetAccidentesTrabajoMortales(int[] periodo, int year)
        {
            try
            {
                var result = from at in _empresaContext.Incidentes
                             where (periodo.Contains(at.FechaIncidente.Month) && at.FechaIncidente.Year == year && at.CategoriasIncidente == CategoriasIncidente.Accidente
                             && at.ConsecuenciasLesion == ConsecuenciasLesion.fatalidadMultiple)
                             group at by new { at.FechaIncidente.Year, at.FechaIncidente.Month } into datosAgrupados
                             orderby datosAgrupados.Key
                             select new { Clave = datosAgrupados.Key, Datos = datosAgrupados };

                var viewModel = new List<IndicadorDetallesViewModel>();
                foreach (var grupo in result)
                {
                    viewModel.Add(new IndicadorDetallesViewModel
                    {
                        MesAnn = (grupo.Clave.Month + "-" + grupo.Clave.Year).ToString(),
                        Numerador = _indicadorHelper.AccidentesTrabajoMortales(year),
                        Denominador = grupo.Datos.Count()
                    });
                }

                foreach (var item in viewModel)
                {
                    item.Resultado = Convert.ToDecimal((double)item.Numerador /
                        (double)item.Denominador) * 100;
                }

                return viewModel;
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
        }

        public IEnumerable<IndicadorDetallesViewModel> GetAllIncidentes(DateTime fechaInicial, DateTime fechaFinal)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IndicadorDetallesViewModel> GetIncidentesInvestigados(DateTime fechaInicial, DateTime fechaFinal)
        {
            throw new NotImplementedException();
        }

        public void PrintIndicadores(string nombre, string ejeX, string ejeY, List<IndicadorDetallesViewModel> lista)
        {
            throw new System.NotImplementedException();
        }

        public void SaveIndicadores(string nombre, string ejeX, string ejeY, List<IndicadorDetallesViewModel> lista)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IndicadorDetallesViewModel> GetSeveridadAccidentalidad(int[] periodo, int year)
        {
            try
            {
                var denominador = _indicadorHelper.NumeroTrabajadoresMes(periodo, year);

                var result = from at in _empresaContext.Incidentes
                             where (periodo.Contains(at.FechaIncidente.Month) && at.FechaIncidente.Year == year && at.CategoriasIncidente == CategoriasIncidente.Accidente)
                             group at by new { at.FechaIncidente.Year, at.FechaIncidente.Month } into datosAgrupados
                             orderby datosAgrupados.Key
                             select new
                             {
                                 Clave = datosAgrupados.Key,
                                 Datos = datosAgrupados,
                                 Dias = datosAgrupados.Sum(di => di.DiasIncapacidad)
                             };

                var viewModel = new List<IndicadorDetallesViewModel>();
                foreach (var grupo in result)
                {
                    viewModel.Add(new IndicadorDetallesViewModel
                    {
                        MesAnn = (grupo.Clave.Month + "-" + grupo.Clave.Year).ToString(),
                        Numerador = grupo.Dias,
                        Denominador = denominador,
                        Resultado = Convert.ToDecimal((double)grupo.Dias / (double)denominador * 100),
                    });
                }
                return viewModel;
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
        }

        public IEnumerable<IndicadorDetallesViewModel> GetAusentismoCausaMedica(int[] periodo, int year)
        {
            try
            {
                var trabajadores = _indicadorHelper.NumeroTrabajadoresMes(periodo, year);
                var result = from at in _empresaContext.Incidentes
                             where (periodo.Contains(at.FechaIncidente.Month)) && at.IncapacidadMedica == true
                             group at by new { at.FechaIncidente.Year, at.FechaIncidente.Month } into datosAgrupados
                             orderby datosAgrupados.Key
                             select new
                             {
                                 Clave = datosAgrupados.Key,
                                 Datos = datosAgrupados,
                                 Dias = datosAgrupados.Sum(di => di.DiasIncapacidad)
                             };

                var viewModel = new List<IndicadorDetallesViewModel>();
                foreach (var grupo in result)
                {
                    viewModel.Add(new IndicadorDetallesViewModel
                    {
                        MesAnn = (grupo.Clave.Month + "-" + grupo.Clave.Year).ToString(),
                        Numerador = grupo.Dias,
                        Denominador = _indicadorHelper.NumeroDiasTrabajadosMes() * trabajadores,
                        Resultado = Convert.ToDecimal((double)grupo.Dias /
                        (double)_indicadorHelper.NumeroDiasTrabajadosMes() * 100)
                    });
                }
                return viewModel;
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
        }

        public IEnumerable<IndicadorDetallesViewModel> GetFatorRiesgoOcupacional()
        {
            try
            {
                var result = from at in _empresaContext.Riesgos
                             group at by new { at.CategoriaPeligroID } into datosAgrupados
                             orderby datosAgrupados.Count() ascending
                             select new { Clave = datosAgrupados.Key, Datos = datosAgrupados };

                var viewModel = new List<IndicadorDetallesViewModel>();
                foreach (var grupo in result)
                {
                    viewModel.Add(new IndicadorDetallesViewModel
                    {
                        ID = grupo.Datos.Key.CategoriaPeligroID,
                        Numerador = grupo.Datos.Count(),
                        Denominador = 1,
                        Resultado = grupo.Datos.Count()
                    });
                }
                foreach (var item in viewModel)
                {
                    item.MesAnn = _empresaContext.CategoriasPeligros.FirstOrDefault(cp => cp.ID == item.ID).Descripcion;
                }

                return viewModel;
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
        }

        public IEnumerable<IndicadorDetallesViewModel> GetAccidentesTrabajoInvestigados(int[] periodo, int year)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IndicadorDetallesViewModel> GetAllIncidentes(int[] periodo, int year)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IndicadorDetallesViewModel> GetIncidentesInvestigados(int[] periodo, int year)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IndicadorDetallesViewModel> GetAllValueRisks()
        {
            try
            {
                var result = _empresaContext.Riesgos.ToList();
                var muyAlto = 0;
                var alto = 0;
                var medio = 0; 
                var bajo = 0;
                var np = 0;
                var nr = 0;
                foreach (var item in result)
                {
                    np = item.NivelDeficiencia * item.NivelExposicion;
                    nr = np * item.NivelConsecuencia;
                    switch (nr)
                    {
                        case int r when (r >= 600):
                            muyAlto++;
                            break;

                        case int r when (r >= 150 && r < 600):
                            alto++;
                            break;

                        case int r when (r >= 40 && r < 150):
                            medio++;
                            break;

                        case int r when (r < 40):
                            bajo++;
                            break;
                    }
                }

                var viewModel = new List<IndicadorDetallesViewModel>();
                viewModel.Add(new IndicadorDetallesViewModel
                {
                    ID = 1,
                    MesAnn = "Muy Alto",
                    Resultado = muyAlto
                });

                viewModel.Add(new IndicadorDetallesViewModel
                {
                    ID = 2,
                    MesAnn = "Alto",
                    Resultado = alto
                });

                viewModel.Add(new IndicadorDetallesViewModel
                {
                    ID = 3,
                    MesAnn = "Medio",
                    Resultado = medio
                });

                viewModel.Add(new IndicadorDetallesViewModel
                {
                    ID = 4,
                    MesAnn = "Bajo",
                    Resultado = bajo
                });

                return viewModel;
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
        }
    }
}