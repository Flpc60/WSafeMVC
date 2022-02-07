using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities.Incidentes
{
    public enum ConsecuenciasMedio
    {
        [Display(Name = "Sin daño al ambiente o impacto positivo")]
        impactoPositivo = 1,
        [Display(Name = "Impacto Menor: Daño ambiental leve, consecuencias bajas o Derrame < 1 Bbl")]
        impactoMenor = 2,
        [Display(Name = "Impacto Medio: Contaminación o descarga suficiente para contaminar el medio ambiente o Derrame > 1 y < 10 Bb")]
        impactoMedio = 3,
        [Display(Name = "Impacto Mayor: Contaminación o descarga suficiente para contaminar el medio ambiente, Pasivos ambientales")]
        impactoMayor = 4,
        [Display(Name = "Impacto Masivo")]
        impactoMasivo = 5,
        [Display(Name = "Impacto Generalizado")]
        impactoGeneralizado = 6
    }
}