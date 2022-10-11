﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities.ICAM
{
    public class RootCause
    {
        public int ID { get; set; }
        public int IncidentID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Causa Principal")]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Razon")]
        public IEnumerable<Reason> Reasons { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Recomendación")]
        public IEnumerable<Recomendation> Recomendations { get; set; }
    }
}