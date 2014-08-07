Public Class frmBarrera
    '0 - NEW BARRERA
    '1 - MOD BARRERA
    Private SaveOption As Integer

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If (SaveOption = 0) Then
            Dim ACTIVE As Integer = 0
            If (Activo.Checked) Then
                ACTIVE = 1
            End If
            frmPrincipal.DB.Execute("INSERT INTO barreras VALUES ('" + Barrera.Text + "','" + Desc.Text + "','" + IP.Text + "'," + Integer.Parse(Puerto.Text).ToString + "," + ACTIVE.ToString + ",1) ")
        End If
    End Sub

    Private Sub frmBarrera_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        frmPrincipal.DB.Desconectar()
    End Sub

    Private Sub frmBarrera_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        frmPrincipal.DB.Conectar()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SaveOption = 0
    End Sub
End Class