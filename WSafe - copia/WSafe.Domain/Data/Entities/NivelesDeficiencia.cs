using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public enum NivelesDeficiencia
    {
        [Display(Name = "Muy Alto (MA)")]
        Muy_alto = 1,
        [Display(Name = "Alto (A)")]
        Alto = 2,
        [Display(Name = "Medio (M)")]
        Medio = 3,
        [Display(Name = "Bajo (B)")]
        Bajo = 4
    }
}