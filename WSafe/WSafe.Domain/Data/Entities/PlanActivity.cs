using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class PlanActivity
    {
        public int ID { get; set; }
        public int EvaluationID { get; set; }
        public int NormaID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "FECHA CUMPLIMIENTO")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime FechaFinal { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "ACTIVIDAD")]
        [MaxLength(100)]
        public string Activity { get; set; }
        [MaxLength(100)]
        [Display(Name = "ENTREGABLES")]
        public string Entregables { get; set; }
        [Display(Name = "FINANCIEROS")]
        public bool Financieros { get; set; }
        [Display(Name = "ADMINISTRATIVO")]
        public bool Administrativos { get; set; }
        [Display(Name = "TÉCNICOS")]
        public bool Tecnicos { get; set; }
        [Display(Name = "HUMANOS")]
        public bool Humanos { get; set; }
        [Display(Name = "TRABAJADOR RESPONSABLE")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int TrabajadorID { get; set; }
        public ActionCategories ActionCategory { get; set; }
        [MaxLength(100)]
        [Display(Name = "OBSERVACIONES")]
        public string Observation { get; set; }
        public StatesActivity StateActivity { get; set; }
        [MaxLength(100)]
        [Display(Name = "FUNDAMENTOS")]
        public string Fundamentos { get; set; }
        public int AuditID { get; set; }
        public short Year { get; set; }
        public int OrganizationID { get; set; }
        public int ClientID { get; set; }
        public int UserID { get; set; }
        public StatesCronogram StateCronogram { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(typeof(short), "1", "9999", ErrorMessage = "Por favor ingrese un número de actividades programadas válido.")]
        public short Programed { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "FECHA INICIAL")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime InitialDate { get; set; }
        public ActivitiesFrequency ActivityFrequency { get; set; }
        public ICollection<SiguePlanAnual> SiguePlanAnual { get; set; }
    }
}