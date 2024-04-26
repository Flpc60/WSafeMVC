function buildTableRow(header, values) {
    let rowHtml = '<tr>';
    rowHtml += `<td style="white-space: nowrap;">${header}</td>`;
    values.forEach(value => {
        let result;
        // Verificar si el denominador es cero
        if (header === "% Eficacia" || header === "% Eficicencia" || header === "% Efectividad") {
            result = (value.denominator !== 0) ? (value.numerator / value.denominator) * 100 : 0;
            result = result.toFixed(0) + "%"; // Redondear a 2 decimales y agregar el símbolo de porcentaje
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
            let html = '';
            response.forEach(function (item, index) {
                html += buildTableRow("Capacitaciones Programadas", [{ numerator: item.P1, denominator: 1 }, { numerator: item.P2, denominator: 1 }, { numerator: item.P3, denominator: 1 }, { numerator: item.P4, denominator: 1 }, { numerator: item.P5, denominator: 1 }, { numerator: item.P6, denominator: 1 }, { numerator: item.P7, denominator: 1 }, { numerator: item.P8, denominator: 1 }, { numerator: item.P9, denominator: 1 }, { numerator: item.P10, denominator: 1 }, { numerator: item.P11, denominator: 1 }, { numerator: item.P12, denominator: 1 }]);
                html += buildTableRow("Capacitaciones Ejecutadas", [{ numerator: item.E1, denominator: 1 }, { numerator: item.E2, denominator: 1 }, { numerator: item.E3, denominator: 1 }, { numerator: item.E4, denominator: 1 }, { numerator: item.E5, denominator: 1 }, { numerator: item.E6, denominator: 1 }, { numerator: item.E7, denominator: 1 }, { numerator: item.E8, denominator: 1 }, { numerator: item.E9, denominator: 1 }, { numerator: item.E10, denominator: 1 }, { numerator: item.E11, denominator: 1 }, { numerator: item.E12, denominator: 1 }]);
                html += buildTableRow("% Eficacia", [{ numerator: item.E1, denominator: item.P1 }, { numerator: item.E2, denominator: item.P2 }, { numerator: item.E3, denominator: item.P3 }, { numerator: item.E4, denominator: item.P4 }, { numerator: item.E5, denominator: item.P5 }, { numerator: item.E6, denominator: item.P6 }, { numerator: item.E7, denominator: item.P7 }, { numerator: item.E8, denominator: item.P8 }, { numerator: item.E9, denominator: item.P9 }, { numerator: item.E10, denominator: item.P10 }, { numerator: item.E11, denominator: item.P11 }, { numerator: item.E12, denominator: item.P12 }]);
                html += buildTableRow("% Eficicencia", [{ numerator: item.C1, denominator: item.Cc1 }, { numerator: item.C2, denominator: item.Cc2 }, { numerator: item.C3, denominator: item.Cc3 }, { numerator: item.C4, denominator: item.Cc4 }, { numerator: item.C5, denominator: item.Cc5 }, { numerator: item.C6, denominator: item.Cc6 }, { numerator: item.C7, denominator: item.Cc7 }, { numerator: item.C8, denominator: item.Cc8 }, { numerator: item.C9, denominator: item.Cc9 }, { numerator: item.C10, denominator: item.Cc10 }, { numerator: item.C11, denominator: item.Cc11 }, { numerator: item.C12, denominator: item.Cc12 }]);
                html += buildTableRow("% Efectividad", [{ numerator: item.Tc1, denominator: item.C1 }, { numerator: item.Tc2, denominator: item.C2 }, { numerator: item.Tc3, denominator: item.C3 }, { numerator: item.Tc4, denominator: item.C4 }, { numerator: item.Tc5, denominator: item.C5 }, { numerator: item.Tc6, denominator: item.C6 }, { numerator: item.Tc7, denominator: item.C7 }, { numerator: item.Tc8, denominator: item.C8 }, { numerator: item.Tc9, denominator: item.C9 }, { numerator: item.Tc10, denominator: item.C10 }, { numerator: item.Tc11, denominator: item.C11 }, { numerator: item.Tc12, denominator: item.C12 }]);
            });
            $('.cronogramaBody').html(html);
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

            var legend = "% DE EFICACIA";
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
                        text: 'CAPACITACIONES ' + legend,
                        color: backgroundColor
                    }
                }
            });

            legend = "% DE EFICIENCIA";
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
                        text: 'CAPACITACIONES ' + legend,
                        color: backgroundColor
                    }
                }
            });

            legend = "% DE EFECTIVIDAD";
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
                        text: 'CAPACITACIONES ' + legend,
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
                    '<td><a href="#" onclick="return getSigueCapacitation(' + item.ID + ')">Editar</a> | <a href = "#" onclick = "DeleteSigueCapacitation(' + item.ID + ')"> Borrar</a></td>';
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

