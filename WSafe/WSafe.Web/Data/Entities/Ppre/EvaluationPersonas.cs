using System.ComponentModel;

namespace WSafe.Domain.Data.Entities.Ppre
{
    public enum EvaluationPersonas
    {
        [Description("Gestión Organizacional")]
        Organizacional = 1,
        [Description("Capacitación y Entrenaniento")]
        Entrenamiento = 2,
        [Description("Características de Seguridad")]
        Seguridad = 3
    }
}
