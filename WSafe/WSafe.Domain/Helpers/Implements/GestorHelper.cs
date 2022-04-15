using System;
using System.Collections.Generic;
using System.Linq;
using WSafe.Domain.Data.Entities;
using WSafe.Web.Models;

namespace WSafe.Domain.Helpers.Implements
{
    public class GestorHelper : IGestorHelper
    {
        private readonly EmpresaContext _empresaContext;
        public GestorHelper(EmpresaContext empresaContext)
        {
            _empresaContext = empresaContext;
        }

        public string GetInterpretaNP(int probabilidad)
        {
            switch (probabilidad)
            {
                case int p when (p >= 24):
                    return "Muy alto (MA)";

                case int p when (p >= 10 && p < 24):
                    return "Alto (A)";

                case int p when (p >= 8 && p < 10):
                    return "Mdio (M)";

                default:
                    return "Bajo (B)";
            }
        }
        public string GetInterpretaNR(int nivelRiesgo)
        {
            switch (nivelRiesgo)
            {
                case int nr when (nr >= 600):
                    return "I";

                case int nr when (nr >= 150 && nr < 600):
                    return "II";

                case int nr when (nr >= 40 && nr < 150):
                    return "III";

                default:
                    return "IV";
            }
        }
        public string GetAceptabilidadNR(string nivelRiesgo)
        {
            switch (nivelRiesgo)
            {
                case "I":
                    return "No Aceptable";

                case "II":
                    return "No Aceptable o Aceptable con control Específico";

                case "III":
                    return "Mejorable";

                default:
                    return "Aceptable";
            }
        }
        public string GetSignificadoNR(string nivelRiesgo)
        {
            switch (nivelRiesgo)
            {
                case "I":
                    return "red";

                case "II":
                    return "yellow";

                case "III":
                    return "orange";

                default:
                    return "green";
            }
        }
        public List<Control> GetControlesFuente(int idRiesgo)
        {
            var list = _empresaContext.Controles
                .Where(c => c.RiesgoID == idRiesgo && c.CategoriaAplicacion == CategoriaAplicacion.Fuente)
                .Select(c => new Control
                {
                    Nombre = c.Nombre,
                    Beneficios = c.Beneficios,
                    Efectividad = c.Efectividad
                }).ToList();
            return list;
        }
        public List<Control> GetControlesMedio(int idRiesgo)
        {
            var list = _empresaContext.Controles
                .Where(c => c.RiesgoID == idRiesgo && c.CategoriaAplicacion == CategoriaAplicacion.Medio)
                .Select(c => new Control
                {
                    Nombre = c.Nombre,
                    Beneficios = c.Beneficios,
                    Efectividad = c.Efectividad
                }).ToList();
            return list;
        }
        public List<Control> GetControlesPersona(int idRiesgo)
        {
            var list = _empresaContext.Controles
                .Where(c => c.RiesgoID == idRiesgo && c.CategoriaAplicacion == CategoriaAplicacion.Individuo)
                .Select(c => new Control
                {
                    Nombre = c.Nombre,
                    Beneficios = c.Beneficios,
                    Efectividad = c.Efectividad
                }).ToList();
            return list;
        }

        public NivelesDeficiencia GetNivelDeficiencia(int deficiencia)
        {
            switch (deficiencia)
            {
                case 10:
                    return NivelesDeficiencia.Muy_alto;

                case 6:
                    return NivelesDeficiencia.Alto;

                case 2:
                    return NivelesDeficiencia.Medio;

                default:
                    return NivelesDeficiencia.Bajo;
            }
        }

        public NivelesExposicion GetNivelExposicion(int exposicion)
        {
            switch (exposicion)
            {
                case 4:
                    return NivelesExposicion.Continua;

                case 3:
                    return NivelesExposicion.Frecuente;

                case 2:
                    return NivelesExposicion.Esporadica;

                default:
                    return NivelesExposicion.Ocasional;
            }
            throw new NotImplementedException();
        }

