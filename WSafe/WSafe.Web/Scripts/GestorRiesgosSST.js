// Agregar funcionlidad principal del lado del cliente...

function AddPlanAccion(accionID, mostrar) {
    $(".tabAddPlanAcc").css("display", "none");
    $(".tabPlanAcc").css("display", "none");
    if ($("#idPrioritaria").is(':checked')) {
        $("#idPrioritaria").val(true)
    }
    else {
        $("#idPrioritaria").val(false)
    }

    if ($("#idPrioritaria").val() == null) { $("#idPrioritaria").val(false) };
    $.ajax({
        type: "POST",
        url: "/Acciones/CreatePlanAccion",
        data: {
            ID: 0,
            AccionID: accionID,
            FechaInicial: $("#idFechaIni").val(),
            FechaFinal: $("#idFechaFin").val(),
            Causa: $("#idCausa").val(),
            Accion: $("#accion").val(),
            TrabajadorID: $("#idRespons").val(),
            Prioritaria: $("#idPrioritaria").val(),
            Costos: $("#idCostos").val()
        },
        dataType: "json",
        success: function (result) {
            alert("El registro ha sido ingresado exitosamente");
            if (mostrar) {
                mostrarPlanAcc();
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function ClearTextBox() {
    $("#idFechaIni").val("");
    $("#idFechaFin").val("");
    $("#idCausa").val("");
    $("#accion").val("");
    $("#idRespons").val("");
    $("#idPrioritaria").val(false);
    $("#idCostos").val("");
    $(".tabGesPlanAcc").css("display", "none");
    $('.tabAddPlanAcc').css("display", "none");
    $('.tabPlanAcc').css("display", "none");
    $('.tabSeguimAcc').css("display", "none");
}

validarFecha = function (fecha) {
    alert("validando fecha : " + fecha);
}

validarCostos = function (valor) {
    alert("validando costos : " + valor);
}

function AddSeguimAccion(accionID, mostrar) {
    $(".tabAddSeguimAcc").css("display", "none");
    $(".tabSeguimAcc").css("display", "none");
    $.ajax({
        type: "POST",
        url: "/Acciones/CreateSeguimientoPlan",
        data: {
            ID: 0,
            AccionID: accionID,
            FechaSeguimiento: $("#idFechaSegui").val(),
            Resultado: $("#idResultado").val(),
            TrabajadorID: $("#idRespons").val(),
        },
        dataType: "json",
        success: function (result) {
            alert("El registro ha sido ingresado exitosamenrte");
            if (mostrar) {
                mostrarSeguimAcc();
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function getPlanByID(PlanID) {
    //ClearTextBox();
    $(".tabPlanAcc").css("display", "block");
    $("#_EditarPlanAcc").css("display", "block");
    planID = PlanID;
    $.ajax({
        async: true,
        type: 'GET',
        url: "/Acciones/UpdatePlanAccion",
        data: { ID: PlanID },
        dataType: "json",
        contentType: "application/json;charset=UTF-8",
        success: function (result) {
            $("#idFechaIni").val(result.FechaInicial);
            $("#idFechaFin").val(result.FechaFinal);
            $("#idCausa").val(result.Causa);
            $("#accion").val(result.Accion);
            $("#idRespons").val(result.TrabajadorID);
            $("#idPrioritaria").val(result.Prioritaria);
            $("#idCostos").val(result.Costos);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function getSeguiByID(seguiID) {
    $(".tabSeguimAcc").css("display", "block");
    $.ajax({
        async: true,
        type: 'GET',
        url: "/Acciones/UpdateSeguimientoAccion",
        data: { ID: seguiID },
        dataType: "json",
        contentType: "application/json;charset=UTF-8",
        success: function (result) {
            var date = new Date(parseInt(result.FechaSeguimiento.substr(6)));
            $("#idFechaSegui").val(date);
            $("#txtResultado").val(result.Resultado);
            $("#txtRespons").val(result.TrabajadorID);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function DeleteSegui(id) {
    var ans = confirm("Esta seguro de querer borrar este registro ?");
    if (ans) {
        $.ajax({
            url: "/Acciones/DeleteSeguimientoAccion/" + id,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            async: true,                                               // si es asincrónico o no
            success: function (result) {
                mostrarSegimAcc();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status);
                alert(thrownError);
            }
        });
    }
}

function DeletePlan(id) {
    var ans = confirm("Esta seguro de querer borrar este registro ?");
    if (ans) {
        $.ajax({
            url: "/Acciones/DeletePlanAccion/" + id,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            async: true,                                               // si es asincrónico o no
            success: function (result) {
                mostrarSeguimAcc();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status);
                alert(thrownError);
            }
        });
    }
}
