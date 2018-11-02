Imports System.IO

Public Class SociosOperativosProceso

    Public Shared Function ObtenerSociosOperativos(Ent As SocioOperativo, Usr As Usuario) As ResultadoProceso(Of List(Of SocioOperativo))
        Dim Retorno As New ResultadoProceso(Of List(Of SocioOperativo))
        Try

            Using CnnObj As Object = Conexion.Conectar
                If CnnObj IsNot Nothing Then

                    Dim Cnn As IConexion = CnnObj

                    Dim Resultado As New List(Of SocioOperativo)
                    Resultado = AccesoDatos.ObtenerSocioOperativo(Cnn, Ent)

                    If Resultado Is Nothing Then
                        Retorno.HayError = True
                        Retorno.MensajeError = "No se realizo la busqueda de los Socios Operativos, intente nuevamente [OSOP]"
                    Else
                        Retorno.Resultado = Resultado
                    End If

                Else
                    Retorno.HayError = True
                    Retorno.MensajeError = "No hay conexión, intente nuevamente"
                End If
            End Using

        Catch ex As Exception
            Modulo("SociosOperativosProceso")
            Funcion("ObtenerSociosOperativos")
            RegistrarError(ex)
            Retorno.HayError = True
            Retorno.MensajeError = "No se completo el proceso [OSOP]"
        End Try
        Return Retorno
    End Function

    Public Shared Function ObtenerUnidadesSocioOperativo(Ent As UnidadSocioOperativo, Usr As Usuario) As ResultadoProceso(Of List(Of UnidadSocioOperativo))
        Dim Retorno As New ResultadoProceso(Of List(Of UnidadSocioOperativo))
        Try

            Using CnnObj As Object = Conexion.Conectar
                If CnnObj IsNot Nothing Then

                    Dim Cnn As IConexion = CnnObj

                    Dim Resultado As New List(Of UnidadSocioOperativo)
                    Resultado = AccesoDatos.ObtenerUnidadSocioOperativo(Cnn, Ent)

                    If Resultado Is Nothing Then
                        Retorno.HayError = True
                        Retorno.MensajeError = "No se realizo la busqueda de las Unidades del Socio Operativo, intente nuevamente [OUSOP]"
                    Else
                        Retorno.Resultado = Resultado
                    End If

                Else
                    Retorno.HayError = True
                    Retorno.MensajeError = "No hay conexión, intente nuevamente"
                End If
            End Using

        Catch ex As Exception
            Modulo("SociosOperativosProceso")
            Funcion("ObtenerSociosOperativos")
            RegistrarError(ex)
            Retorno.HayError = True
            Retorno.MensajeError = "No se completo el proceso [OUSOP]"
        End Try
        Return Retorno
    End Function


    Public Shared Function ValidarEliminarSocioOperativo(Ent As SocioOperativo, Usr As Usuario) As ResultadoProceso(Of String)
        Dim Retorno As New ResultadoProceso(Of String)
        Try


            Using CnnObj As Object = Conexion.Conectar
                If CnnObj IsNot Nothing Then

                    'Dim Cnn As IConexion = CnnObj

                    'Dim Resultado As New List(Of SocioOperativo)
                    'Resultado = AccesoDatos.ObtenerSocioOperativo(Cnn, Ent)

                    'If Resultado Is Nothing Then
                    '    Retorno.HayError = True
                    '    Retorno.MensajeError = "No se realizo la busqueda de los Socios Operativos, intente nuevamente [OSOP]"
                    'Else
                    '    Retorno.Resultado = Resultado
                    'End If

                Else
                    Retorno.HayError = True
                    Retorno.MensajeError = "No hay conexión, intente nuevamente"
                End If
            End Using

        Catch ex As Exception
            Modulo("SociosOperativosProceso")
            Funcion("ValidarEliminarSocioOperativo")
            RegistrarError(ex)
            Retorno.HayError = True
            Retorno.MensajeError = "No se completo el proceso [VESO]"
        End Try
        Return Retorno
    End Function

    Public Shared Function EliminarSocioOperativo(SocioOperativoID As Integer, RFC As String, RutaArchivos As String, Usr As Usuario) As ResultadoProceso(Of Boolean)
        Dim Retorno As New ResultadoProceso(Of Boolean)
        Try

            Using CnnObj As Object = Conexion.Conectar
                If CnnObj IsNot Nothing Then

                    Dim Tran As String = "ELIMINARSOCIOOPERATIVO"
                    Dim Cnn As IConexion = CnnObj

                    If Cnn.IniciarTran(Tran) Then
                        Try

                            Dim Resultado As Boolean = False
                            Resultado = AccesoDatos.EliminarSocioOperativo(Cnn, SocioOperativoID)

                            If Not Resultado Then
                                Retorno.HayError = True
                                Retorno.MensajeError = "No se pudo eliminar el Socio Operativo, intente nuevamente [ESO]"
                            Else
                                Retorno.Resultado = Resultado
                            End If

                            'Se Eliminan los archivos
                            If Not Retorno.HayError Then
                                'Se valida que exista el primer archivo
                                If File.Exists(RutaArchivos & "\" & RFC & ".cer") Then
                                    File.Delete(RutaArchivos & "\" & RFC & ".cer")
                                End If

                                If Not Retorno.HayError Then

                                    If File.Exists(RutaArchivos & "\" & RFC & ".key") Then
                                        File.Delete(RutaArchivos & "\" & RFC & ".key")
                                    End If

                                End If

                            End If

                            If Not Retorno.HayError Then
                                If Cnn.AceptarTran(Tran) Then
                                    Retorno.Resultado = True
                                Else
                                    Cnn.CancelarTran(Tran)
                                    Retorno.HayError = True
                                    Retorno.MensajeError = "No se pudo aceptar la transacción, intente nuevamente"
                                End If

                            End If

                        Catch ex As Exception
                            Modulo("SociosOperativosProceso")
                            Funcion("EliminarSocioOperativo")
                            RegistrarError(ex)
                            Cnn.CancelarTran(Tran)
                            Retorno.HayError = True
                            Retorno.MensajeError = "No se completo el proceso [ESO]"
                        End Try

                    Else
                        Cnn.CancelarTran(Tran)
                        Retorno.HayError = True
                        Retorno.MensajeError = "No se pudo iniciar transacción, intente nuevamente"
                    End If
                Else
                    Retorno.HayError = True
                    Retorno.MensajeError = "No hay conexión, intente nuevamente"
                End If
            End Using

        Catch ex As Exception
            Modulo("SociosOperativosProceso")
            Funcion("EliminarSocioOperativo")
            Retorno.HayError = True
            Retorno.MensajeError = "No se completo el proceso [ESO]"
        End Try
        Return Retorno
    End Function

    Public Shared Function ValidarGuardarSocioOperativo(Ent As SocioOperativo, Usr As Usuario) As ResultadoProceso(Of SocioOperativo)
        Dim Retorno As New ResultadoProceso(Of SocioOperativo)
        Try

            Using CnnObj As Object = Conexion.Conectar
                If CnnObj IsNot Nothing Then

                    Dim Cnn As IConexion = CnnObj

                    Dim Resultado As New List(Of SocioOperativo)
                    Resultado = AccesoDatos.ObtenerSocioOperativo(Cnn, Ent)

                    If Resultado Is Nothing Then
                        Retorno.HayError = True
                        Retorno.MensajeError = "No se realizo la busqueda de los Socios Operativos, intente nuevamente [OSOP]"
                    Else
                        Retorno.Resultado = Resultado.FirstOrDefault()
                    End If

                Else
                    Retorno.HayError = True
                    Retorno.MensajeError = "No hay conexión, intente nuevamente"
                End If
            End Using

        Catch ex As Exception
            Modulo("SociosOperativosProceso")
            Funcion("ValidarGuardarElemento")
            RegistrarError(ex)
            Retorno.HayError = True
            Retorno.MensajeError = "No se completo el proceso [VGSO]"
        End Try
        Return Retorno
    End Function

    Public Shared Function GuardarSocioOperativo(Ent As SocioOperativo, Usr As Usuario) As ResultadoProceso(Of Boolean)
        Dim Retorno As New ResultadoProceso(Of Boolean)
        Try

            Using CnnObj As Object = Conexion.Conectar
                If CnnObj IsNot Nothing Then
                    Dim Tran As String = "AGREGARSOCIOOPERATIVO"
                    Dim Cnn As IConexion = CnnObj

                    If Cnn.IniciarTran(Tran) Then
                        Try

                            Dim Resultado As Boolean = False

                            If Ent.SocioOperativoID <= 0 Then

                                Dim ID As Long = AccesoDatos.AgregarSocioOperativo(Cnn, Ent)

                                If ID <= 0 Then
                                    Resultado = False
                                    Cnn.CancelarTran(Tran)
                                    Retorno.HayError = True
                                    Retorno.MensajeError = "No se pudo Agregar el Socio Operativo, intente nuevamente [GSO]"
                                Else
                                    Resultado = True
                                    Ent.SocioOperativoID = ID
                                    Retorno.Resultado = Resultado
                                End If

                            Else

                                Resultado = AccesoDatos.ModificarSocioOperativo(Cnn, Ent)

                                If Not Resultado Then
                                    Cnn.CancelarTran(Tran)
                                    Retorno.HayError = True
                                    Retorno.MensajeError = "No se pudo Guardar el Socio Operativo, intente nuevamente [GSO]"
                                Else
                                    Retorno.Resultado = Resultado
                                End If

                                'Se eliminan las unidades del socio operativo
                                If Not Retorno.HayError Then
                                    Dim EliminaUnidades As Boolean = AccesoDatos.EliminarUnidadSocioOperativo(Cnn, Ent.SocioOperativoID)

                                    If Not EliminaUnidades Then
                                        Cnn.CancelarTran(Tran)
                                        Retorno.HayError = True
                                        Retorno.MensajeError = "No se pudo Guardar las unidades del Socio Operativo, intente nuevamente [GUSO]"
                                    End If

                                End If

                            End If

                            'Se agregan las unidades del socio operativo
                            If Not Retorno.HayError Then
                                For Each u As UnidadSocioOperativo In Ent.Unidades
                                    u.SocioOperativoID = Ent.SocioOperativoID

                                    Dim AgregaUnidad As Long = AccesoDatos.AgregarUnidadSocioOperativo(Cnn, u)

                                    If AgregaUnidad <= 0 Then
                                        Cnn.CancelarTran(Tran)
                                        Retorno.HayError = True
                                        Retorno.MensajeError = "No se pudo Guardar la unidad N° " & u.Unidad & " del Socio Operativo, intente nuevamente [GUSO]"
                                        Exit For
                                    End If

                                Next
                            End If

                            'Se guardan los archivos
                            If Not Retorno.HayError Then
                                'Se valida que exista el primer archivo
                                If File.Exists(Ent.ArchivoCER) Then
                                    File.Delete(Ent.RutaArchivos & "\" & Ent.RFC & ".cer")
                                    File.Move(Ent.ArchivoCER, Ent.RutaArchivos & "\" & Ent.RFC & ".cer")
                                End If

                                If Not Retorno.HayError Then

                                    If File.Exists(Ent.ArchivoKEY) Then
                                        File.Delete(Ent.RutaArchivos & "\" & Ent.RFC & ".key")
                                        File.Move(Ent.ArchivoKEY, Ent.RutaArchivos & "\" & Ent.RFC & ".key")
                                    End If

                                End If

                            End If

                            'Se acepta la transaccion
                            If Not Retorno.HayError Then
                                If Cnn.AceptarTran(Tran) Then
                                    Retorno.Resultado = True
                                Else
                                    Cnn.CancelarTran(Tran)
                                    Retorno.HayError = True
                                    Retorno.MensajeError = "No se pudo aceptar la transacción, intente nuevamente"
                                End If
                            End If
                        Catch ex As Exception
                            Modulo("SociosOperativosProceso")
                            Funcion("GuardarSocioOperativo")
                            RegistrarError(ex)
                            Cnn.CancelarTran(Tran)
                            Retorno.HayError = True
                            Retorno.MensajeError = "No se completo el proceso [GSO]"
                        End Try

                    Else
                        Cnn.CancelarTran(Tran)
                        Retorno.HayError = True
                        Retorno.MensajeError = "No se pudo iniciar la transacción, intente nuevamente"
                    End If

                Else
                    Retorno.HayError = True
                    Retorno.MensajeError = "No hay conexión, intente nuevamente"
                End If
            End Using

        Catch ex As Exception
            Modulo("SociosOperativosProceso")
            Funcion("GuardarSocioOperativo")
            RegistrarError(ex)
            Retorno.HayError = True
            Retorno.MensajeError = "No se completo el proceso [GSO]"
        End Try
        Return Retorno
    End Function

End Class
