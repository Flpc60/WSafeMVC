using System;
using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class SiguePlanAnual
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "FECHA SEGUIMIENTO")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime DateSigue { get; set; }
        [Display(Name = "TRABAJADOR RESPONSABLE")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int TrabajadorID { get; set; }
        public StatesActivity StateActivity { get; set; }
        public StatesCronogram StateCronogram { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(typeof(short), "1", "9999", ErrorMessage = "Por favor ingrese un número de actividades ejecutadas válido.")]
        public short Executed { get; set; }
        [MaxLength(100)]
        [Display(Name = "OBSERVACIONES")]
        public string Observation { get; set; }
    }
}