﻿using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class Consecuencia
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public CategoriaPeligro CategoriaPeligro { get; set; }
        public Peligro Peligro { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public string Descripcion { get; set; }
    }
}