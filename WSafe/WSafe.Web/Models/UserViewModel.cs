using System.ComponentModel.DataAnnotations;

namespace WSafe.Web.Models
{
    public class UserViewModel
    {
        public int ID { get; set; }
        [Display(Name = "Nombre usuario")]
        public string Name { get; set; }
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }
        [Display(Name = "Rol usuario")]
        public string Role { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Perfiles usuario")]
        public int RoleID { get; set; }
    }
}