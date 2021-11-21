using System;
namespace WSafe.Domain.Data.Entities
{
    class Accion
    {
        public int ID { get; set; }
        public int RiesgoID { get; set; }
        public string Categoria { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public int TrabajadorID { get; set; }
        public string Descripcion { get; set; }
        public string Observaciones { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public string Intervenciones { get; set; }
    }
}