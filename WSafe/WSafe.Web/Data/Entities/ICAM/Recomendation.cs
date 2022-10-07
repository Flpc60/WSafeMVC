namespace WSafe.Domain.Data.Entities.ICAM
{
    public class Recomendation
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int RootCauseID { get; set; }
        public int IncidentID { get; set; }
    }
}