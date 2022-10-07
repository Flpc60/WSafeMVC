namespace WSafe.Domain.Data.Entities.ICAM
{
    public class ChangeAnalice
    {
        public int ID { get; set; }
        public int EventID { get; set; }
        public int IncidentID { get; set; }
        public ChangeCategories ChangeCategory { get; set; }
    }
}