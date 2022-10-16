Public Class frmChart
    'Display a chart of the Probability Distribution.

#Region " Variable Declarations - All the variables used in this form and this application." '=================================================================================================

    Public WithEvents ChartSettings As frmChartSettings

    'Dim ChartInfo As Xml.Linq.XDocument 'Stores the chart information.
    'Dim ChartXml As Xml.Linq.XDocument 'Stores the chart information.
    'Dim ChartInfo As Xml.Linq.XDocument 'Stores the chart information.
    Public ChartInfo As Xml.Linq.XDocument 'Stores the chart information.

    'Dim TitleInfo As IEnumerable(Of XElement)
    Public TitleInfo As IEnumerable(Of XElement)
    Public SeriesInfo As IEnumerable(Of XElement)
    Public AreaInfo As IEnumerable(Of XElement)
    Public PointAnnotInfo As IEnumerable(Of XElement)
    Public AreaAnnotInfo As IEnumerable(Of XElement)


#End Region 'Variable Declarations ------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Properties - All the properties used in this form and this application" '============================================================================================================

    'Private _dataSource As Object = Nothing 'DataSource links to the object containing the Data to be analysed.
    Private _dataSource As DistributionModel = Nothing 'DataSource links to the object containing the Data to be analysed.
    'Every DataSource object must contain: Data, ChartList, ChartName
    'Property DataSource As Object
    Property DataSource As DistributionModel
        Get
            Return _dataSource
        End Get
        Set(value As DistributionModel)
            _dataSource = value

            'Show list of available charts:
            For Each item In _dataSource.ChartList
                cmbChartList.Items.Add(item.Key)
            Next
            If _dataSource.ChartName <> "" Then
                cmbChartList.SelectedIndex = cmbChartList.FindStringExact(_dataSource.ChartName)
            Else
                If cmbChartList.Items.Count = 0 Then
                    Main.Message.AddWarning("No chart availabe for plotting. Use the Chart Settings for to create a chart." & vbCrLf)
                    'Open the Chart Settings form:
                    If IsNothing(ChartSettings) Then
                        ChartSettings = New frmChartSettings
                        ChartSettings.ChartFormNo = FormNo
                        ChartSettings.myParent = Me
                        ChartSettings.Show()
                        ChartSettings.DataSource = DataSource
                        'ChartSettings.TableName = TableName
                        'ChartSettings.ContTableName = TableName
                        ChartSettings.ContTableName = ContTableName
                        ChartSettings.DiscTableName = DiscTableName
                        ChartSettings.ChartName = ChartName
                    Else
                        ChartSettings.Show()
                    End If
                Else
                    cmbChartList.SelectedIndex = 0 'Select the first chart in the list.
                End If
            End If
        End Set
    End Property

    'Private _tableName As String = "" 'The name of the table containing the data to plot.
    'Property TableName As String
    '    Get
    '        Return _tableName
    '    End Get
    '    Set(value As String)
    '        _tableName = value
    '    End Set
    'End Property

    Private _contTableName As String = "" 'The name of the table containing the continuous data to plot.
    Property ContTableName As String
        Get
            Return _contTableName
        End Get
        Set(value As String)
            _contTableName = value
        End Set
    End Property

    Private _discTableName As String = "" 'The name of the table containing the discrete data to plot.
    Property DiscTableName As String
        Get
            Return _discTableName
        End Get
        Set(value As String)
            _discTableName = value
        End Set
    End Property



    Private _chartName As String = "" 'The name of the chart shown on this form.
    Property ChartName As String
        Get
            Return _chartName
        End Get
        Set(value As String)
            _chartName = value
            SelectChart(_chartName)
        End Set
    End Property

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
        Chart1.SuppressExceptions = True
        'Timer1.Interval = 200 'Timer interval of 200ms - Running UpdateAnnotationSettings starts the timer - when 200ms have elapsed, if ChartSettings is open, UpdateAnnotationTabSettings() will be run.
        Timer1.Interval = 500 'Timer interval of 500ms - Running UpdateAnnotationSettings starts the timer - when 200ms have elapsed, if ChartSettings is open, UpdateAnnotationTabSettings() will be run.
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        'Exit the Form
        If IsNothing(ChartSettings) Then
            'The ChartSettings form is closed.
        Else
            ChartSettings.Close()
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

    Private Sub frmChart_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        If FormNo > -1 Then
            Main.ChartClosed()
        End If
    End Sub

#End Region 'Form Display Methods -------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Open and Close Forms - Code used to open and close other forms." '===================================================================================================================

    Private Sub cmbSettings_Click(sender As Object, e As EventArgs) Handles btnSettings.Click
        'Open the Chart Settings form:
        If IsNothing(ChartSettings) Then
            ChartSettings = New frmChartSettings
            ChartSettings.ChartFormNo = FormNo
            ChartSettings.myParent = Me 'Reference this form.
            ChartSettings.Show()
            ChartSettings.DataSource = DataSource
            'ChartSettings.TableName = TableName
            'ChartSettings.ContTableName = TableName
            ChartSettings.ContTableName = ContTableName
            ChartSettings.DiscTableName = DiscTableName
            ChartSettings.ChartName = ChartName
            'ChartSettings.Show()
        Else
            ChartSettings.Show()
        End If
    End Sub

    Private Sub ChartSettings_FormClosed(sender As Object, e As FormClosedEventArgs) Handles ChartSettings.FormClosed
        ChartSettings = Nothing
    End Sub

    Public Sub ReloadChartSettings()
        'If the ChartSettings form is open then ReloadChart
        If IsNothing(ChartSettings) Then
            'The ChartSettings form is not open.
        Else
            ChartSettings.ReloadChart()
        End If
    End Sub


