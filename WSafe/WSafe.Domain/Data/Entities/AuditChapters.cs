using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public enum AuditChapters
    {
        [Display(Name = "POLÍTICA DE SEGURIDAD Y SALUD")]
        Politica = 1,
        [Display(Name = "PLANIFICACIÓN PARA LA IDENTIFICACIÓN DE PELIGROS, EVALUACIÓN Y VALORACIÓN DE RIESGOS")]
        Riesgos = 2,
        [Display(Name = "CUMPLIMIENTO REQUISITOS LEGALES")]
        Requisitos = 3,
        [Display(Name = "PLANIFICACIÓN DE OBJETIVOS Y PROGRAMAS")]
        Objetivos = 4,
        [Display(Name = "RECURSOS, FUNCIONES, RESPONSABILIDAD, RENDICIÓN DE CUENTAS Y AUTORIDAD")]
        Recursos = 5,
        [Display(Name = "COMPETENCIA, FORMACIÓN Y TOMA DE CONCIENCIA")]
        Competencia = 6,
        [Display(Name = "COMUNICACIÓN, PARTICIPACIÓN Y CONSULTA")]
        Comunicación = 7,
        [Display(Name = "DOCUMENTACIÓN DEL SG-SST")]
        Documentación = 7,
        [Display(Name = "CONTROL DOCUMENTOS")]
        ControlDocumentos = 8,
        [Display(Name = "CONTROL OPERATIVO")]
        ControlOperativo = 9,
        [Display(Name = "PREPARACIÓN Y RESPUESTA A EMERGENCIAS")]
        Emergencias = 10,
        [Display(Name = "MONITOREO Y MEDICIÓN DEL DESEMPEÑO")]
        Medición = 11,
        [Display(Name = "INVESTIGACIÓN DE INCODENTES Y ACCIONES CPM")]
        Investigación = 12,
        [Display(Name = "CONTROL DE REGISTROS")]
        ControlRegistros = 13,
        [Display(Name = "AUDITORÍA INTERNA")]
        Audotoría = 14,
        [Display(Name = "REVISIÓN POR LA DIRECCIÓN")]
        Revisión = 15,
        [Display(Name = "LIDERAZGO Y COMPROMISO")]
        Liderazgo = 16,
        [Display(Name = "PARTICIPACIÓN Y CONSULTA DE LOS TRABAJADORES")]
        Participación = 17,
        [Display(Name = "PROGRAMAS DE CAPACITACIÓN Y FORMACIÓN")]
        Capacitación = 18,
        [Display(Name = "MEJORA CONTINUA")]
        Mejora = 19,
        [Display(Name = "GESTIÓN DE LA SALUD")]
        Salud = 20
    }
}