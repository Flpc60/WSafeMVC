using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities.Ppre
{
    public class Pon
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "La respuesta es obligatoria")]
        public int ActionPlanID { get; set; }
        [Required(ErrorMessage = "La respuesta es obligatoria")]
        [Display(Name = "Responsable")]
        public int TrabajadorID { get; set; }
        [Required(ErrorMessage = "La respuesta es obligatoria")]
        [StringLength(500, ErrorMessage = "Los recursos no puede exceder los 500 caracteres")]
        [Display(Name = "Recursos")]
        public string Resources { get; set; }
        [Required(ErrorMessage = "La respuesta es obligatoria")]
        [StringLength(100, ErrorMessage = "El documento no puede exceder los 100 caracteres")]
        [Display(Name = "Documento o registro")]
        public string Document { get; set; }
    }
}