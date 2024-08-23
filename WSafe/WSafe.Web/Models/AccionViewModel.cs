using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using WSafe.Domain.Data.Entities;

namespace WSafe.Web.Models
{
    public class AccionViewModel
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Zona")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una zona.")]
        public int ZonaID { get; set; }
        public IEnumerable<SelectListItem> Zonas { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Proceso")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un proceso.")]
        public int ProcesoID { get; set; }
        public IEnumerable<SelectListItem> Procesos { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Actividad")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una actividad.")]
        public int ActividadID { get; set; }
        public IEnumerable<SelectListItem> Actividades { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Tarea")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una tarea.")]
        public int TareaID { get; set; }
        public IEnumerable<SelectListItem> Tareas { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Fecha solicitud")]
        public string FechaSolicitud { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Tipo acción")]
        public CategoriasAccion Categoria { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Responsable")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un trabajador.")]
        public int TrabajadorID { get; set; }
        public IEnumerable<SelectListItem> Trabajadores { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Fuente origen")]
        public FuentesAccion FuenteAccion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Fuente origen")]
        public string Origen { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Descripción de la no conformidad")]
        [MaxLength(200)]
        public string Descripcion { get; set; }
        [Display(Name = "Indicador Antes")]
        public CategoriasEfectividad EficaciaAntes { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Indicador Despues")]
        public CategoriasEfectividad EficaciaDespues { get; set; }
        [Display(Name = "Fecha cierre")]
        public string FechaCierre { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Efectiva")]
        public bool Efectiva { get; set; }
        public ICollection<PlanAction> Planes { get; set; }
        public ICollection<Seguimiento> Seguimientos { get; set; }
        public string FechaSolicitudStr { get; set; }
        public string FechaCierreStr { get; set; }
        [Display(Name = "Estado")]
        public ActionCategories ActionCategory { get; set; }
        public string ActionState { get; set; }
        public int OrganizationID { get; set; }
        public int ClientID { get; set; }
        public int UserID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Causa 1:")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una causa")]
        public int MainCause1ID { get; set; }
        [Display(Name = "Causa 2:")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una causa")]
        public int MainCause2ID { get; set; }
        [Display(Name = "Causa 3:")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una causa")]
        public int MainCause3ID { get; set; }
        [Display(Name = "Causa 4:")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una causa")]
        public int MainCause4ID { get; set; }
        [Display(Name = "Causa 5:")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una causa")]
        public int MainCause5ID { get; set; }
        public IEnumerable<SelectListItem> Causes { get; set; }
    }
}