<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDistribAnalysis
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
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtModelDescr = New System.Windows.Forms.TextBox()
        Me.txtRVUnitsAbbrev = New System.Windows.Forms.TextBox()
        Me.Label70 = New System.Windows.Forms.Label()
        Me.txtRVUnits = New System.Windows.Forms.TextBox()
        Me.Label68 = New System.Windows.Forms.Label()
        Me.txtContinuity = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cmbModel = New System.Windows.Forms.ComboBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtDistribParams = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtDistribType = New System.Windows.Forms.TextBox()
        Me.cmbRandVar = New System.Windows.Forms.ComboBox()
        Me.txtDescr = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtEntropy = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtSkewness = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtMedian = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtMode = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtStdDev = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtMean = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dgvProbabilities = New System.Windows.Forms.DataGridView()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtTotalPopulation = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.txtPopFormat = New System.Windows.Forms.TextBox()
        Me.btnFormatHelp = New System.Windows.Forms.Button()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtProbFormat = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtProbPctFormat = New System.Windows.Forms.TextBox()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgvProbabilities, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Location = New System.Drawing.Point(893, 12)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(48, 22)
        Me.btnExit.TabIndex = 8
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.Label17)
        Me.GroupBox3.Controls.Add(Me.txtModelDescr)
        Me.GroupBox3.Controls.Add(Me.txtRVUnitsAbbrev)
        Me.GroupBox3.Controls.Add(Me.Label70)
        Me.GroupBox3.Controls.Add(Me.txtRVUnits)
        Me.GroupBox3.Controls.Add(Me.Label68)
        Me.GroupBox3.Controls.Add(Me.txtContinuity)
        Me.GroupBox3.Controls.Add(Me.Label12)
        Me.GroupBox3.Controls.Add(Me.cmbModel)
        Me.GroupBox3.Controls.Add(Me.Label23)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.txtDistribParams)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.txtDistribType)
        Me.GroupBox3.Controls.Add(Me.cmbRandVar)
        Me.GroupBox3.Controls.Add(Me.txtDescr)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 40)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(929, 272)
        Me.GroupBox3.TabIndex = 42
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Distribution Model:"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(6, 49)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(63, 13)
        Me.Label17.TabIndex = 298
        Me.Label17.Text = "Description:"
        '
        'txtModelDescr
        '
        Me.txtModelDescr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtModelDescr.Location = New System.Drawing.Point(75, 46)
        Me.txtModelDescr.Multiline = True
        Me.txtModelDescr.Name = "txtModelDescr"
        Me.txtModelDescr.Size = New System.Drawing.Size(847, 52)
        Me.txtModelDescr.TabIndex = 297
        '
        'txtRVUnitsAbbrev
        '
        Me.txtRVUnitsAbbrev.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRVUnitsAbbrev.Location = New System.Drawing.Point(790, 131)
        Me.txtRVUnitsAbbrev.Name = "txtRVUnitsAbbrev"
        Me.txtRVUnitsAbbrev.Size = New System.Drawing.Size(133, 20)
        Me.txtRVUnitsAbbrev.TabIndex = 296
        '
        'Label70
        '
        Me.Label70.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label70.AutoSize = True
        Me.Label70.Location = New System.Drawing.Point(740, 134)
        Me.Label70.Name = "Label70"
        Me.Label70.Size = New System.Drawing.Size(44, 13)
        Me.Label70.TabIndex = 295
        Me.Label70.Text = "Abbrev:"
        '
        'txtRVUnits
        '
        Me.txtRVUnits.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRVUnits.Location = New System.Drawing.Point(104, 131)
        Me.txtRVUnits.Name = "txtRVUnits"
        Me.txtRVUnits.Size = New System.Drawing.Size(630, 20)
        Me.txtRVUnits.TabIndex = 294
        '
        'Label68
        '
        Me.Label68.AutoSize = True
        Me.Label68.Location = New System.Drawing.Point(64, 134)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(34, 13)
        Me.Label68.TabIndex = 293
        Me.Label68.Text = "Units:"
        '
        'txtContinuity
        '
        Me.txtContinuity.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtContinuity.Location = New System.Drawing.Point(339, 216)
        Me.txtContinuity.Name = "txtContinuity"
        Me.txtContinuity.Size = New System.Drawing.Size(585, 20)
        Me.txtContinuity.TabIndex = 39
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(277, 219)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(56, 13)
        Me.Label12.TabIndex = 40
        Me.Label12.Text = "Continuity:"
        '
        'cmbModel
        '
        Me.cmbModel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbModel.FormattingEnabled = True
        Me.cmbModel.Location = New System.Drawing.Point(76, 19)
        Me.cmbModel.Name = "cmbModel"
        Me.cmbModel.Size = New System.Drawing.Size(847, 21)
        Me.cmbModel.TabIndex = 38
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(29, 22)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(39, 13)
        Me.Label23.TabIndex = 26
        Me.Label23.Text = "Model:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 109)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(90, 13)
        Me.Label1.TabIndex = 27
        Me.Label1.Text = "Random variable:"
        '
        'txtDistribParams
        '
        Me.txtDistribParams.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDistribParams.Location = New System.Drawing.Point(77, 242)
        Me.txtDistribParams.Name = "txtDistribParams"
        Me.txtDistribParams.Size = New System.Drawing.Size(847, 20)
        Me.txtDistribParams.TabIndex = 32
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 160)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 13)
        Me.Label2.TabIndex = 28
        Me.Label2.Text = "Description:"
        '
        'txtDistribType
        '
        Me.txtDistribType.Location = New System.Drawing.Point(77, 216)
        Me.txtDistribType.Name = "txtDistribType"
        Me.txtDistribType.Size = New System.Drawing.Size(194, 20)
        Me.txtDistribType.TabIndex = 30
        '
        'cmbRandVar
        '
        Me.cmbRandVar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbRandVar.FormattingEnabled = True
        Me.cmbRandVar.Location = New System.Drawing.Point(104, 104)
        Me.cmbRandVar.Name = "cmbRandVar"
        Me.cmbRandVar.Size = New System.Drawing.Size(819, 21)
        Me.cmbRandVar.TabIndex = 37
        '
        'txtDescr
        '
        Me.txtDescr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDescr.Location = New System.Drawing.Point(105, 157)
        Me.txtDescr.Multiline = True
        Me.txtDescr.Name = "txtDescr"
        Me.txtDescr.Size = New System.Drawing.Size(819, 52)
        Me.txtDescr.TabIndex = 29
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(7, 245)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(63, 13)
        Me.Label4.TabIndex = 33
        Me.Label4.Text = "Parameters:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 219)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(62, 13)
        Me.Label3.TabIndex = 31
        Me.Label3.Text = "Distribution:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.txtEntropy)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.txtSkewness)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.txtMedian)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtMode)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtStdDev)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtMean)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 318)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(929, 80)
        Me.GroupBox1.TabIndex = 43
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Properties:"
        '
        'txtEntropy
        '
        Me.txtEntropy.Location = New System.Drawing.Point(560, 45)
        Me.txtEntropy.Name = "txtEntropy"
        Me.txtEntropy.Size = New System.Drawing.Size(160, 20)
        Me.txtEntropy.TabIndex = 42
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(495, 48)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(46, 13)
        Me.Label11.TabIndex = 43
        Me.Label11.Text = "Entropy:"
        '
        'txtSkewness
        '
        Me.txtSkewness.Location = New System.Drawing.Point(560, 19)
        Me.txtSkewness.Name = "txtSkewness"
        Me.txtSkewness.Size = New System.Drawing.Size(160, 20)
        Me.txtSkewness.TabIndex = 40
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(495, 22)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(59, 13)
        Me.Label10.TabIndex = 41
        Me.Label10.Text = "Skewness:"
        '
        'txtMedian
        '
        Me.txtMedian.Location = New System.Drawing.Point(329, 45)
        Me.txtMedian.Name = "txtMedian"
        Me.txtMedian.Size = New System.Drawing.Size(160, 20)
        Me.txtMedian.TabIndex = 38
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(277, 48)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(45, 13)
        Me.Label8.TabIndex = 39
        Me.Label8.Text = "Median:"
        '
        'txtMode
        '
        Me.txtMode.Location = New System.Drawing.Point(329, 19)
        Me.txtMode.Name = "txtMode"
        Me.txtMode.Size = New System.Drawing.Size(160, 20)
        Me.txtMode.TabIndex = 36
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(277, 22)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(37, 13)
        Me.Label7.TabIndex = 37
        Me.Label7.Text = "Mode:"
        '
        'txtStdDev
        '
        Me.txtStdDev.Location = New System.Drawing.Point(111, 45)
        Me.txtStdDev.Name = "txtStdDev"
        Me.txtStdDev.Size = New System.Drawing.Size(160, 20)
        Me.txtStdDev.TabIndex = 34
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 48)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(99, 13)
        Me.Label6.TabIndex = 35
        Me.Label6.Text = "Standard deviation:"
        '
        'txtMean
        '
        Me.txtMean.Location = New System.Drawing.Point(111, 19)
        Me.txtMean.Name = "txtMean"
        Me.txtMean.Size = New System.Drawing.Size(160, 20)
        Me.txtMean.TabIndex = 32
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 22)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(37, 13)
        Me.Label5.TabIndex = 33
        Me.Label5.Text = "Mean:"
        '
        'dgvProbabilities
        '
        Me.dgvProbabilities.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvProbabilities.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvProbabilities.Location = New System.Drawing.Point(12, 430)
        Me.dgvProbabilities.Name = "dgvProbabilities"
        Me.dgvProbabilities.Size = New System.Drawing.Size(929, 255)
        Me.dgvProbabilities.TabIndex = 44
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(12, 414)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(66, 13)
        Me.Label9.TabIndex = 45
        Me.Label9.Text = "Probabilities:"
        '
        'txtTotalPopulation
        '
        Me.txtTotalPopulation.Location = New System.Drawing.Point(185, 404)
        Me.txtTotalPopulation.Name = "txtTotalPopulation"
        Me.txtTotalPopulation.Size = New System.Drawing.Size(141, 20)
        Me.txtTotalPopulation.TabIndex = 46
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(93, 407)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(86, 13)
        Me.Label13.TabIndex = 47
        Me.Label13.Text = "Total population:"
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Location = New System.Drawing.Point(332, 407)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(42, 13)
        Me.Label44.TabIndex = 64
        Me.Label44.Text = "Format:"
        '
        'txtPopFormat
        '
        Me.txtPopFormat.Location = New System.Drawing.Point(446, 404)
        Me.txtPopFormat.Name = "txtPopFormat"
        Me.txtPopFormat.Size = New System.Drawing.Size(63, 20)
        Me.txtPopFormat.TabIndex = 65
        '
        'btnFormatHelp
        '
        Me.btnFormatHelp.Location = New System.Drawing.Point(789, 403)
        Me.btnFormatHelp.Name = "btnFormatHelp"
        Me.btnFormatHelp.Size = New System.Drawing.Size(21, 22)
        Me.btnFormatHelp.TabIndex = 67
        Me.btnFormatHelp.Text = "?"
        Me.btnFormatHelp.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(380, 407)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(60, 13)
        Me.Label14.TabIndex = 68
        Me.Label14.Text = "Population:"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(515, 407)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(58, 13)
        Me.Label15.TabIndex = 70
        Me.Label15.Text = "Probability:"
        '
        'txtProbFormat
        '
        Me.txtProbFormat.Location = New System.Drawing.Point(579, 404)
        Me.txtProbFormat.Name = "txtProbFormat"
        Me.txtProbFormat.Size = New System.Drawing.Size(63, 20)
        Me.txtProbFormat.TabIndex = 69
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(656, 407)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(43, 13)
        Me.Label16.TabIndex = 72
        Me.Label16.Text = "Prob %:"
        '
        'txtProbPctFormat
        '
        Me.txtProbPctFormat.Location = New System.Drawing.Point(720, 404)
        Me.txtProbPctFormat.Name = "txtProbPctFormat"
        Me.txtProbPctFormat.Size = New System.Drawing.Size(63, 20)
        Me.txtProbPctFormat.TabIndex = 71
        '
        'frmDistribAnalysis
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(953, 697)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.txtProbPctFormat)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.txtProbFormat)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.btnFormatHelp)
        Me.Controls.Add(Me.Label44)
        Me.Controls.Add(Me.txtPopFormat)
        Me.Controls.Add(Me.txtTotalPopulation)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.dgvProbabilities)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.btnExit)
        Me.Name = "frmDistribAnalysis"
        Me.Text = "Probability Distribution Analysis"
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgvProbabilities, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnExit As Button
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Label17 As Label
    Friend WithEvents txtModelDescr As TextBox
    Friend WithEvents txtRVUnitsAbbrev As TextBox
    Friend WithEvents Label70 As Label
    Friend WithEvents txtRVUnits As TextBox
    Friend WithEvents Label68 As Label
    Friend WithEvents txtContinuity As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents cmbModel As ComboBox
    Friend WithEvents Label23 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents txtDistribParams As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtDistribType As TextBox
    Friend WithEvents cmbRandVar As ComboBox
    Friend WithEvents txtDescr As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents txtMean As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txtMedian As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents txtMode As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents txtStdDev As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents dgvProbabilities As DataGridView
    Friend WithEvents Label9 As Label
    Friend WithEvents txtEntropy As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents txtSkewness As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents txtTotalPopulation As TextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents Label44 As Label
    Friend WithEvents txtPopFormat As TextBox
    Friend WithEvents btnFormatHelp As Button
    Friend WithEvents Label14 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents txtProbFormat As TextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents txtProbPctFormat As TextBox
End Class
