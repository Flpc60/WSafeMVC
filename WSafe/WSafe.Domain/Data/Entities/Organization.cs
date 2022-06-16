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
    }
}