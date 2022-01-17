using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using WSafe.Domain.Data.Entities;

namespace WSafe.Web.Models
{
    public class AccionViewModel
    {
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public int ID { get; set; }
        public int RiesgoID { get; set; }
        public IEnumerable<SelectListItem> Riesgos { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Tipo acción")]
        public CategoriasAccion Categoria { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Fecha solicitud")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaSolicitud { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Responsable")]
        public int TrabajadorID { get; set; }
        public IEnumerable<SelectListItem> Trabajadores { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Fuente origen")]
        public FuentesAccion FuenteAccion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Descripción de la no conformidad")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Causas")]
        public CategoriasCausa CausaAccion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Otras Causas")]
        public CategoriasCausa SubCausa { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Porque de la acción")]
        public CategoriasCausa UltraCausa { get; set; }
    }
}