using WSafe.Domain.Data.Entities;
using WSafe.Domain.Repositories;

namespace WSafe.Domain.Services.Implements
{
    public class TrainingService : GenericService<TrainingTopic>, ITrainingService
    {
        public TrainingService(ITrainingRepository trainingRepository) : base(trainingRepository)
        {
        }
    }
}
