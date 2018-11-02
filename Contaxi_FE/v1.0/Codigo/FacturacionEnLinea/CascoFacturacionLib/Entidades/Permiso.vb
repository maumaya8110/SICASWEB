Imports System
Imports System.Data


Public Class Permiso

Dim _PermisoID As Integer = -1
Dim _PermisoPadreID As Integer = -1
Dim _Descripcion As String = ""
Dim _Orden As Integer = -1


Public Sub New()
'Nada
End Sub

Public Sub New(ByVal Registro as DataRow)
_PermisoID = IIf(ISDBNull(Registro!PermisoID), Nothing, Registro!PermisoID)
_PermisoPadreID = IIf(ISDBNull(Registro!PermisoPadreID), Nothing, Registro!PermisoPadreID)
_Descripcion = IIf(ISDBNull(Registro!Descripcion), Nothing, Registro!Descripcion)
_Orden = IIf(ISDBNull(Registro!Orden), Nothing, Registro!Orden)

End Sub

Public Property PermisoID As Integer
Get
return _PermisoID
End Get
Set(ByVal value As Integer)
_PermisoID = value
End Set
End Property

Public Property PermisoPadreID As Integer
Get
return _PermisoPadreID
End Get
Set(ByVal value As Integer)
_PermisoPadreID = value
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

Public Property Orden As Integer
Get
return _Orden
End Get
Set(ByVal value As Integer)
_Orden = value
End Set
End Property



End Class


