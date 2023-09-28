using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using WSafe.Domain.Data.Entities;

namespace WSafe.Web.Models
{
    public class RecomendationVM
    {
        public int ID { get; set; }
        public DateTime RecomendationDate { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "NOMBRE TRABAJADOR")]
        public int WorkerID { get; set; }
        public IEnumerable<SelectListItem> Workers { get; set; }
        [Display(Name = "CONTINGENCIA")]
        public Contingencias Contingencia { get; set; }
        [Display(Name = "TIPO REINTEGRO")]
        public TiposReintegro TipoReintegro { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "CARGO NUEVO")]
        public int CargoID { get; set; }
        public IEnumerable<SelectListItem> Cargos { get; set; }
        [Display(Name = "ENFERMEDAD")]
        public int PatologyID { get; set; }
        public IEnumerable<SelectListItem> Patologies { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "FECHA EMISIÓN")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EmisionDate { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "EMITIDA POR")]
        public Emissions Emision { get; set; }
        [Display(Name = "ENTIDAD QUE EXPIDE")]
        [MaxLength(20)]
        public string Entity { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "FECHA RECEPCIÓN")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReceptionDate { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "FECHA INICIAL")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime InitialDate { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "FECHA FINAL")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FinalDate { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "DURACIÓN")]
        public short Duration { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "COMPROMISO DE LA EMPRESA")]
        public bool Compromise { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "CONTROLES ADMINISTRATIVOS")]
        [MaxLength(200)]
        public string Controls { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(100)]
        public string EPP { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "DESCRPCIÓN DE TAREAS")]
        [MaxLength(200)]
        public string Tasks { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "COMPROMISO DE LA EMPRESA")]
        public bool WorkerCompromise { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "OBSERVACIONES")]
        [MaxLength(200)]
        public string Observation { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "COORDINADOR AREA")]
        public int CoordinadorID { get; set; }
        public IEnumerable<SelectListItem> Coordinadores { get; set; }
    }
}