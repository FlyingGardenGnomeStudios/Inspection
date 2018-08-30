<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Value_Table_SA
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.tgvDimValues = New AdvancedDataGridView.TreeGridView()
        Me.Balloon = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Ref = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Value = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Units = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.QTY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Type = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SubType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UpperTol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LowerTol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UpperLimit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LowerLimit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FitGrade = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Comments = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.tgvDimValues, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        'tgvDimValues
        '
        Me.tgvDimValues.AllowUserToAddRows = False
        Me.tgvDimValues.AllowUserToDeleteRows = False
        Me.tgvDimValues.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Balloon, Me.Ref, Me.Value, Me.Units, Me.QTY, Me.Type, Me.SubType, Me.UpperTol, Me.LowerTol, Me.UpperLimit, Me.LowerLimit, Me.FitGrade, Me.Comments})
        Me.tgvDimValues.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.tgvDimValues.ImageList = Nothing
        Me.tgvDimValues.Location = New System.Drawing.Point(12, 41)
        Me.tgvDimValues.Name = "tgvDimValues"
        Me.tgvDimValues.RowHeadersVisible = False
        Me.tgvDimValues.Size = New System.Drawing.Size(925, 218)
        Me.tgvDimValues.TabIndex = 11
        '
        'Balloon
        '
        Me.Balloon.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Balloon.HeaderText = "Balloon"
        Me.Balloon.Name = "Balloon"
        Me.Balloon.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Ref
        '
        Me.Ref.HeaderText = "Ref"
        Me.Ref.Name = "Ref"
        Me.Ref.ReadOnly = True
        Me.Ref.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Ref.Visible = False
        '
        'Value
        '
        Me.Value.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Value.HeaderText = "Value"
        Me.Value.Name = "Value"
        Me.Value.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Units
        '
        Me.Units.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Units.HeaderText = "Units"
        Me.Units.Name = "Units"
        Me.Units.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Units.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'QTY
        '
        Me.QTY.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.QTY.HeaderText = "Qty"
        Me.QTY.Name = "QTY"
        Me.QTY.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.QTY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Type
        '
        Me.Type.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Type.HeaderText = "Type"
        Me.Type.Name = "Type"
        Me.Type.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Type.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'SubType
        '
        Me.SubType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.SubType.HeaderText = "Sub-Type"
        Me.SubType.Name = "SubType"
        Me.SubType.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.SubType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'UpperTol
        '
        Me.UpperTol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.UpperTol.HeaderText = "Upper Tol"
        Me.UpperTol.Name = "UpperTol"
        Me.UpperTol.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.UpperTol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'LowerTol
        '
        Me.LowerTol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.LowerTol.HeaderText = "Lower Tol"
        Me.LowerTol.Name = "LowerTol"
        Me.LowerTol.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.LowerTol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'UpperLimit
        '
        Me.UpperLimit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.UpperLimit.HeaderText = "Upper Limit"
        Me.UpperLimit.Name = "UpperLimit"
        Me.UpperLimit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'LowerLimit
        '
        Me.LowerLimit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.LowerLimit.HeaderText = "Lower Limit"
        Me.LowerLimit.Name = "LowerLimit"
        Me.LowerLimit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'FitGrade
        '
        Me.FitGrade.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.FitGrade.HeaderText = "Fit Grade"
        Me.FitGrade.Name = "FitGrade"
        Me.FitGrade.ReadOnly = True
        Me.FitGrade.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Comments
        '
        Me.Comments.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Comments.HeaderText = "Comments"
        Me.Comments.Name = "Comments"
        Me.Comments.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Value_Table_SA
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(949, 271)
        Me.Controls.Add(Me.tgvDimValues)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Name = "Value_Table_SA"
        Me.Text = "Value_Table_SA"
        CType(Me.tgvDimValues, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Button1 As Windows.Forms.Button
    Friend WithEvents Button2 As Windows.Forms.Button
    Friend WithEvents Button3 As Windows.Forms.Button
    Friend WithEvents Button4 As Windows.Forms.Button
    Friend WithEvents tgvDimValues As AdvancedDataGridView.TreeGridView
    Friend WithEvents Balloon As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Ref As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Value As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Units As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents QTY As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Type As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SubType As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UpperTol As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LowerTol As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UpperLimit As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LowerLimit As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FitGrade As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Comments As Windows.Forms.DataGridViewTextBoxColumn
End Class
