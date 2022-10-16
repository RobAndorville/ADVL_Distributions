Public Class frmSeriesAnalysis
    'Analysis and display of a data series.
    'Histograms, PDF and CDF charts.
    'Calculation of series statistics including mean, P50, P10 and P90 values.

#Region " Variable Declarations - All the variables used in this form and this application." '=================================================================================================

    'Public Data As New DataSet  'Dataset used to hold the data values. 
    Public CalcData As New DataSet  'Dataset used to hold the calculated data values. 

    Dim RawPixelsPerInterval As Integer = 200 'The number of pixels per X Axis annotation interval

    Public ModelDistrib As New DistributionInfo

#End Region 'Variable Declarations ------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Properties - All the properties used in this form and this application" '============================================================================================================

    Private _formNo As Integer = -1 'Multiple instances of this form can be displayed. FormNo is the index number of the form in SeriesAnalysisList.
    'If the form is included in Main.SeriesAnalysisList() then FormNo will be > -1 --> when exiting set Main.ClosedFormNo and call Main.SeriesAnalysisClosed(). 
    Public Property FormNo As Integer
        Get
            Return _formNo
        End Get
        Set(ByVal value As Integer)
            _formNo = value
        End Set
    End Property

    'Private _dataSource As Object = Nothing 'DataSource links to the oject containing the Data to be analysed. (Input, Processed, Distribution or MonteCarlo objects in the Main form.)
    'Property DataSource As Object
    '    Get
    '        Return _dataSource
    '    End Get
    '    Set(value As Object)
    '        _dataSource = value
    '        'txtDatasetName.Text = DataSource.Name 'DataSource DOES NOT HAVE NAME PROPERTY
    '        lblTableCount.Text = DataSource.Data.Tables.Count
    '        If DataSource.Data.Tables.Count > 0 Then
    '            lblTableNo.Text = "1"
    '            cmbSourceDataTable.Items.Clear()
    '            For Each item In DataSource.Data.Tables
    '                cmbSourceDataTable.Items.Add(item.TableName)
    '            Next
    '            cmbSourceDataTable.SelectedIndex = 0
    '            SourceTableName = cmbSourceDataTable.SelectedItem.ToString
    '            'NRows = DataSource.Data.Tables(0).Rows.Count
    '        Else
    '            lblTableNo.Text = "0"
    '        End If
    '    End Set
    'End Property

    'Private _dataSourceDescription As String = "" 'A description of the data source object.
    'Property DataSourceDescription As String
    '    Get
    '        Return _dataSourceDescription
    '    End Get
    '    Set(value As String)
    '        _dataSourceDescription = value
    '        txtDataSource.Text = _dataSourceDescription
    '    End Set
    'End Property

    'Private _sourceTableName As String = "" 'The name of the selected table in the DataSource.
    'Property SourceTableName As String
    '    Get
    '        Return _sourceTableName
    '    End Get
    '    Set(value As String)
    '        _sourceTableName = value

    '        If DataSource Is Nothing Then
    '            Main.Message.AddWarning("The data source is empty." & vbCrLf)
    '        Else
    '            If DataSource.Data.Tables.Contains(_sourceTableName) Then
    '                cmbSourceDataTable.SelectedIndex = cmbSourceDataTable.FindStringExact(_sourceTableName)
    '                lblTableNo.Text = cmbSourceDataTable.SelectedIndex + 1
    '                'ShowAllFieldStats()

    '                lblColCount.Text = DataSource.Data.Tables(_sourceTableName).Columns.Count
    '                lblColNo.Text = "0"

    '                NRows = DataSource.Data.Tables(_sourceTableName).Rows.Count

    '                'Get the list of columns:
    '                cmbSourceColumnName.Items.Clear()
    '                For Each item In DataSource.Data.Tables(_sourceTableName).Columns
    '                    cmbSourceColumnName.Items.Add(item.ColumnName)
    '                Next
    '            Else
    '                Main.Message.AddWarning("A table named " & _sourceTableName & " was not found." & vbCrLf)
    '            End If
    '        End If

    '    End Set
    'End Property

    Private _dataTableName As String = "" 'The name of the selected table in the DataSource.
    Property DataTableName As String
        Get
            Return _dataTableName
        End Get
        Set(value As String)
            _dataTableName = value

            'If DataSource Is Nothing Then
            '    Main.Message.AddWarning("The data source is empty." & vbCrLf)
            'Else
            'If DataSource.Data.Tables.Contains(_dataTableName) Then
            If Main.Distribution.Data.Tables.Contains(_dataTableName) Then
                cmbSourceDataTable.SelectedIndex = cmbSourceDataTable.FindStringExact(_dataTableName)
                cmbTable.SelectedIndex = cmbTable.FindStringExact(_dataTableName)
                lblTableNo.Text = cmbSourceDataTable.SelectedIndex + 1
                'ShowAllFieldStats()

                lblColCount.Text = Main.Distribution.Data.Tables(_dataTableName).Columns.Count
                lblColNo.Text = "0"

                NRows = Main.Distribution.Data.Tables(_dataTableName).Rows.Count

                'Get the list of columns:
                cmbSourceColumnName.Items.Clear()
                cmbColumn.Items.Clear()
                For Each item In Main.Distribution.Data.Tables(_dataTableName).Columns
                    cmbSourceColumnName.Items.Add(item.ColumnName)
                    cmbColumn.Items.Add(item.ColumnName)
                Next

            Else
                Main.Message.AddWarning("A table named " & _dataTableName & " was not found." & vbCrLf)
                End If
            'End If

        End Set
    End Property

    'Private _sourceColumnName As String = "" 'The name of the column in the Source Table containing the data series.
    'Property SourceColumnName As String
    '    Get
    '        Return _sourceColumnName
    '    End Get
    '    Set(value As String)
    '        _sourceColumnName = value
    '        If DataSource.Data.Tables.Contains(SourceTableName) Then
    '            If DataSource.Data.Tables(SourceTableName).Columns.Contains(_sourceColumnName) Then
    '                cmbSourceColumnName.SelectedIndex = cmbSourceColumnName.FindStringExact(_sourceColumnName)
    '                lblColNo.Text = cmbSourceColumnName.SelectedIndex + 1
    '                ShowSeriesStats()
    '            Else
    '                Main.Message.AddWarning("A column named " & _sourceColumnName & " was not found." & vbCrLf)
    '            End If
    '        End If
    '    End Set
    'End Property

    Private _dataColumnName As String = "" 'The name of the column in the Source Table containing the data series.
    Property DataColumnName As String
        Get
            Return _dataColumnName
        End Get
        Set(value As String)
            _dataColumnName = value
            'If DataSource.Data.Tables.Contains(SourceTableName) Then
            If Main.Distribution.Data.Tables.Contains(DataTableName) Then
                If Main.Distribution.Data.Tables(DataTableName).Columns.Contains(_dataColumnName) Then
                    cmbSourceColumnName.SelectedIndex = cmbSourceColumnName.FindStringExact(_dataColumnName)
                    cmbColumn.SelectedIndex = cmbColumn.FindStringExact(_dataColumnName)
                    lblColNo.Text = cmbSourceColumnName.SelectedIndex + 1
                    ShowSeriesStats()
                Else
                    Main.Message.AddWarning("A column named " & _dataColumnName & " was not found." & vbCrLf)
                End If
            End If
        End Set
    End Property

    Private _scalarName As String = "" 'The name of the selected scalar 
    Property ScalarName As String
        Get
            Return _scalarName
        End Get
        Set(value As String)
            _scalarName = value
        End Set
    End Property

    Private _isDiscrete As Boolean = False 'If True the distribution is discrete else continuous.
    Property IsDiscrete As Boolean
        Get
            Return _isDiscrete
        End Get
        Set(value As Boolean)
            _isDiscrete = value
            If _isDiscrete Then
                txtContDisc.Text = "Discrete"
            Else
                txtContDisc.Text = "Continuous"
            End If
        End Set
    End Property

    Private _nRows As Integer = 0 'The number of rows in the selected table.
    Property NRows As Integer
        Get
            Return _nRows
        End Get
        Set(value As Integer)
            _nRows = value
            txtNRows.Text = _nRows
        End Set
    End Property

    Private _minSeriesVal As Double = 0 'The minimum data value in the series 
    Property MinSeriesVal As Double
        Get
            Return _minSeriesVal
        End Get
        Set(value As Double)
            _minSeriesVal = value
            txtDataMin.Text = _minSeriesVal
            txtDataRange.Text = _maxSeriesVal - _minSeriesVal
        End Set
    End Property

    Private _maxSeriesVal As Double = 1 'The maximum data value in the series
    Property MaxSeriesVal As Double
        Get
            Return _maxSeriesVal
        End Get
        Set(value As Double)
            _maxSeriesVal = value
            txtDataMax.Text = _maxSeriesVal
            txtDataRange.Text = _maxSeriesVal - _minSeriesVal
        End Set
    End Property

    Private _isSample As Boolean = True 'If True, the data is a sample of the population. If False, the data is the whole population.
    Property IsSample As Boolean
        Get
            Return _isSample
        End Get
        Set(value As Boolean)
            _isSample = value
            If _isSample Then
                rbSample.Checked = True
            Else
                rbPopulation.Checked = True
            End If
            'If DataSource Is Nothing Then
            'Else
            'If DataSource.Data.Tables(SourceTableName) Is Nothing Then
            If Main.Distribution.Data.Tables(DataTableName) Is Nothing Then
            Else
                If Main.Distribution.Data.Tables(DataTableName).Columns.Contains(DataTableName) Then
                    Dim Var As Double = Variance(IsSample)
                    txtVariance.Text = Format(Var, txtVarFormat.Text)
                    txtStdDev.Text = Format(Math.Sqrt(Var), txtStdDevFormat.Text)
                Else
                End If
                End If
            'End If
        End Set
    End Property

    Private _showReverseCDF As Boolean = False 'If True, the Reverse Cumulative Distribution Function is shown
    Property ShowReverseCDF As Boolean
        Get
            Return _showReverseCDF
        End Get
        Set(value As Boolean)
            _showReverseCDF = value
            If _showReverseCDF = True Then
                rbReverseCdf.Checked = True
            Else
                rbCdf.Checked = True
            End If
        End Set
    End Property

    Private _initNIntervals As Integer = 10 'The initial number of intervals - this is refined using preferred interval settings
    Property InitNIntervals As Integer
        Get
            Return _initNIntervals
        End Get
        Set(value As Integer)
            _initNIntervals = value
        End Set
    End Property

    'Private _nIntervals As Integer = 50 'The number of intervals to use in the histogram
    Private _histNIntervals As Integer = 50 'The number of intervals to use in the histogram
    'Property NIntervals As Integer
    Property HistNIntervals As Integer
        Get
            Return _histNIntervals
        End Get
        Set(value As Integer)
            _histNIntervals = value
            txtNIntervals.Text = _histNIntervals
            txtIntervalWidth.Text = (MaxSeriesVal - MinSeriesVal) / _histNIntervals
        End Set
    End Property

    Private _histIntervalWidth As Double 'The data width of each column in the histogram
    Property HistIntervalWidth As Double
        Get
            Return _histIntervalWidth
        End Get
        Set(value As Double)
            _histIntervalWidth = value
        End Set
    End Property

    Private _histMin As Double 'The midpoint value of the minimum column in the histogram
    Property HistMin As Double
        Get
            Return _histMin
        End Get
        Set(value As Double)
            _histMin = value
        End Set
    End Property

    Private _histMax As Double 'The midpoint value of the maximum column in the histogram
    Property HistMax As Double
        Get
            Return _histMax
        End Get
        Set(value As Double)
            _histMax = value
        End Set
    End Property

    Private _chartXMin As Double 'The chart X Axis minimum 
    Property ChartXMin As Double
        Get
            Return _chartXMin
        End Get
        Set(value As Double)
            _chartXMin = value
        End Set
    End Property

    Private _chartXMax As Double 'The chart X Axis maximum
    Property ChartXMax As Double
        Get
            Return _chartXMax
        End Get
        Set(value As Double)
            _chartXMax = value
        End Set
    End Property

    Private _chartXInterval As Double 'The Chart X Axis label interval
    Property ChartXInterval As Double
        Get
            Return _chartXInterval
        End Get
        Set(value As Double)
            _chartXInterval = value
        End Set
    End Property


    'Private _tableName As String = "" 'The calculated data table name selected for display in the Data tab. (The calculated data includes the CDF points and the calculated histogram values.)
    'Property TableName As String
    '    Get
    '        Return _tableName
    '    End Get
    '    Set(value As String)
    '        _tableName = value
    '        cmbTableName.SelectedIndex = cmbTableName.FindStringExact(_tableName)
    '        UpdateDataGridView()
    '    End Set
    'End Property

    Private _calcTableName As String = "" 'The calculated data table name selected for display in the Data tab. (The calculated data includes the CDF points and the calculated histogram values.)
    Property CalcTableName As String
        Get
            Return _calcTableName
        End Get
        Set(value As String)
            _calcTableName = value
            cmbCalcTableName.SelectedIndex = cmbCalcTableName.FindStringExact(_calcTableName)
            UpdateDataGridView()
        End Set
    End Property

    'Distribution Model properties:
    Private _showModel As Boolean = False 'If True show the distribution model.
    Property ShowModel As Boolean
        Get
            Return _showModel
        End Get
        Set(value As Boolean)
            _showModel = value
            chkShowModel.Checked = _showModel
        End Set
    End Property

    Private _distributionName As String = "" 'The name of the model distribution
    Property DistributionName As String
        Get
            Return _distributionName
        End Get
        Set(value As String)
            _distributionName = value
            txtDistributionName.Text = _distributionName
            ModelDistrib.Name = value
            If _distributionName = "" Then
                chkShowModel.Checked = False
                chkShowModel.Enabled = False
                txtDistributionName.Enabled = False
                txtContDisc.Enabled = False
                txtParamAName.Enabled = False
                txtParamAValue.Enabled = False
                txtParamBName.Enabled = False
                txtParamBValue.Enabled = False
                txtParamCName.Enabled = False
                txtParamCValue.Enabled = False
                txtParamDName.Enabled = False
                txtParamDValue.Enabled = False
                txtParamEName.Enabled = False
                txtParamEValue.Enabled = False
            Else
                'chkShowModel.Checked = True
                chkShowModel.Enabled = True
                txtDistributionName.Enabled = True
                txtContDisc.Enabled = True
                txtParamAName.Enabled = True
                txtParamAValue.Enabled = True
                txtParamBName.Enabled = True
                txtParamBValue.Enabled = True
                txtParamCName.Enabled = True
                txtParamCValue.Enabled = True
                txtParamDName.Enabled = True
                txtParamDValue.Enabled = True
                txtParamEName.Enabled = True
                txtParamEValue.Enabled = True
            End If
        End Set
    End Property

    Private _continuity As String 'The Continuity of the distribution (Continuous / Discrete)
    Property Continuity As String
        Get
            Return _continuity
        End Get
        Set(value As String)
            _continuity = value
            txtContDisc.Text = value
        End Set
    End Property

    Private _nSamples As Integer = 256 'The number of samples to use in the Distriubtion Model
    Property NSamples As Integer
        Get
            Return _nSamples
        End Get
        Set(value As Integer)
            _nSamples = value
            txtNSamps.Text = _nSamples
        End Set
    End Property

    Private _paramAName As String = "" 'The name of the first distribution parameter
    Property ParamAName As String
        Get
            Return _paramAName
        End Get
        Set(value As String)
            _paramAName = value
            txtParamAName.Text = _paramAName
        End Set
    End Property

    Private _paramAValue As Double = Double.NaN 'The value of the first distribution parameter
    Property ParamAValue As Double
        Get
            Return _paramAValue
        End Get
        Set(value As Double)
            _paramAValue = value
            txtParamAValue.Text = _paramAValue
            ModelDistrib.ParamA.Value = value
        End Set
    End Property

    Private _paramBName As String = "" 'The name of the second distribution parameter
    Property ParamBName As String
        Get
            Return _paramBName
        End Get
        Set(value As String)
            _paramBName = value
            txtParamBName.Text = _paramBName
        End Set
    End Property

    Private _paramBValue As Double = Double.NaN 'The value of the second distribution parameter
    Property ParamBValue As Double
        Get
            Return _paramBValue
        End Get
        Set(value As Double)
            _paramBValue = value
            txtParamBValue.Text = _paramBValue
            ModelDistrib.ParamB.Value = value
        End Set
    End Property

    Private _paramCName As String = "" 'The name of the third distribution parameter
    Property ParamCName As String
        Get
            Return _paramCName
        End Get
        Set(value As String)
            _paramCName = value
            txtParamCName.Text = _paramCName
        End Set
    End Property

    Private _paramCValue As Double = Double.NaN 'The value of the third distribution parameter
    Property ParamCValue As Double
        Get
            Return _paramCValue
        End Get
        Set(value As Double)
            _paramCValue = value
            txtParamCValue.Text = _paramCValue
            ModelDistrib.ParamC.Value = value
        End Set
    End Property

    Private _paramDName As String = "" 'The name of the fourth distribution parameter
    Property ParamDName As String
        Get
            Return _paramDName
        End Get
        Set(value As String)
            _paramDName = value
            txtParamDName.Text = _paramDName
        End Set
    End Property

    Private _paramDValue As Double = Double.NaN 'The value of the fourth distribution parameter
    Property ParamDValue As Double
        Get
            Return _paramDValue
        End Get
        Set(value As Double)
            _paramDValue = value
            txtParamDValue.Text = _paramDValue
            ModelDistrib.ParamD.Value = value
        End Set
    End Property

    Private _paramEName As String = "" 'The name of the fifth distribution parameter
    Property ParamEName As String
        Get
            Return _paramEName
        End Get
        Set(value As String)
            _paramEName = value
            txtParamEName.Text = _paramEName
        End Set
    End Property

    Private _paramEValue As Double = Double.NaN 'The value of the fifth distribution parameter
    Property ParamEValue As Double
        Get
            Return _paramEValue
        End Get
        Set(value As Double)
            _paramEValue = value
            txtParamEValue.Text = _paramEValue
            ModelDistrib.ParamE.Value = value
        End Set
    End Property

    'The number of the selected distribution.
    'This value is copied from the Adjust form when this Series Analysis form is opened.
    Private _selDistrib As Integer = 0 'The selected Distribution index number in the collection of secondary distributions. (0 if none selected.)
    Property SelDistrib As Integer
        Get
            Return _selDistrib
        End Get
        Set(value As Integer)
            _selDistrib = value
            'txtSelDistrib.Text = _selDistrib
        End Set
    End Property


#End Region 'Properties -----------------------------------------------------------------------------------------------------------------------------------------------------------------------

