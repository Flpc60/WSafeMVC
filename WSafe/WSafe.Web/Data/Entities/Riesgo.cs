using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WSafe.Domain.Data.Entities
{
    public class Riesgo
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public Proceso Proceso { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public Zona Zona { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public Actividad Actividad { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public Tarea Tarea { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public bool Rutinaria { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public CategoriaPeligro CategoriaPeligro { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public Peligro Peligro { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public EfectosPosibles EfectosPosibles { get; set; }
        public Aplicacion ControlesFuente { get; set; }
        public Aplicacion ControlesMedio { get; set; }
        public Aplicacion ControlesIndividuo { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int NivelDeficiencia { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int NivelExposicion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
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
        public ICollection<Accion> Acciones { get; set; }
    }
}