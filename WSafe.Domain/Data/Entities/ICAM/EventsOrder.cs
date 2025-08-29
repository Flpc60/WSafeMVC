using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities.ICAM
{
    public class EventsOrder
    {
        public int ID { get; set; }
        public int EventID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public int Order { get; set; }
    }
}