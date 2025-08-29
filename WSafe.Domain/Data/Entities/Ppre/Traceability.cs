using System;
using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities.Ppre
{
    public class Traceability
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public int ActionPlanID { get; set; }
        public DateTime DateSigue { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public bool Efectividad { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [StringLength(500, ErrorMessage = "Las Observaciones no puede exceder los 500 caracteres")]
        [MaxLength(200)]
        public string Observations { get; set; }
        public int OrganizationID { get; set; }
        public int ClientID { get; set; }
        public int UserID { get; set; }
        public bool GenerateAction { get; set; }
        public CategoriasFinalidad Finality { get; set; }
        public int TrabajadorID { get; set; }
        public CategoriaAplicacion AplicationCategory { get; set; }
    }
}