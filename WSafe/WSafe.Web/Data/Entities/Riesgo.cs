using System.Collections.Generic;

namespace WSafe.Domain.Data.Entities
{
    public class Riesgo
    {
        public int ID { get; set; }
        public Zona Zona { get; set; }
        public Proceso Proceso { get; set; }
        public Actividad Actividad { get; set; }
        public Tarea Tarea { get; set; }
        public bool Rutinaria { get; set; }
        public string Descripcion { get; set; }
        public CategoriaPeligro CategoriaPeligro { get; set; }
        public Peligro Peligro { get; set; }
        public Consecuencia Consecuencia { get; set; }
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
        public string SignificadoNR { get; set; }
        public int NroExpuestos { get; set; }
        public string PeorConsecuencia { get; set; }
        public bool RequisitoLegal { get; set; }
        public string Eliminacion { get; set; }
        public string Sustitucion { get; set; }
        public  ICollection<Operacion> ControlesIngenieria { get; set; }
        public string Señalizacion { get; set; }
        public string EPP { get; set; }
        public ICollection<Accion> Acciones { get; set; }
    }
}