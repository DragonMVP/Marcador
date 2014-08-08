Imports System.Data.SqlClient

Public Class Database
    Private Connection As SqlConnection
    Private ConexString As String

    Public Sub New(ByVal IP As String, ByVal Catalog As String, ByVal ID As String, ByVal PASS As String)
        ConexString = "Data Source=" + IP + "; Initial Catalog=" + Catalog + "; User Id=" + ID + "; Password=" + PASS + ";MultipleActiveResultSets=true"
    End Sub

    Public Sub Conectar()
        Connection = New SqlConnection(ConexString)
        Connection.Open()
    End Sub

    Function Consulta(ByVal Query As String) As SqlDataReader
        Dim Command As New SqlCommand(Query, Connection)
        Dim Reader As SqlDataReader
        Reader = Command.ExecuteReader
        Return Reader
    End Function

    Public Sub Execute(ByVal Query As String)
        Dim Command As New SqlCommand(Query, Connection)
        Command.ExecuteNonQuery()
    End Sub

    Public Sub ExecuteStoredAcceso(ByVal pevCuenta As String, ByVal psvEstado As String, ByVal psvPerfil As String, ByVal psvBarrera As String)
        Dim Command As New SqlCommand("RegistrarAcceso", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Command.Parameters.Add("@pevCuenta", SqlDbType.VarChar)
        Command.Parameters.Add("@psvEstado", SqlDbType.VarChar)
        Command.Parameters.Add("@psvPerfil", SqlDbType.VarChar)
        Command.Parameters.Add("@psvBarrera", SqlDbType.VarChar)
        Command.Parameters("@pevCuenta").Value = pevCuenta
        Command.Parameters("@psvEstado").Value = psvEstado
        Command.Parameters("@psvPerfil").Value = psvPerfil
        Command.Parameters("@psvBarrera").Value = psvBarrera

        Command.ExecuteNonQuery()
    End Sub

    Public Sub ExecuteStoredLogGeneral(ByVal psvBarrera As String, ByVal psvErrorCapturado As String)
        Dim Command As New SqlCommand("RegistrarLogGeneral", Connection)
        Command.CommandType = CommandType.StoredProcedure
        Command.Parameters.Add("@psvBarrera", SqlDbType.VarChar)
        Command.Parameters.Add("@psvErrorCapturado", SqlDbType.VarChar)


        Command.Parameters("@psvBarrera").Value = psvBarrera
        Command.Parameters("@psvErrorCapturado").Value = psvErrorCapturado

        Command.ExecuteNonQuery()
    End Sub
End Class
