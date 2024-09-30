namespace WSafe.Domain.Data.Entities.Ppre
{
    public class EvaluationConcept
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public VulnerabilityTypes VulnerabilitiType { get; set; }
        public EvaluationPersonas EvaluationPerson { get; set; }
        public EvaluationRecursos EvaluationRecurso { get; set; }
        public EvaluationSystems EvaluationSystem { get; set; }
        public int OrganizationID { get; set; }
        public int ClientID { get; set; }
        public int UserID { get; set; }
    }
}
