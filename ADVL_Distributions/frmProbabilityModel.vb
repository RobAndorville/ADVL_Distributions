Public Class frmProbabilityModel
    'Simple probability model design.

#Region " Variable Declarations - All the variables used in this form and this application." '=================================================================================================

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

    Private _modelFileName As String = "" 'The file name of the Probability model.
    Property ModelFileName As String
        Get
            Return _modelFileName
        End Get
        Set(value As String)
            _modelFileName = value
            txtFileName.Text = _modelFileName
        End Set
    End Property

    Private _modelName As String 'The Probability model name.
    Property ModelName As String
        Get
            Return _modelName
        End Get
        Set(value As String)
            _modelName = value
            txtModelName.Text = _modelName
        End Set
    End Property

    Private _modelLabel As String 'The Probability model label.
    Property ModelLabel As String
        Get
            Return _modelLabel
        End Get
        Set(value As String)
            _modelLabel = value
            txtLabel.Text = _modelLabel
        End Set
    End Property

    Private _modelDescription As String 'The Probability model description.
    Property ModelDescription As String
        Get
            Return _modelDescription
        End Get
        Set(value As String)
            _modelDescription = value
            txtDescription.Text = _modelDescription
        End Set
    End Property

    Private _itemCount As Integer 'The number of items in the Probability Model.
    Property ItemCount As Integer
        Get
            Return _itemCount
        End Get
        Set(value As Integer)
            _itemCount = value
            txtItemCount.Text = _itemCount
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

        dgvItems.ColumnCount = 5
        dgvItems.Columns(0).HeaderText = "Name"
        dgvItems.Columns(0).Width = 160
        dgvItems.Columns(1).HeaderText = "Label"
        dgvItems.Columns(1).Width = 160
        dgvItems.Columns(2).HeaderText = "Count"
        dgvItems.Columns(2).Width = 80
        dgvItems.Columns(3).HeaderText = "Probability"
        dgvItems.Columns(3).Width = 80
        dgvItems.Columns(3).ReadOnly = True
        dgvItems.Columns(4).HeaderText = "Description"
        dgvItems.Columns(4).Width = 320
        dgvItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

        RestoreFormSettings()   'Restore the form settings
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

    Private Sub frmProbabilityModel_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        If FormNo > -1 Then
            Main.ProbabilityModelFormClosed()
        End If
    End Sub



#End Region 'Form Display Methods -------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Open and Close Forms - Code used to open and close other forms." '===================================================================================================================

