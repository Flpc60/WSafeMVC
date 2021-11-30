using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using WSafe.Domain.Data.Entities;

namespace WSafe.Domain.Models
{
    public class RiesgoViewModel
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ProcesoID { get; set; }
        public IEnumerable<SelectListItem> Procesos { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ZonaID { get; set; }
        public IEnumerable<SelectListItem> Zonas { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ActividadID { get; set; }
        public IEnumerable<SelectListItem> Actividades { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int TareaID { get; set; }
        public IEnumerable<SelectListItem> Tareas { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public bool Rutinaria { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CategoriaPeligroID { get; set; }
        public CategoriaPeligro CategoriaPeligro { get; set; }
        public IEnumerable<SelectListItem> CategoriasPeligros { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int PeligroID { get; set; }
        public IEnumerable<SelectListItem> Peligros { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public EfectosPosibles EfectosPosibles { get; set; }
        public Aplicacion ControlesFuente { get; set; }
        public Aplicacion ControlesMedio { get; set; }
        public Aplicacion ControlesIndividuo { get; set; }
        public int NivelesDeficienciaID { get; set; }
        public IEnumerable<SelectListItem> NivelesDeficiencia { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int NivelDeficiencia { get; set; }
        public int NivelesExposicionID { get; set; }
        public IEnumerable<SelectListItem> NivelesExposicion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int NivelExposicion { get; set; }
        public int NivelProbabilidad { get; set; }
        [NotMapped]
        public string InterpretacionNP { get; set; }
        public int NivelesConsecuenciaID { get; set; }
        public IEnumerable<SelectListItem> NivelesConsecuencia { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int NivelConsecuencias { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int NivelRiesgo { get; set; }
        [NotMapped]
        public string InterpretacionNR { get; set; }
        [NotMapped]
        public string AceptabilidadNR { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int NroExpuestos { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public Consecuencia PeorConsecuencia { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public bool RequisitoLegal { get; set; }
        public Aplicacion Eliminacion { get; set; }
        public Aplicacion Sustitucion { get; set; }
        public int ControlesIngeneriaID { get; set; }
        public ICollection<Aplicacion> ControlesIngenieria { get; set; }
        public int ControlesAdmonID { get; set; }
        public ICollection<Aplicacion> ControlesAdmon { get; set; }
        public int ControlesEppID { get; set; }
        public ICollection<Aplicacion> ControlesEPP { get; set; }
    }
}