using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using WSafe.Domain.Data.Entities;

namespace WSafe.Domain.Models
{
    public class RiesgoViewModel
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Zona")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una zona.")]
        public int ZonaID { get; set; }
        public IEnumerable<SelectListItem> Zonas { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Proceso")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un proceso.")]
        public int ProcesoID { get; set; }
        public IEnumerable<SelectListItem> Procesos { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Actividad")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una actividad.")]
        public int ActividadID { get; set; }
        public IEnumerable<SelectListItem> Actividades { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Tarea")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una tarea.")]
        public int TareaID { get; set; }
        public IEnumerable<SelectListItem> Tareas { get; set; }
        public bool Rutinaria { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una categoria de peligro.")]
        [Display(Name = "Categoría")]
        public int CategoriaPeligroID { get; set; }
        public IEnumerable<SelectListItem> CategoriasPeligros { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un peligro.")]
        [Display(Name = "Peligro")]
        public int PeligroID { get; set; }
        public IEnumerable<SelectListItem> Peligros { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Efectos posibles")]
        public EfectosPosibles EfectosPosibles { get; set; }
        [Display(Name = "ND")]
        [Range(0, int.MaxValue, ErrorMessage = "Debes seleccionar un nivel de deficiencia.")]
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
        [Display(Name = "NR")]
        public int NivelRiesgo
        {
            get
            {
                return NivelProbabilidad * NivelConsecuencia;
            }
        }

        [Display(Name = "Interpretación NR")]
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
        public CategoriasAceptabilidad AceptabilidadNR { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Nro. Expuestos")]
        public int NroExpuestos { get; set; }
        [Display(Name = "Requisito legal")]
        public bool RequisitoLegal { get; set; }
        public int IncidenteID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Categoria daño")]
        public DangerCategories DangerCategory { get; set; }
        [Display(Name = "CONTROLES EN LA FUENTE :")]
        [MaxLength(300)]
        public string FuenteControls { get; set; }
        [Display(Name = "CONTROLES EN EL MEDIO :")]
        [MaxLength(300)]
        public string MedioControls { get; set; }
        [Display(Name = "CONTROLES EN EL INDIVIDUO :")]
        [MaxLength(300)]
        public string IndividuoControls { get; set; }
        public int OrganizationID { get; set; }
        public int ClientID { get; set; }
        public int UserID { get; set; }
        [Display(Name = "ACEPTABILIDAD:")]
        public string TxtAceptability { get; set; }
    }
}