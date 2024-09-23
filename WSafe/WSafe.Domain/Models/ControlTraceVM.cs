using System;

namespace WSafe.Domain.Models
{
    public class ControlTraceVM
    {
        public int ID { get; set; }
        public string MedidaAct { get; set; }
        public string MedidaAnt { get; set; }
        public DateTime Fecha { get; set; }
        public string Efectividad { get; set; }
        public string Observaciones { get; set; }
        public string Responsable { get; set; }
        public string GenerateAction { get; set; }
        public string Finality { get; set; }
        public string AplicationCategory { get; set; }
     }
}