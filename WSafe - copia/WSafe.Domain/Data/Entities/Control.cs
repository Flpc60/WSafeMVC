using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class Control
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int AplicacionID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int RiesgoID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Codigo { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Aplicación")]
        public CategoriaAplicacion CategoriaAplicacion { get; set; }
        public CategoriasFinalidad Finalidad { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Medida intervención")]
        public JerarquiaControles Intervencion { get; set; }
        public string Beneficios { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public decimal Presupuesto { get; set; }
        public ICollection<Traza> Trazas { get; set; }
        public int Efectividad { get; set; }
        [Display(Name = "Grado efectividad")]
        public CategoriasEfectividad CategoriaEfectividad { get; set; }
    }
}