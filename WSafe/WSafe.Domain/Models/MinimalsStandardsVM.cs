using System;
using System.Collections.Generic;
using WSafe.Domain.Data.Entities;

namespace WSafe.Web.Models
{
    public class MinimalsStandardsVM
    {
        public int ID { get; set; }
        public int OrganizationID { get; set; }
        public string NIT { get; set; }
        public string RazonSocial { get; set; }
        public string Direccion { get; set; }
        public string Municip { get; set; }
        public string Department { get; set; }
        public RiskClasses ClaseRiesgo { get; set; }
        public string EconomicActivity { get; set; }
        public int NumeroTrabajadores { get; set; }
        public string ResponsableSGSST { get; set; }
        public string DocumentResponsable { get; set; }
        public DateTime FechaEvaluation { get; set; }
        public ICollection<CalificationVM> Califications { get; set; }
        public int Cumple { get; set; }
        public int NoCumple { get; set; }
        public int NoAplica { get; set; }
        public decimal StandarsResult { get; set; }
        public decimal AplicationsResult { get; set; }
        public ValorationCategory Category { get; set; }
        public ICollection<PlanActivityVM> Planes { get; set; }
        public string Color { get; set; }
        public string TxtCategory { get; set; }
    }
}