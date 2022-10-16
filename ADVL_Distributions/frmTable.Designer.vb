<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTable
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
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.dgvResults = New System.Windows.Forms.DataGridView()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.btnUpdateStatistics = New System.Windows.Forms.Button()
        Me.dgvStatistics = New System.Windows.Forms.DataGridView()
        Me.btnFormatHelp = New System.Windows.Forms.Button()
        Me.txtVarFormat = New System.Windows.Forms.TextBox()
        Me.txtStdDevFormat = New System.Windows.Forms.TextBox()
        Me.txtAvgFormat = New System.Windows.Forms.TextBox()
        Me.txtSumFormat = New System.Windows.Forms.TextBox()
        Me.txtMaxFormat = New System.Windows.Forms.TextBox()
        Me.txtMinFormat = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.rbBuiltIn = New System.Windows.Forms.RadioButton()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.rbPopulation = New System.Windows.Forms.RadioButton()
        Me.rbSample = New System.Windows.Forms.RadioButton()
        Me.txtNRows = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.cmbTableList = New System.Windows.Forms.ComboBox()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnOpenSampTable = New System.Windows.Forms.Button()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.dgvResults, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.dgvStatistics, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(13, 40)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(996, 580)
        Me.TabControl1.TabIndex = 321
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.dgvResults)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(988, 554)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Data Values"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'dgvResults
        '
        Me.dgvResults.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvResults.Location = New System.Drawing.Point(3, 3)
        Me.dgvResults.Name = "dgvResults"
        Me.dgvResults.Size = New System.Drawing.Size(982, 548)
        Me.dgvResults.TabIndex = 309
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.btnUpdateStatistics)
        Me.TabPage2.Controls.Add(Me.dgvStatistics)
        Me.TabPage2.Controls.Add(Me.btnFormatHelp)
        Me.TabPage2.Controls.Add(Me.txtVarFormat)
        Me.TabPage2.Controls.Add(Me.txtStdDevFormat)
        Me.TabPage2.Controls.Add(Me.txtAvgFormat)
        Me.TabPage2.Controls.Add(Me.txtSumFormat)
        Me.TabPage2.Controls.Add(Me.txtMaxFormat)
        Me.TabPage2.Controls.Add(Me.txtMinFormat)
        Me.TabPage2.Controls.Add(Me.Label15)
        Me.TabPage2.Controls.Add(Me.rbBuiltIn)
        Me.TabPage2.Controls.Add(Me.Label12)
        Me.TabPage2.Controls.Add(Me.rbPopulation)
        Me.TabPage2.Controls.Add(Me.rbSample)
        Me.TabPage2.Controls.Add(Me.txtNRows)
        Me.TabPage2.Controls.Add(Me.Label5)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(988, 554)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Statistics"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'btnUpdateStatistics
        '
        Me.btnUpdateStatistics.Location = New System.Drawing.Point(459, 8)
        Me.btnUpdateStatistics.Name = "btnUpdateStatistics"
        Me.btnUpdateStatistics.Size = New System.Drawing.Size(59, 22)
        Me.btnUpdateStatistics.TabIndex = 326
        Me.btnUpdateStatistics.Text = "Update"
        Me.btnUpdateStatistics.UseVisualStyleBackColor = True
        '
        'dgvStatistics
        '
        Me.dgvStatistics.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvStatistics.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvStatistics.Location = New System.Drawing.Point(6, 64)
        Me.dgvStatistics.Name = "dgvStatistics"
        Me.dgvStatistics.Size = New System.Drawing.Size(976, 484)
        Me.dgvStatistics.TabIndex = 325
        '
        'btnFormatHelp
        '
        Me.btnFormatHelp.Location = New System.Drawing.Point(618, 36)
        Me.btnFormatHelp.Name = "btnFormatHelp"
        Me.btnFormatHelp.Size = New System.Drawing.Size(21, 22)
        Me.btnFormatHelp.TabIndex = 324
        Me.btnFormatHelp.Text = "?"
        Me.btnFormatHelp.UseVisualStyleBackColor = True
        '
        'txtVarFormat
        '
        Me.txtVarFormat.Location = New System.Drawing.Point(524, 36)
        Me.txtVarFormat.Name = "txtVarFormat"
        Me.txtVarFormat.Size = New System.Drawing.Size(88, 20)
        Me.txtVarFormat.TabIndex = 323
        '
        'txtStdDevFormat
        '
        Me.txtStdDevFormat.Location = New System.Drawing.Point(430, 36)
        Me.txtStdDevFormat.Name = "txtStdDevFormat"
        Me.txtStdDevFormat.Size = New System.Drawing.Size(88, 20)
        Me.txtStdDevFormat.TabIndex = 322
        '
        'txtAvgFormat
        '
        Me.txtAvgFormat.Location = New System.Drawing.Point(336, 36)
        Me.txtAvgFormat.Name = "txtAvgFormat"
        Me.txtAvgFormat.Size = New System.Drawing.Size(88, 20)
        Me.txtAvgFormat.TabIndex = 321
        '
        'txtSumFormat
        '
        Me.txtSumFormat.Location = New System.Drawing.Point(242, 36)
        Me.txtSumFormat.Name = "txtSumFormat"
        Me.txtSumFormat.Size = New System.Drawing.Size(88, 20)
        Me.txtSumFormat.TabIndex = 320
        '
        'txtMaxFormat
        '
        Me.txtMaxFormat.Location = New System.Drawing.Point(148, 36)
        Me.txtMaxFormat.Name = "txtMaxFormat"
        Me.txtMaxFormat.Size = New System.Drawing.Size(88, 20)
        Me.txtMaxFormat.TabIndex = 319
        '
        'txtMinFormat
        '
        Me.txtMinFormat.Location = New System.Drawing.Point(54, 36)
        Me.txtMinFormat.Name = "txtMinFormat"
        Me.txtMinFormat.Size = New System.Drawing.Size(88, 20)
        Me.txtMinFormat.TabIndex = 318
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(9, 39)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(39, 13)
        Me.Label15.TabIndex = 317
        Me.Label15.Text = "Format"
        '
        'rbBuiltIn
        '
        Me.rbBuiltIn.AutoSize = True
        Me.rbBuiltIn.Location = New System.Drawing.Point(396, 13)
        Me.rbBuiltIn.Name = "rbBuiltIn"
        Me.rbBuiltIn.Size = New System.Drawing.Size(56, 17)
        Me.rbBuiltIn.TabIndex = 316
        Me.rbBuiltIn.TabStop = True
        Me.rbBuiltIn.Text = "Built-in"
        Me.rbBuiltIn.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(200, 15)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(43, 13)
        Me.Label12.TabIndex = 315
        Me.Label12.Text = "Data is:"
        '
        'rbPopulation
        '
        Me.rbPopulation.AutoSize = True
        Me.rbPopulation.Location = New System.Drawing.Point(315, 13)
        Me.rbPopulation.Name = "rbPopulation"
        Me.rbPopulation.Size = New System.Drawing.Size(75, 17)
        Me.rbPopulation.TabIndex = 314
        Me.rbPopulation.TabStop = True
        Me.rbPopulation.Text = "Population"
        Me.rbPopulation.UseVisualStyleBackColor = True
        '
        'rbSample
        '
        Me.rbSample.AutoSize = True
        Me.rbSample.Location = New System.Drawing.Point(249, 13)
        Me.rbSample.Name = "rbSample"
        Me.rbSample.Size = New System.Drawing.Size(60, 17)
        Me.rbSample.TabIndex = 313
        Me.rbSample.TabStop = True
        Me.rbSample.Text = "Sample"
        Me.rbSample.UseVisualStyleBackColor = True
        '
        'txtNRows
        '
        Me.txtNRows.Location = New System.Drawing.Point(69, 10)
        Me.txtNRows.Name = "txtNRows"
        Me.txtNRows.ReadOnly = True
        Me.txtNRows.Size = New System.Drawing.Size(125, 20)
        Me.txtNRows.TabIndex = 312
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 13)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(57, 13)
        Me.Label5.TabIndex = 311
        Me.Label5.Text = "No. Rows:"
        '
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(341, 12)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(59, 22)
        Me.btnUpdate.TabIndex = 320
        Me.btnUpdate.Text = "Update"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'cmbTableList
        '
        Me.cmbTableList.FormattingEnabled = True
        Me.cmbTableList.Location = New System.Drawing.Point(84, 13)
        Me.cmbTableList.Name = "cmbTableList"
        Me.cmbTableList.Size = New System.Drawing.Size(251, 21)
        Me.cmbTableList.TabIndex = 319
        '
        'Label56
        '
        Me.Label56.AutoSize = True
        Me.Label56.Location = New System.Drawing.Point(12, 17)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(66, 13)
        Me.Label56.TabIndex = 318
        Me.Label56.Text = "Table name:"
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Location = New System.Drawing.Point(961, 12)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(48, 22)
        Me.btnExit.TabIndex = 317
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnOpenSampTable
        '
        Me.btnOpenSampTable.Location = New System.Drawing.Point(406, 12)
        Me.btnOpenSampTable.Name = "btnOpenSampTable"
        Me.btnOpenSampTable.Size = New System.Drawing.Size(118, 22)
        Me.btnOpenSampTable.TabIndex = 322
        Me.btnOpenSampTable.Text = "Open Sample Table"
        Me.btnOpenSampTable.UseVisualStyleBackColor = True
        '
        'frmTable
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1021, 632)
        Me.Controls.Add(Me.btnOpenSampTable)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.btnUpdate)
        Me.Controls.Add(Me.cmbTableList)
        Me.Controls.Add(Me.Label56)
        Me.Controls.Add(Me.btnExit)
        Me.Name = "frmTable"
        Me.Text = "Table"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.dgvResults, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.dgvStatistics, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents dgvResults As DataGridView
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents btnUpdateStatistics As Button
    Friend WithEvents dgvStatistics As DataGridView
    Friend WithEvents btnFormatHelp As Button
    Friend WithEvents txtVarFormat As TextBox
    Friend WithEvents txtStdDevFormat As TextBox
    Friend WithEvents txtAvgFormat As TextBox
    Friend WithEvents txtSumFormat As TextBox
    Friend WithEvents txtMaxFormat As TextBox
    Friend WithEvents txtMinFormat As TextBox
    Friend WithEvents Label15 As Label
    Friend WithEvents rbBuiltIn As RadioButton
    Friend WithEvents Label12 As Label
    Friend WithEvents rbPopulation As RadioButton
    Friend WithEvents rbSample As RadioButton
    Friend WithEvents txtNRows As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents btnUpdate As Button
    Friend WithEvents cmbTableList As ComboBox
    Friend WithEvents Label56 As Label
    Friend WithEvents btnExit As Button
    Friend WithEvents btnOpenSampTable As Button
End Class
