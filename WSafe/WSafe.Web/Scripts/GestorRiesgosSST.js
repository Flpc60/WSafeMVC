// Agregar funcionlidad principal del lado del cliente...

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
            var txtfecha = JSON.stringify(result.FechaSeguimiento);
            //var fechaSigue = txtfecha.slice(0, 10).split("-").reverse().join("/");
            //document.getElementById("txtFechaSeg").innerHTML = fechaSigue;
            $("#txtFechaSeg").val(result.FechaSeguimiento);
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

// las siguientes funciones cran una nueva accion, plan acción y nuevo seguimiento
function AddAccion() {
    // Crea una nueva acción, para una nueva no conformidad
    // Retorna la accionID, y se la pasa a PlanAccion y SeguimientoAccion
    var fechaCierre = $("#fechaSolicitud").val();
    $(".tabIdCausas").css("display", "none");
    if ($("#idEfectiva").is(':checked')) {
        $("#idEfectiva").val(true)
    }
    else {
        $("#idEfectiva").val(false)
    }
    if ($("#idEstado").is(':checked')) {
        $("#idEstado").val(true)
    }
    else {
        $("#idEstado").val(false)
    }
    if ($("#idFechaCierre").is(':empty')) {
        $("#idFechaCierre").val(fechaCierre)
    }

    var accionVM = {
        ID: "0",
        ZonaID: $("#zona").val(),
        ProcesoID: $("#proceso").val(),
        ActividadID: $("#activity").val(),
        TareaID: $("#tarea").val(),
        FechaSolicitud: $("#fechaSolicitud").val(),
        Categoria: $("#categoriaAcc").val(),
        TrabajadorID: $("#idTrabajador").val(),
        FuenteAccion: $("#idFuente").val(),
        Descripcion: $("#idDescrip").val(),
        EficaciaAntes: $("#idEficaciaAnt").val(),
        EficaciaDespues: $("#idEficaciaDesp").val(),
        FechaCierre: $("#idFechaCierre").val(),
        Efectiva: $("#idEfectiva").val(),
        Estado: $("#idEstado").val()
    };

    $.ajax({
        type: "POST",
        url: "/Acciones/CreateAccion",
        data: { model: accionVM },
        dataType: "json",
        success: function (idAccion) {
            $("#txtAccionID").val(idAccion);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function UpdateAccion(id) {
    $(".tabIdCausas").css("display", "none");
    $("#txtAccionID").val(id);
    var accionVM = {
        ID: $("#txtAccionID").val(),
        ZonaID: $("#zona").val(),
        ProcesoID: $("#proceso").val(),
        ActividadID: $("#activity").val(),
        TareaID: $("#tarea").val(),
        FechaSolicitud: $("#fechaSolicitud").val(),
        Categoria: $("#categoriaAcc").val(),
        TrabajadorID: $("#idTrabajador").val(),
        FuenteAccion: $("#idFuente").val(),
        Descripcion: $("#idDescrip").val(),
        EficaciaAntes: $("#idEficaciaAnt").val(),
        EficaciaDespues: $("#idEficaciaDesp").val(),
        FechaCierre: $("#idFechaCierre").val(),
        Efectiva: $("#idEfectiva").val(),
        Estado: $("#idEstado").val()
    };

    $.ajax({
        type: "POST",
        url: "/Acciones/UpdateAccion",
        data: { model: accionVM },
        dataType: "json",
        success: function (idAccion) {
            $("#txtAccionID").val(idAccion);
            alert("Actualzación realizada exitosamente !!")
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function AddPlanAccion() {
    // Crea una nueva accion, captura la accionID de id = txtAccionID
    // llama la acción del controlador CreatePlanAccion
    $(".tabAddPlanAcc").css("display", "none");
    $(".tabPlanAcc").css("display", "none");
    if ($("#idPrioritaria").is(':checked')) {
        $("#idPrioritaria").val(true)
    }
    else {
        $("#idPrioritaria").val(false)
    }

    if ($("#idPrioritaria").val() == null) { $("#idPrioritaria").val(false) };
    var accionID = $("#txtAccionID").val();
    var planAccionVM = {
        ID: 0,
        AccionID: accionID,
        FechaInicial: $("#idFechaIni").val(),
        FechaFinal: $("#idFechaFin").val(),
        Causa: $("#idCausa").val(),
        Accion: $("#accion").val(),
        TrabajadorID: $("#idRespons").val(),
        Prioritaria: $("#idPrioritaria").val(),
        Costos: $("#idCostos").val()
    };

    $.ajax({
        type: "POST",
        url: "/Acciones/CreatePlanAccion",
        data: { model: planAccionVM },
        dataType: "json",
        success: function (result) {
            alert("El registro ha sido ingresado exitosamente");
            ClearTextBox();
            mostrarPlanAcc();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function AddSeguiAcc() {
    $(".tabSeguimAcc").css("display", "none");
    $(".tabSeguimAcc").css("display", "none");
    var accionID = $("#txtAccionID").val();
    var seguimientoVM = {
        ID: "0",
        AccionID: accionID,
        FechaSeguimiento: $("#idFechaSegui").val(),
        Resultado: $("#idResultado").val(),
        TrabajadorID: $("#idRespons").val(),
    };

    $.ajax({
        type: "POST",
        url: "/Acciones/CreateSeguimientoPlan",
        data: { seguimientoAccion: seguimientoVM },
        dataType: "json",
        success: function (seguimientoPlan) {
            ClearTextBox();
            mostrarSeguimAcc();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}