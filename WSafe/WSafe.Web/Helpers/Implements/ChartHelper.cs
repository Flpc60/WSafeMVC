using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Helpers;
using WSafe.Domain.Data.Entities;
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
            chartImage.AddLegend();
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
                var total = 0;
                foreach (var item in result)
                {
                    np = item.NivelDeficiencia * item.NivelExposicion;
                    nr = np * item.NivelConsecuencia;
                    total++;
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

                decimal pMuyAlto = Math.Round(Convert.ToDecimal((double)muyAlto / (double)total * 100), 2);
                decimal pAlto = Math.Round(Convert.ToDecimal((double)alto / (double)total * 100), 2);
                decimal pMedio = Math.Round(Convert.ToDecimal((double)medio / (double)total * 100), 2);
                decimal pBajo = Math.Round(Convert.ToDecimal((double)bajo / (double)total * 100), 2);
                var viewModel = new List<IndicadorDetallesViewModel>();

                viewModel.Add(new IndicadorDetallesViewModel
                {
                    ID = 1,
                    MesAnn = "Muy Alto",
                    Resultado = pMuyAlto
                });

                viewModel.Add(new IndicadorDetallesViewModel
                {
                    ID = 2,
                    MesAnn = "Alto",
                    Resultado = pAlto
                });

                viewModel.Add(new IndicadorDetallesViewModel
                {
                    ID = 3,
                    MesAnn = "Medio",
                    Resultado = pMedio
                });

                viewModel.Add(new IndicadorDetallesViewModel
                {
                    ID = 4,
                    MesAnn = "Bajo",
                    Resultado = pBajo
                });

                return viewModel;
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
        }

        public IEnumerable<IndicadorDetallesViewModel> GetAllValueActivitys()
        {
            try
            {
                var result = _empresaContext.Riesgos.ToList();
                var rutinaria = 0;
                var noRutinaria = 0;
                var total = 0;
                foreach (var item in result)
                {
                    total++;
                    if (item.Rutinaria == true)
                    {
                        rutinaria++;
                    }
                    else
                    {
                        noRutinaria++;
                    }
                }

                decimal pRutinaria = Math.Round(Convert.ToDecimal((double)rutinaria / (double)total * 100), 2);
                decimal pnoRutinaria = Math.Round(Convert.ToDecimal((double)noRutinaria / (double)total * 100), 2);

                var viewModel = new List<IndicadorDetallesViewModel>();
                viewModel.Add(new IndicadorDetallesViewModel
                {
                    ID = 1,
                    MesAnn = "Rutinaria",
                    Resultado = pRutinaria
                });

                viewModel.Add(new IndicadorDetallesViewModel
                {
                    ID = 2,
                    MesAnn = "No Rutinaria",
                    Resultado = pnoRutinaria
                });

                return viewModel;
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
        }

        public IEnumerable<IndicadorDetallesViewModel> GetAllValueDangers()
        {
            try
            {
                var result = _empresaContext.Riesgos.ToList();
                var rutinaria = 0;
                var noRutinaria = 0;
                var total = 0;
                foreach (var item in result)
                {
                    total++;
                    if (item.Rutinaria == true)
                    {
                        rutinaria++;
                    }
                    else
                    {
                        noRutinaria++;
                    }
                }

                decimal pRutinaria = Math.Round(Convert.ToDecimal((double)rutinaria / (double)total * 100), 2);
                decimal pnoRutinaria = Math.Round(Convert.ToDecimal((double)noRutinaria / (double)total * 100), 2);

                var viewModel = new List<IndicadorDetallesViewModel>();
                viewModel.Add(new IndicadorDetallesViewModel
                {
                    ID = 1,
                    MesAnn = "Rutinaria",
                    Resultado = pRutinaria
                });

                viewModel.Add(new IndicadorDetallesViewModel
                {
                    ID = 2,
                    MesAnn = "No Rutinaria",
                    Resultado = pnoRutinaria
                });

                return viewModel;
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
        }

        public IEnumerable<IndicadorDetallesViewModel> GetAllValueEfects()
        {
            try
            {
                var result = _empresaContext.Riesgos.ToList();
                var extremo = 0;
                var leve = 0;
                var moderado = 0;
                var propiedad = 0;
                var procesos = 0;
                var economicas = 0;
                var total = 0;
                foreach (var item in result)
                {
                    total++;
                    switch (item.EfectosPosibles)
                    {
                        case EfectosPosibles.Daño_Extremo:
                            extremo++;
                            break;

                        case EfectosPosibles.Daño_Leve:
                            leve++;
                            break;

                        case EfectosPosibles.Daño_Moderado:
                            moderado++;
                            break;

                        case EfectosPosibles.Daño_Propiedad:
                            propiedad++;
                            break;

                        case EfectosPosibles.Fallas_procesos:
                            procesos++;
                            break;

                        case EfectosPosibles.Pérdidas_económicas:
                            economicas++;
                            break;
                    }
                }

                decimal pExtremo = Math.Round(Convert.ToDecimal((double)extremo / (double)total * 100), 2);
                decimal pLeve = Math.Round(Convert.ToDecimal((double)leve / (double)total * 100), 2);
                decimal pModerado = Math.Round(Convert.ToDecimal((double)moderado / (double)total * 100), 2);
                decimal pPropiedad = Math.Round(Convert.ToDecimal((double)propiedad / (double)total * 100), 2);
                decimal pProcesos = Math.Round(Convert.ToDecimal((double)procesos / (double)total * 100), 2);
                decimal pEconomicas = Math.Round(Convert.ToDecimal((double)economicas / (double)total * 100), 2);
                var viewModel = new List<IndicadorDetallesViewModel>();

                viewModel.Add(new IndicadorDetallesViewModel
                {
                    ID = 1,
                    MesAnn = "Daño Extremo",
                    Resultado = pExtremo
                });

                viewModel.Add(new IndicadorDetallesViewModel
                {
                    ID = 2,
                    MesAnn = "Daño Leve",
                    Resultado = pLeve
                });

                viewModel.Add(new IndicadorDetallesViewModel
                {
                    ID = 3,
                    MesAnn = "Daño Moderado",
                    Resultado = pModerado
                });

                viewModel.Add(new IndicadorDetallesViewModel
                {
                    ID = 4,
                    MesAnn = "Daño a propiedad",
                    Resultado = pPropiedad
                });

                viewModel.Add(new IndicadorDetallesViewModel
                {
                    ID = 5,
                    MesAnn = "Fallas en procesos",
                    Resultado = pProcesos
                });

                viewModel.Add(new IndicadorDetallesViewModel
                {
                    ID = 6,
                    MesAnn = "Perdidas económicas",
                    Resultado = pEconomicas
                });

                return viewModel;
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
        }
        public IEnumerable<IndicadorDetallesViewModel> GetAllNoConformance()
        {
            try
            {
                var result = from a in _empresaContext.Acciones
                             group a by new { a.FechaSolicitud.Year, a.FechaSolicitud.Month } into datosAgrupados
                             orderby datosAgrupados.Key
                             select new { Clave = datosAgrupados.Key, Datos = datosAgrupados };

                var viewModel = new List<IndicadorDetallesViewModel>();
                foreach (var grupo in result)
                {
                    viewModel.Add(new IndicadorDetallesViewModel
                    {
                        MesAnn = (grupo.Clave.Month + "-" + grupo.Clave.Year).ToString(),
                        Numerador = grupo.Datos.Count(),
                        Resultado = grupo.Datos.Count()
                    });
                }
                return viewModel;
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
        }

        public IEnumerable<IndicadorDetallesViewModel> GetAllValueActions()
        {
            try
            {
                var result = _empresaContext.Acciones.ToList();
                var sinIniciar = 0;
                var proceso = 0;
                var finalizada = 0;
                var total = 0;
                foreach (var item in result)
                {
                    total++;
                    switch (item.ActionCategory)
                    {
                        case ActionCategories.Sin_Iniciar:
                            sinIniciar++;
                            break;

                        case ActionCategories.En_Proceso:
                            proceso++;
                            break;

                        case ActionCategories.Finalizada:
                            finalizada++;
                            break;
                    }
                }

                decimal pIniciar = Math.Round(Convert.ToDecimal((double)sinIniciar / (double)total * 100), 2);
                decimal pProceso = Math.Round(Convert.ToDecimal((double)proceso / (double)total * 100), 2);
                decimal pFinalizada = Math.Round(Convert.ToDecimal((double)finalizada / (double)total * 100), 2);
                var viewModel = new List<IndicadorDetallesViewModel>();

                viewModel.Add(new IndicadorDetallesViewModel
                {
                    ID = 1,
                    MesAnn = "Sín Iniciar",
                    Resultado = pIniciar
                });

                viewModel.Add(new IndicadorDetallesViewModel
                {
                    ID = 2,
                    MesAnn = "En Proceso",
                    Resultado = pProceso
                });

                viewModel.Add(new IndicadorDetallesViewModel
                {
                    ID = 3,
                    MesAnn = "Finalizada",
                    Resultado = pFinalizada
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