using System.Collections.Generic;
using System.Threading.Tasks;
using WSafe.Domain.Data;
using WSafe.Domain.Data.Entities;
using WSafe.Web.Models;

namespace WSafe.Domain.Helpers
{
    public interface IConverterHelper
    {
        Task<Riesgo> ToRiesgoAsync(RiesgoViewModel model, bool isNew);
        RiesgoViewModel ToRiesgoViewModel(Riesgo riesgo, int org);
        RiesgoViewModel ToRiesgoViewModelNew(int org);
        AccionViewModel ToAccionViewModel(Accion accion, int org);
        IEnumerable<MatrizRiesgosVM> ToRiesgoViewModelFul(IEnumerable<Riesgo> riesgo);
        IEnumerable<ListaRiesgosVM> ToRiesgoViewModelList(IEnumerable<Riesgo> riesgo);
        Task<Accion> ToAccionAsync(Accion model, bool isNew);
        AccionViewModel ToAccionViewModelNew(int org);
        Task<Incidente> ToIncidenteAsync(IncidenteViewModel model, bool isNew);
        IncidenteViewModel ToIncidenteViewModel(Incidente incidente, int org);
        IncidenteViewModel ToIncidenteViewModelNew(int org);
        IndicadorViewModel ToIndicadorViewModel(Indicador indicador);
        IndicadorViewModel ToIndicadorViewModelNew(Indicador indicador, int[] periodo, int year);
        AccidentadoVM ToLesionadoViewModel(Trabajador lesionado);
        IEnumerable<AplicacionVM> ToIntervencionesViewModel(IEnumerable<Aplicacion> listaAplicacion);
        Task<Aplicacion> ToAplicacionAsync(AplicacionVM model, bool isNew);
        PlanAccionVM ToPlanAccionVM(PlanAction planAccion);
        PlanAccionVM ToPlanAccionVMNew();
        Task<PlanAction> ToPlanAccionAsync(PlanAction plan);
        SeguimientoAccionVM ToSeguimientoAccionVM(SeguimientoAccion seguimientoAccion);
        Task<SeguimientoAccion> ToSeguimientoAccionAsync(SeguimientoAccion model);
        IEnumerable<PlanAccionVM> ToPlanAccionVMList(IEnumerable<PlanAction> plan);
        IEnumerable<SeguimientoAccionVM> ToSeguimientoAccionVMList(IEnumerable<SeguimientoAccion> accion);
        IEnumerable<AccionViewModel> ToAccionVMList(IEnumerable<Accion> accion, int org);
        _DetailsAccionVM ToAccionVMFull(Accion accion, int id);
        MatrizRiesgosVM ToRiesgoVMUnit(Riesgo riesgo);
        IEnumerable<AccidentadoVM> ToListLesionadosVM(IEnumerable<Accidentado> lesionados);
        Task<Accidentado> ToLesionadoAsync(AccidentadoVM model, bool isNew);
        DetailsIncidentVM ToIncidentVMFull(Incidente incidente, int id);
        IEnumerable<UserViewModel> ToUsersVM(int _clientID);
        IEnumerable<AuthorizationVM> ToRolOperationVM(IEnumerable<RoleOperation> roleOperation);
        RoleUserVM ToAuthorizationVM(User user);
        Task<User> ToUserAsync(RoleUserVM user);
        AplicacionVM ToAplicationVM(Aplicacion aplicacion);
        IEnumerable<MovimientVM> ToListMovimientos(IEnumerable<Movimient> movimientos);
        IEnumerable<EvaluationVM> ToEvaluationVMList(IEnumerable<Evaluation> evaluation);
        IEnumerable<CalificationVM> ToCalificationVMList(IEnumerable<CalificationVM> calification);
        IEnumerable<PlanActivityVM> ToPlanActivityVMList(IEnumerable<PlanActivityVM> planActivity);
        PlanActivityVM ToPlanActivityVM(PlanActivity planActivity);
        MinimalsStandardsVM ToMinimalsStandardsVM(Evaluation evaluation);
        Task<Trabajador> ToTrabajadorAsync(WorkersVM model, bool isNew);
        WorkersVM ToTrabajadorVM(Trabajador model);
        IEnumerable<SocioDemographicVM> ToWorkersVM(IEnumerable<Trabajador> trabajador);
        UnsafeactVM ToUnsafeactVM(Data.Entities.Unsafeact unsafeact, int org);
        IEnumerable<UnsafeactsListVM> ToUnsafeactsListVM(IEnumerable<Data.Entities.Unsafeact> unsafeact);
        UnsafeactVM ToUnsafeactsVMNew(int org);
        Task<Data.Entities.Unsafeact> ToUnsafeactAsync(UnsafeactVM model, bool isNew);
        IEnumerable<ActionsMatrixVM> ToActionsMatrixVM(IEnumerable<Accion> lista);
    }   
}