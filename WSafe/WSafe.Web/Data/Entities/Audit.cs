using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class Audit
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "FECHA AUDITORÍA")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime AuditDate { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "PROCESO AUDITADO")]
        [MaxLength(50)]
        public string Process { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "RESPONSABLE AUDITORÍA")]
        public int WorkerID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "DOCUMENTO RESPONSABLE")]
        [MaxLength(20)]
        public string AuditDocument { get; set; }
        public ICollection<AuditAction> AuditActions { get; set; }
        public ICollection<AuditedResult> AuditedResults { get; set; }
    }
}