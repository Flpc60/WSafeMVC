using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class RoleOperation
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public int RoleID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public int Operation { get; set; }
        public int Component { get; set; }
    }
}