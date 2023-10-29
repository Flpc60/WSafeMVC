// Agregar funcionlidad del lado del cliente,
// Implementar UI
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

                html += '<tr>';
                html += '<td>' + item.NivelDeficiencia + '</td>';
                html += '<td>' + item.NivelExposicion + '</td>';
                html += '<td>' + np + '</td>';
                html += '<td>' + interpretaP + '</td>';
                html += '<td>' + item.NivelConsecuencia + '</td>';
                html += '<td>' + nr + '</td>';
                html += '<td>' + interpretaNR + '</td>';
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
                html += '<td><a href="#" onclick="DeleteRole(' + item.ID + ', \'' + item.Name + '\')"> Borrar</a></td>';
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
            //$('#addAuthorize').focus();
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

function getAbsolutePath() {
    var loc = window.location;
    var pathName = loc.pathname.substring(0, loc.pathname.lastIndexOf('/') + 1);
    return loc.href.substring(0, loc.href.length - ((loc.pathname + loc.search + loc.hash).length - pathName.length));
}

function ShowMovimientos(phva) {
    // Mostrar todos los movimientos
    var Item = $("#txtStandardID").val();
    var periodo = $("#periodo").val();
    $.ajax({
        url: "/Movimientos/GetMovimientos",
        data: {
            ciclo: phva,
            item: Item,
            year: periodo
        },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            var html = '', url = '', fileName = '', ruta = '';
            $.each(response.data, function (key, item) {

                switch (item.Ciclo) {
                    case "P":
                        ruta = "1. PLANEAR/";
                        break;

                    case "H":
                        ruta = "2. HACER/";
                        break;
                    case "V":
                        ruta = "3. VERIFICAR/";
                        break;
                    case "A":
                        ruta = "4. ACTUAR/";
                        break;
                }

                url = window.location.origin + "/ORG" + item.OrganizationID + "/SG-SST/" + item.Year + "/" + ruta + item.Item + "/" + item.Document;
                fileName = "1" + item.Document;
                html += '<tr>';
                html += '<td>' + item.Ciclo + '</td>';
                html += '<td>' + item.Item + '</td>';
                html += '<td>' + item.Name + '</td>';
                html += '<td>' + item.Descripcion + '</td>';
                html += '<td>' + item.Type + '</td>';
                html += '<td><a href = "' + url + '" class="btn btn-info">Abrir</a></td>';
                html += '<td><a href = "' + url + '" download="' + fileName + '" class="btn btn-info">Descargar</a ></td>';
                html += '<td><a href = "#" onclick="DeleteMovimient(' + item.ID + ')" class="btn btn-info">Eliminar</a></td>';
                html += '<td><a href = "#" onclick="OpenEmail(' + item.ID + ')" class="btn btn-info">Enviar</a></td>';
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

validarCumplimiento = function (item) {
    var fecha = Date.now();
    var date = Date.parse(item);
    if (date < fecha) {
        alert("Fecha cumplimiento incorrecta !!");
        return false;
    };
}

validarFechaIni = function (item) {
    var fecha = Date.now();
    var date = Date.parse(item);
    if (date > fecha) {
        alert("Fecha incorrecta !!");
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

function DescargarFile(movimientID) {
    $(".tabGesMovimientos").css("display", "none");
    $.ajax({
        async: true,
        type: 'POST',
        url: "/Movimientos/Download",
        data: { id: movimientID },
        dataType: "json",
        success: function (response) {
            var url = response.url;
            var fileName = response.name;
            downLoadFile(url, fileName);
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
            $("#txtResponsable").val(result.Responsable);
            $("#txtEvaluationID").val(result.EvaluationID);
            $("#txtNormaID").val(result.NormaID);
            $("#txtFechaActivity").val(result.FechaActivity);
            $("#txtObservation").val(result.Observation);
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

function DeleteRole(id, role) {
    // Eliminar un rol
    var text = "";
    text += "Esta seguro de querer borrar el rol: " + role.toUpperCase() + " ?\n\n";
    var respuesta = confirm(text);
    if (respuesta == true) {
        $.ajax({
            url: "/Accounts/DeleteRole/" + id,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            async: true,
            success: function (response) {
                document.getElementById("txtLogin").innerHTML = response.mensaj;
                $(".tabMessage").css("display", "block");
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
            async: true,
            success: function (response) {
                document.getElementById("txtAuthorize").innerHTML = response.mensaj;
                $(".tabMessage").css("display", "block");
                $("#btnAddAuthorize").hide();
                $("#txtAuthorize").focus();
                ShowAuthorizations();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status);
                alert(thrownError);
            }
        });
    }
}

function validateAction() {
    var fields = [
        { id: "zona", message: "El contenido de la zona es obligatorio" },
        { id: "proceso", message: "El contenido del proceso es obligatorio" },
        { id: "activity", message: "El contenido de la actividad es obligatorio" },
        { id: "tarea", message: "El contenido de la tarea es obligatorio" },
        { id: "FechaSolicitud", message: "El contenido de la Fecha Solicitud es obligatorio" },
        { id: "categoriaAcc", message: "El contenido del tipo de acción es obligatorio" },
        { id: "idTrabajador", message: "El nombre del traajador es obligatorio" },
        { id: "idFuente", message: "El contenido de la fuente origen es obligatorio" },
        { id: "idDescrip", message: "El contenido de la descripción es obligatorio" },
        { id: "FechaCierre", message: "El contenido de la Fecha Cierre es obligatorio" },
        { id: "txtActionCategory", message: "El contenido del estado de la acción es obligatorio" },
    ];

    for (var i = 0; i < fields.length; i++) {
        var field = fields[i];
        var value = $("#" + field.id).val();

        if (value == "" || value == 0) {
            document.getElementById("txtInfoAct").innerHTML = field.message;
            $("#" + field.id).focus();
            return false;
        }
    }

    document.getElementById("txtInfoAct").innerHTML = "";
    return true;

}

// las siguientes funciones cran una nueva accion, plan acción y nuevo seguimiento
function AddAccion() {
    // Crea una nueva acción, para una nueva no conformidad
    // Retorna la accionID, y se la pasa a PlanAccion y SeguimientoAccion

    if (!validateAction()) {
        return false;
    }

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

    if (!validateAction()) {
        return false;
    }

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
        ActionCategory: $("#txtActionCategory").val(),
        UserID: $("#userID").val()
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

    // Define el rango de la empresa con base en los estándares mónimos
    var range = 3;
    if ($("#txtAgropecuaria").is(':checked')) {
        $("#txtAgropecuaria").val(true)
    }
    else {
        $("#txtAgropecuaria").val(false)
    }

    var char = ($("#txtClaseRiesgo").val()).trim();
    if (($("#txtNumero").val() <= 10)) {
        var input = "I,II,III";
        var pos = input.indexOf(char);
        if (pos >= 0) {
            range = 1;
        }
    }

    if (($("#txtNumero").val() > 10) && ($("#txtNumero").val() <= 50)) {
        var input = "I,II,III";
        var pos = input.indexOf(char);
        if (pos >= 0) {
            range = 2;
        }
    }

    if (($("#txtNumero").val() > 50)) {
            range = 3;
    }

    if (($("#txtNumero").val() <= 50)) {
        input = "IV,V";
        var pos = input.indexOf(char);
        if (pos >= 0) {
            range = 3;
        }
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
        TurnosOperativo: $("#txtOperativo").val(),
        Year: $("#txtYear").val(),
        Agropecuaria: $("#txtAgropecuaria").val(),
        ResponsableSGSST: $("#txtResponsable").val(),
        DocumentResponsable: $("#txtDocResponsable").val(),
        ResolucionLicencia: $("#resolucionLicencia").val(),
        FechaResolucionLicencia: $("#resolucionLicencia").val(),
        ResponsableLicencia: $("#txtLicenciaResponsable").val(),
        RenovacionLicencia: $("#renovacionLicencia").val(),
        FechaRenovacionLicencia: $("#renovacionLicencia").val(),
        RenovacionCurso: $("#renovacionCurso").val(),
        FechaRenovacionCurso: $("#renovacionCurso").val(),
        NivelEstudios: $("#idNivelEstudios").val(),
        MesesExperiencia: $("#txtMesesExperiencia").val(),
        Range: range,
        StandardEvaluation: $("#sdEvaluation").val(),
        StandardMatrixRisk: $("#sdMatrixRisk").val(),
        StandardActions: $("#sdActions").val(),
        StandardIncidents: $("#sdIncidents").val(),
        StandardSocioDemographic: $("#sdSociodemographic").val(),
        StandardUnsafeacts: $("#sdUnsafeacts").val(),
        ControlDate: $("#controlDate").val(),
        ClientID: $("#clientID").val()
    };

    $.ajax({
        type: "POST",
        url: "/Organizations/UpdateOrganization",
        data: { model: organizaVM },
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
    var fields = [
        { id: "txtNit", message: "Falta NIT de la organización" },
        { id: "txtRazon", message: "Falta Razón social de la organización" },
        { id: "txtDireccion", message: "Falta Dirección de la organización" },
        { id: "txtMunicip", message: "Falta Municipio de la organización" },
        { id: "txtDepartment", message: "Falta Departamento de la organización" },
        { id: "txtTelefono", message: "Falta teléfonos de la organización" },
        { id: "txtArl", message: "Falta ARL de la organización" },
        { id: "txtClaseRiesgo", message: "Falta Clase riesgo de la organización" },
        { id: "txtDocument", message: "Falta documento del representante legal de la organización" },
        { id: "txtName", message: "Falta nombre del representante legal de la organización" },
        { id: "txtEconomic", message: "Falta actvidad económica de la organización" },
        { id: "txtNumero", message: "Falta número de trabajadores de la organización" },
        { id: "txtPolitica", message: "Falta política de la organización" },
        { id: "txtProducts", message: "Falta clase productos de la organización" },
        { id: "txtMision", message: "Falta Misión de la organización" },
        { id: "txtVision", message: "Falta Visión de la organización" },
        { id: "txtObjetivos", message: "Falta Objetivos de la organización" },
        { id: "txtAdministrativo", message: "Falta definir turnos administrativos de la organización" },
        { id: "txtOperativo", message: "Falta definir turnos operativos de la organización" },
        { id: "txtYear", message: "Falta definir periodo de la organización" },
        { id: "txtResponsable", message: "Falta definir responsable del SG-SST de la organización" },
    ];

    for (var i = 0; i < fields.length; i++) {
        var field = fields[i];
        var value = $("#" + field.id).val();

        if (value == "") {
            document.getElementById("txtInfoBasic").innerHTML = field.message;
            $("#" + field.id).focus();
            return false;
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
        success: function (response) {
            if (response.data != false) {
                $("#txtPlanAccionID").val(response.data.ID);
            }
            alert(response.mensaj);
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
    //TODO
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
        ActionCategory: $("#txtPlanCategory").val(),
        Responsable: $("#txtResponsable").val(),
        EvaluationID: $("#txtEvaluationID").val(),
        NormaID: $("#txtNormaID").val(),
        FechaActivity: $("#idFechaIni").val(),
        Observation: $("#txtObservation").val()
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

function validateRisk() {
    var fields = [
        { id: "zona", message: "El contenido de la zona es obligatorio" },
        { id: "proceso", message: "El contenido del proceso es obligatorio" },
        { id: "activity", message: "El contenido de la actividad es obligatorio" },
        { id: "tarea", message: "El contenido de la tarea es obligatorio" },
        { id: "txtRutinaria", message: "El contenido de rutinaria es obligatorio" },
        { id: "categoria", message: "El contenido de la categoria es obligatorio" },
        { id: "peligro", message: "El contenido del peligro es obligatorio" },
        { id: "txtEfectos", message: "El contenido de efectos psibles es obligatorio" },
        { id: "exposicion", message: "El contenido de exposición es obligatorio" },
        { id: "consecuencia", message: "El contenido de la consecuencia es obligatorio" },
        { id: "txtExpuestos", message: "El contenido de número de expuestos es obligatorio" },
        { id: "txtDanger", message: "El contenido de categoría daño es obligatorio" }
    ];

    for (var i = 0; i < fields.length; i++) {
        var field = fields[i];
        var value = $("#" + field.id).val();

        if (value == "" || value == 0) {
            document.getElementById("txtInfoRisk").innerHTML = field.message;
            $("#" + field.id).focus();
            return false;
        }
    }

    document.getElementById("txtInfoRisk").innerHTML = "";
    return true;

}

function AddNewRiesgo() {
    // Ingresar nuevo risk
    if (!validateRisk()) {
        return false;
    }

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
        DangerCategory: $("#txtDanger").val(),
        FuenteControls: $("#fuenteControls").val(),
        MedioControls: $("#medioControls").val(),
        IndividuoControls: $("#individuoControls").val()
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
    // Actualizar risk
    if (!validateRisk()) {
        return false;
    }

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
        DangerCategory: $("#txtDanger").val(),
        FuenteControls: $("#fuenteControls").val(),
        MedioControls: $("#medioControls").val(),
        IndividuoControls: $("#individuoControls").val(),
        UserID: $("#userID").val()
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
                ClearTextBox();
            }
            alert(result.mensaj);
            mostrarInterven();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
    //AddPlan(1); // adicionar plan acción
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
        $("#txtRoleID").focus();
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
        $("#txtZone").focus();
    });
    $("#addProcess").click(function () {
        $(".tabAddProcess").css("display", "block");
        $(".tabCerrar").css("display", "none");
        $("#btnAddProcess").show();
        $("#btnCanProcess").show();
        $("#txtProcess").focus();
    });
    $("#addActivity").click(function () {
        $(".tabAddActivitys").css("display", "block");
        $(".tabCerrar").css("display", "none");
        $("#btnAddActivity").show();
        $("#btnCanActivity").show();
        $("#txtActivity").focus();
    });
    $("#addTask").click(function () {
        $(".tabAddTasks").css("display", "block");
        $(".tabCerrar").css("display", "none");
        $("#btnAddTask").show();
        $("#btnCanTask").show();
        $("#txtDescripcion").focus();
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
        TrabajadorID: $("#txtInformante").val(),
        UserID: $("#userID").val()
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
            document.getElementById("txtLogin").innerHTML = response.mensaj;
            $(".tabMessage").css("display", "block");
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
    $(".tabMessage").css("display", "none");
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
            document.getElementById("txtAuthorize").innerHTML = response.mensaj;
            $(".tabMessage").css("display", "block");
            //$("#btnAddAuthorize").hide();
            ShowAuthorizations();
            $("#txtAuthorize").show();
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
        RoleID: "0",
        OrganizationID: "0",
        ClientID: $("#clientID").val()
    };
    $.ajax({
        type: "POST",
        url: "/Accounts/CreateUser",
        data: { model: user },
        dataType: "json",
        success: function (response) {
            document.getElementById("txtLogin").innerHTML = response.mensaj;
            $(".tabMessage").css("display", "block");
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
        RoleID: "0",
        OrganizationID: $("#orgID").val(),
        ClientID: $("#clientID").val()
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
                if (response.result != null) {
                    var html = '';
                    $.each(response.result, function (index, item) {
                        html += '<option value = "' + item.Value + '">' + item.Text + '</option>';
                    });
                    $("#orgID").html(html);
                    $(".tabOrganization").css("display", "block");
                };

                document.getElementById("txtLogin").innerHTML = response.mensaj;
                $(".tabMessage").css("display", "block");
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function authorizeUser() {
    // Crea nueva autorización usuario
    $.ajax({
        type: "POST",
        url: "/Accounts/CreateAuthorization",
        data: {
            userID: $("#userID").val(),
            organizationID: $("#organizationID").val()
        },
        dataType: "json",
        success: function (response) {
            alert(response.mensaj);
            window.location = response.url;
            $(".tabAuthorize").css("display", "none");
            $("#authorize").show();
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
        success: function (data) {
            var arrayData1 = [];
            for (var i = 0; i < data.length; i++) {
                arrayData1.push(data[i].Resultado);
            }
            const ctx = document.getElementById("ValueDangers").getContext('2d');
            const chart = new Chart(ctx, {
                type: 'pie',
                data: {
                    labels: ["MUY ALTO", "ALTO", "MEDIO", "BAJO"],
                    datasets: [
                        {
                            label: "CATEGORÍAS",
                            backgroundColor: [
                                'rgb(255, 99, 132)',
                                'rgb(75, 192, 192)',
                                'rgb(255, 205, 86)',
                                'rgb(201, 203, 207)',
                            ],
                            data: arrayData1
                        },
                    ]
                },
                options: {
                    responsive: true,
                    scales: {
                        x: {
                            display: true,
                            title: {
                                display: true
                            }
                        },
                        y: {
                            display: true,
                            title: {
                                display: true
                            }
                        }
                    },
                    title: {
                        display: true,
                        text: 'VALORACION PELIGROS'
                    }
                }
            });
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
        success: function (data) {
            var arrayLabel = [];
            var arrayData1 = [];
            for (var i = 0; i < data.length; i++) {
                arrayLabel.push(data[i].MesAnn);
                arrayData1.push(data[i].Resultado);
            }
            const ctx = document.getElementById("ClassDangers").getContext('2d');
            const chart = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: arrayLabel,
                    datasets: [
                        {
                            label: "CATEGORÍAS",
                            data: arrayData1,
                            backgroundColor: 'rgba(54, 162, 235, 0.2)',
                            borderColor: 'rgba(54, 162, 235)',
                            borderWidth: 1
                        },
                    ]
                },
                options: {
                    responsive: true,
                    scales: {
                        x: {
                            display: true,
                            title: {
                                display: true
                            }
                        },
                        y: [{
                            display: true,
                            title: {
                                display: true
                            },
                            min: 0
                        }]
                    },
                    title: {
                        display: true,
                        text: 'CLASIFICACIÓN PELIGROS'
                    }
                }
            });
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
        success: function (data) {
            var arrayData1 = [];
            for (var i = 0; i < data.length; i++) {
                arrayData1.push(data[i].Resultado);
            }
            const ctx = document.getElementById("CommonActivitys").getContext('2d');
            const chart = new Chart(ctx, {
                type: 'pie',
                data: {
                    labels: ["RUTINARIA", "NO RUTINARIA"],
                    datasets: [
                        {
                            label: "CATEGORÍAS",
                            backgroundColor: [
                                'rgb(75, 192, 192)',
                                'rgb(255, 205, 86)',
                            ],
                            data: arrayData1
                        },
                    ]
                },
                options: {
                    responsive: true,
                    scales: {
                        x: {
                            display: true,
                            title: {
                                display: true
                            }
                        },
                        y: {
                            display: true,
                            title: {
                                display: true
                            }
                        }
                    },
                    title: {
                        display: true,
                        text: 'ACTIVIDADES RUTINARIAS'
                    }
                }
            });
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
        success: function (data) {
            var arrayData1 = [];
            for (var i = 0; i < data.length; i++) {
                arrayData1.push(data[i].Resultado);
            }
            const ctx = document.getElementById("EfectosPosibles").getContext('2d');
            const chart = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: ["EXTREMO","LEVE","MODERADO","A PROPIEDAD","","FALLAS A PROCESOS","PERDIDAS ECONÓMICAS"],
                    datasets: [
                        {
                            label: "CATEGORÍAS DAÑOS",
                            backgroundColor: 'rgb(255, 99, 132)',
                            data: arrayData1
                        },
                    ]
                },
                options: {
                    responsive: true,
                    scales: {
                        x: {
                            display: true,
                            title: {
                                display: true
                            }
                        },
                        y: {
                            display: true,
                            title: {
                                display: true
                            }
                        }
                    },
                    title: {
                        display: true,
                        text: 'EFECTOS POSIBLES'
                    }
                }
            });
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
    $('#interpretaNP').css({ "backgroundColor": colorStyle, "font-size": "100%", "color":"white"});
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
    $('#interpretaNR').css({ "backgroundColor": colorStyle, "font-size": "100%", "color": "white"});
    $('#txtAceptabilidad').css({ "backgroundColor": colorStyle, "font-size": "100%", "color": "white"});
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
            document.getElementById("numEfectives").innerHTML = "Acciones Efectivas : " + response.numEfectives;
            document.getElementById("noConformance").style.backgroundColor = "gray";
            document.getElementById("numActions").style.backgroundColor = "gray";
            document.getElementById("numCorrective").style.backgroundColor = "gray";
            document.getElementById("numEfectives").style.backgroundColor = "gray";
            $('#noConformance').focus();
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
        success: function (data) {
            var arrayLabel = [];
            var arrayData1 = [];
            for (var i = 0; i < data.length; i++) {
                arrayLabel.push(data[i].MesAnn);
                arrayData1.push(data[i].Resultado);
            }
            const ctx = document.getElementById("NoConformance").getContext('2d');
            const chart = new Chart(ctx, {
                type: 'line',
                data: {
                    labels: arrayLabel,
                    datasets: [
                        {
                            label: "PERIODOS",
                            data: arrayData1,
                            fill: false,
                            borderColor: 'rgb(75, 192, 192)',
                            tension: 0.1,
                        },
                    ]
                },
                options: {
                    responsive: true,
                    scales: {
                        x: {
                            display: true,
                            title: {
                                display: true
                            }
                        },
                        y: {
                            display: true,
                            title: {
                                display: true
                            }
                        }
                    },
                    title: {
                        display: true,
                        text: 'NO CONFORMIDADES'
                    }
                }
            });
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
        success: function (data) {
            var arrayLabel = [];
            var arrayData1 = [];
            for (var i = 0; i < data.length; i++) {
                arrayLabel.push(data[i].MesAnn);
                arrayData1.push(data[i].Resultado);
            }
            const ctx = document.getElementById("NumActions").getContext('2d');
            const chart = new Chart(ctx, {
                type: 'doughnut',
                data: {
                    labels: arrayLabel,
                    datasets: [
                        {
                            label: "CATEGORIAS",
                            data: arrayData1,
                            backgroundColor: [
                                '#F0410E',
                                '#E9F00E',
                                '#56F00E',
                            ],
                        },
                    ]
                },
                options: {
                    responsive: true,
                    scales: {
                        x: {
                            display: true,
                            title: {
                                display: true
                            }
                        },
                        y: {
                            display: true,
                            title: {
                                display: true
                            }
                        }
                    },
                    title: {
                        display: true,
                        text: 'ESTADO ACCIONES'
                    }
                }
            });
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
        success: function (data) {
            var arrayLabel = [];
            var arrayData1 = [];
            for (var i = 0; i < data.length; i++) {
                arrayLabel.push(data[i].MesAnn);
                arrayData1.push(data[i].Resultado);
            }
            const ctx = document.getElementById("NumCorrective").getContext('2d');
            const chart = new Chart(ctx, {
                type: 'doughnut',
                data: {
                    labels: arrayLabel,
                    datasets: [
                        {
                            label: "CATEGORIAS",
                            data: arrayData1,
                            backgroundColor: [
                                '#F0410E',
                                '#E9F00E',
                                '#56F00E',
                            ],
                        },
                    ]
                },
                options: {
                    responsive: true,
                    scales: {
                        x: {
                            display: true,
                            title: {
                                display: true
                            }
                        },
                        y: {
                            display: true,
                            title: {
                                display: true
                            }
                        }
                    },
                    title: {
                        display: true,
                        text: 'TIPO ACCIONES'
                    }
                }
            });
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
            var barrier = '', name = '';
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

function OpenEmail(movimientID) {
    $("#txtMovimientID").val(movimientID);
    $(".tabSendEmail").css("display", "block");
}

function SendEmail() {
    $(".tabGesMovimientos").css("display", "none");
    var movimientID = $("#txtMovimientID").val();
    var asunto = $("#txtAsunto").val();
    $("#txtSendAsunto").val(asunto);
    $.ajax({
        async: true,
        type: 'POST',
        url: "/Movimientos/SendMail",
        data:
        {
            id: movimientID,
            destino1: $("#txtDestino1").val(),
            destino2: $("#txtDestino2").val(),
            destino3: $("#txtDestino3").val(),
            sendAsunto: $("#txtAsunto").val(),
            sendMessage: $("#txtMessage").val(),
        },
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
    $(".tabSendEmail").css("display", "none");
}

function AddEvaluation() {
    // Crea una nueva evaluación
    //$('.tabGesCalifications').css("display", "none");
    $(".tabGesCiclo").css("display", "block");
    var minimos = true;
    if ($("#txtEstandares").is(':checked')) {
        minimos = false;
    }

    $.ajax({
        type: "POST",
        url: "/Evaluations/CreateEvaluation",
        data: {indicador: minimos},
        dataType: "json",
        success: function (response) {
            var evaluationID = response.data;
            if (response.data != false) {
                $("#txtEvaluationID").val(evaluationID);
                $("#btnEvaluation").hide();
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function AddPlanActivity() {
    // Crea un nuevo plan de actividad
    $('.tabAddPlanAcc').css("display", "none");
    //evaluationID = $("#txtEvaluationID").val();
    var normaID = $("#txtNormaID").val();
    var financieros = false;
    var administrativos = false;
    var tecnicos = false;
    var humanos = false;
    if ($("#txtFinancieros").is(':checked')) { financieros = true; }
    if ($("#txtAdministrativos").is(':checked')) { administrativos = true; }
    if ($("#txtTecnicos").is(':checked')) { tecnicos = true; }
    if ($("#txtHumanos").is(':checked')) { humanos = true; }
    var planActivity = {
        ID: "0",
        EvaluationID: evaluationID,
        NormaID: normaID,
        TrabajadorID: $("#txtResponsable").val(),
        FechaFinal: $("#txtFechaFinal").val(),
        Activity: $("#txtActivity").val(),
        Financieros: financieros,
        Administrativos: administrativos,
        Tecnicos: tecnicos,
        Humanos: humanos,
        ActionCategory: $("#txtActionCategory").val(),
        Observation: $("#txtObservation").val(),
        Fundamentos: $("#txtFundamentos").val()
    };
    $.ajax({
        type: "POST",
        url: "/Evaluations/CreatePlanActivity",
        data: { model: planActivity },
        dataType: "json",
        success: function (response) {
            $("#btnUpdCalification").focus();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function ShowCalifications(evaluationID, phva) {
    // Mostrar todos las calificaciones
    //evaluationID = $("#txtEvaluationID").val();
    $.ajax({
        url: "/Evaluations/GetCalifications",
        data: {
            id: evaluationID,
            ciclo: phva
        },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,
        success: function (result) {
            var html = '', cumpleSi = '', cumpleNo = '', justifySi = '', justifyNo = '';
            var totales = 0, cumple = 0, noCumple = 0, noAplica = 0;
            $.each(result, function (key, item) {
                cumpleSi = '', cumpleNo = '', justifySi = '', justifyNo = '';
                totales += item.Valoration;

                if (item.Cumple == true) {
                    cumple++;
                    cumpleSi = "X";
                }
                if (item.NoCumple == true) {
                    noCumple++;
                    cumpleNo = "X";
                }
                if (item.Justify == true) {
                    noAplica++;
                    justifySi = "X";
                }
                if (item.NoJustify == true) {
                    noAplica++;
                    justifyNo = "X";
                }

                html += '<tr class="table-primary">';
                html += '<td>' + item.Item + ' ' + item.Name + '</td>';
                html += '<td>' + item.Verification + '</td>';
                html += '<td>' + item.Valor + '</td>';
                html += '<td style="text-align:center;font-size:large">' + cumpleSi + '</td>';
                html += '<td style="text-align:center;font-size:large">' + cumpleNo + '</td>';
                html += '<td style="text-align:center;font-size:large">' + justifySi + '</td>';
                html += '<td style="text-align:center;font-size:large">' + justifyNo + '</td>';
                html += '<td style="text-align:center;font-size:large">' + item.Valoration + '</td>';
                html += '<td><a href="#" onclick="return getCalificationByID(' + item.ID + ')">Calificar</a></td>';
                html += '<hr />';
                html += '</tr>';
            });

            document.getElementById("txtTotales").innerHTML = "TOTALES : " + totales;
            document.getElementById("txtCumple").innerHTML = "CUMPLE : " + cumple;
            document.getElementById("txtNoCumple").innerHTML = "NO CUMPLE : " + noCumple;
            document.getElementById("txtNoAplica").innerHTML = "NO APLICA : " + noAplica;

            switch (true)
            {
                case (totales > 85) :
                    document.getElementById("txtValoracion").innerHTML = "VALORACIÓN ACEPTABLE";
                    document.getElementById("txtValoracion").style.backgroundColor = "green";
                    break;

                case (totales => 61 && totales <= 85):
                    document.getElementById("txtValoracion").innerHTML = "VALORACIÓN MODERADAMENTE ACEPTABLE";
                    document.getElementById("txtValoracion").style.backgroundColor = "yellow";
                    break;

                case (totales <= 60):
                    document.getElementById("txtValoracion").innerHTML = "VALORACIÓN CRÍTICO";
                    document.getElementById("txtValoracion").style.backgroundColor = "red";
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

function ShowPlanActivities(evaluationID) {
    // Mostrar todos las actividades
    $.ajax({
        url: "/Evaluations/GetPlanActivities",
        data: { id: evaluationID },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            var html = '';
            $.each(response, function (key, item) {
                recursos = '';
                if (item.Financieros) {
                    recursos += 'Financieros, ';
                }

                if (item.Administrativos) {
                    recursos += 'Administrativos, ';
                }

                if (item.Tecnicos) {
                    recursos += 'Técnicos, ';
                }

                if (item.Humanos) {
                    recursos += 'Humanos';
                }

                html += '<tr>';
                html += '<td>' + item.Ciclo + '</td>';
                html += '<td>' + item.Item + ' ' + item.Name + '</td>';
                html += '<td>' + item.Observation + '</td>';
                html += '<td>' + item.Activity + '</td>';
                html += '<td>' + item.Responsable + '</td>';
                html += '<td>' + item.FechaCumplimiento + '</td>';
                html += '<td>' + recursos + '</td>';
                html += '<td>' + item.Fundamentos + '</td>';
                html += '<td>' + item.TxtActionCategory + '</td>';
                html += '<td><a href="#" onclick="return getPlanActivityByID(' + item.ID + ')">Editar</a></td>';
                html += '<hr />';
                html += '</tr>';
            });
            $('.tPlanAcc').html(html);
            $('.tabGesPlanAcc').css("display", "block");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function getCalificationByID(calificationID) {
    $("#txtCalificationID").val(calificationID);
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
                var ciclo = " CICLO : "+ item.Ciclo;
                var standard = " ESTÁNDAR : " + item.Standard;
                var itemStandard = " ITEM ESTÁNDAR : " + item.Item + " " + item.Name;
                var valorItem = " VALOR : " + item.Valor;
                valoration = item.Valor;
                Item = item.Item;

                $("#txtCumple").prop('checked',false)
                $("#txtNoCumple").prop('checked',false)
                $("#txtJustify").prop('checked',false)
                $("#txtNoJustify").prop('checked',false)

                if (item.Cumple == true) {
                    $("#txtCumple").prop('checked',true)
                }
                if (item.NoCumple == true) {
                    $("#txtNoCumple").prop('checked',true)
                }
                if (item.Justify == true) {
                    $("#txtJustify").prop('checked',true)
                }
                if (item.NoJustify == true) {
                    $("#txtNoJustify").prop('checked',true)
                }

                $("#txtNormaID").val(item.NormaID);
                $("#txtValor").val(item.Valor);
                $("#txtCumple").val(item.Cumple);
                $("#txtNoCumple").val(item.NoCumple);
                $("#txtJustify").val(item.Justify);
                $("#txtNoJustify").val(item.NoJustify);
                $("#txtValoration").val(item.Valoration);
                $("#txtObservation").val(item.Observation);
                $("#txtEvaluationID").val(item.EvaluationID);
                $("#txtCumple").focus();
                document.getElementById("txtCiclo").innerHTML = ciclo;
                document.getElementById("txtItem").innerHTML = itemStandard;
                document.getElementById("itemStandard").innerHTML = itemStandard;
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

function getPlanActivityByID(planActivityID) {
    $.ajax({
        async: true,
        type: 'GET',
        url: "/Evaluations/UpdatePlanActivity",
        data: {
            id: planActivityID
        },
        dataType: "json",
        contentType: "application/json;charset=UTF-8",
        success: function (result) {
            var standard = " ESTÁNDAR : " + result.Standard;
            var itemStandard = " ITEM ESTÁNDAR : " + result.Item + " " + result.Name;
            $("#txtFinancieros").prop('checked', false)
            $("#txtAdministrativos").prop('checked', false)
            $("#txtTecnicos").prop('checked', false)
            $("#txtHumanos").prop('checked', false)
            if (result.Financieros) { $("#txtFinancieros").prop('checked', true) };
            if (result.Administrativos) { $("#txtAdministrativos").prop('checked', true) };
            if (result.Tecnicos) { $("#txtTecnicos").prop('checked', true) };
            if (result.Humanos) { $("#txtHumanos").prop('checked', true) };

            $("#txtActivity").val(result.Activity);
            $("#txtResponsable").val(result.TrabajadorID);
            $("#txtFinancieros").val(result.Financieros);
            $("#txtAdministrativos").val(result.Administrativos);
            $("#txtTecnicos").val(result.Tecnicos);
            $("#txtHumanos").val(result.Humanos);
            $("#txtFechaFinal").val(result.FechaCumplimiento);
            $("#txtFundamentos").val(result.Fundamentos);
            $("#txtActionCategory").val(result.ActionCategory);
            $("#txtObservation").val(result.Observation);
            $("#txtEvaluationID").val(result.EvaluationID);
            $("#txtPlanActivityID").val(result.ID);
            //document.getElementById("txtStandard").innerHTML = standard;
            document.getElementById("txtItem").innerHTML = itemStandard;
            $("#btnAddPlanActivity").hide();
            $("#btnUpdPlanActivity").show();
            $("#btnCanPlanActivity").show();
            $(".tabAddPlanAcc").css("display", "block");
            $("#txtObservation").focus();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function DeletePlanActivity(id) {
    $.ajax({
        url: "/Evaluations/DeletePlanActivity/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        async: true,                                             // si es asincrónico o no
        success: function (result) {
            var text = "";
            var evaluationID = result.EvaluationID;
            text += "Esta seguro de querer borrar esta actividad ? :\n\n";
            text += "Observación : " + result.Observation + "\n";
            text += "Actividad : " + result.Activity + "\n";
            text += "Responsable : " + result.Responsable + "\n";
            var respuesta = confirm(text);
            if (respuesta == true) {
                $.ajax({
                    url: "/Evaluations/DeletePlanActivity/" + id,
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
            ShowPlanActivities(evaluationID);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
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

function UpdatePlanActivity() {
    // Actualiza una actividad, captura la evaluationID de id = txtEvaluationID
    $(".tabGesPlanAcc").css("display", "none");
    $(".tabAddCalifications").css("display", "none");
    $(".tabAddPlanAcc").css("display", "none");
    var planActivityID = $("#txtPlanActivityID").val();
    var normaID = $("#txtNormaID").val();
    var financieros = false;
    var administrativos = false;
    var tecnicos = false;
    var humanos = false;
    if ($("#txtFinancieros").is(':checked')) { financieros = true; }
    if ($("#txtAdministrativos").is(':checked')) { administrativos = true; }
    if ($("#txtTecnicos").is(':checked')) { tecnicos = true; }
    if ($("#txtHumanos").is(':checked')) { humanos = true; }

    var planActivityVM =
    {
        ID: planActivityID,
        EvaluationID: evaluationID,
        NormaID: normaID,
        TrabajadorID: $("#txtResponsable").val(),
        FechaFinal: $("#txtFechaFinal").val(),
        Activity: $("#txtActivity").val(),
        Financieros: financieros,
        Administrativos: administrativos,
        Tecnicos: tecnicos,
        Humanos: humanos,
        ActionCategory: $("#txtActionCategory").val(),
        Observation: $("#txtObservation").val(),
        Fundamentos: $("#txtFundamentos").val()
    };

    $.ajax({
        type: "POST",
        url: "/Evaluations/UpdatePlanActivity",
        data: { model: planActivityVM },
        dataType: "json",
        success: function (response) {
            evaluationID = response.data;
            $("#txtEvaluationID").val(evaluationID);
            ShowPlanActivity(evaluationID);
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
    $(".tabAddPlanAcc").css("display", "none");
    //evaluationID = $("#txtEvaluationID").val();
    valoration = 0;
    var Id = $("#txtCalificationID").val();
    normaID = $("#txtNormaID").val();

    var Cumple = false;
    var NoCumple = false;
    var Justify = false;
    var NoJustify = false;
    var Observation = $("#txtObservation").val();

    if ($("#txtCumple").is(':checked') || $("#txtJustify").is(':checked')) {
        valoration = $("#txtValor").val() * 100;
    }
    if ($("#txtCumple").is(':checked')) {
        Cumple = true;
        NoCumple = false;
    }

    if ($("#txtNoCumple").is(':checked')) {
        Cumple = false;
        NoCumple = true;
    }

    if ($("#txtJustify").is(':checked')) {
        Justify = true;
        NoJustify = false;
    }

    if ($("#txtNoJustify").is(':checked')) {
        Justify = false;
        NoJustify = true;
    }
    var calificationVM =
    {
        ID: Id,
        EvaluationID: evaluationID,
        NormaID: normaID,
        Ciclo: "",
        Item: "",
        Standard: "",
        Valor: "",
        Cumple: Cumple,
        NoCumple: NoCumple,
        Justify: Justify,
        NoJustify: NoJustify,
        Valoration: valoration,
        Observation: Observation
    };

    $.ajax({
        type: "POST",
        url: "/Evaluations/EditCalification",
        data: { model: calificationVM },
        dataType: "json",
        success: function (response) {
            evaluationID = response.data;
            $("#txtEvaluationID").val(evaluationID);
            $("#txtCumple").val(false);
            $("#txtNoCumple").val(false);
            $("#txtJustify").val(false);
            $("#txtNoJustify").val(false);
            $("#txtValoration").val("");
            $("#txtObservation").val("");
            $("#txtActivity").val("");
            ShowCalifications(evaluationID, ciclo);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function CancelCalification() {
    $(".tabGesPlanAcc").css("display", "none");
    $(".tabAddPlanAcc").css("display", "none");
    $("#txtCalifications").val("");
}

function CancelPlanActivity() {
    //$(".tabGesCalifications").css("display", "none");
    $(".tabAddPlanAcc").css("display", "none");
    $("#txtResponsable").val("");
    $("#txtFechaFinal").val("");
    $("#txtActivity").val("");
    $("#txtRecursos").val("");
    $("#txtActionCategory").val("");
    $("#txtObservation").val("");
    $("#txtFundamentos").val("");
}

function GestorEvaluations() {
    //Activa ventanas para gestionar calificación estándares
    $("#btnShowPlanActivitys").click(function () {
        ResetTab();
        $(".tabGesPlanAcc").css("display", "block");
        $(".tabCerrar").css("display", "none");
        $(".tabGesCalifications").css("display", "none");
        $(".tabGesEvaluation").css("display", "none");
        evaluationID = $("#txtEvaluationID").val();
        ShowPlanActivities(evaluationID);
    });

    $("#btnShowPlanActivitys").dblclick(function () {
        $(".tabGesPlanAcc").css("display", "none");
        ResetTab();
    });

    $("#btnPlanActivity").click(function () {
        ResetTab();
        $("#btnPlanActivity").focus();
        $(".tabAddPlanAcc").css("display", "block");
        $("#txtActivity").show();
        $(".tabCerrar").css("display", "none");
        $("#btnAddPlanActivity").show();
        $("#btnUpdPlanActivity").hide();
        $("#txtResponsable").val("");
        $("#txtFechaFinal").val("");
        $("#txtActivity").val("");
        $("#txtRecursos").val("");
        $("#txtActionCategory").val("");
        $("#txtObservation").val("");
        $("#txtFundamentos").val("");
    });

    $("#txtNoCumple").click(function () {
        ResetTab();
        $("#btnPlanActivity").show();
        $(".tabAddPlanAcc").css("display", "block");
        $("#txtObservation").focus();
        $(".tabCerrar").css("display", "none");
        $("#btnAddPlanActivity").show();
        $("#btnUpdPlanActivity").hide();
        $("#txtResponsable").val("");
        $("#txtFechaFinal").val("");
        $("#txtActivity").val("");
        $("#txtRecursos").val("");
        $("#txtActionCategory").val("");
        $("#txtObservation").val("");
        $("#txtFundamentos").val("");
    });

    $("#addPlanAcc").click(function () {
        $(".tabAddPlanAcc").css("display", "block");
        $(".tabCerrar").css("display", "none");
        $("#btnAddPlanActivity").show();
        $("#btnUpdPlanActivity").hide();
        $("#txtResponsable").val("");
        $("#txtFechaFinal").val("");
        $("#txtActivity").val("");
        $("#txtRecursos").val("");
        $("#txtActionCategory").val("");
        $("#txtObservation").val("");
        $("#txtFundamentos").val("");
    });

    $("#planear").click(function () {
        ResetTab();
        $("#planear").focus();
        $(".tabCerrar").css("display", "none");
        ciclo = "P";
        evaluationID = $("#txtEvaluationID").val();
        ShowCalifications(evaluationID, ciclo);
    });
    $("#planear").dblclick(function () {
        ResetTab();
    });

    $("#hacer").click(function () {
        ResetTab();
        $(".tabCerrar").css("display", "none");
        $("#hacer").show();
        ciclo = "H";
        evaluationID = $("#txtEvaluationID").val();
        ShowCalifications(evaluationID, ciclo);
    });
    $("#hacer").dblclick(function () {
        ResetTab();
    });

    $("#verificar").click(function () {
        ResetTab();
        $(".tabCerrar").css("display", "none");
        $("#verificar").focus();
        ciclo = "V";
        evaluationID = $("#txtEvaluationID").val();
        ShowCalifications(evaluationID, ciclo);
    });
    $("#verificar").dblclick(function () {
        ResetTab();
    });

    $("#actuar").click(function () {
        ResetTab();
        $(".tabCerrar").css("display", "none");
        $("#actuar").focus();
        ciclo = "A";
        evaluationID = $("#txtEvaluationID").val();
        ShowCalifications(evaluationID, ciclo);
    });
    $("#actuar").dblclick(function () {
        ResetTab();
    });
}

function UpdateEvaluation() {

    // Actualiza una evaluación, captura la evaluationID de id = txtEvaluationID
    $(".tabGesCalifications").css("display", "none");
    $(".tabAddCalifications").css("display", "none");
    $(".tabAddPlanAcc").css("display", "none");
    $(".tabGesPlanAcc").css("display", "none");
    evaluationID = $("#txtEvaluationID").val();
    $.ajax({
        type: "GET",
        url: "/Evaluations/UpdateEvaluation",
        data: { id: evaluationID },
        dataType: "json",
        success: function (response) {
            var html = '', puntaje = '';
            var totales = response.totales, cumple = 0, noCumple = 0, noAplica = 0;
            $.each(response.data, function (key, item) {
                puntaje = '';
                if (item.Cumple) {
                    cumple++;
                    puntaje = "Cumple Totalmente";
                }
                if (item.NoCumple) {
                    noCumple++;
                    puntaje = "No Cumple";
                }
                if (item.Justify) {
                    noAplica++;
                    puntaje = "Justifica";
                }
                if (item.NoJustify) {
                    noAplica++;
                    puntaje = "No Justifica";
                }

                html += '<tr>';
                html += '<td>' + item.Ciclo + '</td>';
                html += '<td>' + item.Standard + '</td>';
                html += '<td>' + item.Item + ' ' + item.Name + '</td>';
                html += '<td>' + item.Valor + '</td>';
                html += '<td>' + puntaje + '</td>';
                html += '<td>' + item.Valoration + '</td>';
                html += '<hr />';
                html += '</tr>';
            });

            document.getElementById("txtTotales").innerHTML = "TOTALES : " + totales;
            document.getElementById("txtCumple").innerHTML = "CUMPLE : " + cumple;
            document.getElementById("txtNoCumple").innerHTML = "NO CUMPLE : " + noCumple;
            document.getElementById("txtNoAplica").innerHTML = "NO APLICA : " + noAplica;

            switch (true) {
                case (totales > 85):
                    document.getElementById("txtValoracion").innerHTML = "VALORACIÓN ACEPTABLE";
                    document.getElementById("txtValoracion").style.backgroundColor = "green";
                    break;

                case (totales >= 61 && totales <= 85):
                    document.getElementById("txtValoracion").innerHTML = "VALORACIÓN MODERADAMENTE ACEPTABLE";
                    document.getElementById("txtValoracion").style.backgroundColor = "yellow";
                    break;

                case (totales <= 60):
                    document.getElementById("txtValoracion").innerHTML = "VALORACIÓN CRÍTICO";
                    document.getElementById("txtValoracion").style.backgroundColor = "red";
                    break;
            }

            $('.tcuerpo').html(html);
            $('.tabGesEvaluation').css("display", "block");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function downLoadEvaluation() {
    $(".tabGesMovimientos").css("display", "none");
    $.ajax({
        async: true,
        type: 'GET',
        url: "/Evaluations/GeneratePDF",
        data: { id: evaluationID },
        dataType: "json",
        success: function (result) {
            //ShowEvaluation();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    $(".tabAddMovimientos").css("display", "none");
    $("#btnAddMovimient").hide();
    $("#btnCanMovimient").show();
}

function DeleteEvaluation(ID) {
    var id = ID;
    $.ajax({
        url: "/Evaluations/DeleteEvaluation/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        async: true,                                               // si es asincrónico o no
        success: function (result) {
            if (result.data == false) {
                alert(result.error);
            } else {
                var text = "";
                text += "Esta seguro de querer borrar esta evaluación ? :\n\n";
                text += "Fecha evaluación : " + result.dateEvaluation + "\n";
                text += "Estándares que cumple : " + result.data.Cumple + "\n";
                text += "Estándares que No cumple : " + result.data.NoCumple + "\n";
                text += "Resultado evaluación estándares mínimos : " + result.data.StandarsResult + "\n";
                var respuesta = confirm(text);

                if (respuesta == true) {
                    $.ajax({
                        url: "/Evaluations/DeleteEvaluation/" + id,
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

function chartCalificationsCiclo() {

    // Gráficar evaluación por ciclo
    //let controlCiclo = document.getElementById("imgCalificationsCiclo");
    //import Chart from '~/Scripts/chart.umd.js';
    $.ajax({
        url: "/Evaluations/GetAllCalifications",
        type: "GET",
        data: { id: evaluationID },
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,
        success: function (data) {
            var arrayData1 = [];
            var arrayData2 = [];
            for (var i = 0; i < data.length; i++) {
                arrayData1.push(data[i].Resultado);
                arrayData2.push(data[i].Resultado1);
            }
            const ctx = document.getElementById("calificationsPhva").getContext('2d');
            const chart = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: ["I. PLANEAR","II. HACER","III. VERIFICAR","IV. ACTUAR"],
                    datasets: [
                        {
                            label: "VALORES MÁXIMOS",
                            backgroundColor: [
                                'rgb(75, 192, 192)',
                                'rgb(75, 192, 192)',
                                'rgb(75, 192, 192)',
                                'rgb(75, 192, 192)',
                            ],
                            data: arrayData1
                        },
                        {
                            label: "VALORES OBTENIDOS",
                            backgroundColor: [
                                'rgb(255, 205, 86)',
                                'rgb(255, 205, 86)',
                                'rgb(255, 205, 86)',
                                'rgb(255, 205, 86)',
                            ],
                            data: arrayData2
                        }
                    ]
                },
                options: {
                    responsive: true,
                    scales: {
                        x: {
                            display: true,
                            title: {
                                display: true
                            }
                        },
                        y: {
                            display: true,
                            title: {
                                display: true
                            }
                        }
                    },
                    title: {
                        display: true,
                        text: 'DESARROLLO POR CICLO PHVA'
                    }
                }
            });
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function chartPorStandard() {

    // Gráficar evaluación por standard
    $.ajax({
        url: "/Evaluations/GetAllCalificationsStandard",
        type: "GET",
        data: { id: evaluationID },
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,
        success: function (data) {
            var arrayData1 = [];
            var arrayData2 = [];
            for (var i = 0; i < data.length; i++) {
                arrayData1.push(data[i].Resultado);
                arrayData2.push(data[i].Resultado1);
            }
            const ctx = document.getElementById("calificationsStandard").getContext('2d');
            const chart = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: ["G. RECURSOS", "G. INTEGRAL", "G. SALUD", "G. RIESGOS", "G. AMENAZAS", "VERIFICACIÓN", "MEJORAMIENTO"],
                    datasets: [
                        {
                            label: "VALORES MÁXIMOS",
                            backgroundColor: [
                                'rgb(75, 192, 192)',
                                'rgb(75, 192, 192)',
                                'rgb(75, 192, 192)',
                                'rgb(75, 192, 192)',
                                'rgb(75, 192, 192)',
                                'rgb(75, 192, 192)',
                                'rgb(75, 192, 192)'
                            ],
                            data: arrayData1
                        },
                        {
                            label: "VALORES OBTENIDOS",
                            backgroundColor: [
                                'rgb(255, 205, 86)',
                                'rgb(255, 205, 86)',
                                'rgb(255, 205, 86)',
                                'rgb(255, 205, 86)',
                                'rgb(255, 205, 86)',
                                'rgb(255, 205, 86)',
                                'rgb(255, 205, 86)'
                            ],
                            data: arrayData2
                        }
                    ]
                },
                options: {
                    responsive: true,
                    scales: {
                        x: {
                            display: true,
                            title: {
                                display: true
                            }
                        },
                        y: {
                            display: true,
                            title: {
                                display: true
                            }
                        }
                    },
                    title: {
                        display: true,
                        text: 'DESARROLLO POR ESTÁNDAR'
                    }
                }
            });
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function getIndicators() {
    var year = $("#txtYear").val();
    if (year = null) {
        year = new Date().getFullYear();
    }

    $.ajax({
        async: true,
        type: 'GET',
        url: "/Indicadores/GetAllIndicators",
        data: {
            year: $("#txtYear").val()
        },
        dataType: "json",
        contentType: "application/json;charset=UTF-8",
        success: function (response) {
            document.getElementById("accidentsProportion").innerHTML = "<strong>ACCIDENTALIDAD: " + response.data.AccidentsProportion + "</strong>";
            document.getElementById("accidents").innerHTML = "<strong>FRACUENCIA AT: " + response.data.Accidents + "</strong>";
            document.getElementById("incidents").innerHTML = "<strong>INCIDENTES: " + response.data.Incidents + "</strong>";
            document.getElementById("ausentisms").innerHTML = "<strong>AUSENTISMOS: " + response.data.Ausentisms + "</strong>";
            document.getElementById("mortality").innerHTML = "<strong># MUERTES: " + response.data.Mortality + "</strong>";
            document.getElementById("mortalityProportion").innerHTML = "<strong>MORTALIDAD: " + response.data.MortalityProportion + "</strong>";
            document.getElementById("minimalStandardsProportion").innerHTML = "<strong>CUMPLIMIENTO ESTÁNDARES: " + response.data.MinimalStandardsProportion + "</strong>";
            document.getElementById("activitiesPlanProportion").innerHTML = "<strong>% EJECUCIÓN ACTIVIDADES: " + response.data.ActivitiesPlanProportion + "</strong>";
            $('.queryIncidents').css("display", "block");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function frecuenciaAccidentalidad() {
    // Gráficar frecuencia accidentalidad
    $.ajax({
        async: true,
        type: 'GET',
        url: "/Indicadores/FrecuenciaAccidentalidad",
        data: {
            year: $("#txtYear").val()
        },
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var arrayLabel = [];
            var arrayData1 = [];
            var arrayData2 = [];
            var arrayData3 = [];
            var arrayData4 = [];
            for (var i = 0; i < data.length; i++) {
                arrayLabel.push(data[i].MesAnn);
                arrayData1.push(data[i].Resultado);
                arrayData2.push(data[i].IF);
                arrayData3.push(data[i].IG);
                arrayData4.push(data[i].ILI);
            }

            // Gráfica frecuencia accidentes
            var text = 'FRECUENCIA ACCIDENTALIDAD ' + $("#txtYear").val();
            const ctx = document.getElementById("atFrecuencia").getContext('2d');
            const chartF = new Chart(ctx, {
                type: 'line',
                data: {
                    labels: arrayLabel,
                    datasets: [
                        {
                            label: "PERIODOS",
                            data: arrayData1,
                            fill: false,
                            borderColor: 'rgb(75, 192, 192)',
                            tension: 0.1,
                        },
                    ]
                },
                options: {
                    responsive: true,
                    scales: {
                        x: {
                            display: true,
                            title: {
                                display: true
                            }
                        },
                        y: {
                            display: true,
                            title: {
                                display: true
                            }
                        }
                    },
                    title: {
                        display: true,
                        text: text
                    }
                }
            });

            // Gráfica indice frecuencia accidentes
            var text = 'INDICE DE FRECUENCIA DE ACCIDENTES ' + $("#txtYear").val();
            const ctxIF = document.getElementById("atIF").getContext('2d');
            const chartIF = new Chart(ctxIF, {
                type: 'line',
                data: {
                    labels: arrayLabel,
                    datasets: [
                        {
                            label: "PERIODOS",
                            data: arrayData2,
                            fill: false,
                            borderColor: 'rgb(75, 192, 192)',
                            tension: 0.1,
                        },
                    ]
                },
                options: {
                    responsive: true,
                    scales: {
                        x: {
                            display: true,
                            title: {
                                display: true
                            }
                        },
                        y: {
                            display: true,
                            title: {
                                display: true
                            }
                        }
                    },
                    title: {
                        display: true,
                        text: text
                    }
                }
            });

            // Gráfica indice severidad accidentes
            var text = 'INDICE DE GRAVEDAD DE ACCIDENTES ' + $("#txtYear").val();
            const ctxIG = document.getElementById("atIG").getContext('2d');
            const chartIG = new Chart(ctxIG, {
                type: 'line',
                data: {
                    labels: arrayLabel,
                    datasets: [
                        {
                            label: "PERIODOS",
                            data: arrayData3,
                            fill: false,
                            borderColor: 'rgb(75, 192, 192)',
                            tension: 0.1,
                        },
                    ]
                },
                options: {
                    responsive: true,
                    scales: {
                        x: {
                            display: true,
                            title: {
                                display: true
                            }
                        },
                        y: {
                            display: true,
                            title: {
                                display: true
                            }
                        }
                    },
                    title: {
                        display: true,
                        text: text
                    }
                }
            });

            // Gráfica indice de lesiones incapacitantes
            var text = 'INDICE DE LESIONES INCAPACITANTES ' + $("#txtYear").val();
            const ctxILI = document.getElementById("atILI").getContext('2d');
            const chartILI = new Chart(ctxILI, {
                type: 'line',
                data: {
                    labels: arrayLabel,
                    datasets: [
                        {
                            label: "PERIODOS",
                            data: arrayData4,
                            fill: false,
                            borderColor: 'rgb(75, 192, 192)',
                            tension: 0.1,
                        },
                    ]
                },
                options: {
                    responsive: true,
                    scales: {
                        x: {
                            display: true,
                            title: {
                                display: true
                            }
                        },
                        y: {
                            display: true,
                            title: {
                                display: true
                            }
                        }
                    },
                    title: {
                        display: true,
                        text: text
                    }
                }
            });

        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function indiceFrecuenciaAccidentes() {
    // Gráficar indice frecuencia accidentes
    $.ajax({
        async: true,
        type: 'GET',
        url: "/Indicadores/IndiceFrecuenciaAccidentes",
        data: {
            year: $("#txtYear").val()
        },
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var arrayLabel = [];
            var arrayData1 = [];
            for (var i = 0; i < data.length; i++) {
                arrayLabel.push(data[i].MesAnn);
                arrayData1.push(data[i].Resultado);
            }
            const ctx = document.getElementById("atFrecuencia").getContext('2d');
            const chart = new Chart(ctx, {
                type: 'line',
                data: {
                    labels: arrayLabel,
                    datasets: [
                        {
                            label: "PERIODOS",
                            data: arrayData1,
                            fill: false,
                            borderColor: 'rgb(75, 192, 192)',
                            tension: 0.1,
                        },
                    ]
                },
                options: {
                    responsive: true,
                    scales: {
                        x: {
                            display: true,
                            title: {
                                display: true
                            }
                        },
                        y: {
                            display: true,
                            title: {
                                display: true
                            }
                        }
                    },
                    title: {
                        display: true,
                        text: 'INDICE FRECUENCIA ACCIDENTES'
                    }
                }
            });
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function severidadAccidentalidad() {
    // Gráficar severidad accidentalidad
    $.ajax({
        async: true,
        type: 'GET',
        url: "/Indicadores/SeveridadAccidentalidad",
        data: {
            year: $("#txtYear").val()
        },
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var arrayLabel = [];
            var arrayData1 = [];
            for (var i = 0; i < data.length; i++) {
                arrayLabel.push(data[i].MesAnn);
                arrayData1.push(data[i].Resultado);
            }
            var text = 'SEVERIDAD ACCIDENTALIDAD ' + $("#txtYear").val();
            const ctx = document.getElementById("atSeveridad").getContext('2d');
            const chart = new Chart(ctx, {
                type: 'line',
                data: {
                    labels: arrayLabel,
                    datasets: [
                        {
                            label: "PERIODOS",
                            data: arrayData1,
                            fill: false,
                            borderColor: 'rgba(54, 162, 235)',
                            tension: 0.1,
                        },
                    ]
                },
                options: {
                    responsive: true,
                    scales: {
                        x: {
                            display: true,
                            title: {
                                display: true
                            }
                        },
                        y: {
                            display: true,
                            title: {
                                display: true
                            }
                        }
                    },
                    title: {
                        display: true,
                        text: text
                    }
                }
            });
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function mortalityProportion() {
    // Gráficar proporción AT mortales
    $.ajax({
        async: true,
        type: 'GET',
        url: "/Indicadores/MortalityProportion",
        data: {
            year: $("#txtYear").val()
        },
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var arrayLabel = [];
            var arrayData1 = [];
            for (var i = 0; i < data.length; i++) {
                arrayLabel.push(data[i].MesAnn);
                arrayData1.push(data[i].Resultado);
            }
            var text = 'PROPORCIÓN ACCIDENTES DE TRABAJO MORTALES ' + $("#txtYear").val();
            const ctx = document.getElementById("Mortality").getContext('2d');
            const chart = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: arrayLabel,
                    datasets: [
                        {
                            label: "PERIODOS",
                            data: arrayData1,
                            backgroundColor: 'rgba(54, 162, 235, 0.2)',
                            borderColor: 'rgba(54, 162, 235)',
                            borderWidth: 1
                        },
                    ]
                },
                options: {
                    responsive: true,
                    scales: {
                        x: {
                            display: true,
                            title: {
                                display: true
                            }
                        },
                        y: {
                            display: true,
                            title: {
                                display: true
                            },
                            ticks: {
                                beginAtZero: true
                            }
                        }
                    },
                    title: {
                        display: true,
                        text: text
                    }
                }
            });
            document.getElementById("Mortality").focus();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function ausentismoIndicator() {
    // Gráficar ausentismos por enfermedad laboral
    $.ajax({
        async: true,
        type: 'GET',
        url: "/Indicadores/GetAusentismo",
        data: {
            year: $("#txtYear").val()
        },
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var arrayLabel = [];
            var arrayData1 = [];
            for (var i = 0; i < data.length; i++) {
                arrayLabel.push(data[i].MesAnn);
                arrayData1.push(data[i].Resultado);
            }
            var text = 'AUSENTISMO POR CAUSA MÉDICA ' + $("#txtYear").val();
            const ctx = document.getElementById("Ausentismo").getContext('2d');
            const chart = new Chart(ctx, {
                type: 'line',
                data: {
                    labels: arrayLabel,
                    datasets: [
                        {
                            label: "PERIODOS",
                            data: arrayData1,
                            fill: false,
                            borderColor: 'rgb(75, 192, 192)',
                            tension: 0.1
                        },
                    ]
                },
                options: {
                    responsive: true,
                    scales: {
                        x: {
                            display: true,
                            title: {
                                display: true
                            }
                        },
                        y: {
                            display: true,
                            title: {
                                display: true
                            }
                        }
                    },
                    title: {
                        display: true,
                        text: text
                    }
                }
            });
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function factorRisk() {
    // Gráficar facteor riesgo ocupacional
    $.ajax({
        async: true,
        type: 'GET',
        url: "/Indicadores/GetFatorRiesgoOcupacional",
        data: {
            year: $("#txtYear").val()
        },
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var arrayLabel = [];
            var arrayData1 = [];
            for (var i = 0; i < data.length; i++) {
                arrayLabel.push(data[i].MesAnn);
                arrayData1.push(data[i].Resultado);
            }
            var text = 'FACTOR RIESGO OCUPACIONAL ' + $("#txtYear").val();
            const ctx = document.getElementById("FactorRisk").getContext('2d');
            const chart = new Chart(ctx, {
                type: 'doughnut',
                data: {
                    labels: arrayLabel,
                    datasets: [
                        {
                            label: "CATEGORIAS",
                            data: arrayData1,
                            backgroundColor: [
                                '#F0410E',
                                '#E9F00E',
                                '#56F00E',
                                '#0EF0D8',
                                '#0EA5F0',
                                '#0E3BF0',
                                '#670EF0'
                            ],
                        },
                    ]
                },
                options: {
                    responsive: true,
                    scales: {
                        x: {
                            display: true,
                            title: {
                                display: true
                            }
                        },
                        y: {
                            display: true,
                            title: {
                                display: true
                            }
                        }
                    },
                    title: {
                        display: true,
                        text: text
                    }
                }
            });
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function escolaridadIndicator() {
    // Gráficar escolaridad
    $.ajax({
        async: true,
        type: 'GET',
        url: "/Indicadores/GetScholarship",
        data: {
            year: $("#txtYear").val()
        },
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var arrayLabel = [];
            var arrayData1 = [];
            for (var i = 0; i < data.length; i++) {
                arrayLabel.push(data[i].MesAnn);
                arrayData1.push(data[i].Resultado);
            }
            const ctx = document.getElementById("Escolaridad").getContext('2d');
            const chart = new Chart(ctx, {
                type: 'doughnut',
                data: {
                    labels: arrayLabel,
                    datasets: [
                        {
                            label: "CATEGORIAS",
                            data: arrayData1,
                            backgroundColor: [
                                '#F0410E',
                                '#E9F00E',
                                '#56F00E',
                                '#0EF0D8',
                                '#0EA5F0',
                                '#0E3BF0',
                                '#670EF0',
                                '#0EA5F0'
                            ],
                        },
                    ]
                },
                options: {
                    responsive: true,
                    scales: {
                        x: {
                            display: true,
                            title: {
                                display: true
                            }
                        },
                        y: {
                            display: true,
                            title: {
                                display: true
                            }
                        }
                    },
                    title: {
                        display: true,
                        text: 'ESCOLARIDAD'
                    }
                }
            });
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function workAreaIndicator() {
    // Gráficar escolaridad
    $.ajax({
        async: true,
        type: 'GET',
        url: "/Indicadores/GetWorkAreas",
        data: {
            year: $("#txtYear").val()
        },
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var arrayLabel = [];
            var arrayData1 = [];
            for (var i = 0; i < data.length; i++) {
                arrayLabel.push(data[i].MesAnn);
                arrayData1.push(data[i].Resultado);
            }
            const ctx = document.getElementById("workArea").getContext('2d');
            const chart = new Chart(ctx, {
                type: 'doughnut',
                data: {
                    labels: arrayLabel,
                    datasets: [
                        {
                            label: "CATEGORIAS",
                            data: arrayData1,
                            backgroundColor: [
                                '#F0410E',
                                '#E9F00E',
                                '#56F00E',
                                '#0EA5F0'
                            ],
                        },
                    ]
                },
                options: {
                    responsive: true,
                    scales: {
                        x: {
                            display: true,
                            title: {
                                display: true
                            }
                        },
                        y: {
                            display: true,
                            title: {
                                display: true
                            }
                        }
                    },
                    title: {
                        display: true,
                        text: 'AREAS DE TRABAJO'
                    }
                }
            });
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function ocupacionIndicator() {
    // Gráficar ocupación
    $.ajax({
        async: true,
        type: 'GET',
        url: "/Indicadores/GetOcupaciones",
        data: {
            year: $("#txtYear").val()
        },
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var arrayLabel = [];
            var arrayData1 = [];
            for (var i = 0; i < data.length; i++) {
                arrayLabel.push(data[i].MesAnn);
                arrayData1.push(data[i].Resultado);
            }
            const ctx = document.getElementById("Ocupacion").getContext('2d');
            const chart = new Chart(ctx, {
                type: 'doughnut',
                data: {
                    labels: arrayLabel,
                    datasets: [
                        {
                            label: "CATEGORIAS",
                            data: arrayData1,
                            backgroundColor: [
                                '#F0410E',
                                '#E9F00E',
                                '#56F00E',
                                '#0EF0D8',
                                '#0EA5F0',
                                '#0E3BF0',
                                '#670EF0',
                                '#0EA5F0'
                            ],
                        },
                    ]
                },
                options: {
                    responsive: true,
                    scales: {
                        x: {
                            display: true,
                            title: {
                                display: true
                            }
                        },
                        y: {
                            display: true,
                            title: {
                                display: true
                            }
                        }
                    },
                    title: {
                        display: true,
                        text: 'OCUPACIÓN'
                    }
                }
            });
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function tipoVinculacionIndicator() {
    // Gráficar tipo vinculación
    $.ajax({
        async: true,
        type: 'GET',
        url: "/Indicadores/GetTiposVinculacion",
        data: {
            year: $("#txtYear").val()
        },
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var arrayLabel = [];
            var arrayData1 = [];
            for (var i = 0; i < data.length; i++) {
                arrayLabel.push(data[i].MesAnn);
                arrayData1.push(data[i].Resultado);
            }
            const ctx = document.getElementById("Vinculación").getContext('2d');
            const chart = new Chart(ctx, {
                type: 'doughnut',
                data: {
                    labels: arrayLabel,
                    datasets: [
                        {
                            label: "CATEGORIAS",
                            data: arrayData1,
                            backgroundColor: [
                                '#F0410E',
                                '#E9F00E',
                                '#56F00E',
                                '#0EF0D8',
                                '#0EA5F0',
                                '#0E3BF0'
                            ],
                        },
                    ]
                },
                options: {
                    responsive: true,
                    scales: {
                        x: {
                            display: true,
                            title: {
                                display: true
                            }
                        },
                        y: {
                            display: true,
                            title: {
                                display: true
                            }
                        }
                    },
                    title: {
                        display: true,
                        text: 'TIPOS VINCULACIÓN'
                    }
                }
            });
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function estadoCivilIndicator() {
    // Gráficar estado civil
    $.ajax({
        async: true,
        type: 'GET',
        url: "/Indicadores/GetEstadosCivil",
        data: {
            year: $("#txtYear").val()
        },
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var arrayLabel = [];
            var arrayData1 = [];
            for (var i = 0; i < data.length; i++) {
                arrayLabel.push(data[i].MesAnn);
                arrayData1.push(data[i].Resultado);
            }
            const ctx = document.getElementById("estadoCivil").getContext('2d');
            const chart = new Chart(ctx, {
                type: 'doughnut',
                data: {
                    labels: arrayLabel,
                    datasets: [
                        {
                            label: "CATEGORIAS",
                            data: arrayData1,
                            backgroundColor: [
                                '#F0410E',
                                '#E9F00E',
                                '#56F00E',
                                '#0EF0D8',
                                '#0EA5F0',
                                '#0E3BF0'
                            ],
                        },
                    ]
                },
                options: {
                    responsive: true,
                    scales: {
                        x: {
                            display: true,
                            title: {
                                display: true
                            }
                        },
                        y: {
                            display: true,
                            title: {
                                display: true
                            }
                        }
                    },
                    title: {
                        display: true,
                        text: 'ESTADO CIVIL'
                    }
                }
            });
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function tipoViviendaIndicator() {
    // Gráficar tipo vivienda
    $.ajax({
        async: true,
        type: 'GET',
        url: "/Indicadores/GetTiposVivienda",
        data: {
            year: $("#txtYear").val()
        },
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var arrayLabel = [];
            var arrayData1 = [];
            for (var i = 0; i < data.length; i++) {
                arrayLabel.push(data[i].MesAnn);
                arrayData1.push(data[i].Resultado);
            }
            const ctx = document.getElementById("tipoVivienda").getContext('2d');
            const chart = new Chart(ctx, {
                type: 'doughnut',
                data: {
                    labels: arrayLabel,
                    datasets: [
                        {
                            label: "CATEGORIAS",
                            data: arrayData1,
                            backgroundColor: [
                                '#56F00E',
                                '#0EF0D8',
                                '#0EA5F0',
                                '#0E3BF0'
                            ],
                        },
                    ]
                },
                options: {
                    responsive: true,
                    scales: {
                        x: {
                            display: true,
                            title: {
                                display: true
                            }
                        },
                        y: {
                            display: true,
                            title: {
                                display: true
                            }
                        }
                    },
                    title: {
                        display: true,
                        text: 'TENENCIA VIVIENDA'
                    }
                }
            });
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function tipoJornadaIndicator() {
    // Gráficar tipo jornada
    $.ajax({
        async: true,
        type: 'GET',
        url: "/Indicadores/GetTiposJornada",
        data: {
            year: $("#txtYear").val()
        },
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var arrayLabel = [];
            var arrayData1 = [];
            for (var i = 0; i < data.length; i++) {
                arrayLabel.push(data[i].MesAnn);
                arrayData1.push(data[i].Resultado);
            }
            const ctx = document.getElementById("tipoJornada").getContext('2d');
            const chart = new Chart(ctx, {
                type: 'doughnut',
                data: {
                    labels: arrayLabel,
                    datasets: [
                        {
                            label: "CATEGORIAS",
                            data: arrayData1,
                            backgroundColor: [
                                '#56F00E',
                                '#0EF0D8',
                                '#0EA5F0'
                            ],
                        },
                    ]
                },
                options: {
                    responsive: true,
                    scales: {
                        x: {
                            display: true,
                            title: {
                                display: true
                            }
                        },
                        y: {
                            display: true,
                            title: {
                                display: true
                            }
                        }
                    },
                    title: {
                        display: true,
                        text: 'TIPO JORNADA'
                    }
                }
            });
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function numeroHijosIndicator() {
    // Gráficar número de hijos
    $.ajax({
        async: true,
        type: 'GET',
        url: "/Indicadores/GetNumeroHijos",
        data: {
            year: $("#txtYear").val()
        },
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var arrayLabel = [];
            var arrayData1 = [];
            for (var i = 0; i < data.length; i++) {
                arrayLabel.push(data[i].MesAnn);
                arrayData1.push(data[i].Resultado);
            }
            const ctx = document.getElementById("numeroHijos").getContext('2d');
            const chart = new Chart(ctx, {
                type: 'doughnut',
                data: {
                    labels: arrayLabel,
                    datasets: [
                        {
                            label: "CATEGORIAS",
                            data: arrayData1,
                            backgroundColor: [
                                '#F0410E',
                                '#E9F00E',
                                '#56F00E',
                                '#0EF0D8',
                                '#0EA5F0',
                                '#0E3BF0',
                                '#670EF0',
                                '#0EA5F0'
                            ],
                        },
                    ]
                },
                options: {
                    responsive: true,
                    scales: {
                        x: {
                            display: true,
                            title: {
                                display: true
                            }
                        },
                        y: {
                            display: true,
                            title: {
                                display: true
                            }
                        }
                    },
                    title: {
                        display: true,
                        text: 'NÚMERO HIJOS'
                    }
                }
            });
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function proportionActions() {
    // Gráficar proporcón de acciones
    $.ajax({
        async: true,
        type: 'GET',
        url: "/Indicadores/GetActionsIndicator",
        data: {
            year: $("#txtYear").val()
        },
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var arrayLabel = [];
            var arrayData1 = [];
            for (var i = 0; i < data.length; i++) {
                arrayLabel.push(data[i].MesAnn);
                arrayData1.push(data[i].Resultado);
            }
            const ctx = document.getElementById("ProportionActions").getContext('2d');
            const chart = new Chart(ctx, {
                type: 'doughnut',
                data: {
                    labels: arrayLabel,
                    datasets: [
                        {
                            label: "CATEGORIAS",
                            data: arrayData1,
                            backgroundColor: [
                                '#F0410E',
                                '#E9F00E',
                                '#56F00E',
                            ],
                        },
                    ]
                },
                options: {
                    responsive: true,
                    scales: {
                        x: {
                            display: true,
                            title: {
                                display: true
                            }
                        },
                        y: {
                            display: true,
                            title: {
                                display: true
                            }
                        }
                    },
                    title: {
                        display: true,
                        text: 'PROPORCIÓN DE ACCIONES CPM EJECUTADAS'
                    }
                }
            });
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function chartEfectiveActions() {

    // Gráficar acciones efectivas / no efectivas
    $.ajax({
        url: "/Acciones/GetEfectiveActions",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,
        success: function (data) {
            var arrayLabel = [];
            var arrayData1 = [];
            for (var i = 0; i < data.length; i++) {
                arrayLabel.push(data[i].MesAnn);
                arrayData1.push(data[i].Resultado);
            }
            const ctx = document.getElementById("efectives").getContext('2d');
            const chart = new Chart(ctx, {
                type: 'doughnut',
                data: {
                    labels: arrayLabel,
                    datasets: [
                        {
                            label: "CATEGORIAS",
                            data: arrayData1,
                            backgroundColor: [
                                '#56F00E',
                                '#E9F00E'
                            ],
                        },
                    ]
                },
                options: {
                    responsive: true,
                    scales: {
                        x: {
                            display: true,
                            title: {
                                display: true
                            }
                        },
                        y: {
                            display: true,
                            title: {
                                display: true
                            }
                        }
                    },
                    title: {
                        display: true,
                        text: 'EFECTIVIDAD ACCIONES'
                    }
                }
            });
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function CreateRisk(unsafeactID) {
    $.ajax({
        async: true,
        type: 'GET',
        url: "/Unsafeacts/CreateRisk",
        data: { id: unsafeactID },
        dataType: "json",
        contentType: "application/json;charset=UTF-8",
        success: function (data) {
            var riesgoVM = {
                ID: unsafeactID,
                ZonaID: data.ZonaID,
                ProcesoID: data.ProcesoID,
                ActividadID: data.ActividadID,
                TareaID: data.TareaID,
                Rutinaria: false,
                CategoriaPeligroID: data.ZonaID,
                PeligroID: data.ZonaID
            }
            $.ajax({
                type: "POST",
                url: "/Unsafeacts/CreateRisk",
                data: { model: riesgoVM },
                dataType: "json",
                success: function (response) {
                    window.location = response.url;
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.status);
                    alert(thrownError);
                }
            });
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function auditedResult() {
    $(document).ready(function () {

        $('#auditChapter').change(function () {
            var auditChapter = $(this).val();
            $.ajax({
                url: '/Audits/GetQuestionsChapter',
                type: 'GET',
                data: { chapter: auditChapter },
                success: function (response) {
                    var model = []; // Crear el modelo aquí
                    response.forEach(function (item,index) {
                        var auditItemID = item.ID;
                        var name = index + 1 + ". " + item.Name + " ?:";
                        var respuesta = {
                            ID: 0,
                            AuditID: $('#auditID').val(),
                            AuditItemID: auditItemID, // Utiliza el valor de AuditItemID obtenido
                            Result: '', // Inicializa Result como una cadena vacía
                            AuditItem: {},
                            Process: $('#auditProcess').val(),
                            AuditChapter: $('#auditChapter').val()
                        };
                        model.push(respuesta);

                        var html = '<div>' + name + '</div>' +
                            '<select class="requisite">' +
                            '<option value="NC">NC</option>' +
                            '<option value="CP">CP</option>' +
                            '<option value="CYD">CYD</option>' +
                            '</select>' + '<hr />';

                        $('.auditContainer').append(html);
                    });

                    // Almacena el modelo en un atributo de datos para usarlo posteriormente
                    $('.auditContainer').data('model', model);
                }
            });
        });
    });
}

function auditedSave() {

    $('#saveAuditedResult').click(function () {
        // Obtiene el modelo desde el atributo de datos
        var model = $('.auditContainer').data('model');

        // Actualiza el valor de Result en el modelo
        $('.requisite').each(function (index) {
            var selectedValue = $(this).find('option:selected').val();
            model[index].Result = selectedValue;
        });

        // Enviar respuestas al servidor usando Ajax
        $.ajax({
            url: '/Audits/CreateAuditedResult',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(model),
            success: function (response) {
                // Manejar el resultado si es necesario
                $('.tabExecute').css('display', 'none');
                alert(response.mensaj);
            }
        });
    });
}
