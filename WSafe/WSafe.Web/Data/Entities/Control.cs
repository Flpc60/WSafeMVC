using System.Collections.Generic;

namespace WSafe.Domain.Data.Entities
{
    public class Control
    {
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public int ID { get; set; }
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Codigo { get; set; }
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Finalidad { get; set; }
        [Display(Name = "Descripción Url")]
        public string DescripcionUrl { get; set; }
        public string DescripcionPath => string.IsNullOrEmpty(DescripcionUrl)
            ? null
            : $"https://wsafe.azurewebsites.net{DescripcionUrl.Substring(1)}";

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public decimal Presupuesto { get; set; }
        public string Aplicacion { get; set; }
        public ICollection<Traza> Trazas { get; set; }
    }
}