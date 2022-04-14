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
        IEnumerable<MatrizRiesgosVM> ToRiesgoViewModelFul(IEnumerable<Riesgo> riesgo, int id);
        IEnumerable<ListaRiesgosVM> ToRiesgoViewModelList(IEnumerable<Riesgo> riesgo);
        Task<Accion> ToAccionAsync(Accion model, bool isNew);
        AccionViewModel ToAccionViewModelNew();
        Task<Incidente> ToIncidenteAsync(IncidenteViewModel model, bool isNew);
        IncidenteViewModel ToIncidenteViewModel(Incidente incidente);
        IncidenteViewModel ToIncidenteViewModelNew();
        IndicadorViewModel ToIndicadorViewModel(Indicador indicador);
        IndicadorViewModel ToIndicadorViewModelNew(Indicador indicador, DateTime fechaInicial, DateTime fechaFinal);
        AccidentadoVM ToLesionadoViewModel(Trabajador lesionado);
        IEnumerable<AplicacionVM> ToIntervencionesViewModel(IEnumerable<Aplicacion> listaAplicacion);
        Task<Aplicacion> ToAplicacionAsync(AplicacionVM model, bool isNew);
        PlanAccionVM ToPlanAccionVM(PlanAccion planAccion);
        PlanAccionVM ToPlanAccionVMNew();
        Task<PlanAccion> ToPlanAccionAsync(PlanAccion plan);
        SeguimientoAccionVM ToSeguimientoAccionVM(SeguimientoAccion seguimientoAccion);
        SeguimientoAccionVM ToSeguimientoAccionVMNew();
        Task<SeguimientoAccion> ToSeguimientoAccionAsync(SeguimientoAccion model);
        IEnumerable<PlanAccionVM> ToPlanAccionVMList(IEnumerable<PlanAccion> plan);
        IEnumerable<SeguimientoAccionVM> ToSeguimientoAccionVMList(IEnumerable<SeguimientoAccion> accion);
        IEnumerable<AccionViewModel> ToAccionVMList(IEnumerable<Accion> accion);
        _DetailsAccionVM ToAccionVMFull(Accion accion, int id);
    }
}