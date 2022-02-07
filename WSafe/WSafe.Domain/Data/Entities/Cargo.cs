﻿using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class Cargo
    {
        [Key]
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public int ID { get; set; }
        public string Codigo { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public string Descripcion { get; set; }
    }
}