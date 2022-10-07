using System.Collections.Generic;

namespace WSafe.Domain.Data.Entities.ICAM
{
    public class Event
    {
        public int ID { get; set; }
        public int IncidentID { get; set; }
        public string Name { get; set; }
        public IEnumerable<Reason> Reasons { get; set; }
        public CausalFactors CausalFactor { get; set; }
        public PotencialCausalFactors PotencialFactor { get; set; }
    }
}