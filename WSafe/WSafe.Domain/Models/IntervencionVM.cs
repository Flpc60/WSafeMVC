using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Models
{
    public class IntervencionVM
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Vulnerability { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Categoria { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Amenaza { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Aplicacion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Finalidad { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Intervencion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(200)]
        public string Beneficios { get; set; }
        public decimal Presupuesto { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Responsable { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string FechaInicial { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string FechaFinal { get; set; }
    }
}
