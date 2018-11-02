Imports System
Imports System.Data


Public Class Facturacion

    Dim _FacturacionID As Long = -1
    Dim _AccountID As Long = -1
    Dim _RFCReceptor As String = ""
    Dim _Serie As String = ""
    Dim _Folio As String = ""
    Dim _UUID As String = ""
    Dim _Fecha As Date = #1/1/1900#

    Public Sub New()
        'Nada
    End Sub

    Public Sub New(ByVal Registro As DataRow)
        _FacturacionID = IIf(IsDBNull(Registro!FacturacionID), Nothing, Registro!FacturacionID)
        _AccountID = IIf(IsDBNull(Registro!AccountID), Nothing, Registro!AccountID)
        _RFCReceptor = IIf(IsDBNull(Registro!RFCReceptor), Nothing, Registro!RFCReceptor)
        _Serie = IIf(IsDBNull(Registro!Serie), Nothing, Registro!Serie)
        _Folio = IIf(IsDBNull(Registro!Folio), Nothing, Registro!Folio)
        _UUID = IIf(IsDBNull(Registro!UUID), Nothing, Registro!UUID)
        _Fecha = IIf(IsDBNull(Registro!Fecha), Nothing, Registro!Fecha)

    End Sub

    Public Property FacturacionID As Long
        Get
            Return _FacturacionID
        End Get
        Set(ByVal value As Long)
            _FacturacionID = value
        End Set
    End Property

    Public Property AccountID As Long
        Get
            Return _AccountID
        End Get
        Set(ByVal value As Long)
            _AccountID = value
        End Set
    End Property

    Public Property RFCReceptor As String
        Get
            Return _RFCReceptor
        End Get
        Set(ByVal value As String)
            _RFCReceptor = value
        End Set
    End Property

    Public Property Serie As String
        Get
            Return _Serie
        End Get
        Set(value As String)
            _Serie = value
        End Set
    End Property

    Public Property Folio As String
        Get
            Return _Folio
        End Get
        Set(value As String)
            _Folio = value
        End Set
    End Property

    Public Property UUID As String
        Get
            Return _UUID
        End Get
        Set(ByVal value As String)
            _UUID = value
        End Set
    End Property

    Public Property Fecha As Date
        Get
            Return _Fecha
        End Get
        Set(ByVal value As Date)
            _Fecha = value
        End Set
    End Property

    Public Property ServicioID As String = ""
    Public Property RFCEmisor As String = ""
    Public Property FolioFactura As String = ""
    Public Property Emisor As String = ""
    Public Property FechaInicial As Date = #1/1/1900#
    Public Property FechaFinal As Date = #1/1/1900#

End Class


