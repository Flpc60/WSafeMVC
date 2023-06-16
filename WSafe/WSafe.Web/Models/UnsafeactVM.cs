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
        public int ZonaID { get; set; }
        [Display(Name = "ZONA")]
        public IEnumerable<SelectListItem> Zonas { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ProcesoID { get; set; }
        [Display(Name = "PROCESO")]
        public IEnumerable<SelectListItem> Procesos { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ActividadID { get; set; }
        [Display(Name = "ACTIVIDAD")]
        public IEnumerable<SelectListItem> Actividades { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int TareaID { get; set; }
        [Display(Name = "TAREA")]
        public IEnumerable<SelectListItem> Tareas { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaReporte { get; set; }
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
        public int CategoriaPeligroID { get; set; }
        [Display(Name = "TIPO PELIGRO")]
        public IEnumerable<SelectListItem> CategoriasPeligro { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int PeligroID { get; set; }
        [Display(Name = "FACTOR RIESGO")]
        public IEnumerable<SelectListItem> Peligros { get; set; }
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
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int WorkerID { get; set; }
        [Display(Name = "NOMBRE QUIEN IDENTIFICA")]
        public IEnumerable<SelectListItem> Workers { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "NOMBRE QUIEN REPORTA")]
        public int Worker1ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "NOMBRE QUIEN RECIBE")]
        public int Worker2ID { get; set; }
        public int MovimientID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int OrganizationID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ClientID { get; set; }
        public int UserID { get; set; }
        [Display(Name = "SUBIR EVIENCIA")]
        [MaxLength(200)]
        public string FileName { get; set; }
    }
}
