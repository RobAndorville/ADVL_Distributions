Public Class frmGenerateSamples
    'Generate a set of sample values from a selected distribution.

#Region " Variable Declarations - All the variables used in this form and this application." '=================================================================================================

    Public WithEvents Distribution As New DistributionModel 'This class holds the Distribution parameters, data and charts.

    Dim DataSetInfo As New Dictionary(Of String, clsDataSetInfo) 'Dictionary of information about each DataSet in a DataTable.

#End Region 'Variable Declarations ------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Properties - All the properties used in this form and this application" '============================================================================================================

    Private _formNo As Integer = -1 'Multiple instances of this form can be displayed. FormNo is the index number of the form in ChartList.
    'If the form is included in Main.ChartList() then FormNo will be > -1 --> when exiting set Main.ClosedFormNo and call Main.ChartClosed(). 
    Public Property FormNo As Integer
        Get
            Return _formNo
        End Get
        Set(ByVal value As Integer)
            _formNo = value
        End Set
    End Property

    Private _selDistrib As Integer = -1 'The selected distribution number. (-1 if none selected.)
    Property SelDistrib As Integer
        Get
            Return _selDistrib
        End Get
        Set(value As Integer)
            _selDistrib = value
            'txtSelDistrib.Text = _selDistrib
            If _selDistrib = 0 Then
                'ClearForm()
            Else
                'UpdateForm()
            End If
        End Set
    End Property

    Private _modelFileName As String = "" 'The name of the distribution model.
    Property ModelFileName As String
        Get
            Return _modelFileName
        End Get
        Set(value As String)
            _modelFileName = value
        End Set
    End Property

    Private _modelLabel As String 'The Distribution model label.
    Property ModelLabel As String
        Get
            Return _modelLabel
        End Get
        Set(value As String)
            _modelLabel = value
        End Set
    End Property

    Private _randomVariableName As String = "" 'The name of the selected random variable
    Property RandomVariableName As String
        Get
            Return _randomVariableName
        End Get
        Set(value As String)
            _randomVariableName = value
        End Set
    End Property

    Private _modified As Boolean = False 'If True, there are modifications that need to be saved.
    Property Modified As Boolean
        Get
            Return _modified
        End Get
        Set(value As Boolean)
            _modified = value
        End Set
    End Property

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
                               <ModelFileName><%= ModelFileName %></ModelFileName>
                               <DistributionNumber><%= SelDistrib %></DistributionNumber>
                           </FormSettings>

        'Add code to include other settings to save after the comment line <!---->

        'Dim SettingsFileName As String = "FormSettings_" & Main.ApplicationInfo.Name & "_" & Me.Text & ".xml"
        Dim SettingsFileName As String = "FormSettings_" & Main.ApplicationInfo.Name & "_" & Me.Text & "_" & FormNo & ".xml"
        Main.Project.SaveXmlSettings(SettingsFileName, settingsData)
    End Sub

    Private Sub RestoreFormSettings()
        'Read the form settings from an XML document.

        'Dim SettingsFileName As String = "FormSettings_" & Main.ApplicationInfo.Name & "_" & Me.Text & ".xml"
        Dim SettingsFileName As String = "FormSettings_" & Main.ApplicationInfo.Name & "_" & Me.Text & "_" & FormNo & ".xml"

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
            If Settings.<FormSettings>.<ModelFileName>.Value <> Nothing Then cmbModel.SelectedIndex = cmbModel.FindStringExact(Settings.<FormSettings>.<ModelFileName>.Value)
            If Settings.<FormSettings>.<DistributionNumber>.Value <> Nothing Then cmbRandVar.SelectedIndex = Settings.<FormSettings>.<DistributionNumber>.Value

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

        'Fill cmbModel with the list of distribution models
        Dim FilenameList As New ArrayList
        Main.Project.GetDataFileList("Distrib", FilenameList)
        For Each item In FilenameList
            cmbModel.Items.Add(item.ToString)
        Next

        cmbType.Items.Add("Int16")
        cmbType.Items.Add("Int32")
        cmbType.Items.Add("Single")
        cmbType.Items.Add("Double")
        cmbType.SelectedIndex = 2

        cmbSampling.Items.Add("Random")
        cmbSampling.Items.Add("Latin Hypercube")
        cmbSampling.Items.Add("Sorted Latin Hypercube")
        cmbSampling.Items.Add("Median Latin Hypercube")
        cmbSampling.Items.Add("Sorted Median Latin Hypercube")
        cmbSampling.SelectedIndex = 0

        'chkShuffle.Checked = True

        'rbReplace.Checked = True

        txtNDistribSamples.Text = "1000"
        txtFromSamp.Text = "1"
        txtSeed.Text = "-1"

        RestoreFormSettings()   'Restore the form settings

        cmbTableName.Text = "Distribution_Samples"
        cmbColumnName.Text = cmbRandVar.Text.Trim.Replace(" ", "_")

        txtSampSetLabel.Text = cmbColumnName.Text
        txtSampSetUnits.Text = txtRVUnits.Text
        txtSampSetUnitsAbbrev.Text = txtRVUnitsAbbrev.Text
        txtSampSetDescr.Text = txtDescr.Text

        'Save Samples settings:
        rbAllColumns.Checked = True
        txtFileName.Text = Distribution.Label & ".SampleTable"
        txtFileDescr.Text = Distribution.Description

        Modified = False

    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        'Exit the Form
        If FormNo > -1 Then
            Main.ClosedFormNo = FormNo 'The Main form property ClosedFormNo is set to this form number. This is used in the DataInfoFormClosed method to select the correct form to set to nothing.
        End If
        Me.Close() 'Close the form
    End Sub

    Private Sub Form_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If WindowState = FormWindowState.Normal Then
            SaveFormSettings()
        Else
            'Dont save settings if the form is minimised.
        End If
    End Sub

    Private Sub frmSampleSingleValue_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        If FormNo > -1 Then
            Main.GenerateSamplesClosed()
        End If
    End Sub



