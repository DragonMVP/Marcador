Public Class Database
    Private g_conn As New ADODB.Connection
    Private rs As New ADODB.Recordset
    Private myDa As New OleDb.OleDbDataAdapter
    Private ConexString As String

    Public Sub New(ByVal IP As String, ByVal Catalog As String, ByVal ID As String, ByVal PASS As String)
        ConexString = "provider=sqloledb;" & _
        "Data Source=" + IP + ";" & _
        "Initial Catalog=" + Catalog + ";" & _
        "User Id=" + ID + ";" & _
        "Password=" + PASS + ""
    End Sub

    Public Sub Conectar()
        g_conn.Open(ConexString)
    End Sub

    Function Consulta(ByVal Query As String) As DataSet
        Dim datas As New DataSet
        rs.Open(Query, g_conn)
        myDa.Fill(datas, rs, "ALUMNO")
        Return datas
    End Function

    Public Sub Execute(ByVal Query As String)
        rs.Open(Query, g_conn)
    End Sub
End Class
