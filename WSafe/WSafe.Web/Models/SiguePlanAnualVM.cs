using System.ComponentModel.DataAnnotations;
using System;
using WSafe.Domain.Data.Entities;
using System.Collections.Generic;
using System.Web.Mvc;

namespace WSafe.Web.Models
{
    public class SiguePlanAnualVM
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "FECHA SEGUIMIENTO")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime DateSigue { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "RESPONSABLE")]
        public int TrabajadorID { get; set; }
        public IEnumerable<SelectListItem> Workers { get; set; }
        [Display(Name = "ESTADO")]
        public StatesActivity StateActivity { get; set; }
        [Display(Name = "CRONOGRAMA")]
        public StatesCronogram StateCronogram { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(typeof(short), "1", "9999", ErrorMessage = "Por favor ingrese un número de actividades ejecutadas válido.")]
        [Display(Name = "TAREAS")]
        public short Executed { get; set; }
        [MaxLength(100)]
        [Display(Name = "OBSERVACIONES")]
        public string Observation { get; set; }
    }
}