Imports Inventor
Imports System.Runtime.InteropServices
Imports Microsoft.Win32
Imports Autodesk.Windows

Namespace Inspection
    <ProgIdAttribute("Inspection.StandardAddInServer"),
    GuidAttribute("5df622b8-f73c-4238-91d0-97e2b5739f0c")>
    Public Class StandardAddInServer
        Implements Inventor.ApplicationAddInServer
        Dim dgvDimValues As Windows.Forms.DataGridView
        Dim oUserIntrerfaceMgr As UserInterfaceManager
        Dim oWindowValue_Table As DockableWindow
        Dim oWindowCharacteristics As DockableWindow
        Dim WithEvents m_AppEvents As ApplicationEvents
        Private WithEvents m_uiEvents As UserInterfaceEvents
        Private WithEvents m_AddNew As ButtonDefinition
        Private WithEvents m_Show As ButtonDefinition
        Private WithEvents m_Hide As ButtonDefinition
        Private WithEvents oInteraction As InteractionEvents
        Private WithEvents oSelect As SelectEvents


#Region "ApplicationAddInServer Members"

        ' This method is called by Inventor when it loads the AddIn. The AddInSiteObject provides access  
        ' to the Inventor Application object. The FirstTime flag indicates if the AddIn is loaded for
        ' the first time. However, with the introduction of the ribbon this argument is always true.
        Public Sub Activate(ByVal addInSiteObject As Inventor.ApplicationAddInSite, ByVal firstTime As Boolean) Implements Inventor.ApplicationAddInServer.Activate
            ' Initialize AddIn members.
            g_inventorApplication = addInSiteObject.Application

            ' Connect to the user-interface events to handle a ribbon reset.
            m_uiEvents = g_inventorApplication.UserInterfaceManager.UserInterfaceEvents

            ' TODO: Add button definitions.

            ' Sample to illustrate creating a button definition.
            ' Dim largeIcon As stdole.IPictureDisp = PictureDispConverter.ToIPictureDisp(My.Resources.inspdimlarge)
            Dim smallIcon As stdole.IPictureDisp = PictureDispConverter.ToIPictureDisp(My.Resources.inspdimsmall)
            Dim controlDefs As Inventor.ControlDefinitions = g_inventorApplication.CommandManager.ControlDefinitions
            m_AddNew = controlDefs.AddButtonDefinition(
                "Insp Dim",
                "Inspection Dimension",
                CommandTypesEnum.kShapeEditCmdType,
                AddInClientID,
                "Insp Dim",
                "Insp Dim",
                PictureDispConverter.ToIPictureDisp(My.Resources.inspdimsmall),
                PictureDispConverter.ToIPictureDisp(My.Resources.inspdimlarge),
                ButtonDisplayEnum.kDisplayTextInLearningMode)
            m_Show = controlDefs.AddButtonDefinition(
                "Show",
                "Show Details",
                CommandTypesEnum.kShapeEditCmdType,
                AddInClientID,
                "Show",
                "Show",
                PictureDispConverter.ToIPictureDisp(My.Resources.inspdimsmall),
                PictureDispConverter.ToIPictureDisp(My.Resources.inspdimlarge),
                ButtonDisplayEnum.kDisplayTextInLearningMode)
            m_Hide = controlDefs.AddButtonDefinition(
                "Hide",
                "Hide Details",
                CommandTypesEnum.kShapeEditCmdType,
                AddInClientID,
                "Hide",
                "Hide",
                PictureDispConverter.ToIPictureDisp(My.Resources.inspdimsmall),
                PictureDispConverter.ToIPictureDisp(My.Resources.inspdimlarge),
                ButtonDisplayEnum.kDisplayTextInLearningMode)
            ' Add to the user interface, if it's the first time.
            If firstTime Then
                AddToUserInterface()
            End If
            AddHandler Autodesk.Windows.ComponentManager.ItemInitialized, AddressOf Me.ComponentManager_ItemInitialized
        End Sub

        ' This method is called by Inventor when the AddIn is unloaded. The AddIn will be
        ' unloaded either manually by the user or when the Inventor session is terminated.
        Public Sub Deactivate() Implements Inventor.ApplicationAddInServer.Deactivate

            ' TODO:  Add ApplicationAddInServer.Deactivate implementation

            ' Release objects.
            m_uiEvents = Nothing
            g_inventorApplication = Nothing

            System.GC.Collect()
            System.GC.WaitForPendingFinalizers()
        End Sub

        ' This property is provided to allow the AddIn to expose an API of its own to other 
        ' programs. Typically, this  would be done by implementing the AddIn's API
        ' interface in a class and returning that class object through this property.
        Public ReadOnly Property Automation() As Object Implements Inventor.ApplicationAddInServer.Automation
            Get
                Return Nothing
            End Get
        End Property

        ' Note:this method is now obsolete, you should use the 
        ' ControlDefinition functionality for implementing commands.
        Public Sub ExecuteCommand(ByVal commandID As Integer) Implements Inventor.ApplicationAddInServer.ExecuteCommand
        End Sub

