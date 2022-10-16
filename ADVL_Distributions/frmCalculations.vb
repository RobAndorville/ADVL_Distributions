Public Class frmCalculations
    'Probability Distribution calculations.

#Region " Variable Declarations - All the variables used in this form and this application." '=================================================================================================

    'Public WithEvents Chart As frmChart

#End Region 'Variable Declarations ------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Properties - All the properties used in this form and this application" '============================================================================================================

    Private _selDistrib As Integer = 0 'The selected distribution number. (0 if none selected.)
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

        dgvPDF.ColumnCount = 2
        dgvPDF.Columns(0).HeaderText = "Value"
        dgvPDF.Columns(0).Width = 50
        dgvPDF.Columns(1).HeaderText = "Probability Density"
        dgvPDF.Columns(1).ReadOnly = True
        dgvPDF.Columns(1).Width = 120
        'dgvPDF.Columns(2).HeaderText = "Label"
        'dgvPDF.Columns(2).Width = 50
        'dgvPDF.Columns(3).HeaderText = "Format"
        'dgvPDF.Columns(3).Width = 50
        'Dim chkPDFChart As New DataGridViewCheckBoxColumn
        'chkPDFChart.FlatStyle = FlatStyle.Flat
        'dgvPDF.Columns.Insert(4, chkPDFChart)
        'dgvPDF.Columns(4).HeaderText = "PDF Chart"
        'dgvPDF.Columns(4).Width = 40

        dgvPdfIntervalProb.ColumnCount = 3
        dgvPdfIntervalProb.Columns(0).HeaderText = "From Value"
        dgvPdfIntervalProb.Columns(0).Width = 50
        dgvPdfIntervalProb.Columns(1).HeaderText = "To Value"
        dgvPdfIntervalProb.Columns(1).Width = 50
        dgvPdfIntervalProb.Columns(2).HeaderText = "Probability"
        dgvPdfIntervalProb.Columns(2).ReadOnly = True
        dgvPdfIntervalProb.Columns(2).Width = 120
        'dgvPdfIntervalProb.Columns(3).HeaderText = "From Label"
        'dgvPdfIntervalProb.Columns(3).Width = 50
        'dgvPdfIntervalProb.Columns(4).HeaderText = "To Label"
        'dgvPdfIntervalProb.Columns(4).Width = 50
        'dgvPdfIntervalProb.Columns(5).HeaderText = "Value Format"
        'dgvPdfIntervalProb.Columns(5).Width = 50
        'dgvPdfIntervalProb.Columns(6).HeaderText = "Prob Label"
        'dgvPdfIntervalProb.Columns(6).Width = 50
        'dgvPdfIntervalProb.Columns(7).HeaderText = "Prob Format"
        'dgvPdfIntervalProb.Columns(7).Width = 50
        'dgvPdfIntervalProb.Columns(8).HeaderText = "Shading Density"
        'dgvPdfIntervalProb.Columns(8).Width = 50
        'dgvPdfIntervalProb.Columns(9).HeaderText = "Shading Color"
        'dgvPdfIntervalProb.Columns(9).Width = 50
        'Dim chkPDFIntProbChart As New DataGridViewCheckBoxColumn
        'chkPDFIntProbChart.FlatStyle = FlatStyle.Flat
        'dgvPdfIntervalProb.Columns.Insert(10, chkPDFIntProbChart)
        'dgvPdfIntervalProb.Columns(10).HeaderText = "PDF Chart"
        'dgvPdfIntervalProb.Columns(10).Width = 40

        dgvPMF.ColumnCount = 2
        dgvPMF.Columns(0).HeaderText = "Value"
        dgvPMF.Columns(0).Width = 50
        dgvPMF.Columns(1).HeaderText = "Probability Mass"
        dgvPMF.Columns(1).ReadOnly = True
        dgvPMF.Columns(1).Width = 120

        dgvPmfIntervalProb.ColumnCount = 3
        dgvPmfIntervalProb.Columns(0).HeaderText = "From Value"
        dgvPmfIntervalProb.Columns(0).Width = 50
        dgvPmfIntervalProb.Columns(1).HeaderText = "To Value"
        dgvPmfIntervalProb.Columns(1).Width = 50
        dgvPmfIntervalProb.Columns(2).HeaderText = "Probability"
        dgvPmfIntervalProb.Columns(2).ReadOnly = True
        dgvPmfIntervalProb.Columns(2).Width = 120

        dgvCDF.ColumnCount = 2
        dgvCDF.Columns(0).HeaderText = "Value"
        dgvCDF.Columns(0).Width = 50
        dgvCDF.Columns(1).HeaderText = "Probability"
        dgvCDF.Columns(1).ReadOnly = True
        dgvCDF.Columns(1).Width = 120

        dgvRevCDF.ColumnCount = 2
        dgvRevCDF.Columns(0).HeaderText = "Value"
        dgvRevCDF.Columns(0).Width = 50
        dgvRevCDF.Columns(1).HeaderText = "Probability"
        dgvRevCDF.Columns(1).ReadOnly = True
        dgvRevCDF.Columns(1).Width = 120

        dgvInvCDF.ColumnCount = 2
        dgvInvCDF.Columns(0).HeaderText = "Probability"
        dgvInvCDF.Columns(0).Width = 70
        dgvInvCDF.Columns(1).HeaderText = "Value"
        dgvInvCDF.Columns(1).ReadOnly = True
        dgvInvCDF.Columns(1).Width = 120

        dgvInvRevCDF.ColumnCount = 2
        dgvInvRevCDF.Columns(0).HeaderText = "Probability"
        dgvInvRevCDF.Columns(0).Width = 70
        dgvInvRevCDF.Columns(1).HeaderText = "Value"
        dgvInvRevCDF.Columns(1).ReadOnly = True
        dgvInvRevCDF.Columns(1).Width = 120

        txtN.Text = "1"

        ShowDistribution()

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

    Private Sub Label3_Click(sender As Object, e As EventArgs)

    End Sub



