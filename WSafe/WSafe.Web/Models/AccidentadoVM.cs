using System;
using System.ComponentModel.DataAnnotations;
using WSafe.Domain.Data.Entities;

namespace WSafe.Web.Models
{
    public class AccidentadoVM
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int IncidenteID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int TrabajadorID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(20)]
        public string Documento { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string NombreCompleto { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha nacimiento")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime FechaNacimiento { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Genero { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Estado civil")]
        public string EstadoCivil { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Tipo vinculación")]
        public string TipoVinculacion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Cargo { get; set; }
    }
}