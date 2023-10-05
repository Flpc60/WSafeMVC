using System.Collections.Generic;
using WSafe.Domain.Data.Entities;

namespace WSafe.Web.Models
{
    public class _DetailsRecomendationVM
    {
        public int ID { get; set; }
        public string Formato { get; set; }
        public string Estandar { get; set; }
        public string Titulo { get; set; }
        public string Fecha { get; set; }
        public string Version { get; set; }
        public string FechaRecomendation { get; set; }
        public string Cargo { get; set; }
        public string WorkerFull { get; set; }
        public string WorkArea { get; set; }
        public string Contingencia { get; set; }
        public string TipoReintegro { get; set; }
        public string NewPosition { get; set; }
        public string Patology { get; set; }
        public string EmisionDate { get; set; }
        public string Emision { get; set; }
        public string Entity { get; set; }
        public string ReceptionDate { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public short Duration { get; set; }
        public string InitialDate { get; set; }
        public string FinalDate { get; set; }
        public string Compromise { get; set; }
        public string Controls { get; set; }
        public string Investigation { get; set; }
        public string EPP { get; set; }
        public string Tasks { get; set; }
        public string WorkerCompromise { get; set; }
        public string Observation { get; set; }
        public string Coordinador { get; set; }    
        public ICollection<SigueRecomendation> Seguimients { get; set; }
    }
}