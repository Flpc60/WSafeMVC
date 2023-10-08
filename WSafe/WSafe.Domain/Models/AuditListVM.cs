using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WSafe.Domain.Data.Entities;

namespace WSafe.Web.Models
{
    public class AuditListVM
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "FECHA AUDITORÍA")]
        public string AuditDate { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "PROCESO AUDITADO")]
        public string Process { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "RESPONSABLE PROCESO AUDITADO")]
        public string Responsable { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "AUDITORES")]
        public string Auditers { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "RESULTADOS AUDITORÍA")]
        public ICollection<SigueAudit> Seguimients { get; set; }
        public ICollection<AuditAction> AuditActions { get; set; }
        public ICollection<AuditedResult> AuditedResults { get; set; }
    }
}