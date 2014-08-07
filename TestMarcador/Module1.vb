Imports System
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Module Module1

    Sub Main()
        Dim DB As New Database("192.168.218.15", "TestMarcador", "sa", "sa")
        Dim marcador As New apiTR515x.apiTR515x("192.168.218.12", 1001)
        Dim respuesta As String
        marcador.conectar()
        marcador.iniciarModoExtendido()
        DB.Conectar()



        While True
            'Try

            Console.WriteLine("Esperando lectura...")
            Dim Lectura As String = (marcador.leerRegistroModoExtendido())
            ' Lectura = "003B,0211321009,1,2014/05/13,10:17:03A8"
            'Lectura = "11XB,0211321009,9,HOLA"
            'Lectura = "153B,0211321009,1,2013/0A/13,10:17:03A8"
            'Lectura = "123123123123"
            Console.WriteLine(Lectura)
            If ValidarEntrada(Lectura) Then
                Console.WriteLine("Registro Correcto")
            Dim LecturaS As String() = Lectura.Split(",")
            'Console.WriteLine(LecturaS.GetValue(1))

            Dim ConsultaAlumno As DataSet = DB.Consulta("SELECT * FROM ALUMNO WHERE ID_ALUM = '" + LecturaS.GetValue(1) + "'")
            Console.WriteLine(ConsultaAlumno.Tables("ALUMNO").Rows.Count)
            If (ConsultaAlumno.Tables("ALUMNO").Rows.Count > 0) Then
                If (ConsultaAlumno.Tables("ALUMNO").Rows(0)(1) = "1") Then
                    marcador.enviarCodigo("ALO-", 10)
                Else
                    marcador.enviarCodigo("NO--", 10)
                End If
            Else
                marcador.enviarCodigo("NOOO", 10)
            End If

            DB.Execute("INSERT INTO MARCADO VALUES ('" + LecturaS(1) + "' , '" + LecturaS(3) + "','" + LecturaS(4) + "')")


            'Console.WriteLine("Continuar?")
            'respuesta = Console.ReadLine()
            'respuesta = "s"
            'If respuesta <> "s" Then
            'Exit While
            'End If
            'Catch Ex As Exception
            '    Console.WriteLine(Ex.Message)
                'End Try
            Else
                Console.WriteLine("Registro Incorrecto")
            End If
        End While
        For i = 1 To marcador.leerCantidadDeRegistros
            Console.WriteLine(marcador.leerRegistro(i))

        Next

        marcador.finalizarModoExtendido()
        marcador.Desconectar()
        Console.ReadLine()
    End Sub
    Public Sub agregarABitacora(ByRef registro As String)

        Try
            Dim path As String = Environment.CurrentDirectory + "\Bitacora.txt"
            If My.Computer.FileSystem.FileExists(path) Then
                Dim bitacora As New System.IO.StreamWriter(path, True)
                bitacora.WriteLine(registro)
                bitacora.Close()
                Console.WriteLine("Se agrego el registro a la bitacora.")
            Else
                Dim fs As FileStream = File.Create(path)
                fs.Close()
                Dim bitacora As New System.IO.StreamWriter(path, True)
                bitacora.WriteLine(registro)
                bitacora.Close()
                Console.WriteLine("Se agrego el registro a la bitacora recien creada.")
            End If
        Catch ex1 As System.IO.IOException
            Console.WriteLine("Ocurrio un error de entrada y salida.")
        Catch ex2 As Exception
            Console.WriteLine("Error")
        End Try
    End Sub

    Public Sub agregarABasura(ByRef basura As String)

        Try
            Dim path As String = Environment.CurrentDirectory + "\error.log"
            If My.Computer.FileSystem.FileExists(path) Then
                Dim basurero As New System.IO.StreamWriter(path, True)
                basurero.WriteLine(basura)
                basurero.Close()
                Console.WriteLine("Se agrego la basura al basurero.")
            Else
                Dim fs As FileStream = File.Create(path)
                fs.Close()
                Dim basurero As New System.IO.StreamWriter(path, True)
                basurero.WriteLine(basura)
                basurero.Close()
                Console.WriteLine("Se agrego la basura al log recien creado.")
            End If
        Catch ex1 As System.IO.IOException
            Console.WriteLine("Ocurrio un error de entrada y salida tratando de agregar.")
        Catch ex As Exception
            Console.WriteLine("Ocurrio un error fatal intentando agregar al basurero.")
        End Try

    End Sub



    Public Sub MostrarBitacora()
        Try
            Dim path As String = "Dame la direccion... o el gatito muere :("
            If My.Computer.FileSystem.FileExists(path) Then
                Dim bitacora As New System.IO.StreamReader(path)
                Dim registro_tmp = bitacora.ReadLine()
                While (registro_tmp <> "")
                    Console.WriteLine(registro_tmp)
                    registro_tmp = bitacora.ReadLine()
                End While
                bitacora.Close()
                Console.WriteLine("Se han mostrado todos los registros.")
            Else
                Console.WriteLine("La bitacora no existe.")
            End If
        Catch ex1 As System.IO.FileLoadException
            Console.WriteLine("Ocurrio un error al cargar el LOG.")
        Catch ex2 As Exception
            Console.WriteLine("Ocurrio un error fatal.")
        End Try
    End Sub

    Public Sub MostrarBasura()

        Try
            Dim path As String = "Quiero jugar un juego..."
            If My.Computer.FileSystem.FileExists(path) Then
                Dim basurero As New System.IO.StreamReader(path)
                Dim basura_tmp = basurero.ReadLine()
                While (basura_tmp <> "")
                    Console.WriteLine(basura_tmp)
                    basura_tmp = basurero.ReadLine()
                End While
                basurero.Close()
                Console.WriteLine("Se han mostrado toda la basura.")
            Else
                Console.WriteLine("El basurero no existe.")
            End If
        Catch ex1 As System.IO.FileLoadException
            Console.WriteLine("Ocurrio un erro al cargar el basurero.")
        Catch ex As Exception
            Console.WriteLine("Ocurrio un error fatal al tratar de mostrar el contenido del basurero.")
        End Try

    End Sub

    Function ValidarEntrada(ByVal Lectura As String) As Boolean
        Dim TestString() As String = Split(Lectura, ",")
        Dim Veredicto As Boolean = False

        Dim Split_1 As New System.Text.RegularExpressions.Regex("[0-9]{3}[A-Z]{1}")
        Dim Split_3 As New System.Text.RegularExpressions.Regex("[0-9]{4}/[0-9]{2}/[0-9]{2}")
        'Patrones TestString(0)= 003B
        'Parontes TestStribng(1)=02+#Numero de Cuenta
        'Patrones TestString(2)= in o out
        'Patrones TestString(3)= Fecha 2014/05/03
        'Patrones TestString(4)= 10:18:05AB

        For i = 0 To TestString.Count - 1

            Console.WriteLine("POSICION===> " + TestString(i))
        Next


        Try

            If TestString(0).Length = 4 Then
                Console.WriteLine("Entre al primer If")
                Console.WriteLine(TestString.Count)
                Console.WriteLine(Split_3.Matches(TestString(3)).Item(0).Success.ToString)
                '  Console.WriteLine(Split_3.Matches(TestString(3)).Item(0).ToString)
                If TestString.Count >= 3 And Split_3.Matches(TestString(3)).Item(0).Success Then
                    Veredicto = True
                End If
            End If

        Catch ex As Exception
            Veredicto = False
            Console.WriteLine("Entre a la excepcion")
        End Try
        Return Veredicto

    End Function

End Module
