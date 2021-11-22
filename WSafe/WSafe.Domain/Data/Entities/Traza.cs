using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSafe.Domain.Data.Entities
{
    public class Traza
    {
        public int ID { get; set; }
        public int RiesgoID { get; set; }
        public int ControlID { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public int Efectividad { get; set; }
        public decimal Presupuesto { get; set; }
    }
}