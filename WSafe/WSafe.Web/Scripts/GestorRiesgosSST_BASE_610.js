﻿// Agregar funcionlidad principal del lado del cliente...
//mostrar resultados

function mostrarPlanAcc() {
    $(".tabGesSeguiPlanAcc").css("display", "none");
    var accionID = $("#txtAccionID").val();
    $.ajax({
        url: "/Acciones/ListarPlanAccion/",
        data: { idAccion: accionID },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,                                               // si es asincrónico o no
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                var indicador = "NO"
                if (item.Prioritaria == true) { indicador = "SI" };
                html += '<tr>';
                html += '<td>' + item.Categoria + '</td>';
                html += '<td>' + item.Accion + '</td>';
                html += '<td>' + indicador + '</td>';
                html += '<td>' + item.Responsable + '</td>';
                html += '<td>' + item.Costos + '</td>';
                html += '<td>' + item.FechaInicial + '</td>';
                html += '<td>' + item.FechaFinal + '</td>';
                html += '<td><a href="#" onclick="return getPlanByID(' + item.ID + ')">Editar</a> | <a href = "#" onclick = "DeletePlan(' + item.ID + ')"> Borrar</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
            $('.tabGesPlanAcc').css("display", "block");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function convertProperty(item, key) {
    // Indica el estado de una propiedad al visualizarla
    if (item == true) {
        $('#key').prop('checked', true);
        return $('#key').val(true);
    } else {
        $('#key').prop('checked', false);
        return $('#key').val(false);
    }
}

//mostrar resultados
function mostrarSeguimAcc() {
    $(".tabGesSeguiPlanAcc").css("display", "none");
    var accionID = $("#txtAccionID").val();
    $.ajax({
        url: "/Acciones/ListarSeguimientoAccion/",
        data: { idAccion: accionID },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,                                               // si es asincrónico o no
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.FechaSeguimiento + '</td>';
                html += '<td>' + item.Resultado + '</td>';
                html += '<td>' + item.Responsable + '</td>';
                html += '<td><a href="#" onclick="return getSeguiByID(' + item.ID + ')">Editar</a> | <a href = "#" onclick = "DeleteSegui(' + item.ID + ')"> Borrar</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
            $('.tabGesSeguiPlanAcc').css("display", "block");
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

validarFechaIni = function () {
    var fecha = Date.now();
    var item = $("#idFechaIni").val();
    var date = Date.parse(item);

    if (date > fecha) {
        alert("Fecha inicial incorrecta !!");
        return false;
    };
}

validarFechaFin = function () {
    var item = $("#idFechaIni").val();
    var fechaIni = Date.parse(item);
    var item = $("#idFechaFin").val();
    var fechaFin = Date.parse(item);

    if (fechaIni > fechaFin) {
        alert("Fecha final incorrecta !!");
        return false;
    };
}

validarSigue = function () {
    var item = $("#idFechaIni").val();
    var fechaIni = Date.parse(item);
    var item = $("#idFechaFin").val();
    var fechaFin = Date.parse(item);
    var item = $("#txtFechaSeg").val();
    var fechaSigue = Date.parse(item);

    if (fechaSigue > fechaFin || fechaSigue < fechaIni) {
        alert("Fecha seguimiento incorrecta !!");
        return false;
    };
}

validarCostos = function () {
    var item = $("#idCostos").val();
    if (item < 0) {
        alert("El consto no puede ser inferior a cero");
        return false;
    }
}

function getPlanByID(PlanID) {
    // Trae la acción a modificar
    //ClearTextBox();
    $(".tabPlanAcc").css("display", "block");
    $("#_EditarPlanAcc").css("display", "block");
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
            $("#txtCausa").val(result.Causa);
            $("#accion").val(result.Accion);
            $("#idRespons").val(result.TrabajadorID);
            $("#idPrioritaria").val(result.Prioritaria);
            $("#idCostos").val(result.Costos);
            $("#txtPlanAccionID").val(result.ID);
            $("#txtAccionID").val(result.AccionID);

            $("#txtAccionID").val(result.AccionID);
            $("#planAccionID").val(result.ID);
            if (result.Prioritaria == true) {
                $("#idPrioritaria").prop('checked', true);
            } else {
                $("#idPrioritaria").prop('checked', false);
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function getSeguiByID(seguiID) {
    // TODO
    $(".tabSeguimAcc").css("display", "block");
    $("#_EditarSigueAcc").css("display", "block");
    $.ajax({
        async: true,
        type: 'GET',
        url: "/Acciones/UpdateSeguimientoAccion",
        data: { ID: seguiID },
        dataType: "json",
        contentType: "application/json;charset=UTF-8",
        success: function (result) {
            $("#txtFechaSeg").val(result.FechaSeguimiento);
            $("#txtResultado").val(result.Resultado);
            $("#idRespons").val(result.TrabajadorID);

            $("#txtAccionID").val(result.AccionID);
            $("#sigueAccionID").val(result.ID);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function DeleteSegui(id) {
    //TODO
    $.ajax({
        url: "/Acciones/DeleteSeguimientoAccion/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        async: true,                                               // si es asincrónico o no
        success: function (result) {
            var text = "";
            text += "Esta seguro de querer borrar este registro ? :\n\n";
            text += "Fecha seguimiento : " + result.FechaSeguimiento + "\n";
            text += "Resultado : " + result.Resultado + "\n";
            text += "Responsable : " + result.Responsable + "\n";
            var respuesta = confirm(text);

            if (respuesta == true) {
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
                alert("El registro ha sido borrado exitosamente");
            }
            ClearTextBox();
            mostrarSeguimAcc();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function DeletePlan(id) {
    $.ajax({
        url: "/Acciones/DeletePlanAccion/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        async: true,                                               // si es asincrónico o no
        success: function (result) {
            var text = "";
            text += "Esta seguro de querer borrar este registro ? :\n\n";
            text += "Fecha inicial : " + result.FechaInicial + "\n";
            text += "Fecha final : " + result.FechaFinal + "\n";
            text += "Causa : " + result.Categoria + "\n";
            text += "Accion : " + result.Accion + "\n";
            text += "Responsable : " + result.Responsable + "\n";
            var respuesta = confirm(text);

            if (respuesta == true) {
                $.ajax({
                    url: "/Acciones/DeletePlanAccion/" + id,
                    type: "POST",
                    contentType: "application/json;charset=UTF-8",
                    dataType: "json",
                    async: true,                                               // si es asincrónico o no
                    success: function (result) {
                        mostrarPlanAcc();
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        alert(xhr.status);
                        alert(thrownError);
                    }
                });
                alert("El registro ha sido borrado exitosamente");
            }
            ClearTextBox();
            mostrarPlanAcc();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function DeleteAccion(ID) {
    var id = ID;
    $.ajax({
        url: "/Acciones/DeleteAccion/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        async: true,                                               // si es asincrónico o no
        success: function (result) {
            if (result.data == false) {
                alert(result.error);
            } else {
                var text = "";
                text += "Esta seguro de querer borrar este registro ? :\n\n";
                text += "Fecha solicitud : " + result.data.FechaSolicitudStr + "\n";
                text += "Categoria : " + result.data.Categoria + "\n";
                text += "Origen : " + result.data.Origen + "\n";
                text += "Descripcion : " + result.data.Descripcion + "\n";
                text += "Eficacia antes : " + result.data.EficaciaAntes + "\n";
                text += "Eficacia despues : " + result.data.EficaciaDespues + "\n";
                text += "Fecha cierre : " + result.data.FechaCierreStr + "\n";
                text += "Efectiva : " + result.data.Efectiva + "\n";
                text += "Estado : " + result.data.Estado + "\n";
                var respuesta = confirm(text);

                if (respuesta == true) {
                    $.ajax({
                        url: "/Acciones/DeleteAccion/" + id,
                        type: "POST",
                        contentType: "application/json;charset=UTF-8",
                        dataType: "json",
                        async: true,
                        success: function (response) {
                            alert(response.message);
                            location.reload(true);
                        },
                        error: function (xhr, ajaxOptions, thrownError) {
                            alert(xhr.status);
                            alert(thrownError);
                        }
                    });
                }

            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
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

function UpdatePlanAcc() {
    // Actualiza una accion creada, captura la accionID de id = txtAccionID
    // llama la acción del controlador UpdatePlanAccion
    $(".tabPlanAcc").css("display", "none");
    $(".tabGesPlanAcc").css("display", "none");
    $(".tabAddPlanAcc").css("display", "none");
    if ($("#idPrioritaria").is(':checked')) {
        $("#idPrioritaria").val(true)
    }
    else {
        $("#idPrioritaria").val(false)
    }

    if ($("#txtCausa").val() == 0) { $("#txtCausa").val(1) };
    if ($("#idPrioritaria").val() == null) { $("#idPrioritaria").val(false) };
    var planAccionVM = {
        ID: $("#planAccionID").val(),
        AccionID: $("#txtAccionID").val(),
        FechaInicial: $("#idFechaIni").val(),
        FechaFinal: $("#idFechaFin").val(),
        Causa: $("#txtCausa").val(),
        Accion: $("#accion").val(),
        TrabajadorID: $("#idRespons").val(),
        Prioritaria: $("#idPrioritaria").val(),
        Costos: $("#idCostos").val()
    };
    $.ajax({
        type: "POST",
        url: "/Acciones/UpdatePlanAccion",
        data: { planAccion: planAccionVM },
        dataType: "json",
        success: function (result) {
            $(".tabGesPlanAcc").css("display", "block");
            mostrarPlanAcc();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function AddSeguiAcc() {
    //TODO
    $(".tabSeguimAcc").css("display", "none");
    $(".tabSeguimAcc").css("display", "none");
    $(".tabAddSeguimAcc").css("display", "none");
    var accionID = $("#txtAccionID").val();
    var seguimientoVM = {
        ID: "0",
        AccionID: accionID,
        FechaSeguimiento: $("#idFechaSegui").val(),
        Resultado: $("#idResultado").val(),
        TrabajadorID: $("#trabajadorID").val()
    };
    $.ajax({
        type: "POST",
        url: "/Acciones/CreateSeguimientoPlan",
        data: { seguimientoAccion: seguimientoVM },
        dataType: "json",
        success: function (seguimientoPlan) {
            alert("El registro ha sido ingresado exitosamente");
            ClearTextBox();
            mostrarSeguimAcc();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function UpdateSigueAcc() {
    //TODO
    // Actualiza un seguimiento a una accion, captura la accionID de id = txtAccionID
    // llama la acción del controlador UpdatePlanAccion
    $(".tabSeguimAcc").css("display", "none");
    $(".tabGesSeguiPlanAcc").css("display", "none");
    $(".tabAddSeguimAcc").css("display", "none");
    var sigueAccionVM = {
        ID: $("#sigueAccionID").val(),
        AccionID: $("#txtAccionID").val(),
        FechaSeguimiento: $("#txtFechaSeg").val(),
        Resultado: $("#txtResultado").val(),
        TrabajadorID: $("#responsID").val(),
    };
    $.ajax({
        type: "POST",
        url: "/Acciones/UpdateSeguimientoAccion",
        data: { model: sigueAccionVM },
        dataType: "json",
        success: function (result) {
            $(".tabGesSeguiPlanAcc").css("display", "block");
            mostrarSeguimAcc;
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}
imprimirPdf = function (id) {
    alert("Descargando pdf..."+id)
}