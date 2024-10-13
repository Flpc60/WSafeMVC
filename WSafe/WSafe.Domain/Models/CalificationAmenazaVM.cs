using System.ComponentModel.DataAnnotations;
using WSafe.Domain.Data.Entities.Ppre;

namespace WSafe.Domain.Models
{
    public class CalificationAmenazaVM
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Clase de Amenaza")]
        public string CategoryAmenaza { get; set; }
        [Display(Name = "Nombre de la Amenaza")]
        public string Name { get; set; }
        [Display(Name = "Descripción de la Amenaza")]
        public string Description { get; set; }
        [Display(Name = "Origen")]
        public OrigenAmenazas OrigenAmenaza { get; set; }
        [Display(Name = "Calificación")]
        public CategoryCalifications Calification { get; set; }
    }
}
