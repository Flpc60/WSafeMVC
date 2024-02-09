using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using WSafe.Domain.Data.Entities;
using WSafe.Web.Data.Entities;

namespace WSafe.Web.Models
{
    public class CreateOccupationalVM
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "FECHA")]
        public DateTime ExaminationDate { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "TRABAJADOR")]
        public int TrabajadorID { get; set; }
        public IEnumerable<SelectListItem> Workers { get; set; }
        [Range(50, 250, ErrorMessage = "La 'Talla' debe estar entre 50 y 210 centímetros.")]
        [Display(Name = "TALLA")]
        public short Talla { get; set; }
        [Range(10, 500, ErrorMessage = "El 'Peso' debe estar entre 10 y 120 kilogramos.")]
        [Display(Name = "PESO")]
        public short Peso { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "TIPO EXÁMEN")]
        public ExaminationTypes ExaminationType { get; set; }
        [StringLength(500, ErrorMessage = "El campo 'Recomendaciones' no puede tener más de 500 caracteres.")]
        [Display(Name = "RECOMENDACIONES")]
        public string Recomendations { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "TIPO RECOMENDACIÓN")]
        public MedicalRecomendations MedicalRecomendation { get; set; }
        public ICollection<SigueOccupational> SigueOccupational { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int OrganizationID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ClientID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int UserID { get; set; }
        [Display(Name = "SUBIR EVIENCIA")]
        [MaxLength(200)]
        public string FileName { get; set; }
    }
}