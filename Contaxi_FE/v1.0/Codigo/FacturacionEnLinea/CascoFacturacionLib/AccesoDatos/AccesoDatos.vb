Public Class AccesoDatos

#Region " DOCUMENTOFACTURACION "
    Public Shared Function AgregarDocumentoFacturacion(ByVal Cnn As IConexion, ByVal Ent As DocumentoFacturacion) As Long

        Try

            Dim Param As New ColeccionParametro

            Param.Add("FacturacionID", Ent.FacturacionID, Parametro.TIPO_PARAMETRO.NUMERO)
            Param.Add("SocioID", Ent.SocioID, Parametro.TIPO_PARAMETRO.NUMERO)

            Param.Add("Identificador", Ent.Identificador, Parametro.TIPO_PARAMETRO.TEXTO)


            Return Cnn.LeerID("spCAS_A_DocumentoFacturacion", Param)

        Catch ex As Exception
            MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
            MKSLibrary.Funciones.Log.Funcion("AgregarDocumentoFacturacion")
            MKSLibrary.Funciones.Log.RegistrarError(ex)
            Return -1
        End Try

    End Function


    Public Shared Function ModificarDocumentoFacturacion(ByVal Cnn As IConexion, ByVal Ent As DocumentoFacturacion) As Boolean

        Try
            Dim Param As New ColeccionParametro

            Param.Add("FacturacionID", Ent.FacturacionID, Parametro.TIPO_PARAMETRO.NUMERO)
            Param.Add("SocioID", Ent.SocioID, Parametro.TIPO_PARAMETRO.NUMERO)


            If Ent.Identificador IsNot Nothing AndAlso Ent.Identificador.Trim.Length > 0 Then
                Param.Add("Identificador", Ent.Identificador, Parametro.TIPO_PARAMETRO.TEXTO)
            End If

            Return Cnn.EjecutarSP("spCAS_C_DocumentoFacturacion", Param)

        Catch ex As Exception
            MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
            MKSLibrary.Funciones.Log.Funcion("ModificarDocumentoFacturacion")
            MKSLibrary.Funciones.Log.RegistrarError(ex)
            Return False
        End Try

    End Function


    Public Shared Function EliminarDocumentoFacturacion(ByVal Cnn As IConexion, ByVal FacturacionID As Long, ByVal SocioID As Integer) As Boolean

        Try
            Dim Param As New ColeccionParametro

            Param.Add("FacturacionID", FacturacionID, Parametro.TIPO_PARAMETRO.NUMERO)
            Param.Add("SocioID", SocioID, Parametro.TIPO_PARAMETRO.NUMERO)


            Return Cnn.EjecutarSP("spCAS_B_DocumentoFacturacion", Param)

        Catch ex As Exception
            MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
            MKSLibrary.Funciones.Log.Funcion("EliminarDocumentoFacturacion")
            MKSLibrary.Funciones.Log.RegistrarError(ex)
            Return False
        End Try

    End Function


    Public Shared Function ObtenerDocumentoFacturacion(ByVal Cnn As IConexion, Optional ByVal Filtro As DocumentoFacturacion = Nothing) As List(Of DocumentoFacturacion)

        Try
            Dim Param As New ColeccionParametro

            If Filtro IsNot Nothing Then

                If Filtro.FacturacionID > -1 Then
                    Param.Add("FacturacionID", Filtro.FacturacionID, Parametro.TIPO_PARAMETRO.NUMERO)
                End If

                If Filtro.SocioID > -1 Then
                    Param.Add("SocioID", Filtro.SocioID, Parametro.TIPO_PARAMETRO.NUMERO)
                End If

                If Filtro.Identificador <> "" Then
                    Param.Add("Identificador", Filtro.Identificador, Parametro.TIPO_PARAMETRO.TEXTO)
                End If
            End If

            Return MKSLibrary.Funciones.Datos.ListaDesdeDataTable(Of DocumentoFacturacion)(Cnn.LeerSP("spCAS_Q_DocumentosFacturacion", Param))

        Catch ex As Exception
            MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
            MKSLibrary.Funciones.Log.Funcion("ObtenerDocumentoFacturacion")
            MKSLibrary.Funciones.Log.RegistrarError(ex)
            Return Nothing
        End Try

    End Function
#End Region

#Region " EQUIPOPARAMETROSISTEMA "
    Public Shared Function AgregarEquipoParametroSistema(ByVal Cnn As IConexion, ByVal Ent As EquipoParametroSistema) As Long

        Try

            Dim Param As New ColeccionParametro

            Param.Add("ParametroID", Ent.ParametroID, Parametro.TIPO_PARAMETRO.TEXTO)
            Param.Add("Equipo", Ent.Equipo, Parametro.TIPO_PARAMETRO.TEXTO)

            Param.Add("Valor", Ent.Valor, Parametro.TIPO_PARAMETRO.TEXTO)
            Param.Add("UsuarioModificadorID", Ent.UsuarioModificadorID, Parametro.TIPO_PARAMETRO.NUMERO)
            Param.Add("FechaModificacion", Ent.FechaModificacion, Parametro.TIPO_PARAMETRO.FECHA)


            Return Cnn.LeerID("spCAS_A_EquipoParametroSistema", Param)

        Catch ex As Exception
            MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
            MKSLibrary.Funciones.Log.Funcion("AgregarEquipoParametroSistema")
            MKSLibrary.Funciones.Log.RegistrarError(ex)
            Return -1
        End Try

    End Function


    Public Shared Function ModificarEquipoParametroSistema(ByVal Cnn As IConexion, ByVal Ent As EquipoParametroSistema) As Boolean

        Try
            Dim Param As New ColeccionParametro

            Param.Add("ParametroID", Ent.ParametroID, Parametro.TIPO_PARAMETRO.TEXTO)
            Param.Add("Equipo", Ent.Equipo, Parametro.TIPO_PARAMETRO.TEXTO)


            If Ent.Valor IsNot Nothing AndAlso Ent.Valor.Trim.Length > 0 Then
                Param.Add("Valor", Ent.Valor, Parametro.TIPO_PARAMETRO.TEXTO)
            End If

            If Ent.UsuarioModificadorID > -1 Then
                Param.Add("UsuarioModificadorID", Ent.UsuarioModificadorID, Parametro.TIPO_PARAMETRO.NUMERO)
            End If

            If Ent.FechaModificacion > #1/1/1900# Then
                Param.Add("FechaModificacion", Ent.FechaModificacion, Parametro.TIPO_PARAMETRO.FECHA)
            End If

            Return Cnn.EjecutarSP("spCAS_C_EquipoParametroSistema", Param)

        Catch ex As Exception
            MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
            MKSLibrary.Funciones.Log.Funcion("ModificarEquipoParametroSistema")
            MKSLibrary.Funciones.Log.RegistrarError(ex)
            Return False
        End Try

    End Function


    Public Shared Function EliminarEquipoParametroSistema(ByVal Cnn As IConexion, ByVal ParametroID As String, ByVal Equipo As String) As Boolean

        Try
            Dim Param As New ColeccionParametro

            Param.Add("ParametroID", ParametroID, Parametro.TIPO_PARAMETRO.TEXTO)
            Param.Add("Equipo", Equipo, Parametro.TIPO_PARAMETRO.TEXTO)


            Return Cnn.EjecutarSP("spCAS_B_EquipoParametroSistema", Param)

        Catch ex As Exception
            MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
            MKSLibrary.Funciones.Log.Funcion("EliminarEquipoParametroSistema")
            MKSLibrary.Funciones.Log.RegistrarError(ex)
            Return False
        End Try

    End Function


    Public Shared Function ObtenerEquipoParametroSistema(ByVal Cnn As IConexion, Optional ByVal Filtro As EquipoParametroSistema = Nothing) As List(Of EquipoParametroSistema)

        Try
            Dim Param As New ColeccionParametro

            If Filtro IsNot Nothing Then

                If Filtro.ParametroID <> "" Then
                    Param.Add("ParametroID", Filtro.ParametroID, Parametro.TIPO_PARAMETRO.TEXTO)
                End If

                If Filtro.Equipo <> "" Then
                    Param.Add("Equipo", Filtro.Equipo, Parametro.TIPO_PARAMETRO.TEXTO)
                End If

                If Filtro.Valor <> "" Then
                    Param.Add("Valor", Filtro.Valor, Parametro.TIPO_PARAMETRO.TEXTO)
                End If

                If Filtro.UsuarioModificadorID > -1 Then
                    Param.Add("UsuarioModificadorID", Filtro.UsuarioModificadorID, Parametro.TIPO_PARAMETRO.NUMERO)
                End If

                If Filtro.FechaModificacion > #1/1/1900# Then
                    Param.Add("FechaModificacion", Filtro.FechaModificacion, Parametro.TIPO_PARAMETRO.FECHA)
                End If
            End If

            Return MKSLibrary.Funciones.Datos.ListaDesdeDataTable(Of EquipoParametroSistema)(Cnn.LeerSP("spCAS_Q_EquiposParametroSistema", Param))

        Catch ex As Exception
            MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
            MKSLibrary.Funciones.Log.Funcion("ObtenerEquipoParametroSistema")
            MKSLibrary.Funciones.Log.RegistrarError(ex)
            Return Nothing
        End Try

    End Function
#End Region

