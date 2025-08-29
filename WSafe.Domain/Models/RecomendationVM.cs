using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using WSafe.Domain.Data.Entities;

namespace WSafe.Domain.Models
{
    public class RecomendationVM
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "TRABAJADOR")]
        public int WorkerID { get; set; }
        public IEnumerable<SelectListItem> Workers { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "CONTINGENCIA")]
        public Contingencias Contingencia { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "REINTEGRO")]
        public TiposReintegro TipoReintegro { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "NUEVO CARGO")]
        public int CargoID { get; set; }
        public IEnumerable<SelectListItem> Cargos { get; set; }
        [Display(Name = "DIAGNÓSTICO")]
        public int PatologyID { get; set; }
        public IEnumerable<SelectListItem> Patologies { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "FECHA EMISIÓN")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EmisionDate { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "EMITE")]
        public Emissions Emision { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "ENTIDAD")]
        [MaxLength(20)]
        public string Entity { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "FECHA RECEPCIÓN")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReceptionDate { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "DESCRIPCIÓN")]
        [MaxLength(200)]
        public string Description { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "TIPO")]
        public RecomendationTypes Type { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "DURACIÓN")]
        public short Duration { get; set; }
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
        [Display(Name = "COMPROMISO EMPRESA")]
        public bool Compromise { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "CONTROLES ADMINISTRATIVOS")]
        [MaxLength(200)]
        public string Controls { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "INVESTIGACIÓN CONTINGENCIA")]
        [MaxLength(200)]
        public string Investigation { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "DESCRPCIÓN DE EPP")]
        [MaxLength(100)]
        public string EPP { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "DESCRPCIÓN DE TAREAS")]
        [MaxLength(200)]
        public string Tasks { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "COMPROMISO TRABAJADOR")]
        public bool WorkerCompromise { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "OBSERVACIONES")]
        [MaxLength(200)]
        public string Observation { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "COORDINADOR AREA")]
        public int CoordinadorID { get; set; }
        public IEnumerable<SelectListItem> Coordinators { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int OrganizationID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ClientID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int UserID { get; set; }
    }
}