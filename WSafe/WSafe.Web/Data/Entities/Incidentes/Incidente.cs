using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities.Incidentes
{
    public class Incidente
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public Zona Zona { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public Proceso Proceso { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public Actividad Actividad { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public Tarea Tarea { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha reporte")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaReporte { get; set; }
        [Display(Name = "Trabajadores lesionados")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public IEnumerable<Trabajador> Lesionados { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha incidente")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaIncidente { get; set; }
        public CategoriasIncidente CategoriaIncidente { get; set; }
        [Display(Name = "Incapacidad médica")]
        public bool IncapacidadMedica { get; set; }
        [Display(Name = "Días de incapacidad médica")]
        public int DiasIncapacidad { get; set; }
        [Display(Name = "Trabajador informante")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public Trabajador Informante { get; set; }
        public string NaturalezaLesion { get; set; }
        public string PartesAfectadas { get; set; }
        public string TipoIncidente { get; set; }
        public string AgenteLesion { get; set; }
        public string DañosOcasionados { get; set; }
        public string TipoVehiculo { get; set; }
        public string MarcaVehiculo { get; set; }
        public string ModeloVehiculo { get; set; }
        public string KilometrajeVehiculo { get; set; }
        public decimal CostosEstimados { get; set; }
        public string DescripcionIncidente { get; set; }
        public string EvitarIncidente { get; set; }
        public string AccionesInmediatas { get; set; }
        public string ComentariosAdicionales { get; set; }
        public string AtencionBrindada { get; set; }
        public CalificacionesEquipo EquipoInvestigador { get; set; }
        public TipoPerdida LesionPersonal { get; set; }
        public TipoPerdida DañoMaterial { get; set; }
        public TipoPerdida MedioAmbiente { get; set; }
        public TipoPerdida Imagen { get; set; }
        public bool RequiereInvestigacion { get; set; }
        public ConsecuenciasLesion ConsecuenciasLesion { get; set; }
        public ConsecuenciasDaño ConsecuenciasDaño { get; set; }
        public ConsecuenciasMedio ConsecuenciasMedio { get; set; }
        public ConsecuenciasImagen ConsecuenciasImagen { get; set; }
        public AccidenteProbabilidad Probabilidad { get; set; }
    }
}