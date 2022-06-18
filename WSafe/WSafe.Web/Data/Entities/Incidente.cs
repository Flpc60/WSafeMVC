using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WSafe.Domain.Data.Entities.Incidentes;

namespace WSafe.Domain.Data.Entities
{
    public class Incidente
    {
        public int ID { get; set; }
        public int ZonaID { get; set; }
        public int ProcesoID { get; set; }
        public int ActividadID { get; set; }
        public int TareaID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha reporte")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime FechaReporte { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha incidente")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime FechaIncidente { get; set; }
        [Display(Name = "Categoría incidente")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public CategoriasIncidente CategoriasIncidente { get; set; }
        [Display(Name = "Incapacidad médica")]
        public bool IncapacidadMedica { get; set; }
        [Display(Name = "Días de incapacidad")]
        public int DiasIncapacidad { get; set; }
        [Display(Name = "Reportado por")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int TrabajadorID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Naturaleza lesión")]
        [MaxLength(150)]
        public string NaturalezaLesion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(150)]
        [Display(Name = "Partes afectadas")]
        public string PartesAfectadas { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(150)]
        [Display(Name = "Tipo incidente")]
        public string TipoIncidente { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(150)]
        [Display(Name = "Agente lesión")]
        public string AgenteLesion { get; set; }
        [MaxLength(150)]
        [Display(Name = "Actos inseguros")]
        public string ActosInseguros { get; set; }
        [MaxLength(150)]
        [Display(Name = "Condiciones inseguras")]
        public string CondicionesInsegura { get; set; }
        [MaxLength(250)]
        [Display(Name = "Tipo daño")]
        public string TipoDaño { get; set; }
        [MaxLength(150)]
        [Display(Name = "Maquinaria, Proceso")]
        public string Afectacion { get; set; }
        [MaxLength(150)]
        [Display(Name = "Daños ocasionados")]
        public string DañosOcasionados { get; set; }
        [MaxLength(20)]
        [Display(Name = "Tipo vehiculo")]
        public string TipoVehiculo { get; set; }
        [MaxLength(20)]
        [Display(Name = "Marca vehiculo")]
        public string MarcaVehiculo { get; set; }
        [MaxLength(20)]
        [Display(Name = "Modelo vehiculo")]
        public string ModeloVehiculo { get; set; }
        [Display(Name = "Kilometraje vehiculo")]
        public int KilometrajeVehiculo { get; set; }
        [Display(Name = "Costos estimados")]
        public decimal CostosEstimados { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(250)]
        [Display(Name = "Descripción incidente")]
        public string DescripcionIncidente { get; set; }
        [MaxLength(150)]
        [Display(Name = "Cómo evitarse")]
        public string EvitarIncidente { get; set; }
        [MaxLength(150)]
        [Display(Name = "Acciones inmediatas")]
        public string AccionesInmediatas { get; set; }
        [MaxLength(150)]
        [Display(Name = "Comentarios Adicionales")]
        public string ComentariosAdicionales { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(150)]
        [Display(Name = "Atención brindada")]
        public string AtencionBrindada { get; set; }
        [Display(Name = "Investigadores")]
        public CalificacionesEquipo EquiposInvestigador { get; set; }
        [Display(Name = "Lesión personal")]
        public TipoPerdida LesionPersonal { get; set; }
        [Display(Name = "Material")]
        public TipoPerdida DañoMaterial { get; set; }
        [Display(Name = "Ambiente")]
        public TipoPerdida MedioAmbiente { get; set; }
        public TipoPerdida Imagen { get; set; }
        [Display(Name = "Investigar")]
        public bool RequiereInvestigacion { get; set; }
        [Display(Name = "Consecuencias lesión")]
        public ConsecuenciasLesion ConsecuenciasLesion { get; set; }
        [Display(Name = "Consecuencias daño")]
        public ConsecuenciasDaño ConsecuenciasDaño { get; set; }
        [Display(Name = "Consecuencias medio")]
        public ConsecuenciasMedio ConsecuenciasMedio { get; set; }
        [Display(Name = "Consecuencias imagen")]
        public ConsecuenciasImagen ConsecuenciasImagen { get; set; }
        public AccidenteProbabilidad Probabilidad { get; set; }
        public int RiesgoID { get; set; }
        public int AccionID { get; set; }
        public ICollection<Accidentado> Lesionados { get; set; }
    }
}