namespace WSafe.Domain.Data.Entities
{
    public class Peligro
    {
        public int ID { get; set; }
        public int CategoriaPeligroID { get; set; }
        public CategoriaPeligro CategoriaPeligro { get; set; }
        public string Nombre { get; set; }
    }
}