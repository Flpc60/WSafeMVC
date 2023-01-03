using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class Norma
    {
        public int ID { get; set; }
        public int OrganizationID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "CICLO")]
        [MaxLength(2)]
        public string Ciclo { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "ITEM")]
        [MaxLength(6)]
        public string Item { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "NOMBRE")]
        [MaxLength(200)]
        public string Name { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "VALOR")]
        public decimal Valor { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "ESTÁNDAR")]
        [MaxLength(2)]
        public string Standard { get; set; }
        public string Verification { get; set; }
        [MaxLength(2)]
        public string Range { get; set; }
    }
}