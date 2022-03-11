// Agregar funcionlidad principal del lado del cliente...

function AddPlanAccion(accionID, mostrar) {
    $(".tabAddPlanAcc").css("display", "none");
    $(".tabPlanAcc").css("display", "none");
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
    $("#idPrioritaria").val("");
    $("#idCostos").val("");
    $(".tabGesPlanAcc").css("display", "none");
    $('.tabAddPlanAcc').css("display", "none");
    $('.tabPlanAcc').css("display", "none");
    $('.tabSeguimAcc').css("display", "none");
}