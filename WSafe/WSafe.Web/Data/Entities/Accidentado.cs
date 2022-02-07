using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WSafe.Domain.Data.Entities
{
    public class Accidentado
    {
        [Key]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int IncidenteID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public Trabajador Trabajador { get; set; }
    }
}