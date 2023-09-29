using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class Recomendation
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "FECHA")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RecomendationDate { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int TrabajadorID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public Contingencias Contingencia { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public TiposReintegro TipoReintegro { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CargoID { get; set; }
        public int PatologyID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "FECHA EMISIÓN")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EmisionDate { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public Emissions Emision { get; set; }
        public string Entity { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "FECHA RECEPCIÓN")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReceptionDate { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(200)]
        public string Description { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public RecomendationTypes Type { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
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
        public bool Compromise { get; set; }
        public string Controls { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(200)]
        public string Investigation { get; set; }
        public string EPP { get; set; }
        public string Tasks { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public bool WorkerCompromise { get; set; }
        public string Observation { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CoordinadorID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int OrganizationID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ClientID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int UserID { get; set; }
        public ICollection<SigueRecomendation> Seguimients { get; set; }
    }
}