#End Region

#Region "User interface definition"
        Private CharacteristicsForm As Characteristics
        Private Value_TableForm As Value_Table
        ' Sub where the user-interface creation is done.  This is called when
        ' the add-in loaded and also if the user interface is reset.
        Private Sub AddToUserInterface()
            ' This is where you'll add code to add buttons to the ribbon.

            '** Sample to illustrate creating a button on a new panel of the Tools tab of the Part ribbon.

            ' Get the part ribbon.
            Dim partRibbon As Ribbon = g_inventorApplication.UserInterfaceManager.Ribbons.Item("Drawing")
            Dim InspectionTab As Inventor.RibbonTab
            ' Get the "Inspection" tab.

            InspectionTab = partRibbon.RibbonTabs.Add("Inspection", "TAB_Inspection", Guid.NewGuid().ToString)
            For Each Tab As Autodesk.Windows.RibbonTab In Autodesk.Windows.ComponentManager.Ribbon.Tabs
                If (Tab.Id = "id_Inspection") Then
                    AddHandler Tab.Activated, AddressOf Me.Tab_Activated
                End If

            Next

            ' Create a new panel.
            Dim AddNew As Inventor.RibbonPanel = InspectionTab.RibbonPanels.Add("Add New", "Insp_Add_New", Guid.NewGuid().ToString)

            ' Add a button.
            AddNew.CommandControls.AddButton(m_AddNew, True, True)
            AddNew.CommandControls.AddButton(m_Show, False, True)
            AddNew.CommandControls.AddButton(m_Hide, False, True)
            oUserIntrerfaceMgr = g_inventorApplication.UserInterfaceManager
            oWindowValue_Table = oUserIntrerfaceMgr.DockableWindows.Add("Value_TableGUID", "InternalValue_Table", "Value Table")
            oWindowCharacteristics = oUserIntrerfaceMgr.DockableWindows.Add("CharacteristicsGUID", "InternalCharacteristics", "Characteristics")

            oWindowCharacteristics.AddChild(CharacteristicsChild)

            oWindowValue_Table.AddChild(ValueTableChild)
            oWindowValue_Table.DisabledDockingStates = Nothing
            oWindowValue_Table.DisabledDockingStates = DockingStateEnum.kDockLeft + DockingStateEnum.kDockRight
            oWindowCharacteristics.DisabledDockingStates = DockingStateEnum.kDockTop + DockingStateEnum.kDockBottom
            oWindowValue_Table.DockingState = DockingStateEnum.kDockBottom
            oWindowCharacteristics.DockingState = DockingStateEnum.kDockLeft

            oWindowCharacteristics.Visible = False
            oWindowValue_Table.Visible = False
        End Sub
        Public Function CharacteristicsChild() As Long

            If Not CharacteristicsForm Is Nothing Then
                CharacteristicsForm.Dispose()
                CharacteristicsForm = Nothing
            End If

            CharacteristicsForm = New Characteristics
            CharacteristicsForm.Show() 'New WindowWrapper(g_inventorApplication.MainFrameHWND))
            Return CharacteristicsForm.Handle.ToInt64()
        End Function
        Public Function ValueTableChild() As Long

            If Not Value_TableForm Is Nothing Then
                Value_TableForm.Dispose()
                Value_TableForm = Nothing
            End If

            Value_TableForm = New Value_Table
            Value_TableForm.Show() 'New WindowWrapper(g_inventorApplication.MainFrameHWND))
            Return Value_TableForm.Handle.ToInt64()
        End Function
        Private Sub m_uiEvents_OnResetRibbonInterface(Context As NameValueMap) Handles m_uiEvents.OnResetRibbonInterface
            ' The ribbon was reset, so add back the add-ins user-interface.
            AddToUserInterface()
        End Sub


        Private Sub ComponentManager_ItemInitialized(ByVal sender As Object, ByVal e As RibbonItemEventArgs)
            'now one Ribbon item is initialized, but the Ribbon control
            'may not be available yet, so check before
            If (Not (Autodesk.Windows.ComponentManager.Ribbon) Is Nothing) Then
                For Each Tab As Autodesk.Windows.RibbonTab In Autodesk.Windows.ComponentManager.Ribbon.Tabs
                    If (Tab.Id = "id_TabTools") Then 'id_Inspection") Then
                        AddHandler Tab.Activated, AddressOf Me.Tab_Activated
                    End If

                Next
                'and remove the event handler
                RemoveHandler Autodesk.Windows.ComponentManager.ItemInitialized, AddressOf Me.ComponentManager_ItemInitialized
            End If

        End Sub

        Private Sub Tab_Activated(ByVal sender As Object, ByVal e As EventArgs)
            System.Windows.Forms.MessageBox.Show(("Tab " _
                        + (ComponentManager.Ribbon.ActiveTab.Id + " Activated!")))
        End Sub

        'Private Sub ComponentManager_ItemInitialized(ByVal sender As Object, ByVal e As RibbonItemEventArgs)
        '    ' Now one Ribbon item is initialized,
        '    ' but the Ribbon control may not be available 
        '    ' yet, so check before
        '    If ComponentManager.Ribbon.ActiveTab.AutomationName = "Inspection" Then
        '        oWindow.Visible = True
        '    Else
        '        oWindow.Visible = False
        '        ''If (Not (Autodesk.Windows.ComponentManager.Ribbon.ActiveTab.Name) Is "id_TabInspection") Then
        '        'For Each Tab As Autodesk.Windows.RibbonTab In Autodesk.Windows.ComponentManager.Ribbon.Tabs
        '        '    ' Replace with the id of the target tab
        '        '    If (Tab.Id = "id_TabInspection") Then
        '        '        AddHandler Tab.Activated, AddressOf Me.Tab_Activated
        '        '        Exit For
        '        '    End If
        '        'Next
        '        ''and remove the event handler
        '        'AddHandler Autodesk.Windows.ComponentManager.ItemInitialized, AddressOf Me.ComponentManager_ItemInitialized
        '    End If

        'End Sub
        '' Sample handler for the button.
        'Private Sub Tab_Activated(ByVal sender As Object, ByVal e As EventArgs)
        '    System.Windows.Forms.MessageBox.Show(("Tab " & (ComponentManager.Ribbon.ActiveTab.Id & " Activated!")))
        'End Sub
        Private Sub AddNew(Context As NameValueMap) Handles m_AddNew.OnExecute

            Dim oDoc As DrawingDocument = g_inventorApplication.ActiveDocument
            Dim oInteraction As InteractionEvents = g_inventorApplication.CommandManager.CreateInteractionEvents
            oInteraction.StatusBarText = "Select a dimension"
            oSelect = oInteraction.SelectEvents
            oSelect.AddSelectionFilter(SelectionFilterEnum.kDrawingDimensionFilter)
            oSelect.SingleSelectEnabled = True
            oInteraction.Start()

            Dim oSheetSettings As SheetSettings = oDoc.SheetSettings
            Dim SelectedFeature = g_inventorApplication.CommandManager.Pick(SelectionFilterEnum.kAllEntitiesFilter, "Select something")
            Dim RefKeyValue() As Byte = New Byte() {}
            Call SelectedFeature.GetReferenceKey(RefKeyValue)
            Dim RefKey As String = g_inventorApplication.ActiveDocument.ReferenceKeyManager.KeyToString(RefKeyValue)
            ' Dim Value_Table As New Value_Table
            'CharacteristicsForm.txtCustomer.Text = "IMM"

            Value_TableForm.PopAddin(Me)

            Select Case SelectedFeature.type
                Case 117474560
                    Value_TableForm.LinearDim(SelectedFeature, RefKey)
                Case 117475328
                    Value_TableForm.DiametralDim(SelectedFeature, RefKey)
                Case 117475072
                    Value_TableForm.RadialDim(SelectedFeature, RefKey)
                Case 117483008
                    Value_TableForm.FCF(SelectedFeature, RefKey)
                Case 117471488
                    Value_TableForm.HoleTableTag(SelectedFeature, RefKey)
                Case 117491712
                    Value_TableForm.HoleTag(SelectedFeature, RefKey)
                Case 117473024
                    Value_TableForm.Note(SelectedFeature, RefKey)
                Case 117484032
                    Value_TableForm.Surface(SelectedFeature, RefKey)
                Case 117488384
                    Value_TableForm.Chamfer(SelectedFeature, RefKey)
                Case 117469952
                    Value_TableForm.HoleTable(SelectedFeature, RefKey)
                Case Else
                    MsgBox("Unknown")
            End Select
        End Sub
        Public Sub CreateSketchedSymbolDefinition()
            ' Set a reference to the drawing document.
            ' This assumes a drawing document is active.
            Dim oDrawDoc As DrawingDocument
            oDrawDoc = g_inventorApplication.ActiveDocument

            ' Create the new sketched symbol definition.
            Dim oSketchedSymbolDef As SketchedSymbolDefinition
            oSketchedSymbolDef = oDrawDoc.SketchedSymbolDefinitions.Add("mySymbol")

            ' Open the sketched symbol definition's sketch for edit. This is done by calling the Edit
            ' method of the SketchedSymbolDefinition to obtain a DrawingSketch. This actually creates
            ' a copy of the sketched symbol definition's and opens it for edit.
            Dim oSketch As DrawingSketch = Nothing
            Call oSketchedSymbolDef.Edit(oSketch)

            Dim oTG As TransientGeometry
            oTG = g_inventorApplication.TransientGeometry

            Dim oInsertionPoint As Point2d = (oTG.CreatePoint2d(0, 0))
            Dim oSketchCircle As SketchCircle
            oSketchCircle = oSketch.SketchCircles.AddByCenterRadius(oTG.CreatePoint2d(0, 0), (My.Settings.BalloonSize / 75))
            Dim oSheetSettings As SheetSettings = oDrawDoc.SheetSettings
            Dim oColor As Color = g_inventorApplication.TransientObjects.CreateColor(oSheetSettings.SheetColor.Red, oSheetSettings.SheetColor.Green, oSheetSettings.SheetColor.Blue, 0)
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
        Public Sub InsertSketchedSymboSample(oDim As Object, ByRef MaxP As Point2d, ByRef MinP As Point2d, ByRef Values As String) 'ByRef Text As Box2d)
            Dim oDrawDoc As DrawingDocument
            oDrawDoc = g_inventorApplication.ActiveDocument
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
            Dim sPromptStrings() As String = {dgvDimValues.RowCount - 1, Values}
            ' Create sketched symbol
            Dim oTG As TransientGeometry
            oTG = g_inventorApplication.TransientGeometry
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
            oCol = g_inventorApplication.TransientObjects.CreateObjectCollection
            oCol.Add(oPoint)
            oCol.Add(oGI)
            oSketchedSymbol = oSheet.SketchedSymbols.Add(oSketchedSymbolDef, oPoint, 0, 1, sPromptStrings)
            oSketchedSymbol.Leader.AddLeader(oCol)
            oSketchedSymbol.LeaderVisible = False
        End Sub
        Private Sub LinearDim(oDim As LinearGeneralDimension, RefKey As Byte())
            dgvDimValues.Rows.Add(dgvDimValues.Rows.Count, g_inventorApplication.ActiveDocument.ReferenceKeyManager.KeyToString(RefKey), oDim.Text.Text, "TBD", oDim.Type, oDim.Tolerance.Upper, oDim.Tolerance.Lower)
            Dim Values As String = "<Number>" & dgvDimValues.Rows.Count &
            "<Reference>" & g_inventorApplication.ActiveDocument.ReferenceKeyManager.KeyToString(RefKey) &
            "<Size>" & My.Settings.BalloonSize &
            "<Position>" & My.Settings.BalloonPos &
            "<BalloonColor>" = "TBD" &
            "<TextColor>" = "TBD" &
            "<Value>" & oDim.Text.Text &
            "<QTY>" & "TBD" &
            "<Type>" & oDim.Type &
            "<UTol>" & oDim.Tolerance.Upper &
            "<LTol>" & oDim.Tolerance.Lower
            InsertSketchedSymboSample(oDim, oDim.Text.RangeBox.MaxPoint, oDim.Text.RangeBox.MinPoint, Values)
        End Sub
        Private Sub DiametralDim(oDim As DiameterGeneralDimension, RefKey As Byte())
            ' Value_TableForm.dgvDimValues.Rows.Add(Value_TableForm.dgvDimValues.Rows.Count, g_inventorApplication.ActiveDocument.ReferenceKeyManager.KeyToString(RefKey), oDim.Text, "TBD", oDim.Type, "", oDim.Tolerance.Upper, oDim.Tolerance.Lower, "", "", "", "Comment")
            Value_TableForm.dgvDimValues.Rows.Add(1)
            Value_TableForm.dgvDimValues(Value_TableForm.dgvDimValues.Columns("Balloon").Index, Value_TableForm.dgvDimValues.RowCount - 1).Value = Value_TableForm.dgvDimValues.RowCount
            Value_TableForm.dgvDimValues(Value_TableForm.dgvDimValues.Columns("Ref").Index, Value_TableForm.dgvDimValues.RowCount - 1).Value = g_inventorApplication.ActiveDocument.ReferenceKeyManager.KeyToString(RefKey)
            Value_TableForm.dgvDimValues(Value_TableForm.dgvDimValues.Columns("Value").Index, Value_TableForm.dgvDimValues.RowCount - 1).Value = oDim.Text.Text
            Value_TableForm.dgvDimValues(Value_TableForm.dgvDimValues.Columns("Qty").Index, Value_TableForm.dgvDimValues.RowCount - 1).Value = "TBD"
            Value_TableForm.dgvDimValues(Value_TableForm.dgvDimValues.Columns("Type").Index, Value_TableForm.dgvDimValues.RowCount - 1).Value = oDim.Type
            Value_TableForm.dgvDimValues(Value_TableForm.dgvDimValues.Columns("UTol").Index, Value_TableForm.dgvDimValues.RowCount - 1).Value = oDim.Tolerance.Upper
            Value_TableForm.dgvDimValues(Value_TableForm.dgvDimValues.Columns("LTol").Index, Value_TableForm.dgvDimValues.RowCount - 1).Value = oDim.Tolerance.Lower
            Dim Values As String = "<Number>" & Value_TableForm.dgvDimValues.Rows.Count &
            "<Reference>" & g_inventorApplication.ActiveDocument.ReferenceKeyManager.KeyToString(RefKey) &
            "<Value>" & oDim.Text.Text &
            "<QTY>" & "TBD" &
            "<Type>" & oDim.Type &
            "<UTol>" & oDim.Tolerance.Upper &
            "<LTol>" & oDim.Tolerance.Lower
            InsertSketchedSymboSample(oDim, oDim.Text.RangeBox.MaxPoint, oDim.Text.RangeBox.MinPoint, Values)
        End Sub
        Private Sub RadialDim(oDim As RadiusGeneralDimension, RefKey As Byte())
            dgvDimValues.Rows.Add(dgvDimValues.Rows.Count, g_inventorApplication.ActiveDocument.ReferenceKeyManager.KeyToString(RefKey), oDim.Text.Text, "TBD", oDim.Type, oDim.Tolerance.Upper, oDim.Tolerance.Lower)
            Dim Values As String = "<Number>" & dgvDimValues.Rows.Count &
            "<Reference>" & g_inventorApplication.ActiveDocument.ReferenceKeyManager.KeyToString(RefKey) &
            "<Value>" & oDim.Text.Text &
            "<QTY>" & "TBD" &
            "<Type>" & oDim.Type &
            "<UTol>" & oDim.Tolerance.Upper &
            "<LTol>" & oDim.Tolerance.Lower
            InsertSketchedSymboSample(oDim, oDim.Text.RangeBox.MaxPoint, oDim.Text.RangeBox.MinPoint, Values)
        End Sub
        Private Sub Note(oDim As DrawingNote, RefKey As Byte())
            dgvDimValues.Rows.Add(dgvDimValues.Rows.Count, g_inventorApplication.ActiveDocument.ReferenceKeyManager.KeyToString(RefKey), oDim.Text, "TBD", oDim.Type)
            Dim Values As String = "<Number>" & dgvDimValues.Rows.Count &
            "<Reference>" & g_inventorApplication.ActiveDocument.ReferenceKeyManager.KeyToString(RefKey) &
            "<Value>" & oDim.Text &
            "<QTY>" & "TBD" &
            "<Type>" & oDim.Type &
            "<UTol>" & oDim.Tolerance.Upper &
            "<LTol>" & oDim.Tolerance.Lower
            InsertSketchedSymboSample(oDim, oDim.RangeBox.MaxPoint, oDim.RangeBox.MinPoint, Values)
        End Sub
        Private Sub HoleTableTag(oDim As HoleTag, RefKey As Byte())
            dgvDimValues.Rows.Add(dgvDimValues.Rows.Count, g_inventorApplication.ActiveDocument.ReferenceKeyManager.KeyToString(RefKey), oDim.Text, "TBD", oDim.Type)
            Dim Values As String = "<Number>" & dgvDimValues.Rows.Count &
            "<Reference>" & g_inventorApplication.ActiveDocument.ReferenceKeyManager.KeyToString(RefKey) &
            "<Value>" & oDim.Text &
            "<QTY>" & "TBD" &
            "<Type>" & oDim.Type &
            "<UTol>" & oDim.Tolerance.Upper &
            "<LTol>" & oDim.Tolerance.Lower
            InsertSketchedSymboSample(oDim, oDim.RangeBox.MaxPoint, oDim.RangeBox.MinPoint, Values)
        End Sub
        Private Sub Surface(oDim As SurfaceTextureSymbol, RefKey As Byte())
            dgvDimValues.Rows.Add(dgvDimValues.Rows.Count, g_inventorApplication.ActiveDocument.ReferenceKeyManager.KeyToString(RefKey), "Value", "TBD", oDim.Type, oDim.MaximumRoughness, oDim.MinimumRoughness)
            Dim Values As String = "<Number>" & dgvDimValues.Rows.Count &
            "<Reference>" & g_inventorApplication.ActiveDocument.ReferenceKeyManager.KeyToString(RefKey) &
            "<Value>" & oDim.Text.Text &
            "<QTY>" & "TBD" &
            "<Type>" & oDim.Type &
            "<UTol>" & oDim.Tolerance.Upper &
            "<LTol>" & oDim.Tolerance.Lower
            InsertSketchedSymboSample(oDim, oDim.Position, oDim.Position, Values)
        End Sub
        Private Sub HoleTag(oDim As HoleThreadNote, RefKey As Byte())
            dgvDimValues.Rows.Add(dgvDimValues.Rows.Count, g_inventorApplication.ActiveDocument.ReferenceKeyManager.KeyToString(RefKey), oDim.Text, "TBD", oDim.Type, oDim.Tolerance.Upper, oDim.Tolerance.Lower)
            Dim Values As String = "<Number>" & dgvDimValues.Rows.Count &
            "<Reference>" & g_inventorApplication.ActiveDocument.ReferenceKeyManager.KeyToString(RefKey) &
            "<Value>" & oDim.Text.Text &
            "<QTY>" & "TBD" &
            "<Type>" & oDim.Type &
            "<UTol>" & oDim.Tolerance.Upper &
            "<LTol>" & oDim.Tolerance.Lower
            InsertSketchedSymboSample(oDim, oDim.Text.RangeBox.MaxPoint, oDim.Text.RangeBox.MinPoint, Values)
        End Sub
        Private Sub Chamfer(oDim As ChamferNote, RefKey As Byte())
            dgvDimValues.Rows.Add(dgvDimValues.Rows.Count, g_inventorApplication.ActiveDocument.ReferenceKeyManager.KeyToString(RefKey), oDim.Text, "TBD", oDim.Type, "NA", "NA")
            Dim Values As String = "<Number>" & dgvDimValues.Rows.Count &
            "<Reference>" & g_inventorApplication.ActiveDocument.ReferenceKeyManager.KeyToString(RefKey) &
            "<Value>" & oDim.Text &
            "<QTY>" & "TBD" &
            "<Type>" & oDim.Type &
            "<UTol>" & oDim.Tolerance.Upper &
            "<LTol>" & oDim.Tolerance.Lower
            InsertSketchedSymboSample(oDim, oDim.RangeBox.MaxPoint, oDim.RangeBox.MinPoint, Values)
        End Sub
        Private Sub HoleTable(oDim As HoleTable, RefKey As Byte())
            Dim Text As String = ""
            For Each Row As HoleTableRow In oDim.HoleTableRows
                For Each Col In oDim.HoleTableColumns
                    Debug.Print(Row.Item(Col).Text)
                Next
                'Text = Text & item.HoleTag & " " & item. & " " & item.DatumTwo & " " & item.DatumThree & vbNewLine
                'dgvDimValues.Rows.Add(dgvDimValues.Rows.Count, oDim.GetHashCode, Text, "TBD", oDim.Type, item.Tolerance, item.LowerTolerance)
            Next
            Dim Values As String = "<Number>" & dgvDimValues.Rows.Count &
            "<Reference>" & g_inventorApplication.ActiveDocument.ReferenceKeyManager.KeyToString(RefKey) &
            "<Value>" & oDim.Text.Text &
            "<QTY>" & "TBD" &
            "<Type>" & oDim.Type &
            "<UTol>" & oDim.Tolerance.Upper &
            "<LTol>" & oDim.Tolerance.Lower
            InsertSketchedSymboSample(oDim, oDim.RangeBox.MaxPoint, oDim.RangeBox.MinPoint, Values)
        End Sub
        Private Sub FCF(oDim As FeatureControlFrame, RefKey As Byte())
            Dim Text As String = ""
            For Each item As FeatureControlFrameRow In oDim.FeatureControlFrameRows

                Text = Text & item.GeometricCharacteristic & " " & item.DatumOne & " " & item.DatumTwo & " " & item.DatumThree & vbNewLine
                dgvDimValues.Rows.Add(dgvDimValues.Rows.Count, g_inventorApplication.ActiveDocument.ReferenceKeyManager.KeyToString(RefKey), Text, "TBD", oDim.Type, item.Tolerance, item.LowerTolerance)
            Next
            Dim Values As String = "<Number>" & dgvDimValues.Rows.Count &
            "<Reference>" & g_inventorApplication.ActiveDocument.ReferenceKeyManager.KeyToString(RefKey) &
            "<Value>" & oDim.Text.Text &
            "<QTY>" & "TBD" &
            "<Type>" & oDim.Type &
            "<UTol>" & oDim.Tolerance.Upper &
            "<LTol>" & oDim.Tolerance.Lower
            InsertSketchedSymboSample(oDim, oDim.Position, oDim.Position, Values)
        End Sub
        Sub HoleTable()
            Dim oDoc As DrawingDocument
            oDoc = g_inventorApplication.ActiveDocument
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

        Private Sub m_Hide_OnExecute(Context As NameValueMap) Handles m_Hide.OnExecute
            DockVisibility(False)
        End Sub
        Public Sub DockVisibility(Visible As Boolean)
            Dim oUserInterfaceMgr As UserInterfaceManager = g_inventorApplication.UserInterfaceManager
            Select Case Visible
                Case True
                    oWindowCharacteristics.Visible = True
                    oWindowValue_Table.Visible = True
                    oWindowCharacteristics.DockingState = DockingStateEnum.kDockLastKnown
                    oWindowValue_Table.DockingState = DockingStateEnum.kDockLastKnown
                Case False
                    oWindowCharacteristics.Visible = False
                    oWindowValue_Table.Visible = False
            End Select
        End Sub

        Private Sub m_Show_OnExecute(Context As NameValueMap) Handles m_Show.OnExecute
            DockVisibility(True)
        End Sub