#End Region 'Open and Close Forms -------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Form Methods - The main actions performed by this form." '===========================================================================================================================



    Private Sub btnPlotChart_Click(sender As Object, e As EventArgs) Handles btnPlotChart.Click
        'Plot the chart.
        Plot()
    End Sub

    Public Sub Plot()
        'Plot the selected chart.
        If cmbChartList.SelectedIndex = -1 Then
            If cmbChartList.Items.Count = 0 Then
                Main.Message.AddWarning("No chart availabe for plotting. Use the Chart Settings for to create a chart." & vbCrLf)
                'Open the Chart Settings form:
                If IsNothing(ChartSettings) Then
                    ChartSettings = New frmChartSettings
                    ChartSettings.ChartFormNo = FormNo
                    ChartSettings.myParent = Me
                    ChartSettings.Show()
                    ChartSettings.DataSource = DataSource
                    ChartSettings.ContTableName = ContTableName
                    ChartSettings.DiscTableName = DiscTableName
                    ChartSettings.ChartName = ChartName
                Else
                    ChartSettings.Show()
                End If
            Else
                Main.Message.AddWarning("A chart has not been selected for plotting." & vbCrLf)
            End If

        Else
            PlotChart(ChartName)
        End If
    End Sub

    Private Sub PlotChart(ByVal ChartName As String)
        'Plot the data chart.

        If DataSource.ChartList.ContainsKey(ChartName) Then
            Try
                Chart1.Legends.Clear() 'Clear the old legends - The new chart may use different areas
            Catch ex As Exception
                Main.Message.AddWarning("PlotChart error: Chart1.Legends.Clear() - " & vbCrLf & ex.Message & vbCrLf)
            End Try

            Chart1.Legends.Add("Legend1") '
            ChartInfo = DataSource.ChartList(ChartName)

            If ChartInfo.<ChartSettings>.<TableName>.Value <> Nothing Then ContTableName = ChartInfo.<ChartSettings>.<TableName>.Value
            If ChartInfo.<ChartSettings>.<ContTableName>.Value <> Nothing Then ContTableName = ChartInfo.<ChartSettings>.<ContTableName>.Value
            If ChartInfo.<ChartSettings>.<DiscTableName>.Value <> Nothing Then DiscTableName = ChartInfo.<ChartSettings>.<DiscTableName>.Value

            If ChartInfo.<ChartSettings>.<FormHeight>.Value <> Nothing Then Me.Height = ChartInfo.<ChartSettings>.<FormHeight>.Value
            If ChartInfo.<ChartSettings>.<FormWidth>.Value <> Nothing Then Me.Width = ChartInfo.<ChartSettings>.<FormWidth>.Value
            If ChartInfo.<ChartSettings>.<FormTop>.Value <> Nothing Then Me.Top = ChartInfo.<ChartSettings>.<FormTop>.Value
            If ChartInfo.<ChartSettings>.<FormLeft>.Value <> Nothing Then Me.Left = ChartInfo.<ChartSettings>.<FormLeft>.Value

            If IsNothing(AreaInfo) Then
                Main.Message.AddWarning("No chart areas are specified." & vbCrLf)
                Exit Sub
            End If
            ApplyChartAreas(AreaInfo)

            'Apply Chart Labels:
            ApplyChartLabels(TitleInfo)

            Chart1.Series.Clear()
            Chart1.Annotations.Clear()
            'ApplyAreaShading()
            'Dim AreaAnnotInfo As IEnumerable(Of XElement) = From item In ChartInfo.<ChartSettings>.<AreaAnnotationCollection>.<AreaAnnotation>
            'ApplyAreaShading(AreaAnnotInfo)
            ApplyAreaShading()
            ApplyAnnotation()

            'Apply Chart Series:
            'Dim SeriesInfo = From item In ChartInfo.<ChartSettings>.<SeriesCollection>.<Series>
            'ApplyChartSeries(SeriesInfo)
            ApplyChartSeries()

            'Dim PointAnnotInfo As IEnumerable(Of XElement) = From item In ChartInfo.<ChartSettings>.<PointAnnotationCollection>.<PointAnnotation>
            'ApplyAnnotation(PointAnnotInfo)
            'ApplyAnnotation() 'ANNOTATION MOVED TO BEFORE ApplyChartSeries SO THAT THE CHART SERIES ARE ON TOP

            'Dim AreaAnnotInfo As IEnumerable(Of XElement) = From item In ChartXml.<ChartSettings>.<AreaAnnotationCollection>.<AreaAnnotation>
            'ApplyAreaShading(AreaAnnotInfo)
        Else
            Main.Message.AddWarning("The Input Data chart named " & ChartName & " was not found in the Chart list." & vbCrLf)
        End If

    End Sub

    Public Sub PlotChart()
        'Plot the chart specified in ChartXml

        'Main.Message.Add("12 Chart1.ChartAreas.Count = " & Chart1.ChartAreas.Count & vbCrLf)

        Chart1.Legends.Clear() 'Clear the old legends - The new chart may use different areas
        Chart1.Legends.Add("Legend1") '

        'ChartXml = DataSource.ChartList(ChartName)

        'If ChartInfo.<ChartSettings>.<TableName>.Value <> Nothing Then TableName = ChartInfo.<ChartSettings>.<TableName>.Value
        If ChartInfo.<ChartSettings>.<TableName>.Value <> Nothing Then ContTableName = ChartInfo.<ChartSettings>.<TableName>.Value
        If ChartInfo.<ChartSettings>.<ContTableName>.Value <> Nothing Then ContTableName = ChartInfo.<ChartSettings>.<ContTableName>.Value
        If ChartInfo.<ChartSettings>.<DiscTableName>.Value <> Nothing Then DiscTableName = ChartInfo.<ChartSettings>.<DiscTableName>.Value

        If ChartInfo.<ChartSettings>.<FormHeight>.Value <> Nothing Then Me.Height = ChartInfo.<ChartSettings>.<FormHeight>.Value
        If ChartInfo.<ChartSettings>.<FormWidth>.Value <> Nothing Then Me.Width = ChartInfo.<ChartSettings>.<FormWidth>.Value
        If ChartInfo.<ChartSettings>.<FormTop>.Value <> Nothing Then Me.Top = ChartInfo.<ChartSettings>.<FormTop>.Value
        If ChartInfo.<ChartSettings>.<FormLeft>.Value <> Nothing Then Me.Left = ChartInfo.<ChartSettings>.<FormLeft>.Value

        'Dim AreaInfo = From item In ChartInfo.<ChartSettings>.<ChartAreasCollection>.<ChartArea>
        If IsNothing(AreaInfo) Then
            Main.Message.AddWarning("No chart areas are specified." & vbCrLf)
            Exit Sub
        End If
        ApplyChartAreas(AreaInfo)


        'Apply Chart Labels:
        'Dim TitleInfo = From item In ChartInfo.<ChartSettings>.<TitlesCollection>.<Title>
        ApplyChartLabels(TitleInfo)

        Chart1.Series.Clear()
        Chart1.Annotations.Clear()
        'ApplyAreaShading()
        'Dim AreaAnnotInfo As IEnumerable(Of XElement) = From item In ChartInfo.<ChartSettings>.<AreaAnnotationCollection>.<AreaAnnotation>
        'ApplyAreaShading(AreaAnnotInfo)
        ApplyAreaShading()
        ApplyAnnotation()


        'Apply Chart Series:
        'Dim SeriesInfo = From item In ChartInfo.<ChartSettings>.<SeriesCollection>.<Series> 'UPDATE 26/6/22 - SeriesInfo already exists
        'ApplyChartSeries(SeriesInfo)
        ApplyChartSeries()


        'Dim PointAnnotInfo As IEnumerable(Of XElement) = From item In ChartInfo.<ChartSettings>.<PointAnnotationCollection>.<PointAnnotation>
        'ApplyAnnotation(PointAnnotInfo)
        'ApplyAnnotation()
    End Sub

    Private Sub ApplyChartAreas(ByRef AreaInfo As IEnumerable(Of XElement))
        'Apply the Chart Areas in AreaInfo to the chart.
        Dim NAreas As Integer = AreaInfo.Count
        Dim AreaName As String
        Dim myFontStyle As FontStyle
        Dim myFontSize As Single
        'Chart1.ChartAreas.Clear()

        'Try
        '    If Chart1.ChartAreas Is Nothing Then

        '    Else
        '        Chart1.ChartAreas.Clear()
        '    End If
        'Catch ex As Exception
        '    Main.Message.AddWarning(ex.Message & vbCrLf)
        'End Try

        If IsNothing(AreaInfo) Then
            Main.Message.AddWarning("No chart areas are specified." & vbCrLf)
            Exit Sub
        End If

        'If Chart1.ChartAreas Is Nothing Then
        If IsNothing(Chart1.ChartAreas) Then

        Else
            Chart1.ChartAreas.Clear()
        End If

        Dim I As Integer = 0
        For Each item In AreaInfo
            AreaName = item.<Name>.Value
            Chart1.ChartAreas.Add(AreaName)

            If I = 0 Then
                Chart1.Legends(0).Name = AreaName
            Else
                If Chart1.Legends.IndexOf(AreaName) = -1 Then Chart1.Legends.Add(AreaName) 'Add the legend if it doesnt exist
            End If

            Chart1.Legends(I).DockedToChartArea = AreaName
            If item.<LegendDocking>.Value = Nothing Then
                Chart1.Legends(I).Docking = DataVisualization.Charting.Docking.Right 'Bottom, Left, Right, Top
            Else
                Select Case item.<LegendDocking>.Value
                    Case "Bottom"
                        Chart1.Legends(I).Docking = DataVisualization.Charting.Docking.Bottom
                    Case "Left"
                        Chart1.Legends(I).Docking = DataVisualization.Charting.Docking.Left
                    Case "Right"
                        Chart1.Legends(I).Docking = DataVisualization.Charting.Docking.Right
                    Case "Top"
                        Chart1.Legends(I).Docking = DataVisualization.Charting.Docking.Top
                    Case Else
                        Chart1.Legends(I).Docking = DataVisualization.Charting.Docking.Right
                End Select
            End If

            Chart1.Legends(I).Alignment = StringAlignment.Center
            Chart1.Legends(I).BorderColor = Color.Black
            Chart1.Legends(I).ShadowColor = Color.Gray
            Try
                Chart1.Legends(I).ForeColor = Color.FromArgb(ChartInfo.<ChartSettings>.<Legend>.<ForeColor>.Value)
                myFontStyle = FontStyle.Regular
                If ChartInfo.<ChartSettings>.<Legend>.<Font>.<Bold>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Bold
                If ChartInfo.<ChartSettings>.<Legend>.<Font>.<Italic>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Italic
                If ChartInfo.<ChartSettings>.<Legend>.<Font>.<Strikeout>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Strikeout
                If ChartInfo.<ChartSettings>.<Legend>.<Font>.<Underline>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Underline
                myFontSize = ChartInfo.<ChartSettings>.<Legend>.<Font>.<Size>.Value
                Chart1.Legends(I).Font = New Font(ChartInfo.<ChartSettings>.<Legend>.<Font>.<Name>.Value, myFontSize, myFontStyle)
            Catch ex As Exception
                Main.Message.AddWarning("The ChartInfo XML file does not save the Legend Font and Color." & vbCrLf)
            End Try

            I += 1

            If item.<CursorXIsUserEnabled>.Value <> Nothing Then Chart1.ChartAreas(AreaName).CursorX.IsUserEnabled = item.<CursorXIsUserEnabled>.Value
            If item.<CursorYIsUserEnabled>.Value <> Nothing Then Chart1.ChartAreas(AreaName).CursorY.IsUserEnabled = item.<CursorYIsUserEnabled>.Value
            If item.<CursorXInterval>.Value <> Nothing Then Chart1.ChartAreas(AreaName).CursorX.Interval = item.<CursorXInterval>.Value
            If item.<CursorYInterval>.Value <> Nothing Then Chart1.ChartAreas(AreaName).CursorY.Interval = item.<CursorYInterval>.Value
            If item.<CursorXIsUserSelectionEnabled>.Value <> Nothing Then Chart1.ChartAreas(AreaName).CursorX.IsUserSelectionEnabled = item.<CursorXIsUserSelectionEnabled>.Value
            If item.<CursorYIsUserSelectionEnabled>.Value <> Nothing Then Chart1.ChartAreas(AreaName).CursorY.IsUserSelectionEnabled = item.<CursorYIsUserSelectionEnabled>.Value

            'AxisX Properties:
            Chart1.ChartAreas(AreaName).AxisX.Title = item.<AxisX>.<Title>.<Text>.Value
            Chart1.ChartAreas(AreaName).AxisX.TitleAlignment = [Enum].Parse(GetType(StringAlignment), item.<AxisX>.<Title>.<Alignment>.Value)
            Chart1.ChartAreas(AreaName).AxisX.TitleForeColor = Color.FromArgb(item.<AxisX>.<Title>.<ForeColor>.Value)
            myFontStyle = FontStyle.Regular
            If item.<AxisX>.<Title>.<Font>.<Bold>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Bold
            If item.<AxisX>.<Title>.<Font>.<Italic>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Italic
            If item.<AxisX>.<Title>.<Font>.<Strikeout>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Strikeout
            If item.<AxisX>.<Title>.<Font>.<Underline>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Underline
            myFontSize = item.<AxisX>.<Title>.<Font>.<Size>.Value
            Chart1.ChartAreas(AreaName).AxisX.TitleFont = New Font(item.<AxisX>.<Title>.<Font>.<Name>.Value, myFontSize, myFontStyle)

            'If item.<AxisX>.<LabelStyleFormat>.Value <> Nothing Then Chart1.ChartAreas(AreaName).AxisX.LabelStyle.Format = item.<AxisX>.<LabelStyleFormat>.Value
            If item.<AxisX>.<LabelStyle>.<Format>.Value <> Nothing Then Chart1.ChartAreas(AreaName).AxisX.LabelStyle.Format = item.<AxisX>.<LabelStyle>.<Format>.Value
            myFontStyle = FontStyle.Regular
            If item.<AxisX>.<LabelStyle>.<Font>.<Bold>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Bold
            If item.<AxisX>.<LabelStyle>.<Font>.<Italic>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Italic
            If item.<AxisX>.<LabelStyle>.<Font>.<Strikeout>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Strikeout
            If item.<AxisX>.<LabelStyle>.<Font>.<Underline>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Underline
            myFontSize = item.<AxisX>.<LabelStyle>.<Font>.<Size>.Value
            Chart1.ChartAreas(AreaName).AxisX.LabelStyle.Font = New Font(item.<AxisX>.<LabelStyle>.<Font>.<Name>.Value, myFontSize, myFontStyle)

            If item.<AxisX>.<AutoMinimum>.Value = True Then
                Chart1.ChartAreas(AreaName).AxisX.Minimum = Double.NaN
            Else
                If item.<AxisX>.<Minimum>.Value <> Nothing Then
                    Chart1.ChartAreas(AreaName).AxisX.Minimum = item.<AxisX>.<Minimum>.Value
                Else
                    Chart1.ChartAreas(AreaName).AxisX.Minimum = Double.NaN
                End If
            End If

            If item.<AxisX>.<AutoMaximum>.Value = True Then
                Chart1.ChartAreas(AreaName).AxisX.Maximum = Double.NaN
            Else
                If item.<AxisX>.<Maximum>.Value <> Nothing Then
                    Chart1.ChartAreas(AreaName).AxisX.Maximum = item.<AxisX>.<Maximum>.Value
                Else
                    Chart1.ChartAreas(AreaName).AxisX.Maximum = Double.NaN
                End If
            End If
            Chart1.ChartAreas(AreaName).AxisX.LineWidth = item.<AxisX>.<LineWidth>.Value
            Chart1.ChartAreas(AreaName).AxisX.Interval = item.<AxisX>.<Interval>.Value
            Chart1.ChartAreas(AreaName).AxisX.IntervalOffset = item.<AxisX>.<IntervalOffset>.Value
            Chart1.ChartAreas(AreaName).AxisX.Crossing = item.<AxisX>.<Crossing>.Value
            If item.<AxisX>.<AutoInterval>.Value = True Then Chart1.ChartAreas(AreaName).AxisX.Interval = Double.NaN
            If item.<AxisX>.<ScaleViewZoomable>.Value <> Nothing Then Chart1.ChartAreas(AreaName).AxisX.ScaleView.Zoomable = item.<AxisX>.<ScaleViewZoomable>.Value

            If item.<AxisX>.<RoundAxisValues>.Value <> Nothing Then
                If item.<AxisX>.<RoundAxisValues>.Value = True Then Chart1.ChartAreas(AreaName).AxisX.RoundAxisValues()
            End If

            'Chart1.ChartAreas(AreaName).AxisX.LabelStyle.Font =
            'Chart1.ChartAreas(AreaName).AxisX.LabelStyle.ForeColor = 
            'Chart1.ChartAreas(AreaName).AxisX.LabelStyle.Format = 
            'Chart1.ChartAreas(AreaName).AxisX.LabelStyle.


            'AxisX2 Properties:
            Chart1.ChartAreas(AreaName).AxisX2.Title = item.<AxisX2>.<Title>.<Text>.Value
            Chart1.ChartAreas(AreaName).AxisX2.TitleAlignment = [Enum].Parse(GetType(StringAlignment), item.<AxisX2>.<Title>.<Alignment>.Value)
            Chart1.ChartAreas(AreaName).AxisX2.TitleForeColor = Color.FromArgb(item.<AxisX2>.<Title>.<ForeColor>.Value)
            myFontStyle = FontStyle.Regular
            If item.<AxisX2>.<Title>.<Font>.<Bold>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Bold
            If item.<AxisX2>.<Title>.<Font>.<Italic>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Italic
            If item.<AxisX2>.<Title>.<Font>.<Strikeout>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Strikeout
            If item.<AxisX2>.<Title>.<Font>.<Underline>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Underline
            myFontSize = item.<AxisX2>.<Title>.<Font>.<Size>.Value
            Chart1.ChartAreas(AreaName).AxisX2.TitleFont = New Font(item.<AxisX2>.<Title>.<Font>.<Name>.Value, myFontSize, myFontStyle)

            'If item.<AxisX2>.<LabelStyleFormat>.Value <> Nothing Then Chart1.ChartAreas(AreaName).AxisX2.LabelStyle.Format = item.<AxisX2>.<LabelStyleFormat>.Value
            If item.<AxisX2>.<LabelStyle>.<Format>.Value <> Nothing Then Chart1.ChartAreas(AreaName).AxisX2.LabelStyle.Format = item.<AxisX2>.<LabelStyle>.<Format>.Value
            myFontStyle = FontStyle.Regular
            If item.<AxisX2>.<LabelStyle>.<Font>.<Bold>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Bold
            If item.<AxisX2>.<LabelStyle>.<Font>.<Italic>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Italic
            If item.<AxisX2>.<LabelStyle>.<Font>.<Strikeout>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Strikeout
            If item.<AxisX2>.<LabelStyle>.<Font>.<Underline>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Underline
            myFontSize = item.<AxisX2>.<LabelStyle>.<Font>.<Size>.Value
            Chart1.ChartAreas(AreaName).AxisX2.LabelStyle.Font = New Font(item.<AxisX2>.<LabelStyle>.<Font>.<Name>.Value, myFontSize, myFontStyle)

            Chart1.ChartAreas(AreaName).AxisX2.Minimum = item.<AxisX2>.<Minimum>.Value
            If item.<AxisX2>.<AutoMinimum>.Value = True Then Chart1.ChartAreas(AreaName).AxisX2.Minimum = Double.NaN
            Chart1.ChartAreas(AreaName).AxisX2.Maximum = item.<AxisX2>.<Maximum>.Value
            If item.<AxisX2>.<AutoMaximum>.Value = True Then Chart1.ChartAreas(AreaName).AxisX2.Maximum = Double.NaN
            Chart1.ChartAreas(AreaName).AxisX2.LineWidth = item.<AxisX2>.<LineWidth>.Value
            Chart1.ChartAreas(AreaName).AxisX2.Interval = item.<AxisX2>.<Interval>.Value
            Chart1.ChartAreas(AreaName).AxisX2.IntervalOffset = item.<AxisX2>.<IntervalOffset>.Value
            Chart1.ChartAreas(AreaName).AxisX2.Crossing = item.<AxisX2>.<Crossing>.Value
            If item.<AxisX2>.<AutoInterval>.Value <> Nothing Then If item.<AxisX2>.<AutoInterval>.Value = True Then Chart1.ChartAreas(AreaName).AxisX2.Interval = Double.NaN

            If item.<AxisX2>.<ScaleViewZoomable>.Value <> Nothing Then Chart1.ChartAreas(AreaName).AxisX2.ScaleView.Zoomable = item.<AxisX2>.<ScaleViewZoomable>.Value

            If item.<AxisX2>.<RoundAxisValues>.Value <> Nothing Then
                If item.<AxisX2>.<RoundAxisValues>.Value = True Then Chart1.ChartAreas(AreaName).AxisX2.RoundAxisValues()
            End If

            'AxisY Properties:
            Chart1.ChartAreas(AreaName).AxisY.Title = item.<AxisY>.<Title>.<Text>.Value
            Chart1.ChartAreas(AreaName).AxisY.TitleAlignment = [Enum].Parse(GetType(StringAlignment), item.<AxisY>.<Title>.<Alignment>.Value)
            Chart1.ChartAreas(AreaName).AxisY.TitleForeColor = Color.FromArgb(item.<AxisY>.<Title>.<ForeColor>.Value)
            myFontStyle = FontStyle.Regular
            If item.<AxisY>.<Title>.<Font>.<Bold>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Bold
            If item.<AxisY>.<Title>.<Font>.<Italic>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Italic
            If item.<AxisY>.<Title>.<Font>.<Strikeout>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Strikeout
            If item.<AxisY>.<Title>.<Font>.<Underline>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Underline
            myFontSize = item.<AxisY>.<Title>.<Font>.<Size>.Value
            Chart1.ChartAreas(AreaName).AxisY.TitleFont = New Font(item.<AxisY>.<Title>.<Font>.<Name>.Value, myFontSize, myFontStyle)

            'If item.<AxisY>.<LabelStyleFormat>.Value <> Nothing Then Chart1.ChartAreas(AreaName).AxisY.LabelStyle.Format = item.<AxisY>.<LabelStyleFormat>.Value
            If item.<AxisY>.<LabelStyle>.<Format>.Value <> Nothing Then Chart1.ChartAreas(AreaName).AxisY.LabelStyle.Format = item.<AxisY>.<LabelStyle>.<Format>.Value
            myFontStyle = FontStyle.Regular
            If item.<AxisY>.<LabelStyle>.<Font>.<Bold>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Bold
            If item.<AxisY>.<LabelStyle>.<Font>.<Italic>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Italic
            If item.<AxisY>.<LabelStyle>.<Font>.<Strikeout>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Strikeout
            If item.<AxisY>.<LabelStyle>.<Font>.<Underline>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Underline
            myFontSize = item.<AxisY>.<LabelStyle>.<Font>.<Size>.Value
            Chart1.ChartAreas(AreaName).AxisY.LabelStyle.Font = New Font(item.<AxisY>.<LabelStyle>.<Font>.<Name>.Value, myFontSize, myFontStyle)

            Chart1.ChartAreas(AreaName).AxisY.Minimum = item.<AxisY>.<Minimum>.Value
            If item.<AxisY>.<AutoMinimum>.Value = True Then
                Chart1.ChartAreas(AreaName).AxisY.Minimum = Double.NaN
            Else
                If item.<AxisY>.<Maximum>.Value <> Nothing Then
                    Chart1.ChartAreas(AreaName).AxisY.Minimum = item.<AxisY>.<Minimum>.Value
                Else
                    Chart1.ChartAreas(AreaName).AxisY.Minimum = Double.NaN
                End If
            End If

            If item.<AxisY>.<AutoMaximum>.Value = True Then
                Chart1.ChartAreas(AreaName).AxisY.Maximum = Double.NaN
            Else
                If item.<AxisY>.<Maximum>.Value <> Nothing Then
                    Chart1.ChartAreas(AreaName).AxisY.Maximum = item.<AxisY>.<Maximum>.Value
                Else
                    Chart1.ChartAreas(AreaName).AxisY.Maximum = Double.NaN
                End If
            End If

            Chart1.ChartAreas(AreaName).AxisY.LineWidth = item.<AxisY>.<LineWidth>.Value
            Chart1.ChartAreas(AreaName).AxisY.Interval = item.<AxisY>.<Interval>.Value
            Chart1.ChartAreas(AreaName).AxisY.IntervalOffset = item.<AxisY>.<IntervalOffset>.Value
            Chart1.ChartAreas(AreaName).AxisY.Crossing = item.<AxisY>.<Crossing>.Value
            If item.<AxisY>.<AutoInterval>.Value <> Nothing Then If item.<AxisY>.<AutoInterval>.Value = True Then Chart1.ChartAreas(AreaName).AxisY.Interval = Double.NaN
            If item.<AxisY>.<ScaleViewZoomable>.Value <> Nothing Then Chart1.ChartAreas(AreaName).AxisY.ScaleView.Zoomable = item.<AxisY>.<ScaleViewZoomable>.Value

            'AxisY2 Properties:
            Chart1.ChartAreas(AreaName).AxisY2.Title = item.<AxisY2>.<Title>.<Text>.Value
            Chart1.ChartAreas(AreaName).AxisY2.TitleAlignment = [Enum].Parse(GetType(StringAlignment), item.<AxisY2>.<Title>.<Alignment>.Value)
            Chart1.ChartAreas(AreaName).AxisY2.TitleForeColor = Color.FromArgb(item.<AxisY2>.<Title>.<ForeColor>.Value)
            myFontStyle = FontStyle.Regular
            If item.<AxisY2>.<Title>.<Font>.<Bold>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Bold
            If item.<AxisY2>.<Title>.<Font>.<Italic>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Italic
            If item.<AxisY2>.<Title>.<Font>.<Strikeout>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Strikeout
            If item.<AxisY2>.<Title>.<Font>.<Underline>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Underline
            myFontSize = item.<AxisY2>.<Title>.<Font>.<Size>.Value
            Chart1.ChartAreas(AreaName).AxisY2.TitleFont = New Font(item.<AxisY2>.<Title>.<Font>.<Name>.Value, myFontSize, myFontStyle)

            'If item.<AxisY2>.<LabelStyleFormat>.Value <> Nothing Then Chart1.ChartAreas(AreaName).AxisY2.LabelStyle.Format = item.<AxisY2>.<LabelStyleFormat>.Value
            If item.<AxisY2>.<LabelStyle>.<Format>.Value <> Nothing Then Chart1.ChartAreas(AreaName).AxisY2.LabelStyle.Format = item.<AxisY2>.<LabelStyle>.<Format>.Value
            myFontStyle = FontStyle.Regular
            If item.<AxisY2>.<LabelStyle>.<Font>.<Bold>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Bold
            If item.<AxisY2>.<LabelStyle>.<Font>.<Italic>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Italic
            If item.<AxisY2>.<LabelStyle>.<Font>.<Strikeout>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Strikeout
            If item.<AxisY2>.<LabelStyle>.<Font>.<Underline>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Underline
            myFontSize = item.<AxisY2>.<LabelStyle>.<Font>.<Size>.Value
            Chart1.ChartAreas(AreaName).AxisY2.LabelStyle.Font = New Font(item.<AxisY2>.<LabelStyle>.<Font>.<Name>.Value, myFontSize, myFontStyle)

            Chart1.ChartAreas(AreaName).AxisY2.Minimum = item.<AxisY2>.<Minimum>.Value
            If item.<AxisY2>.<AutoMinimum>.Value = True Then Chart1.ChartAreas(AreaName).AxisY2.Minimum = Double.NaN
            Chart1.ChartAreas(AreaName).AxisY2.Maximum = item.<AxisY2>.<Maximum>.Value
            If item.<AxisY2>.<AutoMaximum>.Value = True Then Chart1.ChartAreas(AreaName).AxisY2.Maximum = Double.NaN
            Chart1.ChartAreas(AreaName).AxisY2.LineWidth = item.<AxisY2>.<LineWidth>.Value
            Chart1.ChartAreas(AreaName).AxisY2.Interval = item.<AxisY2>.<Interval>.Value
            Chart1.ChartAreas(AreaName).AxisY2.IntervalOffset = item.<AxisY2>.<IntervalOffset>.Value
            Chart1.ChartAreas(AreaName).AxisY2.Crossing = item.<AxisY2>.<Crossing>.Value
            If item.<AxisY2>.<AutoInterval>.Value <> Nothing Then If item.<AxisY2>.<AutoInterval>.Value = True Then Chart1.ChartAreas(AreaName).AxisY2.Interval = Double.NaN
            If item.<AxisY2>.<ScaleViewZoomable>.Value <> Nothing Then Chart1.ChartAreas(AreaName).AxisY2.ScaleView.Zoomable = item.<AxisY2>.<ScaleViewZoomable>.Value

            'Add the Series used to plot vertical bars on the chart.
            'The Series Name will be AreaName & "VertBar"
            Dim VertBarSeriesName As String = AreaName & "VertBar"
            Dim IndexNo As Integer = Chart1.Series.IndexOf(VertBarSeriesName)
            If IndexNo = -1 Then 'Series named VertBarSeriesName does not exist
                Chart1.Series.Add(VertBarSeriesName)
                Chart1.Series(VertBarSeriesName).ChartType = DataVisualization.Charting.SeriesChartType.Column
                Chart1.Series(VertBarSeriesName).Color = Color.Orange
                Chart1.Series(VertBarSeriesName).ChartArea = AreaName
                Chart1.Series(VertBarSeriesName).SetCustomProperty("PixelPointWidth", "2")
                Chart1.Series(VertBarSeriesName).IsVisibleInLegend = False
            Else
                Chart1.Series(VertBarSeriesName).Points.Clear()
            End If
        Next

        'Chart1.ChartAreas(0).AlignmentStyle = DataVisualization.Charting.AreaAlignmentStyles.AxesView
        'Chart1.ChartAreas(0).AlignmentStyle = DataVisualization.Charting.AreaAlignmentStyles.All
        'Chart1.ChartAreas(0).AlignmentStyle = DataVisualization.Charting.AreaAlignmentStyles.PlotPosition
        'Chart1.ChartAreas(0).BorderColor = Color.Black
        'Chart1.ChartAreas(0).BorderWidth = 2


    End Sub

    Private Sub ApplyChartLabels(ByRef TitleInfo As IEnumerable(Of XElement))
        'Apply the Chart Labels in TitleInfo to the chart.
        Dim NTitles As Integer = TitleInfo.Count
        Dim TitleName As String
        Dim myFontStyle As FontStyle
        Dim myFontSize As Single
        Chart1.Titles.Clear()
        For Each item In TitleInfo
            'If Chart1.ChartAreas.IndexOf(item.<ChartArea>.Value) > -1 Then
            If Chart1.ChartAreas.IndexOf(item.<ChartArea>.Value) > -1 Or item.<ChartArea>.Value = "" Then 'If ChartArea = "" THen this is the chart title.
                TitleName = item.<Name>.Value
                Dim NewTitle As New DataVisualization.Charting.Title
                NewTitle.Name = TitleName
                Chart1.Titles.Add(NewTitle)

                Chart1.Titles(TitleName).IsDockedInsideChartArea = False

                'NOTE: The code has been changed. The ChartArea property is set to "" for the main title - the DockedToChartArea will not be set.
                '  The - If TitleName = "MainTitle" Then - code can be remoded in the future. This just handles old chart files with the Chart Area set for the Main Title.
                If TitleName = "MainTitle" Then
                    'Do not set the DockedToChartArea property
                Else
                    If item.<ChartArea>.Value <> Nothing Then
                        If item.<ChartArea>.Value = "" Then

                        Else
                            Chart1.Titles(TitleName).DockedToChartArea = item.<ChartArea>.Value
                        End If
                    End If
                End If

                Chart1.Titles(TitleName).Text = item.<Text>.Value
                Chart1.Titles(TitleName).TextOrientation = [Enum].Parse(GetType(DataVisualization.Charting.TextOrientation), item.<TextOrientation>.Value)

                Try
                    Chart1.Titles(TitleName).Alignment = [Enum].Parse(GetType(ContentAlignment), item.<Alignment>.Value)
                Catch ex As Exception
                    Main.Message.AddWarning("Chart title alighment: " & ex.Message & vbCrLf)
                End Try

                Chart1.Titles(TitleName).ForeColor = Color.FromArgb(item.<ForeColor>.Value)
                myFontStyle = FontStyle.Regular
                If item.<Font>.<Bold>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Bold
                If item.<Font>.<Italic>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Italic
                If item.<Font>.<Strikeout>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Strikeout
                If item.<Font>.<Underline>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Underline
                myFontSize = item.<Font>.<Size>.Value
                Chart1.Titles(TitleName).Font = New Font(item.<Font>.<Name>.Value, myFontSize, myFontStyle)
            Else
                Main.Message.AddWarning("ApplyChartLabels: There is no Chart Area named: " & item.<ChartArea>.Value & vbCrLf)
            End If
        Next
    End Sub

    'Private Sub ApplyChartSeries(ByRef SeriesInfo As IEnumerable(Of XElement))
    Private Sub ApplyChartSeries()
        'Apply the Chart Series in SeriesInfo to the chart.


        'https://docs.microsoft.com/en-us/previous-versions/dd489233(v=vs.140)

        Dim NSeries As Integer = SeriesInfo.Count
        Dim SeriesName As String
        Dim myFontStyle As FontStyle
        Dim myFontSize As Single
        'Chart1.Series.Clear()

        'Chart1.Series.Add("PdfAreaVertBar") 'Add this as the first series - this will be plotted behind all other charts in the PdfArea
        'Chart1.Series.Add("PdfShadingVertBar_0_0") 'Add this as the first series - this will be plotted behind all other charts in the PdfArea

        For Each item In SeriesInfo
            If Chart1.ChartAreas.IndexOf(item.<ChartArea>.Value) > -1 Then
                'Update the SeriesName:
                If item.<DistribNo>.Value <> Nothing And item.<FunctionType>.Value <> Nothing Then
                    Dim DistribNo As Integer = item.<DistribNo>.Value
                    Dim FunctionType As String = item.<FunctionType>.Value
                    Select Case FunctionType
                        Case "PDF"
                            item.<Name>.Value = Main.Distribution.Info(DistribNo - 1).PdfInfo.Name
                        Case "PDFLn"
                            item.<Name>.Value = Main.Distribution.Info(DistribNo - 1).PdfLnInfo.Name
                        Case "PMF"
                            item.<Name>.Value = Main.Distribution.Info(DistribNo - 1).PmfInfo.Name
                        Case "PMFLn"
                            item.<Name>.Value = Main.Distribution.Info(DistribNo - 1).PmfLnInfo.Name
                        Case "CDF"
                            item.<Name>.Value = Main.Distribution.Info(DistribNo - 1).CdfInfo.Name
                        Case "RevCDF"
                            item.<Name>.Value = Main.Distribution.Info(DistribNo - 1).RevCdfInfo.Name
                        Case "InvCDF"
                            item.<Name>.Value = Main.Distribution.Info(DistribNo - 1).InvCdfInfo.Name
                        Case "InvRevCDF"
                            item.<Name>.Value = Main.Distribution.Info(DistribNo - 1).InvRevCdfInfo.Name
                        Case Else
                            Main.Message.AddWarning("Unknown distribution function type: " & FunctionType & vbCrLf)
                    End Select
                End If

                SeriesName = item.<Name>.Value
                If Chart1.Series.IndexOf(SeriesName) = -1 Then
                    Chart1.Series.Add(SeriesName)
                    Chart1.Series(SeriesName).ChartType = [Enum].Parse(GetType(DataVisualization.Charting.SeriesChartType), item.<ChartType>.Value)
                    If item.<ChartArea>.Value <> Nothing Then Chart1.Series(SeriesName).ChartArea = item.<ChartArea>.Value
                    Chart1.Series(SeriesName).Legend = item.<Legend>.Value
                    If item.<LegendText>.Value <> Nothing Then Chart1.Series(SeriesName).LegendText = item.<LegendText>.Value

                    'Point Chart custom properties
                    If item.<EmptyPointValue>.Value <> Nothing Then Chart1.Series(SeriesName).SetCustomProperty("EmptyPointValue", item.<EmptyPointValue>.Value)
                    If item.<LabelStyle>.Value <> Nothing Then Chart1.Series(SeriesName).SetCustomProperty("LabelStyle", item.<LabelStyle>.Value)
                    If item.<PixelPointDepth>.Value <> Nothing Then Chart1.Series(SeriesName).SetCustomProperty("PixelPointDepth", item.<PixelPointDepth>.Value)
                    If item.<PixelPointGapDepth>.Value <> Nothing Then Chart1.Series(SeriesName).SetCustomProperty("PixelPointGapDepth", item.<PixelPointGapDepth>.Value)
                    If item.<ShowMarkerLines>.Value <> Nothing Then Chart1.Series(SeriesName).SetCustomProperty("ShowMarkerLines", item.<ShowMarkerLines>.Value)

                    Chart1.Series(SeriesName).AxisLabel = item.<AxisLabel>.Value
                    Chart1.Series(SeriesName).XAxisType = [Enum].Parse(GetType(DataVisualization.Charting.AxisType), item.<XAxisType>.Value)
                    Chart1.Series(SeriesName).YAxisType = [Enum].Parse(GetType(DataVisualization.Charting.AxisType), item.<YAxisType>.Value)
                    If item.<XValueType>.Value <> Nothing Then Chart1.Series(SeriesName).XValueType = [Enum].Parse(GetType(DataVisualization.Charting.ChartValueType), item.<XValueType>.Value)
                    If item.<YValueType>.Value <> Nothing Then Chart1.Series(SeriesName).YValueType = [Enum].Parse(GetType(DataVisualization.Charting.ChartValueType), item.<YValueType>.Value)
                    If item.<Marker>.<BorderColor>.Value <> Nothing Then Chart1.Series(SeriesName).MarkerBorderColor = Color.FromArgb(item.<Marker>.<BorderColor>.Value)
                    If item.<Marker>.<BorderWidth>.Value <> Nothing Then Chart1.Series(SeriesName).MarkerBorderWidth = item.<Marker>.<BorderWidth>.Value
                    If item.<Marker>.<Color>.Value <> Nothing Then Chart1.Series(SeriesName).MarkerColor = Color.FromArgb(item.<Marker>.<Color>.Value)
                    If item.<Marker>.<Size>.Value <> Nothing Then Chart1.Series(SeriesName).MarkerSize = item.<Marker>.<Size>.Value
                    If item.<Marker>.<Step>.Value <> Nothing Then Chart1.Series(SeriesName).MarkerStep = item.<Marker>.<Step>.Value
                    If item.<Marker>.<Style>.Value <> Nothing Then Chart1.Series(SeriesName).MarkerStyle = [Enum].Parse(GetType(DataVisualization.Charting.MarkerStyle), item.<Marker>.<Style>.Value)
                    If item.<Color>.Value <> Nothing Then Chart1.Series(SeriesName).Color = Color.FromArgb(item.<Color>.Value)
                    If item.<Width>.Value <> Nothing Then
                        Chart1.Series(SeriesName).BorderWidth = item.<Width>.Value
                    Else
                        Main.Message.Add("Series Name: " & SeriesName & "  does not have a line width specified." & vbCrLf)
                    End If
                    If item.<ToolTip>.Value <> Nothing Then Chart1.Series(SeriesName).ToolTip = item.<ToolTip>.Value

                    'Load the data points:
                    Try
                        If item.<Continuity>.Value = "Continuous" Then
                            Chart1.Series(SeriesName).Points.DataBindXY(DataSource.Data.Tables("Continuous_Data_Table").DefaultView, item.<XFieldName>.Value, DataSource.Data.Tables("Continuous_Data_Table").DefaultView, item.<YFieldName>.Value)
                        ElseIf item.<Continuity>.Value = "Discrete" Then
                            Chart1.Series(SeriesName).Points.DataBindXY(DataSource.Data.Tables("Discrete_Data_Table").DefaultView, item.<XFieldName>.Value, DataSource.Data.Tables("Discrete_Data_Table").DefaultView, item.<YFieldName>.Value)
                        Else
                            Main.Message.AddWarning("Chart-ApplyChartSeries: Unknown distribution continuity" & item.<Continuity>.Value & vbCrLf)
                        End If
                    Catch ex As Exception
                        Main.Message.AddWarning("Chart-ApplyChartSeries: " & ex.Message & vbCrLf)
                    End Try
                Else
                    Main.Message.AddWarning("The series name is already used: " & SeriesName & vbCrLf)
                End If
            Else
                Main.Message.AddWarning("ApplyChartSeries: There is no Chart Area named: " & item.<ChartArea>.Value & vbCrLf)
            End If
        Next
    End Sub

    Public Sub UpdateAnnotationSettings(DistribNo As Integer)
        'Update the CDF Prob, Rev CDF Prob, RV Val, and Prob Dens for each of the annotations defiend for the selected distribution.
        'DistribNo is the one-based distribution number to process.

        Dim SelPointAnnotInfo = From item In ChartInfo.<ChartSettings>.<PointAnnotationCollection>.<PointAnnotation> Where item.<DistributionNo>.Value = DistribNo
        Dim CdfProb As Double
        Dim RevCdfProb As Double
        Dim RVValue As Double
        Dim ProbDens As Double
        For Each item In SelPointAnnotInfo
            Select Case item.<Type>.Value
                Case "Probability <="
                    CdfProb = item.<CdfProbability>.Value
                    'RVValue = GetInvCdfValue(CdfProb)
                    RVValue = Main.Distribution.Info(DistribNo - 1).InvCDF(CdfProb)
                    'ProbDens = SelectedDistrib.PDF(RVValue)
                    ProbDens = Main.Distribution.Info(DistribNo - 1).PDF(RVValue)
                    item.<RandVarValue>.Value = RVValue
                    item.<ProbabilityDensity>.Value = ProbDens

                Case "Probability >"
                    RevCdfProb = item.<RevCdfProbability>.Value
                    'RVValue = GetInvRevCdfValue(RevCdfProb)
                    RVValue = Main.Distribution.Info(DistribNo - 1).InvRevCDF(CdfProb)
                    'ProbDens = SelectedDistrib.PDF(RVValue)
                    ProbDens = Main.Distribution.Info(DistribNo - 1).PDF(RVValue)
                    item.<RandVarValue>.Value = RVValue
                    item.<ProbabilityDensity>.Value = ProbDens

                Case "Random Variable Value"
                    RVValue = item.<RandVarValue>.Value
                    'CdfProb = SelectedDistrib.CDF(RVValue)
                    CdfProb = Main.Distribution.Info(DistribNo - 1).CDF(RVValue)
                    'RevCdfProb = SelectedDistrib.RevCDF(RVValue)
                    RevCdfProb = Main.Distribution.Info(DistribNo - 1).RevCDF(RVValue)
                    'ProbDens = SelectedDistrib.PDF(RVValue)
                    ProbDens = Main.Distribution.Info(DistribNo - 1).PDF(RVValue)
                    item.<CdfProbability>.Value = CdfProb
                    item.<RevCdfProbability>.Value = RevCdfProb
                    item.<ProbabilityDensity>.Value = ProbDens

                Case "Mean"
                    'RVValue = SelectedDistrib.Mean
                    RVValue = Main.Distribution.Info(DistribNo - 1).Mean
                    'CdfProb = SelectedDistrib.CDF(RVValue)
                    CdfProb = Main.Distribution.Info(DistribNo - 1).CDF(RVValue)
                    'RevCdfProb = SelectedDistrib.RevCDF(RVValue)
                    RevCdfProb = Main.Distribution.Info(DistribNo - 1).RevCDF(RVValue)
                    'ProbDens = SelectedDistrib.PDF(RVValue)
                    ProbDens = Main.Distribution.Info(DistribNo - 1).PDF(RVValue)
                    item.<CdfProbability>.Value = CdfProb
                    item.<RevCdfProbability>.Value = RevCdfProb
                    item.<RandVarValue>.Value = RVValue
                    item.<ProbabilityDensity>.Value = ProbDens

                Case "Mode"
                    'RVValue = SelectedDistrib.Mode
                    RVValue = Main.Distribution.Info(DistribNo - 1).Mode
                    'CdfProb = SelectedDistrib.CDF(RVValue)
                    CdfProb = Main.Distribution.Info(DistribNo - 1).CDF(RVValue)
                    'RevCdfProb = SelectedDistrib.RevCDF(RVValue)
                    RevCdfProb = Main.Distribution.Info(DistribNo - 1).RevCDF(RVValue)
                    'ProbDens = SelectedDistrib.PDF(RVValue)
                    ProbDens = Main.Distribution.Info(DistribNo - 1).PDF(RVValue)
                    item.<CdfProbability>.Value = CdfProb
                    item.<RevCdfProbability>.Value = RevCdfProb
                    item.<RandVarValue>.Value = RVValue
                    item.<ProbabilityDensity>.Value = ProbDens

                Case "Median"
                    'RVValue = SelectedDistrib.Median
                    RVValue = Main.Distribution.Info(DistribNo - 1).Median
                    'CdfProb = SelectedDistrib.CDF(RVValue)
                    CdfProb = Main.Distribution.Info(DistribNo - 1).CDF(RVValue)
                    'RevCdfProb = SelectedDistrib.RevCDF(RVValue)
                    RevCdfProb = Main.Distribution.Info(DistribNo - 1).RevCDF(RVValue)
                    'ProbDens = SelectedDistrib.PDF(RVValue)
                    ProbDens = Main.Distribution.Info(DistribNo - 1).PDF(RVValue)
                    item.<CdfProbability>.Value = CdfProb
                    item.<RevCdfProbability>.Value = RevCdfProb
                    item.<RandVarValue>.Value = RVValue
                    item.<ProbabilityDensity>.Value = ProbDens

                Case "Standard Deviation"
                    'Dim Mean As Double = SelectedDistrib.Mean
                    Dim Mean As Double = Main.Distribution.Info(DistribNo - 1).Mean
                    'Dim StdDev As Double = SelectedDistrib.StdDev
                    Dim StdDev As Double = Main.Distribution.Info(DistribNo - 1).StdDev
                    Dim Param As Double = item.<Parameter>.Value
                    RVValue = Mean + Param * StdDev
                    'CdfProb = SelectedDistrib.CDF(RVValue)
                    CdfProb = Main.Distribution.Info(DistribNo - 1).CDF(RVValue)
                    'RevCdfProb = SelectedDistrib.RevCDF(RVValue)
                    RevCdfProb = Main.Distribution.Info(DistribNo - 1).RevCDF(RVValue)
                    'ProbDens = SelectedDistrib.PDF(RVValue)
                    ProbDens = Main.Distribution.Info(DistribNo - 1).PDF(RVValue)
                    item.<CdfProbability>.Value = CdfProb
                    item.<RevCdfProbability>.Value = RevCdfProb
                    item.<RandVarValue>.Value = RVValue
                    item.<ProbabilityDensity>.Value = ProbDens

                Case "User Defined Value 1"
                    RVValue = item.<RandVarValue>.Value
                    'CdfProb = SelectedDistrib.CDF(RVValue)
                    CdfProb = Main.Distribution.Info(DistribNo - 1).CDF(RVValue)
                    'RevCdfProb = SelectedDistrib.RevCDF(RVValue)
                    RevCdfProb = Main.Distribution.Info(DistribNo - 1).RevCDF(RVValue)
                    'ProbDens = SelectedDistrib.PDF(RVValue)
                    ProbDens = Main.Distribution.Info(DistribNo - 1).PDF(RVValue)
                    item.<CdfProbability>.Value = CdfProb
                    item.<RevCdfProbability>.Value = RevCdfProb
                    item.<ProbabilityDensity>.Value = ProbDens

                Case "User Defined Value 2"
                    RVValue = item.<RandVarValue>.Value
                    'CdfProb = SelectedDistrib.CDF(RVValue)
                    CdfProb = Main.Distribution.Info(DistribNo - 1).CDF(RVValue)
                    'RevCdfProb = SelectedDistrib.RevCDF(RVValue)
                    RevCdfProb = Main.Distribution.Info(DistribNo - 1).RevCDF(RVValue)
                    'ProbDens = SelectedDistrib.PDF(RVValue)
                    ProbDens = Main.Distribution.Info(DistribNo - 1).PDF(RVValue)
                    item.<CdfProbability>.Value = CdfProb
                    item.<RevCdfProbability>.Value = RevCdfProb
                    item.<ProbabilityDensity>.Value = ProbDens

                Case Else
                    Main.Message.AddWarning("Unknown annotation type: " & item.<Type>.Value & vbCrLf)
            End Select
            'RowNo += 1
        Next
        SelPointAnnotInfo = Nothing
        UpdateAreaAnnotation(DistribNo)
        UpdateSeriesLabels(DistribNo)

        Timer1.Start()
    End Sub

    Public Sub UpdateSeriesLabels(DistribNo As Integer)
        'Update the Series Labels

        If Main.Distribution.Info(DistribNo - 1).AutoUpdateLabels Then
            Dim SelSeriesInfo = From item In ChartInfo.<ChartSettings>.<SeriesCollection>.<Series> Where item.<DistribNo>.Value = DistribNo

            For Each item In SelSeriesInfo
                Select Case item.<FunctionType>.Value
                    Case "PDF"
                        item.<LegendText>.Value = Main.Distribution.Info(DistribNo - 1).PdfInfo.SeriesLabel
                    Case "PDFLn"
                        item.<LegendText>.Value = Main.Distribution.Info(DistribNo - 1).PdfLnInfo.SeriesLabel
                    Case "PMF"
                        item.<LegendText>.Value = Main.Distribution.Info(DistribNo - 1).PmfInfo.SeriesLabel
                    Case "PMFLn"
                        item.<LegendText>.Value = Main.Distribution.Info(DistribNo - 1).PmfLnInfo.SeriesLabel
                    Case "CDF"
                        item.<LegendText>.Value = Main.Distribution.Info(DistribNo - 1).CdfInfo.SeriesLabel
                    Case "RevCDF"
                        item.<LegendText>.Value = Main.Distribution.Info(DistribNo - 1).RevCdfInfo.SeriesLabel
                    Case "InvCDF"
                        item.<LegendText>.Value = Main.Distribution.Info(DistribNo - 1).InvCdfInfo.SeriesLabel
                    Case "InvRevCDF"
                        item.<LegendText>.Value = Main.Distribution.Info(DistribNo - 1).InvRevCdfInfo.SeriesLabel
                    Case Else
                        Main.Message.AddWarning("Chart: Updating  Series Labels: Unknown function type: " & item.<FunctionType>.Value & vbCrLf)
                End Select
            Next
            SelSeriesInfo = Nothing
        End If

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        '500ms after the last Scroll event, the ChartSettings.UpdateAnnotationTabSettings() will run.
        If IsNothing(ChartSettings) Then
            'The ChartSettings form is not open.
        Else
            ChartSettings.UpdateAnnotationTabSettings()
        End If
        Timer1.Stop()
    End Sub


    Public Sub UpdateUserDef1(NewValue As Double, DistribNo As Integer)
        'Update the value of User Defined Value 1 in the specified distribution number.

        'Update the Point Annotation:
        Dim SelPointAnnotInfo = From item In ChartInfo.<ChartSettings>.<PointAnnotationCollection>.<PointAnnotation> Where item.<DistributionNo>.Value = DistribNo
        Dim CdfProb As Double
        Dim RevCdfProb As Double
        Dim RVValue As Double
        Dim ProbDens As Double
        'Dim FromValueCDF As Double
        'Dim Probability As Double
        Dim RowNo As Integer = 0

        For Each item In SelPointAnnotInfo
            If item.<Type>.Value = "User Defined Value 1" Then
                'RVValue = item.<RandVarValue>.Value
                RVValue = NewValue
                CdfProb = Main.Distribution.Info(DistribNo - 1).CDF(RVValue)
                RevCdfProb = Main.Distribution.Info(DistribNo - 1).RevCDF(RVValue)
                ProbDens = Main.Distribution.Info(DistribNo - 1).PDF(RVValue)
                item.<Parameter>.Value = RVValue
                item.<RandVarValue>.Value = RVValue
                item.<CdfProbability>.Value = CdfProb
                item.<RevCdfProbability>.Value = RevCdfProb
                item.<ProbabilityDensity>.Value = ProbDens
            End If
        Next
        SelPointAnnotInfo = Nothing

        'Update the Area Annotation:
        Dim SelAreaAnnotInfo = From item In ChartInfo.<ChartSettings>.<AreaAnnotationCollection>.<AreaAnnotation> Where item.<DistributionNo>.Value = DistribNo
        For Each item In SelAreaAnnotInfo
            If item.<FromValueType>.Value = "User Defined Value 1" Then
                RVValue = NewValue
                CdfProb = Main.Distribution.Info(DistribNo - 1).CDF(RVValue)
                item.<FromValueParameter>.Value = RVValue
                item.<FromValue>.Value = RVValue
                item.<FromValueCDF>.Value = CdfProb
            End If
            If item.<ToValueType>.Value = "User Defined Value 1" Then
                RVValue = NewValue
                CdfProb = Main.Distribution.Info(DistribNo - 1).CDF(RVValue)
                item.<ToValueParameter>.Value = RVValue
                item.<ToValue>.Value = RVValue
                item.<ToValueCDF>.Value = CdfProb
                'FromValueCDF = item.<FromValueCDF>.Value
                'Probability = CdfProb - FromValueCDF
                'item.<Probability>.Value = Probability
                item.<Probability>.Value = CdfProb - item.<FromValueCDF>.Value
            End If
        Next
        SelAreaAnnotInfo = Nothing

        'UpdateAreaAnnotation(DistribNo)
        'Plot()

        Timer1.Start()
    End Sub

    Public Sub UpdateUserDef2(NewValue As Double, DistribNo As Integer)
        'Update the value of User Defined Value 1 in the specified distribution number.

        'Update the Point Annotation:
        Dim SelPointAnnotInfo = From item In ChartInfo.<ChartSettings>.<PointAnnotationCollection>.<PointAnnotation> Where item.<DistributionNo>.Value = DistribNo
        Dim CdfProb As Double
        Dim RevCdfProb As Double
        Dim RVValue As Double
        Dim ProbDens As Double
        'Dim FromValueCDF As Double
        'Dim Probability As Double
        Dim RowNo As Integer = 0

        For Each item In SelPointAnnotInfo
            If item.<Type>.Value = "User Defined Value 2" Then
                'RVValue = item.<RandVarValue>.Value
                RVValue = NewValue
                CdfProb = Main.Distribution.Info(DistribNo - 1).CDF(RVValue)
                RevCdfProb = Main.Distribution.Info(DistribNo - 1).RevCDF(RVValue)
                ProbDens = Main.Distribution.Info(DistribNo - 1).PDF(RVValue)
                item.<Parameter>.Value = RVValue
                item.<RandVarValue>.Value = RVValue
                item.<CdfProbability>.Value = CdfProb
                item.<RevCdfProbability>.Value = RevCdfProb
                item.<ProbabilityDensity>.Value = ProbDens
            End If
        Next
        SelPointAnnotInfo = Nothing

        'Update the Area Annotation:
        Dim SelAreaAnnotInfo = From item In ChartInfo.<ChartSettings>.<AreaAnnotationCollection>.<AreaAnnotation> Where item.<DistributionNo>.Value = DistribNo
        For Each item In SelAreaAnnotInfo
            If item.<FromValueType>.Value = "User Defined Value 2" Then
                RVValue = NewValue
                CdfProb = Main.Distribution.Info(DistribNo - 1).CDF(RVValue)
                item.<FromValueParameter>.Value = RVValue
                item.<FromValue>.Value = RVValue
                item.<FromValueCDF>.Value = CdfProb
            End If
            If item.<ToValueType>.Value = "User Defined Value 2" Then
                RVValue = NewValue
                CdfProb = Main.Distribution.Info(DistribNo - 1).CDF(RVValue)
                item.<ToValueParameter>.Value = RVValue
                item.<ToValue>.Value = RVValue
                item.<ToValueCDF>.Value = CdfProb
                'FromValueCDF = item.<FromValueCDF>.Value
                'Probability = CdfProb - FromValueCDF
                'item.<Probability>.Value = Probability
                item.<Probability>.Value = CdfProb - item.<FromValueCDF>.Value
            End If
        Next
        SelAreaAnnotInfo = Nothing

        'UpdateAreaAnnotation(DistribNo)
        'Plot()
    End Sub

    'Private Sub ApplyAnnotation(ByRef PointAnnotInfo As IEnumerable(Of XElement), NumberFormat As String)
    'Private Sub ApplyAnnotation(ByRef PointAnnotInfo As IEnumerable(Of XElement))
    Private Sub ApplyAnnotation()
        'Apply the Annotation

        'Get the annotation fonts:
        Dim myFontStyle As FontStyle
        Dim myFontSize As Single
        Dim myFontName As String

        'Get PMF annotation font and color:
        myFontStyle = FontStyle.Regular
        If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<PMF>.<Text>.<Bold>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Bold
        If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<PMF>.<Text>.<Italic>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Italic
        If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<PMF>.<Text>.<Strikeout>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Strikeout
        If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<PMF>.<Text>.<Underline>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Underline
        myFontSize = ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<PMF>.<Text>.<Size>.Value
        myFontName = ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<PMF>.<Text>.<FontName>.Value
        Dim PmfAnnotFont As New Font(myFontName, myFontSize, myFontStyle)
        Dim PmfAnnotColor As Color = Color.FromArgb(ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<PMF>.<Text>.<Color>.Value)

        Dim ProbFormat As String = ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<PDF>.<Text>.<ProbFormat>.Value
        Dim RVFormat As String = ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<PDF>.<Text>.<RVFormat>.Value

        Dim PdfArea As Integer = Chart1.ChartAreas.IndexOf("PdfArea")
        If PdfArea > -1 Then 'Annotation can be added to the Pdf Area
            'Get PDF annotation font and color:
            myFontStyle = FontStyle.Regular
            If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<PDF>.<Text>.<Bold>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Bold
            If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<PDF>.<Text>.<Italic>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Italic
            If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<PDF>.<Text>.<Strikeout>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Strikeout
            If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<PDF>.<Text>.<Underline>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Underline
            myFontSize = ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<PDF>.<Text>.<Size>.Value
            myFontName = ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<PDF>.<Text>.<FontName>.Value
            Dim PdfAnnotFont As New Font(myFontName, myFontSize, myFontStyle)
            Dim PdfAnnotColor As Color = Color.FromArgb(ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<PDF>.<Text>.<Color>.Value)

            'Add a series used to plot vertical bars on the CDF chart:
            Dim IndexNo As Integer = Chart1.Series.IndexOf("PdfAreaVertBar")
            If IndexNo = -1 Then 'Series named PdfVerBar does not exist
                Chart1.Series.Add("PdfAreaVertBar")
            Else
                Chart1.Series("PdfAreaVertBar").Points.Clear()
            End If
            Chart1.Series("PdfAreaVertBar").ChartType = DataVisualization.Charting.SeriesChartType.Column
            Chart1.Series("PdfAreaVertBar").ChartArea = "PdfArea"
            Chart1.Series("PdfAreaVertBar").Color = Color.FromArgb(ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<PDF>.<Line>.<Color>.Value)
            Chart1.Series("PdfAreaVertBar").SetCustomProperty("PixelPointWidth", ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<PDF>.<Line>.<Thickness>.Value)
            If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<PDF>.<Circle>.<Show>.Value = True Then
                Chart1.Series("PdfAreaVertBar").MarkerStyle = DataVisualization.Charting.MarkerStyle.Circle
            Else
                Chart1.Series("PdfAreaVertBar").MarkerStyle = DataVisualization.Charting.MarkerStyle.None
            End If
            Chart1.Series("PdfAreaVertBar").MarkerSize = ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<PDF>.<Circle>.<Size>.Value
            Chart1.Series("PdfAreaVertBar").MarkerBorderWidth = ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<PDF>.<Circle>.<Thickness>.Value
            Chart1.Series("PdfAreaVertBar").MarkerBorderColor = Color.FromArgb(ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<PDF>.<Circle>.<Color>.Value)
            Chart1.Series("PdfAreaVertBar").MarkerColor = Color.Transparent
            Chart1.Series("PdfAreaVertBar").IsVisibleInLegend = False

            For Each item In PointAnnotInfo
                If item.<PDF>.Value = True And item.<Show>.Value = True Then 'Add this annotation to the PDF Area
                    'Add the vertical bar:
                    Dim PdfPoint As New DataVisualization.Charting.DataPoint
                    PdfPoint.XValue = item.<RandVarValue>.Value
                    PdfPoint.SetValueY(item.<ProbabilityDensity>.Value)
                    Chart1.Series("PdfAreaVertBar").Points.Add(PdfPoint)
                    'Add the label:
                    Dim PdfAnnot As New DataVisualization.Charting.TextAnnotation
                    PdfAnnot.AxisX = Chart1.ChartAreas("PdfArea").AxisX
                    PdfAnnot.AxisY = Chart1.ChartAreas("PdfArea").AxisY
                    PdfAnnot.AnchorX = item.<RandVarValue>.Value
                    PdfAnnot.AnchorY = item.<ProbabilityDensity>.Value
                    PdfAnnot.AnchorAlignment = [Enum].Parse(GetType(ContentAlignment), item.<Alignment>.Value)
                    If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<PDF>.<Text>.<ShowProbValue>.Value = True Then 'Show the Probability Density value
                        If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<PDF>.<Text>.<ShowRVValue>.Value = True Then 'Show the Random Variable value
                            PdfAnnot.Text = item.<Label>.Value & " (" & Format(Val(item.<RandVarValue>.Value), RVFormat) & ", " & Format(Val(item.<ProbabilityDensity>.Value), ProbFormat) & ")"
                        Else 'Do not show the Random Variable value
                            PdfAnnot.Text = item.<Label>.Value & " (" & Format(Val(item.<ProbabilityDensity>.Value), ProbFormat) & ")"
                        End If
                    Else 'Do not show the Probability Density value
                        If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<PDF>.<Text>.<ShowRVValue>.Value = True Then 'Show the Random Variable value
                            PdfAnnot.Text = item.<Label>.Value & " (" & Format(Val(item.<RandVarValue>.Value), RVFormat) & ")"
                        Else 'Do not show the Random Variable value
                            PdfAnnot.Text = item.<Label>.Value
                        End If
                    End If
                    PdfAnnot.Font = PdfAnnotFont
                    PdfAnnot.ForeColor = PdfAnnotColor
                    Chart1.Annotations.Add(PdfAnnot)
                End If
            Next
            If Chart1.Series("PdfAreaVertBar").Points.Count = 1 Then
                If Chart1.Series("PdfAreaVertBar").Points(0).XValue = 0 Then Chart1.Series("PdfAreaVertBar").Points(0).XValue = 0.00000001 'If all the XValues are 0, the Chart control assumes thay are categories.
            End If
        End If


        'PDFLn Area Annotation
        Dim PdfLnArea As Integer = Chart1.ChartAreas.IndexOf("PdfLnArea")
        If PdfLnArea > -1 Then 'Annotation can be added to the PdfLn Area
            'Get PDFLn annotation font and color:
            myFontStyle = FontStyle.Regular
            If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<PDFLn>.<Text>.<Bold>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Bold
            If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<PDFLn>.<Text>.<Italic>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Italic
            If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<PDFLn>.<Text>.<Strikeout>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Strikeout
            If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<PDFLn>.<Text>.<Underline>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Underline
            myFontSize = ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<PDFLn>.<Text>.<Size>.Value
            myFontName = ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<PDFLn>.<Text>.<FontName>.Value
            Dim PdfLnAnnotFont As New Font(myFontName, myFontSize, myFontStyle)
            Dim PdfLnAnnotColor As Color = Color.FromArgb(ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<PDFLn>.<Text>.<Color>.Value)

            'Add a series used to plot vertical bars on the CDF chart:
            Dim IndexNo As Integer = Chart1.Series.IndexOf("PdfLnAreaVertBar")
            If IndexNo = -1 Then 'Series named PdfLnVertBar does not exist
                Chart1.Series.Add("PdfLnAreaVertBar")
            Else
                Chart1.Series("PdfLnAreaVertBar").Points.Clear()
            End If
            Chart1.Series("PdfLnAreaVertBar").ChartType = DataVisualization.Charting.SeriesChartType.Column
            Chart1.Series("PdfLnAreaVertBar").ChartArea = "PdfLnArea"
            Chart1.Series("PdfLnAreaVertBar").Color = Color.FromArgb(ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<PDFLn>.<Line>.<Color>.Value)
            Chart1.Series("PdfLnAreaVertBar").SetCustomProperty("PixelPointWidth", ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<PDFLn>.<Line>.<Thickness>.Value)
            If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<PDFLn>.<Circle>.<Show>.Value = True Then
                Chart1.Series("PdfLnAreaVertBar").MarkerStyle = DataVisualization.Charting.MarkerStyle.Circle
            Else
                Chart1.Series("PdfLnAreaVertBar").MarkerStyle = DataVisualization.Charting.MarkerStyle.None
            End If
            Chart1.Series("PdfLnAreaVertBar").MarkerSize = ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<PDFLn>.<Circle>.<Size>.Value
            Chart1.Series("PdfLnAreaVertBar").MarkerBorderWidth = ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<PDFLn>.<Circle>.<Thickness>.Value
            Chart1.Series("PdfLnAreaVertBar").MarkerBorderColor = Color.FromArgb(ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<PDFLn>.<Circle>.<Color>.Value)
            Chart1.Series("PdfLnAreaVertBar").MarkerColor = Color.Transparent
            Chart1.Series("PdfLnAreaVertBar").IsVisibleInLegend = False

            For Each item In PointAnnotInfo
                If item.<PDFLn>.Value = True And item.<Show>.Value = True Then 'Add this annotation to the PDFLn Area
                    'Add the vertical bar:
                    Dim PdfLnPoint As New DataVisualization.Charting.DataPoint
                    PdfLnPoint.XValue = item.<RandVarValue>.Value
                    PdfLnPoint.SetValueY(Math.Log(item.<ProbabilityDensity>.Value))
                    Chart1.Series("PdfLnAreaVertBar").Points.Add(PdfLnPoint)
                    'Add the label:
                    Dim PdfLnAnnot As New DataVisualization.Charting.TextAnnotation
                    PdfLnAnnot.AxisX = Chart1.ChartAreas("PdfLnArea").AxisX
                    PdfLnAnnot.AxisY = Chart1.ChartAreas("PdfLnArea").AxisY
                    PdfLnAnnot.AnchorX = item.<RandVarValue>.Value
                    PdfLnAnnot.AnchorY = Math.Log(item.<ProbabilityDensity>.Value)
                    PdfLnAnnot.AnchorAlignment = [Enum].Parse(GetType(ContentAlignment), item.<Alignment>.Value)
                    If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<PDFLn>.<Text>.<ShowProbValue>.Value = True Then 'Show the Probability Density value
                        If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<PDFLn>.<Text>.<ShowRVValue>.Value = True Then 'Show the Random Variable value
                            PdfLnAnnot.Text = item.<Label>.Value & " (" & Format(Val(item.<RandVarValue>.Value), RVFormat) & ", " & Format(Val(Math.Log(item.<ProbabilityDensity>.Value)), ProbFormat) & ")"
                        Else 'Do not show the Random Variable value
                            PdfLnAnnot.Text = item.<Label>.Value & " (" & Format(Val(Math.Log(item.<ProbabilityDensity>.Value)), ProbFormat) & ")"
                        End If
                    Else 'Do not show the Probability Density value
                        If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<PDFLn>.<Text>.<ShowRVValue>.Value = True Then 'Show the Random Variable value
                            PdfLnAnnot.Text = item.<Label>.Value & " (" & Format(Val(item.<RandVarValue>.Value), RVFormat) & ")"
                        Else 'Do not show the Random Variable value
                            PdfLnAnnot.Text = item.<Label>.Value
                        End If
                    End If
                    PdfLnAnnot.Font = PdfLnAnnotFont
                    PdfLnAnnot.ForeColor = PdfLnAnnotColor
                    Chart1.Annotations.Add(PdfLnAnnot)
                End If
            Next
            If Chart1.Series("PdfLnAreaVertBar").Points.Count = 1 Then
                If Chart1.Series("PdfLnAreaVertBar").Points(0).XValue = 0 Then Chart1.Series("PdfLnAreaVertBar").Points(0).XValue = 0.00000001 'If all the XValues are 0, the Chart control assumes thay are categories.
            End If
        End If





        Dim CdfArea As Integer = Chart1.ChartAreas.IndexOf("CdfArea")
        If CdfArea > -1 Then 'Annotation can be added to the Cdf Area
            'Get CDF annotation font and color:
            myFontStyle = FontStyle.Regular
            If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<CDF>.<Text>.<Bold>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Bold
            If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<CDF>.<Text>.<Italic>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Italic
            If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<CDF>.<Text>.<Strikeout>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Strikeout
            If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<CDF>.<Text>.<Underline>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Underline
            myFontSize = ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<CDF>.<Text>.<Size>.Value
            myFontName = ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<CDF>.<Text>.<FontName>.Value
            Dim CdfAnnotFont As New Font(myFontName, myFontSize, myFontStyle)
            Dim CdfAnnotColor As Color = Color.FromArgb(ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<CDF>.<Text>.<Color>.Value)

            'Add a series used to plot vertical bars on the CDF chart:
            Dim IndexNo As Integer = Chart1.Series.IndexOf("CdfAreaVertBar")
            ProbFormat = ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<CDF>.<Text>.<ProbFormat>.Value
            RVFormat = ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<CDF>.<Text>.<RVFormat>.Value
            If IndexNo = -1 Then 'Series named CdfVerBar does not exist
                Chart1.Series.Add("CdfAreaVertBar")
            Else
                Chart1.Series("CdfAreaVertBar").Points.Clear()
            End If
            Chart1.Series("CdfAreaVertBar").ChartType = DataVisualization.Charting.SeriesChartType.Column
            Chart1.Series("CdfAreaVertBar").ChartArea = "CdfArea"
            Chart1.Series("CdfAreaVertBar").Color = Color.FromArgb(ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<CDF>.<Line>.<Color>.Value)
            Chart1.Series("CdfAreaVertBar").SetCustomProperty("PixelPointWidth", ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<CDF>.<Line>.<Thickness>.Value)
            If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<CDF>.<Circle>.<Show>.Value = True Then
                Chart1.Series("CdfAreaVertBar").MarkerStyle = DataVisualization.Charting.MarkerStyle.Circle
            Else
                Chart1.Series("CdfAreaVertBar").MarkerStyle = DataVisualization.Charting.MarkerStyle.None
            End If
            Chart1.Series("CdfAreaVertBar").MarkerSize = ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<CDF>.<Circle>.<Size>.Value
            Chart1.Series("CdfAreaVertBar").MarkerBorderWidth = ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<CDF>.<Circle>.<Thickness>.Value
            Chart1.Series("CdfAreaVertBar").MarkerBorderColor = Color.FromArgb(ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<CDF>.<Circle>.<Color>.Value)
            Chart1.Series("CdfAreaVertBar").MarkerColor = Color.Transparent
            Chart1.Series("CdfAreaVertBar").IsVisibleInLegend = False

            For Each item In PointAnnotInfo
                If item.<CDF>.Value = True And item.<Show>.Value = True Then 'Add this annotation to the CDF Area
                    'Add the vertical bar:
                    Dim CdfPoint As New DataVisualization.Charting.DataPoint
                    CdfPoint.XValue = item.<RandVarValue>.Value
                    CdfPoint.SetValueY(item.<CdfProbability>.Value)
                    Chart1.Series("CdfAreaVertBar").Points.Add(CdfPoint)
                    'Add the label:
                    Dim CdfAnnot As New DataVisualization.Charting.TextAnnotation
                    CdfAnnot.AxisX = Chart1.ChartAreas("CdfArea").AxisX
                    CdfAnnot.AxisY = Chart1.ChartAreas("CdfArea").AxisY
                    CdfAnnot.AnchorX = item.<RandVarValue>.Value
                    CdfAnnot.AnchorY = item.<CdfProbability>.Value
                    CdfAnnot.AnchorAlignment = [Enum].Parse(GetType(ContentAlignment), item.<Alignment>.Value)
                    If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<CDF>.<Text>.<ShowProbValue>.Value = True Then 'Show the Probability value
                        If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<CDF>.<Text>.<ShowRVValue>.Value = True Then 'Show the Random Variable value
                            CdfAnnot.Text = item.<Label>.Value & " (" & Format(Val(item.<RandVarValue>.Value), RVFormat) & ", " & Format(Val(item.<CdfProbability>.Value), ProbFormat) & ")"
                        Else 'Do not show the Random Variable value
                            CdfAnnot.Text = item.<Label>.Value & " (" & Format(Val(item.<CdfProbability>.Value), ProbFormat) & ")"
                        End If
                    Else 'Do not show the Probability value
                        If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<CDF>.<Text>.<ShowRVValue>.Value = True Then 'Show the Random Variable value
                            CdfAnnot.Text = item.<Label>.Value & " (" & Format(Val(item.<RandVarValue>.Value), RVFormat) & ")"
                        Else 'Do not show the Random Variable value
                            CdfAnnot.Text = item.<Label>.Value
                        End If
                    End If
                    CdfAnnot.Font = CdfAnnotFont
                    CdfAnnot.ForeColor = CdfAnnotColor
                    Chart1.Annotations.Add(CdfAnnot)
                End If
            Next
            If Chart1.Series("CdfAreaVertBar").Points.Count = 1 Then
                If Chart1.Series("CdfAreaVertBar").Points(0).XValue = 0 Then Chart1.Series("CdfAreaVertBar").Points(0).XValue = 0.00000001 'If all the XValues are 0, the Chart control assumes they are categories.
            End If
        End If

        Dim RevCdfArea As Integer = Chart1.ChartAreas.IndexOf("RevCdfArea")
        If RevCdfArea > -1 Then 'Annotation can be added to the RevCdf Area
            'Get RevCDF annotation font and color:
            myFontStyle = FontStyle.Regular
            If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<RevCDF>.<Text>.<Bold>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Bold
            If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<RevCDF>.<Text>.<Italic>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Italic
            If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<RevCDF>.<Text>.<Strikeout>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Strikeout
            If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<RevCDF>.<Text>.<Underline>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Underline
            myFontSize = ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<RevCDF>.<Text>.<Size>.Value
            myFontName = ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<RevCDF>.<Text>.<FontName>.Value
            Dim RevCdfAnnotFont As New Font(myFontName, myFontSize, myFontStyle)
            Dim RevCdfAnnotColor As Color = Color.FromArgb(ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<RevCDF>.<Text>.<Color>.Value)

            'Add a series used to plot vertical bars on the RevCDF chart:
            Dim IndexNo As Integer = Chart1.Series.IndexOf("RevCdfAreaVertBar")
            ProbFormat = ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<RevCDF>.<Text>.<ProbFormat>.Value
            RVFormat = ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<RevCDF>.<Text>.<RVFormat>.Value
            If IndexNo = -1 Then 'Series named RevCdfVerBar does not exist
                Chart1.Series.Add("RevCdfAreaVertBar")
            Else
                Chart1.Series("RevCdfAreaVertBar").Points.Clear()
            End If
            Chart1.Series("RevCdfAreaVertBar").ChartType = DataVisualization.Charting.SeriesChartType.Column
            'Chart1.Series("RevCdfAreaVertBar").ChartArea = "CdfArea"
            Chart1.Series("RevCdfAreaVertBar").ChartArea = "RevCdfArea"
            Chart1.Series("RevCdfAreaVertBar").Color = Color.FromArgb(ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<RevCDF>.<Line>.<Color>.Value)
            Chart1.Series("RevCdfAreaVertBar").SetCustomProperty("PixelPointWidth", ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<RevCDF>.<Line>.<Thickness>.Value)
            If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<RevCDF>.<Circle>.<Show>.Value = True Then
                Chart1.Series("RevCdfAreaVertBar").MarkerStyle = DataVisualization.Charting.MarkerStyle.Circle
            Else
                Chart1.Series("RevCdfAreaVertBar").MarkerStyle = DataVisualization.Charting.MarkerStyle.None
            End If
            Chart1.Series("RevCdfAreaVertBar").MarkerSize = ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<RevCDF>.<Circle>.<Size>.Value
            Chart1.Series("RevCdfAreaVertBar").MarkerBorderWidth = ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<RevCDF>.<Circle>.<Thickness>.Value
            Chart1.Series("RevCdfAreaVertBar").MarkerBorderColor = Color.FromArgb(ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<RevCDF>.<Circle>.<Color>.Value)
            Chart1.Series("RevCdfAreaVertBar").MarkerColor = Color.Transparent
            Chart1.Series("RevCdfAreaVertBar").IsVisibleInLegend = False

            For Each item In PointAnnotInfo
                If item.<RevCDF>.Value = True And item.<Show>.Value = True Then 'Add this annotation to the RevCDF Area
                    'Add the vertical bar:
                    Dim RevCdfPoint As New DataVisualization.Charting.DataPoint
                    RevCdfPoint.XValue = item.<RandVarValue>.Value
                    RevCdfPoint.SetValueY(item.<RevCdfProbability>.Value)
                    Chart1.Series("RevCdfAreaVertBar").Points.Add(RevCdfPoint)
                    'Add the label:
                    Dim RevCdfAnnot As New DataVisualization.Charting.TextAnnotation
                    RevCdfAnnot.AxisX = Chart1.ChartAreas("RevCdfArea").AxisX
                    RevCdfAnnot.AxisY = Chart1.ChartAreas("RevCdfArea").AxisY
                    RevCdfAnnot.AnchorX = item.<RandVarValue>.Value
                    RevCdfAnnot.AnchorY = item.<RevCdfProbability>.Value
                    RevCdfAnnot.AnchorAlignment = [Enum].Parse(GetType(ContentAlignment), item.<Alignment>.Value)
                    If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<RevCDF>.<Text>.<ShowProbValue>.Value = True Then 'Show the Probability value
                        If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<RevCDF>.<Text>.<ShowRVValue>.Value = True Then 'Show the Random Variable value
                            RevCdfAnnot.Text = item.<Label>.Value & " (" & Format(Val(item.<RandVarValue>.Value), RVFormat) & ", " & Format(Val(item.<RevCdfProbability>.Value), ProbFormat) & ")"
                        Else 'Do not show the Random Variable value
                            RevCdfAnnot.Text = item.<Label>.Value & " (" & Format(Val(item.<RevCdfProbability>.Value), ProbFormat) & ")"
                        End If
                    Else 'Do not show the Probability value
                        If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<RevCDF>.<Text>.<ShowRVValue>.Value = True Then 'Show the Random Variable value
                            RevCdfAnnot.Text = item.<Label>.Value & " (" & Format(Val(item.<RandVarValue>.Value), RVFormat) & ")"
                        Else 'Do not show the Random Variable value
                            RevCdfAnnot.Text = item.<Label>.Value
                        End If
                    End If
                    RevCdfAnnot.Font = RevCdfAnnotFont
                    RevCdfAnnot.ForeColor = RevCdfAnnotColor
                    Chart1.Annotations.Add(RevCdfAnnot)
                End If
            Next
            If Chart1.Series("RevCdfAreaVertBar").Points.Count = 1 Then
                If Chart1.Series("RevCdfAreaVertBar").Points(0).XValue = 0 Then Chart1.Series("RevCdfAreaVertBar").Points(0).XValue = 0.00000001 'If all the XValues are 0, the Chart control assumes thay are categories.
            End If
        End If

        Dim InvCdfArea As Integer = Chart1.ChartAreas.IndexOf("InvCdfArea")
        If InvCdfArea > -1 Then 'Annotation can be added to the InvCdf Area
            'Get InvCDF annotation font and color:
            myFontStyle = FontStyle.Regular
            If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<InvCDF>.<Text>.<Bold>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Bold
            If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<InvCDF>.<Text>.<Italic>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Italic
            If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<InvCDF>.<Text>.<Strikeout>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Strikeout
            If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<InvCDF>.<Text>.<Underline>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Underline
            myFontSize = ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<InvCDF>.<Text>.<Size>.Value
            myFontName = ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<InvCDF>.<Text>.<FontName>.Value
            Dim InvCdfAnnotFont As New Font(myFontName, myFontSize, myFontStyle)
            Dim InvCdfAnnotColor As Color = Color.FromArgb(ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<InvCDF>.<Text>.<Color>.Value)

            'Add a series used to plot vertical bars on the InvCDF chart:
            Dim IndexNo As Integer = Chart1.Series.IndexOf("InvCdfAreaVertBar")
            ProbFormat = ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<InvCDF>.<Text>.<ProbFormat>.Value
            RVFormat = ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<InvCDF>.<Text>.<RVFormat>.Value
            If IndexNo = -1 Then 'Series named InvCdfVerBar does not exist
                Chart1.Series.Add("InvCdfAreaVertBar")
            Else
                Chart1.Series("InvCdfAreaVertBar").Points.Clear()
            End If
            Chart1.Series("InvCdfAreaVertBar").ChartType = DataVisualization.Charting.SeriesChartType.Column
            'Chart1.Series("InvCdfAreaVertBar").ChartArea = "CdfArea"
            Chart1.Series("InvCdfAreaVertBar").ChartArea = "InvCdfArea"
            Chart1.Series("InvCdfAreaVertBar").Color = Color.FromArgb(ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<InvCDF>.<Line>.<Color>.Value)
            Chart1.Series("InvCdfAreaVertBar").SetCustomProperty("PixelPointWidth", ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<InvCDF>.<Line>.<Thickness>.Value)
            If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<InvCDF>.<Circle>.<Show>.Value = True Then
                Chart1.Series("InvCdfAreaVertBar").MarkerStyle = DataVisualization.Charting.MarkerStyle.Circle
            Else
                Chart1.Series("InvCdfAreaVertBar").MarkerStyle = DataVisualization.Charting.MarkerStyle.None
            End If
            Chart1.Series("InvCdfAreaVertBar").MarkerSize = ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<InvCDF>.<Circle>.<Size>.Value
            Chart1.Series("InvCdfAreaVertBar").MarkerBorderWidth = ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<InvCDF>.<Circle>.<Thickness>.Value
            Chart1.Series("InvCdfAreaVertBar").MarkerBorderColor = Color.FromArgb(ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<InvCDF>.<Circle>.<Color>.Value)
            Chart1.Series("InvCdfAreaVertBar").MarkerColor = Color.Transparent
            Chart1.Series("InvCdfAreaVertBar").IsVisibleInLegend = False

            For Each item In PointAnnotInfo
                If item.<InvCDF>.Value = True And item.<Show>.Value = True Then 'Add this annotation to the InvCDF Area
                    'Add the vertical bar:
                    Dim InvCdfPoint As New DataVisualization.Charting.DataPoint
                    InvCdfPoint.XValue = item.<CdfProbability>.Value
                    InvCdfPoint.SetValueY(item.<RandVarValue>.Value)
                    Chart1.Series("InvCdfAreaVertBar").Points.Add(InvCdfPoint)
                    'Add the label:
                    Dim InvCdfAnnot As New DataVisualization.Charting.TextAnnotation
                    InvCdfAnnot.AxisX = Chart1.ChartAreas("InvCdfArea").AxisX
                    InvCdfAnnot.AxisY = Chart1.ChartAreas("InvCdfArea").AxisY
                    InvCdfAnnot.AnchorX = item.<CdfProbability>.Value
                    InvCdfAnnot.AnchorY = item.<RandVarValue>.Value
                    'InvCdfAnnot.AnchorAlignment = ContentAlignment.MiddleRight
                    InvCdfAnnot.AnchorAlignment = [Enum].Parse(GetType(ContentAlignment), item.<Alignment>.Value)
                    'InvCdfAnnot.Text = item.<Label>.Value & " (" & Format(Val(item.<RandVarValue>.Value), RVFormat) & ")"
                    If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<InvCDF>.<Text>.<ShowProbValue>.Value = True Then 'Show the Probability value
                        If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<InvCDF>.<Text>.<ShowRVValue>.Value = True Then 'Show the Random Variable value
                            InvCdfAnnot.Text = item.<Label>.Value & " (" & Format(Val(item.<CdfProbability>.Value), ProbFormat) & ", " & Format(Val(item.<RandVarValue>.Value), RVFormat) & ")"
                        Else 'Do not show the Random Variable value
                            InvCdfAnnot.Text = item.<Label>.Value & " (" & Format(Val(item.<CdfProbability>.Value), ProbFormat) & ")"
                        End If
                    Else 'Do not show the Probability value
                        If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<InvCDF>.<Text>.<ShowRVValue>.Value = True Then 'Show the Random Variable value
                            InvCdfAnnot.Text = item.<Label>.Value & " (" & Format(Val(item.<RandVarValue>.Value), RVFormat) & ")"
                        Else 'Do not show the Random Variable value
                            InvCdfAnnot.Text = item.<Label>.Value
                        End If
                    End If
                    'InvCdfAnnot.Font = New Font("Arial", 10, FontStyle.Regular Or FontStyle.Bold)
                    InvCdfAnnot.Font = InvCdfAnnotFont
                    InvCdfAnnot.ForeColor = InvCdfAnnotColor
                    Chart1.Annotations.Add(InvCdfAnnot)
                End If
            Next
            If Chart1.Series("InvCdfAreaVertBar").Points.Count = 1 Then
                If Chart1.Series("InvCdfAreaVertBar").Points(0).XValue = 0 Then Chart1.Series("InvCdfAreaVertBar").Points(0).XValue = 0.00000001 'If all the XValues are 0, the Chart control assumes thay are categories.
            End If
        End If

        Dim InvRevCdfArea As Integer = Chart1.ChartAreas.IndexOf("InvRevCdfArea")
        If InvRevCdfArea > -1 Then 'Annotation can be added to the InvRevCdf Area
            'Get InvRevCDF annotation font and color:
            myFontStyle = FontStyle.Regular
            If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<InvRevCDF>.<Text>.<Bold>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Bold
            If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<InvRevCDF>.<Text>.<Italic>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Italic
            If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<InvRevCDF>.<Text>.<Strikeout>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Strikeout
            If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<InvRevCDF>.<Text>.<Underline>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Underline
            myFontSize = ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<InvRevCDF>.<Text>.<Size>.Value
            myFontName = ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<InvRevCDF>.<Text>.<FontName>.Value
            Dim InvRevCdfAnnotFont As New Font(myFontName, myFontSize, myFontStyle)
            Dim InvRevCdfAnnotColor As Color = Color.FromArgb(ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<InvRevCDF>.<Text>.<Color>.Value)

            'Add a series used to plot vertical bars on the InvRevCDF chart:
            Dim IndexNo As Integer = Chart1.Series.IndexOf("InvRevCdfAreaVertBar")
            ProbFormat = ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<InvRevCDF>.<Text>.<ProbFormat>.Value
            RVFormat = ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<InvRevCDF>.<Text>.<RVFormat>.Value
            If IndexNo = -1 Then 'Series named InvRevCdfVerBar does not exist
                Chart1.Series.Add("InvRevCdfAreaVertBar")
            Else
                Chart1.Series("InvRevCdfAreaVertBar").Points.Clear()
            End If
            Chart1.Series("InvRevCdfAreaVertBar").ChartType = DataVisualization.Charting.SeriesChartType.Column
            'Chart1.Series("InvRevCdfAreaVertBar").ChartArea = "CdfArea"
            Chart1.Series("InvRevCdfAreaVertBar").ChartArea = "InvRevCdfArea"
            Chart1.Series("InvRevCdfAreaVertBar").Color = Color.FromArgb(ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<InvRevCDF>.<Line>.<Color>.Value)
            Chart1.Series("InvRevCdfAreaVertBar").SetCustomProperty("PixelPointWidth", ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<InvRevCDF>.<Line>.<Thickness>.Value)
            If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<InvRevCDF>.<Circle>.<Show>.Value = True Then
                Chart1.Series("InvRevCdfAreaVertBar").MarkerStyle = DataVisualization.Charting.MarkerStyle.Circle
            Else
                Chart1.Series("InvRevCdfAreaVertBar").MarkerStyle = DataVisualization.Charting.MarkerStyle.None
            End If
            Chart1.Series("InvRevCdfAreaVertBar").MarkerSize = ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<InvRevCDF>.<Circle>.<Size>.Value
            Chart1.Series("InvRevCdfAreaVertBar").MarkerBorderWidth = ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<InvRevCDF>.<Circle>.<Thickness>.Value
            Chart1.Series("InvRevCdfAreaVertBar").MarkerBorderColor = Color.FromArgb(ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<InvRevCDF>.<Circle>.<Color>.Value)
            Chart1.Series("InvRevCdfAreaVertBar").MarkerColor = Color.Transparent
            Chart1.Series("InvRevCdfAreaVertBar").IsVisibleInLegend = False

            For Each item In PointAnnotInfo
                If item.<InvRevCDF>.Value = True And item.<Show>.Value = True Then 'Add this annotation to the InvRevCDF Area
                    'Add the vertical bar:
                    Dim InvRevCdfPoint As New DataVisualization.Charting.DataPoint
                    InvRevCdfPoint.XValue = item.<RevCdfProbability>.Value
                    InvRevCdfPoint.SetValueY(item.<RandVarValue>.Value)
                    Chart1.Series("InvRevCdfAreaVertBar").Points.Add(InvRevCdfPoint)
                    'Add the label:
                    Dim InvRevCdfAnnot As New DataVisualization.Charting.TextAnnotation
                    InvRevCdfAnnot.AxisX = Chart1.ChartAreas("InvRevCdfArea").AxisX
                    InvRevCdfAnnot.AxisY = Chart1.ChartAreas("InvRevCdfArea").AxisY
                    InvRevCdfAnnot.AnchorX = item.<RevCdfProbability>.Value
                    InvRevCdfAnnot.AnchorY = item.<RandVarValue>.Value
                    InvRevCdfAnnot.AnchorAlignment = [Enum].Parse(GetType(ContentAlignment), item.<Alignment>.Value)
                    If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<InvRevCDF>.<Text>.<ShowProbValue>.Value = True Then 'Show the Probability value
                        If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<InvRevCDF>.<Text>.<ShowRVValue>.Value = True Then 'Show the Random Variable value
                            InvRevCdfAnnot.Text = item.<Label>.Value & " (" & Format(Val(item.<CdfProbability>.Value), ProbFormat) & ", " & Format(Val(item.<RandVarValue>.Value), RVFormat) & ")"
                        Else 'Do not show the Random Variable value
                            InvRevCdfAnnot.Text = item.<Label>.Value & " (" & Format(Val(item.<CdfProbability>.Value), ProbFormat) & ")"
                        End If
                    Else 'Do not show the Probability value
                        If ChartInfo.<ChartSettings>.<PointAnnotationSettings>.<InvRevCDF>.<Text>.<ShowRVValue>.Value = True Then 'Show the Random Variable value
                            InvRevCdfAnnot.Text = item.<Label>.Value & " (" & Format(Val(item.<RandVarValue>.Value), RVFormat) & ")"
                        Else 'Do not show the Random Variable value
                            InvRevCdfAnnot.Text = item.<Label>.Value
                        End If
                    End If
                    InvRevCdfAnnot.Font = InvRevCdfAnnotFont
                    InvRevCdfAnnot.ForeColor = InvRevCdfAnnotColor
                    Chart1.Annotations.Add(InvRevCdfAnnot)
                End If
            Next
            If Chart1.Series("InvRevCdfAreaVertBar").Points.Count = 1 Then
                If Chart1.Series("InvRevCdfAreaVertBar").Points(0).XValue = 0 Then Chart1.Series("InvRevCdfAreaVertBar").Points(0).XValue = 0.00000001 'If all the XValues are 0, the Chart control assumes thay are categories.
            End If
        End If
    End Sub

    'Private Sub ApplyAreaShading(ByRef AreaAnnotInfo As IEnumerable(Of XElement))
    'Private Sub ApplyAreaShading()
    Public Sub ApplyAreaShading()
        'Apply the area shading to the PDF chart.

        'AreaAnnotInfo = From item In ChartInfo.<ChartSettings>.<AreaAnnotationCollection>.<AreaAnnotation> 'Refresh AreaAnnotInfo 'UPDATE 26/6/22

        Dim PdfArea As Integer = Chart1.ChartAreas.IndexOf("PdfArea")

        If PdfArea > -1 Then 'Annotation can be added to the Pdf Area
            'Distribution sampling parameters:
            'Minimum: Main.Distribution.ContSampling.Minimum
            'Interval: Main.Distribution.ContSampling.Interval
            'Maximum: Main.Distribution.ContSampling.Maximum

            For Each item In AreaAnnotInfo
                Dim DistributionNo As Integer = item.<DistributionNo>.Value - 1 'DistributionNo uses zero-based index: index number conversion required.
                'Dim DistribAnnotNo As Integer = item.<DistribAnnotNo>.Value
                Dim DistribAnnotNo As Integer = AreaAnnotInfo.ToList.IndexOf(item)

                Dim ShadingSeriesName As String = "PdfShadingVertBar_" & DistributionNo & "_" & DistribAnnotNo
                Dim IndexNo As Integer = Chart1.Series.IndexOf(ShadingSeriesName)
                If IndexNo = -1 Then  'The Series does not exist
                Else
                    Chart1.Series(ShadingSeriesName).Points.Clear()
                    Chart1.Series(ShadingSeriesName).IsVisibleInLegend = False
                End If
                If item.<Show>.Value = True Then 'Show the shading
                    'Dim DistributionNo As Integer = item.<DistributionNo>.Value - 1
                    If IndexNo = -1 Then   'The Series does not exist
                        Chart1.Series.Add(ShadingSeriesName)
                        Chart1.Series(ShadingSeriesName).ChartType = DataVisualization.Charting.SeriesChartType.Column
                        Chart1.Series(ShadingSeriesName).MarkerStyle = DataVisualization.Charting.MarkerStyle.None
                        Chart1.Series(ShadingSeriesName).ChartArea = "PdfArea"
                        Chart1.Series(ShadingSeriesName).IsVisibleInLegend = False
                    Else
                        Chart1.Series(ShadingSeriesName).Points.Clear()
                        Chart1.Series(ShadingSeriesName).ChartType = DataVisualization.Charting.SeriesChartType.Column
                        Chart1.Series(ShadingSeriesName).MarkerStyle = DataVisualization.Charting.MarkerStyle.None
                        Chart1.Series(ShadingSeriesName).ChartArea = "PdfArea"
                        Chart1.Series(ShadingSeriesName).IsVisibleInLegend = False
                    End If
                    Dim ShadingStart As Double = Main.Distribution.ContSampling.Minimum
                    If item.<FromValue>.Value <> Nothing Then
                        If item.<FromValue>.Value > ShadingStart Then ShadingStart = item.<FromValue>.Value
                    End If
                    Dim ShadingInterval As Double = Main.Distribution.ContSampling.Interval / item.<Density>.Value
                    Dim ShadingEnd As Double = Main.Distribution.ContSampling.Maximum
                    If item.<ToValue>.Value <> Nothing Then
                        If item.<ToValue>.Value < ShadingEnd Then ShadingEnd = item.<ToValue>.Value
                    End If
                    Dim NShadingLines As Integer = Math.Round((ShadingEnd - ShadingStart) / ShadingInterval)
                    Dim I As Integer
                    Dim XPos As Double
                    'Dim YPos As Double
                    Chart1.Series(ShadingSeriesName).SetCustomProperty("PixelPointWidth", item.<Thickness>.Value)
                    Chart1.Series(ShadingSeriesName).Color = Color.FromArgb(item.<Color>.Value)

                    Select Case item.<Intensity>.Value
                        Case "10"
                            Chart1.Series(ShadingSeriesName).BackHatchStyle = DataVisualization.Charting.ChartHatchStyle.Percent90
                        Case "20"
                            Chart1.Series(ShadingSeriesName).BackHatchStyle = DataVisualization.Charting.ChartHatchStyle.Percent80
                        Case "25"
                            Chart1.Series(ShadingSeriesName).BackHatchStyle = DataVisualization.Charting.ChartHatchStyle.Percent75
                        Case "30"
                            Chart1.Series(ShadingSeriesName).BackHatchStyle = DataVisualization.Charting.ChartHatchStyle.Percent70
                        Case "40"
                            Chart1.Series(ShadingSeriesName).BackHatchStyle = DataVisualization.Charting.ChartHatchStyle.Percent60
                        Case "50"
                            Chart1.Series(ShadingSeriesName).BackHatchStyle = DataVisualization.Charting.ChartHatchStyle.Percent50
                        Case "60"
                            Chart1.Series(ShadingSeriesName).BackHatchStyle = DataVisualization.Charting.ChartHatchStyle.Percent40
                        Case "70"
                            Chart1.Series(ShadingSeriesName).BackHatchStyle = DataVisualization.Charting.ChartHatchStyle.Percent30
                        Case "75"
                            Chart1.Series(ShadingSeriesName).BackHatchStyle = DataVisualization.Charting.ChartHatchStyle.Percent25
                        Case "80"
                            Chart1.Series(ShadingSeriesName).BackHatchStyle = DataVisualization.Charting.ChartHatchStyle.Percent20
                        Case "90"
                            Chart1.Series(ShadingSeriesName).BackHatchStyle = DataVisualization.Charting.ChartHatchStyle.Percent10
                        Case "95"
                            Chart1.Series(ShadingSeriesName).BackHatchStyle = DataVisualization.Charting.ChartHatchStyle.Percent05
                        Case "100"
                            Chart1.Series(ShadingSeriesName).BackHatchStyle = DataVisualization.Charting.ChartHatchStyle.None
                        Case Else
                            Chart1.Series(ShadingSeriesName).BackHatchStyle = DataVisualization.Charting.ChartHatchStyle.None
                    End Select

                    'X value weighted average calculation - used to determine the optimal X position for the probability (area) label
                    Dim WtXTotal As Double = 0 'The sum of the X values weighted with the Y values
                    Dim WtTotal As Double = 0 'The sum of the Y weight values
                    Dim YValue As Double

                    For I = 0 To NShadingLines - 1
                        Dim PdfPoint As New DataVisualization.Charting.DataPoint
                        XPos = ShadingStart + I * ShadingInterval
                        PdfPoint.XValue = XPos
                        YValue = Main.Distribution.PdfValue(DistributionNo, XPos)
                        'PdfPoint.SetValueY(Main.Distribution.PdfValue(DistributionNo, XPos))
                        PdfPoint.SetValueY(YValue)
                        'myChart.Series("PdfShadingVertBar").Points.Add(PdfPoint)
                        Chart1.Series(ShadingSeriesName).Points.Add(PdfPoint)
                        WtXTotal += XPos * YValue
                        WtTotal += YValue
                    Next

                    If NShadingLines <= 1 Then 'No shading lines drawn and no WtXTotal or WtTotal values calculated
                        WtXTotal = ShadingStart
                        WtTotal = 1
                    End If

                    'Annotate the Probability (Area):
                    If ChartInfo.<ChartSettings>.<AreaAnnotationSettings>.<DisplayProbability>.Value = True Then
                        Dim NumberFormat As String = ChartInfo.<ChartSettings>.<AreaAnnotationSettings>.<Format>.Value
                        Dim YMin As Double = Chart1.ChartAreas("PdfArea").AxisY.Minimum
                        Dim YMax As Double = Chart1.ChartAreas("PdfArea").AxisY.Maximum
                        Dim YOffset As Double = (YMax - YMin) / 10 'The offset from YMin to display the Probability annotation
                        'Dim XAnnotPos As Double = (ShadingStart + ShadingEnd) / 2 'Annotate the Probability in the middle of the shaded area
                        Dim XAnnotPos As Double = WtXTotal / WtTotal
                        Dim PdfAnnot As New DataVisualization.Charting.TextAnnotation
                        PdfAnnot.AxisX = Chart1.ChartAreas("PdfArea").AxisX
                        PdfAnnot.AxisY = Chart1.ChartAreas("PdfArea").AxisY
                        PdfAnnot.AnchorX = XAnnotPos
                        PdfAnnot.AnchorY = YMin + YOffset
                        PdfAnnot.AnchorAlignment = ContentAlignment.MiddleCenter
                        PdfAnnot.Text = Format(Val(item.<Probability>.Value), NumberFormat)

                        Dim myFontStyle As FontStyle
                        Dim myFontSize As Single = ChartInfo.<ChartSettings>.<AreaAnnotationSettings>.<Size>.Value
                        myFontStyle = FontStyle.Regular
                        If ChartInfo.<ChartSettings>.<AreaAnnotationSettings>.<Bold>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Bold
                        If ChartInfo.<ChartSettings>.<AreaAnnotationSettings>.<Italic>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Italic
                        If ChartInfo.<ChartSettings>.<AreaAnnotationSettings>.<Strikeout>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Strikeout
                        If ChartInfo.<ChartSettings>.<AreaAnnotationSettings>.<Underline>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Underline
                        PdfAnnot.Font = New Font(ChartInfo.<ChartSettings>.<AreaAnnotationSettings>.<FontName>.Value, myFontSize, myFontStyle)
                        If ChartInfo.<ChartSettings>.<AreaAnnotationSettings>.<UseShadingColor>.Value = True Then
                            PdfAnnot.ForeColor = Color.FromArgb(item.<Color>.Value)
                        Else
                            PdfAnnot.ForeColor = Color.FromArgb(ChartInfo.<ChartSettings>.<AreaAnnotationSettings>.<DefaultColor>.Value)
                        End If
                        Chart1.Annotations.Add(PdfAnnot)
                        item.<ChartAnnotNo>.Value = Chart1.Annotations.Count - 1 'Save the annotation number - this will be needed if the annotation is updated.
                    End If
                End If
            Next
        Else
            'There is no PDF area to display the shading.
        End If
    End Sub

    Private Sub UpdateAreaAnnotation(DistribNo As Integer)
        'Update the Probability Area for each of the area annotations defiend for the selected distribution.
        'DistribNo is the one-based distribution number to process.

        Dim SelAreaAnnotInfo = From item In ChartInfo.<ChartSettings>.<AreaAnnotationCollection>.<AreaAnnotation> Where item.<DistributionNo>.Value = DistribNo
        Dim Parameter As Double
        Dim FromValue As Double
        Dim FromValueCDF As Double
        Dim ToValue As Double
        Dim ToValueCDF As Double

        For Each item In SelAreaAnnotInfo
            Select Case item.<FromValueType>.Value
                Case "Minimum"
                    FromValueCDF = 0
                    item.<FromValueCDF>.Value = 0
                    'FromValue = Main.Distribution.Info(DistribNo - 1).InvCDF(FromValueCDF)
                    'item.<FromValue>.Value = FromValue

                Case "Probability <="
                    Parameter = item.<FromValueParameter>.Value
                    FromValueCDF = Parameter
                    FromValue = Main.Distribution.Info(DistribNo - 1).InvCDF(FromValueCDF)
                    item.<FromValue>.Value = FromValue
                    item.<FromValueCDF>.Value = FromValueCDF

                Case "Probability >"
                    Parameter = item.<FromValueParameter>.Value
                    FromValue = Main.Distribution.Info(DistribNo - 1).InvRevCDF(Parameter)
                    FromValueCDF = Main.Distribution.Info(DistribNo - 1).CDF(FromValue)
                    item.<FromValue>.Value = FromValue
                    item.<FromValueCDF>.Value = FromValueCDF

                Case "Random Variable Value"
                    Parameter = item.<FromValueParameter>.Value
                    FromValue = Parameter
                    FromValueCDF = Main.Distribution.Info(DistribNo - 1).CDF(FromValue)
                    item.<FromValue>.Value = FromValue
                    item.<FromValueCDF>.Value = FromValueCDF

                Case "Mean"
                    FromValue = Main.Distribution.Info(DistribNo - 1).Mean
                    FromValueCDF = Main.Distribution.Info(DistribNo - 1).CDF(FromValue)
                    item.<FromValueCDF>.Value = FromValueCDF
                    item.<FromValue>.Value = FromValue

                Case "Median"
                    FromValue = Main.Distribution.Info(DistribNo - 1).Median
                    FromValueCDF = Main.Distribution.Info(DistribNo - 1).CDF(FromValue)
                    item.<FromValueCDF>.Value = FromValueCDF
                    item.<FromValue>.Value = FromValue

                Case "Mode"
                    FromValue = Main.Distribution.Info(DistribNo - 1).Mode
                    FromValueCDF = Main.Distribution.Info(DistribNo - 1).CDF(FromValue)
                    item.<FromValueCDF>.Value = FromValueCDF
                    item.<FromValue>.Value = FromValue

                Case "Standard Deviation"
                    Parameter = item.<FromValueParameter>.Value
                    Dim Mean As Double = Main.Distribution.Info(DistribNo - 1).Mean
                    Dim StdDev As Double = Main.Distribution.Info(DistribNo - 1).StdDev
                    FromValue = Mean + Parameter * StdDev
                    FromValueCDF = Main.Distribution.Info(DistribNo - 1).CDF(FromValue)
                    item.<FromValueCDF>.Value = FromValueCDF
                    item.<FromValue>.Value = FromValue

                Case "User Defined Value 1"
                    Parameter = item.<FromValueParameter>.Value
                    FromValue = Parameter
                    FromValueCDF = Main.Distribution.Info(DistribNo - 1).CDF(FromValue)
                    item.<FromValue>.Value = FromValue
                    item.<FromValueCDF>.Value = FromValueCDF

                Case "User Defined Value 2"
                    Parameter = item.<FromValueParameter>.Value
                    FromValue = Parameter
                    FromValueCDF = Main.Distribution.Info(DistribNo - 1).CDF(FromValue)
                    item.<FromValue>.Value = FromValue
                    item.<FromValueCDF>.Value = FromValueCDF

                Case Else
                    Main.Message.AddWarning("Unknown From Value Type: " & item.<FromValueType>.Value & vbCrLf)
            End Select

            Select Case item.<ToValueType>.Value
                Case "Maximum"
                    ToValueCDF = 1
                    item.<ToValueCDF>.Value = 1
                    item.<Probability>.Value = ToValueCDF - FromValueCDF

                Case "Probability <="
                    Parameter = item.<ToValueParameter>.Value
                    ToValueCDF = Parameter
                    ToValue = Main.Distribution.Info(DistribNo - 1).InvCDF(ToValueCDF)
                    item.<ToValue>.Value = ToValue
                    item.<ToValueCDF>.Value = ToValueCDF
                    item.<Probability>.Value = ToValueCDF - FromValueCDF

                Case "Probability >"
                    Parameter = item.<ToValueParameter>.Value
                    ToValue = Main.Distribution.Info(DistribNo - 1).InvRevCDF(Parameter)
                    ToValueCDF = Main.Distribution.Info(DistribNo - 1).CDF(ToValue)
                    item.<ToValue>.Value = ToValue
                    item.<ToValueCDF>.Value = ToValueCDF
                    item.<Probability>.Value = ToValueCDF - FromValueCDF

                Case "Random Variable Value"
                    Parameter = item.<ToValueParameter>.Value
                    ToValue = Parameter
                    ToValueCDF = Main.Distribution.Info(DistribNo - 1).CDF(ToValue)
                    item.<ToValue>.Value = ToValue
                    item.<ToValueCDF>.Value = ToValueCDF
                    item.<Probability>.Value = ToValueCDF - FromValueCDF

                Case "Mean"
                    ToValue = Main.Distribution.Info(DistribNo - 1).Mean
                    ToValueCDF = Main.Distribution.Info(DistribNo - 1).CDF(ToValue)
                    item.<ToValueCDF>.Value = ToValueCDF
                    item.<Toalue>.Value = ToValue
                    item.<Probability>.Value = ToValueCDF - FromValueCDF

                Case "Median"
                    ToValue = Main.Distribution.Info(DistribNo - 1).Median
                    ToValueCDF = Main.Distribution.Info(DistribNo - 1).CDF(ToValue)
                    item.<ToValueCDF>.Value = ToValueCDF
                    item.<ToValue>.Value = ToValue
                    item.<Probability>.Value = ToValueCDF - FromValueCDF

                Case "Mode"
                    ToValue = Main.Distribution.Info(DistribNo - 1).Mode
                    ToValueCDF = Main.Distribution.Info(DistribNo - 1).CDF(ToValue)
                    item.<ToalueCDF>.Value = ToValueCDF
                    item.<ToValue>.Value = ToValue
                    item.<Probability>.Value = ToValueCDF - FromValueCDF

                Case "Standard Deviation"
                    Parameter = item.<ToValueParameter>.Value
                    Dim Mean As Double = Main.Distribution.Info(DistribNo - 1).Mean
                    Dim StdDev As Double = Main.Distribution.Info(DistribNo - 1).StdDev
                    ToValue = Mean + Parameter * StdDev
                    ToValueCDF = Main.Distribution.Info(DistribNo - 1).CDF(ToValue)
                    item.<ToValueCDF>.Value = ToValueCDF
                    item.<ToValue>.Value = ToValue
                    item.<Probability>.Value = ToValueCDF - FromValueCDF

                Case "User Defined Value 1"
                    Parameter = item.<ToValueParameter>.Value
                    ToValue = Parameter
                    ToValueCDF = Main.Distribution.Info(DistribNo - 1).CDF(ToValue)
                    item.<ToValue>.Value = ToValue
                    item.<ToValueCDF>.Value = ToValueCDF
                    item.<Probability>.Value = ToValueCDF - FromValueCDF

                Case "User Defined Value 2"
                    Parameter = item.<ToValueParameter>.Value
                    ToValue = Parameter
                    ToValueCDF = Main.Distribution.Info(DistribNo - 1).CDF(ToValue)
                    item.<ToValue>.Value = ToValue
                    item.<ToValueCDF>.Value = ToValueCDF
                    item.<Probability>.Value = ToValueCDF - FromValueCDF

                Case Else
                    Main.Message.AddWarning("Unknown To Value Type: " & item.<ToValueType>.Value & vbCrLf)
            End Select
        Next
    End Sub

    'Public Sub UpdateAreaAnnotation()
    '    'Update the Area Shading annotation on the PDF chart.

    '    Dim PdfArea As Integer = Chart1.ChartAreas.IndexOf("PdfArea")

    '    Dim AreaAnnotInfo = From item In ChartXml.<ChartSettings>.<AreaAnnotationCollection>.<AreaAnnotation>

    '    If PdfArea > -1 Then 'Annotation can be added to the Pdf Area

    '        'Distribution sampling parameters:
    '        'Minimum: Main.Distribution.ContSampling.Minimum
    '        'Interval: Main.Distribution.ContSampling.Interval
    '        'Maximum: Main.Distribution.ContSampling.Maximum

    '        For Each item In AreaAnnotInfo
    '            Dim DistributionNo As Integer = item.<DistributionNo>.Value - 1 'DistributionNo uses zero-based index: index number conversion required.
    '            Dim DistribAnnotNo As Integer = item.<DistribAnnotNo>.Value
    '            Dim ShadingSeriesName As String = "PdfShadingVertBar_" & DistributionNo & "_" & DistribAnnotNo
    '            Dim IndexNo As Integer = Chart1.Series.IndexOf(ShadingSeriesName)
    '            If IndexNo = -1 Then  'The Series does not exist
    '            Else
    '                Chart1.Series(ShadingSeriesName).Points.Clear()
    '                Chart1.Series(ShadingSeriesName).IsVisibleInLegend = False
    '            End If
    '            If item.<Show>.Value = True Then 'Show the shading
    '                If IndexNo = -1 Then   'The Series does not exist
    '                    Chart1.Series.Add(ShadingSeriesName)
    '                    Chart1.Series(ShadingSeriesName).ChartType = DataVisualization.Charting.SeriesChartType.Column
    '                    Chart1.Series(ShadingSeriesName).MarkerStyle = DataVisualization.Charting.MarkerStyle.None
    '                    Chart1.Series(ShadingSeriesName).ChartArea = "PdfArea"
    '                    Chart1.Series(ShadingSeriesName).IsVisibleInLegend = False
    '                Else
    '                    Chart1.Series(ShadingSeriesName).Points.Clear()
    '                    Chart1.Series(ShadingSeriesName).ChartType = DataVisualization.Charting.SeriesChartType.Column
    '                    Chart1.Series(ShadingSeriesName).MarkerStyle = DataVisualization.Charting.MarkerStyle.None
    '                    Chart1.Series(ShadingSeriesName).IsVisibleInLegend = False
    '                End If
    '                Dim ShadingStart As Double = Main.Distribution.ContSampling.Minimum
    '                If item.<FromValue>.Value <> Nothing Then
    '                    If item.<FromValue>.Value > ShadingStart Then ShadingStart = item.<FromValue>.Value
    '                End If
    '                Dim ShadingInterval As Double = Main.Distribution.ContSampling.Interval / item.<Density>.Value
    '                Dim ShadingEnd As Double = Main.Distribution.ContSampling.Maximum
    '                If item.<ToValue>.Value <> Nothing Then
    '                    If item.<ToValue>.Value < ShadingEnd Then ShadingEnd = item.<ToValue>.Value
    '                End If
    '                Dim NShadingLines As Integer = Math.Round((ShadingEnd - ShadingStart) / ShadingInterval)
    '                Dim I As Integer
    '                Dim XPos As Double
    '                Chart1.Series(ShadingSeriesName).SetCustomProperty("PixelPointWidth", item.<Thickness>.Value)
    '                Chart1.Series(ShadingSeriesName).Color = Color.FromArgb(item.<Color>.Value)

    '                Select Case item.<Intensity>.Value
    '                    'Case "5"
    '                    '    myChart.Series(ShadingSeriesName).BackHatchStyle = DataVisualization.Charting.ChartHatchStyle.Percent05
    '                    Case "10"
    '                        Chart1.Series(ShadingSeriesName).BackHatchStyle = DataVisualization.Charting.ChartHatchStyle.Percent90
    '                    Case "20"
    '                        Chart1.Series(ShadingSeriesName).BackHatchStyle = DataVisualization.Charting.ChartHatchStyle.Percent80
    '                    Case "25"
    '                        Chart1.Series(ShadingSeriesName).BackHatchStyle = DataVisualization.Charting.ChartHatchStyle.Percent75
    '                    Case "30"
    '                        Chart1.Series(ShadingSeriesName).BackHatchStyle = DataVisualization.Charting.ChartHatchStyle.Percent70
    '                    Case "40"
    '                        Chart1.Series(ShadingSeriesName).BackHatchStyle = DataVisualization.Charting.ChartHatchStyle.Percent60
    '                    Case "50"
    '                        Chart1.Series(ShadingSeriesName).BackHatchStyle = DataVisualization.Charting.ChartHatchStyle.Percent50
    '                    Case "60"
    '                        Chart1.Series(ShadingSeriesName).BackHatchStyle = DataVisualization.Charting.ChartHatchStyle.Percent40
    '                    Case "70"
    '                        Chart1.Series(ShadingSeriesName).BackHatchStyle = DataVisualization.Charting.ChartHatchStyle.Percent30
    '                    Case "75"
    '                        Chart1.Series(ShadingSeriesName).BackHatchStyle = DataVisualization.Charting.ChartHatchStyle.Percent25
    '                    Case "80"
    '                        Chart1.Series(ShadingSeriesName).BackHatchStyle = DataVisualization.Charting.ChartHatchStyle.Percent20
    '                    Case "90"
    '                        Chart1.Series(ShadingSeriesName).BackHatchStyle = DataVisualization.Charting.ChartHatchStyle.Percent10
    '                    Case "95"
    '                        Chart1.Series(ShadingSeriesName).BackHatchStyle = DataVisualization.Charting.ChartHatchStyle.Percent05
    '                    Case "100"
    '                        Chart1.Series(ShadingSeriesName).BackHatchStyle = DataVisualization.Charting.ChartHatchStyle.None
    '                    Case Else
    '                        Chart1.Series(ShadingSeriesName).BackHatchStyle = DataVisualization.Charting.ChartHatchStyle.None
    '                End Select

    '                'X value weighted average calculation - used to determine the optimal X position for the probability (area) label
    '                Dim WtXTotal As Double = 0 'The sum of the X values weighted with the Y values
    '                Dim WtTotal As Double = 0 'The sum of the Y weight values
    '                Dim YValue As Double

    '                For I = 0 To NShadingLines - 1
    '                    Dim PdfPoint As New DataVisualization.Charting.DataPoint
    '                    XPos = ShadingStart + I * ShadingInterval
    '                    PdfPoint.XValue = XPos
    '                    YValue = Main.Distribution.PdfValue(DistributionNo, XPos)
    '                    'PdfPoint.SetValueY(Main.Distribution.PdfValue(DistributionNo, XPos))
    '                    PdfPoint.SetValueY(YValue)
    '                    Chart1.Series(ShadingSeriesName).Points.Add(PdfPoint)
    '                    WtXTotal += XPos * YValue
    '                    WtTotal += YValue
    '                Next

    '                If NShadingLines <= 1 Then 'No shading lines drawn and no WtXTotal or WtTotal values calculated
    '                    WtXTotal = ShadingStart
    '                    WtTotal = 1
    '                End If

    '                'Annotate the Probability (Area):
    '                If ChartXml.<ChartSettings>.<AreaAnnotationSettings>.<DisplayProbability>.Value = True Then
    '                    Dim NumberFormat As String = ChartXml.<ChartSettings>.<AreaAnnotationSettings>.<Format>.Value
    '                    Dim YMin As Double = Chart1.ChartAreas("PdfArea").AxisY.Minimum
    '                    Dim YMax As Double = Chart1.ChartAreas("PdfArea").AxisY.Maximum
    '                    Dim YOffset As Double = (YMax - YMin) / 10 'The offset from YMin to display the Probability annotation
    '                    'Dim XAnnotPos As Double = (ShadingStart + ShadingEnd) / 2 'Annotate the Probability in the middle of the shaded area
    '                    Dim XAnnotPos As Double = WtXTotal / WtTotal
    '                    Dim PdfAnnot As New DataVisualization.Charting.TextAnnotation
    '                    PdfAnnot.AxisX = Chart1.ChartAreas("PdfArea").AxisX
    '                    PdfAnnot.AxisY = Chart1.ChartAreas("PdfArea").AxisY
    '                    PdfAnnot.AnchorX = XAnnotPos
    '                    PdfAnnot.AnchorY = YMin + YOffset
    '                    PdfAnnot.AnchorAlignment = ContentAlignment.MiddleCenter
    '                    PdfAnnot.Text = Format(Val(item.<Probability>.Value), NumberFormat)

    '                    Dim myFontStyle As FontStyle
    '                    Dim myFontSize As Single = ChartXml.<ChartSettings>.<AreaAnnotationSettings>.<Size>.Value
    '                    myFontStyle = FontStyle.Regular
    '                    If ChartXml.<ChartSettings>.<AreaAnnotationSettings>.<Bold>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Bold
    '                    If ChartXml.<ChartSettings>.<AreaAnnotationSettings>.<Italic>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Italic
    '                    If ChartXml.<ChartSettings>.<AreaAnnotationSettings>.<Strikeout>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Strikeout
    '                    If ChartXml.<ChartSettings>.<AreaAnnotationSettings>.<Underline>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Underline
    '                    PdfAnnot.Font = New Font(ChartXml.<ChartSettings>.<AreaAnnotationSettings>.<FontName>.Value, myFontSize, myFontStyle)
    '                    If ChartXml.<ChartSettings>.<AreaAnnotationSettings>.<UseShadingColor>.Value = True Then
    '                        PdfAnnot.ForeColor = Color.FromArgb(item.<Color>.Value)
    '                    Else
    '                        PdfAnnot.ForeColor = Color.FromArgb(ChartXml.<ChartSettings>.<AreaAnnotationSettings>.<DefaultColor>.Value)
    '                    End If
    '                    Chart1.Annotations.Add(PdfAnnot)
    '                    item.<ChartAnnotNo>.Value = Chart1.Annotations.Count - 1 'Save the annotation number - this will be needed if the annotation is updated.
    '                End If
    '            End If
    '        Next
    '    Else
    '        'There is no PDF area to display the shading.
    '    End If

    'End Sub

    'Private Sub ApplyAreaShading_Old(ByRef AreaAnnotInfo As IEnumerable(Of XElement))
    '    'Apply the arera shading to the PDF chart.

    '    Dim PdfArea As Integer = Chart1.ChartAreas.IndexOf("PdfArea")

    '    If PdfArea > -1 Then 'Annotation can be added to the Pdf Area
    '        'Add a series used to plot vertical shading bars on the PDF chart:
    '        Dim IndexNo As Integer = Chart1.Series.IndexOf("PdfShadingVertBar")
    '        If IndexNo = -1 Then 'Series named PdfShadingVertBar does not exist
    '            Chart1.Series.Add("PdfShadingVertBar")
    '            Chart1.Series("PdfShadingVertBar").ChartType = DataVisualization.Charting.SeriesChartType.Column
    '            Chart1.Series("PdfShadingVertBar").Color = Color.Red
    '            Chart1.Series("PdfShadingVertBar").ChartArea = "PdfArea"
    '            Chart1.Series("PdfShadingVertBar").SetCustomProperty("PixelPointWidth", "2")
    '            Chart1.Series("PdfShadingVertBar").IsVisibleInLegend = False
    '        Else
    '            Chart1.Series("PdfShadingVertBar").Points.Clear()
    '            Chart1.Series("PdfShadingVertBar").IsVisibleInLegend = False
    '        End If

    '        'Distribution sampling parameters:
    '        'Minimum: Main.Distribution.ContSampling.Minimum
    '        'Interval: Main.Distribution.ContSampling.Interval
    '        'Maximum: Main.Distribution.ContSampling.Maximum

    '        For Each item In AreaAnnotInfo
    '            If item.<Show>.Value = True Then 'Show the shading
    '                Dim DistributionNo As Integer = item.<DistributionNo>.Value - 1
    '                Dim ShadingStart As Double = Main.Distribution.ContSampling.Minimum
    '                If item.<FromValue>.Value <> Nothing Then
    '                    If item.<FromValue>.Value > ShadingStart Then ShadingStart = item.<FromValue>.Value
    '                End If
    '                Dim ShadingInterval As Double = Main.Distribution.ContSampling.Interval / item.<Density>.Value
    '                Dim ShadingEnd As Double = Main.Distribution.ContSampling.Maximum
    '                If item.<ToValue>.Value <> Nothing Then
    '                    If item.<ToValue>.Value < ShadingEnd Then ShadingEnd = item.<ToValue>.Value
    '                End If
    '                'Dim NShadingLines As Integer = Int((ShadingEnd - ShadingStart) / ShadingInterval)
    '                Dim NShadingLines As Integer = Math.Round((ShadingEnd - ShadingStart) / ShadingInterval)
    '                Dim I As Integer
    '                Dim XPos As Double
    '                Dim YPos As Double
    '                Chart1.Series("PdfShadingVertBar").SetCustomProperty("PixelPointWidth", item.<Thickness>.Value)
    '                Chart1.Series("PdfShadingVertBar").Color = Color.FromArgb(item.<Color>.Value)

    '                For I = 0 To NShadingLines
    '                    Dim PdfPoint As New DataVisualization.Charting.DataPoint
    '                    XPos = ShadingStart + I * ShadingInterval
    '                    PdfPoint.XValue = XPos
    '                    PdfPoint.SetValueY(Main.Distribution.PdfValue(DistributionNo, XPos))
    '                    Chart1.Series("PdfShadingVertBar").Points.Add(PdfPoint)
    '                Next

    '                'Annotate the Probability (Area):
    '                If ChartInfo.<ChartSettings>.<AreaAnnotationSettings>.<DisplayProbability>.Value = True Then
    '                    Dim NumberFormat As String = ChartInfo.<ChartSettings>.<AreaAnnotationSettings>.<Format>.Value
    '                    Dim YMin As Double = Chart1.ChartAreas("PdfArea").AxisY.Minimum
    '                    Dim YMax As Double = Chart1.ChartAreas("PdfArea").AxisY.Maximum
    '                    Dim YOffset As Double = (YMax - YMin) / 10 'The offset from YMin to display the Probability annotation
    '                    'Dim XAnnotPos As Double = (ShadingEnd - ShadingStart) / 2 'Annotate the Probability in the middle of the shaded area
    '                    Dim XAnnotPos As Double = (ShadingStart + ShadingEnd) / 2 'Annotate the Probability in the middle of the shaded area
    '                    Dim PdfAnnot As New DataVisualization.Charting.TextAnnotation
    '                    PdfAnnot.AxisX = Chart1.ChartAreas("PdfArea").AxisX
    '                    PdfAnnot.AxisY = Chart1.ChartAreas("PdfArea").AxisY
    '                    PdfAnnot.AnchorX = XAnnotPos
    '                    PdfAnnot.AnchorY = YMin + YOffset
    '                    PdfAnnot.AnchorAlignment = ContentAlignment.MiddleCenter
    '                    PdfAnnot.Text = Format(Val(item.<Probability>.Value), NumberFormat)

    '                    Dim myFontStyle As FontStyle
    '                    Dim myFontSize As Single = ChartInfo.<ChartSettings>.<AreaAnnotationSettings>.<Size>.Value
    '                    myFontStyle = FontStyle.Regular
    '                    If ChartInfo.<ChartSettings>.<AreaAnnotationSettings>.<Bold>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Bold
    '                    If ChartInfo.<ChartSettings>.<AreaAnnotationSettings>.<Italic>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Italic
    '                    If ChartInfo.<ChartSettings>.<AreaAnnotationSettings>.<Strikeout>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Strikeout
    '                    If ChartInfo.<ChartSettings>.<AreaAnnotationSettings>.<Underline>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Underline
    '                    'PdfAnnot.Font = New Font("Arial", 10, FontStyle.Regular Or FontStyle.Bold)
    '                    PdfAnnot.Font = New Font(ChartInfo.<ChartSettings>.<AreaAnnotationSettings>.<FontName>.Value, myFontSize, myFontStyle)

    '                    'PdfAnnot.Font = New Font("Arial", 10, FontStyle.Regular Or FontStyle.Bold)
    '                    If ChartInfo.<ChartSettings>.<AreaAnnotationSettings>.<UseShadingColor>.Value = True Then
    '                        PdfAnnot.ForeColor = Color.FromArgb(item.<Color>.Value)
    '                    Else
    '                        PdfAnnot.ForeColor = Color.FromArgb(ChartInfo.<ChartSettings>.<AreaAnnotationSettings>.<DefaultColor>.Value)
    '                    End If
    '                    Chart1.Annotations.Add(PdfAnnot)
    '                    item.<ChartAnnotNo>.Value = Chart1.Annotations.Count - 1 'Save the annotation number - this will be needed if the annotation is updated.
    '                End If
    '            End If

    '        Next
    '    Else
    '        'There is no PDF area to display the shading.
    '    End If

    'End Sub




    Private Sub cmbChartList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbChartList.SelectedIndexChanged
        If cmbChartList.SelectedIndex = -1 Then

        Else
            Dim SelChartName As String = cmbChartList.SelectedItem.ToString

            If ChartName = SelChartName Then
                'The Chart Name has not changed.
            Else
                ChartName = SelChartName




                If IsNothing(ChartSettings) Then
                    'The chart settings were not being edited.
                Else
                    If ChartSettings.Modified Then
                        Dim dr As System.Windows.Forms.DialogResult
                        dr = MessageBox.Show("Save the current chart modifications?", "Notice", MessageBoxButtons.YesNo)
                        If dr = System.Windows.Forms.DialogResult.Yes Then
                            ChartSettings.SaveChart()
                        End If
                    End If
                    'ChartSettings.OpenChart(ChartName)
                    ChartSettings.ChartName = ChartName
                End If
                Plot() 'Plot the chart
            End If
        End If
    End Sub

    Private Sub frmChart_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If IsNothing(ChartSettings) Then

        Else
            ChartSettings.txtHeight.Text = Me.Height
            ChartSettings.txtWidth.Text = Me.Width
            CheckFormPos()
        End If
        If IsNothing(ChartInfo) Then
        Else
            ChartInfo.<ChartSettings>.<FormHeight>.Value = Me.Height
            ChartInfo.<ChartSettings>.<FormWidth>.Value = Me.Width
        End If
    End Sub

    Private Sub frmChart_Move(sender As Object, e As EventArgs) Handles Me.Move
        If IsNothing(ChartSettings) Then
            'If IsNothing(ChartInfo) Then
            'Else
            '    ChartInfo.<ChartSettings>.<FormTop>.Value = Me.Top
            '    ChartInfo.<ChartSettings>.<FormLeft>.Value = Me.Left
            'End If
        Else
            ChartSettings.txtTop.Text = Me.Top
            ChartSettings.txtLeft.Text = Me.Left
            CheckFormPos()
        End If
        If IsNothing(ChartInfo) Then
        Else
            ChartInfo.<ChartSettings>.<FormTop>.Value = Me.Top
            ChartInfo.<ChartSettings>.<FormLeft>.Value = Me.Left
        End If
    End Sub

    Public Sub UpdateChartList()
        'Update the Chart List
        Try
            cmbChartList.Items.Clear()

            For Each item In _dataSource.ChartList
                cmbChartList.Items.Add(item.Key)
            Next

            If _dataSource.ChartName <> "" Then
                cmbChartList.SelectedIndex = cmbChartList.FindStringExact(_dataSource.ChartName)
            End If
        Catch ex As Exception
            Main.Message.AddWarning("Chart-UpdateChartList" & ex.Message & vbCrLf)
        End Try
    End Sub

    'Private Sub btnUpdate_Click(sender As Object, e As EventArgs)
    '    UpdateChartList()
    'End Sub

    Public Sub Save()
        'Save any changes made to the chart settings.
        If IsNothing(ChartSettings) Then
            'The chart is not being edited.
        Else
            If ChartSettings.Modified Then ChartSettings.SaveChart()
        End If
    End Sub

    Public Sub SelectChart(ChartName As String)
        'Select the chart named ChartName

        If DataSource.ChartList.ContainsKey(ChartName) Then
            ChartInfo = DataSource.ChartList(ChartName)
            'txtSelChartName.Text = ChartName
            'txtSelChartDescr.Text = ChartInfo.<ChartSettings>.<Description>.Value
            AreaInfo = From item In ChartInfo.<ChartSettings>.<ChartAreasCollection>.<ChartArea>
            SeriesInfo = From item In ChartInfo.<ChartSettings>.<SeriesCollection>.<Series>
            TitleInfo = From item In ChartInfo.<ChartSettings>.<TitlesCollection>.<Title>
            PointAnnotInfo = From item In ChartInfo.<ChartSettings>.<PointAnnotationCollection>.<PointAnnotation>
            AreaAnnotInfo = From item In ChartInfo.<ChartSettings>.<AreaAnnotationCollection>.<AreaAnnotation>

            'UpdateAreaOptions()
            'UpdateTitlesTabSettings()
            'UpdateAreasTabSettings()
            'UpdateSeriesTabSettings()

            'LoadChartInfo() 'Renamed PlotChart - This is now applied separately

            'XmlHtmDisplay1.Rtf = XmlHtmDisplay1.XmlToRtf(frmParent.ChartInfo.ToString, True)
        Else
            Main.Message.AddWarning("Chart not found in the list: " & ChartName & vbCrLf)
        End If


    End Sub

    'Public Sub RefreshSeriesInfo()
    '    SeriesInfo = From item In ChartInfo.<ChartSettings>.<SeriesCollection>.<Series>
    'End Sub


#End Region 'Form Methods ---------------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Form Events - Events that can be triggered by this form." '==========================================================================================================================

#End Region 'Form Events ----------------------------------------------------------------------------------------------------------------------------------------------------------------------


End Class