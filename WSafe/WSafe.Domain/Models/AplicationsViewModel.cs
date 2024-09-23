using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using WSafe.Domain.Data.Entities;

namespace WSafe.Domain.Models
{
    public class AplicationsViewModel
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int RiesgoID { get; set; }
        [MaxLength(100)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Aplicación del control")]
        public CategoriaAplicacion CategoriaAplicacion { get; set; }
        [Display(Name = "Tipo de acción")]
        public CategoriasFinalidad Finalidad { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Medida intervención")]
        public JerarquiaControles Intervencion { get; set; }
        [MaxLength(100)]
        public string Beneficios { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public decimal Presupuesto { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Trabajador responsable")]
        public int TrabajorID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public IEnumerable<SelectListItem> Trabajadores { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha inicial")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime FechaInicial { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha final")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime FechaFinal { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(100)]
        public string Observaciones { get; set; }
    }
}