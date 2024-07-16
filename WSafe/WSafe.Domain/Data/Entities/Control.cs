using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class Control
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Description { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public CategoriaAplicacion CategoriaAplicacion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public JerarquiaControles Intervencion { get; set; }
        public string Beneficios { get; set; }
        public decimal Presupuesto { get; set; }
        public decimal Eficacia { get; set; }
        public CategoriasEfectividad CategoriaEficacia { get; set; }
        public int OrganizationID { get; set; }
        public int ClientID { get; set; }
        public int UserID { get; set; }
    }
}