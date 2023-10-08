﻿using System.ComponentModel.DataAnnotations;
using System;

namespace WSafe.Domain.Data.Entities
{
    public class SigueAudit
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int AuditID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "FECHA")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime SeguimientDate { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(200)]
        [Display(Name = "REPORTE SUPERVISOR")]
        public string SupervisorReport { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(200)]
        [Display(Name = "REPORTE TRABAJADOR")]
        public string WorkerReport { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(200)]
        [Display(Name = "REPORTE SST")]
        public string SSTReport { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(200)]
        [Display(Name = "OBSERVACIONES")]
        public string Observations { get; set; }
    }
}