using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class Calification
    {
        public int ID { get; set; }
        public int EvaluationID { get; set; }
        public int NormaID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Valor del item")]
        public decimal Valor { get; set; }
        [Display(Name = "Onservación")]
        public string Observation { get; set; }
    }
}