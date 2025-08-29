using WSafe.Domain.Data;
using WSafe.Domain.Data.Entities.Ppre;

namespace WSafe.Domain.Repositories.Implements
{
    public class CalificationAmenazaRepository : GenericRepository<CalificationAmenaza>, ICalificationAmenazaRepository
    {
        public CalificationAmenazaRepository(EmpresaContext _empresaContext) : base(_empresaContext) { }
    }
}
