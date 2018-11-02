Imports System
Imports System.Data

Public Class SocioOperativo

    Dim _SocioOperativoID As Integer = -1
    Dim _Nombre As String = ""
    Dim _ContraseñaSAT As String = ""
    Dim _RazonSocial As String = ""
    Dim _RFC As String = ""
    Dim _Calle As String = ""
    Dim _Colonia As String = ""
    Dim _NoExterior As String = ""
    Dim _NoInterior As String = ""
    Dim _Referencia As String = ""
    Dim _Localidad As String = ""
    Dim _Municipio As String = ""
    Dim _Estado As String = ""
    Dim _Pais As String = ""
    Dim _CodigoPostal As String = ""
    Dim _Email As String = ""
    Dim _DesglozaIVA As Boolean = False
    Dim _Regimen As String = ""
    Dim _FechaCreacion As Date = #1/1/1900#
    Dim _UsuarioCreadorID As Integer = -1
    Dim _FechaModificacion As Date = #1/1/1900#
    Dim _UsuarioModificadorID As Integer = -1
    Dim _Estatus As Integer = -1
    Dim _Serie As String = ""
    Dim _Folio As Integer = -1


    Public Sub New()
        'Nada
    End Sub

    Public Sub New(ByVal Registro As DataRow)
        _SocioOperativoID = IIf(IsDBNull(Registro!SocioOperativoID), Nothing, Registro!SocioOperativoID)
        _Nombre = IIf(IsDBNull(Registro!Nombre), Nothing, Registro!Nombre)
        _ContraseñaSAT = IIf(IsDBNull(Registro!ContraseñaSAT), Nothing, Registro!ContraseñaSAT)
        _RazonSocial = IIf(IsDBNull(Registro!RazonSocial), Nothing, Registro!RazonSocial)
        _RFC = IIf(IsDBNull(Registro!RFC), Nothing, Registro!RFC)
        _Calle = IIf(IsDBNull(Registro!Calle), Nothing, Registro!Calle)
        _Colonia = IIf(IsDBNull(Registro!Colonia), Nothing, Registro!Colonia)
        _NoExterior = IIf(IsDBNull(Registro!NoExterior), Nothing, Registro!NoExterior)
        _NoInterior = IIf(IsDBNull(Registro!NoInterior), Nothing, Registro!NoInterior)
        _Referencia = IIf(IsDBNull(Registro!Referencia), Nothing, Registro!Referencia)
        _Localidad = IIf(IsDBNull(Registro!Localidad), Nothing, Registro!Localidad)
        _Municipio = IIf(IsDBNull(Registro!Municipio), Nothing, Registro!Municipio)
        _Estado = IIf(IsDBNull(Registro!Estado), Nothing, Registro!Estado)
        _Pais = IIf(IsDBNull(Registro!Pais), Nothing, Registro!Pais)
        _CodigoPostal = IIf(IsDBNull(Registro!CodigoPostal), Nothing, Registro!CodigoPostal)
        _Email = IIf(IsDBNull(Registro!Email), Nothing, Registro!Email)
        _DesglozaIVA = IIf(IsDBNull(Registro!DesglozaIVA), Nothing, Registro!DesglozaIVA)
        _Regimen = IIf(IsDBNull(Registro!Regimen), Nothing, Registro!Regimen)
        _FechaCreacion = IIf(IsDBNull(Registro!FechaCreacion), Nothing, Registro!FechaCreacion)
        _UsuarioCreadorID = IIf(IsDBNull(Registro!UsuarioCreadorID), Nothing, Registro!UsuarioCreadorID)
        _FechaModificacion = IIf(IsDBNull(Registro!FechaModificacion), Nothing, Registro!FechaModificacion)
        _UsuarioModificadorID = IIf(IsDBNull(Registro!UsuarioModificadorID), Nothing, Registro!UsuarioModificadorID)
        _Estatus = IIf(IsDBNull(Registro!Estatus), Nothing, Registro!Estatus)
        _Serie = IIf(IsDBNull(Registro!Serie), Nothing, Registro!Serie)
        _Folio = IIf(IsDBNull(Registro!Folio), Nothing, Registro!Folio)

    End Sub

    Public Property SocioOperativoID As Integer
        Get
            Return _SocioOperativoID
        End Get
        Set(ByVal value As Integer)
            _SocioOperativoID = value
        End Set
    End Property

    Public Property Nombre As String
        Get
            Return _Nombre
        End Get
        Set(ByVal value As String)
            _Nombre = value
        End Set
    End Property

    Public Property ContraseñaSAT As String
        Get
            Return _ContraseñaSAT
        End Get
        Set(ByVal value As String)
            _ContraseñaSAT = value
        End Set
    End Property

    Public Property RazonSocial As String
        Get
            Return _RazonSocial
        End Get
        Set(ByVal value As String)
            _RazonSocial = value
        End Set
    End Property

    Public Property RFC As String
        Get
            Return _RFC
        End Get
        Set(ByVal value As String)
            _RFC = value
        End Set
    End Property

    Public Property Calle As String
        Get
            Return _Calle
        End Get
        Set(ByVal value As String)
            _Calle = value
        End Set
    End Property

    Public Property Colonia As String
        Get
            Return _Colonia
        End Get
        Set(ByVal value As String)
            _Colonia = value
        End Set
    End Property

    Public Property NoExterior As String
        Get
            Return _NoExterior
        End Get
        Set(ByVal value As String)
            _NoExterior = value
        End Set
    End Property

    Public Property NoInterior As String
        Get
            Return _NoInterior
        End Get
        Set(ByVal value As String)
            _NoInterior = value
        End Set
    End Property

    Public Property Referencia As String
        Get
            Return _Referencia
        End Get
        Set(ByVal value As String)
            _Referencia = value
        End Set
    End Property

    Public Property Localidad As String
        Get
            Return _Localidad
        End Get
        Set(ByVal value As String)
            _Localidad = value
        End Set
    End Property

    Public Property Municipio As String
        Get
            Return _Municipio
        End Get
        Set(ByVal value As String)
            _Municipio = value
        End Set
    End Property

    Public Property Estado As String
        Get
            Return _Estado
        End Get
        Set(ByVal value As String)
            _Estado = value
        End Set
    End Property

    Public Property Pais As String
        Get
            Return _Pais
        End Get
        Set(ByVal value As String)
            _Pais = value
        End Set
    End Property

    Public Property CodigoPostal As String
        Get
            Return _CodigoPostal
        End Get
        Set(ByVal value As String)
            _CodigoPostal = value
        End Set
    End Property

    Public Property Email As String
        Get
            Return _Email
        End Get
        Set(ByVal value As String)
            _Email = value
        End Set
    End Property

    Public Property DesglozaIVA As Boolean
        Get
            Return _DesglozaIVA
        End Get
        Set(value As Boolean)
            _DesglozaIVA = value
        End Set
    End Property

    Public Property Regimen As String
        Get
            Return _Regimen
        End Get
        Set(value As String)
            _Regimen = value
        End Set
    End Property

    Public Property FechaCreacion As Date
        Get
            Return _FechaCreacion
        End Get
        Set(ByVal value As Date)
            _FechaCreacion = value
        End Set
    End Property

    Public Property UsuarioCreadorID As Integer
        Get
            Return _UsuarioCreadorID
        End Get
        Set(ByVal value As Integer)
            _UsuarioCreadorID = value
        End Set
    End Property

    Public Property FechaModificacion As Date
        Get
            Return _FechaModificacion
        End Get
        Set(ByVal value As Date)
            _FechaModificacion = value
        End Set
    End Property

    Public Property UsuarioModificadorID As Integer
        Get
            Return _UsuarioModificadorID
        End Get
        Set(ByVal value As Integer)
            _UsuarioModificadorID = value
        End Set
    End Property

    Public Property Estatus As Integer
        Get
            Return _Estatus
        End Get
        Set(ByVal value As Integer)
            _Estatus = value
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

    Public Property Folio As Integer
        Get
            Return _Folio
        End Get
        Set(value As Integer)
            _Folio = value
        End Set
    End Property

    Public Property TextoEstatus As String = ""
    Public Property ArchivoCER As String = ""
    Public Property ArchivoKEY As String = ""
    Public Property RutaArchivos As String = ""

    Public Property Unidades As New List(Of UnidadSocioOperativo)
    Public Property Unidad As Integer = 0

End Class


