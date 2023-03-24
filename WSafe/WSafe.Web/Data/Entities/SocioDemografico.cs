namespace WSafe.Domain.Data.Entities
{
    public class SocioDemografico
    {
        public int ID { get; set; }
        public string Documento { get; set; }
        public int CargoID { get; set; }
        public int ZonaID { get; set; }
        public int Age { get; set; }
        public EstadosCivil EstadoCivil { get; set; }
        public CategoriasGenero Genero { get; set; }
        public int PersonasACargo { get; set; }
        public NivelesEscolaridad NivelEscolaridad { get; set; }
        public TenenciasVivienda TenenciaVicienda { get; set; }
        public Hobbies UsoTiempoLibre { get; set; }
        public decimal IngresosPromedio { get; set; }
        public int AntiguedadEmpresa { get; set; }
        public int AntiguedadCargo { get; set; }
        public TiposVinculacion TipoContrato { get; set; }
        public bool ActividadesSalud { get; set; }
        public bool Diagnostic { get; set; }
        public string Enfermedad { get; set; }
        public bool Fuma { get; set; }
        public int PaquetesDia { get; set; }
        public bool BebidasAlcoholicas { get; set; }
        public int MyProperty { get; set; }
        public TiposPeriodicidad FrecuenciaBebida { get; set; }
        public TiposDeporte Sport { get; set; }
        public TiposPeriodicidad SportFrecuence { get; set; }
        public bool Consentimiento { get; set; }
    }
}