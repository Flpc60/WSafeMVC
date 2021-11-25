using System;

namespace WSafe.Domain.Data.Entities
{
    public class Aplicacion
    {
        public int ID { get; set; }
        public int RiesgoID { get; set; }
        public Control Control { get; set; }
        public Trabajador Trabajador { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public decimal Presupuesto { get; set; }
        public CategoriasEfectividad CategoriaEfectividad { get; set; }
        public string Observaciones { get; set; }
    }
}