using System;
using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class User
    {
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Nombre usuario")]
        public string Name { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public string Email { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public string Password { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Fecha inscripcion")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public int RoleID { get; set; }
    }
}