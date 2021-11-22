using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Display(Name = "Primer Nombre")]
        public string PrimerNombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Segundo Nombre")]
        public string SegundoNombre { get; set; }
        [NotMapped]
        public string NombreCompleto { get; set; }

        public string Documento { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public DateTime FechaNacimiento { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public string Foto { get; set; }
        public string Firma { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public CategoriasGenero Genero { get; set; }
        public EstadosCivil EstadoCivil { get; set; }
        [Required]
        public TiposVinculacion TipoVinculacion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public Cargo Cargo { get; set; }
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