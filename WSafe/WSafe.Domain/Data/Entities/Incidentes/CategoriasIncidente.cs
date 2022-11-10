using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities.Incidentes
{
    public enum CategoriasIncidente
    {
        [Display(Name = "Incidentes")]
        Incidente = 1,
        [Display(Name = "Accidentes de Trabajo")]
        Accidente = 2,
        [Display(Name = "Enfermedad laboral")]
        Enfermedad = 3
    }
}