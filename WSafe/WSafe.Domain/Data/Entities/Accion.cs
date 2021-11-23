using System;
using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class Accion
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public int RiesgoID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public CategoriasAccion Categoria { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public DateTime FechaSolicitud { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public Trabajador Trabajador { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public string Observaciones { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public DateTime FechaInicial { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public DateTime FechaFinal { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public string Intervenciones { get; set; }
    }
}