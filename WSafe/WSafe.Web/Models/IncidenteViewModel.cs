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
        [Display(Name = "Zona")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una zona.")]
        public int ZonaID { get; set; }
        public IEnumerable<SelectListItem> Zonas { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Proceso")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un proceso.")]
        public int ProcesoID { get; set; }
        public IEnumerable<SelectListItem> Procesos { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Actividad")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una actividad.")]
        public int ActividadID { get; set; }
        public IEnumerable<SelectListItem> Actividades { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Tarea")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una tarea.")]
        public int TareaID { get; set; }
        public IEnumerable<SelectListItem> Tareas { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha reporte")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaReporte { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha incidente")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        private DateTime fechaIncidente { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha incidente")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaIncidente
        {
            get
            {
                if (fechaIncidente <= FechaReporte)
                { return fechaIncidente; }
                return DateTime.Now;
            }
            set
            {
                fechaIncidente = value;
            }
        }
        [Display(Name = "Categoria incidente")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una categoria incidente.")]
        public CategoriasIncidente CategoriasIncidente { get; set; }
        [Display(Name = "Incapacidad médica")]
        public bool IncapacidadMedica { get; set; }
        [Display(Name = "Días de incapacidad")]
        public int DiasIncapacidad { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Lesionados")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un trabajador.")]
        public int TrabajadorID { get; set; }
        public IEnumerable<SelectListItem> Trabajadores { get; set; }
        public IEnumerable<AccidentadoVM> Lesionados { get; set; }
        [Display(Name = "Reportado por")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Informante { get; set; }
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
        [Display(Name = "Maquinaria, Proceso")]
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
        [Display(Name = "Cómo evitarse")]
        public string EvitarIncidente { get; set; }
        [Display(Name = "Acciones inmediatas")]
        public string AccionesInmediatas { get; set; }
        [Display(Name = "Comentarios Adicionales")]
        public string ComentariosAdicionales { get; set; }
        [Display(Name = "Atención brindada")]
        public string AtencionBrindada { get; set; }
        [Display(Name = "Investigadores")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un equipo investigador.")]
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
        [Display(Name = "Requiere investigación")]
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
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una probabilidad.")]
        public AccidenteProbabilidad Probabilidad { get; set; }
    }
}