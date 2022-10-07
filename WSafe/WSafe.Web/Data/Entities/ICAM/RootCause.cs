using System.Collections.Generic;

namespace WSafe.Domain.Data.Entities.ICAM
{
    public class RootCause
    {
        public int ID { get; set; }
        public int CausalAnaliceID { get; set; }
        public string Name { get; set; }
        public IEnumerable<Reason> Reasons { get; set; }
        public IEnumerable<Recomendation> Recomendations { get; set; }
        public int IncidentID { get; set; }
    }
}