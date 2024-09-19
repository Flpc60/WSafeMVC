using System.Collections.Generic;

namespace WSafe.Domain.Data.Entities.Ppre
{
    public enum ScalesCalification
    {
        Sí = 0,
        Parcial = 1,
        No = 2
    }
    public static class ScalesCalificationValues
    {
        public static readonly Dictionary<ScalesCalification, double> Values = new Dictionary<ScalesCalification, double>
        {
            { ScalesCalification.Sí, 0.0 },
            { ScalesCalification.Parcial, 0.5 },
            { ScalesCalification.No, 1.0 }
        };
    }
}
