using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities.Incidentes
{
    public enum ConsecuenciasLesion
    {
        [Display(Name = "Sin Lesión")]
        Sinlesion = 1,
        [Display(Name = "Efecto Menor")]
        EfectoMenor = 2,
        [Display(Name = "Efecto Medio")]
        EfectoMedio = 3,
        [Display(Name = "Efecto Mayor")]
        EfectoMayor = 4,
        [Display(Name = "Fatalidad invalidez")]
        Fatalidad = 5,
        [Display(Name = "Fatalidad Múltiple")]
        FatalidadMultiple = 6
    }
}