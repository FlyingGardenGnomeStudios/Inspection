﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
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
        Me.cmsDGVRBC = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.UnlinkToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteRowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InsertRowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
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
        CType(Me.dgvDimValues, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsDGVRBC.SuspendLayout()
        Me.ToleranceTab.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.dgvLTolerance, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.dgvAngTolerance, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvDimValues
        '
        Me.dgvDimValues.AllowUserToAddRows = False
        Me.dgvDimValues.AllowUserToOrderColumns = True
        Me.dgvDimValues.AllowUserToResizeRows = False
        Me.dgvDimValues.BackgroundColor = System.Drawing.SystemColors.ControlDark
        Me.dgvDimValues.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Ref, Me.Balloon, Me.Value, Me.Units, Me.Qty, Me.Type, Me.SubType, Me.UTol, Me.LTol, Me.ULimit, Me.LLimit, Me.FitGrade, Me.Comments})
        Me.dgvDimValues.ContextMenuStrip = Me.cmsDGVRBC
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvDimValues.DefaultCellStyle = DataGridViewCellStyle4
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
        DataGridViewCellStyle3.Font = New System.Drawing.Font("InspectionXpert GDT", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Value.DefaultCellStyle = DataGridViewCellStyle3
        Me.Value.HeaderText = "Value"
        Me.Value.Name = "Value"
        Me.Value.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
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
        'cmsDGVRBC
        '
        Me.cmsDGVRBC.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UnlinkToolStripMenuItem, Me.DeleteRowToolStripMenuItem, Me.InsertRowToolStripMenuItem})
        Me.cmsDGVRBC.Name = "cmsDGVRBC"
        Me.cmsDGVRBC.Size = New System.Drawing.Size(144, 70)
        '
        'UnlinkToolStripMenuItem
        '
        Me.UnlinkToolStripMenuItem.Name = "UnlinkToolStripMenuItem"
        Me.UnlinkToolStripMenuItem.Size = New System.Drawing.Size(143, 22)
        Me.UnlinkToolStripMenuItem.Text = "Unlink"
        '
        'DeleteRowToolStripMenuItem
        '
        Me.DeleteRowToolStripMenuItem.Name = "DeleteRowToolStripMenuItem"
        Me.DeleteRowToolStripMenuItem.Size = New System.Drawing.Size(143, 22)
        Me.DeleteRowToolStripMenuItem.Text = "Remove Row"
        '
        'InsertRowToolStripMenuItem
        '
        Me.InsertRowToolStripMenuItem.Name = "InsertRowToolStripMenuItem"
        Me.InsertRowToolStripMenuItem.Size = New System.Drawing.Size(143, 22)
        Me.InsertRowToolStripMenuItem.Text = "Insert Row"
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
        'Value_Table_SA
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1223, 273)
        Me.Controls.Add(Me.ToleranceTab)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.dgvDimValues)
        Me.Name = "Value_Table_SA"
        Me.Text = "Value_Table_SA"
        CType(Me.dgvDimValues, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsDGVRBC.ResumeLayout(False)
        Me.ToleranceTab.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        CType(Me.dgvLTolerance, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        CType(Me.dgvAngTolerance, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents dgvDimValues As Windows.Forms.DataGridView
    Friend WithEvents Button1 As Windows.Forms.Button
    Friend WithEvents Button2 As Windows.Forms.Button
    Friend WithEvents Button3 As Windows.Forms.Button
    Friend WithEvents Button4 As Windows.Forms.Button
    Friend WithEvents cmsDGVRBC As Windows.Forms.ContextMenuStrip
    Friend WithEvents UnlinkToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteRowToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents InsertRowToolStripMenuItem As Windows.Forms.ToolStripMenuItem
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
    Friend WithEvents ToleranceTab As Windows.Forms.TabControl
    Friend WithEvents TabPage2 As Windows.Forms.TabPage
    Friend WithEvents dgvLTolerance As Windows.Forms.DataGridView
    Friend WithEvents LinPrecision As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LinUL As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LinLL As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LinUTol As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LinLTol As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TabPage3 As Windows.Forms.TabPage
    Friend WithEvents dgvAngTolerance As Windows.Forms.DataGridView
    Friend WithEvents Precision As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AngUL As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AngLL As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AngUTol As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AngLTol As Windows.Forms.DataGridViewTextBoxColumn
End Class
