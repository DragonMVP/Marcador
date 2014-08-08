<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBarrera
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Barrera = New System.Windows.Forms.TextBox()
        Me.Desc = New System.Windows.Forms.RichTextBox()
        Me.IP = New System.Windows.Forms.TextBox()
        Me.Puerto = New System.Windows.Forms.TextBox()
        Me.Activo = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Campus = New System.Windows.Forms.ComboBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Barrera
        '
        Me.Barrera.Location = New System.Drawing.Point(72, 14)
        Me.Barrera.Name = "Barrera"
        Me.Barrera.Size = New System.Drawing.Size(119, 20)
        Me.Barrera.TabIndex = 0
        '
        'Desc
        '
        Me.Desc.Location = New System.Drawing.Point(72, 40)
        Me.Desc.Name = "Desc"
        Me.Desc.Size = New System.Drawing.Size(181, 73)
        Me.Desc.TabIndex = 1
        Me.Desc.Text = ""
        '
        'IP
        '
        Me.IP.Location = New System.Drawing.Point(72, 119)
        Me.IP.Name = "IP"
        Me.IP.Size = New System.Drawing.Size(119, 20)
        Me.IP.TabIndex = 2
        '
        'Puerto
        '
        Me.Puerto.Location = New System.Drawing.Point(197, 119)
        Me.Puerto.Name = "Puerto"
        Me.Puerto.Size = New System.Drawing.Size(56, 20)
        Me.Puerto.TabIndex = 3
        '
        'Activo
        '
        Me.Activo.AutoSize = True
        Me.Activo.Location = New System.Drawing.Point(197, 16)
        Me.Activo.Name = "Activo"
        Me.Activo.Size = New System.Drawing.Size(56, 17)
        Me.Activo.TabIndex = 4
        Me.Activo.Text = "Activo"
        Me.Activo.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Barrera"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Desc."
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(7, 122)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "IP / Puerto"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 149)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(45, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Campus"
        '
        'Campus
        '
        Me.Campus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Campus.FormattingEnabled = True
        Me.Campus.Items.AddRange(New Object() {"UNITEC Tegucigalpa"})
        Me.Campus.Location = New System.Drawing.Point(72, 145)
        Me.Campus.Name = "Campus"
        Me.Campus.Size = New System.Drawing.Size(181, 21)
        Me.Campus.TabIndex = 10
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(259, 16)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 11
        Me.Button1.Text = "New"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(259, 74)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 12
        Me.Button2.Text = "Save"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(259, 45)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(75, 23)
        Me.Button3.TabIndex = 13
        Me.Button3.Text = "Mod"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(259, 103)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(75, 23)
        Me.Button4.TabIndex = 14
        Me.Button4.Text = "Delete"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(259, 132)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(75, 23)
        Me.Button5.TabIndex = 15
        Me.Button5.Text = "Search"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'frmBarrera
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(337, 178)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Campus)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Activo)
        Me.Controls.Add(Me.Puerto)
        Me.Controls.Add(Me.IP)
        Me.Controls.Add(Me.Desc)
        Me.Controls.Add(Me.Barrera)
        Me.Name = "frmBarrera"
        Me.Text = "frmBarrera"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Barrera As System.Windows.Forms.TextBox
    Friend WithEvents Desc As System.Windows.Forms.RichTextBox
    Friend WithEvents IP As System.Windows.Forms.TextBox
    Friend WithEvents Puerto As System.Windows.Forms.TextBox
    Friend WithEvents Activo As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Campus As System.Windows.Forms.ComboBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
End Class
