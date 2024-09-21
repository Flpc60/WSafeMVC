using System.ComponentModel;

namespace WSafe.Domain.Data.Entities.Ppre
{
    public enum CategoryAmenazas
    {
        [Description("Naturales")]
        Naturales = 1,

        [Description("Tecnológicas")]
        Tecnologicas = 2,

        [Description("Sociales")]
        Sociales = 3
    }
}