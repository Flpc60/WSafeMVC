using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WSafe.Web.Models
{
    public class MatrizRiesgosVM
    {
        public int ID { get; set; }
        [Display(Name = "Proceso")]
        public string Proceso { get; set; }
        [Display(Name = "Zona / Lugar")]
        public string Zona { get; set; }
        [Display(Name = "Actividades")]
        public string Actividad { get; set; }
        public bool Rutinaria { get; set; }
        [Display(Name = "Clasificación")]
        public string CategoriaPeligro { get; set; }
        [Display(Name = "Descripción")]
        public string Peligro { get; set; }
        [Display(Name = "Efectos posibles")]
        public string EfectosPosibles { get; set; }
        public string Fuente { get; set; }
        public string Medio { get; set; }
        public string Individuo { get; set; }

        [Display(Name = "ND")]
        public int NivelDeficiencia { get; set; }
        [Display(Name = "NE")]
        public int NivelExposicion { get; set; }
        [Display(Name = "NP")]
        public int NivelProbabilidad { get; set; }
        [Display(Name = "Interpretación NP")]
        public string InterpretaNP { get; set; }

        [Display(Name = "NC")]
        public int NivelConsecuencia { get; set; }
        [Display(Name = "NR")]
        public int NivelRiesgo { get; set; }
        [Display(Name = "Interpretación NR")]
        public string CategoriaRiesgo{ get; set; }
        [Display(Name = "Aceptabilidad NR")]
        public string AceptabilidadNR { get; set; }
        [Display(Name = "Significado NR")]
        public string SignificadoNR { get; set; }
        [Display(Name = "Nro. Expuestos")]
        public int NroExpuestos { get; set; }
        [Display(Name = "Peor consecuencia")]
        public string PeorConsecuencia { get; set; }
        [Display(Name = "Requisito legal")]
        public bool RequisitoLegal { get; set; }
        [Display(Name = "Eliminación")]
        public string Eliminacion { get; set; }
        [Display(Name = "Sustitución")]
        public string Sustitucion { get; set; }
        [Display(Name = "Controles de ingeniería")]
        public string Ingenieria { get; set; }
        [Display(Name = "Controles de administración")]
        public string Administracion { get; set; }
        [Display(Name = "Señalización")]
        public string Señalizacion { get; set; }
        [Display(Name = "Equipos / EPP")]
        public string EPP { get; set; }
    }
}