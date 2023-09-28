using WSafe.Domain.Data.Entities;
using WSafe.Web.Models;

namespace WSafe.Domain.Repositories.Implements
{
    public class RecomendationRepository : GenericRepository<Recomendation>, IRecomendationRepository
    {
        public RecomendationRepository(EmpresaContext _empresaContext) : base(_empresaContext) { }
    }
}