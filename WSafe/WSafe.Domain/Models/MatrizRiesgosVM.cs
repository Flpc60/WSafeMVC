using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Models
{
    public class MatrizRiesgosVM
    {
        public int ID { get; set; }
        [Display(Name = "PROCESO")]
        public string Proceso { get; set; }
        [Display(Name = "ZONA/LUGAR")]
        public string Zona { get; set; }
        [Display(Name = "ACTIVIDAD")]
        public string Actividad { get; set; }
        [Display(Name = "RUTINARIA")]
        public string Rutinaria { get; set; }
        [Display(Name = "CLASIFICACIÓN")]
        public string CategoriaPeligro { get; set; }
        [Display(Name = "DESCRIPCIÓN")]
        public string Peligro { get; set; }
        [Display(Name = "EFECTOS POSIBLES")]
        public string EfectosPosibles { get; set; }
        [Display(Name = "ND")]
        public int NivelDeficiencia { get; set; }
        [Display(Name = "NE")]
        public int NivelExposicion { get; set; }
        [Display(Name = "NP")]
        public int NivelProbabilidad { get; set; }
        [Display(Name = "INP")]
        public string InterpretaNP { get; set; }

        [Display(Name = "NC")]
        public int NivelConsecuencia { get; set; }
        [Display(Name = "NR")]
        public int NivelRiesgo { get; set; }
        [Display(Name = "INR")]
        public string CategoriaRiesgo { get; set; }
        [Display(Name = "ACEPTABILIDAD")]
        public string AceptabilidadNR { get; set; }
        [Display(Name = "SIGNIFICADO")]
        public string SignificadoNR { get; set; }
        [Display(Name = "# EXPUESTOS")]
        public int NroExpuestos { get; set; }
        [Display(Name = "PEOR CONSECUENCIA")]
        public string PeorConsecuencia { get; set; }
        [Display(Name = "REQUISITO")]
        public string RequisitoLegal { get; set; }
        [Display(Name = "ELIMINACIÓN")]
        public string Eliminacion { get; set; }
        [Display(Name = "SUSTITUCIÓN")]
        public string Sustitucion { get; set; }
        [Display(Name = "CONTROLES INGENIERÍA")]
        public string Ingenieria { get; set; }
        [Display(Name = "CONTROLES ADMINISTRACIÓN")]
        public string Administracion { get; set; }
        [Display(Name = "SEÑALIZACIÓN")]
        public string Señalizacion { get; set; }
        [Display(Name = "EQUIPOS/EPP")]
        public string EPP { get; set; }
        [Display(Name = "CONTROLES EN LA FUENTE")]
        [MaxLength(100)]
        public string FuenteControls { get; set; }
        [Display(Name = "CONTROLES EN EL MEDIO")]
        [MaxLength(100)]
        public string MedioControls { get; set; }
        [Display(Name = "CONTROLES EN EL INDIVIDUO")]
        [MaxLength(100)]
        public string IndividuoControls { get; set; }
    }
}