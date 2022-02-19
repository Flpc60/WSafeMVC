    
$('#categoria').change(function () {
    var selectedCategoria = $("#categoria").val();
    var peligroSelect = $('#peligro');
    peligroSelect.empty();
    if (selectedCategoria != null && selectedCategoria != '')
    {
        $.getJSON('@Url.Action("GetPeligros")', { ID: selectedCategoria }, function (peligros)
        {
            if (peligros != null && !jQuery.isEmptyObject(peligros))
            {
                peligroSelect.append($('<option />',
                {
                    value: null,
                    text: ""
                }));

                $.each(peligros, function (index, item)
                {
                    peligroSelect.append($('<option />',
                    {
                        value: item.Value,
                        text: item.Text
                    }));
                });
            };
        });
    }
});

$('#exposicionSelected').click(function () {
    var selectedExposicion = $('#exposicionSelected').val();
    alert("NE :" + selectedExposicion);
    var ne = 0;
    switch (selectedExposicion)
    {
        case 1:
            ne = 4;
            break;

        case 2:
            ne = 3;
            break;

        case 3:
            ne = 2;
            break;

        default:
            ne = 1;
            break;
    }

$('#exposicion').val(ne);
    var nd = $('#deficiencia').val();
    $('#probabilidad').val(nd * ne);
    var probabilidad = $('#probabilidad').val();
    var interpretaNP = " ";
    switch (probabilidad) {
        case int p when(p >= 24):
            interpretaNP = "Muy alto (MA)";
            break;

        case int p when(p >= 10 && p < 24):
            interpretaNP = "Alto (A)";
            break;

        case int p when(p >= 8 && p < 10):
            interpretaNP = "Mdio (M)";
            break;

        default:
            interpretaNP = "Bajo (B)";
            break;
    }

$('#interpretaNP').val(interpretaNP);

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
});


