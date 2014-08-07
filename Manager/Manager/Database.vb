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

    Public Sub Desconectar()
        Connection.Close()
    End Sub
End Class
