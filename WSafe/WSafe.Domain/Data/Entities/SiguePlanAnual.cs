using System;
using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class SiguePlanAnual
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateSigue { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int TrabajadorID { get; set; }
        public StatesActivity StateActivity { get; set; }
        public StatesCronogram StateCronogram { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(typeof(short), "0", "9999", ErrorMessage = "Por favor ingrese un número de actividades ejecutadas válido.")]
        public short Executed { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(typeof(short), "1", "9999", ErrorMessage = "Por favor ingrese un número de actividades ejecutadas válido.")]
        public short Programed { get; set; }
        [MaxLength(100)]
        public string Observation { get; set; }
        public ActionCategories ActionCategory { get; set; }
        [MaxLength(200)]
        public string FileName { get; set; }
        public int PlanActivityID { get; set; }
    }
}