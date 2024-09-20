using WSafe.Domain.Data.Entities.Ppre;
using WSafe.Domain.Repositories;

namespace WSafe.Domain.Services.Implements
{
    public class CalificationAmenazaService : GenericService<CalificationAmenaza>, ICalificationAmenazaService
    {
        public CalificationAmenazaService(ICalificationAmenazaRepository calificationAmenazaRepository) : base(calificationAmenazaRepository)
        {
        }
    }
}
