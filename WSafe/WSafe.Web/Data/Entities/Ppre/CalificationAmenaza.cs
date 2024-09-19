using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities.Ppre
{
    public class CalificationAmenaza
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "La categoría de amenaza es obligatoria")]
        [Display(Name = "Categoría de Amenaza")]
        public CategoryAmenazas CategoryAmenaza { get; set; }

        [Required(ErrorMessage = "El ID de la amenaza es obligatorio")]
        [Display(Name = "ID de la Amenaza")]
        public int AmenzaID { get; set; }
        [Required(ErrorMessage = "La amenaza es obligatoria")]
        [Display(Name = "Amenaza Asociada")]
        public Amenaza Amenaza { get; set; }

        [Required(ErrorMessage = "La calificación es obligatoria")]
        [Display(Name = "Calificación de la Amenaza")]
        public CategoryCalifications Calification { get; set; }
        public int OrganizationID { get; set; }
        public int ClientID { get; set; }
        public int UserID { get; set; }
    }
}
