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
        IEnumerable<MatrizRiesgosVM> ToRiesgoViewModelFul(IEnumerable<Riesgo> riesgo);
        IEnumerable<ListaRiesgosVM> ToRiesgoViewModelList(IEnumerable<Riesgo> riesgo);
        Task<Accion> ToAccionAsync(Accion model, bool isNew);
        AccionViewModel ToAccionViewModelNew();
        Task<Incidente> ToIncidenteAsync(IncidenteViewModel model, bool isNew);
        IncidenteViewModel ToIncidenteViewModel(Incidente incidente);
        IncidenteViewModel ToIncidenteViewModelNew();
        IndicadorViewModel ToIndicadorViewModel(Indicador indicador);
        IndicadorViewModel ToIndicadorViewModelNew(Indicador indicador, int[] periodo, int year);
        AccidentadoVM ToLesionadoViewModel(Trabajador lesionado);
        IEnumerable<AplicacionVM> ToIntervencionesViewModel(IEnumerable<Aplicacion> listaAplicacion);
        Task<Aplicacion> ToAplicacionAsync(AplicacionVM model, bool isNew);
        PlanAccionVM ToPlanAccionVM(PlanAction planAccion);
        PlanAccionVM ToPlanAccionVMNew();
        Task<PlanAction> ToPlanAccionAsync(PlanAction plan);
        SeguimientoAccionVM ToSeguimientoAccionVM(Seguimiento seguimientoAccion);
        Task<Seguimiento> ToSeguimientoAccionAsync(Seguimiento model);
        IEnumerable<PlanAccionVM> ToPlanAccionVMList(IEnumerable<PlanAction> plan);
        IEnumerable<SeguimientoAccionVM> ToSeguimientoAccionVMList(IEnumerable<Seguimiento> accion);
        IEnumerable<AccionViewModel> ToAccionVMList(IEnumerable<Accion> accion);
        _DetailsAccionVM ToAccionVMFull(Accion accion, int id);
        MatrizRiesgosVM ToRiesgoVMUnit(Riesgo riesgo);
        IEnumerable<AccidentadoVM> ToListLesionadosVM(IEnumerable<Accidentado> lesionados);
        Task<Accidentado> ToLesionadoAsync(AccidentadoVM model, bool isNew);
        DetailsIncidentVM ToIncidentVMFull(Incidente incidente, int id);
        IEnumerable<UserViewModel> ToUsersVM(IEnumerable<User> userList);
        IEnumerable<AuthorizationVM> ToRolOperationVM(IEnumerable<RoleOperation> roleOperation);
        RoleUserVM ToAuthorizationVM(User user);
        Task<User> ToUserAsync(RoleUserVM user);
    }
}