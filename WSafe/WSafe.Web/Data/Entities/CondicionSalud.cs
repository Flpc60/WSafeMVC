using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class CondicionSalud
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ID { get; set; }
        [Display(Name = "DOCUMENTO")]
        [MaxLength(20)]
        public string Docuemento { get; set; }
        public bool OperaEquipos { get; set; }
        [Display(Name = "ENFERMEDAD")]
        [MaxLength(50)]
        public string EnfermedadActual { get; set; }
        [Display(Name = "EVOLUCIÓN ENFERMEDAD")]
        [MaxLength(100)]
        public int EvolucionEnfermedad { get; set; }
        [Display(Name = "ENFERMEDAD LABORAL")]
        [MaxLength(50)]
        public string EnfermedadLaboral { get; set; }
        [Display(Name = "ENFERMEDAD FUERA")]
        [MaxLength(50)]
        public int EnfernedadActividadesFuera { get; set; }
        public bool VisitoMedico { get; set; }
        [Display(Name = "REVISIÓN MÉDICA")]
        [MaxLength(50)]
        public string VisitoMedicoExplicacion { get; set; }
        public bool IncapacidadMedica { get; set; }
        [Display(Name = "INCAPACIDAD MÉDICA")]
        [MaxLength(50)]
        public string IncapacidadMedicaExplicacion { get; set; }
        public int DiasIncapacidad { get; set; }
        public TiposDeporte Sport { get; set; }
        [Display(Name = "MEJORÍAS SALUD")]
        [MaxLength(50)]
        public string MejoramientoSalud { get; set; }
    }
}