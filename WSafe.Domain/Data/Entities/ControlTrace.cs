using System;
using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class ControlTrace
    {
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public int ControlID { get; set; }
        public int CtrlReplaceID { get; set; }
        public int MaintCauseID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public int AplicacionID { get; set; }
        public int RiesgoID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public DateTime DateSigue { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public bool Efectividad { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
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