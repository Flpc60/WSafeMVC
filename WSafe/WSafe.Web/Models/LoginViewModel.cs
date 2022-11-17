using System.ComponentModel.DataAnnotations;

namespace WSafe.Web.Models
{
    public class LoginViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Usuario")]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Correo")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        [MaxLength(50)]
        public string Email { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Contraseña")]
        [MaxLength(100)]
        public string Password { get; set; }
        public bool Estado { get; set; }
    }
}