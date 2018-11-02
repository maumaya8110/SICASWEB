Imports System.Net.Mail
Imports System.Net

Namespace MailHelper

    Public Class SMTP
        Implements IDisposable

        Public Host As String
        Public Port As Integer
        Public Username As String
        Public Password As String
        Public EmailAccount As String
        Public EmailAlias As String
        Public SSL As Boolean

        Public Sub New(host As String, port As Integer, username As String, password As String, emailAccount As String, emailAlias As String, Optional SSL As Boolean = False)
            With Me
                .Host = host
                .Port = port
                .Username = username
                .Password = password
                .EmailAccount = emailAccount
                .EmailAlias = emailAlias
                .SSL = SSL
            End With
        End Sub

        Public Function SendMail(sendTo As String,
                                 subject As String,
                                 body As String,
                                 isBodyHTML As Boolean,
                                 attachments As List(Of String),
                                 Optional secondsTimeout As Integer = 0,
                                 Optional replyTo As String = "") As Boolean
            Try
                Dim client As New SmtpClient(Me.Host)
                Dim message As New MailMessage()

                If Me.Port > 0 Then client.Port = Me.Port
                client.DeliveryMethod = SmtpDeliveryMethod.Network
                client.UseDefaultCredentials = False
                client.Credentials = New NetworkCredential(Me.Username, Me.Password)
                client.EnableSsl = Me.SSL

                With message

                    .From = New MailAddress(Me.EmailAccount, Me.EmailAlias)
                    If replyTo <> "" Then
                        .ReplyToList.Add(New MailAddress(replyTo))
                    End If

                    If Not sendTo.Trim = "" Then
                        sendTo = sendTo.Replace(",", ";")
                        For Each a As String In sendTo.Split(";")
                            .To.Add(New MailAddress(a.Trim))
                        Next
                    End If

                    If Not attachments Is Nothing AndAlso attachments.Count > 0 Then
                        For Each f As String In attachments
                            If IO.File.Exists(f) Then
                                .Attachments.Add(New Attachment(f))
                            Else
                                Throw New Exception(String.Format("El archivo '{0}' no existe.", f))
                            End If
                        Next
                    End If

                    .Subject = subject
                    .Body = body
                    .IsBodyHtml = isBodyHTML
                End With
                If secondsTimeout > 0 Then client.Timeout = secondsTimeout * 1000
                client.Send(message)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
            Return False
        End Function

        Public Shared Sub EnviaMailError(para As String, mensaje As String)
            Dim mh As MailHelper.SMTP = New MailHelper.SMTP(
     System.Configuration.ConfigurationManager.AppSettings("MailServer") _
     , CInt(System.Configuration.ConfigurationManager.AppSettings("MailPort")) _
     , System.Configuration.ConfigurationManager.AppSettings("MailUsername") _
     , System.Configuration.ConfigurationManager.AppSettings("MailPassword") _
     , System.Configuration.ConfigurationManager.AppSettings("MailAccount") _
     , System.Configuration.ConfigurationManager.AppSettings("MailFrom") _
     , False)
            '& Environment.NewLine & "Token " & pFactura.Token
            If Not mh.SendMail(System.Configuration.ConfigurationManager.AppSettings("MailAdministrador"),
                           "Error en la Generación de Factura",
                           "Ocurrió el siguiente error al generar la Factura: " & mensaje, True, Nothing, 0) Then
                Debug.WriteLine("No se pudo enviar el Email al " & para & ".")
            End If
        End Sub

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
            Me.disposedValue = True
        End Sub

        ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
        'Protected Overrides Sub Finalize()
        '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class

End Namespace