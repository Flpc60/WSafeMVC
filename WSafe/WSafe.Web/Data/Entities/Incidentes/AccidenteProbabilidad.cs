using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities.Incidentes
{
    public enum AccidenteProbabilidad
    {
        [Display(Name = "> 1 Año")]
        A =1,
        [Display(Name = "1 Mes a 1 Año")]
        B = 2,
        [Display(Name = "Mensual")]
        C = 3,
        [Display(Name = "Semanal")]
        D = 4,
        [Display(Name = "Diario")]
        E = 5
    }
}