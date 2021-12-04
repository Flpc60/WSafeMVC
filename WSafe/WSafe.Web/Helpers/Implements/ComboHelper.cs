using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public IEnumerable<SelectListItem> GetComboPeligros(int id)
        {
            var list = _empresaContext.Peligros
                .Where(cp => cp.CategoriaPeligroID == id)
                .Select(p => new SelectListItem
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

        public IEnumerable<SelectListItem> GetNivelConsecuencias()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem
            {
                Text = "Mortal o catastrófico (M)",
                Value = "100"
            });

            list.Add(new SelectListItem
            {
                Text = "Muy grave (MG)",
                Value = "60"
            });
            list.Add(new SelectListItem
            {
                Text = "Grave (G)",
                Value = "25"
            });
            list.Add(new SelectListItem
            {
                Text = "Leve (L)",
                Value = "10"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetNivelDeficiencia()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem
            {
                Text = "Muy alto (MA)",
                Value = "10"
            });

            list.Add(new SelectListItem
            {
                Text = "Alto(A)",
                Value = "6"
            });
            list.Add(new SelectListItem
            {
                Text = "Medio(M)",
                Value = "2"
            });
            list.Add(new SelectListItem
            {
                Text = "Bajo(B)",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetNivelExposicion()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem
            {
                Text = "Continua (EC)",
                Value = "4"
            });

            list.Add(new SelectListItem
            {
                Text = "Frecuente (EF)",
                Value = "3"
            });
            list.Add(new SelectListItem
            {
                Text = "Ocasional (EO)",
                Value = "2"
            });
            list.Add(new SelectListItem
            {
                Text = "Esporadica (EE)",
                Value = "1"
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