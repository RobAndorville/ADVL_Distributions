Public Class frmTable
    'Table display form.

#Region " Variable Declarations - All the variables used in this form and this application." '=================================================================================================

    'Public WithEvents ChartSettings As frmChartSettings

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

    'Private _dataSource As Object = Nothing 'DataSource links to the oject containing the Data to be analysed. (Input, Processed, Distribution or MonteCarlo objects in the Main form.)
    ''Every DataSource object must contain: Data, ChartList, ChartName
    'Property DataSource As Object
    '    Get
    '        Return _dataSource
    '    End Get
    '    Set(value As Object)
    '        _dataSource = value

    '        'Show list of available tables:
    '        'For Each item In _dataSource.ChartList
    '        For Each item In _dataSource.Data.Tables
    '            'cmbChartList.Items.Add(item.Key)
    '            cmbTableList.Items.Add(item.TableName)
    '        Next
    '        'If _dataSource.ChartName <> "" Then
    '        '    cmbChartList.SelectedIndex = cmbChartList.FindStringExact(_dataSource.ChartName)
    '        'End If
    '    End Set
    'End Property

    'Private _tableName As String = "" 'The name of the table containing the data to plot.
    'Property TableName As String
    '    Get
    '        Return _tableName
    '    End Get
    '    Set(value As String)
    '        _tableName = value
    '        cmbTableList.SelectedIndex = cmbTableList.FindStringExact(_tableName)
    '        'If DataSource.Data.Tables.Count > 0 Then
    '        '    NRows = DataSource.Data.Tables(TableName).Rows.Count
    '        'End If
    '        If Main.Distribution.Data.Tables.Count > 0 Then
    '            NRows = Main.Distribution.Data.Tables(TableName).Rows.Count
    '        End If
    '    End Set
    'End Property

    'Private _nRows As Integer = 0 'The number of rows in the selected table.
    'Property NRows As Integer
    '    Get
    '        Return _nRows
    '    End Get
    '    Set(value As Integer)
    '        _nRows = value
    '        txtNRows.Text = _nRows
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
                               <MinFormat><%= txtMinFormat.Text %></MinFormat>
                               <MaxFormat><%= txtMaxFormat.Text %></MaxFormat>
                               <SumFormat><%= txtSumFormat.Text %></SumFormat>
                               <AvgFormat><%= txtAvgFormat.Text %></AvgFormat>
                               <StdDevFormat><%= txtStdDevFormat.Text %></StdDevFormat>
                               <VarFormat><%= txtVarFormat.Text %></VarFormat>
                           </FormSettings>

        'Add code to include other settings to save after the comment line <!---->

        'Dim SettingsFileName As String = "FormSettings_" & Main.ApplicationInfo.Name & "_" & Me.Text & "_" & FormNo & ".xml"
        Dim SettingsFileName As String = "FormSettings_" & Main.ApplicationInfo.Name & "_" & Me.Text & ".xml"
        Main.Project.SaveXmlSettings(SettingsFileName, settingsData)
    End Sub

    Private Sub RestoreFormSettings()
        'Read the form settings from an XML document.

        'Dim SettingsFileName As String = "FormSettings_" & Main.ApplicationInfo.Name & "_" & Me.Text & "_" & FormNo & ".xml"
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
            If Settings.<FormSettings>.<MinFormat>.Value <> Nothing Then txtMinFormat.Text = Settings.<FormSettings>.<MinFormat>.Value
            If Settings.<FormSettings>.<MaxFormat>.Value <> Nothing Then txtMaxFormat.Text = Settings.<FormSettings>.<MaxFormat>.Value
            If Settings.<FormSettings>.<SumFormat>.Value <> Nothing Then txtSumFormat.Text = Settings.<FormSettings>.<SumFormat>.Value
            If Settings.<FormSettings>.<AvgFormat>.Value <> Nothing Then txtAvgFormat.Text = Settings.<FormSettings>.<AvgFormat>.Value
            If Settings.<FormSettings>.<StdDevFormat>.Value <> Nothing Then txtStdDevFormat.Text = Settings.<FormSettings>.<StdDevFormat>.Value
            If Settings.<FormSettings>.<VarFormat>.Value <> Nothing Then txtVarFormat.Text = Settings.<FormSettings>.<VarFormat>.Value

            CheckFormPos()
        End If
    End Sub

    Private Sub CheckFormPos()
        'Public Sub CheckFormPos()
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

        'Main.Message.Add("CheckFormPos() run." & vbCrLf)

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

        'Chart1.SuppressExceptions = True
        dgvStatistics.ColumnCount = 7
        dgvStatistics.Columns(0).HeaderText = "Field Name"
        dgvStatistics.Columns(1).HeaderText = "Minimum"
        dgvStatistics.Columns(2).HeaderText = "Maximum"
        dgvStatistics.Columns(3).HeaderText = "Sum"
        dgvStatistics.Columns(4).HeaderText = "Average"
        dgvStatistics.Columns(5).HeaderText = "Standard Deviation"
        dgvStatistics.Columns(6).HeaderText = "Variance"
        dgvStatistics.AllowUserToAddRows = False

        rbSample.Checked = True

        UpdateTableList()

        If cmbTableList.Items.Count > 0 Then
            cmbTableList.SelectedIndex = 0
        End If

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
            Main.TableFormClosed()
        End If
    End Sub



