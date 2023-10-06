using System.Collections.Generic;
using WSafe.Domain.Data.Entities;

namespace WSafe.Web.Models
{
    public class AuditListVM
    {
        public int ID { get; set; }
        public string AuditDate { get; set; }
        public int Process { get; set; }
        public string Responsable { get; set; }
        public ICollection<Auditer> Auditers { get; set; }
        public ICollection<AuditedResult> AuditedResult { get; set; }
    }
}