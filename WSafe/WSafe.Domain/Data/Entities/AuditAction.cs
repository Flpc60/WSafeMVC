using System;

namespace WSafe.Domain.Data.Entities
{
    public class AuditAction
    {
        public int ID { get; set; }
        public int AuditID { get; set; }
        public string NoConformance { get; set; }
        public string Cause { get; set; }
        public string CorrectiveAction { get; set; }
        public int WorkerID { get; set; }
        public DateTime ExecutionDate { get; set; }
        public AuditStates AuditState { get; set; }
    }
}