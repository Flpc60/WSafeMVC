// Agregar funcionlidad principal del lado del cliente...

function AddInterven() {
    $(".tabMediAplica").css("display", "none");
    var riesgoID = $("#txtRiesgoID").val();
    var aplicaVM = {
        ID: "0",
        RiesgoID: riesgoID,
        Nombre: $("#txtNombre").val(),
        CategoriaAplicacion: $("#txtCatAplica").val(),
        Finalidad: $("#txtFinal").val(),
        Intervencion: $("#idInterven").val(),
        Beneficios: $("#idBeneficio").val(),
        Presupuesto: $("#idPresup").val(),
        TrabajadorID: $("#idRespons").val(),
        FechaInicial: $("#idFechaIni").val(),
        Fechafinal: $("#idFechaFin").val(),
        Observaciones: $("#idObserv").val()
    };

    $.ajax({
        type: "POST",
        url: "/Riesgos/AddIntervenciones",
        data: { model: aplicaVM },
        dataType: "json",
        success: function (result) {
            if (result.data != false) {
                $("#txtIntervenID").val(result.data);
                $("#btnAddInterven").hide();
                $(".tabAddInterven").css("display", "none");
            }
            alert(result.mensaj);
            ClearTextBox();
            mostrarInterven();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function mostrarInterven() {
//    $(".tabMediAplica").css("display", "none");
    var riesgoID = $("#txtRiesgoID").val();
    var aplicaID = $("#txtIntervenID").val();

    $.ajax({
        type: "GET",
        url: '/Riesgos/GetIntervenciones',
        data: { idRiesgo: riesgoID },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.Nombre + '</td>';
                html += '<td>' + item.TextCategoria + '</td>';
                html += '<td>' + item.TextIntervencion + '</td>';
                html += '<td>' + item.Responsable + '</td>';
                html += '<td>' + item.TextFechaInicial + '</td>';
                html += '<td>' + item.TextFechaFinal + '</td>';
                html += '<td><a href="#" onclick="return getIntervenByID(' + item.ID + ')">Editar</a> | <a href = "#" onclick = "DeleteInterven(' + item.ID + ')"> Borrar</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
            $('.tabMediAplica').css("display", "block");
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) { // función que va a ejecutar si hubo algún tipo de error en el pedido
            var error = eval("(" + XMLHttpRequest.responseText + ")");
            alert(error.Message);
        }
    });
}

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

function mostrarSeguimAcc() {
    var accionID = $("#txtAccionID").val();
    $.ajax({
        url: "/Acciones/ListarSeguimientoAccion/",
        data: { idAccion: accionID },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,
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

function ConsultarLesionados() {
    var incidenteID = $("#txtIncidenteID").val();
    $.ajax({
        type: "GET",                                            // tipo de request que estamos generando
        url: '/Incidentes/GetAllLesionados',                                      // URL al que vamos a hacer el pedido
        data: { idIncidente: incidenteID },                                         // data es un arreglo JSON que contiene los parámetros que
        contentType: "application/json; charset=utf-8",            // tipo de contenido
        dataType: "json",                                          // formato de transmición de datos
        async: true,                                               // si es asincrónico o no
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.Documento + '</td>';
                html += '<td>' + item.NombreCompleto + '</td>';
                html += '<td>' + item.Genero + '</td>';
                html += '<td>' + item.EstadoCivil + '</td>';
                html += '<td>' + item.TipoVinculacion + '</td>';
                html += '<td>' + item.Cargo + '</td>';
                html += '<td><a href = "#" onclick = "DeleteLesionado(' + item.ID + ')"> Borrar</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
            $('.tabLesionados').css("display", "block");
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) { // función que va a ejecutar si hubo algún tipo de error en el pedido
            var error = eval("(" + XMLHttpRequest.responseText + ")");
            aler(error.Message);
        }
    });
}

function ShowRoles() {
    // Mostrar todos los roles
    $.ajax({
        url: "/Accounts/GetAllRoles/",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.ID + '</td>';
                html += '<td>' + item.Name + '</td>';
                html += '<td><a href = "#" onclick = "DeleteRole(' + item.ID + ')"> Borrar</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
            $('.tabGesRoles').css("display", "block");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function ShowAuthorizations() {
    // Mostrar todos los roles
    $.ajax({
        url: "/Accounts/GetAllAuthorizations/",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.ID + '</td>';
                html += '<td>' + item.Role + '</td>';
                html += '<td>' + item.Component + '</td>';
                html += '<td>' + item.Operation + '</td>';
                html += '<td><a href = "#" onclick = "DeleteRoleOperation(' + item.ID + ')"> Borrar</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
            $('.tabGesAuthorize').css("display", "block");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function ShowZones() {
    // Mostrar todas las zonas
    $.ajax({
        url: "/Organizations/GetAllZonas",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.ID + '</td>';
                html += '<td>' + item.Descripcion + '</td>';
                html += '<td><a href = "#" onclick = "DeleteZone(' + item.ID + ')"> Borrar</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
            $('.tabGesZones').css("display", "block");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function ShowCargos() {
    // Mostrar todos los cargos
    $.ajax({
        url: "/Organizations/GetAllCargos",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.ID + '</td>';
                html += '<td>' + item.Descripcion + '</td>';
                html += '<td><a href = "#" onclick = "DeleteCargo(' + item.ID + ')"> Borrar</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
            $('.tabGesCargos').css("display", "block");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function ShowProcess() {
    // Mostrar todos los procesos
    $.ajax({
        url: "/Organizations/GetAllProcess",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.ID + '</td>';
                html += '<td>' + item.Descripcion + '</td>';
                html += '<td><a href = "#" onclick = "DeleteProcess(' + item.ID + ')"> Borrar</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
            $('.tabGesProcess').css("display", "block");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function ShowActivitys() {
    // Mostrar todos los procesos
    $.ajax({
        url: "/Organizations/GetAllActivitys",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.ID + '</td>';
                html += '<td>' + item.Descripcion + '</td>';
                html += '<td><a href = "#" onclick = "DeleteActivity(' + item.ID + ')"> Borrar</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
            $('.tabGesActivitys').css("display", "block");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function ShowTasks() {
    // Mostrar todos los procesos
    $.ajax({
        url: "/Organizations/GetAllTasks",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.ID + '</td>';
                html += '<td>' + item.Descripcion + '</td>';
                html += '<td><a href = "#" onclick = "DeleteTask(' + item.ID + ')"> Borrar</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
            $('.tabGesTasks').css("display", "block");
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
    $("#txtNombre").val("");
    $("#txtCatAplica").val("");
    $("#txtFinal").val("");
    $("#idInterven").val("");
    $("#idBeneficio").val("");
    $("#idPresup").val("");
    $("#idObserv").val("");
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

function getIntervenByID(intervenID) {
    $(".tabMediAplica").css("display", "none");
    aplicaID = intervenID;
    $.ajax({
        async: true,
        type: 'GET',
        url: "/Riesgos/UpdateIntervencion",
        data: { id: intervenID },
        dataType: "json",
        contentType: "application/json;charset=UTF-8",
        success: function (result) {
            $("#txtIntervenID").val(result.ID);
            $("#txtRiesgoID").val(result.RiesgoID);
            $("#txtNombre").val(result.Nombre);
            $("#txtCatAplica").val(result.CategoriaAplicacion);
            $("#idInterven").val(result.Intervencion);
            $("#idBeneficio").val(result.Beneficios);
            $("#idPresup").val(result.Presupuesto);
            $("#idRespons").val(result.TrabajadorID);
            $("#idFechaIni").val(result.FechaInicial);
            $("#idFechaFin").val(result.FechaFinal);
            $("#idObserv").val(result.Observaciones);
        },
         error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    $(".tabAddInterven").css("display", "block");
    $("#btnAddInterven").hide();
    $("#btnUpdInterven").show();
}

function getPlanByID(PlanID) {
    $("#btnAddPlan").hide();
    $("#btnUpdPlan").show();
    $(".tabAddPlanAcc").css("display", "block");
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
    $("#btnAddSigue").hide();
    $("#btnUpdSigue").show();
    $(".tabAddSeguimAcc").css("display", "block");
//    $("#_EditarSigueAcc").css("display", "block");
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
            $("#txtRespons").val(result.TrabajadorID);
            $("#txtAccionID").val(result.AccionID);
            $("#sigueAccionID").val(result.ID);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function DeleteSegui(id) {
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

function DeleteInterven(id) {
//TODO
    $.ajax({
        url: "/Riesgos/DeleteIntervencion/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        async: true,
        success: function (result) {
            var text = "";
            text += "Esta seguro de querer borrar este registro ? :\n\n";
            text += "Descripción : " + result.data.Nombre + "\n";
            text += "Categoria : " + result.data.TextCategoria + "\n";
            text += "Intervención : " + result.data.TextIntervencion + "\n";
            text += "Presupuesto : " + result.data.Presupuesto + "\n";
            text += "Responsable : " + result.data.Responsable + "\n";
            text += "Fecha inicial : " + result.data.TextFechaInicial + "\n";
            text += "Fecha final : " + result.data.TextFechaFinal + "\n";
            text += "Observaciones : " + result.data.Observaciones + "\n";
            var respuesta = confirm(text);

            if (respuesta == true) {
                $.ajax({
                    url: "/Riesgos/DeleteIntervencion/" + id,
                    type: "POST",
                    contentType: "application/json;charset=UTF-8",
                    dataType: "json",
                    async: true,
                    success: function (result) {
                        mostrarInterven();
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        alert(xhr.status);
                        alert(thrownError);
                    }
                });
                alert("El registro ha sido borrado exitosamente");
            }
            ClearTextBox();
            mostrarInterven();
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
        async: true,                                             // si es asincrónico o no
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

function DeleteLesionado(id) {
    $.ajax({
        url: "/Incidentes/DeleteLesionado/" + id,
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        async: true,
        success: function (result) {
            ConsultarLesionados();
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

function DeleteIncident(ID) {
    var id = ID;
    $.ajax({
        url: "/Incidentes/DeleteIncident/" + id,
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
                text += "Fecha reporte : " + result.data.FechaReportStr + "\n";
                text += "Fecha incidente : " + result.data.FechaIncidentStr + "\n";
                text += "Categoria : " + result.data.CategoriasIncidente + "\n";
                text += "Naturaleza Lesion : " + result.data.NaturalezaLesion + "\n";
                text += "Descripcion : " + result.data.DescripcionIncidente + "\n";
                text += "Consecuencias Daño : " + result.data.ConsecuenciasDaño + "\n";
                text += "Probabilidad : " + result.data.Probabilidad + "\n";
                var respuesta = confirm(text);

                if (respuesta == true) {
                    $.ajax({
                        url: "/Incidentes/DeleteIncident/" + id,
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

function DeleteRiesgo(ID) {
    var id = ID;
    $.ajax({
        url: "/Riesgos/DeleteRiesgo/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        async: true,
        success: function (result) {
            if (result.data == false) {
                alert(result.error);
            } else {
                var text = "";
                text += "Esta seguro de querer borrar este registro ? :\n\n";
                text += "Zona : " + result.ZonaID + "\n";
                text += "Proceso : " + result.ProcesoID + "\n";
                text += "Actividad : " + result.ActividadID +"\n";
                text += "Tarea : " + result.TareaID + "\n";
                text += "Rutinaria : " + result.Rutinaria + "\n";
                text += "Clasificación : " + result.PeligroID +"\n";
                text += "Efectos posibles : " + result.EfectosPosibles + "\n";
                text += "Nivel deficiencia : " + result.NivelDeficiencia +"\n";
                text += "Nivel exposición : " + result.NivelExposicion +"\n";
                text += "Nivel consecuencia : " + result.NivelConsecuencia + "\n";
                text += "Aceptabilidad : " + result.AceptabilidadNR +"\n";
                text += "Nro. expuestos : " + result.NroExpuestos +"\n";
                text += "Requisito legal : " + result.RequisitoLegal +"\n";

                var respuesta = confirm(text);

                if (respuesta == true) {
                    $.ajax({
                        url: "/Riesgos/DeleteRiesgo/" + id,
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

function DeleteZone(id) {
    // Eliminar zonas
    var text = "";
    text += "Esta seguro de querer borrar esta zona ? :" + id + "\n\n";
    var respuesta = confirm(text);
    if (respuesta == true) {
        $.ajax({
            url: "/Organizations/DeleteZone/" + id,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            async: true,                                               // si es asincrónico o no
            success: function (result) {
                alert(result.mensaj);
                ShowZones();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status);
                alert(thrownError);
            }
        });
    }
}

function DeleteCargo(id) {
    // Eliminar cargo
    var text = "";
    text += "Esta seguro de querer borrar este cargo ? :" + id + "\n\n";
    var respuesta = confirm(text);
    if (respuesta == true) {
        $.ajax({
            url: "/Organizations/DeleteCargo/" + id,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            async: true,                                               // si es asincrónico o no
            success: function (result) {
                alert(result.mensaj);
                ShowCargos();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status);
                alert(thrownError);
            }
        });
    }
}

function DeleteProcess(id) {
    // Eliminar procesos
    var text = "";
    text += "Esta seguro de querer borrar este proceso ? :" + id + "\n\n";
    var respuesta = confirm(text);
    if (respuesta == true) {
        $.ajax({
            url: "/Organizations/DeleteProcess/" + id,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            async: true,                                               // si es asincrónico o no
            success: function (result) {
                alert(result.mensaj);
                ShowProcess();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status);
                alert(thrownError);
            }
        });
    }
}

function DeleteActivity(id) {
    // Eliminar actividad
    var text = "";
    text += "Esta seguro de querer borrar esta actividad ? :" + id + "\n\n";
    var respuesta = confirm(text);
    if (respuesta == true) {
        $.ajax({
            url: "/Organizations/DeleteActivity/" + id,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            async: true,                                               // si es asincrónico o no
            success: function (result) {
                alert(result.mensaj);
                ShowActivitys();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status);
                alert(thrownError);
            }
        });
    }
}

function DeleteTask(id) {
    // Eliminar tarea
    var text = "";
    text += "Esta seguro de querer borrar esta tarea ? :" + id + "\n\n";
    var respuesta = confirm(text);
    if (respuesta == true) {
        $.ajax({
            url: "/Organizations/DeleteTask/" + id,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            async: true,                                               // si es asincrónico o no
            success: function (result) {
                alert(result.mensaj);
                ShowTasks();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status);
                alert(thrownError);
            }
        });
    }
}

function DeleteRole(id) {
    // Eliminar un rol
    var text = "";
    text += "Esta seguro de querer borrar este rol ? :" + id + "\n\n";
    var respuesta = confirm(text);
    if (respuesta == true) {
        $.ajax({
            url: "/Accounts/DeleteRole/" + id,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            async: true,                                               // si es asincrónico o no
            success: function (response) {
                alert(response.mensaj);
                ShowRoles();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status);
                alert(thrownError);
            }
        });
    }
}

function DeleteRoleOperation(id) {
    // Eliminar un rol
    var text = "";
    text += "Esta seguro de querer borrar esta autorización ? :" + id + "\n\n";
    var respuesta = confirm(text);
    if (respuesta == true) {
        $.ajax({
            url: "/Accounts/DeleteAuthorization/" + id,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            async: true,                                               // si es asincrónico o no
            success: function (result) {
                alert(result.mensaj);
                ShowAuthorizations();
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
            $("#btnAddAction").hide();
            $("#btnCanAccc").hide();
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

function UpdateOrganization(id) {
// Actualiza datos básicos de la empresa
    $(".tabBasics").css("display", "none");
    var organizaVM = {
        ID: id,
        NIT: $("#txtNit").val(),
        RazonSocial: $("#txtRazon").val(),
        Direccion: $("#txtDireccion").val(),
        Municip: $("#txtMunicip").val(),
        Department: $("#txtDepartment").val(),
        Telefono: $("#txtTelefono").val(),
        ARL: $("#txtArl").val(),
        ClaseRiesgo: $("#txtClaseRiesgo").val(),
        DocumentRepresent: $("#txtDocument").val(),
        NameRepresent: $("#txtName").val(),
        EconomicActivity: $("#txtEconomic").val(),
        NumeroTrabajadores: $("#txtNumero").val(),
        Products: $("#txtProducts").val(),
        Mision: $("#txtMision").val(),
        Vision: $("#txtVision").val(),
        Objetivos: $("#txtObjetivos").val(),
        Procesos: "Procesos",
        Organigrama: "Organigrama",
        TurnosAdministrativo: $("#txtAdministrativo").val(),
        TurnosOperativo: $("#txtOperativo").val()
    };

    $.ajax({
        type: "POST",
        url: "/Organizations/UpdateOrganization",
        data: { organization: organizaVM },
        dataType: "json",
        success: function (result) {
            alert(result.mensaj);
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
        Causa: $("#txtCausa").val(),
        Accion: $("#accion").val(),
        TrabajadorID: $("#idRespons").val(),
        Prioritaria: $("#idPrioritaria").val(),
        Costos: $("#idCostos").val(),
        Responsable: " "
    };

    $.ajax({
        type: "POST",
        url: "/Acciones/CreatePlanAccion",
        data: { model: planAccionVM },
        dataType: "json",
        success: function (result) {
            $("#txtPlanAccionID").val(result.ID);
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
        ID: $("#txtPlanAccionID").val(),
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
    $(".tabSeguimAcc").css("display", "none");
    $(".tabSeguimAcc").css("display", "none");
    $(".tabAddSeguimAcc").css("display", "none");
    var accionID = $("#txtAccionID").val();
    var seguimientoVM = {
        ID: "0",
        AccionID: accionID,
        FechaSeguimiento: $("#txtFechaSeg").val(),
        Resultado: $("#txtResultado").val(),
        TrabajadorID: $("#txtRespons").val()
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
        TrabajadorID: $("#txtRespons").val(),
    };
    $.ajax({
        type: "POST",
        url: "/Acciones/UpdateSeguimientoAccion",
        data: { model: sigueAccionVM },
        dataType: "json",
        success: function (result) {
            mostrarSeguimAcc;
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function AddNewRiesgo() {
    $(".tabPeligros").css("display", "none");
    if ($("#txtRequisito").is(':checked')) {
        $("#txtRequisito").val(true)
    }
    else {
        $("#txtRequisito").val(false)
    }
    if ($("#txtRutinaria").is(':checked')) {
        $("#txtRutinaria").val(true)
    }
    else {
        $("#txtRutinaria").val(false)
    }

    var riesgoVM = {
        ID: "0",
        ZonaID: $("#zona").val(),
        ProcesoID: $("#proceso").val(),
        ActividadID: $("#activity").val(),
        TareaID: $("#tarea").val(),
        Rutinaria: $("#txtRutinaria").val(),
        CategoriaPeligroID: $("#categoria").val(),
        PeligroID: $("#peligro").val(),
        EfectosPosibles: $("#txtEfectos").val(),
        NivelDeficiencia: $("#deficiencia").val(),
        NivelExposicion: $("#exposicion").val(),
        NivelConsecuencia: $("#consecuencia").val(),
        AceptabilidadNR: $("#txtAceptabilidad").val(),
        NroExpuestos: $("#txtExpuestos").val(),
        RequisitoLegal: $("#txtRequisito").val()
    };

    $.ajax({
        type: "POST",
        url: "/Riesgos/CreateRiesgo",
        data: { model: riesgoVM },
        dataType: "json",
        success: function (result) {
            if (result.data != false) {
                $("#txtRiesgoID").val(result.data);
                $("#btnAddRiesgo").hide();
                $(".tabEvalRiesgos").css("display", "none");
            }
            alert(result.mensaj);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function UpdateRiesgo() {
    $(".tabPeligros").css("display", "none");
    if ($("#txtRequisito").is(':checked')) {
        $("#txtRequisito").val(true)
    }
    else {
        $("#txtRequisito").val(false)
    }
    if ($("#txtRutinaria").is(':checked')) {
        $("#txtRutinaria").val(true)
    }
    else {
        $("#txtRutinaria").val(false)
    }

    var riesgoVM = {
        ID: $("#txtRiesgoID").val(),
        ZonaID: $("#zona").val(),
        ProcesoID: $("#proceso").val(),
        ActividadID: $("#activity").val(),
        TareaID: $("#tarea").val(),
        Rutinaria: $("#txtRutinaria").val(),
        CategoriaPeligroID: $("#categoria").val(),
        PeligroID: $("#peligro").val(),
        EfectosPosibles: $("#txtEfectos").val(),
        NivelDeficiencia: $("#deficiencia").val(),
        NivelExposicion: $("#exposicion").val(),
        NivelConsecuencia: $("#consecuencia").val(),
        AceptabilidadNR: $("#txtAceptabilidad").val(),
        NroExpuestos: $("#txtExpuestos").val(),
        RequisitoLegal: $("#txtRequisito").val()
    };

    $.ajax({
        type: "POST",
        url: "/Riesgos/UpdateRiesgo",
        data: { model: riesgoVM },
        dataType: "json",
        success: function (idRiesgo) {
            $("#txtRiesgoID").val(idRiesgo);
            $("#btnUpdRiesgo").hide();
            $(".tabEvalRiesgos").css("display", "none");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function UpdateIntervencion() {
    //TODO
    riesgoID = $("#txtRiesgoID").val();
    aplicaID = $("#txtIntervenID").val();

    var aplicaVM = {
        ID: aplicaID,
        RiesgoID: riesgoID,
        Nombre: $("#txtNombre").val(),
        CategoriaAplicacion: $("#txtCatAplica").val(),
        TrabajadorID: $("#idRespons").val(),
        Intervencion: $("#idInterven").val(),
        Beneficios: $("#idBeneficio").val(),
        Presupuesto: $("#idPresup").val(),
        FechaInicial: $("#idFechaIni").val(),
        FechaFinal: $("#idFechaFin").val(),
        Observaciones: $("#idObserv").val()
    };

    $.ajax({
        type: "POST",
        url: "/Riesgos/UpdateIntervencion",
        data: { model: aplicaVM },
        dataType: "json",
        success: function (result) {
            alert(result.mensaj);
            $(".tabMediAplica").css("display", "block");
            mostrarInterven();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function SetPeriodo() {
    var Periodo = [];
    $("input[name='month']:checked").each(function () {
        Periodo.push(this.value);
    });
    $("#txtPeriodo").val(Periodo);
    return Periodo;
}

function GestorActions() {
//Activa ventanas para gestionar crud de planes de acción y seguimientos
    $("#idCausas").click(function () {
        $(".tabIdCausas").css("display", "block");
        $(".tabAddPlanAcc").css("display", "none");
        $(".tabAddSeguiAcc").css("display", "none");
        $(".tabSeguimAcc").css("display", "none");
        $(".tabGesPlanAcc").css("display", "none");
        $(".tabGesSeguiPlanAcc").css("display", "none");
        $(".tabCerrar").css("display", "none");
    });

    $("#idCausas").dblclick(function () {
        $(".tabIdCausas").css("display", "none");
        $(".tabAddPlanAcc").css("display", "none");
        $(".tabAddSeguiAcc").css("display", "none");
        $(".tabSeguimAcc").css("display", "none");
        $(".tabCerrar").css("display", "block");
    });

    $("#planAcc").click(function () {
        $(".tabGesPlanAcc").css("display", "block");
        $(".tabSeguimAcc").css("display", "none");
        $(".tabCerrar").css("display", "none");
        mostrarPlanAcc();
    });

    $("#planAcc").dblclick(function () {
        $(".tabGesPlanAcc").css("display", "none");
        $(".tabPlanAcc").css("display", "none");
        $(".tabSeguimAcc").css("display", "none");
        $(".tabCerrar").css("display", "block");
    });

    $("#seguimAcc").click(function () {
        $(".tabGesSeguiPlanAcc").css("display", "block");
        $(".tabCerrar").css("display", "none");
        mostrarSeguimAcc();
    });

    $("#seguimAcc").dblclick(function () {
        $(".tabGesSeguiPlanAcc").css("display", "none");
        $(".tabSeguiPlanAcc").css("display", "none");
        $(".tabCerrar").css("display", "block");
    });

    $("#addPlanAcc").click(function () {
        //$(".tabGesPlanAcc").css("display", "none");
        $(".tabAddPlanAcc").css("display", "block");
        $(".tabCerrar").css("display", "none");
        $("#btnAddPlan").show();
        $("#btnUpdPlan").hide();
    });

    $("#addSeguiAcc").click(function () {
        //$(".tabGesPlanAcc").css("display", "none");
        $(".tabAddSeguimAcc").css("display", "block");
        $(".tabCerrar").css("display", "none");
        $("#btnAddSigue").show();
        $("#btnUpdSigue").hide();
    });

}

function GestorAuthorization() {
    //Activa ventanas para gestionar crud de authorizaciones
    $("#roles").click(function () {
        $(".tabGesRoles").css("display", "none");
        $(".tabCerrar").css("display", "none");
        ShowRoles();
    });
    $("#roles").dblclick(function () {
        $(".tabGesRoles").css("display", "none");
        $(".tabRoles").css("display", "none");
        $(".tabCerrar").css("display", "block");
    });

    $("#authorize").click(function () {
        $(".tabGesAuthorize").css("display", "none");
        $(".tabCerrar").css("display", "none");
        ShowRoles();
    });
    $("#roles").dblclick(function () {
        $(".tabGesAuthorize").css("display", "none");
        $(".tabAuthorize").css("display", "none");
        $(".tabCerrar").css("display", "block");
    });

    $("#addRole").click(function () {
        $(".tabAddRoles").css("display", "block");
        $(".tabCerrar").css("display", "none");
        $("#btnAddRole").show();
    });

}

function GestorOrganization() {
    //Activa ventanas para gestionar crud de organización
    $("#basics").click(function () {
        $(".tabBasics").css("display", "block");
        $(".tabAddCargos").css("display", "none");
        $(".tabAddZones").css("display", "none");
        $(".tabProcess").css("display", "none");
        $(".tabActivitys").css("display", "none");
        $(".tabTasks").css("display", "none");
        $(".tabGesCargos").css("display", "none");
        $(".tabGesZones").css("display", "none");
        $(".tabGesProcess").css("display", "none");
        $(".tabGesActivitys").css("display", "none");
        $(".tabGesTasks").css("display", "none");
        $(".tabCerrar").css("display", "none");
    });
    $("#basics").dblclick(function () {
        $(".tabBasics").css("display", "none");
        $(".tabAddCargos").css("display", "none");
        $(".tabAddZones").css("display", "none");
        $(".tabProcess").css("display", "none");
        $(".tabActivitys").css("display", "none");
        $(".tabTasks").css("display", "none");
        $(".tabGesCargos").css("display", "none");
        $(".tabGesZones").css("display", "none");
        $(".tabGesProcess").css("display", "none");
        $(".tabGesActivitys").css("display", "none");
        $(".tabGesTasks").css("display", "none");
        $(".tabCerrar").css("display", "block");
    });

    $("#cargos").click(function () {
        $(".tabGesCargos").css("display", "block");
        $(".tabCerrar").css("display", "none");
        ShowCargos();
    });
    $("#cargos").dblclick(function () {
        $(".tabAddCargos").css("display", "none");
        $(".tabGesCargos").css("display", "none");
        $(".tabCerrar").css("display", "block");
    });

    $("#zones").click(function () {
        $(".tabGeszones").css("display", "block");
        $(".tabCerrar").css("display", "none");
        ShowZones();
    });
    $("#zones").dblclick(function () {
        $(".tabGesZones").css("display", "none");
        $(".tabCerrar").css("display", "block");
    });

    $("#process").click(function () {
        $(".tabGesProcess").css("display", "none");
        $(".tabCerrar").css("display", "none");
        ShowProcess();
    });
    $("#process").dblclick(function () {
        $(".tabGesProcess").css("display", "none");
        $(".tabCerrar").css("display", "block");
    });

    $("#activitys").click(function () {
        $(".tabGesActivitys").css("display", "none");
        $(".tabCerrar").css("display", "none");
        ShowActivitys();
    });
    $("#activitys").dblclick(function () {
        $(".tabGesActivitys").css("display", "none");
        $(".tabCerrar").css("display", "block");
    });

    $("#tasks").click(function () {
        $(".tabGesTasks").css("display", "none");
        $(".tabCerrar").css("display", "none");
        ShowTasks();
    });
    $("#tasks").dblclick(function () {
        $(".tabGesTasks").css("display", "none");
        $(".tabTasks").css("display", "none");
        $(".tabCerrar").css("display", "block");
    });

    $("#addCargo").click(function () {
        //$(".tabGesPlanAcc").css("display", "none");
        $(".tabAddCargos").css("display", "block");
        $(".tabCerrar").css("display", "none");
        $("#btnAddCargo").show();
    });
    $("#addZone").click(function () {
        //$(".tabGesPlanAcc").css("display", "none");
        $(".tabAddZones").css("display", "block");
        $(".tabCerrar").css("display", "none");
        $("#btnAddZone").show();
    });
    $("#addProcess").click(function () {
        //$(".tabGesPlanAcc").css("display", "none");
        $(".tabAddProcess").css("display", "block");
        $(".tabCerrar").css("display", "none");
        $("#btnAddProcess").show();
    });
    $("#addActivity").click(function () {
        //$(".tabGesPlanAcc").css("display", "none");
        $(".tabAddActivitys").css("display", "block");
        $(".tabCerrar").css("display", "none");
        $("#btnAddActivity").show();
    });
    $("#addTask").click(function () {
        //$(".tabGesPlanAcc").css("display", "none");
        $(".tabAddTasks").css("display", "block");
        $(".tabCerrar").css("display", "none");
        $("#btnAddTask").show();
    });

}

function GestorIncidents() {

    $('#general').click(function () {
        $(".tabGeneral").css("display", "block");
    });
    $('#general').dblclick(function () {
        $(".tabGeneral").css("display", "none");
    });

    $('#detalles').click(function () {
        $(".detal").css("display", "block");
    });
    $('#detalles').dblclick(function () {
        $(".detal").css("display", "none");
    });

    $('#lesionados').click(function () {
        $(".tabLesionados").css("display", "block");
    });
    $('#lesionados').dblclick(function () {
        $(".tabLesionados").css("display", "none");
    });

    $('#descripcion').click(function () {
        $(".tabDescrip").css("display", "block");
    });
    $('#descripcion').dblclick(function () {
        $(".tabDescrip").css("display", "none");
    });

    $('#acciones').click(function () {
        $(".tabAcciones").css("display", "block");
    });
    $('#acciones').dblclick(function () {
        $(".tabAcciones").css("display", "none");
    });

    $('#matriz').click(function () {
        $(".tabMatriz").css("display", "block");
    });
    $('#matriz').dblclick(function () {
        $(".tabMatriz").css("display", "none");
    });

    $('#equipoInvest').click(function () {
        var equipo = $('#equipoInvest').val();
        var colorAttrib;
        switch (equipo) {
            case "1":
                colorAttrib = "red";
                break;

            case "2":
                colorAttrib = "yellow";
                break;

            default:
                colorAttrib = "green";
                break;
        }

        $("#equipoInvest").css("backgroundColor", colorAttrib, "color", "black");
    });

    $('#interpretaP').change(function () {
        var valLesion = $('#idLesion').val();
        var valDaño = $('#idDaño').val();
        var valMedio = $('#idMedio').val();
        var valImagen = $('#idImagen').val();
        var valInterpretaP = $('#interpretaP').val();

        if ((valLesion == "1" || valLesion == "2") && (valDaño == "1" || valDaño == "2") && (valMedio == "1" || valMedio == "2")
            && (valImagen == "1" || valImagen == "2")) {
            $('#interpretaP').css("backgroundColor", "green");
        };

        if (valLesion == "3" && valDaño == "3" && valMedio == "3" && valImagen == "3" && (valInterpretaP == "1" || valInterpretaP == "2" || valInterpretaP == "3")) {
            $('#interpretaP').css("backgroundColor", "green");
        };

        if (valLesion == "3" && valDaño == "3" && valMedio == "3" && valImagen == "3" && (valInterpretaP == "4" || valInterpretaP == "5")) {
            $('#interpretaP').css("backgroundColor", "yellow");
        };

        if (valLesion == "4" && valDaño == "4" && valMedio == "4" && valImagen == "4" && (valInterpretaP == "1" || valInterpretaP == "2")) {
            $('#interpretaP').css("backgroundColor", "green");
        };

        if (valLesion == "4" && valDaño == "4" && valMedio == "4" && valImagen == "4" && (valInterpretaP == "3" || valInterpretaP == "4" || valInterpretaP == "5")) {
            $('#interpretaP').css("backgroundColor", "yellow");
        };

        if (valLesion == "5" && valDaño == "5" && valMedio == "5" && valImagen == "5" && valInterpretaP == "1") {
            $('#interpretaP').css("backgroundColor", "green");
        };

        if (valLesion == "5" && valDaño == "5" && valMedio == "5" && valImagen == "5" && (valInterpretaP == "2" || valInterpretaP == "3")) {
            $('#interpretaP').css("backgroundColor", "yellow");
        };

        if (valLesion == "5" && valDaño == "5" && valMedio == "5" && valImagen == "5" && (valInterpretaP == "4" || valInterpretaP == "5")) {
            $('#interpretaP').css("backgroundColor", "red");
        };

        if (valLesion == "6" && valDaño == "6" && valMedio == "6" && valImagen == "6" && (valInterpretaP == "1" || valInterpretaP == "2")) {
            $('#interpretaP').css("backgroundColor", "yellow");
        };

        if (valLesion == "6" && valDaño == "6" && valMedio == "6" && valImagen == "6" && (valInterpretaP == "3" || valInterpretaP == "4" || valInterpretaP == "5")) {
            $('#interpretaP').css("backgroundColor", "red");
        };
    });

    $("#lesionados").click(function () {
        $(".tabLesionados").css("display", "block");
        $(".tabCerrar").css("display", "none");
        ConsultarLesionados();
    });

    $("#lesionados").dblclick(function () {
        $(".tabLesionados").css("display", "none");
        $(".tabCerrar").css("display", "block");
    });

    $("#addLesionado").click(function () {
        //$(".tabGesPlanAcc").css("display", "none");
        $(".tabAddLesionado").css("display", "block");
        $(".tabCerrar").css("display", "none");
        $("#btnAddLesion").show();
    });
}

function CancelPlan() {
    $(".tabAddPlanAcc").css("display", "none");
    $("#idFechaIni").val("");
    $("#idFechaFin").val("");
    $("#txtCausa").val("");
    $("#accion").val("");
    $("#idRespons").val("");
    $("#idPrioritaria").val("");
    $("#idCostos").val("");
    mostrarPlanAcc();
}

function CancelLesion() {
    $(".tabAddLesionado").css("display", "none");
    $("#idLesionado").val("");
    ConsultarLesionados();
}

function CancelSigue() {
    $(".tabAddSeguimAcc").css("display", "none");
    $("#txtFechaSeg").val("");
    $("#txtResultado").val("");
    $("#txtRespons").val("");
    mostrarSeguimAcc();
}

function CancelIncidente() {
    $(".tabMatriz").css("display", "none");
    $("#zona").val("");
    $("#proceso").val("");
    $("#activity").val("");
    $("#tarea").val("");
    $("#txtFechaReporte").val("");
    $("#txtFechaIncidente").val("");
    $("#txtCategoria").val("");
    $("#txtIncapacidad").val("");
    $("#txtDiasIncapacidad").val("");
    $("#txtNaturaleza").val("");
    $("#txtPartes").val("");
    $("#txtTipo").val("");
    $("#txtAgente").val("");
    $("#txtActos").val("");
    $("#txtCondicion").val("");
    $("#txtDaño").val("");
    $("#txtAfectacion").val("");
    $("#txtOcacionados").val("");
    $("#txtCostos").val("");
    $("#txtVehiculo").val("");
    $("#txtMarca").val("");
    $("#txtModelo").val("");
    $("#txtKilometros").val("");
    $("#txtDescripcion").val("");
    $("#txtEvitar").val("");
    $("#txtAcciones").val("");
    $("#txtComentarios").val("");
    $("#txtAtencion").val("");
    $("#idLesion").val("");
    $("#idDaño").val("");
    $("#idMedio").val("");
    $("#idImagen").val("");
    $("#interpretaP").val("");
    $("#equipoInvest").val("");
    $("#idLesion").val("");
    $("#idDaño").val("");
    $("#idMedio").val("");
    $("#idImagen").val("");
    $("#txtInvestiga").val("");
}

// las siguientes funciones crean un nuevo incidente
function AddIncidente() {
    // Crea un nuevo incidente
    $(".tabMatriz").css("display", "none");
    if ($("#txtIncapacidad").is(':checked')) {
        $("#txtIncapacidad").val(true)
    }
    else {
        $("#txtIncapacidad").val(false)
    }

    var incidenteVM = {
        ID: "0",
        ZonaID: $("#zona").val(),
        ProcesoID: $("#proceso").val(),
        ActividadID: $("#activity").val(),
        TareaID: $("#tarea").val(),
        FechaReporte: $("#txtFechaReporte").val(),
        FechaIncidente: $("#txtFechaIncidente").val(),
        CategoriasIncidente: $("#txtCategoria").val(),
        IncapacidadMedica: $("#txtIncapacidad").val(),
        DiasIncapacidad: $("#txtDiasIncapacidad").val(),
        NaturalezaLesion: $("#txtNaturaleza").val(),
        PartesAfectadas: $("#txtPartes").val(),
        TipoIncidente: $("#txtTipo").val(),
        AgenteLesion: $("#txtAgente").val(),
        ActosInseguros: $("#txtActos").val(),
        CondicionesInsegura: $("#txtCondicion").val(),
        TipoDaño: $("#txtDaño").val(),
        Afectacion: $("#txtAfectacion").val(),
        DañosOcasionados: $("#txtOcacionados").val(),
        CostosEstimados: $("#txtCostos").val(),
        TipoVehiculo: $("#txtVehiculo").val(),
        MarcaVehiculo: $("#txtMarca").val(),
        ModeloVehiculo: $("#txtModelo").val(),
        KilometrajeVehiculo: $("#txtKilometros").val(),
        DescripcionIncidente: $("#txtDescripcion").val(),
        EvitarIncidente: $("#txtEvitar").val(),
        AccionesInmediatas: $("#txtAcciones").val(),
        ComentariosAdicionales: $("#txtComentarios").val(),
        AtencionBrindada: $("#txtAtencion").val(),
        ConsecuenciasLesion: $("#idLesion").val(),
        ConsecuenciasDaño: $("#idDaño").val(),
        ConsecuenciasMedio: $("#idMedio").val(),
        ConsecuenciasImagen: $("#idImagen").val(),
        Probabilidad: $("#interpretaP").val(),
        EquiposInvestigador: $("#equipoInvest").val(),
        LesionPersonal: $("#idLesion").val(),
        DañoMaterial: $("#idDaño").val(),
        MedioAmbiente: $("#idMedio").val(),
        Imagen: $("#idImagen").val(),
        RequiereInvestigacion: $("#txtInvestiga").val(),
        TrabajadorID: $("#txtInformante").val()
    };

    $.ajax({
        type: "POST",
        url: "/Incidentes/CreateIncidente",
        data: { model: incidenteVM },
        dataType: "json",
        success: function (result) {
            if (result.data != false) {
                $("#txtIncidenteID").val(result.data);
            }

            $("#btnAddIncidente").hide();
            $("#btnCanIncidente").hide();
            alert(result.mensaj);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function UpdateIncidente() {
    // Crea un nuevo incidente
    $(".tabMatriz").css("display", "none");
    if ($("#txtIncapacidad").is(':checked')) {
        $("#txtIncapacidad").val(true)
    }
    else {
        $("#txtIncapacidad").val(false)
    }

    var incidenteID = $("#txtIncidenteID").val();

    var incidenteVM = {
        ID: incidenteID,
        ZonaID: $("#zona").val(),
        ProcesoID: $("#proceso").val(),
        ActividadID: $("#activity").val(),
        TareaID: $("#tarea").val(),
        FechaReporte: $("#txtFechaReporte").val(),
        FechaIncidente: $("#txtFechaIncidente").val(),
        CategoriasIncidente: $("#txtCategoria").val(),
        IncapacidadMedica: $("#txtIncapacidad").val(),
        DiasIncapacidad: $("#txtDiasIncapacidad").val(),
        NaturalezaLesion: $("#txtNaturaleza").val(),
        PartesAfectadas: $("#txtPartes").val(),
        TipoIncidente: $("#txtTipo").val(),
        AgenteLesion: $("#txtAgente").val(),
        ActosInseguros: $("#txtActos").val(),
        CondicionesInsegura: $("#txtCondicion").val(),
        TipoDaño: $("#txtDaño").val(),
        Afectacion: $("#txtAfectacion").val(),
        DañosOcasionados: $("#txtOcacionados").val(),
        CostosEstimados: $("#txtCostos").val(),
        TipoVehiculo: $("#txtVehiculo").val(),
        MarcaVehiculo: $("#txtMarca").val(),
        ModeloVehiculo: $("#txtModelo").val(),
        KilometrajeVehiculo: $("#txtKilometros").val(),
        DescripcionIncidente: $("#txtDescripcion").val(),
        EvitarIncidente: $("#txtEvitar").val(),
        AccionesInmediatas: $("#txtAcciones").val(),
        ComentariosAdicionales: $("#txtComentarios").val(),
        AtencionBrindada: $("#txtAtencion").val(),
        ConsecuenciasLesion: $("#idLesion").val(),
        ConsecuenciasDaño: $("#idDaño").val(),
        ConsecuenciasMedio: $("#idMedio").val(),
        ConsecuenciasImagen: $("#idImagen").val(),
        Probabilidad: $("#interpretaP").val(),
        EquiposInvestigador: $("#equipoInvest").val(),
        LesionPersonal: $("#idLesion").val(),
        DañoMaterial: $("#idDaño").val(),
        MedioAmbiente: $("#idMedio").val(),
        Imagen: $("#idImagen").val(),
        RequiereInvestigacion: $("#txtInvestiga").val(),
        TrabajadorID: $("#txtInformante").val()
    };

    $.ajax({
        type: "POST",
        url: "/Incidentes/UpdateIncidente",
        data: { model: incidenteVM },
        dataType: "json",
        success: function (idIncidente) {
            $("#btnAddIncidente").hide();
            $("#btnCanIncidente").hide();
            alert("El registro ha sido actualizado exitosamente : ");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function AddLesionados() {
    // Crea un nuevo lesionado
    $(".tabAddLesionado").css("display", "none");
    var incidenteID = $("#txtIncidenteID").val();
    var accidentadoVM = {
        ID: "0",
        IncidenteID: incidenteID,
        TrabajadorID: $("#idLesionado").val()
    };

    $.ajax({
        type: "POST",
        url: "/Incidentes/CreateLesionado",
        data: { model: accidentadoVM },
        dataType: "json",
        success: function (accidenID) {
            $("#btnAddLesion").hide();
            ConsultarLesionados();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function AddRole() {
    // Crea un nuevo role
    $(".tabAddRoles").css("display", "none");
    var roleVM = {
        ID: "0",
        Name: $("#txtName").val()
    };

    $.ajax({
        type: "POST",
        url: "/Accounts/CreateRole",
        data: { model: roleVM },
        dataType: "json",
        success: function (response) {
            alert(response.mensaj);
            $("#btnAddRole").hide();
            ShowRoles();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function AddAuthorization() {
    // Crea un nuevo role
    $(".tabAddAuthorize").css("display", "none");
    var roleVM = {
        ID: "0",
        RoleID: $("#txtRoleID").val(),
        Operation: $("#txtOperation").val(),
        Component: $("#txtComponent").val()
    };
    $.ajax({
        type: "POST",
        url: "/Accounts/CreateRoleOperation",
        data: { model: roleVM },
        dataType: "json",
        success: function (accidenID) {
            $("#btnAddAuthorize").hide();
            ShowAuthorizations();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function AddNewCargo() {
    // TODO
    // Crea un nuevo cargo
    $(".tabAddCargo").css("display", "none");
    var cargoVM = {
        ID: "0",
        Codigo: $("#txtCodigo"),
        descripcion: $("#txtCargo").val()
    };
    $.ajax({
        type: "POST",
        url: "/Organizations/CreateNewCargo",
        data: { model: cargoVM },
        dataType: "json",
        success: function (result) {
            $("#btnAddCargo").hide();
            $("#btnCanCargo").hide();
            $("#txtCodigo").val("");
            $("#txtDescrip").val("");
            $(".tabAddCargos").css("display", "none");
            alert(result.mensaj);
            ShowCargos();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function AddZone() {
    // Crea un nueva zona
    var zoneVM = {
        ID: "0",
        Descripcion: $("#txtZone").val()
    };

    $.ajax({
        type: "POST",
        url: "/Organizations/CreateZone",
        data: { model: zoneVM },
        dataType: "json",
        success: function (result) {
            $("#btnAddZone").hide();
            $("#btnCanZone").hide();
            $("#txtZone").val("");
            $(".tabAddZones").css("display", "none");
            ShowZones();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function AddProcess() {
    // Crea un nuevo process
    $(".tabAddProcess").css("display", "none");
    var processVM = {
        ID: "0",
        Descripcion: $("#txtProcess").val()
    };

    $.ajax({
        type: "POST",
        url: "/Organizations/CreateProcess",
        data: { model: processVM },
        dataType: "json",
        success: function (result) {
            $("#btnAddProcess").hide();
            $("#btnCanProcess").hide();
            $("#txtProcess").val("");
            $(".tabAddProcess").css("display", "none");
            ShowProcess();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function AddActivity() {
    // Crea una nueva activity
    $(".tabAddActivity").css("display", "none");
    var activityVM = {
        ID: "0",
        Descripcion: $("#txtActivity").val()
    };

    $.ajax({
        type: "POST",
        url: "/Organizations/CreateActivity",
        data: { model: activityVM },
        dataType: "json",
        success: function (result) {
            $("#btnAddActivity").hide();
            $("#btnCanActivity").hide();
            $("#txtActivity").val("");
            $(".tabAddActivitys").css("display", "none");
            ShowActivitys();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function AddTask() {
    // Crea una nueva tarea
    $(".tabAddTasks").css("display", "none");
    var taskVM = {
        ID: "0",
        Descripcion: $("#txtTask").val()
    };

    $.ajax({
        type: "POST",
        url: "/Organizations/CreateTask",
        data: { model: taskVM },
        dataType: "json",
        success: function (result) {
            $("#btnAddTask").hide();
            $("#btnCanTask").hide();
            $("#txtTask").val("");
            ShowTasks();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function AddUser() {
    // Crea un nuevo usuario
    var user = {
        ID: "0",
        Name: $("#txtName").val(),
        Email: $("#txtEmail").val(),
        Password: $("#txtPassword").val(),
        RoleID: "0"
    };
    $.ajax({
        type: "POST",
        url: "/Accounts/CreateUser",
        data: { model: user },
        dataType: "json",
        success: function (result) {
            message = result.mensaj;
            alert(result.mensaj);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function LoginUser() {
    // Crea un nuevo usuario
    var loginVM = {
        ID: "0",
        Name: $("#txtName").val(),
        Email: $("#txtEmail").val(),
        Password: $("#txtPassword").val(),
        RoleID: "0"
    };
    $.ajax({
        type: "POST",
        url: "/Accounts/LoginUser",
        data: { model: loginVM },
        dataType: "json",
        success: function (response) {
            if (response.result == 'Redirect') {

                window.location = response.url;
            }
            else {
                alert(response.mensaj);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function SelectComponent() {
    var x = document.getElementById("component").value;
}

