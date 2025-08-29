using WSafe.Domain.Data.Entities.Ppre;
using WSafe.Domain.Repositories;

namespace WSafe.Domain.Services.Implements
{
    public class InterventionService : GenericService<Intervention>, IInterventionService
    {
        public InterventionService(IInterventionRepository interventionRepository) : base(interventionRepository)
        {
        }
    }
}
