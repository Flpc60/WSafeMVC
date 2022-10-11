using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class Operation
    {
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public string Name { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public string Component { get; set; }
    }
}