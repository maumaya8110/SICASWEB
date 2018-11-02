
Public Class LogInAdminProceso

    Public Shared Function ValidarLogIn(Ent As Usuario) As ResultadoProceso(Of Usuario)
        Dim Retorno As New ResultadoProceso(Of Usuario)
        Try
            Using CnnObj As Object = Conexion.Conectar

                If CnnObj IsNot Nothing Then

                    Dim Cnn As IConexion = CnnObj

                    Dim Usr As Usuario = AccesoDatos.ObtenerUsuario(Cnn, Ent).FirstOrDefault

                    If Usr IsNot Nothing Then
                        Retorno.Resultado = Usr
                    Else
                        Retorno.HayError = True
                        Retorno.MensajeError = "No se pudo consultar la información de usuarios, intente nuevamente. [VLI]"
                    End If

                Else
                    Retorno.HayError = True
                    Retorno.MensajeError = "No hay conexión, intente nuevamente. [VLI]"
                End If

            End Using

        Catch ex As Exception
            Modulo("LogInAdminProceso")
            Funcion("ValidarLogIn")
            RegistrarError(ex)
            Retorno.HayError = True
            Retorno.MensajeError = "No se completo el proceso [VLI]"
        End Try
        Return Retorno
    End Function

End Class
