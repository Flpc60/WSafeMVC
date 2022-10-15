﻿using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities.ICAM
{
    public class Reason
    {
        public int ID { get; set; }
        public int IncidentID { get; set; }
        public int RootCauseID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Razón")]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}