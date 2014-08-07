Option Explicit On
Imports System
Imports System.ServiceProcess
Imports System.Diagnostics
Imports System.Threading

Public Class Form1
    Dim horas As Integer
    Dim minutos As Integer
    Dim segundos As Integer
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Timer1.Enabled Then
            Timer1.Stop()
            Label2.Text = "00"
            Label3.Text = "00"
            Label4.Text = "00"
            Timer1.Start()
        Else
            Label2.Text = "00"
            Label3.Text = "00"
            Label4.Text = "00"
            Timer1.Start()
        End If
        Stop_Start_Service(servicesCommands.StartService, "ServiceLayer")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Timer1.Stop()
        Stop_Start_Service(servicesCommands.StopService, "ServiceLayer")
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Interval = 1000
        horas = Label2.Text
        minutos = Label3.Text
        segundos = Label4.Text
        If segundos = 60 Then
            segundos = 0
            minutos += 1
        Else
            segundos += 1
        End If

        If minutos = 60 Then
            minutos = 0
            horas += 1
        End If
        Label2.Text = horas.ToString.PadLeft(2, "0")
        Label3.Text = minutos.ToString.PadLeft(2, "0")
        Label4.Text = segundos.ToString.PadLeft(2, "0")
    End Sub


    Public Enum servicesCommands
        StartService = 1
        StopService = 0
    End Enum 'SimpleServiceCustomCommands

    Private Sub Stop_Start_Service(Action As servicesCommands, Name_Service As String)
        Dim scServices() As ServiceController
        scServices = ServiceController.GetServices

        Dim scTemp As ServiceController
        For Each scTemp In scServices
            If scTemp.ServiceName = Name_Service Then
                Dim sc As New ServiceController(Name_Service)
                If Action = 0 Then
                    Try
                        sc.Stop()
                    Catch ex As System.InvalidOperationException
                        MsgBox("Debe ejecutar el programa con permisos de administrador:" & vbCrLf & ex.Message)
                    End Try
                Else
                    Try
                        sc.Start()
                    Catch ex As System.InvalidOperationException
                        MsgBox("Debe ejecutar el programa con permisos de administrador" & vbCrLf & ex.Message)
                    End Try
                End If
               
            End If
        Next

    End Sub
End Class
