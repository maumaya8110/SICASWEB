Imports System.IO
Imports System.Net
Imports System.Xml
Imports CascoFacturacionLib
Imports System.Text
Imports System.Data.SqlClient

Public Module Negocio

    Private Function Procesar(sp As String, ParamArray parametros() As Object) As ResultadoConsulta
        Dim dtResultado As DataTable = SQLHelper.GetTable(sp, parametros)
        If Not dtResultado Is Nothing AndAlso dtResultado.Rows.Count > 0 Then
            Dim dr As DataRow = dtResultado.Rows(0)
            Dim r As New ResultadoConsulta
            If Not dr("Resultado") Is DBNull.Value Then r.Resultado = CBool(dr("Resultado"))
            If Not String.IsNullOrEmpty(dr("Mensaje")) Then r.Mensaje = dr("Mensaje")
            If Not String.IsNullOrEmpty(dr("Valor")) Then r.Valor = dr("Valor")
            Return r
        End If
        Return Nothing
    End Function

    Function ExisteUsuario(usuario As String, contrasena As String) As ResultadoConsulta
        Return Procesar("Ff_ExisteUsuario", usuario, contrasena)
    End Function

    Function TipoUsuario(usuarioID As String) As TipoUsuarioEnum
        Dim r As TipoUsuarioEnum = TipoUsuarioEnum.NoDefinido
        Try
            r = SQLHelper.ExecuteScalar("FF_ConsultaTipoUsuario", New Guid(usuarioID))
        Catch ex As Exception
            r = TipoUsuarioEnum.NoDefinido
        End Try
        Return r
    End Function

    Function ListadoRazonSocial(usuarioID As String, Optional clienteID As String = "") As DataTable
        If clienteID <> "" Then
            Return SQLHelper.GetTable("Ff_ClientesRazonSocial", New Guid(usuarioID), New Guid(clienteID))
        Else
            Return SQLHelper.GetTable("Ff_ClientesRazonSocial", New Guid(usuarioID))
        End If
    End Function

    Function ListadoEstacion() As DataTable
        Return SQLHelper.GetTable("Ff_Estaciones")
    End Function

    Function AgregarTicket(usuarioID As String, folioTicket As String, importe As Double) As ResultadoConsulta
        Return Procesar("Ff_AgregarTicket", New Guid(usuarioID), folioTicket, importe)
    End Function

    Function EliminarTicket(usuarioID As String, folioTicket As String) As ResultadoConsulta
        Return Procesar("Ff_EliminarTicket", usuarioID, folioTicket)
    End Function

    Function UltimaCaptura(usuarioID As String) As DataTable
        If (usuarioID.Length = 36) Then
            Return SQLHelper.GetTable("Ff_ConsUltaUltimaCaptura", New Guid(usuarioID))
        Else
            Return Nothing
        End If
    End Function

    Function Historial(usuarioID As String, Anio As Integer, Mes As Integer) As DataTable
        Return SQLHelper.GetTable("Ff_Historial", New Guid(usuarioID), Anio, Mes)
    End Function

    Public Sub EnviaMailAlAdministrador(message As String, aSPSessionID As String, token As String)
        Dim mh As MailHelper.SMTP = New MailHelper.SMTP(
            System.Configuration.ConfigurationManager.AppSettings("MailServer") _
            , CInt(System.Configuration.ConfigurationManager.AppSettings("MailPort")) _
            , System.Configuration.ConfigurationManager.AppSettings("MailUsername") _
            , System.Configuration.ConfigurationManager.AppSettings("MailPassword") _
            , System.Configuration.ConfigurationManager.AppSettings("MailAccount") _
            , System.Configuration.ConfigurationManager.AppSettings("MailFrom") _
            , False)
        Dim msgBody As String = "Ocurrió un error al intentar realizar la factura con el Token: " & token & Environment.NewLine & "Mensaje: " & message
        If Not mh.SendMail(System.Configuration.ConfigurationManager.AppSettings("MailAdministrador"), "Error en la Generación de Factura", msgBody, True, Nothing, 0) Then
            Debug.WriteLine("No se pudo enviar el Email al Administrador")
        End If
    End Sub

    Function HistorialLibre(usuarioID As String) As DataTable
        Return SQLHelper.GetTable("Ff_HistorialLibre", New Guid(usuarioID), Today.Month, Today.Year)
    End Function

    Function DatosFactura(usuarioID As String, token As String) As DataTable
        Return SQLHelper.GetTable("Ff_DatosFactura", New Guid(usuarioID), New Guid(token))
    End Function

    Function DatosFacturaToken(token As String) As DataTable
        Return SQLHelper.GetTable("Ff_DatosFacturaToken", New Guid(token))
    End Function

    Function ValidaFacturaCliente(usuarioID As String) As DataTable
        'Return Nothing
        Return SQLHelper.GetTable("Ff_ValidaFacturaCliente", New Guid(usuarioID))
    End Function

    Function DatosFacturacion(usuarioID As String, clienteID As String) As DataTable
        Return SQLHelper.GetTable("Ff_DatosFacturacion", New Guid(usuarioID), New Guid(clienteID))
    End Function

    Function DatosFacturacionTmp(usuarioID As String, clienteID As String) As DataTable
        Return SQLHelper.GetTable("Ff_DatosFacturacionTmp", New Guid(usuarioID), New Guid(clienteID))
    End Function

    Function DatosUsuario(usuarioID As String) As DataTable
        Return SQLHelper.GetTable("Ff_DatosUsuario", usuarioID)
    End Function

    Sub GuardarDatosFiscales(usuarioID As String, clienteID As String, aliasRazonSocial As String, razonSocial As String, RFC As String, calle As String,
                            numeroExterior As String, numeroInterior As String, colonia As String, codigoPostal As String, ciudad As String, localidad As String,
                            estado As String, paisID As Integer)
        If clienteID = "" Then clienteID = "00000000-0000-0000-0000-000000000000"
        SQLHelper.ExecuteNonQuery("Ff_EditarDatosFiscales", New Guid(usuarioID), New Guid(clienteID), aliasRazonSocial, razonSocial, RFC, calle,
                                  numeroExterior, numeroInterior, colonia, codigoPostal, ciudad, localidad, estado, paisID)
    End Sub

    Function GuardarDatosFiscalesTemporales(usuarioID As String, clienteID As String, razonSocial As String, RFC As String, calle As String, NumeroExt As String, NumeroInt As String,
                                            Colonia As String, CP As String, Ciudad As String, Localidad As String, Estado As String, correoElectronico As String) As DataTable
        Return SQLHelper.GetTable("Ff_DatosFiscalesTemporales", New Guid(usuarioID), New Guid(clienteID), razonSocial, RFC, calle, NumeroExt, NumeroInt, Colonia, CP, Ciudad, Localidad, Estado, correoElectronico)
    End Function

    Public Function FacturasCanceladasPorMes(dteFecha As Date) As DataTable
        Return SQLHelper.GetTable("FF_ConsultaFacturasCanceladas", dteFecha)
    End Function

    Function GetDatosFiscalesBySession(usuarioID As String) As DataTable
        If (usuarioID.Length = 36) Then
            Return SQLHelper.GetTable("Ff_GetDatosFiscalesTemporales", New Guid(usuarioID))
        Else
            Return Nothing
        End If
    End Function

    Sub EliminarRazonSocial(usuarioID As String, clienteID As String)
        SQLHelper.ExecuteNonQuery("Ff_EliminarRazonSocial", New Guid(usuarioID), New Guid(clienteID))
    End Sub

    Sub GuardarNombreUsuario(usuarioID As String, nombre As String)
        SQLHelper.ExecuteNonQuery("Ff_GuardarNombreUsuario", usuarioID, nombre)
    End Sub

    Function CambiarContrasena(usuarioID As String, contrasenaActual As String, contrasenaNueva As String) As ResultadoConsulta
        Return Procesar("Ff_CambiarContrasena", usuarioID, contrasenaActual, contrasenaNueva)
    End Function

    Function LimpiaRFC(rfc As String) As String
        Return rfc.Replace(" ", "").Replace("_", "").Replace("-", "").ToUpper
    End Function

    Function DirectorioNumerico(numero As Int64) As String
        Dim l As Integer = 8
        If numero.ToString.Length > l Then
            l = numero.ToString.Length
        End If
        Dim sNumero As String = numero.ToString.PadLeft(l, "0") '99,999,999
        Dim o As Char() = sNumero.ToCharArray
        Dim sb As New Text.StringBuilder
        For i As Integer = 0 To (l - 3)
            If Not sb.Length = 0 Then sb.Append("/")
            sb.Append(o(i).ToString.PadRight(8 - i, "0"))
        Next
        sb.Append("/")
        sb.Append(sNumero.Substring((l - 2), 2))
        Return sb.ToString
    End Function

    Function Facturar(usuarioID As String, clienteID As String, HttpContext As System.Web.HttpContextBase) As ResultadoConsulta
        Dim r As New ResultadoConsulta
        r.Resultado = False
        r.Mensaje = "No hay tickets que facturar"
        Dim tokenFactura As Guid = Guid.NewGuid
        Dim facturaGenerada As Boolean = False
        Dim exFacturacion As Exception = Nothing
        Dim paso As String = "Inicio"

        Dim fechaFactura As DateTime = DateTime.UtcNow.AddHours(-6)
        Dim dtTickets As DataTable = Nothing
        Dim directorioCertificados As String = HttpContext.Server.MapPath("~/App_Data/Certificados")
        Dim directorioKeys As String = HttpContext.Server.MapPath("~/App_Data/Certificados")
        Dim directorioFacturas As String = HttpContext.Server.MapPath("~/App_Data/Facturas")
        Dim archivoXMLSellado As String = ""
        Dim archivoXMLTimbrado As String = ""
        Dim archivoPDF As String = ""
        Dim serie As String = ""
        Dim folio As Integer = 0

        If exFacturacion Is Nothing Then
            Try
                paso = "Bloquea tickets"
                dtTickets = SQLHelper.GetTable("FF_Facturar", New Guid(usuarioID), New Guid(clienteID), tokenFactura, fechaFactura.AddSeconds(-10))
            Catch ex As Exception
                MKSLibrary.Funciones.Modulo("Negocio")
                MKSLibrary.Funciones.Funcion("Facturar")
                Dim ex_aux = New Exception("No se pudo obtener acceso exclusivo a los tickets para facturarlos " & ex.Message & ex.StackTrace, ex)
                MKSLibrary.Funciones.RegistrarError(ex_aux)

                exFacturacion = ex
                r.Resultado = False
                r.Mensaje = String.Format("No se pudo obtener acceso exclusivo a los tickets para facturarlos. ({0})", paso)
                Console.WriteLine(paso)
            End Try
        End If
        Console.WriteLine("Ejecutó Facturar")
        Dim comprobanteOK As Boolean = False
        Dim Rec As New CFDI32.ComprobanteReceptor
        Dim Emisor As New CFDI32.ComprobanteEmisor
        Dim pFactura As New SelloCFDI.ParamFactura

        If exFacturacion Is Nothing Then
            Console.WriteLine("Crea Comprobante")
            Try
                paso = "Crea comprobante"
                If Not dtTickets Is Nothing AndAlso dtTickets.Rows.Count > 0 Then
                    Console.WriteLine("Si hay tickets")
                    Rec.nombre = dtTickets.Rows(0)("Receptor_Nombre").Trim.ToUpper
                    Rec.rfc = dtTickets.Rows(0)("Receptor_RFC").Trim.ToUpper
                    Rec.Domicilio = New CFDI32.t_Ubicacion
                    Rec.Domicilio.calle = dtTickets.Rows(0)("Receptor_Calle").Trim.ToUpper
                    Rec.Domicilio.colonia = dtTickets.Rows(0)("Receptor_Colonia").Trim.ToUpper
                    Rec.Domicilio.noExterior = dtTickets.Rows(0)("Receptor_NoExterior").Trim.ToUpper
                    Rec.Domicilio.noInterior = dtTickets.Rows(0)("Receptor_NoInterior").Trim.ToUpper
                    Rec.Domicilio.localidad = dtTickets.Rows(0)("Receptor_Localidad").Trim.ToUpper
                    Rec.Domicilio.municipio = dtTickets.Rows(0)("Receptor_Ciudad").Trim.ToUpper
                    Rec.Domicilio.estado = dtTickets.Rows(0)("Receptor_Estado").Trim.ToUpper
                    Rec.Domicilio.pais = dtTickets.Rows(0)("Receptor_Pais").Trim.ToUpper
                    Rec.Domicilio.codigoPostal = dtTickets.Rows(0)("Receptor_CodigoPostal").Trim.ToUpper

                    Emisor.nombre = dtTickets.Rows(0)("Emisor_Nombre").Trim.ToUpper
                    Emisor.rfc = dtTickets.Rows(0)("Emisor_RFC").Trim.ToUpper
                    Emisor.DomicilioFiscal = New CFDI32.t_UbicacionFiscal
                    Emisor.DomicilioFiscal.calle = dtTickets.Rows(0)("Emisor_Calle").Trim.ToUpper
                    Emisor.DomicilioFiscal.colonia = dtTickets.Rows(0)("Emisor_Colonia").Trim.ToUpper
                    Emisor.DomicilioFiscal.noExterior = dtTickets.Rows(0)("Emisor_NoExterior").Trim.ToUpper
                    Emisor.DomicilioFiscal.noInterior = dtTickets.Rows(0)("Emisor_NoInterior").Trim.ToUpper
                    Emisor.DomicilioFiscal.localidad = dtTickets.Rows(0)("Emisor_Localidad").Trim.ToUpper
                    Emisor.DomicilioFiscal.municipio = dtTickets.Rows(0)("Emisor_Ciudad").Trim.ToUpper
                    Emisor.DomicilioFiscal.estado = dtTickets.Rows(0)("Emisor_Estado").Trim.ToUpper
                    Emisor.DomicilioFiscal.pais = dtTickets.Rows(0)("Emisor_Pais").Trim.ToUpper
                    Emisor.DomicilioFiscal.codigoPostal = dtTickets.Rows(0)("Emisor_CodigoPostal").Trim.ToUpper

                    pFactura.RegimenFiscal = dtTickets.Rows(0)("Emisor_RegimenFiscal").Trim.ToUpper
                    pFactura.DesglozaIVA = dtTickets.Rows(0)("DesglozaIVA")
                    pFactura.CorreoElectronicoReceptor = dtTickets.Rows(0)("CorreoElectronicoReceptor").Trim.ToLower()
                    pFactura.CorreoElectronicoEmisor = dtTickets.Rows(0)("CorreoElectronicoEmisor").Trim.ToLower()
                    pFactura.Folio = dtTickets.Rows(0)("FolioFactura").ToString().Trim.ToLower()
                    pFactura.Serie = dtTickets.Rows(0)("SerieFactura").ToString().Trim.ToUpper()
                    pFactura.CondicionesDePago = dtTickets.Rows(0)("CondicionesPago").ToString().Trim.ToUpper()
                    pFactura.FormaDePago = dtTickets.Rows(0)("FormaPago").ToString().Trim.ToUpper()

                    pFactura.NumCtaPago = dtTickets.Rows(0)("NumCtaPago").ToString().Trim.ToUpper()

                    pFactura.Version = dtTickets.Rows(0)("Version").ToString().Trim.ToUpper()
                    pFactura.LugarExpedicion = dtTickets.Rows(0)("LugarExpedicion").ToString().Trim
                    pFactura.Moneda = dtTickets.Rows(0)("Moneda").ToString().Trim
                    pFactura.Unidad = dtTickets.Rows(0)("Unidad").ToString().Trim
                    pFactura.ArchivoCER = directorioCertificados + "\" + dtTickets.Rows(0)("ArchivoCER").ToString().Trim
                    pFactura.ArchivoKEY = directorioKeys + "\" + dtTickets.Rows(0)("ArchivoKEY").ToString().Trim
                    pFactura.Contrasena = dtTickets.Rows(0)("Contrasena").ToString().Trim
                    pFactura.WSURL = dtTickets.Rows(0)("WSURL").ToString().Trim
                    pFactura.WSUsuario = dtTickets.Rows(0)("WSUsuario").ToString().Trim
                    pFactura.WSContrasena = dtTickets.Rows(0)("WSContrasena").ToString().Trim
                    pFactura.WSArchivoCER = dtTickets.Rows(0)("WSArchivoCER").ToString().Trim
                    pFactura.WSArchivoCERContrasena = dtTickets.Rows(0)("WSArchivoCERContrasena").ToString().Trim
                    pFactura.PathFacturas = directorioFacturas
                    pFactura.TxtConcepto = dtTickets.Rows(0)("Descripcion").ToString().Trim
                    pFactura.TasaIVA = dtTickets.Rows(0)("TasaIVA")
                    pFactura.Token = dtTickets.Rows(0)("Token").ToString().Trim
                    pFactura.WsurlCancelacion = dtTickets.Rows(0)("WSURLCancelacion").ToString().Trim
                    pFactura.UrlImagen = dtTickets.Rows(0)("urlImagen").ToString().Trim

                    comprobanteOK = True
                End If
            Catch ex As Exception
                MKSLibrary.Funciones.Modulo("Negocio")
                MKSLibrary.Funciones.Funcion("Facturar")
                Dim ex_aux = New Exception("Ocurrió un error en la creación del comprobante " & ex.Message & ex.StackTrace, ex)
                MKSLibrary.Funciones.RegistrarError(ex_aux)

                exFacturacion = ex
                comprobanteOK = False
                r.Resultado = False
                r.Mensaje = String.Format("Ocurrió un error en la creación del comprobante. ({0})", paso)
            End Try
        End If

        '------GMD Código MK Solutions 20160313

        Dim lservicios As New List(Of CascoFacturacionLib.ServicioFacturacion)
        For Each row As DataRow In dtTickets.Rows
            Dim s As CascoFacturacionLib.ServicioFacturacion = New CascoFacturacionLib.ServicioFacturacion
            s.Cliente = row("Receptor_Nombre").ToString().Trim.ToUpper
            s.FechaEjecucion = CType(row("FechaCaptura").ToString(), DateTime)
            s.MetodoPago.Add(row("MetodoPago").ToString().Trim.ToUpper)
            s.Precio = CType(row("Importe").ToString(), Double)
            s.Reservacion = row("Folio").ToString()
            s.ServicioID = row("Folio").ToString()
            lservicios.Add(s)
        Next

        For Each row As DataRow In dtTickets.Rows
            If (pFactura.MetodoDePago.Contains(row("MetodoPago").ToString().Trim.ToUpper) = False) Then
                pFactura.MetodoDePago.Add(row("MetodoPago").ToString().Trim.ToUpper)
            End If
        Next

        Dim Resultado As New ResultadoProceso(Of List(Of CFDIGenerado))
        Resultado = FacturacionProceso.GenerarCFDIs(Rec, Emisor, pFactura, lservicios)

        If Not Resultado.HayError Then
            facturaGenerada = True
            paso = "Registra Archivo PDF"
            Try
                Dim pdfName As String = Resultado.Resultado(0).PDFName
                SQLHelper.ExecuteNonQuery("[dbo].[Ff_RegistraPDF]", tokenFactura, pdfName)
            Catch ex As Exception
                MKSLibrary.Funciones.Modulo("Negocio")
                MKSLibrary.Funciones.Funcion("Facturar")
                Dim ex_aux = New Exception("Ocurrió un error al intentar cerrar la factura " & ex.Message & ex.StackTrace, ex)
                MKSLibrary.Funciones.RegistrarError(ex_aux)

                exFacturacion = ex
                r.Resultado = False
                r.Mensaje = String.Format("Ocurrió un error al intentar cerrar la factura. ({0})", paso)
            End Try

            paso = "Registra Timbre"
            Try
                Dim xmlName = Resultado.Resultado(0).XMLName
                SQLHelper.ExecuteNonQuery("[dbo].[FF_RegistraTimbre]", tokenFactura, New Guid(Resultado.Resultado(0).UUID), Resultado.Resultado(0).Fecha, xmlName, Resultado.Resultado(0).Monto)
            Catch ex As Exception
                MKSLibrary.Funciones.Modulo("Negocio")
                MKSLibrary.Funciones.Funcion("Facturar")
                Dim ex_aux = New Exception("Ocurrió un error al intentar cerrar la factura " & ex.Message & ex.StackTrace, ex)
                MKSLibrary.Funciones.RegistrarError(ex_aux)

                exFacturacion = ex
                r.Resultado = False
                r.Mensaje = String.Format("Ocurrió un error al intentar cerrar la factura. ({0})", paso)
            End Try

            paso = "Cierra Factura"
            Try
                SQLHelper.ExecuteNonQuery("FF_CierraFactura", New Guid(usuarioID), tokenFactura)
            Catch ex As Exception
                MKSLibrary.Funciones.Modulo("Negocio")
                MKSLibrary.Funciones.Funcion("Facturar")
                Dim ex_aux = New Exception("Ocurrió un error al intentar cerrar la factura " & ex.Message & ex.StackTrace, ex)
                MKSLibrary.Funciones.RegistrarError(ex_aux)

                exFacturacion = ex
                r.Resultado = False
                r.Mensaje = String.Format("Ocurrió un error al intentar cerrar la factura. ({0})", paso)
            End Try

            r.Mensaje = ""
            r.Valor = tokenFactura.ToString()
        End If

        '------GMD Código MK Solutions 20160313

        If Not facturaGenerada Then
            Dim mensajeError As String = "L-349 "
            mensajeError += Resultado.MensajeError
            If Not exFacturacion Is Nothing Then mensajeError += exFacturacion.Message + " Stack:" + exFacturacion.StackTrace
            r.Mensaje = mensajeError
            SQLHelper.ExecuteNonQuery("Ff_LiberaTickets", New Guid(usuarioID), New Guid(clienteID), tokenFactura, 1, mensajeError)
        Else
            r.Resultado = True
        End If

        Return r

    End Function

    Function RecuperarContrasena(correoElectronico As String) As ResultadoConsulta
        Return Procesar("Ff_RecuperarContrasena", correoElectronico)
    End Function

    Function RecibeTicket(usuario As String, contrasena As String, folio As String, monto As Decimal, fecha As Date) As ResultadoConsulta
        Return Procesar("FF_RecibeTicket", usuario, contrasena, folio, monto, fecha)
    End Function

    Function ConsultaFacturas(usuario As String, contrasena As String, fechaInicial As Date, fechaFinal As Date) As ResultadoConsulta
        Dim r As ResultadoConsulta = Procesar("FF_ValidaUsuarioWS", usuario, contrasena)
        If r.Resultado = True Then
            r.Datos = SQLHelper.GetTableWithName("Consulta", "FF_ConsultaFacturas", fechaInicial, fechaFinal)
            r.Mensaje = "Consulta exitosa"
        End If
        Return r
    End Function

    Function ConsultaFacturas(fechaInicial As Date, serie As String, folio As String, servicio As String, rfc As String, mail As String) As DataTable
        Return SQLHelper.GetTable("FF_ConsultaFacturacion", fechaInicial, serie, folio, servicio, rfc, mail)
    End Function

    Function ConsultaFacturas(fechaInicial As Date) As DataTable
        Return SQLHelper.GetTable("FF_ConsultaFacturacion", fechaInicial)
    End Function

    Function ConsultaFacturasFolio(usuario As String, contrasena As String, folioInicial As Integer, folioFinal As Integer) As ResultadoConsulta
        Dim r As ResultadoConsulta = Procesar("FF_ValidaUsuarioWS", usuario, contrasena)
        If r.Resultado = True Then
            r.Datos = SQLHelper.GetTableWithName("Consulta", "FF_ConsultaFacturasFolio", folioInicial, folioFinal)
            r.Mensaje = "Consulta exitosa"
        End If
        Return r
    End Function

    Sub RegistraTicketFactura(noIdentificacion As String, valorUnitario As Decimal, tokenFactura As Guid, folio As String, xmlUUID As Guid, xmlSerie As String, xmlFolio As String)
        SQLHelper.ExecuteNonQuery("RegistraTicketFactura", noIdentificacion, valorUnitario, tokenFactura, folio, xmlUUID, xmlSerie, xmlFolio)
    End Sub

    Function CancelarFactura(pUsuarioID As String, pserie As String, pFolio As String) As ResultadoConsulta
        Dim usuario As New Guid(pUsuarioID)
        Dim dt As DataTable = SQLHelper.GetTable("FF_FacturaACancelar", pFolio, pserie)

        If (dt.Rows.Count > 0) Then
            Dim rfc_emisor As String = dt.Rows(0)("Emisor_RFC")
            Dim uuid As String = dt.Rows(0)("UUID").ToString()
            Dim dtEmisor As DataTable = SQLHelper.GetTable("FF_URLCancelacion", rfc_emisor)
            Dim urlCancelacion As String = dtEmisor.Rows(0)("WSURLCancelacion").ToString()
            Dim token As String = dtEmisor.Rows(0)("Token").ToString()
            'Se cancela la factura con DIVERZA
            Dim url As String = ""
            Dim responseCode As Integer = -1
            Dim xmlhttp As MSXML.XMLHTTPRequest = Nothing
            xmlhttp = New MSXML.XMLHTTPRequest
            url = urlCancelacion & "/" & rfc_emisor & "/" & uuid
            If url.Trim.Length <= 0 Then
                Dim ret As ResultadoConsulta = New ResultadoConsulta
                ret.Mensaje = "No se encontro la URL del Servicio de cancelación."
                ret.Resultado = False
                Return ret
            End If
            If url.Trim.Length <= 0 Then
                Dim ret As ResultadoConsulta = New ResultadoConsulta
                ret.Mensaje = "No se encontro el valor del Token para el servicio de cancelación."
                ret.Resultado = False
                Return ret
            End If

            xmlhttp.open("POST", url, False)
            xmlhttp.setRequestHeader("x-auth-token", token)
            xmlhttp.send()
            responseCode = xmlhttp.status
            If Not responseCode = 200 Then
                If responseCode <> 555 Then
                    Dim ret As ResultadoConsulta = New ResultadoConsulta
                    ret.Resultado = False
                    ret.Mensaje = "No se pudo Cancelar la Factura - " & xmlhttp.status & " - " & xmlhttp.responseText
                    Return ret
                End If
            End If
        End If
        Return Procesar("FF_CancelarFactura", usuario, pFolio)
    End Function

    Function ObtieneInformacionDelServicio(servicio As String) As DataTable
        Return SQLHelper.GetTable("dbo.FF_ConsultaServicio", servicio)
    End Function

    Function ObtieneEmisores() As DataTable
        Return SQLHelper.GetTable("dbo.FF_ConsultaEmisores")
    End Function

    Function ActualizaServicio(usuario_ID As String, servicio As CascoFacturacionLib.ServicioURGI) As ResultadoConsulta
        Return Procesar("FF_ActualizaFolio", New Guid(usuario_ID), servicio.Folio, servicio.Monto, servicio.RFC_Emisor, servicio.MetodoPago_ID, servicio.CtaBanco)
    End Function

End Module

