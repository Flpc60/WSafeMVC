using System.ComponentModel.DataAnnotations;

namespace WSafe.Web.Data.Entities
{
    public enum TiposIndicador
    {
        [Display(Name = "Frecuencia de Accidentalidad")]
        FrecuenciaAccidentes = 1,
        [Display(Name = "Severidad de Accidentalidad")]
        SeveridadAccidentes = 2,
        [Display(Name = "Proporción de Accidentes de trabajo mortales")]
        ProporciónAccidentesMortales = 3,
        [Display(Name = "Prevalencia de enfermedad laboral")]
        PrevelenciaEnfermedad = 4,
        [Display(Name = "Ausentismo por causa médica")]
        AusentismoCausaMedica = 5,
        [Display(Name = "Cumplimiento de los objetivos")]
        CumplimientoObjetivos = 6,
        [Display(Name = "Cumplimiento del plan de trabajo anual")]
        CumplimientoPlanAnual = 7,
        [Display(Name = "Evaluación de las NC detectadas en el seguimiento al plan anual de trabajo")]
        EvaluaciónNCPlanAnual = 8,
        [Display(Name = "Análisis de los registros de enfermedades laborales, incidentes, accidentes de trabajo y ausentismo laboral por enfermedad")]
        AnálisisEnfermedadesAccidentesAusentismo = 9,
        [Display(Name = "Indicador Estructura")]
        IndicadorEstructura = 10,
        [Display(Name = "Evaluación inicial")]
        EvaluacionInicial = 11,
        [Display(Name = "Ejecución plan de trabajo anual y su cronograma")]
        EjecuciónPlanAnual = 12,
        [Display(Name = "Cumplimiento de los procesos de reporte e investigación de los " +
            "incidentes, accidentes de trabajo y enfermedades laborales")]
        InvestigacionAccidentesEnfermedades = 13,
        [Display(Name = "Registro estadístico de enfermedades laborales, incidentes, accidentes de trabajo y" +
            "ausentismo laboral por enfermedades laborales")]
        EnfermedadesAccidentesAusentismo = 14,
        [Display(Name = "Ejecución del plan para la prevención y atención de emergencias")]
        EjecuciónPlanPrevenciónEmergencias = 15,
        [Display(Name = "Eficacia inspecciones")]
        EficaciaInspecciones = 16
    }
}