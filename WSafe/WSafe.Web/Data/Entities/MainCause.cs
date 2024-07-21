namespace WSafe.Domain.Data.Entities
{
    public class MainCause
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int OrganizationID { get; set; }
        public int ClientID { get; set; }
        public int UserID { get; set; }
    }
}