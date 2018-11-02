Imports System
Imports System.Data


Public Class PermisoRol

Dim _RolID As Integer = -1
Dim _PermisoID As Integer = -1
Dim _Valor1 As String = ""
Dim _Valor2 As String = ""


Public Sub New()
'Nada
End Sub

Public Sub New(ByVal Registro as DataRow)
_RolID = IIf(ISDBNull(Registro!RolID), Nothing, Registro!RolID)
_PermisoID = IIf(ISDBNull(Registro!PermisoID), Nothing, Registro!PermisoID)
_Valor1 = IIf(ISDBNull(Registro!Valor1), Nothing, Registro!Valor1)
_Valor2 = IIf(ISDBNull(Registro!Valor2), Nothing, Registro!Valor2)

End Sub

Public Property RolID As Integer
Get
return _RolID
End Get
Set(ByVal value As Integer)
_RolID = value
End Set
End Property

Public Property PermisoID As Integer
Get
return _PermisoID
End Get
Set(ByVal value As Integer)
_PermisoID = value
End Set
End Property

Public Property Valor1 As String
Get
return _Valor1
End Get
Set(ByVal value As String)
_Valor1 = value
End Set
End Property

Public Property Valor2 As String
Get
return _Valor2
End Get
Set(ByVal value As String)
_Valor2 = value
End Set
End Property



End Class


