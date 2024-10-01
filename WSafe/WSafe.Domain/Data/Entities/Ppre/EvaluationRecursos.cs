using System.ComponentModel;

namespace WSafe.Domain.Data.Entities.Ppre
{
    public enum EvaluationRecursos
    {
        [Description("Suministros")]
        Suministros = 1,
        [Description("Edificaciones")]
        Edificaciones = 2,
        [Description("Equipos")]
        Equipos = 3
    }
}
