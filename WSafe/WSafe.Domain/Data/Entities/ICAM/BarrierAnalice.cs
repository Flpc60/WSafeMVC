using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities.ICAM
{
    public class BarrierAnalice
    {
        public int ID { get; set; }
        public int EventID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public BarrierCategories BarrierCategory { get; set; }
    }
}