#Region " Process XML files - Read and write XML files." '=====================================================================================================================================

    Private Sub SaveFormSettings()
        'Save the form settings in an XML document.

        dgvAnnot.AllowUserToAddRows = False 'This removed the last blank line

        Dim settingsData = <?xml version="1.0" encoding="utf-8"?>
                           <!---->
                           <FormSettings>
                               <Left><%= Me.Left %></Left>
                               <Top><%= Me.Top %></Top>
                               <Width><%= Me.Width %></Width>
                               <Height><%= Me.Height %></Height>
                               <!---->
                               <IsSample><%= IsSample %></IsSample>
                               <MinFormat><%= txtMinFormat.Text %></MinFormat>
                               <MaxFormat><%= txtMaxFormat.Text %></MaxFormat>
                               <SumFormat><%= txtSumFormat.Text %></SumFormat>
                               <AvgFormat><%= txtAvgFormat.Text %></AvgFormat>
                               <StdDevFormat><%= txtStdDevFormat.Text %></StdDevFormat>
                               <VarFormat><%= txtVarFormat.Text %></VarFormat>
                               <ShowReverseCDF><%= ShowReverseCDF %></ShowReverseCDF>
                               <ShowModel><%= chkShowModel.Checked %></ShowModel>
                               <!---->
                               <Annotations>
                                   <%= From item In dgvAnnot.Rows
                                       Select
                                       <Item>
                                           <CdfChartChecked><%= item.Cells(0).Value %></CdfChartChecked>
                                           <HistogramChecked><%= item.Cells(1).Value %></HistogramChecked>
                                           <Type><%= item.Cells(2).Value %></Type>
                                           <Parameter><%= item.Cells(3).Value %></Parameter>
                                           <Label><%= item.Cells(4).Value %></Label>
                                           <Format><%= item.Cells(7).Value %></Format>
                                       </Item> %>
                               </Annotations>
                           </FormSettings>

        dgvAnnot.AllowUserToAddRows = True

        'Add code to include other settings to save after the comment line <!---->

        'Dim SettingsFileName As String = "FormSettings_" & Main.ApplicationInfo.Name & "_" & Me.Text & ".xml"
        'This version of the settings file name depends on the Monte Carlo model label and the Random Variable name:
        'Dim SettingsFileName As String = "FormSettings_" & Main.MonteCarlo.Label & "_" & SourceColumnName & "_" & Me.Text & ".xml"
        Dim SettingsFileName As String = "FormSettings_" & Main.Distribution.Label & "_" & SelDistrib & "_" & Me.Text & ".xml"

        Main.Project.SaveXmlSettings(SettingsFileName, settingsData)
    End Sub

    Private Sub RestoreFormSettings()
        'Read the form settings from an XML document.

        'Dim SettingsFileName As String = "FormSettings_" & Main.ApplicationInfo.Name & "_" & Me.Text & ".xml"
        'This version of the settings file name depends on the Monte Carlo model label and the Random Variable name:
        'Dim SettingsFileName As String = "FormSettings_" & Main.MonteCarlo.Label & "_" & SourceColumnName & "_" & Me.Text & ".xml"
        'NOTE: SourceColumnName is not avaiable when this method is run. Use Main.txtColumnName instead:
        'Dim SettingsFileName As String = "FormSettings_" & Main.MonteCarlo.Label & "_" & Main.txtColumnName.Text.Trim & "_" & Me.Text & ".xml"
        'NOTE: SourceColumnName is now set before the form is shown!
        'Dim SettingsFileName As String = "FormSettings_" & Main.MonteCarlo.Label & "_" & SourceColumnName & "_" & Me.Text & ".xml"
        Dim SettingsFileName As String = "FormSettings_" & Main.Distribution.Label & "_" & SelDistrib & "_" & Me.Text & ".xml"

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
            If Settings.<FormSettings>.<IsSample>.Value <> Nothing Then IsSample = Settings.<FormSettings>.<IsSample>.Value
            If Settings.<FormSettings>.<MinFormat>.Value <> Nothing Then txtMinFormat.Text = Settings.<FormSettings>.<MinFormat>.Value
            If Settings.<FormSettings>.<MaxFormat>.Value <> Nothing Then txtMaxFormat.Text = Settings.<FormSettings>.<MaxFormat>.Value
            If Settings.<FormSettings>.<SumFormat>.Value <> Nothing Then txtSumFormat.Text = Settings.<FormSettings>.<SumFormat>.Value
            If Settings.<FormSettings>.<AvgFormat>.Value <> Nothing Then txtAvgFormat.Text = Settings.<FormSettings>.<AvgFormat>.Value
            If Settings.<FormSettings>.<StdDevFormat>.Value <> Nothing Then txtStdDevFormat.Text = Settings.<FormSettings>.<StdDevFormat>.Value
            If Settings.<FormSettings>.<VarFormat>.Value <> Nothing Then txtVarFormat.Text = Settings.<FormSettings>.<VarFormat>.Value

            If Settings.<FormSettings>.<ShowReverseCDF>.Value <> Nothing Then
                ShowReverseCDF = Settings.<FormSettings>.<ShowReverseCDF>.Value
                'If ShowReverseCDF Then
                '    RecalcSeriesTable() 'Recalculate the series table. It would have originally calculated the non-reverse CDF by default.
                '    PlotCDF()
                '    UpdateAnnotation()
                'End If
            End If

            If Settings.<FormSettings>.<ShowModel>.Value <> Nothing Then
                If DistributionName = "" Then
                    chkShowModel.Checked = False 'Don't try to show the Distribution Model if the Distribution name has not been provided!
                Else
                    chkShowModel.Checked = Settings.<FormSettings>.<ShowModel>.Value
                End If
                'chkShowModel.Checked = Settings.<FormSettings>.<ShowModel>.Value
            End If



            'Restore annotations:
            Dim Annotations = From item In Settings.<FormSettings>.<Annotations>.<Item>
            For Each Item In Annotations
                If Item.<Format>.Value = Nothing Then
                    dgvAnnot.Rows.Add(Item.<CdfChartChecked>.Value, Item.<HistogramChecked>.Value, Item.<Type>.Value, Item.<Parameter>.Value, Item.<Label>.Value, 0, 0, "")
                Else
                    dgvAnnot.Rows.Add(Item.<CdfChartChecked>.Value, Item.<HistogramChecked>.Value, Item.<Type>.Value, Item.<Parameter>.Value, Item.<Label>.Value, 0, 0, Item.<Format>.Value)
                End If

            Next


            CheckFormPos()
        Else
            ApplyDefaultSettings()
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

        rbSample.Checked = True
        'NOTE: The default interval has been set before the form is loaded!:
        'NIntervals = 50

        dgvAnnot.ColumnCount = 5

        Dim chkAnnotCdf As New DataGridViewCheckBoxColumn
        dgvAnnot.Columns.Insert(0, chkAnnotCdf)
        dgvAnnot.Columns(0).HeaderText = "CDF Chart"
        dgvAnnot.Columns(0).Width = 40

        Dim chkAnnotHist As New DataGridViewCheckBoxColumn
        dgvAnnot.Columns.Insert(1, chkAnnotHist)
        dgvAnnot.Columns(1).HeaderText = "Hist Chart"
        dgvAnnot.Columns(1).Width = 40

        Dim cboAnnotType As New DataGridViewComboBoxColumn
        cboAnnotType.Items.Add("Probability")
        cboAnnotType.Items.Add("Value")
        cboAnnotType.Items.Add("Mean")
        cboAnnotType.Items.Add("Standard Deviation")
        dgvAnnot.Columns.Insert(2, cboAnnotType)
        dgvAnnot.Columns(2).HeaderText = "Annotation Type"
        dgvAnnot.Columns(2).Width = 120

        dgvAnnot.Columns(3).HeaderText = "Parameter"
        dgvAnnot.Columns(3).Width = 80
        dgvAnnot.Columns(4).HeaderText = "Label"
        dgvAnnot.Columns(4).Width = 80
        dgvAnnot.Columns(5).HeaderText = "Probability"
        dgvAnnot.Columns(5).Width = 80
        dgvAnnot.Columns(6).HeaderText = "Value"
        dgvAnnot.Columns(6).Width = 80
        dgvAnnot.Columns(7).HeaderText = "Format"
        dgvAnnot.Columns(7).Width = 80

        rbCdf.Checked = True 'Select ShowInverseCdf = False by default.
        chkShowModel.Checked = True 'Show Distribution Model by default.

        RestoreFormSettings()   'Restore the form settings
        txtWidth.Text = Me.Width
        txtHeight.Text = Me.Height
        txtTop.Text = Me.Top
        txtLeft.Text = Me.Left
        'CheckFormPos()

        ' UpdateAnnotation()

        'Main.Message.Add("Chart.Width: " & Chart1.Width & vbCrLf)

        'UpdateCharts()

        'Update Table list:
        For Each Item In Main.Distribution.Data.Tables
            cmbTable.Items.Add(Item.TableName)
        Next

        txtNSamps.Text = NSamples

    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        'Exit the Form

        'If FormNo > -1 Then
        '    Main.ClosedFormNo = FormNo 'The Main form property ClosedFormNo is set to this form number. This is used in the DataInfoFormClosed method to select the correct form to set to nothing.
        'End If

        Me.Close() 'Close the form
    End Sub

    Private Sub Form_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If WindowState = FormWindowState.Normal Then
            SaveFormSettings()
        Else
            'Dont save settings if the form is minimised.
        End If
    End Sub

    'Private Sub frmSeriesAnalysis_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    '    If FormNo > -1 Then
    '        'Main.SeriesAnalysisClosed()
    '        Main.Adjust.SeriesAnalysisClosed()
    '    End If

    'End Sub

#End Region 'Form Display Methods -------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Open and Close Forms - Code used to open and close other forms." '===================================================================================================================

