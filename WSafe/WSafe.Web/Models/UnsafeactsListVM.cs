using System;
using System.ComponentModel.DataAnnotations;

namespace WSafe.Web.Models
{
    public class UnsafeactsListVM
    {
        public int ID { get; set; }
        [Display(Name = "ZONA")]
        public string Zona { get; set; }
        [Display(Name = "PROCESO")]
        public string Proceso { get; set; }
        [Display(Name = "ACTIVIDAD")]
        public string Actividad { get; set; }
        [Display(Name = "TAREA")]
        public string Tarea { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "OBJETO")]
        public string ActCategory { get; set; }
        [Display(Name = "ANTECEDENTES")]
        [MaxLength(100)]
        public string Antecedentes { get; set; }
        [Display(Name = "FECHA DEL ANTECEDENTE")]
        public DateTime FechaAntecedente { get; set; }
        [Display(Name = "TIPO PELIGRO")]
        public string CategoriaPeligro { get; set; }
        [Display(Name = "FACTOR RIESGO")]
        public string Peligro { get; set; }
        [Display(Name = "DESCRIPCIÓN EVENTO")]
        [MaxLength(100)]
        public string ActDescription { get; set; }
        [Display(Name = "PROBABLES CONSECUENCIAS")]
        public string ProbableConsecuencia { get; set; }
        [Display(Name = "RECOMENDACIONES")]
        public string Recomendations { get; set; }
        [Display(Name = "NOMBRE QUIEN IDENTIFICA")]
        public string Worker { get; set; }
        public int MovimientID { get; set; }
        public int OrganizationID { get; set; }
        public int ClientID { get; set; }
        [Display(Name = "SUBIR EVIENCIA")]
        [MaxLength(100)]
        public string FileName { get; set; }
    }
}