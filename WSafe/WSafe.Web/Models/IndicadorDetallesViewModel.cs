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
        public int Numerador { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int Denominador { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public decimal Resultado { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int AcumuladoNumerador { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int AcumuladoDenominador { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int AcumuladoResultado { get; set; }
    }
}