Imports System.Runtime.InteropServices
Imports Inspection.Inspection
Imports Inventor

Public Class Value_Table
    Dim _invApp As Inventor.Application
    Dim StandardAddinServer As StandardAddInServer
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        _invApp = Marshal.GetActiveObject("Inventor.Application")
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Function PopAddin(CalledFunction As StandardAddInServer) As Object
        PopAddin = CalledFunction
        Return Nothing
    End Function
    Private Sub Value_Table_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        dgvDimValues.Height = dgvDimValues.Parent.Height
    End Sub
    Public Sub DiametralDim(oDim As DiameterGeneralDimension, RefKey As String)
        dgvDimValues.Rows.Add()
        dgvDimValues(dgvDimValues.Columns("Balloon").Index, dgvDimValues.RowCount - 1).Value = dgvDimValues.RowCount
        dgvDimValues(dgvDimValues.Columns("Ref").Index, dgvDimValues.RowCount - 1).Value = RefKey
        dgvDimValues(dgvDimValues.Columns("Value").Index, dgvDimValues.RowCount - 1).Value = oDim.Text.Text
        dgvDimValues(dgvDimValues.Columns("Qty").Index, dgvDimValues.RowCount - 1).Value = "TBD"
        dgvDimValues(dgvDimValues.Columns("Type").Index, dgvDimValues.RowCount - 1).Value = oDim.Type
        dgvDimValues(dgvDimValues.Columns("UTol").Index, dgvDimValues.RowCount - 1).Value = oDim.Tolerance.Upper
        dgvDimValues(dgvDimValues.Columns("LTol").Index, dgvDimValues.RowCount - 1).Value = oDim.Tolerance.Lower
        Dim Values As String = RefKey
        '    "<Number>" & dgvDimValues.Rows.Count + 1 &
        '"<Reference>" & RefKey &
        '"<Value>" & oDim.Text.Text &
        '"<QTY>" & "TBD" &
        '"<Type>" & oDim.Type &
        '"<UTol>" & oDim.Tolerance.Upper &
        '"<LTol>" & oDim.Tolerance.Lower
        InsertSketchedSymbolSample(oDim, oDim.Text.RangeBox.MaxPoint, oDim.Text.RangeBox.MinPoint, Values)
    End Sub
    Public Sub LinearDim(oDim As LinearGeneralDimension, RefKey As String)
        dgvDimValues.Rows.Add()
        dgvDimValues(dgvDimValues.Columns("Balloon").Index, dgvDimValues.RowCount - 1).Value = dgvDimValues.RowCount
        dgvDimValues(dgvDimValues.Columns("Ref").Index, dgvDimValues.RowCount - 1).Value = RefKey
        dgvDimValues(dgvDimValues.Columns("Value").Index, dgvDimValues.RowCount - 1).Value = oDim.Text.Text
        dgvDimValues(dgvDimValues.Columns("Qty").Index, dgvDimValues.RowCount - 1).Value = "TBD"
        dgvDimValues(dgvDimValues.Columns("Type").Index, dgvDimValues.RowCount - 1).Value = oDim.Type
        dgvDimValues(dgvDimValues.Columns("UTol").Index, dgvDimValues.RowCount - 1).Value = oDim.Tolerance.Upper
        dgvDimValues(dgvDimValues.Columns("LTol").Index, dgvDimValues.RowCount - 1).Value = oDim.Tolerance.Lower
        Dim Values As String = RefKey
        '"<Number>" & dgvDimValues.Rows.Count &
        '"<Reference>" & RefKey &
        '"<Size>" & My.Settings.BalloonSize &
        '"<Position>" & My.Settings.BalloonPos &
        '"<BalloonColor>" = "TBD" &
        '"<TextColor>" = "TBD" &
        '"<Value>" & oDim.Text.Text &
        '"<QTY>" & "TBD" &
        '"<Type>" & oDim.Type &
        '"<UTol>" & oDim.Tolerance.Upper &
        '"<LTol>" & oDim.Tolerance.Lower
        InsertSketchedSymbolSample(oDim, oDim.Text.RangeBox.MaxPoint, oDim.Text.RangeBox.MinPoint, Values)
    End Sub

    Public Sub RadialDim(oDim As RadiusGeneralDimension, RefKey As String)
        dgvDimValues.Rows.Add()
        dgvDimValues(dgvDimValues.Columns("Balloon").Index, dgvDimValues.RowCount - 1).Value = dgvDimValues.RowCount
        dgvDimValues(dgvDimValues.Columns("Ref").Index, dgvDimValues.RowCount - 1).Value = RefKey
        dgvDimValues(dgvDimValues.Columns("Value").Index, dgvDimValues.RowCount - 1).Value = oDim.Text.Text
        dgvDimValues(dgvDimValues.Columns("Qty").Index, dgvDimValues.RowCount - 1).Value = "TBD"
        dgvDimValues(dgvDimValues.Columns("Type").Index, dgvDimValues.RowCount - 1).Value = oDim.Type
        dgvDimValues(dgvDimValues.Columns("UTol").Index, dgvDimValues.RowCount - 1).Value = oDim.Tolerance.Upper
        dgvDimValues(dgvDimValues.Columns("LTol").Index, dgvDimValues.RowCount - 1).Value = oDim.Tolerance.Lower
        Dim Values As String = RefKey
        '"<Number>" & dgvDimValues.Rows.Count &
        '    "<Reference>" & RefKey &
        '    "<Value>" & oDim.Text.Text &
        '    "<QTY>" & "TBD" &
        '    "<Type>" & oDim.Type &
        '    "<UTol>" & oDim.Tolerance.Upper &
        '    "<LTol>" & oDim.Tolerance.Lower
        InsertSketchedSymbolSample(oDim, oDim.Text.RangeBox.MaxPoint, oDim.Text.RangeBox.MinPoint, Values)
    End Sub
    Public Sub Note(oDim As DrawingNote, RefKey As String)
        dgvDimValues.Rows.Add()
        dgvDimValues(dgvDimValues.Columns("Balloon").Index, dgvDimValues.RowCount - 1).Value = dgvDimValues.RowCount
        dgvDimValues(dgvDimValues.Columns("Ref").Index, dgvDimValues.RowCount - 1).Value = RefKey
        dgvDimValues(dgvDimValues.Columns("Value").Index, dgvDimValues.RowCount - 1).Value = oDim.Text
        dgvDimValues(dgvDimValues.Columns("Qty").Index, dgvDimValues.RowCount - 1).Value = "TBD"
        dgvDimValues(dgvDimValues.Columns("Type").Index, dgvDimValues.RowCount - 1).Value = oDim.Type
        Dim Values As String = RefKey
        '"<Number>" & dgvDimValues.Rows.Count &
        '    "<Reference>" & RefKey &
        '    "<Value>" & oDim.Text &
        '    "<QTY>" & "TBD" &
        '    "<Type>" & oDim.Type &
        '    "<UTol>" & oDim.Tolerance.Upper &
        '    "<LTol>" & oDim.Tolerance.Lower
        InsertSketchedSymbolSample(oDim, oDim.RangeBox.MaxPoint, oDim.RangeBox.MinPoint, Values)
    End Sub
    Public Sub HoleTableTag(oDim As HoleTag, RefKey As String)
        dgvDimValues.Rows.Add()
        dgvDimValues(dgvDimValues.Columns("Balloon").Index, dgvDimValues.RowCount - 1).Value = dgvDimValues.RowCount
        dgvDimValues(dgvDimValues.Columns("Ref").Index, dgvDimValues.RowCount - 1).Value = RefKey
        dgvDimValues(dgvDimValues.Columns("Value").Index, dgvDimValues.RowCount - 1).Value = oDim.Text
        dgvDimValues(dgvDimValues.Columns("Qty").Index, dgvDimValues.RowCount - 1).Value = "TBD"
        dgvDimValues(dgvDimValues.Columns("Type").Index, dgvDimValues.RowCount - 1).Value = oDim.Type
        Dim Values As String = RefKey
        '"<Number>" & dgvDimValues.Rows.Count &
        '    "<Reference>" & RefKey &
        '    "<Value>" & oDim.Text &
        '    "<QTY>" & "TBD" &
        '    "<Type>" & oDim.Type &
        '    "<UTol>" & oDim.Tolerance.Upper &
        '    "<LTol>" & oDim.Tolerance.Lower
        InsertSketchedSymbolSample(oDim, oDim.RangeBox.MaxPoint, oDim.RangeBox.MinPoint, Values)
    End Sub
    Public Sub Surface(oDim As SurfaceTextureSymbol, RefKey As String)
        dgvDimValues.Rows.Add()
        dgvDimValues(dgvDimValues.Columns("Balloon").Index, dgvDimValues.RowCount - 1).Value = dgvDimValues.RowCount
        dgvDimValues(dgvDimValues.Columns("Ref").Index, dgvDimValues.RowCount - 1).Value = RefKey
        dgvDimValues(dgvDimValues.Columns("Value").Index, dgvDimValues.RowCount - 1).Value = oDim.Text.Text
        dgvDimValues(dgvDimValues.Columns("Qty").Index, dgvDimValues.RowCount - 1).Value = "TBD"
        dgvDimValues(dgvDimValues.Columns("Type").Index, dgvDimValues.RowCount - 1).Value = oDim.Type
        dgvDimValues(dgvDimValues.Columns("UTol").Index, dgvDimValues.RowCount - 1).Value = oDim.MaximumRoughness
        dgvDimValues(dgvDimValues.Columns("LTol").Index, dgvDimValues.RowCount - 1).Value = oDim.MinimumRoughness
        Dim Values As String = RefKey
        '"<Number>" & dgvDimValues.Rows.Count &
        '    "<Reference>" & RefKey &
        '    "<Value>" & oDim.Text.Text &
        '    "<QTY>" & "TBD" &
        '    "<Type>" & oDim.Type &
        '    "<UTol>" & oDim.Tolerance.Upper &
        '    "<LTol>" & oDim.Tolerance.Lower
        InsertSketchedSymbolSample(oDim, oDim.Position, oDim.Position, Values)
    End Sub
    Public Sub HoleTag(oDim As HoleThreadNote, RefKey As String)
        dgvDimValues.Rows.Add()
        dgvDimValues(dgvDimValues.Columns("Balloon").Index, dgvDimValues.RowCount - 1).Value = dgvDimValues.RowCount
        dgvDimValues(dgvDimValues.Columns("Ref").Index, dgvDimValues.RowCount - 1).Value = RefKey
        dgvDimValues(dgvDimValues.Columns("Value").Index, dgvDimValues.RowCount - 1).Value = oDim.Text.Text
        dgvDimValues(dgvDimValues.Columns("Qty").Index, dgvDimValues.RowCount - 1).Value = "TBD"
        dgvDimValues(dgvDimValues.Columns("Type").Index, dgvDimValues.RowCount - 1).Value = oDim.Type
        dgvDimValues(dgvDimValues.Columns("UTol").Index, dgvDimValues.RowCount - 1).Value = oDim.Tolerance.Upper
        dgvDimValues(dgvDimValues.Columns("LTol").Index, dgvDimValues.RowCount - 1).Value = oDim.Tolerance.Lower
        Dim Values As String = RefKey
        '"<Number>" & dgvDimValues.Rows.Count &
        '    "<Reference>" & RefKey &
        '    "<Value>" & oDim.Text.Text &
        '    "<QTY>" & "TBD" &
        '    "<Type>" & oDim.Type &
        '    "<UTol>" & oDim.Tolerance.Upper &
        '    "<LTol>" & oDim.Tolerance.Lower
        InsertSketchedSymbolSample(oDim, oDim.Text.RangeBox.MaxPoint, oDim.Text.RangeBox.MinPoint, Values)
    End Sub
    Public Sub Chamfer(oDim As ChamferNote, RefKey As String)
        dgvDimValues.Rows.Add()
        dgvDimValues(dgvDimValues.Columns("Balloon").Index, dgvDimValues.RowCount - 1).Value = dgvDimValues.RowCount
        dgvDimValues(dgvDimValues.Columns("Ref").Index, dgvDimValues.RowCount - 1).Value = RefKey
        dgvDimValues(dgvDimValues.Columns("Value").Index, dgvDimValues.RowCount - 1).Value = oDim.Text
        dgvDimValues(dgvDimValues.Columns("Qty").Index, dgvDimValues.RowCount - 1).Value = "TBD"
        dgvDimValues(dgvDimValues.Columns("Type").Index, dgvDimValues.RowCount - 1).Value = oDim.Type
        dgvDimValues(dgvDimValues.Columns("UTol").Index, dgvDimValues.RowCount - 1).Value = "NA"
        dgvDimValues(dgvDimValues.Columns("LTol").Index, dgvDimValues.RowCount - 1).Value = "NA"
        Dim Values As String = RefKey
        '"<Number>" & dgvDimValues.Rows.Count &
        '    "<Reference>" & RefKey &
        '    "<Value>" & oDim.Text &
        '    "<QTY>" & "TBD" &
        '    "<Type>" & oDim.Type &
        '    "<UTol>" & oDim.Tolerance.Upper &
        '    "<LTol>" & oDim.Tolerance.Lower
        InsertSketchedSymbolSample(oDim, oDim.RangeBox.MaxPoint, oDim.RangeBox.MinPoint, Values)
    End Sub
    Public Sub HoleTable(oDim As HoleTable, RefKey As String)
        Dim Text As String = ""
        For Each Row As HoleTableRow In oDim.HoleTableRows
            For Each Col In oDim.HoleTableColumns
                Debug.Print(Row.Item(Col).Text)
            Next
            'Text = Text & item.HoleTag & " " & item. & " " & item.DatumTwo & " " & item.DatumThree & vbNewLine
            'dgvDimValues.Rows.Add(dgvDimValues.Rows.Count, oDim.GetHashCode, Text, "TBD", oDim.Type, item.Tolerance, item.LowerTolerance)
        Next
        Dim Values As String = RefKey
        '"<Number>" & dgvDimValues.Rows.Count &
        '    "<Reference>" & RefKey &
        '    "<Value>" & oDim.Text.Text &
        '    "<QTY>" & "TBD" &
        '    "<Type>" & oDim.Type &
        '    "<UTol>" & oDim.Tolerance.Upper &
        '    "<LTol>" & oDim.Tolerance.Lower
        InsertSketchedSymbolSample(oDim, oDim.RangeBox.MaxPoint, oDim.RangeBox.MinPoint, Values)
    End Sub
    Public Sub FCF(oDim As FeatureControlFrame, RefKey As String)
        Dim Text As String = ""
        For Each item As FeatureControlFrameRow In oDim.FeatureControlFrameRows

            Text = Text & item.GeometricCharacteristic & " " & item.DatumOne & " " & item.DatumTwo & " " & item.DatumThree & vbNewLine
            dgvDimValues.Rows.Add(dgvDimValues.Rows.Count, _invApp.ReferenceKeyManager.KeyToString(RefKey), Text, "TBD", oDim.Type, item.Tolerance, item.LowerTolerance)
        Next
        Dim Values As String = RefKey
        '"<Number>" & dgvDimValues.Rows.Count &
        '    "<Reference>" & RefKey &
        '    "<Value>" & oDim.Text.Text &
        '    "<QTY>" & "TBD" &
        '    "<Type>" & oDim.Type &
        '    "<UTol>" & oDim.Tolerance.Upper &
        '    "<LTol>" & oDim.Tolerance.Lower
        InsertSketchedSymbolSample(oDim, oDim.Position, oDim.Position, Values)
    End Sub
    Public Sub HoleTable()
        Dim oDoc As DrawingDocument
        oDoc = _invApp
        Dim oHoleTable As HoleTable
        oHoleTable = oDoc.ActiveSheet.HoleTables.Item(1)
        Dim oRow As HoleTableRow
        For Each oRow In oHoleTable.HoleTableRows
            Dim subItem As HoleTableCell
            For Each subItem In oRow
                Debug.Print(subItem.Text)
            Next
        Next
    End Sub
    Private Sub InsertSketchedSymbolSample(oDim As Object, ByRef MaxP As Point2d, ByRef MinP As Point2d, ByRef Values As String) 'ByRef Text As Box2d)
        Dim oDrawDoc As DrawingDocument
        oDrawDoc = _invApp.ActiveDocument
        ' Obtain a sketched symbol definition.
        Dim oSketchedSymbolDef As SketchedSymbolDefinition = Nothing
        For i = 1 To oDrawDoc.SketchedSymbolDefinitions.Count
            If oDrawDoc.SketchedSymbolDefinitions.Item(i).Name = "mySymbol" Then
                oSketchedSymbolDef = oDrawDoc.SketchedSymbolDefinitions.Item(i)
                Exit For
            End If
        Next
        If oSketchedSymbolDef Is Nothing Then
            CreateSketchedSymbolDefinition()
            For i = 1 To oDrawDoc.SketchedSymbolDefinitions.Count
                If oDrawDoc.SketchedSymbolDefinitions.Item(i).Name = "mySymbol" Then
                    oSketchedSymbolDef = oDrawDoc.SketchedSymbolDefinitions.Item(i)
                    Exit For
                End If
            Next
        End If
        Dim oSheet As Sheet
        oSheet = oDrawDoc.ActiveSheet
        Dim sPromptStrings() As String = {dgvDimValues.RowCount, Values}
        ' Create sketched symbol
        Dim oTG As TransientGeometry
        oTG = _invApp.TransientGeometry
        Dim oPoint As Point2d = Nothing
        Select Case My.Settings.BalloonPos
            Case 0
                oPoint = oTG.CreatePoint2d(MinP.X + (MaxP.X - MinP.X) / 2, MaxP.Y + (My.Settings.BalloonSize / 100))
            Case 45
                oPoint = oTG.CreatePoint2d(MaxP.X + (My.Settings.BalloonSize / 100), MaxP.Y + (My.Settings.BalloonSize / 100))
            Case 90
                oPoint = oTG.CreatePoint2d(MaxP.X + (My.Settings.BalloonSize / 100), MinP.Y + (MaxP.Y - MinP.Y) / 2)
            Case 135
                oPoint = oTG.CreatePoint2d(MaxP.X + (My.Settings.BalloonSize / 100), MinP.Y - (My.Settings.BalloonSize / 100))
            Case 180
                oPoint = oTG.CreatePoint2d(MinP.X + (MaxP.X - MinP.X) / 2, MinP.Y - (My.Settings.BalloonSize / 100))
            Case 2100
                oPoint = oTG.CreatePoint2d(MinP.X - (My.Settings.BalloonSize / 100), MinP.Y - (My.Settings.BalloonSize / 100))
            Case 270
                oPoint = oTG.CreatePoint2d(MinP.X - (My.Settings.BalloonSize / 100), MinP.Y + (MaxP.Y - MinP.Y) / 2)
            Case 315
                oPoint = oTG.CreatePoint2d(MinP.X - (My.Settings.BalloonSize / 100), MaxP.Y + (My.Settings.BalloonSize / 100))
        End Select
        Dim oSketchedSymbol As SketchedSymbol
        Dim oGI As GeometryIntent
        oGI = oSheet.CreateGeometryIntent(oDim, oPoint)
        Dim oCol As ObjectCollection
        oCol = _invApp.TransientObjects.CreateObjectCollection
        oCol.Add(oPoint)
        oCol.Add(oGI)
        oSketchedSymbol = oSheet.SketchedSymbols.Add(oSketchedSymbolDef, oPoint, 0, 1, sPromptStrings)
        oSketchedSymbol.Leader.AddLeader(oCol)
        oSketchedSymbol.LeaderVisible = False
    End Sub
    Private Sub CreateSketchedSymbolDefinition()
        ' Set a reference to the drawing document.
        ' This assumes a drawing document is active.
        Dim oDrawDoc As DrawingDocument
        oDrawDoc = _invApp.ActiveDocument

        ' Create the new sketched symbol definition.
        Dim oSketchedSymbolDef As SketchedSymbolDefinition
        oSketchedSymbolDef = oDrawDoc.SketchedSymbolDefinitions.Add("mySymbol")

        ' Open the sketched symbol definition's sketch for edit. This is done by calling the Edit
        ' method of the SketchedSymbolDefinition to obtain a DrawingSketch. This actually creates
        ' a copy of the sketched symbol definition's and opens it for edit.
        Dim oSketch As DrawingSketch = Nothing
        Call oSketchedSymbolDef.Edit(oSketch)

        Dim oTG As TransientGeometry
        oTG = _invApp.TransientGeometry

        Dim oInsertionPoint As Point2d = (oTG.CreatePoint2d(0, 0))
        Dim oSketchCircle As SketchCircle
        oSketchCircle = oSketch.SketchCircles.AddByCenterRadius(oTG.CreatePoint2d(0, 0), (My.Settings.BalloonSize / 75))
        Dim oSheetSettings As SheetSettings = oDrawDoc.SheetSettings
        Dim oColor As Color = _invApp.TransientObjects.CreateColor(oSheetSettings.SheetColor.Red, oSheetSettings.SheetColor.Green, oSheetSettings.SheetColor.Blue, 0)
        ' Add a prompted text field at the center of the sketch circle.
        Dim sText As String
        sText = "<StyleOverride FontSize = '" & My.Settings.BalloonSize / 50 & "'>" & "<Prompt>Number</Prompt>" & "</StyleOverride>"
        Dim oTextBox As TextBox
        Dim ValueText As String = "<StyleOverride FontSize = '" & 0.0001 & "'>" & "<Prompt>Value</Prompt>" & "</StyleOverride>"

        oTextBox = oSketch.TextBoxes.AddFitted(oTG.CreatePoint2d(0, 0), sText)
        oTextBox.VerticalJustification = VerticalTextAlignmentEnum.kAlignTextMiddle
        oTextBox.HorizontalJustification = HorizontalTextAlignmentEnum.kAlignTextCenter
        oTextBox = oSketch.TextBoxes.AddFitted(oTG.CreatePoint2d(0, 0), ValueText)
        oTextBox.Color = oColor
        ' oTextBox.FormattedText = "<StyleOverride FontSize = '0.08'></StyleOverride>"
        Call oSketchedSymbolDef.ExitEdit(True)
    End Sub
End Class