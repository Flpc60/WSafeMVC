using System;
using System.ComponentModel.DataAnnotations;
using WSafe.Domain.Data.Entities;

namespace WSafe.Web.Models
{
    public class AplicacionVM
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int RiesgoID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Descripción")]
        [MaxLength(100)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Aplicación del control")]
        public CategoriaAplicacion CategoriaAplicacion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Medida intervención")]
        public JerarquiaControles Intervencion { get; set; }
        [MaxLength(100)]
        public string Beneficios { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public decimal Presupuesto { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Responsable")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un trabajador.")]
        public int TrabajadorID { get; set; }
        public string Responsable { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha inicial")]
        public string FechaInicial { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha final")]
        public string FechaFinal { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(100)]
        public string Observaciones { get; set; }
        public string TextFechaInicial { get; set; }
        public string TextFechaFinal { get; set; }
        public string TextCategoria { get; set; }
        public string TextIntervencion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "ND")]
        public int NivelDeficiencia { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "NE")]
        public int NivelExposicion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "NC")]
        public int NivelConsecuencia { get; set; }
        public CategoriasAceptabilidad Aceptabilidad { get; set; }
    }
}