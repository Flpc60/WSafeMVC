using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class NonConformity
    {
        public int ID { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
    }
}