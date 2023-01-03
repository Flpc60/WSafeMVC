using System;
using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class PlanActivity
    {
        public int ID { get; set; }
        public int EvaluationID { get; set; }
        public int NormaID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "FECHA CUMPLIMIENTO")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime FechaFinal { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "ACTIVIDAD")]
        [MaxLength(100)]
        public string Activity { get; set; }
        [Display(Name = "TRABAJADOR RESPONSABLE")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int TrabajadorID { get; set; }
        [Display(Name = "PRESUPUESTO")]
        public decimal Presupuesto { get; set; }
        public ActionCategories ActionCategory { get; set; }
        [MaxLength(100)]
        [Display(Name = "OBSERVACIONES")]
        public string Observation { get; set; }
    }
}