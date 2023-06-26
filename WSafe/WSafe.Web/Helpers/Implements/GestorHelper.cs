using System;
using System.Collections.Generic;
using System.IO;
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

                case "IV":
                    return "Aceptable";

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
                    return "Perdidas económicas";

                default:
                    return "Nomina";
            }
        }

        public string GetCategoriaAplicacion(CategoriaAplicacion categoria)
        {
            switch (categoria)
            {
                case CategoriaAplicacion.Fuente:
                    return "Fuente";

                case CategoriaAplicacion.Medio:
                    return "Medio";

                case CategoriaAplicacion.Individuo:
                    return "Individuo";

                default:
                    return "Fuente";
            }
        }

        public string GetJerarquiaControl(JerarquiaControles categoria)
        {
            switch (categoria)
            {
                case JerarquiaControles.Controles_Admon:
                    return "Controles administrativos";
                case JerarquiaControles.Controles_Ingeniería:
                    return "Controles de ingeniería";
                case JerarquiaControles.Eliminacion:
                    return "Eliminación";
                case JerarquiaControles.Sustitucion:
                    return "Sustitución";
                case JerarquiaControles.EPP:
                    return "EPP";
                case JerarquiaControles.Señaliza:
                    return "Señalización";

                default:
                    return "Controles administrativos";
            }
        }
        public string GetPeriodo(int[] periodo)
        {
            var result = "";
            for (int i = 0; i < periodo.Length; i++)
            {
                switch (periodo[i])
                {
                    case 1:
                        result += "ENERO, ";
                        break;

                    case 2:
                        result += "FEBRERO, ";
                        break;

                    case 3:
                        result += "MARZO, ";
                        break;

                    case 4:
                        result += "ABRIL, ";
                        break;

                    case 5:
                        result += "MAYO, ";
                        break;

                    case 6:
                        result += "JUNIO, ";
                        break;

                    case 7:
                        result += "JULIO, ";
                        break;

                    case 8:
                        result += "AGOSTO, ";
                        break;

                    case 9:
                        result += "SEPTIEMBRE, ";
                        break;

                    case 10:
                        result += "OCTUBRE, ";
                        break;

                    case 11:
                        result += "NOVIEMBRE, ";
                        break;

                    case 12:
                        result += "DICIEMBRE";
                        break;
                }
            }
            return result;
        }

        public string GetRole(int role)
        {
            return _empresaContext.Roles.FirstOrDefault(r => r.ID == role).Name.ToUpper().Trim();
        }
        public string GetComponent(int component)
        {
            switch (component)
            {
                case (int)Components.ENTIDAD:
                    return "ENTIDAD";

                case (int)Components.RIESGOS:
                    return "RIESGOS";

                case (int)Components.INCIDENTES:
                    return "INCIDENTES";

                case (int)Components.ACCIONES:
                    return "ACCIONES";

                case (int)Components.INDICADORES:
                    return "INDICADORES";

                case (int)Components.USUARIOS:
                    return "USUARIOS";

                case (int)Components.SGSST:
                    return "SG-SST";

                case (int)Components.EVALUACIÓN:
                    return "EVALUACIÓN";

                case (int)Components.TRABAJADORES:
                    return "TRABAJADORES";

                case (int)Components.ACTOS_INSEGUROS:
                    return "ACTOS_INSEGUROS";

                default:
                    return "ACTOS_INSEGUROS";
            }
        }
        public string GetOperation(int operation)
        {
            switch (operation)
            {
                case (int)Operations.Consultar:
                    return "Consultar";

                case (int)Operations.Modificar:
                    return "Modificar";

                case (int)Operations.Adicionar:
                    return "Adicionar";

                case (int)Operations.Borrar:
                    return "Borrar";

                default:
                    return "Consultar";
            }
        }
        public string GetActionCategory(int estado)
        {
            switch (estado)
            {
                case 1:
                    return "Sín iniciar";

                case 2:
                    return "En proceso";

                case 3:
                    return "Finalizada";

                default:
                    return "Sín iniciar";
            }
        }
        public string GetCiclo(string ciclo)
        {
            switch (ciclo.Trim())
            {
                case "P":
                    return "PLANEAR";

                case "H":
                    return "HACER";

                case "V":
                    return "VERIFICAR";

                case "A":
                    return "ACTUAR";

                default:
                    return "PLANEAR";

            }
        }
        public string GetStandard(string standar)
        {
            switch (standar.Trim())
            {
                case "R":
                    return "RECURSOS";

                case "I":
                    return "GESTIÓN INTEGRAL";

                case "S":
                    return "GESTIÓN SALUD";

                case "P":
                    return "GESTIÓN PELIGROS";


                case "A":
                    return "GESTIÓN AMENAZAS";

                case "V":
                    return "VERIFICACIÓN";

                case "M":
                    return "MEJORAMIENTO";

                default:
                    return "RECURSOS";

            }
        }

        public string GetRecurso(RecursosCategory recurso)
        {
            switch (recurso)
            {
                case RecursosCategory.Administrativos:
                    return "Administrativos";

                case RecursosCategory.Financieros:
                    return "Financieros";

                case RecursosCategory.Tecnicos:
                    return "Técnicos";

                case RecursosCategory.Humanos:
                    return "Humanos";

                default:
                    return "Administrativos";
            }
        }
        public void CreateFolder(string path)
        {

            // create nodo ORG/SG-SST/YEAR
            var directory = path;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // create nodo ORG/SG-SST/YEAR/PLANEAR
            directory = $"{path}/1. PLANEAR";
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // create nodo ORG/SG-SST/YEAR/HACER
            directory = $"{path}/2. HACER";
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // create nodo ORG/SG-SST/YEAR/VERIFICAR
            directory = $"{path}/3. VERIFICAR";
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // create nodo ORG/SG-SST/YEAR/ACTUAR
            directory = $"{path}/4. ACTUAR";
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var list = _empresaContext.Normas.OrderByDescending(n => n.Item).ToList();
            foreach (var norma in list)
            {
                switch (norma.Ciclo)
                {
                    case "P":
                        directory = $"{path}/1. PLANEAR/{norma.Item}";
                        break;

                    case "H":
                        directory = $"{path}/2. HACER/{norma.Item}";
                        break;

                    case "V":
                        directory = $"{path}/3. VERIFICAR/{norma.Item}";
                        break;

                    case "A":
                        directory = $"{path}/4. ACTUAR/{norma.Item}";
                        break;
                }

                // create nodo ORG/SG-SST/YEAR/ACTUAR/ITEM
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

            }
            return;
        }
        public string GetEscolaridad(NivelesEscolaridad escolaridad)
        {
            switch (escolaridad)
            {
                case NivelesEscolaridad.Primaria:
                    return "Primaria";

                case NivelesEscolaridad.Secundaria:
                    return "Secundaria";

                case NivelesEscolaridad.Doctorado:
                    return "Doctorado";

                case NivelesEscolaridad.Especialización:
                    return "Especialización";

                case NivelesEscolaridad.Maestría:
                    return "Maestría";

                case NivelesEscolaridad.Tecnólogo:
                    return "Tecnólogo";

                case NivelesEscolaridad.Técnico:
                    return "Técnico";

                case NivelesEscolaridad.Profesional:
                    return "Profesional";

                default:
                    return "Primaria";
            }
        }
        public string GetWorkArea(WorkAreas area)
        {
            switch (area)
            {
                case WorkAreas.Administrativa:
                    return "Administrativa";

                case WorkAreas.Contratista:
                    return "Contratista";

                case WorkAreas.Operativa:
                    return "Operativa";

                case WorkAreas.Practicante:
                    return "Practicante";

                default:
                    return "Administrativa";
            }
        }
        public string GetTipoJornada(TiposJornada jornada)
        {
            switch (jornada)
            {
                case TiposJornada.Diurna:
                    return "Diurna";

                case TiposJornada.Nocturna:
                    return "Nocturna";

                case TiposJornada.Rotativa:
                    return "Rotativa";

                default:
                    return "Diurna";
            }
        }
        public int CalculateAge(DateTime dateOfBirth)
        {
            DateTime currentDate = DateTime.Now;
            int age = currentDate.Year - dateOfBirth.Year;

            // Check if the birthday has already occurred this year
            if (currentDate.Month < dateOfBirth.Month ||
                (currentDate.Month == dateOfBirth.Month && currentDate.Day < dateOfBirth.Day))
            {
                age--;
            }

            return age;
        }
        public string GetBloodType(TiposSangre blood)
        {
            switch (blood)
            {
                case TiposSangre.Type1:
                    return "AB+";

                case TiposSangre.Type2:
                    return "AB-";

                case TiposSangre.Type3:
                    return "A+";

                case TiposSangre.Type4:
                    return "A-";

                case TiposSangre.Type5:
                    return "B+";

                case TiposSangre.Type6:
                    return "B-";

                case TiposSangre.Type7:
                    return "O+";

                case TiposSangre.Type8:
                    return "O-";

                default:
                    return "AB+";
            }
        }
        public string GetStratum(EstratoCategories stratum)
        {
            switch (stratum)
            {
                case EstratoCategories.Estrato1:
                    return "Estrato 1";

                case EstratoCategories.Estrato2:
                    return "Estrato 2";

                case EstratoCategories.Estrato3:
                    return "Estrato 3";

                case EstratoCategories.Estrato4:
                    return "Estrato 4";

                case EstratoCategories.Estrato5:
                    return "Estrato 5";

                case EstratoCategories.Estrato6:
                    return "Estrato 6";

                case EstratoCategories.Estrato7:
                    return "Estrato 7";

                case EstratoCategories.Estrato8:
                    return "Estrato 8";

                case EstratoCategories.Estrato9:
                    return "Estrato 9";

                case EstratoCategories.Estrato10:
                    return "Estrato 10";

                default:
                    return "Estrato 1";
            }
        }
        public string GetTenenciaVivienda(TenenciasVivienda tenencia)
        {
            switch (tenencia)
            {
                case TenenciasVivienda.Propia:
                    return "Propia";

                case TenenciasVivienda.Arrendada:
                    return "Arrendada";

                case TenenciasVivienda.Familiar:
                    return "Familiar";

                case TenenciasVivienda.Otra:
                    return "Otra";

                default:
                    return "Propia";
            }
        }
    }
}