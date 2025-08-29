using System.ComponentModel.DataAnnotations;

namespace WSafe.Web.Data.Entities
{
    public enum FreeTimeCategories
    {
        [Display(Name = "Otro trabajo")]
        Trabajo = 1,
        [Display(Name = "Labores domésticas")]
        Domesticas,
        [Display(Name = "Recreación y deporte")]
        Deporte,
        [Display(Name = "Estudio")]
        Estudio,
        [Display(Name = "Ninguno")]
        Ninguno
    }
}