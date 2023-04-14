using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public enum TiposEvento
    {
        [Display(Name = "CAIDA DE PERSONAS")]
        CaidaPersonas = 1,
        [Display(Name = "CAÍDA DE OBJETOS")]
        CaidaObjetos = 2,
        [Display(Name = "PISADAS, CHOQUES O GOLPES")]
        Pisadas = 3,
        [Display(Name = "ATRAPAMIENTOS")]
        Atrapamientos = 4,
        [Display(Name = "SOBREESFUERZO, ESFUERZO EXCESIVO O FALSO MOVIMIENTO")]
        SobreEsfuerzo = 5,
        [Display(Name = "EXPOSICIÓN O CONTACTO CON TEMPERATURA EXTREMA")]
        ExposicionTemperatura = 6,
        [Display(Name = "EXPOSICIÓN O CONTACTO CON LA ELECTRICIDAD")]
        ExposicionElectricidad = 7,
        [Display(Name = "EXPOSICIÓN O CONTACTO CON SUSTANCIAS NOCIVAS, RADIACIONES O SALPICADURAS")]
        ExposicionSustancias = 8,
        [Display(Name = "CONTACTO CON ELEMENTOS CORTOPUNZANTES")]
        ContactoCortopunzantes = 9,
        [Display(Name = "PROYECCIÓN DE PARTICULAS")]
        ProyeccionParticulas = 10,
        [Display(Name = "MACROORGANISMOS( PISADAS, MORDEDURA, PICADURAS)")]
        Macroorganismos = 11,
        [Display(Name = "OTRO")]
        Otro = 12
    }
}