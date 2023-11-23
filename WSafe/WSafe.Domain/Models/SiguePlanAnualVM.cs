﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using WSafe.Domain.Data.Entities;

namespace WSafe.Web.Models
{
    public class SiguePlanAnualVM
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "FECHA CUMPLIMIENTO")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime FechaFinal { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "FECHA SEGUIMIENTO")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime DateSigue { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "ACTIVIDAD")]
        [MaxLength(100)]
        public string Activity { get; set; }
        [MaxLength(100)]
        [Display(Name = "ENTREGABLES")]
        public string Entregables { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "RESPONSABLE")]
        public int TrabajadorID { get; set; }
        public IEnumerable<SelectListItem> Workers { get; set; }
        [MaxLength(100)]
        [Display(Name = "OBSERVACIONES")]
        public string Observation { get; set; }
        [Display(Name = "ESTADO")]
        public StatesActivity StateActivity { get; set; }
        [Display(Name = "CRONOGRAMA")]
        public StatesCronogram StateCronogram { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(typeof(short), "1", "9999", ErrorMessage = "Por favor ingrese un número de actividades programadas válido.")]
        [Display(Name = "PROGRAMADAS")]
        public short Programed { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(typeof(short), "1", "9999", ErrorMessage = "Por favor ingrese un número de actividades ejecutadas válido.")]
        [Display(Name = "EJECUTADAS")]
        public short Executed { get; set; }
        [Display(Name = "ACCIÓN")]
        public ActionCategories ActionCategory { get; set; }
        [Display(Name = "SUBIR EVIENCIA")]
        [MaxLength(200)]
        public string FileName { get; set; }
        public int PlanActivityID { get; set; }
    }
}