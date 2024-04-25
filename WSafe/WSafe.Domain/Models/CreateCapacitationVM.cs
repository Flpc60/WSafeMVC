using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using WSafe.Domain.Data.Entities;
using System.Web.Mvc;

namespace WSafe.Web.Models
{
    public class CreateCapacitationVM
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Actividad")]
        public int TrainingTopicID { get; set; }
        public IEnumerable<SelectListItem> TrainingTopics { get; set; }
        [MaxLength(200)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Objetive { get; set; }
        [MaxLength(200)]
        public string Content { get; set; }
        [MaxLength(200)]
        public string Resources { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Responsable")]
        public int TrabajadorID { get; set; }
        [Display(Name = "Estado")]
        public IEnumerable<SelectListItem> Workers { get; set; }
        public StatesCronogram StateCronogram { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(typeof(short), "1", "9999", ErrorMessage = "Por favor ingrese un número de actividades programadas válido.")]
        [Display(Name = "Programadas")]
        public short Programed { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(typeof(short), "1", "9999", ErrorMessage = "Por favor ingrese un número de actividades ejecutadas válido.")]
        [Display(Name = "Ejecutadas")]
        public short Executed { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(typeof(short), "1", "9999", ErrorMessage = "Por favor ingrese un número de trabajadores citados  válido.")]
        public short Citados { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(typeof(short), "1", "9999", ErrorMessage = "Por favor ingrese un número de trabajadores capacitados válido.")]
        public short Capacitados { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(typeof(short), "1", "9999", ErrorMessage = "Por favor ingrese un número de trabajadores evaluados válido.")]
        public short Evaluados { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha inicial")]
        public DateTime InitialDate { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha final")]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Frecuencia")]
        public ActivitiesFrequency ActivityFrequency { get; set; }
        public ICollection<Schedule> Shedule { get; set; }
        public int OrganizationID { get; set; }
        public int ClientID { get; set; }
        public int UserID { get; set; }
    }
}