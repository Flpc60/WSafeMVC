using WSafe.Domain.Data.Entities.Ppre;
using WSafe.Domain.Repositories;

namespace WSafe.Domain.Services.Implements
{
    public class ActionPlanService : GenericService<ActionPlan>, IActionPlanService
    {
        public ActionPlanService(IActionPlanRepository actionPlanRepository) : base(actionPlanRepository)
        {
        }
    }
}
