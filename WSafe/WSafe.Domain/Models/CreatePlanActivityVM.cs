using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using WSafe.Domain.Data.Entities;

namespace WSafe.Web.Models
{
    public class CreatePlanActivityVM
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "ESTÁNDAR MÍNIMO")]
        public int NormaID { get; set; }
        public IEnumerable<SelectListItem> Normas { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "ACTIVIDAD")]
        [MaxLength(100)]
        public string Activity { get; set; }
        [MaxLength(200)]
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
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "RESPONSABLE")]
        public int TrabajadorID { get; set; }
        public IEnumerable<SelectListItem> Workers { get; set; }
        [MaxLength(100)]
        [Display(Name = "OBSERVACIONES")]
        public string Observation { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "FECHA INICIAL")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime InitialDate { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "FECHA CUMPLIMIENTO")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaFinal { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(typeof(short), "1", "9999", ErrorMessage = "Por favor ingrese un número de actividades programadas válido.")]
        [Display(Name = "PROGRAMADAS")]
        public short Programed { get; set; }
        [Display(Name = "FRECUENCIA")]
        public ActivitiesFrequency ActivityFrequency { get; set; }
        [Display(Name = "ESTADO")]
        public StatesActivity StateActivity { get; set; }
        [Display(Name = "ACCIÓN")]
        public ActionCategories ActionCategory { get; set; }
        [Display(Name = "CRONOGRAMA")]
        public StatesCronogram StateCronogram { get; set; }
        public int EvaluationID { get; set; }
        public int AuditID { get; set; }
        public int OrganizationID { get; set; }
        public int ClientID { get; set; }
        public int UserID { get; set; }
        [Display(Name = "SUBIR EVIENCIA")]
        [MaxLength(200)]
        public string FileName { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "FECHA")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateSigue { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(typeof(short), "1", "9999", ErrorMessage = "Por favor ingrese un número de actividades programadas válido.")]
        [Display(Name = "EJECUTADAS")]
        public short Executed { get; set; }
        public string TextDateSigue { get; set; }
    }
}