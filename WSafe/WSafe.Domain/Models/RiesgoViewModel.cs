using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using WSafe.Domain.Data.Entities;

namespace WSafe.Domain.Models
{
    public class RiesgoViewModel
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un proceso.")]
        public int ZonaID { get; set; }
        [Display(Name = "Zona")]
        public IEnumerable<SelectListItem> Zonas { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un proceso.")]
        public int ProcesoID { get; set; }
        [Display(Name = "Proceso")]
        public IEnumerable<SelectListItem> Procesos { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un proceso.")]
        public int ActividadID { get; set; }
        [Display(Name = "Actividad")]
        public IEnumerable<SelectListItem> Actividades { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un proceso.")]
        public int TareaID { get; set; }
        [Display(Name = "Tarea")]
        public IEnumerable<SelectListItem> Tareas { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public bool Rutinaria { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un proceso.")]
        public int CategoriaPeligroID { get; set; }
        [Display(Name = "Clasificación")]
        public IEnumerable<SelectListItem> CategoriasPeligros { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un proceso.")]
        public int PeligroID { get; set; }
        [Display(Name = "Descripción")]
        public IEnumerable<SelectListItem> Peligros { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Efectos posibles")]
        public EfectosPosibles EfectosPosibles { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un proceso.")]
        public int NivelesDeficienciaID { get; set; }
        [Display(Name = "ND")]
        public IEnumerable<SelectListItem> NivelesDeficiencia { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un proceso.")]
        public int NivelesExposicionID { get; set; }
        [Display(Name = "NE")]
        public IEnumerable<SelectListItem> NivelesExposicion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "NP")]
        public int NivelProbabilidad { get; set; }
        [Display(Name = "Interpretación NP")]
        public string InterpretacionNP { get; set; }
        public int NivelesConsecuenciaID { get; set; }
        [Display(Name = "NC")]
        public IEnumerable<SelectListItem> NivelesConsecuencia { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "NR")]
        public int NivelRiesgo { get; set; }
        [Display(Name = "Interpretación NR")]
        public string InterpretacionNR { get; set; }
        [Display(Name = "Aceptabilidad NR")]
        public string AceptabilidadNR { get; set; }
        [Display(Name = "Significado NR")]
        public string SignificadoNR { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Nro. Expuestos")]
        public int NroExpuestos { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Peor consecuencia")]
        public Consecuencia PeorConsecuencia { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Requisito legal")]
        public bool RequisitoLegal { get; set; }
    }
}