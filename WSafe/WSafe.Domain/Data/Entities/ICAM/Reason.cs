﻿namespace WSafe.Domain.Data.Entities.ICAM
{
    public class Reason
    {
        public int ID { get; set; }
        public int IncidentID { get; set; }
        public int RootCauseID { get; set; }
        public string Name { get; set; }
    }
}