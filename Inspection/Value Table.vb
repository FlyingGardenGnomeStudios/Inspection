Imports System.Runtime.InteropServices
Imports Inventor

Public Class Value_Table
    Dim _invApp As Inventor.Application
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        _invApp = Marshal.GetActiveObject("Inventor.Application")
        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class