Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports System.IO
Imports System.Drawing
Imports System.Web.UI.WebControls
Imports System.Xml.Serialization
Imports System.Xml.Linq
Imports System.Xml
Imports System.Web


Public Class CFDIProceso

    Public Shared Function CrearCFDI(Comp As CFDI32.Comprobante, pFactura As SelloCFDI.ParamFactura) As ResultadoProceso(Of CFDIGenerado)
        Dim Retorno As New ResultadoProceso(Of CFDIGenerado)

        Try
            Dim cfdiGen As New CFDIGenerado
            cfdiGen.Folio = Comp.folio
            Dim ComprobanteFinal As New CFDI32.Comprobante

            'Obtener el XML del CFDI Sin Firmar
            Dim xmlSerNS As New System.Xml.Serialization.XmlSerializerNamespaces
            xmlSerNS.Add("xsi", "http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/cfd/3/cfdv32.xsd")
            xmlSerNS.Add("xs", "http://www.w3.org/2001/XMLSchema-instance")
            xmlSerNS.Add("cfdi", "http://www.sat.gob.mx/cfd/3")

            Dim xmlSer As New System.Xml.Serialization.XmlSerializer(GetType(CFDI32.Comprobante))
            Dim xmlCFDI As Byte()

            Using MemStr As New MemoryStream
                Using strW As New StreamWriter(MemStr, System.Text.Encoding.UTF8)
                    xmlSer.Serialize(strW, Comp, xmlSerNS)
                    xmlCFDI = MemStr.ToArray
                End Using
            End Using

            '-----FIRMAR CFDI-----
            Dim xmlFirmado As String = FirmarCFDI(xmlCFDI, pFactura)

            Debug.WriteLine(xmlFirmado)

            If xmlFirmado.Trim.Length <= 0 Then
                MKSLibrary.Funciones.Modulo("CREAR CFDI")
                MKSLibrary.Funciones.Funcion("Firma CFDI")
                Dim ex_aux = New Exception("No se pudo firmar el cfdi ")
                MKSLibrary.Funciones.RegistrarError(ex_aux)

                Retorno.HayError = True
                Retorno.MensajeError = "No se pudo firmar el cfdi [1]"
                Return Retorno
                Dim smsg As String = "Cliente: " & pFactura.CorreoElectronicoReceptor & Environment.NewLine &
                Retorno.MensajeError & Environment.NewLine
                MailHelper.SMTP.EnviaMailError(System.Configuration.ConfigurationManager.AppSettings("MailAdministrador"), smsg)
            End If

            'Se obtienen los bytes del xml firmado
            Dim xmlCFDIFirmado As Byte() = System.Text.Encoding.UTF8.GetBytes(xmlFirmado)

            Using stream As New MemoryStream(xmlCFDIFirmado)
                stream.Seek(0, SeekOrigin.Begin)
                Dim serializer As New XmlSerializer(GetType(CFDI32.Comprobante))
                ComprobanteFinal = CType(serializer.Deserialize(stream), CFDI32.Comprobante)
            End Using

            Dim xmlTimbrado As String = ""
            'To Do probar funcionalidad de timbrado DIVERZA
            Dim RTimbre As New ResultadoProceso(Of String)
            RTimbre = TimbrarComprobante(xmlCFDIFirmado, pFactura)

            If Not RTimbre.HayError AndAlso RTimbre.Resultado.Trim.Length > 0 Then
                xmlTimbrado = RTimbre.Resultado
            Else
                MKSLibrary.Funciones.Modulo("CREAR CFDI")
                MKSLibrary.Funciones.Funcion("Timbrar CFDI")
                Dim ex_aux = New Exception(RTimbre.MensajeError)
                MKSLibrary.Funciones.RegistrarError(ex_aux)

                Retorno.HayError = True
                Retorno.MensajeError = RTimbre.MensajeError
                Return Retorno
                Dim smsg As String = "Cliente: " & pFactura.CorreoElectronicoReceptor & Environment.NewLine &
                RTimbre.MensajeError & Environment.NewLine
                MailHelper.SMTP.EnviaMailError(System.Configuration.ConfigurationManager.AppSettings("MailAdministrador"), smsg)
            End If

            'Bytes del timbre fiscal
            Dim xmlCFDITimbre As Byte() = System.Text.Encoding.UTF8.GetBytes(xmlTimbrado)
            Dim XMLTimbre As New CFDI32.TimbreFiscal.TimbreFiscalDigital

            '-----TIMBRE FISCAL
            Using stream As New MemoryStream(xmlCFDITimbre)
                stream.Seek(0, SeekOrigin.Begin)
                Dim xDoc As New XmlDocument
                Dim nt As New NameTable()
                Dim nsmgr As New XmlNamespaceManager(nt)
                nsmgr.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance")

                ' Create the XmlParserContext.
                Dim context As New XmlParserContext(Nothing, nsmgr, Nothing, XmlSpace.None)

                ' Create the reader. 
                Dim settings As New XmlReaderSettings()
                settings.ConformanceLevel = ConformanceLevel.Fragment
                Dim reader As XmlReader = XmlReader.Create(New StringReader(xmlTimbrado), settings, context)

                Dim serializer As New XmlSerializer(GetType(CFDI32.TimbreFiscal.TimbreFiscalDigital))
                XMLTimbre = CType(serializer.Deserialize(reader), CFDI32.TimbreFiscal.TimbreFiscalDigital)

            End Using

            '-----COMPROBANTE FINAL
            Using stream As New MemoryStream(xmlCFDITimbre)
                stream.Seek(0, SeekOrigin.Begin)
                Dim xDoc As New XmlDocument
                Dim nt As New NameTable()
                Dim nsmgr As New XmlNamespaceManager(nt)
                nsmgr.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance")

                ' Create the XmlParserContext.
                Dim context As New XmlParserContext(Nothing, nsmgr, Nothing, XmlSpace.None)

                ' Create the reader. 
                Dim settings As New XmlReaderSettings()
                settings.ConformanceLevel = ConformanceLevel.Fragment
                Dim reader As XmlReader = XmlReader.Create(New StringReader(xmlTimbrado), settings, context)
                xDoc.Load(reader)

                ComprobanteFinal.Complemento = New CFDI32.ComprobanteComplemento()
                ComprobanteFinal.Complemento.Any = New XmlElement() {xDoc.DocumentElement}
            End Using

            '-----ASIGNAR BYTES DE XML FINAL-----
            Dim xmlSerNSComprobante As New System.Xml.Serialization.XmlSerializerNamespaces
            xmlSerNSComprobante.Add("cfdi", "http://www.sat.gob.mx/cfd/3")
            xmlSerNSComprobante.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance")

            Dim xmlSerComprobante As New System.Xml.Serialization.XmlSerializer(GetType(CFDI32.Comprobante))
            Dim xmlCFDIComprobante As Byte()

            Using MemStr As New MemoryStream
                Using strW As New StreamWriter(MemStr, System.Text.Encoding.UTF8)
                    xmlSerComprobante.Serialize(strW, ComprobanteFinal, xmlSerNSComprobante)
                    xmlCFDIComprobante = MemStr.ToArray
                End Using
            End Using

            'Archivo xml creado en ruta temporal
            Dim CodigoTemp As String = MKSLibrary.Funciones.CodigoUnico(16)
            Dim RutaTemp As String = pFactura.PathFacturas & "\" & CodigoTemp & ".xml"
            File.WriteAllBytes(RutaTemp, xmlCFDIComprobante)

            Dim Cadena As String = ""
            Cadena = FirmaSAT.Tfd.MakePipeStringFromXml(RutaTemp)

            File.Delete(RutaTemp)
            If Cadena.Trim.Length <= 0 Then
                Retorno.HayError = True
                Retorno.MensajeError = "No se pudo generar la Cadena original del complemento de certificación digital del SAT"
                Return Retorno
            End If

            '-----CREACION DEL PDF-----
            Dim pdf As Byte() = CrearPDFComprobante(ComprobanteFinal, XMLTimbre, Cadena, pFactura)

            If pdf Is Nothing Then
                Retorno.HayError = True
                Retorno.MensajeError = "No se pudo crear el pdf"
                Return Retorno

                Dim smsg As String = "Cliente: " & pFactura.CorreoElectronicoReceptor & Environment.NewLine & Retorno.MensajeError
                MailHelper.SMTP.EnviaMailError(System.Configuration.ConfigurationManager.AppSettings("MailAdministrador"), smsg)
            End If

            cfdiGen.UUID = XMLTimbre.UUID
            cfdiGen.ArchivoXML = xmlCFDIComprobante
            cfdiGen.ArchivoPDF = pdf
            cfdiGen.Monto = Format(ComprobanteFinal.Conceptos.Sum(Function(c) c.importe), "0.00")
            cfdiGen.Cliente = Comp.Receptor.nombre

            'Regresar el Resultado
            Retorno.Resultado = cfdiGen
        Catch ex As Exception
            Modulo("CFDIProceso")
            Funcion("CrearCFDI")
            RegistrarError(ex)
            Debug.WriteLine(ex.Message)
            Retorno.HayError = True
            Retorno.MensajeError = "No se completo el proceso de generación del CFDI [CCFDI]"
            Dim smsg As String = "Cliente: " & pFactura.CorreoElectronicoReceptor & Environment.NewLine &
                Retorno.MensajeError & Environment.NewLine &
                "Exception: " & ex.Message & Environment.NewLine &
                "Stack: " & ex.StackTrace
            MailHelper.SMTP.EnviaMailError(System.Configuration.ConfigurationManager.AppSettings("MailAdministrador"), smsg)
        End Try
        Return Retorno
    End Function

    Public Shared Function FirmarCFDI(ByVal xml As Byte(), pFactura As SelloCFDI.ParamFactura) As String
        Dim keyFile As String = pFactura.ArchivoKEY
        Dim cerFile As String = pFactura.ArchivoCER
        Try
            If File.Exists(keyFile) AndAlso File.Exists(cerFile) Then
                Dim xmlFirmado As Byte() = FirmaSAT.Sat.SignXmlToBytes(xml, keyFile, pFactura.Contrasena, cerFile, FirmaSAT.SignOptions.Default)
                Dim strXML As String = System.Text.Encoding.UTF8.GetString(xmlFirmado)
                Debug.WriteLine(strXML)
                Return strXML
            End If

        Catch ex As Exception
            Modulo("CFDIProceso")
            Funcion("FirmarCFDI")
            Dim s As String = String.Format("Keyfile: {0}. cerFile: {1} mensaje: {2}, stack: {3}", keyFile, cerFile, ex.Message, ex.StackTrace)
            Dim ex_aux As Exception = New Exception(s, ex)
            RegistrarError(ex)
            Debug.WriteLine(ex.Message)
        End Try

        Return ""

    End Function



    Public Shared Function RepresentacionCFDI(xml As String) As ResultadoProceso(Of Byte())

        Dim Retorno As New ResultadoProceso(Of Byte())

        Try

        Catch ex As Exception

        End Try

        Return Retorno

    End Function

    Private Shared Function CrearPDFComprobante(cfdi As CFDI32.Comprobante, timbre As CFDI32.TimbreFiscal.TimbreFiscalDigital, cadena As String, pFactura As SelloCFDI.ParamFactura) As Byte()
        Try
            Dim Retorno() As Byte

            Dim DocPDF As Document
            Dim MemPDF As MemoryStream = Nothing
            Dim Bytes As New List(Of Byte)
            Dim MemZIP As MemoryStream = Nothing
            Dim PDFReaders As New List(Of PdfReader)

            DocPDF = New Document(PageSize.LETTER, 10, 10, 20, 20)
            MemPDF = New MemoryStream

            Dim Writer As PdfWriter = PdfWriter.GetInstance(DocPDF, MemPDF)
            Writer.CloseStream = False

            DocPDF.OpenDocument()

            ImprimirPagina(DocPDF, Writer, cfdi.Receptor, cfdi, timbre, cadena, pFactura)

            DocPDF.Close()

            PDFReaders.Add(New PdfReader(MemPDF.GetBuffer))

            MemPDF.Dispose()

            Dim MultiDocPDF As Document = New Document(PDFReaders.First.GetPageSizeWithRotation(1))
            Dim MultiMemPDF As New MemoryStream
            Dim MultiWriter As PdfWriter = PdfWriter.GetInstance(MultiDocPDF, MultiMemPDF)

            MultiWriter.CloseStream = False

            MultiDocPDF.Open()

            Dim MultiCB As PdfContentByte = MultiWriter.DirectContent
            Dim impPage As PdfImportedPage
            Dim Rotacion As Integer = 0

            For Each Rdr As PdfReader In PDFReaders

                For i = 1 To Rdr.NumberOfPages

                    MultiDocPDF.SetPageSize(Rdr.GetPageSizeWithRotation(i))
                    MultiDocPDF.NewPage()

                    impPage = MultiWriter.GetImportedPage(Rdr, i)

                    Rotacion = Rdr.GetPageRotation(i)

                    If Rotacion = 90 OrElse Rotacion = 270 Then

                        MultiCB.AddTemplate(impPage, 0, -1.0F, 1.0F, 0, 0, Rdr.GetPageSizeWithRotation(i).Height)

                    Else

                        MultiCB.AddTemplate(impPage, 1.0F, 0, 0, 1.0F, 0, 0)

                    End If

                Next

            Next

            MultiDocPDF.Close()

            Retorno = MultiMemPDF.GetBuffer

            Return Retorno 'TODO: arreglar el resultado a regresar

        Catch ex As Exception
            Modulo("FacturacionProceso")
            Funcion("CrearPDFComprobante")
            RegistrarError(ex)
            Return Nothing
        End Try
    End Function

    Private Shared Function ImprimirPagina(DocPDF As Document, writer As PdfWriter, Rec As CFDI32.ComprobanteReceptor, cfdi As CFDI32.Comprobante, timbre As CFDI32.TimbreFiscal.TimbreFiscalDigital, Cadena As String, pFactura As SelloCFDI.ParamFactura) As Boolean

        Try

            DocPDF.NewPage()
            Dim DesglozaIVA As Boolean = pFactura.DesglozaIVA
            Dim TamañoLetra As Single = 9

            Dim TablaHoja As New PdfPTable(1)
            TablaHoja.WidthPercentage = 100
            TablaHoja.DefaultCell.UseVariableBorders = True
            TablaHoja.DefaultCell.Border = itextsharp.text.Rectangle.RECTANGLE
            TablaHoja.DefaultCell.BorderColor = New itextsharp.text.BaseColor(192, 192, 192)

            Dim CellTablaVale As New PdfPCell

            'TABLA 1
            Dim Tabla1 As New PdfPTable(2)
            Tabla1.WidthPercentage = 100

            'Tabla1.DefaultCell.UseVariableBorders = True
            'Tabla1.DefaultCell.Border = iTextSharp.text.Rectangle.RECTANGLE
            'Tabla1.DefaultCell.BorderColor = New iTextSharp.text.BaseColor(192, 192, 192)
            'TABLA LOGO
            Dim tblLogo As New PdfPTable(1)
            tblLogo.WidthPercentage = 100
            tblLogo.HorizontalAlignment = HorizontalAlign.Center

            'Dim Img As System.Drawing.Image = System.Drawing.Image.FromFile(Funciones.RutaApp() & pFactura.UrlImagen) 'TODO: Aqui va la imagen 'HttpContext.Current.Server.MapPath("~/Imagenes/LogoHeader.png"))
            'Dim PdfImg As itextsharp.text.Image = itextsharp.text.Image.GetInstance(Img, System.Drawing.Imaging.ImageFormat.Jpeg)
            Dim ImgHeight As Single = 30
            Dim ImgWidth As Single '= ImgHeight * (PdfImg.Width / PdfImg.Height)
            'Dim pageWidth = DocPDF.PageSize.Width - (DocPDF.LeftMargin + DocPDF.RightMargin)
            'PdfImg.SetAbsolutePosition(DocPDF.LeftMargin, DocPDF.PageSize.Height - (ImgHeight + 30))
            'PdfImg.ScaleAbsolute(ImgWidth, ImgHeight)

            CellTablaVale = New PdfPCell() 'PdfImg
            CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
            tblLogo.AddCell(CellTablaVale)

            ''Se agrega log a tabla 1-------------------------------------------------------
            Tabla1.AddCell(tblLogo)

            'TABLA DATOS FACTURA
            Dim tblDatosFac As New PdfPTable(1)
            tblDatosFac.WidthPercentage = 100

            '1
            Dim celText As New Paragraph
            celText.Add("Folio Fiscal")
            celText.Font.Color = itextsharp.text.BaseColor.WHITE
            celText.Alignment = Element.ALIGN_CENTER
            celText.Font.Size = TamañoLetra

            CellTablaVale = New PdfPCell()
            CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
            CellTablaVale.BackgroundColor = itextsharp.text.BaseColor.BLACK
            CellTablaVale.AddElement(celText)
            tblDatosFac.AddCell(CellTablaVale)

            '2
            celText = New Paragraph
            celText.Add(timbre.UUID.ToUpper)
            celText.Alignment = Element.ALIGN_CENTER
            celText.Font.Size = TamañoLetra

            CellTablaVale = New PdfPCell()
            CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
            CellTablaVale.AddElement(celText)
            tblDatosFac.AddCell(CellTablaVale)

            '3
            celText = New Paragraph
            celText.Add("Factura Número")
            celText.Font.Color = itextsharp.text.BaseColor.WHITE
            celText.Alignment = Element.ALIGN_CENTER
            celText.Font.Size = TamañoLetra

            CellTablaVale = New PdfPCell()
            CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
            CellTablaVale.BackgroundColor = itextsharp.text.BaseColor.BLACK
            CellTablaVale.AddElement(celText)
            tblDatosFac.AddCell(CellTablaVale)

            '4
            celText = New Paragraph
            celText.Add(cfdi.serie & " " & cfdi.folio)
            celText.Alignment = Element.ALIGN_CENTER
            celText.Font.Size = TamañoLetra

            CellTablaVale = New PdfPCell()
            CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
            CellTablaVale.AddElement(celText)
            tblDatosFac.AddCell(CellTablaVale)

            '5
            celText = New Paragraph
            celText.Add("No. de serie del CSD del emisor")
            celText.Font.Color = itextsharp.text.BaseColor.WHITE
            celText.Alignment = Element.ALIGN_CENTER
            celText.Font.Size = TamañoLetra

            CellTablaVale = New PdfPCell()
            CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
            CellTablaVale.BackgroundColor = itextsharp.text.BaseColor.BLACK
            CellTablaVale.AddElement(celText)
            tblDatosFac.AddCell(CellTablaVale)

            '6
            celText = New Paragraph
            celText.Add(timbre.noCertificadoSAT)
            celText.Alignment = Element.ALIGN_CENTER
            celText.Font.Size = TamañoLetra

            CellTablaVale = New PdfPCell()
            CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
            CellTablaVale.AddElement(celText)
            tblDatosFac.AddCell(CellTablaVale)

            '7
            celText = New Paragraph
            celText.Add("Fecha y Hora de emisión")
            celText.Font.Color = itextsharp.text.BaseColor.WHITE
            celText.Alignment = Element.ALIGN_CENTER
            celText.Font.Size = TamañoLetra

            CellTablaVale = New PdfPCell()
            CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
            CellTablaVale.BackgroundColor = itextsharp.text.BaseColor.BLACK
            CellTablaVale.AddElement(celText)
            tblDatosFac.AddCell(CellTablaVale)

            '8
            celText = New Paragraph
            celText.Add(cfdi.fecha)
            celText.Alignment = Element.ALIGN_CENTER
            celText.Font.Size = TamañoLetra

            CellTablaVale = New PdfPCell()
            CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
            CellTablaVale.AddElement(celText)
            tblDatosFac.AddCell(CellTablaVale)

            'Se agrega log a tabla 1
            Tabla1.AddCell(tblDatosFac)

            'Se agrega primera parte de la Tabla principal
            TablaHoja.AddCell(Tabla1)

            'Proceso de Tabla 2------------------------------------------------------------------
            Dim Tabla2 As New PdfPTable(3)
            Tabla2.WidthPercentage = 100

            'TABLA FECHA Y HORA
            Dim tblDatosFh As New PdfPTable(1)
            tblDatosFh.WidthPercentage = 100

            celText = New Paragraph
            celText.Add("Fecha y Hora de certificación")
            celText.Font.Color = itextsharp.text.BaseColor.WHITE
            celText.Alignment = Element.ALIGN_CENTER
            celText.Font.Size = TamañoLetra

            CellTablaVale = New PdfPCell()
            CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
            CellTablaVale.BackgroundColor = itextsharp.text.BaseColor.BLACK
            CellTablaVale.AddElement(celText)
            tblDatosFh.AddCell(CellTablaVale)

            celText = New Paragraph
            celText.Add(cfdi.fecha)
            celText.Alignment = Element.ALIGN_CENTER
            celText.Font.Size = TamañoLetra

            CellTablaVale = New PdfPCell()
            CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
            CellTablaVale.AddElement(celText)
            tblDatosFh.AddCell(CellTablaVale)

            celText = New Paragraph
            celText.Add("Lugar de Expedición: " & cfdi.LugarExpedicion)
            celText.Font.Color = itextsharp.text.BaseColor.WHITE
            celText.Alignment = Element.ALIGN_CENTER
            celText.Font.Size = TamañoLetra

            CellTablaVale = New PdfPCell()
            CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
            CellTablaVale.BackgroundColor = itextsharp.text.BaseColor.BLACK
            CellTablaVale.AddElement(celText)
            tblDatosFh.AddCell(CellTablaVale)

            'Se agrega columna de fecha y hora de certificación
            Tabla2.AddCell(tblDatosFh)

            'TABLA no SERIE
            Dim tblSerie As New PdfPTable(1)
            tblSerie.WidthPercentage = 100

            celText = New Paragraph
            celText.Add("No. de serie del CSD del SAT")
            celText.Font.Color = itextsharp.text.BaseColor.WHITE
            celText.Alignment = Element.ALIGN_CENTER
            celText.Font.Size = TamañoLetra

            CellTablaVale = New PdfPCell()
            CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
            CellTablaVale.BackgroundColor = itextsharp.text.BaseColor.BLACK
            CellTablaVale.AddElement(celText)
            tblSerie.AddCell(CellTablaVale)

            celText = New Paragraph
            celText.Add(cfdi.noCertificado)
            celText.Alignment = Element.ALIGN_CENTER
            celText.Font.Size = TamañoLetra

            CellTablaVale = New PdfPCell()
            CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
            CellTablaVale.AddElement(celText)
            tblSerie.AddCell(CellTablaVale)

            'Se agrega columna de fecha y hora de certificación
            Tabla2.AddCell(tblSerie)

            'TABLA no SERIE
            Dim tblFPago As New PdfPTable(1)
            tblFPago.WidthPercentage = 100

            celText = New Paragraph
            celText.Add("Forma de Pago")
            celText.Font.Color = itextsharp.text.BaseColor.WHITE
            celText.Alignment = Element.ALIGN_CENTER
            celText.Font.Size = TamañoLetra

            CellTablaVale = New PdfPCell()
            CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
            CellTablaVale.BackgroundColor = itextsharp.text.BaseColor.BLACK
            CellTablaVale.AddElement(celText)
            tblFPago.AddCell(CellTablaVale)

            celText = New Paragraph
            celText.Add(cfdi.formaDePago)
            celText.Alignment = Element.ALIGN_CENTER
            celText.Font.Size = TamañoLetra

            CellTablaVale = New PdfPCell()
            CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
            CellTablaVale.AddElement(celText)
            tblFPago.AddCell(CellTablaVale)

            celText = New Paragraph
            celText.Add("Tipo de Cambio: " & Format(cfdi.TipoCambio, "0.00"))
            celText.Font.Color = itextsharp.text.BaseColor.WHITE
            celText.Font.Size = TamañoLetra

            CellTablaVale = New PdfPCell()
            CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
            CellTablaVale.BackgroundColor = itextsharp.text.BaseColor.BLACK
            CellTablaVale.AddElement(celText)
            tblDatosFh.AddCell(CellTablaVale)

            'Se agrega columna de fecha y hora de certificación
            Tabla2.AddCell(tblFPago)

            celText = New Paragraph
            celText.Add(cfdi.Emisor.RegimenFiscal(0).Regimen)
            celText.Alignment = Element.ALIGN_CENTER
            celText.Font.Color = itextsharp.text.BaseColor.BLACK
            celText.Font.Size = 8

            CellTablaVale = New PdfPCell()
            'CellTablaVale.BorderColor = iTextSharp.text.BaseColor.WHITE
            CellTablaVale.AddElement(celText)
            'CellTablaVale.FixedHeight = 15
            CellTablaVale.Colspan = 3
            Tabla2.AddCell(CellTablaVale)

            'Se agrega segunda parte de la Tabla principal
            TablaHoja.AddCell(Tabla2)

            'Proceso de Tabla 3-------------------------------------------------
            Dim Tabla3 As New PdfPTable(2)
            Tabla3.WidthPercentage = 100

            'TABLA EMISOR
            Dim tblDatosE As New PdfPTable(1)
            tblDatosE.WidthPercentage = 100

            celText = New Paragraph
            celText.Add("Emisor")
            celText.Font.Color = itextsharp.text.BaseColor.WHITE
            celText.Alignment = Element.ALIGN_CENTER
            celText.Font.Size = TamañoLetra

            CellTablaVale = New PdfPCell()
            CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
            CellTablaVale.BackgroundColor = itextsharp.text.BaseColor.BLACK
            CellTablaVale.AddElement(celText)
            tblDatosE.AddCell(CellTablaVale)

            celText = New Paragraph
            Dim nint_aux As String = ""
            If cfdi.Emisor.DomicilioFiscal.noInterior <> Nothing Then
                nint_aux = " " & cfdi.Emisor.DomicilioFiscal.noInterior.ToUpper()
            End If
            celText.Add(cfdi.Emisor.nombre.ToUpper & ", " & cfdi.Emisor.DomicilioFiscal.calle.ToUpper & ", " & cfdi.Emisor.DomicilioFiscal.noExterior.ToUpper & nint_aux & ", " & cfdi.Emisor.DomicilioFiscal.colonia.ToUpper & ", " & cfdi.Emisor.DomicilioFiscal.localidad.ToUpper & ", " & cfdi.Emisor.DomicilioFiscal.municipio.ToUpper & ", " & cfdi.Emisor.DomicilioFiscal.estado.ToUpper & ", CP. " & cfdi.Emisor.DomicilioFiscal.codigoPostal.ToUpper & ", " & cfdi.Emisor.DomicilioFiscal.pais.ToUpper & ", " & cfdi.Emisor.DomicilioFiscal.referencia & "  RFC: " & cfdi.Emisor.rfc.ToUpper)
            'celText.Add(cfdi.Emisor.nombre.ToUpper & ", " & cfdi.Emisor.DomicilioFiscal.calle.ToUpper & ", " & cfdi.Emisor.DomicilioFiscal.noExterior.ToUpper & " " & ", " & cfdi.Emisor.DomicilioFiscal.colonia.ToUpper & ", " & cfdi.Emisor.DomicilioFiscal.localidad.ToUpper & ", " & cfdi.Emisor.DomicilioFiscal.municipio.ToUpper & ", " & cfdi.Emisor.DomicilioFiscal.estado.ToUpper & ", CP. " & cfdi.Emisor.DomicilioFiscal.codigoPostal.ToUpper & ", " & cfdi.Emisor.DomicilioFiscal.pais.ToUpper & ", " & cfdi.Emisor.DomicilioFiscal.referencia & "  RFC: " & cfdi.Emisor.rfc.ToUpper)
            celText.Alignment = Element.ALIGN_LEFT
            celText.Font.Size = TamañoLetra

            CellTablaVale = New PdfPCell()
            CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
            CellTablaVale.AddElement(celText)
            tblDatosE.AddCell(CellTablaVale)

            Tabla3.AddCell(tblDatosE)

            'TABLA RECEPTOR
            Dim tblDatosR As New PdfPTable(1)
            tblDatosR.WidthPercentage = 100

            celText = New Paragraph
            celText.Add("Receptor")
            celText.Font.Color = itextsharp.text.BaseColor.WHITE
            celText.Alignment = Element.ALIGN_CENTER
            celText.Font.Size = TamañoLetra

            CellTablaVale = New PdfPCell()
            CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
            CellTablaVale.BackgroundColor = itextsharp.text.BaseColor.BLACK
            CellTablaVale.AddElement(celText)
            tblDatosR.AddCell(CellTablaVale)

            celText = New Paragraph
            nint_aux = ""
            If cfdi.Receptor.Domicilio.noInterior <> Nothing Then
                nint_aux = " " & cfdi.Receptor.Domicilio.noInterior.ToUpper()
            End If
            celText.Add(cfdi.Receptor.nombre & ", " & cfdi.Receptor.Domicilio.calle & ", " & cfdi.Receptor.Domicilio.noExterior & nint_aux & ", " & cfdi.Receptor.Domicilio.colonia & ", " & cfdi.Receptor.Domicilio.localidad & ", " & cfdi.Receptor.Domicilio.municipio & ", " & cfdi.Receptor.Domicilio.estado & ", CP. " & cfdi.Receptor.Domicilio.codigoPostal & ", " & cfdi.Receptor.Domicilio.pais & "  RFC: " & cfdi.Receptor.rfc)
            'celText.Add(cfdi.Receptor.nombre & ", " & cfdi.Receptor.Domicilio.calle & ", " & cfdi.Receptor.Domicilio.noExterior & ", " & cfdi.Receptor.Domicilio.colonia & ", " & cfdi.Receptor.Domicilio.localidad & ", " & cfdi.Receptor.Domicilio.municipio & ", " & cfdi.Receptor.Domicilio.estado & ", CP. " & cfdi.Receptor.Domicilio.codigoPostal & ", " & cfdi.Receptor.Domicilio.pais & "  RFC: " & cfdi.Receptor.rfc)
            celText.Alignment = Element.ALIGN_LEFT
            celText.Font.Size = TamañoLetra

            CellTablaVale = New PdfPCell()
            CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
            CellTablaVale.AddElement(celText)
            tblDatosR.AddCell(CellTablaVale)

            Tabla3.AddCell(tblDatosR)

            TablaHoja.AddCell(Tabla3)

            'Proceso de creacion de tabla 4------------------------------------------
            Dim con As Integer = 0
            Dim Subtotal As Double = 0

            Dim Tabla4 As New PdfPTable(7)

            Tabla4.WidthPercentage = 100
            Tabla4.SetWidths({30, 40, 160, 40, 40, 40, 50})

            For Each c As CFDI32.ComprobanteConcepto In cfdi.Conceptos

                If con = 0 Then
                    'Encabezado
                    celText = New Paragraph
                    celText.Add("Cantidad")
                    celText.Font.Color = itextsharp.text.BaseColor.WHITE
                    celText.Alignment = Element.ALIGN_CENTER
                    celText.Font.Size = TamañoLetra

                    CellTablaVale = New PdfPCell()
                    CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
                    CellTablaVale.BackgroundColor = itextsharp.text.BaseColor.BLACK
                    CellTablaVale.AddElement(celText)
                    Tabla4.AddCell(CellTablaVale)

                    celText = New Paragraph
                    celText.Add("Unidad")
                    celText.Font.Color = itextsharp.text.BaseColor.WHITE
                    celText.Alignment = Element.ALIGN_CENTER
                    celText.Font.Size = TamañoLetra

                    CellTablaVale = New PdfPCell()
                    CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
                    CellTablaVale.BackgroundColor = itextsharp.text.BaseColor.BLACK
                    CellTablaVale.AddElement(celText)
                    Tabla4.AddCell(CellTablaVale)

                    celText = New Paragraph
                    celText.Add("Concepto")
                    celText.Font.Color = itextsharp.text.BaseColor.WHITE
                    celText.Alignment = Element.ALIGN_CENTER
                    celText.Font.Size = TamañoLetra

                    CellTablaVale = New PdfPCell()
                    CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
                    CellTablaVale.BackgroundColor = itextsharp.text.BaseColor.BLACK
                    CellTablaVale.AddElement(celText)

                    'Se asinga el colspan al formato en caso de no tener iva
                    If Not pFactura.DesglozaIVA Then
                        CellTablaVale.Colspan = 2
                    End If

                    Tabla4.AddCell(CellTablaVale)

                    celText = New Paragraph
                    celText.Add("Precio")
                    celText.Font.Color = itextsharp.text.BaseColor.WHITE
                    celText.Alignment = Element.ALIGN_CENTER
                    celText.Font.Size = TamañoLetra

                    CellTablaVale = New PdfPCell()
                    CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
                    CellTablaVale.BackgroundColor = itextsharp.text.BaseColor.BLACK
                    CellTablaVale.AddElement(celText)
                    Tabla4.AddCell(CellTablaVale)

                    celText = New Paragraph
                    celText.Add("Importe")
                    celText.Font.Color = itextsharp.text.BaseColor.WHITE
                    celText.Alignment = Element.ALIGN_CENTER
                    celText.Font.Size = TamañoLetra

                    CellTablaVale = New PdfPCell()
                    CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
                    CellTablaVale.BackgroundColor = itextsharp.text.BaseColor.BLACK
                    CellTablaVale.AddElement(celText)
                    Tabla4.AddCell(CellTablaVale)

                    If DesglozaIVA Then
                        celText = New Paragraph
                        celText.Add("IVA")
                        celText.Font.Color = itextsharp.text.BaseColor.WHITE
                        celText.Alignment = Element.ALIGN_CENTER
                        celText.Font.Size = TamañoLetra

                        CellTablaVale = New PdfPCell()
                        CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
                        CellTablaVale.BackgroundColor = itextsharp.text.BaseColor.BLACK
                        CellTablaVale.AddElement(celText)
                        Tabla4.AddCell(CellTablaVale)
                    End If

                    celText = New Paragraph
                    celText.Add("Total")
                    celText.Font.Color = itextsharp.text.BaseColor.WHITE
                    celText.Alignment = Element.ALIGN_CENTER
                    celText.Font.Size = TamañoLetra

                    CellTablaVale = New PdfPCell()
                    CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
                    CellTablaVale.BackgroundColor = itextsharp.text.BaseColor.BLACK
                    CellTablaVale.AddElement(celText)
                    Tabla4.AddCell(CellTablaVale)

                End If

                'Detalle de los conceptos
                celText = New Paragraph
                celText.Add(c.cantidad)
                celText.Alignment = Element.ALIGN_CENTER
                celText.Font.Size = TamañoLetra

                CellTablaVale = New PdfPCell()
                CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
                CellTablaVale.AddElement(celText)
                Tabla4.AddCell(CellTablaVale)

                celText = New Paragraph
                celText.Add("Servicio")
                celText.Alignment = Element.ALIGN_CENTER
                celText.Font.Size = TamañoLetra

                CellTablaVale = New PdfPCell()
                CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
                CellTablaVale.AddElement(celText)
                Tabla4.AddCell(CellTablaVale)

                celText = New Paragraph
                celText.Add(c.descripcion)
                celText.Alignment = Element.ALIGN_LEFT
                celText.Font.Size = TamañoLetra

                CellTablaVale = New PdfPCell()
                CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
                CellTablaVale.AddElement(celText)

                'Se asinga el colspan al formato en caso de no tener iva
                If Not DesglozaIVA Then
                    CellTablaVale.Colspan = 2
                End If

                Tabla4.AddCell(CellTablaVale)

                celText = New Paragraph
                celText.Add("$ " & Format(c.valorUnitario, "0.00"))
                celText.Alignment = Element.ALIGN_RIGHT
                celText.Font.Size = TamañoLetra

                CellTablaVale = New PdfPCell()
                CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
                CellTablaVale.AddElement(celText)
                Tabla4.AddCell(CellTablaVale)

                celText = New Paragraph
                If DesglozaIVA Then
                    celText.Add("$ " & Format((c.importe / 1.16), "0.00"))
                Else
                    celText.Add("$ " & Format(c.importe, "0.00"))
                End If
                celText.Alignment = Element.ALIGN_RIGHT
                celText.Font.Size = TamañoLetra

                CellTablaVale = New PdfPCell()
                CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
                CellTablaVale.AddElement(celText)
                Tabla4.AddCell(CellTablaVale)

                If DesglozaIVA Then
                    celText = New Paragraph
                    celText.Add("$ " & Format(((c.importe / 1.16) * 0.16), "0.00"))
                    celText.Alignment = Element.ALIGN_RIGHT
                    celText.Font.Size = TamañoLetra

                    CellTablaVale = New PdfPCell()
                    CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
                    CellTablaVale.AddElement(celText)
                    Tabla4.AddCell(CellTablaVale)
                End If

                celText = New Paragraph
                celText.Add("$ " & Format(c.importe, "0.00"))
                celText.Alignment = Element.ALIGN_RIGHT
                celText.Font.Size = TamañoLetra

                CellTablaVale = New PdfPCell()
                CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
                CellTablaVale.AddElement(celText)
                Tabla4.AddCell(CellTablaVale)

                'Se asgina valor del subtotal
                If DesglozaIVA Then
                    Subtotal += (c.importe / 1.16)
                Else
                    Subtotal += c.importe
                End If

                con = con + 1
            Next

            'Asignacion de montos
            'SUBTOTAL
            celText = New Paragraph
            celText.Add("")

            CellTablaVale = New PdfPCell()
            CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
            CellTablaVale.AddElement(celText)
            Tabla4.AddCell(CellTablaVale)

            celText = New Paragraph
            celText.Add("")

            CellTablaVale = New PdfPCell()
            CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
            CellTablaVale.AddElement(celText)
            Tabla4.AddCell(CellTablaVale)

            celText = New Paragraph
            celText.Add("")

            CellTablaVale = New PdfPCell()
            CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
            CellTablaVale.AddElement(celText)
            Tabla4.AddCell(CellTablaVale)

            celText = New Paragraph
            celText.Add("")

            CellTablaVale = New PdfPCell()
            CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
            CellTablaVale.AddElement(celText)
            Tabla4.AddCell(CellTablaVale)

            celText = New Paragraph
            celText.Add("")

            CellTablaVale = New PdfPCell()
            CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
            CellTablaVale.AddElement(celText)
            Tabla4.AddCell(CellTablaVale)

            celText = New Paragraph
            celText.Add("Subtotal")
            celText.Font.Color = itextsharp.text.BaseColor.WHITE
            celText.Alignment = Element.ALIGN_RIGHT
            celText.Font.Size = TamañoLetra

            CellTablaVale = New PdfPCell()
            CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
            CellTablaVale.BackgroundColor = itextsharp.text.BaseColor.BLACK
            CellTablaVale.AddElement(celText)
            Tabla4.AddCell(CellTablaVale)

            celText = New Paragraph
            celText.Add("$ " & Format(Subtotal, "0.00") & " MXP")
            celText.Alignment = Element.ALIGN_RIGHT
            celText.Font.Size = TamañoLetra

            CellTablaVale = New PdfPCell()
            CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
            CellTablaVale.AddElement(celText)
            Tabla4.AddCell(CellTablaVale)


            'Se asinga el colspan al formato en caso de no tener iva
            If DesglozaIVA Then
                'IVA
                celText = New Paragraph
                celText.Add("")

                CellTablaVale = New PdfPCell()
                CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
                CellTablaVale.AddElement(celText)
                Tabla4.AddCell(CellTablaVale)

                celText = New Paragraph
                celText.Add("")

                CellTablaVale = New PdfPCell()
                CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
                CellTablaVale.AddElement(celText)
                Tabla4.AddCell(CellTablaVale)

                celText = New Paragraph
                celText.Add("")

                CellTablaVale = New PdfPCell()
                CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
                CellTablaVale.AddElement(celText)
                Tabla4.AddCell(CellTablaVale)

                celText = New Paragraph
                celText.Add("")

                CellTablaVale = New PdfPCell()
                CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
                CellTablaVale.AddElement(celText)
                Tabla4.AddCell(CellTablaVale)

                celText = New Paragraph
                celText.Add("")

                CellTablaVale = New PdfPCell()
                CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
                CellTablaVale.AddElement(celText)
                Tabla4.AddCell(CellTablaVale)

                celText = New Paragraph
                celText.Add("IVA ")
                celText.Font.Color = itextsharp.text.BaseColor.WHITE
                celText.Alignment = Element.ALIGN_RIGHT
                celText.Font.Size = TamañoLetra

                CellTablaVale = New PdfPCell()
                CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
                CellTablaVale.BackgroundColor = itextsharp.text.BaseColor.BLACK
                CellTablaVale.AddElement(celText)
                Tabla4.AddCell(CellTablaVale)

                celText = New Paragraph
                celText.Add("$ " & Format(Subtotal * 0.16, "0.00") & " MXP")
                celText.Alignment = Element.ALIGN_RIGHT
                celText.Font.Size = TamañoLetra

                CellTablaVale = New PdfPCell()
                CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
                CellTablaVale.AddElement(celText)
                Tabla4.AddCell(CellTablaVale)
            End If

            'TOTAL
            celText = New Paragraph
            celText.Add("")

            CellTablaVale = New PdfPCell()
            CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
            CellTablaVale.AddElement(celText)
            Tabla4.AddCell(CellTablaVale)

            celText = New Paragraph
            celText.Add("")

            CellTablaVale = New PdfPCell()
            CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
            CellTablaVale.AddElement(celText)
            Tabla4.AddCell(CellTablaVale)

            celText = New Paragraph
            celText.Add("")

            CellTablaVale = New PdfPCell()
            CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
            CellTablaVale.AddElement(celText)
            Tabla4.AddCell(CellTablaVale)

            celText = New Paragraph
            celText.Add("")

            CellTablaVale = New PdfPCell()
            CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
            CellTablaVale.AddElement(celText)
            Tabla4.AddCell(CellTablaVale)

            celText = New Paragraph
            celText.Add("")

            CellTablaVale = New PdfPCell()
            CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
            CellTablaVale.AddElement(celText)
            Tabla4.AddCell(CellTablaVale)

            celText = New Paragraph
            celText.Add("TOTAL")
            celText.Font.Color = itextsharp.text.BaseColor.WHITE
            celText.Alignment = Element.ALIGN_RIGHT
            celText.Font.Size = TamañoLetra

            CellTablaVale = New PdfPCell()
            CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
            CellTablaVale.BackgroundColor = itextsharp.text.BaseColor.BLACK
            CellTablaVale.AddElement(celText)
            Tabla4.AddCell(CellTablaVale)

            celText = New Paragraph
            If DesglozaIVA Then
                celText.Add("$ " & Format(Subtotal * 1.16, "0.00") & " MXP")
            Else
                celText.Add("$ " & Format(Subtotal, "0.00") & " MXP")
            End If
            celText.Alignment = Element.ALIGN_RIGHT
            celText.Font.Size = TamañoLetra

            CellTablaVale = New PdfPCell()
            CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
            CellTablaVale.AddElement(celText)
            Tabla4.AddCell(CellTablaVale)

            TablaHoja.AddCell(Tabla4)

            'Se inicia proceso para tabla 5
            Dim Tabla5 As New PdfPTable(1)
            Tabla5.WidthPercentage = 100
            Tabla5.DefaultCell.UseVariableBorders = True
            Tabla5.DefaultCell.Border = itextsharp.text.Rectangle.RECTANGLE
            Tabla5.DefaultCell.BorderColor = New itextsharp.text.BaseColor(192, 192, 192)

            celText = New Paragraph

            If DesglozaIVA Then
                celText.Add("TOTAL EN LETRA: " & MKSLibrary.Funciones.CantidadLetra(Subtotal * 1.16, 2) & " pesos 00/100 M.N.") ' Format(Subtotal * 1.16, "0.00"))
            Else
                celText.Add("TOTAL EN LETRA: " & MKSLibrary.Funciones.CantidadLetra(Subtotal, 2) & " pesos 00/100 M.N.") ' Format(Subtotal * 1.16, "0.00"))
            End If

            celText.Font.Size = TamañoLetra

            CellTablaVale = New PdfPCell()
            'CellTablaVale.Padding = 1
            CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
            CellTablaVale.AddElement(celText)
            Tabla5.AddCell(CellTablaVale)

            celText = New Paragraph
            celText.Add("Método de Pago: " & cfdi.metodoDePago & "        Número de Cuenta: " & cfdi.NumCtaPago) ' Format(Subtotal * 1.16, "0.00"))
            celText.Font.Size = TamañoLetra

            CellTablaVale = New PdfPCell()
            CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
            CellTablaVale.AddElement(celText)
            Tabla5.AddCell(CellTablaVale)

            celText = New Paragraph
            celText.Add("Condiciones de Pago: " & cfdi.condicionesDePago) ' Format(Subtotal * 1.16, "0.00"))
            celText.Font.Size = TamañoLetra

            CellTablaVale = New PdfPCell()
            CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
            CellTablaVale.AddElement(celText)
            Tabla5.AddCell(CellTablaVale)

            TablaHoja.AddCell(Tabla5)

            'Se inicia proceso para generacion de Tabla 6
            Dim Tabla6 As New PdfPTable(2)
            Tabla6.SetWidths({150, 50})
            Tabla6.DefaultCell.UseVariableBorders = True
            Tabla6.DefaultCell.Border = itextsharp.text.Rectangle.RECTANGLE
            Tabla6.DefaultCell.BorderColor = New itextsharp.text.BaseColor(192, 192, 192)

            'Tabla Sellos
            Dim TablaSellos As New PdfPTable(1)
            TablaSellos.WidthPercentage = 100
            TablaSellos.DefaultCell.UseVariableBorders = True
            TablaSellos.DefaultCell.Border = itextsharp.text.Rectangle.RECTANGLE
            TablaSellos.DefaultCell.BorderColor = New itextsharp.text.BaseColor(192, 192, 192)

            celText = New Paragraph
            celText.Add("Cadena original del complemento de certificación digital del SAT")
            celText.Font.Color = itextsharp.text.BaseColor.WHITE
            celText.Alignment = Element.ALIGN_CENTER
            celText.Font.Size = TamañoLetra

            CellTablaVale = New PdfPCell()
            CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
            CellTablaVale.BackgroundColor = itextsharp.text.BaseColor.BLACK
            CellTablaVale.AddElement(celText)
            TablaSellos.AddCell(CellTablaVale)

            celText = New Paragraph
            celText.Add(Cadena)
            celText.Alignment = Element.ALIGN_CENTER
            celText.Font.Size = TamañoLetra

            CellTablaVale = New PdfPCell()
            CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
            CellTablaVale.AddElement(celText)
            TablaSellos.AddCell(CellTablaVale)

            celText = New Paragraph
            celText.Add("Sello digital del emisor")
            celText.Font.Color = itextsharp.text.BaseColor.WHITE
            celText.Alignment = Element.ALIGN_CENTER
            celText.Font.Size = TamañoLetra

            CellTablaVale = New PdfPCell()
            CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
            CellTablaVale.BackgroundColor = itextsharp.text.BaseColor.BLACK
            CellTablaVale.AddElement(celText)
            TablaSellos.AddCell(CellTablaVale)

            celText = New Paragraph
            celText.Add(timbre.selloCFD)
            celText.Alignment = Element.ALIGN_CENTER
            celText.Font.Size = TamañoLetra

            CellTablaVale = New PdfPCell()
            CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
            CellTablaVale.AddElement(celText)
            TablaSellos.AddCell(CellTablaVale)

            celText = New Paragraph
            celText.Add("Sello digital del SAT")
            celText.Font.Color = itextsharp.text.BaseColor.WHITE
            celText.Alignment = Element.ALIGN_CENTER
            celText.Font.Size = TamañoLetra

            CellTablaVale = New PdfPCell()
            CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
            CellTablaVale.BackgroundColor = itextsharp.text.BaseColor.BLACK
            CellTablaVale.AddElement(celText)
            TablaSellos.AddCell(CellTablaVale)

            celText = New Paragraph
            celText.Add(timbre.selloSAT)
            celText.Alignment = Element.ALIGN_CENTER
            celText.Font.Size = TamañoLetra

            CellTablaVale = New PdfPCell()
            CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
            CellTablaVale.AddElement(celText)
            TablaSellos.AddCell(CellTablaVale)

            Tabla6.AddCell(TablaSellos)

            'Tabla CBB
            Dim TablaCBB As New PdfPTable(1)
            TablaCBB.WidthPercentage = 100

            'Generar el CBB como Imagen
            Dim CBGen As New MessagingToolkit.Barcode.BarcodeEncoder

            CBGen.Height = 140
            CBGen.Width = 140
            CBGen.LabelPosition = MessagingToolkit.Barcode.LabelPositions.BottomCenter

            Dim Img As System.Drawing.Image = System.Drawing.Image.FromFile(Funciones.RutaApp() & pFactura.UrlImagen) 'TODO: Aqui va la imagen 'HttpContext.Current.Server.MapPath("~/Imagenes/LogoHeader.png"))
            Dim CBImg As Bitmap = CBGen.Encode(MessagingToolkit.Barcode.BarcodeFormat.QRCode, "?re=" & cfdi.Receptor.rfc & "&rr=" & cfdi.folio & "&tt=" & cfdi.noCertificado & "&id=" & timbre.UUID) 'TODO: usar el uuid en lugar del rfc del emisor
            Dim PdfImgCBB As itextsharp.text.Image = itextsharp.text.Image.GetInstance(Img, System.Drawing.Imaging.ImageFormat.Jpeg)
            PdfImgCBB = itextsharp.text.Image.GetInstance(CBImg, System.Drawing.Imaging.ImageFormat.Jpeg)
            ImgHeight = 10
            ImgWidth = 10
            'ImgWidth = ImgHeight * (PdfImgCBB.Width / PdfImgCBB.Height) * 2
            'PdfImgCBB.SetAbsolutePosition(DocPDF.PageSize.Width - ImgWidth - DocPDF.RightMargin, DocPDF.PageSize.Height - (ImgHeight + 10))
            'PdfImgCBB.ScaleAbsolute(ImgWidth, ImgHeight)

            'celText = New Paragraph
            'celText.Add(PdfImgCBB)
            'celText.Alignment = Element.ALIGN_CENTER

            CellTablaVale = New PdfPCell(PdfImgCBB)
            CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
            'CellTablaVale.AddElement(celText)
            TablaCBB.AddCell(CellTablaVale)

            Tabla6.AddCell(TablaCBB)

            'Textos
            celText = New Paragraph
            celText.Add("Este documento es una representación impresa de un CFDI")
            celText.Alignment = Element.ALIGN_CENTER
            celText.Font.Size = TamañoLetra

            CellTablaVale = New PdfPCell()
            'CellTablaVale.Padding = 1
            CellTablaVale.BorderColor = itextsharp.text.BaseColor.WHITE
            CellTablaVale.Colspan = 2
            CellTablaVale.AddElement(celText)
            Tabla6.AddCell(CellTablaVale)

            TablaHoja.AddCell(Tabla6)

            DocPDF.Add(TablaHoja)

            Return True

        Catch ex As Exception
            Modulo("CFDIProceso")
            Funcion("ImprimirPagina")
            RegistrarError(ex)
            Return False
        End Try

    End Function

    Private Shared Function TimbrarComprobante(cfdi As Byte(), pFactura As SelloCFDI.ParamFactura) As ResultadoProceso(Of String)
        Dim Retorno As New ResultadoProceso(Of String)
        Dim url As String = ""
        Try
            Dim token As String = ""
            Dim responseCode As Integer = -1
            Dim xmlhttp As MSXML.XMLHTTPRequest = Nothing

            xmlhttp = New MSXML.XMLHTTPRequest
            url = pFactura.WSURL

            If url.Trim.Length <= 0 Then
                Modulo("CFDIProceso")
                Funcion("TimbrarComprobante")
                Dim ex_aux1 As Exception = New Exception("No se encontro la URL del Servicio de timbrado." & Environment.NewLine & url)
                RegistrarError(ex_aux1)

                Retorno.HayError = True
                Retorno.MensajeError = "No se encontro la URL del Servicio de timbrado."
                Dim smsg As String = "Cliente: " & pFactura.CorreoElectronicoReceptor & Retorno.MensajeError
                MailHelper.SMTP.EnviaMailError(System.Configuration.ConfigurationManager.AppSettings("MailAdministrador"), smsg)
                Return Retorno
            End If

            token = pFactura.Token

            If token.Length <= 0 Then
                Modulo("CFDIProceso")
                Funcion("TimbrarComprobante")
                Dim ex_aux2 As Exception = New Exception("No se encontro el valor del Token para el servicio de timbrado." & Environment.NewLine & url)
                RegistrarError(ex_aux2)

                Retorno.HayError = True
                Retorno.MensajeError = "No se encontro el valor del Token para el servicio de timbrado."
                Dim smsg As String = "Cliente: " & pFactura.CorreoElectronicoReceptor & Retorno.MensajeError
                MailHelper.SMTP.EnviaMailError(System.Configuration.ConfigurationManager.AppSettings("MailAdministrador"), smsg)
                Return Retorno
            End If

            xmlhttp.open("POST", url, False)
            xmlhttp.setRequestHeader("x-auth-token", token)
            xmlhttp.send(System.Text.Encoding.UTF8.GetString(cfdi))

            responseCode = xmlhttp.status
            If responseCode = 200 Then
                Retorno.Resultado = xmlhttp.responseText
            Else
                Modulo("CFDIProceso")
                Funcion("TimbrarComprobante")
                Dim ex_aux As Exception = New Exception("No se pudo Timbrar el CFDI - " & xmlhttp.status & " - " & xmlhttp.responseText)
                RegistrarError(ex_aux)

                Retorno.HayError = True
                Retorno.MensajeError = "No se pudo Timbrar el CFDI - " & xmlhttp.status & " - " & xmlhttp.responseText
                Dim smsg As String = "Cliente: " & pFactura.CorreoElectronicoReceptor & Retorno.MensajeError
                MailHelper.SMTP.EnviaMailError(System.Configuration.ConfigurationManager.AppSettings("MailAdministrador"), smsg)
            End If
        Catch ex As Exception
            Modulo("CFDIProceso")
            Funcion("TimbrarComprobante")
            Dim ex_aux As Exception = New Exception("No se completo el proceso de timbrado, [TC] L-1295" & Environment.NewLine & url & Environment.NewLine & ex.Message & Environment.NewLine & ex.StackTrace, ex)
            RegistrarError(ex_aux)
            Retorno.HayError = True
            Retorno.MensajeError = "No se completo el proceso de timbrado [TC] L-1295"
            Dim smsg As String = "Cliente: " & pFactura.CorreoElectronicoReceptor & "Exception: " + ex.Message + Environment.NewLine + "Stack: " + ex.StackTrace
            MailHelper.SMTP.EnviaMailError(System.Configuration.ConfigurationManager.AppSettings("MailAdministrador"), smsg)
        End Try
        Return Retorno
    End Function

End Class
