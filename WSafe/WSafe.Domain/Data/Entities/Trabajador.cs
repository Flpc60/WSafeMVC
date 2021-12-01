using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WSafe.Domain.Data.Entities
{
    public class Trabajador
    {
        public int ID { get; set; }
        [Display(Name = "Primer Apellido")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string PrimerApellido { get; set; }
        [Display(Name = "Segundo Apellido")]
        public string SegundoApellido { get; set; }
        [Display(Name = "Primer Nombre")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
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
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Documento { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha nacimiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaNacimiento { get; set; }
        public string Foto { get; set; }
        public string Firma { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public CategoriasGenero Genero { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public EstadosCivil EstadoCivil { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public TiposVinculacion TipoVinculacion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public Cargo Cargo { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha ingreso")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaIngreso { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string EPS { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string AFP { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string ARL { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Telefonos { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public bool AltoRiesgo { get; set; }
        public string ActividadAltoRiesgo { get; set; }
    }
}