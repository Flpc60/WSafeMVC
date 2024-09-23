using System.Collections.Generic;
using WSafe.Domain.Data.Entities;

namespace WSafe.Domain.Models
{
    public class _DetailsAccionVM
    {
        public int ID { get; set; }
        public string Formato { get; set; }
        public string Estandar { get; set; }
        public string Titulo { get; set; }
        public string Fecha { get; set; }
        public string Version { get; set; }
        public string FechaSolicitud { get; set; }
        public CategoriasAccion Categoria { get; set; }
        public string Responsable { get; set; }
        public string Cargo { get; set; }
        public string Proceso { get; set; }
        public string FuenteAccion { get; set; }
        public string Descripcion { get; set; }
        public CategoriasEfectividad EficaciaAntes { get; set; }
        public CategoriasEfectividad EficaciaDespues { get; set; }
        public string FechaCierre { get; set; }
        public bool Efectiva { get; set; }
        public ICollection<PlanAction> Planes { get; set; }
        public ICollection<SeguimientoAccion> Seguimientos { get; set; }
        public ActionCategories ActionCategory { get; set; }
        public string MainCause1 { get; set; }
        public string MainCause2 { get; set; }
        public string MainCause3 { get; set; }
        public string MainCause4 { get; set; }
        public string MainCause5 { get; set; }
    }
}