        public NivelesConsecuencia GetNivelConsecuencia(int consecuencia)
        {
            switch (consecuencia)
            {
                case 100:
                    return NivelesConsecuencia.Mortal_catastrófico;

                case 60:
                    return NivelesConsecuencia.Muy_grave;

                case 25:
                    return NivelesConsecuencia.Grave;

                default:
                    return NivelesConsecuencia.Leve;
            }
        }
        public string GetGenero(CategoriasGenero genero)
        {
            switch (genero)
            {
                case CategoriasGenero.Masculino:
                    return "Masculino";

                case CategoriasGenero.Femenino:
                    return "Femenino";

                case CategoriasGenero.Otro:
                    return "Otro";

                default:
                    return "Masculino";
            }
        }
        public string GetEstadoCivil(EstadosCivil estado)
        {
            switch (estado)
            {
                case EstadosCivil.Soltero:
                    return "Soltero";

                case EstadosCivil.Casado:
                    return "Casado";

                case EstadosCivil.Union_libre:
                    return "Unión libre";

                case EstadosCivil.Separado:
                    return "Separado";

                case EstadosCivil.Divorsiado:
                    return "Divorsiado";

                case EstadosCivil.Viudo:
                    return "Viudo";

                default:
                    return "Soltero";
            }
        }
        public string GetTipoVinculacion(TiposVinculacion tipo)
        {
            switch (tipo)
            {
                case TiposVinculacion.Nomina:
                    return "Nomina";

                case TiposVinculacion.Prestacion_Servicios:
                    return "Prestacion_Servicios";

                case TiposVinculacion.Agremiacion:
                    return "Agremiacion";

                default:
                    return "Nomina";
            }
        }
        public string GetCausaAccion(CategoriasCausa categoria)
        {
            switch (categoria)
            {
                case CategoriasCausa.Medicion:
                    return "Falta medición o control";
                case CategoriasCausa.Incumplimiento:
                    return "Incumplimiento de un  método o procedimiento";
                case CategoriasCausa.Inexistente:
                    return "Método inexistente";
                case CategoriasCausa.Inadecuada:
                    return "Planeación inadecuada";
                case CategoriasCausa.Economicos:
                    return "Falta de recursos económicos";
                case CategoriasCausa.Tecnicos:
                    return "Falta de recursos técnicos";
                case CategoriasCausa.Fisicos:
                    return "Falta de recursos físicos";
                case CategoriasCausa.Insumos:
                    return "Falta de insumos o suministros";
                case CategoriasCausa.Humanos:
                    return "Falta de talento humano";
                case CategoriasCausa.Entrenamiento:
                    return "Falta de entrenamiento";
                case CategoriasCausa.ClimaOrg:
                    return "Dificultades en el clima Org";
                case CategoriasCausa.Gobernabilidad:
                    return "Dificultades en la gobernabilidad";
                default:
                    return "Falta medición o control";
            }
        }
        public string GetFuenteAccion(FuentesAccion fuentes)
        {
            switch (fuentes)
            {
                case FuentesAccion.AuditoriaInt:
                    return "Auditoria Interna  de Calidad o de Gestión";

                case FuentesAccion.AuditoriaExt:
                    return "Auditoria externa";
                case FuentesAccion.Mapa:
                    return "Mapa de riesgos";
                case FuentesAccion.NoConformidad:
                    return "Producto y/o servicio no conforme";
                case FuentesAccion.Indicador:
                    return "Indicadores de Gestión de los procesos";
                case FuentesAccion.Incumplimiento:
                    return "Incumplimiento de documentos del SIG";
                case FuentesAccion.AccPropuesta:
                    return "Acciones propuestas en reunión, comité, consejos";
                case FuentesAccion.Quejas:
                    return "Quejas, reclamos o Sugerencias";
                case FuentesAccion.Revision:
                    return "Revisión por la dirección";
                case FuentesAccion.Encuesta:
                    return "Encuesta de Satisfacción";
                case FuentesAccion.Incidente:
                    return "Investigación incidente / accidente";
                case FuentesAccion.OtraFuente:
                    return "Otra";
                default:
                    return "Auditoria Interna  de Calidad o de Gestión";
            }
        }

        public string GetEfectos(EfectosPosibles efecto)
        {
            switch (efecto)
            {
                case EfectosPosibles.Daño_Extremo:
                    return "Daño extremo";

                case EfectosPosibles.Daño_Leve:
                    return "Daño leve";

                case EfectosPosibles.Daño_Moderado:
                    return "Daño Moderado";

                case EfectosPosibles.Daño_Propiedad:
                    return "Daño a la propiedad";

                case EfectosPosibles.Fallas_procesos:
                    return "Fallas en los procesos";

                case EfectosPosibles.Pérdidas_económicas:
                    return "Persidas económicas";

                default:
                    return "Nomina";
            }
        }
    }
}