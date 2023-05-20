using System.Collections.Generic;
using System.Web.Mvc;
using WSafe.Domain.Data.Entities;

namespace WSafe.Domain.Helpers
{
    public interface IComboHelper
    {
        IEnumerable<SelectListItem> GetComboTrabajadores();
        IEnumerable<SelectListItem> GetComboUsers();
        IEnumerable<SelectListItem> GetComboProcesos();
        IEnumerable<SelectListItem> GetComboZonas();
        IEnumerable<SelectListItem> GetComboActividades();
        IEnumerable<SelectListItem> GetComboTareas();
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
        IEnumerable<SelectListItem> GetCargosAll();
    }
}