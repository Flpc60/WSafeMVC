using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class Patology
    {
        public int ID { get; set; }
        [MaxLength(6)]
        public string Code { get; set; }
        [MaxLength(200)]
        public string Name { get; set; }
    }
}