$('#categoria').change(function () {
    var selectedCategoria = $("#categoria").val();
    var peligroSelect = $('#peligro');
    peligroSelect.empty();
    if (selectedCategoria != null && selectedCategoria != '') {
        $.getJSON('@Url.Action("GetPeligros")', { ID: selectedCategoria }, function (peligros) {
            if (peligros != null && !jQuery.isEmptyObject(peligros)) {
                $.each(peligros, function (index, item) {
                    peligroSelect.append($('<option />',
                        {
                            value: item.Value,
                            text: item.Text
                        }));
                });
            };
        });
    }
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

function calcularProbabilidad() {
    var nd = $('#deficiencia').val();
    var ne = $('#exposicion').val();
    var probabilidad = nd * ne;
    $('#probabilidad').val(probabilidad);
    var interpretaNP = " ";
    switch (true) {
        case (probabilidad >= 24 && probabilidad <= 40):
            interpretaNP = "Muy alto (MA)";
            break;
        case (probabilidad >= 10 && probabilidad < 24):
            interpretaNP = "Alto (A)";
            break;
        case (probabilidad >= 8 && probabilidad < 10):
            interpretaNP = "Mdio (M)";
            break;
        case (probabilidad >= 2 && probabilidad < 8):
            interpretaNP = "Bajo (B)";
            break;

        default:
            interpretaNP = "Bajo (B)";
            break;
    }
    $('#interpretaNP').val(interpretaNP);
}

function calcularRiesgo() {
    calcularProbabilidad();
    var np = $('#probabilidad').val();
    var nc = $('#consecuencia').val();
    var riesgo = np * nc;
    $('#riesgo').val(riesgo);
    var interpretaNR = " ";
    var colorStyle = " ";
    switch (true) {
        case (riesgo >= 600):
            interpretaNR = "I";
            colorStyle = "red";
            break;
        case (riesgo >= 150 && riesgo < 600):
            interpretaNR = "II";
            colorStyle = "yellow";
            break;
        case (riesgo >= 40 && riesgo < 150):
            interpretaNR = "III";
            colorStyle = "orange";
            break;

        default:
            interpretaNR = "IV";
            colorStyle = "green";
            break;
    }
    $('#interpretaNR').val(interpretaNR);
    $('#interpretaNR').css({ "backgroundColor": colorStyle, "font-size": "200%" });
}

$('#activity').change(function () {
    var activitySelect = $('#activity').val();
    activitySelect.empty();
    $.getJSON('@Url.Action("GetActivities")', function (activities) {
        if (activities != null && !jQuery.isEmptyObject(activities)) {
            $.each(activities, function (index, item) {
                activitySelect.append($('<option />',
                    {
                        value: item.Value,
                        text: item.Text
                    }));
            });
        };
    });
});

$('#tarea').change(function () {
    var tareasSelect = $('#tarea').val();
    tareasSelect.empty();
    $.getJSON('@Url.Action("GetTareas")', function (tareas) {
        if (tareas != null && !jQuery.isEmptyObject(tareas)) {
            $.each(tareas, function (index, item) {
                tareasSelect.append($('<option />',
                    {
                        value: item.Value,
                        text: item.Text
                    }));
            });
        };
    });
});


$(document).ready(function () {
    $("#consultarInte").click(function () {
        $.ajax({
            type: "GET",                                              // tipo de request que estamos generando
            url: 'GeIntervenciones',                                  // URL al que vamos a hacer el pedido
            data: { ID: @Model.ID
    },                                  // data es un arreglo JSON que contiene los parámetros que
        contentType: "application/json; charset=utf-8",           // tipo de contenido
        dataType: "json",                                         // formato de transmición de datos
        async: true,                                              // si es asincrónico o no
        success: function (intervencion) {                        // función que va a ejecutar si el pedido fue exitoso
            if (intervencion != null && !jQuery.isEmptyObject(intervencion)) {
                $.each(intervencion, function (i, datos) {
                    var consulta = "";
                    consulta += '<tr>';
                    consulta += '<td>' + datos.Nombre + '</td>';
                    consulta += '<td>' + datos.CategoriaAplicacion + '</td>';
                    consulta += '<td>' + datos.Finalidad + '</td>';
                    consulta += '<td>' + datos.Intervencion + '</td>';
                    consulta += '<td>' + datos.Beneficios + '</td>';
                    consulta += '<td>' + datos.Presupuesto + '</td>';
                    consulta += '<td>' + datos.Trabajador + '</td>';
                    consulta += '<td>' + datos.FechaInicial + '</td>';
                    consulta += '<td>' + datos.Fechafinal + '</td>';
                    consulta += '<td>' + datos.Observaciones + '</td>';
                    consulta += '</tr>';
                    $("#listaInterven").append(consulta);
                });

                $(".consInterven").css("display", "block");
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) { // función que va a ejecutar si hubo algún tipo de error en el pedido
                var error = eval("(" + XMLHttpRequest.responseText + ")");
                aler(error.Message);
            }
        });
});
        });

$(document).ready(function () {
    $("#idPeligros").click(function () {
        $(".tabPeligros").css("display", "block");
    });

    $("#evalRiesgos").click(function () {
        $(".tabEvalRiesgos").css("display", "block");
    });

    $("#mediAplica").click(function () {
        $(".tabMediAplica").css("display", "block");
        $(".tabInterven").css("display", "block");
    });
});

$('#zona').change(function () {
    var zonasSelect = $('#zona').val();
    zonasSelect.empty();
    $.getJSON('@Url.Action("GetZonas")', function (zonas) {
        if (zonas != null && !jQuery.isEmptyObject(zonas)) {
            $.each(zonas, function (index, item) {
                zonasSelect.append($('<option />',
                    {
                        value: item.Value,
                        text: item.Text
                    }));
            });
        };
    });
});

$('#proceso').change(function () {
    var procesosSelect = $('#proceso').val();
    procesosSelect.empty();
    $.getJSON('@Url.Action("GetProcesos")', function (procesos) {
        if (procesos != null && !jQuery.isEmptyObject(procesos)) {
            $.each(procesos, function (index, item) {
                procesosSelect.append($('<option />',
                    {
                        value: item.Value,
                        text: item.Text
                    }));
            });
        };
    });
});

$('#activity').change(function () {
    var activitySelect = $('#activity').val();
    activitySelect.empty();
    $.getJSON('@Url.Action("GetActivities")', function (activities) {
        if (activities != null && !jQuery.isEmptyObject(activities)) {
            $.each(activities, function (index, item) {
                activitySelect.append($('<option />',
                    {
                        value: item.Value,
                        text: item.Text
                    }));
            });
        };
    });
});

$('#tarea').change(function () {
    var tareasSelect = $('#tarea').val();
    tareasSelect.empty();
    $.getJSON('@Url.Action("GetTareas")', function (tareas) {
        if (tareas != null && !jQuery.isEmptyObject(tareas)) {
            $.each(tareas, function (index, item) {
                tareasSelect.append($('<option />',
                    {
                        value: item.Value,
                        text: item.Text
                    }));
            });
        };
    });
});

$('#categoria').change(function () {
    var selectedCategoria = $("#categoria").val();
    var peligroSelect = $('#peligro');
    peligroSelect.empty();
    if (selectedCategoria != null && selectedCategoria != '') {
        $.getJSON('@Url.Action("GetPeligros")', { ID: selectedCategoria }, function (peligros) {
            if (peligros != null && !jQuery.isEmptyObject(peligros)) {
                $.each(peligros, function (index, item) {
                    peligroSelect.append($('<option />',
                        {
                            value: item.Value,
                            text: item.Text
                        }));
                });
            };
        });
    }
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

function calcularProbabilidad() {
    var nd = $('#deficiencia').val();
    var ne = $('#exposicion').val();
    var probabilidad = nd * ne;
    $('#probabilidad').val(probabilidad);
    var interpretaNP = " ";
    switch (true) {
        case (probabilidad >= 24 && probabilidad <= 40):
            interpretaNP = "Muy alto (MA)";
            break;
        case (probabilidad >= 10 && probabilidad < 24):
            interpretaNP = "Alto (A)";
            break;
        case (probabilidad >= 8 && probabilidad < 10):
            interpretaNP = "Mdio (M)";
            break;
        case (probabilidad >= 2 && probabilidad < 8):
            interpretaNP = "Bajo (B)";
            break;

        default:
            interpretaNP = "Bajo (B)";
            break;
    }
    $('#interpretaNP').val(interpretaNP);
}

function calcularRiesgo() {
    calcularProbabilidad();
    var np = $('#probabilidad').val();
    var nc = $('#consecuencia').val();
    var riesgo = np * nc;
    $('#riesgo').val(riesgo);
    var interpretaNR = " ";
    var colorStyle = " ";
    switch (true) {
        case (riesgo >= 600):
            interpretaNR = "I";
            colorStyle = "red";
            break;
        case (riesgo >= 150 && riesgo < 600):
            interpretaNR = "II";
            colorStyle = "yellow";
            break;
        case (riesgo >= 40 && riesgo < 150):
            interpretaNR = "III";
            colorStyle = "orange";
            break;

        default:
            interpretaNR = "IV";
            colorStyle = "green";
            break;
    }
    $('#interpretaNR').val(interpretaNR);
    $('#interpretaNR').css({ "backgroundColor": colorStyle, "font-size": "200%" });
}

$(document).ready(function () {
    $("#addInterven").click(function () {
        $(".tabMediAplica").css("display", "none");
        var model = @Model.Intervenciones
        model.Add(new AplicacionVM(
            {
                RiesgoID: model.RiesgoID,
                Nombre: $("#idNombre").val(),
                CategoriaAplicacion: $("#idCatApli").val(),
                Finalidad: $("#idFinal").val(),
                TrabajadorID: $("#idRespons").val(),
                Intervencion: $("#idInterven").val(),
                Presupuesto: $("#idPresup").val(),
                FechaInicial: $("#idFechaIni").val(),
                FechaFinal: $("#idFechaFin").val(),
                Observaciones: $("#idObserv").val()
            }));
    });
});
