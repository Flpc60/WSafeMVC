using System;
using System.ComponentModel.DataAnnotations;
using WSafe.Domain.Data.Entities;

namespace WSafe.Domain.Data
{
    public class Unsafeact
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "ZONA DONDE OCURRE EVENTO")]
        public int ZonaID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ProcesoID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ActividadID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int TareaID { get; set; }
        public bool Rutinaria { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime FechaReporte { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "OBJETO DEL REPORTE")]
        public ActCategories ActCategory { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "ANTECEDENTES")]
        [MaxLength(100)]
        public string Antecedentes { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "FECHA DEL ANTECEDENTE")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime FechaAntecednte { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "EVIDENCIA FOTOGRÁFICA")]
        [MaxLength(100)]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [MaxLength(200)]
        public string Document { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [MaxLength(100)]
        public string Path { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "TIPO PELIGRO")]
        public int CategoriaPeligroID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "FACTOR RIESGO")]
        public int PeligroID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "DESCRIPCIÓN EVENTO")]
        [MaxLength(100)]
        public string ActDescription { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "PROBABLES CONSECUENCIAS")]
        public string ProbableConsecuencia { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "RECOMENDACIONES")]
        public string Recomendations { get; set; }
        public int WorkerID { get; set; }
        public int Worker1ID { get; set; }
        public int Worker2ID { get; set; }
        public int OrganizationID { get; set; }
        public int ClientID { get; set; }
    }
}
