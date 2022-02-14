using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WSafe.Domain.Data.Entities;
using WSafe.Web.Models;

namespace WSafe.Domain.Helpers
{
    public interface IConverterHelper
    {
        Task<Riesgo> ToRiesgoAsync(RiesgoViewModel model, bool isNew);
        RiesgoViewModel ToRiesgoViewModel(Riesgo riesgo);
        RiesgoViewModel ToRiesgoViewModelNew();
        AccionViewModel ToAccionViewModel(Accion accion);
        IEnumerable<ListaRiesgosVM> ToRiesgoViewModelList(IEnumerable<Riesgo> riesgo);
        Task<Accion> ToAccionAsync(AccionViewModel model, bool isNew);
        AccionViewModel ToAccionViewModelNew(int id);
        Task<Incidente> ToIncidenteAsync(IncidenteViewModel model, bool isNew);
        IncidenteViewModel ToIncidenteViewModel(Incidente incidente);
        IncidenteViewModel ToIncidenteViewModelNew();
        IndicadorViewModel ToIndicadorViewModel(Indicador indicador);
        IndicadorViewModel ToIndicadorViewModelNew(Indicador indicador, DateTime fechaInicial, DateTime fechaFinal);
        AccidentadoVM ToLesionadoViewModel(Trabajador lesionado);
    }
}