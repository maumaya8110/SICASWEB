Public Class ParamFacturas
    Dim _desglozaIVA As Boolean = False
    Dim _regimenFiscal As String
    Dim _correoElectronicoReceptor As String
    Dim _correoElectronicoEmisor As String
    Dim _moneda As String
    Dim _metodoDePago As String
    Dim _lugarExpedicion As String
    Dim _version As String
    Dim _numCtaPago As String
    Dim _formaDePago As String
    Dim _condicionesDePago As String
    Dim _serie As String
    Dim _folio As String
    Dim _archivoCER As String
    Dim _archivoKEY As String
    Dim _contrasena As String
    Dim _WSURL As String
    Dim _WSUsuario As String
    Dim _WSContrasena As String
    Dim _WSArchivoCER As String
    Dim _WSArchivoCERContrasena As String
    Dim _pathFacturas As String
    Dim _TasaIVA As Double
    Dim _txtConcepto As String
    Dim _token As String
    Dim _wsurlCancelacion As String
    Dim _unidad As String
    Dim _urlImagen As String

    Public Property UrlImagen As String
        Get
            Return _urlImagen
        End Get
        Set(value As String)
            _urlImagen = value
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

    Public Property WSURLCancelacion As String
        Get
            Return _wsurlCancelacion
        End Get
        Set(value As String)
            _wsurlCancelacion = value
        End Set
    End Property

    Public Property Token As String
        Get
            Return _token
        End Get
        Set(value As String)
            _token = value
        End Set
    End Property

    Public Property TxtConcepto As String
        Get
            Return _txtConcepto
        End Get
        Set(value As String)
            _txtConcepto = value
        End Set
    End Property

    Public Property TasaIVA As Double
        Get
            Return _TasaIVA
        End Get
        Set(value As Double)
            _TasaIVA = value
        End Set
    End Property

    Public Property PathFacturas As String
        Get
            Return _pathFacturas
        End Get
        Set(value As String)
            _pathFacturas = value
        End Set
    End Property

    Public Property WSArchivoCERContrasena
        Get
            Return _WSArchivoCERContrasena
        End Get
        Set(value)
            _WSArchivoCERContrasena = value
        End Set
    End Property

    Public Property WSArchivoCER As String
        Get
            Return _WSArchivoCER
        End Get
        Set(value As String)
            _WSArchivoCER = value
        End Set
    End Property

    Public Property WSContrasena As String
        Get
            Return _WSContrasena
        End Get
        Set(value As String)
            _WSContrasena = value
        End Set
    End Property

    Public Property WSUsuario As String
        Get
            Return _WSUsuario
        End Get
        Set(value As String)
            _WSUsuario = value
        End Set
    End Property

    Public Property WSURL As String
        Get
            Return _WSURL
        End Get
        Set(value As String)
            _WSURL = value
        End Set
    End Property

    Public Property Contrasena As String
        Get
            Return _contrasena
        End Get
        Set(value As String)
            _contrasena = value
        End Set
    End Property

    Public Property ArchivoKey As String
        Get
            Return _archivoKEY
        End Get
        Set(value As String)
            _archivoKEY = value
        End Set
    End Property

    Public Property ArchivoCER As String
        Get
            Return _archivoCER
        End Get
        Set(value As String)
            _archivoCER = value
        End Set
    End Property

    Public Property Folio As String
        Get
            Return _folio
        End Get
        Set(value As String)
            _folio = value
        End Set
    End Property

    Public Property Serie As String
        Get
            Return _serie
        End Get
        Set(value As String)
            _serie = value
        End Set
    End Property

    Public Property CondicionesDePago As String
        Get
            Return _condicionesDePago
        End Get
        Set(value As String)
            _condicionesDePago = value
        End Set
    End Property

    Public Property FormaDePago As String
        Get
            Return _formaDePago
        End Get
        Set(value As String)
            _formaDePago = value
        End Set
    End Property

    Public Property NumCtaPago As String
        Get
            Return _numCtaPago
        End Get
        Set(value As String)
            _numCtaPago = value
        End Set
    End Property

    Public Property Version As String
        Get
            Return _version
        End Get
        Set(value As String)
            _version = value
        End Set
    End Property

    Public Property LugarExpedicion As String
        Get
            Return _lugarExpedicion
        End Get
        Set(value As String)
            _lugarExpedicion = value
        End Set
    End Property

    Public Property MetodoDePago As String
        Get
            Return _metodoDePago
        End Get
        Set(value As String)
            _metodoDePago = value
        End Set
    End Property

    Public Property Moneda As String
        Get
            Return _moneda
        End Get
        Set(value As String)
            _moneda = value
        End Set
    End Property

    Public Property CorreoElectronicoEmisor As String
        Get
            Return _correoElectronicoEmisor
        End Get
        Set(value As String)
            _correoElectronicoEmisor = value
        End Set
    End Property

    Public Property CorreoElectronicoReceptor As String
        Get
            Return _correoElectronicoReceptor
        End Get
        Set(value As String)
            _correoElectronicoReceptor = value
        End Set
    End Property

    Public Property RegimenFiscal As String
        Get
            Return _regimenFiscal
        End Get
        Set(value As String)
            _regimenFiscal = value
        End Set
    End Property

    Public Property DesglozaIVA As Boolean
        Get
            Return _desglozaIVA
        End Get
        Set(value As Boolean)
            _desglozaIVA = value
        End Set
    End Property
End Class
