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
        public string TipoChart { get; set; }
        public decimal Meta { get; set; }
        public string Fuente { get; set; }
        public string Tipo { get; set; }
        public string Responsable { get; set; }
    }
}