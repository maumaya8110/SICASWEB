<Serializable()>
Public Class ItemCFDI

    Dim _tokenFactura As String
    Public Property TokenFactura As String
        Get
            Return _tokenFactura
        End Get
        Set(value As String)
            _tokenFactura = value
        End Set
    End Property

    Dim _facturacionLibre As Boolean
    Public Property FacturacionLibre As Boolean
        Get
            Return _facturacionLibre
        End Get
        Set(value As Boolean)
            _facturacionLibre = value
        End Set
    End Property

    Dim _emisor_RFC As String
    Public Property Emisor_RFC As String
        Get
            Return _emisor_RFC
        End Get
        Set(value As String)
            _emisor_RFC = value
        End Set
    End Property

    Dim _folio As String
    Public Property Folio As String
        Get
            Return _folio
        End Get
        Set(value As String)
            _folio = value
        End Set
    End Property

    Dim _fechaFactura As String
    Public Property FechaFactura As String
        Get
            Return _fechaFactura
        End Get
        Set(value As String)
            _fechaFactura = value
        End Set
    End Property


    Dim _fechaCancelacion As String
    Public Property FechaCancelacion As String
        Get
            Return _fechaCancelacion
        End Get
        Set(value As String)
            _fechaCancelacion = value
        End Set
    End Property

    Dim _total As Double
    Public Property Total As Double
        Get
            Return _total
        End Get
        Set(value As Double)
            _total = value
        End Set
    End Property

    Dim _receptor_RFC As String
    Public Property Receptor_RFC As String
        Get
            Return _receptor_RFC
        End Get
        Set(value As String)
            _receptor_RFC = value
        End Set
    End Property

    Dim _receptor_Nombre As String
    Public Property Receptor_Nombre As String
        Get
            Return _receptor_Nombre
        End Get
        Set(value As String)
            _receptor_Nombre = value
        End Set
    End Property

    Dim _correoElectronico As String
    Public Property CorreoElectronico As String
        Get
            Return _correoElectronico
        End Get
        Set(value As String)
            _correoElectronico = value
        End Set
    End Property

    Dim _tickets As String
    Public Property Tickets As String
        Get
            Return _tickets
        End Get
        Set(value As String)
            _tickets = value
        End Set
    End Property

    Public Property Serie As String = ""

    Public Property UUID As String = ""
    Public Property ArchivoPDF As String
    Public Property ArchivoXML As String

    Public Property ConError As Boolean

    Public Property DetalleError As String
End Class
