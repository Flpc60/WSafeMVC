// funciones gráficas y auditorías
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
                    labels: ["I. PLANEAR", "II. HACER", "III. VERIFICAR", "IV. ACTUAR"],
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
    var auditChapter = $('#auditChapter').val();
    $('.auditContainer').empty();
    $.ajax({
        url: '/Audits/GetQuestionsChapter',
        type: 'GET',
        data: { chapter: auditChapter },
        success: function (response) {
            var model = []; // Crear el modelo aquí
            response.forEach(function (item, index) {
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
                    '<select class="requisite-select">' +
                    '<option value="NC">No cumple</option>' +
                    '<option value="CP">Cumple parcialmente</option>' +
                    '<option value="CYD">Cumple y documenta</option>' +
                    '</select>' + '<hr />';

                $('.auditContainer').append(html);
            });

            // Almacena el modelo en un atributo de datos para usarlo posteriormente
            $('.auditContainer').data('model', model);
            $("#saveAuditedResult").show();
        }
    });
}

function auditedSave() {

    $('#saveAuditedResult').click(function () {
        $(this).prop('disabled', true);
        // Obtiene el modelo desde el atributo de datos
        var model = $('.auditContainer').data('model');

        // Actualiza el valor de Result en el modelo
        $('.requisite-select').each(function (index) {
            var selectedValue = $(this).find('option:selected').val();
            model[index].Result = selectedValue;
            if (selectedValue == "NC" || selectedValue == "CP") {
                $("#txtNoCumple").prop('checked', true);
            }
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
                $('.auditContainer').empty();
                $('.auditContainer').css('display', 'none');
                alert(response.message);
            }
        });
        $(this).prop('disabled', false);
    });
}

function asistenceAuditedResult() {
    $("#asistentAudited").hide();
    var auditChapterValue = $('#auditChapter').val(); // Almacena el valor de #auditChapter
    var auditProcessValue = $('#auditProcess').val(); // Almacena el valor de #auditProcess

    $.ajax({
        url: '/Audits/GetAsistenceAuditedResult',
        type: 'GET',
        data: {
            chapter: auditChapterValue,
            process: auditProcessValue,
        },
        success: function (response) {
            var model = []; // Crear el modelo aquí
            response.forEach(function (item, index) {
                var auditItemID = item.ID;
                var name = index + 1 + ". " + item.Name + " ?:";
                var respuesta = {
                    ID: 0,
                    AuditID: $('#auditID').val(),
                    AuditItemID: auditItemID, // Utiliza el valor de AuditItemID obtenido
                    Result: item.Result,
                    AuditItem: {},
                    Process: $('#auditProcess').val(),
                    AuditChapter: $('#auditChapter').val()
                };
                model.push(respuesta);

                var html = '<div>' + name + '</div>' +
                    '<select class="requisite">' +
                    '<option value="0" '  + (item.Result == 0 ? 'selected' : '') + '>NC</option>' +
                    '<option value="1" '  + (item.Result == 1 ? 'selected' : '') + '>CP</option>' +
                    '<option value="2" '  + (item.Result == 2 ? 'selected' : '') + '>CYD</option>' +
                    '</select>' + '<hr />';

                $('.auditContainer').append(html);
            });

            // Almacena el modelo en un atributo de datos para usarlo posteriormente
            $('.auditContainer').data('model', model);
        }
    });
}
