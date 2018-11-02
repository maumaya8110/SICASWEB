Public Class ServicioFacturacion
    Dim _servicioID As String = String.Empty
    Dim _numeroConfirmacion As String = String.Empty
    Dim _fechaEjecucion As DateTime
    Dim _cliente As String = String.Empty
    Dim _unidad As String = String.Empty
    Dim _socioID As String = String.Empty
    Dim _precio As Double
    Dim _metodoPago As List(Of String) = New List(Of String)

    Public Property ServicioID As String
        Get
            Return _servicioID
        End Get
        Set(value As String)
            _servicioID = value
        End Set
    End Property

    Public Property Reservacion As String
        Get
            Return _numeroConfirmacion
        End Get
        Set(value As String)
            _numeroConfirmacion = value
        End Set
    End Property

    Public Property FechaEjecucion As DateTime
        Get
            Return _fechaEjecucion
        End Get
        Set(value As DateTime)
            _fechaEjecucion = value
        End Set
    End Property

    Public Property Cliente As String
        Get
            Return _cliente
        End Get
        Set(value As String)
            _cliente = value
        End Set
    End Property

    Public Property Unidad As String
        Get
            Return _unidad
        End Get
        Set(value As String)
            _unidad = value
        End Set
    End Property

    Public Property SocioID As String
        Get
            Return _socioID
        End Get
        Set(value As String)
            _socioID = value
        End Set
    End Property
    Public Property Precio As Double
        Get
            Return _precio
        End Get
        Set(value As Double)
            _precio = value
        End Set
    End Property

    Public Property MetodoPago As List(Of String)
        Get
            Return _metodoPago
        End Get
        Set(value As List(Of String))
            _metodoPago = value
        End Set
    End Property
End Class
