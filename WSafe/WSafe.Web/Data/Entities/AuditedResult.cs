using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class AuditedResult
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int AuditID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public AuditItem AuditItem { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public AuditCalifications Result { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public WorkAreas Process { get; set; }
        public AuditChapters AuditChapter { get; set; }
    }
}