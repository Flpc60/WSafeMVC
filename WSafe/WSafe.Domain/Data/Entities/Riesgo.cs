using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class Riesgo
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public Zona Zona { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public Proceso Proceso { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public Actividad Actividad { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public Tarea Tarea { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public bool Rutinaria { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public Peligro Peligro { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Efectos posibles")]
        public EfectosPosibles EfectosPosibles { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "ND")]
        public int NivelDeficiencia { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "NE")]
        public int NivelExposicion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "NP")]
        public int NivelProbabilidad
        {
            get
            {
                return NivelDeficiencia * NivelExposicion;
            }
        }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "NC")]
        public int NivelConsecuencia { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "NR")]
        public int NivelRiesgo
        {
            get
            {
                return NivelProbabilidad * NivelConsecuencia;
            }
        }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Categoria")]
        public string CategoriaRiesgo
        {
            get
            {
                switch (NivelRiesgo)
                {
                    case int nr when (nr >= 600):
                        return "I";

                    case int nr when (nr >= 150 && nr < 600):
                        return "II";

                    case int nr when (nr >= 40 && nr < 150):
                        return "III";

                    default:
                        return "IV";
                }
            }
         }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Aceptabilidad riesgo")]
        public CategoriasAceptabilidad Aceptabilidad { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Expuestos")]
        public int NroExpuestos { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Requisito")]
        public bool RequisitoLegal { get; set; }
        public ICollection<Aplicacion> MedidasIntervencion { get; set; }
        public ICollection<Accion> Acciones { get; set; }
    }
}