Public Class Globales

    Public Shared Property SMTPServidor As String = ""
    Public Shared Property SMTPPuerto As String = ""
    Public Shared Property SMTPUsuario As String = ""
    Public Shared Property SMTPPass As String = ""
    Public Shared Property SMTPUsarCuenta As String = ""

    Public Enum TRES_ESTADOS
        NINGUNO = -1
        FALSO = 0
        VERDADERO = 1
    End Enum

    Public Enum TIPO_PARAMETRO

        TEXTO = 1
        NUMERO_ENTERO = 2
        NUMERO_DECIMAL = 3
        FECHA = 4
        BOOLEANO = 5
        CORREOS = 6
        URL = 7
        LISTA = 8

    End Enum

    Public Enum OPERACION_ESTATUS
        NINGUNO = -1
        INACTIVO = 0
        ACTIVO = 1
    End Enum

    Public Enum USUARIO_ESTATUS

        NINGUNO = -1
        INACTIVO = 0
        ACTIVO = 1

    End Enum

    Public Enum ROL_ESTATUS
        NINGUNO = -1
        INACTIVO = 0
        ACTIVO = 1
    End Enum

    Public Enum ROLES_USUARIO
        NINGUNO = 0
        ADMIN = 1
        CLIENTE = 2
    End Enum

    Public Enum PEDIDO_ESTATUS
        NINGUNO = -1
        PENDIENTE = 0
        PROCESADO = 1
        CANCELADO = 2
    End Enum

    Public Enum PERMISOS

        NO_ASIGNADO = -1
        CONFIGURACION = 1000000
        CONFIGURACION_PARAMETROS_SISTEMA = 1010000
        CONFIGURACION_PARAMETROS_SISTEMA_CONSULTAR = 1010001
        CONFIGURACION_PARAMETROS_SISTEMA_MODIFICAR = 1010002
        CONFIGURACION_CATALOGOS = 1020000
        CONFIGURACION_CATALOGOS_SOCIOS_OPERATIVOS = 1020100
        CONFIGURACION_CATALOGOS_SOCIOS_OPERATIVOS_CONSULTAR = 1020101
        CONFIGURACION_CATALOGOS_SOCIOS_OPERATIVOS_AGREGAR = 1020102
        CONFIGURACION_CATALOGOS_SOCIOS_OPERATIVOS_MODIFICAR = 1020103
        CONFIGURACION_CATALOGOS_SOCIOS_OPERATIVOS_ELIMINAR = 1020104

        OPERACION = 2000000
        OPERACION_FACTURAS = 2010000
        OPERACION_FACTURAS_CONSULTAR = 2010001
        OPERACION_FACTURAS_AGREGAR = 2010002
        OPERACION_FACTURAS_MODIFICAR = 2010003
        OPERACION_FACTURAS_ELIMINAR = 2010004
        REPORTES = 3000000

    End Enum

    Public Enum TIPO_CODIGO_POSTAL

        NINGUNO = -1
        DESCONOCIDO = 0
        ORIGEN = 1
        DESTINO = 2

    End Enum

    Public Enum ZONA

        NUNGUNA = -1
        DESCONOCIDA = 0
        ZONA_1 = 1
        ZONA_2 = 2
        ZONA_3 = 3
        ZONA_4 = 4
        ZONA_5 = 5
        ZONA_6 = 6
        ZONA_7 = 7
        ZONA_8 = 8

    End Enum

End Class
