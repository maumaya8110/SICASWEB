Imports System
Imports System.Data


Public Class ParametroSistema

Dim _ParametroID As String = ""
Dim _Descripcion As String = ""
Dim _Valor As String = ""
Dim _PorEquipo As Boolean = False
Dim _TipoParametroID As Integer = -1
Dim _Minimo As Decimal = -1
Dim _Maximo As Decimal = -1
Dim _Orden As Integer = -1
Dim _UsuarioModificadorID As Integer = -1
Dim _FechaModificacion As Date = #1/1/1900#


Public Sub New()
'Nada
End Sub

Public Sub New(ByVal Registro as DataRow)
_ParametroID = IIf(ISDBNull(Registro!ParametroID), Nothing, Registro!ParametroID)
_Descripcion = IIf(ISDBNull(Registro!Descripcion), Nothing, Registro!Descripcion)
_Valor = IIf(ISDBNull(Registro!Valor), Nothing, Registro!Valor)
_PorEquipo = IIf(ISDBNull(Registro!PorEquipo), Nothing, Registro!PorEquipo)
_TipoParametroID = IIf(ISDBNull(Registro!TipoParametroID), Nothing, Registro!TipoParametroID)
_Minimo = IIf(ISDBNull(Registro!Minimo), Nothing, Registro!Minimo)
_Maximo = IIf(ISDBNull(Registro!Maximo), Nothing, Registro!Maximo)
_Orden = IIf(ISDBNull(Registro!Orden), Nothing, Registro!Orden)
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

Public Property Descripcion As String
Get
return _Descripcion
End Get
Set(ByVal value As String)
_Descripcion = value
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

Public Property PorEquipo As Boolean
Get
return _PorEquipo
End Get
Set(ByVal value As Boolean)
_PorEquipo = value
End Set
End Property

Public Property TipoParametroID As Integer
Get
return _TipoParametroID
End Get
Set(ByVal value As Integer)
_TipoParametroID = value
End Set
End Property

Public Property Minimo As Decimal
Get
return _Minimo
End Get
Set(ByVal value As Decimal)
_Minimo = value
End Set
End Property

Public Property Maximo As Decimal
Get
return _Maximo
End Get
Set(ByVal value As Decimal)
_Maximo = value
End Set
End Property

Public Property Orden As Integer
Get
return _Orden
End Get
Set(ByVal value As Integer)
_Orden = value
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


