using System.ComponentModel;

namespace WSafe.Domain.Data.Entities.Ppre
{
    public enum EvaluationSystems
    {
        [Description("Servicios")]
        Servicios = 1,
        [Description("Sistemas Alternos")]
        Sistemas = 2,
        [Description("Recuperación")]
        Recuperacion = 3
    }
}

