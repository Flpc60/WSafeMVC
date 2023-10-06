namespace WSafe.Domain.Data.Entities
{
    public class AuditItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int NormaID { get; set; }
        public AuditChapters AuditChapter { get; set; }
    }
}