﻿using System;
using System.Collections.Generic;
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
        public ConverterHelper
            (EmpresaContext empresaContext,
            IComboHelper comboHelper,
            IGestorHelper gestorHelper,
            IChartHelper chartHelper)
        {
            _empresaContext = empresaContext;
            _comboHelper = comboHelper;
            _gestorHelper = gestorHelper;
            _chartHelper = chartHelper;
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
                MedidasIntervencion = isNew ? new List<Aplicacion>() : null,
                Acciones = isNew ? new List<Accion>() : null,
                IncidenteID = model.IncidenteID
            };
            return result;
        }
        public RiesgoViewModel ToRiesgoViewModel(Riesgo riesgo)
        {
            var model = new RiesgoViewModel
            {
                ID = riesgo.ID,
                ZonaID = riesgo.Zona.ID,
                Zonas = _comboHelper.GetComboZonas(),
                ProcesoID = riesgo.Proceso.ID,
                Procesos = _comboHelper.GetComboProcesos(),
                ActividadID = riesgo.Actividad.ID,
                Actividades = _comboHelper.GetComboActividades(),
                TareaID = riesgo.Tarea.ID,
                Tareas = _comboHelper.GetComboTareas(),
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
                IncidenteID = riesgo.IncidenteID
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
        public AccionViewModel ToAccionViewModelNew(int id)
        {
            var model = new AccionViewModel
            {
                RiesgoID = id,
                Trabajadores = _comboHelper.GetComboTrabajadores()
            };

            return model;
        }
        public AccionViewModel ToAccionViewModel(Accion accion)
        {
            var model = new AccionViewModel
            {
                RiesgoID = accion.ID,
                Categoria = accion.Categoria,
                FechaSolicitud = accion.FechaSolicitud,
                TrabajadorID = accion.Trabajador.ID,
                Trabajadores = _comboHelper.GetComboTrabajadores(),
                FuenteAccion = accion.FuenteAccion,
                Descripcion = accion.Descripcion,
                //CausasAccion = accion.CausasAccion,
                //FechaInicial = accion.FechaInicial,
                //FechaFinal = accion.FechaFinal,
                //Plan = accion.PlanAcion,
                //Seguimiento = accion.Seguimiento,
                //FechaSeguimiento = accion.FechaSeguimiento,
                //FechaCierre = accion.FechaCierre,
                //Efectividad = accion.Efectividad
            };

            return model;
        }
        public async Task<Accion> ToAccionAsync(AccionViewModel model, bool isNew)
        {
            var result = new Accion
            {
                ID = isNew ? 0 : model.ID,
                RiesgoID = model.RiesgoID,
                Categoria = model.Categoria,
                FechaSolicitud = model.FechaSolicitud,
                Trabajador = await _empresaContext.Trabajadores.FindAsync(model.TrabajadorID),
                FuenteAccion = model.FuenteAccion,
                Descripcion = model.Descripcion,
                //CausasAccion = model.CausasAccion,
                //FechaInicial = model.FechaInicial,
                //FechaFinal = model.FechaFinal,
                //Plan = model.Plan,
                //Seguimiento = model.Seguimiento,
                //FechaSeguimiento = model.FechaSeguimiento,
                //FechaCierre = model.FechaCierre,
                //Efectividad = model.Efectividad
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
                TrabajadorID = model.TrabajadorID,
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
                Probabilidad = model.Probabilidad
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
                Trabajadores = _comboHelper.GetComboTrabajadores()
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
                Probabilidad = incidente.Probabilidad
            };

            return model;
        }

        public IndicadorViewModel ToIndicadorViewModel(Indicador indicador)
        {
            var datos = _chartHelper.GetFrecuenciaAccidentes(Convert.ToDateTime("2021/06/01"), Convert.ToDateTime("2021/12/31"));
            //_chartHelper.DrawImagen(indicador.TipoChart, "Indicador de frecuencia", datos);
            var model = new IndicadorViewModel
            {
                ID = indicador.ID,
                FechaInicial = Convert.ToDateTime("2021/06/01"),
                FechaFinal = Convert.ToDateTime("2021/12/31"),
                Indicadores = _comboHelper.GetComboIndicadores(),
                Nombre = indicador.Nombre,
                Definicion = indicador.Definicion,
                Numerador = indicador.Numerador,
                Denominador = indicador.Denominador,
                Formula = indicador.Formula,
                Interpretacion = indicador.Interpretacion,
                Periodicidad = indicador.Periodicidad,
                Datos = datos,
                Imagen = ".../Images/Indicador.jpg"
            };
            return model;
        }
    }
}