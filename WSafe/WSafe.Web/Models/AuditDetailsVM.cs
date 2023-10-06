using System.Collections.Generic;
using WSafe.Domain.Data.Entities;

namespace WSafe.Domain.Models
{
    public class AuditDetailsVM
    {
        public int ID { get; set; }
        public string AuditDate { get; set; }
        public string Process { get; set; }
        public string Responsable { get; set; }
        public ICollection<Auditer> Auditers { get; set; }
        public ICollection<AuditedResult> AuditedResult { get; set; }
    }
}