#End Region 'Form Display Methods -------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Open and Close Forms - Code used to open and close other forms." '===================================================================================================================

#End Region 'Open and Close Forms -------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Form Methods - The main actions performed by this form." '===========================================================================================================================

    Private Sub cmbModel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbModel.SelectedIndexChanged
        'Open the selected distribution model and update the list of distributions.

        If cmbModel.SelectedIndex = -1 Then
            Main.Message.AddWarning("Please select a distribution model." & vbCrLf)
            ModelFileName = ""
        Else
            Distribution.Clear()
            cmbRandVar.Items.Clear()
            txtDescr.Text = ""
            txtDistribType.Text = ""
            txtDistribParams.Text = ""
            txtSampleVal.Text = ""

            Dim XDoc As System.Xml.Linq.XDocument
            Dim FileName As String = cmbModel.SelectedItem.ToString
            ModelFileName = FileName
            Main.Project.ReadXmlData(FileName, XDoc)
            Distribution.FileName = FileName
            Distribution.FromXDoc(XDoc)
            txtModelDescr.Text = Distribution.Description
            ModelLabel = Distribution.Label

            For Each item In Distribution.Info
                If item.RVName = "" Then
                    cmbRandVar.Items.Add(item.Name & " " & item.Suffix)
                Else
                    cmbRandVar.Items.Add(item.RVName)
                End If
            Next
        End If
    End Sub

    Private Sub cmbRandVar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbRandVar.SelectedIndexChanged
        'Open the selected Random Variable.
        SelDistrib = cmbRandVar.SelectedIndex

        If SelDistrib = -1 Then
            'No random variable selected
            RandomVariableName = ""
            txtSampleVal.Text = ""
        Else
            'Update the form:
            txtDescr.Text = Distribution.Info(SelDistrib).RVDescription
            lblUnitsAbbrev.Text = Distribution.Info(SelDistrib).RVAbbrevUnits
            txtDistribType.Text = Distribution.Info(SelDistrib).Name
            txtDistribParams.Text = Distribution.Info(SelDistrib).Suffix
            RandomVariableName = cmbRandVar.SelectedItem.ToString
            txtRVUnits.Text = Distribution.Info(SelDistrib).RVUnits
            txtRVUnitsAbbrev.Text = Distribution.Info(SelDistrib).RVAbbrevUnits
            txtContinuity.Text = Distribution.Info(SelDistrib).Continuity

            'Generate a Sample:
            Dim myRandom As New Random
            'txtSampleVal.Text = Distribution.Info(SelDistrib).InvCDF(myRandom.NextDouble)
            txtSampleVal.Text = Format(Distribution.Info(SelDistrib).InvCDF(myRandom.NextDouble), txtSingleFormat.Text)

            'Update the Multople Samples group:
            Dim ColumnName As String = cmbRandVar.Text.Trim.Replace(" ", "_")
            If cmbColumnName.Items.Contains(ColumnName) Then
                cmbColumnName.SelectedIndex = cmbColumnName.FindStringExact(ColumnName)
            Else
                cmbColumnName.Text = ColumnName
            End If
            txtSampSetLabel.Text = cmbColumnName.Text
            txtSampSetUnits.Text = txtRVUnits.Text
            txtSampSetUnitsAbbrev.Text = txtRVUnitsAbbrev.Text
            txtSampSetDescr.Text = txtDescr.Text

        End If

    End Sub

    Private Sub btnSample_Click(sender As Object, e As EventArgs) Handles btnSample.Click
        'Generate a single sample value
        Dim myRandom As New Random
        'txtSampleVal.Text = Distribution.Info(SelDistrib).InvCDF(myRandom.NextDouble)
        txtSampleVal.Text = Format(Distribution.Info(SelDistrib).InvCDF(myRandom.NextDouble), txtSingleFormat.Text)
    End Sub

    Private Sub btnGenerateSamples_Click(sender As Object, e As EventArgs) Handles btnGenerateSamples.Click
        'Generate the distribution samples.

        Dim TableName As String = cmbTableName.Text.Trim.Replace(" ", "_")
        Dim ColumnName As String = cmbColumnName.Text.Trim.Replace(" ", "_")
        Dim NumberType As String = cmbType.SelectedItem.ToString
        Dim NSamples As Integer = txtNDistribSamples.Text
        Dim FromSamp As Integer = txtFromSamp.Text
        Dim Sampling As String = cmbSampling.SelectedItem.ToString
        Dim Seed As Integer = txtSeed.Text

        Main.Message.Add("Table name: " & TableName & "  Column name: " & ColumnName & vbCrLf)

        If Main.Distribution.Data.Tables.Contains(TableName) Then

        Else
            Main.Distribution.Data.Tables.Add(TableName)
        End If

        If Main.Distribution.Data.Tables(TableName).Columns.Contains(ColumnName) Then

        Else
            Main.Distribution.CreateNewColumn(TableName, ColumnName, NumberType)
            DataSetInfo.Add(ColumnName, New clsDataSetInfo)
        End If

        UpdateTableList()
        cmbTableName.SelectedIndex = cmbTableName.FindStringExact(TableName)

        cmbColumnName.Items.Clear()
        For Each Item In Main.Distribution.Data.Tables(TableName).Columns
            cmbColumnName.Items.Add(Item.ColumnName)
        Next
        cmbColumnName.SelectedIndex = cmbColumnName.FindStringExact(ColumnName)

        Main.DisableGridViews()
        Main.Distribution.GenerateDistribSamples(Distribution.Info(SelDistrib), NSamples, FromSamp, NumberType, Sampling, Seed, ColumnName, TableName)
        Main.EnableGridViews()

        Dim Parameters As String = ""
        If FromSamp = 1 Then 'New parameters
            Parameters = "Samples: " & FromSamp & " to " & FromSamp + NSamples & "; "
            Parameters &= "Distribution: " & Distribution.Info(SelDistrib).Name & "; "
            Parameters &= "Parameters: " & Distribution.Info(SelDistrib).Suffix & "; "
            Parameters &= "Number Type: " & NumberType & "; "
            Parameters &= "Sampling: " & Sampling & "; "
            Parameters &= "Seed: " & Seed & "; "
            DataSetInfo(ColumnName).Composite = False
            DataSetInfo(ColumnName).Continuity = Distribution.Info(SelDistrib).Continuity
            DataSetInfo(ColumnName).DistributionType = Distribution.Info(SelDistrib).Name
            DataSetInfo(ColumnName).NParameters = Distribution.Info(SelDistrib).NParams
            DataSetInfo(ColumnName).ParamAName = Distribution.Info(SelDistrib).ParamA.Name
            DataSetInfo(ColumnName).ParamAValue = Distribution.Info(SelDistrib).ParamA.Value
            DataSetInfo(ColumnName).ParamBName = Distribution.Info(SelDistrib).ParamB.Name
            DataSetInfo(ColumnName).ParamBValue = Distribution.Info(SelDistrib).ParamB.Value
            DataSetInfo(ColumnName).ParamCName = Distribution.Info(SelDistrib).ParamC.Name
            DataSetInfo(ColumnName).ParamCValue = Distribution.Info(SelDistrib).ParamC.Value
            DataSetInfo(ColumnName).ParamDName = Distribution.Info(SelDistrib).ParamD.Name
            DataSetInfo(ColumnName).ParamDValue = Distribution.Info(SelDistrib).ParamD.Value
            DataSetInfo(ColumnName).ParamEName = Distribution.Info(SelDistrib).ParamE.Name
            DataSetInfo(ColumnName).ParamEValue = Distribution.Info(SelDistrib).ParamE.Value
        Else 'Append parameters to existing parameters.
            Parameters = DataSetInfo(ColumnName).Parameters
            Parameters &= "Samples: " & FromSamp & " to " & FromSamp + NSamples & "; "
            Parameters &= "Distribution: " & Distribution.Info(SelDistrib).Name & "; "
            Parameters &= "Parameters: " & Distribution.Info(SelDistrib).Suffix & "; "
            Parameters &= "Number Type: " & NumberType & "; "
            Parameters &= "Sampling: " & Sampling & "; "
            Parameters &= "Seed: " & Seed & "; "
            DataSetInfo(ColumnName).Composite = True 'The DataSet contains additional samples using different distribution parameters - a composite DataSet.
        End If

        txtSampSetParams.Text = Parameters

        DataSetInfo(ColumnName).Label = txtSampSetLabel.Text
        DataSetInfo(ColumnName).Units = txtSampSetUnits.Text
        DataSetInfo(ColumnName).UnitsAbbrev = txtSampSetUnitsAbbrev.Text
        DataSetInfo(ColumnName).Description = txtSampSetDescr.Text
        DataSetInfo(ColumnName).Parameters = Parameters
        DataSetInfo(ColumnName).Sampling = cmbSampling.SelectedItem.ToString
        DataSetInfo(ColumnName).NumberType = cmbType.SelectedItem.ToString
        DataSetInfo(ColumnName).Format = txtFormat.Text
        DataSetInfo(ColumnName).Sampling = Distribution.Info(SelDistrib).Name
    End Sub

    Private Sub UpdateTableList()
        'Update the table list in cmbTableName
        cmbTableName.Items.Clear()
        For Each Item In Main.Distribution.Data.Tables
            If Item.TableName = "Continuous_Data_Table" Then
            Else
                If Item.TableName = "Discrete_Data_Table" Then
                Else
                    cmbTableName.Items.Add(Item.TableName)
                End If
            End If
        Next
    End Sub

    Private Sub btnSeriesAnalysis_Click(sender As Object, e As EventArgs) Handles btnSeriesAnalysis.Click
        'Open a new Series Analysi form

        Dim FormNo As Integer = Main.OpenNewSeriesAnalysisForm
        Dim ColumnName As String = cmbColumnName.Text.Trim.Replace(" ", "_")

        Main.SeriesAnalysisList(FormNo).DistributionName = DataSetInfo(ColumnName).DistributionType
        Main.SeriesAnalysisList(FormNo).Continuity = DataSetInfo(ColumnName).Continuity
        If DataSetInfo(ColumnName).Composite = True Then
            Main.SeriesAnalysisList(FormNo).ShowModel = False
        Else
            Main.SeriesAnalysisList(FormNo).ShowModel = True
        End If
        Main.SeriesAnalysisList(FormNo).ClearParameters
        Dim NParams As Integer = DataSetInfo(ColumnName).NParameters
        Main.SeriesAnalysisList(FormNo).ParamAName = DataSetInfo(ColumnName).ParamAName
        Main.SeriesAnalysisList(FormNo).ParamAValue = DataSetInfo(ColumnName).ParamAValue
        If NParams > 1 Then
            Main.SeriesAnalysisList(FormNo).ParamBName = DataSetInfo(ColumnName).ParamBName
            Main.SeriesAnalysisList(FormNo).ParamBValue = DataSetInfo(ColumnName).ParamBValue
            If NParams > 2 Then
                Main.SeriesAnalysisList(FormNo).ParamCName = DataSetInfo(ColumnName).ParamCName
                Main.SeriesAnalysisList(FormNo).ParamCValue = DataSetInfo(ColumnName).ParamCValue
                If NParams > 3 Then
                    Main.SeriesAnalysisList(FormNo).ParamDName = DataSetInfo(ColumnName).ParamDName
                    Main.SeriesAnalysisList(FormNo).ParamDValue = DataSetInfo(ColumnName).ParamDValue
                    If NParams > 4 Then
                        Main.SeriesAnalysisList(FormNo).ParamEName = DataSetInfo(ColumnName).ParamEName
                        Main.SeriesAnalysisList(FormNo).ParamEValue = DataSetInfo(ColumnName).ParamEValue
                    End If
                End If
            End If
        End If

        'OLD CODE: (The new code uses the distribution information saved in the DataSetInfo() dictionary)
        'Main.SeriesAnalysisList(FormNo).DistributionName = Distribution.Info(SelDistrib).Name
        'Main.SeriesAnalysisList(FormNo).Continuity = Distribution.Info(SelDistrib).Continuity
        'Main.SeriesAnalysisList(FormNo).ShowModel = True
        'Main.SeriesAnalysisList(FormNo).ClearParameters
        'Dim NParams As Integer = Distribution.Info(SelDistrib).NParams
        'Main.SeriesAnalysisList(FormNo).ParamAName = Distribution.Info(SelDistrib).ParamA.Name
        'Main.SeriesAnalysisList(FormNo).ParamAValue = Distribution.Info(SelDistrib).ParamA.Value
        'If NParams > 1 Then
        '    Main.SeriesAnalysisList(FormNo).ParamBName = Distribution.Info(SelDistrib).ParamB.Name
        '    Main.SeriesAnalysisList(FormNo).ParamBValue = Distribution.Info(SelDistrib).ParamB.Value
        '    If NParams > 2 Then
        '        Main.SeriesAnalysisList(FormNo).ParamCName = Distribution.Info(SelDistrib).ParamC.Name
        '        Main.SeriesAnalysisList(FormNo).ParamCValue = Distribution.Info(SelDistrib).ParamC.Value
        '        If NParams > 3 Then
        '            Main.SeriesAnalysisList(FormNo).ParamDName = Distribution.Info(SelDistrib).ParamD.Name
        '            Main.SeriesAnalysisList(FormNo).ParamDValue = Distribution.Info(SelDistrib).ParamD.Value
        '            If NParams > 4 Then
        '                Main.SeriesAnalysisList(FormNo).ParamEName = Distribution.Info(SelDistrib).ParamE.Name
        '                Main.SeriesAnalysisList(FormNo).ParamEValue = Distribution.Info(SelDistrib).ParamE.Value
        '            End If
        '        End If
        '    End If
        'End If

        Main.SeriesAnalysisList(FormNo).DataTableName = cmbTableName.SelectedItem.ToString
        Main.SeriesAnalysisList(FormNo).DataColumnName = cmbColumnName.SelectedItem.ToString
    End Sub

    Private Sub txtNDistribSamples_TextChanged(sender As Object, e As EventArgs) Handles txtNDistribSamples.TextChanged

    End Sub

    Private Sub txtNDistribSamples_LostFocus(sender As Object, e As EventArgs) Handles txtNDistribSamples.LostFocus

    End Sub

    Private Sub txtFromSamp_TextChanged(sender As Object, e As EventArgs) Handles txtFromSamp.TextChanged

    End Sub

    Private Sub txtFromSamp_LostFocus(sender As Object, e As EventArgs) Handles txtFromSamp.LostFocus
        Dim FromSamp As Integer = txtFromSamp.Text
        If FromSamp < 1 Then
            txtFromSamp.Text = 1
        Else
            txtFromSamp.Text = FromSamp
        End If
    End Sub

    Private Sub btnShuffle_Click(sender As Object, e As EventArgs) Handles btnShuffle.Click
        'Shuffle the data in the selected column

        Dim TableName As String = cmbTableName.Text
        Dim ColumnName As String = cmbColumnName.Text
        If Main.Distribution.Data.Tables.Contains(TableName) Then
            If Main.Distribution.Data.Tables(TableName).Columns.Contains(ColumnName) Then
                Main.Distribution.ShuffleColumn(TableName, ColumnName)
            Else
                Main.Message.AddWarning("Column not found: " & ColumnName & vbCrLf)
            End If
        Else
            Main.Message.AddWarning("Table not found: " & TableName & vbCrLf)
        End If
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClearColumn.Click
        'Clear the data in the selected table

        Dim TableName As String = cmbTableName.Text
        If Main.Distribution.Data.Tables.Contains(TableName) Then
            Main.Distribution.Data.Tables(TableName).Rows.Clear()
        Else
            Main.Message.AddWarning("Table not found: " & TableName & vbCrLf)
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        'Save the distribution samples

        If rbAllColumns.Checked = True Then
            SaveAllColumns()
        Else
            SaveCurrentColumn()
        End If





        'Dim SampleSetName As String = txtFileName.Text.Trim
        'Dim Label As String = txtSampSetLabel.Text.Trim
        'Dim Units As String = txtSampSetUnits.Text.Trim
        'Dim UnitsAbbrev As String = txtSampSetUnitsAbbrev.Text.Trim
        'Dim Description As String = txtFileDescr.Text.Trim
        'Dim DistribType As String = txtDistribType.Text.Trim
        'Dim Continuity As String = txtContinuity.Text.Trim
        'Dim Parameters As String = txtDistribParams.Text.Trim
        'Dim NumberType As String = cmbType.Text
        'Dim Sampling As String = cmbSampling.Text


    End Sub

    Private Sub SaveAllColumns()
        'Save all of the generated sample columns.

        Dim XDoc = <?xml version="1.0" encoding="utf-8"?>
                   <!---->
                   <!--Sample Table-->
                   <SampleTable>
                       <TableName><%= cmbTableName.Text %></TableName>
                       <Description><%= txtFileDescr.Text %></Description>
                       <!--DataSet Information List-->
                       <DataSetInfoList>
                           <%= From item In DataSetInfo
                               Select
                               <DataSetInfo>
                                   <ColumnName><%= item.Key %></ColumnName>
                                   <Label><%= item.Value.Label %></Label>
                                   <Description><%= item.Value.Description %></Description>
                                   <Units><%= item.Value.Units %></Units>
                                   <UnitsAbbrev><%= item.Value.UnitsAbbrev %></UnitsAbbrev>
                                   <DistributionType><%= item.Value.DistributionType %></DistributionType>
                                   <Continuity><%= item.Value.Continuity %></Continuity>
                                   <Parameters><%= item.Value.Parameters %></Parameters>
                                   <NumberType><%= item.Value.NumberType %></NumberType>
                                   <Format><%= item.Value.Format %></Format>
                                   <Sampling><%= item.Value.Sampling %></Sampling>
                                   <Composite><%= item.Value.Composite %></Composite>
                                   <NParameters><%= item.Value.NParameters %></NParameters>
                                   <ParamAName><%= item.Value.ParamAName %></ParamAName>
                                   <ParamAValue><%= item.Value.ParamAValue %></ParamAValue>
                                   <ParamBName><%= item.Value.ParamBName %></ParamBName>
                                   <ParamBValue><%= item.Value.ParamBValue %></ParamBValue>
                                   <ParamCName><%= item.Value.ParamCName %></ParamCName>
                                   <ParamCValue><%= item.Value.ParamCValue %></ParamCValue>
                                   <ParamDName><%= item.Value.ParamDName %></ParamDName>
                                   <ParamDValue><%= item.Value.ParamDValue %></ParamDValue>
                                   <ParamEName><%= item.Value.ParamEName %></ParamEName>
                                   <ParamEValue><%= item.Value.ParamEValue %></ParamEValue>
                               </DataSetInfo> %>
                       </DataSetInfoList>
                       <!--Samples-->
                       <Samples>
                           <%= From Row As DataRow In Main.Distribution.Data.Tables(cmbTableName.Text).Rows
                               Select
                               <Row><%= String.Join(", ", Row.ItemArray) %></Row> %>
                       </Samples>
                   </SampleTable>


        '<%= DataToXDoc(cmbTableName.Text).<Samples> %>


        Main.Project.SaveXmlData(txtFileName.Text, XDoc)

    End Sub

    'NOT USED:
    Private Function DataToXDoc(TableName As String) As System.Xml.Linq.XDocument
        'Return the Table Data in an XDocument.
        If Main.Distribution.Data.Tables.Contains(TableName) Then
            'Dim dataRows As System.Xml.Linq.XDocument = Xml.Linq.XDocument.Parse(Main.Distribution.Data.GetXml)
            'Dim dataRows As System.Xml.Linq.XDocument = Xml.Linq.XDocument.Parse(Main.Distribution.Data.Tables(TableName).WriteXml().
            'For Each Row As DataRow In Main.Distribution.Data.Tables(TableName).Rows

            'Next

            'Dim XDoc = <?xml version="1.0" encoding="utf-8"?>
            '           <Samples>
            '               <%= From row As DataRow In Main.Distribution.Data.Tables(TableName).Rows
            '                   Select
            '                   <Row>
            '                       <%= From item In row.ItemArray
            '                           Select
            '                           <C><%= item.Value %></C>
            '                       %>
            '                   </Row> %>
            '           </Samples>


            Dim XDoc = <?xml version="1.0" encoding="utf-8"?>
                       <Samples>
                           <%= From Row As DataRow In Main.Distribution.Data.Tables(TableName).Rows
                               Select
                               <Row><%= Row.ItemArray %></Row> %>
                       </Samples>


        Else

        End If



    End Function

    Private Sub SaveCurrentColumn()
        'Save the current sample column.

        For Each item In DataSetInfo

        Next


    End Sub

    Private Sub btnFormatHelp_Click(sender As Object, e As EventArgs) Handles btnFormatHelp.Click
        'Show Format information.
        MessageBox.Show("Format string examples:" & vbCrLf & "N4 - Number displayed with thousands separator and 4 decimal places" & vbCrLf & "F4 - Number displayed with 4 decimal places.", "Number Formatting")
    End Sub


