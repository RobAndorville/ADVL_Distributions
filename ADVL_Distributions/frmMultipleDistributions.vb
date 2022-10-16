Public Class frmMultipleDistributions
    'Specify multiple Distribution parameter sets.

#Region " Variable Declarations - All the variables used in this form and this application." '=================================================================================================

#End Region 'Variable Declarations ------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Properties - All the properties used in this form and this application" '============================================================================================================

    Private _selParamSet As Integer = 0 'The selected parameter set index number. (0 if none selected.)
    Property SelParamSet As Integer
        Get
            Return _selParamSet
        End Get
        Set(value As Integer)
            _selParamSet = value
            txtSelDistrib.Text = _selParamSet
            If SelParamSet = 0 Then
                ClearForm()
            Else
                UpdateForm()
            End If
        End Set
    End Property

    Private _nParamSets As Integer = 0 'The number of multiple parameter sets.

    Property NParamSets As Integer
        Get
            Return _nParamSets
        End Get
        Set(value As Integer)
            _nParamSets = value
            txtNDistribs.Text = _nParamSets
        End Set
    End Property

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

#End Region 'Properties -----------------------------------------------------------------------------------------------------------------------------------------------------------------------

#Region " Process XML files - Read and write XML files." '=====================================================================================================================================

    Private Sub SaveFormSettings()
        'Save the form settings in an XML document.
        Dim settingsData = <?xml version="1.0" encoding="utf-8"?>
                           <!---->
                           <FormSettings>
                               <Left><%= Me.Left %></Left>
                               <Top><%= Me.Top %></Top>
                               <Width><%= Me.Width %></Width>
                               <Height><%= Me.Height %></Height>
                               <!---->
                           </FormSettings>

        'Add code to include other settings to save after the comment line <!---->

        Dim SettingsFileName As String = "FormSettings_" & Main.ApplicationInfo.Name & "_" & Me.Text & ".xml"
        Main.Project.SaveXmlSettings(SettingsFileName, settingsData)
    End Sub

    Private Sub RestoreFormSettings()
        'Read the form settings from an XML document.

        Dim SettingsFileName As String = "FormSettings_" & Main.ApplicationInfo.Name & "_" & Me.Text & ".xml"

        If Main.Project.SettingsFileExists(SettingsFileName) Then
            Dim Settings As System.Xml.Linq.XDocument
            Main.Project.ReadXmlSettings(SettingsFileName, Settings)

            If IsNothing(Settings) Then 'There is no Settings XML data.
                Exit Sub
            End If

            'Restore form position and size:
            If Settings.<FormSettings>.<Left>.Value <> Nothing Then Me.Left = Settings.<FormSettings>.<Left>.Value
            If Settings.<FormSettings>.<Top>.Value <> Nothing Then Me.Top = Settings.<FormSettings>.<Top>.Value
            If Settings.<FormSettings>.<Height>.Value <> Nothing Then Me.Height = Settings.<FormSettings>.<Height>.Value
            If Settings.<FormSettings>.<Width>.Value <> Nothing Then Me.Width = Settings.<FormSettings>.<Width>.Value

            'Add code to read other saved setting here:

            CheckFormPos()
        End If
    End Sub

    Private Sub CheckFormPos()
        'Check that the form can be seen on a screen.

        Dim MinWidthVisible As Integer = 192 'Minimum number of X pixels visible. The form will be moved if this many form pixels are not visible.
        Dim MinHeightVisible As Integer = 64 'Minimum number of Y pixels visible. The form will be moved if this many form pixels are not visible.

        Dim FormRect As New Rectangle(Me.Left, Me.Top, Me.Width, Me.Height)
        Dim WARect As Rectangle = Screen.GetWorkingArea(FormRect) 'The Working Area rectangle - the usable area of the screen containing the form.

        'Check if the top of the form is above the top of the Working Area:
        If Me.Top < WARect.Top Then
            Me.Top = WARect.Top
        End If

        'Check if the top of the form is too close to the bottom of the Working Area:
        If (Me.Top + MinHeightVisible) > (WARect.Top + WARect.Height) Then
            Me.Top = WARect.Top + WARect.Height - MinHeightVisible
        End If

        'Check if the left edge of the form is too close to the right edge of the Working Area:
        If (Me.Left + MinWidthVisible) > (WARect.Left + WARect.Width) Then
            Me.Left = WARect.Left + WARect.Width - MinWidthVisible
        End If

        'Check if the right edge of the form is too close to the left edge of the Working Area:
        If (Me.Left + Me.Width - MinWidthVisible) < WARect.Left Then
            Me.Left = WARect.Left - Me.Width + MinWidthVisible
        End If

    End Sub

    Protected Overrides Sub WndProc(ByRef m As Message) 'Save the form settings before the form is minimised:
        If m.Msg = &H112 Then 'SysCommand
            If m.WParam.ToInt32 = &HF020 Then 'Form is being minimised
                SaveFormSettings()
            End If
        End If
        MyBase.WndProc(m)
    End Sub

