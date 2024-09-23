using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Models
{
    public class CalificationAmenazaVM
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Clase de Amenaza")]
        public string CategoryAmenaza { get; set; }
        [Display(Name = "Nombre de la Amenaza")]
        public string Name { get; set; }
        [Display(Name = "Descripción de la Amenaza")]
        public string Description { get; set; }
        [Display(Name = "Origen")]
        public string Origen { get; set; }
        [Display(Name = "Calificación")]
        public string Calification { get; set; }
    }
}
