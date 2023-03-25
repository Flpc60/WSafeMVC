using System.ComponentModel.DataAnnotations;

namespace WSafe.Web.Models
{
    public class DashboardVM
    {
        public int ID { get; set; }
        [Display(Name = "AÑO")]
        public int Year { get; set; }
        [Display(Name = "ACCIDENTALIDAD")]
        public decimal AccidentsProportion { get; set; }
        [Display(Name = "% INDUCCIÓN")]
        public decimal InductionProportion { get; set; }
        [Display(Name = "% INSPECCIONES")]
        public decimal InspectionProportion { get; set; }
        [Display(Name = "% CUMPLIMIENTO ESTÁNDARES")]
        public decimal MinimalStandardsProportion { get; set; }
        [Display(Name = "% CUMPLIMIENTO PLAN ACTIVIDADES")]
        public decimal ActivitiesPlanProportion { get; set; }
        [Display(Name = "% CUMPLIMIENTO PLAN CAPACITACIÓN")]
        public decimal CapacitationPlanProportion { get; set; }
        [Display(Name = "% ACCIDENTES INVESTIGADOS")]
        public decimal ResearchAccidentsProportion { get; set; }
        [Display(Name = "% ENFERMEDADES LABORALES INVESTIGADAS")]
        public decimal ResearchELProportion { get; set; }
        [Display(Name = "NÚMERO INCIDENTES")]
        public int Incidents { get; set; }
        [Display(Name = "NÚMERO ACCIDENTES")]
        public int Accidents { get; set; }
        [Display(Name = "DÍAS AUSENTISMO")]
        public int Ausentisms { get; set; }
        [Display(Name = "AT MORTALES")]
        public int Mortality { get; set; }
        [Display(Name = "PROPORCIÓN AT")]
        public int AccidentsFrecuency { get; set; }
        [Display(Name = "SEVERIDAD AT")]
        public int AccidentsSeverity { get; set; }
        [Display(Name = "PROPORCIÓN EL")]
        public int ELFrecuence { get; set; }
        [Display(Name = "PREVALENCIA EL")]
        public int ELPrevalence { get; set; }
        [Display(Name = "INCIDENCIA EL")]
        public int ELIncidence { get; set; }
        [Display(Name = "LESIONES INCAPACITANTES")]
        public decimal ILIAT { get; set; }
    }
}