#End Region 'Form Display Methods -------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Open and Close Forms - Code used to open and close other forms." '===================================================================================================================

    Private Sub btnChartPdf_Click(sender As Object, e As EventArgs) Handles btnChartPdf.Click
        'Show the Calculations form.
        'If IsNothing(Chart) Then
        '    Chart = New frmChart
        '    Chart.DataSource = Main.Distribution
        '    Chart.TableName = "Continuous_Data_Table"
        '    Chart.Plot()
        '    Chart.Show()
        'Else
        '    Chart.Show()
        'End If

        'Open a Chart form.

        'If Distribution.Data.Tables.Contains("DataTable") Then
        If Main.Distribution.Data.Tables.Contains("Continuous_Data_Table") Then
            'The Data Table exists
        Else
            'Distribution.GenerateData() 'Generate the data before displaying the chart.
            'UpdateDataTableDisplay()
            Main.GenerateData()
        End If

        Dim ChartNo As Integer = Main.OpenNewChart()
        Main.ChartList(ChartNo).DataSource = Main.Distribution
        'ChartList(ChartNo).TableName = "DataTable"
        Main.ChartList(ChartNo).TableName = "Continuous_Data_Table"
        Main.ChartList(ChartNo).Plot



    End Sub



    'Private Sub Chart_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Chart.FormClosed
    '    Chart = Nothing
    'End Sub



