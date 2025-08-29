using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Models
{
    public class VulnerabilitiesAnalysisVM
    {
        [Key]
        public int ID { get; set; }
        public string Type { get; set; }
        public string CategoryAmenaza { get; set; }
        public string EvaluationConcept { get; set; }
        public string Name { get; set; }
        public Dictionary<string, ResponseVM> Responses { get; set; }
    }

    public class ResponseVM
    {
        public int ID { get; set; }
        public string Amenaza { get; set; }
        public string Response { get; set; }
        public string Observation { get; set; }
        public decimal Result { get; set; }
    }
}
