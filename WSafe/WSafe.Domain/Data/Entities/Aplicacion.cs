using System;
using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class Aplicacion
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int RiesgoID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public CategoriaAplicacion CategoriaAplicacion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public Control Control { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public Trabajador Trabajador { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha inicial")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicial { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha final")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaFinal { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public decimal Presupuesto { get; set; }
        public CategoriasEfectividad CategoriaEfectividad { get; set; }
        public string Observaciones { get; set; }
    }
}