using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities.Incidentes
{
    public enum ConsecuenciasDaño
    {
        [Display(Name = "Sin daño")]
        Sinlesion = 1,
        [Display(Name = "Daño Menor")]
        EfectoMenor = 2,
        [Display(Name = "Daño Medio")]
        EfectoMedio = 3,
        [Display(Name = "Daño Mayor")]
        EfectoMayor = 4,
        [Display(Name = "Daño Superor")]
        EfectoSuperior = 5,
        [Display(Name = "Daño generalizado")]
        EfectoFatalGeneralizado = 6
    }
}