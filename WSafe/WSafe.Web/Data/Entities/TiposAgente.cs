using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public enum TiposAgente
    {
        [Display(Name = "MÁQUINAS Y/O EQUIPOS")]
        Maquinas = 1,
        [Display(Name = "MEDIOS DE TRANSPORTE")]
        MediosTransporte = 2,
        [Display(Name = "APARATOS")]
        Aparatos = 3,
        [Display(Name = "HERRAMIENTAS, IMPLEMENTOS O UTENSILIOS")]
        Herramientas = 4,
        [Display(Name = "MATERIALES O SUSTANCIAS")]
        Materiales = 5,
        [Display(Name = "RADIACIONES")]
        Radiaciones = 6,
        [Display(Name = " AMBIENTE DE TRABAJO")]
        AmbienteTrabajo = 7,
        [Display(Name = " OTROS AGENTES NO CLASIFICADOS")]
        Otros = 8,
        [Display(Name = "ANIMALES")]
        Animales = 9,
        [Display(Name = "AGENTES NO CLASIFICADOS POR FALTA DE DATOS")]
        NoClasificados = 10
    }
}