#Region " FACTURACION "
    Public Shared Function AgregarFacturacion(ByVal Cnn As IConexion, ByVal Ent As Facturacion) As Long

        Try

            Dim Param As New ColeccionParametro

            Param.Add("AccountID", Ent.AccountID, Parametro.TIPO_PARAMETRO.NUMERO)
            Param.Add("RFCReceptor", Ent.RFCReceptor, Parametro.TIPO_PARAMETRO.TEXTO)
            Param.Add("Serie", Ent.Serie, Parametro.TIPO_PARAMETRO.TEXTO)
            Param.Add("Folio", Ent.Folio, Parametro.TIPO_PARAMETRO.TEXTO)
            Param.Add("UUID", Ent.UUID, Parametro.TIPO_PARAMETRO.TEXTO)
            Param.Add("Fecha", Ent.Fecha, Parametro.TIPO_PARAMETRO.FECHA)

            Return Cnn.LeerID("spCAS_A_Facturacion", Param)

        Catch ex As Exception
            MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
            MKSLibrary.Funciones.Log.Funcion("AgregarFacturacion")
            MKSLibrary.Funciones.Log.RegistrarError(ex)
            Return -1
        End Try

    End Function


    Public Shared Function ModificarFacturacion(ByVal Cnn As IConexion, ByVal Ent As Facturacion) As Boolean

        Try
            Dim Param As New ColeccionParametro

            Param.Add("FacturacionID", Ent.FacturacionID, Parametro.TIPO_PARAMETRO.NUMERO)


            If Ent.AccountID > -1 Then
                Param.Add("AccountID", Ent.AccountID, Parametro.TIPO_PARAMETRO.NUMERO)
            End If

            If Ent.RFCReceptor IsNot Nothing AndAlso Ent.RFCReceptor.Trim.Length > 0 Then
                Param.Add("RFCReceptor", Ent.RFCReceptor, Parametro.TIPO_PARAMETRO.TEXTO)
            End If

            If Ent.Serie IsNot Nothing AndAlso Ent.Serie.Trim.Length > 0 Then
                Param.Add("Serie", Ent.Serie, Parametro.TIPO_PARAMETRO.TEXTO)
            End If

            If Ent.Folio IsNot Nothing AndAlso Ent.Folio.Trim.Length > 0 Then
                Param.Add("Folio", Ent.Folio, Parametro.TIPO_PARAMETRO.TEXTO)
            End If

            If Ent.UUID IsNot Nothing AndAlso Ent.UUID.Trim.Length > 0 Then
                Param.Add("UUID", Ent.UUID, Parametro.TIPO_PARAMETRO.TEXTO)
            End If

            If Ent.Fecha > #1/1/1900# Then
                Param.Add("Fecha", Ent.Fecha, Parametro.TIPO_PARAMETRO.FECHA)
            End If

            Return Cnn.EjecutarSP("spCAS_C_Facturacion", Param)

        Catch ex As Exception
            MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
            MKSLibrary.Funciones.Log.Funcion("ModificarFacturacion")
            MKSLibrary.Funciones.Log.RegistrarError(ex)
            Return False
        End Try

    End Function

    Public Shared Function EliminarFacturacion(ByVal Cnn As IConexion, ByVal FacturacionID As Long) As Boolean

        Try
            Dim Param As New ColeccionParametro

            Param.Add("FacturacionID", FacturacionID, Parametro.TIPO_PARAMETRO.NUMERO)


            Return Cnn.EjecutarSP("spCAS_B_Facturacion", Param)

        Catch ex As Exception
            MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
            MKSLibrary.Funciones.Log.Funcion("EliminarFacturacion")
            MKSLibrary.Funciones.Log.RegistrarError(ex)
            Return False
        End Try

    End Function

    Public Shared Function ObtenerFacturacion(ByVal Cnn As IConexion, Optional ByVal Filtro As Facturacion = Nothing) As List(Of Facturacion)

        Try
            Dim Param As New ColeccionParametro

            If Filtro IsNot Nothing Then

                If Filtro.FacturacionID > -1 Then
                    Param.Add("FacturacionID", Filtro.FacturacionID, Parametro.TIPO_PARAMETRO.NUMERO)
                End If

                If Filtro.AccountID > -1 Then
                    Param.Add("AccountID", Filtro.AccountID, Parametro.TIPO_PARAMETRO.NUMERO)
                End If

                If Filtro.RFCReceptor <> "" Then
                    Param.Add("RFCReceptor", Filtro.RFCReceptor, Parametro.TIPO_PARAMETRO.TEXTO)
                End If

                If Filtro.RFCEmisor <> "" Then
                    Param.Add("RFCEmisor", Filtro.RFCEmisor, Parametro.TIPO_PARAMETRO.TEXTO)
                End If

                If Filtro.Serie <> "" Then
                    Param.Add("Serie", Filtro.Serie, Parametro.TIPO_PARAMETRO.TEXTO)
                End If

                If Filtro.Folio <> "" Then
                    Param.Add("Folio", Filtro.Folio, Parametro.TIPO_PARAMETRO.TEXTO)
                End If

                If Filtro.FolioFactura <> "" Then
                    Param.Add("FolioFactura", Filtro.FolioFactura, Parametro.TIPO_PARAMETRO.TEXTO)
                End If

                If Filtro.UUID <> "" Then
                    Param.Add("UUID", Filtro.UUID, Parametro.TIPO_PARAMETRO.TEXTO)
                End If

                If Filtro.Fecha > #1/1/1900# Then
                    Param.Add("Fecha", Filtro.Fecha, Parametro.TIPO_PARAMETRO.FECHA)
                End If

                If Filtro.FechaInicial > #1/1/1900# Then
                    Param.Add("FechaInicial", Filtro.FechaInicial, Parametro.TIPO_PARAMETRO.FECHA)
                End If

                If Filtro.FechaFinal > #1/1/1900# Then
                    Param.Add("FechaFinal", Filtro.FechaFinal, Parametro.TIPO_PARAMETRO.FECHA)
                End If

            End If

            Return MKSLibrary.Funciones.Datos.ListaDesdeDataTable(Of Facturacion)(Cnn.LeerSP("spCAS_Q_Facturacion", Param))

        Catch ex As Exception
            MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
            MKSLibrary.Funciones.Log.Funcion("ObtenerFacturacion")
            MKSLibrary.Funciones.Log.RegistrarError(ex)
            Return Nothing
        End Try

    End Function

    Public Shared Function ObtenerFacturacionServicios(ByVal Cnn As IConexion, Optional ByVal Filtro As Facturacion = Nothing) As List(Of Facturacion)

        Try
            Dim Param As New ColeccionParametro

            If Filtro IsNot Nothing Then

                If Filtro.FacturacionID > -1 Then
                    Param.Add("FacturacionID", Filtro.FacturacionID, Parametro.TIPO_PARAMETRO.NUMERO)
                End If

                If Filtro.AccountID > -1 Then
                    Param.Add("AccountID", Filtro.AccountID, Parametro.TIPO_PARAMETRO.NUMERO)
                End If

                If Filtro.RFCReceptor <> "" Then
                    Param.Add("RFCReceptor", Filtro.RFCReceptor, Parametro.TIPO_PARAMETRO.TEXTO)
                End If

                If Filtro.UUID <> "" Then
                    Param.Add("UUID", Filtro.UUID, Parametro.TIPO_PARAMETRO.TEXTO)
                End If

                If Filtro.Fecha > #1/1/1900# Then
                    Param.Add("Fecha", Filtro.Fecha, Parametro.TIPO_PARAMETRO.FECHA)
                End If

                If Filtro.ServicioID <> "" Then
                    Param.Add("ServicioID", Filtro.ServicioID, Parametro.TIPO_PARAMETRO.TEXTO)
                End If

            End If

            Return MKSLibrary.Funciones.Datos.ListaDesdeDataTable(Of Facturacion)(Cnn.LeerSP("spCAS_Q_FacturacionServicios", Param))

        Catch ex As Exception
            MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
            MKSLibrary.Funciones.Log.Funcion("ObtenerFacturacionServicios")
            MKSLibrary.Funciones.Log.RegistrarError(ex)
            Return Nothing
        End Try

    End Function
#End Region

