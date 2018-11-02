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

function RenderOpcionesEnComboWithEvents(cmb, lista, fncChange, trigger) {
    cmb.empty();
    $.each(lista, function (idx, obj) {
        var o = $('<option value="' + obj.id + '" ' + obj.selected + '>' + obj.descripcion + '</option>');
        o.data('item', obj);
        o.appendTo(cmb);
    });
    if (lista.length == 1)
        cmb.attr('disabled', 'disabled');

    cmb.chosen();

    if (fncChange != null) {
        cmb.on('change', fncChange);

        if (trigger == true)
            cmb.trigger('change');
    }
};

function RenderTable(tbl, litems, fncUpdateCmbs, fncGetModel, urlDelete) {
    var cuerpo = tbl.find('tbody');
    var total_req = 0;
    cuerpo.empty();
    $.each(litems, function (idx, fila) {
        if (fila.id == 0)
            fila.id = idx + 1;
        var row = $('<tr></tr>');
        var celda = $('<td>' + (idx + 1) + '</td>');
        celda.appendTo(row);
        celda = $('<td>' + fila.cantidad + '</td>');
        celda.appendTo(row);
        celda = $('<td style="text-align:left">' + fila.item.descripcion + '</td>');
        celda.appendTo(row);
        celda = $('<td>' + accounting.formatMoney(fila.item.preciocompra) + '</td>');
        celda.appendTo(row);
        total_req = total_req + fila.monto;
        celda = $('<td>' + accounting.formatMoney(fila.monto) + '</td>');
        celda.appendTo(row);
        var btn = $('<span class="glyphicon glyphicon-remove btnEliminar" title="Quitar" style="cursor:pointer;"></span>');
        btn.data('fila', fila);
        btn.data('urlElimina', urlDelete);
        btn.data('fncGetModelo', fncGetModel);
        btn.data('fncBuscaItem', BuscaItem);
        btn.on('click', EliminaFila);
        celda = $('<td></td>');
        btn.appendTo(celda);
        celda.appendTo(row);
        row.appendTo(cuerpo);
    });
    var row = $('<tr></tr>');
    var celda = $('<td colspan="4" style="text-align:right;font-weight:bold;font-size:1.2em;">Total:</td>');
    celda.appendTo(row);
    celda = $('<td style="font-weight:bold;font-size:1.2em;">' + accounting.formatMoney(total_req) + '</td>');
    celda.appendTo(row);
    celda = $('<td>&nbsp;</td>');
    celda.appendTo(row);
    row.appendTo(cuerpo);
    if (litems != undefined)
        fncUpdateCmbs(litems.length);
};

