using System;
using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Models
{
    public class EvaluationVM
    {
        public int ID { get; set; }
        public int OrganizationID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "FECHA EVALUACIÓN")]
        public string FechaEvaluation { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "CUMPLE")]
        public int Cumple { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "NO CUMPLE")]
        public int NoCumple { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "NO APLICA")]
        public int NoAplica { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "RESULTADO ESTANDARES")]
        public decimal StandarsResult { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "RESULTADO APLICA")]
        public decimal AplicationsResult { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "ACTIVIDADES")]
        public int Activitys { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "EJECUTADAS")]
        public int Ejecutadas { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "% AVANCE")]
        public decimal Avance { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "VALORACIÓN")]
        public string Category { get; set; }
        public string Color { get; set; }
    }
}