// Agregar funcionlidad principal del lado del cliente...

const { each } = require("jquery");

function viewHistory() {
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
            var fecha = Date.now();
            var html = '', interpretaP = '', interpretaNR = '', aceptabilidad = '';
            var year = 0, month = 0, day = 0, np = 0, nr = 0;
            $.each(result, function (key, item) {
                np = item.NivelDeficiencia * item.NivelExposicion;
                nr = np * item.NivelConsecuencia;

                switch (true) {
                    case (np >= 24):
                        interpretaP = "Muy alto (MA)";
                        break;

                    case (np >= 10 && np < 24):
                        interpretaP = "Alto (A)";
                        break;

                    case (np >= 8 && np < 10):
                        interpretaP = "Mdio (M)";
                        break;

                    case (np < 8):
                        interpretaP = "Bajo (B)";
                        break;
                }

                switch (true) {
                    case (nr >= 600):
                        interpretaNR = "I";

                    case (nr >= 150 && nr < 600):
                        interpretaNR = "II";
                        break;

                    case (nr >= 40 && nr < 150):
                        interpretaNR = "III";
                        break;

                    case (nr < 40):
                        interpretaNR = "IV";
                        break;
                }

                switch (item.Aceptabilidad) {
                    case 1:
                        aceptabilidad = "No Aceptable";
                        break;

                    case 2:
                        aceptabilidad = "No Aceptable o Aceptable con control Específico";
                        break;

                    case 3:
                        aceptabilidad = "Mejorable";
                        break;

                    case 4:
                        aceptabilidad = "Aceptable";
                        break;
                }

                html += '<tr>';
                html += '<td>' + item.NivelDeficiencia + '</td>';
                html += '<td>' + item.NivelExposicion + '</td>';
                html += '<td>' + np + '</td>';
                html += '<td>' + interpretaP + '</td>';
                html += '<td>' + item.NivelConsecuencia + '</td>';
                html += '<td>' + nr + '</td>';
                html += '<td>' + interpretaNR + '</td>';
                html += '<td>' + aceptabilidad + '</td>';
                html += '<td>' + item.Nombre + '</td>';
                html += '<td>' + item.TextCategoria + '</td>';
                html += '<td>' + item.TextIntervencion + '</td>';
                html += '<td>' + item.Responsable + '</td>';
                html += '<td>' + item.TextFechaInicial + '</td>';
                html += '<td>' + item.TextFechaFinal + '</td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
            $('.tabIncidents').css("display", "none");
            $('.tabHistory').css("display", "block");
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) { // función que va a ejecutar si hubo algún tipo de error en el pedido
            var error = eval("(" + XMLHttpRequest.responseText + ")");
            alert(error.Message);
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
            var fecha = Date.now();
            var html = '';
            var year = 0, month = 0, day = 0;
            $.each(result, function (key, item) {

                html += '<tr>';
                html += '<td>' + item.Nombre + '</td>';
                html += '<td>' + item.TextCategoria + '</td>';
                html += '<td>' + item.TextIntervencion + '</td>';
                html += '<td>' + item.Responsable + '</td>';
                html += '<td>' + item.TextFechaInicial + '</td>';

                year = item.TextFechaFinal.substring(0, 4);
                month = item.TextFechaFinal.substring(5, 7);
                day = item.TextFechaFinal.substring(8, 10);
                date = new Date(year, month -1, day);
                date = Date.parse(date);
                if (fecha > date) {
                    html += '<td style="background-color:red">' + item.TextFechaFinal + '</td>';
                } else {
                    html += '<td style="background-color:green">' + item.TextFechaFinal + '</td>';
                }

                html +=
                    '<td><a href="#" onclick="return getIntervenByID(' + item.ID + ')">Editar</a> | <a href = "#" onclick = "DeleteInterven(' + item.ID + ')"> Borrar</a></td>';
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

function showIncidents() {
    //$(".tabGesSeguiPlanAcc").css("display", "none");
    var zona = $("#zona").val();
    var proceso = $("#proceso").val();
    var activity = $("#activity").val();
    $.ajax({
        url: "/Riesgos/GetAllIncidents/",
        data: {
            idZona: zona,
            idProceso: proceso,
            idActivity: activity
        },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,                                               // si es asincrónico o no
        success: function (result) {
            var html = '';
            var categoria = '';
            var incapacidad = '';
            $.each(result, function (key, item) {
                if (item.CategoriasIncidente == 1) {
                    categoria = 'Incidente';
                } else {
                    categoria = 'Accidente';
                }

                if (item.IncapacidadMedica == true) {
                    incapacidad = 'SI';
                } else {
                    incapacidad = 'Accidente';
                }

                html += '<tr>';
                html += '<td>' + item.TxtFechaIncident + '</td>';
                html += '<td>' + categoria + '</td>';
                html += '<td>' + incapacidad + '</td>';
                html += '<td>' + item.DiasIncapacidad + '</td>';
                html += '<td>' + item.NaturalezaLesion + '</td>';
                html += '<td>' + item.PartesAfectadas + '</td>';
                html += '<td>' + item.TipoIncidente + '</td>';
                html += '<td>' + item.AgenteLesion + '</td>';
                html += '<td>' + item.ActosInseguros + '</td>';
                html += '<td>' + item.CondicionesInsegura + '</td>';
            });
            $('.tbody').html(html);
            $(".tabHistory").css("display", "none");
            $('.tabIncidents').css("display", "block");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function mostrarPlanAcc() {
    $(".tabGesSeguiPlanAcc").css("display", "none");
    var accionID = $("#txtAccionID").val();
    if (accionID == "") {
        return;
    }

    $.ajax({
        url: "/Acciones/ListarPlanAccion/",
        data: { idAccion: accionID },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,                                               // si es asincrónico o no
        success: function (result) {
            var fecha = Date.now();
            var html = '';
            var year = 0, month = 0, day = 0;
            $.each(result, function (key, item) {
                var estado = "";
                var txtColor = "";
                switch (item.ActionCategory)
                {
                    case 1:
                        estado = "Sín iniciar";
                        txtColor = "background-color:red";
                        break;

                    case 2:
                        estado = "En proceso";
                        txtColor = "background-color:yellow";
                        break;

                    case 3:
                        estado = "Finalizada";
                        txtColor = "background-color:green";
                        break;
                }

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

                year = item.FechaFinal.substring(0, 4);
                month = item.FechaFinal.substring(5, 7);
                day = item.FechaFinal.substring(8, 10);
                date = new Date(year, month - 1, day);
                date = Date.parse(item.FechaFinal);
                if (fecha > date) {
                    html += '<td style="background-color:red">' + estado + '</td>';
                } else {
                    html += '<td style='+ txtColor + '>' + estado + '</td>';
                }
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

function ShowMovimientos(phva) {
    // Mostrar todos los movimientos
    var Item = $("#txtStandardID").val();
    $.ajax({
        url: "/Movimientos/GetMovimientos",
        data: {
            ciclo: phva,
            item: Item
        },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.Ciclo + '</td>';
                html += '<td>' + item.Item + '</td>';
                html += '<td>' + item.Name + '</td>';
                html += '<td>' + item.Descripcion + '</td>';
                html += '<td>' + item.Type + '</td>';
                html += '<td><a href = "#" onclick = "EditMovimient(' + item.ID + ')" class="btn btn-primary">Abrir</a></td>';

                if (item.Type == ".PDF") {
                    html += '<td><a href = "#" onclick = "GeneratePDF(' + item.ID + ')" class="btn btn-warning" style ="pointer-events: none;">Generar PDF</a></td>';
                } else {

                    html += '<td><a href = "#" onclick = "GeneratePDF(' + item.ID + ')" class="btn btn-warning">Generar PDF</a></td>';
                }

                html += '<td><a href = "#" onclick = "DownLoadPDF(' + item.ID + ')" class="btn btn-success">Descargar</a></td>';
                html += '<td><a href = "#" onclick = "DeleteMovimient(' + item.ID + ')" class="btn btn-danger">Eliminar</a></td>';
                html += '<td><a href = "#" onclick = "SendEmail(' + item.ID + ')" class="btn btn-info">Enviar correo</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
            $('.tabGesMovimientos').css("display", "block");
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
                html += '<td>' + item.Codigo + '</td>';
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
    $("#FechaInicial").val("");
    $("#FechaFinal").val("");
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
    $("#txtNombre").val("");
}

validarFechaIni = function (item) {
    var fecha = Date.now();
    var date = Date.parse(item);
    if (date > fecha) {
        alert("Fecha inicial incorrecta !!");
        return false;
    };
}

validarFechaFin = function (ini, fin) {
    var fechaIni = Date.parse(ini);
    var fechaFin = Date.parse(fin);
    if (fechaIni > fechaFin) {
        alert("Fecha final incorrecta !!");
        return false;
    };
}

validarFechaIncidente = function (ini, fin) {
    var fechaIni = Date.parse(ini);
    var fechaFin = Date.parse(fin);
    if (fechaIni < fechaFin) {
        alert("Fecha incidente incorrecta !!");
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

function DownLoadPDF(movimientID) {
    $(".tabGesMovimientos").css("display", "none");
    $.ajax({
        async: true,
        type: 'POST',
        url: "/Movimientos/Download",
        data: { id: movimientID },
        dataType: "json",
        success: function (result) {
            alert(result.mensaj);
            ShowMovimientos(ciclo);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    $(".tabAddMovimientos").css("display", "none");
    $("#btnAddMovimient").hide();
    $("#btnCanMovimient").show();
}

function GeneratePDF(movimientID) {
    $(".tabGesMovimientos").css("display", "none");
    $.ajax({
        async: true,
        type: 'POST',
        url: "/Movimientos/CreatePDF",
        data: { id: movimientID },
        dataType: "json",
        success: function (result) {
            alert(result.mensaj);
            ShowMovimientos(ciclo);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    $(".tabAddMovimientos").css("display", "none");
    $("#btnAddMovimient").hide();
    $("#btnCanMovimient").show();
}

function EditMovimient(movimientID) {
    $(".tabGesMovimientos").css("display", "none");
    $.ajax({
        async: true,
        type: 'GET',
        url: "/Movimientos/OpenFile",
        data: { id: movimientID },
        dataType: "json",
        contentType: "application/json;charset=UTF-8",
        success: function (result) {
            alert(result.mensaj);
            ShowMovimientos(ciclo);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    $(".tabGesMovimientos").css("display", "block");
    $(".tabAddMovimientos").css("display", "none");
    $("#btnAddMovimient").hide();
    $("#btnCanMovimient").show();
}

function DeleteMovimient(movimientID) {
    $(".tabGesMovimientos").css("display", "none");
    $.ajax({
        async: true,
        type: 'POST',
        url: "/Movimientos/DeleteFile",
        data: { id: movimientID },
        dataType: "json",
        success: function (result) {
            alert(result.mensaj);
            ShowMovimientos(ciclo);
        },
         error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    $(".tabGesMovimientos").css("display", "block");
    $(".tabAddMovimientos").css("display", "none");
    $("#btnAddMovimient").hide();
    $("#btnCanMovimient").show();
}

function getIntervenByID(intervenID) {
    $(".tabMediAplica").css("display", "none");
    //aplicaID = intervenID;
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
            $("#FechaInicial").val(result.FechaInicial);
            $("#FechaFinal").val(result.FechaFinal);
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
            $("#txtActionCategory").val(result.ActionCategory);
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
            if (result.data != false) {
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
                            alert(result.mensaj);
                        },
                        error: function (xhr, ajaxOptions, thrownError) {
                            alert(xhr.status);
                            alert(thrownError);
                        }
                    });
                }
            }
            else {

                alert(result.mensaj);
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
                text += "Estado : " + result.data.ActionState + "\n";
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
    text += "Esta seguro de querer borrar esta zona ? :" + "\n\n";
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
    text += "Esta seguro de querer borrar este cargo ? :" + "\n\n";
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
    // Eliminar una autorización
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
            success: function (response) {
                alert(response.mensaj);
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
    $(".tabIdCausas").css("display", "none");
    if ($("#idEfectiva").is(':checked')) {
        $("#idEfectiva").val(true)
    }
    else {
        $("#idEfectiva").val(false)
    }

    var accionVM = {
        ID: "0",
        ZonaID: $("#zona").val(),
        ProcesoID: $("#proceso").val(),
        ActividadID: $("#activity").val(),
        TareaID: $("#tarea").val(),
        FechaSolicitud: $("#FechaSolicitud").val(),
        Categoria: $("#categoriaAcc").val(),
        TrabajadorID: $("#idTrabajador").val(),
        FuenteAccion: $("#idFuente").val(),
        Descripcion: $("#idDescrip").val(),
        EficaciaAntes: $("#idEficaciaAnt").val(),
        EficaciaDespues: $("#idEficaciaDesp").val(),
        FechaCierre: $("#FechaCierre").val(),
        Efectiva: $("#idEfectiva").val(),
        ActionCategory: $("#txtActionCategory").val()
    };

    $.ajax({
        type: "POST",
        url: "/Acciones/CreateAccion",
        data: { model: accionVM },
        dataType: "json",
        success: function (response) {
            if (response.data != false) {
                $("#txtAccionID").val(response.data);
                $("#btnAddAction").hide();
                $("#btnCanAccc").hide();
            }
            alert(response.mensaj);
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
    if ($("#idEfectiva").is(':checked')) {
        $("#idEfectiva").val(true)
    }
    else {
        $("#idEfectiva").val(false)
    }

    var accionVM = {
        ID: $("#txtAccionID").val(),
        ZonaID: $("#zona").val(),
        ProcesoID: $("#proceso").val(),
        ActividadID: $("#activity").val(),
        TareaID: $("#tarea").val(),
        FechaSolicitud: $("#FechaSolicitud").val(),
        Categoria: $("#categoriaAcc").val(),
        TrabajadorID: $("#idTrabajador").val(),
        FuenteAccion: $("#idFuente").val(),
        Descripcion: $("#idDescrip").val(),
        EficaciaAntes: $("#idEficaciaAnt").val(),
        EficaciaDespues: $("#idEficaciaDesp").val(),
        FechaCierre: $("#FechaCierre").val(),
        Efectiva: $("#idEfectiva").val(),
        ActionCategory: $("#txtActionCategory").val()
    };

    $.ajax({
        type: "POST",
        url: "/Acciones/UpdateAccion",
        data: { model: accionVM },
        dataType: "json",
        success: function (response) {
            if (response.data != false) {
                $("#txtAccionID").val(response.data);
            }
            alert(response.mensaj);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function UpdateOrganization(id) {
    // Actualiza datos básicos de la empresa
    
    if (!validateOrganization()) {
        return false;
    }

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
        Politica: $("#txtPolitica").val(),
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

function validateOrganization() {
    if ($('#txtNit').val() == "") {
        document.getElementById("txtInfoBasic").innerHTML = "Falta NIT de la organización";
        $("#txtNit").focus();
        return false;
    }
    else {
        if ($('#txtRazon').val() == "") {
            document.getElementById("txtInfoBasic").innerHTML = "Falta Razón social de la organización";
            $("#txtRazon").focus();
            return false;
        }
        else {
            if ($('#txtDireccion').val() == "") {
                document.getElementById("txtInfoBasic").innerHTML = "Falta Dirección de la organización";
                $("#txtDireccion").focus();
                return false;
            }
            else {
                if ($('#txtMunicip').val() == "") {
                    document.getElementById("txtInfoBasic").innerHTML = "Falta Municipio de la organización";
                    $("#txtMunicip").focus();
                    return false;
                }
                else {
                    if ($('#txtDepartment').val() == "") {
                        document.getElementById("txtInfoBasic").innerHTML = "Falta Departamento de la organización";
                        $("#txtDepartment").focus();
                        return false;
                    }
                    else {
                        if ($('#txtTelefono').val() == "") {
                            document.getElementById("txtInfoBasic").innerHTML = "Falta teléfonos de la organización";
                            $("#txtTelefono").focus();
                            return false;
                        }
                        else {
                            if ($('#txtArl').val() == "") {
                                document.getElementById("txtInfoBasic").innerHTML = "Falta ARL de la organización";
                                $("#txtArl").focus();
                                return false;
                            }
                            else {
                                if ($('#txtClaseRiesgo').val() == "") {
                                    document.getElementById("txtInfoBasic").innerHTML = "Falta Clase riesgo de la organización";
                                    $("#txtClaseRiesgo").focus();
                                    return false;
                                }
                                else {
                                    if ($('#txtDocument').val() == "") {
                                        document.getElementById("txtInfoBasic").innerHTML = "Falta documento del representante legal de la organización";
                                        $("#txtDocument").focus();
                                        return false;
                                    }
                                    else {
                                        if ($('#txtName').val() == "") {
                                            document.getElementById("txtInfoBasic").innerHTML = "Falta nombre del representante legal de la organización";
                                            $("#txtName").focus();
                                            return false;
                                        }
                                        else {
                                            if ($('#txtEconomic').val() == "") {
                                                document.getElementById("txtInfoBasic").innerHTML = "Falta actvidad económica de la organización";
                                                $("#txtEconomic").focus();
                                                return false;
                                            }
                                            else {
                                                if ($('#txtNumero').val() == "") {
                                                    document.getElementById("txtInfoBasic").innerHTML = "Falta número de trabajadores de la organización";
                                                    $("#txtNumero").focus();
                                                    return false;
                                                }
                                                else {
                                                    if ($('#txtPolitica').val() == "") {
                                                        document.getElementById("txtInfoBasic").innerHTML = "Falta política de la organización";
                                                        $("#txtPolitica").focus();
                                                        return false;
                                                    }
                                                    else {
                                                        if ($('#txtProducts').val() == "") {
                                                            document.getElementById("txtInfoBasic").innerHTML = "Falta clase productos de la organización";
                                                            $("#txtProducts").focus();
                                                            return false;
                                                        }
                                                        else {
                                                            if ($('#txtMision').val() == "") {
                                                                document.getElementById("txtInfoBasic").innerHTML = "Falta Misión de la organización";
                                                                $("#txtMision").focus();
                                                                return false;
                                                            }
                                                            else {
                                                                if ($('#txtVision').val() == "") {
                                                                    document.getElementById("txtInfoBasic").innerHTML = "Falta Visión de la organización";
                                                                    $("#txtVision").focus();
                                                                    return false;
                                                                }
                                                                else {
                                                                    if ($('#txtObjetivos').val() == "") {
                                                                        document.getElementById("txtInfoBasic").innerHTML = "Falta Objetivos de la organización";
                                                                        $("#txtObjetivos").focus();
                                                                        return false;
                                                                    }
                                                                    else {
                                                                        if ($('#txtAdministrativo').val() == "") {
                                                                            document.getElementById("txtInfoBasic").innerHTML = "Falta definir turnos administrativos de la organización";
                                                                            $("#txtAdministrativo").focus();
                                                                            return false;
                                                                        }
                                                                        else {
                                                                            if ($('#txtOperativo').val() == "") {
                                                                                document.getElementById("txtInfoBasic").innerHTML = "Falta definir turnos operativos de la organización";
                                                                                $("#txtOperativo").focus();
                                                                                return false;
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    document.getElementById("txtInfoBasic").innerHTML = "";
    return true;
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
        Responsable: " ",
        ActionCategory: $("#txtPlanCategory").val()
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
        Costos: $("#idCostos").val(),
        ActionCategory: $("#txtPlanCategory").val()
    };
    $.ajax({
        type: "POST",
        url: "/Acciones/UpdatePlanAccion",
        data: { planAccion: planAccionVM },
        dataType: "json",
        success: function (response) {
            alert(response.mensaj);
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
        success: function (response) {
            alert(response.mensaj);
            mostrarSeguimAcc();
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
        NroExpuestos: $("#txtExpuestos").val(),
        RequisitoLegal: $("#txtRequisito").val(),
        DangerCategory: $("#txtDanger").val()
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
                $(".tabIncidents").css("display", "none");
                $(".tabHistory").css("display", "none");
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
        RequisitoLegal: $("#txtRequisito").val(),
        DangerCategory: $("#txtDanger").val()
    };

    $.ajax({
        type: "POST",
        url: "/Riesgos/UpdateRiesgo",
        data: { model: riesgoVM },
        dataType: "json",
        success: function (response) {
            if (response.data != false) {
                $("#txtRiesgoID").val(response.data);
                $("#btnUpdRiesgo").hide();
                $(".tabEvalRiesgos").css("display", "none");
                $(".tabIncidents").css("display", "none");
                $(".tabHistory").css("display", "none");
            }
            alert(response.mensaj);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

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
        FechaInicial: $("#FechaInicial").val(),
        Fechafinal: $("#FechaFinal").val(),
        Observaciones: $("#idObserv").val(),
        NivelDeficiencia: $("#deficiencia").val(),
        NivelExposicion: $("#exposicion").val(),
        NivelConsecuencia: $("#consecuencia").val(),
        AceptabilidadNR: $("#txtAceptabilidad").val()
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
    AddPlan(1); // adicionar plan acción
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
        FechaInicial: $("#FechaInicial").val(),
        FechaFinal: $("#FechaFinal").val(),
        Observaciones: $("#idObserv").val(),
        NivelDeficiencia: $("#deficiencia").val(),
        NivelExposicion: $("#exposicion").val(),
        NivelConsecuencia: $("#consecuencia").val(),
        AceptabilidadNR: $("#txtAceptabilidad").val()
    };

    $.ajax({
        type: "POST",
        url: "/Riesgos/UpdateIntervencion",
        data: { model: aplicaVM },
        dataType: "json",
        success: function (result) {
            alert(result.mensaj);
            $(".tabMediAplica").css("display", "block");
            $(".tabAddInterven").css("display", "none");
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
        ShowAuthorizations();
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
    $("#addAuthorize").click(function () {
        $(".tabAddAuthorize").css("display", "block");
        $(".tabCerrar").css("display", "none");
        $("#btnAddAthorize").show();
    });

}

function GestorMovimient() {
    //Activa ventanas para gestionar crud de movimientos

    $("#planear").click(function () {
        ResetTab();
        $(".tabGesMovimientos").css("display", "block");
        $("#addMovimient").show();
        $("#planear").focus();
        $(".tabCerrar").css("display", "none");
        ciclo = "P";
        loadNormas(ciclo);
    });
    $("#planear").dblclick(function () {
        ResetTab();
    });

    $("#hacer").click(function () {
        ResetTab();
        $(".tabGesMovimientos").css("display", "block");
        $(".tabCerrar").css("display", "none");
        $("#addMovimient").show();
        $("#hacer").show();
        ciclo = "H";
        loadNormas(ciclo);
    });
    $("#hacer").dblclick(function () {
        ResetTab();
    });

    $("#verificar").click(function () {
        ResetTab();
        $(".tabGesMovimientos").css("display", "block");
        $(".tabCerrar").css("display", "none");
        $("#addMovimient").show();
        $("#verificar").focus();
        ciclo = "V";
        loadNormas(ciclo);
    });
    $("#verificar").dblclick(function () {
        ResetTab();
    });

    $("#actuar").click(function () {
        ResetTab();
        $(".tabGesMovimientos").css("display", "block");
        $(".tabCerrar").css("display", "none");
        $("#addMovimient").show();
        $("#actuar").focus();
        ciclo = "A";
        loadNormas(ciclo);
    });
    $("#actuar").dblclick(function () {
        ResetTab();
    });

    $("#addMovimient").click(function () {
        $(".tabAddMovimientos").css("display", "block");
        $(".tabCerrar").css("display", "none");
        $("#txtStandardID").val("");
        $("#txtDescripcion").val("");
        $("#fileLoad").val("");
        $("#txtStandardID").focus();
        $("#btnAddMovimient").show();
        $("#addMovimient").hide();
        $("#btnCanMovimient").show();
    });

    $("#selectMovimient").click(function () {
        $(".tabCerrar").css("display", "none");
        ShowMovimientos(ciclo);
    });
}

function GestorOrganization() {
    //Activa ventanas para gestionar crud de organización

    $("#basics").click(function () {
        ResetTab();
        $(".tabBasics").css("display", "block");
        $("#txtNit").focus();
    });
    $("#basics").dblclick(function () {
        ResetTab();
    });

    $("#cargos").click(function () {
        ResetTab();
        $(".tabGesCargos").css("display", "block");
        $(".tabCerrar").css("display", "none");
        ShowCargos();
    });
    $("#cargos").dblclick(function () {
        ResetTab();
    });

    $("#zones").click(function () {
        ResetTab();
        $(".tabGeszones").css("display", "block");
        $(".tabCerrar").css("display", "none");
        ShowZones();
    });
    $("#zones").dblclick(function () {
        ResetTab();
    });

    $("#process").click(function () {
        ResetTab();
        $(".tabGesProcess").css("display", "block");
        $(".tabCerrar").css("display", "none");
        ShowProcess();
    });
    $("#process").dblclick(function () {
        ResetTab();
    });

    $("#activitys").click(function () {
        ResetTab();
        $(".tabGesActivitys").css("display", "block");
        $(".tabCerrar").css("display", "none");
        ShowActivitys();
    });
    $("#activitys").dblclick(function () {
        ResetTab();
    });

    $("#tasks").click(function () {
        ResetTab();
        $(".tabGesTasks").css("display", "block");
        $(".tabCerrar").css("display", "none");
        ShowTasks();
    });
    $("#tasks").dblclick(function () {
        ResetTab();
    });

    $("#addCargo").click(function () {
        $(".tabAddCargos").css("display", "block");
        $(".tabCerrar").css("display", "none");
        $("#txtCodigo").focus();
        $("#btnAddCargo").show();
        $("#btnCanCargo").show();
    });
    $("#addZone").click(function () {
        $(".tabAddZones").css("display", "block");
        $(".tabCerrar").css("display", "none");
        $("#btnAddZone").show();
        $("#btnCanZone").show();
    });
    $("#addProcess").click(function () {
        $(".tabAddProcess").css("display", "block");
        $(".tabCerrar").css("display", "none");
        $("#btnAddProcess").show();
        $("#btnCanProcess").show();
    });
    $("#addActivity").click(function () {
        $(".tabAddActivitys").css("display", "block");
        $(".tabCerrar").css("display", "none");
        $("#btnAddActivity").show();
        $("#btnCanActivity").show();
    });
    $("#addTask").click(function () {
        $(".tabAddTasks").css("display", "block");
        $(".tabCerrar").css("display", "none");
        $("#btnAddTask").show();
        $("#btnCanTask").show();
    });
}

function ResetTab() {
    $(".tabBasics").css("display", "none");
    $(".tabAddCargos").css("display", "none");
    $(".tabAddZones").css("display", "none");
    $(".tabAddProcess").css("display", "none");
    $(".tabAddActivitys").css("display", "none");
    $(".tabAddTasks").css("display", "none");
    $(".tabAddEvents").css("display", "none");
    $(".tabAddCauses").css("display", "none");
    $(".tabAddRootCauses").css("display", "none");
    $(".tabAddRecomendations").css("display", "none");
    $(".tabAddPlanAcc").css("display", "none");
    $(".tabAddInterven").css("display", "none");
    $(".tabAddBarriers").css("display", "none");
    $(".tabAddMovimientos").css("display", "none");
    $(".tabGesCargos").css("display", "none");
    $(".tabGesZones").css("display", "none");
    $(".tabGesProcess").css("display", "none");
    $(".tabGesActivitys").css("display", "none");
    $(".tabGesTasks").css("display", "none");
    $(".tabGesEvents").css("display", "none");
    $(".tabGesCauses").css("display", "none");
    $(".tabGesRootCauses").css("display", "none");
    $(".tabGesRecomendations").css("display", "none");
    $(".tabGesMovimientos").css("display", "none");
    $(".tabPeligros").css("display", "none");
    $(".tabEvalRiesgos").css("display", "none");
    $(".tabGesPlanAcc").css("display", "none");
    $(".tabMediAplica").css("display", "none");
    $(".tabGesBarriers").css("display", "none");
    $(".tabCerrar").css("display", "block");
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

    $('#investigate').click(function () {
        $(".tabInvestigation").css("display", "block");
    });

    $('#investigate').dblclick(function () {
        $(".tabInvestigation").css("display", "none");
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

    $("#events").click(function () {
        ResetTab();
        $(".tabGesEvents").css("display", "block");
        $(".tabCerrar").css("display", "none");
        ShowEvents();
    });
    $("#events").dblclick(function () {
        ResetTab();
    });

    $("#causes").click(function () {
        ResetTab();
        $(".tabGesCauses").css("display", "block");
        $(".tabCerrar").css("display", "none");
        ShowCauses();
    });
    $("#causes").dblclick(function () {
        ResetTab();
    });

    $("#barriers").click(function () {
        ResetTab();
        $(".tabGesBarriers").css("display", "block");
        $(".tabCerrar").css("display", "none");
        ShowBarriers();
    });
    $("#rootCauses").dblclick(function () {
        ResetTab();
    });

    $("#rootCauses").click(function () {
        ResetTab();
        $(".tabGesRootCauses").css("display", "block");
        $(".tabCerrar").css("display", "none");
        ShowRootCauses();
    });
    $("#rootCauses").dblclick(function () {
        ResetTab();
    });

    $("#recomendations").click(function () {
        ResetTab();
        $(".tabGesRecomendations").css("display", "block");
        $(".tabCerrar").css("display", "none");
        ShowRecomendations();
    });
    $("#recomendations").dblclick(function () {
        ResetTab();
    });

    $("#plans").click(function () {
        ResetTab();
        $(".tabGesPlanAcc").css("display", "block");
        $(".tabCerrar").css("display", "none");
        mostrarPlanAcc();
    });
    $("#plans").dblclick(function () {
        ResetTab();
    });

    $("#addEvent").click(function () {
        $(".tabAddEvents").css("display", "block");
        $(".tabCerrar").css("display", "none");
        $("#txtEvent").focus();
        $("#btnAddEvent").show();
        $("#btnCanEvent").show();
        $("#btnUpdEvent").hide();
    });
    $("#addCauses").click(function () {
        $(".tabAddCauses").css("display", "block");
        $(".tabCerrar").css("display", "none");
        $("#btnAddCause").show();
        $("#btnCanCause").show();
        $("#btnUpdCause").hide();
        loadCauses("txtCauseAnalice");
    });
    $("#addBarriers").click(function () {
        $(".tabAddBarriers").css("display", "block");
        $(".tabCerrar").css("display", "none");
        $("#btnAddBarrier").show();
        $("#btnCanBarrier").show();
        $("#btnUpdBarrier").hide();
        loadCauses("txtBarrier");
    });
    $("#addRootCauses").click(function () {
        $(".tabAddRootCauses").css("display", "block");
        $(".tabCerrar").css("display", "none");
        $("#btnAddRootCause").show();
        $("#btnCanRootCause").show();
        $("#btnUpdRootCause").hide();
    });
    $("#addRecomendations").click(function () {
        $(".tabAddRecomendations").css("display", "block");
        $(".tabCerrar").css("display", "none");
        $("#btnAddRecomendation").show();
        $("#btnCanRecomendation").show();
        $("#btnUpdRecomendation").hide();
        loadData();
    });
    $("#addPlans").click(function () {
        $(".tabAddPlanAcc").css("display", "block");
        $(".tabCerrar").css("display", "none");
        $("#btnAddPlan").show();
        $("#btnCanPlan").show();
        $("#btnUpdPlan").hide();
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

function CancelEvent() {
    $(".tabAddEvent").css("display", "none");
    $("#txtEvent").val("");
    $("#txtOrder").val("");
    ShowEvents();
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

function validateIncidents() {
    if ($('#zona').val() == "0") {
        document.getElementById("txtMessage").innerHTML = "Falta zona de ocurrencia del incidente";
        $("#zona").focus();
        return false;
    }
    else {
        if ($('#proceso').val() == "0") {
            document.getElementById("txtMessage").innerHTML = "Falta proceso de ocurrencia del incidente";
            $("#proceso").focus();
            return false;
        }
        else {
            if ($('#activity').val() == "0") {
                document.getElementById("txtMessage").innerHTML = "Falta actividad de ocurrencia del incidente";
                $("#activity").focus();
                return false;
            }
            else {
                if ($('#tarea').val() == "0") {
                    document.getElementById("txtMessage").innerHTML = "Falta tarea de ocurrencia del incidente";
                    $("#tarea").focus();
                    return false;
                }
                else {
                    if ($('#txtFechaReporte').val() == "") {
                        document.getElementById("txtMessage").innerHTML = "Falta fecha reprte del incidente";
                        $("#txtFechaReporte").focus();
                        return false;
                    }
                    else {
                        if ($('#txtFechaIncidente').val() == "") {
                            document.getElementById("txtMessage").innerHTML = "Falta fecha del incidente";
                            $("#txtFechaIncidente").focus();
                            return false;
                        }
                        else {
                            if ($('#txtCategoria').val() == "") {
                                document.getElementById("txtMessage").innerHTML = "Falta categoría del incidente";
                                $("#txtCategoria").focus();
                                return false;
                            }
                            else {
                                if ($('#txtIncapacidad').val() == "") {
                                    document.getElementById("txtMessage").innerHTML = "Falta incapacidad del incidente";
                                    $("#txtIncapacidad").focus();
                                    return false;
                                }
                                else {
                                    if ($('#txtDiasIncapacidad').val() == "") {
                                        document.getElementById("txtMessage").innerHTML = "Falta días incapacidad del incidente";
                                        $("#txtDiasIncapacidad").focus();
                                        return false;
                                    }
                                    else {
                                        if ($('#txtNaturaleza').val() == "") {
                                            document.getElementById("txtMessage").innerHTML = "Falta naturaleza del incidente";
                                            $("#txtNaturaleza").focus();
                                            return false;
                                        }
                                        else {
                                            if ($('#txtPartes').val() == "") {
                                                document.getElementById("txtMessage").innerHTML = "Falta partes afectadas del incidente";
                                                $("#txtPartes").focus();
                                                return false;
                                            }
                                            else {
                                                if ($('#txtTipo').val() == "") {
                                                    document.getElementById("txtMessage").innerHTML = "Falta tipo incidente";
                                                    $("#txtTipo").focus();
                                                    return false;
                                                }
                                                else {
                                                    if ($('#txtActos').val() == "") {
                                                        document.getElementById("txtMessage").innerHTML = "Falta actos inseguros";
                                                        $("#txtActos").focus();
                                                        return false;
                                                    }
                                                    else {
                                                        if ($('#txtAgente').val() == "") {
                                                            document.getElementById("txtMessage").innerHTML = "Falta agente de la lesión";
                                                            $("#txtAgente").focus();
                                                            return false;
                                                        }
                                                        else {
                                                            if ($('#txtCondicion').val() == "") {
                                                                document.getElementById("txtMessage").innerHTML = "Falta condiciones inseguras";
                                                                $("#txtCondicion").focus();
                                                                return false;
                                                            }
                                                            else {
                                                                if ($('#txtDaño').val() == "") {
                                                                    document.getElementById("txtMessage").innerHTML = "Falta tipo daño";
                                                                    $("#txtDaño").focus();
                                                                    return false;
                                                                }
                                                                else {
                                                                    if ($('#txtAfectacion').val() == "") {
                                                                        document.getElementById("txtMessage").innerHTML = "Falta maquinaria, equipo, procesos afectados";
                                                                        $("#txtAfectacion").focus();
                                                                        return false;
                                                                    }
                                                                    else {
                                                                        if ($('#txtOcacionados').val() == "") {
                                                                            document.getElementById("txtMessage").innerHTML = "Falta definir daños ocasionados";
                                                                            $("#txtOcacionados").focus();
                                                                            return false;
                                                                        }
                                                                        else {
                                                                            if ($('#txtCostos').val() == "") {
                                                                                document.getElementById("txtMessage").innerHTML = "Falta definir costos ocasionados";
                                                                                $("#txtCostos").focus();
                                                                                return false;
                                                                            }
                                                                            else {
                                                                                if ($('#txtDescripcion').val() == "") {
                                                                                    document.getElementById("txtMessage").innerHTML = "Falta definir descripción del incidente";
                                                                                    $("#txtDescripcion").focus();
                                                                                    return false;
                                                                                }
                                                                                else {
                                                                                    if ($('#txtEvitar').val() == "") {
                                                                                        document.getElementById("txtMessage").innerHTML = "Falta definir como se pudo evitar el incidente";
                                                                                        $("#txtEvitar").focus();
                                                                                        return false;
                                                                                    }
                                                                                    else {
                                                                                        if ($('#txtAcciones').val() == "") {
                                                                                            document.getElementById("txtMessage").innerHTML = "Falta definir acciones inmediatas";
                                                                                            $("#txtAcciones").focus();
                                                                                            return false;
                                                                                        }
                                                                                        else {
                                                                                            if ($('#txtAtencion').val() == "") {
                                                                                                document.getElementById("txtMessage").innerHTML = "Falta definir atención brindada";
                                                                                                $("#txtAtencion").focus();
                                                                                                return false;
                                                                                            }
                                                                                            else {
                                                                                                if ($('#txtOcacionados').val() == "") {
                                                                                                    document.getElementById("txtMessage").innerHTML = "Falta definir daños ocasionados";
                                                                                                    $("#txtOcacionados").focus();
                                                                                                    return false;
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    document.getElementById("txtMessage").innerHTML = "";
    return true;
}

// las siguientes funciones crean un nuevo incidente
function AddIncidente() {
    // Crea un nuevo incidente
    if (!validateIncidents()) {
        return false;
    }

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
        FechaReporte: $("#FechaReporte").val(),
        FechaIncidente: $("#FechaIncidente").val(),
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
        LesionPersonal: $("#txtLesion").val(),
        DañoMaterial: $("#txtMaterial").val(),
        MedioAmbiente: $("#txtAmbiente").val(),
        Imagen: $("#txtImagen").val(),
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
                $("#btnAddIncidente").hide();
                $("#btnCanIncidente").hide();
            }
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
        FechaReporte: $("#FechaReporte").val(),
        FechaIncidente: $("#FechaIncidente").val(),
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
        LesionPersonal: $("#txtLesion").val(),
        DañoMaterial: $("#txtMaterial").val(),
        MedioAmbiente: $("#txtAmbiente").val(),
        Imagen: $("#txtImagen").val(),
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
    // Crea una nueva autorización
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
        success: function (response) {
            alert(response.mensaj);
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
    // Crea un nuevo cargo
    $(".tabAddCargos").css("display", "none");
    var cargoVM = {
        ID: "0",
        Codigo: $("#txtCodigo").val(),
        descripcion: $("#txtDescrip").val()
    };
    $.ajax({
        type: "POST",
        url: "/Organizations/CreateNewCargo",
        data: { model: cargoVM },
        dataType: "json",
        success: function (response) {
            $("#btnAddCargo").hide();
            $("#btnCanCargo").hide();
            $("#txtCodigo").val("");
            $("#txtDescrip").val("");
            $(".tabAddCargos").css("display", "none");
            $("#addCargo").focus();
            alert(response.mensaj);
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
        success: function (response) {
            $("#btnAddZone").hide();
            $("#btnCanZone").hide();
            $("#txtZone").val("");
            $(".tabAddZones").css("display", "none");
            alert(response.mensaj);
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
        success: function (response) {
            $("#btnAddProcess").hide();
            $("#btnCanProcess").hide();
            $("#txtProcess").val("");
            $(".tabAddProcess").css("display", "none");
            alert(response.mensaj);
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
        success: function (response) {
            $("#btnAddActivity").hide();
            $("#btnCanActivity").hide();
            $("#txtActivity").val("");
            $(".tabAddActivitys").css("display", "none");
            alert(response.mensaj);
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
        Descripcion: $("#txtDescripcion").val()
    };

    $.ajax({
        type: "POST",
        url: "/Organizations/CreateTask",
        data: { model: taskVM },
        dataType: "json",
        success: function (response) {
            $("#btnAddTask").hide();
            $("#btnCanTask").hide();
            $("#txtDescripcion").val("");
            alert(response.mensaj);
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
        success: function (response) {
            document.getElementById("txtLogin").innerHTML = response.mensaj;
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
                document.getElementById("txtLogin").innerHTML = response.mensaj;
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function SelectComponent() {
    $("#txtComponent").val(document.getElementById("txtComponent").value);
}

function DeleteUser(id) {
    // Eliminar un usuario
    var text = "";
    text += "Esta seguro de querer borrar este usuario ? :" + "\n\n";
    var respuesta = confirm(text);
    if (respuesta == true) {
        $.ajax({
            url: "/Accounts/DeleteUser/" + id,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            async: true,
            success: function (response) {
                alert(response.mensaj);
                if (response.result == 'Redirect') {
                    window.location = response.url;
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status);
                alert(thrownError);
            }
        });
    }
}

function CancelAccion() {
    $(".tabIdCausas").css("display", "none");
    $("#zona").val("");
    $("#proceso").val("");
    $("#activity").val("");
    $("#tarea").val("");
    $("#fechaSolicitud").val("");
    $("#categoriaAcc").val("");
    $("#idTrabajador").val("");
    $("#idFuente").val("");
    $("#idDescrip").val("");
    $("#idEficaciaAnt").val("");
    $("#idEficaciaDesp").val("");
    $("#idFechaCierre").val("");
    $("#idEfectiva").val("");
    $("#idEstado").val("");
}

// Gráficas

function chartValueDangers() {
    // Gráficar valoración peligros
    $.ajax({
        url: "/Riesgos/GetAllRisks",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            $("#ValueDangers").attr("src", response);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function chartClassDangers() {
    // Gráficar clasificación peligros
    $.ajax({
        url: "/Riesgos/GetAllDangers",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            $("#ClassDangers").attr("src", response);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function chartCommonActivitys() {
    // Gráficar actividades rutinarias
    $.ajax({
        url: "/Riesgos/GetAllActivitys",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            $("#CommonActivitys").attr("src", response);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function chartEfectosPosibles() {
    // Gráficar efectos posibles
    $.ajax({
        url: "/Riesgos/GetAllEfects",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            $("#EfectosPosibles").attr("src", response);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function gestorRiesgos() {
// implementa funcionalidad de riesgos
    ClearTextBox();

    $("#idPeligros").click(function () {
        $(".tabPeligros").css("display", "block");
        $(".tabEvalRiesgos").css("display", "none");
        $(".tabMediAplica").css("display", "none");
        $("#idPeligros").show();
    });

    $("#idPeligros").dblclick(function () {
        $(".tabPeligros").css("display", "none");
    });

    $("#evalRiesgos").click(function () {
        $(".tabEvalRiesgos").css("display", "block");
        $(".tabPeligros").css("display", "none");
        $(".tabMediAplica").css("display", "none");
        $("#evalRiesgos").show();
    });

    $("#evalRiesgos").dblclick(function () {
        $(".tabEvalRiesgos").css("display", "none");
    });

    $("#mediAplica").click(function () {
        $(".tabMediAplica").css("display", "block");
        $(".tabEvalRiesgos").css("display", "none");
        $(".tabPeligros").css("display", "none");
        $("#mediAplica").show();
        mostrarInterven();
    });

    $("#mediAplica").dblclick(function () {
        $(".tabMediAplica").css("display", "none");
        $(".tabAddInterven").css("display", "none");
    });

    $("#aplicarMedi").click(function () {
        $(".tabInterven").css("display", "block");
    });

    $("#aplicarMedi").dblclick(function () {
        $(".tabInterven").css("display", "none");
    });

    $("#addIntervencion").click(function () {
        ClearTextBox();
        $(".tabAddInterven").css("display", "block");
        $(".tabCerrar").css("display", "none");
        $(".tabMediAplica").css("display", "none");
        $("#btnUpdInterven").hide();
        $("#btnAddInterven").show();
    });

    $('#FechaInicial').change(function () {
        var fecha = $("#FechaInicial").val();
        validarFechaIni(fecha);
    });

    $('#FechaFinal').change(function () {
        var fechaIni = $("#FechaInicial").val();
        var fechaFin = $("#FechaFinal").val();
        validarFechaFin(fechaIni, fechaFin);
    });

    $('#deficienciaSelected').click(function () {
        var selectedCategoria = $('#deficienciaSelected').val();
        var nd = 0;
        switch (selectedCategoria) {
            case "1":
                nd = 10;
                break;

            case "2":
                nd = 6;
                break;

            case "3":
                nd = 2;
                break;

            default:
                nd = 0;
                break;
        }

        $('#deficiencia').val(nd);
        calcularProbabilidad();
    });

    $('#exposicionSelected').click(function () {
        var selectedExposicion = $('#exposicionSelected').val();
        var ne = 0;
        switch (selectedExposicion) {
            case "1":
                ne = 4;
                break;

            case "2":
                ne = 3;
                break;

            case "3":
                ne = 2;
                break;

            default:
                ne = 1;
                break;
        }

        $('#exposicion').val(ne);
        calcularProbabilidad();
    });

    $('#consecuenciaSelected').click(function () {
        var selectedExposicion = $('#consecuenciaSelected').val();
        var nc = 0;
        switch (selectedExposicion) {
            case "1":
                nc = 100;
                break;

            case "2":
                nc = 60;
                break;

            case "3":
                nc = 25;
                break;

            default:
                nc = 10;
                break;
        }
        $('#consecuencia').val(nc);
        calcularRiesgo();
    });

    $("#plans").click(function () {
        ResetTab();
        $(".tabGesPlanAcc").css("display", "block");
        $(".tabCerrar").css("display", "none");
        mostrarPlanAcc();
    });

    $("#plans").dblclick(function () {
        ResetTab();
    });

    $("#addPlans").click(function () {
        $(".tabAddPlanAcc").css("display", "block");
        $(".tabCerrar").css("display", "none");
        $("#btnAddPlan").show();
        $("#btnCanPlan").show();
        $("#btnUpdPlan").hide();
    });
}

function calcularProbabilidad() {
    var nd = $('#deficiencia').val();
    var ne = $('#exposicion').val();
    var probabilidad = nd * ne;
    $('#probabilidad').val(probabilidad);
    var interpretaNP = " ";
    var colorStyle = " ";
    switch (true) {
        case (probabilidad >= 24 && probabilidad <= 40):
            interpretaNP = "Muy alto (MA)";
            colorStyle = "red";
            break;
        case (probabilidad >= 10 && probabilidad < 24):
            interpretaNP = "Alto (A)";
            colorStyle = "orange";
            break;
        case (probabilidad >= 8 && probabilidad < 10):
            interpretaNP = "Mdio (M)";
            colorStyle = "yellow";
            break;
        case (probabilidad >= 2 && probabilidad < 8):
            interpretaNP = "Bajo (B)";
            colorStyle = "green";
            break;

        default:
            interpretaNP = "Bajo (B)";
            colorStyle = "green";
            break;
    }
    $('#interpretaNP').val(interpretaNP);
    $('#interpretaNP').css({ "backgroundColor": colorStyle, "font-size": "100%"});
}

function calcularRiesgo() {
    calcularProbabilidad();
    var np = $('#probabilidad').val();
    var nc = $('#consecuencia').val();
    var riesgo = np * nc;
    $('#riesgo').val(riesgo);
    var interpretaNR = " ";
    var colorStyle = " ";
    var aceptability = "";
    switch (true) {
        case (riesgo >= 600):
            interpretaNR = "I";
            colorStyle = "red";
            aceptability = "No Aceptable";
            break;

        case (riesgo >= 150 && riesgo < 600):
            interpretaNR = "II";
            colorStyle = "orange";
            aceptability = "No Aceptable o Aceptable con control Específico";
            break;

        case (riesgo >= 40 && riesgo < 150):
            interpretaNR = "III";
            colorStyle = "yellow";
            aceptability = "Mejorable";
            break;

        case (riesgo < 40):
            interpretaNR = "IV";
            colorStyle = "green";
            aceptability = "Aceptable";
            break;

        default:
            interpretaNR = "IV";
            colorStyle = "green";
            aceptability = "Aceptable";
            break;
    }

    $('#txtAceptabilidad').val(aceptability);
    $('#interpretaNR').val(interpretaNR);
    $('#interpretaNR').css({ "backgroundColor": colorStyle, "font-size": "100%" });
    $('#txtAceptabilidad').css({ "backgroundColor": colorStyle, "font-size": "100%"});
}

function FindActions() {

    // Gráficar acciones
    $.ajax({
        url: "/Acciones/GetAllPlans",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            document.getElementById("noConformance").innerHTML = "No conformidades : " + response.noConformance;
            document.getElementById("numActions").innerHTML = "Estado Acciones : " + response.numPlans;
            document.getElementById("numCorrective").innerHTML = "Acciones Correctivas : " + response.numCorrective;
            document.getElementById("noConformance").style.backgroundColor = "gray";
            document.getElementById("numActions").style.backgroundColor = "gray";
            document.getElementById("numCorrective").style.backgroundColor = "gray";
            $("#numActions").focus();
            $(".tabConsActions").css("display", "block");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function chartNoConformance() {

    // Gráficar no conformidades
    $.ajax({
        url: "/Acciones/GetNoConformance",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            $("#NoConformance").attr("src", response);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function chartActions() {

    // Gráficar acciones
    $.ajax({
        url: "/Acciones/GetAllActions",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            $("#NumActions").attr("src", response);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function chartCorrectiveActions() {

    // Gráficar acciones correctivas / preventivas / mejora
    $.ajax({
        url: "/Acciones/GetAllCorrectiveActions",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            $("#NumCorrective").attr("src", response);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function AddEvent() {
    // Crea un nuevo evento
    $('.tabAddEvents').css("display", "none");
    var event = {
        ID: "0",
        IncidentID: $("#txtIncidenteID").val(),
        Name: $("#txtEvent").val(),
        Order: $("#txtOrder").val(),
    };
    $.ajax({
        type: "POST",
        url: "/Incidentes/CreateEvent",
        data: { model: event },
        dataType: "json",
        success: function (response) {
            var num = +($("#txtOrder").val()) + 1;
            $("#txtEvent").val("");
            $("#txtOrder").val(num);
            $("#btnAddEvent").hide();
            $("#btnCanEvent").hide();
            alert(response.mensaj);
            ShowEvents();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}
function ShowEvents() {
    // Mostrar todos los eventos
    var incidentID = $("#txtIncidenteID").val();
    $.ajax({
        url: "/Incidentes/GetEvents",
        data: { id: incidentID },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,
        success: function (result) {
            if (result != null) {
                var html = '';
                var name = '';
                var num = 0;
                $.each(result, function (key, item) {
                    num++;
                    name = item.Name.toUpperCase();
                    html += '<tr>';
                    html += '<td>' + num + '</td>';
                    html += '<td>' + name + '</td>';
                    html += '<td><a href="#" onclick="return getEventByID(' + item.ID + ')">Editar</a> | <a href = "#" onclick = "DeleteEvent(' + item.ID + ')"> Borrar</a></td>';
                    html += '</tr>';
                });
                $('.tbody').html(html);
            }
            $('.tabGesEvents').css("display", "block");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function getEventByID(eventID) {
    $.ajax({
        async: true,
        type: 'GET',
        url: "/Incidentes/UpdateEvent",
        data: { id: eventID },
        dataType: "json",
        contentType: "application/json;charset=UTF-8",
        success: function (result) {
            $("#txtEventID").val(result.ID);
            $("#txtEvent").val(result.Name);
            $("#txtEvent").focus();
            $("#txtOrder").val(result.Order);
            $("#txtEvent").focus();
            $("#btnAddEvent").hide();
            $("#btnUpdEvent").show();
            $("#btnCanEvent").show();
            $(".tabAddEvents").css("display", "block");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function DeleteEvent(id) {
    $.ajax({
        url: "/Incidentes/DeleteEvent/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        async: true,                                             // si es asincrónico o no
        success: function (result) {
            var text = "";
            text += "Esta seguro de querer borrar este evento ? :\n\n";
            text += "Evento : " + result.data.Name + "\n";
            text += "Orden : " + result.data.Order + "\n";
            var respuesta = confirm(text);
            if (respuesta == true) {
                $.ajax({
                    url: "/Incidentes/DeleteEvent/" + id,
                    type: "POST",
                    contentType: "application/json;charset=UTF-8",
                    dataType: "json",
                    async: true,                                               // si es asincrónico o no
                    success: function (result) {
                        alert(result.mensaj);
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        alert(xhr.status);
                        alert(thrownError);
                    }
                });
            }
            ClearTextBox();
            ShowEvents();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function UpdateEvent() {
    // Actualiza un evento creado, captura la incidenteID de id = txtIncidenteID
    $(".tabGesEvents").css("display", "none");
    $(".tabAddEvents").css("display", "none");
    var eventVM = {
        ID: $("#txtEventID").val(),
        IncidentID: $("#txtIncidenteID").val(),
        Name: $("#txtEvent").val(),
        Order: $("#txtOrder").val()
    };
    $.ajax({
        type: "POST",
        url: "/Incidentes/UpdateEvent",
        data: { model: eventVM },
        dataType: "json",
        success: function (response) {
            alert(response.mensaj);
            $(".tabGesEvents").css("display", "block");
            ShowEvents();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function AddCause() {
    // Crea un nuevo factor causal
    $('.tabAddCauses').css("display", "none");
    var cause = {
        ID: "0",
        IncidentID: $("#txtIncidenteID").val(),
        EventID: $("#txtCauseAnalice").val(),
        CausalFactor: $("#txtCause").val(),
        PotencialFactor: $("#txtPotencial").val()
    };
    $.ajax({
        type: "POST",
        url: "/Incidentes/CreateCause",
        data: { model: cause },
        dataType: "json",
        success: function (response) {
            $("#txtCauseAnalice").val("");
            $("#txtCause").val("");
            $("#txtPotencial").val("");
            $("#btnAddCause").hide();
            $("#btnCanCause").hide();
            alert(response.mensaj);
            ShowCauses();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function ShowCauses() {
    // Mostrar todos los factores causales
    var incidentID = $("#txtIncidenteID").val();
    $.ajax({
        url: "/Incidentes/GetCauses",
        data: { id: incidentID },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,
        success: function (result) {
            var html = '';
            var cause = '';
            var potencial = '';
            $.each(result, function (key, item) {
                name = item.Name.toUpperCase();
                switch (item.CausalFactor) {
                    case 1:
                        cause = "Causa Directa";
                        break;

                    case 2:
                        cause = "Causa Contribuyente";
                        break;

                    case 3:
                        cause = "Causa Principal";
                        break;
                }

                switch (item.PotencialFactor) {
                    case 1:
                        potencial = "Falta de conciencia";
                        break;

                    case 2:
                        potencial = "Falta de prácticas seguras de trabajo";
                        break;

                    case 3:
                        potencial = "Falta de aplicación de prácticas laborales seguras";
                        break;

                    case 4:
                        potencial = "Equipo/materiales inadecuados";
                        break;

                    case 5:
                        potencial = "Diseños inadecuados";
                        break;
                }

                html += '<tr>';
                html += '<td>' + name + '</td>';
                html += '<td>' + cause + '</td>';
                html += '<td>' + potencial + '</td>';
                html += '<td><a href="#" onclick="return getCauseByID(' + item.ID + ')">Editar</a> | <a href = "#" onclick = "DeleteCause(' + item.ID + ')"> Borrar</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
            $('.tabGesCauses').css("display", "block");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function getCauseByID(causeID) {
    $.ajax({
        async: true,
        type: 'GET',
        url: "/Incidentes/UpdateCause",
        data: { id: causeID },
        dataType: "json",
        contentType: "application/json;charset=UTF-8",
        success: function (result) {
            $.each(result, function (key, item) {
                $("#txtCauseID").val(item.ID);
                $("#txtCauseAnalice").val(item.Event);
                $("#txtCauseAnalice").focus();
                $("#txtCause").val(item.CausalFactor);
                $("#txtPotencial").val(item.PotencialFactor);
            });
            $("#btnAddCause").hide();
            $("#btnUpdCause").show();
            $("#btnCanCause").show();
            $(".tabAddCauses").css("display", "block");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function DeleteCause(id) {
    $.ajax({
        url: "/Incidentes/DeleteCause/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        async: true,                                             // si es asincrónico o no
        success: function (result) {
            var text = "";
            text += "Esta seguro de querer borrar esta causa ? :\n\n";
            text += "Evento : " + result.data.EventID + "\n";
            text += "Causal Factor : " + result.data.CausalFactor + "\n";
            text += "Potencial Factor : " + result.data.PotencialFactor + "\n";
            var respuesta = confirm(text);
            if (respuesta == true) {
                $.ajax({
                    url: "/Incidentes/DeleteCause/" + id,
                    type: "POST",
                    contentType: "application/json;charset=UTF-8",
                    dataType: "json",
                    async: true,                                               // si es asincrónico o no
                    success: function (result) {
                        alert(result.mensaj);
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        alert(xhr.status);
                        alert(thrownError);
                    }
                });
            }
            ClearTextBox();
            ShowCauses();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function UpdateCause() {
    // Actualiza un evento creado, captura la incidenteID de id = txtIncidenteID
    $(".tabGesCauses").css("display", "none");
    $(".tabAddCauses").css("display", "none");
    var eventVM = {
        ID: $("#txtCauseID").val(),
        IncidentID: $("#txtIncidenteID").val(),
        EventID: $("#txtCauseAnalice").val(),
        CausalFactor: $("#txtCause").val(),
        PotencialFactor: $("#txtPotencial").val()
    };
    $.ajax({
        type: "POST",
        url: "/Incidentes/UpdateCause",
        data: { model: eventVM },
        dataType: "json",
        success: function (response) {
            alert(response.mensaj);
            $(".tabGesCauses").css("display", "block");
            ShowCauses();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function CancelCause() {
    $(".tabGesCauses").css("display", "none");
    $(".tabAddCauses").css("display", "none");
    $("#txtCauseAnalice").val("");
    $("#txtCause").val("");
    $("#txtPotencial").val("");
}

function AddBarrier() {
    // Crea una nueva defensa
    $('.tabAddBarriers').css("display", "none");
    var barrier = {
        ID: "0",
        IncidentID: $("#txtIncidenteID").val(),
        EventID: $("#txtBarrier").val(),
        BarrierCategory: $("#txtBarrierCat").val()
    };
    $.ajax({
        type: "POST",
        url: "/Incidentes/CreateBarrier",
        data: { model: barrier },
        dataType: "json",
        success: function (response) {
            $("#txtBarrier").val("");
            $("#txtBarrierCat").val("");
            $("#btnAddBarrier").hide();
            $("#btnCanBarrier").hide();
            alert(response.mensaj);
            ShowBarriers();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function ShowBarriers() {
    // Mostrar todos las defenas
    var incidentID = $("#txtIncidenteID").val();
    $.ajax({
        url: "/Incidentes/GetBarriers",
        data: { id: incidentID },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,
        success: function (result) {
            var html = '';
            var barrier = '';
            $.each(result, function (key, item) {
                name = item.Name.toUpperCase();
                switch (item.BarrierCategory) {
                    case 1:
                        barrier = "Equipo";
                        break;

                    case 2:
                        barrier = "Diseño";
                        break;

                    case 3:
                        barrier = "Administración";
                        break;

                    case 4:
                        barrier = "Supervisión";
                        break;

                    case 5:
                        barrier = "Dispositivos";
                        break;

                    case 6:
                        barrier = "Habilidades";
                        break;

                }

                html += '<tr>';
                html += '<td>' + name + '</td>';
                html += '<td>' + barrier + '</td>';
                html += '<td><a href="#" onclick="return getBarrierByID(' + item.ID + ')">Editar</a> | <a href = "#" onclick = "DeleteBarrier(' + item.ID + ')"> Borrar</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
            $('.tabGesBarriers').css("display", "block");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function getBarrierByID(barrierID) {
    $.ajax({
        async: true,
        type: 'GET',
        url: "/Incidentes/UpdateBarrier",
        data: { id: barrierID },
        dataType: "json",
        contentType: "application/json;charset=UTF-8",
        success: function (result) {
            $.each(result, function (key, item) {
                $("#txtBarrierID").val(item.ID);
                $("#txtBarrier").val(item.Event);
                $("#txtBarrier").focus();
                $("#txtBarrierCat").val(item.BarrierCategory);
            });

            $("#btnUpdBarrier").show();
            $("#btnCanBarrier").show();
            $("#btnAddBarrier").hide();
            $(".tabAddBarriers").css("display", "block");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function DeleteBarrier(id) {
    $.ajax({
        url: "/Incidentes/DeleteBarrier/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        async: true,                                             // si es asincrónico o no
        success: function (result) {
            var text = "";
            text += "Esta seguro de querer borrar esta causa ? :\n\n";
            text += "Evento : " + result.data.EventID + "\n";
            text += "Categoría defensa : " + result.data.BarrierCategory + "\n";
            var respuesta = confirm(text);
            if (respuesta == true) {
                $.ajax({
                    url: "/Incidentes/DeleteBarrier/" + id,
                    type: "POST",
                    contentType: "application/json;charset=UTF-8",
                    dataType: "json",
                    async: true,                                               // si es asincrónico o no
                    success: function (result) {
                        alert(result.mensaj);
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        alert(xhr.status);
                        alert(thrownError);
                    }
                });
            }
            ClearTextBox();
            ShowBarriers();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function UpdateBarrier() {
    // Actualiza una defensa, captura la incidenteID de id = txtIncidenteID
    $(".tabGesBarriers").css("display", "none");
    $(".tabAddBarriers").css("display", "none");
    var barrierVM = {
        ID: $("#txtBarrierID").val(),
        IncidentID: $("#txtIncidenteID").val(),
        EventID: $("#txtBarrier").val(),
        BarrierCategory: $("#txtBarrierCat").val()
    };
    $.ajax({
        type: "POST",
        url: "/Incidentes/UpdateBarrier",
        data: { model: barrierVM },
        dataType: "json",
        success: function (response) {
            alert(response.mensaj);
            $(".tabGesBarriers").css("display", "block");
            ShowBarriers();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function CancelBarrier() {
    $(".tabGesBarrier").css("display", "none");
    $(".tabAddBarriers").css("display", "none");
    $("#txtBarrier").val("");
    $("#txtBarrierCat").val("");
}

function AddRootCause() {
    // Crea una nueva Causa principal
    $('.tabAddRootCauses').css("display", "none");
    var reason = $("#txtReason").val();
    var rootCause = {
        ID: "0",
        IncidentID: $("#txtIncidenteID").val(),
        Name: $("#txtRootCause").val()
    };
    $.ajax({
        type: "POST",
        url: "/Incidentes/CreateRootCause",
        data: {
            model: rootCause,
            reason: reason
        },
        dataType: "json",
        success: function (response) {
            $("#txtReason").val("");
            $("#btnAddRootCause").hide();
            $("#btnCanRootCause").hide();
            alert(response.mensaj);
            ShowRootCauses();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function ShowRootCauses() {
    // Mostrar todos las defenas
    var incidentID = $("#txtIncidenteID").val();
    $.ajax({
        url: "/Incidentes/GetRootCauses",
        data: { id: incidentID },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                name = item.Name.toUpperCase();
                reason = item.Reason.toUpperCase();
                html += '<tr>';
                html += '<td>' + name + '</td>';
                html += '<td>' + reason + '</td>';
                html += '<td><a href="#" onclick="return getRootCauseByID(' + item.ID + ')">Editar</a> | <a href = "#" onclick = "DeleteRootCause(' + item.ID + ')"> Borrar</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
            $('.tabGesRootCauses').css("display", "block");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function getRootCauseByID(rootCauseID) {
    $.ajax({
        async: true,
        type: 'GET',
        url: "/Incidentes/UpdateRootCause",
        data: {
            id: rootCauseID
        },
        dataType: "json",
        contentType: "application/json;charset=UTF-8",
        success: function (result) {
            $.each(result, function (key, item) {
                $("#txtRootCauseID").val(item.ID);
                $("#txtRootCause").val(item.ID);
                $("#txtReasonID").val(item.ReasonID);
                $("#txtReason").val(item.Reason);
            });
            $("#btnUpdRootCause").show();
            $("#btnCanRootCause").show();
            $("#btnAddRootCause").hide();
            $(".tabAddRootCauses").css("display", "block");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function DeleteRootCause(id) {
    $.ajax({
        url: "/Incidentes/DeleteRootCause/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        async: true,                                             // si es asincrónico o no
        success: function (result) {
            var text = "";
            text += "Esta seguro de querer borrar esta causa ? :\n\n";
            text += "Causa principal : " + result.data.Name + "\n";
            var respuesta = confirm(text);
            if (respuesta == true) {
                $.ajax({
                    url: "/Incidentes/DeleteRootCause/" + id,
                    type: "POST",
                    contentType: "application/json;charset=UTF-8",
                    dataType: "json",
                    async: true,                                               // si es asincrónico o no
                    success: function (result) {
                        alert(result.mensaj);
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        alert(xhr.status);
                        alert(thrownError);
                    }
                });
            }
            ClearTextBox();
            ShowRootCauses();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function UpdateRootCause() {
    // Actualiza una defensa, captura la incidenteID de id = txtIncidenteID
    $(".tabGesBRootCauses").css("display", "none");
    $(".tabAddRootCauses").css("display", "none");
    var reason = $("#txtReason").val();
    var reasonID = $("#txtReasonID").val();
    var rootCauseVM = {
        ID: $("#txtRootCauseID").val(),
        IncidentID: $("#txtIncidenteID").val(),
        Name: $("#txtRootCause").val()
    };
    $.ajax({
        type: "POST",
        url: "/Incidentes/UpdateRootCause",
        data: {
            model: rootCauseVM,
            reasonID: reasonID,
            reason: reason
        },
        dataType: "json",
        success: function (response) {
            alert(response.mensaj);
            $(".tabGesRootCause").css("display", "block");
            ShowRootCauses();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function CancelRootCause() {
    $(".tabGesRootCause").css("display", "none");
    $(".tabAddRootCauses").css("display", "none");
    $("#txtRootCause").val("");
    $("#txtReason").val("");
}

function AddRecomendation() {
    // Crea una nueva recomendación
    $('.tabAddRecomendations').css("display", "none");
    var recomendationVM = {
        ID: "0",
        IncidentID: $("#txtIncidenteID").val(),
        RootCauseID: $("#txtMainCause").val(),
        Name: $("#txtRecomendation").val()
    };
    $.ajax({
        type: "POST",
        url: "/Incidentes/CreateRecomendation",
        data: {model: recomendationVM},
        dataType: "json",
        success: function (response) {
            $("#txtRecomendation").val("");
            $("#btnAddRecomendation").hide();
            $("#btnCanRecomendation").hide();
            alert(response.mensaj);
            ShowRecomendations();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
    AddPlan(2); // adicionar recomendación
}

function ShowRecomendations() {
    // Mostrar todos las recomendaciones
    var incidentID = $("#txtIncidenteID").val();
    $.ajax({
        url: "/Incidentes/GetRecomendations",
        data: { id: incidentID },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,
        success: function (result) {
            var html = '';
            var name = "";
            var recomendation = "";
            $.each(result, function (key, item) {
                name = item.Name.toUpperCase();
                recomendation = item.Recomendation.toUpperCase();
                html += '<tr>';
                html += '<td>' + name + '</td>';
                html += '<td>' + recomendation + '</td>';
                html += '<td><a href="#" onclick="return getRecomendationByID(' + item.RecomendationID + ')">Editar</a> | <a href = "#" onclick = "DeleteRecomendation(' + item.RecomendationID + ')"> Borrar</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
            $('.tabGesRemomendations').css("display", "block");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function getRecomendationByID(recomendationID) {
    $.ajax({
        async: true,
        type: 'GET',
        url: "/Incidentes/UpdateRecomendation",
        data: {
            id: recomendationID
        },
        dataType: "json",
        contentType: "application/json;charset=UTF-8",
        success: function (result) {
            $.each(result, function (key, item) {
                $("#txtRootCauseID").val(item.ID);
                $("#txtMainCause").val(item.ID);
                $("#txtMainCause").focus();
                $("#txtRecomendationID").val(item.RecomendationID);
                $("#txtRecomendation").val(item.Recomendation);
            });
            $("#btnUpdRecomendation").show();
            $("#btnCanRecomendation").show();
            $("#btnAddRecomendation").hide();
            $(".tabAddRecomendations").css("display", "block");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function DeleteRecomendation(id) {
    $.ajax({
        url: "/Incidentes/DeleteRecomendation/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        async: true,                                             // si es asincrónico o no
        success: function (result) {
            var text = "";
            text += "Esta seguro de querer borrar esta recomendación ? :\n\n";
            text += "Recomendación : " + result.data.Name + "\n";
            var respuesta = confirm(text);
            if (respuesta == true) {
                $.ajax({
                    url: "/Incidentes/DeleteRecomendation/" + id,
                    type: "POST",
                    contentType: "application/json;charset=UTF-8",
                    dataType: "json",
                    async: true,                                               // si es asincrónico o no
                    success: function (result) {
                        alert(result.mensaj);
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        alert(xhr.status);
                        alert(thrownError);
                    }
                });
            }
            ClearTextBox();
            ShowRecomendations();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function UpdateRecomendation() {
    // Actualiza una recomendación, captura la incidenteID de id = txtIncidenteID
    $(".tabGesRecomendations").css("display", "none");
    $(".tabAddRecomendations").css("display", "none");
    var recomendationVM = {
        ID: $("#txtRecomendationID").val(),
        IncidentID: $("#txtIncidenteID").val(),
        RootCauseID: $("#txtRootCauseID").val(),
        Name: $("#txtRecomendation").val()
    };
    $.ajax({
        type: "POST",
        url: "/Incidentes/UpdateRecomendation",
        data: { model: recomendationVM },
        dataType: "json",
        success: function (response) {
            alert(response.mensaj);
            $(".tabGesRecomendations").css("display", "block");
            ShowRecomendations();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function CancelRecomendation() {
    $(".tabGesRecomendations").css("display", "none");
    $(".tabAddRecomendations").css("display", "none");
    $("#txtRecomendation").val("");
}

function AddPlan(id) {

    // Adicionar nueva acción
    var descrip = "";
    var trabajador = "";
    var fechaIni = "";
    var fechaFin = "";
    var accion = "";

    if (id == 1) {
        fechaIni = $("#FechaInicial").val();
        fechaFin = $("#FechaFinal").val();
        trabajador = $("#idRespons").val();
        descrip = "Matriz de Riesgos";
        accion = $("#txtNombre").val();
    } else {
        fechaIni = $("#FechaReporte").val();
        fechaFin = $("#FechaReporte").val();
        trabajador = $("#txtInformante").val();
        descrip = "Investigación incidente / accidente";
        accion = $("#txtRecomendation").val();
    }

    if ($("#txtAccionID").val() == "") {
        var accionVM = {
            ID: "0",
            ZonaID: $("#zona").val(),
            ProcesoID: $("#proceso").val(),
            ActividadID: $("#activity").val(),
            TareaID: $("#tarea").val(),
            FechaSolicitud: fechaIni,
            Categoria: "2",
            TrabajadorID: trabajador,
            FuenteAccion: "11",
            Descripcion: descrip,
            EficaciaAntes: "3",
            EficaciaDespues: "3",
            FechaCierre: fechaFin,
            Efectiva: false,
            ActionCategory: "1"
        };

        $.ajax({
            type: "POST",
            url: "/Acciones/CreateAccion",
            data: { model: accionVM },
            dataType: "json",
            success: function (response) {
                if (response.data != false) {
                    $("#txtAccionID").val(response.data);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status);
                alert(thrownError);
            }
        });
    }

    // Crea un nuevo plan
    $('.tabAddPlans').css("display", "none");
    var accionID = $("#txtAccionID").val();
    var planAccionVM = {
        ID: 0,
        AccionID: accionID,
        FechaInicial: fechaIni,
        FechaFinal: fechaFin,
        Causa: "1",
        Accion: accion,
        TrabajadorID: trabajador,
        Prioritaria: true,
        Costos: "0",
        Responsable: trabajador,
        ActionCategory: "1"
    };

    $.ajax({
        type: "POST",
        url: "/Acciones/CreatePlanAccion",
        data: { model: planAccionVM },
        dataType: "json",
        success: function (response) {
            $("#btnAddPlan").hide();
            $("#btnCanPlan").hide();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function getRecomendationByID(recomendationID) {
    $.ajax({
        async: true,
        type: 'GET',
        url: "/Incidentes/UpdateRecomendation",
        data: {
            id: recomendationID
        },
        dataType: "json",
        contentType: "application/json;charset=UTF-8",
        success: function (result) {
            $.each(result, function (key, item) {
                $("#txtRootCauseID").val(item.ID);
                $("#txtMainCause").val(item.ID);
                $("#txtMainCause").focus();
                $("#txtRecomendationID").val(item.RecomendationID);
                $("#txtRecomendation").val(item.Recomendation);
            });
            $("#btnUpdRecomendation").show();
            $("#btnCanRecomendation").show();
            $("#btnAddRecomendation").hide();
            $(".tabAddRecomendations").css("display", "block");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function DeleteRecomendation(id) {
    $.ajax({
        url: "/Incidentes/DeleteRecomendation/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        async: true,                                             // si es asincrónico o no
        success: function (result) {
            var text = "";
            text += "Esta seguro de querer borrar esta recomendación ? :\n\n";
            text += "Recomendación : " + result.data.Name + "\n";
            var respuesta = confirm(text);
            if (respuesta == true) {
                $.ajax({
                    url: "/Incidentes/DeleteRecomendation/" + id,
                    type: "POST",
                    contentType: "application/json;charset=UTF-8",
                    dataType: "json",
                    async: true,                                               // si es asincrónico o no
                    success: function (result) {
                        alert(result.mensaj);
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        alert(xhr.status);
                        alert(thrownError);
                    }
                });
            }
            ClearTextBox();
            ShowRecomendations();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function UpdateRecomendation() {
    // Actualiza una recomendación, captura la incidenteID de id = txtIncidenteID
    $(".tabGesRecomendations").css("display", "none");
    $(".tabAddRecomendations").css("display", "none");
    var recomendationVM = {
        ID: $("#txtRecomendationID").val(),
        IncidentID: $("#txtIncidenteID").val(),
        RootCauseID: $("#txtRootCauseID").val(),
        Name: $("#txtRecomendation").val()
    };
    $.ajax({
        type: "POST",
        url: "/Incidentes/UpdateRecomendation",
        data: { model: recomendationVM },
        dataType: "json",
        success: function (response) {
            alert(response.mensaj);
            $(".tabGesRecomendations").css("display", "block");
            ShowRecomendations();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function CancelRecomendation() {
    $(".tabGesRecomendations").css("display", "none");
    $(".tabAddRecomendations").css("display", "none");
    $("#txtRecomendation").val("");
}

function SendEmail(movimientID) {
    $(".tabGesMovimientos").css("display", "none");
    $.ajax({
        async: true,
        type: 'POST',
        url: "/Movimientos/SendMail",
        data: { id: movimientID },
        dataType: "json",
        success: function (result) {
            alert(result.mensaj);
            ShowMovimientos(ciclo);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    $(".tabGesMovimientos").css("display", "block");
    $(".tabAddMovimientos").css("display", "none");
    $("#btnAddMovimient").hide();
    $("#btnCanMovimient").show();
}

function AddEvaluation() {
    // Crea una nueva evaluación
    $('.tabGesCalifications').css("display", "none");
    $.ajax({
        type: "POST",
        url: "/Evaluations/CreateEvaluation",
        dataType: "json",
        success: function (response) {
            var evaluationID = response.data;
            if (response.data != false) {
                $("#txtEvaluationID").val(evaluationID);
            }
            alert(response.mensaj);
            ShowCalifications(evaluationID);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function ShowCalifications(evaluationID) {
    // Mostrar todos las calificaciones
    var evaluationID = $("#txtEvaluationID").val();
    $.ajax({
        url: "/Evaluations/GetCalifications",
        data: { id: evaluationID },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,
        success: function (result) {
            var html = '';
            var totales = 0, cumple = 0, noCumple = 0, noAplica = 0;
            $.each(result, function (key, item) {
                totales += item.Valoration;

                if (item.Cumple) {
                    cumple++;
                }
                if (item.NoCumple) {
                    noCumple++;
                }
                if (item.Justify) {
                    NoAplica++;
                }
                if (item.NoJustify) {
                    NoAplica++;
                }

                html += '<tr>';
                html += '<td>' + item.Ciclo + '</td>';
                html += '<td>' + item.Standard + '</td>';
                html += '<td>' + item.Item + '</td>';
                html += '<td>' + item.Name + '</td>';
                html += '<td>' + item.Valor + '</td>';
                html += '<td>' + item.Cumple + '</td>';
                html += '<td>' + item.NoCumple + '</td>';
                html += '<td>' + item.Justify + '</td>';
                html += '<td>' + item.NoJustify + '</td>';
                html += '<td>' + item.Valoration + '</td>';
                html += '<td>' + item.item.Observation + '</td>';
                html += '<td><a href="#" onclick="return getCalificationByID(' + item.ID + ')">Editar</a></td>';
                html += '</tr>';
            });

            $("#txtTotales").val("TOTALES : " + totales);
            $("#txtCumple").val("CUMPLE : " + cumple);
            $("#txtNoCumple").val("NO CUMPLE : " + noCumple);
            $("#txtNoAplica").val("NO APLICA : " + noAplica);

            switch (true)
            {
                case (totales > 85) :
                    $("#txtValoracion").val("VALORACIÓN ACEPTABLE");
                    $("#txtValoracion").css('back-ground', 'green');
                    break;

                case (totales => 61 && totales <= 85):
                    $("#txtValoracion").val("VALORACIÓN MODERADAMENTE ACEPTABLE");
                    $("#txtValoracion").css('back-ground', 'yellow');
                    break;

                case (totales <= 60):
                    $("#txtValoracion").val("VALORACIÓN CRITICO");
                    $("#txtValoracion").css('back-ground', 'red');
                    break;
            }
            $('.tbody').html(html);
            $('.tabGesCalifications').css("display", "block");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function getCalificationByID(calificationID) {
    $.ajax({
        async: true,
        type: 'GET',
        url: "/Evaluations/UpdateCalification",
        data: {
            id: calificationID
        },
        dataType: "json",
        contentType: "application/json;charset=UTF-8",
        success: function (result) {
            $.each(result, function (key, item) {
                var standard =
                    item.Ciclo + " " + item.Standard + " " + item.Item + " " + item.Name + " " + item.Valor;

                $("#txtValor").val(item.Valor);
                $("#txtStandard").val(standard);
                $("#txtCumple").val(item.Cumple);
                $("#txtNoCumple").val(item.NoCumple);
                $("#txtJustify").val(item.Justify);
                $("#txtNoJustify").val(item.NoJustify);
                $("#txtValoration").val(item.Valoration);
                $("#txtObservation").val(item.Observation);
                $("#txtCumple").focus();
                $("#txtCalificationID").val(item.ID);
            });
            $("#btnUpdCalification").show();
            $("#btnCanCalification").show();
            $(".tabAddCalifications").css("display", "block");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function DeleteCalification(id) {
    $.ajax({
        url: "/Incidentes/DeleteRecomendation/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        async: true,                                             // si es asincrónico o no
        success: function (result) {
            var text = "";
            text += "Esta seguro de querer borrar esta recomendación ? :\n\n";
            text += "Recomendación : " + result.data.Name + "\n";
            var respuesta = confirm(text);
            if (respuesta == true) {
                $.ajax({
                    url: "/Incidentes/DeleteRecomendation/" + id,
                    type: "POST",
                    contentType: "application/json;charset=UTF-8",
                    dataType: "json",
                    async: true,                                               // si es asincrónico o no
                    success: function (result) {
                        alert(result.mensaj);
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        alert(xhr.status);
                        alert(thrownError);
                    }
                });
            }
            ClearTextBox();
            ShowRecomendations();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function UpdateCalification() {
    // Actualiza una actualización, captura la evaluationID de id = txtEvaluationID
    $(".tabGesCalifications").css("display", "none");
    $(".tabAddCalifications").css("display", "none");

    var valoration = 0;
    if ($("#txtCumple").is(':checked') || $("#txtJustify").is(':checked')) {
        valoration = $("#txtValor").val();
    }
    var evaluationID = $("#txtEvaluationID").val();
    var calificationVM = {
        ID: $("#txtCalificationID").val(),
        Cumple: $("#txtCumple").val(),
        NoCumple: $("#txtNoCumple").val(),
        Justify: $("#txtJustify").val(),
        NoJustify: $("#txtNoJustify").val(),
        Valoration: valoration,
        Observation: $("#txtObservation").val()
    };
    $.ajax({
        type: "POST",
        url: "/Evaluations/UpdateCapacitacion",
        data: { model: calificationVM },
        dataType: "json",
        success: function (response) {
            alert(response.mensaj);
            ShowCalifications(evaluationID);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function CancelCalifications() {
    $(".tabGesCalifications").css("display", "none");
    $(".tabAddCalifications").css("display", "none");
    $("#txtCalifications").val("");
}
