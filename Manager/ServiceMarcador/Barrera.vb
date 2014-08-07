Imports System.Threading
Imports System.Data.SqlClient
Imports System.Net.NetworkInformation

Public Class Barrera
    Private IP As String
    Private Port As Integer
    Private Hilo As Thread
    Private Marcador As apiTR515x.apiTR515x
    Private AutorizarAdmin As Boolean = False
    Private AutorizarAlumno As Boolean = False
    Private AutorizarDocente As Boolean = False
    Private DB As Database
    Private NumMarcador As Integer
    Private Activo As Boolean

    Public Sub New(ByVal IP As String, ByVal Puerto As Integer, ByRef BaseDatos As Database, ByVal NumMarcador As Integer)
        DB = BaseDatos
        Me.IP = IP
        Port = Puerto
        Me.NumMarcador = NumMarcador

        Dim Autorizaciones As SqlDataReader = DB.Consulta("SELECT AutorizaDocentes,AutorizaAdministrativos,AutorizaAlumnos FROM barreras WHERE ip = '" + IP + "'")
        If Autorizaciones.Read() Then
            If (Autorizaciones("AutorizaDocentes") = "1") Then
                AutorizarDocente = True
            End If
            If (Autorizaciones("AutorizaAdministrativos") = "1") Then
                AutorizarAdmin = True
            End If
            If (Autorizaciones("AutorizaAlumnos") = "1") Then
                AutorizarAlumno = True
            End If
        End If
        Console.WriteLine("FINISH")
    End Sub

    Public Sub Conectar()
        Marcador = New apiTR515x.apiTR515x(IP, Port)
        Marcador.conectar()
        Marcador.iniciarModoExtendido()
        Console.WriteLine("FINISH CONECTADO")
        Hilo = New Thread(AddressOf LecturaMarcador)
        Hilo.IsBackground = True
        Hilo.Start()
        Activo = True
    End Sub

    Public Sub Desconectar()
        Hilo.Abort()

        Marcador.Desconectar()
        Activo = False
    End Sub

    Public Function getActivo() As Boolean
        Return Activo
    End Function

    Public Function isConnected() As Boolean
        Dim pingComputer As Ping = New Ping()
        Dim reply As PingReply = pingComputer.Send(IP)
        If reply.Status = IPStatus.Success Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub LecturaMarcador()
        Do
            Try
                Dim Lectura As String = (Marcador.leerRegistroModoExtendido())
                If ValidarEntrada(Lectura) Then
                    Dim PartesLectura As String() = Lectura.Split(",")
                    Dim Consulta As SqlDataReader = DB.Consulta("SELECT estado FROM informacionX WHERE codigoCarne = '" + PartesLectura.GetValue(1).ToString + "'")
                    Dim RANGO As String
                    RANGO = PartesLectura.GetValue(1).ToString.Substring(0, 2)
                    If (Consulta.Read) Then
                        If (Consulta("estado") = "1") Then
                            Marcador.enviarCodigo("8000", "01")
                            CheckRelay(RANGO)
                            agregarABitacora(Lectura, 0, NumMarcador)
                        Else
                            Marcador.enviarCodigo("8001", "01")
                            agregarABitacora(Lectura, 1, NumMarcador)
                        End If
                    Else
                        Marcador.enviarCodigo("ERR-", "01")
                        agregarABitacora(Lectura, -1, NumMarcador)
                    End If
                Else
                    agregarABasura(Lectura, NumMarcador)
                    Marcador.eliminarRegistros()
                End If 'Fin if validar Lecutura
            Catch Exception As System.ArgumentException
                Console.WriteLine(Exception)
            End Try
        Loop
    End Sub

    Private Sub agregarABitacora(ByRef registro As String, ByVal estado As String, ByVal IdMarcador As Integer)
        Try
            '#registro, cuenta, in - out, fecha
            'cuenta, estado, perfil, barrera, fecha(DATE)
            Dim valores As String() = registro.Split(",")
            Dim Fecha As Date
            Fecha = Convert.ToDateTime(valores(3))
            DB.Execute("INSERT INTO Accesos(CUENTA, ESTADO, PERFIL, BARRERA) VALUES ('" + valores(1).ToString + "','" + estado.ToString + "','" + valores(2).ToString + "','" + IdMarcador.ToString + "')")

        Catch ex1 As System.IO.IOException
        Catch ex2 As Exception
            Console.WriteLine(ex2)
        End Try
    End Sub

    Private Sub agregarABasura(ByRef basura As String, ByVal marcador As Integer)
        Try
            'LogGenerado

            'barrera, errorGenerado, fecha(DATE)
            Dim Fecha As Date
            Fecha = Convert.ToDateTime(My.Computer.Clock.LocalTime.ToString())
            DB.Execute("INSERT INTO logGeneral(barrera, errorCapturado) VALUES ('" + marcador + "','" + basura + "')")

        Catch ex1 As System.IO.IOException
        Catch ex As Exception
        End Try
    End Sub

    Private Function ValidarEntrada(ByVal Lectura As String) As Boolean
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


    Private Sub CheckRelay(RANGO As String)


        If (RANGO.Equals("01")) Then 'Si fue administrador abrimos
            If (Me.AutorizarAdmin = True) Then
                Marcador.activarRelay("01")
            End If 'FIn if Rango 1

        ElseIf (Me.AutorizarAlumno = True) Then

            If (RANGO.Equals("02")) Then
                Marcador.activarRelay("01")
            End If ''FIn if Rango 2

        ElseIf (Me.AutorizarDocente = True) Then

            If (RANGO.Equals("03")) Then
                Marcador.activarRelay("01")
            End If 'FIn if Rango 3
        End If 'End If todos los casos

    End Sub



End Class
