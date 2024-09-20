using System.ComponentModel.DataAnnotations;
using WSafe.Domain.Data.Entities.Ppre;

namespace WSafe.Web.Models
{
    public class CreateCalificationVM
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "La categoría de amenaza es obligatoria")]
        [Display(Name = "Tipo de Amenaza")]
        public CategoryAmenazas CategoryAmenaza { get; set; }
        [Required(ErrorMessage = "El ID de la amenaza es obligatorio")]
        [Display(Name = "Amenaza")]
        public Amenaza Amenaza { get; set; }
        [Required(ErrorMessage = "La calificación es obligatoria")]
        [Display(Name = "Calificación de la Amenaza")]
        public CategoryCalifications Calification { get; set; }
    }
}
