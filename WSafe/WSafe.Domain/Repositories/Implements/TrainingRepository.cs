using WSafe.Domain.Data;
using WSafe.Domain.Data.Entities;

namespace WSafe.Domain.Repositories.Implements
{
    public class TrainingRepository : GenericRepository<TrainingTopic>, ITrainingRepository
    {
        public TrainingRepository(EmpresaContext _empresaContext) : base(_empresaContext) { }
    }
}
