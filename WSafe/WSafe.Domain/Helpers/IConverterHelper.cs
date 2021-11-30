using System.Threading.Tasks;
using WSafe.Domain.Data.Entities;
using WSafe.Domain.Models;

namespace WSafe.Domain.Helpers
{
    interface IConverterHelper
    {
        Task<Riesgo> ToPropertyAsync(RiesgoViewModel model, bool isNew);

        RiesgoViewModel ToPropertyViewModel(Riesgo riesgo);
    }
}