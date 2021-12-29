using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public enum FuentesAccion
    {
        [Display(Name = "Auditoria Interna  de Calidad o de Gestión")]
        AuditoriaInt = 1,
        [Display(Name = "Auditoria Externa")]
        AuditoriaExt = 2,
        [Display(Name = "Mapa de Riesgos")]
        Mapa = 3,
        [Display(Name = "Producto y/o servicio no conforme")]
        NoConformidad = 4,
        [Display(Name = "Indicadores de Gestión de los procesos")]
        Indicador = 5,
        [Display(Name = "Incumplimiento de documentos del SIG")]
        Incumplimiento = 6,
        [Display(Name = "Acciones propuestas en reunión, comité, consejos")]
        AccPropuesta = 7,
        [Display(Name = "Quejas, reclamos o Sugerencias")]
        Quejas = 8,
        [Display(Name = "Revisión por la dirección")]
        Revision = 9,
        [Display(Name = "Encuesta de Satisfacción")]
        Encuesta = 10,
        [Display(Name = "Otra fuente")]
        OtraFuente = 11
    }
}