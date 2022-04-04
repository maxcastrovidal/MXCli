function IdTipoProducto_onchange() {

    

    IdTipoProducto = document.getElementById('IdTipoProducto').value;

    if (IdTipoProducto.value == '') { return }

    $.ajax({
        type: 'POST',
        url: '/Productos/ListaProductos',
        dataType: 'json',
        data: { IdTipoProducto: IdTipoProducto },
        success: function (data) {
            var select = "Descripción Producto *"
            select += "<select name = 'IdProducto' class='form-control' > ";
            select += "<option value=''>(Seleccionar)</option>";

            $.each(data, function (i, item) {
                select += "<option value=" + item.Id + ">" + item.Nombre + "</option>";
            })
            select += "</select>"
            var div = document.getElementById('DivIdProducto')
            div.innerHTML = select
        },
        error: function (ex) {
            var r = jQuery.parseJSON(response.responseText);
            alert("Message: " + r.Message);
            alert("StackTrace: " + r.StackTrace);
            alert("ExceptionType: " + r.ExceptionType);
        }
    });

    return;
};