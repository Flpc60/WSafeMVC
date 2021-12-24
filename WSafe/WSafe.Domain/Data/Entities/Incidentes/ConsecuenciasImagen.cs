using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities.Incidentes
{
    public enum ConsecuenciasImagen
    {
        [Display(Name = "Sin Impacto")]
        SinImpacto = 1,
        [Display(Name = "Daño Interno")]
        Interno = 2,
        [Display(Name = "Local")]
        Local = 3,
        [Display(Name = "Regional")]
        Regional = 3,
        [Display(Name = "Clientes")]
        Clientes = 4,
        [Display(Name = "Nacional")]
        Nacional = 5
    }
}