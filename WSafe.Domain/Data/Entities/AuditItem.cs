using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class AuditItem
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(400)]
        public string Name { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int NormaID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "PROCESO SG-SST")]
        public AuditChapters AuditChapter { get; set; }
    }
}