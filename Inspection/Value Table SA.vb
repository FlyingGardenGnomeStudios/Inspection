﻿
Imports System.Runtime.InteropServices
Imports Inspection.Inspection
Imports Inventor
Imports System.Linq
Imports System.IO
Imports System.Data
Imports Microsoft.Office.Interop
Imports System.Windows.Forms
Imports System.Text.RegularExpressions
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
    Dim Read As Boolean = False

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
        Read = False
        ' CurrRow = dgvDimValues.RowCount + 1
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
            ' CurrRow = dgvDimValues.RowCount
            DimType(SelectedFeature, RefKey, Insert, CurrRow + 1)


        End If
    End Sub
    Private Sub DimType(SelectedFeature As Object, RefKey As String, Insert As Boolean, Balloon As Decimal)
        'Try
        ' _invApp.ScreenUpdating = False
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
                    HoleTag(SelectedFeature, RefKey, "Hole Callout", Insert, Balloon)
                Case 117491712
                    ThreadNote(SelectedFeature, RefKey, "Hole Callout", Insert, Balloon)
                Case 117473024
                    Note(SelectedFeature, RefKey, "Note", Insert, Balloon)
                Case 117484032
                    Surface(SelectedFeature, RefKey)
                Case 117488384
                    Chamfer(SelectedFeature, RefKey, "Chamfer", Insert, Balloon)
                Case 117469952
                    HoleTable(SelectedFeature, RefKey)
                Case Else
                    MsgBox("Unknown")
            End Select
        'Catch ex As Exception
        'MessageBox.Show(Me, ex.Message, "Error")
        'Finally
        _invApp.ScreenUpdating = True
        'End Try

    End Sub

