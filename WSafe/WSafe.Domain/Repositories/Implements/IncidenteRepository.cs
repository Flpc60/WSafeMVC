﻿using WSafe.Domain.Data.Entities;
using WSafe.Web.Models;

namespace WSafe.Domain.Repositories.Implements
{
    public class IncidenteRepository : GenericRepository<Incidente>, IIncidenteRepository
    {
        public IncidenteRepository(EmpresaContext _empresaContext) : base(_empresaContext) { }
    }
}