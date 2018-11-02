Imports System.Configuration
Imports System.Data.SqlClient


Public Module SQLHelper

    Private m_ReuseConnection As Boolean
    Public Property ReuseConnection As Boolean
        Get
            Return m_ReuseConnection
        End Get
        Set(value As Boolean)
            If Not value AndAlso Connection.State <> ConnectionState.Closed Then
                Connection.Close()
            End If
            m_ReuseConnection = value
        End Set
    End Property


    Private _ConnectionString As String
    Public Property ConnectionString() As String
        Get
            If _ConnectionString = "" Then
                Try
                    Dim useConnectionString As String = ""
                    If Reflection.Assembly.GetEntryAssembly Is Nothing Then
                        If Not ConfigurationManager.AppSettings(DefaultConnectionStringKey) Is Nothing Then
                            useConnectionString = ConfigurationManager.AppSettings(DefaultConnectionStringKey)
                        End If
                    Else
                        Dim MySettingsNamespace As String = Reflection.Assembly.GetEntryAssembly.GetName.Name & ".My.MySettings"
                        Dim MyClientSettings As ClientSettingsSection = ConfigurationManager.GetSection("applicationSettings/" & MySettingsNamespace)
                        Dim connStrKey As String = MyClientSettings.Settings.Get("DefaultConnectionString").Value.ValueXml.InnerText 'ConfigurationManager.AppSettings("DefaultConnectionString")
                        useConnectionString = MySettingsNamespace & "." & connStrKey
                    End If
                    If Not String.IsNullOrEmpty(useConnectionString) Then
                        _ConnectionString = ConfigurationManager.ConnectionStrings(useConnectionString).ConnectionString
                    End If

                Catch ex As Exception
                    _ConnectionString = ""
                End Try
            End If
            Return _ConnectionString
        End Get
        Set(ByVal value As String)
            _ConnectionString = value
        End Set
    End Property

    Private m_CommandTimeout As Integer
    Public Property CommandTimeout() As String
        Get
            Return m_CommandTimeout
        End Get
        Set(ByVal value As String)
            m_CommandTimeout = value
        End Set
    End Property

    Private m_DefaultConnectionStringKey As String
    Public Property DefaultConnectionStringKey() As String
        Get
            If m_DefaultConnectionStringKey = "" Then m_DefaultConnectionStringKey = "DefaultConnectionString"
            Return m_DefaultConnectionStringKey
        End Get
        Set(ByVal value As String)
            m_DefaultConnectionStringKey = value
            _ConnectionString = ""
        End Set
    End Property


    Public Function BuildConnectionString(ByVal server As String, ByVal database As String, Optional ByVal user As String = "", Optional ByVal password As String = "") As String
        If user = "" Then
            Return String.Format("Server={0};Database={1};trusted_connection=true;", server, database)
        Else
            Return String.Format("Server={0};Database={1};user={2};password={3};", server, database, user, password)
        End If
    End Function

    Private _Connection As SqlConnection
    Public ReadOnly Property Connection() As SqlConnection
        Get
            If ReuseConnection Then
                If _Connection Is Nothing Then
                    If ConnectionString <> "" Then
                        _Connection = New SqlConnection(ConnectionString)
                    End If
                End If
                Return _Connection
            End If
            If ConnectionString = "" Then Return Nothing
            Return New SqlConnection(ConnectionString)
        End Get
    End Property

    Public Function ConnectionOK() As Boolean
        Try
            Dim conn As New SqlConnection(ConnectionString)
            conn.Open()
            conn.Close()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Friend Function GetSPCommand(ByVal storedProcedureName As String, ByVal ParamArray parameters() As Object) As SqlCommand
        'crea un nuevo objeto, asignale la conexion
        Dim cmd As New SqlCommand(storedProcedureName, Connection)
        'especifica que es un stored procedure lo que se ejecutara
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandTimeout = SQLHelper.CommandTimeout

        If Not parameters Is Nothing Then
            'carga los parametros
            If cmd.Parameters.Count <> parameters.Length Then
                If Not cmd.Connection.State = ConnectionState.Open Then cmd.Connection.Open()
                SqlCommandBuilder.DeriveParameters(cmd)
                If Not ReuseConnection Then cmd.Connection.Close()
            End If

            'asigna los valores
            If cmd.Parameters.Count > 0 AndAlso parameters.Length > 0 Then
                'valida para no tomar en cuenta los parametros de tipo return value
                Dim i, j As Integer
                i = 0 : j = 0
                If cmd.Parameters(0).Direction = ParameterDirection.ReturnValue Then i = 1
                For i = i To cmd.Parameters.Count - 1
                    Try
                        cmd.Parameters(i).Value = parameters(j)
                    Catch
                    End Try
                    j += 1
                Next
            End If
        End If
        'devuelve el objeto cmd
        Return cmd
    End Function

    Friend Function GetCommand(ByVal query As String) As Common.DbCommand
        Dim cmd As New SqlCommand(query, Connection)
        cmd.CommandTimeout = CommandTimeout
        Return cmd
    End Function

    Public Function GetReader(ByVal StoredProcedureName As String, _
    ByVal ParamArray parameters() As Object) As SqlDataReader
        Dim cmd As SqlCommand = GetSPCommand(StoredProcedureName, parameters)
        Return GetReader(cmd)
    End Function

    Public Function GetReader(ByVal cmd As SqlCommand) As SqlDataReader
        With cmd
            If .Connection.State = ConnectionState.Closed Then .Connection.Open()
        End With
        Return cmd.ExecuteReader(CommandBehavior.CloseConnection)
    End Function

    Public Function BuildQuery(ByVal columns As String, ByVal from As String, Optional ByVal where As String = "", Optional ByVal orderBy As String = "") As String
        Dim sb As New System.Text.StringBuilder
        If columns = "" Then columns = "*"
        sb.Append("SELECT ")
        sb.Append(columns)
        sb.Append(" FROM ")
        sb.Append(from)
        If Not where = "" Then
            sb.Append(" WHERE ")
            sb.Append(where)
        End If
        If Not orderBy = "" Then
            sb.Append(" ORDER BY ")
            sb.Append(orderBy)
        End If
        Return sb.ToString
    End Function

    Public Function GetReaderFromQuery(ByVal query As String) As SqlDataReader
        If query.ToLower.Contains("delete ") _
        OrElse query.ToLower.Contains("insert ") _
        OrElse query.ToLower.Contains("update ") _
        OrElse query.ToLower.Contains("/*") _
        OrElse query.ToLower.Contains("--") _
        OrElse query.ToLower.Contains("alter ") _
        OrElse query.ToLower.Contains("truncate ") _
        OrElse query.ToLower.Contains("create ") _
        OrElse query.ToLower.Contains("drop ") _
        OrElse query.ToLower.Contains("set ") Then
            Return Nothing
        Else
            Return GetReader(GetCommand(query))
        End If
    End Function

    Public Function GetTableWithName(tableName As String, ByVal StoredProcedureName As String, _
   ByVal ParamArray parameters() As Object) As DataTable
        Dim dt As New DataTable(tableName)
        Dim dr As SqlDataReader = GetReader(StoredProcedureName, parameters)
        If Not dr Is Nothing Then
            dt.Load(dr)
            dr.Close()
        End If
        Return dt
    End Function

    Public Function GetTableFromQueryWithName(tableName As String, ByVal query As String) As DataTable
        Dim dt As New DataTable(tableName)
        Dim dr As SqlDataReader = GetReaderFromQuery(query)
        If Not dr Is Nothing Then
            dt.Load(dr)
            dr.Close()
        End If
        Return dt
    End Function

    Public Function GetTable(ByVal StoredProcedureName As String, _
    ByVal ParamArray parameters() As Object) As DataTable
        Return GetTableWithName("Table1", StoredProcedureName, parameters)
    End Function

    Public Function GetTableFromQuery(ByVal query As String) As DataTable
        Return GetTableFromQueryWithName("Table1", query)
    End Function

    Public Function ExecuteScalar(ByVal StoredProcedureName As String, _
    ByVal ParamArray parameters() As Object) As Object
        Return ExecuteScalar(GetSPCommand(StoredProcedureName, parameters))
    End Function

    Public Function ExecuteScalarFromText(ByVal query As String) As Object
        Return ExecuteScalar(GetCommand(query))
    End Function

    Public Function ExecuteScalar(ByRef cmd As SqlCommand) As Object
        Dim r As Object = Nothing
        With cmd
            If .Connection.State = ConnectionState.Closed Then .Connection.Open()
            r = .ExecuteScalar
            If Not ReuseConnection Then .Connection.Close()
        End With
        Return r
    End Function

    Public Function ExecuteNonQuery(ByVal StoredProcedureName As String, _
    ByVal ParamArray parameters() As Object) As Integer
        Return ExecuteNonQuery(GetSPCommand(StoredProcedureName, parameters))
    End Function

    Public Function ExecuteNonQueryFromText(ByVal conn As SqlConnection, ByVal query As String)
        Dim cmd As SqlCommand = GetCommand(query)
        cmd.Connection = conn
        Return ExecuteNonQuery(cmd)
    End Function

    Public Function ExecuteNonQueryFromText(ByVal query As String) As Integer
        Return ExecuteNonQuery(GetCommand(query))
    End Function

    Public Function ExecuteNonQuery(ByRef cmd As SqlCommand) As Object
        Dim r As Object = Nothing
        With cmd
            If .Connection.State = ConnectionState.Closed Then .Connection.Open()
            r = .ExecuteNonQuery
            If Not ReuseConnection Then .Connection.Close()
        End With
        Return r
    End Function







End Module
