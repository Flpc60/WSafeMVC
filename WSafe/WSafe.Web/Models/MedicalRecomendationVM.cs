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
        [Display(Name = "TALLA")]
        public int Talla { get; set; }
        [Display(Name = "PESO")]
        public int Peso { get; set; }
        [Display(Name = "TIPO EXÁMEN")]
        public string ExaminationType { get; set; }
        [Display(Name = "RECOMENDACIONES")]
        public string Recomendations { get; set; }
        [Display(Name = "TIPO RECOMENDACIÓN")]
        public string MedicalRecomendation { get; set; }
        public IEnumerable<SigueOccupational> SigueOccupational { get; set; }
    }
}