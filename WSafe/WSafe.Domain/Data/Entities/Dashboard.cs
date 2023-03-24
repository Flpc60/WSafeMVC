namespace WSafe.Domain.Data.Entities
{
    public class Dashboard
    {
        public int ID { get; set; }
        public int Year { get; set; }
        public decimal AccidentsProportion { get; set; }
        public decimal InductionProportion { get; set; }
        public decimal InspectionProportion { get; set; }
        public decimal MinimalStandardsProportion { get; set; }
        public decimal ActivitiesPlanProportion { get; set; }
        public decimal CapacitationPlanProportion { get; set; }
        public decimal ResearchAccidentsProportion { get; set; }
        public decimal ResearchELProportion { get; set; }
        public int Incdents { get; set; }
        public int Accidents { get; set; }
        public int Ausentisms { get; set; }
        public int Mortality { get; set; }
        public int AccidentsFrecuency { get; set; }
        public int AccidentsSeverity { get; set; }
        public int ELFrecuence { get; set; }
        public int ELPrevalence { get; set; }
        public int ELIncidence { get; set; }
        public decimal ILIAT { get; set; }
    }
}