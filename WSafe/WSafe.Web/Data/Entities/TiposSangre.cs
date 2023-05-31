using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public enum TiposSangre
    {
        [Display(Name = "AB+")]
        Type1 = 1,
        [Display(Name = "AB-")]
        Type2 = 2,
        [Display(Name = "A+")]
        Type3 = 3,
        [Display(Name = "A-")]
        Type4 = 4,
        [Display(Name = "B+")]
        Type5 = 5,
        [Display(Name = "B-")]
        Type6 = 6,
        [Display(Name = "O+")]
        Type7 = 7,
        [Display(Name = "O-")]
        Type8 = 8
    }
}