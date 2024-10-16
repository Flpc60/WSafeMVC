﻿using System;
using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities.Ppre
{
    public class Intervention
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres")]
        public string Description { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int VulnerabilityID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public CategoriaAplicacion CategoriaAplicacion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public CategoriasFinalidad Finalidad { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public JerarquiaControles Intervencion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(200)]
        public string Beneficios { get; set; }
        public decimal Presupuesto { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int TrabajadorID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicial { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaFinal { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int OrganizationID { get; set; }
        public int ClientID { get; set; }
        public int UserID { get; set; }
    }
}
