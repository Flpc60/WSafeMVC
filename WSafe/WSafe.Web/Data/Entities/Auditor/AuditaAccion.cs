using System;
using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities.Auditor
{
    public class AuditaAccion
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public DateTime Fecha { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Usuario { get; set; }
    }
}