#End Region 'Process XML Files ----------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Form Display Methods - Code used to display this form." '============================================================================================================================

    Private Sub Form_Load(sender As Object, e As EventArgs) Handles Me.Load
        RestoreFormSettings()   'Restore the form settings


        dgvParams.ColumnCount = 8
        dgvParams.Columns(0).HeaderText = "Name"
        dgvParams.Columns(0).ReadOnly = True

        dgvParams.Columns(1).HeaderText = "Symbol"

        dgvParams.Columns(2).HeaderText = "Value"
        dgvParams.Columns(3).HeaderText = "Type"
        dgvParams.Columns(3).ReadOnly = True
        dgvParams.Columns(4).HeaderText = "No. Type"
        dgvParams.Columns(4).ReadOnly = True
        dgvParams.Columns(5).HeaderText = "Min"
        dgvParams.Columns(5).ReadOnly = True
        dgvParams.Columns(6).HeaderText = "Max"
        dgvParams.Columns(6).ReadOnly = True
        dgvParams.Columns(7).HeaderText = "Description"
        'dgvParams.Columns(7).ReadOnly = True
        dgvParams.Columns(7).ReadOnly = False
        dgvParams.AllowUserToAddRows = False

        'dgvFields.ColumnCount = 6
        'dgvFields.ColumnCount = 7
        'dgvFields.ColumnCount = 8
        dgvFields.ColumnCount = 15
        dgvFields.Columns(0).HeaderText = "Name"
        dgvFields.Columns(0).ReadOnly = True
        dgvFields.Columns(1).HeaderText = "Valid"
        dgvFields.Columns(1).ReadOnly = True

        'Dim cboGenerate As New DataGridViewComboBoxColumn 'Used for selecting if the Field is generated.
        'cboGenerate.FlatStyle = FlatStyle.Flat
        'cboGenerate.Items.Add("True") 'Generate the Field.
        'cboGenerate.Items.Add("False") 'Do not Generate the Field.
        'dgvFields.Columns.Insert(2, cboGenerate)
        'dgvFields.Columns(2).HeaderText = "Generate"
        'dgvFields.Columns(2).Width = 150

        Dim chkGenerate As New DataGridViewCheckBoxColumn
        chkGenerate.FlatStyle = FlatStyle.Flat
        dgvFields.Columns.Insert(2, chkGenerate)
        dgvFields.Columns(2).HeaderText = "Generate"
        dgvFields.Columns(2).Width = 150

        Dim cboNumType As New DataGridViewComboBoxColumn 'Used for selecting the number type
        cboNumType.FlatStyle = FlatStyle.Flat
        cboNumType.Items.Add("Single")
        cboNumType.Items.Add("Double")
        cboNumType.Items.Add("Integer")
        cboNumType.Items.Add("Long")
        dgvFields.Columns.Insert(3, cboNumType)
        dgvFields.Columns(3).HeaderText = "Number Type"

        dgvFields.Columns(4).HeaderText = "Format"
        dgvFields.Columns(4).ReadOnly = False

        Dim cboAlignment As New DataGridViewComboBoxColumn 'Used for selecting the Field alignment
        cboAlignment.FlatStyle = FlatStyle.Flat
        cboAlignment.Items.Add("NotSet")
        cboAlignment.Items.Add("TopLeft")
        cboAlignment.Items.Add("TopCenter")
        cboAlignment.Items.Add("TopRight")
        cboAlignment.Items.Add("MiddleLeft")
        cboAlignment.Items.Add("MiddleCenter")
        cboAlignment.Items.Add("MiddleRight")
        cboAlignment.Items.Add("BottomLeft")
        cboAlignment.Items.Add("BottomCenter")
        cboAlignment.Items.Add("BottomRight")
        dgvFields.Columns.Insert(5, cboAlignment)
        dgvFields.Columns(5).HeaderText = "Alignment"
        dgvFields.Columns(5).Width = 150

        'dgvFields.Columns(6).HeaderText = "Units"
        'dgvFields.Columns(6).ReadOnly = False
        'dgvFields.Columns(7).HeaderText = "Label"
        'dgvFields.Columns(7).ReadOnly = False

        dgvFields.Columns(6).HeaderText = "Value Label"
        dgvFields.Columns(6).ReadOnly = False
        dgvFields.Columns(7).HeaderText = "Units"
        dgvFields.Columns(7).ReadOnly = False

        dgvFields.Columns(8).HeaderText = "Label Prefix"
        dgvFields.Columns(8).ReadOnly = False

        dgvFields.Columns(9).HeaderText = "Series Label"
        dgvFields.Columns(9).ReadOnly = False

        dgvFields.Columns(10).HeaderText = "Description"
        dgvFields.Columns(10).ReadOnly = False

        Dim cboMarkerFill As New DataGridViewComboBoxColumn 'Used for selecting a transparent marker (No Fill)
        cboMarkerFill.Items.Add("Yes")
        cboMarkerFill.Items.Add("No")
        'dgvFields.Columns.Insert(9, cboMarkerFill) 'Insert the combo box column used to select the Marker Fill
        dgvFields.Columns.Insert(11, cboMarkerFill) 'Insert the combo box column used to select the Marker Fill
        dgvFields.Columns(11).HeaderText = "Marker Fill"

        dgvFields.Columns(12).HeaderText = "Marker Color"

        dgvFields.Columns(13).HeaderText = "Border Color"

        dgvFields.Columns(14).HeaderText = "Border Width"

        Dim cboMarkerStyle As New DataGridViewComboBoxColumn 'Used for selecting the Marker Style
        For Each marker In [Enum].GetNames(GetType(DataVisualization.Charting.MarkerStyle))
            cboMarkerStyle.Items.Add(marker)
        Next
        dgvFields.Columns.Insert(15, cboMarkerStyle)
        dgvFields.Columns(15).HeaderText = "Marker Style"

        dgvFields.Columns(16).HeaderText = "Marker Size"

        dgvFields.Columns(17).HeaderText = "Marker Step"

        dgvFields.Columns(18).HeaderText = "Line Color"

        dgvFields.Columns(19).HeaderText = "Line Width"

        dgvFields.AllowUserToAddRows = False

        cmbDefMkrFill.Items.Add("Yes")
        cmbDefMkrFill.Items.Add("No")

        For Each marker In [Enum].GetNames(GetType(DataVisualization.Charting.MarkerStyle))
            cmbDefMkrStyle.Items.Add(marker)
        Next

        'NParamSets = Main.Distribution.MultiDistrib.Count
        NParamSets = Main.Distribution.Info.Count
        If NParamSets > 0 Then
            SelParamSet = 1
        Else
            SelParamSet = 0
        End If

        chkUpdateLabel.Checked = True

    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        'Exit the Form
        Me.Close() 'Close the form
    End Sub

    Private Sub Form_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If WindowState = FormWindowState.Normal Then
            SaveFormSettings()
        Else
            'Dont save settings if the form is minimised.
        End If
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        'Add a new parameter set to the list.

        Main.Distribution.Info.Add(New DistributionInfo)

        NParamSets = Main.Distribution.Info.Count
        Dim MultiDistribNo As Integer = NParamSets

        ''Copy the original distribution to the new Multi Distribution:
        'Copy the previous Distribution to the new Distribution:
        'Main.Distribution.Info(MultiDistribNo - 1).NParams = Main.Distribution.Distrib.NParams
        Main.Distribution.Info(MultiDistribNo - 1).NParams = Main.Distribution.Info(MultiDistribNo - 2).NParams
        Main.Distribution.Info(MultiDistribNo - 1).Name = Main.Distribution.Info(MultiDistribNo - 2).Name
        Main.Distribution.Info(MultiDistribNo - 1).Continuity = Main.Distribution.Info(MultiDistribNo - 2).Continuity
        Main.Distribution.Info(MultiDistribNo - 1).RangeMax = Main.Distribution.Info(MultiDistribNo - 2).RangeMax
        Main.Distribution.Info(MultiDistribNo - 1).RangeMin = Main.Distribution.Info(MultiDistribNo - 2).RangeMin

        If MultiDistribNo > 21 Then 'Use a random color for the default color.
            Dim myRandom As New Random
            Main.Distribution.Info(MultiDistribNo - 1).Display.MarkerColor = Color.FromArgb(255, myRandom.Next(0, 255), myRandom.Next(0, 255), myRandom.Next(0, 255))
            Main.Distribution.Info(MultiDistribNo - 1).Display.LineColor = Main.Distribution.Info(MultiDistribNo - 1).Display.MarkerColor
        Else 'Use the list of distinct colors for the default color.
            Main.Distribution.Info(MultiDistribNo - 1).Display.MarkerColor = Main.Distribution.DistinctCols(MultiDistribNo)
            Main.Distribution.Info(MultiDistribNo - 1).Display.LineColor = Main.Distribution.DistinctCols(MultiDistribNo)
        End If

        ''Main.Distribution.MultiDistrib(MultiDistribNo - 1).ParamA = Main.Distribution.Distrib.ParamA 'THIS CREATES A LINK NOT A COPY OF ParamA!
        'Main.Distribution.MultiDistrib(MultiDistribNo - 1).ParamA = Main.Distribution.MultiDistrib(MultiDistribNo - 2).ParamA 'THIS CREATES A LINK NOT A COPY OF ParamA!
        'Main.Distribution.Info(MultiDistribNo - 1).ParamA.Name = Main.Distribution.Distrib.ParamA.Name
        Main.Distribution.Info(MultiDistribNo - 1).ParamA.Name = Main.Distribution.Info(MultiDistribNo - 2).ParamA.Name
        Main.Distribution.Info(MultiDistribNo - 1).ParamA.Symbol = Main.Distribution.Info(MultiDistribNo - 2).ParamA.Symbol
        Main.Distribution.Info(MultiDistribNo - 1).ParamA.Value = Main.Distribution.Info(MultiDistribNo - 2).ParamA.Value
        Main.Distribution.Info(MultiDistribNo - 1).ParamA.Type = Main.Distribution.Info(MultiDistribNo - 2).ParamA.Type
        Main.Distribution.Info(MultiDistribNo - 1).ParamA.NumberType = Main.Distribution.Info(MultiDistribNo - 2).ParamA.NumberType
        Main.Distribution.Info(MultiDistribNo - 1).ParamA.Minimum = Main.Distribution.Info(MultiDistribNo - 2).ParamA.Minimum
        Main.Distribution.Info(MultiDistribNo - 1).ParamA.Maximum = Main.Distribution.Info(MultiDistribNo - 2).ParamA.Maximum
        Main.Distribution.Info(MultiDistribNo - 1).ParamA.Increment = Main.Distribution.Info(MultiDistribNo - 2).ParamA.Increment
        Main.Distribution.Info(MultiDistribNo - 1).ParamA.Description = Main.Distribution.Info(MultiDistribNo - 2).ParamA.Description

        Main.Distribution.Info(MultiDistribNo - 1).ParamB.Name = Main.Distribution.Info(MultiDistribNo - 2).ParamB.Name
        Main.Distribution.Info(MultiDistribNo - 1).ParamB.Symbol = Main.Distribution.Info(MultiDistribNo - 2).ParamB.Symbol
        Main.Distribution.Info(MultiDistribNo - 1).ParamB.Value = Main.Distribution.Info(MultiDistribNo - 2).ParamB.Value
        Main.Distribution.Info(MultiDistribNo - 1).ParamB.Type = Main.Distribution.Info(MultiDistribNo - 2).ParamB.Type
        Main.Distribution.Info(MultiDistribNo - 1).ParamB.NumberType = Main.Distribution.Info(MultiDistribNo - 2).ParamB.NumberType
        Main.Distribution.Info(MultiDistribNo - 1).ParamB.Minimum = Main.Distribution.Info(MultiDistribNo - 2).ParamB.Minimum
        Main.Distribution.Info(MultiDistribNo - 1).ParamB.Maximum = Main.Distribution.Info(MultiDistribNo - 2).ParamB.Maximum
        Main.Distribution.Info(MultiDistribNo - 1).ParamB.Increment = Main.Distribution.Info(MultiDistribNo - 2).ParamB.Increment
        Main.Distribution.Info(MultiDistribNo - 1).ParamB.Description = Main.Distribution.Info(MultiDistribNo - 2).ParamB.Description

        Main.Distribution.Info(MultiDistribNo - 1).ParamC.Name = Main.Distribution.Info(MultiDistribNo - 2).ParamC.Name
        Main.Distribution.Info(MultiDistribNo - 1).ParamC.Symbol = Main.Distribution.Info(MultiDistribNo - 2).ParamC.Symbol
        Main.Distribution.Info(MultiDistribNo - 1).ParamC.Value = Main.Distribution.Info(MultiDistribNo - 2).ParamC.Value
        Main.Distribution.Info(MultiDistribNo - 1).ParamC.Type = Main.Distribution.Info(MultiDistribNo - 2).ParamC.Type
        Main.Distribution.Info(MultiDistribNo - 1).ParamC.NumberType = Main.Distribution.Info(MultiDistribNo - 2).ParamC.NumberType
        Main.Distribution.Info(MultiDistribNo - 1).ParamC.Minimum = Main.Distribution.Info(MultiDistribNo - 2).ParamC.Minimum
        Main.Distribution.Info(MultiDistribNo - 1).ParamC.Maximum = Main.Distribution.Info(MultiDistribNo - 2).ParamC.Maximum
        Main.Distribution.Info(MultiDistribNo - 1).ParamC.Increment = Main.Distribution.Info(MultiDistribNo - 2).ParamC.Increment
        Main.Distribution.Info(MultiDistribNo - 1).ParamC.Description = Main.Distribution.Info(MultiDistribNo - 2).ParamC.Description

        Main.Distribution.Info(MultiDistribNo - 1).ParamD.Name = Main.Distribution.Info(MultiDistribNo - 2).ParamD.Name
        Main.Distribution.Info(MultiDistribNo - 1).ParamD.Symbol = Main.Distribution.Info(MultiDistribNo - 2).ParamD.Symbol
        Main.Distribution.Info(MultiDistribNo - 1).ParamD.Value = Main.Distribution.Info(MultiDistribNo - 2).ParamD.Value
        Main.Distribution.Info(MultiDistribNo - 1).ParamD.Type = Main.Distribution.Info(MultiDistribNo - 2).ParamD.Type
        Main.Distribution.Info(MultiDistribNo - 1).ParamD.NumberType = Main.Distribution.Info(MultiDistribNo - 2).ParamD.NumberType
        Main.Distribution.Info(MultiDistribNo - 1).ParamD.Minimum = Main.Distribution.Info(MultiDistribNo - 2).ParamD.Minimum
        Main.Distribution.Info(MultiDistribNo - 1).ParamD.Maximum = Main.Distribution.Info(MultiDistribNo - 2).ParamD.Maximum
        Main.Distribution.Info(MultiDistribNo - 1).ParamD.Increment = Main.Distribution.Info(MultiDistribNo - 2).ParamD.Increment
        Main.Distribution.Info(MultiDistribNo - 1).ParamD.Description = Main.Distribution.Info(MultiDistribNo - 2).ParamD.Description

        Main.Distribution.Info(MultiDistribNo - 1).ParamE.Name = Main.Distribution.Info(MultiDistribNo - 2).ParamE.Name
        Main.Distribution.Info(MultiDistribNo - 1).ParamE.Symbol = Main.Distribution.Info(MultiDistribNo - 2).ParamE.Symbol
        Main.Distribution.Info(MultiDistribNo - 1).ParamE.Value = Main.Distribution.Info(MultiDistribNo - 2).ParamE.Value
        Main.Distribution.Info(MultiDistribNo - 1).ParamE.Type = Main.Distribution.Info(MultiDistribNo - 2).ParamE.Type
        Main.Distribution.Info(MultiDistribNo - 1).ParamE.NumberType = Main.Distribution.Info(MultiDistribNo - 2).ParamE.NumberType
        Main.Distribution.Info(MultiDistribNo - 1).ParamE.Minimum = Main.Distribution.Info(MultiDistribNo - 2).ParamE.Minimum
        Main.Distribution.Info(MultiDistribNo - 1).ParamE.Maximum = Main.Distribution.Info(MultiDistribNo - 2).ParamE.Maximum
        Main.Distribution.Info(MultiDistribNo - 1).ParamE.Increment = Main.Distribution.Info(MultiDistribNo - 2).ParamE.Increment
        Main.Distribution.Info(MultiDistribNo - 1).ParamE.Description = Main.Distribution.Info(MultiDistribNo - 2).ParamE.Description

        Main.Distribution.Info(MultiDistribNo - 1).PdfInfo.Name = Main.Distribution.Info(MultiDistribNo - 2).PdfInfo.Name & "_" & MultiDistribNo 'e.g. The first multiple distribution will have the PDF data named PDF_1
        Main.Distribution.Info(MultiDistribNo - 1).PdfInfo.Valid = Main.Distribution.Info(MultiDistribNo - 2).PdfInfo.Valid
        Main.Distribution.Info(MultiDistribNo - 1).PdfInfo.Generate = Main.Distribution.Info(MultiDistribNo - 2).PdfInfo.Generate
        Main.Distribution.Info(MultiDistribNo - 1).PdfInfo.NumType = Main.Distribution.Info(MultiDistribNo - 2).PdfInfo.NumType
        Main.Distribution.Info(MultiDistribNo - 1).PdfInfo.Format = Main.Distribution.Info(MultiDistribNo - 2).PdfInfo.Format
        Main.Distribution.Info(MultiDistribNo - 1).PdfInfo.Alignment = Main.Distribution.Info(MultiDistribNo - 2).PdfInfo.Alignment
        Main.Distribution.Info(MultiDistribNo - 1).PdfInfo.ValueLabel = Main.Distribution.Info(MultiDistribNo - 2).PdfInfo.ValueLabel
        Main.Distribution.Info(MultiDistribNo - 1).PdfInfo.Units = Main.Distribution.Info(MultiDistribNo - 2).PdfInfo.Units
        Main.Distribution.Info(MultiDistribNo - 1).PdfInfo.LabelPrefix = Main.Distribution.Info(MultiDistribNo - 2).PdfInfo.LabelPrefix
        Main.Distribution.Info(MultiDistribNo - 1).PdfInfo.SeriesLabel = Main.Distribution.Info(MultiDistribNo - 2).PdfInfo.SeriesLabel
        Main.Distribution.Info(MultiDistribNo - 1).PdfInfo.Legend = Main.Distribution.Info(MultiDistribNo - 2).PdfInfo.Legend
        Main.Distribution.Info(MultiDistribNo - 1).PdfInfo.Description = Main.Distribution.Info(MultiDistribNo - 2).PdfInfo.Description

        Main.Distribution.Info(MultiDistribNo - 1).PdfLnInfo.Name = Main.Distribution.Info(MultiDistribNo - 2).PdfLnInfo.Name & "_" & MultiDistribNo 'e.g. The first multiple distribution will have the PDF data named PDF_1
        Main.Distribution.Info(MultiDistribNo - 1).PdfLnInfo.Valid = Main.Distribution.Info(MultiDistribNo - 2).PdfLnInfo.Valid
        Main.Distribution.Info(MultiDistribNo - 1).PdfLnInfo.Generate = Main.Distribution.Info(MultiDistribNo - 2).PdfLnInfo.Generate
        Main.Distribution.Info(MultiDistribNo - 1).PdfLnInfo.NumType = Main.Distribution.Info(MultiDistribNo - 2).PdfLnInfo.NumType
        Main.Distribution.Info(MultiDistribNo - 1).PdfLnInfo.Format = Main.Distribution.Info(MultiDistribNo - 2).PdfLnInfo.Format
        Main.Distribution.Info(MultiDistribNo - 1).PdfLnInfo.Alignment = Main.Distribution.Info(MultiDistribNo - 2).PdfLnInfo.Alignment
        Main.Distribution.Info(MultiDistribNo - 1).PdfLnInfo.ValueLabel = Main.Distribution.Info(MultiDistribNo - 2).PdfLnInfo.ValueLabel
        Main.Distribution.Info(MultiDistribNo - 1).PdfLnInfo.Units = Main.Distribution.Info(MultiDistribNo - 2).PdfLnInfo.Units
        Main.Distribution.Info(MultiDistribNo - 1).PdfLnInfo.LabelPrefix = Main.Distribution.Info(MultiDistribNo - 2).PdfLnInfo.LabelPrefix
        Main.Distribution.Info(MultiDistribNo - 1).PdfLnInfo.SeriesLabel = Main.Distribution.Info(MultiDistribNo - 2).PdfLnInfo.SeriesLabel
        Main.Distribution.Info(MultiDistribNo - 1).PdfLnInfo.Legend = Main.Distribution.Info(MultiDistribNo - 2).PdfLnInfo.Legend
        Main.Distribution.Info(MultiDistribNo - 1).PdfLnInfo.Description = Main.Distribution.Info(MultiDistribNo - 2).PdfLnInfo.Description

        Main.Distribution.Info(MultiDistribNo - 1).PmfInfo.Name = Main.Distribution.Info(MultiDistribNo - 2).PmfInfo.Name & "_" & MultiDistribNo 'e.g. The first multiple distribution will have the PDF data named PDF_1
        Main.Distribution.Info(MultiDistribNo - 1).PmfInfo.Valid = Main.Distribution.Info(MultiDistribNo - 2).PmfInfo.Valid
        Main.Distribution.Info(MultiDistribNo - 1).PmfInfo.Generate = Main.Distribution.Info(MultiDistribNo - 2).PmfInfo.Generate
        Main.Distribution.Info(MultiDistribNo - 1).PmfInfo.NumType = Main.Distribution.Info(MultiDistribNo - 2).PmfInfo.NumType
        Main.Distribution.Info(MultiDistribNo - 1).PmfInfo.Format = Main.Distribution.Info(MultiDistribNo - 2).PmfInfo.Format
        Main.Distribution.Info(MultiDistribNo - 1).PmfInfo.Alignment = Main.Distribution.Info(MultiDistribNo - 2).PmfInfo.Alignment
        Main.Distribution.Info(MultiDistribNo - 1).PmfInfo.ValueLabel = Main.Distribution.Info(MultiDistribNo - 2).PmfInfo.ValueLabel
        Main.Distribution.Info(MultiDistribNo - 1).PmfInfo.Units = Main.Distribution.Info(MultiDistribNo - 2).PmfInfo.Units
        Main.Distribution.Info(MultiDistribNo - 1).PmfInfo.LabelPrefix = Main.Distribution.Info(MultiDistribNo - 2).PmfInfo.LabelPrefix
        Main.Distribution.Info(MultiDistribNo - 1).PmfInfo.SeriesLabel = Main.Distribution.Info(MultiDistribNo - 2).PmfInfo.SeriesLabel
        Main.Distribution.Info(MultiDistribNo - 1).PmfInfo.Legend = Main.Distribution.Info(MultiDistribNo - 2).PmfInfo.Legend
        Main.Distribution.Info(MultiDistribNo - 1).PmfInfo.Description = Main.Distribution.Info(MultiDistribNo - 2).PmfInfo.Description

        Main.Distribution.Info(MultiDistribNo - 1).PmfLnInfo.Name = Main.Distribution.Info(MultiDistribNo - 2).PmfLnInfo.Name & "_" & MultiDistribNo 'e.g. The first multiple distribution will have the PDF data named PDF_1
        Main.Distribution.Info(MultiDistribNo - 1).PmfLnInfo.Valid = Main.Distribution.Info(MultiDistribNo - 2).PmfLnInfo.Valid
        Main.Distribution.Info(MultiDistribNo - 1).PmfLnInfo.Generate = Main.Distribution.Info(MultiDistribNo - 2).PmfLnInfo.Generate
        Main.Distribution.Info(MultiDistribNo - 1).PmfLnInfo.NumType = Main.Distribution.Info(MultiDistribNo - 2).PmfLnInfo.NumType
        Main.Distribution.Info(MultiDistribNo - 1).PmfLnInfo.Format = Main.Distribution.Info(MultiDistribNo - 2).PmfLnInfo.Format
        Main.Distribution.Info(MultiDistribNo - 1).PmfLnInfo.Alignment = Main.Distribution.Info(MultiDistribNo - 2).PmfLnInfo.Alignment
        Main.Distribution.Info(MultiDistribNo - 1).PmfLnInfo.ValueLabel = Main.Distribution.Info(MultiDistribNo - 2).PmfLnInfo.ValueLabel
        Main.Distribution.Info(MultiDistribNo - 1).PmfLnInfo.Units = Main.Distribution.Info(MultiDistribNo - 2).PmfLnInfo.Units
        Main.Distribution.Info(MultiDistribNo - 1).PmfLnInfo.LabelPrefix = Main.Distribution.Info(MultiDistribNo - 2).PmfLnInfo.LabelPrefix
        Main.Distribution.Info(MultiDistribNo - 1).PmfLnInfo.SeriesLabel = Main.Distribution.Info(MultiDistribNo - 2).PmfLnInfo.SeriesLabel
        Main.Distribution.Info(MultiDistribNo - 1).PmfLnInfo.Legend = Main.Distribution.Info(MultiDistribNo - 2).PmfLnInfo.Legend
        Main.Distribution.Info(MultiDistribNo - 1).PmfLnInfo.Description = Main.Distribution.Info(MultiDistribNo - 2).PmfLnInfo.Description

        Main.Distribution.Info(MultiDistribNo - 1).CdfInfo.Name = Main.Distribution.Info(MultiDistribNo - 2).CdfInfo.Name & "_" & MultiDistribNo 'e.g. The first multiple distribution will have the PDF data named PDF_1
        Main.Distribution.Info(MultiDistribNo - 1).CdfInfo.Valid = Main.Distribution.Info(MultiDistribNo - 2).CdfInfo.Valid
        Main.Distribution.Info(MultiDistribNo - 1).CdfInfo.Generate = Main.Distribution.Info(MultiDistribNo - 2).CdfInfo.Generate
        Main.Distribution.Info(MultiDistribNo - 1).CdfInfo.NumType = Main.Distribution.Info(MultiDistribNo - 2).CdfInfo.NumType
        Main.Distribution.Info(MultiDistribNo - 1).CdfInfo.Format = Main.Distribution.Info(MultiDistribNo - 2).CdfInfo.Format
        Main.Distribution.Info(MultiDistribNo - 1).CdfInfo.Alignment = Main.Distribution.Info(MultiDistribNo - 2).CdfInfo.Alignment
        Main.Distribution.Info(MultiDistribNo - 1).CdfInfo.ValueLabel = Main.Distribution.Info(MultiDistribNo - 2).CdfInfo.ValueLabel
        Main.Distribution.Info(MultiDistribNo - 1).CdfInfo.Units = Main.Distribution.Info(MultiDistribNo - 2).CdfInfo.Units
        Main.Distribution.Info(MultiDistribNo - 1).CdfInfo.LabelPrefix = Main.Distribution.Info(MultiDistribNo - 2).CdfInfo.LabelPrefix
        Main.Distribution.Info(MultiDistribNo - 1).CdfInfo.SeriesLabel = Main.Distribution.Info(MultiDistribNo - 2).CdfInfo.SeriesLabel
        Main.Distribution.Info(MultiDistribNo - 1).CdfInfo.Legend = Main.Distribution.Info(MultiDistribNo - 2).CdfInfo.Legend
        Main.Distribution.Info(MultiDistribNo - 1).CdfInfo.Description = Main.Distribution.Info(MultiDistribNo - 2).CdfInfo.Description

        Main.Distribution.Info(MultiDistribNo - 1).InvCdfInfo.Name = Main.Distribution.Info(MultiDistribNo - 2).InvCdfInfo.Name & "_" & MultiDistribNo 'e.g. The first multiple distribution will have the PDF data named PDF_1
        Main.Distribution.Info(MultiDistribNo - 1).InvCdfInfo.Valid = Main.Distribution.Info(MultiDistribNo - 2).InvCdfInfo.Valid
        Main.Distribution.Info(MultiDistribNo - 1).InvCdfInfo.Generate = Main.Distribution.Info(MultiDistribNo - 2).InvCdfInfo.Generate
        Main.Distribution.Info(MultiDistribNo - 1).InvCdfInfo.NumType = Main.Distribution.Info(MultiDistribNo - 2).InvCdfInfo.NumType
        Main.Distribution.Info(MultiDistribNo - 1).InvCdfInfo.Format = Main.Distribution.Info(MultiDistribNo - 2).InvCdfInfo.Format
        Main.Distribution.Info(MultiDistribNo - 1).InvCdfInfo.Alignment = Main.Distribution.Info(MultiDistribNo - 2).InvCdfInfo.Alignment
        Main.Distribution.Info(MultiDistribNo - 1).InvCdfInfo.ValueLabel = Main.Distribution.Info(MultiDistribNo - 2).InvCdfInfo.ValueLabel
        Main.Distribution.Info(MultiDistribNo - 1).InvCdfInfo.Units = Main.Distribution.Info(MultiDistribNo - 2).InvCdfInfo.Units
        Main.Distribution.Info(MultiDistribNo - 1).InvCdfInfo.LabelPrefix = Main.Distribution.Info(MultiDistribNo - 2).InvCdfInfo.LabelPrefix
        Main.Distribution.Info(MultiDistribNo - 1).InvCdfInfo.SeriesLabel = Main.Distribution.Info(MultiDistribNo - 2).InvCdfInfo.SeriesLabel
        Main.Distribution.Info(MultiDistribNo - 1).InvCdfInfo.Legend = Main.Distribution.Info(MultiDistribNo - 2).InvCdfInfo.Legend
        Main.Distribution.Info(MultiDistribNo - 1).InvCdfInfo.Description = Main.Distribution.Info(MultiDistribNo - 2).InvCdfInfo.Description

        Main.Distribution.Info(MultiDistribNo - 1).Suffix = Main.Distribution.Info(MultiDistribNo - 2).Suffix

        SelParamSet = MultiDistribNo

        'Generate the distribution data:
        If Main.Distribution.Data.Tables.Contains("DataTable") Then
            Main.Distribution.UpdateData(MultiDistribNo) 'Just add the data from the new distribution.
            Main.UpdateDataTableDisplay()
        Else
            Main.Distribution.GenerateData() 'Generate all the data
            Main.UpdateDataTableDisplay()
        End If

    End Sub

