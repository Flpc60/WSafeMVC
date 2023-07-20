using System.ComponentModel.DataAnnotations;

namespace WSafe.Web.Models
{
    public class ActionsMatrixVM
    {
        public int ID { get; set; }
        [Display(Name = "FECHA SOLICITUD")]
        public string FechaSolicitud { get; set; }
        [Display(Name = "ZONA")]
        public string Zona { get; set; }
        [Display(Name = "PROCESO")]
        public string Proceso { get; set; }
        [Display(Name = "ACTIVIDAD")]
        public string Actividad { get; set; }
        [Display(Name = "FUENTE ORIGEN")]
        public string FuenteAccion { get; set; }
        [Display(Name = "TIPO ACCIÓN")]
        public string Categoria { get; set; }
        [Display(Name = "DESCRIPCIÓN DEL HALLAZGO")]
        [MaxLength(100)]
        public string Descripcion { get; set; }
        [Display(Name = "CAUSA PRINCIPAL")]
        public string CategoriaCausa { get; set; }
        public string Planes { get; set; }
        [Display(Name = "FECHA CIERRE")]
        public string FechaCierre { get; set; }
        [Display(Name = "RESPONSABLE")]
        public string Responsable { get; set; }
        [Display(Name = "ESTADO")]
        public string ActionState { get; set; }
        [Display(Name = "EFECTIVA")]
        public bool Efectiva { get; set; }
        [Display(Name = "EFICACIA")]
        public decimal Eficacia { get; set; }
        public int OrganizationID { get; set; }
        public int ClientID { get; set; }
        public int UserID { get; set; }
    }
}