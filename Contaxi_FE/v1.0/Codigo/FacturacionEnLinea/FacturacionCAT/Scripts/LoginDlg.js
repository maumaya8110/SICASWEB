$(function () {

    var $formLogin = $('#login-form');
    var $formLost = $('#lost-form');
    var $formRegister = $('#register-form');
    var $divForms = $('#div-forms');
    var $modalAnimateTime = 300;
    var $msgAnimateTime = 150;
    var $msgShowTime = 2000;

    $("form").submit(function () {
        switch (this.id) {
            case "login-form":
                var $lg_username = $('#login_username').val();
                var $lg_password = $('#login_password').val();

                if (!IsValidEmail($lg_username)) {
                    msgChange($('#div-login-msg'), $('#icon-login-msg'), $('#text-login-msg'), "error", "glyphicon-remove", "Favor de verificar el e-mail");
                } else {
                    IsValidUser($lg_username, $lg_password);
                }
                return false;
                break;
            case "lost-form":
                var $ls_email = $('#lost_email').val();
                if (!IsValidEmail($ls_email)) {
                    msgChange($('#div-lost-msg'), $('#icon-lost-msg'), $('#text-lost-msg'), "error", "glyphicon-remove", "Favor de verificar el e-mail.");
                } else {
                    SendChgPwd($ls_email);
                }
                return false;
                break;
            case "register-form":
                var $rg_email = $('#register_email').val();
                var $rg_password = $('#register_password').val();
                RegistroOK($rg_email, $rg_password)
                return false;
                break;
            default:
                return false;
        }
        return false;
    });

    $('#login_register_btn').click(function () { modalAnimate($formLogin, $formRegister) });
    $('#register_login_btn').click(function () { modalAnimate($formRegister, $formLogin); });
    $('#login_lost_btn').click(function () { modalAnimate($formLogin, $formLost); });
    $('#lost_login_btn').click(function () { modalAnimate($formLost, $formLogin); });
    $('#lost_register_btn').click(function () { modalAnimate($formLost, $formRegister); });
    $('#register_lost_btn').click(function () { modalAnimate($formRegister, $formLost); });

    function modalAnimate($oldForm, $newForm) {
        var $oldH = $oldForm.height();
        var $newH = $newForm.height();
        $divForms.css("height", $oldH);
        $oldForm.fadeToggle($modalAnimateTime, function () {
            $divForms.animate({ height: $newH }, $modalAnimateTime, function () {
                $newForm.fadeToggle($modalAnimateTime);
            });
        });
    }

    function msgFade($msgId, $msgText) {
        $msgId.fadeOut($msgAnimateTime, function () {
            $(this).text($msgText).fadeIn($msgAnimateTime);
        });
    }

    function msgChange($divTag, $iconTag, $textTag, $divClass, $iconClass, $msgText) {
        var $msgOld = $divTag.text();
        msgFade($textTag, $msgText);
        $divTag.addClass($divClass);
        $iconTag.removeClass("glyphicon-chevron-right");
        $iconTag.addClass($iconClass + " " + $divClass);
        setTimeout(function () {
            msgFade($textTag, $msgOld);
            $divTag.removeClass($divClass);
            $iconTag.addClass("glyphicon-chevron-right");
            $iconTag.removeClass($iconClass + " " + $divClass);
        }, $msgShowTime);
    }

    function IsValidEmail(email) {
        var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        return regex.test(email);
    }

    function RegistroOK(pemail, ppwd) {
        $.ajax({
            type: "POST",
            url: rutaRegistra,
            cache: false,
            dataType: 'json',
            data: { email: pemail, password: ppwd },
            success: function (data) {
                var r = data;
                if (r.indexOf('OK')<=0) {
                    msgChange($('#div-register-msg'), $('#icon-register-msg'), $('#text-register-msg'), "error", "glyphicon-remove", "Error en Registro");
                } else {
                    msgChange($('#div-register-msg'), $('#icon-register-msg'), $('#text-register-msg'), "success", "glyphicon-ok", "Registro OK");
                    alert("En breve recibirá un correo electrónico para validar y confirmar su registro.");
                    $('#register_login_btn').trigger('click');
                }
            },
            error: function (xhr, status, errormsg) {
                alert(errormsg);
            }
        });
    }

    function IsValidUser(usuario, pwd) {
        $.ajax({
            type: "POST",
            url: rutaIngresa,
            cache: false,
            dataType: 'json',
            data: { username: usuario, pass: pwd },
            success: function (data) {
                var r = data;

                if (!r.Resultado) {
                    msgChange($('#div-login-msg'), $('#icon-login-msg'), $('#text-login-msg'), "error", "glyphicon-remove", data.Mensaje);
                }
                else {
                    msgChange($('#div-login-msg'), $('#icon-login-msg'), $('#text-login-msg'), "success", "glyphicon-ok", "Bienvenido");
                    window.location = r.Valor;
                }
            },
            error: function (xhr, status, errormsg) {
                alert(errormsg);
            }
        });
    }

    function SendChgPwd(param_email) {
        $.ajax({
            type: "POST",
            url: rutaRecupera,
            cache: false,
            dataType: 'json',
            data: { email: param_email },
            success: function (data) {
                var r = data;
                if (r.Resultado) {
                    msgChange($('#div-lost-msg'), $('#icon-lost-msg'), $('#text-lost-msg'), "success", "glyphicon-ok", r.Mensaje);
                    var delay = 500;
                    setTimeout(function () {
                        window.location = r.Valor;
                    }, delay);
                    
                }
                else {
                    msgChange($('#div-lost-msg'), $('#icon-lost-msg'), $('#text-lost-msg'), "error", "glyphicon-remove", r.Mensaje);
                }
            },
            error: function (xhr, status, errormsg) {
                alert(errormsg);
            }
        });
    }
});