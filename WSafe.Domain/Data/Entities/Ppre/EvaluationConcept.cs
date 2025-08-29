using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities.Ppre
{
    public class EvaluationConcept
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public VulnerabilityTypes VulnerabilityType { get; set; }
        public EvaluationPersonas? EvaluationPerson { get; set; }
        public EvaluationRecursos? EvaluationRecurso { get; set; }
        public EvaluationSystems? EvaluationSystem { get; set; }
        public int OrganizationID { get; set; }
        public int ClientID { get; set; }
        public int UserID { get; set; }
    }
}
