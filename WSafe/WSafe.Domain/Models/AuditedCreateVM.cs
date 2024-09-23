using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using WSafe.Domain.Data.Entities;

namespace WSafe.Domain.Models
{
    public class AuditedCreateVM
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "FECHA AUDITORÍA")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime AuditDate { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "AUDITOR")]
        public int AuditerID { get; set; }
        public IEnumerable<SelectListItem> Auditers { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "PROCESOS / AREAS A AUDITAR")]
        public WorkAreas AuditProcess { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "RESPONSABLE")]
        public int WorkerID { get; set; }
        public IEnumerable<SelectListItem> Workers { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int OrganizationID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ClientID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int UserID { get; set; }
        [Display(Name = "REQUISITO A AUDITAR")]
        public IEnumerable<SelectListItem> AuditChapter { get; set; }
    }
}