﻿Imports System.ComponentModel
Imports System.Configuration.Install

Public Class ProjectInstaller

    Public Sub New()

        MyBase.New()

        'El Diseñador de componentes requiere esta llamada.
        InitializeComponent()
        ServiceInstaller1.DisplayName = "Barreras"
        'Agregue el código de inicialización después de llamar a InitializeComponent

    End Sub

End Class
