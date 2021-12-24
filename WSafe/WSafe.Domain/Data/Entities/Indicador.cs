namespace WSafe.Domain.Data.Entities
{
    public class Indicador
    {
        public int ID { get; set; }
        public string periodo { get; set; }
        public TiposIndicador TipoIndicador { get; set; }
        public decimal Resultado { get; set; }
    }
}