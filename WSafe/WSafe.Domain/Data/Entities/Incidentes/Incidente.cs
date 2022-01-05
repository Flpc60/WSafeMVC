using System;
using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities.Incidentes
{
    public class Incidente
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha reporte")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaReporte { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha incidente")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaIncidente { get; set; }
        [Display(Name = "Categoría incidente")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public CategoriasIncidente CategoriaIncidente { get; set; }
        [Display(Name = "Incapacidad médica")]
        public bool IncapacidadMedica { get; set; }
        [Display(Name = "Días de incapacidad médica")]
        public int DiasIncapacidad { get; set; }
        [Display(Name = "Naturaleza lesión")]
        public string NaturalezaLesion { get; set; }
        [Display(Name = "Partes afectadas")]
        public string PartesAfectadas { get; set; }
        [Display(Name = "Tipo incidente")]
        public string TipoIncidente { get; set; }
        [Display(Name = "Agente lesión")]
        public string AgenteLesion { get; set; }
        [Display(Name = "Actos inseguros")]
        public string ActosInseguros { get; set; }
        [Display(Name = "Condiciones inseguras")]
        public string CondicionesInsegura { get; set; }
        [Display(Name = "Tipo daño")]
        public string TipoDaño { get; set; }
        [Display(Name = "Maquinaria, Equipo, Proceso afectado")]
        public string Afectacion { get; set; }
        [Display(Name = "Daños ocasionados")]
        public string DañosOcasionados { get; set; }
        [Display(Name = "Tipo vehiculo")]
        public string TipoVehiculo { get; set; }
        [Display(Name = "Marca vehiculo")]
        public string MarcaVehiculo { get; set; }
        [Display(Name = "Modelo vehiculo")]
        public string ModeloVehiculo { get; set; }
        [Display(Name = "Kilometraje vehiculo")]
        public int KilometrajeVehiculo { get; set; }
        [Display(Name = "Costos estimados")]
        public decimal CostosEstimados { get; set; }
        [Display(Name = "Descripción incidente")]
        public string DescripcionIncidente { get; set; }
        [Display(Name = "Cómo pudo haberse evitado")]
        public string EvitarIncidente { get; set; }
        [Display(Name = "Qué acciones inmediatas se tomaron después del evento")]
        public string AccionesInmediatas { get; set; }
        [Display(Name = "Comentarios Adicionales")]
        public string ComentariosAdicionales { get; set; }
        [Display(Name = "Atención brindada")]
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