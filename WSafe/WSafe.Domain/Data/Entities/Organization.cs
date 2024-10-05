using System;
using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class Organization
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(20)]
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
        [MaxLength(20)]
        public string ARL { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Clase Riesgo")]
        public RiskClasses ClaseRiesgo { get; set; }
        public DocumentTypes DocumentType { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Documento Represente Legal")]
        [MaxLength(20)]
        public string DocumentRepresent { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Represente Legal")]
        [MaxLength(50)]
        public string NameRepresent { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Actividad Economica")]
        [MaxLength(100)]
        public string EconomicActivity { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(typeof(short), "1", "9999", ErrorMessage = "Por favor ingrese un número de trabajadores válido.")]
        public short NumeroTrabajadores { get; set; }
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
        [Range(typeof(short), "2000", "9999", ErrorMessage = "Por favor ingrese un año válido.")]
        public short Year { get; set; }
        [Display(Name = "Producción Agropecuaria")]
        public bool Agropecuaria { get; set; }
        [Display(Name = "Responsable SG-SST")]
        [MaxLength(50)]
        public string ResponsableSGSST { get; set; }
        [Display(Name = "Documento Responsable SG-SST")]
        [MaxLength(20)]
        public string DocumentResponsable { get; set; }
        [Display(Name = "Fecha resolución licencia")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ResolucionLicencia { get; set; }
        [Display(Name = "Número de la Licencia")]
        [MaxLength(20)]
        public string ResponsableLicencia { get; set; }
        [Display(Name = "Renovación Licencia")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RenovacionLicencia { get; set; }
        [Display(Name = "Renovación curso SG-SST")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
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
        public int StandardIncidents { get; set; }
        [MaxLength(12)]
        public string FechaResolucionLicencia { get; set; }
        [MaxLength(12)]
        public string FechaRenovacionLicencia { get; set; }
        [MaxLength(12)]
        public string FechaRenovacionCurso { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ControlDate { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ClientID { get; set; }
        public short StandardSocioDemographic { get; set; }
        public short StandardUnsafeacts { get; set; }
        public short StandardIndicators { get; set; }
        public short StandardRecomendations { get; set; }
        public short StandardAudits { get; set; }
        public short StandardAnnualPlan { get; set; }
        public short StandardOccupational { get; set; }
        public short StandardCapacitation { get; set; }
        public short StandardEmergenciesPLan { get; set; }
    }
}