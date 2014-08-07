Imports System.Threading
Imports System.Net.NetworkInformation
Imports System.Data.SqlClient

Module Module1

    Sub Main()
        OnStart()
        While True

        End While
    End Sub
    'Numero de hilos activos para manejarlos con los arraylist
    Private HilosActivos As Integer
    Private CheckConnection As Thread
    Private Barreras As List(Of Barrera)
    Private DataBase As Database

    Private Sub OnStart()
        HilosActivos = 0
        Barreras = New List(Of Barrera)
        CheckConnection = New Thread(AddressOf CheckConnections)
        DataBase = New Database("192.168.218.130\sqlseguridad", "Carnetizacion", "sps", "sps123")
        DataBase.Conectar()

        Dim DataBarreras As SqlDataReader = DataBase.Consulta("SELECT ip,puerto FROM barreras WHERE activo = 1")
        If (DataBarreras.HasRows) Then
            Dim I As Integer = 0
            While (DataBarreras.Read)
                Barreras.Add(New Barrera(DataBarreras("ip"), DataBarreras("puerto"), DataBase, I))
                Barreras(I).Conectar()
                I += 1
            End While
        End If

        CheckConnection.Start()
    End Sub

    Private Sub OnStop()

        For i = 0 To Barreras.Count - 1
            Barreras(i).Desconectar()
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

End Module
