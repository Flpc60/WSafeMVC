using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WSafe.Domain.Data.Entities;

namespace WSafe.Web.Models
{
    public class ListCapacitationVM
    {
        public int ID { get; set; }
        [Display(Name = "ACTIVIDAD")]
        [MaxLength(200)]
        public string Activity { get; set; }
        [Display(Name = "OBJETIVO")]
        [MaxLength(200)]
        public int Objetive { get; set; }
        [Display(Name = "CONTENIDO")]
        [MaxLength(100)]
        public string Content { get; set; }
        [Display(Name = "RECURSOS")]
        [MaxLength(100)]
        public string Resources { get; set; }
        [Display(Name = "RESPONSABLE")]
        [MaxLength(100)]
        public string Responsable { get; set; }
        [Display(Name = "EFICACIA")]
        public string Eficacia { get; set; }
        [Display(Name = "EFICIENCIA")]
        public string Eficiencia { get; set; }
        [Display(Name = "EFECTIVIDAD")]
        public string Efectividad { get; set; }
        public short P1 { get; set; }
        public short E1 { get; set; }
        public short Cc1 { get; set; }
        public short C1 { get; set; }
        public short Tc1 { get; set; }
        public short P2 { get; set; }
        public short E2 { get; set; }
        public short Cc2 { get; set; }
        public short C2 { get; set; }
        public short Tc2 { get; set; }
        public short P3 { get; set; }
        public short E3 { get; set; }
        public short Cc3 { get; set; }
        public short C3 { get; set; }
        public short Tc3 { get; set; }
        public short P4 { get; set; }
        public short E4 { get; set; }
        public short Cc4 { get; set; }
        public short C4 { get; set; }
        public short Tc4 { get; set; }
        public short P5 { get; set; }
        public short E5 { get; set; }
        public short Cc5 { get; set; }
        public short C5 { get; set; }
        public short Tc5 { get; set; }
        public short P6 { get; set; }
        public short E6 { get; set; }
        public short Cc6 { get; set; }
        public short C6 { get; set; }
        public short Tc6 { get; set; }
        public short P7 { get; set; }
        public short E7 { get; set; }
        public short Cc7 { get; set; }
        public short C7 { get; set; }
        public short Tc7 { get; set; }
        public short P8 { get; set; }
        public short E8 { get; set; }
        public short Cc8 { get; set; }
        public short C8 { get; set; }
        public short Tc8 { get; set; }
        public short P9 { get; set; }
        public short E9 { get; set; }
        public short Cc9 { get; set; }
        public short C9 { get; set; }
        public short Tc9 { get; set; }
        public short P10 { get; set; }
        public short E10 { get; set; }
        public short Cc10 { get; set; }
        public short C10 { get; set; }
        public short Tc10 { get; set; }
        public short P11 { get; set; }
        public short E11 { get; set; }
        public short Cc11 { get; set; }
        public short C11 { get; set; }
        public short Tc11 { get; set; }
        public short P12 { get; set; }
        public short E12 { get; set; }
        public short Cc12 { get; set; }
        public short C12 { get; set; }
        public short Tc12 { get; set; }
        public short P13 { get; set; }
        public short E13 { get; set; }
        public short Cc13 { get; set; }
        public short C13 { get; set; }
        public short Tc13 { get; set; }
        [Display(Name = "SEGUIMIENTOS")]
        public IEnumerable<Schedule> Shedules { get; set; }
    }
}