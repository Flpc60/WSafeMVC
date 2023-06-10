using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class Movimient
    {
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public int OrganizationID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public int UserID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public int NormaID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [MaxLength(200)]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [MaxLength(200)]
        public string Document { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(4)]
        public string Year { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [MaxLength(6)]
        public string Item { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [MaxLength(2)]
        public string Ciclo { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [MaxLength(6)]
        public string Type { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [MaxLength(100)]
        public string Path { get; set; }
        public int ClientID { get; set; }
    }
}
