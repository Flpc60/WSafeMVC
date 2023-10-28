function auditedResult() {
    $(document).ready(function () {
        $('#auditChapter').change(function () {
            var auditChapter = $(this).val();
            $.ajax({
                url: '/Audits/GetQuestionsChapter',
                type: 'GET',
                data: { chapter: auditChapter },
                success: function (response) {
                    response.forEach(function (item) {
                        var html = '<div>' + item.Name + '</div>' +
                            '<select class="requisite" data-auditItemID="' + item.ID + '">' +
                            '<option value="NC">NC</option>' +
                            '<option value="CP">CP</option>' +
                            '<option value="CYD">CYD</option>' +
                            '</select>';
                        $('.auditContainer').append(html);
                    });
                }
            });
        });
    });
}

function auditedSave() {
    $(document).ready(function () {
        $('#saveAuditedResult').click(function () {
            var model = [];
            $('.requisite').each(function () {
                var auditItemID = $(this).data('auditItemID'); // Obtén el valor de AuditItemID del atributo data
                var selectedValue = $(this).find('option:selected').val(); // Obtén el valor del elemento <option> seleccionado
                var respuesta = {
                    ID: 0,
                    AuditID: $('#auditID').val(),
                    AuditItemID: auditItemID, // Utiliza el valor de AuditItemID obtenido
                    Result: selectedValue, // Utiliza el valor del elemento <option> seleccionado
                    AuditItem: null,
                    Process: null,
                    AuditChapter: $('#auditChapter').val()
                    // Otras propiedades que necesites enviar
                };
                model.push(respuesta);
            });

            // Enviar respuestas al servidor usando Ajax
            $.ajax({
                url: '/Audits/CreateAuditedResult',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(model),
                success: function (response) {
                    // Manejar el resultado si es necesario
                    alert(response.mensaj);
                }
            });
        });
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
                    response.forEach(function (item) {
                        var auditItemID = item.ID;
                        var html = '<div>' + item.Name + '</div>' +
                            '<input type="hidden" class="auditItemID" value="' + auditItemID + '">' +
                            '<select class="requisite">' +
                            '<option value="NC">NC</option>' +
                            '<option value="CP">CP</option>' +
                            '<option value="CYD">CYD</option>' +
                            '</select>';

                        $('.auditContainer').append(html);
                    });
                }
            });
        });
    });
}

function auditedSave() {
    $(document).ready(function () {
        $('#saveAuditedResult').click(function () {
            var model = [];
            $('.requisite').each(function () {
                var auditedItemID = $(this).siblings('.auditItemID').val(); // Obtén el valor de AuditItemID del campo oculto
                var selectedValue = $(this).find('option:selected').val(); // Obtén el valor del elemento <option> seleccionado
                var respuesta = {
                    ID: 0,
                    AuditID: $('#auditID').val(),
                    AuditItemID: auditedItemID, // Utiliza el valor de AuditItemID obtenido
                    Result: selectedValue,
                    AuditItem: null,
                    Process: null,
                    AuditChapter: $('#auditChapter').val()
                    // Otras propiedades que necesites enviar
                };
                model.push(respuesta);
            });

            // Enviar respuestas al servidor usando Ajax
            $.ajax({
                url: '/Audits/CreateAuditedResult',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(model),
                success: function (response) {
                    // Manejar el resultado si es necesario
                    alert(response.mensaj);
                }
            });
        });
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
                    response.forEach(function (item) {
                        var auditItemID = item.ID;
                        var respuesta = {
                            ID: 0,
                            AuditID: $('#auditID').val(),
                            AuditItemID: auditItemID, // Utiliza el valor de AuditItemID obtenido
                            Result: '', // Inicializa Result como una cadena vacía
                            AuditItem: {},
                            Process: $('#auditProcess').val(),
                            AuditChapter: $('#auditChapter').val()
                            // Otras propiedades que necesites enviar
                        };
                        model.push(respuesta);

                        var html = '<div>' + item.Name + '</div>' +
                            '<select class="requisite">' +
                            '<option value="NC">NC</option>' +
                            '<option value="CP">CP</option>' +
                            '<option value="CYD">CYD</option>' +
                            '</select>';

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
    $(document).ready(function () {

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
                    alert(response.mensaj);
                }
            });
        });
    });
}
