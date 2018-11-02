Imports System
Imports System.Data


Public Class Usuario

    Public Enum USUARIO_ESTATUS
        ACTIVO = 1
        INACTIVO = 0
        BAJA = -1000
    End Enum

Dim _UsuarioID As Integer = -1
Dim _Login As String = ""
Dim _Nombre As String = ""
Dim _Password As String = ""
Dim _RolID As Integer = -1
Dim _FechaCreacion As Date = #1/1/1900#
Dim _UsuarioCreadorID As Integer = -1
Dim _FechaModificacion As Date = #1/1/1900#
Dim _UsuarioModificadorID As Integer = -1
Dim _Estatus As Integer = -1


Public Sub New()
'Nada
End Sub

Public Sub New(ByVal Registro as DataRow)
_UsuarioID = IIf(ISDBNull(Registro!UsuarioID), Nothing, Registro!UsuarioID)
_Login = IIf(ISDBNull(Registro!Login), Nothing, Registro!Login)
_Nombre = IIf(ISDBNull(Registro!Nombre), Nothing, Registro!Nombre)
_Password = IIf(ISDBNull(Registro!Password), Nothing, Registro!Password)
_RolID = IIf(ISDBNull(Registro!RolID), Nothing, Registro!RolID)
_FechaCreacion = IIf(ISDBNull(Registro!FechaCreacion), Nothing, Registro!FechaCreacion)
_UsuarioCreadorID = IIf(ISDBNull(Registro!UsuarioCreadorID), Nothing, Registro!UsuarioCreadorID)
_FechaModificacion = IIf(ISDBNull(Registro!FechaModificacion), Nothing, Registro!FechaModificacion)
_UsuarioModificadorID = IIf(ISDBNull(Registro!UsuarioModificadorID), Nothing, Registro!UsuarioModificadorID)
_Estatus = IIf(ISDBNull(Registro!Estatus), Nothing, Registro!Estatus)

End Sub

Public Property UsuarioID As Integer
Get
return _UsuarioID
End Get
Set(ByVal value As Integer)
_UsuarioID = value
End Set
End Property

Public Property Login As String
Get
return _Login
End Get
Set(ByVal value As String)
_Login = value
End Set
End Property

Public Property Nombre As String
Get
return _Nombre
End Get
Set(ByVal value As String)
_Nombre = value
End Set
End Property

Public Property Password As String
Get
return _Password
End Get
Set(ByVal value As String)
_Password = value
End Set
End Property

Public Property RolID As Integer
Get
return _RolID
End Get
Set(ByVal value As Integer)
_RolID = value
End Set
End Property

Public Property FechaCreacion As Date
Get
return _FechaCreacion
End Get
Set(ByVal value As Date)
_FechaCreacion = value
End Set
End Property

Public Property UsuarioCreadorID As Integer
Get
return _UsuarioCreadorID
End Get
Set(ByVal value As Integer)
_UsuarioCreadorID = value
End Set
End Property

Public Property FechaModificacion As Date
Get
return _FechaModificacion
End Get
Set(ByVal value As Date)
_FechaModificacion = value
End Set
End Property

Public Property UsuarioModificadorID As Integer
Get
return _UsuarioModificadorID
End Get
Set(ByVal value As Integer)
_UsuarioModificadorID = value
End Set
End Property

Public Property Estatus As Integer
Get
return _Estatus
End Get
Set(ByVal value As Integer)
_Estatus = value
End Set
End Property



End Class


