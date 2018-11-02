Imports System.Net.Mail
Imports System.Net
Imports System.Drawing
Imports System.IO
Imports MKSLibrary.Conectividad.Comun
Imports MKSLibrary.Funciones
Imports System.Web

Public Class Funciones

    Public Shared Function RutaApp() As String
        Return System.IO.Path.GetDirectoryName(New Uri(System.Reflection.Assembly.GetExecutingAssembly.GetName.CodeBase).LocalPath)
    End Function

    Function ToAbsoluteURL(relativeURL As String, HttpContext As System.Web.HttpContextBase) As String
        Dim sDelete As String = System.Configuration.ConfigurationManager.AppSettings("AppName")
        Dim sPort As String = System.Configuration.ConfigurationManager.AppSettings("AppPort")
        relativeURL = relativeURL.Replace(sDelete, "")

        If String.IsNullOrEmpty(relativeURL) Then Return relativeURL
        If HttpContext Is Nothing Then Return relativeURL
        If relativeURL.StartsWith("/") Then relativeURL = relativeURL.Insert(0, "~")
        If Not relativeURL.StartsWith("~/") Then relativeURL = relativeURL.Insert(0, "~/")
        Dim url As System.Uri = HttpContext.Request.Url
        'Dim port As String = IIf(url.Port = 80, "", String.Format(":{0}", url.Port))
        'port = IIf(port = "", String.Format(":{0}", sPort), port)
        Return String.Format("{0}://{1}{2}{3}", url.Scheme, url.Host, sPort, VirtualPathUtility.ToAbsolute(relativeURL))
    End Function

    Public Shared Function LeerConfig() As Boolean

        Try

            Dim Cfg As New IO.StreamReader(RutaApp() & "\Sistema.cfg")

            Dim Contenido As String = Cfg.ReadToEnd
            Cfg.Close()

            Dim Lineas() As String = Contenido.Split(ControlChars.NewLine)

            For Each Linea As String In Lineas

                If Linea.Trim.Length > 0 AndAlso Not Linea.Trim.StartsWith(";") Then

                    Try

                        Dim Prop As String = Linea.Split("=")(0).Trim.ToUpper
                        Dim Valor As String = Linea.Split("=")(1).Trim

                        Select Case Prop

                            Case "SRV"
                                Conexion.Server = Valor
                            Case "DBS"
                                Conexion.Database = Valor
                            Case "USR"
                                Conexion.User = Valor
                            Case "PWD"
                                Conexion.Password = Valor

                                'Parametros de Email
                            Case "SMTPSRV"
                                Globales.SMTPServidor = Valor

                            Case "SMTPPRT"
                                Globales.SMTPPuerto = Valor

                            Case "SMTPUSR"
                                Globales.SMTPUsuario = Valor

                            Case "SMTPPWD"
                                Globales.SMTPPass = Valor

                            Case "SMTPCTA"
                                Globales.SMTPUsarCuenta = Valor


                        End Select

                    Catch ex As Exception
                        Return False
                    End Try

                End If

            Next

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Shared Function LeerParametro(ParametroID As String) As ParametroSistema

        Try

            Using CnnObj As Object = Conexion.Conectar

                If CnnObj IsNot Nothing Then

                    Dim Cnn As IConexion = CnnObj

                    Dim Param As New ParametroSistema
                    Param.ParametroID = ParametroID

                    Param = AccesoDatos.ObtenerParametroSistema(Cnn, Param).FirstOrDefault

                    Return Param

                Else

                    Return Nothing

                End If

            End Using

        Catch ex As Exception
            Log.Modulo("Funciones")
            Log.Funcion("LeerParametro")
            Log.RegistrarError(ex)
            Return Nothing
        End Try

    End Function

    Public Shared Function ValidarFecha(TextoFecha As String, Formato As String) As Date?

        Dim dt As Date

        If Date.TryParseExact(TextoFecha.Trim, Formato.Trim, Globalization.CultureInfo.GetCultureInfo("es-MX").DateTimeFormat, Globalization.DateTimeStyles.None, dt) Then
            Return dt
        Else
            Return Nothing
        End If

    End Function

    Public Shared Function ImagenDesdeBytes(bytes As Byte()) As Image

        Using mem As New MemoryStream(bytes)

            Dim bmp As Image = Image.FromStream(mem)

            Return bmp

        End Using

    End Function

    Public Shared Function ObtenerPermisosUsuario(RolID As Integer) As ResultadoProceso(Of List(Of PermisoRol))
        Dim Retorno As New ResultadoProceso(Of List(Of PermisoRol))
        Try
            Using CnnObj As Object = Conexion.Conectar

                If CnnObj IsNot Nothing Then

                    Dim Cnn As IConexion = CnnObj

                    Dim PR As New PermisoRol
                    PR.RolID = RolID

                    Dim ListaPermmisos As List(Of PermisoRol) = AccesoDatos.ObtenerPermisoRol(Cnn, PR)

                    If ListaPermmisos Is Nothing Then
                        Retorno.HayError = True
                        Retorno.MensajeError = "No se pudo consultar el listado de permisos, intente nuevamente"
                    Else
                        Retorno.Resultado = ListaPermmisos
                    End If

                Else
                    Retorno.HayError = True
                    Retorno.MensajeError = "No hay conexión, intente nuevamente"
                End If

            End Using
        Catch ex As Exception
            Modulo("Funciones")
            Funcion("ObtenerPermisosUsuario")
            RegistrarError(ex)
            Retorno.HayError = True
            Retorno.MensajeError = "No se completo el Proceso [OPU]"
        End Try
        Return Retorno
    End Function

    Public Shared Function ObtenerUsuarioFirmado(Usr As Usuario) As ResultadoProceso(Of Usuario)
        Dim Retorno As New ResultadoProceso(Of Usuario)
        Try

            Using CnnObj As Object = Conexion.Conectar

                If CnnObj IsNot Nothing Then

                    Dim Cnn As IConexion = CnnObj

                    Dim ListaUsr As New List(Of Usuario)
                    ListaUsr = AccesoDatos.ObtenerUsuario(Cnn, Usr)

                    If ListaUsr Is Nothing Then
                        Retorno.HayError = True
                        Retorno.MensajeError = "No se pudo consultar la información del usuario, intente nuevamente"
                    Else
                        Retorno.Resultado = ListaUsr.FirstOrDefault
                    End If

                Else

                    Retorno.HayError = True
                    Retorno.MensajeError = "No har conexión, intente nuevamente"
                End If

            End Using

        Catch ex As Exception
            Modulo("Funciones")
            Funcion("ObtenerUsuarioFirmado")
            RegistrarError(ex)
            Retorno.HayError = True
            Retorno.MensajeError = "No se completo el Proceso [OUF]"
        End Try
        Return Retorno
    End Function

End Class
