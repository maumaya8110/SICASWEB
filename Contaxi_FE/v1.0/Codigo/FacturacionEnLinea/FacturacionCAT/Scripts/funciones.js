Number.prototype.formatMoney = function (decPlaces, thouSeparator, decSeparator) {
    var n = this,
        decPlaces = isNaN(decPlaces = Math.abs(decPlaces)) ? 2 : decPlaces,
        decSeparator = decSeparator == undefined ? "." : decSeparator,
        thouSeparator = thouSeparator == undefined ? "," : thouSeparator,
        sign = n < 0 ? "-" : "",
        i = parseInt(n = Math.abs(+n || 0).toFixed(decPlaces)) + "",
        j = (j = i.length) > 3 ? j % 3 : 0;
    return sign + (j ? i.substr(0, j) + thouSeparator : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + thouSeparator) + (decPlaces ? decSeparator + Math.abs(n - i).toFixed(decPlaces).slice(2) : "");
};

function hasTime(d) {
    return !!(d.getUTCHours() || d.getUTCMinutes() || d.getUTCSeconds());
}

function zeroFill(n) {
    if ((n + '').length == 1)
        return '0' + n;

    return n;
}

function formatDate(d) {
    if (hasTime(d)) {
        var s = zeroFill(d.getDate()) + '/' + zeroFill((d.getMonth() + 1)) + '/' + d.getFullYear();
        s += ' ' + zeroFill(d.getHours()) + ':' + zeroFill(d.getMinutes()) + ':' + zeroFill(d.getSeconds());
    } else {
        var s = zeroFill(d.getDate()) + '/' + zeroFill((d.getMonth() + 1)) + '/' + d.getFullYear();
    }

    return s;
}

function formatJSONDate(jsonDate) {
    var newDate = new Date(parseInt(jsonDate.substr(6)));
    return newDate;
}

function ValidEmail(evt) {
    var ctrl = $(this);
    var txtValue = ctrl.val();
    if (txtValue.length > 0) {
        var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (!regex.test(txtValue)) {
            alert('Favor de verificar el e-mail.');
            this.focus();
            evt.preventDefault();
            return false;
        }
    }
}

function ValidRFC(evt) {
    var ctrl = $(this);
    var txtValue = ctrl.val();
    txtValue = txtValue.toUpperCase();
    if (txtValue.length > 0) {
        var regex = /^([A-ZÑ\x26]{3,4}([0-9]{2})(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1])[A-Z|\d]{3})$/;
        if (!regex.test(txtValue)) {
            alert('Favor de verificar el RFC.');
            this.focus();
            evt.preventDefault();
            return false;
        }
    }
    ctrl.text(txtValue);
}

function ValidaContrasena(str) {
    var regex = (/^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,20}$/);
    if (!regex.test(str)) {
        alert('La contraseña no cumple con las especificaciones. Favor de verificar.');
        return false;
    }
    return true;
}

function ValidaPwd(evt, hdl) {
    var pwd = $(this).val();
    var b = false;
    if ($.trim(pwd).length > 0) {
        b = ValidaContrasena($.trim(pwd));
        if (!b)
            this.focus();
    }
    return b;
}

function CampoRequerido(evt, hdl){
    if($(this).val().length == 0){
        alert("El valor de este campo es requerido");
        this.focus();
        return;
    }
    $(this).css("border-color","black");
};