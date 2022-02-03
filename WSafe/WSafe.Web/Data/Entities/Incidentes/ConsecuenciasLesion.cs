using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities.Incidentes
{
    public enum ConsecuenciasLesion
    {
        [Display(Name = "Sin Lesión: Casi accidente")]
        sinLesion = 1,
        [Display(Name = "Efecto Menor: Primer auxilio, lesión sin incapacidad")]
        efectoMenor = 2,
        [Display(Name = "Efecto Medio: Incap. parcial")]
        efectoMedio = 3,
        [Display(Name = "Efecto Mayor: Incap. permanent")]
        EfectoMayor = 4,
        [Display(Name = "Fatalidad e invalidez: Por ATEP")]
        fatalidadInvalidez = 5,
        [Display(Name = "Fatalidad Múltiple: Por ATEP")]
        fatalidadMultiple = 6
    }
}