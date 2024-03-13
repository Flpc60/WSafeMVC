using System;
using System.ComponentModel.DataAnnotations;

namespace WSafe.Web.Data.Entities
{
    public class SigueOccupational
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "FECHA")]
        public DateTime SigueDate { get; set; }
        [Required(ErrorMessage = "El campo 'Resultado' es obligatorio.")]
        [StringLength(200, ErrorMessage = "El campo 'Resultado' no puede tener más de 200 caracteres.")]
        [Display(Name = "REULTADO")]
        public string Resultado { get; set; }
        [StringLength(500, ErrorMessage = "El campo 'Recomendaciones' no puede tener más de 500 caracteres.")]
        [Display(Name = "RECOMENDACIONES")]
        public string Recomendations { get; set; }
        [Required(ErrorMessage = "Se requiere el ID de la evaluación médica asociada.")]
        public int OccupationalID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int TrabajadorID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int OrganizationID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ClientID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int UserID { get; set; }
    }
}
