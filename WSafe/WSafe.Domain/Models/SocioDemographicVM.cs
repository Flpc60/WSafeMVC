using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Models
{
    public class SocioDemographicVM
    {
        public int ID { get; set; }
        [Display(Name = "GÉNERO")]
        public string Genero { get; set; }
        [Display(Name = "CÉDULA")]
        public string Documento { get; set; }
        [Display(Name = "NOMBRES COMPLETOS")]
        public string FullName { get; set; }
        [Display(Name = "FECHA NACIMIENTO")]
        public string FechaNacimiento { get; set; }
        [Display(Name = "FECHA INGRESO")]
        public string FechaIngreso { get; set; }
        [Display(Name = "ESCOLARIDAD")]
        public string Escolaridad { get; set; }
        [Display(Name = "PROFESIÓN")]
        public string Profesion { get; set; }
        [Display(Name = "AREA DE TRABAJO")]
        public string WorkArea { get; set; }
        [Display(Name = "OCUPACIÓN")]
        public string Cargo { get; set; }
        [Display(Name = "TIPO VINCULACIÓN")]
        public string TipoVinculacion { get; set; }
        [Display(Name = "TIPO DE JORNADA")]
        public string TipoJornada { get; set; }
        public string EPS { get; set; }
        public string AFP { get; set; }
        public string ARL { get; set; }
        [Display(Name = "EDAD")]
        public string Age { get; set; }
        [Display(Name = "TIPO DE SANGRE")]
        public string TipoSangre { get; set; }
        [Display(Name = "ANTIGUEDAD")]
        public string Antiguedad { get; set; }
        [Display(Name = "ESTADO CIVIL")]
        public string EstadoCivil { get; set; }
        [Display(Name = "CONYUGE")]
        public string Conyuge { get; set; }
        [Display(Name = "NÚMERO DE HIJOS")]
        public string NumberHijos { get; set; }
        [Display(Name = "ESTRATO SOCIOECONÓMICO")]
        public string StratumCategory { get; set; }
        [Display(Name = "TELÉFONOS")]
        public string Telefonos { get; set; }
        [Display(Name = "EMAIL")]
        public string Email { get; set; }
        [Display(Name = "DIRECCIÓN RESIDENCIA")]
        public string Direccion { get; set; }
        [Display(Name = "TIPO DE VIVIENDA")]
        public string TenenciaVivienda { get; set; }
        [Display(Name = "ENFERMEDAD QUE PADECE")]
        public string Enfermedad { get; set; }
        [Display(Name = "TRATAMIENTO ACTUAL")]
        public string Tratamiento { get; set; }
        [Display(Name = "RECOMENDACIONES ESPECIALES")]
        public string SpecialRecomendations { get; set; }
    }
}