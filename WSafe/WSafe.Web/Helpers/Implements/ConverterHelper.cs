﻿using System;
using System.Collections.Generic;
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
                Zona = await _empresaContext.Zonas.FindAsync(model.ZonaID),
                Proceso = await _empresaContext.Procesos.FindAsync(model.ProcesoID),
                Actividad = await _empresaContext.Actividades.FindAsync(model.ActividadID),
                Tarea = await _empresaContext.Tareas.FindAsync(model.TareaID),
                Rutinaria = model.Rutinaria,
                Peligro = await _empresaContext.Peligros.FindAsync(model.PeligroID),
                EfectosPosibles = model.EfectosPosibles,
                NivelDeficiencia = model.NivelDeficiencia,
                NivelExposicion = model.NivelExposicion,
                NivelConsecuencia = model.NivelConsecuencia,
                Aceptabilidad = model.AceptabilidadNR,
                NroExpuestos = model.NroExpuestos,
                RequisitoLegal = model.RequisitoLegal,
                IncidenteID = model.IncidenteID
            };
            foreach (var item in model.Intervenciones)
            {
                result.MedidasIntervencion.Add(new Aplicacion
                {
                    RiesgoID = item.ID,
                    Nombre = item.Nombre,
                    CategoriaAplicacion = item.CategoriaAplicacion,
                    Finalidad = item.Finalidad,
                    Intervencion = item.Intervencion,
                    Beneficios = item.Beneficios,
                    Presupuesto = item.Presupuesto,
                    //Trabajador = item.Trabajadores,
                    Observaciones = item.Observaciones
                });
            }

            return result;
        }
        public RiesgoViewModel ToRiesgoViewModel(Riesgo riesgo)
        {
            var model = new RiesgoViewModel
            {
                ID = riesgo.ID,
                ZonaID = riesgo.Zona.ID,
                //Zonas = _comboHelper.GetComboZonas(),
                ProcesoID = riesgo.Proceso.ID,
                //Procesos = _comboHelper.GetComboProcesos(),
                ActividadID = riesgo.Actividad.ID,
                //Actividades = _comboHelper.GetComboActividades(),
                TareaID = riesgo.Tarea.ID,
                //Tareas = _comboHelper.GetComboTareas(),
                Rutinaria = riesgo.Rutinaria,
                CategoriaPeligroID = riesgo.Peligro.CategoriaPeligroID,
                CategoriasPeligros = _comboHelper.GetComboCategoriaPeligros(),
                PeligroID = riesgo.Peligro.ID,
                Peligros = _comboHelper.GetComboPeligros(riesgo.Peligro.CategoriaPeligroID),
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
                Intervenciones = new List<AplicacionVM>()
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
                Intervenciones = new List<AplicacionVM>()
            };

            model.Intervenciones.Add(
                new AplicacionVM()
                {
                    Trabajadores = _comboHelper.GetComboTrabajadores()
                }
                );

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
                Planes = new List<PlanAccion>(),
                Seguimientos = new List<SeguimientoAccion>()
            };

            model.Planes.Add(new PlanAccion());
            model.Seguimientos.Add( new SeguimientoAccion());
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
                Planes = new List<PlanAccion>(),
                Seguimientos = new List<SeguimientoAccion>(),
                FechaSolicitudStr = accion.FechaSolicitud.ToString("yyyy-MM-dd"),
                FechaCierreStr = accion.FechaCierre.ToString("yyyy-MM-dd")
            };
            model.Planes.Add(new PlanAccion() { AccionID = accion.ID });
            model.Seguimientos.Add(new SeguimientoAccion() { AccionID = accion.ID });
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
                CategoriaIncidente = model.CategoriasIncidente,
                IncapacidadMedica = model.IncapacidadMedica,
                DiasIncapacidad = model.DiasIncapacidad,
                Informante = model.Informante,
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
                EquipoInvestigador = model.EquiposInvestigador,
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
                //Lesionados = model.Lesionados
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
                CategoriasIncidente = incidente.CategoriaIncidente,
                IncapacidadMedica = incidente.IncapacidadMedica,
                DiasIncapacidad = incidente.DiasIncapacidad,
                Informante = incidente.Informante,
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
                EquiposInvestigador = incidente.EquipoInvestigador,
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
            var datos = _chartHelper.GetFrecuenciaAccidentes(Convert.ToDateTime("2021/06/01"), Convert.ToDateTime("2021/12/31"));
            //_chartHelper.DrawImagen(indicador.TipoChart, indicador.Nombre, datos);
            var model = new IndicadorViewModel
            {
                ID = indicador.ID,
                FechaInicial = Convert.ToDateTime("2021/06/01"),
                FechaFinal = Convert.ToDateTime("2021/12/31"),
                Nombre = indicador.Nombre,
                Definicion = indicador.Definicion,
                Numerador = indicador.Numerador,
                Denominador = indicador.Denominador,
                Formula = indicador.Formula,
                Interpretacion = indicador.Interpretacion,
                Periodicidad = indicador.Periodicidad,
                Datos = datos,
                Imagen = "~/Images/chart05.jpg"
            };
            return model;
        }
        public IndicadorViewModel ToIndicadorViewModelNew(Indicador indicador, DateTime fechaInicial, DateTime fechaFinal)
        {
            Random random = new Random();
            var filename = "chart" + random.Next(1, 100) + ".jpg";
            var filePathName = "~/Images/" + filename;

            var datos = _chartHelper.GetFrecuenciaAccidentes(fechaInicial, fechaFinal);
            switch (indicador.ID)
            {
                case 1:
                    datos = _chartHelper.GetFrecuenciaAccidentes(fechaInicial, fechaFinal);
                    break;

                case 2:
                    datos = _chartHelper.GetSeveridadAccidentalidad(fechaInicial, fechaFinal);
                    break;

                case 3:
                    datos = _chartHelper.GetAccidentesTrabajoMortales(fechaInicial, fechaFinal);
                    break;

                case 4:
                    datos = _chartHelper.GetAccidentesTrabajoMortales(fechaInicial, fechaFinal);
                    break;

                case 5:
                    datos = _chartHelper.GetAccidentesTrabajoMortales(fechaInicial, fechaFinal);
                    break;

                case 6:
                    datos = _chartHelper.GetAusentismoCausaMedica(fechaInicial, fechaFinal);
                    break;

                case 7:
                    datos = _chartHelper.GetFatorRiesgoOcupacional();
                    break;
            }

            _chartHelper.DrawImagen(filePathName, indicador.TipoChart, indicador.Nombre, datos);
            var model = new IndicadorViewModel
            {
                ID = indicador.ID,
                FechaInicial = fechaInicial,
                FechaFinal = fechaFinal,
                Nombre = indicador.Nombre,
                Definicion = indicador.Definicion,
                Numerador = indicador.Numerador,
                Denominador = indicador.Denominador,
                Formula = indicador.Formula,
                Interpretacion = indicador.Interpretacion,
                Periodicidad = indicador.Periodicidad,
                Datos = datos,
                Imagen = filePathName
            };
            return model;
        }

        public IEnumerable<ListaRiesgosVM> ToRiesgoViewModelList(IEnumerable<Riesgo> riesgo)
        {
            var model = new List<ListaRiesgosVM>();
            foreach (var item in riesgo)
            {
                model.Add(new ListaRiesgosVM
                {
                    ID = item.ID,
                    Zona = _empresaContext.Zonas.Find(item.Zona.ID).Descripcion,
                    Proceso = _empresaContext.Procesos.Find(item.Proceso.ID).Descripcion,
                    Actividad = _empresaContext.Actividades.Find(item.Actividad.ID).Descripcion,
                    Tarea = _empresaContext.Tareas.Find(item.Tarea.ID).Descripcion,
                    Rutinaria = item.Rutinaria,
                    Clasificacion = _empresaContext.CategoriasPeligros.Find(item.Peligro.CategoriaPeligroID).Descripcion,
                    Peligro = _empresaContext.Peligros.Find(item.Peligro.ID).Descripcion,
                    EfectosPosibles = item.EfectosPosibles,
                    NivelDeficiencia = item.NivelDeficiencia,
                    NivelExposicion = item.NivelExposicion,
                    NivelConsecuencia = item.NivelConsecuencia,
                    Aceptabilidad = item.Aceptabilidad,
                    NroExpuestos = item.NroExpuestos,
                    RequisitoLegal = item.RequisitoLegal
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

        public IEnumerable<AplicacionVM> ToIntervencionesViewModel(IEnumerable<Aplicacion> listaAplicacion)
        {
            var modelo = new List<AplicacionVM>();
            foreach (var item in listaAplicacion)
            {
                modelo.Add(new AplicacionVM
                {
                    RiesgoID = item.ID,
                    Nombre = item.Nombre,
                    CategoriaAplicacion = item.CategoriaAplicacion,
                    Finalidad = item.Finalidad,
                    Intervencion = item.Intervencion,
                    Beneficios = item.Beneficios,
                    Presupuesto = item.Presupuesto,
                    Trabajadores = _comboHelper.GetComboTrabajadores(),
                    FechaInicial = item.FechaInicial,
                    FechaFinal = item.FechaFinal,
                    Observaciones = item.Observaciones
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
                Finalidad = model.Finalidad,
                Intervencion = model.Intervencion,
                Beneficios = model.Beneficios,
                Presupuesto = model.Presupuesto,
                Trabajador = await _empresaContext.Trabajadores.FindAsync(model.TrabajadorID),
                FechaInicial = model.FechaInicial,
                FechaFinal = model.FechaFinal,
                Observaciones = model.Observaciones
            };

            return result;
        }
        public PlanAccionVM ToPlanAccionVM(PlanAccion plan)
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
        public async Task<PlanAccion> ToPlanAccionAsync(PlanAccion plan)
        {
            
            var result = new PlanAccion
            {
                ID = plan.ID,
                AccionID = plan.AccionID,
                FechaInicial = plan.FechaInicial,
                FechaFinal = plan.FechaFinal,
                Causa = plan.Causa,
                Accion = plan.Accion,
                TrabajadorID = plan.TrabajadorID,
                Prioritaria = plan.Prioritaria,
                Costos = plan.Costos
            };
            return result;
        }
        public SeguimientoAccionVM ToSeguimientoAccionVM(SeguimientoAccion seguimientoAccion)
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

        public SeguimientoAccionVM ToSeguimientoAccionVMNew()
        {
            throw new NotImplementedException();
        }

        public async Task<SeguimientoAccion> ToSeguimientoAccionAsync(SeguimientoAccion model)
        {
            var result = new SeguimientoAccion
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

        public IEnumerable<PlanAccionVM> ToPlanAccionVMList(IEnumerable<PlanAccion> plan)
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
        public IEnumerable<SeguimientoAccionVM> ToSeguimientoAccionVMList(IEnumerable<SeguimientoAccion> accion)
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
    }
}