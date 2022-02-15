using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WSafe.Domain.Data.Entities;

namespace WSafe.Web.Models
{
    public class ListaRiesgosVM
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Zona { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Proceso { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Actividad { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Tarea { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public bool Rutinaria { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Clasificación")]
        public string Clasificacion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Peligro { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Efectos posibles")]
        public EfectosPosibles EfectosPosibles { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "ND")]
        public int NivelDeficiencia { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "NE")]
        public int NivelExposicion { get; set; }
        private int _nivelProbabilidad { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "NP")]
        public int NivelProbabilidad
        {
            get
            {
                return _nivelProbabilidad = NivelDeficiencia * NivelExposicion;
            }
            set
            {
                _nivelProbabilidad = value;
            }
        }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "NC")]
        public int NivelConsecuencia { get; set; }
        private int _nivelRiesgo { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "NR")]
        public int NivelRiesgo
        {
            get
            {
                return _nivelRiesgo = NivelProbabilidad * NivelConsecuencia;
            }
            set
            {
                _nivelRiesgo = value;
            }
        }
        private string _categoriaRiesgo { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "INR")]
        public string CategoriaRiesgo
        {
            get
            {
                var cat = "";
                switch (NivelRiesgo)
                {
                    case int nr when (nr >= 600):
                        cat = "I";
                        break;
                    case int nr when (nr >= 150 && nr < 600):
                        cat = "II";
                        break;

                    case int nr when (nr >= 40 && nr < 150):
                        cat = "III";
                        break;

                    default:
                        cat = "IV";
                        break;
                }
                return _categoriaRiesgo = cat;
            }
            set
            {
                _categoriaRiesgo = value;
            }
        }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public CategoriasAceptabilidad Aceptabilidad { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Expuestos")]
        public int NroExpuestos { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Requisito")]
        public bool RequisitoLegal { get; set; }
        public ICollection<AplicacionVM> Intervenciones { get; set; }
    }
}