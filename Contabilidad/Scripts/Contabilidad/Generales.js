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
            alert(errormsg);
        }
    });
    return l;
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

function ConvertToDate(value) {
    value = value.replace('/Date(', '').replace(')/', '');
    var dvalue = new Date(parseInt(value));
    return dvalue;
}

var toType = function (obj) {
    return ({}).toString.call(obj).match(/\s([a-z|A-Z]+)/)[1].toLowerCase()
}