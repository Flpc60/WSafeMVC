using System;
using System.Collections.Generic;
using System.Linq;
using WSafe.Domain.Data;
using WSafe.Domain.Data.Entities;
using WSafe.Web.Models;

namespace WSafe.Domain.Helpers.Implements
{
    public class GestorHelper : IGestorHelper
    {
        private readonly Data.EmpresaContext _empresaContext;
        public GestorHelper(Data.EmpresaContext empresaContext)
        {
            _empresaContext = empresaContext;
        }

        public string GetInterpretaNP(int probabilidad)
        {
            switch (probabilidad)
            {
                case int p when (p >= 24):
                    return "Muy alto (MA)";

                case int p when (p >= 10 && p < 24):
                    return "Alto (A)";

                case int p when (p >= 8 && p < 10):
                    return "Mdio (M)";

                default:
                    return "Bajo (B)";
            }
        }
        public string GetInterpretaNR(int nivelRiesgo)
        {
            switch (nivelRiesgo)
            {
                case int nr when (nr >= 600):
                    return "I";

                case int nr when (nr >= 150 && nr < 600):
                    return "II";

                case int nr when (nr >= 40 && nr < 150):
                    return "III";

                default:
                    return "IV";
            }
        }
        public string GetAceptabilidadNR(string nivelRiesgo)
        {
            switch (nivelRiesgo)
            {
                case "I":
                    return "No Aceptable";

                case "II":
                    return "No Aceptable o Aceptable con control Específico";

                case "III":
                    return "Mejorable";

                default:
                    return "Aceptable";
            }
        }
        public string GetSignificadoNR(string nivelRiesgo)
        {
            switch (nivelRiesgo)
            {
                case "I":
                    return "red";

                case "II":
                    return "yellow";

                case "III":
                    return "orange";

                default:
                    return "green";
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
            var list = _empresaContext.Controles
                .Where(c => c.RiesgoID == idRiesgo && c.CategoriaAplicacion == CategoriaAplicacion.Individuo)
                .Select(c => new Control
                {
                    Nombre = c.Nombre,
                    Beneficios = c.Beneficios,
                    Efectividad = c.Efectividad
                }).ToList();
            return list;
        }

        public NivelesDeficiencia GetNivelDeficiencia(int deficiencia)
        {
            switch (deficiencia)
            {
                case 10:
                    return NivelesDeficiencia.Muy_alto;

                case 6:
                    return NivelesDeficiencia.Alto;

                case 2:
                    return NivelesDeficiencia.Medio;

                default:
                    return NivelesDeficiencia.Bajo;
            }
        }

        public NivelesExposicion GetNivelExposicion(int exposicion)
        {
            switch (exposicion)
            {
                case 4:
                    return NivelesExposicion.Continua;

                case 3:
                    return NivelesExposicion.Frecuente;

                case 2:
                    return NivelesExposicion.Esporadica;

                default:
                    return NivelesExposicion.Ocasional;
            }
            throw new NotImplementedException();
        }

        public NivelesConsecuencia GetNivelConsecuencia(int consecuencia)
        {
            switch (consecuencia)
            {
                case 100:
                    return NivelesConsecuencia.Mortal_catastrófico;

                case 60:
                    return NivelesConsecuencia.Muy_grave;

                case 25:
                    return NivelesConsecuencia.Grave;

                default:
                    return NivelesConsecuencia.Leve;
            }
        }
    }
}