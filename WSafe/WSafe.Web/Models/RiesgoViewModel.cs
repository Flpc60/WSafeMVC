using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using WSafe.Domain.Data.Entities;

namespace WSafe.Domain.Models
{
    public class RiesgoViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public Proceso Proceso { get; set; }
        public IEnumerable<SelectListItem> Procesos { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public Zona Zona { get; set; }
        public IEnumerable<SelectListItem> Zonas { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public Actividad Actividad { get; set; }
        public IEnumerable<SelectListItem> Actividades { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public Tarea Tarea { get; set; }
        public IEnumerable<SelectListItem> Tareas { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public bool Rutinaria { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public CategoriaPeligro CategoriaPeligro { get; set; }
        public IEnumerable<SelectListItem> CategoriasPeligros { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public Peligro Peligro { get; set; }
        public IEnumerable<SelectListItem> Peligros { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public EfectosPosibles EfectosPosibles { get; set; }
        public Aplicacion ControlesFuente { get; set; }
        public Aplicacion ControlesMedio { get; set; }
        public Aplicacion ControlesIndividuo { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int NivelDeficiencia { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int NivelExposicion { get; set; }
        public int NivelProbabilidad { get; set; }
        [NotMapped]
        public string InterpretacionNP { get; set; }
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
        public ICollection<Aplicacion> ControlesIngenieria { get; set; }
        public ICollection<Aplicacion> ControlesAdmon { get; set; }
        public ICollection<Aplicacion> ControlesEPP { get; set; }
    }
}