#Region " PARAMETROSISTEMA "
    Public Shared Function AgregarParametroSistema(ByVal Cnn As IConexion, ByVal Ent As ParametroSistema) As Long

        Try

            Dim Param As New ColeccionParametro

            Param.Add("ParametroID", Ent.ParametroID, Parametro.TIPO_PARAMETRO.TEXTO)

            Param.Add("Descripcion", Ent.Descripcion, Parametro.TIPO_PARAMETRO.TEXTO)
            Param.Add("Valor", Ent.Valor, Parametro.TIPO_PARAMETRO.TEXTO)
            Param.Add("PorEquipo", Ent.PorEquipo, Parametro.TIPO_PARAMETRO.BOLEANO)
            Param.Add("TipoParametroID", Ent.TipoParametroID, Parametro.TIPO_PARAMETRO.NUMERO)
            Param.Add("Minimo", Ent.Minimo, Parametro.TIPO_PARAMETRO.NUMERO)
            Param.Add("Maximo", Ent.Maximo, Parametro.TIPO_PARAMETRO.NUMERO)
            Param.Add("Orden", Ent.Orden, Parametro.TIPO_PARAMETRO.NUMERO)
            Param.Add("UsuarioModificadorID", Ent.UsuarioModificadorID, Parametro.TIPO_PARAMETRO.NUMERO)
            Param.Add("FechaModificacion", Ent.FechaModificacion, Parametro.TIPO_PARAMETRO.FECHA)


            Return Cnn.LeerID("spCAS_A_ParametroSistema", Param)

        Catch ex As Exception
            MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
            MKSLibrary.Funciones.Log.Funcion("AgregarParametroSistema")
            MKSLibrary.Funciones.Log.RegistrarError(ex)
            Return -1
        End Try

    End Function


    Public Shared Function ModificarParametroSistema(ByVal Cnn As IConexion, ByVal Ent As ParametroSistema) As Boolean

        Try
            Dim Param As New ColeccionParametro

            Param.Add("ParametroID", Ent.ParametroID, Parametro.TIPO_PARAMETRO.TEXTO)


            If Ent.Descripcion IsNot Nothing AndAlso Ent.Descripcion.Trim.Length > 0 Then
                Param.Add("Descripcion", Ent.Descripcion, Parametro.TIPO_PARAMETRO.TEXTO)
            End If

            If Ent.Valor IsNot Nothing AndAlso Ent.Valor.Trim.Length > 0 Then
                Param.Add("Valor", Ent.Valor, Parametro.TIPO_PARAMETRO.TEXTO)
            End If

            If Ent.PorEquipo > False Then
                Param.Add("PorEquipo", Ent.PorEquipo, Parametro.TIPO_PARAMETRO.BOLEANO)
            End If

            If Ent.TipoParametroID > -1 Then
                Param.Add("TipoParametroID", Ent.TipoParametroID, Parametro.TIPO_PARAMETRO.NUMERO)
            End If

            If Ent.Minimo > -1 Then
                Param.Add("Minimo", Ent.Minimo, Parametro.TIPO_PARAMETRO.NUMERO)
            End If

            If Ent.Maximo > -1 Then
                Param.Add("Maximo", Ent.Maximo, Parametro.TIPO_PARAMETRO.NUMERO)
            End If

            If Ent.Orden > -1 Then
                Param.Add("Orden", Ent.Orden, Parametro.TIPO_PARAMETRO.NUMERO)
            End If

            If Ent.UsuarioModificadorID > -1 Then
                Param.Add("UsuarioModificadorID", Ent.UsuarioModificadorID, Parametro.TIPO_PARAMETRO.NUMERO)
            End If

            If Ent.FechaModificacion > #1/1/1900# Then
                Param.Add("FechaModificacion", Ent.FechaModificacion, Parametro.TIPO_PARAMETRO.FECHA)
            End If

            Return Cnn.EjecutarSP("spCAS_C_ParametroSistema", Param)

        Catch ex As Exception
            MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
            MKSLibrary.Funciones.Log.Funcion("ModificarParametroSistema")
            MKSLibrary.Funciones.Log.RegistrarError(ex)
            Return False
        End Try

    End Function


    Public Shared Function EliminarParametroSistema(ByVal Cnn As IConexion, ByVal ParametroID As String) As Boolean

        Try
            Dim Param As New ColeccionParametro

            Param.Add("ParametroID", ParametroID, Parametro.TIPO_PARAMETRO.TEXTO)


            Return Cnn.EjecutarSP("spCAS_B_ParametroSistema", Param)

        Catch ex As Exception
            MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
            MKSLibrary.Funciones.Log.Funcion("EliminarParametroSistema")
            MKSLibrary.Funciones.Log.RegistrarError(ex)
            Return False
        End Try

    End Function


    Public Shared Function ObtenerParametroSistema(ByVal Cnn As IConexion, Optional ByVal Filtro As ParametroSistema = Nothing) As List(Of ParametroSistema)

        Try
            Dim Param As New ColeccionParametro

            If Filtro IsNot Nothing Then

                If Filtro.ParametroID <> "" Then
                    Param.Add("ParametroID", Filtro.ParametroID, Parametro.TIPO_PARAMETRO.TEXTO)
                End If

                If Filtro.Descripcion <> "" Then
                    Param.Add("Descripcion", Filtro.Descripcion, Parametro.TIPO_PARAMETRO.TEXTO)
                End If

                If Filtro.Valor <> "" Then
                    Param.Add("Valor", Filtro.Valor, Parametro.TIPO_PARAMETRO.TEXTO)
                End If

                If Filtro.PorEquipo > False Then
                    Param.Add("PorEquipo", Filtro.PorEquipo, Parametro.TIPO_PARAMETRO.BOLEANO)
                End If

                If Filtro.TipoParametroID > -1 Then
                    Param.Add("TipoParametroID", Filtro.TipoParametroID, Parametro.TIPO_PARAMETRO.NUMERO)
                End If

                If Filtro.Minimo > -1 Then
                    Param.Add("Minimo", Filtro.Minimo, Parametro.TIPO_PARAMETRO.NUMERO)
                End If

                If Filtro.Maximo > -1 Then
                    Param.Add("Maximo", Filtro.Maximo, Parametro.TIPO_PARAMETRO.NUMERO)
                End If

                If Filtro.Orden > -1 Then
                    Param.Add("Orden", Filtro.Orden, Parametro.TIPO_PARAMETRO.NUMERO)
                End If

                If Filtro.UsuarioModificadorID > -1 Then
                    Param.Add("UsuarioModificadorID", Filtro.UsuarioModificadorID, Parametro.TIPO_PARAMETRO.NUMERO)
                End If

                If Filtro.FechaModificacion > #1/1/1900# Then
                    Param.Add("FechaModificacion", Filtro.FechaModificacion, Parametro.TIPO_PARAMETRO.FECHA)
                End If
            End If

            Return MKSLibrary.Funciones.Datos.ListaDesdeDataTable(Of ParametroSistema)(Cnn.LeerSP("spCAS_Q_ParametrosSistema", Param))

        Catch ex As Exception
            MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
            MKSLibrary.Funciones.Log.Funcion("ObtenerParametroSistema")
            MKSLibrary.Funciones.Log.RegistrarError(ex)
            Return Nothing
        End Try

    End Function
#End Region

#Region " PERMISO "
    Public Shared Function AgregarPermiso(ByVal Cnn As IConexion, ByVal Ent As Permiso) As Long

        Try

            Dim Param As New ColeccionParametro

            Param.Add("PermisoID", Ent.PermisoID, Parametro.TIPO_PARAMETRO.NUMERO)

            Param.Add("PermisoPadreID", Ent.PermisoPadreID, Parametro.TIPO_PARAMETRO.NUMERO)
            Param.Add("Descripcion", Ent.Descripcion, Parametro.TIPO_PARAMETRO.TEXTO)
            Param.Add("Orden", Ent.Orden, Parametro.TIPO_PARAMETRO.NUMERO)


            Return Cnn.LeerID("spCAS_A_Permiso", Param)

        Catch ex As Exception
            MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
            MKSLibrary.Funciones.Log.Funcion("AgregarPermiso")
            MKSLibrary.Funciones.Log.RegistrarError(ex)
            Return -1
        End Try

    End Function


    Public Shared Function ModificarPermiso(ByVal Cnn As IConexion, ByVal Ent As Permiso) As Boolean

        Try
            Dim Param As New ColeccionParametro

            Param.Add("PermisoID", Ent.PermisoID, Parametro.TIPO_PARAMETRO.NUMERO)


            If Ent.PermisoPadreID > -1 Then
                Param.Add("PermisoPadreID", Ent.PermisoPadreID, Parametro.TIPO_PARAMETRO.NUMERO)
            End If

            If Ent.Descripcion IsNot Nothing AndAlso Ent.Descripcion.Trim.Length > 0 Then
                Param.Add("Descripcion", Ent.Descripcion, Parametro.TIPO_PARAMETRO.TEXTO)
            End If

            If Ent.Orden > -1 Then
                Param.Add("Orden", Ent.Orden, Parametro.TIPO_PARAMETRO.NUMERO)
            End If

            Return Cnn.EjecutarSP("spCAS_C_Permiso", Param)

        Catch ex As Exception
            MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
            MKSLibrary.Funciones.Log.Funcion("ModificarPermiso")
            MKSLibrary.Funciones.Log.RegistrarError(ex)
            Return False
        End Try

    End Function


    Public Shared Function EliminarPermiso(ByVal Cnn As IConexion, ByVal PermisoID As Integer) As Boolean

        Try
            Dim Param As New ColeccionParametro

            Param.Add("PermisoID", PermisoID, Parametro.TIPO_PARAMETRO.NUMERO)


            Return Cnn.EjecutarSP("spCAS_B_Permiso", Param)

        Catch ex As Exception
            MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
            MKSLibrary.Funciones.Log.Funcion("EliminarPermiso")
            MKSLibrary.Funciones.Log.RegistrarError(ex)
            Return False
        End Try

    End Function


    Public Shared Function ObtenerPermiso(ByVal Cnn As IConexion, Optional ByVal Filtro As Permiso = Nothing) As List(Of Permiso)

        Try
            Dim Param As New ColeccionParametro

            If Filtro IsNot Nothing Then

                If Filtro.PermisoID > -1 Then
                    Param.Add("PermisoID", Filtro.PermisoID, Parametro.TIPO_PARAMETRO.NUMERO)
                End If

                If Filtro.PermisoPadreID > -1 Then
                    Param.Add("PermisoPadreID", Filtro.PermisoPadreID, Parametro.TIPO_PARAMETRO.NUMERO)
                End If

                If Filtro.Descripcion <> "" Then
                    Param.Add("Descripcion", Filtro.Descripcion, Parametro.TIPO_PARAMETRO.TEXTO)
                End If

                If Filtro.Orden > -1 Then
                    Param.Add("Orden", Filtro.Orden, Parametro.TIPO_PARAMETRO.NUMERO)
                End If
            End If

            Return MKSLibrary.Funciones.Datos.ListaDesdeDataTable(Of Permiso)(Cnn.LeerSP("spCAS_Q_Permisos", Param))

        Catch ex As Exception
            MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
            MKSLibrary.Funciones.Log.Funcion("ObtenerPermiso")
            MKSLibrary.Funciones.Log.RegistrarError(ex)
            Return Nothing
        End Try

    End Function
