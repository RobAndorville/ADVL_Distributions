Public Class frmMostLikelyParams
    'Find the most likely distribution parameters for a set of samples.

#Region " Variable Declarations - All the variables used in this form and this application." '=================================================================================================

    Dim WithEvents ParamEst As New clsParamEstimation

    Dim WithEvents DistribInfo As New DistributionInfo

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
            'Debug.Print("FormNo = " & _formNo)
        End Set
    End Property


    'NOTE: ParamEst.SourceTable and ParamEst.SourceColumn no used.
    'Private _tableName As String = "" 'The name of the table containing the samples.
    'Property TableName As String
    '    Get
    '        Return _tableName
    '    End Get
    '    Set(value As String)
    '        _tableName = value
    '    End Set
    'End Property

    'Private _columnName As String = "" 'The name of the column containing the samples.
    'Property ColumnName As String
    '    Get
    '        Return _columnName
    '    End Get
    '    Set(value As String)
    '        _columnName = value
    '    End Set
    'End Property

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

        ParamEst.SourceData = Main.Distribution.Data 'Set the SourceData - ParamEst will read the samples deom this DataSet.

        ParamEst.XmlList = Main.Distribution.XmlList 'Get the list of distributions.
        ParamEst.Distribution = DistribInfo

        Dim Distribs = From item In ParamEst.XmlList.<DistributionList>.<Distribution>

        For Each item In Distribs
            cmbDistribution.Items.Add(item.<Name>.Value)
        Next

        dgvParams.RowCount = 5
        dgvParams.ColumnCount = 2
        dgvParams.Columns(0).HeaderText = "Name"
        dgvParams.Columns(0).Width = 120
        dgvParams.Columns(1).HeaderText = "Value"
        dgvParams.Columns(1).Width = 120

        'Fill the list of tables:
        For Each item In Main.Distribution.Data.Tables
            cmbTableName.Items.Add(item.TableName)
        Next

        dgvCalculations.AutoGenerateColumns = True
        dgvCalculations.DataSource = ParamEst.Data.Tables("Model_Fitting")
        dgvCalculations.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        dgvCalculations.AutoResizeColumns()

        RestoreFormSettings()   'Restore the form settings
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        'Exit the Form

        If FormNo > -1 Then
            Main.ClosedFormNo = FormNo 'The Main form property ClosedFormNo is set to this form number. This is used in the ChartFormClosed method to select the correct form to set to nothing.
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

    Private Sub frmTable_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        If FormNo > -1 Then
            Main.MostLikelyParamsFormClosed()
        End If
    End Sub




#End Region 'Form Display Methods -------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Open and Close Forms - Code used to open and close other forms." '===================================================================================================================

