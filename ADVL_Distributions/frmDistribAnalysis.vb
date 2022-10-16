Public Class frmDistribAnalysis
    'Probability Distribution Analysis.

#Region " Variable Declarations - All the variables used in this form and this application." '=================================================================================================

    Public WithEvents Distribution As New DistributionModel 'This class holds the Distribution parameters, data and charts.

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
                               <PopulationFormat><%= txtPopFormat.Text %></PopulationFormat>
                               <ProbabilityFormat><%= txtProbFormat.Text %></ProbabilityFormat>
                               <ProbPercentFormat><%= txtProbPctFormat.Text %></ProbPercentFormat>
                               <TotalPopulation><%= txtTotalPopulation.Text.Replace(",", "") %></TotalPopulation>
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
            If Settings.<FormSettings>.<ModelFileName>.Value <> Nothing Then cmbModel.SelectedIndex = cmbModel.FindStringExact(Settings.<FormSettings>.<ModelFileName>.Value)
            If Settings.<FormSettings>.<DistributionNumber>.Value <> Nothing Then cmbRandVar.SelectedIndex = Settings.<FormSettings>.<DistributionNumber>.Value

            'If Settings.<FormSettings>.<PopulationFormat>.Value <> Nothing Then txtPopFormat.Text = Settings.<FormSettings>.<PopulationFormat>.Value
            If Settings.<FormSettings>.<ProbabilityFormat>.Value <> Nothing Then txtProbFormat.Text = Settings.<FormSettings>.<ProbabilityFormat>.Value
            If Settings.<FormSettings>.<ProbPercentFormat>.Value <> Nothing Then txtProbPctFormat.Text = Settings.<FormSettings>.<ProbPercentFormat>.Value

            'If Settings.<FormSettings>.<TotalPopulation>.Value <> Nothing Then txtTotalPopulation.Text = Format(Settings.<FormSettings>.<TotalPopulation>.Value, txtPopFormat.Text)
            If Settings.<FormSettings>.<TotalPopulation>.Value <> Nothing Then
                If Settings.<FormSettings>.<PopulationFormat>.Value <> Nothing Then
                    txtPopFormat.Text = Settings.<FormSettings>.<PopulationFormat>.Value
                    txtTotalPopulation.Text = Format(Val(Settings.<FormSettings>.<TotalPopulation>.Value), Settings.<FormSettings>.<PopulationFormat>.Value)
                Else
                    txtTotalPopulation.Text = Settings.<FormSettings>.<TotalPopulation>.Value
                End If
            Else
                If Settings.<FormSettings>.<PopulationFormat>.Value <> Nothing Then txtPopFormat.Text = Settings.<FormSettings>.<PopulationFormat>.Value
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

        'Fill cmbModel with the list of distribution models
        Dim FilenameList As New ArrayList
        Main.Project.GetDataFileList("Distrib", FilenameList)
        For Each item In FilenameList
            cmbModel.Items.Add(item.ToString)
        Next

        'dgvAreaAnnot.ColumnCount = 10
        'dgvProbabilities.ColumnCount = 7
        'dgvProbabilities.ColumnCount = 9
        dgvProbabilities.ColumnCount = 11

        'Dim chkShowArea As New DataGridViewCheckBoxColumn
        'dgvProbabilities.Columns.Insert(0, chkShowArea)
        'dgvProbabilities.Columns(0).HeaderText = "Show"
        'dgvProbabilities.Columns(0).Width = 40

        Dim cboFromAnnotType As New DataGridViewComboBoxColumn
        cboFromAnnotType.Items.Add("Minimum")
        cboFromAnnotType.Items.Add("Probability <=")
        cboFromAnnotType.Items.Add("Probability >")
        cboFromAnnotType.Items.Add("Random Variable Value")
        cboFromAnnotType.Items.Add("Mean")
        cboFromAnnotType.Items.Add("Median")
        cboFromAnnotType.Items.Add("Mode")
        cboFromAnnotType.Items.Add("Standard Deviation")
        'cboFromAnnotType.Items.Add("User Defined Value 1")
        'cboFromAnnotType.Items.Add("User Defined Value 2")
        dgvProbabilities.Columns.Insert(0, cboFromAnnotType)
        dgvProbabilities.Columns(0).HeaderText = "From Value Type"
        dgvProbabilities.Columns(0).Width = 140

        dgvProbabilities.Columns(1).HeaderText = "From Value Parameter"
        dgvProbabilities.Columns(1).Width = 80
        dgvProbabilities.Columns(2).HeaderText = "From Value"
        dgvProbabilities.Columns(2).Width = 80
        dgvProbabilities.Columns(3).HeaderText = "From Value CDF"
        dgvProbabilities.Columns(3).Width = 110

        Dim cboToAnnotType As New DataGridViewComboBoxColumn
        cboToAnnotType.Items.Add("Maximum")
        cboToAnnotType.Items.Add("Probability <=")
        cboToAnnotType.Items.Add("Probability >")
        cboToAnnotType.Items.Add("Random Variable Value")
        cboToAnnotType.Items.Add("Mean")
        cboToAnnotType.Items.Add("Median")
        cboToAnnotType.Items.Add("Mode")
        cboToAnnotType.Items.Add("Standard Deviation")
        'cboToAnnotType.Items.Add("User Defined Value 1")
        'cboToAnnotType.Items.Add("User Defined Value 2")
        dgvProbabilities.Columns.Insert(4, cboToAnnotType)
        dgvProbabilities.Columns(4).HeaderText = "To Value Type"
        dgvProbabilities.Columns(4).Width = 140

        dgvProbabilities.Columns(5).HeaderText = "To Value Parameter"
        dgvProbabilities.Columns(5).Width = 80
        dgvProbabilities.Columns(6).HeaderText = "To Value"
        dgvProbabilities.Columns(6).Width = 80
        dgvProbabilities.Columns(7).HeaderText = "To Value CDF"
        dgvProbabilities.Columns(7).Width = 110

        dgvProbabilities.Columns(8).HeaderText = "Probability"
        dgvProbabilities.Columns(8).Width = 110

        dgvProbabilities.Columns(9).HeaderText = "Prob %"
        dgvProbabilities.Columns(9).Width = 110

        dgvProbabilities.Columns(10).HeaderText = "One in"
        dgvProbabilities.Columns(10).Width = 110
        dgvProbabilities.Columns(10).DefaultCellStyle.Format = "N4"   'N4 - Number displayed with thousands separator and 4 decimal places

        dgvProbabilities.Columns(11).HeaderText = "Population"
        dgvProbabilities.Columns(11).Width = 110

        dgvProbabilities.Columns(12).HeaderText = "Description"
        dgvProbabilities.Columns(12).Width = 500


        'dgvAreaAnnot.Columns(10).HeaderText = "Color"
        'dgvAreaAnnot.Columns(10).Width = 80
        'dgvAreaAnnot.Columns(11).HeaderText = "Thickness"
        'dgvAreaAnnot.Columns(11).Width = 80
        'dgvAreaAnnot.Columns(12).HeaderText = "Density"
        'dgvAreaAnnot.Columns(12).Width = 80

        'Dim cboIntensity As New DataGridViewComboBoxColumn
        ''cboIntensity.Items.Add("5")
        'cboIntensity.Items.Add("10")
        'cboIntensity.Items.Add("20")
        'cboIntensity.Items.Add("25")
        'cboIntensity.Items.Add("30")
        'cboIntensity.Items.Add("40")
        'cboIntensity.Items.Add("50")
        'cboIntensity.Items.Add("60")
        'cboIntensity.Items.Add("70")
        'cboIntensity.Items.Add("75")
        'cboIntensity.Items.Add("80")
        'cboIntensity.Items.Add("90")
        'cboIntensity.Items.Add("95")
        'cboIntensity.Items.Add("100")
        'dgvAreaAnnot.Columns.Insert(13, cboIntensity)
        'dgvAreaAnnot.Columns(13).HeaderText = "Intensity"
        'dgvAreaAnnot.Columns(13).Width = 80


        RestoreFormSettings()   'Restore the form settings
        dgvProbabilities.Columns(3).DefaultCellStyle.Format = txtProbFormat.Text 'From Value CDF
        dgvProbabilities.Columns(7).DefaultCellStyle.Format = txtProbFormat.Text 'To Value CDF
        dgvProbabilities.Columns(8).DefaultCellStyle.Format = txtProbFormat.Text 'Probability
        dgvProbabilities.Columns(9).DefaultCellStyle.Format = txtProbPctFormat.Text 'Probability Percent
        dgvProbabilities.Columns(10).DefaultCellStyle.Format = txtPopFormat.Text   'One In - Default: N4 - Number displayed with thousands separator and 4 decimal places
        dgvProbabilities.Columns(11).DefaultCellStyle.Format = txtPopFormat.Text 'Population  - Default: N4 - Number displayed with thousands separator and 4 decimal places

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

    Private Sub Form_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        If FormNo > -1 Then
            Main.DistribAnalysisClosed()
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
            'txtSampleVal.Text = ""

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
            'txtSampleVal.Text = ""
        Else
            'Update the form:
            txtDescr.Text = Distribution.Info(SelDistrib).RVDescription
            'lblUnitsAbbrev.Text = Distribution.Info(SelDistrib).RVAbbrevUnits
            txtDistribType.Text = Distribution.Info(SelDistrib).Name
            txtDistribParams.Text = Distribution.Info(SelDistrib).Suffix
            RandomVariableName = cmbRandVar.SelectedItem.ToString
            txtRVUnits.Text = Distribution.Info(SelDistrib).RVUnits
            txtRVUnitsAbbrev.Text = Distribution.Info(SelDistrib).RVAbbrevUnits
            txtContinuity.Text = Distribution.Info(SelDistrib).Continuity

            txtMean.Text = Distribution.Info(SelDistrib).Mean
            txtMode.Text = Distribution.Info(SelDistrib).Mode
            txtMedian.Text = Distribution.Info(SelDistrib).Median
            txtStdDev.Text = Distribution.Info(SelDistrib).StdDev
            txtSkewness.Text = Distribution.Info(SelDistrib).Skewness
            txtEntropy.Text = Distribution.Info(SelDistrib).Entropy

            RecalcAll()


            'Generate a Sample:
            'Dim myRandom As New Random
            'txtSampleVal.Text = Format(Distribution.Info(SelDistrib).InvCDF(myRandom.NextDouble), txtSingleFormat.Text)

            ''Update the Multople Samples group:
            'Dim ColumnName As String = cmbRandVar.Text.Trim.Replace(" ", "_")
            'If cmbColumnName.Items.Contains(ColumnName) Then
            '    cmbColumnName.SelectedIndex = cmbColumnName.FindStringExact(ColumnName)
            'Else
            '    cmbColumnName.Text = ColumnName
            'End If
            'txtSampSetLabel.Text = cmbColumnName.Text
            'txtSampSetUnits.Text = txtRVUnits.Text
            'txtSampSetUnitsAbbrev.Text = txtRVUnitsAbbrev.Text
            'txtSampSetDescr.Text = txtDescr.Text

        End If

    End Sub



    'Private Sub dgvAreaAnnot_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvAreaAnnot.CellContentClick
    '    'The Area Annotation settings have been edited.

    '    Dim RowNo As Integer = e.RowIndex
    '    Dim RowCount As Integer = dgvAreaAnnot.RowCount
    '    If RowNo = RowCount - 1 Then
    '        'This is the last user-add row - not yet editable!
    '    Else
    '        Dim ColNo As Integer = e.ColumnIndex
    '        Dim DistrbNo As Integer = SelDistrib
    '        Dim AnnotNo As Integer = RowNo
    '        Dim SelPointAnnotInfo = From item In myParent.ChartInfo.<ChartSettings>.<AreaAnnotationCollection>.<AreaAnnotation> Where item.<DistributionNo>.Value = SelDistrib And item.<DistribAnnotNo>.Value = AnnotNo
    '    End If
    'End Sub

    'Private Sub dgvAreaAnnot_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvAreaAnnot.CellClick

    '    Dim ColNo As Integer = e.ColumnIndex
    '    If ColNo = 10 Then 'Area shading color has changed.
    '        Dim RowNo As Integer = e.RowIndex
    '        ColorDialog1.Color = dgvAreaAnnot.Rows(RowNo).Cells(ColNo).Style.BackColor
    '        Dim Result As DialogResult = ColorDialog1.ShowDialog()
    '        If Result = DialogResult.OK Then
    '            If dgvAreaAnnot.Rows(RowNo).Cells(ColNo).Style.BackColor = ColorDialog1.Color Then
    '                'The shading color has not been changed.
    '            Else
    '                dgvAreaAnnot.Rows(RowNo).Cells(ColNo).Style.BackColor = ColorDialog1.Color
    '                Dim AnnotNo As Integer = RowNo
    '                Dim SelAreaAnnotInfo = From item In myParent.ChartInfo.<ChartSettings>.<AreaAnnotationCollection>.<AreaAnnotation> Where item.<DistributionNo>.Value = SelDistrib And item.<DistribAnnotNo>.Value = AnnotNo
    '                SelAreaAnnotInfo.<Color>.Value = ColorDialog1.Color.ToArgb.ToString
    '            End If
    '        End If
    '    End If
    'End Sub

    'Private Sub dgvAreaAnnot_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvAreaAnnot.CellEndEdit
    '    'The Area Annotation settings have been edited.

    '    Dim RowNo As Integer = e.RowIndex
    '    Dim RowCount As Integer = dgvAreaAnnot.RowCount
    '    If RowNo = RowCount - 1 Then
    '        'This is the last (user added) row - not yet editable!
    '    Else
    '        Dim ColNo As Integer = e.ColumnIndex
    '        Dim DistribNo As Integer = SelDistrib
    '        Dim AnnotNo As Integer = RowNo
    '        Dim SelAreaAnnotInfo = From item In myParent.ChartInfo.<ChartSettings>.<AreaAnnotationCollection>.<AreaAnnotation> Where item.<DistributionNo>.Value = SelDistrib And item.<DistribAnnotNo>.Value = AnnotNo
    '        Select Case ColNo
    '            Case 0 'Show
    '                SelAreaAnnotInfo.<Show>.Value = dgvAreaAnnot.Rows(RowNo).Cells(0).Value
    '                XmlHtmDisplay1.Rtf = XmlHtmDisplay1.XmlToRtf(myParent.ChartInfo.ToString, True) 'Update the XML display
    '            Case 1 'From Value Type

    '            Case 2 'From Value Parameter

    '            Case 3 'From Value

    '            Case 4 'From Value CDF

    '            Case 5  'To Value Type

    '            Case 6 'To Value Parameter

    '            Case 7  'To Value

    '            Case 8 'To Value CDF

    '            Case 9 'Probability

    '            Case 10 'Color

    '            Case 11 'Thickness
    '                SelAreaAnnotInfo.<Thickness>.Value = dgvAreaAnnot.Rows(RowNo).Cells(11).Value
    '                XmlHtmDisplay1.Rtf = XmlHtmDisplay1.XmlToRtf(myParent.ChartInfo.ToString, True) 'Update the XML display
    '            Case 12 'Density
    '                SelAreaAnnotInfo.<Density>.Value = dgvAreaAnnot.Rows(RowNo).Cells(12).Value
    '                XmlHtmDisplay1.Rtf = XmlHtmDisplay1.XmlToRtf(myParent.ChartInfo.ToString, True) 'Update the XML display
    '            Case 13
    '                SelAreaAnnotInfo.<Intensity>.Value = dgvAreaAnnot.Rows(RowNo).Cells(13).Value
    '                XmlHtmDisplay1.Rtf = XmlHtmDisplay1.XmlToRtf(myParent.ChartInfo.ToString, True) 'Update the XML display
    '        End Select
    '    End If
    'End Sub

    Private Sub dgvAreaAnnot_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles dgvProbabilities.EditingControlShowing

        'If dgvProbabilities.CurrentCell.ColumnIndex = 1 Then 'From Value Type selected
        If dgvProbabilities.CurrentCell.ColumnIndex = 0 Then 'From Value Type selected
            Dim combo As ComboBox = CType(e.Control, ComboBox)
            If (combo IsNot Nothing) Then
                combo.Name = "cboFromValueType"
                'Remove current handler:
                RemoveHandler combo.SelectionChangeCommitted, New EventHandler(AddressOf AreaComboBox_SelectionChangeCommitted)
                'Add the event handler:
                AddHandler combo.SelectionChangeCommitted, New EventHandler(AddressOf AreaComboBox_SelectionChangeCommitted)
            End If
            'ElseIf dgvProbabilities.CurrentCell.ColumnIndex = 5 Then 'To Value Type selected
        ElseIf dgvProbabilities.CurrentCell.ColumnIndex = 4 Then 'To Value Type selected
            Dim combo As ComboBox = CType(e.Control, ComboBox)
            If (combo IsNot Nothing) Then
                combo.Name = "cboToValueType"
                'Remove current handler:
                RemoveHandler combo.SelectionChangeCommitted, New EventHandler(AddressOf AreaComboBox_SelectionChangeCommitted)
                'Add the event handler:
                AddHandler combo.SelectionChangeCommitted, New EventHandler(AddressOf AreaComboBox_SelectionChangeCommitted)
            End If
            'ElseIf dgvProbabilities.CurrentCell.ColumnIndex = 13 Then 'Shading Intensity selected
            '    Dim combo As ComboBox = CType(e.Control, ComboBox)
            '    If (combo IsNot Nothing) Then
            '        combo.Name = "cboIntensity"
            '        'Remove current handler:
            '        RemoveHandler combo.SelectionChangeCommitted, New EventHandler(AddressOf AreaComboBox_SelectionChangeCommitted)
            '        'Add the event handler:
            '        AddHandler combo.SelectionChangeCommitted, New EventHandler(AddressOf AreaComboBox_SelectionChangeCommitted)
            '    End If
        End If
    End Sub

    Private Sub AreaComboBox_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim combo As ComboBox = CType(sender, ComboBox)

        Dim RowNo As Integer = dgvProbabilities.SelectedCells(0).RowIndex
        dgvProbabilities.AllowUserToAddRows = False

        If combo.Name = "cboFromValueType" Then
            Select Case combo.SelectedItem.ToString
                Case "Minimum"
                    If RowNo > dgvProbabilities.RowCount - 1 Then 'Add a new row.
                        Dim Mean As Double = Distribution.Info(SelDistrib).Mean 'Use the Mean value as the To Value as the Default.
                        'dgvProbabilities.Rows.Add({True, "Minimum", "", "", 0, "Mean", "", Mean, Distribution.Info(SelDistrib).CDF(Mean), Distribution.Info(SelDistrib).CDF(Mean), "", 1, 2, "50"})
                        dgvProbabilities.Rows.Add({"Minimum", "", "", 0, "Mean", "", Mean, Distribution.Info(SelDistrib).CDF(Mean), Distribution.Info(SelDistrib).CDF(Mean)})
                        dgvProbabilities.Rows(RowNo).Cells(9).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * 100
                        dgvProbabilities.Rows(RowNo).Cells(10).Value = 1 / dgvProbabilities.Rows(RowNo).Cells(8).Value
                        If txtTotalPopulation.Text <> "" Then
                            'dgvProbabilities.Rows(RowNo).Cells(10).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * Val(txtTotalPopulation.Text)
                            dgvProbabilities.Rows(RowNo).Cells(11).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * Val(txtTotalPopulation.Text.Replace(",", ""))
                        End If

                        'dgvAreaAnnot.Rows(RowNo).Cells(10).Style.BackColor = Color.Red
                        'AddXmlAreaAnnotInfo(RowNo)
                    Else
                        'dgvProbabilities.Rows(RowNo).Cells(0).Value = True 'Show
                        dgvProbabilities.Rows(RowNo).Cells(0).Value = "Minimum" 'From Value Type
                        dgvProbabilities.Rows(RowNo).Cells(1).Value = "" 'From Value Parameter - not required.
                        dgvProbabilities.Rows(RowNo).Cells(2).Value = "" 'From Value - not required.
                        dgvProbabilities.Rows(RowNo).Cells(3).Value = 0 'From Value CDF - Zero.
                        If dgvProbabilities.Rows(RowNo).Cells(3).ValueType = GetType(String) Then 'If the Value Type is a string, then the probability can not be calculated.
                            'The To Value CDF is not defined.
                            dgvProbabilities.Rows(RowNo).Cells(8).Value = "" 'The Probability can not be calculated.
                        Else
                            'Calculate the Probability that the Random Variable will lie between From Value and To Value:
                            dgvProbabilities.Rows(RowNo).Cells(8).Value = dgvProbabilities.Rows(RowNo).Cells(7).Value - dgvProbabilities.Rows(RowNo).Cells(3).Value
                        End If
                        'UpdateXmlAreaAnnotInfo(RowNo)
                    End If
                Case "Probability <="
                    Dim P10Value As Double = Distribution.Info(SelDistrib).InvCDF(0.1)   'Default probability value: 0.1 (P10)
                    If RowNo > dgvProbabilities.RowCount - 1 Then  'Add a new row.
                        Dim Mean As Double = Distribution.Info(SelDistrib).Mean 'Use the Mean value as the To Value as the Default.
                        dgvProbabilities.Rows.Add({"Probability <=", 0.1, P10Value, 0.1, "Mean", "", Mean, Distribution.Info(SelDistrib).CDF(Mean), Distribution.Info(SelDistrib).CDF(Mean) - 0.1})
                        dgvProbabilities.Rows(RowNo).Cells(9).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * 100
                        dgvProbabilities.Rows(RowNo).Cells(10).Value = 1 / dgvProbabilities.Rows(RowNo).Cells(8).Value
                        If txtTotalPopulation.Text <> "" Then
                            'dgvProbabilities.Rows(RowNo).Cells(10).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * Val(txtTotalPopulation.Text)
                            dgvProbabilities.Rows(RowNo).Cells(11).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * Val(txtTotalPopulation.Text.Replace(",", ""))
                        End If
                        'dgvProbabilities.Rows(RowNo).Cells(10).Style.BackColor = Color.Red
                        'AddXmlAreaAnnotInfo(RowNo)
                    Else
                        'dgvProbabilities.Rows(RowNo).Cells(0).Value = True 'Show
                        dgvProbabilities.Rows(RowNo).Cells(0).Value = "Probability <=" 'From Value Type
                        dgvProbabilities.Rows(RowNo).Cells(1).Value = 0.1 'From Value Parameter - use default value of 0.1.
                        dgvProbabilities.Rows(RowNo).Cells(2).Value = P10Value 'From Value - Default: P10Value.
                        dgvProbabilities.Rows(RowNo).Cells(3).Value = 0.1 'From Value CDF - 0.1.
                        If dgvProbabilities.Rows(RowNo).Cells(3).ValueType = GetType(String) Then 'If the Value Type is a string, then the probability can not be calculated.
                            'The To Value CDF is not defined.
                            dgvProbabilities.Rows(RowNo).Cells(8).Value = "" 'The Probability can not be calculated.
                        Else
                            'Calculate the Probability that the Random Variable will lie between From Value and To Value:
                            dgvProbabilities.Rows(RowNo).Cells(8).Value = dgvProbabilities.Rows(RowNo).Cells(7).Value - dgvProbabilities.Rows(RowNo).Cells(3).Value
                        End If
                        'UpdateXmlAreaAnnotInfo(RowNo)
                    End If

                Case "Probability >"
                    Dim P90Value As Double = GetInvRevCdfValue(0.9)
                    If RowNo > dgvProbabilities.RowCount - 1 Then  'Add a new row.
                        Dim Mean As Double = Distribution.Info(SelDistrib).Mean 'Use the Mean value as the To Value as the Default.
                        'Note for RevCDF = 0.9, the corresponding CDF = 0.1
                        dgvProbabilities.Rows.Add({"Probability >", 0.9, P90Value, 0.1, "Mean", "", Mean, Distribution.Info(SelDistrib).CDF(Mean), Distribution.Info(SelDistrib).CDF(Mean) - 0.1})
                        dgvProbabilities.Rows(RowNo).Cells(9).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * 100
                        dgvProbabilities.Rows(RowNo).Cells(10).Value = 1 / dgvProbabilities.Rows(RowNo).Cells(8).Value
                        If txtTotalPopulation.Text <> "" Then
                            'dgvProbabilities.Rows(RowNo).Cells(10).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * Val(txtTotalPopulation.Text)
                            dgvProbabilities.Rows(RowNo).Cells(11).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * Val(txtTotalPopulation.Text.Replace(",", ""))
                        End If
                        'dgvProbabilities.Rows(RowNo).Cells(10).Style.BackColor = Color.Red
                        'AddXmlAreaAnnotInfo(RowNo)
                    Else
                        'dgvProbabilities.Rows(RowNo).Cells(0).Value = True 'Show
                        dgvProbabilities.Rows(RowNo).Cells(0).Value = "Probability >" 'From Value Type
                        dgvProbabilities.Rows(RowNo).Cells(1).Value = 0.9 'From Value Parameter - use default value of 0.9.
                        dgvProbabilities.Rows(RowNo).Cells(2).Value = P90Value 'From Value - Default: P90Value.
                        dgvProbabilities.Rows(RowNo).Cells(3).Value = 0.1 'From Value CDF - 0.1. (CDF value is 1 - RevCDF value)
                        If dgvProbabilities.Rows(RowNo).Cells(3).ValueType = GetType(String) Then 'If the Value Type is a string, then the probability can not be calculated.
                            'The To Value CDF is not defined.
                            dgvProbabilities.Rows(RowNo).Cells(8).Value = "" 'The Probability can not be calculated.
                        Else
                            'Calculate the Probability that the Random Variable will lie between From Value and To Value:
                            dgvProbabilities.Rows(RowNo).Cells(8).Value = dgvProbabilities.Rows(RowNo).Cells(7).Value - dgvProbabilities.Rows(RowNo).Cells(3).Value
                        End If
                        'UpdateXmlAreaAnnotInfo(RowNo)
                    End If

                Case "Random Variable Value"
                    Dim P10Value As Double = Distribution.Info(SelDistrib).InvCDF(0.1)   'Default probability value: 0.1 (P10)
                    If RowNo > dgvProbabilities.RowCount - 1 Then  'Add a new row.
                        Dim Mean As Double = Distribution.Info(SelDistrib).Mean 'Use the Mean value as the To Value as the Default.
                        dgvProbabilities.Rows.Add({"Random Variable Value", P10Value, P10Value, 0.1, "Mean", "", Mean, Distribution.Info(SelDistrib).CDF(Mean), Distribution.Info(SelDistrib).CDF(Mean) - 0.1})
                        dgvProbabilities.Rows(RowNo).Cells(9).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * 100
                        dgvProbabilities.Rows(RowNo).Cells(10).Value = 1 / dgvProbabilities.Rows(RowNo).Cells(8).Value
                        If txtTotalPopulation.Text <> "" Then
                            'dgvProbabilities.Rows(RowNo).Cells(10).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * Val(txtTotalPopulation.Text)
                            dgvProbabilities.Rows(RowNo).Cells(11).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * Val(txtTotalPopulation.Text.Replace(",", ""))
                        End If
                        'dgvProbabilities.Rows(RowNo).Cells(10).Style.BackColor = Color.Red
                        'AddXmlAreaAnnotInfo(RowNo)
                    Else
                        'dgvProbabilities.Rows(RowNo).Cells(0).Value = True 'Show
                        dgvProbabilities.Rows(RowNo).Cells(0).Value = "Random Variable Value" 'From Value Type
                        dgvProbabilities.Rows(RowNo).Cells(1).Value = P10Value 'From Value Parameter - use default value of 0.9.
                        dgvProbabilities.Rows(RowNo).Cells(2).Value = P10Value 'From Value - Default: P90Value.
                        dgvProbabilities.Rows(RowNo).Cells(3).Value = 0.1 'From Value CDF - 0.1. 
                        If dgvProbabilities.Rows(RowNo).Cells(3).ValueType = GetType(String) Then 'If the Value Type is a string, then the probability can not be calculated.
                            'The To Value CDF is not defined.
                            dgvProbabilities.Rows(RowNo).Cells(8).Value = "" 'The Probability can not be calculated.
                        Else
                            'Calculate the Probability that the Random Variable will lie between From Value and To Value:
                            dgvProbabilities.Rows(RowNo).Cells(8).Value = dgvProbabilities.Rows(RowNo).Cells(7).Value - dgvProbabilities.Rows(RowNo).Cells(3).Value
                        End If
                        'UpdateXmlAreaAnnotInfo(RowNo)
                    End If

                Case "Mean"
                    Dim Mean As Double = Distribution.Info(SelDistrib).Mean
                    If RowNo > dgvProbabilities.RowCount - 1 Then  'Add a new row.
                        Dim P90Value As Double = Distribution.Info(SelDistrib).InvCDF(0.9)   'Default probability value: 0.9 (P90)
                        dgvProbabilities.Rows.Add({"Mean", "", Mean, Distribution.Info(SelDistrib).CDF(Mean), "Probability <=", 0.9, Distribution.Info(SelDistrib).InvCDF(0.9), 0.9, 0.9 - Distribution.Info(SelDistrib).CDF(Mean)})
                        dgvProbabilities.Rows(RowNo).Cells(9).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * 100
                        dgvProbabilities.Rows(RowNo).Cells(10).Value = 1 / dgvProbabilities.Rows(RowNo).Cells(8).Value
                        If txtTotalPopulation.Text <> "" Then
                            'dgvProbabilities.Rows(RowNo).Cells(10).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * Val(txtTotalPopulation.Text)
                            dgvProbabilities.Rows(RowNo).Cells(11).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * Val(txtTotalPopulation.Text.Replace(",", ""))
                        End If
                        'dgvProbabilities.Rows(RowNo).Cells(10).Style.BackColor = Color.Red
                        'AddXmlAreaAnnotInfo(RowNo)
                    Else
                        'dgvProbabilities.Rows(RowNo).Cells(0).Value = True 'Show
                        dgvProbabilities.Rows(RowNo).Cells(0).Value = "Mean" 'From Value Type
                        dgvProbabilities.Rows(RowNo).Cells(1).Value = "" 'From Value Parameter - no parameter required.
                        dgvProbabilities.Rows(RowNo).Cells(2).Value = Mean 'From Value - Mean.
                        dgvProbabilities.Rows(RowNo).Cells(3).Value = Distribution.Info(SelDistrib).CDF(Mean) 'From Value CDF. 
                        If dgvProbabilities.Rows(RowNo).Cells(3).ValueType = GetType(String) Then 'If the Value Type is a string, then the probability can not be calculated.
                            'The To Value CDF is not defined.
                            dgvProbabilities.Rows(RowNo).Cells(8).Value = "" 'The Probability can not be calculated.
                        Else
                            'Calculate the Probability that the Random Variable will lie between From Value and To Value:
                            dgvProbabilities.Rows(RowNo).Cells(8).Value = dgvProbabilities.Rows(RowNo).Cells(7).Value - dgvProbabilities.Rows(RowNo).Cells(3).Value
                        End If
                        'UpdateXmlAreaAnnotInfo(RowNo)
                    End If

                Case "Median"
                    Dim Median As Double = Distribution.Info(SelDistrib).Median
                    If RowNo > dgvProbabilities.RowCount - 1 Then  'Add a new row.
                        Dim P90Value As Double = Distribution.Info(SelDistrib).InvCDF(0.9)   'Default probability value: 0.9 (P90)
                        dgvProbabilities.Rows.Add({"Median", "", Median, Distribution.Info(SelDistrib).CDF(Median), "Probability <=", 0.9, Distribution.Info(SelDistrib).InvCDF(0.9), 0.9, 0.9 - Distribution.Info(SelDistrib).CDF(Median)})
                        dgvProbabilities.Rows(RowNo).Cells(9).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * 100
                        dgvProbabilities.Rows(RowNo).Cells(10).Value = 1 / dgvProbabilities.Rows(RowNo).Cells(8).Value
                        If txtTotalPopulation.Text <> "" Then
                            'dgvProbabilities.Rows(RowNo).Cells(10).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * Val(txtTotalPopulation.Text)
                            dgvProbabilities.Rows(RowNo).Cells(11).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * Val(txtTotalPopulation.Text.Replace(",", ""))
                        End If
                        'dgvProbabilities.Rows(RowNo).Cells(10).Style.BackColor = Color.Red
                        'AddXmlAreaAnnotInfo(RowNo)
                    Else
                        'dgvProbabilities.Rows(RowNo).Cells(0).Value = True 'Show
                        dgvProbabilities.Rows(RowNo).Cells(0).Value = "Median" 'From Value Type
                        dgvProbabilities.Rows(RowNo).Cells(1).Value = "" 'From Value Parameter - no parameter required.
                        dgvProbabilities.Rows(RowNo).Cells(2).Value = Median 'From Value - Mean.
                        dgvProbabilities.Rows(RowNo).Cells(3).Value = Distribution.Info(SelDistrib).CDF(Median) 'From Value CDF. 
                        If dgvProbabilities.Rows(RowNo).Cells(3).ValueType = GetType(String) Then 'If the Value Type is a string, then the probability can not be calculated.
                            'The To Value CDF is not defined.
                            dgvProbabilities.Rows(RowNo).Cells(8).Value = "" 'The Probability can not be calculated.
                        Else
                            'Calculate the Probability that the Random Variable will lie between From Value and To Value:
                            dgvProbabilities.Rows(RowNo).Cells(8).Value = dgvProbabilities.Rows(RowNo).Cells(7).Value - dgvProbabilities.Rows(RowNo).Cells(3).Value
                        End If
                        'UpdateXmlAreaAnnotInfo(RowNo)
                    End If

                Case "Mode"
                    Dim Mode As Double = Distribution.Info(SelDistrib).Mode
                    If RowNo > dgvProbabilities.RowCount - 1 Then  'Add a new row.
                        Dim P90Value As Double = Distribution.Info(SelDistrib).InvCDF(0.9)   'Default probability value: 0.9 (P90)
                        dgvProbabilities.Rows.Add({"Mode", "", Mode, Distribution.Info(SelDistrib).CDF(Mode), "Probability <=", 0.9, Distribution.Info(SelDistrib).InvCDF(0.9), 0.9, 0.9 - Distribution.Info(SelDistrib).CDF(Mode)})
                        dgvProbabilities.Rows(RowNo).Cells(9).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * 100
                        dgvProbabilities.Rows(RowNo).Cells(10).Value = 1 / dgvProbabilities.Rows(RowNo).Cells(8).Value
                        If txtTotalPopulation.Text <> "" Then
                            'dgvProbabilities.Rows(RowNo).Cells(10).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * Val(txtTotalPopulation.Text)
                            dgvProbabilities.Rows(RowNo).Cells(11).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * Val(txtTotalPopulation.Text.Replace(",", ""))
                        End If
                        'dgvProbabilities.Rows(RowNo).Cells(10).Style.BackColor = Color.Red
                        'AddXmlAreaAnnotInfo(RowNo)
                    Else
                        'dgvProbabilities.Rows(RowNo).Cells(0).Value = True 'Show
                        dgvProbabilities.Rows(RowNo).Cells(0).Value = "Mode" 'From Value Type
                        dgvProbabilities.Rows(RowNo).Cells(1).Value = "" 'From Value Parameter - no parameter required.
                        dgvProbabilities.Rows(RowNo).Cells(2).Value = Mode 'From Value - Mean.
                        dgvProbabilities.Rows(RowNo).Cells(3).Value = Distribution.Info(SelDistrib).CDF(Mode) 'From Value CDF. 
                        If dgvProbabilities.Rows(RowNo).Cells(3).ValueType = GetType(String) Then 'If the Value Type is a string, then the probability can not be calculated.
                            'The To Value CDF is not defined.
                            dgvProbabilities.Rows(RowNo).Cells(8).Value = "" 'The Probability can not be calculated.
                        Else
                            'Calculate the Probability that the Random Variable will lie between From Value and To Value:
                            dgvProbabilities.Rows(RowNo).Cells(8).Value = dgvProbabilities.Rows(RowNo).Cells(7).Value - dgvProbabilities.Rows(RowNo).Cells(3).Value
                        End If
                        'UpdateXmlAreaAnnotInfo(RowNo)
                    End If

                Case "Standard Deviation"
                    Dim Mean As Double = Distribution.Info(SelDistrib).Mean
                    Dim StdDev As Double = Distribution.Info(SelDistrib).StdDev 'The Standard Deviation of the series

                    Dim SDevN As Integer
                    Dim ValFound As Boolean
                    'Find an unused negative standard deviation number
                    Dim Row As Integer
                    For SDevN = -1 To -dgvProbabilities.RowCount Step -1
                        ValFound = False
                        For Row = 0 To dgvProbabilities.RowCount - 1
                            If dgvProbabilities.Rows(Row).Cells(0).Value = "Standard Deviation" Then
                                If dgvProbabilities.Rows(Row).Cells(1).Value = SDevN Then
                                    ValFound = True
                                    Exit For
                                End If
                            End If
                        Next
                        If ValFound = False Then Exit For 'SDevN contains an unused negative standard deviation numner
                    Next

                    Dim SDevNValue = Mean + SDevN * StdDev

                    If RowNo > dgvProbabilities.RowCount - 1 Then 'Add new row
                        'Use Mean as the default To Value
                        dgvProbabilities.Rows.Add({"Standard Deviation", SDevN, SDevNValue, Distribution.Info(SelDistrib).CDF(SDevNValue), "Mean", "", Mean, Distribution.Info(SelDistrib).CDF(Mean), Distribution.Info(SelDistrib).CDF(Mean) - Distribution.Info(SelDistrib).CDF(SDevNValue)})
                        dgvProbabilities.Rows(RowNo).Cells(9).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * 100
                        dgvProbabilities.Rows(RowNo).Cells(10).Value = 1 / dgvProbabilities.Rows(RowNo).Cells(8).Value
                        If txtTotalPopulation.Text <> "" Then
                            'dgvProbabilities.Rows(RowNo).Cells(10).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * Val(txtTotalPopulation.Text)
                            dgvProbabilities.Rows(RowNo).Cells(11).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * Val(txtTotalPopulation.Text.Replace(",", ""))
                        End If
                        'dgvProbabilities.Rows(RowNo).Cells(10).Style.BackColor = Color.Red
                        'AddXmlAreaAnnotInfo(RowNo)
                    Else 'Update row
                        'dgvProbabilities.Rows(RowNo).Cells(0).Value = True 'Show
                        dgvProbabilities.Rows(RowNo).Cells(0).Value = "Standard Deviation" 'From Value Type
                        dgvProbabilities.Rows(RowNo).Cells(1).Value = SDevN 'From Value Parameter
                        dgvProbabilities.Rows(RowNo).Cells(2).Value = SDevNValue 'From Value 
                        dgvProbabilities.Rows(RowNo).Cells(3).Value = Distribution.Info(SelDistrib).CDF(SDevNValue) 'From Value CDF. 
                        If dgvProbabilities.Rows(RowNo).Cells(3).ValueType = GetType(String) Then 'If the Value Type is a string, then the probability can not be calculated.
                            'The To Value CDF is not defined.
                            dgvProbabilities.Rows(RowNo).Cells(8).Value = "" 'The Probability can not be calculated.
                        Else
                            'Calculate the Probability that the Random Variable will lie between From Value and To Value:
                            dgvProbabilities.Rows(RowNo).Cells(8).Value = dgvProbabilities.Rows(RowNo).Cells(7).Value - dgvProbabilities.Rows(RowNo).Cells(3).Value
                        End If
                        'UpdateXmlAreaAnnotInfo(RowNo)
                    End If

                    'Case "User Defined Value 1"
                    '    Dim UserDef1 As Double = Val(txtUserDef1.Text)

                    '    If RowNo > dgvAreaAnnot.RowCount - 1 Then
                    '        Dim Mean As Double = SelectedDistrib.Mean 'Use the Mean value as the To Value as the Default.
                    '        dgvAreaAnnot.Rows.Add({True, "User Defined Value 1", UserDef1, UserDef1, SelectedDistrib.CDF(UserDef1), "Mean", "", Mean, SelectedDistrib.CDF(Mean), SelectedDistrib.CDF(Mean) - SelectedDistrib.CDF(UserDef1), "", 1, 2, "50"}) 'From Value Type | From Value Parameter | From Value | From Value CDF | To Value Type | To Value Parameter | To Value | To Value CDF | Probability | Color | Thickness | Density | Intensity
                    '        dgvAreaAnnot.Rows(RowNo).Cells(10).Style.BackColor = Color.Red
                    '        'AddXmlAreaAnnotInfo(RowNo)
                    '    Else
                    '        'From Value Type | From Value Parameter | From Value | From Value CDF | To Value Type | To Value Parameter | To Value | To Value CDF | Probability | Color | Thickness | Density
                    '        dgvAreaAnnot.Rows(RowNo).Cells(0).Value = True 'Show
                    '        dgvAreaAnnot.Rows(RowNo).Cells(1).Value = "User Defined Value 1" 'From Value Type
                    '        dgvAreaAnnot.Rows(RowNo).Cells(2).Value = UserDef1 'From Value Parameter - use default value of 0.1.
                    '        dgvAreaAnnot.Rows(RowNo).Cells(3).Value = UserDef1 'From Value - Default: P10Value.
                    '        dgvAreaAnnot.Rows(RowNo).Cells(4).Value = SelectedDistrib.CDF(UserDef1) 'From Value CDF. 
                    '        If dgvAreaAnnot.Rows(RowNo).Cells(4).ValueType = GetType(String) Then 'If the Value Type is a string, then the probability can not be calculated.
                    '            'The To Value CDF is not defined.
                    '            dgvAreaAnnot.Rows(RowNo).Cells(9).Value = "" 'The Probability can not be calculated.
                    '        Else
                    '            'Calculate the Probability that the Random Variable will lie between From Value and To Value:
                    '            dgvAreaAnnot.Rows(RowNo).Cells(9).Value = dgvAreaAnnot.Rows(RowNo).Cells(8).Value - dgvAreaAnnot.Rows(RowNo).Cells(4).Value
                    '        End If
                    '        'UpdateXmlAreaAnnotInfo(RowNo)
                    '    End If

                    'Case "User Defined Value 2"
                    '    Dim UserDef2 As Double = Val(txtUserDef2.Text)

                    '    If RowNo > dgvAreaAnnot.RowCount - 1 Then
                    '        Dim Mean As Double = SelectedDistrib.Mean 'Use the Mean value as the To Value as the Default.
                    '        dgvAreaAnnot.Rows.Add({True, "User Defined Value 2", UserDef2, UserDef2, SelectedDistrib.CDF(UserDef2), "Mean", "", Mean, SelectedDistrib.CDF(Mean), SelectedDistrib.CDF(Mean) - SelectedDistrib.CDF(UserDef2), "", 1, 2, "50"}) 'From Value Type | From Value Parameter | From Value | From Value CDF | To Value Type | To Value Parameter | To Value | To Value CDF | Probability | Color | Thickness | Density | Intensity
                    '        dgvAreaAnnot.Rows(RowNo).Cells(10).Style.BackColor = Color.Red
                    '        'AddXmlAreaAnnotInfo(RowNo)
                    '    Else
                    '        dgvAreaAnnot.Rows(RowNo).Cells(0).Value = True 'Show
                    '        dgvAreaAnnot.Rows(RowNo).Cells(1).Value = "User Defined Value 2" 'From Value Type
                    '        dgvAreaAnnot.Rows(RowNo).Cells(2).Value = UserDef2 'From Value Parameter - use default value of 0.1.
                    '        dgvAreaAnnot.Rows(RowNo).Cells(3).Value = UserDef2 'From Value - Default: P10Value.
                    '        dgvAreaAnnot.Rows(RowNo).Cells(4).Value = SelectedDistrib.CDF(UserDef2) 'From Value CDF. 
                    '        If dgvAreaAnnot.Rows(RowNo).Cells(4).ValueType = GetType(String) Then 'If the Value Type is a string, then the probability can not be calculated.
                    '            'The To Value CDF is not defined.
                    '            dgvAreaAnnot.Rows(RowNo).Cells(9).Value = "" 'The Probability can not be calculated.
                    '        Else
                    '            'Calculate the Probability that the Random Variable will lie between From Value and To Value:
                    '            dgvAreaAnnot.Rows(RowNo).Cells(9).Value = dgvAreaAnnot.Rows(RowNo).Cells(8).Value - dgvAreaAnnot.Rows(RowNo).Cells(4).Value
                    '        End If
                    '        'UpdateXmlAreaAnnotInfo(RowNo)
                    '    End If

            End Select

        ElseIf combo.Name = "cboToValueType" Then
            Select Case combo.SelectedItem.ToString
                Case "Maximum"
                    If RowNo > dgvProbabilities.RowCount - 1 Then  'Add a new row.
                        Dim Mean As Double = Distribution.Info(SelDistrib).Mean 'Use the Mean value as the From Value as the Default.
                        dgvProbabilities.Rows.Add({"Mean", "", Mean, Distribution.Info(SelDistrib).CDF(Mean), "Maximum", "", "", 1, 1 - Distribution.Info(SelDistrib).CDF(Mean)})
                        dgvProbabilities.Rows(RowNo).Cells(9).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * 100
                        dgvProbabilities.Rows(RowNo).Cells(10).Value = 1 / dgvProbabilities.Rows(RowNo).Cells(8).Value
                        If txtTotalPopulation.Text <> "" Then
                            'dgvProbabilities.Rows(RowNo).Cells(10).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * Val(txtTotalPopulation.Text)
                            dgvProbabilities.Rows(RowNo).Cells(11).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * Val(txtTotalPopulation.Text.Replace(",", ""))
                        End If
                        'dgvProbabilities.Rows(RowNo).Cells(10).Style.BackColor = Color.Red
                        'AddXmlAreaAnnotInfo(RowNo)
                    Else
                        dgvProbabilities.Rows(RowNo).Cells(4).Value = "Maximum" 'To Value Type
                        dgvProbabilities.Rows(RowNo).Cells(5).Value = "" 'To Value Parameter - not required.
                        dgvProbabilities.Rows(RowNo).Cells(6).Value = "" 'To Value - not required.
                        dgvProbabilities.Rows(RowNo).Cells(7).Value = 1 'To Value CDF - One.
                        If dgvProbabilities.Rows(RowNo).Cells(3).ValueType = GetType(String) Then 'If the Value Type is a string, then the probability can not be calculated.
                            'The To Value CDF is not defined.
                            dgvProbabilities.Rows(RowNo).Cells(8).Value = "" 'The Probability can not be calculated.
                        Else
                            'Calculate the Probability that the Random Variable will lie between From Value and To Value:
                            dgvProbabilities.Rows(RowNo).Cells(8).Value = dgvProbabilities.Rows(RowNo).Cells(7).Value - dgvProbabilities.Rows(RowNo).Cells(3).Value
                        End If
                        'UpdateXmlAreaAnnotInfo(RowNo)
                    End If

                Case "Probability <="
                    Dim P90Value As Double = Distribution.Info(SelDistrib).InvCDF(0.9)   'Default probability value: 0.9 (P90)
                    If RowNo > dgvProbabilities.RowCount - 1 Then 'Add a new row.
                        Dim Mean As Double = Distribution.Info(SelDistrib).Mean 'Use the Mean value as the From Value as the Default.
                        dgvProbabilities.Rows.Add({"Mean", "", Mean, Distribution.Info(SelDistrib).CDF(Mean), "Probability <=", 0.9, P90Value, 0.9, 0.9 - Distribution.Info(SelDistrib).CDF(Mean)})
                        dgvProbabilities.Rows(RowNo).Cells(9).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * 100
                        dgvProbabilities.Rows(RowNo).Cells(10).Value = 1 / dgvProbabilities.Rows(RowNo).Cells(8).Value
                        If txtTotalPopulation.Text <> "" Then
                            'dgvProbabilities.Rows(RowNo).Cells(10).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * Val(txtTotalPopulation.Text)
                            dgvProbabilities.Rows(RowNo).Cells(11).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * Val(txtTotalPopulation.Text.Replace(",", ""))
                        End If
                        'dgvProbabilities.Rows(RowNo).Cells(10).Style.BackColor = Color.Red
                        'AddXmlAreaAnnotInfo(RowNo)
                    Else
                        dgvProbabilities.Rows(RowNo).Cells(4).Value = "Probability <=" 'To Value Type
                        dgvProbabilities.Rows(RowNo).Cells(5).Value = 0.9 'To Value Parameter - Default: 0.9.
                        dgvProbabilities.Rows(RowNo).Cells(6).Value = P90Value 'To Value - Default: P90Value.
                        dgvProbabilities.Rows(RowNo).Cells(7).Value = 0.9 'To Value CDF - Default 0.9.
                        If dgvProbabilities.Rows(RowNo).Cells(3).ValueType = GetType(String) Then 'If the Value Type is a string, then the probability can not be calculated.
                            'The To Value CDF is not defined.
                            dgvProbabilities.Rows(RowNo).Cells(8).Value = "" 'The Probability can not be calculated.
                        Else
                            'Calculate the Probability that the Random Variable will lie between From Value and To Value:
                            dgvProbabilities.Rows(RowNo).Cells(8).Value = dgvProbabilities.Rows(RowNo).Cells(7).Value - dgvProbabilities.Rows(RowNo).Cells(3).Value
                        End If
                        'UpdateXmlAreaAnnotInfo(RowNo)
                    End If

                Case "Probability >"
                    Dim P10Value As Double = GetInvRevCdfValue(0.1)  'Default probability value: 0.1 (P10)
                    If RowNo > dgvProbabilities.RowCount - 1 Then 'Add a new row.
                        Dim Mean As Double = Distribution.Info(SelDistrib).Mean 'Use the Mean value as the From Value as the Default.
                        'Note for RevCDF = 0.1, the corresponding CDF = 0.9
                        dgvProbabilities.Rows.Add({"Mean", "", Mean, Distribution.Info(SelDistrib).CDF(Mean), "Probability >", 0.1, P10Value, 0.1, 0.9 - Distribution.Info(SelDistrib).CDF(Mean)})
                        dgvProbabilities.Rows(RowNo).Cells(9).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * 100
                        dgvProbabilities.Rows(RowNo).Cells(10).Value = 1 / dgvProbabilities.Rows(RowNo).Cells(8).Value
                        If txtTotalPopulation.Text <> "" Then
                            'dgvProbabilities.Rows(RowNo).Cells(10).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * Val(txtTotalPopulation.Text)
                            dgvProbabilities.Rows(RowNo).Cells(11).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * Val(txtTotalPopulation.Text.Replace(",", ""))
                        End If
                        'dgvProbabilities.Rows(RowNo).Cells(10).Style.BackColor = Color.Red
                        'AddXmlAreaAnnotInfo(RowNo)
                    Else
                        dgvProbabilities.Rows(RowNo).Cells(4).Value = "Probability >" 'To Value Type
                        dgvProbabilities.Rows(RowNo).Cells(5).Value = 0.1 'To Value Parameter - Default: 0.1.
                        dgvProbabilities.Rows(RowNo).Cells(6).Value = P10Value 'To Value - Default: P10Value.
                        dgvProbabilities.Rows(RowNo).Cells(7).Value = 0.9 'To Value CDF - Default 0.9.
                        If dgvProbabilities.Rows(RowNo).Cells(3).ValueType = GetType(String) Then 'If the Value Type is a string, then the probability can not be calculated.
                            'The To Value CDF is not defined.
                            dgvProbabilities.Rows(RowNo).Cells(8).Value = "" 'The Probability can not be calculated.
                        Else
                            'Calculate the Probability that the Random Variable will lie between From Value and To Value:
                            dgvProbabilities.Rows(RowNo).Cells(8).Value = dgvProbabilities.Rows(RowNo).Cells(7).Value - dgvProbabilities.Rows(RowNo).Cells(3).Value
                        End If
                        'UpdateXmlAreaAnnotInfo(RowNo)
                    End If

                Case "Random Variable Value"
                    'Dim P90Value As Double = Distribution.Info(SelDistrib).CDF(0.9)   'Default probability value: 0.9 (P90)
                    Dim P90Value As Double = Distribution.Info(SelDistrib).InvCDF(0.9)   'Default probability value: 0.9 (P90)
                    If RowNo > dgvProbabilities.RowCount - 1 Then 'Add a new row.
                        Dim Mean As Double = Distribution.Info(SelDistrib).Mean 'Use the Mean value as the From Value as the Default.
                        dgvProbabilities.Rows.Add({"Mean", "", Mean, Distribution.Info(SelDistrib).CDF(Mean), "Random Variable Value", 0.9, P90Value, 0.9, 0.9 - Distribution.Info(SelDistrib).CDF(Mean)})
                        dgvProbabilities.Rows(RowNo).Cells(9).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * 100
                        dgvProbabilities.Rows(RowNo).Cells(10).Value = 1 / dgvProbabilities.Rows(RowNo).Cells(8).Value
                        If txtTotalPopulation.Text <> "" Then
                            'dgvProbabilities.Rows(RowNo).Cells(10).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * Val(txtTotalPopulation.Text)
                            dgvProbabilities.Rows(RowNo).Cells(11).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * Val(txtTotalPopulation.Text.Replace(",", ""))
                        End If
                        'dgvProbabilities.Rows(RowNo).Cells(10).Style.BackColor = Color.Red
                        'AddXmlAreaAnnotInfo(RowNo)
                    Else
                        dgvProbabilities.Rows(RowNo).Cells(4).Value = "Random Variable Value" 'To Value Type
                        'dgvProbabilities.Rows(RowNo).Cells(5).Value = 0.9 'To Value Parameter - Default: 0.9.
                        dgvProbabilities.Rows(RowNo).Cells(5).Value = P90Value 'To Value - Default: P90Value.
                        dgvProbabilities.Rows(RowNo).Cells(6).Value = P90Value 'To Value - Default: P90Value.
                        dgvProbabilities.Rows(RowNo).Cells(7).Value = 0.9 'To Value CDF - Default 0.9.
                        If dgvProbabilities.Rows(RowNo).Cells(3).ValueType = GetType(String) Then 'If the Value Type is a string, then the probability can not be calculated.
                            'The To Value CDF is not defined.
                            dgvProbabilities.Rows(RowNo).Cells(8).Value = "" 'The Probability can not be calculated.
                        Else
                            dgvProbabilities.Rows(RowNo).Cells(8).Value = dgvProbabilities.Rows(RowNo).Cells(7).Value - dgvProbabilities.Rows(RowNo).Cells(3).Value
                        End If
                        'UpdateXmlAreaAnnotInfo(RowNo)
                    End If

                Case "Mean"
                    Dim Mean As Double = Distribution.Info(SelDistrib).Mean
                    If RowNo > dgvProbabilities.RowCount - 1 Then 'Add a new row.
                        Dim P10Value As Double = Distribution.Info(SelDistrib).InvCDF(0.1)   'Default probability value: 0.1 (P10)
                        dgvProbabilities.Rows.Add({"Probability <=", 0.1, P10Value, 0.1, "Mean", "", Mean, Distribution.Info(SelDistrib).CDF(Mean), Distribution.Info(SelDistrib).CDF(Mean) - 0.1})
                        dgvProbabilities.Rows(RowNo).Cells(9).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * 100
                        dgvProbabilities.Rows(RowNo).Cells(10).Value = 1 / dgvProbabilities.Rows(RowNo).Cells(8).Value
                        If txtTotalPopulation.Text <> "" Then
                            'dgvProbabilities.Rows(RowNo).Cells(10).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * Val(txtTotalPopulation.Text)
                            dgvProbabilities.Rows(RowNo).Cells(11).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * Val(txtTotalPopulation.Text.Replace(",", ""))
                        End If
                        'dgvProbabilities.Rows(RowNo).Cells(10).Style.BackColor = Color.Red
                        'AddXmlAreaAnnotInfo(RowNo)
                    Else
                        dgvProbabilities.Rows(RowNo).Cells(4).Value = "Mean" 'To Value Type
                        dgvProbabilities.Rows(RowNo).Cells(5).Value = "" 'To Value Parameter - no parameter required.
                        dgvProbabilities.Rows(RowNo).Cells(6).Value = Mean 'To Value - Mean.
                        dgvProbabilities.Rows(RowNo).Cells(7).Value = Distribution.Info(SelDistrib).CDF(Mean) 'To Value CDF
                        If dgvProbabilities.Rows(RowNo).Cells(3).ValueType = GetType(String) Then 'If the Value Type is a string, then the probability can not be calculated.
                            'The To Value CDF is not defined.
                            dgvProbabilities.Rows(RowNo).Cells(8).Value = "" 'The Probability can not be calculated.
                        Else
                            'Calculate the Probability that the Random Variable will lie between From Value and To Value:
                            dgvProbabilities.Rows(RowNo).Cells(8).Value = dgvProbabilities.Rows(RowNo).Cells(7).Value - dgvProbabilities.Rows(RowNo).Cells(3).Value
                        End If
                        'UpdateXmlAreaAnnotInfo(RowNo)
                    End If

                Case "Median"
                    Dim Median As Double = Distribution.Info(SelDistrib).Median
                    If RowNo > dgvProbabilities.RowCount - 1 Then 'Add a new row.
                        Dim P10Value As Double = Distribution.Info(SelDistrib).InvCDF(0.1)   'Default probability value: 0.1 (P10)
                        dgvProbabilities.Rows.Add({"Probability <=", 0.1, P10Value, 0.1, "Median", "", Median, Distribution.Info(SelDistrib).CDF(Median), Distribution.Info(SelDistrib).CDF(Median) - 0.1})
                        dgvProbabilities.Rows(RowNo).Cells(9).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * 100
                        dgvProbabilities.Rows(RowNo).Cells(10).Value = 1 / dgvProbabilities.Rows(RowNo).Cells(8).Value
                        If txtTotalPopulation.Text <> "" Then
                            'dgvProbabilities.Rows(RowNo).Cells(10).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * Val(txtTotalPopulation.Text)
                            dgvProbabilities.Rows(RowNo).Cells(11).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * Val(txtTotalPopulation.Text.Replace(",", ""))
                        End If
                        'dgvAreaAnnot.Rows(RowNo).Cells(10).Style.BackColor = Color.Red
                        'AddXmlAreaAnnotInfo(RowNo)
                    Else
                        dgvProbabilities.Rows(RowNo).Cells(4).Value = "Median" 'To Value Type
                        dgvProbabilities.Rows(RowNo).Cells(5).Value = "" 'To Value Parameter - no parameter required.
                        dgvProbabilities.Rows(RowNo).Cells(6).Value = Median 'To Value - Median.
                        dgvProbabilities.Rows(RowNo).Cells(7).Value = Distribution.Info(SelDistrib).CDF(Median) 'To Value CDF
                        If dgvProbabilities.Rows(RowNo).Cells(3).ValueType = GetType(String) Then 'If the Value Type is a string, then the probability can not be calculated.
                            'The To Value CDF is not defined.
                            dgvProbabilities.Rows(RowNo).Cells(8).Value = "" 'The Probability can not be calculated.
                        Else
                            'Calculate the Probability that the Random Variable will lie between From Value and To Value:
                            dgvProbabilities.Rows(RowNo).Cells(8).Value = dgvProbabilities.Rows(RowNo).Cells(7).Value - dgvProbabilities.Rows(RowNo).Cells(3).Value
                        End If
                        'UpdateXmlAreaAnnotInfo(RowNo)
                    End If

                Case "Mode"
                    Dim Mode As Double = Distribution.Info(SelDistrib).Mode
                    If RowNo > dgvProbabilities.RowCount - 1 Then 'Add a new row.
                        Dim P10Value As Double = Distribution.Info(SelDistrib).InvCDF(0.1)   'Default probability value: 0.1 (P10)
                        dgvProbabilities.Rows.Add({"Probability <=", 0.1, P10Value, 0.1, "Mode", "", Mode, Distribution.Info(SelDistrib).CDF(Mode), Distribution.Info(SelDistrib).CDF(Mode) - 0.1})
                        dgvProbabilities.Rows(RowNo).Cells(9).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * 100
                        dgvProbabilities.Rows(RowNo).Cells(10).Value = 1 / dgvProbabilities.Rows(RowNo).Cells(8).Value
                        If txtTotalPopulation.Text <> "" Then
                            'dgvProbabilities.Rows(RowNo).Cells(10).Value = Int(dgvProbabilities.Rows(RowNo).Cells(8).Value * Val(txtTotalPopulation.Text))
                            'dgvProbabilities.Rows(RowNo).Cells(10).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * Val(txtTotalPopulation.Text)
                            dgvProbabilities.Rows(RowNo).Cells(11).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * Val(txtTotalPopulation.Text.Replace(",", ""))
                        End If
                        'dgvProbabilities.Rows(RowNo).Cells(10).Style.BackColor = Color.Red
                        'AddXmlAreaAnnotInfo(RowNo)
                    Else
                        dgvProbabilities.Rows(RowNo).Cells(4).Value = "Mode" 'To Value Type
                        dgvProbabilities.Rows(RowNo).Cells(5).Value = "" 'To Value Parameter - no parameter required.
                        dgvProbabilities.Rows(RowNo).Cells(6).Value = Mode 'To Value - Mode.
                        dgvProbabilities.Rows(RowNo).Cells(7).Value = Distribution.Info(SelDistrib).CDF(Mode) 'To Value CDF
                        If dgvProbabilities.Rows(RowNo).Cells(3).ValueType = GetType(String) Then 'If the Value Type is a string, then the probability can not be calculated.
                            'The To Value CDF is not defined.
                            dgvProbabilities.Rows(RowNo).Cells(8).Value = "" 'The Probability can not be calculated.
                        Else
                            'Calculate the Probability that the Random Variable will lie between From Value and To Value:
                            dgvProbabilities.Rows(RowNo).Cells(8).Value = dgvProbabilities.Rows(RowNo).Cells(7).Value - dgvProbabilities.Rows(RowNo).Cells(3).Value
                        End If
                        'UpdateXmlAreaAnnotInfo(RowNo)
                    End If

                Case "Standard Deviation"
                    Dim Mean As Double = Distribution.Info(SelDistrib).Mean
                    Dim StdDev As Double = Distribution.Info(SelDistrib).StdDev 'The Standard Deviation of the series

                    Dim SDev As Integer
                    Dim ValFound As Boolean
                    Dim Row As Integer
                    For SDev = 1 To dgvProbabilities.RowCount
                        ValFound = False
                        For Row = 0 To dgvProbabilities.RowCount - 1
                            If dgvProbabilities.Rows(Row).Cells(4).Value = "Standard Deviation" Then
                                If dgvProbabilities.Rows(Row).Cells(5).Value = SDev Then
                                    ValFound = True
                                    Exit For
                                End If
                            End If
                        Next
                        If ValFound = False Then Exit For 'SDev contains an unused standard deviation number.
                    Next

                    Dim SDevValue = Mean + SDev * StdDev

                    If RowNo > dgvProbabilities.RowCount - 1 Then 'Add new row
                        Dim P10Value As Double = Distribution.Info(SelDistrib).InvCDF(0.1)   'Default probability value: 0.1 (P10)
                        dgvProbabilities.Rows.Add({"Mean", "", Mean, Distribution.Info(SelDistrib).CDF(Mean), "Standard Deviation", SDev, SDevValue, Distribution.Info(SelDistrib).CDF(SDevValue), Distribution.Info(SelDistrib).CDF(SDevValue) - Distribution.Info(SelDistrib).CDF(Mean)})
                        dgvProbabilities.Rows(RowNo).Cells(9).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * 100
                        dgvProbabilities.Rows(RowNo).Cells(10).Value = 1 / dgvProbabilities.Rows(RowNo).Cells(8).Value
                        If txtTotalPopulation.Text <> "" Then
                            'dgvProbabilities.Rows(RowNo).Cells(10).Value = Int(dgvProbabilities.Rows(RowNo).Cells(8).Value * Val(txtTotalPopulation.Text))
                            'dgvProbabilities.Rows(RowNo).Cells(10).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * Val(txtTotalPopulation.Text)
                            dgvProbabilities.Rows(RowNo).Cells(11).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * Val(txtTotalPopulation.Text.Replace(",", ""))
                        End If
                        'dgvProbabilities.Rows(RowNo).Cells(10).Style.BackColor = Color.Red
                        'AddXmlAreaAnnotInfo(RowNo)
                    Else 'Update row
                        dgvProbabilities.Rows(RowNo).Cells(4).Value = "Standard Deviation" 'To Value Type
                        dgvProbabilities.Rows(RowNo).Cells(5).Value = SDev 'To Value Parameter
                        dgvProbabilities.Rows(RowNo).Cells(6).Value = SDevValue 'To Value
                        dgvProbabilities.Rows(RowNo).Cells(7).Value = Distribution.Info(SelDistrib).CDF(SDevValue) 'To Value CDF
                        If dgvProbabilities.Rows(RowNo).Cells(3).ValueType = GetType(String) Then 'If the Value Type is a string, then the probability can not be calculated.
                            'The To Value CDF is not defined.
                            dgvProbabilities.Rows(RowNo).Cells(8).Value = "" 'The Probability can not be calculated.
                        Else
                            'Calculate the Probability that the Random Variable will lie between From Value and To Value:
                            dgvProbabilities.Rows(RowNo).Cells(8).Value = dgvProbabilities.Rows(RowNo).Cells(7).Value - dgvProbabilities.Rows(RowNo).Cells(3).Value
                        End If
                        'UpdateXmlAreaAnnotInfo(RowNo)
                    End If

                    'Case "User Defined Value 1"
                    '    Dim UserDef1 As Double = Val(txtUserDef1.Text)

                    '    If RowNo > dgvAreaAnnot.RowCount - 1 Then
                    '        Dim Mean As Double = SelectedDistrib.Mean 'Use the Mean value as the To Value as the Default.
                    '        dgvAreaAnnot.Rows.Add({True, "Mean", "", Mean, SelectedDistrib.CDF(Mean), "User Defined Value 1", UserDef1, UserDef1, SelectedDistrib.CDF(UserDef1), SelectedDistrib.CDF(UserDef1) - SelectedDistrib.CDF(Mean), "", 1, 2, "50"}) 'From Value Type | From Value Parameter | From Value | From Value CDF | To Value Type | To Value Parameter | To Value | To Value CDF | Probability | Color | Thickness | Density | Intensity
                    '        dgvAreaAnnot.Rows(RowNo).Cells(10).Style.BackColor = Color.Red
                    '        AddXmlAreaAnnotInfo(RowNo)
                    '    Else
                    '        dgvAreaAnnot.Rows(RowNo).Cells(5).Value = "User Defined Value 1" 'From Value Type
                    '        dgvAreaAnnot.Rows(RowNo).Cells(6).Value = UserDef1 'From Value Parameter - use default value of 0.1.
                    '        dgvAreaAnnot.Rows(RowNo).Cells(7).Value = UserDef1 'From Value - Default: P10Value.
                    '        dgvAreaAnnot.Rows(RowNo).Cells(8).Value = SelectedDistrib.CDF(UserDef1) 'From Value CDF. 
                    '        If dgvAreaAnnot.Rows(RowNo).Cells(4).ValueType = GetType(String) Then 'If the Value Type is a string, then the probability can not be calculated.
                    '            'The To Value CDF is not defined.
                    '            dgvAreaAnnot.Rows(RowNo).Cells(9).Value = "" 'The Probability can not be calculated.
                    '        Else
                    '            'Calculate the Probability that the Random Variable will lie between From Value and To Value:
                    '            dgvAreaAnnot.Rows(RowNo).Cells(9).Value = dgvAreaAnnot.Rows(RowNo).Cells(8).Value - dgvAreaAnnot.Rows(RowNo).Cells(4).Value
                    '        End If
                    '        UpdateXmlAreaAnnotInfo(RowNo)
                    '    End If

                    'Case "User Defined Value 2"
                    '    Dim UserDef2 As Double = Val(txtUserDef2.Text)

                    '    If RowNo > dgvAreaAnnot.RowCount - 1 Then
                    '        Dim Mean As Double = SelectedDistrib.Mean 'Use the Mean value as the To Value as the Default.
                    '        dgvAreaAnnot.Rows.Add({True, "Mean", "", Mean, SelectedDistrib.CDF(Mean), "User Defined Value 2", UserDef2, UserDef2, SelectedDistrib.CDF(UserDef2), SelectedDistrib.CDF(UserDef2) - SelectedDistrib.CDF(Mean), "", 1, 2, "50"}) 'From Value Type | From Value Parameter | From Value | From Value CDF | To Value Type | To Value Parameter | To Value | To Value CDF | Probability | Color | Thickness | Density | Intensity
                    '        dgvAreaAnnot.Rows(RowNo).Cells(10).Style.BackColor = Color.Red
                    '        AddXmlAreaAnnotInfo(RowNo)
                    '    Else
                    '        dgvAreaAnnot.Rows(RowNo).Cells(5).Value = "User Defined Value 2" 'From Value Type
                    '        dgvAreaAnnot.Rows(RowNo).Cells(6).Value = UserDef2 'From Value Parameter - use default value of 0.1.
                    '        dgvAreaAnnot.Rows(RowNo).Cells(7).Value = UserDef2 'From Value - Default: P10Value.
                    '        dgvAreaAnnot.Rows(RowNo).Cells(8).Value = SelectedDistrib.CDF(UserDef2) 'From Value CDF. 
                    '        If dgvAreaAnnot.Rows(RowNo).Cells(4).ValueType = GetType(String) Then 'If the Value Type is a string, then the probability can not be calculated.
                    '            'The To Value CDF is not defined.
                    '            dgvAreaAnnot.Rows(RowNo).Cells(9).Value = "" 'The Probability can not be calculated.
                    '        Else
                    '            'Calculate the Probability that the Random Variable will lie between From Value and To Value:
                    '            dgvAreaAnnot.Rows(RowNo).Cells(9).Value = dgvAreaAnnot.Rows(RowNo).Cells(8).Value - dgvAreaAnnot.Rows(RowNo).Cells(4).Value
                    '        End If
                    '        UpdateXmlAreaAnnotInfo(RowNo)
                    '    End If

            End Select
        Else
            Main.Message.AddWarning("Unknown combo box: " & combo.Name & vbCrLf)
        End If

        dgvProbabilities.AllowUserToAddRows = True

    End Sub

    Private Function GetInvCdfValue(ByVal Prob As Double) As Double
        'Get the value corresponding the the given probability value.
        If Distribution.Info(SelDistrib).Continuity = "Continuous" Then
            Return Distribution.Info(SelDistrib).InvCDF(Prob)
        Else
            Main.Message.AddWarning("Inverse CDF is not yet supported for discrete distributions." & vbCrLf)
            Return Double.NaN
        End If
    End Function

    Private Function GetInvRevCdfValue(ByVal Prob As Double) As Double
        'Get the Inverse Reverse Cdf value.
        'Get the value corresponding the the given probability value.
        If Distribution.Info(SelDistrib).Continuity = "Continuous" Then
            Return Distribution.Info(SelDistrib).InvCDF(1 - Prob)
        Else
            Main.Message.AddWarning("Inverse Reverse CDF is not yet supported for discrete distributions." & vbCrLf)
            Return Double.NaN
        End If
    End Function

    Private Sub dgvProbabilities_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvProbabilities.CellContentClick

    End Sub

    Private Sub dgvProbabilities_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvProbabilities.CellEndEdit
        'The Probabilities settings have been edited.

        Dim RowNo As Integer = e.RowIndex
        Dim RowCount As Integer = dgvProbabilities.RowCount

        If RowNo = RowCount - 1 Then
            'This is the last (user added) row - not yet editable!
        Else
            Dim ColNo As Integer = e.ColumnIndex
            'Dim DistribNo As Integer = SelDistrib
            Select Case ColNo

                Case 0 'From Value Type

                Case 1 'From Value Parameter
                    Try
                        If dgvProbabilities.Rows(RowNo).Cells(0).Value = "Minimum" Then

                        ElseIf dgvProbabilities.Rows(RowNo).Cells(0).Value = "Probability <=" Then

                        ElseIf dgvProbabilities.Rows(RowNo).Cells(0).Value = "Probability >" Then

                        ElseIf dgvProbabilities.Rows(RowNo).Cells(0).Value = "Random Variable Value" Then
                            dgvProbabilities.Rows(RowNo).Cells(2).Value = dgvProbabilities.Rows(RowNo).Cells(1).Value
                            dgvProbabilities.Rows(RowNo).Cells(3).Value = Distribution.Info(SelDistrib).CDF(dgvProbabilities.Rows(RowNo).Cells(1).Value)
                            dgvProbabilities.Rows(RowNo).Cells(8).Value = dgvProbabilities.Rows(RowNo).Cells(7).Value - dgvProbabilities.Rows(RowNo).Cells(3).Value
                            dgvProbabilities.Rows(RowNo).Cells(9).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * 100
                            dgvProbabilities.Rows(RowNo).Cells(10).Value = 1 / dgvProbabilities.Rows(RowNo).Cells(8).Value
                            If txtTotalPopulation.Text <> "" Then
                                'dgvProbabilities.Rows(RowNo).Cells(10).Value = Int(dgvProbabilities.Rows(RowNo).Cells(8).Value * Val(txtTotalPopulation.Text))
                                'dgvProbabilities.Rows(RowNo).Cells(10).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * Val(txtTotalPopulation.Text)
                                dgvProbabilities.Rows(RowNo).Cells(11).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * Val(txtTotalPopulation.Text.Replace(",", ""))
                            End If
                        ElseIf dgvProbabilities.Rows(RowNo).Cells(4).Value = "Mean" Then

                        ElseIf dgvProbabilities.Rows(RowNo).Cells(4).Value = "Median" Then

                        ElseIf dgvProbabilities.Rows(RowNo).Cells(4).Value = "Mode" Then

                        ElseIf dgvProbabilities.Rows(RowNo).Cells(4).Value = "Standard Deviation" Then

                        End If
                    Catch ex As Exception
                        Main.Message.AddWarning(ex.Message & vbCrLf)
                    End Try

                Case 2 'From Value

                Case 3 'From Value CDF

                Case 4  'To Value Type

                Case 5 'To Value Parameter
                    Try
                        If dgvProbabilities.Rows(RowNo).Cells(4).Value = "Minimum" Then

                        ElseIf dgvProbabilities.Rows(RowNo).Cells(4).Value = "Probability <=" Then

                        ElseIf dgvProbabilities.Rows(RowNo).Cells(4).Value = "Probability >" Then

                        ElseIf dgvProbabilities.Rows(RowNo).Cells(4).Value = "Random Variable Value" Then
                            dgvProbabilities.Rows(RowNo).Cells(6).Value = dgvProbabilities.Rows(RowNo).Cells(5).Value
                            dgvProbabilities.Rows(RowNo).Cells(7).Value = Distribution.Info(SelDistrib).CDF(dgvProbabilities.Rows(RowNo).Cells(5).Value)
                            dgvProbabilities.Rows(RowNo).Cells(8).Value = dgvProbabilities.Rows(RowNo).Cells(7).Value - dgvProbabilities.Rows(RowNo).Cells(3).Value
                            dgvProbabilities.Rows(RowNo).Cells(9).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * 100
                            dgvProbabilities.Rows(RowNo).Cells(10).Value = 1 / dgvProbabilities.Rows(RowNo).Cells(8).Value
                            If txtTotalPopulation.Text <> "" Then
                                'dgvProbabilities.Rows(RowNo).Cells(10).Value = Int(dgvProbabilities.Rows(RowNo).Cells(8).Value * Val(txtTotalPopulation.Text))
                                'dgvProbabilities.Rows(RowNo).Cells(10).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * Val(txtTotalPopulation.Text)
                                dgvProbabilities.Rows(RowNo).Cells(11).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * Val(txtTotalPopulation.Text.Replace(",", ""))
                            End If
                        ElseIf dgvProbabilities.Rows(RowNo).Cells(4).Value = "Mean" Then

                        ElseIf dgvProbabilities.Rows(RowNo).Cells(4).Value = "Median" Then

                        ElseIf dgvProbabilities.Rows(RowNo).Cells(4).Value = "Mode" Then

                        ElseIf dgvProbabilities.Rows(RowNo).Cells(4).Value = "Standard Deviation" Then

                        End If
                    Catch ex As Exception
                        Main.Message.AddWarning(ex.Message & vbCrLf)
                    End Try

                Case 6  'To Value

                Case 7 'To Value CDF

                Case 8 'Probability

                Case 9 'Probability %

                Case 10 'Population


            End Select

        End If


    End Sub

    Private Sub txtTotalPopulation_TextChanged(sender As Object, e As EventArgs) Handles txtTotalPopulation.TextChanged
        'RecalcPopulation()
    End Sub

    Private Sub txtTotalPopulation_LostFocus(sender As Object, e As EventArgs) Handles txtTotalPopulation.LostFocus
        RecalcPopulation()
    End Sub

    Private Sub RecalcPopulation()
        'Recalculate the populations

        Dim Population As Single = Val(txtTotalPopulation.Text.Replace(",", ""))
        txtTotalPopulation.Text = Format(Population, txtPopFormat.Text)

        If txtTotalPopulation.Text <> "" Then
            Dim TotalPop As Integer = Val(txtTotalPopulation.Text.Replace(",", ""))
            Dim RowCount As Integer = dgvProbabilities.RowCount
            Dim RowNo As Integer
            For RowNo = 0 To RowCount - 2
                'dgvProbabilities.Rows(RowNo).Cells(10).Value = Int(dgvProbabilities.Rows(RowNo).Cells(8).Value * TotalPop)
                'dgvProbabilities.Rows(RowNo).Cells(10).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * TotalPop
                'dgvProbabilities.Rows(RowNo).Cells(11).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * Val(txtTotalPopulation.Text)
                dgvProbabilities.Rows(RowNo).Cells(11).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * Val(txtTotalPopulation.Text.Replace(",", ""))
            Next
        End If

    End Sub

    Private Sub RecalcAll()
        'Recalculate all values in dgvProbabilities

        Dim TotalPop As Integer = Val(txtTotalPopulation.Text.Replace(",", ""))
        Dim RowCount As Integer = dgvProbabilities.RowCount
        Dim RowNo As Integer
        For RowNo = 0 To RowCount - 2
            Select Case dgvProbabilities.Rows(RowNo).Cells(0).Value
                Case "Minimum"

                Case "Probability <="
                    dgvProbabilities.Rows(RowNo).Cells(2).Value = Distribution.Info(SelDistrib).InvCDF(dgvProbabilities.Rows(RowNo).Cells(1).Value)

                Case "Probability >"
                    dgvProbabilities.Rows(RowNo).Cells(2).Value = Distribution.Info(SelDistrib).InvRevCDF(dgvProbabilities.Rows(RowNo).Cells(1).Value)

                Case "Random Variable Value"
                    dgvProbabilities.Rows(RowNo).Cells(3).Value = Distribution.Info(SelDistrib).CDF(dgvProbabilities.Rows(RowNo).Cells(1).Value)

                Case "Mean"
                    dgvProbabilities.Rows(RowNo).Cells(2).Value = Distribution.Info(SelDistrib).Mean
                    dgvProbabilities.Rows(RowNo).Cells(3).Value = Distribution.Info(SelDistrib).CDF(dgvProbabilities.Rows(RowNo).Cells(2).Value)

                Case "Median"
                    dgvProbabilities.Rows(RowNo).Cells(2).Value = Distribution.Info(SelDistrib).Median
                    dgvProbabilities.Rows(RowNo).Cells(3).Value = Distribution.Info(SelDistrib).CDF(dgvProbabilities.Rows(RowNo).Cells(2).Value)

                Case "Mode"
                    dgvProbabilities.Rows(RowNo).Cells(2).Value = Distribution.Info(SelDistrib).Mode
                    dgvProbabilities.Rows(RowNo).Cells(3).Value = Distribution.Info(SelDistrib).CDF(dgvProbabilities.Rows(RowNo).Cells(2).Value)

                Case "Standard Deviation"
                    dgvProbabilities.Rows(RowNo).Cells(2).Value = Distribution.Info(SelDistrib).CDF(Distribution.Info(SelDistrib).Mean + dgvProbabilities.Rows(RowNo).Cells(1).Value * Distribution.Info(SelDistrib).StdDev)
                    dgvProbabilities.Rows(RowNo).Cells(3).Value = Distribution.Info(SelDistrib).CDF(dgvProbabilities.Rows(RowNo).Cells(2).Value)
            End Select

            Select Case dgvProbabilities.Rows(RowNo).Cells(4).Value
                Case "Maximum"

                Case "Probability <="
                    dgvProbabilities.Rows(RowNo).Cells(6).Value = Distribution.Info(SelDistrib).InvCDF(dgvProbabilities.Rows(RowNo).Cells(5).Value)

                Case "Probability >"
                    dgvProbabilities.Rows(RowNo).Cells(6).Value = Distribution.Info(SelDistrib).InvRevCDF(dgvProbabilities.Rows(RowNo).Cells(5).Value)

                Case "Random Variable Value"
                    dgvProbabilities.Rows(RowNo).Cells(7).Value = Distribution.Info(SelDistrib).CDF(dgvProbabilities.Rows(RowNo).Cells(5).Value)

                Case "Mean"
                    dgvProbabilities.Rows(RowNo).Cells(6).Value = Distribution.Info(SelDistrib).Mean
                    dgvProbabilities.Rows(RowNo).Cells(7).Value = Distribution.Info(SelDistrib).CDF(dgvProbabilities.Rows(RowNo).Cells(6).Value)

                Case "Median"
                    dgvProbabilities.Rows(RowNo).Cells(6).Value = Distribution.Info(SelDistrib).Median
                    dgvProbabilities.Rows(RowNo).Cells(7).Value = Distribution.Info(SelDistrib).CDF(dgvProbabilities.Rows(RowNo).Cells(6).Value)

                Case "Mode"
                    dgvProbabilities.Rows(RowNo).Cells(6).Value = Distribution.Info(SelDistrib).Mode
                    dgvProbabilities.Rows(RowNo).Cells(7).Value = Distribution.Info(SelDistrib).CDF(dgvProbabilities.Rows(RowNo).Cells(6).Value)

                Case "Standard Deviation"
                    dgvProbabilities.Rows(RowNo).Cells(6).Value = Distribution.Info(SelDistrib).CDF(Distribution.Info(SelDistrib).Mean + dgvProbabilities.Rows(RowNo).Cells(5).Value * Distribution.Info(SelDistrib).StdDev)
                    dgvProbabilities.Rows(RowNo).Cells(7).Value = Distribution.Info(SelDistrib).CDF(dgvProbabilities.Rows(RowNo).Cells(6).Value)
            End Select

            dgvProbabilities.Rows(RowNo).Cells(8).Value = dgvProbabilities.Rows(RowNo).Cells(7).Value - dgvProbabilities.Rows(RowNo).Cells(3).Value
            dgvProbabilities.Rows(RowNo).Cells(9).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * 100
            dgvProbabilities.Rows(RowNo).Cells(10).Value = 1 / dgvProbabilities.Rows(RowNo).Cells(8).Value
            'dgvProbabilities.Rows(RowNo).Cells(10).Value = Int(dgvProbabilities.Rows(RowNo).Cells(8).Value * TotalPop)
            'dgvProbabilities.Rows(RowNo).Cells(10).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * TotalPop
            dgvProbabilities.Rows(RowNo).Cells(11).Value = dgvProbabilities.Rows(RowNo).Cells(8).Value * Val(txtTotalPopulation.Text.Replace(",", ""))

        Next

    End Sub

    Private Sub btnFormatHelp_Click(sender As Object, e As EventArgs) Handles btnFormatHelp.Click
        'Show Format inforamtion.
        MessageBox.Show("Format string examples:" & vbCrLf & "N4 - Number displayed with thousands separator and 4 decimal places" & vbCrLf & "F4 - Number displayed with 4 decimal places.", "Number Formatting")
    End Sub

    Private Sub txtPopFormat_TextChanged(sender As Object, e As EventArgs) Handles txtPopFormat.TextChanged

    End Sub

    Private Sub txtPopFormat_LostFocus(sender As Object, e As EventArgs) Handles txtPopFormat.LostFocus
        'Population Format updated
        dgvProbabilities.Columns(10).DefaultCellStyle.Format = txtPopFormat.Text
        dgvProbabilities.Columns(11).DefaultCellStyle.Format = txtPopFormat.Text
        txtTotalPopulation.Text = Format(Val(txtTotalPopulation.Text.Replace(",", "")), txtPopFormat.Text)
    End Sub

    Private Sub txtProbFormat_TextChanged(sender As Object, e As EventArgs) Handles txtProbFormat.TextChanged

    End Sub

    Private Sub txtProbFormat_LostFocus(sender As Object, e As EventArgs) Handles txtProbFormat.LostFocus
        'Probability Format updated
        dgvProbabilities.Columns(3).DefaultCellStyle.Format = txtProbFormat.Text
        dgvProbabilities.Columns(7).DefaultCellStyle.Format = txtProbFormat.Text
        dgvProbabilities.Columns(8).DefaultCellStyle.Format = txtProbFormat.Text
    End Sub

    Private Sub txtProbPctFormat_TextChanged(sender As Object, e As EventArgs) Handles txtProbPctFormat.TextChanged

    End Sub

    Private Sub txtProbPctFormat_LostFocus(sender As Object, e As EventArgs) Handles txtProbPctFormat.LostFocus
        'Probability Percent Format updated
        dgvProbabilities.Columns(9).DefaultCellStyle.Format = txtProbPctFormat.Text
    End Sub





#End Region 'Form Methods ---------------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Form Events - Events that can be triggered by this form." '==========================================================================================================================

#End Region 'Form Events ----------------------------------------------------------------------------------------------------------------------------------------------------------------------

End Class