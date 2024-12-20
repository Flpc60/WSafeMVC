﻿// funcionalidad del lado del cliente

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
                            location.reload();
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
        success: function (response) {
            $('#controlMedidaID').append($('<option>', {
                value: response.id,
                text: response.name,
                selected: true
            }));
            $('#createControlModal').modal('hide');
            $("#description").val("");
            $("#beneficio").val("");
            $("#categoriaApp").val("");
            $("#finalidad").val("");
            $("#intervencion").val("");
            $("#presupuesto").val("");
            $("#intervencion").val("");
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
        success: function (response) {
            if (response.success) {
                //alert("Causa principal adicionada !!");
                $(".tabAddCause").css("display", "none");
                $("#btnAdddCause").show();
                $("#mainCause").val("");
                $('#cause1').append($('<option>', {
                    value: response.id,
                    text: response.name,
                    //selected: true
                }));
                $('#cause2').append($('<option>', {
                    value: response.id,
                    text: response.name,
                    //selected: true
                }));
                $('#cause3').append($('<option>', {
                    value: response.id,
                    text: response.name,
                    //selected: true
                }));
                $('#cause4').append($('<option>', {
                    value: response.id,
                    text: response.name,
                    //selected: true
                }));
                $('#cause5').append($('<option>', {
                    value: response.id,
                    text: response.name,
                    //selected: true
                }));

                $('#createCauseModal').modal('hide');
            } else {
                alert("Error: " + response.message);
            }
        },
        error: function (xhr, status, error) {
            alert("Error del servidor: " + error);
        }
    });
}

