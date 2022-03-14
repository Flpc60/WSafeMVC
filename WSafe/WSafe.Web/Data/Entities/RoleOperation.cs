using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class RoleOperation
    {
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public int RolID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public int OperationID { get; set; }
    }
}