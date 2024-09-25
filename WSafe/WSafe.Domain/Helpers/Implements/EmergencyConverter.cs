using System;
using System.Collections.Generic;
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

                switch (item.EvaluationConcept.VulnerabilitiType)
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
        public CrtVulnerabilityVM ToCrtVulnerabilityVMNew(int org)
        {
            var model = new CrtVulnerabilityVM
            {
                Amenazas = _comboHelper.GetAllAmenazas(org, 1),
                EvaluationConcepts = _comboHelper.GetAllEvaluationConcepts(org)
            };
            return model;
        }
        public async Task<Vulnerability> ToVulnerabilityAsync(CrtVulnerabilityVM model, bool isNew)
        {
            var amenaza = await _empresaContext.Amenazas.FindAsync(model.AmenazaID);
            var evaluation = await _empresaContext.EvaluationConcepts.FindAsync(model.EvaluationConceptID);
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
                UserID = model.UserID
            };

            return result;
        }
        public IEnumerable<IntervencionVM> GetInterventionsAll(int id)
        {
            try
            {
                string type = string.Empty;
                var vulnerability = _empresaContext.Vulnerabilities.Find(id);
                var amenaza = _empresaContext.Amenazas.Find(vulnerability.AmenazaID);
                var evaluation = _empresaContext.EvaluationConcepts.Find(vulnerability.EvaluationConceptID);

                switch (vulnerability.EvaluationConcept.VulnerabilitiType)
                {
                    case VulnerabilityTypes.Personas:
                        type = "Personas";
                        break;

                    case VulnerabilityTypes.Recursos:
                        type = "Recursos";
                        break;

                    case VulnerabilityTypes.Sistemas:
                        break;
                }

                string categoria = string.Empty;
                switch (amenaza.CategoryAmenaza)
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

                var trace = (from i in _empresaContext.Interventions
                             join e in _empresaContext.Trabajadores on i.TrabajadorID equals e.ID into WorkerJoin
                             from e in WorkerJoin.DefaultIfEmpty()
                             where i.VulnerabilityID == id
                             orderby i.FechaInicial
                             select new
                             {
                                 i.ID,
                                 Vulnerability = type,
                                 Categoría = categoria,
                                 Amenaza = amenaza.Name,
                                 i.CategoriaAplicacion,
                                 i.Finalidad,
                                 i.Intervencion,
                                 Benefiicos = i.Beneficios != null ? i.Beneficios.ToUpper() : "N/A",
                                 i.Presupuesto,
                                 Responsable = e != null ? (e.Nombres + " " + e.PrimerApellido + " " + e.SegundoApellido).ToUpper() : "N/A",
                                 i.FechaInicial,
                                 i.FechaFinal
                             }).ToList();

                var model = trace.Select(item => new IntervencionVM
                {
                    ID = item.ID,
                    Vulnerability = item.Vulnerability,
                    Categoria = item.Categoría,
                    Amenaza = item.Amenaza,
                    Aplicacion = _gestorHelper.GetCategoriaAplicacion(item.CategoriaAplicacion),
                    Finalidad = _gestorHelper.GetActionType((int)item.Finalidad),
                    Intervencion = _gestorHelper.GetJerarquiaControl((JerarquiaControles)(int)item.Intervencion),
                    Presupuesto = item.Presupuesto,
                    Responsable = item.Responsable
                }).ToList();

                return model;
            }
            catch (Exception ex)
            {
                return new List<IntervencionVM>();
            }
        }
    }
}
