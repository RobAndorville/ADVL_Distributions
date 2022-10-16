<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCalculations
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
        Me.TabControl2 = New System.Windows.Forms.TabControl()
        Me.TabPage6 = New System.Windows.Forms.TabPage()
        Me.dgvPDF = New System.Windows.Forms.DataGridView()
        Me.TabPage7 = New System.Windows.Forms.TabPage()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dgvPdfIntervalProb = New System.Windows.Forms.DataGridView()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.TabPage8 = New System.Windows.Forms.TabPage()
        Me.TabControl3 = New System.Windows.Forms.TabControl()
        Me.TabPage9 = New System.Windows.Forms.TabPage()
        Me.dgvPMF = New System.Windows.Forms.DataGridView()
        Me.TabPage10 = New System.Windows.Forms.TabPage()
        Me.dgvPmfIntervalProb = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.dgvCDF = New System.Windows.Forms.DataGridView()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.dgvRevCDF = New System.Windows.Forms.DataGridView()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.dgvInvCDF = New System.Windows.Forms.DataGridView()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.dgvInvRevCDF = New System.Windows.Forms.DataGridView()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.btnChartPdf = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btnAddStdDev = New System.Windows.Forms.Button()
        Me.btnAddMean = New System.Windows.Forms.Button()
        Me.txtNStdDev = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtN = New System.Windows.Forms.TextBox()
        Me.txtMean = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtDistribution = New System.Windows.Forms.TextBox()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabControl2.SuspendLayout()
        Me.TabPage6.SuspendLayout()
        CType(Me.dgvPDF, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage7.SuspendLayout()
        CType(Me.dgvPdfIntervalProb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage8.SuspendLayout()
        Me.TabControl3.SuspendLayout()
        Me.TabPage9.SuspendLayout()
        CType(Me.dgvPMF, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage10.SuspendLayout()
        CType(Me.dgvPmfIntervalProb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.dgvCDF, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.dgvRevCDF, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage4.SuspendLayout()
        CType(Me.dgvInvCDF, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage5.SuspendLayout()
        CType(Me.dgvInvRevCDF, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Location = New System.Drawing.Point(608, 12)
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
        Me.TabControl1.Controls.Add(Me.TabPage8)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Controls.Add(Me.TabPage5)
        Me.TabControl1.Location = New System.Drawing.Point(12, 157)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(644, 479)
        Me.TabControl1.TabIndex = 11
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.TabControl2)
        Me.TabPage1.Controls.Add(Me.Label13)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(636, 453)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "PDF"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabControl2
        '
        Me.TabControl2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl2.Controls.Add(Me.TabPage6)
        Me.TabControl2.Controls.Add(Me.TabPage7)
        Me.TabControl2.Location = New System.Drawing.Point(7, 50)
        Me.TabControl2.Name = "TabControl2"
        Me.TabControl2.SelectedIndex = 0
        Me.TabControl2.Size = New System.Drawing.Size(624, 397)
        Me.TabControl2.TabIndex = 5
        '
        'TabPage6
        '
        Me.TabPage6.Controls.Add(Me.dgvPDF)
        Me.TabPage6.Location = New System.Drawing.Point(4, 22)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage6.Size = New System.Drawing.Size(616, 371)
        Me.TabPage6.TabIndex = 0
        Me.TabPage6.Text = "Probability Density"
        Me.TabPage6.UseVisualStyleBackColor = True
        '
        'dgvPDF
        '
        Me.dgvPDF.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvPDF.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPDF.Location = New System.Drawing.Point(6, 35)
        Me.dgvPDF.Name = "dgvPDF"
        Me.dgvPDF.Size = New System.Drawing.Size(604, 330)
        Me.dgvPDF.TabIndex = 4
        '
        'TabPage7
        '
        Me.TabPage7.Controls.Add(Me.Label2)
        Me.TabPage7.Controls.Add(Me.dgvPdfIntervalProb)
        Me.TabPage7.Location = New System.Drawing.Point(4, 22)
        Me.TabPage7.Name = "TabPage7"
        Me.TabPage7.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage7.Size = New System.Drawing.Size(616, 371)
        Me.TabPage7.TabIndex = 1
        Me.TabPage7.Text = "Interval Probability"
        Me.TabPage7.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(556, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "The probability that the random variable will take a value greater than From Valu" &
    "e and less than or equal to To Value."
        '
        'dgvPdfIntervalProb
        '
        Me.dgvPdfIntervalProb.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvPdfIntervalProb.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPdfIntervalProb.Location = New System.Drawing.Point(6, 35)
        Me.dgvPdfIntervalProb.Name = "dgvPdfIntervalProb"
        Me.dgvPdfIntervalProb.Size = New System.Drawing.Size(604, 330)
        Me.dgvPdfIntervalProb.TabIndex = 5
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(6, 3)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(361, 13)
        Me.Label13.TabIndex = 2
        Me.Label13.Text = "Probability Density Function (PDF) calculations for a continuous distribution."
        '
        'TabPage8
        '
        Me.TabPage8.Controls.Add(Me.TabControl3)
        Me.TabPage8.Controls.Add(Me.Label1)
        Me.TabPage8.Location = New System.Drawing.Point(4, 22)
        Me.TabPage8.Name = "TabPage8"
        Me.TabPage8.Size = New System.Drawing.Size(636, 453)
        Me.TabPage8.TabIndex = 5
        Me.TabPage8.Text = "PMF"
        Me.TabPage8.UseVisualStyleBackColor = True
        '
        'TabControl3
        '
        Me.TabControl3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl3.Controls.Add(Me.TabPage9)
        Me.TabControl3.Controls.Add(Me.TabPage10)
        Me.TabControl3.Location = New System.Drawing.Point(7, 19)
        Me.TabControl3.Name = "TabControl3"
        Me.TabControl3.SelectedIndex = 0
        Me.TabControl3.Size = New System.Drawing.Size(624, 428)
        Me.TabControl3.TabIndex = 6
        '
        'TabPage9
        '
        Me.TabPage9.Controls.Add(Me.dgvPMF)
        Me.TabPage9.Location = New System.Drawing.Point(4, 22)
        Me.TabPage9.Name = "TabPage9"
        Me.TabPage9.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage9.Size = New System.Drawing.Size(616, 402)
        Me.TabPage9.TabIndex = 0
        Me.TabPage9.Text = "Probability Mass"
        Me.TabPage9.UseVisualStyleBackColor = True
        '
        'dgvPMF
        '
        Me.dgvPMF.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvPMF.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPMF.Location = New System.Drawing.Point(6, 35)
        Me.dgvPMF.Name = "dgvPMF"
        Me.dgvPMF.Size = New System.Drawing.Size(604, 361)
        Me.dgvPMF.TabIndex = 4
        '
        'TabPage10
        '
        Me.TabPage10.Controls.Add(Me.dgvPmfIntervalProb)
        Me.TabPage10.Location = New System.Drawing.Point(4, 22)
        Me.TabPage10.Name = "TabPage10"
        Me.TabPage10.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage10.Size = New System.Drawing.Size(616, 402)
        Me.TabPage10.TabIndex = 1
        Me.TabPage10.Text = "Interval Probability"
        Me.TabPage10.UseVisualStyleBackColor = True
        '
        'dgvPmfIntervalProb
        '
        Me.dgvPmfIntervalProb.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvPmfIntervalProb.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPmfIntervalProb.Location = New System.Drawing.Point(6, 35)
        Me.dgvPmfIntervalProb.Name = "dgvPmfIntervalProb"
        Me.dgvPmfIntervalProb.Size = New System.Drawing.Size(604, 361)
        Me.dgvPmfIntervalProb.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(337, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Probability Mass Function (PMF) calculations for a discrete distribution."
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Label20)
        Me.TabPage2.Controls.Add(Me.dgvCDF)
        Me.TabPage2.Controls.Add(Me.Label4)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(636, 453)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "CDF"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(6, 16)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(421, 13)
        Me.Label20.TabIndex = 6
        Me.Label20.Text = "CDF(x) = the probability that the random variable will take a value less than or " &
    "equal to x."
        '
        'dgvCDF
        '
        Me.dgvCDF.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvCDF.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCDF.Location = New System.Drawing.Point(6, 36)
        Me.dgvCDF.Name = "dgvCDF"
        Me.dgvCDF.Size = New System.Drawing.Size(624, 411)
        Me.dgvCDF.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(250, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Cumulative Distribution Function (CDF) calculations."
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Label21)
        Me.TabPage3.Controls.Add(Me.dgvRevCDF)
        Me.TabPage3.Controls.Add(Me.Label14)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(636, 453)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "RevCDF"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(6, 16)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(403, 13)
        Me.Label21.TabIndex = 7
        Me.Label21.Text = "RevCDF(x) = the probability that the random variable will take a value greater th" &
    "an x."
        '
        'dgvRevCDF
        '
        Me.dgvRevCDF.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvRevCDF.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvRevCDF.Location = New System.Drawing.Point(6, 36)
        Me.dgvRevCDF.Name = "dgvRevCDF"
        Me.dgvRevCDF.Size = New System.Drawing.Size(624, 411)
        Me.dgvRevCDF.TabIndex = 6
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(6, 3)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(313, 13)
        Me.Label14.TabIndex = 4
        Me.Label14.Text = "Reverse Cumulative Distribution Function (RevCDF) calculations."
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.dgvInvCDF)
        Me.TabPage4.Controls.Add(Me.Label15)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(636, 453)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "InvCDF"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'dgvInvCDF
        '
        Me.dgvInvCDF.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvInvCDF.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvInvCDF.Location = New System.Drawing.Point(6, 19)
        Me.dgvInvCDF.Name = "dgvInvCDF"
        Me.dgvInvCDF.Size = New System.Drawing.Size(624, 428)
        Me.dgvInvCDF.TabIndex = 7
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(6, 3)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(303, 13)
        Me.Label15.TabIndex = 4
        Me.Label15.Text = "Inverse Cumulative Distribution Function (InvCDF) calculations."
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.dgvInvRevCDF)
        Me.TabPage5.Controls.Add(Me.Label16)
        Me.TabPage5.Location = New System.Drawing.Point(4, 22)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Size = New System.Drawing.Size(636, 453)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "InvRevCDF"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'dgvInvRevCDF
        '
        Me.dgvInvRevCDF.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvInvRevCDF.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvInvRevCDF.Location = New System.Drawing.Point(6, 19)
        Me.dgvInvRevCDF.Name = "dgvInvRevCDF"
        Me.dgvInvRevCDF.Size = New System.Drawing.Size(624, 428)
        Me.dgvInvRevCDF.TabIndex = 8
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(6, 3)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(366, 13)
        Me.Label16.TabIndex = 5
        Me.Label16.Text = "Inverse Reverse Cumulative Distribution Function (InvRevCDF) calculations."
        '
        'btnChartPdf
        '
        Me.btnChartPdf.Location = New System.Drawing.Point(12, 12)
        Me.btnChartPdf.Name = "btnChartPdf"
        Me.btnChartPdf.Size = New System.Drawing.Size(43, 22)
        Me.btnChartPdf.TabIndex = 23
        Me.btnChartPdf.Text = "Chart"
        Me.btnChartPdf.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnAddStdDev)
        Me.GroupBox3.Controls.Add(Me.btnAddMean)
        Me.GroupBox3.Controls.Add(Me.txtNStdDev)
        Me.GroupBox3.Controls.Add(Me.Label19)
        Me.GroupBox3.Controls.Add(Me.txtN)
        Me.GroupBox3.Controls.Add(Me.txtMean)
        Me.GroupBox3.Controls.Add(Me.Label18)
        Me.GroupBox3.Controls.Add(Me.Label17)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 66)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(376, 85)
        Me.GroupBox3.TabIndex = 12
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Special Values"
        '
        'btnAddStdDev
        '
        Me.btnAddStdDev.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddStdDev.Location = New System.Drawing.Point(295, 51)
        Me.btnAddStdDev.Name = "btnAddStdDev"
        Me.btnAddStdDev.Size = New System.Drawing.Size(36, 22)
        Me.btnAddStdDev.TabIndex = 23
        Me.btnAddStdDev.Text = "Add"
        Me.btnAddStdDev.UseVisualStyleBackColor = True
        '
        'btnAddMean
        '
        Me.btnAddMean.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddMean.Location = New System.Drawing.Point(295, 25)
        Me.btnAddMean.Name = "btnAddMean"
        Me.btnAddMean.Size = New System.Drawing.Size(36, 22)
        Me.btnAddMean.TabIndex = 22
        Me.btnAddMean.Text = "Add"
        Me.btnAddMean.UseVisualStyleBackColor = True
        '
        'txtNStdDev
        '
        Me.txtNStdDev.Location = New System.Drawing.Point(168, 50)
        Me.txtNStdDev.Name = "txtNStdDev"
        Me.txtNStdDev.ReadOnly = True
        Me.txtNStdDev.Size = New System.Drawing.Size(121, 20)
        Me.txtNStdDev.TabIndex = 21
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(148, 53)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(14, 13)
        Me.Label19.TabIndex = 20
        Me.Label19.Text = "σ"
        '
        'txtN
        '
        Me.txtN.Location = New System.Drawing.Point(113, 50)
        Me.txtN.Name = "txtN"
        Me.txtN.Size = New System.Drawing.Size(29, 20)
        Me.txtN.TabIndex = 19
        '
        'txtMean
        '
        Me.txtMean.Location = New System.Drawing.Point(168, 24)
        Me.txtMean.Name = "txtMean"
        Me.txtMean.ReadOnly = True
        Me.txtMean.Size = New System.Drawing.Size(121, 20)
        Me.txtMean.TabIndex = 18
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(6, 53)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(101, 13)
        Me.Label18.TabIndex = 14
        Me.Label18.Text = "Standard Deviation:"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(70, 27)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(37, 13)
        Me.Label17.TabIndex = 13
        Me.Label17.Text = "Mean:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 43)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(114, 13)
        Me.Label3.TabIndex = 24
        Me.Label3.Text = "Probability distribution: "
        '
        'txtDistribution
        '
        Me.txtDistribution.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDistribution.Location = New System.Drawing.Point(132, 40)
        Me.txtDistribution.Name = "txtDistribution"
        Me.txtDistribution.ReadOnly = True
        Me.txtDistribution.Size = New System.Drawing.Size(524, 20)
        Me.txtDistribution.TabIndex = 25
        '
        'frmCalculations
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(668, 648)
        Me.Controls.Add(Me.txtDistribution)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnChartPdf)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.btnExit)
        Me.Name = "frmCalculations"
        Me.Text = "Calculations"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabControl2.ResumeLayout(False)
        Me.TabPage6.ResumeLayout(False)
        CType(Me.dgvPDF, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage7.ResumeLayout(False)
        Me.TabPage7.PerformLayout()
        CType(Me.dgvPdfIntervalProb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage8.ResumeLayout(False)
        Me.TabPage8.PerformLayout()
        Me.TabControl3.ResumeLayout(False)
        Me.TabPage9.ResumeLayout(False)
        CType(Me.dgvPMF, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage10.ResumeLayout(False)
        CType(Me.dgvPmfIntervalProb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.dgvCDF, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        CType(Me.dgvRevCDF, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage4.PerformLayout()
        CType(Me.dgvInvCDF, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage5.ResumeLayout(False)
        Me.TabPage5.PerformLayout()
        CType(Me.dgvInvRevCDF, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnExit As Button
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents dgvPDF As DataGridView
    Friend WithEvents Label13 As Label
    Friend WithEvents dgvCDF As DataGridView
    Friend WithEvents Label4 As Label
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents dgvRevCDF As DataGridView
    Friend WithEvents Label14 As Label
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents dgvInvCDF As DataGridView
    Friend WithEvents Label15 As Label
    Friend WithEvents TabPage5 As TabPage
    Friend WithEvents dgvInvRevCDF As DataGridView
    Friend WithEvents Label16 As Label
    Friend WithEvents TabControl2 As TabControl
    Friend WithEvents TabPage6 As TabPage
    Friend WithEvents TabPage7 As TabPage
    Friend WithEvents dgvPdfIntervalProb As DataGridView
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents txtNStdDev As TextBox
    Friend WithEvents Label19 As Label
    Friend WithEvents txtN As TextBox
    Friend WithEvents txtMean As TextBox
    Friend WithEvents Label18 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents TabPage8 As TabPage
    Friend WithEvents TabControl3 As TabControl
    Friend WithEvents TabPage9 As TabPage
    Friend WithEvents dgvPMF As DataGridView
    Friend WithEvents TabPage10 As TabPage
    Friend WithEvents dgvPmfIntervalProb As DataGridView
    Friend WithEvents Label1 As Label
    Friend WithEvents btnAddStdDev As Button
    Friend WithEvents btnAddMean As Button
    Friend WithEvents btnChartPdf As Button
    Friend WithEvents Label20 As Label
    Friend WithEvents Label21 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents txtDistribution As TextBox
End Class
