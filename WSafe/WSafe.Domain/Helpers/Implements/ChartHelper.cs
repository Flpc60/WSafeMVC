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
        // Construir gráficas e indicadores
        private readonly EmpresaContext _empresaContext;
        private readonly IndicadorHelper _indicadorHelper;
        private readonly GestorHelper _gestorHelper;
        public ChartHelper(EmpresaContext empresaContext, IndicadorHelper indicadorHelper, GestorHelper gestorHelper)
        {
            _empresaContext = empresaContext;
            _indicadorHelper = indicadorHelper;
            _gestorHelper = gestorHelper;
        }
        public void DrawImagen(string archivo, string tipo, string nombre, IEnumerable<IndicadorDetallesViewModel> lista)
        {
            var chartImage = new Chart(width: 600, height: 400, theme: ChartTheme.Green);
            chartImage.AddTitle(nombre);
            chartImage.SetXAxis("CATEGORÍA");
            chartImage.SetYAxis("VALORES");
            chartImage.AddSeries(name: "MÁXIMO", chartType: tipo,
                xValue: lista, xField: "MesAnn",
                yValues: lista, yFields: "Resultado");
            chartImage.AddSeries(name: "OBTENIDO", chartType: tipo,
                xValue: lista, xField: "MesAnn1",
                yValues: lista, yFields: "Resultado1");
            chartImage.Save(path: archivo);
        }
        public IEnumerable<IndicadorDetallesViewModel> GetFrecuenciaAccidentes(int[] periodo, int year, int _orgID)
        {
            try
            {
                var denominador = _indicadorHelper.PromedioTrabajadores(year, _orgID);

                var result = (from at in _empresaContext.Incidentes
                             where (at.FechaIncidente.Year == year && periodo.Contains(at.FechaIncidente.Month) && at.CategoriasIncidente == CategoriasIncidente.Accidente)
                             group at by new { at.FechaIncidente.Year, at.FechaIncidente.Month } into datosAgrupados
                             orderby datosAgrupados.Key
                             select new { 
                                 Clave = datosAgrupados.Key, Datos = datosAgrupados
                             }).ToList();

                var viewModel = new List<IndicadorDetallesViewModel>();
                foreach (var grupo in result)
                {
                    viewModel.Add(new IndicadorDetallesViewModel
                    {
                        MesAnn = (grupo.Clave.Month + "-" + grupo.Clave.Year).ToString(),
                        Numerador = grupo.Datos.Count(),
                        Denominador = (int)denominador,
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

        //Accidentes de trabajo mortales
        public IEnumerable<IndicadorDetallesViewModel> GetAccidentesTrabajoMortales(int[] periodo, int year, int _orgID)
        {
            try
            {
                var result = from at in _empresaContext.Incidentes
                             where (at.FechaIncidente.Year == year && periodo.Contains(at.FechaIncidente.Month) && at.CategoriasIncidente == CategoriasIncidente.Accidente
                             && at.ConsecuenciasLesion == ConsecuenciasLesion.fatalidadMultiple && at.OrganizationID == _orgID)
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
                        Denominador = _indicadorHelper.AccidentesTrabajo(year, grupo.Clave.Month)
                });
                }

                foreach (var item in viewModel)
                {
                    if (item.Denominador > 0)
                    {
                        item.Resultado = Convert.ToDecimal((double)item.Numerador /
                            (double)item.Denominador) * 100;
                    }
                    else
                    {
                        item.Resultado = 0;
                    }
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

        public IEnumerable<IndicadorDetallesViewModel> GetSeveridadAccidentalidad(int[] periodo, int year, int _orgID)
        {
            try
            {
                var denominador = _indicadorHelper.PromedioTrabajadores(year, _orgID);

                var result = from at in _empresaContext.Incidentes
                             where (at.FechaIncidente.Year == year && periodo.Contains(at.FechaIncidente.Month) && at.CategoriasIncidente == CategoriasIncidente.Accidente && at.OrganizationID == _orgID)
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
                        Denominador = (int)denominador,
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
                var result = from at in _empresaContext.Incidentes
                             where (at.FechaIncidente.Year == year && periodo.Contains(at.FechaIncidente.Month)) && at.IncapacidadMedica == true
                             group at by new { at.FechaIncidente.Year, at.FechaIncidente.Month } into           datosAgrupados
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
                    var denominador = _indicadorHelper.NumeroDiasTrabajadosMes(grupo.Clave.Month, year);
                    decimal resultado = 0;
                    if (denominador > 0)
                    {
                        resultado = Convert.ToDecimal((double)grupo.Dias /
                        (double)_indicadorHelper.NumeroDiasTrabajadosMes(grupo.Clave.Month, year) * 100);
                    }

                    viewModel.Add(new IndicadorDetallesViewModel
                    {
                        MesAnn = (grupo.Clave.Month + "-" + grupo.Clave.Year).ToString(),
                        Numerador = grupo.Dias,
                        Resultado = resultado
                    });
                }
                return viewModel;
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
        }

        public IEnumerable<IndicadorDetallesViewModel> GetFatorRiesgoOcupacional(int year, int _orgID)
        {
            try
            {
                var result = from r in _empresaContext.Riesgos
                             where r.OrganizationID == _orgID
                             group r by new { r.CategoriaPeligroID } into datosAgrupados
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
                    item.MesAnn = _empresaContext.CategoriasPeligros.FirstOrDefault(cp => cp.ID == item.ID).Descripcion.ToUpper();
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

        public IEnumerable<IndicadorDetallesViewModel> GetAllValueRisks(int _orgID)
        {
            try
            {
                var result = _empresaContext.Riesgos
                    .Where(r => r.OrganizationID == _orgID)
                    .ToList();
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

        public IEnumerable<IndicadorDetallesViewModel> GetAllValueActivitys(int _orgID)
        {
            try
            {
                var result = _empresaContext.Riesgos
                    .Where(r => r.OrganizationID == _orgID)
                    .ToList();
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

        public IEnumerable<IndicadorDetallesViewModel> GetAllValueDangers(int _orgID)
        {
            try
            {
                var result = _empresaContext.Riesgos
                    .Where(r =>r.OrganizationID == _orgID)
                    .ToList();
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

        public IEnumerable<IndicadorDetallesViewModel> GetAllValueEfects(int _orgID)
        {
            try
            {
                var result = _empresaContext.Riesgos
                    .Where(r => r.OrganizationID == _orgID)
                    .ToList();
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
        public IEnumerable<IndicadorDetallesViewModel> GetAllNoConformance(int _orgID)
        {
            try
            {
                var result = from a in _empresaContext.Acciones
                             where (a.OrganizationID == _orgID)
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

        public IEnumerable<IndicadorDetallesViewModel> GetAllValueActions(int _orgID)
        {
            try
            {
                var result = _empresaContext.Acciones
                    .Where(r => r.OrganizationID == _orgID)
                    .ToList();
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

        public IEnumerable<IndicadorDetallesViewModel> GetAllValueCorrectiveActions(int year, int _orgID)
        {
            try
            {
                var result = _empresaContext.Acciones
                    .Where(a => a.FechaSolicitud.Year == year && a.OrganizationID == _orgID)
                    .ToList();
                var correctivas = 0;
                var preventivas = 0;
                var mejoras = 0;
                var total = 0;
                foreach (var item in result)
                {
                    total++;
                    switch (item.Categoria)
                    {
                        case CategoriasAccion.Correctiva:
                            correctivas++;
                            break;

                        case CategoriasAccion.Preventiva:
                            preventivas++;
                            break;

                        case CategoriasAccion.Mejora:
                            mejoras++;
                            break;
                    }
                }

                decimal pCorrectiva = Math.Round(Convert.ToDecimal((double)correctivas / (double)total * 100), 2);
                decimal pPreventiva = Math.Round(Convert.ToDecimal((double)preventivas / (double)total * 100), 2);
                decimal pMejora = Math.Round(Convert.ToDecimal((double)mejoras / (double)total * 100), 2);
                var viewModel = new List<IndicadorDetallesViewModel>();

                viewModel.Add(new IndicadorDetallesViewModel
                {
                    ID = 1,
                    MesAnn = "Preventivas",
                    Resultado = pPreventiva
                });

                viewModel.Add(new IndicadorDetallesViewModel
                {
                    ID = 2,
                    MesAnn = "Correctivas",
                    Resultado = pCorrectiva
                });

                viewModel.Add(new IndicadorDetallesViewModel
                {
                    ID = 3,
                    MesAnn = "Mejoras",
                    Resultado = pMejora
                });

                return viewModel;
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
        }
        public IEnumerable<IndicadorDetallesViewModel> GetAllCalifications(int id)
        {
            try
            {
                var list = (from c in _empresaContext.Califications
                            join n in _empresaContext.Normas on c.NormaID equals n.ID
                            where c.EvaluationID == id
                            orderby n.Item
                            select new
                            {
                                ID = c.ID,
                                Ciclo = n.Ciclo,
                                Standard = n.Standard,
                                Valor = n.Valor,
                                Valoration = c.Valoration,
                            }
                ).ToList();
                decimal pMaximo = 0;
                decimal pObtenido = 0;
                decimal hMaximo = 0;
                decimal hObtenido = 0;
                decimal vMaximo = 0;
                decimal vObtenido = 0;
                decimal aMaximo = 0;
                decimal aObtenido = 0;
                foreach (var item in list)
                {
                    switch (item.Ciclo)
                    {
                        case "P":
                            pMaximo += item.Valor;
                            pObtenido += item.Valoration;
                            break;
                        case "H":
                            hMaximo += item.Valor;
                            hObtenido += item.Valoration;
                            break;
                        case "V":
                            vMaximo += item.Valor;
                            vObtenido += item.Valoration;
                            break;
                        case "A":
                            aMaximo += item.Valor;
                            aObtenido += item.Valoration;
                            break;
                        default:
                            pMaximo += item.Valor;
                            pObtenido += item.Valoration;
                            break;
                    }
                }
                var viewModel = new List<IndicadorDetallesViewModel>();
                viewModel.Add(new IndicadorDetallesViewModel
                {
                    ID = 1,
                    MesAnn = "I. PLANEAR",
                    Resultado = pMaximo,
                    Resultado1 = pObtenido
                });
                viewModel.Add(new IndicadorDetallesViewModel
                {
                    ID = 2,
                    MesAnn = "II. HACER",
                    Resultado = hMaximo,
                    Resultado1 = hObtenido
                });

                viewModel.Add(new IndicadorDetallesViewModel
                {
                    ID = 3,
                    MesAnn = "III. VERIFICAR",
                    Resultado = vMaximo,
                    Resultado1 = vObtenido
                });

                viewModel.Add(new IndicadorDetallesViewModel
                {
                    ID = 4,
                    MesAnn = "IV. ACTUAR",
                    Resultado = aMaximo,
                    Resultado1 = aObtenido
                });
                return viewModel;
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
        }
        public IEnumerable<IndicadorDetallesViewModel> GetAllCalificationsStandard(int id)
        {
            try
            {
                var list = (from c in _empresaContext.Califications
                            join n in _empresaContext.Normas on c.NormaID equals n.ID
                            where c.EvaluationID == id
                            orderby n.Item
                            select new
                            {
                                ID = c.ID,
                                Ciclo = n.Ciclo,
                                Standard = n.Standard,
                                Valor = n.Valor,
                                Valoration = c.Valoration,
                            }
                ).ToList();
                decimal rMaximo = 0;
                decimal rObtenido = 0;
                decimal iMaximo = 0;
                decimal iObtenido = 0;
                decimal sMaximo = 0;
                decimal sObtenido = 0;
                decimal pMaximo = 0;
                decimal pObtenido = 0;
                decimal aMaximo = 0;
                decimal aObtenido = 0;
                decimal vMaximo = 0;
                decimal vObtenido = 0;
                decimal mMaximo = 0;
                decimal mObtenido = 0;
                foreach (var item in list)
                {
                    switch (item.Standard)
                    {
                        case "R":
                            rMaximo += item.Valor;
                            rObtenido += item.Valoration;
                            break;
                        case "I":
                            iMaximo += item.Valor;
                            iObtenido += item.Valoration;
                            break;
                        case "S":
                            sMaximo += item.Valor;
                            sObtenido += item.Valoration;
                            break;
                        case "P":
                            pMaximo += item.Valor;
                            pObtenido += item.Valoration;
                            break;
                        case "A":
                            aMaximo += item.Valor;
                            aObtenido += item.Valoration;
                            break;
                        case "V":
                            vMaximo += item.Valor;
                            vObtenido += item.Valoration;
                            break;
                        case "M":
                            mMaximo += item.Valor;
                            mObtenido += item.Valoration;
                            break;
                        default:
                            pMaximo += item.Valor;
                            pObtenido += item.Valoration;
                            break;
                    }
                }
                var viewModel = new List<IndicadorDetallesViewModel>();
                viewModel.Add(new IndicadorDetallesViewModel
                {
                    ID = 1,
                    MesAnn = "RECURSOS",
                    Resultado = rMaximo,
                    Resultado1 = rObtenido
                });
                viewModel.Add(new IndicadorDetallesViewModel
                {
                    ID = 2,
                    MesAnn = "GESTION INTEGRAL",
                    Resultado = iMaximo,
                    Resultado1 = iObtenido
                });
                viewModel.Add(new IndicadorDetallesViewModel
                {
                    ID = 3,
                    MesAnn = "GESTIÓN SALUD",
                    Resultado = sMaximo,
                    Resultado1 = sObtenido
                });
                viewModel.Add(new IndicadorDetallesViewModel
                {
                    ID = 4,
                    MesAnn = "GESTIÓN RIESGOS",
                    Resultado = pMaximo,
                    Resultado1 = pObtenido
                });
                viewModel.Add(new IndicadorDetallesViewModel
                {
                    ID = 5,
                    MesAnn = "GESTIÓN AMENAZAS",
                    Resultado = aMaximo,
                    Resultado1 = aObtenido
                });
                viewModel.Add(new IndicadorDetallesViewModel
                {
                    ID = 6,
                    MesAnn = "VERIFICACIÓN",
                    Resultado = vMaximo,
                    Resultado1 = vObtenido
                });
                viewModel.Add(new IndicadorDetallesViewModel
                {
                    ID = 7,
                    MesAnn = "MEJORAMIENTO",
                    Resultado = mMaximo,
                    Resultado1 = mObtenido
                });
                return viewModel;
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
        }

        public IEnumerable<IndicadorDetallesViewModel> GetAllEfectiveActions(int year, int _orgID)
        {
            try
            {
                var result = _empresaContext.Acciones
                    .Where(a => a.OrganizationID == _orgID && a.FechaCierre.Year == year)
                    .ToList();
                var efectives = 0;
                var notEfectives = 0;
                var total = 0;
                decimal pEfectives = 0;
                decimal pNotEfectives = 0;
                foreach (var item in result)
                {
                    total++;
                    if (item.Efectiva)
                    {
                        efectives++;
                    }
                    else
                    {
                        notEfectives++;
                    }
                }

                if (total > 0)
                {
                    pEfectives = Math.Round(Convert.ToDecimal((double)efectives / (double)total * 100), 2);
                    pNotEfectives = Math.Round(Convert.ToDecimal((double)notEfectives / (double)total * 100), 2);
                }
                var viewModel = new List<IndicadorDetallesViewModel>();

                viewModel.Add(new IndicadorDetallesViewModel
                {
                    ID = 1,
                    MesAnn = "Efectivas",
                    Resultado = pEfectives
                });

                viewModel.Add(new IndicadorDetallesViewModel
                {
                    ID = 2,
                    MesAnn = "No Efectivas",
                    Resultado = pNotEfectives
                });

                return viewModel;
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
        }
        public IEnumerable<IndicadorDetallesViewModel> GetAllScholarship(int year, int _orgID)
        {
            try
            {
                var result = from t in _empresaContext.Trabajadores
                             where t.OrganizationID == _orgID
                             group t by new { t.Escolaridad } into datosAgrupados
                             orderby datosAgrupados.Count() ascending
                             select new { Clave = datosAgrupados.Key, Datos = datosAgrupados };

                var viewModel = new List<IndicadorDetallesViewModel>();
                foreach (var grupo in result)
                {
                    viewModel.Add(new IndicadorDetallesViewModel
                    {
                        MesAnn = grupo.Clave.Escolaridad.ToString(),
                        Numerador = grupo.Datos.Count(),
                        Denominador = 1,
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
        public IEnumerable<IndicadorDetallesViewModel> GetAllWorkAreas(int year, int _orgID)
        {
            try
            {
                var result = from t in _empresaContext.Trabajadores
                             where t.OrganizationID == _orgID
                             group t by new { t.WorkArea } into datosAgrupados
                             orderby datosAgrupados.Count() ascending
                             select new { Clave = datosAgrupados.Key, Datos = datosAgrupados };

                var viewModel = new List<IndicadorDetallesViewModel>();
                foreach (var grupo in result)
                {
                    viewModel.Add(new IndicadorDetallesViewModel
                    {
                        MesAnn = grupo.Clave.WorkArea.ToString(),
                        Numerador = grupo.Datos.Count(),
                        Denominador = 1,
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
        public IEnumerable<IndicadorDetallesViewModel> GetAllOcupaciones(int year, int _orgID)
        {
            try
            {
                var result = from t in _empresaContext.Trabajadores
                             where t.OrganizationID == _orgID
                             group t by new { t.CargoID } into datosAgrupados
                             orderby datosAgrupados.Count() ascending
                             select new { Clave = datosAgrupados.Key, Datos = datosAgrupados };

                var viewModel = new List<IndicadorDetallesViewModel>();
                foreach (var grupo in result)
                {
                    viewModel.Add(new IndicadorDetallesViewModel
                    {
                        MesAnn = _empresaContext.Cargos.Find(grupo.Clave.CargoID).Descripcion,
                        Numerador = grupo.Datos.Count(),
                        Denominador = 1,
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
        public IEnumerable<IndicadorDetallesViewModel> GetAllTiposVinculacion(int year, int _orgID)
        {
            try
            {
                var result = from t in _empresaContext.Trabajadores
                             where t.OrganizationID == _orgID
                             group t by new { t.TipoVinculacion } into datosAgrupados
                             orderby datosAgrupados.Count() ascending
                             select new { Clave = datosAgrupados.Key, Datos = datosAgrupados };

                var viewModel = new List<IndicadorDetallesViewModel>();
                foreach (var grupo in result)
                {
                    viewModel.Add(new IndicadorDetallesViewModel
                    {
                        MesAnn = grupo.Clave.TipoVinculacion.ToString(),
                        Numerador = grupo.Datos.Count(),
                        Denominador = 1,
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
        public IEnumerable<IndicadorDetallesViewModel> GetAllEstadosCivil(int year, int _orgID)
        {
            try
            {
                var result = from t in _empresaContext.Trabajadores
                             where t.OrganizationID == _orgID
                             group t by new { t.EstadoCivil } into datosAgrupados
                             orderby datosAgrupados.Count() ascending
                             select new { Clave = datosAgrupados.Key, Datos = datosAgrupados };

                var viewModel = new List<IndicadorDetallesViewModel>();
                foreach (var grupo in result)
                {
                    viewModel.Add(new IndicadorDetallesViewModel
                    {
                        MesAnn = grupo.Clave.EstadoCivil.ToString(),
                        Numerador = grupo.Datos.Count(),
                        Denominador = 1,
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
        public IEnumerable<IndicadorDetallesViewModel> GetAllTiposVivienda(int year, int _orgID)
        {
            try
            {
                var result = from t in _empresaContext.Trabajadores
                             where t.OrganizationID == _orgID
                             group t by new { t.TenenciaVivienda } into datosAgrupados
                             orderby datosAgrupados.Count() ascending
                             select new { Clave = datosAgrupados.Key, Datos = datosAgrupados };

                var viewModel = new List<IndicadorDetallesViewModel>();
                foreach (var grupo in result)
                {
                    viewModel.Add(new IndicadorDetallesViewModel
                    {
                        MesAnn = grupo.Clave.TenenciaVivienda.ToString(),
                        Numerador = grupo.Datos.Count(),
                        Denominador = 1,
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
        public IEnumerable<IndicadorDetallesViewModel> GetAllTiposJornada(int year, int _orgID)
        {
            try
            {
                var result = from t in _empresaContext.Trabajadores
                             where t.OrganizationID == _orgID
                             group t by new { t.TipoJornada } into datosAgrupados
                             orderby datosAgrupados.Count() ascending
                             select new { Clave = datosAgrupados.Key, Datos = datosAgrupados };

                var viewModel = new List<IndicadorDetallesViewModel>();
                foreach (var grupo in result)
                {
                    viewModel.Add(new IndicadorDetallesViewModel
                    {
                        MesAnn = grupo.Clave.TipoJornada.ToString(),
                        Numerador = grupo.Datos.Count(),
                        Denominador = 1,
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
        public IEnumerable<IndicadorDetallesViewModel> GetAllNumeroHijos(int year, int _orgID)
        {
            try
            {
                var result = from t in _empresaContext.Trabajadores
                             where t.OrganizationID == _orgID
                             group t by new { t.NumberHijos } into datosAgrupados
                             orderby datosAgrupados.Count() ascending
                             select new { Clave = datosAgrupados.Key, Datos = datosAgrupados };

                var viewModel = new List<IndicadorDetallesViewModel>();
                foreach (var grupo in result)
                {
                    viewModel.Add(new IndicadorDetallesViewModel
                    {
                        MesAnn = grupo.Clave.NumberHijos.ToString(),
                        Numerador = grupo.Datos.Count(),
                        Denominador = 1,
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
    }
}