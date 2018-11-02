Imports System
Imports System.Data


Public Class DocumentoFacturacion

Dim _FacturacionID As Long = -1
Dim _SocioID As Integer = -1
Dim _Identificador As String = ""


Public Sub New()
'Nada
End Sub

Public Sub New(ByVal Registro as DataRow)
_FacturacionID = IIf(ISDBNull(Registro!FacturacionID), Nothing, Registro!FacturacionID)
_SocioID = IIf(ISDBNull(Registro!SocioID), Nothing, Registro!SocioID)
_Identificador = IIf(ISDBNull(Registro!Identificador), Nothing, Registro!Identificador)

End Sub

Public Property FacturacionID As Long
Get
return _FacturacionID
End Get
Set(ByVal value As Long)
_FacturacionID = value
End Set
End Property

Public Property SocioID As Integer
Get
return _SocioID
End Get
Set(ByVal value As Integer)
_SocioID = value
End Set
End Property

Public Property Identificador As String
Get
return _Identificador
End Get
Set(ByVal value As String)
_Identificador = value
End Set
End Property



End Class


