using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSafe.Domain.Data.Entities
{
    public class Peligro
    {
        public int ID { get; set; }
        public int CategoriaPeligroID { get; set; }
        public CategoriaPeligro CategoriaPeligro { get; set; }
        public string Descripcion { get; set; }
    }
}