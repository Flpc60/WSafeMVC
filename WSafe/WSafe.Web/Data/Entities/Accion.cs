using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class Accion
    {
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ZonaID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ProcesoID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ActividadID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int TareaID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Fecha solicitud")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaSolicitud { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Tipo acción")]
        public CategoriasAccion Categoria { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Nombre de quien reporta")]
        public int TrabajadorID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Fuente origen")]
        public FuentesAccion FuenteAccion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Descripción de la no conformidad")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Variable o Indicador de Control Antes")]
        public CategoriasEfectividad EficaciaAntes { get; set; }
        [Display(Name = "Variable o Indicador de Control Despues")]
        public CategoriasEfectividad EficaciaDespues { get; set; }
        [Display(Name = "Fecha cierre")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaCierre { get; set; }
        [Display(Name = "La acción tomada fué efectiva ?")]
        public bool Efectiva { get; set; }
        [Display(Name = "Abierta o Cerrada ?")]
        public bool Estado { get; set; }
        public IEnumerable<PlanAccion> Planes { get; set; }
        public IEnumerable<SeguimientoAccion> Seguimientos { get; set; }
    }
}