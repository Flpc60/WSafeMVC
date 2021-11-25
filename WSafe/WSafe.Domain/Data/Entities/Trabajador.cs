using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WSafe.Domain.Data.Entities
{
    public class Trabajador
    {
        public int ID { get; set; }
        [Display(Name = "Primer Apellido")]
        public string PrimerApellido { get; set; }
        [Display(Name = "Segundo Apellido")]
        public string SegundoApellido { get; set; }
        [Display(Name = "Primer Nombre")]
        public string PrimerNombre { get; set; }
        [Display(Name = "Segundo Nombre")]
        public string SegundoNombre { get; set; }
        [NotMapped]
        public string NombreCompleto
        {
            get
            {
                return PrimerNombre + " " + SegundoNombre + " " + PrimerApellido + " " + SegundoApellido;
            }
        }
        public string Documento { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Foto { get; set; }
        public string Firma { get; set; }
        public string Email { get; set; }
        public CategoriasGenero Genero { get; set; }
        public EstadosCivil EstadoCivil { get; set; }
        public TiposVinculacion TipoVinculacion { get; set; }
        public Cargo Cargo { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string EPS { get; set; }
        public string AFP { get; set; }
        public string ARL { get; set; }
        public string Direccion { get; set; }
        public string Telefonos { get; set; }
        public bool AltoRiesgo { get; set; }
        public string ActividadAltoRiesgo { get; set; }
    }
}