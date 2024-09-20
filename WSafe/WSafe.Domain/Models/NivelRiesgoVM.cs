using System.ComponentModel.DataAnnotations;

namespace WSafe.Web.Models
{
    public class NivelRiesgoVM
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Clase de Amenaza")]
        public string CategoryAmenaza { get; set; }
        [Display(Name = "Amenaza")]
        public string Amenaza { get; set; }
        [Display(Name = "Calificación (A)")]
        public string Calification { get; set; }
        [Display(Name = "Personas (P)")]
        public string Personas { get; set; }
        [Display(Name = "Recursos (R)")]
        public string Resursos { get; set; }
        [Display(Name = "Sistemas y procesps (S)")]
        public string Sistemas { get; set; }
        public string ImageCalification { get; set; }
        public string RiskCalification { get; set; }
    }
}