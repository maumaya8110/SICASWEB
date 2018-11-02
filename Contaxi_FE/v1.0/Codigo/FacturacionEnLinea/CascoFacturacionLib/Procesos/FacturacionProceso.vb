Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports System.IO
Imports System.Drawing
Imports System.Web.UI.WebControls

Public Class FacturacionProceso

    Public Shared Function GenerarCFDIs(Receptor As CFDI32.ComprobanteReceptor, Emisor As CFDI32.ComprobanteEmisor, pFactura As SelloCFDI.ParamFactura, Servicios As List(Of CascoFacturacionLib.ServicioFacturacion)) As ResultadoProceso(Of List(Of CFDIGenerado))

        Dim Retorno As New ResultadoProceso(Of List(Of CFDIGenerado))
        Try
            Retorno.Resultado = New List(Of CFDIGenerado)

            'Generar un CFDI para cada Proveedor
            Dim cfdi As CFDI32.Comprobante = CrearComprobanteSocio(Emisor, Servicios, Receptor, pFactura)

            If cfdi Is Nothing Then
                Retorno.HayError = True
                Retorno.MensajeError = "No se pudo generar el comprobante para el Socios: " & Emisor.rfc
            End If

            'Se genera el CFDI
            Dim CFDIProc As New ResultadoProceso(Of CFDIGenerado)
            CFDIProc = CFDIProceso.CrearCFDI(cfdi, pFactura)

            If CFDIProc.HayError Then
                MKSLibrary.Funciones.Modulo("Facturacion")
                MKSLibrary.Funciones.Funcion("GenerarCFDI Linea: " & 29)

                Retorno.HayError = True
                Retorno.MensajeError = CFDIProc.MensajeError
                Return Retorno
            End If

            'Se guardan archivos creados en carpeta de la aplicacion
            If CFDIProc.Resultado.ArchivoXML IsNot Nothing AndAlso CFDIProc.Resultado.ArchivoXML.Length > 0 Then
                'TODO: Quitar esta validacion cuando ya funcionen los servicios faltantes
                If File.Exists(pFactura.PathFacturas & "\" & CFDIProc.Resultado.UUID & ".xml") Then
                    File.Delete(pFactura.PathFacturas & "\" & CFDIProc.Resultado.UUID & ".xml")
                End If

                File.WriteAllBytes(pFactura.PathFacturas & "\" & CFDIProc.Resultado.UUID & ".xml", CFDIProc.Resultado.ArchivoXML)
            End If

            If CFDIProc.Resultado.ArchivoPDF IsNot Nothing AndAlso CFDIProc.Resultado.ArchivoPDF.Length > 0 Then
                'TODO: Quitar esta validacion cuando ya funcionen los servicios faltantes
                If File.Exists(pFactura.PathFacturas & "\" & CFDIProc.Resultado.UUID & ".pdf") Then
                    File.Delete(pFactura.PathFacturas & "\" & CFDIProc.Resultado.UUID & ".pdf")
                End If

                File.WriteAllBytes(pFactura.PathFacturas & "\" & CFDIProc.Resultado.UUID & ".pdf", CFDIProc.Resultado.ArchivoPDF)
            End If

            'Se registra en la BD el servicio facturado
            Dim Fac As New Facturacion
            Fac.RFCReceptor = cfdi.Receptor.rfc
            Fac.Serie = cfdi.serie
            Fac.Folio = cfdi.folio
            Fac.UUID = CFDIProc.Resultado.UUID
            Fac.AccountID = -1

            CFDIProc.Resultado.PDFName = CFDIProc.Resultado.UUID & ".pdf"
            CFDIProc.Resultado.XMLName = CFDIProc.Resultado.UUID & ".xml"

            Retorno.Resultado.Add(CFDIProc.Resultado)

            'Enviar por Email cada CFDI
            Dim em As New List(Of String)
            em.Add(pFactura.CorreoElectronicoReceptor)

            'Adjuntos
            Dim Adjuntos As New List(Of String)
            Adjuntos.Add(pFactura.PathFacturas & "\" & CFDIProc.Resultado.UUID & ".xml")
            Adjuntos.Add(pFactura.PathFacturas & "\" & CFDIProc.Resultado.UUID & ".pdf")

            'If Not Funciones.EnviarEmail(em, "Generación de Facturas", "Se ha realizado la Generación de la Factura: " & CFDIProc.Resultado.UUID, Adjuntos) Then
            Dim mh As MailHelper.SMTP = New MailHelper.SMTP(
     Configuration.ConfigurationManager.AppSettings("MailServer") _
     , CInt(Configuration.ConfigurationManager.AppSettings("MailPort")) _
     , Configuration.ConfigurationManager.AppSettings("MailUsername") _
     , Configuration.ConfigurationManager.AppSettings("MailPassword") _
     , Configuration.ConfigurationManager.AppSettings("MailAccount") _
     , Configuration.ConfigurationManager.AppSettings("MailFrom") _
     , False)

            'If Not (em, "Generación de Facturas", "Se ha realizado la Generación de la Factura: " & CFDIProc.Resultado.UUID, Adjuntos) Then
            If Not mh.SendMail(em(0).ToLower, "Generación de Factura", "Se ha realizado la Generación de la Factura: " & CFDIProc.Resultado.UUID, True, Adjuntos, 0) Then
                Debug.WriteLine("No se pudo enviar el Email para el Socio: " & Emisor.rfc)
                Retorno.HayError = True
                Retorno.MensajeError = "No se pudo enviar el Email para el Socio: " & Emisor.rfc
            End If

        Catch ex As Exception
            Modulo("FacturacionProceso")
            Funcion("GenerarCFDIs")
            Dim ex_aux = New Exception(ex.Message & ex.StackTrace, ex)
            RegistrarError(ex_aux)
            Retorno.HayError = True
            Retorno.MensajeError = "No se pudo generar la Facturación [EX2]"
        End Try

        Return Retorno

    End Function

    Private Shared Function CrearComprobanteSocio(Emisor As CFDI32.ComprobanteEmisor, Servicios As List(Of CascoFacturacionLib.ServicioFacturacion), Receptor As CFDI32.ComprobanteReceptor, pFactura As SelloCFDI.ParamFactura) As CFDI32.Comprobante
        Try
            Dim Comp As New CFDI32.Comprobante

            Comp.Moneda = pFactura.Moneda
            Comp.fecha = DateTime.Now.AddSeconds(-10).ToString("yyyy/MM/ddTHH:mm:ss")
            Comp.LugarExpedicion = pFactura.LugarExpedicion
            Comp.version = pFactura.Version
            Comp.NumCtaPago = pFactura.NumCtaPago
            Comp.tipoDeComprobante = CFDI32.ComprobanteTipoDeComprobante.ingreso
            Comp.condicionesDePago = pFactura.CondicionesDePago
            Comp.metodoDePago = String.Join(",", pFactura.MetodoDePago)
            Comp.formaDePago = pFactura.FormaDePago

            If pFactura.DesglozaIVA Then
                Comp.subTotal = CDec(Format((Servicios.Sum(Function(s) s.Precio) / pFactura.TasaIVA), "0.00"))
            Else
                Comp.subTotal = CDec(Format((Servicios.Sum(Function(s) s.Precio)), "0.00"))
            End If

            Comp.total = CDec(Format(Servicios.Sum(Function(s) s.Precio), "0.00"))
            Comp.serie = pFactura.Serie
            Comp.folio = pFactura.Folio
            Comp.sello = ""
            Comp.certificado = ""
            Comp.noCertificado = ""

            'DATOS EMISOR
            Comp.Emisor = New CFDI32.ComprobanteEmisor
            Comp.Emisor.nombre = Emisor.nombre
            Comp.Emisor.rfc = Emisor.rfc
            Comp.Emisor.DomicilioFiscal = New CFDI32.t_UbicacionFiscal
            With Comp.Emisor.DomicilioFiscal
                .calle = Emisor.DomicilioFiscal.calle
                .codigoPostal = Emisor.DomicilioFiscal.codigoPostal
                .colonia = Emisor.DomicilioFiscal.colonia
                .estado = Emisor.DomicilioFiscal.estado
                .localidad = Emisor.DomicilioFiscal.localidad
                .municipio = Emisor.DomicilioFiscal.municipio
                .noExterior = Emisor.DomicilioFiscal.noExterior
                If Emisor.DomicilioFiscal.noInterior.Trim.Length > 0 Then
                    .noInterior = Emisor.DomicilioFiscal.noInterior
                End If
                .pais = Emisor.DomicilioFiscal.pais
            End With

            Dim Regimenes As New List(Of CFDI32.ComprobanteEmisorRegimenFiscal)
            Regimenes.Add(New CFDI32.ComprobanteEmisorRegimenFiscal)
            Regimenes(0).Regimen = pFactura.RegimenFiscal
            Comp.Emisor.RegimenFiscal = Regimenes.ToArray

            'DATOS RECEPTOR
            Comp.Receptor = New CFDI32.ComprobanteReceptor
            Comp.Receptor.Domicilio = New CFDI32.t_Ubicacion
            With Comp.Receptor.Domicilio
                .calle = Receptor.Domicilio.calle
                .codigoPostal = Receptor.Domicilio.codigoPostal
                .noExterior = Receptor.Domicilio.noExterior
                If Receptor.Domicilio.noInterior.Trim.Length > 0 Then
                    .noInterior = Receptor.Domicilio.noInterior
                End If
                .colonia = Receptor.Domicilio.colonia
                .localidad = Receptor.Domicilio.localidad
                .municipio = Receptor.Domicilio.municipio
                .estado = Receptor.Domicilio.estado
                .pais = Receptor.Domicilio.pais
            End With
            Comp.Receptor.rfc = Receptor.rfc
            Comp.Receptor.nombre = Receptor.nombre

            'CONCEPTOS(SERVICIOS)
            Dim Conceptos As New List(Of CFDI32.ComprobanteConcepto)
            For Each s As CascoFacturacionLib.ServicioFacturacion In Servicios
                'Agregar Conceptos
                Dim Cpto As New CFDI32.ComprobanteConcepto
                Cpto.cantidad = 1
                Cpto.unidad = pFactura.Unidad
                Dim Desc As String = ""
                Cpto.descripcion = pFactura.TxtConcepto.Trim() & " #" & s.ServicioID.ToString()
                Cpto.valorUnitario = s.Precio
                Cpto.importe = s.Precio
                Conceptos.Add(Cpto)
            Next
            Comp.Conceptos = Conceptos.ToArray

            'IMPUESTOS
            Comp.Impuestos = New CFDI32.ComprobanteImpuestos
            Dim ImpTraslado As New List(Of CFDI32.ComprobanteImpuestosTraslado)
            Dim Iva As New CFDI32.ComprobanteImpuestosTraslado
            Iva.impuesto = CFDI32.ComprobanteImpuestosTrasladoImpuesto.IVA

            If pFactura.DesglozaIVA Then
                Iva.tasa = 1 - pFactura.TasaIVA
                Iva.importe = (Servicios.Sum(Function(s) s.Precio) * pFactura.TasaIVA)
            Else
                Iva.tasa = 0
                Iva.importe = CDec(Format(0, "0.00"))
            End If

            ImpTraslado.Add(Iva)
            Comp.Impuestos.Traslados = ImpTraslado.ToArray
            Return Comp
        Catch ex As Exception
            Modulo("FacturacionProceso")
            Funcion("FacturacionProceso")
            Dim ex_aux = New Exception(ex.Message & ex.StackTrace, ex)
            RegistrarError(ex_aux)
            Return Nothing
        End Try
    End Function

    Public Shared Function ObtenerSigFolioSocio(ID As Integer) As ResultadoProceso(Of Integer)
        Dim Retorno As New ResultadoProceso(Of Integer)
        Try

            Using CnnObj As Object = Conexion.Conectar

                If CnnObj IsNot Nothing Then

                    Dim Cnn As IConexion = CnnObj

                    Dim SO As New SocioOperativo
                    SO.SocioOperativoID = ID

                    Dim Fol As Long = 0
                    Fol = AccesoDatos.ObtenerSigFolioSocioOperativo(Cnn, SO)

                    If Fol > 0 Then
                        Retorno.Resultado = Fol
                    Else
                        Retorno.HayError = True
                        Retorno.MensajeError = "No se pudo obtener el valor del Folio"
                    End If

                Else
                    Retorno.HayError = True
                    Retorno.MensajeError = "No hay conexión [OSFS]"
                End If

            End Using

        Catch ex As Exception
            Modulo("FacturacionProceso")
            Funcion("ObtenerSigFolioSocio")
            Dim ex_aux = New Exception(ex.Message & ex.StackTrace, ex)
            RegistrarError(ex_aux)
            Retorno.HayError = True
            Retorno.MensajeError = "No se completo el proceso [OSFS]"
        End Try
        Return Retorno
    End Function

    Public Shared Function ObtenerSocioOperativo(Unidad As Integer) As ResultadoProceso(Of Integer)
        Dim Retorno As New ResultadoProceso(Of Integer)
        Try

            Using CnnObj As Object = Conexion.Conectar

                If CnnObj IsNot Nothing Then

                    Dim Cnn As IConexion = CnnObj

                    Dim SO As New UnidadSocioOperativo
                    SO.Unidad = Unidad

                    Dim Fol As List(Of UnidadSocioOperativo) = AccesoDatos.ObtenerUnidadSocioOperativo(Cnn, SO)

                    If Fol Is Nothing Then
                        Retorno.HayError = True
                        Retorno.MensajeError = "No se pudo obtener consultar la información del Socio Operativo"
                    ElseIf Fol.Count <= 0 Then
                        Retorno.HayError = True
                        Retorno.MensajeError = "No se encontro la información del Socio Operativo con N° de Unidad " & Unidad & "."
                    Else
                        Retorno.Resultado = Fol.FirstOrDefault().SocioOperativoID
                    End If

                Else
                    Retorno.HayError = True
                    Retorno.MensajeError = "No hay conexión [OSFS]"
                End If

            End Using

        Catch ex As Exception
            Modulo("FacturacionProceso")
            Funcion("ObtenerSigFolioSocio")
            Dim ex_aux = New Exception(ex.Message & ex.StackTrace, ex)
            RegistrarError(ex_aux)
            Retorno.HayError = True
            Retorno.MensajeError = "No se completo el proceso [OSFS]"
        End Try
        Return Retorno
    End Function

End Class
