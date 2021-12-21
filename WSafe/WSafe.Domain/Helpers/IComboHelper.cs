using System.Collections.Generic;
using System.Web.Mvc;
using WSafe.Domain.Data.Entities;

namespace WSafe.Domain.Helpers
{
    public interface IComboHelper
    {
        IEnumerable<SelectListItem> GetComboProcesos();
        IEnumerable<SelectListItem> GetComboZonas();
        IEnumerable<SelectListItem> GetComboActividades();
        IEnumerable<SelectListItem> GetComboTareas();
        IEnumerable<SelectListItem> GetComboCategoriaPeligros();
        IEnumerable<SelectListItem> GetComboPeligros(int id);
        IEnumerable<SelectListItem> GetNivelDeficiencia();
        IEnumerable<SelectListItem> GetNivelExposicion();
        IEnumerable<SelectListItem> GetNivelConsecuencias();
    }
}