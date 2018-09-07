
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
    Dim ListLength() As String = {"Inch", "Foot", "Yard", "Mile", "Micron", "Millimeter", "Centimeter", "Meter"}
    Dim ListAngle() As String = {"Degrees", "Radians"}
    Dim StandardAddinServer As StandardAddInServer
    Private Settings As Settings
    Private WithEvents oSelect As SelectEvents
    Dim CurrRow As Integer
    Dim BalVal As String

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
        AddBalloon(dgvDimValues.RowCount, False)
    End Sub
    Private Sub AddBalloon(ByVal CurrRow As Integer, ByVal Insert As Boolean)
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
            Dim oDim As GeneralDimension = SelectedFeature
            InsertSketchedSymbolSample(oDim, oDim.Text.RangeBox.MaxPoint, oDim.Text.RangeBox.MinPoint, RefKey, CurrRow + 1)
            DimType(SelectedFeature, RefKey, Insert, CurrRow + 1)
        End If
    End Sub
    Private Sub DimType(SelectedFeature As Object, RefKey As String, Insert As Boolean, Balloon As Decimal)
        Dim oType As String
        Select Case SelectedFeature.type
            'Linear Diametral Radial
            Case 117474560
                oType = "Linear"
                LinearDim(SelectedFeature, RefKey, "Linear", Insert, Balloon)
            Case 117475328
                oType = "Diameter"
                LinearDim(SelectedFeature, RefKey, "Diametral", Insert, Balloon)
            Case 117475072
                oType = "Radial"
                LinearDim(SelectedFeature, RefKey, "Radial", Insert, Balloon)
            Case 117474816
                oType = "Angular"
                LinearDim(SelectedFeature, RefKey, "Angular", Insert, Balloon)
            Case 117483008
                FCF(SelectedFeature, RefKey)
            Case 117471488
                HoleTableTag(SelectedFeature, RefKey)
            Case 117491712
                ThreadNote(SelectedFeature, RefKey)
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
    Public Sub LinearDim(oDim As GeneralDimension, RefKey As String, oType As String, Insert As Boolean, Balloon As Integer)
        Dim Value, Prefix, Tag As String
        Prefix = ""
        Tag = ""
        Dim linuTol, linlTol, anglTol, anguTol As Decimal
        Dim StringValue As String = ""
        dgvDimValues.Rows.Insert(CurrRow)
        Dim Units As String = "Centimeter"
        Dim TolMod As Decimal = 1
        If oType = "Angular" Then
            Units = "Degrees"
            Value = FormatNumber(_invApp.ActiveDocument.UnitsOfMeasure.ConvertUnits(oDim.ModelValue, UnitsTypeEnum.kRadianAngleUnits, UnitsTypeEnum.kDefaultDisplayAngleUnits), 8)
            If oDim.Style.AngularFormatIsDecimalDegrees = True Then
                StringValue = Value
                Tag = Chr(176)
            Else
                StringValue = Value
            End If
            TolMod = Math.PI / 180
        Else
            Value = FormatNumber(_invApp.ActiveDocument.UnitsOfMeasure.ConvertUnits(oDim.ModelValue, UnitsTypeEnum.kDefaultDisplayLengthUnits, UnitsTypeEnum.kDefaultDisplayLengthUnits), 8)
            StringValue = Value
            Select Case oDim.Style.LinearUnits
                Case UnitsTypeEnum.kMillimeterLengthUnits
                    Units = "Millimeter"
                    StringValue = Math.Round(Value * 10, oDim.Precision)
                    TolMod = 0.1
                    Value = StringValue
                Case UnitsTypeEnum.kCentimeterLengthUnits
                    Units = "Centimeter"
                    StringValue = Math.Round(Value * 1, oDim.Precision)
                Case UnitsTypeEnum.kMeterLengthUnits
                    Units = "Meter"
                    StringValue = Math.Round(Value / 100, oDim.Precision)
                    TolMod = 100
                    Value = StringValue
                Case UnitsTypeEnum.kMicronLengthUnits
                    Units = "Micron"
                    TolMod = 0.0001
                    StringValue = Math.Round(Value * 10000, oDim.Precision)
                    Value = StringValue
                Case UnitsTypeEnum.kInchLengthUnits
                    Units = "Inch"
                    TolMod = 2.54
                    Value = Math.Round(Value / 2.54, oDim.Precision)
                    If Not oDim.Style.DisplayFormat = DisplayFormatEnum.kDecimalFormat Then
                        StringValue = GetFraction(Value, oDim.Style.LinearPrecision)
                    Else
                        StringValue = FormatNumber(Value, oDim.Precision)
                    End If
                Case UnitsTypeEnum.kFootLengthUnits
                    Units = "Foot"
                    StringValue = Math.Round(Value / 2.54 / 12, oDim.Precision)
                    TolMod = 2.54 * 12
                    Value = StringValue
                Case UnitsTypeEnum.kYardLengthUnits
                    Units = "Yard"
                    StringValue = Math.Round(Value / 2.54 / 12 / 3, oDim.Precision)
                    TolMod = 2.54 * 12 * 3
                    Value = StringValue
                Case UnitsTypeEnum.kMileLengthUnits
                    Units = "Mile"
                    TolMod = 2.54 * 12 * 5280
                    StringValue = Math.Round(Value / 2.54 / 12 / 5280, oDim.Precision)
                    Value = StringValue
            End Select
            Select Case oType
                Case "Diametral"
                    Prefix = Chr(216)
                Case "Radial"
                    Prefix = "R"
            End Select
        End If
        If oDim.Style.DisplayFormat = DisplayFormatEnum.kDecimalFormat Then
            Select Case oDim.Tolerance.ToleranceType
                Case ToleranceTypeEnum.kMaxTolerance
                    anglTol = Math.Abs(My.Settings("AngN" & oDim.Precision)) + My.Settings("AngP" & oDim.Precision)
                    linlTol = Math.Abs(My.Settings("LinN" & oDim.Precision)) + My.Settings("LinP" & oDim.Precision)
                    linuTol = 0
                Case ToleranceTypeEnum.kMinTolerance
                    linuTol = My.Settings("LinP" & oDim.Precision) + Math.Abs(My.Settings("LinN" & oDim.Precision))
                    linlTol = 0
                Case ToleranceTypeEnum.kSymmetricTolerance
                    anglTol = oDim.Tolerance.Upper / TolMod
                    anglTol = anglTol * -1
                    linlTol = oDim.Tolerance.Upper / TolMod
                    linuTol = linlTol * -1
                Case ToleranceTypeEnum.kDefaultTolerance, ToleranceTypeEnum.kBasicTolerance
                    If oDim.Tolerance.Upper = 0 Then
                        linuTol = My.Settings("LinP" & oDim.Precision)
                    Else
                        linuTol = oDim.Tolerance.Upper / TolMod
                    End If
                    If oDim.Tolerance.Lower = 0 Then
                        linlTol = My.Settings("LinN" & oDim.Precision)
                    Else
                        linlTol = oDim.Tolerance.Lower / TolMod
                    End If
                Case ToleranceTypeEnum.kDeviationTolerance,
                     ToleranceTypeEnum.kLimitLinearTolerance,
                     ToleranceTypeEnum.kLimitsStackedTolerance,
                     ToleranceTypeEnum.kLimitsFitsShowTolerance,
                     ToleranceTypeEnum.kLimitsFitsShowSizeTolerance,
                     ToleranceTypeEnum.kLimitsFitsLinearTolerance,
                     ToleranceTypeEnum.kLimitsFitsStackedTolerance
                    linuTol = oDim.Tolerance.Upper / TolMod
                    linlTol = oDim.Tolerance.Lower / TolMod
            End Select
        Else
            linuTol = My.Settings.LinP0
            linlTol = My.Settings.LinN0
        End If

        dgvDimValues(dgvDimValues.Columns("Balloon").Index, CurrRow).Value = Balloon + 1
        dgvDimValues(dgvDimValues.Columns("Ref").Index, CurrRow).Value = RefKey

        dgvDimValues(dgvDimValues.Columns("Qty").Index, CurrRow).Value = 1
        dgvDimValues(dgvDimValues.Columns("Type").Index, CurrRow).Value = "Dimension"
        dgvDimValues(dgvDimValues.Columns("SubType").Index, CurrRow).Value = oType
        dgvDimValues(dgvDimValues.Columns("Units").Index, CurrRow).Value = Units
        If oType = "Angular" Then
            If oDim.Style.AngularFormatIsDecimalDegrees = False Then
                dgvDimValues(dgvDimValues.Columns("Value").Index, CurrRow).Value = Prefix & ReturnDegreesMinutesSecondsFromDecimalDegrees(StringValue, oDim.Style.AngularPrecision) & Tag
                dgvDimValues(dgvDimValues.Columns("LTol").Index, CurrRow).Value = ReturnDegreesMinutesSecondsFromDecimalDegrees(linuTol, oDim.Style.ToleranceAngularPrecision)
                dgvDimValues(dgvDimValues.Columns("UTol").Index, CurrRow).Value = ReturnDegreesMinutesSecondsFromDecimalDegrees(linlTol, oDim.Style.ToleranceAngularPrecision)
                dgvDimValues(dgvDimValues.Columns("ULimit").Index, CurrRow).Value = ReturnDegreesMinutesSecondsFromDecimalDegrees(Value + linuTol, oDim.Style.AngularPrecision)
                dgvDimValues(dgvDimValues.Columns("LLimit").Index, CurrRow).Value = ReturnDegreesMinutesSecondsFromDecimalDegrees(Value + linlTol, oDim.Style.AngularPrecision)
            Else
                dgvDimValues(dgvDimValues.Columns("Value").Index, CurrRow).Value = Prefix & FormatNumber(StringValue, oDim.Precision) & Tag
                dgvDimValues(dgvDimValues.Columns("UTol").Index, CurrRow).Value = FormatNumber(linuTol, oDim.TolerancePrecision)
                dgvDimValues(dgvDimValues.Columns("LTol").Index, CurrRow).Value = FormatNumber(linlTol, oDim.TolerancePrecision)
                dgvDimValues(dgvDimValues.Columns("ULimit").Index, CurrRow).Value = FormatNumber(Value + linuTol, oDim.Precision)
                dgvDimValues(dgvDimValues.Columns("LLimit").Index, CurrRow).Value = FormatNumber(Value + linlTol, oDim.Precision)
            End If
        Else
            dgvDimValues(dgvDimValues.Columns("Value").Index, CurrRow).Value = Prefix & FormatNumber(StringValue, oDim.Precision) & Tag
            dgvDimValues(dgvDimValues.Columns("UTol").Index, CurrRow).Value = FormatNumber(linuTol, oDim.TolerancePrecision)
            dgvDimValues(dgvDimValues.Columns("LTol").Index, CurrRow).Value = FormatNumber(linlTol, oDim.TolerancePrecision)
            dgvDimValues(dgvDimValues.Columns("ULimit").Index, CurrRow).Value = FormatNumber(Value + linuTol, oDim.Precision)
            dgvDimValues(dgvDimValues.Columns("LLimit").Index, CurrRow).Value = FormatNumber(Value + linlTol, oDim.Precision)
        End If
        If oDim.Tolerance.ToleranceType = ToleranceTypeEnum.kLimitsFitsLinearTolerance Or
            oDim.Tolerance.ToleranceType = ToleranceTypeEnum.kLimitsFitsShowSizeTolerance Or
            oDim.Tolerance.ToleranceType = ToleranceTypeEnum.kLimitsFitsShowTolerance Or
            oDim.Tolerance.ToleranceType = ToleranceTypeEnum.kLimitsFitsStackedTolerance Then
            If oDim.Tolerance.ShaftTolerance <> "" AndAlso oDim.Tolerance.HoleTolerance <> "" Then
                dgvDimValues(dgvDimValues.Columns("FitGrade").Index, CurrRow).Value = oDim.Tolerance.HoleTolerance & "/" & oDim.Tolerance.ShaftTolerance
                'dgvDimValues(dgvDimValues.Columns("linuTol").Index, CurrRow).Value = "NA"
                'dgvDimValues(dgvDimValues.Columns("Linltol").Index, CurrRow).Value = "NA"
                'dgvDimValues(dgvDimValues.Columns("ULimit").Index, CurrRow).Value = "NA"
                'dgvDimValues(dgvDimValues.Columns("LLimit").Index, CurrRow).Value = "NA"
            ElseIf oDim.Tolerance.ShaftTolerance <> "" Then
                dgvDimValues(dgvDimValues.Columns("FitGrade").Index, CurrRow).Value = oDim.Tolerance.ShaftTolerance
            ElseIf oDim.Tolerance.HoleTolerance <> "" Then
                dgvDimValues(dgvDimValues.Columns("FitGrade").Index, CurrRow).Value = oDim.Tolerance.HoleTolerance
            End If
        End If
        dgvDimValues(dgvDimValues.Columns("Comments").Index, CurrRow).Value = Strings.Replace(Strings.Replace(oDim.Text.FormattedText, "<DimensionValue/>", ""), "<Br/>", " " & vbCrLf & " ")
        CurrRow += 1
    End Sub
