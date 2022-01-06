using WSafe.Domain.Data.Entities;
using WSafe.Domain.Repositories;

namespace WSafe.Domain.Services.Implements
{
    public class AccionService : GenericService<Accion>, IAccionService
    {
        public AccionService(IAccionRepository accionRepository) : base(accionRepository)
        {
        }
    }
}