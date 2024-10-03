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
    }
}
