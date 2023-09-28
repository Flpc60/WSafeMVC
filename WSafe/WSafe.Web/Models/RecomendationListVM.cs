using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WSafe.Domain.Data.Entities;

namespace WSafe.Web.Models
{
    public class RecomendationListVM
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "NOMBRE TRABAJADOR")]
        public string Trabajador { get; set; }
        [Display(Name = "CONTINGENCIA")]
        public string Contingencia { get; set; }
        [Display(Name = "TIPO REINTEGRO")]
        public string TipoReintegro { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "CARGO NUEVO")]
        public string Cargo { get; set; }
        [Display(Name = "ENFERMEDAD")]
        public string Patology { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "FECHA EMISIÓN")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EmisionDate { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "EMITIDA POR")]
        public string Emision { get; set; }
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
        public string Coordinador { get; set; }
        public ICollection<SigueRecomendation> Seguimients { get; set; }
    }
}