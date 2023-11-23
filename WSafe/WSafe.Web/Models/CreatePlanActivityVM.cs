using System.ComponentModel.DataAnnotations;
using System;
using WSafe.Domain.Data.Entities;
using System.Collections.Generic;
using System.Web.Mvc;

namespace WSafe.Web.Models
{
    public class CreatePlanActivityVM
    {
        public int ID { get; set; }
        public int EvaluationID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "ESTÁNDAR MÍNIMO")]
        public int NormaID { get; set; }
        public IEnumerable<SelectListItem> Normas { get; set; }
        public string Ciclo { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "ITEM")]
        [MaxLength(6)]
        public string Item { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "ESTÁNDAR")]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "FECHA CUMPLIMIENTO")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime FechaFinal { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "ACTIVIDAD")]
        [MaxLength(100)]
        public string Activity { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "RESPONSABLE")]
        public int TrabajadorID { get; set; }
        public IEnumerable<SelectListItem> Workers { get; set; }
        [Display(Name = "FINANCIEROS")]
        public bool Financieros { get; set; }
        [Display(Name = "ADMINISTRATIVO")]
        public bool Administrativos { get; set; }
        [Display(Name = "TÉCNICOS")]
        public bool Tecnicos { get; set; }
        [Display(Name = "HUMANOS")]
        public bool Humanos { get; set; }
        public ActionCategories ActionCategory { get; set; }
        [MaxLength(100)]
        [Display(Name = "OBSERVACIONES")]
        public string Observation { get; set; }
        [MaxLength(100)]
        [Display(Name = "FUNDAMENTOS")]
        public string Fundamentos { get; set; }
        public int AuditID { get; set; }
        public short Year { get; set; }
        public int OrganizationID { get; set; }
        public int ClientID { get; set; }
        public int UserID { get; set; }
        [MaxLength(100)]
        [Display(Name = "ENTREGABLE")]
        public string Entregable { get; set; }
        public StatesActivity StateActivity { get; set; }
        public StatesCronogram StateCronogram { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(typeof(short), "1", "9999", ErrorMessage = "Por favor ingrese un número de actividades programadas válido.")]
        public short Programed { get; set; }
    }
}