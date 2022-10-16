<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProbabilityModel
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
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnNewProbModel = New System.Windows.Forms.Button()
        Me.btnOpenProbModel = New System.Windows.Forms.Button()
        Me.btnSaveProbModel = New System.Windows.Forms.Button()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtLabel = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.txtModelName = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtFileName = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Label69 = New System.Windows.Forms.Label()
        Me.txtNotes = New System.Windows.Forms.TextBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.txtItemCount = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnDown = New System.Windows.Forms.Button()
        Me.btnUp = New System.Windows.Forms.Button()
        Me.btnDeleteItem = New System.Windows.Forms.Button()
        Me.btnAddItem = New System.Windows.Forms.Button()
        Me.dgvItems = New System.Windows.Forms.DataGridView()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.dgvItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Location = New System.Drawing.Point(737, 12)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(48, 22)
        Me.btnExit.TabIndex = 8
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnNewProbModel
        '
        Me.btnNewProbModel.Location = New System.Drawing.Point(80, 6)
        Me.btnNewProbModel.Name = "btnNewProbModel"
        Me.btnNewProbModel.Size = New System.Drawing.Size(45, 22)
        Me.btnNewProbModel.TabIndex = 340
        Me.btnNewProbModel.Text = "New"
        Me.btnNewProbModel.UseVisualStyleBackColor = True
        '
        'btnOpenProbModel
        '
        Me.btnOpenProbModel.Location = New System.Drawing.Point(182, 6)
        Me.btnOpenProbModel.Name = "btnOpenProbModel"
        Me.btnOpenProbModel.Size = New System.Drawing.Size(45, 22)
        Me.btnOpenProbModel.TabIndex = 339
        Me.btnOpenProbModel.Text = "Open"
        Me.btnOpenProbModel.UseVisualStyleBackColor = True
        '
        'btnSaveProbModel
        '
        Me.btnSaveProbModel.Location = New System.Drawing.Point(131, 6)
        Me.btnSaveProbModel.Name = "btnSaveProbModel"
        Me.btnSaveProbModel.Size = New System.Drawing.Size(45, 22)
        Me.btnSaveProbModel.TabIndex = 338
        Me.btnSaveProbModel.Text = "Save"
        Me.btnSaveProbModel.UseVisualStyleBackColor = True
        '
        'txtDescription
        '
        Me.txtDescription.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDescription.Location = New System.Drawing.Point(80, 112)
        Me.txtDescription.Multiline = True
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(679, 62)
        Me.txtDescription.TabIndex = 337
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(6, 115)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(63, 13)
        Me.Label24.TabIndex = 336
        Me.Label24.Text = "Description:"
        '
        'txtLabel
        '
        Me.txtLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtLabel.Location = New System.Drawing.Point(80, 86)
        Me.txtLabel.Name = "txtLabel"
        Me.txtLabel.Size = New System.Drawing.Size(679, 20)
        Me.txtLabel.TabIndex = 335
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(6, 89)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(36, 13)
        Me.Label25.TabIndex = 334
        Me.Label25.Text = "Label:"
        '
        'txtModelName
        '
        Me.txtModelName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtModelName.Location = New System.Drawing.Point(80, 60)
        Me.txtModelName.Name = "txtModelName"
        Me.txtModelName.Size = New System.Drawing.Size(679, 20)
        Me.txtModelName.TabIndex = 333
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(6, 63)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(68, 13)
        Me.Label23.TabIndex = 332
        Me.Label23.Text = "Model name:"
        '
        'txtFileName
        '
        Me.txtFileName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtFileName.Location = New System.Drawing.Point(80, 34)
        Me.txtFileName.Name = "txtFileName"
        Me.txtFileName.Size = New System.Drawing.Size(679, 20)
        Me.txtFileName.TabIndex = 331
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(6, 37)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(55, 13)
        Me.Label22.TabIndex = 330
        Me.Label22.Text = "File name:"
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(12, 40)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(773, 402)
        Me.TabControl1.TabIndex = 341
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Label69)
        Me.TabPage1.Controls.Add(Me.txtNotes)
        Me.TabPage1.Controls.Add(Me.btnNewProbModel)
        Me.TabPage1.Controls.Add(Me.Label22)
        Me.TabPage1.Controls.Add(Me.btnOpenProbModel)
        Me.TabPage1.Controls.Add(Me.txtFileName)
        Me.TabPage1.Controls.Add(Me.btnSaveProbModel)
        Me.TabPage1.Controls.Add(Me.Label23)
        Me.TabPage1.Controls.Add(Me.txtDescription)
        Me.TabPage1.Controls.Add(Me.txtModelName)
        Me.TabPage1.Controls.Add(Me.Label24)
        Me.TabPage1.Controls.Add(Me.Label25)
        Me.TabPage1.Controls.Add(Me.txtLabel)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(765, 376)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Summary"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Label69
        '
        Me.Label69.AutoSize = True
        Me.Label69.Location = New System.Drawing.Point(6, 183)
        Me.Label69.Name = "Label69"
        Me.Label69.Size = New System.Drawing.Size(38, 13)
        Me.Label69.TabIndex = 342
        Me.Label69.Text = "Notes:"
        '
        'txtNotes
        '
        Me.txtNotes.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNotes.Location = New System.Drawing.Point(80, 180)
        Me.txtNotes.Multiline = True
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.Size = New System.Drawing.Size(679, 190)
        Me.txtNotes.TabIndex = 341
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.txtItemCount)
        Me.TabPage2.Controls.Add(Me.Label1)
        Me.TabPage2.Controls.Add(Me.btnDown)
        Me.TabPage2.Controls.Add(Me.btnUp)
        Me.TabPage2.Controls.Add(Me.btnDeleteItem)
        Me.TabPage2.Controls.Add(Me.btnAddItem)
        Me.TabPage2.Controls.Add(Me.dgvItems)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(765, 376)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Items"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'txtItemCount
        '
        Me.txtItemCount.Location = New System.Drawing.Point(358, 8)
        Me.txtItemCount.Name = "txtItemCount"
        Me.txtItemCount.ReadOnly = True
        Me.txtItemCount.Size = New System.Drawing.Size(154, 20)
        Me.txtItemCount.TabIndex = 346
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(269, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 13)
        Me.Label1.TabIndex = 345
        Me.Label1.Text = "Number of items"
        '
        'btnDown
        '
        Me.btnDown.Location = New System.Drawing.Point(184, 6)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(79, 22)
        Me.btnDown.TabIndex = 344
        Me.btnDown.Text = "Move Down"
        Me.btnDown.UseVisualStyleBackColor = True
        '
        'btnUp
        '
        Me.btnUp.Location = New System.Drawing.Point(116, 6)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(62, 22)
        Me.btnUp.TabIndex = 343
        Me.btnUp.Text = "Move Up"
        Me.btnUp.UseVisualStyleBackColor = True
        '
        'btnDeleteItem
        '
        Me.btnDeleteItem.Location = New System.Drawing.Point(57, 6)
        Me.btnDeleteItem.Name = "btnDeleteItem"
        Me.btnDeleteItem.Size = New System.Drawing.Size(53, 22)
        Me.btnDeleteItem.TabIndex = 342
        Me.btnDeleteItem.Text = "Delete"
        Me.btnDeleteItem.UseVisualStyleBackColor = True
        '
        'btnAddItem
        '
        Me.btnAddItem.Location = New System.Drawing.Point(6, 6)
        Me.btnAddItem.Name = "btnAddItem"
        Me.btnAddItem.Size = New System.Drawing.Size(45, 22)
        Me.btnAddItem.TabIndex = 341
        Me.btnAddItem.Text = "Add"
        Me.btnAddItem.UseVisualStyleBackColor = True
        '
        'dgvItems
        '
        Me.dgvItems.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvItems.Location = New System.Drawing.Point(6, 34)
        Me.dgvItems.Name = "dgvItems"
        Me.dgvItems.Size = New System.Drawing.Size(753, 336)
        Me.dgvItems.TabIndex = 0
        '
        'frmProbabilityModel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(797, 454)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.btnExit)
        Me.Name = "frmProbabilityModel"
        Me.Text = "Probability Model"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.dgvItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnExit As Button
    Friend WithEvents btnNewProbModel As Button
    Friend WithEvents btnOpenProbModel As Button
    Friend WithEvents btnSaveProbModel As Button
    Friend WithEvents txtDescription As TextBox
    Friend WithEvents Label24 As Label
    Friend WithEvents txtLabel As TextBox
    Friend WithEvents Label25 As Label
    Friend WithEvents txtModelName As TextBox
    Friend WithEvents Label23 As Label
    Friend WithEvents txtFileName As TextBox
    Friend WithEvents Label22 As Label
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents dgvItems As DataGridView
    Friend WithEvents Label69 As Label
    Friend WithEvents txtNotes As TextBox
    Friend WithEvents btnDown As Button
    Friend WithEvents btnUp As Button
    Friend WithEvents btnDeleteItem As Button
    Friend WithEvents btnAddItem As Button
    Friend WithEvents txtItemCount As TextBox
    Friend WithEvents Label1 As Label
End Class
