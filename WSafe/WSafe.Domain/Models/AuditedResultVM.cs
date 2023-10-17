﻿using System.ComponentModel.DataAnnotations;

namespace WSafe.Web.Models
{
    public class AuditedResultVM
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Chapter { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Requisite { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string RequisiteItem { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string NC { get; set; }
        public string CP { get; set; }
        public string CYD { get; set; }
        public int OrderResult { get; set; }
    }
}