function getSigueOccupational(id) {
    $.ajax({
        async: true,
        type: 'GET',
        url: "/Occupationals/UpdateSigueOccupational",
        data: {
            id: id
        },
        dataType: "json",
        contentType: "application/json;charset=UTF-8",
        success: function (response) {
            var sigueDate = moment(response.SigueDate);
            var formattedDate = sigueDate.format('YYYY-MM-DD');

            $("#dateSigue").val(formattedDate);
            $("#workerID").val(response.TrabajadorID);
            $("#resultado").val(response.Resultado);
            $("#recomendations").val(response.Recomendations);
            $("#sigueOccupationalID").val(response.ID);
            $("#btnAddTraceability").hide();
            $("#btnUpdTraceability").show();
            $("#btnCanTraceability").show();
            $(".tabAddSigue").css("display", "block");
            $("#dateSigue").focus();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function addSigueOccupational(id) {
    // Crea una nueva trazabilidad
    $('.tabAddSigue').css("display", "none");
    var sigueOccupational = {
        ID: "0",
        SigueDate: $("#dateSigue").val(),
        Resultado: $("#resultado").val(),
        Recomendations: $("#recomendations").val(),
        OccupationalID: id,
        TrabajadorID: $("#workerID").val(),
        OrganizationID: 1,
        ClientID: 1,
        UserID: 1
    };
    $.ajax({
        type: "POST",
        url: "/Occupationals/CreateSigueOccupational",
        data: { model: sigueOccupational },
        dataType: "json",
        success: function (response) {
            $("#btnAddTraceability").hide();
            $("#btnUpdTraceability").hide();
            $(".tabAddSigue").css("display", "none");
            alert(response.mensaj);
            ShowSigueOccupationals(id);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function updateSigueOccupational(id) {
    // Actualiza una trazabilidad
    $('.tabAddSigue').css("display", "none");
    var sigueOccupational = {
        ID: $("#sigueOccupationalID").val(),
        SigueDate: $("#dateSigue").val(),
        Resultado: $("#resultado").val(),
        Recomendations: $("#recomendations").val(),
        OccupationalID: id,
        TrabajadorID: $("#workerID").val(),
        OrganizationID: 1,
        ClientID: 1,
        UserID: 1
    };
    $.ajax({
        type: "POST",
        url: "/Occupationals/UpdateSigueOccupational",
        data: { model: sigueOccupational },
        dataType: "json",
        success: function (response) {
            $(".tabAddSigue").css("display", "none");
            alert(response.mensaj);
            ShowSigueOccupationals(response.data);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function cancelSigueOccupational() {
    $(".tabAddSigue").css("display", "none");
    $("#dateSigue").val("");
    $("#resultado").val("");
    $("#recomendations").val("");
}

function DeleteSigueOccupational(id) {
    $.ajax({
        url: "/Occupationals/DeleteSigueOccupational/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        async: true,                                             // si es asincrónico o no
        success: function (result) {
            var sigueDate = moment(result.SigueDate);
            var formattedDate = sigueDate.format('YYYY-MM-DD');

            var text = "";
            text += "Esta seguro de querer borrar este seguimiento ?:\n\n";
            text += "Fecha: " + formattedDate + "\n";
            text += "Resultado: " + result.Resultado + "\n";
            text += "Recomendaciones: " + result.Recomendations + "\n";
            var respuesta = confirm(text);
            if (respuesta == true) {
                $.ajax({
                    url: "/Occupationals/DeleteSigueOccupational/" + id,
                    type: "POST",
                    contentType: "application/json;charset=UTF-8",
                    dataType: "json",
                    async: true,                                               // si es asincrónico o no
                    success: function (response) {
                        $(".tabAddSigue").css("display", "none");
                        alert(response.mensaj);
                        ShowSigueOccupationals(response.data);
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

function deleteOccupational(id) {
    $.ajax({
        url: "/Occupationals/DeleteOccupational/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        async: true,                                               // si es asincrónico o no
        success: function (result) {
            if (result.data == false) {
                alert(result.error);
            } else {
                var text = "";
                text += "Esta seguro de querer borrar esta evaluación ocupacional ?:\n\n";
                text += "Fecha: " + formatDate(result.data.ExaminationDate) + "\n";
                text += "Recomendación: " + result.data.Recomendations + "\n";
                text += "Peso: " + result.data.Peso + "\n";
                text += "Talla: " + result.data.Talla + "\n";
                var respuesta = confirm(text);

                if (respuesta == true) {
                    $.ajax({
                        url: "/Occupationals/DeleteOccupational/" + id,
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
            $("#btnAddTraceability").hide();
            $("#btnUpdTraceability").hide();
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
    var siguePlanAnualVM = {
        ID: $("#siguePlanID").val(),
        DateSigue: $("#dateSigue").val(),
        TrabajadorID: $("#sigueWorkerID").val(),
        StateActivity: $("#stateActivity").val(),
        StateCronogram: $("#stateCronogram").val(),
        Programed: $("#programed").val(),
        Executed: $("#executed").val(),
        PlanActivityID: planActivityID,
        Observation: $("#observation").val(),
        FileName: $("#fileName").val(),
        ActionCategory: $("#actionCategory").val(),
        OrganizationID: 1,
        ClientID: 1,
        UserID: 1
    };
    $.ajax({
        type: "POST",
        url: "/AnnualPlans/UpdateSiguePlanAnual",
        data: { model: siguePlanAnualVM },
        dataType: "json",
        success: function (response) {
            alert(response.mensaj);
            $(".tabAnnualPlan").css("display", "block");
            ShowAnnualPlan(planActivityID);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}
