using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public enum NivelesConsecuencia
    {
        [Display(Name = "Mortal o Catastrófico (M)")]
        Mortal_catastrófico = 1,
        [Display(Name = "Muy grave (MG)")]
        Muy_grave = 2,
        [Display(Name = "Grave (G)")]
        Grave = 3,
        [Display(Name = "Leve (L)")]
        Leve = 4
    }
}