#End Region

#Region " PERMISOROL "
    Public Shared Function AgregarPermisoRol(ByVal Cnn As IConexion, ByVal Ent As PermisoRol) As Long

        Try

            Dim Param As New ColeccionParametro

            Param.Add("RolID", Ent.RolID, Parametro.TIPO_PARAMETRO.NUMERO)
            Param.Add("PermisoID", Ent.PermisoID, Parametro.TIPO_PARAMETRO.NUMERO)

            Param.Add("Valor1", Ent.Valor1, Parametro.TIPO_PARAMETRO.TEXTO)
            Param.Add("Valor2", Ent.Valor2, Parametro.TIPO_PARAMETRO.TEXTO)


            Return Cnn.LeerID("spCAS_A_PermisoRol", Param)

        Catch ex As Exception
            MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
            MKSLibrary.Funciones.Log.Funcion("AgregarPermisoRol")
            MKSLibrary.Funciones.Log.RegistrarError(ex)
            Return -1
        End Try

    End Function


    Public Shared Function ModificarPermisoRol(ByVal Cnn As IConexion, ByVal Ent As PermisoRol) As Boolean

        Try
            Dim Param As New ColeccionParametro

            Param.Add("RolID", Ent.RolID, Parametro.TIPO_PARAMETRO.NUMERO)
            Param.Add("PermisoID", Ent.PermisoID, Parametro.TIPO_PARAMETRO.NUMERO)


            If Ent.Valor1 IsNot Nothing AndAlso Ent.Valor1.Trim.Length > 0 Then
                Param.Add("Valor1", Ent.Valor1, Parametro.TIPO_PARAMETRO.TEXTO)
            End If

            If Ent.Valor2 IsNot Nothing AndAlso Ent.Valor2.Trim.Length > 0 Then
                Param.Add("Valor2", Ent.Valor2, Parametro.TIPO_PARAMETRO.TEXTO)
            End If

            Return Cnn.EjecutarSP("spCAS_C_PermisoRol", Param)

        Catch ex As Exception
            MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
            MKSLibrary.Funciones.Log.Funcion("ModificarPermisoRol")
            MKSLibrary.Funciones.Log.RegistrarError(ex)
            Return False
        End Try

    End Function


    Public Shared Function EliminarPermisoRol(ByVal Cnn As IConexion, ByVal RolID As Integer, ByVal PermisoID As Integer) As Boolean

        Try
            Dim Param As New ColeccionParametro

            Param.Add("RolID", RolID, Parametro.TIPO_PARAMETRO.NUMERO)
            Param.Add("PermisoID", PermisoID, Parametro.TIPO_PARAMETRO.NUMERO)


            Return Cnn.EjecutarSP("spCAS_B_PermisoRol", Param)

        Catch ex As Exception
            MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
            MKSLibrary.Funciones.Log.Funcion("EliminarPermisoRol")
            MKSLibrary.Funciones.Log.RegistrarError(ex)
            Return False
        End Try

    End Function


    Public Shared Function ObtenerPermisoRol(ByVal Cnn As IConexion, Optional ByVal Filtro As PermisoRol = Nothing) As List(Of PermisoRol)

        Try
            Dim Param As New ColeccionParametro

            If Filtro IsNot Nothing Then

                If Filtro.RolID > -1 Then
                    Param.Add("RolID", Filtro.RolID, Parametro.TIPO_PARAMETRO.NUMERO)
                End If

                If Filtro.PermisoID > -1 Then
                    Param.Add("PermisoID", Filtro.PermisoID, Parametro.TIPO_PARAMETRO.NUMERO)
                End If

                If Filtro.Valor1 <> "" Then
                    Param.Add("Valor1", Filtro.Valor1, Parametro.TIPO_PARAMETRO.TEXTO)
                End If

                If Filtro.Valor2 <> "" Then
                    Param.Add("Valor2", Filtro.Valor2, Parametro.TIPO_PARAMETRO.TEXTO)
                End If
            End If

            Return MKSLibrary.Funciones.Datos.ListaDesdeDataTable(Of PermisoRol)(Cnn.LeerSP("spCAS_Q_PermisosRol", Param))

        Catch ex As Exception
            MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
            MKSLibrary.Funciones.Log.Funcion("ObtenerPermisoRol")
            MKSLibrary.Funciones.Log.RegistrarError(ex)
            Return Nothing
        End Try

    End Function
#End Region

#Region " PERMISOUSUARIO "
    Public Shared Function AgregarPermisoUsuario(ByVal Cnn As IConexion, ByVal Ent As PermisoUsuario) As Long

        Try

            Dim Param As New ColeccionParametro

            Param.Add("UsuarioID", Ent.UsuarioID, Parametro.TIPO_PARAMETRO.NUMERO)
            Param.Add("PermisoID", Ent.PermisoID, Parametro.TIPO_PARAMETRO.NUMERO)

            Param.Add("Valor1", Ent.Valor1, Parametro.TIPO_PARAMETRO.TEXTO)
            Param.Add("Valor2", Ent.Valor2, Parametro.TIPO_PARAMETRO.TEXTO)


            Return Cnn.LeerID("spCAS_A_PermisoUsuario", Param)

        Catch ex As Exception
            MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
            MKSLibrary.Funciones.Log.Funcion("AgregarPermisoUsuario")
            MKSLibrary.Funciones.Log.RegistrarError(ex)
            Return -1
        End Try

    End Function


    Public Shared Function ModificarPermisoUsuario(ByVal Cnn As IConexion, ByVal Ent As PermisoUsuario) As Boolean

        Try
            Dim Param As New ColeccionParametro

            Param.Add("UsuarioID", Ent.UsuarioID, Parametro.TIPO_PARAMETRO.NUMERO)
            Param.Add("PermisoID", Ent.PermisoID, Parametro.TIPO_PARAMETRO.NUMERO)


            If Ent.Valor1 IsNot Nothing AndAlso Ent.Valor1.Trim.Length > 0 Then
                Param.Add("Valor1", Ent.Valor1, Parametro.TIPO_PARAMETRO.TEXTO)
            End If

            If Ent.Valor2 IsNot Nothing AndAlso Ent.Valor2.Trim.Length > 0 Then
                Param.Add("Valor2", Ent.Valor2, Parametro.TIPO_PARAMETRO.TEXTO)
            End If

            Return Cnn.EjecutarSP("spCAS_C_PermisoUsuario", Param)

        Catch ex As Exception
            MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
            MKSLibrary.Funciones.Log.Funcion("ModificarPermisoUsuario")
            MKSLibrary.Funciones.Log.RegistrarError(ex)
            Return False
        End Try

    End Function


    Public Shared Function EliminarPermisoUsuario(ByVal Cnn As IConexion, ByVal UsuarioID As Integer, ByVal PermisoID As Integer) As Boolean

        Try
            Dim Param As New ColeccionParametro

            Param.Add("UsuarioID", UsuarioID, Parametro.TIPO_PARAMETRO.NUMERO)
            Param.Add("PermisoID", PermisoID, Parametro.TIPO_PARAMETRO.NUMERO)


            Return Cnn.EjecutarSP("spCAS_B_PermisoUsuario", Param)

        Catch ex As Exception
            MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
            MKSLibrary.Funciones.Log.Funcion("EliminarPermisoUsuario")
            MKSLibrary.Funciones.Log.RegistrarError(ex)
            Return False
        End Try

    End Function


    Public Shared Function ObtenerPermisoUsuario(ByVal Cnn As IConexion, Optional ByVal Filtro As PermisoUsuario = Nothing) As List(Of PermisoUsuario)

        Try
            Dim Param As New ColeccionParametro

            If Filtro IsNot Nothing Then

                If Filtro.UsuarioID > -1 Then
                    Param.Add("UsuarioID", Filtro.UsuarioID, Parametro.TIPO_PARAMETRO.NUMERO)
                End If

                If Filtro.PermisoID > -1 Then
                    Param.Add("PermisoID", Filtro.PermisoID, Parametro.TIPO_PARAMETRO.NUMERO)
                End If

                If Filtro.Valor1 <> "" Then
                    Param.Add("Valor1", Filtro.Valor1, Parametro.TIPO_PARAMETRO.TEXTO)
                End If

                If Filtro.Valor2 <> "" Then
                    Param.Add("Valor2", Filtro.Valor2, Parametro.TIPO_PARAMETRO.TEXTO)
                End If
            End If

            Return MKSLibrary.Funciones.Datos.ListaDesdeDataTable(Of PermisoUsuario)(Cnn.LeerSP("spCAS_Q_PermisosUsuario", Param))

        Catch ex As Exception
            MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
            MKSLibrary.Funciones.Log.Funcion("ObtenerPermisoUsuario")
            MKSLibrary.Funciones.Log.RegistrarError(ex)
            Return Nothing
        End Try

    End Function
#End Region

