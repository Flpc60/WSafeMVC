using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WSafe.Domain.Data;
using WSafe.Domain.Data.Entities;
using WSafe.Domain.Data.Entities.Ppre;
using WSafe.Domain.Models;

namespace WSafe.Domain.Helpers.Implements
{
    public class EmergencyConverter : IEmergencyConverter
    {
        private readonly EmpresaContext _empresaContext;
        private readonly IComboHelper _comboHelper;
        private readonly IGestorHelper _gestorHelper;
        private readonly IChartHelper _chartHelper;
        private readonly IIndicadorHelper _indicadorHelper;
        public EmergencyConverter
            (EmpresaContext empresaContext,
            IComboHelper comboHelper,
            IGestorHelper gestorHelper,
            IChartHelper chartHelper,
            IIndicadorHelper indicadorHelper)
        {
            _empresaContext = empresaContext;
            _comboHelper = comboHelper;
            _gestorHelper = gestorHelper;
            _chartHelper = chartHelper;
            _indicadorHelper = indicadorHelper;
        }
        public IEnumerable<VulnerabilityVM> ToListVulnerabilityVM(IEnumerable<Vulnerability> list, int _orgID)
        {
            var filtered = list
                .Where(v => v.OrganizationID == _orgID)
                .OrderBy(v => v.CategoryAmenaza)
                .ThenBy(v => v.AmenazaID)
                .ThenBy(v => v.EvaluationConceptID);

            var model = new List<VulnerabilityVM>();
            foreach (var item in filtered)
            {
                string categoria = string.Empty;

                switch (item.CategoryAmenaza)
                {
                    case CategoryAmenazas.Naturales:
                        categoria = "Naturales";
                        break;

                    case CategoryAmenazas.Tecnologicas:
                        categoria = "Tecnológicas";
                        break;

                    case CategoryAmenazas.Sociales:
                        categoria = "Sociales";
                        break;
                }

                string type = string.Empty;
                string aspecto = string.Empty;

                switch (item.VulnerabilityType)
                {
                    case VulnerabilityTypes.Personas:
                        type = "Personas";
                        if (item.EvaluationConcept.EvaluationPerson == EvaluationPersonas.Organizacional) { aspecto = "Gestión Organizacional"; }
                        if (item.EvaluationConcept.EvaluationPerson == EvaluationPersonas.Entrenamiento) { aspecto = "Capacitación y Entrenaniento"; }
                        if (item.EvaluationConcept.EvaluationPerson == EvaluationPersonas.Seguridad) { aspecto = "Características de Seguridad"; }
                        break;

                    case VulnerabilityTypes.Recursos:
                        type = "Recursos";
                        if (item.EvaluationConcept.EvaluationRecurso == EvaluationRecursos.Suministros) { aspecto = "Suministros"; }
                        if (item.EvaluationConcept.EvaluationRecurso == EvaluationRecursos.Edificaciones) { aspecto = "Edificaciones"; }
                        if (item.EvaluationConcept.EvaluationRecurso == EvaluationRecursos.Equipos) { aspecto = "Equipos"; }
                        break;

                    case VulnerabilityTypes.Sistemas:
                        type = "Sistemas y Procesos";
                        if (item.EvaluationConcept.EvaluationSystem == EvaluationSystems.Servicios) { aspecto = "Servicios"; }
                        if (item.EvaluationConcept.EvaluationSystem == EvaluationSystems.Sistemas) { aspecto = "Sistemas alternos"; }
                        if (item.EvaluationConcept.EvaluationSystem == EvaluationSystems.Recuperacion) { aspecto = "Recuperación"; }
                        break;
                }

                string response = string.Empty;
                switch (item.Response)
                {
                    case ScalesCalification.Sí:
                        response = "SI";
                        break;

                    case ScalesCalification.Parcial:
                        response = "PARCIAL";
                        break;

                    case ScalesCalification.No:
                        response = "NO";
                        break;
                }

                var vulnerabityVM = new VulnerabilityVM
                {
                    ID = item.ID,
                    Type = type,
                    CategoryAmenaza = categoria,
                    Amenaza = item.Amenaza.Name,
                    EvaluationConcept = aspecto,
                    Item = item.EvaluationConcept.Name,
                    Response = response,
                    Observation = item.Observation
                };

                model.Add(vulnerabityVM);
            }

            return model;
        }
        public CrtVulnerabilityVM ToCrtVulnerabilityVMNew(int org, int id)
        {
            var model = new CrtVulnerabilityVM
            {
                Amenazas = _comboHelper.GetAllAmenazas(org, 1),
                EvaluationConcepts = _comboHelper.GetAllEvaluationConcepts(org, id)
            };
            return model;
        }
        public async Task<Vulnerability> ToVulnerabilityAsync(CrtVulnerabilityVM model, bool isNew)
        {
            var amenaza = await _empresaContext.Amenazas.FindAsync(model.AmenazaID);
            var evaluation = await _empresaContext.EvaluationConcepts.FindAsync(model.EvaluationConceptID);

            if (amenaza == null)
            {
                amenaza = new Amenaza { ID = model.AmenazaID };
                _empresaContext.Amenazas.Attach(amenaza);
            }

            if (evaluation == null)
            {
                evaluation = new EvaluationConcept { ID = model.EvaluationConceptID };
                _empresaContext.EvaluationConcepts.Attach(evaluation);
            }

            var result = new Vulnerability
            {
                ID = isNew ? 0 : model.ID,
                CategoryAmenaza = model.CategoryAmenaza,
                AmenazaID = model.AmenazaID,
                Amenaza = amenaza,
                EvaluationConceptID = model.EvaluationConceptID,
                EvaluationConcept = evaluation,
                Response = model.Response,
                OrganizationID = model.OrganizationID,
                ClientID = model.ClientID,
                UserID = model.UserID,
                VulnerabilityType = model.Types
            };

            return result;
        }
        public IEnumerable<IntervencionVM> GetInterventionsAll(int id)
        {
            try
            {
                var model = new List<IntervencionVM>();
                var vulnerability = _empresaContext.Vulnerabilities
                    .Where(v => v.ID == id)
                    .Include(v => v.Amenaza)
                    .Include(v => v.EvaluationConcept)
                    .Include(v => v.Interventions)
                    .FirstOrDefault();

                string type = string.Empty;
                switch (vulnerability.VulnerabilityType)
                {
                    case VulnerabilityTypes.Personas:
                        type = "Personas";
                        break;
                    case VulnerabilityTypes.Recursos:
                        type = "Recursos";
                        break;
                    case VulnerabilityTypes.Sistemas:
                        type = "Sistemas";
                        break;
                    default:
                        type = string.Empty;
                        break;
                }

                string categoria = string.Empty;
                switch (vulnerability.Amenaza.CategoryAmenaza)
                {
                    case CategoryAmenazas.Naturales:
                        categoria = "Naturales";
                        break;
                    case CategoryAmenazas.Tecnologicas:
                        categoria = "Tecnológicas";
                        break;
                    case CategoryAmenazas.Sociales:
                        categoria = "Sociales";
                        break;
                    default:
                        categoria = string.Empty;
                        break;
                }

                foreach (var item in vulnerability.Interventions)
                {
                    var worker = _empresaContext.Trabajadores.Find(item.TrabajadorID);
                    var intervencion = new IntervencionVM
                    {
                        ID = item.ID,
                        Vulnerability = type,
                        Categoria = categoria,
                        Amenaza = vulnerability.Amenaza.Name,
                        Aplicacion = _gestorHelper.GetCategoriaAplicacion(item.CategoriaAplicacion),
                        Description = item.Description.ToUpper(),
                        Finalidad = _gestorHelper.GetActionType((int)item.Finalidad),
                        Intervencion = _gestorHelper.GetJerarquiaControl((JerarquiaControles)(int)item.Intervencion),
                        Beneficios = item.Beneficios ?? "N/A",
                        Presupuesto = item.Presupuesto,
                        Responsable = (worker.Nombres + " " + worker.PrimerApellido + " " + worker.SegundoApellido).ToUpper(),
                        FechaInicial = item.FechaInicial,
                        FechaFinal = item.FechaFinal
                    };
                    model.Add(intervencion);

                }
                return model;

            }
            catch (Exception)
            {
                return new List<IntervencionVM>();
            }
        }
        public async Task<CrtVulnerabilityVM> ToCrtVulnerabilityVM(int id, int org)
        {
            var vulnerability = await _empresaContext.Vulnerabilities
                .Where(v => v.ID == id)
                .Include(v => v.Amenaza)
                .Include(v => v.EvaluationConcept)
                .Include(v => v.Interventions)
                .FirstOrDefaultAsync();

            var model = new CrtVulnerabilityVM
            {
                ID = vulnerability.ID,
                Types = vulnerability.VulnerabilityType,
                CategoryAmenaza = vulnerability.CategoryAmenaza,
                AmenazaID = vulnerability.AmenazaID,
                Amenazas = _comboHelper.GetAllAmenazas(org, (int)vulnerability.CategoryAmenaza),
                EvaluationConceptID = vulnerability.EvaluationConceptID,
                EvaluationConcepts = _comboHelper.GetAllEvaluationConcepts(org, (int)vulnerability.VulnerabilityType),
                Response = vulnerability.Response,
                Observation = vulnerability.Observation
            };
            return model;
        }
        public async Task<IEnumerable<VulnerabilityAnalisisVM>> GetConsolidateVulnerability(int id, int _orgID)
        {
            var model = new List<VulnerabilityAnalisisVM>();
            string type = string.Empty;

            switch (id)
            {
                case 1:
                    type = "Personas";
                    break;

                case 2:
                    type = "Recursos";
                    break;

                case 3:
                    type = "Sistemas y procesos";
                    break;
            }

            var consolidate = await _empresaContext.Vulnerabilities
                .Where(v => v.OrganizationID == _orgID && (int)v.VulnerabilityType == id)
                .Include(v => v.Amenaza)
                .Include(v => v.EvaluationConcept)
                .OrderBy(v => v.CategoryAmenaza)
                .ThenBy(v => v.AmenazaID)
                .ThenBy(v => v.EvaluationConceptID)
                .ToListAsync();

            foreach (var category in consolidate.GroupBy(v => v.CategoryAmenaza))
            {
                string categoria = string.Empty;
                switch (category.Key)
                {
                    case CategoryAmenazas.Naturales:
                        categoria = "Naturales";
                        break;

                    case CategoryAmenazas.Tecnologicas:
                        categoria = "Tecnológicas";
                        break;

                    case CategoryAmenazas.Sociales:
                        categoria = "Sociales";
                        break;
                }

                var vulnerability = new VulnerabilityAnalisisVM
                {
                    VulnerabilityType = type,
                    CategoryAmenaza = categoria,
                    Results = new Dictionary<string, ResultInterpretationVM>()
                };

                foreach (var amenaza in category.GroupBy(v => v.AmenazaID))
                {
                    string amenazaName = amenaza.FirstOrDefault()?.Amenaza.Name;
                    var i = 0;
                    double sum = 0;
                    foreach (var evalConcept in amenaza.GroupBy(v => v.EvaluationConceptID))
                    {
                        string evalName = evalConcept.FirstOrDefault()?.EvaluationConcept.Name;
                        sum += evalConcept.Sum(item =>
                            item.Response == ScalesCalification.Sí ? 1.0 :
                            item.Response == ScalesCalification.Parcial ? 0.5 : 0.0
                        );
                        i++;
                    }

                    double result = sum / i;
                    string interpretation = GetInterpretation(result);

                    vulnerability.Results[amenazaName] = new ResultInterpretationVM
                    {
                        Result = (decimal)result,
                        Interpretation = interpretation
                    };
                }

                model.Add(vulnerability);
            }

            return model;
        }
        private string GetInterpretation(double result)
        {
            if (result >= 0 && result <= 0.33)
            {
                return "MALO";
            }
            else if (result > 0.33 && result <= 0.67)
            {
                return "REGULAR";
            }
            else if (result > 0.67 && result <= 1.0)
            {
                return "BUENO";
            }
            return "NA";
        }
    }
}
