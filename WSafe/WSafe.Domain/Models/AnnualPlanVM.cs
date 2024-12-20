﻿using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Models
{
    public class AnnualPlanVM
    {
        public int ID { get; set; }
        [Display(Name = "PHVA")]
        [MaxLength(2)]
        public string Cycle { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "ACTIVIDAD")]
        [MaxLength(100)]
        public string Activity { get; set; }
        [MaxLength(100)]
        [Display(Name = "ENTREGABLES")]
        public string Entregables { get; set; }
        [Display(Name = "RECURSOS")]
        public string Recursos { get; set; }
        [Display(Name = "RESPONSABLE")]
        public string Responsable { get; set; }
        [MaxLength(200)]
        [Display(Name = "OBSERVACIONES")]
        public string Observation { get; set; }
        [Display(Name = "ESTADO")]
        public string StateActivity { get; set; }
        [Display(Name = "PROGRAMADAS")]
        public short Programed { get; set; }
        [Display(Name = "EJECUTADAS")]
        public short Executed { get; set; }
        [Display(Name = "CUMPLIMIENTO")]
        public string PorcentajeCumplimiento { get; set; }
        [MaxLength(200)]
        [Display(Name = "SEGUIMIENTOS")]
        public string Seguimients { get; set; }
    }
}