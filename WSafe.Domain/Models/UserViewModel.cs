using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Models
{
    public class UserViewModel
    {
        public int ID { get; set; }
        [Display(Name = "USUARIO")]
        public string Name { get; set; }
        [Display(Name = "CORREO")]
        public string Email { get; set; }
        [Display(Name = "ROL")]
        public string Role { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Perfiles usuario")]
        public int RoleID { get; set; }
        [Display(Name = "ORGANIZACIÓN")]
        [MaxLength(50)]
        public string RazonSocial { get; set; }
        [Display(Name = "FECHA REGISTRO")]
        [MaxLength(10)]
        public string RegisterDate { get; set; }
    }
}