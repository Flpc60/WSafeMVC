namespace WSafe.Domain.Data.Entities
{
    public class Calification
    {
        public int ID { get; set; }
        public int EvaluationID { get; set; }
        public int NormaID { get; set; }
        public bool Cumple { get; set; }
        public bool Justifica { get; set; }
        public decimal Valoration { get; set; }
        public string Observation { get; set; }
    }
}