using System;
using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class SigueRecomendation
    {
        public int ID { get; set; }
        public int RecomendationID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public DateTime Seguimient { get; set; }
        [MaxLength(200)]
        public string SupervisorConcept { get; set; }
        [MaxLength(200)]
        public string WorkerConcept { get; set; }
        [MaxLength(200)]
        public string SSTConcept { get; set; }
        [MaxLength(200)]
        public string Observations { get; set; }
        public DateTime NewSeguimient { get; set; }
    }
}