using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WSafe.Domain.Data.Entities
{
    public class Trabajador
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [MaxLength(50, ErrorMessage = "El campo {0}, debe estar entre {1} caracteres o menos"), MinLength(5)]
        [Display(Name = "Primer Apellido")]
        public string PrimerApellido { get; set; }
        [Display(Name = "Segundo Apellido")]
        public string SegundoApellido { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [MaxLength(50, ErrorMessage = "El campo {0}, debe estar entre {1} caracteres o menos"), MinLength(5)]
        [Display(Name = "Primer Nombre")]
        public string PrimerNombre { get; set; }
        [Display(Name = "Segundo Nombre")]
        public string SegundoNombre { get; set; }
        [NotMapped]
        public string NombreCompleto
        {
            get
            {
                return PrimerNombre + " " + SegundoNombre + " " + PrimerApellido + " " + SegundoApellido;
            }
        }
        [MaxLength(20, ErrorMessage = "El campo {0}, debe contener máximo {1} caracteres")]
        public string Documento { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public DateTime FechaNacimiento { get; set; }
        public string Foto { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public string Firma { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public CategoriasGenero Genero { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public EstadosCivil EstadoCivil { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public TiposVinculacion TipoVinculacion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public Cargo Cargo { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public DateTime FechaIngreso { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public string EPS { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public string AFP { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public string ARL { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public string Telefonos { get; set; }
        public bool AltoRiesgo { get; set; }
        public string ActividadAltoRiesgo { get; set; }
    }
}