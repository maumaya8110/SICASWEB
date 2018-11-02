<Serializable()>
Public Class ResultadoProceso(Of T)

    Sub New()
        'Nada
    End Sub

    Public Property Resultado As T
    Public Property MensajeResultado As String = ""

    Public Property HayError As Boolean = False
    Public Property MensajeError As String = ""

    Public Sub New(Resultado As T, HayError As Boolean, MensajeError As String)
        Me.Resultado = Resultado
        Me.HayError = HayError
        Me.MensajeError = MensajeError
    End Sub

    Public Sub New(Resultado As T, MensajeResultado As String, HayError As Boolean, MensajeError As String)
        Me.Resultado = Resultado
        Me.MensajeResultado = MensajeResultado
        Me.HayError = HayError
        Me.MensajeError = MensajeError
    End Sub

End Class
