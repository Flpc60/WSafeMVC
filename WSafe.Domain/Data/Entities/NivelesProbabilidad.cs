using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public enum NivelesProbabilidad
    {
        [Display(Name = "Muy Alto (MA)")]
        Continua = 1,
        [Display(Name = "Alto (A)")]
        Frecuente = 2,
        [Display(Name = "Medio (M)")]
        Ocasional = 3,
        [Display(Name = "Bajo (B)" )]
        Esporadica = 4
    }
}