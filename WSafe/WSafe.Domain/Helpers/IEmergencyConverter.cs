using System.Collections.Generic;
using System.Threading.Tasks;
using WSafe.Domain.Data.Entities.Ppre;
using WSafe.Domain.Models;

namespace WSafe.Domain.Helpers
{
    public interface IEmergencyConverter
    {
        IEnumerable<VulnerabilityVM> ToListVulnerabilityVM(IEnumerable<Vulnerability> list, int _orgID);
        CrtVulnerabilityVM ToCrtVulnerabilityVMNew(int org);
        Task<Vulnerability> ToVulnerabilityAsync(CrtVulnerabilityVM model, bool isNew);
        IEnumerable<IntervencionVM> GetInterventionsAll(int id);
    }
}
