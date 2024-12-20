﻿using System.Collections.Generic;
using System.Threading.Tasks;
using WSafe.Domain.Data.Entities.Ppre;
using WSafe.Domain.Models;

namespace WSafe.Domain.Helpers
{
    public interface IEmergencyConverter
    {
        Task<IEnumerable<VulnerabilityVM>> ToListVulnerabilityVM(int _orgID, int id);
        CrtVulnerabilityVM ToCrtVulnerabilityVMNew(int org, int id);
        Task<Vulnerability> ToVulnerabilityAsync(CrtVulnerabilityVM model, bool isNew);
        IEnumerable<IntervencionVM> GetInterventionsAll(int id);
        Task<CrtVulnerabilityVM> ToCrtVulnerabilityVM(int id, int org);
        Task<IEnumerable<VulnerabilityAnalisisVM>> GetConsolidateVulnerability(int id, int _orgID);
        IEnumerable<CalificationAmenazaVM> ToListCalificationAmenazaVM(IEnumerable<CalificationAmenaza> list, int _orgID);
        Task<IEnumerable<RiskLevelVM>> ToListRiskLevelVM(int _orgID);
        Task<IEnumerable<VulnerabilitiesAnalysisVM>> ToVulnerabilitiesVM(int _orgID, int id);
    }
}
