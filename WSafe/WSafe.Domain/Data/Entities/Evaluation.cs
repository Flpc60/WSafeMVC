﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class Evaluation
    {
        public int ID { get; set; }
        public int OrganizationID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Fecha evaluación")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime FechaEvaluacion { get; set; }
        public IEnumerable<Calification> Califications { get; set; }
    }
}