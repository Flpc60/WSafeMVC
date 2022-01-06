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
                return Documento + " " + Nombres + " " + PrimerApellido + " " + SegundoApellido;
            }
        }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha nacimiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaNacimiento { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha pago nomina")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
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
        public string EPS { get; set; }
        public string AFP { get; set; }
        public string ARL { get; set; }
    }
}