using WSafe.Domain.Data.Entities.Incidentes;
using WSafe.Domain.Repositories;

namespace WSafe.Domain.Services.Implements
{
    public class IncidenteService : GenericService<Incidente>, IIncidenteService
    {
        public IncidenteService(IIncidenteRepository incidenteRepository) : base(incidenteRepository)
        {
        }
    }
}