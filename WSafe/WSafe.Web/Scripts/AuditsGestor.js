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
            document.getElementById("activitiesPlanProportion").innerHTML = "<strong>% EJECUCIÓN PLAN ANUAL: " + response.data.ActivitiesPlanProportion + "</strong>";
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
                        text: 'PERSONAS A CARGO'
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
        data: {
            chapter: auditChapter
        },
        success: function (response) {
            var list = []; // Crear el modelo aquí
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
                list.push(respuesta);

                var html = '<div>' + name + '</div>' +
                    '<select class="requisite-select">' +
                    '<option value="NC">No cumple</option>' +
                    '<option value="CP">Cumple parcialmente</option>' +
                    '<option value="CYD">Cumple y documenta</option>' +
                    '</select>' + '<hr />';

                $('.auditContainer').append(html);
            });

            // Almacena el modelo en un atributo de datos para usarlo posteriormente
            $('.auditContainer').data('model', list);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function auditedSave() {
    $(this).prop('disabled', true);
    // Obtiene el modelo desde el atributo de datos
    var modelo = $('.auditContainer').data('model');

    // Actualiza el valor de Result en el modelo
    $('.requisite-select').each(function (index) {
        var selectedValue = $(this).find('option:selected').val();
        if (selectedValue == "NC" || selectedValue == "CP") {
            $("#txtNoCumple").prop('checked', true);
        }
        modelo[index].Result = selectedValue;
    });

    // Enviar respuestas al servidor usando Ajax
    $.ajax({
        url: '/Audits/CreateAuditedResult',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(modelo),
        success: function (response) {
            // Manejar el resultado si es necesario
            $('.tabExecute').css('display', 'none');
            $('.auditContainer').empty();
            $('.auditContainer').css('display', 'none');
            $('#saveAuditedResult').css("display", "none");
            alert(response.message);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
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

function ShowAnnualPlan(planActivityID) {
    // Mostrar todos las actividades
    $('#btnSigue').hide();
    $.ajax({
        url: "/AnnualPlans/GetPlanActivities",
        data: { id: planActivityID },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            var html = '';
            response.forEach(function (item, index) {
                switch (item.TxtStateCronogram)
                {
                    case "PROGRAMADA":
                        var color = "yellow";
                        break;

                    case "EJECUTADA":
                        var color = "green";
                        break;

                    case "REPROGRAMADA":
                        var color = "red";
                        break;

                    default:
                        break;
                }
                html += '<tr>';
                html += '<td style="white-space: nowrap;">' + item.TextDateSigue + '</td>';
                html += '<td>' + item.Responsable + '</td>';
                html += '<td>' + item.Observation + '</td>';
                html += '<td style="background-color:' + color + ';">' + item.TxtStateCronogram + '</td>';
                html += '<td>' + item.TxtActionCategory + '</td>';
                html += '<td>' + item.Programed + '</td>';
                html += '<td>' + item.Executed + '</td>';
                html += '<td>' + item.FileName + '</td>';
                html +=
                    '<td><a href="#" onclick="return getSeguiByID(' + item.ID + ')">Editar</a> | <a href = "#" onclick = "DeleteSeguiPlan(' + item.ID + ')"> Borrar</a></td>';
                html += '<hr />';
                html += '</tr>';
            });
            $('.gestSeguimients').html(html);
            $('.gestSeguimients').focus();
            $('.tabAnnualPlan').css("display", "block");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function getSeguiByID(id) {
    $.ajax({
        async: true,
        type: 'GET',
        url: "/AnnualPlans/UpdateSiguePlanAnual",
        data: {
            id: id
        },
        dataType: "json",
        contentType: "application/json;charset=UTF-8",
        success: function (response) {
            $("#dateSigue").val(response.TextDateSigue);
            $("#workerID").val(response.TrabajadorID);
            $("#stateActivity").val(response.StateActivity);
            $("#stateCronogram").val(response.StateCronogram);
            $("#programed").val(response.Programed);
            $("#executed").val(response.Executed);
            $("#fileName").val(response.fileName);
            $("#observation").val(response.Observation);
            $("#actionCategory").val(response.ActionCategory);
            $("#siguePlanID").val(response.ID);
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

function updateSeguimient() {
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
        Fundamentos: $("#txtFundamentos").val(),
        AuditID: auditID
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

function DeleteSeguiPlan(id) {
    $.ajax({
        url: "/AnnualPlans/DeleteSiguePlanAnual/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        async: true,                                             // si es asincrónico o no
        success: function (result) {
            var text = "";
            text += "Esta seguro de querer borrar este seguimiento ?:\n\n";
            text += "Fecha: " + result.TextDateSigue + "\n";
            text += "Observación : " + result.Observation + "\n";
            text += "Programadas: " + result.Programed + "\n";
            text += "Ejecutadas: " + result.Executed + "\n";
            var respuesta = confirm(text);
            if (respuesta == true) {
                $.ajax({
                    url: "/AnnualPlans/DeleteSiguePlanAnual/" + id,
                    type: "POST",
                    contentType: "application/json;charset=UTF-8",
                    dataType: "json",
                    async: true,                                               // si es asincrónico o no
                    success: function (result) {
                        alert(result.mensaj);
                        var planActivityID = result.data;
                        ShowAnnualPlan(planActivityID);
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

function addTraceability(planActivityID) {
    // Crea una nueva trazabilidad
    $('.tabAddSigue').css("display", "none");
    var siguePlanAnual = {
        ID: "0",
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
        UserID:1
    };
    $.ajax({
        type: "POST",
        url: "/AnnualPlans/CreateTraceability",
        data: { model: siguePlanAnual },
        dataType: "json",
        success: function (response) {
            $("#siguePlanID").val(response.ID);
            $("#btnAddTraceability").hide();
            $("#btnUpdTraceability").hide();
            $(".tabAddSigue").css("display", "none");
            alert(response.mensaj);
            ShowAnnualPlan(planActivityID);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function updateTraceability(planActivityID) {
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

function cancelTrazability() {
    $(".tabGesRecomendations").css("display", "none");
    $(".tabAddRecomendations").css("display", "none");
    $("#txtRecomendation").val("");
}
function chartAnnualPlan(year) {

    // Gráficar plan anual
    $.ajax({
        url: "/AnnualPlans/AnnualPlanGraphic",
        type: "GET",
        data: { year: year },
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,
        success: function (data) {
            var arrayLabel = [];
            var arrayData1 = [];
            var arrayData2 = [];
            var backgroundColor1 = Array(data.length).fill('rgb(0, 99, 132, 0.6)');
            var backgroundColor2 = Array(data.length).fill('rgb(99, 132, 0, 0.6)');
            var programed = 0, executed = 0;

            for (var i = 0; i < data.length; i++) {
                arrayLabel.push(data[i].MesAnn);
                arrayData1.push(data[i].Resultado);
                arrayData2.push(data[i].Resultado1);
                programed = programed + data[i].Resultado;
                executed = executed + data[i].Resultado1;
            }
            var efectivity = (programed) > 0 ? executed / programed *100 : 0;
            efectivity = (efectivity).toFixed(1) + "%";
            var legend = "  => PORCENTAJE DE CUMPLIMIENTO: " + efectivity;
            const ctx = document.getElementById("annualPlanActivities").getContext('2d');

            const chart = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: arrayLabel,
                    datasets: [
                        {
                            label: "ACTIVIDADES PROGRAMADAS",
                            backgroundColor: backgroundColor1,
                            data: arrayData1
                        },
                        {
                            label: "ACTIVIDADES EJECUTADAS",
                            backgroundColor: backgroundColor2,
                            data: arrayData2
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
                                color: backgroundColor2
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
                        text: 'PLAN DE TRABAJO ANUAL AÑO: ' + year + legend,
                        color: backgroundColor2
                    }
                }
            });

            const canvasElement = document.getElementById("annualPlanActivities");
            canvasElement.tabIndex = 0;
            canvasElement.focus();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
}

function formatDate(date) {
    var dateString = parseInt(date.replace(/\/Date\((\d+)\)\//, '$1'));
    var jsDate = new Date(dateString);
    return jsDate.toLocaleDateString('es-ES');
}

function deleteActivityPlan(id) {
    $.ajax({
        url: "/AnnualPlans/DeleteActivityPlan/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        async: true,                                               // si es asincrónico o no
        success: function (result) {
            if (result.data == false) {
                alert(result.error);
            } else {
                var text = "";
                text += "Esta seguro de querer borrar esta actividad ?:\n\n";
                text += "Fecha Inicial: " + formatDate(result.data.InitialDate) + "\n";
                text += "Fecha Final: " + formatDate(result.data.FechaFinal) + "\n";
                text += "Actividad: " + result.data.Activity + "\n";
                text += "Programadas: " + result.data.Programed + "\n";
                text += "Entregables: " + result.data.Entregables + "\n";
                var respuesta = confirm(text);

                if (respuesta == true) {
                    $.ajax({
                        url: "/AnnualPlans/DeleteActivityPlan/" + id,
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

function logout() {
    $.ajax({
        type: "POST",
        url: "/Accounts/Logout",
        success: function (reponse) {
            window.location.href = "/Accounts/Login";
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.error(xhr.status + ": " + thrownError);
        }
    });
}

function ShowSigueOccupationals(id) {
    // Mostrar todos los seguimientos
    $('#btnSigue').hide();
    $.ajax({
        url: "/Occupationals/GetSigueOccupationals",
        data: { id: id },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            var html = '';
            response.forEach(function (item, index) {
                var sigueDate = moment(item.SigueDate);
                var formattedDate = sigueDate.format('YYYY-MM-DD');
                html += '<tr>';
                html += '<td style="white-space: nowrap;">' + formattedDate + '</td>';
                html += '<td>' + item.Resultado + '</td>';
                html += '<td>' + item.Recomendations + '</td>';
                html +=
                    '<td><a href="#" onclick="return getSigueOccupational(' + item.ID + ')">Editar</a> | <a href = "#" onclick = "DeleteSigueOccupational(' + item.ID + ')"> Borrar</a></td>';
                html += '<hr />';
                html += '</tr>';
            });
            $('.seguimientosBody').html(html);
            $('.seguimientosBody').focus();
            $('.tabSigueOccupational').css("display", "block");
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