#Region " ROL "
    Public Shared Function AgregarRol(ByVal Cnn As IConexion, ByVal Ent As Rol) As Long

        Try

            Dim Param As New ColeccionParametro


            Param.Add("Nombre", Ent.Nombre, Parametro.TIPO_PARAMETRO.TEXTO)
            Param.Add("Descripcion", Ent.Descripcion, Parametro.TIPO_PARAMETRO.TEXTO)
            Param.Add("FechaCreacion", Ent.FechaCreacion, Parametro.TIPO_PARAMETRO.FECHA)
            Param.Add("UsuarioCreadorID", Ent.UsuarioCreadorID, Parametro.TIPO_PARAMETRO.NUMERO)
            Param.Add("FechaModificacion", Ent.FechaModificacion, Parametro.TIPO_PARAMETRO.FECHA)
            Param.Add("UsuarioModificadorID", Ent.UsuarioModificadorID, Parametro.TIPO_PARAMETRO.NUMERO)
            Param.Add("Estatus", Ent.Estatus, Parametro.TIPO_PARAMETRO.NUMERO)


            Return Cnn.LeerID("spCAS_A_Rol", Param)

        Catch ex As Exception
            MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
            MKSLibrary.Funciones.Log.Funcion("AgregarRol")
            MKSLibrary.Funciones.Log.RegistrarError(ex)
            Return -1
        End Try

    End Function


    Public Shared Function ModificarRol(ByVal Cnn As IConexion, ByVal Ent As Rol) As Boolean

        Try
            Dim Param As New ColeccionParametro

            Param.Add("RolID", Ent.RolID, Parametro.TIPO_PARAMETRO.NUMERO)


            If Ent.Nombre IsNot Nothing AndAlso Ent.Nombre.Trim.Length > 0 Then
                Param.Add("Nombre", Ent.Nombre, Parametro.TIPO_PARAMETRO.TEXTO)
            End If

            If Ent.Descripcion IsNot Nothing AndAlso Ent.Descripcion.Trim.Length > 0 Then
                Param.Add("Descripcion", Ent.Descripcion, Parametro.TIPO_PARAMETRO.TEXTO)
            End If

            If Ent.FechaCreacion > #1/1/1900# Then
                Param.Add("FechaCreacion", Ent.FechaCreacion, Parametro.TIPO_PARAMETRO.FECHA)
            End If

            If Ent.UsuarioCreadorID > -1 Then
                Param.Add("UsuarioCreadorID", Ent.UsuarioCreadorID, Parametro.TIPO_PARAMETRO.NUMERO)
            End If

            If Ent.FechaModificacion > #1/1/1900# Then
                Param.Add("FechaModificacion", Ent.FechaModificacion, Parametro.TIPO_PARAMETRO.FECHA)
            End If

            If Ent.UsuarioModificadorID > -1 Then
                Param.Add("UsuarioModificadorID", Ent.UsuarioModificadorID, Parametro.TIPO_PARAMETRO.NUMERO)
            End If

            If Ent.Estatus > -1 Then
                Param.Add("Estatus", Ent.Estatus, Parametro.TIPO_PARAMETRO.NUMERO)
            End If

            Return Cnn.EjecutarSP("spCAS_C_Rol", Param)

        Catch ex As Exception
            MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
            MKSLibrary.Funciones.Log.Funcion("ModificarRol")
            MKSLibrary.Funciones.Log.RegistrarError(ex)
            Return False
        End Try

    End Function


    Public Shared Function EliminarRol(ByVal Cnn As IConexion, ByVal RolID As Integer) As Boolean

        Try
            Dim Param As New ColeccionParametro

            Param.Add("RolID", RolID, Parametro.TIPO_PARAMETRO.NUMERO)


            Return Cnn.EjecutarSP("spCAS_B_Rol", Param)

        Catch ex As Exception
            MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
            MKSLibrary.Funciones.Log.Funcion("EliminarRol")
            MKSLibrary.Funciones.Log.RegistrarError(ex)
            Return False
        End Try

    End Function


    Public Shared Function ObtenerRol(ByVal Cnn As IConexion, Optional ByVal Filtro As Rol = Nothing) As List(Of Rol)

        Try
            Dim Param As New ColeccionParametro

            If Filtro IsNot Nothing Then

                If Filtro.RolID > -1 Then
                    Param.Add("RolID", Filtro.RolID, Parametro.TIPO_PARAMETRO.NUMERO)
                End If

                If Filtro.Nombre <> "" Then
                    Param.Add("Nombre", Filtro.Nombre, Parametro.TIPO_PARAMETRO.TEXTO)
                End If

                If Filtro.Descripcion <> "" Then
                    Param.Add("Descripcion", Filtro.Descripcion, Parametro.TIPO_PARAMETRO.TEXTO)
                End If

                If Filtro.FechaCreacion > #1/1/1900# Then
                    Param.Add("FechaCreacion", Filtro.FechaCreacion, Parametro.TIPO_PARAMETRO.FECHA)
                End If

                If Filtro.UsuarioCreadorID > -1 Then
                    Param.Add("UsuarioCreadorID", Filtro.UsuarioCreadorID, Parametro.TIPO_PARAMETRO.NUMERO)
                End If

                If Filtro.FechaModificacion > #1/1/1900# Then
                    Param.Add("FechaModificacion", Filtro.FechaModificacion, Parametro.TIPO_PARAMETRO.FECHA)
                End If

                If Filtro.UsuarioModificadorID > -1 Then
                    Param.Add("UsuarioModificadorID", Filtro.UsuarioModificadorID, Parametro.TIPO_PARAMETRO.NUMERO)
                End If

                If Filtro.Estatus > -1 Then
                    Param.Add("Estatus", Filtro.Estatus, Parametro.TIPO_PARAMETRO.NUMERO)
                End If
            End If

            Return MKSLibrary.Funciones.Datos.ListaDesdeDataTable(Of Rol)(Cnn.LeerSP("spCAS_Q_Roles", Param))

        Catch ex As Exception
            MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
            MKSLibrary.Funciones.Log.Funcion("ObtenerRol")
            MKSLibrary.Funciones.Log.RegistrarError(ex)
            Return Nothing
        End Try

    End Function
#End Region

#Region " SERVICIOFACTURACION "
    'Public Shared Function AgregarServicioFacturacion(ByVal Cnn As IConexion, ByVal Ent As ServicioFacturacion) As Long

    '    Try

    '        Dim Param As New ColeccionParametro

    '        Param.Add("FacturacionID", Ent.FacturacionID, Parametro.TIPO_PARAMETRO.NUMERO)
    '        Param.Add("ServicioID", Ent.ServicioID, Parametro.TIPO_PARAMETRO.TEXTO)

    '        Param.Add("Monto", Ent.Monto, Parametro.TIPO_PARAMETRO.NUMERO)
    '        Param.Add("SocioID", Ent.SocioID, Parametro.TIPO_PARAMETRO.NUMERO)


    '        Return Cnn.LeerID("spCAS_A_ServicioFacturacion", Param)

    '    Catch ex As Exception
    '        MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
    '        MKSLibrary.Funciones.Log.Funcion("AgregarServicioFacturacion")
    '        MKSLibrary.Funciones.Log.RegistrarError(ex)
    '        Return -1
    '    End Try

    'End Function


    'Public Shared Function ModificarServicioFacturacion(ByVal Cnn As IConexion, ByVal Ent As ServicioFacturacion) As Boolean

    '    Try
    '        Dim Param As New ColeccionParametro

    '        Param.Add("FacturacionID", Ent.FacturacionID, Parametro.TIPO_PARAMETRO.NUMERO)
    '        Param.Add("ServicioID", Ent.ServicioID, Parametro.TIPO_PARAMETRO.TEXTO)


    '        If Ent.Monto > -1 Then
    '            Param.Add("Monto", Ent.Monto, Parametro.TIPO_PARAMETRO.NUMERO)
    '        End If

    '        If Ent.SocioID > -1 Then
    '            Param.Add("SocioID", Ent.SocioID, Parametro.TIPO_PARAMETRO.NUMERO)
    '        End If

    '        Return Cnn.EjecutarSP("spCAS_C_ServicioFacturacion", Param)

    '    Catch ex As Exception
    '        MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
    '        MKSLibrary.Funciones.Log.Funcion("ModificarServicioFacturacion")
    '        MKSLibrary.Funciones.Log.RegistrarError(ex)
    '        Return False
    '    End Try

    'End Function


    'Public Shared Function EliminarServicioFacturacion(ByVal Cnn As IConexion, ByVal FacturacionID As Long) As Boolean

    '    Try
    '        Dim Param As New ColeccionParametro

    '        Param.Add("FacturacionID", FacturacionID, Parametro.TIPO_PARAMETRO.NUMERO)

    '        Return Cnn.EjecutarSP("spCAS_B_ServicioFacturacion", Param)

    '    Catch ex As Exception
    '        MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
    '        MKSLibrary.Funciones.Log.Funcion("EliminarServicioFacturacion")
    '        MKSLibrary.Funciones.Log.RegistrarError(ex)
    '        Return False
    '    End Try

    'End Function


    'Public Shared Function ObtenerServicioFacturacion(ByVal Cnn As IConexion, Optional ByVal Filtro As ServicioFacturacion = Nothing) As List(Of ServicioFacturacion)

    '    Try
    '        Dim Param As New ColeccionParametro

    '        If Filtro IsNot Nothing Then

    '            If Filtro.FacturacionID > -1 Then
    '                Param.Add("FacturacionID", Filtro.FacturacionID, Parametro.TIPO_PARAMETRO.NUMERO)
    '            End If

    '            If Filtro.ServicioID <> "" Then
    '                Param.Add("ServicioID", Filtro.ServicioID, Parametro.TIPO_PARAMETRO.TEXTO)
    '            End If

    '            If Filtro.Monto > -1 Then
    '                Param.Add("Monto", Filtro.Monto, Parametro.TIPO_PARAMETRO.NUMERO)
    '            End If

    '            If Filtro.SocioID > -1 Then
    '                Param.Add("SocioID", Filtro.SocioID, Parametro.TIPO_PARAMETRO.NUMERO)
    '            End If
    '        End If

    '        Return MKSLibrary.Funciones.Datos.ListaDesdeDataTable(Of ServicioFacturacion)(Cnn.LeerSP("spCAS_Q_ServiciosFacturacion", Param))

    '    Catch ex As Exception
    '        MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
    '        MKSLibrary.Funciones.Log.Funcion("ObtenerServicioFacturacion")
    '        MKSLibrary.Funciones.Log.RegistrarError(ex)
    '        Return Nothing
    '    End Try

    'End Function
