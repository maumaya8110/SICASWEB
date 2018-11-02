function AddFolio()
{
    var txtFolio = $('#txtFolio');
    var sfolio = txtFolio.val();
    if (sfolio.trim().length == 0) {
        alert('Se requiere un número de Servicio');
        txtFolio.focus();
        return false;
    }

    if (isNaN(sfolio)) {
        alert('Solo se permiten números.');
        txtFolio.focus();
        return false;
    }

    var txtImporte = $('#txtImporte');
    var sImporte = txtImporte.val();
    if (sImporte.trim().length == 0) {
        alert('Se requiere un importe');
        txtImporte.focus();
        return false;
    }

    if (isNaN(sImporte)) {
        alert('Solo se permiten números.');
        txtImporte.focus();
        return false;
    }
        

    $.ajax({
        type: "POST",
        url: rutaAgregarServicio,
        cache: false,
        data: { folio: sfolio, importe : sImporte },
        success: function (data) {
            if (data.indexOf('Error') < 0) {
                txtFolio.val('');
                txtImporte.val('');
                RenderUltimaCaptura();
                txtFolio.focus();
            }
            else {
                alert(data);
                txtFolio.focus();
            }
        },
        error: function (xhr, status, errormsg) {
            alert(errormsg);
        }
    });
}

function fncGetDatosFiscalesTmp()
{
    var df = [];
    $.ajax({
        type: "POST",
        url: urlGetDatosFiscales,
        cache: false,
        dataType: 'json',
        async: false,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            df = data;
        },
        error: function (xhr, status, errormsg) {
            alert(errormsg);
        }
    });
    return df;
}

function fncTicketsUltimaFactura() {
    var lTickets = [];
    $.ajax({
        type: "POST",
        url: urlTicketsUltimaFactura,
        cache: false,
        dataType: 'json',
        async: false,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            lTickets = data;
        },
        error: function (xhr, status, errormsg) {
            alert(errormsg);
        }
    });
    return lTickets;
}

function fncGuardaDatosFiscalesTmp(datos) {
    var b = '';
    $.ajax({
        type: "POST",
        url: urlSetDatosFiscalesTmp,
        cache: false,
        dataType: 'json',
        data: JSON.stringify(datos),
        async: false,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            b = data;
        },
        error: function (xhr, status, errormsg) {
            alert(errormsg);
        }
    });
    return b;
}

function RenderUltimaCaptura() {
    var tblBody = $("#tbTickets");
    tblBody.empty();
    var tickets = fncTicketsUltimaFactura();
    if (tickets.length > 0) {
        $.each(tickets, function (i, o) {
            var tmpTR = $('<tr></tr>');

            var tmpTD = $('<td>' + o.Folio + '</td>');
            tmpTD.appendTo(tmpTR);

            tmpTD = $('<td style="text-align:center;">' + o.Fecha + '</td>');
            tmpTD.appendTo(tmpTR);

            tmpTD = $('<td>' + o.Producto + '</td>');
            tmpTD.appendTo(tmpTR);

            tmpTD = $('<td style="text-align:center;">' + o.Unidad + '</td>');
            tmpTD.appendTo(tmpTR);

            tmpTD = $('<td style="text-align:center;">' + o.MetodoPago + '</td>');
            tmpTD.appendTo(tmpTR);

            tmpTD = $('<td style="text-align:center;">' + o.CuentaBanco + '</td>');
            tmpTD.appendTo(tmpTR);

            tmpTD = $('<td style="text-align:right;">' + '$' + parseFloat(o.Precio).formatMoney(2, ',', '.') + '</td>');
            tmpTD.appendTo(tmpTR);

            tmpTD = $('<td style="text-align:centerright;">' + '$' + parseFloat(o.Importe).formatMoney(2, ',', '.') + '</td>');
            tmpTD.appendTo(tmpTR);

            tmpTD = $('<td style="text-align:center; cursor:pointer;"></td>');
            tmpInput = $('<img src="' + urlimg_close + '" alt="Quitar" title="Quita el boleto de los servicios a facturar" class="cmdEliminar"/>');
            tmpInput.data("servicio", o);
            tmpInput.appendTo(tmpTD);
            tmpTD.appendTo(tmpTR);
            tmpTR.appendTo(tblBody);
        });
        $('img.cmdEliminar').on("click", EliminarServicio);
    }
}

function SetDatosFiscales() {
    var datosF = fncGetDatosFiscalesTmp();
    if (datosF != undefined && datosF != null) {
        $('#txtColonia').val(datosF.Colonia);
        $('#txtCP').val(datosF.Cp);
        $('#txtCiudad').val(datosF.Ciudad);
        $('#txtLocalidad').val(datosF.Localidad);
        $("#cmbEstado option:contains(" + datosF.Estado + ")").attr('selected', 'selected');
        $('#txtNoExt').val(datosF.NumeroExterior);
        $('#txtNoInt').val(datosF.NumeroInterior);
        $('#txtRFC').val(datosF.RFC);
        $('#txtCalle').val(datosF.Calle);
        $('#txtRazonSocial').val(datosF.RazonSocial);
        $('#txtRazonSocial').data('token', datosF.Token);
        $('#txtEmail').val(datosF.Email);
        $('#txtEmailConfirm').val(datosF.ConfirmacionEmail);
    }
}

function EliminarServicio(evt, hdl) {
    var serv = $(evt.target).data('servicio');
    var sfolio = serv.Folio;
    $.ajax({
        type: "POST",
        url: rutaEliminarTicket,
        cache: false,
        data: { folio: sfolio },
        success: function (data) {
            if (data.indexOf('Error') < 0) {
                RenderUltimaCaptura();
            }
            else {
                alert(data);
            }
        },
        error: function (xhr, status, errormsg) {
            alert(errormsg);
        }
    });
}
