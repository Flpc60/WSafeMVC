using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities.Incidentes
{
    public enum ConsecuenciasLesion
    {
        [Display(Name = "Leve")]
        Leve = 1,
        [Display(Name = "Grave")]
        Grave = 2,
        [Display(Name = "Muy grave")]
        MuyGrave = 3,
        [Display(Name = "Mortal")]
        Mortal = 4
    }
}