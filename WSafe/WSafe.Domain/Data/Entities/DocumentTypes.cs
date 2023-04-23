using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public enum DocumentTypes
    {
        [Display(Name = "Cédula ciudadanía")]
        Cedula = 1,
        [Display(Name = "Nit")]
        Nit = 2,
        [Display(Name = "Cédula extranjería")]
        Extranjería = 3,
        [Display(Name = "Tarjeta profesional")]
        Tarjeta = 4
    }
}