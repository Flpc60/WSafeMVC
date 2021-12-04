using WSafe.Domain.Data.Entities;
using WSafe.Domain.Models;
using WSafe.Domain.Repositories;

namespace WSafe.Domain.Services.Implements
{
    public class RiesgoService : GenericService<RiesgoViewModel>, IRiesgoService
    {
        public RiesgoService(IRiesgoRepository riesgoRepository) : base(riesgoRepository)
        {
        }
    }
}