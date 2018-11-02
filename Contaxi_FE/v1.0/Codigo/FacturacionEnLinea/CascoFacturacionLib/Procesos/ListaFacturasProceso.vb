Imports System.IO

Public Class ListaFacturasProceso

    Public Shared Function ObtenerFacturas(Ent As Facturacion, Usr As Usuario) As ResultadoProceso(Of List(Of Facturacion))
        Dim Retorno As New ResultadoProceso(Of List(Of Facturacion))
        Try

            Using CnnObj As Object = Conexion.Conectar
                If CnnObj IsNot Nothing Then

                    Dim Cnn As IConexion = CnnObj

                    Dim Resultado As New List(Of Facturacion)
                    Resultado = AccesoDatos.ObtenerFacturacion(Cnn, Ent)

                    If Resultado Is Nothing Then
                        Retorno.HayError = True
                        Retorno.MensajeError = "No se realizo la busqueda de las Facturas [OF]"
                    Else
                        Retorno.Resultado = Resultado
                    End If

                Else
                    Retorno.HayError = True
                    Retorno.MensajeError = "No hay conexión"
                End If
            End Using

        Catch ex As Exception
            Modulo("ListaFacturasProceso")
            Funcion("ObtenerFacturas")
            RegistrarError(ex)
            Retorno.HayError = True
            Retorno.MensajeError = "No se completo el proceso [OF]"
        End Try
        Return Retorno
    End Function

    'Public Shared Function EliminarFactura(FacturacionID As Integer) As ResultadoProceso(Of Boolean)
    '    Dim Retorno As New ResultadoProceso(Of Boolean)
    '    Try
    '        Using CnnObj As Object = Conexion.Conectar
    '            If CnnObj IsNot Nothing Then
    '                Dim Tran As String = "ELIMINARFACTURA"
    '                Dim Cnn As IConexion = CnnObj
    '                If Cnn.IniciarTran(Tran) Then
    '                    Try
    '                        'Se obtiene la informacion de la Factura
    '                        Dim Factura As New Facturacion
    '                        Factura.FacturacionID = FacturacionID
    '                        Dim ListaFacturas As List(Of Facturacion) = AccesoDatos.ObtenerFacturacion(Cnn, Factura)
    '                        If ListaFacturas IsNot Nothing AndAlso ListaFacturas.Count > 0 Then
    '                            Factura = ListaFacturas.FirstOrDefault
    '                        Else
    '                            Retorno.HayError = True
    '                            Retorno.MensajeError = "No se pudo consultar la información de la Factura, intente nuevamente"
    '                        End If
    '                        If Not Retorno.HayError Then
    '                            'Se cancela la factura con DIVERZA
    '                            Dim url As String = ""
    '                            Dim token As String = ""
    '                            Dim responseCode As Integer = -1
    '                            Dim xmlhttp As MSXML.XMLHTTPRequest = Nothing

    '                            xmlhttp = New MSXML.XMLHTTPRequest
    '                            'TODO: Quitar el UUID de pruebas una vez terminadas.
    '                            url = Funciones.LeerParametro("DVZAUrl_Cancelacion").Valor & "/" & Factura.RFCEmisor & "/" & Factura.UUID

    '                            If url.Trim.Length <= 0 Then
    '                                Retorno.HayError = True
    '                                Retorno.MensajeError = "No se encontro la URL del Servicio de cancelación, intente nuevamente."
    '                                Return Retorno
    '                            End If

    '                            token = Funciones.LeerParametro("DVZAToken").Valor

    '                            If url.Trim.Length <= 0 Then
    '                                Retorno.HayError = True
    '                                Retorno.MensajeError = "No se encontro el valor del Token para el servicio de cancelación, intente nuevamente."
    '                                Return Retorno
    '                            End If

    '                            xmlhttp.open("POST", url, False)
    '                            xmlhttp.setRequestHeader("x-auth-token", token)
    '                            xmlhttp.send()

    '                            responseCode = xmlhttp.status

    '                            If Not responseCode = 200 Then
    '                                If responseCode <> 555 Then
    '                                    Retorno.HayError = True
    '                                    Retorno.MensajeError = "No se pudo Cancelar la Factura - " & xmlhttp.status & " - " & xmlhttp.responseText
    '                                End If
    '                            End If
    '                        End If
    '                        'Se eliminan los servicios de facturacion.
    '                        If Not Retorno.HayError Then
    '                            Dim EliminaServicios As Boolean = False
    '                            EliminaServicios = AccesoDatos.EliminarServicioFacturacion(Cnn, Factura.FacturacionID)

    '                            If Not EliminaServicios Then
    '                                Retorno.HayError = True
    '                                Retorno.MensajeError = "No se pudo Eliminar los Servicios de la Factura [EF]"
    '                            End If
    '                        End If
    '                        'Se elimina la factura
    '                        If Not Retorno.HayError Then
    '                            Dim EliminaFactura As Boolean = False
    '                            EliminaFactura = AccesoDatos.EliminarFacturacion(Cnn, FacturacionID)

    '                            If Not EliminaFactura Then
    '                                Retorno.HayError = True
    '                                Retorno.MensajeError = "No se pudo eliminar la Factura [EF]"
    '                            End If
    '                        End If
    '                        'Se eliminan los archivos de la factura
    '                        If Not Retorno.HayError Then

    '                            If File.Exists(Funciones.RutaApp() & "\CFDIS_GENERADOS\" & Factura.UUID & ".pdf") Then
    '                                File.Delete(Funciones.RutaApp() & "\CFDIS_GENERADOS\" & Factura.UUID & ".pdf")
    '                            End If
    '                            If File.Exists(Funciones.RutaApp() & "\CFDIS_GENERADOS\" & Factura.UUID & ".xml") Then
    '                                File.Delete(Funciones.RutaApp() & "\CFDIS_GENERADOS\" & Factura.UUID & ".xml")
    '                            End If
    '                        End If
    '                        If Not Retorno.HayError Then
    '                            If Cnn.AceptarTran(Tran) Then
    '                                Retorno.Resultado = True
    '                            Else
    '                                Cnn.CancelarTran(Tran)
    '                                Retorno.HayError = True
    '                                Retorno.MensajeError = "No se pudo aceptar la transacción, intente nuevamente"
    '                            End If
    '                        End If
    '                    Catch ex As Exception
    '                        Modulo("ListaFacturasProceso")
    '                        Funcion("EliminarFactura")
    '                        RegistrarError(ex)
    '                        Cnn.CancelarTran(Tran)
    '                        Retorno.HayError = True
    '                        Retorno.MensajeError = "No se completo el proceso [EF]"
    '                    End Try
    '                Else
    '                    Cnn.CancelarTran(Tran)
    '                    Retorno.HayError = True
    '                    Retorno.MensajeError = "No se pudo iniciar transacción, intente nuevamente"
    '                End If
    '            Else
    '                Retorno.HayError = True
    '                Retorno.MensajeError = "No hay conexión, intente nuevamente"
    '            End If
    '        End Using
    '    Catch ex As Exception
    '        Modulo("ListaFacturasProceso")
    '        Funcion("EliminarFactura")
    '        Retorno.HayError = True
    '        Retorno.MensajeError = "No se completo el proceso [EF]"
    '    End Try
    '    Return Retorno
    'End Function

End Class
