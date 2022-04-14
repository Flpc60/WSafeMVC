using System.ComponentModel.DataAnnotations;

namespace WSafe.Web.Models
{
    public class IntervencionVM
    {
        public int ID { get; set; }
        [Display(Name = "Eliminación")]
        public string Eliminacion { get; set; }
        [Display(Name = "Sustitución")]
        public string Sustitucion { get; set; }
        [Display(Name = "Controles de ingeniería")]
        public string Ingenieria { get; set; }
        [Display(Name = "Administración")]
        public string Administracion { get; set; }
        [Display(Name = "Señalización")]
        public string Señalizacion { get; set; }
        [Display(Name = "Equipos / EPP")]
        public string EPP { get; set; }
    }
}