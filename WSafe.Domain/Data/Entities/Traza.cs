using System;
using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class Traza
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public int ControlID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public int AplicacionID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public int RiesgoID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public DateTime FechaInicial { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public DateTime FechaFinal { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public int Efectividad { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public decimal Presupuesto { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public string EstadoActual { get; set; }
    }
}