#End Region

#Region " SOCIOOPERATIVO "
    Public Shared Function AgregarSocioOperativo(ByVal Cnn As IConexion, ByVal Ent As SocioOperativo) As Long

        Try

            Dim Param As New ColeccionParametro

            Param.Add("Nombre", Ent.Nombre, Parametro.TIPO_PARAMETRO.TEXTO)
            Param.Add("ContraseñaSAT", Ent.ContraseñaSAT, Parametro.TIPO_PARAMETRO.TEXTO)
            Param.Add("RazonSocial", Ent.RazonSocial, Parametro.TIPO_PARAMETRO.TEXTO)
            Param.Add("RFC", Ent.RFC, Parametro.TIPO_PARAMETRO.TEXTO)
            Param.Add("Calle", Ent.Calle, Parametro.TIPO_PARAMETRO.TEXTO)
            Param.Add("Colonia", Ent.Colonia, Parametro.TIPO_PARAMETRO.TEXTO)
            Param.Add("NoExterior", Ent.NoExterior, Parametro.TIPO_PARAMETRO.TEXTO)
            Param.Add("NoInterior", Ent.NoInterior, Parametro.TIPO_PARAMETRO.TEXTO)
            Param.Add("Referencia", Ent.Referencia, Parametro.TIPO_PARAMETRO.TEXTO)
            Param.Add("Localidad", Ent.Localidad, Parametro.TIPO_PARAMETRO.TEXTO)
            Param.Add("Municipio", Ent.Municipio, Parametro.TIPO_PARAMETRO.TEXTO)
            Param.Add("Estado", Ent.Estado, Parametro.TIPO_PARAMETRO.TEXTO)
            Param.Add("Pais", Ent.Pais, Parametro.TIPO_PARAMETRO.TEXTO)
            Param.Add("CodigoPostal", Ent.CodigoPostal, Parametro.TIPO_PARAMETRO.TEXTO)
            Param.Add("Email", Ent.Email, Parametro.TIPO_PARAMETRO.TEXTO)
            Param.Add("DesglozaIVA", Ent.DesglozaIVA, Parametro.TIPO_PARAMETRO.BOLEANO)
            Param.Add("Regimen", Ent.Regimen, Parametro.TIPO_PARAMETRO.TEXTO)
            Param.Add("FechaCreacion", Ent.FechaCreacion, Parametro.TIPO_PARAMETRO.FECHA)
            Param.Add("UsuarioCreadorID", Ent.UsuarioCreadorID, Parametro.TIPO_PARAMETRO.NUMERO)
            Param.Add("FechaModificacion", Ent.FechaModificacion, Parametro.TIPO_PARAMETRO.FECHA)
            Param.Add("UsuarioModificadorID", Ent.UsuarioModificadorID, Parametro.TIPO_PARAMETRO.NUMERO)
            Param.Add("Estatus", Ent.Estatus, Parametro.TIPO_PARAMETRO.NUMERO)

            Return Cnn.LeerID("spCAS_A_SocioOperativo", Param)

        Catch ex As Exception
            MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
            MKSLibrary.Funciones.Log.Funcion("AgregarSocioOperativo")
            MKSLibrary.Funciones.Log.RegistrarError(ex)
            Return -1
        End Try

    End Function


    Public Shared Function ModificarSocioOperativo(ByVal Cnn As IConexion, ByVal Ent As SocioOperativo) As Boolean

        Try
            Dim Param As New ColeccionParametro

            Param.Add("SocioOperativoID", Ent.SocioOperativoID, Parametro.TIPO_PARAMETRO.NUMERO)

            If Ent.Nombre IsNot Nothing AndAlso Ent.Nombre.Trim.Length > 0 Then
                Param.Add("Nombre", Ent.Nombre, Parametro.TIPO_PARAMETRO.TEXTO)
            End If

            If Ent.ContraseñaSAT IsNot Nothing AndAlso Ent.ContraseñaSAT.Trim.Length > 0 Then
                Param.Add("ContraseñaSAT", Ent.ContraseñaSAT, Parametro.TIPO_PARAMETRO.TEXTO)
            End If

            If Ent.RazonSocial IsNot Nothing AndAlso Ent.RazonSocial.Trim.Length > 0 Then
                Param.Add("RazonSocial", Ent.RazonSocial, Parametro.TIPO_PARAMETRO.TEXTO)
            End If

            If Ent.RFC IsNot Nothing AndAlso Ent.RFC.Trim.Length > 0 Then
                Param.Add("RFC", Ent.RFC, Parametro.TIPO_PARAMETRO.TEXTO)
            End If

            If Ent.Calle IsNot Nothing AndAlso Ent.Calle.Trim.Length > 0 Then
                Param.Add("Calle", Ent.Calle, Parametro.TIPO_PARAMETRO.TEXTO)
            End If

            If Ent.Colonia IsNot Nothing AndAlso Ent.Colonia.Trim.Length > 0 Then
                Param.Add("Colonia", Ent.Colonia, Parametro.TIPO_PARAMETRO.TEXTO)
            End If

            If Ent.NoExterior IsNot Nothing AndAlso Ent.NoExterior.Trim.Length > 0 Then
                Param.Add("NoExterior", Ent.NoExterior, Parametro.TIPO_PARAMETRO.TEXTO)
            End If

            If Ent.NoInterior IsNot Nothing AndAlso Ent.NoInterior.Trim.Length > 0 Then
                Param.Add("NoInterior", Ent.NoInterior, Parametro.TIPO_PARAMETRO.TEXTO)
            End If

            If Ent.Referencia IsNot Nothing AndAlso Ent.Referencia.Trim.Length > 0 Then
                Param.Add("Referencia", Ent.Referencia, Parametro.TIPO_PARAMETRO.TEXTO)
            End If

            If Ent.Localidad IsNot Nothing AndAlso Ent.Localidad.Trim.Length > 0 Then
                Param.Add("Localidad", Ent.Localidad, Parametro.TIPO_PARAMETRO.TEXTO)
            End If

            If Ent.Municipio IsNot Nothing AndAlso Ent.Municipio.Trim.Length > 0 Then
                Param.Add("Municipio", Ent.Municipio, Parametro.TIPO_PARAMETRO.TEXTO)
            End If

            If Ent.Estado IsNot Nothing AndAlso Ent.Estado.Trim.Length > 0 Then
                Param.Add("Estado", Ent.Estado, Parametro.TIPO_PARAMETRO.TEXTO)
            End If

            If Ent.Pais IsNot Nothing AndAlso Ent.Pais.Trim.Length > 0 Then
                Param.Add("Pais", Ent.Pais, Parametro.TIPO_PARAMETRO.TEXTO)
            End If

            If Ent.CodigoPostal IsNot Nothing AndAlso Ent.CodigoPostal.Trim.Length > 0 Then
                Param.Add("CodigoPostal", Ent.CodigoPostal, Parametro.TIPO_PARAMETRO.TEXTO)
            End If

            If Ent.Email IsNot Nothing AndAlso Ent.Email.Trim.Length > 0 Then
                Param.Add("Email", Ent.Email, Parametro.TIPO_PARAMETRO.TEXTO)
            End If

            Param.Add("DesglozaIVA", Ent.DesglozaIVA, Parametro.TIPO_PARAMETRO.BOLEANO)

            If Ent.Regimen IsNot Nothing AndAlso Ent.Regimen.Trim.Length > 0 Then
                Param.Add("Regimen", Ent.Regimen, Parametro.TIPO_PARAMETRO.TEXTO)
            End If

            If Ent.FechaCreacion > #1/1/1900# Then
                Param.Add("FechaCreacion", Ent.FechaCreacion, Parametro.TIPO_PARAMETRO.FECHA)
            End If

            If Ent.UsuarioCreadorID > -1 Then
                Param.Add("UsuarioCreadorID", Ent.UsuarioCreadorID, Parametro.TIPO_PARAMETRO.NUMERO)
            End If

            If Ent.FechaModificacion > #1/1/1900# Then
                Param.Add("FechaModificacion", Ent.FechaModificacion, Parametro.TIPO_PARAMETRO.FECHA)
            End If

            If Ent.UsuarioModificadorID > -1 Then
                Param.Add("UsuarioModificadorID", Ent.UsuarioModificadorID, Parametro.TIPO_PARAMETRO.NUMERO)
            End If

            If Ent.Estatus > -1 Then
                Param.Add("Estatus", Ent.Estatus, Parametro.TIPO_PARAMETRO.NUMERO)
            End If

            Return Cnn.EjecutarSP("spCAS_C_SocioOperativo", Param)

        Catch ex As Exception
            MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
            MKSLibrary.Funciones.Log.Funcion("ModificarSocioOperativo")
            MKSLibrary.Funciones.Log.RegistrarError(ex)
            Return False
        End Try

    End Function

    Public Shared Function ModificarFolioSocioOperativo(ByVal Cnn As IConexion, ByVal Ent As SocioOperativo) As Boolean

        Try
            Dim Param As New ColeccionParametro

            Param.Add("SocioOperativoID", Ent.SocioOperativoID, Parametro.TIPO_PARAMETRO.NUMERO)
            Param.Add("Folio", Ent.Folio, Parametro.TIPO_PARAMETRO.NUMERO)

            Return Cnn.EjecutarSP("spCAS_C_FolioSocioOperativo", Param)

        Catch ex As Exception
            MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
            MKSLibrary.Funciones.Log.Funcion("ModificarFolioSocioOperativo")
            MKSLibrary.Funciones.Log.RegistrarError(ex)
            Return False
        End Try

    End Function


    Public Shared Function EliminarSocioOperativo(ByVal Cnn As IConexion, ByVal SocioOperativoID As Integer) As Boolean

        Try
            Dim Param As New ColeccionParametro

            Param.Add("SocioOperativoID", SocioOperativoID, Parametro.TIPO_PARAMETRO.NUMERO)


            Return Cnn.EjecutarSP("spCAS_B_SocioOperativo", Param)

        Catch ex As Exception
            MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
            MKSLibrary.Funciones.Log.Funcion("EliminarSocioOperativo")
            MKSLibrary.Funciones.Log.RegistrarError(ex)
            Return False
        End Try

    End Function


    Public Shared Function ObtenerSocioOperativo(ByVal Cnn As IConexion, Optional ByVal Filtro As SocioOperativo = Nothing) As List(Of SocioOperativo)

        Try
            Dim Param As New ColeccionParametro

            If Filtro IsNot Nothing Then

                If Filtro.SocioOperativoID > -1 Then
                    Param.Add("SocioOperativoID", Filtro.SocioOperativoID, Parametro.TIPO_PARAMETRO.NUMERO)
                End If

                If Filtro.Nombre <> "" Then
                    Param.Add("Nombre", Filtro.Nombre, Parametro.TIPO_PARAMETRO.TEXTO)
                End If

                If Filtro.ContraseñaSAT <> "" Then
                    Param.Add("ContraseñaSAT", Filtro.ContraseñaSAT, Parametro.TIPO_PARAMETRO.NUMERO)
                End If


                If Filtro.RazonSocial <> "" Then
                    Param.Add("RazonSocial", Filtro.RazonSocial, Parametro.TIPO_PARAMETRO.TEXTO)
                End If

                If Filtro.RFC <> "" Then
                    Param.Add("RFC", Filtro.RFC, Parametro.TIPO_PARAMETRO.TEXTO)
                End If

                If Filtro.Calle <> "" Then
                    Param.Add("Calle", Filtro.Calle, Parametro.TIPO_PARAMETRO.TEXTO)
                End If

                If Filtro.Colonia <> "" Then
                    Param.Add("Colonia", Filtro.Colonia, Parametro.TIPO_PARAMETRO.TEXTO)
                End If

                If Filtro.NoExterior <> "" Then
                    Param.Add("NoExterior", Filtro.NoExterior, Parametro.TIPO_PARAMETRO.TEXTO)
                End If

                If Filtro.NoInterior <> "" Then
                    Param.Add("NoInterior", Filtro.NoInterior, Parametro.TIPO_PARAMETRO.TEXTO)
                End If

                If Filtro.Referencia <> "" Then
                    Param.Add("Referencia", Filtro.Referencia, Parametro.TIPO_PARAMETRO.TEXTO)
                End If

                If Filtro.Localidad <> "" Then
                    Param.Add("Localidad", Filtro.Localidad, Parametro.TIPO_PARAMETRO.TEXTO)
                End If

                If Filtro.Municipio <> "" Then
                    Param.Add("Municipio", Filtro.Municipio, Parametro.TIPO_PARAMETRO.TEXTO)
                End If

                If Filtro.Estado <> "" Then
                    Param.Add("Estado", Filtro.Estado, Parametro.TIPO_PARAMETRO.TEXTO)
                End If

                If Filtro.Pais <> "" Then
                    Param.Add("Pais", Filtro.Pais, Parametro.TIPO_PARAMETRO.TEXTO)
                End If

                If Filtro.CodigoPostal <> "" Then
                    Param.Add("CodigoPostal", Filtro.CodigoPostal, Parametro.TIPO_PARAMETRO.TEXTO)
                End If

                If Filtro.Email <> "" Then
                    Param.Add("Email", Filtro.Email, Parametro.TIPO_PARAMETRO.TEXTO)
                End If

                If Filtro.Regimen <> "" Then
                    Param.Add("Regimen", Filtro.Regimen, Parametro.TIPO_PARAMETRO.TEXTO)
                End If

                If Filtro.FechaCreacion > #1/1/1900# Then
                    Param.Add("FechaCreacion", Filtro.FechaCreacion, Parametro.TIPO_PARAMETRO.FECHA)
                End If

                If Filtro.UsuarioCreadorID > -1 Then
                    Param.Add("UsuarioCreadorID", Filtro.UsuarioCreadorID, Parametro.TIPO_PARAMETRO.NUMERO)
                End If

                If Filtro.FechaModificacion > #1/1/1900# Then
                    Param.Add("FechaModificacion", Filtro.FechaModificacion, Parametro.TIPO_PARAMETRO.FECHA)
                End If

                If Filtro.UsuarioModificadorID > -1 Then
                    Param.Add("UsuarioModificadorID", Filtro.UsuarioModificadorID, Parametro.TIPO_PARAMETRO.NUMERO)
                End If

                If Filtro.Estatus > -1 Then
                    Param.Add("Estatus", Filtro.Estatus, Parametro.TIPO_PARAMETRO.NUMERO)
                End If
            End If

            Return MKSLibrary.Funciones.Datos.ListaDesdeDataTable(Of SocioOperativo)(Cnn.LeerSP("spCAS_Q_SociosOperativos", Param))

        Catch ex As Exception
            MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
            MKSLibrary.Funciones.Log.Funcion("ObtenerSocioOperativo")
            MKSLibrary.Funciones.Log.RegistrarError(ex)
            Return Nothing
        End Try

    End Function

    Public Shared Function ObtenerSigFolioSocioOperativo(ByVal Cnn As IConexion, Optional ByVal Filtro As SocioOperativo = Nothing) As Long

        Try
            Dim Param As New ColeccionParametro

            If Filtro IsNot Nothing Then

                If Filtro.SocioOperativoID > -1 Then
                    Param.Add("SocioOperativoID", Filtro.SocioOperativoID, Parametro.TIPO_PARAMETRO.NUMERO)
                End If

            End If

            Return Cnn.LeerID("spCAS_Q_SigFolioSocioOperativo", Param)

        Catch ex As Exception
            MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
            MKSLibrary.Funciones.Log.Funcion("ObtenerSigFolioSocioOperativo")
            MKSLibrary.Funciones.Log.RegistrarError(ex)
            Return Nothing
        End Try

    End Function

