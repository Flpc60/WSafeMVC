using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class Peligro
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public CategoriaPeligro CategoriaPeligro { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Descripcion { get; set; }
    }
}