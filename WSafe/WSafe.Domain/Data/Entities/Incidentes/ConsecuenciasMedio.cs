using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities.Incidentes
{
    public enum ConsecuenciasMedio
    {
        [Display(Name = "Impacto Positivo")]
        Positivo = 1,
        [Display(Name = "Impacto Menor")]
        Menor = 2,
        [Display(Name = "Impacto Medio")]
        Medio = 3,
        [Display(Name = "Impacto Mayor")]
        Mayor = 4,
        [Display(Name = "Impacto Masivo")]
        Masivo = 5,
        [Display(Name = "Impacto Generalizado")]
        Generalizado = 6
    }
}