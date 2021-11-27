﻿using System.Data.Entity;
using WSafe.Domain.Data.Entities;

namespace WSafe.Domain.Data
{
    public class EmpresaContext : DbContext
    {
        public EmpresaContext() : base("EmpresaContext")
        {
        }

        public DbSet<Riesgo> Riesgos { get; set; }
        public DbSet<Proceso> Procesos { get; set; }
        public DbSet<Zona> Zonas { get; set; }
        public DbSet<Actividad> Actividades { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<CategoriaPeligro> CategoriasPeligros { get; set; }
        public DbSet<Peligro> Peligros { get; set; }
        public DbSet<Trabajador> Trabajadores { get; set; }
    }
}