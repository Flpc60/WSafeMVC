using System.Collections.Generic;

namespace WSafe.Domain.Data.Entities
{
    public class Consecuencia
    {
        public int ID { get; set; }
        public int CategoriaPeligroID { get; set; }
        public int PeligroID { get; set; }
        public Peligro Peligro { get; set; }
        public string Nombre { get; set; }
    }
}