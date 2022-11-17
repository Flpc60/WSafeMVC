using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSafe.Domain.Data.Entities;
using WSafe.Web.Models;

namespace WSafe.Domain.Helpers.Implements
{
    //Conversión de async / sync
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
            var nr = model.NivelDeficiencia * model.NivelExposicion * model.NivelConsecuencia;
            var cat = _gestorHelper.GetInterpretaNR(nr);
            var acepta = _gestorHelper.GetAceptabilidadNR(cat);
            var aceptaNR = 0;
            switch (acepta)
            {
                case "I":
                    aceptaNR = (int)CategoriasAceptabilidad.NoAceptable;
                    break;

                case "II":
                    aceptaNR = (int)CategoriasAceptabilidad.AceptableControlEspecifico;
                    break;

                case "III":
                    aceptaNR = (int)CategoriasAceptabilidad.Mejorable;
                    break;

                case "IV":
                    aceptaNR = (int)CategoriasAceptabilidad.Aceptable;
                    break;

                default:
                    aceptaNR = (int)CategoriasAceptabilidad.Aceptable;
                    break;
            }

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
                Aceptabilidad = (CategoriasAceptabilidad)aceptaNR,
                NroExpuestos = model.NroExpuestos,
                RequisitoLegal = model.RequisitoLegal,
                IncidenteID = model.IncidenteID,
                DangerCategory = model.DangerCategory
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
                Tareas = _comboHelper.GetComboTareas(),
                DangerCategory = riesgo.DangerCategory
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
                FechaSolicitud = accion.FechaSolicitud.ToString("yyyy-MM-dd"),
                Categoria = accion.Categoria,
                TrabajadorID = accion.TrabajadorID,
                Trabajadores = _comboHelper.GetComboTrabajadores(),
                FuenteAccion = accion.FuenteAccion,
                Origen = _gestorHelper.GetFuenteAccion(accion.FuenteAccion).ToUpper(),
                Descripcion = accion.Descripcion,
                EficaciaAntes = accion.EficaciaAntes,
                EficaciaDespues = accion.EficaciaDespues,
                FechaCierre = accion.FechaCierre.ToString("yyyy-MM-dd"),
                Efectiva = accion.Efectiva,
                ActionCategory = accion.ActionCategory,
                Planes = new List<PlanAction>(),
                Seguimientos = new List<Seguimiento>(),
                FechaSolicitudStr = accion.FechaSolicitud.ToString("yyyy-MM-dd"),
                FechaCierreStr = accion.FechaCierre.ToString("yyyy-MM-dd"),
                ActionState = _gestorHelper.GetActionCategory((int)accion.ActionCategory)
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
                ActionCategory = model.ActionCategory
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
                FechaReporte = Convert.ToDateTime(model.FechaReporte),
                FechaIncidente = Convert.ToDateTime(model.FechaIncidente),
                CategoriasIncidente = model.CategoriasIncidente,
                IncapacidadMedica = model.IncapacidadMedica,
                DiasIncapacidad = model.DiasIncapacidad,
                NaturalezaLesion = model.NaturalezaLesion.ToUpper(),
                PartesAfectadas = model.PartesAfectadas.ToUpper(),
                TipoIncidente = model.TipoIncidente.ToUpper(),
                AgenteLesion = model.AgenteLesion.ToUpper(),
                ActosInseguros = model.ActosInseguros.ToUpper(),
                CondicionesInsegura = model.CondicionesInsegura.ToUpper(),
                TipoDaño = model.TipoDaño.ToUpper(),
                Afectacion = model.Afectacion.ToUpper(),
                DañosOcasionados = model.DañosOcasionados.ToUpper(),
                TipoVehiculo = model.TipoVehiculo.ToUpper(),
                MarcaVehiculo = model.MarcaVehiculo.ToUpper(),
                ModeloVehiculo = model.ModeloVehiculo.ToUpper(),
                KilometrajeVehiculo = model.KilometrajeVehiculo,
                CostosEstimados = model.CostosEstimados,
                DescripcionIncidente = model.DescripcionIncidente.ToUpper(),
                EvitarIncidente = model.EvitarIncidente.ToUpper(),
                AccionesInmediatas = model.AccionesInmediatas.ToUpper(),
                ComentariosAdicionales = model.ComentariosAdicionales.ToUpper(),
                AtencionBrindada = model.AtencionBrindada.ToUpper(),
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
                FechaReporte = incidente.FechaReporte.ToString("yyyy-MM-dd"),
                FechaIncidente = incidente.FechaIncidente.ToString("yyyy-MM-dd"),
                CategoriasIncidente = incidente.CategoriasIncidente,
                IncapacidadMedica = incidente.IncapacidadMedica,
                DiasIncapacidad = incidente.DiasIncapacidad,
                TrabajadorID = incidente.TrabajadorID,
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
                Lesionados = new List<AccidentadoVM>(),
                FechaIncidentStr = incidente.FechaIncidente.ToString("yyyy-MM-dd"),
                FechaReportStr = incidente.FechaReporte.ToString("yyyy-MM-dd")
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
                    TextRequisito = requisito,
                    Aceptability = _gestorHelper.GetAceptabilidadNR(item.CategoriaRiesgo)
                });
            }

            return model;
        }
        public AccidentadoVM ToLesionadoViewModel(Trabajador lesionado)
        {
            var cargo = _empresaContext.Cargos.Find(lesionado.CargoID);

            var modelo = new AccidentadoVM()
            {
                TrabajadorID = lesionado.ID,
                Documento = lesionado.Documento,
                NombreCompleto = lesionado.NombreCompleto,
                FechaNacimiento = lesionado.FechaNacimiento,
                Genero = _gestorHelper.GetGenero(lesionado.Genero),
                EstadoCivil = _gestorHelper.GetEstadoCivil(lesionado.EstadoCivil),
                TipoVinculacion = _gestorHelper.GetTipoVinculacion(lesionado.TipoVinculacion),
                Cargo = cargo.Descripcion
            };
            return modelo;
        }
        public IEnumerable<AccidentadoVM> ToListLesionadosVM(IEnumerable<Accidentado> lesionados)
        {
            var modelo = new List<AccidentadoVM>();
            foreach (var item in lesionados)
            {
                var lesionado = _empresaContext.Trabajadores.Find(item.TrabajadorID);
                var cargo = _empresaContext.Cargos.Find(lesionado.CargoID);
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
                    Cargo = cargo.Descripcion
                });
            }
            return modelo;
        }
        public AplicacionVM ToAplicationVM(Aplicacion aplicacion)
        {
            var modelo = new AplicacionVM
            {
                ID = aplicacion.ID,
                RiesgoID = aplicacion.RiesgoID,
                Nombre = aplicacion.Nombre.ToUpper(),
                CategoriaAplicacion = aplicacion.CategoriaAplicacion,
                Intervencion = aplicacion.Intervencion,
                Beneficios = aplicacion.Beneficios.ToUpper(),
                Presupuesto = aplicacion.Presupuesto,
                TrabajadorID = aplicacion.TrabajadorID,
                FechaInicial = aplicacion.FechaInicial.ToString("yyyy-MM-dd"),
                FechaFinal = aplicacion.FechaFinal.ToString("yyyy-MM-dd"),
                Observaciones = aplicacion.Observaciones.ToUpper(),
                Responsable = _empresaContext.Trabajadores.Find(aplicacion.TrabajadorID).NombreCompleto.ToUpper(),
                TextCategoria = _gestorHelper.GetCategoriaAplicacion(aplicacion.CategoriaAplicacion),
                TextIntervencion = _gestorHelper.GetJerarquiaControl(aplicacion.Intervencion),
                TextFechaInicial = aplicacion.FechaInicial.ToString("yyyy-MM-dd"),
                TextFechaFinal = aplicacion.FechaFinal.ToString("yyyy-MM-dd")
            };

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
                    Beneficios = item.Beneficios.ToUpper(),
                    Presupuesto = item.Presupuesto,
                    TrabajadorID = item.TrabajadorID,
                    Responsable = _empresaContext.Trabajadores.Find(item.TrabajadorID).NombreCompleto.ToUpper(),
                    FechaInicial = item.FechaInicial.ToString("yyyy-MM-dd"),
                    FechaFinal = item.FechaFinal.ToString("yyyy-MM-dd"),
                    TextFechaInicial = item.FechaInicial.ToString("yyyy-MM-dd"),
                    TextFechaFinal = item.FechaFinal.ToString("yyyy-MM-dd"),
                    Observaciones = item.Observaciones.ToUpper(),
                    TextCategoria = _gestorHelper.GetCategoriaAplicacion(item.CategoriaAplicacion),
                    TextIntervencion = _gestorHelper.GetJerarquiaControl(item.Intervencion),
                    NivelDeficiencia = item.NivelDeficiencia,
                    NivelExposicion = item.NivelExposicion,
                    NivelConsecuencia = item.NivelConsecuencia,
                    Aceptabilidad = item.Aceptabilidad
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
                FechaInicial = Convert.ToDateTime(model.FechaInicial),
                FechaFinal = Convert.ToDateTime(model.FechaFinal),
                Observaciones = model.Observaciones,
                NivelDeficiencia = model.NivelDeficiencia,
                NivelExposicion = model.NivelExposicion,
                NivelConsecuencia = model.NivelConsecuencia,
                Aceptabilidad = model.Aceptabilidad
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
                Responsable = _empresaContext.Trabajadores.Find(plan.TrabajadorID).NombreCompleto.ToUpper(),
                ActionCategory = plan.ActionCategory
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
                Responsable = plan.Responsable,
                ActionCategory = plan.ActionCategory
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
                    FechaInicial = item.FechaInicial.ToString("yyyy-MM-dd"),
                    FechaFinal = item.FechaFinal.ToString("yyyy-MM-dd"),
                    Causa = item.Causa,
                    Categoria = _gestorHelper.GetCausaAccion(item.Causa).ToUpper(),
                    Accion = item.Accion.ToUpper(),
                    Prioritaria = item.Prioritaria,
                    Costos = item.Costos,
                    TrabajadorID = item.TrabajadorID,
                    Responsable = _empresaContext.Trabajadores.Find(item.TrabajadorID).NombreCompleto.ToUpper(),
                    ActionCategory = item.ActionCategory
                });
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
                    FechaSolicitud = item.FechaSolicitud.ToString("yyyy-MM-dd"),
                    Categoria = item.Categoria,
                    TrabajadorID = item.TrabajadorID,
                    Trabajadores = _comboHelper.GetComboTrabajadores(),
                    FuenteAccion = item.FuenteAccion,
                    Origen = _gestorHelper.GetFuenteAccion(item.FuenteAccion).ToUpper(),
                    Descripcion = item.Descripcion.ToUpper(),
                    EficaciaAntes = item.EficaciaAntes,
                    EficaciaDespues = item.EficaciaDespues,
                    FechaCierre = item.FechaCierre.ToString("yyyy-MM-dd"),
                    Efectiva = item.Efectiva,
                    ActionCategory = item.ActionCategory,
                    ActionState = _gestorHelper.GetActionCategory((int)item.ActionCategory)
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
                    FechaSeguimiento = item.FechaSeguimiento.ToString("yyyy-MM-dd"),
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
            var responsable = _empresaContext.Trabajadores.FirstOrDefault(t => t.ID == accion.TrabajadorID);
            var cargo = _empresaContext.Cargos.FirstOrDefault(t => t.ID == responsable.CargoID);

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
                Cargo = cargo.Descripcion.ToUpper(),
                Proceso = _empresaContext.Procesos.Find(accion.ProcesoID).Descripcion,
                FuenteAccion = _gestorHelper.GetFuenteAccion(accion.FuenteAccion).ToUpper(),
                Descripcion = accion.Descripcion.ToUpper(),
                EficaciaAntes = accion.EficaciaAntes,
                EficaciaDespues = accion.EficaciaDespues,
                FechaCierre = accion.FechaCierre.ToString("dd-MM-yyyy"),
                Efectiva = accion.Efectiva,
                ActionCategory = accion.ActionCategory,
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
        public DetailsIncidentVM ToIncidentVMFull(Incidente incidente, int id)
        {
            var lesionados = _empresaContext.Accidentados.Where(a => a.IncidenteID == incidente.ID).ToList();
            var responsable = _empresaContext.Trabajadores.FirstOrDefault(t => t.ID == incidente.TrabajadorID);
            var zona = _empresaContext.Zonas.FirstOrDefault(z => z.ID == incidente.ZonaID).Descripcion;
            var proceso = _empresaContext.Procesos.FirstOrDefault(p => p.ID == incidente.ProcesoID).Descripcion;
            var actividad = _empresaContext.Actividades.FirstOrDefault(a => a.ID == incidente.ActividadID).Descripcion;
            var tarea = _empresaContext.Tareas.FirstOrDefault(t => t.ID == incidente.TareaID).Descripcion;
            var lugar = zona.Replace(" ", String.Empty).ToUpper() + " - " + proceso.Replace(" ", String.Empty).ToUpper();

            var document = _empresaContext.Documents.FirstOrDefault(d => d.ID == id);
            var model = new DetailsIncidentVM
            {
                ID = incidente.ID,
                Formato = document.Formato,
                Estandar = document.Estandar,
                Titulo = document.Titulo,
                Version = document.Version,
                Lugar = lugar,
                FechaReporte = incidente.FechaReporte.ToString("dd-MM-yyyy"),
                FechaIncidente = incidente.FechaIncidente.ToString("dd-MM-yyyy"),
                CategoriasIncidente = incidente.CategoriasIncidente,
                IncapacidadMedica = incidente.IncapacidadMedica,
                DiasIncapacidad = incidente.DiasIncapacidad,
                Informante = responsable.NombreCompleto,
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
                Lesionados = new List<AccidentadoVM>(),
                Firma = responsable.Firma
            };

            var modelo = new List<AccidentadoVM>();
            foreach (var item in lesionados)
            {
                var lesionado = _empresaContext.Trabajadores.Find(item.TrabajadorID);
                var cargo = _empresaContext.Cargos.Find(lesionado.CargoID).Descripcion;
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
                    Cargo = cargo
                });
            }
            model.Lesionados = modelo;

            return model;
        }

        public IEnumerable<UserViewModel> ToUsersVM(IEnumerable<User> userList)
        {
            var modelo = new List<UserViewModel>();
            var role = " ";
            foreach (var item in userList)
            {
                if (item.RoleID != 0)
                {
                    role = _gestorHelper.GetRole(item.RoleID);
                }
                else
                {
                    role = "No tiene permisos";
                }

                modelo.Add(new UserViewModel
                {
                    ID = item.ID,
                    Name = item.Name,
                    Email = item.Email,
                    Role = role
                });
            }
            return modelo;
        }

        public IEnumerable<AuthorizationVM> ToRolOperationVM(IEnumerable<RoleOperation> roleOperation)
        {
            var modelo = new List<AuthorizationVM>();
            var role = " ";
            foreach (var item in roleOperation)
            {
                if (item.RoleID != 0)
                {
                    role = _gestorHelper.GetRole(item.RoleID);
                }
                else
                {
                    role = "No tiene permisos";
                }
                modelo.Add(new AuthorizationVM
                {
                    ID = item.ID,
                    RoleID = item.RoleID,
                    Role = role,
                    ComponentID = item.Component,
                    Component = _gestorHelper.GetComponent(item.Component),
                    OperationID = item.Operation,
                    Operation = _gestorHelper.GetOperation(item.Operation),
                });
            }
            return modelo;
        }

        public RoleUserVM ToAuthorizationVM(User user)
        {
            var modelo = new RoleUserVM()
            {
                ID = user.ID,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                RoleID = user.RoleID,
                Roles = _comboHelper.GetAllRoles()
            };
            return modelo;
        }
        public async Task<User> ToUserAsync(RoleUserVM user)
        {
            var result = new User
            {
                ID = user.ID,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                RoleID = user.RoleID
            };
            return result;
        }
        public IEnumerable<MovimientVM> ToListMovimientos(IEnumerable<Movimient> movimientos)
        {
            var model = new List<MovimientVM>();
            foreach (var item in movimientos)
            {
                model.Add(new MovimientVM
                {
                    ID = item.ID,
                    OrganizationID = item.OrganizationID,
                    Ciclo = item.Ciclo,
                    Item = item.Item,
                    Name = _empresaContext.Normas.Find(item.NormaID).Name.ToUpper(),
                    NormaID = item.NormaID,
                    Descripcion = item.Descripcion,
                    Document = item.Document,
                    Year = item.Year,
                    Type = item.Type
                }); ;
            }
            return model;
        }
    }
}
