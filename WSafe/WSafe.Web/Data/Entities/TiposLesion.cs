using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public enum TiposLesion
    {
        [Display(Name = " FRACTURA")]
        Fractura = 1,
        [Display(Name = " LUXACIÓN")]
        Luxacion = 2,
        [Display(Name = "TORCEDURA, ESGUINCE, DESGARRO MUSCULAR, HERNIA O LACERACIÓN DE MÚSCULO O TENDÓN SIN HERIDA")]
        Torcedura = 3,
        [Display(Name = "CONMOCIÓN O TRAUMA INTERNO")]
        Conmocion = 4,
        [Display(Name = "AMPUTACIÓN O ENUCLEACIÓN (Exclusión o pérdida del ojo)")]
        PerdidaOjo = 5,
        [Display(Name = "HERIDA")]
        Herida = 6,
        [Display(Name = "TRAUMA SUPERFICIAL (Incluye rasguño, punción o pinchazo y lesión en ojo por cuerpo extraño)")]
        TraumaSuperficial = 7,
        [Display(Name = "GOLPE, CONTUSIÓN O APLASTAMIENTO")]
        Golpe = 8,
        [Display(Name = " QUEMADURA")]
        Quemadura = 9,
        [Display(Name = "ENVENENAMIENTO O INTOXICACIÓN AGUDA O ALERGIA")]
        Envenenamiento = 10,
        [Display(Name = " EFECTO DEL TIEMPO, DEL CLIMA U OTRO RELACIONADO CON EL AMBIENTE")]
        EfectoClima = 11,
        [Display(Name = "ASFIXIA")]
        Asfixia = 12,
        [Display(Name = "EFECTO DE LA ELECTRICIDAD")]
        EfectoElectricidad = 13,
        [Display(Name = "EFECTO NOCIVO DE LA RADIACIÓN")]
        EfectoRadiacion = 14,
        [Display(Name = "LESIONES MÚLTIPLES")]
        LesionesMultiples = 15,
        [Display(Name = "OTRO")]
        Otro = 16,
        [Display(Name = "NO APLICA")]
        NoAplica = 17
    }
}