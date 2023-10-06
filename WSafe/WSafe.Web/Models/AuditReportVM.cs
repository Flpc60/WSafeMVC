using System.Collections.Generic;
using WSafe.Domain.Data.Entities;

namespace WSafe.Web.Models
{
    public class AuditReportVM
    {
        public int ID { get; set; }
        public string AuditDate { get; set; }
        public string Responsable { get; set; }
        public string Process { get; set; }
        public ICollection<Auditer> Auditers { get; set; }
        public ICollection<AuditAction> AuditActions { get; set; }
    }
}