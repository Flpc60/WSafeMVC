﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using WSafe.Domain.Data.Entities;

namespace WSafe.Web.Models
{
    public class RiesgoViewModel
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una zona.")]
        public int ZonaID { get; set; }
        [Display(Name = "Zona")]
        public IEnumerable<SelectListItem> Zonas { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un proceso.")]
        public int ProcesoID { get; set; }
        [Display(Name = "Proceso")]
        public IEnumerable<SelectListItem> Procesos { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una actividad.")]
        public int ActividadID { get; set; }
        [Display(Name = "Actividad")]
        public IEnumerable<SelectListItem> Actividades { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una tarea.")]
        public int TareaID { get; set; }
        [Display(Name = "Tarea")]
        public IEnumerable<SelectListItem> Tareas { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public bool Rutinaria { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una categoria de peligro.")]
        public int CategoriaPeligroID { get; set; }
        [Display(Name = "Clasificación")]
        public IEnumerable<SelectListItem> CategoriasPeligros { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un peligro.")]
        public int PeligroID { get; set; }
        [Display(Name = "Descripción")]
        public IEnumerable<SelectListItem> Peligros { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Efectos posibles")]
        public EfectosPosibles EfectosPosibles { get; set; }
        [Display(Name = "ND")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un nivel de deficiencia.")]
        public int NivelDeficiencia { get; set; }
        [Display(Name = "Niveles Deficiencia")]
        public NivelesDeficiencia NivelesDeficiencia { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "NE")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un nivel de exposición.")]
        public int NivelExposicion { get; set; }
        [Display(Name = "Niveles Exposición")]
        public NivelesExposicion NivelesExposicion { get; set; }
        [Display(Name = "NP")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un nivel de probabilidad.")]
        public int NivelProbabilidad
        {
            get
            {
                return NivelDeficiencia * NivelExposicion;
            }
        }
        [Display(Name = "Interpretación NP")]
        public string InterpretacionNP
        {
            get
            {
                switch (NivelProbabilidad)
                {
                    case int p when (p >= 24):
                        return "Muy alto (MA)";

                    case int p when (p >= 10 && p < 24):
                        return "Alto (A)";

                    case int p when (p >= 8 && p < 10):
                        return "Mdio (M)";

                    default:
                        return "Bajo (B)";
                }

            }
        }
        [Display(Name = "NC")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un nivel de consecuencia.")]
        public int NivelConsecuencia { get; set; }
        [Display(Name = "Niveles Consecuencia")]
        public NivelesConsecuencia NivelesConsecuencia { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "NR")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un nivel de consecuencia.")]
        public int NivelRiesgo
        {
            get
            {
                return NivelProbabilidad * NivelConsecuencia;
            }
        }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Categoria")]
        public string CategoriaRiesgo
        {
            get
            {
                switch (NivelRiesgo)
                {
                    case int nr when (nr >= 600):
                        return "I";

                    case int nr when (nr >= 150 && nr < 600):
                        return "II";

                    case int nr when (nr >= 40 && nr < 150):
                        return "III";

                    default:
                        return "IV";
                }
            }
        }
        [Display(Name = "Aceptabilidad NR")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public CategoriasAceptabilidad AceptabilidadNR { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Nro. Expuestos")]
        public int NroExpuestos { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Requisito legal")]
        public bool RequisitoLegal { get; set; }
    }
}