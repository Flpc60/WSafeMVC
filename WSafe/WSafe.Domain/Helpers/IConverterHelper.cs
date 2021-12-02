using System.Threading.Tasks;
using WSafe.Domain.Data.Entities;
using WSafe.Domain.Models;

namespace WSafe.Domain.Helpers
{
    interface IConverterHelper
    {
        Task<Riesgo> ToRiesgoAsync(RiesgoViewModel model, bool isNew);

        RiesgoViewModel ToRiesgoViewModel(Riesgo riesgo);
    }
}