Public Class frmAdjust
    'Adjust the Distribution parameters.

#Region " Variable Declarations - All the variables used in this form and this application." '=================================================================================================

    'Public ParamA As Double
    'Public ParamB As Double
    'Public ParamC As Double
    'Public ParamD As Double
    'Public ParamE As Double

    'Public ParamAIncr As Double
    'Public ParamBIncr As Double
    'Public ParamCIncr As Double
    'Public ParamDIncr As Double
    'Public ParamEIncr As Double

    'Private CharCode As Integer

    'Public Distribution As DistributionModel 'This variable will point to the Distribution of the parent form.

    Dim SelectedDistrib As DistributionInfo 'This variable points to the selected distribution - either Main.Distribution.Distrib or Main.Distribution.MultiDistrib(DistribNo - 1)

    Dim MouseABDown As Boolean 'True if the mouse is down over pbAB
    Dim MouseABPosNow As New Point 'Stores the current mouse position in pbAB

    Dim MouseCDDown As Boolean 'True if the mouse is down over pbCD
    Dim MouseCDPosNow As New Point 'Stores the current mouse position in pbCD

    'Dim MouseADown As Boolean 'True if the mouse is down over pbA
    'Dim MouseAPosNow As New Point 'Stores the current mouse position in pbA
    'Dim MouseBDown As Boolean 'True if the mouse is down over pbB
    'Dim MouseBPosNow As New Point 'Stores the current mouse position in pbB
    'Dim MouseCDown As Boolean 'True if the mouse is down over pbC
    'Dim MouseCPosNow As New Point 'Stores the current mouse position in pbC
    'Dim MouseDDown As Boolean 'True if the mouse is down over pbD
    'Dim MouseDPosNow As New Point 'Stores the current mouse position in pbD
    'Dim MouseEDown As Boolean 'True if the mouse is down over pbE
    'Dim MouseEPosNow As New Point 'Stores the current mouse position in pbE

    'Objects used to convert between 1D and 2D pixel positions and parameter values:
    Dim ABInfo As New Graph2DInfo
    Dim CDInfo As New Graph2DInfo
    'Dim AInfo As New Graph1DInfo
    'Dim BInfo As New Graph1DInfo
    'Dim CInfo As New Graph1DInfo
    'Dim DInfo As New Graph1DInfo
    'Dim EInfo As New Graph1DInfo


    ''TEST CODE:
    ''https://www.dreamincode.net/forums/topic/59049-simple-drawing-selection-shape-or-rubberband-shape/
    'Private _bRubberBandingOn As Boolean = False '-- State to control if we are drawing the rubber banding object
    'Private _pClickStart As New Point '-- The place where the mouse button went 'down'.
    'Private _pClickStop As New Point '-- The place where the mouse button went 'up'.
    'Private _pNow As New Point '-- Holds the current mouse location to make the shape appear to follow the mouse cursor.

    Private StartTab As Integer 'Save the start tab value. This is read from the Form Settings. Only select this tab after the form has loaded. 
    '(There are issues with applying this tab setting early - Selecting the Calculated Values tab makes the form slow to load. - The Graphical tab should be shown initially to avoid display errors.)

    Public WithEvents SeriesAnalysis As frmSeriesAnalysis

#End Region 'Variable Declarations ------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Properties - All the properties used in this form and this application" '============================================================================================================

    Private _selDistrib As Integer = 0 'The selected Distribution index number in the collection of secondary distributions. (0 if none selected.)
    Property SelDistrib As Integer
        Get
            Return _selDistrib
        End Get
        Set(value As Integer)
            _selDistrib = value
            txtSelDistrib.Text = _selDistrib
            'If SelParamSet = 0 Then
            '    ClearForm()
            'Else
            '    UpdateForm()
            'End If
        End Set
    End Property

    Private _nDistribs As Integer = 0 'The number of Distributions in the collection of secondary distributions.
    Property NDistribs As Integer
        Get
            Return _nDistribs
        End Get
        Set(value As Integer)
            _nDistribs = value
            txtNDistribs.Text = _nDistribs
        End Set
    End Property

    Private _nParams As Integer = 2 'The number of parameters used to define the distribution.
    Property NParams As Integer
        Get
            Return _nParams
        End Get
        Set(value As Integer)
            _nParams = value
            ApplyNParams()
        End Set
    End Property

    Private _paramAName As String = "" 'Parameter A name.
    Property ParamAName As String
        Get
            Return _paramAName
        End Get
        Set(value As String)
            _paramAName = value
            txtParamAName.Text = _paramAName
        End Set
    End Property

    Private _paramBName As String = "" 'Parameter B name.
    Property ParamBName As String
        Get
            Return _paramBName
        End Get
        Set(value As String)
            _paramBName = value
            txtParamBName.Text = _paramBName
        End Set
    End Property

    Private _paramCName As String = "" 'Parameter C name.
    Property ParamCName As String
        Get
            Return _paramCName
        End Get
        Set(value As String)
            _paramCName = value
            txtParamCName.Text = _paramCName
        End Set
    End Property

    Private _paramDName As String = "" 'Parameter D name.
    Property ParamDName As String
        Get
            Return _paramDName
        End Get
        Set(value As String)
            _paramDName = value
            txtParamDName.Text = _paramDName
        End Set
    End Property

    Private _paramEName As String = "" 'Parameter E name.
    Property ParamEName As String
        Get
            Return _paramEName
        End Get
        Set(value As String)
            _paramEName = value
            txtParamEName.Text = _paramEName
        End Set
    End Property

    Private _paramAOrigVal As Double  'Parameter A original value.
    Property ParamAOrigVal As Double
        Get
            Return _paramAOrigVal
        End Get
        Set(value As Double)
            _paramAOrigVal = value
            txtParamAOrig.Text = _paramAOrigVal
        End Set
    End Property

    Private _paramBOrigVal As Double  'Parameter B original value.
    Property ParamBOrigVal As Double
        Get
            Return _paramBOrigVal
        End Get
        Set(value As Double)
            _paramBOrigVal = value
            txtParamBOrig.Text = _paramBOrigVal
        End Set
    End Property

    Private _paramCOrigVal As Double  'Parameter C original value.
    Property ParamCOrigVal As Double
        Get
            Return _paramCOrigVal
        End Get
        Set(value As Double)
            _paramCOrigVal = value
            txtParamCOrig.Text = _paramCOrigVal
        End Set
    End Property

    Private _paramDOrigVal As Double  'Parameter D original value.
    Property ParamDOrigVal As Double
        Get
            Return _paramDOrigVal
        End Get
        Set(value As Double)
            _paramDOrigVal = value
            txtParamDOrig.Text = _paramDOrigVal
        End Set
    End Property

    Private _paramEOrigVal As Double  'Parameter E original value.
    Property ParamEOrigVal As Double
        Get
            Return _paramEOrigVal
        End Get
        Set(value As Double)
            _paramEOrigVal = value
            txtParamEOrig.Text = _paramEOrigVal
        End Set
    End Property

    'Private _updateParamAVal As Double 'Parameter A Value - Used by the graphical controls to update all variables with ParamAVal.
    'Property UpdateParamAVal As Double
    '    Get
    '        Return _updateParamAVal
    '    End Get
    '    Set(value As Double)
    '        _updateParamAVal = value
    '        ParamAVal = value
    '        Main.Distribution.Info(SelDistrib - 1).ParamA.Value = value
    '    End Set
    'End Property

    Private _paramAVal As Double  'Parameter A value.
    Property ParamAVal As Double
        Get
            Return _paramAVal
        End Get
        Set(value As Double)
            _paramAVal = value
            If Round Then _paramAVal = DecRound(_paramAVal)
            '_paramAVal = Main.Distribution.Info(SelDistrib - 1).ValidRangeAdjust("ParamA", _paramAVal, 0.001)
            _paramAVal = SelectedDistrib.ValidRangeAdjust("ParamA", _paramAVal, 0.001)
            txtParamAValue.Text = _paramAVal
        End Set
    End Property

    'Private _updateParamBVal As Double 'Parameter B Value - Used by the graphical controls to update all variables with ParamBVal.
    'Property UpdateParamBVal As Double
    '    Get
    '        Return _updateParamBVal
    '    End Get
    '    Set(value As Double)
    '        _updateParamBVal = value
    '        ParamBVal = value
    '        Main.Distribution.Info(SelDistrib - 1).ParamB.Value = value
    '    End Set
    'End Property

    Private _paramBVal As Double  'Parameter B value.
    Property ParamBVal As Double
        Get
            Return _paramBVal
        End Get
        Set(value As Double)
            _paramBVal = value
            If Round Then _paramBVal = DecRound(_paramBVal)
            _paramBVal = SelectedDistrib.ValidRangeAdjust("ParamB", _paramBVal, 0.001)
            txtParamBValue.Text = _paramBVal
        End Set
    End Property

    'Private _updateParamCVal As Double 'Parameter C Value - Used by the graphical controls to update all variables with ParamCVal.
    'Property UpdateParamCVal As Double
    '    Get
    '        Return _updateParamCVal
    '    End Get
    '    Set(value As Double)
    '        _updateParamCVal = value
    '        ParamCVal = value
    '        Main.Distribution.Info(SelDistrib - 1).ParamC.Value = value
    '    End Set
    'End Property

    Private _paramCVal As Double  'Parameter C value.
    Property ParamCVal As Double
        Get
            Return _paramCVal
        End Get
        Set(value As Double)
            _paramCVal = value
            If Round Then _paramCVal = DecRound(_paramCVal)
            _paramCVal = SelectedDistrib.ValidRangeAdjust("ParamC", _paramCVal, 0.001)
            txtParamCValue.Text = _paramCVal
        End Set
    End Property

    'Private _updateParamDVal As Double 'Parameter D Value - Used by the graphical controls to update all variables with ParamDVal.
    'Property UpdateParamDVal As Double
    '    Get
    '        Return _updateParamDVal
    '    End Get
    '    Set(value As Double)
    '        _updateParamDVal = value
    '        ParamDVal = value
    '        Main.Distribution.Info(SelDistrib - 1).ParamD.Value = value
    '    End Set
    'End Property

    Private _paramDVal As Double  'Parameter D value.
    Property ParamDVal As Double
        Get
            Return _paramDVal
        End Get
        Set(value As Double)
            _paramDVal = value
            If Round Then _paramDVal = DecRound(_paramDVal)
            _paramDVal = SelectedDistrib.ValidRangeAdjust("ParamD", _paramDVal, 0.001)
            txtParamDValue.Text = _paramDVal
        End Set
    End Property

    'Private _updateParamEVal As Double 'Parameter E Value - Used by the graphical controls to update all variables with ParamEVal.
    'Property UpdateParamEVal As Double
    '    Get
    '        Return _updateParamEVal
    '    End Get
    '    Set(value As Double)
    '        _updateParamEVal = value
    '        ParamEVal = value
    '        Main.Distribution.Info(SelDistrib - 1).ParamE.Value = value
    '    End Set
    'End Property

    Private _paramEVal As Double  'Parameter E value.
    Property ParamEVal As Double
        Get
            Return _paramEVal
        End Get
        Set(value As Double)
            _paramEVal = value
            If Round Then _paramEVal = DecRound(_paramEVal)
            _paramEVal = SelectedDistrib.ValidRangeAdjust("ParamE", _paramEVal, 0.001)
            txtParamEValue.Text = _paramEVal
        End Set
    End Property

    Private _paramAIncr As Double  'Parameter A value increment.
    'Private _paramAIncr As Single  'Parameter A value increment.
    Property ParamAIncr As Double
        Get
            Return _paramAIncr
        End Get
        Set(value As Double)
            _paramAIncr = value
            txtParamAIncr.Text = _paramAIncr
        End Set
    End Property

    Private _paramBIncr As Double  'Parameter B value increment.
    Property ParamBIncr As Double
        Get
            Return _paramBIncr
        End Get
        Set(value As Double)
            _paramBIncr = value
            txtParamBIncr.Text = _paramBIncr
        End Set
    End Property

    Private _paramCIncr As Double  'Parameter C value increment.
    Property ParamCIncr As Double
        Get
            Return _paramCIncr
        End Get
        Set(value As Double)
            _paramCIncr = value
            txtParamCIncr.Text = _paramCIncr
        End Set
    End Property

    Private _paramDIncr As Double  'Parameter D value increment.
    Property ParamDIncr As Double
        Get
            Return _paramDIncr
        End Get
        Set(value As Double)
            _paramDIncr = value
            txtParamDIncr.Text = _paramDIncr
        End Set
    End Property

    Private _paramEIncr As Double  'Parameter E value increment.
    Property ParamEIncr As Double
        Get
            Return _paramEIncr
        End Get
        Set(value As Double)
            _paramEIncr = value
            txtParamEIncr.Text = _paramEIncr
        End Set
    End Property



    Private _paramAAdjustMin As Double  'Parameter A adjust minimum.
    Property ParamAAdjustMin As Double
        Get
            Return _paramAAdjustMin
        End Get
        Set(value As Double)
            _paramAAdjustMin = value
        End Set
    End Property

    Private _paramBAdjustMin As Double  'Parameter B adjust minimum.
    Property ParamBAdjustMin As Double
        Get
            Return _paramBAdjustMin
        End Get
        Set(value As Double)
            _paramBAdjustMin = value
        End Set
    End Property

    Private _paramCAdjustMin As Double  'Parameter C adjust minimum.
    Property ParamCAdjustMin As Double
        Get
            Return _paramCAdjustMin
        End Get
        Set(value As Double)
            _paramCAdjustMin = value
        End Set
    End Property

    Private _paramDAdjustMin As Double  'Parameter D adjust minimum.
    Property ParamDAdjustMin As Double
        Get
            Return _paramDAdjustMin
        End Get
        Set(value As Double)
            _paramDAdjustMin = value
        End Set
    End Property

    Private _paramEAdjustMin As Double  'Parameter E adjust minimum.
    Property ParamEAdjustMin As Double
        Get
            Return _paramEAdjustMin
        End Get
        Set(value As Double)
            _paramEAdjustMin = value
        End Set
    End Property



    Private _paramAAdjustMax As Double  'Parameter A adjust maximum.
    Property ParamAAdjustMax As Double
        Get
            Return _paramAAdjustMax
        End Get
        Set(value As Double)
            _paramAAdjustMax = value
        End Set
    End Property

    Private _paramBAdjustMax As Double  'Parameter B adjust maximum.
    Property ParamBAdjustMax As Double
        Get
            Return _paramBAdjustMax
        End Get
        Set(value As Double)
            _paramBAdjustMax = value
        End Set
    End Property

    Private _paramCAdjustMax As Double  'Parameter C adjust maximum.
    Property ParamCAdjustMax As Double
        Get
            Return _paramCAdjustMax
        End Get
        Set(value As Double)
            _paramCAdjustMax = value
        End Set
    End Property

    Private _paramDAdjustMax As Double  'Parameter D adjust maximum.
    Property ParamDAdjustMax As Double
        Get
            Return _paramDAdjustMax
        End Get
        Set(value As Double)
            _paramDAdjustMax = value
        End Set
    End Property

    Private _paramEAdjustMax As Double  'Parameter E adjust maximum.
    Property ParamEAdjustMax As Double
        Get
            Return _paramEAdjustMax
        End Get
        Set(value As Double)
            _paramEAdjustMax = value
        End Set
    End Property

    Private _decRounding As Integer = 3 'The number of decimal places used for rounding the parameter values.
    Property DecRounding As Integer
        Get
            Return _decRounding
        End Get
        Set(value As Integer)
            _decRounding = value
        End Set
    End Property

    Private _round As Boolean = True 'If True, parameter values will be rounded to the specifield number of decomal places.
    Property Round As Boolean
        Get
            Return _round
        End Get
        Set(value As Boolean)
            _round = value
        End Set
    End Property

    Private _logLikelihood As Double = 0 'The natural log of the likelihood that a set of sample values was generated by the specified distribution.
    Property LogLikelihood As Double
        Get
            Return _logLikelihood
        End Get
        Set(value As Double)
            _logLikelihood = value
            txtLogLikelihood.Text = _logLikelihood
        End Set
    End Property

    Private _inUse As Boolean = False 'If InUse = True then LoadParamInfo will no be run.
    Property InUse As Boolean
        Get
            Return _inUse
        End Get
        Set(value As Boolean)
            _inUse = value
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
                               <SelectedTabIndex><%= TabControl1.SelectedIndex %></SelectedTabIndex>
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
            If Settings.<FormSettings>.<SelectedTabIndex>.Value <> Nothing Then
                TabControl1.SelectedIndex = 1 'The graphical display settings are incorrect unless this tab is displayed first! (Not sure why - to be checked.)
                'TabControl1.SelectedIndex = Settings.<FormSettings>.<SelectedTabIndex>.Value
                StartTab = Settings.<FormSettings>.<SelectedTabIndex>.Value 'Save this setting to apply after the form has loaded.
            Else
                TabControl1.SelectedIndex = 1 'The graphical display settings are incorrect unless this tab is displayed first! (Not sure why - to be checked.)
            End If

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

        NDistribs = Main.Distribution.Info.Count
        txtNDistribs.Text = NDistribs
        If NDistribs = 0 Then
            _selDistrib = 0
            txtSelDistrib.Text = "0"
        Else
            _selDistrib = 1
            txtSelDistrib.Text = "1"
            SelectedDistrib = Main.Distribution.Info(SelDistrib - 1)
            LoadParamInfo()
        End If

        chkUpdateLabel.Checked = True

        DecRounding = 3
        txtDecRound.Text = "3"
        chkRound.Checked = True

        cmbTableName.Items.Add("Samples")
        cmbTableName.Items.Add("Model_Fitting")

        dgvDataValues.AutoGenerateColumns = True
        dgvDataValues.DataSource = SelectedDistrib.ParamEst.Data.Tables("Samples")
        dgvDataValues.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        dgvDataValues.AutoResizeColumns()

        dgvCalculations.AutoGenerateColumns = True
        dgvCalculations.DataSource = SelectedDistrib.ParamEst.Data.Tables("Model_Fitting")
        cmbTableName.SelectedIndex = cmbTableName.FindStringExact("Model_Fitting")
        dgvCalculations.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        dgvCalculations.AutoResizeColumns()

        SelectedDistrib.ParamEst.CopySamplesToModelFitting()
        UpdateModelData()

        dgvCalculations.Update()

        TabControl1.SelectedIndex = StartTab

        'Timer1.Interval = 200 'Timer interval of 200ms - Moving the scrollbars starts the timer - when 200ms have elapsed, the distribution parameters will be updated.
        'Timer1.Interval = 500 'Timer interval of 500ms - Moving the scrollbars starts the timer - when 500ms have elapsed, the distribution parameters will be updated.
        Timer1.Interval = 800 'Timer interval of 800ms - Moving the scrollbars starts the timer - when 800ms have elapsed, the distribution parameters will be updated.

    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        'Exit the Form

        If IsNothing(SeriesAnalysis) Then

        Else
            SeriesAnalysis.Close()
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

#End Region 'Form Display Methods -------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Open and Close Forms - Code used to open and close other forms." '===================================================================================================================