#End Region 'Open and Close Forms -------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Form Methods - The main actions performed by this form." '===========================================================================================================================

    Private Sub dgvPDF_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPDF.CellContentClick

    End Sub

    Private Sub dgvPDF_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPDF.CellEndEdit
        'The Probability Density Calculations grid cell has been edited.

        Dim RowNo As Integer = e.RowIndex
        Dim ColNo As Integer = e.ColumnIndex

        If ColNo = 0 Then 'The Random Variable Value has been changed.
            Dim XValue As Double = dgvPDF.Rows(RowNo).Cells(0).Value
            dgvPDF.Rows(RowNo).Cells(1).Value = Main.Distribution.PdfValue(SelDistrib - 1, XValue)
        End If

    End Sub

    'Private Sub dgvPDF_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPDF.CellContentClick

    'End Sub

    'Private Sub dgvPDF_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPDF.CellEndEdit
    '    'The Probability Density Calculations grid cell has been edited.

    '    Dim RowNo As Integer = e.RowIndex
    '    Dim ColNo As Integer = e.ColumnIndex

    '    If ColNo = 0 Then 'The Random Variable Value has been changed.
    '        Dim XValue As Double = dgvPDF.Rows(RowNo).Cells(0).Value
    '        dgvPDF.Rows(RowNo).Cells(1).Value = Main.Distribution.PdfValue(SelDistrib - 1, XValue)
    '    End If

    'End Sub

    Private Sub dgvPdfIntervalProb_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPdfIntervalProb.CellContentClick

    End Sub

    Private Sub dgvPdfIntervalProb_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPdfIntervalProb.CellEndEdit
        'The Interval Probability Calculations grid cell has been edited.

        Dim RowNo As Integer = e.RowIndex
        Dim ColNo As Integer = e.ColumnIndex

        Try
            If ColNo = 0 Then 'The From Value has been changed.
                If dgvPdfIntervalProb.Rows(RowNo).Cells(1).Value = "" Then 'The To Value is not defined
                    dgvPdfIntervalProb.Rows(RowNo).Cells(2).Value = Main.Distribution.RevCdfValue(SelDistrib - 1, dgvPdfIntervalProb.Rows(RowNo).Cells(0).Value)
                Else
                    dgvPdfIntervalProb.Rows(RowNo).Cells(2).Value = Main.Distribution.IntervalProb(SelDistrib - 1, dgvPdfIntervalProb.Rows(RowNo).Cells(0).Value, dgvPdfIntervalProb.Rows(RowNo).Cells(1).Value)
                End If
            ElseIf ColNo = 1 Then 'The To Value has been changed.
                If dgvPdfIntervalProb.Rows(RowNo).Cells(0).Value = "" Then 'The From Value is not defined
                    dgvPdfIntervalProb.Rows(RowNo).Cells(2).Value = Main.Distribution.CdfValue(SelDistrib - 1, dgvPdfIntervalProb.Rows(RowNo).Cells(1).Value)
                Else
                    dgvPdfIntervalProb.Rows(RowNo).Cells(2).Value = Main.Distribution.IntervalProb(SelDistrib - 1, dgvPdfIntervalProb.Rows(RowNo).Cells(0).Value, dgvPdfIntervalProb.Rows(RowNo).Cells(1).Value)
                End If
            End If
        Catch ex As Exception
            Main.Message.AddWarning(ex.Message & vbCrLf)
        End Try

    End Sub

    'Private Sub dgvPdfIntervalProb_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPdfIntervalProb.CellContentClick

    'End Sub

    'Private Sub dgvPdfIntervalProb_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPdfIntervalProb.CellEndEdit
    '    'The Interval Probability Calculations grid cell has been edited.

    '    Dim RowNo As Integer = e.RowIndex
    '    Dim ColNo As Integer = e.ColumnIndex

    '    Try
    '        If ColNo = 0 Then 'The From Value has been changed.
    '            If dgvPdfIntervalProb.Rows(RowNo).Cells(1).Value = "" Then 'The To Value is not defined
    '                dgvPdfIntervalProb.Rows(RowNo).Cells(2).Value = Main.Distribution.RevCdfValue(SelDistrib - 1, dgvPdfIntervalProb.Rows(RowNo).Cells(0).Value)
    '            Else
    '                dgvPdfIntervalProb.Rows(RowNo).Cells(2).Value = Main.Distribution.IntervalProb(SelDistrib - 1, dgvPdfIntervalProb.Rows(RowNo).Cells(0).Value, dgvPdfIntervalProb.Rows(RowNo).Cells(1).Value)
    '            End If
    '        ElseIf ColNo = 1 Then 'The To Value has been changed.
    '            If dgvPdfIntervalProb.Rows(RowNo).Cells(0).Value = "" Then 'The From Value is not defined
    '                dgvPdfIntervalProb.Rows(RowNo).Cells(2).Value = Main.Distribution.CdfValue(SelDistrib - 1, dgvPdfIntervalProb.Rows(RowNo).Cells(1).Value)
    '            Else
    '                dgvPdfIntervalProb.Rows(RowNo).Cells(2).Value = Main.Distribution.IntervalProb(SelDistrib - 1, dgvPdfIntervalProb.Rows(RowNo).Cells(0).Value, dgvPdfIntervalProb.Rows(RowNo).Cells(1).Value)
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Main.Message.AddWarning(ex.Message & vbCrLf)
    '    End Try

    'End Sub

    Public Sub ShowDistribution()
        'Show the distribution name and parameters in txtDistribution.

        txtDistribution.Text = Main.Distribution.Info(Main.SelDistrib - 1).Name & " " & Main.Distribution.Info(Main.SelDistrib - 1).Suffix

    End Sub

    Private Sub dgvPMF_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPMF.CellContentClick

    End Sub

    Private Sub dgvPMF_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPMF.CellEndEdit
        'The Probability Mass Calculations grid cell has been edited.

        Dim RowNo As Integer = e.RowIndex
        Dim ColNo As Integer = e.ColumnIndex

        If ColNo = 0 Then 'The Random Variable Value has been changed.
            Dim XValue As Double = dgvPMF.Rows(RowNo).Cells(0).Value
            dgvPMF.Rows(RowNo).Cells(1).Value = Main.Distribution.PmfValue(SelDistrib - 1, XValue)
        End If
    End Sub

    Private Sub dgvPmfIntervalProb_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPmfIntervalProb.CellContentClick

    End Sub

    Private Sub dgvPmfIntervalProb_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPmfIntervalProb.CellEndEdit
        'The PMF Interval Probability Calculations grid cell has been edited.

        Dim RowNo As Integer = e.RowIndex
        Dim ColNo As Integer = e.ColumnIndex

        Try
            If ColNo = 0 Then 'The From Value has been changed.
                If dgvPmfIntervalProb.Rows(RowNo).Cells(1).Value = "" Then 'The To Value is not defined
                    dgvPmfIntervalProb.Rows(RowNo).Cells(2).Value = Main.Distribution.RevCdfValue(SelDistrib - 1, dgvPmfIntervalProb.Rows(RowNo).Cells(0).Value)
                Else
                    dgvPmfIntervalProb.Rows(RowNo).Cells(2).Value = Main.Distribution.IntervalProb(SelDistrib - 1, dgvPmfIntervalProb.Rows(RowNo).Cells(0).Value, dgvPmfIntervalProb.Rows(RowNo).Cells(1).Value)
                End If
            ElseIf ColNo = 1 Then 'The To Value has been changed.
                If dgvPmfIntervalProb.Rows(RowNo).Cells(0).Value = "" Then 'The From Value is not defined
                    dgvPmfIntervalProb.Rows(RowNo).Cells(2).Value = Main.Distribution.CdfValue(SelDistrib - 1, dgvPmfIntervalProb.Rows(RowNo).Cells(1).Value)
                Else
                    dgvPmfIntervalProb.Rows(RowNo).Cells(2).Value = Main.Distribution.IntervalProb(SelDistrib - 1, dgvPmfIntervalProb.Rows(RowNo).Cells(0).Value, dgvPmfIntervalProb.Rows(RowNo).Cells(1).Value)
                End If
            End If
        Catch ex As Exception
            Main.Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub

    Private Sub dgvCDF_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCDF.CellContentClick

    End Sub

    Private Sub dgvCDF_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCDF.CellEndEdit
        'The Cumulative Distribution Function Calculations grid cell has been edited.

        Dim RowNo As Integer = e.RowIndex
        Dim ColNo As Integer = e.ColumnIndex

        If ColNo = 0 Then 'The Random Variable Value has been changed.
            Dim XValue As Double = dgvCDF.Rows(RowNo).Cells(0).Value
            dgvCDF.Rows(RowNo).Cells(1).Value = Main.Distribution.CdfValue(SelDistrib - 1, XValue)
        End If
    End Sub

    Private Sub dgvRevCDF_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvRevCDF.CellContentClick

    End Sub

    Private Sub dgvRevCDF_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvRevCDF.CellEndEdit
        'The Reverse Cumulative Distribution Function Calculations grid cell has been edited.

        Dim RowNo As Integer = e.RowIndex
        Dim ColNo As Integer = e.ColumnIndex

        If ColNo = 0 Then 'The Random Variable Value has been changed.
            Dim XValue As Double = dgvRevCDF.Rows(RowNo).Cells(0).Value
            dgvRevCDF.Rows(RowNo).Cells(1).Value = Main.Distribution.RevCdfValue(SelDistrib - 1, XValue)
        End If
    End Sub

    Private Sub dgvInvCDF_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvInvCDF.CellContentClick

    End Sub

    Private Sub dgvInvCDF_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvInvCDF.CellEndEdit
        'The Inverse Cumulative Distribution Function Calculations grid cell has been edited.

        Dim RowNo As Integer = e.RowIndex
        Dim ColNo As Integer = e.ColumnIndex

        If ColNo = 0 Then 'The Random Variable Value has been changed.
            Dim XValue As Double = dgvInvCDF.Rows(RowNo).Cells(0).Value
            dgvInvCDF.Rows(RowNo).Cells(1).Value = Main.Distribution.InvCdfValue(SelDistrib - 1, XValue)
        End If
    End Sub

    Private Sub dgvInvRevCDF_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvInvRevCDF.CellContentClick

    End Sub

    Private Sub dgvInvRevCDF_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvInvRevCDF.CellEndEdit
        'The Inverse Reverse Cumulative Distribution Function Calculations grid cell has been edited.

        Dim RowNo As Integer = e.RowIndex
        Dim ColNo As Integer = e.ColumnIndex

        If ColNo = 0 Then 'The Random Variable Value has been changed.
            Dim XValue As Double = dgvInvRevCDF.Rows(RowNo).Cells(0).Value
            dgvInvRevCDF.Rows(RowNo).Cells(1).Value = Main.Distribution.InvRevCdfValue(SelDistrib - 1, XValue)
        End If
    End Sub










#End Region 'Form Methods ---------------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Form Events - Events that can be triggered by this form." '==========================================================================================================================

#End Region 'Form Events ----------------------------------------------------------------------------------------------------------------------------------------------------------------------

End Class