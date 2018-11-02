Imports System
Imports System.Data

<Serializable()>
Public Class UnidadSocioOperativo

    Dim _UnidadSocioOperativoID As Integer = -1
    Dim _Unidad As Integer = -1
    Dim _SocioOperativoID As String = ""


    Public Sub New()
        'Nada
    End Sub

    Public Sub New(ByVal Registro As DataRow)
        _UnidadSocioOperativoID = IIf(IsDBNull(Registro!UnidadSocioOperativoID), Nothing, Registro!UnidadSocioOperativoID)
        _Unidad = IIf(IsDBNull(Registro!Unidad), Nothing, Registro!Unidad)
        _SocioOperativoID = IIf(IsDBNull(Registro!SocioOperativoID), Nothing, Registro!SocioOperativoID)

    End Sub

    Public Property UnidadSocioOperativoID As Integer
        Get
            Return _UnidadSocioOperativoID
        End Get
        Set(ByVal value As Integer)
            _UnidadSocioOperativoID = value
        End Set
    End Property

    Public Property Unidad As Integer
        Get
            Return _Unidad
        End Get
        Set(ByVal value As Integer)
            _Unidad = value
        End Set
    End Property

    Public Property SocioOperativoID As String
        Get
            Return _SocioOperativoID
        End Get
        Set(ByVal value As String)
            _SocioOperativoID = value
        End Set
    End Property



End Class


