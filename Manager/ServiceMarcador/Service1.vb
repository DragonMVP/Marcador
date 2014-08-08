Imports System.Threading
Imports System.IO

Public Class Service1
    'Lista de marcadores
    Private Marcadores As List(Of apiTR515x.apiTR515x)

    'Numero de hilos activos para manejarlos con los arraylist
    Private HilosActivos As Integer
    Private HilosMarcadores As List(Of Thread)


    Protected Overrides Sub OnStart(ByVal args() As String)
        HilosActivos = 0
        Marcadores = New List(Of apiTR515x.apiTR515x)
        HilosMarcadores = New List(Of Thread)
        'Objeto que va a leer del archivo INI
        Dim LectorConfig As New AMS.Profile.Ini("C:\MarcadorConfig\Config.ini")
        'Se obtiene el valor de el INI ; Numero de marcadores
        Dim numMarcador As Integer = LectorConfig.GetValue("Marcadores", "numMarcadores")
        'Dejando las IP consecutivas :D
        For I = 0 To numMarcador - 1
            'Se crea el marcador y se conecta :D
            Marcadores.Add(New apiTR515x.apiTR515x(LectorConfig.GetValue("IP", "num" + I.ToString), 1001))
            Marcadores(I).conectar()
            Marcadores(I).iniciarModoExtendido()

            'Se crean los hilos :D
            HilosMarcadores.Add(New Thread(AddressOf HiloMarcador))
            HilosMarcadores(I).IsBackground = True
            HilosMarcadores(I).Start()
        Next
    End Sub

    Private Sub HiloMarcador()
        Dim MarcadorHilo As Integer = HilosActivos
        HilosActivos += 1
        Do
            LecturaMarcador(Marcadores(MarcadorHilo))
        Loop
    End Sub

    Private Sub LecturaMarcador(ByRef Marcador As apiTR515x.apiTR515x)
        'Try
        Dim Lectura As String = (Marcador.leerRegistroModoExtendido())
        If ValidarEntrada(Lectura) Then
            agregarABitacora(Lectura)
            Marcador.enviarCodigo("ALFA", 10)
            '---------- Parte Conexion a BD ------------'
            'Dim PartesLectura As String() = Lectura.Split(",")
            'Dim ConsultaAlumno As DataSet = DB.Consulta("SELECT * FROM ALUMNO WHERE ID_ALUM = '" + PartesLectura.GetValue(1) + "'")
            'If (ConsultaAlumno.Tables("ALUMNO").Rows.Count > 0) Then
            '    If (ConsultaAlumno.Tables("ALUMNO").Rows(0)(1) = "1") Then
            '        Marcador.enviarCodigo("ALO-", 10)
            '    Else
            '        Marcador.enviarCodigo("NO--", 10)
            '    End If
            'Else
            '    Marcador.enviarCodigo("NOOO", 10)
            'End If
        Else
            agregarABasura(Lectura)
        End If
        Marcador.eliminarRegistros()
    End Sub

    Protected Overrides Sub OnStop()
        For i = 0 To Marcadores.Count - 1
            Marcadores(i).finalizarModoExtendido()
            Marcadores(i).Desconectar()
        Next
    End Sub
    Public Sub agregarABitacora(ByRef registro As String)
        Try
            Dim path As String = "C:\MarcadorConfig\Bitacora.txt"
            If My.Computer.FileSystem.FileExists(path) Then
                Dim bitacora As New System.IO.StreamWriter(path, True)
                bitacora.WriteLine(registro)
                bitacora.Close()
            Else
                Dim fs As FileStream = File.Create(path)
                fs.Close()
                Dim bitacora As New System.IO.StreamWriter(path, True)
                bitacora.WriteLine(registro)
                bitacora.Close()
            End If
        Catch ex1 As System.IO.IOException
        Catch ex2 As Exception
        End Try
    End Sub

    Public Sub agregarABasura(ByRef basura As String)
        Try
            Dim path As String = "C:\MarcadorConfig\error.log"
            If My.Computer.FileSystem.FileExists(path) Then
                Dim basurero As New System.IO.StreamWriter(path, True)
                basurero.WriteLine(basura)
                basurero.Close()
            Else
                Dim fs As FileStream = File.Create(path)
                fs.Close()
                Dim basurero As New System.IO.StreamWriter(path, True)
                basurero.WriteLine(basura)
                basurero.Close()
            End If
        Catch ex1 As System.IO.IOException
        Catch ex As Exception
        End Try
    End Sub

    Public Sub MostrarBitacora()
        Try
            Dim path As String = "C:\MarcadorConfig\Bitacora.txt"
            If My.Computer.FileSystem.FileExists(path) Then
                Dim bitacora As New System.IO.StreamReader(path)
                Dim registro_tmp = bitacora.ReadLine()
                While (registro_tmp <> "")
                    Console.WriteLine(registro_tmp)
                    registro_tmp = bitacora.ReadLine()
                End While
                bitacora.Close()
            End If
        Catch ex1 As System.IO.FileLoadException
        Catch ex2 As Exception
        End Try
    End Sub

    Public Sub MostrarBasura()
        Try
            Dim path As String = "C:\MarcadorConfig\error.log"
            If My.Computer.FileSystem.FileExists(path) Then
                Dim basurero As New System.IO.StreamReader(path)
                Dim basura_tmp = basurero.ReadLine()
                While (basura_tmp <> "")
                    basura_tmp = basurero.ReadLine()
                End While
                basurero.Close()
            End If
        Catch ex1 As System.IO.FileLoadException
        Catch ex As Exception
        End Try

    End Sub

    Function ValidarEntrada(ByVal Lectura As String) As Boolean
        Dim TestString() As String = Split(Lectura, ",")
        Dim Veredicto As Boolean = False

        Dim Split_1 As New System.Text.RegularExpressions.Regex("[0-9]{3}[A-Z]{1}")
        Dim Split_3 As New System.Text.RegularExpressions.Regex("[0-9]{4}/[0-9]{2}/[0-9]{2}")

        Try
            If TestString(0).Length = 4 Then
                If TestString.Length >= 3 And Split_3.Matches(TestString(3)).Item(0).Success Then
                    Veredicto = True
                End If
            End If

        Catch ex As Exception
            Veredicto = False
        End Try
        Return Veredicto
    End Function

End Class
