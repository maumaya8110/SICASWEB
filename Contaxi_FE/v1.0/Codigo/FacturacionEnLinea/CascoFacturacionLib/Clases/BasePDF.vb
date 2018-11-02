Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports System.IO
Imports System.Drawing

Public Class BasePDF
    Implements iTextSharp.text.pdf.IPdfPageEvent

    Public Event HeaderAdicional(Writer As PdfWriter, PDFDoc As Document)

    Protected moTemplate As PdfTemplate
    Protected moCB As PdfContentByte
    Protected moBF As BaseFont = Nothing

    Dim dColor As iTextSharp.text.BaseColor = New iTextSharp.text.BaseColor(0, 255, 255)
    Dim dFont As iTextSharp.text.Font = New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD)

    Public Property Tabla As PdfPTable
    Public Property Titulo As String
    Public Property Folio As String
    Public Property FechaGeneracion As String = ""
    Public Property Paquete As String = ""
    Public Property SubPaquete As String = ""
    Public Property Realizo As String = ""
    Public Property ReiniciarPagina As Boolean = False

    Public Property WaterMarkText As String = ""

    Dim ServicioIDAnt As Integer = 0
    Dim ValePaginas As Integer = 0

    Public Function HeaderPDF(Writer As PdfWriter, PDFDoc As Document) As Boolean

        Try

            Dim Img As System.Drawing.Image = System.Drawing.Image.FromFile(Funciones.RutaApp() & "/Imagenes/LogoHeader.png")

            Dim PdfImg As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(Img, System.Drawing.Imaging.ImageFormat.Jpeg)
            Dim ImgHeight As Single = 30
            Dim ImgWidth As Single = ImgHeight * (PdfImg.Width / PdfImg.Height)

            Dim pageWidth = PDFDoc.PageSize.Width - (PDFDoc.LeftMargin + PDFDoc.RightMargin)

            PdfImg.SetAbsolutePosition(PDFDoc.LeftMargin, PDFDoc.PageSize.Height - (ImgHeight + 10))

            PdfImg.ScaleAbsolute(ImgWidth, ImgHeight)
            PDFDoc.Add(PdfImg)

            ImgHeight = 25
            ImgWidth = ImgHeight * (PdfImg.Width / PdfImg.Height) * 2

            PdfImg.SetAbsolutePosition(PDFDoc.PageSize.Width - ImgWidth - PDFDoc.RightMargin, PDFDoc.PageSize.Height - (ImgHeight + 10))

            PdfImg.ScaleAbsolute(ImgWidth, ImgHeight)
            PDFDoc.Add(PdfImg)

            moCB.BeginText()
            moCB.SetFontAndSize(moBF, 9)
            Dim fLen As Single = moBF.GetWidthPoint(Me.Folio, 9)
            moCB.SetTextMatrix(PDFDoc.PageSize.Width - PDFDoc.RightMargin - (fLen / 2) - (ImgWidth / 2), PDFDoc.PageSize.Height - 10 - ImgHeight - 8)
            moCB.ShowText(Me.Folio)
            moCB.EndText()

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function FooterPDF(Writer As PdfWriter, PDFDoc As Document) As Boolean

        Try

            Dim oTable As New PdfPTable(1)
            Dim oCell As PdfPCell

            With oTable
                '---Column 1:  some title
                oCell = New PdfPCell(New iTextSharp.text.Phrase(Me.Titulo, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD))) 'New iTextSharp.text.Phrase(Me.Titulo, FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD)))
                oCell.Border = 0
                'oCell.BackgroundColor = dColor
                oCell.HorizontalAlignment = Element.ALIGN_CENTER
                .AddCell(oCell)

                oCell = New PdfPCell(New iTextSharp.text.Phrase(Me.FechaGeneracion, FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.NORMAL)))
                oCell.Border = 0
                'oCell.BackgroundColor = dColor
                oCell.HorizontalAlignment = Element.ALIGN_CENTER
                .AddCell(oCell)

                oTable.TotalWidth = PDFDoc.PageSize.Width - 70
                oTable.WriteSelectedRows(0, -1, 36, PDFDoc.PageSize.Height - 10, Writer.DirectContent)

            End With

            '---Column 2: PageNumber
            Dim iPageNumber As Integer = Writer.PageNumber
            Dim sText As String = "PÁGINA " & iPageNumber & " / "

            Dim fLen As Single = moBF.GetWidthPoint(sText, 10)

            moCB.BeginText()
            moCB.SetFontAndSize(moBF, 8)
            moCB.SetTextMatrix(PDFDoc.PageSize.Width / 2 - 20, 25)
            moCB.ShowText(sText)
            moCB.EndText()

            moCB.AddTemplate(moTemplate, (PDFDoc.PageSize.Width / 2) + (fLen / 2), 25)
            moCB.BeginText()
            moCB.SetFontAndSize(moBF, 14)
            moCB.SetTextMatrix(300, 300)
            moCB.EndText()

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Sub OnChapter(writer As iTextSharp.text.pdf.PdfWriter, document As iTextSharp.text.Document, paragraphPosition As Single, title As iTextSharp.text.Paragraph) Implements iTextSharp.text.pdf.IPdfPageEvent.OnChapter

    End Sub

    Public Sub OnChapterEnd(writer As iTextSharp.text.pdf.PdfWriter, document As iTextSharp.text.Document, paragraphPosition As Single) Implements iTextSharp.text.pdf.IPdfPageEvent.OnChapterEnd

    End Sub

    Public Sub OnCloseDocument(writer As iTextSharp.text.pdf.PdfWriter, document As iTextSharp.text.Document) Implements iTextSharp.text.pdf.IPdfPageEvent.OnCloseDocument

        moTemplate.BeginText()
        moTemplate.SetFontAndSize(moBF, 8)
        moTemplate.ShowText((writer.PageNumber - 1).ToString)
        moTemplate.EndText()

    End Sub

    Public Overridable Sub OnEndPage(writer As iTextSharp.text.pdf.PdfWriter, document As iTextSharp.text.Document) Implements iTextSharp.text.pdf.IPdfPageEvent.OnEndPage
        HeaderPDF(writer, document)
        FooterPDF(writer, document)
        If WaterMarkText.Trim.Length > 0 Then

            Dim fontsize As Single = 80
            Dim xposition As Single = 300
            Dim yposition As Single = 400
            Dim angle As Single = 45

            Try
                Dim under As PdfContentByte = writer.DirectContentUnder
                Dim baseFont As BaseFont = baseFont.CreateFont(baseFont.HELVETICA, baseFont.WINANSI, baseFont.EMBEDDED)
                under.BeginText()
                under.SetColorFill(BaseColor.LIGHT_GRAY)
                under.SetFontAndSize(baseFont, fontsize)
                under.ShowTextAligned(PdfContentByte.ALIGN_CENTER, WaterMarkText, xposition, yposition, angle)
                under.EndText()

            Catch ex As Exception
                Modulo("PDFWaterMark")
                Funcion("OnStartPage")
                RegistrarError(ex)
            End Try
        End If
    End Sub

    Public Sub OnGenericTag(writer As iTextSharp.text.pdf.PdfWriter, document As iTextSharp.text.Document, rect As iTextSharp.text.Rectangle, text As String) Implements iTextSharp.text.pdf.IPdfPageEvent.OnGenericTag

    End Sub

    Public Sub OnOpenDocument(writer As iTextSharp.text.pdf.PdfWriter, document As iTextSharp.text.Document) Implements iTextSharp.text.pdf.IPdfPageEvent.OnOpenDocument
        Try
            moBF = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED)
            moCB = writer.DirectContent
            moTemplate = moCB.CreateTemplate(50, 50)

        Catch de As DocumentException
            Debug.Print(de.Message())
        Catch ioe As IOException
            Debug.Print(ioe.Message())
        End Try
    End Sub

    Public Sub OnParagraph(writer As iTextSharp.text.pdf.PdfWriter, document As iTextSharp.text.Document, paragraphPosition As Single) Implements iTextSharp.text.pdf.IPdfPageEvent.OnParagraph

    End Sub

    Public Sub OnParagraphEnd(writer As iTextSharp.text.pdf.PdfWriter, document As iTextSharp.text.Document, paragraphPosition As Single) Implements iTextSharp.text.pdf.IPdfPageEvent.OnParagraphEnd

    End Sub

    Public Sub OnSection(writer As iTextSharp.text.pdf.PdfWriter, document As iTextSharp.text.Document, paragraphPosition As Single, depth As Integer, title As iTextSharp.text.Paragraph) Implements iTextSharp.text.pdf.IPdfPageEvent.OnSection

    End Sub

    Public Sub OnSectionEnd(writer As iTextSharp.text.pdf.PdfWriter, document As iTextSharp.text.Document, paragraphPosition As Single) Implements iTextSharp.text.pdf.IPdfPageEvent.OnSectionEnd

    End Sub

    Public Overridable Sub OnStartPage(writer As iTextSharp.text.pdf.PdfWriter, document As iTextSharp.text.Document) Implements iTextSharp.text.pdf.IPdfPageEvent.OnStartPage
        'pagecount = pagecount + 1

        RaiseEvent HeaderAdicional(writer, document)

        'If WaterMarkText.Trim.Length > 0 Then

        '    Dim fontsize As Single = 80
        '    Dim xposition As Single = 300
        '    Dim yposition As Single = 400
        '    Dim angle As Single = 45

        '    Try
        '        Dim under As PdfContentByte = writer.DirectContentUnder
        '        Dim baseFont As BaseFont = baseFont.CreateFont(baseFont.HELVETICA, baseFont.WINANSI, baseFont.EMBEDDED)
        '        under.BeginText()
        '        under.SetColorFill(BaseColor.LIGHT_GRAY)
        '        under.SetFontAndSize(baseFont, fontsize)
        '        under.ShowTextAligned(PdfContentByte.ALIGN_CENTER, WaterMarkText, xposition, yposition, angle)
        '        under.EndText()

        '    Catch ex As Exception
        '        Modulo("PDFWaterMark")
        '        Funcion("OnStartPage")
        '        RegistrarError(ex)
        '    End Try
        'End If

    End Sub
End Class
