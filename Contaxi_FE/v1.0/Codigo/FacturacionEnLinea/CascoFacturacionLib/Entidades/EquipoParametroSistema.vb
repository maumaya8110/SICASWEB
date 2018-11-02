Imports System
Imports System.Data


Public Class EquipoParametroSistema

Dim _ParametroID As String = ""
Dim _Equipo As String = ""
Dim _Valor As String = ""
Dim _UsuarioModificadorID As Integer = -1
Dim _FechaModificacion As Date = #1/1/1900#


Public Sub New()
'Nada
End Sub

Public Sub New(ByVal Registro as DataRow)
_ParametroID = IIf(ISDBNull(Registro!ParametroID), Nothing, Registro!ParametroID)
_Equipo = IIf(ISDBNull(Registro!Equipo), Nothing, Registro!Equipo)
_Valor = IIf(ISDBNull(Registro!Valor), Nothing, Registro!Valor)
_UsuarioModificadorID = IIf(ISDBNull(Registro!UsuarioModificadorID), Nothing, Registro!UsuarioModificadorID)
_FechaModificacion = IIf(ISDBNull(Registro!FechaModificacion), Nothing, Registro!FechaModificacion)

End Sub

Public Property ParametroID As String
Get
return _ParametroID
End Get
Set(ByVal value As String)
_ParametroID = value
End Set
End Property

Public Property Equipo As String
Get
return _Equipo
End Get
Set(ByVal value As String)
_Equipo = value
End Set
End Property

Public Property Valor As String
Get
return _Valor
End Get
Set(ByVal value As String)
_Valor = value
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

Public Property FechaModificacion As Date
Get
return _FechaModificacion
End Get
Set(ByVal value As Date)
_FechaModificacion = value
End Set
End Property



End Class


