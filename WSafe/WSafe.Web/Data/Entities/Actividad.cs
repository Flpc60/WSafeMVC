using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class Actividad
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public string Descripcion { get; set; }
    }
}