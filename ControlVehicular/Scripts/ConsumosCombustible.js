function ObtieneListaJson(params, ruta) {
    var l = [];

    $.ajax({
        type: "POST",
        url: ruta,
        cache: false,
        dataType: 'json',
        contentType: "application/json",
        async: false,
        data: JSON.stringify(params),
        success: function (data) {
            l = data;
        },
        error: function (xhr, status, errormsg) {
            debugger;
        }
    });
    return l;
};

function RenderTable(tbl, litems) {
    var cuerpo = tbl.find('tbody');
    var kminicial = 0;
    var kmFinal = 0;
    debugger;
    cuerpo.empty();
    $.each(litems, function (idx, fila) {
        var row = $('<tr></tr>');
        row.data("item", fila);
        row.on("click", function (evt, hdl) {
            evt.preventDefault();
            var fila_aux = $(this).data('item');
            alert(fila_aux.descripcion);
        });

        for (propiedad in fila) {
            var celda = $('<td>' + fila[propiedad] + '</td>');
            celda.appendTo(row);
        }
        row.appendTo(cuerpo);
    });
    //var row = $('<tr></tr>');
    //var celda = $('<td colspan="4" style="text-align:right;font-weight:bold;font-size:1.2em;">Total:</td>');
    //celda.appendTo(row);
    //celda = $('<td style="font-weight:bold;font-size:1.2em;">' + accounting.formatMoney(total_req) + '</td>');
    //celda.appendTo(row);
    //celda = $('<td>&nbsp;</td>');
    //celda.appendTo(row);
    //row.appendTo(cuerpo);
};

function RenderOpcionesEnCombo(cmb, lista) {
    cmb.empty();
    $.each(lista, function (idx, obj) {
        var o = $('<option value="' + obj.id + '" ' + obj.selected + '>' + obj.descripcion + '</option>');
        o.data('item', obj);
        o.appendTo(cmb);
    });
    if (lista.length == 1)
        cmb.attr('disabled', 'disabled');
    cmb.chosen();
};