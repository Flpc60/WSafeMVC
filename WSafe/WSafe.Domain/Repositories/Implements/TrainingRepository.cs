using WSafe.Domain.Data.Entities;
using WSafe.Web.Models;

namespace WSafe.Domain.Repositories.Implements
{
    public class TrainingRepository : GenericRepository<TrainingTopic>, ITrainingRepository
    {
        public TrainingRepository(EmpresaContext _empresaContext) : base(_empresaContext) { }
    }
}
