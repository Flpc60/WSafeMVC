using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities.Ppre
{
    public class Amenaza
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        [Display(Name = "Nombre de la Amenaza")]
        public string Name { get; set; }

        [StringLength(250, ErrorMessage = "La descripción no puede exceder los 250 caracteres")]
        [Display(Name = "Descripción de la Amenaza")]
        public string Description { get; set; }

        [Required(ErrorMessage = "La categoría es obligatoria")]
        [Display(Name = "Categoría de Amenaza")]
        public CategoryAmenazas CategoryAmenaza { get; set; }
        [Required(ErrorMessage = "El origen es obligatorio")]
        [Display(Name = "Origen de la amenaza")]
        public OrigenAmenazas OrigenAmenaza { get; set; }
        public int OrganizationID { get; set; }
        public int ClientID { get; set; }
        public int UserID { get; set; }
    }
}
