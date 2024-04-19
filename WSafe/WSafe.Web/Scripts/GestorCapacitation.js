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