#End Region 'Form Display Methods -------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Open and Close Forms - Code used to open and close other forms." '===================================================================================================================

#End Region 'Open and Close Forms -------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Form Methods - The main actions performed by this form." '===========================================================================================================================


    Private Sub UpdateForm()
        'Update the form with the selected parameter set.

        ''Update the Model Summary tab: -----------------------------------------------------------
        'txtFileName.Text = Distribution.FileName
        'txtModelName.Text = Distribution.ModelName
        'txtLabel.Text = Distribution.Label
        'txtDescription.Text = Distribution.Description
        'txtNotes.Text = Distribution.Notes

        'Update the Distribution tab: ------------------------------------------------------------
        txtDistName.Text = Main.Distribution.Info(SelParamSet - 1).Name
        'txtContinuity.Text = Main.Distribution.Distrib.Continuity
        'txtContinuity.Text = Main.Distribution.Continuity
        txtNParams.Text = Main.Distribution.Info(SelParamSet - 1).NParams
        dgvParams.RowCount = Main.Distribution.Info(SelParamSet - 1).NParams

        'chkUpdateLabel.Checked = Main.Distribution.MultiDistrib(SelParamSet - 1).AutoUpdateSuffix
        chkUpdateLabel.Checked = Main.Distribution.Info(SelParamSet - 1).AutoUpdateLabels

        If Main.Distribution.Info(SelParamSet - 1).NParams > 0 Then
            dgvParams.Rows(0).Cells(0).Value = Main.Distribution.Info(SelParamSet - 1).ParamA.Name
            dgvParams.Rows(0).Cells(1).Value = NameToSymbol(Main.Distribution.Info(SelParamSet - 1).ParamA.Symbol)
            dgvParams.Rows(0).Cells(2).Value = Main.Distribution.Info(SelParamSet - 1).ParamA.Value
            dgvParams.Rows(0).Cells(3).Value = Main.Distribution.Info(SelParamSet - 1).ParamA.Type
            dgvParams.Rows(0).Cells(4).Value = Main.Distribution.Info(SelParamSet - 1).ParamA.NumberType
            dgvParams.Rows(0).Cells(5).Value = Main.Distribution.Info(SelParamSet - 1).ParamA.Minimum
            dgvParams.Rows(0).Cells(6).Value = Main.Distribution.Info(SelParamSet - 1).ParamA.Maximum
            dgvParams.Rows(0).Cells(7).Value = Main.Distribution.Info(SelParamSet - 1).ParamA.Description
            If Main.Distribution.Info(SelParamSet - 1).NParams > 1 Then
                dgvParams.Rows(1).Cells(0).Value = Main.Distribution.Info(SelParamSet - 1).ParamB.Name
                dgvParams.Rows(1).Cells(1).Value = NameToSymbol(Main.Distribution.Info(SelParamSet - 1).ParamB.Symbol)
                dgvParams.Rows(1).Cells(2).Value = Main.Distribution.Info(SelParamSet - 1).ParamB.Value
                dgvParams.Rows(1).Cells(3).Value = Main.Distribution.Info(SelParamSet - 1).ParamB.Type
                dgvParams.Rows(1).Cells(4).Value = Main.Distribution.Info(SelParamSet - 1).ParamB.NumberType
                dgvParams.Rows(1).Cells(5).Value = Main.Distribution.Info(SelParamSet - 1).ParamB.Minimum
                dgvParams.Rows(1).Cells(6).Value = Main.Distribution.Info(SelParamSet - 1).ParamB.Maximum
                dgvParams.Rows(1).Cells(7).Value = Main.Distribution.Info(SelParamSet - 1).ParamB.Description
                If Main.Distribution.Info(SelParamSet - 1).NParams > 2 Then
                    dgvParams.Rows(2).Cells(0).Value = Main.Distribution.Info(SelParamSet - 1).ParamC.Name
                    dgvParams.Rows(2).Cells(1).Value = NameToSymbol(Main.Distribution.Info(SelParamSet - 1).ParamC.Symbol)
                    dgvParams.Rows(2).Cells(2).Value = Main.Distribution.Info(SelParamSet - 1).ParamC.Value
                    dgvParams.Rows(2).Cells(3).Value = Main.Distribution.Info(SelParamSet - 1).ParamC.Type
                    dgvParams.Rows(2).Cells(4).Value = Main.Distribution.Info(SelParamSet - 1).ParamC.NumberType
                    dgvParams.Rows(2).Cells(5).Value = Main.Distribution.Info(SelParamSet - 1).ParamC.Minimum
                    dgvParams.Rows(2).Cells(6).Value = Main.Distribution.Info(SelParamSet - 1).ParamC.Maximum
                    dgvParams.Rows(2).Cells(7).Value = Main.Distribution.Info(SelParamSet - 1).ParamC.Description
                    If Main.Distribution.Info(SelParamSet - 1).NParams > 3 Then
                        dgvParams.Rows(3).Cells(0).Value = Main.Distribution.Info(SelParamSet - 1).ParamD.Name
                        dgvParams.Rows(3).Cells(1).Value = NameToSymbol(Main.Distribution.Info(SelParamSet - 1).ParamD.Symbol)
                        dgvParams.Rows(3).Cells(2).Value = Main.Distribution.Info(SelParamSet - 1).ParamD.Value
                        dgvParams.Rows(3).Cells(3).Value = Main.Distribution.Info(SelParamSet - 1).ParamD.Type
                        dgvParams.Rows(3).Cells(4).Value = Main.Distribution.Info(SelParamSet - 1).ParamD.NumberType
                        dgvParams.Rows(3).Cells(5).Value = Main.Distribution.Info(SelParamSet - 1).ParamD.Minimum
                        dgvParams.Rows(3).Cells(6).Value = Main.Distribution.Info(SelParamSet - 1).ParamD.Maximum
                        dgvParams.Rows(3).Cells(7).Value = Main.Distribution.Info(SelParamSet - 1).ParamD.Description
                        If Main.Distribution.Info(SelParamSet - 1).NParams > 4 Then
                            dgvParams.Rows(4).Cells(0).Value = Main.Distribution.Info(SelParamSet - 1).ParamE.Name
                            dgvParams.Rows(4).Cells(1).Value = NameToSymbol(Main.Distribution.Info(SelParamSet - 1).ParamD.Symbol)
                            dgvParams.Rows(4).Cells(2).Value = Main.Distribution.Info(SelParamSet - 1).ParamE.Value
                            dgvParams.Rows(4).Cells(3).Value = Main.Distribution.Info(SelParamSet - 1).ParamE.Type
                            dgvParams.Rows(4).Cells(4).Value = Main.Distribution.Info(SelParamSet - 1).ParamE.NumberType
                            dgvParams.Rows(4).Cells(5).Value = Main.Distribution.Info(SelParamSet - 1).ParamE.Minimum
                            dgvParams.Rows(4).Cells(6).Value = Main.Distribution.Info(SelParamSet - 1).ParamE.Maximum
                            dgvParams.Rows(4).Cells(7).Value = Main.Distribution.Info(SelParamSet - 1).ParamE.Description
                            If Main.Distribution.Info(SelParamSet - 1).NParams > 5 Then
                                'Too many parameters!
                            End If
                        End If
                    End If
                End If
            End If
        End If

        'dgvParams.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)
        dgvParams.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        dgvParams.AutoResizeColumns()

        cmbDefMkrFill.SelectedIndex = cmbDefMkrFill.FindStringExact(Main.Distribution.Info(SelParamSet - 1).Display.MarkerFill)
        txtDefMkrColor.BackColor = Main.Distribution.Info(SelParamSet - 1).Display.MarkerColor
        txtDefBorderColor.BackColor = Main.Distribution.Info(SelParamSet - 1).Display.BorderColor
        txtDefBorderWidth.Text = Main.Distribution.Info(SelParamSet - 1).Display.BorderWidth
        cmbDefMkrStyle.SelectedIndex = cmbDefMkrStyle.FindStringExact(Main.Distribution.Info(SelParamSet - 1).Display.MarkerStyle)
        txtDefMkrSize.Text = Main.Distribution.Info(SelParamSet - 1).Display.MarkerSize
        txtDefMkrStep.Text = Main.Distribution.Info(SelParamSet - 1).Display.MarkerStep
        txtDefLineColor.BackColor = Main.Distribution.Info(SelParamSet - 1).Display.LineColor
        txtDefLineWidth.Text = Main.Distribution.Info(SelParamSet - 1).Display.LineWidth

        txtDefFrom.Text = Main.Distribution.Info(SelParamSet - 1).RangeMin
        txtDefTo.Text = Main.Distribution.Info(SelParamSet - 1).RangeMax

        'If Main.Distribution.DistribContinuity = "Continuous" Then
        '    chkPdf.Enabled = True
        '    chkPdfLn.Enabled = True
        '    chkPmf.Enabled = False
        '    chkPmfLn.Enabled = False
        '    chkCdf.Enabled = True
        '    chkInvCdf.Enabled = True

        '    If Distribution.PdfInfo.Generate = True Then chkPdf.Checked = True Else chkPdf.Checked = False
        '    If Distribution.PdfLnInfo.Generate = True Then chkPdfLn.Checked = True Else chkPdfLn.Checked = False
        '    If Distribution.CdfInfo.Generate = True Then chkCdf.Checked = True Else chkCdf.Checked = False
        '    If Distribution.InvCdfInfo.Generate = True Then chkInvCdf.Checked = True Else chkInvCdf.Checked = False
        '    If Distribution.InvCdfInfo.Valid = True Then chkInvCdf.Enabled = True Else chkInvCdf.Enabled = False

        '    ''Update Continuous sampling settings:
        '    'txtMinValue.Text = Distribution.ContSampling.Minimum
        '    'chkLockSampMin.Checked = Distribution.ContSampling.MinLock
        '    'txtMaxValue.Text = Distribution.ContSampling.Maximum
        '    'chkLockSampMax.Checked = Distribution.ContSampling.MaxLock
        '    'txtSampleInt.Text = Distribution.ContSampling.Interval
        '    'chkLockSampInt.Checked = Distribution.ContSampling.IntervalLock
        '    'txtNSamples.Text = Distribution.ContSampling.NSamples
        '    'chkLockNSamples.Checked = Distribution.ContSampling.NSamplesLock
        '    'txtXAxisLabel.Text = Distribution.ContSampling.Label
        '    'txtXAxisUnits.Text = Distribution.ContSampling.Units

        '    ''txtMinValue.Enabled = True
        '    ''chkLockSampMin.Enabled = True
        '    ''txtMaxValue.Enabled = True
        '    ''chkLockSampMax.Enabled = True
        '    ''txtSampleInt.Enabled = True
        '    ''chkLockSampInt.Enabled = True
        '    ''txtNSamples.Enabled = True
        '    ''chkLockNSamples.Enabled = True
        '    ''txtXAxisLabel.Enabled = True
        '    ''txtXAxisUnits.Enabled = True
        '    ''txtXAxisDescription.Text = Distribution.ContSampling.Description
        '    ''txtXAxisDescription.Enabled = True
        '    'GroupBox2.Enabled = True

        '    ''Disable Discrete sampling settings:
        '    ''txtDiscMin.Enabled = False
        '    ''txtDiscMax.Enabled = False
        '    ''txtDiscXAxisLabel.Enabled = False
        '    ''txtDiscXAxisUnits.Enabled = False
        '    ''txtDiscXAxisDescr.Enabled = False
        '    'GroupBox9.Enabled = False

        'ElseIf Distribution.DistribContinuity = "Discrete" Then
        '    chkPdf.Enabled = False
        '    chkPdfLn.Enabled = False
        '    chkPmf.Enabled = True
        '    chkPmfLn.Enabled = True
        '    chkCdf.Enabled = True
        '    chkInvCdf.Enabled = False

        '    If Distribution.PmfInfo.Generate = True Then chkPmf.Checked = True Else chkPmf.Checked = False
        '    If Distribution.PmfLnInfo.Generate = True Then chkPmfLn.Checked = True Else chkPmfLn.Checked = False
        '    If Distribution.CdfInfo.Generate = True Then chkCdf.Checked = True Else chkCdf.Checked = False

        '    ''Update Discrete sampling settings:
        '    'txtDiscMin.Text = Distribution.DiscSampling.Minimum
        '    'txtDiscMax.Text = Distribution.DiscSampling.Maximum
        '    'txtDiscXAxisLabel.Text = Distribution.DiscSampling.Label
        '    'txtDiscXAxisUnits.Text = Distribution.DiscSampling.Units
        '    'txtDiscXAxisDescr.Text = Distribution.DiscSampling.Description
        '    ''txtDiscMin.Enabled = True
        '    ''txtDiscMax.Enabled = True
        '    ''txtDiscXAxisLabel.Enabled = True
        '    ''txtDiscXAxisUnits.Enabled = True
        '    ''txtDiscXAxisDescr.Enabled = True
        '    'GroupBox9.Enabled = True

        '    ''Disable Continuous sampling settings:
        '    ''txtMinValue.Enabled = False
        '    ''chkLockSampMin.Enabled = False
        '    ''txtMaxValue.Enabled = False
        '    ''chkLockSampMax.Enabled = False
        '    ''txtSampleInt.Enabled = False
        '    ''chkLockSampInt.Enabled = False
        '    ''txtNSamples.Enabled = False
        '    ''chkLockNSamples.Enabled = False
        '    ''txtXAxisLabel.Enabled = False
        '    ''txtXAxisUnits.Enabled = False
        '    ''txtXAxisDescription.Enabled = False
        '    'GroupBox2.Enabled = False

        'Else
        '    'Not a valid continuity string.
        'End If

        'Update the Fields tab: ------------------------------------------------------------------
        dgvFields.Rows.Clear()
        'If Main.Distribution.Distrib.Continuity = "Continuous" Then
        If Main.Distribution.Info(SelParamSet - 1).Continuity = "Continuous" Then
            'dgvFields.Rows.Add(Main.Distribution.MultiDistrib(SelParamSet - 1).ValueInfo.Name, Main.Distribution.MultiDistrib(SelParamSet - 1).ValueInfo.Valid, Main.Distribution.MultiDistrib(SelParamSet - 1).ValueInfo.Generate.ToString, Main.Distribution.MultiDistrib(SelParamSet - 1).ValueInfo.NumType, Main.Distribution.MultiDistrib(SelParamSet - 1).ValueInfo.Format, Main.Distribution.MultiDistrib(SelParamSet - 1).ValueInfo.Alignment, Main.Distribution.MultiDistrib(SelParamSet - 1).ValueInfo.Units, Main.Distribution.MultiDistrib(SelParamSet - 1).ValueInfo.Label, Main.Distribution.MultiDistrib(SelParamSet - 1).ValueInfo.Description)
            dgvFields.Rows.Add(Main.Distribution.Info(SelParamSet - 1).PdfInfo.Name, Main.Distribution.Info(SelParamSet - 1).PdfInfo.Valid, Main.Distribution.Info(SelParamSet - 1).PdfInfo.Generate.ToString, Main.Distribution.Info(SelParamSet - 1).PdfInfo.NumType, Main.Distribution.Info(SelParamSet - 1).PdfInfo.Format, Main.Distribution.Info(SelParamSet - 1).PdfInfo.Alignment, Main.Distribution.Info(SelParamSet - 1).PdfInfo.ValueLabel, Main.Distribution.Info(SelParamSet - 1).PdfInfo.Units, Main.Distribution.Info(SelParamSet - 1).PdfInfo.LabelPrefix, Main.Distribution.Info(SelParamSet - 1).PdfInfo.SeriesLabel, Main.Distribution.Info(SelParamSet - 1).PdfInfo.Description)
            dgvFields.Rows.Add(Main.Distribution.Info(SelParamSet - 1).PdfLnInfo.Name, Main.Distribution.Info(SelParamSet - 1).PdfLnInfo.Valid, Main.Distribution.Info(SelParamSet - 1).PdfLnInfo.Generate.ToString, Main.Distribution.Info(SelParamSet - 1).PdfLnInfo.NumType, Main.Distribution.Info(SelParamSet - 1).PdfLnInfo.Format, Main.Distribution.Info(SelParamSet - 1).PdfLnInfo.Alignment, Main.Distribution.Info(SelParamSet - 1).PdfLnInfo.ValueLabel, Main.Distribution.Info(SelParamSet - 1).PdfLnInfo.Units, Main.Distribution.Info(SelParamSet - 1).PdfLnInfo.LabelPrefix, Main.Distribution.Info(SelParamSet - 1).PdfLnInfo.SeriesLabel, Main.Distribution.Info(SelParamSet - 1).PdfLnInfo.Description)
            dgvFields.Rows.Add(Main.Distribution.Info(SelParamSet - 1).CdfInfo.Name, Main.Distribution.Info(SelParamSet - 1).CdfInfo.Valid, Main.Distribution.Info(SelParamSet - 1).CdfInfo.Generate.ToString, Main.Distribution.Info(SelParamSet - 1).CdfInfo.NumType, Main.Distribution.Info(SelParamSet - 1).CdfInfo.Format, Main.Distribution.Info(SelParamSet - 1).CdfInfo.Alignment, Main.Distribution.Info(SelParamSet - 1).CdfInfo.ValueLabel, Main.Distribution.Info(SelParamSet - 1).CdfInfo.Units, Main.Distribution.Info(SelParamSet - 1).CdfInfo.LabelPrefix, Main.Distribution.Info(SelParamSet - 1).CdfInfo.SeriesLabel, Main.Distribution.Info(SelParamSet - 1).CdfInfo.Description)
            'dgvFields.Rows.Add(Main.Distribution.MultiDistrib(SelParamSet - 1).ProbabilityInfo.Name, Main.Distribution.MultiDistrib(SelParamSet - 1).ProbabilityInfo.Valid, Main.Distribution.MultiDistrib(SelParamSet - 1).ProbabilityInfo.Generate.ToString, Main.Distribution.MultiDistrib(SelParamSet - 1).ProbabilityInfo.NumType, Main.Distribution.MultiDistrib(SelParamSet - 1).ProbabilityInfo.Format, Main.Distribution.MultiDistrib(SelParamSet - 1).ProbabilityInfo.Alignment, Main.Distribution.MultiDistrib(SelParamSet - 1).ProbabilityInfo.Units, Main.Distribution.MultiDistrib(SelParamSet - 1).ProbabilityInfo.Label, Main.Distribution.MultiDistrib(SelParamSet - 1).ProbabilityInfo.Description)
            dgvFields.Rows.Add(Main.Distribution.Info(SelParamSet - 1).InvCdfInfo.Name, Main.Distribution.Info(SelParamSet - 1).InvCdfInfo.Valid, Main.Distribution.Info(SelParamSet - 1).InvCdfInfo.Generate.ToString, Main.Distribution.Info(SelParamSet - 1).InvCdfInfo.NumType, Main.Distribution.Info(SelParamSet - 1).InvCdfInfo.Format, Main.Distribution.Info(SelParamSet - 1).InvCdfInfo.Alignment, Main.Distribution.Info(SelParamSet - 1).InvCdfInfo.ValueLabel, Main.Distribution.Info(SelParamSet - 1).InvCdfInfo.Units, Main.Distribution.Info(SelParamSet - 1).InvCdfInfo.LabelPrefix, Main.Distribution.Info(SelParamSet - 1).InvCdfInfo.SeriesLabel, Main.Distribution.Info(SelParamSet - 1).InvCdfInfo.Description)

            'Add PDF display settings:
            'dgvFields.Rows(1).Cells(11).Value = Main.Distribution.MultiDistrib(SelParamSet - 1).PdfInfo.Display.MarkerFill
            dgvFields.Rows(0).Cells(11).Value = Main.Distribution.Info(SelParamSet - 1).PdfInfo.Display.MarkerFill
            dgvFields.Rows(0).Cells(12).Style.BackColor = Main.Distribution.Info(SelParamSet - 1).PdfInfo.Display.MarkerColor
            dgvFields.Rows(0).Cells(13).Style.BackColor = Main.Distribution.Info(SelParamSet - 1).PdfInfo.Display.BorderColor
            dgvFields.Rows(0).Cells(14).Value = Main.Distribution.Info(SelParamSet - 1).PdfInfo.Display.BorderWidth
            dgvFields.Rows(0).Cells(15).Value = Main.Distribution.Info(SelParamSet - 1).PdfInfo.Display.MarkerStyle
            dgvFields.Rows(0).Cells(16).Value = Main.Distribution.Info(SelParamSet - 1).PdfInfo.Display.MarkerSize
            dgvFields.Rows(0).Cells(17).Value = Main.Distribution.Info(SelParamSet - 1).PdfInfo.Display.MarkerStep
            dgvFields.Rows(0).Cells(18).Style.BackColor = Main.Distribution.Info(SelParamSet - 1).PdfInfo.Display.LineColor
            dgvFields.Rows(0).Cells(19).Value = Main.Distribution.Info(SelParamSet - 1).PdfInfo.Display.LineWidth

            'Add PDFLn display settings:
            dgvFields.Rows(1).Cells(11).Value = Main.Distribution.Info(SelParamSet - 1).PdfLnInfo.Display.MarkerFill
            dgvFields.Rows(1).Cells(12).Style.BackColor = Main.Distribution.Info(SelParamSet - 1).PdfLnInfo.Display.MarkerColor
            dgvFields.Rows(1).Cells(13).Style.BackColor = Main.Distribution.Info(SelParamSet - 1).PdfLnInfo.Display.BorderColor
            dgvFields.Rows(1).Cells(14).Value = Main.Distribution.Info(SelParamSet - 1).PdfLnInfo.Display.BorderWidth
            dgvFields.Rows(1).Cells(15).Value = Main.Distribution.Info(SelParamSet - 1).PdfLnInfo.Display.MarkerStyle
            dgvFields.Rows(1).Cells(16).Value = Main.Distribution.Info(SelParamSet - 1).PdfLnInfo.Display.MarkerSize
            dgvFields.Rows(1).Cells(17).Value = Main.Distribution.Info(SelParamSet - 1).PdfLnInfo.Display.MarkerStep
            dgvFields.Rows(1).Cells(18).Style.BackColor = Main.Distribution.Info(SelParamSet - 1).PdfLnInfo.Display.LineColor
            dgvFields.Rows(1).Cells(19).Value = Main.Distribution.Info(SelParamSet - 1).PdfLnInfo.Display.LineWidth

            'Add CDF display settings:
            dgvFields.Rows(2).Cells(11).Value = Main.Distribution.Info(SelParamSet - 1).CdfInfo.Display.MarkerFill
            dgvFields.Rows(2).Cells(12).Style.BackColor = Main.Distribution.Info(SelParamSet - 1).CdfInfo.Display.MarkerColor
            dgvFields.Rows(2).Cells(13).Style.BackColor = Main.Distribution.Info(SelParamSet - 1).CdfInfo.Display.BorderColor
            dgvFields.Rows(2).Cells(14).Value = Main.Distribution.Info(SelParamSet - 1).CdfInfo.Display.BorderWidth
            dgvFields.Rows(2).Cells(15).Value = Main.Distribution.Info(SelParamSet - 1).CdfInfo.Display.MarkerStyle
            dgvFields.Rows(2).Cells(16).Value = Main.Distribution.Info(SelParamSet - 1).CdfInfo.Display.MarkerSize
            dgvFields.Rows(2).Cells(17).Value = Main.Distribution.Info(SelParamSet - 1).CdfInfo.Display.MarkerStep
            dgvFields.Rows(2).Cells(18).Style.BackColor = Main.Distribution.Info(SelParamSet - 1).CdfInfo.Display.LineColor
            dgvFields.Rows(2).Cells(19).Value = Main.Distribution.Info(SelParamSet - 1).CdfInfo.Display.LineWidth

            'Add InvCDF display settings:
            dgvFields.Rows(3).Cells(11).Value = Main.Distribution.Info(SelParamSet - 1).InvCdfInfo.Display.MarkerFill
            dgvFields.Rows(3).Cells(12).Style.BackColor = Main.Distribution.Info(SelParamSet - 1).InvCdfInfo.Display.MarkerColor
            dgvFields.Rows(3).Cells(13).Style.BackColor = Main.Distribution.Info(SelParamSet - 1).InvCdfInfo.Display.BorderColor
            dgvFields.Rows(3).Cells(14).Value = Main.Distribution.Info(SelParamSet - 1).InvCdfInfo.Display.BorderWidth
            dgvFields.Rows(3).Cells(15).Value = Main.Distribution.Info(SelParamSet - 1).InvCdfInfo.Display.MarkerStyle
            dgvFields.Rows(3).Cells(16).Value = Main.Distribution.Info(SelParamSet - 1).InvCdfInfo.Display.MarkerSize
            dgvFields.Rows(3).Cells(17).Value = Main.Distribution.Info(SelParamSet - 1).InvCdfInfo.Display.MarkerStep
            dgvFields.Rows(3).Cells(18).Style.BackColor = Main.Distribution.Info(SelParamSet - 1).InvCdfInfo.Display.LineColor
            dgvFields.Rows(3).Cells(19).Value = Main.Distribution.Info(SelParamSet - 1).InvCdfInfo.Display.LineWidth



            'dgvFields.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)
            dgvFields.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            dgvFields.AutoResizeColumns()

            'ElseIf Main.Distribution.Distrib.Continuity = "Discrete" Then
        ElseIf Main.Distribution.Info(SelParamSet - 1).Continuity = "Discrete" Then
            'dgvFields.Rows.Add(Main.Distribution.MultiDistrib(SelParamSet - 1).ValueInfo.Name, Main.Distribution.MultiDistrib(SelParamSet - 1).ValueInfo.Valid, Main.Distribution.MultiDistrib(SelParamSet - 1).ValueInfo.Generate.ToString, Main.Distribution.MultiDistrib(SelParamSet - 1).ValueInfo.NumType, Main.Distribution.MultiDistrib(SelParamSet - 1).ValueInfo.Format, Main.Distribution.MultiDistrib(SelParamSet - 1).ValueInfo.Alignment, Main.Distribution.MultiDistrib(SelParamSet - 1).ValueInfo.Units, Main.Distribution.MultiDistrib(SelParamSet - 1).ValueInfo.Label, Main.Distribution.MultiDistrib(SelParamSet - 1).ValueInfo.Description)
            dgvFields.Rows.Add(Main.Distribution.Info(SelParamSet - 1).PmfInfo.Name, Main.Distribution.Info(SelParamSet - 1).PmfInfo.Valid, Main.Distribution.Info(SelParamSet - 1).PmfInfo.Generate.ToString, Main.Distribution.Info(SelParamSet - 1).PmfInfo.NumType, Main.Distribution.Info(SelParamSet - 1).PmfInfo.Format, Main.Distribution.Info(SelParamSet - 1).PmfInfo.Alignment, Main.Distribution.Info(SelParamSet - 1).PmfInfo.ValueLabel, Main.Distribution.Info(SelParamSet - 1).PmfInfo.Units, Main.Distribution.Info(SelParamSet - 1).PmfInfo.LabelPrefix, Main.Distribution.Info(SelParamSet - 1).PmfInfo.SeriesLabel, Main.Distribution.Info(SelParamSet - 1).PmfInfo.Description)
            dgvFields.Rows.Add(Main.Distribution.Info(SelParamSet - 1).PmfLnInfo.Name, Main.Distribution.Info(SelParamSet - 1).PmfLnInfo.Valid, Main.Distribution.Info(SelParamSet - 1).PmfLnInfo.Generate.ToString, Main.Distribution.Info(SelParamSet - 1).PmfLnInfo.NumType, Main.Distribution.Info(SelParamSet - 1).PmfLnInfo.Format, Main.Distribution.Info(SelParamSet - 1).PmfLnInfo.Alignment, Main.Distribution.Info(SelParamSet - 1).PmfLnInfo.ValueLabel, Main.Distribution.Info(SelParamSet - 1).PmfLnInfo.Units, Main.Distribution.Info(SelParamSet - 1).PmfLnInfo.LabelPrefix, Main.Distribution.Info(SelParamSet - 1).PmfLnInfo.SeriesLabel, Main.Distribution.Info(SelParamSet - 1).PmfLnInfo.Description)
            dgvFields.Rows.Add(Main.Distribution.Info(SelParamSet - 1).CdfInfo.Name, Main.Distribution.Info(SelParamSet - 1).CdfInfo.Valid, Main.Distribution.Info(SelParamSet - 1).CdfInfo.Generate.ToString, Main.Distribution.Info(SelParamSet - 1).CdfInfo.NumType, Main.Distribution.Info(SelParamSet - 1).CdfInfo.Format, Main.Distribution.Info(SelParamSet - 1).CdfInfo.Alignment, Main.Distribution.Info(SelParamSet - 1).CdfInfo.ValueLabel, Main.Distribution.Info(SelParamSet - 1).CdfInfo.Units, Main.Distribution.Info(SelParamSet - 1).CdfInfo.LabelPrefix, Main.Distribution.Info(SelParamSet - 1).CdfInfo.SeriesLabel, Main.Distribution.Info(SelParamSet - 1).CdfInfo.Description)

            'Add PMF display settings:
            'dgvFields.Rows(1).Cells(11).Value = Main.Distribution.MultiDistrib(SelParamSet - 1).PmfInfo.Display.MarkerFill
            dgvFields.Rows(0).Cells(11).Value = Main.Distribution.Info(SelParamSet - 1).PmfInfo.Display.MarkerFill
            dgvFields.Rows(0).Cells(12).Style.BackColor = Main.Distribution.Info(SelParamSet - 1).PmfInfo.Display.MarkerColor
            dgvFields.Rows(0).Cells(13).Style.BackColor = Main.Distribution.Info(SelParamSet - 1).PmfInfo.Display.BorderColor
            dgvFields.Rows(0).Cells(14).Value = Main.Distribution.Info(SelParamSet - 1).PmfInfo.Display.BorderWidth
            dgvFields.Rows(0).Cells(15).Value = Main.Distribution.Info(SelParamSet - 1).PmfInfo.Display.MarkerStyle
            dgvFields.Rows(0).Cells(16).Value = Main.Distribution.Info(SelParamSet - 1).PmfInfo.Display.MarkerSize
            dgvFields.Rows(0).Cells(17).Value = Main.Distribution.Info(SelParamSet - 1).PmfInfo.Display.MarkerStep
            dgvFields.Rows(0).Cells(18).Style.BackColor = Main.Distribution.Info(SelParamSet - 1).PmfInfo.Display.LineColor
            dgvFields.Rows(0).Cells(19).Value = Main.Distribution.Info(SelParamSet - 1).PmfInfo.Display.LineWidth

            'Add PMFLn display settings:
            dgvFields.Rows(1).Cells(11).Value = Main.Distribution.Info(SelParamSet - 1).PmfLnInfo.Display.MarkerFill
            dgvFields.Rows(1).Cells(12).Style.BackColor = Main.Distribution.Info(SelParamSet - 1).PmfLnInfo.Display.MarkerColor
            dgvFields.Rows(1).Cells(13).Style.BackColor = Main.Distribution.Info(SelParamSet - 1).PmfLnInfo.Display.BorderColor
            dgvFields.Rows(1).Cells(14).Value = Main.Distribution.Info(SelParamSet - 1).PmfLnInfo.Display.BorderWidth
            dgvFields.Rows(1).Cells(15).Value = Main.Distribution.Info(SelParamSet - 1).PmfLnInfo.Display.MarkerStyle
            dgvFields.Rows(1).Cells(16).Value = Main.Distribution.Info(SelParamSet - 1).PmfLnInfo.Display.MarkerSize
            dgvFields.Rows(1).Cells(17).Value = Main.Distribution.Info(SelParamSet - 1).PmfLnInfo.Display.MarkerStep
            dgvFields.Rows(1).Cells(18).Style.BackColor = Main.Distribution.Info(SelParamSet - 1).PmfLnInfo.Display.LineColor
            dgvFields.Rows(1).Cells(19).Value = Main.Distribution.Info(SelParamSet - 1).PmfLnInfo.Display.LineWidth

            'Add CDF display settings:
            dgvFields.Rows(2).Cells(11).Value = Main.Distribution.Info(SelParamSet - 1).CdfInfo.Display.MarkerFill
            dgvFields.Rows(2).Cells(12).Style.BackColor = Main.Distribution.Info(SelParamSet - 1).CdfInfo.Display.MarkerColor
            dgvFields.Rows(2).Cells(13).Style.BackColor = Main.Distribution.Info(SelParamSet - 1).CdfInfo.Display.BorderColor
            dgvFields.Rows(2).Cells(14).Value = Main.Distribution.Info(SelParamSet - 1).CdfInfo.Display.BorderWidth
            dgvFields.Rows(2).Cells(15).Value = Main.Distribution.Info(SelParamSet - 1).CdfInfo.Display.MarkerStyle
            dgvFields.Rows(2).Cells(16).Value = Main.Distribution.Info(SelParamSet - 1).CdfInfo.Display.MarkerSize
            dgvFields.Rows(2).Cells(17).Value = Main.Distribution.Info(SelParamSet - 1).CdfInfo.Display.MarkerStep
            dgvFields.Rows(2).Cells(18).Style.BackColor = Main.Distribution.Info(SelParamSet - 1).CdfInfo.Display.LineColor
            dgvFields.Rows(2).Cells(19).Value = Main.Distribution.Info(SelParamSet - 1).CdfInfo.Display.LineWidth

            'dgvFields.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)
            dgvFields.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            dgvFields.AutoResizeColumns()

        Else
            'Invalid continuity string.
        End If

        txtSuffix.Text = Main.Distribution.Info(SelParamSet - 1).Suffix

    End Sub

    Private Sub ClearForm()
        'Clearm the form data

        txtDistName.Text = ""
        'txtContinuity.Text = Main.Distribution.Continuity
        txtNParams.Text = ""
        dgvParams.RowCount = 0
        txtDefFrom.Text = ""
        txtDefTo.Text = ""

        dgvFields.RowCount = 0


    End Sub

    Private Function NameToSymbol(SymbolName As String) As String
        'Convert a parameter symbol name to the symbol character(s).
        Select Case SymbolName.ToLower

            Case "alpha"
                Return ChrW(945)

            Case "beta"
                Return ChrW(946)

            Case "gamma"
                Return ChrW(947)

            Case "delta"
                Return ChrW(948)

            Case "epsilon"
                Return ChrW(949)

            Case "zeta"
                Return ChrW(950)

            Case "eta"
                Return ChrW(951)

            Case "theta"
                Return ChrW(952)

            Case "iota"
                Return ChrW(953)

            Case "kappa"
                Return ChrW(954)

            Case "lambda"
                Return ChrW(955)

            Case "mu"
                Return ChrW(956)

            Case "nu"
                Return ChrW(957)

            Case "xi"
                Return ChrW(958)

            Case "omicron"
                Return ChrW(959)

            Case "pi"
                Return ChrW(960)

            Case "rho"
                Return ChrW(961)

            Case "sigma"
                Return ChrW(963)

            Case "tau"
                Return ChrW(964)

            Case "upsilon"
                Return ChrW(965)

            Case "phi"
                Return ChrW(966)

            Case "chi"
                Return ChrW(967)

            Case "psi"
                Return ChrW(968)

            Case "omega"
                Return ChrW(969)

            Case Else
                Return SymbolName
        End Select
    End Function

    Private Sub btnPrev_Click(sender As Object, e As EventArgs) Handles btnPrev.Click
        If SelParamSet > 1 Then
            SelParamSet -= 1
        Else
            'The first parameter set is already selected.
        End If
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If SelParamSet < NParamSets Then
            SelParamSet += 1
        Else
            'The last parameter set is already selected.
        End If
    End Sub

    Private Sub dgvParams_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvParams.CellContentClick

    End Sub

    Private Sub dgvParams_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvParams.CellEndEdit
        'A distribution parameter has been changed.

        Dim RowNo As Integer = e.RowIndex
        Dim ColNo As Integer = e.ColumnIndex

        'If ColNo = 1 Then 'A distribution parameter has been changed.
        If ColNo = 2 Then 'A distribution parameter has been changed.
            Select Case RowNo
                Case 0
                    'Main.Distribution.Distrib.ParamA.Value = dgvParams.Rows(RowNo).Cells(2).Value
                    Main.Distribution.Info(SelParamSet - 1).ParamA.Value = dgvParams.Rows(RowNo).Cells(2).Value
                    UpdateSuffix()
                Case 1
                    'Main.Distribution.Distrib.ParamB.Value = dgvParams.Rows(RowNo).Cells(2).Value
                    Main.Distribution.Info(SelParamSet - 1).ParamB.Value = dgvParams.Rows(RowNo).Cells(2).Value
                    UpdateSuffix()
                Case 2
                    'Main.Distribution.Distrib.ParamC.Value = dgvParams.Rows(RowNo).Cells(2).Value
                    Main.Distribution.Info(SelParamSet - 1).ParamC.Value = dgvParams.Rows(RowNo).Cells(2).Value
                    UpdateSuffix()
                Case 3
                    'Main.Distribution.Distrib.ParamD.Value = dgvParams.Rows(RowNo).Cells(2).Value
                    Main.Distribution.Info(SelParamSet - 1).ParamD.Value = dgvParams.Rows(RowNo).Cells(2).Value
                    UpdateSuffix()
                Case 4
                    'Main.Distribution.Distrib.ParamE.Value = dgvParams.Rows(RowNo).Cells(2).Value
                    Main.Distribution.Info(SelParamSet - 1).ParamE.Value = dgvParams.Rows(RowNo).Cells(2).Value
                    UpdateSuffix()
                Case Else
                    Main.Message.AddWarning("Unknown parameter number: " & RowNo & vbCrLf)
            End Select
        ElseIf ColNo = 7 Then 'The parameter description has been changed.
            Select Case RowNo
                Case 0
                    Main.Distribution.Info(SelParamSet - 1).ParamA.Description = dgvParams.Rows(RowNo).Cells(7).Value
                    UpdateSuffix()
                Case 1
                    Main.Distribution.Info(SelParamSet - 1).ParamB.Description = dgvParams.Rows(RowNo).Cells(7).Value
                    UpdateSuffix()
                Case 2
                    Main.Distribution.Info(SelParamSet - 1).ParamC.Description = dgvParams.Rows(RowNo).Cells(7).Value
                    UpdateSuffix()
                Case 3
                    Main.Distribution.Info(SelParamSet - 1).ParamD.Description = dgvParams.Rows(RowNo).Cells(7).Value
                    UpdateSuffix()
                Case 4
                    Main.Distribution.Info(SelParamSet - 1).ParamE.Description = dgvParams.Rows(RowNo).Cells(7).Value
                    UpdateSuffix()
                Case Else
                    Main.Message.AddWarning("Unknown parameter number: " & RowNo & vbCrLf)
            End Select
        End If

    End Sub

    Private Sub UpdateSuffix()
        'Update the series name using the parameter values.

        ''txtSuffix.Text = txtParamASymbol.Text.Trim & "=" & txtParamAValue.Text.Trim
        'txtSuffix.Text = dgvParams.Rows(0).Cells(1).Value & "=" & dgvParams.Rows(0).Cells(2).Value
        ''If Distribution.Distrib.NParams > 1 Then
        'If Main.Distribution.MultiDistrib(SelParamSet - 1).NParams > 1 Then
        '    txtSuffix.Text = txtSuffix.Text & ", " & dgvParams.Rows(1).Cells(1).Value & "=" & dgvParams.Rows(1).Cells(2).Value
        '    If Main.Distribution.MultiDistrib(SelParamSet - 1).NParams > 2 Then
        '        txtSuffix.Text = txtSuffix.Text & ", " & dgvParams.Rows(2).Cells(1).Value & "=" & dgvParams.Rows(2).Cells(2).Value
        '        If Main.Distribution.MultiDistrib(SelParamSet - 1).NParams > 3 Then
        '            txtSuffix.Text = txtSuffix.Text & ", " & dgvParams.Rows(3).Cells(1).Value & "=" & dgvParams.Rows(3).Cells(2).Value
        '            If Main.Distribution.MultiDistrib(SelParamSet - 1).NParams > 4 Then
        '                txtSuffix.Text = txtSuffix.Text & ", " & dgvParams.Rows(4).Cells(1).Value & "=" & dgvParams.Rows(4).Cells(2).Value
        '            End If
        '        End If
        '    End If
        'End If

        Main.Distribution.Info(SelParamSet - 1).UpdateSuffix()
        txtSuffix.Text = Main.Distribution.Info(SelParamSet - 1).Suffix

        If chkUpdateLabel.Checked Then


            ''Distribution.Distrib.PdfInfo.SeriesLabel = Distribution.Distrib.PdfInfo.LabelPrefix & " " & txtSuffix.Text
            'Main.Distribution.MultiDistrib(SelParamSet - 1).PdfInfo.SeriesLabel = Main.Distribution.MultiDistrib(SelParamSet - 1).PdfInfo.LabelPrefix & " " & txtSuffix.Text
            ''Distribution.Distrib.PdfLnInfo.SeriesLabel = Distribution.Distrib.PdfLnInfo.LabelPrefix & " " & txtSuffix.Text
            'Main.Distribution.MultiDistrib(SelParamSet - 1).PdfLnInfo.SeriesLabel = Main.Distribution.MultiDistrib(SelParamSet - 1).PdfLnInfo.LabelPrefix & " " & txtSuffix.Text
            ''Distribution.Distrib.CdfInfo.SeriesLabel = Distribution.Distrib.CdfInfo.LabelPrefix & " " & txtSuffix.Text
            'Main.Distribution.MultiDistrib(SelParamSet - 1).CdfInfo.SeriesLabel = Main.Distribution.MultiDistrib(SelParamSet - 1).CdfInfo.LabelPrefix & " " & txtSuffix.Text
            ''Distribution.Distrib.InvCdfInfo.SeriesLabel = Distribution.Distrib.InvCdfInfo.LabelPrefix & " " & txtSuffix.Text
            'Main.Distribution.MultiDistrib(SelParamSet - 1).InvCdfInfo.SeriesLabel = Main.Distribution.MultiDistrib(SelParamSet - 1).InvCdfInfo.LabelPrefix & " " & txtSuffix.Text

            Main.Distribution.Info(SelParamSet - 1).UpdateSeriesLabels()


            '    'Update the Fields tab: ------------------------------------------------------------------
            '    dgvFields.Rows.Clear()
            '    If Distribution.Distrib.Continuity = "Continuous" Then
            '        dgvFields.Rows.Add(Distribution.Distrib.ValueInfo.Name, Distribution.Distrib.ValueInfo.Valid, Distribution.Distrib.ValueInfo.Generate.ToString, Distribution.Distrib.ValueInfo.NumType, Distribution.Distrib.ValueInfo.Format, Distribution.Distrib.ValueInfo.Alignment, Distribution.Distrib.ValueInfo.ValueLabel, Distribution.Distrib.ValueInfo.Units, Distribution.Distrib.ValueInfo.LabelPrefix, Distribution.Distrib.ValueInfo.SeriesLabel, Distribution.Distrib.ValueInfo.Description)
            '        dgvFields.Rows.Add(Distribution.Distrib.PdfInfo.Name, Distribution.Distrib.PdfInfo.Valid, Distribution.Distrib.PdfInfo.Generate.ToString, Distribution.Distrib.PdfInfo.NumType, Distribution.Distrib.PdfInfo.Format, Distribution.Distrib.PdfInfo.Alignment, Distribution.Distrib.PdfInfo.ValueLabel, Distribution.Distrib.PdfInfo.Units, Distribution.Distrib.PdfInfo.LabelPrefix, Distribution.Distrib.PdfInfo.SeriesLabel, Distribution.Distrib.PdfInfo.Description)
            '        dgvFields.Rows.Add(Distribution.Distrib.PdfLnInfo.Name, Distribution.Distrib.PdfLnInfo.Valid, Distribution.Distrib.PdfLnInfo.Generate.ToString, Distribution.Distrib.PdfLnInfo.NumType, Distribution.Distrib.PdfLnInfo.Format, Distribution.Distrib.PdfLnInfo.Alignment, Distribution.Distrib.PdfLnInfo.ValueLabel, Distribution.Distrib.PdfLnInfo.Units, Distribution.Distrib.PdfLnInfo.LabelPrefix, Distribution.Distrib.PdfLnInfo.SeriesLabel, Distribution.Distrib.PdfLnInfo.Description)
            '        dgvFields.Rows.Add(Distribution.Distrib.CdfInfo.Name, Distribution.Distrib.CdfInfo.Valid, Distribution.Distrib.CdfInfo.Generate.ToString, Distribution.Distrib.CdfInfo.NumType, Distribution.Distrib.CdfInfo.Format, Distribution.Distrib.CdfInfo.Alignment, Distribution.Distrib.CdfInfo.ValueLabel, Distribution.Distrib.CdfInfo.Units, Distribution.Distrib.CdfInfo.LabelPrefix, Distribution.Distrib.CdfInfo.SeriesLabel, Distribution.Distrib.CdfInfo.Description)
            '        dgvFields.Rows.Add(Distribution.Distrib.ProbabilityInfo.Name, Distribution.Distrib.ProbabilityInfo.Valid, Distribution.Distrib.ProbabilityInfo.Generate.ToString, Distribution.Distrib.ProbabilityInfo.NumType, Distribution.Distrib.ProbabilityInfo.Format, Distribution.Distrib.ProbabilityInfo.Alignment, Distribution.Distrib.ProbabilityInfo.ValueLabel, Distribution.Distrib.ProbabilityInfo.Units, Distribution.Distrib.ProbabilityInfo.LabelPrefix, Distribution.Distrib.ProbabilityInfo.SeriesLabel, Distribution.Distrib.ProbabilityInfo.Description)
            '        dgvFields.Rows.Add(Distribution.Distrib.InvCdfInfo.Name, Distribution.Distrib.InvCdfInfo.Valid, Distribution.Distrib.InvCdfInfo.Generate.ToString, Distribution.Distrib.InvCdfInfo.NumType, Distribution.Distrib.InvCdfInfo.Format, Distribution.Distrib.InvCdfInfo.Alignment, Distribution.Distrib.InvCdfInfo.ValueLabel, Distribution.Distrib.InvCdfInfo.Units, Distribution.Distrib.InvCdfInfo.LabelPrefix, Distribution.Distrib.InvCdfInfo.SeriesLabel, Distribution.Distrib.InvCdfInfo.Description)
            '        'dgvFields.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)
            '        dgvFields.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            '        dgvFields.AutoResizeColumns()

            '    ElseIf Distribution.Distrib.Continuity = "Discrete" Then
            '        dgvFields.Rows.Add(Distribution.Distrib.ValueInfo.Name, Distribution.Distrib.ValueInfo.Valid, Distribution.Distrib.ValueInfo.Generate.ToString, Distribution.Distrib.ValueInfo.NumType, Distribution.Distrib.ValueInfo.Format, Distribution.Distrib.ValueInfo.Alignment, Distribution.Distrib.ValueInfo.ValueLabel, Distribution.Distrib.ValueInfo.Units, Distribution.Distrib.ValueInfo.LabelPrefix, Distribution.Distrib.ValueInfo.SeriesLabel, Distribution.Distrib.ValueInfo.Description)
            '        dgvFields.Rows.Add(Distribution.Distrib.PmfInfo.Name, Distribution.Distrib.PmfInfo.Valid, Distribution.Distrib.PmfInfo.Generate.ToString, Distribution.Distrib.PmfInfo.NumType, Distribution.Distrib.PmfInfo.Format, Distribution.Distrib.PmfInfo.Alignment, Distribution.Distrib.PmfInfo.ValueLabel, Distribution.Distrib.PmfInfo.Units, Distribution.Distrib.PmfInfo.LabelPrefix, Distribution.Distrib.PmfInfo.SeriesLabel, Distribution.Distrib.PmfInfo.Description)
            '        dgvFields.Rows.Add(Distribution.Distrib.PmfLnInfo.Name, Distribution.Distrib.PmfLnInfo.Valid, Distribution.Distrib.PmfLnInfo.Generate.ToString, Distribution.Distrib.PmfLnInfo.NumType, Distribution.Distrib.PmfLnInfo.Format, Distribution.Distrib.PmfLnInfo.Alignment, Distribution.Distrib.PmfLnInfo.ValueLabel, Distribution.Distrib.PmfLnInfo.Units, Distribution.Distrib.PmfLnInfo.LabelPrefix, Distribution.Distrib.PmfLnInfo.SeriesLabel, Distribution.Distrib.PmfLnInfo.Description)
            '        dgvFields.Rows.Add(Distribution.Distrib.CdfInfo.Name, Distribution.Distrib.CdfInfo.Valid, Distribution.Distrib.CdfInfo.Generate.ToString, Distribution.Distrib.CdfInfo.NumType, Distribution.Distrib.CdfInfo.Format, Distribution.Distrib.CdfInfo.Alignment, Distribution.Distrib.CdfInfo.ValueLabel, Distribution.Distrib.CdfInfo.Units, Distribution.Distrib.CdfInfo.LabelPrefix, Distribution.Distrib.CdfInfo.SeriesLabel, Distribution.Distrib.CdfInfo.Description)
            '        'dgvFields.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)
            '        dgvFields.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            '        dgvFields.AutoResizeColumns()

            '    Else
            '        'Invalid continuity string.
            '    End If
            'End If

            'Update the Fields tab: ------------------------------------------------------------------
            dgvFields.Rows.Clear()
            'If Main.Distribution.Distrib.Continuity = "Continuous" Then
            If Main.Distribution.Info(SelParamSet - 1).Continuity = "Continuous" Then
                'dgvFields.Rows.Add(Main.Distribution.MultiDistrib(SelParamSet - 1).ValueInfo.Name, Main.Distribution.MultiDistrib(SelParamSet - 1).ValueInfo.Valid, Main.Distribution.MultiDistrib(SelParamSet - 1).ValueInfo.Generate.ToString, Main.Distribution.MultiDistrib(SelParamSet - 1).ValueInfo.NumType, Main.Distribution.MultiDistrib(SelParamSet - 1).ValueInfo.Format, Main.Distribution.MultiDistrib(SelParamSet - 1).ValueInfo.Alignment, Main.Distribution.MultiDistrib(SelParamSet - 1).ValueInfo.Units, Main.Distribution.MultiDistrib(SelParamSet - 1).ValueInfo.Label, Main.Distribution.MultiDistrib(SelParamSet - 1).ValueInfo.Description)
                dgvFields.Rows.Add(Main.Distribution.Info(SelParamSet - 1).PdfInfo.Name, Main.Distribution.Info(SelParamSet - 1).PdfInfo.Valid, Main.Distribution.Info(SelParamSet - 1).PdfInfo.Generate.ToString, Main.Distribution.Info(SelParamSet - 1).PdfInfo.NumType, Main.Distribution.Info(SelParamSet - 1).PdfInfo.Format, Main.Distribution.Info(SelParamSet - 1).PdfInfo.Alignment, Main.Distribution.Info(SelParamSet - 1).PdfInfo.ValueLabel, Main.Distribution.Info(SelParamSet - 1).PdfInfo.Units, Main.Distribution.Info(SelParamSet - 1).PdfInfo.LabelPrefix, Main.Distribution.Info(SelParamSet - 1).PdfInfo.SeriesLabel, Main.Distribution.Info(SelParamSet - 1).PdfInfo.Description)
                dgvFields.Rows.Add(Main.Distribution.Info(SelParamSet - 1).PdfLnInfo.Name, Main.Distribution.Info(SelParamSet - 1).PdfLnInfo.Valid, Main.Distribution.Info(SelParamSet - 1).PdfLnInfo.Generate.ToString, Main.Distribution.Info(SelParamSet - 1).PdfLnInfo.NumType, Main.Distribution.Info(SelParamSet - 1).PdfLnInfo.Format, Main.Distribution.Info(SelParamSet - 1).PdfLnInfo.Alignment, Main.Distribution.Info(SelParamSet - 1).PdfLnInfo.ValueLabel, Main.Distribution.Info(SelParamSet - 1).PdfLnInfo.Units, Main.Distribution.Info(SelParamSet - 1).PdfLnInfo.LabelPrefix, Main.Distribution.Info(SelParamSet - 1).PdfLnInfo.SeriesLabel, Main.Distribution.Info(SelParamSet - 1).PdfLnInfo.Description)
                dgvFields.Rows.Add(Main.Distribution.Info(SelParamSet - 1).CdfInfo.Name, Main.Distribution.Info(SelParamSet - 1).CdfInfo.Valid, Main.Distribution.Info(SelParamSet - 1).CdfInfo.Generate.ToString, Main.Distribution.Info(SelParamSet - 1).CdfInfo.NumType, Main.Distribution.Info(SelParamSet - 1).CdfInfo.Format, Main.Distribution.Info(SelParamSet - 1).CdfInfo.Alignment, Main.Distribution.Info(SelParamSet - 1).CdfInfo.ValueLabel, Main.Distribution.Info(SelParamSet - 1).CdfInfo.Units, Main.Distribution.Info(SelParamSet - 1).CdfInfo.LabelPrefix, Main.Distribution.Info(SelParamSet - 1).CdfInfo.SeriesLabel, Main.Distribution.Info(SelParamSet - 1).CdfInfo.Description)
                'dgvFields.Rows.Add(Main.Distribution.MultiDistrib(SelParamSet - 1).ProbabilityInfo.Name, Main.Distribution.MultiDistrib(SelParamSet - 1).ProbabilityInfo.Valid, Main.Distribution.MultiDistrib(SelParamSet - 1).ProbabilityInfo.Generate.ToString, Main.Distribution.MultiDistrib(SelParamSet - 1).ProbabilityInfo.NumType, Main.Distribution.MultiDistrib(SelParamSet - 1).ProbabilityInfo.Format, Main.Distribution.MultiDistrib(SelParamSet - 1).ProbabilityInfo.Alignment, Main.Distribution.MultiDistrib(SelParamSet - 1).ProbabilityInfo.Units, Main.Distribution.MultiDistrib(SelParamSet - 1).ProbabilityInfo.Label, Main.Distribution.MultiDistrib(SelParamSet - 1).ProbabilityInfo.Description)
                dgvFields.Rows.Add(Main.Distribution.Info(SelParamSet - 1).InvCdfInfo.Name, Main.Distribution.Info(SelParamSet - 1).InvCdfInfo.Valid, Main.Distribution.Info(SelParamSet - 1).InvCdfInfo.Generate.ToString, Main.Distribution.Info(SelParamSet - 1).InvCdfInfo.NumType, Main.Distribution.Info(SelParamSet - 1).InvCdfInfo.Format, Main.Distribution.Info(SelParamSet - 1).InvCdfInfo.Alignment, Main.Distribution.Info(SelParamSet - 1).InvCdfInfo.ValueLabel, Main.Distribution.Info(SelParamSet - 1).InvCdfInfo.Units, Main.Distribution.Info(SelParamSet - 1).InvCdfInfo.LabelPrefix, Main.Distribution.Info(SelParamSet - 1).InvCdfInfo.SeriesLabel, Main.Distribution.Info(SelParamSet - 1).InvCdfInfo.Description)

                'Add PDF display settings:
                dgvFields.Rows(0).Cells(11).Value = Main.Distribution.Info(SelParamSet - 1).PdfInfo.Display.MarkerFill
                dgvFields.Rows(0).Cells(12).Style.BackColor = Main.Distribution.Info(SelParamSet - 1).PdfInfo.Display.MarkerColor
                dgvFields.Rows(0).Cells(13).Style.BackColor = Main.Distribution.Info(SelParamSet - 1).PdfInfo.Display.BorderColor
                dgvFields.Rows(0).Cells(14).Value = Main.Distribution.Info(SelParamSet - 1).PdfInfo.Display.BorderWidth
                dgvFields.Rows(0).Cells(15).Value = Main.Distribution.Info(SelParamSet - 1).PdfInfo.Display.MarkerStyle
                dgvFields.Rows(0).Cells(16).Value = Main.Distribution.Info(SelParamSet - 1).PdfInfo.Display.MarkerSize
                dgvFields.Rows(0).Cells(17).Value = Main.Distribution.Info(SelParamSet - 1).PdfInfo.Display.MarkerStep
                dgvFields.Rows(0).Cells(18).Style.BackColor = Main.Distribution.Info(SelParamSet - 1).PdfInfo.Display.LineColor
                dgvFields.Rows(0).Cells(19).Value = Main.Distribution.Info(SelParamSet - 1).PdfInfo.Display.LineWidth

                'Add PDFLn display settings:
                dgvFields.Rows(1).Cells(11).Value = Main.Distribution.Info(SelParamSet - 1).PdfLnInfo.Display.MarkerFill
                dgvFields.Rows(1).Cells(12).Style.BackColor = Main.Distribution.Info(SelParamSet - 1).PdfLnInfo.Display.MarkerColor
                dgvFields.Rows(1).Cells(13).Style.BackColor = Main.Distribution.Info(SelParamSet - 1).PdfLnInfo.Display.BorderColor
                dgvFields.Rows(1).Cells(14).Value = Main.Distribution.Info(SelParamSet - 1).PdfLnInfo.Display.BorderWidth
                dgvFields.Rows(1).Cells(15).Value = Main.Distribution.Info(SelParamSet - 1).PdfLnInfo.Display.MarkerStyle
                dgvFields.Rows(1).Cells(16).Value = Main.Distribution.Info(SelParamSet - 1).PdfLnInfo.Display.MarkerSize
                dgvFields.Rows(1).Cells(17).Value = Main.Distribution.Info(SelParamSet - 1).PdfLnInfo.Display.MarkerStep
                dgvFields.Rows(1).Cells(18).Style.BackColor = Main.Distribution.Info(SelParamSet - 1).PdfLnInfo.Display.LineColor
                dgvFields.Rows(1).Cells(19).Value = Main.Distribution.Info(SelParamSet - 1).PdfLnInfo.Display.LineWidth

                'Add CDF display settings:
                dgvFields.Rows(2).Cells(11).Value = Main.Distribution.Info(SelParamSet - 1).CdfInfo.Display.MarkerFill
                dgvFields.Rows(2).Cells(12).Style.BackColor = Main.Distribution.Info(SelParamSet - 1).CdfInfo.Display.MarkerColor
                dgvFields.Rows(2).Cells(13).Style.BackColor = Main.Distribution.Info(SelParamSet - 1).CdfInfo.Display.BorderColor
                dgvFields.Rows(2).Cells(14).Value = Main.Distribution.Info(SelParamSet - 1).CdfInfo.Display.BorderWidth
                dgvFields.Rows(2).Cells(15).Value = Main.Distribution.Info(SelParamSet - 1).CdfInfo.Display.MarkerStyle
                dgvFields.Rows(2).Cells(16).Value = Main.Distribution.Info(SelParamSet - 1).CdfInfo.Display.MarkerSize
                dgvFields.Rows(2).Cells(17).Value = Main.Distribution.Info(SelParamSet - 1).CdfInfo.Display.MarkerStep
                dgvFields.Rows(2).Cells(18).Style.BackColor = Main.Distribution.Info(SelParamSet - 1).CdfInfo.Display.LineColor
                dgvFields.Rows(2).Cells(19).Value = Main.Distribution.Info(SelParamSet - 1).CdfInfo.Display.LineWidth

                'Add InvCDF display settings:
                dgvFields.Rows(3).Cells(11).Value = Main.Distribution.Info(SelParamSet - 1).InvCdfInfo.Display.MarkerFill
                dgvFields.Rows(3).Cells(12).Style.BackColor = Main.Distribution.Info(SelParamSet - 1).InvCdfInfo.Display.MarkerColor
                dgvFields.Rows(3).Cells(13).Style.BackColor = Main.Distribution.Info(SelParamSet - 1).InvCdfInfo.Display.BorderColor
                dgvFields.Rows(3).Cells(14).Value = Main.Distribution.Info(SelParamSet - 1).InvCdfInfo.Display.BorderWidth
                dgvFields.Rows(3).Cells(15).Value = Main.Distribution.Info(SelParamSet - 1).InvCdfInfo.Display.MarkerStyle
                dgvFields.Rows(3).Cells(16).Value = Main.Distribution.Info(SelParamSet - 1).InvCdfInfo.Display.MarkerSize
                dgvFields.Rows(3).Cells(17).Value = Main.Distribution.Info(SelParamSet - 1).InvCdfInfo.Display.MarkerStep
                dgvFields.Rows(3).Cells(18).Style.BackColor = Main.Distribution.Info(SelParamSet - 1).InvCdfInfo.Display.LineColor
                dgvFields.Rows(3).Cells(19).Value = Main.Distribution.Info(SelParamSet - 1).InvCdfInfo.Display.LineWidth


                'dgvFields.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)
                dgvFields.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
                dgvFields.AutoResizeColumns()

                'ElseIf Main.Distribution.Distrib.Continuity = "Discrete" Then
            ElseIf Main.Distribution.Info(SelParamSet - 1).Continuity = "Discrete" Then
                'dgvFields.Rows.Add(Main.Distribution.MultiDistrib(SelParamSet - 1).ValueInfo.Name, Main.Distribution.MultiDistrib(SelParamSet - 1).ValueInfo.Valid, Main.Distribution.MultiDistrib(SelParamSet - 1).ValueInfo.Generate.ToString, Main.Distribution.MultiDistrib(SelParamSet - 1).ValueInfo.NumType, Main.Distribution.MultiDistrib(SelParamSet - 1).ValueInfo.Format, Main.Distribution.MultiDistrib(SelParamSet - 1).ValueInfo.Alignment, Main.Distribution.MultiDistrib(SelParamSet - 1).ValueInfo.Units, Main.Distribution.MultiDistrib(SelParamSet - 1).ValueInfo.Label, Main.Distribution.MultiDistrib(SelParamSet - 1).ValueInfo.Description)
                dgvFields.Rows.Add(Main.Distribution.Info(SelParamSet - 1).PmfInfo.Name, Main.Distribution.Info(SelParamSet - 1).PmfInfo.Valid, Main.Distribution.Info(SelParamSet - 1).PmfInfo.Generate.ToString, Main.Distribution.Info(SelParamSet - 1).PmfInfo.NumType, Main.Distribution.Info(SelParamSet - 1).PmfInfo.Format, Main.Distribution.Info(SelParamSet - 1).PmfInfo.Alignment, Main.Distribution.Info(SelParamSet - 1).PmfInfo.ValueLabel, Main.Distribution.Info(SelParamSet - 1).PmfInfo.Units, Main.Distribution.Info(SelParamSet - 1).PmfInfo.LabelPrefix, Main.Distribution.Info(SelParamSet - 1).PmfInfo.SeriesLabel, Main.Distribution.Info(SelParamSet - 1).PmfInfo.Description)
                dgvFields.Rows.Add(Main.Distribution.Info(SelParamSet - 1).PmfLnInfo.Name, Main.Distribution.Info(SelParamSet - 1).PmfLnInfo.Valid, Main.Distribution.Info(SelParamSet - 1).PmfLnInfo.Generate.ToString, Main.Distribution.Info(SelParamSet - 1).PmfLnInfo.NumType, Main.Distribution.Info(SelParamSet - 1).PmfLnInfo.Format, Main.Distribution.Info(SelParamSet - 1).PmfLnInfo.Alignment, Main.Distribution.Info(SelParamSet - 1).PmfLnInfo.ValueLabel, Main.Distribution.Info(SelParamSet - 1).PmfLnInfo.Units, Main.Distribution.Info(SelParamSet - 1).PmfLnInfo.LabelPrefix, Main.Distribution.Info(SelParamSet - 1).PmfLnInfo.SeriesLabel, Main.Distribution.Info(SelParamSet - 1).PmfLnInfo.Description)
                dgvFields.Rows.Add(Main.Distribution.Info(SelParamSet - 1).CdfInfo.Name, Main.Distribution.Info(SelParamSet - 1).CdfInfo.Valid, Main.Distribution.Info(SelParamSet - 1).CdfInfo.Generate.ToString, Main.Distribution.Info(SelParamSet - 1).CdfInfo.NumType, Main.Distribution.Info(SelParamSet - 1).CdfInfo.Format, Main.Distribution.Info(SelParamSet - 1).CdfInfo.Alignment, Main.Distribution.Info(SelParamSet - 1).CdfInfo.ValueLabel, Main.Distribution.Info(SelParamSet - 1).CdfInfo.Units, Main.Distribution.Info(SelParamSet - 1).CdfInfo.LabelPrefix, Main.Distribution.Info(SelParamSet - 1).CdfInfo.SeriesLabel, Main.Distribution.Info(SelParamSet - 1).CdfInfo.Description)

                'Add PMF display settings:
                dgvFields.Rows(0).Cells(11).Value = Main.Distribution.Info(SelParamSet - 1).PmfInfo.Display.MarkerFill
                dgvFields.Rows(0).Cells(12).Style.BackColor = Main.Distribution.Info(SelParamSet - 1).PmfInfo.Display.MarkerColor
                dgvFields.Rows(0).Cells(13).Style.BackColor = Main.Distribution.Info(SelParamSet - 1).PmfInfo.Display.BorderColor
                dgvFields.Rows(0).Cells(14).Value = Main.Distribution.Info(SelParamSet - 1).PmfInfo.Display.BorderWidth
                dgvFields.Rows(0).Cells(15).Value = Main.Distribution.Info(SelParamSet - 1).PmfInfo.Display.MarkerStyle
                dgvFields.Rows(0).Cells(16).Value = Main.Distribution.Info(SelParamSet - 1).PmfInfo.Display.MarkerSize
                dgvFields.Rows(0).Cells(17).Value = Main.Distribution.Info(SelParamSet - 1).PmfInfo.Display.MarkerStep
                dgvFields.Rows(0).Cells(18).Style.BackColor = Main.Distribution.Info(SelParamSet - 1).PmfInfo.Display.LineColor
                dgvFields.Rows(0).Cells(19).Value = Main.Distribution.Info(SelParamSet - 1).PmfInfo.Display.LineWidth

                'Add PMFLn display settings:
                dgvFields.Rows(1).Cells(11).Value = Main.Distribution.Info(SelParamSet - 1).PmfLnInfo.Display.MarkerFill
                dgvFields.Rows(1).Cells(12).Style.BackColor = Main.Distribution.Info(SelParamSet - 1).PmfLnInfo.Display.MarkerColor
                dgvFields.Rows(1).Cells(13).Style.BackColor = Main.Distribution.Info(SelParamSet - 1).PmfLnInfo.Display.BorderColor
                dgvFields.Rows(1).Cells(14).Value = Main.Distribution.Info(SelParamSet - 1).PmfLnInfo.Display.BorderWidth
                dgvFields.Rows(1).Cells(15).Value = Main.Distribution.Info(SelParamSet - 1).PmfLnInfo.Display.MarkerStyle
                dgvFields.Rows(1).Cells(16).Value = Main.Distribution.Info(SelParamSet - 1).PmfLnInfo.Display.MarkerSize
                dgvFields.Rows(1).Cells(17).Value = Main.Distribution.Info(SelParamSet - 1).PmfLnInfo.Display.MarkerStep
                dgvFields.Rows(1).Cells(18).Style.BackColor = Main.Distribution.Info(SelParamSet - 1).PmfLnInfo.Display.LineColor
                dgvFields.Rows(1).Cells(19).Value = Main.Distribution.Info(SelParamSet - 1).PmfLnInfo.Display.LineWidth

                'Add CDF display settings:
                dgvFields.Rows(2).Cells(11).Value = Main.Distribution.Info(SelParamSet - 1).CdfInfo.Display.MarkerFill
                dgvFields.Rows(2).Cells(12).Style.BackColor = Main.Distribution.Info(SelParamSet - 1).CdfInfo.Display.MarkerColor
                dgvFields.Rows(2).Cells(13).Style.BackColor = Main.Distribution.Info(SelParamSet - 1).CdfInfo.Display.BorderColor
                dgvFields.Rows(2).Cells(14).Value = Main.Distribution.Info(SelParamSet - 1).CdfInfo.Display.BorderWidth
                dgvFields.Rows(2).Cells(15).Value = Main.Distribution.Info(SelParamSet - 1).CdfInfo.Display.MarkerStyle
                dgvFields.Rows(2).Cells(16).Value = Main.Distribution.Info(SelParamSet - 1).CdfInfo.Display.MarkerSize
                dgvFields.Rows(2).Cells(17).Value = Main.Distribution.Info(SelParamSet - 1).CdfInfo.Display.MarkerStep
                dgvFields.Rows(2).Cells(18).Style.BackColor = Main.Distribution.Info(SelParamSet - 1).CdfInfo.Display.LineColor
                dgvFields.Rows(2).Cells(19).Value = Main.Distribution.Info(SelParamSet - 1).CdfInfo.Display.LineWidth


                'dgvFields.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)
                dgvFields.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
                dgvFields.AutoResizeColumns()

            Else
                'Invalid continuity string.
            End If
        End If

    End Sub

    Private Sub cmbDefMkrFill_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDefMkrFill.SelectedIndexChanged
        'The default Marker Fill setting has changed.
        Main.Distribution.Info(SelParamSet - 1).Display.MarkerFill = cmbDefMkrFill.SelectedItem.ToString
    End Sub

    Private Sub txtDefMkrColor_TextChanged(sender As Object, e As EventArgs) Handles txtDefMkrColor.TextChanged

    End Sub

    Private Sub txtDefMkrColor_Click(sender As Object, e As EventArgs) Handles txtDefMkrColor.Click
        'Select the default Marker color.
        ColorDialog1.Color = txtDefMkrColor.BackColor
        ColorDialog1.ShowDialog()
        txtDefMkrColor.BackColor = ColorDialog1.Color
        Main.Distribution.Info(SelParamSet - 1).Display.MarkerColor = ColorDialog1.Color
    End Sub

    Private Sub txtDefBorderColor_TextChanged(sender As Object, e As EventArgs) Handles txtDefBorderColor.TextChanged

    End Sub

    Private Sub txtDefBorderColor_Click(sender As Object, e As EventArgs) Handles txtDefBorderColor.Click
        'Select the default Marker Border color.
        ColorDialog1.Color = txtDefBorderColor.BackColor
        ColorDialog1.ShowDialog()
        txtDefBorderColor.BackColor = ColorDialog1.Color
        Main.Distribution.Info(SelParamSet - 1).Display.BorderColor = ColorDialog1.Color
    End Sub

    Private Sub txtDefBorderWidth_TextChanged(sender As Object, e As EventArgs) Handles txtDefBorderWidth.TextChanged

    End Sub

    Private Sub txtDefBorderWidth_LostFocus(sender As Object, e As EventArgs) Handles txtDefBorderWidth.LostFocus
        'The Marker Border Width has been changed.
        Try
            Dim Width As Integer = txtDefBorderWidth.Text
            Main.Distribution.Info(SelParamSet - 1).Display.BorderWidth = Width
        Catch ex As Exception
            Main.Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub

    Private Sub cmbDefMkrStyle_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDefMkrStyle.SelectedIndexChanged
        'The default Marker Style setting has changed.
        Main.Distribution.Info(SelParamSet - 1).Display.MarkerStyle = cmbDefMkrStyle.SelectedItem.ToString
    End Sub

    Private Sub txtDefMkrSize_TextChanged(sender As Object, e As EventArgs) Handles txtDefMkrSize.TextChanged

    End Sub

    Private Sub txtDefMkrSize_LostFocus(sender As Object, e As EventArgs) Handles txtDefMkrSize.LostFocus
        'The Marker Size has been changed.
        Try
            Dim Size As Integer = txtDefMkrSize.Text
            Main.Distribution.Info(SelParamSet - 1).Display.MarkerSize = Size
        Catch ex As Exception
            Main.Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub

    Private Sub txtDefMkrStep_TextChanged(sender As Object, e As EventArgs) Handles txtDefMkrStep.TextChanged

    End Sub

    Private Sub txtDefMkrStep_LostFocus(sender As Object, e As EventArgs) Handles txtDefMkrStep.LostFocus
        'The Marker Step has been changed.
        Try
            Dim MarkerStep As Integer = txtDefMkrStep.Text
            Main.Distribution.Info(SelParamSet - 1).Display.MarkerStep = MarkerStep
        Catch ex As Exception
            Main.Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        'Delete the selected distribution

        Main.SaveAllOpenCharts() 'Save the changes made to all open charts.

        Dim DelDistribNo As Integer = SelParamSet
        Dim OrigDistribCount As Integer = Main.Distribution.Info.Count

        Main.Distribution.Info.RemoveAt(SelParamSet - 1) 'Remove the selected distribution

        'Remove the corresponding table columns.
        If Main.Distribution.Data.Tables.Contains("DataTable") Then
                If Main.Distribution.Data.Tables("DataTable").Columns.Contains("PDF_" & DelDistribNo) Then Main.Distribution.Data.Tables("DataTable").Columns.Remove("PDF_" & DelDistribNo)
                If Main.Distribution.Data.Tables("DataTable").Columns.Contains("PDFLn_" & DelDistribNo) Then Main.Distribution.Data.Tables("DataTable").Columns.Remove("PDFLn_" & DelDistribNo)
                If Main.Distribution.Data.Tables("DataTable").Columns.Contains("PMF_" & DelDistribNo) Then Main.Distribution.Data.Tables("DataTable").Columns.Remove("PMF_" & DelDistribNo)
                If Main.Distribution.Data.Tables("DataTable").Columns.Contains("PMFLn_" & DelDistribNo) Then Main.Distribution.Data.Tables("DataTable").Columns.Remove("PMFLn_" & DelDistribNo)
                If Main.Distribution.Data.Tables("DataTable").Columns.Contains("CDF_" & DelDistribNo) Then Main.Distribution.Data.Tables("DataTable").Columns.Remove("CDF_" & DelDistribNo)
                If Main.Distribution.Data.Tables("DataTable").Columns.Contains("InvCDF_" & DelDistribNo) Then Main.Distribution.Data.Tables("DataTable").Columns.Remove("InvCDF_" & DelDistribNo)
            End If

            If DelDistribNo < OrigDistribCount Then 'Some of the Series numbers of the generated data need to be updated.
                Dim I As Integer
                For I = DelDistribNo To OrigDistribCount - 1
                'Rename any existing DataTable columns corresponding to distributions that now have new index numbers.
                Main.Distribution.Info(I - 1).PdfInfo.Name = "PDF_" & I
                If Main.Distribution.Data.Tables("DataTable").Columns.Contains("PDF_" & I + 1) Then Main.Distribution.Data.Tables("DataTable").Columns("PDF_" & I + 1).ColumnName = "PDF_" & I
                Main.Distribution.Info(I - 1).PdfLnInfo.Name = "PDFLn_" & I
                If Main.Distribution.Data.Tables("DataTable").Columns.Contains("PDFLn_" & I + 1) Then Main.Distribution.Data.Tables("DataTable").Columns("PDFLn_" & I + 1).ColumnName = "PDFLn_" & I
                Main.Distribution.Info(I - 1).PmfInfo.Name = "PMF_" & I
                If Main.Distribution.Data.Tables("DataTable").Columns.Contains("PMF_" & I + 1) Then Main.Distribution.Data.Tables("DataTable").Columns("PMF_" & I + 1).ColumnName = "PMF_" & I
                Main.Distribution.Info(I - 1).PmfLnInfo.Name = "PMFLn_" & I
                If Main.Distribution.Data.Tables("DataTable").Columns.Contains("PMFLn_" & I + 1) Then Main.Distribution.Data.Tables("DataTable").Columns("PMFLn_" & I + 1).ColumnName = "PMFLn_" & I
                Main.Distribution.Info(I - 1).CdfInfo.Name = "CDF_" & I
                If Main.Distribution.Data.Tables("DataTable").Columns.Contains("CDF_" & I + 1) Then Main.Distribution.Data.Tables("DataTable").Columns("CDF_" & I + 1).ColumnName = "CDF_" & I
                Main.Distribution.Info(I - 1).InvCdfInfo.Name = "InvCDF_" & I
                If Main.Distribution.Data.Tables("DataTable").Columns.Contains("InvCDF_" & I + 1) Then Main.Distribution.Data.Tables("DataTable").Columns("InvCDF_" & I + 1).ColumnName = "InvCDF_" & I
            Next
            End If

        'Open each saved chart and delete any series using the deleted distribution and update the DistribNo.
        Dim SeriesInfo As IEnumerable(Of XElement)
        Dim Index As Integer
        Dim ProcessNext As Boolean
        Dim NewSeriesIndex As Integer
        For Each ChartInfo In Main.Distribution.ChartList
            SeriesInfo = From item In ChartInfo.Value.<ChartSettings>.<SeriesCollection>.<Series>
            Index = 0
            ProcessNext = True
            If SeriesInfo.Count > 0 Then
                While ProcessNext
                    If SeriesInfo(Index).<DistribNo>.Value = DelDistribNo Then 'This Series uses data from the deleted distribution.
                        SeriesInfo(Index).Remove()
                    ElseIf SeriesInfo(Index).<DistribNo>.Value > DelDistribNo Then 'This Series Number must be decremented to fill the index number gap left by the deleted series.
                        NewSeriesIndex = SeriesInfo(Index).<DistribNo>.Value - 1
                        SeriesInfo(Index).<DistribNo>.Value = NewSeriesIndex
                        Select Case SeriesInfo(Index).<FunctionType>.Value
                            Case "PDF"
                                SeriesInfo(Index).<YFieldName>.Value = "PDF_" & NewSeriesIndex
                            Case "PDFLn"
                                SeriesInfo(Index).<YFieldName>.Value = "PDFLn_" & NewSeriesIndex
                            Case "PMF"
                                SeriesInfo(Index).<YFieldName>.Value = "PMF_" & NewSeriesIndex
                            Case "PMFLn"
                                SeriesInfo(Index).<YFieldName>.Value = "PMFLn_" & NewSeriesIndex
                            Case "CDF"
                                SeriesInfo(Index).<YFieldName>.Value = "CDF_" & NewSeriesIndex
                            Case "InvCDF"
                                SeriesInfo(Index).<YFieldName>.Value = "InvCDF_" & NewSeriesIndex
                            Case Else
                                Main.Message.AddWarning("Unknown Function Type: " & SeriesInfo(Index).<FunctionType>.Value & vbCrLf)
                        End Select
                        Index += 1
                    Else 'This series number remains unchanged.
                        Index += 1
                    End If
                    If SeriesInfo.Count <= Index Then ProcessNext = False
                End While
            End If
        Next

        NParamSets = Main.Distribution.Info.Count

        'Redisplay the form.
        If SelParamSet > Main.Distribution.Info.Count Then
            SelParamSet -= 1
        Else
            UpdateForm()
        End If

        'Reload any open charts:
        Main.ReplotAllOpenCharts()

    End Sub

    Private Sub AddToChart_Click(sender As Object, e As EventArgs) Handles AddToChart.Click
        'Add the new distribution to any open charts.

        Main.SaveAllOpenCharts() 'Save the changes made to all open charts.

        Dim ShowPdf As Boolean
        Dim ShowPdfLn As Boolean
        Dim ShowPmf As Boolean
        Dim ShowPmfLn As Boolean
        Dim ShowCdf As Boolean
        Dim ShowInvCdf As Boolean

        Dim Distrib As DistributionInfo = Main.Distribution.Info(SelParamSet - 1)


        If Distrib.PdfInfo.Valid = True And Distrib.PdfInfo.Generate = True Then ShowPdf = True Else ShowPdf = False
        If Distrib.PdfLnInfo.Valid = True And Distrib.PdfLnInfo.Generate = True Then ShowPdfLn = True Else ShowPdfLn = False
        If Distrib.PmfInfo.Valid = True And Distrib.PmfInfo.Generate = True Then ShowPmf = True Else ShowPmf = False
        If Distrib.PmfLnInfo.Valid = True And Distrib.PmfLnInfo.Generate = True Then ShowPmfLn = True Else ShowPmfLn = False
        If Distrib.CdfInfo.Valid = True And Distrib.CdfInfo.Generate = True Then ShowCdf = True Else ShowCdf = False
        If Distrib.InvCdfInfo.Valid = True And Distrib.InvCdfInfo.Generate = True Then ShowInvCdf = True Else ShowInvCdf = False

        'Process all open charts
        Dim I As Integer
        Dim ChartName As String
        Dim ChartInfo As Xml.Linq.XDocument 'Stores the chart information.
        Dim SeriesInfo As IEnumerable(Of XElement)

        For I = 0 To Main.ChartList.Count - 1
            If IsNothing(Main.ChartList(I)) Then
                'The chart form has been closed.
            Else
                ChartName = Main.ChartList(I).ChartName
                ChartInfo = Main.Distribution.ChartList(ChartName)
                SeriesInfo = From item In ChartInfo.<ChartSettings>.<SeriesCollection>.<Series>
                'If ShowPdf Then SeriesInfo.Ancestors("SeriesCollection").Nodes.Last.AddAfterSelf(Series(ShowPdf, SelParamSet, "PDF", Distrib.PdfInfo.SeriesLabel, "PdfArea", "Value", Distrib.PdfInfo.Name, Color.Black, Distrib.Display.MarkerColor, Distrib.Display.LineColor, Main.Distribution.Distrib.PdfInfo.Display.MarkerStyle))
                If ShowPdf Then SeriesInfo.Ancestors("SeriesCollection").Nodes.Last.AddAfterSelf(Series(ShowPdf, SelParamSet, "PDF", Distrib.PdfInfo.SeriesLabel, "PdfArea", "Value", Distrib.PdfInfo.Name, Color.Black, Distrib.Display.MarkerColor, Distrib.Display.LineColor, Main.Distribution.Info(0).PdfInfo.Display.MarkerStyle))
                'If ShowPdfLn Then SeriesInfo.Ancestors("SeriesCollection").Nodes.Last.AddAfterSelf(Series(ShowPdfLn, SelParamSet, "PDFLn", Distrib.PdfLnInfo.SeriesLabel, "PdfLnArea", "Value", Distrib.PdfLnInfo.Name, Color.Black, Distrib.Display.MarkerColor, Distrib.Display.LineColor, Main.Distribution.Distrib.PdfLnInfo.Display.MarkerStyle))
                If ShowPdfLn Then SeriesInfo.Ancestors("SeriesCollection").Nodes.Last.AddAfterSelf(Series(ShowPdfLn, SelParamSet, "PDFLn", Distrib.PdfLnInfo.SeriesLabel, "PdfLnArea", "Value", Distrib.PdfLnInfo.Name, Color.Black, Distrib.Display.MarkerColor, Distrib.Display.LineColor, Main.Distribution.Info(0).PdfLnInfo.Display.MarkerStyle))
                'If ShowPmf Then SeriesInfo.Ancestors("SeriesCollection").Nodes.Last.AddAfterSelf(Series(ShowPmf, SelParamSet, "PMF", Distrib.PdfInfo.SeriesLabel, "PmfArea", "Value", Distrib.PmfInfo.Name, Color.Black, Distrib.Display.MarkerColor, Distrib.Display.LineColor, Main.Distribution.Distrib.PmfInfo.Display.MarkerStyle))
                If ShowPmf Then SeriesInfo.Ancestors("SeriesCollection").Nodes.Last.AddAfterSelf(Series(ShowPmf, SelParamSet, "PMF", Distrib.PdfInfo.SeriesLabel, "PmfArea", "Value", Distrib.PmfInfo.Name, Color.Black, Distrib.Display.MarkerColor, Distrib.Display.LineColor, Main.Distribution.Info(0).PmfInfo.Display.MarkerStyle))
                'If ShowPmfLn Then SeriesInfo.Ancestors("SeriesCollection").Nodes.Last.AddAfterSelf(Series(ShowPmfLn, SelParamSet, "PMFLn", Distrib.PdfInfo.SeriesLabel, "PmfLnArea", "Value", Distrib.PmfLnInfo.Name, Color.Black, Distrib.Display.MarkerColor, Distrib.Display.LineColor, Main.Distribution.Distrib.PmfLnInfo.Display.MarkerStyle))
                If ShowPmfLn Then SeriesInfo.Ancestors("SeriesCollection").Nodes.Last.AddAfterSelf(Series(ShowPmfLn, SelParamSet, "PMFLn", Distrib.PdfInfo.SeriesLabel, "PmfLnArea", "Value", Distrib.PmfLnInfo.Name, Color.Black, Distrib.Display.MarkerColor, Distrib.Display.LineColor, Main.Distribution.Info(0).PmfLnInfo.Display.MarkerStyle))
                'If ShowCdf Then SeriesInfo.Ancestors("SeriesCollection").Nodes.Last.AddAfterSelf(Series(ShowCdf, SelParamSet, "CDF", Distrib.PdfInfo.SeriesLabel, "CdfArea", "Value", Distrib.CdfInfo.Name, Color.Black, Distrib.Display.MarkerColor, Distrib.Display.LineColor, Main.Distribution.Distrib.CdfInfo.Display.MarkerStyle))
                If ShowCdf Then SeriesInfo.Ancestors("SeriesCollection").Nodes.Last.AddAfterSelf(Series(ShowCdf, SelParamSet, "CDF", Distrib.PdfInfo.SeriesLabel, "CdfArea", "Value", Distrib.CdfInfo.Name, Color.Black, Distrib.Display.MarkerColor, Distrib.Display.LineColor, Main.Distribution.Info(0).CdfInfo.Display.MarkerStyle))
                'If ShowInvCdf Then SeriesInfo.Ancestors("SeriesCollection").Nodes.Last.AddAfterSelf(Series(ShowInvCdf, SelParamSet, "InvCDF", Distrib.PdfInfo.SeriesLabel, "InvCdfArea", "Value", Distrib.InvCdfInfo.Name, Color.Black, Distrib.Display.MarkerColor, Distrib.Display.LineColor, Main.Distribution.Distrib.InvCdfInfo.Display.MarkerStyle))
                'If ShowInvCdf Then SeriesInfo.Ancestors("SeriesCollection").Nodes.Last.AddAfterSelf(Series(ShowInvCdf, SelParamSet, "InvCDF", Distrib.PdfInfo.SeriesLabel, "InvCdfArea", "Probability", Distrib.InvCdfInfo.Name, Color.Black, Distrib.Display.MarkerColor, Distrib.Display.LineColor, Main.Distribution.Distrib.InvCdfInfo.Display.MarkerStyle))
                If ShowInvCdf Then SeriesInfo.Ancestors("SeriesCollection").Nodes.Last.AddAfterSelf(Series(ShowInvCdf, SelParamSet, "InvCDF", Distrib.PdfInfo.SeriesLabel, "InvCdfArea", "Probability", Distrib.InvCdfInfo.Name, Color.Black, Distrib.Display.MarkerColor, Distrib.Display.LineColor, Main.Distribution.Info(0).InvCdfInfo.Display.MarkerStyle))
                'Main.Distribution.ChartList(ChartName) = SeriesInfo 'Save the updated ChartInfo
                Main.Distribution.ChartList(ChartName) = ChartInfo 'Save the updated ChartInfo
                Main.Distribution.Modified = True
                Main.ChartList(I).Plot() 'Replot the updated chart
                Main.ChartList(I).ReloadChartSettings()  'Reload the ChartInfo in any open ChartSettings form
            End If
        Next

        ''Redisplay all open charts:
        'Main.ReplotAllOpenCharts()
        ''Reload any ChartInfo data open in ChartSettings forms:



    End Sub

    Private Function Series(Exists As Boolean, DistribNo As Integer, FunctionType As String, SeriesName As String, ChartAreaName As String, XFieldName As String, YFieldName As String, BorderColor As Color, FillColor As Color, LineColor As Color, MarkerStyle As String) As IEnumerable(Of XElement)
        'Generate a ChartInfo Series from the specified XFieldName and YFieldName.
        'DistribNo is 0 for the primary distribution.
        'DistribNo is 1, 2, ... for the secondary distributions.
        'FunctionType is PDF, PDFLn, PMF, PMFLn, CDF or InvCDF.

        If Exists Then
            Dim XDoc = <?xml version="1.0" encoding="utf-8"?>
                       <Series>
                           <DistribNo><%= DistribNo %></DistribNo>
                           <FunctionType><%= FunctionType %></FunctionType>
                           <Name><%= SeriesName %></Name>
                           <ChartType>Line</ChartType>
                           <ChartArea><%= ChartAreaName %></ChartArea>
                           <Legend>Legend1</Legend>
                           <AxisLabel/>
                           <XFieldName><%= XFieldName %></XFieldName>
                           <XAxisType>Primary</XAxisType>
                           <XValueType>Auto</XValueType>
                           <YFieldName><%= YFieldName %></YFieldName>
                           <YAxisType>Primary</YAxisType>
                           <YValueType>Auto</YValueType>
                           <Marker>
                               <BorderColor><%= BorderColor.ToArgb.ToString %></BorderColor>
                               <BorderWidth>1</BorderWidth>
                               <Fill>Yes</Fill>
                               <Color><%= FillColor.ToArgb.ToString %></Color>
                               <Size>6</Size>
                               <Step>1</Step>
                               <Style><%= MarkerStyle %></Style>
                           </Marker>
                           <Color><%= LineColor.ToArgb.ToString %></Color>
                           <Width>1</Width>
                           <ToolTip/>
                       </Series>

            Return XDoc.<Series>
        Else
            'This series does not exist. Return nothing.
        End If

    End Function



#End Region 'Form Methods ---------------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Form Events - Events that can be triggered by this form." '==========================================================================================================================

#End Region 'Form Events ----------------------------------------------------------------------------------------------------------------------------------------------------------------------


End Class