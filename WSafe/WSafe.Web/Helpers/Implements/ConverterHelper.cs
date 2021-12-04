﻿using System;
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
        public Task<Riesgo> ToRiesgoAsync(RiesgoViewModel model, bool isNew)
        {
            throw new NotImplementedException();
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
                NivelesDeficiencia = _comboHelper.GetNivelDeficiencia(),
                NivelesExposicionID = riesgo.NivelExposicion,
                NivelesExposicion = _comboHelper.GetNivelExposicion(),
                NivelProbabilidad = riesgo.NivelProbabilidad,
                InterpretacionNP = _gestorHelper.GetInterpretaNP(riesgo.NivelProbabilidad),
                NivelesConsecuenciaID = riesgo.NivelConsecuencia,
                NivelesConsecuencia = _comboHelper.GetNivelConsecuencias(),
                NivelRiesgo = riesgo.NivelRiesgo,
                InterpretacionNR = _gestorHelper.GetInterpretaNR(riesgo.NivelRiesgo),
                AceptabilidadNR = riesgo.Aceptabilidad,
                SignificadoNR = _gestorHelper.GetSignificadoNR(riesgo.CategoriaRiesgo),
                NroExpuestos = riesgo.NroExpuestos,
                PeorConsecuencia = riesgo.PeorConsecuencia,
                RequisitoLegal = riesgo.RequisitoLegal
            };
        }
    }
}