#Region "DimensionParsing"
    Function ReturnDegreesMinutesSecondsFromDecimalDegrees(ByVal DecimalDegrees As Decimal, Precision As AngularPrecisionEnum) As String
        Dim DecDegAbs As Decimal = Math.Abs(DecimalDegrees)
        Dim ReturnValue As String = "'"
        Dim DegreeSymbol As String = "°"
        Dim MinutesSymbol As String = "’"
        Dim SecondsSymbol As String = Chr(34)
        Dim Degrees As String = Math.Truncate(DecDegAbs) & DegreeSymbol
        Dim MinutesDecimal As Decimal = (DecDegAbs - Math.Truncate(DecDegAbs)) * 60
        Dim SecondsDecimal As Decimal = (MinutesDecimal - Math.Truncate(MinutesDecimal))
        Dim Minutes As String = Math.Truncate(MinutesDecimal) & MinutesSymbol
        Dim Seconds As String = String.Format("{0:##.0000}", (SecondsDecimal * 60)) & SecondsSymbol
        ReturnValue = Degrees & " " & Minutes & " " & Seconds
        Select Case Precision
            Case AngularPrecisionEnum.kDegreesAngularPrecision
                ReturnValue = Strings.Left(ReturnValue, InStr(ReturnValue, "°"))
            Case AngularPrecisionEnum.kMinutesAngularPrecision
                ReturnValue = Strings.Left(ReturnValue, InStr(ReturnValue, "'"))
            Case AngularPrecisionEnum.kSecondsAngularPrecision
                ReturnValue = Strings.Left(ReturnValue, InStr(ReturnValue, ".") - 1)
            Case AngularPrecisionEnum.kSecondsOneDecimalPlaceAngularPrecision
                ReturnValue = Strings.Left(ReturnValue, InStr(ReturnValue, ".") + 1)
            Case AngularPrecisionEnum.kSecondsTwoDecimalPlaceAngularPrecision
                ReturnValue = Strings.Left(ReturnValue, InStr(ReturnValue, ".") + 2)
            Case AngularPrecisionEnum.kSecondsThreeDecimalPlaceAngularPrecision
                ReturnValue = Strings.Left(ReturnValue, InStr(ReturnValue, ".") + 3)
            Case AngularPrecisionEnum.kSecondsFourDecimalPlaceAngularPrecision
                ReturnValue = Strings.Left(ReturnValue, InStr(ReturnValue, ".") + 4)
        End Select
        Return ReturnValue
    End Function
    Private Function GetFraction(ByVal Input As Decimal, ByVal Accuracy As LinearPrecisionEnum) As String
        Dim Whole As Integer = Math.Floor(Input)
        Dim Remainder As Decimal = Input - Whole
        Select Case Accuracy
            Case LinearPrecisionEnum.kZeroFractionalLinearPrecision
                Return Whole
            Case LinearPrecisionEnum.kHalfFractionalLinearPrecision
                Remainder = Math.Round(Remainder * 2)
                If Not Remainder = 0 Then
                    Return Whole & " " & ReduceFraction(Remainder, 2, "/")
                Else
                    Return Whole
                End If
            Case LinearPrecisionEnum.kQuarterFractionalLinearPrecision
                If Not Remainder = 0 Then
                    Remainder = Math.Round(Remainder * 4)
                    Return Whole & " " & ReduceFraction(Remainder, 4, "/")
                Else
                    Return Whole
                End If
            Case LinearPrecisionEnum.kEighthsFractionalLinearPrecision
                If Not Remainder = 0 Then
                    Remainder = Math.Round(Remainder * 8)

                    Return Whole & " " & ReduceFraction(Remainder, 8, "/")
                Else
                    Return Whole
                End If
            Case LinearPrecisionEnum.kSixteenthsFractionalLinearPrecision
                If Not Remainder = 0 Then
                    Remainder = Math.Round(Remainder * 16)
                    Return Whole & " " & ReduceFraction(Remainder, 16, "/")
                Else
                    Return Whole
                End If
            Case LinearPrecisionEnum.kThirtySecondsFractionalLinearPrecision
                If Not Remainder = 0 Then
                    Remainder = Math.Round(Remainder * 32)
                    Return Whole & " " & ReduceFraction(Remainder, 32, "/")
                Else
                    Return Whole
                End If
            Case LinearPrecisionEnum.kSixtyFourthsFractionalLinearPrecision
                If Not Remainder = 0 Then
                    Remainder = Math.Round(Remainder * 64)
                    Return Whole & " " & ReduceFraction(Remainder, 64, "/")
                Else
                    Return Whole
                End If
            Case LinearPrecisionEnum.kOneTwentyEighthsFractionalLinearPrecision
                If Not Remainder = 0 Then
                    Remainder = Math.Round(Remainder * 128)
                    Return Whole & " " & ReduceFraction(Remainder, 128, "/")
                Else
                    Return Whole
                End If
        End Select
    End Function
    Public Shared Function ReduceFraction(ByVal Numerator As Integer, ByVal Demoninator As Integer, Optional ByVal Splitter As String = "/") As String
        Return ReduceNumerator(Numerator, Demoninator) & Splitter & ReduceDenominator(Numerator, Demoninator)
    End Function
    Public Shared Function ReduceNumerator(ByVal Numerator As Integer, ByVal Denominator As Integer) As Long
        Return Numerator / Factor(Numerator, Denominator)
    End Function

    Public Shared Function ReduceDenominator(ByVal Numerator As Integer, ByVal Denominator As Integer) As Long
        Return Denominator / Factor(Numerator, Denominator)
    End Function

    Public Shared Function Factor(ByVal Numerator As Integer, ByVal Denominator As Integer) As Long

        Dim Temp As Long

        Do While Denominator > 0
            Temp = Denominator
            Denominator = Numerator Mod Denominator
            Numerator = Temp
        Loop

        Return Numerator

    End Function
