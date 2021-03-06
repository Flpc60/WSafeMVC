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
        public string Nombres { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Documento { get; set; }
        [NotMapped]
        public string NombreCompleto
        {
            get
            {
                return Nombres + " " + PrimerApellido + " " + SegundoApellido;
            }
        }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha nacimiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime FechaNacimiento { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha pago nomina")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime FechaNomina { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Dias a pagar")]
        public int DiasPago { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public CategoriasGenero Genero { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Estado civil")]
        public EstadosCivil EstadoCivil { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Tipo vinculación")]
        public TiposVinculacion TipoVinculacion { get; set; }
        [MaxLength(20)]
        public string EPS { get; set; }
        [MaxLength(20)]
        public string AFP { get; set; }
        [MaxLength(20)]
        public string ARL { get; set; }
        public int CargoID { get; set; }
        [MaxLength(20)]
        public string Firma { get; set; }
        [MaxLength(20)]
        public string Foto { get; set; }
        [MaxLength(50)]
        public string Direccion { get; set; }
    }
}