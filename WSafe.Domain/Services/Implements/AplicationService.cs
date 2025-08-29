using WSafe.Domain.Data.Entities;
using WSafe.Domain.Repositories;

namespace WSafe.Domain.Services.Implements
{
    public class AplicationService : GenericService<Aplicacion>, IAplicationService
    {
        public AplicationService(IAplicationRepository aplicationRepository) : base(aplicationRepository)
        {
        }
    }
}