using System;
using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class Client
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(100)]
        [Display(Name = "Nombre")]
        public string Name { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Tipo documento")]
        public DocumentTypes DocumentType { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(20)]
        [Display(Name = "Número documento")]
        public string Document { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(20)]
        [Display(Name = "Teléfono")]
        public string Phone { get; set; }
        public int Folders { get; set; }
        public DateTime ControlDate { get; set; }
    }
}