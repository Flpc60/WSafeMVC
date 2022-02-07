using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities.Incidentes
{
    public enum ConsecuenciasDaño
    {
        [Display(Name = "Sin Daño: 0 a 5")]
        SinDaño = 1,
        [Display(Name = "Daño Menor: 6 a 10")]
        dañoMenor = 2,
        [Display(Name = "Daño Medio: 11 a 15")]
        dañoMedio = 3,
        [Display(Name = "Daño Mayor: 16 a 25")]
        dañoMayor = 4,
        [Display(Name = "Daño Superior: 26 a 30")]
        dañoSuperior = 5,
        [Display(Name = "Daño Generalizado: >  30")]
        dañoGeneralizado = 6
    }
}