function RenderSolicitudMaterialModal(div, solicitud) {
    var dBody = div.find('#divModalBody');
    var total_req = 0;
    dBody.empty();
    var sbody = '<div class="row">';
    sbody = sbody + '<div class="col-sm-12">';
    sbody = sbody + '<div class="panel panel-default">';
    sbody = sbody + '<div class="panel-body">';
    sbody = sbody + '<form role="form" class="form-horizontal">';

    sbody = sbody + '<div class="form-group">';
    sbody = sbody + '<div class="col-sm-6"><span>Estatus:</span><span style="font-weight:bold;"> ' + solicitud.Estatus.descripcion + '</span></div>';
    sbody = sbody + '<div class="col-sm-6"><span>Fecha:</span><span style="font-weight:bold;"> ' + solicitud.FechaElabora + '</span></div>';
    sbody = sbody + '</div>';

    sbody = sbody + '<div class="form-group">';
    sbody = sbody + '<div class="col-sm-6"><span>División:</span><span style="font-weight:bold;"> ' + solicitud.Division.descripcion + '</span></div>';
    sbody = sbody + '<div class="col-sm-6"><span>Empresa:</span><span style="font-weight:bold;"> ' + solicitud.Empresa.descripcion + '</span></div>';
    sbody = sbody + '</div>';

    sbody = sbody + '<div class="form-group">';
    sbody = sbody + '<div class="col-sm-6"><span>Departamento:</span><span style="font-weight:bold;"> ' + solicitud.Departamento.descripcion + '</span></div>';
    sbody = sbody + '<div class="col-sm-6"><span>Almacen:</span><span style="font-weight:bold;"> ' + solicitud.Almacen.descripcion + '</span></div>';
    sbody = sbody + '</div>';
    sbody = sbody + '</form>';

    sbody = sbody + '<ul class="nav nav-pills">';
    sbody = sbody + '<li class="active"><a data-toggle="pill" href="#solicitud">Artículos</a></li>';
    sbody = sbody + '<li><a data-toggle="pill" href="#soportes">Soportes</a></li>';
    sbody = sbody + '<li><a data-toggle="pill" href="#comentarios">Comentarios</a></li>';
    sbody = sbody + '</ul>';

    sbody = sbody + '<div class="tab-content">';

    //Articulos Solicitud
    sbody = sbody + '<div id="solicitud" class="tab-pane fade in active">';
    sbody = sbody + '<div class="table-responsive">';

    sbody = sbody + '<table class="table table-striped table-hover" id="tblDetalle">';
    sbody = sbody + '<thead>';
    sbody = sbody + '<tr style="font-weight:bold; font-size: 1.2em;">';
    sbody = sbody + '<td>#</td>';
    sbody = sbody + '<td>Cantidad</td>';
    sbody = sbody + '<td>Descripción</td>';
    sbody = sbody + '<td title="Precio Unitario">P.U.</td>';
    sbody = sbody + '<td>Monto</td>';
    sbody = sbody + '</tr>';
    sbody = sbody + '</thead>';
    sbody = sbody + '<tbody>';

    var contador = 1;
    $.each(solicitud.articulos, function (idx, fila) {
        total_req = total_req + fila.monto;
        sbody = sbody + '<tr><td>' + contador + '</td><td>' + fila.cantidad + '</td><td>' + fila.item.descripcion + '</td><td style="text-align:right;">' + accounting.formatMoney(fila.item.preciocompra) + '</td><td style="text-align:right;">' + accounting.formatMoney(fila.monto) + '</td></tr>';
        contador++;
    });

    sbody = sbody + '<tr><td colspan="4" style="text-align:right;font-weight:bold;font-size:1.2em;">Total:</td><td  style="text-align:right;font-weight:bold;font-size:1.2em;">' + accounting.formatMoney(total_req) + '</td></tr>';

    sbody = sbody + '</tbody>';
    sbody = sbody + '</table>';
    sbody = sbody + '</div>';
    sbody = sbody + '</div>';

    //Soportes Solicitud
    sbody = sbody + '<div id="soportes" class="tab-pane">';
    sbody = sbody + '<table id="tblSoportes" class="table table-striped table-hover">';
    sbody = sbody + '<thead>';
    sbody = sbody + '<tr><td>#</td><td>Descripción</td><td>Tipo</td><td>Ruta</td><td>Req.</td><td></td></tr>';
    sbody = sbody + '</thead>';
    sbody = sbody + '<tbody>';
    sbody = sbody + '<tr><td>1</td><td>Evaluación Física</td><td>PDF</td><td><span class="glyphicon glyphicon-search" title="Ver" style="cursor:pointer;"></span></td></tr>';
    sbody = sbody + '<tr><td>2</td><td>Convenio</td><td>PDF</td><td><span class="glyphicon glyphicon-search" title="Ver" style="cursor:pointer;"></span></td></tr>';
    sbody = sbody + '<tr><td>3</td><td>Factura</td><td>XML</td><td><span class="glyphicon glyphicon-search" title="Ver" style="cursor:pointer;"></span></td></tr>';
    sbody = sbody + '</tbody>';
    sbody = sbody + '</table>';
    sbody = sbody + '</div>';

    //Comentarios
    sbody = sbody + '<div id="comentarios" class="tab-pane">';
    sbody = sbody + '<form role="form" class="form-horizontal">';
    sbody = sbody + '<div class="form-group" style="text-align:center;">';
    sbody = sbody + '<label class="control-label col-sm-1" for="txtAddComentario">Comentario:</label>';
    sbody = sbody + '<div class="col-sm-9">';
    sbody = sbody + '<input type="text" class="form-control" id="txtAddComentario" value="" placeholder="Introduzca un comentario..." />';
    sbody = sbody + '</div>';
    sbody = sbody + '<div class="col-sm-2">';
    sbody = sbody + '<button class="btn btn-toolbar" title="Agregar Comentario" id="btnAddComment" onclick="AddComment(event)"><span class="glyphicon glyphicon-plus"></span></button>';
    sbody = sbody + '</div>';
    sbody = sbody + '</div>';
    sbody = sbody + '<div class="form-group">';
    var scomentarios = solicitud.Comentarios == undefined ? '' : solicitud.Comentarios;
    sbody = sbody + '<textarea id="txtComentariosSolicitud" style="width:100%; height:100%" readonly>' + scomentarios + '</textarea>';
    sbody = sbody + '</div>';
    sbody = sbody + '</form>';
    sbody = sbody + '</div>';

    sbody = sbody + '</div>';

    sbody = sbody + '</div>';
    sbody = sbody + '</div>';
    sbody = sbody + '</div>';
    sbody = sbody + '</div>';

    $(sbody).appendTo(dBody);

    div.data('solicitud', solicitud);
    div.modal('show');
};

