
Imports System.Runtime.InteropServices
Imports Inspection.Inspection
Imports Inventor
Imports System.Linq
Imports System.IO
Imports System.Data
Imports Microsoft.Office.Interop
Imports System.Windows.Forms
Imports System.Collections.Generic

Public Class Value_Table_SA
    Dim _invApp As Inventor.Application
    Dim ListDim() As String = {"Linear", "Angular", "Diametral", "Radial"}
    Dim ListTable() As String = {"Hole Table", "Feature Control Frame"}
    Dim ListNote() As String = {"Note", "Hole Callout", "Chamfer"}
    Dim ListOther() As String = {"Weld", "Surface Roughness"}
    Dim ListLength() As String = {"Inch", "Foot", "Centimeter", "Millimeter"}
    Dim ListAngle() As String = {"Degrees", "Radians"}
    Dim StandardAddinServer As StandardAddInServer
    Private CharacteristicsForm As Settings
    Private WithEvents oSelect As SelectEvents

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        _invApp = Marshal.GetActiveObject("Inventor.Application")
        AddHandler dgvDimValues.CellValueChanged, AddressOf Me.dgvDimValues_CellValueChanged
        AddHandler dgvDimValues.CurrentCellDirtyStateChanged, AddressOf Me.dgvDimValues_CurrentCellDirtyStateChanged
        ' Add any initialization after the InitializeComponent() call.
        Refresh()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim oDoc As DrawingDocument = _invApp.ActiveDocument
        Dim oInteraction As InteractionEvents = _invApp.CommandManager.CreateInteractionEvents
        oInteraction.StatusBarText = "Select a dimension"
        oSelect = oInteraction.SelectEvents
        oSelect.AddSelectionFilter(SelectionFilterEnum.kDrawingDimensionFilter)
        oSelect.SingleSelectEnabled = True
        oInteraction.Start()

        Dim oSheetSettings As SheetSettings = oDoc.SheetSettings
        Dim SelectedFeature = _invApp.CommandManager.Pick(SelectionFilterEnum.kAllEntitiesFilter, "Select something")
        If SelectedFeature Is Nothing Then Exit Sub
        Dim RefKeyValue() As Byte = New Byte() {}
        Call SelectedFeature.GetReferenceKey(RefKeyValue)
        Dim RefKey As String = _invApp.ActiveDocument.ReferenceKeyManager.KeyToString(RefKeyValue)


        Dim Dup As Boolean = False
        For Each row In dgvDimValues.Rows
            If dgvDimValues(dgvDimValues.Columns("Ref").Index, row.index).Value = RefKey Then
                Dup = True
                Exit For
            End If
        Next
        If Dup = True Then
            MsgBox("Balloon already exists")
        Else
            DimType(SelectedFeature, RefKey)
            Dim oDim As GeneralDimension = SelectedFeature
            InsertSketchedSymbolSample(oDim, oDim.Text.RangeBox.MaxPoint, oDim.Text.RangeBox.MinPoint, RefKey)

        End If
    End Sub
    Private Sub DimType(SelectedFeature As Object, RefKey As String)
        Dim oType As String
        Select Case SelectedFeature.type
            'Linear Diametral Radial
            Case 117474560
                oType = "Linear"
                LinearDim(SelectedFeature, RefKey, "Linear")
            Case 117475328
                oType = "Diameter"
                LinearDim(SelectedFeature, RefKey, "Diameter")
            Case 117475072
                oType = "Radial"
                LinearDim(SelectedFeature, RefKey, "Radial")
            Case 117474816
                oType = "Angular"
                LinearDim(SelectedFeature, RefKey, "Angular")
            Case 117483008
                FCF(SelectedFeature, RefKey)
            Case 117471488
                HoleTableTag(SelectedFeature, RefKey)
            Case 117491712
                HoleTag(SelectedFeature, RefKey)
            Case 117473024
                Note(SelectedFeature, RefKey)
            Case 117484032
                Surface(SelectedFeature, RefKey)
            Case 117488384
                Chamfer(SelectedFeature, RefKey)
            Case 117469952
                HoleTable(SelectedFeature, RefKey)
            Case Else
                MsgBox("Unknown")

        End Select

    End Sub
    Public Sub LinearDim(oDim As GeneralDimension, RefKey As String, oType As String)
        Dim Value, Prefix, Tag As String
        Prefix = ""
        Tag = ""
        Dim uTol, lTol As Decimal
        If oType = "Angular" Then
            Value = FormatNumber(_invApp.ActiveDocument.UnitsOfMeasure.ConvertUnits(oDim.ModelValue, UnitsTypeEnum.kDatabaseAngleUnits, UnitsTypeEnum.kDefaultDisplayAngleUnits), oDim.Precision)
            Tag = Chr(176)
        Else
            Value = FormatNumber(_invApp.ActiveDocument.UnitsOfMeasure.ConvertUnits(oDim.ModelValue, UnitsTypeEnum.kDatabaseLengthUnits, UnitsTypeEnum.kDefaultDisplayLengthUnits), oDim.Precision)
            Select Case Strings.Left(oDim.Text.Text, 1)
                Case "n"
                    Prefix = Chr(216)
                Case "R"
                    Prefix = "R"
            End Select
        End If
        If oDim.Tolerance.ToleranceType = ToleranceTypeEnum.kMaxTolerance OrElse oDim.Tolerance.ToleranceType = ToleranceTypeEnum.kMinTolerance Then
            If oDim.Tolerance.ToleranceType = ToleranceTypeEnum.kMaxTolerance Then
                lTol = uTol + lTol
                uTol = 0
            Else
                uTol = uTol + lTol
                lTol = 0
            End If
        End If
        If oDim.Tolerance.Upper = 0 AndAlso oDim.Tolerance.ToleranceType = ToleranceTypeEnum.kDefaultTolerance Then
            'Return deafault upper tol
            uTol = 0.005
        Else
            uTol = oDim.Tolerance.Upper
        End If
        If oDim.Tolerance.Lower = 0 Then
            'Return deafault upper tol
            lTol = -0.005
        Else
            lTol = oDim.Tolerance.Lower
        End If
        Dim QTY As Integer = 5
        dgvDimValues.Rows.Add()
        dgvDimValues(dgvDimValues.Columns("Balloon").Index, dgvDimValues.RowCount - 1).Value = dgvDimValues.RowCount
        dgvDimValues(dgvDimValues.Columns("Ref").Index, dgvDimValues.RowCount - 1).Value = RefKey
        dgvDimValues(dgvDimValues.Columns("Value").Index, dgvDimValues.RowCount - 1).Value = Prefix & Value & Tag
        dgvDimValues(dgvDimValues.Columns("Qty").Index, dgvDimValues.RowCount - 1).Value = QTY
        Dim Skip As Boolean = False
        If QTY > 1 Then
            For Each control In Me.Controls
                If Not control.name.contains("QTY") AndAlso control.top = dgvDimValues.Top + 1 + dgvDimValues.GetCellDisplayRectangle(dgvDimValues.Columns("Qty").Index, dgvDimValues.RowCount - 1, dgvDimValues.RowCount - 1).Top Then
                    Skip = True
                    Exit For
                End If
            Next
            If Skip = False Then
                Dim pb As New PictureBox
                pb.Parent = dgvDimValues
                pb.Width = dgvDimValues.GetCellDisplayRectangle(dgvDimValues.Columns("Qty").Index, dgvDimValues.RowCount - 1, dgvDimValues.RowCount - 1).Height - 2
                pb.Height = dgvDimValues.GetCellDisplayRectangle(dgvDimValues.Columns("Qty").Index, dgvDimValues.RowCount - 1, dgvDimValues.RowCount - 1).Height - 2
                pb.Name = "Unlink"
                pb.Image = My.Resources.unlink
                pb.Top = dgvDimValues.Top + 1 + dgvDimValues.GetCellDisplayRectangle(dgvDimValues.Columns("Qty").Index, dgvDimValues.RowCount - 1, dgvDimValues.RowCount - 1).Top
                pb.Left = dgvDimValues.GetCellDisplayRectangle(dgvDimValues.Columns("Qty").Index, dgvDimValues.RowCount - 1, dgvDimValues.RowCount - 1).Right - pb.Width / 2 - 1
                pb.SizeMode = PictureBoxSizeMode.Zoom
                pb.BackColor = System.Drawing.Color.White
                pb.Tag = dgvDimValues(dgvDimValues.Columns("Ref").Index, dgvDimValues.RowCount - 1).Value
                AddHandler pb.Click, AddressOf Me.ExpandQuantity
                Me.Controls.Add(pb)
                pb.BringToFront()
            End If
        End If
            dgvDimValues(dgvDimValues.Columns("Type").Index, dgvDimValues.RowCount - 1).Value = "Dimension"
        dgvDimValues(dgvDimValues.Columns("SubType").Index, dgvDimValues.RowCount - 1).Value = oType
        dgvDimValues(dgvDimValues.Columns("UTol").Index, dgvDimValues.RowCount - 1).Value = uTol
        dgvDimValues(dgvDimValues.Columns("LTol").Index, dgvDimValues.RowCount - 1).Value = lTol
        dgvDimValues(dgvDimValues.Columns("ULimit").Index, dgvDimValues.RowCount - 1).Value = Value + uTol
        dgvDimValues(dgvDimValues.Columns("LLimit").Index, dgvDimValues.RowCount - 1).Value = Value + lTol
        If oDim.Tolerance.ToleranceType = ToleranceTypeEnum.kLimitsFitsLinearTolerance Or
            oDim.Tolerance.ToleranceType = ToleranceTypeEnum.kLimitsFitsShowSizeTolerance Or
            oDim.Tolerance.ToleranceType = ToleranceTypeEnum.kLimitsFitsShowTolerance Or
            oDim.Tolerance.ToleranceType = ToleranceTypeEnum.kLimitsFitsStackedTolerance Then
            If oDim.Tolerance.ShaftTolerance <> "" AndAlso oDim.Tolerance.HoleTolerance <> "" Then
                dgvDimValues(dgvDimValues.Columns("FitGrade").Index, dgvDimValues.RowCount - 1).Value = oDim.Tolerance.HoleTolerance & "/" & oDim.Tolerance.ShaftTolerance
                dgvDimValues(dgvDimValues.Columns("UTol").Index, dgvDimValues.RowCount - 1).Value = "NA"
                dgvDimValues(dgvDimValues.Columns("LTol").Index, dgvDimValues.RowCount - 1).Value = "NA"
                dgvDimValues(dgvDimValues.Columns("ULimit").Index, dgvDimValues.RowCount - 1).Value = "NA"
                dgvDimValues(dgvDimValues.Columns("LLimit").Index, dgvDimValues.RowCount - 1).Value = "NA"
            ElseIf oDim.Tolerance.ShaftTolerance <> "" Then
                dgvDimValues(dgvDimValues.Columns("FitGrade").Index, dgvDimValues.RowCount - 1).Value = oDim.Tolerance.ShaftTolerance
            ElseIf oDim.Tolerance.HoleTolerance <> "" Then
                dgvDimValues(dgvDimValues.Columns("FitGrade").Index, dgvDimValues.RowCount - 1).Value = oDim.Tolerance.HoleTolerance
            End If
        End If

    End Sub
    Private Sub ExpandQuantity()
        Dim ScreenLoc As Drawing.Point = dgvDimValues.PointToScreen(dgvDimValues.Location)
        Dim XLoc As Integer = DataGridView.MousePosition.X - ScreenLoc.X + dgvDimValues.Left
        Dim yLoc As Integer = DataGridView.MousePosition.Y - ScreenLoc.Y + dgvDimValues.Top
        Dim Hit As DataGridView.HitTestInfo = dgvDimValues.HitTest(XLoc, yLoc)
        Dim CurrRow As Integer = Hit.RowIndex
        Dim CurrCol As Integer = Hit.ColumnIndex
        Dim LinkButton As PictureBox
        For Each LinkButton In Me.Controls
            If LinkButton.Tag = dgvDimValues(dgvDimValues.Columns("Ref").Index, CurrRow).Value Then
                If LinkButton.Name = "Unlink" Then
                    LinkButton.Image = My.Resources.link
                Else
                    LinkButton.Image = My.Resources.unlink
                End If

                If LinkButton.Name = "Unlink" Then
                    LinkButton.Name = "Link"
                    Dim Qty As Integer = dgvDimValues(dgvDimValues.Columns("Qty").Index, CurrRow).Value
                    dgvDimValues(dgvDimValues.Columns("Qty").Index, CurrRow).Value = 1
                    For X = 1 To Qty - 1
                        dgvDimValues.Rows.AddCopy(CurrRow)
                        For I As Integer = 0 To dgvDimValues.ColumnCount - 1
                            dgvDimValues.Rows.Item(CurrRow + X).Cells(I).Value = dgvDimValues.Rows.Item(CurrRow).Cells(I).Value
                        Next
                        dgvDimValues(dgvDimValues.Columns("Balloon").Index, CurrRow + X).Value = dgvDimValues(dgvDimValues.Columns("Balloon").Index, CurrRow).Value & "." & X + 1
                    Next
                    dgvDimValues(dgvDimValues.Columns("Balloon").Index, CurrRow).Value = dgvDimValues(dgvDimValues.Columns("Balloon").Index, CurrRow).Value & ".1"
                Else
                    LinkButton.Name = "Unlink"
                    Dim BalNum As Integer = dgvDimValues(dgvDimValues.Columns("Balloon").Index, CurrRow).Value
                    dgvDimValues(dgvDimValues.Columns("Balloon").Index, CurrRow).Value = BalNum
                    For Row = CurrRow + 1 To dgvDimValues.RowCount - 1

                        If InStr(dgvDimValues(dgvDimValues.Columns("Balloon").Index, Row).Value, BalNum & ".") <> 0 Then
                            dgvDimValues(dgvDimValues.Columns("QTY").Index, CurrRow).Value = dgvDimValues(dgvDimValues.Columns("QTY").Index, CurrRow).Value + 1
                        Else
                            Exit For
                        End If

                    Next
                    For Row = CurrRow + dgvDimValues(dgvDimValues.Columns("QTY").Index, CurrRow).Value - 1 To CurrRow + 1 Step -1
                        dgvDimValues.Rows.RemoveAt(Row)
                    Next
                End If
            End If
            Exit For
        Next
    End Sub
    Sub parameterInfo()

        Dim oDoc As PartDocument

        oDoc = _invApp.ActiveDocument

        Dim para As Parameter

        Dim UOM As UnitsOfMeasure

        UOM = oDoc.UnitsOfMeasure

        For Each para In oDoc.ComponentDefinition.Parameters

            Debug.Print("Expression is " + para.Expression)

            Debug.Print("Display unit is " + para.Units)

            Debug.Print("ModelVaule is " + para.ModelValue.ToString)

            Debug.Print("Value is " + para.Value.ToString)

            Dim displayValue As String
            displayValue = UOM.ConvertUnits(para.Value, UOM.GetTypeFromString(UOM.GetDatabaseUnitsFromExpression(para.Expression, para.Units)), para.Units)
            Debug.Print("Display value is " + displayValue)

        Next

    End Sub
    Public Sub Note(oDim As DrawingNote, RefKey As String)
        dgvDimValues.Rows.Add()
        dgvDimValues(dgvDimValues.Columns("Balloon").Index, dgvDimValues.RowCount - 1).Value = dgvDimValues.RowCount
        dgvDimValues(dgvDimValues.Columns("Ref").Index, dgvDimValues.RowCount - 1).Value = RefKey
        dgvDimValues(dgvDimValues.Columns("Value").Index, dgvDimValues.RowCount - 1).Value = oDim.Text
        dgvDimValues(dgvDimValues.Columns("Qty").Index, dgvDimValues.RowCount - 1).Value = "TBD"
        dgvDimValues(dgvDimValues.Columns("Type").Index, dgvDimValues.RowCount - 1).Value = oDim.Type
        Dim Values As String = RefKey
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
        InsertSketchedSymbolSample(oDim, oDim.RangeBox.MaxPoint, oDim.RangeBox.MinPoint, Values)
    End Sub
    Public Sub FCF(oDim As FeatureControlFrame, RefKey As String)
        Dim Text As String = ""
        For Each item As FeatureControlFrameRow In oDim.FeatureControlFrameRows

            Text = Text & item.GeometricCharacteristic & " " & item.DatumOne & " " & item.DatumTwo & " " & item.DatumThree & vbNewLine
            dgvDimValues.Rows.Add(dgvDimValues.Rows.Count, _invApp.ReferenceKeyManager.KeyToString(RefKey), Text, "TBD", oDim.Type, item.Tolerance, item.LowerTolerance)
        Next
        Dim Values As String = RefKey
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
            If oDrawDoc.SketchedSymbolDefinitions.Item(i).Name = "Insp" Then
                oSketchedSymbolDef = oDrawDoc.SketchedSymbolDefinitions.Item(i)
                Exit For
            End If
        Next
        If oSketchedSymbolDef Is Nothing Then
            CreateSketchedSymbolDefinition()
            For i = 1 To oDrawDoc.SketchedSymbolDefinitions.Count
                If oDrawDoc.SketchedSymbolDefinitions.Item(i).Name = "Insp" Then
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
        oSketchedSymbolDef = oDrawDoc.SketchedSymbolDefinitions.Add("Insp")

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
        sText = "<StyleOverride FontSize='" & My.Settings.BalloonSize / 50 & "'>" & "<Prompt>Number</Prompt>" & "</StyleOverride>"
        Dim oTextBox As Inventor.TextBox
        Dim ValueText As String = "<StyleOverride FontSize='" & 0.0001 & "'>" & "<Prompt>Value</Prompt>" & "</StyleOverride>"

        oTextBox = oSketch.TextBoxes.AddFitted(oTG.CreatePoint2d(0, 0), sText)
        oTextBox.VerticalJustification = VerticalTextAlignmentEnum.kAlignTextMiddle
        oTextBox.HorizontalJustification = HorizontalTextAlignmentEnum.kAlignTextCenter
        oTextBox = oSketch.TextBoxes.AddFitted(oTG.CreatePoint2d(0, 0), ValueText)
        oTextBox.Color = oColor
        ' oTextBox.FormattedText = "<StyleOverride FontSize='0.08'></StyleOverride>"
        Call oSketchedSymbolDef.ExitEdit(True)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Refresh()
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'WritePrivate()
        Dim WriteString(dgvDimValues.RowCount, dgvDimValues.ColumnCount - 1) As String
        For column = 0 To dgvDimValues.ColumnCount - 1
            WriteString(0, column) = dgvDimValues.Columns(column).HeaderText
        Next
        For i = 0 To dgvDimValues.RowCount - 1
            For j = 0 To dgvDimValues.ColumnCount - 1
                WriteString(i + 1, j) = dgvDimValues(j, i).Value
            Next
        Next
        Dim sw As System.IO.StreamWriter = New System.IO.StreamWriter(IO.Path.Combine(IO.Path.GetTempPath, "InspTable.csv"))
        Dim str As String = ""
        For i As Int32 = WriteString.GetLowerBound(0) To WriteString.GetUpperBound(0)
            For j As Int32 = WriteString.GetLowerBound(1) To WriteString.GetUpperBound(1)
                str += WriteString(i, j) + ","
            Next
            sw.WriteLine(str)
            str = ""
        Next
        sw.Flush()
        sw.Close()

        Dim oleRef As ReferencedOLEFileDescriptor
        For Each oleRef In _invApp.ActiveDocument.ReferencedOLEFileDescriptors
            If oleRef.DisplayName = "InspTable" Then
                oleRef.Delete()
                Exit For
            End If
        Next
        oleRef = _invApp.ActiveDocument.ReferencedOLEFileDescriptors.Add(IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "InspTable.csv"), OLEDocumentTypeEnum.kOLEDocumentEmbeddingObject)
        oleRef.DisplayName = "InspTable"
        'oleRef.BrowserVisible = False
        'oleRef.Visible = False
        Kill(IO.Path.Combine(IO.Path.GetTempPath, "InspTable.csv"))
    End Sub
    Public Overrides Sub Refresh()
        dgvDimValues.Rows.Clear()
        For Each control In Me.Controls
            If control.name.contains("QTY") Then
                Me.Controls.Remove(control)
                control.dispose
            End If
        Next
        Dim oDoc As Document = _invApp.ActiveDocument
        Dim Ref() As Byte = New Byte() {}
        Try
            For Each oSketch As SketchedSymbol In oDoc.ActiveSheet.SketchedSymbols
                If oSketch.Name = "Insp" Then
                    Dim key As String = oSketch.GetResultText(oSketch.Definition.Sketch.TextBoxes.Item(2))
                    Call oDoc.ReferenceKeyManager.StringToKey(key, Ref)
                    Dim Refobj As Object = oDoc.ReferenceKeyManager.BindKeyToObject(Ref)
                    oDoc.SelectSet.Clear()
                    Dim oDim As Object = Refobj
                    DimType(oDim, key)
                End If
            Next
        Catch
        End Try
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        dgvDimValues.Rows.Clear()
        Dim oDoc As DrawingDocument = _invApp.ActiveDocument
        For Each EmbeddedFile As ReferencedOLEFileDescriptor In oDoc.ReferencedOLEFileDescriptors2(OLEDocumentTypeEnum.kOLEDocumentEmbeddingObject)
            If EmbeddedFile.DisplayName = "InspTable" Then
                Dim oExcelWB As Excel.Workbook = Nothing
                Dim osheet As Excel.Worksheet
                EmbeddedFile.Activate(OLEVerbEnum.kHideOLEVerb, oExcelWB)
                osheet = oExcelWB.ActiveSheet
                For Y = 2 To osheet.UsedRange.Rows.Count
                    dgvDimValues.Rows.Add()
                    For X = 1 To dgvDimValues.ColumnCount
                        Select Case osheet.Cells(1, X).value
                            Case "Reference"
                                dgvDimValues(dgvDimValues.Columns("Ref").Index, dgvDimValues.RowCount - 1).Value = osheet.Cells(Y, X).Value
                            Case "Balloon"
                                dgvDimValues(dgvDimValues.Columns("Balloon").Index, dgvDimValues.RowCount - 1).Value = osheet.Cells(Y, X).value
                            Case "Value"
                                dgvDimValues(dgvDimValues.Columns("Value").Index, dgvDimValues.RowCount - 1).Value = osheet.Cells(Y, X).value
                            Case "QTY"
                                dgvDimValues(dgvDimValues.Columns("Qty").Index, dgvDimValues.RowCount - 1).Value = osheet.Cells(Y, X).value
                            Case "Type"
                                dgvDimValues(dgvDimValues.Columns("Type").Index, dgvDimValues.RowCount - 1).Value = osheet.Cells(Y, X).value
                            Case "Sub-Type"
                                dgvDimValues(dgvDimValues.Columns("SubType").Index, dgvDimValues.RowCount - 1).Value = osheet.Cells(Y, X).value
                            Case "Upper Tol"
                                dgvDimValues(dgvDimValues.Columns("UTol").Index, dgvDimValues.RowCount - 1).Value = osheet.Cells(Y, X).value
                            Case "Lower Tol"
                                dgvDimValues(dgvDimValues.Columns("LTol").Index, dgvDimValues.RowCount - 1).Value = osheet.Cells(Y, X).value
                            Case "Upper Limit"
                                dgvDimValues(dgvDimValues.Columns("ULimit").Index, dgvDimValues.RowCount - 1).Value = osheet.Cells(Y, X).value
                            Case "Lower Limit"
                                dgvDimValues(dgvDimValues.Columns("LLimit").Index, dgvDimValues.RowCount - 1).Value = osheet.Cells(Y, X).value
                            Case "Fit Grade"
                                dgvDimValues(dgvDimValues.Columns("FitGrade").Index, dgvDimValues.RowCount - 1).Value = osheet.Cells(Y, X).value
                            Case "Comments"
                                dgvDimValues(dgvDimValues.Columns("Comments").Index, dgvDimValues.RowCount - 1).Value = osheet.Cells(Y, X).value
                        End Select
                    Next
                Next
            End If
        Next
    End Sub
    Private Sub dgvDimValues_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Me.dgvDimValues.IsCurrentCellDirty Then
            ' This fires the cell value changed handler below
            dgvDimValues.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If

    End Sub

    Private Sub dgvDimValues_CellValueChanged(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs)
        Dim cb As DataGridViewComboBoxCell
        If dgvDimValues.Columns(e.ColumnIndex).HeaderText = "Type" Then
            cb = CType(dgvDimValues.Rows(e.RowIndex).Cells(e.ColumnIndex), DataGridViewComboBoxCell)
            Dim SubTypeCell As New DataGridViewComboBoxCell
            SubTypeCell = dgvDimValues.Rows(e.RowIndex).Cells(dgvDimValues.Columns("SubType").Index)
            SubTypeCell.Items.Clear()
            Select Case cb.Value
                Case "Dimension"
                    SubTypeCell.Items.AddRange(ListDim)
                    SubTypeCell.Value = "Diametral"
                Case "Geometric Tol"
                    SubTypeCell.Items.AddRange(ListTable)
                    SubTypeCell.Value = "Hole Table"
                Case "Note"
                    SubTypeCell.Items.AddRange(ListNote)
                    SubTypeCell.Value = "Note"
                Case "Other"
                    SubTypeCell.Items.AddRange(ListOther)
                    SubTypeCell.Value = "Weld"
                Case Nothing

                Case Else
            End Select
        ElseIf dgvDimValues.Columns(e.ColumnIndex).HeaderText = "Sub-Type" Then
            cb = CType(dgvDimValues.Rows(e.RowIndex).Cells(e.ColumnIndex), DataGridViewComboBoxCell)
            Dim UnitCell As New DataGridViewComboBoxCell
            UnitCell = dgvDimValues.Rows(e.RowIndex).Cells(dgvDimValues.Columns("Units").Index)
            UnitCell.Items.Clear()
            Select Case cb.Value
                Case "Angular"
                    UnitCell.Items.AddRange(ListAngle)
                    UnitCell.Value = "Degrees"
                Case Else
                    UnitCell.Items.AddRange(ListLength)
                    UnitCell.Value = "Inch"
            End Select

        End If
    End Sub

    Private Sub Value_Table_SA_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'TolerancesDataSet._Default_Precision_Angular' table. You can move, or remove it, as needed.
        Me.Default_Precision_AngularTableAdapter.Fill(Me.TolerancesDataSet._Default_Precision_Angular)
        'TODO: This line of code loads data into the 'TolerancesDataSet._Default_Precision_Linear' table. You can move, or remove it, as needed.
        Me.Default_Precision_LinearTableAdapter.Fill(Me.TolerancesDataSet._Default_Precision_Linear)

    End Sub
End Class