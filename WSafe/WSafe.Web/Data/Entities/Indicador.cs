using WSafe.Web.Data.Entities.Incidentes;

namespace WSafe.Domain.Data.Entities
{
    public class Indicador
    {
        public int ID { get; set; }
        public IndicadorMes IndicadorMes { get; set; }
        public int Anno { get; set; }
        public TiposIndicador TipoIndicador { get; set; }
        public decimal Resultado { get; set; }
        public int Numerador { get; set; }
        public int Denominador { get; set; }
    }
}