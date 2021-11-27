using System.Collections.Generic;
using System.Web.Mvc;

namespace WSafe.Domain.Helpers
{
    interface IComboHelper
    {
        IEnumerable<SelectListItem> GetComboProcesos();

        IEnumerable<SelectListItem> GetComboZonas();

        IEnumerable<SelectListItem> GetComboActividades();
        IEnumerable<SelectListItem> GetComboTareas();
        IEnumerable<SelectListItem> GetComboCategoriaPeligros();
        IEnumerable<SelectListItem> GetComboPeligros();
    }
}