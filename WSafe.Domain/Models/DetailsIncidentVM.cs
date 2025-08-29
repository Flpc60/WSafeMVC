using System.Collections.Generic;
using WSafe.Domain.Data.Entities.Incidentes;

namespace WSafe.Domain.Models
{
    public class DetailsIncidentVM
    {
        public int ID { get; set; }
        public string Formato { get; set; }
        public string Estandar { get; set; }
        public string Titulo { get; set; }
        public string Fecha { get; set; }
        public string Version { get; set; }
        public string Lugar { get; set; }
        public string FechaReporte { get; set; }
        public string FechaIncidente { get; set; }
        public CategoriasIncidente CategoriasIncidente { get; set; }
        public bool IncapacidadMedica { get; set; }
        public int DiasIncapacidad { get; set; }
        public string Informante { get; set; }
        public string NaturalezaLesion { get; set; }
        public string PartesAfectadas { get; set; }
        public string TipoIncidente { get; set; }
        public string AgenteLesion { get; set; }
        public string ActosInseguros { get; set; }
        public string CondicionesInsegura { get; set; }
        public string TipoDaño { get; set; }
        public string Afectacion { get; set; }
        public string DañosOcasionados { get; set; }
        public string TipoVehiculo { get; set; }
        public string MarcaVehiculo { get; set; }
        public string ModeloVehiculo { get; set; }
        public int KilometrajeVehiculo { get; set; }
        public decimal CostosEstimados { get; set; }
        public string DescripcionIncidente { get; set; }
        public string EvitarIncidente { get; set; }
        public string AccionesInmediatas { get; set; }
        public string ComentariosAdicionales { get; set; }
        public string AtencionBrindada { get; set; }
        public CalificacionesEquipo EquiposInvestigador { get; set; }
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
        public IEnumerable<AccidentadoVM> Lesionados { get; set; }
        public string Firma { get; set; }
    }
}