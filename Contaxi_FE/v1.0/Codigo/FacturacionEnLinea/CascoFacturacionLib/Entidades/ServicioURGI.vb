Public Class ServicioURGI
    Dim _Folio As String
    Public Property Folio As String
        Get
            Return _Folio
        End Get
        Set(value As String)
            _Folio = value
        End Set
    End Property

    Dim _Monto As Double
    Public Property Monto As Double
        Get
            Return _Monto
        End Get
        Set(value As Double)
            _Monto = value
        End Set
    End Property

    Dim _Fecha As String
    Public Property Fecha As String
        Get
            Return _Fecha
        End Get
        Set(value As String)
            _Fecha = value
        End Set
    End Property

    Dim _Contacto As String
    Public Property Contacto As String
        Get
            Return _Contacto
        End Get
        Set(value As String)
            _Contacto = value
        End Set
    End Property

    Dim _Unidad As String
    Public Property Unidad As String
        Get
            Return _Unidad
        End Get
        Set(value As String)
            _Unidad = value
        End Set
    End Property

    Dim _RFC_Emisor As String
    Public Property RFC_Emisor As String
        Get
            Return _RFC_Emisor
        End Get
        Set(value As String)
            _RFC_Emisor = value
        End Set
    End Property

    Dim _MetodoPago_ID As Integer
    Public Property MetodoPago_ID As Integer
        Get
            Return _MetodoPago_ID
        End Get
        Set(value As Integer)
            _MetodoPago_ID = value
        End Set
    End Property

    Dim _CtaBanco As String
    Public Property CtaBanco As String
        Get
            Return _CtaBanco
        End Get
        Set(value As String)
            _CtaBanco = value
        End Set
    End Property
End Class
