using System;
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
        [Display(Name = "AUDITOR")]
        public string Auditer { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "NO CONFORMIDAD")]
        [MaxLength(100)]
        public string NoConformance { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "CAUSA NO CONFORMIDAD")]
        [MaxLength(100)]
        public string Cause { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "ACCIÓN CORRECTIVA")]
        [MaxLength(100)]
        public string CorrectiveAction { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "RESPONSABLE ACCIÓN CORRECTIVA")]
        public string ResponsableAction { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "FECHA EJECUCIÓN")]
        public string ExecutionDate { get; set; }
        public ICollection<AuditAction> AuditActions { get; set; }
        public ICollection<AuditedResult> AuditedResults { get; set; }
        public ICollection<SigueAudit> Seguimients { get; set; }
    }
}