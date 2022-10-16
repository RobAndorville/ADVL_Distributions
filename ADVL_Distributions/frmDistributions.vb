Public Class frmDistributions
    'This form is used to select a statistical distribution.

#Region " Variable Declarations - All the variables used in this form and this application." '=================================================================================================

    Dim DistList As Xml.Linq.XDocument 'A list of distributions
    Dim DistInfo As IEnumerable(Of XElement) 'Information about the selected distribution

    Dim ParamA As Double
    Dim ParamB As Double
    Dim ParamC As Double
    Dim ParamD As Double
    Dim ParamE As Double

    Public Distribution As DistributionModel 'This is a reference to the Distribution object in the parent form.

#End Region 'Variable Declarations ------------------------------------------------------------------------------------------------------------------------------------------------------------

#Region " Properties - All the properties used in this form and this application" '============================================================================================================

    Private _distributionType As String = "Continuous 2 parameter" 'The type of distribution (Continuous 2 parameter, Continuous 3 parameter, Continuous 4 parameter, Discrete 1 parameter, Discrete 2 parameter).
    Property DistributionType As String
        Get
            Return _distributionType
        End Get
        Set(value As String)
            _distributionType = value
            Select Case _distributionType
                Case "Continuous 1 parameter"
                    rbContinuous1.Checked = True
                    IsDiscreteDistribution = False
                Case "Continuous 2 parameter"
                    rbContinuous2.Checked = True
                    IsDiscreteDistribution = False
                Case "Continuous 3 parameter"
                    rbContinuous3.Checked = True
                    IsDiscreteDistribution = False
                Case "Continuous 4 parameter"
                    rbContinuous4.Checked = True
                    IsDiscreteDistribution = False
                Case "Continuous 5 parameter"
                    rbContinuous5.Checked = True
                    IsDiscreteDistribution = False
                Case "Discrete 1 parameter"
                    rbDiscrete1.Checked = True
                    IsDiscreteDistribution = True
                Case "Discrete 2 parameter"
                    rbDiscrete2.Checked = True
                    IsDiscreteDistribution = True
                Case "Discrete 3 parameter"
                    rbDiscrete3.Checked = True
                    IsDiscreteDistribution = True
                Case "Special Function"
                    rbSpecial.Checked = True
                Case Else
                    Main.Message.AddWarning("Unknown distribution type: " & _distributionType & vbCrLf)
            End Select
        End Set
    End Property

    Private _isDiscreteDistribution As Boolean = False 'If True, the selected distribution is discrete.
    Property IsDiscreteDistribution As Boolean
        Get
            Return _isDiscreteDistribution
        End Get
        Set(value As Boolean)
            _isDiscreteDistribution = value
            'If _isDiscreteDistribution Then
            '    GroupBox9.Enabled = True
            'Else
            '    GroupBox9.Enabled = False
            'End If
        End Set
    End Property

    Private _distributionName As String = "" 'The name of the selected distribution.
    Property DistributionName As String
        Get
            Return _distributionName
        End Get
        Set(value As String)
            _distributionName = value
            'txtDistributionName.Text = _distributionName
            ShowDistributionInfo(_distributionName)
        End Set
    End Property

    Private _nDistribParams As Integer 'The number of parameters in the selected distribution.
    Property NDistribParams As Integer
        Get
            Return _nDistribParams
        End Get
        Set(value As Integer)
            _nDistribParams = value
        End Set
    End Property

    'Private _functionType As String = "PDF" 'The distribution Function Type: PDF = probability density function, PDFLn = natural logarithm of the PDF, CDF = cumulative density function, InvCDF = inverse CDF.
    'Property FunctionType As String
    '    Get
    '        Return _functionType
    '    End Get
    '    Set(value As String)
    '        _functionType = value
    '        UpdateFunctionTypeInfo()


    '    End Set
    'End Property

    'Private _dataChanged As Boolean = False 'If True, the data has changed and should be saved.
    'Property DataChanged As Boolean
    '    Get
    '        Return _dataChanged
    '    End Get
    '    Set(value As Boolean)
    '        _dataChanged = value
    '    End Set
    'End Property

    'Private _samplingChanged As Boolean = False 'If True, the X Axis sampling settings has been changed and the samples should be re-generated.
    'Property SamplingChanged As Boolean
    '    Get
    '        Return _samplingChanged
    '    End Get
    '    Set(value As Boolean)
    '        _samplingChanged = value
    '    End Set
    'End Property

    'Private _destinationTableMode As String = "Add" 'The Destination Table Mode: Add new field or Update existing field. (Add, Update)
    'Property DestinationTableMode As String
    '    Get
    '        Return _destinationTableMode
    '    End Get
    '    Set(value As String)
    '        _destinationTableMode = value
    '        If _destinationTableMode = "Add" Then
    '            rbAddDistField.Checked = True
    '        ElseIf _destinationTableMode = "Update" Then
    '            rbUpdateDistField.Checked = True
    '        Else
    '            Main.Message.AddWarning("Unknown Destiation Table Mode: " & _destinationTableMode & vbCrLf)
    '        End If
    '    End Set
    'End Property

#End Region 'Properties -----------------------------------------------------------------------------------------------------------------------------------------------------------------------

#Region " Process XML files - Read and write XML files." '=====================================================================================================================================

    Private Sub SaveFormSettings()
        'Save the form settings in an XML document.

        'Select the dfirst item in each combo box if there is no selection:
        If cmbCont1.SelectedIndex = -1 Then cmbCont1.SelectedIndex = 0
        If cmbCont2.SelectedIndex = -1 Then cmbCont2.SelectedIndex = 0
        If cmbCont3.SelectedIndex = -1 Then cmbCont3.SelectedIndex = 0
        If cmbCont4.SelectedIndex = -1 Then cmbCont4.SelectedIndex = 0
        If cmbCont5.SelectedIndex = -1 Then cmbCont5.SelectedIndex = 0
        If cmbDisc1.SelectedIndex = -1 Then cmbDisc1.SelectedIndex = 0
        If cmbDisc2.SelectedIndex = -1 Then cmbDisc2.SelectedIndex = 0
        If cmbDisc3.SelectedIndex = -1 Then cmbDisc3.SelectedIndex = 0
        'If cmbSpecial.SelectedIndex = -1 Then cmbSpecial.SelectedIndex = 0
        If cmbAllDistributions.SelectedIndex = -1 Then cmbAllDistributions.SelectedIndex = 0
        Dim settingsData = <?xml version="1.0" encoding="utf-8"?>
                           <!---->
                           <FormSettings>
                               <Left><%= Me.Left %></Left>
                               <Top><%= Me.Top %></Top>
                               <Width><%= Me.Width %></Width>
                               <Height><%= Me.Height %></Height>
                               <!---->
                               <DistributionType><%= DistributionType %></DistributionType>
                               <Continuous1Distribution><%= cmbCont1.SelectedItem.ToString %></Continuous1Distribution>
                               <Continuous2Distribution><%= cmbCont2.SelectedItem.ToString %></Continuous2Distribution>
                               <Continuous3Distribution><%= cmbCont3.SelectedItem.ToString %></Continuous3Distribution>
                               <Continuous4Distribution><%= cmbCont4.SelectedItem.ToString %></Continuous4Distribution>
                               <Continuous5Distribution><%= cmbCont5.SelectedItem.ToString %></Continuous5Distribution>
                               <Discrete1Distribution><%= cmbDisc1.SelectedItem.ToString %></Discrete1Distribution>
                               <Discrete2Distribution><%= cmbDisc2.SelectedItem.ToString %></Discrete2Distribution>
                               <Discrete3Distribution><%= cmbDisc3.SelectedItem.ToString %></Discrete3Distribution>
                               <AllDistributions><%= cmbAllDistributions.SelectedItem.ToString %></AllDistributions>
                               <DistributionName><%= DistributionName %></DistributionName>
                               <ShowContinuousDistributions><%= chkContinuous.Checked %></ShowContinuousDistributions>
                               <ShowDiscreteDistributions><%= chkDiscrete.Checked %></ShowDiscreteDistributions>
                               <CalcFormat><%= txtFormat.Text %></CalcFormat>
                           </FormSettings>

        '<SpecialFunction><%= cmbSpecial.SelectedItem.ToString %></SpecialFunction>

        '<FunctionType><%= FunctionType %></FunctionType>

        '<DestinationTableMode><%= DestinationTableMode %></DestinationTableMode>
        '<!---->
        '<LockMinimum><%= chkLockSampMin.Checked %></LockMinimum>
        '<LockMaximum><%= chkLockSampMax.Checked %></LockMaximum>
        '<LockSampleInt><%= chkLockSampInt.Checked %></LockSampleInt>
        '<LockNSamples><%= chkLockNSamples.Checked %></LockNSamples>

        '<SamplingType><%= SamplingType %></SamplingType>

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
            If Settings.<FormSettings>.<DistributionType>.Value <> Nothing Then DistributionType = Settings.<FormSettings>.<DistributionType>.Value
            If Settings.<FormSettings>.<Continuous1Distribution>.Value <> Nothing Then cmbCont1.SelectedIndex = cmbCont1.FindStringExact(Settings.<FormSettings>.<Continuous1Distribution>.Value)
            If Settings.<FormSettings>.<Continuous2Distribution>.Value <> Nothing Then cmbCont2.SelectedIndex = cmbCont2.FindStringExact(Settings.<FormSettings>.<Continuous2Distribution>.Value)
            If Settings.<FormSettings>.<Continuous3Distribution>.Value <> Nothing Then cmbCont3.SelectedIndex = cmbCont3.FindStringExact(Settings.<FormSettings>.<Continuous3Distribution>.Value)
            If Settings.<FormSettings>.<Continuous4Distribution>.Value <> Nothing Then cmbCont4.SelectedIndex = cmbCont4.FindStringExact(Settings.<FormSettings>.<Continuous4Distribution>.Value)
            If Settings.<FormSettings>.<Continuous5Distribution>.Value <> Nothing Then cmbCont5.SelectedIndex = cmbCont5.FindStringExact(Settings.<FormSettings>.<Continuous5Distribution>.Value)
            If Settings.<FormSettings>.<Discrete1Distribution>.Value <> Nothing Then cmbDisc1.SelectedIndex = cmbDisc1.FindStringExact(Settings.<FormSettings>.<Discrete1Distribution>.Value)
            If Settings.<FormSettings>.<Discrete2Distribution>.Value <> Nothing Then cmbDisc2.SelectedIndex = cmbDisc2.FindStringExact(Settings.<FormSettings>.<Discrete2Distribution>.Value)
            If Settings.<FormSettings>.<Discrete3Distribution>.Value <> Nothing Then cmbDisc3.SelectedIndex = cmbDisc3.FindStringExact(Settings.<FormSettings>.<Discrete3Distribution>.Value)
            'If Settings.<FormSettings>.<SpecialFunction>.Value <> Nothing Then cmbSpecial.SelectedIndex = cmbSpecial.FindStringExact(Settings.<FormSettings>.<SpecialFunction>.Value)
            If Settings.<FormSettings>.<AllDistributions>.Value <> Nothing Then cmbAllDistributions.SelectedIndex = cmbAllDistributions.FindStringExact(Settings.<FormSettings>.<AllDistributions>.Value)

            'If Settings.<FormSettings>.<FunctionType>.Value <> Nothing Then FunctionType = Settings.<FormSettings>.<FunctionType>.Value
            'If Settings.<FormSettings>.<DestinationTableMode>.Value <> Nothing Then DestinationTableMode = Settings.<FormSettings>.<DestinationTableMode>.Value
            If Settings.<FormSettings>.<DistributionName>.Value <> Nothing Then DistributionName = Settings.<FormSettings>.<DistributionName>.Value

            'If Settings.<FormSettings>.<LockMinimum>.Value <> Nothing Then chkLockSampMin.Checked = Settings.<FormSettings>.<LockMinimum>.Value
            'If Settings.<FormSettings>.<LockMaximum>.Value <> Nothing Then chkLockSampMax.Checked = Settings.<FormSettings>.<LockMaximum>.Value
            'If Settings.<FormSettings>.<LockSampleInt>.Value <> Nothing Then chkLockSampInt.Checked = Settings.<FormSettings>.<LockSampleInt>.Value
            'If Settings.<FormSettings>.<LockNSamples>.Value <> Nothing Then chkLockNSamples.Checked = Settings.<FormSettings>.<LockNSamples>.Value

            If Settings.<FormSettings>.<ShowContinuousDistributions>.Value <> Nothing Then chkContinuous.Checked = Settings.<FormSettings>.<ShowContinuousDistributions>.Value Else chkContinuous.Checked = True
            If Settings.<FormSettings>.<ShowDiscreteDistributions>.Value <> Nothing Then chkDiscrete.Checked = Settings.<FormSettings>.<ShowDiscreteDistributions>.Value Else chkDiscrete.Checked = True

            If Settings.<FormSettings>.<CalcFormat>.Value <> Nothing Then txtFormat.Text = Settings.<FormSettings>.<CalcFormat>.Value

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


        'cmbDataSource.Items.Add("Input Data")
        'cmbDataSource.Items.Add("Processed Data")
        'cmbDataSource.Items.Add("Distribution Data")
        'cmbDataSource.SelectedIndex = 0

        'cmbDestinationTable.Items.Add("Input Data")
        'cmbDestinationTable.Items.Add("Processed Data")
        'cmbDestinationTable.Items.Add("Distribution Data")
        'cmbDestinationTable.SelectedIndex = 0

        'rbPDF.Checked = True

        'txtMinValue.Text = 0
        'txtMaxValue.Text = 1
        'txtSampleInt.Text = 0.1
        'txtNSamples.Text = 10

        'txtXAxisLabel.Text = "X_Axis"
        'txtXAxisUnits.Text = ""
        'txtXAxisDescription.Text = "X Axis"

        Dim cboAlignment As New DataGridViewComboBoxColumn 'Used for selecting the Field alignment
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

        'dgvInfo.ColumnCount = 7
        'dgvInfo.Columns(0).HeaderText = "Field Name"
        'dgvInfo.Columns(1).HeaderText = "Col No"
        'dgvInfo.Columns(2).HeaderText = "Type"
        'dgvInfo.Columns(3).HeaderText = "Format"
        'dgvInfo.Columns.Insert(4, cboAlignment)
        'dgvInfo.Columns(4).HeaderText = "Alignment"
        'dgvInfo.Columns(5).HeaderText = "Units"
        'dgvInfo.Columns(6).HeaderText = "Label"
        'dgvInfo.Columns(7).HeaderText = "Description"
        'dgvInfo.AllowUserToAddRows = False

        'rbAddDistField.Checked = True 'Default selection


        GenerateDistInfo() 'Populate the DistList XDoc with the distribution information.
        ApplyDistInfo()  'Apply the Distribution List Information to the combo boxes.

        'By default, lock the sample minimum, maximum and no. of samples:
        'chkLockSampMin.Checked = True
        'chkLockSampMax.Checked = True
        'chkLockNSamples.Checked = True

        'XmlHtmDisplay1.WordWrap = False

        'XmlHtmDisplay1.Settings.ClearAllTextTypes()
        ''Default message display settings:
        'XmlHtmDisplay1.Settings.AddNewTextType("Warning")
        'XmlHtmDisplay1.Settings.TextType("Warning").FontName = "Arial"
        'XmlHtmDisplay1.Settings.TextType("Warning").Bold = True
        'XmlHtmDisplay1.Settings.TextType("Warning").Color = Color.Red
        'XmlHtmDisplay1.Settings.TextType("Warning").PointSize = 12

        'XmlHtmDisplay1.Settings.AddNewTextType("Default")
        'XmlHtmDisplay1.Settings.TextType("Default").FontName = "Arial"
        'XmlHtmDisplay1.Settings.TextType("Default").Bold = False
        'XmlHtmDisplay1.Settings.TextType("Default").Color = Color.Black
        'XmlHtmDisplay1.Settings.TextType("Default").PointSize = 10

        'XmlHtmDisplay1.Settings.UpdateFontIndexes()
        'XmlHtmDisplay1.Settings.UpdateColorIndexes()

        RestoreFormSettings()   'Restore the form settings

        ''Restore the Distribution Data Info:
        'txtDistributionDataTableName.Text = Main.Distribution.DataTableFileName
        'txtDistributionDataName.Text = Main.Distribution.Name
        'txtDistributionDataDesc.Text = Main.Distribution.Description

        'ShowDistributionDataInfo() 'This shows the data fields in the Information tab.


    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        'Exit the Form

        'If DataChanged Then
        '    Dim Result As DialogResult = MessageBox.Show("Do you want to save the changes in the current Data Table?", "Warning", MessageBoxButtons.YesNoCancel)
        '    If Result = DialogResult.Yes Then
        '        SaveDataTable()
        '    ElseIf Result = DialogResult.Cancel Then
        '        Exit Sub
        '    End If
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



#End Region 'Form Display Methods -------------------------------------------------------------------------------------------------------------------------------------------------------------

#Region " Open and Close Forms - Code used to open and close other forms." '===================================================================================================================

