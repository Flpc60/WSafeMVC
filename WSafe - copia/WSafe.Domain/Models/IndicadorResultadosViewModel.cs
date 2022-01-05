﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WSafe.Web.Models
{
    public class IndicadorResultadosViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Nombre del indicador")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Definicion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Numerador { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Denominador { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Formula { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Interpretacion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Periodicidad { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha inicial")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicial { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha final")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaFinal { get; set; }
        public IEnumerable<IndicadorDetallesViewModel> Datos { get; set; }
    }
}