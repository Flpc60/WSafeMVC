using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using WSafe.Domain.Data.Entities;

namespace WSafe.Web.Models
{
    public class WorkersVM
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ID { get; set; }
        [Display(Name = "Primer Apellido")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50)]
        public string PrimerApellido { get; set; }
        [Display(Name = "Segundo Apellido")]
        [MaxLength(50)]
        public string SegundoApellido { get; set; }
        [Display(Name = "Nombres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50)]
        public string Nombres { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(20)]
        public string Documento { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha nacimiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaNacimiento { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public CategoriasGenero Genero { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Estado civil")]
        public EstadosCivil EstadoCivil { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Dirección residencia")]
        [MaxLength(50)]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Teléfonos")]
        [MaxLength(20)]
        public string Telefonos { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha de ingreso")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaIngreso { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Tipo vinculación")]
        public TiposVinculacion TipoVinculacion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CargoID { get; set; }
        [MaxLength(20)]
        [Display(Name = "Cargo")]
        public IEnumerable<SelectListItem> Cargos { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string EPS { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(20)]
        public string AFP { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(20)]
        public string ARL { get; set; }
        [Display(Name = "Fecha retiro")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaRetiro { get; set; }
        public int OrganizationID { get; set; }
        public int ClientID { get; set; }
        public int UserID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Tipo documento")]
        public DocumentTypes DocumentType { get; set; }
        [Display(Name = "Profesión")]
        public string Profesion { get; set; }
        [Display(Name = "Area de trabajo")]
        public WorkAreas WorkArea { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Tipo jornada")]
        public TiposJornada TipoJornada { get; set; }
        [Display(Name = "Tipo de sangre")]
        public TiposSangre TipoSangre { get; set; }
        [Display(Name = "Nombre del conyuge")]
        public string Conyuge { get; set; }
        [Display(Name = "Número de hijos")]
        public int NumberHijos { get; set; }
        [Display(Name = "Estrato socioeconómico")]
        public EstratoCategories StratumCategory { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Correo electrónico")]
        [MaxLength(50)]
        public string Email { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Tipo de vivienda")]
        public TenenciasVivienda TenenciaVivienda { get; set; }
        [Display(Name = "Enfermedad que padece")]
        public string Enfermedad { get; set; }
        [Display(Name = "Tratamiento que recibe")]
        public string Tratamiento { get; set; }
        [Display(Name = "Recomendaciones especiales")]
        public string SpecialRecomendations { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Nivel estudios")]
        public NivelesEscolaridad Escolaridad { get; set; }
    }
}