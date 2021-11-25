using WSafe.Domain.Data.Entities;
using WSafe.Domain.Repositories;

namespace WSafe.Domain.Services.Implements
{
    public class RiesgoService : GenericService<Riesgo>, IRiesgoService
    {
        private IRiesgoRepository _riesgoRepository;

        public RiesgoService(IRiesgoRepository riesgoRepository) : base(riesgoRepository)
        {
            _riesgoRepository = riesgoRepository;
        }
    }
}