#End Region

#Region " UNIDADSOCIOOPERATIVO "
    Public Shared Function AgregarUnidadSocioOperativo(ByVal Cnn As IConexion, ByVal Ent As UnidadSocioOperativo) As Long

        Try

            Dim Param As New ColeccionParametro


            Param.Add("Unidad", Ent.Unidad, Parametro.TIPO_PARAMETRO.NUMERO)
            Param.Add("SocioOperativoID", Ent.SocioOperativoID, Parametro.TIPO_PARAMETRO.TEXTO)


            Return Cnn.LeerID("spCAS_A_UnidadSocioOperativo", Param)

        Catch ex As Exception
            MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
            MKSLibrary.Funciones.Log.Funcion("AgregarUnidadSocioOperativo")
            MKSLibrary.Funciones.Log.RegistrarError(ex)
            Return -1
        End Try

    End Function


    Public Shared Function ModificarUnidadSocioOperativo(ByVal Cnn As IConexion, ByVal Ent As UnidadSocioOperativo) As Boolean

        Try
            Dim Param As New ColeccionParametro

            Param.Add("UnidadSocioOperativoID", Ent.UnidadSocioOperativoID, Parametro.TIPO_PARAMETRO.NUMERO)


            If Ent.Unidad > -1 Then
                Param.Add("Unidad", Ent.Unidad, Parametro.TIPO_PARAMETRO.NUMERO)
            End If

            If Ent.SocioOperativoID IsNot Nothing AndAlso Ent.SocioOperativoID.Trim.Length > 0 Then
                Param.Add("SocioOperativoID", Ent.SocioOperativoID, Parametro.TIPO_PARAMETRO.TEXTO)
            End If

            Return Cnn.EjecutarSP("spCAS_C_UnidadSocioOperativo", Param)

        Catch ex As Exception
            MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
            MKSLibrary.Funciones.Log.Funcion("ModificarUnidadSocioOperativo")
            MKSLibrary.Funciones.Log.RegistrarError(ex)
            Return False
        End Try

    End Function


    Public Shared Function EliminarUnidadSocioOperativo(ByVal Cnn As IConexion, ByVal SocioOperativoID As Integer) As Boolean

        Try
            Dim Param As New ColeccionParametro

            Param.Add("SocioOperativoID", SocioOperativoID, Parametro.TIPO_PARAMETRO.NUMERO)


            Return Cnn.EjecutarSP("spCAS_B_UnidadSocioOperativo", Param)

        Catch ex As Exception
            MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
            MKSLibrary.Funciones.Log.Funcion("EliminarUnidadSocioOperativo")
            MKSLibrary.Funciones.Log.RegistrarError(ex)
            Return False
        End Try

    End Function


    Public Shared Function ObtenerUnidadSocioOperativo(ByVal Cnn As IConexion, Optional ByVal Filtro As UnidadSocioOperativo = Nothing) As List(Of UnidadSocioOperativo)

        Try
            Dim Param As New ColeccionParametro

            If Filtro IsNot Nothing Then

                If Filtro.UnidadSocioOperativoID > -1 Then
                    Param.Add("UnidadSocioOperativoID", Filtro.UnidadSocioOperativoID, Parametro.TIPO_PARAMETRO.NUMERO)
                End If

                If Filtro.Unidad > -1 Then
                    Param.Add("Unidad", Filtro.Unidad, Parametro.TIPO_PARAMETRO.NUMERO)
                End If

                If Filtro.SocioOperativoID <> "" Then
                    Param.Add("SocioOperativoID", Filtro.SocioOperativoID, Parametro.TIPO_PARAMETRO.TEXTO)
                End If
            End If

            Return MKSLibrary.Funciones.Datos.ListaDesdeDataTable(Of UnidadSocioOperativo)(Cnn.LeerSP("spCAS_Q_UnidadesSociosOperativos", Param))

        Catch ex As Exception
            MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
            MKSLibrary.Funciones.Log.Funcion("ObtenerUnidadSocioOperativo")
            MKSLibrary.Funciones.Log.RegistrarError(ex)
            Return Nothing
        End Try

    End Function