function RenderReqModalAutoriza(div, solicitud) {
    var dBody = div.find('#divModalBody');
    var total_req = 0;
    dBody.empty();
    var sbody = '<div class="row">';
    sbody = sbody + '<div class="col-sm-12">';
    sbody = sbody + '<div class="panel panel-default">';
    sbody = sbody + '<div class="panel-body">';
    sbody = sbody + '<form role="form" class="form-horizontal">';

    sbody = sbody + '<div class="form-group">';
    sbody = sbody + '<div class="col-sm-6"><span>Estatus:</span><span style="font-weight:bold;background-color:yellow;">' + solicitud.Estatus.descripcion + '</span></div>';
    sbody = sbody + '<div class="col-sm-6"><span>Fecha:</span><span style="font-weight:bold;"> ' + solicitud.FechaElabora + '</span></div>';
    sbody = sbody + '</div>';

    sbody = sbody + '<div class="form-group">';
    sbody = sbody + '<div class="col-sm-6"><span>División:</span><span style="font-weight:bold;"> ' + solicitud.Division.descripcion + '</span></div>';
    sbody = sbody + '<div class="col-sm-6"><span>Empresa:</span><span style="font-weight:bold;"> ' + solicitud.Empresa.descripcion + '</span></div>';
    sbody = sbody + '</div>';

    sbody = sbody + '<div class="form-group">';
    sbody = sbody + '<div class="col-sm-6"><span>Departamento:</span><span style="font-weight:bold;"> ' + solicitud.Departamento.descripcion + '</span></div>';
    sbody = sbody + '<div class="col-sm-6"><span>Almacen:</span><span style="font-weight:bold;"> ' + solicitud.Almacen.descripcion + '</span></div>';
    sbody = sbody + '</div>';
    sbody = sbody + '</form>';

    sbody = sbody + '<ul class="nav nav-pills">';
    sbody = sbody + '<li class="active"><a data-toggle="pill" href="#solicitud">Artículos</a></li>';
    sbody = sbody + '<li><a data-toggle="pill" href="#soportes">Soportes</a></li>';
    sbody = sbody + '<li><a data-toggle="pill" href="#comentarios">Comentarios</a></li>';
    sbody = sbody + '</ul>';

    sbody = sbody + '<div class="tab-content">';

    //Articulos Solicitud
    sbody = sbody + '<div id="solicitud" class="tab-pane fade in active">';
    sbody = sbody + '<div class="table-responsive">';
    sbody = sbody + '<table class="table table-striped table-hover" id="tblDetalle">';
    sbody = sbody + '<thead>';
    sbody = sbody + '<tr style="font-weight:bold; font-size: 1.2em;">';
    sbody = sbody + '<td>#</td>';
    sbody = sbody + '<td>Cantidad</td>';
    sbody = sbody + '<td>Descripción</td>';
    sbody = sbody + '<td title="Precio Unitario">P.U.</td>';
    sbody = sbody + '<td>Monto</td>';
    sbody = sbody + '</tr>';
    sbody = sbody + '</thead>';
    sbody = sbody + '<tbody>';

    var contador = 1;
    $.each(solicitud.articulos, function (idx, fila) {
        total_req = total_req + fila.monto;
        sbody = sbody + '<tr><td>' + contador + '</td><td>' + fila.cantidad + '</td><td>' + fila.item.descripcion + '</td><td style="text-align:right;">' + accounting.formatMoney(fila.item.preciocompra) + '</td><td style="text-align:right;">' + accounting.formatMoney(fila.monto) + '</td></tr>';
        contador++;
    });

    sbody = sbody + '<tr><td colspan="4" style="text-align:right;font-weight:bold;font-size:1.2em;">Total:</td><td  style="text-align:right;font-weight:bold;font-size:1.2em;">' + accounting.formatMoney(total_req) + '</td></tr>';

    sbody = sbody + '</tbody>';
    sbody = sbody + '</table>';
    sbody = sbody + '</div>';
    sbody = sbody + '</div>';
    
    //Soportes Solicitud
    sbody = sbody + '<div id="soportes" class="tab-pane">';
    sbody = sbody + '<table id="tblSoportes" class="table table-striped table-hover">';
    sbody = sbody + '<thead>';
    sbody = sbody + '<tr><td>#</td><td>Descripción</td><td>Tipo</td><td>Ruta</td><td>Req.</td><td></td></tr>';
    sbody = sbody + '</thead>';
    sbody = sbody + '<tbody>';
    sbody = sbody + '<tr><td>1</td><td>Evaluación Física</td><td>PDF</td><td><span class="glyphicon glyphicon-search" title="Ver" style="cursor:pointer;"></span></td></tr>';
    sbody = sbody + '<tr><td>2</td><td>Convenio</td><td>PDF</td><td><span class="glyphicon glyphicon-search" title="Ver" style="cursor:pointer;"></span></td></tr>';
    sbody = sbody + '<tr><td>3</td><td>Factura</td><td>XML</td><td><span class="glyphicon glyphicon-search" title="Ver" style="cursor:pointer;"></span></td></tr>';
    sbody = sbody + '</tbody>';
    sbody = sbody + '</table>';
    sbody = sbody + '</div>';

    //Comentarios
    sbody = sbody + '<div id="comentarios" class="tab-pane">';
    sbody = sbody + '<form role="form" class="form-horizontal">';
    sbody = sbody + '<div class="form-group" style="text-align:center;">';
    sbody = sbody + '<label class="control-label col-sm-1" for="txtAddComentario">Comentario:</label>';
    sbody = sbody + '<div class="col-sm-9">';
    sbody = sbody + '<input type="text" class="form-control" id="txtAddComentario" value="" placeholder="Introduzca un comentario..." />';
    sbody = sbody + '</div>';
    sbody = sbody + '<div class="col-sm-2">';
    sbody = sbody + '<button class="btn btn-toolbar" title="Agregar Comentario" id="btnAddComment" onclick="AddComment(event)"><span class="glyphicon glyphicon-plus"></span></button>';
    sbody = sbody + '</div>';
    sbody = sbody + '</div>';
    sbody = sbody + '<div class="form-group">';
    var scomentarios = solicitud.Comentarios == undefined ? '' : solicitud.Comentarios;
    sbody = sbody + '<textarea id="txtComentariosSolicitud" style="width:100%; height:100%" readonly>' + scomentarios + '</textarea>';
    sbody = sbody + '</div>';
    sbody = sbody + '</form>';
    sbody = sbody + '</div>';

    sbody = sbody + '<div class="tab-content">'; //tab-content

    sbody = sbody + '</div>';

    sbody = sbody + '</div>';
    sbody = sbody + '</div>';
    sbody = sbody + '</div>';
    sbody = sbody + '</div>';
    $(sbody).appendTo(dBody);

    div.data('solicitud', solicitud);
    div.modal('show');
};

