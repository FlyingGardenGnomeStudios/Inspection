Imports Inspection
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Value_Table_SA
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Me.components = New System.ComponentModel.Container()
        Me.dgvDimValues = New System.Windows.Forms.DataGridView()
        Me.Ref = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Balloon = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Value = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Units = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.Qty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Type = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.SubType = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.UTol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LTol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ULimit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LLimit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FitGrade = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Comments = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.DefaultPrecisionLinearBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.TolerancesDataSet = New TolerancesDataSet()
        Me.Default_Precision_LinearTableAdapter = New TolerancesDataSetTableAdapters.Default_Precision_LinearTableAdapter()
        Me.ToleranceTab = New System.Windows.Forms.TabControl()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.IDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PrecisionDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TolDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TolDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UnitsDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.IDDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PrecisionDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TolDataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TolDataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DefaultPrecisionAngularBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Default_Precision_AngularTableAdapter = New TolerancesDataSetTableAdapters.Default_Precision_AngularTableAdapter()
        CType(Me.dgvDimValues, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DefaultPrecisionLinearBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TolerancesDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToleranceTab.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DefaultPrecisionAngularBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvDimValues
        '
        Me.dgvDimValues.AllowUserToAddRows = False
        Me.dgvDimValues.AllowUserToOrderColumns = True
        Me.dgvDimValues.AllowUserToResizeRows = False
        Me.dgvDimValues.BackgroundColor = System.Drawing.SystemColors.ControlDark
        Me.dgvDimValues.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Ref, Me.Balloon, Me.Value, Me.Units, Me.Qty, Me.Type, Me.SubType, Me.UTol, Me.LTol, Me.ULimit, Me.LLimit, Me.FitGrade, Me.Comments})
        Me.dgvDimValues.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.dgvDimValues.Location = New System.Drawing.Point(12, 76)
        Me.dgvDimValues.Name = "dgvDimValues"
        Me.dgvDimValues.RowHeadersVisible = False
        Me.dgvDimValues.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvDimValues.Size = New System.Drawing.Size(930, 183)
        Me.dgvDimValues.TabIndex = 6
        '
        'Ref
        '
        Me.Ref.HeaderText = "Reference"
        Me.Ref.Name = "Ref"
        Me.Ref.ReadOnly = True
        Me.Ref.Visible = False
        '
        'Balloon
        '
        Me.Balloon.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Balloon.HeaderText = "Balloon"
        Me.Balloon.Name = "Balloon"
        '
        'Value
        '
        Me.Value.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Value.HeaderText = "Value"
        Me.Value.Name = "Value"
        '
        'Units
        '
        Me.Units.HeaderText = "Units"
        Me.Units.Name = "Units"
        '
        'Qty
        '
        Me.Qty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Qty.HeaderText = "QTY"
        Me.Qty.Name = "Qty"
        Me.Qty.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'Type
        '
        Me.Type.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Type.HeaderText = "Type"
        Me.Type.Items.AddRange(New Object() {"Dimension", "Geometric Tol", "Note", "Other"})
        Me.Type.Name = "Type"
        Me.Type.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Type.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'SubType
        '
        Me.SubType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.SubType.HeaderText = "Sub-Type"
        Me.SubType.Items.AddRange(New Object() {"Linear", "Angular", "Diametral", "Radial"})
        Me.SubType.Name = "SubType"
        Me.SubType.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.SubType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'UTol
        '
        Me.UTol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.UTol.HeaderText = "Upper Tol"
        Me.UTol.Name = "UTol"
        '
        'LTol
        '
        Me.LTol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.LTol.HeaderText = "Lower Tol"
        Me.LTol.Name = "LTol"
        '
        'ULimit
        '
        Me.ULimit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.ULimit.HeaderText = "Upper Limit"
        Me.ULimit.Name = "ULimit"
        '
        'LLimit
        '
        Me.LLimit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.LLimit.HeaderText = "Lower Limit"
        Me.LLimit.Name = "LLimit"
        '
        'FitGrade
        '
        Me.FitGrade.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.FitGrade.HeaderText = "Fit Grade"
        Me.FitGrade.Name = "FitGrade"
        '
        'Comments
        '
        Me.Comments.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Comments.HeaderText = "Comments"
        Me.Comments.Name = "Comments"
        Me.Comments.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(12, 12)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 7
        Me.Button1.Text = "Add"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(148, 11)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 8
        Me.Button2.Text = "Refresh"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(282, 11)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(75, 23)
        Me.Button3.TabIndex = 9
        Me.Button3.Text = "Load"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(455, 11)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(75, 23)
        Me.Button4.TabIndex = 10
        Me.Button4.Text = "Save"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'DefaultPrecisionLinearBindingSource
        '
        Me.DefaultPrecisionLinearBindingSource.DataMember = "Default Precision-Linear"
        Me.DefaultPrecisionLinearBindingSource.DataSource = Me.TolerancesDataSet
        '
        'TolerancesDataSet
        '
        Me.TolerancesDataSet.DataSetName = "TolerancesDataSet"
        Me.TolerancesDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'Default_Precision_LinearTableAdapter
        '
        Me.Default_Precision_LinearTableAdapter.ClearBeforeFill = True
        '
        'ToleranceTab
        '
        Me.ToleranceTab.Controls.Add(Me.TabPage2)
        Me.ToleranceTab.Controls.Add(Me.TabPage3)
        Me.ToleranceTab.Location = New System.Drawing.Point(948, 69)
        Me.ToleranceTab.Name = "ToleranceTab"
        Me.ToleranceTab.SelectedIndex = 0
        Me.ToleranceTab.Size = New System.Drawing.Size(270, 190)
        Me.ToleranceTab.TabIndex = 11
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.DataGridView1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(262, 164)
        Me.TabPage2.TabIndex = 0
        Me.TabPage2.Text = "Linear"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IDDataGridViewTextBoxColumn, Me.PrecisionDataGridViewTextBoxColumn, Me.TolDataGridViewTextBoxColumn, Me.TolDataGridViewTextBoxColumn1, Me.UnitsDataGridViewTextBoxColumn})
        Me.DataGridView1.DataSource = Me.DefaultPrecisionLinearBindingSource
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(3, 3)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(256, 158)
        Me.DataGridView1.TabIndex = 0
        '
        'IDDataGridViewTextBoxColumn
        '
        Me.IDDataGridViewTextBoxColumn.DataPropertyName = "ID"
        Me.IDDataGridViewTextBoxColumn.HeaderText = "ID"
        Me.IDDataGridViewTextBoxColumn.Name = "IDDataGridViewTextBoxColumn"
        '
        'PrecisionDataGridViewTextBoxColumn
        '
        Me.PrecisionDataGridViewTextBoxColumn.DataPropertyName = "Precision"
        Me.PrecisionDataGridViewTextBoxColumn.HeaderText = "Precision"
        Me.PrecisionDataGridViewTextBoxColumn.Name = "PrecisionDataGridViewTextBoxColumn"
        '
        'TolDataGridViewTextBoxColumn
        '
        Me.TolDataGridViewTextBoxColumn.DataPropertyName = "+Tol"
        Me.TolDataGridViewTextBoxColumn.HeaderText = "+Tol"
        Me.TolDataGridViewTextBoxColumn.Name = "TolDataGridViewTextBoxColumn"
        '
        'TolDataGridViewTextBoxColumn1
        '
        Me.TolDataGridViewTextBoxColumn1.DataPropertyName = "-Tol"
        Me.TolDataGridViewTextBoxColumn1.HeaderText = "-Tol"
        Me.TolDataGridViewTextBoxColumn1.Name = "TolDataGridViewTextBoxColumn1"
        '
        'UnitsDataGridViewTextBoxColumn
        '
        Me.UnitsDataGridViewTextBoxColumn.DataPropertyName = "Units"
        Me.UnitsDataGridViewTextBoxColumn.HeaderText = "Units"
        Me.UnitsDataGridViewTextBoxColumn.Name = "UnitsDataGridViewTextBoxColumn"
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.DataGridView2)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(262, 164)
        Me.TabPage3.TabIndex = 1
        Me.TabPage3.Text = "Angular"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'DataGridView2
        '
        Me.DataGridView2.AutoGenerateColumns = False
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IDDataGridViewTextBoxColumn1, Me.PrecisionDataGridViewTextBoxColumn1, Me.TolDataGridViewTextBoxColumn2, Me.TolDataGridViewTextBoxColumn3})
        Me.DataGridView2.DataSource = Me.DefaultPrecisionAngularBindingSource
        Me.DataGridView2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView2.Location = New System.Drawing.Point(3, 3)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.Size = New System.Drawing.Size(256, 158)
        Me.DataGridView2.TabIndex = 0
        '
        'IDDataGridViewTextBoxColumn1
        '
        Me.IDDataGridViewTextBoxColumn1.DataPropertyName = "ID"
        Me.IDDataGridViewTextBoxColumn1.HeaderText = "ID"
        Me.IDDataGridViewTextBoxColumn1.Name = "IDDataGridViewTextBoxColumn1"
        '
        'PrecisionDataGridViewTextBoxColumn1
        '
        Me.PrecisionDataGridViewTextBoxColumn1.DataPropertyName = "Precision"
        Me.PrecisionDataGridViewTextBoxColumn1.HeaderText = "Precision"
        Me.PrecisionDataGridViewTextBoxColumn1.Name = "PrecisionDataGridViewTextBoxColumn1"
        '
        'TolDataGridViewTextBoxColumn2
        '
        Me.TolDataGridViewTextBoxColumn2.DataPropertyName = "+Tol"
        Me.TolDataGridViewTextBoxColumn2.HeaderText = "+Tol"
        Me.TolDataGridViewTextBoxColumn2.Name = "TolDataGridViewTextBoxColumn2"
        '
        'TolDataGridViewTextBoxColumn3
        '
        Me.TolDataGridViewTextBoxColumn3.DataPropertyName = "-Tol"
        Me.TolDataGridViewTextBoxColumn3.HeaderText = "-Tol"
        Me.TolDataGridViewTextBoxColumn3.Name = "TolDataGridViewTextBoxColumn3"
        '
        'DefaultPrecisionAngularBindingSource
        '
        Me.DefaultPrecisionAngularBindingSource.DataMember = "Default Precision-Angular"
        Me.DefaultPrecisionAngularBindingSource.DataSource = Me.TolerancesDataSet
        '
        'Default_Precision_AngularTableAdapter
        '
        Me.Default_Precision_AngularTableAdapter.ClearBeforeFill = True
        '
        'Value_Table_SA
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1274, 271)
        Me.Controls.Add(Me.ToleranceTab)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.dgvDimValues)
        Me.Name = "Value_Table_SA"
        Me.Text = "Value_Table_SA"
        CType(Me.dgvDimValues, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DefaultPrecisionLinearBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TolerancesDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToleranceTab.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DefaultPrecisionAngularBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents dgvDimValues As Windows.Forms.DataGridView
    Friend WithEvents Button1 As Windows.Forms.Button
    Friend WithEvents Button2 As Windows.Forms.Button
    Friend WithEvents Button3 As Windows.Forms.Button
    Friend WithEvents Button4 As Windows.Forms.Button
    Friend WithEvents Ref As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Balloon As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Value As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Units As Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents Qty As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Type As Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents SubType As Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents UTol As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LTol As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ULimit As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LLimit As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FitGrade As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Comments As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TolerancesDataSet As TolerancesDataSet
    Friend WithEvents DefaultPrecisionLinearBindingSource As Windows.Forms.BindingSource
    Friend WithEvents Default_Precision_LinearTableAdapter As TolerancesDataSetTableAdapters.Default_Precision_LinearTableAdapter
    Friend WithEvents ToleranceTab As Windows.Forms.TabControl
    Friend WithEvents TabPage2 As Windows.Forms.TabPage
    Friend WithEvents DataGridView1 As Windows.Forms.DataGridView
    Friend WithEvents IDDataGridViewTextBoxColumn As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PrecisionDataGridViewTextBoxColumn As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TolDataGridViewTextBoxColumn As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TolDataGridViewTextBoxColumn1 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UnitsDataGridViewTextBoxColumn As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TabPage3 As Windows.Forms.TabPage
    Friend WithEvents DataGridView2 As Windows.Forms.DataGridView
    Friend WithEvents DefaultPrecisionAngularBindingSource As Windows.Forms.BindingSource
    Friend WithEvents Default_Precision_AngularTableAdapter As TolerancesDataSetTableAdapters.Default_Precision_AngularTableAdapter
    Friend WithEvents IDDataGridViewTextBoxColumn1 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PrecisionDataGridViewTextBoxColumn1 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TolDataGridViewTextBoxColumn2 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TolDataGridViewTextBoxColumn3 As Windows.Forms.DataGridViewTextBoxColumn
End Class
