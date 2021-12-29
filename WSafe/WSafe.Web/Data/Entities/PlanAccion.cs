using System;
using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class PlanAccion
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha inicial")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public int AccionID { get; set; }
        public int RiesgoID { get; set; }
        public DateTime FechaInicial { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha final")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaFinal { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Plan acción")]
        public string Accion { get; set; }
        [Display(Name = "Trabajador responsable")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public Trabajador Trabajador { get; set; }
        public CategoriasEfectividad Efectividad { get; set; }
        [Display(Name = "Acción prioritaria")]
        public bool Prioritaria { get; set; }
        [Display(Name = "Costos ejecución")]
        public decimal Costos { get; set; }
    }
}