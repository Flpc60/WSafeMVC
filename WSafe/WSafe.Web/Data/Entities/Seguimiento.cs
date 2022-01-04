using System;
using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class Seguimiento
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int PlanAccionID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int AccionID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha seguimiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaSeguimiento { get; set; }
        public CategoriasAccion TipoAccion { get; set; }
        [Display(Name = "Nombre quien reporta")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public Trabajador Trabajador { get; set; }
        public string Resultado { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Fecha cierre")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaCierre { get; set; }
    }
}