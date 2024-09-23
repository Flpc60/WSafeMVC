namespace WSafe.Domain.Models
{
    public class CalificationVM
    {
        public int ID { get; set; }
        public int EvaluationID { get; set; }
        public int NormaID { get; set; }
        public string Ciclo { get; set; }
        public string Item { get; set; }
        public string Name { get; set; }
        public decimal Valor { get; set; }
        public string Standard { get; set; }
        public bool Cumple { get; set; }
        public bool NoCumple { get; set; }
        public bool Justify { get; set; }
        public bool NoJustify { get; set; }
        public decimal Valoration { get; set; }
        public string Observation { get; set; }
        public string Verification { get; set; }
        public string TxtCumple { get; set; }
        public string TxtNoCumple { get; set; }
        public string TxtJustify { get; set; }
        public string TxtNoJustify { get; set; }
    }
}