#End Region 'Open and Close Forms -------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Form Methods - The main actions performed by this form." '===========================================================================================================================

    Private Sub GenerateDistInfo()
        'Populate the DistList XDoc with the distribution information.
        DistList = <?xml version="1.0" encoding="utf-8"?>
                   <DistributionList>
                       <!--The Bernoulli Distribution-->
                       <Distribution>
                           <Name>Bernoulli</Name>
                           <Description>A discrete probability distribution of a random variable that takes the value of 1 with probability p and the value of 0 with probability q = 1 - p. </Description>
                           <Usage>Used in medicine to design and analyse clinical trials. A bernoulli distribution is used to model a person experiencing an event such as death, a disease or disease exposure.</Usage>
                           <InformationLink>https://en.wikipedia.org/wiki/Bernoulli_distribution</InformationLink>
                           <InformationLink>https://numerics.mathdotnet.com/api/MathNet.Numerics.Distributions/Bernoulli.htm</InformationLink>
                           <InformationLink>https://mathworld.wolfram.com/BernoulliDistribution.html</InformationLink>
                           <InformationLink>https://www.statisticshowto.com/bernoulli-distribution/</InformationLink>
                           <Continuity>Discrete</Continuity>
                           <NRandomVariables>Univariate</NRandomVariables>
                           <XValueRange>
                               <Minimum>0</Minimum>
                               <Maximum>1</Maximum>
                               <DefaultMin>0</DefaultMin>
                               <DefaultMax>1</DefaultMax>
                           </XValueRange>
                           <NParameters>1</NParameters>
                           <Default>
                               <ParameterA>
                                   <Type>Probability</Type>
                                   <Name>P success</Name>
                                   <Symbol>p</Symbol>
                                   <Abbreviation>p</Abbreviation>
                                   <Description>Probability of generating 1</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>0</Minimum>
                                   <Maximum>1</Maximum>
                                   <Default>0.5</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>1</AdjustMax>
                                   <Increment>0.1</Increment>
                               </ParameterA>
                           </Default>
                           <NAlternatePrameters>0</NAlternatePrameters>
                           <Types>
                               <PMF>
                                   <XAxis>
                                       <Description>Number of successes</Description>
                                       <Label>k</Label>
                                       <Units>Successes</Units>
                                   </XAxis>
                               </PMF>
                               <PMFLn>
                                   <XAxis>
                                       <Description>Number of successes</Description>
                                       <Label>k</Label>
                                       <Units>Successes</Units>
                                   </XAxis>
                               </PMFLn>
                               <CDF>
                                   <XAxis>
                                       <Description>Number of successes</Description>
                                       <Label>x</Label>
                                       <Units>Successes</Units>
                                   </XAxis>
                               </CDF>
                           </Types>
                       </Distribution>
                       <!--The Beta Distribution-->
                       <Distribution>
                           <Name>Beta</Name>
                           <Description>A continuous probability distribution with two parameters. Defined on the interval 0 to 1 inclusive. Used to model a probability distribution of probabilities.</Description>
                           <Usage>Can be used to analyse sport statistics such as baseball batting averages.</Usage>
                           <InformationLink>https://en.wikipedia.org/wiki/Beta_distribution</InformationLink>
                           <InformationLink>https://numerics.mathdotnet.com/api/MathNet.Numerics.Distributions/Beta.htm</InformationLink>
                           <InformationLink>https://mathworld.wolfram.com/BetaDistribution.html</InformationLink>
                           <InformationLink>https://towardsdatascience.com/beta-distribution-intuition-examples-and-derivation-cf00f4db57af</InformationLink>
                           <InformationLink>https://www.itl.nist.gov/div898/handbook/eda/section3/eda366h.htm</InformationLink>
                           <InformationLink>http://varianceexplained.org/statistics/beta_distribution_and_baseball/</InformationLink>
                           <Continuity>Continuous</Continuity>
                           <NRandomVariables>Univariate</NRandomVariables>
                           <XValueRange>
                               <Minimum>0</Minimum>
                               <Maximum>1</Maximum>
                               <DefaultMin>0</DefaultMin>
                               <DefaultMax>1</DefaultMax>
                           </XValueRange>
                           <NParameters>2</NParameters>
                           <Default>
                               <ParameterA>
                                   <Type>Shape</Type>
                                   <Name>alpha</Name>
                                   <Symbol>Alpha</Symbol>
                                   <Abbreviation>a</Abbreviation>
                                   <Description>A shape parameter.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt 0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>2</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.1</Increment>
                               </ParameterA>
                               <ParameterB>
                                   <Type>Shape</Type>
                                   <Name>beta</Name>
                                   <Symbol>Beta</Symbol>
                                   <Abbreviation>b</Abbreviation>
                                   <Description>A shape parameter.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt 0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>5</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>1</Increment>
                               </ParameterB>
                           </Default>
                           <NAlternateParameterSets>0</NAlternateParameterSets>
                           <Types>
                               <PDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDF>
                               <PDFLn>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDFLn>
                               <CDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </CDF>
                               <InvCDF>
                                   <XAxis>
                                       <Description>Probability</Description>
                                       <Label>p</Label>
                                   </XAxis>
                               </InvCDF>
                           </Types>
                       </Distribution>
                       <!--The Beta Scaled Distribution-->
                       <Distribution>
                           <Name>Beta Scaled</Name>
                           <Description>A version of the Beta distribution with an additional two parameters that extend the range of the distribution beyond the 0 to 1 range of the Beta distribution.</Description>
                           <Usage></Usage>
                           <InformationLink>https://en.wikipedia.org/wiki/Beta_distribution</InformationLink>
                           <InformationLink>https://numerics.mathdotnet.com/api/MathNet.Numerics.Distributions/BetaScaled.htm</InformationLink>
                           <Continuity>Continuous</Continuity>
                           <NRandomVariables>Univariate</NRandomVariables>
                           <XValueRange>
                               <DefaultMin>0</DefaultMin>
                               <DefaultMax>1</DefaultMax>
                           </XValueRange>
                           <NParameters>4</NParameters>
                           <Default>
                               <ParameterA>
                                   <Type>Shape</Type>
                                   <Name>alpha</Name>
                                   <Symbol>Alpha</Symbol>
                                   <Abbreviation>a</Abbreviation>
                                   <Description>A shape parameter.</Description>
                                   <NumberType>Real</NumberType>
                                   <!--<Minimum>gt 0</Minimum>-->
                                   <!--<Minimum>gt 1</Minimum>-->
                                   <Minimum>gt 0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>1</Default>
                                   <!--<AdjustMin>0</AdjustMin>-->
                                   <!--<AdjustMin>1</AdjustMin>-->
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterA>
                               <ParameterB>
                                   <Type>Shape</Type>
                                   <Name>beta</Name>
                                   <Symbol>Beta</Symbol>
                                   <Abbreviation>b</Abbreviation>
                                   <Description>A shape parameter.</Description>
                                   <NumberType>Real</NumberType>
                                   <!--<Minimum>gt 0</Minimum>-->
                                   <!--<Minimum>gt 1</Minimum>-->
                                   <Minimum>gt 0</Minimum>
                                   <Default>1</Default>
                                   <!--<AdjustMin>0</AdjustMin>-->
                                   <!--<AdjustMin>1</AdjustMin>-->
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterB>
                               <ParameterC>
                                   <Type>Location</Type>
                                   <Name>mu</Name>
                                   <Symbol>Mu</Symbol>
                                   <Abbreviation>location</Abbreviation>
                                   <Description>The location of the distribution.</Description>
                                   <NumberType>Real</NumberType>
                                   <Default>1</Default>
                                   <AdjustMin>-10</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>1</Increment>
                               </ParameterC>
                               <ParameterD>
                                   <Type>Scale</Type>
                                   <Name>sigma</Name>
                                   <Symbol>Sigma</Symbol>
                                   <Abbreviation>scale</Abbreviation>
                                   <Description>The scale of the distribution.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt 0</Minimum>
                                   <Default>1</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterD>
                           </Default>
                           <NAlternateParameterSets>0</NAlternateParameterSets>
                           <Types>
                               <PDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDF>
                               <PDFLn>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDFLn>
                               <CDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </CDF>
                               <InvCDF>
                                   <XAxis>
                                       <Description>Probability</Description>
                                       <Label>p</Label>
                                   </XAxis>
                               </InvCDF>
                           </Types>
                       </Distribution>
                       <!--The Binomial Distribution-->
                       <Distribution>
                           <Name>Binomial</Name>
                           <Description>The discrete probability distribution of the number of successes in a sequence of independent experiments with binary outcomes. The two distribution parameters are n (number of experiments) and p (the probability of success).</Description>
                           <Usage>Used in situations where the number of tials (or observations) is fixed, each trial is independent and the probability of success is the same for each trial. Usage examples include the analysis of a drug treatment or analysis of a lottery.</Usage>
                           <InformationLink>https://en.wikipedia.org/wiki/Binomial_distribution</InformationLink>
                           <InformationLink>https://numerics.mathdotnet.com/api/MathNet.Numerics.Distributions/Binomial.htm</InformationLink>
                           <InformationLink>https://mathworld.wolfram.com/BinomialDistribution.html</InformationLink>
                           <Continuity>Discrete</Continuity>
                           <NRandomVariables>Univariate</NRandomVariables>
                           <XValueRange>
                               <Minimum>0</Minimum>
                               <Maximum>+inf</Maximum>
                               <DefaultMin>0</DefaultMin>
                               <DefaultMax>32</DefaultMax>
                           </XValueRange>
                           <NParameters>2</NParameters>
                           <Default>
                               <ParameterA>
                                   <Type>Probability</Type>
                                   <Name>P success</Name>
                                   <Symbol>p</Symbol>
                                   <Abbreviation>p</Abbreviation>
                                   <Description>Probability of success.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>0</Minimum>
                                   <Maximum>1</Maximum>
                                   <Default>0.5</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>1</AdjustMax>
                                   <Increment>0.1</Increment>
                               </ParameterA>
                               <ParameterB>
                                   <Type>Number</Type>
                                   <Name>No. trials</Name>
                                   <Symbol>N</Symbol>
                                   <Abbreviation>n</Abbreviation>
                                   <Description>Number of Bernoulli trials.</Description>
                                   <NumberType>Integer</NumberType>
                                   <Minimum>0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>32</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>100</AdjustMax>
                                   <Increment>1</Increment>
                               </ParameterB>
                           </Default>
                           <NAlternateParameterSets>0</NAlternateParameterSets>
                           <Types>
                               <PMF>
                                   <XAxis>
                                       <Description>Number of successes</Description>
                                       <Label>k</Label>
                                       <Units>Successes</Units>
                                   </XAxis>
                               </PMF>
                               <PMFLn>
                                   <XAxis>
                                       <Description>Number of successes</Description>
                                       <Label>k</Label>
                                       <Units>Successes</Units>
                                   </XAxis>
                               </PMFLn>
                               <CDF>
                                   <XAxis>
                                       <Description>Number of successes</Description>
                                       <Label>x</Label>
                                       <Units>Successes</Units>
                                   </XAxis>
                               </CDF>
                           </Types>
                       </Distribution>
                       <!--The Burr Distribution-->
                       <Distribution>
                           <Name>Burr</Name>
                           <Description>Three parameter continuous distribution with a flexible shape and controllable scale.</Description>
                           <Usage>Used to model houshold income, crop prices, travel time, flood levels and failure data.</Usage>
                           <InformationLink>https://en.wikipedia.org/wiki/Burr_distribution</InformationLink>
                           <InformationLink>https://numerics.mathdotnet.com/api/MathNet.Numerics.Distributions/Burr.htm</InformationLink>
                           <InformationLink>https://www.mathworks.com/help/stats/burr-type-xii-distribution.html</InformationLink>
                           <InformationLink>https://www.statisticshowto.com/burr-distribution/</InformationLink>
                           <Continuity>Continuous</Continuity>
                           <NRandomVariables>Univariate</NRandomVariables>
                           <XValueRange>
                               <Minimum>0</Minimum>
                               <Maximum>+inf</Maximum>
                               <DefaultMin>0</DefaultMin>
                               <DefaultMax>3</DefaultMax>
                           </XValueRange>
                           <NParameters>3</NParameters>
                           <Default>
                               <ParameterA>
                                   <Type>Scale</Type>
                                   <Name>alpha</Name>
                                   <Symbol>Alpha</Symbol>
                                   <Abbreviation>a</Abbreviation>
                                   <Description>The scale parameter.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt 0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>1</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterA>
                               <ParameterB>
                                   <Type>Shape</Type>
                                   <Name>c</Name>
                                   <Symbol>c</Symbol>
                                   <Abbreviation>c</Abbreviation>
                                   <Description>The first shape parameter.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt 0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>1</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterB>
                               <ParameterC>
                                   <Type>Shape</Type>
                                   <Name>k</Name>
                                   <Symbol>k</Symbol>
                                   <Abbreviation>k</Abbreviation>
                                   <Description>The second shape parameter.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt 0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>1</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterC>
                           </Default>
                           <NAlternateParameterSets>0</NAlternateParameterSets>
                           <Types>
                               <PDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDF>
                               <PDFLn>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDFLn>
                               <CDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </CDF>
                           </Types>
                       </Distribution>
                       <!--The Categorical Distribution-->
                       <Distribution>
                           <Name>Categorical</Name>
                           <Description>The discrete distribution of a categorical random variable.</Description>
                           <Usage>Used to analyse categorical data such as age, race, sex or educational level.</Usage>
                           <InformationLink>https://en.wikipedia.org/wiki/Categorical_distribution</InformationLink>
                           <InformationLink>https://numerics.mathdotnet.com/api/MathNet.Numerics.Distributions/Categorical.htm</InformationLink>
                           <InformationLink>https://www.statisticshowto.com/categorical-distribution/</InformationLink>
                           <InformationLink>http://bois.caltech.edu/distribution_explorer/discrete/categorical.html</InformationLink>
                           <Continuity>Discrete</Continuity>
                           <NRandomVariables>Univariate</NRandomVariables>
                           <XValueRange>
                               <Minimum>0</Minimum>
                               <Maximum>+inf</Maximum>
                               <DefaultMin>1</DefaultMin>
                               <DefaultMax>4</DefaultMax>
                           </XValueRange>
                           <NParameters>1</NParameters>
                           <Default>
                               <ParameterA>
                                   <Type>Array of ratios</Type>
                                   <Name>Probability Mass</Name>
                                   <Symbol></Symbol>
                                   <Abbreviation>probMass</Abbreviation>
                                   <Description>An array of non-negative ratios. This array does not need to be normalized as this is often impossible using floating point arithmetic.</Description>
                                   <NumberType>Real()</NumberType>
                                   <Minimum>0</Minimum>
                                   <Maximum>1</Maximum>
                                   <Increment>0.1</Increment>
                               </ParameterA>
                           </Default>
                           <NAlternateParameterSets>0</NAlternateParameterSets>
                           <Types>
                               <PMF>
                                   <XAxis>
                                       <Description>Category</Description>
                                       <Label>k</Label>
                                       <Units>Category</Units>
                                   </XAxis>
                               </PMF>
                               <PMFLn>
                                   <XAxis>
                                       <Description>Category</Description>
                                       <Label>k</Label>
                                       <Units>Category</Units>
                                   </XAxis>
                               </PMFLn>
                               <CDF>
                                   <XAxis>
                                       <Description>Category</Description>
                                       <Label>x</Label>
                                       <Units>Category</Units>
                                   </XAxis>
                               </CDF>
                               <InvCDF>
                                   <XAxis>
                                       <Description>Probability</Description>
                                       <Label>p</Label>
                                   </XAxis>
                               </InvCDF>
                           </Types>
                       </Distribution>
                       <!--The Cauchy Distribution-->
                       <Distribution>
                           <Name>Cauchy</Name>
                           <Alias>Lorenz</Alias>
                           <Alias>Lorenzian</Alias>
                           <Description>The distribution of the x intercept of a ray emitted from a point. The mean and variance of this distribution do not exist.</Description>
                           <Usage>Used in robustness studies, to model the ratio of two normal random variables and has been used to model situations in physics.</Usage>
                           <InformationLink>https://en.wikipedia.org/wiki/Cauchy_distribution</InformationLink>
                           <InformationLink>https://numerics.mathdotnet.com/api/MathNet.Numerics.Distributions/Cauchy.htm</InformationLink>
                           <InformationLink>https://www.statisticshowto.com/cauchy-distribution-2/</InformationLink>
                           <Continuity>Continuous</Continuity>
                           <NRandomVariables>Univariate</NRandomVariables>
                           <XValueRange>
                               <Minimum>-inf</Minimum>
                               <Maximum>+inf</Maximum>
                               <DefaultMin>-5</DefaultMin>
                               <DefaultMax>5</DefaultMax>
                           </XValueRange>
                           <NParameters>2</NParameters>
                           <Default>
                               <ParameterA>
                                   <Type>Location</Type>
                                   <Name>x0</Name>
                                   <Symbol>X0</Symbol>
                                   <Abbreviation>location</Abbreviation>
                                   <Description>The location of the distribution.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>-inf</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>0</Default>
                                   <AdjustMin>-10</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>1</Increment>
                               </ParameterA>
                               <ParameterB>
                                   <Type>Scale</Type>
                                   <Name>gamma</Name>
                                   <Symbol>Gamma</Symbol>
                                   <Abbreviation>scale</Abbreviation>
                                   <Description>The scale of the distribution. Half-width at half-maximum.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt 0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>1</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterB>
                           </Default>
                           <NAlternateParameterSets>1</NAlternateParameterSets>
                           <Alternate1>
                               <ParameterA>
                                   <Type>Location</Type>
                                   <Name>x0</Name>
                                   <Symbol>X0</Symbol>
                                   <Abbreviation>location</Abbreviation>
                                   <Description>The location of the distribution.</Description>
                                   <Default>0</Default>
                                   <NumberType>Real</NumberType>
                                   <Minimum>-inf</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <AdjustMin>-10</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>1</Increment>
                               </ParameterA>
                               <ParameterB>
                                   <Type>Scale</Type>
                                   <Name>2gamma</Name>
                                   <Symbol>2Gamma</Symbol>
                                   <Abbreviation>scale</Abbreviation>
                                   <Description>The scale of the distribution. Full-width at half-maximum.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt 0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>2</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterB>
                           </Alternate1>
                           <Types>
                               <PDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDF>
                               <PDFLn>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDFLn>
                               <CDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </CDF>
                               <InvCDF>
                                   <XAxis>
                                       <Description>Probability</Description>
                                       <Label>p</Label>
                                   </XAxis>
                               </InvCDF>
                           </Types>
                       </Distribution>
                       <!--The Chi Distribution-->
                       <Distribution>
                           <Name>Chi</Name>
                           <Description>The distribution of the positive square root of the sum of squares of a set of independent random variables.</Description>
                           <Usage></Usage>
                           <InformationLink>https://en.wikipedia.org/wiki/Chi_distribution</InformationLink>
                           <InformationLink>https://numerics.mathdotnet.com/api/MathNet.Numerics.Distributions/Chi.htm</InformationLink>
                           <InformationLink>https://mathworld.wolfram.com/ChiDistribution.html</InformationLink>
                           <Continuity>Continuous</Continuity>
                           <NRandomVariables>Univariate</NRandomVariables>
                           <XValueRange>
                               <Minimum>0</Minimum>
                               <Maximum>+inf</Maximum>
                               <DefaultMin>0</DefaultMin>
                               <DefaultMax>4</DefaultMax>
                           </XValueRange>
                           <NParameters>1</NParameters>
                           <Default>
                               <ParameterA>
                                   <Type>Freedom</Type>
                                   <Name>k</Name>
                                   <Symbol>k</Symbol>
                                   <Abbreviation>freedom</Abbreviation>
                                   <Description>The degrees of freedom of the distribution.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt 0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>1</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterA>
                           </Default>
                           <NAlternateParameterSets>0</NAlternateParameterSets>
                           <Types>
                               <PDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDF>
                               <PDFLn>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDFLn>
                               <CDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </CDF>
                           </Types>
                       </Distribution>
                       <!--The Chi Squared Distribution-->
                       <Distribution>
                           <Name>Chi Squared</Name>
                           <Alias>Chi Square</Alias>
                           <Description>The sum of the squares of k independent standard normal random variables.</Description>
                           <Usage>The distribution is widely used in inferential statistics.</Usage>
                           <InformationLink>https://en.wikipedia.org/wiki/Chi-square_distribution</InformationLink>
                           <InformationLink>https://numerics.mathdotnet.com/api/MathNet.Numerics.Distributions/ChiSquared.htm</InformationLink>
                           <InformationLink></InformationLink>
                           <InformationLink></InformationLink>
                           <Continuity>Continuous</Continuity>
                           <NRandomVariables>Univariate</NRandomVariables>
                           <XValueRange>
                               <Minimum>0 or gt 0 if k eq 1</Minimum>
                               <Maximum>+inf</Maximum>
                               <DefaultMin>0</DefaultMin>
                               <DefaultMax>8</DefaultMax>
                           </XValueRange>
                           <NParameters>1</NParameters>
                           <Default>
                               <ParameterA>
                                   <Type>Freedom</Type>
                                   <Name>k</Name>
                                   <Symbol>k</Symbol>
                                   <Abbreviation>freedom</Abbreviation>
                                   <Description>The degrees of freedom of the distribution.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt 0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>3</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.5</Increment>
                               </ParameterA>
                           </Default>
                           <NAlternateParameterSets>0</NAlternateParameterSets>
                           <Types>
                               <PDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDF>
                               <PDFLn>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDFLn>
                               <CDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </CDF>
                               <InvCDF>
                                   <XAxis>
                                       <Description>Probability</Description>
                                       <Label>p</Label>
                                   </XAxis>
                               </InvCDF>
                           </Types>
                       </Distribution>
                       <!--The Continuous Uniform Distribution-->
                       <Distribution>
                           <Name>Continuous Uniform</Name>
                           <Alias>Rectangular</Alias>
                           <Description>Distribution corresponding to an experiment with arbitrary outcomes between certain bounds.</Description>
                           <Usage>Situations where we have no knowledge of the likely value of a random variable within a range, such as the position of an air molecule in a room or the position of a puncture in a tyre.</Usage>
                           <InformationLink>https://en.wikipedia.org/wiki/Uniform_distribution_(continuous)</InformationLink>
                           <InformationLink>https://numerics.mathdotnet.com/api/MathNet.Numerics.Distributions/ContinuousUniform.htm</InformationLink>
                           <InformationLink>https://blog.palisade.com/2008/12/23/uses-of-the-uniform-continuous-distribution/</InformationLink>
                           <Continuity>Continuous</Continuity>
                           <NRandomVariables>Univariate</NRandomVariables>
                           <XValueRange>
                               <Minimum>-inf</Minimum>
                               <Maximum>+inf</Maximum>
                               <DefaultMin>0</DefaultMin>
                               <DefaultMax>4</DefaultMax>
                           </XValueRange>
                           <NParameters>2</NParameters>
                           <Default>
                               <ParameterA>
                                   <Type></Type>
                                   <Name>a</Name>
                                   <Symbol>a</Symbol>
                                   <Abbreviation>lower</Abbreviation>
                                   <Description>Lower value</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>-inf</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>1</Default>
                                   <AdjustMin>-10</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>1</Increment>
                               </ParameterA>
                               <ParameterB>
                                   <Type></Type>
                                   <Name>b</Name>
                                   <Symbol>b</Symbol>
                                   <Abbreviation>upper</Abbreviation>
                                   <Description>Upper value</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>a</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>3</Default>
                                   <AdjustMin>-10</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>1</Increment>
                               </ParameterB>
                           </Default>
                           <NAlternateParameterSets>0</NAlternateParameterSets>
                           <Types>
                               <PDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDF>
                               <PDFLn>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDFLn>
                               <CDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </CDF>
                               <InvCDF>
                                   <XAxis>
                                       <Description>Probability</Description>
                                       <Label>p</Label>
                                   </XAxis>
                               </InvCDF>
                           </Types>
                       </Distribution>
                       <!--The Conway Maxwell Poisson Distribution-->
                       <Distribution>
                           <Name>Conway-Maxwell-Poisson</Name>
                           <Description>A generalization of the Poisson, Geometric and Bernoulli distributions.</Description>
                           <Usage>Used for analysing queueing systems.</Usage>
                           <InformationLink>https://en.wikipedia.org/wiki/Conway%E2%80%93Maxwell%E2%80%93Poisson_distribution</InformationLink>
                           <InformationLink>https://numerics.mathdotnet.com/api/MathNet.Numerics.Distributions/ConwayMaxwellPoisson.htm</InformationLink>
                           <InformationLink></InformationLink>
                           <Continuity>Discrete</Continuity>
                           <NRandomVariables>Univariate</NRandomVariables>
                           <XValueRange>
                               <Minimum>0</Minimum>
                               <Maximum>+inf</Maximum>
                               <DefaultMin>0</DefaultMin>
                               <DefaultMax>16</DefaultMax>
                           </XValueRange>
                           <NParameters>2</NParameters>
                           <Default>
                               <ParameterA>
                                   <Type></Type>
                                   <Name>lambda</Name>
                                   <Symbol>Lambda</Symbol>
                                   <Abbreviation>lambda</Abbreviation>
                                   <Description>The lambda parameter.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt 0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>3</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.5</Increment>
                               </ParameterA>
                               <ParameterB>
                                   <Type></Type>
                                   <Name>nu</Name>
                                   <Symbol>Nu</Symbol>
                                   <Abbreviation>nu</Abbreviation>
                                   <Description>The rate of decay.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>1</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterB>
                           </Default>
                           <NAlternateParameterSets>0</NAlternateParameterSets>
                           <Types>
                               <PMF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>k</Label>
                                       <Units>Occurrences</Units>
                                   </XAxis>
                               </PMF>
                               <PMFLn>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>k</Label>
                                       <Units>Occurrences</Units>
                                   </XAxis>
                               </PMFLn>
                               <CDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                       <Units>Occurrences</Units>
                                   </XAxis>
                               </CDF>
                           </Types>
                       </Distribution>
                       <!--The Discrete Uniform Distribution-->
                       <Distribution>
                           <Name>Discrete Uniform</Name>
                           <Description>The probability distribution for a finite number of values equally likely to be observed.</Description>
                           <Usage>Can be used to analyse lotteries or card games.</Usage>
                           <InformationLink>https://en.wikipedia.org/wiki/Discrete_uniform_distribution</InformationLink>
                           <InformationLink>https://numerics.mathdotnet.com/api/MathNet.Numerics.Distributions/DiscreteUniform.htm</InformationLink>
                           <Continuity>Discrete</Continuity>
                           <NRandomVariables>Univariate</NRandomVariables>
                           <XValueRange>
                               <Minimum>-inf</Minimum>
                               <Maximum>+inf</Maximum>
                               <DefaultMin>0</DefaultMin>
                               <DefaultMax>5</DefaultMax>
                           </XValueRange>
                           <NParameters>2</NParameters>
                           <Default>
                               <ParameterA>
                                   <Type></Type>
                                   <Name>a</Name>
                                   <Symbol>a</Symbol>
                                   <Abbreviation>lower</Abbreviation>
                                   <Description>Lower value, inclusive.</Description>
                                   <NumberType>Integer</NumberType>
                                   <Minimum>-inf</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>1</Default>
                                   <AdjustMin>-10</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>1</Increment>
                               </ParameterA>
                               <ParameterB>
                                   <Type></Type>
                                   <Name>b</Name>
                                   <Symbol>b</Symbol>
                                   <Abbreviation>upper</Abbreviation>
                                   <Description>Upper value, inclusive.</Description>
                                   <NumberType>Integer</NumberType>
                                   <Minimum>a</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>4</Default>
                                   <AdjustMin>-0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>1</Increment>
                               </ParameterB>
                           </Default>
                           <NAlternateParameterSets>0</NAlternateParameterSets>
                           <Types>
                               <PMF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>k</Label>
                                   </XAxis>
                               </PMF>
                               <PMFLn>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>k</Label>
                                   </XAxis>
                               </PMFLn>
                               <CDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </CDF>
                           </Types>
                       </Distribution>
                       <!--The Erlang Distribution-->
                       <Distribution>
                           <Name>Erlang</Name>
                           <Description>The Erlang distribution is a generalization of the exponential distribution. </Description>
                           <Usage>Used in the study of telecommunication networks and queueing systems.</Usage>
                           <InformationLink>https://en.wikipedia.org/wiki/Erlang_distribution</InformationLink>
                           <InformationLink>https://numerics.mathdotnet.com/api/MathNet.Numerics.Distributions/Erlang.htm</InformationLink>
                           <InformationLink>https://www.sciencedirect.com/topics/mathematics/erlang-random-variable</InformationLink>
                           <InformationLink></InformationLink>
                           <Continuity>Continuous</Continuity>
                           <NRandomVariables>Univariate</NRandomVariables>
                           <XValueRange>
                               <Minimum>0</Minimum>
                               <Maximum>+inf</Maximum>
                               <DefaultMin>0</DefaultMin>
                               <DefaultMax>20</DefaultMax>
                           </XValueRange>
                           <NParameters>2</NParameters>
                           <Default>
                               <ParameterA>
                                   <Type>Shape</Type>
                                   <Name>k</Name>
                                   <Symbol>k</Symbol>
                                   <Abbreviation>shape</Abbreviation>
                                   <Description>The shape parameter.</Description>
                                   <NumberType>Integer</NumberType>
                                   <Minimum>1</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>2</Default>
                                   <AdjustMin>1</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterA>
                               <ParameterB>
                                   <Type>Rate</Type>
                                   <Name>lambda</Name>
                                   <Symbol>Lambda</Symbol>
                                   <Abbreviation>rate</Abbreviation>
                                   <Description>The rate or inverse scale.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt 0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>2</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterB>
                           </Default>
                           <NAlternateParameterSets>1</NAlternateParameterSets>
                           <Alternate1>
                               <ParameterA>
                                   <Type>Shape</Type>
                                   <Name>k</Name>
                                   <Symbol>k</Symbol>
                                   <Abbreviation>shape</Abbreviation>
                                   <Description>The shape parameter.</Description>
                                   <NumberType>Integer</NumberType>
                                   <Minimum>1</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>2</Default>
                                   <AdjustMin>1</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterA>
                               <ParameterB>
                                   <Type>Scale</Type>
                                   <Name>mu</Name>
                                   <Symbol>Mu</Symbol>
                                   <Abbreviation>scale</Abbreviation>
                                   <Description>Reciprocal of the rate</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt 0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>0.5</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterB>
                           </Alternate1>
                           <Types>
                               <PDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDF>
                               <PDFLn>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDFLn>
                               <CDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </CDF>
                           </Types>
                       </Distribution>
                       <!--The Exponential Distribution-->
                       <Distribution>
                           <Name>Exponential</Name>
                           <Description>The distribution of the time between events that occur continuously and independently at a constant average rate (a Poisson point process).</Description>
                           <Usage>Used in reliability analysis to model data with a constant failure rate.</Usage>
                           <InformationLink>https://en.wikipedia.org/wiki/Exponential_distribution</InformationLink>
                           <InformationLink>https://numerics.mathdotnet.com/api/MathNet.Numerics.Distributions/Exponential.htm</InformationLink>
                           <InformationLink>https://mathworld.wolfram.com/ExponentialDistribution.html</InformationLink>
                           <Continuity>Continuous</Continuity>
                           <NRandomVariables>Univariate</NRandomVariables>
                           <XValueRange>
                               <Minimum>0</Minimum>
                               <Maximum>+inf</Maximum>
                               <DefaultMin>0</DefaultMin>
                               <DefaultMax>5</DefaultMax>
                           </XValueRange>
                           <NParameters>1</NParameters>
                           <Default>
                               <ParameterA>
                                   <Type>Rate</Type>
                                   <Name>lambda</Name>
                                   <Symbol>Lambda</Symbol>
                                   <Abbreviation>rate</Abbreviation>
                                   <Description>The rate parameter.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt 0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>1</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterA>
                           </Default>
                           <NAlternateParameterSets>1</NAlternateParameterSets>
                           <Alternate1>
                               <ParameterA>
                                   <Type>Scale</Type>
                                   <Name>beta</Name>
                                   <Symbol>Beta</Symbol>
                                   <Abbreviation>scale</Abbreviation>
                                   <Description>Inverse of rate.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt 0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>1</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterA>
                           </Alternate1>
                           <Types>
                               <PDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDF>
                               <PDFLn>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDFLn>
                               <CDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </CDF>
                               <InvCDF>
                                   <XAxis>
                                       <Description>Probability</Description>
                                       <Label>p</Label>
                                   </XAxis>
                               </InvCDF>
                           </Types>
                       </Distribution>
                       <!--The Fisher Snedecor Distribution-->
                       <Distribution>
                           <Name>Fisher-Snedecor</Name>
                           <Alias>F</Alias>
                           <Alias>Shedecor's F</Alias>
                           <Description>Used for hypothesis testing  with the comparison of variances between two samples. Also used to test if one model is statistically better than another.</Description>
                           <Usage>Used to test whether two independent samples have been drawn from normal populations with the same variance.</Usage>
                           <InformationLink>https://en.wikipedia.org/wiki/F-distribution</InformationLink>
                           <InformationLink>https://numerics.mathdotnet.com/api/MathNet.Numerics.Distributions/FisherSnedecor.htm</InformationLink>
                           <InformationLink>https://wiki.analytica.com/index.php?title=F-distribution</InformationLink>
                           <Continuity>Continuous</Continuity>
                           <NRandomVariables>Univariate</NRandomVariables>
                           <XValueRange>
                               <Minimum>gt 0 if d1 eq 1 else 0</Minimum>
                               <Maximum>+inf</Maximum>
                               <DefaultMin>0</DefaultMin>
                               <DefaultMax>5</DefaultMax>
                           </XValueRange>
                           <NParameters>2</NParameters>
                           <Default>
                               <ParameterA>
                                   <Type></Type>
                                   <Name>d1</Name>
                                   <Symbol>d1</Symbol>
                                   <Abbreviation>d1</Abbreviation>
                                   <Description>The first degree of freedom.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt 0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>5</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterA>
                               <ParameterB>
                                   <Type></Type>
                                   <Name>d2</Name>
                                   <Symbol>d2</Symbol>
                                   <Abbreviation>d2</Abbreviation>
                                   <Description>The second degree of freedom.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt 0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>2</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterB>
                           </Default>
                           <NAlternateParameterSets>0</NAlternateParameterSets>
                           <Types>
                               <PDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDF>
                               <PDFLn>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDFLn>
                               <CDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </CDF>
                               <InvCDF>
                                   <XAxis>
                                       <Description>Probability</Description>
                                       <Label>p</Label>
                                   </XAxis>
                               </InvCDF>
                           </Types>
                       </Distribution>
                       <!--The Gamma Distribution-->
                       <Distribution>
                           <Name>Gamma</Name>
                           <Description>A continuous, positive-only unimodal distribution.</Description>
                           <Usage>Widely used in engineering, science and business to model continuous variables that are always positive and have skewed distributions. Used for modelling waiting times between events that are Poisson distributed. </Usage>
                           <InformationLink>https://en.wikipedia.org/wiki/Gamma_distribution</InformationLink>
                           <InformationLink>https://numerics.mathdotnet.com/api/MathNet.Numerics.Distributions/Gamma.htm</InformationLink>
                           <InformationLink>https://wiki.analytica.com/index.php?title=Gamma_distribution</InformationLink>
                           <InformationLink>https://alexpghayes.github.io/distributions3/reference/gamma.html</InformationLink>
                           <Continuity>Continuous</Continuity>
                           <NRandomVariables>Univariate</NRandomVariables>
                           <XValueRange>
                               <Minimum>gt 0</Minimum>
                               <Maximum>+inf</Maximum>
                               <DefaultMin>0</DefaultMin>
                               <DefaultMax>20</DefaultMax>
                           </XValueRange>
                           <NParameters>2</NParameters>
                           <Default>
                               <ParameterA>
                                   <Type>Shape</Type>
                                   <Name>alpha</Name>
                                   <Symbol>Alpha</Symbol>
                                   <Abbreviation>shape</Abbreviation>
                                   <Description>Shape parameter.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt 0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>2</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterA>
                               <ParameterB>
                                   <Type>Rate</Type>
                                   <Name>beta</Name>
                                   <Symbol>Beta</Symbol>
                                   <Abbreviation>rate</Abbreviation>
                                   <Description>The rate or inverse scale.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt 0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>0.5</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.1</Increment>
                               </ParameterB>
                           </Default>
                           <NAlternateParameterSets>2</NAlternateParameterSets>
                           <Alternate1>
                               <ParameterA>
                                   <Type>Shape</Type>
                                   <Name>k</Name>
                                   <Symbol>k</Symbol>
                                   <Abbreviation>shape</Abbreviation>
                                   <Description>Shape parameter.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt 0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>2</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterA>
                               <ParameterB>
                                   <Type>Scale</Type>
                                   <Name>theta</Name>
                                   <Symbol>Theta</Symbol>
                                   <Abbreviation>scale</Abbreviation>
                                   <Description>Inverse of rate or scale.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt 0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>2</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.1</Increment>
                               </ParameterB>
                           </Alternate1>
                           <Alternate2>
                               <ParameterA>
                                   <Type>Shape</Type>
                                   <Name>k</Name>
                                   <Symbol>k</Symbol>
                                   <Abbreviation>shape</Abbreviation>
                                   <Description>Shape parameter.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt 0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>2</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterA>
                               <ParameterB>
                                   <Type>Mean</Type>
                                   <Name>mu</Name>
                                   <Symbol>Mu</Symbol>
                                   <Abbreviation>mean</Abbreviation>
                                   <Description>k * Theta or Alpha / Beta (shape * scale or shape / rate)</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt 0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>4</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterB>
                           </Alternate2>
                           <Types>
                               <PDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDF>
                               <PDFLn>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDFLn>
                               <CDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </CDF>
                               <InvCDF>
                                   <XAxis>
                                       <Description>Probability</Description>
                                       <Label>p</Label>
                                   </XAxis>
                               </InvCDF>
                           </Types>
                       </Distribution>
                       <!--The Geometric Distribution-->
                       <Distribution>
                           <Name>Geometric</Name>
                           <Description>The probability distribution of the number of Bernoulli trials needed to get one success. A Bernoulli trial has two possible outcomes, success or failure, with a constant probability of success.</Description>
                           <Usage>Useful for modelling situations where we want to know how many attempts are likely to be needed for success. Used in population modelling and economics.</Usage>
                           <InformationLink>https://en.wikipedia.org/wiki/Geometric_distribution</InformationLink>
                           <InformationLink>https://numerics.mathdotnet.com/api/MathNet.Numerics.Distributions/Geometric.htm</InformationLink>
                           <InformationLink>https://mathworld.wolfram.com/GeometricDistribution.html</InformationLink>
                           <InformationLink>https://www.statisticshowto.com/geometric-distribution/</InformationLink>
                           <InformationLink>https://www.rdocumentation.org/packages/distributions3/versions/0.1.1/topics/Geometric</InformationLink>
                           <Continuity>Discrete</Continuity>
                           <NRandomVariables>Univariate</NRandomVariables>
                           <XValueRange>
                               <Minimum>1</Minimum>
                               <Maximum>+inf</Maximum>
                               <DefaultMin>1</DefaultMin>
                               <DefaultMax>10</DefaultMax>
                           </XValueRange>
                           <NParameters>1</NParameters>
                           <Default>
                               <ParameterA>
                                   <Type>Probability</Type>
                                   <Name>P success</Name>
                                   <Symbol>p</Symbol>
                                   <Abbreviation>p</Abbreviation>
                                   <Description>The probability of generating 1</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt 0</Minimum>
                                   <Maximum>1</Maximum>
                                   <Default>0.5</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>1</AdjustMax>
                                   <Increment>0.1</Increment>
                               </ParameterA>
                           </Default>
                           <NAlternateParameterSets>0</NAlternateParameterSets>
                           <Types>
                               <PMF>
                                   <XAxis>
                                       <Description>Number of trials</Description>
                                       <Label>k</Label>
                                       <Units>Trials</Units>
                                   </XAxis>
                               </PMF>
                               <PMFLn>
                                   <XAxis>
                                       <Description>Number of trials</Description>
                                       <Label>k</Label>
                                       <Units>Trials</Units>
                                   </XAxis>
                               </PMFLn>
                               <CDF>
                                   <XAxis>
                                       <Description>Number of trials</Description>
                                       <Label>x</Label>
                                       <Units>Trials</Units>
                                   </XAxis>
                               </CDF>
                           </Types>
                       </Distribution>
                       <!--The Hypergeometric Distribution-->
                       <Distribution>
                           <Name>Hypergeometric</Name>
                           <Description>Distribution describing the probability of k successes in n draws, without replacement.</Description>
                           <Usage>Used to calculate probabilities when sampling without replacement.</Usage>
                           <InformationLink>https://en.wikipedia.org/wiki/Hypergeometric_distribution</InformationLink>
                           <InformationLink>https://numerics.mathdotnet.com/api/MathNet.Numerics.Distributions/Hypergeometric.htm</InformationLink>
                           <InformationLink>http://onlinestatbook.com/2/probability/hypergeometric.html</InformationLink>
                           <InformationLink>https://www.statisticshowto.com/hypergeometric-distribution-examples/</InformationLink>
                           <InformationLink>https://www.rdocumentation.org/packages/distributions3/versions/0.1.1/topics/HyperGeometric</InformationLink>
                           <Continuity>Discrete</Continuity>
                           <NRandomVariables>Univariate</NRandomVariables>
                           <XValueRange>
                               <Minimum>Maximum of 0 and draws + success - population</Minimum>
                               <Maximum>Minimum of draws and success</Maximum>
                               <DefaultMin>0</DefaultMin>
                               <DefaultMax>60</DefaultMax>
                           </XValueRange>
                           <NParameters>3</NParameters>
                           <Default>
                               <ParameterA>
                                   <Type>Number</Type>
                                   <Name>population</Name>
                                   <Symbol></Symbol>
                                   <Abbreviation>population</Abbreviation>
                                   <Description>The size of the population.</Description>
                                   <NumberType>Integer</NumberType>
                                   <Minimum>0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>500</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>1000</AdjustMax>
                                   <Increment>1</Increment>
                               </ParameterA>
                               <ParameterB>
                                   <Type>Number</Type>
                                   <Name>success</Name>
                                   <Symbol></Symbol>
                                   <Abbreviation>success</Abbreviation>
                                   <Description>The number of successes within the population.</Description>
                                   <NumberType>Integer</NumberType>
                                   <Minimum>0</Minimum>
                                   <Maximum>population</Maximum>
                                   <Default>50</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>100</AdjustMax>
                                   <Increment>1</Increment>
                               </ParameterB>
                               <ParameterC>
                                   <Type>Number</Type>
                                   <Name>draws</Name>
                                   <Symbol></Symbol>
                                   <Abbreviation>draws</Abbreviation>
                                   <Description>The number of draws without replacement.</Description>
                                   <NumberType>Integer</NumberType>
                                   <Minimum>0</Minimum>
                                   <Maximum>population</Maximum>
                                   <Default>200</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>1000</AdjustMax>
                                   <Increment>1</Increment>
                               </ParameterC>
                           </Default>
                           <NAlternateParameterSets>0</NAlternateParameterSets>
                           <Types>
                               <PMF>
                                   <XAxis>
                                       <Description>Number of successes</Description>
                                       <Label>k</Label>
                                       <Units>Successes</Units>
                                   </XAxis>
                               </PMF>
                               <PMFLn>
                                   <XAxis>
                                       <Description>Number of successes</Description>
                                       <Label>k</Label>
                                       <Units>Successes</Units>
                                   </XAxis>
                               </PMFLn>
                               <CDF>
                                   <XAxis>
                                       <Description>Number of successes or less</Description>
                                       <Label>x</Label>
                                       <Units>Successes</Units>
                                   </XAxis>
                               </CDF>
                           </Types>
                       </Distribution>
                       <!--The Inverse Gamma Distribution-->
                       <Distribution>
                           <Name>Inverse Gamma</Name>
                           <Alias>Inverted Gamma</Alias>
                           <Description>The reciprocal of the Gamma distribution.</Description>
                           <Usage>Commonly used in Bayesian statistics. Also used in machine learning, reliability theory and survival analysis.</Usage>
                           <InformationLink>https://en.wikipedia.org/wiki/Inverse-gamma_distribution</InformationLink>
                           <InformationLink>https://numerics.mathdotnet.com/api/MathNet.Numerics.Distributions/InverseGamma.htm</InformationLink>
                           <InformationLink>https://www.statisticshowto.com/inverse-gamma-distribution/</InformationLink>
                           <Continuity>Continuous</Continuity>
                           <NRandomVariables>Univariate</NRandomVariables>
                           <XValueRange>
                               <Minimum>gt 0</Minimum>
                               <Maximum>+inf</Maximum>
                               <DefaultMin>0</DefaultMin>
                               <DefaultMax>3</DefaultMax>
                           </XValueRange>
                           <NParameters>2</NParameters>
                           <Default>
                               <ParameterA>
                                   <Type>Shape</Type>
                                   <Name>alpha</Name>
                                   <Symbol>Alpha</Symbol>
                                   <Abbreviation>shape</Abbreviation>
                                   <Description>Shape parameter.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt 0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>2</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterA>
                               <ParameterB>
                                   <Type>Scale</Type>
                                   <Name>beta</Name>
                                   <Symbol>Beta</Symbol>
                                   <Abbreviation>scale</Abbreviation>
                                   <Description>Scale parameter.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt 0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>1</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterB>
                           </Default>
                           <NAlternateParameterSets>0</NAlternateParameterSets>
                           <Types>
                               <PDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDF>
                               <PDFLn>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDFLn>
                               <CDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </CDF>
                           </Types>
                       </Distribution>
                       <!--The Inverse Gaussian Distribution-->
                       <Distribution>
                           <Name>Inverse Gaussian</Name>
                           <Alias>Wald</Alias>
                           <Alias>Normal Inverse Gaussian</Alias>
                           <Description>An exponential distribution with a single mode and a long tail.</Description>
                           <Usage>Used to model non-negative, positively skewed data. Used in business, survival analysis, finance and medicine.</Usage>
                           <InformationLink>https://en.wikipedia.org/wiki/Inverse_Gaussian_distribution</InformationLink>
                           <InformationLink>https://numerics.mathdotnet.com/api/MathNet.Numerics.Distributions/InverseGaussian.htm</InformationLink>
                           <InformationLink>https://www.statisticshowto.com/inverse-gaussian/</InformationLink>
                           <Continuity>Continuous</Continuity>
                           <NRandomVariables>Univariate</NRandomVariables>
                           <XValueRange>
                               <Minimum>gt 0</Minimum>
                               <Maximum>+inf</Maximum>
                               <DefaultMin>0</DefaultMin>
                               <DefaultMax>3</DefaultMax>
                           </XValueRange>
                           <NParameters>2</NParameters>
                           <Default>
                               <ParameterA>
                                   <Type>Mean</Type>
                                   <Name>mu</Name>
                                   <Symbol>Mu</Symbol>
                                   <Abbreviation>mu</Abbreviation>
                                   <Description>The mean of the distribution.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt 0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>1</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterA>
                               <ParameterB>
                                   <Type>Shape</Type>
                                   <Name>lambda</Name>
                                   <Symbol>Lambda</Symbol>
                                   <Abbreviation>lambda</Abbreviation>
                                   <Description>Shape parameter.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt 0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>2</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterB>
                           </Default>
                           <NAlternateParameterSets>0</NAlternateParameterSets>
                           <Types>
                               <PDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDF>
                               <PDFLn>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDFLn>
                               <CDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </CDF>
                               <InvCDF>
                                   <XAxis>
                                       <Description>Probability</Description>
                                       <Label>p</Label>
                                   </XAxis>
                               </InvCDF>
                           </Types>
                       </Distribution>
                       <!--The Laplace Distribution-->
                       <Distribution>
                           <Name>Laplace</Name>
                           <Alias>Double Exponential</Alias>
                           <Description>The distribution of differences between two independent random variables with identical exponential distributions. Unimodal (one peak) and symmetrical. The peak is sharper than the normal distribution.</Description>
                           <Usage>Used to model phenonmena with heavy tails or when data has a higher peak than the normal distribution.</Usage>
                           <InformationLink>https://en.wikipedia.org/wiki/Laplace_distribution</InformationLink>
                           <InformationLink>https://numerics.mathdotnet.com/api/MathNet.Numerics.Distributions/Laplace.htm</InformationLink>
                           <InformationLink>https://mathworld.wolfram.com/LaplaceDistribution.html</InformationLink>
                           <InformationLink>https://www.sciencedirect.com/topics/mathematics/laplace-distribution</InformationLink>
                           <InformationLink>https://www.randomservices.org/random/special/Laplace.html</InformationLink>
                           <InformationLink>https://www.statisticshowto.com/laplace-distribution-double-exponential/</InformationLink>
                           <InformationLink>https://www.rdocumentation.org/packages/ExtDist/versions/0.6-3/topics/Laplace</InformationLink>
                           <Continuity>Continuous</Continuity>
                           <NRandomVariables>Univariate</NRandomVariables>
                           <XValueRange>
                               <Minimum>-inf</Minimum>
                               <Maximum>+inf</Maximum>
                               <DefaultMin>-10</DefaultMin>
                               <DefaultMax>10</DefaultMax>
                           </XValueRange>
                           <NParameters>2</NParameters>
                           <Default>
                               <ParameterA>
                                   <Type>Location</Type>
                                   <Name>mu</Name>
                                   <Symbol>Mu</Symbol>
                                   <Abbreviation>location</Abbreviation>
                                   <Description>The location of the distribution.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>-inf</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>0</Default>
                                   <AdjustMin>-10</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.5</Increment>
                               </ParameterA>
                               <ParameterB>
                                   <Type>Scale</Type>
                                   <Name>b</Name>
                                   <Symbol>b</Symbol>
                                   <Abbreviation>scale</Abbreviation>
                                   <Description>The scale of the distribution.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt 0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>2</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterB>
                           </Default>
                           <NAlternateParameterSets>0</NAlternateParameterSets>
                           <Types>
                               <PDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDF>
                               <PDFLn>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDFLn>
                               <CDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </CDF>
                           </Types>
                       </Distribution>
                       <!--The Log Normal Distribution-->
                       <Distribution>
                           <Name>Log Normal</Name>
                           <Description>The probability distribution of a random variable whose logarithm is normally distributed. A log normal distribution is the result of the product of of a large number of independent, identically distributed variables.</Description>
                           <Usage>Used to model measurements in engineering, medicine, economics and other fields. Measurements include energy, concentrations, lengths and financial returns.</Usage>
                           <InformationLink>https://en.wikipedia.org/wiki/Log-normal_distribution</InformationLink>
                           <InformationLink>https://numerics.mathdotnet.com/api/MathNet.Numerics.Distributions/LogNormal.htm</InformationLink>
                           <InformationLink>https://www.statisticshowto.com/lognormal-distribution/</InformationLink>
                           <Continuity>Continuous</Continuity>
                           <NRandomVariables>Univariate</NRandomVariables>
                           <XValueRange>
                               <Minimum>gt 0</Minimum>
                               <Maximum>+inf</Maximum>
                               <DefaultMin>0</DefaultMin>
                               <DefaultMax>3</DefaultMax>
                           </XValueRange>
                           <NParameters>2</NParameters>
                           <Default>
                               <ParameterA>
                                   <Type>Scale</Type>
                                   <Name>mu</Name>
                                   <Symbol>Mu</Symbol>
                                   <Abbreviation>mu</Abbreviation>
                                   <Description>The mean of the variable's natural logarithm</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>-inf</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>0</Default>
                                   <AdjustMin>-10</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.5</Increment>
                               </ParameterA>
                               <ParameterB>
                                   <Type>Shape</Type>
                                   <Name>sigma</Name>
                                   <Symbol>Sigma</Symbol>
                                   <Abbreviation>sigma</Abbreviation>
                                   <Description>The standard deviation of the variable's natural logarithm.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>0.5</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterB>
                           </Default>
                           <NAlternateParameterSets>0</NAlternateParameterSets>
                           <Types>
                               <PDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDF>
                               <PDFLn>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDFLn>
                               <CDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </CDF>
                               <InvCDF>
                                   <XAxis>
                                       <Description>Probability</Description>
                                       <Label>p</Label>
                                   </XAxis>
                               </InvCDF>
                           </Types>
                       </Distribution>
                       <!--The Negative Binomial Distribution-->
                       <Distribution>
                           <Name>Negative Binomial</Name>
                           <Alias>Pascal</Alias>
                           <Description>The number of failures in a sequence of Bernoulli trials before a specified number of successes occurs. (The distribution is described as "negative" because the number of failures is counted instead of the number of successes.)</Description>
                           <Usage>Analysis of sport probabilities (basketball, baseball etc).</Usage>
                           <InformationLink>https://en.wikipedia.org/wiki/Negative_binomial_distribution</InformationLink>
                           <InformationLink>https://numerics.mathdotnet.com/api/MathNet.Numerics.Distributions/NegativeBinomial.htm</InformationLink>
                           <InformationLink>https://mathworld.wolfram.com/NegativeBinomialDistribution.html</InformationLink>
                           <InformationLink>https://stattrek.com/probability-distributions/negative-binomial.aspx</InformationLink>
                           <InformationLink>https://www.thoughtco.com/negative-binomial-distribution-4091991</InformationLink>
                           <InformationLink>https://www.vosesoftware.com/riskwiki/NegativeBinomial.php</InformationLink>
                           <Continuity>Discrete</Continuity>
                           <NRandomVariables>Univariate</NRandomVariables>
                           <XValueRange>
                               <Minimum>0</Minimum>
                               <Maximum>+inf</Maximum>
                               <DefaultMin>0</DefaultMin>
                               <DefaultMax>30</DefaultMax>
                           </XValueRange>
                           <NParameters>2</NParameters>
                           <Default>
                               <ParameterA>
                                   <Type>Number</Type>
                                   <Name>r</Name>
                                   <Symbol>r</Symbol>
                                   <Abbreviation>r</Abbreviation>
                                   <Description>The number of successes required to stop the experiment</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>10</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>100</AdjustMax>
                                   <Increment>1</Increment>
                               </ParameterA>
                               <ParameterB>
                                   <Type>Probability</Type>
                                   <Name>P success</Name>
                                   <Symbol>p</Symbol>
                                   <Abbreviation>p</Abbreviation>
                                   <Description>Probability of success</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>0</Minimum>
                                   <Maximum>1</Maximum>
                                   <Default>0.6</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>1</AdjustMax>
                                   <Increment>0.1</Increment>
                               </ParameterB>
                           </Default>
                           <NAlternateParameterSets>0</NAlternateParameterSets>
                           <Types>
                               <PMF>
                                   <XAxis>
                                       <Description>Number of failures</Description>
                                       <Label>k</Label>
                                       <Units>Successes</Units>
                                   </XAxis>
                               </PMF>
                               <PMFLn>
                                   <XAxis>
                                       <Description>Number of failures</Description>
                                       <Label>k</Label>
                                   </XAxis>
                               </PMFLn>
                               <CDF>
                                   <XAxis>
                                       <Description>Number of failures</Description>
                                       <Label>x</Label>
                                       <Units>Successes</Units>
                                   </XAxis>
                               </CDF>
                           </Types>
                       </Distribution>
                       <!--The Normal Distribution-->
                       <Distribution>
                           <Name>Normal</Name>
                           <Alias>Gaussian</Alias>
                           <Alias>Gauss</Alias>
                           <Alias>Laplace Gauss</Alias>
                           <Alias>"Bell Curve"</Alias>
                           <Description>The most common distribution function for independent, randomly generated variables. The distribution is symmetrical, with a bell shape and an equal mean and median located at the center of the distribution.</Description>
                           <Usage>Used to model many natural phenomena such as peoples heights, blood pressure and IQ scores.</Usage>
                           <InformationLink>https://en.wikipedia.org/wiki/Normal_distribution</InformationLink>
                           <InformationLink>https://numerics.mathdotnet.com/api/MathNet.Numerics.Distributions/Normal.htm</InformationLink>
                           <InformationLink>https://www.britannica.com/topic/normal-distribution</InformationLink>
                           <InformationLink>https://www.itl.nist.gov/div898/handbook/eda/section3/eda3661.htm</InformationLink>
                           <InformationLink>https://mathworld.wolfram.com/NormalDistribution.html</InformationLink>
                           <Continuity>Continuous</Continuity>
                           <NRandomVariables>Univariate</NRandomVariables>
                           <XValueRange>
                               <Minimum>-inf</Minimum>
                               <Maximum>+inf</Maximum>
                               <DefaultMin>-5</DefaultMin>
                               <DefaultMax>5</DefaultMax>
                           </XValueRange>
                           <NParameters>2</NParameters>
                           <Default>
                               <ParameterA>
                                   <Type>Location</Type>
                                   <Name>Mean</Name>
                                   <Symbol>Mu</Symbol>
                                   <Abbreviation>mean</Abbreviation>
                                   <Description>The mean of the distribution.</Description>
                                   <Minimum>-inf</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <NumberType>Real</NumberType>
                                   <Default>0</Default>
                                   <AdjustMin>-10</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.5</Increment>
                               </ParameterA>
                               <ParameterB>
                                   <Type>Scale</Type>
                                   <Name>Standard Deviation</Name>
                                   <Symbol>Sigma</Symbol>
                                   <Abbreviation>stddev</Abbreviation>
                                   <Description>The standard deviation of the distribution.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt 0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>1</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterB>
                           </Default>
                           <NAlternateParameterSets>1</NAlternateParameterSets>
                           <Alternate1>
                               <ParameterA>
                                   <Type>Location</Type>
                                   <Name>Mean</Name>
                                   <Symbol>Mu</Symbol>
                                   <Abbreviation>mean</Abbreviation>
                                   <Description>The mean of the distribution.</Description>
                                   <Minimum>-inf</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <NumberType>Real</NumberType>
                                   <Default>0</Default>
                                   <AdjustMin>-10</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                               </ParameterA>
                               <ParameterB>
                                   <Type>Scale</Type>
                                   <Name>Variance</Name>
                                   <Symbol>Sigma squared</Symbol>
                                   <Abbreviation>variance</Abbreviation>
                                   <Description>The variance of the distribution (standard deviation squared).</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt 0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>1</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                               </ParameterB>
                           </Alternate1>
                           <Types>
                               <PDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDF>
                               <PDFLn>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDFLn>
                               <CDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </CDF>
                               <InvCDF>
                                   <XAxis>
                                       <Description>Probability</Description>
                                       <Label>p</Label>
                                   </XAxis>
                               </InvCDF>
                           </Types>
                       </Distribution>
                       <!--The Pareto Distribution-->
                       <Distribution>
                           <Name>Pareto</Name>
                           <Description>A power-law probability distribution.</Description>
                           <Usage>Description of social, scientific, actuarial and other types of observable phenomena. Examples include sizes of sand particles, sizes of cities and towns, sizes of meteorites and sizes of listed companies.</Usage>
                           <InformationLink>https://en.wikipedia.org/wiki/Pareto_distribution</InformationLink>
                           <InformationLink>https://numerics.mathdotnet.com/api/MathNet.Numerics.Distributions/Pareto.htm</InformationLink>
                           <InformationLink>http://wiki.stat.ucla.edu/socr/index.php/AP_Statistics_Curriculum_2007_Pareto</InformationLink>
                           <InformationLink>https://www.statisticshowto.com/pareto-distribution/</InformationLink>
                           <InformationLink>https://www.sciencedirect.com/topics/computer-science/pareto-distribution</InformationLink>
                           <Continuity>Continuous</Continuity>
                           <NRandomVariables>Univariate</NRandomVariables>
                           <XValueRange>
                               <Minimum>gt xm</Minimum>
                               <Maximum>+inf</Maximum>
                               <DefaultMin>0</DefaultMin>
                               <DefaultMax>5</DefaultMax>
                           </XValueRange>
                           <NParameters>2</NParameters>
                           <Default>
                               <ParameterA>
                                   <Type>Scale</Type>
                                   <Name>xm</Name>
                                   <Symbol>xm</Symbol>
                                   <Abbreviation>scale</Abbreviation>
                                   <Description>Minimum possible value of x.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt 0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>1</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterA>
                               <ParameterB>
                                   <Type>Shape</Type>
                                   <Name>alpha</Name>
                                   <Symbol>Alpha</Symbol>
                                   <Abbreviation>shape</Abbreviation>
                                   <Description>Shape parameter.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt 0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>1</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterB>
                           </Default>
                           <NAlternateParameterSets>0</NAlternateParameterSets>
                           <Types>
                               <PDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDF>
                               <PDFLn>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDFLn>
                               <CDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </CDF>
                               <InvCDF>
                                   <XAxis>
                                       <Description>Probability</Description>
                                       <Label>p</Label>
                                   </XAxis>
                               </InvCDF>
                           </Types>
                       </Distribution>
                       <!--The Poisson Distribution-->
                       <Distribution>
                           <Name>Poisson</Name>
                           <Description>The probability of a given number of events occurring in a fixed interval of time or space if the independent events occur with a known constant mean rate.</Description>
                           <Usage>Number of stars in a fixed volume of space, number of decayed nuclei in a radiactive source within a given period of time, number of mail items received each day.</Usage>
                           <InformationLink>https://en.wikipedia.org/wiki/Poisson_distribution</InformationLink>
                           <InformationLink>https://numerics.mathdotnet.com/api/MathNet.Numerics.Distributions/Poisson.htm</InformationLink>
                           <InformationLink>http://wiki.stat.ucla.edu/socr/index.php/AP_Statistics_Curriculum_2007_Distrib_Poisson</InformationLink>
                           <InformationLink>https://www.rdocumentation.org/packages/distributions3/versions/0.1.1/topics/Poisson</InformationLink>
                           <Continuity>Discrete</Continuity>
                           <NRandomVariables>Univariate</NRandomVariables>
                           <XValueRange>
                               <Minimum>0</Minimum>
                               <Maximum>+inf</Maximum>
                               <DefaultMin>0</DefaultMin>
                               <DefaultMax>20</DefaultMax>
                           </XValueRange>
                           <NParameters>1</NParameters>
                           <Default>
                               <ParameterA>
                                   <Type>Rate</Type>
                                   <Name>lambda</Name>
                                   <Symbol>Lambda</Symbol>
                                   <Abbreviation>lambda</Abbreviation>
                                   <Description>The expected number of occurrences that occur during the given period.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt 0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>4</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterA>
                           </Default>
                           <NAlternateParameterSets>0</NAlternateParameterSets>
                           <Types>
                               <PMF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>k</Label>
                                       <Units>Occurrences</Units>
                                   </XAxis>
                               </PMF>
                               <PMFLn>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>k</Label>
                                       <Units>Occurrences</Units>
                                   </XAxis>
                               </PMFLn>
                               <CDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                       <Units>Occurrences</Units>
                                   </XAxis>
                               </CDF>
                           </Types>
                       </Distribution>
                       <!--The Rayleigh Distribution-->
                       <Distribution>
                           <Name>Rayleigh</Name>
                           <Description>A continuous probability distribution for non-negative valued random variables.</Description>
                           <Usage>Model wind speed and wave heights. Model the lifetime of an item where the lifetime depends on the age of the object.</Usage>
                           <InformationLink>https://en.wikipedia.org/wiki/Rayleigh_distribution</InformationLink>
                           <InformationLink>https://numerics.mathdotnet.com/api/MathNet.Numerics.Distributions/Rayleigh.htm</InformationLink>
                           <InformationLink>https://www.statisticshowto.com/rayleigh-distribution/</InformationLink>
                           <InformationLink>https://www.tutorialspoint.com/statistics/rayleigh_distribution.htm</InformationLink>
                           <InformationLink></InformationLink>
                           <Continuity>Continuous</Continuity>
                           <NRandomVariables>Univariate</NRandomVariables>
                           <XValueRange>
                               <Minimum>0</Minimum>
                               <Maximum>+inf</Maximum>
                               <DefaultMin>0</DefaultMin>
                               <DefaultMax>10</DefaultMax>
                           </XValueRange>
                           <NParameters>1</NParameters>
                           <Default>
                               <ParameterA>
                                   <Type>Scale</Type>
                                   <Name>sigma</Name>
                                   <Symbol>Sigma</Symbol>
                                   <Abbreviation>scale</Abbreviation>
                                   <Description>The scale of the distribution.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt 0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>1</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterA>
                           </Default>
                           <NAlternateParameterSets>0</NAlternateParameterSets>
                           <Types>
                               <PDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDF>
                               <PDFLn>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDFLn>
                               <CDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </CDF>
                               <InvCDF>
                                   <XAxis>
                                       <Description>Probability</Description>
                                       <Label>p</Label>
                                   </XAxis>
                               </InvCDF>
                           </Types>
                       </Distribution>
                       <!--The Skewed Generalized Error Distribution-->
                       <Distribution>
                           <Name>Skewed Generalized Error</Name>
                           <Description>A special case of the Skewed Generalized T distribution (where parameter q is set to + infinity).</Description>
                           <Usage></Usage>
                           <InformationLink>https://en.wikipedia.org/wiki/Skewed_generalized_t_distribution#Skewed_generalized_error_distribution</InformationLink>
                           <InformationLink>https://numerics.mathdotnet.com/api/MathNet.Numerics.Distributions/SkewedGeneralizedError.htm</InformationLink>
                           <Continuity>Continuous</Continuity>
                           <NRandomVariables>Univariate</NRandomVariables>
                           <XValueRange>
                               <Minimum>-inf</Minimum>
                               <Maximum>+inf</Maximum>
                           </XValueRange>
                           <NParameters>4</NParameters>
                           <Default>
                               <ParameterA>
                                   <Type>Location</Type>
                                   <Name>mu</Name>
                                   <Symbol>Mu</Symbol>
                                   <Abbreviation>location</Abbreviation>
                                   <Description>The location of the distribution.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>-inf</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>0</Default>
                                   <AdjustMin>-10</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.5</Increment>
                               </ParameterA>
                               <ParameterB>
                                   <Type>Scale</Type>
                                   <Name>sigma</Name>
                                   <Symbol>Sigma</Symbol>
                                   <Abbreviation>scale</Abbreviation>
                                   <Description>The scale of the distribution.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt 0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>1</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterB>
                               <ParameterC>
                                   <Type>Skew</Type>
                                   <Name>lambda</Name>
                                   <Symbol>Lambda</Symbol>
                                   <Abbreviation>skew</Abbreviation>
                                   <Description>Skew parameter.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt -1</Minimum>
                                   <Maximum>lt 1</Maximum>
                                   <Default>0</Default>
                                   <AdjustMin>-1</AdjustMin>
                                   <AdjustMax>1</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterC>
                               <ParameterD>
                                   <Type>Kurtosis</Type>
                                   <Name>p</Name>
                                   <Symbol></Symbol>
                                   <Abbreviation>p</Abbreviation>
                                   <Description>Kurtosis parameter.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt 0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>1</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterD>
                           </Default>
                           <NAlternateParameterSets>0</NAlternateParameterSets>
                           <Types>
                               <PDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDF>
                               <PDFLn>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDFLn>
                               <CDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </CDF>
                               <InvCDF>
                                   <XAxis>
                                       <Description>Probability</Description>
                                       <Label>pr</Label>
                                   </XAxis>
                               </InvCDF>
                           </Types>
                       </Distribution>
                       <!--The Skewed Generalized T Distribution-->
                       <Distribution>
                           <Name>Skewed Generalized T</Name>
                           <Description>A highly flexible five parameter univariate distribution.</Description>
                           <Usage></Usage>
                           <InformationLink>https://en.wikipedia.org/wiki/Skewed_generalized_t_distribution#Skewed_generalized_error_distribution</InformationLink>
                           <InformationLink>https://numerics.mathdotnet.com/api/MathNet.Numerics.Distributions/SkewedGeneralizedT.htm</InformationLink>
                           <Continuity>Continuous</Continuity>
                           <NRandomVariables>Univariate</NRandomVariables>
                           <XValueRange>
                               <Minimum>-inf</Minimum>
                               <Maximum>+inf</Maximum>
                           </XValueRange>
                           <NParameters>5</NParameters>
                           <Default>
                               <ParameterA>
                                   <Type>Location</Type>
                                   <Name>mu</Name>
                                   <Symbol>Mu</Symbol>
                                   <Abbreviation>location</Abbreviation>
                                   <Description>The location of the distribution.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>-inf</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>0</Default>
                                   <AdjustMin>-10</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.5</Increment>
                               </ParameterA>
                               <ParameterB>
                                   <Type>Scale</Type>
                                   <Name>sigma</Name>
                                   <Symbol>Sigma</Symbol>
                                   <Abbreviation>scale</Abbreviation>
                                   <Description>The scale of the distribution.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt 0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>1</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterB>
                               <ParameterC>
                                   <Type>Skew</Type>
                                   <Name>lambda</Name>
                                   <Symbol>Lambda</Symbol>
                                   <Abbreviation>skew</Abbreviation>
                                   <Description>The skew.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt -1</Minimum>
                                   <Maximum>lt 1</Maximum>
                                   <Default>0</Default>
                                   <AdjustMin>-1</AdjustMin>
                                   <AdjustMax>1</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterC>
                               <ParameterD>
                                   <Type>Kurtosis</Type>
                                   <Name>p</Name>
                                   <Symbol></Symbol>
                                   <Abbreviation>p</Abbreviation>
                                   <Description>First kurtosis parameter. (Error if value lt ~1.415?)</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt 0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>2</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterD>
                               <ParameterE>
                                   <Type>Kurtosis</Type>
                                   <Name>q</Name>
                                   <Symbol></Symbol>
                                   <Abbreviation>q</Abbreviation>
                                   <Description>Second kurtosis parameter.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt 0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>1</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterE>
                           </Default>
                           <NAlternateParameterSets>0</NAlternateParameterSets>
                           <Types>
                               <PDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDF>
                               <PDFLn>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDFLn>
                               <CDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </CDF>
                               <InvCDF>
                                   <XAxis>
                                       <Description>Probability</Description>
                                       <Label>pr</Label>
                                   </XAxis>
                               </InvCDF>
                           </Types>
                       </Distribution>
                       <!--The Stable Distribution-->
                       <Distribution>
                           <Name>Stable</Name>
                           <Description>A distribution where the linear combination of two independent random variables with this distribution also has the same distribution. Can be considered a generaization of the Normal distribution.</Description>
                           <Usage>Suitable for modelling heavy tails and skewness.</Usage>
                           <InformationLink>https://en.wikipedia.org/wiki/Stable_distribution</InformationLink>
                           <InformationLink>https://numerics.mathdotnet.com/api/MathNet.Numerics.Distributions/Stable.htm</InformationLink>
                           <InformationLink>https://www.mathworks.com/help/stats/stable-distribution.html</InformationLink>
                           <InformationLink>https://www.randomservices.org/random/special/Stable.html</InformationLink>
                           <InformationLink>https://reference.wolfram.com/language/ref/StableDistribution.html</InformationLink>
                           <Continuity>Continuous</Continuity>
                           <NRandomVariables>Univariate</NRandomVariables>
                           <XValueRange>
                               <Minimum>-inf or Mu if Alpha lt 1 and Beta eq 1</Minimum>
                               <Maximum>+inf or Mu if Alpha lt 1 and Beta eq -1</Maximum>
                               <DefaultMin>-4</DefaultMin>
                               <DefaultMax>4</DefaultMax>
                           </XValueRange>
                           <NParameters>4</NParameters>
                           <Default>
                               <ParameterA>
                                   <Type>Shape</Type>
                                   <Name>alpha</Name>
                                   <Symbol>Alpha</Symbol>
                                   <Abbreviation>alpha</Abbreviation>
                                   <Description>First shape parameter - Stability.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt 0</Minimum>
                                   <Maximum>2</Maximum>
                                   <Default>1</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>2</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterA>
                               <ParameterB>
                                   <Type>Shape</Type>
                                   <Name>beta</Name>
                                   <Symbol>Beta</Symbol>
                                   <Abbreviation>beta</Abbreviation>
                                   <Description>Second shape parameter - Skewness.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>-1</Minimum>
                                   <Maximum>1</Maximum>
                                   <Default>0</Default>
                                   <AdjustMin>-1</AdjustMin>
                                   <AdjustMax>1</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterB>
                               <ParameterC>
                                   <Type>Scale</Type>
                                   <Name>c</Name>
                                   <Symbol></Symbol>
                                   <Abbreviation>scale</Abbreviation>
                                   <Description>The scale parameter.</Description>
                                   <NumberType></NumberType>
                                   <Minimum>gt 0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>1</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterC>
                               <ParameterD>
                                   <Type>Location</Type>
                                   <Name>mu</Name>
                                   <Symbol>Mu</Symbol>
                                   <Abbreviation>location</Abbreviation>
                                   <Description>The location parameter.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>-inf</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>0</Default>
                                   <AdjustMin>-10</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.5</Increment>
                               </ParameterD>
                           </Default>
                           <NAlternateParameterSets>0</NAlternateParameterSets>
                           <Types>
                               <PDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDF>
                               <PDFLn>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDFLn>
                               <CDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </CDF>
                           </Types>
                       </Distribution>
                       <!--The Student's T Distribution-->
                       <Distribution>
                           <Name>Student's T</Name>
                           <Description>The distribution was published by William Gosset in 1908 under the pseudonym "Student". Similar to the Normal distribution but with greater chance of extreme values (fatter tails).</Description>
                           <Usage>Used instead of the Normal distribution when the sample size is small (under 30) or the variance (or standard deviation) is unknown. The distribution is shorter and fatter than the Normal distribution.</Usage>
                           <InformationLink>https://en.wikipedia.org/wiki/Student%27s_t-distribution</InformationLink>
                           <InformationLink>https://numerics.mathdotnet.com/api/MathNet.Numerics.Distributions/StudentT.htm</InformationLink>
                           <InformationLink>https://www.statisticshowto.com/probability-and-statistics/t-distribution/</InformationLink>
                           <InformationLink>https://mathworld.wolfram.com/Studentst-Distribution.html</InformationLink>
                           <InformationLink>https://www.investopedia.com/terms/t/tdistribution.asp</InformationLink>
                           <InformationLink>https://www.mathworks.com/help/stats/students-t-distribution.html</InformationLink>
                           <Continuity>Continuous</Continuity>
                           <NRandomVariables>Univariate</NRandomVariables>
                           <XValueRange>
                               <Minimum>-inf</Minimum>
                               <Maximum>+inf</Maximum>
                               <DefaultMin>-5</DefaultMin>
                               <DefaultMax>5</DefaultMax>
                           </XValueRange>
                           <NParameters>3</NParameters>
                           <Default>
                               <ParameterA>
                                   <Type>Location</Type>
                                   <Name>mu</Name>
                                   <Symbol>Mu</Symbol>
                                   <Abbreviation>location</Abbreviation>
                                   <Description>The location of the distribution.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>-inf</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>0</Default>
                                   <AdjustMin>-10</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.5</Increment>
                               </ParameterA>
                               <ParameterB>
                                   <Type>Scale</Type>
                                   <Name>sigma</Name>
                                   <Symbol>Sigma</Symbol>
                                   <Abbreviation>scale</Abbreviation>
                                   <Description>The scale of the distribution.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt 0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>1</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterB>
                               <ParameterC>
                                   <Type>Freedom</Type>
                                   <Name>nu</Name>
                                   <Symbol>Nu</Symbol>
                                   <Abbreviation>freedom</Abbreviation>
                                   <Description>Degrees of freedom: number of samples minus one.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt 0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>4</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterC>
                           </Default>
                           <NAlternateParameterSets>0</NAlternateParameterSets>
                           <Types>
                               <PDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDF>
                               <PDFLn>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDFLn>
                               <CDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </CDF>
                               <InvCDF>
                                   <XAxis>
                                       <Description>Probability</Description>
                                       <Label>p</Label>
                                   </XAxis>
                               </InvCDF>
                           </Types>
                       </Distribution>
                       <!--The Triangular Distribution-->
                       <Distribution>
                           <Name>Triangular</Name>
                           <Description>A continuous probability distribution with a lower limit, an upper limit and a mode within these limits.</Description>
                           <Usage>A simplistic distribution used when limited sample data is available. Used in business and economic simulations, project management and modelling natural phenomena.</Usage>
                           <InformationLink>https://en.wikipedia.org/wiki/Triangular_distribution</InformationLink>
                           <InformationLink>https://numerics.mathdotnet.com/api/MathNet.Numerics.Distributions/Triangular.htm</InformationLink>
                           <InformationLink>https://www.mathworks.com/help/stats/triangular-distribution.html</InformationLink>
                           <InformationLink>https://www.statisticshowto.com/triangular-distribution/</InformationLink>
                           <Continuity>Continuous</Continuity>
                           <NRandomVariables>Univariate</NRandomVariables>
                           <XValueRange>
                               <Minimum>-inf</Minimum>
                               <Maximum>+inf</Maximum>
                               <DefaultMin>0</DefaultMin>
                               <DefaultMax>4</DefaultMax>
                           </XValueRange>
                           <NParameters>3</NParameters>
                           <Default>
                               <ParameterA>
                                   <Type></Type>
                                   <Name>Minimum</Name>
                                   <Symbol>a</Symbol>
                                   <Abbreviation>lower</Abbreviation>
                                   <Description>The minimum value.</Description>
                                   <NumberType>Real</NumberType>
                                   <Maximum>lt upper</Maximum>
                                   <Maximum>+inf</Maximum>
                                   <Default>1</Default>
                                   <AdjustMin>-10</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.5</Increment>
                               </ParameterA>
                               <ParameterB>
                                   <Type></Type>
                                   <Name>Maximum</Name>
                                   <Symbol>b</Symbol>
                                   <Abbreviation>upper</Abbreviation>
                                   <Description>The maximum value.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt lower</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>3</Default>
                                   <AdjustMin>-10</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.5</Increment>
                               </ParameterB>
                               <ParameterC>
                                   <Type></Type>
                                   <Name>Peak</Name>
                                   <Symbol>c</Symbol>
                                   <Abbreviation>mode</Abbreviation>
                                   <Description>The peak value.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt lower</Minimum>
                                   <Maximum>lt upper</Maximum>
                                   <Default>2</Default>
                                   <AdjustMin>-10</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.5</Increment>
                               </ParameterC>
                           </Default>
                           <NAlternateParameterSets>0</NAlternateParameterSets>
                           <Types>
                               <PDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDF>
                               <PDFLn>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDFLn>
                               <CDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </CDF>
                               <InvCDF>
                                   <XAxis>
                                       <Description>Probability</Description>
                                       <Label>p</Label>
                                   </XAxis>
                               </InvCDF>
                           </Types>
                       </Distribution>
                       <!--The Truncated Pareto Distribution-->
                       <Distribution>
                           <Name>Truncated Pareto</Name>
                           <Alias>Bounded Pareto</Alias>
                           <Description>A power-law probability distribution with upper and lower bounds.</Description>
                           <Usage></Usage>
                           <InformationLink>https://en.wikipedia.org/wiki/Pareto_distribution</InformationLink>
                           <InformationLink>https://numerics.mathdotnet.com/api/MathNet.Numerics.Distributions/TruncatedPareto.htm</InformationLink>
                           <InformationLink></InformationLink>
                           <InformationLink></InformationLink>
                           <InformationLink></InformationLink>
                           <Continuity>Continuous</Continuity>
                           <NRandomVariables>Univariate</NRandomVariables>
                           <XValueRange>
                               <Minimum>xm</Minimum>
                               <Maximum>T</Maximum>
                               <DefaultMin>0</DefaultMin>
                               <DefaultMax>10</DefaultMax>
                           </XValueRange>
                           <NParameters>3</NParameters>
                           <Default>
                               <ParameterA>
                                   <Type>Scale</Type>
                                   <Name>xm</Name>
                                   <Symbol></Symbol>
                                   <Abbreviation>scale</Abbreviation>
                                   <Description>The scale parameter. Minimum x value.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt 0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>1</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterA>
                               <ParameterB>
                                   <Type>Shape</Type>
                                   <Name>alpha</Name>
                                   <Symbol>Alpha</Symbol>
                                   <Abbreviation>shape</Abbreviation>
                                   <Description>The shape of the distribution.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt 0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>1</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterB>
                               <ParameterC>
                                   <Type>Truncation</Type>
                                   <Name>T</Name>
                                   <Symbol></Symbol>
                                   <Abbreviation>truncation</Abbreviation>
                                   <Description>x Truncation value or the maximum value.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt xm</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>8</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterC>
                           </Default>
                           <NAlternateParameterSets>0</NAlternateParameterSets>
                           <Types>
                               <PDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDF>
                               <PDFLn>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDFLn>
                               <CDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </CDF>
                               <InvCDF>
                                   <XAxis>
                                       <Description>Probability</Description>
                                       <Label>p</Label>
                                   </XAxis>
                               </InvCDF>
                           </Types>
                       </Distribution>
                       <!--The Weibull Distribution-->
                       <Distribution>
                           <Name>Weibull</Name>
                           <Description>The Weibull distribution is versatile because of the shape parameter.</Description>
                           <Usage>Used in reliability engineering. The two parameter Weibull is commonly used in failure analysis - no failure occurs before zero time.</Usage>
                           <InformationLink>https://en.wikipedia.org/wiki/Weibull_distribution</InformationLink>
                           <InformationLink>https://numerics.mathdotnet.com/api/MathNet.Numerics.Distributions/Weibull.htm</InformationLink>
                           <InformationLink></InformationLink>
                           <InformationLink></InformationLink>
                           <Continuity>Continuous</Continuity>
                           <NRandomVariables>Univariate</NRandomVariables>
                           <XValueRange>
                               <Minimum>0</Minimum>
                               <Maximum>+inf</Maximum>
                               <DefaultMin>0</DefaultMin>
                               <DefaultMax>3</DefaultMax>
                           </XValueRange>
                           <NParameters>2</NParameters>
                           <Default>
                               <ParameterA>
                                   <Type>Shape</Type>
                                   <Name>k</Name>
                                   <Symbol></Symbol>
                                   <Abbreviation>shape</Abbreviation>
                                   <Description>The shape parameter.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt 0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>2</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterA>
                               <ParameterB>
                                   <Type>Scale</Type>
                                   <Name>lambda</Name>
                                   <Symbol>Lambda</Symbol>
                                   <Abbreviation>scale</Abbreviation>
                                   <Description>The scale parameter.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>gt 0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>1</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterB>
                           </Default>
                           <NAlternateParameterSets>0</NAlternateParameterSets>
                           <Types>
                               <PDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDF>
                               <PDFLn>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </PDFLn>
                               <CDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                   </XAxis>
                               </CDF>
                           </Types>
                       </Distribution>
                       <!--The Zipf Distribution-->
                       <Distribution>
                           <Name>Zipf</Name>
                           <Alias>Discrete Pareto</Alias>
                           <Alias>Zeta</Alias>
                           <Description>A Zipf distribution is used to model data based on zipf's law, where the nth common item occurs 1/n times as often as the most common item.</Description>
                           <Usage>Commonly used in linguistics, insurance and modelling rare events.</Usage>
                           <InformationLink>https://en.wikipedia.org/wiki/Pareto_distribution</InformationLink>
                           <InformationLink>https://numerics.mathdotnet.com/api/MathNet.Numerics.Distributions/Zipf.htm</InformationLink>
                           <InformationLink>https://mathworld.wolfram.com/ZipfDistribution.html</InformationLink>
                           <InformationLink></InformationLink>
                           <Continuity>Discrete</Continuity>
                           <NRandomVariables>Univariate</NRandomVariables>
                           <XValueRange>
                               <Minimum>1</Minimum>
                               <Maximum>+inf</Maximum>
                               <DefaultMin>1</DefaultMin>
                               <DefaultMax>25</DefaultMax>
                           </XValueRange>
                           <NParameters>2</NParameters>
                           <Default>
                               <ParameterA>
                                   <Type></Type>
                                   <Name>s</Name>
                                   <Symbol></Symbol>
                                   <Abbreviation>s</Abbreviation>
                                   <Description>The value of the exponent used to characterize the distribution.</Description>
                                   <NumberType>Real</NumberType>
                                   <Minimum>-inf</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>2</Default>
                                   <AdjustMin>-10</AdjustMin>
                                   <AdjustMax>10</AdjustMax>
                                   <Increment>0.2</Increment>
                               </ParameterA>
                               <ParameterB>
                                   <Type></Type>
                                   <Name>n</Name>
                                   <Symbol></Symbol>
                                   <Abbreviation>n</Abbreviation>
                                   <Description>Number of elements in a population.</Description>
                                   <NumberType>Integer</NumberType>
                                   <Minimum>0</Minimum>
                                   <Maximum>+inf</Maximum>
                                   <Default>20</Default>
                                   <AdjustMin>0</AdjustMin>
                                   <AdjustMax>100</AdjustMax>
                                   <Increment>1</Increment>
                               </ParameterB>
                           </Default>
                           <NAlternateParameterSets>0</NAlternateParameterSets>
                           <Types>
                               <PMF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>k</Label>
                                       <Units>Item</Units>
                                   </XAxis>
                               </PMF>
                               <PMFLn>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>k</Label>
                                       <Units>Item</Units>
                                   </XAxis>
                               </PMFLn>
                               <CDF>
                                   <XAxis>
                                       <Description>Random Variable Value</Description>
                                       <Label>x</Label>
                                       <Units>Item</Units>
                                   </XAxis>
                               </CDF>
                           </Types>
                       </Distribution>
                   </DistributionList>

        'MathNet.Numerics.Distributions.

        'NOTE:
        'For Zipf distribution:
        'Types.PMF.XAxis.Minimum = 1
        'Types.PMF.XAxis.DefaultMin = 1
        'Types.PMFLn.XAxis.DefaultMin = 0
        'These properties have been moved to .XValueRange - Not sure why .PMF properties were different than .PMFLn properties

        '<Types>
        '    <PMF>
        '        <XAxis>
        '            <Description>Random Variable Value</Description>
        '            <Label>k</Label>
        '            <Units>Item</Units>
        '            <Minimum>1</Minimum>
        '            <DefaultMin>1</DefaultMin>
        '            <DefaultMax>25</DefaultMax>
        '        </XAxis>
        '    </PMF>
        '    <PMFLn>
        '        <XAxis>
        '            <Description>Random Variable Value</Description>
        '            <Label>k</Label>
        '            <Units>Item</Units>
        '            <DefaultMin>0</DefaultMin>
        '            <DefaultMax>25</DefaultMax>
        '        </XAxis>
        '    </PMFLn>
        '    <CDF>
        '        <XAxis>
        '            <Description>Random Variable Value</Description>
        '            <Label>x</Label>
        '            <Units>Item</Units>
        '            <DefaultMin>0</DefaultMin>
        '            <DefaultMax>25</DefaultMax>
        '        </XAxis>
        '    </CDF>
        '</Types>

        'NOTE: InvCDF types always have Minimum 0 and Maximum 1: (No need to specify for every distribution.)
        '<Minimum>0</Minimum>
        '<Maximum>1</Maximum>
        '<DefaultMin>0</DefaultMin>
        '<DefaultMax>1</DefaultMax>

    End Sub

    Private Sub ApplyDistInfo()
        'Apply the Distribution List Information to the combo boxes.

        Dim Distributions = From item In DistList.<DistributionList>.<Distribution>

        'Clear the distribution combo boxes:
        cmbCont1.Items.Clear()
        cmbCont2.Items.Clear()
        cmbCont3.Items.Clear()
        cmbCont4.Items.Clear()
        cmbCont5.Items.Clear()
        cmbDisc1.Items.Clear()
        cmbDisc2.Items.Clear()
        cmbDisc3.Items.Clear()
        cmbSpecial.Items.Clear()
        cmbAllDistributions.Items.Clear()

        For Each dist In Distributions
            'cmbAllDistributions.Items.Add(dist.<Name>.Value)
            If chkContinuous.Checked And dist.<Continuity>.Value = "Continuous" Then cmbAllDistributions.Items.Add(dist.<Name>.Value)
            If chkDiscrete.Checked And dist.<Continuity>.Value = "Discrete" Then cmbAllDistributions.Items.Add(dist.<Name>.Value)

            Select Case dist.<Continuity>.Value
                Case "Continuous"
                    Select Case dist.<NParameters>.Value
                        Case "1"
                            'Main.Message.AddWarning("1 parameter in distribution: " & dist.<Name>.Value & " To be coded!" & vbCrLf)
                            cmbCont1.Items.Add(dist.<Name>.Value)
                        Case "2"
                            cmbCont2.Items.Add(dist.<Name>.Value)
                        Case "3"
                            cmbCont3.Items.Add(dist.<Name>.Value)
                        Case "4"
                            cmbCont4.Items.Add(dist.<Name>.Value)
                        Case "5"
                            cmbCont5.Items.Add(dist.<Name>.Value)
                        Case Else
                            Main.Message.AddWarning("Unknown number of parameters: " & dist.<NParameters>.Value & " in distribution: " & dist.<Name>.Value & vbCrLf)
                    End Select
                Case "Discrete"
                    Select Case dist.<NParameters>.Value
                        Case "1"
                            cmbDisc1.Items.Add(dist.<Name>.Value)
                        Case "2"
                            cmbDisc2.Items.Add(dist.<Name>.Value)
                        Case "3"
                            cmbDisc3.Items.Add(dist.<Name>.Value)
                        Case Else
                            Main.Message.AddWarning("Unknown number of parameters: " & dist.<NParameters>.Value & " in distribution: " & dist.<Name>.Value & vbCrLf)
                    End Select
                Case Else
                    Main.Message.AddWarning("Unknown Continuity value: " & dist.<Continuity>.Value & " in distribution: " & dist.<Name>.Value & vbCrLf)
            End Select
        Next

        'Add a blank item to any combo box with no items
        If cmbCont1.Items.Count = 0 Then cmbCont1.Items.Add("")
        If cmbCont2.Items.Count = 0 Then cmbCont2.Items.Add("")
        If cmbCont3.Items.Count = 0 Then cmbCont3.Items.Add("")
        If cmbCont4.Items.Count = 0 Then cmbCont4.Items.Add("")
        If cmbCont5.Items.Count = 0 Then cmbCont5.Items.Add("")
        If cmbDisc1.Items.Count = 0 Then cmbDisc1.Items.Add("")
        If cmbDisc2.Items.Count = 0 Then cmbDisc2.Items.Add("")
        If cmbDisc3.Items.Count = 0 Then cmbDisc3.Items.Add("")
        'If cmbSpecial.Items.Count = 0 Then cmbSpecial.Items.Add("")
        If cmbAllDistributions.Items.Count = 0 Then cmbAllDistributions.Items.Add("")

        'Select the default items in the dostribution combo boxes
        cmbCont1.SelectedIndex = 0
        cmbCont2.SelectedIndex = 0
        cmbCont3.SelectedIndex = 0
        cmbCont4.SelectedIndex = 0
        cmbCont5.SelectedIndex = 0
        cmbDisc1.SelectedIndex = 0
        cmbDisc2.SelectedIndex = 0
        cmbDisc3.SelectedIndex = 0
        'cmbSpecial.SelectedIndex = 0
        cmbAllDistributions.SelectedIndex = 0

        If chkContinuous.Checked Then
            cmbCont1.Enabled = True
            cmbCont2.Enabled = True
            cmbCont3.Enabled = True
            cmbCont4.Enabled = True
            cmbCont5.Enabled = True
        Else
            cmbCont1.Enabled = False
            cmbCont2.Enabled = False
            cmbCont3.Enabled = False
            cmbCont4.Enabled = False
            cmbCont5.Enabled = False
        End If

        If chkDiscrete.Checked Then
            cmbDisc1.Enabled = True
            cmbDisc2.Enabled = True
            cmbDisc3.Enabled = True
        Else
            cmbDisc1.Enabled = False
            cmbDisc2.Enabled = False
            cmbDisc3.Enabled = False
        End If

    End Sub

    Private Sub cmbCont1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCont1.SelectedIndexChanged
        If cmbCont1.Focused Then
            DistributionType = "Continuous 1 parameter"
            DistributionName = cmbCont1.SelectedItem.ToString
        End If
    End Sub

    Private Sub cmbCont2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCont2.SelectedIndexChanged
        If cmbCont2.Focused Then
            DistributionType = "Continuous 2 parameter"
            DistributionName = cmbCont2.SelectedItem.ToString
        End If
    End Sub

    Private Sub cmbCont3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCont3.SelectedIndexChanged
        If cmbCont3.Focused Then
            DistributionType = "Continuous 3 parameter"
            DistributionName = cmbCont3.SelectedItem.ToString
        End If
    End Sub

    Private Sub cmbCont4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCont4.SelectedIndexChanged
        If cmbCont4.Focused Then
            DistributionType = "Continuous 4 parameter"
            DistributionName = cmbCont4.SelectedItem.ToString
        End If
    End Sub

    Private Sub cmbCont5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCont5.SelectedIndexChanged
        If cmbCont5.Focused Then
            DistributionType = "Continuous 5 parameter"
            DistributionName = cmbCont5.SelectedItem.ToString
        End If
    End Sub

    Private Sub cmbDisc1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDisc1.SelectedIndexChanged
        If cmbDisc1.Focused Then
            DistributionType = "Discrete 1 parameter"
            DistributionName = cmbDisc1.SelectedItem.ToString
        End If
    End Sub

    Private Sub cmbDisc2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDisc2.SelectedIndexChanged
        If cmbDisc2.Focused Then
            DistributionType = "Discrete 2 parameter"
            DistributionName = cmbDisc2.SelectedItem.ToString
        End If
    End Sub

    Private Sub cmbDisc3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDisc3.SelectedIndexChanged
        If cmbDisc3.Focused Then
            DistributionType = "Discrete 3 parameter"
            DistributionName = cmbDisc3.SelectedItem.ToString
        End If
    End Sub

    Private Sub cmbAllDistributions_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAllDistributions.SelectedIndexChanged
        If cmbAllDistributions.Focus Then
            DistributionName = cmbAllDistributions.SelectedItem.ToString
            Select Case DistInfo.<Continuity>.Value
                Case "Continuous"
                    Select Case DistInfo.<NParameters>.Value
                        Case "1"
                            cmbCont1.SelectedIndex = cmbCont1.FindStringExact(DistributionName)
                            DistributionType = "Continuous 1 parameter"
                        Case "2"
                            cmbCont2.SelectedIndex = cmbCont2.FindStringExact(DistributionName)
                            DistributionType = "Continuous 2 parameter"
                        Case "3"
                            cmbCont3.SelectedIndex = cmbCont3.FindStringExact(DistributionName)
                            DistributionType = "Continuous 3 parameter"
                        Case "4"
                            cmbCont4.SelectedIndex = cmbCont4.FindStringExact(DistributionName)
                            DistributionType = "Continuous 4 parameter"
                        Case "5"
                            cmbCont5.SelectedIndex = cmbCont5.FindStringExact(DistributionName)
                            DistributionType = "Continuous 5 parameter"
                        Case Else
                            Main.Message.AddWarning("Unknown number of parameters: " & DistInfo.<NParameters>.Value & vbCrLf)
                    End Select
                Case "Discrete"
                    Select Case DistInfo.<NParameters>.Value
                        Case "1"
                            cmbDisc1.SelectedIndex = cmbDisc1.FindStringExact(DistributionName)
                            DistributionType = "Discrete 1 parameter"
                        Case "2"
                            cmbDisc2.SelectedIndex = cmbDisc2.FindStringExact(DistributionName)
                            DistributionType = "Discrete 2 parameter"
                        Case "3"
                            cmbDisc3.SelectedIndex = cmbDisc3.FindStringExact(DistributionName)
                            DistributionType = "Discrete 3 parameter"
                        Case Else
                            Main.Message.AddWarning("Unknown number of parameters: " & DistInfo.<NParameters>.Value & vbCrLf)
                    End Select
                Case Else
                    Main.Message.AddWarning("Unknown Continuity parameter: " & DistInfo.<Continuity>.Value & vbCrLf)
            End Select
        End If
    End Sub


    Private Sub ShowDistributionInfo(ByVal DistName As String)
        'Display information about the distribution with the name DistName

        DistInfo = From item In DistList.<DistributionList>.<Distribution> Where item.<Name>.Value = DistName

        If DistInfo.<Name>.Value <> Nothing Then
            txtDistName.Text = DistName
            txtContinuity.Text = DistInfo.<Continuity>.Value
            If txtContinuity.Text = "Discrete" Then IsDiscreteDistribution = True Else IsDiscreteDistribution = False
            txtNRandomVariables.Text = DistInfo.<NRandomVariables>.Value
            NDistribParams = DistInfo.<NParameters>.Value
            txtNParams.Text = DistInfo.<NParameters>.Value
            txtDescription.Text = DistInfo.<Description>.Value
            txtUsage.Text = DistInfo.<Usage>.Value

            'If DistInfo.<Types>.<PDF>.DescendantNodes.Count = 0 Then rbPDF.Enabled = False Else rbPDF.Enabled = True
            'If DistInfo.<Types>.<PDFLn>.DescendantNodes.Count = 0 Then rbPDFLn.Enabled = False Else rbPDFLn.Enabled = True
            'If DistInfo.<Types>.<PMF>.DescendantNodes.Count = 0 Then rbPMF.Enabled = False Else rbPMF.Enabled = True
            'If DistInfo.<Types>.<PMFLn>.DescendantNodes.Count = 0 Then rbPMFLn.Enabled = False Else rbPMFLn.Enabled = True
            'If DistInfo.<Types>.<CDF>.DescendantNodes.Count = 0 Then rbCDF.Enabled = False Else rbCDF.Enabled = True
            'If DistInfo.<Types>.<InvCDF>.DescendantNodes.Count = 0 Then rbInvCDF.Enabled = False Else rbInvCDF.Enabled = True

            'If DistInfo.<Types>.<PDF>.DescendantNodes.Count = 0 Then lblPdf.Enabled = False Else lblPdf.Enabled = True
            'If DistInfo.<Types>.<PDFLn>.DescendantNodes.Count = 0 Then lblPdfLn.Enabled = False Else lblPdfLn.Enabled = True
            'If DistInfo.<Types>.<PMF>.DescendantNodes.Count = 0 Then lblPmf.Enabled = False Else lblPmf.Enabled = True
            'If DistInfo.<Types>.<PMFLn>.DescendantNodes.Count = 0 Then lblPmfLn.Enabled = False Else lblPmfLn.Enabled = True
            'If DistInfo.<Types>.<CDF>.DescendantNodes.Count = 0 Then lblCdf.Enabled = False Else lblCdf.Enabled = True
            'If DistInfo.<Types>.<InvCDF>.DescendantNodes.Count = 0 Then lblInvCdf.Enabled = False Else lblInvCdf.Enabled = True

            cmbFunctionType.Items.Clear()

            If DistInfo.<Types>.<PDF>.DescendantNodes.Count = 0 Then
                lblPdf.Enabled = False
            Else
                lblPdf.Enabled = True
                cmbFunctionType.Items.Add("PDF")
            End If
            If DistInfo.<Types>.<PDFLn>.DescendantNodes.Count = 0 Then
                lblPdfLn.Enabled = False
            Else
                lblPdfLn.Enabled = True
                cmbFunctionType.Items.Add("PDFLn")
            End If
            If DistInfo.<Types>.<PMF>.DescendantNodes.Count = 0 Then
                lblPmf.Enabled = False
            Else
                lblPmf.Enabled = True
                cmbFunctionType.Items.Add("PMF")
            End If
            If DistInfo.<Types>.<PMFLn>.DescendantNodes.Count = 0 Then
                lblPmfLn.Enabled = False
            Else
                lblPmfLn.Enabled = True
                cmbFunctionType.Items.Add("PMFLn")
            End If
            If DistInfo.<Types>.<CDF>.DescendantNodes.Count = 0 Then
                lblCdf.Enabled = False
            Else
                lblCdf.Enabled = True
                cmbFunctionType.Items.Add("CDF")
            End If
            If DistInfo.<Types>.<InvCDF>.DescendantNodes.Count = 0 Then
                lblInvCdf.Enabled = False
                cmbFunctionType.Items.Add("InvCDF") 'The InvCDF function value will be estimated by interpolating values from the CDF function.
            Else
                lblInvCdf.Enabled = True
                cmbFunctionType.Items.Add("InvCDF") 'The explicit InvCDF function will be used.
            End If

            If cmbFunctionType.Items.Count > 0 Then cmbFunctionType.SelectedIndex = 0

            If DistInfo.<XValueRange>.<Minimum>.Value = Nothing Then
                txtXMin.Text = ""
            Else
                txtXMin.Text = DistInfo.<XValueRange>.<Minimum>.Value
            End If

            If DistInfo.<XValueRange>.<Maximum>.Value = Nothing Then
                txtXMax.Text = ""
            Else
                txtXMax.Text = DistInfo.<XValueRange>.<Maximum>.Value
            End If

            'If rbPDF.Checked Then
            '    If rbPDF.Enabled = False Then
            '        If rbPMF.Enabled Then
            '            FunctionType = "PMF"
            '        ElseIf rbPMFLn.Enabled Then
            '            FunctionType = "PMFLn"
            '        ElseIf rbPDFLn.Enabled Then
            '            FunctionType = "PDFLn"
            '        ElseIf rbCDF.Enabled Then
            '            FunctionType = "CDF"
            '        ElseIf rbInvCDF.Enabled Then
            '            FunctionType = "InvCDF"
            '        Else
            '            'No valid sections!
            '        End If
            '    End If
            'ElseIf rbPDFLn.Checked Then
            '    If rbPDFLn.Enabled = False Then
            '        If rbPMFLn.Enabled Then
            '            FunctionType = "PMFLn"
            '        ElseIf rbPMF.Enabled Then
            '            FunctionType = "PMF"
            '        ElseIf rbPDF.Enabled Then
            '            FunctionType = "PDF"
            '        ElseIf rbCDF.Enabled Then
            '            FunctionType = "CDF"
            '        ElseIf rbInvCDF.Enabled Then
            '            FunctionType = "InvCDF"
            '        Else
            '            'No valid sections!
            '        End If
            '    End If
            'ElseIf rbPMF.Checked Then
            '    If rbPMF.Enabled = False Then
            '        If rbPDF.Enabled Then
            '            FunctionType = "PDF"
            '        ElseIf rbPDFLn.Enabled Then
            '            FunctionType = "PDFLn"
            '        ElseIf rbPMFLn.Enabled Then
            '            FunctionType = "PMFLn"
            '        ElseIf rbCDF.Enabled Then
            '            FunctionType = "CDF"
            '        ElseIf rbInvCDF.Enabled Then
            '            FunctionType = "InvCDF"
            '        Else
            '            'No valid sections!
            '        End If
            '    End If
            'ElseIf rbPMFLn.Checked Then
            '    If rbPMFLn.Enabled = False Then
            '        If rbPDFLn.Enabled Then
            '            FunctionType = "PDFLn"
            '        ElseIf rbPDF.Enabled Then
            '            FunctionType = "PDF"
            '        ElseIf rbPMF.Enabled Then
            '            FunctionType = "PMF"
            '        ElseIf rbCDF.Enabled Then
            '            FunctionType = "CDF"
            '        ElseIf rbInvCDF.Enabled Then
            '            FunctionType = "InvCDF"
            '        Else
            '            'No valid sections!
            '        End If
            '    ElseIf rbCDF.Checked Then
            '        If rbCDF.Enabled = False Then
            '            If rbPDF.Enabled Then
            '                FunctionType = "PDF"
            '            ElseIf rbPDFLn.Enabled Then
            '                FunctionType = "PDFLn"
            '            ElseIf rbPMF.Enabled Then
            '                FunctionType = "PMF"
            '            ElseIf rbPMFLn.Enabled Then
            '                FunctionType = "PMFLn"
            '            ElseIf rbInvCDF.Enabled Then
            '                FunctionType = "InvCDF"
            '            Else
            '                'No valid sections!
            '            End If
            '        End If
            '    ElseIf rbInvCDF.Checked Then
            '        If rbInvCDF.Enabled = False Then
            '            If rbCDF.Enabled Then
            '                FunctionType = "CDF"
            '            ElseIf rbPDF.Enabled Then
            '                FunctionType = "PDF"
            '            ElseIf rbPDFLn.Enabled Then
            '                FunctionType = "PDFLn"
            '            ElseIf rbPMF.Enabled Then
            '                FunctionType = "PMF"
            '            ElseIf rbPMFLn.Enabled Then
            '                FunctionType = "PMFLn"
            '            Else
            '                'No valid sections!
            '            End If
            '        Else 'Nothing has been checked
            '            'Pick the first possible type:
            '            If rbPDF.Enabled = True Then
            '                FunctionType = "PDF"
            '            ElseIf rbPDFLn.Enabled Then
            '                FunctionType = "PDFLn"
            '            ElseIf rbPMF.Enabled Then
            '                FunctionType = "PMF"
            '            ElseIf rbPMFLn.Enabled Then
            '                FunctionType = "PMFLn"
            '            ElseIf rbCDF.Enabled Then
            '                FunctionType = "CDF"
            '            ElseIf rbInvCDF.Enabled Then
            '                FunctionType = "InvCDF"
            '            End If
            '        End If
            '    End If
            'End If

            'UpdateFunctionTypeInfo()


            Dim NParams As Integer = DistInfo.<NParameters>.Value
            If NParams > 0 Then
                If DistInfo.<Default>.<ParameterA>.<Default>.Value = Nothing Then
                    txtAValue.Text = ""
                Else
                    txtAValue.Text = DistInfo.<Default>.<ParameterA>.<Default>.Value
                    ParamA = Val(txtAValue.Text)
                End If
                If DistInfo.<Default>.<ParameterA>.<Type>.Value = Nothing Then lblParamA.Text = "" Else lblParamA.Text = DistInfo.<Default>.<ParameterA>.<Type>.Value
                If DistInfo.<Default>.<ParameterA>.<Name>.Value = Nothing Then txtAName.Text = "" Else txtAName.Text = DistInfo.<Default>.<ParameterA>.<Name>.Value
                If DistInfo.<Default>.<ParameterA>.<Symbol>.Value = Nothing Then txtASymbol.Text = "" Else txtASymbol.Text = DistInfo.<Default>.<ParameterA>.<Symbol>.Value
                If DistInfo.<Default>.<ParameterA>.<NumberType>.Value = Nothing Then txtANoType.Text = "" Else txtANoType.Text = DistInfo.<Default>.<ParameterA>.<NumberType>.Value
                If DistInfo.<Default>.<ParameterA>.<Minimum>.Value = Nothing Then txtAMin.Text = "" Else txtAMin.Text = DistInfo.<Default>.<ParameterA>.<Minimum>.Value
                If DistInfo.<Default>.<ParameterA>.<Maximum>.Value = Nothing Then txtAMax.Text = "" Else txtAMax.Text = DistInfo.<Default>.<ParameterA>.<Maximum>.Value
                If DistInfo.<Default>.<ParameterA>.<Description>.Value = Nothing Then txtADescr.Text = "" Else txtADescr.Text = DistInfo.<Default>.<ParameterA>.<Description>.Value
                If DistInfo.<Default>.<ParameterA>.<AdjustMin>.Value = Nothing Then txtAAdjMin.Text = "" Else txtAAdjMin.Text = DistInfo.<Default>.<ParameterA>.<AdjustMin>.Value
                If DistInfo.<Default>.<ParameterA>.<AdjustMax>.Value = Nothing Then txtAAdjMax.Text = "" Else txtAAdjMax.Text = DistInfo.<Default>.<ParameterA>.<AdjustMax>.Value
                If DistInfo.<Default>.<ParameterA>.<Increment>.Value = Nothing Then txtAIncr.Text = "" Else txtAIncr.Text = DistInfo.<Default>.<ParameterA>.<Increment>.Value
                If NParams > 1 Then
                    If DistInfo.<Default>.<ParameterB>.<Default>.Value = Nothing Then
                        txtBValue.Text = ""
                    Else
                        txtBValue.Text = DistInfo.<Default>.<ParameterB>.<Default>.Value
                        ParamB = Val(txtBValue.Text)
                    End If
                    If DistInfo.<Default>.<ParameterB>.<Type>.Value = Nothing Then lblParamB.Text = "" Else lblParamB.Text = DistInfo.<Default>.<ParameterB>.<Type>.Value
                    If DistInfo.<Default>.<ParameterB>.<Name>.Value = Nothing Then txtBName.Text = "" Else txtBName.Text = DistInfo.<Default>.<ParameterB>.<Name>.Value
                    If DistInfo.<Default>.<ParameterB>.<Symbol>.Value = Nothing Then txtBSymbol.Text = "" Else txtBSymbol.Text = DistInfo.<Default>.<ParameterB>.<Symbol>.Value
                    If DistInfo.<Default>.<ParameterB>.<NumberType>.Value = Nothing Then txtBNoType.Text = "" Else txtBNoType.Text = DistInfo.<Default>.<ParameterB>.<NumberType>.Value
                    If DistInfo.<Default>.<ParameterB>.<Minimum>.Value = Nothing Then txtBMin.Text = "" Else txtBMin.Text = DistInfo.<Default>.<ParameterB>.<Minimum>.Value
                    If DistInfo.<Default>.<ParameterB>.<Maximum>.Value = Nothing Then txtBMax.Text = "" Else txtBMax.Text = DistInfo.<Default>.<ParameterB>.<Maximum>.Value
                    If DistInfo.<Default>.<ParameterB>.<Description>.Value = Nothing Then txtBDescr.Text = "" Else txtBDescr.Text = DistInfo.<Default>.<ParameterB>.<Description>.Value
                    If DistInfo.<Default>.<ParameterB>.<AdjustMin>.Value = Nothing Then txtBAdjMin.Text = "" Else txtBAdjMin.Text = DistInfo.<Default>.<ParameterB>.<AdjustMin>.Value
                    If DistInfo.<Default>.<ParameterB>.<AdjustMax>.Value = Nothing Then txtBAdjMax.Text = "" Else txtBAdjMax.Text = DistInfo.<Default>.<ParameterB>.<AdjustMax>.Value
                    If DistInfo.<Default>.<ParameterB>.<Increment>.Value = Nothing Then txtBIncr.Text = "" Else txtBIncr.Text = DistInfo.<Default>.<ParameterB>.<Increment>.Value
                    If NParams > 2 Then
                        If DistInfo.<Default>.<ParameterC>.<Default>.Value = Nothing Then
                            txtCValue.Text = ""
                        Else
                            txtCValue.Text = DistInfo.<Default>.<ParameterC>.<Default>.Value
                            ParamC = Val(txtCValue.Text)
                        End If
                        If DistInfo.<Default>.<ParameterC>.<Type>.Value = Nothing Then lblParamC.Text = "" Else lblParamC.Text = DistInfo.<Default>.<ParameterC>.<Type>.Value
                        If DistInfo.<Default>.<ParameterC>.<Name>.Value = Nothing Then txtCName.Text = "" Else txtCName.Text = DistInfo.<Default>.<ParameterC>.<Name>.Value
                        If DistInfo.<Default>.<ParameterC>.<Symbol>.Value = Nothing Then txtCSymbol.Text = "" Else txtCSymbol.Text = DistInfo.<Default>.<ParameterC>.<Symbol>.Value
                        If DistInfo.<Default>.<ParameterC>.<NumberType>.Value = Nothing Then txtCNoType.Text = "" Else txtCNoType.Text = DistInfo.<Default>.<ParameterC>.<NumberType>.Value
                        If DistInfo.<Default>.<ParameterC>.<Minimum>.Value = Nothing Then txtCMin.Text = "" Else txtCMin.Text = DistInfo.<Default>.<ParameterC>.<Minimum>.Value
                        If DistInfo.<Default>.<ParameterC>.<Maximum>.Value = Nothing Then txtCMax.Text = "" Else txtCMax.Text = DistInfo.<Default>.<ParameterC>.<Maximum>.Value
                        If DistInfo.<Default>.<ParameterC>.<Description>.Value = Nothing Then txtCDescr.Text = "" Else txtCDescr.Text = DistInfo.<Default>.<ParameterC>.<Description>.Value
                        If DistInfo.<Default>.<ParameterC>.<AdjustMin>.Value = Nothing Then txtCAdjMin.Text = "" Else txtCAdjMin.Text = DistInfo.<Default>.<ParameterC>.<AdjustMin>.Value
                        If DistInfo.<Default>.<ParameterC>.<AdjustMax>.Value = Nothing Then txtCAdjMax.Text = "" Else txtCAdjMax.Text = DistInfo.<Default>.<ParameterC>.<AdjustMax>.Value
                        If DistInfo.<Default>.<ParameterC>.<Increment>.Value = Nothing Then txtCIncr.Text = "" Else txtCIncr.Text = DistInfo.<Default>.<ParameterC>.<Increment>.Value
                        If NParams > 3 Then
                            If DistInfo.<Default>.<ParameterD>.<Default>.Value = Nothing Then
                                txtDValue.Text = ""
                            Else
                                txtDValue.Text = DistInfo.<Default>.<ParameterD>.<Default>.Value
                                ParamD = Val(txtDValue.Text)
                            End If
                            If DistInfo.<Default>.<ParameterD>.<Type>.Value = Nothing Then lblParamD.Text = "" Else lblParamD.Text = DistInfo.<Default>.<ParameterD>.<Type>.Value
                            If DistInfo.<Default>.<ParameterD>.<Name>.Value = Nothing Then txtDName.Text = "" Else txtDName.Text = DistInfo.<Default>.<ParameterD>.<Name>.Value
                            If DistInfo.<Default>.<ParameterD>.<Symbol>.Value = Nothing Then txtDSymbol.Text = "" Else txtDSymbol.Text = DistInfo.<Default>.<ParameterD>.<Symbol>.Value
                            If DistInfo.<Default>.<ParameterD>.<NumberType>.Value = Nothing Then txtDNoType.Text = "" Else txtDNoType.Text = DistInfo.<Default>.<ParameterD>.<NumberType>.Value
                            If DistInfo.<Default>.<ParameterD>.<Minimum>.Value = Nothing Then txtDMin.Text = "" Else txtDMin.Text = DistInfo.<Default>.<ParameterD>.<Minimum>.Value
                            If DistInfo.<Default>.<ParameterD>.<Maximum>.Value = Nothing Then txtDMax.Text = "" Else txtDMax.Text = DistInfo.<Default>.<ParameterD>.<Maximum>.Value
                            If DistInfo.<Default>.<ParameterD>.<Description>.Value = Nothing Then txtDDescr.Text = "" Else txtDDescr.Text = DistInfo.<Default>.<ParameterD>.<Description>.Value
                            If DistInfo.<Default>.<ParameterD>.<AdjustMin>.Value = Nothing Then txtDAdjMin.Text = "" Else txtDAdjMin.Text = DistInfo.<Default>.<ParameterD>.<AdjustMin>.Value
                            If DistInfo.<Default>.<ParameterD>.<AdjustMax>.Value = Nothing Then txtDAdjMax.Text = "" Else txtDAdjMax.Text = DistInfo.<Default>.<ParameterD>.<AdjustMax>.Value
                            If DistInfo.<Default>.<ParameterD>.<Increment>.Value = Nothing Then txtDIncr.Text = "" Else txtDIncr.Text = DistInfo.<Default>.<ParameterD>.<Increment>.Value
                            If NParams > 4 Then
                                If DistInfo.<Default>.<ParameterE>.<Default>.Value = Nothing Then
                                    txtEValue.Text = ""
                                Else
                                    txtEValue.Text = DistInfo.<Default>.<ParameterE>.<Default>.Value
                                    ParamE = Val(txtEValue.Text)
                                End If
                                If DistInfo.<Default>.<ParameterE>.<Type>.Value = Nothing Then lblParamE.Text = "" Else lblParamE.Text = DistInfo.<Default>.<ParameterE>.<Type>.Value
                                If DistInfo.<Default>.<ParameterE>.<Name>.Value = Nothing Then txtEName.Text = "" Else txtEName.Text = DistInfo.<Default>.<ParameterE>.<Name>.Value
                                If DistInfo.<Default>.<ParameterE>.<Symbol>.Value = Nothing Then txtESymbol.Text = "" Else txtESymbol.Text = DistInfo.<Default>.<ParameterE>.<Symbol>.Value
                                If DistInfo.<Default>.<ParameterE>.<NumberType>.Value = Nothing Then txtENoType.Text = "" Else txtENoType.Text = DistInfo.<Default>.<ParameterE>.<NumberType>.Value
                                If DistInfo.<Default>.<ParameterE>.<Minimum>.Value = Nothing Then txtEMin.Text = "" Else txtEMin.Text = DistInfo.<Default>.<ParameterE>.<Minimum>.Value
                                If DistInfo.<Default>.<ParameterE>.<Maximum>.Value = Nothing Then txtEMax.Text = "" Else txtEMax.Text = DistInfo.<Default>.<ParameterE>.<Maximum>.Value
                                If DistInfo.<Default>.<ParameterE>.<Description>.Value = Nothing Then txtEDescr.Text = "" Else txtEDescr.Text = DistInfo.<Default>.<ParameterE>.<Description>.Value
                                If DistInfo.<Default>.<ParameterE>.<AdjustMin>.Value = Nothing Then txtEAdjMin.Text = "" Else txtEAdjMin.Text = DistInfo.<Default>.<ParameterE>.<AdjustMin>.Value
                                If DistInfo.<Default>.<ParameterE>.<AdjustMax>.Value = Nothing Then txtEAdjMax.Text = "" Else txtEAdjMax.Text = DistInfo.<Default>.<ParameterE>.<AdjustMax>.Value
                                If DistInfo.<Default>.<ParameterE>.<Increment>.Value = Nothing Then txtEIncr.Text = "" Else txtEIncr.Text = DistInfo.<Default>.<ParameterE>.<Increment>.Value
                            End If
                        End If
                    End If
                End If
            End If
            UpdateStatistics()
        Else
            Main.Message.AddWarning("Distribution name: " & DistName & " not found in the list." & vbCrLf)
            txtDistName.Text = ""
            txtContinuity.Text = ""
            txtNRandomVariables.Text = ""
            txtNParams.Text = ""
            txtDescription.Text = ""
            lblParamA.Text = ""
            lblParamB.Text = ""
        End If

    End Sub

    Private Sub EnableParameters(ByVal N As Integer)
        'Enable the Distribution parameters

        'Clear the parameter text:
        lblParamA.Text = ""
        lblParamB.Text = ""
        lblParamC.Text = ""
        lblParamD.Text = ""
        lblParamE.Text = ""
        txtAValue.Text = ""
        txtBValue.Text = ""
        txtCValue.Text = ""
        txtDValue.Text = ""
        txtEValue.Text = ""
        txtAName.Text = ""
        txtBName.Text = ""
        txtCName.Text = ""
        txtDName.Text = ""
        txtEName.Text = ""
        txtASymbol.Text = ""
        txtBSymbol.Text = ""
        txtCSymbol.Text = ""
        txtDSymbol.Text = ""
        txtESymbol.Text = ""
        txtANoType.Text = ""
        txtBNoType.Text = ""
        txtCNoType.Text = ""
        txtDNoType.Text = ""
        txtENoType.Text = ""
        txtAMin.Text = ""
        txtBMin.Text = ""
        txtCMin.Text = ""
        txtDMin.Text = ""
        txtEMin.Text = ""
        txtAMax.Text = ""
        txtBMax.Text = ""
        txtCMax.Text = ""
        txtDMax.Text = ""
        txtEMax.Text = ""
        txtADescr.Text = ""
        txtBDescr.Text = ""
        txtCDescr.Text = ""
        txtDDescr.Text = ""
        txtEDescr.Text = ""

        If N = 1 Then
            'Set up the Parameter Input for one parameter:
            lblA.Enabled = True
            lblB.Enabled = False
            lblC.Enabled = False
            lblD.Enabled = False
            lblE.Enabled = False
            txtAValue.Enabled = True
            txtBValue.Enabled = False
            txtCValue.Enabled = False
            txtDValue.Enabled = False
            txtEValue.Enabled = False
            txtAName.Enabled = True
            txtBName.Enabled = False
            txtCName.Enabled = False
            txtDName.Enabled = False
            txtEName.Enabled = False
            txtASymbol.Enabled = True
            txtBSymbol.Enabled = False
            txtCSymbol.Enabled = False
            txtDSymbol.Enabled = False
            txtESymbol.Enabled = False
            txtANoType.Enabled = True
            txtBNoType.Enabled = False
            txtCNoType.Enabled = False
            txtDNoType.Enabled = False
            txtENoType.Enabled = False
            txtAMin.Enabled = True
            txtBMin.Enabled = False
            txtCMin.Enabled = False
            txtDMin.Enabled = False
            txtEMin.Enabled = False
            txtAMax.Enabled = True
            txtBMax.Enabled = False
            txtCMax.Enabled = False
            txtDMax.Enabled = False
            txtEMax.Enabled = False
            txtADescr.Enabled = True
            txtBDescr.Enabled = False
            txtCDescr.Enabled = False
            txtDDescr.Enabled = False
            txtEDescr.Enabled = False
        ElseIf N = 2 Then
            'Set up the Parameter Input for two parameters:
            lblA.Enabled = True
            lblB.Enabled = True
            lblC.Enabled = False
            lblD.Enabled = False
            lblE.Enabled = False
            txtAValue.Enabled = True
            txtBValue.Enabled = True
            txtCValue.Enabled = False
            txtDValue.Enabled = False
            txtEValue.Enabled = False
            txtAName.Enabled = True
            txtBName.Enabled = True
            txtCName.Enabled = False
            txtDName.Enabled = False
            txtEName.Enabled = False
            txtASymbol.Enabled = True
            txtBSymbol.Enabled = True
            txtCSymbol.Enabled = False
            txtDSymbol.Enabled = False
            txtESymbol.Enabled = False
            txtANoType.Enabled = True
            txtBNoType.Enabled = True
            txtCNoType.Enabled = False
            txtDNoType.Enabled = False
            txtENoType.Enabled = False
            txtAMin.Enabled = True
            txtBMin.Enabled = True
            txtCMin.Enabled = False
            txtDMin.Enabled = False
            txtEMin.Enabled = False
            txtAMax.Enabled = True
            txtBMax.Enabled = True
            txtCMax.Enabled = False
            txtDMax.Enabled = False
            txtEMax.Enabled = False
            txtADescr.Enabled = True
            txtBDescr.Enabled = True
            txtCDescr.Enabled = False
            txtDDescr.Enabled = False
            txtEDescr.Enabled = False
        ElseIf N = 3 Then
            'Set up the Parameter Input for three parameters:
            lblA.Enabled = True
            lblB.Enabled = True
            lblC.Enabled = True
            lblD.Enabled = False
            lblE.Enabled = False
            txtAValue.Enabled = True
            txtBValue.Enabled = True
            txtCValue.Enabled = True
            txtDValue.Enabled = False
            txtEValue.Enabled = False
            txtAName.Enabled = True
            txtBName.Enabled = True
            txtCName.Enabled = True
            txtDName.Enabled = False
            txtEName.Enabled = False
            txtASymbol.Enabled = True
            txtBSymbol.Enabled = True
            txtCSymbol.Enabled = True
            txtDSymbol.Enabled = False
            txtESymbol.Enabled = False
            txtANoType.Enabled = True
            txtBNoType.Enabled = True
            txtCNoType.Enabled = True
            txtDNoType.Enabled = False
            txtENoType.Enabled = False
            txtAMin.Enabled = True
            txtBMin.Enabled = True
            txtCMin.Enabled = True
            txtDMin.Enabled = False
            txtEMin.Enabled = False
            txtAMax.Enabled = True
            txtBMax.Enabled = True
            txtCMax.Enabled = True
            txtDMax.Enabled = False
            txtEMax.Enabled = False
            txtADescr.Enabled = True
            txtBDescr.Enabled = True
            txtCDescr.Enabled = True
            txtDDescr.Enabled = False
            txtEDescr.Enabled = False
        ElseIf N = 4 Then
            'Set up the Parameter Input for four parameters:
            lblA.Enabled = True
            lblB.Enabled = True
            lblC.Enabled = True
            lblD.Enabled = True
            lblE.Enabled = False
            txtAValue.Enabled = True
            txtBValue.Enabled = True
            txtCValue.Enabled = True
            txtDValue.Enabled = True
            txtEValue.Enabled = False
            txtAName.Enabled = True
            txtBName.Enabled = True
            txtCName.Enabled = True
            txtDName.Enabled = True
            txtEName.Enabled = False
            txtASymbol.Enabled = True
            txtBSymbol.Enabled = True
            txtCSymbol.Enabled = True
            txtDSymbol.Enabled = True
            txtESymbol.Enabled = False
            txtANoType.Enabled = True
            txtBNoType.Enabled = True
            txtCNoType.Enabled = True
            txtDNoType.Enabled = True
            txtENoType.Enabled = False
            txtAMin.Enabled = True
            txtBMin.Enabled = True
            txtCMin.Enabled = True
            txtDMin.Enabled = True
            txtEMin.Enabled = False
            txtAMax.Enabled = True
            txtBMax.Enabled = True
            txtCMax.Enabled = True
            txtDMax.Enabled = True
            txtEMax.Enabled = False
            txtADescr.Enabled = True
            txtBDescr.Enabled = True
            txtCDescr.Enabled = True
            txtDDescr.Enabled = True
            txtEDescr.Enabled = False
        ElseIf N = 5 Then
            'Set up the Parameter Input for five parameters:
            lblA.Enabled = True
            lblB.Enabled = True
            lblC.Enabled = True
            lblD.Enabled = True
            lblE.Enabled = True
            txtAValue.Enabled = True
            txtBValue.Enabled = True
            txtCValue.Enabled = True
            txtDValue.Enabled = True
            txtEValue.Enabled = True
            txtAName.Enabled = True
            txtBName.Enabled = True
            txtCName.Enabled = True
            txtDName.Enabled = True
            txtEName.Enabled = True
            txtASymbol.Enabled = True
            txtBSymbol.Enabled = True
            txtCSymbol.Enabled = True
            txtDSymbol.Enabled = True
            txtESymbol.Enabled = True
            txtANoType.Enabled = True
            txtBNoType.Enabled = True
            txtCNoType.Enabled = True
            txtDNoType.Enabled = True
            txtENoType.Enabled = True
            txtAMin.Enabled = True
            txtBMin.Enabled = True
            txtCMin.Enabled = True
            txtDMin.Enabled = True
            txtEMin.Enabled = True
            txtAMax.Enabled = True
            txtBMax.Enabled = True
            txtCMax.Enabled = True
            txtDMax.Enabled = True
            txtEMax.Enabled = True
            txtADescr.Enabled = True
            txtBDescr.Enabled = True
            txtCDescr.Enabled = True
            txtDDescr.Enabled = True
            txtEDescr.Enabled = True
        Else
            Main.Message.AddWarning("More than 5 distribution parameters specified: " & N & vbCrLf)
        End If

    End Sub

    Private Sub rbContinuous1_CheckedChanged(sender As Object, e As EventArgs) Handles rbContinuous1.CheckedChanged
        If rbContinuous1.Checked Then
            EnableParameters(1)
            If cmbCont1.SelectedIndex = -1 Then cmbCont1.SelectedIndex = 0
            DistributionName = cmbCont1.SelectedItem.ToString
            _distributionType = "Continuous 1 parameter"
        End If
    End Sub

    Private Sub rbContinuous2_CheckedChanged(sender As Object, e As EventArgs) Handles rbContinuous2.CheckedChanged
        If rbContinuous2.Checked Then
            EnableParameters(2)
            If cmbCont2.SelectedIndex = -1 Then cmbCont2.SelectedIndex = 0
            DistributionName = cmbCont2.SelectedItem.ToString
            _distributionType = "Continuous 2 parameter"
        End If
    End Sub

    Private Sub rbContinuous3_CheckedChanged(sender As Object, e As EventArgs) Handles rbContinuous3.CheckedChanged
        If rbContinuous3.Checked Then
            EnableParameters(3)
            If cmbCont3.SelectedIndex = -1 Then cmbCont3.SelectedIndex = 0
            DistributionName = cmbCont3.SelectedItem.ToString
            _distributionType = "Continuous 3 parameter"
        End If
    End Sub

    Private Sub rbContinuous4_CheckedChanged(sender As Object, e As EventArgs) Handles rbContinuous4.CheckedChanged
        If rbContinuous4.Checked Then
            EnableParameters(4)
            If cmbCont4.SelectedIndex = -1 Then cmbCont4.SelectedIndex = 0
            DistributionName = cmbCont4.SelectedItem.ToString
            _distributionType = "Continuous 4 parameter"
        End If
    End Sub

    Private Sub rbContinuous5_CheckedChanged(sender As Object, e As EventArgs) Handles rbContinuous5.CheckedChanged
        If rbContinuous5.Checked Then
            EnableParameters(5)
            If cmbCont5.SelectedIndex = -1 Then cmbCont5.SelectedIndex = 0
            DistributionName = cmbCont5.SelectedItem.ToString
            _distributionType = "Continuous 5 parameter"
        End If
    End Sub

    Private Sub rbDiscrete1_CheckedChanged(sender As Object, e As EventArgs) Handles rbDiscrete1.CheckedChanged
        If rbDiscrete1.Checked Then
            EnableParameters(1)
            If cmbDisc1.SelectedIndex = -1 Then cmbDisc1.SelectedIndex = 0
            DistributionName = cmbDisc1.SelectedItem.ToString
            _distributionType = "Discrete 1 parameter"
        End If
    End Sub

    Private Sub rbDiscrete2_CheckedChanged(sender As Object, e As EventArgs) Handles rbDiscrete2.CheckedChanged
        If rbDiscrete2.Checked Then
            EnableParameters(2)
            If cmbDisc2.SelectedIndex = -1 Then cmbDisc2.SelectedIndex = 0
            DistributionName = cmbDisc2.SelectedItem.ToString
            _distributionType = "Discrete 2 parameter"
        End If
    End Sub

    Private Sub rbDiscrete3_CheckedChanged(sender As Object, e As EventArgs) Handles rbDiscrete3.CheckedChanged
        If rbDiscrete3.Checked Then
            EnableParameters(3)
            If cmbDisc3.SelectedIndex = -1 Then cmbDisc3.SelectedIndex = 0
            DistributionName = cmbDisc3.SelectedItem.ToString
            _distributionType = "Discrete 3 parameter"
        End If
    End Sub

    Private Sub chkContinuous_CheckedChanged(sender As Object, e As EventArgs) Handles chkContinuous.CheckedChanged
        ApplyDistInfo()
    End Sub

    Private Sub chkDiscrete_CheckedChanged(sender As Object, e As EventArgs) Handles chkDiscrete.CheckedChanged
        ApplyDistInfo()
    End Sub

    Private Sub btnApply_Click(sender As Object, e As EventArgs) Handles btnApply.Click
        'Apply the selected distribution.

        Dim DistribInfo As New Main.clsDistribInfo
        DistribInfo.Name = DistributionName
        DistribInfo.Continuity = txtContinuity.Text
        DistribInfo.NParams = txtNParams.Text
        DistribInfo.RangeMin = txtXMin.Text
        DistribInfo.RangeMax = txtXMax.Text

        DistribInfo.PdfValid = lblPdf.Enabled
        DistribInfo.PdfLnValid = lblPdfLn.Enabled
        DistribInfo.PmfValid = lblPmf.Enabled
        DistribInfo.PmfLnValid = lblPmfLn.Enabled
        DistribInfo.CdfValid = lblCdf.Enabled
        DistribInfo.InvCdfValid = lblInvCdf.Enabled

        'DistribInfo.ParamA = txtAValue.Text
        DistribInfo.ParamAInfo.Value = txtAValue.Text
        DistribInfo.ParamAInfo.Name = txtAName.Text
        DistribInfo.ParamAInfo.Type = lblParamA.Text 'Added 12Oct22
        DistribInfo.ParamAInfo.Symbol = txtASymbol.Text
        DistribInfo.ParamAInfo.NumberType = txtANoType.Text
        DistribInfo.ParamAInfo.Minimum = txtAMin.Text
        DistribInfo.ParamAInfo.Maximum = txtAMax.Text
        DistribInfo.ParamAInfo.Description = txtADescr.Text
        DistribInfo.ParamAInfo.AdjustMin = txtAAdjMin.Text
        DistribInfo.ParamAInfo.AdjustMax = txtAAdjMax.Text
        DistribInfo.ParamAInfo.Increment = txtAIncr.Text

        If DistribInfo.NParams > 1 Then
            'DistribInfo.ParamB = txtBValue.Text
            DistribInfo.ParamBInfo.Value = txtBValue.Text
            DistribInfo.ParamBInfo.Name = txtBName.Text
            DistribInfo.ParamBInfo.Type = lblParamB.Text 'Added 12Oct22
            DistribInfo.ParamBInfo.Symbol = txtBSymbol.Text
            DistribInfo.ParamBInfo.NumberType = txtBNoType.Text
            DistribInfo.ParamBInfo.Minimum = txtBMin.Text
            DistribInfo.ParamBInfo.Maximum = txtBMax.Text
            DistribInfo.ParamBInfo.Description = txtBDescr.Text
            DistribInfo.ParamBInfo.AdjustMin = txtBAdjMin.Text
            DistribInfo.ParamBInfo.AdjustMax = txtBAdjMax.Text
            DistribInfo.ParamBInfo.Increment = txtBIncr.Text
            If DistribInfo.NParams > 2 Then
                'DistribInfo.ParamC = txtCValue.Text
                DistribInfo.ParamCInfo.Value = txtCValue.Text
                DistribInfo.ParamCInfo.Name = txtCName.Text
                DistribInfo.ParamCInfo.Type = lblParamC.Text 'Added 12Oct22
                DistribInfo.ParamCInfo.Symbol = txtCSymbol.Text
                DistribInfo.ParamCInfo.NumberType = txtCNoType.Text
                DistribInfo.ParamCInfo.Minimum = txtCMin.Text
                DistribInfo.ParamCInfo.Maximum = txtCMax.Text
                DistribInfo.ParamCInfo.Description = txtCDescr.Text
                DistribInfo.ParamCInfo.AdjustMin = txtCAdjMin.Text
                DistribInfo.ParamCInfo.AdjustMax = txtCAdjMax.Text
                DistribInfo.ParamCInfo.Increment = txtCIncr.Text
                If DistribInfo.NParams > 3 Then
                    'DistribInfo.ParamD = txtDValue.Text
                    DistribInfo.ParamDInfo.Value = txtDValue.Text
                    DistribInfo.ParamDInfo.Name = txtDName.Text
                    DistribInfo.ParamDInfo.Type = lblParamD.Text 'Added 12Oct22
                    DistribInfo.ParamDInfo.Symbol = txtDSymbol.Text
                    DistribInfo.ParamDInfo.NumberType = txtDNoType.Text
                    DistribInfo.ParamDInfo.Minimum = txtDMin.Text
                    DistribInfo.ParamDInfo.Maximum = txtDMax.Text
                    DistribInfo.ParamDInfo.Description = txtDDescr.Text
                    DistribInfo.ParamDInfo.AdjustMin = txtDAdjMin.Text
                    DistribInfo.ParamDInfo.AdjustMax = txtDAdjMax.Text
                    DistribInfo.ParamDInfo.Increment = txtDIncr.Text
                    If DistribInfo.NParams > 4 Then
                        'DistribInfo.ParamE = txtEValue.Text
                        DistribInfo.ParamEInfo.Value = txtEValue.Text
                        DistribInfo.ParamEInfo.Name = txtEName.Text
                        DistribInfo.ParamEInfo.Type = lblParamE.Text 'Added 12Oct22
                        DistribInfo.ParamEInfo.Symbol = txtESymbol.Text
                        DistribInfo.ParamEInfo.NumberType = txtENoType.Text
                        DistribInfo.ParamEInfo.Minimum = txtEMin.Text
                        DistribInfo.ParamEInfo.Maximum = txtEMax.Text
                        DistribInfo.ParamEInfo.Description = txtEDescr.Text
                        DistribInfo.ParamEInfo.AdjustMin = txtEAdjMin.Text
                        DistribInfo.ParamEInfo.AdjustMax = txtEAdjMax.Text
                        DistribInfo.ParamEInfo.Increment = txtEIncr.Text
                    End If
                End If
            End If
        End If
        RaiseEvent DistributionInfo(DistribInfo)
    End Sub

    Private Sub btnCalcDistValue_Click(sender As Object, e As EventArgs) Handles btnCalcDistValue.Click
        'Calculate the Distribution value.
        txtXVal.Text = Val(txtXVal.Text)
        If txtXVal.Text.Trim = "" Then
            If cmbFunctionType.SelectedItem.ToString = "InvCDF" Then
                Main.Message.AddWarning("No probability value entered." & vbCrLf)
            Else
                Main.Message.AddWarning("No X value entered." & vbCrLf)
            End If
        Else
            Dim Params(0 To NDistribParams - 1) As Double
            Params(0) = txtAValue.Text
            If NDistribParams > 1 Then
                Params(1) = txtBValue.Text
                If NDistribParams > 2 Then
                    Params(2) = txtCValue.Text
                    If NDistribParams > 3 Then
                        Params(3) = txtDValue.Text
                        If NDistribParams > 4 Then
                            Params(4) = txtEValue.Text
                        End If
                    End If
                End If
            End If

            txtDistVal.Text = Format(Main.Distribution.DistributionValue(DistributionName, Params, cmbFunctionType.SelectedItem.ToString, txtXVal.Text), txtFormat.Text)
        End If
    End Sub

    Private Sub btnFormatHelp_Click(sender As Object, e As EventArgs) Handles btnFormatHelp.Click
        MessageBox.Show("Format string examples:" & vbCrLf & "N4 - Number displayed with thousands separator and 4 decimal places" & vbCrLf & "F4 - Number displayed with 4 decimal places.", "Number Formatting")
    End Sub

    Private Sub UpdateStatistics()
        'Update the distribution statitics.

        'Dim Distrib As Object 'This will store the MathNet distribution class used to calculate the distribution parameters such has Entropy, Mean, Median, Mode, Skewness, StdDev and Variance.

        Select Case DistributionName
            Case "Bernoulli"
                'This is a discrete distribution.
                Dim Distrib As New MathNet.Numerics.Distributions.Bernoulli(ParamA)
                txtXMinStat.Text = Distrib.Minimum
                txtXMaxStat.Text = Distrib.Maximum
                txtEntropy.Text = Distrib.Entropy
                txtMean.Text = Distrib.Mean
                txtMedian.Text = Distrib.Median
                txtMode.Text = Distrib.Mode
                txtStdDev.Text = Distrib.StdDev
                txtVariance.Text = Distrib.Variance
                txtSkewness.Text = Distrib.Skewness

            Case "Beta"
                Dim Distrib As New MathNet.Numerics.Distributions.Beta(ParamA, ParamB)
                txtXMinStat.Text = Distrib.Minimum
                txtXMaxStat.Text = Distrib.Maximum
                txtEntropy.Text = Distrib.Entropy
                txtMean.Text = Distrib.Mean
                'txtMedian.Text = Distrib.Median
                txtMedian.Text = ""
                txtMode.Text = Distrib.Mode
                txtStdDev.Text = Distrib.StdDev
                txtVariance.Text = Distrib.Variance
                txtSkewness.Text = Distrib.Skewness

            Case "Beta Scaled"
                Dim Distrib As New MathNet.Numerics.Distributions.BetaScaled(ParamA, ParamB, ParamC, ParamD)
                txtXMinStat.Text = Distrib.Minimum
                txtXMaxStat.Text = Distrib.Maximum
                'txtEntropy.Text = Distrib.Entropy
                txtEntropy.Text = ""
                txtMean.Text = Distrib.Mean
                'txtMedian.Text = Distrib.Median
                txtMedian.Text = ""
                txtMode.Text = Distrib.Mode
                txtStdDev.Text = Distrib.StdDev
                txtVariance.Text = Distrib.Variance
                txtSkewness.Text = Distrib.Skewness

            Case "Binomial"
                'This is a discrete distribution.
                Dim Distrib As New MathNet.Numerics.Distributions.Binomial(ParamA, ParamB)
                txtXMinStat.Text = Distrib.Minimum
                txtXMaxStat.Text = Distrib.Maximum
                txtEntropy.Text = Distrib.Entropy
                txtMean.Text = Distrib.Mean
                txtMedian.Text = Distrib.Median
                txtMode.Text = Distrib.Mode
                txtStdDev.Text = Distrib.StdDev
                txtVariance.Text = Distrib.Variance
                txtSkewness.Text = Distrib.Skewness

            Case "Burr"
                Dim Distrib As New MathNet.Numerics.Distributions.Burr(ParamA, ParamB, ParamC)
                txtXMinStat.Text = Distrib.Minimum
                txtXMaxStat.Text = Distrib.Maximum
                'txtEntropy.Text = Distrib.Entropy
                txtEntropy.Text = ""
                txtMean.Text = Distrib.Mean
                txtMedian.Text = Distrib.Median
                txtMode.Text = Distrib.Mode
                txtStdDev.Text = Distrib.StdDev
                txtVariance.Text = Distrib.Variance
                'txtSkewness.Text = Distrib.Skewness
                txtSkewness.Text = ""

            Case "Categorical"
                'This is a discrete distribution.


            Case "Cauchy"
                Dim Distrib As New MathNet.Numerics.Distributions.Cauchy(ParamA, ParamB)
                txtXMinStat.Text = Distrib.Minimum
                txtXMaxStat.Text = Distrib.Maximum
                txtEntropy.Text = Distrib.Entropy
                'txtMean.Text = Distrib.Mean
                txtMean.Text = ""
                txtMedian.Text = Distrib.Median
                txtMode.Text = Distrib.Mode
                'txtStdDev.Text = Distrib.StdDev
                txtStdDev.Text = ""
                'txtVariance.Text = Distrib.Variance
                txtVariance.Text = ""
                'txtSkewness.Text = Distrib.Skewness
                txtSkewness.Text = ""

            Case "Chi"
                Dim Distrib As New MathNet.Numerics.Distributions.Chi(ParamA)
                txtXMinStat.Text = Distrib.Minimum
                txtXMaxStat.Text = Distrib.Maximum
                txtEntropy.Text = Distrib.Entropy
                txtMean.Text = Distrib.Mean
                'txtMedian.Text = Distrib.Median
                txtMedian.Text = ""
                txtMode.Text = Distrib.Mode
                txtStdDev.Text = Distrib.StdDev
                txtVariance.Text = Distrib.Variance
                txtSkewness.Text = Distrib.Skewness



            Case "Chi Squared"
                Dim Distrib As New MathNet.Numerics.Distributions.ChiSquared(ParamA)
                txtXMinStat.Text = Distrib.Minimum
                txtXMaxStat.Text = Distrib.Maximum
                txtEntropy.Text = Distrib.Entropy
                txtMean.Text = Distrib.Mean
                txtMedian.Text = Distrib.Median
                txtMode.Text = Distrib.Mode
                txtStdDev.Text = Distrib.StdDev
                txtVariance.Text = Distrib.Variance
                txtSkewness.Text = Distrib.Skewness

            Case "Continuous Uniform"
                Dim Distrib As New MathNet.Numerics.Distributions.ContinuousUniform(ParamA, ParamB)
                txtXMinStat.Text = Distrib.Minimum
                txtXMaxStat.Text = Distrib.Maximum
                txtEntropy.Text = Distrib.Entropy
                txtMean.Text = Distrib.Mean
                txtMedian.Text = Distrib.Median
                txtMode.Text = Distrib.Mode
                txtStdDev.Text = Distrib.StdDev
                txtVariance.Text = Distrib.Variance
                txtSkewness.Text = Distrib.Skewness

            Case "Conway-Maxwell-Poisson"
                'This is a discrete distribution.
                Dim Distrib As New MathNet.Numerics.Distributions.ConwayMaxwellPoisson(ParamA, ParamB)
                txtXMinStat.Text = Distrib.Minimum
                'txtXMaxStat.Text = Distrib.Maximum
                txtXMaxStat.Text = ""
                'txtEntropy.Text = Distrib.Entropy
                txtEntropy.Text = ""
                txtMean.Text = Distrib.Mean
                'txtMedian.Text = Distrib.Median
                txtMedian.Text = ""
                'txtMode.Text = Distrib.Mode
                txtMode.Text = ""
                txtStdDev.Text = Distrib.StdDev
                txtVariance.Text = Distrib.Variance
                'txtSkewness.Text = Distrib.Skewness
                txtSkewness.Text = ""

            Case "Discrete Uniform"
                'This is a discrete distribution.
                Dim Distrib As New MathNet.Numerics.Distributions.DiscreteUniform(ParamA, ParamB)
                txtXMinStat.Text = Distrib.Minimum
                txtXMaxStat.Text = Distrib.Maximum
                txtEntropy.Text = Distrib.Entropy
                txtMean.Text = Distrib.Mean
                txtMedian.Text = Distrib.Median
                txtMode.Text = Distrib.Mode
                txtStdDev.Text = Distrib.StdDev
                txtVariance.Text = Distrib.Variance
                txtSkewness.Text = Distrib.Skewness

            Case "Erlang"
                Dim Distrib As New MathNet.Numerics.Distributions.Erlang(ParamA, ParamB)
                txtXMinStat.Text = Distrib.Minimum
                txtXMaxStat.Text = Distrib.Maximum
                txtEntropy.Text = Distrib.Entropy
                txtMean.Text = Distrib.Mean
                'txtMedian.Text = Distrib.Median
                txtMedian.Text = ""
                txtMode.Text = Distrib.Mode
                txtStdDev.Text = Distrib.StdDev
                txtVariance.Text = Distrib.Variance
                txtSkewness.Text = Distrib.Skewness

            Case "Exponential"
                Dim Distrib As New MathNet.Numerics.Distributions.Exponential(ParamA)
                txtXMinStat.Text = Distrib.Minimum
                txtXMaxStat.Text = Distrib.Maximum
                txtEntropy.Text = Distrib.Entropy
                txtMean.Text = Distrib.Mean
                txtMedian.Text = Distrib.Median
                txtMode.Text = Distrib.Mode
                txtStdDev.Text = Distrib.StdDev
                txtVariance.Text = Distrib.Variance
                txtSkewness.Text = Distrib.Skewness

            Case "Fisher-Snedecor"
                Dim Distrib As New MathNet.Numerics.Distributions.FisherSnedecor(ParamA, ParamB)
                txtXMinStat.Text = Distrib.Minimum
                txtXMaxStat.Text = Distrib.Maximum
                'txtEntropy.Text = Distrib.Entropy
                txtEntropy.Text = ""
                'txtMean.Text = Distrib.Mean
                txtMean.Text = ""
                'txtMedian.Text = Distrib.Median
                txtMedian.Text = ""
                txtMode.Text = Distrib.Mode
                'txtStdDev.Text = Distrib.StdDev
                txtStdDev.Text = ""
                'txtVariance.Text = Distrib.Variance
                txtVariance.Text = ""
                'txtSkewness.Text = Distrib.Skewness
                txtSkewness.Text = ""

            Case "Gamma"
                Dim Distrib As New MathNet.Numerics.Distributions.Gamma(ParamA, ParamB)
                txtXMinStat.Text = Distrib.Minimum
                txtXMaxStat.Text = Distrib.Maximum
                txtEntropy.Text = Distrib.Entropy
                txtMean.Text = Distrib.Mean
                'txtMedian.Text = Distrib.Median
                txtMedian.Text = ""
                txtMode.Text = Distrib.Mode
                txtStdDev.Text = Distrib.StdDev
                txtVariance.Text = Distrib.Variance
                txtSkewness.Text = Distrib.Skewness

            Case "Geometric"
                'This is a discrete distribution.
                Dim Distrib As New MathNet.Numerics.Distributions.Geometric(ParamA)
                txtXMinStat.Text = Distrib.Minimum
                txtXMaxStat.Text = Distrib.Maximum
                txtEntropy.Text = Distrib.Entropy
                txtMean.Text = Distrib.Mean
                txtMedian.Text = Distrib.Median
                txtMode.Text = Distrib.Mode
                txtStdDev.Text = Distrib.StdDev
                txtVariance.Text = Distrib.Variance
                txtSkewness.Text = Distrib.Skewness

            Case "Hypergeometric"
                'This is a discrete distribution.
                Dim Distrib As New MathNet.Numerics.Distributions.Hypergeometric(ParamA, ParamB, ParamC)
                txtXMinStat.Text = Distrib.Minimum
                txtXMaxStat.Text = Distrib.Maximum
                'txtEntropy.Text = Distrib.Entropy
                txtEntropy.Text = ""
                txtMean.Text = Distrib.Mean
                'txtMedian.Text = Distrib.Median
                txtMedian.Text = ""
                txtMode.Text = Distrib.Mode
                txtStdDev.Text = Distrib.StdDev
                txtVariance.Text = Distrib.Variance
                txtSkewness.Text = Distrib.Skewness

            Case "Inverse Gamma"
                Dim Distrib As New MathNet.Numerics.Distributions.InverseGamma(ParamA, ParamB)
                txtXMinStat.Text = Distrib.Minimum
                txtXMaxStat.Text = Distrib.Maximum
                txtEntropy.Text = Distrib.Entropy
                Try
                    txtMean.Text = Distrib.Mean
                Catch ex As Exception
                    Main.Message.AddWarning(ex.Message & vbCrLf)
                    txtMean.Text = ""
                End Try

                'txtMedian.Text = Distrib.Median
                txtMedian.Text = ""
                txtMode.Text = Distrib.Mode
                txtStdDev.Text = Distrib.StdDev
                'txtVariance.Text = Distrib.Variance
                txtVariance.Text = ""
                txtSkewness.Text = ""

            Case "Inverse Gaussian"
                Dim Distrib As New MathNet.Numerics.Distributions.InverseGaussian(ParamA, ParamB)
                txtXMinStat.Text = Distrib.Minimum
                txtXMaxStat.Text = Distrib.Maximum
                'txtEntropy.Text = Distrib.Entropy
                txtEntropy.Text = ""
                txtMean.Text = Distrib.Mean
                txtMedian.Text = Distrib.Median
                txtMode.Text = Distrib.Mode
                txtStdDev.Text = Distrib.StdDev
                txtVariance.Text = Distrib.Variance
                txtSkewness.Text = Distrib.Skewness

            Case "Laplace"
                Dim Distrib As New MathNet.Numerics.Distributions.Laplace(ParamA, ParamB)
                txtXMinStat.Text = Distrib.Minimum
                txtXMaxStat.Text = Distrib.Maximum
                txtEntropy.Text = Distrib.Entropy
                txtMean.Text = Distrib.Mean
                txtMedian.Text = Distrib.Median
                txtMode.Text = Distrib.Mode
                txtStdDev.Text = Distrib.StdDev
                txtVariance.Text = Distrib.Variance
                txtSkewness.Text = Distrib.Skewness

            Case "Log Normal"
                Dim Distrib As New MathNet.Numerics.Distributions.LogNormal(ParamA, ParamB)
                txtXMinStat.Text = Distrib.Minimum
                txtXMaxStat.Text = Distrib.Maximum
                txtEntropy.Text = Distrib.Entropy
                txtMean.Text = Distrib.Mean
                txtMedian.Text = Distrib.Median
                txtMode.Text = Distrib.Mode
                txtStdDev.Text = Distrib.StdDev
                txtVariance.Text = Distrib.Variance
                txtSkewness.Text = Distrib.Skewness

            Case "Negative Binomial"
                'This is a discrete distribution.
                Dim Distrib As New MathNet.Numerics.Distributions.NegativeBinomial(ParamA, ParamB)
                txtXMinStat.Text = Distrib.Minimum
                txtXMaxStat.Text = Distrib.Maximum
                'txtEntropy.Text = Distrib.Entropy
                txtEntropy.Text = ""
                txtMean.Text = Distrib.Mean
                'txtMedian.Text = Distrib.Median
                txtMedian.Text = ""
                txtMode.Text = Distrib.Mode
                txtStdDev.Text = Distrib.StdDev
                txtVariance.Text = Distrib.Variance
                txtSkewness.Text = Distrib.Skewness

            Case "Normal"
                Dim Distrib As New MathNet.Numerics.Distributions.Normal(ParamA, ParamB)
                txtXMinStat.Text = Distrib.Minimum
                txtXMaxStat.Text = Distrib.Maximum
                txtEntropy.Text = Distrib.Entropy
                txtMean.Text = Distrib.Mean
                txtMedian.Text = Distrib.Median
                txtMode.Text = Distrib.Mode
                txtStdDev.Text = Distrib.StdDev
                txtVariance.Text = Distrib.Variance
                txtSkewness.Text = Distrib.Skewness

            Case "Pareto"
                Dim Distrib As New MathNet.Numerics.Distributions.Pareto(ParamA, ParamB)
                txtXMinStat.Text = Distrib.Minimum
                txtXMaxStat.Text = Distrib.Maximum
                txtEntropy.Text = Distrib.Entropy
                'txtMean.Text = Distrib.Mean
                txtMean.Text = ""
                txtMedian.Text = Distrib.Median
                txtMode.Text = Distrib.Mode
                txtStdDev.Text = Distrib.StdDev
                txtVariance.Text = Distrib.Variance
                txtSkewness.Text = Distrib.Skewness

            Case "Poisson"
                'This is a discrete distribution.
                Dim Distrib As New MathNet.Numerics.Distributions.Poisson(ParamA)
                txtXMinStat.Text = Distrib.Minimum
                txtXMaxStat.Text = Distrib.Maximum
                txtEntropy.Text = Distrib.Entropy
                txtMean.Text = Distrib.Mean
                txtMedian.Text = Distrib.Median
                txtMode.Text = Distrib.Mode
                txtStdDev.Text = Distrib.StdDev
                txtVariance.Text = Distrib.Variance
                txtSkewness.Text = Distrib.Skewness

            Case "Rayleigh"
                Dim Distrib As New MathNet.Numerics.Distributions.Rayleigh(ParamA)
                txtXMinStat.Text = Distrib.Minimum
                txtXMaxStat.Text = Distrib.Maximum
                txtEntropy.Text = Distrib.Entropy
                txtMean.Text = Distrib.Mean
                txtMedian.Text = Distrib.Median
                txtMode.Text = Distrib.Mode
                txtStdDev.Text = Distrib.StdDev
                txtVariance.Text = Distrib.Variance
                txtSkewness.Text = Distrib.Skewness

            Case "Skewed Generalized Error"
                Dim Distrib As New MathNet.Numerics.Distributions.SkewedGeneralizedError(ParamA, ParamB, ParamC, ParamD)
                txtXMinStat.Text = Distrib.Minimum
                txtXMaxStat.Text = Distrib.Maximum
                'txtEntropy.Text = Distrib.Entropy
                txtEntropy.Text = ""
                txtMean.Text = Distrib.Mean
                txtMedian.Text = Distrib.Median
                txtMode.Text = Distrib.Mode
                txtStdDev.Text = Distrib.StdDev
                txtVariance.Text = Distrib.Variance
                txtSkewness.Text = Distrib.Skewness

            Case "Skewed Generalized T"
                Dim Distrib As Object
                Try
                    'Dim Distrib As New MathNet.Numerics.Distributions.SkewedGeneralizedT(ParamA, ParamB, ParamC, ParamD, ParamE)
                    Distrib = New MathNet.Numerics.Distributions.SkewedGeneralizedT(ParamA, ParamB, ParamC, ParamD, ParamE)
                    'txtXMinStat.Text = Distrib.Minimum
                    'txtXMaxStat.Text = Distrib.Maximum
                    'txtEntropy.Text = Distrib.Entropy
                    'txtMean.Text = Distrib.Mean
                    'txtMedian.Text = Distrib.Median
                    'txtMode.Text = Distrib.Mode
                    'txtStdDev.Text = Distrib.StdDev
                    'txtVariance.Text = Distrib.Variance
                    'txtSkewness.Text = Distrib.Skewness
                Catch ex As Exception
                    Main.Message.AddWarning(ex.Message & vbCrLf)
                    cmbAllDistributions.Focus() 'In case this error was raised after editing a parameter value, move the focus from the text box!
                    Distrib = Nothing
                    'txtXMinStat.Text = ""
                    'txtXMaxStat.Text = ""
                    'txtEntropy.Text = ""
                    'txtMean.Text = ""
                    'txtMedian.Text = ""
                    'txtMode.Text = ""
                    'txtStdDev.Text = ""
                    'txtVariance.Text = ""
                    'txtSkewness.Text = ""
                End Try

                If IsNothing(Distrib) Then
                    txtXMinStat.Text = ""
                    txtXMaxStat.Text = ""
                    txtEntropy.Text = ""
                    txtMean.Text = ""
                    txtMedian.Text = ""
                    txtMode.Text = ""
                    txtStdDev.Text = ""
                    txtVariance.Text = ""
                    txtSkewness.Text = ""
                Else
                    txtXMinStat.Text = Distrib.Minimum
                    txtXMaxStat.Text = Distrib.Maximum
                    'txtEntropy.Text = Distrib.Entropy
                    txtEntropy.Text = ""
                    txtMean.Text = Distrib.Mean
                    txtMedian.Text = Distrib.Median
                    txtMode.Text = Distrib.Mode
                    txtStdDev.Text = Distrib.StdDev
                    txtVariance.Text = Distrib.Variance
                    txtSkewness.Text = Distrib.Skewness
                End If

            Case "Stable"
                Dim Distrib As New MathNet.Numerics.Distributions.Stable(ParamA, ParamB, ParamC, ParamD)
                txtXMinStat.Text = Distrib.Minimum
                txtXMaxStat.Text = Distrib.Maximum
                'txtEntropy.Text = Distrib.Entropy
                txtEntropy.Text = ""
                'txtMean.Text = Distrib.Mean
                txtMean.Text = ""
                txtMedian.Text = Distrib.Median
                txtMode.Text = Distrib.Mode
                txtStdDev.Text = Distrib.StdDev
                txtVariance.Text = Distrib.Variance
                'txtSkewness.Text = Distrib.Skewness
                txtSkewness.Text = ""

            Case "Student's T"
                Dim Distrib As New MathNet.Numerics.Distributions.StudentT(ParamA, ParamB, ParamC)
                txtXMinStat.Text = Distrib.Minimum
                txtXMaxStat.Text = Distrib.Maximum
                txtEntropy.Text = Distrib.Entropy
                txtMean.Text = Distrib.Mean
                txtMedian.Text = Distrib.Median
                txtMode.Text = Distrib.Mode
                txtStdDev.Text = Distrib.StdDev
                txtVariance.Text = Distrib.Variance
                txtSkewness.Text = Distrib.Skewness

            Case "Triangular"
                Dim Distrib As New MathNet.Numerics.Distributions.Triangular(ParamA, ParamB, ParamC)
                txtXMinStat.Text = Distrib.Minimum
                txtXMaxStat.Text = Distrib.Maximum
                txtEntropy.Text = Distrib.Entropy
                txtMean.Text = Distrib.Mean
                txtMedian.Text = Distrib.Median
                txtMode.Text = Distrib.Mode
                txtStdDev.Text = Distrib.StdDev
                txtVariance.Text = Distrib.Variance
                txtSkewness.Text = Distrib.Skewness

            Case "Truncated Pareto"
                Dim Distrib As New MathNet.Numerics.Distributions.TruncatedPareto(ParamA, ParamB, ParamC)
                txtXMinStat.Text = Distrib.Minimum
                txtXMaxStat.Text = Distrib.Maximum
                'txtEntropy.Text = Distrib.Entropy
                txtEntropy.Text = ""
                txtMean.Text = Distrib.Mean
                txtMedian.Text = Distrib.Median
                'txtMode.Text = Distrib.Mode
                txtMode.Text = ""
                txtStdDev.Text = Distrib.StdDev
                txtVariance.Text = Distrib.Variance
                txtSkewness.Text = Distrib.Skewness

            Case "Weibull"
                Dim Distrib As New MathNet.Numerics.Distributions.Weibull(ParamA, ParamB)
                txtXMinStat.Text = Distrib.Minimum
                txtXMaxStat.Text = Distrib.Maximum
                txtEntropy.Text = Distrib.Entropy
                txtMean.Text = Distrib.Mean
                txtMedian.Text = Distrib.Median
                txtMode.Text = Distrib.Mode
                txtStdDev.Text = Distrib.StdDev
                txtVariance.Text = Distrib.Variance
                txtSkewness.Text = Distrib.Skewness

            Case "Zipf"
                'This is a discrete distribution.
                Dim Distrib As New MathNet.Numerics.Distributions.Zipf(ParamA, ParamB)
                txtXMinStat.Text = Distrib.Minimum
                txtXMaxStat.Text = Distrib.Maximum
                txtEntropy.Text = Distrib.Entropy
                txtMean.Text = Distrib.Mean
                'txtMedian.Text = Distrib.Median
                txtMedian.Text = ""
                txtMode.Text = Distrib.Mode
                'txtStdDev.Text = Distrib.StdDev
                txtStdDev.Text = ""
                'txtVariance.Text = Distrib.Variance
                txtVariance.Text = ""
                'txtSkewness.Text = Distrib.Skewness
                txtSkewness.Text = ""

            Case Else
                RaiseEvent ErrorMessage("Unknown distribution: " & DistributionName & vbCrLf)
                'Distrib = Nothing
                txtXMinStat.Text = ""
                txtXMaxStat.Text = ""
                txtEntropy.Text = ""
                txtMean.Text = ""
                txtMedian.Text = ""
                txtMode.Text = ""
                txtStdDev.Text = ""
                txtSkewness.Text = ""
        End Select

    End Sub

    Private Sub txtAValue_TextChanged(sender As Object, e As EventArgs) Handles txtAValue.TextChanged

    End Sub

    Private Sub txtAValue_LostFocus(sender As Object, e As EventArgs) Handles txtAValue.LostFocus
        ParamA = Val(txtAValue.Text.Trim)
        UpdateStatistics()
    End Sub

    Private Sub txtBValue_TextChanged(sender As Object, e As EventArgs) Handles txtBValue.TextChanged

    End Sub

    Private Sub txtBValue_LostFocus(sender As Object, e As EventArgs) Handles txtBValue.LostFocus
        ParamB = Val(txtBValue.Text.Trim)
        UpdateStatistics()
    End Sub

    Private Sub txtCValue_TextChanged(sender As Object, e As EventArgs) Handles txtCValue.TextChanged

    End Sub

    Private Sub txtCValue_LostFocus(sender As Object, e As EventArgs) Handles txtCValue.LostFocus
        ParamC = Val(txtCValue.Text.Trim)
        UpdateStatistics()
    End Sub

    Private Sub txtDValue_TextChanged(sender As Object, e As EventArgs) Handles txtDValue.TextChanged

    End Sub

    Private Sub txtDValue_LostFocus(sender As Object, e As EventArgs) Handles txtDValue.LostFocus
        ParamD = Val(txtDValue.Text.Trim)
        UpdateStatistics()
    End Sub

    Private Sub txtEValue_TextChanged(sender As Object, e As EventArgs) Handles txtEValue.TextChanged

    End Sub

    Private Sub txtEValue_LostFocus(sender As Object, e As EventArgs) Handles txtEValue.LostFocus
        ParamE = Val(txtEValue.Text.Trim)
        UpdateStatistics()
    End Sub

    Private Sub cmbFunctionType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFunctionType.SelectedIndexChanged
        If cmbFunctionType.SelectedItem.ToString = "InvCDF" Then
            lblXDescr.Text = "Probability value:"
            lblXLabel.Text = "P ="
        Else
            lblXDescr.Text = "Random variable value:"
            lblXLabel.Text = "X ="
        End If
    End Sub

#End Region 'Form Methods ---------------------------------------------------------------------------------------------------------------------------------------------------------------------

#Region " Form Events - Events that can be triggered by this form." '==========================================================================================================================

    Event ErrorMessage(ByVal Msg As String) 'Send an error message.
    Event Message(ByVal Msg As String) 'Send a normal message.
    Event DistributionInfo(ByVal Info As Main.clsDistribInfo) 'Send information about the selected distribution.

    Private Sub txtDistVal_TextChanged(sender As Object, e As EventArgs) Handles txtDistVal.TextChanged

    End Sub












#End Region 'Form Events ----------------------------------------------------------------------------------------------------------------------------------------------------------------------








End Class