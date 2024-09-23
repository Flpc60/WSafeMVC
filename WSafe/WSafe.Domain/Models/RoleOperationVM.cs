using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WSafe.Domain.Models
{
    public class RoleOperationVM
    {
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Rol usuario")]
        public int RoleID { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Operación")]
        public int OperationID { get; set; }
        public IEnumerable<SelectListItem> Operations { get; set; }
    }
}