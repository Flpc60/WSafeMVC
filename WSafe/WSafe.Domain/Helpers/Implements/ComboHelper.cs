using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WSafe.Domain.Data;

namespace WSafe.Domain.Helpers.Implements
{
    public class ComboHelper : IComboHelper
    {
        private readonly EmpresaContext _empresaContext;
        public ComboHelper(EmpresaContext empresaContext)
        {
            _empresaContext = empresaContext;
        }

        public IEnumerable<SelectListItem> GetComboActividades()
        {
            var list = _empresaContext.Actividades.Select(a => new SelectListItem
            {
                Text = a.Descripcion,
                Value = $"{a.ID}"
            })
                .OrderBy(a => a.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Seleccione una actividad ...)",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboCategoriaPeligros()
        {
            var list = _empresaContext.CategoriasPeligros.Select(cp => new SelectListItem
            {
                Text = cp.Descripcion,
                Value = $"{cp.ID}"
            })
                .OrderBy(cp => cp.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Seleccione una categoria peligro ...)",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboPeligros()
        {
            var list = _empresaContext.Peligros.Select(p => new SelectListItem
            {
                Text = p.Descripcion,
                Value = $"{p.ID}"
            })
                .OrderBy(p => p.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Seleccione un peligro ...)",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboProcesos()
        {
            var list = _empresaContext.Procesos.Select(p => new SelectListItem
            {
                Text = p.Descripcion,
                Value = $"{p.ID}"
            })
                .OrderBy(p => p.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Seleccione un proceso...)",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboTareas()
        {
            var list = _empresaContext.Tareas.Select(t => new SelectListItem
            {
                Text = t.Descripcion,
                Value = $"{t.ID}"
            })
                .OrderBy(t => t.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Seleccione una tarea...)",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboZonas()
        {
            var list = _empresaContext.Zonas.Select(z => new SelectListItem
            {
                Text = z.Descripcion,
                Value = $"{z.ID}"
            })
                .OrderBy(z => z.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Seleccione una zona...)",
                Value = "0"
            });

            return list;
        }
    }
}