#End Region 'Form Methods ---------------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Form Events - Events that can be triggered by this form." '==========================================================================================================================

#End Region 'Form Events ----------------------------------------------------------------------------------------------------------------------------------------------------------------------

    Private Class clsDataSetInfo
        'Stores information about a DataSet generated from a distribution.
        'Label
        'Description
        'Units
        'UnitsAbbrev
        'DistributionType
        'Continuity
        'Parameters
        'NumberType
        'Format
        'Sampling
        'Composite
        'NParameters
        'ParamAName
        'ParamAValue
        'ParamBName
        'ParamBValue
        'ParamCName
        'ParamCValue
        'ParamDName
        'ParamDValue
        'ParamEName
        'ParamEValue

        Private _label As String = "" 'A label for the DataSet.
        Property Label As String
            Get
                Return _label
            End Get
            Set(value As String)
                _label = value
            End Set
        End Property


        Private _description As String = "" 'A description of the DataSet.
        Property Description As String
            Get
                Return _description
            End Get
            Set(value As String)
                _description = value
            End Set
        End Property

        Private _units As String = "" 'The measurement units used for the DataSet samples.
        Property Units As String
            Get
                Return _units
            End Get
            Set(value As String)
                _units = value
            End Set
        End Property

        Private _unitsAbbrev As String = "" 'The measurement units abbreviation.
        Property UnitsAbbrev As String
            Get
                Return _unitsAbbrev
            End Get
            Set(value As String)
                _unitsAbbrev = value
            End Set
        End Property

        Private _distributionType As String = "" 'The type of distribution used to generate the samples.
        Property DistributionType As String
            Get
                Return _distributionType
            End Get
            Set(value As String)
                _distributionType = value
            End Set
        End Property

        Private _continuity As String = "" 'The continuity of the distribution (Continuous or Discrete)
        Property Continuity As String
            Get
                Return _continuity
            End Get
            Set(value As String)
                _continuity = value
            End Set
        End Property

        Private _parameters As String = "" 'A summary of the parameters used to generate the samples.
        Property Parameters As String
            Get
                Return _parameters
            End Get
            Set(value As String)
                _parameters = value
            End Set
        End Property

        Private _numberType As String = "" 'The number type used to store the sample values. (Single, Double
        Property NumberType As String
            Get
                Return _numberType
            End Get
            Set(value As String)
                _numberType = value
            End Set
        End Property

        Private _format As String = "" 'The format string used to display the number.
        Property Format
            Get
                Return _format
            End Get
            Set(value)
                _format = value
            End Set
        End Property

        Private _sampling As String = "" 'The type of sampling used to generate the samples. (Random, Latin Hypercube, Median Latin Hypercube, Sorted Latin Hypercube, Sorted Median Latin Hypercube)
        Property Sampling As String
            Get
                Return _sampling
            End Get
            Set(value As String)
                _sampling = value
            End Set
        End Property

        Private _composite As Boolean = False 'If True, the samples are a composite of multiple distributions.
        Property Composite As Boolean
            Get
                Return _composite
            End Get
            Set(value As Boolean)
                _composite = value
            End Set
        End Property

        Private _nParameters As Integer = 1 'The number of parameters in the distribution.
        Property NParameters As Integer
            Get
                Return _nParameters
            End Get
            Set(value As Integer)
                _nParameters = value
            End Set
        End Property

        Private _paramAName As String = "" 'The name of distribution Parameter A. For a composite distribution, this is Parameter A of the first distribution used.
        Property ParamAName As String
            Get
                Return _paramAName
            End Get
            Set(value As String)
                _paramAName = value
            End Set
        End Property

        Private _paramAValue As Double 'THe value of distribution Parameter A. For a composite distribution, this is Parameter A of the first distribution used.
        Property ParamAValue As Double
            Get
                Return _paramAValue
            End Get
            Set(value As Double)
                _paramAValue = value
            End Set
        End Property

        Private _paramBName As String = "" 'The name of distribution Parameter A. For a composite distribution, this is Parameter A of the first distribution used.
        Property ParamBName As String
            Get
                Return _paramBName
            End Get
            Set(value As String)
                _paramBName = value
            End Set
        End Property

        Private _paramBValue As Double 'THe value of distribution Parameter A. For a composite distribution, this is Parameter A of the first distribution used.
        Property ParamBValue As Double
            Get
                Return _paramBValue
            End Get
            Set(value As Double)
                _paramBValue = value
            End Set
        End Property

        Private _paramCName As String = "" 'The name of distribution Parameter A. For a composite distribution, this is Parameter A of the first distribution used.
        Property ParamCName As String
            Get
                Return _paramCName
            End Get
            Set(value As String)
                _paramCName = value
            End Set
        End Property

        Private _paramCValue As Double 'THe value of distribution Parameter A. For a composite distribution, this is Parameter A of the first distribution used.
        Property ParamCValue As Double
            Get
                Return _paramCValue
            End Get
            Set(value As Double)
                _paramCValue = value
            End Set
        End Property

        Private _paramDName As String = "" 'The name of distribution Parameter A. For a composite distribution, this is Parameter A of the first distribution used.
        Property ParamDName As String
            Get
                Return _paramDName
            End Get
            Set(value As String)
                _paramDName = value
            End Set
        End Property

        Private _paramDValue As Double 'THe value of distribution Parameter A. For a composite distribution, this is Parameter A of the first distribution used.
        Property ParamDValue As Double
            Get
                Return _paramDValue
            End Get
            Set(value As Double)
                _paramDValue = value
            End Set
        End Property

        Private _paramEName As String = "" 'The name of distribution Parameter A. For a composite distribution, this is Parameter A of the first distribution used.
        Property ParamEName As String
            Get
                Return _paramEName
            End Get
            Set(value As String)
                _paramEName = value
            End Set
        End Property

        Private _paramEValue As Double 'THe value of distribution Parameter A. For a composite distribution, this is Parameter A of the first distribution used.
        Property ParamEValue As Double
            Get
                Return _paramEValue
            End Get
            Set(value As Double)
                _paramEValue = value
            End Set
        End Property
    End Class

End Class

