using System;
using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class Trabajador
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Primer Apellido")]
        public string PrimerApellido { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Segundo Apellido")]
        public string SegundoApellido { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public string Nombres { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public string Documento { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public DateTime FechaNacimiento { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public string Email { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public Sede Sede { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public Genero Genero { get; set; }
        public EstadoCivil EstadoCivil { get; set; }
        [Required]
        public TiposVinculacion TipoVinculacion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public Cargo Cargo { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public Centro Centro { get; set; }
        public DateTime FechaIngreso { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public string EPS { get; set; }
        public string AFP { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public string ARL { get; set; }
        public string Direccion { get; set; }
        public string Telefonos { get; set; }
        public bool AltoRiesgo { get; set; }
        public string ActividadAltoRiesgo { get; set; }

    }
}
