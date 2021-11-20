using System.Collections.Generic;

namespace WSafe.Domain.Data.Entities
{
    public class Riesgo
    {
        public int ID { get; set; }
        public int CargoID { get; set; }
        public int ProcesoID { get; set; }
        public int ActividadID { get; set; }
        public int TareaID { get; set; }
        public string TipoVinculacion { get; set; }
        public bool Rutinaria { get; set; }
        public string Descripcion { get; set; }
        public string ClasePeligro { get; set; }
        public string TipoPeligro { get; set; }
        public string ActoInseguro { get; set; }
        public string CondicionInsegura { get; set; }
        public string EfectosPosibles { get; set; }
        public string TiempoExposicion { get; set; }
        public string FrecuenciaExposicion { get; set; }
        public string ControlEliminacion { get; set; }
        public string ControlSustitucion { get; set; }
        public string ControlIngenieria { get; set; }
        public string ControlAdministrativo { get; set; }
        public string ControlSeñalizacion { get; set; }
        public string ControlEPP { get; set; }
        public List<Inspeccion> Inspecciones { get; set; }
        public int NivelDeficiencia { get; set; }
        public int NivelExposicion { get; set; }
        public int NivelProbabilidad { get; set; }
        public int NivelConsecuencias { get; set; }
        public int NivelRiesgo { get; set; }
        public int TrabajadoresExpuestos { get; set; }
        public bool AgenteCancerigeno { get; set; }
        public string PeorConsecuencia { get; set; }
        public bool RequisitoLegal { get; set; }
        public string Eliminacion { get; set; }
        public string Sustitucion { get; set; }
        public List<Control> ControlesIngenieria { get; set; }
        public List<ControlAdmon> ControlesAdministrativo { get; set; }
        public string EPP { get; set; }
        public List<SeguimientoRiesgo> Seguiminetos { get; set; }

    }
}