#End Region 'Open and Close Forms -------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Form Methods - The main actions performed by this form." '===========================================================================================================================

    Private Sub ApplyNParams()

        txtParamAName.Enabled = True
        txtParamASymbol.Enabled = True
        txtParamAOrig.Enabled = True
        txtParamAValue.Enabled = True
        txtParamAIncr.Enabled = True
        btnParamADecr.Enabled = True
        btnParamAIncr.Enabled = True

        If NParams > 1 Then
            txtParamBName.Enabled = True
            txtParamBSymbol.Enabled = True
            txtParamBOrig.Enabled = True
            txtParamBValue.Enabled = True
            txtParamBIncr.Enabled = True
            btnParamBDecr.Enabled = True
            btnParamBIncr.Enabled = True
            If NParams > 2 Then
                txtParamCName.Enabled = True
                txtParamCSymbol.Enabled = True
                txtParamCOrig.Enabled = True
                txtParamCValue.Enabled = True
                txtParamCIncr.Enabled = True
                btnParamCDecr.Enabled = True
                btnParamCIncr.Enabled = True
                If NParams > 3 Then
                    txtParamDName.Enabled = True
                    txtParamDSymbol.Enabled = True
                    txtParamDOrig.Enabled = True
                    txtParamDValue.Enabled = True
                    txtParamDIncr.Enabled = True
                    btnParamDDecr.Enabled = True
                    btnParamDIncr.Enabled = True
                    If NParams > 4 Then
                        txtParamEName.Enabled = True
                        txtParamESymbol.Enabled = True
                        txtParamEOrig.Enabled = True
                        txtParamEValue.Enabled = True
                        txtParamEIncr.Enabled = True
                        btnParamEDecr.Enabled = True
                        btnParamEIncr.Enabled = True
                    End If
                End If
            End If
        End If

        If NParams < 5 Then
            txtParamEName.Enabled = False : txtParamEName.Text = ""
            txtParamESymbol.Enabled = False : txtParamESymbol.Text = ""
            txtParamEOrig.Enabled = False : txtParamEOrig.Text = ""
            txtParamEValue.Enabled = False : txtParamEValue.Text = ""
            txtParamEIncr.Enabled = False : txtParamEIncr.Text = ""
            btnParamEDecr.Enabled = False : btnParamEDecr.Text = ""
            btnParamEIncr.Enabled = False : btnParamEIncr.Text = ""
            If NParams < 4 Then
                txtParamDName.Enabled = False : txtParamDName.Text = ""
                txtParamDSymbol.Enabled = False : txtParamDSymbol.Text = ""
                txtParamDOrig.Enabled = False : txtParamDOrig.Text = ""
                txtParamDValue.Enabled = False : txtParamDValue.Text = ""
                txtParamDIncr.Enabled = False : txtParamDIncr.Text = ""
                btnParamDDecr.Enabled = False : btnParamDDecr.Text = ""
                btnParamDIncr.Enabled = False : btnParamDIncr.Text = ""
                If NParams < 3 Then
                    txtParamCName.Enabled = False : txtParamCName.Text = ""
                    txtParamCSymbol.Enabled = False : txtParamCSymbol.Text = ""
                    txtParamCOrig.Enabled = False : txtParamCOrig.Text = ""
                    txtParamCValue.Enabled = False : txtParamCValue.Text = ""
                    txtParamCIncr.Enabled = False : txtParamCIncr.Text = ""
                    btnParamCDecr.Enabled = False : btnParamCDecr.Text = ""
                    btnParamCIncr.Enabled = False : btnParamCIncr.Text = ""
                    If NParams < 2 Then
                        txtParamBName.Enabled = False : txtParamBName.Text = ""
                        txtParamBSymbol.Enabled = False : txtParamBSymbol.Text = ""
                        txtParamBOrig.Enabled = False : txtParamBOrig.Text = ""
                        txtParamBValue.Enabled = False : txtParamBValue.Text = ""
                        txtParamBIncr.Enabled = False : txtParamBIncr.Text = ""
                        btnParamBDecr.Enabled = False : btnParamBDecr.Text = ""
                        btnParamBIncr.Enabled = False : btnParamBIncr.Text = ""
                    End If
                End If
            End If
        End If

    End Sub

    Private Sub btnParamADecr_Click(sender As Object, e As EventArgs) Handles btnParamADecr.Click
        'Decrement Parameter A
        ParamAVal = ParamAVal - ParamAIncr
        ApplyParamAVal()

    End Sub

    Private Sub btnParamAIncr_Click(sender As Object, e As EventArgs) Handles btnParamAIncr.Click
        'Increment Parameter A
        ParamAVal = ParamAVal + ParamAIncr
        ApplyParamAVal()

    End Sub

    Private Sub ApplyParamAVal()
        'Apply the new value in ParamAVal.

        SelectedDistrib.ParamA.Value = ParamAVal

        If cmbX1.SelectedIndex = 1 Then
            ABInfo.XParamVal = ParamAVal
            txtX1.Text = ParamAVal
            MouseABPosNow.X = ABInfo.XPos
            pbAB.Invalidate() 'Redraw pbAB
        ElseIf cmbY1.SelectedIndex = 1 Then
            ABInfo.YParamVal = ParamAVal
            txtY1.Text = ParamAVal
            MouseABPosNow.Y = ABInfo.YPos
            pbAB.Invalidate() 'Redraw pbAB
        ElseIf cmbX2.SelectedIndex = 1 Then
            CDInfo.XParamVal = ParamAVal
            txtX2.Text = ParamAVal
            MouseCDPosNow.X = ABInfo.XPos
            pbCD.Invalidate() 'Redraw pbCD
        ElseIf cmbY2.SelectedIndex = 1 Then
            CDInfo.YParamVal = ParamAVal
            txtY2.Text = ParamAVal
            MouseCDPosNow.Y = ABInfo.YPos
            pbCD.Invalidate() 'Redraw pbCD
        End If

        txtA.Text = ParamAVal
        hsA.Value = 1000 * (ParamAVal - ParamAAdjustMin) / (ParamAAdjustMax - ParamAAdjustMin)
        'AInfo.XParamVal = ParamAVal
        'MouseAPosNow.X = AInfo.XPos
        'pbA.Invalidate() 'Redraw pbA

        If Main.SelDistrib = SelDistrib And Main.dgvParams.Rows.Count > 0 Then Main.dgvParams.Rows(0).Cells(2).Value = ParamAVal

        'UpdateSuffix()
        If SelectedDistrib.AutoUpdateLabels Then
            SelectedDistrib.UpdateSuffix() 'This will also update the series labels.
            txtSuffix.Text = SelectedDistrib.Suffix
        End If

        UpdateData()
        Main.UpdateAnnotationSettings(SelDistrib)
        Main.ReplotCharts()
    End Sub

    Private Sub btnParamBDecr_Click(sender As Object, e As EventArgs) Handles btnParamBDecr.Click
        'Decrement Parameter B
        ParamBVal = ParamBVal - ParamBIncr
        ApplyParamBVal()

    End Sub

    Private Sub ApplyParamBVal()
        'Apply the new value in ParamBVal.

        SelectedDistrib.ParamB.Value = ParamBVal

        If cmbX1.SelectedIndex = 2 Then
            ABInfo.XParamVal = ParamBVal
            txtX1.Text = ParamBVal
            MouseABPosNow.X = ABInfo.XPos
            pbAB.Invalidate() 'Redraw pbAB
        ElseIf cmbY1.SelectedIndex = 2 Then
            ABInfo.YParamVal = ParamBVal
            txtY1.Text = ParamBVal
            MouseABPosNow.Y = ABInfo.YPos
            pbAB.Invalidate() 'Redraw pbAB
        ElseIf cmbX2.SelectedIndex = 2 Then
            CDInfo.XParamVal = ParamBVal
            txtX2.Text = ParamBVal
            MouseCDPosNow.X = ABInfo.XPos
            pbCD.Invalidate() 'Redraw pbCD
        ElseIf cmbY2.SelectedIndex = 2 Then
            CDInfo.YParamVal = ParamBVal
            txtY2.Text = ParamBVal
            MouseCDPosNow.Y = ABInfo.YPos
            pbCD.Invalidate() 'Redraw pbCD
        End If

        txtB.Text = ParamBVal
        hsB.Value = 1000 * (ParamBVal - ParamBAdjustMin) / (ParamBAdjustMax - ParamBAdjustMin)
        'BInfo.XParamVal = ParamBVal
        'MouseBPosNow.X = BInfo.XPos
        'pbB.Invalidate() 'Redraw pbB

        If Main.SelDistrib = SelDistrib And Main.dgvParams.Rows.Count > 1 Then Main.dgvParams.Rows(1).Cells(2).Value = ParamBVal

        'UpdateSuffix()
        If SelectedDistrib.AutoUpdateLabels Then
            SelectedDistrib.UpdateSuffix() 'This will also update the series labels.
            txtSuffix.Text = SelectedDistrib.Suffix
        End If

        UpdateData()
        Main.UpdateAnnotationSettings(SelDistrib)
        Main.ReplotCharts()
    End Sub

    Private Sub btnParamBIncr_Click(sender As Object, e As EventArgs) Handles btnParamBIncr.Click
        'Increment Parameter B
        ParamBVal = ParamBVal + ParamBIncr
        ApplyParamBVal()

    End Sub

    Private Sub btnParamCDecr_Click(sender As Object, e As EventArgs) Handles btnParamCDecr.Click
        'Decrement Parameter C
        ParamCVal = ParamCVal - ParamCIncr
        ApplyParamCVal()

    End Sub

    Private Sub ApplyParamCVal()
        'Apply the new value in ParamCVal.

        SelectedDistrib.ParamC.Value = ParamCVal

        If cmbX1.SelectedIndex = 2 Then
            ABInfo.XParamVal = ParamCVal
            txtX1.Text = ParamCVal
            MouseABPosNow.X = ABInfo.XPos
            pbAB.Invalidate() 'Redraw pbAB
        ElseIf cmbY1.SelectedIndex = 2 Then
            ABInfo.YParamVal = ParamCVal
            txtY1.Text = ParamCVal
            MouseABPosNow.Y = ABInfo.YPos
            pbAB.Invalidate() 'Redraw pbAB
        ElseIf cmbX2.SelectedIndex = 2 Then
            CDInfo.XParamVal = ParamCVal
            txtX2.Text = ParamCVal
            MouseCDPosNow.X = ABInfo.XPos
            pbCD.Invalidate() 'Redraw pbCD
        ElseIf cmbY2.SelectedIndex = 2 Then
            CDInfo.YParamVal = ParamCVal
            txtY2.Text = ParamCVal
            MouseCDPosNow.Y = ABInfo.YPos
            pbCD.Invalidate() 'Redraw pbCD
        End If

        txtC.Text = ParamCVal
        hsC.Value = 1000 * (ParamCVal - ParamCAdjustMin) / (ParamCAdjustMax - ParamCAdjustMin)
        'CInfo.XParamVal = ParamCVal
        'MouseCPosNow.X = CInfo.XPos
        'pbC.Invalidate() 'Redraw pbC

        If Main.SelDistrib = SelDistrib And Main.dgvParams.Rows.Count > 2 Then Main.dgvParams.Rows(2).Cells(2).Value = ParamCVal

        'UpdateSeriesName()
        'UpdateSuffix()
        If SelectedDistrib.AutoUpdateLabels Then
            SelectedDistrib.UpdateSuffix() 'This will also update the series labels.
            txtSuffix.Text = SelectedDistrib.Suffix
        End If
        'Main.GenerateData()
        UpdateData()
        Main.UpdateAnnotationSettings(SelDistrib)
        Main.ReplotCharts()
    End Sub
    Private Sub btnParamCIncr_Click(sender As Object, e As EventArgs) Handles btnParamCIncr.Click
        'Increment Parameter C
        ParamCVal = ParamCVal + ParamCIncr
        ApplyParamCVal()

    End Sub

    Private Sub btnParamDDecr_Click(sender As Object, e As EventArgs) Handles btnParamDDecr.Click
        'Decrement Parameter D
        ParamDVal = ParamDVal - ParamDIncr
        ApplyParamDVal()

    End Sub

    Private Sub ApplyParamDVal()
        'Apply the new value in ParamDVal.

        SelectedDistrib.ParamD.Value = ParamDVal

        If cmbX1.SelectedIndex = 2 Then
            ABInfo.XParamVal = ParamDVal
            txtX1.Text = ParamDVal
            MouseABPosNow.X = ABInfo.XPos
            pbAB.Invalidate() 'Redraw pbAB
        ElseIf cmbY1.SelectedIndex = 2 Then
            ABInfo.YParamVal = ParamDVal
            txtY1.Text = ParamDVal
            MouseABPosNow.Y = ABInfo.YPos
            pbAB.Invalidate() 'Redraw pbAB
        ElseIf cmbX2.SelectedIndex = 2 Then
            CDInfo.XParamVal = ParamDVal
            txtX2.Text = ParamDVal
            MouseCDPosNow.X = ABInfo.XPos
            pbCD.Invalidate() 'Redraw pbCD
        ElseIf cmbY2.SelectedIndex = 2 Then
            CDInfo.YParamVal = ParamDVal
            txtY2.Text = ParamDVal
            MouseCDPosNow.Y = ABInfo.YPos
            pbCD.Invalidate() 'Redraw pbCD
        End If

        txtD.Text = ParamDVal
        hsD.Value = 1000 * (ParamDVal - ParamDAdjustMin) / (ParamDAdjustMax - ParamDAdjustMin)
        'DInfo.XParamVal = ParamDVal
        'MouseDPosNow.X = DInfo.XPos
        'pbD.Invalidate() 'Redraw pbD

        If Main.SelDistrib = SelDistrib And Main.dgvParams.Rows.Count > 3 Then Main.dgvParams.Rows(3).Cells(2).Value = ParamDVal


        'UpdateSuffix()
        If SelectedDistrib.AutoUpdateLabels Then
            SelectedDistrib.UpdateSuffix() 'This will also update the series labels.
            txtSuffix.Text = SelectedDistrib.Suffix
        End If

        UpdateData()
        Main.UpdateAnnotationSettings(SelDistrib)
        Main.ReplotCharts()
    End Sub
    Private Sub btnParamDIncr_Click(sender As Object, e As EventArgs) Handles btnParamDIncr.Click
        'Increment Parameter D
        ParamDVal = ParamDVal + ParamDIncr
        ApplyParamDVal()

    End Sub

    Private Sub btnParamEDecr_Click(sender As Object, e As EventArgs) Handles btnParamEDecr.Click
        'Decrement Parameter E
        ParamEVal = ParamEVal - ParamEIncr
        ApplyParamEVal()

    End Sub

    Private Sub ApplyParamEVal()
        'Apply the new value in ParamEVal.

        SelectedDistrib.ParamE.Value = ParamEVal

        If cmbX1.SelectedIndex = 2 Then
            ABInfo.XParamVal = ParamEVal
            txtX1.Text = ParamEVal
            MouseABPosNow.X = ABInfo.XPos
            pbAB.Invalidate() 'Redraw pbAB
        ElseIf cmbY1.SelectedIndex = 2 Then
            ABInfo.YParamVal = ParamEVal
            txtY1.Text = ParamEVal
            MouseABPosNow.Y = ABInfo.YPos
            pbAB.Invalidate() 'Redraw pbAB
        ElseIf cmbX2.SelectedIndex = 2 Then
            CDInfo.XParamVal = ParamEVal
            txtX2.Text = ParamEVal
            MouseCDPosNow.X = ABInfo.XPos
            pbCD.Invalidate() 'Redraw pbCD
        ElseIf cmbY2.SelectedIndex = 2 Then
            CDInfo.YParamVal = ParamEVal
            txtY2.Text = ParamEVal
            MouseCDPosNow.Y = ABInfo.YPos
            pbCD.Invalidate() 'Redraw pbCD
        End If

        txtE.Text = ParamEVal
        hsE.Value = 1000 * (ParamEVal - ParamEAdjustMin) / (ParamEAdjustMax - ParamEAdjustMin)
        'EInfo.XParamVal = ParamEVal
        'MouseEPosNow.X = EInfo.XPos
        'pbE.Invalidate() 'Redraw pbE

        If Main.SelDistrib = SelDistrib And Main.dgvParams.Rows.Count > 3 Then Main.dgvParams.Rows(4).Cells(2).Value = ParamEVal


        'UpdateSuffix()
        If SelectedDistrib.AutoUpdateLabels Then
            SelectedDistrib.UpdateSuffix() 'This will also update the series labels.
            txtSuffix.Text = SelectedDistrib.Suffix
        End If

        UpdateData()
        Main.UpdateAnnotationSettings(SelDistrib)
        Main.ReplotCharts()
    End Sub

    Private Sub btnParamEIncr_Click(sender As Object, e As EventArgs) Handles btnParamEIncr.Click
        'Increment Parameter E
        ParamEVal = ParamEVal + ParamEIncr
        ApplyParamEVal()

    End Sub

    Private Sub UpdateData()
        'Recalculate the data for the selected distribution.

        If SelDistrib > 0 Then
            Main.Distribution.UpdateData(SelDistrib)
            If chkUpdateLogLikelihood.Checked Then txtLogLikelihood.Text = SelectedDistrib.ParamEst.LogLikelihoodFn
            If SelectedDistrib.ParamEst.Data.Tables("Samples").Rows.Count > 0 Then
                If IsNothing(SeriesAnalysis) Then
                    'The Series Analysis chart is not displayed.
                Else
                    UpdateModelData()
                    SeriesAnalysis.UpdateCharts()
                End If
            End If

        End If

    End Sub

    Private Sub txtParamAIncr_TextChanged(sender As Object, e As EventArgs) Handles txtParamAIncr.TextChanged

    End Sub

    Private Sub txtParamAIncr_LostFocus(sender As Object, e As EventArgs) Handles txtParamAIncr.LostFocus
        Try
            _paramAIncr = txtParamAIncr.Text
            SelectedDistrib.ParamA.Increment = _paramAIncr
        Catch ex As Exception
            Main.Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub

    Private Sub txtParamBIncr_TextChanged(sender As Object, e As EventArgs) Handles txtParamBIncr.TextChanged

    End Sub

    Private Sub txtParamBIncr_LostFocus(sender As Object, e As EventArgs) Handles txtParamBIncr.LostFocus
        Try
            _paramBIncr = txtParamBIncr.Text
            SelectedDistrib.ParamB.Increment = _paramBIncr
        Catch ex As Exception
            Main.Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub

    Private Sub txtParamCIncr_TextChanged(sender As Object, e As EventArgs) Handles txtParamCIncr.TextChanged

    End Sub

    Private Sub txtParamCIncr_LostFocus(sender As Object, e As EventArgs) Handles txtParamCIncr.LostFocus
        Try
            _paramCIncr = txtParamCIncr.Text
            SelectedDistrib.ParamC.Increment = _paramCIncr
        Catch ex As Exception
            Main.Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub

    Private Sub txtParamDIncr_TextChanged(sender As Object, e As EventArgs) Handles txtParamDIncr.TextChanged

    End Sub

    Private Sub txtParamDIncr_LostFocus(sender As Object, e As EventArgs) Handles txtParamDIncr.LostFocus
        Try
            _paramDIncr = txtParamDIncr.Text
            SelectedDistrib.ParamD.Increment = _paramDIncr
        Catch ex As Exception
            Main.Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub

    Private Sub txtParamEIncr_TextChanged(sender As Object, e As EventArgs) Handles txtParamEIncr.TextChanged

    End Sub

    Private Sub txtParamEIncr_LostFocus(sender As Object, e As EventArgs) Handles txtParamEIncr.LostFocus
        Try
            _paramEIncr = txtParamEIncr.Text
            SelectedDistrib.ParamE.Increment = _paramEIncr
        Catch ex As Exception
            Main.Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub

    Private Sub txtParamAValue_TextChanged(sender As Object, e As EventArgs) Handles txtParamAValue.TextChanged

    End Sub

    Private Sub txtParamAValue_LostFocus(sender As Object, e As EventArgs) Handles txtParamAValue.LostFocus
        Try
            _paramAVal = txtParamAValue.Text
            SelectedDistrib.ParamA.Value = ParamAVal
            'If rbPrimary.Checked And Main.dgvParams.Rows.Count > 0 Then Main.dgvParams.Rows(0).Cells(2).Value = ParamAVal
            If Main.SelDistrib = SelDistrib And Main.dgvParams.Rows.Count > 0 Then Main.dgvParams.Rows(0).Cells(2).Value = ParamAVal

            'UpdateSeriesName()
            'UpdateSuffix()
            If SelectedDistrib.AutoUpdateLabels Then
                SelectedDistrib.UpdateSuffix() 'This will also update the series labels.
                txtSuffix.Text = SelectedDistrib.Suffix
            End If
            'Main.GenerateData()
            UpdateData()
            Main.UpdateAnnotationSettings(SelDistrib)
            Main.ReplotCharts()
        Catch ex As Exception
            Main.Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub

    Private Sub txtParamBValue_TextChanged(sender As Object, e As EventArgs) Handles txtParamBValue.TextChanged

    End Sub

    Private Sub txtParamBValue_LostFocus(sender As Object, e As EventArgs) Handles txtParamBValue.LostFocus
        Try
            _paramBVal = txtParamBValue.Text
            SelectedDistrib.ParamB.Value = ParamBVal
            'If rbPrimary.Checked And Main.dgvParams.Rows.Count > 1 Then Main.dgvParams.Rows(1).Cells(2).Value = ParamBVal
            If Main.SelDistrib = SelDistrib And Main.dgvParams.Rows.Count > 1 Then Main.dgvParams.Rows(1).Cells(2).Value = ParamBVal

            'UpdateSeriesName()
            'UpdateSuffix()
            If SelectedDistrib.AutoUpdateLabels Then
                SelectedDistrib.UpdateSuffix() 'This will also update the series labels.
                txtSuffix.Text = SelectedDistrib.Suffix
            End If
            'Main.GenerateData()
            UpdateData()
            Main.UpdateAnnotationSettings(SelDistrib)
            Main.ReplotCharts()
        Catch ex As Exception
            Main.Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub

    Private Sub txtParamCValue_TextChanged(sender As Object, e As EventArgs) Handles txtParamCValue.TextChanged

    End Sub

    Private Sub txtParamCValue_LostFocus(sender As Object, e As EventArgs) Handles txtParamCValue.LostFocus
        Try
            _paramCVal = txtParamCValue.Text
            SelectedDistrib.ParamC.Value = ParamCVal
            'If rbPrimary.Checked And Main.dgvParams.Rows.Count > 2 Then Main.dgvParams.Rows(2).Cells(2).Value = ParamCVal
            If Main.SelDistrib = SelDistrib And Main.dgvParams.Rows.Count > 2 Then Main.dgvParams.Rows(2).Cells(2).Value = ParamCVal
            'UpdateSeriesName()
            'UpdateSuffix()
            If SelectedDistrib.AutoUpdateLabels Then
                SelectedDistrib.UpdateSuffix() 'This will also update the series labels.
                txtSuffix.Text = SelectedDistrib.Suffix
            End If
            'Main.GenerateData()
            UpdateData()
            Main.UpdateAnnotationSettings(SelDistrib)
            Main.ReplotCharts()
        Catch ex As Exception
            Main.Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub

    Private Sub txtParamDValue_TextChanged(sender As Object, e As EventArgs) Handles txtParamDValue.TextChanged

    End Sub

    Private Sub txtParamDValue_LostFocus(sender As Object, e As EventArgs) Handles txtParamDValue.LostFocus
        Try
            _paramDVal = txtParamDValue.Text
            SelectedDistrib.ParamD.Value = ParamDVal
            'If rbPrimary.Checked And Main.dgvParams.Rows.Count > 3 Then Main.dgvParams.Rows(3).Cells(2).Value = ParamDVal
            If Main.SelDistrib = SelDistrib And Main.dgvParams.Rows.Count > 3 Then Main.dgvParams.Rows(3).Cells(2).Value = ParamDVal
            'UpdateSeriesName()
            'UpdateSuffix()
            If SelectedDistrib.AutoUpdateLabels Then
                SelectedDistrib.UpdateSuffix() 'This will also update the series labels.
                txtSuffix.Text = SelectedDistrib.Suffix
            End If
            'Main.GenerateData()
            UpdateData()
            Main.UpdateAnnotationSettings(SelDistrib)
            Main.ReplotCharts()
        Catch ex As Exception
            Main.Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub

    Private Sub txtParamEValue_TextChanged(sender As Object, e As EventArgs) Handles txtParamEValue.TextChanged

    End Sub

    Private Sub txtParamEValue_LostFocus(sender As Object, e As EventArgs) Handles txtParamEValue.LostFocus
        Try
            _paramEVal = txtParamEValue.Text
            SelectedDistrib.ParamE.Value = ParamEVal
            'If rbPrimary.Checked And Main.dgvParams.Rows.Count > 4 Then Main.dgvParams.Rows(4).Cells(2).Value = ParamEVal
            If Main.SelDistrib = SelDistrib And Main.dgvParams.Rows.Count > 4 Then Main.dgvParams.Rows(4).Cells(2).Value = ParamEVal
            'UpdateSeriesName()
            'UpdateSuffix()
            If SelectedDistrib.AutoUpdateLabels Then
                SelectedDistrib.UpdateSuffix() 'This will also update the series labels.
                txtSuffix.Text = SelectedDistrib.Suffix
            End If
            'Main.GenerateData()
            UpdateData()
            Main.UpdateAnnotationSettings(SelDistrib)
            Main.ReplotCharts()
        Catch ex As Exception
            Main.Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub

    Private Sub btnRestore_Click(sender As Object, e As EventArgs) Handles btnRestore.Click
        'Restore the original parameter values

        ParamAVal = ParamAOrigVal
        'Main.Distribution.Distrib.ParamA.Value = ParamAOrigVal
        SelectedDistrib.ParamA.Value = ParamAOrigVal
        'If Main.dgvParams.Rows.Count > 0 Then Main.dgvParams.Rows(0).Cells(2).Value = ParamAOrigVal
        'If rbPrimary.Checked And Main.dgvParams.Rows.Count > 0 Then Main.dgvParams.Rows(0).Cells(2).Value = ParamAOrigVal 'Update the Primary Distribution parameters displayed on the Main form.
        If Main.SelDistrib = SelDistrib And Main.dgvParams.Rows.Count > 0 Then Main.dgvParams.Rows(0).Cells(2).Value = ParamAOrigVal
        If NParams > 1 Then
            ParamBVal = ParamBOrigVal
            SelectedDistrib.ParamB.Value = ParamBOrigVal
            'If rbPrimary.Checked And Main.dgvParams.Rows.Count > 1 Then Main.dgvParams.Rows(1).Cells(2).Value = ParamBOrigVal 'Update the Primary Distribution parameters displayed on the Main form.
            If Main.SelDistrib = SelDistrib And Main.dgvParams.Rows.Count > 1 Then Main.dgvParams.Rows(1).Cells(2).Value = ParamBOrigVal
            If NParams > 2 Then
                ParamCVal = ParamCOrigVal
                SelectedDistrib.ParamC.Value = ParamCOrigVal
                'If rbPrimary.Checked And Main.dgvParams.Rows.Count > 2 Then Main.dgvParams.Rows(2).Cells(2).Value = ParamCOrigVal 'Update the Primary Distribution parameters displayed on the Main form.
                If Main.SelDistrib = SelDistrib And Main.dgvParams.Rows.Count > 2 Then Main.dgvParams.Rows(2).Cells(2).Value = ParamCOrigVal
                If NParams > 3 Then
                    ParamDVal = ParamDOrigVal
                    SelectedDistrib.ParamD.Value = ParamDOrigVal
                    'If rbPrimary.Checked And Main.dgvParams.Rows.Count > 3 Then Main.dgvParams.Rows(3).Cells(2).Value = ParamDOrigVal 'Update the Primary Distribution parameters displayed on the Main form.
                    If Main.SelDistrib = SelDistrib And Main.dgvParams.Rows.Count > 3 Then Main.dgvParams.Rows(3).Cells(2).Value = ParamDOrigVal
                    If NParams > 4 Then
                        ParamEVal = ParamEOrigVal
                        SelectedDistrib.ParamE.Value = ParamEOrigVal
                        'If rbPrimary.Checked And Main.dgvParams.Rows.Count > 4 Then Main.dgvParams.Rows(4).Cells(2).Value = ParamEOrigVal 'Update the Primary Distribution parameters displayed on the Main form.
                        If Main.SelDistrib = SelDistrib And Main.dgvParams.Rows.Count > 4 Then Main.dgvParams.Rows(4).Cells(2).Value = ParamEOrigVal
                    End If
                End If
            End If
        End If
        'UpdateSuffix()
        If SelectedDistrib.AutoUpdateLabels Then
            SelectedDistrib.UpdateSuffix() 'This will also update the series labels.
            txtSuffix.Text = SelectedDistrib.Suffix
        End If
        'Main.GenerateData()
        UpdateData()
        Main.UpdateAnnotationSettings(SelDistrib)
        Main.ReplotCharts()
    End Sub

    Public Sub DefaultSymbols()
        'Show the default parameter symbols.

        'Clear the current symbols:
        txtParamASymbol.Text = ""
        txtParamBSymbol.Text = ""
        txtParamCSymbol.Text = ""
        txtParamDSymbol.Text = ""
        txtParamESymbol.Text = ""

        txtParamASymbol.Text = NameToSymbol(SelectedDistrib.ParamA.Symbol)
        If NParams > 1 Then
            txtParamBSymbol.Text = NameToSymbol(SelectedDistrib.ParamB.Symbol)
            If NParams > 2 Then
                txtParamCSymbol.Text = NameToSymbol(SelectedDistrib.ParamC.Symbol)
                If NParams > 3 Then
                    txtParamDSymbol.Text = NameToSymbol(SelectedDistrib.ParamD.Symbol)
                    If NParams > 4 Then
                        txtParamESymbol.Text = NameToSymbol(SelectedDistrib.ParamE.Symbol)
                    End If
                End If
            End If
        End If


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

    Public Sub LoadParamInfo()
        'Load the parameter information from the selected distribution.

        'NParams = Distribution.Distrib.NParams
        NParams = SelectedDistrib.NParams
        EnableGraphControls(NParams)

        'NOTE: All rounding is now done within the Property.
        'ABInfo.Round = Round
        'ABInfo.DecRounding = DecRounding


        Dim SampInt As Double
        If SelectedDistrib.Continuity = "Continuous" Then
            SampInt = Main.Distribution.ContSampling.Interval
        Else
            SampInt = 1
        End If
        'If SelectedDistrib.ParamA.Increment = 0 Then SelectedDistrib.ParamA.Increment = SampInt / 5
        'If SelectedDistrib.ParamB.Increment = 0 Then SelectedDistrib.ParamB.Increment = SampInt / 5
        'If SelectedDistrib.ParamC.Increment = 0 Then SelectedDistrib.ParamC.Increment = SampInt / 5
        'If SelectedDistrib.ParamD.Increment = 0 Then SelectedDistrib.ParamD.Increment = SampInt / 5
        'If SelectedDistrib.ParamE.Increment = 0 Then SelectedDistrib.ParamE.Increment = SampInt / 5
        If SelectedDistrib.ParamA.Increment = 0 Then SelectedDistrib.ParamA.Increment = SampInt * 2
        If SelectedDistrib.ParamB.Increment = 0 Then SelectedDistrib.ParamB.Increment = SampInt * 2
        If SelectedDistrib.ParamC.Increment = 0 Then SelectedDistrib.ParamC.Increment = SampInt * 2
        If SelectedDistrib.ParamD.Increment = 0 Then SelectedDistrib.ParamD.Increment = SampInt * 2
        If SelectedDistrib.ParamE.Increment = 0 Then SelectedDistrib.ParamE.Increment = SampInt * 2

        ParamAName = SelectedDistrib.ParamA.Name
        txtParamASymbol.Text = NameToSymbol(SelectedDistrib.ParamA.Symbol)
        ParamAOrigVal = SelectedDistrib.ParamA.Value
        ParamAVal = SelectedDistrib.ParamA.Value
        ParamAIncr = SelectedDistrib.ParamA.Increment
        ParamAAdjustMin = SelectedDistrib.ParamA.AdjustMin
        ParamAAdjustMax = SelectedDistrib.ParamA.AdjustMax

        ABInfo.XParamVal = ParamAVal
        ABInfo.XParamMin = ParamAAdjustMin
        ABInfo.XParamMax = ParamAAdjustMax
        ABInfo.XMin = 0
        ABInfo.XMax = pbAB.Width
        MouseABPosNow.X = ABInfo.XPos

        'AInfo.XParamVal = ParamAVal
        'AInfo.XParamMin = ParamAAdjustMin
        'AInfo.XParamMax = ParamAAdjustMax
        'AInfo.XMin = 0
        'AInfo.XMax = pbA.Width
        'MouseAPosNow.X = AInfo.XPos

        'hsA.Minimum = 1
        hsA.Minimum = 0
        hsA.LargeChange = 100
        'hsA.Maximum = 1000 + hsA.LargeChange - 1
        hsA.Maximum = 1000 + hsA.LargeChange
        hsA.Value = 1000 * (ParamAVal - ParamAAdjustMin) / (ParamAAdjustMax - ParamAAdjustMin)


        cmbX1.Items.Add("None")
        cmbY1.Items.Add("None")
        cmbX2.Items.Add("None")
        cmbY2.Items.Add("None")

        cmbX1.Items.Add(ParamAName)
        cmbY1.Items.Add(ParamAName)
        cmbX2.Items.Add(ParamAName)
        cmbY2.Items.Add(ParamAName)

        cmbX1.SelectedIndex = 1
        cmbY1.SelectedIndex = 0
        cmbX2.SelectedIndex = 0
        cmbY2.SelectedIndex = 0

        txtAName.Text = ParamAName
        txtA.Text = ParamAVal
        txtAMin.Text = ParamAAdjustMin
        txtAMax.Text = ParamAAdjustMax

        If SelectedDistrib.NParams > 1 Then
            ParamBName = SelectedDistrib.ParamB.Name
            txtParamBSymbol.Text = NameToSymbol(SelectedDistrib.ParamB.Symbol)
            ParamBOrigVal = SelectedDistrib.ParamB.Value
            ParamBVal = SelectedDistrib.ParamB.Value
            ParamBIncr = SelectedDistrib.ParamB.Increment
            ParamBAdjustMin = SelectedDistrib.ParamB.AdjustMin
            ParamBAdjustMax = SelectedDistrib.ParamB.AdjustMax

            ABInfo.YParamVal = ParamBVal
            ABInfo.YParamMin = ParamBAdjustMin
            ABInfo.YParamMax = ParamBAdjustMax
            ABInfo.YMin = pbAB.Height
            ABInfo.YMax = 0
            MouseABPosNow.Y = ABInfo.YPos

            'BInfo.XParamVal = ParamBVal
            'BInfo.XParamMin = ParamBAdjustMin
            'BInfo.XParamMax = ParamBAdjustMax
            'BInfo.XMin = 0
            'BInfo.XMax = pbB.Width
            'MouseBPosNow.X = BInfo.XPos

            hsB.Minimum = 0
            hsB.LargeChange = 100
            hsB.Maximum = 1000 + hsB.LargeChange
            hsB.Value = 1000 * (ParamBVal - ParamBAdjustMin) / (ParamBAdjustMax - ParamBAdjustMin)

            cmbX1.Items.Add(ParamBName)
            cmbY1.Items.Add(ParamBName)
            cmbX2.Items.Add(ParamBName)
            cmbY2.Items.Add(ParamBName)
            cmbY1.SelectedIndex = 2

            txtBName.Text = ParamBName
            txtB.Text = ParamBVal
            txtBMin.Text = ParamBAdjustMin
            txtBMax.Text = ParamBAdjustMax

            If SelectedDistrib.NParams > 2 Then
                ParamCName = SelectedDistrib.ParamC.Name
                txtParamCSymbol.Text = NameToSymbol(SelectedDistrib.ParamC.Symbol)
                ParamCOrigVal = SelectedDistrib.ParamC.Value
                ParamCVal = SelectedDistrib.ParamC.Value
                ParamCIncr = SelectedDistrib.ParamC.Increment
                ParamCAdjustMin = SelectedDistrib.ParamC.AdjustMin
                ParamCAdjustMax = SelectedDistrib.ParamC.AdjustMax

                CDInfo.XParamVal = ParamCVal
                CDInfo.XParamMin = ParamCAdjustMin
                CDInfo.XParamMax = ParamCAdjustMax
                CDInfo.XMin = 0
                CDInfo.XMax = pbCD.Width
                MouseCDPosNow.X = CDInfo.XPos

                'CInfo.XParamVal = ParamCVal
                'CInfo.XParamMin = ParamCAdjustMin
                'CInfo.XParamMax = ParamCAdjustMax
                'CInfo.XMin = 0
                'CInfo.XMax = pbC.Width
                'MouseCPosNow.X = CInfo.XPos

                hsC.Minimum = 0
                hsC.LargeChange = 100
                hsC.Maximum = 1000 + hsC.LargeChange
                hsC.Value = 1000 * (ParamCVal - ParamCAdjustMin) / (ParamCAdjustMax - ParamCAdjustMin)

                cmbX1.Items.Add(ParamCName)
                cmbY1.Items.Add(ParamCName)
                cmbX2.Items.Add(ParamCName)
                cmbY2.Items.Add(ParamCName)
                cmbX2.SelectedIndex = 3

                txtCName.Text = ParamCName
                txtC.Text = ParamCVal
                txtCMin.Text = ParamCAdjustMin
                txtCMax.Text = ParamCAdjustMax

                If SelectedDistrib.NParams > 3 Then
                    ParamCName = SelectedDistrib.ParamD.Name
                    txtParamDSymbol.Text = NameToSymbol(SelectedDistrib.ParamD.Symbol)
                    ParamDOrigVal = SelectedDistrib.ParamD.Value
                    ParamDVal = SelectedDistrib.ParamD.Value
                    ParamDIncr = SelectedDistrib.ParamD.Increment
                    ParamDAdjustMin = SelectedDistrib.ParamD.AdjustMin
                    ParamDAdjustMax = SelectedDistrib.ParamD.AdjustMax

                    CDInfo.YParamVal = ParamDVal
                    CDInfo.YParamMin = ParamDAdjustMin
                    CDInfo.YParamMax = ParamDAdjustMax
                    CDInfo.YMin = pbAB.Height
                    CDInfo.YMax = 0
                    MouseCDPosNow.Y = CDInfo.YPos

                    'DInfo.XParamVal = ParamDVal
                    'DInfo.XParamMin = ParamDAdjustMin
                    'DInfo.XParamMax = ParamDAdjustMax
                    'DInfo.XMin = 0
                    'DInfo.XMax = pbD.Width
                    'MouseDPosNow.X = DInfo.XPos

                    hsD.Minimum = 0
                    hsD.LargeChange = 100
                    hsD.Maximum = 1000 + hsD.LargeChange
                    hsD.Value = 1000 * (ParamDVal - ParamDAdjustMin) / (ParamDAdjustMax - ParamDAdjustMin)

                    cmbX1.Items.Add(ParamCName)
                    cmbY1.Items.Add(ParamCName)
                    cmbX2.Items.Add(ParamCName)
                    cmbY2.Items.Add(ParamCName)
                    cmbY2.SelectedIndex = 4

                    txtDName.Text = ParamDName
                    txtD.Text = ParamDVal
                    txtDMin.Text = ParamDAdjustMin
                    txtDMax.Text = ParamDAdjustMax

                    If SelectedDistrib.NParams > 4 Then
                        ParamEName = SelectedDistrib.ParamE.Name
                        txtParamESymbol.Text = NameToSymbol(SelectedDistrib.ParamE.Symbol)
                        ParamEOrigVal = SelectedDistrib.ParamE.Value
                        ParamEVal = SelectedDistrib.ParamE.Value
                        ParamEIncr = SelectedDistrib.ParamE.Increment
                        ParamEAdjustMin = SelectedDistrib.ParamE.AdjustMin
                        ParamEAdjustMax = SelectedDistrib.ParamE.AdjustMax

                        'EInfo.XParamVal = ParamEVal
                        'EInfo.XParamMin = ParamEAdjustMin
                        'EInfo.XParamMax = ParamEAdjustMax
                        'EInfo.XMin = 0
                        'EInfo.XMax = pbD.Width
                        'MouseEPosNow.X = EInfo.XPos

                        hsE.Minimum = 0
                        hsE.LargeChange = 100
                        hsE.Maximum = 1000 + hsE.LargeChange
                        hsE.Value = 1000 * (ParamEVal - ParamEAdjustMin) / (ParamEAdjustMax - ParamEAdjustMin)

                        cmbX1.Items.Add(ParamEName)
                        cmbY1.Items.Add(ParamEName)
                        cmbX2.Items.Add(ParamEName)
                        cmbY2.Items.Add(ParamEName)
                        cmbY2.SelectedIndex = 5

                        txtEName.Text = ParamEName
                        txtE.Text = ParamEVal
                        txtEMin.Text = ParamEAdjustMin
                        txtEMax.Text = ParamEAdjustMax

                    End If
                End If
            End If
        End If

        txtSuffix.Text = SelectedDistrib.Suffix

        txtDistribColor.BackColor = SelectedDistrib.Display.MarkerColor

        txtSampSetName.Text = SelectedDistrib.ParamEst.SampleSetName
        txtSampSetLabel.Text = SelectedDistrib.ParamEst.SampleSetLabel
        txtSampSetUnits.Text = SelectedDistrib.ParamEst.SampleSetUnits
        txtSampSetDescr.Text = SelectedDistrib.ParamEst.SampleSetDescription
        dgvDataValues.DataSource = SelectedDistrib.ParamEst.Data.Tables("Samples")
        dgvDataValues.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        dgvDataValues.AutoResizeColumns()

        If SelectedDistrib.ParamEst.Data.Tables.Contains("Samples") Then
            If SelectedDistrib.ParamEst.Data.Tables("Samples").Rows.Count > 0 Then
                txtNSamples.Text = SelectedDistrib.ParamEst.Data.Tables("Samples").Rows.Count
                txtMean.Text = SelectedDistrib.ParamEst.Data.Tables("Samples").Compute("Avg(Value)", "")
                txtStdDev.Text = SelectedDistrib.ParamEst.Data.Tables("Samples").Compute("StDev(Value)", "")
            Else
                txtNSamples.Text = "0"
                txtMean.Text = ""
                txtStdDev.Text = ""
            End If
        Else
            txtNSamples.Text = ""
            txtMean.Text = ""
            txtStdDev.Text = ""
        End If
    End Sub

    Private Sub EnableGraphControls(NParams As Integer)
        'Enable the graphical controls for NParams.

        'Deactivate controls for NParams > 1
        pbAB.Enabled = False
        'pbAB.BackColor = Color.Silver     '192, 192, 192
        'pbAB.BackColor = Color.LightGray  '211, 211, 211
        'pbAB.BackColor = Color.Gainsboro  '220, 220, 220
        'pbAB.BackColor = Color.GhostWhite '248, 248, 255
        pbAB.BackColor = Color.WhiteSmoke  '245, 245, 245
        cmbX1.Enabled = False
        txtX1.Enabled = False
        txtX1Min.Enabled = False
        txtX1Max.Enabled = False
        cmbY1.Enabled = False
        txtY1.Enabled = False
        txtY1Min.Enabled = False
        txtY1Max.Enabled = False

        pbCD.Enabled = False
        'pbCD.BackColor = Color.Silver
        'pbCD.BackColor = Color.LightGray
        'pbCD.BackColor = Color.Gainsboro
        'pbCD.BackColor = Color.GhostWhite
        pbCD.BackColor = Color.WhiteSmoke
        cmbX2.Enabled = False
        txtX2.Enabled = False
        txtX2Min.Enabled = False
        txtX2Max.Enabled = False
        cmbY2.Enabled = False
        txtY2.Enabled = False
        txtY2Min.Enabled = False
        txtY2Max.Enabled = False

        'pbB.Enabled = False
        'pbB.BackColor = Color.WhiteSmoke
        hsB.Enabled = False
        'cmbB.Enabled = False
        txtBName.Enabled = False
        txtB.Enabled = False
        txtBMin.Enabled = False
        txtBMax.Enabled = False

        'pbC.Enabled = False
        'pbC.BackColor = Color.WhiteSmoke
        hsC.Enabled = False
        'cmbC.Enabled = False
        txtCName.Enabled = False
        txtC.Enabled = False
        txtCMin.Enabled = False
        txtCMax.Enabled = False

        'pbD.Enabled = False
        'pbD.BackColor = Color.WhiteSmoke
        hsD.Enabled = False
        'cmbD.Enabled = False
        txtDName.Enabled = False
        txtD.Enabled = False
        txtDMin.Enabled = False
        txtDMax.Enabled = False

        'pbE.Enabled = False
        'pbE.BackColor = Color.WhiteSmoke
        hsE.Enabled = False
        'cmbE.Enabled = False
        txtEName.Enabled = False
        txtE.Enabled = False
        txtEMin.Enabled = False
        txtEMax.Enabled = False

        If NParams > 1 Then
            pbAB.Enabled = True
            pbAB.BackColor = Color.White
            cmbX1.Enabled = True
            txtX1.Enabled = True
            txtX1Min.Enabled = True
            txtX1Max.Enabled = True
            cmbY1.Enabled = True
            txtY1.Enabled = True
            txtY1Min.Enabled = True
            txtY1Max.Enabled = True

            'pbB.Enabled = True
            'pbB.BackColor = Color.White
            hsB.Enabled = True
            'cmbB.Enabled = True
            txtBName.Enabled = True
            txtB.Enabled = True
            txtBMin.Enabled = True
            txtBMax.Enabled = True

            If NParams > 2 Then
                'pbC.Enabled = True
                'pbC.BackColor = Color.White
                hsC.Enabled = True
                'cmbC.Enabled = True
                txtCName.Enabled = True
                txtC.Enabled = True
                txtCMin.Enabled = True
                txtCMax.Enabled = True

                If NParams > 3 Then
                    pbCD.Enabled = True
                    pbCD.BackColor = Color.White
                    cmbX2.Enabled = True
                    txtX2.Enabled = True
                    txtX2Min.Enabled = True
                    txtX2Max.Enabled = True
                    cmbY2.Enabled = True
                    txtY2.Enabled = True
                    txtY2Min.Enabled = True
                    txtY2Max.Enabled = True

                    'pbD.Enabled = True
                    'pbD.BackColor = Color.White
                    hsD.Enabled = True
                    'cmbD.Enabled = True
                    txtDName.Enabled = True
                    txtD.Enabled = True
                    txtDMin.Enabled = True
                    txtDMax.Enabled = True

                    If NParams > 4 Then
                        'pbE.Enabled = True
                        'pbE.BackColor = Color.White
                        hsE.Enabled = True
                        'cmbE.Enabled = True
                        txtEName.Enabled = True
                        txtE.Enabled = True
                        txtEMin.Enabled = True
                        txtEMax.Enabled = True

                    End If
                End If
            End If
        End If
    End Sub

    Private Sub UpdateABPos()
        'Update the cursor position on pbAB

    End Sub

    ''Public Sub LoadParamInfo()
    'Public Sub LoadPrimaryParamInfo()
    '    'Load the parameter information from the Primary distribution.

    '    rbPrimary.Checked = True

    '    NParams = Distribution.Distrib.NParams
    '    ParamAName = Distribution.Distrib.ParamA.Name
    '    txtParamASymbol.Text = NameToSymbol(Distribution.Distrib.ParamA.Symbol)
    '    ParamAOrigVal = Distribution.Distrib.ParamA.Value
    '    ParamAVal = Distribution.Distrib.ParamA.Value

    '    Dim SampInt As Double
    '    If Distribution.Distrib.Continuity = "Continuous" Then
    '        SampInt = Distribution.ContSampling.Interval
    '    Else
    '        SampInt = 1
    '    End If
    '    If Distribution.Distrib.ParamA.Increment = 0 Then Distribution.Distrib.ParamA.Increment = SampInt / 5
    '    If Distribution.Distrib.ParamB.Increment = 0 Then Distribution.Distrib.ParamB.Increment = SampInt / 5
    '    If Distribution.Distrib.ParamC.Increment = 0 Then Distribution.Distrib.ParamC.Increment = SampInt / 5
    '    If Distribution.Distrib.ParamD.Increment = 0 Then Distribution.Distrib.ParamD.Increment = SampInt / 5
    '    If Distribution.Distrib.ParamE.Increment = 0 Then Distribution.Distrib.ParamE.Increment = SampInt / 5

    '    ParamAIncr = Distribution.Distrib.ParamA.Increment

    '    If Distribution.Distrib.NParams > 1 Then
    '        ParamBName = Distribution.Distrib.ParamB.Name
    '        txtParamBSymbol.Text = NameToSymbol(Distribution.Distrib.ParamB.Symbol)
    '        ParamBOrigVal = Distribution.Distrib.ParamB.Value
    '        ParamBVal = Distribution.Distrib.ParamB.Value
    '        ParamBIncr = Distribution.Distrib.ParamB.Increment
    '        If Distribution.Distrib.NParams > 2 Then
    '            ParamCName = Distribution.Distrib.ParamC.Name
    '            txtParamCSymbol.Text = NameToSymbol(Distribution.Distrib.ParamC.Symbol)
    '            ParamCOrigVal = Distribution.Distrib.ParamC.Value
    '            ParamCVal = Distribution.Distrib.ParamC.Value
    '            ParamCIncr = Distribution.Distrib.ParamC.Increment
    '            If Distribution.Distrib.NParams > 3 Then
    '                ParamDName = Distribution.Distrib.ParamD.Name
    '                txtParamDSymbol.Text = NameToSymbol(Distribution.Distrib.ParamD.Symbol)
    '                ParamDOrigVal = Distribution.Distrib.ParamD.Value
    '                ParamDVal = Distribution.Distrib.ParamD.Value
    '                ParamDIncr = Distribution.Distrib.ParamD.Increment
    '                If Distribution.Distrib.NParams > 4 Then
    '                    ParamEName = Distribution.Distrib.ParamE.Name
    '                    txtParamESymbol.Text = NameToSymbol(Distribution.Distrib.ParamE.Symbol)
    '                    ParamEOrigVal = Distribution.Distrib.ParamE.Value
    '                    ParamEVal = Distribution.Distrib.ParamE.Value
    '                    ParamEIncr = Distribution.Distrib.ParamE.Increment
    '                End If
    '            End If
    '        End If
    '    End If
    '    UpdateSuffix()
    'End Sub

    'Private Sub UpdateSeriesName()
    'Private Sub UpdateSuffix()
    'Update the series name using the parameter values.

    'txtSuffix.Text = txtParamASymbol.Text.Trim & "=" & txtParamAValue.Text.Trim
    'If Distribution.Distrib.NParams > 1 Then
    '    txtSuffix.Text = txtSuffix.Text & ", " & txtParamBSymbol.Text.Trim & "=" & txtParamBValue.Text.Trim
    '    If Distribution.Distrib.NParams > 2 Then
    '        txtSuffix.Text = txtSuffix.Text & ", " & txtParamCSymbol.Text.Trim & "=" & txtParamCValue.Text.Trim
    '        If Distribution.Distrib.NParams > 3 Then
    '            txtSuffix.Text = txtSuffix.Text & ", " & txtParamDSymbol.Text.Trim & "=" & txtParamDValue.Text.Trim
    '            If Distribution.Distrib.NParams > 4 Then
    '                txtSuffix.Text = txtSuffix.Text & ", " & txtParamESymbol.Text.Trim & "=" & txtParamEValue.Text.Trim
    '            End If
    '        End If
    '    End If
    'End If

    'If PrimaryDistribSelected Then 'The Primary Distribution is selected
    '    'Update the Primary Distribution suffix and series labels.
    '    Main.UpdateSuffix()
    'Else 'One of the Secondary Distributions is selected
    '    'Update the selected Secondary Distribution suffix and series labels.

    'End If


    'End Sub

    'Public Sub LoadSecondaryParamInfo(DistribNo As Integer)
    '    'Load the parameter information from a Secondary distribution.

    '    If DistribNo < 1 Then
    '        Main.Message.AddWarning("The Secondary distribution number is not valid: " & DistribNo & vbCrLf)
    '    ElseIf DistribNo > NDistribs Then
    '        Main.Message.AddWarning("The Secondary distribution number is larger than the number of seconday distributions: " & DistribNo & vbCrLf)
    '    Else 'Valid secondary distribution number selected.

    '    End If

    '    'NParams = Distribution.MultiDistrib(DistribNo).NParams
    '    NParams = Distribution.MultiDistrib(DistribNo - 1).NParams
    '    ParamAName = Distribution.MultiDistrib(DistribNo - 1).ParamA.Name
    '    txtParamASymbol.Text = NameToSymbol(Distribution.MultiDistrib(DistribNo - 1).ParamA.Symbol)
    '    ParamAOrigVal = Distribution.MultiDistrib(DistribNo - 1).ParamA.Value
    '    ParamAVal = Distribution.MultiDistrib(DistribNo - 1).ParamA.Value

    '    Dim SampInt As Double
    '    If Distribution.MultiDistrib(DistribNo - 1).Continuity = "Continuous" Then
    '        SampInt = Distribution.ContSampling.Interval
    '    Else
    '        SampInt = 1
    '    End If
    '    If Distribution.MultiDistrib(DistribNo - 1).ParamA.Increment = 0 Then Distribution.MultiDistrib(DistribNo - 1).ParamA.Increment = SampInt / 5
    '    If Distribution.MultiDistrib(DistribNo - 1).ParamB.Increment = 0 Then Distribution.MultiDistrib(DistribNo - 1).ParamB.Increment = SampInt / 5
    '    If Distribution.MultiDistrib(DistribNo - 1).ParamC.Increment = 0 Then Distribution.MultiDistrib(DistribNo - 1).ParamC.Increment = SampInt / 5
    '    If Distribution.MultiDistrib(DistribNo - 1).ParamD.Increment = 0 Then Distribution.MultiDistrib(DistribNo - 1).ParamD.Increment = SampInt / 5
    '    If Distribution.MultiDistrib(DistribNo - 1).ParamE.Increment = 0 Then Distribution.MultiDistrib(DistribNo - 1).ParamE.Increment = SampInt / 5

    '    ParamAIncr = Distribution.MultiDistrib(DistribNo - 1).ParamA.Increment

    '    If Distribution.MultiDistrib(DistribNo - 1).NParams > 1 Then
    '        ParamBName = Distribution.MultiDistrib(DistribNo - 1).ParamB.Name
    '        txtParamBSymbol.Text = NameToSymbol(Distribution.MultiDistrib(DistribNo - 1).ParamB.Symbol)
    '        ParamBOrigVal = Distribution.MultiDistrib(DistribNo - 1).ParamB.Value
    '        ParamBVal = Distribution.MultiDistrib(DistribNo - 1).ParamB.Value
    '        ParamBIncr = Distribution.MultiDistrib(DistribNo - 1).ParamB.Increment
    '        If Distribution.MultiDistrib(DistribNo - 1).NParams > 2 Then
    '            ParamCName = Distribution.MultiDistrib(DistribNo - 1).ParamC.Name
    '            txtParamCSymbol.Text = NameToSymbol(Distribution.MultiDistrib(DistribNo - 1).ParamC.Symbol)
    '            ParamCOrigVal = Distribution.MultiDistrib(DistribNo - 1).ParamC.Value
    '            ParamCVal = Distribution.MultiDistrib(DistribNo - 1).ParamC.Value
    '            ParamCIncr = Distribution.MultiDistrib(DistribNo - 1).ParamC.Increment
    '            If Distribution.MultiDistrib(DistribNo - 1).NParams > 3 Then
    '                ParamDName = Distribution.MultiDistrib(DistribNo - 1).ParamD.Name
    '                txtParamDSymbol.Text = NameToSymbol(Distribution.MultiDistrib(DistribNo - 1).ParamD.Symbol)
    '                ParamDOrigVal = Distribution.MultiDistrib(DistribNo - 1).ParamD.Value
    '                ParamDVal = Distribution.MultiDistrib(DistribNo - 1).ParamD.Value
    '                ParamDIncr = Distribution.MultiDistrib(DistribNo - 1).ParamD.Increment
    '                If Distribution.MultiDistrib(DistribNo - 1).NParams > 4 Then
    '                    ParamEName = Distribution.MultiDistrib(DistribNo - 1).ParamE.Name
    '                    txtParamESymbol.Text = NameToSymbol(Distribution.MultiDistrib(DistribNo - 1).ParamE.Symbol)
    '                    ParamEOrigVal = Distribution.MultiDistrib(DistribNo - 1).ParamE.Value
    '                    ParamEVal = Distribution.MultiDistrib(DistribNo - 1).ParamE.Value
    '                    ParamEIncr = Distribution.MultiDistrib(DistribNo - 1).ParamE.Increment
    '                End If
    '            End If
    '        End If
    '    End If

    '    txtSuffix.Text = Distribution.MultiDistrib(DistribNo - 1).Suffix

    'End Sub

    Private Sub btnPrevDistrib_Click(sender As Object, e As EventArgs) Handles btnPrevDistrib.Click
        If NDistribs = 0 Then

        Else
            If SelDistrib > 1 Then
                SelDistrib -= 1
                SelectedDistrib = Main.Distribution.Info(SelDistrib - 1)
                LoadParamInfo()
            End If
        End If

    End Sub

    Private Sub btnNextDistrib_Click(sender As Object, e As EventArgs) Handles btnNextDistrib.Click
        If NDistribs = 0 Then

        Else
            If SelDistrib < NDistribs Then
                SelDistrib += 1
                SelectedDistrib = Main.Distribution.Info(SelDistrib - 1)
                LoadParamInfo()
            End If
        End If

    End Sub


    Private Sub txtDecRound_TextChanged(sender As Object, e As EventArgs) Handles txtDecRound.TextChanged

    End Sub

    Private Sub txtDecRound_LostFocus(sender As Object, e As EventArgs) Handles txtDecRound.LostFocus
        Try
            DecRounding = txtDecRound.Text
            'ABInfo.DecRounding = txtDecRound.Text
        Catch ex As Exception
            Main.Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub

    Private Function DecRound(InputVal As Double) As Double
        'Round the InputVal to the DecRounding number of decimal places.

        Dim DecVal As Decimal = InputVal
        Return Math.Round(DecVal, DecRounding)


        'Dim DecFactor As Integer = 10 ^ DecRounding
        'Return Int(InputVal * DecFactor) / DecFactor
    End Function

    Private Sub chkRound_CheckedChanged(sender As Object, e As EventArgs) Handles chkRound.CheckedChanged
        If chkRound.Checked Then
            Round = True
            'ABInfo.Round = True
        Else
            Round = False
            'ABInfo.Round = False
        End If
    End Sub

    Private Sub pbAB_Click(sender As Object, e As EventArgs) Handles pbAB.Click

    End Sub

    Private Sub pbAB_MouseMove(sender As Object, e As MouseEventArgs) Handles pbAB.MouseMove
        If MouseABDown Then
            MouseABPosNow.X = e.X
            MouseABPosNow.Y = e.Y
            If MouseABPosNow.X < 0 Then MouseABPosNow.X = 0
            If MouseABPosNow.X > pbAB.Width Then MouseABPosNow.X = pbAB.Width
            If MouseABPosNow.Y < 0 Then MouseABPosNow.Y = 0
            If MouseABPosNow.Y > pbAB.Height Then MouseABPosNow.Y = pbAB.Height

            'ABInfo.XPos = e.X
            ABInfo.XPos = MouseABPosNow.X
            'ABInfo.YPos = e.Y
            ABInfo.YPos = MouseABPosNow.Y
            'txtX1.Text = ABInfo.XParamVal 'Update the X parameter value
            'txtY1.Text = ABInfo.YParamVal 'Update the Y parameter value

            Select Case cmbX1.SelectedIndex
                Case 0  'No parameter selected.

                Case 1 'Parameter A selected
                    ParamAVal = ABInfo.XParamVal
                    'Main.Distribution.Info(SelDistrib - 1).ParamA.Value = ABInfo.XParamVal
                    'Main.Distribution.Info(SelDistrib - 1).ParamA.Value = ParamAVal
                    SelectedDistrib.ParamA.Value = ParamAVal
                    txtX1.Text = ParamAVal 'Update the X parameter value
                    txtA.Text = ParamAVal

                    'AInfo.XParamVal = ParamAVal
                    'MouseAPosNow.X = AInfo.XPos
                    'pbA.Invalidate() 'Redraw pbA

                    hsA.Value = 1000 * (ParamAVal - ParamAAdjustMin) / (ParamAAdjustMax - ParamAAdjustMin)

                Case 2 'Parameter B selected
                    ParamBVal = ABInfo.XParamVal
                    SelectedDistrib.ParamB.Value = ParamBVal
                    txtX1.Text = ParamBVal 'Update the X parameter value
                    txtB.Text = ParamBVal
                    'BInfo.XParamVal = ParamBVal
                    'MouseBPosNow.X = BInfo.XPos
                    'pbB.Invalidate() 'Redraw pbB
                    hsB.Value = 1000 * (ParamBVal - ParamBAdjustMin) / (ParamBAdjustMax - ParamBAdjustMin)

                Case 3 'Parameter C selected
                    ParamCVal = ABInfo.XParamVal
                    SelectedDistrib.ParamC.Value = ParamCVal
                    txtX1.Text = ParamCVal 'Update the X parameter value
                    txtC.Text = ParamCVal
                    'CInfo.XParamVal = ParamCVal
                    'MouseCPosNow.X = CInfo.XPos
                    'pbC.Invalidate() 'Redraw pbC
                    hsC.Value = 1000 * (ParamCVal - ParamCAdjustMin) / (ParamCAdjustMax - ParamCAdjustMin)

                Case 4 'Parameter D selected
                    ParamDVal = ABInfo.XParamVal
                    SelectedDistrib.ParamD.Value = ParamDVal
                    txtX1.Text = ParamDVal 'Update the X parameter value
                    txtD.Text = ParamDVal
                    'DInfo.XParamVal = ParamDVal
                    'MouseDPosNow.X = DInfo.XPos
                    'pbD.Invalidate() 'Redraw pbD
                    hsD.Value = 1000 * (ParamDVal - ParamDAdjustMin) / (ParamDAdjustMax - ParamDAdjustMin)

                Case 5 'Parameter E selected
                    ParamEVal = ABInfo.XParamVal
                    SelectedDistrib.ParamE.Value = ParamEVal
                    txtX1.Text = ParamEVal 'Update the X parameter value
                    txtE.Text = ParamEVal
                    'EInfo.XParamVal = ParamEVal
                    'MouseEPosNow.X = EInfo.XPos
                    'pbE.Invalidate() 'Redraw pbE
                    hsE.Value = 1000 * (ParamEVal - ParamEAdjustMin) / (ParamEAdjustMax - ParamEAdjustMin)

            End Select
            Select Case cmbY1.SelectedIndex
                Case 0 'No parameter selected.

                Case 1 'Parameter A selected
                    ParamAVal = ABInfo.YParamVal
                    'Main.Distribution.Info(SelDistrib - 1).ParamA.Value = ABInfo.YParamVal
                    SelectedDistrib.ParamA.Value = ParamAVal
                    txtY1.Text = ParamAVal 'Update the Y parameter value
                    txtA.Text = ParamAVal

                    'AInfo.XParamVal = ParamAVal
                    'MouseAPosNow.X = AInfo.XPos
                    'pbA.Invalidate() 'Redraw pbA
                    hsA.Value = 1000 * (ParamAVal - ParamAAdjustMin) / (ParamAAdjustMax - ParamAAdjustMin)

                Case 2 'Parameter B selected
                    ParamBVal = ABInfo.YParamVal
                    SelectedDistrib.ParamB.Value = ParamBVal
                    txtY1.Text = ParamBVal 'Update the Y parameter value
                    txtB.Text = ParamBVal
                    'BInfo.XParamVal = ParamBVal
                    'MouseBPosNow.X = BInfo.XPos
                    'pbB.Invalidate() 'Redraw pbB
                    hsB.Value = 1000 * (ParamBVal - ParamBAdjustMin) / (ParamBAdjustMax - ParamBAdjustMin)

                Case 3 'Parameter C selected
                    ParamCVal = ABInfo.YParamVal
                    SelectedDistrib.ParamC.Value = ParamCVal
                    txtY1.Text = ParamCVal 'Update the Y parameter value
                    txtC.Text = ParamCVal
                    'CInfo.XParamVal = ParamCVal
                    'MouseCPosNow.X = CInfo.XPos
                    'pbC.Invalidate() 'Redraw pbC
                    hsC.Value = 1000 * (ParamCVal - ParamCAdjustMin) / (ParamCAdjustMax - ParamCAdjustMin)

                Case 4 'Parameter D selected
                    ParamDVal = ABInfo.YParamVal
                    SelectedDistrib.ParamD.Value = ParamDVal
                    txtY1.Text = ParamDVal 'Update the Y parameter value
                    txtD.Text = ParamDVal
                    'DInfo.XParamVal = ParamDVal
                    'MouseDPosNow.X = DInfo.XPos
                    'pbD.Invalidate() 'Redraw pbD
                    hsD.Value = 1000 * (ParamDVal - ParamDAdjustMin) / (ParamDAdjustMax - ParamDAdjustMin)

                Case 5 'Parameter E selected
                    ParamEVal = ABInfo.YParamVal
                    SelectedDistrib.ParamE.Value = ParamEVal
                    txtY1.Text = ParamEVal 'Update the Y parameter value
                    txtE.Text = ParamEVal
                    'EInfo.XParamVal = ParamEVal
                    'MouseEPosNow.X = EInfo.XPos
                    'pbE.Invalidate() 'Redraw pbE
                    hsE.Value = 1000 * (ParamEVal - ParamEAdjustMin) / (ParamEAdjustMax - ParamEAdjustMin)

            End Select
            UpdateData()
            Main.UpdateAnnotationSettings(SelDistrib)
            Main.ReplotCharts()

            pbAB.Invalidate() 'Redraw pbAB

            Timer1.Start()
        End If
    End Sub

    Private Sub pbAB_MouseDown(sender As Object, e As MouseEventArgs) Handles pbAB.MouseDown
        InUse = True
        MouseABDown = True
        MouseABPosNow.X = e.X
        MouseABPosNow.Y = e.Y


        If MouseABPosNow.X < 0 Then MouseABPosNow.X = 0
        If MouseABPosNow.X > pbAB.Width Then MouseABPosNow.X = pbAB.Width
        If MouseABPosNow.Y < 0 Then MouseABPosNow.Y = 0
        If MouseABPosNow.Y > pbAB.Height Then MouseABPosNow.Y = pbAB.Height

        'ABInfo.XPos = e.X
        ABInfo.XPos = MouseABPosNow.X
        'ABInfo.YPos = e.Y
        ABInfo.YPos = MouseABPosNow.Y
        'txtX1.Text = ABInfo.XParamVal 'Update the X parameter value
        'txtY1.Text = ABInfo.YParamVal 'Update the Y parameter value

        Select Case cmbX1.SelectedIndex
            Case 0  'No parameter selected.

            Case 1 'Parameter A selected
                ParamAVal = ABInfo.XParamVal
                'Main.Distribution.Info(SelDistrib - 1).ParamA.Value = ABInfo.XParamVal
                SelectedDistrib.ParamA.Value = ParamAVal
                txtX1.Text = ParamAVal 'Update the X parameter value
                txtA.Text = ParamAVal
                'AInfo.XParamVal = ParamAVal
                'MouseAPosNow.X = AInfo.XPos
                'pbA.Invalidate() 'Redraw pbA
                hsA.Value = 1000 * (ParamAVal - ParamAAdjustMin) / (ParamAAdjustMax - ParamAAdjustMin)
            Case 2 'Parameter B selected
                ParamBVal = ABInfo.XParamVal
                SelectedDistrib.ParamB.Value = ParamBVal
                txtX1.Text = ParamBVal 'Update the X parameter value
                txtB.Text = ParamBVal
                'BInfo.XParamVal = ParamBVal
                'MouseBPosNow.X = BInfo.XPos
                'pbB.Invalidate() 'Redraw pbB
                hsB.Value = 1000 * (ParamBVal - ParamBAdjustMin) / (ParamBAdjustMax - ParamBAdjustMin)

            Case 3 'Parameter C selected
                ParamCVal = ABInfo.XParamVal
                SelectedDistrib.ParamC.Value = ParamCVal
                txtX1.Text = ParamCVal 'Update the X parameter value
                txtC.Text = ParamCVal
                'CInfo.XParamVal = ParamCVal
                'MouseCPosNow.X = CInfo.XPos
                'pbC.Invalidate() 'Redraw pbC
                hsC.Value = 1000 * (ParamCVal - ParamCAdjustMin) / (ParamCAdjustMax - ParamCAdjustMin)

            Case 4 'Parameter D selected
                ParamDVal = ABInfo.XParamVal
                SelectedDistrib.ParamD.Value = ParamDVal
                txtX1.Text = ParamDVal 'Update the X parameter value
                txtD.Text = ParamDVal
                'DInfo.XParamVal = ParamDVal
                'MouseDPosNow.X = DInfo.XPos
                'pbD.Invalidate() 'Redraw pbD
                hsD.Value = 1000 * (ParamDVal - ParamDAdjustMin) / (ParamDAdjustMax - ParamDAdjustMin)

            Case 5 'Parameter E selected
                ParamEVal = ABInfo.XParamVal
                SelectedDistrib.ParamE.Value = ParamEVal
                txtX1.Text = ParamEVal 'Update the X parameter value
                txtE.Text = ParamDVal
                'EInfo.XParamVal = ParamEVal
                'MouseEPosNow.X = EInfo.XPos
                'pbE.Invalidate() 'Redraw pbE
                hsE.Value = 1000 * (ParamEVal - ParamEAdjustMin) / (ParamEAdjustMax - ParamEAdjustMin)

        End Select
        Select Case cmbY1.SelectedIndex
            Case 0 'No parameter selected.

            Case 1 'Parameter A selected
                ParamAVal = ABInfo.YParamVal
                'Main.Distribution.Info(SelDistrib - 1).ParamA.Value = ABInfo.YParamVal
                SelectedDistrib.ParamA.Value = ParamAVal
                txtY1.Text = ParamAVal 'Update the Y parameter value
                txtA.Text = ParamAVal
                'AInfo.XParamVal = ParamAVal
                'MouseAPosNow.X = AInfo.XPos
                'pbA.Invalidate() 'Redraw pbA
                hsA.Value = 1000 * (ParamAVal - ParamAAdjustMin) / (ParamAAdjustMax - ParamAAdjustMin)
            Case 2 'Parameter B selected
                ParamBVal = ABInfo.YParamVal
                SelectedDistrib.ParamB.Value = ParamBVal
                txtY1.Text = ParamBVal 'Update the Y parameter value
                txtB.Text = ParamBVal
                'BInfo.XParamVal = ParamBVal
                'MouseBPosNow.X = BInfo.XPos
                'pbB.Invalidate() 'Redraw pbB
                hsB.Value = 1000 * (ParamBVal - ParamBAdjustMin) / (ParamBAdjustMax - ParamBAdjustMin)

            Case 3 'Parameter C selected
                ParamCVal = ABInfo.YParamVal
                SelectedDistrib.ParamC.Value = ParamCVal
                txtY1.Text = ParamCVal 'Update the Y parameter value
                txtC.Text = ParamCVal
                'CInfo.XParamVal = ParamCVal
                'MouseCPosNow.X = CInfo.XPos
                'pbC.Invalidate() 'Redraw pbC
                hsC.Value = 1000 * (ParamCVal - ParamCAdjustMin) / (ParamCAdjustMax - ParamCAdjustMin)

            Case 4 'Parameter D selected
                ParamDVal = ABInfo.YParamVal
                SelectedDistrib.ParamD.Value = ParamDVal
                txtY1.Text = ParamDVal 'Update the Y parameter value
                txtD.Text = ParamDVal
                'DInfo.XParamVal = ParamDVal
                'MouseDPosNow.X = DInfo.XPos
                'pbD.Invalidate() 'Redraw pbD
                hsD.Value = 1000 * (ParamDVal - ParamDAdjustMin) / (ParamDAdjustMax - ParamDAdjustMin)

            Case 5 'Parameter E selected
                ParamEVal = ABInfo.YParamVal
                SelectedDistrib.ParamE.Value = ParamEVal
                txtY1.Text = ParamEVal 'Update the Y parameter value
                txtE.Text = ParamEVal
                'EInfo.XParamVal = ParamEVal
                'MouseEPosNow.X = EInfo.XPos
                'pbE.Invalidate() 'Redraw pbE
                hsE.Value = 1000 * (ParamEVal - ParamEAdjustMin) / (ParamEAdjustMax - ParamEAdjustMin)

        End Select
        UpdateData()
        Main.UpdateAnnotationSettings(SelDistrib)
        Main.ReplotCharts()
        pbAB.Invalidate() 'Redraw pbAB
    End Sub

    Private Sub pbAB_MouseUp(sender As Object, e As MouseEventArgs) Handles pbAB.MouseUp
        MouseABDown = False
        pbAB.Invalidate()
        InUse = False
    End Sub

    Private Sub pbAB_MouseLeave(sender As Object, e As EventArgs) Handles pbAB.MouseLeave
        'Mouse1Down = False
        'InUse = False
    End Sub

    Private Sub pbAB_Paint(sender As Object, e As PaintEventArgs) Handles pbAB.Paint
        Dim myRectangle As New Rectangle
        Dim myPen As New Pen(Color.Black, 3)
        Dim Radius As Integer = 5
        myRectangle.X = MouseABPosNow.X - Radius
        myRectangle.Y = MouseABPosNow.Y - Radius
        myRectangle.Width = Radius * 2
        myRectangle.Height = Radius * 2
        e.Graphics.DrawEllipse(myPen, myRectangle)
    End Sub

    Private Sub pbCD_Click(sender As Object, e As EventArgs) Handles pbCD.Click

    End Sub

    Private Sub pbCD_MouseDown(sender As Object, e As MouseEventArgs) Handles pbCD.MouseDown
        InUse = True
        MouseCDDown = True
        MouseCDPosNow.X = e.X
        MouseCDPosNow.Y = e.Y
        pbCD.Invalidate() 'Redraw pbCD
    End Sub

    Private Sub pbCD_MouseMove(sender As Object, e As MouseEventArgs) Handles pbCD.MouseMove
        If MouseCDDown Then
            MouseCDPosNow.X = e.X
            MouseCDPosNow.Y = e.Y
            pbCD.Invalidate() 'Redraw pbCD
        End If
    End Sub

    Private Sub pbCD_MouseUp(sender As Object, e As MouseEventArgs) Handles pbCD.MouseUp
        MouseCDDown = False
        pbCD.Invalidate()
        InUse = False
    End Sub

    Private Sub pbCD_Paint(sender As Object, e As PaintEventArgs) Handles pbCD.Paint
        Dim myRectangle As New Rectangle
        Dim myPen As New Pen(Color.Black, 3)
        Dim Radius As Integer = 5
        myRectangle.X = MouseCDPosNow.X - Radius
        myRectangle.Y = MouseCDPosNow.Y - Radius
        myRectangle.Width = Radius * 2
        myRectangle.Height = Radius * 2
        e.Graphics.DrawEllipse(myPen, myRectangle)
    End Sub

    Private Sub pbA_Click(sender As Object, e As EventArgs)

    End Sub

    'Private Sub pbA_MouseDown(sender As Object, e As MouseEventArgs)
    '    MouseADown = True
    '    MouseAPosNow.X = e.X
    '    MouseAPosNow.Y = e.Y
    '    If MouseAPosNow.X < 0 Then MouseAPosNow.X = 0
    '    If MouseAPosNow.X > pbB.Width Then MouseAPosNow.X = pbA.Width

    '    pbA.Invalidate() 'Redraw pbA
    '    AInfo.XPos = e.X
    '    ParamAVal = AInfo.XParamVal
    '    SelectedDistrib.ParamA.Value = ParamAVal
    '    txtA.Text = ParamAVal
    '    If cmbX1.SelectedIndex = 1 Then
    '        ABInfo.XParamVal = ParamAVal
    '        txtX1.Text = ParamAVal
    '        MouseABPosNow.X = ABInfo.XPos
    '        pbAB.Invalidate() 'Redraw pbAB
    '    ElseIf cmbY1.SelectedIndex = 1 Then
    '        ABInfo.YParamVal = ParamAVal
    '        txtY1.Text = ParamAVal
    '        MouseABPosNow.Y = ABInfo.XPos
    '        pbAB.Invalidate() 'Redraw pbAB
    '    ElseIf cmbX2.SelectedIndex = 1 Then
    '        CDInfo.XParamVal = ParamAVal
    '        txtX2.Text = ParamAVal
    '        MouseCDPosNow.X = ABInfo.XPos
    '        pbCD.Invalidate() 'Redraw pbCD
    '    ElseIf cmbY2.SelectedIndex = 1 Then
    '        CDInfo.YParamVal = ParamAVal
    '        txtY2.Text = ParamAVal
    '        MouseCDPosNow.Y = ABInfo.XPos
    '        pbCD.Invalidate() 'Redraw pbCD
    '    End If
    '    UpdateData()
    '    Main.UpdateAnnotationSettings(SelDistrib)
    '    Main.ReplotCharts()
    'End Sub

    'Private Sub pbA_MouseMove(sender As Object, e As MouseEventArgs)
    '    If MouseADown Then
    '        MouseAPosNow.X = e.X
    '        MouseAPosNow.Y = e.Y
    '        If MouseAPosNow.X < 0 Then MouseAPosNow.X = 0
    '        If MouseAPosNow.X > pbB.Width Then MouseAPosNow.X = pbA.Width

    '        pbA.Invalidate() 'Redraw pbA
    '        'AInfo.XPos = e.X
    '        AInfo.XPos = MouseAPosNow.X
    '        ParamAVal = AInfo.XParamVal
    '        SelectedDistrib.ParamA.Value = ParamAVal
    '        txtA.Text = ParamAVal
    '        If cmbX1.SelectedIndex = 1 Then
    '            ABInfo.XParamVal = ParamAVal
    '            txtX1.Text = ParamAVal
    '            MouseABPosNow.X = ABInfo.XPos
    '            pbAB.Invalidate() 'Redraw pbAB
    '        ElseIf cmbY1.SelectedIndex = 1 Then
    '            ABInfo.YParamVal = ParamAVal
    '            txtY1.Text = ParamAVal
    '            MouseABPosNow.Y = ABInfo.YPos
    '            pbAB.Invalidate() 'Redraw pbAB
    '        ElseIf cmbX2.SelectedIndex = 1 Then
    '            CDInfo.XParamVal = ParamAVal
    '            txtX2.Text = ParamAVal
    '            MouseCDPosNow.X = ABInfo.XPos
    '            pbCD.Invalidate() 'Redraw pbCD
    '        ElseIf cmbY2.SelectedIndex = 1 Then
    '            CDInfo.YParamVal = ParamAVal
    '            txtY2.Text = ParamAVal
    '            MouseCDPosNow.Y = ABInfo.YPos
    '            pbCD.Invalidate() 'Redraw pbCD
    '        End If
    '        UpdateData()
    '        Main.UpdateAnnotationSettings(SelDistrib)
    '        Main.ReplotCharts()
    '    End If
    'End Sub

    Private Sub hsA_Scroll(sender As Object, e As ScrollEventArgs) Handles hsA.Scroll
        ParamAVal = ParamAAdjustMin + (ParamAAdjustMax - ParamAAdjustMin) * hsA.Value / 1000
        SelectedDistrib.ParamA.Value = ParamAVal
        txtA.Text = ParamAVal

        'AInfo.XParamVal = ParamAVal
        'MouseAPosNow.X = AInfo.XPos
        'pbA.Invalidate() 'Redraw pbA

        If cmbX1.SelectedIndex = 1 Then
            ABInfo.XParamVal = ParamAVal
            txtX1.Text = ParamAVal
            MouseABPosNow.X = ABInfo.XPos
            pbAB.Invalidate() 'Redraw pbAB
        ElseIf cmbY1.SelectedIndex = 1 Then
            ABInfo.YParamVal = ParamAVal
            txtY1.Text = ParamAVal
            MouseABPosNow.Y = ABInfo.YPos
            pbAB.Invalidate() 'Redraw pbAB
        ElseIf cmbX2.SelectedIndex = 1 Then
            CDInfo.XParamVal = ParamAVal
            txtX2.Text = ParamAVal
            MouseCDPosNow.X = ABInfo.XPos
            pbCD.Invalidate() 'Redraw pbCD
        ElseIf cmbY2.SelectedIndex = 1 Then
            CDInfo.YParamVal = ParamAVal
            txtY2.Text = ParamAVal
            MouseCDPosNow.Y = ABInfo.YPos
            pbCD.Invalidate() 'Redraw pbCD
        End If

        'Main.UpdateParameters() 'Update the displayed parameters on the Main form - TOO SLOW
        UpdateData()
        Main.UpdateAnnotationSettings(SelDistrib)
        Main.ReplotCharts()
        Timer1.Start()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        '500ms after the last Scroll event, the distribution parameter display will be updated.
        Main.UpdateParameters()
        Timer1.Stop()
    End Sub



    Private Sub hsB_Scroll(sender As Object, e As ScrollEventArgs) Handles hsB.Scroll
        ParamBVal = ParamBAdjustMin + (ParamBAdjustMax - ParamBAdjustMin) * hsB.Value / 1000
        SelectedDistrib.ParamB.Value = ParamBVal
        txtB.Text = ParamBVal
        If cmbX1.SelectedIndex = 2 Then
            ABInfo.XParamVal = ParamBVal
            txtX1.Text = ParamBVal
            MouseABPosNow.X = ABInfo.XPos
            pbAB.Invalidate() 'Redraw pbAB
        ElseIf cmbY1.SelectedIndex = 2 Then
            ABInfo.YParamVal = ParamBVal
            txtY1.Text = ParamBVal
            MouseABPosNow.Y = ABInfo.YPos
            pbAB.Invalidate() 'Redraw pbAB
        ElseIf cmbX2.SelectedIndex = 2 Then
            CDInfo.XParamVal = ParamBVal
            txtX2.Text = ParamBVal
            MouseCDPosNow.X = ABInfo.XPos
            pbCD.Invalidate() 'Redraw pbCD
        ElseIf cmbY2.SelectedIndex = 2 Then
            CDInfo.YParamVal = ParamBVal
            txtY2.Text = ParamBVal
            MouseCDPosNow.Y = ABInfo.YPos
            pbCD.Invalidate() 'Redraw pbCD
        End If
        UpdateData()
        Main.UpdateAnnotationSettings(SelDistrib)
        Main.ReplotCharts()
        Timer1.Start()
    End Sub

    Private Sub hsC_Scroll(sender As Object, e As ScrollEventArgs) Handles hsC.Scroll

        ParamCVal = ParamCAdjustMin + (ParamCAdjustMax - ParamCAdjustMin) * hsC.Value / 1000
        SelectedDistrib.ParamC.Value = ParamCVal
        txtC.Text = ParamCVal
        If cmbX1.SelectedIndex = 3 Then
            ABInfo.XParamVal = ParamCVal
            txtX1.Text = ParamCVal
            MouseABPosNow.X = ABInfo.XPos
            pbAB.Invalidate() 'Redraw pbAB
        ElseIf cmbY1.SelectedIndex = 3 Then
            ABInfo.YParamVal = ParamCVal
            txtY1.Text = ParamCVal
            MouseABPosNow.Y = ABInfo.YPos
            pbAB.Invalidate() 'Redraw pbAB
        ElseIf cmbX2.SelectedIndex = 3 Then
            CDInfo.XParamVal = ParamCVal
            txtX2.Text = ParamCVal
            MouseCDPosNow.X = ABInfo.XPos
            pbCD.Invalidate() 'Redraw pbCD
        ElseIf cmbY2.SelectedIndex = 3 Then
            CDInfo.YParamVal = ParamCVal
            txtY2.Text = ParamCVal
            MouseCDPosNow.Y = ABInfo.YPos
            pbCD.Invalidate() 'Redraw pbCD
        End If
        UpdateData()
        Main.UpdateAnnotationSettings(SelDistrib)
        Main.ReplotCharts()
        Timer1.Start()
    End Sub

    Private Sub hsD_Scroll(sender As Object, e As ScrollEventArgs) Handles hsD.Scroll


        ParamDVal = ParamDAdjustMin + (ParamDAdjustMax - ParamDAdjustMin) * hsD.Value / 1000
        SelectedDistrib.ParamD.Value = ParamDVal
        txtD.Text = ParamDVal
        If cmbX1.SelectedIndex = 4 Then
            ABInfo.XParamVal = ParamDVal
            txtX1.Text = ParamDVal
            MouseABPosNow.X = ABInfo.XPos
            pbAB.Invalidate() 'Redraw pbAB
        ElseIf cmbY1.SelectedIndex = 4 Then
            ABInfo.YParamVal = ParamDVal
            txtY1.Text = ParamDVal
            MouseABPosNow.Y = ABInfo.YPos
            pbAB.Invalidate() 'Redraw pbAB
        ElseIf cmbX2.SelectedIndex = 4 Then
            CDInfo.XParamVal = ParamDVal
            txtX2.Text = ParamDVal
            MouseCDPosNow.X = ABInfo.XPos
            pbCD.Invalidate() 'Redraw pbCD
        ElseIf cmbY2.SelectedIndex = 4 Then
            CDInfo.YParamVal = ParamDVal
            txtY2.Text = ParamDVal
            MouseCDPosNow.Y = ABInfo.YPos
            pbCD.Invalidate() 'Redraw pbCD
        End If
        UpdateData()
        Main.UpdateAnnotationSettings(SelDistrib)
        Main.ReplotCharts()
        Timer1.Start()
    End Sub

    Private Sub hsE_Scroll(sender As Object, e As ScrollEventArgs) Handles hsE.Scroll

        ParamEVal = ParamEAdjustMin + (ParamEAdjustMax - ParamEAdjustMin) * hsE.Value / 1000
        SelectedDistrib.ParamE.Value = ParamEVal
        txtE.Text = ParamEVal
        If cmbX1.SelectedIndex = 5 Then
            ABInfo.XParamVal = ParamEVal
            txtX1.Text = ParamEVal
            MouseABPosNow.X = ABInfo.XPos
            pbAB.Invalidate() 'Redraw pbAB
        ElseIf cmbY1.SelectedIndex = 5 Then
            ABInfo.YParamVal = ParamEVal
            txtY1.Text = ParamEVal
            MouseABPosNow.Y = ABInfo.YPos
            pbAB.Invalidate() 'Redraw pbAB
        ElseIf cmbX2.SelectedIndex = 5 Then
            CDInfo.XParamVal = ParamEVal
            txtX2.Text = ParamEVal
            MouseCDPosNow.X = ABInfo.XPos
            pbCD.Invalidate() 'Redraw pbCD
        ElseIf cmbY2.SelectedIndex = 5 Then
            CDInfo.YParamVal = ParamEVal
            txtY2.Text = ParamEVal
            MouseCDPosNow.Y = ABInfo.YPos
            pbCD.Invalidate() 'Redraw pbCD
        End If
        UpdateData()
        Main.UpdateAnnotationSettings(SelDistrib)
        Main.ReplotCharts()
        Timer1.Start()
    End Sub

    'Private Sub pbA_MouseUp(sender As Object, e As MouseEventArgs)
    '    MouseADown = False
    '    pbA.Invalidate()
    'End Sub

    'Private Sub pbA_Paint(sender As Object, e As PaintEventArgs)
    '    Dim myRectangle As New Rectangle
    '    Dim myPen As New Pen(Color.Black, 3)
    '    Dim Radius As Integer = 5
    '    myRectangle.X = MouseAPosNow.X - Radius
    '    myRectangle.Y = 0
    '    myRectangle.Width = Radius * 2
    '    myRectangle.Height = pbA.Height - 6
    '    e.Graphics.DrawRectangle(myPen, myRectangle)
    'End Sub

    Private Sub pbB_Click(sender As Object, e As EventArgs)

    End Sub

    'Private Sub pbB_MouseDown(sender As Object, e As MouseEventArgs)
    '    MouseBDown = True
    '    MouseBPosNow.X = e.X
    '    MouseBPosNow.Y = e.Y
    '    If MouseBPosNow.X < 0 Then MouseBPosNow.X = 0
    '    If MouseBPosNow.X > pbB.Width Then MouseBPosNow.X = pbB.Width

    '    pbB.Invalidate() 'Redraw pbB

    '    BInfo.XPos = MouseBPosNow.X
    '    ParamBVal = BInfo.XParamVal
    '    SelectedDistrib.ParamB.Value = ParamBVal
    '    txtB.Text = ParamBVal
    '    If cmbX1.SelectedIndex = 2 Then
    '        ABInfo.XParamVal = ParamBVal
    '        txtX1.Text = ParamBVal
    '        MouseABPosNow.X = ABInfo.XPos
    '        pbAB.Invalidate() 'Redraw pbAB
    '    ElseIf cmbY1.SelectedIndex = 2 Then
    '        ABInfo.YParamVal = ParamBVal
    '        txtY1.Text = ParamBVal
    '        MouseABPosNow.Y = ABInfo.YPos
    '        pbAB.Invalidate() 'Redraw pbAB
    '    ElseIf cmbX2.SelectedIndex = 2 Then
    '        CDInfo.XParamVal = ParamBVal
    '        txtX2.Text = ParamBVal
    '        MouseCDPosNow.X = ABInfo.XPos
    '        pbCD.Invalidate() 'Redraw pbCD
    '    ElseIf cmbY2.SelectedIndex = 2 Then
    '        CDInfo.YParamVal = ParamBVal
    '        txtY2.Text = ParamBVal
    '        MouseCDPosNow.Y = ABInfo.YPos
    '        pbCD.Invalidate() 'Redraw pbCD
    '    End If
    '    UpdateData()
    '    Main.UpdateAnnotationSettings(SelDistrib)
    '    Main.ReplotCharts()

    'End Sub

    'Private Sub pbB_MouseMove(sender As Object, e As MouseEventArgs)
    '    If MouseBDown Then
    '        MouseBPosNow.X = e.X
    '        MouseBPosNow.Y = e.Y
    '        If MouseBPosNow.X < 0 Then MouseBPosNow.X = 0
    '        If MouseBPosNow.X > pbB.Width Then MouseBPosNow.X = pbB.Width

    '        pbB.Invalidate() 'Redraw pbB
    '        'BInfo.XPos = e.X
    '        BInfo.XPos = MouseBPosNow.X
    '        ParamBVal = BInfo.XParamVal
    '        SelectedDistrib.ParamB.Value = ParamBVal
    '        txtB.Text = ParamBVal
    '        If cmbX1.SelectedIndex = 2 Then
    '            ABInfo.XParamVal = ParamBVal
    '            txtX1.Text = ParamBVal
    '            MouseABPosNow.X = ABInfo.XPos
    '            pbAB.Invalidate() 'Redraw pbAB
    '        ElseIf cmbY1.SelectedIndex = 2 Then
    '            ABInfo.YParamVal = ParamBVal
    '            txtY1.Text = ParamBVal
    '            MouseABPosNow.Y = ABInfo.YPos
    '            pbAB.Invalidate() 'Redraw pbAB
    '        ElseIf cmbX2.SelectedIndex = 2 Then
    '            CDInfo.XParamVal = ParamBVal
    '            txtX2.Text = ParamBVal
    '            MouseCDPosNow.X = ABInfo.XPos
    '            pbCD.Invalidate() 'Redraw pbCD
    '        ElseIf cmbY2.SelectedIndex = 2 Then
    '            CDInfo.YParamVal = ParamBVal
    '            txtY2.Text = ParamBVal
    '            MouseCDPosNow.Y = ABInfo.YPos
    '            pbCD.Invalidate() 'Redraw pbCD
    '        End If
    '        UpdateData()
    '        Main.UpdateAnnotationSettings(SelDistrib)
    '        Main.ReplotCharts()
    '    End If
    'End Sub

    'Private Sub pbB_MouseUp(sender As Object, e As MouseEventArgs)
    '    MouseBDown = False
    '    pbB.Invalidate()
    'End Sub

    'Private Sub pbB_Paint(sender As Object, e As PaintEventArgs)
    '    Dim myRectangle As New Rectangle
    '    Dim myPen As New Pen(Color.Black, 3)
    '    Dim Radius As Integer = 5
    '    myRectangle.X = MouseBPosNow.X - Radius
    '    myRectangle.Y = 0
    '    myRectangle.Width = Radius * 2
    '    myRectangle.Height = pbB.Height - 6
    '    e.Graphics.DrawRectangle(myPen, myRectangle)
    'End Sub

    'Private Sub pbC_Click(sender As Object, e As EventArgs)

    'End Sub

    'Private Sub pbC_MouseDown(sender As Object, e As MouseEventArgs)
    '    MouseCDown = True
    '    MouseCPosNow.X = e.X
    '    MouseCPosNow.Y = e.Y
    '    If MouseCPosNow.X < 0 Then MouseCPosNow.X = 0
    '    If MouseCPosNow.X > pbC.Width Then MouseCPosNow.X = pbC.Width

    '    pbC.Invalidate() 'Redraw pbC

    '    CInfo.XPos = e.X
    '    ParamCVal = CInfo.XParamVal
    '    SelectedDistrib.ParamC.Value = ParamCVal
    '    txtC.Text = ParamCVal
    '    If cmbX1.SelectedIndex = 3 Then
    '        ABInfo.XParamVal = ParamCVal
    '        txtX1.Text = ParamCVal
    '        MouseABPosNow.X = ABInfo.XPos
    '        pbAB.Invalidate() 'Redraw pbAB
    '    ElseIf cmbY1.SelectedIndex = 3 Then
    '        ABInfo.YParamVal = ParamCVal
    '        txtY1.Text = ParamCVal
    '        MouseABPosNow.Y = ABInfo.XPos
    '        pbAB.Invalidate() 'Redraw pbAB
    '    ElseIf cmbX2.SelectedIndex = 3 Then
    '        CDInfo.XParamVal = ParamCVal
    '        txtX2.Text = ParamCVal
    '        MouseCDPosNow.X = ABInfo.XPos
    '        pbCD.Invalidate() 'Redraw pbCD
    '    ElseIf cmbY2.SelectedIndex = 3 Then
    '        CDInfo.YParamVal = ParamCVal
    '        txtY2.Text = ParamCVal
    '        MouseCDPosNow.Y = ABInfo.XPos
    '        pbCD.Invalidate() 'Redraw pbCD
    '    End If
    '    UpdateData()
    '    Main.UpdateAnnotationSettings(SelDistrib)
    '    Main.ReplotCharts()

    'End Sub

    'Private Sub pbC_MouseMove(sender As Object, e As MouseEventArgs)
    '    If MouseCDown Then
    '        MouseCPosNow.X = e.X
    '        MouseCPosNow.Y = e.Y
    '        If MouseCPosNow.X < 0 Then MouseCPosNow.X = 0
    '        If MouseCPosNow.X > pbC.Width Then MouseCPosNow.X = pbC.Width

    '        pbC.Invalidate() 'Redraw pbC
    '        CInfo.XPos = MouseCPosNow.X
    '        ParamCVal = CInfo.XParamVal
    '        SelectedDistrib.ParamC.Value = ParamCVal
    '        txtC.Text = ParamCVal
    '        If cmbX1.SelectedIndex = 3 Then
    '            ABInfo.XParamVal = ParamCVal
    '            txtX1.Text = ParamCVal
    '            MouseABPosNow.X = ABInfo.XPos
    '            pbAB.Invalidate() 'Redraw pbAB
    '        ElseIf cmbY1.SelectedIndex = 3 Then
    '            ABInfo.YParamVal = ParamCVal
    '            txtY1.Text = ParamCVal
    '            MouseABPosNow.Y = ABInfo.YPos
    '            pbAB.Invalidate() 'Redraw pbAB
    '        ElseIf cmbX2.SelectedIndex = 3 Then
    '            CDInfo.XParamVal = ParamCVal
    '            txtX2.Text = ParamCVal
    '            MouseCDPosNow.X = ABInfo.XPos
    '            pbCD.Invalidate() 'Redraw pbCD
    '        ElseIf cmbY2.SelectedIndex = 3 Then
    '            CDInfo.YParamVal = ParamCVal
    '            txtY2.Text = ParamCVal
    '            MouseCDPosNow.Y = ABInfo.YPos
    '            pbCD.Invalidate() 'Redraw pbCD
    '        End If
    '        UpdateData()
    '        Main.UpdateAnnotationSettings(SelDistrib)
    '        Main.ReplotCharts()
    '    End If
    'End Sub

    'Private Sub pbC_MouseUp(sender As Object, e As MouseEventArgs)
    '    MouseCDown = False
    '    pbC.Invalidate()
    'End Sub

    'Private Sub pbC_Paint(sender As Object, e As PaintEventArgs)
    '    Dim myRectangle As New Rectangle
    '    Dim myPen As New Pen(Color.Black, 3)
    '    Dim Radius As Integer = 5
    '    myRectangle.X = MouseCPosNow.X - Radius
    '    myRectangle.Y = 0
    '    myRectangle.Width = Radius * 2
    '    myRectangle.Height = pbC.Height - 6
    '    e.Graphics.DrawRectangle(myPen, myRectangle)
    'End Sub

    'Private Sub pbD_Click(sender As Object, e As EventArgs)

    'End Sub

    'Private Sub pbD_MouseDown(sender As Object, e As MouseEventArgs)
    '    MouseDDown = True
    '    MouseDPosNow.X = e.X
    '    MouseDPosNow.Y = e.Y
    '    If MouseDPosNow.X < 0 Then MouseDPosNow.X = 0
    '    If MouseDPosNow.X > pbD.Width Then MouseDPosNow.X = pbD.Width

    '    pbD.Invalidate() 'Redraw pbD

    '    DInfo.XPos = e.X
    '    ParamDVal = DInfo.XParamVal
    '    SelectedDistrib.ParamA.Value = ParamDVal
    '    txtD.Text = ParamDVal
    '    If cmbX1.SelectedIndex = 4 Then
    '        ABInfo.XParamVal = ParamDVal
    '        txtX1.Text = ParamDVal
    '        MouseABPosNow.X = ABInfo.XPos
    '        pbAB.Invalidate() 'Redraw pbAB
    '    ElseIf cmbY1.SelectedIndex = 4 Then
    '        ABInfo.YParamVal = ParamDVal
    '        txtY1.Text = ParamDVal
    '        MouseABPosNow.Y = ABInfo.XPos
    '        pbAB.Invalidate() 'Redraw pbAB
    '    ElseIf cmbX2.SelectedIndex = 4 Then
    '        CDInfo.XParamVal = ParamDVal
    '        txtX2.Text = ParamDVal
    '        MouseCDPosNow.X = ABInfo.XPos
    '        pbCD.Invalidate() 'Redraw pbCD
    '    ElseIf cmbY2.SelectedIndex = 4 Then
    '        CDInfo.YParamVal = ParamDVal
    '        txtY2.Text = ParamDVal
    '        MouseCDPosNow.Y = ABInfo.XPos
    '        pbCD.Invalidate() 'Redraw pbCD
    '    End If
    '    UpdateData()
    '    Main.UpdateAnnotationSettings(SelDistrib)
    '    Main.ReplotCharts()

    'End Sub

    'Private Sub pbD_MouseMove(sender As Object, e As MouseEventArgs)
    '    If MouseDDown Then
    '        MouseDPosNow.X = e.X
    '        MouseDPosNow.Y = e.Y
    '        If MouseDPosNow.X < 0 Then MouseDPosNow.X = 0
    '        If MouseDPosNow.X > pbD.Width Then MouseDPosNow.X = pbD.Width

    '        pbD.Invalidate() 'Redraw pbD
    '        DInfo.XPos = MouseDPosNow.X
    '        ParamDVal = DInfo.XParamVal
    '        SelectedDistrib.ParamD.Value = ParamDVal
    '        txtD.Text = ParamDVal
    '        If cmbX1.SelectedIndex = 4 Then
    '            ABInfo.XParamVal = ParamDVal
    '            txtX1.Text = ParamDVal
    '            MouseABPosNow.X = ABInfo.XPos
    '            pbAB.Invalidate() 'Redraw pbAB
    '        ElseIf cmbY1.SelectedIndex = 4 Then
    '            ABInfo.YParamVal = ParamDVal
    '            txtY1.Text = ParamDVal
    '            MouseABPosNow.Y = ABInfo.YPos
    '            pbAB.Invalidate() 'Redraw pbAB
    '        ElseIf cmbX2.SelectedIndex = 4 Then
    '            CDInfo.XParamVal = ParamDVal
    '            txtX2.Text = ParamDVal
    '            MouseCDPosNow.X = ABInfo.XPos
    '            pbCD.Invalidate() 'Redraw pbCD
    '        ElseIf cmbY2.SelectedIndex = 4 Then
    '            CDInfo.YParamVal = ParamDVal
    '            txtY2.Text = ParamDVal
    '            MouseCDPosNow.Y = ABInfo.YPos
    '            pbCD.Invalidate() 'Redraw pbCD
    '        End If
    '        UpdateData()
    '        Main.UpdateAnnotationSettings(SelDistrib)
    '        Main.ReplotCharts()
    '    End If
    'End Sub

    'Private Sub pbD_MouseUp(sender As Object, e As MouseEventArgs)
    '    MouseDDown = False
    '    pbD.Invalidate()
    'End Sub

    'Private Sub pbD_Paint(sender As Object, e As PaintEventArgs)
    '    Dim myRectangle As New Rectangle
    '    Dim myPen As New Pen(Color.Black, 3)
    '    Dim Radius As Integer = 5
    '    myRectangle.X = MouseDPosNow.X - Radius
    '    myRectangle.Y = 0
    '    myRectangle.Width = Radius * 2
    '    myRectangle.Height = pbD.Height - 6
    '    e.Graphics.DrawRectangle(myPen, myRectangle)
    'End Sub

    'Private Sub pbE_Click(sender As Object, e As EventArgs)

    'End Sub

    'Private Sub pbE_MouseDown(sender As Object, e As MouseEventArgs)
    '    MouseEDown = True
    '    MouseEPosNow.X = e.X
    '    MouseEPosNow.Y = e.Y
    '    If MouseEPosNow.X < 0 Then MouseEPosNow.X = 0
    '    If MouseEPosNow.X > pbD.Width Then MouseEPosNow.X = pbE.Width

    '    pbE.Invalidate() 'Redraw pbE

    '    EInfo.XPos = e.X
    '    ParamEVal = EInfo.XParamVal
    '    SelectedDistrib.ParamE.Value = ParamEVal
    '    txtE.Text = ParamEVal
    '    If cmbX1.SelectedIndex = 5 Then
    '        ABInfo.XParamVal = ParamEVal
    '        txtX1.Text = ParamEVal
    '        MouseABPosNow.X = ABInfo.XPos
    '        pbAB.Invalidate() 'Redraw pbAB
    '    ElseIf cmbY1.SelectedIndex = 5 Then
    '        ABInfo.YParamVal = ParamEVal
    '        txtY1.Text = ParamEVal
    '        MouseABPosNow.Y = ABInfo.XPos
    '        pbAB.Invalidate() 'Redraw pbAB
    '    ElseIf cmbX2.SelectedIndex = 5 Then
    '        CDInfo.XParamVal = ParamEVal
    '        txtX2.Text = ParamEVal
    '        MouseCDPosNow.X = ABInfo.XPos
    '        pbCD.Invalidate() 'Redraw pbCD
    '    ElseIf cmbY2.SelectedIndex = 5 Then
    '        CDInfo.YParamVal = ParamEVal
    '        txtY2.Text = ParamEVal
    '        MouseCDPosNow.Y = ABInfo.XPos
    '        pbCD.Invalidate() 'Redraw pbCD
    '    End If
    '    UpdateData()
    '    Main.UpdateAnnotationSettings(SelDistrib)
    '    Main.ReplotCharts()

    'End Sub

    'Private Sub pbE_MouseMove(sender As Object, e As MouseEventArgs)
    '    If MouseEDown Then
    '        MouseEPosNow.X = e.X
    '        MouseEPosNow.Y = e.Y
    '        If MouseEPosNow.X < 0 Then MouseEPosNow.X = 0
    '        If MouseEPosNow.X > pbD.Width Then MouseEPosNow.X = pbE.Width

    '        pbE.Invalidate() 'Redraw pbE
    '        EInfo.XPos = MouseEPosNow.X
    '        ParamEVal = EInfo.XParamVal
    '        SelectedDistrib.ParamE.Value = ParamEVal
    '        txtE.Text = ParamEVal
    '        If cmbX1.SelectedIndex = 5 Then
    '            ABInfo.XParamVal = ParamEVal
    '            txtX1.Text = ParamEVal
    '            MouseABPosNow.X = ABInfo.XPos
    '            pbAB.Invalidate() 'Redraw pbAB
    '        ElseIf cmbY1.SelectedIndex = 5 Then
    '            ABInfo.YParamVal = ParamEVal
    '            txtY1.Text = ParamEVal
    '            MouseABPosNow.Y = ABInfo.YPos
    '            pbAB.Invalidate() 'Redraw pbAB
    '        ElseIf cmbX2.SelectedIndex = 5 Then
    '            CDInfo.XParamVal = ParamEVal
    '            txtX2.Text = ParamEVal
    '            MouseCDPosNow.X = ABInfo.XPos
    '            pbCD.Invalidate() 'Redraw pbCD
    '        ElseIf cmbY2.SelectedIndex = 5 Then
    '            CDInfo.YParamVal = ParamEVal
    '            txtY2.Text = ParamEVal
    '            MouseCDPosNow.Y = ABInfo.YPos
    '            pbCD.Invalidate() 'Redraw pbCD
    '        End If
    '        UpdateData()
    '        Main.UpdateAnnotationSettings(SelDistrib)
    '        Main.ReplotCharts()
    '    End If
    'End Sub

    'Private Sub pbE_MouseUp(sender As Object, e As MouseEventArgs)
    '    MouseEDown = False
    '    pbE.Invalidate()
    'End Sub

    'Private Sub pbE_Paint(sender As Object, e As PaintEventArgs)
    '    Dim myRectangle As New Rectangle
    '    Dim myPen As New Pen(Color.Black, 3)
    '    Dim Radius As Integer = 5
    '    myRectangle.X = MouseEPosNow.X - Radius
    '    myRectangle.Y = 0
    '    myRectangle.Width = Radius * 2
    '    myRectangle.Height = pbE.Height - 6
    '    e.Graphics.DrawRectangle(myPen, myRectangle)
    'End Sub

    Private Sub TabPage2_Click(sender As Object, e As EventArgs) Handles TabPage2.Click

    End Sub

    Private Sub TabPage2_Resize(sender As Object, e As EventArgs) Handles TabPage2.Resize
        'TabPage2 has changed size - reposition pbAB and pbCD
        pbAB.Width = TabPage2.Width / 2 - 6 - 3
        pbCD.Left = pbAB.Width + 6 + 3
        pbCD.Width = pbAB.Width

        Label21.Left = pbCD.Left
        cmbX2.Left = Label21.Left + Label21.Width + 6
        txtX2.Left = cmbX2.Left + cmbX2.Width + 6

        Label19.Left = pbCD.Left
        txtX2Min.Left = Label19.Left + Label19.Width + 6
        Label18.Left = txtX2Min.Left + txtX2Min.Width + 6
        txtX2Max.Left = Label18.Left + Label18.Width + 6

        Label20.Left = pbCD.Left
        cmbY2.Left = Label20.Left + Label20.Width + 6
        txtY2.Left = cmbY2.Left + cmbY2.Width + 6

        Label17.Left = pbCD.Left
        txtY2Min.Left = Label17.Left + Label17.Width + 6
        Label16.Left = txtY2Min.Left + txtY2Min.Width + 6
        txtY2Max.Left = Label16.Left + Label16.Width + 6

    End Sub

    Private Sub cmbX1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbX1.SelectedIndexChanged
        Select Case cmbX1.SelectedIndex
            Case 0 'None selected
                txtX1.Text = ""
                txtX1Min.Text = ""
                txtX1Max.Text = ""

            Case 1 'Param A selected
                txtX1.Text = ParamAVal
                txtX1Min.Text = ParamAAdjustMin
                txtX1Max.Text = ParamAAdjustMax
                'Set up ABInfo - used to generate parameter values graphically:
                ABInfo.XParamMin = ParamAAdjustMin
                ABInfo.XParamMax = ParamAAdjustMax
                ABInfo.XParamVal = ParamAVal


            Case 2 'Param B selected
                txtX1.Text = ParamBVal
                txtX1Min.Text = ParamBAdjustMin
                txtX1Max.Text = ParamBAdjustMax
                'Set up ABInfo - used to generate parameter values graphically:
                ABInfo.XParamMin = ParamBAdjustMin
                ABInfo.XParamMax = ParamBAdjustMax
                ABInfo.XParamVal = ParamBVal

            Case 3 'Param C selected
                txtX1.Text = ParamCVal
                txtX1Min.Text = ParamCAdjustMin
                txtX1Max.Text = ParamCAdjustMax
                'Set up ABInfo - used to generate parameter values graphically:
                ABInfo.XParamMin = ParamCAdjustMin
                ABInfo.XParamMax = ParamCAdjustMax
                ABInfo.XParamVal = ParamCVal

            Case 4 'Param D selected
                txtX1.Text = ParamDVal
                txtX1Min.Text = ParamDAdjustMin
                txtX1Max.Text = ParamDAdjustMax
                'Set up ABInfo - used to generate parameter values graphically:
                ABInfo.XParamMin = ParamDAdjustMin
                ABInfo.XParamMax = ParamDAdjustMax
                ABInfo.XParamVal = ParamDVal

            Case 5 'Param E selected
                txtX1.Text = ParamEVal
                txtX1Min.Text = ParamEAdjustMin
                txtX1Max.Text = ParamEAdjustMax
                'Set up ABInfo - used to generate parameter values graphically:
                ABInfo.XParamMin = ParamEAdjustMin
                ABInfo.XParamMax = ParamEAdjustMax
                ABInfo.XParamVal = ParamEVal

        End Select
    End Sub

    Private Sub cmbY1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbY1.SelectedIndexChanged
        Select Case cmbY1.SelectedIndex
            Case 0 'None selected
                txtY1.Text = ""
                txtY1Min.Text = ""
                txtY1Max.Text = ""

            Case 1 'Param A selected
                txtY1.Text = ParamAVal
                txtY1Min.Text = ParamAAdjustMin
                txtY1Max.Text = ParamAAdjustMax

            Case 2 'Param B selected
                txtY1.Text = ParamBVal
                txtY1Min.Text = ParamBAdjustMin
                txtY1Max.Text = ParamBAdjustMax

            Case 3 'Param C selected
                txtY1.Text = ParamCVal
                txtY1Min.Text = ParamCAdjustMin
                txtY1Max.Text = ParamCAdjustMax

            Case 4 'Param D selected
                txtY1.Text = ParamDVal
                txtY1Min.Text = ParamDAdjustMin
                txtY1Max.Text = ParamDAdjustMax

            Case 5 'Param E selected
                txtY1.Text = ParamEVal
                txtY1Min.Text = ParamEAdjustMin
                txtY1Max.Text = ParamEAdjustMax

        End Select
    End Sub

    Private Sub cmbX2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbX2.SelectedIndexChanged
        Select Case cmbX2.SelectedIndex
            Case 0 'None selected
                txtX2.Text = ""
                txtX2Min.Text = ""
                txtX2Max.Text = ""

            Case 1 'Param A selected
                txtX2.Text = ParamAVal
                txtX2Min.Text = ParamAAdjustMin
                txtX2Max.Text = ParamAAdjustMax

            Case 2 'Param B selected
                txtX2.Text = ParamBVal
                txtX2Min.Text = ParamBAdjustMin
                txtX2Max.Text = ParamBAdjustMax

            Case 3 'Param C selected
                txtX2.Text = ParamCVal
                txtX2Min.Text = ParamCAdjustMin
                txtX2Max.Text = ParamCAdjustMax

            Case 4 'Param D selected
                txtX2.Text = ParamDVal
                txtX2Min.Text = ParamDAdjustMin
                txtX2Max.Text = ParamDAdjustMax

            Case 5 'Param E selected
                txtX2.Text = ParamEVal
                txtX2Min.Text = ParamEAdjustMin
                txtX2Max.Text = ParamEAdjustMax

        End Select
    End Sub

    Private Sub cmbY2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbY2.SelectedIndexChanged
        Select Case cmbY2.SelectedIndex
            Case 0 'None selected
                txtY2.Text = ""
                txtY2Min.Text = ""
                txtY2Max.Text = ""

            Case 1 'Param A selected
                txtY2.Text = ParamAVal
                txtY2Min.Text = ParamAAdjustMin
                txtY2Max.Text = ParamAAdjustMax

            Case 2 'Param B selected
                txtY2.Text = ParamBVal
                txtY2Min.Text = ParamBAdjustMin
                txtY2Max.Text = ParamBAdjustMax

            Case 3 'Param C selected
                txtY2.Text = ParamCVal
                txtY2Min.Text = ParamCAdjustMin
                txtY2Max.Text = ParamCAdjustMax

            Case 4 'Param D selected
                txtY2.Text = ParamDVal
                txtY2Min.Text = ParamDAdjustMin
                txtY2Max.Text = ParamDAdjustMax

            Case 5 'Param E selected
                txtY2.Text = ParamEVal
                txtY2Min.Text = ParamEAdjustMin
                txtY2Max.Text = ParamEAdjustMax

        End Select
    End Sub

    'Private Sub cmbA_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    Select Case cmbA.SelectedIndex
    '        Case 0 'None selected
    '            txtA.Text = ""
    '            txtAMin.Text = ""
    '            txtAMax.Text = ""

    '        Case 1 'Param A selected
    '            txtA.Text = ParamAVal
    '            txtAMin.Text = ParamAAdjustMin
    '            txtAMax.Text = ParamAAdjustMax

    '        Case 2 'Param B selected
    '            txtA.Text = ParamBVal
    '            txtAMin.Text = ParamBAdjustMin
    '            txtAMax.Text = ParamBAdjustMax

    '        Case 3 'Param C selected
    '            txtA.Text = ParamCVal
    '            txtAMin.Text = ParamCAdjustMin
    '            txtAMax.Text = ParamCAdjustMax

    '        Case 4 'Param D selected
    '            txtA.Text = ParamDVal
    '            txtAMin.Text = ParamDAdjustMin
    '            txtAMax.Text = ParamDAdjustMax

    '        Case 5 'Param E selected
    '            txtA.Text = ParamEVal
    '            txtAMin.Text = ParamEAdjustMin
    '            txtAMax.Text = ParamEAdjustMax

    '    End Select
    'End Sub

    'Private Sub cmbB_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    Select Case cmbB.SelectedIndex
    '        Case 0 'None selected
    '            txtB.Text = ""
    '            txtBMin.Text = ""
    '            txtBMax.Text = ""

    '        Case 1 'Param A selected
    '            txtB.Text = ParamAVal
    '            txtBMin.Text = ParamAAdjustMin
    '            txtBMax.Text = ParamAAdjustMax

    '        Case 2 'Param B selected
    '            txtB.Text = ParamBVal
    '            txtBMin.Text = ParamBAdjustMin
    '            txtBMax.Text = ParamBAdjustMax

    '        Case 3 'Param C selected
    '            txtB.Text = ParamCVal
    '            txtBMin.Text = ParamCAdjustMin
    '            txtBMax.Text = ParamCAdjustMax

    '        Case 4 'Param D selected
    '            txtB.Text = ParamDVal
    '            txtBMin.Text = ParamDAdjustMin
    '            txtBMax.Text = ParamDAdjustMax

    '        Case 5 'Param E selected
    '            txtB.Text = ParamEVal
    '            txtBMin.Text = ParamEAdjustMin
    '            txtBMax.Text = ParamEAdjustMax

    '    End Select
    'End Sub

    'Private Sub cmbC_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    Select Case cmbB.SelectedIndex
    '        Case 0 'None selected
    '            txtC.Text = ""
    '            txtCMin.Text = ""
    '            txtCMax.Text = ""

    '        Case 1 'Param A selected
    '            txtC.Text = ParamAVal
    '            txtCMin.Text = ParamAAdjustMin
    '            txtCMax.Text = ParamAAdjustMax

    '        Case 2 'Param B selected
    '            txtC.Text = ParamBVal
    '            txtCMin.Text = ParamBAdjustMin
    '            txtCMax.Text = ParamBAdjustMax

    '        Case 3 'Param C selected
    '            txtC.Text = ParamCVal
    '            txtCMin.Text = ParamCAdjustMin
    '            txtCMax.Text = ParamCAdjustMax

    '        Case 4 'Param D selected
    '            txtC.Text = ParamDVal
    '            txtCMin.Text = ParamDAdjustMin
    '            txtCMax.Text = ParamDAdjustMax

    '        Case 5 'Param E selected
    '            txtC.Text = ParamEVal
    '            txtCMin.Text = ParamEAdjustMin
    '            txtCMax.Text = ParamEAdjustMax

    '    End Select
    'End Sub

    'Private Sub cmbD_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    Select Case cmbB.SelectedIndex
    '        Case 0 'None selected
    '            txtD.Text = ""
    '            txtDMin.Text = ""
    '            txtDMax.Text = ""

    '        Case 1 'Param A selected
    '            txtD.Text = ParamAVal
    '            txtDMin.Text = ParamAAdjustMin
    '            txtDMax.Text = ParamAAdjustMax

    '        Case 2 'Param B selected
    '            txtD.Text = ParamBVal
    '            txtDMin.Text = ParamBAdjustMin
    '            txtDMax.Text = ParamBAdjustMax

    '        Case 3 'Param C selected
    '            txtD.Text = ParamCVal
    '            txtDMin.Text = ParamCAdjustMin
    '            txtDMax.Text = ParamCAdjustMax

    '        Case 4 'Param D selected
    '            txtD.Text = ParamDVal
    '            txtDMin.Text = ParamDAdjustMin
    '            txtDMax.Text = ParamDAdjustMax

    '        Case 5 'Param E selected
    '            txtD.Text = ParamEVal
    '            txtDMin.Text = ParamEAdjustMin
    '            txtDMax.Text = ParamEAdjustMax

    '    End Select
    'End Sub

    'Private Sub cmbE_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    Select Case cmbB.SelectedIndex
    '        Case 0 'None selected
    '            txtE.Text = ""
    '            txtEMin.Text = ""
    '            txtEMax.Text = ""

    '        Case 1 'Param A selected
    '            txtE.Text = ParamAVal
    '            txtEMin.Text = ParamAAdjustMin
    '            txtEMax.Text = ParamAAdjustMax

    '        Case 2 'Param B selected
    '            txtE.Text = ParamBVal
    '            txtEMin.Text = ParamBAdjustMin
    '            txtEMax.Text = ParamBAdjustMax

    '        Case 3 'Param C selected
    '            txtE.Text = ParamCVal
    '            txtEMin.Text = ParamCAdjustMin
    '            txtEMax.Text = ParamCAdjustMax

    '        Case 4 'Param D selected
    '            txtE.Text = ParamDVal
    '            txtEMin.Text = ParamDAdjustMin
    '            txtEMax.Text = ParamDAdjustMax

    '        Case 5 'Param E selected
    '            txtE.Text = ParamEVal
    '            txtEMin.Text = ParamEAdjustMin
    '            txtEMax.Text = ParamEAdjustMax

    '    End Select
    'End Sub

    Public Sub UpdateParamAValue(Value As Double)
        SelectedDistrib.ParamA.Value = Value
        ParamAVal = Value
    End Sub

    Private Sub txtX1Min_TextChanged(sender As Object, e As EventArgs) Handles txtX1Min.TextChanged

    End Sub

    Private Sub txtX1Min_LostFocus(sender As Object, e As EventArgs) Handles txtX1Min.LostFocus
        Try
            Select Case cmbX1.SelectedIndex
                Case 0

                Case 1
                    ParamAAdjustMin = txtX1Min.Text
                    SelectedDistrib.ParamA.AdjustMin = ParamAAdjustMin
                    ABInfo.XParamMin = ParamAAdjustMin
                    MouseABPosNow.X = ABInfo.XPos
                    txtAMin.Text = ParamAAdjustMin
                    'AInfo.XParamMin = ParamAAdjustMin
                    'MouseAPosNow.X = AInfo.XPos
                    'pbA.Invalidate()
                    hsA.Value = 1000 * (ParamAVal - ParamAAdjustMin) / (ParamAAdjustMax - ParamAAdjustMin)
                Case 2
                    ParamBAdjustMin = txtX1Min.Text
                    SelectedDistrib.ParamB.AdjustMin = ParamBAdjustMin
                    ABInfo.XParamMin = ParamBAdjustMin
                    MouseABPosNow.X = ABInfo.XPos
                    txtBMin.Text = ParamBAdjustMin
                    'BInfo.XParamMin = ParamBAdjustMin
                    'MouseBPosNow.X = BInfo.XPos
                    'pbB.Invalidate()
                    hsB.Value = 1000 * (ParamBVal - ParamBAdjustMin) / (ParamBAdjustMax - ParamBAdjustMin)

                Case 3
                    ParamCAdjustMin = txtX1Min.Text
                    SelectedDistrib.ParamC.AdjustMin = ParamCAdjustMin
                    ABInfo.XParamMin = ParamCAdjustMin
                    MouseABPosNow.X = ABInfo.XPos
                    txtCMin.Text = ParamCAdjustMin
                    'CInfo.XParamMin = ParamCAdjustMin
                    'MouseCPosNow.X = CInfo.XPos
                    'pbC.Invalidate()
                    hsC.Value = 1000 * (ParamCVal - ParamCAdjustMin) / (ParamCAdjustMax - ParamCAdjustMin)

                Case 4
                    ParamDAdjustMin = txtX1Min.Text
                    SelectedDistrib.ParamD.AdjustMin = ParamDAdjustMin
                    ABInfo.XParamMin = ParamDAdjustMin
                    MouseABPosNow.X = ABInfo.XPos
                    txtDMin.Text = ParamDAdjustMin
                    'DInfo.XParamMin = ParamDAdjustMin
                    'MouseDPosNow.X = DInfo.XPos
                    'pbD.Invalidate()
                    hsD.Value = 1000 * (ParamDVal - ParamDAdjustMin) / (ParamDAdjustMax - ParamDAdjustMin)

                Case 5
                    ParamEAdjustMin = txtX1Min.Text
                    SelectedDistrib.ParamE.AdjustMin = ParamEAdjustMin
                    ABInfo.XParamMin = ParamEAdjustMin
                    MouseABPosNow.X = ABInfo.XPos
                    txtEMin.Text = ParamEAdjustMin
                    'EInfo.XParamMin = ParamEAdjustMin
                    'MouseEPosNow.X = EInfo.XPos
                    'pbE.Invalidate()
                    hsE.Value = 1000 * (ParamEVal - ParamEAdjustMin) / (ParamEAdjustMax - ParamEAdjustMin)

            End Select
            Main.Distribution.Modified = True
            pbAB.Invalidate()
        Catch ex As Exception
            Main.Message.AddWarning(ex.Message)
        End Try
    End Sub

    Private Sub txtX1Max_TextChanged(sender As Object, e As EventArgs) Handles txtX1Max.TextChanged

    End Sub

    Private Sub txtX1Max_LostFocus(sender As Object, e As EventArgs) Handles txtX1Max.LostFocus
        Try
            Select Case cmbX1.SelectedIndex
                Case 0

                Case 1
                    ParamAAdjustMax = txtX1Max.Text
                    SelectedDistrib.ParamA.AdjustMax = ParamAAdjustMax
                    ABInfo.XParamMax = ParamAAdjustMax
                    MouseABPosNow.X = ABInfo.XPos
                    txtAMax.Text = ParamAAdjustMax
                    'AInfo.XParamMax = ParamAAdjustMax
                    'MouseAPosNow.X = AInfo.XPos
                    'pbA.Invalidate()
                    hsA.Value = 1000 * (ParamAVal - ParamAAdjustMin) / (ParamAAdjustMax - ParamAAdjustMin)
                Case 2
                    ParamBAdjustMax = txtX1Max.Text
                    SelectedDistrib.ParamB.AdjustMax = ParamBAdjustMax
                    ABInfo.XParamMax = ParamBAdjustMax
                    MouseABPosNow.X = ABInfo.XPos
                    txtBMax.Text = ParamBAdjustMax
                    'BInfo.XParamMax = ParamBAdjustMax
                    'MouseBPosNow.X = BInfo.XPos
                    'pbB.Invalidate()
                    hsB.Value = 1000 * (ParamBVal - ParamBAdjustMin) / (ParamBAdjustMax - ParamBAdjustMin)

                Case 3
                    ParamCAdjustMax = txtX1Max.Text
                    SelectedDistrib.ParamC.AdjustMax = ParamCAdjustMax
                    ABInfo.XParamMax = ParamCAdjustMax
                    MouseABPosNow.X = ABInfo.XPos
                    txtCMax.Text = ParamCAdjustMax
                    'CInfo.XParamMax = ParamCAdjustMax
                    'MouseCPosNow.X = CInfo.XPos
                    'pbC.Invalidate()
                    hsC.Value = 1000 * (ParamCVal - ParamCAdjustMin) / (ParamCAdjustMax - ParamCAdjustMin)

                Case 4
                    ParamDAdjustMax = txtX1Max.Text
                    ABInfo.XParamMax = ParamDAdjustMax
                    SelectedDistrib.ParamD.AdjustMax = ParamDAdjustMax
                    MouseABPosNow.X = ABInfo.XPos
                    txtDMax.Text = ParamDAdjustMax
                    'DInfo.XParamMax = ParamDAdjustMax
                    'MouseDPosNow.X = DInfo.XPos
                    'pbD.Invalidate()
                    hsD.Value = 1000 * (ParamDVal - ParamDAdjustMin) / (ParamDAdjustMax - ParamDAdjustMin)

                Case 5
                    ParamEAdjustMax = txtX1Max.Text
                    SelectedDistrib.ParamE.AdjustMax = ParamEAdjustMax
                    ABInfo.XParamMax = ParamEAdjustMax
                    MouseABPosNow.X = ABInfo.XPos
                    txtEMax.Text = ParamEAdjustMax
                    'EInfo.XParamMax = ParamEAdjustMax
                    'MouseEPosNow.X = EInfo.XPos
                    'pbE.Invalidate()
                    hsE.Value = 1000 * (ParamEVal - ParamEAdjustMin) / (ParamEAdjustMax - ParamEAdjustMin)

            End Select
            Main.Distribution.Modified = True
            pbAB.Invalidate()
        Catch ex As Exception
            Main.Message.AddWarning(ex.Message)
        End Try
    End Sub

    Private Sub txtY1Min_TextChanged(sender As Object, e As EventArgs) Handles txtY1Min.TextChanged

    End Sub

    Private Sub txtY1Min_LostFocus(sender As Object, e As EventArgs) Handles txtY1Min.LostFocus
        Try
            Select Case cmbY1.SelectedIndex
                Case 0

                Case 1
                    ParamAAdjustMin = txtY1Min.Text
                    SelectedDistrib.ParamA.AdjustMin = ParamAAdjustMin
                    ABInfo.YParamMin = ParamAAdjustMin
                    MouseABPosNow.Y = ABInfo.YPos
                    txtAMin.Text = ParamAAdjustMin
                    'AInfo.XParamMin = ParamAAdjustMin
                    'MouseAPosNow.X = AInfo.XPos
                    'pbA.Invalidate()
                    hsA.Value = 1000 * (ParamAVal - ParamAAdjustMin) / (ParamAAdjustMax - ParamAAdjustMin)
                Case 2
                    ParamBAdjustMin = txtY1Min.Text
                    SelectedDistrib.ParamB.AdjustMin = ParamBAdjustMin
                    ABInfo.YParamMin = ParamBAdjustMin
                    MouseABPosNow.Y = ABInfo.YPos
                    txtBMin.Text = ParamBAdjustMin
                    'BInfo.XParamMin = ParamBAdjustMin
                    'MouseBPosNow.X = BInfo.XPos
                    'pbB.Invalidate()
                    hsB.Value = 1000 * (ParamBVal - ParamBAdjustMin) / (ParamBAdjustMax - ParamBAdjustMin)

                Case 3
                    ParamCAdjustMin = txtY1Min.Text
                    SelectedDistrib.ParamC.AdjustMin = ParamCAdjustMin
                    ABInfo.YParamMin = ParamCAdjustMin
                    MouseABPosNow.Y = ABInfo.YPos
                    txtCMin.Text = ParamCAdjustMin
                    'CInfo.XParamMin = ParamCAdjustMin
                    'MouseCPosNow.X = CInfo.XPos
                    'pbC.Invalidate()
                    hsC.Value = 1000 * (ParamCVal - ParamCAdjustMin) / (ParamCAdjustMax - ParamCAdjustMin)

                Case 4
                    ParamDAdjustMin = txtY1Min.Text
                    SelectedDistrib.ParamD.AdjustMin = ParamDAdjustMin
                    ABInfo.YParamMin = ParamDAdjustMin
                    MouseABPosNow.Y = ABInfo.YPos
                    txtDMin.Text = ParamDAdjustMin
                    'DInfo.XParamMin = ParamDAdjustMin
                    'MouseDPosNow.X = DInfo.XPos
                    'pbD.Invalidate()
                    hsD.Value = 1000 * (ParamDVal - ParamDAdjustMin) / (ParamDAdjustMax - ParamDAdjustMin)

                Case 5
                    ParamEAdjustMin = txtY1Min.Text
                    SelectedDistrib.ParamE.AdjustMin = ParamEAdjustMin
                    ABInfo.YParamMin = ParamEAdjustMin
                    MouseABPosNow.Y = ABInfo.YPos
                    txtEMin.Text = ParamEAdjustMin
                    'EInfo.XParamMin = ParamEAdjustMin
                    'MouseEPosNow.X = EInfo.XPos
                    'pbE.Invalidate()
                    hsE.Value = 1000 * (ParamEVal - ParamEAdjustMin) / (ParamEAdjustMax - ParamEAdjustMin)

            End Select
            Main.Distribution.Modified = True
            pbAB.Invalidate()
        Catch ex As Exception
            Main.Message.AddWarning(ex.Message)
        End Try
    End Sub

    Private Sub txtY1Max_TextChanged(sender As Object, e As EventArgs) Handles txtY1Max.TextChanged

    End Sub

    Private Sub txtY1Max_LostFocus(sender As Object, e As EventArgs) Handles txtY1Max.LostFocus
        Try
            Select Case cmbY1.SelectedIndex
                Case 0

                Case 1
                    ParamAAdjustMax = txtY1Max.Text
                    SelectedDistrib.ParamA.AdjustMax = ParamAAdjustMax
                    ABInfo.YParamMax = ParamAAdjustMax
                    MouseABPosNow.Y = ABInfo.YPos
                    txtAMax.Text = ParamAAdjustMax
                    'AInfo.XParamMax = ParamAAdjustMax
                    'MouseAPosNow.X = AInfo.XPos
                    'pbA.Invalidate()
                    hsA.Value = 1000 * (ParamAVal - ParamAAdjustMin) / (ParamAAdjustMax - ParamAAdjustMin)
                Case 2
                    ParamBAdjustMax = txtY1Max.Text
                    SelectedDistrib.ParamB.AdjustMax = ParamBAdjustMax
                    ABInfo.YParamMax = ParamBAdjustMax
                    MouseABPosNow.Y = ABInfo.YPos
                    txtBMax.Text = ParamBAdjustMax
                    'BInfo.XParamMax = ParamBAdjustMax
                    'MouseBPosNow.X = BInfo.XPos
                    'pbB.Invalidate()
                    hsB.Value = 1000 * (ParamBVal - ParamBAdjustMin) / (ParamBAdjustMax - ParamBAdjustMin)

                Case 3
                    ParamCAdjustMax = txtY1Max.Text
                    SelectedDistrib.ParamC.AdjustMax = ParamCAdjustMax
                    ABInfo.YParamMax = ParamCAdjustMax
                    MouseABPosNow.Y = ABInfo.YPos
                    txtCMax.Text = ParamCAdjustMax
                    'CInfo.XParamMax = ParamCAdjustMax
                    'MouseCPosNow.X = CInfo.XPos
                    'pbC.Invalidate()
                    hsC.Value = 1000 * (ParamCVal - ParamCAdjustMin) / (ParamCAdjustMax - ParamCAdjustMin)

                Case 4
                    ParamDAdjustMax = txtY1Max.Text
                    SelectedDistrib.ParamD.AdjustMax = ParamDAdjustMax
                    ABInfo.YParamMax = ParamDAdjustMax
                    MouseABPosNow.Y = ABInfo.YPos
                    txtDMax.Text = ParamDAdjustMax
                    'DInfo.XParamMax = ParamDAdjustMax
                    'MouseDPosNow.X = DInfo.XPos
                    'pbD.Invalidate()
                    hsD.Value = 1000 * (ParamDVal - ParamDAdjustMin) / (ParamDAdjustMax - ParamDAdjustMin)

                Case 5
                    ParamEAdjustMin = txtY1Max.Text
                    SelectedDistrib.ParamE.AdjustMax = ParamEAdjustMax
                    ABInfo.YParamMax = ParamEAdjustMin
                    MouseABPosNow.Y = ABInfo.YPos
                    txtEMax.Text = ParamEAdjustMax
                    'EInfo.XParamMax = ParamEAdjustMax
                    'MouseEPosNow.X = EInfo.XPos
                    'pbE.Invalidate()
                    hsE.Value = 1000 * (ParamEVal - ParamEAdjustMin) / (ParamEAdjustMax - ParamEAdjustMin)

            End Select
            Main.Distribution.Modified = True
            pbAB.Invalidate()
        Catch ex As Exception
            Main.Message.AddWarning(ex.Message)
        End Try
    End Sub

    Private Sub txtX2Min_TextChanged(sender As Object, e As EventArgs) Handles txtX2Min.TextChanged

    End Sub

    Private Sub txtX2Min_LostFocus(sender As Object, e As EventArgs) Handles txtX2Min.LostFocus
        Try
            Select Case cmbX2.SelectedIndex
                Case 0

                Case 1
                    ParamAAdjustMin = txtX2Min.Text
                    SelectedDistrib.ParamA.AdjustMin = ParamAAdjustMin
                    CDInfo.XParamMin = ParamAAdjustMin
                    MouseCDPosNow.X = CDInfo.XPos
                    txtAMin.Text = ParamAAdjustMin
                    'AInfo.XParamMin = ParamAAdjustMin
                    'MouseAPosNow.X = AInfo.XPos
                    'pbA.Invalidate()
                    hsA.Value = 1000 * (ParamAVal - ParamAAdjustMin) / (ParamAAdjustMax - ParamAAdjustMin)
                Case 2
                    ParamBAdjustMin = txtX2Min.Text
                    SelectedDistrib.ParamB.AdjustMin = ParamBAdjustMin
                    CDInfo.XParamMin = ParamBAdjustMin
                    MouseCDPosNow.X = CDInfo.XPos
                    txtBMin.Text = ParamBAdjustMin
                    'BInfo.XParamMin = ParamBAdjustMin
                    'MouseBPosNow.X = BInfo.XPos
                    'pbB.Invalidate()
                    hsB.Value = 1000 * (ParamBVal - ParamBAdjustMin) / (ParamBAdjustMax - ParamBAdjustMin)

                Case 3
                    ParamCAdjustMin = txtX2Min.Text
                    SelectedDistrib.ParamC.AdjustMin = ParamCAdjustMin
                    CDInfo.XParamMin = ParamCAdjustMin
                    MouseCDPosNow.X = CDInfo.XPos
                    txtCMin.Text = ParamCAdjustMin
                    'CInfo.XParamMin = ParamCAdjustMin
                    'MouseCPosNow.X = CInfo.XPos
                    'pbC.Invalidate()
                    hsC.Value = 1000 * (ParamCVal - ParamCAdjustMin) / (ParamCAdjustMax - ParamCAdjustMin)

                Case 4
                    ParamDAdjustMin = txtX2Min.Text
                    SelectedDistrib.ParamD.AdjustMin = ParamDAdjustMin
                    CDInfo.XParamMin = ParamDAdjustMin
                    MouseCDPosNow.X = CDInfo.XPos
                    txtDMin.Text = ParamDAdjustMin
                    'DInfo.XParamMin = ParamDAdjustMin
                    'MouseDPosNow.X = DInfo.XPos
                    'pbD.Invalidate()
                    hsD.Value = 1000 * (ParamDVal - ParamDAdjustMin) / (ParamDAdjustMax - ParamDAdjustMin)

                Case 5
                    ParamEAdjustMin = txtX2Min.Text
                    SelectedDistrib.ParamE.AdjustMin = ParamEAdjustMin
                    CDInfo.XParamMin = ParamEAdjustMin
                    MouseCDPosNow.X = CDInfo.XPos
                    txtEMin.Text = ParamEAdjustMin
                    'EInfo.XParamMin = ParamEAdjustMin
                    'MouseEPosNow.X = EInfo.XPos
                    'pbE.Invalidate()
                    hsE.Value = 1000 * (ParamEVal - ParamEAdjustMin) / (ParamEAdjustMax - ParamEAdjustMin)

            End Select
            Main.Distribution.Modified = True
            pbCD.Invalidate()
        Catch ex As Exception
            Main.Message.AddWarning(ex.Message)
        End Try
    End Sub

    Private Sub txtX2Max_TextChanged(sender As Object, e As EventArgs) Handles txtX2Max.TextChanged

    End Sub

    Private Sub txtX2Max_LostFocus(sender As Object, e As EventArgs) Handles txtX2Max.LostFocus
        Try
            Select Case cmbX2.SelectedIndex
                Case 0

                Case 1
                    ParamAAdjustMax = txtX2Max.Text
                    SelectedDistrib.ParamA.AdjustMax = ParamAAdjustMax
                    CDInfo.XParamMax = ParamAAdjustMax
                    MouseCDPosNow.X = CDInfo.XPos
                    txtAMax.Text = ParamAAdjustMax
                    'AInfo.XParamMax = ParamAAdjustMax
                    'MouseAPosNow.X = AInfo.XPos
                    'pbA.Invalidate()
                    hsA.Value = 1000 * (ParamAVal - ParamAAdjustMin) / (ParamAAdjustMax - ParamAAdjustMin)
                Case 2
                    ParamBAdjustMax = txtX2Max.Text
                    SelectedDistrib.ParamB.AdjustMax = ParamBAdjustMax
                    CDInfo.XParamMax = ParamBAdjustMax
                    MouseCDPosNow.X = CDInfo.XPos
                    txtBMax.Text = ParamBAdjustMax
                    'BInfo.XParamMax = ParamBAdjustMax
                    'MouseBPosNow.X = BInfo.XPos
                    'pbB.Invalidate()
                    hsB.Value = 1000 * (ParamBVal - ParamBAdjustMin) / (ParamBAdjustMax - ParamBAdjustMin)

                Case 3
                    ParamCAdjustMax = txtX2Max.Text
                    SelectedDistrib.ParamC.AdjustMax = ParamCAdjustMax
                    CDInfo.XParamMax = ParamCAdjustMax
                    MouseCDPosNow.X = CDInfo.XPos
                    txtCMax.Text = ParamCAdjustMax
                    'CInfo.XParamMax = ParamCAdjustMax
                    'MouseCPosNow.X = CInfo.XPos
                    'pbC.Invalidate()
                    hsC.Value = 1000 * (ParamCVal - ParamCAdjustMin) / (ParamCAdjustMax - ParamCAdjustMin)

                Case 4
                    ParamDAdjustMax = txtX2Max.Text
                    CDInfo.XParamMax = ParamDAdjustMax
                    SelectedDistrib.ParamD.AdjustMax = ParamDAdjustMax
                    MouseCDPosNow.X = CDInfo.XPos
                    txtDMax.Text = ParamDAdjustMax
                    'DInfo.XParamMax = ParamDAdjustMax
                    'MouseDPosNow.X = DInfo.XPos
                    'pbD.Invalidate()
                    hsD.Value = 1000 * (ParamDVal - ParamDAdjustMin) / (ParamDAdjustMax - ParamDAdjustMin)

                Case 5
                    ParamEAdjustMax = txtX2Max.Text
                    SelectedDistrib.ParamE.AdjustMax = ParamEAdjustMax
                    CDInfo.XParamMax = ParamEAdjustMax
                    MouseCDPosNow.X = CDInfo.XPos
                    txtEMax.Text = ParamEAdjustMax
                    'EInfo.XParamMax = ParamEAdjustMax
                    'MouseEPosNow.X = EInfo.XPos
                    'pbE.Invalidate()
                    hsE.Value = 1000 * (ParamEVal - ParamEAdjustMin) / (ParamEAdjustMax - ParamEAdjustMin)

            End Select
            Main.Distribution.Modified = True
            pbCD.Invalidate()
        Catch ex As Exception
            Main.Message.AddWarning(ex.Message)
        End Try
    End Sub

    Private Sub txtY2Min_TextChanged(sender As Object, e As EventArgs) Handles txtY2Min.TextChanged

    End Sub

    Private Sub txtY2Min_LostFocus(sender As Object, e As EventArgs) Handles txtY2Min.LostFocus
        Try
            Select Case cmbY2.SelectedIndex
                Case 0

                Case 1
                    ParamAAdjustMin = txtY2Min.Text
                    SelectedDistrib.ParamA.AdjustMin = ParamAAdjustMin
                    CDInfo.YParamMin = ParamAAdjustMin
                    MouseCDPosNow.Y = CDInfo.YPos
                    txtAMin.Text = ParamAAdjustMin
                    'AInfo.XParamMin = ParamAAdjustMin
                    'MouseAPosNow.X = AInfo.XPos
                    'pbA.Invalidate()
                    hsA.Value = 1000 * (ParamAVal - ParamAAdjustMin) / (ParamAAdjustMax - ParamAAdjustMin)
                Case 2
                    ParamBAdjustMin = txtY2Min.Text
                    SelectedDistrib.ParamB.AdjustMin = ParamBAdjustMin
                    CDInfo.YParamMin = ParamBAdjustMin
                    MouseCDPosNow.Y = CDInfo.YPos
                    txtBMin.Text = ParamBAdjustMin
                    'BInfo.XParamMin = ParamBAdjustMin
                    'MouseBPosNow.X = BInfo.XPos
                    'pbB.Invalidate()
                    hsB.Value = 1000 * (ParamBVal - ParamBAdjustMin) / (ParamBAdjustMax - ParamBAdjustMin)

                Case 3
                    ParamCAdjustMin = txtY2Min.Text
                    SelectedDistrib.ParamC.AdjustMin = ParamCAdjustMin
                    CDInfo.YParamMin = ParamCAdjustMin
                    MouseCDPosNow.Y = CDInfo.YPos
                    txtCMin.Text = ParamCAdjustMin
                    'CInfo.XParamMin = ParamCAdjustMin
                    'MouseCPosNow.X = CInfo.XPos
                    'pbC.Invalidate()
                    hsC.Value = 1000 * (ParamCVal - ParamCAdjustMin) / (ParamCAdjustMax - ParamCAdjustMin)

                Case 4
                    ParamDAdjustMin = txtY2Min.Text
                    SelectedDistrib.ParamD.AdjustMin = ParamDAdjustMin
                    CDInfo.YParamMin = ParamDAdjustMin
                    MouseCDPosNow.Y = CDInfo.YPos
                    txtDMin.Text = ParamDAdjustMin
                    'DInfo.XParamMin = ParamDAdjustMin
                    'MouseDPosNow.X = DInfo.XPos
                    'pbD.Invalidate()
                    hsD.Value = 1000 * (ParamDVal - ParamDAdjustMin) / (ParamDAdjustMax - ParamDAdjustMin)

                Case 5
                    ParamEAdjustMin = txtY2Min.Text
                    SelectedDistrib.ParamE.AdjustMin = ParamEAdjustMin
                    CDInfo.YParamMin = ParamEAdjustMin
                    MouseCDPosNow.Y = CDInfo.YPos
                    txtEMin.Text = ParamEAdjustMin
                    'EInfo.XParamMin = ParamEAdjustMin
                    'MouseEPosNow.X = EInfo.XPos
                    'pbE.Invalidate()
                    hsE.Value = 1000 * (ParamEVal - ParamEAdjustMin) / (ParamEAdjustMax - ParamEAdjustMin)

            End Select
            Main.Distribution.Modified = True
            pbCD.Invalidate()
        Catch ex As Exception
            Main.Message.AddWarning(ex.Message)
        End Try
    End Sub

    Private Sub txtY2Max_TextChanged(sender As Object, e As EventArgs) Handles txtY2Max.TextChanged

    End Sub

    Private Sub txtY2Max_LostFocus(sender As Object, e As EventArgs) Handles txtY2Max.LostFocus
        Try
            Select Case cmbY2.SelectedIndex
                Case 0

                Case 1
                    ParamAAdjustMax = txtY2Max.Text
                    'SelectedDistrib.ParamA.AdjustMin = ParamAAdjustMax
                    SelectedDistrib.ParamA.AdjustMax = ParamAAdjustMax
                    CDInfo.YParamMax = ParamAAdjustMax
                    MouseCDPosNow.Y = CDInfo.YPos
                    txtAMax.Text = ParamAAdjustMax
                    'AInfo.XParamMax = ParamAAdjustMax
                    'MouseAPosNow.X = AInfo.XPos
                    'pbA.Invalidate()
                    hsA.Value = 1000 * (ParamAVal - ParamAAdjustMin) / (ParamAAdjustMax - ParamAAdjustMin)
                Case 2
                    ParamBAdjustMax = txtY2Max.Text
                    'SelectedDistrib.ParamB.AdjustMin = ParamBAdjustMax
                    SelectedDistrib.ParamB.AdjustMax = ParamBAdjustMax
                    CDInfo.YParamMax = ParamBAdjustMax
                    MouseCDPosNow.Y = CDInfo.YPos
                    txtBMax.Text = ParamBAdjustMax
                    'BInfo.XParamMax = ParamBAdjustMax
                    'MouseBPosNow.X = BInfo.XPos
                    'pbB.Invalidate()
                    hsB.Value = 1000 * (ParamBVal - ParamBAdjustMin) / (ParamBAdjustMax - ParamBAdjustMin)

                Case 3
                    ParamCAdjustMax = txtY2Max.Text
                    'SelectedDistrib.ParamC.AdjustMin = ParamCAdjustMax
                    SelectedDistrib.ParamC.AdjustMax = ParamCAdjustMax
                    CDInfo.YParamMax = ParamCAdjustMax
                    MouseCDPosNow.Y = CDInfo.YPos
                    txtCMax.Text = ParamCAdjustMax
                    'CInfo.XParamMax = ParamCAdjustMax
                    'MouseCPosNow.X = CInfo.XPos
                    'pbC.Invalidate()
                    hsC.Value = 1000 * (ParamCVal - ParamCAdjustMin) / (ParamCAdjustMax - ParamCAdjustMin)

                Case 4
                    ParamDAdjustMax = txtY2Max.Text
                    'SelectedDistrib.ParamD.AdjustMin = ParamDAdjustMax
                    SelectedDistrib.ParamD.AdjustMax = ParamDAdjustMax
                    CDInfo.YParamMax = ParamDAdjustMax
                    MouseCDPosNow.Y = CDInfo.YPos
                    txtDMax.Text = ParamDAdjustMax
                    'DInfo.XParamMax = ParamDAdjustMax
                    'MouseDPosNow.X = DInfo.XPos
                    'pbD.Invalidate()
                    hsD.Value = 1000 * (ParamDVal - ParamDAdjustMin) / (ParamDAdjustMax - ParamDAdjustMin)

                Case 5
                    ParamEAdjustMax = txtY2Max.Text
                    'SelectedDistrib.ParamE.AdjustMin = ParamEAdjustMax
                    SelectedDistrib.ParamE.AdjustMax = ParamEAdjustMax
                    CDInfo.YParamMax = ParamEAdjustMax
                    MouseCDPosNow.Y = CDInfo.YPos
                    txtEMax.Text = ParamEAdjustMax
                    'EInfo.XParamMin = ParamEAdjustMin
                    'MouseEPosNow.X = EInfo.XPos
                    'pbE.Invalidate()
                    hsE.Value = 1000 * (ParamEVal - ParamEAdjustMin) / (ParamEAdjustMax - ParamEAdjustMin)

            End Select
            Main.Distribution.Modified = True
            pbCD.Invalidate()
        Catch ex As Exception
            Main.Message.AddWarning(ex.Message)
        End Try
    End Sub

    Private Sub txtAMin_TextChanged(sender As Object, e As EventArgs) Handles txtAMin.TextChanged

    End Sub

    Private Sub txtAMin_LostFocus(sender As Object, e As EventArgs) Handles txtAMin.LostFocus

        ParamAAdjustMin = txtAMin.Text
        SelectedDistrib.ParamA.AdjustMin = ParamAAdjustMin
        'AInfo.XParamMin = ParamAAdjustMin
        'MouseAPosNow.X = AInfo.XPos
        'pbA.Invalidate()
        hsA.Value = 1000 * (ParamAVal - ParamAAdjustMin) / (ParamAAdjustMax - ParamAAdjustMin)

        If cmbX1.SelectedIndex = 1 Then
            txtX1Min.Text = ParamAAdjustMin
            ABInfo.XParamMin = ParamAAdjustMin
            MouseABPosNow.X = ABInfo.XPos
            pbAB.Invalidate()
        End If
        If cmbY1.SelectedIndex = 1 Then
            txtY1Min.Text = ParamAAdjustMin
            ABInfo.YParamMin = ParamAAdjustMin
            MouseABPosNow.Y = ABInfo.YPos
            pbAB.Invalidate()
        End If
        If cmbX2.SelectedIndex = 1 Then
            txtX2Min.Text = ParamAAdjustMin
            CDInfo.XParamMin = ParamAAdjustMin
            MouseCDPosNow.X = CDInfo.XPos
            pbCD.Invalidate()
        End If
        If cmbY2.SelectedIndex = 1 Then
            txtY2Min.Text = ParamAAdjustMin
            CDInfo.YParamMin = ParamAAdjustMin
            MouseCDPosNow.Y = CDInfo.YPos
            pbCD.Invalidate()
        End If
    End Sub

    Private Sub txtAMax_TextChanged(sender As Object, e As EventArgs) Handles txtAMax.TextChanged

    End Sub

    Private Sub txtAMax_LostFocus(sender As Object, e As EventArgs) Handles txtAMax.LostFocus
        ParamAAdjustMax = txtAMax.Text
        SelectedDistrib.ParamA.AdjustMax = ParamAAdjustMax
        'AInfo.XParamMax = ParamAAdjustMax
        'MouseAPosNow.X = AInfo.XPos
        'pbA.Invalidate()
        hsA.Value = 1000 * (ParamAVal - ParamAAdjustMin) / (ParamAAdjustMax - ParamAAdjustMin)

        If cmbX1.SelectedIndex = 1 Then
            txtX1Max.Text = ParamAAdjustMax
            ABInfo.XParamMax = ParamAAdjustMax
            MouseABPosNow.X = ABInfo.XPos
            pbAB.Invalidate()
        End If
        If cmbY1.SelectedIndex = 1 Then
            txtY1Max.Text = ParamAAdjustMax
            ABInfo.YParamMax = ParamAAdjustMax
            MouseABPosNow.Y = ABInfo.YPos
            pbAB.Invalidate()
        End If
        If cmbX2.SelectedIndex = 1 Then
            txtX2Max.Text = ParamAAdjustMax
            CDInfo.XParamMax = ParamAAdjustMax
            MouseCDPosNow.X = CDInfo.XPos
            pbCD.Invalidate()
        End If
        If cmbY2.SelectedIndex = 1 Then
            txtY2Max.Text = ParamAAdjustMax
            CDInfo.YParamMax = ParamAAdjustMax
            MouseCDPosNow.Y = CDInfo.YPos
            pbCD.Invalidate()
        End If
    End Sub

    Private Sub txtBMin_TextChanged(sender As Object, e As EventArgs) Handles txtBMin.TextChanged

    End Sub

    Private Sub txtBMin_LostFocus(sender As Object, e As EventArgs) Handles txtBMin.LostFocus

        ParamBAdjustMin = txtBMin.Text
        SelectedDistrib.ParamB.AdjustMin = ParamBAdjustMin
        'BInfo.XParamMin = ParamBAdjustMin
        'MouseBPosNow.X = BInfo.XPos
        'pbB.Invalidate()
        hsB.Value = 1000 * (ParamBVal - ParamBAdjustMin) / (ParamBAdjustMax - ParamBAdjustMin)

        If cmbX1.SelectedIndex = 2 Then
            txtX1Min.Text = ParamBAdjustMin
            ABInfo.XParamMin = ParamBAdjustMin
            MouseABPosNow.X = ABInfo.XPos
            pbAB.Invalidate()
        End If
        If cmbY1.SelectedIndex = 2 Then
            txtY1Min.Text = ParamBAdjustMin
            ABInfo.YParamMin = ParamBAdjustMin
            MouseABPosNow.Y = ABInfo.YPos
            pbAB.Invalidate()
        End If
        If cmbX2.SelectedIndex = 2 Then
            txtX2Min.Text = ParamBAdjustMin
            CDInfo.XParamMin = ParamBAdjustMin
            MouseCDPosNow.X = CDInfo.XPos
            pbCD.Invalidate()
        End If
        If cmbY2.SelectedIndex = 2 Then
            txtY2Min.Text = ParamBAdjustMin
            CDInfo.YParamMin = ParamBAdjustMin
            MouseCDPosNow.Y = CDInfo.YPos
            pbCD.Invalidate()
        End If
    End Sub

    Private Sub txtBMax_TextChanged(sender As Object, e As EventArgs) Handles txtBMax.TextChanged

    End Sub

    Private Sub txtBMax_LostFocus(sender As Object, e As EventArgs) Handles txtBMax.LostFocus

        ParamBAdjustMax = txtBMax.Text
        SelectedDistrib.ParamB.AdjustMax = ParamBAdjustMax
        'BInfo.XParamMax = ParamBAdjustMax
        'MouseBPosNow.X = BInfo.XPos
        'pbB.Invalidate()
        hsB.Value = 1000 * (ParamBVal - ParamBAdjustMin) / (ParamBAdjustMax - ParamBAdjustMin)

        If cmbX1.SelectedIndex = 2 Then
            txtX1Max.Text = ParamBAdjustMax
            ABInfo.XParamMax = ParamBAdjustMax
            MouseABPosNow.X = ABInfo.XPos
            pbAB.Invalidate()
        End If
        If cmbY1.SelectedIndex = 2 Then
            txtY1Max.Text = ParamBAdjustMax
            ABInfo.YParamMax = ParamBAdjustMax
            MouseABPosNow.Y = ABInfo.YPos
            pbAB.Invalidate()
        End If
        If cmbX2.SelectedIndex = 2 Then
            txtX2Max.Text = ParamBAdjustMax
            CDInfo.XParamMax = ParamBAdjustMax
            MouseCDPosNow.X = CDInfo.XPos
            pbCD.Invalidate()
        End If
        If cmbY2.SelectedIndex = 2 Then
            txtY2Max.Text = ParamBAdjustMax
            CDInfo.YParamMax = ParamBAdjustMax
            MouseCDPosNow.Y = CDInfo.YPos
            pbCD.Invalidate()
        End If
    End Sub

    Private Sub txtCMin_TextChanged(sender As Object, e As EventArgs) Handles txtCMin.TextChanged

    End Sub

    Private Sub txtCMin_LostFocus(sender As Object, e As EventArgs) Handles txtCMin.LostFocus

        ParamCAdjustMin = txtCMin.Text
        SelectedDistrib.ParamC.AdjustMin = ParamCAdjustMin
        'CInfo.XParamMin = ParamCAdjustMin
        'MouseCPosNow.X = CInfo.XPos
        'pbC.Invalidate()
        hsC.Value = 1000 * (ParamCVal - ParamCAdjustMin) / (ParamCAdjustMax - ParamCAdjustMin)

        If cmbX1.SelectedIndex = 3 Then
            txtX1Min.Text = ParamCAdjustMin
            ABInfo.XParamMin = ParamCAdjustMin
            MouseABPosNow.X = ABInfo.XPos
            pbAB.Invalidate()
        End If
        If cmbY1.SelectedIndex = 3 Then
            txtY1Min.Text = ParamCAdjustMin
            ABInfo.YParamMin = ParamCAdjustMin
            MouseABPosNow.Y = ABInfo.YPos
            pbAB.Invalidate()
        End If
        If cmbX2.SelectedIndex = 3 Then
            txtX2Min.Text = ParamCAdjustMin
            CDInfo.XParamMin = ParamCAdjustMin
            MouseCDPosNow.X = CDInfo.XPos
            pbCD.Invalidate()
        End If
        If cmbY2.SelectedIndex = 3 Then
            txtY2Min.Text = ParamCAdjustMin
            CDInfo.YParamMin = ParamCAdjustMin
            MouseCDPosNow.Y = CDInfo.YPos
            pbCD.Invalidate()
        End If
    End Sub

    Private Sub txtCMax_TextChanged(sender As Object, e As EventArgs) Handles txtCMax.TextChanged

    End Sub

    Private Sub txtCMax_LostFocus(sender As Object, e As EventArgs) Handles txtCMax.LostFocus

        ParamCAdjustMax = txtCMax.Text
        SelectedDistrib.ParamC.AdjustMax = ParamCAdjustMax
        'CInfo.XParamMax = ParamCAdjustMax
        'MouseCPosNow.X = CInfo.XPos
        'pbC.Invalidate()
        hsC.Value = 1000 * (ParamCVal - ParamCAdjustMin) / (ParamCAdjustMax - ParamCAdjustMin)

        If cmbX1.SelectedIndex = 3 Then
            txtX1Max.Text = ParamCAdjustMax
            ABInfo.XParamMax = ParamCAdjustMax
            MouseABPosNow.X = ABInfo.XPos
            pbAB.Invalidate()
        End If
        If cmbY1.SelectedIndex = 3 Then
            txtY1Max.Text = ParamCAdjustMax
            ABInfo.YParamMax = ParamCAdjustMax
            MouseABPosNow.Y = ABInfo.YPos
            pbAB.Invalidate()
        End If
        If cmbX2.SelectedIndex = 3 Then
            txtX2Max.Text = ParamCAdjustMax
            CDInfo.XParamMax = ParamCAdjustMax
            MouseCDPosNow.X = CDInfo.XPos
            pbCD.Invalidate()
        End If
        If cmbY2.SelectedIndex = 3 Then
            txtY2Max.Text = ParamCAdjustMax
            CDInfo.YParamMax = ParamCAdjustMax
            MouseCDPosNow.Y = CDInfo.YPos
            pbCD.Invalidate()
        End If
    End Sub

    Private Sub txtDMin_TextChanged(sender As Object, e As EventArgs) Handles txtDMin.TextChanged

    End Sub

    Private Sub txtDMin_LostFocus(sender As Object, e As EventArgs) Handles txtDMin.LostFocus

        ParamDAdjustMin = txtDMin.Text
        SelectedDistrib.ParamD.AdjustMin = ParamDAdjustMin
        'DInfo.XParamMin = ParamDAdjustMin
        'MouseDPosNow.X = DInfo.XPos
        'pbD.Invalidate()
        hsD.Value = 1000 * (ParamDVal - ParamDAdjustMin) / (ParamDAdjustMax - ParamDAdjustMin)

        If cmbX1.SelectedIndex = 4 Then
            txtX1Min.Text = ParamDAdjustMin
            ABInfo.XParamMin = ParamDAdjustMin
            MouseABPosNow.X = ABInfo.XPos
            pbAB.Invalidate()
        End If
        If cmbY1.SelectedIndex = 4 Then
            txtY1Min.Text = ParamDAdjustMin
            ABInfo.YParamMin = ParamDAdjustMin
            MouseABPosNow.Y = ABInfo.YPos
            pbAB.Invalidate()
        End If
        If cmbX2.SelectedIndex = 4 Then
            txtX2Min.Text = ParamDAdjustMin
            CDInfo.XParamMin = ParamDAdjustMin
            MouseCDPosNow.X = CDInfo.XPos
            pbCD.Invalidate()
        End If
        If cmbY2.SelectedIndex = 4 Then
            txtY2Min.Text = ParamDAdjustMin
            CDInfo.YParamMin = ParamDAdjustMin
            MouseCDPosNow.Y = CDInfo.YPos
            pbCD.Invalidate()
        End If
    End Sub

    Private Sub txtDMax_TextChanged(sender As Object, e As EventArgs) Handles txtDMax.TextChanged

    End Sub

    Private Sub txtDMax_LostFocus(sender As Object, e As EventArgs) Handles txtDMax.LostFocus

        ParamDAdjustMax = txtDMax.Text
        SelectedDistrib.ParamD.AdjustMax = ParamDAdjustMax
        'DInfo.XParamMax = ParamDAdjustMax
        'MouseDPosNow.X = DInfo.XPos
        'pbD.Invalidate()
        hsD.Value = 1000 * (ParamDVal - ParamDAdjustMin) / (ParamDAdjustMax - ParamDAdjustMin)

        If cmbX1.SelectedIndex = 4 Then
            txtX1Max.Text = ParamDAdjustMax
            ABInfo.XParamMax = ParamDAdjustMax
            MouseABPosNow.X = ABInfo.XPos
            pbAB.Invalidate()
        End If
        If cmbY1.SelectedIndex = 4 Then
            txtY1Max.Text = ParamDAdjustMax
            ABInfo.YParamMax = ParamDAdjustMax
            MouseABPosNow.Y = ABInfo.YPos
            pbAB.Invalidate()
        End If
        If cmbX2.SelectedIndex = 4 Then
            txtX2Max.Text = ParamDAdjustMax
            CDInfo.XParamMax = ParamDAdjustMax
            MouseCDPosNow.X = CDInfo.XPos
            pbCD.Invalidate()
        End If
        If cmbY2.SelectedIndex = 4 Then
            txtY2Max.Text = ParamDAdjustMax
            CDInfo.YParamMax = ParamDAdjustMax
            MouseCDPosNow.Y = CDInfo.YPos
            pbCD.Invalidate()
        End If
    End Sub

    Private Sub txtEMin_TextChanged(sender As Object, e As EventArgs) Handles txtEMin.TextChanged

    End Sub

    Private Sub txtEMin_LostFocus(sender As Object, e As EventArgs) Handles txtEMin.LostFocus

        ParamEAdjustMin = txtEMin.Text
        SelectedDistrib.ParamE.AdjustMin = ParamEAdjustMin
        'EInfo.XParamMin = ParamEAdjustMin
        'MouseEPosNow.X = EInfo.XPos
        'pbE.Invalidate()
        hsE.Value = 1000 * (ParamEVal - ParamEAdjustMin) / (ParamEAdjustMax - ParamEAdjustMin)

        If cmbX1.SelectedIndex = 5 Then
            txtX1Min.Text = ParamEAdjustMin
            ABInfo.XParamMin = ParamEAdjustMin
            MouseABPosNow.X = ABInfo.XPos
            pbAB.Invalidate()
        End If
        If cmbY1.SelectedIndex = 5 Then
            txtY1Min.Text = ParamEAdjustMin
            ABInfo.YParamMin = ParamEAdjustMin
            MouseABPosNow.Y = ABInfo.YPos
            pbAB.Invalidate()
        End If
        If cmbX2.SelectedIndex = 5 Then
            txtX2Min.Text = ParamEAdjustMin
            CDInfo.XParamMin = ParamEAdjustMin
            MouseCDPosNow.X = CDInfo.XPos
            pbCD.Invalidate()
        End If
        If cmbY2.SelectedIndex = 5 Then
            txtY2Min.Text = ParamEAdjustMin
            CDInfo.YParamMin = ParamEAdjustMin
            MouseCDPosNow.Y = CDInfo.YPos
            pbCD.Invalidate()
        End If
    End Sub

    Private Sub txtEMax_TextChanged(sender As Object, e As EventArgs) Handles txtEMax.TextChanged

    End Sub

    Private Sub txtEMax_LostFocus(sender As Object, e As EventArgs) Handles txtEMax.LostFocus

        ParamEAdjustMax = txtEMax.Text
        SelectedDistrib.ParamE.AdjustMax = ParamEAdjustMax
        'EInfo.XParamMax = ParamEAdjustMax
        'MouseEPosNow.X = EInfo.XPos
        'pbE.Invalidate()
        hsE.Value = 1000 * (ParamEVal - ParamEAdjustMin) / (ParamEAdjustMax - ParamEAdjustMin)

        If cmbX1.SelectedIndex = 5 Then
            txtX1Max.Text = ParamEAdjustMax
            ABInfo.XParamMax = ParamEAdjustMax
            MouseABPosNow.X = ABInfo.XPos
            pbAB.Invalidate()
        End If
        If cmbY1.SelectedIndex = 5 Then
            txtY1Max.Text = ParamEAdjustMax
            ABInfo.YParamMax = ParamEAdjustMax
            MouseABPosNow.Y = ABInfo.YPos
            pbAB.Invalidate()
        End If
        If cmbX2.SelectedIndex = 5 Then
            txtX2Max.Text = ParamEAdjustMax
            CDInfo.XParamMax = ParamEAdjustMax
            MouseCDPosNow.X = CDInfo.XPos
            pbCD.Invalidate()
        End If
        If cmbY2.SelectedIndex = 5 Then
            txtY2Max.Text = ParamEAdjustMax
            CDInfo.YParamMax = ParamEAdjustMax
            MouseCDPosNow.Y = CDInfo.YPos
            pbCD.Invalidate()
        End If
    End Sub

    Private Sub frmAdjust_ContextMenuStripChanged(sender As Object, e As EventArgs) Handles Me.ContextMenuStripChanged

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDataValues.CellContentClick

    End Sub

    Private Sub ContextMenuStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles ContextMenuStrip1.ItemClicked

        If e.ClickedItem.ToString = "Paste" Then
            pastefromclipboardtodatagridview(dgvDataValues)
        End If

    End Sub

    Sub pastefromclipboardtodatagridview(ByVal dgv As DataGridView)
        Try


            Dim rowSplitter As Char() = {vbCr, vbLf}
            Dim columnSplitter As Char() = {vbTab}

            'If dgv.RowCount = 0 Then
            '    dgv.Rows.Add()
            '    dgv.Rows(0).Selected = True
            'End If

            If dgv.SelectedRows.Count = 0 Then
                dgv.Rows(0).Selected = True
            End If

            'get the text from clipboard

            Dim dataInClipboard As IDataObject = Clipboard.GetDataObject()
            Dim stringInClipboard As String = CStr(dataInClipboard.GetData(DataFormats.Text))

            'split it into lines
            Dim rowsInClipboard As String() = stringInClipboard.Split(rowSplitter, StringSplitOptions.RemoveEmptyEntries)

            'get the row and column of selected cell in grid
            Dim r As Integer = dgv.SelectedCells(0).RowIndex
            Dim c As Integer = dgv.SelectedCells(0).ColumnIndex
            Dim I As Integer
            Dim RowsToAdd As Integer

            'add rows into grid to fit clipboard lines
            If (dgv.Rows.Count < (r + rowsInClipboard.Length)) Then
                'dgv.Rows.Add(r + rowsInClipboard.Length - dgv.Rows.Count)
                RowsToAdd = r + rowsInClipboard.Length - dgv.Rows.Count

                'For I = 1 To RowsToAdd
                For I = 0 To RowsToAdd
                    SelectedDistrib.ParamEst.Data.Tables("Samples").Rows.Add({0})
                Next

                'Add an extra row at the end.
                'dgv.Rows.Add()
            End If

            ' loop through the lines, split them into cells and place the values in the corresponding cell.
            Dim iRow As Integer = 0
            While iRow < rowsInClipboard.Length
                'split row into cell values
                Dim valuesInRow As String() = rowsInClipboard(iRow).Split(columnSplitter)
                'cycle through cell values
                Dim iCol As Integer = 0
                While iCol < valuesInRow.Length
                    'assign cell value, only if it within columns of the grid
                    If (dgv.ColumnCount - 1 >= c + iCol) Then
                        'dgv.Rows(r + iRow).Cells(c + iCol).Value = valuesInRow(iCol)
                        SelectedDistrib.ParamEst.Data.Tables("Samples").Rows(r + iRow).Item(c + iCol) = valuesInRow(iCol)
                    End If
                    iCol += 1
                End While
                iRow += 1
            End While

        Catch ex As Exception
            Main.Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub


    Private Sub CopyDataTableToClipboard(ByRef myTable As DataTable, InclHeaders As Boolean)
        Dim strCopy As String = ""

        If InclHeaders Then 'Include the column headers in the copied data.
            For Each myCol As DataColumn In myTable.Columns
                strCopy &= myCol.Caption & Chr(Keys.Tab)
            Next
            strCopy = strCopy.Substring(0, strCopy.Length - 1)
            strCopy &= Environment.NewLine    'Get rows
        End If

        For Each myRow As DataRow In myTable.Rows
            For Each myItem In myRow.ItemArray
                If myItem IsNot Nothing Then
                    strCopy &= myItem.ToString
                End If
                strCopy &= Chr(Keys.Tab)
            Next
            strCopy = strCopy.Substring(0, strCopy.Length - 1)
            strCopy &= Environment.NewLine
        Next

        Dim DataObj As New DataObject
        DataObj.SetText(strCopy)
        Clipboard.SetDataObject(DataObj, True)
    End Sub

    Private Sub btnCopyDataValues_Click(sender As Object, e As EventArgs) Handles btnCopyDataValues.Click
        CopyDataTableToClipboard(SelectedDistrib.ParamEst.Data.Tables("Samples"), chkInclHeaders.Checked)
    End Sub

    'Private Sub btnShowRange_Click(sender As Object, e As EventArgs) Handles btnShowRange.Click
    '    'Show the selection range

    '    'txtFromCol.Text = dgvDataValues.SelectedCells(0).ColumnIndex
    '    'txtFromRow.Text = dgvDataValues.SelectedCells(0).RowIndex
    '    'Dim NSelCells As Integer = dgvDataValues.SelectedCells.Count
    '    'txtToCol.Text = dgvDataValues.SelectedCells(NSelCells - 1).ColumnIndex
    '    'txtToRow.Text = dgvDataValues.SelectedCells(NSelCells - 1).RowIndex

    '    Dim ThisRow As Integer
    '    Dim ThisCol As Integer
    '    Dim FirstRow As Integer = dgvDataValues.SelectedCells(0).RowIndex
    '    Dim FirstCol As Integer = dgvDataValues.SelectedCells(0).ColumnIndex
    '    Dim LastRow As Integer = FirstRow
    '    Dim LastCol As Integer = FirstCol

    '    For Each Item In dgvDataValues.SelectedCells
    '        ThisRow = Item.RowIndex
    '        ThisCol = Item.ColumnIndex
    '        If ThisRow < FirstRow Then FirstRow = ThisRow
    '        If ThisRow > LastRow Then LastRow = ThisRow
    '        If ThisCol < FirstCol Then FirstCol = ThisCol
    '        If ThisCol > LastCol Then LastCol = ThisCol
    '    Next

    '    txtFromCol.Text = FirstCol
    '    txtFromRow.Text = FirstRow
    '    txtToCol.Text = LastCol
    '    txtToRow.Text = LastRow

    'End Sub

    'Private Sub btnSaveVals_Click(sender As Object, e As EventArgs) Handles btnSaveVals.Click
    '    'Save the values in SelectedDistrib.ParamEst.Data.Tables("Samples")

    '    '-----------------------------------------------------------------------------
    '    'Dim XmlData As New IO.MemoryStream
    '    'SelectedDistrib.ParamEst.Data.Tables("Samples").WriteXml(XmlData)
    '    'Main.Project.SaveData("Test.xml", XmlData)

    '    '-----------------------------------------------------------------------------
    '    'Dim XDoc = <?xml version="1.0" encoding="utf-8"?>
    '    '           <!---->
    '    '           <Samples>

    '    '           </Samples>



    '    '-----------------------------------------------------------------------------
    '    'Dim decl As New XDeclaration("1.0", "utf-8", "yes")
    '    'Dim doc As New XDocument(decl, Nothing) 'Create an XDocument 
    '    'Dim Samples As New XElement("Samples")

    '    'Dim ValsPerLine As Integer = 10 'Write 10 values per line
    '    'Dim ValString As String = "" 'String containing one line of values
    '    'Dim I As Integer = 0 'Row index
    '    'Dim myTable As DataTable = SelectedDistrib.ParamEst.Data.Tables("Samples")
    '    ''Dim NRows As Integer = SelectedDistrib.ParamEst.Data.Tables("Samples").Rows.Count

    '    'For Each myRow As DataRow In myTable.Rows
    '    '    I += 1

    '    '    If I Mod ValsPerLine = 0 Then
    '    '        ValString &= myRow.Item(0)
    '    '        Dim Value As New XElement("Values", ValString)
    '    '        Samples.Add(Value)
    '    '        ValString = ""
    '    '    Else
    '    '        ValString &= myRow.Item(0) & ", "
    '    '    End If
    '    'Next

    '    'If ValString = "" Then

    '    'Else
    '    '    Dim Value As New XElement("Values", ValString.Remove(ValString.Length - 2, 2))
    '    '    Samples.Add(Value)
    '    'End If

    '    'doc.Add(Samples)

    '    'Main.Project.SaveXmlData("Test2", doc)

    '    '--------------------------------------------------------------------------------------
    '    Dim decl As New XDeclaration("1.0", "utf-8", "yes")
    '    Dim doc As New XDocument(decl, Nothing) 'Create an XDocument 
    '    Dim Samples As New XElement("Samples")

    '    Dim ValsPerLine As Integer = 10 'Write 10 values per line
    '    Dim ValString As String = "" 'String containing one line of values
    '    Dim I As Integer = 0 'Row index
    '    Dim myTable As DataTable = SelectedDistrib.ParamEst.Data.Tables("Samples")

    '    For Each myRow As DataRow In myTable.Rows
    '        I += 1
    '        If I Mod ValsPerLine = 0 Then
    '            'ValString &= myRow.Item(0) & vbCrLf
    '            ValString &= myRow.Item(0) & " " & vbLf
    '        Else
    '            ValString &= myRow.Item(0) & " "
    '        End If
    '    Next

    '    Dim Values As New XCData(ValString)
    '    Samples.Add(Values)
    '    doc.Add(Samples)

    '    Main.Project.SaveXmlData("Test3.xml", doc)

    'End Sub

    'Private Sub btnReadVals_Click(sender As Object, e As EventArgs) Handles btnReadVals.Click
    '    'Read the sample values:

    '    Dim SamplesFile As System.Xml.Linq.XDocument
    '    Main.Project.ReadXmlSettings("Test3.xml", SamplesFile)

    '    Dim SamplesString As String = SamplesFile.<Samples>.Value

    '    'Main.Message.Add("Samples string: " & vbCrLf & SamplesString & vbCrLf & vbCrLf)

    '    'Now get each value separately:
    '    'Dim Split As String() = SamplesString.Replace(vbCrLf, "").Split(" ")
    '    'Dim Split As String() = SamplesString.Replace(vbCr, "").Replace(vbLf, " ").Split(" ") 'This avoids issues with lines ending with Lf instead of CrLf
    '    'Dim Split As String() = SamplesString.Replace(vbCr, "").Replace(vbLf, " ").Split(" ", StringSplitOptions.RemoveEmptyEntries) 'This avoids issues with lines ending with Lf instead of CrLf
    '    Dim Split As String() = SamplesString.Replace(vbCr, "").Replace(vbLf, " ").Split(New Char() {" "}, StringSplitOptions.RemoveEmptyEntries)

    '    Main.Message.Add(Split.Length & " Values read." & vbCrLf & vbCrLf)

    '    Dim myTable As DataTable = SelectedDistrib.ParamEst.Data.Tables("Samples")
    '    For Each value In Split
    '        Main.Message.Add(value & vbCrLf)
    '        If value.Trim <> "" Then myTable.Rows.Add(value)
    '    Next

    'End Sub

    'NOTE: This code is now contained within the DistributionModel class. (In the clsParamEst sub class.)
    'Private Sub CopySamplesToModelFitting()
    '    'Copy the Samples to the Model_Fitting table

    '    SelectedDistrib.ParamEst.Data.Tables("Model_Fitting").Rows.Clear()

    '    For Each Row As DataRow In SelectedDistrib.ParamEst.Data.Tables("Samples").Rows
    '        SelectedDistrib.ParamEst.Data.Tables("Model_Fitting").Rows.Add(Row.Item(0))
    '    Next

    '    'Sort the Samples column:
    '    SelectedDistrib.ParamEst.Data.Tables("Model_Fitting").DefaultView.Sort = "Value" & " ASC"

    '    'Generate the eCDF and Reverse_eCDF values:
    '    Dim NValues As Integer = SelectedDistrib.ParamEst.Data.Tables("Model_Fitting").Rows.Count
    '    Dim I As Integer
    '    For I = 1 To NValues
    '        SelectedDistrib.ParamEst.Data.Tables("Model_Fitting").DefaultView.Item(I - 1).Item("eCDF") = I / NValues 'Calculate each eCDF value (empirical CDF) (for continuous distributions)
    '        SelectedDistrib.ParamEst.Data.Tables("Model_Fitting").DefaultView.Item(I - 1).Item("Reverse_eCDF") = 1 - (I / NValues) 'Calculate each Reverse_eCDF value (for continuous distributions)
    '    Next

    'End Sub

    Private Sub btnCopySamples_Click(sender As Object, e As EventArgs) Handles btnCopySamples.Click
        'Copy the Sample values to the Model_Fitting table:
        SelectedDistrib.ParamEst.CopySamplesToModelFitting()
    End Sub

    Private Sub UpdateModelData()
        'Update the Model_CDF, Model_Rev_CDF, Model_Prob_Dens and Ln_Model_Prob_Dens column in the Model_Fitting table.

        dgvCalculations.DataSource = vbNull 'This stops the datagridview updating while the tabel is recalculating

        Dim NValues As Integer = SelectedDistrib.ParamEst.Data.Tables("Model_Fitting").Rows.Count
        Dim I As Integer
        Dim myTable As DataTable = SelectedDistrib.ParamEst.Data.Tables("Model_Fitting")

        Select Case SelectedDistrib.Name
            Case "Beta"
                Dim ParamAValue As Double = SelectedDistrib.ParamA.Value
                Dim ParamBValue As Double = SelectedDistrib.ParamB.Value
                For I = 1 To NValues
                    myTable.DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.Beta.CDF(ParamAValue, ParamBValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - myTable.DefaultView.Item(I - 1).Item("Model_CDF")
                    myTable.DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.Beta.PDF(ParamAValue, ParamBValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Ln_Model_Prob_Dens") = MathNet.Numerics.Distributions.Beta.PDFLn(ParamAValue, ParamBValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                Next

            Case "Beta Scaled"
                Dim ParamAValue As Double = SelectedDistrib.ParamA.Value
                Dim ParamBValue As Double = SelectedDistrib.ParamB.Value
                Dim ParamCValue As Double = SelectedDistrib.ParamC.Value
                Dim ParamDValue As Double = SelectedDistrib.ParamD.Value
                For I = 1 To NValues
                    myTable.DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.BetaScaled.CDF(ParamAValue, ParamBValue, ParamCValue, ParamDValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - myTable.DefaultView.Item(I - 1).Item("Model_CDF")
                    myTable.DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.BetaScaled.PDF(ParamAValue, ParamBValue, ParamCValue, ParamDValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Ln_Model_Prob_Dens") = MathNet.Numerics.Distributions.BetaScaled.PDFLn(ParamAValue, ParamBValue, ParamCValue, ParamDValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                Next

            Case "Cauchy"
                Dim ParamAValue As Double = SelectedDistrib.ParamA.Value
                Dim ParamBValue As Double = SelectedDistrib.ParamB.Value
                For I = 1 To NValues
                    myTable.DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.Cauchy.CDF(ParamAValue, ParamBValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - myTable.DefaultView.Item(I - 1).Item("Model_CDF")
                    myTable.DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.Cauchy.PDF(ParamAValue, ParamBValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Ln_Model_Prob_Dens") = MathNet.Numerics.Distributions.Cauchy.PDFLn(ParamAValue, ParamBValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                Next

            Case "Chi Squared"
                Dim ParamAValue As Double = SelectedDistrib.ParamA.Value
                For I = 1 To NValues
                    myTable.DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.ChiSquared.CDF(ParamAValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - myTable.DefaultView.Item(I - 1).Item("Model_CDF")
                    myTable.DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.ChiSquared.PDF(ParamAValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Ln_Model_Prob_Dens") = MathNet.Numerics.Distributions.ChiSquared.PDFLn(ParamAValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                Next

            Case "Continuous Uniform"
                Dim ParamAValue As Double = SelectedDistrib.ParamA.Value
                Dim ParamBValue As Double = SelectedDistrib.ParamB.Value
                For I = 1 To NValues
                    myTable.DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.ContinuousUniform.CDF(ParamAValue, ParamBValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - myTable.DefaultView.Item(I - 1).Item("Model_CDF")
                    myTable.DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.ContinuousUniform.PDF(ParamAValue, ParamBValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Ln_Model_Prob_Dens") = MathNet.Numerics.Distributions.ContinuousUniform.PDFLn(ParamAValue, ParamBValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                Next

            Case "Exponential"
                Dim ParamAValue As Double = SelectedDistrib.ParamA.Value
                For I = 1 To NValues
                    myTable.DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.Exponential.CDF(ParamAValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - myTable.DefaultView.Item(I - 1).Item("Model_CDF")
                    myTable.DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.Exponential.PDF(ParamAValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Ln_Model_Prob_Dens") = MathNet.Numerics.Distributions.Exponential.PDFLn(ParamAValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                Next

            Case "Fisher-Snedecor"
                Dim ParamAValue As Double = SelectedDistrib.ParamA.Value
                Dim ParamBValue As Double = SelectedDistrib.ParamB.Value
                For I = 1 To NValues
                    myTable.DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.FisherSnedecor.CDF(ParamAValue, ParamBValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - myTable.DefaultView.Item(I - 1).Item("Model_CDF")
                    myTable.DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.FisherSnedecor.PDF(ParamAValue, ParamBValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Ln_Model_Prob_Dens") = MathNet.Numerics.Distributions.FisherSnedecor.PDFLn(ParamAValue, ParamBValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                Next

            Case "Gamma"
                Dim ParamAValue As Double = SelectedDistrib.ParamA.Value
                Dim ParamBValue As Double = SelectedDistrib.ParamB.Value
                For I = 1 To NValues
                    myTable.DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.Gamma.CDF(ParamAValue, ParamBValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - myTable.DefaultView.Item(I - 1).Item("Model_CDF")
                    myTable.DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.Gamma.PDF(ParamAValue, ParamBValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Ln_Model_Prob_Dens") = MathNet.Numerics.Distributions.Gamma.PDFLn(ParamAValue, ParamBValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                Next

            Case "Inverse Gaussian"
                Dim ParamAValue As Double = SelectedDistrib.ParamA.Value
                Dim ParamBValue As Double = SelectedDistrib.ParamB.Value
                For I = 1 To NValues
                    myTable.DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.InverseGaussian.CDF(ParamAValue, ParamBValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - myTable.DefaultView.Item(I - 1).Item("Model_CDF")
                    myTable.DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.InverseGaussian.PDF(ParamAValue, ParamBValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Ln_Model_Prob_Dens") = MathNet.Numerics.Distributions.InverseGaussian.PDFLn(ParamAValue, ParamBValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                Next

            Case "Log Normal"
                Dim ParamAValue As Double = SelectedDistrib.ParamA.Value
                Dim ParamBValue As Double = SelectedDistrib.ParamB.Value
                For I = 1 To NValues
                    myTable.DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.LogNormal.CDF(ParamAValue, ParamBValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - myTable.DefaultView.Item(I - 1).Item("Model_CDF")
                    myTable.DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.LogNormal.PDF(ParamAValue, ParamBValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Ln_Model_Prob_Dens") = MathNet.Numerics.Distributions.LogNormal.PDFLn(ParamAValue, ParamBValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                Next

            Case "Normal"
                Dim ParamAValue As Double = SelectedDistrib.ParamA.Value
                Dim ParamBValue As Double = SelectedDistrib.ParamB.Value
                For I = 1 To NValues
                    myTable.DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.Normal.CDF(ParamAValue, ParamBValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - myTable.DefaultView.Item(I - 1).Item("Model_CDF")
                    myTable.DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.Normal.PDF(ParamAValue, ParamBValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Ln_Model_Prob_Dens") = MathNet.Numerics.Distributions.Normal.PDFLn(ParamAValue, ParamBValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                Next

            Case "Pareto"
                Dim ParamAValue As Double = SelectedDistrib.ParamA.Value
                Dim ParamBValue As Double = SelectedDistrib.ParamB.Value
                For I = 1 To NValues
                    myTable.DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.Pareto.CDF(ParamAValue, ParamBValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - myTable.DefaultView.Item(I - 1).Item("Model_CDF")
                    myTable.DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.Pareto.PDF(ParamAValue, ParamBValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Ln_Model_Prob_Dens") = MathNet.Numerics.Distributions.Pareto.PDFLn(ParamAValue, ParamBValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                Next

            Case "Rayleigh"
                Dim ParamAValue As Double = SelectedDistrib.ParamA.Value
                For I = 1 To NValues
                    myTable.DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.Rayleigh.CDF(ParamAValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - myTable.DefaultView.Item(I - 1).Item("Model_CDF")
                    myTable.DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.Rayleigh.PDF(ParamAValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Ln_Model_Prob_Dens") = MathNet.Numerics.Distributions.Rayleigh.PDFLn(ParamAValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                Next

            Case "Skewed Generalized Error"
                Dim ParamAValue As Double = SelectedDistrib.ParamA.Value
                Dim ParamBValue As Double = SelectedDistrib.ParamB.Value
                Dim ParamCValue As Double = SelectedDistrib.ParamC.Value
                Dim ParamDValue As Double = SelectedDistrib.ParamD.Value
                For I = 1 To NValues
                    myTable.DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.SkewedGeneralizedError.CDF(ParamAValue, ParamBValue, ParamCValue, ParamDValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - myTable.DefaultView.Item(I - 1).Item("Model_CDF")
                    myTable.DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.SkewedGeneralizedError.PDF(ParamAValue, ParamBValue, ParamCValue, ParamDValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Ln_Model_Prob_Dens") = MathNet.Numerics.Distributions.SkewedGeneralizedError.PDFLn(ParamAValue, ParamBValue, ParamCValue, ParamDValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                Next

            Case "Skewed Generalized T"
                Dim ParamAValue As Double = SelectedDistrib.ParamA.Value
                Dim ParamBValue As Double = SelectedDistrib.ParamB.Value
                Dim ParamCValue As Double = SelectedDistrib.ParamC.Value
                Dim ParamDValue As Double = SelectedDistrib.ParamD.Value
                Dim ParamEValue As Double = SelectedDistrib.ParamE.Value
                For I = 1 To NValues
                    myTable.DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.SkewedGeneralizedT.CDF(ParamAValue, ParamBValue, ParamCValue, ParamDValue, ParamEValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - myTable.DefaultView.Item(I - 1).Item("Model_CDF")
                    myTable.DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.SkewedGeneralizedT.PDF(ParamAValue, ParamBValue, ParamCValue, ParamDValue, ParamEValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Ln_Model_Prob_Dens") = MathNet.Numerics.Distributions.SkewedGeneralizedT.PDFLn(ParamAValue, ParamBValue, ParamCValue, ParamDValue, ParamEValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                Next

            Case "Student's T"
                Dim ParamAValue As Double = SelectedDistrib.ParamA.Value
                Dim ParamBValue As Double = SelectedDistrib.ParamB.Value
                Dim ParamCValue As Double = SelectedDistrib.ParamC.Value
                For I = 1 To NValues
                    myTable.DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.StudentT.CDF(ParamAValue, ParamBValue, ParamCValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - myTable.DefaultView.Item(I - 1).Item("Model_CDF")
                    myTable.DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.StudentT.PDF(ParamAValue, ParamBValue, ParamCValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Ln_Model_Prob_Dens") = MathNet.Numerics.Distributions.StudentT.PDFLn(ParamAValue, ParamBValue, ParamCValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                Next

            Case "Triangular"
                Dim ParamAValue As Double = SelectedDistrib.ParamA.Value
                Dim ParamBValue As Double = SelectedDistrib.ParamB.Value
                Dim ParamCValue As Double = SelectedDistrib.ParamC.Value
                For I = 1 To NValues
                    myTable.DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.Triangular.CDF(ParamAValue, ParamBValue, ParamCValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - myTable.DefaultView.Item(I - 1).Item("Model_CDF")
                    myTable.DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.Triangular.PDF(ParamAValue, ParamBValue, ParamCValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Ln_Model_Prob_Dens") = MathNet.Numerics.Distributions.Triangular.PDFLn(ParamAValue, ParamBValue, ParamCValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                Next

            Case "Truncated Pareto"
                Dim ParamAValue As Double = SelectedDistrib.ParamA.Value
                Dim ParamBValue As Double = SelectedDistrib.ParamB.Value
                Dim ParamCValue As Double = SelectedDistrib.ParamC.Value
                For I = 1 To NValues
                    myTable.DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.TruncatedPareto.CDF(ParamAValue, ParamBValue, ParamCValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - myTable.DefaultView.Item(I - 1).Item("Model_CDF")
                    myTable.DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.TruncatedPareto.PDF(ParamAValue, ParamBValue, ParamCValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Ln_Model_Prob_Dens") = MathNet.Numerics.Distributions.TruncatedPareto.PDFLn(ParamAValue, ParamBValue, ParamCValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                Next

                'NOTE: The discrete distributions are calculated at integer X values. The following code assimes the "Value" cells contain suitable values.
            Case "Bernoulli" 'Discrete
                Dim ParamAValue As Double = SelectedDistrib.ParamA.Value
                For I = 1 To NValues
                    myTable.DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.Bernoulli.CDF(ParamAValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - myTable.DefaultView.Item(I - 1).Item("Model_CDF")
                    myTable.DefaultView.Item(I - 1).Item("Model_Prob_Mass") = MathNet.Numerics.Distributions.Bernoulli.PMF(ParamAValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Ln_Model_Prob_Mass") = MathNet.Numerics.Distributions.Bernoulli.PMFLn(ParamAValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                Next

            Case "Binomial" 'Discrete
                Dim ParamAValue As Double = SelectedDistrib.ParamA.Value
                Dim ParamBValue As Double = SelectedDistrib.ParamB.Value
                For I = 1 To NValues
                    myTable.DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.Binomial.CDF(ParamAValue, ParamBValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - myTable.DefaultView.Item(I - 1).Item("Model_CDF")
                    myTable.DefaultView.Item(I - 1).Item("Model_Prob_Mass") = MathNet.Numerics.Distributions.Binomial.PMF(ParamAValue, ParamBValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Ln_Model_Prob_Mass") = MathNet.Numerics.Distributions.Binomial.PMFLn(ParamAValue, ParamBValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                Next

            Case "Categorical" 'Discrete

            Case "Conway-Maxwell-Poisson" 'Discrete
                Dim ParamAValue As Double = SelectedDistrib.ParamA.Value
                Dim ParamBValue As Double = SelectedDistrib.ParamB.Value
                For I = 1 To NValues
                    myTable.DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.ConwayMaxwellPoisson.CDF(ParamAValue, ParamBValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - myTable.DefaultView.Item(I - 1).Item("Model_CDF")
                    myTable.DefaultView.Item(I - 1).Item("Model_Prob_Mass") = MathNet.Numerics.Distributions.ConwayMaxwellPoisson.PMF(ParamAValue, ParamBValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Ln_Model_Prob_Mass") = MathNet.Numerics.Distributions.ConwayMaxwellPoisson.PMFLn(ParamAValue, ParamBValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                Next

            Case "Discrete Uniform" 'Discrete
                Dim ParamAValue As Double = SelectedDistrib.ParamA.Value
                Dim ParamBValue As Double = SelectedDistrib.ParamB.Value
                For I = 1 To NValues
                    myTable.DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.DiscreteUniform.CDF(ParamAValue, ParamBValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - myTable.DefaultView.Item(I - 1).Item("Model_CDF")
                    myTable.DefaultView.Item(I - 1).Item("Model_Prob_Mass") = MathNet.Numerics.Distributions.DiscreteUniform.PMF(ParamAValue, ParamBValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Ln_Model_Prob_Mass") = MathNet.Numerics.Distributions.DiscreteUniform.PMFLn(ParamAValue, ParamBValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                Next

            Case "Geometric" 'Discrete
                Dim ParamAValue As Double = SelectedDistrib.ParamA.Value
                For I = 1 To NValues
                    myTable.DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.Geometric.CDF(ParamAValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - myTable.DefaultView.Item(I - 1).Item("Model_CDF")
                    myTable.DefaultView.Item(I - 1).Item("Model_Prob_Mass") = MathNet.Numerics.Distributions.Geometric.PMF(ParamAValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Ln_Model_Prob_Mass") = MathNet.Numerics.Distributions.Geometric.PMFLn(ParamAValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                Next

            Case "Hypergeometric" 'Discrete
                Dim ParamAValue As Double = SelectedDistrib.ParamA.Value
                Dim ParamBValue As Double = SelectedDistrib.ParamB.Value
                Dim ParamCValue As Double = SelectedDistrib.ParamC.Value
                For I = 1 To NValues
                    myTable.DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.Hypergeometric.CDF(ParamAValue, ParamBValue, ParamCValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - myTable.DefaultView.Item(I - 1).Item("Model_CDF")
                    myTable.DefaultView.Item(I - 1).Item("Model_Prob_Mass") = MathNet.Numerics.Distributions.Hypergeometric.PMF(ParamAValue, ParamBValue, ParamCValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Ln_Model_Prob_Mass") = MathNet.Numerics.Distributions.Hypergeometric.PMFLn(ParamAValue, ParamBValue, ParamCValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                Next

            Case "Negative Binomial" 'Discrete
                Dim ParamAValue As Double = SelectedDistrib.ParamA.Value
                Dim ParamBValue As Double = SelectedDistrib.ParamB.Value
                For I = 1 To NValues
                    myTable.DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.NegativeBinomial.CDF(ParamAValue, ParamBValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - myTable.DefaultView.Item(I - 1).Item("Model_CDF")
                    myTable.DefaultView.Item(I - 1).Item("Model_Prob_Mass") = MathNet.Numerics.Distributions.NegativeBinomial.PMF(ParamAValue, ParamBValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Ln_Model_Prob_Mass") = MathNet.Numerics.Distributions.NegativeBinomial.PMFLn(ParamAValue, ParamBValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                Next

            Case "Poisson" 'Discrete
                Dim ParamAValue As Double = SelectedDistrib.ParamA.Value
                For I = 1 To NValues
                    myTable.DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.Poisson.CDF(ParamAValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - myTable.DefaultView.Item(I - 1).Item("Model_CDF")
                    myTable.DefaultView.Item(I - 1).Item("Model_Prob_Mass") = MathNet.Numerics.Distributions.Poisson.PMF(ParamAValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Ln_Model_Prob_Mass") = MathNet.Numerics.Distributions.Poisson.PMFLn(ParamAValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                Next

            Case "Zipf" 'Discrete
                Dim ParamAValue As Double = SelectedDistrib.ParamA.Value
                Dim ParamBValue As Double = SelectedDistrib.ParamB.Value
                For I = 1 To NValues
                    myTable.DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.Zipf.CDF(ParamAValue, ParamBValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - myTable.DefaultView.Item(I - 1).Item("Model_CDF")
                    myTable.DefaultView.Item(I - 1).Item("Model_Prob_Mass") = MathNet.Numerics.Distributions.Zipf.PMF(ParamAValue, ParamBValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                    myTable.DefaultView.Item(I - 1).Item("Ln_Model_Prob_Mass") = MathNet.Numerics.Distributions.Zipf.PMFLn(ParamAValue, ParamBValue, myTable.DefaultView.Item(I - 1).Item("Value"))
                Next

            Case ""
                'No model

            Case Else
                Main.Message.AddWarning("Unknown model distribution: " & SelectedDistrib.Name & vbCrLf)

        End Select

        Dim TableName As String = cmbTableName.SelectedItem.ToString.Trim
        If TableName = "" Then
            'No table selected for display
        Else
            dgvCalculations.DataSource = SelectedDistrib.ParamEst.Data.Tables(TableName) 'Display the updated data
            dgvCalculations.Update()
        End If

        Try
            LogLikelihood = myTable.Compute("SUM(Ln_Model_Prob_Dens)", "")
        Catch ex As Exception
            Main.Message.AddWarning("Error calculating Log Likelihood:" & vbCrLf & ex.Message & vbCrLf)
        End Try

    End Sub

    Private Sub btnUpdateModelData_Click(sender As Object, e As EventArgs) Handles btnUpdateModelData.Click
        UpdateModelData()
    End Sub

    Private Sub txtSampSetName_TextChanged(sender As Object, e As EventArgs) Handles txtSampSetName.TextChanged

    End Sub

    Private Sub txtSampSetName_LostFocus(sender As Object, e As EventArgs) Handles txtSampSetName.LostFocus
        SelectedDistrib.ParamEst.SampleSetName = txtSampSetName.Text.Trim
    End Sub

    Private Sub txtSampSetLabel_TextChanged(sender As Object, e As EventArgs) Handles txtSampSetLabel.TextChanged

    End Sub

    Private Sub txtSampSetLabel_LostFocus(sender As Object, e As EventArgs) Handles txtSampSetLabel.LostFocus
        SelectedDistrib.ParamEst.SampleSetLabel = txtSampSetLabel.Text.Trim
    End Sub

    Private Sub txtSampSetUnits_TextChanged(sender As Object, e As EventArgs) Handles txtSampSetUnits.TextChanged

    End Sub

    Private Sub txtSampSetUnits_LostFocus(sender As Object, e As EventArgs) Handles txtSampSetUnits.LostFocus
        SelectedDistrib.ParamEst.SampleSetUnits = txtSampSetUnits.Text.Trim
    End Sub

    Private Sub txtSampSetDescr_TextChanged(sender As Object, e As EventArgs) Handles txtSampSetDescr.TextChanged

    End Sub

    Private Sub txtSampSetDescr_LostFocus(sender As Object, e As EventArgs) Handles txtSampSetDescr.LostFocus
        SelectedDistrib.ParamEst.SampleSetDescription = txtSampSetDescr.Text.Trim
    End Sub

    Private Sub btnFindMatch_Click(sender As Object, e As EventArgs) Handles btnFindMatch.Click
        'Find Distribution parameters that match the samples.

        If SelectedDistrib.ParamEst.Data.Tables("Samples").Rows.Count > 0 Then
            'SelectedDistrib.ParamEst.RefineParams(0.001)
            'ParamAVal = SelectedDistrib.ParamA.Value
            'If NParams > 1 Then
            '    ParamBVal = SelectedDistrib.ParamB.Value
            '    If NParams > 2 Then
            '        ParamCVal = SelectedDistrib.ParamC.Value
            '        If NParams > 3 Then
            '            ParamDVal = SelectedDistrib.ParamD.Value
            '            If NParams > 4 Then
            '                ParamEVal = SelectedDistrib.ParamE.Value
            '            End If
            '        End If
            '    End If
            'End If
            'ApplyRefinedVals()

            SelectedDistrib.ParamEst.RefineParams(0.001, ParamAVal, ParamBVal, ParamCVal, ParamDVal, ParamEVal) 'This will update ParamAVal etc.
            SelectedDistrib.ParamA.Value = ParamAVal
            If NParams > 1 Then
                SelectedDistrib.ParamB.Value = ParamBVal
                If NParams > 2 Then
                    SelectedDistrib.ParamC.Value = ParamCVal
                    If NParams > 3 Then
                        SelectedDistrib.ParamD.Value = ParamDVal
                        If NParams > 4 Then
                            SelectedDistrib.ParamE.Value = ParamEVal
                        End If
                    End If
                End If
            End If
            ApplyRefinedVals()

        Else
            Main.Message.AddWarning("There are no distribution samples available." & vbCrLf)
        End If


    End Sub

    Private Sub ApplyRefinedVals()
        'Apply the refined parameter values

        'ParamAVal = SelectedDistrib.ParamA.Value
        If cmbX1.SelectedIndex = 1 Then
            ABInfo.XParamVal = ParamAVal
            txtX1.Text = ParamAVal
            MouseABPosNow.X = ABInfo.XPos
            pbAB.Invalidate() 'Redraw pbAB
        ElseIf cmbY1.SelectedIndex = 1 Then
            ABInfo.YParamVal = ParamAVal
            txtY1.Text = ParamAVal
            MouseABPosNow.Y = ABInfo.YPos
            pbAB.Invalidate() 'Redraw pbAB
        ElseIf cmbX2.SelectedIndex = 1 Then
            CDInfo.XParamVal = ParamAVal
            txtX2.Text = ParamAVal
            MouseCDPosNow.X = ABInfo.XPos
            pbCD.Invalidate() 'Redraw pbCD
        ElseIf cmbY2.SelectedIndex = 1 Then
            CDInfo.YParamVal = ParamAVal
            txtY2.Text = ParamAVal
            MouseCDPosNow.Y = ABInfo.YPos
            pbCD.Invalidate() 'Redraw pbCD
        End If

        txtA.Text = ParamAVal
        'AInfo.XParamVal = ParamAVal
        'MouseAPosNow.X = AInfo.XPos
        'pbA.Invalidate() 'Redraw pbA
        hsA.Value = 1000 * (ParamAVal - ParamAAdjustMin) / (ParamAAdjustMax - ParamAAdjustMin)

        If Main.SelDistrib = SelDistrib And Main.dgvParams.Rows.Count > 0 Then Main.dgvParams.Rows(0).Cells(2).Value = ParamAVal

        If NParams > 1 Then
            'ParamBVal = SelectedDistrib.ParamB.Value
            If cmbX1.SelectedIndex = 2 Then
                ABInfo.XParamVal = ParamBVal
                txtX1.Text = ParamBVal
                MouseABPosNow.X = ABInfo.XPos
                pbAB.Invalidate() 'Redraw pbAB
            ElseIf cmbY1.SelectedIndex = 2 Then
                ABInfo.YParamVal = ParamBVal
                txtY1.Text = ParamBVal
                MouseABPosNow.Y = ABInfo.YPos
                pbAB.Invalidate() 'Redraw pbAB
            ElseIf cmbX2.SelectedIndex = 2 Then
                CDInfo.XParamVal = ParamBVal
                txtX2.Text = ParamBVal
                MouseCDPosNow.X = ABInfo.XPos
                pbCD.Invalidate() 'Redraw pbCD
            ElseIf cmbY2.SelectedIndex = 2 Then
                CDInfo.YParamVal = ParamBVal
                txtY2.Text = ParamBVal
                MouseCDPosNow.Y = ABInfo.YPos
                pbCD.Invalidate() 'Redraw pbCD
            End If

            txtB.Text = ParamBVal
            'BInfo.XParamVal = ParamBVal
            'MouseBPosNow.X = BInfo.XPos
            'pbB.Invalidate() 'Redraw pbB
            hsB.Value = 1000 * (ParamBVal - ParamBAdjustMin) / (ParamBAdjustMax - ParamBAdjustMin)

            If Main.SelDistrib = SelDistrib And Main.dgvParams.Rows.Count > 1 Then Main.dgvParams.Rows(1).Cells(2).Value = ParamBVal

            If NParams > 2 Then
                'ParamCVal = SelectedDistrib.ParamC.Value

                If cmbX1.SelectedIndex = 2 Then
                    ABInfo.XParamVal = ParamCVal
                    txtX1.Text = ParamCVal
                    MouseABPosNow.X = ABInfo.XPos
                    pbAB.Invalidate() 'Redraw pbAB
                ElseIf cmbY1.SelectedIndex = 2 Then
                    ABInfo.YParamVal = ParamCVal
                    txtY1.Text = ParamCVal
                    MouseABPosNow.Y = ABInfo.YPos
                    pbAB.Invalidate() 'Redraw pbAB
                ElseIf cmbX2.SelectedIndex = 2 Then
                    CDInfo.XParamVal = ParamCVal
                    txtX2.Text = ParamCVal
                    MouseCDPosNow.X = ABInfo.XPos
                    pbCD.Invalidate() 'Redraw pbCD
                ElseIf cmbY2.SelectedIndex = 2 Then
                    CDInfo.YParamVal = ParamCVal
                    txtY2.Text = ParamCVal
                    MouseCDPosNow.Y = ABInfo.YPos
                    pbCD.Invalidate() 'Redraw pbCD
                End If

                txtC.Text = ParamCVal
                'CInfo.XParamVal = ParamCVal
                'MouseCPosNow.X = CInfo.XPos
                'pbC.Invalidate() 'Redraw pbC
                hsC.Value = 1000 * (ParamCVal - ParamCAdjustMin) / (ParamCAdjustMax - ParamCAdjustMin)

                If Main.SelDistrib = SelDistrib And Main.dgvParams.Rows.Count > 2 Then Main.dgvParams.Rows(2).Cells(2).Value = ParamCVal

                If NParams > 3 Then
                    'ParamDVal = SelectedDistrib.ParamD.Value

                    If cmbX1.SelectedIndex = 2 Then
                        ABInfo.XParamVal = ParamDVal
                        txtX1.Text = ParamDVal
                        MouseABPosNow.X = ABInfo.XPos
                        pbAB.Invalidate() 'Redraw pbAB
                    ElseIf cmbY1.SelectedIndex = 2 Then
                        ABInfo.YParamVal = ParamDVal
                        txtY1.Text = ParamDVal
                        MouseABPosNow.Y = ABInfo.YPos
                        pbAB.Invalidate() 'Redraw pbAB
                    ElseIf cmbX2.SelectedIndex = 2 Then
                        CDInfo.XParamVal = ParamDVal
                        txtX2.Text = ParamDVal
                        MouseCDPosNow.X = ABInfo.XPos
                        pbCD.Invalidate() 'Redraw pbCD
                    ElseIf cmbY2.SelectedIndex = 2 Then
                        CDInfo.YParamVal = ParamDVal
                        txtY2.Text = ParamDVal
                        MouseCDPosNow.Y = ABInfo.YPos
                        pbCD.Invalidate() 'Redraw pbCD
                    End If

                    txtD.Text = ParamDVal
                    'DInfo.XParamVal = ParamDVal
                    'MouseDPosNow.X = DInfo.XPos
                    'pbD.Invalidate() 'Redraw pbD
                    hsD.Value = 1000 * (ParamDVal - ParamDAdjustMin) / (ParamDAdjustMax - ParamDAdjustMin)

                    If Main.SelDistrib = SelDistrib And Main.dgvParams.Rows.Count > 3 Then Main.dgvParams.Rows(3).Cells(2).Value = ParamDVal

                    If NParams > 4 Then
                        'ParamEVal = SelectedDistrib.ParamE.Value

                        If cmbX1.SelectedIndex = 2 Then
                            ABInfo.XParamVal = ParamEVal
                            txtX1.Text = ParamEVal
                            MouseABPosNow.X = ABInfo.XPos
                            pbAB.Invalidate() 'Redraw pbAB
                        ElseIf cmbY1.SelectedIndex = 2 Then
                            ABInfo.YParamVal = ParamEVal
                            txtY1.Text = ParamEVal
                            MouseABPosNow.Y = ABInfo.YPos
                            pbAB.Invalidate() 'Redraw pbAB
                        ElseIf cmbX2.SelectedIndex = 2 Then
                            CDInfo.XParamVal = ParamEVal
                            txtX2.Text = ParamEVal
                            MouseCDPosNow.X = ABInfo.XPos
                            pbCD.Invalidate() 'Redraw pbCD
                        ElseIf cmbY2.SelectedIndex = 2 Then
                            CDInfo.YParamVal = ParamEVal
                            txtY2.Text = ParamEVal
                            MouseCDPosNow.Y = ABInfo.YPos
                            pbCD.Invalidate() 'Redraw pbCD
                        End If

                        txtE.Text = ParamEVal
                        'EInfo.XParamVal = ParamEVal
                        'MouseEPosNow.X = EInfo.XPos
                        'pbE.Invalidate() 'Redraw pbE
                        hsE.Value = 1000 * (ParamEVal - ParamEAdjustMin) / (ParamEAdjustMax - ParamEAdjustMin)

                        'If Main.SelDistrib = SelDistrib And Main.dgvParams.Rows.Count > 3 Then Main.dgvParams.Rows(4).Cells(2).Value = ParamEVal
                        If Main.SelDistrib = SelDistrib And Main.dgvParams.Rows.Count > 4 Then Main.dgvParams.Rows(4).Cells(2).Value = ParamEVal
                    End If
                End If
            End If
        End If

        'UpdateSuffix()
        If SelectedDistrib.AutoUpdateLabels Then
            SelectedDistrib.UpdateSuffix() 'This will also update the series labels.
            txtSuffix.Text = SelectedDistrib.Suffix
        End If

        UpdateData()
        Main.UpdateAnnotationSettings(SelDistrib)
        Main.ReplotCharts()
    End Sub

    Private Sub btnSeriesdAnalysis_Click(sender As Object, e As EventArgs) Handles btnSeriesdAnalysis.Click
        'Show the Series Analysis form.

        If SelectedDistrib.ParamEst.Data.Tables("Samples").Rows.Count > 0 Then
            If IsNothing(SeriesAnalysis) Then
                UpdateModelData()
                SeriesAnalysis = New frmSeriesAnalysis
                'SeriesAnalysis.DataSource = Main.
                'SeriesAnalysis.DataSource = SelectedDistrib.ParamEst.Data

                'SeriesAnalysis.DataSource = SelectedDistrib.ParamEst
                'SeriesAnalysis.DataSourceDescription = "Parameter Estimation"
                'SeriesAnalysis.SourceTableName = "Samples"
                'SeriesAnalysis.SourceColumnName = "Value"
                SeriesAnalysis.DataTableName = "Samples"
                SeriesAnalysis.DataColumnName = "Value"

                If SelectedDistrib.Continuity = "Discrete" Then SeriesAnalysis.IsDiscrete = True Else SeriesAnalysis.IsDiscrete = False
                SeriesAnalysis.DistributionName = SelectedDistrib.Name
                SeriesAnalysis.ParamAName = SelectedDistrib.ParamA.Name
                SeriesAnalysis.ParamAValue = SelectedDistrib.ParamA.Value
                SeriesAnalysis.ParamBName = SelectedDistrib.ParamB.Name
                SeriesAnalysis.ParamBValue = SelectedDistrib.ParamB.Value
                SeriesAnalysis.ParamCName = SelectedDistrib.ParamC.Name
                SeriesAnalysis.ParamCValue = SelectedDistrib.ParamC.Value
                SeriesAnalysis.ParamDName = SelectedDistrib.ParamD.Name
                SeriesAnalysis.ParamDValue = SelectedDistrib.ParamD.Value
                SeriesAnalysis.ParamEName = SelectedDistrib.ParamE.Name
                SeriesAnalysis.ParamEValue = SelectedDistrib.ParamE.Value
                SeriesAnalysis.Show()
            Else
                UpdateModelData()
                SeriesAnalysis.UpdateCharts()
                SeriesAnalysis.Show()
                SeriesAnalysis.BringToFront()
            End If
        Else
            'No samples to analyse
        End If
    End Sub

    Private Sub SeriesAnalysis_Closed(sender As Object, e As EventArgs) Handles SeriesAnalysis.Closed
        SeriesAnalysis = Nothing
    End Sub

    Private Sub cmbTableName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTableName.SelectedIndexChanged
        'The selected table name has changed
        Dim TableName As String = cmbTableName.SelectedItem.ToString.Trim
        dgvCalculations.DataSource = SelectedDistrib.ParamEst.Data.Tables(TableName)
        dgvCalculations.Update()
    End Sub



    Private Sub hsA_MouseEnter(sender As Object, e As EventArgs) Handles hsA.MouseEnter
        InUse = True
    End Sub

    Private Sub hsA_MouseLeave(sender As Object, e As EventArgs) Handles hsA.MouseLeave
        InUse = False
    End Sub

    Private Sub hsB_MouseEnter(sender As Object, e As EventArgs) Handles hsB.MouseEnter
        InUse = True
    End Sub

    Private Sub hsB_MouseLeave(sender As Object, e As EventArgs) Handles hsB.MouseLeave
        InUse = False
    End Sub

    Private Sub hsC_MouseEnter(sender As Object, e As EventArgs) Handles hsC.MouseEnter
        InUse = True
    End Sub

    Private Sub hsC_MouseLeave(sender As Object, e As EventArgs) Handles hsC.MouseLeave
        InUse = False
    End Sub

    Private Sub hsD_MouseEnter(sender As Object, e As EventArgs) Handles hsD.MouseEnter
        InUse = True
    End Sub

    Private Sub hsD_MouseLeave(sender As Object, e As EventArgs) Handles hsD.MouseLeave
        InUse = False
    End Sub

    Private Sub hsE_MouseEnter(sender As Object, e As EventArgs) Handles hsE.MouseEnter
        InUse = True
    End Sub

    Private Sub hsE_MouseLeave(sender As Object, e As EventArgs) Handles hsE.MouseLeave
        InUse = False
    End Sub









    'Private Sub TabPage1_GotFocus(sender As Object, e As EventArgs) Handles TabPage1.GotFocus
    '    'If Me.CanFocus Then
    '    '    pbAB.Invalidate()  'Redraw pbAB
    '    '    pbCD.Invalidate()
    '    'End If
    '    'If pbAB.CanFocus Then pbAB.Invalidate()  'Redraw pbAB
    'End Sub

    'Private Sub TabControl1_TabIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.TabIndexChanged
    '    'If TabControl1.SelectedTab.Name = "Graphical" Then
    '    If TabControl1.SelectedTab.Text = "Graphical" Then
    '        pbAB.Invalidate() 'Redraw pbAB
    '        pbCD.Invalidate()
    '    End If
    'End Sub

    'Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
    '    If TabControl1.SelectedIndex = 1 Then
    '        pbAB.Invalidate() 'Redraw pbAB
    '        pbCD.Invalidate()
    '    End If
    'End Sub


#End Region 'Form Methods ---------------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Form Events - Events that can be triggered by this form." '==========================================================================================================================

#End Region 'Form Events ----------------------------------------------------------------------------------------------------------------------------------------------------------------------

End Class


Public Class Graph1DInfo
    'Store and calculate information for a 1D graphical parameter adjustment.

    Private _xPos As Integer = 50 'The mouse X position on the graphic display.
    Property XPos As Integer
        Get
            Return _xPos
        End Get
        Set(value As Integer)
            _xPos = value
            'Update XParamVal:
            If Round Then
                _xParamVal = DecRound(_xParamMin + (_xParamMax - _xParamMin) * (_xPos - _xMin) / (_xMax - _xMin))
            Else
                _xParamVal = _xParamMin + (_xParamMax - _xParamMin) * (_xPos - _xMin) / (_xMax - _xMin)
            End If
        End Set
    End Property

    Private _xMin As Integer = 0 'The minimum X position on the graphic display.
    Property XMin As Integer
        Get
            Return _xMin
        End Get
        Set(value As Integer)
            _xMin = value
            'Update XPos:
            _xPos = Int(_xMin + (_xMax - _xMin) * (_xParamVal - _xParamMin) / (_xParamMax - _xParamMin))
        End Set
    End Property

    Private _xMax As Integer = 100 'The maximum X position on the graphic display.
    Property XMax As Integer
        Get
            Return _xMax
        End Get
        Set(value As Integer)
            _xMax = value
            'Update XPos:
            _xPos = Int(_xMin + (_xMax - _xMin) * (_xParamVal - _xParamMin) / (_xParamMax - _xParamMin))
        End Set
    End Property

    Private _xParamVal As Double = 0.5 'The X parameter value corresponding to the X position.
    Property XParamVal As Double
        Get
            Return _xParamVal
        End Get
        Set(value As Double)
            _xParamVal = value
            'Update XPos:
            _xPos = Int(_xMin + (_xMax - _xMin) * (_xParamVal - _xParamMin) / (_xParamMax - _xParamMin))
        End Set
    End Property

    Private _xParamMin As Double = 0 'The minimum X parameter value.
    Property XParamMin As Double
        Get
            Return _xParamMin
        End Get
        Set(value As Double)
            _xParamMin = value
            'Update XPos:
            _xPos = Int(_xMin + (_xMax - _xMin) * (_xParamVal - _xParamMin) / (_xParamMax - _xParamMin))
        End Set
    End Property

    Private _xParamMax As Double = 1 'The maximum X parameter value.
    Property XParamMax As Double
        Get
            Return _xParamMax
        End Get
        Set(value As Double)
            _xParamMax = value
            'Update XPos:
            _xPos = Int(_xMin + (_xMax - _xMin) * (_xParamVal - _xParamMin) / (_xParamMax - _xParamMin))
        End Set
    End Property

    Private _decRounding As Integer = 3 'The number of decimal places used for rounding the parameter values.
    Property DecRounding As Integer
        Get
            Return _decRounding
        End Get
        Set(value As Integer)
            _decRounding = value
        End Set
    End Property

    Private _round As Boolean = True 'If True, parameter values will be rounded to the specifield number of decomal places.
    Property Round As Boolean
        Get
            Return _round
        End Get
        Set(value As Boolean)
            _round = value
        End Set
    End Property

    Private Function DecRound(InputVal As Double) As Double
        'Round the InputVal to the DecRounding number of decimal places.
        Dim DecVal As Decimal = InputVal
        Return Math.Round(DecVal, DecRounding)
    End Function

End Class 'Graph1DInfo

Public Class Graph2DInfo
    'Store and calculate information for a 2D graphical parameter adjustment.

    'Public UpdateXParamValue As Object 'This will be referenced to the X parameter value to update.
    'Public UpdateYParamValue As Object 'This will be referenced to the Y parameter value to update.


    Private _xPos As Integer = 50 'The mouse X position on the graphic display.
    Property XPos As Integer
        Get
            Return _xPos
        End Get
        Set(value As Integer)
            _xPos = value
            'Update XParamVal:
            _xParamVal = _xParamMin + (_xParamMax - _xParamMin) * (_xPos - _xMin) / (_xMax - _xMin)
            'If Round Then
            '    _xParamVal = DecRound(_xParamMin + (_xParamMax - _xParamMin) * (_xPos - _xMin) / (_xMax - _xMin))
            'Else
            '    _xParamVal = _xParamMin + (_xParamMax - _xParamMin) * (_xPos - _xMin) / (_xMax - _xMin)
            'End If
        End Set
    End Property

    Private _yPos As Integer = 50 'The mouse Y position on the graphic display.
    Property YPos As Integer
        Get
            Return _yPos
        End Get
        Set(value As Integer)
            _yPos = value
            'Update YParamVal:
            _yParamVal = _yParamMin + (_yParamMax - _yParamMin) * (_yPos - _yMin) / (_yMax - _yMin)
            'If Round Then
            '    _yParamVal = DecRound(_yParamMin + (_yParamMax - _yParamMin) * (_yPos - _yMin) / (_yMax - _yMin))
            'Else
            '    _yParamVal = _yParamMin + (_yParamMax - _yParamMin) * (_yPos - _yMin) / (_yMax - _yMin)
            'End If
        End Set
    End Property

    Private _xMin As Integer = 0 'The minimum X position on the graphic display.
    Property XMin As Integer
        Get
            Return _xMin
        End Get
        Set(value As Integer)
            _xMin = value
            'Update XPos:
            _xPos = Int(_xMin + (_xMax - _xMin) * (_xParamVal - _xParamMin) / (_xParamMax - _xParamMin))
        End Set
    End Property

    Private _xMax As Integer = 100 'The maximum X position on the graphic display.
    Property XMax As Integer
        Get
            Return _xMax
        End Get
        Set(value As Integer)
            _xMax = value
            'Update XPos:
            _xPos = Int(_xMin + (_xMax - _xMin) * (_xParamVal - _xParamMin) / (_xParamMax - _xParamMin))
        End Set
    End Property

    Private _yMin As Integer = 100 'The minimum Y position on the graphic display. (In a picturebox Y increases downwards.)
    Property YMin As Integer
        Get
            Return _yMin
        End Get
        Set(value As Integer)
            _yMin = value
            'Update YPos:
            _yPos = Int(_yMin + (_yMax - _yMin) * (_yParamVal - _yParamMin) / (_yParamMax - _yParamMin))
        End Set
    End Property

    Private _yMax As Integer = 0 'The maximum Y position on the graphic display.
    Property YMax As Integer
        Get
            Return _yMax
        End Get
        Set(value As Integer)
            _yMax = value
            'Update YPos:
            _yPos = Int(_yMin + (_yMax - _yMin) * (_yParamVal - _yParamMin) / (_yParamMax - _yParamMin))
        End Set
    End Property

    Private _xParamVal As Double = 0.5 'The X parameter value corresponding to the X position.
    Property XParamVal As Double
        Get
            Return _xParamVal
        End Get
        Set(value As Double)
            _xParamVal = value
            'Update XPos:
            _xPos = Int(_xMin + (_xMax - _xMin) * (_xParamVal - _xParamMin) / (_xParamMax - _xParamMin))
        End Set
    End Property

    Private _xParamMin As Double = 0 'The minimum X parameter value.
    Property XParamMin As Double
        Get
            Return _xParamMin
        End Get
        Set(value As Double)
            _xParamMin = value
            'Update XPos:
            If _xParamMax = _xParamMin Then _xParamMax = _xParamMin + 1
            _xPos = Int(_xMin + (_xMax - _xMin) * (_xParamVal - _xParamMin) / (_xParamMax - _xParamMin))
        End Set
    End Property

    Private _xParamMax As Double = 1 'The maximum X parameter value.
    Property XParamMax As Double
        Get
            Return _xParamMax
        End Get
        Set(value As Double)
            _xParamMax = value
            'Update XPos:
            _xPos = Int(_xMin + (_xMax - _xMin) * (_xParamVal - _xParamMin) / (_xParamMax - _xParamMin))
        End Set
    End Property

    Private _yParamVal As Double = 0.5 'The Y parameter value corresponding to the Y position.
    Property YParamVal As Double
        Get
            Return _yParamVal
        End Get
        Set(value As Double)
            _yParamVal = value
            'Update YPos:
            _yPos = Int(_yMin + (_yMax - _yMin) * (_yParamVal - _yParamMin) / (_yParamMax - _yParamMin))
        End Set
    End Property

    Private _yParamMin As Double = 0 'The minimum Y parameter value.
    Property YParamMin As Double
        Get
            Return _yParamMin
        End Get
        Set(value As Double)
            _yParamMin = value
            'Update YPos:
            'If _yParamMin = _yParamMax Then _yParamMin -= 0.0000001
            If _yParamMin = _yParamMax Then _yParamMin -= 0.0001
            _yPos = Int(_yMin + (_yMax - _yMin) * (_yParamVal - _yParamMin) / (_yParamMax - _yParamMin))
        End Set
    End Property

    Private _yParamMax As Double = 1 'The maximum Y parameter value.
    Property YParamMax As Double
        Get
            Return _yParamMax
        End Get
        Set(value As Double)
            _yParamMax = value
            'Update YPos:
            'If _yParamMin = _yParamMax Then _yParamMin -= 1
            _yPos = Int(_yMin + (_yMax - _yMin) * (_yParamVal - _yParamMin) / (_yParamMax - _yParamMin))
        End Set
    End Property

    'NOTE: All rounding is now done within the property in the host form.
    'Private _decRounding As Integer = 3 'The number of decimal places used for rounding the parameter values.
    'Property DecRounding As Integer
    '    Get
    '        Return _decRounding
    '    End Get
    '    Set(value As Integer)
    '        _decRounding = value
    '    End Set
    'End Property

    'Private _round As Boolean = True 'If True, parameter values will be rounded to the specifield number of decomal places.
    'Property Round As Boolean
    '    Get
    '        Return _round
    '    End Get
    '    Set(value As Boolean)
    '        _round = value
    '    End Set
    'End Property

    'Private Function DecRound(InputVal As Double) As Double
    '    'Round the InputVal to the DecRounding number of decimal places.
    '    Dim DecVal As Decimal = InputVal
    '    Return Math.Round(DecVal, DecRounding)
    'End Function


End Class