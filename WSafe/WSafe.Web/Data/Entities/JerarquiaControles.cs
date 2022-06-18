using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public enum JerarquiaControles
    {
        Eliminacion = 1,
        Sustitucion = 2,
        [Display(Name = "Controles de ingeniería")]
        Controles_Ingeniería = 3,
        [Display(Name = "Controles de administración")]
        Controles_Admon = 4,
        [Display(Name = "Señalización")]
        Señaliza = 5,
        [Display(Name = "Equipos, EPP")]
        EPP = 6
    }
}