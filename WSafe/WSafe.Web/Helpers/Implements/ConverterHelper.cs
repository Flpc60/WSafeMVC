using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WSafe.Domain.Data.Entities;
using WSafe.Web.Models;

namespace WSafe.Domain.Helpers.Implements
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly EmpresaContext _empresaContext;
        private readonly IComboHelper _comboHelper;
        private readonly IGestorHelper _gestorHelper;
        private readonly IChartHelper _chartHelper;
        private readonly IIndicadorHelper _indicadorHelper;
        public ConverterHelper
            (EmpresaContext empresaContext,
            IComboHelper comboHelper,
            IGestorHelper gestorHelper,
            IChartHelper chartHelper,
            IIndicadorHelper indicadorHelper)
        {
            _empresaContext = empresaContext;
            _comboHelper = comboHelper;
            _gestorHelper = gestorHelper;
            _chartHelper = chartHelper;
            _indicadorHelper = indicadorHelper;
        }
        public async Task<Riesgo> ToRiesgoAsync(RiesgoViewModel model, bool isNew)
        {
            var result = new Riesgo
            {
                ID = isNew ? 0 : model.ID,
                ZonaID = model.ZonaID,
                ProcesoID = model.ProcesoID,
                ActividadID = model.ActividadID,
                TareaID = model.TareaID,
                Rutinaria = model.Rutinaria,
                CategoriaPeligroID = model.CategoriaPeligroID,
                PeligroID = model.PeligroID,
                EfectosPosibles = model.EfectosPosibles,
                NivelDeficiencia = model.NivelDeficiencia,
                NivelExposicion = model.NivelExposicion,
                NivelConsecuencia = model.NivelConsecuencia,
                Aceptabilidad = model.AceptabilidadNR,
                NroExpuestos = model.NroExpuestos,
                RequisitoLegal = model.RequisitoLegal,
                IncidenteID = model.IncidenteID
            };
            return result;
        }
        public RiesgoViewModel ToRiesgoViewModel(Riesgo riesgo)
        {
            var model = new RiesgoViewModel
            {
                ID = riesgo.ID,
                ZonaID = riesgo.ZonaID,
                ProcesoID = riesgo.ProcesoID,
                ActividadID = riesgo.ActividadID,
                TareaID = riesgo.TareaID,
                Rutinaria = riesgo.Rutinaria,
                CategoriaPeligroID = riesgo.CategoriaPeligroID,
                CategoriasPeligros = _comboHelper.GetComboCategoriaPeligros(),
                PeligroID = riesgo.PeligroID,
                Peligros = _comboHelper.GetComboPeligros(riesgo.CategoriaPeligroID),
                EfectosPosibles = riesgo.EfectosPosibles,
                NivelDeficiencia = riesgo.NivelDeficiencia,
                NivelesDeficiencia = _gestorHelper.GetNivelDeficiencia(riesgo.NivelDeficiencia),
                NivelExposicion = riesgo.NivelExposicion,
                NivelesExposicion = _gestorHelper.GetNivelExposicion(riesgo.NivelExposicion),
                NivelConsecuencia = riesgo.NivelConsecuencia,
                NivelesConsecuencia = _gestorHelper.GetNivelConsecuencia(riesgo.NivelConsecuencia),
                AceptabilidadNR = riesgo.Aceptabilidad,
                NroExpuestos = riesgo.NroExpuestos,
                RequisitoLegal = riesgo.RequisitoLegal,
                IncidenteID = riesgo.IncidenteID,
                Zonas = _comboHelper.GetComboZonas(),
                Procesos = _comboHelper.GetComboProcesos(),
                Actividades = _comboHelper.GetComboActividades(),
                Tareas = _comboHelper.GetComboTareas()
            };

            return model;
        }
        public RiesgoViewModel ToRiesgoViewModelNew()
        {
            var model = new RiesgoViewModel
            {
                Zonas = _comboHelper.GetComboZonas(),
                Procesos = _comboHelper.GetComboProcesos(),
                Actividades = _comboHelper.GetComboActividades(),
                Tareas = _comboHelper.GetComboTareas(),
                CategoriasPeligros = _comboHelper.GetComboCategoriaPeligros(),
                Peligros = _comboHelper.GetComboPeligros(1),
            };
            return model;
        }
        public AccionViewModel ToAccionViewModelNew()
        {
            var model = new AccionViewModel
            {
                Zonas = _comboHelper.GetComboZonas(),
                Procesos = _comboHelper.GetComboProcesos(),
                Actividades = _comboHelper.GetComboActividades(),
                Tareas = _comboHelper.GetComboTareas(),
                Trabajadores = _comboHelper.GetComboTrabajadores(),
                Planes = new List<PlanAction>(),
                Seguimientos = new List<Seguimiento>()
            };

            model.Planes.Add(new PlanAction());
            model.Seguimientos.Add(new Seguimiento());
            return model;
        }
        public AccionViewModel ToAccionViewModel(Accion accion)
        {
            var model = new AccionViewModel
            {
                ID = accion.ID,
                ZonaID = accion.ZonaID,
                Zonas = _comboHelper.GetComboZonas(),
                ProcesoID = accion.ProcesoID,
                Procesos = _comboHelper.GetComboProcesos(),
                ActividadID = accion.ActividadID,
                Actividades = _comboHelper.GetComboActividades(),
                TareaID = accion.TareaID,
                Tareas = _comboHelper.GetComboTareas(),
                FechaSolicitud = accion.FechaSolicitud,
                Categoria = accion.Categoria,
                TrabajadorID = accion.TrabajadorID,
                Trabajadores = _comboHelper.GetComboTrabajadores(),
                FuenteAccion = accion.FuenteAccion,
                Origen = _gestorHelper.GetFuenteAccion(accion.FuenteAccion).ToUpper(),
                Descripcion = accion.Descripcion,
                EficaciaAntes = accion.EficaciaAntes,
                EficaciaDespues = accion.EficaciaDespues,
                FechaCierre = accion.FechaCierre,
                Efectiva = accion.Efectiva,
                Estado = accion.Estado,
                Planes = new List<PlanAction>(),
                Seguimientos = new List<Seguimiento>(),
                FechaSolicitudStr = accion.FechaSolicitud.ToString("yyyy-MM-dd"),
                FechaCierreStr = accion.FechaCierre.ToString("yyyy-MM-dd")
            };
            model.Planes.Add(new PlanAction() { AccionID = accion.ID });
            model.Seguimientos.Add(new Seguimiento() { AccionID = accion.ID });
            return model;
        }
        public async Task<Accion> ToAccionAsync(Accion model, bool isNew)
        {
            var result = new Accion
            {
                ID = isNew ? 0 : model.ID,
                ZonaID = model.ZonaID,
                ProcesoID = model.ProcesoID,
                ActividadID = model.ActividadID,
                TareaID = model.TareaID,
                FechaSolicitud = model.FechaSolicitud,
                Categoria = model.Categoria,
                TrabajadorID = model.TrabajadorID,
                FuenteAccion = model.FuenteAccion,
                Descripcion = model.Descripcion,
                EficaciaAntes = model.EficaciaAntes,
                EficaciaDespues = model.EficaciaDespues,
                FechaCierre = model.FechaCierre,
                Efectiva = model.Efectiva,
                Estado = model.Estado
            };
            return result;
        }

        public async Task<Incidente> ToIncidenteAsync(IncidenteViewModel model, bool isNew)
        {
            var result = new Incidente
            {
                ID = isNew ? 0 : model.ID,
                ZonaID = model.ZonaID,
                ProcesoID = model.ProcesoID,
                ActividadID = model.ActividadID,
                TareaID = model.TareaID,
                FechaReporte = model.FechaReporte,
                FechaIncidente = model.FechaIncidente,
                CategoriasIncidente = model.CategoriasIncidente,
                IncapacidadMedica = model.IncapacidadMedica,
                DiasIncapacidad = model.DiasIncapacidad,
                NaturalezaLesion = model.NaturalezaLesion,
                PartesAfectadas = model.PartesAfectadas,
                TipoIncidente = model.TipoIncidente,
                AgenteLesion = model.AgenteLesion,
                ActosInseguros = model.ActosInseguros,
                CondicionesInsegura = model.CondicionesInsegura,
                TipoDaño = model.TipoDaño,
                Afectacion = model.Afectacion,
                DañosOcasionados = model.DañosOcasionados,
                TipoVehiculo = model.TipoVehiculo,
                MarcaVehiculo = model.MarcaVehiculo,
                ModeloVehiculo = model.ModeloVehiculo,
                KilometrajeVehiculo = model.KilometrajeVehiculo,
                CostosEstimados = model.CostosEstimados,
                DescripcionIncidente = model.DescripcionIncidente,
                EvitarIncidente = model.EvitarIncidente,
                AccionesInmediatas = model.AccionesInmediatas,
                ComentariosAdicionales = model.ComentariosAdicionales,
                AtencionBrindada = model.AtencionBrindada,
                EquiposInvestigador = model.EquiposInvestigador,
                LesionPersonal = model.LesionPersonal,
                DañoMaterial = model.DañoMaterial,
                MedioAmbiente = model.MedioAmbiente,
                Imagen = model.Imagen,
                RequiereInvestigacion = model.RequiereInvestigacion,
                ConsecuenciasLesion = model.ConsecuenciasLesion,
                ConsecuenciasDaño = model.ConsecuenciasDaño,
                ConsecuenciasMedio = model.ConsecuenciasMedio,
                ConsecuenciasImagen = model.ConsecuenciasImagen,
                Probabilidad = model.Probabilidad,
                TrabajadorID = model.TrabajadorID
            };
            return result;
        }
        public IncidenteViewModel ToIncidenteViewModelNew()
        {
            var model = new IncidenteViewModel
            {
                Zonas = _comboHelper.GetComboZonas(),
                Procesos = _comboHelper.GetComboProcesos(),
                Actividades = _comboHelper.GetComboActividades(),
                Tareas = _comboHelper.GetComboTareas(),
                Trabajadores = _comboHelper.GetComboTrabajadores(),
                Lesionados = new List<AccidentadoVM>()
            };

            return model;
        }

        public IncidenteViewModel ToIncidenteViewModel(Incidente incidente)
        {
            var model = new IncidenteViewModel
            {
                ID = incidente.ID,
                ZonaID = incidente.ZonaID,
                Zonas = _comboHelper.GetComboZonas(),
                ProcesoID = incidente.ProcesoID,
                Procesos = _comboHelper.GetComboProcesos(),
                ActividadID = incidente.ActividadID,
                Actividades = _comboHelper.GetComboActividades(),
                TareaID = incidente.TareaID,
                Tareas = _comboHelper.GetComboTareas(),
                FechaReporte = incidente.FechaReporte,
                FechaIncidente = incidente.FechaIncidente,
                CategoriasIncidente = incidente.CategoriasIncidente,
                IncapacidadMedica = incidente.IncapacidadMedica,
                DiasIncapacidad = incidente.DiasIncapacidad,
                Trabajadores = _comboHelper.GetComboTrabajadores(),
                NaturalezaLesion = incidente.NaturalezaLesion,
                PartesAfectadas = incidente.PartesAfectadas,
                TipoIncidente = incidente.TipoIncidente,
                AgenteLesion = incidente.AgenteLesion,
                ActosInseguros = incidente.ActosInseguros,
                CondicionesInsegura = incidente.CondicionesInsegura,
                TipoDaño = incidente.TipoDaño,
                Afectacion = incidente.Afectacion,
                DañosOcasionados = incidente.DañosOcasionados,
                TipoVehiculo = incidente.TipoVehiculo,
                MarcaVehiculo = incidente.MarcaVehiculo,
                ModeloVehiculo = incidente.ModeloVehiculo,
                KilometrajeVehiculo = incidente.KilometrajeVehiculo,
                CostosEstimados = incidente.CostosEstimados,
                DescripcionIncidente = incidente.DescripcionIncidente,
                EvitarIncidente = incidente.EvitarIncidente,
                AccionesInmediatas = incidente.AccionesInmediatas,
                ComentariosAdicionales = incidente.ComentariosAdicionales,
                AtencionBrindada = incidente.AtencionBrindada,
                EquiposInvestigador = incidente.EquiposInvestigador,
                LesionPersonal = incidente.LesionPersonal,
                DañoMaterial = incidente.DañoMaterial,
                MedioAmbiente = incidente.MedioAmbiente,
                Imagen = incidente.Imagen,
                RequiereInvestigacion = incidente.RequiereInvestigacion,
                ConsecuenciasLesion = incidente.ConsecuenciasLesion,
                ConsecuenciasDaño = incidente.ConsecuenciasDaño,
                ConsecuenciasMedio = incidente.ConsecuenciasMedio,
                ConsecuenciasImagen = incidente.ConsecuenciasImagen,
                Probabilidad = incidente.Probabilidad,
                Lesionados = new List<AccidentadoVM>()
            };

            return model;
        }

        public IndicadorViewModel ToIndicadorViewModel(Indicador indicador)
        {
            //var datos = _chartHelper.GetFrecuenciaAccidentes(Convert.ToDateTime("2021/06/01"), Convert.ToDateTime("2021/12/31"));
            //_chartHelper.DrawImagen(indicador.TipoChart, indicador.Nombre, datos);
            var model = new IndicadorViewModel
            {
                ID = indicador.ID,
                Nombre = indicador.Nombre,
                Definicion = indicador.Definicion,
                Numerador = indicador.Numerador,
                Denominador = indicador.Denominador,
                Formula = indicador.Formula,
                Interpretacion = indicador.Interpretacion,
                Periodicidad = indicador.Periodicidad,
                //Datos = datos,
                Imagen = "~/Images/chart05.jpg"
            };
            return model;
        }
        public IndicadorViewModel ToIndicadorViewModelNew(Indicador indicador, int[] periodo, int year) 
        {
            Random random = new Random();
            var filename = "chart" + random.Next(1, 100) + ".jpg";
            var filePathName = "~/Images/" + filename;

            var datos = _chartHelper.GetFrecuenciaAccidentes(periodo, year);
            switch (indicador.ID)
            {
                case 1:
                    datos = _chartHelper.GetFrecuenciaAccidentes(periodo, year);
                    break;

                case 2:
                    datos = _chartHelper.GetSeveridadAccidentalidad(periodo, year);
                    break;

                case 3:
                    datos = _chartHelper.GetAccidentesTrabajoMortales(periodo, year);
                    break;

                case 4:
                    datos = _chartHelper.GetAccidentesTrabajoMortales(periodo, year);
                    break;

                case 5:
                    datos = _chartHelper.GetAccidentesTrabajoMortales(periodo, year);
                    break;

                case 6:
                    datos = _chartHelper.GetAusentismoCausaMedica(periodo, year);
                    break;

                case 7:
                    datos = _chartHelper.GetFatorRiesgoOcupacional();
                    break;
            }

            _chartHelper.DrawImagen(filePathName, indicador.TipoChart, indicador.Nombre, datos);
            var model = new IndicadorViewModel
            {
                ID = indicador.ID,
                Year = year,
                Periodo = _gestorHelper.GetPeriodo(periodo),
                Nombre = indicador.Nombre,
                Definicion = indicador.Definicion,
                Numerador = indicador.Numerador,
                Denominador = indicador.Denominador,
                Formula = indicador.Formula,
                Interpretacion = indicador.Interpretacion,
                Periodicidad = indicador.Periodicidad,
                Datos = datos,
                Imagen = filePathName,
                Titulo = ("Ficha tecnica indicador " + indicador.Nombre).ToUpper()
            };
            return model;
        }
        public IEnumerable<ListaRiesgosVM> ToRiesgoViewModelList(IEnumerable<Riesgo> riesgo)
        {
            var rutinaria = "";
            var requisito = "";
            var model = new List<ListaRiesgosVM>();
            foreach (var item in riesgo)
            {

                if (item.Rutinaria)
                {
                    rutinaria = "Si";
                }
                else
                {
                    rutinaria = "No";
                }

                if (item.RequisitoLegal)
                {
                    requisito = "Si";
                }
                else
                {
                    requisito = "No";
                }


                model.Add(new ListaRiesgosVM
                {
                    ID = item.ID,
                    Zona = _empresaContext.Zonas.Find(item.ZonaID).Descripcion,
                    Proceso = _empresaContext.Procesos.Find(item.ProcesoID).Descripcion,
                    Actividad = _empresaContext.Actividades.Find(item.ActividadID).Descripcion,
                    Tarea = _empresaContext.Tareas.Find(item.TareaID).Descripcion,
                    Rutinaria = item.Rutinaria,
                    Clasificacion = _empresaContext.CategoriasPeligros.Find(item.CategoriaPeligroID).Descripcion,
                    Peligro = _empresaContext.Peligros.Find(item.PeligroID).Descripcion,
                    EfectosPosibles = item.EfectosPosibles,
                    NivelDeficiencia = item.NivelDeficiencia,
                    NivelExposicion = item.NivelExposicion,
                    NivelConsecuencia = item.NivelConsecuencia,
                    Aceptabilidad = item.Aceptabilidad,
                    NroExpuestos = item.NroExpuestos,
                    RequisitoLegal = item.RequisitoLegal,
                    TextRutinaria = rutinaria,
                    TextRequisito = requisito
                });
            }

            return model;
        }
        public AccidentadoVM ToLesionadoViewModel(Trabajador lesionado)
        {
            var modelo = new AccidentadoVM()
            {
                TrabajadorID = lesionado.ID,
                Documento = lesionado.Documento,
                NombreCompleto = lesionado.NombreCompleto,
                FechaNacimiento = lesionado.FechaNacimiento,
                Genero = _gestorHelper.GetGenero(lesionado.Genero),
                EstadoCivil = _gestorHelper.GetEstadoCivil(lesionado.EstadoCivil),
                TipoVinculacion = _gestorHelper.GetTipoVinculacion(lesionado.TipoVinculacion),
                Cargo = lesionado.Cargo.Descripcion
            };
            return modelo;
        }
        public IEnumerable<AccidentadoVM> ToListLesionadosVM(IEnumerable<Accidentado> lesionados)
        {
            var modelo = new List<AccidentadoVM>();
            foreach (var item in lesionados)
            {
                var lesionado = _empresaContext.Trabajadores.Find(item.TrabajadorID);
                modelo.Add(new AccidentadoVM
                {
                    ID = item.ID,
                    TrabajadorID = item.TrabajadorID,
                    Documento = lesionado.Documento,
                    NombreCompleto = lesionado.NombreCompleto,
                    FechaNacimiento = lesionado.FechaNacimiento,
                    Genero = _gestorHelper.GetGenero(lesionado.Genero),
                    EstadoCivil = _gestorHelper.GetEstadoCivil(lesionado.EstadoCivil),
                    TipoVinculacion = _gestorHelper.GetTipoVinculacion(lesionado.TipoVinculacion),
                    Cargo = lesionado.Cargo.Descripcion
                });
            }
            return modelo;
        }
        public IEnumerable<AplicacionVM> ToIntervencionesViewModel(IEnumerable<Aplicacion> listaAplicacion)
        {
            var modelo = new List<AplicacionVM>();
            foreach (var item in listaAplicacion)
            {
                modelo.Add(new AplicacionVM
                {
                    ID = item.ID,
                    RiesgoID = item.RiesgoID,
                    Nombre = item.Nombre.ToUpper(),
                    CategoriaAplicacion = item.CategoriaAplicacion,
                    Intervencion = item.Intervencion,
                    Beneficios = item.Beneficios,
                    Presupuesto = item.Presupuesto,
                    TrabajadorID = item.TrabajadorID,
                    Responsable = _empresaContext.Trabajadores.Find(item.TrabajadorID).NombreCompleto.ToUpper(),
                    FechaInicial = item.FechaInicial,
                    FechaFinal = item.FechaFinal,
                    TextFechaInicial = item.FechaInicial.ToString("yyyy-MM-dd"),
                    TextFechaFinal = item.FechaFinal.ToString("yyyy-MM-dd"),
                    Observaciones = item.Observaciones,
                    TextCategoria = _gestorHelper.GetCategoriaAplicacion(item.CategoriaAplicacion),
                    TextIntervencion = _gestorHelper.GetJerarquiaControl(item.Intervencion)
                });
            }
            return modelo;
        }
        public async Task<Aplicacion> ToAplicacionAsync(AplicacionVM model, bool isNew)
        {
            var result = new Aplicacion
            {
                ID = isNew ? 0 : model.ID,
                RiesgoID = model.RiesgoID,
                Nombre = model.Nombre,
                CategoriaAplicacion = model.CategoriaAplicacion,
                Intervencion = model.Intervencion,
                Beneficios = model.Beneficios,
                Presupuesto = model.Presupuesto,
                TrabajadorID = model.TrabajadorID,
                FechaInicial = model.FechaInicial,
                FechaFinal = model.FechaFinal,
                Observaciones = model.Observaciones
            };

            return result;
        }
        public PlanAccionVM ToPlanAccionVM(PlanAction plan)
        {
            var result = new PlanAccionVM
            {
                ID = plan.ID,
                AccionID = plan.AccionID,
                FechaInicial = plan.FechaInicial.ToString("yyyy-MM-dd"),
                FechaFinal = plan.FechaFinal.ToString("yyyy-MM-dd"),
                Causa = plan.Causa,
                Categoria = _gestorHelper.GetCausaAccion(plan.Causa).ToUpper(),
                Accion = plan.Accion,
                Prioritaria = plan.Prioritaria,
                Costos = plan.Costos,
                TrabajadorID = plan.TrabajadorID,
                Responsable = _empresaContext.Trabajadores.Find(plan.TrabajadorID).NombreCompleto.ToUpper()
            };
            return result;
        }
        public PlanAccionVM ToPlanAccionVMNew()
        {
            throw new NotImplementedException();
        }
        public async Task<PlanAction> ToPlanAccionAsync(PlanAction plan)
        {
            var result = new PlanAction
            {
                ID = plan.ID,
                AccionID = plan.AccionID,
                FechaInicial = plan.FechaInicial,
                FechaFinal = plan.FechaFinal,
                Causa = plan.Causa,
                Accion = plan.Accion,
                TrabajadorID = plan.TrabajadorID,
                Prioritaria = plan.Prioritaria,
                Costos = plan.Costos,
                Responsable = plan.Responsable
            };
            return result;
        }
        public SeguimientoAccionVM ToSeguimientoAccionVM(Seguimiento seguimientoAccion)
        {
            var result = new SeguimientoAccionVM
            {
                ID = seguimientoAccion.ID,
                AccionID = seguimientoAccion.AccionID,
                FechaSeguimiento = seguimientoAccion.FechaSeguimiento.ToString("yyyy-MM-dd"),
                Resultado = seguimientoAccion.Resultado.ToUpper(),
                TrabajadorID = seguimientoAccion.TrabajadorID,
                Responsable = _empresaContext.Trabajadores.Find(seguimientoAccion.TrabajadorID).NombreCompleto.ToUpper()
            };
            return result;
        }
        public async Task<Seguimiento> ToSeguimientoAccionAsync(Seguimiento model)
        {
            var result = new Seguimiento
            {
                ID = model.ID,
                AccionID = model.AccionID,
                FechaSeguimiento = model.FechaSeguimiento,
                Resultado = model.Resultado.ToUpper(),
                TrabajadorID = model.TrabajadorID
            };
            return result;

            throw new NotImplementedException();
        }

        public IEnumerable<PlanAccionVM> ToPlanAccionVMList(IEnumerable<PlanAction> plan)
        {
            var model = new List<PlanAccionVM>();
            foreach (var item in plan)
            {
                model.Add(new PlanAccionVM
                {
                    ID = item.ID,
                    AccionID = item.AccionID,
                    FechaInicial = item.FechaInicial.ToString("dd/MM/yyyy"),
                    FechaFinal = item.FechaFinal.ToString("dd/MM/yyyy"),
                    Causa = item.Causa,
                    Categoria = _gestorHelper.GetCausaAccion(item.Causa).ToUpper(),
                    Accion = item.Accion.ToUpper(),
                    Prioritaria = item.Prioritaria,
                    Costos = item.Costos,
                    TrabajadorID = item.TrabajadorID,
                    Responsable = _empresaContext.Trabajadores.Find(item.TrabajadorID).NombreCompleto.ToUpper()
                }); ;
            }
            return model;
        }
        // Crea nueva lista de AccionViewModel
        public IEnumerable<AccionViewModel> ToAccionVMList(IEnumerable<Accion> accion)
        {
            var model = new List<AccionViewModel>();
            foreach (var item in accion)
            {
                model.Add(new AccionViewModel
                {
                    ID = item.ID,
                    FechaSolicitud = item.FechaSolicitud,
                    Categoria = item.Categoria,
                    TrabajadorID = item.TrabajadorID,
                    Trabajadores = _comboHelper.GetComboTrabajadores(),
                    FuenteAccion = item.FuenteAccion,
                    Origen = _gestorHelper.GetFuenteAccion(item.FuenteAccion).ToUpper(),
                    Descripcion = item.Descripcion.ToUpper(),
                    EficaciaAntes = item.EficaciaAntes,
                    EficaciaDespues = item.EficaciaDespues,
                    FechaCierre = item.FechaCierre,
                    Efectiva = item.Efectiva,
                    Estado = item.Estado
                });
            }
            return model;
        }
        public IEnumerable<SeguimientoAccionVM> ToSeguimientoAccionVMList(IEnumerable<Seguimiento> accion)
        {
            var model = new List<SeguimientoAccionVM>();
            foreach (var item in accion)
            {
                model.Add(new SeguimientoAccionVM
                {
                    ID = item.ID,
                    AccionID = item.AccionID,
                    FechaSeguimiento = item.FechaSeguimiento.ToString("dd/MM/yyyy"),
                    Resultado = item.Resultado.ToUpper(),
                    TrabajadorID = item.TrabajadorID,
                    Responsable = _empresaContext.Trabajadores.Find(item.TrabajadorID).NombreCompleto.ToUpper()
                }); ;
            }
            return model;
        }
        public _DetailsAccionVM ToAccionVMFull(Accion accion, int id)
        {
            var planes = _empresaContext.PlanActions.Where(pa => pa.AccionID == accion.ID).ToList();
            var sigue = _empresaContext.Seguimientos.Where(sa => sa.AccionID == accion.ID).ToList();
            var responsable = _empresaContext.Trabajadores
                .Include(c => c.Cargo)
                .FirstOrDefault(t => t.ID == accion.TrabajadorID);

            var document = _empresaContext.Documents.FirstOrDefault(d => d.ID == id);
            var model = new _DetailsAccionVM
            {
                ID = accion.ID,
                Formato = document.Formato,
                Estandar = document.Estandar,
                Titulo = document.Titulo,
                Version = document.Version,
                FechaSolicitud = accion.FechaSolicitud.ToString("dd-MM-yyyy"),
                Categoria = accion.Categoria,
                Responsable = responsable.NombreCompleto.ToUpper(),
                Cargo = responsable.Cargo.Descripcion.ToUpper(),
                Proceso = _empresaContext.Procesos.Find(accion.ProcesoID).Descripcion,
                FuenteAccion = _gestorHelper.GetFuenteAccion(accion.FuenteAccion).ToUpper(),
                Descripcion = accion.Descripcion.ToUpper(),
                EficaciaAntes = accion.EficaciaAntes,
                EficaciaDespues = accion.EficaciaDespues,
                FechaCierre = accion.FechaCierre.ToString("dd-MM-yyyy"),
                Efectiva = accion.Efectiva,
                Estado = accion.Estado,
                Planes = planes,
                Seguimientos = sigue
            };

            foreach (var item in model.Planes)
            {
                item.Responsable = _empresaContext.Trabajadores.Find(item.TrabajadorID).NombreCompleto.ToUpper();
                item.FechaInicial.ToString("dd-MM-yyyy");
                item.FechaFinal.ToString("dd-MM-yyyy");
                item.Accion.ToUpper();
            }

            foreach (var item in model.Seguimientos)
            {
                item.Responsable = _empresaContext.Trabajadores.Find(item.TrabajadorID).NombreCompleto.ToUpper();
                item.FechaSeguimiento.ToString("dd-MM-yyyy");
                item.Resultado.ToUpper();
            }
            return model;
        }

        public IEnumerable<MatrizRiesgosVM> ToRiesgoViewModelFul(IEnumerable<Riesgo> riesgo)
        {
            var model = new List<MatrizRiesgosVM>();
            var fuente = "";
            var individuo = "";
            var medio = "";
            var eliminacion = "";
            var sustituto = "";
            var ingenieria = "";
            var admon = "";
            var señales = "";
            var epp = "";
            var rutinaria = "";
            var requisito = "";

            foreach (var item in riesgo)
            {
                fuente = "";
                individuo = "";
                medio = "";
                eliminacion = "";
                sustituto = "";
                ingenieria = "";
                admon = "";
                señales = "";
                epp = "";
                rutinaria = "";
                requisito = "";

                if (item.Rutinaria)
                {
                    rutinaria = "Si";
                }
                else
                {
                    rutinaria = "No";
                }

                if (item.RequisitoLegal)
                {
                    requisito = "Si";
                }
                else
                {
                    requisito = "No";
                }

                foreach (var apl in item.MedidasIntervencion)
                {
                    switch (apl.CategoriaAplicacion)
                    {
                        case CategoriaAplicacion.Fuente:
                            fuente += apl.Nombre + "\n";
                            break;

                        case CategoriaAplicacion.Medio:
                            medio += apl.Nombre + "\n";
                            break;

                        case CategoriaAplicacion.Individuo:
                            individuo += apl.Nombre + "\n";
                            break;
                    }

                    switch (apl.Intervencion)
                    {
                        case JerarquiaControles.Eliminacion:
                            eliminacion += apl.Nombre + "\n";
                            break;

                        case JerarquiaControles.Sustitucion:
                            sustituto += apl.Nombre + "\n";
                            break;

                        case JerarquiaControles.Controles_Ingeniería:
                            ingenieria += apl.Nombre + "\n";
                            break;

                        case JerarquiaControles.Controles_Admon:
                            admon += apl.Nombre + "\n";
                            break;

                        case JerarquiaControles.Señaliza:
                            señales += apl.Nombre + "\n";
                            break;

                        case JerarquiaControles.EPP:
                            epp += apl.Nombre + "\n";
                            break;

                    }
                }
                model.Add(new MatrizRiesgosVM
                {
                    ID = item.ID,
                    Proceso = _empresaContext.Procesos.Find(item.ProcesoID).Descripcion,
                    Zona = _empresaContext.Zonas.Find(item.ZonaID).Descripcion,
                    Actividad = _empresaContext.Actividades.Find(item.ActividadID).Descripcion,
                    Rutinaria = rutinaria,
                    CategoriaPeligro = _empresaContext.CategoriasPeligros.Find(item.CategoriaPeligroID).Descripcion,
                    Peligro = _empresaContext.Peligros.Find(item.PeligroID).Descripcion,
                    EfectosPosibles = _gestorHelper.GetEfectos(item.EfectosPosibles),
                    Fuente = fuente,
                    Medio = medio,
                    Individuo = individuo,
                    NivelDeficiencia = item.NivelDeficiencia,
                    NivelExposicion = item.NivelExposicion,
                    NivelProbabilidad = item.NivelProbabilidad,
                    InterpretaNP = _gestorHelper.GetInterpretaNP(item.NivelProbabilidad),
                    NivelConsecuencia = item.NivelConsecuencia,
                    NivelRiesgo = item.NivelRiesgo,
                    CategoriaRiesgo = _gestorHelper.GetInterpretaNR(item.NivelRiesgo),
                    AceptabilidadNR = _gestorHelper.GetAceptabilidadNR(item.CategoriaRiesgo),
                    SignificadoNR = _gestorHelper.GetSignificadoNR(item.CategoriaRiesgo),
                    NroExpuestos = item.NroExpuestos,
                    PeorConsecuencia = item.PeorConsecuencia,
                    RequisitoLegal = requisito,
                    Eliminacion = eliminacion,
                    Sustitucion = sustituto,
                    Ingenieria = ingenieria,
                    Administracion = admon,
                    Señalizacion = señales,
                    EPP = epp
                });
            }
            return model;
        }

        public MatrizRiesgosVM ToRiesgoVMUnit(Riesgo riesgo)
        {
            var model = new MatrizRiesgosVM
            {
                ID = riesgo.ID,
                Proceso = _empresaContext.Procesos.Find(riesgo.ProcesoID).Descripcion,
                Zona = _empresaContext.Zonas.Find(riesgo.ZonaID).Descripcion,
                Actividad = _empresaContext.Actividades.Find(riesgo.ActividadID).Descripcion,
                CategoriaPeligro = _empresaContext.CategoriasPeligros.Find(riesgo.CategoriaPeligroID).Descripcion,
                Peligro = _empresaContext.Peligros.Find(riesgo.PeligroID).Descripcion,
                EfectosPosibles = _gestorHelper.GetEfectos(riesgo.EfectosPosibles),
                NivelDeficiencia = riesgo.NivelDeficiencia,
                NivelExposicion = riesgo.NivelExposicion,
                NivelProbabilidad = riesgo.NivelProbabilidad,
                InterpretaNP = _gestorHelper.GetInterpretaNP(riesgo.NivelProbabilidad),
                NivelConsecuencia = riesgo.NivelConsecuencia,
                NivelRiesgo = riesgo.NivelRiesgo,
                CategoriaRiesgo = _gestorHelper.GetInterpretaNR(riesgo.NivelRiesgo),
                AceptabilidadNR = _gestorHelper.GetAceptabilidadNR(riesgo.CategoriaRiesgo),
                SignificadoNR = _gestorHelper.GetSignificadoNR(riesgo.CategoriaRiesgo),
                NroExpuestos = riesgo.NroExpuestos,
                PeorConsecuencia = riesgo.PeorConsecuencia
            };
            return model;
        }
        public async Task<Accidentado> ToLesionadoAsync(AccidentadoVM model, bool isNew)
        {
            var result = new Accidentado
            {
                ID = isNew ? 0 : model.ID,
                IncidenteID = model.IncidenteID,
                TrabajadorID = model.TrabajadorID,
            };
            return result;
        }
    }
}