#End Region 'Open and Close Forms -------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Form Methods - The main actions performed by this form." '===========================================================================================================================


    Public Sub UpdateCharts()
        'Update the Chart displays
        GetHistogramSettings()
        'CalcChartData()
        'CalcHistogramData()
        CalcChartData()
        'GetChartXScale(200) 'Get the chart X Scale settings with about 200 pixels between X Axis labels NOTE: THIS CANNOT BE DONE HERE - NEED ChartArea(0) DEFINED BEFORE THE SCALE IS CALCULATED - THIS IS NOW EMBEDDED IN PlotCharts()
        PlotCharts()
        UpdateAnnotation()
    End Sub

    Private Sub ReplotCharts()
        'Replot the charts without regenerating the chart data:
        GetHistogramSettings()
        PlotCharts()
        UpdateAnnotation()
    End Sub



    Public Sub ShowSeriesStats()
        'Show the statistics of the series.

        'If DataSource.Data.Tables(SourceTableName) Is Nothing Then
        If Main.Distribution.Data.Tables(DataTableName) Is Nothing Then
            Main.Message.AddWarning("There is no table named " & DataTableName & vbCrLf)
        Else
            'If DataSource.Data.Tables(DataTableName).Columns.Contains(SourceColumnName) Then
            If Main.Distribution.Data.Tables(DataTableName).Columns.Contains(DataColumnName) Then
                Dim RowNo As Integer
                Dim Var As Double
                'MinSeriesVal = Main.Distribution.Data.Tables(DataTableName).Compute("Min(" & SourceColumnName & ")", "")
                'MinSeriesVal = Main.Distribution.Data.Tables(DataTableName).Compute("Min(" & DataColumnName & ")", "")
                MinSeriesVal = Main.Distribution.Data.Tables(DataTableName).Compute("Min([" & DataColumnName & "])", "")
                MaxSeriesVal = Main.Distribution.Data.Tables(DataTableName).Compute("Max([" & DataColumnName & "])", "")

                txtMinimum.Text = Format(MinSeriesVal, txtMinFormat.Text)
                txtMaximum.Text = Format(MaxSeriesVal, txtMaxFormat.Text)
                txtSum.Text = Format(Main.Distribution.Data.Tables(DataTableName).Compute("Sum([" & DataColumnName & "])", ""), txtSumFormat.Text)
                txtAverage.Text = Format(Main.Distribution.Data.Tables(DataTableName).Compute("Avg([" & DataColumnName & "])", ""), txtAvgFormat.Text)
                Var = Variance(IsSample)
                txtVariance.Text = Format(Var, txtVarFormat.Text)
                txtStdDev.Text = Format(Math.Sqrt(Var), txtStdDevFormat.Text)
            Else
                Main.Message.AddWarning("There is no column named " & DataColumnName & vbCrLf)
            End If
        End If
    End Sub

    Private Function Variance(ByVal IsSample As Boolean) As Double
        'Calculates the variance of the data in column ColumnName.
        'If IsSample is True, the Sample variance is calculated, else the Population variance is calculated.

        'Dim Mean As Double = Main.Distribution.Data.Tables(SourceTableName).Compute("Avg(" & DataColumnName & ")", "")
        Dim Mean As Double = Main.Distribution.Data.Tables(DataTableName).Compute("Avg([" & DataColumnName & "])", "")
        Dim DiffSq As Double = 0

        Try
            For Each Row As DataRow In Main.Distribution.Data.Tables(DataTableName).Rows
                DiffSq += (Row.Item(DataColumnName) - Mean) ^ 2
            Next
        Catch ex As Exception
            Main.Message.AddWarning("Error calculating variance: " & vbCrLf & ex.Message & vbCrLf)
        End Try

        If IsSample Then
            Return DiffSq / (NRows - 1)
        Else
            Return DiffSq / NRows
        End If
    End Function


    Private Sub txtMinFormat_TextChanged(sender As Object, e As EventArgs) Handles txtMinFormat.TextChanged

    End Sub

    Private Sub txtMinFormat_LostFocus(sender As Object, e As EventArgs) Handles txtMinFormat.LostFocus
        If Main.Distribution.Data.Tables(DataTableName) Is Nothing Then
        Else
            If Main.Distribution.Data.Tables(DataTableName).Columns.Contains(DataColumnName) Then
                'Dim RowNo As Integer
                'Dim Var As Double
                txtMinimum.Text = Format(Main.Distribution.Data.Tables(DataTableName).Compute("Min([" & DataColumnName & "])", ""), txtMinFormat.Text)
            Else
            End If
        End If
    End Sub

    Private Sub btnFormatHelp_Click(sender As Object, e As EventArgs) Handles btnFormatHelp.Click
        'Show Format information.
        MessageBox.Show("Format string examples:" & vbCrLf & "N4 - Number displayed with thousands separator and 4 decimal places" & vbCrLf & "F4 - Number displayed with 4 decimal places.", "Number Formatting")
    End Sub

    Private Sub btnIsSample_Click(sender As Object, e As EventArgs) Handles btnIsSample.Click
        'Show the difference between the population and a sample of the population.
        MessageBox.Show("Is the series a sample of the population or the whole population?" & vbCrLf & "The standard devation and variance is calculated differently for a sample of the population and the whole population.", "Sample or Population")
    End Sub

    Private Sub cmbColumnName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSourceColumnName.SelectedIndexChanged
        If cmbSourceColumnName.Focused Then
            'SourceColumnName = cmbSourceColumnName.SelectedItem.ToString
            DataColumnName = cmbSourceColumnName.SelectedItem.ToString
        End If
    End Sub

    Private Sub rbSample_CheckedChanged(sender As Object, e As EventArgs) Handles rbSample.CheckedChanged
        If rbSample.Focused Then
            If rbSample.Checked Then
                IsSample = True
            End If
        End If
    End Sub

    Private Sub rbPopulation_CheckedChanged(sender As Object, e As EventArgs) Handles rbPopulation.CheckedChanged
        If rbPopulation.Focused Then
            If rbPopulation.Checked Then
                IsSample = False
            End If
        End If
    End Sub

    Private Sub txtMaxFormat_TextChanged(sender As Object, e As EventArgs) Handles txtMaxFormat.TextChanged

    End Sub

    Private Sub txtMaxFormat_LostFocus(sender As Object, e As EventArgs) Handles txtMaxFormat.LostFocus
        If Main.Distribution.Data.Tables(DataTableName) Is Nothing Then
        Else
            If Main.Distribution.Data.Tables(DataTableName).Columns.Contains(DataColumnName) Then
                'Dim RowNo As Integer
                'Dim Var As Double
                txtMaximum.Text = Format(Main.Distribution.Data.Tables(DataTableName).Compute("Max([" & DataColumnName & "])", ""), txtMaxFormat.Text)
            Else
            End If
        End If
    End Sub

    Private Sub txtSumFormat_TextChanged(sender As Object, e As EventArgs) Handles txtSumFormat.TextChanged

    End Sub

    Private Sub txtSumFormat_LostFocus(sender As Object, e As EventArgs) Handles txtSumFormat.LostFocus
        If Main.Distribution.Data.Tables(DataTableName) Is Nothing Then
        Else
            If Main.Distribution.Data.Tables(DataTableName).Columns.Contains(DataColumnName) Then
                'Dim RowNo As Integer
                'Dim Var As Double
                txtSum.Text = Format(Main.Distribution.Data.Tables(DataTableName).Compute("Sum([" & DataColumnName & "])", ""), txtSumFormat.Text)
            Else
            End If
        End If
    End Sub

    Private Sub txtAvgFormat_TextChanged(sender As Object, e As EventArgs) Handles txtAvgFormat.TextChanged

    End Sub

    Private Sub txtAvgFormat_LostFocus(sender As Object, e As EventArgs) Handles txtAvgFormat.LostFocus
        If Main.Distribution.Data.Tables(DataTableName) Is Nothing Then
        Else
            If Main.Distribution.Data.Tables(DataTableName).Columns.Contains(DataColumnName) Then
                'Dim RowNo As Integer
                'Dim Var As Double
                txtAverage.Text = Format(Main.Distribution.Data.Tables(DataTableName).Compute("Avg([" & DataColumnName & "])", ""), txtAvgFormat.Text)
            Else
            End If
        End If
    End Sub

    Private Sub txtStdDevFormat_TextChanged(sender As Object, e As EventArgs) Handles txtStdDevFormat.TextChanged

    End Sub

    Private Sub txtStdDevFormat_LostFocus(sender As Object, e As EventArgs) Handles txtStdDevFormat.LostFocus
        If Main.Distribution.Data.Tables(DataTableName) Is Nothing Then
        Else
            If Main.Distribution.Data.Tables(DataTableName).Columns.Contains(DataColumnName) Then
                'Dim RowNo As Integer
                'Dim Var As Double
                'txtAverage.Text = Format(DataSource.Data.Tables(TableName).Compute("Avg(" & ColumnName & ")", ""), txtAvgFormat.Text)
                Dim Var As Double = Variance(IsSample)
                'txtVariance.Text = Format(Var, txtVarFormat.Text)
                txtStdDev.Text = Format(Math.Sqrt(Var), txtStdDevFormat.Text)
            Else
            End If
        End If
    End Sub

    Private Sub txtVarFormat_TextChanged(sender As Object, e As EventArgs) Handles txtVarFormat.TextChanged

    End Sub

    Private Sub txtVarFormat_LostFocus(sender As Object, e As EventArgs) Handles txtVarFormat.LostFocus
        If Main.Distribution.Data.Tables(DataTableName) Is Nothing Then
        Else
            If Main.Distribution.Data.Tables(DataTableName).Columns.Contains(DataColumnName) Then
                'Dim RowNo As Integer
                'Dim Var As Double
                'txtAverage.Text = Format(DataSource.Data.Tables(TableName).Compute("Avg(" & ColumnName & ")", ""), txtAvgFormat.Text)
                Dim Var As Double = Variance(IsSample)
                txtVariance.Text = Format(Var, txtVarFormat.Text)
                'txtStdDev.Text = Format(Math.Sqrt(Var), txtStdDevFormat.Text)
            Else
            End If
        End If
    End Sub


    Private Sub CalcChartData()
        'Calculate the chart data.

        'Series                 Table of calculated data used to plot the CDF
        '    Value              The value of a random variable
        '    CDF                The CDF probability of that value               (for continuous distributions)
        '    Reverse_CDF        The Reverse CDF probability of that value       (for continuous distributions)
        '    Model_CDF          The model CDF probability of that value         (for continuous distributions)
        '    Model_Rev_CDF      The model Reverse CDF probability of that value (for continuous distributions)
        '    Model_Prob_Dens    The model Probability Density at that value     (for continuous distributions)

        'UPDATE:
        'Histogram
        '    Mid_Interval
        '    Count
        '    Probability        The probability of a value in that interval     (for continuous distributions)
        '    Prob_Mass          The probability mass at that value              (for discrete distributions)
        '    Prob_Density       The probability density in that interval        (for continuous distributions)
        '    Model_CDF          The model CDF probability of that value         (for discrete distributions)
        '    Model_Rev_CDF      The model Reverse CDF probability of that value (for discrete distributions)
        '    Model_Prob_Mass    The model probability mass at that value        (for discrete distributions)

        'If Main.BayesSim.Data.Tables.Contains(SourceTableName) Then
        If Main.Distribution.Data.Tables.Contains(DataTableName) Then
            'OK the required table exists.
        Else
            Main.Message.AddWarning("The table named " & DataTableName & " does not exist." & vbCrLf)
            Exit Sub
        End If

        CalcData.Clear()
        CalcData.Reset()
        'cmbTableName.Items.Clear()
        cmbCalcTableName.Items.Clear()

        'Generate the data used to plot the Cumulative Distribution Function: -----------------------------------------------------------------------------
        CalcData.Tables.Add("Series") 'Create the Series table.
        cmbCalcTableName.Items.Add("Series") 'Add the table to the selection list

        CalcData.Tables("Series").Columns.Add("Value", Main.Distribution.Data.Tables(DataTableName).Columns(DataColumnName).DataType) 'Create the Value column
        'Copy the data from the DataSource to the Series table:
        For Each Row As DataRow In Main.Distribution.Data.Tables(DataTableName).Rows
            CalcData.Tables("Series").Rows.Add(Row.Item(DataColumnName))
        Next

        CalcData.Tables("Series").DefaultView.Sort = "Value ASC"     'Sort the data in ascending order

        Dim NValues As Integer = CalcData.Tables("Series").Rows.Count 'NValues is the number of data values in the series.

        If IsDiscrete Then
            'The calculated values for a discrete distribution are in the Histogram table.
        Else
            'CalcData.Tables("Series").Columns.Add("CDF", System.Type.GetType("System.Double"))    'Create the Cumulative Distribution Function Probability column (for continuous distributions)
            CalcData.Tables("Series").Columns.Add("eCDF", System.Type.GetType("System.Double"))    'Create the Cumulative Distribution Function Probability column (for continuous distributions)
            'CalcData.Tables("Series").Columns.Add("Reverse_CDF", System.Type.GetType("System.Double"))    'Create the Reverse Cumulative Distribution Function Probability column (for continuous distributions)
            CalcData.Tables("Series").Columns.Add("Reverse_eCDF", System.Type.GetType("System.Double"))    'Create the Reverse Cumulative Distribution Function Probability column (for continuous distributions)

            'NOTE: The Value column is sorted in ascending order. The CDF column ranges from 1/NValues to 1 in uniform increments.
            '      So a cross-plot of Value on the X axis and CDF on the Y axis displays the Cumulative Density Function.
            For I = 1 To NValues
                'CalcData.Tables("Series").DefaultView.Item(I - 1).Item("CDF") = I / NValues 'Calculate each CDF value (for continuous distributions)
                CalcData.Tables("Series").DefaultView.Item(I - 1).Item("eCDF") = I / NValues 'Calculate each CDF value (for continuous distributions)
                'CalcData.Tables("Series").DefaultView.Item(I - 1).Item("Reverse_CDF") = 1 - (I / NValues) 'Calculate each Reverse_CDF value (for continuous distributions)
                CalcData.Tables("Series").DefaultView.Item(I - 1).Item("Reverse_eCDF") = 1 - (I / NValues) 'Calculate each Reverse_CDF value (for continuous distributions)
            Next
            'End If
        End If

        CalcTableName = "Series" 'This updates the cmbTableName selection and updates the data displayed in the Data tab.

        '==================================================================================================================================================


        'Generate the Histogram data ----------------------------------------------------------------------------------------------------------------------

        CalcData.Tables.Add("Histogram") 'Create the Histogram table
        cmbCalcTableName.Items.Add("Histogram") 'Add the table to the selection list

        'CalcData.Tables("Histogram").Columns.Add("Value", System.Type.GetType("System.Double"))        'Create the Value column
        'CalcData.Tables("Histogram").Columns.Add("Survey_Prob", System.Type.GetType("System.Double"))        'Create the Survey Probability column - this is the probability corresponding to the Survey Count in the Value column. UPDATE:15May22
        'CalcData.Tables("Histogram").Columns.Add("Count", System.Type.GetType("System.Int32"))         'Create the Count column
        'CalcData.Tables("Histogram").Columns.Add("Probability", System.Type.GetType("System.Double"))  'Create the probability column

        ''3Sep21 - Adding cumulative counts and probabilities:
        'CalcData.Tables("Histogram").Columns.Add("Cum_Count", System.Type.GetType("System.Int32"))         'Create the Cumulative Count column
        'CalcData.Tables("Histogram").Columns.Add("Cum_Probability", System.Type.GetType("System.Double"))  'Create the Cumulative probability column
        'CalcData.Tables("Histogram").Columns.Add("Reverse_Cum_Prob", System.Type.GetType("System.Double"))  'Create the Reverse Cumulative probability column

        If IsDiscrete Then 'This is a discrete distribution
            CalcData.Tables("Histogram").Columns.Add("Value", System.Type.GetType("System.Double"))        'Create the Value column
            'CalcData.Tables("Histogram").Columns.Add("Survey_Prob", System.Type.GetType("System.Double"))        'Create the Survey Probability column - this is the probability corresponding to the Survey Count in the Value column. UPDATE:15May22
            CalcData.Tables("Histogram").Columns.Add("Count", System.Type.GetType("System.Int32"))         'Create the Count column
            CalcData.Tables("Histogram").Columns.Add("Probability", System.Type.GetType("System.Double"))  'Create the probability column

            '3Sep21 - Adding cumulative counts and probabilities:
            CalcData.Tables("Histogram").Columns.Add("Cum_Count", System.Type.GetType("System.Int32"))         'Create the Cumulative Count column
            CalcData.Tables("Histogram").Columns.Add("Cum_Probability", System.Type.GetType("System.Double"))  'Create the Cumulative probability column
            CalcData.Tables("Histogram").Columns.Add("Reverse_Cum_Prob", System.Type.GetType("System.Double"))  'Create the Reverse Cumulative probability column

            Dim DistVal As Integer = MinSeriesVal 'The current distribution value
            Dim PointCount As Integer = 0 'The count of points having the current distribution value
            Dim CumPointCount As Integer = 0 'The cumulative count of points.
            For Each Row As DataRowView In CalcData.Tables("Series").DefaultView 'This uses the sorted row view
                If Row.Item("Value") = DistVal Then
                    PointCount += 1
                    CumPointCount += 1
                Else
                    'CalcData.Tables("Histogram").Rows.Add(DistVal, DistVal / NTrials, PointCount, PointCount / NRows, CumPointCount, CumPointCount / NRows, 1 - (CumPointCount / NRows)) 'Write the Value, Count, Probability, Cumulative Count, Cumulative Probability and Reverse Cumulative Probability to the table. (Discrete distributions do not have Prob_Density values.)
                    CalcData.Tables("Histogram").Rows.Add(DistVal, DistVal / NValues, PointCount, PointCount / NRows, CumPointCount, CumPointCount / NRows, 1 - (CumPointCount / NRows)) 'Write the Value, Count, Probability, Cumulative Count, Cumulative Probability and Reverse Cumulative Probability to the table. (Discrete distributions do not have Prob_Density values.)
                    PointCount = 0 'Reset the point count
                    DistVal += 1 'Increment the distribution value
                    'While Row.Item("Value") <> DistVal
                    '    CalcData.Tables("Histogram").Rows.Add(DistVal, DistVal / NTrials, 0, 0, CumPointCount, CumPointCount / NRows, 1 - (CumPointCount / NRows)) 'Write the Value, Count, Probability, Cumulative Count, Cumulative Probability and Reverse Cumulative Probability to the table. (The count and probability are zero.)
                    While Row.Item("Value") <> DistVal
                        CalcData.Tables("Histogram").Rows.Add(DistVal, DistVal / NValues, 0, 0, CumPointCount, CumPointCount / NRows, 1 - (CumPointCount / NRows)) 'Write the Value, Count, Probability, Cumulative Count, Cumulative Probability and Reverse Cumulative Probability to the table. (The count and probability are zero.)
                        DistVal += 1 'Increment the distribution value
                    End While
                    PointCount += 1
                    CumPointCount += 1
                End If
            Next
            'If PointCount > 0 Then CalcData.Tables("Histogram").Rows.Add(DistVal, DistVal / NTrials, PointCount, PointCount / NRows, CumPointCount, CumPointCount / NRows, 1 - (CumPointCount / NRows)) 'Write the last PointCount.
            If PointCount > 0 Then CalcData.Tables("Histogram").Rows.Add(DistVal, DistVal / NValues, PointCount, PointCount / NRows, CumPointCount, CumPointCount / NRows, 1 - (CumPointCount / NRows)) 'Write the last PointCount.
        Else 'This is a continuous distribution
            CalcData.Tables("Histogram").Columns.Add("Value", System.Type.GetType("System.Double"))        'Create the Value column
            CalcData.Tables("Histogram").Columns.Add("Count", System.Type.GetType("System.Int32"))         'Create the Count column
            CalcData.Tables("Histogram").Columns.Add("Probability", System.Type.GetType("System.Double"))  'Create the probability column
            CalcData.Tables("Histogram").Columns.Add("Prob_Density", System.Type.GetType("System.Double"))  'Create the Probability Density column (Only for a continuous distribution)
            Dim StartGap As Double = HistMin - MinSeriesVal 'The gap between the first data value in the series and the midpoint of the first column in the histogram.
            Dim EndInterval As Double = HistMin + HistIntervalWidth / 2 'The maximum value of the current histogram interval
            Dim PointCount As Integer = 0 'The count of points within the current histogram interval
            For Each Row As DataRowView In CalcData.Tables("Series").DefaultView 'This uses the sorted row view
                If Row.Item("Value") < EndInterval Then
                    PointCount += 1
                Else
                    CalcData.Tables("Histogram").Rows.Add(EndInterval - HistIntervalWidth / 2, PointCount, PointCount / NRows, PointCount / NRows / HistIntervalWidth) 'Write the Mid_Interval, Count, Probability and Prob_Density to the table.
                    PointCount = 0
                    EndInterval += HistIntervalWidth
                End If
            Next

            If ShowModel Then
                CalcData.Tables.Add("ModelData")
                cmbCalcTableName.Items.Add("ModelData") 'Add the table to the selection list
                CalcData.Tables("ModelData").Columns.Add("Value", System.Type.GetType("System.Double")) 'Create the Value column
                CalcData.Tables("ModelData").Columns.Add("Prob_Density", System.Type.GetType("System.Double")) 'Create the Probability Density column
                CalcData.Tables("ModelData").Columns.Add("CDF", System.Type.GetType("System.Double")) 'Create the CDF column
                CalcData.Tables("ModelData").Columns.Add("Reverse_CDF", System.Type.GetType("System.Double")) 'Create the ReverseCDF column
                Dim I As Integer
                Dim SampInt As Double = (HistMax - HistMin) / (NSamples - 1)
                Dim Value As Double = HistMin
                For I = 1 To NSamples
                    CalcData.Tables("ModelData").Rows.Add(Value, ModelDistrib.PDF(Value), ModelDistrib.CDF(Value), ModelDistrib.RevCDF(Value))
                    Value += SampInt
                Next
            End If
        End If

        Dim NHistRows As Integer = CalcData.Tables("Histogram").Rows.Count 'NHistRows is the number of Histogram rows.

        '==================================================================================================================================================

        'If chkShowModel.Checked Then
        '    '    'Generate the data used to plot the Model CDF and PDF (or PMF): -------------------------------------------------------------------

        '    If IsDiscrete Then
        '        CalcData.Tables("Histogram").Columns.Add("Model_CDF", System.Type.GetType("System.Double"))       'Create the Model CDF column              (for discrete distributions)
        '        CalcData.Tables("Histogram").Columns.Add("Model_Rev_CDF", System.Type.GetType("System.Double"))   'Create the Model Reverse CDF column      (for discrete distributions)
        '        CalcData.Tables("Histogram").Columns.Add("Model_Prob_Mass", System.Type.GetType("System.Double")) 'Create the Model Probability Mass column (for discrete distributions)
        '    Else
        '        CalcData.Tables("Series").Columns.Add("Model_CDF", System.Type.GetType("System.Double"))       'Create the Model CDF column                 (for continuous distributions)
        '        CalcData.Tables("Series").Columns.Add("Model_Rev_CDF", System.Type.GetType("System.Double"))   'Create the Model Reverse CDF column         (for continuous distributions)
        '        CalcData.Tables("Series").Columns.Add("Model_Prob_Dens", System.Type.GetType("System.Double")) 'Create the Model Probability Density column (for continuous distributions)
        '    End If
        'End If


    End Sub


    'UPDATE: Calculate the Histogram data in DataSource.Data
    'UPDATE: NOW USING CalcChartData()
    Private Sub CalcHistogramData()

        'If Main.Distribution.Data.Tables.Contains("Histogram") Then
        If CalcData.Tables.Contains("Histogram") Then
            'Main.Distribution.Data.Tables.Remove("Histogram")
            CalcData.Tables.Remove("Histogram")
        Else
            cmbCalcTableName.Items.Add("Histogram") 'Add the table to the selection list
        End If

        'Main.Distribution.Data.Tables.Add("Histogram") 'Create the Histogram table
        CalcData.Tables.Add("Histogram") 'Create the Histogram table
        'cmbTableName.Items.Add("Histogram") 'Add the table to the selection list

        'Main.Distribution.Data.Tables("Histogram").Columns.Add("Mid_Interval", System.Type.GetType("System.Double")) 'Create the Mid Interval column
        'CalcData.Tables("Histogram").Columns.Add("Mid_Interval", System.Type.GetType("System.Double")) 'Create the Mid Interval column
        CalcData.Tables("Histogram").Columns.Add("Value", System.Type.GetType("System.Double")) 'Create the Mid Interval column 'UPDATE 15May22 - Use Value instead of Mid_Interval ????
        'Main.Distribution.Data.Tables("Histogram").Columns.Add("Count", System.Type.GetType("System.Int32"))         'Create the Count column
        CalcData.Tables("Histogram").Columns.Add("Count", System.Type.GetType("System.Int32"))         'Create the Count column
        'Main.Distribution.Data.Tables("Histogram").Columns.Add("Probability", System.Type.GetType("System.Double"))  'Create the probability column
        CalcData.Tables("Histogram").Columns.Add("Probability", System.Type.GetType("System.Double"))  'Create the probability column

        'If IsDiscrete Then Else Data.Tables("Histogram").Columns.Add("Prob_Density", System.Type.GetType("System.Double"))  'Create the probability density column (Only for a continuous distribution)

        If IsDiscrete Then 'This is a discrete distribution
            Dim DistVal As Integer = MinSeriesVal 'The current distribution value
            Dim PointCount As Integer = 0 'The count of points having the current distribution value
            'For Each Row As DataRowView In DataSource.Data.Tables("Series").DefaultView 'This uses the sorted row view
            'For Each Row As DataRowView In Main.Distribution.Data.Tables("Model_Fitting").DefaultView 'This uses the sorted row view
            For Each Row As DataRowView In CalcData.Tables("Series").DefaultView 'This uses the sorted row view
                If Row.Item("Value") = DistVal Then
                    PointCount += 1
                Else
                    'Main.Distribution.Data.Tables("Histogram").Rows.Add(DistVal, PointCount, PointCount / NRows) 'Write the Mid_Interval, Count,  and Probability to the table. (Discrete distributions do not have Prob_Density values.)
                    CalcData.Tables("Histogram").Rows.Add(DistVal, PointCount, PointCount / NRows) 'Write the Mid_Interval, Count,  and Probability to the table. (Discrete distributions do not have Prob_Density values.)
                    PointCount = 0 'Reset the point count
                    DistVal += 1 'Increment the distribution value
                End If
            Next
        Else 'This is a continuous distribution
            'Main.Distribution.Data.Tables("Histogram").Columns.Add("Prob_Density", System.Type.GetType("System.Double"))  'Create the probability density column (Only for a continuous distribution)
            CalcData.Tables("Histogram").Columns.Add("Prob_Density", System.Type.GetType("System.Double"))  'Create the probability density column (Only for a continuous distribution)
            Dim StartGap As Double = HistMin - MinSeriesVal 'The gap between the first data value in the series and the midpoint of the first column in the histogram.
            Dim EndInterval As Double = HistMin + HistIntervalWidth / 2 'The maximum value of the current histogram interval
            Dim PointCount As Integer = 0 'The count of points within the current histogram interval
            'For Each Row As DataRowView In DataSource.Data.Tables("Series").DefaultView 'This uses the sorted row view
            'For Each Row As DataRowView In Main.Distribution.Data.Tables("Model_Fitting").DefaultView 'This uses the sorted row view
            For Each Row As DataRowView In CalcData.Tables("Series").DefaultView 'This uses the sorted row view
                If Row.Item("Value") < EndInterval Then
                    PointCount += 1
                Else
                    'Main.Distribution.Data.Tables("Histogram").Rows.Add(EndInterval - HistIntervalWidth / 2, PointCount, PointCount / NRows, PointCount / NRows / HistIntervalWidth) 'Write the Mid_Interval, Count, Probability and Prob_Density to the table.
                    CalcData.Tables("Histogram").Rows.Add(EndInterval - HistIntervalWidth / 2, PointCount, PointCount / NRows, PointCount / NRows / HistIntervalWidth) 'Write the Mid_Interval, Count, Probability and Prob_Density to the table.
                    PointCount = 0
                    EndInterval += HistIntervalWidth
                End If
            Next
        End If

        'Dim NHistRows As Integer = Main.Distribution.Data.Tables("Histogram").Rows.Count 'NHistRows is the number of Histogram rows.
        Dim NHistRows As Integer = CalcData.Tables("Histogram").Rows.Count 'NHistRows is the number of Histogram rows.
    End Sub

    'Private Sub CalcChartData()
    '    'Calculate the chart data.

    '    'Series             Table of calculated data used to plot the CDF
    '    '    Value          The value of a random variable
    '    '    Probability    The probability of that value
    '    '    Model_Prob     The distribution model probability

    '    'Histogram
    '    '    Mid_Interval
    '    '    Count
    '    '    Probability
    '    '    Prob_Density
    '    '    Model_Prob
    '    '    Model_Prob_Dens

    '    'UPDATE:
    '    'Series                 Table of calculated data used to plot the CDF
    '    '    Value              The value of a random variable
    '    '    CDF                The CDF probability of that value               (for continuous distributions)
    '    '    Reverse_CDF        The Reverse CDF probability of that value       (for continuous distributions)
    '    '    Model_CDF          The model CDF probability of that value         (for continuous distributions)
    '    '    Model_Rev_CDF      The model Reverse CDF probability of that value (for continuous distributions)
    '    '    Model_Prob_Dens    The model Probability Density at that value     (for continuous distributions)

    '    'UPDATE:
    '    'Histogram
    '    '    Mid_Interval
    '    '    Count
    '    '    Probability        The probability of a value in that interval     (for continuous distributions)
    '    '    Prob_Mass          The probability mass at that value              (for discrete distributions)
    '    '    Prob_Density       The probability density in that interval        (for continuous distributions)
    '    '    Model_CDF          The model CDF probability of that value         (for discrete distributions)
    '    '    Model_Rev_CDF      The model Reverse CDF probability of that value (for discrete distributions)
    '    '    Model_Prob_Mass    The model probability mass at that value        (for discrete distributions)

    '    Data.Clear()
    '    Data.Reset()
    '    cmbTableName.Items.Clear()

    '    'Generate the data used to plot the Cumulative Distribution Function: -----------------------------------------------------------------------------
    '    Data.Tables.Add("Series") 'Create the Series table.
    '    cmbTableName.Items.Add("Series") 'Add the table to the selection list

    '    Data.Tables("Series").Columns.Add("Value", DataSource.Data.Tables(SourceTableName).Columns(SourceColumnName).DataType) 'Create the Value column
    '    'Copy the data from the DataSource to the Series table:
    '    For Each Row As DataRow In DataSource.Data.Tables(SourceTableName).Rows
    '        Data.Tables("Series").Rows.Add(Row.Item(SourceColumnName))
    '    Next

    '    Data.Tables("Series").DefaultView.Sort = "Value ASC"     'Sort the data in ascending order

    '    Dim NValues As Integer = Data.Tables("Series").Rows.Count 'NValues is the number of data values in the series.

    '    'Data.Tables("Series").Columns.Add("Probability", System.Type.GetType("System.Double"))    'Create the Probability column
    '    If IsDiscrete Then
    '        'The calculated values for a discrete distribution are in the Histogram table.
    '    Else
    '        Data.Tables("Series").Columns.Add("CDF", System.Type.GetType("System.Double"))    'Create the Cumulative Distribution Function Probability column (for continuous distributions)
    '        Data.Tables("Series").Columns.Add("Reverse_CDF", System.Type.GetType("System.Double"))    'Create the Reverse Cumulative Distribution Function Probability column (for continuous distributions)

    '        'NOTE: Model data is generated later - this uses a lengthy Select Case statement and is in a separate loop
    '        'If chkShowModel.Checked Then
    '        '    Data.Tables("Series").Columns.Add("Model_CDF", System.Type.GetType("System.Double"))    'Create the Model Cumulative Distribution Function Probability column (for continuous distributions)
    '        '    Data.Tables("Series").Columns.Add("Model_Rev_CDF", System.Type.GetType("System.Double"))    'Create the Model Reverse Cumulative Distribution Function Probability column (for continuous distributions)
    '        '    Data.Tables("Series").Columns.Add("Model_Prob_Dens", System.Type.GetType("System.Double"))    'Create the Model Probability Density Function column (for continuous distributions)
    '        '    For I = 1 To NValues
    '        '        Data.Tables("Series").DefaultView.Item(I - 1).Item("CDF") = I / NValues 'Calculate each CDF value (for continuous distributions)
    '        '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Reverse_CDF") = 1 - (I / NValues) 'Calculate each Reverse_CDF value (for continuous distributions)

    '        '    Next
    '        'Else
    '        For I = 1 To NValues
    '            Data.Tables("Series").DefaultView.Item(I - 1).Item("CDF") = I / NValues 'Calculate each CDF value (for continuous distributions)
    '            Data.Tables("Series").DefaultView.Item(I - 1).Item("Reverse_CDF") = 1 - (I / NValues) 'Calculate each Reverse_CDF value (for continuous distributions)
    '        Next
    '        'End If
    '    End If

    '    ''Dim NIntervals As Integer = Data.Tables("Series").Rows.Count - 1
    '    'Dim NValues As Integer = Data.Tables("Series").Rows.Count

    '    'If ShowReverseCDF Then
    '    '    'For I = 0 To NIntervals
    '    '    For I = 1 To NValues
    '    '        'Data.Tables("Series").DefaultView.Item(I).Item("Probability") = 1 - (I / NIntervals)
    '    '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Probability") = 1 - (I / NValues)
    '    '    Next
    '    'Else
    '    '    'For I = 0 To NIntervals
    '    '    For I = 1 To NValues
    '    '        'Data.Tables("Series").DefaultView.Item(I).Item("Probability") = I / NIntervals
    '    '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Probability") = I / NValues
    '    '    Next
    '    'End If

    '    TableName = "Series" 'This updates the cmbTableName selection and updates the data displayed in the Data tab.
    '    '==================================================================================================================================================


    '    'Generate the Histogram data ----------------------------------------------------------------------------------------------------------------------

    '    Data.Tables.Add("Histogram") 'Create the Histogram table
    '    cmbTableName.Items.Add("Histogram") 'Add the table to the selection list

    '    Data.Tables("Histogram").Columns.Add("Mid_Interval", System.Type.GetType("System.Double")) 'Create the Mid Interval column
    '    Data.Tables("Histogram").Columns.Add("Count", System.Type.GetType("System.Int32"))         'Create the Count column
    '    Data.Tables("Histogram").Columns.Add("Probability", System.Type.GetType("System.Double"))  'Create the probability column

    '    'If IsDiscrete Then Else Data.Tables("Histogram").Columns.Add("Prob_Density", System.Type.GetType("System.Double"))  'Create the probability density column (Only for a continuous distribution)

    '    If IsDiscrete Then 'This is a discrete distribution
    '        Dim DistVal As Integer = MinSeriesVal 'The current distribution value
    '        Dim PointCount As Integer = 0 'The count of points having the current distribution value
    '        For Each Row As DataRowView In Data.Tables("Series").DefaultView 'This uses the sorted row view
    '            If Row.Item("Value") = DistVal Then
    '                PointCount += 1
    '            Else
    '                Data.Tables("Histogram").Rows.Add(DistVal, PointCount, PointCount / NRows) 'Write the Mid_Interval, Count,  and Probability to the table. (Discrete distributions do not have Prob_Density values.)
    '                PointCount = 0 'Reset the point count
    '                DistVal += 1 'Increment the distribution value
    '            End If
    '        Next
    '    Else 'This is a continuous distribution
    '        Data.Tables("Histogram").Columns.Add("Prob_Density", System.Type.GetType("System.Double"))  'Create the probability density column (Only for a continuous distribution)
    '        Dim StartGap As Double = HistMin - MinSeriesVal 'The gap between the first data value in the series and the midpoint of the first column in the histogram.
    '        Dim EndInterval As Double = HistMin + HistIntervalWidth / 2 'The maximum value of the current histogram interval
    '        Dim PointCount As Integer = 0 'The count of points within the current histogram interval
    '        For Each Row As DataRowView In Data.Tables("Series").DefaultView 'This uses the sorted row view
    '            If Row.Item("Value") < EndInterval Then
    '                PointCount += 1
    '            Else
    '                Data.Tables("Histogram").Rows.Add(EndInterval - HistIntervalWidth / 2, PointCount, PointCount / NRows, PointCount / NRows / HistIntervalWidth) 'Write the Mid_Interval, Count, Probability and Prob_Density to the table.
    '                PointCount = 0
    '                EndInterval += HistIntervalWidth
    '            End If
    '        Next
    '    End If

    '    Dim NHistRows As Integer = Data.Tables("Histogram").Rows.Count 'NHistRows is the number of Histogram rows.

    '    '==================================================================================================================================================

    '    If chkShowModel.Checked Then
    '        'Generate the data used to plot the Model CDF and PDF (or PMF): -------------------------------------------------------------------

    '        ''Generate the data used to plot the Model Cumulative Distribution Function: -------------------------------------------------------------------
    '        'Data.Tables("Series").Columns.Add("Model_Prob", System.Type.GetType("System.Double"))    'Create the Model Probability column

    '        'Data.Tables("Histogram").Columns.Add("Model_Prob", System.Type.GetType("System.Double"))    'Create the Model Probability column
    '        'If IsDiscrete Then Else Data.Tables("Histogram").Columns.Add("Model_Prob_Dens", System.Type.GetType("System.Double"))  'Create the Model probability density column (Only for a continuous distribution)

    '        If IsDiscrete Then
    '            Data.Tables("Histogram").Columns.Add("Model_CDF", System.Type.GetType("System.Double"))       'Create the Model CDF column              (for discrete distributions)
    '            Data.Tables("Histogram").Columns.Add("Model_Rev_CDF", System.Type.GetType("System.Double"))   'Create the Model Reverse CDF column      (for discrete distributions)
    '            Data.Tables("Histogram").Columns.Add("Model_Prob_Mass", System.Type.GetType("System.Double")) 'Create the Model Probability Mass column (for discrete distributions)
    '        Else
    '            Data.Tables("Series").Columns.Add("Model_CDF", System.Type.GetType("System.Double"))       'Create the Model CDF column                 (for continuous distributions)
    '            Data.Tables("Series").Columns.Add("Model_Rev_CDF", System.Type.GetType("System.Double"))   'Create the Model Reverse CDF column         (for continuous distributions)
    '            Data.Tables("Series").Columns.Add("Model_Prob_Dens", System.Type.GetType("System.Double")) 'Create the Model Probability Density column (for continuous distributions)
    '        End If

    '        Select Case DistributionName
    '            Case "C2 - Beta"
    '                'If ShowReverseCDF Then
    '                '    'For I = 0 To NIntervals
    '                '    For I = 1 To NValues
    '                '        'Data.Tables("Series").DefaultView.Item(I).Item("Model_Prob") = 1 - MathNet.Numerics.Distributions.Beta.CDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I).Item("Value"))
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - MathNet.Numerics.Distributions.Beta.CDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'Else
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = MathNet.Numerics.Distributions.Beta.CDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'End If
    '                For I = 1 To NValues
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.Beta.CDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                    'Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - MathNet.Numerics.Distributions.Beta.CDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF")
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.Beta.PDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                Next

    '            Case "C4 - Beta Scaled"
    '                'If ShowReverseCDF Then
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = 1 - MathNet.Numerics.Distributions.BetaScaled.CDF(ParamAValue, ParamBValue, ParamCValue, ParamDValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'Else
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = MathNet.Numerics.Distributions.BetaScaled.CDF(ParamAValue, ParamBValue, ParamCValue, ParamDValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'End If
    '                For I = 1 To NValues
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.BetaScaled.CDF(ParamAValue, ParamBValue, ParamCValue, ParamDValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF")
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.BetaScaled.PDF(ParamAValue, ParamBValue, ParamCValue, ParamDValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                Next

    '            Case "C2 - Cauchy"
    '                'If ShowReverseCDF Then
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = 1 - MathNet.Numerics.Distributions.Cauchy.CDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'Else
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = MathNet.Numerics.Distributions.Cauchy.CDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'End If
    '                For I = 1 To NValues
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.Cauchy.CDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF")
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.Cauchy.PDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                Next
    '            Case "C1 - Chi Squared"
    '                'If ShowReverseCDF Then
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = 1 - MathNet.Numerics.Distributions.ChiSquared.CDF(ParamAValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'Else
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = MathNet.Numerics.Distributions.ChiSquared.CDF(ParamAValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'End If
    '                For I = 1 To NValues
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.ChiSquared.CDF(ParamAValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF")
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.ChiSquared.PDF(ParamAValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                Next
    '            Case "C2 - Continuous Uniform"
    '                'If ShowReverseCDF Then
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = 1 - MathNet.Numerics.Distributions.ContinuousUniform.CDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'Else
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = MathNet.Numerics.Distributions.ContinuousUniform.CDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'End If
    '                For I = 1 To NValues
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.ContinuousUniform.CDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF")
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.ContinuousUniform.PDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                Next

    '            Case "C1 - Exponential"
    '                'If ShowReverseCDF Then
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = 1 - MathNet.Numerics.Distributions.Exponential.CDF(ParamAValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'Else
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = MathNet.Numerics.Distributions.Exponential.CDF(ParamAValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'End If
    '                For I = 1 To NValues
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.Exponential.CDF(ParamAValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF")
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.Exponential.PDF(ParamAValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                Next

    '            Case "C2 - Fisher-Snedecor"
    '                'If ShowReverseCDF Then
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = 1 - MathNet.Numerics.Distributions.FisherSnedecor.CDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'Else
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = MathNet.Numerics.Distributions.FisherSnedecor.CDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'End If
    '                For I = 1 To NValues
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.FisherSnedecor.CDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF")
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.FisherSnedecor.PDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                Next

    '            Case "C2 - Gamma"
    '                'If ShowReverseCDF Then
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = 1 - MathNet.Numerics.Distributions.Gamma.CDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'Else
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = MathNet.Numerics.Distributions.Gamma.CDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'End If
    '                For I = 1 To NValues
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.Gamma.CDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF")
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.Gamma.PDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                Next

    '            Case "C2 - Inverse Gaussian"
    '                'If ShowReverseCDF Then
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = 1 - MathNet.Numerics.Distributions.InverseGaussian.CDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'Else
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = MathNet.Numerics.Distributions.InverseGaussian.CDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'End If
    '                For I = 1 To NValues
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.InverseGaussian.CDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF")
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.InverseGaussian.PDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                Next

    '            Case "C2 - Log Normal"
    '                'If ShowReverseCDF Then
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = 1 - MathNet.Numerics.Distributions.LogNormal.CDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'Else
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = MathNet.Numerics.Distributions.LogNormal.CDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'End If
    '                For I = 1 To NValues
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.LogNormal.CDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF")
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.LogNormal.PDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                Next

    '            Case "C2 - Normal"
    '                'If ShowReverseCDF Then
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = 1 - MathNet.Numerics.Distributions.Normal.CDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'Else
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = MathNet.Numerics.Distributions.Normal.CDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'End If
    '                For I = 1 To NValues
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.Normal.CDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF")
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.Normal.PDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                Next

    '            Case "C2 - Pareto"
    '                'If ShowReverseCDF Then
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = 1 - MathNet.Numerics.Distributions.Pareto.CDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'Else
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = MathNet.Numerics.Distributions.Pareto.CDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'End If
    '                For I = 1 To NValues
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.Pareto.CDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF")
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.Pareto.PDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                Next

    '            Case "C1 - Rayleigh"
    '                'If ShowReverseCDF Then
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = 1 - MathNet.Numerics.Distributions.Rayleigh.CDF(ParamAValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'Else
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = MathNet.Numerics.Distributions.Rayleigh.CDF(ParamAValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'End If
    '                For I = 1 To NValues
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.Rayleigh.CDF(ParamAValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF")
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.Rayleigh.PDF(ParamAValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                Next

    '            Case "C4 - Skewed Generalized Error"
    '                'If ShowReverseCDF Then
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = 1 - MathNet.Numerics.Distributions.SkewedGeneralizedError.CDF(ParamAValue, ParamBValue, ParamCValue, ParamDValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'Else
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = MathNet.Numerics.Distributions.SkewedGeneralizedError.CDF(ParamAValue, ParamBValue, ParamCValue, ParamDValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'End If
    '                For I = 1 To NValues
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.SkewedGeneralizedError.CDF(ParamAValue, ParamBValue, ParamCValue, ParamDValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF")
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.SkewedGeneralizedError.PDF(ParamAValue, ParamBValue, ParamCValue, ParamDValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                Next

    '            Case "C5 - Skewed Generalized T"
    '                'If ShowReverseCDF Then
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = 1 - MathNet.Numerics.Distributions.SkewedGeneralizedT.CDF(ParamAValue, ParamBValue, ParamCValue, ParamDValue, ParamEValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'Else
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = MathNet.Numerics.Distributions.SkewedGeneralizedT.CDF(ParamAValue, ParamBValue, ParamCValue, ParamDValue, ParamEValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'End If
    '                For I = 1 To NValues
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.SkewedGeneralizedT.CDF(ParamAValue, ParamBValue, ParamCValue, ParamDValue, ParamEValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF")
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.SkewedGeneralizedT.PDF(ParamAValue, ParamBValue, ParamCValue, ParamDValue, ParamEValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                Next

    '            Case "C3 - Student's T"
    '                'If ShowReverseCDF Then
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = 1 - MathNet.Numerics.Distributions.StudentT.CDF(ParamAValue, ParamBValue, ParamCValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'Else
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = MathNet.Numerics.Distributions.StudentT.CDF(ParamAValue, ParamBValue, ParamCValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'End If
    '                For I = 1 To NValues
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.StudentT.CDF(ParamAValue, ParamBValue, ParamCValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF")
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.StudentT.PDF(ParamAValue, ParamBValue, ParamCValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                Next

    '            Case "C3 - Triangular"
    '                'If ShowReverseCDF Then
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = 1 - MathNet.Numerics.Distributions.Triangular.CDF(ParamAValue, ParamBValue, ParamCValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'Else
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = MathNet.Numerics.Distributions.Triangular.CDF(ParamAValue, ParamBValue, ParamCValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'End If
    '                For I = 1 To NValues
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.Triangular.CDF(ParamAValue, ParamBValue, ParamCValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF")
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.Triangular.PDF(ParamAValue, ParamBValue, ParamCValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                Next

    '            Case "C3 - Truncated Pareto"
    '                'If ShowReverseCDF Then
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = 1 - MathNet.Numerics.Distributions.TruncatedPareto.CDF(ParamAValue, ParamBValue, ParamCValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'Else
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = MathNet.Numerics.Distributions.TruncatedPareto.CDF(ParamAValue, ParamBValue, ParamCValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'End If
    '                For I = 1 To NValues
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.TruncatedPareto.CDF(ParamAValue, ParamBValue, ParamCValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF")
    '                    Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.TruncatedPareto.PDF(ParamAValue, ParamBValue, ParamCValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                Next

    '            Case "D1 - Bernoulli"
    '                'If ShowReverseCDF Then
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = 1 - MathNet.Numerics.Distributions.Bernoulli.CDF(ParamAValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'Else
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = MathNet.Numerics.Distributions.Bernoulli.CDF(ParamAValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'End If
    '                For I = 1 To NHistRows
    '                    Data.Tables("Histogram").Rows(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.Bernoulli.CDF(ParamAValue, Data.Tables("Histogram").Rows(I - 1).Item("Mid_Interval"))
    '                    Data.Tables("Histogram").Rows(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Histogram").Rows(I - 1).Item("Model_CDF")
    '                    Data.Tables("Histogram").Rows(I - 1).Item("Model_Prob_Mass") = MathNet.Numerics.Distributions.Bernoulli.PMF(ParamAValue, Data.Tables("Histogram").Rows(I - 1).Item("Mid_Interval"))
    '                Next
    '            Case "D2 - Binomial"
    '                'If ShowReverseCDF Then
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = 1 - MathNet.Numerics.Distributions.Binomial.CDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'Else
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = MathNet.Numerics.Distributions.Binomial.CDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'End If
    '                For I = 1 To NHistRows
    '                    Data.Tables("Histogram").Rows(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.Binomial.CDF(ParamAValue, ParamBValue, Data.Tables("Histogram").Rows(I - 1).Item("Mid_Interval"))
    '                    Data.Tables("Histogram").Rows(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Histogram").Rows(I - 1).Item("Model_CDF")
    '                    Data.Tables("Histogram").Rows(I - 1).Item("Model_Prob_Mass") = MathNet.Numerics.Distributions.Binomial.PMF(ParamAValue, ParamBValue, Data.Tables("Histogram").Rows(I - 1).Item("Mid_Interval"))
    '                Next

    '            Case "D1 - Categorical"
    '                'If ShowReverseCDF Then
    '                '    For I = 0 To NIntervals
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = 1 - MathNet.Numerics.Distributions.Categorical.CDF(ParamAValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'Else
    '                '    For I = 0 To NIntervals
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = MathNet.Numerics.Distributions.Categorical.CDF(ParamAValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'End If

    '            Case "D2 - Conway-Maxwell-Poisson"
    '                'If ShowReverseCDF Then
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = 1 - MathNet.Numerics.Distributions.ConwayMaxwellPoisson.CDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'Else
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = MathNet.Numerics.Distributions.ConwayMaxwellPoisson.CDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'End If
    '                For I = 1 To NHistRows
    '                    Data.Tables("Histogram").Rows(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.ConwayMaxwellPoisson.CDF(ParamAValue, ParamBValue, Data.Tables("Histogram").Rows(I - 1).Item("Mid_Interval"))
    '                    Data.Tables("Histogram").Rows(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Histogram").Rows(I - 1).Item("Model_CDF")
    '                    Data.Tables("Histogram").Rows(I - 1).Item("Model_Prob_Mass") = MathNet.Numerics.Distributions.ConwayMaxwellPoisson.PMF(ParamAValue, ParamBValue, Data.Tables("Histogram").Rows(I - 1).Item("Mid_Interval"))
    '                Next

    '            Case "D2 - Discrete Uniform"
    '                'If ShowReverseCDF Then
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = 1 - MathNet.Numerics.Distributions.DiscreteUniform.CDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'Else
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = MathNet.Numerics.Distributions.DiscreteUniform.CDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'End If
    '                For I = 1 To NHistRows
    '                    Data.Tables("Histogram").Rows(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.DiscreteUniform.CDF(ParamAValue, ParamBValue, Data.Tables("Histogram").Rows(I - 1).Item("Mid_Interval"))
    '                    Data.Tables("Histogram").Rows(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Histogram").Rows(I - 1).Item("Model_CDF")
    '                    Data.Tables("Histogram").Rows(I - 1).Item("Model_Prob_Mass") = MathNet.Numerics.Distributions.DiscreteUniform.PMF(ParamAValue, ParamBValue, Data.Tables("Histogram").Rows(I - 1).Item("Mid_Interval"))
    '                Next

    '            Case "D1 - Geometric"
    '                'If ShowReverseCDF Then
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = 1 - MathNet.Numerics.Distributions.Geometric.CDF(ParamAValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'Else
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = MathNet.Numerics.Distributions.Geometric.CDF(ParamAValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'End If
    '                For I = 1 To NHistRows
    '                    Data.Tables("Histogram").Rows(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.Geometric.CDF(ParamAValue, Data.Tables("Histogram").Rows(I - 1).Item("Mid_Interval"))
    '                    Data.Tables("Histogram").Rows(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Histogram").Rows(I - 1).Item("Model_CDF")
    '                    Data.Tables("Histogram").Rows(I - 1).Item("Model_Prob_Mass") = MathNet.Numerics.Distributions.Geometric.PMF(ParamAValue, Data.Tables("Histogram").Rows(I - 1).Item("Mid_Interval"))
    '                Next

    '            Case "D3 - Hypergeometric"
    '                'If ShowReverseCDF Then
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = 1 - MathNet.Numerics.Distributions.Hypergeometric.CDF(ParamAValue, ParamBValue, ParamCValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'Else
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = MathNet.Numerics.Distributions.Hypergeometric.CDF(ParamAValue, ParamBValue, ParamCValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'End If
    '                For I = 1 To NHistRows
    '                    Data.Tables("Histogram").Rows(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.Hypergeometric.CDF(ParamAValue, ParamBValue, ParamCValue, Data.Tables("Histogram").Rows(I - 1).Item("Mid_Interval"))
    '                    Data.Tables("Histogram").Rows(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Histogram").Rows(I - 1).Item("Model_CDF")
    '                    Data.Tables("Histogram").Rows(I - 1).Item("Model_Prob_Mass") = MathNet.Numerics.Distributions.Hypergeometric.PMF(ParamAValue, ParamBValue, ParamCValue, Data.Tables("Histogram").Rows(I - 1).Item("Mid_Interval"))
    '                Next

    '            Case "D2 - Negative Binomial"
    '                'If ShowReverseCDF Then
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = 1 - MathNet.Numerics.Distributions.NegativeBinomial.CDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'Else
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = MathNet.Numerics.Distributions.NegativeBinomial.CDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'End If
    '                For I = 1 To NHistRows
    '                    Data.Tables("Histogram").Rows(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.NegativeBinomial.CDF(ParamAValue, ParamBValue, Data.Tables("Histogram").Rows(I - 1).Item("Mid_Interval"))
    '                    Data.Tables("Histogram").Rows(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Histogram").Rows(I - 1).Item("Model_CDF")
    '                    Data.Tables("Histogram").Rows(I - 1).Item("Model_Prob_Mass") = MathNet.Numerics.Distributions.NegativeBinomial.PMF(ParamAValue, ParamBValue, Data.Tables("Histogram").Rows(I - 1).Item("Mid_Interval"))
    '                Next

    '            Case "D1 Poisson"
    '                'If ShowReverseCDF Then
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = 1 - MathNet.Numerics.Distributions.Poisson.CDF(ParamAValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'Else
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = MathNet.Numerics.Distributions.Poisson.CDF(ParamAValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'End If
    '                For I = 1 To NHistRows
    '                    Data.Tables("Histogram").Rows(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.Poisson.CDF(ParamAValue, Data.Tables("Histogram").Rows(I - 1).Item("Mid_Interval"))
    '                    Data.Tables("Histogram").Rows(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Histogram").Rows(I - 1).Item("Model_CDF")
    '                    Data.Tables("Histogram").Rows(I - 1).Item("Model_Prob_Mass") = MathNet.Numerics.Distributions.Poisson.PMF(ParamAValue, Data.Tables("Histogram").Rows(I - 1).Item("Mid_Interval"))
    '                Next

    '            Case "D2 - Zipf"
    '                'If ShowReverseCDF Then
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = 1 - MathNet.Numerics.Distributions.Zipf.CDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'Else
    '                '    For I = 1 To NValues
    '                '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob") = MathNet.Numerics.Distributions.Zipf.CDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
    '                '    Next
    '                'End If
    '                For I = 1 To NHistRows
    '                    Data.Tables("Histogram").Rows(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.Zipf.CDF(ParamAValue, ParamBValue, Data.Tables("Histogram").Rows(I - 1).Item("Mid_Interval"))
    '                    Data.Tables("Histogram").Rows(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Histogram").Rows(I - 1).Item("Model_CDF")
    '                    Data.Tables("Histogram").Rows(I - 1).Item("Model_Prob_Mass") = MathNet.Numerics.Distributions.Zipf.PMF(ParamAValue, ParamBValue, Data.Tables("Histogram").Rows(I - 1).Item("Mid_Interval"))
    '                Next

    '            Case ""
    '                'No model

    '            Case Else
    '                Main.Message.AddWarning("Unknown model distribution: " & DistributionName & vbCrLf)

    '        End Select



    '        '==============================================================================================================================================
    '    End If




    '    'If chkShowModel.Checked Then
    '    '    'Generate the data used to plot the Model Histogram: -------------------------------------------------------------------
    '    '    Data.Tables("Histogram").Columns.Add("Model_Prob", System.Type.GetType("System.Double"))    'Create the Model Probability column
    '    '    If IsDiscrete Then Else Data.Tables("Histogram").Columns.Add("Model_Prob_Dens", System.Type.GetType("System.Double"))  'Create the Model probability density column (Only for a continuous distribution)


    '    '    '==============================================================================================================================================
    '    'End If



    'End Sub

    ''OLD CODE: [Replaced by CalcChartData()]
    'Private Sub SetUpSeriesTable()
    '    'Set up the Series table used to plot the CDF.

    '    'The Series table will contain the columns:
    '    'Data - the data points copied from the data source then sorted (ascending)
    '    'Probability - the cumulative probability of each data point - smallest point has 0 probability, the largest point has 1 probability.

    '    Data.Clear()
    '    Data.Reset()
    '    cmbTableName.Items.Clear()

    '    Data.Tables.Add("Series") 'Create the Series table.
    '    cmbTableName.Items.Add("Series")

    '    Data.Tables("Series").Columns.Add("Value", DataSource.Data.Tables(SourceTableName).Columns(SourceColumnName).DataType) 'Create the Value column
    '    'Copy the data from the DataSource to the Series table:

    '    For Each Row As DataRow In DataSource.Data.Tables(SourceTableName).Rows
    '        Data.Tables("Series").Rows.Add(Row.Item(SourceColumnName))
    '        'I += 1
    '    Next

    '    Data.Tables("Series").DefaultView.Sort = "Value ASC"     'Sort the data in ascending order

    '    'Create the Probability column:
    '    Data.Tables("Series").Columns.Add("Probability", System.Type.GetType("System.Double"))

    '    'Dim NIntervals As Integer = Data.Tables("Series").Rows.Count - 1
    '    Dim NValues As Integer = Data.Tables("Series").Rows.Count

    '    'For I = 0 To NIntervals
    '    '    Data.Tables("Series").DefaultView.Item(I).Item("Probability") = I / NIntervals
    '    'Next

    '    'UPDATE to allow for Reverse CDF:
    '    If ShowReverseCDF Then
    '        'For I = 0 To NIntervals
    '        For I = 1 To NValues
    '            'Data.Tables("Series").DefaultView.Item(I).Item("Probability") = 1 - (I / NIntervals)
    '            Data.Tables("Series").DefaultView.Item(I - 1).Item("Probability") = 1 - (I - 1 / NValues)
    '        Next
    '    Else
    '        'For I = 0 To NIntervals
    '        For I = 1 To NValues
    '            'Data.Tables("Series").DefaultView.Item(I).Item("Probability") = I / NIntervals
    '            Data.Tables("Series").DefaultView.Item(I - 1).Item("Probability") = I - 1 / NValues
    '        Next
    '    End If

    '    TableName = "Series" 'This updates the cmbTableName selection and updates the data displayed in the Data tab.

    'End Sub

    Private Sub RecalcSeriesTable()
        'Recalculate the Series table used to plot the CDF.

        'Dim NIntervals As Integer = Data.Tables("Series").Rows.Count - 1
        'Dim NIntervals As Integer = Main.Distribution.Data.Tables("Model_Fitting").Rows.Count - 1
        Dim NIntervals As Integer = CalcData.Tables("Series").Rows.Count - 1

        'For I = 0 To NIntervals
        '    Data.Tables("Series").DefaultView.Item(I).Item("Probability") = I / NIntervals
        'Next

        'UPDATE to allow for Reverse CDF:
        If ShowReverseCDF Then
            For I = 0 To NIntervals
                'Data.Tables("Series").DefaultView.Item(I).Item("Probability") = 1 - (I / NIntervals)
                'Main.Distribution.Data.Tables("Model_Fitting").DefaultView.Item(I).Item("Probability") = 1 - (I / NIntervals)
                CalcData.Tables("Series").DefaultView.Item(I).Item("Probability") = 1 - (I / NIntervals)
            Next
        Else
            For I = 0 To NIntervals
                'Data.Tables("Series").DefaultView.Item(I).Item("Probability") = I / NIntervals
                'Main.Distribution.Data.Tables("Model_Fitting").DefaultView.Item(I).Item("Probability") = I / NIntervals
                CalcData.Tables("Series").DefaultView.Item(I).Item("Probability") = I / NIntervals
            Next
        End If

    End Sub


    Private Sub PlotCharts()
        'Plot the CDF and PDF charts

        Try
            'Plot the CDF of the Series Data:

            Chart1.Legends(0).Docking = DataVisualization.Charting.Docking.Bottom

            Chart1.Series(0).ChartType = DataVisualization.Charting.SeriesChartType.Line
            Chart1.Series(0).Color = Color.Red
            Chart1.Series(0).BorderWidth = 3

            'Dim DataInfoIndex As Integer = Main.MonteCarlo.DataInfoNameIndex(SourceColumnName)
            'If DataInfoIndex = -1 Then 'This is a calculated value so there is no information about this in MonteCarlo.DataInfo
            '    If ScalarName.Trim = "" Then
            '        Chart1.ChartAreas(0).AxisX.Title = SourceColumnName
            '    Else
            '        If Main.CalcInfo.ContainsKey(ScalarName) Then
            '            Chart1.ChartAreas(0).AxisX.Title = ScalarName & " (" & Main.CalcInfo(ScalarName).UnitsAbbrev & ")" 'Show the CalcInfo abbreviated units if available
            '        Else
            '            Chart1.ChartAreas(0).AxisX.Title = ScalarName
            '        End If
            '    End If
            'Else

            '    If ScalarName.Trim = "" Then
            '        Chart1.ChartAreas(0).AxisX.Title = SourceColumnName & " (" & Main.MonteCarlo.DataInfo(DataInfoIndex).Units & ")"
            '    Else
            '        If Main.CalcInfo.ContainsKey(ScalarName) Then
            '            If Main.CalcInfo(ScalarName).UnitsAbbrev.Trim = "" Then
            '                Chart1.ChartAreas(0).AxisX.Title = ScalarName & " (" & Main.MonteCarlo.DataInfo(DataInfoIndex).Units & ")"
            '            Else
            '                Chart1.ChartAreas(0).AxisX.Title = ScalarName & " (" & Main.CalcInfo(ScalarName).UnitsAbbrev & ")"
            '            End If

            '        Else
            '            Chart1.ChartAreas(0).AxisX.Title = ScalarName & " (" & Main.MonteCarlo.DataInfo(DataInfoIndex).Units & ")"
            '        End If
            '    End If
            'End If

            Chart1.ChartAreas(0).AxisX.TitleFont = New Font("Arial", 12, FontStyle.Regular Or FontStyle.Bold)
            'Chart1.ChartAreas(0).AxisX.LabelStyle.Format = "#.##"

            Chart1.ChartAreas(0).AxisY.Title = "Cumulative" & vbCrLf & "Probability"
            Chart1.ChartAreas(0).AxisY.TitleFont = New Font("Arial", 12, FontStyle.Regular Or FontStyle.Bold)

            Chart1.ChartAreas(0).AxisY.Minimum = 0
            Chart1.ChartAreas(0).AxisY.Maximum = 1

            'Calculate the XAxis Scale settings:
            Dim XAxisLength As Single = Chart1.ChartAreas(0).InnerPlotPosition.Width 'X Axis length in relative coordinates, which range from 0 to 100.
            If XAxisLength = 0 Then XAxisLength = 100 'When the form is first loaded, XAxisLength is 0!
            Dim ChartPixelWidth As Integer = Chart1.Width 'The width of the entire chart image in pixels.
            Dim XAxisPixelLength As Integer = Int(ChartPixelWidth * XAxisLength / 100)
            Dim XRawNIntervals As Integer = Int(XAxisPixelLength / RawPixelsPerInterval) 'The Raw number of axis annotation intervals based on the Raw pixels per annotation.

            Dim XRawInterval As Double = (MaxSeriesVal - MinSeriesVal) / XRawNIntervals 'First calculate the Raw Interval
            ChartXInterval = PreferredInterval(XRawInterval) 'The preferred X Axis lavel interval has the significant digits: 1, 2, 2.5, 5, 10.
            ChartXMin = Math.Floor(MinSeriesVal / ChartXInterval) * ChartXInterval 'The preferred Axis Minimum for the Chart display
            ChartXMax = Math.Ceiling(MaxSeriesVal / ChartXInterval) * ChartXInterval 'The preferred Axis Maximum for the Chart display
            '------------------------------------------

            Chart1.ChartAreas(0).AxisX.Minimum = ChartXMin
            Chart1.ChartAreas(0).AxisX.Maximum = ChartXMax
            Chart1.ChartAreas(0).AxisX.Interval = ChartXInterval

            Chart1.Titles.Clear()
            Chart1.Titles.Add("Title1")
            Chart1.Titles(0).Text = "Random Variable: " & DataColumnName
            Chart1.Titles(0).Font = New Font("Arial", 16, FontStyle.Regular Or FontStyle.Bold)

            If ShowReverseCDF Then
                Chart1.Series(0).Name = "Reverse Cumulative Distribution Function"
                'Chart1.Series("Reverse Cumulative Distribution Function").Points.DataBindXY(Data.Tables("Series").DefaultView, "Value", Data.Tables("Series").DefaultView, "Reverse_CDF")
                'Chart1.Series("Reverse Cumulative Distribution Function").Points.DataBindXY(Main.Distribution.Data.Tables("Model_Fitting").DefaultView, "Value", Main.Distribution.Data.Tables("Model_Fitting").DefaultView, "Reverse_eCDF") 'Reverse_eCDF is the Empirical Reverse CDF data.
                Chart1.Series("Reverse Cumulative Distribution Function").Points.DataBindXY(CalcData.Tables("Series").DefaultView, "Value", CalcData.Tables("Series").DefaultView, "Reverse_eCDF") 'Reverse_eCDF is the Empirical Reverse CDF data.
                Chart1.Titles.Add("Title2")
                Chart1.Titles(1).Text = "Reverse Cumulative Distribution Function"
            Else
                Chart1.Series(0).Name = "Cumulative Distribution Function"
                'Chart1.Series("Cumulative Distribution Function").Points.DataBindXY(Data.Tables("Series").DefaultView, "Value", Data.Tables("Series").DefaultView, "CDF")
                'Chart1.Series("Cumulative Distribution Function").Points.DataBindXY(Main.Distribution.Data.Tables("Model_Fitting").DefaultView, "Value", Main.Distribution.Data.Tables("Model_Fitting").DefaultView, "eCDF") 'eCDF is the Empirical CDF data
                Chart1.Series("Cumulative Distribution Function").Points.DataBindXY(CalcData.Tables("Series").DefaultView, "Value", CalcData.Tables("Series").DefaultView, "eCDF") 'eCDF is the Empirical CDF data
                Chart1.Titles.Add("Title2")
                Chart1.Titles(1).Text = "Cumulative Distribution Function"
            End If

            Chart1.Titles(1).Font = New Font("Arial", 14, FontStyle.Regular Or FontStyle.Bold)
            Chart1.Titles(1).DockedToChartArea = Chart1.ChartAreas(0).Name
            Chart1.Titles(1).IsDockedInsideChartArea = False
            Chart1.Titles(1).Docking = DataVisualization.Charting.Docking.Top

        Catch ex As Exception
            Main.Message.AddWarning("Error plotting the Series Data CDF: " & vbCrLf & ex.Message & vbCrLf)
        End Try

        Try
            'Plot the PDF Histogram (Continuous distribution) or PMF (Discrete distribution):
            If Chart1.Series.Count > 1 Then
                If Chart1.Series(1).Name = "PDF Histogram" Then

                Else
                    Chart1.Series(1).Name = "PDF Histogram"
                End If
            Else
                Chart1.Series.Add("PDF Histogram")
                Chart1.ChartAreas.Add("HistArea")
            End If

            Chart1.Series("PDF Histogram").ChartType = DataVisualization.Charting.SeriesChartType.Column
            Chart1.Series("PDF Histogram").Color = Color.Blue
            Chart1.Series("PDF Histogram").ChartArea = "HistArea"

            Chart1.Titles.Add("Title3")


            If IsDiscrete Then
                'Chart1.Series("PDF Histogram").Points.DataBindXY(Data.Tables("Histogram").DefaultView, "Mid_Interval", Data.Tables("Histogram").DefaultView, "Prob_Mass")
                'Chart1.Series("PDF Histogram").Points.DataBindXY(Main.Distribution.Data.Tables("Histogram").DefaultView, "Mid_Interval", Main.Distribution.Data.Tables("Histogram").DefaultView, "Prob_Mass")
                'Chart1.Series("PDF Histogram").Points.DataBindXY(CalcData.Tables("Histogram").DefaultView, "Mid_Interval", CalcData.Tables("Histogram").DefaultView, "Prob_Mass")
                Chart1.Series("PDF Histogram").Points.DataBindXY(CalcData.Tables("Histogram").DefaultView, "Value", CalcData.Tables("Histogram").DefaultView, "Prob_Mass") 'UPDATE 15May22 - Use Value instead of Mid_Interval ????
                Chart1.Titles(2).Text = "Probability Mass"
                Chart1.ChartAreas("HistArea").AxisY.Title = "Probability Mass"
            Else
                'Chart1.Series("PDF Histogram").Points.DataBindXY(Data.Tables("Histogram").DefaultView, "Mid_Interval", Data.Tables("Histogram").DefaultView, "Prob_Density")
                'Chart1.Series("PDF Histogram").Points.DataBindXY(Main.Distribution.Data.Tables("Histogram").DefaultView, "Mid_Interval", Main.Distribution.Data.Tables("Histogram").DefaultView, "Prob_Density")
                'Chart1.Series("PDF Histogram").Points.DataBindXY(CalcData.Tables("Histogram").DefaultView, "Mid_Interval", CalcData.Tables("Histogram").DefaultView, "Prob_Density")
                Chart1.Series("PDF Histogram").Points.DataBindXY(CalcData.Tables("Histogram").DefaultView, "Value", CalcData.Tables("Histogram").DefaultView, "Prob_Density") 'UPDATE 15May22 - Use Value instead of Mid_Interval ????
                Chart1.Titles(2).Text = "Probability Density Histogram"
                Chart1.ChartAreas("HistArea").AxisY.Title = "Probability Density"
            End If
            Chart1.Titles(2).Font = New Font("Arial", 14, FontStyle.Regular Or FontStyle.Bold)
            Chart1.Titles(2).DockedToChartArea = "HistArea"
            Chart1.Titles(2).IsDockedInsideChartArea = False
            Chart1.Titles(2).Docking = DataVisualization.Charting.Docking.Top

            Chart1.ChartAreas("HistArea").AxisX.Minimum = ChartXMin
            Chart1.ChartAreas("HistArea").AxisX.Maximum = ChartXMax
            Chart1.ChartAreas("HistArea").AxisX.Interval = ChartXInterval

            'Dim DataInfoIndex As Integer = Main.MonteCarlo.DataInfoNameIndex(SourceColumnName)
            'If DataInfoIndex = -1 Then  'This is a calculated value so there is no information about this in MonteCarlo.DataInfo
            '    If ScalarName.Trim = "" Then
            '        Chart1.ChartAreas("HistArea").AxisX.Title = SourceColumnName
            '    ElseIf Main.CalcInfo.ContainsKey(ScalarName) Then
            '        Chart1.ChartAreas("HistArea").AxisX.Title = ScalarName & " (" & Main.CalcInfo(ScalarName).UnitsAbbrev & ")" 'Show the CalcInfo abbreviated units if available
            '    Else
            '        Chart1.ChartAreas("HistArea").AxisX.Title = ScalarName
            '    End If
            'Else
            '    If ScalarName.Trim = "" Then
            '        Chart1.ChartAreas("HistArea").AxisX.Title = SourceColumnName & " (" & Main.MonteCarlo.DataInfo(DataInfoIndex).Units & ")"
            '    Else
            '        If Main.CalcInfo.ContainsKey(ScalarName) Then
            '            If Main.CalcInfo(ScalarName).UnitsAbbrev.Trim = "" Then
            '                Chart1.ChartAreas("HistArea").AxisX.Title = ScalarName & " (" & Main.MonteCarlo.DataInfo(DataInfoIndex).Units & ")"
            '            Else
            '                Chart1.ChartAreas("HistArea").AxisX.Title = ScalarName & " (" & Main.CalcInfo(ScalarName).UnitsAbbrev & ")"
            '            End If
            '        Else
            '            Chart1.ChartAreas("HistArea").AxisX.Title = ScalarName & " (" & Main.MonteCarlo.DataInfo(DataInfoIndex).Units & ")"
            '        End If
            '    End If
            'End If

            Chart1.ChartAreas("HistArea").AxisX.TitleFont = New Font("Arial", 12, FontStyle.Regular Or FontStyle.Bold)
            Chart1.ChartAreas("HistArea").AxisY.TitleFont = New Font("Arial", 12, FontStyle.Regular Or FontStyle.Bold)

            Chart1.ChartAreas(0).AlignWithChartArea = "HistArea"
            Chart1.ChartAreas(0).AlignmentOrientation = DataVisualization.Charting.AreaAlignmentOrientations.Vertical
            Chart1.ChartAreas(0).AlignmentStyle = DataVisualization.Charting.AreaAlignmentStyles.All

            Chart1.ChartAreas(0).AxisX.RoundAxisValues()

        Catch ex As Exception
            Main.Message.AddWarning("Error plotting the Series Data PDF or PMF: " & vbCrLf & ex.Message & vbCrLf)
        End Try


        'Add a series used to plot vertical bars on the CDF chart:
        Dim IndexNo As Integer = Chart1.Series.IndexOf("CdfVertBar")
        If IndexNo = -1 Then 'Series named CdfVerBar does not exist
            Chart1.Series.Add("CdfVertBar")
            Chart1.Series("CdfVertBar").ChartType = DataVisualization.Charting.SeriesChartType.Column
            Chart1.Series("CdfVertBar").Color = Color.Orange
            Chart1.Series("CdfVertBar").ChartArea = Chart1.ChartAreas(0).Name
            Chart1.Series("CdfVertBar").SetCustomProperty("PixelPointWidth", "2")
            Chart1.Series("CdfVertBar").IsVisibleInLegend = False
        Else

        End If

        'Add a series used to plot circle markers on the histogram:
        IndexNo = Chart1.Series.IndexOf("HistPoints")
        If IndexNo = -1 Then  'Series named HistPoints does not exist
            Chart1.Series.Add("HistPoints")
            Chart1.Series("HistPoints").ChartType = DataVisualization.Charting.SeriesChartType.Point
            Chart1.Series("HistPoints").Color = Color.Transparent
            Chart1.Series("HistPoints").ChartArea = "HistArea"
            Chart1.Series("HistPoints").IsVisibleInLegend = False
            Chart1.Series("HistPoints").MarkerStyle = DataVisualization.Charting.MarkerStyle.Circle
            Chart1.Series("HistPoints").MarkerSize = 20
            Chart1.Series("HistPoints").MarkerBorderWidth = 2
            Chart1.Series("HistPoints").MarkerBorderColor = Color.Orange
        Else

        End If

        If chkShowModel.Checked Then 'Plot the CDF and PDF (or PMF) of the Model distribution
            If CalcData.Tables.Contains("ModelData") Then
                If IsDiscrete Then 'Discrete distribution model

                Else 'Continuous distribution model

                    If ShowReverseCDF Then
                        If Chart1.Series.Count > 4 Then
                            If Chart1.Series(4).Name = "Model Reverse CDF" Then
                            Else
                                Chart1.Series(4).Name = "Model Reverse CDF"
                            End If
                        Else
                            Chart1.Series.Add("Model Reverse CDF")
                        End If

                        Chart1.Series("Model Reverse CDF").ChartArea = Chart1.ChartAreas(0).Name
                        Chart1.Series("Model Reverse CDF").ChartType = DataVisualization.Charting.SeriesChartType.Line
                        Chart1.Series("Model Reverse CDF").Color = Color.Black
                        Chart1.Series("Model Reverse CDF").BorderWidth = 1
                        Chart1.Series("Model Reverse CDF").BorderDashStyle = DataVisualization.Charting.ChartDashStyle.Dot


                        'Chart1.Series("Model Reverse CDF").Points.DataBindXY(CalcData.Tables("Series").DefaultView, "Value", CalcData.Tables("Series").DefaultView, "Model_Rev_CDF")
                        Chart1.Series("Model CDF").Points.DataBindXY(CalcData.Tables("ModelData").DefaultView, "Value", CalcData.Tables("ModelData").DefaultView, "Reverse_CDF")
                        'Chart1.Series("Model CDF").IsVisibleInLegend = False
                    Else
                        'If Chart1.Series.Count > 2 Then
                        If Chart1.Series.Count > 4 Then
                            If Chart1.Series(4).Name = "Model CDF" Then
                            Else
                                Chart1.Series(4).Name = "Model CDF"
                            End If
                        Else
                            Chart1.Series.Add("Model CDF")
                        End If

                        Chart1.Series("Model CDF").ChartArea = Chart1.ChartAreas(0).Name

                        Chart1.Series("Model CDF").ChartType = DataVisualization.Charting.SeriesChartType.Line
                        Chart1.Series("Model CDF").Color = Color.Black
                        Chart1.Series("Model CDF").BorderWidth = 1
                        Chart1.Series("Model CDF").BorderDashStyle = DataVisualization.Charting.ChartDashStyle.Dot


                        'Chart1.Series("Model CDF").Points.DataBindXY(CalcData.Tables("Series").DefaultView, "Value", CalcData.Tables("Series").DefaultView, "Model_CDF")
                        Chart1.Series("Model CDF").Points.DataBindXY(CalcData.Tables("ModelData").DefaultView, "Value", CalcData.Tables("ModelData").DefaultView, "CDF")
                        'Chart1.Series("Model CDF").IsVisibleInLegend = False
                    End If

                    If Chart1.Series.Count > 5 Then
                        If Chart1.Series(5).Name = "Model PDF" Then
                        Else
                            Chart1.Series(5).Name = "Model PDF"
                        End If
                    Else
                        Chart1.Series.Add("Model PDF")
                    End If

                    Chart1.Series("Model PDF").ChartArea = "HistArea"

                    Chart1.Series("Model PDF").ChartType = DataVisualization.Charting.SeriesChartType.Line
                    Chart1.Series("Model PDF").Color = Color.Black
                    Chart1.Series("Model PDF").BorderWidth = 2
                    'Chart1.Series("Model PDF").BorderDashStyle = DataVisualization.Charting.ChartDashStyle.Dash


                    'Chart1.Series("Model PDF").Points.DataBindXY(CalcData.Tables("Series").DefaultView, "Value", CalcData.Tables("Series").DefaultView, "Model_Prob_Dens")
                    Chart1.Series("Model PDF").Points.DataBindXY(CalcData.Tables("ModelData").DefaultView, "Value", CalcData.Tables("ModelData").DefaultView, "Prob_Density")
                    'Chart1.Series("Model PDF").IsVisibleInLegend = False

                End If
            Else
                Main.Message.AddWarning("The ModelData table is missing." & vbCrLf)
            End If
        Else
            'Remove any Model series:
            Dim SeriesNo As Integer = Chart1.Series.IndexOf("Model CDF")
            If SeriesNo = -1 Then
                'The Model CDF series does not exist
            Else
                Chart1.Series.RemoveAt(SeriesNo)
            End If

            SeriesNo = Chart1.Series.IndexOf("Model Reverse CDF")
            If SeriesNo = -1 Then
                'The Model Reverse CDF series does not exist
            Else
                Chart1.Series.RemoveAt(SeriesNo)
            End If

            SeriesNo = Chart1.Series.IndexOf("Model PDF")
            If SeriesNo = -1 Then
                'The Model PDF series does not exist
            Else
                Chart1.Series.RemoveAt(SeriesNo)
            End If

        End If

        ApplyChartMinMax()

        Dim NTitles As Integer = Chart1.Titles.Count
        If NTitles > 0 Then
            NumericUpDown1.Minimum = 1
            NumericUpDown1.Maximum = NTitles
            NumericUpDown1.Increment = 1
            NumericUpDown1.Value = 1
            txtEditTitle.Text = Chart1.Titles(0).Text
            txtEditTitle.Font = Chart1.Titles(0).Font
            txtEditTitle.ForeColor = Chart1.Titles(0).ForeColor
        Else
            NumericUpDown1.Minimum = 0
            NumericUpDown1.Maximum = 0
            NumericUpDown1.Increment = 1
            NumericUpDown1.Value = 0
            txtEditTitle.Text = ""
        End If

    End Sub

    ''OLD CODE: [Replaced by PlotCharts()]
    'Private Sub PlotCDF()
    '    'Plot the Cumulative Density Function on the chart.

    '    Try
    '        If ShowReverseCDF Then
    '            Chart1.Series(0).Name = "Reverse Cumulative Distribution Function"
    '        Else
    '            Chart1.Series(0).Name = "Cumulative Distribution Function"
    '        End If

    '        Chart1.Series(0).ChartType = DataVisualization.Charting.SeriesChartType.Line

    '        Chart1.Series(0).Color = Color.Red
    '        Chart1.Series(0).BorderWidth = 2

    '        Chart1.ChartAreas(0).AxisX.Minimum = Val(txtChartMin.Text)

    '        Chart1.ChartAreas(0).AxisX.Maximum = Val(txtChartMax.Text)
    '        Chart1.ChartAreas(0).AxisX.Interval = Val(txtChartInterval.Text)

    '        Dim DataInfoIndex As Integer = Main.MonteCarlo.DataInfoNameIndex(SourceColumnName)
    '        If DataInfoIndex = -1 Then
    '            Chart1.ChartAreas(0).AxisX.Title = SourceColumnName
    '        Else
    '            Chart1.ChartAreas(0).AxisX.Title = SourceColumnName & " (" & Main.MonteCarlo.DataInfo(DataInfoIndex).Units & ")"
    '        End If

    '        Chart1.ChartAreas(0).AxisX.TitleFont = New Font("Arial", 12, FontStyle.Regular Or FontStyle.Bold)
    '        Chart1.ChartAreas(0).AxisX.LabelStyle.Format = "#.##"

    '        'Chart1.ChartAreas(0).AxisY.Title = "Probability"
    '        Chart1.ChartAreas(0).AxisY.Title = "Cumulative" & vbCrLf & "Probability"
    '        Chart1.ChartAreas(0).AxisY.TitleFont = New Font("Arial", 12, FontStyle.Regular Or FontStyle.Bold)

    '        Chart1.ChartAreas(0).AxisY.Minimum = 0
    '        Chart1.ChartAreas(0).AxisY.Maximum = 1



    '        'Chart1.ChartAreas(0).AxisX.RoundAxisValues()
    '        'Chart1.Series("CDF").Points.DataBindXY(Data.Tables("Series").DefaultView, "CDF", Data.Tables("Series").DefaultView, "Probability")
    '        'Chart1.Series("Cumulative Distribution Function").Points.DataBindXY(Data.Tables("Series").DefaultView, "CDF", Data.Tables("Series").DefaultView, "Probability")
    '        If ShowReverseCDF Then
    '            Chart1.Series("Reverse Cumulative Distribution Function").Points.DataBindXY(Data.Tables("Series").DefaultView, "Value", Data.Tables("Series").DefaultView, "Probability")
    '            Chart1.Series("Reverse Cumulative Distribution Function").IsVisibleInLegend = False
    '        Else
    '            Chart1.Series("Cumulative Distribution Function").Points.DataBindXY(Data.Tables("Series").DefaultView, "Value", Data.Tables("Series").DefaultView, "Probability")
    '            Chart1.Series("Cumulative Distribution Function").IsVisibleInLegend = False
    '        End If




    '        Chart1.Titles.Clear()
    '        Chart1.Titles.Add("Title1")
    '        Chart1.Titles(0).Text = "Random Variable: " & SourceColumnName
    '        Chart1.Titles(0).Font = New Font("Arial", 16, FontStyle.Regular Or FontStyle.Bold)
    '        Chart1.Titles(0).DockedToChartArea = Chart1.ChartAreas(0).Name
    '        Chart1.Titles(0).IsDockedInsideChartArea = False
    '        Chart1.Titles(0).Docking = DataVisualization.Charting.Docking.Top

    '        Chart1.Titles.Add("Title2")
    '        If ShowReverseCDF Then
    '            Chart1.Titles(1).Text = "Reverse Cumulative Distribution Function"
    '        Else
    '            Chart1.Titles(1).Text = "Cumulative Distribution Function"
    '        End If

    '        Chart1.Titles(1).Font = New Font("Arial", 14, FontStyle.Regular Or FontStyle.Bold)
    '        Chart1.Titles(1).DockedToChartArea = Chart1.ChartAreas(0).Name
    '        Chart1.Titles(1).IsDockedInsideChartArea = False
    '        Chart1.Titles(1).Docking = DataVisualization.Charting.Docking.Top

    '        ''Add a series used to plot vertical bars on the CDF chart:
    '        'Chart1.Series.Add("VertBar")
    '        'Chart1.Series("VertBar").ChartType = DataVisualization.Charting.SeriesChartType.Column
    '        'Chart1.Series("VertBar").Color = Color.Orange

    '    Catch ex As Exception
    '        Main.Message.AddWarning(ex.Message & vbCrLf)
    '    End Try
    'End Sub

    Private Sub GenHistogramData(ByVal MidVal As Double, ByVal IntWidth As Double)
        'Generate the histogram data.
        'MidVal is the mid value of an interval - MidVal can be in the first interval - If there is data preceeding this interval, intervals will be added to include all data.
        'IntWidth is the width of the intervals

        'Remove the old PDF table if it exists.
        'If Data.Tables.Contains("Histogram") Then
        If Main.Distribution.Data.Tables.Contains("Histogram") Then
            Main.Distribution.Data.Tables.Remove("Histogram")
        Else
            Main.Adjust.cmbTableName.Items.Add("Histogram")
        End If

        Main.Distribution.Data.Tables.Add("Histogram")
        'cmbTableName.Items.Add("Histogram")


        'Create the Mid_Interval column:
        'Main.Distribution.Data.Tables("Histogram").Columns.Add("Mid_Interval", System.Type.GetType("System.Double"))
        'CalcData.Tables("Histogram").Columns.Add("Mid_Interval", System.Type.GetType("System.Double"))
        CalcData.Tables("Histogram").Columns.Add("Value", System.Type.GetType("System.Double")) 'UPDATE 15May22 - Use Value instead of Mid_Interval ????
        'Create the Count column:
        'Main.Distribution.Data.Tables("Histogram").Columns.Add("Count", System.Type.GetType("System.Int32"))
        CalcData.Tables("Histogram").Columns.Add("Count", System.Type.GetType("System.Int32"))
        'Create the Probability column:
        'Main.Distribution.Data.Tables("Histogram").Columns.Add("Probability", System.Type.GetType("System.Double"))
        CalcData.Tables("Histogram").Columns.Add("Probability", System.Type.GetType("System.Double"))

        'The MinSeriesVal property contains the minimum data value in the series.
        Dim StartGap As Double = MidVal - MinSeriesVal 'The gap between the first data value in the series and the MidVal histogram interval.
        'Dim FirstMidVal As Double = MidVal + Int(StartGap / IntWidth) * IntWidth 'The mid value of the first histogram interval - to include all data points.
        Dim FirstMidVal As Double = MidVal - Int(StartGap / IntWidth) * IntWidth 'The mid value of the first histogram interval - to include all data points.

        Dim MidInterval As Double = FirstMidVal 'The mid value of the current histogram interval
        Dim EndInterval As Double = FirstMidVal + IntWidth / 2 'The maximum value of the current histogram interval
        Dim PointCount As Integer = 0 'The count of points within the current histogram interval
        'For Each Row As DataRowView In DataSource.Data.Tables("Series").DefaultView 'This uses the sorted row view
        'For Each Row As DataRowView In Main.Distribution.Data.Tables("Model_Fitting").DefaultView 'This uses the sorted row view
        For Each Row As DataRowView In CalcData.Tables("Series").DefaultView 'This uses the sorted row view
            'If Row.Item("CDF") < EndInterval Then
            If Row.Item("Value") < EndInterval Then
                PointCount += 1
            Else
                'Main.Distribution.Data.Tables("Histogram").Rows.Add(EndInterval - IntWidth / 2, PointCount, PointCount / NRows)
                CalcData.Tables("Histogram").Rows.Add(EndInterval - IntWidth / 2, PointCount, PointCount / NRows)
                PointCount = 0
                EndInterval += IntWidth
            End If
        Next
    End Sub

    ''OLD CODE: [Replaced by PlotCharts()]
    'Private Sub PlotHistogram()
    '    'Plot the histogram chart.

    '    'If Chart1.Series(0).Name = "CDF" Then
    '    If (Chart1.Series(0).Name = "Cumulative Distribution Function") Or (Chart1.Series(0).Name = "Reverse Cumulative Distribution Function") Then
    '        If Chart1.Series.Count > 1 Then
    '            If Chart1.Series(1).Name = "Histogram" Then

    '            Else
    '                Chart1.Series(1).Name = "Histogram"
    '            End If
    '        Else
    '            Chart1.Series.Add("Histogram")
    '            Chart1.ChartAreas.Add("HistArea")
    '            'Chart1.Series("Histogram").ChartType = DataVisualization.Charting.SeriesChartType.Line
    '            ''Chart1.Series("Histogram").ChartArea = Chart1.ChartAreas(0).Name
    '            'Chart1.ChartAreas.Add("HistArea")
    '            'Chart1.Series("Histogram").ChartArea = "HistArea"
    '            ''Chart1.Series("Histogram").XAxisType = DataVisualization.Charting.AxisType.Secondary
    '            ''Chart1.Series("Histogram").YAxisType = DataVisualization.Charting.AxisType.Secondary
    '            'Chart1.ChartAreas(0).AxisX.RoundAxisValues()
    '            'Chart1.Series("Histogram").Points.DataBindXY(Data.Tables("Histogram").DefaultView, "Mid_Interval", Data.Tables("Histogram").DefaultView, "Probability")

    '            ''Use the same X Axis ranges:
    '            'Chart1.ChartAreas(0).AxisX.Minimum = Chart1.ChartAreas("HistArea").AxisX.Minimum
    '            'Chart1.ChartAreas(0).AxisX.Maximum = Chart1.ChartAreas("HistArea").AxisX.Maximum
    '        End If
    '        'Chart1.Series("Histogram").ChartType = DataVisualization.Charting.SeriesChartType.Line
    '        'Chart1.Series("Histogram").ChartType = DataVisualization.Charting.SeriesChartType.Bar
    '        Chart1.Series("Histogram").ChartType = DataVisualization.Charting.SeriesChartType.Column

    '        Chart1.Series("Histogram").Color = Color.Blue

    '        'Chart1.Series("Histogram").ChartArea = Chart1.ChartAreas(0).Name
    '        'Chart1.ChartAreas.Add("HistArea")
    '        Chart1.Series("Histogram").ChartArea = "HistArea"
    '        'Chart1.Series("Histogram").XAxisType = DataVisualization.Charting.AxisType.Secondary
    '        'Chart1.Series("Histogram").YAxisType = DataVisualization.Charting.AxisType.Secondary
    '        'Chart1.ChartAreas(0).AxisX.RoundAxisValues()
    '        Chart1.Series("Histogram").Points.DataBindXY(Data.Tables("Histogram").DefaultView, "Mid_Interval", Data.Tables("Histogram").DefaultView, "Probability")

    '        Chart1.Series("Histogram").IsVisibleInLegend = False

    '        'Use the same X Axis ranges:
    '        'Chart1.ChartAreas(0).AxisX.Minimum = Chart1.ChartAreas("HistArea").AxisX.Minimum
    '        'Chart1.ChartAreas(0).AxisX.Maximum = Chart1.ChartAreas("HistArea").AxisX.Maximum

    '        Chart1.ChartAreas("HistArea").AxisX.Minimum = Val(txtChartMin.Text)
    '        Chart1.ChartAreas("HistArea").AxisX.Maximum = Val(txtChartMax.Text)
    '        Chart1.ChartAreas("HistArea").AxisX.Interval = Val(txtChartInterval.Text)

    '        Dim DataInfoIndex As Integer = Main.MonteCarlo.DataInfoNameIndex(SourceColumnName)
    '        If DataInfoIndex = -1 Then
    '            Chart1.ChartAreas("HistArea").AxisX.Title = SourceColumnName
    '        Else
    '            Chart1.ChartAreas("HistArea").AxisX.Title = SourceColumnName & " (" & Main.MonteCarlo.DataInfo(DataInfoIndex).Units & ")"
    '        End If
    '        Chart1.ChartAreas("HistArea").AxisX.TitleFont = New Font("Arial", 12, FontStyle.Regular Or FontStyle.Bold)

    '        Chart1.ChartAreas("HistArea").AxisY.Title = "Probability"
    '        Chart1.ChartAreas("HistArea").AxisY.TitleFont = New Font("Arial", 12, FontStyle.Regular Or FontStyle.Bold)

    '        Chart1.Titles.Add("Title3")
    '        Chart1.Titles(2).Text = "Histogram"
    '        Chart1.Titles(2).Font = New Font("Arial", 14, FontStyle.Regular Or FontStyle.Bold)
    '        Chart1.Titles(2).DockedToChartArea = "HistArea"
    '        Chart1.Titles(2).IsDockedInsideChartArea = False
    '        Chart1.Titles(2).Docking = DataVisualization.Charting.Docking.Top


    '        Chart1.ChartAreas(0).AlignWithChartArea = "HistArea"
    '        Chart1.ChartAreas(0).AlignmentOrientation = DataVisualization.Charting.AreaAlignmentOrientations.Vertical
    '        Chart1.ChartAreas(0).AlignmentStyle = DataVisualization.Charting.AreaAlignmentStyles.All

    '        Chart1.ChartAreas(0).AxisX.RoundAxisValues()

    '        Main.Message.Add("Chart1.ChartAreas(0).AxisX.Minimum " & Chart1.ChartAreas(0).AxisX.Minimum & vbCrLf)
    '        Main.Message.Add("Chart1.ChartAreas(0).AxisX.Maximum " & Chart1.ChartAreas(0).AxisX.Maximum & vbCrLf)
    '        Main.Message.Add("Chart1.ChartAreas(0).AxisX.Interval " & Chart1.ChartAreas(0).AxisX.Interval & vbCrLf)


    '        Main.Message.Add("Chart1.ChartAreas(1).AxisX.Minimum " & Chart1.ChartAreas(1).AxisX.Minimum & vbCrLf)
    '        Main.Message.Add("Chart1.ChartAreas(1).AxisX.Maximum " & Chart1.ChartAreas(1).AxisX.Maximum & vbCrLf)
    '        Main.Message.Add("Chart1.ChartAreas(1).AxisX.Interval " & Chart1.ChartAreas(1).AxisX.Interval & vbCrLf)

    '        'Add a series used to plot vertical bars on the CDF chart:
    '        Dim IndexNo As Integer = Chart1.Series.IndexOf("CdfVertBar")
    '        If IndexNo = -1 Then 'Series named CdfVerBar does not exist
    '            Chart1.Series.Add("CdfVertBar")
    '            Chart1.Series("CdfVertBar").ChartType = DataVisualization.Charting.SeriesChartType.Column
    '            Chart1.Series("CdfVertBar").Color = Color.Orange
    '            Chart1.Series("CdfVertBar").ChartArea = Chart1.ChartAreas(0).Name
    '            Chart1.Series("CdfVertBar").SetCustomProperty("PixelPointWidth", "2")
    '            Chart1.Series("CdfVertBar").IsVisibleInLegend = False
    '        Else

    '        End If

    '        'Add points with circle markers instead:
    '        IndexNo = Chart1.Series.IndexOf("HistPoints")
    '        If IndexNo = -1 Then  'Series named HistPoints does not exist
    '            Chart1.Series.Add("HistPoints")
    '            Chart1.Series("HistPoints").ChartType = DataVisualization.Charting.SeriesChartType.Point
    '            Chart1.Series("HistPoints").Color = Color.Transparent
    '            Chart1.Series("HistPoints").ChartArea = "HistArea"
    '            Chart1.Series("HistPoints").IsVisibleInLegend = False
    '            Chart1.Series("HistPoints").MarkerStyle = DataVisualization.Charting.MarkerStyle.Circle
    '            Chart1.Series("HistPoints").MarkerSize = 20
    '            Chart1.Series("HistPoints").MarkerBorderWidth = 2
    '            Chart1.Series("HistPoints").MarkerBorderColor = Color.Orange
    '        Else

    '        End If

    '    Else
    '        Chart1.Series(0).Name = "Histogram"
    '        Chart1.Series("Histogram").ChartType = DataVisualization.Charting.SeriesChartType.Line
    '        Chart1.ChartAreas(0).AxisX.LabelStyle.Format = "#.##"
    '        Chart1.ChartAreas(0).AxisX.RoundAxisValues()
    '        Chart1.Series("Histogram").Points.DataBindXY(Data.Tables("Histogram").DefaultView, "Mid_Interval", Data.Tables("Histogram").DefaultView, "Probability")
    '    End If

    'End Sub

    'Private Sub cmbTableName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCalcTableName.SelectedIndexChanged
    Private Sub cmbCalcTableName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCalcTableName.SelectedIndexChanged
        If cmbCalcTableName.Focused Then
            _calcTableName = cmbCalcTableName.SelectedItem.ToString
            UpdateDataGridView()
        End If
    End Sub

    Private Sub UpdateDataGridView()
        'Update the data grid biew in the Data tab

        dgvData.Columns.Clear()
        dgvData.AutoGenerateColumns = True
        'dgvData.DataSource = Data.Tables(TableName)
        'dgvData.DataSource = Main.Distribution.Data.Tables(TableName)
        'dgvData.DataSource = Main.Distribution.Data.Tables(TableName)
        dgvData.DataSource = CalcData.Tables(CalcTableName)
        dgvData.AutoResizeColumns()
        dgvData.Update()
        dgvData.Refresh()
    End Sub

    Private Sub btnUpdateHistogram_Click(sender As Object, e As EventArgs) Handles btnUpdateHistogram.Click
        'Update the Histogram data

        If txtIntervalWidth.Text.Trim = "" Then
            Main.Message.AddWarning("Enter an interval width to use in the histogram." & vbCrLf)
        Else

            GenHistogramData(Val(txtHistMin.Text), Val(txtIntervalWidth.Text))
            'PlotHistogram()
            PlotCharts()
        End If
    End Sub

    Private Sub btnShowChartMinMax_Click(sender As Object, e As EventArgs) Handles btnShowChartMinMax.Click
        'Show the current Chart Minimum, Maximum and Interval
        If Chart1.ChartAreas.Count = 0 Then
            Main.Message.AddWarning("No chart areas are defined." & vbCrLf)
        Else
            txtChartMin.Text = Chart1.ChartAreas(0).AxisX.Minimum
            txtChartMax.Text = Chart1.ChartAreas(0).AxisX.Maximum
            txtChartInterval.Text = Chart1.ChartAreas(0).AxisX.Interval
        End If
    End Sub

    Private Sub btnUpdateChartMinMax_Click(sender As Object, e As EventArgs) Handles btnUpdateChartMinMax.Click
        'Update the chart displays with the specified minimum, maximum and interval.

        ApplyChartMinMax()
    End Sub

    Private Sub ApplyChartMinMax()
        'Apply the Chart Minimum, Maximum and Interval if specified.

        Dim NChartAreas As Integer = Chart1.ChartAreas.Count
        Dim I As Integer

        Try
            If txtChartMin.Text.Trim = "" Then
                'The Chart Minimum is not specified.
            Else
                Dim ChartMin As Double = txtChartMin.Text
                For I = 0 To NChartAreas - 1
                    Chart1.ChartAreas(I).AxisX.Minimum = ChartMin
                Next
            End If
        Catch ex As Exception
            Main.Message.AddWarning(ex.Message & vbCrLf)
        End Try

        Try
            If txtChartMax.Text.Trim = "" Then
                'The Chart Maximum is not specified.
            Else
                Dim ChartMax As Double = txtChartMax.Text
                For I = 0 To NChartAreas - 1
                    Chart1.ChartAreas(I).AxisX.Maximum = ChartMax
                Next
            End If
        Catch ex As Exception
            Main.Message.AddWarning(ex.Message & vbCrLf)
        End Try

        Try
            If txtChartInterval.Text.Trim = "" Then
                'The Chart Interval is not specified.
            Else
                Dim ChartInt As Double = txtChartInterval.Text
                For I = 0 To NChartAreas - 1
                    Chart1.ChartAreas(I).AxisX.Interval = ChartInt
                Next
            End If
        Catch ex As Exception
            Main.Message.AddWarning(ex.Message & vbCrLf)
        End Try

    End Sub

    Private Sub txtNIntervals_TextChanged(sender As Object, e As EventArgs) Handles txtNIntervals.TextChanged

    End Sub

    Private Sub txtNIntervals_LostFocus(sender As Object, e As EventArgs) Handles txtNIntervals.LostFocus
        HistNIntervals = Val(txtNIntervals.Text)
    End Sub

    Private Sub txtIntervalWidth_TextChanged(sender As Object, e As EventArgs) Handles txtIntervalWidth.TextChanged

    End Sub

    Private Sub txtIntervalWidth_LostFocus(sender As Object, e As EventArgs) Handles txtIntervalWidth.LostFocus
        _histNIntervals = Int((MaxSeriesVal - MinSeriesVal) / Val(txtIntervalWidth.Text))
        txtNIntervals.Text = _histNIntervals
    End Sub

    Private Function PreferredInterval(ByVal RawInterval As Double) As Double
        'Return a preferred interval value from a raw interval.
        'Preferred intervals are rounded to a number containing fewer significant figures.
        'Examples: Raw    Preferred
        '          0.234  0.25
        '          497    500
        '          89.4   100  
        '          18.1   20

        'Convert the RawInterval to scientific notation Coeff x 10 ^ Exponent
        Dim Coeff As Double
        Dim Exponent As Integer

        Dim Log10RawInt As Double = Math.Log10(RawInterval)
        Exponent = Math.Floor(Log10RawInt)
        Coeff = RawInterval / 10 ^ Exponent

        Dim PreferredCoeff = NearestPrefCoeff(Coeff, {1, 2, 2.5, 5, 10}) 'Select the coefficient from the preferred coefficient list - the one nearest to the raw coefficient
        Return PreferredCoeff * 10 ^ Exponent 'The preferred interval is reconstructed from the preferred coefficient and the exponent
    End Function

    Private Function NearestPrefCoeff(ByVal RawCoeff As Double, ByVal PrefCoeff() As Double) As Double
        'Returns the nearest preferred coefficient to the Raw Coefficient

        If PrefCoeff.Count > 0 Then
            Dim Nearest As Double = PrefCoeff(0) 'The current nearest preferred coefficent
            Dim NearestAbsDiff As Double = Math.Abs(RawCoeff - PrefCoeff(0)) 'The current nearest absolute difference between the Raw Coefficient and the Preferred Coefficient
            Dim AbsDiff As Double 'The absolute difference between the Raw Coefficient and the Preferred Coefficient
            For Each item In PrefCoeff
                AbsDiff = Math.Abs(RawCoeff - item)
                If AbsDiff < NearestAbsDiff Then
                    Nearest = item
                    NearestAbsDiff = AbsDiff
                End If
            Next
            Return Nearest
        Else
            Main.Message.AddWarning("There are no preferred coefficents in the list." & vbCrLf)
            Return RawCoeff
        End If
    End Function


    'OLD CODE: [Replaced by GetHistogramSettings()]
    Private Sub SetUpHistogram(ByVal NIntervals As Integer)
        'Set up the Histogram settings to match the specified NIntervals.


        Dim RawInterval As Double = (MaxSeriesVal - MinSeriesVal) / NIntervals 'First calculate the Raw Interval
        Dim PrefInterval As Double = PreferredInterval(RawInterval) 'The preferred interval has the significant digits: 1, 2, 2.5, 5, 10
        Dim PrefMin As Double = Math.Floor(MinSeriesVal / PrefInterval) * PrefInterval
        Dim PrefMax As Double = Math.Ceiling(MaxSeriesVal / PrefInterval) * PrefInterval
        Dim NewNIntervals As Integer = Math.Round((PrefMax - PrefMin) / PrefInterval)

        txtHistMin.Text = PrefMin
        txtHistMax.Text = PrefMax
        txtNIntervals.Text = NewNIntervals
        txtIntervalWidth.Text = PrefInterval
        Main.Message.Add("Preferred histogram interval: " & PrefInterval & vbCrLf)
        Main.Message.Add("Number of intervals: " & NewNIntervals & vbCrLf)

    End Sub

    Private Sub GetHistogramSettings()
        'Get settings to display a histogram of the data.

        If IsDiscrete Then
            HistIntervalWidth = 1
            HistMin = MinSeriesVal
            HistMax = MaxSeriesVal
            HistNIntervals = MaxSeriesVal - MinSeriesVal + 1
        Else
            InitNIntervals = Int(NRows / 100) 'Initial number of intervals = the numer of data rows / 100. For 1000 points: 10 intervals, 10000 points: 100 intervals. The number of intervals will be adjusted below.
            Dim RawInterval As Double = (MaxSeriesVal - MinSeriesVal) / InitNIntervals 'First calculate the Raw Interval - InitNIntervals (the initial number of intervals) must be set
            HistIntervalWidth = PreferredInterval(RawInterval) 'The preferred interval has the significant digits: 1, 2, 2.5, 5, 10
            HistMin = Math.Floor(MinSeriesVal / HistIntervalWidth) * HistIntervalWidth
            HistMax = Math.Ceiling(MaxSeriesVal / HistIntervalWidth) * HistIntervalWidth
            HistNIntervals = Math.Round(((HistMax - HistMin) / HistIntervalWidth) + 1)
        End If
    End Sub

    'OLD CODE: [Replaced by GetChartXScale()]
    Private Sub SetUpChartScale(ByVal NLabels As Integer)
        'Set up the Chart X Scale to match the specified number of labels.

        Dim RawInterval As Double = (MaxSeriesVal - MinSeriesVal) / NLabels
        Dim PrefInterval As Double = PreferredInterval(RawInterval)
        Dim PrefMin As Double = Math.Floor(MinSeriesVal / PrefInterval) * PrefInterval
        Dim PrefMax As Double = Math.Ceiling(MaxSeriesVal / PrefInterval) * PrefInterval

        txtChartMin.Text = PrefMin
        txtChartMax.Text = PrefMax
        txtChartInterval.Text = PrefInterval

    End Sub

    Private Sub GetChartXScale(ByVal RawPixelsPerInterval As Integer)
        'Get sutable values for ChartXMin, ChartXMax and ChartXInterval.
        'ChartXInterval is generated using the RawPixelPerInterval value.

        Dim XAxisLength As Single = Chart1.ChartAreas(0).InnerPlotPosition.Width 'X Axis length in relative coordinates, which range from 0 to 100.
        Dim ChartPixelWidth As Integer = Chart1.Width 'The width of the entire chart image in pixels.
        Dim XAxisPixelLength As Integer = Int(ChartPixelWidth * XAxisLength / 100)
        Dim XRawNIntervals As Integer = Int(XAxisPixelLength / RawPixelsPerInterval) 'The Raw number of axis annotation intervals based on the Raw pixels per annotation.

        Dim XRawInterval As Double = (MaxSeriesVal - MinSeriesVal) / XRawNIntervals 'First calculate the Raw Interval
        ChartXInterval = PreferredInterval(XRawInterval) 'The preferred X Axis lavel interval has the significant digits: 1, 2, 2.5, 5, 10.
        ChartXMin = Math.Floor(MinSeriesVal / ChartXInterval) * ChartXInterval 'The preferred Axis Minimum for the Chart display
        ChartXMax = Math.Ceiling(MaxSeriesVal / ChartXInterval) * ChartXInterval 'The preferred Axis Maximum for the Chart display
    End Sub

    Private Function GetValue(ByVal Prob As Double) As Double
        'Get the value corresponding the the given probability value.
        'The value is interpolated from the records in the Series table spanning the probability value.

        Dim Rows() As DataRow
        Dim Row1 As DataRow
        Dim Row2 As DataRow

        If ShowReverseCDF Then
            'Row1 = Data.Tables("Series").Select("Reverse_CDF < '" & Prob & "'").Last
            'Row1 = DataSource.Data.Tables("Model_Fitting").Select("Reverse_eCDF < '" & Prob & "'").Last
            'Rows = Main.Distribution.Data.Tables("Model_Fitting").Select("Reverse_eCDF < '" & Prob & "'")
            Rows = CalcData.Tables("Series").Select("Reverse_eCDF < '" & Prob & "'")
            Row1 = Rows.Last
            'Row2 = Data.Tables("Series").Select("Reverse_CDF > '" & Prob & "'").First
            'Row2 = DataSource.Data.Tables("Model_Fitting").Select("Reverse_eCDF > '" & Prob & "'").First
            'Rows = Main.Distribution.Data.Tables("Model_Fitting").Select("Reverse_eCDF > '" & Prob & "'")
            Rows = CalcData.Tables("Series").Select("Reverse_eCDF > '" & Prob & "'")
            Row2 = Rows.First
        Else
            'Row1 = Data.Tables("Series").Select("CDF < '" & Prob & "'").Last
            'Row1 = DataSource.Data.Tables("Model_Fitting").Select("eCDF < '" & Prob & "'").Last
            'Rows = Main.Distribution.Data.Tables("Model_Fitting").Select("eCDF < '" & Prob & "'")
            Rows = CalcData.Tables("Series").Select("eCDF < '" & Prob & "'")
            Row1 = Rows.Last
            'Row2 = Data.Tables("Series").Select("CDF > '" & Prob & "'").First
            'Row2 = DataSource.Data.Tables("Model_Fitting").Select("eCDF > '" & Prob & "'").First
            'Rows = Main.Distribution.Data.Tables("Model_Fitting").Select("eCDF > '" & Prob & "'")
            Rows = CalcData.Tables("Series").Select("eCDF > '" & Prob & "'")
            Row2 = Rows.First
        End If


        If Row1 Is Nothing Then
            Main.Message.AddWarning("Error interpolating the value." & vbCrLf)
            Return 0
        Else
            If Row2 Is Nothing Then
                Main.Message.AddWarning("Error interpolating the value." & vbCrLf)
                Return 0
            Else
                If ShowReverseCDF Then
                    'Return Row1.Item("Value") + (Prob - Row1.Item("Reverse_CDF")) * (Row2.Item("Value") - Row1.Item("Value")) / (Row2.Item("Reverse_CDF") - Row1.Item("Reverse_CDF"))
                    Return Row1.Item("Value") + (Prob - Row1.Item("Reverse_eCDF")) * (Row2.Item("Value") - Row1.Item("Value")) / (Row2.Item("Reverse_eCDF") - Row1.Item("Reverse_eCDF"))
                Else
                    'Return Row1.Item("Value") + (Prob - Row1.Item("CDF")) * (Row2.Item("Value") - Row1.Item("Value")) / (Row2.Item("CDF") - Row1.Item("CDF"))
                    Return Row1.Item("Value") + (Prob - Row1.Item("eCDF")) * (Row2.Item("Value") - Row1.Item("Value")) / (Row2.Item("eCDF") - Row1.Item("eCDF"))
                End If
            End If
        End If
    End Function


    Private Function GetProb(ByVal Value As Double) As Double
        'Get the probability corresponding to the given value.
        'The probability is interpolated from the records inthe Series table spanning the value
        Try
            Dim Rows() As DataRow
            Dim Row1 As DataRow
            Dim Row2 As DataRow
            'Dim Row1 As DataRow = Data.Tables("Series").Select("Value < '" & Value & "'").Last
            'Dim Row1 As DataRow = DataSource.Data.Tables("Model_Fitting").Select("Value < '" & Value & "'").Last
            'Rows = Main.Distribution.Data.Tables("Model_Fitting").Select("Value < '" & Value & "'")
            Rows = CalcData.Tables("Series").Select("Value < '" & Value & "'")
            Row1 = Rows.Last
            'Dim Row2 As DataRow = Data.Tables("Series").Select("Value > '" & Value & "'").First
            'Dim Row2 As DataRow = DataSource.Data.Tables("Model_Fitting").Select("Value > '" & Value & "'").First
            'Rows = Main.Distribution.Data.Tables("Model_Fitting").Select("Value > '" & Value & "'")
            Rows = CalcData.Tables("Series").Select("Value > '" & Value & "'")
            Row2 = Rows.First

            If Row1 Is Nothing Then
                Main.Message.AddWarning("Error interpolating the value." & vbCrLf)
                Return 0
            Else
                If Row2 Is Nothing Then
                    Main.Message.AddWarning("Error interpolating the value." & vbCrLf)
                    Return 0
                Else
                    'Return Row1.Item("CDF") + (Value - Row1.Item("Value")) * (Row2.Item("CDF") - Row1.Item("CDF")) / (Row2.Item("Value") - Row1.Item("Value"))
                    Return Row1.Item("eCDF") + (Value - Row1.Item("Value")) * (Row2.Item("eCDF") - Row1.Item("eCDF")) / (Row2.Item("Value") - Row1.Item("Value"))
                End If
            End If
        Catch ex As Exception
            Main.Message.AddWarning(ex.Message & vbCrLf)
        End Try

    End Function

    Private Sub dgvAnnot_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvAnnot.CellContentClick

    End Sub


    Private Sub AddProbAnnot(ByVal RowNo As Integer)
        'Add a probability annotation entry at the specified row number.

        Dim P1 As Boolean = False 'If True then the P1 annotation is in the list
        Dim P10 As Boolean = False 'If True then the P10 annotation is in the list
        Dim P50 As Boolean = False 'If True then the P50 annotation is in the list
        Dim P90 As Boolean = False 'If True then the P90 annotation is in the list
        Dim P99 As Boolean = False 'If True then the P99 annotation is in the list

        Dim ProbVal As Double 'The selected probability value

        dgvAnnot.AllowUserToAddRows = False

        Dim I As Integer
        For I = 0 To dgvAnnot.RowCount - 1
            If dgvAnnot.Rows(I).Cells(2).Value = "Probability" Then
                If dgvAnnot.Rows(I).Cells(5).Value = 0.01 Then P1 = True
                If dgvAnnot.Rows(I).Cells(5).Value = 0.1 Then P10 = True
                If dgvAnnot.Rows(I).Cells(5).Value = 0.5 Then P50 = True
                If dgvAnnot.Rows(I).Cells(5).Value = 0.9 Then P90 = True
                If dgvAnnot.Rows(I).Cells(5).Value = 0.99 Then P99 = True
            End If
        Next

        If P10 = False Then 'Enter P10 probability annotation settings:
            Dim P10Value As Double = GetValue(0.1)
            If RowNo > dgvAnnot.RowCount - 1 Then
                dgvAnnot.Rows.Add({True, True, "Probability", 0.1, "P10", 0.1, P10Value})
            Else
                dgvAnnot.Rows(RowNo).Cells(0).Value = True
                dgvAnnot.Rows(RowNo).Cells(1).Value = True
                dgvAnnot.Rows(RowNo).Cells(2).Value = "Probability"
                dgvAnnot.Rows(RowNo).Cells(3).Value = 0.1
                dgvAnnot.Rows(RowNo).Cells(4).Value = "P10"
                dgvAnnot.Rows(RowNo).Cells(5).Value = 0.1
                dgvAnnot.Rows(RowNo).Cells(6).Value = P10Value
            End If
            DisplayAnnot(0.1, P10Value, "P10")
        ElseIf P50 = False Then 'Enter P50 probability annotation settings:
            Dim P50Value As Double = GetValue(0.5)
            If RowNo > dgvAnnot.RowCount - 1 Then
                dgvAnnot.Rows.Add({True, True, "Probability", 0.5, "P50", 0.5, P50Value})
            Else
                dgvAnnot.Rows(RowNo).Cells(0).Value = True
                dgvAnnot.Rows(RowNo).Cells(1).Value = True
                dgvAnnot.Rows(RowNo).Cells(2).Value = "Probability"
                dgvAnnot.Rows(RowNo).Cells(3).Value = 0.5
                dgvAnnot.Rows(RowNo).Cells(4).Value = "P50"
                dgvAnnot.Rows(RowNo).Cells(5).Value = 0.5
                dgvAnnot.Rows(RowNo).Cells(6).Value = P50Value
            End If
            DisplayAnnot(0.5, P50Value, "P50")
        ElseIf P90 = False Then 'Enter P90 probability annotation settings:
            Dim P90Value As Double = GetValue(0.9)
            If RowNo > dgvAnnot.RowCount - 1 Then
                dgvAnnot.Rows.Add({True, True, "Probability", 0.9, "P90", 0.9, P90Value})
            Else
                dgvAnnot.Rows(RowNo).Cells(0).Value = True
                dgvAnnot.Rows(RowNo).Cells(1).Value = True
                dgvAnnot.Rows(RowNo).Cells(2).Value = "Probability"
                dgvAnnot.Rows(RowNo).Cells(3).Value = 0.9
                dgvAnnot.Rows(RowNo).Cells(4).Value = "P90"
                dgvAnnot.Rows(RowNo).Cells(5).Value = 0.9
                dgvAnnot.Rows(RowNo).Cells(6).Value = P90Value
            End If
            DisplayAnnot(0.9, P90Value, "P90")
        ElseIf P1 = False Then 'Enter P1 probability annotation settings:
            Dim P1Value As Double = GetValue(0.01)
            If RowNo > dgvAnnot.RowCount - 1 Then
                dgvAnnot.Rows.Add({True, True, "Probability", 0.01, "P1", 0.01, P1Value})
            Else
                dgvAnnot.Rows(RowNo).Cells(0).Value = True
                dgvAnnot.Rows(RowNo).Cells(1).Value = True
                dgvAnnot.Rows(RowNo).Cells(2).Value = "Probability"
                dgvAnnot.Rows(RowNo).Cells(3).Value = 0.01
                dgvAnnot.Rows(RowNo).Cells(4).Value = "P1"
                dgvAnnot.Rows(RowNo).Cells(5).Value = 0.01
                dgvAnnot.Rows(RowNo).Cells(6).Value = P1Value
            End If
            DisplayAnnot(0.01, P1Value, "P1")
        ElseIf P99 = False Then 'Enter P99 probability annotation settings:
            Dim P99Value As Double = GetValue(0.99)
            If RowNo > dgvAnnot.RowCount - 1 Then
                dgvAnnot.Rows.Add({True, True, "Probability", 0.99, "P99", 0.99, P99Value})
            Else
                dgvAnnot.Rows(RowNo).Cells(0).Value = True
                dgvAnnot.Rows(RowNo).Cells(1).Value = True
                dgvAnnot.Rows(RowNo).Cells(2).Value = "Probability"
                dgvAnnot.Rows(RowNo).Cells(3).Value = 0.99
                dgvAnnot.Rows(RowNo).Cells(4).Value = "P99"
                dgvAnnot.Rows(RowNo).Cells(5).Value = 0.99
                dgvAnnot.Rows(RowNo).Cells(6).Value = P99Value
            End If
            DisplayAnnot(0.99, P99Value, "P99")
        Else
            'User defined probability annotation.
        End If
        dgvAnnot.AllowUserToAddRows = True
    End Sub

    Private Sub AddMeanAnnot(ByVal RowNo As Integer)
        'Add a mean annotation entry at the specified row number.

        Dim MeanAnnotated As Boolean = False 'If True then the Mean annotation is in the list

        Dim MeanVal As Double 'The Mean value
        Dim Prob As Double 'The corresponding probability value

        dgvAnnot.AllowUserToAddRows = False

        'Check if the Mean value is already annotated:
        Dim I As Integer
        For I = 0 To dgvAnnot.RowCount - 1
            If dgvAnnot.Rows(I).Cells(2).Value = "Mean" Then MeanAnnotated = True
        Next

        If MeanAnnotated = False Then
            MeanVal = txtAverage.Text
            Prob = GetProb(MeanVal)
            If RowNo > dgvAnnot.RowCount - 1 Then
                dgvAnnot.Rows.Add({True, True, "Mean", "", "Mean", Prob, MeanVal})
            Else
                dgvAnnot.Rows(RowNo).Cells(0).Value = True
                dgvAnnot.Rows(RowNo).Cells(1).Value = True
                dgvAnnot.Rows(RowNo).Cells(2).Value = "Mean"
                dgvAnnot.Rows(RowNo).Cells(3).Value = ""
                dgvAnnot.Rows(RowNo).Cells(4).Value = "Mean"
                dgvAnnot.Rows(RowNo).Cells(5).Value = Prob
                dgvAnnot.Rows(RowNo).Cells(6).Value = MeanVal
            End If
            DisplayAnnot(Prob, MeanVal, "Mean")
        Else
            'The Mean annotation is already in the list.
        End If

        dgvAnnot.AllowUserToAddRows = True
    End Sub

    Private Sub AddValueAnnot(ByVal RowNo As Integer)
        'Add a value annotation entry at the specified row number.

        Dim Value As Double = dgvAnnot.Rows(RowNo).Cells(3).Value  'The value to be annotated
        Dim Prob As Double = GetProb(Value) 'The corresponding probability value

        dgvAnnot.AllowUserToAddRows = False
        If RowNo > dgvAnnot.RowCount - 1 Then
            dgvAnnot.Rows.Add({True, True, "Value", Prob, "Value", Prob, Value})
        Else
            dgvAnnot.Rows(RowNo).Cells(0).Value = True
            dgvAnnot.Rows(RowNo).Cells(1).Value = True
            dgvAnnot.Rows(RowNo).Cells(2).Value = "Value"
            dgvAnnot.Rows(RowNo).Cells(3).Value = Prob
            dgvAnnot.Rows(RowNo).Cells(4).Value = "Value"
            dgvAnnot.Rows(RowNo).Cells(5).Value = Prob
            dgvAnnot.Rows(RowNo).Cells(6).Value = Value
        End If

        dgvAnnot.AllowUserToAddRows = True
    End Sub

    Private Sub AddStdDevAnnot(ByVal RowNo As Integer)
        'Add a standard deviation annotation entry at the specified row number.

        dgvAnnot.AllowUserToAddRows = False

        Dim SDev1 As Boolean = False 'Corresonds to a Standard Deviation parameter of 1
        Dim SDevN1 As Boolean = False 'Corresonds to a Standard Deviation parameter of -1
        Dim SDev2 As Boolean = False 'Corresonds to a Standard Deviation parameter of 2
        Dim SDevN2 As Boolean = False 'Corresonds to a Standard Deviation parameter of -2
        Dim SDev3 As Boolean = False 'Corresonds to a Standard Deviation parameter of 3
        Dim SDevN3 As Boolean = False 'Corresonds to a Standard Deviation parameter of -3

        'Check if any Standard Deviation values are already annotated:
        Dim I As Integer
        For I = 0 To dgvAnnot.RowCount - 1
            If dgvAnnot.Rows(I).Cells(2).Value = "Standard Deviation" Then
                If dgvAnnot.Rows(I).Cells(3).Value = 1 Then SDev1 = True
                If dgvAnnot.Rows(I).Cells(3).Value = -1 Then SDevN1 = True
                If dgvAnnot.Rows(I).Cells(3).Value = 2 Then SDev2 = True
                If dgvAnnot.Rows(I).Cells(3).Value = -2 Then SDevN2 = True
                If dgvAnnot.Rows(I).Cells(3).Value = 3 Then SDev3 = True
                If dgvAnnot.Rows(I).Cells(3).Value = -3 Then SDevN3 = True
            End If
        Next

        Dim StdDev As Double = txtStdDev.Text 'The Standard Deviation of the series
        Dim Mean As Double = txtAverage.Text ' The Mean value of the series

        If SDev1 = False Then
            Dim SDev1Value As Double = Mean + StdDev
            Dim SDev1Prob As Double = GetProb(SDev1Value)
            Dim Label As String = "1" & ChrW(963)
            If RowNo > dgvAnnot.RowCount - 1 Then
                dgvAnnot.Rows.Add({True, True, "Standard Deviation", 1, Label, SDev1Prob, SDev1Value})
            Else
                dgvAnnot.Rows(RowNo).Cells(0).Value = True
                dgvAnnot.Rows(RowNo).Cells(1).Value = True
                dgvAnnot.Rows(RowNo).Cells(2).Value = "Standard Deviation"
                dgvAnnot.Rows(RowNo).Cells(3).Value = 1
                dgvAnnot.Rows(RowNo).Cells(4).Value = Label
                dgvAnnot.Rows(RowNo).Cells(5).Value = SDev1Prob
                dgvAnnot.Rows(RowNo).Cells(6).Value = SDev1Value
            End If
            DisplayAnnot(SDev1Prob, SDev1Value, Label)
        ElseIf SDevN1 = False Then
            Dim SDevN1Value As Double = Mean - StdDev
            Dim SDevN1Prob As Double = GetProb(SDevN1Value)
            Dim Label As String = "-1" & ChrW(963)
            If RowNo > dgvAnnot.RowCount - 1 Then
                dgvAnnot.Rows.Add({True, True, "Standard Deviation", -1, Label, SDevN1Prob, SDevN1Value})
            Else
                dgvAnnot.Rows(RowNo).Cells(0).Value = True
                dgvAnnot.Rows(RowNo).Cells(1).Value = True
                dgvAnnot.Rows(RowNo).Cells(2).Value = "Standard Deviation"
                dgvAnnot.Rows(RowNo).Cells(3).Value = 1
                dgvAnnot.Rows(RowNo).Cells(4).Value = Label
                dgvAnnot.Rows(RowNo).Cells(5).Value = SDevN1Prob
                dgvAnnot.Rows(RowNo).Cells(6).Value = SDevN1Value
            End If
            DisplayAnnot(SDevN1Prob, SDevN1Value, Label)
        ElseIf SDev2 = False Then
            Dim SDev2Value As Double = Mean + 2 * StdDev
            Dim SDev2Prob As Double = GetProb(SDev2Value)
            Dim Label As String = "2" & ChrW(963)
            If RowNo > dgvAnnot.RowCount - 1 Then
                dgvAnnot.Rows.Add({True, True, "Standard Deviation", 2, Label, SDev2Prob, SDev2Value})
            Else
                dgvAnnot.Rows(RowNo).Cells(0).Value = True
                dgvAnnot.Rows(RowNo).Cells(1).Value = True
                dgvAnnot.Rows(RowNo).Cells(2).Value = "Standard Deviation"
                dgvAnnot.Rows(RowNo).Cells(3).Value = 2
                dgvAnnot.Rows(RowNo).Cells(4).Value = Label
                dgvAnnot.Rows(RowNo).Cells(5).Value = SDev2Prob
                dgvAnnot.Rows(RowNo).Cells(6).Value = SDev2Value
            End If
            DisplayAnnot(SDev2Prob, SDev2Value, Label)
        ElseIf SDevN2 = False Then
            Dim SDevN2Value As Double = Mean - 2 * StdDev
            Dim SDevN2Prob As Double = GetProb(SDevN2Value)
            Dim Label As String = "-2" & ChrW(963)
            If RowNo > dgvAnnot.RowCount - 1 Then
                dgvAnnot.Rows.Add({True, True, "Standard Deviation", -2, Label, SDevN2Prob, SDevN2Value})
            Else
                dgvAnnot.Rows(RowNo).Cells(0).Value = True
                dgvAnnot.Rows(RowNo).Cells(1).Value = True
                dgvAnnot.Rows(RowNo).Cells(2).Value = "Standard Deviation"
                dgvAnnot.Rows(RowNo).Cells(3).Value = -2
                dgvAnnot.Rows(RowNo).Cells(4).Value = Label
                dgvAnnot.Rows(RowNo).Cells(5).Value = SDevN2Prob
                dgvAnnot.Rows(RowNo).Cells(6).Value = SDevN2Value
            End If
            DisplayAnnot(SDevN2Prob, SDevN2Value, Label)
        ElseIf SDev3 = False Then
            Dim SDev3Value As Double = Mean + 3 * StdDev
            Dim SDev3Prob As Double = GetProb(SDev3Value)
            Dim Label As String = "3" & ChrW(963)
            If RowNo > dgvAnnot.RowCount - 1 Then
                dgvAnnot.Rows.Add({True, True, "Standard Deviation", 3, Label, SDev3Prob, SDev3Value})
            Else
                dgvAnnot.Rows(RowNo).Cells(0).Value = True
                dgvAnnot.Rows(RowNo).Cells(1).Value = True
                dgvAnnot.Rows(RowNo).Cells(2).Value = "Standard Deviation"
                dgvAnnot.Rows(RowNo).Cells(3).Value = 3
                dgvAnnot.Rows(RowNo).Cells(4).Value = Label
                dgvAnnot.Rows(RowNo).Cells(5).Value = SDev3Prob
                dgvAnnot.Rows(RowNo).Cells(6).Value = SDev3Value
            End If
            DisplayAnnot(SDev3Prob, SDev3Value, Label)
        ElseIf SDevN3 = False Then
            Dim SDevN3Value As Double = Mean - 3 * StdDev
            Dim SDevN3Prob As Double = GetProb(SDevN3Value)
            Dim Label As String = "-3" & ChrW(963)
            If RowNo > dgvAnnot.RowCount - 1 Then
                dgvAnnot.Rows.Add({True, True, "Standard Deviation", -3, Label, SDevN3Prob, SDevN3Value})
            Else
                dgvAnnot.Rows(RowNo).Cells(0).Value = True
                dgvAnnot.Rows(RowNo).Cells(1).Value = True
                dgvAnnot.Rows(RowNo).Cells(2).Value = "Standard Deviation"
                dgvAnnot.Rows(RowNo).Cells(3).Value = -3
                dgvAnnot.Rows(RowNo).Cells(4).Value = Label
                dgvAnnot.Rows(RowNo).Cells(5).Value = SDevN3Prob
                dgvAnnot.Rows(RowNo).Cells(6).Value = SDevN3Value
            End If
            DisplayAnnot(SDevN3Prob, SDevN3Value, Label)
        Else
            Dim SDev0Value As Double = Mean
            Dim SDev0Prob As Double = GetProb(SDev0Value)
            Dim Label As String = "0" & ChrW(963)
            If RowNo > dgvAnnot.RowCount - 1 Then
                dgvAnnot.Rows.Add({True, True, "Standard Deviation", 0, Label, SDev0Prob, SDev0Value})
            Else
                dgvAnnot.Rows(RowNo).Cells(0).Value = True
                dgvAnnot.Rows(RowNo).Cells(1).Value = True
                dgvAnnot.Rows(RowNo).Cells(2).Value = "Standard Deviation"
                dgvAnnot.Rows(RowNo).Cells(3).Value = 0
                dgvAnnot.Rows(RowNo).Cells(4).Value = Label
                dgvAnnot.Rows(RowNo).Cells(5).Value = SDev0Prob
                dgvAnnot.Rows(RowNo).Cells(6).Value = SDev0Value
            End If
            DisplayAnnot(SDev0Prob, SDev0Value, Label)
        End If

        dgvAnnot.AllowUserToAddRows = True
    End Sub

    Private Sub DisplayAnnot(ByVal Prob As Double, ByVal Value As Double, ByVal Text As String)
        'Display the probability annotation on the Series Analysis charts.

        'Display the annotation on the CDF chart: ========================================================

        'Add the vertical bar:
        Dim CDFPoint As New DataVisualization.Charting.DataPoint
        CDFPoint.XValue = Value
        CDFPoint.SetValueY(Prob)
        Chart1.Series("CdfVertBar").Points.Add(CDFPoint)

        'Add the label:
        Dim Annot As New DataVisualization.Charting.TextAnnotation
        Annot.AxisX = Chart1.ChartAreas(0).AxisX
        Annot.AxisY = Chart1.ChartAreas(0).AxisY
        Annot.AnchorX = Value
        Annot.AnchorY = Prob

        Annot.AnchorAlignment = ContentAlignment.MiddleRight

        Annot.Text = Text
        Annot.Font = New Font("Arial", 10, FontStyle.Regular Or FontStyle.Bold)

        Chart1.Annotations.Add(Annot)

        'Display the annotation on the Histogram: ====================================================

        Dim HistPoint As New DataVisualization.Charting.DataPoint
        HistPoint.XValue = Value
        Dim YValue As Double = GetHistProb(Value)
        HistPoint.SetValueY(YValue)
        Chart1.Series("HistPoints").Points.Add(HistPoint)

    End Sub

    Private Function GetHistProb(ByVal Value As Double, AdjacentBars As Integer) As Double
        'Get the histogram probability corresponding to the value.
        'If AdjacentBars > 0, the maximum value including the specified number of adjacent bars returned.

        If AdjacentBars = 0 Then
            Dim Rows() As DataRow
            Dim Row1 As DataRow
            Dim Row2 As DataRow
            'Dim Row1 As DataRow = Data.Tables("Histogram").Select("Mid_Interval <= '" & Value & "'").Last
            'Dim Row1 As DataRow = DataSource.Data.Tables("Histogram").Select("Mid_Interval <= '" & Value & "'").Last
            'Rows = Main.Distribution.Data.Tables("Histogram").Select("Mid_Interval <= '" & Value & "'")
            'Rows = CalcData.Tables("Histogram").Select("Mid_Interval <= '" & Value & "'")
            Rows = CalcData.Tables("Histogram").Select("Value <= '" & Value & "'") 'UPDATE 15May22 - Use Value instead of Mid_Interval ????
            Row1 = Rows.Last
            'Dim Row2 As DataRow = Data.Tables("Histogram").Select("Mid_Interval >= '" & Value & "'").First
            'Dim Row2 As DataRow = DataSource.Data.Tables("Histogram").Select("Mid_Interval >= '" & Value & "'").First
            'Rows = Main.Distribution.Data.Tables("Histogram").Select("Mid_Interval >= '" & Value & "'")
            'Rows = CalcData.Tables("Histogram").Select("Mid_Interval >= '" & Value & "'")
            Rows = CalcData.Tables("Histogram").Select("Value >= '" & Value & "'") 'UPDATE 15May22 - Use Value instead of Mid_Interval ????
            Row2 = Rows.First
            If Row1.Item("Probability") > Row2.Item("Probability") Then
                Return Row1.Item("Probability")
            Else
                Return Row2.Item("Probability")
            End If
        Else
            'Dim Row1 As IEnumerable(Of DataRow) = Data.Tables("Histogram").Select("Mid_Interval <= '" & Value & "'", "Mid_Interval DESC").Take(AdjacentBars + 1)
            'Dim Row1 As IEnumerable(Of DataRow) = Main.Distribution.Data.Tables("Histogram").Select("Mid_Interval <= '" & Value & "'", "Mid_Interval DESC").Take(AdjacentBars + 1)
            'Dim Row1 As IEnumerable(Of DataRow) = CalcData.Tables("Histogram").Select("Mid_Interval <= '" & Value & "'", "Mid_Interval DESC").Take(AdjacentBars + 1)
            Dim Row1 As IEnumerable(Of DataRow) = CalcData.Tables("Histogram").Select("Value <= '" & Value & "'", "Value DESC").Take(AdjacentBars + 1) 'UPDATE 15May22 - Use Value instead of Mid_Interval ????
            'Dim Row2 As IEnumerable(Of DataRow) = Data.Tables("Histogram").Select("Mid_Interval >= '" & Value & "'", "Mid_Interval ASC").Take(AdjacentBars + 1)
            'Dim Row2 As IEnumerable(Of DataRow) = Main.Distribution.Data.Tables("Histogram").Select("Mid_Interval >= '" & Value & "'", "Mid_Interval ASC").Take(AdjacentBars + 1)
            'Dim Row2 As IEnumerable(Of DataRow) = CalcData.Tables("Histogram").Select("Mid_Interval >= '" & Value & "'", "Mid_Interval ASC").Take(AdjacentBars + 1)
            Dim Row2 As IEnumerable(Of DataRow) = CalcData.Tables("Histogram").Select("Value >= '" & Value & "'", "Value ASC").Take(AdjacentBars + 1) 'UPDATE 15May22 - Use Value instead of Mid_Interval ????
            If Row1(AdjacentBars).Item("Probability") > Row2(AdjacentBars).Item("Probability") Then
                Return Row1(AdjacentBars).Item("Probability")
            Else
                Return Row2(AdjacentBars).Item("Probability")
            End If
        End If
    End Function

    Private Function GetHistProb(ByVal Value As Double) As Double
        'Get the histogram probability corresponding to the value.
        Try
            Dim Rows() As DataRow
            Dim Row1 As DataRow
            Dim Row2 As DataRow
            'Dim Row1 As DataRow = Data.Tables("Histogram").Select("Mid_Interval <= '" & Value & "'").Last
            'Dim Row1 As DataRow = DataSource.Data.Tables("Histogram").Select("Mid_Interval <= '" & Value & "'").Last
            'Rows = Main.Distribution.Data.Tables("Histogram").Select("Mid_Interval <= '" & Value & "'")
            'Rows = CalcData.Tables("Histogram").Select("Mid_Interval <= '" & Value & "'")
            Rows = CalcData.Tables("Histogram").Select("Value <= '" & Value & "'") 'UPDATE 15May22 - Use Value instead of Mid_Interval ????
            Row1 = Rows.Last
            'Dim Row2 As DataRow = Data.Tables("Histogram").Select("Mid_Interval >= '" & Value & "'").First
            'Dim Row2 As DataRow = DataSource.Data.Tables("Histogram").Select("Mid_Interval >= '" & Value & "'").First
            'Rows = Main.Distribution.Data.Tables("Histogram").Select("Mid_Interval >= '" & Value & "'")
            'Rows = CalcData.Tables("Histogram").Select("Mid_Interval >= '" & Value & "'")
            Rows = CalcData.Tables("Histogram").Select("Value >= '" & Value & "'") 'UPDATE 15May22 - Use Value instead of Mid_Interval ????
            Row2 = Rows.First

            'If Math.Abs(Value - Row1.Item("Mid_Interval")) < Math.Abs(Value - Row2.Item("Mid_Interval")) Then 'Row1 is closer to Value
            If Math.Abs(Value - Row1.Item("Value")) < Math.Abs(Value - Row2.Item("Value")) Then 'Row1 is closer to Value 'UPDATE 15May22 - Use Value instead of Mid_Interval ????
                Return Row1.Item("Probability")
            Else 'Row2 is closer to Value
                Return Row2.Item("Probability")
            End If
        Catch ex As Exception
            Main.Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Function


    Private Sub dgvAnnot_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles dgvAnnot.EditingControlShowing

        'If dgvAnnot.CurrentCell.ColumnIndex = 0 Then 'Annotation Type selected
        If dgvAnnot.CurrentCell.ColumnIndex = 2 Then 'Annotation Type selected
            Dim combo As ComboBox = CType(e.Control, ComboBox)
            If (combo IsNot Nothing) Then
                combo.Name = "cboAnnotType"
                'Remove current handler:
                RemoveHandler combo.SelectionChangeCommitted, New EventHandler(AddressOf ComboBox_SelectionChangeCommitted)
                'Add the event handler:
                AddHandler combo.SelectionChangeCommitted, New EventHandler(AddressOf ComboBox_SelectionChangeCommitted)
            End If
        End If
    End Sub

    Private Sub ComboBox_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim combo As ComboBox = CType(sender, ComboBox)

        If combo.Name = "cboAnnotType" Then
            Main.Message.Add("Selected annotation type: " & combo.SelectedItem.ToString & vbCrLf)

            Dim RowNo As Integer = dgvAnnot.SelectedCells(0).RowIndex
            Main.Message.Add("Selected row: " & RowNo & vbCrLf)

            Select Case combo.SelectedItem.ToString
                Case "Probability"
                    AddProbAnnot(RowNo)
                Case "Value"
                    AddValueAnnot(RowNo)
                Case "Mean"
                    AddMeanAnnot(RowNo)
                Case "Standard Deviation"
                    AddStdDevAnnot(RowNo)
                Case Else

            End Select
        Else
            Main.Message.AddWarning("Unknown combo box: " & combo.Name & vbCrLf)
        End If
    End Sub

    Private Sub btnDeleteAnnot_Click(sender As Object, e As EventArgs) Handles btnDeleteAnnot.Click
        'Delete the selected annotation.

        If dgvAnnot.SelectedRows.Count > 0 Then
            dgvAnnot.Rows.RemoveAt(dgvAnnot.SelectedRows(0).Index)
        Else
            Main.Message.AddWarning("Select an annotation entry to delete." & vbCrLf)
        End If
    End Sub

    Private Sub btnUpdateAnnotation_Click(sender As Object, e As EventArgs) Handles btnUpdateAnnotation.Click
        'Update the Annotations.
        UpdateAnnotation()
    End Sub

    Private Sub UpdateAnnotation()
        'Update the Annotations

        Try
            Chart1.Annotations.Clear()
            Chart1.Series("CdfVertBar").Points.Clear()
            Chart1.Series("HistPoints").Points.Clear()

            UpdateAnnotationValues()

            dgvAnnot.AllowUserToAddRows = False
            Dim I As Integer
            For I = 0 To dgvAnnot.RowCount - 1
                If dgvAnnot.Rows(I).Cells(0).Value = True Then 'Display annotation on the CDF chart
                    'Add the vertical bar:
                    Dim CdfPoint As New DataVisualization.Charting.DataPoint
                    CdfPoint.XValue = dgvAnnot.Rows(I).Cells(6).Value
                    CdfPoint.SetValueY(dgvAnnot.Rows(I).Cells(5).Value)
                    Chart1.Series("CdfVertBar").Points.Add(CdfPoint)
                    'Add the label:
                    Dim CdfAnnot As New DataVisualization.Charting.TextAnnotation
                    CdfAnnot.AxisX = Chart1.ChartAreas(0).AxisX
                    CdfAnnot.AxisY = Chart1.ChartAreas(0).AxisY
                    CdfAnnot.AnchorX = dgvAnnot.Rows(I).Cells(6).Value
                    CdfAnnot.AnchorY = dgvAnnot.Rows(I).Cells(5).Value
                    CdfAnnot.AnchorAlignment = ContentAlignment.MiddleRight
                    CdfAnnot.Text = dgvAnnot.Rows(I).Cells(4).Value & " (" & Format(dgvAnnot.Rows(I).Cells(6).Value, dgvAnnot.Rows(I).Cells(7).Value) & ")"
                    CdfAnnot.Font = New Font("Arial", 10, FontStyle.Regular Or FontStyle.Bold)
                    Chart1.Annotations.Add(CdfAnnot)
                End If
                If dgvAnnot.Rows(I).Cells(1).Value = True Then 'Display annotation on the Histogram
                    'Add the circle symbol to the histogram
                    Dim HistPoint As New DataVisualization.Charting.DataPoint
                    HistPoint.XValue = dgvAnnot.Rows(I).Cells(6).Value
                    Dim YValue As Double = GetHistProb(dgvAnnot.Rows(I).Cells(6).Value) / HistIntervalWidth 'This is the Probability Density
                    HistPoint.SetValueY(YValue)
                    Chart1.Series("HistPoints").Points.Add(HistPoint)

                End If
            Next

            dgvAnnot.AllowUserToAddRows = True
        Catch ex As Exception
            Main.Message.AddWarning(ex.Message & vbCrLf)
        End Try

    End Sub

    Private Sub ApplyDefaultSettings()
        'Apply default settings: Default annotations
        'This method will run if a form settings file is not found.

        dgvAnnot.Rows.Add({True, True, "Probability", 0.1, "P10", 0.1, 0})
        dgvAnnot.Rows.Add({True, True, "Probability", 0.5, "P50", 0.5, 0})
        dgvAnnot.Rows.Add({True, True, "Probability", 0.9, "P90", 0.9, 0})

    End Sub

    Private Sub UpdateAnnotationValues()
        'Update the annotation values using the current CDF and Histogram data.

        dgvAnnot.AllowUserToAddRows = False
        Dim I As Integer
        For I = 0 To dgvAnnot.RowCount - 1
            Select Case dgvAnnot.Rows(I).Cells(2).Value
                Case "Probability"
                    dgvAnnot.Rows(I).Cells(5).Value = dgvAnnot.Rows(I).Cells(3).Value
                    dgvAnnot.Rows(I).Cells(6).Value = GetValue(dgvAnnot.Rows(I).Cells(5).Value)
                Case "Value"
                    dgvAnnot.Rows(I).Cells(6).Value = dgvAnnot.Rows(I).Cells(3).Value
                    dgvAnnot.Rows(I).Cells(5).Value = GetProb(dgvAnnot.Rows(I).Cells(6).Value)
                Case "Mean"
                    dgvAnnot.Rows(I).Cells(3).Value = ""
                    dgvAnnot.Rows(I).Cells(6).Value = Val(txtAverage.Text)
                    dgvAnnot.Rows(I).Cells(5).Value = GetProb(txtAverage.Text)
                Case "Standard Deviation"
                    dgvAnnot.Rows(I).Cells(6).Value = txtAverage.Text + dgvAnnot.Rows(I).Cells(3).Value * txtStdDev.Text
                    dgvAnnot.Rows(I).Cells(5).Value = GetProb(dgvAnnot.Rows(I).Cells(6).Value)
                Case Else
                    Main.Message.AddWarning("Unknown annotation type: " & dgvAnnot.Rows(I).Cells(2).Value & vbCrLf)
            End Select
        Next
        dgvAnnot.AllowUserToAddRows = True
    End Sub

    Private Sub frmSeriesAnalysis_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        txtHeight.Text = Me.Height
        txtWidth.Text = Me.Width
    End Sub

    Private Sub frmSeriesAnalysis_Move(sender As Object, e As EventArgs) Handles Me.Move
        txtTop.Text = Me.Top
        txtLeft.Text = Me.Left
    End Sub

    Private Sub btnApplySize_Click(sender As Object, e As EventArgs) Handles btnApplySize.Click
        'Apply the Height and Width settings.

        Dim Height As Integer = Int(txtHeight.Text)
        Dim Width As Integer = Int(txtWidth.Text)
        Dim Top As Integer = Int(txtTop.Text)
        Dim Left As Integer = Int(txtLeft.Text)

        If Height < 200 Then Height = 200 '200 pixels minimum height
        If Width < 200 Then Width = 200 '200 pixels minimum width

        Me.Height = Height
        Me.Width = Width
        Me.Top = Top
        Me.Left = Left
        CheckFormPos()
    End Sub

    Private Sub rbCdf_CheckedChanged(sender As Object, e As EventArgs) Handles rbCdf.CheckedChanged
        If rbCdf.Checked Then _showReverseCDF = False
    End Sub

    Private Sub rbReverseCdf_CheckedChanged(sender As Object, e As EventArgs) Handles rbReverseCdf.CheckedChanged
        If rbReverseCdf.Checked Then _showReverseCDF = True
    End Sub

    Private Sub btnUpdateCharts_Click(sender As Object, e As EventArgs) Handles btnUpdateCharts.Click
        'Update the charts.
        UpdateCharts()
    End Sub

    Private Sub btnFormatHelp2_Click(sender As Object, e As EventArgs) Handles btnFormatHelp2.Click
        'Show Format information.
        MessageBox.Show("Format string examples:" & vbCrLf & "N4 - Number displayed with thousands separator and 4 decimal places" & vbCrLf & "F4 - Number displayed with 4 decimal places.", "Number Formatting")
    End Sub

    Private Sub btnPlot_Click(sender As Object, e As EventArgs) Handles btnPlot.Click
        'UpdateCharts()
        ReplotCharts()
    End Sub

    Private Sub frmSeriesAnalysis_Shown(sender As Object, e As EventArgs) Handles Me.Shown

    End Sub

    Private Sub chkShowModel_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowModel.CheckedChanged

    End Sub

    Private Sub btnUpdateCharts2_Click(sender As Object, e As EventArgs) Handles btnUpdateCharts2.Click
        UpdateCharts()
    End Sub

    Private Sub btnChartTitleFont_Click(sender As Object, e As EventArgs) Handles btnChartTitleFont.Click
        'Edit chart title font
        FontDialog1.Font = txtAddTitle.Font
        FontDialog1.ShowDialog()
        txtAddTitle.Font = FontDialog1.Font
    End Sub

    Private Sub btnChartTitleColor_Click(sender As Object, e As EventArgs) Handles btnChartTitleColor.Click
        ColorDialog1.Color = txtAddTitle.ForeColor
        ColorDialog1.ShowDialog()
        txtAddTitle.ForeColor = ColorDialog1.Color
    End Sub

    Private Sub btnAddTitle_Click(sender As Object, e As EventArgs) Handles btnAddTitle.Click
        'Add a new title to Chart1

        Dim NewTitleNo As Integer = Chart1.Titles.Count
        Dim NewTitleName As String = "Title" & NewTitleNo + 1
        Chart1.Titles.Add(NewTitleName)

        Chart1.Titles(NewTitleNo).Text = txtAddTitle.Text
        Chart1.Titles(NewTitleNo).Font = txtAddTitle.Font
        Chart1.Titles(NewTitleNo).ForeColor = txtAddTitle.ForeColor

    End Sub

    Private Sub btnEditFont_Click(sender As Object, e As EventArgs) Handles btnEditFont.Click
        'Edit chart title font
        FontDialog1.Font = txtEditTitle.Font
        FontDialog1.ShowDialog()
        txtEditTitle.Font = FontDialog1.Font
    End Sub

    Private Sub btnEditColor_Click(sender As Object, e As EventArgs) Handles btnEditColor.Click
        ColorDialog1.Color = txtEditTitle.ForeColor
        ColorDialog1.ShowDialog()
        txtEditTitle.ForeColor = ColorDialog1.Color
    End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged

        Dim TitleNo As Integer = NumericUpDown1.Value - 1

        If TitleNo < 0 Then
            Main.Message.AddWarning("The selected title number is too low." & vbCrLf)
        ElseIf TitleNo > Chart1.Titles.Count - 1 Then
            Main.Message.AddWarning("The selected title number is too high." & vbCrLf)
        Else
            txtEditTitle.Text = Chart1.Titles(TitleNo).Text
            txtEditTitle.Font = Chart1.Titles(TitleNo).Font
            txtEditTitle.ForeColor = Chart1.Titles(TitleNo).ForeColor
        End If
    End Sub

    Private Sub btnEditTitle_Click(sender As Object, e As EventArgs) Handles btnEditTitle.Click

        Dim TitleNo As Integer = NumericUpDown1.Value - 1

        If TitleNo < 0 Then
            Main.Message.AddWarning("The selected title number is too low." & vbCrLf)
        ElseIf TitleNo > Chart1.Titles.Count - 1 Then
            Main.Message.AddWarning("The selected title number is too high." & vbCrLf)
        Else
            Chart1.Titles(TitleNo).Text = txtEditTitle.Text
            Chart1.Titles(TitleNo).Font = txtEditTitle.Font
            Chart1.Titles(TitleNo).ForeColor = txtEditTitle.ForeColor
        End If
    End Sub

    Private Sub cmbTable_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTable.SelectedIndexChanged
        'A Table has been selected.
        DataTableName = cmbTable.SelectedItem.ToString

    End Sub

    Private Sub cmbColumn_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbColumn.SelectedIndexChanged
        'A Column has been selected.
        DataColumnName = cmbColumn.SelectedItem.ToString
        UpdateCharts()
    End Sub

    Public Sub ClearParameters()
        'Clear the Distribution Parameters
        txtParamAName.Text = ""
        txtParamAValue.Text = ""
        txtParamBName.Text = ""
        txtParamBValue.Text = ""
        txtParamCName.Text = ""
        txtParamCValue.Text = ""
        txtParamDName.Text = ""
        txtParamDValue.Text = ""
        txtParamEName.Text = ""
        txtParamEValue.Text = ""
    End Sub


#End Region 'Form Methods ---------------------------------------------------------------------------------------------------------------------------------------------------------------------


    'Form Events ----------------------------------------------------------------------------------------------------------------------------------------------------------------------


End Class