#End Region 'Open and Close Forms -------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Form Methods - The main actions performed by this form." '===========================================================================================================================

    Private Sub cmbDistribution_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDistribution.SelectedIndexChanged
        'A distribution has been selected
        ApplyDistribSettings()
    End Sub

    Private Sub ApplyDistribSettings()
        'Apply the selected distribution settings

        'Dim DistribName As String = cmbDistribution.Text
        ParamEst.Distribution.Name = cmbDistribution.Text
        'Dim Distrib = From item In ParamEst.XmlList.<DistributionList>.<Distribution> Where item.<Name>.Value = DistribName
        Dim Distrib = From item In ParamEst.XmlList.<DistributionList>.<Distribution> Where item.<Name>.Value = ParamEst.Distribution.Name

        If Distrib.Count = 1 Then
            'txtContinuity.Text = Distrib(0).<Continuity>.Value
            txtContinuity.Text = Distrib.<Continuity>.Value
            'txtNParams.Text = Distrib(0).<NParameters>.Value
            txtNParams.Text = Distrib.<NParameters>.Value
            'txtDescription.Text = Distrib(0).<Description>.Value
            txtDescription.Text = Distrib.<Description>.Value
            'DistribInfo.FromXDoc(Distrib(0))
            'DistribInfo.FromXDoc(Distrib)
            DistribInfo.Usage = Distrib.<Usage>.Value
            DistribInfo.NParams = Distrib.<NParameters>.Value
            DistribInfo.Continuity = Distrib.<Continuity>.Value
            DistribInfo.ParamA.Type = Distrib.<Default>.<ParameterA>.<Type>.Value
            DistribInfo.ParamA.Name = Distrib.<Default>.<ParameterA>.<Name>.Value
            DistribInfo.ParamA.Symbol = Distrib.<Default>.<ParameterA>.<Symbol>.Value
            DistribInfo.ParamA.Description = Distrib.<Default>.<ParameterA>.<Description>.Value
            DistribInfo.ParamA.Minimum = Distrib.<Default>.<ParameterA>.<Minimum>.Value
            DistribInfo.ParamA.Maximum = Distrib.<Default>.<ParameterA>.<Maximum>.Value
            DistribInfo.ParamA.NumberType = Distrib.<Default>.<ParameterA>.<NumberType>.Value
            DistribInfo.ParamA.Value = Distrib.<Default>.<ParameterA>.<Default>.Value
            DistribInfo.ParamA.AdjustMin = Distrib.<Default>.<ParameterA>.<AdjustMin>.Value
            DistribInfo.ParamA.AdjustMax = Distrib.<Default>.<ParameterA>.<AdjustMax>.Value
            DistribInfo.ParamA.Increment = Distrib.<Default>.<ParameterA>.<Increment>.Value
            If DistribInfo.NParams > 1 Then
                DistribInfo.ParamB.Type = Distrib.<Default>.<ParameterB>.<Type>.Value
                DistribInfo.ParamB.Name = Distrib.<Default>.<ParameterB>.<Name>.Value
                DistribInfo.ParamB.Symbol = Distrib.<Default>.<ParameterB>.<Symbol>.Value
                DistribInfo.ParamB.Description = Distrib.<Default>.<ParameterB>.<Description>.Value
                DistribInfo.ParamB.Minimum = Distrib.<Default>.<ParameterB>.<Minimum>.Value
                DistribInfo.ParamB.Maximum = Distrib.<Default>.<ParameterB>.<Maximum>.Value
                DistribInfo.ParamB.NumberType = Distrib.<Default>.<ParameterB>.<NumberType>.Value
                DistribInfo.ParamB.Value = Distrib.<Default>.<ParameterB>.<Default>.Value
                DistribInfo.ParamB.AdjustMin = Distrib.<Default>.<ParameterB>.<AdjustMin>.Value
                DistribInfo.ParamB.AdjustMax = Distrib.<Default>.<ParameterB>.<AdjustMax>.Value
                DistribInfo.ParamB.Increment = Distrib.<Default>.<ParameterB>.<Increment>.Value
                If DistribInfo.NParams > 2 Then
                    DistribInfo.ParamC.Type = Distrib.<Default>.<ParameterC>.<Type>.Value
                    DistribInfo.ParamC.Name = Distrib.<Default>.<ParameterC>.<Name>.Value
                    DistribInfo.ParamC.Symbol = Distrib.<Default>.<ParameterC>.<Symbol>.Value
                    DistribInfo.ParamC.Description = Distrib.<Default>.<ParameterC>.<Description>.Value
                    DistribInfo.ParamC.Minimum = Distrib.<Default>.<ParameterC>.<Minimum>.Value
                    DistribInfo.ParamC.Maximum = Distrib.<Default>.<ParameterC>.<Maximum>.Value
                    DistribInfo.ParamC.NumberType = Distrib.<Default>.<ParameterC>.<NumberType>.Value
                    DistribInfo.ParamC.Value = Distrib.<Default>.<ParameterC>.<Default>.Value
                    DistribInfo.ParamC.AdjustMin = Distrib.<Default>.<ParameterC>.<AdjustMin>.Value
                    DistribInfo.ParamC.AdjustMax = Distrib.<Default>.<ParameterC>.<AdjustMax>.Value
                    DistribInfo.ParamC.Increment = Distrib.<Default>.<ParameterC>.<Increment>.Value
                    If DistribInfo.NParams > 3 Then
                        DistribInfo.ParamD.Type = Distrib.<Default>.<ParameterD>.<Type>.Value
                        DistribInfo.ParamD.Name = Distrib.<Default>.<ParameterD>.<Name>.Value
                        DistribInfo.ParamD.Symbol = Distrib.<Default>.<ParameterD>.<Symbol>.Value
                        DistribInfo.ParamD.Description = Distrib.<Default>.<ParameterD>.<Description>.Value
                        DistribInfo.ParamD.Minimum = Distrib.<Default>.<ParameterD>.<Minimum>.Value
                        DistribInfo.ParamD.Maximum = Distrib.<Default>.<ParameterD>.<Maximum>.Value
                        DistribInfo.ParamD.NumberType = Distrib.<Default>.<ParameterD>.<NumberType>.Value
                        DistribInfo.ParamD.Value = Distrib.<Default>.<ParameterD>.<Default>.Value
                        DistribInfo.ParamD.AdjustMin = Distrib.<Default>.<ParameterD>.<AdjustMin>.Value
                        DistribInfo.ParamD.AdjustMax = Distrib.<Default>.<ParameterD>.<AdjustMax>.Value
                        DistribInfo.ParamD.Increment = Distrib.<Default>.<ParameterD>.<Increment>.Value
                        If DistribInfo.NParams > 4 Then
                            DistribInfo.ParamE.Type = Distrib.<Default>.<ParameterE>.<Type>.Value
                            DistribInfo.ParamE.Name = Distrib.<Default>.<ParameterE>.<Name>.Value
                            DistribInfo.ParamE.Symbol = Distrib.<Default>.<ParameterE>.<Symbol>.Value
                            DistribInfo.ParamE.Description = Distrib.<Default>.<ParameterE>.<Description>.Value
                            DistribInfo.ParamE.Minimum = Distrib.<Default>.<ParameterE>.<Minimum>.Value
                            DistribInfo.ParamE.Maximum = Distrib.<Default>.<ParameterE>.<Maximum>.Value
                            DistribInfo.ParamE.NumberType = Distrib.<Default>.<ParameterE>.<NumberType>.Value
                            DistribInfo.ParamE.Value = Distrib.<Default>.<ParameterE>.<Default>.Value
                            DistribInfo.ParamE.AdjustMin = Distrib.<Default>.<ParameterE>.<AdjustMin>.Value
                            DistribInfo.ParamE.AdjustMax = Distrib.<Default>.<ParameterE>.<AdjustMax>.Value
                            DistribInfo.ParamE.Increment = Distrib.<Default>.<ParameterE>.<Increment>.Value
                        End If
                    End If
                End If
            End If
            ShowParameters()
        ElseIf Distrib.Count = 0 Then
            'Main.Message.AddWarning("No settings found for distribution: " & DistribName & vbCrLf)
            Main.Message.AddWarning("No settings found for distribution: " & ParamEst.Distribution.Name & vbCrLf)
            txtContinuity.Text = ""
            txtNParams.Text = ""
            txtDescription.Text = ""
            ClearParameters()
        Else
            'Main.Message.AddWarning("More than one entry found for distribution: " & DistribName & vbCrLf)
            Main.Message.AddWarning("More than one entry found for distribution: " & ParamEst.Distribution.Name & vbCrLf)
            txtContinuity.Text = ""
            txtNParams.Text = ""
            txtDescription.Text = ""
            ClearParameters()
        End If
    End Sub

    Private Sub ShowParameters()
        'Show the parameters contained in DistribInfo.

        ''Clear the existing parameters:
        'dgvParams.Rows(0).Cells(0).Value = ""
        'dgvParams.Rows(0).Cells(1).Value = ""
        'dgvParams.Rows(1).Cells(0).Value = ""
        'dgvParams.Rows(1).Cells(1).Value = ""
        'dgvParams.Rows(2).Cells(0).Value = ""
        'dgvParams.Rows(2).Cells(1).Value = ""
        'dgvParams.Rows(3).Cells(0).Value = ""
        'dgvParams.Rows(3).Cells(1).Value = ""
        'dgvParams.Rows(4).Cells(0).Value = ""
        'dgvParams.Rows(4).Cells(1).Value = ""
        ClearParameters()

        'Show the new parameters:
        dgvParams.Rows(0).Cells(0).Value = DistribInfo.ParamA.Name
        dgvParams.Rows(0).Cells(0).Style.BackColor = Color.White
        dgvParams.Rows(0).Cells(1).Value = DistribInfo.ParamA.Value
        dgvParams.Rows(0).Cells(1).Style.BackColor = Color.White
        dgvParams.Rows(0).Cells(1).ReadOnly = False
        If DistribInfo.NParams > 1 Then
            dgvParams.Rows(1).Cells(0).Value = DistribInfo.ParamB.Name
            dgvParams.Rows(1).Cells(0).Style.BackColor = Color.White
            dgvParams.Rows(1).Cells(1).Value = DistribInfo.ParamB.Value
            dgvParams.Rows(1).Cells(1).Style.BackColor = Color.White
            dgvParams.Rows(1).Cells(1).ReadOnly = False
            If DistribInfo.NParams > 2 Then
                dgvParams.Rows(2).Cells(0).Value = DistribInfo.ParamC.Name
                dgvParams.Rows(2).Cells(0).Style.BackColor = Color.White
                dgvParams.Rows(2).Cells(1).Value = DistribInfo.ParamC.Value
                dgvParams.Rows(2).Cells(1).Style.BackColor = Color.White
                dgvParams.Rows(2).Cells(1).ReadOnly = False
                If DistribInfo.NParams > 3 Then
                    dgvParams.Rows(3).Cells(0).Value = DistribInfo.ParamD.Name
                    dgvParams.Rows(3).Cells(0).Style.BackColor = Color.White
                    dgvParams.Rows(3).Cells(1).Value = DistribInfo.ParamD.Value
                    dgvParams.Rows(3).Cells(1).Style.BackColor = Color.White
                    dgvParams.Rows(3).Cells(1).ReadOnly = False
                    If DistribInfo.NParams > 4 Then
                        dgvParams.Rows(4).Cells(0).Value = DistribInfo.ParamE.Name
                        dgvParams.Rows(4).Cells(0).Style.BackColor = Color.White
                        dgvParams.Rows(4).Cells(1).Value = DistribInfo.ParamE.Value
                        dgvParams.Rows(4).Cells(1).Style.BackColor = Color.White
                        dgvParams.Rows(4).Cells(1).ReadOnly = False
                    End If
                End If
            End If
        End If

    End Sub

    Private Sub ClearParameters()
        'Clear the existing parameters:
        dgvParams.Rows(0).Cells(0).Value = ""
        dgvParams.Rows(0).Cells(1).Value = ""
        dgvParams.Rows(1).Cells(0).Value = ""
        dgvParams.Rows(1).Cells(1).Value = ""
        dgvParams.Rows(2).Cells(0).Value = ""
        dgvParams.Rows(2).Cells(1).Value = ""
        dgvParams.Rows(3).Cells(0).Value = ""
        dgvParams.Rows(3).Cells(1).Value = ""
        dgvParams.Rows(4).Cells(0).Value = ""
        dgvParams.Rows(4).Cells(1).Value = ""

        dgvParams.Rows(0).Cells(0).ReadOnly = True
        dgvParams.Rows(1).Cells(0).ReadOnly = True
        dgvParams.Rows(2).Cells(0).ReadOnly = True
        dgvParams.Rows(3).Cells(0).ReadOnly = True
        dgvParams.Rows(4).Cells(0).ReadOnly = True

        dgvParams.Rows(0).Cells(1).ReadOnly = True
        dgvParams.Rows(1).Cells(1).ReadOnly = True
        dgvParams.Rows(2).Cells(1).ReadOnly = True
        dgvParams.Rows(3).Cells(1).ReadOnly = True
        dgvParams.Rows(4).Cells(1).ReadOnly = True

        dgvParams.Rows(0).Cells(0).Style.BackColor = Color.LightGray
        dgvParams.Rows(1).Cells(0).Style.BackColor = Color.LightGray
        dgvParams.Rows(2).Cells(0).Style.BackColor = Color.LightGray
        dgvParams.Rows(3).Cells(0).Style.BackColor = Color.LightGray
        dgvParams.Rows(4).Cells(0).Style.BackColor = Color.LightGray

        dgvParams.Rows(0).Cells(1).Style.BackColor = Color.LightGray
        dgvParams.Rows(1).Cells(1).Style.BackColor = Color.LightGray
        dgvParams.Rows(2).Cells(1).Style.BackColor = Color.LightGray
        dgvParams.Rows(3).Cells(1).Style.BackColor = Color.LightGray
        dgvParams.Rows(4).Cells(1).Style.BackColor = Color.LightGray

    End Sub

    Private Sub cmbTableName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTableName.SelectedIndexChanged
        'TableName = cmbTableName.SelectedItem.ToString
        ParamEst.SourceTable = cmbTableName.SelectedItem.ToString
        'Fill the list of columns:
        cmbColumnName.Items.Clear()
        'For Each item In Main.Distribution.Data.Tables(TableName).Columns
        For Each item In Main.Distribution.Data.Tables(ParamEst.SourceTable).Columns
            cmbColumnName.Items.Add(item.ColumnName)
        Next
        If cmbColumnName.Items.Count > 0 Then
            cmbColumnName.SelectedIndex = 0
            'ColumnName = cmbColumnName.SelectedItem.ToString
            'CopySamplesToModelFitting()
        End If

        'ParamEst.Data.Tables("Model_Fitting").Rows.Clear()
    End Sub

    Private Sub cmbColumnName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbColumnName.SelectedIndexChanged

        dgvCalculations.DataSource = Nothing
        'ColumnName = cmbColumnName.SelectedItem.ToString
        ParamEst.SourceColumn = cmbColumnName.SelectedItem.ToString
        'CopySamplesToModelFitting()
        dgvCalculations.DataSource = ParamEst.Data.Tables("Model_Fitting")
        dgvCalculations.AutoResizeColumns()

    End Sub

    Private Sub CopySamplesToModelFitting()
        ParamEst.Data.Tables("Model_Fitting").Rows.Clear()
        'For Each Row As DataRow In Main.Distribution.Data.Tables(TableName).Rows
        For Each Row As DataRow In Main.Distribution.Data.Tables(ParamEst.SourceTable).Rows
            'ParamEst.Data.Tables("Model_Fitting").Rows.Add(Row(ColumnName))
            ParamEst.Data.Tables("Model_Fitting").Rows.Add(Row(ParamEst.SourceColumn))
        Next

        'dgvCalculations.AutoResizeColumns()
        'dgvCalculations.Update()
        'dgvCalculations.Refresh()
    End Sub

    Private Sub btnOpenSampTable_Click(sender As Object, e As EventArgs) Handles btnOpenSampTable.Click
        'Open a Sample Table file.

        Dim FileName As String = Main.Project.SelectDataFile("Sample Table", "SampleTable")

        If FileName = "" Then
            'No file has been selected.
        Else
            Dim XDoc As System.Xml.Linq.XDocument
            Main.Project.ReadXmlData(FileName, XDoc)

            Dim TableName As String = XDoc.<SampleTable>.<TableName>.Value
            If Main.Distribution.Data.Tables.Contains(TableName) Then
                If MessageBox.Show("Overwrite existing table named " & TableName & "?", "Notice", MessageBoxButtons.YesNoCancel) = DialogResult.Yes Then
                    Main.Distribution.Data.Tables.Remove(TableName)
                Else
                    Exit Sub
                End If
            End If
            'Open the Sample Table
            'Create the Table:
            Main.Distribution.Data.Tables.Add(TableName)

            'Create the Sample Columns:
            Dim Columns = From item In XDoc.<SampleTable>.<DataSetInfoList>.<DataSetInfo>
            For Each ColInfo In Columns
                Select Case ColInfo.<NumberType>.Value
                    Case "Int16"
                        Main.Distribution.Data.Tables(TableName).Columns.Add(ColInfo.<ColumnName>.Value, System.Type.GetType("System.Int16"))
                    Case "Int32"
                        Main.Distribution.Data.Tables(TableName).Columns.Add(ColInfo.<ColumnName>.Value, System.Type.GetType("System.Int32"))
                    Case "Single"
                        Main.Distribution.Data.Tables(TableName).Columns.Add(ColInfo.<ColumnName>.Value, System.Type.GetType("System.Single"))
                    Case "Double"
                        Main.Distribution.Data.Tables(TableName).Columns.Add(ColInfo.<ColumnName>.Value, System.Type.GetType("System.Double"))
                    Case Else
                        Main.Message.AddWarning("Unknown number type: " & ColInfo.<NumberType>.Value & vbCrLf)
                End Select
            Next

            'Read the sample data an write to the columns:
            Dim Rows = From item In XDoc.<SampleTable>.<Samples>.<Row>
            For Each RowInfo In Rows
                Main.Distribution.Data.Tables(TableName).Rows.Add(Split(RowInfo.Value, ","))
            Next

            'Update the table list:
            If cmbTableName.Items.Contains(TableName) Then
                cmbTableName.Items.Remove(TableName)
            End If
            cmbTableName.Items.Add(TableName)
            cmbTableName.SelectedIndex = cmbTableName.FindStringExact(TableName) 'Select the table

            If Main.cmbTableName.Items.Contains(TableName) Then
                Main.cmbTableName.Items.Remove(TableName)
            End If
            Main.cmbTableName.Items.Add(TableName)

            ''Display the Table:
            'cmbTableList.SelectedIndex = cmbTableList.FindStringExact(TableName)
            'UpdateTable()
        End If
    End Sub

    Private Sub btnUpdateData_Click(sender As Object, e As EventArgs) Handles btnUpdateData.Click
        'Update the calculated distribution values in the Model Fitting table
        UpdateDataTable()

        'dgvCalculations.DataSource = Nothing

        'ParamEst.UpdateModelTableDistribValues()

        'dgvCalculations.DataSource = ParamEst.Data.Tables("Model_Fitting")
        'dgvCalculations.AutoResizeColumns()

        'txtLogLikelihood.Text = ParamEst.Data.Tables("Model_Fitting").Compute("Sum([Ln_Model_Prob_Dens])", "")

    End Sub

    Private Sub UpdateDataTable()
        'Update the calculated distribution values in the Model Fitting table

        dgvCalculations.DataSource = Nothing

        ParamEst.UpdateModelTableDistribValues()

        dgvCalculations.DataSource = ParamEst.Data.Tables("Model_Fitting")
        dgvCalculations.AutoResizeColumns()

        txtLogLikelihood.Text = ParamEst.Data.Tables("Model_Fitting").Compute("Sum([Ln_Model_Prob_Dens])", "")
    End Sub

    Private Sub dgvParams_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvParams.CellContentClick

    End Sub

    Private Sub dgvParams_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvParams.CellEndEdit
        'A distribution parameter may have been changed.

        Dim ColNo As Integer = e.ColumnIndex
        If ColNo = 1 Then
            Dim RowNo As Integer = e.RowIndex
            If RowNo = 0 Then
                ParamEst.Distribution.ParamA.Value = dgvParams.Rows(0).Cells(1).Value
            ElseIf RowNo = 1 Then
                ParamEst.Distribution.ParamB.Value = dgvParams.Rows(1).Cells(1).Value
            ElseIf RowNo = 2 Then
                ParamEst.Distribution.ParamC.Value = dgvParams.Rows(2).Cells(1).Value
            ElseIf RowNo = 3 Then
                ParamEst.Distribution.ParamD.Value = dgvParams.Rows(3).Cells(1).Value
            ElseIf RowNo = 4 Then
                ParamEst.Distribution.ParamE.Value = dgvParams.Rows(4).Cells(1).Value
            End If
        End If
    End Sub

    Private Sub btnFindParams_Click(sender As Object, e As EventArgs) Handles btnFindParams.Click
        'Find the most likely parameters for the selected distribution.

        ParamEst.RefineParams(0.001)

        dgvParams.Rows(0).Cells(1).Value = ParamEst.Distribution.ParamA.Value
        If ParamEst.Distribution.NParams > 1 Then
            dgvParams.Rows(1).Cells(1).Value = ParamEst.Distribution.ParamB.Value
            If ParamEst.Distribution.NParams > 2 Then
                dgvParams.Rows(2).Cells(1).Value = ParamEst.Distribution.ParamC.Value
                If ParamEst.Distribution.NParams > 3 Then
                    dgvParams.Rows(3).Cells(1).Value = ParamEst.Distribution.ParamD.Value
                    If ParamEst.Distribution.NParams > 4 Then
                        dgvParams.Rows(4).Cells(1).Value = ParamEst.Distribution.ParamE.Value
                    End If
                End If
            End If
        End If

        UpdateDataTable()

    End Sub

    Private Sub ParamEst_ErrorMessage(Msg As String) Handles ParamEst.ErrorMessage
        Main.Message.AddWarning(Msg)
    End Sub

    Private Sub ParamEst_Message(Msg As String) Handles ParamEst.Message
        Main.Message.Add(Msg)
    End Sub

    Private Sub DistribInfo_ErrorMessage(Msg As String) Handles DistribInfo.ErrorMessage
        Main.Message.AddWarning(Msg)
    End Sub

    Private Sub DistribInfo_Message(Msg As String) Handles DistribInfo.Message
        Main.Message.Add(Msg)
    End Sub


#End Region 'Form Methods ---------------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Form Events - Events that can be triggered by this form." '==========================================================================================================================

#End Region 'Form Events ----------------------------------------------------------------------------------------------------------------------------------------------------------------------

End Class