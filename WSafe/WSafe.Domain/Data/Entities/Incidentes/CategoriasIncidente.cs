using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities.Incidentes
{
    public enum CategoriasIncidente
    {
        [Display(Name = "Incidente")]
        Incidente = 1,
        [Display(Name = "Accidente de Trabajo")]
        Accidente = 2,
        [Display(Name = "Accidente Mortal")]
        Mortal = 3
    }
}