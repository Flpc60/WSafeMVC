using System.ComponentModel;

namespace WSafe.Domain.Data.Entities
{
    public enum NivelesDeficiencia
    {
        [Description("Muy Alto (MA)")]
        Muy_alto = 10,
        [Description("Alto (A) ")]
        Alto = 6,
        [Description("Medio (M) ")]
        Medio = 2,
        [Description("Bajo (B) ")]
        Bajo = 0
    }
}