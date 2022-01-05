using WSafe.Domain.Data.Entities;
using WSafe.Domain.Repositories;

namespace WSafe.Domain.Services.Implements
{
    public class RiesgoService : GenericService<Riesgo>, IRiesgoService
    {
        public RiesgoService(IRiesgoRepository riesgoRepository) : base(riesgoRepository)
        {
        }
    }
}