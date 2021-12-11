using System.Collections.Generic;
using System.Threading.Tasks;
using WSafe.Domain.Data;
using WSafe.Domain.Data.Entities;
using WSafe.Domain.Models;

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
                ID = isNew ? 1 : model.ID,
                Zona = await _empresaContext.Zonas.FindAsync(model.ZonaID),
                Proceso = await _empresaContext.Procesos.FindAsync(model.ProcesoID),
                Actividad = await _empresaContext.Actividades.FindAsync(model.ActividadID),
                Tarea = await _empresaContext.Tareas.FindAsync(model.TareaID),
                Rutinaria = model.Rutinaria,
                Peligro = await _empresaContext.Peligros.FindAsync(model.PeligroID),
                EfectosPosibles = model.EfectosPosibles,
                NivelDeficiencia = model.NivelesDeficienciaID,
                NivelExposicion = model.NivelesExposicionID,
                NivelConsecuencia = model.NivelesConsecuenciaID,
                CategoriaRiesgo = _gestorHelper.GetInterpretaNR(model.NivelRiesgo),
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
            return new RiesgoViewModel
            {
                ID = riesgo.ID,
                ProcesoID = riesgo.Proceso.ID,
                Procesos = _comboHelper.GetComboProcesos(),
                ZonaID = riesgo.Zona.ID,
                Zonas = _comboHelper.GetComboZonas(),
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
                NivelesDeficienciaID = riesgo.NivelDeficiencia,
                NivelesExposicionID = riesgo.NivelExposicion,
                NivelProbabilidad = riesgo.NivelProbabilidad,
                InterpretacionNP = _gestorHelper.GetInterpretaNP(riesgo.NivelProbabilidad),
                NivelesConsecuenciaID = riesgo.NivelConsecuencia,
                NivelRiesgo = riesgo.NivelRiesgo,
                InterpretacionNR = _gestorHelper.GetInterpretaNR(riesgo.NivelRiesgo),
                AceptabilidadNR = riesgo.Aceptabilidad,
                SignificadoNR = _gestorHelper.GetSignificadoNR(riesgo.CategoriaRiesgo),
                NroExpuestos = riesgo.NroExpuestos,
                RequisitoLegal = riesgo.RequisitoLegal
            };
        }
    }
}