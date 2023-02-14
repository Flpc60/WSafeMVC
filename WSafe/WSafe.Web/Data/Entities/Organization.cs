using System;
using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class Organization
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string NIT { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Razon Social")]
        [MaxLength(50)]
        public string RazonSocial { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Direccion comercial")]
        [MaxLength(50)]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Municipio")]
        [MaxLength(50)]
        public string Municip { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Departamento")]
        [MaxLength(50)]
        public string Department { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Teléfonos")]
        [MaxLength(50)]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50)]
        public string ARL { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Clase Riesgo")]
        [MaxLength(10)]
        public string ClaseRiesgo { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Documento Represente Legal")]
        [MaxLength(20)]
        public string DocumentRepresent { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Nombre Represente Legal")]
        [MaxLength(50)]
        public string NameRepresent { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Actividad Economica")]
        [MaxLength(100)]
        public string EconomicActivity { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes ingresar número trabajadores")]
        public int NumeroTrabajadores { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Política")]
        [MaxLength(150)]
        public string Politica { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Productos")]
        [MaxLength(150)]
        public string Products { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(150)]
        public string Mision { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(150)]
        public string Vision { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(150)]
        public string Objetivos { get; set; }
        [MaxLength(150)]
        public string Procesos { get; set; }
        [MaxLength(150)]
        public string Organigrama { get; set; }
        [Display(Name = "Turnos Administrativo")]
        [MaxLength(150)]
        public string TurnosAdministrativo { get; set; }
        [Display(Name = "Turnos Operativo")]
        [MaxLength(150)]
        public string TurnosOperativo { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Año del periodo : ")]
        [MaxLength(4)]
        public string Year { get; set; }
        [Display(Name = "Es unidad de Producción Agropecuaria")]
        public bool Agropecuaria { get; set; }
        [Display(Name = "Responsable SG-SST")]
        [MaxLength(50)]
        public string ResponsableSGSST { get; set; }
        [Display(Name = "Documento Responsable SG-SST")]
        [MaxLength(20)]
        public string DocumentResponsable { get; set; }
        [Display(Name = "Resolución de licencia")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime ResolucionLicencia { get; set; }
        [Display(Name = "Número de la Licencia")]
        [MaxLength(20)]
        public string ResponsableLicencia { get; set; }
        [Display(Name = "Renovación de licencia")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime RenovacionLicencia { get; set; }
        [Display(Name = "Renovación de curso SG-SSTlicencia")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime RenovacionCurso { get; set; }
        [Display(Name = "Nivel de Estudios")]
        public NivelesEscolaridad NivelEstudios { get; set; }
        [Display(Name = "Meses de experiencia")]
        public int MesesExperiencia { get; set; }
        [MaxLength(2)]
        public string Range { get; set; }
        public int StandardEvaluation { get; set; }
        public int StandardMatrixRisk { get; set; }
        public int StandardActions { get; set; }
    }
}