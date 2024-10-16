﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WSafe.Domain.Data;
using WSafe.Domain.Data.Entities;
using WSafe.Domain.Data.Entities.Ppre;
using WSafe.Domain.Models;
using static iTextSharp.text.pdf.AcroFields;
using static iTextSharp.tool.xml.html.HTML;

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
        public IEnumerable<VulnerabilityAnalisisVM> GetConsolidateVulnerability(int id, int _orgID)
        {
            var model = new List<VulnerabilityAnalisisVM>();
            string type = _gestorHelper.GetVulnerabilityType(id);

            var consolidate = _empresaContext.Vulnerabilities
                .Where(v => v.OrganizationID == _orgID && (int)v.VulnerabilityType == id)
                .Include(v => v.Amenaza)
                .Include(v => v.EvaluationConcept)
                .OrderBy(v => v.CategoryAmenaza)
                .ThenBy(v => v.AmenazaID)
                .ThenBy(v => v.EvaluationConcept)
                .ToList();

            foreach (var category in consolidate.GroupBy(v => v.CategoryAmenaza))
            {
                string categoria = _gestorHelper.GetAmenazaCategory(category.Key);
                string amenazaName = string.Empty;
                double total = 0;
                int i4 = 0;
                foreach (var amenaza in category.GroupBy(v => v.AmenazaID))
                {
                    amenazaName = amenaza.FirstOrDefault()?.Amenaza?.Name ?? "Sin Amenaza";
                    double sum1 = 0, sum2 = 0, sum3 = 0;
                    int i1 = 0, i2 = 0, i3 = 0;
                    string aspect1 = string.Empty, aspect2 = string.Empty, aspect3 = string.Empty;

                    foreach (var evalConcept in amenaza.GroupBy(v => v.EvaluationConcept))
                    {
                        if (evalConcept.Key.EvaluationPerson == EvaluationPersonas.Organizacional)
                        {
                            aspect1 = "Gestión Organizacional";
                            sum1 += evalConcept.Sum(item =>
                                item.Response == ScalesCalification.Sí ? 1.0 :
                                item.Response == ScalesCalification.Parcial ? 0.5 : 0.0
                            );
                            i1++;
                        }
                        if (evalConcept.Key.EvaluationPerson == EvaluationPersonas.Entrenamiento)
                        {
                            aspect2 = "Capacitación y Entrenaniento";
                            sum2 += evalConcept.Sum(item =>
                                item.Response == ScalesCalification.Sí ? 1.0 :
                                item.Response == ScalesCalification.Parcial ? 0.5 : 0.0
                            );
                            i2++;
                        }
                        if (evalConcept.Key.EvaluationPerson == EvaluationPersonas.Seguridad)
                        {
                            aspect3 = "Características de Seguridad";
                            sum3 += evalConcept.Sum(item =>
                                item.Response == ScalesCalification.Sí ? 1.0 :
                                item.Response == ScalesCalification.Parcial ? 0.5 : 0.0
                            );
                            i3++;
                        }
                        if (evalConcept.Key.EvaluationRecurso == EvaluationRecursos.Suministros)
                        {
                            aspect1 = "Suministros";
                            sum1 += evalConcept.Sum(item =>
                                item.Response == ScalesCalification.Sí ? 1.0 :
                                item.Response == ScalesCalification.Parcial ? 0.5 : 0.0
                            );
                            i1++;
                        }
                        if (evalConcept.Key.EvaluationRecurso == EvaluationRecursos.Edificaciones)
                        {
                            aspect2 = "Edificaciones";
                            sum2 += evalConcept.Sum(item =>
                                item.Response == ScalesCalification.Sí ? 1.0 :
                                item.Response == ScalesCalification.Parcial ? 0.5 : 0.0
                            );
                            i2++;
                        }
                        if (evalConcept.Key.EvaluationRecurso == EvaluationRecursos.Equipos)
                        {
                            aspect3 = "Equipos";
                            sum3 += evalConcept.Sum(item =>
                                item.Response == ScalesCalification.Sí ? 1.0 :
                                item.Response == ScalesCalification.Parcial ? 0.5 : 0.0
                            );
                            i3++;
                        }
                        if (evalConcept.Key.EvaluationSystem == EvaluationSystems.Servicios)
                        {
                            aspect1 = "Servicios";
                            sum1 += evalConcept.Sum(item =>
                                item.Response == ScalesCalification.Sí ? 1.0 :
                                item.Response == ScalesCalification.Parcial ? 0.5 : 0.0
                            );
                            i1++;
                        }
                        if (evalConcept.Key.EvaluationSystem == EvaluationSystems.Sistemas)
                        {
                            aspect2 = "Sistemas alternos";
                            sum2 += evalConcept.Sum(item =>
                                item.Response == ScalesCalification.Sí ? 1.0 :
                                item.Response == ScalesCalification.Parcial ? 0.5 : 0.0
                            );
                            i2++;
                        }
                        if (evalConcept.Key.EvaluationSystem == EvaluationSystems.Recuperacion)
                        {
                            aspect3 = "Recuperacion";
                            sum3 += evalConcept.Sum(item =>
                                item.Response == ScalesCalification.Sí ? 1.0 :
                                item.Response == ScalesCalification.Parcial ? 0.5 : 0.0
                            );
                            i3++;
                        }
                        total += evalConcept.Sum(item =>
                            item.Response == ScalesCalification.Sí ? 1.0 :
                            item.Response == ScalesCalification.Parcial ? 0.5 : 0.0
                        );
                        i4++;
                    }

                    double result1 = i1 > 0 ? sum1 / i1 : 0.0;
                    string interpretation1 = _gestorHelper.GetInterpretation(result1);

                    double result2 = i2 > 0 ? sum2 / i2 : 0.0;
                    string interpretation2 = _gestorHelper.GetInterpretation(result2);

                    double result3 = i3 > 0 ? sum3 / i3 : 0.0;
                    string interpretation3 = _gestorHelper.GetInterpretation(result3);

                    double result4 = i4 > 0 ? total / i4 : 0.0;
                    string interpretation4 = _gestorHelper.GetVulnerabilityInterpretation(result4);

                    if (i1 > 0)
                    {
                        var vulnerability = new VulnerabilityAnalisisVM
                        {
                            ID = 0,
                            VulnerabilityType = type,
                            CategoryAmenaza = categoria,
                            EvaluationConcept = aspect1,
                            Results = new Dictionary<string, ResultInterpretationVM>()
                        };
                        vulnerability.Results[amenazaName] = new ResultInterpretationVM
                        {
                            ID = 0,
                            Amenaza = amenazaName,
                            Result = (decimal)result1,
                            Interpretation = interpretation1
                        };
                        model.Add(vulnerability);
                    }

                    if (i2 > 0)
                    {
                        var vulnerability = new VulnerabilityAnalisisVM
                        {
                            ID = 0,
                            VulnerabilityType = type,
                            CategoryAmenaza = categoria,
                            EvaluationConcept = aspect2,
                            Results = new Dictionary<string, ResultInterpretationVM>()
                        };
                        vulnerability.Results[amenazaName] = new ResultInterpretationVM
                        {
                            ID = 0,
                            Amenaza = amenazaName,
                            Result = (decimal)result2,
                            Interpretation = interpretation2
                        };
                        model.Add(vulnerability);
                    }

                    if (i3 > 0)
                    {
                        var vulnerability = new VulnerabilityAnalisisVM
                        {
                            ID = 0,
                            VulnerabilityType = type,
                            CategoryAmenaza = categoria,
                            EvaluationConcept = aspect3,
                            Results = new Dictionary<string, ResultInterpretationVM>()
                        };
                        vulnerability.Results[amenazaName] = new ResultInterpretationVM
                        {
                            ID = 0,
                            Amenaza = amenazaName,
                            Result = (decimal)result3,
                            Interpretation = interpretation3
                        };
                        model.Add(vulnerability);
                    }

                    if (i4 > 0)
                    {
                        var vulnerabilityTotal = new VulnerabilityAnalisisVM
                        {
                            ID = 0,
                            CategoryAmenaza = categoria,
                            VulnerabilityType = type,
                            EvaluationConcept = "RESULTADOS",
                            Results = new Dictionary<string, ResultInterpretationVM>()
                        };

                        vulnerabilityTotal.Results[amenazaName] = new ResultInterpretationVM
                        {
                            ID = 0,
                            Amenaza = amenazaName,
                            Result = (decimal)result4,
                            Interpretation = _gestorHelper.GetVulnerabilityInterpretation((double)result4)
                        };
                        model.Add(vulnerabilityTotal);
                    }
                }
            }

            return model;
        }
        public IEnumerable<CalificationAmenazaVM> ToListCalificationAmenazaVM(IEnumerable<CalificationAmenaza> list, int _orgID)
        {
            var model = new List<CalificationAmenazaVM>();
            foreach (var item in list)
            {
                string categoria = _gestorHelper.GetAmenazaCategory(item.CategoryAmenaza);
                var calificationVM = new CalificationAmenazaVM
                {
                    ID = item.ID,
                    CategoryAmenaza = categoria,
                    Name = item.Amenaza.Name,
                    Description = item.Amenaza.Description,
                    OrigenAmenaza = _gestorHelper.GetOrigenAmenaza(item.Amenaza.OrigenAmenaza),
                    Calification = _gestorHelper.GetCalification(item.Calification)
                };

                model.Add(calificationVM);
            }

            return model;
        }
        public async Task<IEnumerable<RiskLevelVM>> ToListRiskLevelVM(int _orgID)
        {
            var model = new List<RiskLevelVM>();

            var vulnerabilities = await (from v in _empresaContext.Vulnerabilities
                                         join c in _empresaContext.CalificationAmenazas on v.AmenazaID equals c.ID
                                         where v.OrganizationID == _orgID
                                         orderby v.CategoryAmenaza, v.AmenazaID
                                         select new
                                         {
                                             ID = v.ID,
                                             VulnerabilityType = v.VulnerabilityType,
                                             CategoryAmenaza = v.CategoryAmenaza,
                                             AmenazaID = v.AmenazaID,
                                             Amenaza = v.Amenaza.Name,
                                             Calification = c.Calification,
                                             Response = v.Response
                                         }).ToListAsync();

            foreach (var category in vulnerabilities.GroupBy(v => v.CategoryAmenaza))
            {
                string categoria = _gestorHelper.GetAmenazaCategory(category.Key);
                string calification = _gestorHelper.GetCalification(category.First().Calification);
                double total = 0;
                int i4 = 0;

                foreach (var amenaza in category.GroupBy(v => v.AmenazaID))
                {
                    string amenazaName = amenaza.First().Amenaza;
                    double sum1 = 0, sum2 = 0, sum3 = 0;
                    int i1 = 0, i2 = 0, i3 = 0;
                    foreach (var type in amenaza.GroupBy(v => v.VulnerabilityType))
                    {
                        if (type.Key == VulnerabilityTypes.Personas)
                        {
                            sum1 += type.Sum(item =>
                                item.Response == ScalesCalification.Sí ? 1.0 :
                                item.Response == ScalesCalification.Parcial ? 0.5 : 0.0
                            );
                            i1++;
                        }
                        else if (type.Key == VulnerabilityTypes.Recursos)
                        {
                            sum2 += type.Sum(item =>
                                item.Response == ScalesCalification.Sí ? 1.0 :
                                item.Response == ScalesCalification.Parcial ? 0.5 : 0.0
                            );
                            i2++;
                        }
                        else if (type.Key == VulnerabilityTypes.Sistemas)
                        {
                            sum3 += type.Sum(item =>
                                item.Response == ScalesCalification.Sí ? 1.0 :
                                item.Response == ScalesCalification.Parcial ? 0.5 : 0.0
                            );
                            i3++;
                        }

                        total += type.Sum(item =>
                            item.Response == ScalesCalification.Sí ? 1.0 :
                            item.Response == ScalesCalification.Parcial ? 0.5 : 0.0
                        );
                        i4++;
                    }

                    double result1 = i1 > 0 ? sum1 / i1 : 0.0;
                    string interpretation1 = _gestorHelper.GetVulnerabilityInterpretation(result1);

                    double result2 = i2 > 0 ? sum2 / i2 : 0.0;
                    string interpretation2 = _gestorHelper.GetVulnerabilityInterpretation(result2);

                    double result3 = i3 > 0 ? sum3 / i3 : 0.0;
                    string interpretation3 = _gestorHelper.GetVulnerabilityInterpretation(result3);

                    double result4 = i4 > 0 ? total / i4 : 0.0;
                    string interpretation4 = _gestorHelper.GetRiskLevelInterpretation(calification, interpretation1, interpretation2, interpretation3);

                    if (i4 > 0)
                    {
                        var vulnerability = new RiskLevelVM
                        {
                            ID = 0,
                            CategoryAmenaza = categoria,
                            Name = amenazaName,
                            Calification = calification,
                            RiskPersons = interpretation1,
                            RiskResources = interpretation2,
                            RiskSystems = interpretation3,
                            RiskLevelResults = interpretation4
                        };

                        model.Add(vulnerability);
                    }
                }
            }

            return model;
        }
        public async Task<IEnumerable<VulnerabilitiesAnalysisVM>> ToVulnerabilitiesVM(int _orgID, int id)
        {
            string type = _gestorHelper.GetVulnerabilityType(id);
            var model = new List<VulnerabilitiesAnalysisVM>();

            var consolidate = await _empresaContext.Vulnerabilities
                .Where(v => v.OrganizationID == _orgID && (int)v.VulnerabilityType == id)
                .Include(v => v.Amenaza)
                .Include(v => v.EvaluationConcept)
                .OrderBy(v => v.CategoryAmenaza)
                .ThenBy(v => v.AmenazaID)
                .ThenBy(v => v.EvaluationConceptID)
                .ThenBy(v => v.EvaluationConcept.EvaluationPerson)
                .ThenBy(v => v.EvaluationConcept.EvaluationRecurso)
                .ThenBy(v => v.EvaluationConcept.EvaluationSystem)
                .ToListAsync();

            foreach (var vulneara in consolidate.GroupBy(v =>v.CategoryAmenaza))
            {
                string categoria = _gestorHelper.GetAmenazaCategory(vulneara.Key);
                foreach (var amenaza in vulneara.GroupBy(a => a.Amenaza))
                {
                    string amenazaName = amenaza.First()?.Amenaza.Name ?? " ";

                    string observation = amenaza.First()?.Observation ?? " ";
                    double sum = 0;
                    int i = 0;
                    string aspect = string.Empty;
                    string response = _gestorHelper.GetResponse(amenaza.First().Response);
                    decimal result = _gestorHelper.GetResponseValue(amenaza.First().Response);
                    foreach (var evalua in amenaza.GroupBy(e => e.EvaluationConcept.EvaluationPerson))
                    {
                        string name = evalua.First()?.EvaluationConcept.Name ?? " ";
                        if (evalua.Key == EvaluationPersonas.Organizacional)
                        {
                            aspect = "Calificación Gestión Organizacional";
                            sum += evalua.Sum(item =>
                                item.Response == ScalesCalification.Sí ? 1.0 :
                                item.Response == ScalesCalification.Parcial ? 0.5 : 0.0
                            );
                            i++;
                        }
                        if (evalua.Key == EvaluationPersonas.Entrenamiento)
                        {
                            aspect = "Calificación Capacitación y Entrenaniento";
                            sum += evalua.Sum(item =>
                                item.Response == ScalesCalification.Sí ? 1.0 :
                                item.Response == ScalesCalification.Parcial ? 0.5 : 0.0
                            );
                            i++;
                        }
                        if (evalua.Key == EvaluationPersonas.Seguridad)
                        {
                            aspect = "Calificación Características de Seguridad";
                            sum += evalua.Sum(item =>
                                item.Response == ScalesCalification.Sí ? 1.0 :
                                item.Response == ScalesCalification.Parcial ? 0.5 : 0.0
                            );
                            i++;
                        }

                        if (i > 0)
                        {
                            var vulnerabilityVM = new VulnerabilitiesAnalysisVM
                            {
                                ID = 0,
                                Type = type,
                                CategoryAmenaza = categoria,
                                EvaluationConcept = aspect,
                                Name = name,
                                Responses = new Dictionary<string, ResponseVM>
                                {
                                    [amenazaName] = new ResponseVM
                                    {
                                        ID = 0,
                                        Amenaza = amenazaName,
                                        Response = response,
                                        Observation = observation,
                                        Result = result
                                    }
                                }
                            };

                            model.Add(vulnerabilityVM);
                        }
                    }

                    if (i > 0)
                    {
                        var vulnerabilitySumVM = new VulnerabilitiesAnalysisVM
                        {
                            ID = 0,
                            Type = type,
                            CategoryAmenaza = categoria,
                            EvaluationConcept = aspect,
                            Name = aspect,
                            Responses = new Dictionary<string, ResponseVM>
                            {
                                [amenazaName] = new ResponseVM
                                {
                                    ID = 0,
                                    Amenaza = amenazaName,
                                    Response = result.ToString(),
                                    Observation = _gestorHelper.GetInterpretation((double)result),
                                    Result = result
                                }
                            }
                        };

                        model.Add(vulnerabilitySumVM);
                    }
                }
            }

            return model;
        }
    }
}
