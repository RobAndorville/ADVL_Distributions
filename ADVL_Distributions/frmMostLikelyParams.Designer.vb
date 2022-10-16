<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMostLikelyParams
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
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.dgvParams = New System.Windows.Forms.DataGridView()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtNParams = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtContinuity = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbDistribution = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnOpenSampTable = New System.Windows.Forms.Button()
        Me.cmbColumnName = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cmbTableName = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.dgvCalculations = New System.Windows.Forms.DataGridView()
        Me.btnUpdateData = New System.Windows.Forms.Button()
        Me.txtLogLikelihood = New System.Windows.Forms.TextBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.btnFindParams = New System.Windows.Forms.Button()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dgvParams, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.dgvCalculations, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Location = New System.Drawing.Point(575, 12)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(48, 22)
        Me.btnExit.TabIndex = 8
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
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
        Me.TabControl1.Size = New System.Drawing.Size(611, 477)
        Me.TabControl1.TabIndex = 9
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.GroupBox2)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(603, 451)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Settings"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.dgvParams)
        Me.GroupBox2.Controls.Add(Me.txtDescription)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.txtNParams)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.txtContinuity)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.cmbDistribution)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Location = New System.Drawing.Point(6, 97)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(591, 303)
        Me.GroupBox2.TabIndex = 50
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Distribution:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 133)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(63, 13)
        Me.Label6.TabIndex = 57
        Me.Label6.Text = "Parameters:"
        '
        'dgvParams
        '
        Me.dgvParams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvParams.Location = New System.Drawing.Point(6, 149)
        Me.dgvParams.Name = "dgvParams"
        Me.dgvParams.Size = New System.Drawing.Size(319, 136)
        Me.dgvParams.TabIndex = 56
        '
        'txtDescription
        '
        Me.txtDescription.Location = New System.Drawing.Point(74, 71)
        Me.txtDescription.Multiline = True
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReadOnly = True
        Me.txtDescription.Size = New System.Drawing.Size(511, 59)
        Me.txtDescription.TabIndex = 55
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 74)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(63, 13)
        Me.Label4.TabIndex = 54
        Me.Label4.Text = "Description:"
        '
        'txtNParams
        '
        Me.txtNParams.Location = New System.Drawing.Point(258, 46)
        Me.txtNParams.Name = "txtNParams"
        Me.txtNParams.ReadOnly = True
        Me.txtNParams.Size = New System.Drawing.Size(35, 20)
        Me.txtNParams.TabIndex = 53
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(169, 49)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(83, 13)
        Me.Label3.TabIndex = 52
        Me.Label3.Text = "No. Parameters:"
        '
        'txtContinuity
        '
        Me.txtContinuity.Location = New System.Drawing.Point(74, 46)
        Me.txtContinuity.Name = "txtContinuity"
        Me.txtContinuity.ReadOnly = True
        Me.txtContinuity.Size = New System.Drawing.Size(89, 20)
        Me.txtContinuity.TabIndex = 51
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 49)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 13)
        Me.Label2.TabIndex = 50
        Me.Label2.Text = "Continuity:"
        '
        'cmbDistribution
        '
        Me.cmbDistribution.FormattingEnabled = True
        Me.cmbDistribution.Location = New System.Drawing.Point(74, 19)
        Me.cmbDistribution.Name = "cmbDistribution"
        Me.cmbDistribution.Size = New System.Drawing.Size(219, 21)
        Me.cmbDistribution.TabIndex = 49
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 13)
        Me.Label1.TabIndex = 48
        Me.Label1.Text = "Name:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.btnOpenSampTable)
        Me.GroupBox1.Controls.Add(Me.cmbColumnName)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.cmbTableName)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(591, 85)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Distribution Samples"
        '
        'btnOpenSampTable
        '
        Me.btnOpenSampTable.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOpenSampTable.Location = New System.Drawing.Point(467, 17)
        Me.btnOpenSampTable.Name = "btnOpenSampTable"
        Me.btnOpenSampTable.Size = New System.Drawing.Size(118, 22)
        Me.btnOpenSampTable.TabIndex = 323
        Me.btnOpenSampTable.Text = "Open Sample Table"
        Me.btnOpenSampTable.UseVisualStyleBackColor = True
        '
        'cmbColumnName
        '
        Me.cmbColumnName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbColumnName.FormattingEnabled = True
        Me.cmbColumnName.Location = New System.Drawing.Point(57, 46)
        Me.cmbColumnName.Name = "cmbColumnName"
        Me.cmbColumnName.Size = New System.Drawing.Size(404, 21)
        Me.cmbColumnName.TabIndex = 49
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 49)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(45, 13)
        Me.Label7.TabIndex = 48
        Me.Label7.Text = "Column:"
        '
        'cmbTableName
        '
        Me.cmbTableName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbTableName.FormattingEnabled = True
        Me.cmbTableName.Location = New System.Drawing.Point(57, 19)
        Me.cmbTableName.Name = "cmbTableName"
        Me.cmbTableName.Size = New System.Drawing.Size(404, 21)
        Me.cmbTableName.TabIndex = 47
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 22)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(37, 13)
        Me.Label5.TabIndex = 46
        Me.Label5.Text = "Table:"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.dgvCalculations)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(603, 451)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Data"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'dgvCalculations
        '
        Me.dgvCalculations.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvCalculations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCalculations.Location = New System.Drawing.Point(3, 3)
        Me.dgvCalculations.Name = "dgvCalculations"
        Me.dgvCalculations.Size = New System.Drawing.Size(597, 445)
        Me.dgvCalculations.TabIndex = 2
        '
        'btnUpdateData
        '
        Me.btnUpdateData.Location = New System.Drawing.Point(136, 12)
        Me.btnUpdateData.Name = "btnUpdateData"
        Me.btnUpdateData.Size = New System.Drawing.Size(118, 22)
        Me.btnUpdateData.TabIndex = 324
        Me.btnUpdateData.Text = "Update Data Table"
        Me.btnUpdateData.UseVisualStyleBackColor = True
        '
        'txtLogLikelihood
        '
        Me.txtLogLikelihood.Location = New System.Drawing.Point(345, 12)
        Me.txtLogLikelihood.Name = "txtLogLikelihood"
        Me.txtLogLikelihood.ReadOnly = True
        Me.txtLogLikelihood.Size = New System.Drawing.Size(175, 20)
        Me.txtLogLikelihood.TabIndex = 351
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(260, 15)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(79, 13)
        Me.Label33.TabIndex = 350
        Me.Label33.Text = "Log Likelihood:"
        '
        'btnFindParams
        '
        Me.btnFindParams.Location = New System.Drawing.Point(12, 12)
        Me.btnFindParams.Name = "btnFindParams"
        Me.btnFindParams.Size = New System.Drawing.Size(118, 22)
        Me.btnFindParams.TabIndex = 352
        Me.btnFindParams.Text = "Find Parameters"
        Me.btnFindParams.UseVisualStyleBackColor = True
        '
        'frmMostLikelyParams
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(635, 529)
        Me.Controls.Add(Me.btnFindParams)
        Me.Controls.Add(Me.txtLogLikelihood)
        Me.Controls.Add(Me.Label33)
        Me.Controls.Add(Me.btnUpdateData)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.btnExit)
        Me.Name = "frmMostLikelyParams"
        Me.Text = "Most Likely Parameters"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.dgvParams, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.dgvCalculations, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnExit As Button
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents cmbColumnName As ComboBox
    Friend WithEvents Label7 As Label
    Friend WithEvents cmbTableName As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents btnOpenSampTable As Button
    Friend WithEvents dgvCalculations As DataGridView
    Friend WithEvents cmbDistribution As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents txtNParams As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtContinuity As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents dgvParams As DataGridView
    Friend WithEvents txtDescription As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents btnUpdateData As Button
    Friend WithEvents txtLogLikelihood As TextBox
    Friend WithEvents Label33 As Label
    Friend WithEvents btnFindParams As Button
End Class
