using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities.Incidentes
{
    public enum AccidenteProbabilidad
    {
        [Display(Name = "> 1 Año")]
        Probabilidad1 =1,
        [Display(Name = "1 Mes a 1 Año")]
        Probabilidad2 = 2,
        [Display(Name = "Mensual")]
        Probabilidad3 = 3,
        [Display(Name = "Semanal")]
        Probabilidad4 = 4,
        [Display(Name = "Diario")]
        Probabilidad5 = 5
    }
}