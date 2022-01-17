using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public enum CategoriasCausa
    {
        [Display(Name = "Falta medición o control")]
        Medicion = 1,
        [Display(Name = "Incumplimiento de un  método")]
        Incumplimiento = 2,
        [Display(Name = "Método inexistente")]
        Inexistente = 3,
        [Display(Name = "Planeación inadecuada")]
        Inadecuada = 4,
        [Display(Name = "Falta de recursos económicos")]
        Economicos = 5,
        [Display(Name = "Falta de recursos técnicos")]
        Tecnicos = 6,
        [Display(Name = "Falta de recursos físicos")]
        Fisicos = 7,
        [Display(Name = "Falta de insumos o suministros")]
        Insumos = 8,
        [Display(Name = "Falta de talento humano")]
        Humanos = 9,
        [Display(Name = "Falta de entrenamiento")]
        Entrenamiento = 10,
        [Display(Name = "Dificultades en el clima Org")]
        ClimaOrg = 10,
        [Display(Name = "Dificultades en la gobernabilidad")]
        Gobernabilidad = 11
    }
}