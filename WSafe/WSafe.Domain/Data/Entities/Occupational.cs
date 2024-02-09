using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WSafe.Web.Data.Entities;

namespace WSafe.Domain.Data.Entities
{
    public class Occupational
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ExaminationDate { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int TrabajadorID { get; set; }
        [StringLength(500, ErrorMessage = "El campo 'Recomendaciones' no puede tener más de 500 caracteres.")]
        public string Recomendations { get; set; }
        [Range(50, 250, ErrorMessage = "La 'Talla' debe estar entre 50 y 210 centímetros.")]
        public short Talla { get; set; }
        [Range(10, 500, ErrorMessage = "El 'Peso' debe estar entre 10 y 120 kilogramos.")]
        public short Peso { get; set; }
        public ExaminationTypes ExaminationType { get; set; }
        public MedicalRecomendations MedicalRecomendation { get; set; }
        public ICollection<SigueOccupational> SigueOccupational { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int OrganizationID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ClientID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int UserID { get; set; }
        [MaxLength(200)]
        public string FileName { get; set; }
    }
}