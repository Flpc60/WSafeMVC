using System;
using System.ComponentModel.DataAnnotations;

namespace WSafe.Web.Models
{
    public class AccidentadoVM
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int IncidenteID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int TrabajadorID { get; set; }
        [MaxLength(20)]
        public string Documento { get; set; }
        public string NombreCompleto { get; set; }
        [Display(Name = "Fecha nacimiento")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime FechaNacimiento { get; set; }
        public string Genero { get; set; }
        [Display(Name = "Estado civil")]
        public string EstadoCivil { get; set; }
        [Display(Name = "Tipo vinculación")]
        public string TipoVinculacion { get; set; }
        public string Cargo { get; set; }
    }
}