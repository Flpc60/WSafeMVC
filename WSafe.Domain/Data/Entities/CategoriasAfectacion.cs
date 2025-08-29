using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public enum CategoriasAfectacion
    {
        [Display(Name = "CABEZA")]
        Cabeza = 1,
        [Display(Name = "OJO")]
        Ojo = 2,
        [Display(Name = "CUELLO")]
        Cuello = 3,
        [Display(Name = "TRONCO")]
        Tronco = 4,
        [Display(Name = "TORAX")]
        Torax = 5,
        [Display(Name = "ABDOMEN")]
        Abdomen = 6,
        [Display(Name = " MIEMBROS SUPERIORES")]
        MiembrosSuperiores = 7,
        [Display(Name = "MANOS")]
        Manos = 8,
        [Display(Name = "MIEMBROS INFERIORES")]
        MiembrosInferiores = 9,
        [Display(Name = "PIES")]
        Pies = 10,
        [Display(Name = "UBICACIONES MÚLTIPLES")]
        UbicacionesMultiples = 11,
        [Display(Name = "LESIONES GENERALES U OTRAS")]
        LesionesGenerales = 12
    }
}