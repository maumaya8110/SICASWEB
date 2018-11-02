function fncCurrency(value) {
    return accounting.formatMoney(value);
};

function RenderCustomCheckBox(value, extra) {
    var schecked = (value == true) ? 'checked' : '';
    return '<input type="checkbox" ' + schecked + ' ' + extra + '></input>';
}

function RenderNotNullDate(value) {
    var dt = new Date(parseInt(value.substr(6), 10));
    var mdt = moment(dt, "DD/MM/YYYY");
    var mindte = moment('01/01/2001', "DD/MM/YYYY");
    if (mdt > mindte)
        return '<span>' + mdt.format('DD/MM/YYYY') + '</span>';
}

function SetDateTimeControl(e, h) {
    var datos = e.data;
    var celda = $(this);
    celda.empty();
    var dte = $("<div class='input-group date' id='dte_" + datos.id + "'><input type='text' class='form-control' /><span class='input-group-addon'><span class='glyphicon glyphicon-calendar'></span></span></div>");
    dte.appendTo(celda);
    dte.datetimepicker({ defaultDate: new Date(), format: 'DD/MM/YYYY' }).on('dp.change', Consultar);
}

function SetDateTimePicker(ctrl, startDate) {
    ctrl.datetimepicker({
        language: 'en',
        setStartDate: new Date(),
        todayBtn: 1,
        autoclose: 1,
        todayHighlight: 1,
        startView: 2,
        minView: 2,
        forceParse: 0,
        useCurrent: false,
        format: 'dd/mm/yyyy'
    });
}

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
}

function RenderOpcionesEnCombo(cmb, lista) {
    cmb.empty();
    $.each(lista, function (idx, obj) {
        var o = $('<option value="' + obj.id + '" ' + obj.selected + '>' + obj.descripcion + '</option>');
        o.data('item', obj);
        o.appendTo(cmb);
    });
    if (lista.length == 1)
        cmb.attr('disabled', 'disabled');
    cmb.select2();
};

function RenderOpcionesEnComboWithEvents(cmb, lista, fncChange, trigger) {
    cmb.empty();
    $.each(lista, function (idx, obj) {
        var o = $('<option value="' + obj.id + '" ' + obj.selected + '>' + obj.descripcion + '</option>');
        o.data('item', obj);
        o.appendTo(cmb);
    });
    if (lista.length == 1)
        cmb.attr('disabled', 'disabled');
    cmb.select2();
    if (fncChange != null) {
        cmb.on('change', fncChange);
        if (trigger == true)
            cmb.trigger('change');
    }
};

function RenderOpcionesEnComboOptionsGroupWithEvents(cmb, lista, fncChange, trigger) {
    cmb.empty();
    var optgroup;
    var empresa = null;
    $.each(lista, function (idx, obj) {
        if (empresa == null || empresa.id != obj.empresa.id) {
            empresa = obj.empresa;
            optgroup = $('<optgroup label="' + empresa.descripcion + '"></optgroup>');
            optgroup.appendTo(cmb);
        }
        var o = $('<option value="' + obj.id + '" ' + obj.selected + '>' + obj.descripcion + '</option>');
        o.data('item', obj);
        o.appendTo(optgroup);
    });
    if (lista.length == 1)
        cmb.attr('disabled', 'disabled');
    cmb.select2();
    if (fncChange != null) {
        cmb.on('change', fncChange);
        if (trigger == true)
            cmb.trigger('change');
    }
};

function JSONToCSVConvertor(JSONData, ReportTitle, ShowLabel) {
    var arrData = typeof JSONData != 'object' ? JSON.parse(JSONData) : JSONData;

    var CSV = '';
    //CSV += ReportTitle + '\r\n\n';

    if (ShowLabel) {
        var row = "";

        for (var index in arrData[0]) {
            row += index + ',';
        }
        row = row.slice(0, -1);
        CSV += row + '\r\n';
    }

    for (var i = 0; i < arrData.length; i++) {
        var row = "";
        for (var index in arrData[i]) {
            row += '"' + arrData[i][index] + '",';
        }
        row.slice(0, row.length - 1);
        CSV += row + '\r\n';
    }

    if (CSV == '') {
        alert("Datos Inválidos");
        return;
    }

    var fileName = "SICAS_";
    fileName += ReportTitle.replace(/ /g, "_");

    var uri = 'data:text/csv;charset=utf-8,' + escape(CSV);

    var link = document.createElement("a");
    link.href = uri;
    link.style = "visibility:hidden";
    link.download = fileName + ".csv";
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
};