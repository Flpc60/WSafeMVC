using WSafe.Domain.Data;
using WSafe.Domain.Data.Entities;

namespace WSafe.Domain.Repositories.Implements
{
    public class RecomendationRepository : GenericRepository<Recomendation>, IRecomendationRepository
    {
        public RecomendationRepository(EmpresaContext _empresaContext) : base(_empresaContext) { }
    }
}