#End Region
    Private Sub Renumber_Balloons(ByVal CurrRow As Integer)
        Dim oDoc As Document = _invApp.ActiveDocument
        Dim Ref() As Byte = New Byte() {}
        Try
            For Each oSketch As SketchedSymbol In oDoc.ActiveSheet.SketchedSymbols
                If oSketch.Name = "Insp" Then
                    Dim key As String = oSketch.GetResultText(oSketch.Definition.Sketch.TextBoxes.Item(2))
                    Call oDoc.ReferenceKeyManager.StringToKey(key, Ref)
                    If key = dgvDimValues(dgvDimValues.Columns("Ref").Index, CurrRow).Value Then
                        oSketch.SetPromptResultText(oSketch.Definition.Sketch.TextBoxes.Item(1), dgvDimValues(dgvDimValues.Columns("Balloon").Index, CurrRow).Value)
                        Exit For
                    End If
                End If
            Next
        Catch
        End Try
    End Sub
    Private Sub ExpandQuantity()
        If cmsDGVRBC.Items.Item(0).Text = "Unlink Balloons" Then
            Dim Qty As Integer = dgvDimValues(dgvDimValues.Columns("Qty").Index, CurrRow).Value
            dgvDimValues(dgvDimValues.Columns("Qty").Index, CurrRow).Value = 1
            dgvDimValues(dgvDimValues.Columns("Balloon").Index, CurrRow).Value += (1 / 10)
            For X = 1 To Qty - 1
                If CurrRow <> dgvDimValues.RowCount Then
                    dgvDimValues.Rows.InsertCopy(CurrRow, CurrRow + X)
                Else
                    dgvDimValues.Rows.AddCopy(CurrRow)
                End If

                For I As Integer = 0 To dgvDimValues.ColumnCount - 1
                    dgvDimValues.Rows.Item(CurrRow + X).Cells(I).Value = dgvDimValues.Rows.Item(CurrRow).Cells(I).Value
                Next
                dgvDimValues(dgvDimValues.Columns("Balloon").Index, CurrRow + X).Value += (X / 10)
            Next
        Else
            For Each row In dgvDimValues.Rows
                If dgvDimValues(dgvDimValues.Columns("Ref").Index, row.index).Value = dgvDimValues(dgvDimValues.Columns("Ref").Index, CurrRow).Value Then
                    CurrRow = row.index
                    Exit For
                End If
            Next
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
    End Sub
    Public Sub Note(oDim As DrawingNote, RefKey As String)
        dgvDimValues.Rows.Add()
        dgvDimValues(dgvDimValues.Columns("Balloon").Index, dgvDimValues.RowCount - 1).Value = dgvDimValues.RowCount
        dgvDimValues(dgvDimValues.Columns("Ref").Index, dgvDimValues.RowCount - 1).Value = RefKey
        dgvDimValues(dgvDimValues.Columns("Value").Index, dgvDimValues.RowCount - 1).Value = oDim.Text
        dgvDimValues(dgvDimValues.Columns("Qty").Index, dgvDimValues.RowCount - 1).Value = "TBD"
        dgvDimValues(dgvDimValues.Columns("Type").Index, dgvDimValues.RowCount - 1).Value = oDim.Type
        Dim Values As String = RefKey
        InsertSketchedSymbolSample(oDim, oDim.RangeBox.MaxPoint, oDim.RangeBox.MinPoint, Values, CurrRow)
    End Sub
    Public Sub HoleTableTag(oDim As HoleTag, RefKey As String)
        dgvDimValues.Rows.Add()
        dgvDimValues(dgvDimValues.Columns("Balloon").Index, dgvDimValues.RowCount - 1).Value = dgvDimValues.RowCount
        dgvDimValues(dgvDimValues.Columns("Ref").Index, dgvDimValues.RowCount - 1).Value = RefKey
        dgvDimValues(dgvDimValues.Columns("Value").Index, dgvDimValues.RowCount - 1).Value = oDim.Text
        dgvDimValues(dgvDimValues.Columns("Qty").Index, dgvDimValues.RowCount - 1).Value = "TBD"
        dgvDimValues(dgvDimValues.Columns("Type").Index, dgvDimValues.RowCount - 1).Value = oDim.Type
        Dim Values As String = RefKey
        InsertSketchedSymbolSample(oDim, oDim.RangeBox.MaxPoint, oDim.RangeBox.MinPoint, Values, CurrRow)
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
        InsertSketchedSymbolSample(oDim, oDim.Position, oDim.Position, Values, CurrRow)
    End Sub
    Public Sub HoleTag(oDim As HoleTag, RefKey As String)
        dgvDimValues.Rows.Add()
        dgvDimValues(dgvDimValues.Columns("Balloon").Index, dgvDimValues.RowCount - 1).Value = dgvDimValues.RowCount
        dgvDimValues(dgvDimValues.Columns("Ref").Index, dgvDimValues.RowCount - 1).Value = RefKey
        dgvDimValues(dgvDimValues.Columns("Value").Index, dgvDimValues.RowCount - 1).Value = oDim.Text
        dgvDimValues(dgvDimValues.Columns("Qty").Index, dgvDimValues.RowCount - 1).Value = oDim.Type.kBOMQuantityObject
        dgvDimValues(dgvDimValues.Columns("Type").Index, dgvDimValues.RowCount - 1).Value = oDim.Type
        dgvDimValues(dgvDimValues.Columns("UTol").Index, dgvDimValues.RowCount - 1).Value = oDim.Tolerance.Upper
        dgvDimValues(dgvDimValues.Columns("LTol").Index, dgvDimValues.RowCount - 1).Value = oDim.Tolerance.Lower
        Dim Values As String = RefKey
        InsertSketchedSymbolSample(oDim, oDim.RangeBox.MaxPoint, oDim.RangeBox.MinPoint, Values, CurrRow)
    End Sub
    Public Sub ThreadNote(oDim As HoleThreadNote, RefKey As String)
        dgvDimValues.Rows.Add()
        dgvDimValues(dgvDimValues.Columns("Balloon").Index, dgvDimValues.RowCount - 1).Value = dgvDimValues.RowCount
        dgvDimValues(dgvDimValues.Columns("Ref").Index, dgvDimValues.RowCount - 1).Value = RefKey
        dgvDimValues(dgvDimValues.Columns("Value").Index, dgvDimValues.RowCount - 1).Value = oDim.Text
        dgvDimValues(dgvDimValues.Columns("Qty").Index, dgvDimValues.RowCount - 1).Value = oDim.qu
        dgvDimValues(dgvDimValues.Columns("Type").Index, dgvDimValues.RowCount - 1).Value = oDim.Type
        dgvDimValues(dgvDimValues.Columns("UTol").Index, dgvDimValues.RowCount - 1).Value = oDim.Tolerance.Upper
        dgvDimValues(dgvDimValues.Columns("LTol").Index, dgvDimValues.RowCount - 1).Value = oDim.Tolerance.Lower
        Dim Values As String = RefKey
        InsertSketchedSymbolSample(oDim, oDim.RangeBox.MaxPoint, oDim.RangeBox.MinPoint, Values, CurrRow)
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
        InsertSketchedSymbolSample(oDim, oDim.RangeBox.MaxPoint, oDim.RangeBox.MinPoint, Values, CurrRow)
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
        InsertSketchedSymbolSample(oDim, oDim.RangeBox.MaxPoint, oDim.RangeBox.MinPoint, Values, CurrRow)
    End Sub
    Public Sub FCF(oDim As FeatureControlFrame, RefKey As String)
        Dim Text As String = ""
        For Each item As FeatureControlFrameRow In oDim.FeatureControlFrameRows

            Text = Text & item.GeometricCharacteristic & " " & item.DatumOne & " " & item.DatumTwo & " " & item.DatumThree & vbNewLine
            dgvDimValues.Rows.Add(dgvDimValues.Rows.Count, _invApp.ReferenceKeyManager.KeyToString(RefKey), Text, "TBD", oDim.Type, item.Tolerance, item.LowerTolerance)
        Next
        Dim Values As String = RefKey
        InsertSketchedSymbolSample(oDim, oDim.Position, oDim.Position, Values, CurrRow)
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
    Private Sub InsertSketchedSymbolSample(oDim As Object, ByRef MaxP As Point2d, ByRef MinP As Point2d, ByRef Values As String, ByRef Balloon As Integer) 'ByRef Text As Box2d)
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
        Dim sPromptStrings() As String = {Balloon, Values}
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
        CurrRow = 0
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
                    DimType(oDim, key, False, CurrRow)
                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
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
    Private Sub UnlinkToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UnlinkToolStripMenuItem.Click
        ExpandQuantity()
    End Sub
    Private Sub dgvDimValues_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvDimValues.CellMouseDown
        If e.Button = MouseButtons.Right Then
            dgvDimValues.ClearSelection()
            Dim ScreenLoc As Drawing.Point = dgvDimValues.PointToScreen(dgvDimValues.Location)
            Dim XLoc As Integer = DataGridView.MousePosition.X - ScreenLoc.X + dgvDimValues.Left
            Dim yLoc As Integer = DataGridView.MousePosition.Y - ScreenLoc.Y + dgvDimValues.Top
            Dim Hit As DataGridView.HitTestInfo = dgvDimValues.HitTest(XLoc, yLoc)
            If Hit.RowIndex < 0 Then Exit Sub
            CurrRow = Hit.RowIndex
            Dim CurrCol As Integer = Hit.ColumnIndex

            If Strings.InStr(dgvDimValues(dgvDimValues.Columns("Balloon").Index, CurrRow).Value, ".") <> 0 Then
                cmsDGVRBC.Items.Item(0).Text = "Link Balloons"
            Else
                cmsDGVRBC.Items.Item(0).Text = "Unlink Balloons"
            End If
        End If
    End Sub
    Private Sub DeleteRowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteRowToolStripMenuItem.Click
        Dim oDoc As Document = _invApp.ActiveDocument
        Dim Reference As String = dgvDimValues(dgvDimValues.Columns("Ref").Index, CurrRow).Value
        Dim Ref() As Byte = New Byte() {}
        Try
            For Each oSketch As SketchedSymbol In oDoc.ActiveSheet.SketchedSymbols
                If oSketch.Name = "Insp" Then
                    Dim key As String = oSketch.GetResultText(oSketch.Definition.Sketch.TextBoxes.Item(2))
                    Call oDoc.ReferenceKeyManager.StringToKey(key, Ref)
                    If key = Reference Then
                        oSketch.Delete()
                        dgvDimValues.Rows.RemoveAt(CurrRow)
                    End If
                End If
            Next
        Catch
        End Try
    End Sub
    Private Sub InsertRowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InsertRowToolStripMenuItem.Click
        dgvDimValues.ClearSelection()
        AddBalloon(CurrRow, True)
        For Row = CurrRow To dgvDimValues.RowCount - 1
            dgvDimValues(dgvDimValues.Columns("Balloon").Index, Row).Value = Row + 1
            Dim oDoc As Document = _invApp.ActiveDocument
            Dim Ref() As Byte = New Byte() {}
            Try
                For Each oSketch As SketchedSymbol In oDoc.ActiveSheet.SketchedSymbols
                    If oSketch.Name = "Insp" Then
                        Dim key As String = oSketch.GetResultText(oSketch.Definition.Sketch.TextBoxes.Item(2))
                        Call oDoc.ReferenceKeyManager.StringToKey(key, Ref)
                        If key = dgvDimValues(dgvDimValues.Columns("Ref").Index, Row).Value Then
                            oSketch.SetPromptResultText(oSketch.Definition.Sketch.TextBoxes.Item(1), dgvDimValues(dgvDimValues.Columns("Balloon").Index, Row).Value)
                            Exit For
                        End If
                    End If
                Next
            Catch
            End Try
        Next
    End Sub

    Private Sub dgvDimValues_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDimValues.CellEndEdit
        If dgvDimValues.Columns(e.ColumnIndex).HeaderText = "Balloon" Then
            For Each row In dgvDimValues.Rows
                If row.index <> e.RowIndex And dgvDimValues(dgvDimValues.Columns("Balloon").Index, row.index).Value =
                    dgvDimValues(dgvDimValues.Columns("Balloon").Index, e.RowIndex).Value Then
                    MsgBox("Balloon values must be unique",, "Error")
                    dgvDimValues(dgvDimValues.Columns("Balloon").Index, e.RowIndex).Value = BalVal
                End If
            Next
            Renumber_Balloons(e.RowIndex)
        End If
    End Sub

    Private Sub dgvDimValues_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles dgvDimValues.CellBeginEdit
        If dgvDimValues.Columns(e.ColumnIndex).HeaderText = "Balloon" Then
            BalVal = dgvDimValues(e.ColumnIndex, e.RowIndex).Value
        End If
    End Sub
End Class