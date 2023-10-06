using System;
using System.Collections.Generic;

namespace WSafe.Domain.Data.Entities
{
    public class Audit
    {
        public int ID { get; set; }
        public DateTime AuditDate { get; set; }
        public string Process { get; set; }
        public int WorkerID { get; set; }
        public ICollection<AuditAction> AuditActions { get; set; }
        public ICollection<AuditedResult> AuditedResults { get; set; }
        public string AuditDocument { get; set; }
    }
}