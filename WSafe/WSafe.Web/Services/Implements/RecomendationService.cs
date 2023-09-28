using WSafe.Domain.Data.Entities;
using WSafe.Domain.Repositories;

namespace WSafe.Domain.Services.Implements
{
    public class RecomendationService : GenericService<Recomendation>, IRecomendationService
    {
        public RecomendationService(IRecomendationRepository recomendationRepository) : base(recomendationRepository)
        {
        }
    }
}