using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class Control
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Codigo { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public CategoriasFinalidad Finalidad { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Descripción")]
        public JerarquiaControles Intervencion { get; set; }
        public string Beneficios { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public decimal Presupuesto { get; set; }
        public ICollection<Traza> Trazas { get; set; }
        public int Efectividad { get; set; }
    }
}