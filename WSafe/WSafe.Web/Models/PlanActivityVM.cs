using System;
using System.ComponentModel.DataAnnotations;
using WSafe.Domain.Data.Entities;

namespace WSafe.Web.Models
{
    public class PlanActivityVM
    {
        public int ID { get; set; }
        public int EvaluationID { get; set; }
        public int NormaID { get; set; }
        public int TrabajadorID { get; set; }
        public string Responsable { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "FECHA CUMPLIMIENTO")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaFinal { get; set; }
        public string FechaCumplimiento { get; set; }
        public string Activity { get; set; }
        public RecursosCategory Recurso { get; set; }
        public string TxtRecurso { get; set; }
        public ActionCategories ActionCategory { get; set; }
        public string TxtActionCategory { get; set; }
        public string Observation { get; set; }
        public string Ciclo { get; set; }
        public string Item { get; set; }
        public string Name { get; set; }
        public string Fundamentos { get; set; }
    }
}