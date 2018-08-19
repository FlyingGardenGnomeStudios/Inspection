<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Characteristics
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.dgvProperties = New System.Windows.Forms.DataGridView()
        Me.Item = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Value = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgvProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvProperties
        '
        Me.dgvProperties.AllowUserToAddRows = False
        Me.dgvProperties.AllowUserToDeleteRows = False
        Me.dgvProperties.AllowUserToResizeRows = False
        Me.dgvProperties.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvProperties.ColumnHeadersVisible = False
        Me.dgvProperties.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Item, Me.Value})
        Me.dgvProperties.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvProperties.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke
        Me.dgvProperties.Location = New System.Drawing.Point(0, 0)
        Me.dgvProperties.Name = "dgvProperties"
        Me.dgvProperties.RowHeadersVisible = False
        Me.dgvProperties.Size = New System.Drawing.Size(225, 492)
        Me.dgvProperties.TabIndex = 0
        '
        'Item
        '
        Me.Item.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Item.HeaderText = "Item"
        Me.Item.Name = "Item"
        Me.Item.ReadOnly = True
        '
        'Value
        '
        Me.Value.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Value.HeaderText = "Value"
        Me.Value.Name = "Value"
        '
        'Characteristics
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.dgvProperties)
        Me.Name = "Characteristics"
        Me.Size = New System.Drawing.Size(225, 492)
        CType(Me.dgvProperties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents dgvProperties As Windows.Forms.DataGridView
    Friend WithEvents Item As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Value As Windows.Forms.DataGridViewTextBoxColumn
End Class
