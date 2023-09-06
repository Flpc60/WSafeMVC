using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class Indicador
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Nombre del indicador")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Definicion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Numerador { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Denominador { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Formula { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Interpretacion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Periodicidad { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string TipoChart { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public decimal Meta { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(10)]
        public string Standard { get; set; }
    }
}