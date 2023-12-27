using System;
using System.ComponentModel.DataAnnotations;
using WSafe.Domain.Data.Entities;

namespace WSafe.Web.Models
{
    public class PlanAccionVM
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int AccionID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha inicial")]
        public string FechaInicial { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha final")]
        public string FechaFinal { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Causa que originó la acción")]
        public CategoriasCausa Causa { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Causa que originó la acción")]
        public string Categoria { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Plan acción")]
        public string Accion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Responsable")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un trabajador.")]
        public int TrabajadorID { get; set; }
        [MaxLength(100)]
        public string Responsable { get; set; }
        [Display(Name = "Acción prioritaria")]
        public bool Prioritaria { get; set; }
        [Display(Name = "Costos ejecución")]
        public decimal Costos { get; set; }
        public ActionCategories ActionCategory { get; set; }
        public int EvaluationID { get; set; }
        public string FechaActivity { get; set; }
        public int NormaID { get; set; }
        [MaxLength(100)]
        public string Observation { get; set; }
        public int OrganizationID { get; set; }
        public int ClientID { get; set; }
        public int UserID { get; set; }
    }
}