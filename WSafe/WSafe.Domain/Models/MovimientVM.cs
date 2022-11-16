using System.ComponentModel.DataAnnotations;

namespace WSafe.Web.Models
{
    public class MovimientVM
    {
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public int ID { get; set; }
        public int OrganizationID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "PHVA")]
        [MaxLength(2)]
        public string Ciclo { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "ITEM")]
        [MaxLength(6)]
        public string Item { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "ESTÁNDAR")]
        [MaxLength(200)]
        public string Name { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public int NormaID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "DESCRIPCIÓN ARCHIVO")]
        [MaxLength(200)]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "ARCHIVO")]
        public string Document { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(4)]
        public string Year { get; set; }
    }
}