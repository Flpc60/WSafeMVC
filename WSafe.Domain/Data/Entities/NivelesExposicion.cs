using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public enum NivelesExposicion
    {
        [Display(Name = "Continua (EC)")]
        Continua = 1,
        [Display(Name = "Frecuente (EF)")]
        Frecuente = 2,
        [Display(Name = "Ocasional (EO)")]
        Ocasional = 3,
        [Display(Name = "Esporádica (EE)")]
        Esporadica = 4
    }
}