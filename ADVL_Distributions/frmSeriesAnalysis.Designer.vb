<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSeriesAnalysis
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
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Me.btnPlot = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Chart1 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.txtMinFormat = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.btnIsSample = New System.Windows.Forms.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtMaxFormat = New System.Windows.Forms.TextBox()
        Me.rbPopulation = New System.Windows.Forms.RadioButton()
        Me.txtVariance = New System.Windows.Forms.TextBox()
        Me.rbSample = New System.Windows.Forms.RadioButton()
        Me.txtSumFormat = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtAvgFormat = New System.Windows.Forms.TextBox()
        Me.txtStdDev = New System.Windows.Forms.TextBox()
        Me.txtStdDevFormat = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtVarFormat = New System.Windows.Forms.TextBox()
        Me.txtAverage = New System.Windows.Forms.TextBox()
        Me.btnFormatHelp = New System.Windows.Forms.Button()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtSum = New System.Windows.Forms.TextBox()
        Me.txtMinimum = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtMaximum = New System.Windows.Forms.TextBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.txtDataSource = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblColCount = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.cmbSourceDataTable = New System.Windows.Forms.ComboBox()
        Me.lblColNo = New System.Windows.Forms.Label()
        Me.lblTableNo = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbSourceColumnName = New System.Windows.Forms.ComboBox()
        Me.lblTableCount = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtDatasetName = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtNRows = New System.Windows.Forms.TextBox()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.cmbCalcTableName = New System.Windows.Forms.ComboBox()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.dgvData = New System.Windows.Forms.DataGridView()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.btnUpdateCharts2 = New System.Windows.Forms.Button()
        Me.txtParamEValue = New System.Windows.Forms.TextBox()
        Me.txtParamDValue = New System.Windows.Forms.TextBox()
        Me.txtParamCValue = New System.Windows.Forms.TextBox()
        Me.txtParamBValue = New System.Windows.Forms.TextBox()
        Me.txtParamEName = New System.Windows.Forms.TextBox()
        Me.txtParamDName = New System.Windows.Forms.TextBox()
        Me.txtParamCName = New System.Windows.Forms.TextBox()
        Me.txtParamBName = New System.Windows.Forms.TextBox()
        Me.txtParamAValue = New System.Windows.Forms.TextBox()
        Me.txtParamAName = New System.Windows.Forms.TextBox()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.txtContDisc = New System.Windows.Forms.TextBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.chkShowModel = New System.Windows.Forms.CheckBox()
        Me.txtDistributionName = New System.Windows.Forms.TextBox()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.rbCdf = New System.Windows.Forms.RadioButton()
        Me.btnUpdateCharts = New System.Windows.Forms.Button()
        Me.rbReverseCdf = New System.Windows.Forms.RadioButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.txtLeft = New System.Windows.Forms.TextBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtTop = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.btnApplySize = New System.Windows.Forms.Button()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtHeight = New System.Windows.Forms.TextBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.txtWidth = New System.Windows.Forms.TextBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnShowChartMinMax = New System.Windows.Forms.Button()
        Me.btnUpdateChartMinMax = New System.Windows.Forms.Button()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtHistMin = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.txtHistMax = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.txtChartInterval = New System.Windows.Forms.TextBox()
        Me.txtChartMax = New System.Windows.Forms.TextBox()
        Me.txtChartMin = New System.Windows.Forms.TextBox()
        Me.txtIntervalWidth = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.btnUpdateHistogram = New System.Windows.Forms.Button()
        Me.txtDataMax = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtDataMin = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtDataRange = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtNIntervals = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.btnEditColor = New System.Windows.Forms.Button()
        Me.btnEditFont = New System.Windows.Forms.Button()
        Me.txtEditTitle = New System.Windows.Forms.TextBox()
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown()
        Me.btnEditTitle = New System.Windows.Forms.Button()
        Me.btnChartTitleColor = New System.Windows.Forms.Button()
        Me.btnChartTitleFont = New System.Windows.Forms.Button()
        Me.txtAddTitle = New System.Windows.Forms.TextBox()
        Me.btnAddTitle = New System.Windows.Forms.Button()
        Me.btnFormatHelp2 = New System.Windows.Forms.Button()
        Me.btnUpdateAnnotation = New System.Windows.Forms.Button()
        Me.btnDeleteAnnot = New System.Windows.Forms.Button()
        Me.dgvAnnot = New System.Windows.Forms.DataGridView()
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.FontDialog1 = New System.Windows.Forms.FontDialog()
        Me.cmbTable = New System.Windows.Forms.ComboBox()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.cmbColumn = New System.Windows.Forms.ComboBox()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.txtNSamps = New System.Windows.Forms.TextBox()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        CType(Me.dgvData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvAnnot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnPlot
        '
        Me.btnPlot.Location = New System.Drawing.Point(552, 12)
        Me.btnPlot.Name = "btnPlot"
        Me.btnPlot.Size = New System.Drawing.Size(48, 22)
        Me.btnPlot.TabIndex = 14
        Me.btnPlot.Text = "Plot"
        Me.btnPlot.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Location = New System.Drawing.Point(686, 12)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(48, 22)
        Me.btnExit.TabIndex = 15
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
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage5)
        Me.TabControl1.Location = New System.Drawing.Point(12, 40)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(722, 688)
        Me.TabControl1.TabIndex = 16
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Chart1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(714, 662)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Chart"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Chart1
        '
        Me.Chart1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        ChartArea1.Name = "ChartArea1"
        Me.Chart1.ChartAreas.Add(ChartArea1)
        Legend1.Name = "Legend1"
        Me.Chart1.Legends.Add(Legend1)
        Me.Chart1.Location = New System.Drawing.Point(6, 6)
        Me.Chart1.Name = "Chart1"
        Series1.ChartArea = "ChartArea1"
        Series1.Legend = "Legend1"
        Series1.Name = "Series1"
        Me.Chart1.Series.Add(Series1)
        Me.Chart1.Size = New System.Drawing.Size(702, 650)
        Me.Chart1.TabIndex = 0
        Me.Chart1.Text = "Chart1"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.GroupBox4)
        Me.TabPage2.Controls.Add(Me.GroupBox3)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(714, 662)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Statistics"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.txtMinFormat)
        Me.GroupBox4.Controls.Add(Me.Label15)
        Me.GroupBox4.Controls.Add(Me.btnIsSample)
        Me.GroupBox4.Controls.Add(Me.Label12)
        Me.GroupBox4.Controls.Add(Me.txtMaxFormat)
        Me.GroupBox4.Controls.Add(Me.rbPopulation)
        Me.GroupBox4.Controls.Add(Me.txtVariance)
        Me.GroupBox4.Controls.Add(Me.rbSample)
        Me.GroupBox4.Controls.Add(Me.txtSumFormat)
        Me.GroupBox4.Controls.Add(Me.Label11)
        Me.GroupBox4.Controls.Add(Me.txtAvgFormat)
        Me.GroupBox4.Controls.Add(Me.txtStdDev)
        Me.GroupBox4.Controls.Add(Me.txtStdDevFormat)
        Me.GroupBox4.Controls.Add(Me.Label10)
        Me.GroupBox4.Controls.Add(Me.txtVarFormat)
        Me.GroupBox4.Controls.Add(Me.txtAverage)
        Me.GroupBox4.Controls.Add(Me.btnFormatHelp)
        Me.GroupBox4.Controls.Add(Me.Label9)
        Me.GroupBox4.Controls.Add(Me.Label6)
        Me.GroupBox4.Controls.Add(Me.txtSum)
        Me.GroupBox4.Controls.Add(Me.txtMinimum)
        Me.GroupBox4.Controls.Add(Me.Label8)
        Me.GroupBox4.Controls.Add(Me.Label7)
        Me.GroupBox4.Controls.Add(Me.txtMaximum)
        Me.GroupBox4.Location = New System.Drawing.Point(6, 169)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(369, 226)
        Me.GroupBox4.TabIndex = 354
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Series Statistics:"
        '
        'txtMinFormat
        '
        Me.txtMinFormat.Location = New System.Drawing.Point(244, 31)
        Me.txtMinFormat.Name = "txtMinFormat"
        Me.txtMinFormat.Size = New System.Drawing.Size(88, 20)
        Me.txtMinFormat.TabIndex = 327
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(241, 15)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(39, 13)
        Me.Label15.TabIndex = 326
        Me.Label15.Text = "Format"
        '
        'btnIsSample
        '
        Me.btnIsSample.Location = New System.Drawing.Point(263, 184)
        Me.btnIsSample.Name = "btnIsSample"
        Me.btnIsSample.Size = New System.Drawing.Size(21, 22)
        Me.btnIsSample.TabIndex = 352
        Me.btnIsSample.Text = "?"
        Me.btnIsSample.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(61, 189)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(43, 13)
        Me.Label12.TabIndex = 325
        Me.Label12.Text = "Data is:"
        '
        'txtMaxFormat
        '
        Me.txtMaxFormat.Location = New System.Drawing.Point(244, 57)
        Me.txtMaxFormat.Name = "txtMaxFormat"
        Me.txtMaxFormat.Size = New System.Drawing.Size(88, 20)
        Me.txtMaxFormat.TabIndex = 328
        '
        'rbPopulation
        '
        Me.rbPopulation.AutoSize = True
        Me.rbPopulation.Location = New System.Drawing.Point(182, 187)
        Me.rbPopulation.Name = "rbPopulation"
        Me.rbPopulation.Size = New System.Drawing.Size(75, 17)
        Me.rbPopulation.TabIndex = 324
        Me.rbPopulation.TabStop = True
        Me.rbPopulation.Text = "Population"
        Me.rbPopulation.UseVisualStyleBackColor = True
        '
        'txtVariance
        '
        Me.txtVariance.Location = New System.Drawing.Point(116, 161)
        Me.txtVariance.Name = "txtVariance"
        Me.txtVariance.Size = New System.Drawing.Size(122, 20)
        Me.txtVariance.TabIndex = 346
        '
        'rbSample
        '
        Me.rbSample.AutoSize = True
        Me.rbSample.Location = New System.Drawing.Point(116, 187)
        Me.rbSample.Name = "rbSample"
        Me.rbSample.Size = New System.Drawing.Size(60, 17)
        Me.rbSample.TabIndex = 323
        Me.rbSample.TabStop = True
        Me.rbSample.Text = "Sample"
        Me.rbSample.UseVisualStyleBackColor = True
        '
        'txtSumFormat
        '
        Me.txtSumFormat.Location = New System.Drawing.Point(244, 83)
        Me.txtSumFormat.Name = "txtSumFormat"
        Me.txtSumFormat.Size = New System.Drawing.Size(88, 20)
        Me.txtSumFormat.TabIndex = 329
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(11, 164)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(52, 13)
        Me.Label11.TabIndex = 345
        Me.Label11.Text = "Variance:"
        '
        'txtAvgFormat
        '
        Me.txtAvgFormat.Location = New System.Drawing.Point(244, 109)
        Me.txtAvgFormat.Name = "txtAvgFormat"
        Me.txtAvgFormat.Size = New System.Drawing.Size(88, 20)
        Me.txtAvgFormat.TabIndex = 330
        '
        'txtStdDev
        '
        Me.txtStdDev.Location = New System.Drawing.Point(116, 135)
        Me.txtStdDev.Name = "txtStdDev"
        Me.txtStdDev.Size = New System.Drawing.Size(122, 20)
        Me.txtStdDev.TabIndex = 344
        '
        'txtStdDevFormat
        '
        Me.txtStdDevFormat.Location = New System.Drawing.Point(244, 135)
        Me.txtStdDevFormat.Name = "txtStdDevFormat"
        Me.txtStdDevFormat.Size = New System.Drawing.Size(88, 20)
        Me.txtStdDevFormat.TabIndex = 331
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(11, 138)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(99, 13)
        Me.Label10.TabIndex = 343
        Me.Label10.Text = "Standard deviation:"
        '
        'txtVarFormat
        '
        Me.txtVarFormat.Location = New System.Drawing.Point(244, 161)
        Me.txtVarFormat.Name = "txtVarFormat"
        Me.txtVarFormat.Size = New System.Drawing.Size(88, 20)
        Me.txtVarFormat.TabIndex = 332
        '
        'txtAverage
        '
        Me.txtAverage.Location = New System.Drawing.Point(116, 109)
        Me.txtAverage.Name = "txtAverage"
        Me.txtAverage.Size = New System.Drawing.Size(122, 20)
        Me.txtAverage.TabIndex = 342
        '
        'btnFormatHelp
        '
        Me.btnFormatHelp.Location = New System.Drawing.Point(338, 29)
        Me.btnFormatHelp.Name = "btnFormatHelp"
        Me.btnFormatHelp.Size = New System.Drawing.Size(21, 22)
        Me.btnFormatHelp.TabIndex = 333
        Me.btnFormatHelp.Text = "?"
        Me.btnFormatHelp.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(11, 112)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(50, 13)
        Me.Label9.TabIndex = 341
        Me.Label9.Text = "Average:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(11, 34)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(51, 13)
        Me.Label6.TabIndex = 335
        Me.Label6.Text = "Minimum:"
        '
        'txtSum
        '
        Me.txtSum.Location = New System.Drawing.Point(116, 83)
        Me.txtSum.Name = "txtSum"
        Me.txtSum.Size = New System.Drawing.Size(122, 20)
        Me.txtSum.TabIndex = 340
        '
        'txtMinimum
        '
        Me.txtMinimum.Location = New System.Drawing.Point(116, 31)
        Me.txtMinimum.Name = "txtMinimum"
        Me.txtMinimum.Size = New System.Drawing.Size(122, 20)
        Me.txtMinimum.TabIndex = 336
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(11, 86)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(31, 13)
        Me.Label8.TabIndex = 339
        Me.Label8.Text = "Sum:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(11, 60)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(54, 13)
        Me.Label7.TabIndex = 337
        Me.Label7.Text = "Maximum:"
        '
        'txtMaximum
        '
        Me.txtMaximum.Location = New System.Drawing.Point(116, 57)
        Me.txtMaximum.Name = "txtMaximum"
        Me.txtMaximum.Size = New System.Drawing.Size(122, 20)
        Me.txtMaximum.TabIndex = 338
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txtDataSource)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.lblColCount)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.Label16)
        Me.GroupBox3.Controls.Add(Me.cmbSourceDataTable)
        Me.GroupBox3.Controls.Add(Me.lblColNo)
        Me.GroupBox3.Controls.Add(Me.lblTableNo)
        Me.GroupBox3.Controls.Add(Me.Label13)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.cmbSourceColumnName)
        Me.GroupBox3.Controls.Add(Me.lblTableCount)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.txtDatasetName)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.txtNRows)
        Me.GroupBox3.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(451, 157)
        Me.GroupBox3.TabIndex = 353
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Series Information:"
        '
        'txtDataSource
        '
        Me.txtDataSource.Location = New System.Drawing.Point(113, 19)
        Me.txtDataSource.Name = "txtDataSource"
        Me.txtDataSource.ReadOnly = True
        Me.txtDataSource.Size = New System.Drawing.Size(238, 20)
        Me.txtDataSource.TabIndex = 313
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(70, 13)
        Me.Label1.TabIndex = 312
        Me.Label1.Text = "Data Source:"
        '
        'lblColCount
        '
        Me.lblColCount.AutoSize = True
        Me.lblColCount.Location = New System.Drawing.Point(406, 101)
        Me.lblColCount.Name = "lblColCount"
        Me.lblColCount.Size = New System.Drawing.Size(35, 13)
        Me.lblColCount.TabIndex = 351
        Me.lblColCount.Text = "Count"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 74)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 13)
        Me.Label2.TabIndex = 314
        Me.Label2.Text = "Table Name:"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(384, 101)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(16, 13)
        Me.Label16.TabIndex = 350
        Me.Label16.Text = "of"
        '
        'cmbSourceDataTable
        '
        Me.cmbSourceDataTable.FormattingEnabled = True
        Me.cmbSourceDataTable.Location = New System.Drawing.Point(113, 71)
        Me.cmbSourceDataTable.Name = "cmbSourceDataTable"
        Me.cmbSourceDataTable.Size = New System.Drawing.Size(238, 21)
        Me.cmbSourceDataTable.TabIndex = 315
        '
        'lblColNo
        '
        Me.lblColNo.AutoSize = True
        Me.lblColNo.Location = New System.Drawing.Point(357, 101)
        Me.lblColNo.Name = "lblColNo"
        Me.lblColNo.Size = New System.Drawing.Size(21, 13)
        Me.lblColNo.TabIndex = 349
        Me.lblColNo.Text = "No"
        '
        'lblTableNo
        '
        Me.lblTableNo.AutoSize = True
        Me.lblTableNo.Location = New System.Drawing.Point(357, 74)
        Me.lblTableNo.Name = "lblTableNo"
        Me.lblTableNo.Size = New System.Drawing.Size(21, 13)
        Me.lblTableNo.TabIndex = 316
        Me.lblTableNo.Text = "No"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(8, 101)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(76, 13)
        Me.Label13.TabIndex = 348
        Me.Label13.Text = "Column Name:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(384, 74)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(16, 13)
        Me.Label3.TabIndex = 317
        Me.Label3.Text = "of"
        '
        'cmbSourceColumnName
        '
        Me.cmbSourceColumnName.FormattingEnabled = True
        Me.cmbSourceColumnName.Location = New System.Drawing.Point(113, 98)
        Me.cmbSourceColumnName.Name = "cmbSourceColumnName"
        Me.cmbSourceColumnName.Size = New System.Drawing.Size(238, 21)
        Me.cmbSourceColumnName.TabIndex = 347
        '
        'lblTableCount
        '
        Me.lblTableCount.AutoSize = True
        Me.lblTableCount.Location = New System.Drawing.Point(406, 74)
        Me.lblTableCount.Name = "lblTableCount"
        Me.lblTableCount.Size = New System.Drawing.Size(35, 13)
        Me.lblTableCount.TabIndex = 318
        Me.lblTableCount.Text = "Count"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 48)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(78, 13)
        Me.Label4.TabIndex = 319
        Me.Label4.Text = "Dataset Name:"
        '
        'txtDatasetName
        '
        Me.txtDatasetName.Location = New System.Drawing.Point(113, 45)
        Me.txtDatasetName.Name = "txtDatasetName"
        Me.txtDatasetName.ReadOnly = True
        Me.txtDatasetName.Size = New System.Drawing.Size(238, 20)
        Me.txtDatasetName.TabIndex = 320
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 128)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(57, 13)
        Me.Label5.TabIndex = 321
        Me.Label5.Text = "No. Rows:"
        '
        'txtNRows
        '
        Me.txtNRows.Location = New System.Drawing.Point(113, 125)
        Me.txtNRows.Name = "txtNRows"
        Me.txtNRows.ReadOnly = True
        Me.txtNRows.Size = New System.Drawing.Size(125, 20)
        Me.txtNRows.TabIndex = 322
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.cmbCalcTableName)
        Me.TabPage4.Controls.Add(Me.Label56)
        Me.TabPage4.Controls.Add(Me.dgvData)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(714, 662)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Data"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'cmbCalcTableName
        '
        Me.cmbCalcTableName.FormattingEnabled = True
        Me.cmbCalcTableName.Location = New System.Drawing.Point(78, 6)
        Me.cmbCalcTableName.Name = "cmbCalcTableName"
        Me.cmbCalcTableName.Size = New System.Drawing.Size(251, 21)
        Me.cmbCalcTableName.TabIndex = 297
        '
        'Label56
        '
        Me.Label56.AutoSize = True
        Me.Label56.Location = New System.Drawing.Point(6, 10)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(66, 13)
        Me.Label56.TabIndex = 296
        Me.Label56.Text = "Table name:"
        '
        'dgvData
        '
        Me.dgvData.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvData.Location = New System.Drawing.Point(3, 33)
        Me.dgvData.Name = "dgvData"
        Me.dgvData.Size = New System.Drawing.Size(732, 626)
        Me.dgvData.TabIndex = 1
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.GroupBox6)
        Me.TabPage3.Controls.Add(Me.GroupBox5)
        Me.TabPage3.Controls.Add(Me.GroupBox2)
        Me.TabPage3.Controls.Add(Me.GroupBox1)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(714, 662)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Settings"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.txtNSamps)
        Me.GroupBox6.Controls.Add(Me.Label39)
        Me.GroupBox6.Controls.Add(Me.btnUpdateCharts2)
        Me.GroupBox6.Controls.Add(Me.txtParamEValue)
        Me.GroupBox6.Controls.Add(Me.txtParamDValue)
        Me.GroupBox6.Controls.Add(Me.txtParamCValue)
        Me.GroupBox6.Controls.Add(Me.txtParamBValue)
        Me.GroupBox6.Controls.Add(Me.txtParamEName)
        Me.GroupBox6.Controls.Add(Me.txtParamDName)
        Me.GroupBox6.Controls.Add(Me.txtParamCName)
        Me.GroupBox6.Controls.Add(Me.txtParamBName)
        Me.GroupBox6.Controls.Add(Me.txtParamAValue)
        Me.GroupBox6.Controls.Add(Me.txtParamAName)
        Me.GroupBox6.Controls.Add(Me.Label35)
        Me.GroupBox6.Controls.Add(Me.txtContDisc)
        Me.GroupBox6.Controls.Add(Me.Label33)
        Me.GroupBox6.Controls.Add(Me.chkShowModel)
        Me.GroupBox6.Controls.Add(Me.txtDistributionName)
        Me.GroupBox6.Controls.Add(Me.Label32)
        Me.GroupBox6.Location = New System.Drawing.Point(6, 105)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(334, 251)
        Me.GroupBox6.TabIndex = 32
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Data Model:"
        '
        'btnUpdateCharts2
        '
        Me.btnUpdateCharts2.Location = New System.Drawing.Point(266, 15)
        Me.btnUpdateCharts2.Name = "btnUpdateCharts2"
        Me.btnUpdateCharts2.Size = New System.Drawing.Size(62, 22)
        Me.btnUpdateCharts2.TabIndex = 31
        Me.btnUpdateCharts2.Text = "Update"
        Me.btnUpdateCharts2.UseVisualStyleBackColor = True
        '
        'txtParamEValue
        '
        Me.txtParamEValue.Location = New System.Drawing.Point(170, 220)
        Me.txtParamEValue.Name = "txtParamEValue"
        Me.txtParamEValue.Size = New System.Drawing.Size(158, 20)
        Me.txtParamEValue.TabIndex = 17
        '
        'txtParamDValue
        '
        Me.txtParamDValue.Location = New System.Drawing.Point(170, 194)
        Me.txtParamDValue.Name = "txtParamDValue"
        Me.txtParamDValue.Size = New System.Drawing.Size(158, 20)
        Me.txtParamDValue.TabIndex = 16
        '
        'txtParamCValue
        '
        Me.txtParamCValue.Location = New System.Drawing.Point(170, 168)
        Me.txtParamCValue.Name = "txtParamCValue"
        Me.txtParamCValue.Size = New System.Drawing.Size(158, 20)
        Me.txtParamCValue.TabIndex = 15
        '
        'txtParamBValue
        '
        Me.txtParamBValue.Location = New System.Drawing.Point(170, 142)
        Me.txtParamBValue.Name = "txtParamBValue"
        Me.txtParamBValue.Size = New System.Drawing.Size(158, 20)
        Me.txtParamBValue.TabIndex = 14
        '
        'txtParamEName
        '
        Me.txtParamEName.Location = New System.Drawing.Point(6, 220)
        Me.txtParamEName.Name = "txtParamEName"
        Me.txtParamEName.Size = New System.Drawing.Size(158, 20)
        Me.txtParamEName.TabIndex = 13
        '
        'txtParamDName
        '
        Me.txtParamDName.Location = New System.Drawing.Point(6, 194)
        Me.txtParamDName.Name = "txtParamDName"
        Me.txtParamDName.Size = New System.Drawing.Size(158, 20)
        Me.txtParamDName.TabIndex = 12
        '
        'txtParamCName
        '
        Me.txtParamCName.Location = New System.Drawing.Point(6, 168)
        Me.txtParamCName.Name = "txtParamCName"
        Me.txtParamCName.Size = New System.Drawing.Size(158, 20)
        Me.txtParamCName.TabIndex = 11
        '
        'txtParamBName
        '
        Me.txtParamBName.Location = New System.Drawing.Point(6, 142)
        Me.txtParamBName.Name = "txtParamBName"
        Me.txtParamBName.Size = New System.Drawing.Size(158, 20)
        Me.txtParamBName.TabIndex = 10
        '
        'txtParamAValue
        '
        Me.txtParamAValue.Location = New System.Drawing.Point(170, 116)
        Me.txtParamAValue.Name = "txtParamAValue"
        Me.txtParamAValue.Size = New System.Drawing.Size(158, 20)
        Me.txtParamAValue.TabIndex = 9
        '
        'txtParamAName
        '
        Me.txtParamAName.Location = New System.Drawing.Point(6, 116)
        Me.txtParamAName.Name = "txtParamAName"
        Me.txtParamAName.Size = New System.Drawing.Size(158, 20)
        Me.txtParamAName.TabIndex = 8
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(6, 100)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(63, 13)
        Me.Label35.TabIndex = 7
        Me.Label35.Text = "Parameters:"
        '
        'txtContDisc
        '
        Me.txtContDisc.Location = New System.Drawing.Point(119, 68)
        Me.txtContDisc.Name = "txtContDisc"
        Me.txtContDisc.Size = New System.Drawing.Size(133, 20)
        Me.txtContDisc.TabIndex = 6
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(6, 73)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(107, 13)
        Me.Label33.TabIndex = 5
        Me.Label33.Text = "Continuous/Discrete:"
        '
        'chkShowModel
        '
        Me.chkShowModel.AutoSize = True
        Me.chkShowModel.Location = New System.Drawing.Point(6, 19)
        Me.chkShowModel.Name = "chkShowModel"
        Me.chkShowModel.Size = New System.Drawing.Size(137, 17)
        Me.chkShowModel.TabIndex = 4
        Me.chkShowModel.Text = "Show distribution model"
        Me.chkShowModel.UseVisualStyleBackColor = True
        '
        'txtDistributionName
        '
        Me.txtDistributionName.Location = New System.Drawing.Point(103, 42)
        Me.txtDistributionName.Name = "txtDistributionName"
        Me.txtDistributionName.Size = New System.Drawing.Size(225, 20)
        Me.txtDistributionName.TabIndex = 3
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(6, 45)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(91, 13)
        Me.Label32.TabIndex = 2
        Me.Label32.Text = "Distribution name:"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.rbCdf)
        Me.GroupBox5.Controls.Add(Me.btnUpdateCharts)
        Me.GroupBox5.Controls.Add(Me.rbReverseCdf)
        Me.GroupBox5.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(234, 93)
        Me.GroupBox5.TabIndex = 31
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Cumulative Distribution Function:"
        '
        'rbCdf
        '
        Me.rbCdf.AutoSize = True
        Me.rbCdf.Location = New System.Drawing.Point(6, 19)
        Me.rbCdf.Name = "rbCdf"
        Me.rbCdf.Size = New System.Drawing.Size(176, 17)
        Me.rbCdf.TabIndex = 28
        Me.rbCdf.TabStop = True
        Me.rbCdf.Text = "Cumulative Distribution Function"
        Me.rbCdf.UseVisualStyleBackColor = True
        '
        'btnUpdateCharts
        '
        Me.btnUpdateCharts.Location = New System.Drawing.Point(6, 65)
        Me.btnUpdateCharts.Name = "btnUpdateCharts"
        Me.btnUpdateCharts.Size = New System.Drawing.Size(62, 22)
        Me.btnUpdateCharts.TabIndex = 30
        Me.btnUpdateCharts.Text = "Update"
        Me.btnUpdateCharts.UseVisualStyleBackColor = True
        '
        'rbReverseCdf
        '
        Me.rbReverseCdf.AutoSize = True
        Me.rbReverseCdf.Location = New System.Drawing.Point(6, 42)
        Me.rbReverseCdf.Name = "rbReverseCdf"
        Me.rbReverseCdf.Size = New System.Drawing.Size(219, 17)
        Me.rbReverseCdf.TabIndex = 29
        Me.rbReverseCdf.TabStop = True
        Me.rbReverseCdf.Text = "Reverse Cumulative Distribution Function"
        Me.rbReverseCdf.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label27)
        Me.GroupBox2.Controls.Add(Me.txtLeft)
        Me.GroupBox2.Controls.Add(Me.Label28)
        Me.GroupBox2.Controls.Add(Me.Label24)
        Me.GroupBox2.Controls.Add(Me.txtTop)
        Me.GroupBox2.Controls.Add(Me.Label26)
        Me.GroupBox2.Controls.Add(Me.btnApplySize)
        Me.GroupBox2.Controls.Add(Me.Label23)
        Me.GroupBox2.Controls.Add(Me.Label22)
        Me.GroupBox2.Controls.Add(Me.txtHeight)
        Me.GroupBox2.Controls.Add(Me.Label37)
        Me.GroupBox2.Controls.Add(Me.txtWidth)
        Me.GroupBox2.Controls.Add(Me.Label34)
        Me.GroupBox2.Location = New System.Drawing.Point(346, 362)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(190, 156)
        Me.GroupBox2.TabIndex = 27
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Chart Window Size:"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(146, 100)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(33, 13)
        Me.Label27.TabIndex = 17
        Me.Label27.Text = "pixels"
        '
        'txtLeft
        '
        Me.txtLeft.Location = New System.Drawing.Point(50, 97)
        Me.txtLeft.Name = "txtLeft"
        Me.txtLeft.Size = New System.Drawing.Size(90, 20)
        Me.txtLeft.TabIndex = 16
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(6, 100)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(28, 13)
        Me.Label28.TabIndex = 15
        Me.Label28.Text = "Left:"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(146, 74)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(33, 13)
        Me.Label24.TabIndex = 14
        Me.Label24.Text = "pixels"
        '
        'txtTop
        '
        Me.txtTop.Location = New System.Drawing.Point(50, 71)
        Me.txtTop.Name = "txtTop"
        Me.txtTop.Size = New System.Drawing.Size(90, 20)
        Me.txtTop.TabIndex = 13
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(6, 74)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(29, 13)
        Me.Label26.TabIndex = 12
        Me.Label26.Text = "Top:"
        '
        'btnApplySize
        '
        Me.btnApplySize.Location = New System.Drawing.Point(50, 123)
        Me.btnApplySize.Name = "btnApplySize"
        Me.btnApplySize.Size = New System.Drawing.Size(62, 22)
        Me.btnApplySize.TabIndex = 11
        Me.btnApplySize.Text = "Apply"
        Me.btnApplySize.UseVisualStyleBackColor = True
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(146, 48)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(33, 13)
        Me.Label23.TabIndex = 8
        Me.Label23.Text = "pixels"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(146, 22)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(33, 13)
        Me.Label22.TabIndex = 7
        Me.Label22.Text = "pixels"
        '
        'txtHeight
        '
        Me.txtHeight.Location = New System.Drawing.Point(50, 45)
        Me.txtHeight.Name = "txtHeight"
        Me.txtHeight.Size = New System.Drawing.Size(90, 20)
        Me.txtHeight.TabIndex = 6
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(6, 48)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(41, 13)
        Me.Label37.TabIndex = 5
        Me.Label37.Text = "Height:"
        '
        'txtWidth
        '
        Me.txtWidth.Location = New System.Drawing.Point(50, 19)
        Me.txtWidth.Name = "txtWidth"
        Me.txtWidth.Size = New System.Drawing.Size(90, 20)
        Me.txtWidth.TabIndex = 2
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(6, 22)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(38, 13)
        Me.Label34.TabIndex = 1
        Me.Label34.Text = "Width:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnShowChartMinMax)
        Me.GroupBox1.Controls.Add(Me.btnUpdateChartMinMax)
        Me.GroupBox1.Controls.Add(Me.Label31)
        Me.GroupBox1.Controls.Add(Me.Label25)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.txtHistMin)
        Me.GroupBox1.Controls.Add(Me.Label30)
        Me.GroupBox1.Controls.Add(Me.txtHistMax)
        Me.GroupBox1.Controls.Add(Me.Label29)
        Me.GroupBox1.Controls.Add(Me.txtChartInterval)
        Me.GroupBox1.Controls.Add(Me.txtChartMax)
        Me.GroupBox1.Controls.Add(Me.txtChartMin)
        Me.GroupBox1.Controls.Add(Me.txtIntervalWidth)
        Me.GroupBox1.Controls.Add(Me.Label21)
        Me.GroupBox1.Controls.Add(Me.btnUpdateHistogram)
        Me.GroupBox1.Controls.Add(Me.txtDataMax)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.txtDataMin)
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.txtDataRange)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.txtNIntervals)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 362)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(334, 286)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Histogram Settings:"
        '
        'btnShowChartMinMax
        '
        Me.btnShowChartMinMax.Location = New System.Drawing.Point(196, 132)
        Me.btnShowChartMinMax.Name = "btnShowChartMinMax"
        Me.btnShowChartMinMax.Size = New System.Drawing.Size(56, 22)
        Me.btnShowChartMinMax.TabIndex = 27
        Me.btnShowChartMinMax.Text = "Show"
        Me.btnShowChartMinMax.UseVisualStyleBackColor = True
        '
        'btnUpdateChartMinMax
        '
        Me.btnUpdateChartMinMax.Location = New System.Drawing.Point(258, 132)
        Me.btnUpdateChartMinMax.Name = "btnUpdateChartMinMax"
        Me.btnUpdateChartMinMax.Size = New System.Drawing.Size(62, 22)
        Me.btnUpdateChartMinMax.TabIndex = 26
        Me.btnUpdateChartMinMax.Text = "Update"
        Me.btnUpdateChartMinMax.UseVisualStyleBackColor = True
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(145, 254)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(45, 13)
        Me.Label31.TabIndex = 25
        Me.Label31.Text = "Interval:"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(193, 157)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(35, 13)
        Me.Label25.TabIndex = 24
        Me.Label25.Text = "Chart:"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(63, 157)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(33, 13)
        Me.Label20.TabIndex = 23
        Me.Label20.Text = "Data:"
        '
        'txtHistMin
        '
        Me.txtHistMin.Location = New System.Drawing.Point(113, 28)
        Me.txtHistMin.Name = "txtHistMin"
        Me.txtHistMin.Size = New System.Drawing.Size(68, 20)
        Me.txtHistMin.TabIndex = 22
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(6, 31)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(100, 13)
        Me.Label30.TabIndex = 21
        Me.Label30.Text = "Histogram minimum:"
        '
        'txtHistMax
        '
        Me.txtHistMax.Location = New System.Drawing.Point(113, 54)
        Me.txtHistMax.Name = "txtHistMax"
        Me.txtHistMax.Size = New System.Drawing.Size(68, 20)
        Me.txtHistMax.TabIndex = 20
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(6, 57)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(103, 13)
        Me.Label29.TabIndex = 19
        Me.Label29.Text = "Histogram maximum:"
        '
        'txtChartInterval
        '
        Me.txtChartInterval.Location = New System.Drawing.Point(196, 251)
        Me.txtChartInterval.Name = "txtChartInterval"
        Me.txtChartInterval.Size = New System.Drawing.Size(124, 20)
        Me.txtChartInterval.TabIndex = 18
        '
        'txtChartMax
        '
        Me.txtChartMax.Location = New System.Drawing.Point(196, 199)
        Me.txtChartMax.Name = "txtChartMax"
        Me.txtChartMax.Size = New System.Drawing.Size(124, 20)
        Me.txtChartMax.TabIndex = 16
        '
        'txtChartMin
        '
        Me.txtChartMin.Location = New System.Drawing.Point(196, 173)
        Me.txtChartMin.Name = "txtChartMin"
        Me.txtChartMin.Size = New System.Drawing.Size(124, 20)
        Me.txtChartMin.TabIndex = 14
        '
        'txtIntervalWidth
        '
        Me.txtIntervalWidth.Location = New System.Drawing.Point(113, 106)
        Me.txtIntervalWidth.Name = "txtIntervalWidth"
        Me.txtIntervalWidth.Size = New System.Drawing.Size(68, 20)
        Me.txtIntervalWidth.TabIndex = 12
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(6, 108)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(73, 13)
        Me.Label21.TabIndex = 11
        Me.Label21.Text = "Interval width:"
        '
        'btnUpdateHistogram
        '
        Me.btnUpdateHistogram.Location = New System.Drawing.Point(187, 78)
        Me.btnUpdateHistogram.Name = "btnUpdateHistogram"
        Me.btnUpdateHistogram.Size = New System.Drawing.Size(62, 22)
        Me.btnUpdateHistogram.TabIndex = 10
        Me.btnUpdateHistogram.Text = "Update"
        Me.btnUpdateHistogram.UseVisualStyleBackColor = True
        '
        'txtDataMax
        '
        Me.txtDataMax.Location = New System.Drawing.Point(66, 199)
        Me.txtDataMax.Name = "txtDataMax"
        Me.txtDataMax.ReadOnly = True
        Me.txtDataMax.Size = New System.Drawing.Size(124, 20)
        Me.txtDataMax.TabIndex = 7
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(6, 202)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(54, 13)
        Me.Label19.TabIndex = 6
        Me.Label19.Text = "Maximum:"
        '
        'txtDataMin
        '
        Me.txtDataMin.Location = New System.Drawing.Point(66, 173)
        Me.txtDataMin.Name = "txtDataMin"
        Me.txtDataMin.ReadOnly = True
        Me.txtDataMin.Size = New System.Drawing.Size(124, 20)
        Me.txtDataMin.TabIndex = 5
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(6, 176)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(51, 13)
        Me.Label18.TabIndex = 4
        Me.Label18.Text = "Minimum:"
        '
        'txtDataRange
        '
        Me.txtDataRange.Location = New System.Drawing.Point(66, 225)
        Me.txtDataRange.Name = "txtDataRange"
        Me.txtDataRange.ReadOnly = True
        Me.txtDataRange.Size = New System.Drawing.Size(124, 20)
        Me.txtDataRange.TabIndex = 3
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(6, 228)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(42, 13)
        Me.Label17.TabIndex = 2
        Me.Label17.Text = "Range:"
        '
        'txtNIntervals
        '
        Me.txtNIntervals.Location = New System.Drawing.Point(113, 80)
        Me.txtNIntervals.Name = "txtNIntervals"
        Me.txtNIntervals.Size = New System.Drawing.Size(68, 20)
        Me.txtNIntervals.TabIndex = 1
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(6, 83)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(101, 13)
        Me.Label14.TabIndex = 0
        Me.Label14.Text = "Number of intervals:"
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.btnEditColor)
        Me.TabPage5.Controls.Add(Me.btnEditFont)
        Me.TabPage5.Controls.Add(Me.txtEditTitle)
        Me.TabPage5.Controls.Add(Me.NumericUpDown1)
        Me.TabPage5.Controls.Add(Me.btnEditTitle)
        Me.TabPage5.Controls.Add(Me.btnChartTitleColor)
        Me.TabPage5.Controls.Add(Me.btnChartTitleFont)
        Me.TabPage5.Controls.Add(Me.txtAddTitle)
        Me.TabPage5.Controls.Add(Me.btnAddTitle)
        Me.TabPage5.Controls.Add(Me.btnFormatHelp2)
        Me.TabPage5.Controls.Add(Me.btnUpdateAnnotation)
        Me.TabPage5.Controls.Add(Me.btnDeleteAnnot)
        Me.TabPage5.Controls.Add(Me.dgvAnnot)
        Me.TabPage5.Location = New System.Drawing.Point(4, 22)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Size = New System.Drawing.Size(714, 662)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "Annotation"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'btnEditColor
        '
        Me.btnEditColor.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEditColor.Location = New System.Drawing.Point(689, 423)
        Me.btnEditColor.Name = "btnEditColor"
        Me.btnEditColor.Size = New System.Drawing.Size(44, 22)
        Me.btnEditColor.TabIndex = 311
        Me.btnEditColor.Text = "Color"
        Me.btnEditColor.UseVisualStyleBackColor = True
        '
        'btnEditFont
        '
        Me.btnEditFont.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEditFont.Location = New System.Drawing.Point(643, 423)
        Me.btnEditFont.Name = "btnEditFont"
        Me.btnEditFont.Size = New System.Drawing.Size(40, 22)
        Me.btnEditFont.TabIndex = 310
        Me.btnEditFont.Text = "Font"
        Me.btnEditFont.UseVisualStyleBackColor = True
        '
        'txtEditTitle
        '
        Me.txtEditTitle.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtEditTitle.Location = New System.Drawing.Point(114, 424)
        Me.txtEditTitle.Name = "txtEditTitle"
        Me.txtEditTitle.Size = New System.Drawing.Size(523, 20)
        Me.txtEditTitle.TabIndex = 309
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.Location = New System.Drawing.Point(69, 424)
        Me.NumericUpDown1.Name = "NumericUpDown1"
        Me.NumericUpDown1.Size = New System.Drawing.Size(39, 20)
        Me.NumericUpDown1.TabIndex = 308
        '
        'btnEditTitle
        '
        Me.btnEditTitle.Location = New System.Drawing.Point(3, 424)
        Me.btnEditTitle.Name = "btnEditTitle"
        Me.btnEditTitle.Size = New System.Drawing.Size(60, 22)
        Me.btnEditTitle.TabIndex = 307
        Me.btnEditTitle.Text = "Edit Title"
        Me.btnEditTitle.UseVisualStyleBackColor = True
        '
        'btnChartTitleColor
        '
        Me.btnChartTitleColor.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnChartTitleColor.Location = New System.Drawing.Point(689, 380)
        Me.btnChartTitleColor.Name = "btnChartTitleColor"
        Me.btnChartTitleColor.Size = New System.Drawing.Size(44, 22)
        Me.btnChartTitleColor.TabIndex = 306
        Me.btnChartTitleColor.Text = "Color"
        Me.btnChartTitleColor.UseVisualStyleBackColor = True
        '
        'btnChartTitleFont
        '
        Me.btnChartTitleFont.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnChartTitleFont.Location = New System.Drawing.Point(643, 381)
        Me.btnChartTitleFont.Name = "btnChartTitleFont"
        Me.btnChartTitleFont.Size = New System.Drawing.Size(40, 22)
        Me.btnChartTitleFont.TabIndex = 305
        Me.btnChartTitleFont.Text = "Font"
        Me.btnChartTitleFont.UseVisualStyleBackColor = True
        '
        'txtAddTitle
        '
        Me.txtAddTitle.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtAddTitle.Location = New System.Drawing.Point(69, 382)
        Me.txtAddTitle.Name = "txtAddTitle"
        Me.txtAddTitle.Size = New System.Drawing.Size(568, 20)
        Me.txtAddTitle.TabIndex = 292
        '
        'btnAddTitle
        '
        Me.btnAddTitle.Location = New System.Drawing.Point(3, 381)
        Me.btnAddTitle.Name = "btnAddTitle"
        Me.btnAddTitle.Size = New System.Drawing.Size(60, 22)
        Me.btnAddTitle.TabIndex = 291
        Me.btnAddTitle.Text = "Add Title"
        Me.btnAddTitle.UseVisualStyleBackColor = True
        '
        'btnFormatHelp2
        '
        Me.btnFormatHelp2.Location = New System.Drawing.Point(125, 320)
        Me.btnFormatHelp2.Name = "btnFormatHelp2"
        Me.btnFormatHelp2.Size = New System.Drawing.Size(79, 22)
        Me.btnFormatHelp2.TabIndex = 290
        Me.btnFormatHelp2.Text = "Format Help"
        Me.btnFormatHelp2.UseVisualStyleBackColor = True
        '
        'btnUpdateAnnotation
        '
        Me.btnUpdateAnnotation.Location = New System.Drawing.Point(64, 320)
        Me.btnUpdateAnnotation.Name = "btnUpdateAnnotation"
        Me.btnUpdateAnnotation.Size = New System.Drawing.Size(55, 22)
        Me.btnUpdateAnnotation.TabIndex = 20
        Me.btnUpdateAnnotation.Text = "Update"
        Me.btnUpdateAnnotation.UseVisualStyleBackColor = True
        '
        'btnDeleteAnnot
        '
        Me.btnDeleteAnnot.Location = New System.Drawing.Point(3, 320)
        Me.btnDeleteAnnot.Name = "btnDeleteAnnot"
        Me.btnDeleteAnnot.Size = New System.Drawing.Size(55, 22)
        Me.btnDeleteAnnot.TabIndex = 19
        Me.btnDeleteAnnot.Text = "Delete"
        Me.btnDeleteAnnot.UseVisualStyleBackColor = True
        '
        'dgvAnnot
        '
        Me.dgvAnnot.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvAnnot.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvAnnot.Location = New System.Drawing.Point(3, 3)
        Me.dgvAnnot.Name = "dgvAnnot"
        Me.dgvAnnot.Size = New System.Drawing.Size(732, 311)
        Me.dgvAnnot.TabIndex = 0
        '
        'cmbTable
        '
        Me.cmbTable.FormattingEnabled = True
        Me.cmbTable.Location = New System.Drawing.Point(55, 12)
        Me.cmbTable.Name = "cmbTable"
        Me.cmbTable.Size = New System.Drawing.Size(217, 21)
        Me.cmbTable.TabIndex = 17
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(12, 15)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(37, 13)
        Me.Label36.TabIndex = 18
        Me.Label36.Text = "Table:"
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(278, 15)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(45, 13)
        Me.Label38.TabIndex = 19
        Me.Label38.Text = "Column:"
        '
        'cmbColumn
        '
        Me.cmbColumn.FormattingEnabled = True
        Me.cmbColumn.Location = New System.Drawing.Point(329, 12)
        Me.cmbColumn.Name = "cmbColumn"
        Me.cmbColumn.Size = New System.Drawing.Size(217, 21)
        Me.cmbColumn.TabIndex = 20
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Location = New System.Drawing.Point(258, 71)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(70, 13)
        Me.Label39.TabIndex = 32
        Me.Label39.Text = "No. Samples:"
        '
        'txtNSamps
        '
        Me.txtNSamps.Location = New System.Drawing.Point(259, 87)
        Me.txtNSamps.Name = "txtNSamps"
        Me.txtNSamps.Size = New System.Drawing.Size(69, 20)
        Me.txtNSamps.TabIndex = 33
        '
        'frmSeriesAnalysis
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(746, 740)
        Me.Controls.Add(Me.cmbColumn)
        Me.Controls.Add(Me.Label38)
        Me.Controls.Add(Me.Label36)
        Me.Controls.Add(Me.cmbTable)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnPlot)
        Me.Name = "frmSeriesAnalysis"
        Me.Text = "Series Analysis"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage4.PerformLayout()
        CType(Me.dgvData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabPage5.ResumeLayout(False)
        Me.TabPage5.PerformLayout()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvAnnot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnPlot As Button
    Friend WithEvents btnExit As Button
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents Chart1 As DataVisualization.Charting.Chart
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents txtMinFormat As TextBox
    Friend WithEvents Label15 As Label
    Friend WithEvents btnIsSample As Button
    Friend WithEvents Label12 As Label
    Friend WithEvents txtMaxFormat As TextBox
    Friend WithEvents rbPopulation As RadioButton
    Friend WithEvents txtVariance As TextBox
    Friend WithEvents rbSample As RadioButton
    Friend WithEvents txtSumFormat As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents txtAvgFormat As TextBox
    Friend WithEvents txtStdDev As TextBox
    Friend WithEvents txtStdDevFormat As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents txtVarFormat As TextBox
    Friend WithEvents txtAverage As TextBox
    Friend WithEvents btnFormatHelp As Button
    Friend WithEvents Label9 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents txtSum As TextBox
    Friend WithEvents txtMinimum As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents txtMaximum As TextBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents txtDataSource As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents lblColCount As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents cmbSourceDataTable As ComboBox
    Friend WithEvents lblColNo As Label
    Friend WithEvents lblTableNo As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents cmbSourceColumnName As ComboBox
    Friend WithEvents lblTableCount As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents txtDatasetName As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txtNRows As TextBox
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents cmbCalcTableName As ComboBox
    Friend WithEvents Label56 As Label
    Friend WithEvents dgvData As DataGridView
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents GroupBox6 As GroupBox
    Friend WithEvents btnUpdateCharts2 As Button
    Friend WithEvents txtParamEValue As TextBox
    Friend WithEvents txtParamDValue As TextBox
    Friend WithEvents txtParamCValue As TextBox
    Friend WithEvents txtParamBValue As TextBox
    Friend WithEvents txtParamEName As TextBox
    Friend WithEvents txtParamDName As TextBox
    Friend WithEvents txtParamCName As TextBox
    Friend WithEvents txtParamBName As TextBox
    Friend WithEvents txtParamAValue As TextBox
    Friend WithEvents txtParamAName As TextBox
    Friend WithEvents Label35 As Label
    Friend WithEvents txtContDisc As TextBox
    Friend WithEvents Label33 As Label
    Friend WithEvents chkShowModel As CheckBox
    Friend WithEvents txtDistributionName As TextBox
    Friend WithEvents Label32 As Label
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents rbCdf As RadioButton
    Friend WithEvents btnUpdateCharts As Button
    Friend WithEvents rbReverseCdf As RadioButton
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label27 As Label
    Friend WithEvents txtLeft As TextBox
    Friend WithEvents Label28 As Label
    Friend WithEvents Label24 As Label
    Friend WithEvents txtTop As TextBox
    Friend WithEvents Label26 As Label
    Friend WithEvents btnApplySize As Button
    Friend WithEvents Label23 As Label
    Friend WithEvents Label22 As Label
    Friend WithEvents txtHeight As TextBox
    Friend WithEvents Label37 As Label
    Friend WithEvents txtWidth As TextBox
    Friend WithEvents Label34 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents btnShowChartMinMax As Button
    Friend WithEvents btnUpdateChartMinMax As Button
    Friend WithEvents Label31 As Label
    Friend WithEvents Label25 As Label
    Friend WithEvents Label20 As Label
    Friend WithEvents txtHistMin As TextBox
    Friend WithEvents Label30 As Label
    Friend WithEvents txtHistMax As TextBox
    Friend WithEvents Label29 As Label
    Friend WithEvents txtChartInterval As TextBox
    Friend WithEvents txtChartMax As TextBox
    Friend WithEvents txtChartMin As TextBox
    Friend WithEvents txtIntervalWidth As TextBox
    Friend WithEvents Label21 As Label
    Friend WithEvents btnUpdateHistogram As Button
    Friend WithEvents txtDataMax As TextBox
    Friend WithEvents Label19 As Label
    Friend WithEvents txtDataMin As TextBox
    Friend WithEvents Label18 As Label
    Friend WithEvents txtDataRange As TextBox
    Friend WithEvents Label17 As Label
    Friend WithEvents txtNIntervals As TextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents TabPage5 As TabPage
    Friend WithEvents btnEditColor As Button
    Friend WithEvents btnEditFont As Button
    Friend WithEvents txtEditTitle As TextBox
    Friend WithEvents NumericUpDown1 As NumericUpDown
    Friend WithEvents btnEditTitle As Button
    Friend WithEvents btnChartTitleColor As Button
    Friend WithEvents btnChartTitleFont As Button
    Friend WithEvents txtAddTitle As TextBox
    Friend WithEvents btnAddTitle As Button
    Friend WithEvents btnFormatHelp2 As Button
    Friend WithEvents btnUpdateAnnotation As Button
    Friend WithEvents btnDeleteAnnot As Button
    Friend WithEvents dgvAnnot As DataGridView
    Friend WithEvents ColorDialog1 As ColorDialog
    Friend WithEvents FontDialog1 As FontDialog
    Friend WithEvents cmbTable As ComboBox
    Friend WithEvents Label36 As Label
    Friend WithEvents Label38 As Label
    Friend WithEvents cmbColumn As ComboBox
    Friend WithEvents txtNSamps As TextBox
    Friend WithEvents Label39 As Label
End Class
