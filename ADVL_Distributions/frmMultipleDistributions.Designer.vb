<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMultipleDistributions
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
        Me.dgvParams = New System.Windows.Forms.DataGridView()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.txtNParams = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtContinuity = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtDistName = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.txtDefTo = New System.Windows.Forms.TextBox()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.txtDefFrom = New System.Windows.Forms.TextBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.txtSelDistrib = New System.Windows.Forms.TextBox()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.btnPrev = New System.Windows.Forms.Button()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.txtNDistribs = New System.Windows.Forms.TextBox()
        Me.btnGenerate2 = New System.Windows.Forms.Button()
        Me.dgvFields = New System.Windows.Forms.DataGridView()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.btnFormatHelp = New System.Windows.Forms.Button()
        Me.btnApplyFormats = New System.Windows.Forms.Button()
        Me.chkUpdateLabel = New System.Windows.Forms.CheckBox()
        Me.txtSuffix = New System.Windows.Forms.TextBox()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.btnAdjust = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.txtDefLineWidth = New System.Windows.Forms.TextBox()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.txtDefLineColor = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtDefMkrStep = New System.Windows.Forms.TextBox()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.txtDefMkrSize = New System.Windows.Forms.TextBox()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.cmbDefMkrStyle = New System.Windows.Forms.ComboBox()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.txtDefBorderWidth = New System.Windows.Forms.TextBox()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.txtDefBorderColor = New System.Windows.Forms.TextBox()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.txtDefMkrColor = New System.Windows.Forms.TextBox()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.cmbDefMkrFill = New System.Windows.Forms.ComboBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.AddToChart = New System.Windows.Forms.Button()
        CType(Me.dgvParams, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvFields, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Location = New System.Drawing.Point(580, 12)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(48, 22)
        Me.btnExit.TabIndex = 8
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'dgvParams
        '
        Me.dgvParams.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvParams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvParams.Location = New System.Drawing.Point(12, 143)
        Me.dgvParams.Name = "dgvParams"
        Me.dgvParams.Size = New System.Drawing.Size(616, 150)
        Me.dgvParams.TabIndex = 10
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(9, 127)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(63, 13)
        Me.Label15.TabIndex = 9
        Me.Label15.Text = "Parameters:"
        '
        'btnSelect
        '
        Me.btnSelect.Location = New System.Drawing.Point(11, 74)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(47, 22)
        Me.btnSelect.TabIndex = 17
        Me.btnSelect.Text = "Select"
        Me.btnSelect.UseVisualStyleBackColor = True
        '
        'txtNParams
        '
        Me.txtNParams.Location = New System.Drawing.Point(565, 76)
        Me.txtNParams.Name = "txtNParams"
        Me.txtNParams.ReadOnly = True
        Me.txtNParams.Size = New System.Drawing.Size(42, 20)
        Me.txtNParams.TabIndex = 16
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(476, 79)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(83, 13)
        Me.Label14.TabIndex = 15
        Me.Label14.Text = "No. Parameters:"
        '
        'txtContinuity
        '
        Me.txtContinuity.Location = New System.Drawing.Point(351, 76)
        Me.txtContinuity.Name = "txtContinuity"
        Me.txtContinuity.ReadOnly = True
        Me.txtContinuity.Size = New System.Drawing.Size(119, 20)
        Me.txtContinuity.TabIndex = 14
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(289, 79)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(56, 13)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "Continuity:"
        '
        'txtDistName
        '
        Me.txtDistName.Location = New System.Drawing.Point(108, 76)
        Me.txtDistName.Name = "txtDistName"
        Me.txtDistName.ReadOnly = True
        Me.txtDistName.Size = New System.Drawing.Size(175, 20)
        Me.txtDistName.TabIndex = 12
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(64, 79)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(38, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Name:"
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(495, 105)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(104, 13)
        Me.Label36.TabIndex = 245
        Me.Label36.Text = "(blank if unbounded)"
        '
        'txtDefTo
        '
        Me.txtDefTo.Location = New System.Drawing.Point(314, 102)
        Me.txtDefTo.Name = "txtDefTo"
        Me.txtDefTo.ReadOnly = True
        Me.txtDefTo.Size = New System.Drawing.Size(175, 20)
        Me.txtDefTo.TabIndex = 244
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(289, 105)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(19, 13)
        Me.Label35.TabIndex = 243
        Me.Label35.Text = "to:"
        '
        'txtDefFrom
        '
        Me.txtDefFrom.Location = New System.Drawing.Point(108, 102)
        Me.txtDefFrom.Name = "txtDefFrom"
        Me.txtDefFrom.ReadOnly = True
        Me.txtDefFrom.Size = New System.Drawing.Size(175, 20)
        Me.txtDefFrom.TabIndex = 242
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(32, 105)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(70, 13)
        Me.Label31.TabIndex = 241
        Me.Label31.Text = "Defined from:"
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(405, 41)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(39, 22)
        Me.btnDelete.TabIndex = 337
        Me.btnDelete.Text = "Del"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(360, 41)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(39, 22)
        Me.btnAdd.TabIndex = 336
        Me.btnAdd.Text = "Add"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'txtSelDistrib
        '
        Me.txtSelDistrib.Location = New System.Drawing.Point(144, 42)
        Me.txtSelDistrib.Name = "txtSelDistrib"
        Me.txtSelDistrib.Size = New System.Drawing.Size(45, 20)
        Me.txtSelDistrib.TabIndex = 335
        '
        'Label60
        '
        Me.Label60.AutoSize = True
        Me.Label60.Location = New System.Drawing.Point(12, 45)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(113, 13)
        Me.Label60.TabIndex = 334
        Me.Label60.Text = "Secondary Distribution"
        '
        'btnNext
        '
        Me.btnNext.Location = New System.Drawing.Point(315, 41)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(39, 22)
        Me.btnNext.TabIndex = 333
        Me.btnNext.Text = "Next"
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'btnPrev
        '
        Me.btnPrev.Location = New System.Drawing.Point(270, 41)
        Me.btnPrev.Name = "btnPrev"
        Me.btnPrev.Size = New System.Drawing.Size(39, 22)
        Me.btnPrev.TabIndex = 332
        Me.btnPrev.Text = "Prev"
        Me.btnPrev.UseVisualStyleBackColor = True
        '
        'Label61
        '
        Me.Label61.AutoSize = True
        Me.Label61.Location = New System.Drawing.Point(195, 45)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(16, 13)
        Me.Label61.TabIndex = 331
        Me.Label61.Text = "of"
        '
        'txtNDistribs
        '
        Me.txtNDistribs.Location = New System.Drawing.Point(217, 42)
        Me.txtNDistribs.Name = "txtNDistribs"
        Me.txtNDistribs.ReadOnly = True
        Me.txtNDistribs.Size = New System.Drawing.Size(47, 20)
        Me.txtNDistribs.TabIndex = 330
        '
        'btnGenerate2
        '
        Me.btnGenerate2.Location = New System.Drawing.Point(12, 12)
        Me.btnGenerate2.Name = "btnGenerate2"
        Me.btnGenerate2.Size = New System.Drawing.Size(99, 22)
        Me.btnGenerate2.TabIndex = 338
        Me.btnGenerate2.Text = "Generate Data"
        Me.btnGenerate2.UseVisualStyleBackColor = True
        '
        'dgvFields
        '
        Me.dgvFields.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvFields.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvFields.Location = New System.Drawing.Point(12, 424)
        Me.dgvFields.Name = "dgvFields"
        Me.dgvFields.Size = New System.Drawing.Size(616, 192)
        Me.dgvFields.TabIndex = 340
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Location = New System.Drawing.Point(11, 408)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(37, 13)
        Me.Label39.TabIndex = 339
        Me.Label39.Text = "Fields:"
        '
        'btnFormatHelp
        '
        Me.btnFormatHelp.Location = New System.Drawing.Point(116, 622)
        Me.btnFormatHelp.Name = "btnFormatHelp"
        Me.btnFormatHelp.Size = New System.Drawing.Size(99, 22)
        Me.btnFormatHelp.TabIndex = 342
        Me.btnFormatHelp.Text = "Format Help"
        Me.btnFormatHelp.UseVisualStyleBackColor = True
        '
        'btnApplyFormats
        '
        Me.btnApplyFormats.Location = New System.Drawing.Point(11, 622)
        Me.btnApplyFormats.Name = "btnApplyFormats"
        Me.btnApplyFormats.Size = New System.Drawing.Size(99, 22)
        Me.btnApplyFormats.TabIndex = 341
        Me.btnApplyFormats.Text = "Apply Formats"
        Me.btnApplyFormats.UseVisualStyleBackColor = True
        '
        'chkUpdateLabel
        '
        Me.chkUpdateLabel.AutoSize = True
        Me.chkUpdateLabel.Location = New System.Drawing.Point(385, 302)
        Me.chkUpdateLabel.Name = "chkUpdateLabel"
        Me.chkUpdateLabel.Size = New System.Drawing.Size(116, 17)
        Me.chkUpdateLabel.TabIndex = 345
        Me.chkUpdateLabel.Text = "Update series label"
        Me.chkUpdateLabel.UseVisualStyleBackColor = True
        '
        'txtSuffix
        '
        Me.txtSuffix.Location = New System.Drawing.Point(181, 299)
        Me.txtSuffix.Name = "txtSuffix"
        Me.txtSuffix.Size = New System.Drawing.Size(198, 20)
        Me.txtSuffix.TabIndex = 344
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Location = New System.Drawing.Point(139, 303)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(36, 13)
        Me.Label40.TabIndex = 343
        Me.Label40.Text = "Suffix:"
        '
        'btnAdjust
        '
        Me.btnAdjust.Location = New System.Drawing.Point(12, 299)
        Me.btnAdjust.Name = "btnAdjust"
        Me.btnAdjust.Size = New System.Drawing.Size(121, 22)
        Me.btnAdjust.TabIndex = 346
        Me.btnAdjust.Text = "Adjust Parameters"
        Me.btnAdjust.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txtDefLineWidth)
        Me.GroupBox3.Controls.Add(Me.Label59)
        Me.GroupBox3.Controls.Add(Me.txtDefLineColor)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.txtDefMkrStep)
        Me.GroupBox3.Controls.Add(Me.Label58)
        Me.GroupBox3.Controls.Add(Me.txtDefMkrSize)
        Me.GroupBox3.Controls.Add(Me.Label57)
        Me.GroupBox3.Controls.Add(Me.cmbDefMkrStyle)
        Me.GroupBox3.Controls.Add(Me.Label56)
        Me.GroupBox3.Controls.Add(Me.txtDefBorderWidth)
        Me.GroupBox3.Controls.Add(Me.Label55)
        Me.GroupBox3.Controls.Add(Me.txtDefBorderColor)
        Me.GroupBox3.Controls.Add(Me.Label54)
        Me.GroupBox3.Controls.Add(Me.txtDefMkrColor)
        Me.GroupBox3.Controls.Add(Me.Label53)
        Me.GroupBox3.Controls.Add(Me.cmbDefMkrFill)
        Me.GroupBox3.Controls.Add(Me.Label38)
        Me.GroupBox3.Location = New System.Drawing.Point(11, 327)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(573, 64)
        Me.GroupBox3.TabIndex = 347
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Default distribution display settings:"
        '
        'txtDefLineWidth
        '
        Me.txtDefLineWidth.Location = New System.Drawing.Point(514, 34)
        Me.txtDefLineWidth.Name = "txtDefLineWidth"
        Me.txtDefLineWidth.Size = New System.Drawing.Size(49, 20)
        Me.txtDefLineWidth.TabIndex = 306
        '
        'Label59
        '
        Me.Label59.AutoSize = True
        Me.Label59.Location = New System.Drawing.Point(516, 18)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(35, 13)
        Me.Label59.TabIndex = 305
        Me.Label59.Text = "Width"
        '
        'txtDefLineColor
        '
        Me.txtDefLineColor.Location = New System.Drawing.Point(459, 34)
        Me.txtDefLineColor.Name = "txtDefLineColor"
        Me.txtDefLineColor.Size = New System.Drawing.Size(49, 20)
        Me.txtDefLineColor.TabIndex = 304
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(457, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 13)
        Me.Label1.TabIndex = 303
        Me.Label1.Text = "Line color"
        '
        'txtDefMkrStep
        '
        Me.txtDefMkrStep.Location = New System.Drawing.Point(404, 34)
        Me.txtDefMkrStep.Name = "txtDefMkrStep"
        Me.txtDefMkrStep.Size = New System.Drawing.Size(49, 20)
        Me.txtDefMkrStep.TabIndex = 302
        '
        'Label58
        '
        Me.Label58.AutoSize = True
        Me.Label58.Location = New System.Drawing.Point(401, 18)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(29, 13)
        Me.Label58.TabIndex = 301
        Me.Label58.Text = "Step"
        '
        'txtDefMkrSize
        '
        Me.txtDefMkrSize.Location = New System.Drawing.Point(349, 34)
        Me.txtDefMkrSize.Name = "txtDefMkrSize"
        Me.txtDefMkrSize.Size = New System.Drawing.Size(49, 20)
        Me.txtDefMkrSize.TabIndex = 300
        '
        'Label57
        '
        Me.Label57.AutoSize = True
        Me.Label57.Location = New System.Drawing.Point(346, 18)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(27, 13)
        Me.Label57.TabIndex = 299
        Me.Label57.Text = "Size"
        '
        'cmbDefMkrStyle
        '
        Me.cmbDefMkrStyle.FormattingEnabled = True
        Me.cmbDefMkrStyle.Location = New System.Drawing.Point(260, 34)
        Me.cmbDefMkrStyle.Name = "cmbDefMkrStyle"
        Me.cmbDefMkrStyle.Size = New System.Drawing.Size(83, 21)
        Me.cmbDefMkrStyle.TabIndex = 298
        '
        'Label56
        '
        Me.Label56.AutoSize = True
        Me.Label56.Location = New System.Drawing.Point(262, 18)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(64, 13)
        Me.Label56.TabIndex = 297
        Me.Label56.Text = "Marker style"
        '
        'txtDefBorderWidth
        '
        Me.txtDefBorderWidth.Location = New System.Drawing.Point(205, 35)
        Me.txtDefBorderWidth.Name = "txtDefBorderWidth"
        Me.txtDefBorderWidth.Size = New System.Drawing.Size(49, 20)
        Me.txtDefBorderWidth.TabIndex = 296
        '
        'Label55
        '
        Me.Label55.AutoSize = True
        Me.Label55.Location = New System.Drawing.Point(217, 19)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(35, 13)
        Me.Label55.TabIndex = 295
        Me.Label55.Text = "Width"
        '
        'txtDefBorderColor
        '
        Me.txtDefBorderColor.Location = New System.Drawing.Point(150, 35)
        Me.txtDefBorderColor.Name = "txtDefBorderColor"
        Me.txtDefBorderColor.Size = New System.Drawing.Size(49, 20)
        Me.txtDefBorderColor.TabIndex = 294
        '
        'Label54
        '
        Me.Label54.AutoSize = True
        Me.Label54.Location = New System.Drawing.Point(147, 18)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(64, 13)
        Me.Label54.TabIndex = 293
        Me.Label54.Text = "Border color"
        '
        'txtDefMkrColor
        '
        Me.txtDefMkrColor.Location = New System.Drawing.Point(95, 34)
        Me.txtDefMkrColor.Name = "txtDefMkrColor"
        Me.txtDefMkrColor.Size = New System.Drawing.Size(49, 20)
        Me.txtDefMkrColor.TabIndex = 292
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.Location = New System.Drawing.Point(96, 18)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(31, 13)
        Me.Label53.TabIndex = 291
        Me.Label53.Text = "Color"
        '
        'cmbDefMkrFill
        '
        Me.cmbDefMkrFill.FormattingEnabled = True
        Me.cmbDefMkrFill.Location = New System.Drawing.Point(6, 34)
        Me.cmbDefMkrFill.Name = "cmbDefMkrFill"
        Me.cmbDefMkrFill.Size = New System.Drawing.Size(83, 21)
        Me.cmbDefMkrFill.TabIndex = 290
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(7, 18)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(52, 13)
        Me.Label38.TabIndex = 289
        Me.Label38.Text = "Marker fill"
        '
        'AddToChart
        '
        Me.AddToChart.Location = New System.Drawing.Point(482, 40)
        Me.AddToChart.Name = "AddToChart"
        Me.AddToChart.Size = New System.Drawing.Size(117, 22)
        Me.AddToChart.TabIndex = 348
        Me.AddToChart.Text = "Add to Open Chart"
        Me.AddToChart.UseVisualStyleBackColor = True
        '
        'frmMultipleDistributions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(640, 734)
        Me.Controls.Add(Me.AddToChart)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.btnAdjust)
        Me.Controls.Add(Me.chkUpdateLabel)
        Me.Controls.Add(Me.txtSuffix)
        Me.Controls.Add(Me.Label40)
        Me.Controls.Add(Me.btnFormatHelp)
        Me.Controls.Add(Me.btnApplyFormats)
        Me.Controls.Add(Me.dgvFields)
        Me.Controls.Add(Me.Label39)
        Me.Controls.Add(Me.btnGenerate2)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.txtSelDistrib)
        Me.Controls.Add(Me.Label60)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.btnPrev)
        Me.Controls.Add(Me.Label61)
        Me.Controls.Add(Me.txtNDistribs)
        Me.Controls.Add(Me.Label36)
        Me.Controls.Add(Me.txtDefTo)
        Me.Controls.Add(Me.Label35)
        Me.Controls.Add(Me.txtDefFrom)
        Me.Controls.Add(Me.Label31)
        Me.Controls.Add(Me.btnSelect)
        Me.Controls.Add(Me.txtNParams)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.txtContinuity)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtDistName)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.dgvParams)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.btnExit)
        Me.Name = "frmMultipleDistributions"
        Me.Text = "Multiple Distributions"
        CType(Me.dgvParams, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvFields, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnExit As Button
    Friend WithEvents dgvParams As DataGridView
    Friend WithEvents Label15 As Label
    Friend WithEvents btnSelect As Button
    Friend WithEvents txtNParams As TextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents txtContinuity As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents txtDistName As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label36 As Label
    Friend WithEvents txtDefTo As TextBox
    Friend WithEvents Label35 As Label
    Friend WithEvents txtDefFrom As TextBox
    Friend WithEvents Label31 As Label
    Friend WithEvents btnDelete As Button
    Friend WithEvents btnAdd As Button
    Friend WithEvents txtSelDistrib As TextBox
    Friend WithEvents Label60 As Label
    Friend WithEvents btnNext As Button
    Friend WithEvents btnPrev As Button
    Friend WithEvents Label61 As Label
    Friend WithEvents txtNDistribs As TextBox
    Friend WithEvents btnGenerate2 As Button
    Friend WithEvents dgvFields As DataGridView
    Friend WithEvents Label39 As Label
    Friend WithEvents btnFormatHelp As Button
    Friend WithEvents btnApplyFormats As Button
    Friend WithEvents chkUpdateLabel As CheckBox
    Friend WithEvents txtSuffix As TextBox
    Friend WithEvents Label40 As Label
    Friend WithEvents btnAdjust As Button
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents txtDefLineWidth As TextBox
    Friend WithEvents Label59 As Label
    Friend WithEvents txtDefLineColor As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtDefMkrStep As TextBox
    Friend WithEvents Label58 As Label
    Friend WithEvents txtDefMkrSize As TextBox
    Friend WithEvents Label57 As Label
    Friend WithEvents cmbDefMkrStyle As ComboBox
    Friend WithEvents Label56 As Label
    Friend WithEvents txtDefBorderWidth As TextBox
    Friend WithEvents Label55 As Label
    Friend WithEvents txtDefBorderColor As TextBox
    Friend WithEvents Label54 As Label
    Friend WithEvents txtDefMkrColor As TextBox
    Friend WithEvents Label53 As Label
    Friend WithEvents cmbDefMkrFill As ComboBox
    Friend WithEvents Label38 As Label
    Friend WithEvents ColorDialog1 As ColorDialog
    Friend WithEvents AddToChart As Button
End Class
