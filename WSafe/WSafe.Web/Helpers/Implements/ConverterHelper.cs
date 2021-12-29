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
        public ConverterHelper(EmpresaContext empresaContext, IComboHelper comboHelper, IGestorHelper gestorHelper)
        {
            _empresaContext = empresaContext;
            _comboHelper = comboHelper;
            _gestorHelper = gestorHelper;
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
                RequisitoLegal = riesgo.RequisitoLegal
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
    }
}