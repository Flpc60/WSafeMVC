using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class TrainingTopic
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(200)]
        public string Name { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(200)]
        public string Objetive { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(200)]
        public string Content { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(200)]
        public string Resources { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public ActivitiesFrequency ActivityFrequency { get; set; }
        public int OrganizationID { get; set; }
    }
}