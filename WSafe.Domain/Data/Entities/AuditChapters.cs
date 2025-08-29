using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Linq;

namespace WSafe.Domain.Data.Entities
{
    public enum AuditChapters
    {
        [Display(Name = "POLÍTICA DE SST")]
        Politica = 1,
        [Display(Name = "PLANIFICACIÓN PARA LA IDENTIFICACIÓN DE PELIGROS, EVALUACIÓN Y VALORACIÓN DE RIESGOS")]
        Riesgos = 2,
        [Display(Name = "PLANIFICACIÓN PARA EL CUMPLIMIENTO DE REQUISITOS LEGALES")]
        Requisitos = 3,
        [Display(Name = "PLANIFICACIÓN DE OBJETIVOS Y PROGRAMAS")]
        Objetivos = 4,
        [Display(Name = "IMPLEMENTACIÓN - RECURSOS, FUNCIONES, RESPONSABILIDAD, RENDICIÓN DE CUENTAS Y AUTORIDAD")]
        Recursos = 5,
        [Display(Name = "IMPLEMENTACIÓN - COMPETENCIA, FORMACIÓN Y TOMA DE CONCIENCIA")]
        Competencia = 6,
        [Display(Name = "IMPLEMENTACIÓN - COMUNICACIÓN, PARTICIPACIÓN Y CONSULTA")]
        Comunicación = 7,
        [Display(Name = "IMPLEMENTACIÓN - DOCUMENTACIÓN DEL SG-SST")]
        Documentación = 8,
        [Display(Name = "IMPLEMENTACIÓN - CONTROL DOCUMENTOS")]
        ControlDocumentos = 9,
        [Display(Name = "IMPLEMENTACIÓN - CONTROL OPERATIVO")]
        ControlOperativo = 10,
        [Display(Name = "IMPLEMENTACIÓN - PREPARACIÓN Y RESPUESTA A EMERGENCIAS")]
        Emergencias = 11,
        [Display(Name = "VERIFICACIÓN - MONITOREO Y MEDICIÓN DEL DESEMPEÑO")]
        Medición = 12,
        [Display(Name = "VERIFICACIÓN - EVALUACIÓN CUMPLIMIENTO LEGAL")]
        Cumplimiento = 13,
        [Display(Name = "VERIFICACIÓN - INVESTIGACIÓN DE INCODENTES Y ACCIONES CPM")]
        Investigación = 14,
        [Display(Name = "VERIFICACIÓN - CONTROL DE REGISTROS")]
        ControlRegistros = 15,
        [Display(Name = "VERIFICACIÓN - AUDITORÍA INTERNA")]
        Auditoría = 16,
        [Display(Name = "REVISIÓN POR LA DIRECCIÓN")]
        Revisión = 17,
        [Display(Name = "LIDERAZGO Y COMPROMISO")]
        Liderazgo = 18,
        [Display(Name = "PARTICIPACIÓN Y CONSULTA DE LOS TRABAJADORES")]
        Participación = 19,
        [Display(Name = "PROGRAMAS DE CAPACITACIÓN Y FORMACIÓN")]
        Capacitación = 20,
        [Display(Name = "MEJORA CONTINUA")]
        Mejora = 21,
        [Display(Name = "GESTIÓN DE LA SALUD")]
        Salud = 22
    }
    public static class EnumHelper
    {
        public static List<SelectListItem> GetEnumSelectList<AuditChapters>()
        {
            var enumValues = Enum.GetValues(typeof(AuditChapters)).Cast<AuditChapters>();
            var selectListItems = new List<SelectListItem>();

            foreach (var value in enumValues)
            {
                var displayAttribute = value.GetType()
                    .GetField(value.ToString())
                    .GetCustomAttributes(typeof(DisplayAttribute), false)
                    as DisplayAttribute[];

                var displayName = displayAttribute?[0]?.Name ?? value.ToString();

                selectListItems.Add(new SelectListItem
                {
                    Text = displayName,
                    Value = ((int)Convert.ChangeType(value, typeof(int))).ToString()
                });
            }

            return selectListItems;
        }
    }
}

