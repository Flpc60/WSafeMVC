using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WSafe.Domain.Data.Entities;
using WSafe.Web.Models;

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
                Value = a.ID.ToString()
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
                Value = cp.ID.ToString()
            })
                .OrderBy(cp => cp.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Seleccione una categoria de peligro ...)",
                Value = "0"
            });

            return list;
        }
        public IEnumerable<SelectListItem> GetComboPeligros(int categoriaID)
        {
            var list = _empresaContext.Peligros
                .Where(p => p.CategoriaPeligroID == categoriaID)
                .Select(p => new SelectListItem
                {
                    Text = p.Descripcion,
                    Value = p.ID.ToString()
                })
                .OrderBy(p => p.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Seleccione un tipo de peligro ...)",
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
                Value = p.ID.ToString()
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
                Value = t.ID.ToString()
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
                Value = z.ID.ToString()
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
        public IEnumerable<SelectListItem> GetComboTrabajadores()
        {
            var list = _empresaContext.Trabajadores.Select(t => new SelectListItem
            {
                Text = t.Nombres + " " + t.PrimerApellido + " " + t.Documento,
                Value = t.ID.ToString()
            })
                .OrderBy(t => t.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Seleccione un trabajador...)",
                Value = "0"
            });

            return list;
        }
        public IEnumerable<SelectListItem> GetComboIndicadores()
        {
            var list = _empresaContext.Indicadores.Select(i => new SelectListItem
            {
                Text = i.Nombre,
                Value = i.ID.ToString()
            })
                .OrderBy(t => t.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Seleccione un indicador...)",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboRiesgo()
        {
            //TODO
            var list = _empresaContext.Riesgos.Select(t => new SelectListItem
            {
                Text = t.TareaID + " " + t.PeligroID + " " + t.NivelRiesgo,
                Value = t.ID.ToString()
            })
                .OrderBy(t => t.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Seleccione un riesgo...)",
                Value = "0"
            });

            return list;
        }
        public IEnumerable<SelectListItem> GetAllCausas()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem
            {
                Text = "Seleccione una causa...",
                Value = "0"
            });

            list.Add(new SelectListItem
            {
                Text = "Falta medición o control",
                Value = "1"
            });

            list.Add(new SelectListItem
            {
                Text = "Incumplimiento de un  método o procedimiento",
                Value = "2"
            });
            list.Add(new SelectListItem
            {
                Text = "Método inexistente",
                Value = "3"
            });
            list.Add(new SelectListItem
            {
                Text = "Planeación inadecuada",
                Value = "4"
            });
            list.Add(new SelectListItem
            {
                Text = "Falta de recursos económicos",
                Value = "5"
            });
            list.Add(new SelectListItem
            {
                Text = "Falta de recursos técnicos",
                Value = "6"
            });
            list.Add(new SelectListItem
            {
                Text = "Falta de recursos físicos",
                Value = "7"
            });
            list.Add(new SelectListItem
            {
                Text = "Falta de insumos o suministros",
                Value = "8"
            });
            list.Add(new SelectListItem
            {
                Text = "Falta de talento humano",
                Value = "9"
            });
            list.Add(new SelectListItem
            {
                Text = "Falta de entrenamiento",
                Value = "10"
            });
            list.Add(new SelectListItem
            {
                Text = "Dificultades en el clima Org",
                Value = "11"
            });
            list.Add(new SelectListItem
            {
                Text = "Dificultades en la gobernabilidad",
                Value = "12"
            });

            return list;
        }
        public IEnumerable<Role> GetAllRoles()
        {
            return _empresaContext.Roles.ToList();
        }
        public IEnumerable<Cargo> GetAllCargos()
        {
            return _empresaContext.Cargos.OrderBy(c => c.Descripcion).ToList();
        }
        public IEnumerable<Zona> GetAllZonas()
        {
            return _empresaContext.Zonas.OrderBy(z=> z.Descripcion).ToList();
        }
        public IEnumerable<Proceso> GetAllProcess()
        {
            return _empresaContext.Procesos.OrderBy(p => p.Descripcion).ToList();
        }
        public IEnumerable<Actividad> GetAllActivitys()
        {
            return _empresaContext.Actividades.OrderBy(a => a.Descripcion).ToList();
        }
        public IEnumerable<Tarea> GetAllTareas()
        {
            return _empresaContext.Tareas.OrderBy(t => t.Descripcion).ToList();
        }
    }
}