#End Region

    End Class
End Namespace


Public Module Globals
    ' Inventor application object.
    Public g_inventorApplication As Inventor.Application

#Region "Function to get the add-in client ID."
    ' This function uses reflection to get the GuidAttribute associated with the add-in.
    Public Function AddInClientID() As String
        Dim guid As String = ""
        Try
            Dim t As Type = GetType(Inspection.StandardAddInServer)
            Dim customAttributes() As Object = t.GetCustomAttributes(GetType(GuidAttribute), False)
            Dim guidAttribute As GuidAttribute = CType(customAttributes(0), GuidAttribute)
            guid = "{" + guidAttribute.Value.ToString() + "}"
        Catch
        End Try

        Return guid
    End Function
#End Region

#Region "hWnd Wrapper Class"
    ' This class is used to wrap a Win32 hWnd as a .Net IWind32Window class.
    ' This is primarily used for parenting a dialog to the Inventor window.
    '
    ' For example:
    ' myForm.Show(New WindowWrapper(g_inventorApplication.MainFrameHWND))
    '
    Public Class WindowWrapper
        Implements System.Windows.Forms.IWin32Window
        Public Sub New(ByVal handle As IntPtr)
            _hwnd = handle
        End Sub

        Public ReadOnly Property Handle() As IntPtr _
          Implements System.Windows.Forms.IWin32Window.Handle
            Get
                Return _hwnd
            End Get
        End Property

        Private _hwnd As IntPtr
    End Class
