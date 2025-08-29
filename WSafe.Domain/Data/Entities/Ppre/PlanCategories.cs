using System.ComponentModel;

namespace WSafe.Domain.Data.Entities.Ppre
{
    public enum PlanCategories
    {
        [Description("General")]
        General,
        [Description("Atención Médica")]
        Atencion,
        [Description("Vigilancia y Seguridad")]
        Vigilancia,
        [Description("Incendios")]
        Incendios,
        [Description("Evacuación")]
        Evacuacion,
        [Description("Información Pública")]
        Informacion
    }
}
