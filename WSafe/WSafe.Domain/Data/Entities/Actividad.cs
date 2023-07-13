using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class Actividad
    {
        [Key]
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public string Descripcion { get; set; }
        public int OrganizationID { get; set; }
        public int ClientID { get; set; }
    }
}