#End Region

#Region " USUARIO "
    Public Shared Function AgregarUsuario(ByVal Cnn As IConexion, ByVal Ent As Usuario) As Long

        Try

            Dim Param As New ColeccionParametro

            Param.Add("Login", Ent.Login, Parametro.TIPO_PARAMETRO.TEXTO)

            Param.Add("Nombre", Ent.Nombre, Parametro.TIPO_PARAMETRO.TEXTO)
            Param.Add("Password", Ent.Password, Parametro.TIPO_PARAMETRO.TEXTO)
            Param.Add("RolID", Ent.RolID, Parametro.TIPO_PARAMETRO.NUMERO)
            Param.Add("FechaCreacion", Ent.FechaCreacion, Parametro.TIPO_PARAMETRO.FECHA)
            Param.Add("UsuarioCreadorID", Ent.UsuarioCreadorID, Parametro.TIPO_PARAMETRO.NUMERO)
            Param.Add("FechaModificacion", Ent.FechaModificacion, Parametro.TIPO_PARAMETRO.FECHA)
            Param.Add("UsuarioModificadorID", Ent.UsuarioModificadorID, Parametro.TIPO_PARAMETRO.NUMERO)
            Param.Add("Estatus", Ent.Estatus, Parametro.TIPO_PARAMETRO.NUMERO)


            Return Cnn.LeerID("spCAS_A_Usuario", Param)

        Catch ex As Exception
            MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
            MKSLibrary.Funciones.Log.Funcion("AgregarUsuario")
            MKSLibrary.Funciones.Log.RegistrarError(ex)
            Return -1
        End Try

    End Function


    Public Shared Function ModificarUsuario(ByVal Cnn As IConexion, ByVal Ent As Usuario) As Boolean

        Try
            Dim Param As New ColeccionParametro

            Param.Add("UsuarioID", Ent.UsuarioID, Parametro.TIPO_PARAMETRO.NUMERO)
            Param.Add("Login", Ent.Login, Parametro.TIPO_PARAMETRO.TEXTO)


            If Ent.Nombre IsNot Nothing AndAlso Ent.Nombre.Trim.Length > 0 Then
                Param.Add("Nombre", Ent.Nombre, Parametro.TIPO_PARAMETRO.TEXTO)
            End If

            If Ent.Password IsNot Nothing AndAlso Ent.Password.Trim.Length > 0 Then
                Param.Add("Password", Ent.Password, Parametro.TIPO_PARAMETRO.TEXTO)
            End If

            If Ent.RolID > -1 Then
                Param.Add("RolID", Ent.RolID, Parametro.TIPO_PARAMETRO.NUMERO)
            End If

            If Ent.FechaCreacion > #1/1/1900# Then
                Param.Add("FechaCreacion", Ent.FechaCreacion, Parametro.TIPO_PARAMETRO.FECHA)
            End If

            If Ent.UsuarioCreadorID > -1 Then
                Param.Add("UsuarioCreadorID", Ent.UsuarioCreadorID, Parametro.TIPO_PARAMETRO.NUMERO)
            End If

            If Ent.FechaModificacion > #1/1/1900# Then
                Param.Add("FechaModificacion", Ent.FechaModificacion, Parametro.TIPO_PARAMETRO.FECHA)
            End If

            If Ent.UsuarioModificadorID > -1 Then
                Param.Add("UsuarioModificadorID", Ent.UsuarioModificadorID, Parametro.TIPO_PARAMETRO.NUMERO)
            End If

            If Ent.Estatus > -1 Then
                Param.Add("Estatus", Ent.Estatus, Parametro.TIPO_PARAMETRO.NUMERO)
            End If

            Return Cnn.EjecutarSP("spCAS_C_Usuario", Param)

        Catch ex As Exception
            MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
            MKSLibrary.Funciones.Log.Funcion("ModificarUsuario")
            MKSLibrary.Funciones.Log.RegistrarError(ex)
            Return False
        End Try

    End Function


    Public Shared Function EliminarUsuario(ByVal Cnn As IConexion, ByVal UsuarioID As Integer, ByVal Login As String) As Boolean

        Try
            Dim Param As New ColeccionParametro

            Param.Add("UsuarioID", UsuarioID, Parametro.TIPO_PARAMETRO.NUMERO)
            Param.Add("Login", Login, Parametro.TIPO_PARAMETRO.TEXTO)


            Return Cnn.EjecutarSP("spCAS_B_Usuario", Param)

        Catch ex As Exception
            MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
            MKSLibrary.Funciones.Log.Funcion("EliminarUsuario")
            MKSLibrary.Funciones.Log.RegistrarError(ex)
            Return False
        End Try

    End Function


    Public Shared Function ObtenerUsuario(ByVal Cnn As IConexion, Optional ByVal Filtro As Usuario = Nothing) As List(Of Usuario)

        Try
            Dim Param As New ColeccionParametro

            If Filtro IsNot Nothing Then

                If Filtro.UsuarioID > -1 Then
                    Param.Add("UsuarioID", Filtro.UsuarioID, Parametro.TIPO_PARAMETRO.NUMERO)
                End If

                If Filtro.Login <> "" Then
                    Param.Add("Login", Filtro.Login, Parametro.TIPO_PARAMETRO.TEXTO)
                End If

                If Filtro.Nombre <> "" Then
                    Param.Add("Nombre", Filtro.Nombre, Parametro.TIPO_PARAMETRO.TEXTO)
                End If

                If Filtro.Password <> "" Then
                    Param.Add("Password", Filtro.Password, Parametro.TIPO_PARAMETRO.TEXTO)
                End If

                If Filtro.RolID > -1 Then
                    Param.Add("RolID", Filtro.RolID, Parametro.TIPO_PARAMETRO.NUMERO)
                End If

                If Filtro.FechaCreacion > #1/1/1900# Then
                    Param.Add("FechaCreacion", Filtro.FechaCreacion, Parametro.TIPO_PARAMETRO.FECHA)
                End If

                If Filtro.UsuarioCreadorID > -1 Then
                    Param.Add("UsuarioCreadorID", Filtro.UsuarioCreadorID, Parametro.TIPO_PARAMETRO.NUMERO)
                End If

                If Filtro.FechaModificacion > #1/1/1900# Then
                    Param.Add("FechaModificacion", Filtro.FechaModificacion, Parametro.TIPO_PARAMETRO.FECHA)
                End If

                If Filtro.UsuarioModificadorID > -1 Then
                    Param.Add("UsuarioModificadorID", Filtro.UsuarioModificadorID, Parametro.TIPO_PARAMETRO.NUMERO)
                End If

                If Filtro.Estatus > -1 Then
                    Param.Add("Estatus", Filtro.Estatus, Parametro.TIPO_PARAMETRO.NUMERO)
                End If
            End If

            Return MKSLibrary.Funciones.Datos.ListaDesdeDataTable(Of Usuario)(Cnn.LeerSP("spCAS_Q_Usuarios", Param))

        Catch ex As Exception
            MKSLibrary.Funciones.Log.Modulo("AccesoDatos")
            MKSLibrary.Funciones.Log.Funcion("ObtenerUsuario")
            MKSLibrary.Funciones.Log.RegistrarError(ex)
            Return Nothing
        End Try

    End Function
#End Region

End Class