#End Region 'Form Display Methods -------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Form Methods - The main actions performed by this form." '===========================================================================================================================


    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        'Update the Table display
        'If cmbTableList.SelectedIndex = -1 Then
        '    'No table has been selected
        'Else
        '    Dim TableName As String = cmbTableList.SelectedItem.ToString
        '    If DataSource.Data.Tables.Contains(TableName) Then
        '        dgvResults.Columns.Clear()
        '        dgvResults.AutoGenerateColumns = True
        '        dgvResults.DataSource = Nothing
        '        dgvResults.DataSource = DataSource.Data.Tables(TableName)
        '        'dgvResults.AutoGenerateColumns = True
        '        dgvResults.AutoResizeColumns()
        '        dgvResults.Update()
        '        dgvResults.Refresh()

        '        ShowAllFieldStats()

        '    End If
        'End If
        UpdateTable()
    End Sub

    Private Sub RepositionFormatBoxes()
        'Reposition the Format boxes above the correponding table columns

        If dgvStatistics.Columns.Count > 6 Then
            txtMinFormat.Left = dgvStatistics.Left + dgvStatistics.RowHeadersWidth + dgvStatistics.Columns(0).Width
            txtMinFormat.Width = dgvStatistics.Columns(1).Width - 6

            txtMaxFormat.Left = dgvStatistics.Left + dgvStatistics.RowHeadersWidth + dgvStatistics.Columns(0).Width + dgvStatistics.Columns(1).Width
            txtMaxFormat.Width = dgvStatistics.Columns(2).Width - 6

            txtSumFormat.Left = dgvStatistics.Left + dgvStatistics.RowHeadersWidth + dgvStatistics.Columns(0).Width + dgvStatistics.Columns(1).Width + dgvStatistics.Columns(2).Width
            txtSumFormat.Width = dgvStatistics.Columns(3).Width - 6

            txtAvgFormat.Left = dgvStatistics.Left + dgvStatistics.RowHeadersWidth + dgvStatistics.Columns(0).Width + dgvStatistics.Columns(1).Width + dgvStatistics.Columns(2).Width + dgvStatistics.Columns(3).Width
            txtAvgFormat.Width = dgvStatistics.Columns(4).Width - 6

            txtStdDevFormat.Left = dgvStatistics.Left + dgvStatistics.RowHeadersWidth + dgvStatistics.Columns(0).Width + dgvStatistics.Columns(1).Width + dgvStatistics.Columns(2).Width + dgvStatistics.Columns(3).Width + dgvStatistics.Columns(4).Width
            txtStdDevFormat.Width = dgvStatistics.Columns(5).Width - 6

            txtVarFormat.Left = dgvStatistics.Left + dgvStatistics.RowHeadersWidth + dgvStatistics.Columns(0).Width + dgvStatistics.Columns(1).Width + dgvStatistics.Columns(2).Width + dgvStatistics.Columns(3).Width + dgvStatistics.Columns(4).Width + dgvStatistics.Columns(5).Width
            txtVarFormat.Width = dgvStatistics.Columns(6).Width - 6

            btnFormatHelp.Left = dgvStatistics.Left + dgvStatistics.RowHeadersWidth + dgvStatistics.Columns(0).Width + dgvStatistics.Columns(1).Width + dgvStatistics.Columns(2).Width + dgvStatistics.Columns(3).Width + dgvStatistics.Columns(4).Width + dgvStatistics.Columns(5).Width + dgvStatistics.Columns(6).Width
        End If

    End Sub

    Public Sub UpdateTable()
        'Update the table display
        If cmbTableList.SelectedIndex = -1 Then
            'No table has been selected
        Else
            Dim TableName As String = cmbTableList.SelectedItem.ToString
            If Main.Distribution.Data.Tables.Contains(TableName) Then
                dgvResults.Columns.Clear()
                dgvResults.AutoGenerateColumns = True
                dgvResults.DataSource = Nothing
                dgvResults.DataSource = Main.Distribution.Data.Tables(TableName)
                'dgvResults.AutoGenerateColumns = True
                dgvResults.AutoResizeColumns()
                dgvResults.Update()
                dgvResults.Refresh()

                ShowAllFieldStats()

                'RepositionFormatBoxes()

                ''Format the column data:
                'For Each Item In Main.MonteCarlo.DataInfo
                '    If Item.Table = TableName Then
                '        If Item.Format = "" Then
                '            'No format defined.
                '        Else
                '            Try
                '                dgvResults.Columns(Item.Name).DefaultCellStyle.Format = Item.Format
                '            Catch ex As Exception
                '                Main.Message.AddWarning("Error formatting column " & Item.Name & " : " & ex.Message & vbCrLf)
                '            End Try
                '        End If
                '    End If
                'Next

                dgvResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
                dgvResults.AutoResizeColumns()

            End If
        End If
    End Sub

    Public Sub ShowAllFieldStats()
        'Show the statistics for all of the fields in the selected table.
        Dim TableName As String = cmbTableList.SelectedItem.ToString

        If Main.Distribution.Data.Tables(TableName) Is Nothing Then
            Main.Message.AddWarning("There is no table named " & TableName & vbCrLf)
        Else
            Dim RowNo As Integer
            Dim Var As Double
            'cboCorrVariables.Items.Clear()
            'cboCovVariables.Items.Clear()
            dgvStatistics.Rows.Clear()

            txtNRows.Text = Main.Distribution.Data.Tables(TableName).Rows.Count

            For Each item In Main.Distribution.Data.Tables(TableName).Columns
                dgvStatistics.Rows.Add()
                RowNo = dgvStatistics.Rows.Count - 1
                dgvStatistics.Rows(RowNo).Cells(0).Value = item.ColumnName
                'dgvStatistics.Rows(RowNo).Cells(1).Value = Main.Distribution.Data.Tables(TableName).Compute("Min(" & item.ColumnName & ")", "")
                dgvStatistics.Rows(RowNo).Cells(1).Value = Main.Distribution.Data.Tables(TableName).Compute("Min([" & item.ColumnName & "])", "")
                dgvStatistics.Rows(RowNo).Cells(2).Value = Main.Distribution.Data.Tables(TableName).Compute("Max([" & item.ColumnName & "])", "")
                dgvStatistics.Rows(RowNo).Cells(3).Value = Main.Distribution.Data.Tables(TableName).Compute("Sum([" & item.ColumnName & "])", "")
                dgvStatistics.Rows(RowNo).Cells(4).Value = Main.Distribution.Data.Tables(TableName).Compute("Avg([" & item.ColumnName & "])", "")
                If rbSample.Checked Then
                    Var = Variance(item.ColumnName, True)
                    dgvStatistics.Rows(RowNo).Cells(6).Value = Var
                    dgvStatistics.Rows(RowNo).Cells(5).Value = Math.Sqrt(Var)
                ElseIf rbPopulation.Checked Then
                    Var = Variance(item.ColumnName, False)
                    dgvStatistics.Rows(RowNo).Cells(6).Value = Var
                    dgvStatistics.Rows(RowNo).Cells(5).Value = Math.Sqrt(Var)
                Else
                    dgvStatistics.Rows(RowNo).Cells(5).Value = Main.Distribution.Data.Tables(TableName).Compute("StDev([" & item.ColumnName & "])", "")
                    dgvStatistics.Rows(RowNo).Cells(6).Value = Main.Distribution.Data.Tables(TableName).Compute("Var([" & item.ColumnName & "])", "")
                End If

                'cboCorrVariables.Items.Add(item.ColumnName)
                'cboCovVariables.Items.Add(item.ColumnName)
            Next

            dgvStatistics.Columns(1).DefaultCellStyle.Format = txtMinFormat.Text
            dgvStatistics.Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvStatistics.Columns(2).DefaultCellStyle.Format = txtMaxFormat.Text
            dgvStatistics.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvStatistics.Columns(3).DefaultCellStyle.Format = txtSumFormat.Text
            dgvStatistics.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvStatistics.Columns(4).DefaultCellStyle.Format = txtAvgFormat.Text
            dgvStatistics.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvStatistics.Columns(5).DefaultCellStyle.Format = txtStdDevFormat.Text
            dgvStatistics.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvStatistics.Columns(6).DefaultCellStyle.Format = txtVarFormat.Text
            dgvStatistics.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            'dgvStatistics.AutoResizeColumns()
            dgvStatistics.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            'dgvStatistics.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader
            'RepositionFormatBoxes()

        End If
    End Sub

    Private Function Variance(ByVal ColumnName As String, ByVal IsSample As Boolean) As Double
        'Calculates the variance of the data in column ColumnName.
        'If IsSample is True, the Sample variance is calculated, else the Population variance is calculated.

        Dim TableName As String = cmbTableList.SelectedItem.ToString
        Dim NRows As Integer = Main.Distribution.Data.Tables(TableName).Rows.Count
        'Main.Message.Add("NRows = " & NRows & vbCrLf)
        'Dim ColName As String = "[" & ColumnName & "]"

        'Dim Mean As Double = DataSource.Data.Tables(TableName).Compute("Avg(" & DataSource.Data.Tables(TableName).Columns(ColumnName) & ")", "")
        Dim Mean As Double = Main.Distribution.Data.Tables(TableName).Compute("Avg([" & ColumnName & "])", "")
        'Main.Message.Add("Mean = " & Mean & vbCrLf)
        'Dim Var As Double = 0
        Dim DiffSq As Double = 0
        For Each Row As DataRow In Main.Distribution.Data.Tables(TableName).Rows
            'Var += (Row.Item(ColumnName) - Mean) ^ 2
            If IsDBNull(Row.Item(ColumnName)) Then
                'Do not try to calculate using DBNull values!
            Else
                DiffSq += (Row.Item(ColumnName) - Mean) ^ 2
            End If
        Next
        If IsSample Then
            'Var /= NRows - 1
            Return DiffSq / (NRows - 1)
        Else
            'Var /= NRows
            Return DiffSq / NRows
        End If
        'Return Var
    End Function

    Private Sub btnFormatHelp_Click(sender As Object, e As EventArgs) Handles btnFormatHelp.Click
        'Show Format information.
        MessageBox.Show("Format string examples:" & vbCrLf & "N4 - Number displayed with thousands separator and 4 decimal places" & vbCrLf & "F4 - Number displayed with 4 decimal places.", "Number Formatting")
    End Sub

    Private Sub txtMinFormat_LostFocus(sender As Object, e As EventArgs) Handles txtMinFormat.LostFocus
        dgvStatistics.Columns(1).DefaultCellStyle.Format = txtMinFormat.Text
    End Sub

    Private Sub txtMaxFormat_LostFocus(sender As Object, e As EventArgs) Handles txtMaxFormat.LostFocus
        dgvStatistics.Columns(2).DefaultCellStyle.Format = txtMaxFormat.Text
    End Sub

    Private Sub txtSumFormat_LostFocus(sender As Object, e As EventArgs) Handles txtSumFormat.LostFocus
        dgvStatistics.Columns(3).DefaultCellStyle.Format = txtSumFormat.Text
    End Sub

    Private Sub txtAvgFormat_LostFocus(sender As Object, e As EventArgs) Handles txtAvgFormat.LostFocus
        dgvStatistics.Columns(4).DefaultCellStyle.Format = txtAvgFormat.Text
    End Sub

    Private Sub txtStdDevFormat_LostFocus(sender As Object, e As EventArgs) Handles txtStdDevFormat.LostFocus
        dgvStatistics.Columns(5).DefaultCellStyle.Format = txtStdDevFormat.Text
    End Sub

    Private Sub txtVarFormat_LostFocus(sender As Object, e As EventArgs) Handles txtVarFormat.LostFocus
        dgvStatistics.Columns(6).DefaultCellStyle.Format = txtVarFormat.Text
    End Sub

    Private Sub dgvStatistics_Resize(sender As Object, e As EventArgs) Handles dgvStatistics.Resize
        'RepositionFormatBoxes()
    End Sub

    Private Sub dgvStatistics_ColumnWidthChanged(sender As Object, e As DataGridViewColumnEventArgs) Handles dgvStatistics.ColumnWidthChanged
        RepositionFormatBoxes()
    End Sub

    Private Sub btnUpdateStatistics_Click(sender As Object, e As EventArgs) Handles btnUpdateStatistics.Click
        ShowAllFieldStats()
    End Sub

    Public Sub UpdateTableList()
        cmbTableList.Items.Clear()
        For Each Item In Main.Distribution.Data.Tables
            cmbTableList.Items.Add(Item.TableName)
        Next
    End Sub

    Private Sub cmbTableList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTableList.SelectedIndexChanged
        UpdateTable()
    End Sub

    Public Sub DisableGridView()
        'dgvResults.Enabled = False
        dgvResults.DataSource = Nothing
    End Sub

    Public Sub EnableGridView()
        'dgvResults.Enabled = True
        UpdateTable()
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
                'Main.Distribution.Data.Tables(TableName).Rows.Add(RowInfo)
                'Main.Distribution.Data.Tables(TableName).Rows.Add(RowInfo.ToString)
                'Main.Distribution.Data.Tables(TableName).Rows.Add(RowInfo.Value)
                'Main.Distribution.Data.Tables(TableName).Rows.Add(RowInfo.Value.ToString)
                'Main.Distribution.Data.Tables(TableName).Rows.Add(Split(",", RowInfo.Value))
                Main.Distribution.Data.Tables(TableName).Rows.Add(Split(RowInfo.Value, ","))
            Next

            'Update the table list:
            If cmbTableList.Items.Contains(TableName) Then

            Else
                cmbTableList.Items.Add(TableName)
            End If

            If Main.cmbTableName.Items.Contains(TableName) Then

            Else
                Main.cmbTableName.Items.Add(TableName)
            End If

            'Display the Table:
            cmbTableList.SelectedIndex = cmbTableList.FindStringExact(TableName)
            UpdateTable()
        End If
    End Sub




#End Region 'Form Methods ---------------------------------------------------------------------------------------------------------------------------------------------------------------------


End Class