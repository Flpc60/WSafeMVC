using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities.Ppre
{
    public class ActionPlan
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "La respuesta es obligatoria")]
        [Display(Name = "Categoría acción")]
        public PlanCategories PlanCategoriy { get; set; }
        [Required(ErrorMessage = "La respuesta es obligatoria")]
        [StringLength(500, ErrorMessage = "El objetivo no puede exceder los 500 caracteres")]
        [Display(Name = "Objetivo")]
        public string Objetive { get; set; }
        [Required(ErrorMessage = "La respuesta es obligatoria")]
        [Display(Name = "Responsable")]
        public int TrabajadorID { get; set; }
        [Required(ErrorMessage = "La respuesta es obligatoria")]
        [StringLength(500, ErrorMessage = "Los recursos no puede exceder los 500 caracteres")]
        [Display(Name = "Recursos")]
        public string Resources { get; set; }
        [Required(ErrorMessage = "La respuesta es obligatoria")]
        [StringLength(500, ErrorMessage = "Las Acciones Antes no puede exceder los 500 caracteres")]
        [Display(Name = "Acciones Antes")]
        public string Before { get; set; }
        [Required(ErrorMessage = "La respuesta es obligatoria")]
        [StringLength(500, ErrorMessage = "Las Acciones Durante no puede exceder los 500 caracteres")]
        [Display(Name = "Acciones Durante")]
        public string During { get; set; }
        [Required(ErrorMessage = "La respuesta es obligatoria")]
        [StringLength(500, ErrorMessage = "Las Acciones Después no puede exceder los 500 caracteres")]
        [Display(Name = "Después")]
        public string After { get; set; }
        public ICollection<Traceability> Traceability { get; set; }
    }
}
