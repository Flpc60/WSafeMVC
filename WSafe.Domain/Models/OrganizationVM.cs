using System;
using System.ComponentModel.DataAnnotations;
using WSafe.Domain.Data.Entities;

namespace WSafe.Domain.Models
{
    public class OrganizationVM
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(20)]
        [Display(Name = "NIT :")]
        public string NIT { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "RAZÓN SOCIAL :")]
        [MaxLength(50)]
        public string RazonSocial { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "DORECCIÓN COMERCIAL :")]
        [MaxLength(50)]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "MUNICIPIO :")]
        [MaxLength(50)]
        public string Municip { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "DEPARTAMENTO :")]
        [MaxLength(50)]
        public string Department { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "TELÉFONOS :")]
        [MaxLength(50)]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(20)]
        [Display(Name = "ARL :")]
        public string ARL { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "CLASE RIESGO :")]
        [MaxLength(10)]
        public string ClaseRiesgo { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "TIPO DOCUMENTO :")]
        public DocumentTypes DocumentType { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "DOCUMENTO REPRESENTANTE LEGAL :")]
        [MaxLength(20)]
        public string DocumentRepresent { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "REPRESENTANTE LEGAL :")]
        [MaxLength(50)]
        public string NameRepresent { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "ACTIVIDAD ECONÓMICA :")]
        [MaxLength(100)]
        public string EconomicActivity { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes ingresar número trabajadores")]
        [Display(Name = "NÚMERO TRABAJADORES :")]
        public int NumeroTrabajadores { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "POLÍTICA :")]
        [MaxLength(150)]
        public string Politica { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "PRODUCTOS :")]
        [MaxLength(150)]
        public string Products { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(150)]
        [Display(Name = "MISIÓN :")]
        public string Mision { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(150)]
        [Display(Name = "VISIÓN :")]
        public string Vision { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(150)]
        [Display(Name = "OBJETIVOS :")]
        public string Objetivos { get; set; }
        [MaxLength(150)]
        [Display(Name = "PROCESOS :")]
        public string Procesos { get; set; }
        [Display(Name = "TURNOS ADMINISTRATIVOS :")]
        [MaxLength(150)]
        public string TurnosAdministrativo { get; set; }
        [Display(Name = "TURNOS OPERATIVO :")]
        [MaxLength(150)]
        public string TurnosOperativo { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "PERIODO :")]
        [MaxLength(4)]
        public string Year { get; set; }
        [Display(Name = "PRODUCCIÓN AGROPECUARIA :")]
        public bool Agropecuaria { get; set; }
        [Display(Name = "RESPONSABLE SG-SST")]
        [MaxLength(50)]
        public string ResponsableSGSST { get; set; }
        [Display(Name = "DOCUMENTO RESPONSABLE SG-SST")]
        [MaxLength(20)]
        public string DocumentResponsable { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "FECHA RESOLUCIÓN LICENCIA SST :")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ResolucionLicencia { get; set; }
        [Display(Name = "RESPONSABLE LICENCIA SST :")]
        [MaxLength(20)]
        public string ResponsableLicencia { get; set; }
        [Display(Name = "RENOVACIÓN LICENCIA SST :")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RenovacionLicencia { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "RENOVACIÓN CURSO SST :")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RenovacionCurso { get; set; }
        [Display(Name = "NIVEL DE ESTUDIOS :")]
        public NivelesEscolaridad NivelEstudios { get; set; }
        [Display(Name = "MESES DE EXPERIENCIA EN SST :")]
        public int MesesExperiencia { get; set; }
        public int ClientID { get; set; }
    }
}