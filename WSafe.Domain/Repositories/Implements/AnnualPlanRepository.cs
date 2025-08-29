using WSafe.Domain.Data;
using WSafe.Domain.Data.Entities;

namespace WSafe.Domain.Repositories.Implements
{
    public class AnnualPlanRepository : GenericRepository<PlanActivity>, IAnnualPlanRepository
    {
        public AnnualPlanRepository(EmpresaContext _empresaContext) : base(_empresaContext) { }
    }
}