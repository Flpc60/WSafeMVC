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
        public int ProcesoID { get; set; }
        public IEnumerable<SelectListItem> Procesos { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un proceso.")]
        public int ZonaID { get; set; }
        public IEnumerable<SelectListItem> Zonas { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un proceso.")]
        public int ActividadID { get; set; }
        public IEnumerable<SelectListItem> Actividades { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un proceso.")]
        public int TareaID { get; set; }
        public IEnumerable<SelectListItem> Tareas { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public bool Rutinaria { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un proceso.")]
        public int CategoriaPeligroID { get; set; }
        public IEnumerable<SelectListItem> CategoriasPeligros { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un proceso.")]
        public int PeligroID { get; set; }
        public IEnumerable<SelectListItem> Peligros { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public EfectosPosibles EfectosPosibles { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un proceso.")]
        public int NivelesDeficienciaID { get; set; }
        public IEnumerable<SelectListItem> NivelesDeficiencia { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un proceso.")]
        public int NivelesExposicionID { get; set; }
        public IEnumerable<SelectListItem> NivelesExposicion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int NivelProbabilidad { get; set; }
        public string InterpretacionNP { get; set; }
        public int NivelesConsecuenciaID { get; set; }
        public IEnumerable<SelectListItem> NivelesConsecuencia { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int NivelRiesgo { get; set; }
        public string InterpretacionNR { get; set; }
        public string AceptabilidadNR { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int NroExpuestos { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public Consecuencia PeorConsecuencia { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public bool RequisitoLegal { get; set; }
    }
}