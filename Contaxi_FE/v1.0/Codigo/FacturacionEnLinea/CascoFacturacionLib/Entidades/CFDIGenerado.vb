<Serializable()>
Public Class CFDIGenerado

    Public Property Folio As String = ""
    Public Property UUID As String = ""
    Public Property ArchivoPDF As Byte()
    Public Property ArchivoXML As Byte()
    Public Property Unidad As String = ""
    Public Property PDFName As String
    Public Property XMLName As String

    Public Property Cliente As String = ""

    Public Property Monto As Double = 0

    Public Property Fecha As DateTime = #1/1/1900#

End Class
