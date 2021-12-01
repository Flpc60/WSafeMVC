using System;
using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class Accion
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public int RiesgoID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public CategoriasAccion Categoria { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Fecha solicitud")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaSolicitud { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public Trabajador Trabajador { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public string Intervenciones { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public string Observaciones { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Fecha inicial")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicial { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Fecha final")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaFinal { get; set; }
        public MedidasControl MedidasControl { get; set; }
    }
}