#Region "DimensionParsing"
    Function ReturnDegreesMinutesSecondsFromDecimalDegrees(ByVal DecimalDegrees As Decimal, Precision As AngularPrecisionEnum) As String
        Dim DecDegAbs As Decimal = Math.Abs(DecimalDegrees)
        Dim ReturnValue As String = ""
        Dim DegreeSymbol As String = "°"
        Dim MinutesSymbol As String = "’"
        Dim SecondsSymbol As String = Chr(34)
        Dim Degrees As String = Math.Truncate(DecDegAbs) & DegreeSymbol
        Dim MinutesDecimal As Decimal = (DecDegAbs - Math.Truncate(DecDegAbs)) * 60
        Dim SecondsDecimal As Decimal = (MinutesDecimal - Math.Truncate(MinutesDecimal))
        Dim Minutes As String = Math.Truncate(MinutesDecimal) & MinutesSymbol
        Dim Seconds As String = String.Format("{0:##0.0000}", (SecondsDecimal * 60)) & SecondsSymbol
        ReturnValue = Degrees & " " & Minutes & " " & Seconds
        Select Case Precision
            Case AngularPrecisionEnum.kDegreesAngularPrecision, 0
                ReturnValue = Strings.Left(ReturnValue, InStr(ReturnValue, "°"))
            Case AngularPrecisionEnum.kMinutesAngularPrecision, 1
                ReturnValue = Strings.Left(ReturnValue, InStr(ReturnValue, "’"))
            Case AngularPrecisionEnum.kSecondsAngularPrecision, 2
                ReturnValue = Strings.Left(ReturnValue, InStr(ReturnValue, ".") - 1)
            Case AngularPrecisionEnum.kSecondsOneDecimalPlaceAngularPrecision, 3
                ReturnValue = Strings.Left(ReturnValue, InStr(ReturnValue, ".") + 1)
            Case AngularPrecisionEnum.kSecondsTwoDecimalPlaceAngularPrecision, 4
                ReturnValue = Strings.Left(ReturnValue, InStr(ReturnValue, ".") + 2)
            Case AngularPrecisionEnum.kSecondsThreeDecimalPlaceAngularPrecision, 5
                ReturnValue = Strings.Left(ReturnValue, InStr(ReturnValue, ".") + 3)
            Case AngularPrecisionEnum.kSecondsFourDecimalPlaceAngularPrecision, 6
                ReturnValue = Strings.Left(ReturnValue, InStr(ReturnValue, ".") + 4)
        End Select
        Return ReturnValue
    End Function
    Private Function GetFraction(ByVal Input As Decimal, ByVal Accuracy As LinearPrecisionEnum) As String
        Dim Neg As String = ""
        If InStr(Input, "-") <> 0 Then
            Neg = "-"
            Input = Math.Abs(Input)
        End If
        Dim Whole As String = Math.Floor(Input)
        Dim Remainder As Decimal = Input - CInt(Whole)
        If Whole = 0 Then Whole = Nothing
        Select Case Accuracy
            Case LinearPrecisionEnum.kZeroFractionalLinearPrecision, 0
                Return Neg & Whole
            Case LinearPrecisionEnum.kHalfFractionalLinearPrecision, 1
                Remainder = Math.Round(Remainder * 2)
                If Not Remainder = 0 Then
                    Return Neg & Whole & " " & ReduceFraction(Remainder, 2, "/")
                Else
                    Return Neg & Whole
                End If
            Case LinearPrecisionEnum.kQuarterFractionalLinearPrecision, 2
                If Not Remainder = 0 Then
                    Remainder = Math.Round(Remainder * 4)
                    Return Neg & Whole & " " & ReduceFraction(Remainder, 4, "/")
                Else
                    Return Neg & Whole
                End If
            Case LinearPrecisionEnum.kEighthsFractionalLinearPrecision, 3
                If Not Remainder = 0 Then
                    Remainder = Math.Round(Remainder * 8)

                    Return Neg & Whole & " " & ReduceFraction(Remainder, 8, "/")
                Else
                    Return Neg & Whole
                End If
            Case LinearPrecisionEnum.kSixteenthsFractionalLinearPrecision, 4
                If Not Remainder = 0 Then
                    Remainder = Math.Round(Remainder * 16)
                    Return Neg & Whole & " " & ReduceFraction(Remainder, 16, "/")
                Else
                    Return Neg & Whole
                End If
            Case LinearPrecisionEnum.kThirtySecondsFractionalLinearPrecision, 5
                If Not Remainder = 0 Then
                    Remainder = Math.Round(Remainder * 32)
                    Return Neg & Whole & " " & ReduceFraction(Remainder, 32, "/")
                Else
                    Return Neg & Whole
                End If
            Case LinearPrecisionEnum.kSixtyFourthsFractionalLinearPrecision, 6
                If Not Remainder = 0 Then
                    Remainder = Math.Round(Remainder * 64)
                    Return Neg & Whole & " " & ReduceFraction(Remainder, 64, "/")
                Else
                    Return Neg & Whole
                End If
            Case LinearPrecisionEnum.kOneTwentyEighthsFractionalLinearPrecision, 7
                If Not Remainder = 0 Then
                    Remainder = Math.Round(Remainder * 128)
                    Return Neg & Whole & " " & ReduceFraction(Remainder, 128, "/")
                Else
                    Return Neg & Whole
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
    Private Function Fraction_To_Decimal(ByRef Value As String) As Decimal
        Dim sPattern As String = "((?<whole>\d+) (?<num>\d+)/(?<den>\d+))"    ' 1 1/2 case
        Dim regExp = New Regex(sPattern, RegexOptions.Compiled)
        Dim m As Match = regExp.Match(Value)
        Dim dDecValue As Decimal
        If regExp.Match(Value).Success Then
            dDecValue = CInt(m.Groups("whole").Value) + CInt(m.Groups("num").Value) / CInt(m.Groups("den").Value)
        Else
            sPattern = "((?<num>\d+)/(?<den>\d+))"              ' 3/4 case
            regExp = New System.Text.RegularExpressions.Regex(sPattern, RegexOptions.Compiled)
            m = regExp.Match(Value)
            If m.Success Then
                dDecValue = CInt(m.Groups("num").Value) / CInt(m.Groups("den").Value)
            Else
                sPattern = "(?<dec>\d+\.?\d+)"                  ' 0.5 case
                regExp = New System.Text.RegularExpressions.Regex(sPattern, RegexOptions.Compiled)
                m = regExp.Match(Value)
                If m.Success Then
                    dDecValue = CDbl(m.Groups("dec").Value)
                Else
                    sPattern = "(?<whole>\d+)"                  ' 2 case
                    regExp = New System.Text.RegularExpressions.Regex(sPattern, RegexOptions.Compiled)
                    m = regExp.Match(Value)
                    If m.Success Then
                        dDecValue = CDbl(m.Groups("whole").Value)
                    Else
                        MsgBox("Error in value")
                    End If
                End If
            End If
        End If
        Return dDecValue
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
    Private Sub Parse_Units(ByVal oDim As GeneralDimension, ByVal oType As String, ByRef Units As String, ByVal Value As String, ByVal StringValue As String,
                            ByRef Tolmod As Decimal, ByRef Prefix As String, ByVal Precision As Integer)
        If oType = "Angular" Then
            Units = "Degrees"
            Value = FormatNumber(_invApp.ActiveDocument.UnitsOfMeasure.ConvertUnits(oDim.ModelValue, UnitsTypeEnum.kRadianAngleUnits, UnitsTypeEnum.kDefaultDisplayAngleUnits), 8)
            If oDim.Style.AngularFormatIsDecimalDegrees = True Then
                StringValue = Value
                Tag = Chr(176)
            Else
                StringValue = Value
            End If
            Tolmod = Math.PI / 180
        Else
            Value = FormatNumber(_invApp.ActiveDocument.UnitsOfMeasure.ConvertUnits(oDim.ModelValue, UnitsTypeEnum.kDefaultDisplayLengthUnits, UnitsTypeEnum.kDefaultDisplayLengthUnits), 8)
            StringValue = Value
            Select Case oDim.Style.LinearUnits
                Case UnitsTypeEnum.kMillimeterLengthUnits
                    Units = "Millimeter"
                    StringValue = Math.Round(Value * 10, Precision)
                    Tolmod = 0.1
                    Value = StringValue
                Case UnitsTypeEnum.kCentimeterLengthUnits
                    Units = "Centimeter"
                    StringValue = Math.Round(Value * 1, Precision)
                Case UnitsTypeEnum.kMeterLengthUnits
                    Units = "Meter"
                    StringValue = Math.Round(Value / 100, Precision)
                    Tolmod = 100
                    Value = StringValue
                Case UnitsTypeEnum.kMicronLengthUnits
                    Units = "Micron"
                    Tolmod = 0.0001
                    StringValue = Math.Round(Value * 10000, Precision)
                    Value = StringValue
                Case UnitsTypeEnum.kInchLengthUnits
                    Units = "Inch"
                    Tolmod = 2.54
                    Value = Math.Round(Value / 2.54, Precision)
                    If Not oDim.Style.DisplayFormat = DisplayFormatEnum.kDecimalFormat Then
                        StringValue = GetFraction(Value, Precision)
                    Else
                        StringValue = FormatNumber(Value, Precision)
                    End If
                Case UnitsTypeEnum.kFootLengthUnits
                    Units = "Foot"
                    StringValue = Math.Round(Value / 2.54 / 12, Precision)
                    Tolmod = 2.54 * 12
                    Value = StringValue
                Case UnitsTypeEnum.kYardLengthUnits
                    Units = "Yard"
                    StringValue = Math.Round(Value / 2.54 / 12 / 3, Precision)
                    Tolmod = 2.54 * 12 * 3
                    Value = StringValue
                Case UnitsTypeEnum.kMileLengthUnits
                    Units = "Mile"
                    Tolmod = 2.54 * 12 * 5280
                    StringValue = Math.Round(Value / 2.54 / 12 / 5280, Precision)
                    Value = StringValue
            End Select
            Select Case oType
                Case "Diametral"
                    Prefix = Chr(216)
                Case "Radial"
                    Prefix = "R"
            End Select
        End If
    End Sub
    Private Sub Parse_Tolerances(ByVal oDim As GeneralDimension, ByRef anglTol As Decimal, ByRef anguTol As Decimal, ByRef linlTol As Decimal,
                                 ByRef linuTol As Decimal, ByRef TolMod As Decimal, ByVal TolType As ToleranceTypeEnum, ByVal angPrecision As AngularPrecisionEnum,
                                 ByVal linPrecision As LinearPrecisionEnum, ByVal uTol As Decimal, lTol As Decimal)
        If oDim.Style.DisplayFormat = DisplayFormatEnum.kDecimalFormat Then
            Select Case TolType 'oDim.Tolerance.ToleranceType
                Case ToleranceTypeEnum.kMaxTolerance
                    anglTol = Math.Abs(My.Settings("AngN" & linPrecision)) + My.Settings("AngP" & linPrecision)
                    anguTol = 0
                    linlTol = Math.Abs(My.Settings("LinN" & linPrecision)) + My.Settings("LinP" & linPrecision)
                    linuTol = 0
                Case ToleranceTypeEnum.kMinTolerance
                    linuTol = My.Settings("LinP" & linPrecision) + Math.Abs(My.Settings("LinN" & linPrecision))
                    linlTol = 0
                    anguTol = My.Settings("AngP" & linPrecision) + Math.Abs(My.Settings("AngN" & linPrecision))
                    anglTol = 0
                Case ToleranceTypeEnum.kSymmetricTolerance
                    anguTol = uTol / TolMod
                    anglTol = anguTol * -1
                    linuTol = uTol / TolMod
                    linlTol = linuTol * -1
                Case ToleranceTypeEnum.kDefaultTolerance, ToleranceTypeEnum.kBasicTolerance
                    If uTol = 0 Then
                        linuTol = My.Settings("LinP" & linPrecision)
                        anguTol = My.Settings("AngP" & linPrecision)
                    Else
                        linuTol = uTol / TolMod
                        anguTol = uTol / TolMod
                    End If
                    If lTol = 0 Then
                        linlTol = My.Settings("LinN" & linPrecision)
                        anglTol = My.Settings("AngN" & linPrecision)
                    Else
                        linlTol = lTol / TolMod
                        anglTol = lTol / TolMod
                    End If
                Case ToleranceTypeEnum.kDeviationTolerance,
                     ToleranceTypeEnum.kLimitLinearTolerance,
                     ToleranceTypeEnum.kLimitsStackedTolerance,
                     ToleranceTypeEnum.kLimitsFitsShowTolerance,
                     ToleranceTypeEnum.kLimitsFitsShowSizeTolerance,
                     ToleranceTypeEnum.kLimitsFitsLinearTolerance,
                     ToleranceTypeEnum.kLimitsFitsStackedTolerance
                    linuTol = uTol / TolMod
                    linlTol = lTol / TolMod
            End Select
        Else
            linuTol = My.Settings.LinP0
            linlTol = My.Settings.LinN0
            anguTol = My.Settings.AngP0
            anglTol = My.Settings.AngN0
        End If
    End Sub
    Private Sub Add_To_Table(ByVal oType As String, ByVal RefKey As String, ByVal oDim As GeneralDimension, ByVal Prefix As String, ByVal StringValue As String, ByVal Precision As Integer,
                            ByVal TolPrecision As Integer, ByVal linuTol As Decimal, ByVal linlTol As Decimal, ByVal Value As String, ByVal Balloon As String, ByVal Type As String, ByVal Units As String,
                            ByVal Comment As String)
        dgvDimValues(dgvDimValues.Columns("Balloon").Index, CurrRow).Value = Balloon
        dgvDimValues(dgvDimValues.Columns("Ref").Index, CurrRow).Value = RefKey
        dgvDimValues(dgvDimValues.Columns("Qty").Index, CurrRow).Value = 1
        dgvDimValues(dgvDimValues.Columns("Type").Index, CurrRow).Value = Type
        dgvDimValues(dgvDimValues.Columns("SubType").Index, CurrRow).Value = oType
        dgvDimValues(dgvDimValues.Columns("Units").Index, CurrRow).Value = Units
        If oType = "Angular" Then
            If oDim.Style.AngularFormatIsDecimalDegrees = False Then
                dgvDimValues(dgvDimValues.Columns("Value").Index, CurrRow).Value = Prefix & ReturnDegreesMinutesSecondsFromDecimalDegrees(StringValue, Precision) & Tag
                dgvDimValues(dgvDimValues.Columns("LTol").Index, CurrRow).Value = ReturnDegreesMinutesSecondsFromDecimalDegrees(linuTol, TolPrecision)
                dgvDimValues(dgvDimValues.Columns("UTol").Index, CurrRow).Value = ReturnDegreesMinutesSecondsFromDecimalDegrees(linlTol, TolPrecision)
                dgvDimValues(dgvDimValues.Columns("ULimit").Index, CurrRow).Value = ReturnDegreesMinutesSecondsFromDecimalDegrees(Value + linuTol, Precision)
                dgvDimValues(dgvDimValues.Columns("LLimit").Index, CurrRow).Value = ReturnDegreesMinutesSecondsFromDecimalDegrees(Value + linlTol, Precision)
            Else
                dgvDimValues(dgvDimValues.Columns("Value").Index, CurrRow).Value = Prefix & FormatNumber(StringValue, Precision) & Tag
                dgvDimValues(dgvDimValues.Columns("UTol").Index, CurrRow).Value = FormatNumber(linuTol, TolPrecision)
                dgvDimValues(dgvDimValues.Columns("LTol").Index, CurrRow).Value = FormatNumber(linlTol, TolPrecision)
                dgvDimValues(dgvDimValues.Columns("ULimit").Index, CurrRow).Value = FormatNumber(Value + linuTol, Precision)
                dgvDimValues(dgvDimValues.Columns("LLimit").Index, CurrRow).Value = FormatNumber(Value + linlTol, Precision)
            End If
        Else
            If Not oDim.Style.DisplayFormat = DisplayFormatEnum.kDecimalFormat Then
                dgvDimValues(dgvDimValues.Columns("Value").Index, CurrRow).Value = Prefix & GetFraction(Value, Precision) & Tag
                dgvDimValues(dgvDimValues.Columns("UTol").Index, CurrRow).Value = GetFraction(linuTol, TolPrecision)
                dgvDimValues(dgvDimValues.Columns("LTol").Index, CurrRow).Value = GetFraction(linlTol, TolPrecision)
                dgvDimValues(dgvDimValues.Columns("ULimit").Index, CurrRow).Value = GetFraction(Value + linuTol, Precision)
                dgvDimValues(dgvDimValues.Columns("LLimit").Index, CurrRow).Value = GetFraction(Value + linlTol, Precision)
            Else
                dgvDimValues(dgvDimValues.Columns("Value").Index, CurrRow).Value = Prefix & FormatNumber(StringValue, Precision) & Tag
                dgvDimValues(dgvDimValues.Columns("UTol").Index, CurrRow).Value = FormatNumber(linuTol, TolPrecision)
                dgvDimValues(dgvDimValues.Columns("LTol").Index, CurrRow).Value = FormatNumber(linlTol, TolPrecision)
                dgvDimValues(dgvDimValues.Columns("ULimit").Index, CurrRow).Value = FormatNumber(Value + linuTol, Precision)
                dgvDimValues(dgvDimValues.Columns("LLimit").Index, CurrRow).Value = FormatNumber(Value + linlTol, Precision)
            End If
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
        dgvDimValues(dgvDimValues.Columns("Comments").Index, CurrRow).Value = Comment
    End Sub
    Public Sub LinearDim(oDim As GeneralDimension, RefKey As String, oType As String, Insert As Boolean, Balloon As Integer)
        If Read = False Then InsertSketchedSymbolSample(oDim, oDim.Text.RangeBox.MaxPoint, oDim.Text.RangeBox.MinPoint, RefKey, Balloon)
        Dim Value, Prefix, Tag As String
        Prefix = ""
        Tag = ""
        Value = ""
        Dim linuTol, linlTol, anglTol, anguTol As Decimal
        Dim StringValue As String = ""
        dgvDimValues.Rows.Insert(CurrRow)
        Dim Units As String = "Centimeter"
        Dim TolMod As Decimal = 1
        Parse_Units(oDim, oType, Units, Value, StringValue, TolMod, Prefix, oDim.Precision)
        Parse_Tolerances(oDim, anglTol, anguTol, linlTol, linuTol, TolMod, oDim.Tolerance.ToleranceType, oDim.TolerancePrecision, oDim.TolerancePrecision, oDim.Tolerance.Upper, oDim.Tolerance.Lower)
        Add_To_Table(oType, RefKey, oDim, Prefix, StringValue, oDim.Precision, oDim.TolerancePrecision, linuTol, linlTol, Value, Balloon, "Dimension", Units,
                     Strings.Replace(Strings.Replace(oDim.Text.FormattedText, "<DimensionValue/>", ""), "<Br/>", " " & vbCrLf & " "))
        'dgvDimValues(dgvDimValues.Columns("Comments").Index, CurrRow).Value = Strings.Replace(Strings.Replace(oDim.Text.FormattedText, "<DimensionValue/>", ""), "<Br/>", " " & vbCrLf & " ")
        CurrRow += 1
    End Sub
    Public Sub Note(oDim As DrawingNote, RefKey As String, oType As String, Insert As Boolean, Balloon As Integer)
        If Read = False Then InsertSketchedSymbolSample(oDim, oDim.RangeBox.MaxPoint, oDim.RangeBox.MinPoint, RefKey, Balloon)
        dgvDimValues.Rows.Add()
        dgvDimValues(dgvDimValues.Columns("Balloon").Index, dgvDimValues.RowCount - 1).Value = Balloon
        dgvDimValues(dgvDimValues.Columns("Ref").Index, dgvDimValues.RowCount - 1).Value = RefKey
        dgvDimValues(dgvDimValues.Columns("Type").Index, CurrRow).Value = "Note"
        dgvDimValues(dgvDimValues.Columns("Value").Index, dgvDimValues.RowCount - 1).Value = oDim.Text
        dgvDimValues(dgvDimValues.Columns("Qty").Index, dgvDimValues.RowCount - 1).Value = "NA"
        dgvDimValues(dgvDimValues.Columns("Type").Index, dgvDimValues.RowCount - 1).Value = oType
        Dim Values As String = RefKey
        'InsertSketchedSymbolSample(oDim, oDim.RangeBox.MaxPoint, oDim.RangeBox.MinPoint, Values, CurrRow)
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
    Public Sub HoleTag(oDim As HoleTag, RefKey As String, oType As String, Insert As Boolean, Balloon As Integer)
        If Read = False Then InsertSketchedSymbolSample(oDim, oDim.RangeBox.MaxPoint, oDim.RangeBox.MinPoint, RefKey, Balloon)
        dgvDimValues.Rows.Add()
        Dim QTY As Integer


        dgvDimValues(dgvDimValues.Columns("Balloon").Index, dgvDimValues.RowCount - 1).Value = Balloon
        dgvDimValues(dgvDimValues.Columns("Ref").Index, dgvDimValues.RowCount - 1).Value = RefKey
        dgvDimValues(dgvDimValues.Columns("Value").Index, dgvDimValues.RowCount - 1).Value = oDim.Text
        dgvDimValues(dgvDimValues.Columns("Qty").Index, dgvDimValues.RowCount - 1).Value = "NA"
        dgvDimValues(dgvDimValues.Columns("Type").Index, dgvDimValues.RowCount - 1).Value = "Note"
        dgvDimValues(dgvDimValues.Columns("SubType").Index, dgvDimValues.RowCount - 1).Value = oDim.Type
        dgvDimValues(dgvDimValues.Columns("UTol").Index, dgvDimValues.RowCount - 1).Value = oDim.Tolerance.Upper
        dgvDimValues(dgvDimValues.Columns("LTol").Index, dgvDimValues.RowCount - 1).Value = oDim.Tolerance.Lower
        Dim Values As String = RefKey
    End Sub
    Public Sub ThreadNote(ByRef oDim As HoleThreadNote, ByRef RefKey As String, ByRef oType As String, ByRef Insert As Boolean, ByRef Balloon As Integer)
        If Read = False Then InsertSketchedSymbolSample(oDim, oDim.Text.RangeBox.MaxPoint, oDim.Text.RangeBox.MinPoint, RefKey, Balloon)
        Dim Value, Prefix, Tag As String
        Prefix = ""
        Tag = ""
        Value = ""
        Dim linuTol, linlTol, anglTol, anguTol As Decimal
        Dim StringValue As String = ""
        Dim Units As String = "Centimeter"
        Dim TolMod As Decimal = 1
        Dim SavedNote As String = ""
        SavedNote = oDim.FormattedHoleThreadNote
        'If Settings.chkSpearateComponents.Checked = True Then
        'Setup single hole
        ' End If
        Dim DimProps() As String = Strings.Split(SavedNote, "</HoleProperty>")
        Debug.Print(oDim.Text.Text)
        oDim.FormattedHoleThreadNote = "<QuantityNote/>"
        Dim QTY As Integer = Replace(oDim.Text.Text, "X", "")
        _invApp.CommandManager.ControlDefinitions.Item("AppUndoCmd").Execute()
        For x As Integer = 0 To DimProps.Length - 2
            Dim TestCompare As String = Strings.Right(DimProps(x), Len(DimProps(x)) - InStr(DimProps(x), "<") + 1)
            TestCompare = Strings.Replace(TestCompare, "<QuantityNote/>", "")
            TestCompare = Strings.Replace(TestCompare, "</HoleProperty>", "")
            TestCompare = Strings.Right(TestCompare, Len(TestCompare) - InStr(TestCompare, "<StyleOverride") + 1)
            If TestCompare.Contains("<StyleOverride") Then
                TestCompare = TestCompare & "</HoleProperty></StyleOverride>"
            Else
                TestCompare = TestCompare & "</HoleProperty>"
            End If

            If TestCompare.Contains("SetTolerances") = False AndAlso TestCompare.Contains("SetTolerances=") = False Then
                Dim DimValue As String = TestCompare
                If DimValue.Contains("True") = True Then
                    oDim.FormattedHoleThreadNote = Replace(DimValue, "True", " SetTolerances='True")
                ElseIf DimValue.Contains("False") = True Then
                    oDim.FormattedHoleThreadNote = Replace(DimValue, "False", " SetTolerances='False")
                Else
                    oDim.FormattedHoleThreadNote = TestCompare
                End If
            Else
                oDim.FormattedHoleThreadNote = TestCompare
            End If

            Dim FindString As String = "ToleranceType='"
            Dim Y As Integer = Strings.InStr(TestCompare, FindString) + Len(FindString)
            Dim Z As Integer = Strings.InStr(Y, TestCompare, "'")
            Dim Tolerance As Integer
            Debug.Print(Strings.Mid(TestCompare, Y, Z - Y))
            Dim TolType As ToleranceTypeEnum
            'Dim RegexTest As Regex = New Regex("(?<=\D)\d")
            Dim RegexTest As Regex = New Regex("^\d")
            Dim RegexTest2 As Regex = New Regex("\\|`")
            Select Case Strings.Mid(TestCompare, Y, Z - Y)
                Case "kDeviationTolerance"
                    TolType = ToleranceTypeEnum.kDeviationTolerance
                    Debug.Print(RegexTest.Match(oDim.Text.Text).Index)
                    Value = Strings.Right(oDim.Text.Text, Len(oDim.Text.Text) - RegexTest.Match(oDim.Text.Text).Index)
                    'Value = Strings.Left(Value, RegexTest2.Match(Value).Index)
                    If Value.Contains("\") Or Value.Contains("`") Then Value = Strings.Left(Value, RegexTest2.Match(Value).Index)
                    If Value.Contains("/") = True Then Value = Fraction_To_Decimal(Value)
                Case "kSymmetricTolerance"
                    TolType = ToleranceTypeEnum.kSymmetricTolerance
                    Value = Strings.Right(oDim.Text.Text, Len(oDim.Text.Text) - RegexTest.Match(oDim.Text.Text).Index)
                    If Value.Contains("\") Or Value.Contains("`") Then Value = Strings.Left(Value, RegexTest2.Match(Value).Index)
                    If Value.Contains("/") = True Then Value = Fraction_To_Decimal(Value)
                Case "kLimitsStackedTolerance"
                    TolType = ToleranceTypeEnum.kLimitsStackedTolerance
                    oDim.FormattedHoleThreadNote = Replace(TestCompare, "kLimitsStackedTolerance", "kSymmetricTolerance")
                    Value = Strings.Right(oDim.Text.Text, Len(oDim.Text.Text) - RegexTest.Match(oDim.Text.Text).Index)
                    'Value = Strings.Left(Value, RegexTest2.Match(Value).Index)
                    If Value.Contains("\") Or Value.Contains("`") Then Value = Strings.Left(Value, RegexTest2.Match(Value).Index)
                    If Value.Contains("/") = True Then Value = Fraction_To_Decimal(Value)
                Case "kMaxTolerance"
                    TolType = ToleranceTypeEnum.kMaxTolerance
                    Value = Strings.Right(oDim.Text.Text, Len(oDim.Text.Text) - RegexTest.Match(oDim.Text.Text).Index)
                    'Value = Strings.Left(Value, InStr(Value, "MAX") - 1)
                    If Value.Contains("\") Or Value.Contains("`") Then Value = Strings.Left(Value, RegexTest2.Match(Value).Index)
                    If Value.Contains("/") = True Then Value = Fraction_To_Decimal(Value)
                Case "kMinTolerance"
                    TolType = ToleranceTypeEnum.kMinTolerance
                    Value = Strings.Right(oDim.Text.Text, Len(oDim.Text.Text) - RegexTest.Match(oDim.Text.Text).Index)
                    'Value = Strings.Left(Value, InStr(Value, "MAX") - 1)
                    If Value.Contains("\") Or Value.Contains("`") Then Value = Strings.Left(Value, RegexTest2.Match(Value).Index)
                    If Value.Contains("/") = True Then Value = Fraction_To_Decimal(Value)
                    ''''needs to be finished
            End Select


            If TolType = ToleranceTypeEnum.kLimitsStackedTolerance Then _invApp.CommandManager.ControlDefinitions.Item("AppUndoCmd").Execute()
            FindString = " Precision='"
            Y = Strings.InStr(TestCompare, FindString) + Len(FindString)
            Z = Strings.InStr(Y, TestCompare, "'")
            Dim Precision As Decimal = Strings.Mid(TestCompare, Y, Z - Y)

            FindString = "TolerancePrecision='"
            Y = Strings.InStr(TestCompare, FindString) + Len(FindString)
            Z = Strings.InStr(Y, TestCompare, "'")
            Dim TolPrecision As Decimal = Strings.Mid(TestCompare, Y, Z - Y)

            FindString = "UpperTolerance='"
            Y = Strings.InStr(TestCompare, FindString) + Len(FindString)
            Z = Strings.InStr(Y, TestCompare, "'")
            Debug.Print(Strings.Mid(TestCompare, Y, Z - Y))
            Dim UTol As Decimal = Strings.Mid(TestCompare, Y, Z - Y)

            FindString = "LowerTolerance='"
            Y = Strings.InStr(TestCompare, FindString) + Len(FindString)
            Z = Strings.InStr(Y, TestCompare, "'")
            Dim lTol As Decimal
            lTol = Strings.Mid(TestCompare, Y, Z - Y)

            dgvDimValues.Rows.Add()
            Prefix = ""
            Dim Suffix As String = ""
            Dim Comment As String = ""
            oType = "Hole Callout"
#Region "Select Hole Case"
            Y = Strings.InStr(TestCompare, "HolePropertyID='") + 16
            Z = Strings.InStrRev(TestCompare, "HoleProperty'") + 12
            Select Case Strings.Mid(TestCompare, Y, Z - Y)
                Case "kHoleDiameterHoleProperty"
                    Prefix = ""
                    Suffix = ""
                    Comment = "Hole Diameter"
                Case "kHoleDepthHoleProperty"
                    Prefix = ""
                    Suffix = ""
                    Comment = "Hole Depth"
                Case "kCBoreDiameterHoleProperty"
                    Prefix = ""
                    Suffix = ""
                    Comment = "CBore Diameter"
                Case "kCSinkDiameterHoleProperty"
                    Prefix = ""
                    Suffix = ""
                    Comment = "CSink Diameter"
                Case "kCSinkAngleHoleProperty"
                    Prefix = "X"
                    Suffix = "⁰"
                    Comment = "CSink Angle"
                    oType = "Angular"
                Case "kCSinkDepthHoleProperty"
                    Prefix = ""
                    Suffix = ""
                    Comment = "CSink Angle"
                Case "kCBoreDepthHoleProperty"
                    Prefix = ""
                    Suffix = ""
                    Comment = "CBore Depth"
                Case "kThreadDesignationHoleProperty"
                    Prefix = ""
                    Suffix = ""
                    Comment = "Thread Designation"
                Case "kCustomDesignationProperty"
                    Prefix = ""
                    Suffix = ""
                    Comment = "Custome Designation"
                Case "kThreadPitchHoleProperty"
                    Prefix = ""
                    Suffix = "TPI"
                    Comment = "Thread Pitch"
                Case "kThreadClassHoleProperty"
                    Comment = "Thread Class"
                    Prefix = ""
                    Suffix = ""
                Case "kThreadDepthHoleProperty"
                    Comment = "Thread Depth"
                    Prefix = ""
                    Suffix = ""
                Case "kTapDrillDiameterHoleProperty"
                    Comment = "Tap Drill Diameter"
                    Prefix = ""
                    Suffix = ""
                Case "kFastenerTypeHoleProperty"
                    Comment = "Fastener Type"
                    Prefix = ""
                    Suffix = ""
                Case "kFasternerSizeHoleProperty"
                    Comment = "Fastener Size"
                    Prefix = ""
                    Suffix = ""
                Case "kFasternerFitHoleProperty"
                    Comment = "Fastener Fit"
                    Prefix = ""
                    Suffix = ""
            End Select
#End Region
            Parse_Units(oDim, oType, Units, Value, StringValue, TolMod, Prefix, Precision)
            Parse_Tolerances(oDim, anglTol, anguTol, linlTol, linuTol, TolMod, TolType, oDim.Precision, TolPrecision, UTol, lTol)
            Add_To_Table(oType, RefKey, oDim, Prefix, Value, Precision, TolPrecision, linuTol, linlTol, Value, Balloon & Convert.ToChar(x + 65), "Note", Units, Comment)
            'dgvDimValues(dgvDimValues.Columns("Balloon").Index, dgvDimValues.RowCount - 1).Value = Balloon & Convert.ToChar(x + 65)
            'dgvDimValues(dgvDimValues.Columns("Ref").Index, dgvDimValues.RowCount - 1).Value = RefKey
            'dgvDimValues(dgvDimValues.Columns("Qty").Index, dgvDimValues.RowCount - 1).Value = QTY
            'dgvDimValues(dgvDimValues.Columns("Type").Index, dgvDimValues.RowCount - 1).Value = "Note"
            'dgvDimValues(dgvDimValues.Columns("SubType").Index, dgvDimValues.RowCount - 1).Value = oType

            'dgvDimValues(dgvDimValues.Columns("LTol").Index, dgvDimValues.RowCount - 1).Value = oDim.Tolerance.Lower

            'dgvDimValues(dgvDimValues.Columns("Value").Index, dgvDimValues.RowCount - 1).Value =
            'dgvDimValues(dgvDimValues.Columns("Comments").Index, dgvDimValues.RowCount - 1).Value = Comment
            _invApp.CommandManager.ControlDefinitions.Item("AppUndoCmd").Execute()

            CurrRow += 1
        Next
    End Sub
    Public Sub Chamfer(oDim As ChamferNote, RefKey As String, oType As String, insert As Boolean, Balloon As Integer)
        If Read = False Then InsertSketchedSymbolSample(oDim, oDim.RangeBox.MaxPoint, oDim.RangeBox.MinPoint, RefKey, Balloon)
        dgvDimValues.Rows.Add()
        dgvDimValues(dgvDimValues.Columns("Balloon").Index, dgvDimValues.RowCount - 1).Value = Balloon
        dgvDimValues(dgvDimValues.Columns("Ref").Index, dgvDimValues.RowCount - 1).Value = RefKey
        dgvDimValues(dgvDimValues.Columns("Value").Index, dgvDimValues.RowCount - 1).Value = oDim.Text
        dgvDimValues(dgvDimValues.Columns("Qty").Index, dgvDimValues.RowCount - 1).Value = "TBD"
        dgvDimValues(dgvDimValues.Columns("Type").Index, dgvDimValues.RowCount - 1).Value = "Note"
        dgvDimValues(dgvDimValues.Columns("SubType").Index, dgvDimValues.RowCount - 1).Value = oType
        dgvDimValues(dgvDimValues.Columns("UTol").Index, dgvDimValues.RowCount - 1).Value = "NA"
        dgvDimValues(dgvDimValues.Columns("LTol").Index, dgvDimValues.RowCount - 1).Value = "NA"
        Dim Values As String = RefKey
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
            Case 210
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
        Read = True
        CurrRow = 0
        Dim oDoc As Document = _invApp.ActiveDocument
        Dim Ref() As Byte = New Byte() {}
        'Try
        For Each oSketch As SketchedSymbol In oDoc.ActiveSheet.SketchedSymbols
                If oSketch.Name = "Insp" Then
                    Dim key As String = oSketch.GetResultText(oSketch.Definition.Sketch.TextBoxes.Item(2))
                    Call oDoc.ReferenceKeyManager.StringToKey(key, Ref)
                    Dim Refobj As Object = oDoc.ReferenceKeyManager.BindKeyToObject(Ref)
                    oDoc.SelectSet.Clear()
                    Dim oDim As Object = Refobj
                    DimType(oDim, key, False, CurrRow + 1)
                End If
            Next
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'Finally
        'End Try

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
            If dgvDimValues(dgvDimValues.Columns("Qty").Index, CurrRow).Value = 1 AndAlso Strings.InStr(dgvDimValues(dgvDimValues.Columns("Balloon").Index, CurrRow).Value, ".") <> 0 Then
                cmsDGVRBC.Items.Item(0).Visible = False
            Else
                cmsDGVRBC.Items.Item(0).Visible = True
            End If
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