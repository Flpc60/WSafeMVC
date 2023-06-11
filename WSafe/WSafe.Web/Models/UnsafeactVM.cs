using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using WSafe.Domain.Data.Entities;

namespace WSafe.Web.Models
{
    public class UnsafeactVM
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "ZONA DONDE OCURRE EVENTO")]
        public int ZonaID { get; set; }
        public IEnumerable<SelectListItem> Zona { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ProcesoID { get; set; }
        public IEnumerable<SelectListItem> Proceso { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ActividadID { get; set; }
        public IEnumerable<SelectListItem> Actividad { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int TareaID { get; set; }
        public IEnumerable<SelectListItem> Tarea { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "OBJETO DEL REPORTE")]
        public ActCategories ActCategory { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "ANTECEDENTES")]
        [MaxLength(100)]
        public string Antecedentes { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "FECHA DEL ANTECEDENTE")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaAntecedente { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "TIPO PELIGRO")]
        public int CategoriaPeligroID { get; set; }
        public IEnumerable<SelectListItem> CategoriaPeligro { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "FACTOR RIESGO")]
        public int PeligroID { get; set; }
        public IEnumerable<SelectListItem> Peligro { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "DESCRIPCIÓN EVENTO")]
        [MaxLength(100)]
        public string ActDescription { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "PROBABLES CONSECUENCIAS")]
        public string ProbableConsecuencia { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "RECOMENDACIONES")]
        public string Recomendations { get; set; }
        [Display(Name = "NOMBRE QUIEN IDENTIFICA")]
        public int WorkerID { get; set; }
        public IEnumerable<SelectListItem> Worker { get; set; }
        [Display(Name = "NOMBRE QUIEN REPORTA")]
        public int Worker1ID { get; set; }
        public IEnumerable<SelectListItem> Worker1 { get; set; }
        [Display(Name = "NOMBRE QUIEN RECIBE")]
        public int Worker2ID { get; set; }
        public IEnumerable<SelectListItem> Worker2 { get; set; }
        public int MovimientID { get; set; }
        public int OrganizationID { get; set; }
        public int ClientID { get; set; }
    }
}