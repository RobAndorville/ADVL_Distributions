<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGenerateSamples
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
        Me.cmbModel = New System.Windows.Forms.ComboBox()
        Me.cmbRandVar = New System.Windows.Forms.ComboBox()
        Me.lblUnitsAbbrev = New System.Windows.Forms.Label()
        Me.txtSampleVal = New System.Windows.Forms.TextBox()
        Me.btnSample = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtDistribParams = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtDistribType = New System.Windows.Forms.TextBox()
        Me.txtDescr = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnFormatHelp = New System.Windows.Forms.Button()
        Me.txtSingleFormat = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtFormat = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.btnClearTable = New System.Windows.Forms.Button()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtSampSetParams = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtSampSetDescr = New System.Windows.Forms.TextBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.rbCurrentColumn = New System.Windows.Forms.RadioButton()
        Me.rbAllColumns = New System.Windows.Forms.RadioButton()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.txtFileDescr = New System.Windows.Forms.TextBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.txtFileName = New System.Windows.Forms.TextBox()
        Me.btnClearColumn = New System.Windows.Forms.Button()
        Me.txtFromSamp = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.btnSeriesAnalysis = New System.Windows.Forms.Button()
        Me.txtSampSetUnitsAbbrev = New System.Windows.Forms.TextBox()
        Me.txtSeed = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.btnShuffle = New System.Windows.Forms.Button()
        Me.txtSampSetUnits = New System.Windows.Forms.TextBox()
        Me.btnGenerateSamples = New System.Windows.Forms.Button()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtSampSetLabel = New System.Windows.Forms.TextBox()
        Me.cmbSampling = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbType = New System.Windows.Forms.ComboBox()
        Me.txtNDistribSamples = New System.Windows.Forms.TextBox()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.cmbColumnName = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cmbTableName = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.txtRVUnitsAbbrev = New System.Windows.Forms.TextBox()
        Me.Label70 = New System.Windows.Forms.Label()
        Me.txtRVUnits = New System.Windows.Forms.TextBox()
        Me.Label68 = New System.Windows.Forms.Label()
        Me.txtContinuity = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtModelDescr = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Location = New System.Drawing.Point(514, 12)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(48, 22)
        Me.btnExit.TabIndex = 8
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'cmbModel
        '
        Me.cmbModel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbModel.FormattingEnabled = True
        Me.cmbModel.Location = New System.Drawing.Point(76, 19)
        Me.cmbModel.Name = "cmbModel"
        Me.cmbModel.Size = New System.Drawing.Size(468, 21)
        Me.cmbModel.TabIndex = 38
        '
        'cmbRandVar
        '
        Me.cmbRandVar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbRandVar.FormattingEnabled = True
        Me.cmbRandVar.Location = New System.Drawing.Point(104, 104)
        Me.cmbRandVar.Name = "cmbRandVar"
        Me.cmbRandVar.Size = New System.Drawing.Size(440, 21)
        Me.cmbRandVar.TabIndex = 37
        '
        'lblUnitsAbbrev
        '
        Me.lblUnitsAbbrev.AutoSize = True
        Me.lblUnitsAbbrev.Location = New System.Drawing.Point(268, 22)
        Me.lblUnitsAbbrev.Name = "lblUnitsAbbrev"
        Me.lblUnitsAbbrev.Size = New System.Drawing.Size(31, 13)
        Me.lblUnitsAbbrev.TabIndex = 36
        Me.lblUnitsAbbrev.Text = "Units"
        '
        'txtSampleVal
        '
        Me.txtSampleVal.Location = New System.Drawing.Point(68, 19)
        Me.txtSampleVal.Name = "txtSampleVal"
        Me.txtSampleVal.Size = New System.Drawing.Size(194, 20)
        Me.txtSampleVal.TabIndex = 35
        '
        'btnSample
        '
        Me.btnSample.Location = New System.Drawing.Point(6, 19)
        Me.btnSample.Name = "btnSample"
        Me.btnSample.Size = New System.Drawing.Size(56, 22)
        Me.btnSample.TabIndex = 34
        Me.btnSample.Text = "Sample"
        Me.btnSample.UseVisualStyleBackColor = True
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
        'txtDistribParams
        '
        Me.txtDistribParams.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDistribParams.Location = New System.Drawing.Point(77, 242)
        Me.txtDistribParams.Name = "txtDistribParams"
        Me.txtDistribParams.Size = New System.Drawing.Size(468, 20)
        Me.txtDistribParams.TabIndex = 32
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
        'txtDistribType
        '
        Me.txtDistribType.Location = New System.Drawing.Point(77, 216)
        Me.txtDistribType.Name = "txtDistribType"
        Me.txtDistribType.Size = New System.Drawing.Size(194, 20)
        Me.txtDistribType.TabIndex = 30
        '
        'txtDescr
        '
        Me.txtDescr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDescr.Location = New System.Drawing.Point(105, 157)
        Me.txtDescr.Multiline = True
        Me.txtDescr.Name = "txtDescr"
        Me.txtDescr.Size = New System.Drawing.Size(440, 52)
        Me.txtDescr.TabIndex = 29
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
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 109)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(90, 13)
        Me.Label1.TabIndex = 27
        Me.Label1.Text = "Random variable:"
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
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnFormatHelp)
        Me.GroupBox1.Controls.Add(Me.txtSingleFormat)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.btnSample)
        Me.GroupBox1.Controls.Add(Me.txtSampleVal)
        Me.GroupBox1.Controls.Add(Me.lblUnitsAbbrev)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 318)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(535, 57)
        Me.GroupBox1.TabIndex = 39
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Single Sample:"
        '
        'btnFormatHelp
        '
        Me.btnFormatHelp.Location = New System.Drawing.Point(444, 19)
        Me.btnFormatHelp.Name = "btnFormatHelp"
        Me.btnFormatHelp.Size = New System.Drawing.Size(80, 22)
        Me.btnFormatHelp.TabIndex = 394
        Me.btnFormatHelp.Text = "Format Help"
        Me.btnFormatHelp.UseVisualStyleBackColor = True
        '
        'txtSingleFormat
        '
        Me.txtSingleFormat.Location = New System.Drawing.Point(367, 19)
        Me.txtSingleFormat.Name = "txtSingleFormat"
        Me.txtSingleFormat.Size = New System.Drawing.Size(71, 20)
        Me.txtSingleFormat.TabIndex = 393
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(319, 21)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(42, 13)
        Me.Label16.TabIndex = 392
        Me.Label16.Text = "Format:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.txtFormat)
        Me.GroupBox2.Controls.Add(Me.Label15)
        Me.GroupBox2.Controls.Add(Me.btnClearTable)
        Me.GroupBox2.Controls.Add(Me.Label14)
        Me.GroupBox2.Controls.Add(Me.txtSampSetParams)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.txtSampSetDescr)
        Me.GroupBox2.Controls.Add(Me.GroupBox4)
        Me.GroupBox2.Controls.Add(Me.btnClearColumn)
        Me.GroupBox2.Controls.Add(Me.txtFromSamp)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.btnSeriesAnalysis)
        Me.GroupBox2.Controls.Add(Me.txtSampSetUnitsAbbrev)
        Me.GroupBox2.Controls.Add(Me.txtSeed)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.Label36)
        Me.GroupBox2.Controls.Add(Me.btnShuffle)
        Me.GroupBox2.Controls.Add(Me.txtSampSetUnits)
        Me.GroupBox2.Controls.Add(Me.btnGenerateSamples)
        Me.GroupBox2.Controls.Add(Me.Label35)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.txtSampSetLabel)
        Me.GroupBox2.Controls.Add(Me.cmbSampling)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.cmbType)
        Me.GroupBox2.Controls.Add(Me.txtNDistribSamples)
        Me.GroupBox2.Controls.Add(Me.Label62)
        Me.GroupBox2.Controls.Add(Me.cmbColumnName)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.cmbTableName)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 381)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(550, 495)
        Me.GroupBox2.TabIndex = 40
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Multiple Samples:"
        '
        'txtFormat
        '
        Me.txtFormat.Location = New System.Drawing.Point(429, 78)
        Me.txtFormat.Name = "txtFormat"
        Me.txtFormat.Size = New System.Drawing.Size(71, 20)
        Me.txtFormat.TabIndex = 391
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(381, 80)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(42, 13)
        Me.Label15.TabIndex = 390
        Me.Label15.Text = "Format:"
        '
        'btnClearTable
        '
        Me.btnClearTable.Location = New System.Drawing.Point(368, 313)
        Me.btnClearTable.Name = "btnClearTable"
        Me.btnClearTable.Size = New System.Drawing.Size(85, 22)
        Me.btnClearTable.TabIndex = 389
        Me.btnClearTable.Text = "Clear Table"
        Me.btnClearTable.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(24, 244)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(63, 13)
        Me.Label14.TabIndex = 388
        Me.Label14.Text = "Parameters:"
        '
        'txtSampSetParams
        '
        Me.txtSampSetParams.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSampSetParams.Location = New System.Drawing.Point(93, 241)
        Me.txtSampSetParams.Multiline = True
        Me.txtSampSetParams.Name = "txtSampSetParams"
        Me.txtSampSetParams.Size = New System.Drawing.Size(451, 66)
        Me.txtSampSetParams.TabIndex = 387
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(23, 186)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(63, 13)
        Me.Label13.TabIndex = 386
        Me.Label13.Text = "Description:"
        '
        'txtSampSetDescr
        '
        Me.txtSampSetDescr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSampSetDescr.Location = New System.Drawing.Point(92, 183)
        Me.txtSampSetDescr.Multiline = True
        Me.txtSampSetDescr.Name = "txtSampSetDescr"
        Me.txtSampSetDescr.Size = New System.Drawing.Size(452, 52)
        Me.txtSampSetDescr.TabIndex = 385
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.Controls.Add(Me.rbCurrentColumn)
        Me.GroupBox4.Controls.Add(Me.rbAllColumns)
        Me.GroupBox4.Controls.Add(Me.btnSave)
        Me.GroupBox4.Controls.Add(Me.txtFileDescr)
        Me.GroupBox4.Controls.Add(Me.Label34)
        Me.GroupBox4.Controls.Add(Me.Label32)
        Me.GroupBox4.Controls.Add(Me.txtFileName)
        Me.GroupBox4.Location = New System.Drawing.Point(7, 341)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(538, 144)
        Me.GroupBox4.TabIndex = 311
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Save Samples:"
        '
        'rbCurrentColumn
        '
        Me.rbCurrentColumn.AutoSize = True
        Me.rbCurrentColumn.Location = New System.Drawing.Point(6, 87)
        Me.rbCurrentColumn.Name = "rbCurrentColumn"
        Me.rbCurrentColumn.Size = New System.Drawing.Size(97, 17)
        Me.rbCurrentColumn.TabIndex = 386
        Me.rbCurrentColumn.TabStop = True
        Me.rbCurrentColumn.Text = "Current Column"
        Me.rbCurrentColumn.UseVisualStyleBackColor = True
        '
        'rbAllColumns
        '
        Me.rbAllColumns.AutoSize = True
        Me.rbAllColumns.Location = New System.Drawing.Point(6, 64)
        Me.rbAllColumns.Name = "rbAllColumns"
        Me.rbAllColumns.Size = New System.Drawing.Size(79, 17)
        Me.rbAllColumns.TabIndex = 42
        Me.rbAllColumns.TabStop = True
        Me.rbAllColumns.Text = "All Columns"
        Me.rbAllColumns.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(6, 110)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(56, 22)
        Me.btnSave.TabIndex = 385
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'txtFileDescr
        '
        Me.txtFileDescr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtFileDescr.Location = New System.Drawing.Point(109, 45)
        Me.txtFileDescr.Multiline = True
        Me.txtFileDescr.Name = "txtFileDescr"
        Me.txtFileDescr.Size = New System.Drawing.Size(423, 87)
        Me.txtFileDescr.TabIndex = 384
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(6, 48)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(63, 13)
        Me.Label34.TabIndex = 383
        Me.Label34.Text = "Description:"
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(6, 22)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(57, 13)
        Me.Label32.TabIndex = 376
        Me.Label32.Text = "File Name:"
        '
        'txtFileName
        '
        Me.txtFileName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtFileName.Location = New System.Drawing.Point(109, 19)
        Me.txtFileName.Name = "txtFileName"
        Me.txtFileName.Size = New System.Drawing.Size(423, 20)
        Me.txtFileName.TabIndex = 375
        '
        'btnClearColumn
        '
        Me.btnClearColumn.Location = New System.Drawing.Point(277, 313)
        Me.btnClearColumn.Name = "btnClearColumn"
        Me.btnClearColumn.Size = New System.Drawing.Size(85, 22)
        Me.btnClearColumn.TabIndex = 310
        Me.btnClearColumn.Text = "Clear Column"
        Me.btnClearColumn.UseVisualStyleBackColor = True
        '
        'txtFromSamp
        '
        Me.txtFromSamp.Location = New System.Drawing.Point(92, 104)
        Me.txtFromSamp.Name = "txtFromSamp"
        Me.txtFromSamp.Size = New System.Drawing.Size(120, 20)
        Me.txtFromSamp.TabIndex = 309
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(6, 107)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(84, 13)
        Me.Label10.TabIndex = 308
        Me.Label10.Text = "From sample no:"
        '
        'btnSeriesAnalysis
        '
        Me.btnSeriesAnalysis.Location = New System.Drawing.Point(123, 313)
        Me.btnSeriesAnalysis.Name = "btnSeriesAnalysis"
        Me.btnSeriesAnalysis.Size = New System.Drawing.Size(89, 22)
        Me.btnSeriesAnalysis.TabIndex = 307
        Me.btnSeriesAnalysis.Text = "Series Analysis"
        Me.btnSeriesAnalysis.UseVisualStyleBackColor = True
        '
        'txtSampSetUnitsAbbrev
        '
        Me.txtSampSetUnitsAbbrev.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSampSetUnitsAbbrev.Location = New System.Drawing.Point(423, 157)
        Me.txtSampSetUnitsAbbrev.Name = "txtSampSetUnitsAbbrev"
        Me.txtSampSetUnitsAbbrev.Size = New System.Drawing.Size(121, 20)
        Me.txtSampSetUnitsAbbrev.TabIndex = 382
        '
        'txtSeed
        '
        Me.txtSeed.Location = New System.Drawing.Point(441, 105)
        Me.txtSeed.Name = "txtSeed"
        Me.txtSeed.Size = New System.Drawing.Size(59, 20)
        Me.txtSeed.TabIndex = 306
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(373, 160)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(44, 13)
        Me.Label11.TabIndex = 381
        Me.Label11.Text = "Abbrev:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(400, 107)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(35, 13)
        Me.Label9.TabIndex = 305
        Me.Label9.Text = "Seed:"
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(52, 160)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(34, 13)
        Me.Label36.TabIndex = 380
        Me.Label36.Text = "Units:"
        '
        'btnShuffle
        '
        Me.btnShuffle.Location = New System.Drawing.Point(215, 313)
        Me.btnShuffle.Name = "btnShuffle"
        Me.btnShuffle.Size = New System.Drawing.Size(56, 22)
        Me.btnShuffle.TabIndex = 304
        Me.btnShuffle.Text = "Shuffle"
        Me.btnShuffle.UseVisualStyleBackColor = True
        '
        'txtSampSetUnits
        '
        Me.txtSampSetUnits.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSampSetUnits.Location = New System.Drawing.Point(92, 157)
        Me.txtSampSetUnits.Name = "txtSampSetUnits"
        Me.txtSampSetUnits.Size = New System.Drawing.Size(275, 20)
        Me.txtSampSetUnits.TabIndex = 379
        '
        'btnGenerateSamples
        '
        Me.btnGenerateSamples.Location = New System.Drawing.Point(6, 313)
        Me.btnGenerateSamples.Name = "btnGenerateSamples"
        Me.btnGenerateSamples.Size = New System.Drawing.Size(111, 22)
        Me.btnGenerateSamples.TabIndex = 300
        Me.btnGenerateSamples.Text = "Generate Samples"
        Me.btnGenerateSamples.UseVisualStyleBackColor = True
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(50, 134)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(36, 13)
        Me.Label35.TabIndex = 378
        Me.Label35.Text = "Label:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(218, 107)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(53, 13)
        Me.Label8.TabIndex = 299
        Me.Label8.Text = "Sampling:"
        '
        'txtSampSetLabel
        '
        Me.txtSampSetLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSampSetLabel.Location = New System.Drawing.Point(92, 131)
        Me.txtSampSetLabel.Name = "txtSampSetLabel"
        Me.txtSampSetLabel.Size = New System.Drawing.Size(452, 20)
        Me.txtSampSetLabel.TabIndex = 377
        '
        'cmbSampling
        '
        Me.cmbSampling.FormattingEnabled = True
        Me.cmbSampling.Location = New System.Drawing.Point(277, 104)
        Me.cmbSampling.Name = "cmbSampling"
        Me.cmbSampling.Size = New System.Drawing.Size(117, 21)
        Me.cmbSampling.TabIndex = 298
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(218, 80)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(34, 13)
        Me.Label6.TabIndex = 297
        Me.Label6.Text = "Type:"
        '
        'cmbType
        '
        Me.cmbType.FormattingEnabled = True
        Me.cmbType.Location = New System.Drawing.Point(258, 77)
        Me.cmbType.Name = "cmbType"
        Me.cmbType.Size = New System.Drawing.Size(117, 21)
        Me.cmbType.TabIndex = 296
        '
        'txtNDistribSamples
        '
        Me.txtNDistribSamples.Location = New System.Drawing.Point(92, 77)
        Me.txtNDistribSamples.Name = "txtNDistribSamples"
        Me.txtNDistribSamples.Size = New System.Drawing.Size(120, 20)
        Me.txtNDistribSamples.TabIndex = 295
        '
        'Label62
        '
        Me.Label62.AutoSize = True
        Me.Label62.Location = New System.Drawing.Point(6, 80)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(80, 13)
        Me.Label62.TabIndex = 294
        Me.Label62.Text = "No. of samples:"
        '
        'cmbColumnName
        '
        Me.cmbColumnName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbColumnName.FormattingEnabled = True
        Me.cmbColumnName.Location = New System.Drawing.Point(92, 50)
        Me.cmbColumnName.Name = "cmbColumnName"
        Me.cmbColumnName.Size = New System.Drawing.Size(452, 21)
        Me.cmbColumnName.TabIndex = 45
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 53)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(45, 13)
        Me.Label7.TabIndex = 44
        Me.Label7.Text = "Column:"
        '
        'cmbTableName
        '
        Me.cmbTableName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbTableName.FormattingEnabled = True
        Me.cmbTableName.Location = New System.Drawing.Point(92, 21)
        Me.cmbTableName.Name = "cmbTableName"
        Me.cmbTableName.Size = New System.Drawing.Size(452, 21)
        Me.cmbTableName.TabIndex = 40
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 24)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(37, 13)
        Me.Label5.TabIndex = 39
        Me.Label5.Text = "Table:"
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
        Me.GroupBox3.Size = New System.Drawing.Size(550, 272)
        Me.GroupBox3.TabIndex = 41
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Distribution Model:"
        '
        'txtRVUnitsAbbrev
        '
        Me.txtRVUnitsAbbrev.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRVUnitsAbbrev.Location = New System.Drawing.Point(411, 131)
        Me.txtRVUnitsAbbrev.Name = "txtRVUnitsAbbrev"
        Me.txtRVUnitsAbbrev.Size = New System.Drawing.Size(133, 20)
        Me.txtRVUnitsAbbrev.TabIndex = 296
        '
        'Label70
        '
        Me.Label70.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label70.AutoSize = True
        Me.Label70.Location = New System.Drawing.Point(361, 134)
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
        Me.txtRVUnits.Size = New System.Drawing.Size(251, 20)
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
        Me.txtContinuity.Size = New System.Drawing.Size(206, 20)
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
        'txtModelDescr
        '
        Me.txtModelDescr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtModelDescr.Location = New System.Drawing.Point(75, 46)
        Me.txtModelDescr.Multiline = True
        Me.txtModelDescr.Name = "txtModelDescr"
        Me.txtModelDescr.Size = New System.Drawing.Size(468, 52)
        Me.txtModelDescr.TabIndex = 297
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
        'frmGenerateSamples
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(574, 882)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnExit)
        Me.Name = "frmGenerateSamples"
        Me.Text = "Generate Samples"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnExit As Button
    Friend WithEvents cmbModel As ComboBox
    Friend WithEvents cmbRandVar As ComboBox
    Friend WithEvents lblUnitsAbbrev As Label
    Friend WithEvents txtSampleVal As TextBox
    Friend WithEvents btnSample As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents txtDistribParams As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtDistribType As TextBox
    Friend WithEvents txtDescr As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label23 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents cmbColumnName As ComboBox
    Friend WithEvents Label7 As Label
    Friend WithEvents cmbTableName As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents cmbSampling As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents cmbType As ComboBox
    Friend WithEvents txtNDistribSamples As TextBox
    Friend WithEvents Label62 As Label
    Friend WithEvents btnGenerateSamples As Button
    Friend WithEvents btnShuffle As Button
    Friend WithEvents txtSeed As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents btnSeriesAnalysis As Button
    Friend WithEvents txtFromSamp As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents btnClearColumn As Button
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents txtSampSetUnitsAbbrev As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents Label36 As Label
    Friend WithEvents txtSampSetUnits As TextBox
    Friend WithEvents Label35 As Label
    Friend WithEvents txtSampSetLabel As TextBox
    Friend WithEvents Label32 As Label
    Friend WithEvents txtFileName As TextBox
    Friend WithEvents btnSave As Button
    Friend WithEvents txtFileDescr As TextBox
    Friend WithEvents Label34 As Label
    Friend WithEvents txtContinuity As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents rbCurrentColumn As RadioButton
    Friend WithEvents rbAllColumns As RadioButton
    Friend WithEvents txtRVUnitsAbbrev As TextBox
    Friend WithEvents Label70 As Label
    Friend WithEvents txtRVUnits As TextBox
    Friend WithEvents Label68 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents txtSampSetDescr As TextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents txtSampSetParams As TextBox
    Friend WithEvents btnClearTable As Button
    Friend WithEvents txtFormat As TextBox
    Friend WithEvents Label15 As Label
    Friend WithEvents txtSingleFormat As TextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents btnFormatHelp As Button
    Friend WithEvents Label17 As Label
    Friend WithEvents txtModelDescr As TextBox
End Class
