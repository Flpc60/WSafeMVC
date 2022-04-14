using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WSafe.Web.Models
{
    public class MatrizRiesgosVM
    {
        public int ID { get; set; }
        public string Formato { get; set; }
        public string Estandar { get; set; }
        public string Titulo { get; set; }
        public string Fecha { get; set; }
        public string Version { get; set; }
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
        public int NivelConsecuencia { get; set; }
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
        public string AceptabilidadNR { get; set; }
        [Display(Name = "Significado NR")]
        public string SignificadoNR { get; set; }
        [Display(Name = "Nro. Expuestos")]
        public int NroExpuestos { get; set; }
        [Display(Name = "Peor consecuencia")]
        public string PeroConsecuencia { get; set; }
        [Display(Name = "Requisito legal")]
        public bool RequisitoLegal { get; set; }
        public ICollection<IntervencionVM> Intervenciones { get; set; }
    }
}