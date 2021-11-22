using System.Collections.Generic;

namespace WSafe.Domain.Data.Entities
{
    public class Riesgo
    {
        public int ID { get; set; }
        public Proceso Proceso { get; set; }
        public Zona Zona { get; set; }
        public Actividad Actividad { get; set; }
        public Tarea Tarea { get; set; }
        public bool Rutinaria { get; set; }
        public CategoriaPeligro CategoriaPeligro { get; set; }
        public Peligro Peligro { get; set; }
        public string EfectosPosibles { get; set; }
        public ICollection<Operacion> ControlesFuente { get; set; }
        public ICollection<Operacion> ControlesMedio { get; set; }
        public ICollection<Operacion> ControlesIndividuo { get; set; }
        public int NivelDeficiencia { get; set; }
        public int NivelExposicion { get; set; }
        public int NivelProbabilidad { get; set; }
        public string InterpretacionNP { get; set; }
        public int NivelConsecuencias { get; set; }
        public int NivelRiesgo { get; set; }
        public string InterpretacionNR { get; set; }
        public string AceptabilidadNR { get; set; }
        public int NroExpuestos { get; set; }
        public Consecuencia PeorConsecuencia { get; set; }
        public bool RequisitoLegal { get; set; }
        public Operacion Eliminacion { get; set; }
        public Operacion Sustitucion { get; set; }
        public ICollection<Operacion> ControlesIngenieria { get; set; }
        public ICollection<Operacion> ControlesAdmon { get; set; }
        public ICollection<Operacion> ControlesEPP { get; set; }
        public ICollection<Accion> Acciones { get; set; }
    }
}