function addControlTrace() {
    var efectividad = false;
    var generateAction = false;
    if ($("#efectividad").is(':checked')) { efectividad = true; }
    if ($("#actionTraceID").is(':checked')) { generateAction = true; }
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
        UserID: 1,
        GenerateAction: generateAction,
        Finality: $("#finality").val(),
        TrabajadorID: $("#idRespons").val(),
        AplicationCategory: $("#txtCatAplica").val()
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

function showControlTraceAll() {
    // Mostrar todos los seguimientos
    const id = $("#txtRiesgoID").val();
    $('#btnSigue').hide();

    $.ajax({
        url: "/Riesgos/GetSigueIntervencionesAll",
        data: { id: id },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            if (response.length > 0) {
                let html = `
                <div class="table-responsive" style="background-color: azure; width:100%;">
                    <table class="table table-striped table-bordered">
                        <thead>
                            <tr style="background-color:gainsboro;">
                                <th>CONTROL ACTUAL</th>
                                <th>ANTERIOR</th>
                                <th>FECHA</th>
                                <th>EFECTIVIDAD</th>
                                <th>OBSERVACIONES</th>
                                <th>RESPONSABLE</th>
                                <th>ACCIÓN</th>
                                <th>FINALIDAD</th>
                                <th>APLICACIÓN</th>
                            </tr>
                        </thead>
                        <tbody>`;

                response.forEach(function (item) {
                    const formattedDate = moment(item.Fecha).format('YYYY-MM-DD');
                    html += `
                    <tr>
                        <td>${item.MedidaAct}</td>
                        <td>${item.MedidaAnt}</td>
                        <td style="white-space: nowrap;">${formattedDate}</td>
                        <td>${item.Efectividad}</td>
                        <td>${item.Observaciones}</td>
                        <td>${item.Responsable}</td>
                        <td>${item.GenerateAction}</td>
                        <td>${item.Finality}</td>
                        <td>${item.AplicationCategory}</td>
                    </tr>`;
                });

                html += `
                        </tbody>
                    </table>
                </div>`;

                $('.showControlTrace').html(html);
                $('.showControlTrace').focus();
            }
            if (response.length == 0) { alert("No hay seguimientos para este riesgo !!"); }
            $('#btnShowControlTrace').hide();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.error(`Error: ${xhr.status} - ${thrownError}`);
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
    var interpretaNR = " ";
    var colorStyle = " ";
    var aceptability = 0;
    var txtAceptability = "";
    switch (true) {
        case (riesgo >= 600):
            interpretaNR = "I";
            colorStyle = "red";
            aceptability = 1;
            txtAceptability = "No Aceptable";
            break;

        case (riesgo >= 150 && riesgo < 600):
            interpretaNR = "II";
            colorStyle = "orange";
            aceptability = 2;
            txtAceptability = "No Aceptable  o Aceptable con control específico";
            break;

        case (riesgo >= 40 && riesgo < 150):
            interpretaNR = "III";
            colorStyle = "yellow";
            aceptability = 3;
            txtAceptability = "Mejorable";
            break;

        case (riesgo < 40):
            interpretaNR = "IV";
            colorStyle = "green";
            aceptability = 4;
            txtAceptability = "Aceptable";
            break;

        default:
            interpretaNR = "IV";
            colorStyle = "green";
            aceptability = 4;
            txtAceptability = "Aceptable";
            break;
    }

    $('#updTxtAceptability').text(txtAceptability);
    $('#updTxtAceptability').css({ "backgroundColor": colorStyle, "font-size": "100%", "color": "white" });
    $('#updAceptabilidad').val(aceptability);
}

function validateCauses(mainCauseId) {
    var cause1 = document.getElementById("cause1").value;
    var cause2 = document.getElementById("cause2").value;
    var cause3 = document.getElementById("cause3").value;
    var cause4 = document.getElementById("cause4").value;
    var cause5 = document.getElementById("cause5").value;

    var causes = [cause1, cause2, cause3, cause4, cause5];

    // Filtra los valores vacíos y aquellos que tengan valor '0'
    var filteredCauses = causes.filter(function (value) {
        return value !== "" && value !== '0';
    });

    // Crea un set para verificar si hay duplicados
    var uniqueCauses = new Set(filteredCauses);

    if (uniqueCauses.size !== filteredCauses.length) {
        alert("Esta causa ya la seleccionaste, selecciona una nueva causa!!");
        // Resetea el valor del dropdown que generó el evento
        document.getElementById(mainCauseId).value = '0';
        return false;
    } else {
        return true;
    }
}

function filterPeligros(id) {
    var selectedCategoria = $("#categoria").val();
    var peligroSelect = $('#peligro');
    peligroSelect.empty();
    if (selectedCategoria != null && selectedCategoria != '') {
        $.ajax({
            async: true,
            type: 'GET',
            url: "/Riesgos/GetPeligros",
            data: {
                id: id
            },
            dataType: "json",
            contentType: "application/json;charset=UTF-8",
            success: function (response) {
                if (response != null && !jQuery.isEmptyObject(response)) {
                    $.each(response, function (index, item) {
                        peligroSelect.append($('<option />',
                            {
                                value: item.Value,
                                text: item.Text
                            }));
                    });
                };
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

function uploadSignature() {
    var formData = new FormData();
    var fileInput = $('#fileInput')[0].files[0];
    let responsable = false;
    if ($("#responsableSgsst").is(':checked')) { responsable = true; }

    if (fileInput) {
        formData.append('file', fileInput);
        formData.append("Name", $("#txtName").val());
        formData.append("Email", $("#txtEmail").val());
        formData.append("Password", $("#txtPassword").val());
        formData.append("responsable", responsable);

        $.ajax({
            url: '/Accounts/UploadSignature',
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            success: function (response) {
                $('#uploadStatus').html('<p style="color: green;">Firma subida con éxito.</p>');
                setTimeout(function () {
                    location.reload();
                }, 6000);
            },
            error: function () {
                $('#uploadStatus').html('<p style="color: red;">Error al subir la firma.</p>');
            }
        });
    } else {
        alert("Por favor, seleccione un archivo.");
    }
}

function addVulmerability() {
    var efectividad = false;
    var generateAction = false;
    if ($("#efectividad").is(':checked')) { efectividad = true; }
    if ($("#actionTraceID").is(':checked')) { generateAction = true; }
    var vulnerabilityVM = {
        ID:0,
        Types: $("#type").val(),
        CategoryAmenaza: $("#categoryAmenaza").val(),
        AmenazaID: $("#amenazaID").val(),
        EvaluationConceptID: $("#evaluationConceptID").val(),
        Response: $("#response").val(), 
        OrganizationID: 1,
        ClientID: 1,
        UserID: 1
    };

    $.ajax({
        type: "POST",
        url: "/Vulnerabilities/Create",
        data: JSON.stringify(vulnerabilityVM),
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (result) {
            if (result.data != false) {
                alert("Vulnerabilidad adicionada !!");
                $("#btnBlankAmenaza").focus();
                $("#vulnerabilityID").val(result.data);
            } else {
                alert("Error: " + result.mensaj);
            }
            $("#btnAddIntervention").show();
        },
        error: function (xhr, status, error) {
            alert("Error del servidor: " + error);
        }
    });
}

function showIntervencionesAll() {
    // Mostrar todos los seguimientos
    const id = $("#vulnerabilityID").val();
    $('#btnSigue').hide();

    $.ajax({
        url: "/Vulnerabilities/GetIntervencionesAll",
        data: { id: id },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            if (response.length > 0) {
                let html = `
                <div class="table-responsive" style="background-color: azure; width:100%;">
                    <table class="table table-striped table-bordered">
                        <thead>
                            <tr style="background-color:gainsboro;">
                                <th>RIESGO</th>
                                <th>CLASE</th>
                                <th>AMENAZA</th>
                                <th>APLICACIÓN</th>
                                <th>DESCRIPCIÓN</th>
                                <th>FINALIDAD</th>
                                <th>INTERVENCIÓN</th>
                                <th>BENEFICIOS</th>
                                <th>RESPONSABLE</th>
                                <th>FECHA INICIAL</th>
                                <th>FECHA FINAL</th>
                            </tr>
                        </thead>
                        <tbody>`;

                response.forEach(function (item) {
                    const initialDate = moment(item.FechaInicial).format('YYYY-MM-DD');
                    const finalDate = moment(item.FechaFinal).format('YYYY-MM-DD');
                    html += `
                    <tr>
                        <td>${item.Vulnerability}</td>
                        <td>${item.Categoria}</td>
                        <td>${item.Amenaza}</td>
                        <td>${item.Aplicacion}</td>
                        <td>${item.Description}</td>
                        <td>${item.Finalidad}</td>
                        <td>${item.Intervencion}</td>
                        <td>${item.Beneficios}</td>
                        <td>${item.Responsable}</td>
                        <td style="white-space: nowrap;">${initialDate}</td>
                        <td style="white-space: nowrap;">${finalDate}</td>
                    </tr>`;
                });

                html += `
                        </tbody>
                    </table>
                </div>`;

                $('.showIntervenciones').html(html);
                $('.showIntervenciones').focus();
            }
            if (response.length == 0) { alert("No hay medidas de intervención para este riesgo !!"); }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.error(`Error: ${xhr.status} - ${thrownError}`);
        }
    });
}
function addNewIntervention() {
    $(".tabMediAplica").css("display", "none");
    var vulnerabilityID = $("#vulnerabilityID").val();
    var aplicaVM = {
        ID: "0",
        Description: $("#description").val(),
        VulnerabilityID: vulnerabilityID,
        CategoriaAplicacion: $("#categoriaApp").val(),
        Finalidad: $("#finalidad").val(),
        Intervencion: $("#intervencion").val(),
        Beneficios: $("#beneficio").val(),
        Presupuesto: $("#presupuesto").val(),
        TrabajadorID: $("#trabajadorID").val(),
        FechaInicial: $("#FechaInicial").val(),
        FechaFinal: $("#FechaFinal").val()
    };

    $.ajax({
        type: "POST",
        url: "/Vulnerabilities/CreateIntervention",
        data: { model: aplicaVM },
        dataType: "json",
        success: function (result) {
            if (result.data != false) {
                $("#interventionID").val(result.data);
            }
            alert(result.mensaj);
            $('#createInterventionModal').modal('hide');
            showIntervencionesAll();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function addNewEvaluation() {
    var evaluationVM = {
        ID: 0,
        Name: $("#name").val(),
        VulnerabilitiType: $("#vulnerability").val(),
        EvaluationPerson: $("#evaluationPerson").val(),
        EvaluationRecurso: $("evaluationRecurso").val(),
        EvaluationSystem: $("#evaluationSystem").val(),
        ClientID: 1,
        OrganizationID: 1,
        UserID: 1
    };

    $.ajax({
        type: "POST",
        url: "/Vulnerabilities/CreateEvaluation",
        data: JSON.stringify(evaluationVM),
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (result) {
            if (result.data != false) {
                alert("Evaluación adicionada !!");
                $("#Name").val("");
                $("#VulnerabilityType").val("");
                $("#vulnerabilityID").val(result.data);
                $("#EvaluationPerson").val("");
                $("#EvaluationRecurso").val("");
                $("#EvaluationSystem").val("");
            } else {
                alert("Error: " + result.mensaj);
            }
            $("#btnaddVulmerability").hide();
            $("#btnAddIntervention").show();
        },
        error: function (xhr, status, error) {
            alert("Error del servidor: " + error);
        }
    });
}

function blankVulmerability() {
    $("#type").val("");
    $("#categoryAmenaza").val("");
    $("#vulnerabilityID").val("");
    $("#amenazaID").val("");
    $("#evaluationConceptID").val("");
    $("#response").val("");
    $("#btnaddVulmerability").show();
    $("#btnAddIntervention").show();
    $("#type").focus();
}
function filterEvaluations(id) {
    var selectedCategoria = $("#type").val();
    var evaluationSelect = $('#evaluationConceptID');
    evaluationSelect.empty();
    if (selectedCategoria != null && selectedCategoria != '') {
        $.ajax({
            async: true,
            type: 'GET',
            url: "/Vulnerabilities/GetEvaluations",
            data: {
                id: id
            },
            dataType: "json",
            contentType: "application/json;charset=UTF-8",
            success: function (response) {
                if (response != null && !jQuery.isEmptyObject(response)) {
                    $.each(response, function (index, item) {
                        evaluationSelect.append($('<option />',
                            {
                                value: item.Value,
                                text: item.Text
                            }));
                    });
                };
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}
function filterAmenazas(id) {
    var selectedCategoria = $("#categoryAmenaza").val();
    var amenazaSelect = $('#amenazaID');
    amenazaSelect.empty();
    if (selectedCategoria != null && selectedCategoria != '') {
        $.ajax({
            async: true,
            type: 'GET',
            url: "/Vulnerabilities/GetAmenazas",
            data: {
                id: id
            },
            dataType: "json",
            contentType: "application/json;charset=UTF-8",
            success: function (response) {
                if (response != null && !jQuery.isEmptyObject(response)) {
                    $.each(response, function (index, item) {
                        amenazaSelect.append($('<option />',
                            {
                                value: item.Value,
                                text: item.Text
                            }));
                    });
                };
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

function updateVulmerability() {
    const id = $("#vulnerabilityID").val();
    var vulnerabilityVM = {
        ID: id,
        Types: $("#type").val(),
        CategoryAmenaza: $("#categoryAmenaza").val(),
        AmenazaID: $("#amenazaID").val(),
        EvaluationConceptID: $("#evaluationConceptID").val(),
        Response: $("#response").val(),
        OrganizationID: 1,
        ClientID: 1,
        UserID: 1
    };

    $.ajax({
        type: "POST",
        url: "/Vulnerabilities/Edit",
        data: JSON.stringify(vulnerabilityVM),
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (result) {
            if (result.data != false) {
                alert("Vulnerabilidad actualizada !!");
                $("#btnBlankAmenaza").focus();
                $("#vulnerabilityID").val(result.data);
            } else {
                alert("Error: " + result.mensaj);
            }
            $("#btnAddIntervention").show();
        },
        error: function (xhr, status, error) {
            alert("Error del servidor: " + error);
        }
    });
}
function showConsolidateVulnerabilities(id) {

    $.ajax({
        url: "/Vulnerabilities/GetVulnerabilitiesByID",
        data: { id: id },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            let html = '';
            if (response.length > 0) {
                html += `
                <div class="table-responsive showConsolidate" tabindex="-1" style="background-color: azure; width:100%;">
                    <table class="table table-striped table-bordered">
                        <thead>
                            <tr style="background-color:gainsboro;">
                                <th>PUNTO A EVALUAR</th>`;

                let threats = new Set();
                response.forEach(function (item) {
                    Object.keys(item.Results).forEach(function (threat) {
                        threats.add(threat);
                    });
                });
                threats.forEach(function (threat) {
                    html += `<th colspan="2">${threat}</th>`;
                });

                html += `</tr><tr style="background-color:lightgray;">`;
                html += `</tr></thead><tbody>`;

                let evaluationConceptsMap = {};

                response.forEach(function (item) {
                    if (!evaluationConceptsMap[item.EvaluationConcept]) {
                        evaluationConceptsMap[item.EvaluationConcept] = {};
                    }
                    Object.keys(item.Results).forEach(function (threat) {
                        evaluationConceptsMap[item.EvaluationConcept][threat] = item.Results[threat];
                    });
                });
                Object.keys(evaluationConceptsMap).forEach(function (concept) {
                    html += `<tr><td>${concept}</td>`;

                    threats.forEach(function (threat) {
                        let color = "";
                        let resultObj = evaluationConceptsMap[concept][threat];
                        if (resultObj) {
                            let result = resultObj.Result.toFixed(2) || 'N/A';
                            let interpretation = resultObj.Interpretation || 'N/A';
                            if (interpretation == "BAJA") { color = "green"; }
                            if (interpretation == "MEDIA") { color = "yellow"; }
                            if (interpretation == "ALTA") { color = "red"; }

                            html += `<td>${result}</td><td style="background-color: ${color}; color: black;">${interpretation}</td>`;
                        } else {
                            html += `<td>N/A</td><td>N/A</td>`;
                        }
                    });

                    html += `</tr>`;
                });

                html += `</tbody></table></div>`;
                $('.showConsolidateVulnerabilities').html(html);
                $('.showConsolidate').focus();
            } else {
                alert("No hay resultados para mostrar para este riesgo!!");
            }
        },
        error: function (xhr, status, error) {
            alert("Error del servidor: " + error);
        }
    });
}
function showCalificationAmenazas() {
    return new Promise((resolve, reject) => {
        $.ajax({
            url: "/Vulnerabilities/CalificationAmenazas",
            type: "GET",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            async: true,
            success: function (response) {
                let html = '';
                if (response.length > 0) {
                    html += `
                <div class="table-responsive showConsolidate" tabindex="0" style="background-color: azure; width:100%;">
                    <table class="table table-striped table-bordered">
                        <thead>
                            <tr style="background-color:gainsboro;">
                                <th>CLASE</th>
                                <th>AMENAZA</th>
                                <th>DESCRIPCIÓN DE LA AMENAZA</th>
                                <th>ORIGEN</th>
                                <th>CALIFICACIÓN</th>
                            </tr>
                        </thead>
                        <tbody>`;

                    response.forEach(function (item) {
                        var color = "";
                        if (item.Calification == "Posible") { color = "green"; }
                        if (item.Calification == "Probable") { color = "yellow"; }
                        if (item.Calification == "Inminente") { color = "red"; }

                        html += `
                        <tr>
                            <td>${item.CategoryAmenaza}</td>
                            <td>${item.Name}</td>
                            <td>${item.Description}</td>
                            <td>${item.OrigenAmenaza}</td>
                            <td style="background-color: ${color}; color: black;">${item.Calification}</td>
                        </tr>`;
                    });

                    html += `</tbody></table></div>`;
                    $('.showCalification').html(html);
                    $('.showConsolidate').focus();

                    resolve();
                } else {
                    alert("No hay resultados para mostrar !!");
                    resolve();
                }
            },
            error: function (xhr, status, error) {
                alert("Error del servidor: " + error);
                reject(error);
            }
        });
    });
}
function showRiskLevelAmenazas() {
    return new Promise((resolve, reject) => {
        $.ajax({
            url: "/Vulnerabilities/LevelRiskAmenazas",
            type: "GET",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            async: true,
            success: function (response) {
                var color = '';
                var color1 = '';

                let html = '';
                if (response.length > 0) {
                    html += `
                <div class="table-responsive showConsolidate" tabindex="0" style="background-color: azure; width:100%;">
                    <p style= "text-align: center;font-size: 14px;">NIVELES DE RIESGO PARA AMENAZAS</P>
                    <table class="table table-striped table-bordered">
                        <thead>
                            <tr style="background-color:gainsboro;">
                                <th>CLASE</th>
                                <th>AMENAZA</th>
                                <th>CALIFICACIÓN (A)</th>
                                <th>PERSONAS (P)</th>
                                <th>RECURSOS (R)</th>
                                <th>SISTEMAS Y PROCESOS (S)</th>
                                <th>CALIFICACIÓN</th>
                            </tr>
                        </thead>
                        <tbody>`;

                    response.forEach(function (item) {
                        if (item.RiskLevelResults == "Bajo") { color = "green"; }
                        if (item.RiskLevelResults == "Medio") { color = "yellow"; }
                        if (item.RiskLevelResults == "Alto") { color = "red"; }
                        if (item.Calification == "Posible") { color1 = "green"; }
                        if (item.Calification == "Probable") { color1 = "yellow"; }
                        if (item.Calification == "Inminente") { color1 = "red"; }

                        html += `
                        <tr>
                            <td>${item.CategoryAmenaza}</td>
                            <td>${item.Name}</td>
                            <td style="background-color: ${color1}; color: black;">${item.Calification}</td>
                            <td>${item.RiskPersons}</td>
                            <td>${item.RiskResources}</td>
                            <td>${item.RiskSystems}</td>
                            <td style="background-color: ${color}; color: black;">${item.RiskLevelResults}</td>
                        </tr>`;
                    });

                    html += `</tbody></table></div>`;
                    $('.showRiskLevel').html(html);
                    $('.showConsolidate').focus();

                    resolve();
                } else {
                    alert("No hay resultados para mostrar !!");
                    resolve();
                }
            },
            error: function (xhr, status, error) {
                alert("Error del servidor: " + error);
                reject(error);
            }
        });
    });
}
function showVulnerabilitiesDetail(id) {
    return new Promise((resolve, reject) => {
        $.ajax({
            url: "/Vulnerabilities/GetVulnerabilitiesDetail",
            data: { id: id },
            type: "GET",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            async: true,
            success: function (response) {
                if (response.length > 0) {
                    let threats = new Set();
                    response.forEach(function (item) {
                        Object.keys(item.Responses).forEach(function (threat) {
                            threats.add(threat);
                        });
                    });

                    let threatArray = Array.from(threats);

                    let matrix = [];
                    let firstThreat = threatArray[0]; // La primera amenaza para generar las filas

                    response.forEach(function (item) {
                        // Crear filas solo si la respuesta es de la primera amenaza
                        if (item.Responses.hasOwnProperty(firstThreat)) {
                            // Crear una fila de la matriz para cada Name
                            let row = new Array(threatArray.length * 2 + 1).fill('');  // Añadir 1 para el Name y 2 columnas por cada amenaza
                            row[0] = item.Name;  // El Name en la primera columna
                            matrix.push(row);
                        }
                    });

                    function placeResultInRow(name, responseObj, threatIndex) {
                        // Buscar la fila donde el Name coincida
                        let targetRow = matrix.find(row => row[0] === name);

                        if (targetRow) {
                            let responsePosition = threatIndex * 2 + 1;  // Columna para la respuesta
                            let observationPosition = responsePosition + 1;  // Columna para la observación

                            // Colocar la respuesta y la observación en la fila correspondiente
                            targetRow[responsePosition] = responseObj.Response;
                            targetRow[observationPosition] = responseObj.Observation;
                        }
                    }

                    // Llenar la matriz con las respuestas y observaciones para cada amenaza, sin agregar más filas
                    threatArray.forEach(function (threat, threatIndex) {
                        response.forEach(function (item) {
                            let responseObj = item.Responses[threat];
                            if (responseObj) {
                                // Colocar el resultado correspondiente en la fila de cada Name, pero solo en la columna de la amenaza actual
                                placeResultInRow(item.Name, responseObj, threatIndex);
                            }
                        });
                    });

                    // Generar el HTML para la tabla
                    let html = `
                <div class="table-responsive showConsolidate" tabindex="-1" style="background-color: azure; width:100%;">
                    <table class="table table-striped table-bordered">
                        <thead>
                            <tr style="background-color:gainsboro;">
                                <th>PUNTO A EVALUAR</th>`;

                    // Añadir las columnas de las amenazas
                    threatArray.forEach(function (threat) {
                        html += `<th colspan="2">${threat}</th>`;
                    });

                    html += `</tr><tr style="background-color:lightgray;"><th></th>`;
                    threatArray.forEach(function () {
                        html += `<th>Rta</th><th>Observación</th>`;
                    });
                    html += `</tr></thead><tbody>`;
                    matrix.forEach(function (row) {
                        html += `<tr>`;
                        row.forEach(function (cell, index) {
                            let cellValue = cell || '';  // Si la celda está vacía, poner una celda vacía
                            if (index === 0) {  // Primera columna con los Name
                                html += `<td>${cellValue}</td>`;
                            } else {  // Celdas de respuesta y observación
                                let color = '';
                                if (index % 2 === 0) {  // Celdas de observación
                                    if (cellValue === "MALO") { color = "red"; }
                                    else if (cellValue === "REGULAR") { color = "yellow"; }
                                    else if (cellValue === "BUENO") { color = "green"; }
                                    html += `<td style="background-color: ${color}; color: black;">${cellValue}</td>`;
                                } else {  // Celdas de respuesta
                                    html += `<td>${cellValue}</td>`;
                                }
                            }
                            if (cellValue == "RESULTADO") {
                                // html += `</tr><tr style="background-color:lightgray;"><th>${cellValue.EvaluationConcept}</th><th>Rta</th><th>Observación</th><tr>`;
                            }
                        });
                        html += `</tr>`;
                    });

                    html += `</tbody></table></div>`;
                    $(`.showVulnerabilitiesDetail-${id}`).html(html);
                    $('.showConsolidate').focus();

                    resolve();
                } else {
                    alert("No hay resultados para mostrar para este riesgo!!");
                    resolve();
                }
            },
            error: function (xhr, status, error) {
                alert("Error del servidor: " + error);
                reject(error);
            }
        });
    });
}
async function showVulnerabilitiesPDF() {
    try {
        $('.showCalification').html('');
        $('.showVulnerabilitiesDetail-1').html('');
        $('.showVulnerabilitiesDetail-2').html('');
        $('.showVulnerabilitiesDetail-3').html('');
        $('.showRiskLevel').html('');

        await showCalificationAmenazas();
        await showVulnerabilitiesDetail(1);
        await showVulnerabilitiesDetail(2);
        await showVulnerabilitiesDetail(3);
        await showRiskLevelAmenazas();

        let calification = $('.showCalification').html() || "";
        let encodedCalification = encodeURIComponent(calification);

        let vulnera1 = $('.showVulnerabilitiesDetail-1').html() || "";
        let encodedVulnera1 = encodeURIComponent(vulnera1);

        let vulnera2 = $('.showVulnerabilitiesDetail-2').html() || "";
        let encodedVulnera2 = encodeURIComponent(vulnera2);

        let vulnera3 = $('.showVulnerabilitiesDetail-3').html() || "";
        let encodedVulnera3 = encodeURIComponent(vulnera3);

        let riskLevel = $('.showRiskLevel').html() || "";
        let encodedRiskLevel = encodeURIComponent(riskLevel);

        $.ajax({
            url: "/Vulnerabilities/PrintVulnerabilitiesToPdf",
            type: "POST",
            data: {
                calification: encodedCalification,
                vulnera1: encodedVulnera1,
                vulnera2: encodedVulnera2,
                vulnera3: encodedVulnera3,
                riskLevel: encodedRiskLevel
            },
            success: function (response) {
                if (response && response.fileUrl) {
                    window.open(response.fileUrl, '_blank');
                } else {
                    alert("Error: El archivo PDF no está disponible.");
                }
            },
            error: function (xhr, status, error) {
                alert("Error al generar el PDF: " + error);
            }
        });
    } catch (error) {
        alert("Error en el proceso: " + error.message);
    }
}
