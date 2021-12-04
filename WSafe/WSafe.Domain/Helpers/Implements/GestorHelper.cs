using System;
using System.Collections.Generic;
using System.Linq;
using WSafe.Domain.Data;
using WSafe.Domain.Data.Entities;

namespace WSafe.Domain.Helpers.Implements
{
    public class GestorHelper : IGestorHelper
    {
        private readonly EmpresaContext _empresaContext;
        public GestorHelper(EmpresaContext empresaContext)
        {
            _empresaContext = empresaContext;
        }

        public string GetInterpretaNP(int probabilidad)
        {
            switch (probabilidad)
            {
                case int p when (p >= 24):
                    return "Muy alto (MA)";
                    break;

                case int p when (p >= 10 && p < 24):
                    return "Alto (A)";
                    break;

                case int p when (p >= 8 && p < 10):
                    return "Mdio (M)";
                    break;

                default:
                    return "Bajo (B)";
                    break;
            }
        }
        public string GetInterpretaNR(int nivelRiesgo)
        {
            switch (nivelRiesgo)
            {
                case int nr when (nr >= 600):
                    return "I";
                    break;

                case int nr when (nr >= 150 && nr < 600):
                    return "II";
                    break;

                case int nr when (nr >= 40 && nr < 150):
                    return "III";
                    break;

                default:
                    return "IV";
                    break;
            }
        }
        public string GetAceptabilidadNR(string nivelRiesgo)
        {
            switch (nivelRiesgo)
            {
                case "I":
                    return "No Aceptable";
                    break;

                case "II":
                    return "No Aceptable o Aceptable con control Específico";
                    break;

                case "III":
                    return "Mejorable";
                    break;


                default:
                    return "Aceptable";
                    break;
            }
        }
        public string GetSignificadoNR(string nivelRiesgo)
        {
            switch (nivelRiesgo)
            {
                case "I":
                    return "red";
                    break;

                case "II":
                    return "yellow";
                    break;

                case "III":
                    return "orange";
                    break;


                default:
                    return "green";
                    break;
            }
        }
        public List<Control> GetControlesFuente(int idRiesgo)
        {
            var list = _empresaContext.Controles
                .Where(c => c.RiesgoID == idRiesgo && c.CategoriaAplicacion == CategoriaAplicacion.Fuente)
                .Select(c => new Control
                {
                    Nombre = c.Nombre,
                    Beneficios = c.Beneficios,
                    Efectividad = c.Efectividad
                }).ToList();
            return list;
        }
        public List<Control> GetControlesMedio(int idRiesgo)
        {
            var list = _empresaContext.Controles
                .Where(c => c.RiesgoID == idRiesgo && c.CategoriaAplicacion == CategoriaAplicacion.Medio)
                .Select(c => new Control
                {
                    Nombre = c.Nombre,
                    Beneficios = c.Beneficios,
                    Efectividad = c.Efectividad
                }).ToList();
            return list;
        }
        public List<Control> GetControlesPersona(int idRiesgo)
        {
            var  list = _empresaContext.Controles
                .Where(c => c.RiesgoID == idRiesgo && c.CategoriaAplicacion == CategoriaAplicacion.Individuo)
                .Select(c => new Control
                {
                    Nombre = c.Nombre,
                    Beneficios = c.Beneficios,
                    Efectividad = c.Efectividad
                }).ToList();
            return list;
        }
    }
}