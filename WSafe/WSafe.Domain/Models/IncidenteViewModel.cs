using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using WSafe.Domain.Data.Entities.Incidentes;

namespace WSafe.Web.Models
{
    public class IncidenteViewModel
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha reporte")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaReporte { get; set; }
        [Display(Name = "Trabajadores lesionados")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int LesionadoID { get; set; }
        public IEnumerable<SelectListItem> Lesionados { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha incidente")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaIncidente { get; set; }
        [Display(Name = "Categoria incidente")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una categoria incidente.")]
        public CategoriasIncidente CategoriasIncidente { get; set; }
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
        [Display(Name = "Equipo investigador")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un equipo investigador.")]
        public int EquipoID { get; set; }
        public CalificacionesEquipo EquiposInvestigador { get; set; }

        [Display(Name = "Lesión personal")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un daño personal.")]
        public TipoPerdida LesionPersonal { get; set; }
        [Display(Name = "Daño material")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un daño material.")]
        public TipoPerdida DañoMaterial { get; set; }
        [Display(Name = "Medio ambiente")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un daño material.")]
        public TipoPerdida MedioAmbiente { get; set; }
        [Display(Name = "Imagen")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un daño material.")]
        public TipoPerdida Imagen { get; set; }
        [Display(Name = "Requiere investigación ?")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public bool RequiereInvestigacion { get; set; }
        [Display(Name = "Consecuencia lesión")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un daño material.")]
        public ConsecuenciasLesion ConsecuenciasLesion { get; set; }
        [Display(Name = "Consecuencia daño")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un daño material.")]
        public ConsecuenciasDaño ConsecuenciasDaño { get; set; }
        [Display(Name = "Consecuencia medio")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un daño material.")]
        public ConsecuenciasMedio ConsecuenciasMedio { get; set; }
        [Display(Name = "Consecuecia Imagen")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un daño material.")]
        public ConsecuenciasImagen ConsecuenciasImagen { get; set; }
        [Display(Name = "Nivel Probabilidad")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un daño material.")]
        public AccidenteProbabilidad Probabilidad { get; set; }
    }
}