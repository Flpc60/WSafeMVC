namespace WSafe.Domain.Data.Entities.ICAM
{
    public class CausalAnalice
    {
        public int ID { get; set; }
        public int EventID { get; set; }
        public int IncidentID { get; set; }
        public string Name { get; set; }
        public CausalFactors CausalFactor { get; set; }
        public PotencialCausalFactors PotencialFactor { get; set; }
    }
}