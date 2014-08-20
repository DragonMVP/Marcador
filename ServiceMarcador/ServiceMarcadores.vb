Imports System.Threading
Imports System.IO
Imports System.Net.NetworkInformation
Imports System.Data.SqlClient
Imports AMS.Profile

Public Class ServiceMarcadores
    'Private MarcadoresConectados As Integer
    Private CheckConnection As Thread
    Private Barreras As List(Of Barrera)
    Private DataBase As Database
    Private ConfigFile As Ini

    Protected Overrides Sub OnStart(ByVal args As String())
        ConfigFile = New Ini(AppDomain.CurrentDomain.BaseDirectory + "Servidor.ini")
        Dim IPServidor = ConfigFile.GetValue("Servidor", "IP")
        Dim DatabaseName = ConfigFile.GetValue("Servidor", "Database")
        Dim Username = ConfigFile.GetValue("Servidor", "Username")
        Dim Password = ConfigFile.GetValue("Servidor", "Password")

        Barreras = New List(Of Barrera)
        CheckConnection = New Thread(AddressOf CheckConnections)
        DataBase = New Database(IPServidor, DatabaseName, Username, Password)
        DataBase.Conectar()

        Dim DataBarreras As SqlDataReader = DataBase.Consulta("SELECT ip,puerto,barrera FROM barreras WHERE activo = 1")
        If (DataBarreras.HasRows) Then
            Dim I As Integer = 0
            While (DataBarreras.Read)
                Barreras.Add(New Barrera(DataBarreras("ip"), DataBarreras("puerto"), DataBase, DataBarreras("barrera")))
                Barreras(I).Conectar()
                I += 1
            End While
        End If

        CheckConnection.Start()
    End Sub

    Protected Overrides Sub OnStop()

        For I = 0 To Barreras.Count - 1
            Barreras(I).Desconectar()
        Next

        Barreras.Clear()
    End Sub

    Private Sub CheckConnections()
        Do
            For I = 0 To Barreras.Count - 1
                If (Barreras(I).isConnected = False And Barreras(I).getActivo()) Then
                    Barreras(I).Desconectar()
                    Console.WriteLine("Desconectado " + I.ToString)
                ElseIf (Barreras(I).isConnected And Barreras(I).getActivo() = False) Then
                    Barreras(I).Conectar()
                    Console.WriteLine("Conectado " + I.ToString)
                End If
            Next
        Loop
    End Sub

End Class
