Imports MKSLibrary.Conectividad.Comun

Public Class Conexion

    Public Shared Property Server As String = ""
    Public Shared Property Database As String = ""
    Public Shared Property User As String = ""
    Public Shared Property Password As String = ""

    Public Shared Function Conectar() As IConexion

        Try

            If Not Funciones.LeerConfig() Then
                Return Nothing
            End If

            Dim Cnn As IConexion = New MKSLibrary.Conectividad.SQLServer.SQLServerConexionDisposable(Server, Database, User, Password)

            If Cnn.Conectar Then
                Return Cnn
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

End Class
