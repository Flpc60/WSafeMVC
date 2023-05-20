using System.Collections.Generic;
using WSafe.Domain.Data.Entities;

namespace WSafe.Domain.Helpers
{
    public interface IGestorHelper
    {
        List<Control> GetControlesFuente(int id);
        List<Control> GetControlesMedio(int id);
        List<Control> GetControlesPersona(int id);
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
    }
}
