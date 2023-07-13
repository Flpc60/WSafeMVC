﻿using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class Proceso
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Descripcion { get; set; }
        public int OrganizationID { get; set; }
        public int ClientID { get; set; }
    }
}