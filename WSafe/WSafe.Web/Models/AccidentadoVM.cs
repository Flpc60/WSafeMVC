using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WSafe.Web.Models
{
    public class AccidentadoVM
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Accidentado")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un trabajador.")]
        public int TrabajadorID { get; set; }
        public IEnumerable<SelectListItem> Trabajadores { get; set; }

    }
}