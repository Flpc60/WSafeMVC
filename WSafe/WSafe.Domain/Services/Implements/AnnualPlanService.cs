using WSafe.Domain.Data.Entities;
using WSafe.Domain.Repositories;

namespace WSafe.Domain.Services.Implements
{
    public class AnnualPlanService : GenericService<PlanActivity>, IAnnualPlanService
    {
        public AnnualPlanService(IAnnualPlanRepository annualPlanRepository) : base(annualPlanRepository)
        {
        }
    }
}