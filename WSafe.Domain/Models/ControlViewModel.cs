using System.ComponentModel.DataAnnotations;
using WSafe.Domain.Data.Entities;

namespace WSafe.Domain.Models
{
    public class ControlViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Codigo { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Aplicación")]
        public int CategoriaAppID { get; set; }
        public CategoriaAplicacion CategoriaAplicacion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Finalidad Aplicación")]
        public int FinalidadID { get; set; }
        public CategoriasFinalidad Finalidad { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Medida de intervención")]
        public JerarquiaControles Intervencion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Beneficios { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public decimal Presupuesto { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Medida de intervención")]
        public int EfectividadID { get; set; }
        public CategoriasEfectividad CategoriaEfectividad { get; set; }
    }
}