using System.Collections.Generic;
using System.Web.Mvc;
using WSafe.Domain.Data.Entities;

namespace WSafe.Domain.Helpers
{
    public interface IComboHelper
    {
        IEnumerable<SelectListItem> GetComboTrabajadores(int org);
        IEnumerable<SelectListItem> GetComboUsers();
        IEnumerable<SelectListItem> GetComboProcesos(int org);
        IEnumerable<SelectListItem> GetComboZonas(int org);
        IEnumerable<SelectListItem> GetComboActividades(int org);
        IEnumerable<SelectListItem> GetComboTareas(int org);
        IEnumerable<SelectListItem> GetComboCategoriaPeligros();
        IEnumerable<SelectListItem> GetComboPeligros(int id);
        IEnumerable<SelectListItem> GetComboIndicadores();
        IEnumerable<SelectListItem> GetNivelDeficiencia();
        IEnumerable<SelectListItem> GetNivelExposicion();
        IEnumerable<SelectListItem> GetNivelConsecuencias();
        IEnumerable<SelectListItem> GetComboRiesgo();
        IEnumerable<SelectListItem> GetAllCausas();
        IEnumerable<SelectListItem> GetAllRoles();
        IEnumerable<RoleOperation> GetAllAuthorizations();
        IEnumerable<Cargo> GetAllCargos();
        IEnumerable<Zona> GetAllZonas();
        IEnumerable<Proceso> GetAllProcess();
        IEnumerable<Actividad> GetAllActivitys();
        IEnumerable<Tarea> GetAllTareas();
        IEnumerable<Role> GetNameRoles();
        IEnumerable<SelectListItem> GetRootCauses(int incidentID);
        IEnumerable<SelectListItem> GetClients();
        IEnumerable<SelectListItem> GetCargosAll(int orgID);
        IEnumerable<SelectListItem> GetPatologiesAll();
        IEnumerable<SelectListItem> GetWorkersFull(int orgID);
    }
}