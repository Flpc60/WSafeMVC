function buildTableRow(header, values) {
    var rowHtml = '';
    rowHtml += '<tr style="background-color:gainsboro;">';
    rowHtml += `<td style="white-space: nowrap;">${header}</td>`;
    values.forEach(value => {
        var result;
        // Verificar si el denominador es cero
        if (header === "% Eficacia" || header === "% Eficicencia" || header === "% Efectividad") {
            result = (value.denominator !== 0) ? (value.numerator / value.denominator) * 100 : 0;
            result = result.toFixed(0) + "%";
        } else {
            result = (value.denominator !== 0) ? value.numerator / value.denominator : 0;
        }
        rowHtml += `<td>${result}</td>`;
    });
    rowHtml += '</tr>';
    return rowHtml;
}

function ShowSchedule() {
    $.ajax({
        url: "/Capacitations/GetScheduleAll",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            var html = '';
            response.forEach(function (item, index) {
                html += buildTableRow("Capacitaciones Programadas", [{ numerator: item.P1, denominator: 1 }, { numerator: item.P2, denominator: 1 }, { numerator: item.P3, denominator: 1 }, { numerator: item.P4, denominator: 1 }, { numerator: item.P5, denominator: 1 }, { numerator: item.P6, denominator: 1 }, { numerator: item.P7, denominator: 1 }, { numerator: item.P8, denominator: 1 }, { numerator: item.P9, denominator: 1 }, { numerator: item.P10, denominator: 1 }, { numerator: item.P11, denominator: 1 }, { numerator: item.P12, denominator: 1 }]);
                html += buildTableRow("Capacitaciones Ejecutadas", [{ numerator: item.E1, denominator: 1 }, { numerator: item.E2, denominator: 1 }, { numerator: item.E3, denominator: 1 }, { numerator: item.E4, denominator: 1 }, { numerator: item.E5, denominator: 1 }, { numerator: item.E6, denominator: 1 }, { numerator: item.E7, denominator: 1 }, { numerator: item.E8, denominator: 1 }, { numerator: item.E9, denominator: 1 }, { numerator: item.E10, denominator: 1 }, { numerator: item.E11, denominator: 1 }, { numerator: item.E12, denominator: 1 }]);
                html += buildTableRow("% Eficacia", [{ numerator: item.E1, denominator: item.P1 }, { numerator: item.E2, denominator: item.P2 }, { numerator: item.E3, denominator: item.P3 }, { numerator: item.E4, denominator: item.P4 }, { numerator: item.E5, denominator: item.P5 }, { numerator: item.E6, denominator: item.P6 }, { numerator: item.E7, denominator: item.P7 }, { numerator: item.E8, denominator: item.P8 }, { numerator: item.E9, denominator: item.P9 }, { numerator: item.E10, denominator: item.P10 }, { numerator: item.E11, denominator: item.P11 }, { numerator: item.E12, denominator: item.P12 }]);
                html += buildTableRow("% Eficicencia", [{ numerator: item.C1, denominator: item.Cc1 }, { numerator: item.C2, denominator: item.Cc2 }, { numerator: item.C3, denominator: item.Cc3 }, { numerator: item.C4, denominator: item.Cc4 }, { numerator: item.C5, denominator: item.Cc5 }, { numerator: item.C6, denominator: item.Cc6 }, { numerator: item.C7, denominator: item.Cc7 }, { numerator: item.C8, denominator: item.Cc8 }, { numerator: item.C9, denominator: item.Cc9 }, { numerator: item.C10, denominator: item.Cc10 }, { numerator: item.C11, denominator: item.Cc11 }, { numerator: item.C12, denominator: item.Cc12 }]);
                html += buildTableRow("% Efectividad", [{ numerator: item.Tc1, denominator: item.C1 }, { numerator: item.Tc2, denominator: item.C2 }, { numerator: item.Tc3, denominator: item.C3 }, { numerator: item.Tc4, denominator: item.C4 }, { numerator: item.Tc5, denominator: item.C5 }, { numerator: item.Tc6, denominator: item.C6 }, { numerator: item.Tc7, denominator: item.C7 }, { numerator: item.Tc8, denominator: item.C8 }, { numerator: item.Tc9, denominator: item.C9 }, { numerator: item.Tc10, denominator: item.C10 }, { numerator: item.Tc11, denominator: item.C11 }, { numerator: item.Tc12, denominator: item.C12 }]);
            });
            $('.cronogramaBody').html(html);
            $('.cronograma1Body').html(html);
            $('.cronogramaBody').focus();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}
function chartCapacitations() {

    // Gráficar capacitaciones
    $.ajax({
        url: "/Capacitations/GetScheduleAll",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,
        success: function (data) {
            var arrayLabel = ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"];
            var eficacia = [];
            var eficiencia = [];
            var efectividad = [];
            var backgroundColor = 'rgb(81, 152, 255)';
            data.forEach(function (item, index) {
                for (var i = 1; i <= 12; i++) {
                    var numerator = item[`E${i}`]; // Accede a la propiedad del objeto usando la notación de corchetes
                    var denominator = item[`P${i}`]; // Accede a la propiedad del objeto usando la notación de corchetes
                    var resultEficacia = (denominator !== 0) ? (numerator / denominator) * 100 : 0;
                    eficacia.push(resultEficacia);

                    numerator = item[`C${i}`]; // Accede a la propiedad del objeto usando la notación de corchetes
                    denominator = item[`Cc${i}`]; // Accede a la propiedad del objeto usando la notación de corchetes
                    var resultEficiencia = (denominator !== 0) ? (numerator / denominator) * 100 : 0;
                    eficiencia.push(resultEficiencia);

                    numerator = item[`Tc${i}`]; // Accede a la propiedad del objeto usando la notación de corchetes
                    denominator = item[`C${i}`]; // Accede a la propiedad del objeto usando la notación de corchetes
                    var resultEfectividad = (denominator !== 0) ? (numerator / denominator) * 100 : 0;
                    efectividad.push(resultEfectividad);
                }
            });

            var legend = "% DE EFICACIA CAPACITACIONES";
            var ctx = document.getElementById("capacitationEficacia").getContext('2d');

            var chart = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: arrayLabel,
                    datasets: [
                        {
                            label: "ACTIVIDADES EJECUTADAS",
                            backgroundColor: backgroundColor,
                            data: eficacia
                        }
                    ]
                },
                options: {
                    plugins: {
                        legend: {
                            title: {
                                display: true,
                                text: legend
                            },
                            display: true,
                            position: 'inside',
                            labels: {
                                color: backgroundColor
                            }
                        }
                    },
                    responsive: true,
                    scales: {
                        x: {
                            display: true,
                            title: {
                                display: true
                            }
                        },
                        yAxes: [{
                            display: true,
                            title: {
                                display: true
                            },
                            ticks: {
                                beginAtZero: true
                            }
                        }]
                    },
                    title: {
                        display: true,
                        text: legend,
                        color: backgroundColor
                    }
                }
            });

            legend = "% DE EFICIENCIA CAPACITACIONES";
            ctx = document.getElementById("capacitationEficiencia").getContext('2d');

            chart = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: arrayLabel,
                    datasets: [
                        {
                            label: "ACTIVIDADES EJECUTADAS",
                            backgroundColor: backgroundColor,
                            data: eficiencia
                        }
                    ]
                },
                options: {
                    plugins: {
                        legend: {
                            title: {
                                display: true,
                                text: legend
                            },
                            display: true,
                            position: 'inside',
                            labels: {
                                color: backgroundColor
                            }
                        }
                    },
                    responsive: true,
                    scales: {
                        x: {
                            display: true,
                            title: {
                                display: true
                            }
                        },
                        yAxes: [{
                            display: true,
                            title: {
                                display: true
                            },
                            ticks: {
                                beginAtZero: true
                            }
                        }]
                    },
                    title: {
                        display: true,
                        text: legend,
                        color: backgroundColor
                    }
                }
            });

            legend = "% DE EFECTIVIDAD CAPACITACIONES";
            ctx = document.getElementById("capacitationEfectividad").getContext('2d');

            chart = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: arrayLabel,
                    datasets: [
                        {
                            label: "ACTIVIDADES EJECUTADAS",
                            backgroundColor: backgroundColor,
                            data: efectividad
                        }
                    ]
                },
                options: {
                    plugins: {
                        legend: {
                            title: {
                                display: true,
                                text: legend
                            },
                            display: true,
                            position: 'inside',
                            labels: {
                                color: backgroundColor
                            }
                        }
                    },
                    responsive: true,
                    scales: {
                        x: {
                            display: true,
                            title: {
                                display: true
                            }
                        },
                        yAxes: [{
                            display: true,
                            title: {
                                display: true
                            },
                            ticks: {
                                beginAtZero: true
                            }
                        }]
                    },
                    title: {
                        display: true,
                        text: legend,
                        color: backgroundColor
                    }
                }
            });

            var canvasElement = document.getElementById("capacitationEficacia");
            canvasElement.tabIndex = 0;
            canvasElement.focus();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function getTrainingTopic() {
    var id = $("#trainingID").val();
    $.ajax({
        async: true,
        type: 'GET',
        url: "/Capacitations/GetTrainingTopicName",
        data: { id: id },
        dataType: "json",
        contentType: "application/json;charset=UTF-8",
        success: function (response) {
            $("#trainingID").val(response.ID);
            $("#objetive").val(response.Objetive);
            $("#contenido").val(response.Content);
            $("#Resources").val(response.Resources);
            $('.tabTraining').css("display", "block");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function ShowCapacitations(id) {
    // Mostrar todos los seguimientos
    $('#btnSigue').hide();
    $.ajax({
        url: "/Capacitations/GetSigueCapacitations",
        data: { id: id },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            var html = '';
            response.forEach(function (item, index) {
                switch (item.StateCronogram) {
                    case 1:
                        var color = "yellow";
                        var estado = "Programada";
                        break;

                    case 2:
                        var color = "green";
                        var estado = "Ejecutada";
                        break;

                    case 3:
                        var color = "red";
                        var estado = "Reprogramada";
                        break;

                    default:
                        break;
                }
                var sigueDate = moment(item.DateSigue);
                var formattedDate = sigueDate.format('YYYY-MM-DD');
                html += '<tr>';
                html += '<td style="white-space: nowrap;">' + formattedDate + '</td>';
                html += '<td style="background-color:' + color + ';">' + estado + '</td>';
                html += '<td>' + item.Programed + '</td>';
                html += '<td>' + item.Executed + '</td>';
                html += '<td>' + item.Citados + '</td>';
                html += '<td>' + item.Capacitados + '</td>';
                html += '<td>' + item.Evaluados + '</td>';
                html +=
                    '<td><a href="#" onclick="return getSigueCapacitation(' + item.ID + ')">Editar</a> | <a href = "#" onclick = "deleteSigueCapacitation(' + item.ID + ')"> Borrar</a></td>';
                html += '<hr />';
                html += '</tr>';
            });
            $('.sigueCapacitation').html(html);
            $('.sigueCapacitation').focus();
            $('.tabSigueCapacitation').css("display", "block");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}
function deleteCapacitation(id) {
    $.ajax({
        url: "/Capacitations/DeleteCapacitation/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        async: true,                                               // si es asincrónico o no
        success: function (response) {
            if (response.data == false) {
                alert(response.error);
            } else {
                var sigueDate = moment(response.data.InitialDate);
                var formattedDate = sigueDate.format('YYYY-MM-DD');
                var text = "";
                text += "Esta seguro de querer borrar esta capacitación ?:\n\n";
                text += "Fecha inicial: " + formattedDate + "\n";
                text += "Estado: " + response.data.StateCronogram + "\n";
                text += "Programadas: " + response.data.Programed + "\n";
                text += "Ejecutadas: " + response.data.Executed + "\n";
                text += "Citados: " + response.data.Citados + "\n";
                text += "Capacitados: " + response.data.Capacitados + "\n";
                text += "Evaluados: " + response.data.Evaluados + "\n";
                var respuesta = confirm(text);

                if (respuesta == true) {
                    $.ajax({
                        url: "/Capacitations/DeleteCapacitation/" + id,
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

function addSigueCapacitation(id) {
    // Crea una nueva trazabilidad
    $('.tabAddSigue').css("display", "none");
    var schedule = {
        ID: "0",
        DateSigue: $("#dateSigue").val(),
        TrabajadorID: $("#workerID").val(),
        StateCronogram: $("#stateCronogram").val(),
        Programed: $("#sigueProgramed").val(),
        Executed: $("#sigueExecuted").val(),
        Citados: $("#citados").val(),
        Capacitados: $("#capacitados").val(),
        Evaluados: $("#sigueEvaluados").val(),
        CapacitationID: id,
        OrganizationID: 1,
        ClientID: 1,
        UserID: 1
    };
    $.ajax({
        type: "POST",
        url: "/Capacitations/CreateSigueCapacitation",
        data: { model: schedule },
        dataType: "json",
        async: true,
        success: function (response) {
            $("#sigueCapacitation").val(response.ID);
            $("#btnAddCapacitation").show();
            $("#btnUpdCapacitation").hide();
            $(".tabAddSigue").css("display", "none");
            alert(response.mensaj);
            ShowCapacitations(id);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function updateSigueCapacitation(id) {
    // Actualiza una trazabilidad
    $('.tabAddSigue').css("display", "none");
    var schedule = {
        ID: $("#sigueCapacitation").val(),
        DateSigue: $("#dateSigue").val(),
        TrabajadorID: $("#workerID").val(),
        StateCronogram: $("#stateCronogram").val(),
        Programed: $("#sigueProgramed").val(),
        Executed: $("#sigueExecuted").val(),
        Citados: $("#citados").val(),
        Capacitados: $("#capacitados").val(),
        Evaluados: $("#sigueEvaluados").val(),
        CapacitationID: id,
        OrganizationID: 1,
        ClientID: 1,
        UserID: 1
    };
    $.ajax({
        type: "POST",
        url: "/Capacitations/UpdateSigueCapacitation",
        data: { model: schedule },
        dataType: "json",
        async: true,
        success: function (response) {
            alert(response.mensaj);
            $(".tabSigueCapacitation").css("display", "block");
            ShowCapacitations(id);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function getSigueCapacitation(id) {
    $.ajax({
        async: true,
        type: 'GET',
        url: "/Capacitations/UpdateSigueCapacitation",
        data: {
            id: id
        },
        dataType: "json",
        contentType: "application/json;charset=UTF-8",
        success: function (response) {
            var sigueDate = moment(response.DateSigue);
            var formattedDate = sigueDate.format('YYYY-MM-DD');

            $("#dateSigue").val(formattedDate);
            $("#workerID").val(response.TrabajadorID);
            $("#stateCronogram").val(response.StateCronogram);
            $("#sigueProgramed").val(response.Programed);
            $("#sigueExecuted").val(response.Executed);
            $("#sigueCitados").val(response.Citados);
            $("#sigueCapacitados").val(response.Capacitados);
            $("#sigueEvaluados").val(response.Evaluados);
            $("#sigueCapacitation").val(response.ID);
            $("#btnAddCapacitation").hide();
            $("#btnUpdCapacitation").show();
            $("#btnCanCapacitation").show();
            $(".tabAddSigue").css("display", "block");
            $("#dateSigue").focus();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function deleteSigueCapacitation(id) {
    $.ajax({
        url: "/Capacitations/DeleteSigueCapacitation/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        async: true,                                               // si es asincrónico o no
        success: function (response) {
            var sigueDate = moment(response.DateSigue);
            var formattedDate = sigueDate.format('YYYY-MM-DD');
            var text = "";
            text += "Esta seguro de querer borrar esta capacitación ?:\n\n";
            text += "Fecha: " + formattedDate + "\n";
            text += "Estado: " + response.StateCronogram + "\n";
            text += "Programadas: " + response.Programed + "\n";
            text += "Ejecutadas: " + response.Executed + "\n";
            text += "Citados: " + response.Citados + "\n";
            text += "Capacitados: " + response.Capacitados + "\n";
            text += "Evaluados: " + response.Evaluados + "\n";
            var respuesta = confirm(text);

            if (respuesta == true) {
                $.ajax({
                    url: "/Capacitations/DeleteSigueCapacitation/" + id,
                    type: "POST",
                    contentType: "application/json;charset=UTF-8",
                    dataType: "json",
                    async: true,
                    success: function (response) {
                        $("#sigueCapacitation").val(response.data);
                        $("#btnAddTraceability").hide();
                        $("#btnUpdTraceability").hide();
                        $(".tabAddSigue").css("display", "none");
                        alert(response.mensaj);
                        ShowCapacitations(response.data);
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        alert(xhr.status);
                        alert(thrownError);
                    }
                });
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function generateCapacitationsPdf() {
    ShowSchedule();
    chartCapacitations();
    var cronograma = $('.cronogramaBody').html();
    var encodedData = encodeURIComponent(cronograma);
    var eficacia = document.getElementById("capacitationEficacia");
    var encodedEficacia = eficacia.toDataURL();;
    var eficiencia = document.getElementById("capacitationEficiencia");
    var encodedEficiencia = eficiencia.toDataURL();;
    var efectividad = document.getElementById("capacitationEfectividad");
    var encodedEfectividad = efectividad.toDataURL();;

    $.ajax({
        url: "/Capacitations/CapacitationsPdf",
        type: "POST",
        data: {
            cronograma: encodedData,
            eficacia: encodedEficacia,
            eficiencia: encodedEficiencia,
            efectividad: encodedEfectividad
        },
        success: function (pdfUrl) {
            window.open(pdfUrl, '_blank');
            window.location.href = '/Capacitations/index';
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}
function addTrainingTopic() {
    var trainingTopic = {
        Name: $("#Name").val(),
        Objetive: $("#Objetive").val(),
        Content: $("#Content").val(),
        Resources: $("#Resources").val(),
        ActivityFrequency: $("#ActivityFrequency").val(),
        OrganizationID: 1
    };

    $.ajax({
        type: "POST",
        url: "/Capacitations/AddTrainingTopic",
        data: JSON.stringify(trainingTopic), // Convertir el objeto a una cadena JSON
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (result) {
            if (result.success) {
                alert("Actividad adicionada !!");
                // Opcional: actualizar el dropdown o la página para reflejar el nuevo registro
                location.reload();
            } else {
                alert("Error: " + result.message);
            }
            $(".tabNewTopic").css("display", "none");
        },
        error: function (xhr, status, error) {
            alert("Error del servidor: " + error);
        }
    });
}

function addNewControl() {
    var controlVM = {
        Description: $("#description").val(),
        Beneficios: $("#beneficio").val(),
        CategoriaAplicacion: $("#categoriaApp").val(),
        Finalidad: $("#finalidad").val(),
        Intervencion: $("#intervencion").val(),
        Presupuesto: $("#presupuesto").val(),
        Intervencion: $("#intervencion").val(),
        OrganizationID: 1,
        ClientID: 1,
        UserID: 1
    };

    $.ajax({
        type: "POST",
        url: "/Riesgos/AddControl",
        data: JSON.stringify(controlVM),
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (result) {
            if (result.success) {
                alert("Control adicionado !!");
                location.reload();
            } else {
                alert("Error: " + result.message);
            }
            $(".tabAddControl").css("display", "none");
        },
        error: function (xhr, status, error) {
            alert("Error del servidor: " + error);
        }
    });
}

function addMainCause() {
    var causeMainVM = {
        Name: $("#mainCause").val(),
        OrganizationID: 1,
        ClientID: 1,
        UserID: 1
    };

    $.ajax({
        type: "POST",
        url: "/Riesgos/AddMainCause",
        data: JSON.stringify(causeMainVM),
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (result) {
            if (result.success) {
                alert("Causa principal adicionada !!");
                location.reload();
            } else {
                alert("Error: " + result.message);
            }
            $(".tabAddMainCause").css("display", "none");
        },
        error: function (xhr, status, error) {
            alert("Error del servidor: " + error);
        }
    });
}

function addControlTrace() {
    var efectividad = false;
    if ($("#efectividad").is(':checked')) { efectividad = true; }
    var controlTraceVM = {
        ControlID: $("#controlMedidaID").val(),
        CtrlReplaceID: $("#controlTraceID").val(),
        MaintCauseID: $("#maintCauseID").val(),
        AplicacionID: $("#txtIntervenID").val(),
        RiesgoID: $("#txtRiesgoID").val(),
        DateSigue: $("#FechaFinal").val(),
        Efectividad: efectividad,
        Observations: $("#observations").val(),
        OrganizationID: 1,
        ClientID: 1,
        UserID: 1
    };

    $.ajax({
        type: "POST",
        url: "/Riesgos/AddControlTrace",
        data: JSON.stringify(controlTraceVM),
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (result) {
            if (result.success) {
                var actionTraceID = $("#actionTraceID").val();
                if (actionTraceID) {
                    var mainCause = $("#maintCauseID").val();
                    var fechaSolicitud = $("#FechaFinal").val();
                    var description = "Falto seguimiento a la medida de intervención de riesgos";
                    var fechaCierre = $("#FechaFinal").val();
                    var responsable = $("#idRespons").val();
                    $("#FechaSolicitud").val(fechaSolicitud);
                    $("#idFuente").val(mainCause);
                    $("#idDescrip").val(description);
                    $("#idEficaciaAnt").val("4");
                    $("#idEficaciaDesp").val("4");
                    $("#FechaCierre").val(fechaCierre);
                    $("#idEfectiva").val(false);
                    $("#txtActionCategory").val("1");
                    $("#idTrabajador").val(responsable);
                    $("#txtInfoAct").val("");
                    AddAccion();
                };
                alert("Seguimiento adicionado !!");
                location.reload();
            } else {
                alert("Error: " + result.message);
            }
            $(".tabAddControlTrace").css("display", "none");
        },
        error: function (xhr, status, error) {
            alert("Error del servidor: " + error);
        }
    });
}

function showControlTraceAll(id) {
    // Mostrar todos los seguimientos
    $('#btnSigue').hide();
    $.ajax({
        url: "/Riesgos/GetSigueIntervencionesAll",
        data: { id: id },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            var html = '';
            response.forEach(function (item, index) {
                var sigueDate = moment(item.Fecha);
                var formattedDate = sigueDate.format('YYYY-MM-DD');
                html += '<tr>';
                html += '<td>' + item.MedidaAnt + '</td>';
                html += '<td>' + item.MedidaAct + '</td>';
                html += '<td>' + item.Causa + '</td>';
                html += '<td style="white-space: nowrap;">' + formattedDate + '</td>';
                html += '<td>' + item.Efectividad + '</td>';
                html += '<td>' + item.Observaciones + '</td>';
                html += '</tr>';
            });
            $('.showControlTrace').html(html);
            $('.showControlTrace').focus();
            $(".tabShowControlTrace").css("display", "block");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}
function aceptabilidadRiesgo() {
    var nd = $('#updDeficiencia').val();
    var ne = $('#updExposicion').val();
    var nc = $('#updConsecuencia').val();
    var np = nd * ne;
    var riesgo = np * nc;
    $('#riesgo').val(riesgo);
    var colorStyle = " ";
    var aceptability = "";
    switch (true) {
        case (riesgo >= 600):
            colorStyle = "red";
            aceptability = 1;
            break;

        case (riesgo >= 150 && riesgo < 600):
            colorStyle = "orange";
            aceptability = 2;
            break;

        case (riesgo >= 40 && riesgo < 150):
            colorStyle = "yellow";
            aceptability = 3;
            break;

        case (riesgo < 40):
            colorStyle = "green";
            aceptability = 4;
            break;

        default:
            colorStyle = "green";
            aceptability = 4;
            break;
    }

    $('#updAceptabilidad').val(aceptability);
    $('#updAceptabilidad').css({ "backgroundColor": colorStyle, "font-size": "100%", "color": "white" });
}
