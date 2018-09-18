Imports System.Runtime.InteropServices
Imports Inventor

Public Class Settings
    Dim _invApp As Inventor.Application
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        _invApp = Marshal.GetActiveObject("Inventor.Application")
        Dim TolString() As String
        'For X = 0 To 4
        '    TolString = {X, My.Settings("LinP" & X), My.Settings("LinN" & X)}
        '    dgvLTolerance.Rows.Add(TolString)
        'Next
        'For X = 0 To 2
        '    TolString = {X, My.Settings("AngP" & X), My.Settings("AngN" & X)}
        '    dgvAngTolerance.Rows.Add(TolString)
        'Next
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub Characteristics_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ddpGeneralSettings.Width = CharacteristicsTab.Width - 8
        ddpSampleing.Width = ddpGeneralSettings.Width
        ddpDimensions.Width = ddpGeneralSettings.Width
        ddpNotes.Width = ddpGeneralSettings.Width
        ddpHoleCallout.Width = ddpGeneralSettings.Width
        ddpTolerance.Width = ddpGeneralSettings.Width
        gpbType.Width = CharacteristicsTab.Width - 17
        gpbUnits.Width = gpbType.Width
        cmbAngle.Width = gpbUnits.Width - 83
        cmbPrimary.Width = cmbAngle.Width
        cmbSecondary.Width = cmbAngle.Width
        ToleranceTab.Width = gpbType.Width
        rtbHoleCallout.Width = gpbType.Width
        rtbNotes.Width = gpbType.Width
        txtCustomer.Width = CharacteristicsTab.Width - 149
        txtDocName.Width = txtCustomer.Width
        txtDocNum.Width = txtCustomer.Width
        txtDocRev.Width = txtCustomer.Width
        txtJobNumber.Width = txtCustomer.Width
        txtLotSize.Width = txtCustomer.Width
        txtPartName.Width = txtCustomer.Width
        txtPartNumber.Width = txtCustomer.Width
        txtPartRev.Width = txtCustomer.Width
        txtSampleSize.Width = txtCustomer.Width
    End Sub

    Private Sub ddpDimensions_Resize(sender As Object, e As EventArgs) Handles ddpDimensions.Resize
        ddpNotes.Location = New Drawing.Point(ddpDimensions.Location.X, ddpDimensions.Location.Y + ddpDimensions.Height + 6)
        ddpHoleCallout.Location = New Drawing.Point(ddpNotes.Location.X, ddpNotes.Location.Y + ddpNotes.Height + 6)
    End Sub

    Private Sub ddpGeneralSettings_Resize(sender As Object, e As EventArgs) Handles ddpGeneralSettings.Resize
        ddpSampleing.Location = New Drawing.Point(ddpGeneralSettings.Location.X, ddpGeneralSettings.Location.Y + ddpGeneralSettings.Height + 6)
    End Sub

    Private Sub ddpNotes_Resize(sender As Object, e As EventArgs) Handles ddpNotes.Resize
        ddpHoleCallout.Location = New Drawing.Point(ddpNotes.Location.X, ddpNotes.Location.Y + ddpNotes.Height + 6)
    End Sub
End Class
