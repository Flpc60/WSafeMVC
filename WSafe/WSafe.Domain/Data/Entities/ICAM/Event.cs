using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities.ICAM
{
    public class Event
    {
        public int ID { get; set; }
        public int IncidentID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Evento")]
        [MaxLength(50)]
        public string Name { get; set; }
        public int Order { get; set; }
    }
}