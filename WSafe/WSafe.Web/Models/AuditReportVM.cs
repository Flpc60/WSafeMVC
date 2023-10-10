using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WSafe.Domain.Data.Entities;

namespace WSafe.Web.Models
{
    public class AuditReportVM
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "FECHA AUDITORÍA")]
        public string AuditDate { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "RESPONSABLE PROCESO AUDITADO")]
        public string Responsable { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "PROCESO AUDITADO")]
        public string Process { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "AUDITORES")]
        public ICollection<Auditer> Auditers { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "ACCIONES CPM AUDITORÍA")]
        public ICollection<AuditedAction> AuditActions { get; set; }
    }
}