#End Region 'Open and Close Forms -------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Form Methods - The main actions performed by this form." '===========================================================================================================================

    Private Sub btnNewProbModel_Click(sender As Object, e As EventArgs) Handles btnNewProbModel.Click
        'Create a new Probability Model.

        'Get the new model File Name, Model Name and Description:
        Dim EntryForm As New ADVL_Utilities_Library_1.frmNewDataNameModal
        EntryForm.EntryName = "NewProbModel"
        EntryForm.Title = "New Probability Model"
        EntryForm.FileExtension = "ProbModel"
        EntryForm.GetFileName = True
        EntryForm.GetDataName = True
        EntryForm.GetDataLabel = True
        EntryForm.GetDataDescription = True
        EntryForm.SettingsLocn = Main.Project.SettingsLocn
        EntryForm.DataLocn = Main.Project.DataLocn
        EntryForm.ApplicationName = Main.ApplicationInfo.Name
        EntryForm.RestoreFormSettings()

        If EntryForm.ShowDialog() = DialogResult.OK Then
            If txtFileName.Text.Trim = "" Then
                'There is no model to save.
            Else
                'If Distribution.Modified Then
                If Modified Then
                    Dim Result As DialogResult = MessageBox.Show("Do you want to save the changes in the current Probability model?", "Warning", MessageBoxButtons.YesNoCancel)
                    If Result = DialogResult.Yes Then
                        SaveProbModel()
                    ElseIf Result = DialogResult.Cancel Then
                        Exit Sub
                    Else
                        'Contunue without saving the current model.
                        Modified = False
                    End If
                Else

                End If
            End If

            dgvItems.Rows.Clear()
            txtNotes.Text = ""
            'txtFileName.Text = EntryForm.FileName
            'txtModelName.Text = EntryForm.DataName
            'txtLabel.Text = EntryForm.DataLabel
            'txtDescription.Text = EntryForm.DataDescription
            ModelFileName = EntryForm.FileName
            ModelName = EntryForm.DataName
            ModelLabel = EntryForm.DataLabel
            ModelDescription = EntryForm.DataDescription

            'Distribution.Clear() 'Clear the current Probability model.

            'SelTableName = ""
            'dgvDataTable.DataSource = vbNull

            'Distribution.FileName = EntryForm.FileName
            'Distribution.ModelName = EntryForm.DataName
            'Distribution.Label = EntryForm.DataLabel
            'Distribution.Description = EntryForm.DataDescription

            'NDistribs = Distribution.Info.Count
            'If NDistribs > 0 Then SelDistrib = 1 Else SelDistrib = 0

            'UpdateForm()
            'UpdateSuffix()
        End If
    End Sub

    Private Sub SaveProbModel()
        'Save the Probability Model.

        dgvItems.AllowUserToAddRows = False
        Dim XDoc = <?xml version="1.0" encoding="utf-8"?>
                   <!---->
                   <!--Probability Model-->
                   <ProbabilityModel>
                       <Name><%= ModelName %></Name>
                       <Label><%= ModelLabel %></Label>
                       <Description><%= ModelDescription %></Description>
                       <ItemCount><%= ItemCount %></ItemCount>
                       <ItemTypeList>
                           <%= From Item In dgvItems.Rows
                               Select
                           <Item>
                               <Name><%= Item.Cells(0).Value %></Name>
                               <Label><%= Item.Cells(1).Value %></Label>
                               <Count><%= Item.Cells(2).Value %></Count>
                               <Probability><%= Item.Cells(3).Value %></Probability>
                               <Description><%= Item.Cells(4).Value %></Description>
                           </Item>
                           %>
                       </ItemTypeList>
                   </ProbabilityModel>
        dgvItems.AllowUserToAddRows = True

        Main.Project.SaveXmlData(ModelFileName, XDoc)

    End Sub

    Private Sub btnAddItem_Click(sender As Object, e As EventArgs) Handles btnAddItem.Click
        'Add a new probability item
        dgvItems.AllowUserToAddRows = False
        Dim RowCount As Integer = dgvItems.RowCount
        AddProbItem("Item_" & RowCount, "Item_" & RowCount, 1, 0, "")
        dgvItems.AllowUserToAddRows = True
        UpdateProbabilities()
    End Sub

    Private Sub AddProbItem(Name As String, Label As String, Count As Integer, Probability As Double, Description As String)
        'Add a Probability item to the list.
        dgvItems.Rows.Add(Name, Label, Count, Probability, Description)
        UpdateProbabilities()
    End Sub

    Private Sub UpdateProbabilities()
        'Update the Probability values in the Item list.
        dgvItems.AllowUserToAddRows = False
        Dim Count As Integer = 0
        For Each Item In dgvItems.Rows
            Count += Item.Cells(2).Value
        Next
        ItemCount = Count
        For Each Item In dgvItems.Rows
            Item.Cells(3).Value = Item.Cells(2).Value / Count
        Next
        dgvItems.AllowUserToAddRows = True
        dgvItems.AutoResizeColumns()
    End Sub

    Private Sub btnOpenProbModel_Click(sender As Object, e As EventArgs) Handles btnOpenProbModel.Click
        'Open a Probability Model.

        Dim FileName As String = Main.Project.SelectDataFile("Probability Model files", "ProbModel")

        If FileName = "" Then
            'No file has been selected.
        Else
            OpenProbModel(FileName)
        End If
    End Sub

    Private Sub OpenProbModel(FileName As String)
        'Open the Probability Model with thye name FileName.

        Dim XDoc As System.Xml.Linq.XDocument
        Main.Project.ReadXmlData(FileName, XDoc)

        dgvItems.Rows.Clear()
        txtNotes.Text = ""
        ModelFileName = FileName
        ModelName = XDoc.<ProbabilityModel>.<Name>.Value
        ModelLabel = XDoc.<ProbabilityModel>.<Label>.Value
        ModelDescription = XDoc.<ProbabilityModel>.<Description>.Value
        ItemCount = XDoc.<ProbabilityModel>.<ItemCount>.Value

        Dim ItemTypeList = From Item In XDoc.<ProbabilityModel>.<ItemTypeList>.<Item>

        For Each Item In ItemTypeList
            dgvItems.Rows.Add(Item.<Name>.Value, Item.<Label>.Value, Item.<Count>.Value, Item.<Probability>.Value, Item.<Description>.Value)
        Next

    End Sub

    Private Sub btnSaveProbModel_Click(sender As Object, e As EventArgs) Handles btnSaveProbModel.Click
        SaveProbModel()
    End Sub

    'Private Sub ClearProbModel()
    '    'Clear the Probability Model

    'End Sub

#End Region 'Form Methods ---------------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Form Events - Events that can be triggered by this form." '==========================================================================================================================

#End Region 'Form Events ----------------------------------------------------------------------------------------------------------------------------------------------------------------------


End Class