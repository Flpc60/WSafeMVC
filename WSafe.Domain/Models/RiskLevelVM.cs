using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Models
{
    public class RiskLevelVM
    {
        public int ID { get; set; }
        public string CategoryAmenaza { get; set; }
        public string Name { get; set; }
        public string Calification { get; set; }
        public string RiskPersons { get; set; }
        public string RiskResources { get; set; }
        public string RiskSystems { get; set; }
        public string RiskLevelResults { get; set; }
    }
}
