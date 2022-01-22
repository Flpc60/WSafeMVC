    
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