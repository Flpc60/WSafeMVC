using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class Recomendation
    {
        public int ID { get; set; }
        public DateTime RecomendationDate { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int TrabajadorID { get; set; }
        public Contingencias Contingencia { get; set; }
        public TiposReintegro TipoReintegro { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CargoID { get; set; }
        [MaxLength(6)]
        public string CodigoDX { get; set; }
        public DateTime EmisionDate { get; set; }
        [MaxLength(20)]
        public string Entity { get; set; }
        public DateTime ReceptionDate { get; set; }
        public int PatologyID { get; set; }
        public short Duration { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime FinalDate { get; set; }
        public bool Compromise { get; set; }
        [MaxLength(200)]
        public string Controls { get; set; }
        [MaxLength(100)]
        public string EPP { get; set; }
        [MaxLength(200)]
        public string Tasks { get; set; }
        public bool WorkerCompromise { get; set; }
        [MaxLength(200)]
        public string Observation { get; set; }
        public int CoordinadorID { get; set; }
        public ICollection<SigueRecomendation> Seguimients { get; set; }
    }
}