using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WSafe.Domain.Data.Entities
{
    public class Trabajador
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ID { get; set; }
        [Display(Name = "Primer Apellido")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50)]
        public string PrimerApellido { get; set; }
        [Display(Name = "Segundo Apellido")]
        [MaxLength(50)]
        public string SegundoApellido { get; set; }
        [Display(Name = "Nombres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50)]
        public string Nombres { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(20)]
        public string Documento { get; set; }
        [NotMapped]
        [MaxLength(120)]
        public string NombreCompleto
        {
            get
            {
                return Nombres.Trim() + " " + PrimerApellido.Trim() + " " + SegundoApellido.Trim();
            }
        }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha nacimiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaNacimiento { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public CategoriasGenero Genero { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Estado civil")]
        public EstadosCivil EstadoCivil { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50)]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(20)]
        public string Telefonos { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha de ingreso")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaIngreso { get; set; }
        [Display(Name = "Dias a pagar")]
        public int DiasPago { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Tipo vinculación")]
        public TiposVinculacion TipoVinculacion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CargoID { get; set; }
        [MaxLength(20)]
        public string EPS { get; set; }
        [MaxLength(20)]
        public string AFP { get; set; }
        [MaxLength(20)]
        public string ARL { get; set; }
        [MaxLength(20)]
        public string Firma { get; set; }
        [Display(Name = "Fecha retiro")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaRetiro { get; set; }
        public int OrganizationID { get; set; }
        public int ClientID { get; set; }
        public int UserID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Tipo documento")]
        public DocumentTypes DocumentType { get; set; }
    }
}