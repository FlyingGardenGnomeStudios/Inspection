﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Value_Table
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
        Me.dgvDimValues = New System.Windows.Forms.DataGridView()
        Me.Balloon = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Ref = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Value = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Units = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Qty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Type = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SubType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UTol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LTol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ULimit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LLimit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FitGrade = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Comments = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgvDimValues, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvDimValues
        '
        Me.dgvDimValues.AllowUserToAddRows = False
        Me.dgvDimValues.AllowUserToOrderColumns = True
        Me.dgvDimValues.AllowUserToResizeRows = False
        Me.dgvDimValues.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Balloon, Me.Ref, Me.Value, Me.Units, Me.Qty, Me.Type, Me.SubType, Me.UTol, Me.LTol, Me.ULimit, Me.LLimit, Me.FitGrade, Me.Comments})
        Me.dgvDimValues.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.dgvDimValues.Location = New System.Drawing.Point(3, 3)
        Me.dgvDimValues.Name = "dgvDimValues"
        Me.dgvDimValues.RowHeadersVisible = False
        Me.dgvDimValues.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvDimValues.Size = New System.Drawing.Size(930, 183)
        Me.dgvDimValues.TabIndex = 5
        '
        'Balloon
        '
        Me.Balloon.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.Balloon.HeaderText = "Balloon"
        Me.Balloon.Name = "Balloon"
        Me.Balloon.Width = 67
        '
        'Ref
        '
        Me.Ref.HeaderText = "Reference"
        Me.Ref.Name = "Ref"
        Me.Ref.ReadOnly = True
        Me.Ref.Visible = False
        '
        'Value
        '
        Me.Value.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.Value.HeaderText = "Value"
        Me.Value.Name = "Value"
        Me.Value.Width = 59
        '
        'Units
        '
        Me.Units.HeaderText = "Units"
        Me.Units.Name = "Units"
        Me.Units.ReadOnly = True
        '
        'Qty
        '
        Me.Qty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.Qty.HeaderText = "QTY"
        Me.Qty.Name = "Qty"
        Me.Qty.Width = 54
        '
        'Type
        '
        Me.Type.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.Type.HeaderText = "Type"
        Me.Type.Name = "Type"
        Me.Type.Width = 56
        '
        'SubType
        '
        Me.SubType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.SubType.HeaderText = "Sub-Type"
        Me.SubType.Name = "SubType"
        Me.SubType.Width = 78
        '
        'UTol
        '
        Me.UTol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.UTol.HeaderText = "Upper Tol"
        Me.UTol.Name = "UTol"
        Me.UTol.Width = 79
        '
        'LTol
        '
        Me.LTol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.LTol.HeaderText = "Lower Tol"
        Me.LTol.Name = "LTol"
        Me.LTol.Width = 79
        '
        'ULimit
        '
        Me.ULimit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.ULimit.HeaderText = "Upper Limit"
        Me.ULimit.Name = "ULimit"
        Me.ULimit.Width = 85
        '
        'LLimit
        '
        Me.LLimit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.LLimit.HeaderText = "Lower Limit"
        Me.LLimit.Name = "LLimit"
        Me.LLimit.Width = 85
        '
        'FitGrade
        '
        Me.FitGrade.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.FitGrade.HeaderText = "Fit Grade"
        Me.FitGrade.Name = "FitGrade"
        Me.FitGrade.Width = 75
        '
        'Comments
        '
        Me.Comments.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Comments.HeaderText = "Comments"
        Me.Comments.Name = "Comments"
        '
        'Value_Table
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.dgvDimValues)
        Me.Name = "Value_Table"
        Me.Size = New System.Drawing.Size(940, 194)
        CType(Me.dgvDimValues, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents dgvDimValues As Windows.Forms.DataGridView
    Friend WithEvents Balloon As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Ref As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Value As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Units As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Qty As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Type As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SubType As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UTol As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LTol As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ULimit As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LLimit As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FitGrade As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Comments As Windows.Forms.DataGridViewTextBoxColumn
End Class
