using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public enum ActCategories
    {
        [Display(Name = "Acto inseguro")]
        Acto = 1,
        [Display(Name = "Condición insegura")]
        Condicion = 2,
    }
}