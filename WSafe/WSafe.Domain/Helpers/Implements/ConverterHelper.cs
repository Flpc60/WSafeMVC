using System;
using System.Threading.Tasks;
using WSafe.Domain.Data;
using WSafe.Domain.Data.Entities;
using WSafe.Domain.Models;

namespace WSafe.Domain.Helpers.Implements
{
    class ConverterHelper : IConverterHelper
    {
        private readonly EmpresaContext _empresaCntext;
        private readonly IComboHelper _comboHelper;
        public ConverterHelper(EmpresaContext empresaContext, IComboHelper comboHelper)
        {
            _empresaCntext = empresaContext;
            _comboHelper = comboHelper;
        }

        public Task<Riesgo> ToPropertyAsync(RiesgoViewModel model, bool isNew)
        {

            throw new NotImplementedException();
        }

        public RiesgoViewModel ToPropertyViewModel(Riesgo riesgo)
        {
            return new RiesgoViewModel
            {
                ProcesoID = riesgo.ID

            };
        }
    }
}