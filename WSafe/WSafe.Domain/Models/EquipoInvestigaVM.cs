using System;
using System.ComponentModel.DataAnnotations;
using WSafe.Domain.Data.Entities;

namespace WSafe.Domain.Models
{
    public class EquipoInvestigaVM
    {
        public int ID { get; set; }
        public string Documento { get; set; }
        public string NombreCompleto { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha nacimiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaNacimiento { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Genero { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Cargo { get; set; }
    }
}