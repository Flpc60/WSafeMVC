using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class Accion
    {
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
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime FechaSolicitud { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Tipo")]
        public CategoriasAccion Categoria { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Nombre de quien reporta")]
        public int TrabajadorID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Origen")]
        public FuentesAccion FuenteAccion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "No conformidad")]
        [MaxLength(200)]
        public string Descripcion { get; set; }
        [Display(Name = "Indicador Antes")]
        public CategoriasEfectividad EficaciaAntes { get; set; }
        [Display(Name = "Indicador Despues")]
        public CategoriasEfectividad EficaciaDespues { get; set; }
        [Display(Name = "Fecha cierre")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime FechaCierre { get; set; }
        [Display(Name = "Efectiva")]
        public bool Efectiva { get; set; }
        public int RiesgoID { get; set; }
        public ICollection<PlanAction> Planes { get; set; }
        public IEnumerable<SeguimientoAccion> Seguimientos { get; set; }
        public ActionCategories ActionCategory { get; set; }
        public int PlanNumber => Planes == null ? 0 : Planes.Count;
        public int OrganizationID { get; set; }
        public int ClientID { get; set; }
        public int UserID { get; set; }
        public int ControlTraceID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public int MainCause1ID { get; set; }
        public int MainCause2ID { get; set; }
        public int MainCause3ID { get; set; }
        public int MainCause4ID { get; set; }
        public int MainCause5ID { get; set; }
    }
}