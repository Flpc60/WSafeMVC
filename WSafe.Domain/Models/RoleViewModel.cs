using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WSafe.Domain.Models
{
    public class RoleViewModel
    {
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Rol usuario")]
        public string Name { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }
        public int UserID { get; set; }
    }
}