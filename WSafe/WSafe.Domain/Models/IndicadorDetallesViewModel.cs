using System.ComponentModel.DataAnnotations;

namespace WSafe.Web.Models
{
    public class IndicadorDetallesViewModel
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Periodo")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una zona.")]
        public string MesAnn { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(0, 999999, ErrorMessage = "El campo {0} debe estar entre {1} y {2}")]
        public int Numerador { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(0, 999999, ErrorMessage = "El campo {0} debe estar entre {1} y {2}")]
        public int Denominador { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(0, 999, ErrorMessage = "El campo {0} debe estar entre {1} y {2}")]
        public decimal Resultado { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int AcumuladoNumerador { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int AcumuladoDenominador { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int AcumuladoResultado { get; set; }
        public string MesAnn1 { get; set; }
        public decimal Resultado1 { get; set; }
        public decimal IF { get; set; }
        public decimal IG { get; set; }
        public decimal ILI { get; set; }
        public int HHT { get; set; }
        public int Ausentismos { get; set; }
        public int Accidentes { get; set; }
    }
}