using System;
using WSafe.Domain.Data.Entities;
using WSafe.Domain.Data.Entities.Ppre;

namespace WSafe.Domain.Helpers
{
    public interface IGestorHelper
    {
        string GetInterpretaNP(int probabilidad);
        string GetInterpretaNR(int nivelRiesgo);
        string GetAceptabilidadNR(string nivelRiesgo);
        string GetSignificadoNR(string nivelRiesgo);
        NivelesDeficiencia GetNivelDeficiencia(int deficiencia);
        NivelesExposicion GetNivelExposicion(int exposicion);
        NivelesConsecuencia GetNivelConsecuencia(int consecuencia);
        string GetGenero(CategoriasGenero genero);
        string GetEstadoCivil(EstadosCivil estado);
        string GetTipoVinculacion(TiposVinculacion tipo);
        string GetCausaAccion(CategoriasCausa categoria);
        string GetFuenteAccion(FuentesAccion fuentes);
        string GetEfectos(EfectosPosibles efecto);
        string GetJerarquiaControl(JerarquiaControles categoria);
        string GetCategoriaAplicacion(CategoriaAplicacion categoria);
        string GetPeriodo(int[] periodo);
        string GetRole(int role);
        string GetComponent(int component);
        string GetOperation(int operation);
        string GetActionCategory(int estado);
        string GetCiclo(string ciclo);
        string GetStandard(string standar);
        string GetRecurso(RecursosCategory recurso);
        void CreateFolder(string path);
        string GetEscolaridad(NivelesEscolaridad escolaridad);
        string GetWorkArea(WorkAreas area);
        string GetTipoJornada(TiposJornada jornada);
        int CalculateAge(DateTime dateOfBirth);
        string GetBloodType(TiposSangre blood);
        string GetStratum(EstratoCategories stratum);
        string GetTenenciaVivienda(TenenciasVivienda tenencia);
        string GetActionType(int categoria);
        string GetContingencia(Contingencias contingencia);
        string GetTipoReintegro(TiposReintegro type);
        string GetEmission(Emissions emission);
        string GetRecomendationType(RecomendationTypes type);
        string GetAuditChapter(AuditChapters chapter);
        string GetRiskClass(int riskClass);
        string GetStateActivity(StatesActivity state);
        string GetStateCronogram(StatesCronogram state);
        string GetExaminationType(ExaminationTypes type);
        string GetMedicalRecomendation(MedicalRecomendations medical);
        string GetVulnerabilityType(int id);
        string GetAmenazaCategory(CategoryAmenazas category);
        string GetInterpretation(double result);
        string GetVulnerabilityInterpretation(double result);
        string GetOrigenAmenaza(OrigenAmenazas origen);
        string GetCalification(CategoryCalifications calification);
        string GetRiskLevelInterpretation(string calification, string result1, string result2, string result3);
    }
}
