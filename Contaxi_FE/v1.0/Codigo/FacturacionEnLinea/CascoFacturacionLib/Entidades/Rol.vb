Imports System
Imports System.Data


Public Class Rol

Dim _RolID As Integer = -1
Dim _Nombre As String = ""
Dim _Descripcion As String = ""
Dim _FechaCreacion As Date = #1/1/1900#
Dim _UsuarioCreadorID As Integer = -1
Dim _FechaModificacion As Date = #1/1/1900#
Dim _UsuarioModificadorID As Integer = -1
Dim _Estatus As Integer = -1


Public Sub New()
'Nada
End Sub

Public Sub New(ByVal Registro as DataRow)
_RolID = IIf(ISDBNull(Registro!RolID), Nothing, Registro!RolID)
_Nombre = IIf(ISDBNull(Registro!Nombre), Nothing, Registro!Nombre)
_Descripcion = IIf(ISDBNull(Registro!Descripcion), Nothing, Registro!Descripcion)
_FechaCreacion = IIf(ISDBNull(Registro!FechaCreacion), Nothing, Registro!FechaCreacion)
_UsuarioCreadorID = IIf(ISDBNull(Registro!UsuarioCreadorID), Nothing, Registro!UsuarioCreadorID)
_FechaModificacion = IIf(ISDBNull(Registro!FechaModificacion), Nothing, Registro!FechaModificacion)
_UsuarioModificadorID = IIf(ISDBNull(Registro!UsuarioModificadorID), Nothing, Registro!UsuarioModificadorID)
_Estatus = IIf(ISDBNull(Registro!Estatus), Nothing, Registro!Estatus)

End Sub

Public Property RolID As Integer
Get
return _RolID
End Get
Set(ByVal value As Integer)
_RolID = value
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

Public Property Descripcion As String
Get
return _Descripcion
End Get
Set(ByVal value As String)
_Descripcion = value
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


