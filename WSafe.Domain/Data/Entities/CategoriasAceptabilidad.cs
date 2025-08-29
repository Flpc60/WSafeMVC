using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public enum CategoriasAceptabilidad
    {
        [Display(Name = "No Aceptable")]
        NoAceptable = 1,
        [Display(Name = "No Aceptable  o Aceptable con control específico")]
        AceptableControlEspecifico = 2,
        [Display(Name = "Mejorable")]
        Mejorable = 3,
        [Display(Name = "Aceptable")]
        Aceptable = 4
    }
}