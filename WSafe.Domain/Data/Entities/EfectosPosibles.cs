using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public enum EfectosPosibles
    {
        [Display(Name = "Daño leve")]
        Daño_Leve = 1,
        [Display(Name = "Daño moderado")]
        Daño_Moderado = 2,
        [Display(Name = "Daño extremo")]
        Daño_Extremo = 3,
        [Display(Name = "Daños a la propiedad")]
        Daño_Propiedad = 4,
        [Display(Name = "Fallas en los procesos")]
        Fallas_procesos = 5,
        [Display(Name = "Pérdidas económicas")]
        Pérdidas_económicas = 6
    }
}