function BuscaItem(lista, item) {
    var indice = -1;
    $.each(lista, function (idx, obj) {
        if (obj.id == item.id)
            indice = idx;
    });
    return indice;
};

function ExisteArticulo(lista, item) {
    var indice = -1;

    $.each(lista, function (idx, obj) {
        if (obj.item.id == item.id)
            indice = idx;
    });
    return indice;
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

function EliminaFila(evt, hdl) {
    evt.preventDefault();
    var fila_aux = $(this).data('fila');
    var fncGetModelo = $(this).data('fncGetModelo');
    var fncBuscaItem = $(this).data('fncBuscaItem');
    var UrlElimina = $(this).data('urlElimina');
    var modelo = fncGetModelo();
    var indice = fncBuscaItem(modelo.articulos, fila_aux);
    
    if (indice > -1) {
        if (modelo.id > 0) {
            debugger;
            var params = { idSolicitud: modelo.id, idx: modelo.articulos[indice].id };
            $.ajax({
                type: "POST",
                url: UrlElimina,
                cache: false,
                dataType: 'json',
                contentType: "application/json",
                async: false,
                data: JSON.stringify(params),
                success: function (data) {
                    if (data)
                        modelo.articulos.splice(indice, 1);
                },
                error: function (xhr, status, errormsg) {
                    alert(errormsg);
                }
            });
        }
        else
            modelo.articulos.splice(indice, 1);
    }
    RenderTable($("#tblDetalle"), modelo.articulos, EnableDisableCmbs, fncGetModelo);
};

function Valida(modelo) {
    if (modelo.Division == undefined || modelo.Division.id <= 0) {
        alert("Es necesario seleccionar una Divisíon");
        return false;
    }
    if (modelo.Empresa == undefined || modelo.Empresa.id <= 0) {
        alert("Es necesario seleccionar una Empresa");
        return false;
    }
    if (modelo.Departamento == undefined || modelo.Departamento.id <= 0) {
        alert("Es necesario seleccionar un Departamento");
        return false;
    }
    if (modelo.Almacen == undefined || modelo.Almacen.id <= 0) {
        alert("Es necesario seleccionar un Almacen");
        return false;
    }
    if (modelo.articulos == undefined || modelo.articulos.length <= 0) {
        alert("Debe agregar por lo menos un Artículo / Servicio a la Solicitud");
        return false;
    }
    return true;
};

function CancelarSolicitud(ruta, fncGetModelo) {
    var modelo = fncGetModelo();
    if (modelo.id <= 0) {
        alert("No es posible cancelar la Solicitud, ya que aún no se ha guardado");
        return;
    }
    if (!Valida(modelo)) { return; }
    var r = confirm("Confirma la cancelación de la Solicitud con Folio " + modelo.id + "?");
    if (r == true) {
        var params = { solicitud_id: modelo.id, estatus_id: 3 };
        if (modelo.id > 0) {
            $.ajax({
                type: "POST",
                url: ruta,
                cache: false,
                dataType: 'json',
                contentType: "application/json",
                async: false,
                data: JSON.stringify(params),
                success: function (data) {
                    alert("Solicitud Cancelada con éxito");
                    location.href = rutaHome;
                },
                error: function (xhr, status, errormsg) {
                    alert(errormsg);
                }
            });
        }
    }
};