using System.ComponentModel.DataAnnotations;

namespace WSafe.Web.Data.Entities
{
    public enum IngresoCategories
    {
        [Display(Name = "Mínimo Legal (S.M.L.)")]
        Minimo = 1,
        [Display(Name = "Entre 1 a 3 S.M.L.")]
        De1a3,
        [Display(Name = "Entre 4 a 5 S.M.L.")]
        De4a5,
        [Display(Name = "Entre 5 y 6 S.M.L.")]
        De5a6,
        [Display(Name = "Mas de 7 S.M.L.")]
        Masde7
    }
}