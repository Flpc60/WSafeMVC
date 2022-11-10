using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class Movimient
    {
        public int ID { get; set; }
        public int OrganizationID { get; set; }
        public int NormaID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Nombre")]
        [MaxLength(200)]
        public string Name { get; set; }
        public byte[]  Document { get; set; }
    }
}