using WSafe.Domain.Data.Entities.Ppre;
using WSafe.Web.Models;

namespace WSafe.Domain.Repositories.Implements
{
    public class CalificationAmenazaRepository : GenericRepository<CalificationAmenaza>, ICalificationAmenazaRepository
    {
        public CalificationAmenazaRepository(EmpresaContext _empresaContext) : base(_empresaContext) { }
    }
}
