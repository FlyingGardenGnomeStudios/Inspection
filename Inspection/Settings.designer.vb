<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Settings
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.ddpGeneralSettings = New ScrewTurn.DropDownPanel()
        Me.txtJobNumber = New System.Windows.Forms.TextBox()
        Me.btnJobNumber = New System.Windows.Forms.Button()
        Me.txtCustomer = New System.Windows.Forms.TextBox()
        Me.btnCustomer = New System.Windows.Forms.Button()
        Me.txtDocRev = New System.Windows.Forms.TextBox()
        Me.btnDocRev = New System.Windows.Forms.Button()
        Me.txtDocName = New System.Windows.Forms.TextBox()
        Me.btnDocName = New System.Windows.Forms.Button()
        Me.txtDocNum = New System.Windows.Forms.TextBox()
        Me.btnDocNum = New System.Windows.Forms.Button()
        Me.txtPartRev = New System.Windows.Forms.TextBox()
        Me.btnPartRev = New System.Windows.Forms.Button()
        Me.txtPartName = New System.Windows.Forms.TextBox()
        Me.btnPartName = New System.Windows.Forms.Button()
        Me.txtPartNumber = New System.Windows.Forms.TextBox()
        Me.btnPartNum = New System.Windows.Forms.Button()
        Me.CharacteristicsTab = New System.Windows.Forms.TabControl()
        Me.tbpSettings = New System.Windows.Forms.TabPage()
        Me.ddpSampleing = New ScrewTurn.DropDownPanel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtSampleSize = New System.Windows.Forms.TextBox()
        Me.txtLotSize = New System.Windows.Forms.TextBox()
        Me.tbpExtSettings = New System.Windows.Forms.TabPage()
        Me.ddpHoleCallout = New ScrewTurn.DropDownPanel()
        Me.CheckBox5 = New System.Windows.Forms.CheckBox()
        Me.rtbHoleCallout = New System.Windows.Forms.RichTextBox()
        Me.CheckBox7 = New System.Windows.Forms.CheckBox()
        Me.ddpNotes = New ScrewTurn.DropDownPanel()
        Me.CheckBox4 = New System.Windows.Forms.CheckBox()
        Me.rtbNotes = New System.Windows.Forms.RichTextBox()
        Me.CheckBox6 = New System.Windows.Forms.CheckBox()
        Me.ddpDimensions = New ScrewTurn.DropDownPanel()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.CheckBox3 = New System.Windows.Forms.CheckBox()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.ddpTolerance = New ScrewTurn.DropDownPanel()
        Me.ToleranceTab = New System.Windows.Forms.TabControl()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.dgvLTolerance = New System.Windows.Forms.DataGridView()
        Me.LinPrecision = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LinUL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LinLL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LinUTol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LinLTol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.dgvAngTolerance = New System.Windows.Forms.DataGridView()
        Me.Precision = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AngUL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AngLL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AngUTol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AngLTol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.gpbUnits = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbAngle = New System.Windows.Forms.ComboBox()
        Me.cmbSecondary = New System.Windows.Forms.ComboBox()
        Me.cmbPrimary = New System.Windows.Forms.ComboBox()
        Me.gpbType = New System.Windows.Forms.GroupBox()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.CheckBox8 = New System.Windows.Forms.CheckBox()
        Me.ddpExtractionVariables = New ScrewTurn.DropDownPanel()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Variable = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Value = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ddpGeneralSettings.SuspendLayout()
        Me.CharacteristicsTab.SuspendLayout()
        Me.tbpSettings.SuspendLayout()
        Me.ddpSampleing.SuspendLayout()
        Me.tbpExtSettings.SuspendLayout()
        Me.ddpHoleCallout.SuspendLayout()
        Me.ddpNotes.SuspendLayout()
        Me.ddpDimensions.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.ddpTolerance.SuspendLayout()
        Me.ToleranceTab.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.dgvLTolerance, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.dgvAngTolerance, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gpbUnits.SuspendLayout()
        Me.gpbType.SuspendLayout()
        Me.ddpExtractionVariables.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ddpGeneralSettings
        '
        Me.ddpGeneralSettings.AutoCollapseDelay = -1
        Me.ddpGeneralSettings.Controls.Add(Me.txtJobNumber)
        Me.ddpGeneralSettings.Controls.Add(Me.btnJobNumber)
        Me.ddpGeneralSettings.Controls.Add(Me.txtCustomer)
        Me.ddpGeneralSettings.Controls.Add(Me.btnCustomer)
        Me.ddpGeneralSettings.Controls.Add(Me.txtDocRev)
        Me.ddpGeneralSettings.Controls.Add(Me.btnDocRev)
        Me.ddpGeneralSettings.Controls.Add(Me.txtDocName)
        Me.ddpGeneralSettings.Controls.Add(Me.btnDocName)
        Me.ddpGeneralSettings.Controls.Add(Me.txtDocNum)
        Me.ddpGeneralSettings.Controls.Add(Me.btnDocNum)
        Me.ddpGeneralSettings.Controls.Add(Me.txtPartRev)
        Me.ddpGeneralSettings.Controls.Add(Me.btnPartRev)
        Me.ddpGeneralSettings.Controls.Add(Me.txtPartName)
        Me.ddpGeneralSettings.Controls.Add(Me.btnPartName)
        Me.ddpGeneralSettings.Controls.Add(Me.txtPartNumber)
        Me.ddpGeneralSettings.Controls.Add(Me.btnPartNum)
        Me.ddpGeneralSettings.EnableHeaderMenu = True
        Me.ddpGeneralSettings.ExpandAnimationSpeed = ScrewTurn.AnimationSpeed.Medium
        Me.ddpGeneralSettings.Expanded = True
        Me.ddpGeneralSettings.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddpGeneralSettings.HeaderHeight = 20
        Me.ddpGeneralSettings.HeaderIconNormal = Nothing
        Me.ddpGeneralSettings.HeaderIconOver = Nothing
        Me.ddpGeneralSettings.HeaderText = "General Settings"
        Me.ddpGeneralSettings.HomeLocation = New System.Drawing.Point(0, 3)
        Me.ddpGeneralSettings.HotTrackStyle = ScrewTurn.HotTrackStyle.Both
        Me.ddpGeneralSettings.Location = New System.Drawing.Point(0, 3)
        Me.ddpGeneralSettings.ManageControls = False
        Me.ddpGeneralSettings.Moveable = False
        Me.ddpGeneralSettings.Name = "ddpGeneralSettings"
        Me.ddpGeneralSettings.RoundedCorners = False
        Me.ddpGeneralSettings.Size = New System.Drawing.Size(279, 223)
        Me.ddpGeneralSettings.TabIndex = 0
        '
        'txtJobNumber
        '
        Me.txtJobNumber.Location = New System.Drawing.Point(130, 195)
        Me.txtJobNumber.MinimumSize = New System.Drawing.Size(50, 20)
        Me.txtJobNumber.Name = "txtJobNumber"
        Me.txtJobNumber.Size = New System.Drawing.Size(148, 20)
        Me.txtJobNumber.TabIndex = 16
        '
        'btnJobNumber
        '
        Me.btnJobNumber.Location = New System.Drawing.Point(3, 192)
        Me.btnJobNumber.Name = "btnJobNumber"
        Me.btnJobNumber.Size = New System.Drawing.Size(121, 23)
        Me.btnJobNumber.TabIndex = 15
        Me.btnJobNumber.Text = "Job Number"
        Me.btnJobNumber.UseVisualStyleBackColor = True
        '
        'txtCustomer
        '
        Me.txtCustomer.Location = New System.Drawing.Point(130, 171)
        Me.txtCustomer.MinimumSize = New System.Drawing.Size(50, 20)
        Me.txtCustomer.Name = "txtCustomer"
        Me.txtCustomer.Size = New System.Drawing.Size(148, 20)
        Me.txtCustomer.TabIndex = 14
        '
        'btnCustomer
        '
        Me.btnCustomer.Location = New System.Drawing.Point(3, 168)
        Me.btnCustomer.Name = "btnCustomer"
        Me.btnCustomer.Size = New System.Drawing.Size(121, 23)
        Me.btnCustomer.TabIndex = 13
        Me.btnCustomer.Text = "Customer"
        Me.btnCustomer.UseVisualStyleBackColor = True
        '
        'txtDocRev
        '
        Me.txtDocRev.Location = New System.Drawing.Point(130, 147)
        Me.txtDocRev.MinimumSize = New System.Drawing.Size(50, 20)
        Me.txtDocRev.Name = "txtDocRev"
        Me.txtDocRev.Size = New System.Drawing.Size(148, 20)
        Me.txtDocRev.TabIndex = 12
        '
        'btnDocRev
        '
        Me.btnDocRev.Location = New System.Drawing.Point(3, 144)
        Me.btnDocRev.Name = "btnDocRev"
        Me.btnDocRev.Size = New System.Drawing.Size(121, 23)
        Me.btnDocRev.TabIndex = 11
        Me.btnDocRev.Text = "Document Revision"
        Me.btnDocRev.UseVisualStyleBackColor = True
        '
        'txtDocName
        '
        Me.txtDocName.Location = New System.Drawing.Point(130, 123)
        Me.txtDocName.MinimumSize = New System.Drawing.Size(50, 20)
        Me.txtDocName.Name = "txtDocName"
        Me.txtDocName.Size = New System.Drawing.Size(148, 20)
        Me.txtDocName.TabIndex = 10
        '
        'btnDocName
        '
        Me.btnDocName.Location = New System.Drawing.Point(3, 120)
        Me.btnDocName.Name = "btnDocName"
        Me.btnDocName.Size = New System.Drawing.Size(121, 23)
        Me.btnDocName.TabIndex = 9
        Me.btnDocName.Text = "Document Name"
        Me.btnDocName.UseVisualStyleBackColor = True
        '
        'txtDocNum
        '
        Me.txtDocNum.Location = New System.Drawing.Point(130, 99)
        Me.txtDocNum.MinimumSize = New System.Drawing.Size(50, 20)
        Me.txtDocNum.Name = "txtDocNum"
        Me.txtDocNum.Size = New System.Drawing.Size(148, 20)
        Me.txtDocNum.TabIndex = 8
        '
        'btnDocNum
        '
        Me.btnDocNum.Location = New System.Drawing.Point(3, 96)
        Me.btnDocNum.Name = "btnDocNum"
        Me.btnDocNum.Size = New System.Drawing.Size(121, 23)
        Me.btnDocNum.TabIndex = 7
        Me.btnDocNum.Text = "Document Number"
        Me.btnDocNum.UseVisualStyleBackColor = True
        '
        'txtPartRev
        '
        Me.txtPartRev.Location = New System.Drawing.Point(130, 75)
        Me.txtPartRev.MinimumSize = New System.Drawing.Size(50, 20)
        Me.txtPartRev.Name = "txtPartRev"
        Me.txtPartRev.Size = New System.Drawing.Size(148, 20)
        Me.txtPartRev.TabIndex = 6
        '
        'btnPartRev
        '
        Me.btnPartRev.Location = New System.Drawing.Point(3, 72)
        Me.btnPartRev.Name = "btnPartRev"
        Me.btnPartRev.Size = New System.Drawing.Size(121, 23)
        Me.btnPartRev.TabIndex = 5
        Me.btnPartRev.Text = "Part Revision"
        Me.btnPartRev.UseVisualStyleBackColor = True
        '
        'txtPartName
        '
        Me.txtPartName.Location = New System.Drawing.Point(130, 51)
        Me.txtPartName.MinimumSize = New System.Drawing.Size(50, 20)
        Me.txtPartName.Name = "txtPartName"
        Me.txtPartName.Size = New System.Drawing.Size(148, 20)
        Me.txtPartName.TabIndex = 4
        '
        'btnPartName
        '
        Me.btnPartName.Location = New System.Drawing.Point(3, 48)
        Me.btnPartName.Name = "btnPartName"
        Me.btnPartName.Size = New System.Drawing.Size(121, 23)
        Me.btnPartName.TabIndex = 3
        Me.btnPartName.Text = "Part Name"
        Me.btnPartName.UseVisualStyleBackColor = True
        '
        'txtPartNumber
        '
        Me.txtPartNumber.Location = New System.Drawing.Point(130, 27)
        Me.txtPartNumber.MinimumSize = New System.Drawing.Size(50, 20)
        Me.txtPartNumber.Name = "txtPartNumber"
        Me.txtPartNumber.Size = New System.Drawing.Size(148, 20)
        Me.txtPartNumber.TabIndex = 2
        '
        'btnPartNum
        '
        Me.btnPartNum.Location = New System.Drawing.Point(3, 24)
        Me.btnPartNum.Name = "btnPartNum"
        Me.btnPartNum.Size = New System.Drawing.Size(121, 23)
        Me.btnPartNum.TabIndex = 1
        Me.btnPartNum.Text = "Part Number"
        Me.btnPartNum.UseVisualStyleBackColor = True
        '
        'CharacteristicsTab
        '
        Me.CharacteristicsTab.Controls.Add(Me.tbpSettings)
        Me.CharacteristicsTab.Controls.Add(Me.tbpExtSettings)
        Me.CharacteristicsTab.Controls.Add(Me.TabPage1)
        Me.CharacteristicsTab.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CharacteristicsTab.Location = New System.Drawing.Point(0, 0)
        Me.CharacteristicsTab.Name = "CharacteristicsTab"
        Me.CharacteristicsTab.SelectedIndex = 0
        Me.CharacteristicsTab.Size = New System.Drawing.Size(287, 497)
        Me.CharacteristicsTab.TabIndex = 2
        '
        'tbpSettings
        '
        Me.tbpSettings.Controls.Add(Me.ddpSampleing)
        Me.tbpSettings.Controls.Add(Me.ddpGeneralSettings)
        Me.tbpSettings.Location = New System.Drawing.Point(4, 22)
        Me.tbpSettings.Name = "tbpSettings"
        Me.tbpSettings.Padding = New System.Windows.Forms.Padding(3)
        Me.tbpSettings.Size = New System.Drawing.Size(279, 471)
        Me.tbpSettings.TabIndex = 0
        Me.tbpSettings.Text = "Settings"
        Me.tbpSettings.UseVisualStyleBackColor = True
        '
        'ddpSampleing
        '
        Me.ddpSampleing.AutoCollapseDelay = -1
        Me.ddpSampleing.Controls.Add(Me.Label4)
        Me.ddpSampleing.Controls.Add(Me.Label5)
        Me.ddpSampleing.Controls.Add(Me.txtSampleSize)
        Me.ddpSampleing.Controls.Add(Me.txtLotSize)
        Me.ddpSampleing.EnableHeaderMenu = True
        Me.ddpSampleing.ExpandAnimationSpeed = ScrewTurn.AnimationSpeed.Medium
        Me.ddpSampleing.Expanded = True
        Me.ddpSampleing.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddpSampleing.HeaderHeight = 20
        Me.ddpSampleing.HeaderIconNormal = Nothing
        Me.ddpSampleing.HeaderIconOver = Nothing
        Me.ddpSampleing.HeaderText = "Sampling"
        Me.ddpSampleing.HomeLocation = New System.Drawing.Point(0, 232)
        Me.ddpSampleing.HotTrackStyle = ScrewTurn.HotTrackStyle.Both
        Me.ddpSampleing.Location = New System.Drawing.Point(0, 232)
        Me.ddpSampleing.ManageControls = False
        Me.ddpSampleing.Moveable = False
        Me.ddpSampleing.Name = "ddpSampleing"
        Me.ddpSampleing.RoundedCorners = False
        Me.ddpSampleing.Size = New System.Drawing.Size(280, 77)
        Me.ddpSampleing.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(7, 54)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 13)
        Me.Label4.TabIndex = 16
        Me.Label4.Text = "Sample Size"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(7, 30)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(45, 13)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "Lot Size"
        '
        'txtSampleSize
        '
        Me.txtSampleSize.Location = New System.Drawing.Point(130, 51)
        Me.txtSampleSize.MinimumSize = New System.Drawing.Size(50, 20)
        Me.txtSampleSize.Name = "txtSampleSize"
        Me.txtSampleSize.Size = New System.Drawing.Size(148, 20)
        Me.txtSampleSize.TabIndex = 4
        '
        'txtLotSize
        '
        Me.txtLotSize.Location = New System.Drawing.Point(130, 27)
        Me.txtLotSize.MinimumSize = New System.Drawing.Size(50, 20)
        Me.txtLotSize.Name = "txtLotSize"
        Me.txtLotSize.Size = New System.Drawing.Size(148, 20)
        Me.txtLotSize.TabIndex = 2
        '
        'tbpExtSettings
        '
        Me.tbpExtSettings.Controls.Add(Me.ddpExtractionVariables)
        Me.tbpExtSettings.Controls.Add(Me.ddpHoleCallout)
        Me.tbpExtSettings.Controls.Add(Me.ddpNotes)
        Me.tbpExtSettings.Controls.Add(Me.ddpDimensions)
        Me.tbpExtSettings.Location = New System.Drawing.Point(4, 22)
        Me.tbpExtSettings.Name = "tbpExtSettings"
        Me.tbpExtSettings.Padding = New System.Windows.Forms.Padding(3)
        Me.tbpExtSettings.Size = New System.Drawing.Size(279, 471)
        Me.tbpExtSettings.TabIndex = 1
        Me.tbpExtSettings.Text = "Extraction Settings"
        Me.tbpExtSettings.UseVisualStyleBackColor = True
        '
        'ddpHoleCallout
        '
        Me.ddpHoleCallout.AutoCollapseDelay = -1
        Me.ddpHoleCallout.Controls.Add(Me.CheckBox8)
        Me.ddpHoleCallout.Controls.Add(Me.CheckBox5)
        Me.ddpHoleCallout.Controls.Add(Me.rtbHoleCallout)
        Me.ddpHoleCallout.Controls.Add(Me.CheckBox7)
        Me.ddpHoleCallout.EnableHeaderMenu = True
        Me.ddpHoleCallout.ExpandAnimationSpeed = ScrewTurn.AnimationSpeed.Medium
        Me.ddpHoleCallout.Expanded = True
        Me.ddpHoleCallout.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddpHoleCallout.HeaderHeight = 20
        Me.ddpHoleCallout.HeaderIconNormal = Nothing
        Me.ddpHoleCallout.HeaderIconOver = Nothing
        Me.ddpHoleCallout.HeaderText = "Hole Callout"
        Me.ddpHoleCallout.HomeLocation = New System.Drawing.Point(0, 218)
        Me.ddpHoleCallout.HotTrackStyle = ScrewTurn.HotTrackStyle.Both
        Me.ddpHoleCallout.Location = New System.Drawing.Point(0, 218)
        Me.ddpHoleCallout.ManageControls = False
        Me.ddpHoleCallout.Moveable = False
        Me.ddpHoleCallout.Name = "ddpHoleCallout"
        Me.ddpHoleCallout.RoundedCorners = False
        Me.ddpHoleCallout.Size = New System.Drawing.Size(279, 135)
        Me.ddpHoleCallout.TabIndex = 2
        '
        'CheckBox5
        '
        Me.CheckBox5.AutoSize = True
        Me.CheckBox5.Location = New System.Drawing.Point(3, 115)
        Me.CheckBox5.Name = "CheckBox5"
        Me.CheckBox5.Size = New System.Drawing.Size(71, 17)
        Me.CheckBox5.TabIndex = 5
        Me.CheckBox5.Text = "Use GDT"
        Me.CheckBox5.UseVisualStyleBackColor = True
        '
        'rtbHoleCallout
        '
        Me.rtbHoleCallout.Enabled = False
        Me.rtbHoleCallout.Location = New System.Drawing.Point(3, 70)
        Me.rtbHoleCallout.Name = "rtbHoleCallout"
        Me.rtbHoleCallout.Size = New System.Drawing.Size(267, 44)
        Me.rtbHoleCallout.TabIndex = 2
        Me.rtbHoleCallout.Text = ""
        '
        'CheckBox7
        '
        Me.CheckBox7.AutoSize = True
        Me.CheckBox7.Checked = True
        Me.CheckBox7.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox7.Location = New System.Drawing.Point(6, 47)
        Me.CheckBox7.Name = "CheckBox7"
        Me.CheckBox7.Size = New System.Drawing.Size(80, 17)
        Me.CheckBox7.TabIndex = 1
        Me.CheckBox7.Text = "Use Callout"
        Me.CheckBox7.UseVisualStyleBackColor = True
        '
        'ddpNotes
        '
        Me.ddpNotes.AutoCollapseDelay = -1
        Me.ddpNotes.Controls.Add(Me.CheckBox4)
        Me.ddpNotes.Controls.Add(Me.rtbNotes)
        Me.ddpNotes.Controls.Add(Me.CheckBox6)
        Me.ddpNotes.EnableHeaderMenu = True
        Me.ddpNotes.ExpandAnimationSpeed = ScrewTurn.AnimationSpeed.Medium
        Me.ddpNotes.Expanded = True
        Me.ddpNotes.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddpNotes.HeaderHeight = 20
        Me.ddpNotes.HeaderIconNormal = Nothing
        Me.ddpNotes.HeaderIconOver = Nothing
        Me.ddpNotes.HeaderText = "Notes"
        Me.ddpNotes.HomeLocation = New System.Drawing.Point(0, 82)
        Me.ddpNotes.HotTrackStyle = ScrewTurn.HotTrackStyle.Both
        Me.ddpNotes.Location = New System.Drawing.Point(0, 82)
        Me.ddpNotes.ManageControls = False
        Me.ddpNotes.Moveable = False
        Me.ddpNotes.Name = "ddpNotes"
        Me.ddpNotes.RoundedCorners = False
        Me.ddpNotes.Size = New System.Drawing.Size(279, 130)
        Me.ddpNotes.TabIndex = 1
        '
        'CheckBox4
        '
        Me.CheckBox4.AutoSize = True
        Me.CheckBox4.Location = New System.Drawing.Point(3, 87)
        Me.CheckBox4.Name = "CheckBox4"
        Me.CheckBox4.Size = New System.Drawing.Size(71, 17)
        Me.CheckBox4.TabIndex = 5
        Me.CheckBox4.Text = "Use GDT"
        Me.CheckBox4.UseVisualStyleBackColor = True
        '
        'rtbNotes
        '
        Me.rtbNotes.Enabled = False
        Me.rtbNotes.Location = New System.Drawing.Point(3, 42)
        Me.rtbNotes.Name = "rtbNotes"
        Me.rtbNotes.Size = New System.Drawing.Size(267, 44)
        Me.rtbNotes.TabIndex = 2
        Me.rtbNotes.Text = ""
        '
        'CheckBox6
        '
        Me.CheckBox6.AutoSize = True
        Me.CheckBox6.Checked = True
        Me.CheckBox6.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox6.Location = New System.Drawing.Point(3, 25)
        Me.CheckBox6.Name = "CheckBox6"
        Me.CheckBox6.Size = New System.Drawing.Size(95, 17)
        Me.CheckBox6.TabIndex = 1
        Me.CheckBox6.Text = "Use Note Text"
        Me.CheckBox6.UseVisualStyleBackColor = True
        '
        'ddpDimensions
        '
        Me.ddpDimensions.AutoCollapseDelay = -1
        Me.ddpDimensions.Controls.Add(Me.CheckBox1)
        Me.ddpDimensions.Controls.Add(Me.CheckBox3)
        Me.ddpDimensions.Controls.Add(Me.CheckBox2)
        Me.ddpDimensions.EnableHeaderMenu = True
        Me.ddpDimensions.ExpandAnimationSpeed = ScrewTurn.AnimationSpeed.Medium
        Me.ddpDimensions.Expanded = True
        Me.ddpDimensions.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddpDimensions.HeaderHeight = 20
        Me.ddpDimensions.HeaderIconNormal = Nothing
        Me.ddpDimensions.HeaderIconOver = Nothing
        Me.ddpDimensions.HeaderText = "Dimensions"
        Me.ddpDimensions.HomeLocation = New System.Drawing.Point(0, 3)
        Me.ddpDimensions.HotTrackStyle = ScrewTurn.HotTrackStyle.Both
        Me.ddpDimensions.Location = New System.Drawing.Point(0, 3)
        Me.ddpDimensions.ManageControls = False
        Me.ddpDimensions.Moveable = False
        Me.ddpDimensions.Name = "ddpDimensions"
        Me.ddpDimensions.RoundedCorners = False
        Me.ddpDimensions.Size = New System.Drawing.Size(279, 73)
        Me.ddpDimensions.TabIndex = 0
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(3, 52)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(71, 17)
        Me.CheckBox1.TabIndex = 4
        Me.CheckBox1.Text = "Use GDT"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'CheckBox3
        '
        Me.CheckBox3.AutoSize = True
        Me.CheckBox3.Location = New System.Drawing.Point(3, 37)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(113, 17)
        Me.CheckBox3.TabIndex = 3
        Me.CheckBox3.Text = "Overridden Values"
        Me.CheckBox3.UseVisualStyleBackColor = True
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Location = New System.Drawing.Point(3, 22)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(104, 17)
        Me.CheckBox2.TabIndex = 2
        Me.CheckBox2.Text = "Secondary Units"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.ddpTolerance)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(279, 471)
        Me.TabPage1.TabIndex = 2
        Me.TabPage1.Text = "Tolerance Settings"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'ddpTolerance
        '
        Me.ddpTolerance.AutoCollapseDelay = -1
        Me.ddpTolerance.Controls.Add(Me.ToleranceTab)
        Me.ddpTolerance.Controls.Add(Me.gpbUnits)
        Me.ddpTolerance.Controls.Add(Me.gpbType)
        Me.ddpTolerance.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ddpTolerance.EnableHeaderMenu = True
        Me.ddpTolerance.ExpandAnimationSpeed = ScrewTurn.AnimationSpeed.Medium
        Me.ddpTolerance.Expanded = True
        Me.ddpTolerance.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddpTolerance.HeaderHeight = 20
        Me.ddpTolerance.HeaderIconNormal = Nothing
        Me.ddpTolerance.HeaderIconOver = Nothing
        Me.ddpTolerance.HeaderText = "Tolerance Settings"
        Me.ddpTolerance.HomeLocation = New System.Drawing.Point(3, 3)
        Me.ddpTolerance.HotTrackStyle = ScrewTurn.HotTrackStyle.Both
        Me.ddpTolerance.Location = New System.Drawing.Point(3, 3)
        Me.ddpTolerance.ManageControls = False
        Me.ddpTolerance.Moveable = False
        Me.ddpTolerance.Name = "ddpTolerance"
        Me.ddpTolerance.RoundedCorners = False
        Me.ddpTolerance.Size = New System.Drawing.Size(273, 465)
        Me.ddpTolerance.TabIndex = 1
        '
        'ToleranceTab
        '
        Me.ToleranceTab.Controls.Add(Me.TabPage2)
        Me.ToleranceTab.Controls.Add(Me.TabPage3)
        Me.ToleranceTab.Location = New System.Drawing.Point(3, 184)
        Me.ToleranceTab.Name = "ToleranceTab"
        Me.ToleranceTab.SelectedIndex = 0
        Me.ToleranceTab.Size = New System.Drawing.Size(270, 190)
        Me.ToleranceTab.TabIndex = 3
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.dgvLTolerance)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(262, 164)
        Me.TabPage2.TabIndex = 0
        Me.TabPage2.Text = "Linear"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'dgvLTolerance
        '
        Me.dgvLTolerance.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvLTolerance.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.LinPrecision, Me.LinUL, Me.LinLL, Me.LinUTol, Me.LinLTol})
        Me.dgvLTolerance.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvLTolerance.Location = New System.Drawing.Point(3, 3)
        Me.dgvLTolerance.Name = "dgvLTolerance"
        Me.dgvLTolerance.RowHeadersVisible = False
        Me.dgvLTolerance.Size = New System.Drawing.Size(256, 158)
        Me.dgvLTolerance.TabIndex = 1
        '
        'LinPrecision
        '
        Me.LinPrecision.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.LinPrecision.HeaderText = "Precision"
        Me.LinPrecision.Name = "LinPrecision"
        Me.LinPrecision.ReadOnly = True
        '
        'LinUL
        '
        Me.LinUL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.LinUL.HeaderText = "Upper Limit"
        Me.LinUL.Name = "LinUL"
        '
        'LinLL
        '
        Me.LinLL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.LinLL.HeaderText = "Lower Limit"
        Me.LinLL.Name = "LinLL"
        '
        'LinUTol
        '
        Me.LinUTol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.LinUTol.HeaderText = "+Tolerance"
        Me.LinUTol.Name = "LinUTol"
        '
        'LinLTol
        '
        Me.LinLTol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.LinLTol.HeaderText = "-Tolerance"
        Me.LinLTol.Name = "LinLTol"
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.dgvAngTolerance)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(262, 164)
        Me.TabPage3.TabIndex = 1
        Me.TabPage3.Text = "Angular"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'dgvAngTolerance
        '
        Me.dgvAngTolerance.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvAngTolerance.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Precision, Me.AngUL, Me.AngLL, Me.AngUTol, Me.AngLTol})
        Me.dgvAngTolerance.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvAngTolerance.Location = New System.Drawing.Point(3, 3)
        Me.dgvAngTolerance.Name = "dgvAngTolerance"
        Me.dgvAngTolerance.RowHeadersVisible = False
        Me.dgvAngTolerance.Size = New System.Drawing.Size(256, 158)
        Me.dgvAngTolerance.TabIndex = 0
        '
        'Precision
        '
        Me.Precision.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Precision.HeaderText = "Precision"
        Me.Precision.Name = "Precision"
        Me.Precision.ReadOnly = True
        '
        'AngUL
        '
        Me.AngUL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.AngUL.HeaderText = "Upper Limit"
        Me.AngUL.Name = "AngUL"
        '
        'AngLL
        '
        Me.AngLL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.AngLL.HeaderText = "Lower Limit"
        Me.AngLL.Name = "AngLL"
        '
        'AngUTol
        '
        Me.AngUTol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.AngUTol.HeaderText = "+Tolerance"
        Me.AngUTol.Name = "AngUTol"
        '
        'AngLTol
        '
        Me.AngLTol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.AngLTol.HeaderText = "-Tolerance"
        Me.AngLTol.Name = "AngLTol"
        '
        'gpbUnits
        '
        Me.gpbUnits.Controls.Add(Me.Label3)
        Me.gpbUnits.Controls.Add(Me.Label2)
        Me.gpbUnits.Controls.Add(Me.Label1)
        Me.gpbUnits.Controls.Add(Me.cmbAngle)
        Me.gpbUnits.Controls.Add(Me.cmbSecondary)
        Me.gpbUnits.Controls.Add(Me.cmbPrimary)
        Me.gpbUnits.Location = New System.Drawing.Point(3, 84)
        Me.gpbUnits.Name = "gpbUnits"
        Me.gpbUnits.Size = New System.Drawing.Size(270, 100)
        Me.gpbUnits.TabIndex = 2
        Me.gpbUnits.TabStop = False
        Me.gpbUnits.Text = "Units"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 73)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(43, 13)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Angular"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(3, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 13)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "Secondary"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 13)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Primary"
        '
        'cmbAngle
        '
        Me.cmbAngle.DisplayMember = "Degree"
        Me.cmbAngle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAngle.FormattingEnabled = True
        Me.cmbAngle.Items.AddRange(New Object() {"Degrees", "Radians"})
        Me.cmbAngle.Location = New System.Drawing.Point(71, 70)
        Me.cmbAngle.MinimumSize = New System.Drawing.Size(50, 0)
        Me.cmbAngle.Name = "cmbAngle"
        Me.cmbAngle.Size = New System.Drawing.Size(192, 21)
        Me.cmbAngle.TabIndex = 11
        '
        'cmbSecondary
        '
        Me.cmbSecondary.DisplayMember = "Millimeter"
        Me.cmbSecondary.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSecondary.FormattingEnabled = True
        Me.cmbSecondary.Items.AddRange(New Object() {"Inch", "Foot", "Centimeter", "Millimeter", "Meter", "Micron"})
        Me.cmbSecondary.Location = New System.Drawing.Point(71, 45)
        Me.cmbSecondary.MinimumSize = New System.Drawing.Size(50, 0)
        Me.cmbSecondary.Name = "cmbSecondary"
        Me.cmbSecondary.Size = New System.Drawing.Size(192, 21)
        Me.cmbSecondary.TabIndex = 9
        '
        'cmbPrimary
        '
        Me.cmbPrimary.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPrimary.FormattingEnabled = True
        Me.cmbPrimary.Items.AddRange(New Object() {"Inch", "Foot", "Centimeter", "Millimeter", "Meter", "Micron"})
        Me.cmbPrimary.Location = New System.Drawing.Point(71, 21)
        Me.cmbPrimary.MinimumSize = New System.Drawing.Size(50, 0)
        Me.cmbPrimary.Name = "cmbPrimary"
        Me.cmbPrimary.Size = New System.Drawing.Size(192, 21)
        Me.cmbPrimary.TabIndex = 8
        '
        'gpbType
        '
        Me.gpbType.Controls.Add(Me.RadioButton2)
        Me.gpbType.Controls.Add(Me.RadioButton1)
        Me.gpbType.Location = New System.Drawing.Point(3, 25)
        Me.gpbType.Name = "gpbType"
        Me.gpbType.Size = New System.Drawing.Size(270, 59)
        Me.gpbType.TabIndex = 1
        Me.gpbType.TabStop = False
        Me.gpbType.Text = "Type"
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Location = New System.Drawing.Point(6, 34)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(72, 17)
        Me.RadioButton2.TabIndex = 1
        Me.RadioButton2.TabStop = True
        Me.RadioButton2.Text = "By Range"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Checked = True
        Me.RadioButton1.Location = New System.Drawing.Point(6, 19)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(83, 17)
        Me.RadioButton1.TabIndex = 0
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "By Precision"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'CheckBox8
        '
        Me.CheckBox8.AutoSize = True
        Me.CheckBox8.Checked = True
        Me.CheckBox8.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox8.Location = New System.Drawing.Point(6, 26)
        Me.CheckBox8.Name = "CheckBox8"
        Me.CheckBox8.Size = New System.Drawing.Size(156, 17)
        Me.CheckBox8.TabIndex = 6
        Me.CheckBox8.Text = "Separate Hole Components"
        Me.CheckBox8.UseVisualStyleBackColor = True
        '
        'ddpExtractionVariables
        '
        Me.ddpExtractionVariables.AutoCollapseDelay = -1
        Me.ddpExtractionVariables.Controls.Add(Me.DataGridView1)
        Me.ddpExtractionVariables.EnableHeaderMenu = True
        Me.ddpExtractionVariables.ExpandAnimationSpeed = ScrewTurn.AnimationSpeed.Medium
        Me.ddpExtractionVariables.Expanded = True
        Me.ddpExtractionVariables.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddpExtractionVariables.HeaderHeight = 20
        Me.ddpExtractionVariables.HeaderIconNormal = Nothing
        Me.ddpExtractionVariables.HeaderIconOver = Nothing
        Me.ddpExtractionVariables.HeaderText = "Extraction Variables"
        Me.ddpExtractionVariables.HomeLocation = New System.Drawing.Point(-1, 359)
        Me.ddpExtractionVariables.HotTrackStyle = ScrewTurn.HotTrackStyle.Both
        Me.ddpExtractionVariables.Location = New System.Drawing.Point(-1, 359)
        Me.ddpExtractionVariables.ManageControls = False
        Me.ddpExtractionVariables.Moveable = False
        Me.ddpExtractionVariables.Name = "ddpExtractionVariables"
        Me.ddpExtractionVariables.RoundedCorners = False
        Me.ddpExtractionVariables.Size = New System.Drawing.Size(280, 112)
        Me.ddpExtractionVariables.TabIndex = 5
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Variable, Me.Value})
        Me.DataGridView1.Location = New System.Drawing.Point(3, 21)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.Size = New System.Drawing.Size(278, 88)
        Me.DataGridView1.TabIndex = 1
        '
        'Variable
        '
        Me.Variable.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Variable.HeaderText = "Variable"
        Me.Variable.Name = "Variable"
        Me.Variable.ReadOnly = True
        '
        'Value
        '
        Me.Value.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Value.HeaderText = "Value"
        Me.Value.Name = "Value"
        '
        'Settings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.CharacteristicsTab)
        Me.Name = "Settings"
        Me.Size = New System.Drawing.Size(287, 497)
        Me.ddpGeneralSettings.ResumeLayout(False)
        Me.ddpGeneralSettings.PerformLayout()
        Me.CharacteristicsTab.ResumeLayout(False)
        Me.tbpSettings.ResumeLayout(False)
        Me.ddpSampleing.ResumeLayout(False)
        Me.ddpSampleing.PerformLayout()
        Me.tbpExtSettings.ResumeLayout(False)
        Me.ddpHoleCallout.ResumeLayout(False)
        Me.ddpHoleCallout.PerformLayout()
        Me.ddpNotes.ResumeLayout(False)
        Me.ddpNotes.PerformLayout()
        Me.ddpDimensions.ResumeLayout(False)
        Me.ddpDimensions.PerformLayout()
        Me.TabPage1.ResumeLayout(False)
        Me.ddpTolerance.ResumeLayout(False)
        Me.ToleranceTab.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        CType(Me.dgvLTolerance, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        CType(Me.dgvAngTolerance, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gpbUnits.ResumeLayout(False)
        Me.gpbUnits.PerformLayout()
        Me.gpbType.ResumeLayout(False)
        Me.gpbType.PerformLayout()
        Me.ddpExtractionVariables.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ddpGeneralSettings As ScrewTurn.DropDownPanel
    Friend WithEvents txtJobNumber As Windows.Forms.TextBox
    Friend WithEvents btnJobNumber As Windows.Forms.Button
    Friend WithEvents txtCustomer As Windows.Forms.TextBox
    Friend WithEvents btnCustomer As Windows.Forms.Button
    Friend WithEvents txtDocRev As Windows.Forms.TextBox
    Friend WithEvents btnDocRev As Windows.Forms.Button
    Friend WithEvents txtDocName As Windows.Forms.TextBox
    Friend WithEvents btnDocName As Windows.Forms.Button
    Friend WithEvents txtDocNum As Windows.Forms.TextBox
    Friend WithEvents btnDocNum As Windows.Forms.Button
    Friend WithEvents txtPartRev As Windows.Forms.TextBox
    Friend WithEvents btnPartRev As Windows.Forms.Button
    Friend WithEvents txtPartName As Windows.Forms.TextBox
    Friend WithEvents btnPartName As Windows.Forms.Button
    Friend WithEvents txtPartNumber As Windows.Forms.TextBox
    Friend WithEvents btnPartNum As Windows.Forms.Button
    Friend WithEvents CharacteristicsTab As Windows.Forms.TabControl
    Friend WithEvents tbpSettings As Windows.Forms.TabPage
    Friend WithEvents tbpExtSettings As Windows.Forms.TabPage
    Friend WithEvents ddpNotes As ScrewTurn.DropDownPanel
    Friend WithEvents ddpDimensions As ScrewTurn.DropDownPanel
    Friend WithEvents CheckBox3 As Windows.Forms.CheckBox
    Friend WithEvents CheckBox2 As Windows.Forms.CheckBox
    Friend WithEvents ddpSampleing As ScrewTurn.DropDownPanel
    Friend WithEvents txtSampleSize As Windows.Forms.TextBox
    Friend WithEvents txtLotSize As Windows.Forms.TextBox
    Friend WithEvents ddpHoleCallout As ScrewTurn.DropDownPanel
    Friend WithEvents CheckBox5 As Windows.Forms.CheckBox
    Friend WithEvents rtbHoleCallout As Windows.Forms.RichTextBox
    Friend WithEvents CheckBox7 As Windows.Forms.CheckBox
    Friend WithEvents CheckBox4 As Windows.Forms.CheckBox
    Friend WithEvents rtbNotes As Windows.Forms.RichTextBox
    Friend WithEvents CheckBox6 As Windows.Forms.CheckBox
    Friend WithEvents CheckBox1 As Windows.Forms.CheckBox
    Friend WithEvents TabPage1 As Windows.Forms.TabPage
    Friend WithEvents ddpTolerance As ScrewTurn.DropDownPanel
    Friend WithEvents gpbUnits As Windows.Forms.GroupBox
    Friend WithEvents cmbSecondary As Windows.Forms.ComboBox
    Friend WithEvents cmbPrimary As Windows.Forms.ComboBox
    Friend WithEvents gpbType As Windows.Forms.GroupBox
    Friend WithEvents RadioButton2 As Windows.Forms.RadioButton
    Friend WithEvents RadioButton1 As Windows.Forms.RadioButton
    Friend WithEvents ToleranceTab As Windows.Forms.TabControl
    Friend WithEvents TabPage2 As Windows.Forms.TabPage
    Friend WithEvents TabPage3 As Windows.Forms.TabPage
    Friend WithEvents dgvAngTolerance As Windows.Forms.DataGridView
    Friend WithEvents cmbAngle As Windows.Forms.ComboBox
    Friend WithEvents dgvLTolerance As Windows.Forms.DataGridView
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents LinPrecision As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LinUL As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LinLL As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LinUTol As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LinLTol As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Precision As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AngUL As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AngLL As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AngUTol As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AngLTol As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CheckBox8 As Windows.Forms.CheckBox
    Friend WithEvents ddpExtractionVariables As ScrewTurn.DropDownPanel
    Friend WithEvents DataGridView1 As Windows.Forms.DataGridView
    Friend WithEvents Variable As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Value As Windows.Forms.DataGridViewTextBoxColumn
End Class
