using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class Auditer
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "NOMBRES")]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "APELLIDOS")]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "DOCUMENTO")]
        [MaxLength(20)]
        public string Document { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "REGISTRO")]
        [MaxLength(20)]
        public string RegisterNumber { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "IDONEIDAD")]
        [MaxLength(100)]
        public string Idoneidad { get; set; }
    }
}