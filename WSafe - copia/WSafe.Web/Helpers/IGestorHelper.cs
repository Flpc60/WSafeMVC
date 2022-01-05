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
    }
}