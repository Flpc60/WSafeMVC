using WSafe.Domain.Data.Entities;
using WSafe.Web.Models;

namespace WSafe.Domain.Repositories.Implements
{
    public class AnnualPlanRepository : GenericRepository<PlanActivity>, IAnnualPlanRepository
    {
        public AnnualPlanRepository(EmpresaContext _empresaContext) : base(_empresaContext) { }
    }
}