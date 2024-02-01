using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WSafe.Web.Data.Entities;

namespace WSafe.Web.Models
{
    public class MedicalRecomendationVM
    {
        public int ID { get; set; }
        [Display(Name = "FECHA EXÁMEN")]
        public string ExaminationDate { get; set; }
        [Display(Name = "TRABAJADOR")]
        public string Trabajador { get; set; }
        [Display(Name = "TALLA (Cms)")]
        public string Talla { get; set; }
        [Display(Name = "PESO (Kgs)")]
        public string Peso { get; set; }
        [Display(Name = "TIPO EXÁMEN")]
        public string ExaminationType { get; set; }
        [Display(Name = "RECOMENDACIONES")]
        public string Recomendations { get; set; }
        [Display(Name = "TIPO RECOMENDACIÓN")]
        public string MedicalRecomendation { get; set; }
        public IEnumerable<SigueOccupational> SigueOccupational { get; set; }
        [Display(Name = "DOCUMENTO")]
        public string Document { get; set; }
        [Display(Name = "EDAD")]
        public string Age { get; set; }
        [Display(Name = "OCUPACIÓN")]
        public string Cargo { get; set; }
    }
}