using WSafe.Domain.Data.Entities.Ppre;
using WSafe.Web.Models;

namespace WSafe.Domain.Repositories.Implements
{
    public class ActionPlanRepository : GenericRepository<ActionPlan>, IActionPlanRepository
    {
        public ActionPlanRepository(EmpresaContext _empresaContext) : base(_empresaContext) { }
    }
}