#End Region

#Region "Image Converter"
    ' Class used to convert bitmaps and icons from their .Net native types into
    ' an IPictureDisp object which is what the Inventor API requires. A typical
    ' usage is shown below where MyIcon is a bitmap or icon that's available
    ' as a resource of the project.
    '
    ' Dim smallIcon As stdole.IPictureDisp = PictureDispConverter.ToIPictureDisp(My.Resources.MyIcon)

    Public NotInheritable Class PictureDispConverter
        <DllImport("OleAut32.dll", EntryPoint:="OleCreatePictureIndirect", ExactSpelling:=True, PreserveSig:=False)>
        Private Shared Function OleCreatePictureIndirect(
            <MarshalAs(UnmanagedType.AsAny)> ByVal picdesc As Object,
            ByRef iid As Guid,
            <MarshalAs(UnmanagedType.Bool)> ByVal fOwn As Boolean) As stdole.IPictureDisp
        End Function

        Shared iPictureDispGuid As Guid = GetType(stdole.IPictureDisp).GUID

        Private NotInheritable Class PICTDESC
            Private Sub New()
            End Sub

            'Picture Types
            Public Const PICTYPE_BITMAP As Short = 1
            Public Const PICTYPE_ICON As Short = 3

            <StructLayout(LayoutKind.Sequential)>
            Public Class Icon
                Friend cbSizeOfStruct As Integer = Marshal.SizeOf(GetType(PICTDESC.Icon))
                Friend picType As Integer = PICTDESC.PICTYPE_ICON
                Friend hicon As IntPtr = IntPtr.Zero
                Friend unused1 As Integer
                Friend unused2 As Integer

                Friend Sub New(ByVal icon As System.Drawing.Icon)
                    Me.hicon = icon.ToBitmap().GetHicon()
                End Sub
            End Class

            <StructLayout(LayoutKind.Sequential)>
            Public Class Bitmap
                Friend cbSizeOfStruct As Integer = Marshal.SizeOf(GetType(PICTDESC.Bitmap))
                Friend picType As Integer = PICTDESC.PICTYPE_BITMAP
                Friend hbitmap As IntPtr = IntPtr.Zero
                Friend hpal As IntPtr = IntPtr.Zero
                Friend unused As Integer

                Friend Sub New(ByVal bitmap As System.Drawing.Bitmap)
                    Me.hbitmap = bitmap.GetHbitmap()
                End Sub
            End Class
        End Class

        Public Shared Function ToIPictureDisp(ByVal icon As System.Drawing.Icon) As stdole.IPictureDisp
            Dim pictIcon As New PICTDESC.Icon(icon)
            Return OleCreatePictureIndirect(pictIcon, iPictureDispGuid, True)
        End Function

        Public Shared Function ToIPictureDisp(ByVal bmp As System.Drawing.Bitmap) As stdole.IPictureDisp
            Dim pictBmp As New PICTDESC.Bitmap(bmp)
            Return OleCreatePictureIndirect(pictBmp, iPictureDispGuid, True)
        End Function
    End Class
#End Region
End Module
