using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities.ICAM
{
    public class CausalAnalice
    {
        public int ID { get; set; }
        public int IncidentID { get; set; }
        public int EventID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public CausalFactors CausalFactor { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public PotencialCausalFactors PotencialFactor { get; set; }
    }
}