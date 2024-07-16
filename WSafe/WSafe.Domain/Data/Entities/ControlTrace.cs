using System;
using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class ControlTrace
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public int ControlID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public int AplicacionID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public int RiesgoID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public DateTime DateSigue { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public bool Efectividad { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public decimal Presupuesto { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public CategoriaAplicacion CategoriaAplicacion { get; set; }
        public bool Active { get; set; }
        public int OrganizationID { get; set; }
        public int ClientID { get; set; }
        public int UserID { get; set; }
    }
}