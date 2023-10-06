namespace WSafe.Domain.Data.Entities
{
    public class AuditedResult
    {
        public int ID { get; set; }
        public int AuditID { get; set; }
        public int AuditItemID { get; set; }
        public AuditCalifications Result { get; set; }
    }
}