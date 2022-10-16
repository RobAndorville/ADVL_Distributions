'==============================================================================================================================================================================================
'
'Copyright 2016 Signalworks Pty Ltd, ABN 26 066 681 598

'Licensed under the Apache License, Version 2.0 (the "License");
'you may not use this file except in compliance with the License.
'You may obtain a copy of the License at
'
'http://www.apache.org/licenses/LICENSE-2.0
'
'Unless required by applicable law or agreed to in writing, software
'distributed under the License is distributed on an "AS IS" BASIS,
''WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
'See the License for the specific language governing permissions and
'limitations under the License.
'
'----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Imports System.ComponentModel
Imports System.Security.Permissions
<PermissionSet(SecurityAction.Demand, Name:="FullTrust")>
<System.Runtime.InteropServices.ComVisibleAttribute(True)> 'Note: There should be no blank lines between this line and the line: Public Class Main
Public Class Main
    'The ADVL_Distributions application is used to design probability distributions.
    '
    'An Andorville™ application has the following features:
    '   Each application is relatively simple and has a single purpose.
    '   Networked applications - Any application can exchange information and instructions with any Andorville™ application connected to the Message Service. 
    '   Project based - Data is stored in directory or file based projects.
    '   Open source code-base - A basic set of Andorville™ applications and libraries are licenced under the Apache License, Version 2.0.


#Region " Coding Notes - Notes on the code used in this class." '==============================================================================================================================

    'ADD THE SYSTEM UTILITIES REFERENCE: ==========================================================================================
    'The following references are required by this software: 
    'ADVL_Utilities_Library_1.dll
    'To add the reference, press Project \ Add Reference... 
    '  Select the Browse option then press the Browse button
    '  Find the ADVL_Utilities_Library_1.dll file (it should be located in the directory ...\Projects\ADVL_Utilities_Library_1\ADVL_Utilities_Library_1\bin\Debug\)
    '  Press the Add button. Press the OK button.
    'The Utilities Library is used for Project Management, Archive file management, running XSequence files and running XMessage files.
    'If there are problems with a reference, try deleting it from the references list and adding it again.

    'Add a reference to System.IO.Compression:
    '  Project \ Add Refernce... \ Assemblies \ System.IO.Compression

    'ADD THE SERVICE REFERENCE: ===================================================================================================
    'A service reference to the Message Service must be added to the source code before this service can be used.
    'This is used to connect to the Application Network.

    'Adding the service reference to a project that includes the Message Service project: -----------------------------------------
    'Project \ Add Service Reference
    'Press the Discover button.
    'Expand the items in the Services window and select IMsgService.
    'Press OK.
    '------------------------------------------------------------------------------------------------------------------------------
    '------------------------------------------------------------------------------------------------------------------------------
    'Adding the service reference to other projects that dont include the Message Service project: -------------------------------
    'Run the ADVL_Network_1 application to start the message service.
    'In Microsoft Visual Studio select: Project \ Add Service Reference
    'Enter the address: http://localhost:8734/ADVLService
    'Press the Go button.
    'MsgService is found.
    'Press OK to add ServiceReference1 to the project.
    '------------------------------------------------------------------------------------------------------------------------------
    '
    'ADD THE MsgServiceCallback CODE: =============================================================================================
    'This is used to connect to the Application Network.
    'In Microsoft Visual Studio select: Project \ Add Class
    'MsgServiceCallback.vb
    'Add the following code to the class:
    'Imports System.ServiceModel
    'Public Class MsgServiceCallback
    '    Implements ServiceReference1.IMsgServiceCallback
    '    Public Sub OnSendMessage(message As String) Implements ServiceReference1.IMsgServiceCallback.OnSendMessage
    '        'A message has been received.
    '        'Set the InstrReceived property value to the message (usually in XMessage format). This will also apply the instructions in the XMessage.
    '        Main.InstrReceived = message
    '    End Sub
    'End Class
    '------------------------------------------------------------------------------------------------------------------------------
    '
    'DEBUGGING TIPS:
    '1. If an application based on the Application Template does not initially run correctly,
    '    check that the copied methods, such as Main_Load, have the correct Handles statement.
    '    For example: the Main_Load method should have the following declaration: Private Sub Main_Load(sender As Object, e As EventArgs) Handles Me.Load
    '      It will not run when the application loads, with this declaration:      Private Sub Main_Load(sender As Object, e As EventArgs)
    '    For example: the Main_FormClosing method should have the following declaration: Private Sub Main_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
    '      It will not run when the application closes, with this declaration:     Private Sub Main_FormClosing(sender As Object, e As FormClosingEventArgs)
    '------------------------------------------------------------------------------------------------------------------------------
    '
    'ADD THE Timer1 Control to the Main Form: =====================================================================================
    'Select the Main.vb [Design] tab.
    'Press Toolbox \ Components \ Timer and add Timer1 to the Main form.
    '------------------------------------------------------------------------------------------------------------------------------
    '
    'EDIT THE DefaultAppProperties() CODE: ========================================================================================
    'This sets the Application properties that are stored in the Application_Info_ADVL_2.xml settings file.
    'The following properties need to be updated:
    '  ApplicationInfo.Name
    '  ApplicationInfo.Description
    '  ApplicationInfo.CreationDate
    '  ApplicationInfo.Author
    '  ApplicationInfo.Copyright
    '  ApplicationInfo.Trademarks
    '  ApplicationInfo.License
    '  ApplicationInfo.SourceCode          (Optional - Preliminary implemetation coded.)
    '  ApplicationInfo.ModificationSummary (Optional - Preliminary implemetation coded.)
    '  ApplicationInfo.Libraries           (Optional - Preliminary implemetation coded.)
    '------------------------------------------------------------------------------------------------------------------------------
    '
    'ADD THE Application Icon: ====================================================================================================
    'Double-click My Project in the Solution Explorer window to open the project tab.
    'In the Application section press the Icon box and select Browse.
    'Select an application icon.
    'This icon can also be selected for the Main form icon by editing the properties of this form.
    '------------------------------------------------------------------------------------------------------------------------------
    '
    'EDIT THE Application Info Text: ==============================================================================================
    'The Application Info Text is used to label the application icon in the Application Network tree view.
    'This is edited in the SendApplicationInfo() method of the Main form.
    'Edit the line of code: Dim text As New XElement("Text", "Application Template").
    'Replace the default text "Application Template" with the required text.
    'Note that this text can be updated at any time and when the updated executable is run, it will update the Application Network tree view the next time it is connected.
    '------------------------------------------------------------------------------------------------------------------------------
    '
    'Calling JavaScript from VB.NET:
    'The following Imports statement and permissions are required for the Main form:
    'Imports System.Security.Permissions
    '<PermissionSet(SecurityAction.Demand, Name:="FullTrust")> _
    '<System.Runtime.InteropServices.ComVisibleAttribute(True)> _
    'NOTE: the line continuation characters (_) will disappear form the code view after they have been typed!
    '------------------------------------------------------------------------------------------------------------------------------
    'Calling VB.NET from JavaScript
    'Add the following line to the Main.Load method:
    '  Me.WebBrowser1.ObjectForScripting = Me
    '------------------------------------------------------------------------------------------------------------------------------
    '
    'Adding a Context Menu Strip:
    'In Visual Studio select the tab Main.vb [Design]
    'Select Toolbox \ Menus & Toolbars \ ContextMenuStrip and add it to the form. ContextMenuStrip1 appears in the panel below the form.
    'Right-click ContextMenuStrip1 and select Edit Items...
    'Press Add to add a new menu item
    '  Add item: Name: ToolStripMenuItem1_EditWorkflowTabPage         Text: Edit Workflow Tab Page (Edit the name and text on the right half of the Items Collection Editor.)
    '  Add item: Name: ToolStripMenuItem1_ShowStartPageInWorkflowTab  Text: Show Start Page In Workflow Tab
    'Select the Workflows button on the main form and select ContectMenuStrip property = ContextMenuStrip1
    '------------------------------------------------------------------------------------------------------------------------------
    '
    'Edit the AppInfoHtmlString function to display the appropriate information about the application.
    '------------------------------------------------------------------------------------------------------------------------------
    '
    'The ADVL_Network_1 application should be running the first time the new application is run.
    'The Network application will automatically send its executable file location to the new application.
    'This will allow the new application to start the Network when required.
    '------------------------------------------------------------------------------------------------------------------------------
    '
    'Other code edits:
    '  Main.Load - Message.AddText("------------------- Starting Application: ADVL Application Template ----------------- " & vbCrLf, "Heading")
    '  Private Sub SendApplicationInfo() - Dim text As New XElement("Text", "Application Template")
    '------------------------------------------------------------------------------------------------------------------------------
    '
    'To use MathNet.Numerics:
    '  Install the .nupkg file in a Visual Studio project:
    '    Right-click References in the Solution Explorer window.
    '      Select Manage NuGet Packages...
    '  In the NuGet tab, select Browse and search for MathNet
    '  Select MathNet.Numerics and press the Install button.
    '------------------------------------------------------------------------------------------------------------------------------
    '
    'To use the XmlHtmDisplay:
    'Add the ADVL_Utilities_Library_1 project to the solution:
    'File \ Add \ Existing Project
    '\source\repos\ADVL_Utilities_Library_1\ADVL_Utilities_Library_1\ADVL_Utilities_Library_1.vbproj
    'The Toolbox now includes the XmlHtmDisplay
    'With the ADVL_Utilities_Library_1 project added to the solution, the old Reference should be removed from the References list.
    '------------------------------------------------------------------------------------------------------------------------------


#End Region 'Coding Notes ---------------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Variable Declarations - All the variables and class objects used in this form and this application." '===============================================================================

    Public WithEvents ApplicationInfo As New ADVL_Utilities_Library_1.ApplicationInfo 'This object is used to store application information.
    Public WithEvents Project As New ADVL_Utilities_Library_1.Project 'This object is used to store Project information.
    Public WithEvents Message As New ADVL_Utilities_Library_1.Message 'This object is used to display messages in the Messages window.
    Public WithEvents ApplicationUsage As New ADVL_Utilities_Library_1.Usage 'This object stores application usage information.

    'Declare Forms used by the application:
    Public WithEvents WebPageList As frmWebPageList
    Public WithEvents ProjectArchive As frmArchive 'Form used to view the files in a Project archive
    Public WithEvents SettingsArchive As frmArchive 'Form used to view the files in a Settings archive
    Public WithEvents DataArchive As frmArchive 'Form used to view the files in a Data archive
    Public WithEvents SystemArchive As frmArchive 'Form used to view the files in a System archive

    Public WithEvents NewHtmlDisplay As frmHtmlDisplay
    Public HtmlDisplayFormList As New ArrayList 'Used for displaying multiple HtmlDisplay forms.

    Public WithEvents NewWebPage As frmWebPage
    Public WebPageFormList As New ArrayList 'Used for displaying multiple WebView forms.

    Public WithEvents Distributions As frmDistributions 'The form used to generate statistical distributions.

    Public WithEvents Chart As frmChart
    Public ChartList As New ArrayList 'Used for displaying multiple Chart forms.

    Public WithEvents Adjust As frmAdjust 'Used to adjust the distribution parameters.

    Public WithEvents MultipleDistribs As frmMultipleDistributions 'Used to define multiple distribution parameters.

    Public WithEvents Calculations As frmCalculations 'Calculate values from the distribution.

    'Public WithEvents SampleSingleValue As frmSampleSingleValue
    'Public SampleSingleValueList As New ArrayList 'Used for displaying multiple SampleSingleValue forms.

    Public WithEvents GenerateSamples As frmGenerateSamples
    Public GenerateSamplesList As New ArrayList 'Used for displaying multiple GenerateSamples forms.

    Public WithEvents Table As frmTable
    Public TableList As New ArrayList 'Used for displaying multiple Table forms

    Public WithEvents SeriesAnalysis As frmSeriesAnalysis
    Public SeriesAnalysisList As New ArrayList 'Used for displaying multiple SeriesAnalysis forms

    Public WithEvents MostLikelyParams As frmMostLikelyParams
    Public MostLikelyParamsList As New ArrayList  'Used for displaying multiple MostLikelyParams forms

    Public WithEvents ProbabilityModel As frmProbabilityModel
    Public ProbabilityModelList As New ArrayList  'Used for displaying multiple ProbabilityModel forms

    Public WithEvents DistribAnalysis As frmDistribAnalysis
    Public DistribAnalysisList As New ArrayList 'Used for displaying multiple DistribAnalysis forms

    'Declare objects used to connect to the Message Service:
    Public client As ServiceReference1.MsgServiceClient
    Public WithEvents XMsg As New ADVL_Utilities_Library_1.XMessage
    Dim XDoc As New System.Xml.XmlDocument
    Public Status As New System.Collections.Specialized.StringCollection
    Dim ClientProNetName As String = "" 'The name of the client Project Network requesting service. 
    Dim ClientAppName As String = "" 'The name of the client requesting service
    Dim ClientConnName As String = "" 'The name of the client connection requesting service
    Dim MessageXDoc As System.Xml.Linq.XDocument
    Dim xmessage As XElement 'This will contain the message. It will be added to MessageXDoc.
    Dim xlocns As New List(Of XElement) 'A list of locations. Each location forms part of the reply message. The information in the reply message will be sent to the specified location in the client application.
    Dim MessageText As String = "" 'The text of a message sent through the Application Network.

    Public OnCompletionInstruction As String = "Stop" 'The last instruction returned on completion of the processing of an XMessage.
    Public EndInstruction As String = "Stop" 'Another method of specifying the last instruction. This is processed in the EndOfSequence section of XMsg.Instructions.

    Public ConnectionName As String = "" 'The name of the connection used to connect this application to the ComNet (Message Service).

    Public ProNetName As String = "" 'The name of the Project Network
    Public ProNetPath As String = "" 'The path of the Project Network

    Public AdvlNetworkAppPath As String = "" 'The application path of the ADVL Network application (ComNet). This is where the "Application.Lock" file will be while ComNet is running
    Public AdvlNetworkExePath As String = "" 'The executable path of the ADVL Network.

    'Variable for local processing of an XMessage:
    Public WithEvents XMsgLocal As New ADVL_Utilities_Library_1.XMessage
    Dim XDocLocal As New System.Xml.XmlDocument
    Public StatusLocal As New System.Collections.Specialized.StringCollection

    'Main.Load variables:
    Dim ProjectSelected As Boolean = False 'If True, a project has been selected using Command Arguments. Used in Main.Load.
    Dim StartupConnectionName As String = "" 'If not "" the application will be connected to the ComNet using this connection name in  Main.Load.

    'The following variables are used to run JavaScript in Web Pages loaded into the Document View: -------------------
    Public WithEvents XSeq As New ADVL_Utilities_Library_1.XSequence
    'To run an XSequence:
    '  XSeq.RunXSequence(xDoc, Status) 'ImportStatus in Import
    '    Handle events:
    '      XSeq.ErrorMsg
    '      XSeq.Instruction(Info, Locn)

    Private XStatus As New System.Collections.Specialized.StringCollection

    'Variables used to restore Item values on a web page.
    Private FormName As String
    Private ItemName As String
    Private SelectId As String

    'StartProject variables:
    Private StartProject_AppName As String  'The application name
    Private StartProject_ConnName As String 'The connection name
    Private StartProject_ProjID As String   'The project ID
    Private StartProject_ProjName As String ' The project name

    Private WithEvents bgwComCheck As New System.ComponentModel.BackgroundWorker 'Used to perform communication checks on a separate thread.

    Public WithEvents bgwSendMessage As New System.ComponentModel.BackgroundWorker 'Used to send a message through the Message Service.
    Dim SendMessageParams As New clsSendMessageParams 'This holds the Send Message parameters: .ProjectNetworkName, .ConnectionName & .Message

    'Alternative SendMessage background worker - needed to send a message while instructions are being processed.
    Public WithEvents bgwSendMessageAlt As New System.ComponentModel.BackgroundWorker 'Used to send a message through the Message Service - alternative backgound worker.
    Dim SendMessageParamsAlt As New clsSendMessageParams 'This hold the Send Message parameters: .ProjectNetworkName, .ConnectionName & .Message - for the alternative background worker.

    Public WithEvents bgwRunInstruction As New System.ComponentModel.BackgroundWorker 'Used to run a single instruction
    Dim InstructionParams As New clsInstructionParams 'This holds the Info and Locn parameters of an instruction.

    Public WithEvents Distribution As New DistributionModel 'This class holds the Distribution parameters, data and charts.

    'Color calculation variables:
    Dim DecRed As Integer 'Red: 0 to 255
    Dim DecGreen As Integer 'Green: 0 to 255
    Dim DecBlue As Integer 'Blue: 0 to 255

    Dim SngHue As Single 'Hue: 0.0 to 360.0
    Dim SngLum As Single 'Luminance: 0.0 to 1.0
    Dim SngSat As Single 'Satunration: 0.0 to 1.0

    Public WithEvents bgwCancelConn As New System.ComponentModel.BackgroundWorker 'Used to cancel the Client.Connect while it is trying to connect.
    Dim myLock As New Object 'Lock object used with SyncLock in bgwCancelConn.
    Private ConnCancelled As Boolean = False 'True if the connection attempt has been cancelled.

#End Region 'Variable Declarations ------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Properties - All the properties used in this form and this application" '============================================================================================================

    Private _connectionHashcode As Integer 'The Message Service connection hashcode. This is used to identify a connection in the Message Service when reconnecting.
    Property ConnectionHashcode As Integer
        Get
            Return _connectionHashcode
        End Get
        Set(value As Integer)
            _connectionHashcode = value
        End Set
    End Property

    Private _connectedToComNet As Boolean = False  'True if the application is connected to the Communication Network (Message Service).
    Property ConnectedToComNet As Boolean
        Get
            Return _connectedToComNet
        End Get
        Set(value As Boolean)
            _connectedToComNet = value
        End Set
    End Property

    Private _instrReceived As String = "" 'Contains Instructions received via the Message Service.
    Property InstrReceived As String
        Get
            Return _instrReceived
        End Get
        Set(value As String)
            If value = Nothing Then
                Message.Add("Empty message received!")
            Else
                _instrReceived = value
                ProcessInstructions(_instrReceived)
            End If
        End Set
    End Property

    Private Sub ProcessInstructions(ByVal Instructions As String)
        'Process the XMessage instructions.

        Dim MsgType As String
        If Instructions.StartsWith("<XMsg>") Then
            MsgType = "XMsg"
            If ShowXMessages Then
                'Add the message header to the XMessages window:
                Message.XAddText("Message received: " & vbCrLf, "XmlReceivedNotice")
            End If
        ElseIf Instructions.StartsWith("<XSys>") Then
            MsgType = "XSys"
            If ShowSysMessages Then
                'Add the message header to the XMessages window:
                Message.XAddText("System Message received: " & vbCrLf, "XmlReceivedNotice")
            End If
        Else
            MsgType = "Unknown"
        End If

        If MsgType = "XMsg" Or MsgType = "XSys" Then 'This is an XMessage or XSystem set of instructions.
            Try
                'Inititalise the reply message:
                ClientProNetName = ""
                ClientConnName = ""
                ClientAppName = ""
                xlocns.Clear() 'Clear the list of locations in the reply message. 
                Dim Decl As New XDeclaration("1.0", "utf-8", "yes")
                MessageXDoc = New XDocument(Decl, Nothing) 'Reply message - this will be sent to the Client App.
                xmessage = New XElement(MsgType)
                xlocns.Add(New XElement("Main")) 'Initially set the location in the Client App to Main.

                'Run the received message:
                Dim XmlHeader As String = "<?xml version=""1.0"" encoding=""utf-8"" standalone=""yes""?>"
                XDoc.LoadXml(XmlHeader & vbCrLf & Instructions.Replace("&", "&amp;")) 'Replace "&" with "&amp:" before loading the XML text.

                If (MsgType = "XMsg") And ShowXMessages Then
                    Message.XAddXml(XDoc)  'Add the message to the XMessages window.
                    Message.XAddText(vbCrLf, "Normal") 'Add extra line
                ElseIf (MsgType = "XSys") And ShowSysMessages Then
                    Message.XAddXml(XDoc)  'Add the message to the XMessages window.
                    Message.XAddText(vbCrLf, "Normal") 'Add extra line
                End If

                XMsg.Run(XDoc, Status)
            Catch ex As Exception
                Message.Add("Error running XMsg: " & ex.Message & vbCrLf)
            End Try

            'XMessage has been run.
            'Reply to this message:
            'Add the message reply to the XMessages window:
            'Complete the MessageXDoc:
            xmessage.Add(xlocns(xlocns.Count - 1)) 'Add the last location reply instructions to the message.
            MessageXDoc.Add(xmessage)
            MessageText = MessageXDoc.ToString

            If ClientConnName = "" Then
                'No client to send a message to - process the message locally.

                If (MsgType = "XMsg") And ShowXMessages Then
                    Message.XAddText("Message processed locally:" & vbCrLf, "XmlSentNotice")
                    Message.XAddXml(MessageText)
                    Message.XAddText(vbCrLf, "Normal") 'Add extra line
                ElseIf (MsgType = "XSys") And ShowSysMessages Then
                    Message.XAddText("System Message processed locally:" & vbCrLf, "XmlSentNotice")
                    Message.XAddXml(MessageText)
                    Message.XAddText(vbCrLf, "Normal") 'Add extra line
                End If
                ProcessLocalInstructions(MessageText)
            Else
                If (MsgType = "XMsg") And ShowXMessages Then
                    Message.XAddText("Message sent to [" & ClientProNetName & "]." & ClientConnName & ":" & vbCrLf, "XmlSentNotice")   'NOTE: There is no SendMessage code in the Message Service application!
                    Message.XAddXml(MessageText)
                    Message.XAddText(vbCrLf, "Normal") 'Add extra line
                ElseIf (MsgType = "XSys") And ShowSysMessages Then
                    Message.XAddText("System Message sent to [" & ClientProNetName & "]." & ClientConnName & ":" & vbCrLf, "XmlSentNotice")   'NOTE: There is no SendMessage code in the Message Service application!
                    Message.XAddXml(MessageText)
                    Message.XAddText(vbCrLf, "Normal") 'Add extra line
                End If

                'Send Message on a new thread:
                SendMessageParams.ProjectNetworkName = ClientProNetName
                SendMessageParams.ConnectionName = ClientConnName
                SendMessageParams.Message = MessageText
                If bgwSendMessage.IsBusy Then
                    Message.AddWarning("Send Message backgroundworker is busy." & vbCrLf)
                Else
                    bgwSendMessage.RunWorkerAsync(SendMessageParams)
                End If
            End If

        Else 'This is not an XMessage!
            If Instructions.StartsWith("<XMsgBlk>") Then 'This is an XMessageBlock.
                'Process the received message:
                Dim XmlHeader As String = "<?xml version=""1.0"" encoding=""utf-8"" standalone=""yes""?>"
                XDoc.LoadXml(XmlHeader & vbCrLf & Instructions.Replace("&", "&amp;")) 'Replace "&" with "&amp:" before loading the XML text.
                If ShowXMessages Then
                    Message.XAddXml(XDoc)   'Add the message to the XMessages window.
                    Message.XAddText(vbCrLf, "Normal") 'Add extra line
                End If

                'Process the XMessageBlock:
                Dim XMsgBlkLocn As String
                XMsgBlkLocn = XDoc.GetElementsByTagName("ClientLocn")(0).InnerText
                Select Case XMsgBlkLocn
                    Case "TestLocn" 'Replace this with the required location name.
                        Dim XInfo As Xml.XmlNodeList = XDoc.GetElementsByTagName("XInfo") 'Get the XInfo node list
                        Dim InfoXDoc As New Xml.Linq.XDocument 'Create an XDocument to hold the information contained in XInfo 
                        InfoXDoc = XDocument.Parse("<?xml version=""1.0"" encoding=""utf-8"" standalone=""yes""?>" & vbCrLf & XInfo(0).InnerXml) 'Read the information into InfoXDoc
                        'Add processing instructions here 
                        ' The information in the InfoXDoc is usually sent to an XDocument in the application or stored as an XML file in the project.

                    Case Else
                        Message.AddWarning("Unknown XInfo Message location: " & XMsgBlkLocn & vbCrLf)
                End Select
            Else
                Message.XAddText("The message is not an XMessage or XMessageBlock: " & vbCrLf & Instructions & vbCrLf & vbCrLf, "Normal")
            End If
        End If
    End Sub

    Private Sub ProcessLocalInstructions(ByVal Instructions As String)
        'Process the XMessage instructions locally.

        If Instructions.StartsWith("<XMsg>") Or Instructions.StartsWith("<XSys>") Then 'This is an XMessage set of instructions.
            'Run the received message:
            Dim XmlHeader As String = "<?xml version=""1.0"" encoding=""utf-8"" standalone=""yes""?>"
            XDocLocal.LoadXml(XmlHeader & vbCrLf & Instructions)
            XMsgLocal.Run(XDocLocal, StatusLocal)
        Else 'This is not an XMessage!
            Message.XAddText("The message is not an XMessage: " & Instructions & vbCrLf, "Normal")
        End If
    End Sub

    Private _showXMessages As Boolean = True 'If True, XMessages that are sent or received will be shown in the Messages window.
    Property ShowXMessages As Boolean
        Get
            Return _showXMessages
        End Get
        Set(value As Boolean)
            _showXMessages = value
        End Set
    End Property

    Private _showSysMessages As Boolean = True 'If True, System messages that are sent or received will be shown in the messages window.
    Property ShowSysMessages As Boolean
        Get
            Return _showSysMessages
        End Get
        Set(value As Boolean)
            _showSysMessages = value
        End Set
    End Property

    Private _closedFormNo As Integer 'Temporarily holds the number of the form that is being closed. 
    Property ClosedFormNo As Integer
        Get
            Return _closedFormNo
        End Get
        Set(value As Integer)
            _closedFormNo = value
        End Set
    End Property

    Private _workflowFileName As String = "" 'The file name of the html document displayed in the Workflow tab.
    Public Property WorkflowFileName As String
        Get
            Return _workflowFileName
        End Get
        Set(value As String)
            _workflowFileName = value
        End Set
    End Property

    Private _selDistrib As Integer = 0 'The selected distribution number. (0 if none selected.)
    Property SelDistrib As Integer
        Get
            Return _selDistrib
        End Get
        Set(value As Integer)
            _selDistrib = value
            txtSelDistrib.Text = _selDistrib
            If _selDistrib = 0 Then
                'ClearForm()
            Else
                UpdateForm()
            End If
        End Set
    End Property

    Private _nDistribs As Integer = 0 'The number of distributions in the model.

    Property NDistribs As Integer
        Get
            Return _nDistribs
        End Get
        Set(value As Integer)
            _nDistribs = value
            txtNDistribs.Text = _nDistribs
        End Set
    End Property

    Private _nContinuous As Integer = 0 'The number of continuous distributions in the model.
    Property NContinuous As Integer
        Get
            Return _nContinuous
        End Get
        Set(value As Integer)
            _nContinuous = value
        End Set
    End Property

    Private _nDiscrete As Integer = 0 'The number of discrete distributions in the model.
    Property NDiscrete As Integer
        Get
            Return _nDiscrete
        End Get
        Set(value As Integer)
            _nDiscrete = value
        End Set
    End Property

    Private _selTableName As String = "" 'The selected table name.
    Property SelTableName As String
        Get
            Return _selTableName
        End Get
        Set(value As String)
            _selTableName = value
            UpdateDataTableDisplay()
        End Set
    End Property

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

#End Region 'Properties -----------------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Process XML Files - Read and write XML files." '=====================================================================================================================================

    Private Sub SaveFormSettings()
        'Save the form settings in an XML document.
        Dim settingsData = <?xml version="1.0" encoding="utf-8"?>
                           <!---->
                           <!--Form settings for Main form.-->
                           <FormSettings>
                               <Left><%= Me.Left %></Left>
                               <Top><%= Me.Top %></Top>
                               <Width><%= Me.Width %></Width>
                               <Height><%= Me.Height %></Height>
                               <AdvlNetworkAppPath><%= AdvlNetworkAppPath %></AdvlNetworkAppPath>
                               <AdvlNetworkExePath><%= AdvlNetworkExePath %></AdvlNetworkExePath>
                               <ShowXMessages><%= ShowXMessages %></ShowXMessages>
                               <ShowSysMessages><%= ShowSysMessages %></ShowSysMessages>
                               <WorkFlowFileName><%= WorkflowFileName %></WorkFlowFileName>
                               <!---->
                               <SelectedTabIndex><%= TabControl1.SelectedIndex %></SelectedTabIndex>
                           </FormSettings>

        'Add code to include other settings to save after the comment line <!---->

        Dim SettingsFileName As String = "FormSettings_" & ApplicationInfo.Name & " - Main.xml"
        Project.SaveXmlSettings(SettingsFileName, settingsData)
    End Sub

    Private Sub RestoreFormSettings()
        'Read the form settings from an XML document.

        Dim SettingsFileName As String = "FormSettings_" & ApplicationInfo.Name & " - Main.xml"

        If Project.SettingsFileExists(SettingsFileName) Then
            Dim Settings As System.Xml.Linq.XDocument
            Project.ReadXmlSettings(SettingsFileName, Settings)

            If IsNothing(Settings) Then 'There is no Settings XML data.
                Exit Sub
            End If

            'Restore form position and size:
            If Settings.<FormSettings>.<Left>.Value <> Nothing Then Me.Left = Settings.<FormSettings>.<Left>.Value
            If Settings.<FormSettings>.<Top>.Value <> Nothing Then Me.Top = Settings.<FormSettings>.<Top>.Value
            If Settings.<FormSettings>.<Height>.Value <> Nothing Then Me.Height = Settings.<FormSettings>.<Height>.Value
            If Settings.<FormSettings>.<Width>.Value <> Nothing Then Me.Width = Settings.<FormSettings>.<Width>.Value

            If Settings.<FormSettings>.<AdvlNetworkAppPath>.Value <> Nothing Then AdvlNetworkAppPath = Settings.<FormSettings>.<AdvlNetworkAppPath>.Value
            If Settings.<FormSettings>.<AdvlNetworkExePath>.Value <> Nothing Then AdvlNetworkExePath = Settings.<FormSettings>.<AdvlNetworkExePath>.Value

            If Settings.<FormSettings>.<ShowXMessages>.Value <> Nothing Then ShowXMessages = Settings.<FormSettings>.<ShowXMessages>.Value
            If Settings.<FormSettings>.<ShowSysMessages>.Value <> Nothing Then ShowSysMessages = Settings.<FormSettings>.<ShowSysMessages>.Value

            If Settings.<FormSettings>.<WorkFlowFileName>.Value <> Nothing Then WorkflowFileName = Settings.<FormSettings>.<WorkFlowFileName>.Value

            'Add code to read other saved setting here:
            If Settings.<FormSettings>.<SelectedTabIndex>.Value <> Nothing Then TabControl1.SelectedIndex = Settings.<FormSettings>.<SelectedTabIndex>.Value

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

    Private Sub ReadApplicationInfo()
        'Read the Application Information.

        If ApplicationInfo.FileExists Then
            ApplicationInfo.ReadFile()
        Else
            'There is no Application_Info_ADVL_2.xml file.
            DefaultAppProperties() 'Create a new Application Info file with default application properties.
            ApplicationInfo.WriteFile() 'Write the file now. The file information may be used by other applications.
        End If
    End Sub

    Private Sub DefaultAppProperties()
        'These properties will be saved in the Application_Info.xml file in the application directory.
        'If this file is deleted, it will be re-created using these default application properties.

        'Change this to show your application Name, Description and Creation Date.
        ApplicationInfo.Name = "ADVL_Distributions"

        'ApplicationInfo.ApplicationDir is set when the application is started.
        ApplicationInfo.ExecutablePath = Application.ExecutablePath

        ApplicationInfo.Description = "The Distributions application is used to design probability distributions."
        ApplicationInfo.CreationDate = "1-Nov-2021 12:00:00"

        'Author -----------------------------------------------------------------------------------------------------------
        'Change this to show your Name, Description and Contact information.
        ApplicationInfo.Author.Name = "Signalworks Pty Ltd"
        ApplicationInfo.Author.Description = "Signalworks Pty Ltd" & vbCrLf &
            "Australian Proprietary Company" & vbCrLf &
            "ABN 26 066 681 598" & vbCrLf &
            "Registration Date 05/10/1994"

        ApplicationInfo.Author.Contact = "http://www.andorville.com.au/"

        'File Associations: -----------------------------------------------------------------------------------------------
        'Add any file associations here.
        'The file extension and a description of files that can be opened by this application are specified.
        'The example below specifies a coordinate system parameter file type with the file extension .ADVLCoord.
        'Dim Assn1 As New ADVL_System_Utilities.FileAssociation
        'Assn1.Extension = "ADVLCoord"
        'Assn1.Description = "Andorville™ software coordinate system parameter file"
        'ApplicationInfo.FileAssociations.Add(Assn1)

        'Version ----------------------------------------------------------------------------------------------------------
        ApplicationInfo.Version.Major = My.Application.Info.Version.Major
        ApplicationInfo.Version.Minor = My.Application.Info.Version.Minor
        ApplicationInfo.Version.Build = My.Application.Info.Version.Build
        ApplicationInfo.Version.Revision = My.Application.Info.Version.Revision

        'Copyright --------------------------------------------------------------------------------------------------------
        'Add your copyright information here.
        ApplicationInfo.Copyright.OwnerName = "Signalworks Pty Ltd, ABN 26 066 681 598"
        ApplicationInfo.Copyright.PublicationYear = "2021"

        'Trademarks -------------------------------------------------------------------------------------------------------
        'Add your trademark information here.
        Dim Trademark1 As New ADVL_Utilities_Library_1.Trademark
        Trademark1.OwnerName = "Signalworks Pty Ltd, ABN 26 066 681 598"
        Trademark1.Text = "Andorville"
        Trademark1.Registered = False
        Trademark1.GenericTerm = "software"
        ApplicationInfo.Trademarks.Add(Trademark1)
        Dim Trademark2 As New ADVL_Utilities_Library_1.Trademark
        Trademark2.OwnerName = "Signalworks Pty Ltd, ABN 26 066 681 598"
        Trademark2.Text = "AL-H7"
        Trademark2.Registered = False
        Trademark2.GenericTerm = "software"
        ApplicationInfo.Trademarks.Add(Trademark2)
        Dim Trademark3 As New ADVL_Utilities_Library_1.Trademark
        Trademark3.OwnerName = "Signalworks Pty Ltd, ABN 26 066 681 598"
        Trademark3.Text = "AL-M7"
        Trademark3.Registered = False
        Trademark3.GenericTerm = "software"
        ApplicationInfo.Trademarks.Add(Trademark3)
        Dim Trademark4 As New ADVL_Utilities_Library_1.Trademark
        Trademark4.OwnerName = "Signalworks Pty Ltd, ABN 26 066 681 598"
        Trademark4.Text = "AL-S7"
        Trademark4.Registered = False
        Trademark4.GenericTerm = "software"
        ApplicationInfo.Trademarks.Add(Trademark4)
        Dim Trademark5 As New ADVL_Utilities_Library_1.Trademark
        Trademark5.OwnerName = "Signalworks Pty Ltd, ABN 26 066 681 598"
        Trademark5.Text = "AL-Q7"
        Trademark5.Registered = False
        Trademark5.GenericTerm = "software"
        ApplicationInfo.Trademarks.Add(Trademark5)

        'License -------------------------------------------------------------------------------------------------------
        'Add your license information here.
        ApplicationInfo.License.CopyrightOwnerName = "Signalworks Pty Ltd, ABN 26 066 681 598"
        ApplicationInfo.License.PublicationYear = "2021"

        'License Links:
        'http://choosealicense.com/
        'http://www.apache.org/licenses/
        'http://opensource.org/

        'Apache License 2.0 ---------------------------------------------
        ApplicationInfo.License.Code = ADVL_Utilities_Library_1.License.Codes.Apache_License_2_0
        ApplicationInfo.License.Notice = ApplicationInfo.License.ApacheLicenseNotice 'Get the pre-defined Aapche license notice.
        ApplicationInfo.License.Text = ApplicationInfo.License.ApacheLicenseText     'Get the pre-defined Apache license text.

        'Code to use other pre-defined license types is shown below:

        'GNU General Public License, version 3 --------------------------
        'ApplicationInfo.License.Type = ADVL_Utilities_Library_1.License.Types.GNU_GPL_V3_0
        'ApplicationInfo.License.Notice = 'Add the License Notice to ADVL_Utilities_Library_1 License class.
        'ApplicationInfo.License.Text = 'Add the License Text to ADVL_Utilities_Library_1 License class.

        'The MIT License ------------------------------------------------
        'ApplicationInfo.License.Type = ADVL_Utilities_Library_1.License.Types.MIT_License
        'ApplicationInfo.License.Notice = ApplicationInfo.License.MITLicenseNotice
        'ApplicationInfo.License.Text = ApplicationInfo.License.MITLicenseText

        'No License Specified -------------------------------------------
        'ApplicationInfo.License.Type = ADVL_Utilities_Library_1.License.Types.None
        'ApplicationInfo.License.Notice = ""
        'ApplicationInfo.License.Text = ""

        'The Unlicense --------------------------------------------------
        'ApplicationInfo.License.Type = ADVL_Utilities_Library_1.License.Types.The_Unlicense
        'ApplicationInfo.License.Notice = ApplicationInfo.License.UnLicenseNotice
        'ApplicationInfo.License.Text = ApplicationInfo.License.UnLicenseText

        'Unknown License ------------------------------------------------
        'ApplicationInfo.License.Type = ADVL_Utilities_Library_1.License.Types.Unknown
        'ApplicationInfo.License.Notice = ""
        'ApplicationInfo.License.Text = ""

        'Source Code: --------------------------------------------------------------------------------------------------
        'Add your source code information here if required.
        'THIS SECTION WILL BE UPDATED TO ALLOW A GITHUB LINK.
        ApplicationInfo.SourceCode.Language = "Visual Basic 2019"
        ApplicationInfo.SourceCode.FileName = ""
        ApplicationInfo.SourceCode.FileSize = 0
        ApplicationInfo.SourceCode.FileHash = ""
        ApplicationInfo.SourceCode.WebLink = ""
        ApplicationInfo.SourceCode.Contact = ""
        ApplicationInfo.SourceCode.Comments = ""

        'ModificationSummary: -----------------------------------------------------------------------------------------
        'Add any source code modification here is required.
        ApplicationInfo.ModificationSummary.BaseCodeName = ""
        ApplicationInfo.ModificationSummary.BaseCodeDescription = ""
        ApplicationInfo.ModificationSummary.BaseCodeVersion.Major = 0
        ApplicationInfo.ModificationSummary.BaseCodeVersion.Minor = 0
        ApplicationInfo.ModificationSummary.BaseCodeVersion.Build = 0
        ApplicationInfo.ModificationSummary.BaseCodeVersion.Revision = 0
        ApplicationInfo.ModificationSummary.Description = "This is the first released version of the application. No earlier base code used."

        'Library List: ------------------------------------------------------------------------------------------------
        'Add the ADVL_Utilties_Library_1 library:
        Dim NewLib As New ADVL_Utilities_Library_1.LibrarySummary
        NewLib.Name = "ADVL_System_Utilities"
        NewLib.Description = "System Utility classes used in Andorville™ software development system applications"
        NewLib.CreationDate = "7-Jan-2016 12:00:00"
        NewLib.LicenseNotice = "Copyright 2016 Signalworks Pty Ltd, ABN 26 066 681 598" & vbCrLf &
                               vbCrLf &
                               "Licensed under the Apache License, Version 2.0 (the ""License"");" & vbCrLf &
                               "you may not use this file except in compliance with the License." & vbCrLf &
                               "You may obtain a copy of the License at" & vbCrLf &
                               vbCrLf &
                               "http://www.apache.org/licenses/LICENSE-2.0" & vbCrLf &
                               vbCrLf &
                               "Unless required by applicable law or agreed to in writing, software" & vbCrLf &
                               "distributed under the License is distributed on an ""AS IS"" BASIS," & vbCrLf &
                               "WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied." & vbCrLf &
                               "See the License for the specific language governing permissions and" & vbCrLf &
                               "limitations under the License." & vbCrLf

        NewLib.CopyrightNotice = "Copyright 2016 Signalworks Pty Ltd, ABN 26 066 681 598"

        NewLib.Version.Major = 1
        NewLib.Version.Minor = 0
        'NewLib.Version.Build = 1
        NewLib.Version.Build = 0
        NewLib.Version.Revision = 0

        NewLib.Author.Name = "Signalworks Pty Ltd"
        NewLib.Author.Description = "Signalworks Pty Ltd" & vbCrLf &
            "Australian Proprietary Company" & vbCrLf &
            "ABN 26 066 681 598" & vbCrLf &
            "Registration Date 05/10/1994"

        NewLib.Author.Contact = "http://www.andorville.com.au/"

        Dim NewClass1 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass1.Name = "ZipComp"
        NewClass1.Description = "The ZipComp class is used to compress files into and extract files from a zip file."
        NewLib.Classes.Add(NewClass1)
        Dim NewClass2 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass2.Name = "XSequence"
        NewClass2.Description = "The XSequence class is used to run an XML property sequence (XSequence) file. XSequence files are used to record and replay processing sequences in Andorville™ software applications."
        NewLib.Classes.Add(NewClass2)
        Dim NewClass3 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass3.Name = "XMessage"
        NewClass3.Description = "The XMessage class is used to read an XML Message (XMessage). An XMessage is a simplified XSequence used to exchange information between Andorville™ software applications."
        NewLib.Classes.Add(NewClass3)
        Dim NewClass4 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass4.Name = "Location"
        NewClass4.Description = "The Location class consists of properties and methods to store data in a location, which is either a directory or archive file."
        NewLib.Classes.Add(NewClass4)
        Dim NewClass5 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass5.Name = "Project"
        NewClass5.Description = "An Andorville™ software application can store data within one or more projects. Each project stores a set of related data files. The Project class contains properties and methods used to manage a project."
        NewLib.Classes.Add(NewClass5)
        Dim NewClass6 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass6.Name = "ProjectSummary"
        NewClass6.Description = "ProjectSummary stores a summary of a project."
        NewLib.Classes.Add(NewClass6)
        Dim NewClass7 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass7.Name = "DataFileInfo"
        NewClass7.Description = "The DataFileInfo class stores information about a data file."
        NewLib.Classes.Add(NewClass7)
        Dim NewClass8 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass8.Name = "Message"
        NewClass8.Description = "The Message class contains text properties and methods used to display messages in an Andorville™ software application."
        NewLib.Classes.Add(NewClass8)
        Dim NewClass9 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass9.Name = "ApplicationSummary"
        NewClass9.Description = "The ApplicationSummary class stores a summary of an Andorville™ software application."
        NewLib.Classes.Add(NewClass9)
        Dim NewClass10 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass10.Name = "LibrarySummary"
        NewClass10.Description = "The LibrarySummary class stores a summary of a software library used by an application."
        NewLib.Classes.Add(NewClass10)
        Dim NewClass11 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass11.Name = "ClassSummary"
        NewClass11.Description = "The ClassSummary class stores a summary of a class contained in a software library."
        NewLib.Classes.Add(NewClass11)
        Dim NewClass12 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass12.Name = "ModificationSummary"
        NewClass12.Description = "The ModificationSummary class stores a summary of any modifications made to an application or library."
        NewLib.Classes.Add(NewClass12)
        Dim NewClass13 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass13.Name = "ApplicationInfo"
        NewClass13.Description = "The ApplicationInfo class stores information about an Andorville™ software application."
        NewLib.Classes.Add(NewClass13)
        Dim NewClass14 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass14.Name = "Version"
        NewClass14.Description = "The Version class stores application, library or project version information."
        NewLib.Classes.Add(NewClass14)
        Dim NewClass15 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass15.Name = "Author"
        NewClass15.Description = "The Author class stores information about an Author."
        NewLib.Classes.Add(NewClass15)
        Dim NewClass16 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass16.Name = "FileAssociation"
        NewClass16.Description = "The FileAssociation class stores the file association extension and description. An application can open files on its file association list."
        NewLib.Classes.Add(NewClass16)
        Dim NewClass17 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass17.Name = "Copyright"
        NewClass17.Description = "The Copyright class stores copyright information."
        NewLib.Classes.Add(NewClass17)
        Dim NewClass18 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass18.Name = "License"
        NewClass18.Description = "The License class stores license information."
        NewLib.Classes.Add(NewClass18)
        Dim NewClass19 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass19.Name = "SourceCode"
        NewClass19.Description = "The SourceCode class stores information about the source code for the application."
        NewLib.Classes.Add(NewClass19)
        Dim NewClass20 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass20.Name = "Usage"
        NewClass20.Description = "The Usage class stores information about application or project usage."
        NewLib.Classes.Add(NewClass20)
        Dim NewClass21 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass21.Name = "Trademark"
        NewClass21.Description = "The Trademark class stored information about a trademark used by the author of an application or data."
        NewLib.Classes.Add(NewClass21)

        ApplicationInfo.Libraries.Add(NewLib)

        'Add other library information here: --------------------------------------------------------------------------

    End Sub

    'Save the form settings if the form is being minimised:
    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = &H112 Then 'SysCommand
            If m.WParam.ToInt32 = &HF020 Then 'Form is being minimised
                SaveFormSettings()
            End If
        End If
        MyBase.WndProc(m)
    End Sub

    Private Sub SaveProjectSettings()
        'Save the project settings in an XML file.
        'Add any Project Settings to be saved into the settingsData XDocument.
        Dim settingsData = <?xml version="1.0" encoding="utf-8"?>
                           <!---->
                           <!--Project settings for ADVL_Coordinates_1 application.-->
                           <ProjectSettings>
                           </ProjectSettings>

        Dim SettingsFileName As String = "ProjectSettings_" & ApplicationInfo.Name & "_" & ".xml"
        Project.SaveXmlSettings(SettingsFileName, settingsData)
    End Sub

    Private Sub RestoreProjectSettings()
        'Restore the project settings from an XML document.

        Dim SettingsFileName As String = "ProjectSettings_" & ApplicationInfo.Name & "_" & ".xml"

        If Project.SettingsFileExists(SettingsFileName) Then
            Dim Settings As System.Xml.Linq.XDocument
            Project.ReadXmlSettings(SettingsFileName, Settings)

            If IsNothing(Settings) Then 'There is no Settings XML data.
                Exit Sub
            End If

            'Restore a Project Setting example:
            If Settings.<ProjectSettings>.<Setting1>.Value = Nothing Then
                'Project setting not saved.
                'Setting1 = ""
            Else
                'Setting1 = Settings.<ProjectSettings>.<Setting1>.Value
            End If

            'Continue restoring saved settings.

        End If

    End Sub

#End Region 'Process XML Files ----------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Form Display Methods - Code used to display this form." '============================================================================================================================

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Loading the Main form.

        'Set the Application Directory path: ------------------------------------------------
        Project.ApplicationDir = My.Application.Info.DirectoryPath.ToString

        'Read the Application Information file: ---------------------------------------------
        ApplicationInfo.ApplicationDir = My.Application.Info.DirectoryPath.ToString 'Set the Application Directory property

        ''Get the Application Version Information:
        'ApplicationInfo.Version.Major = My.Application.Info.Version.Major
        'ApplicationInfo.Version.Minor = My.Application.Info.Version.Minor
        'ApplicationInfo.Version.Build = My.Application.Info.Version.Build
        'ApplicationInfo.Version.Revision = My.Application.Info.Version.Revision

        If ApplicationInfo.ApplicationLocked Then
            MessageBox.Show("The application is locked. If the application is not already in use, remove the 'Application_Info.lock file from the application directory: " & ApplicationInfo.ApplicationDir, "Notice", MessageBoxButtons.OK)
            Dim dr As System.Windows.Forms.DialogResult
            dr = MessageBox.Show("Press 'Yes' to unlock the application", "Notice", MessageBoxButtons.YesNo)
            If dr = System.Windows.Forms.DialogResult.Yes Then
                ApplicationInfo.UnlockApplication()
            Else
                Application.Exit()
                Exit Sub
            End If
        End If

        ReadApplicationInfo()

        'Read the Application Usage information: --------------------------------------------
        ApplicationUsage.StartTime = Now
        ApplicationUsage.SaveLocn.Type = ADVL_Utilities_Library_1.FileLocation.Types.Directory
        ApplicationUsage.SaveLocn.Path = Project.ApplicationDir
        ApplicationUsage.RestoreUsageInfo()

        'Restore Project information: -------------------------------------------------------
        Project.Application.Name = ApplicationInfo.Name

        'Set up Message object:
        Message.ApplicationName = ApplicationInfo.Name

        'Set up a temporary initial settings location:
        Dim TempLocn As New ADVL_Utilities_Library_1.FileLocation
        TempLocn.Type = ADVL_Utilities_Library_1.FileLocation.Types.Directory
        TempLocn.Path = ApplicationInfo.ApplicationDir
        Message.SettingsLocn = TempLocn

        Me.Show() 'Show this form before showing the Message form - This will show the App icon on top in the TaskBar.

        'Start showing messages here - Message system is set up.
        'Message.AddText("------------------- Starting Application: ADVL Application Template ----------------- " & vbCrLf, "Heading")
        Message.AddText("------------------- Starting Application: ADVL Distributions ------------------------ " & vbCrLf, "Heading")
        'Message.AddText("Application usage: Total duration = " & Format(ApplicationUsage.TotalDuration.TotalHours, "#.##") & " hours" & vbCrLf, "Normal")
        Dim TotalDuration As String = ApplicationUsage.TotalDuration.Days.ToString.PadLeft(5, "0"c) & "d:" &
                           ApplicationUsage.TotalDuration.Hours.ToString.PadLeft(2, "0"c) & "h:" &
                           ApplicationUsage.TotalDuration.Minutes.ToString.PadLeft(2, "0"c) & "m:" &
                           ApplicationUsage.TotalDuration.Seconds.ToString.PadLeft(2, "0"c) & "s"
        Message.AddText("Application usage: Total duration = " & TotalDuration & vbCrLf, "Normal")

        'https://msdn.microsoft.com/en-us/library/z2d603cy(v=vs.80).aspx#Y550
        'Process any command line arguments:
        Try
            For Each s As String In My.Application.CommandLineArgs
                Message.Add("Command line argument: " & vbCrLf)
                Message.AddXml(s & vbCrLf & vbCrLf)
                InstrReceived = s
            Next
        Catch ex As Exception
            Message.AddWarning("Error processing command line arguments: " & ex.Message & vbCrLf)
        End Try

        If ProjectSelected = False Then
            'Read the Settings Location for the last project used:
            Project.ReadLastProjectInfo()
            'The Last_Project_Info.xml file contains:
            '  Project Name and Description. Settings Location Type and Settings Location Path.
            Message.Add("Last project details:" & vbCrLf)
            Message.Add("Project Type:  " & Project.Type.ToString & vbCrLf)
            Message.Add("Project Path:  " & Project.Path & vbCrLf)

            'At this point read the application start arguments, if any.
            'The selected project may be changed here.

            'Check if the project is locked:
            If Project.ProjectLocked Then
                Message.AddWarning("The project is locked: " & Project.Name & vbCrLf)
                Dim dr As System.Windows.Forms.DialogResult
                dr = MessageBox.Show("Press 'Yes' to unlock the project", "Notice", MessageBoxButtons.YesNo)
                If dr = System.Windows.Forms.DialogResult.Yes Then
                    Project.UnlockProject()
                    Message.AddWarning("The project has been unlocked: " & Project.Name & vbCrLf)
                    'Read the Project Information file: -------------------------------------------------
                    Message.Add("Reading project info." & vbCrLf)
                    Project.ReadProjectInfoFile()                 'Read the file in the SettingsLocation: ADVL_Project_Info.xml
                    Project.ReadParameters()
                    Project.ReadParentParameters()
                    If Project.ParentParameterExists("ProNetName") Then
                        Project.AddParameter("ProNetName", Project.ParentParameter("ProNetName").Value, Project.ParentParameter("ProNetName").Description) 'AddParameter will update the parameter if it already exists.
                        ProNetName = Project.Parameter("ProNetName").Value
                    Else
                        ProNetName = Project.GetParameter("ProNetName")
                    End If
                    If Project.ParentParameterExists("ProNetPath") Then 'Get the parent parameter value - it may have been updated.
                        Project.AddParameter("ProNetPath", Project.ParentParameter("ProNetPath").Value, Project.ParentParameter("ProNetPath").Description) 'AddParameter will update the parameter if it already exists.
                        ProNetPath = Project.Parameter("ProNetPath").Value
                    Else
                        ProNetPath = Project.GetParameter("ProNetPath") 'If the parameter does not exist, the value is set to ""
                    End If
                    Project.SaveParameters() 'These should be saved now - child projects look for parent parameters in the parameter file.

                    Project.LockProject() 'Lock the project while it is open in this application.
                    'Set the project start time. This is used to track project usage.
                    Project.Usage.StartTime = Now
                    ApplicationInfo.SettingsLocn = Project.SettingsLocn
                    'Set up the Message object:
                    Message.SettingsLocn = Project.SettingsLocn
                    Message.Show()
                Else
                    'Continue without any project selected.
                    Project.Name = ""
                    Project.Type = ADVL_Utilities_Library_1.Project.Types.None
                    Project.Description = ""
                    Project.SettingsLocn.Path = ""
                    Project.DataLocn.Path = ""
                End If
            Else
                'Read the Project Information file: -------------------------------------------------
                Message.Add("Reading project info." & vbCrLf)
                Project.ReadProjectInfoFile()  'Read the file in the Project Location: ADVL_Project_Info.xml
                Project.ReadParameters()
                Project.ReadParentParameters()
                If Project.ParentParameterExists("ProNetName") Then
                    Project.AddParameter("ProNetName", Project.ParentParameter("ProNetName").Value, Project.ParentParameter("ProNetName").Description) 'AddParameter will update the parameter if it already exists.
                    ProNetName = Project.Parameter("ProNetName").Value
                Else
                    ProNetName = Project.GetParameter("ProNetName")
                End If
                If Project.ParentParameterExists("ProNetPath") Then 'Get the parent parameter value - it may have been updated.
                    Project.AddParameter("ProNetPath", Project.ParentParameter("ProNetPath").Value, Project.ParentParameter("ProNetPath").Description) 'AddParameter will update the parameter if it already exists.
                    ProNetPath = Project.Parameter("ProNetPath").Value
                Else
                    ProNetPath = Project.GetParameter("ProNetPath") 'If the parameter does not exist, the value is set to ""
                End If
                Project.SaveParameters() 'These should be saved now - child projects look for parent parameters in the parameter file.

                Project.LockProject() 'Lock the project while it is open in this application.
                'Set the project start time. This is used to track project usage.
                Project.Usage.StartTime = Now
                ApplicationInfo.SettingsLocn = Project.SettingsLocn
                'Set up the Message object:
                Message.SettingsLocn = Project.SettingsLocn
                Message.Show() 'Added 18May19
            End If

        Else  'Project has been opened using Command Line arguments.
            Project.ReadParameters()
            Project.ReadParentParameters()
            If Project.ParentParameterExists("ProNetName") Then
                Project.AddParameter("ProNetName", Project.ParentParameter("ProNetName").Value, Project.ParentParameter("ProNetName").Description) 'AddParameter will update the parameter if it already exists.
                ProNetName = Project.Parameter("ProNetName").Value
            Else
                ProNetName = Project.GetParameter("ProNetName")
            End If
            If Project.ParentParameterExists("ProNetPath") Then 'Get the parent parameter value - it may have been updated.
                Project.AddParameter("ProNetPath", Project.ParentParameter("ProNetPath").Value, Project.ParentParameter("ProNetPath").Description) 'AddParameter will update the parameter if it already exists.
                ProNetPath = Project.Parameter("ProNetPath").Value
            Else
                ProNetPath = Project.GetParameter("ProNetPath") 'If the parameter does not exist, the value is set to ""
            End If
            Project.SaveParameters() 'These should be saved now - child projects look for parent parameters in the parameter file.

            Project.LockProject() 'Lock the project while it is open in this application.
            ProjectSelected = False 'Reset the Project Selected flag.

            'Set up the Message object:
            Message.SettingsLocn = Project.SettingsLocn
            Message.Show() 'Added 18May19
        End If

        'START Initialise the form: ===============================================================

        Me.WebBrowser1.ObjectForScripting = Me
        'IF THE LINE ABOVE PRODUCES AN ERROR ON STARTUP, CHECK THAT THE CODE ON THE FOLLOWING THREE LINES IS INSERTED JUST ABOVE THE Public Class Main STATEMENT.
        'Imports System.Security.Permissions
        '<PermissionSet(SecurityAction.Demand, Name:="FullTrust")>
        '<System.Runtime.InteropServices.ComVisibleAttribute(True)>

        bgwSendMessage.WorkerReportsProgress = True
        bgwSendMessage.WorkerSupportsCancellation = True

        bgwSendMessageAlt.WorkerReportsProgress = True
        bgwSendMessageAlt.WorkerSupportsCancellation = True

        bgwRunInstruction.WorkerReportsProgress = True
        bgwRunInstruction.WorkerSupportsCancellation = True

        InitialiseForm() 'Initialise the form for a new project.



        'dgvParams.ColumnCount = 7
        dgvParams.ColumnCount = 8
        dgvParams.Columns(0).HeaderText = "Name"
        dgvParams.Columns(0).ReadOnly = True

        dgvParams.Columns(1).HeaderText = "Symbol"

        dgvParams.Columns(2).HeaderText = "Value"
        dgvParams.Columns(3).HeaderText = "Type"
        dgvParams.Columns(3).ReadOnly = True
        dgvParams.Columns(4).HeaderText = "No. Type"
        dgvParams.Columns(4).ReadOnly = True
        dgvParams.Columns(5).HeaderText = "Min"
        dgvParams.Columns(5).ReadOnly = True
        dgvParams.Columns(6).HeaderText = "Max"
        dgvParams.Columns(6).ReadOnly = True
        dgvParams.Columns(7).HeaderText = "Description"
        'dgvParams.Columns(7).ReadOnly = True
        dgvParams.Columns(7).ReadOnly = False

        dgvParams.AllowUserToAddRows = False

        'dgvFields.ColumnCount = 8
        'dgvFields.ColumnCount = 7
        'dgvFields.ColumnCount = 6
        'dgvFields.ColumnCount = 7
        'dgvFields.ColumnCount = 8
        dgvFields.ColumnCount = 15
        dgvFields.Columns(0).HeaderText = "Name"
        dgvFields.Columns(0).ReadOnly = True
        dgvFields.Columns(1).HeaderText = "Valid"
        dgvFields.Columns(1).ReadOnly = True

        'Dim cboGenerate As New DataGridViewComboBoxColumn 'Used for selecting if the Field is generated.
        'cboGenerate.FlatStyle = FlatStyle.Flat
        'cboGenerate.Items.Add("True") 'Generate the Field.
        'cboGenerate.Items.Add("False") 'Do not Generate the Field.
        'dgvFields.Columns.Insert(2, cboGenerate)
        'dgvFields.Columns(2).HeaderText = "Generate"
        'dgvFields.Columns(2).Width = 150

        Dim chkGenerate As New DataGridViewCheckBoxColumn
        chkGenerate.FlatStyle = FlatStyle.Flat
        dgvFields.Columns.Insert(2, chkGenerate)
        dgvFields.Columns(2).HeaderText = "Generate"
        dgvFields.Columns(2).Width = 150

        'dgvFields.Columns(3).HeaderText = "Column Number"
        'dgvFields.Columns(3).ReadOnly = True

        'dgvFields.Columns(3).HeaderText = "Number Type"
        'dgvFields.Columns(3).ReadOnly = True
        Dim cboNumType As New DataGridViewComboBoxColumn 'Used for selecting the number type
        cboNumType.FlatStyle = FlatStyle.Flat
        cboNumType.Items.Add("Single")
        cboNumType.Items.Add("Double")
        cboNumType.Items.Add("Integer")
        cboNumType.Items.Add("Long")
        dgvFields.Columns.Insert(3, cboNumType)
        dgvFields.Columns(3).HeaderText = "Number Type"

        dgvFields.Columns(4).HeaderText = "Format"
        dgvFields.Columns(4).ReadOnly = False


        Dim cboAlignment As New DataGridViewComboBoxColumn 'Used for selecting the Field alignment
        cboAlignment.FlatStyle = FlatStyle.Flat
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
        dgvFields.Columns.Insert(5, cboAlignment)
        dgvFields.Columns(5).HeaderText = "Alignment"
        dgvFields.Columns(5).Width = 150

        'dgvFields.Columns(6).HeaderText = "Units"
        'dgvFields.Columns(6).ReadOnly = False
        'dgvFields.Columns(7).HeaderText = "Label"
        'dgvFields.Columns(7).ReadOnly = False

        dgvFields.Columns(6).HeaderText = "Value Label"
        dgvFields.Columns(6).ReadOnly = False
        dgvFields.Columns(7).HeaderText = "Units"
        dgvFields.Columns(7).ReadOnly = False

        dgvFields.Columns(8).HeaderText = "Label Prefix"
        dgvFields.Columns(8).ReadOnly = False

        dgvFields.Columns(9).HeaderText = "Series Label"
        dgvFields.Columns(9).ReadOnly = False

        dgvFields.Columns(10).HeaderText = "Description"
        dgvFields.Columns(10).ReadOnly = False
        dgvFields.AllowUserToAddRows = False

        Dim cboMarkerFill As New DataGridViewComboBoxColumn 'Used for selecting a transparent marker (No Fill)
        cboMarkerFill.Items.Add("Yes")
        cboMarkerFill.Items.Add("No")
        'dgvFields.Columns.Insert(9, cboMarkerFill) 'Insert the combo box column used to select the Marker Fill
        dgvFields.Columns.Insert(11, cboMarkerFill) 'Insert the combo box column used to select the Marker Fill
        dgvFields.Columns(11).HeaderText = "Marker Fill"

        dgvFields.Columns(12).HeaderText = "Marker Color"

        dgvFields.Columns(13).HeaderText = "Border Color"

        dgvFields.Columns(14).HeaderText = "Border Width"

        Dim cboMarkerStyle As New DataGridViewComboBoxColumn 'Used for selecting the Marker Style
        For Each marker In [Enum].GetNames(GetType(DataVisualization.Charting.MarkerStyle))
            cboMarkerStyle.Items.Add(marker)
        Next
        dgvFields.Columns.Insert(15, cboMarkerStyle)
        dgvFields.Columns(15).HeaderText = "Marker Style"

        dgvFields.Columns(16).HeaderText = "Marker Size"

        dgvFields.Columns(17).HeaderText = "Marker Step"

        dgvFields.Columns(18).HeaderText = "Line Color"

        dgvFields.Columns(19).HeaderText = "Line Width"

        dgvFields.AllowUserToAddRows = False


        dgvXFields.ColumnCount = 5
        dgvXFields.Columns(0).HeaderText = "Name"
        dgvXFields.Columns(0).ReadOnly = True

        Dim cboXNumType As New DataGridViewComboBoxColumn 'Used for selecting the number type
        cboXNumType.FlatStyle = FlatStyle.Flat
        cboXNumType.Items.Add("Single")
        cboXNumType.Items.Add("Double")
        cboXNumType.Items.Add("Integer")
        cboXNumType.Items.Add("Long")
        dgvXFields.Columns.Insert(1, cboXNumType)
        dgvXFields.Columns(1).HeaderText = "Number Type"

        dgvXFields.Columns(2).HeaderText = "Format"
        dgvXFields.Columns(2).ReadOnly = False

        Dim cboXAlignment As New DataGridViewComboBoxColumn 'Used for selecting the Field alignment
        cboXAlignment.FlatStyle = FlatStyle.Flat
        cboXAlignment.Items.Add("NotSet")
        cboXAlignment.Items.Add("TopLeft")
        cboXAlignment.Items.Add("TopCenter")
        cboXAlignment.Items.Add("TopRight")
        cboXAlignment.Items.Add("MiddleLeft")
        cboXAlignment.Items.Add("MiddleCenter")
        cboXAlignment.Items.Add("MiddleRight")
        cboXAlignment.Items.Add("BottomLeft")
        cboXAlignment.Items.Add("BottomCenter")
        cboXAlignment.Items.Add("BottomRight")
        dgvXFields.Columns.Insert(3, cboXAlignment)
        dgvXFields.Columns(3).HeaderText = "Alignment"
        dgvXFields.Columns(3).Width = 150

        dgvXFields.Columns(4).HeaderText = "Value Label"
        dgvXFields.Columns(4).ReadOnly = False
        dgvXFields.Columns(5).HeaderText = "Units"
        dgvXFields.Columns(5).ReadOnly = False
        dgvXFields.Columns(6).HeaderText = "Description"
        dgvXFields.Columns(6).ReadOnly = False

        dgvXFields.AllowUserToAddRows = False


        chkUpdateLabel.Checked = True

        'Get a list of Color names:
        For Each Color As KnownColor In [Enum].GetValues(GetType(KnownColor)) 'ActiveBorder to MenuHighlight - Includes system color names
            If Color > 27 And Color < 168 Then
                cmbColors.Items.Add([Enum].GetName(GetType(KnownColor), Color)) 'AliceBlue to YellowGreen - System color names not included.
            End If
        Next

        rbDecimal.Checked = True

        cmbDefMkrFill.Items.Add("Yes")
        cmbDefMkrFill.Items.Add("No")

        For Each marker In [Enum].GetNames(GetType(DataVisualization.Charting.MarkerStyle))
            cmbDefMkrStyle.Items.Add(marker)
        Next

        SelDistrib = 0
        NDistribs = 0

        Timer2.Interval = 1000 '1000ms interval. UpdateAnnotationSettings starts the timer. After 1000ms, it the Ajust form is open, Adjust.LoadParamInfo is run. This updates the parameters on the Adjust form.

        'END   Initialise the form: ---------------------------------------------------------------

        RestoreFormSettings() 'Restore the form settings
        OpenStartPage()
        Message.ShowXMessages = ShowXMessages
        Message.ShowSysMessages = ShowSysMessages
        RestoreProjectSettings() 'Restore the Project settings

        ShowProjectInfo() 'Show the project information.

        Message.AddText("------------------- Started OK -------------------------------------------------------------------------- " & vbCrLf & vbCrLf, "Heading")

        bgwCancelConn.WorkerSupportsCancellation = True
        bgwCancelConn.RunWorkerAsync() 'Show the cancel dialog.

        If StartupConnectionName = "" Then
            If Project.ConnectOnOpen Then
                ConnectToComNet() 'The Project is set to connect when it is opened.
            ElseIf ApplicationInfo.ConnectOnStartup Then
                ConnectToComNet() 'The Application is set to connect when it is started.
            Else
                'Don't connect to ComNet.
            End If
        Else
            'Connect to ComNet using the connection name StartupConnectionName.
            ConnectToComNet(StartupConnectionName)
        End If

        If bgwCancelConn.IsBusy Then
            SendKeys.Send("{ESC}") 'Close the MessageBox
            bgwCancelConn.CancelAsync()
            bgwCancelConn.Dispose()
            Message.Add("Cancel Connection Dialog stopped." & vbCrLf)
        End If

        'Get the Application Version Information:
        If System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed Then
            'Application is network deployed.
            ApplicationInfo.Version.Number = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString
            ApplicationInfo.Version.Major = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.Major
            ApplicationInfo.Version.Minor = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.Minor
            ApplicationInfo.Version.Build = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.Build
            ApplicationInfo.Version.Revision = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.Revision
            ApplicationInfo.Version.Source = "Publish"
            Message.Add("Application version: " & System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString & vbCrLf)
        Else
            'Application is not network deployed.
            ApplicationInfo.Version.Number = My.Application.Info.Version.ToString
            ApplicationInfo.Version.Major = My.Application.Info.Version.Major
            ApplicationInfo.Version.Minor = My.Application.Info.Version.Minor
            ApplicationInfo.Version.Build = My.Application.Info.Version.Build
            ApplicationInfo.Version.Revision = My.Application.Info.Version.Revision
            ApplicationInfo.Version.Source = "Assembly"
            Message.Add("Application version: " & My.Application.Info.Version.ToString & vbCrLf)
        End If

    End Sub

    Private Sub InitialiseForm()
        'Initialise the form for a new project.
        'OpenStartPage()
    End Sub

    Private Sub ShowProjectInfo()
        'Show the project information:

        txtParentProject.Text = Project.ParentProjectName
        txtProNetName.Text = Project.GetParameter("ProNetName")
        txtProjectName.Text = Project.Name
        txtProjectDescription.Text = Project.Description
        Select Case Project.Type
            Case ADVL_Utilities_Library_1.Project.Types.Directory
                txtProjectType.Text = "Directory"
            Case ADVL_Utilities_Library_1.Project.Types.Archive
                txtProjectType.Text = "Archive"
            Case ADVL_Utilities_Library_1.Project.Types.Hybrid
                txtProjectType.Text = "Hybrid"
            Case ADVL_Utilities_Library_1.Project.Types.None
                txtProjectType.Text = "None"
        End Select

        txtCreationDate.Text = Format(Project.Usage.FirstUsed, "d-MMM-yyyy H:mm:ss")
        txtLastUsed.Text = Format(Project.Usage.LastUsed, "d-MMM-yyyy H:mm:ss")

        txtProjectPath.Text = Project.Path

        Select Case Project.SettingsLocn.Type
            Case ADVL_Utilities_Library_1.FileLocation.Types.Directory
                txtSettingsLocationType.Text = "Directory"
            Case ADVL_Utilities_Library_1.FileLocation.Types.Archive
                txtSettingsLocationType.Text = "Archive"
        End Select
        txtSettingsPath.Text = Project.SettingsLocn.Path

        Select Case Project.DataLocn.Type
            Case ADVL_Utilities_Library_1.FileLocation.Types.Directory
                txtDataLocationType.Text = "Directory"
            Case ADVL_Utilities_Library_1.FileLocation.Types.Archive
                txtDataLocationType.Text = "Archive"
        End Select
        txtDataPath.Text = Project.DataLocn.Path

        Select Case Project.SystemLocn.Type
            Case ADVL_Utilities_Library_1.FileLocation.Types.Directory
                txtSystemLocationType.Text = "Directory"
            Case ADVL_Utilities_Library_1.FileLocation.Types.Archive
                txtSystemLocationType.Text = "Archive"
        End Select
        txtSystemPath.Text = Project.SystemLocn.Path

        If Project.ConnectOnOpen Then
            chkConnect.Checked = True
        Else
            chkConnect.Checked = False
        End If

        'txtTotalDuration.Text = Project.Usage.TotalDuration.Days.ToString.PadLeft(5, "0"c) & ":" &
        '                        Project.Usage.TotalDuration.Hours.ToString.PadLeft(2, "0"c) & ":" &
        '                        Project.Usage.TotalDuration.Minutes.ToString.PadLeft(2, "0"c) & ":" &
        '                        Project.Usage.TotalDuration.Seconds.ToString.PadLeft(2, "0"c)

        'txtCurrentDuration.Text = Project.Usage.CurrentDuration.Days.ToString.PadLeft(5, "0"c) & ":" &
        '                          Project.Usage.CurrentDuration.Hours.ToString.PadLeft(2, "0"c) & ":" &
        '                          Project.Usage.CurrentDuration.Minutes.ToString.PadLeft(2, "0"c) & ":" &
        '                          Project.Usage.CurrentDuration.Seconds.ToString.PadLeft(2, "0"c)

        txtTotalDuration.Text = Project.Usage.TotalDuration.Days.ToString.PadLeft(5, "0"c) & "d:" &
                Project.Usage.TotalDuration.Hours.ToString.PadLeft(2, "0"c) & "h:" &
                Project.Usage.TotalDuration.Minutes.ToString.PadLeft(2, "0"c) & "m:" &
                Project.Usage.TotalDuration.Seconds.ToString.PadLeft(2, "0"c) & "s"

        txtCurrentDuration.Text = Project.Usage.CurrentDuration.Days.ToString.PadLeft(5, "0"c) & "d:" &
                                  Project.Usage.CurrentDuration.Hours.ToString.PadLeft(2, "0"c) & "h:" &
                                  Project.Usage.CurrentDuration.Minutes.ToString.PadLeft(2, "0"c) & "m:" &
                                  Project.Usage.CurrentDuration.Seconds.ToString.PadLeft(2, "0"c) & "s"

    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        'Exit the Application

        If Distribution.Modified = True Then
            Dim dr As System.Windows.Forms.DialogResult
            dr = MessageBox.Show("Press 'Yes' to save changes to the Distribution Model.", "Notice", MessageBoxButtons.YesNo)
            If dr = System.Windows.Forms.DialogResult.Yes Then
                SaveDistModel()
            End If
        End If

        DisconnectFromComNet() 'Disconnect from the Communication Network (Message Service).

        SaveProjectSettings() 'Save project settings.

        ApplicationInfo.WriteFile() 'Update the Application Information file.

        Project.SaveLastProjectInfo() 'Save information about the last project used.

        Project.SaveParameters()

        'Project.SaveProjectInfoFile() 'Update the Project Information file. This is not required unless there is a change made to the project.

        Project.Usage.SaveUsageInfo() 'Save Project usage information.

        Project.UnlockProject() 'Unlock the project.

        ApplicationUsage.SaveUsageInfo() 'Save Application usage information.
        ApplicationInfo.UnlockApplication()

        Application.Exit()

    End Sub

    Private Sub Main_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        'Save the form settings if the form state is normal. (A minimised form will have the incorrect size and location.)
        If WindowState = FormWindowState.Normal Then
            SaveFormSettings()
        End If
    End Sub


#End Region 'Form Display Methods -------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Open and Close Forms - Code used to open and close other forms." '===================================================================================================================

    'Private Sub btnOpenTemplateForm_Click(sender As Object, e As EventArgs) Handles btnOpenTemplateForm.Click
    '    'Open the Template form:
    '    If IsNothing(TemplateForm) Then
    '        TemplateForm = New frmTemplate
    '        TemplateForm.Show()
    '    Else
    '        TemplateForm.Show()
    '    End If
    'End Sub

    'Private Sub TemplateForm_FormClosed(sender As Object, e As FormClosedEventArgs) Handles TemplateForm.FormClosed
    '    TemplateForm = Nothing
    'End Sub

    Private Sub btnMessages_Click(sender As Object, e As EventArgs) Handles btnMessages.Click
        'Show the Messages form.
        Message.ApplicationName = ApplicationInfo.Name
        Message.SettingsLocn = Project.SettingsLocn
        Message.Show()
        Message.ShowXMessages = ShowXMessages
        Message.MessageForm.BringToFront()
    End Sub

    Private Sub btnWebPages_Click(sender As Object, e As EventArgs) Handles btnWebPages.Click
        'Open the Web Pages form.

        If IsNothing(WebPageList) Then
            WebPageList = New frmWebPageList
            WebPageList.Show()
        Else
            WebPageList.Show()
            WebPageList.BringToFront()
        End If
    End Sub

    Private Sub WebPageList_FormClosed(sender As Object, e As FormClosedEventArgs) Handles WebPageList.FormClosed
        WebPageList = Nothing
    End Sub

    Public Function OpenNewWebPage() As Integer
        'Open a new HTML Web View window, or reuse an existing one if avaiable.
        'The new forms index number in WebViewFormList is returned.

        NewWebPage = New frmWebPage
        If WebPageFormList.Count = 0 Then
            WebPageFormList.Add(NewWebPage)
            WebPageFormList(0).FormNo = 0
            WebPageFormList(0).Show
            Return 0 'The new HTML Display is at position 0 in WebViewFormList()
        Else
            Dim I As Integer
            Dim FormAdded As Boolean = False
            For I = 0 To WebPageFormList.Count - 1 'Check if there are closed forms in WebViewFormList. They can be re-used.
                If IsNothing(WebPageFormList(I)) Then
                    WebPageFormList(I) = NewWebPage
                    WebPageFormList(I).FormNo = I
                    WebPageFormList(I).Show
                    FormAdded = True
                    Return I 'The new Html Display is at position I in WebViewFormList()
                    Exit For
                End If
            Next
            If FormAdded = False Then 'Add a new form to WebViewFormList
                Dim FormNo As Integer
                WebPageFormList.Add(NewWebPage)
                FormNo = WebPageFormList.Count - 1
                WebPageFormList(FormNo).FormNo = FormNo
                WebPageFormList(FormNo).Show
                Return FormNo 'The new WebPage is at position FormNo in WebPageFormList()
            End If
        End If
    End Function

    Public Sub WebPageFormClosed()
        'This subroutine is called when the Web Page form has been closed.
        'The subroutine is usually called from the FormClosed event of the WebPage form.
        'The WebPage form may have multiple instances.
        'The ClosedFormNumber property should contain the number of the instance of the WebPage form.
        'This property should be updated by the WebPage form when it is being closed.
        'The ClosedFormNumber property value is used to determine which element in WebPageList should be set to Nothing.

        If WebPageFormList.Count < ClosedFormNo + 1 Then
            'ClosedFormNo is too large to exist in WebPageFormList
            Exit Sub
        End If

        If IsNothing(WebPageFormList(ClosedFormNo)) Then
            'The form is already set to nothing
        Else
            WebPageFormList(ClosedFormNo) = Nothing
        End If
    End Sub

    Public Function OpenNewHtmlDisplayPage() As Integer
        'Open a new HTML display window, or reuse an existing one if avaiable.
        'The new forms index number in HtmlDisplayFormList is returned.

        NewHtmlDisplay = New frmHtmlDisplay
        If HtmlDisplayFormList.Count = 0 Then
            HtmlDisplayFormList.Add(NewHtmlDisplay)
            HtmlDisplayFormList(0).FormNo = 0
            HtmlDisplayFormList(0).Show
            Return 0 'The new HTML Display is at position 0 in HtmlDisplayFormList()
        Else
            Dim I As Integer
            Dim FormAdded As Boolean = False
            For I = 0 To HtmlDisplayFormList.Count - 1 'Check if there are closed forms in HtmlDisplayFormList. They can be re-used.
                If IsNothing(HtmlDisplayFormList(I)) Then
                    HtmlDisplayFormList(I) = NewHtmlDisplay
                    HtmlDisplayFormList(I).FormNo = I
                    HtmlDisplayFormList(I).Show
                    FormAdded = True
                    Return I 'The new Html Display is at position I in HtmlDisplayFormList()
                    Exit For
                End If
            Next
            If FormAdded = False Then 'Add a new form to HtmlDisplayFormList
                Dim FormNo As Integer
                HtmlDisplayFormList.Add(NewHtmlDisplay)
                FormNo = HtmlDisplayFormList.Count - 1
                HtmlDisplayFormList(FormNo).FormNo = FormNo
                HtmlDisplayFormList(FormNo).Show
                Return FormNo 'The new HtmlDisplay is at position FormNo in HtmlDisplayFormList()
            End If
        End If
    End Function

    Public Sub HtmlDisplayFormClosed()
        'This subroutine is called when the Html Display form has been closed.
        'The subroutine is usually called from the FormClosed event of the HtmlDisplay form.
        'The HtmlDisplay form may have multiple instances.
        'The ClosedFormNumber property should contain the number of the instance of the HtmlDisplay form.
        'This property should be updated by the HtmlDisplay form when it is being closed.
        'The ClosedFormNumber property value is used to determine which element in HtmlDisplayList should be set to Nothing.

        If HtmlDisplayFormList.Count < ClosedFormNo + 1 Then
            'ClosedFormNo is too large to exist in HtmlDisplayFormList
            Exit Sub
        End If

        If IsNothing(HtmlDisplayFormList(ClosedFormNo)) Then
            'The form is already set to nothing
        Else
            HtmlDisplayFormList(ClosedFormNo) = Nothing
        End If
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        'Show the Distributions form to select or edit a distribution.
        If IsNothing(Distributions) Then
            Distributions = New frmDistributions
            Distributions.Distribution = Distribution 'Pass a reference to the Distribution object.
            Distributions.Show()
        Else
            Distributions.Show()
        End If
    End Sub

    Private Sub Distributions_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Distributions.FormClosed
        Distributions = Nothing
    End Sub

    Private Sub Distributions_DistributionInfo(DistribInfo As clsDistribInfo) Handles Distributions.DistributionInfo
        'Distribution parameters have been selected.

        If SelDistrib < 1 Then
            Message.AddWarning("Please select or create a distribution." & vbCrLf)
            Exit Sub
        End If

        'Distribution.Distrib.Name = DistribInfo.Name
        Distribution.Info(SelDistrib - 1).Name = DistribInfo.Name
        Distribution.Info(SelDistrib - 1).Continuity = DistribInfo.Continuity
        Distribution.UpdateDistribCounts()
        Distribution.Info(SelDistrib - 1).NParams = DistribInfo.NParams
        Distribution.Info(SelDistrib - 1).RangeMin = DistribInfo.RangeMin
        Distribution.Info(SelDistrib - 1).RangeMax = DistribInfo.RangeMax

        Distribution.Info(SelDistrib - 1).PdfInfo.Valid = DistribInfo.PdfValid
        Distribution.Info(SelDistrib - 1).PdfLnInfo.Valid = DistribInfo.PdfLnValid
        Distribution.Info(SelDistrib - 1).PmfInfo.Valid = DistribInfo.PmfValid
        Distribution.Info(SelDistrib - 1).PmfLnInfo.Valid = DistribInfo.PmfLnValid
        Distribution.Info(SelDistrib - 1).CdfInfo.Valid = DistribInfo.CdfValid
        Distribution.Info(SelDistrib - 1).InvCdfInfo.Valid = DistribInfo.InvCdfValid

        If Not Distribution.Info(SelDistrib - 1).PdfInfo.Valid Then Distribution.Info(SelDistrib - 1).PdfInfo.Generate = False
        If Not Distribution.Info(SelDistrib - 1).PdfLnInfo.Valid Then Distribution.Info(SelDistrib - 1).PdfLnInfo.Generate = False
        If Not Distribution.Info(SelDistrib - 1).PmfInfo.Valid Then Distribution.Info(SelDistrib - 1).PmfInfo.Generate = False
        If Not Distribution.Info(SelDistrib - 1).PmfLnInfo.Valid Then Distribution.Info(SelDistrib - 1).PmfLnInfo.Generate = False
        If Not Distribution.Info(SelDistrib - 1).CdfInfo.Valid Then Distribution.Info(SelDistrib - 1).CdfInfo.Generate = False
        If Not Distribution.Info(SelDistrib - 1).InvCdfInfo.Valid Then Distribution.Info(SelDistrib - 1).InvCdfInfo.Generate = False

        'Distribution.Info(SelDistrib - 1).ParamA.Value = DistribInfo.ParamA
        Distribution.Info(SelDistrib - 1).ParamA.Value = DistribInfo.ParamAInfo.Value
        Distribution.Info(SelDistrib - 1).ParamA.Name = DistribInfo.ParamAInfo.Name
        Distribution.Info(SelDistrib - 1).ParamA.Type = DistribInfo.ParamAInfo.Type 'Added 12Oct22
        Distribution.Info(SelDistrib - 1).ParamA.Symbol = DistribInfo.ParamAInfo.Symbol
        Distribution.Info(SelDistrib - 1).ParamA.NumberType = DistribInfo.ParamAInfo.NumberType
        Distribution.Info(SelDistrib - 1).ParamA.Minimum = DistribInfo.ParamAInfo.Minimum
        Distribution.Info(SelDistrib - 1).ParamA.Maximum = DistribInfo.ParamAInfo.Maximum
        Distribution.Info(SelDistrib - 1).ParamA.Description = DistribInfo.ParamAInfo.Description
        Distribution.Info(SelDistrib - 1).ParamA.AdjustMin = DistribInfo.ParamAInfo.AdjustMin
        Distribution.Info(SelDistrib - 1).ParamA.AdjustMax = DistribInfo.ParamAInfo.AdjustMax

        If Distribution.Info(SelDistrib - 1).NParams > 1 Then
            'Distribution.Info(SelDistrib - 1).ParamB.Value = DistribInfo.ParamB
            Distribution.Info(SelDistrib - 1).ParamB.Value = DistribInfo.ParamBInfo.Value
            Distribution.Info(SelDistrib - 1).ParamB.Name = DistribInfo.ParamBInfo.Name
            Distribution.Info(SelDistrib - 1).ParamB.Type = DistribInfo.ParamBInfo.Type 'Added 12Oct22
            Distribution.Info(SelDistrib - 1).ParamB.Symbol = DistribInfo.ParamBInfo.Symbol
            Distribution.Info(SelDistrib - 1).ParamB.NumberType = DistribInfo.ParamBInfo.NumberType
            Distribution.Info(SelDistrib - 1).ParamB.Minimum = DistribInfo.ParamBInfo.Minimum
            Distribution.Info(SelDistrib - 1).ParamB.Maximum = DistribInfo.ParamBInfo.Maximum
            Distribution.Info(SelDistrib - 1).ParamB.Description = DistribInfo.ParamBInfo.Description
            Distribution.Info(SelDistrib - 1).ParamB.AdjustMin = DistribInfo.ParamBInfo.AdjustMin
            Distribution.Info(SelDistrib - 1).ParamB.AdjustMax = DistribInfo.ParamBInfo.AdjustMax
            If Distribution.Info(SelDistrib - 1).NParams > 2 Then
                'Distribution.Info(SelDistrib - 1).ParamC.Value = DistribInfo.ParamC
                Distribution.Info(SelDistrib - 1).ParamC.Value = DistribInfo.ParamCInfo.Value
                Distribution.Info(SelDistrib - 1).ParamC.Name = DistribInfo.ParamCInfo.Name
                Distribution.Info(SelDistrib - 1).ParamC.Type = DistribInfo.ParamCInfo.Type 'Added 12Oct22
                Distribution.Info(SelDistrib - 1).ParamC.Symbol = DistribInfo.ParamCInfo.Symbol
                Distribution.Info(SelDistrib - 1).ParamC.NumberType = DistribInfo.ParamCInfo.NumberType
                Distribution.Info(SelDistrib - 1).ParamC.Minimum = DistribInfo.ParamCInfo.Minimum
                Distribution.Info(SelDistrib - 1).ParamC.Maximum = DistribInfo.ParamCInfo.Maximum
                Distribution.Info(SelDistrib - 1).ParamC.Description = DistribInfo.ParamCInfo.Description
                Distribution.Info(SelDistrib - 1).ParamC.AdjustMin = DistribInfo.ParamCInfo.AdjustMin
                Distribution.Info(SelDistrib - 1).ParamC.AdjustMax = DistribInfo.ParamCInfo.AdjustMax
                If Distribution.Info(SelDistrib - 1).NParams > 3 Then
                    'Distribution.Info(SelDistrib - 1).ParamD.Value = DistribInfo.ParamD
                    Distribution.Info(SelDistrib - 1).ParamD.Value = DistribInfo.ParamDInfo.Value
                    Distribution.Info(SelDistrib - 1).ParamD.Name = DistribInfo.ParamDInfo.Name
                    Distribution.Info(SelDistrib - 1).ParamD.Type = DistribInfo.ParamDInfo.Type 'Added 12Oct22
                    Distribution.Info(SelDistrib - 1).ParamD.Symbol = DistribInfo.ParamDInfo.Symbol
                    Distribution.Info(SelDistrib - 1).ParamD.NumberType = DistribInfo.ParamDInfo.NumberType
                    Distribution.Info(SelDistrib - 1).ParamD.Minimum = DistribInfo.ParamDInfo.Minimum
                    Distribution.Info(SelDistrib - 1).ParamD.Maximum = DistribInfo.ParamDInfo.Maximum
                    Distribution.Info(SelDistrib - 1).ParamD.Description = DistribInfo.ParamDInfo.Description
                    Distribution.Info(SelDistrib - 1).ParamD.AdjustMin = DistribInfo.ParamDInfo.AdjustMin
                    Distribution.Info(SelDistrib - 1).ParamD.AdjustMax = DistribInfo.ParamDInfo.AdjustMax
                    If Distribution.Info(SelDistrib - 1).NParams > 4 Then
                        'Distribution.Info(SelDistrib - 1).ParamE.Value = DistribInfo.ParamE
                        Distribution.Info(SelDistrib - 1).ParamE.Value = DistribInfo.ParamEInfo.Value
                        Distribution.Info(SelDistrib - 1).ParamE.Name = DistribInfo.ParamEInfo.Name
                        Distribution.Info(SelDistrib - 1).ParamE.Type = DistribInfo.ParamEInfo.Type 'Added 12Oct22
                        Distribution.Info(SelDistrib - 1).ParamE.Symbol = DistribInfo.ParamEInfo.Symbol
                        Distribution.Info(SelDistrib - 1).ParamE.NumberType = DistribInfo.ParamEInfo.NumberType
                        Distribution.Info(SelDistrib - 1).ParamE.Minimum = DistribInfo.ParamEInfo.Minimum
                        Distribution.Info(SelDistrib - 1).ParamE.Maximum = DistribInfo.ParamEInfo.Maximum
                        Distribution.Info(SelDistrib - 1).ParamE.Description = DistribInfo.ParamEInfo.Description
                        Distribution.Info(SelDistrib - 1).ParamE.AdjustMin = DistribInfo.ParamEInfo.AdjustMin
                        Distribution.Info(SelDistrib - 1).ParamE.AdjustMax = DistribInfo.ParamEInfo.AdjustMax
                    End If
                End If
            End If
        End If


        'Update the Distribution tab: ------------------------------------------------------------
        txtDistName.Text = Distribution.Info(SelDistrib - 1).Name
        txtContinuity.Text = Distribution.Info(SelDistrib - 1).Continuity
        txtNParams.Text = Distribution.Info(SelDistrib - 1).NParams
        dgvParams.RowCount = Distribution.Info(SelDistrib - 1).NParams

        txtRVName.Text = Distribution.Info(SelDistrib - 1).RVName
        txtRVDescr.Text = Distribution.Info(SelDistrib - 1).RVDescription
        txtRVMeasurement.Text = Distribution.Info(SelDistrib - 1).RVMeasurement
        txtRVUnits.Text = Distribution.Info(SelDistrib - 1).RVUnits
        txtRVUnitsAbbrev.Text = Distribution.Info(SelDistrib - 1).RVAbbrevUnits

        If Distribution.Info(SelDistrib - 1).NParams > 0 Then
            dgvParams.Rows(0).Cells(0).Value = Distribution.Info(SelDistrib - 1).ParamA.Name
            dgvParams.Rows(0).Cells(1).Value = NameToSymbol(Distribution.Info(SelDistrib - 1).ParamA.Symbol)
            dgvParams.Rows(0).Cells(2).Value = Distribution.Info(SelDistrib - 1).ParamA.Value
            dgvParams.Rows(0).Cells(3).Value = Distribution.Info(SelDistrib - 1).ParamA.Type
            dgvParams.Rows(0).Cells(4).Value = Distribution.Info(SelDistrib - 1).ParamA.NumberType
            dgvParams.Rows(0).Cells(5).Value = Distribution.Info(SelDistrib - 1).ParamA.Minimum
            dgvParams.Rows(0).Cells(6).Value = Distribution.Info(SelDistrib - 1).ParamA.Maximum
            dgvParams.Rows(0).Cells(7).Value = Distribution.Info(SelDistrib - 1).ParamA.Description
            If Distribution.Info(SelDistrib - 1).NParams > 1 Then
                dgvParams.Rows(1).Cells(0).Value = Distribution.Info(SelDistrib - 1).ParamB.Name
                dgvParams.Rows(1).Cells(1).Value = NameToSymbol(Distribution.Info(SelDistrib - 1).ParamB.Symbol)
                dgvParams.Rows(1).Cells(2).Value = Distribution.Info(SelDistrib - 1).ParamB.Value
                dgvParams.Rows(1).Cells(3).Value = Distribution.Info(SelDistrib - 1).ParamB.Type
                dgvParams.Rows(1).Cells(4).Value = Distribution.Info(SelDistrib - 1).ParamB.NumberType
                dgvParams.Rows(1).Cells(5).Value = Distribution.Info(SelDistrib - 1).ParamB.Minimum
                dgvParams.Rows(1).Cells(6).Value = Distribution.Info(SelDistrib - 1).ParamB.Maximum
                dgvParams.Rows(1).Cells(7).Value = Distribution.Info(SelDistrib - 1).ParamB.Description
                If Distribution.Info(SelDistrib - 1).NParams > 2 Then
                    dgvParams.Rows(2).Cells(0).Value = Distribution.Info(SelDistrib - 1).ParamC.Name
                    dgvParams.Rows(2).Cells(1).Value = NameToSymbol(Distribution.Info(SelDistrib - 1).ParamC.Symbol)
                    dgvParams.Rows(2).Cells(2).Value = Distribution.Info(SelDistrib - 1).ParamC.Value
                    dgvParams.Rows(2).Cells(3).Value = Distribution.Info(SelDistrib - 1).ParamC.Type
                    dgvParams.Rows(2).Cells(4).Value = Distribution.Info(SelDistrib - 1).ParamC.NumberType
                    dgvParams.Rows(2).Cells(5).Value = Distribution.Info(SelDistrib - 1).ParamC.Minimum
                    dgvParams.Rows(2).Cells(6).Value = Distribution.Info(SelDistrib - 1).ParamC.Maximum
                    dgvParams.Rows(2).Cells(7).Value = Distribution.Info(SelDistrib - 1).ParamC.Description
                    If Distribution.Info(SelDistrib - 1).NParams > 3 Then
                        dgvParams.Rows(3).Cells(0).Value = Distribution.Info(SelDistrib - 1).ParamD.Name
                        dgvParams.Rows(3).Cells(1).Value = NameToSymbol(Distribution.Info(SelDistrib - 1).ParamD.Symbol)
                        dgvParams.Rows(3).Cells(2).Value = Distribution.Info(SelDistrib - 1).ParamD.Value
                        dgvParams.Rows(3).Cells(3).Value = Distribution.Info(SelDistrib - 1).ParamD.Type
                        dgvParams.Rows(3).Cells(4).Value = Distribution.Info(SelDistrib - 1).ParamD.NumberType
                        dgvParams.Rows(3).Cells(5).Value = Distribution.Info(SelDistrib - 1).ParamD.Minimum
                        dgvParams.Rows(3).Cells(6).Value = Distribution.Info(SelDistrib - 1).ParamD.Maximum
                        dgvParams.Rows(3).Cells(7).Value = Distribution.Info(SelDistrib - 1).ParamD.Description
                        If Distribution.Info(SelDistrib - 1).NParams > 4 Then
                            dgvParams.Rows(4).Cells(0).Value = Distribution.Info(SelDistrib - 1).ParamE.Name
                            dgvParams.Rows(4).Cells(1).Value = NameToSymbol(Distribution.Info(SelDistrib - 1).ParamE.Symbol)
                            dgvParams.Rows(4).Cells(2).Value = Distribution.Info(SelDistrib - 1).ParamE.Value
                            dgvParams.Rows(4).Cells(3).Value = Distribution.Info(SelDistrib - 1).ParamE.Type
                            dgvParams.Rows(4).Cells(4).Value = Distribution.Info(SelDistrib - 1).ParamE.NumberType
                            dgvParams.Rows(4).Cells(5).Value = Distribution.Info(SelDistrib - 1).ParamE.Minimum
                            dgvParams.Rows(4).Cells(6).Value = Distribution.Info(SelDistrib - 1).ParamE.Maximum
                            dgvParams.Rows(4).Cells(7).Value = Distribution.Info(SelDistrib - 1).ParamE.Description
                            If Distribution.Info(SelDistrib - 1).NParams > 5 Then
                                'Too many parameters!
                            End If
                        End If
                    End If
                End If
            End If
        End If

        dgvParams.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        dgvParams.AutoResizeColumns()

        txtDefFrom.Text = Distribution.Info(SelDistrib - 1).RangeMin
        txtDefTo.Text = Distribution.Info(SelDistrib - 1).RangeMax

        txtSuffix.Text = Distribution.Info(SelDistrib - 1).Suffix
        chkUpdateLabel.Checked = Distribution.Info(SelDistrib - 1).AutoUpdateLabels

        cmbDefMkrFill.SelectedIndex = cmbDefMkrFill.FindStringExact(Distribution.Info(SelDistrib - 1).Display.MarkerFill)
        txtDefMkrColor.BackColor = Distribution.Info(SelDistrib - 1).Display.MarkerColor
        txtDefBorderColor.BackColor = Distribution.Info(SelDistrib - 1).Display.BorderColor
        txtDefBorderWidth.Text = Distribution.Info(SelDistrib - 1).Display.BorderWidth
        cmbDefMkrStyle.SelectedIndex = cmbDefMkrStyle.FindStringExact(Distribution.Info(SelDistrib - 1).Display.MarkerStyle)
        txtDefMkrSize.Text = Distribution.Info(SelDistrib - 1).Display.MarkerSize
        txtDefMkrStep.Text = Distribution.Info(SelDistrib - 1).Display.MarkerStep
        txtDefLineColor.BackColor = Distribution.Info(SelDistrib - 1).Display.LineColor
        txtDefLineWidth.Text = Distribution.Info(SelDistrib - 1).Display.LineWidth


        If Distribution.Info(SelDistrib - 1).Continuity = "Continuous" Then
            'Update Continuous sampling settings:
            txtMinValue.Text = Distribution.ContSampling.Minimum
            chkLockSampMin.Checked = Distribution.ContSampling.MinLock
            txtMaxValue.Text = Distribution.ContSampling.Maximum
            chkLockSampMax.Checked = Distribution.ContSampling.MaxLock
            txtSampleInt.Text = Distribution.ContSampling.Interval
            chkLockSampInt.Checked = Distribution.ContSampling.IntervalLock
            txtNSamples.Text = Distribution.ContSampling.NSamples
            chkLockNSamples.Checked = Distribution.ContSampling.NSamplesLock
            txtXAxisLabel.Text = Distribution.ContSampling.Label
            txtXAxisUnits.Text = Distribution.ContSampling.Units

            'UPDATE: leave the continuous and disctrete groupboxes enabled.
            'GroupBox2.Enabled = True
            'GroupBox9.Enabled = False

        ElseIf Distribution.Info(SelDistrib - 1).Continuity = "Discrete" Then
            'Update Discrete sampling settings:
            txtDiscMin.Text = Distribution.DiscSampling.Minimum
            txtDiscMax.Text = Distribution.DiscSampling.Maximum
            txtDiscXAxisLabel.Text = Distribution.DiscSampling.Label
            txtDiscXAxisUnits.Text = Distribution.DiscSampling.Units
            txtDiscXAxisDescr.Text = Distribution.DiscSampling.Description
            'UPDATE: leave the continuous and disctrete groupboxes enabled.
            'GroupBox9.Enabled = True
            'GroupBox2.Enabled = False
        Else
            'Not a valid continuity string.
        End If

        'Update the Fields tab: ------------------------------------------------------------------
        dgvFields.Rows.Clear()
        If Distribution.Info(SelDistrib - 1).Continuity = "Continuous" Then
            'dgvFields.Rows.Add(Distribution.Info(SelDistrib - 1).ValueInfo.Name, Distribution.Info(SelDistrib - 1).ValueInfo.Valid, Distribution.Info(SelDistrib - 1).ValueInfo.Generate.ToString, Distribution.Info(SelDistrib - 1).ValueInfo.NumType, Distribution.Info(SelDistrib - 1).ValueInfo.Format, Distribution.Info(SelDistrib - 1).ValueInfo.Alignment, Distribution.Info(SelDistrib - 1).ValueInfo.ValueLabel, Distribution.Info(SelDistrib - 1).ValueInfo.Units, Distribution.Info(SelDistrib - 1).ValueInfo.LabelPrefix, Distribution.Info(SelDistrib - 1).ValueInfo.SeriesLabel, Distribution.Info(SelDistrib - 1).ValueInfo.Description)
            dgvFields.Rows.Add(Distribution.Info(SelDistrib - 1).PdfInfo.Name, Distribution.Info(SelDistrib - 1).PdfInfo.Valid, Distribution.Info(SelDistrib - 1).PdfInfo.Generate.ToString, Distribution.Info(SelDistrib - 1).PdfInfo.NumType, Distribution.Info(SelDistrib - 1).PdfInfo.Format, Distribution.Info(SelDistrib - 1).PdfInfo.Alignment, Distribution.Info(SelDistrib - 1).PdfInfo.ValueLabel, Distribution.Info(SelDistrib - 1).PdfInfo.Units, Distribution.Info(SelDistrib - 1).PdfInfo.LabelPrefix, Distribution.Info(SelDistrib - 1).PdfInfo.SeriesLabel, Distribution.Info(SelDistrib - 1).PdfInfo.Description)
            dgvFields.Rows.Add(Distribution.Info(SelDistrib - 1).PdfLnInfo.Name, Distribution.Info(SelDistrib - 1).PdfLnInfo.Valid, Distribution.Info(SelDistrib - 1).PdfLnInfo.Generate.ToString, Distribution.Info(SelDistrib - 1).PdfLnInfo.NumType, Distribution.Info(SelDistrib - 1).PdfLnInfo.Format, Distribution.Info(SelDistrib - 1).PdfLnInfo.Alignment, Distribution.Info(SelDistrib - 1).PdfLnInfo.ValueLabel, Distribution.Info(SelDistrib - 1).PdfLnInfo.Units, Distribution.Info(SelDistrib - 1).PdfLnInfo.LabelPrefix, Distribution.Info(SelDistrib - 1).PdfLnInfo.SeriesLabel, Distribution.Info(SelDistrib - 1).PdfLnInfo.Description)
            dgvFields.Rows.Add(Distribution.Info(SelDistrib - 1).CdfInfo.Name, Distribution.Info(SelDistrib - 1).CdfInfo.Valid, Distribution.Info(SelDistrib - 1).CdfInfo.Generate.ToString, Distribution.Info(SelDistrib - 1).CdfInfo.NumType, Distribution.Info(SelDistrib - 1).CdfInfo.Format, Distribution.Info(SelDistrib - 1).CdfInfo.Alignment, Distribution.Info(SelDistrib - 1).CdfInfo.ValueLabel, Distribution.Info(SelDistrib - 1).CdfInfo.Units, Distribution.Info(SelDistrib - 1).CdfInfo.LabelPrefix, Distribution.Info(SelDistrib - 1).CdfInfo.SeriesLabel, Distribution.Info(SelDistrib - 1).CdfInfo.Description)

            dgvFields.Rows.Add(Distribution.Info(SelDistrib - 1).RevCdfInfo.Name, Distribution.Info(SelDistrib - 1).RevCdfInfo.Valid, Distribution.Info(SelDistrib - 1).RevCdfInfo.Generate.ToString, Distribution.Info(SelDistrib - 1).RevCdfInfo.NumType, Distribution.Info(SelDistrib - 1).RevCdfInfo.Format, Distribution.Info(SelDistrib - 1).RevCdfInfo.Alignment, Distribution.Info(SelDistrib - 1).RevCdfInfo.ValueLabel, Distribution.Info(SelDistrib - 1).RevCdfInfo.Units, Distribution.Info(SelDistrib - 1).RevCdfInfo.LabelPrefix, Distribution.Info(SelDistrib - 1).RevCdfInfo.SeriesLabel, Distribution.Info(SelDistrib - 1).RevCdfInfo.Description)

            'dgvFields.Rows.Add(Distribution.Info(SelDistrib - 1).ProbabilityInfo.Name, Distribution.Info(SelDistrib - 1).ProbabilityInfo.Valid, Distribution.Info(SelDistrib - 1).ProbabilityInfo.Generate.ToString, Distribution.Info(SelDistrib - 1).ProbabilityInfo.NumType, Distribution.Info(SelDistrib - 1).ProbabilityInfo.Format, Distribution.Info(SelDistrib - 1).ProbabilityInfo.Alignment, Distribution.Info(SelDistrib - 1).ProbabilityInfo.ValueLabel, Distribution.Info(SelDistrib - 1).ProbabilityInfo.Units, Distribution.Info(SelDistrib - 1).ProbabilityInfo.LabelPrefix, Distribution.Info(SelDistrib - 1).ProbabilityInfo.SeriesLabel, Distribution.Info(SelDistrib - 1).ProbabilityInfo.Description)
            dgvFields.Rows.Add(Distribution.Info(SelDistrib - 1).InvCdfInfo.Name, Distribution.Info(SelDistrib - 1).InvCdfInfo.Valid, Distribution.Info(SelDistrib - 1).InvCdfInfo.Generate.ToString, Distribution.Info(SelDistrib - 1).InvCdfInfo.NumType, Distribution.Info(SelDistrib - 1).InvCdfInfo.Format, Distribution.Info(SelDistrib - 1).InvCdfInfo.Alignment, Distribution.Info(SelDistrib - 1).InvCdfInfo.ValueLabel, Distribution.Info(SelDistrib - 1).InvCdfInfo.Units, Distribution.Info(SelDistrib - 1).InvCdfInfo.LabelPrefix, Distribution.Info(SelDistrib - 1).InvCdfInfo.SeriesLabel, Distribution.Info(SelDistrib - 1).InvCdfInfo.Description)

            dgvFields.Rows.Add(Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Name, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Valid, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Generate.ToString, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.NumType, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Format, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Alignment, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.ValueLabel, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Units, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.LabelPrefix, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.SeriesLabel, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Description)

            'Add PDF display settings:
            'dgvFields.Rows(1).Cells(11).Value = Distribution.Info(SelDistrib - 1).PdfInfo.Display.MarkerFill
            dgvFields.Rows(0).Cells(11).Value = Distribution.Info(SelDistrib - 1).PdfInfo.Display.MarkerFill
            dgvFields.Rows(0).Cells(12).Style.BackColor = Distribution.Info(SelDistrib - 1).PdfInfo.Display.MarkerColor
            dgvFields.Rows(0).Cells(13).Style.BackColor = Distribution.Info(SelDistrib - 1).PdfInfo.Display.BorderColor
            dgvFields.Rows(0).Cells(14).Value = Distribution.Info(SelDistrib - 1).PdfInfo.Display.BorderWidth
            dgvFields.Rows(0).Cells(15).Value = Distribution.Info(SelDistrib - 1).PdfInfo.Display.MarkerStyle
            dgvFields.Rows(0).Cells(16).Value = Distribution.Info(SelDistrib - 1).PdfInfo.Display.MarkerSize
            dgvFields.Rows(0).Cells(17).Value = Distribution.Info(SelDistrib - 1).PdfInfo.Display.MarkerStep
            dgvFields.Rows(0).Cells(18).Style.BackColor = Distribution.Info(SelDistrib - 1).PdfInfo.Display.LineColor
            dgvFields.Rows(0).Cells(19).Value = Distribution.Info(SelDistrib - 1).PdfInfo.Display.LineWidth

            'Add PDFLn display settings:
            dgvFields.Rows(1).Cells(11).Value = Distribution.Info(SelDistrib - 1).PdfLnInfo.Display.MarkerFill
            dgvFields.Rows(1).Cells(12).Style.BackColor = Distribution.Info(SelDistrib - 1).PdfLnInfo.Display.MarkerColor
            dgvFields.Rows(1).Cells(13).Style.BackColor = Distribution.Info(SelDistrib - 1).PdfLnInfo.Display.BorderColor
            dgvFields.Rows(1).Cells(14).Value = Distribution.Info(SelDistrib - 1).PdfLnInfo.Display.BorderWidth
            dgvFields.Rows(1).Cells(15).Value = Distribution.Info(SelDistrib - 1).PdfLnInfo.Display.MarkerStyle
            dgvFields.Rows(1).Cells(16).Value = Distribution.Info(SelDistrib - 1).PdfLnInfo.Display.MarkerSize
            dgvFields.Rows(1).Cells(17).Value = Distribution.Info(SelDistrib - 1).PdfLnInfo.Display.MarkerStep
            dgvFields.Rows(1).Cells(18).Style.BackColor = Distribution.Info(SelDistrib - 1).PdfLnInfo.Display.LineColor
            dgvFields.Rows(1).Cells(19).Value = Distribution.Info(SelDistrib - 1).PdfLnInfo.Display.LineWidth

            'Add CDF display settings:
            dgvFields.Rows(2).Cells(11).Value = Distribution.Info(SelDistrib - 1).CdfInfo.Display.MarkerFill
            dgvFields.Rows(2).Cells(12).Style.BackColor = Distribution.Info(SelDistrib - 1).CdfInfo.Display.MarkerColor
            dgvFields.Rows(2).Cells(13).Style.BackColor = Distribution.Info(SelDistrib - 1).CdfInfo.Display.BorderColor
            dgvFields.Rows(2).Cells(14).Value = Distribution.Info(SelDistrib - 1).CdfInfo.Display.BorderWidth
            dgvFields.Rows(2).Cells(15).Value = Distribution.Info(SelDistrib - 1).CdfInfo.Display.MarkerStyle
            dgvFields.Rows(2).Cells(16).Value = Distribution.Info(SelDistrib - 1).CdfInfo.Display.MarkerSize
            dgvFields.Rows(2).Cells(17).Value = Distribution.Info(SelDistrib - 1).CdfInfo.Display.MarkerStep
            dgvFields.Rows(2).Cells(18).Style.BackColor = Distribution.Info(SelDistrib - 1).CdfInfo.Display.LineColor
            dgvFields.Rows(2).Cells(19).Value = Distribution.Info(SelDistrib - 1).CdfInfo.Display.LineWidth

            'Add RevCDF display settings:
            dgvFields.Rows(3).Cells(11).Value = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.MarkerFill
            dgvFields.Rows(3).Cells(12).Style.BackColor = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.MarkerColor
            dgvFields.Rows(3).Cells(13).Style.BackColor = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.BorderColor
            dgvFields.Rows(3).Cells(14).Value = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.BorderWidth
            dgvFields.Rows(3).Cells(15).Value = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.MarkerStyle
            dgvFields.Rows(3).Cells(16).Value = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.MarkerSize
            dgvFields.Rows(3).Cells(17).Value = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.MarkerStep
            dgvFields.Rows(3).Cells(18).Style.BackColor = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.LineColor
            dgvFields.Rows(3).Cells(19).Value = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.LineWidth

            ''Add InvCDF display settings:
            'dgvFields.Rows(3).Cells(11).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerFill
            'dgvFields.Rows(3).Cells(12).Style.BackColor = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerColor
            'dgvFields.Rows(3).Cells(13).Style.BackColor = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.BorderColor
            'dgvFields.Rows(3).Cells(14).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.BorderWidth
            'dgvFields.Rows(3).Cells(15).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerStyle
            'dgvFields.Rows(3).Cells(16).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerSize
            'dgvFields.Rows(3).Cells(17).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerStep
            'dgvFields.Rows(3).Cells(18).Style.BackColor = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.LineColor
            'dgvFields.Rows(3).Cells(19).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.LineWidth

            'Add InvCDF display settings:
            dgvFields.Rows(4).Cells(11).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerFill
            dgvFields.Rows(4).Cells(12).Style.BackColor = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerColor
            dgvFields.Rows(4).Cells(13).Style.BackColor = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.BorderColor
            dgvFields.Rows(4).Cells(14).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.BorderWidth
            dgvFields.Rows(4).Cells(15).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerStyle
            dgvFields.Rows(4).Cells(16).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerSize
            dgvFields.Rows(4).Cells(17).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerStep
            dgvFields.Rows(4).Cells(18).Style.BackColor = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.LineColor
            dgvFields.Rows(4).Cells(19).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.LineWidth

            'Add InvRevCDF display settings:
            dgvFields.Rows(5).Cells(11).Value = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.MarkerFill
            dgvFields.Rows(5).Cells(12).Style.BackColor = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.MarkerColor
            dgvFields.Rows(5).Cells(13).Style.BackColor = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.BorderColor
            dgvFields.Rows(5).Cells(14).Value = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.BorderWidth
            dgvFields.Rows(5).Cells(15).Value = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.MarkerStyle
            dgvFields.Rows(5).Cells(16).Value = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.MarkerSize
            dgvFields.Rows(5).Cells(17).Value = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.MarkerStep
            dgvFields.Rows(5).Cells(18).Style.BackColor = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.LineColor
            dgvFields.Rows(5).Cells(19).Value = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.LineWidth

            'dgvFields.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)
            dgvFields.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            dgvFields.AutoResizeColumns()

            UpdateSuffix() 'NOTE: This updates dgvFields!!!

        ElseIf Distribution.Info(SelDistrib - 1).Continuity = "Discrete" Then
            'dgvFields.Rows.Add(Distribution.Info(SelDistrib - 1).ValueInfo.Name, Distribution.Info(SelDistrib - 1).ValueInfo.Valid, Distribution.Info(SelDistrib - 1).ValueInfo.Generate.ToString, Distribution.Info(SelDistrib - 1).ValueInfo.NumType, Distribution.Info(SelDistrib - 1).ValueInfo.Format, Distribution.Info(SelDistrib - 1).ValueInfo.Alignment, Distribution.Info(SelDistrib - 1).ValueInfo.ValueLabel, Distribution.Info(SelDistrib - 1).ValueInfo.Units, Distribution.Info(SelDistrib - 1).ValueInfo.LabelPrefix, Distribution.Info(SelDistrib - 1).ValueInfo.SeriesLabel, Distribution.Info(SelDistrib - 1).ValueInfo.Description)
            dgvFields.Rows.Add(Distribution.Info(SelDistrib - 1).PmfInfo.Name, Distribution.Info(SelDistrib - 1).PmfInfo.Valid, Distribution.Info(SelDistrib - 1).PmfInfo.Generate.ToString, Distribution.Info(SelDistrib - 1).PmfInfo.NumType, Distribution.Info(SelDistrib - 1).PmfInfo.Format, Distribution.Info(SelDistrib - 1).PmfInfo.Alignment, Distribution.Info(SelDistrib - 1).PmfInfo.ValueLabel, Distribution.Info(SelDistrib - 1).PmfInfo.Units, Distribution.Info(SelDistrib - 1).PmfInfo.LabelPrefix, Distribution.Info(SelDistrib - 1).PmfInfo.SeriesLabel, Distribution.Info(SelDistrib - 1).PmfInfo.Description)
            dgvFields.Rows.Add(Distribution.Info(SelDistrib - 1).PmfLnInfo.Name, Distribution.Info(SelDistrib - 1).PmfLnInfo.Valid, Distribution.Info(SelDistrib - 1).PmfLnInfo.Generate.ToString, Distribution.Info(SelDistrib - 1).PmfLnInfo.NumType, Distribution.Info(SelDistrib - 1).PmfLnInfo.Format, Distribution.Info(SelDistrib - 1).PmfLnInfo.Alignment, Distribution.Info(SelDistrib - 1).PmfLnInfo.ValueLabel, Distribution.Info(SelDistrib - 1).PmfLnInfo.Units, Distribution.Info(SelDistrib - 1).PmfLnInfo.LabelPrefix, Distribution.Info(SelDistrib - 1).PmfLnInfo.SeriesLabel, Distribution.Info(SelDistrib - 1).PmfLnInfo.Description)
            dgvFields.Rows.Add(Distribution.Info(SelDistrib - 1).CdfInfo.Name, Distribution.Info(SelDistrib - 1).CdfInfo.Valid, Distribution.Info(SelDistrib - 1).CdfInfo.Generate.ToString, Distribution.Info(SelDistrib - 1).CdfInfo.NumType, Distribution.Info(SelDistrib - 1).CdfInfo.Format, Distribution.Info(SelDistrib - 1).CdfInfo.Alignment, Distribution.Info(SelDistrib - 1).CdfInfo.ValueLabel, Distribution.Info(SelDistrib - 1).CdfInfo.Units, Distribution.Info(SelDistrib - 1).CdfInfo.LabelPrefix, Distribution.Info(SelDistrib - 1).CdfInfo.SeriesLabel, Distribution.Info(SelDistrib - 1).CdfInfo.Description)

            dgvFields.Rows.Add(Distribution.Info(SelDistrib - 1).RevCdfInfo.Name, Distribution.Info(SelDistrib - 1).RevCdfInfo.Valid, Distribution.Info(SelDistrib - 1).RevCdfInfo.Generate.ToString, Distribution.Info(SelDistrib - 1).RevCdfInfo.NumType, Distribution.Info(SelDistrib - 1).RevCdfInfo.Format, Distribution.Info(SelDistrib - 1).RevCdfInfo.Alignment, Distribution.Info(SelDistrib - 1).RevCdfInfo.ValueLabel, Distribution.Info(SelDistrib - 1).RevCdfInfo.Units, Distribution.Info(SelDistrib - 1).RevCdfInfo.LabelPrefix, Distribution.Info(SelDistrib - 1).RevCdfInfo.SeriesLabel, Distribution.Info(SelDistrib - 1).RevCdfInfo.Description)

            dgvFields.Rows.Add(Distribution.Info(SelDistrib - 1).InvCdfInfo.Name, Distribution.Info(SelDistrib - 1).InvCdfInfo.Valid, Distribution.Info(SelDistrib - 1).InvCdfInfo.Generate.ToString, Distribution.Info(SelDistrib - 1).InvCdfInfo.NumType, Distribution.Info(SelDistrib - 1).InvCdfInfo.Format, Distribution.Info(SelDistrib - 1).InvCdfInfo.Alignment, Distribution.Info(SelDistrib - 1).InvCdfInfo.ValueLabel, Distribution.Info(SelDistrib - 1).InvCdfInfo.Units, Distribution.Info(SelDistrib - 1).InvCdfInfo.LabelPrefix, Distribution.Info(SelDistrib - 1).InvCdfInfo.SeriesLabel, Distribution.Info(SelDistrib - 1).InvCdfInfo.Description)

            dgvFields.Rows.Add(Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Name, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Valid, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Generate.ToString, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.NumType, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Format, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Alignment, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.ValueLabel, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Units, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.LabelPrefix, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.SeriesLabel, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Description)

            'Add PMF display settings:
            dgvFields.Rows(0).Cells(11).Value = Distribution.Info(SelDistrib - 1).PmfInfo.Display.MarkerFill
            dgvFields.Rows(0).Cells(12).Style.BackColor = Distribution.Info(SelDistrib - 1).PmfInfo.Display.MarkerColor
            dgvFields.Rows(0).Cells(13).Style.BackColor = Distribution.Info(SelDistrib - 1).PmfInfo.Display.BorderColor
            dgvFields.Rows(0).Cells(14).Value = Distribution.Info(SelDistrib - 1).PmfInfo.Display.BorderWidth
            dgvFields.Rows(0).Cells(15).Value = Distribution.Info(SelDistrib - 1).PmfInfo.Display.MarkerStyle
            dgvFields.Rows(0).Cells(16).Value = Distribution.Info(SelDistrib - 1).PmfInfo.Display.MarkerSize
            dgvFields.Rows(0).Cells(17).Value = Distribution.Info(SelDistrib - 1).PmfInfo.Display.MarkerStep
            dgvFields.Rows(0).Cells(18).Style.BackColor = Distribution.Info(SelDistrib - 1).PmfInfo.Display.LineColor
            dgvFields.Rows(0).Cells(19).Value = Distribution.Info(SelDistrib - 1).PmfInfo.Display.LineWidth

            'Add PMFLn display settings:
            dgvFields.Rows(1).Cells(11).Value = Distribution.Info(SelDistrib - 1).PmfLnInfo.Display.MarkerFill
            dgvFields.Rows(1).Cells(12).Style.BackColor = Distribution.Info(SelDistrib - 1).PmfLnInfo.Display.MarkerColor
            dgvFields.Rows(1).Cells(13).Style.BackColor = Distribution.Info(SelDistrib - 1).PmfLnInfo.Display.BorderColor
            dgvFields.Rows(1).Cells(14).Value = Distribution.Info(SelDistrib - 1).PmfLnInfo.Display.BorderWidth
            dgvFields.Rows(1).Cells(15).Value = Distribution.Info(SelDistrib - 1).PmfLnInfo.Display.MarkerStyle
            dgvFields.Rows(1).Cells(16).Value = Distribution.Info(SelDistrib - 1).PmfLnInfo.Display.MarkerSize
            dgvFields.Rows(1).Cells(17).Value = Distribution.Info(SelDistrib - 1).PmfLnInfo.Display.MarkerStep
            dgvFields.Rows(1).Cells(18).Style.BackColor = Distribution.Info(SelDistrib - 1).PmfLnInfo.Display.LineColor
            dgvFields.Rows(1).Cells(19).Value = Distribution.Info(SelDistrib - 1).PmfLnInfo.Display.LineWidth

            'Add CDF display settings:
            dgvFields.Rows(2).Cells(11).Value = Distribution.Info(SelDistrib - 1).CdfInfo.Display.MarkerFill
            dgvFields.Rows(2).Cells(12).Style.BackColor = Distribution.Info(SelDistrib - 1).CdfInfo.Display.MarkerColor
            dgvFields.Rows(2).Cells(13).Style.BackColor = Distribution.Info(SelDistrib - 1).CdfInfo.Display.BorderColor
            dgvFields.Rows(2).Cells(14).Value = Distribution.Info(SelDistrib - 1).CdfInfo.Display.BorderWidth
            dgvFields.Rows(2).Cells(15).Value = Distribution.Info(SelDistrib - 1).CdfInfo.Display.MarkerStyle
            dgvFields.Rows(2).Cells(16).Value = Distribution.Info(SelDistrib - 1).CdfInfo.Display.MarkerSize
            dgvFields.Rows(2).Cells(17).Value = Distribution.Info(SelDistrib - 1).CdfInfo.Display.MarkerStep
            dgvFields.Rows(2).Cells(18).Style.BackColor = Distribution.Info(SelDistrib - 1).CdfInfo.Display.LineColor
            dgvFields.Rows(2).Cells(19).Value = Distribution.Info(SelDistrib - 1).CdfInfo.Display.LineWidth



            'Add RevCDF display settings:
            dgvFields.Rows(3).Cells(11).Value = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.MarkerFill
            dgvFields.Rows(3).Cells(12).Style.BackColor = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.MarkerColor
            dgvFields.Rows(3).Cells(13).Style.BackColor = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.BorderColor
            dgvFields.Rows(3).Cells(14).Value = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.BorderWidth
            dgvFields.Rows(3).Cells(15).Value = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.MarkerStyle
            dgvFields.Rows(3).Cells(16).Value = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.MarkerSize
            dgvFields.Rows(3).Cells(17).Value = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.MarkerStep
            dgvFields.Rows(3).Cells(18).Style.BackColor = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.LineColor
            dgvFields.Rows(3).Cells(19).Value = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.LineWidth

            'Add InvCDF display settings:
            dgvFields.Rows(4).Cells(11).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerFill
            dgvFields.Rows(4).Cells(12).Style.BackColor = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerColor
            dgvFields.Rows(4).Cells(13).Style.BackColor = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.BorderColor
            dgvFields.Rows(4).Cells(14).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.BorderWidth
            dgvFields.Rows(4).Cells(15).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerStyle
            dgvFields.Rows(4).Cells(16).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerSize
            dgvFields.Rows(4).Cells(17).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerStep
            dgvFields.Rows(4).Cells(18).Style.BackColor = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.LineColor
            dgvFields.Rows(4).Cells(19).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.LineWidth

            'Add InvRevCDF display settings:
            dgvFields.Rows(5).Cells(11).Value = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.MarkerFill
            dgvFields.Rows(5).Cells(12).Style.BackColor = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.MarkerColor
            dgvFields.Rows(5).Cells(13).Style.BackColor = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.BorderColor
            dgvFields.Rows(5).Cells(14).Value = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.BorderWidth
            dgvFields.Rows(5).Cells(15).Value = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.MarkerStyle
            dgvFields.Rows(5).Cells(16).Value = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.MarkerSize
            dgvFields.Rows(5).Cells(17).Value = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.MarkerStep
            dgvFields.Rows(5).Cells(18).Style.BackColor = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.LineColor
            dgvFields.Rows(5).Cells(19).Value = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.LineWidth


            'dgvFields.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)
            dgvFields.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            dgvFields.AutoResizeColumns()

            UpdateSuffix() 'NOTE: This updates dgvFields!!!

        Else
            'Invalid continuity string.
        End If


    End Sub

    Private Sub btnChart_Click(sender As Object, e As EventArgs) Handles btnChart.Click
        'Open a Chart form.

        If Distribution.NContinuous = 0 And Distribution.NDiscrete = 0 Then
            Message.AddWarning("There are no distributions to chart." & vbCrLf)
        Else
            If Distribution.Data.Tables.Contains("Continuous_Data_Table") Then
                'The Data Table exists
                If Distribution.Data.Tables("Continuous_Data_Table").Rows.Count = 0 Then
                    GenerateData()
                End If
            Else
                    GenerateData()
            End If

            Dim ChartNo As Integer = OpenNewChart()
            ChartList(ChartNo).ContTableName = "Continuous_Data_Table" 'Set this property first. It may be needed when the DataSource property is set.
            ChartList(ChartNo).DiscTableName = "Discrete_Data_Table" 'Set this property first. It may be needed when the DataSource property is set.
            ChartList(ChartNo).DataSource = Distribution
        End If
    End Sub

    Private Sub btnChart2_Click(sender As Object, e As EventArgs) Handles btnChart2.Click
        'Open a Chart form.

        If Distribution.NContinuous = 0 And Distribution.NDiscrete = 0 Then
            Message.AddWarning("There are no distributions to chart." & vbCrLf)
        Else
            If Distribution.Data.Tables.Contains("Continuous_Data_Table") Then
                'The Data Table exists
            Else
                GenerateData()
            End If

            Dim ChartNo As Integer = OpenNewChart()
            ChartList(ChartNo).ContTableName = "Continuous_Data_Table" 'Set this property first. It may be needed when the DataSource property is set.
            ChartList(ChartNo).DiscTableName = "Discrete_Data_Table" 'Set this property first. It may be needed when the DataSource property is set.

            ChartList(ChartNo).DataSource = Distribution
        End If

    End Sub

    Public Function OpenNewChart() As Integer
        'Open a new Chart form, or reuse an existing one if avaialable.
        'The new forms index number in ChartFormList is returned.

        Chart = New frmChart
        If ChartList.Count = 0 Then
            ChartList.Add(Chart)
            ChartList(0).FormNo = 0
            ChartList(0).Show
            Return 0 'The new Chart is at position 0 in ChartList()
        Else
            Dim I As Integer
            Dim ChartAdded As Boolean = False
            For I = 0 To ChartList.Count - 1
                If IsNothing(ChartList(I)) Then
                    ChartList(I) = Chart
                    ChartList(I).FormNo = I
                    ChartList(I).Show
                    ChartAdded = True
                    Return I 'The new Chart is at position I in ChartList()
                    Exit For
                End If
            Next
            If ChartAdded = False Then 'Add a new Chart to ChartList
                Dim ChartNo As Integer
                ChartList.Add(Chart)
                ChartNo = ChartList.Count - 1
                ChartList(ChartNo).FormNo = ChartNo
                ChartList(ChartNo).Show
                Return ChartNo 'The new Chart is at position ChartNo in ChartList()
            End If
        End If
    End Function

    Public Function OpenNewGenerateSamples() As Integer
        'Open a new GenerateSamples form, or reuse an existing one if available.
        'The new forms index number in GenerateSamplesList is returned.

        GenerateSamples = New frmGenerateSamples
        If GenerateSamplesList.Count = 0 Then
            GenerateSamplesList.Add(GenerateSamples)
            GenerateSamplesList(0).FormNo = 0
            GenerateSamplesList(0).Show
            Return 0 'The new Form is at position 0 in GenerateSamplesList ()
        Else
            Dim I As Integer
            Dim FormAdded As Boolean = False
            For I = 0 To GenerateSamplesList.Count - 1
                If IsNothing(GenerateSamplesList(I)) Then
                    GenerateSamplesList(I) = GenerateSamples
                    GenerateSamplesList(I).FormNo = I
                    GenerateSamplesList(I).Show
                    FormAdded = True
                    Return I 'The new Form is at position I in GenerateSamplesList ()
                    Exit For
                End If
            Next
            If FormAdded = False Then 'Add a new Form to GenerateSamplesList 
                Dim FormNo As Integer
                GenerateSamplesList.Add(GenerateSamples)
                FormNo = GenerateSamplesList.Count - 1
                GenerateSamplesList(FormNo).FormNo = FormNo
                GenerateSamplesList(FormNo).Show
                Return FormNo 'The new Form is at position FormNo in GenerateSamplesList ()
            End If
        End If
    End Function

    Public Function OpenNewDistribAnalysis()
        'Open a new DistribAnalysis form, or reuse an existing one if available.
        'The new forms index number in DistribAnalysisList is returned.

        DistribAnalysis = New frmDistribAnalysis
        If DistribAnalysisList.Count = 0 Then
            DistribAnalysisList.Add(DistribAnalysis)
            DistribAnalysisList(0).FormNo = 0
            DistribAnalysisList(0).Show
            Return 0 'The new Form is at position 0 in DistribAnalysisList ()
        Else
            Dim I As Integer
            Dim FormAdded As Boolean = False
            For I = 0 To DistribAnalysisList.Count - 1
                If IsNothing(DistribAnalysisList(I)) Then
                    DistribAnalysisList(I) = DistribAnalysis
                    DistribAnalysisList(I).FormNo = I
                    DistribAnalysisList(I).Show
                    FormAdded = True
                    Return I 'The new Form is at position I in DistribAnalysisList ()
                    Exit For
                End If
            Next
            If FormAdded = False Then 'Add a new Form to DistribAnalysisList 
                Dim FormNo As Integer
                DistribAnalysisList.Add(DistribAnalysis)
                FormNo = DistribAnalysisList.Count - 1
                DistribAnalysisList(FormNo).FormNo = FormNo
                DistribAnalysisList(FormNo).Show
                Return FormNo 'The new Form is at position FormNo in DistribAnalysisList ()
            End If
        End If
    End Function

    Public Function OpenNewTableForm() As Integer
        'Open a new Table form, or reuse an existing one if available.
        'The new forms index number in TableList is returned.

        Table = New frmTable
        If TableList.Count = 0 Then
            TableList.Add(Table)
            TableList(0).FormNo = 0
            TableList(0).Show
            Return 0 'The new Form is at position 0 in TableList ()
        Else
            Dim I As Integer
            Dim FormAdded As Boolean = False
            For I = 0 To TableList.Count - 1
                If IsNothing(TableList(I)) Then
                    TableList(I) = Table
                    TableList(I).FormNo = I
                    TableList(I).Show
                    FormAdded = True
                    Return I 'The new Form is at position I in TableList ()
                    Exit For
                End If
            Next
            If FormAdded = False Then 'Add a new Form to TableList 
                Dim FormNo As Integer
                TableList.Add(Table)
                FormNo = TableList.Count - 1
                TableList(FormNo).FormNo = FormNo
                TableList(FormNo).Show
                Return FormNo 'The new Form is at position FormNo in TableList ()
            End If
        End If
    End Function

    Public Function OpenNewMostLikelyParamsForm() As Integer
        'Open a new MostLikelyParams form, or reuse an existing one if available.
        'The new forms index number in MostLikelyParamsList is returned.

        MostLikelyParams = New frmMostLikelyParams
        If MostLikelyParamsList.Count = 0 Then
            MostLikelyParamsList.Add(MostLikelyParams)
            MostLikelyParamsList(0).FormNo = 0
            MostLikelyParamsList(0).Show
            Return 0 'The new Form is at position 0 in MostLikelyParamsList ()
        Else
            Dim I As Integer
            Dim FormAdded As Boolean = False
            For I = 0 To MostLikelyParamsList.Count - 1
                If IsNothing(MostLikelyParamsList(I)) Then
                    MostLikelyParamsList(I) = MostLikelyParams
                    MostLikelyParamsList(I).FormNo = I
                    MostLikelyParamsList(I).Show
                    FormAdded = True
                    Return I 'The new Form is at position I in MostLikelyParamsList ()
                    Exit For
                End If
            Next
            If FormAdded = False Then 'Add a new Form to MostLikelyParamsList 
                Dim FormNo As Integer
                MostLikelyParamsList.Add(MostLikelyParams)
                FormNo = MostLikelyParamsList.Count - 1
                MostLikelyParamsList(FormNo).FormNo = FormNo
                MostLikelyParamsList(FormNo).Show
                Return FormNo 'The new Form is at position FormNo in MostLikelyParamsList ()
            End If
        End If
    End Function

    Public Function OpenNewProbabilityModelForm() As Integer
        'Open a new ProbabilityModel form, or reuse an existing one if available.
        'The new forms index number in ProbabilityModelList is returned.

        ProbabilityModel = New frmProbabilityModel
        If ProbabilityModelList.Count = 0 Then
            ProbabilityModelList.Add(ProbabilityModel)
            ProbabilityModelList(0).FormNo = 0
            ProbabilityModelList(0).Show
            Return 0 'The new Form is at position 0 in ProbabilityModelList ()
        Else
            Dim I As Integer
            Dim FormAdded As Boolean = False
            For I = 0 To ProbabilityModelList.Count - 1
                If IsNothing(ProbabilityModelList(I)) Then
                    ProbabilityModelList(I) = ProbabilityModel
                    ProbabilityModelList(I).FormNo = I
                    ProbabilityModelList(I).Show
                    FormAdded = True
                    Return I 'The new Form is at position I in ProbabilityModelList ()
                    Exit For
                End If
            Next
            If FormAdded = False Then 'Add a new Form to ProbabilityModelList 
                Dim FormNo As Integer
                ProbabilityModelList.Add(ProbabilityModel)
                FormNo = ProbabilityModelList.Count - 1
                ProbabilityModelList(FormNo).FormNo = FormNo
                ProbabilityModelList(FormNo).Show
                Return FormNo 'The new Form is at position FormNo in ProbabilityModelList ()
            End If
        End If
    End Function

    Public Function OpenNewSeriesAnalysisForm() As Integer
        'Open a new SeriesAnalysis form, or reuse an existing one if available.
        'The new forms index number in SeriesAnalysisList is returned.

        SeriesAnalysis = New frmSeriesAnalysis
        If SeriesAnalysisList.Count = 0 Then
            SeriesAnalysisList.Add(SeriesAnalysis)
            SeriesAnalysisList(0).FormNo = 0
            SeriesAnalysisList(0).Show
            Return 0 'The new Form is at position 0 in SeriesAnalysisList ()
        Else
            Dim I As Integer
            Dim FormAdded As Boolean = False
            For I = 0 To SeriesAnalysisList.Count - 1
                If IsNothing(SeriesAnalysisList(I)) Then
                    SeriesAnalysisList(I) = SeriesAnalysis
                    SeriesAnalysisList(I).FormNo = I
                    SeriesAnalysisList(I).Show
                    FormAdded = True
                    Return I 'The new Form is at position I in SeriesAnalysisList ()
                    Exit For
                End If
            Next
            If FormAdded = False Then 'Add a new Form to SeriesAnalysisList 
                Dim FormNo As Integer
                SeriesAnalysisList.Add(SeriesAnalysis)
                FormNo = SeriesAnalysisList.Count - 1
                SeriesAnalysisList(FormNo).FormNo = FormNo
                SeriesAnalysisList(FormNo).Show
                Return FormNo 'The new Form is at position FormNo in SeriesAnalysisList ()
            End If
        End If
    End Function

    'Public Function OpenNewSampleSingleValue() As Integer
    '    'Open a new SampleSingleValue form, or reuse an existing one if available.
    '    'The new forms index number in SampleSingleValueList is returned.

    '    SampleSingleValue = New frmSampleSingleValue
    '    If SampleSingleValueList.Count = 0 Then
    '        SampleSingleValueList.Add(SampleSingleValue)
    '        SampleSingleValueList(0).FormNo = 0
    '        SampleSingleValueList(0).Show
    '        Return 0 'The new Form is at position 0 in SampleSingleValueList()
    '    Else
    '        Dim I As Integer
    '        Dim FormAdded As Boolean = False
    '        For I = 0 To SampleSingleValueList.Count - 1
    '            If IsNothing(SampleSingleValueList(I)) Then
    '                SampleSingleValueList(I) = SampleSingleValue
    '                SampleSingleValueList(I).FormNo = I
    '                SampleSingleValueList(I).Show
    '                FormAdded = True
    '                Return I 'The new Form is at position I in SampleSingleValueList()
    '                Exit For
    '            End If
    '        Next
    '        If FormAdded = False Then 'Add a new Form to SampleSingleValueList
    '            Dim FormNo As Integer
    '            SampleSingleValueList.Add(SampleSingleValue)
    '            FormNo = SampleSingleValueList.Count - 1
    '            SampleSingleValueList(FormNo).FormNo = FormNo
    '            SampleSingleValueList(FormNo).Show
    '            Return FormNo 'The new Form is at position FormNo in SampleSingleValueListList()
    '        End If
    '    End If

    'End Function

    Public Sub SaveAllOpenCharts()
        'Save any modifications made to all open charts
        Dim I As Integer
        For I = 0 To ChartList.Count - 1
            If IsNothing(ChartList(I)) Then
                'This chart form has been closed.
            Else
                ChartList(I).Save
            End If
        Next
    End Sub

    Public Sub ReplotAllOpenCharts()
        'RePlot all open charts
        Dim I As Integer
        For I = 0 To ChartList.Count - 1
            If IsNothing(ChartList(I)) Then
                'This chart form has been closed.
            Else
                ChartList(I).Plot
            End If
        Next
    End Sub

    Public Sub ChartClosed()
        'This subroutine is called when the Chart form has been closed.
        'The subroutine is usually called from the FormClosed event of the Chart form.
        'The Chart form may have multiple instances.
        'The ClosedFormNumber property should contain the number of the instance of the Chart form.
        'This property should be updated by the Chart form when it is being closed.
        'The ClosedFormNumber property value is used to determine which element in ChartList should be set to Nothing.

        If ChartList.Count < ClosedFormNo + 1 Then
            'ClosedFormNo is too large to exist in ChartList
            Exit Sub
        End If

        If IsNothing(ChartList(ClosedFormNo)) Then
            'The form is already set to nothing
        Else
            ChartList(ClosedFormNo) = Nothing
        End If
    End Sub

    Public Sub GenerateSamplesClosed()
        'This subroutine is called when the GenerateSamples form has been closed.
        'The subroutine is usually called from the FormClosed event of the GenerateSamples form.
        'The GenerateSamples form may have multiple instances.
        'The ClosedFormNumber property should contain the number of the instance of the GenerateSamples form.
        'This property should be updated by the GenerateSamples form when it is being closed.
        'The ClosedFormNumber property value is used to determine which element in GenerateSamplesList should be set to Nothing.

        If GenerateSamplesList.Count < ClosedFormNo + 1 Then
            'ClosedFormNo is too large to exist in GenerateSamplesList 
            Exit Sub
        End If

        If IsNothing(GenerateSamplesList(ClosedFormNo)) Then
            'The form is already set to nothing
        Else
            GenerateSamplesList(ClosedFormNo) = Nothing
        End If
    End Sub

    Public Sub DistribAnalysisClosed()
        'This subroutine is called when the DistribAnalysis form has been closed.
        'The subroutine is usually called from the FormClosed event of the DistribAnalysis form.
        'The DistribAnalysis form may have multiple instances.
        'The ClosedFormNumber property should contain the number of the instance of the DistribAnalysis form.
        'This property should be updated by the DistribAnalysis form when it is being closed.
        'The ClosedFormNumber property value is used to determine which element in DistribAnalysisList should be set to Nothing.

        If DistribAnalysisList.Count < ClosedFormNo + 1 Then
            'ClosedFormNo is too large to exist in DistribAnalysisist 
            Exit Sub
        End If

        If IsNothing(DistribAnalysisList(ClosedFormNo)) Then
            'The form is already set to nothing
        Else
            DistribAnalysisList(ClosedFormNo) = Nothing
        End If
    End Sub

    'Public Sub SampleSingleValueClosed()
    '    'This subroutine is called when the SampleSingleValue form has been closed.
    '    'The subroutine is usually called from the FormClosed event of the SampleSingleValue form.
    '    'The SampleSingleValue form may have multiple instances.
    '    'The ClosedFormNumber property should contain the number of the instance of the SampleSingleValue form.
    '    'This property should be updated by the SampleSingleValue form when it is being closed.
    '    'The ClosedFormNumber property value is used to determine which element in SampleSingleValueList should be set to Nothing.

    '    If SampleSingleValueList.Count < ClosedFormNo + 1 Then
    '        'ClosedFormNo is too large to exist in SampleSingleValueList
    '        Exit Sub
    '    End If

    '    If IsNothing(SampleSingleValueList(ClosedFormNo)) Then
    '        'The form is already set to nothing
    '    Else
    '        SampleSingleValueList(ClosedFormNo) = Nothing
    '    End If
    'End Sub

    Private Sub btnAdjust_Click(sender As Object, e As EventArgs) Handles btnAdjust.Click
        'Show the Adjust Parameters form.
        If IsNothing(Adjust) Then
            Adjust = New frmAdjust
            Adjust.Show()
            'Adjust.Distribution = Distribution
            'Adjust.LoadPrimaryParamInfo()
        Else
            Adjust.Show()
        End If
    End Sub

    Private Sub btnAdjust2_Click(sender As Object, e As EventArgs) Handles btnAdjust2.Click
        'Show the Adjust Parameters form.
        If IsNothing(Adjust) Then
            Adjust = New frmAdjust
            Adjust.Show()
        Else
            Adjust.Show()
        End If
    End Sub

    Private Sub Adjust_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Adjust.FormClosed
        Adjust = Nothing
    End Sub

    'Private Sub btnMultipleDistribs_Click(sender As Object, e As EventArgs) Handles btnMultipleDistribs.Click
    '    'Show the Multiple Parameters form.
    '    If IsNothing(MultipleDistribs) Then
    '        MultipleDistribs = New frmMultipleDistributions
    '        MultipleDistribs.Show()
    '        'Adjust.Distribution = Distribution
    '        'Adjust.LoadParamInfo()
    '    Else
    '        MultipleDistribs.Show()
    '    End If
    'End Sub

    'Private Sub MultipleDistribs_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MultipleDistribs.FormClosed
    '    MultipleDistribs = Nothing
    'End Sub

    Private Sub btnCalculations_Click(sender As Object, e As EventArgs) Handles btnCalculations.Click
        'Show the Calculationss form.
        If IsNothing(Calculations) Then
            Calculations = New frmCalculations
            Calculations.SelDistrib = SelDistrib
            Calculations.Show()
        Else
            Calculations.Show()
        End If
    End Sub

    Private Sub Calculations_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Calculations.FormClosed
        Calculations = Nothing
    End Sub

    Private Sub btnViewData_Click(sender As Object, e As EventArgs) Handles btnViewData.Click
        'Open a new Table form:
        Dim FormNo As Integer = OpenNewTableForm()
    End Sub

    Public Sub TableFormClosed()
        'This subroutine is called when the Table form has been closed.
        'The subroutine is usually called from the FormClosed event of the Table form.
        'The Table form may have multiple instances.
        'The ClosedFormNumber property should contain the number of the instance of the Table form.
        'This property should be updated by the Table form when it is being closed.
        'The ClosedFormNumber property value is used to determine which element in TableList should be set to Nothing.

        If TableList.Count < ClosedFormNo + 1 Then
            'ClosedFormNo is too large to exist in TableList 
            Exit Sub
        End If

        If IsNothing(TableList(ClosedFormNo)) Then
            'The form is already set to nothing
        Else
            TableList(ClosedFormNo) = Nothing
        End If
    End Sub

    Private Sub btnMostLikelyParams_Click(sender As Object, e As EventArgs) Handles btnMostLikelyParams.Click
        'Open a new MostLikelyParams form:
        Dim FormNo As Integer = OpenNewMostLikelyParamsForm()
    End Sub

    Public Sub MostLikelyParamsFormClosed()
        'This subroutine is called when the MostLikelyParams form has been closed.
        'The subroutine is usually called from the FormClosed event of the MostLikelyParams form.
        'The MostLikelyParams form may have multiple instances.
        'The ClosedFormNumber property should contain the number of the instance of the MostLikelyParams form.
        'This property should be updated by the MostLikelyParams form when it is being closed.
        'The ClosedFormNumber property value is used to determine which element in MostLikelyParamsList should be set to Nothing.

        If MostLikelyParamsList.Count < ClosedFormNo + 1 Then
            'ClosedFormNo is too large to exist in MostLikelyParamsList 
            Exit Sub
        End If

        If IsNothing(MostLikelyParamsList(ClosedFormNo)) Then
            'The form is already set to nothing
        Else
            MostLikelyParamsList(ClosedFormNo) = Nothing
        End If
    End Sub

    Private Sub btnProbabilityModel_Click(sender As Object, e As EventArgs) Handles btnProbabilityModel.Click
        'Open a new ProbabilityModel form:
        Dim FormNo As Integer = OpenNewProbabilityModelForm()
    End Sub

    Public Sub ProbabilityModelFormClosed()
        'This subroutine is called when the ProbabilityModel form has been closed.
        'The subroutine is usually called from the FormClosed event of the ProbabilityModel form.
        'The ProbabilityModel form may have multiple instances.
        'The ClosedFormNumber property should contain the number of the instance of the ProbabilityModel form.
        'This property should be updated by the ProbabilityModel form when it is being closed.
        'The ClosedFormNumber property value is used to determine which element in ProbabilityModelList should be set to Nothing.

        If ProbabilityModelList.Count < ClosedFormNo + 1 Then
            'ClosedFormNo is too large to exist in ProbabilityModelList 
            Exit Sub
        End If

        If IsNothing(ProbabilityModelList(ClosedFormNo)) Then
            'The form is already set to nothing
        Else
            ProbabilityModelList(ClosedFormNo) = Nothing
        End If
    End Sub



    Private Sub btnSeriesAnalysis_Click(sender As Object, e As EventArgs) Handles btnSeriesAnalysis.Click
        'Open a new SeriesAnalysis form:
        Dim FormNo As Integer = OpenNewSeriesAnalysisForm()
    End Sub

    Public Sub SeriesAnalysisFormClosed()
        'This subroutine is called when the SeriesAnalysis form has been closed.
        'The subroutine is usually called from the FormClosed event of the SeriesAnalysis form.
        'The SeriesAnalysis form may have multiple instances.
        'The ClosedFormNumber property should contain the number of the instance of the SeriesAnalysis form.
        'This property should be updated by the SeriesAnalysis form when it is being closed.
        'The ClosedFormNumber property value is used to determine which element in SeriesAnalysisList should be set to Nothing.

        If SeriesAnalysisList.Count < ClosedFormNo + 1 Then
            'ClosedFormNo is too large to exist in SeriesAnalysisList 
            Exit Sub
        End If

        If IsNothing(SeriesAnalysisList(ClosedFormNo)) Then
            'The form is already set to nothing
        Else
            SeriesAnalysisList(ClosedFormNo) = Nothing
        End If
    End Sub

#End Region 'Open and Close Forms -------------------------------------------------------------------------------------------------------------------------------------------------------------





#Region " Form Methods - The main actions performed by this form." '===========================================================================================================================

    Public Sub CloseAppAtConnection(ByVal ProNetName As String, ByVal ConnectionName As String)
        'Close the application and project at the specified connection.

        If IsNothing(client) Then
            Message.Add("No client connection available!" & vbCrLf)
        Else
            If client.State = ServiceModel.CommunicationState.Faulted Then
                Message.Add("client state is faulted. Message not sent!" & vbCrLf)
            Else
                'Create the XML instructions to close the application at the connection.
                Dim decl As New XDeclaration("1.0", "utf-8", "yes")
                Dim doc As New XDocument(decl, Nothing) 'Create an XDocument to store the instructions.
                Dim xmessage As New XElement("XMsg") 'This indicates the start of the message in the XMessage class

                Dim command As New XElement("Command", "Close")
                xmessage.Add(command)
                doc.Add(xmessage)

                'Show the message sent:
                Message.XAddText("Message sent to: [" & ProNetName & "]." & ConnectionName & ":" & vbCrLf, "XmlSentNotice")
                Message.XAddXml(doc.ToString)
                Message.XAddText(vbCrLf, "Normal") 'Add extra line
                client.SendMessage(ProNetName, ConnectionName, doc.ToString)
            End If
        End If
    End Sub

    Private Sub btnProject_Click(sender As Object, e As EventArgs) Handles btnProject.Click
        Project.SelectProject()
    End Sub

    Private Sub btnParameters_Click(sender As Object, e As EventArgs) Handles btnParameters.Click
        Project.ShowParameters()
    End Sub

    Private Sub btnAppInfo_Click(sender As Object, e As EventArgs) Handles btnAppInfo.Click
        ApplicationInfo.ShowInfo()
    End Sub

    Private Sub btnAndorville_Click(sender As Object, e As EventArgs) Handles btnAndorville.Click
        ApplicationInfo.ShowInfo()
    End Sub

    Private Sub ApplicationInfo_UpdateExePath() Handles ApplicationInfo.UpdateExePath
        'Update the Executable Path.
        ApplicationInfo.ExecutablePath = Application.ExecutablePath
    End Sub

    Private Sub ApplicationInfo_RestoreDefaults() Handles ApplicationInfo.RestoreDefaults
        'Restore the default application settings.
        DefaultAppProperties()
    End Sub

    Public Sub UpdateWebPage(ByVal FileName As String)
        'Update the web page in WebPageFormList if the Web file name is FileName.

        Dim NPages As Integer = WebPageFormList.Count
        Dim I As Integer

        Try
            For I = 0 To NPages - 1
                If IsNothing(WebPageFormList(I)) Then
                    'Web page has been deleted!
                Else
                    If WebPageFormList(I).FileName = FileName Then
                        WebPageFormList(I).OpenDocument
                    End If
                End If
            Next
        Catch ex As Exception
            Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub


#Region " Start Page Code" '=========================================================================================================================================

    Public Sub OpenStartPage()
        'Open the workflow page:

        If Project.DataFileExists(WorkflowFileName) Then
            'Note: WorkflowFileName should have been restored when the application started.
            DisplayWorkflow()
        ElseIf Project.DataFileExists("StartPage.html") Then
            WorkflowFileName = "StartPage.html"
            DisplayWorkflow()
        Else
            CreateStartPage()
            WorkflowFileName = "StartPage.html"
            DisplayWorkflow()
        End If

        'Open the StartPage.html file and display in the Workflow tab.
        'If Project.DataFileExists("StartPage.html") Then
        '    WorkflowFileName = "StartPage.html"
        '    DisplayWorkflow()
        'Else
        '    CreateStartPage()
        '    WorkflowFileName = "StartPage.html"
        '    DisplayWorkflow()
        'End If

    End Sub

    Public Sub DisplayWorkflow()
        'Display the StartPage.html file in the Start Page tab.

        If Project.DataFileExists(WorkflowFileName) Then
            Dim rtbData As New IO.MemoryStream
            Project.ReadData(WorkflowFileName, rtbData)
            rtbData.Position = 0
            Dim sr As New IO.StreamReader(rtbData)
            WebBrowser1.DocumentText = sr.ReadToEnd()
        Else
            Message.AddWarning("Web page file not found: " & WorkflowFileName & vbCrLf)
        End If
    End Sub

    Private Sub CreateStartPage()
        'Create a new default StartPage.html file.

        Dim htmData As New IO.MemoryStream
        Dim sw As New IO.StreamWriter(htmData)
        sw.Write(AppInfoHtmlString("Application Information")) 'Create a web page providing information about the application.
        sw.Flush()
        Project.SaveData("StartPage.html", htmData)
    End Sub

    Public Function AppInfoHtmlString(ByVal DocumentTitle As String) As String
        'Create an Application Information Web Page.

        'This function should be edited to provide a brief description of the Application.

        Dim sb As New System.Text.StringBuilder

        sb.Append("<!DOCTYPE html>" & vbCrLf)
        sb.Append("<html>" & vbCrLf)
        sb.Append("<head>" & vbCrLf)
        sb.Append("<title>" & DocumentTitle & "</title>" & vbCrLf)
        sb.Append("<meta name=""description"" content=""Application information."">" & vbCrLf)
        sb.Append("</head>" & vbCrLf)

        sb.Append("<body style=""font-family:arial;"">" & vbCrLf & vbCrLf)

        sb.Append("<h2>" & "Andorville&trade; Distributions" & "</h2>" & vbCrLf & vbCrLf) 'Add the page title.
        sb.Append("<hr>" & vbCrLf) 'Add a horizontal divider line.
        sb.Append("<p>The Distributions application is used to design probability distributions.</p>" & vbCrLf) 'Add an application description.
        sb.Append("<hr>" & vbCrLf & vbCrLf) 'Add a horizontal divider line.

        sb.Append(DefaultJavaScriptString)

        sb.Append("</body>" & vbCrLf)
        sb.Append("</html>" & vbCrLf)

        Return sb.ToString

    End Function

    Public Function DefaultJavaScriptString() As String
        'Generate the default JavaScript section of an Andorville(TM) Workflow Web Page.

        Dim sb As New System.Text.StringBuilder

        'Add JavaScript section:
        sb.Append("<script>" & vbCrLf & vbCrLf)

        'START: User defined JavaScript functions ==========================================================================
        'Add functions to implement the main actions performed by this web page.
        sb.Append("//START: User defined JavaScript functions ==========================================================================" & vbCrLf)
        sb.Append("//  Add functions to implement the main actions performed by this web page." & vbCrLf & vbCrLf)

        sb.Append("//END:   User defined JavaScript functions __________________________________________________________________________" & vbCrLf & vbCrLf & vbCrLf)
        'END:   User defined JavaScript functions --------------------------------------------------------------------------


        'START: User modified JavaScript functions ==========================================================================
        'Modify these function to save all required web page settings and process all expected XMessage instructions.
        sb.Append("//START: User modified JavaScript functions ==========================================================================" & vbCrLf)
        sb.Append("//  Modify these function to save all required web page settings and process all expected XMessage instructions." & vbCrLf & vbCrLf)

        'Add the Start Up code section.
        sb.Append("//Code to execute on Start Up:" & vbCrLf)
        sb.Append("function StartUpCode() {" & vbCrLf)
        sb.Append("  RestoreSettings() ;" & vbCrLf)
        sb.Append("}" & vbCrLf & vbCrLf)

        'Add the SaveSettings function - This is used to save web page settings between sessions.
        sb.Append("//Save the web page settings." & vbCrLf)
        sb.Append("function SaveSettings() {" & vbCrLf)
        sb.Append("  var xSettings = ""<Settings>"" + "" \n"" ; //String containing the web page settings in XML format." & vbCrLf)
        sb.Append("  //Add xml lines to save each setting." & vbCrLf & vbCrLf)
        sb.Append("  xSettings +=    ""</Settings>"" + ""\n"" ; //End of the Settings element." & vbCrLf)
        sb.Append(vbCrLf)
        sb.Append("  //Save the settings as an XML file in the project." & vbCrLf)
        sb.Append("  window.external.SaveHtmlSettings(xSettings) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Process a single XMsg instruction (Information:Location pair)
        sb.Append("//Process an XMessage instruction:" & vbCrLf)
        sb.Append("function XMsgInstruction(Info, Locn) {" & vbCrLf)
        sb.Append("  switch(Locn) {" & vbCrLf)
        sb.Append("  //Insert case statements here." & vbCrLf)
        sb.Append(vbCrLf)

        'sb.Append(vbCrLf)
        'sb.Append("  case ""Status"" :" & vbCrLf)
        'sb.Append("    if (Info = ""OK"") { " & vbCrLf)
        'sb.Append("      //Instruction processing completed OK:" & vbCrLf)
        'sb.Append("      } else {" & vbCrLf)
        'sb.Append("      window.external.AddWarning(""Error: Unknown Status information: "" + "" Info: "" + Info + ""\r\n"") ;" & vbCrLf)
        'sb.Append("     }" & vbCrLf)
        'sb.Append("    break ;" & vbCrLf)
        'sb.Append(vbCrLf)

        'sb.Append("  case ""OnCompletion"" :" & vbCrLf)
        sb.Append("  case ""EndInstruction"" :" & vbCrLf)
        sb.Append("    switch(Info) {" & vbCrLf)
        sb.Append("      case ""Stop"" :" & vbCrLf)
        sb.Append("        //Do nothing." & vbCrLf)
        sb.Append("        break ;" & vbCrLf)
        sb.Append(vbCrLf)
        sb.Append("      default:" & vbCrLf)
        'sb.Append("        window.external.AddWarning(""Error: Unknown OnCompletion information:  "" + "" Info: "" + Info + ""\r\n"") ;" & vbCrLf)
        sb.Append("        window.external.AddWarning(""Error: Unknown EndInstruction information:  "" + "" Info: "" + Info + ""\r\n"") ;" & vbCrLf)
        sb.Append("        break ;" & vbCrLf)
        sb.Append("    }" & vbCrLf)
        sb.Append("    break ;" & vbCrLf)
        sb.Append(vbCrLf)

        sb.Append("  case ""Status"" :" & vbCrLf)
        sb.Append("    switch(Info) {" & vbCrLf)
        sb.Append("      case ""OK"" :" & vbCrLf)
        sb.Append("        //Instruction processing completed OK." & vbCrLf)
        sb.Append("        break ;" & vbCrLf)
        sb.Append(vbCrLf)
        sb.Append("      default:" & vbCrLf)
        sb.Append("        window.external.AddWarning(""Error: Unknown Status information:  "" + "" Info: "" + Info + ""\r\n"") ;" & vbCrLf)
        sb.Append("        break ;" & vbCrLf)
        sb.Append("    }" & vbCrLf)
        sb.Append("    break ;" & vbCrLf)
        sb.Append(vbCrLf)

        sb.Append("  default:" & vbCrLf)
        sb.Append("    window.external.AddWarning(""Unknown location: "" + Locn + ""\r\n"") ;" & vbCrLf)
        sb.Append("  }" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        sb.Append("//END:   User modified JavaScript functions __________________________________________________________________________" & vbCrLf & vbCrLf & vbCrLf)
        'END:   User modified JavaScript functions --------------------------------------------------------------------------

        'START: Required Document Library Web Page JavaScript functions ==========================================================================
        sb.Append("//START: Required Document Library Web Page JavaScript functions ==========================================================================" & vbCrLf & vbCrLf)

        'Add the AddText function - This sends a message to the message window using a named text type.
        sb.Append("//Add text to the Message window using a named txt type:" & vbCrLf)
        sb.Append("function AddText(Msg, TextType) {" & vbCrLf)
        sb.Append("  window.external.AddText(Msg, TextType) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Add the AddMessage function - This sends a message to the message window using default black text.
        sb.Append("//Add a message to the Message window using the default black text:" & vbCrLf)
        sb.Append("function AddMessage(Msg) {" & vbCrLf)
        sb.Append("  window.external.AddMessage(Msg) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Add the AddWarning function - This sends a red, bold warning message to the message window.
        sb.Append("//Add a warning message to the Message window using bold red text:" & vbCrLf)
        sb.Append("function AddWarning(Msg) {" & vbCrLf)
        sb.Append("  window.external.AddWarning(Msg) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Add the RestoreSettings function - This is used to restore web page settings.
        sb.Append("//Restore the web page settings." & vbCrLf)
        sb.Append("function RestoreSettings() {" & vbCrLf)
        sb.Append("  window.external.RestoreHtmlSettings() " & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'This line runs the RestoreSettings function when the web page is loaded.
        sb.Append("//Restore the web page settings when the page loads." & vbCrLf)
        'sb.Append("window.onload = RestoreSettings; " & vbCrLf)
        sb.Append("window.onload = StartUpCode ; " & vbCrLf)
        sb.Append(vbCrLf)

        'Restores a single setting on the web page.
        sb.Append("//Restore a web page setting." & vbCrLf)
        sb.Append("  function RestoreSetting(FormName, ItemName, ItemValue) {" & vbCrLf)
        sb.Append("  document.forms[FormName][ItemName].value = ItemValue ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Add the RestoreOption function - This is used to add an option to a Select list.
        sb.Append("//Restore a Select control Option." & vbCrLf)
        sb.Append("function RestoreOption(SelectId, OptionText) {" & vbCrLf)
        sb.Append("  var x = document.getElementById(SelectId) ;" & vbCrLf)
        sb.Append("  var option = document.createElement(""Option"") ;" & vbCrLf)
        sb.Append("  option.text = OptionText ;" & vbCrLf)
        sb.Append("  x.add(option) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        sb.Append("//END:   Required Document Library Web Page JavaScript functions __________________________________________________________________________" & vbCrLf & vbCrLf)
        'END:   Required Document Library Web Page JavaScript functions --------------------------------------------------------------------------

        sb.Append("</script>" & vbCrLf & vbCrLf)

        Return sb.ToString

    End Function

    Public Function DefaultJavaScriptString_Old() As String
        'Generate the default JavaScript section of an Andorville(TM) Workflow Web Page.

        Dim sb As New System.Text.StringBuilder

        'Add JavaScript section:
        sb.Append("<script>" & vbCrLf & vbCrLf)

        'START: User defined JavaScript functions ==========================================================================
        'Add functions to implement the main actions performed by this web page.
        sb.Append("//START: User defined JavaScript functions ==========================================================================" & vbCrLf)
        sb.Append("//  Add functions to implement the main actions performed by this web page." & vbCrLf & vbCrLf)

        sb.Append("//END:   User defined JavaScript functions __________________________________________________________________________" & vbCrLf & vbCrLf & vbCrLf)
        'END:   User defined JavaScript functions --------------------------------------------------------------------------


        'START: User modified JavaScript functions ==========================================================================
        'Modify these function to save all required web page settings and process all expected XMessage instructions.
        sb.Append("//START: User modified JavaScript functions ==========================================================================" & vbCrLf)
        sb.Append("//  Modify these function to save all required web page settings and process all expected XMessage instructions." & vbCrLf & vbCrLf)

        'Add the Start Up code section.
        sb.Append("//Code to execute on Start Up:" & vbCrLf)
        sb.Append("function StartUpCode() {" & vbCrLf)
        sb.Append("  RestoreSettings() ;" & vbCrLf)
        'sb.Append("  GetCalcsDbPath() ;" & vbCrLf)
        sb.Append("}" & vbCrLf & vbCrLf)

        'Add the SaveSettings function - This is used to save web page settings between sessions.
        sb.Append("//Save the web page settings." & vbCrLf)
        sb.Append("function SaveSettings() {" & vbCrLf)
        sb.Append("  var xSettings = ""<Settings>"" + "" \n"" ; //String containing the web page settings in XML format." & vbCrLf)
        sb.Append("  //Add xml lines to save each setting." & vbCrLf & vbCrLf)
        sb.Append("  xSettings +=    ""</Settings>"" + ""\n"" ; //End of the Settings element." & vbCrLf)
        sb.Append(vbCrLf)
        sb.Append("  //Save the settings as an XML file in the project." & vbCrLf)
        sb.Append("  window.external.SaveHtmlSettings(xSettings) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Process a single XMsg instruction (Information:Location pair)
        sb.Append("//Process an XMessage instruction:" & vbCrLf)
        sb.Append("function XMsgInstruction(Info, Locn) {" & vbCrLf)
        sb.Append("  switch(Locn) {" & vbCrLf)
        sb.Append("  //Insert case statements here." & vbCrLf)
        sb.Append("  case ""Status"" :" & vbCrLf)
        sb.Append("    if (Info = ""OK"") { " & vbCrLf)
        sb.Append("      //Instruction processing completed OK:" & vbCrLf)
        sb.Append("      } else {" & vbCrLf)
        sb.Append("      window.external.AddWarning(""Error: Unknown Status information: "" + "" Info: "" + Info + ""\r\n"") ;" & vbCrLf)
        sb.Append("     }" & vbCrLf)
        sb.Append("    break ;" & vbCrLf)
        sb.Append(vbCrLf)
        sb.Append("  default:" & vbCrLf)
        sb.Append("    window.external.AddWarning(""Unknown location: "" + Locn + ""\r\n"") ;" & vbCrLf)
        sb.Append("  }" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        sb.Append("//END:   User modified JavaScript functions __________________________________________________________________________" & vbCrLf & vbCrLf & vbCrLf)
        'END:   User modified JavaScript functions --------------------------------------------------------------------------

        'START: Required Document Library Web Page JavaScript functions ==========================================================================
        sb.Append("//START: Required Document Library Web Page JavaScript functions ==========================================================================" & vbCrLf & vbCrLf)

        'Add the AddText function - This sends a message to the message window using a named text type.
        sb.Append("//Add text to the Message window using a named txt type:" & vbCrLf)
        sb.Append("function AddText(Msg, TextType) {" & vbCrLf)
        sb.Append("  window.external.AddText(Msg, TextType) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Add the AddMessage function - This sends a message to the message window using default black text.
        sb.Append("//Add a message to the Message window using the default black text:" & vbCrLf)
        sb.Append("function AddMessage(Msg) {" & vbCrLf)
        sb.Append("  window.external.AddMessage(Msg) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Add the AddWarning function - This sends a red, bold warning message to the message window.
        sb.Append("//Add a warning message to the Message window using bold red text:" & vbCrLf)
        sb.Append("function AddWarning(Msg) {" & vbCrLf)
        sb.Append("  window.external.AddWarning(Msg) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Add the RestoreSettings function - This is used to restore web page settings.
        sb.Append("//Restore the web page settings." & vbCrLf)
        sb.Append("function RestoreSettings() {" & vbCrLf)
        sb.Append("  window.external.RestoreHtmlSettings() " & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'This line runs the RestoreSettings function when the web page is loaded.
        sb.Append("//Restore the web page settings when the page loads." & vbCrLf)
        'sb.Append("window.onload = RestoreSettings; " & vbCrLf)
        sb.Append("window.onload = StartUpCode ; " & vbCrLf)
        sb.Append(vbCrLf)

        'Restores a single setting on the web page.
        sb.Append("//Restore a web page setting." & vbCrLf)
        sb.Append("  function RestoreSetting(FormName, ItemName, ItemValue) {" & vbCrLf)
        sb.Append("  document.forms[FormName][ItemName].value = ItemValue ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Add the RestoreOption function - This is used to add an option to a Select list.
        sb.Append("//Restore a Select control Option." & vbCrLf)
        sb.Append("function RestoreOption(SelectId, OptionText) {" & vbCrLf)
        sb.Append("  var x = document.getElementById(SelectId) ;" & vbCrLf)
        sb.Append("  var option = document.createElement(""Option"") ;" & vbCrLf)
        sb.Append("  option.text = OptionText ;" & vbCrLf)
        sb.Append("  x.add(option) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        sb.Append("//END:   Required Document Library Web Page JavaScript functions __________________________________________________________________________" & vbCrLf & vbCrLf)
        'END:   Required Document Library Web Page JavaScript functions --------------------------------------------------------------------------

        sb.Append("</script>" & vbCrLf & vbCrLf)

        Return sb.ToString

    End Function

    Public Function DefaultHtmlString(ByVal DocumentTitle As String) As String
        'Create a blank HTML Web Page.

        Dim sb As New System.Text.StringBuilder

        sb.Append("<!DOCTYPE html>" & vbCrLf)
        sb.Append("<html>" & vbCrLf)
        sb.Append("<!-- Andorville(TM) Workflow File -->" & vbCrLf)
        sb.Append("<!-- Application Name:    " & ApplicationInfo.Name & " -->" & vbCrLf)
        sb.Append("<!-- Application Version: " & My.Application.Info.Version.ToString & " -->" & vbCrLf)
        sb.Append("<!-- Creation Date:          " & Format(Now, "dd MMMM yyyy") & " -->" & vbCrLf)
        sb.Append("<head>" & vbCrLf)
        sb.Append("<title>" & DocumentTitle & "</title>" & vbCrLf)
        sb.Append("<meta name=""description"" content=""Workflow description."">" & vbCrLf)
        sb.Append("</head>" & vbCrLf)

        sb.Append("<body style=""font-family:arial;"">" & vbCrLf & vbCrLf)

        sb.Append("<h2>" & DocumentTitle & "</h2>" & vbCrLf & vbCrLf)

        sb.Append(DefaultJavaScriptString)

        sb.Append("</body>" & vbCrLf)
        sb.Append("</html>" & vbCrLf)

        Return sb.ToString

    End Function

    Public Function DefaultHtmlString_Old(ByVal DocumentTitle As String) As String
        'Create a blank HTML Web Page.

        Dim sb As New System.Text.StringBuilder

        sb.Append("<!DOCTYPE html>" & vbCrLf)
        sb.Append("<html>" & vbCrLf & "<head>" & vbCrLf & "<title>" & DocumentTitle & "</title>" & vbCrLf)
        sb.Append("</head>" & vbCrLf & "<body>" & vbCrLf & vbCrLf)
        sb.Append("<h1>" & DocumentTitle & "</h1>" & vbCrLf & vbCrLf)

        'Add JavaScript section:
        sb.Append("<script>" & vbCrLf & vbCrLf)

        'START: User defined JavaScript functions ==========================================================================
        'Add functions to implement the main actions performed by this web page.
        sb.Append("//START: User defined JavaScript functions ==========================================================================" & vbCrLf)
        sb.Append("//  Add functions to implement the main actions performed by this web page." & vbCrLf & vbCrLf)

        sb.Append("//END:   User defined JavaScript functions __________________________________________________________________________" & vbCrLf & vbCrLf & vbCrLf)
        'END:   User defined JavaScript functions --------------------------------------------------------------------------


        'START: User modified JavaScript functions ==========================================================================
        'Modify these function to save all required web page settings and process all expected XMessage instructions.
        sb.Append("//START: User modified JavaScript functions ==========================================================================" & vbCrLf)
        sb.Append("//  Modify these function to save all required web page settings and process all expected XMessage instructions." & vbCrLf & vbCrLf)

        'Add the SaveSettings function - This is used to save web page settings between sessions.
        sb.Append("//Save the web page settings." & vbCrLf)
        sb.Append("function SaveSettings() {" & vbCrLf)
        sb.Append("  var xSettings = ""<Settings>"" + "" \n"" ; //String containing the web page settings in XML format." & vbCrLf)
        sb.Append("  //Add xml lines to save each setting." & vbCrLf & vbCrLf)
        sb.Append("  xSettings +=    ""</Settings>"" + ""\n"" ; //End of the Settings element." & vbCrLf)
        sb.Append(vbCrLf)
        sb.Append("  //Save the settings as an XML file in the project." & vbCrLf)
        sb.Append("  window.external.SaveHtmlSettings(xSettings) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Process a single XMsg instruction (Information:Location pair)
        sb.Append("//Process an XMessage instruction:" & vbCrLf)
        sb.Append("function XMsgInstruction(Info, Locn) {" & vbCrLf)
        sb.Append("  switch(Locn) {" & vbCrLf)
        sb.Append("  //Insert case statements here." & vbCrLf)
        sb.Append("  default:" & vbCrLf)
        sb.Append("    window.external.AddWarning(""Unknown location: "" + Locn + ""\r\n"") ;" & vbCrLf)
        sb.Append("  }" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        sb.Append("//END:   User modified JavaScript functions __________________________________________________________________________" & vbCrLf & vbCrLf & vbCrLf)
        'END:   User modified JavaScript functions --------------------------------------------------------------------------

        'START: Required Document Library Web Page JavaScript functions ==========================================================================
        sb.Append("//START: Required Document Library Web Page JavaScript functions ==========================================================================" & vbCrLf & vbCrLf)

        'Add the AddText function - This sends a message to the message window using a named text type.
        sb.Append("//Add text to the Message window using a named txt type:" & vbCrLf)
        sb.Append("function AddText(Msg, TextType) {" & vbCrLf)
        sb.Append("  window.external.AddText(Msg, TextType) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Add the AddMessage function - This sends a message to the message window using default black text.
        sb.Append("//Add a message to the Message window using the default black text:" & vbCrLf)
        sb.Append("function AddMessage(Msg) {" & vbCrLf)
        sb.Append("  window.external.AddMessage(Msg) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Add the AddWarning function - This sends a red, bold warning message to the message window.
        sb.Append("//Add a warning message to the Message window using bold red text:" & vbCrLf)
        sb.Append("function AddWarning(Msg) {" & vbCrLf)
        sb.Append("  window.external.AddWarning(Msg) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Add the RestoreSettings function - This is used to restore web page settings.
        sb.Append("//Restore the web page settings." & vbCrLf)
        sb.Append("function RestoreSettings() {" & vbCrLf)
        sb.Append("  window.external.RestoreHtmlSettings() " & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'This line runs the RestoreSettings function when the web page is loaded.
        sb.Append("//Restore the web page settings when the page loads." & vbCrLf)
        sb.Append("window.onload = RestoreSettings; " & vbCrLf)
        sb.Append(vbCrLf)

        'Restores a single setting on the web page.
        sb.Append("//Restore a web page setting." & vbCrLf)
        sb.Append("  function RestoreSetting(FormName, ItemName, ItemValue) {" & vbCrLf)
        sb.Append("  document.forms[FormName][ItemName].value = ItemValue ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Add the RestoreOption function - This is used to add an option to a Select list.
        sb.Append("//Restore a Select control Option." & vbCrLf)
        sb.Append("function RestoreOption(SelectId, OptionText) {" & vbCrLf)
        sb.Append("  var x = document.getElementById(SelectId) ;" & vbCrLf)
        sb.Append("  var option = document.createElement(""Option"") ;" & vbCrLf)
        sb.Append("  option.text = OptionText ;" & vbCrLf)
        sb.Append("  x.add(option) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        sb.Append("//END:   Required Document Library Web Page JavaScript functions __________________________________________________________________________" & vbCrLf & vbCrLf)
        'END:   Required Document Library Web Page JavaScript functions --------------------------------------------------------------------------

        sb.Append("</script>" & vbCrLf & vbCrLf)

        sb.Append("</body>" & vbCrLf & "</html>" & vbCrLf)

        Return sb.ToString

    End Function

#End Region 'Start Page Code ------------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Methods Called by JavaScript - A collection of methods that can be called by JavaScript in a web page shown in WebBrowser1" '==================================
    'These methods are used to display HTML pages in the Workflow tab.
    'The same methods can be found in the WebView form, which displays web pages on seprate forms.


    'Display Messages ==============================================================================================

    Public Sub AddMessage(ByVal Msg As String)
        'Add a normal text message to the Message window.
        Message.Add(Msg)
    End Sub

    Public Sub AddWarning(ByVal Msg As String)
        'Add a warning text message to the Message window.
        Message.AddWarning(Msg)
    End Sub

    Public Sub AddTextTypeMessage(ByVal Msg As String, ByVal TextType As String)
        'Add a message with the specified Text Type to the Message window.
        Message.AddText(Msg, TextType)
    End Sub

    Public Sub AddXmlMessage(ByVal XmlText As String)
        'Add an Xml message to the Message window.
        Message.AddXml(XmlText)
    End Sub

    'END Display Messages ------------------------------------------------------------------------------------------


    'Run an XSequence ==============================================================================================

    Public Sub RunClipboardXSeq()
        'Run the XSequence instructions in the clipboard.

        Dim XDocSeq As System.Xml.Linq.XDocument
        Try
            XDocSeq = XDocument.Parse(My.Computer.Clipboard.GetText)
        Catch ex As Exception
            Message.AddWarning("Error reading Clipboard data. " & ex.Message & vbCrLf)
            Exit Sub
        End Try

        If IsNothing(XDocSeq) Then
            Message.Add("No XSequence instructions were found in the clipboard.")
        Else
            Dim XmlSeq As New System.Xml.XmlDocument
            Try
                XmlSeq.LoadXml(XDocSeq.ToString) 'Convert XDocSeq to an XmlDocument to process with XSeq.
                'Run the sequence:
                XSeq.RunXSequence(XmlSeq, Status)
            Catch ex As Exception
                Message.AddWarning("Error restoring HTML settings. " & ex.Message & vbCrLf)
            End Try
        End If
    End Sub

    Public Sub RunXSequence(ByVal XSequence As String)
        'Run the XMSequence
        Dim XmlSeq As New System.Xml.XmlDocument
        XmlSeq.LoadXml(XSequence)
        XSeq.RunXSequence(XmlSeq, Status)
    End Sub

    Private Sub XSeq_ErrorMsg(ErrMsg As String) Handles XSeq.ErrorMsg
        Message.AddWarning(ErrMsg & vbCrLf)
    End Sub

    Private Sub XSeq_Instruction(Data As String, Locn As String) Handles XSeq.Instruction
        'Execute each instruction produced by running the XSeq file.

        Select Case Locn

            'Restore Web Page Settings: -------------------------------------------------
            Case "Settings:Form:Name"
                FormName = Data

            Case "Settings:Form:Item:Name"
                ItemName = Data

            Case "Settings:Form:Item:Value"
                RestoreSetting(FormName, ItemName, Data)

            Case "Settings:Form:SelectId"
                SelectId = Data

            Case "Settings:Form:OptionText"
                RestoreOption(SelectId, Data)
            'END Restore Web Page Settings: ---------------------------------------------

            ''Start Project commands: ----------------------------------------------------

            'Case "StartProject:AppName"
            '    StartProject_AppName = Data

            'Case "StartProject:ConnectionName"
            '    StartProject_ConnName = Data

            'Case "StartProject:ProNetName"
            '    StartProject_ProNetName = Data

            'Case "StartProject:ProjectID"
            '    StartProject_ProjID = Data

            'Case "StartProject:ProjectName"
            '    StartProject_ProjName = Data

            'Case "StartProject:Command"
            '    Select Case Data
            '        Case "Apply"
            '            If StartProject_ProjName <> "" Then
            '                StartApp_ProjectName(StartProject_AppName, StartProject_ProjName, StartProject_ConnName)
            '            ElseIf StartProject_ProjID <> "" Then
            '                StartApp_ProjectID(StartProject_AppName, StartProject_ProjID, StartProject_ConnName)
            '            Else
            '                Message.AddWarning("Project not specified. Project Name and Project ID are blank." & vbCrLf)
            '            End If
            '        Case Else
            '            Message.AddWarning("Unknown Start Project command : " & Data & vbCrLf)
            '    End Select

            ''END Start project commands ---------------------------------------------

            Case "Settings"
                'Do nothing


            Case "EndOfSequence"
                'Main.Message.Add("End of processing sequence" & Data & vbCrLf)

            Case Else
                Message.AddWarning("Unknown location: " & Locn & "  Data: " & Data & vbCrLf)

        End Select
    End Sub

    'END Run an XSequence ------------------------------------------------------------------------------------------


    'Run an XMessage ===============================================================================================

    Public Sub RunXMessage(ByVal XMsg As String)
        'Run the XMessage by sending it to InstrReceived.
        InstrReceived = XMsg
    End Sub

    Public Sub SendXMessage(ByVal ConnName As String, ByVal XMsg As String)
        'Send the XMessage to the application with the connection name ConnName.
        If IsNothing(client) Then
            Message.Add("No client connection available!" & vbCrLf)
        Else
            If client.State = ServiceModel.CommunicationState.Faulted Then
                Message.Add("client state is faulted. Message not sent!" & vbCrLf)
            Else
                If bgwSendMessage.IsBusy Then
                    Message.AddWarning("Send Message backgroundworker is busy." & vbCrLf)
                Else
                    Dim SendMessageParams As New Main.clsSendMessageParams
                    SendMessageParams.ProjectNetworkName = ProNetName
                    SendMessageParams.ConnectionName = ConnName
                    SendMessageParams.Message = XMsg
                    bgwSendMessage.RunWorkerAsync(SendMessageParams)
                    If ShowXMessages Then
                        Message.XAddText("Message sent to " & "[" & ProNetName & "]." & ConnName & ":" & vbCrLf, "XmlSentNotice")
                        Message.XAddXml(XMsg)
                        Message.XAddText(vbCrLf, "Normal") 'Add extra line
                    End If
                End If
            End If
        End If
    End Sub

    Public Sub SendXMessageExt(ByVal ProNetName As String, ByVal ConnName As String, ByVal XMsg As String)
        'Send the XMsg to the application with the connection name ConnName and Project Network Name ProNetname.
        'This version can send the XMessage to a connection external to the current Project Network.
        If IsNothing(client) Then
            Message.Add("No client connection available!" & vbCrLf)
        Else
            If client.State = ServiceModel.CommunicationState.Faulted Then
                Message.Add("client state is faulted. Message not sent!" & vbCrLf)
            Else
                If bgwSendMessage.IsBusy Then
                    Message.AddWarning("Send Message backgroundworker is busy." & vbCrLf)
                Else
                    Dim SendMessageParams As New Main.clsSendMessageParams
                    SendMessageParams.ProjectNetworkName = ProNetName
                    SendMessageParams.ConnectionName = ConnName
                    SendMessageParams.Message = XMsg
                    bgwSendMessage.RunWorkerAsync(SendMessageParams)
                    If ShowXMessages Then
                        Message.XAddText("Message sent to " & "[" & ProNetName & "]." & ConnName & ":" & vbCrLf, "XmlSentNotice")
                        Message.XAddXml(XMsg)
                        Message.XAddText(vbCrLf, "Normal") 'Add extra line
                    End If
                End If
            End If
        End If
    End Sub

    Public Sub SendXMessageWait(ByVal ConnName As String, ByVal XMsg As String)
        'Send the XMsg to the application with the connection name ConnName.
        'Wait for the connection to be made.
        If IsNothing(client) Then
            Message.Add("No client connection available!" & vbCrLf)
        Else
            Try
                'Application.DoEvents() 'TRY THE METHOD WITHOUT THE DOEVENTS
                If client.State = ServiceModel.CommunicationState.Faulted Then
                    Message.Add("client state is faulted. Message not sent!" & vbCrLf)
                Else
                    Dim StartTime As Date = Now
                    Dim Duration As TimeSpan
                    'Wait up to 16 seconds for the connection ConnName to be established
                    While client.ConnectionExists(ProNetName, ConnName) = False 'Wait until the required connection is made.
                        System.Threading.Thread.Sleep(1000) 'Pause for 1000ms
                        Duration = Now - StartTime
                        If Duration.Seconds > 16 Then Exit While
                    End While

                    If client.ConnectionExists(ProNetName, ConnName) = False Then
                        Message.AddWarning("Connection not available: " & ConnName & " in application network: " & ProNetName & vbCrLf)
                    Else
                        If bgwSendMessage.IsBusy Then
                            Message.AddWarning("Send Message backgroundworker is busy." & vbCrLf)
                        Else
                            Dim SendMessageParams As New Main.clsSendMessageParams
                            SendMessageParams.ProjectNetworkName = ProNetName
                            SendMessageParams.ConnectionName = ConnName
                            SendMessageParams.Message = XMsg
                            bgwSendMessage.RunWorkerAsync(SendMessageParams)
                            If ShowXMessages Then
                                Message.XAddText("Message sent to " & "[" & ProNetName & "]." & ConnName & ":" & vbCrLf, "XmlSentNotice")
                                Message.XAddXml(XMsg)
                                Message.XAddText(vbCrLf, "Normal") 'Add extra line
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception
                Message.AddWarning(ex.Message & vbCrLf)
            End Try
        End If
    End Sub

    Public Sub SendXMessageExtWait(ByVal ProNetName As String, ByVal ConnName As String, ByVal XMsg As String)
        'Send the XMsg to the application with the connection name ConnName and Project Network Name ProNetName.
        'Wait for the connection to be made.
        'This version can send the XMessage to a connection external to the current Project Network.
        If IsNothing(client) Then
            Message.Add("No client connection available!" & vbCrLf)
        Else
            If client.State = ServiceModel.CommunicationState.Faulted Then
                Message.Add("client state is faulted. Message not sent!" & vbCrLf)
            Else
                Dim StartTime As Date = Now
                Dim Duration As TimeSpan
                'Wait up to 16 seconds for the connection ConnName to be established
                While client.ConnectionExists(ProNetName, ConnName) = False
                    System.Threading.Thread.Sleep(1000) 'Pause for 1000ms
                    Duration = Now - StartTime
                    If Duration.Seconds > 16 Then Exit While
                End While

                If client.ConnectionExists(ProNetName, ConnName) = False Then
                    Message.AddWarning("Connection not available: " & ConnName & " in application network: " & ProNetName & vbCrLf)
                Else
                    If bgwSendMessage.IsBusy Then
                        Message.AddWarning("Send Message backgroundworker is busy." & vbCrLf)
                    Else
                        Dim SendMessageParams As New Main.clsSendMessageParams
                        SendMessageParams.ProjectNetworkName = ProNetName
                        SendMessageParams.ConnectionName = ConnName
                        SendMessageParams.Message = XMsg
                        bgwSendMessage.RunWorkerAsync(SendMessageParams)
                        If ShowXMessages Then
                            Message.XAddText("Message sent to " & "[" & ProNetName & "]." & ConnName & ":" & vbCrLf, "XmlSentNotice")
                            Message.XAddXml(XMsg)
                            Message.XAddText(vbCrLf, "Normal") 'Add extra line
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Public Sub XMsgInstruction(ByVal Info As String, ByVal Locn As String)
        'Send the XMessage Instruction to the JavaScript function XMsgInstruction for processing.
        Me.WebBrowser1.Document.InvokeScript("XMsgInstruction", New String() {Info, Locn})
    End Sub

    'END Run an XMessage -------------------------------------------------------------------------------------------


    'Get Information ===============================================================================================

    Public Function GetFormNo() As String
        'Return the Form Number of the current instance of the WebPage form.
        'Return FormNo.ToString
        Return "-1" 'The Main Form is not a Web Page form.
    End Function

    Public Function GetParentFormNo() As String
        'Return the Form Number of the Parent Form (that called this form).
        'Return ParentWebPageFormNo.ToString
        Return "-1" 'The Main Form does not have a Parent Web Page.
    End Function

    Public Function GetConnectionName() As String
        'Return the Connection Name of the Project.
        Return ConnectionName
    End Function

    Public Function GetProNetName() As String
        'Return the Project Network Name of the Project.
        Return ProNetName
    End Function

    Public Sub ParentProjectName(ByVal FormName As String, ByVal ItemName As String)
        'Return the Parent Project name:
        RestoreSetting(FormName, ItemName, Project.ParentProjectName)
    End Sub

    Public Sub ParentProjectPath(ByVal FormName As String, ByVal ItemName As String)
        'Return the Parent Project path:
        RestoreSetting(FormName, ItemName, Project.ParentProjectPath)
    End Sub

    Public Sub ParentProjectParameterValue(ByVal FormName As String, ByVal ItemName As String, ByVal ParameterName As String)
        'Return the specified Parent Project parameter value:
        RestoreSetting(FormName, ItemName, Project.ParentParameter(ParameterName).Value)
    End Sub

    Public Sub ProjectParameterValue(ByVal FormName As String, ByVal ItemName As String, ByVal ParameterName As String)
        'Return the specified Project parameter value:
        RestoreSetting(FormName, ItemName, Project.Parameter(ParameterName).Value)
    End Sub

    Public Sub ProjectNetworkName(ByVal FormName As String, ByVal ItemName As String)
        'Return the name of the Project Network:
        RestoreSetting(FormName, ItemName, Project.Parameter("ProNetName").Value)
    End Sub

    'END Get Information -------------------------------------------------------------------------------------------


    'Open a Web Page ===============================================================================================

    Public Sub OpenWebPage(ByVal FileName As String)
        'Open the web page with the specified File Name.

        If FileName = "" Then

        Else
            'First check if the HTML file is already open:
            Dim FileFound As Boolean = False
            If WebPageFormList.Count = 0 Then

            Else
                Dim I As Integer
                For I = 0 To WebPageFormList.Count - 1
                    If WebPageFormList(I) Is Nothing Then

                    Else
                        If WebPageFormList(I).FileName = FileName Then
                            FileFound = True
                            WebPageFormList(I).BringToFront
                        End If
                    End If
                Next
            End If

            If FileFound = False Then
                Dim FormNo As Integer = OpenNewWebPage()
                WebPageFormList(FormNo).FileName = FileName
                WebPageFormList(FormNo).OpenDocument
                WebPageFormList(FormNo).BringToFront
            End If
        End If
    End Sub

    'END Open a Web Page -------------------------------------------------------------------------------------------


    'Open and Close Projects =======================================================================================

    Public Sub OpenProjectAtRelativePath(ByVal RelativePath As String, ByVal ConnectionName As String)
        'Open the Project at the specified Relative Path using the specified Connection Name.

        Dim ProjectPath As String
        If RelativePath.StartsWith("\") Then
            ProjectPath = Project.Path & RelativePath
            client.StartProjectAtPath(ProjectPath, ConnectionName)
        Else
            ProjectPath = Project.Path & "\" & RelativePath
            client.StartProjectAtPath(ProjectPath, ConnectionName)
        End If
    End Sub

    Public Sub CheckOpenProjectAtRelativePath(ByVal RelativePath As String, ByVal ConnectionName As String)
        'Check if the project at the specified Relative Path is open.
        'Open it if it is not already open.
        'Open the Project at the specified Relative Path using the specified Connection Name.

        Dim ProjectPath As String
        If RelativePath.StartsWith("\") Then
            ProjectPath = Project.Path & RelativePath
            If client.ProjectOpen(ProjectPath) Then
                'Project is already open.
            Else
                client.StartProjectAtPath(ProjectPath, ConnectionName)
            End If
        Else
            ProjectPath = Project.Path & "\" & RelativePath
            If client.ProjectOpen(ProjectPath) Then
                'Project is already open.
            Else
                client.StartProjectAtPath(ProjectPath, ConnectionName)
            End If
        End If
    End Sub

    Public Sub OpenProjectAtProNetPath(ByVal RelativePath As String, ByVal ConnectionName As String)
        'Open the Project at the specified Path (relative to the Project Network Path) using the specified Connection Name.

        Dim ProjectPath As String
        If RelativePath.StartsWith("\") Then
            If Project.ParameterExists("ProNetPath") Then
                ProjectPath = Project.GetParameter("ProNetPath") & RelativePath
                client.StartProjectAtPath(ProjectPath, ConnectionName)
            Else
                Message.AddWarning("The Project Network Path is not known." & vbCrLf)
            End If
        Else
            If Project.ParameterExists("ProNetPath") Then
                ProjectPath = Project.GetParameter("ProNetPath") & "\" & RelativePath
                client.StartProjectAtPath(ProjectPath, ConnectionName)
            Else
                Message.AddWarning("The Project Network Path is not known." & vbCrLf)
            End If
        End If
    End Sub

    Public Sub CheckOpenProjectAtProNetPath(ByVal RelativePath As String, ByVal ConnectionName As String)
        'Check if the project at the specified Path (relative to the Project Network Path) is open.
        'Open it if it is not already open.
        'Open the Project at the specified Path using the specified Connection Name.

        Dim ProjectPath As String
        If RelativePath.StartsWith("\") Then
            If Project.ParameterExists("ProNetPath") Then
                ProjectPath = Project.GetParameter("ProNetPath") & RelativePath
                'client.StartProjectAtPath(ProjectPath, ConnectionName)
                If client.ProjectOpen(ProjectPath) Then
                    'Project is already open.
                Else
                    client.StartProjectAtPath(ProjectPath, ConnectionName)
                End If
            Else
                Message.AddWarning("The Project Network Path is not known." & vbCrLf)
            End If
        Else
            If Project.ParameterExists("ProNetPath") Then
                ProjectPath = Project.GetParameter("ProNetPath") & "\" & RelativePath
                'client.StartProjectAtPath(ProjectPath, ConnectionName)
                If client.ProjectOpen(ProjectPath) Then
                    'Project is already open.
                Else
                    client.StartProjectAtPath(ProjectPath, ConnectionName)
                End If
            Else
                Message.AddWarning("The Project Network Path is not known." & vbCrLf)
            End If
        End If
    End Sub


    Public Sub CloseProjectAtConnection(ByVal ProNetName As String, ByVal ConnectionName As String)
        'Close the Project at the specified connection.

        If IsNothing(client) Then
            Message.Add("No client connection available!" & vbCrLf)
        Else
            If client.State = ServiceModel.CommunicationState.Faulted Then
                Message.Add("client state is faulted. Message not sent!" & vbCrLf)
            Else
                'Create the XML instructions to close the application at the connection.
                Dim decl As New XDeclaration("1.0", "utf-8", "yes")
                Dim doc As New XDocument(decl, Nothing) 'Create an XDocument to store the instructions.
                Dim xmessage As New XElement("XMsg") 'This indicates the start of the message in the XMessage class

                'NOTE: No reply expected. No need to provide the following client information(?)
                'Dim clientConnName As New XElement("ClientConnectionName", Me.ConnectionName)
                'xmessage.Add(clientConnName)

                Dim command As New XElement("Command", "Close")
                xmessage.Add(command)
                doc.Add(xmessage)

                'Show the message sent:
                Message.XAddText("Message sent to: [" & ProNetName & "]." & ConnectionName & ":" & vbCrLf, "XmlSentNotice")
                Message.XAddXml(doc.ToString)
                Message.XAddText(vbCrLf, "Normal") 'Add extra line

                client.SendMessage(ProNetName, ConnectionName, doc.ToString)
            End If
        End If
    End Sub

    'END Open and Close Projects -----------------------------------------------------------------------------------


    'System Methods ================================================================================================

    Public Sub SaveHtmlSettings(ByVal xSettings As String, ByVal FileName As String)
        'Save the Html settings for a web page.

        'Convert the XSettings to XML format:
        Dim XmlHeader As String = "<?xml version=""1.0"" encoding=""utf-8"" standalone=""yes""?>"
        Dim XDocSettings As New System.Xml.Linq.XDocument

        Try
            XDocSettings = System.Xml.Linq.XDocument.Parse(XmlHeader & vbCrLf & xSettings)
        Catch ex As Exception
            Message.AddWarning("Error saving HTML settings file. " & ex.Message & vbCrLf)
        End Try

        Project.SaveXmlData(FileName, XDocSettings)
    End Sub

    Public Sub RestoreHtmlSettings()
        'Restore the Html settings for a web page.

        Dim SettingsFileName As String = WorkflowFileName & "Settings"
        Dim XDocSettings As New System.Xml.Linq.XDocument
        Project.ReadXmlData(SettingsFileName, XDocSettings)

        If XDocSettings Is Nothing Then
            'Message.Add("No HTML Settings file : " & SettingsFileName & vbCrLf)
        Else
            Dim XSettings As New System.Xml.XmlDocument
            Try
                XSettings.LoadXml(XDocSettings.ToString)
                'Run the Settings file:
                XSeq.RunXSequence(XSettings, Status)
            Catch ex As Exception
                Message.AddWarning("Error restoring HTML settings. " & ex.Message & vbCrLf)
            End Try
        End If
    End Sub

    Public Sub RestoreSetting(ByVal FormName As String, ByVal ItemName As String, ByVal ItemValue As String)
        'Restore the setting value with the specified Form Name and Item Name.
        Me.WebBrowser1.Document.InvokeScript("RestoreSetting", New String() {FormName, ItemName, ItemValue})
    End Sub

    Public Sub RestoreOption(ByVal SelectId As String, ByVal OptionText As String)
        'Restore the Option text in the Select control with the Id SelectId.
        Me.WebBrowser1.Document.InvokeScript("RestoreOption", New String() {SelectId, OptionText})
    End Sub

    Private Sub SaveWebPageSettings()
        'Call the SaveSettings JavaScript function:
        Try
            Me.WebBrowser1.Document.InvokeScript("SaveSettings")
        Catch ex As Exception
            Message.AddWarning("Web page settings not saved: " & ex.Message & vbCrLf)
        End Try
    End Sub

    'END System Methods --------------------------------------------------------------------------------------------


    'Legacy Code (These methods should no longer be used) ==========================================================

    Public Sub JSMethodTest1()
        'Test method that is called from JavaScript.
        Message.Add("JSMethodTest1 called OK." & vbCrLf)
    End Sub

    Public Sub JSMethodTest2(ByVal Var1 As String, ByVal Var2 As String)
        'Test method that is called from JavaScript.
        Message.Add("Var1 = " & Var1 & " Var2 = " & Var2 & vbCrLf)
    End Sub

    Public Sub JSDisplayXml(ByRef XDoc As XDocument)
        Message.Add(XDoc.ToString & vbCrLf & vbCrLf)
    End Sub

    Public Sub ShowMessage(ByVal Msg As String)
        Message.Add(Msg)
    End Sub

    Public Sub AddText(ByVal Msg As String, ByVal TextType As String)
        Message.AddText(Msg, TextType)
    End Sub

    'END Legacy Code -----------------------------------------------------------------------------------------------


#End Region 'Methods Called by JavaScript -------------------------------------------------------------------------------------------------------------------------------


#Region " Project Events Code"

    Private Sub Project_Message(Msg As String) Handles Project.Message
        'Display the Project message:
        Message.Add(Msg & vbCrLf)
    End Sub

    Private Sub Project_ErrorMessage(Msg As String) Handles Project.ErrorMessage
        'Display the Project error message:
        Message.AddWarning(Msg & vbCrLf)
    End Sub

    Private Sub Project_Closing() Handles Project.Closing
        'The current project is closing.
        CloseProject()
        'SaveFormSettings() 'Save the form settings - they are saved in the Project before is closes.
        'SaveProjectSettings() 'Update this subroutine if project settings need to be saved.
        'Project.Usage.SaveUsageInfo() 'Save the current project usage information.
        'Project.UnlockProject() 'Unlock the current project before it Is closed.
        'If ConnectedToComNet Then DisconnectFromComNet() 'ADDED 9Apr20
    End Sub

    Private Sub CloseProject()
        'Close the Project:
        SaveFormSettings() 'Save the form settings - they are saved in the Project before is closes.
        SaveProjectSettings() 'Update this subroutine if project settings need to be saved.
        Project.Usage.SaveUsageInfo() 'Save the current project usage information.
        Project.UnlockProject() 'Unlock the current project before it Is closed.
        If ConnectedToComNet Then DisconnectFromComNet() 'ADDED 9Apr20
    End Sub

    Private Sub Project_Selected() Handles Project.Selected
        'A new project has been selected.
        OpenProject()
        'RestoreFormSettings()
        'Project.ReadProjectInfoFile()

        'Project.ReadParameters()
        'Project.ReadParentParameters()
        'If Project.ParentParameterExists("ProNetName") Then
        '    Project.AddParameter("ProNetName", Project.ParentParameter("ProNetName").Value, Project.ParentParameter("ProNetName").Description) 'AddParameter will update the parameter if it already exists.
        '    ProNetName = Project.Parameter("ProNetName").Value
        'Else
        '    ProNetName = Project.GetParameter("ProNetName")
        'End If
        'If Project.ParentParameterExists("ProNetPath") Then 'Get the parent parameter value - it may have been updated.
        '    Project.AddParameter("ProNetPath", Project.ParentParameter("ProNetPath").Value, Project.ParentParameter("ProNetPath").Description) 'AddParameter will update the parameter if it already exists.
        '    ProNetPath = Project.Parameter("ProNetPath").Value
        'Else
        '    ProNetPath = Project.GetParameter("ProNetPath") 'If the parameter does not exist, the value is set to ""
        'End If
        'Project.SaveParameters() 'These should be saved now - child projects look for parent parameters in the parameter file.

        'Project.LockProject() 'Lock the project while it is open in this application.

        'Project.Usage.StartTime = Now

        'ApplicationInfo.SettingsLocn = Project.SettingsLocn
        'Message.SettingsLocn = Project.SettingsLocn
        'Message.Show() 'Added 18May19

        ''Restore the new project settings:
        'RestoreProjectSettings() 'Update this subroutine if project settings need to be restored.

        'ShowProjectInfo()

        'If Project.ConnectOnOpen Then
        '    ConnectToComNet() 'The Project is set to connect when it is opened.
        'ElseIf ApplicationInfo.ConnectOnStartup Then
        '    ConnectToComNet() 'The Application is set to connect when it is started.
        'Else
        '    'Don't connect to ComNet.
        'End If

    End Sub

    Private Sub OpenProject()
        'Open the Project:
        RestoreFormSettings()
        Project.ReadProjectInfoFile()

        Project.ReadParameters()
        Project.ReadParentParameters()
        If Project.ParentParameterExists("ProNetName") Then
            Project.AddParameter("ProNetName", Project.ParentParameter("ProNetName").Value, Project.ParentParameter("ProNetName").Description) 'AddParameter will update the parameter if it already exists.
            ProNetName = Project.Parameter("ProNetName").Value
        Else
            ProNetName = Project.GetParameter("ProNetName")
        End If
        If Project.ParentParameterExists("ProNetPath") Then 'Get the parent parameter value - it may have been updated.
            Project.AddParameter("ProNetPath", Project.ParentParameter("ProNetPath").Value, Project.ParentParameter("ProNetPath").Description) 'AddParameter will update the parameter if it already exists.
            ProNetPath = Project.Parameter("ProNetPath").Value
        Else
            ProNetPath = Project.GetParameter("ProNetPath") 'If the parameter does not exist, the value is set to ""
        End If
        Project.SaveParameters() 'These should be saved now - child projects look for parent parameters in the parameter file.

        Project.LockProject() 'Lock the project while it is open in this application.

        Project.Usage.StartTime = Now

        ApplicationInfo.SettingsLocn = Project.SettingsLocn
        Message.SettingsLocn = Project.SettingsLocn
        Message.Show() 'Added 18May19

        'Restore the new project settings:
        RestoreProjectSettings() 'Update this subroutine if project settings need to be restored.

        ShowProjectInfo()

        If Project.ConnectOnOpen Then
            ConnectToComNet() 'The Project is set to connect when it is opened.
        ElseIf ApplicationInfo.ConnectOnStartup Then
            ConnectToComNet() 'The Application is set to connect when it is started.
        Else
            'Don't connect to ComNet.
        End If
    End Sub

    Private Sub chkConnect_LostFocus(sender As Object, e As EventArgs) Handles chkConnect.LostFocus
        If chkConnect.Checked Then
            Project.ConnectOnOpen = True
        Else
            Project.ConnectOnOpen = False
        End If
        Project.SaveProjectInfoFile()
    End Sub

#End Region 'Project Events Code

#Region " Online/Offline Code" '=========================================================================================================================================

    Private Sub btnOnline_Click(sender As Object, e As EventArgs) Handles btnOnline.Click
        'Connect to or disconnect from the Message System (ComNet).
        If ConnectedToComNet = False Then
            ConnectToComNet()
        Else
            DisconnectFromComNet()
        End If
    End Sub

    Private Sub ConnectToComNet()
        'Connect to the Message Service. (ComNet)

        If IsNothing(client) Then
            client = New ServiceReference1.MsgServiceClient(New System.ServiceModel.InstanceContext(New MsgServiceCallback))
        End If

        'UPDATE 14 Feb 2021 - If the VS2019 version of the ADVL Network is running it may not detected by ComNetRunning()!
        'Check if the Message Service is running by trying to open a connection:
        Try
            client.Endpoint.Binding.SendTimeout = New System.TimeSpan(0, 0, 16) 'Temporarily set the send timeaout to 16 seconds (8 seconds is too short for a slow computer!)
            ConnectionName = ApplicationInfo.Name 'This name will be modified if it is already used in an existing connection.
            ConnectionName = client.Connect(ProNetName, ApplicationInfo.Name, ConnectionName, Project.Name, Project.Description, Project.Type, Project.Path, False, False)
            If ConnectionName <> "" Then
                Message.Add("Connected to the Andorville™ Network with Connection Name: [" & ProNetName & "]." & ConnectionName & vbCrLf)
                client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeout to 1 hour
                btnOnline.Text = "Online"
                btnOnline.ForeColor = Color.ForestGreen
                ConnectedToComNet = True
                SendApplicationInfo()
                SendProjectInfo()
                client.GetAdvlNetworkAppInfoAsync() 'Update the Exe Path in case it has changed. This path may be needed in the future to start the ComNet (Message Service).

                bgwComCheck.WorkerReportsProgress = True
                bgwComCheck.WorkerSupportsCancellation = True
                If bgwComCheck.IsBusy Then
                    'The ComCheck thread is already running.
                Else
                    bgwComCheck.RunWorkerAsync() 'Start the ComCheck thread.
                End If
                Exit Sub 'Connection made OK
            Else
                'Message.Add("Connection to the Andorville™ Network failed!" & vbCrLf)
                Message.Add("The Andorville™ Network was not found. Attempting to start it." & vbCrLf)
                client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeout to 1 hour
            End If
        Catch ex As System.TimeoutException
            Message.Add("Message Service Check Timeout error. Check if the Andorville™ Network (Message Service) is running." & vbCrLf)
            client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeout to 1 hour
            Message.Add("Attempting to start the Message Service." & vbCrLf)
        Catch ex As Exception
            Message.Add("Error message: " & ex.Message & vbCrLf)
            client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeout to 1 hour
            Message.Add("Attempting to start the Message Service." & vbCrLf)
        End Try
        'END UPDATE

        If ComNetRunning() Then
            'The Application.Lock file has been found at AdvlNetworkAppPath
            'The Message Service is Running.
        Else 'The Message Service is NOT running'
            'Start the Andorville™ Network:
            If AdvlNetworkAppPath = "" Then
                Message.AddWarning("Andorville™ Network application path is unknown." & vbCrLf)
            Else
                If System.IO.File.Exists(AdvlNetworkExePath) Then 'OK to start the Message Service application:
                    Shell(Chr(34) & AdvlNetworkExePath & Chr(34), AppWinStyle.NormalFocus) 'Start Message Service application with no argument
                Else
                    'Incorrect Message Service Executable path.
                    Message.AddWarning("Andorville™ Network exe file not found. Service not started." & vbCrLf)
                End If
            End If
        End If

        'Try to fix a faulted client state:
        If client.State = ServiceModel.CommunicationState.Faulted Then
            client = Nothing
            client = New ServiceReference1.MsgServiceClient(New System.ServiceModel.InstanceContext(New MsgServiceCallback))
        End If

        If client.State = ServiceModel.CommunicationState.Faulted Then
            Message.AddWarning("Client state is faulted. Connection not made!" & vbCrLf)
        Else
            Try
                client.Endpoint.Binding.SendTimeout = New System.TimeSpan(0, 0, 16) 'Temporarily set the send timeaout to 16 seconds (8 seconds is too short for a slow computer!)

                ConnectionName = ApplicationInfo.Name 'This name will be modified if it is already used in an existing connection.
                ConnectionName = client.Connect(ProNetName, ApplicationInfo.Name, ConnectionName, Project.Name, Project.Description, Project.Type, Project.Path, False, False)

                If ConnectionName <> "" Then
                    Message.Add("Connected to the Andorville™ Network with Connection Name: [" & ProNetName & "]." & ConnectionName & vbCrLf)
                    client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeout to 1 hour
                    btnOnline.Text = "Online"
                    btnOnline.ForeColor = Color.ForestGreen
                    ConnectedToComNet = True
                    SendApplicationInfo()
                    SendProjectInfo()
                    client.GetAdvlNetworkAppInfoAsync() 'Update the Exe Path in case it has changed. This path may be needed in the future to start the ComNet (Message Service).

                    bgwComCheck.WorkerReportsProgress = True
                    bgwComCheck.WorkerSupportsCancellation = True
                    If bgwComCheck.IsBusy Then
                        'The ComCheck thread is already running.
                    Else
                        bgwComCheck.RunWorkerAsync() 'Start the ComCheck thread.
                    End If

                Else
                    Message.Add("Connection to the Andorville™ Network failed!" & vbCrLf)
                    client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeout to 1 hour
                End If
            Catch ex As System.TimeoutException
                Message.Add("Timeout error. Check if the Andorville™ Network (Message Service) is running." & vbCrLf)
            Catch ex As Exception
                Message.Add("Error message: " & ex.Message & vbCrLf)
                client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeout to 1 hour
            End Try
        End If
    End Sub

    Private Sub ConnectToComNet(ByVal ConnName As String)
        'Connect to the Message Service (ComNet) with the connection name ConnName.

        'UPDATE 14 Feb 2021 - If the VS2019 version of the ADVL Network is running it may not be detected by ComNetRunning()!
        'Check if the Message Service is running by trying to open a connection:

        If IsNothing(client) Then
            client = New ServiceReference1.MsgServiceClient(New System.ServiceModel.InstanceContext(New MsgServiceCallback))
        End If

        Try
            client.Endpoint.Binding.SendTimeout = New System.TimeSpan(0, 0, 16) 'Temporarily set the send timeaout to 16 seconds (8 seconds is too short for a slow computer!)
            ConnectionName = ConnName 'This name will be modified if it is already used in an existing connection.
            ConnectionName = client.Connect(ProNetName, ApplicationInfo.Name, ConnectionName, Project.Name, Project.Description, Project.Type, Project.Path, False, False)
            If ConnectionName <> "" Then
                Message.Add("Connected to the Andorville™ Network with Connection Name: [" & ProNetName & "]." & ConnectionName & vbCrLf)
                client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeout to 1 hour
                btnOnline.Text = "Online"
                btnOnline.ForeColor = Color.ForestGreen
                ConnectedToComNet = True
                SendApplicationInfo()
                SendProjectInfo()
                client.GetAdvlNetworkAppInfoAsync() 'Update the Exe Path in case it has changed. This path may be needed in the future to start the ComNet (Message Service).

                bgwComCheck.WorkerReportsProgress = True
                bgwComCheck.WorkerSupportsCancellation = True
                If bgwComCheck.IsBusy Then
                    'The ComCheck thread is already running.
                Else
                    bgwComCheck.RunWorkerAsync() 'Start the ComCheck thread.
                End If
                Exit Sub 'Connection made OK
            Else
                'Message.Add("Connection to the Andorville™ Network failed!" & vbCrLf)
                Message.Add("The Andorville™ Network was not found. Attempting to start it." & vbCrLf)
                client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeout to 1 hour
            End If
        Catch ex As System.TimeoutException
            Message.Add("Message Service Check Timeout error. Check if the Andorville™ Network (Message Service) is running." & vbCrLf)
            client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeout to 1 hour
            Message.Add("Attempting to start the Message Service." & vbCrLf)
        Catch ex As Exception
            Message.Add("Error message: " & ex.Message & vbCrLf)
            client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeout to 1 hour
            Message.Add("Attempting to start the Message Service." & vbCrLf)
        End Try
        'END UPDATE

        If ConnectedToComNet = False Then
            If IsNothing(client) Then
                client = New ServiceReference1.MsgServiceClient(New System.ServiceModel.InstanceContext(New MsgServiceCallback))
            End If

            'Try to fix a faulted client state:
            If client.State = ServiceModel.CommunicationState.Faulted Then
                client = Nothing
                client = New ServiceReference1.MsgServiceClient(New System.ServiceModel.InstanceContext(New MsgServiceCallback))
            End If

            If client.State = ServiceModel.CommunicationState.Faulted Then
                Message.AddWarning("client state is faulted. Connection not made!" & vbCrLf)
            Else
                Try
                    client.Endpoint.Binding.SendTimeout = New System.TimeSpan(0, 0, 16) 'Temporarily set the send timeout to 16 seconds (8 seconds is too short for a slow computer!)
                    ConnectionName = ConnName 'This name will be modified if it is already used in an existing connection.
                    ConnectionName = client.Connect(ProNetName, ApplicationInfo.Name, ConnectionName, Project.Name, Project.Description, Project.Type, Project.Path, False, False)

                    If ConnectionName <> "" Then
                        Message.Add("Connected to the Andorville™ Network with Connection Name: [" & ProNetName & "]." & ConnectionName & vbCrLf)
                        client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeout to 1 hour
                        btnOnline.Text = "Online"
                        btnOnline.ForeColor = Color.ForestGreen
                        ConnectedToComNet = True
                        SendApplicationInfo()
                        SendProjectInfo()
                        client.GetAdvlNetworkAppInfoAsync() 'Update the Exe Path in case it has changed. This path may be needed in the future to start the ComNet (Message Service).

                        bgwComCheck.WorkerReportsProgress = True
                        bgwComCheck.WorkerSupportsCancellation = True
                        If bgwComCheck.IsBusy Then
                            'The ComCheck thread is already running.
                        Else
                            bgwComCheck.RunWorkerAsync() 'Start the ComCheck thread.
                        End If

                    Else
                        Message.Add("Connection to the Andorville™ Network failed!" & vbCrLf)
                        client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeout to 1 hour
                    End If
                Catch ex As System.TimeoutException
                    Message.Add("Timeout error. Check if the Andorville™ Network (Message Service) is running." & vbCrLf)
                Catch ex As Exception
                    Message.Add("Error message: " & ex.Message & vbCrLf)
                    client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeout to 1 hour
                End Try
            End If
        Else
            Message.AddWarning("Already connected to the Andorville™ Network (Message Service)." & vbCrLf)
        End If
    End Sub

    Private Sub bgwCancelConn_DoWork(sender As Object, e As DoWorkEventArgs) Handles bgwCancelConn.DoWork
        'Show a MessageBox with the option to run the application in Stand-alone mode.
        Dim dr As System.Windows.Forms.DialogResult
        dr = MessageBox.Show("Press OK to run in Stand-alone mode.", "Attempting to connect to the Message Service.", MessageBoxButtons.OKCancel)
        If dr = System.Windows.Forms.DialogResult.OK Then
            SyncLock myLock
                ConnCancelled = True
                client.Close()
                Debug.WriteLine("Client Closed")
            End SyncLock
        End If
    End Sub

    Private Sub DisconnectFromComNet()
        'Disconnect from the Communication Network (Message Service).

        If ConnectedToComNet = True Then
            If IsNothing(client) Then
                Message.Add("Already disconnected from the Andorville™ Network (Message Service)." & vbCrLf)
                btnOnline.Text = "Offline"
                btnOnline.ForeColor = Color.Red
                ConnectedToComNet = False
                ConnectionName = ""
            Else
                If client.State = ServiceModel.CommunicationState.Faulted Then
                    Message.Add("client state is faulted." & vbCrLf)
                    ConnectionName = ""
                Else
                    Try
                        client.Disconnect(ProNetName, ConnectionName)
                        btnOnline.Text = "Offline"
                        btnOnline.ForeColor = Color.Red
                        ConnectedToComNet = False
                        ConnectionName = ""
                        Message.Add("Disconnected from the Andorville™ Network (Message Service)." & vbCrLf)

                        If bgwComCheck.IsBusy Then
                            bgwComCheck.CancelAsync()
                        End If

                    Catch ex As Exception
                        Message.AddWarning("Error disconnecting from Andorville™ Network (Message Service): " & ex.Message & vbCrLf)
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub SendApplicationInfo()
        'Send the application information to the Network application.

        If IsNothing(client) Then
            Message.Add("No client connection available!" & vbCrLf)
        Else
            If client.State = ServiceModel.CommunicationState.Faulted Then
                Message.Add("Client state is faulted. Message not sent!" & vbCrLf)
            Else
                'Create the XML instructions to send application information.
                Dim decl As New XDeclaration("1.0", "utf-8", "yes")
                Dim doc As New XDocument(decl, Nothing) 'Create an XDocument to store the instructions.
                Dim xmessage As New XElement("XMsg") 'This indicates the start of the message in the XMessage class
                Dim applicationInfo As New XElement("ApplicationInfo")
                Dim name As New XElement("Name", Me.ApplicationInfo.Name)
                applicationInfo.Add(name)

                Dim text As New XElement("Text", "Distributions")
                applicationInfo.Add(text)

                Dim exePath As New XElement("ExecutablePath", Me.ApplicationInfo.ExecutablePath)
                applicationInfo.Add(exePath)

                Dim directory As New XElement("Directory", Me.ApplicationInfo.ApplicationDir)
                applicationInfo.Add(directory)
                Dim description As New XElement("Description", Me.ApplicationInfo.Description)
                applicationInfo.Add(description)
                xmessage.Add(applicationInfo)
                doc.Add(xmessage)

                'Show the message sent to ComNet:
                Message.XAddText("Message sent to " & "Message Service" & ":" & vbCrLf, "XmlSentNotice")
                Message.XAddXml(doc.ToString)
                Message.XAddText(vbCrLf, "Normal") 'Add extra line

                client.SendMessage("", "MessageService", doc.ToString)
            End If
        End If
    End Sub

    Private Sub SendProjectInfo()
        'Send the project information to the Network application.

        If ConnectedToComNet = False Then
            Message.AddWarning("The application is not connected to the Message Service." & vbCrLf)
        Else 'Connected to the Message Service (ComNet).
            If IsNothing(client) Then
                Message.Add("No client connection available!" & vbCrLf)
            Else
                If client.State = ServiceModel.CommunicationState.Faulted Then
                    Message.Add("Client state is faulted. Message not sent!" & vbCrLf)
                Else
                    'Construct the XMessage to send to AppNet:
                    Dim decl As New XDeclaration("1.0", "utf-8", "yes")
                    Dim doc As New XDocument(decl, Nothing) 'Create an XDocument to store the instructions.
                    Dim xmessage As New XElement("XMsg") 'This indicates the start of the message in the XMessage class
                    Dim projectInfo As New XElement("ProjectInfo")

                    Dim Path As New XElement("Path", Project.Path)
                    projectInfo.Add(Path)
                    xmessage.Add(projectInfo)
                    doc.Add(xmessage)

                    'Show the message sent to the Message Service:
                    Message.XAddText("Message sent to " & "Message Service" & ":" & vbCrLf, "XmlSentNotice")
                    Message.XAddXml(doc.ToString)
                    Message.XAddText(vbCrLf, "Normal") 'Add extra line
                    client.SendMessage("", "MessageService", doc.ToString)
                End If
            End If
        End If
    End Sub

    Public Sub SendProjectInfo(ByVal ProjectPath As String)
        'Send the project information to the Network application.
        'This version of SendProjectInfo uses the ProjectPath argument.

        If ConnectedToComNet = False Then
            Message.AddWarning("The application is not connected to the Message Service." & vbCrLf)
        Else 'Connected to the Message Service (ComNet).
            If IsNothing(client) Then
                Message.Add("No client connection available!" & vbCrLf)
            Else
                If client.State = ServiceModel.CommunicationState.Faulted Then
                    Message.Add("Client state is faulted. Message not sent!" & vbCrLf)
                Else
                    'Construct the XMessage to send to AppNet:
                    Dim decl As New XDeclaration("1.0", "utf-8", "yes")
                    Dim doc As New XDocument(decl, Nothing) 'Create an XDocument to store the instructions.
                    Dim xmessage As New XElement("XMsg") 'This indicates the start of the message in the XMessage class
                    Dim projectInfo As New XElement("ProjectInfo")

                    'Dim Path As New XElement("Path", Project.Path)
                    Dim Path As New XElement("Path", ProjectPath)
                    projectInfo.Add(Path)
                    xmessage.Add(projectInfo)
                    doc.Add(xmessage)

                    'Show the message sent to the Message Service:
                    Message.XAddText("Message sent to " & "Message Service" & ":" & vbCrLf, "XmlSentNotice")
                    Message.XAddXml(doc.ToString)
                    Message.XAddText(vbCrLf, "Normal") 'Add extra line
                    client.SendMessage("", "MessageService", doc.ToString)
                End If
            End If
        End If
    End Sub

    Private Function ComNetRunning() As Boolean
        'Return True if ComNet (Message Service) is running.
        ''If System.IO.File.Exists(MsgServiceAppPath & "\Application.Lock") Then
        'If System.IO.File.Exists(AdvlNetworkAppPath & "\Application.Lock") Then
        '    Return True
        'Else
        '    Return False
        'End If

        'If MsgServiceAppPath = "" Then
        If AdvlNetworkAppPath = "" Then
            'Message.Add("Message Service application path is not known." & vbCrLf)
            Message.Add("Andorville™ Network application path is not known." & vbCrLf)
            'Message.Add("Run the Message Service before connecting to update the path." & vbCrLf)
            Message.Add("Run the Andorville™ Network before connecting to update the path." & vbCrLf)
            Return False
        Else
            'If System.IO.File.Exists(MsgServiceAppPath & "\Application.Lock") Then
            If System.IO.File.Exists(AdvlNetworkAppPath & "\Application.Lock") Then
                'Message.Add("AppLock found - ComNet is running." & vbCrLf)
                Return True
            Else
                'Message.Add("AppLock not found - ComNet is running." & vbCrLf)
                Return False
            End If
        End If

    End Function

#End Region 'Online/Offline code ----------------------------------------------------------------------------------------------------------------------------------------

    Private Sub TabPage2_Enter(sender As Object, e As EventArgs) Handles TabPage2.Enter
        'Update the current duration:

        'txtCurrentDuration.Text = Project.Usage.CurrentDuration.Days.ToString.PadLeft(5, "0"c) & ":" &
        '                           Project.Usage.CurrentDuration.Hours.ToString.PadLeft(2, "0"c) & ":" &
        '                           Project.Usage.CurrentDuration.Minutes.ToString.PadLeft(2, "0"c) & ":" &
        '                           Project.Usage.CurrentDuration.Seconds.ToString.PadLeft(2, "0"c)

        txtCurrentDuration.Text = Project.Usage.CurrentDuration.Days.ToString.PadLeft(5, "0"c) & "d:" &
                           Project.Usage.CurrentDuration.Hours.ToString.PadLeft(2, "0"c) & "h:" &
                           Project.Usage.CurrentDuration.Minutes.ToString.PadLeft(2, "0"c) & "m:" &
                           Project.Usage.CurrentDuration.Seconds.ToString.PadLeft(2, "0"c) & "s"

        Timer1.Interval = 5000 '5 seconds
        Timer1.Enabled = True
        Timer1.Start()

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'Update the current duration:

        'txtCurrentDuration.Text = Project.Usage.CurrentDuration.Days.ToString.PadLeft(5, "0"c) & ":" &
        '                   Project.Usage.CurrentDuration.Hours.ToString.PadLeft(2, "0"c) & ":" &
        '                   Project.Usage.CurrentDuration.Minutes.ToString.PadLeft(2, "0"c) & ":" &
        '                   Project.Usage.CurrentDuration.Seconds.ToString.PadLeft(2, "0"c)

        txtCurrentDuration.Text = Project.Usage.CurrentDuration.Days.ToString.PadLeft(5, "0"c) & "d:" &
                   Project.Usage.CurrentDuration.Hours.ToString.PadLeft(2, "0"c) & "h:" &
                   Project.Usage.CurrentDuration.Minutes.ToString.PadLeft(2, "0"c) & "m:" &
                   Project.Usage.CurrentDuration.Seconds.ToString.PadLeft(2, "0"c) & "s"
    End Sub

    Private Sub TabPage2_Leave(sender As Object, e As EventArgs) Handles TabPage2.Leave
        Timer1.Enabled = False
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        'Add the current project to the Message Service list.

        If Project.ParentProjectName <> "" Then
            Message.AddWarning("This project has a parent: " & Project.ParentProjectName & vbCrLf)
            Message.AddWarning("Child projects can not be added to the list." & vbCrLf)
            Exit Sub
        End If

        If ConnectedToComNet = False Then
            Message.AddWarning("The application is not connected to the Message Service." & vbCrLf)
        Else 'Connected to the Message Service (ComNet).
            If IsNothing(client) Then
                Message.Add("No client connection available!" & vbCrLf)
            Else
                If client.State = ServiceModel.CommunicationState.Faulted Then
                    Message.Add("Client state is faulted. Message not sent!" & vbCrLf)
                Else
                    'Construct the XMessage to send to AppNet:
                    Dim decl As New XDeclaration("1.0", "utf-8", "yes")
                    Dim doc As New XDocument(decl, Nothing) 'Create an XDocument to store the instructions.
                    Dim xmessage As New XElement("XMsg") 'This indicates the start of the message in the XMessage class
                    Dim projectInfo As New XElement("ProjectInfo")

                    Dim Path As New XElement("Path", Project.Path)
                    projectInfo.Add(Path)
                    xmessage.Add(projectInfo)
                    doc.Add(xmessage)

                    'Show the message sent to AppNet:
                    Message.XAddText("Message sent to " & "Message Service" & ":" & vbCrLf, "XmlSentNotice")
                    Message.XAddXml(doc.ToString)
                    Message.XAddText(vbCrLf, "Normal") 'Add extra line
                    client.SendMessage("", "MessageService", doc.ToString)
                End If
            End If
        End If
    End Sub

    Private Sub btnOpenProject_Click(sender As Object, e As EventArgs) Handles btnOpenProject.Click

        If Project.Type = ADVL_Utilities_Library_1.Project.Types.Archive Then
            If IsNothing(ProjectArchive) Then
                ProjectArchive = New frmArchive
                ProjectArchive.Show()
                ProjectArchive.Title = "Project Archive"
                ProjectArchive.Path = Project.Path
            Else
                ProjectArchive.Show()
                ProjectArchive.BringToFront()
            End If
        Else
            Process.Start(Project.Path)
        End If

    End Sub

    Private Sub ProjectArchive_FormClosed(sender As Object, e As FormClosedEventArgs) Handles ProjectArchive.FormClosed
        ProjectArchive = Nothing
    End Sub

    Private Sub btnOpenSettings_Click(sender As Object, e As EventArgs) Handles btnOpenSettings.Click
        If Project.SettingsLocn.Type = ADVL_Utilities_Library_1.FileLocation.Types.Directory Then
            Process.Start(Project.SettingsLocn.Path)
        ElseIf Project.SettingsLocn.Type = ADVL_Utilities_Library_1.FileLocation.Types.Archive Then
            If IsNothing(SettingsArchive) Then
                SettingsArchive = New frmArchive
                SettingsArchive.Show()
                SettingsArchive.Title = "Settings Archive"
                SettingsArchive.Path = Project.SettingsLocn.Path
            Else
                SettingsArchive.Show()
                SettingsArchive.BringToFront()
            End If
        End If
    End Sub

    Private Sub SettingsArchive_FormClosed(sender As Object, e As FormClosedEventArgs) Handles SettingsArchive.FormClosed
        SettingsArchive = Nothing
    End Sub

    Private Sub btnOpenData_Click(sender As Object, e As EventArgs) Handles btnOpenData.Click
        If Project.DataLocn.Type = ADVL_Utilities_Library_1.FileLocation.Types.Directory Then
            Process.Start(Project.DataLocn.Path)
        ElseIf Project.DataLocn.Type = ADVL_Utilities_Library_1.FileLocation.Types.Archive Then
            If IsNothing(DataArchive) Then
                DataArchive = New frmArchive
                DataArchive.Show()
                DataArchive.Title = "Data Archive"
                DataArchive.Path = Project.DataLocn.Path
            Else
                DataArchive.Show()
                DataArchive.BringToFront()
            End If
        End If
    End Sub

    Private Sub DataArchive_FormClosed(sender As Object, e As FormClosedEventArgs) Handles DataArchive.FormClosed
        DataArchive = Nothing
    End Sub


    Private Sub btnOpenSystem_Click(sender As Object, e As EventArgs) Handles btnOpenSystem.Click
        If Project.SystemLocn.Type = ADVL_Utilities_Library_1.FileLocation.Types.Directory Then
            Process.Start(Project.SystemLocn.Path)
        ElseIf Project.SystemLocn.Type = ADVL_Utilities_Library_1.FileLocation.Types.Archive Then
            If IsNothing(SystemArchive) Then
                SystemArchive = New frmArchive
                SystemArchive.Show()
                SystemArchive.Title = "System Archive"
                SystemArchive.Path = Project.SystemLocn.Path
            Else
                SystemArchive.Show()
                SystemArchive.BringToFront()
            End If
        End If
    End Sub

    Private Sub SystemArchive_FormClosed(sender As Object, e As FormClosedEventArgs) Handles SystemArchive.FormClosed
        SystemArchive = Nothing
    End Sub

    Private Sub btnOpenAppDir_Click(sender As Object, e As EventArgs) Handles btnOpenAppDir.Click
        Process.Start(ApplicationInfo.ApplicationDir)
    End Sub

    Private Sub btnCreateArchive_Click(sender As Object, e As EventArgs) Handles btnCreateArchive.Click
        'Create a Project Archive file.
        If Project.Type = ADVL_Utilities_Library_1.Project.Types.Archive Then
            Message.Add("The Project is an Archive type. It is already in an archived format." & vbCrLf)

        Else
            'The project is contained in the directory Project.Path.
            'This directory and contents will be saved in a zip file in the parent directory with the same name but with extension .AdvlArchive.

            Dim ParentDir As String = System.IO.Directory.GetParent(Project.Path).FullName
            Dim ProjectArchiveName As String = System.IO.Path.GetFileName(Project.Path) & ".AdvlArchive"

            If My.Computer.FileSystem.FileExists(ParentDir & "\" & ProjectArchiveName) Then 'The Project Archive file already exists.
                Message.Add("The Project Archive file already exists: " & ParentDir & "\" & ProjectArchiveName & vbCrLf)
            Else 'The Project Archive file does not exist. OK to create the Archive.
                System.IO.Compression.ZipFile.CreateFromDirectory(Project.Path, ParentDir & "\" & ProjectArchiveName)

                'Remove all Lock files:
                Dim Zip As System.IO.Compression.ZipArchive
                Zip = System.IO.Compression.ZipFile.Open(ParentDir & "\" & ProjectArchiveName, IO.Compression.ZipArchiveMode.Update)
                Dim DeleteList As New List(Of String) 'List of entry names to delete
                Dim myEntry As System.IO.Compression.ZipArchiveEntry
                For Each entry As System.IO.Compression.ZipArchiveEntry In Zip.Entries
                    If entry.Name = "Project.Lock" Then
                        DeleteList.Add(entry.FullName)
                    End If
                Next
                For Each item In DeleteList
                    myEntry = Zip.GetEntry(item)
                    myEntry.Delete()
                Next
                Zip.Dispose()

                Message.Add("Project Archive file created: " & ParentDir & "\" & ProjectArchiveName & vbCrLf)
            End If
        End If
    End Sub

    Private Sub btnOpenArchive_Click(sender As Object, e As EventArgs) Handles btnOpenArchive.Click
        'Open a Project Archive file.

        'Use the OpenFileDialog to look for an .AdvlArchive file.      
        OpenFileDialog1.Title = "Select an Archived Project File"
        OpenFileDialog1.InitialDirectory = System.IO.Directory.GetParent(Project.Path).FullName 'Start looking in the ParentDir.
        OpenFileDialog1.Filter = "Archived Project|*.AdvlArchive"
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            Dim FileName As String = OpenFileDialog1.FileName
            OpenArchivedProject(FileName)
        End If
    End Sub

    Private Sub OpenArchivedProject(ByVal FilePath As String)
        'Open the archived project at the specified path.

        Dim Zip As System.IO.Compression.ZipArchive
        Try
            Zip = System.IO.Compression.ZipFile.OpenRead(FilePath)

            Dim Entry As System.IO.Compression.ZipArchiveEntry = Zip.GetEntry("Project_Info_ADVL_2.xml")
            If IsNothing(Entry) Then
                Message.AddWarning("The file is not an Archived Andorville Project." & vbCrLf)
                'Check if it is an Archive project type with a .AdvlProject extension.
                'NOTE: These are already zip files so no need to archive.

            Else
                Message.Add("The file is an Archived Andorville Project." & vbCrLf)
                Dim ParentDir As String = System.IO.Directory.GetParent(FilePath).FullName
                Dim ProjectName As String = System.IO.Path.GetFileNameWithoutExtension(FilePath)
                Message.Add("The Project will be expanded in the directory: " & ParentDir & vbCrLf)
                Message.Add("The Project name will be: " & ProjectName & vbCrLf)
                Zip.Dispose()
                If System.IO.Directory.Exists(ParentDir & "\" & ProjectName) Then
                    Message.AddWarning("The Project already exists: " & ParentDir & "\" & ProjectName & vbCrLf)
                Else
                    System.IO.Compression.ZipFile.ExtractToDirectory(FilePath, ParentDir & "\" & ProjectName) 'Extract the project from the archive                   
                    Project.AddProjectToList(ParentDir & "\" & ProjectName)
                    'Open the new project                 
                    CloseProject()  'Close the current project
                    Project.SelectProject(ParentDir & "\" & ProjectName) 'Select the project at the specifed path.
                    OpenProject() 'Open the selected project.
                End If
            End If
        Catch ex As Exception
            Message.AddWarning("Error opening Archived Andorville Project: " & ex.Message & vbCrLf)
        End Try
    End Sub

    Private Sub TabPage2_DragEnter(sender As Object, e As DragEventArgs) Handles TabPage2.DragEnter
        'DragEnter: An object has been dragged into TabPage2 - Project Information tab.
        'This code is required to get the link to the item(s) being dragged into Project Information:
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Link
        End If
    End Sub

    Private Sub TabPage2_DragDrop(sender As Object, e As DragEventArgs) Handles TabPage2.DragDrop
        'A file has been dropped into the Project Information tab.

        Dim Path As String()
        Path = e.Data.GetData(DataFormats.FileDrop)
        Dim I As Integer

        If Path.Count > 0 Then
            If Path.Count > 1 Then
                Message.AddWarning("More than one file has been dropped into the Project Information tab. Only the first one will be opened." & vbCrLf)
            End If

            Try
                Dim ArchivedProjectPath As String = Path(0)
                If ArchivedProjectPath.EndsWith(".AdvlArchive") Then
                    Message.Add("The archived project will be opened: " & vbCrLf & ArchivedProjectPath & vbCrLf)
                    OpenArchivedProject(ArchivedProjectPath)
                Else
                    Message.Add("The dropped file is not an archived project: " & vbCrLf & ArchivedProjectPath & vbCrLf)
                End If
            Catch ex As Exception
                Message.AddWarning("Error opening dropped archived project. " & ex.Message & vbCrLf)
            End Try
        End If
    End Sub

    Private Sub btnOpenParentDir_Click(sender As Object, e As EventArgs) Handles btnOpenParentDir.Click
        'Open the Parent directory of the selected project.
        Dim ParentDir As String = System.IO.Directory.GetParent(Project.Path).FullName
        If System.IO.Directory.Exists(ParentDir) Then
            Process.Start(ParentDir)
        Else
            Message.AddWarning("The parent directory was not found: " & ParentDir & vbCrLf)
        End If

    End Sub


#Region " Process XMessages" '===========================================================================================================================================

    Private Sub XMsg_Instruction(Data As String, Locn As String) Handles XMsg.Instruction
        'Process an XMessage instruction.
        'An XMessage is a simplified XSequence. It is used to exchange information between Andorville™ applications.
        '
        'An XSequence file is an AL-H7™ Information Sequence stored in an XML format.
        'AL-H7™ is the name of a programming system that uses sequences of data and location value pairs to store information or processing steps.
        'Any program, mathematical expression or data set can be expressed as an Information Sequence.

        'Add code here to process the XMessage instructions.
        'See other Andorville™ applications for examples.

        If IsDBNull(Data) Then
            Data = ""
        End If

        'Intercept instructions with the prefix "WebPage_"
        If Locn.StartsWith("WebPage_") Then 'Send the Data, Location data to the correct Web Page:
            'Message.Add("Web Page Location: " & Locn & vbCrLf)
            If Locn.Contains(":") Then
                Dim EndOfWebPageNoString As Integer = Locn.IndexOf(":")
                If Locn.Contains("-") Then
                    Dim HyphenLocn As Integer = Locn.IndexOf("-")
                    If HyphenLocn < EndOfWebPageNoString Then 'Web Page Location contains a sub-location in the web page - WebPage_1-SubLocn:Locn - SubLocn:Locn will be sent to Web page 1
                        EndOfWebPageNoString = HyphenLocn
                    End If
                End If
                Dim PageNoLen As Integer = EndOfWebPageNoString - 8
                Dim WebPageNoString As String = Locn.Substring(8, PageNoLen)
                Dim WebPageNo As Integer = CInt(WebPageNoString)
                Dim WebPageData As String = Data
                Dim WebPageLocn As String = Locn.Substring(EndOfWebPageNoString + 1)

                'Message.Add("WebPageData = " & WebPageData & "  WebPageLocn = " & WebPageLocn & vbCrLf)

                WebPageFormList(WebPageNo).XMsgInstruction(WebPageData, WebPageLocn)
            Else
                Message.AddWarning("XMessage instruction location is not complete: " & Locn & vbCrLf)
            End If
        Else

            Select Case Locn

                Case "ClientProNetName"
                    ClientProNetName = Data 'The name of the Client Application Network requesting service. AD

                Case "ClientName"
                    ClientAppName = Data 'The name of the Client application requesting service.

                Case "ClientConnectionName"
                    ClientConnName = Data 'The name of the client connection requesting service.

                Case "ClientLocn" 'The Location within the Client requesting service.
                    Dim statusOK As New XElement("Status", "OK") 'Add Status OK element when the Client Location is changed
                    xlocns(xlocns.Count - 1).Add(statusOK)

                    xmessage.Add(xlocns(xlocns.Count - 1)) 'Add the instructions for the last location to the reply xmessage
                    xlocns.Add(New XElement(Data)) 'Start the new location instructions

                Case "OnCompletion"
                    OnCompletionInstruction = Data

                Case "Main"
                 'Blank message - do nothing.

                Case "Main:EndInstruction"
                    Select Case Data
                        Case "Stop"
                            'Stop at the end of the instruction sequence.

                            'Add other cases here:
                    End Select

                Case "Main:Status"
                    Select Case Data
                        Case "OK"
                            'Main instructions completed OK
                    End Select



                Case "Command"
                    Select Case Data
                        Case "ConnectToComNet" 'Startup Command
                            If ConnectedToComNet = False Then
                                ConnectToComNet()
                            End If
                        Case "AppComCheck"
                            'Add the Appplication Communication info to the reply message:
                            Dim clientProNetName As New XElement("ClientProNetName", ProNetName) 'The Project Network Name
                            xlocns(xlocns.Count - 1).Add(clientProNetName)
                            Dim clientName As New XElement("ClientName", "ADVL_Distributions") 'The name of this application.
                            xlocns(xlocns.Count - 1).Add(clientName)
                            Dim clientConnectionName As New XElement("ClientConnectionName", ConnectionName)
                            xlocns(xlocns.Count - 1).Add(clientConnectionName)
                            '<Status>OK</Status> will be automatically appended to the XMessage before it is sent.
                    End Select


            'Startup Command Arguments ================================================
                Case "ProNetName"
                'This is currently not used.
                'The ProNetName is determined elsewhere.

                Case "ProjectName"
                    If Project.OpenProject(Data) = True Then
                        ProjectSelected = True 'Project has been opened OK.
                    Else
                        ProjectSelected = False 'Project could not be opened.
                    End If

                Case "ProjectID"
                    Message.AddWarning("Add code to handle ProjectID parameter at StartUp!" & vbCrLf)
                'Note the ComNet will usually select a project using ProjectPath.

                Case "ProjectPath"
                    If Project.OpenProjectPath(Data) = True Then
                        ProjectSelected = True 'Project has been opened OK.
                        'THE PROJECT IS LOCKED IN THE Form.Load EVENT:

                        ApplicationInfo.SettingsLocn = Project.SettingsLocn
                        Message.SettingsLocn = Project.SettingsLocn 'Set up the Message object
                        Message.Show() 'Added 18May19

                        'txtTotalDuration.Text = Project.Usage.TotalDuration.Days.ToString.PadLeft(5, "0"c) & ":" &
                        '              Project.Usage.TotalDuration.Hours.ToString.PadLeft(2, "0"c) & ":" &
                        '              Project.Usage.TotalDuration.Minutes.ToString.PadLeft(2, "0"c) & ":" &
                        '              Project.Usage.TotalDuration.Seconds.ToString.PadLeft(2, "0"c)

                        'txtCurrentDuration.Text = Project.Usage.CurrentDuration.Days.ToString.PadLeft(5, "0"c) & ":" &
                        '               Project.Usage.CurrentDuration.Hours.ToString.PadLeft(2, "0"c) & ":" &
                        '               Project.Usage.CurrentDuration.Minutes.ToString.PadLeft(2, "0"c) & ":" &
                        '               Project.Usage.CurrentDuration.Seconds.ToString.PadLeft(2, "0"c)

                        txtTotalDuration.Text = Project.Usage.TotalDuration.Days.ToString.PadLeft(5, "0"c) & "d:" &
                                        Project.Usage.TotalDuration.Hours.ToString.PadLeft(2, "0"c) & "h:" &
                                        Project.Usage.TotalDuration.Minutes.ToString.PadLeft(2, "0"c) & "m:" &
                                        Project.Usage.TotalDuration.Seconds.ToString.PadLeft(2, "0"c) & "s"

                        txtCurrentDuration.Text = Project.Usage.CurrentDuration.Days.ToString.PadLeft(5, "0"c) & "d:" &
                                       Project.Usage.CurrentDuration.Hours.ToString.PadLeft(2, "0"c) & "h:" &
                                       Project.Usage.CurrentDuration.Minutes.ToString.PadLeft(2, "0"c) & "m:" &
                                       Project.Usage.CurrentDuration.Seconds.ToString.PadLeft(2, "0"c) & "s"

                    Else
                        ProjectSelected = False 'Project could not be opened.
                        Message.AddWarning("Project could not be opened at path: " & Data & vbCrLf)
                    End If

                Case "ConnectionName"
                    StartupConnectionName = Data
            '--------------------------------------------------------------------------

            'Application Information  =================================================
            'returned by client.GetAdvlNetworkAppInfoAsync()
                Case "AdvlNetworkAppInfo:Name"
                'The name of the Andorville™ Network Application. (Not used.)

                Case "AdvlNetworkAppInfo:ExePath"
                    'The executable file path of the Andorville™ Network Application.
                    AdvlNetworkExePath = Data

                Case "AdvlNetworkAppInfo:Path"
                    'The path of the Andorville™ Network Application (ComNet). (This is where an Application.Lock file will be found while ComNet is running.)
                    AdvlNetworkAppPath = Data
           '---------------------------------------------------------------------------

           'Message Window Instructions  ==============================================
                Case "MessageWindow:Left"
                    If IsNothing(Message.MessageForm) Then
                        Message.ApplicationName = ApplicationInfo.Name
                        Message.SettingsLocn = Project.SettingsLocn
                        Message.Show()
                    End If
                    Message.MessageForm.Left = Data
                Case "MessageWindow:Top"
                    If IsNothing(Message.MessageForm) Then
                        Message.ApplicationName = ApplicationInfo.Name
                        Message.SettingsLocn = Project.SettingsLocn
                        Message.Show()
                    End If
                    Message.MessageForm.Top = Data
                Case "MessageWindow:Width"
                    If IsNothing(Message.MessageForm) Then
                        Message.ApplicationName = ApplicationInfo.Name
                        Message.SettingsLocn = Project.SettingsLocn
                        Message.Show()
                    End If
                    Message.MessageForm.Width = Data
                Case "MessageWindow:Height"
                    If IsNothing(Message.MessageForm) Then
                        Message.ApplicationName = ApplicationInfo.Name
                        Message.SettingsLocn = Project.SettingsLocn
                        Message.Show()
                    End If
                    Message.MessageForm.Height = Data
                Case "MessageWindow:Command"
                    Select Case Data
                        Case "BringToFront"
                            If IsNothing(Message.MessageForm) Then
                                Message.ApplicationName = ApplicationInfo.Name
                                Message.SettingsLocn = Project.SettingsLocn
                                Message.Show()
                            End If
                            Message.MessageForm.Activate()
                            Message.MessageForm.TopMost = True
                            Message.MessageForm.TopMost = False
                        Case "SaveSettings"
                            Message.MessageForm.SaveFormSettings()
                    End Select

            '---------------------------------------------------------------------------

            'Command to bring the Application window to the front:
                Case "ApplicationWindow:Command"
                    Select Case Data
                        Case "BringToFront"
                            Me.Activate()
                            Me.TopMost = True
                            Me.TopMost = False
                    End Select

                Case "EndOfSequence"
                    'End of Information Sequence reached.
                    'Add Status OK element at the end of the sequence:
                    Dim statusOK As New XElement("Status", "OK")
                    xlocns(xlocns.Count - 1).Add(statusOK)

                    Select Case EndInstruction
                        Case "Stop"
                            'No instructions.

                            'Add any other Cases here:

                        Case Else
                            Message.AddWarning("Unknown End Instruction: " & EndInstruction & vbCrLf)
                    End Select
                    EndInstruction = "Stop"

                    'Add the final EndInstruction:
                    If OnCompletionInstruction = "Stop" Then
                        'Final EndInstruction is not required.
                    Else
                        Dim xEndInstruction As New XElement("EndInstruction", OnCompletionInstruction)
                        xlocns(xlocns.Count - 1).Add(xEndInstruction)
                        OnCompletionInstruction = "Stop" 'Reset the OnCompletion Instruction
                    End If

                Case Else
                    Message.AddWarning("Unknown location: " & Locn & vbCrLf)
                    Message.AddWarning("            data: " & Data & vbCrLf & vbCrLf)
            End Select
        End If
    End Sub

    Private Sub XMsgLocal_Instruction(Data As String, Locn As String) Handles XMsgLocal.Instruction
        'Process an XMessage instruction locally.

        If IsDBNull(Data) Then
            Data = ""
        End If

        'Intercept instructions with the prefix "WebPage_"
        If Locn.StartsWith("WebPage_") Then 'Send the Data, Location data to the correct Web Page:
            'Message.Add("Web Page Location: " & Locn & vbCrLf)
            If Locn.Contains(":") Then
                Dim EndOfWebPageNoString As Integer = Locn.IndexOf(":")
                If Locn.Contains("-") Then
                    Dim HyphenLocn As Integer = Locn.IndexOf("-")
                    If HyphenLocn < EndOfWebPageNoString Then 'Web Page Location contains a sub-location in the web page - WebPage_1-SubLocn:Locn - SubLocn:Locn will be sent to Web page 1
                        EndOfWebPageNoString = HyphenLocn
                    End If
                End If
                Dim PageNoLen As Integer = EndOfWebPageNoString - 8
                Dim WebPageNoString As String = Locn.Substring(8, PageNoLen)
                Dim WebPageNo As Integer = CInt(WebPageNoString)
                Dim WebPageData As String = Data
                Dim WebPageLocn As String = Locn.Substring(EndOfWebPageNoString + 1)

                'Message.Add("WebPageData = " & WebPageData & "  WebPageLocn = " & WebPageLocn & vbCrLf)

                WebPageFormList(WebPageNo).XMsgInstruction(WebPageData, WebPageLocn)
            Else
                Message.AddWarning("XMessage instruction location is not complete: " & Locn & vbCrLf)
            End If
        Else

            Select Case Locn
                Case "ClientName"
                    ClientAppName = Data 'The name of the Client requesting service.

                'UPDATE:
                Case "OnCompletion"
                    OnCompletionInstruction = Data

                Case "Main"
                 'Blank message - do nothing.


                Case "Main:EndInstruction"
                    Select Case Data
                        Case "Stop"
                            'Stop at the end of the instruction sequence.

                            'Add other cases here:
                    End Select

                Case "Main:Status"
                    Select Case Data
                        Case "OK"
                            'Main instructions completed OK
                    End Select

                Case "EndOfSequence"
                    'End of Information Vector Sequence reached.

                Case Else
                    Message.AddWarning("Local XMessage: " & Locn & vbCrLf)
                    Message.AddWarning("Unknown location: " & Locn & vbCrLf)
                    Message.AddWarning("            data: " & Data & vbCrLf & vbCrLf)
            End Select
        End If
    End Sub



#End Region 'Process XMessages ------------------------------------------------------------------------------------------------------------------------------------------


    Private Sub ToolStripMenuItem1_EditWorkflowTabPage_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1_EditWorkflowTabPage.Click
        'Edit the Workflow Web Page:

        If WorkflowFileName = "" Then
            Message.AddWarning("No page to edit." & vbCrLf)
        Else
            Dim FormNo As Integer = OpenNewHtmlDisplayPage()
            HtmlDisplayFormList(FormNo).FileName = WorkflowFileName
            HtmlDisplayFormList(FormNo).OpenDocument
        End If

    End Sub

    Private Sub ToolStripMenuItem1_ShowStartPageInWorkflowTab_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1_ShowStartPageInWorkflowTab.Click
        'Show the Start Page in the Workflow Tab:
        OpenStartPage()
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs)

    End Sub

    'Private Sub Main_Move(sender As Object, e As EventArgs) Handles Me.Move
    '    txtLeft.Text = Me.Left
    '    txtTop.Text = Me.Top
    'End Sub

    Private Sub bgwComCheck_DoWork(sender As Object, e As DoWorkEventArgs) Handles bgwComCheck.DoWork
        'The communications check thread.
        While ConnectedToComNet
            Try
                If client.IsAlive() Then
                    bgwComCheck.ReportProgress(1, Format(Now, "HH:mm:ss") & " Connection OK." & vbCrLf)
                Else
                    bgwComCheck.ReportProgress(1, Format(Now, "HH:mm:ss") & " Connection Fault.")
                End If
            Catch ex As Exception
                bgwComCheck.ReportProgress(1, "Error in bgeComCheck_DoWork!" & vbCrLf)
                bgwComCheck.ReportProgress(1, ex.Message & vbCrLf)
            End Try

            'System.Threading.Thread.Sleep(60000) 'Sleep time in milliseconds (60 seconds) - For testing only.
            'System.Threading.Thread.Sleep(3600000) 'Sleep time in milliseconds (60 minutes)
            System.Threading.Thread.Sleep(1800000) 'Sleep time in milliseconds (30 minutes)
        End While
    End Sub

    Private Sub bgwComCheck_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles bgwComCheck.ProgressChanged
        Message.Add(e.UserState.ToString) 'Show the ComCheck message 
    End Sub

    Private Sub XMsg_ErrorMsg(ErrMsg As String) Handles XMsg.ErrorMsg
        Message.AddWarning(ErrMsg & vbCrLf)
    End Sub

    Private Sub bgwSendMessage_DoWork(sender As Object, e As DoWorkEventArgs) Handles bgwSendMessage.DoWork
        'Send a message on a separate thread:
        Try
            If IsNothing(client) Then
                bgwSendMessage.ReportProgress(1, "No Connection available. Message not sent!")
            Else
                If client.State = ServiceModel.CommunicationState.Faulted Then
                    bgwSendMessage.ReportProgress(1, "Connection state is faulted. Message not sent!")
                Else
                    Dim SendMessageParams As clsSendMessageParams = e.Argument
                    client.SendMessage(SendMessageParams.ProjectNetworkName, SendMessageParams.ConnectionName, SendMessageParams.Message)
                End If
            End If
        Catch ex As Exception
            bgwSendMessage.ReportProgress(1, ex.Message)
        End Try
    End Sub

    Private Sub bgwSendMessage_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles bgwSendMessage.ProgressChanged
        'Display an error message:
        Message.AddWarning("Send Message error: " & e.UserState.ToString & vbCrLf) 'Show the bgwSendMessage message 
    End Sub

    Private Sub bgwSendMessageAlt_DoWork(sender As Object, e As DoWorkEventArgs) Handles bgwSendMessageAlt.DoWork
        'Alternative SendMessage background worker - used to send a message while instructions are being processed. 
        'Send a message on a separate thread
        Try
            If IsNothing(client) Then
                bgwSendMessageAlt.ReportProgress(1, "No Connection available. Message not sent!")
            Else
                If client.State = ServiceModel.CommunicationState.Faulted Then
                    bgwSendMessageAlt.ReportProgress(1, "Connection state is faulted. Message not sent!")
                Else
                    Dim SendMessageParamsAlt As clsSendMessageParams = e.Argument
                    client.SendMessage(SendMessageParamsAlt.ProjectNetworkName, SendMessageParamsAlt.ConnectionName, SendMessageParamsAlt.Message)
                End If
            End If
        Catch ex As Exception
            bgwSendMessageAlt.ReportProgress(1, ex.Message)
        End Try
    End Sub

    Private Sub bgwSendMessageAlt_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles bgwSendMessageAlt.ProgressChanged
        'Display an error message:
        Message.AddWarning("Send Message error: " & e.UserState.ToString & vbCrLf) 'Show the bgwSendMessageAlt message 
    End Sub

    Private Sub bgwRunInstruction_DoWork(sender As Object, e As DoWorkEventArgs) Handles bgwRunInstruction.DoWork
        'Run a single instruction.
        Try
            Dim Instruction As clsInstructionParams = e.Argument
            XMsg_Instruction(Instruction.Info, Instruction.Locn)
        Catch ex As Exception
            bgwRunInstruction.ReportProgress(1, ex.Message)
        End Try
    End Sub

    Private Sub bgwRunInstruction_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles bgwRunInstruction.ProgressChanged
        'Display an error message:
        Message.AddWarning("Run Instruction error: " & e.UserState.ToString & vbCrLf) 'Show the bgwRunInstruction message 
    End Sub


    Private Sub btnShowProjectInfo_Click(sender As Object, e As EventArgs) Handles btnShowProjectInfo.Click
        'Show the current Project information:
        Message.Add("--------------------------------------------------------------------------------------" & vbCrLf)
        Message.Add("Project ------------------------ " & vbCrLf)
        Message.Add("   Name: " & Project.Name & vbCrLf)
        Message.Add("   Type: " & Project.Type.ToString & vbCrLf)
        Message.Add("   Description: " & Project.Description & vbCrLf)
        Message.Add("   Creation Date: " & Project.CreationDate & vbCrLf)
        Message.Add("   ID: " & Project.ID & vbCrLf)
        Message.Add("   Relative Path: " & Project.RelativePath & vbCrLf)
        Message.Add("   Path: " & Project.Path & vbCrLf & vbCrLf)

        Message.Add("Parent Project ----------------- " & vbCrLf)
        Message.Add("   Name: " & Project.ParentProjectName & vbCrLf)
        Message.Add("   Path: " & Project.ParentProjectPath & vbCrLf)

        Message.Add("Application -------------------- " & vbCrLf)
        Message.Add("   Name: " & Project.Application.Name & vbCrLf)
        Message.Add("   Description: " & Project.Application.Description & vbCrLf)
        Message.Add("   Path: " & Project.ApplicationDir & vbCrLf)

        Message.Add("Settings ----------------------- " & vbCrLf)
        Message.Add("   Settings Relative Location Type: " & Project.SettingsRelLocn.Type.ToString & vbCrLf)
        Message.Add("   Settings Relative Location Path: " & Project.SettingsRelLocn.Path & vbCrLf)
        Message.Add("   Settings Location Type: " & Project.SettingsLocn.Type.ToString & vbCrLf)
        Message.Add("   Settings Location Path: " & Project.SettingsLocn.Path & vbCrLf)

        Message.Add("Data --------------------------- " & vbCrLf)
        Message.Add("   Data Relative Location Type: " & Project.DataRelLocn.Type.ToString & vbCrLf)
        Message.Add("   Data Relative Location Path: " & Project.DataRelLocn.Path & vbCrLf)
        Message.Add("   Data Location Type: " & Project.DataLocn.Type.ToString & vbCrLf)
        Message.Add("   Data Location Path: " & Project.DataLocn.Path & vbCrLf)

        Message.Add("System ------------------------- " & vbCrLf)
        Message.Add("   System Relative Location Type: " & Project.SystemRelLocn.Type.ToString & vbCrLf)
        Message.Add("   System Relative Location Path: " & Project.SystemRelLocn.Path & vbCrLf)
        Message.Add("   System Location Type: " & Project.SystemLocn.Type.ToString & vbCrLf)
        Message.Add("   System Location Path: " & Project.SystemLocn.Path & vbCrLf)
        Message.Add("======================================================================================" & vbCrLf)

    End Sub

    Private Sub Message_ShowXMessagesChanged(Show As Boolean) Handles Message.ShowXMessagesChanged
        ShowXMessages = Show
    End Sub

    Private Sub Message_ShowSysMessagesChanged(Show As Boolean) Handles Message.ShowSysMessagesChanged
        ShowSysMessages = Show
    End Sub

    Private Sub Project_NewProjectCreated(ProjectPath As String) Handles Project.NewProjectCreated
        SendProjectInfo(ProjectPath) 'Send the path of the new project to the Network application. The new project will be added to the list of projects.
    End Sub

    Private Sub btnNewMCModel_Click(sender As Object, e As EventArgs) Handles btnNewDistModel.Click
        'Create a new Distribution Data Model.

        'Get the new model File Name, Model Name and Description:
        Dim EntryForm As New ADVL_Utilities_Library_1.frmNewDataNameModal
        EntryForm.EntryName = "NewDistModel"
        EntryForm.Title = "New Distribution Model"
        EntryForm.FileExtension = "Distrib"
        EntryForm.GetFileName = True
        EntryForm.GetDataName = True
        EntryForm.GetDataLabel = True
        EntryForm.GetDataDescription = True
        EntryForm.SettingsLocn = Project.SettingsLocn
        EntryForm.DataLocn = Project.DataLocn
        EntryForm.ApplicationName = ApplicationInfo.Name
        EntryForm.RestoreFormSettings()

        If EntryForm.ShowDialog() = DialogResult.OK Then
            If txtFileName.Text.Trim = "" Then
                'There is no model to save.
            Else
                If Distribution.Modified Then
                    Dim Result As DialogResult = MessageBox.Show("Do you want to save the changes in the current Distribution model?", "Warning", MessageBoxButtons.YesNoCancel)
                    If Result = DialogResult.Yes Then
                        'SaveBayesModel()
                        SaveDistribModel()
                    ElseIf Result = DialogResult.Cancel Then
                        Exit Sub
                    Else
                        'Contunue without saving the current model.
                        'Bayes.Modified = False
                        Distribution.Modified = False
                    End If
                Else

                End If
            End If
            Distribution.Clear() 'Clear the current Distribution model.
            'UpdateDataTableDisplay()

            'dgvDataTable.Rows.Clear()
            'dgvDataTable.Columns.Clear()
            SelTableName = ""
            dgvDataTable.DataSource = vbNull


            'UpdateSuffix()
            Distribution.FileName = EntryForm.FileName
            Distribution.ModelName = EntryForm.DataName
            Distribution.Label = EntryForm.DataLabel
            Distribution.Description = EntryForm.DataDescription

            Distribution.UpdateDistribCounts()
            NContinuous = Distribution.NContinuous
            NDiscrete = Distribution.NDiscrete
            NDistribs = Distribution.Info.Count
            If NDistribs > 0 Then SelDistrib = 1 Else SelDistrib = 0

            UpdateForm()
            UpdateSuffix()
        End If
    End Sub

    Public Sub SaveDistribModel()
        'Save the Distribution model.

        Dim FileName As String = txtFileName.Text.Trim

        'Check if a file name has been specified:
        If FileName = "" Then
            Message.AddWarning("Please enter a file name." & vbCrLf)
            Exit Sub
        End If

        'Check the fine name extension:
        If LCase(FileName).EndsWith(".distrib") Then
            FileName = IO.Path.GetFileNameWithoutExtension(FileName) & ".Distrib"
        ElseIf FileName.Contains(".") Then
            Message.AddWarning("Unknown file extension: " & IO.Path.GetExtension(FileName) & vbCrLf)
            Exit Sub
        Else
            FileName = FileName & ".Distrib"
        End If

        txtFileName.Text = FileName

        'Update the Distribution settings:
        Distribution.ModelName = txtModelName.Text.Trim
        Distribution.Label = txtLabel.Text.Trim
        Distribution.Description = txtDescription.Text.Trim

        Project.SaveXmlData(FileName, Distribution.ToXDoc)
        Distribution.Modified = False
    End Sub

    Private Sub btnOpenMCModel_Click(sender As Object, e As EventArgs) Handles btnOpenDistModel.Click
        'Open a Distribution Data Model.

        Dim FileName As String = Project.SelectDataFile("Distribution Data Model files", "Distrib")
        If FileName = "" Then
            'No file has been selected.
        Else
            OpenDistModel(FileName)
            GenerateData()
        End If
    End Sub

    Private Sub OpenDistModel(FileName As String)
        'Open the Distribution Data Model with the name FileName.

        'NOTE: The existing model will be overwritten. No need to remove.
        ''Remove the existing model data:
        'txtFileName.Text = ""
        'txtModelName.Text = ""
        'txtLabel.Text = ""
        'txtDescription.Text = ""
        'txtNotes.Text = ""

        'Clear any existing data:
        Distribution.Clear() 'Clear the current data.
        SelTableName = ""
        dgvDataTable.DataSource = vbNull
        cmbTableName.Items.Clear() 'Clear the list of avaialable tables.

        Dim XDoc As System.Xml.Linq.XDocument
        Project.ReadXmlData(FileName, XDoc)
        Distribution.FileName = FileName
        txtFileName.Text = FileName
        Distribution.FromXDoc(XDoc)

        NDistribs = Distribution.Info.Count
        If NDistribs > 0 Then SelDistrib = 1 Else SelDistrib = 0
        UpdateForm()

    End Sub

    Private Sub UpdateForm()
        'Public Sub UpdateForm()
        'Update the application form with the Distribution Data Model.

        'Update the Model Summary tab: -----------------------------------------------------------
        txtFileName.Text = Distribution.FileName
        txtModelName.Text = Distribution.ModelName
        txtLabel.Text = Distribution.Label
        txtDescription.Text = Distribution.Description
        txtNotes.Text = Distribution.Notes

        If SelDistrib = 0 Then
            Message.AddWarning("A distribution has not been selected." & vbCrLf)
        Else

            'Update the Distribution tab: ------------------------------------------------------------
            txtDistName.Text = Distribution.Info(SelDistrib - 1).Name
            txtContinuity.Text = Distribution.Info(SelDistrib - 1).Continuity
            txtNParams.Text = Distribution.Info(SelDistrib - 1).NParams
            dgvParams.RowCount = Distribution.Info(SelDistrib - 1).NParams
            'chkUpdateLabel.Checked = Distribution.Distrib.AutoUpdateSuffix
            chkUpdateLabel.Checked = Distribution.Info(SelDistrib - 1).AutoUpdateLabels

            txtRVName.Text = Distribution.Info(SelDistrib - 1).RVName
            txtRVDescr.Text = Distribution.Info(SelDistrib - 1).RVDescription
            txtRVMeasurement.Text = Distribution.Info(SelDistrib - 1).RVMeasurement
            txtRVUnits.Text = Distribution.Info(SelDistrib - 1).RVUnits
            txtRVUnitsAbbrev.Text = Distribution.Info(SelDistrib - 1).RVAbbrevUnits

            If Distribution.Info(SelDistrib - 1).NParams > 0 Then
                dgvParams.Rows(0).Cells(0).Value = Distribution.Info(SelDistrib - 1).ParamA.Name
                dgvParams.Rows(0).Cells(1).Value = NameToSymbol(Distribution.Info(SelDistrib - 1).ParamA.Symbol)
                dgvParams.Rows(0).Cells(2).Value = Distribution.Info(SelDistrib - 1).ParamA.Value
                dgvParams.Rows(0).Cells(3).Value = Distribution.Info(SelDistrib - 1).ParamA.Type
                dgvParams.Rows(0).Cells(4).Value = Distribution.Info(SelDistrib - 1).ParamA.NumberType
                dgvParams.Rows(0).Cells(5).Value = Distribution.Info(SelDistrib - 1).ParamA.Minimum
                dgvParams.Rows(0).Cells(6).Value = Distribution.Info(SelDistrib - 1).ParamA.Maximum
                dgvParams.Rows(0).Cells(7).Value = Distribution.Info(SelDistrib - 1).ParamA.Description
                If Distribution.Info(SelDistrib - 1).NParams > 1 Then
                    dgvParams.Rows(1).Cells(0).Value = Distribution.Info(SelDistrib - 1).ParamB.Name
                    dgvParams.Rows(1).Cells(1).Value = NameToSymbol(Distribution.Info(SelDistrib - 1).ParamB.Symbol)
                    dgvParams.Rows(1).Cells(2).Value = Distribution.Info(SelDistrib - 1).ParamB.Value
                    dgvParams.Rows(1).Cells(3).Value = Distribution.Info(SelDistrib - 1).ParamB.Type
                    dgvParams.Rows(1).Cells(4).Value = Distribution.Info(SelDistrib - 1).ParamB.NumberType
                    dgvParams.Rows(1).Cells(5).Value = Distribution.Info(SelDistrib - 1).ParamB.Minimum
                    dgvParams.Rows(1).Cells(6).Value = Distribution.Info(SelDistrib - 1).ParamB.Maximum
                    dgvParams.Rows(1).Cells(7).Value = Distribution.Info(SelDistrib - 1).ParamB.Description
                    If Distribution.Info(SelDistrib - 1).NParams > 2 Then
                        dgvParams.Rows(2).Cells(0).Value = Distribution.Info(SelDistrib - 1).ParamC.Name
                        dgvParams.Rows(2).Cells(1).Value = NameToSymbol(Distribution.Info(SelDistrib - 1).ParamC.Symbol)
                        dgvParams.Rows(2).Cells(2).Value = Distribution.Info(SelDistrib - 1).ParamC.Value
                        dgvParams.Rows(2).Cells(3).Value = Distribution.Info(SelDistrib - 1).ParamC.Type
                        dgvParams.Rows(2).Cells(4).Value = Distribution.Info(SelDistrib - 1).ParamC.NumberType
                        dgvParams.Rows(2).Cells(5).Value = Distribution.Info(SelDistrib - 1).ParamC.Minimum
                        dgvParams.Rows(2).Cells(6).Value = Distribution.Info(SelDistrib - 1).ParamC.Maximum
                        dgvParams.Rows(2).Cells(7).Value = Distribution.Info(SelDistrib - 1).ParamC.Description
                        If Distribution.Info(SelDistrib - 1).NParams > 3 Then
                            dgvParams.Rows(3).Cells(0).Value = Distribution.Info(SelDistrib - 1).ParamD.Name
                            dgvParams.Rows(3).Cells(1).Value = NameToSymbol(Distribution.Info(SelDistrib - 1).ParamD.Symbol)
                            dgvParams.Rows(3).Cells(2).Value = Distribution.Info(SelDistrib - 1).ParamD.Value
                            dgvParams.Rows(3).Cells(3).Value = Distribution.Info(SelDistrib - 1).ParamD.Type
                            dgvParams.Rows(3).Cells(4).Value = Distribution.Info(SelDistrib - 1).ParamD.NumberType
                            dgvParams.Rows(3).Cells(5).Value = Distribution.Info(SelDistrib - 1).ParamD.Minimum
                            dgvParams.Rows(3).Cells(6).Value = Distribution.Info(SelDistrib - 1).ParamD.Maximum
                            dgvParams.Rows(3).Cells(7).Value = Distribution.Info(SelDistrib - 1).ParamD.Description
                            If Distribution.Info(SelDistrib - 1).NParams > 4 Then
                                dgvParams.Rows(4).Cells(0).Value = Distribution.Info(SelDistrib - 1).ParamE.Name
                                dgvParams.Rows(4).Cells(1).Value = NameToSymbol(Distribution.Info(SelDistrib - 1).ParamE.Symbol)
                                dgvParams.Rows(4).Cells(2).Value = Distribution.Info(SelDistrib - 1).ParamE.Value
                                dgvParams.Rows(4).Cells(3).Value = Distribution.Info(SelDistrib - 1).ParamE.Type
                                dgvParams.Rows(4).Cells(4).Value = Distribution.Info(SelDistrib - 1).ParamE.NumberType
                                dgvParams.Rows(4).Cells(5).Value = Distribution.Info(SelDistrib - 1).ParamE.Minimum
                                dgvParams.Rows(4).Cells(6).Value = Distribution.Info(SelDistrib - 1).ParamE.Maximum
                                dgvParams.Rows(4).Cells(7).Value = Distribution.Info(SelDistrib - 1).ParamE.Description
                                If Distribution.Info(SelDistrib - 1).NParams > 5 Then
                                    'Too many parameters!
                                End If
                            End If
                        End If
                    End If
                End If
            End If

            'dgvParams.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)
            dgvParams.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            dgvParams.AutoResizeColumns()

            'txtSuffix.Text = Distribution.Info(SelDistrib - 1).Suffix
            UpdateSuffix()

            cmbDefMkrFill.SelectedIndex = cmbDefMkrFill.FindStringExact(Distribution.Info(SelDistrib - 1).Display.MarkerFill)
            txtDefMkrColor.BackColor = Distribution.Info(SelDistrib - 1).Display.MarkerColor
            txtDefBorderColor.BackColor = Distribution.Info(SelDistrib - 1).Display.BorderColor
            txtDefBorderWidth.Text = Distribution.Info(SelDistrib - 1).Display.BorderWidth
            cmbDefMkrStyle.SelectedIndex = cmbDefMkrStyle.FindStringExact(Distribution.Info(SelDistrib - 1).Display.MarkerStyle)
            txtDefMkrSize.Text = Distribution.Info(SelDistrib - 1).Display.MarkerSize
            txtDefMkrStep.Text = Distribution.Info(SelDistrib - 1).Display.MarkerStep
            txtDefLineColor.BackColor = Distribution.Info(SelDistrib - 1).Display.LineColor
            txtDefLineWidth.Text = Distribution.Info(SelDistrib - 1).Display.LineWidth

            txtDefFrom.Text = Distribution.Info(SelDistrib - 1).RangeMin
            txtDefTo.Text = Distribution.Info(SelDistrib - 1).RangeMax


            'If Distribution.Continuity = "Continuous" Then

            'Update Continuous sampling settings:
            txtMinValue.Text = Distribution.ContSampling.Minimum
            chkLockSampMin.Checked = Distribution.ContSampling.MinLock
            txtMaxValue.Text = Distribution.ContSampling.Maximum
            chkLockSampMax.Checked = Distribution.ContSampling.MaxLock
            txtSampleInt.Text = Distribution.ContSampling.Interval
            chkLockSampInt.Checked = Distribution.ContSampling.IntervalLock
            txtNSamples.Text = Distribution.ContSampling.NSamples
            chkLockNSamples.Checked = Distribution.ContSampling.NSamplesLock
            txtXAxisLabel.Text = Distribution.ContSampling.Label
            txtXAxisUnits.Text = Distribution.ContSampling.Units
            txtXAxisDescription.Text = Distribution.ContSampling.Description

            GroupBox2.Enabled = True

            'GroupBox9.Enabled = False

            'ElseIf Distribution.Continuity = "Discrete" Then

            'Update Discrete sampling settings:
            txtDiscMin.Text = Distribution.DiscSampling.Minimum
            txtDiscMax.Text = Distribution.DiscSampling.Maximum
            txtDiscXAxisLabel.Text = Distribution.DiscSampling.Label
            txtDiscXAxisUnits.Text = Distribution.DiscSampling.Units
            txtDiscXAxisDescr.Text = Distribution.DiscSampling.Description

            GroupBox9.Enabled = True

            'GroupBox2.Enabled = False

            'Else
            '    'Not a valid continuity string.
            'End If

            'Update the Function Fields tab: ------------------------------------------------------------------
            dgvFields.Rows.Clear()
            If Distribution.Info(SelDistrib - 1).Continuity = "Continuous" Then
                'dgvFields.Rows.Add(Distribution.Info(SelDistrib - 1).ValueInfo.Name, Distribution.Info(SelDistrib - 1).ValueInfo.Valid, Distribution.Info(SelDistrib - 1).ValueInfo.Generate.ToString, Distribution.Info(SelDistrib - 1).ValueInfo.NumType, Distribution.Info(SelDistrib - 1).ValueInfo.Format, Distribution.Info(SelDistrib - 1).ValueInfo.Alignment, Distribution.Info(SelDistrib - 1).ValueInfo.ValueLabel, Distribution.Info(SelDistrib - 1).ValueInfo.Units, Distribution.Info(SelDistrib - 1).ValueInfo.LabelPrefix, Distribution.Info(SelDistrib - 1).ValueInfo.SeriesLabel, Distribution.Info(SelDistrib - 1).ValueInfo.Description)
                dgvFields.Rows.Add(Distribution.Info(SelDistrib - 1).PdfInfo.Name, Distribution.Info(SelDistrib - 1).PdfInfo.Valid, Distribution.Info(SelDistrib - 1).PdfInfo.Generate.ToString, Distribution.Info(SelDistrib - 1).PdfInfo.NumType, Distribution.Info(SelDistrib - 1).PdfInfo.Format, Distribution.Info(SelDistrib - 1).PdfInfo.Alignment, Distribution.Info(SelDistrib - 1).PdfInfo.ValueLabel, Distribution.Info(SelDistrib - 1).PdfInfo.Units, Distribution.Info(SelDistrib - 1).PdfInfo.LabelPrefix, Distribution.Info(SelDistrib - 1).PdfInfo.SeriesLabel, Distribution.Info(SelDistrib - 1).PdfInfo.Description)
                dgvFields.Rows.Add(Distribution.Info(SelDistrib - 1).PdfLnInfo.Name, Distribution.Info(SelDistrib - 1).PdfLnInfo.Valid, Distribution.Info(SelDistrib - 1).PdfLnInfo.Generate.ToString, Distribution.Info(SelDistrib - 1).PdfLnInfo.NumType, Distribution.Info(SelDistrib - 1).PdfLnInfo.Format, Distribution.Info(SelDistrib - 1).PdfLnInfo.Alignment, Distribution.Info(SelDistrib - 1).PdfLnInfo.ValueLabel, Distribution.Info(SelDistrib - 1).PdfLnInfo.Units, Distribution.Info(SelDistrib - 1).PdfLnInfo.LabelPrefix, Distribution.Info(SelDistrib - 1).PdfLnInfo.SeriesLabel, Distribution.Info(SelDistrib - 1).PdfLnInfo.Description)
                dgvFields.Rows.Add(Distribution.Info(SelDistrib - 1).CdfInfo.Name, Distribution.Info(SelDistrib - 1).CdfInfo.Valid, Distribution.Info(SelDistrib - 1).CdfInfo.Generate.ToString, Distribution.Info(SelDistrib - 1).CdfInfo.NumType, Distribution.Info(SelDistrib - 1).CdfInfo.Format, Distribution.Info(SelDistrib - 1).CdfInfo.Alignment, Distribution.Info(SelDistrib - 1).CdfInfo.ValueLabel, Distribution.Info(SelDistrib - 1).CdfInfo.Units, Distribution.Info(SelDistrib - 1).CdfInfo.LabelPrefix, Distribution.Info(SelDistrib - 1).CdfInfo.SeriesLabel, Distribution.Info(SelDistrib - 1).CdfInfo.Description)

                dgvFields.Rows.Add(Distribution.Info(SelDistrib - 1).RevCdfInfo.Name, Distribution.Info(SelDistrib - 1).RevCdfInfo.Valid, Distribution.Info(SelDistrib - 1).RevCdfInfo.Generate.ToString, Distribution.Info(SelDistrib - 1).RevCdfInfo.NumType, Distribution.Info(SelDistrib - 1).RevCdfInfo.Format, Distribution.Info(SelDistrib - 1).RevCdfInfo.Alignment, Distribution.Info(SelDistrib - 1).RevCdfInfo.ValueLabel, Distribution.Info(SelDistrib - 1).RevCdfInfo.Units, Distribution.Info(SelDistrib - 1).RevCdfInfo.LabelPrefix, Distribution.Info(SelDistrib - 1).RevCdfInfo.SeriesLabel, Distribution.Info(SelDistrib - 1).RevCdfInfo.Description)

                'dgvFields.Rows.Add(Distribution.Info(SelDistrib - 1).ProbabilityInfo.Name, Distribution.Info(SelDistrib - 1).ProbabilityInfo.Valid, Distribution.Info(SelDistrib - 1).ProbabilityInfo.Generate.ToString, Distribution.Info(SelDistrib - 1).ProbabilityInfo.NumType, Distribution.Info(SelDistrib - 1).ProbabilityInfo.Format, Distribution.Info(SelDistrib - 1).ProbabilityInfo.Alignment, Distribution.Info(SelDistrib - 1).ProbabilityInfo.ValueLabel, Distribution.Info(SelDistrib - 1).ProbabilityInfo.Units, Distribution.Info(SelDistrib - 1).ProbabilityInfo.LabelPrefix, Distribution.Info(SelDistrib - 1).ProbabilityInfo.SeriesLabel, Distribution.Info(SelDistrib - 1).ProbabilityInfo.Description)
                dgvFields.Rows.Add(Distribution.Info(SelDistrib - 1).InvCdfInfo.Name, Distribution.Info(SelDistrib - 1).InvCdfInfo.Valid, Distribution.Info(SelDistrib - 1).InvCdfInfo.Generate.ToString, Distribution.Info(SelDistrib - 1).InvCdfInfo.NumType, Distribution.Info(SelDistrib - 1).InvCdfInfo.Format, Distribution.Info(SelDistrib - 1).InvCdfInfo.Alignment, Distribution.Info(SelDistrib - 1).InvCdfInfo.ValueLabel, Distribution.Info(SelDistrib - 1).InvCdfInfo.Units, Distribution.Info(SelDistrib - 1).InvCdfInfo.LabelPrefix, Distribution.Info(SelDistrib - 1).InvCdfInfo.SeriesLabel, Distribution.Info(SelDistrib - 1).InvCdfInfo.Description)

                dgvFields.Rows.Add(Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Name, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Valid, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Generate.ToString, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.NumType, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Format, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Alignment, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.ValueLabel, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Units, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.LabelPrefix, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.SeriesLabel, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Description)

                'Add PDF display settings:
                'dgvFields.Rows(1).Cells(11).Value = Distribution.Info(SelDistrib - 1).PdfInfo.Display.MarkerFill
                dgvFields.Rows(0).Cells(11).Value = Distribution.Info(SelDistrib - 1).PdfInfo.Display.MarkerFill
                dgvFields.Rows(0).Cells(12).Style.BackColor = Distribution.Info(SelDistrib - 1).PdfInfo.Display.MarkerColor
                dgvFields.Rows(0).Cells(13).Style.BackColor = Distribution.Info(SelDistrib - 1).PdfInfo.Display.BorderColor
                dgvFields.Rows(0).Cells(14).Value = Distribution.Info(SelDistrib - 1).PdfInfo.Display.BorderWidth
                dgvFields.Rows(0).Cells(15).Value = Distribution.Info(SelDistrib - 1).PdfInfo.Display.MarkerStyle
                dgvFields.Rows(0).Cells(16).Value = Distribution.Info(SelDistrib - 1).PdfInfo.Display.MarkerSize
                dgvFields.Rows(0).Cells(17).Value = Distribution.Info(SelDistrib - 1).PdfInfo.Display.MarkerStep
                dgvFields.Rows(0).Cells(18).Style.BackColor = Distribution.Info(SelDistrib - 1).PdfInfo.Display.LineColor
                dgvFields.Rows(0).Cells(19).Value = Distribution.Info(SelDistrib - 1).PdfInfo.Display.LineWidth

                'Add PDFLn display settings:
                dgvFields.Rows(1).Cells(11).Value = Distribution.Info(SelDistrib - 1).PdfLnInfo.Display.MarkerFill
                dgvFields.Rows(1).Cells(12).Style.BackColor = Distribution.Info(SelDistrib - 1).PdfLnInfo.Display.MarkerColor
                dgvFields.Rows(1).Cells(13).Style.BackColor = Distribution.Info(SelDistrib - 1).PdfLnInfo.Display.BorderColor
                dgvFields.Rows(1).Cells(14).Value = Distribution.Info(SelDistrib - 1).PdfLnInfo.Display.BorderWidth
                dgvFields.Rows(1).Cells(15).Value = Distribution.Info(SelDistrib - 1).PdfLnInfo.Display.MarkerStyle
                dgvFields.Rows(1).Cells(16).Value = Distribution.Info(SelDistrib - 1).PdfLnInfo.Display.MarkerSize
                dgvFields.Rows(1).Cells(17).Value = Distribution.Info(SelDistrib - 1).PdfLnInfo.Display.MarkerStep
                dgvFields.Rows(1).Cells(18).Style.BackColor = Distribution.Info(SelDistrib - 1).PdfLnInfo.Display.LineColor
                dgvFields.Rows(1).Cells(19).Value = Distribution.Info(SelDistrib - 1).PdfLnInfo.Display.LineWidth

                'Add CDF display settings:
                dgvFields.Rows(2).Cells(11).Value = Distribution.Info(SelDistrib - 1).CdfInfo.Display.MarkerFill
                dgvFields.Rows(2).Cells(12).Style.BackColor = Distribution.Info(SelDistrib - 1).CdfInfo.Display.MarkerColor
                dgvFields.Rows(2).Cells(13).Style.BackColor = Distribution.Info(SelDistrib - 1).CdfInfo.Display.BorderColor
                dgvFields.Rows(2).Cells(14).Value = Distribution.Info(SelDistrib - 1).CdfInfo.Display.BorderWidth
                dgvFields.Rows(2).Cells(15).Value = Distribution.Info(SelDistrib - 1).CdfInfo.Display.MarkerStyle
                dgvFields.Rows(2).Cells(16).Value = Distribution.Info(SelDistrib - 1).CdfInfo.Display.MarkerSize
                dgvFields.Rows(2).Cells(17).Value = Distribution.Info(SelDistrib - 1).CdfInfo.Display.MarkerStep
                dgvFields.Rows(2).Cells(18).Style.BackColor = Distribution.Info(SelDistrib - 1).CdfInfo.Display.LineColor
                dgvFields.Rows(2).Cells(19).Value = Distribution.Info(SelDistrib - 1).CdfInfo.Display.LineWidth

                'Add RevCDF display settings:
                dgvFields.Rows(3).Cells(11).Value = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.MarkerFill
                dgvFields.Rows(3).Cells(12).Style.BackColor = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.MarkerColor
                dgvFields.Rows(3).Cells(13).Style.BackColor = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.BorderColor
                dgvFields.Rows(3).Cells(14).Value = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.BorderWidth
                dgvFields.Rows(3).Cells(15).Value = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.MarkerStyle
                dgvFields.Rows(3).Cells(16).Value = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.MarkerSize
                dgvFields.Rows(3).Cells(17).Value = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.MarkerStep
                dgvFields.Rows(3).Cells(18).Style.BackColor = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.LineColor
                dgvFields.Rows(3).Cells(19).Value = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.LineWidth

                ''Add InvCDF display settings:
                'dgvFields.Rows(3).Cells(11).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerFill
                'dgvFields.Rows(3).Cells(12).Style.BackColor = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerColor
                'dgvFields.Rows(3).Cells(13).Style.BackColor = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.BorderColor
                'dgvFields.Rows(3).Cells(14).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.BorderWidth
                'dgvFields.Rows(3).Cells(15).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerStyle
                'dgvFields.Rows(3).Cells(16).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerSize
                'dgvFields.Rows(3).Cells(17).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerStep
                'dgvFields.Rows(3).Cells(18).Style.BackColor = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.LineColor
                'dgvFields.Rows(3).Cells(19).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.LineWidth

                'Add InvCDF display settings:
                dgvFields.Rows(4).Cells(11).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerFill
                dgvFields.Rows(4).Cells(12).Style.BackColor = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerColor
                dgvFields.Rows(4).Cells(13).Style.BackColor = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.BorderColor
                dgvFields.Rows(4).Cells(14).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.BorderWidth
                dgvFields.Rows(4).Cells(15).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerStyle
                dgvFields.Rows(4).Cells(16).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerSize
                dgvFields.Rows(4).Cells(17).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerStep
                dgvFields.Rows(4).Cells(18).Style.BackColor = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.LineColor
                dgvFields.Rows(4).Cells(19).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.LineWidth

                'Add InvRevCDF display settings:
                dgvFields.Rows(5).Cells(11).Value = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.MarkerFill
                dgvFields.Rows(5).Cells(12).Style.BackColor = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.MarkerColor
                dgvFields.Rows(5).Cells(13).Style.BackColor = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.BorderColor
                dgvFields.Rows(5).Cells(14).Value = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.BorderWidth
                dgvFields.Rows(5).Cells(15).Value = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.MarkerStyle
                dgvFields.Rows(5).Cells(16).Value = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.MarkerSize
                dgvFields.Rows(5).Cells(17).Value = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.MarkerStep
                dgvFields.Rows(5).Cells(18).Style.BackColor = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.LineColor
                dgvFields.Rows(5).Cells(19).Value = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.LineWidth


                'dgvFields.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)
                dgvFields.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
                dgvFields.AutoResizeColumns()

            ElseIf Distribution.Info(SelDistrib - 1).Continuity = "Discrete" Then
                'dgvFields.Rows.Add(Distribution.Info(SelDistrib - 1).ValueInfo.Name, Distribution.Info(SelDistrib - 1).ValueInfo.Valid, Distribution.Info(SelDistrib - 1).ValueInfo.Generate.ToString, Distribution.Info(SelDistrib - 1).ValueInfo.NumType, Distribution.Info(SelDistrib - 1).ValueInfo.Format, Distribution.Info(SelDistrib - 1).ValueInfo.Alignment, Distribution.Info(SelDistrib - 1).ValueInfo.ValueLabel, Distribution.Info(SelDistrib - 1).ValueInfo.Units, Distribution.Info(SelDistrib - 1).ValueInfo.LabelPrefix, Distribution.Info(SelDistrib - 1).ValueInfo.SeriesLabel, Distribution.Info(SelDistrib - 1).ValueInfo.Description)
                dgvFields.Rows.Add(Distribution.Info(SelDistrib - 1).PmfInfo.Name, Distribution.Info(SelDistrib - 1).PmfInfo.Valid, Distribution.Info(SelDistrib - 1).PmfInfo.Generate.ToString, Distribution.Info(SelDistrib - 1).PmfInfo.NumType, Distribution.Info(SelDistrib - 1).PmfInfo.Format, Distribution.Info(SelDistrib - 1).PmfInfo.Alignment, Distribution.Info(SelDistrib - 1).PmfInfo.ValueLabel, Distribution.Info(SelDistrib - 1).PmfInfo.Units, Distribution.Info(SelDistrib - 1).PmfInfo.LabelPrefix, Distribution.Info(SelDistrib - 1).PmfInfo.SeriesLabel, Distribution.Info(SelDistrib - 1).PmfInfo.Description)
                dgvFields.Rows.Add(Distribution.Info(SelDistrib - 1).PmfLnInfo.Name, Distribution.Info(SelDistrib - 1).PmfLnInfo.Valid, Distribution.Info(SelDistrib - 1).PmfLnInfo.Generate.ToString, Distribution.Info(SelDistrib - 1).PmfLnInfo.NumType, Distribution.Info(SelDistrib - 1).PmfLnInfo.Format, Distribution.Info(SelDistrib - 1).PmfLnInfo.Alignment, Distribution.Info(SelDistrib - 1).PmfLnInfo.ValueLabel, Distribution.Info(SelDistrib - 1).PmfLnInfo.Units, Distribution.Info(SelDistrib - 1).PmfLnInfo.LabelPrefix, Distribution.Info(SelDistrib - 1).PmfLnInfo.SeriesLabel, Distribution.Info(SelDistrib - 1).PmfLnInfo.Description)
                dgvFields.Rows.Add(Distribution.Info(SelDistrib - 1).CdfInfo.Name, Distribution.Info(SelDistrib - 1).CdfInfo.Valid, Distribution.Info(SelDistrib - 1).CdfInfo.Generate.ToString, Distribution.Info(SelDistrib - 1).CdfInfo.NumType, Distribution.Info(SelDistrib - 1).CdfInfo.Format, Distribution.Info(SelDistrib - 1).CdfInfo.Alignment, Distribution.Info(SelDistrib - 1).CdfInfo.ValueLabel, Distribution.Info(SelDistrib - 1).CdfInfo.Units, Distribution.Info(SelDistrib - 1).CdfInfo.LabelPrefix, Distribution.Info(SelDistrib - 1).CdfInfo.SeriesLabel, Distribution.Info(SelDistrib - 1).CdfInfo.Description)

                dgvFields.Rows.Add(Distribution.Info(SelDistrib - 1).RevCdfInfo.Name, Distribution.Info(SelDistrib - 1).RevCdfInfo.Valid, Distribution.Info(SelDistrib - 1).RevCdfInfo.Generate.ToString, Distribution.Info(SelDistrib - 1).RevCdfInfo.NumType, Distribution.Info(SelDistrib - 1).RevCdfInfo.Format, Distribution.Info(SelDistrib - 1).RevCdfInfo.Alignment, Distribution.Info(SelDistrib - 1).RevCdfInfo.ValueLabel, Distribution.Info(SelDistrib - 1).RevCdfInfo.Units, Distribution.Info(SelDistrib - 1).RevCdfInfo.LabelPrefix, Distribution.Info(SelDistrib - 1).RevCdfInfo.SeriesLabel, Distribution.Info(SelDistrib - 1).RevCdfInfo.Description)

                dgvFields.Rows.Add(Distribution.Info(SelDistrib - 1).InvCdfInfo.Name, Distribution.Info(SelDistrib - 1).InvCdfInfo.Valid, Distribution.Info(SelDistrib - 1).InvCdfInfo.Generate.ToString, Distribution.Info(SelDistrib - 1).InvCdfInfo.NumType, Distribution.Info(SelDistrib - 1).InvCdfInfo.Format, Distribution.Info(SelDistrib - 1).InvCdfInfo.Alignment, Distribution.Info(SelDistrib - 1).InvCdfInfo.ValueLabel, Distribution.Info(SelDistrib - 1).InvCdfInfo.Units, Distribution.Info(SelDistrib - 1).InvCdfInfo.LabelPrefix, Distribution.Info(SelDistrib - 1).InvCdfInfo.SeriesLabel, Distribution.Info(SelDistrib - 1).InvCdfInfo.Description)

                dgvFields.Rows.Add(Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Name, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Valid, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Generate.ToString, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.NumType, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Format, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Alignment, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.ValueLabel, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Units, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.LabelPrefix, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.SeriesLabel, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Description)

                'Add PMF display settings:
                'dgvFields.Rows(1).Cells(11).Value = Distribution.Info(SelDistrib - 1).PmfInfo.Display.MarkerFill
                dgvFields.Rows(0).Cells(11).Value = Distribution.Info(SelDistrib - 1).PmfInfo.Display.MarkerFill
                dgvFields.Rows(0).Cells(12).Style.BackColor = Distribution.Info(SelDistrib - 1).PmfInfo.Display.MarkerColor
                dgvFields.Rows(0).Cells(13).Style.BackColor = Distribution.Info(SelDistrib - 1).PmfInfo.Display.BorderColor
                dgvFields.Rows(0).Cells(14).Value = Distribution.Info(SelDistrib - 1).PmfInfo.Display.BorderWidth
                dgvFields.Rows(0).Cells(15).Value = Distribution.Info(SelDistrib - 1).PmfInfo.Display.MarkerStyle
                dgvFields.Rows(0).Cells(16).Value = Distribution.Info(SelDistrib - 1).PmfInfo.Display.MarkerSize
                dgvFields.Rows(0).Cells(17).Value = Distribution.Info(SelDistrib - 1).PmfInfo.Display.MarkerStep
                dgvFields.Rows(0).Cells(18).Style.BackColor = Distribution.Info(SelDistrib - 1).PmfInfo.Display.LineColor
                dgvFields.Rows(0).Cells(19).Value = Distribution.Info(SelDistrib - 1).PmfInfo.Display.LineWidth

                'Add PMFLn display settings:
                dgvFields.Rows(1).Cells(11).Value = Distribution.Info(SelDistrib - 1).PmfLnInfo.Display.MarkerFill
                dgvFields.Rows(1).Cells(12).Style.BackColor = Distribution.Info(SelDistrib - 1).PmfLnInfo.Display.MarkerColor
                dgvFields.Rows(1).Cells(13).Style.BackColor = Distribution.Info(SelDistrib - 1).PmfLnInfo.Display.BorderColor
                dgvFields.Rows(1).Cells(14).Value = Distribution.Info(SelDistrib - 1).PmfLnInfo.Display.BorderWidth
                dgvFields.Rows(1).Cells(15).Value = Distribution.Info(SelDistrib - 1).PmfLnInfo.Display.MarkerStyle
                dgvFields.Rows(1).Cells(16).Value = Distribution.Info(SelDistrib - 1).PmfLnInfo.Display.MarkerSize
                dgvFields.Rows(1).Cells(17).Value = Distribution.Info(SelDistrib - 1).PmfLnInfo.Display.MarkerStep
                dgvFields.Rows(1).Cells(18).Style.BackColor = Distribution.Info(SelDistrib - 1).PmfLnInfo.Display.LineColor
                dgvFields.Rows(1).Cells(19).Value = Distribution.Info(SelDistrib - 1).PmfLnInfo.Display.LineWidth

                'Add CDF display settings:
                dgvFields.Rows(2).Cells(11).Value = Distribution.Info(SelDistrib - 1).CdfInfo.Display.MarkerFill
                dgvFields.Rows(2).Cells(12).Style.BackColor = Distribution.Info(SelDistrib - 1).CdfInfo.Display.MarkerColor
                dgvFields.Rows(2).Cells(13).Style.BackColor = Distribution.Info(SelDistrib - 1).CdfInfo.Display.BorderColor
                dgvFields.Rows(2).Cells(14).Value = Distribution.Info(SelDistrib - 1).CdfInfo.Display.BorderWidth
                dgvFields.Rows(2).Cells(15).Value = Distribution.Info(SelDistrib - 1).CdfInfo.Display.MarkerStyle
                dgvFields.Rows(2).Cells(16).Value = Distribution.Info(SelDistrib - 1).CdfInfo.Display.MarkerSize
                dgvFields.Rows(2).Cells(17).Value = Distribution.Info(SelDistrib - 1).CdfInfo.Display.MarkerStep
                dgvFields.Rows(2).Cells(18).Style.BackColor = Distribution.Info(SelDistrib - 1).CdfInfo.Display.LineColor
                dgvFields.Rows(2).Cells(19).Value = Distribution.Info(SelDistrib - 1).CdfInfo.Display.LineWidth

                'Add RevCDF display settings:
                dgvFields.Rows(3).Cells(11).Value = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.MarkerFill
                dgvFields.Rows(3).Cells(12).Style.BackColor = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.MarkerColor
                dgvFields.Rows(3).Cells(13).Style.BackColor = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.BorderColor
                dgvFields.Rows(3).Cells(14).Value = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.BorderWidth
                dgvFields.Rows(3).Cells(15).Value = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.MarkerStyle
                dgvFields.Rows(3).Cells(16).Value = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.MarkerSize
                dgvFields.Rows(3).Cells(17).Value = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.MarkerStep
                dgvFields.Rows(3).Cells(18).Style.BackColor = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.LineColor
                dgvFields.Rows(3).Cells(19).Value = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.LineWidth

                'Add InvCDF display settings:
                dgvFields.Rows(4).Cells(11).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerFill
                dgvFields.Rows(4).Cells(12).Style.BackColor = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerColor
                dgvFields.Rows(4).Cells(13).Style.BackColor = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.BorderColor
                dgvFields.Rows(4).Cells(14).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.BorderWidth
                dgvFields.Rows(4).Cells(15).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerStyle
                dgvFields.Rows(4).Cells(16).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerSize
                dgvFields.Rows(4).Cells(17).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerStep
                dgvFields.Rows(4).Cells(18).Style.BackColor = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.LineColor
                dgvFields.Rows(4).Cells(19).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.LineWidth

                'Add InvRevCDF display settings:
                dgvFields.Rows(5).Cells(11).Value = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.MarkerFill
                dgvFields.Rows(5).Cells(12).Style.BackColor = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.MarkerColor
                dgvFields.Rows(5).Cells(13).Style.BackColor = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.BorderColor
                dgvFields.Rows(5).Cells(14).Value = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.BorderWidth
                dgvFields.Rows(5).Cells(15).Value = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.MarkerStyle
                dgvFields.Rows(5).Cells(16).Value = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.MarkerSize
                dgvFields.Rows(5).Cells(17).Value = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.MarkerStep
                dgvFields.Rows(5).Cells(18).Style.BackColor = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.LineColor
                dgvFields.Rows(5).Cells(19).Value = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.LineWidth






                'dgvFields.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)
                dgvFields.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
                dgvFields.AutoResizeColumns()

            Else
                'Invalid continuity string.
            End If

            'Update the X Axis Fields tab: -------------------------------------------------------------------------
            dgvXFields.Rows.Clear()
            dgvXFields.Rows.Add(Distribution.XValueInfo.Name, Distribution.XValueInfo.NumType, Distribution.XValueInfo.Format, Distribution.XValueInfo.Alignment, Distribution.XValueInfo.ValueLabel, Distribution.XValueInfo.Units, Distribution.XValueInfo.Description)
            dgvXFields.Rows.Add(Distribution.XProbInfo.Name, Distribution.XProbInfo.NumType, Distribution.XProbInfo.Format, Distribution.XProbInfo.Alignment, Distribution.XProbInfo.ValueLabel, Distribution.XProbInfo.Units, Distribution.XProbInfo.Description)

            dgvXFields.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            dgvXFields.AutoResizeColumns()

        End If
    End Sub

    Public Sub UpdateParameters()
        'Update the displayed distribution parameters

        If Distribution.Info(SelDistrib - 1).NParams > 0 Then
            dgvParams.Rows(0).Cells(0).Value = Distribution.Info(SelDistrib - 1).ParamA.Name
            dgvParams.Rows(0).Cells(1).Value = NameToSymbol(Distribution.Info(SelDistrib - 1).ParamA.Symbol)
            dgvParams.Rows(0).Cells(2).Value = Distribution.Info(SelDistrib - 1).ParamA.Value
            dgvParams.Rows(0).Cells(3).Value = Distribution.Info(SelDistrib - 1).ParamA.Type
            dgvParams.Rows(0).Cells(4).Value = Distribution.Info(SelDistrib - 1).ParamA.NumberType
            dgvParams.Rows(0).Cells(5).Value = Distribution.Info(SelDistrib - 1).ParamA.Minimum
            dgvParams.Rows(0).Cells(6).Value = Distribution.Info(SelDistrib - 1).ParamA.Maximum
            dgvParams.Rows(0).Cells(7).Value = Distribution.Info(SelDistrib - 1).ParamA.Description
            If Distribution.Info(SelDistrib - 1).NParams > 1 Then
                dgvParams.Rows(1).Cells(0).Value = Distribution.Info(SelDistrib - 1).ParamB.Name
                dgvParams.Rows(1).Cells(1).Value = NameToSymbol(Distribution.Info(SelDistrib - 1).ParamB.Symbol)
                dgvParams.Rows(1).Cells(2).Value = Distribution.Info(SelDistrib - 1).ParamB.Value
                dgvParams.Rows(1).Cells(3).Value = Distribution.Info(SelDistrib - 1).ParamB.Type
                dgvParams.Rows(1).Cells(4).Value = Distribution.Info(SelDistrib - 1).ParamB.NumberType
                dgvParams.Rows(1).Cells(5).Value = Distribution.Info(SelDistrib - 1).ParamB.Minimum
                dgvParams.Rows(1).Cells(6).Value = Distribution.Info(SelDistrib - 1).ParamB.Maximum
                dgvParams.Rows(1).Cells(7).Value = Distribution.Info(SelDistrib - 1).ParamB.Description
                If Distribution.Info(SelDistrib - 1).NParams > 2 Then
                    dgvParams.Rows(2).Cells(0).Value = Distribution.Info(SelDistrib - 1).ParamC.Name
                    dgvParams.Rows(2).Cells(1).Value = NameToSymbol(Distribution.Info(SelDistrib - 1).ParamC.Symbol)
                    dgvParams.Rows(2).Cells(2).Value = Distribution.Info(SelDistrib - 1).ParamC.Value
                    dgvParams.Rows(2).Cells(3).Value = Distribution.Info(SelDistrib - 1).ParamC.Type
                    dgvParams.Rows(2).Cells(4).Value = Distribution.Info(SelDistrib - 1).ParamC.NumberType
                    dgvParams.Rows(2).Cells(5).Value = Distribution.Info(SelDistrib - 1).ParamC.Minimum
                    dgvParams.Rows(2).Cells(6).Value = Distribution.Info(SelDistrib - 1).ParamC.Maximum
                    dgvParams.Rows(2).Cells(7).Value = Distribution.Info(SelDistrib - 1).ParamC.Description
                    If Distribution.Info(SelDistrib - 1).NParams > 3 Then
                        dgvParams.Rows(3).Cells(0).Value = Distribution.Info(SelDistrib - 1).ParamD.Name
                        dgvParams.Rows(3).Cells(1).Value = NameToSymbol(Distribution.Info(SelDistrib - 1).ParamD.Symbol)
                        dgvParams.Rows(3).Cells(2).Value = Distribution.Info(SelDistrib - 1).ParamD.Value
                        dgvParams.Rows(3).Cells(3).Value = Distribution.Info(SelDistrib - 1).ParamD.Type
                        dgvParams.Rows(3).Cells(4).Value = Distribution.Info(SelDistrib - 1).ParamD.NumberType
                        dgvParams.Rows(3).Cells(5).Value = Distribution.Info(SelDistrib - 1).ParamD.Minimum
                        dgvParams.Rows(3).Cells(6).Value = Distribution.Info(SelDistrib - 1).ParamD.Maximum
                        dgvParams.Rows(3).Cells(7).Value = Distribution.Info(SelDistrib - 1).ParamD.Description
                        If Distribution.Info(SelDistrib - 1).NParams > 4 Then
                            dgvParams.Rows(4).Cells(0).Value = Distribution.Info(SelDistrib - 1).ParamE.Name
                            dgvParams.Rows(4).Cells(1).Value = NameToSymbol(Distribution.Info(SelDistrib - 1).ParamE.Symbol)
                            dgvParams.Rows(4).Cells(2).Value = Distribution.Info(SelDistrib - 1).ParamE.Value
                            dgvParams.Rows(4).Cells(3).Value = Distribution.Info(SelDistrib - 1).ParamE.Type
                            dgvParams.Rows(4).Cells(4).Value = Distribution.Info(SelDistrib - 1).ParamE.NumberType
                            dgvParams.Rows(4).Cells(5).Value = Distribution.Info(SelDistrib - 1).ParamE.Minimum
                            dgvParams.Rows(4).Cells(6).Value = Distribution.Info(SelDistrib - 1).ParamE.Maximum
                            dgvParams.Rows(4).Cells(7).Value = Distribution.Info(SelDistrib - 1).ParamE.Description
                            If Distribution.Info(SelDistrib - 1).NParams > 5 Then
                                'Too many parameters!
                            End If
                        End If
                    End If
                End If
            End If
        End If

        'txtSuffix.Text = Distribution.Info(SelDistrib - 1).Suffix
        UpdateSuffix()

        'dgvParams.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)
        dgvParams.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        dgvParams.AutoResizeColumns()
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

    Private Sub dgvFields_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvFields.CellContentClick

    End Sub

    Private Sub dgvFields_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvFields.CellClick
        Dim CellCol As Integer = e.ColumnIndex


        If CellCol = 12 Then 'Marker color changed.
            Dim CellRow As Integer = e.RowIndex
            'If CellRow = 0 Then
            '    'This is the Value field - no display settings are specified - this is the X axis coordinate value for PDF, PDFLn, PMF, PMFLn and CDF displays.
            'ElseIf CellRow = 4 Then
            '    'This is the Probability field - no display settings are specified - this is the X axis coordinate value for InvCDF displays.
            'Else
            ColorDialog1.Color = dgvFields.Rows(CellRow).Cells(CellCol).Style.BackColor
            ColorDialog1.ShowDialog()
            dgvFields.Rows(CellRow).Cells(CellCol).Style.BackColor = ColorDialog1.Color
            Select Case dgvFields.Rows(CellRow).Cells(0).Value
                    'Case "PDF"
                Case "PDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).PdfInfo.Display.MarkerColor = ColorDialog1.Color
                Case "PDFLn_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).PdfLnInfo.Display.MarkerColor = ColorDialog1.Color
                Case "PMF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).PmfInfo.Display.MarkerColor = ColorDialog1.Color
                Case "PMFLn_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).PmfLnInfo.Display.MarkerColor = ColorDialog1.Color
                Case "CDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).CdfInfo.Display.MarkerColor = ColorDialog1.Color
                Case "RevCDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.MarkerColor = ColorDialog1.Color
                Case "InvCDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerColor = ColorDialog1.Color
                Case "InvRevCDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.MarkerColor = ColorDialog1.Color
                Case Else
                    Message.AddWarning("Unknown field name: " & dgvFields.Rows(CellRow).Cells(0).Value & vbCrLf)
            End Select
            'End If

        ElseIf CellCol = 13 Then 'Border color changed
            Dim CellRow As Integer = e.RowIndex
            'If CellRow = 0 Then
            '    'This is the Value field - no display settings are specified - this is the X axis coordinate value for PDF, PDFLn, PMF, PMFLn and CDF displays.
            'ElseIf CellRow = 4 Then
            '    'This is the Probability field - no display settings are specified - this is the X axis coordinate value for InvCDF displays.
            'Else
            ColorDialog1.Color = dgvFields.Rows(CellRow).Cells(CellCol).Style.BackColor
            ColorDialog1.ShowDialog()
            dgvFields.Rows(CellRow).Cells(CellCol).Style.BackColor = ColorDialog1.Color
            Select Case dgvFields.Rows(CellRow).Cells(0).Value
                    'Case "PDF"
                Case "PDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).PdfInfo.Display.BorderColor = ColorDialog1.Color
                Case "PDFLn_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).PdfLnInfo.Display.BorderColor = ColorDialog1.Color
                Case "PMF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).PmfInfo.Display.BorderColor = ColorDialog1.Color
                Case "PMFLn_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).PmfLnInfo.Display.BorderColor = ColorDialog1.Color
                Case "CDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).CdfInfo.Display.BorderColor = ColorDialog1.Color
                Case "RevCDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.BorderColor = ColorDialog1.Color
                Case "InvCDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.BorderColor = ColorDialog1.Color
                Case "InvRevCDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.BorderColor = ColorDialog1.Color
                Case Else
                    Message.AddWarning("Unknown field name: " & dgvFields.Rows(CellRow).Cells(0).Value & vbCrLf)
            End Select
            'End If

        ElseIf CellCol = 18 Then 'Line Color Changed
            Dim CellRow As Integer = e.RowIndex
            'If CellRow = 0 Then
            '    'This is the Value field - no display settings are specified - this is the X axis coordinate value for PDF, PDFLn, PMF, PMFLn and CDF displays.
            'ElseIf CellRow = 4 Then
            '    'This is the Probability field - no display settings are specified - this is the X axis coordinate value for InvCDF displays.
            'Else
            ColorDialog1.Color = dgvFields.Rows(CellRow).Cells(CellCol).Style.BackColor
            ColorDialog1.ShowDialog()
            dgvFields.Rows(CellRow).Cells(CellCol).Style.BackColor = ColorDialog1.Color
            Select Case dgvFields.Rows(CellRow).Cells(0).Value
                    'Case "PDF"
                Case "PDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).PdfInfo.Display.LineColor = ColorDialog1.Color
                Case "PDFLn_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).PdfLnInfo.Display.LineColor = ColorDialog1.Color
                Case "PMF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).PmfInfo.Display.LineColor = ColorDialog1.Color
                Case "PMFLn_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).PmfLnInfo.Display.LineColor = ColorDialog1.Color
                Case "CDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).CdfInfo.Display.LineColor = ColorDialog1.Color
                Case "RevCDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.LineColor = ColorDialog1.Color
                Case "InvCDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.LineColor = ColorDialog1.Color
                Case "InvRevCDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.LineColor = ColorDialog1.Color
                Case Else
                    Message.AddWarning("Unknown field name: " & dgvFields.Rows(CellRow).Cells(0).Value & vbCrLf)
            End Select
            'End If
        Else
            'The clicked cell was not used to select a color.
        End If
    End Sub




    Private Sub dgvFields_DataError(sender As Object, e As DataGridViewDataErrorEventArgs)
        'Message.AddWarning(e.Exception.Message & vbCrLf)
    End Sub

    Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
        GenerateData()
        'Distribution.GenerateData()
        'dgvDataTable.AutoGenerateColumns = True
        'dgvDataTable.DataSource = Distribution.Data.Tables("DataTable")

        ''Apply the cell formats:
        'If Distribution.ValueInfo.Generate Then dgvDataTable.Columns("Value").DefaultCellStyle.Format = Distribution.ValueInfo.Format
        'If Distribution.PdfInfo.Generate Then dgvDataTable.Columns("PDF").DefaultCellStyle.Format = Distribution.PdfInfo.Format
        'If Distribution.PdfLnInfo.Generate Then dgvDataTable.Columns("PDFLn").DefaultCellStyle.Format = Distribution.PdfLnInfo.Format
        'If Distribution.CdfInfo.Generate Then dgvDataTable.Columns("CDF").DefaultCellStyle.Format = Distribution.CdfInfo.Format
        'If Distribution.ProbabilityInfo.Generate Then dgvDataTable.Columns("Probability").DefaultCellStyle.Format = Distribution.ProbabilityInfo.Format
        'If Distribution.InvCdfInfo.Generate Then dgvDataTable.Columns("InvCDF").DefaultCellStyle.Format = Distribution.InvCdfInfo.Format

        ''Apply the cell alignments:
        'If Distribution.ValueInfo.Generate Then dgvDataTable.Columns("Value").DefaultCellStyle.Alignment = DirectCast([Enum].Parse(GetType(DataGridViewContentAlignment), Distribution.ValueInfo.Alignment), DataGridViewContentAlignment)
        'If Distribution.PdfInfo.Generate Then dgvDataTable.Columns("PDF").DefaultCellStyle.Alignment = DirectCast([Enum].Parse(GetType(DataGridViewContentAlignment), Distribution.PdfInfo.Alignment), DataGridViewContentAlignment)
        'If Distribution.PdfLnInfo.Generate Then dgvDataTable.Columns("PDFLn").DefaultCellStyle.Alignment = DirectCast([Enum].Parse(GetType(DataGridViewContentAlignment), Distribution.PdfLnInfo.Alignment), DataGridViewContentAlignment)
        'If Distribution.CdfInfo.Generate Then dgvDataTable.Columns("CDF").DefaultCellStyle.Alignment = DirectCast([Enum].Parse(GetType(DataGridViewContentAlignment), Distribution.CdfInfo.Alignment), DataGridViewContentAlignment)
        'If Distribution.ProbabilityInfo.Generate Then dgvDataTable.Columns("Probability").DefaultCellStyle.Alignment = DirectCast([Enum].Parse(GetType(DataGridViewContentAlignment), Distribution.ProbabilityInfo.Alignment), DataGridViewContentAlignment)
        'If Distribution.InvCdfInfo.Generate Then dgvDataTable.Columns("InvCDF").DefaultCellStyle.Alignment = DirectCast([Enum].Parse(GetType(DataGridViewContentAlignment), Distribution.InvCdfInfo.Alignment), DataGridViewContentAlignment)

        'dgvDataTable.AutoResizeColumns()
        'dgvDataTable.Update()
        'dgvDataTable.Refresh()
    End Sub



    Public Sub GenerateData()
        Distribution.GenerateData()

        cmbTableName.Items.Clear()

        For Each Item As DataTable In Distribution.Data.Tables
            cmbTableName.Items.Add(Item.TableName)
        Next

        If cmbTableName.Items.Count > 0 Then
            cmbTableName.SelectedIndex = 0
            SelTableName = cmbTableName.SelectedItem.ToString
        End If

        UpdateDataTableDisplay()
        'dgvDataTable.AutoGenerateColumns = True
        'dgvDataTable.DataSource = Distribution.Data.Tables("DataTable")

        ''Apply the cell formats:
        'If Distribution.Distrib.ValueInfo.Generate And Distribution.Distrib.ValueInfo.Valid Then dgvDataTable.Columns("Value").DefaultCellStyle.Format = Distribution.Distrib.ValueInfo.Format
        'If Distribution.Distrib.PdfInfo.Generate And Distribution.Distrib.PdfInfo.Valid Then dgvDataTable.Columns("PDF").DefaultCellStyle.Format = Distribution.Distrib.PdfInfo.Format
        'If Distribution.Distrib.PdfLnInfo.Generate And Distribution.Distrib.PdfLnInfo.Valid Then dgvDataTable.Columns("PDFLn").DefaultCellStyle.Format = Distribution.Distrib.PdfLnInfo.Format
        'If Distribution.Distrib.CdfInfo.Generate And Distribution.Distrib.CdfInfo.Valid Then dgvDataTable.Columns("CDF").DefaultCellStyle.Format = Distribution.Distrib.CdfInfo.Format
        'If Distribution.Distrib.ProbabilityInfo.Generate And Distribution.Distrib.ProbabilityInfo.Valid Then dgvDataTable.Columns("Probability").DefaultCellStyle.Format = Distribution.Distrib.ProbabilityInfo.Format
        'If Distribution.Distrib.InvCdfInfo.Generate And Distribution.Distrib.InvCdfInfo.Valid Then dgvDataTable.Columns("InvCDF").DefaultCellStyle.Format = Distribution.Distrib.InvCdfInfo.Format

        ''Apply the cell alignments:
        'If Distribution.Distrib.ValueInfo.Generate And Distribution.Distrib.ValueInfo.Valid Then dgvDataTable.Columns("Value").DefaultCellStyle.Alignment = DirectCast([Enum].Parse(GetType(DataGridViewContentAlignment), Distribution.Distrib.ValueInfo.Alignment), DataGridViewContentAlignment)
        'If Distribution.Distrib.PdfInfo.Generate And Distribution.Distrib.PdfInfo.Valid Then dgvDataTable.Columns("PDF").DefaultCellStyle.Alignment = DirectCast([Enum].Parse(GetType(DataGridViewContentAlignment), Distribution.Distrib.PdfInfo.Alignment), DataGridViewContentAlignment)
        'If Distribution.Distrib.PdfLnInfo.Generate And Distribution.Distrib.PdfLnInfo.Valid Then dgvDataTable.Columns("PDFLn").DefaultCellStyle.Alignment = DirectCast([Enum].Parse(GetType(DataGridViewContentAlignment), Distribution.Distrib.PdfLnInfo.Alignment), DataGridViewContentAlignment)
        'If Distribution.Distrib.CdfInfo.Generate And Distribution.Distrib.CdfInfo.Valid Then dgvDataTable.Columns("CDF").DefaultCellStyle.Alignment = DirectCast([Enum].Parse(GetType(DataGridViewContentAlignment), Distribution.Distrib.CdfInfo.Alignment), DataGridViewContentAlignment)
        'If Distribution.Distrib.ProbabilityInfo.Generate And Distribution.Distrib.ProbabilityInfo.Valid Then dgvDataTable.Columns("Probability").DefaultCellStyle.Alignment = DirectCast([Enum].Parse(GetType(DataGridViewContentAlignment), Distribution.Distrib.ProbabilityInfo.Alignment), DataGridViewContentAlignment)
        'If Distribution.Distrib.InvCdfInfo.Generate And Distribution.Distrib.InvCdfInfo.Valid Then dgvDataTable.Columns("InvCDF").DefaultCellStyle.Alignment = DirectCast([Enum].Parse(GetType(DataGridViewContentAlignment), Distribution.Distrib.InvCdfInfo.Alignment), DataGridViewContentAlignment)

        'dgvDataTable.AutoResizeColumns()
        'dgvDataTable.Update()
        'dgvDataTable.Refresh()
    End Sub

    Public Sub UpdateDataTableDisplay()
        'Update the Data Table display.

        If SelTableName = "" Then
            dgvDataTable.DataSource = vbNull
        Else
            dgvDataTable.AutoGenerateColumns = True
            'dgvDataTable.DataSource = Distribution.Data.Tables("DataTable")
            'dgvDataTable.DataSource = Distribution.Data.Tables("ContinuousDataTable")

            If Distribution.Data.Tables.Contains(SelTableName) Then
                dgvDataTable.DataSource = Distribution.Data.Tables(SelTableName)

                If SelTableName = "Continuous_Data_Table" Then

                    'Apply the cell formats:
                    Dim I As Integer
                    For I = 1 To NDistribs
                        If Distribution.Info(I - 1).Continuity = "Continuous" Then
                            Try
                                'If Distribution.Info(SelDistrib - 1).PdfInfo.Generate And Distribution.Info(SelDistrib - 1).PdfInfo.Valid Then dgvDataTable.Columns("PDF_" & I).DefaultCellStyle.Format = Distribution.Info(SelDistrib - 1).PdfInfo.Format
                                'If Distribution.Info(SelDistrib - 1).PdfLnInfo.Generate And Distribution.Info(SelDistrib - 1).PdfLnInfo.Valid Then dgvDataTable.Columns("PDFLn_" & I).DefaultCellStyle.Format = Distribution.Info(SelDistrib - 1).PdfLnInfo.Format
                                'If Distribution.Info(SelDistrib - 1).CdfInfo.Generate And Distribution.Info(SelDistrib - 1).CdfInfo.Valid Then dgvDataTable.Columns("CDF_" & I).DefaultCellStyle.Format = Distribution.Info(SelDistrib - 1).CdfInfo.Format
                                'If Distribution.Info(SelDistrib - 1).InvCdfInfo.Generate And Distribution.Info(SelDistrib - 1).InvCdfInfo.Valid Then dgvDataTable.Columns("InvCDF_" & I).DefaultCellStyle.Format = Distribution.Info(SelDistrib - 1).InvCdfInfo.Format

                                ''Apply the cell alignments:
                                'If Distribution.Info(SelDistrib - 1).PdfInfo.Generate And Distribution.Info(SelDistrib - 1).PdfInfo.Valid Then dgvDataTable.Columns("PDF_" & I).DefaultCellStyle.Alignment = DirectCast([Enum].Parse(GetType(DataGridViewContentAlignment), Distribution.Info(SelDistrib - 1).PdfInfo.Alignment), DataGridViewContentAlignment)
                                'If Distribution.Info(SelDistrib - 1).PdfLnInfo.Generate And Distribution.Info(SelDistrib - 1).PdfLnInfo.Valid Then dgvDataTable.Columns("PDFLn_" & I).DefaultCellStyle.Alignment = DirectCast([Enum].Parse(GetType(DataGridViewContentAlignment), Distribution.Info(SelDistrib - 1).PdfLnInfo.Alignment), DataGridViewContentAlignment)
                                'If Distribution.Info(SelDistrib - 1).CdfInfo.Generate And Distribution.Info(SelDistrib - 1).CdfInfo.Valid Then dgvDataTable.Columns("CDF_" & I).DefaultCellStyle.Alignment = DirectCast([Enum].Parse(GetType(DataGridViewContentAlignment), Distribution.Info(SelDistrib - 1).CdfInfo.Alignment), DataGridViewContentAlignment)
                                'If Distribution.Info(SelDistrib - 1).InvCdfInfo.Generate And Distribution.Info(SelDistrib - 1).InvCdfInfo.Valid Then dgvDataTable.Columns("InvCDF_" & I).DefaultCellStyle.Alignment = DirectCast([Enum].Parse(GetType(DataGridViewContentAlignment), Distribution.Info(SelDistrib - 1).InvCdfInfo.Alignment), DataGridViewContentAlignment)
                                If Distribution.Info(I - 1).Continuity = "Continuous" Then
                                    If Distribution.Info(I - 1).PdfInfo.Generate And Distribution.Info(I - 1).PdfInfo.Valid Then dgvDataTable.Columns("PDF_" & I).DefaultCellStyle.Format = Distribution.Info(I - 1).PdfInfo.Format
                                    If Distribution.Info(I - 1).PdfLnInfo.Generate And Distribution.Info(I - 1).PdfLnInfo.Valid Then dgvDataTable.Columns("PDFLn_" & I).DefaultCellStyle.Format = Distribution.Info(I - 1).PdfLnInfo.Format
                                    If Distribution.Info(I - 1).CdfInfo.Generate And Distribution.Info(I - 1).CdfInfo.Valid Then dgvDataTable.Columns("CDF_" & I).DefaultCellStyle.Format = Distribution.Info(I - 1).CdfInfo.Format
                                    If Distribution.Info(I - 1).RevCdfInfo.Generate And Distribution.Info(I - 1).RevCdfInfo.Valid Then dgvDataTable.Columns("RevCDF_" & I).DefaultCellStyle.Format = Distribution.Info(I - 1).RevCdfInfo.Format
                                    If Distribution.Info(I - 1).InvCdfInfo.Generate And Distribution.Info(I - 1).InvCdfInfo.Valid Then dgvDataTable.Columns("InvCDF_" & I).DefaultCellStyle.Format = Distribution.Info(I - 1).InvCdfInfo.Format
                                    If Distribution.Info(I - 1).InvRevCdfInfo.Generate And Distribution.Info(I - 1).InvRevCdfInfo.Valid Then dgvDataTable.Columns("InvRevCDF_" & I).DefaultCellStyle.Format = Distribution.Info(I - 1).InvRevCdfInfo.Format

                                    'Apply the cell alignments:
                                    If Distribution.Info(I - 1).PdfInfo.Generate And Distribution.Info(I - 1).PdfInfo.Valid Then dgvDataTable.Columns("PDF_" & I).DefaultCellStyle.Alignment = DirectCast([Enum].Parse(GetType(DataGridViewContentAlignment), Distribution.Info(I - 1).PdfInfo.Alignment), DataGridViewContentAlignment)
                                    If Distribution.Info(I - 1).PdfLnInfo.Generate And Distribution.Info(I - 1).PdfLnInfo.Valid Then dgvDataTable.Columns("PDFLn_" & I).DefaultCellStyle.Alignment = DirectCast([Enum].Parse(GetType(DataGridViewContentAlignment), Distribution.Info(I - 1).PdfLnInfo.Alignment), DataGridViewContentAlignment)
                                    If Distribution.Info(I - 1).CdfInfo.Generate And Distribution.Info(I - 1).CdfInfo.Valid Then dgvDataTable.Columns("CDF_" & I).DefaultCellStyle.Alignment = DirectCast([Enum].Parse(GetType(DataGridViewContentAlignment), Distribution.Info(I - 1).CdfInfo.Alignment), DataGridViewContentAlignment)
                                    If Distribution.Info(I - 1).RevCdfInfo.Generate And Distribution.Info(I - 1).RevCdfInfo.Valid Then dgvDataTable.Columns("RevCDF_" & I).DefaultCellStyle.Alignment = DirectCast([Enum].Parse(GetType(DataGridViewContentAlignment), Distribution.Info(I - 1).RevCdfInfo.Alignment), DataGridViewContentAlignment)
                                    If Distribution.Info(I - 1).InvCdfInfo.Generate And Distribution.Info(I - 1).InvCdfInfo.Valid Then dgvDataTable.Columns("InvCDF_" & I).DefaultCellStyle.Alignment = DirectCast([Enum].Parse(GetType(DataGridViewContentAlignment), Distribution.Info(I - 1).InvCdfInfo.Alignment), DataGridViewContentAlignment)
                                    If Distribution.Info(I - 1).InvRevCdfInfo.Generate And Distribution.Info(I - 1).InvRevCdfInfo.Valid Then dgvDataTable.Columns("InvRevCDF_" & I).DefaultCellStyle.Alignment = DirectCast([Enum].Parse(GetType(DataGridViewContentAlignment), Distribution.Info(I - 1).InvRevCdfInfo.Alignment), DataGridViewContentAlignment)
                                End If
                            Catch ex As Exception
                                Message.AddWarning("Error updating table: Continuous_Data_Table " & vbCrLf & ex.Message & vbCrLf)
                            End Try
                        End If
                    Next
                ElseIf SelTableName = "Discrete_Data_Table" Then
                    'Apply the cell formats:
                    Dim I As Integer
                    For I = 1 To NDistribs
                        If Distribution.Info(I - 1).Continuity = "Discrete" Then
                            Try
                                'If Distribution.Info(SelDistrib - 1).PdfInfo.Generate And Distribution.Info(SelDistrib - 1).PdfInfo.Valid Then dgvDataTable.Columns("PDF_" & I).DefaultCellStyle.Format = Distribution.Info(SelDistrib - 1).PdfInfo.Format
                                'If Distribution.Info(SelDistrib - 1).PdfLnInfo.Generate And Distribution.Info(SelDistrib - 1).PdfLnInfo.Valid Then dgvDataTable.Columns("PDFLn_" & I).DefaultCellStyle.Format = Distribution.Info(SelDistrib - 1).PdfLnInfo.Format
                                'If Distribution.Info(SelDistrib - 1).CdfInfo.Generate And Distribution.Info(SelDistrib - 1).CdfInfo.Valid Then dgvDataTable.Columns("CDF_" & I).DefaultCellStyle.Format = Distribution.Info(SelDistrib - 1).CdfInfo.Format
                                'If Distribution.Info(SelDistrib - 1).InvCdfInfo.Generate And Distribution.Info(SelDistrib - 1).InvCdfInfo.Valid Then dgvDataTable.Columns("InvCDF_" & I).DefaultCellStyle.Format = Distribution.Info(SelDistrib - 1).InvCdfInfo.Format

                                ''Apply the cell alignments:
                                'If Distribution.Info(SelDistrib - 1).PdfInfo.Generate And Distribution.Info(SelDistrib - 1).PdfInfo.Valid Then dgvDataTable.Columns("PDF_" & I).DefaultCellStyle.Alignment = DirectCast([Enum].Parse(GetType(DataGridViewContentAlignment), Distribution.Info(SelDistrib - 1).PdfInfo.Alignment), DataGridViewContentAlignment)
                                'If Distribution.Info(SelDistrib - 1).PdfLnInfo.Generate And Distribution.Info(SelDistrib - 1).PdfLnInfo.Valid Then dgvDataTable.Columns("PDFLn_" & I).DefaultCellStyle.Alignment = DirectCast([Enum].Parse(GetType(DataGridViewContentAlignment), Distribution.Info(SelDistrib - 1).PdfLnInfo.Alignment), DataGridViewContentAlignment)
                                'If Distribution.Info(SelDistrib - 1).CdfInfo.Generate And Distribution.Info(SelDistrib - 1).CdfInfo.Valid Then dgvDataTable.Columns("CDF_" & I).DefaultCellStyle.Alignment = DirectCast([Enum].Parse(GetType(DataGridViewContentAlignment), Distribution.Info(SelDistrib - 1).CdfInfo.Alignment), DataGridViewContentAlignment)
                                'If Distribution.Info(SelDistrib - 1).InvCdfInfo.Generate And Distribution.Info(SelDistrib - 1).InvCdfInfo.Valid Then dgvDataTable.Columns("InvCDF_" & I).DefaultCellStyle.Alignment = DirectCast([Enum].Parse(GetType(DataGridViewContentAlignment), Distribution.Info(SelDistrib - 1).InvCdfInfo.Alignment), DataGridViewContentAlignment)

                                If Distribution.Info(I - 1).Continuity = "Discrete" Then
                                    If Distribution.Info(I - 1).PdfInfo.Generate And Distribution.Info(I - 1).PdfInfo.Valid Then dgvDataTable.Columns("PDF_" & I).DefaultCellStyle.Format = Distribution.Info(I - 1).PdfInfo.Format
                                    If Distribution.Info(I - 1).PdfLnInfo.Generate And Distribution.Info(I - 1).PdfLnInfo.Valid Then dgvDataTable.Columns("PDFLn_" & I).DefaultCellStyle.Format = Distribution.Info(I - 1).PdfLnInfo.Format
                                    If Distribution.Info(I - 1).CdfInfo.Generate And Distribution.Info(I - 1).CdfInfo.Valid Then dgvDataTable.Columns("CDF_" & I).DefaultCellStyle.Format = Distribution.Info(I - 1).CdfInfo.Format
                                    If Distribution.Info(I - 1).RevCdfInfo.Generate And Distribution.Info(I - 1).RevCdfInfo.Valid Then dgvDataTable.Columns("RevCDF_" & I).DefaultCellStyle.Format = Distribution.Info(I - 1).RevCdfInfo.Format
                                    If Distribution.Info(I - 1).InvCdfInfo.Generate And Distribution.Info(I - 1).InvCdfInfo.Valid Then dgvDataTable.Columns("InvCDF_" & I).DefaultCellStyle.Format = Distribution.Info(I - 1).InvCdfInfo.Format
                                    If Distribution.Info(I - 1).InvRevCdfInfo.Generate And Distribution.Info(I - 1).InvRevCdfInfo.Valid Then dgvDataTable.Columns("InvRevCDF_" & I).DefaultCellStyle.Format = Distribution.Info(I - 1).InvRevCdfInfo.Format

                                    'Apply the cell alignments:
                                    If Distribution.Info(I - 1).PdfInfo.Generate And Distribution.Info(I - 1).PdfInfo.Valid Then dgvDataTable.Columns("PDF_" & I).DefaultCellStyle.Alignment = DirectCast([Enum].Parse(GetType(DataGridViewContentAlignment), Distribution.Info(I - 1).PdfInfo.Alignment), DataGridViewContentAlignment)
                                    If Distribution.Info(I - 1).PdfLnInfo.Generate And Distribution.Info(I - 1).PdfLnInfo.Valid Then dgvDataTable.Columns("PDFLn_" & I).DefaultCellStyle.Alignment = DirectCast([Enum].Parse(GetType(DataGridViewContentAlignment), Distribution.Info(I - 1).PdfLnInfo.Alignment), DataGridViewContentAlignment)
                                    If Distribution.Info(I - 1).CdfInfo.Generate And Distribution.Info(I - 1).CdfInfo.Valid Then dgvDataTable.Columns("CDF_" & I).DefaultCellStyle.Alignment = DirectCast([Enum].Parse(GetType(DataGridViewContentAlignment), Distribution.Info(I - 1).CdfInfo.Alignment), DataGridViewContentAlignment)
                                    If Distribution.Info(I - 1).RevCdfInfo.Generate And Distribution.Info(I - 1).RevCdfInfo.Valid Then dgvDataTable.Columns("RevCDF_" & I).DefaultCellStyle.Alignment = DirectCast([Enum].Parse(GetType(DataGridViewContentAlignment), Distribution.Info(I - 1).RevCdfInfo.Alignment), DataGridViewContentAlignment)
                                    If Distribution.Info(I - 1).InvCdfInfo.Generate And Distribution.Info(I - 1).InvCdfInfo.Valid Then dgvDataTable.Columns("InvCDF_" & I).DefaultCellStyle.Alignment = DirectCast([Enum].Parse(GetType(DataGridViewContentAlignment), Distribution.Info(I - 1).InvCdfInfo.Alignment), DataGridViewContentAlignment)
                                    If Distribution.Info(I - 1).InvRevCdfInfo.Generate And Distribution.Info(I - 1).InvRevCdfInfo.Valid Then dgvDataTable.Columns("InvRevCDF_" & I).DefaultCellStyle.Alignment = DirectCast([Enum].Parse(GetType(DataGridViewContentAlignment), Distribution.Info(I - 1).InvRevCdfInfo.Alignment), DataGridViewContentAlignment)
                                End If
                            Catch ex As Exception
                                Message.AddWarning("Error updating table: Discrete_Data_Table " & vbCrLf & ex.Message & vbCrLf)
                            End Try
                        End If
                    Next
                End If
            Else
                Message.AddWarning("The table was not found: " & SelTableName & vbCrLf)
                dgvDataTable.DataSource = vbNull
            End If
        End If

        dgvDataTable.AutoResizeColumns()
        dgvDataTable.Update()
        dgvDataTable.Refresh()
    End Sub

    Public Sub GenerateData(DistribNo As Integer)
        'Generate the data for the selected Distribution Number.


    End Sub

    Private Sub dgvFields_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvFields.CellEndEdit
        'Field information has been changed
        'Name | Valid | Generate | Number Type | Format | Alignment | Units | Label | Description

        'Message.Add("Row = " & e.RowIndex & "  Col = " & e.ColumnIndex & vbCrLf)
        'Dim ColName As String = dgvFields.Rows(e.RowIndex).Cells(0).Value
        Dim FieldName As String = dgvFields.Rows(e.RowIndex).Cells(0).Value

        If e.ColumnIndex = 2 Then 'Generate has been changed
            Dim Generate As String = dgvFields.Rows(e.RowIndex).Cells(2).Value
            Select Case FieldName
                'Case "Value"
                '    If Distribution.Info(SelDistrib - 1).ValueInfo.Generate = Generate Then
                '        'The Generate value has not been changed.
                '    Else
                '        Distribution.Info(SelDistrib - 1).ValueInfo.Generate = Generate
                '        GenerateData()
                '    End If
                'Case "PDF"
                Case "PDF_" & SelDistrib
                    If Distribution.Info(SelDistrib - 1).PdfInfo.Generate = Generate Then
                        'The Generate value has not been changed.
                    Else
                        Distribution.Info(SelDistrib - 1).PdfInfo.Generate = Generate
                        GenerateData()
                    End If
                Case "PDFLn_" & SelDistrib
                    If Distribution.Info(SelDistrib - 1).PdfLnInfo.Generate = Generate Then
                        'The Generate value has not been changed.
                    Else
                        Distribution.Info(SelDistrib - 1).PdfLnInfo.Generate = Generate
                        GenerateData()
                    End If
                Case "CDF_" & SelDistrib
                    If Distribution.Info(SelDistrib - 1).CdfInfo.Generate = Generate Then
                        'The Generate value has not been changed.
                    Else
                        Distribution.Info(SelDistrib - 1).CdfInfo.Generate = Generate
                        GenerateData()
                    End If

                Case "RevCDF_" & SelDistrib
                    If Distribution.Info(SelDistrib - 1).RevCdfInfo.Generate = Generate Then
                        'The Generate value has not been changed.
                    Else
                        Distribution.Info(SelDistrib - 1).RevCdfInfo.Generate = Generate
                        GenerateData()
                    End If

                'Case "Probability"
                '    If Distribution.Info(SelDistrib - 1).ProbabilityInfo.Generate = Generate Then
                '        'The Generate value has not been changed.
                '    Else
                '        Distribution.Info(SelDistrib - 1).ProbabilityInfo.Generate = Generate
                '        GenerateData()
                '    End If
                Case "InvCDF_" & SelDistrib
                    If Distribution.Info(SelDistrib - 1).InvCdfInfo.Generate = Generate Then
                        'The Generate value has not been changed.
                    Else
                        Distribution.Info(SelDistrib - 1).InvCdfInfo.Generate = Generate
                        GenerateData()
                    End If

                Case "InvRevCDF_" & SelDistrib
                    If Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Generate = Generate Then
                        'The Generate value has not been changed.
                    Else
                        Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Generate = Generate
                        GenerateData()
                    End If

                Case "PMF_" & SelDistrib
                    If Distribution.Info(SelDistrib - 1).PmfInfo.Generate = Generate Then
                        'The Generate value has not been changed.
                    Else
                        Distribution.Info(SelDistrib - 1).PmfInfo.Generate = Generate
                        GenerateData()
                    End If
                Case "PMFLn_" & SelDistrib
                    If Distribution.Info(SelDistrib - 1).PmfLnInfo.Generate = Generate Then
                        'The Generate value has not been changed.
                    Else
                        Distribution.Info(SelDistrib - 1).PmfLnInfo.Generate = Generate
                        GenerateData()
                    End If
                Case Else
                    Message.AddWarning("Unknown Field name: " & FieldName & vbCrLf)
            End Select

        ElseIf e.ColumnIndex = 4 Then 'The number format has been changed
            Dim Format As String = dgvFields.Rows(e.RowIndex).Cells(4).Value
            If dgvFields.Rows(e.RowIndex).Cells(2).Value = "True" Then 'The Field is generated.
                If dgvDataTable.Columns.Contains(FieldName) Then
                    'dgvDataTable.Columns(ColName).DefaultCellStyle.Format = dgvFields.Rows(e.RowIndex).Cells(4).Value
                    dgvDataTable.Columns(FieldName).DefaultCellStyle.Format = Format
                End If
            End If
            Select Case FieldName
                'Case "Value"
                '    Distribution.Info(SelDistrib - 1).ValueInfo.Format = Format
                'Case "PDF"
                Case "PDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).PdfInfo.Format = Format
                Case "PDFLn_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).PdfLnInfo.Format = Format
                Case "CDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).CdfInfo.Format = Format
                Case "RevCDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).RevCdfInfo.Format = Format
                'Case "Probability"
                '    Distribution.Info(SelDistrib - 1).ProbabilityInfo.Format = Format
                Case "InvCDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).InvCdfInfo.Format = Format
                Case "InvRevCDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Format = Format
                Case "PMF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).PmfInfo.Format = Format
                Case "PMFLn_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).PmfLnInfo.Format = Format
                Case Else
                    Message.AddWarning("Unknown Field name: " & FieldName & vbCrLf)
            End Select
        ElseIf e.ColumnIndex = 5 Then 'The Alignment has been changed
            'Dim Alignment As String = dgvFields.Rows(e.RowIndex).Cells(5).Value
            'Dim Alignment As DataGridViewContentAlignment = dgvFields.Rows(e.RowIndex).Cells(5).Value
            Dim Alignment As DataGridViewContentAlignment = DirectCast([Enum].Parse(GetType(DataGridViewContentAlignment), dgvFields.Rows(e.RowIndex).Cells(5).Value), DataGridViewContentAlignment)
            If dgvFields.Rows(e.RowIndex).Cells(2).Value = "True" Then 'The Field is generated.
                dgvDataTable.Columns(FieldName).DefaultCellStyle.Alignment = Alignment
            End If
            Select Case FieldName
                'Case "Value"
                '    Distribution.Info(SelDistrib - 1).ValueInfo.Alignment = Alignment.ToString
                'Case "PDF"
                Case "PDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).PdfInfo.Alignment = Alignment.ToString
                Case "PDFLn_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).PdfLnInfo.Alignment = Alignment.ToString
                Case "CDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).CdfInfo.Alignment = Alignment.ToString
                Case "RevCDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).RevCdfInfo.Alignment = Alignment.ToString
                'Case "Probability"
                '    Distribution.Info(SelDistrib - 1).ProbabilityInfo.Alignment = Alignment.ToString
                Case "InvCDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).InvCdfInfo.Alignment = Alignment.ToString
                Case "InvRevCDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Alignment = Alignment.ToString
                Case "PMF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).PmfInfo.Alignment = Alignment.ToString
                Case "PMFLn_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).PmfLnInfo.Alignment = Alignment.ToString
                Case Else
                    Message.AddWarning("Unknown Field name: " & FieldName & vbCrLf)
            End Select
        ElseIf e.ColumnIndex = 9 Then 'The Series Label has been changed
            Dim SeriesLabel As String = dgvFields.Rows(e.RowIndex).Cells(9).Value
            Select Case FieldName
                Case "PDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).PdfInfo.SeriesLabel = SeriesLabel
                Case "PDFLn_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).PdfLnInfo.SeriesLabel = SeriesLabel
                Case "CDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).CdfInfo.SeriesLabel = SeriesLabel
                Case "RevCDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).RevCdfInfo.SeriesLabel = SeriesLabel
                Case "InvCDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).InvCdfInfo.SeriesLabel = SeriesLabel
                Case "InvRevCDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).InvRevCdfInfo.SeriesLabel = SeriesLabel
                Case "PMF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).PmfInfo.SeriesLabel = SeriesLabel
                Case "PMFLn_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).PmfLnInfo.SeriesLabel = SeriesLabel
                Case Else
                    Message.AddWarning("Unknown Field name: " & FieldName & vbCrLf)
            End Select

        ElseIf e.ColumnIndex = 11 Then 'The Marker Fill has been changed
            Dim MarkerFill As String = dgvFields.Rows(e.RowIndex).Cells(11).Value
            Select Case FieldName
                'Case "Value"
                '    Distribution.Info(SelDistrib - 1).ValueInfo.Display.MarkerFill = MarkerFill
                'Case "PDF"
                Case "PDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).PdfInfo.Display.MarkerFill = MarkerFill
                Case "PDFLn_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).PdfLnInfo.Display.MarkerFill = MarkerFill
                Case "CDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).CdfInfo.Display.MarkerFill = MarkerFill
                Case "RevCDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.MarkerFill = MarkerFill
                'Case "Probability"
                '    Distribution.Info(SelDistrib - 1).ProbabilityInfo.Display.MarkerFill = MarkerFill
                Case "InvCDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerFill = MarkerFill
                Case "InvRevCDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.MarkerFill = MarkerFill
                Case "PMF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).PmfInfo.Display.MarkerFill = MarkerFill
                Case "PMFLn_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).PmfLnInfo.Display.MarkerFill = MarkerFill
                Case Else
                    Message.AddWarning("Unknown Field name: " & FieldName & vbCrLf)
            End Select
        ElseIf e.ColumnIndex = 14 Then 'The Marker Border Width has been changed
            Dim BorderWidth As Integer = dgvFields.Rows(e.RowIndex).Cells(14).Value
            Select Case FieldName
                'Case "Value"
                '    Distribution.Info(SelDistrib - 1).ValueInfo.Display.BorderWidth = BorderWidth
                'Case "PDF"
                Case "PDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).PdfInfo.Display.BorderWidth = BorderWidth
                Case "PDFLn_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).PdfLnInfo.Display.BorderWidth = BorderWidth
                Case "CDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).CdfInfo.Display.BorderWidth = BorderWidth
                Case "RevCDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.BorderWidth = BorderWidth
                Case "RevCDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.BorderWidth = BorderWidth
                'Case "Probability"
                '    Distribution.Info(SelDistrib - 1).ProbabilityInfo.Display.BorderWidth = BorderWidth
                Case "InvCDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.BorderWidth = BorderWidth
                Case "InvRevCDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.BorderWidth = BorderWidth
                Case "PMF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).PmfInfo.Display.BorderWidth = BorderWidth
                Case "PMFLn_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).PmfLnInfo.Display.BorderWidth = BorderWidth
                Case Else
                    Message.AddWarning("Unknown Field name: " & FieldName & vbCrLf)
            End Select

        ElseIf e.ColumnIndex = 15 Then 'The Marker Style has been changed
            Dim MarkerStyle As String = dgvFields.Rows(e.RowIndex).Cells(11).Value
            Select Case FieldName
                'Case "Value"
                '    Distribution.Info(SelDistrib - 1).ValueInfo.Display.MarkerStyle = MarkerStyle
                'Case "PDF"
                Case "PDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).PdfInfo.Display.MarkerStyle = MarkerStyle
                Case "PDFLn_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).PdfLnInfo.Display.MarkerStyle = MarkerStyle
                Case "CDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).CdfInfo.Display.MarkerStyle = MarkerStyle
                Case "RevCDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.MarkerStyle = MarkerStyle
                'Case "Probability"
                '    Distribution.Info(SelDistrib - 1).ProbabilityInfo.Display.MarkerStyle = MarkerStyle
                Case "InvCDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerStyle = MarkerStyle
                Case "InvRevCDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.MarkerStyle = MarkerStyle
                Case "PMF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).PmfInfo.Display.MarkerStyle = MarkerStyle
                Case "PMFLn_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).PmfLnInfo.Display.MarkerStyle = MarkerStyle
                Case Else
                    Message.AddWarning("Unknown Field name: " & FieldName & vbCrLf)
            End Select

        ElseIf e.ColumnIndex = 16 Then 'The Marker Size has been changed
            Dim MarkerSize As Integer = dgvFields.Rows(e.RowIndex).Cells(14).Value
            Select Case FieldName
                'Case "Value"
                '    Distribution.Info(SelDistrib - 1).ValueInfo.Display.MarkerSize = MarkerSize
                Case "PDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).PdfInfo.Display.MarkerSize = MarkerSize
                Case "PDFLn_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).PdfLnInfo.Display.MarkerSize = MarkerSize
                Case "CDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).CdfInfo.Display.MarkerSize = MarkerSize
                Case "RevCDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.MarkerSize = MarkerSize
                Case "InvCDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerSize = MarkerSize
                'Case "Probability"
                '    Distribution.Info(SelDistrib - 1).ProbabilityInfo.Display.MarkerSize = MarkerSize
                Case "InvCDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerSize = MarkerSize
                Case "InvRevCDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.MarkerSize = MarkerSize
                Case "PMF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).PmfInfo.Display.MarkerSize = MarkerSize
                Case "PMFLn_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).PmfLnInfo.Display.MarkerSize = MarkerSize
                Case Else
                    Message.AddWarning("Unknown Field name: " & FieldName & vbCrLf)
            End Select

        ElseIf e.ColumnIndex = 17 Then 'The Marker Step has been changed
            Dim MarkerStep As Integer = dgvFields.Rows(e.RowIndex).Cells(14).Value
            Select Case FieldName
                'Case "Value"
                '    Distribution.Info(SelDistrib - 1).ValueInfo.Display.MarkerStep = MarkerStep
                Case "PDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).PdfInfo.Display.MarkerStep = MarkerStep
                Case "PDFLn_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).PdfLnInfo.Display.MarkerSize = MarkerStep
                Case "CDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).CdfInfo.Display.MarkerStep = MarkerStep
                Case "RevCDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.MarkerStep = MarkerStep
                'Case "Probability"
                '    Distribution.Info(SelDistrib - 1).ProbabilityInfo.Display.MarkerStep = MarkerStep
                Case "InvCDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerStep = MarkerStep
                Case "InvRevCDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.MarkerStep = MarkerStep
                Case "PMF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).PmfInfo.Display.MarkerStep = MarkerStep
                Case "PMFLn_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).PmfLnInfo.Display.MarkerStep = MarkerStep
                Case Else
                    Message.AddWarning("Unknown Field name: " & FieldName & vbCrLf)
            End Select

        ElseIf e.ColumnIndex = 19 Then 'The Marker Line Width has been changed
            Dim LineWidth As Integer = dgvFields.Rows(e.RowIndex).Cells(14).Value
            Select Case FieldName
                'Case "Value"
                '    Distribution.Info(SelDistrib - 1).ValueInfo.Display.LineWidth = LineWidth
                Case "PDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).PdfInfo.Display.LineWidth = LineWidth
                Case "PDFLn_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).PdfLnInfo.Display.LineWidth = LineWidth
                Case "CDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).CdfInfo.Display.LineWidth = LineWidth
                Case "RevCDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.LineWidth = LineWidth
                'Case "Probability"
                '    Distribution.Info(SelDistrib - 1).ProbabilityInfo.Display.LineWidth = LineWidth
                Case "InvCDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.LineWidth = LineWidth
                Case "InvRevCDF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.LineWidth = LineWidth
                Case "PMF_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).PmfInfo.Display.LineWidth = LineWidth
                Case "PMFLn_" & SelDistrib
                    Distribution.Info(SelDistrib - 1).PmfLnInfo.Display.LineWidth = LineWidth
                Case Else
                    Message.AddWarning("Unknown Field name: " & FieldName & vbCrLf)
            End Select

        End If


    End Sub

    Private Sub btnFormatHelp_Click(sender As Object, e As EventArgs) Handles btnFormatHelp.Click
        'Show Format information.
        MessageBox.Show("Format string examples:" & vbCrLf & "N4 - Number displayed with thousands separator and 4 decimal places" & vbCrLf & "F4 - Number displayed with 4 decimal places.", "Number Formatting")
    End Sub

    Private Sub Distribution_Warning(Msg As String) Handles Distribution.ErrorMessage
        Message.AddWarning(Msg)
    End Sub

    Private Sub Distribution_Message(Msg As String) Handles Distribution.Message
        Message.Add(Msg)
    End Sub

    Private Sub txtRVName_TextChanged(sender As Object, e As EventArgs) Handles txtRVName.TextChanged

    End Sub

    Private Sub txtRVName_LostFocus(sender As Object, e As EventArgs) Handles txtRVName.LostFocus
        Distribution.Info(SelDistrib - 1).RVName = txtRVName.Text.Trim
    End Sub

    Private Sub txtRVDescr_TextChanged(sender As Object, e As EventArgs) Handles txtRVDescr.TextChanged

    End Sub

    Private Sub txtRVDescr_LostFocus(sender As Object, e As EventArgs) Handles txtRVDescr.LostFocus
        Distribution.Info(SelDistrib - 1).RVDescription = txtRVDescr.Text.Trim
    End Sub

    Private Sub txtRVMeasurement_TextChanged(sender As Object, e As EventArgs) Handles txtRVMeasurement.TextChanged

    End Sub

    Private Sub txtRVMeasurement_LostFocus(sender As Object, e As EventArgs) Handles txtRVMeasurement.LostFocus
        Distribution.Info(SelDistrib - 1).RVMeasurement = txtRVMeasurement.Text.Trim
        If SelDistrib = 1 Then
            'Update the Axis Value label
            Distribution.XValueInfo.ValueLabel = txtRVMeasurement.Text.Trim
            Distribution.ContSampling.Label = txtRVMeasurement.Text.Trim
            txtXAxisLabel.Text = txtRVMeasurement.Text.Trim
        End If
    End Sub

    Private Sub txtRVUnits_TextChanged(sender As Object, e As EventArgs) Handles txtRVUnits.TextChanged

    End Sub

    Private Sub txtRVUnits_LostFocus(sender As Object, e As EventArgs) Handles txtRVUnits.LostFocus
        Distribution.Info(SelDistrib - 1).RVUnits = txtRVUnits.Text.Trim

    End Sub

    Private Sub txtRVUnitsAbbrev_TextChanged(sender As Object, e As EventArgs) Handles txtRVUnitsAbbrev.TextChanged

    End Sub

    Private Sub txtRVUnitsAbbrev_LostFocus(sender As Object, e As EventArgs) Handles txtRVUnitsAbbrev.LostFocus
        Distribution.Info(SelDistrib - 1).RVAbbrevUnits = txtRVUnitsAbbrev.Text.Trim
        If SelDistrib = 1 Then
            'Update the Axis Units
            Distribution.XValueInfo.Units = txtRVUnitsAbbrev.Text.Trim
            Distribution.ContSampling.Units = txtRVUnitsAbbrev.Text.Trim
            txtXAxisUnits.Text = txtRVUnitsAbbrev.Text.Trim
        End If
    End Sub

    Private Sub dgvParams_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvParams.CellContentClick

    End Sub

    Private Sub dgvParams_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvParams.CellEndEdit
        'A distribution parameter has been changed.

        Dim RowNo As Integer = e.RowIndex
        Dim ColNo As Integer = e.ColumnIndex

        'If ColNo = 1 Then 'A distribution parameter has been changed.
        If ColNo = 2 Then 'A distribution parameter has been changed.
            Select Case RowNo
                Case 0
                    Distribution.Info(SelDistrib - 1).ParamA.Value = dgvParams.Rows(RowNo).Cells(2).Value
                    UpdateSuffix()
                    Distribution.UpdateData(SelDistrib)
                    UpdateAnnotationSettings(SelDistrib)
                    ReplotCharts()
                Case 1
                    Distribution.Info(SelDistrib - 1).ParamB.Value = dgvParams.Rows(RowNo).Cells(2).Value
                    UpdateSuffix()
                    Distribution.UpdateData(SelDistrib)
                    UpdateAnnotationSettings(SelDistrib)
                    ReplotCharts()
                Case 2
                    Distribution.Info(SelDistrib - 1).ParamC.Value = dgvParams.Rows(RowNo).Cells(2).Value
                    UpdateSuffix()
                    Distribution.UpdateData(SelDistrib)
                    UpdateAnnotationSettings(SelDistrib)
                    ReplotCharts()
                Case 3
                    Distribution.Info(SelDistrib - 1).ParamD.Value = dgvParams.Rows(RowNo).Cells(2).Value
                    UpdateSuffix()
                    Distribution.UpdateData(SelDistrib)
                    UpdateAnnotationSettings(SelDistrib)
                    ReplotCharts()
                Case 4
                    Distribution.Info(SelDistrib - 1).ParamE.Value = dgvParams.Rows(RowNo).Cells(2).Value
                    UpdateSuffix()
                    Distribution.UpdateData(SelDistrib)
                    UpdateAnnotationSettings(SelDistrib)
                    ReplotCharts()
                Case Else
                    Message.AddWarning("Unknown parameter number: " & RowNo & vbCrLf)
            End Select

        ElseIf ColNo = 7 Then 'The parameter description has been changed.
            Select Case RowNo
                Case 0
                    Distribution.Info(SelDistrib - 1).ParamA.Description = dgvParams.Rows(RowNo).Cells(7).Value
                    'UpdateSuffix()
                Case 1
                    Distribution.Info(SelDistrib - 1).ParamB.Description = dgvParams.Rows(RowNo).Cells(7).Value
                    'UpdateSuffix()
                Case 2
                    Distribution.Info(SelDistrib - 1).ParamC.Description = dgvParams.Rows(RowNo).Cells(7).Value
                    'UpdateSuffix()
                Case 3
                    Distribution.Info(SelDistrib - 1).ParamD.Description = dgvParams.Rows(RowNo).Cells(7).Value
                    'UpdateSuffix()
                Case 4
                    Distribution.Info(SelDistrib - 1).ParamE.Description = dgvParams.Rows(RowNo).Cells(7).Value
                    'UpdateSuffix()
                Case Else
                    Message.AddWarning("Unknown parameter number: " & RowNo & vbCrLf)
            End Select
        End If
    End Sub

    Private Sub dgvXFields_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvXFields.CellContentClick

    End Sub

    Private Sub dgvXFields_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvXFields.CellEndEdit
        'An X Axis field setting has been changed.

        Dim RowNo As Integer = e.RowIndex
        Dim ColNo As Integer = e.ColumnIndex

        If ColNo = 2 Then
            'The Format has changed
            If RowNo = 0 Then
                Distribution.XValueInfo.Format = dgvXFields.Rows(0).Cells(2).Value
            ElseIf RowNo = 1 Then
                Distribution.XProbInfo.Format = dgvXFields.Rows(1).Cells(2).Value
            End If

        ElseIf ColNo = 3 Then
            'The Alignment has changed
            Dim Alignment As DataGridViewContentAlignment = DirectCast([Enum].Parse(GetType(DataGridViewContentAlignment), dgvXFields.Rows(e.RowIndex).Cells(3).Value), DataGridViewContentAlignment)
            If RowNo = 0 Then
                Distribution.XValueInfo.Alignment = Alignment.ToString
            ElseIf RowNo = 1 Then
                Distribution.XProbInfo.Alignment = Alignment.ToString
            End If

        ElseIf ColNo = 4 Then
            'The Value Label has changed
            If RowNo = 0 Then
                Distribution.XValueInfo.ValueLabel = dgvXFields.Rows(0).Cells(4).Value
            ElseIf RowNo = 1 Then
                Distribution.XProbInfo.ValueLabel = dgvXFields.Rows(1).Cells(4).Value
            End If

        ElseIf ColNo = 5 Then
            'The Units have changed
            If RowNo = 0 Then
                Distribution.XValueInfo.Units = dgvXFields.Rows(0).Cells(5).Value
            ElseIf RowNo = 1 Then
                Distribution.XProbInfo.Units = dgvXFields.Rows(1).Cells(5).Value
            End If

        ElseIf ColNo = 6 Then
            'The Description has changed
            If RowNo = 0 Then
                Distribution.XValueInfo.Description = dgvXFields.Rows(0).Cells(6).Value
            ElseIf RowNo = 1 Then
                Distribution.XProbInfo.Description = dgvXFields.Rows(1).Cells(6).Value
            End If

        End If

    End Sub

    'Private Sub UpdateSuffix()
    Public Sub UpdateSuffix()
        'Update the series name using the parameter values.

        Distribution.Info(SelDistrib - 1).UpdateSuffix()
        txtSuffix.Text = Distribution.Info(SelDistrib - 1).Suffix

        If chkUpdateLabel.Checked Then

            Distribution.Info(SelDistrib - 1).UpdateSeriesLabels()

            'Update the Fields tab: ------------------------------------------------------------------
            dgvFields.Rows.Clear()
            If Distribution.Info(SelDistrib - 1).Continuity = "Continuous" Then
                'dgvFields.Rows.Add(Distribution.Info(SelDistrib - 1).ValueInfo.Name, Distribution.Info(SelDistrib - 1).ValueInfo.Valid, Distribution.Info(SelDistrib - 1).ValueInfo.Generate.ToString, Distribution.Info(SelDistrib - 1).ValueInfo.NumType, Distribution.Info(SelDistrib - 1).ValueInfo.Format, Distribution.Info(SelDistrib - 1).ValueInfo.Alignment, Distribution.Info(SelDistrib - 1).ValueInfo.ValueLabel, Distribution.Info(SelDistrib - 1).ValueInfo.Units, Distribution.Info(SelDistrib - 1).ValueInfo.LabelPrefix, Distribution.Info(SelDistrib - 1).ValueInfo.SeriesLabel, Distribution.Info(SelDistrib - 1).ValueInfo.Description)
                dgvFields.Rows.Add(Distribution.Info(SelDistrib - 1).PdfInfo.Name, Distribution.Info(SelDistrib - 1).PdfInfo.Valid, Distribution.Info(SelDistrib - 1).PdfInfo.Generate.ToString, Distribution.Info(SelDistrib - 1).PdfInfo.NumType, Distribution.Info(SelDistrib - 1).PdfInfo.Format, Distribution.Info(SelDistrib - 1).PdfInfo.Alignment, Distribution.Info(SelDistrib - 1).PdfInfo.ValueLabel, Distribution.Info(SelDistrib - 1).PdfInfo.Units, Distribution.Info(SelDistrib - 1).PdfInfo.LabelPrefix, Distribution.Info(SelDistrib - 1).PdfInfo.SeriesLabel, Distribution.Info(SelDistrib - 1).PdfInfo.Description)
                dgvFields.Rows.Add(Distribution.Info(SelDistrib - 1).PdfLnInfo.Name, Distribution.Info(SelDistrib - 1).PdfLnInfo.Valid, Distribution.Info(SelDistrib - 1).PdfLnInfo.Generate.ToString, Distribution.Info(SelDistrib - 1).PdfLnInfo.NumType, Distribution.Info(SelDistrib - 1).PdfLnInfo.Format, Distribution.Info(SelDistrib - 1).PdfLnInfo.Alignment, Distribution.Info(SelDistrib - 1).PdfLnInfo.ValueLabel, Distribution.Info(SelDistrib - 1).PdfLnInfo.Units, Distribution.Info(SelDistrib - 1).PdfLnInfo.LabelPrefix, Distribution.Info(SelDistrib - 1).PdfLnInfo.SeriesLabel, Distribution.Info(SelDistrib - 1).PdfLnInfo.Description)
                dgvFields.Rows.Add(Distribution.Info(SelDistrib - 1).CdfInfo.Name, Distribution.Info(SelDistrib - 1).CdfInfo.Valid, Distribution.Info(SelDistrib - 1).CdfInfo.Generate.ToString, Distribution.Info(SelDistrib - 1).CdfInfo.NumType, Distribution.Info(SelDistrib - 1).CdfInfo.Format, Distribution.Info(SelDistrib - 1).CdfInfo.Alignment, Distribution.Info(SelDistrib - 1).CdfInfo.ValueLabel, Distribution.Info(SelDistrib - 1).CdfInfo.Units, Distribution.Info(SelDistrib - 1).CdfInfo.LabelPrefix, Distribution.Info(SelDistrib - 1).CdfInfo.SeriesLabel, Distribution.Info(SelDistrib - 1).CdfInfo.Description)

                dgvFields.Rows.Add(Distribution.Info(SelDistrib - 1).RevCdfInfo.Name, Distribution.Info(SelDistrib - 1).RevCdfInfo.Valid, Distribution.Info(SelDistrib - 1).RevCdfInfo.Generate.ToString, Distribution.Info(SelDistrib - 1).RevCdfInfo.NumType, Distribution.Info(SelDistrib - 1).RevCdfInfo.Format, Distribution.Info(SelDistrib - 1).RevCdfInfo.Alignment, Distribution.Info(SelDistrib - 1).RevCdfInfo.ValueLabel, Distribution.Info(SelDistrib - 1).RevCdfInfo.Units, Distribution.Info(SelDistrib - 1).RevCdfInfo.LabelPrefix, Distribution.Info(SelDistrib - 1).RevCdfInfo.SeriesLabel, Distribution.Info(SelDistrib - 1).RevCdfInfo.Description)

                'dgvFields.Rows.Add(Distribution.Info(SelDistrib - 1).ProbabilityInfo.Name, Distribution.Info(SelDistrib - 1).ProbabilityInfo.Valid, Distribution.Info(SelDistrib - 1).ProbabilityInfo.Generate.ToString, Distribution.Info(SelDistrib - 1).ProbabilityInfo.NumType, Distribution.Info(SelDistrib - 1).ProbabilityInfo.Format, Distribution.Info(SelDistrib - 1).ProbabilityInfo.Alignment, Distribution.Info(SelDistrib - 1).ProbabilityInfo.ValueLabel, Distribution.Info(SelDistrib - 1).ProbabilityInfo.Units, Distribution.Info(SelDistrib - 1).ProbabilityInfo.LabelPrefix, Distribution.Info(SelDistrib - 1).ProbabilityInfo.SeriesLabel, Distribution.Info(SelDistrib - 1).ProbabilityInfo.Description)
                dgvFields.Rows.Add(Distribution.Info(SelDistrib - 1).InvCdfInfo.Name, Distribution.Info(SelDistrib - 1).InvCdfInfo.Valid, Distribution.Info(SelDistrib - 1).InvCdfInfo.Generate.ToString, Distribution.Info(SelDistrib - 1).InvCdfInfo.NumType, Distribution.Info(SelDistrib - 1).InvCdfInfo.Format, Distribution.Info(SelDistrib - 1).InvCdfInfo.Alignment, Distribution.Info(SelDistrib - 1).InvCdfInfo.ValueLabel, Distribution.Info(SelDistrib - 1).InvCdfInfo.Units, Distribution.Info(SelDistrib - 1).InvCdfInfo.LabelPrefix, Distribution.Info(SelDistrib - 1).InvCdfInfo.SeriesLabel, Distribution.Info(SelDistrib - 1).InvCdfInfo.Description)

                dgvFields.Rows.Add(Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Name, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Valid, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Generate.ToString, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.NumType, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Format, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Alignment, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.ValueLabel, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Units, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.LabelPrefix, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.SeriesLabel, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Description)

                'Add PDF display settings:
                'dgvFields.Rows(1).Cells(11).Value = Distribution.Info(SelDistrib - 1).PdfInfo.Display.MarkerFill
                dgvFields.Rows(0).Cells(11).Value = Distribution.Info(SelDistrib - 1).PdfInfo.Display.MarkerFill
                dgvFields.Rows(0).Cells(12).Style.BackColor = Distribution.Info(SelDistrib - 1).PdfInfo.Display.MarkerColor
                dgvFields.Rows(0).Cells(13).Style.BackColor = Distribution.Info(SelDistrib - 1).PdfInfo.Display.BorderColor
                dgvFields.Rows(0).Cells(14).Value = Distribution.Info(SelDistrib - 1).PdfInfo.Display.BorderWidth
                dgvFields.Rows(0).Cells(15).Value = Distribution.Info(SelDistrib - 1).PdfInfo.Display.MarkerStyle
                dgvFields.Rows(0).Cells(16).Value = Distribution.Info(SelDistrib - 1).PdfInfo.Display.MarkerSize
                dgvFields.Rows(0).Cells(17).Value = Distribution.Info(SelDistrib - 1).PdfInfo.Display.MarkerStep
                dgvFields.Rows(0).Cells(18).Style.BackColor = Distribution.Info(SelDistrib - 1).PdfInfo.Display.LineColor
                dgvFields.Rows(0).Cells(19).Value = Distribution.Info(SelDistrib - 1).PdfInfo.Display.LineWidth

                'Add PDFLn display settings:
                dgvFields.Rows(1).Cells(11).Value = Distribution.Info(SelDistrib - 1).PdfLnInfo.Display.MarkerFill
                dgvFields.Rows(1).Cells(12).Style.BackColor = Distribution.Info(SelDistrib - 1).PdfLnInfo.Display.MarkerColor
                dgvFields.Rows(1).Cells(13).Style.BackColor = Distribution.Info(SelDistrib - 1).PdfLnInfo.Display.BorderColor
                dgvFields.Rows(1).Cells(14).Value = Distribution.Info(SelDistrib - 1).PdfLnInfo.Display.BorderWidth
                dgvFields.Rows(1).Cells(15).Value = Distribution.Info(SelDistrib - 1).PdfLnInfo.Display.MarkerStyle
                dgvFields.Rows(1).Cells(16).Value = Distribution.Info(SelDistrib - 1).PdfLnInfo.Display.MarkerSize
                dgvFields.Rows(1).Cells(17).Value = Distribution.Info(SelDistrib - 1).PdfLnInfo.Display.MarkerStep
                dgvFields.Rows(1).Cells(18).Style.BackColor = Distribution.Info(SelDistrib - 1).PdfLnInfo.Display.LineColor
                dgvFields.Rows(1).Cells(19).Value = Distribution.Info(SelDistrib - 1).PdfLnInfo.Display.LineWidth

                'Add CDF display settings:
                dgvFields.Rows(2).Cells(11).Value = Distribution.Info(SelDistrib - 1).CdfInfo.Display.MarkerFill
                dgvFields.Rows(2).Cells(12).Style.BackColor = Distribution.Info(SelDistrib - 1).CdfInfo.Display.MarkerColor
                dgvFields.Rows(2).Cells(13).Style.BackColor = Distribution.Info(SelDistrib - 1).CdfInfo.Display.BorderColor
                dgvFields.Rows(2).Cells(14).Value = Distribution.Info(SelDistrib - 1).CdfInfo.Display.BorderWidth
                dgvFields.Rows(2).Cells(15).Value = Distribution.Info(SelDistrib - 1).CdfInfo.Display.MarkerStyle
                dgvFields.Rows(2).Cells(16).Value = Distribution.Info(SelDistrib - 1).CdfInfo.Display.MarkerSize
                dgvFields.Rows(2).Cells(17).Value = Distribution.Info(SelDistrib - 1).CdfInfo.Display.MarkerStep
                dgvFields.Rows(2).Cells(18).Style.BackColor = Distribution.Info(SelDistrib - 1).CdfInfo.Display.LineColor
                dgvFields.Rows(2).Cells(19).Value = Distribution.Info(SelDistrib - 1).CdfInfo.Display.LineWidth

                'Add RevCDF display settings:
                dgvFields.Rows(3).Cells(11).Value = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.MarkerFill
                dgvFields.Rows(3).Cells(12).Style.BackColor = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.MarkerColor
                dgvFields.Rows(3).Cells(13).Style.BackColor = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.BorderColor
                dgvFields.Rows(3).Cells(14).Value = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.BorderWidth
                dgvFields.Rows(3).Cells(15).Value = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.MarkerStyle
                dgvFields.Rows(3).Cells(16).Value = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.MarkerSize
                dgvFields.Rows(3).Cells(17).Value = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.MarkerStep
                dgvFields.Rows(3).Cells(18).Style.BackColor = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.LineColor
                dgvFields.Rows(3).Cells(19).Value = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.LineWidth

                ''Add InvCDF display settings:
                'dgvFields.Rows(3).Cells(11).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerFill
                'dgvFields.Rows(3).Cells(12).Style.BackColor = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerColor
                'dgvFields.Rows(3).Cells(13).Style.BackColor = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.BorderColor
                'dgvFields.Rows(3).Cells(14).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.BorderWidth
                'dgvFields.Rows(3).Cells(15).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerStyle
                'dgvFields.Rows(3).Cells(16).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerSize
                'dgvFields.Rows(3).Cells(17).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerStep
                'dgvFields.Rows(3).Cells(18).Style.BackColor = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.LineColor
                'dgvFields.Rows(3).Cells(19).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.LineWidth

                'Add InvCDF display settings:
                dgvFields.Rows(4).Cells(11).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerFill
                dgvFields.Rows(4).Cells(12).Style.BackColor = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerColor
                dgvFields.Rows(4).Cells(13).Style.BackColor = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.BorderColor
                dgvFields.Rows(4).Cells(14).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.BorderWidth
                dgvFields.Rows(4).Cells(15).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerStyle
                dgvFields.Rows(4).Cells(16).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerSize
                dgvFields.Rows(4).Cells(17).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerStep
                dgvFields.Rows(4).Cells(18).Style.BackColor = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.LineColor
                dgvFields.Rows(4).Cells(19).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.LineWidth

                'Add InvRevCDF display settings:
                dgvFields.Rows(5).Cells(11).Value = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.MarkerFill
                dgvFields.Rows(5).Cells(12).Style.BackColor = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.MarkerColor
                dgvFields.Rows(5).Cells(13).Style.BackColor = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.BorderColor
                dgvFields.Rows(5).Cells(14).Value = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.BorderWidth
                dgvFields.Rows(5).Cells(15).Value = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.MarkerStyle
                dgvFields.Rows(5).Cells(16).Value = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.MarkerSize
                dgvFields.Rows(5).Cells(17).Value = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.MarkerStep
                dgvFields.Rows(5).Cells(18).Style.BackColor = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.LineColor
                dgvFields.Rows(5).Cells(19).Value = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.LineWidth

                'dgvFields.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)
                dgvFields.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
                dgvFields.AutoResizeColumns()

            ElseIf Distribution.Info(SelDistrib - 1).Continuity = "Discrete" Then
                'dgvFields.Rows.Add(Distribution.Info(SelDistrib - 1).ValueInfo.Name, Distribution.Info(SelDistrib - 1).ValueInfo.Valid, Distribution.Info(SelDistrib - 1).ValueInfo.Generate.ToString, Distribution.Info(SelDistrib - 1).ValueInfo.NumType, Distribution.Info(SelDistrib - 1).ValueInfo.Format, Distribution.Info(SelDistrib - 1).ValueInfo.Alignment, Distribution.Info(SelDistrib - 1).ValueInfo.ValueLabel, Distribution.Info(SelDistrib - 1).ValueInfo.Units, Distribution.Info(SelDistrib - 1).ValueInfo.LabelPrefix, Distribution.Info(SelDistrib - 1).ValueInfo.SeriesLabel, Distribution.Info(SelDistrib - 1).ValueInfo.Description)
                dgvFields.Rows.Add(Distribution.Info(SelDistrib - 1).PmfInfo.Name, Distribution.Info(SelDistrib - 1).PmfInfo.Valid, Distribution.Info(SelDistrib - 1).PmfInfo.Generate.ToString, Distribution.Info(SelDistrib - 1).PmfInfo.NumType, Distribution.Info(SelDistrib - 1).PmfInfo.Format, Distribution.Info(SelDistrib - 1).PmfInfo.Alignment, Distribution.Info(SelDistrib - 1).PmfInfo.ValueLabel, Distribution.Info(SelDistrib - 1).PmfInfo.Units, Distribution.Info(SelDistrib - 1).PmfInfo.LabelPrefix, Distribution.Info(SelDistrib - 1).PmfInfo.SeriesLabel, Distribution.Info(SelDistrib - 1).PmfInfo.Description)
                dgvFields.Rows.Add(Distribution.Info(SelDistrib - 1).PmfLnInfo.Name, Distribution.Info(SelDistrib - 1).PmfLnInfo.Valid, Distribution.Info(SelDistrib - 1).PmfLnInfo.Generate.ToString, Distribution.Info(SelDistrib - 1).PmfLnInfo.NumType, Distribution.Info(SelDistrib - 1).PmfLnInfo.Format, Distribution.Info(SelDistrib - 1).PmfLnInfo.Alignment, Distribution.Info(SelDistrib - 1).PmfLnInfo.ValueLabel, Distribution.Info(SelDistrib - 1).PmfLnInfo.Units, Distribution.Info(SelDistrib - 1).PmfLnInfo.LabelPrefix, Distribution.Info(SelDistrib - 1).PmfLnInfo.SeriesLabel, Distribution.Info(SelDistrib - 1).PmfLnInfo.Description)
                dgvFields.Rows.Add(Distribution.Info(SelDistrib - 1).CdfInfo.Name, Distribution.Info(SelDistrib - 1).CdfInfo.Valid, Distribution.Info(SelDistrib - 1).CdfInfo.Generate.ToString, Distribution.Info(SelDistrib - 1).CdfInfo.NumType, Distribution.Info(SelDistrib - 1).CdfInfo.Format, Distribution.Info(SelDistrib - 1).CdfInfo.Alignment, Distribution.Info(SelDistrib - 1).CdfInfo.ValueLabel, Distribution.Info(SelDistrib - 1).CdfInfo.Units, Distribution.Info(SelDistrib - 1).CdfInfo.LabelPrefix, Distribution.Info(SelDistrib - 1).CdfInfo.SeriesLabel, Distribution.Info(SelDistrib - 1).CdfInfo.Description)

                dgvFields.Rows.Add(Distribution.Info(SelDistrib - 1).RevCdfInfo.Name, Distribution.Info(SelDistrib - 1).RevCdfInfo.Valid, Distribution.Info(SelDistrib - 1).RevCdfInfo.Generate.ToString, Distribution.Info(SelDistrib - 1).RevCdfInfo.NumType, Distribution.Info(SelDistrib - 1).RevCdfInfo.Format, Distribution.Info(SelDistrib - 1).RevCdfInfo.Alignment, Distribution.Info(SelDistrib - 1).RevCdfInfo.ValueLabel, Distribution.Info(SelDistrib - 1).RevCdfInfo.Units, Distribution.Info(SelDistrib - 1).RevCdfInfo.LabelPrefix, Distribution.Info(SelDistrib - 1).RevCdfInfo.SeriesLabel, Distribution.Info(SelDistrib - 1).RevCdfInfo.Description)

                'dgvFields.Rows.Add(Distribution.Info(SelDistrib - 1).ProbabilityInfo.Name, Distribution.Info(SelDistrib - 1).ProbabilityInfo.Valid, Distribution.Info(SelDistrib - 1).ProbabilityInfo.Generate.ToString, Distribution.Info(SelDistrib - 1).ProbabilityInfo.NumType, Distribution.Info(SelDistrib - 1).ProbabilityInfo.Format, Distribution.Info(SelDistrib - 1).ProbabilityInfo.Alignment, Distribution.Info(SelDistrib - 1).ProbabilityInfo.ValueLabel, Distribution.Info(SelDistrib - 1).ProbabilityInfo.Units, Distribution.Info(SelDistrib - 1).ProbabilityInfo.LabelPrefix, Distribution.Info(SelDistrib - 1).ProbabilityInfo.SeriesLabel, Distribution.Info(SelDistrib - 1).ProbabilityInfo.Description)
                dgvFields.Rows.Add(Distribution.Info(SelDistrib - 1).InvCdfInfo.Name, Distribution.Info(SelDistrib - 1).InvCdfInfo.Valid, Distribution.Info(SelDistrib - 1).InvCdfInfo.Generate.ToString, Distribution.Info(SelDistrib - 1).InvCdfInfo.NumType, Distribution.Info(SelDistrib - 1).InvCdfInfo.Format, Distribution.Info(SelDistrib - 1).InvCdfInfo.Alignment, Distribution.Info(SelDistrib - 1).InvCdfInfo.ValueLabel, Distribution.Info(SelDistrib - 1).InvCdfInfo.Units, Distribution.Info(SelDistrib - 1).InvCdfInfo.LabelPrefix, Distribution.Info(SelDistrib - 1).InvCdfInfo.SeriesLabel, Distribution.Info(SelDistrib - 1).InvCdfInfo.Description)

                dgvFields.Rows.Add(Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Name, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Valid, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Generate.ToString, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.NumType, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Format, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Alignment, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.ValueLabel, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Units, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.LabelPrefix, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.SeriesLabel, Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Description)


                'Add PMF display settings:
                dgvFields.Rows(0).Cells(11).Value = Distribution.Info(SelDistrib - 1).PmfInfo.Display.MarkerFill
                dgvFields.Rows(0).Cells(12).Style.BackColor = Distribution.Info(SelDistrib - 1).PmfInfo.Display.MarkerColor
                dgvFields.Rows(0).Cells(13).Style.BackColor = Distribution.Info(SelDistrib - 1).PmfInfo.Display.BorderColor
                dgvFields.Rows(0).Cells(14).Value = Distribution.Info(SelDistrib - 1).PmfInfo.Display.BorderWidth
                dgvFields.Rows(0).Cells(15).Value = Distribution.Info(SelDistrib - 1).PmfInfo.Display.MarkerStyle
                dgvFields.Rows(0).Cells(16).Value = Distribution.Info(SelDistrib - 1).PmfInfo.Display.MarkerSize
                dgvFields.Rows(0).Cells(17).Value = Distribution.Info(SelDistrib - 1).PmfInfo.Display.MarkerStep
                dgvFields.Rows(0).Cells(18).Style.BackColor = Distribution.Info(SelDistrib - 1).PmfInfo.Display.LineColor
                dgvFields.Rows(0).Cells(19).Value = Distribution.Info(SelDistrib - 1).PmfInfo.Display.LineWidth

                'Add PMFLn display settings:
                dgvFields.Rows(1).Cells(11).Value = Distribution.Info(SelDistrib - 1).PmfLnInfo.Display.MarkerFill
                dgvFields.Rows(1).Cells(12).Style.BackColor = Distribution.Info(SelDistrib - 1).PmfLnInfo.Display.MarkerColor
                dgvFields.Rows(1).Cells(13).Style.BackColor = Distribution.Info(SelDistrib - 1).PmfLnInfo.Display.BorderColor
                dgvFields.Rows(1).Cells(14).Value = Distribution.Info(SelDistrib - 1).PmfLnInfo.Display.BorderWidth
                dgvFields.Rows(1).Cells(15).Value = Distribution.Info(SelDistrib - 1).PmfLnInfo.Display.MarkerStyle
                dgvFields.Rows(1).Cells(16).Value = Distribution.Info(SelDistrib - 1).PmfLnInfo.Display.MarkerSize
                dgvFields.Rows(1).Cells(17).Value = Distribution.Info(SelDistrib - 1).PmfLnInfo.Display.MarkerStep
                dgvFields.Rows(1).Cells(18).Style.BackColor = Distribution.Info(SelDistrib - 1).PmfLnInfo.Display.LineColor
                dgvFields.Rows(1).Cells(19).Value = Distribution.Info(SelDistrib - 1).PmfLnInfo.Display.LineWidth

                'Add CDF display settings:
                dgvFields.Rows(2).Cells(11).Value = Distribution.Info(SelDistrib - 1).CdfInfo.Display.MarkerFill
                dgvFields.Rows(2).Cells(12).Style.BackColor = Distribution.Info(SelDistrib - 1).CdfInfo.Display.MarkerColor
                dgvFields.Rows(2).Cells(13).Style.BackColor = Distribution.Info(SelDistrib - 1).CdfInfo.Display.BorderColor
                dgvFields.Rows(2).Cells(14).Value = Distribution.Info(SelDistrib - 1).CdfInfo.Display.BorderWidth
                dgvFields.Rows(2).Cells(15).Value = Distribution.Info(SelDistrib - 1).CdfInfo.Display.MarkerStyle
                dgvFields.Rows(2).Cells(16).Value = Distribution.Info(SelDistrib - 1).CdfInfo.Display.MarkerSize
                dgvFields.Rows(2).Cells(17).Value = Distribution.Info(SelDistrib - 1).CdfInfo.Display.MarkerStep
                dgvFields.Rows(2).Cells(18).Style.BackColor = Distribution.Info(SelDistrib - 1).CdfInfo.Display.LineColor
                dgvFields.Rows(2).Cells(19).Value = Distribution.Info(SelDistrib - 1).CdfInfo.Display.LineWidth

                'Add RevCDF display settings:
                dgvFields.Rows(3).Cells(11).Value = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.MarkerFill
                dgvFields.Rows(3).Cells(12).Style.BackColor = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.MarkerColor
                dgvFields.Rows(3).Cells(13).Style.BackColor = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.BorderColor
                dgvFields.Rows(3).Cells(14).Value = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.BorderWidth
                dgvFields.Rows(3).Cells(15).Value = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.MarkerStyle
                dgvFields.Rows(3).Cells(16).Value = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.MarkerSize
                dgvFields.Rows(3).Cells(17).Value = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.MarkerStep
                dgvFields.Rows(3).Cells(18).Style.BackColor = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.LineColor
                dgvFields.Rows(3).Cells(19).Value = Distribution.Info(SelDistrib - 1).RevCdfInfo.Display.LineWidth

                'Add InvCDF display settings:
                dgvFields.Rows(4).Cells(11).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerFill
                dgvFields.Rows(4).Cells(12).Style.BackColor = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerColor
                dgvFields.Rows(4).Cells(13).Style.BackColor = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.BorderColor
                dgvFields.Rows(4).Cells(14).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.BorderWidth
                dgvFields.Rows(4).Cells(15).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerStyle
                dgvFields.Rows(4).Cells(16).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerSize
                dgvFields.Rows(4).Cells(17).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.MarkerStep
                dgvFields.Rows(4).Cells(18).Style.BackColor = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.LineColor
                dgvFields.Rows(4).Cells(19).Value = Distribution.Info(SelDistrib - 1).InvCdfInfo.Display.LineWidth

                'Add InvRevCDF display settings:
                dgvFields.Rows(5).Cells(11).Value = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.MarkerFill
                dgvFields.Rows(5).Cells(12).Style.BackColor = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.MarkerColor
                dgvFields.Rows(5).Cells(13).Style.BackColor = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.BorderColor
                dgvFields.Rows(5).Cells(14).Value = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.BorderWidth
                dgvFields.Rows(5).Cells(15).Value = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.MarkerStyle
                dgvFields.Rows(5).Cells(16).Value = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.MarkerSize
                dgvFields.Rows(5).Cells(17).Value = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.MarkerStep
                dgvFields.Rows(5).Cells(18).Style.BackColor = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.LineColor
                dgvFields.Rows(5).Cells(19).Value = Distribution.Info(SelDistrib - 1).InvRevCdfInfo.Display.LineWidth


                dgvFields.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
                dgvFields.AutoResizeColumns()
            Else
                'Invalid continuity string.
            End If
        End If
    End Sub

    Private Sub txtMinValue_TextChanged(sender As Object, e As EventArgs) Handles txtMinValue.TextChanged

    End Sub

    Private Sub txtMinValue_LostFocus(sender As Object, e As EventArgs) Handles txtMinValue.LostFocus
        Distribution.ContSampling.Minimum = txtMinValue.Text
        Distribution.ContSampling.RecalcSamplingSettings("Minimum")
        ReDisplayContSampling()
    End Sub

    Private Sub chkLockSampMin_CheckedChanged(sender As Object, e As EventArgs) Handles chkLockSampMin.CheckedChanged
        Distribution.ContSampling.MinLock = chkLockSampMin.Checked
    End Sub

    Private Sub txtMaxValue_TextChanged(sender As Object, e As EventArgs) Handles txtMaxValue.TextChanged

    End Sub

    Private Sub txtMaxValue_LostFocus(sender As Object, e As EventArgs) Handles txtMaxValue.LostFocus
        Distribution.ContSampling.Maximum = txtMaxValue.Text
        Distribution.ContSampling.RecalcSamplingSettings("Maximum")
        ReDisplayContSampling()
    End Sub

    Private Sub chkLockSampMax_CheckedChanged(sender As Object, e As EventArgs) Handles chkLockSampMax.CheckedChanged
        Distribution.ContSampling.MaxLock = chkLockSampMax.Checked
    End Sub

    Private Sub txtSampleInt_TextChanged(sender As Object, e As EventArgs) Handles txtSampleInt.TextChanged

    End Sub

    Private Sub txtSampleInt_LostFocus(sender As Object, e As EventArgs) Handles txtSampleInt.LostFocus
        Distribution.ContSampling.Interval = txtSampleInt.Text
        Distribution.ContSampling.RecalcSamplingSettings("Interval")
        ReDisplayContSampling()
    End Sub

    Private Sub chkLockSampInt_CheckedChanged(sender As Object, e As EventArgs) Handles chkLockSampInt.CheckedChanged
        Distribution.ContSampling.IntervalLock = chkLockSampInt.Checked
    End Sub

    Private Sub txtNSamples_TextChanged(sender As Object, e As EventArgs) Handles txtNSamples.TextChanged

    End Sub

    Private Sub txtNSamples_LostFocus(sender As Object, e As EventArgs) Handles txtNSamples.LostFocus
        Distribution.ContSampling.NSamples = txtNSamples.Text
        Distribution.ContSampling.RecalcSamplingSettings("NSamples")
        ReDisplayContSampling()
    End Sub

    Private Sub chkLockNSamples_CheckedChanged(sender As Object, e As EventArgs) Handles chkLockNSamples.CheckedChanged
        Distribution.ContSampling.NSamplesLock = chkLockNSamples.Checked
    End Sub

    Private Sub ReDisplayContSampling()
        'Redisplay the Continuous Sampling parameters.
        If Distribution.ContSampling.MinLock Then Else txtMinValue.Text = Distribution.ContSampling.Minimum
        If Distribution.ContSampling.MaxLock Then Else txtMaxValue.Text = Distribution.ContSampling.Maximum
        If Distribution.ContSampling.IntervalLock Then Else txtSampleInt.Text = Distribution.ContSampling.Interval
        If Distribution.ContSampling.NSamplesLock Then Else txtNSamples.Text = Distribution.ContSampling.NSamples
    End Sub

    Private Sub txtXAxisLabel_TextChanged(sender As Object, e As EventArgs) Handles txtXAxisLabel.TextChanged

    End Sub

    Private Sub txtXAxisLabel_LostFocus(sender As Object, e As EventArgs) Handles txtXAxisLabel.LostFocus
        Distribution.ContSampling.Label = txtXAxisLabel.Text
    End Sub

    Private Sub txtXAxisUnits_TextChanged(sender As Object, e As EventArgs) Handles txtXAxisUnits.TextChanged

    End Sub

    Private Sub txtXAxisUnits_LostFocus(sender As Object, e As EventArgs) Handles txtXAxisUnits.LostFocus
        Distribution.ContSampling.Units = txtXAxisUnits.Text
    End Sub

    Private Sub txtXAxisDescription_TextChanged(sender As Object, e As EventArgs) Handles txtXAxisUnits.LostFocus

    End Sub

    Private Sub txtXAxisDescription_LostFocus(sender As Object, e As EventArgs) Handles txtXAxisDescription.LostFocus
        Distribution.ContSampling.Description = txtXAxisDescription.Text
    End Sub

    Private Sub txtDiscMin_TextChanged(sender As Object, e As EventArgs) Handles txtDiscMin.TextChanged

    End Sub

    Private Sub txtDiscMin_LostFocus(sender As Object, e As EventArgs) Handles txtDiscMin.LostFocus
        Distribution.DiscSampling.Minimum = txtDiscMin.Text
    End Sub

    Private Sub txtDiscMax_TextChanged(sender As Object, e As EventArgs) Handles txtDiscMax.TextChanged

    End Sub

    Private Sub txtDiscMax_LostFocus(sender As Object, e As EventArgs) Handles txtDiscMax.LostFocus
        Distribution.DiscSampling.Maximum = txtDiscMax.Text
    End Sub

    Private Sub txtDiscXAxisLabel_TextChanged(sender As Object, e As EventArgs) Handles txtDiscXAxisLabel.TextChanged

    End Sub

    Private Sub txtDiscXAxisLabel_LostFocus(sender As Object, e As EventArgs) Handles txtDiscXAxisLabel.LostFocus
        Distribution.DiscSampling.Label = txtDiscXAxisLabel.Text
    End Sub

    Private Sub txtDiscXAxisUnits_TextChanged(sender As Object, e As EventArgs) Handles txtDiscXAxisUnits.TextChanged

    End Sub

    Private Sub txtDiscXAxisUnits_LostFocus(sender As Object, e As EventArgs) Handles txtDiscXAxisUnits.LostFocus
        Distribution.DiscSampling.Units = txtDiscXAxisUnits.Text
    End Sub

    Private Sub txtDiscXAxisDescr_TextChanged(sender As Object, e As EventArgs) Handles txtDiscXAxisDescr.TextChanged

    End Sub

    Private Sub txtDiscXAxisDescr_LostFocus(sender As Object, e As EventArgs) Handles txtDiscXAxisDescr.LostFocus
        Distribution.DiscSampling.Description = txtDiscXAxisDescr.Text
    End Sub

    Private Sub cmbDefMkrFill_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDefMkrFill.SelectedIndexChanged
        'The default Marker Fill setting has changed.
        Distribution.Info(SelDistrib - 1).Display.MarkerFill = cmbDefMkrFill.SelectedItem.ToString
    End Sub

    Private Sub txtDefMkrColor_TextChanged(sender As Object, e As EventArgs) Handles txtDefMkrColor.TextChanged

    End Sub

    Private Sub txtDefMkrColor_Click(sender As Object, e As EventArgs) Handles txtDefMkrColor.Click
        'Select the default Marker color.
        ColorDialog1.Color = txtDefMkrColor.BackColor
        ColorDialog1.ShowDialog
        txtDefMkrColor.BackColor = ColorDialog1.Color
        Distribution.Info(SelDistrib - 1).Display.MarkerColor = ColorDialog1.Color
    End Sub

    Private Sub txtDefBorderColor_TextChanged(sender As Object, e As EventArgs) Handles txtDefBorderColor.TextChanged

    End Sub

    Private Sub txtDefBorderColor_Click(sender As Object, e As EventArgs) Handles txtDefBorderColor.Click
        'Select the default Marker Border color.
        ColorDialog1.Color = txtDefBorderColor.BackColor
        ColorDialog1.ShowDialog()
        txtDefBorderColor.BackColor = ColorDialog1.Color
        Distribution.Info(SelDistrib - 1).Display.BorderColor = ColorDialog1.Color
    End Sub

    Private Sub txtDefBorderWidth_TextChanged(sender As Object, e As EventArgs) Handles txtDefBorderWidth.TextChanged

    End Sub

    Private Sub txtDefBorderWidth_LostFocus(sender As Object, e As EventArgs) Handles txtDefBorderWidth.LostFocus
        'The Marker Border Width has been changed.
        Try
            Dim Width As Integer = txtDefBorderWidth.Text
            Distribution.Info(SelDistrib - 1).Display.BorderWidth = Width
        Catch ex As Exception
            Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub

    Private Sub cmbDefMkrStyle_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDefMkrStyle.SelectedIndexChanged
        'The default Marker Style setting has changed.
        Distribution.Info(SelDistrib - 1).Display.MarkerStyle = cmbDefMkrStyle.SelectedItem.ToString
    End Sub

    Private Sub txtDefMkrSize_TextChanged(sender As Object, e As EventArgs) Handles txtDefMkrSize.TextChanged

    End Sub

    Private Sub txtDefMkrSize_LostFocus(sender As Object, e As EventArgs) Handles txtDefMkrSize.LostFocus
        'The Marker Size has been changed.
        Try
            Dim Size As Integer = txtDefMkrSize.Text
            Distribution.Info(SelDistrib - 1).Display.MarkerSize = Size
        Catch ex As Exception
            Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub

    Private Sub txtDefMkrStep_TextChanged(sender As Object, e As EventArgs) Handles txtDefMkrStep.TextChanged

    End Sub

    Private Sub txtDefMkrStep_LostFocus(sender As Object, e As EventArgs) Handles txtDefMkrStep.LostFocus
        'The Marker Step has been changed.
        Try
            Dim MarkerStep As Integer = txtDefMkrStep.Text
            Distribution.Info(SelDistrib - 1).Display.MarkerStep = MarkerStep
        Catch ex As Exception
            Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub

    Private Sub txtDefLineColor_TextChanged(sender As Object, e As EventArgs) Handles txtDefLineColor.TextChanged

    End Sub

    Private Sub txtDefLineColor_Click(sender As Object, e As EventArgs) Handles txtDefLineColor.Click
        'Select the default Line color.
        ColorDialog1.Color = txtDefLineColor.BackColor
        ColorDialog1.ShowDialog()
        txtDefLineColor.BackColor = ColorDialog1.Color
        Distribution.Info(SelDistrib - 1).Display.LineColor = ColorDialog1.Color
    End Sub

    Private Sub txtDefLineWidth_TextChanged(sender As Object, e As EventArgs) Handles txtDefLineWidth.TextChanged

    End Sub

    Private Sub txtDefLineWidth_LostFocus(sender As Object, e As EventArgs) Handles txtDefLineWidth.LostFocus
        'The Line Width has been changed.
        Try
            Dim Width As Integer = txtDefLineWidth.Text
            Distribution.Info(SelDistrib - 1).Display.LineWidth = Width
        Catch ex As Exception
            Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub

    Public Sub ReplotCharts()
        'Replot all open charts
        If ChartList.Count = 0 Then
            'No chart forms have been opened.
        Else
            Dim I As Integer
            For I = 0 To ChartList.Count - 1
                If IsNothing(ChartList(I)) Then

                Else
                    ChartList(I).Plot()
                End If
            Next
        End If
    End Sub

    Public Sub UpdateAnnotationSettings(DistribNo As Integer)
        'Update the annotatopn settings for the specified Distribution in all open charts
        If ChartList.Count = 0 Then
            'No chart forms have been opened.
        Else
            Dim I As Integer
            For I = 0 To ChartList.Count - 1
                If IsNothing(ChartList(I)) Then

                Else
                    ChartList(I).UpdateAnnotationSettings(DistribNo)

                End If
            Next
        End If

        'THE FOLLOWING CODE SLOWS THE CHART UPDATE. A timer is now used to update the Adjust form 1 second after the annotation updates have ceased.
        'If IsNothing(Adjust) Then
        '    'The Adjust Parameters form is closed.
        'Else
        '    Adjust.LoadParamInfo()
        'End If
        Timer2.Start()

    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        '200ms after the last Scroll event, the distribution parameter display will be updated.
        If IsNothing(Adjust) Then
            'The Adjust Parameters form is closed.
        Else
            'If Adjust.Focused Or Adjust.pbAB.Focused Or Adjust.pbCD.Focused Or Adjust.hsA.Focused Or Adjust.hsB.Focused Or Adjust.hsC.Focused Or Adjust.hsD.Focused Or Adjust.hsE.Focused Then
            If Adjust.InUse = True Then
                'No need to update - the form is in use.
            Else
                Adjust.LoadParamInfo()
                Message.Add("Adjust.LoadParamInfo" & vbCrLf)
            End If

        End If
        Timer2.Stop()
    End Sub


    Private Sub btnSaveMCModel_Click(sender As Object, e As EventArgs) Handles btnSaveDistModel.Click
        'Save the Distribution Data Model
        SaveDistModel()
    End Sub

    'Private Sub SaveDistModel()
    Public Sub SaveDistModel()

        Dim FileName As String = txtFileName.Text.Trim

        'Check if a file name has been specified:
        If FileName = "" Then
            Message.AddWarning("Please enter a file name." & vbCrLf)
            Exit Sub
        End If

        'Check the file name extension:
        If LCase(FileName).EndsWith(".distrib") Then
            FileName = IO.Path.GetFileNameWithoutExtension(FileName) & ".Distrib"
        ElseIf FileName.Contains(".") Then
            Message.AddWarning("Unknown file extension: " & IO.Path.GetExtension(FileName) & vbCrLf)
            Exit Sub
        Else
            FileName = FileName & ".Distrib"
        End If

        txtFileName.Text = FileName

        'Update the Distribution Data Model settings:
        'Distribution.FileName = txtModelName.Text.Trim
        Distribution.FileName = FileName
        Distribution.ModelName = txtModelName.Text.Trim
        Distribution.Label = txtLabel.Text.Trim
        Distribution.Description = txtDescription.Text.Trim
        Distribution.Notes = txtNotes.Text.Trim

        Project.SaveXmlData(FileName, Distribution.ToXDoc)
        Distribution.Modified = False

    End Sub

    Private Sub cmbColors_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbColors.SelectedIndexChanged

        If cmbColors.Focused Then 'Only update the selected color if the control has the focus. If the nearest named color is selected remotely, the selected color should not be changed.
            Dim SelColor As Color = Color.FromName(cmbColors.SelectedItem.ToString)
            txtNamedColor.BackColor = SelColor
            SetRgbVals(SelColor)
            SetRgbNamedVals(SelColor)
            SetHlsVals(SelColor)
            Dim myColor As Color = Color.FromArgb(255, DecRed, DecGreen, DecBlue)
            txtSpecColor.BackColor = myColor
        End If

    End Sub

    Private Function ReadColorVal(StrVal As String) As Integer
        'Read a color value string and convert it to an integer value
        'The color value can be decimal or hexadecimal.
        If rbHex.Checked Then 'StrVal is hexadecimal.
            Try
                Return Convert.ToInt32(StrVal.Trim, 16)
            Catch ex As Exception
                Message.AddWarning(ex.Message & vbCrLf)
            End Try
        Else 'StrVal is decimal.
            Try
                Return Int(StrVal)
            Catch ex As Exception
                Message.AddWarning(ex.Message & vbCrLf)
            End Try
        End If
    End Function

    Private Function WriteColorVal(IntVal As Integer) As String
        'Write a color value integer as a string value.
        'The string value can be decimal or hexadecimal.
        If rbHex.Checked Then 'Return a hexadecimal number string.
            Return Hex(IntVal)
        Else 'Return a decimal number string
            Return IntVal
        End If

    End Function

    Private Sub txtRed_TextChanged(sender As Object, e As EventArgs) Handles txtRed.TextChanged

    End Sub

    Private Sub txtRed_LostFocus(sender As Object, e As EventArgs) Handles txtRed.LostFocus
        Try
            'Dim Red As Integer = txtRed.Text
            Dim Red As Integer = ReadColorVal(txtRed.Text)
            If Red < 0 Then Red = 0
            If Red > 255 Then Red = 255
            'txtRed.Text = Red
            txtRed.Text = WriteColorVal(Red)
            DecRed = Red
            Dim myColor As Color = Color.FromArgb(255, DecRed, DecGreen, DecBlue)
            SetHlsVals(myColor)
            txtSpecColor.BackColor = myColor
            GetNearestNamedColor(myColor)
        Catch ex As Exception
            Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub

    Private Sub SetHlsVals(myColor As Color)
        'Sets the displayed Hue, Luminance and Saturation values.

        txtHue.Text = WriteColorVal(Int(myColor.GetHue * 240 / 360))  'This is displayed as an integer between 0 and 239
        txtLum.Text = WriteColorVal(Int(myColor.GetBrightness * 240)) 'This is displayed as an integer between 0 and 240
        txtSat.Text = WriteColorVal(Int(myColor.GetSaturation * 240)) 'This is displayed as an integer between 0 and 240

        SngHue = myColor.GetHue         'This value ranges from 0.0 to 360.0
        SngLum = myColor.GetBrightness  'This value ranges from 0.0 to 1.0
        SngSat = myColor.GetSaturation  'This value ranges from 0.0 to 1.0
    End Sub

    Private Sub SetRgbVals(myColor As Color)
        'Sets the displayed Red, Green and Blue values.
        DecRed = myColor.R
        DecGreen = myColor.G
        DecBlue = myColor.B

        txtRed.Text = WriteColorVal(DecRed)
        txtGreen.Text = WriteColorVal(DecGreen)
        txtBlue.Text = WriteColorVal(DecBlue)
    End Sub

    Private Sub SetRgbNamedVals(myColor As Color)
        'Sets the displayed Named Red, Green and Blue values.
        txtNamedRed.Text = WriteColorVal(myColor.R)
        txtNamedGreen.Text = WriteColorVal(myColor.G)
        txtNamedBlue.Text = WriteColorVal(myColor.B)
    End Sub

    Private Sub GetNearestNamedColor(myColor As Color)
        'Find the nearst named color to the specified color.
        'Dim myColorName As String
        'Dim myNamedColor As Color
        Dim NearestColorName As String
        Dim NearestColorDist As Integer = 10000
        Dim NearestColor As Color
        'Dim NearestColor As Color

        Dim ColorDist As Integer 'The color distance between myColor and the known color.
        Dim ColorName As String
        Dim NamedColor As Color

        'Search the list of Color names:
        For Each KColor As KnownColor In [Enum].GetValues(GetType(KnownColor)) 'ActiveBorder to MenuHighlight - Includes system color names
            If KColor > 27 And KColor < 168 Then
                'cmbColors.Items.Add([Enum].GetName(GetType(KnownColor), Color)) 'AliceBlue to YellowGreen - System color names not included.
                ColorName = [Enum].GetName(GetType(KnownColor), KColor)
                NamedColor = Color.FromName(ColorName)
                ColorDist = Math.Abs(CInt(myColor.R) - CInt(NamedColor.R)) + Math.Abs(CInt(myColor.G) - CInt(NamedColor.G)) + Math.Abs(CInt(myColor.B) - CInt(NamedColor.B))
                If ColorDist < NearestColorDist Then
                    NearestColorDist = ColorDist
                    NearestColorName = ColorName
                End If
            End If
        Next

        cmbColors.SelectedIndex = cmbColors.FindStringExact(NearestColorName)
        NearestColor = Color.FromName(NearestColorName)
        'txtNamedColor.BackColor = Color.FromName(NearestColorName)
        txtNamedColor.BackColor = NearestColor
        SetRgbNamedVals(NearestColor)

    End Sub

    Private Sub txtGreen_TextChanged(sender As Object, e As EventArgs) Handles txtGreen.TextChanged

    End Sub

    Private Sub txtGreen_LostFocus(sender As Object, e As EventArgs) Handles txtGreen.LostFocus
        Try
            'Dim Green As Integer = txtGreen.Text
            Dim Green As Integer = ReadColorVal(txtGreen.Text)
            If Green < 0 Then Green = 0
            If Green > 255 Then Green = 255
            'txtGreen.Text = Green
            txtGreen.Text = WriteColorVal(Green)
            DecGreen = Green
            Dim myColor As Color = Color.FromArgb(255, DecRed, DecGreen, DecBlue)
            SetHlsVals(myColor)
            txtSpecColor.BackColor = myColor
            GetNearestNamedColor(myColor)
        Catch ex As Exception
            Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub

    Private Sub txtBlue_TextChanged(sender As Object, e As EventArgs) Handles txtBlue.TextChanged

    End Sub

    Private Sub txtBlue_LostFocus(sender As Object, e As EventArgs) Handles txtBlue.LostFocus
        Try
            'Dim Blue As Integer = txtBlue.Text
            Dim Blue As Integer = ReadColorVal(txtBlue.Text)
            If Blue < 0 Then Blue = 0
            If Blue > 255 Then Blue = 255
            'txtBlue.Text = Blue
            txtBlue.Text = WriteColorVal(Blue)
            DecBlue = Blue
            Dim myColor As Color = Color.FromArgb(255, DecRed, DecGreen, DecBlue)
            SetHlsVals(myColor)
            txtSpecColor.BackColor = myColor
            GetNearestNamedColor(myColor)
        Catch ex As Exception
            Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub

    Private Sub txtHue_TextChanged(sender As Object, e As EventArgs) Handles txtHue.TextChanged

    End Sub

    Private Sub txtHue_LostFocus(sender As Object, e As EventArgs) Handles txtHue.LostFocus
        Try
            'Dim Hue As Integer = txtHue.Text
            Dim Hue As Integer = ReadColorVal(txtHue.Text)
            If Hue < 0 Then Hue = 0
            If Hue > 239 Then Hue = 239
            txtHue.Text = WriteColorVal(Hue)
            SngHue = Hue * 360 / 239
            Dim myColor As Color = HlsToColor(SngHue, SngLum, SngSat)
            SetRgbVals(myColor)
            txtSpecColor.BackColor = myColor
            GetNearestNamedColor(myColor)
        Catch ex As Exception
            Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub

    Private Function HlsToColor(Hue As Single, Lum As Single, Sat As Single) As Color
        'Convert Hue, Luminance and Saturation to a Color.
        'Hue: 0.0 to 360.0
        'Lum: 0.0 to 1.0
        'Sat: 0.0 to 1.0

        'https://stackoverflow.com/questions/4123998/algorithm-to-switch-between-rgb-and-hsb-color-values

        Dim Red As Single
        Dim Green As Single
        Dim Blue As Single

        If Sat = 0 Then
            'All colors are the same - gray.
            Red = Lum
            Green = Lum
            Blue = Lum
        Else
            'Calculate the appropriate sector of a 6-part color wheel.
            Dim SectorPos As Single = Hue / 60
            Dim SectorNum As Integer = CInt(Math.Floor(SectorPos))

            'Find the fractional part of the sectror - how may degrees into the sector
            Dim FracSector As Single = SectorPos - SectorNum

            'Calculate values for the three axes of the color
            Dim P As Single = Lum * (1 - Sat)
            Dim Q As Single = Lum * (1 - (Sat * FracSector))
            Dim T As Single = Lum * (1 - (Sat * (1 - FracSector)))

            'Assign the fractional colors to red, green and blue components based on the sector
            Select Case SectorNum
                Case 0, 6
                    Red = Lum
                    Green = T
                    Blue = P
                Case 1
                    Red = Q
                    Green = Lum
                    Blue = P
                Case 2
                    Red = P
                    Green = Lum
                    Blue = T
                Case 3
                    Red = P
                    Green = Q
                    Blue = Lum
                Case 4
                    Red = T
                    Green = P
                    Blue = Lum
                Case 5
                    Red = Lum
                    Green = P
                    Blue = Q
            End Select

        End If

        Return Color.FromArgb(255, CInt(Math.Round(Red * 255, MidpointRounding.AwayFromZero)), CInt(Math.Round(Green * 255, MidpointRounding.AwayFromZero)), CInt(Math.Round(Blue * 255, MidpointRounding.AwayFromZero)))

        'http://computer-programming-forum.com/6-vbdotnet/f07e444c1af0f405.htm
        'https://stackoverflow.com/questions/2353211/hsl-to-rgb-color-conversion
        'http://www.xbeat.net/vbspeed/c_HSLToRGB2.htm

    End Function

    Private Sub txtSat_TextChanged(sender As Object, e As EventArgs) Handles txtSat.TextChanged

    End Sub

    Private Sub txtSat_LostFocus(sender As Object, e As EventArgs) Handles txtSat.LostFocus
        Try
            'Dim Sat As Integer = txtSat.Text
            Dim Sat As Integer = ReadColorVal(txtSat.Text)
            If Sat < 0 Then Sat = 0
            If Sat > 240 Then Sat = 240
            txtSat.Text = WriteColorVal(Sat)
            SngSat = Sat / 240
            Dim myColor As Color = HlsToColor(SngHue, SngLum, SngSat)
            SetRgbVals(myColor)
            txtSpecColor.BackColor = myColor
            GetNearestNamedColor(myColor)
        Catch ex As Exception
            Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub

    Private Sub txtLum_TextChanged(sender As Object, e As EventArgs) Handles txtLum.TextChanged

    End Sub

    Private Sub txtLum_LostFocus(sender As Object, e As EventArgs) Handles txtLum.LostFocus
        Try
            'Dim Lum As Integer = txtLum.Text
            Dim Lum As Integer = ReadColorVal(txtLum.Text)
            If Lum < 0 Then Lum = 0
            If Lum > 240 Then Lum = 240
            txtLum.Text = WriteColorVal(Lum)
            SngLum = Lum / 240
            Dim myColor As Color = HlsToColor(SngHue, SngLum, SngSat)
            SetRgbVals(myColor)
            txtSpecColor.BackColor = myColor
            GetNearestNamedColor(myColor)
        Catch ex As Exception
            Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub

    Private Sub rbHex_CheckedChanged(sender As Object, e As EventArgs) Handles rbHex.CheckedChanged
        If rbHex.Checked Then
            txtRed.Text = WriteColorVal(DecRed)
            txtGreen.Text = WriteColorVal(DecGreen)
            txtBlue.Text = WriteColorVal(DecBlue)

            txtHue.Text = WriteColorVal(Int(SngHue * 240 / 360))  'This is displayed as an integer between 0 and 239
            txtLum.Text = WriteColorVal(Int(SngLum * 240)) 'This is displayed as an integer between 0 and 240
            txtSat.Text = WriteColorVal(Int(SngSat * 240)) 'This is displayed as an integer between 0 and 240

            SetRgbNamedVals(txtNamedColor.BackColor)
        End If
    End Sub

    Private Sub rbDecimal_CheckedChanged(sender As Object, e As EventArgs) Handles rbDecimal.CheckedChanged
        If rbDecimal.Checked Then
            txtRed.Text = WriteColorVal(DecRed)
            txtGreen.Text = WriteColorVal(DecGreen)
            txtBlue.Text = WriteColorVal(DecBlue)

            txtHue.Text = WriteColorVal(Int(SngHue * 240 / 360))  'This is displayed as an integer between 0 and 239
            txtLum.Text = WriteColorVal(Int(SngLum * 240)) 'This is displayed as an integer between 0 and 240
            txtSat.Text = WriteColorVal(Int(SngSat * 240)) 'This is displayed as an integer between 0 and 240

            SetRgbNamedVals(txtNamedColor.BackColor)
        End If
    End Sub

    Private Sub txtHex_TextChanged(sender As Object, e As EventArgs) Handles txtHex.TextChanged

        If txtHex.Focused Then
            Dim HexVal As String = txtHex.Text.Trim
            If Len(HexVal) = 6 Then
                ProcessHexColorString(HexVal)
                'DecRed = Convert.ToInt32(Mid(HexVal, 1, 2), 16)
                'DecGreen = Convert.ToInt32(Mid(HexVal, 3, 2), 16)
                'DecBlue = Convert.ToInt32(Mid(HexVal, 5, 2), 16)

                'txtRed.Text = WriteColorVal(DecRed)
                'txtGreen.Text = WriteColorVal(DecGreen)
                'txtBlue.Text = WriteColorVal(DecBlue)

                'Dim myColor As Color = Color.FromArgb(255, DecRed, DecGreen, DecBlue)
                'txtSpecColor.BackColor = myColor

                'SetHlsVals(myColor)

                'GetNearestNamedColor(myColor)

                ''txtNamedHex.Text = Hex(ReadColorVal(txtNamedRed.Text)) & Hex(ReadColorVal(txtNamedGreen.Text)) & Hex(ReadColorVal(txtNamedBlue.Text))

                'Dim HexRed As String = Hex(ReadColorVal(txtNamedRed.Text))
                'If Len(HexRed) = 1 Then HexRed = "0" & HexRed
                'Dim HexGreen As String = Hex(ReadColorVal(txtNamedGreen.Text))
                'If Len(HexGreen) = 1 Then HexGreen = "0" & HexGreen
                'Dim HexBlue As String = Hex(ReadColorVal(txtNamedBlue.Text))
                'If Len(HexBlue) = 1 Then HexBlue = "0" & HexBlue
                'txtNamedHex.Text = HexRed & HexGreen & HexBlue

            Else
                'Not 6 characters!
                If HexVal.StartsWith("#") Then
                    HexVal = HexVal.Substring(1)
                    If Len(HexVal) = 6 Then
                        ProcessHexColorString(HexVal)
                    Else
                        Message.AddWarning("Unknown HexaDecimal color string: " & HexVal & vbCrLf)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub ProcessHexColorString(HexVal As String)
        'Process the HexaDecimal color string.

        DecRed = Convert.ToInt32(Mid(HexVal, 1, 2), 16)
        DecGreen = Convert.ToInt32(Mid(HexVal, 3, 2), 16)
        DecBlue = Convert.ToInt32(Mid(HexVal, 5, 2), 16)

        txtRed.Text = WriteColorVal(DecRed)
        txtGreen.Text = WriteColorVal(DecGreen)
        txtBlue.Text = WriteColorVal(DecBlue)

        Dim myColor As Color = Color.FromArgb(255, DecRed, DecGreen, DecBlue)
        txtSpecColor.BackColor = myColor

        SetHlsVals(myColor)

        GetNearestNamedColor(myColor)

        'txtNamedHex.Text = Hex(ReadColorVal(txtNamedRed.Text)) & Hex(ReadColorVal(txtNamedGreen.Text)) & Hex(ReadColorVal(txtNamedBlue.Text))

        Dim HexRed As String = Hex(ReadColorVal(txtNamedRed.Text))
        If Len(HexRed) = 1 Then HexRed = "0" & HexRed
        Dim HexGreen As String = Hex(ReadColorVal(txtNamedGreen.Text))
        If Len(HexGreen) = 1 Then HexGreen = "0" & HexGreen
        Dim HexBlue As String = Hex(ReadColorVal(txtNamedBlue.Text))
        If Len(HexBlue) = 1 Then HexBlue = "0" & HexBlue
        txtNamedHex.Text = HexRed & HexGreen & HexBlue
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        'If SelParamSet < NParamSets Then
        If SelDistrib < NDistribs Then
            SelDistrib += 1
        Else
            'The last parameter set is already selected.
        End If
    End Sub

    Private Sub btnPrev_Click(sender As Object, e As EventArgs) Handles btnPrev.Click
        If SelDistrib > 1 Then
            SelDistrib -= 1
        Else
            'The first parameter set is already selected.
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        'Delete the selected distribution

        If NDistribs = 1 Then
            Message.AddWarning("There must be at least one distribution in the model." & vbCrLf)
        Else
            SaveAllOpenCharts() 'Save the changes made to all open charts.

            Dim DelDistribNo As Integer = SelDistrib
            Dim OrigDistribCount As Integer = Distribution.Info.Count

            Distribution.Info.RemoveAt(SelDistrib - 1) 'Remove the selected distribution

            'Remove the corresponding table columns.
            'If Distribution.Data.Tables.Contains("DataTable") Then
            If Distribution.Data.Tables.Contains("Continuous_Data_Table") Then
                'If Distribution.Data.Tables("DataTable").Columns.Contains("PDF_" & DelDistribNo) Then Distribution.Data.Tables("DataTable").Columns.Remove("PDF_" & DelDistribNo)
                'If Distribution.Data.Tables("Continuous_Data_Table").Columns.Contains("PDF_" & DelDistribNo) Then Distribution.Data.Tables("DataTable").Columns.Remove("PDF_" & DelDistribNo)
                If Distribution.Data.Tables("Continuous_Data_Table").Columns.Contains("PDF_" & DelDistribNo) Then Distribution.Data.Tables("Continuous_Data_Table").Columns.Remove("PDF_" & DelDistribNo)
                If Distribution.Data.Tables("Continuous_Data_Table").Columns.Contains("PDFLn_" & DelDistribNo) Then Distribution.Data.Tables("Continuous_Data_Table").Columns.Remove("PDFLn_" & DelDistribNo)
                If Distribution.Data.Tables("Continuous_Data_Table").Columns.Contains("PMF_" & DelDistribNo) Then Distribution.Data.Tables("Continuous_Data_Table").Columns.Remove("PMF_" & DelDistribNo)
                If Distribution.Data.Tables("Continuous_Data_Table").Columns.Contains("PMFLn_" & DelDistribNo) Then Distribution.Data.Tables("Continuous_Data_Table").Columns.Remove("PMFLn_" & DelDistribNo)
                If Distribution.Data.Tables("Continuous_Data_Table").Columns.Contains("CDF_" & DelDistribNo) Then Distribution.Data.Tables("Continuous_Data_Table").Columns.Remove("CDF_" & DelDistribNo)
                If Distribution.Data.Tables("Continuous_Data_Table").Columns.Contains("RevCDF_" & DelDistribNo) Then Distribution.Data.Tables("Continuous_Data_Table").Columns.Remove("RevCDF_" & DelDistribNo)
                If Distribution.Data.Tables("Continuous_Data_Table").Columns.Contains("InvCDF_" & DelDistribNo) Then Distribution.Data.Tables("Continuous_Data_Table").Columns.Remove("InvCDF_" & DelDistribNo)
                If Distribution.Data.Tables("Continuous_Data_Table").Columns.Contains("InvRevCDF_" & DelDistribNo) Then Distribution.Data.Tables("Continuous_Data_Table").Columns.Remove("InvRevCDF_" & DelDistribNo)
            End If

            If DelDistribNo < OrigDistribCount Then 'Some of the Series numbers of the generated data need to be updated.
                Dim I As Integer
                For I = DelDistribNo To OrigDistribCount - 1
                    'Rename any existing DataTable columns corresponding to distributions that now have new index numbers.
                    Distribution.Info(I - 1).PdfInfo.Name = "PDF_" & I
                    'If Distribution.Data.Tables("DataTable").Columns.Contains("PDF_" & I + 1) Then Distribution.Data.Tables("DataTable").Columns("PDF_" & I + 1).ColumnName = "PDF_" & I
                    'If Distribution.Data.Tables("Continuous_Data_Table").Columns.Contains("PDF_" & I + 1) Then Distribution.Data.Tables("DataTable").Columns("PDF_" & I + 1).ColumnName = "PDF_" & I
                    If Distribution.Data.Tables("Continuous_Data_Table").Columns.Contains("PDF_" & I + 1) Then Distribution.Data.Tables("Continuous_Data_Table").Columns("PDF_" & I + 1).ColumnName = "PDF_" & I
                    Distribution.Info(I - 1).PdfLnInfo.Name = "PDFLn_" & I
                    If Distribution.Data.Tables("Continuous_Data_Table").Columns.Contains("PDFLn_" & I + 1) Then Distribution.Data.Tables("Continuous_Data_Table").Columns("PDFLn_" & I + 1).ColumnName = "PDFLn_" & I
                    Distribution.Info(I - 1).PmfInfo.Name = "PMF_" & I
                    If Distribution.Data.Tables("Continuous_Data_Table").Columns.Contains("PMF_" & I + 1) Then Distribution.Data.Tables("Continuous_Data_Table").Columns("PMF_" & I + 1).ColumnName = "PMF_" & I
                    Distribution.Info(I - 1).PmfLnInfo.Name = "PMFLn_" & I
                    If Distribution.Data.Tables("Continuous_Data_Table").Columns.Contains("PMFLn_" & I + 1) Then Distribution.Data.Tables("Continuous_Data_Table").Columns("PMFLn_" & I + 1).ColumnName = "PMFLn_" & I
                    Distribution.Info(I - 1).CdfInfo.Name = "CDF_" & I
                    If Distribution.Data.Tables("Continuous_Data_Table").Columns.Contains("CDF_" & I + 1) Then Distribution.Data.Tables("Continuous_Data_Table").Columns("CDF_" & I + 1).ColumnName = "CDF_" & I

                    Distribution.Info(I - 1).RevCdfInfo.Name = "RevCDF_" & I
                    If Distribution.Data.Tables("Continuous_Data_Table").Columns.Contains("RevCDF_" & I + 1) Then Distribution.Data.Tables("Continuous_Data_Table").Columns("RevCDF_" & I + 1).ColumnName = "RevCDF_" & I

                    Distribution.Info(I - 1).InvCdfInfo.Name = "InvCDF_" & I
                    If Distribution.Data.Tables("Continuous_Data_Table").Columns.Contains("InvCDF_" & I + 1) Then Distribution.Data.Tables("Continuous_Data_Table").Columns("InvCDF_" & I + 1).ColumnName = "InvCDF_" & I

                    Distribution.Info(I - 1).InvRevCdfInfo.Name = "InvRevCDF_" & I
                    If Distribution.Data.Tables("Continuous_Data_Table").Columns.Contains("InvRevCDF_" & I + 1) Then Distribution.Data.Tables("Continuous_Data_Table").Columns("InvRevCDF_" & I + 1).ColumnName = "InvRevCDF_" & I
                Next
            End If

            'Open each saved chart and delete any series using the deleted distribution and update the DistribNo.
            Dim SeriesInfo As IEnumerable(Of XElement)
            Dim Index As Integer
            Dim ProcessNext As Boolean
            Dim NewSeriesIndex As Integer
            For Each ChartInfo In Distribution.ChartList
                SeriesInfo = From item In ChartInfo.Value.<ChartSettings>.<SeriesCollection>.<Series>
                Index = 0
                ProcessNext = True
                If SeriesInfo.Count > 0 Then
                    While ProcessNext
                        If SeriesInfo(Index).<DistribNo>.Value = DelDistribNo Then 'This Series uses data from the deleted distribution.
                            SeriesInfo(Index).Remove()
                        ElseIf SeriesInfo(Index).<DistribNo>.Value > DelDistribNo Then 'This Series Number must be decremented to fill the index number gap left by the deleted series.
                            NewSeriesIndex = SeriesInfo(Index).<DistribNo>.Value - 1
                            SeriesInfo(Index).<DistribNo>.Value = NewSeriesIndex
                            Select Case SeriesInfo(Index).<FunctionType>.Value
                                Case "PDF"
                                    SeriesInfo(Index).<YFieldName>.Value = "PDF_" & NewSeriesIndex
                                Case "PDFLn"
                                    SeriesInfo(Index).<YFieldName>.Value = "PDFLn_" & NewSeriesIndex
                                Case "PMF"
                                    SeriesInfo(Index).<YFieldName>.Value = "PMF_" & NewSeriesIndex
                                Case "PMFLn"
                                    SeriesInfo(Index).<YFieldName>.Value = "PMFLn_" & NewSeriesIndex
                                Case "CDF"
                                    SeriesInfo(Index).<YFieldName>.Value = "CDF_" & NewSeriesIndex
                                Case "RevCDF"
                                    SeriesInfo(Index).<YFieldName>.Value = "RevCDF_" & NewSeriesIndex
                                Case "InvCDF"
                                    SeriesInfo(Index).<YFieldName>.Value = "InvCDF_" & NewSeriesIndex
                                Case "InvRevCDF"
                                    SeriesInfo(Index).<YFieldName>.Value = "InvRevCDF_" & NewSeriesIndex
                                Case Else
                                    Message.AddWarning("Unknown Function Type: " & SeriesInfo(Index).<FunctionType>.Value & vbCrLf)
                            End Select
                            Index += 1
                        Else 'This series number remains unchanged.
                            Index += 1
                        End If
                        If SeriesInfo.Count <= Index Then ProcessNext = False
                    End While
                End If
            Next

            Distribution.UpdateDistribCounts()

            NDistribs = Distribution.Info.Count
            NContinuous = Distribution.NContinuous
            NDiscrete = Distribution.NDiscrete

            'Redisplay the form.
            If SelDistrib > Distribution.Info.Count Then
                SelDistrib -= 1
            Else
                UpdateForm()
            End If

            'Reload any open charts:
            ReplotAllOpenCharts()
        End If
    End Sub

    Private Sub btnAddDistrib_Click(sender As Object, e As EventArgs) Handles btnAddDistrib.Click
        'Add a new distribution to the list.

        Distribution.Info.Add(New DistributionInfo)

        Distribution.UpdateDistribCounts() 'Update the counts of Continuous and Discrete distriubtions.
        NDistribs = Distribution.Info.Count
        NContinuous = Distribution.NContinuous
        NDiscrete = Distribution.NDiscrete

        'Dim MultiDistribNo As Integer = NParamSets
        Dim NewDistribNo As Integer = NDistribs

        ''Copy the original distribution to the new Multi Distribution:
        'Copy the previous Distribution to the new Distribution:
        'Main.Distribution.Info(MultiDistribNo - 1).NParams = Main.Distribution.Distrib.NParams
        Distribution.Info(NewDistribNo - 1).NParams = Distribution.Info(NewDistribNo - 2).NParams
        Distribution.Info(NewDistribNo - 1).Name = Distribution.Info(NewDistribNo - 2).Name
        Distribution.Info(NewDistribNo - 1).Continuity = Distribution.Info(NewDistribNo - 2).Continuity
        Distribution.Info(NewDistribNo - 1).RangeMax = Distribution.Info(NewDistribNo - 2).RangeMax
        Distribution.Info(NewDistribNo - 1).RangeMin = Distribution.Info(NewDistribNo - 2).RangeMin

        If NewDistribNo > 21 Then 'Use a random color for the default color.
            Dim myRandom As New Random
            Distribution.Info(NewDistribNo - 1).Display.MarkerColor = Color.FromArgb(255, myRandom.Next(0, 255), myRandom.Next(0, 255), myRandom.Next(0, 255))
            Distribution.Info(NewDistribNo - 1).Display.LineColor = Distribution.Info(NewDistribNo - 1).Display.MarkerColor
        Else 'Use the list of distinct colors for the default color.
            Distribution.Info(NewDistribNo - 1).Display.MarkerColor = Distribution.DistinctCols(NewDistribNo)
            Distribution.Info(NewDistribNo - 1).Display.LineColor = Distribution.DistinctCols(NewDistribNo)
        End If

        ''Main.Distribution.MultiDistrib(MultiDistribNo - 1).ParamA = Main.Distribution.Distrib.ParamA 'THIS CREATES A LINK NOT A COPY OF ParamA!
        'Main.Distribution.MultiDistrib(MultiDistribNo - 1).ParamA = Main.Distribution.MultiDistrib(MultiDistribNo - 2).ParamA 'THIS CREATES A LINK NOT A COPY OF ParamA!
        'Main.Distribution.Info(MultiDistribNo - 1).ParamA.Name = Main.Distribution.Distrib.ParamA.Name
        Distribution.Info(NewDistribNo - 1).ParamA.Name = Distribution.Info(NewDistribNo - 2).ParamA.Name
        Distribution.Info(NewDistribNo - 1).ParamA.Symbol = Distribution.Info(NewDistribNo - 2).ParamA.Symbol
        Distribution.Info(NewDistribNo - 1).ParamA.Value = Distribution.Info(NewDistribNo - 2).ParamA.Value
        Distribution.Info(NewDistribNo - 1).ParamA.Type = Distribution.Info(NewDistribNo - 2).ParamA.Type
        Distribution.Info(NewDistribNo - 1).ParamA.NumberType = Distribution.Info(NewDistribNo - 2).ParamA.NumberType
        Distribution.Info(NewDistribNo - 1).ParamA.Minimum = Distribution.Info(NewDistribNo - 2).ParamA.Minimum
        Distribution.Info(NewDistribNo - 1).ParamA.Maximum = Distribution.Info(NewDistribNo - 2).ParamA.Maximum
        Distribution.Info(NewDistribNo - 1).ParamA.Increment = Distribution.Info(NewDistribNo - 2).ParamA.Increment
        Distribution.Info(NewDistribNo - 1).ParamA.Description = Distribution.Info(NewDistribNo - 2).ParamA.Description
        Distribution.Info(NewDistribNo - 1).ParamA.AdjustMin = Distribution.Info(NewDistribNo - 2).ParamA.AdjustMin
        Distribution.Info(NewDistribNo - 1).ParamA.AdjustMax = Distribution.Info(NewDistribNo - 2).ParamA.AdjustMax

        Distribution.Info(NewDistribNo - 1).ParamB.Name = Distribution.Info(NewDistribNo - 2).ParamB.Name
        Distribution.Info(NewDistribNo - 1).ParamB.Symbol = Distribution.Info(NewDistribNo - 2).ParamB.Symbol
        Distribution.Info(NewDistribNo - 1).ParamB.Value = Distribution.Info(NewDistribNo - 2).ParamB.Value
        Distribution.Info(NewDistribNo - 1).ParamB.Type = Distribution.Info(NewDistribNo - 2).ParamB.Type
        Distribution.Info(NewDistribNo - 1).ParamB.NumberType = Distribution.Info(NewDistribNo - 2).ParamB.NumberType
        Distribution.Info(NewDistribNo - 1).ParamB.Minimum = Distribution.Info(NewDistribNo - 2).ParamB.Minimum
        Distribution.Info(NewDistribNo - 1).ParamB.Maximum = Distribution.Info(NewDistribNo - 2).ParamB.Maximum
        Distribution.Info(NewDistribNo - 1).ParamB.Increment = Distribution.Info(NewDistribNo - 2).ParamB.Increment
        Distribution.Info(NewDistribNo - 1).ParamB.Description = Distribution.Info(NewDistribNo - 2).ParamB.Description
        Distribution.Info(NewDistribNo - 1).ParamB.AdjustMin = Distribution.Info(NewDistribNo - 2).ParamB.AdjustMin
        Distribution.Info(NewDistribNo - 1).ParamB.AdjustMax = Distribution.Info(NewDistribNo - 2).ParamB.AdjustMax

        Distribution.Info(NewDistribNo - 1).ParamC.Name = Distribution.Info(NewDistribNo - 2).ParamC.Name
        Distribution.Info(NewDistribNo - 1).ParamC.Symbol = Distribution.Info(NewDistribNo - 2).ParamC.Symbol
        Distribution.Info(NewDistribNo - 1).ParamC.Value = Distribution.Info(NewDistribNo - 2).ParamC.Value
        Distribution.Info(NewDistribNo - 1).ParamC.Type = Distribution.Info(NewDistribNo - 2).ParamC.Type
        Distribution.Info(NewDistribNo - 1).ParamC.NumberType = Distribution.Info(NewDistribNo - 2).ParamC.NumberType
        Distribution.Info(NewDistribNo - 1).ParamC.Minimum = Distribution.Info(NewDistribNo - 2).ParamC.Minimum
        Distribution.Info(NewDistribNo - 1).ParamC.Maximum = Distribution.Info(NewDistribNo - 2).ParamC.Maximum
        Distribution.Info(NewDistribNo - 1).ParamC.Increment = Distribution.Info(NewDistribNo - 2).ParamC.Increment
        Distribution.Info(NewDistribNo - 1).ParamC.Description = Distribution.Info(NewDistribNo - 2).ParamC.Description
        Distribution.Info(NewDistribNo - 1).ParamC.AdjustMin = Distribution.Info(NewDistribNo - 2).ParamC.AdjustMin
        Distribution.Info(NewDistribNo - 1).ParamC.AdjustMax = Distribution.Info(NewDistribNo - 2).ParamC.AdjustMax

        Distribution.Info(NewDistribNo - 1).ParamD.Name = Distribution.Info(NewDistribNo - 2).ParamD.Name
        Distribution.Info(NewDistribNo - 1).ParamD.Symbol = Distribution.Info(NewDistribNo - 2).ParamD.Symbol
        Distribution.Info(NewDistribNo - 1).ParamD.Value = Distribution.Info(NewDistribNo - 2).ParamD.Value
        Distribution.Info(NewDistribNo - 1).ParamD.Type = Distribution.Info(NewDistribNo - 2).ParamD.Type
        Distribution.Info(NewDistribNo - 1).ParamD.NumberType = Distribution.Info(NewDistribNo - 2).ParamD.NumberType
        Distribution.Info(NewDistribNo - 1).ParamD.Minimum = Distribution.Info(NewDistribNo - 2).ParamD.Minimum
        Distribution.Info(NewDistribNo - 1).ParamD.Maximum = Distribution.Info(NewDistribNo - 2).ParamD.Maximum
        Distribution.Info(NewDistribNo - 1).ParamD.Increment = Distribution.Info(NewDistribNo - 2).ParamD.Increment
        Distribution.Info(NewDistribNo - 1).ParamD.Description = Distribution.Info(NewDistribNo - 2).ParamD.Description
        Distribution.Info(NewDistribNo - 1).ParamD.AdjustMin = Distribution.Info(NewDistribNo - 2).ParamD.AdjustMin
        Distribution.Info(NewDistribNo - 1).ParamD.AdjustMax = Distribution.Info(NewDistribNo - 2).ParamD.AdjustMax

        Distribution.Info(NewDistribNo - 1).ParamE.Name = Distribution.Info(NewDistribNo - 2).ParamE.Name
        Distribution.Info(NewDistribNo - 1).ParamE.Symbol = Distribution.Info(NewDistribNo - 2).ParamE.Symbol
        Distribution.Info(NewDistribNo - 1).ParamE.Value = Distribution.Info(NewDistribNo - 2).ParamE.Value
        Distribution.Info(NewDistribNo - 1).ParamE.Type = Distribution.Info(NewDistribNo - 2).ParamE.Type
        Distribution.Info(NewDistribNo - 1).ParamE.NumberType = Distribution.Info(NewDistribNo - 2).ParamE.NumberType
        Distribution.Info(NewDistribNo - 1).ParamE.Minimum = Distribution.Info(NewDistribNo - 2).ParamE.Minimum
        Distribution.Info(NewDistribNo - 1).ParamE.Maximum = Distribution.Info(NewDistribNo - 2).ParamE.Maximum
        Distribution.Info(NewDistribNo - 1).ParamE.Increment = Distribution.Info(NewDistribNo - 2).ParamE.Increment
        Distribution.Info(NewDistribNo - 1).ParamE.Description = Distribution.Info(NewDistribNo - 2).ParamE.Description
        Distribution.Info(NewDistribNo - 1).ParamE.AdjustMin = Distribution.Info(NewDistribNo - 2).ParamE.AdjustMin
        Distribution.Info(NewDistribNo - 1).ParamE.AdjustMax = Distribution.Info(NewDistribNo - 2).ParamE.AdjustMax

        'Distribution.Info(NewDistribNo - 1).PdfInfo.Name = Distribution.Info(NewDistribNo - 2).PdfInfo.Name & "_" & NewDistribNo 'e.g. The first distribution will have the PDF data named PDF_1
        Distribution.Info(NewDistribNo - 1).PdfInfo.Name = "PDF_" & NewDistribNo 'e.g. The first distribution will have the PDF data named PDF_1
        Distribution.Info(NewDistribNo - 1).PdfInfo.Valid = Distribution.Info(NewDistribNo - 2).PdfInfo.Valid
        Distribution.Info(NewDistribNo - 1).PdfInfo.Generate = Distribution.Info(NewDistribNo - 2).PdfInfo.Generate
        Distribution.Info(NewDistribNo - 1).PdfInfo.NumType = Distribution.Info(NewDistribNo - 2).PdfInfo.NumType
        Distribution.Info(NewDistribNo - 1).PdfInfo.Format = Distribution.Info(NewDistribNo - 2).PdfInfo.Format
        Distribution.Info(NewDistribNo - 1).PdfInfo.Alignment = Distribution.Info(NewDistribNo - 2).PdfInfo.Alignment
        Distribution.Info(NewDistribNo - 1).PdfInfo.ValueLabel = Distribution.Info(NewDistribNo - 2).PdfInfo.ValueLabel
        Distribution.Info(NewDistribNo - 1).PdfInfo.Units = Distribution.Info(NewDistribNo - 2).PdfInfo.Units
        Distribution.Info(NewDistribNo - 1).PdfInfo.LabelPrefix = Distribution.Info(NewDistribNo - 2).PdfInfo.LabelPrefix
        Distribution.Info(NewDistribNo - 1).PdfInfo.SeriesLabel = Distribution.Info(NewDistribNo - 2).PdfInfo.SeriesLabel
        Distribution.Info(NewDistribNo - 1).PdfInfo.Legend = Distribution.Info(NewDistribNo - 2).PdfInfo.Legend
        Distribution.Info(NewDistribNo - 1).PdfInfo.Description = Distribution.Info(NewDistribNo - 2).PdfInfo.Description

        'Distribution.Info(NewDistribNo - 1).PdfLnInfo.Name = Distribution.Info(NewDistribNo - 2).PdfLnInfo.Name & "_" & NewDistribNo 'e.g. The first multiple distribution will have the PDF data named PDF_1
        Distribution.Info(NewDistribNo - 1).PdfLnInfo.Name = "PDFLn_" & NewDistribNo 'e.g. The first distribution will have the PDFLn data named PDFLn_1
        Distribution.Info(NewDistribNo - 1).PdfLnInfo.Valid = Distribution.Info(NewDistribNo - 2).PdfLnInfo.Valid
        Distribution.Info(NewDistribNo - 1).PdfLnInfo.Generate = Distribution.Info(NewDistribNo - 2).PdfLnInfo.Generate
        Distribution.Info(NewDistribNo - 1).PdfLnInfo.NumType = Distribution.Info(NewDistribNo - 2).PdfLnInfo.NumType
        Distribution.Info(NewDistribNo - 1).PdfLnInfo.Format = Distribution.Info(NewDistribNo - 2).PdfLnInfo.Format
        Distribution.Info(NewDistribNo - 1).PdfLnInfo.Alignment = Distribution.Info(NewDistribNo - 2).PdfLnInfo.Alignment
        Distribution.Info(NewDistribNo - 1).PdfLnInfo.ValueLabel = Distribution.Info(NewDistribNo - 2).PdfLnInfo.ValueLabel
        Distribution.Info(NewDistribNo - 1).PdfLnInfo.Units = Distribution.Info(NewDistribNo - 2).PdfLnInfo.Units
        Distribution.Info(NewDistribNo - 1).PdfLnInfo.LabelPrefix = Distribution.Info(NewDistribNo - 2).PdfLnInfo.LabelPrefix
        Distribution.Info(NewDistribNo - 1).PdfLnInfo.SeriesLabel = Distribution.Info(NewDistribNo - 2).PdfLnInfo.SeriesLabel
        Distribution.Info(NewDistribNo - 1).PdfLnInfo.Legend = Distribution.Info(NewDistribNo - 2).PdfLnInfo.Legend
        Distribution.Info(NewDistribNo - 1).PdfLnInfo.Description = Distribution.Info(NewDistribNo - 2).PdfLnInfo.Description

        'Distribution.Info(NewDistribNo - 1).PmfInfo.Name = Distribution.Info(NewDistribNo - 2).PmfInfo.Name & "_" & NewDistribNo 'e.g. The first multiple distribution will have the PDF data named PDF_1
        Distribution.Info(NewDistribNo - 1).PmfInfo.Name = "PMF_" & NewDistribNo 'e.g. The first distribution will have the PMF data named PMF_1
        Distribution.Info(NewDistribNo - 1).PmfInfo.Valid = Distribution.Info(NewDistribNo - 2).PmfInfo.Valid
        Distribution.Info(NewDistribNo - 1).PmfInfo.Generate = Distribution.Info(NewDistribNo - 2).PmfInfo.Generate
        Distribution.Info(NewDistribNo - 1).PmfInfo.NumType = Distribution.Info(NewDistribNo - 2).PmfInfo.NumType
        Distribution.Info(NewDistribNo - 1).PmfInfo.Format = Distribution.Info(NewDistribNo - 2).PmfInfo.Format
        Distribution.Info(NewDistribNo - 1).PmfInfo.Alignment = Distribution.Info(NewDistribNo - 2).PmfInfo.Alignment
        Distribution.Info(NewDistribNo - 1).PmfInfo.ValueLabel = Distribution.Info(NewDistribNo - 2).PmfInfo.ValueLabel
        Distribution.Info(NewDistribNo - 1).PmfInfo.Units = Distribution.Info(NewDistribNo - 2).PmfInfo.Units
        Distribution.Info(NewDistribNo - 1).PmfInfo.LabelPrefix = Distribution.Info(NewDistribNo - 2).PmfInfo.LabelPrefix
        Distribution.Info(NewDistribNo - 1).PmfInfo.SeriesLabel = Distribution.Info(NewDistribNo - 2).PmfInfo.SeriesLabel
        Distribution.Info(NewDistribNo - 1).PmfInfo.Legend = Distribution.Info(NewDistribNo - 2).PmfInfo.Legend
        Distribution.Info(NewDistribNo - 1).PmfInfo.Description = Distribution.Info(NewDistribNo - 2).PmfInfo.Description

        'Distribution.Info(NewDistribNo - 1).PmfLnInfo.Name = Distribution.Info(NewDistribNo - 2).PmfLnInfo.Name & "_" & NewDistribNo 'e.g. The first multiple distribution will have the PDF data named PDF_1
        Distribution.Info(NewDistribNo - 1).PmfLnInfo.Name = "PMFLn_" & NewDistribNo 'e.g. The first distribution will have the PMFLn data named PMFLn_1
        Distribution.Info(NewDistribNo - 1).PmfLnInfo.Valid = Distribution.Info(NewDistribNo - 2).PmfLnInfo.Valid
        Distribution.Info(NewDistribNo - 1).PmfLnInfo.Generate = Distribution.Info(NewDistribNo - 2).PmfLnInfo.Generate
        Distribution.Info(NewDistribNo - 1).PmfLnInfo.NumType = Distribution.Info(NewDistribNo - 2).PmfLnInfo.NumType
        Distribution.Info(NewDistribNo - 1).PmfLnInfo.Format = Distribution.Info(NewDistribNo - 2).PmfLnInfo.Format
        Distribution.Info(NewDistribNo - 1).PmfLnInfo.Alignment = Distribution.Info(NewDistribNo - 2).PmfLnInfo.Alignment
        Distribution.Info(NewDistribNo - 1).PmfLnInfo.ValueLabel = Distribution.Info(NewDistribNo - 2).PmfLnInfo.ValueLabel
        Distribution.Info(NewDistribNo - 1).PmfLnInfo.Units = Distribution.Info(NewDistribNo - 2).PmfLnInfo.Units
        Distribution.Info(NewDistribNo - 1).PmfLnInfo.LabelPrefix = Distribution.Info(NewDistribNo - 2).PmfLnInfo.LabelPrefix
        Distribution.Info(NewDistribNo - 1).PmfLnInfo.SeriesLabel = Distribution.Info(NewDistribNo - 2).PmfLnInfo.SeriesLabel
        Distribution.Info(NewDistribNo - 1).PmfLnInfo.Legend = Distribution.Info(NewDistribNo - 2).PmfLnInfo.Legend
        Distribution.Info(NewDistribNo - 1).PmfLnInfo.Description = Distribution.Info(NewDistribNo - 2).PmfLnInfo.Description

        'Distribution.Info(NewDistribNo - 1).CdfInfo.Name = Distribution.Info(NewDistribNo - 2).CdfInfo.Name & "_" & NewDistribNo 'e.g. The first multiple distribution will have the PDF data named PDF_1
        Distribution.Info(NewDistribNo - 1).CdfInfo.Name = "CDF_" & NewDistribNo 'e.g. The first distribution will have the CDF data named CDF_1
        Distribution.Info(NewDistribNo - 1).CdfInfo.Valid = Distribution.Info(NewDistribNo - 2).CdfInfo.Valid
        Distribution.Info(NewDistribNo - 1).CdfInfo.Generate = Distribution.Info(NewDistribNo - 2).CdfInfo.Generate
        Distribution.Info(NewDistribNo - 1).CdfInfo.NumType = Distribution.Info(NewDistribNo - 2).CdfInfo.NumType
        Distribution.Info(NewDistribNo - 1).CdfInfo.Format = Distribution.Info(NewDistribNo - 2).CdfInfo.Format
        Distribution.Info(NewDistribNo - 1).CdfInfo.Alignment = Distribution.Info(NewDistribNo - 2).CdfInfo.Alignment
        Distribution.Info(NewDistribNo - 1).CdfInfo.ValueLabel = Distribution.Info(NewDistribNo - 2).CdfInfo.ValueLabel
        Distribution.Info(NewDistribNo - 1).CdfInfo.Units = Distribution.Info(NewDistribNo - 2).CdfInfo.Units
        Distribution.Info(NewDistribNo - 1).CdfInfo.LabelPrefix = Distribution.Info(NewDistribNo - 2).CdfInfo.LabelPrefix
        Distribution.Info(NewDistribNo - 1).CdfInfo.SeriesLabel = Distribution.Info(NewDistribNo - 2).CdfInfo.SeriesLabel
        Distribution.Info(NewDistribNo - 1).CdfInfo.Legend = Distribution.Info(NewDistribNo - 2).CdfInfo.Legend
        Distribution.Info(NewDistribNo - 1).CdfInfo.Description = Distribution.Info(NewDistribNo - 2).CdfInfo.Description

        Distribution.Info(NewDistribNo - 1).RevCdfInfo.Name = "RevCDF_" & NewDistribNo 'e.g. The first distribution will have the RevCDF data named RevCDF_1
        Distribution.Info(NewDistribNo - 1).RevCdfInfo.Valid = Distribution.Info(NewDistribNo - 2).RevCdfInfo.Valid
        Distribution.Info(NewDistribNo - 1).RevCdfInfo.Generate = Distribution.Info(NewDistribNo - 2).RevCdfInfo.Generate
        Distribution.Info(NewDistribNo - 1).RevCdfInfo.NumType = Distribution.Info(NewDistribNo - 2).RevCdfInfo.NumType
        Distribution.Info(NewDistribNo - 1).RevCdfInfo.Format = Distribution.Info(NewDistribNo - 2).RevCdfInfo.Format
        Distribution.Info(NewDistribNo - 1).RevCdfInfo.Alignment = Distribution.Info(NewDistribNo - 2).RevCdfInfo.Alignment
        Distribution.Info(NewDistribNo - 1).RevCdfInfo.ValueLabel = Distribution.Info(NewDistribNo - 2).RevCdfInfo.ValueLabel
        Distribution.Info(NewDistribNo - 1).RevCdfInfo.Units = Distribution.Info(NewDistribNo - 2).RevCdfInfo.Units
        Distribution.Info(NewDistribNo - 1).RevCdfInfo.LabelPrefix = Distribution.Info(NewDistribNo - 2).RevCdfInfo.LabelPrefix
        Distribution.Info(NewDistribNo - 1).RevCdfInfo.SeriesLabel = Distribution.Info(NewDistribNo - 2).RevCdfInfo.SeriesLabel
        Distribution.Info(NewDistribNo - 1).RevCdfInfo.Legend = Distribution.Info(NewDistribNo - 2).RevCdfInfo.Legend
        Distribution.Info(NewDistribNo - 1).RevCdfInfo.Description = Distribution.Info(NewDistribNo - 2).RevCdfInfo.Description

        'Distribution.Info(NewDistribNo - 1).InvCdfInfo.Name = Distribution.Info(NewDistribNo - 2).InvCdfInfo.Name & "_" & NewDistribNo 'e.g. The first multiple distribution will have the PDF data named PDF_1
        Distribution.Info(NewDistribNo - 1).InvCdfInfo.Name = "InvCDF_" & NewDistribNo 'e.g. The first  distribution will have the InvCDF data named InvCDF_1
        Distribution.Info(NewDistribNo - 1).InvCdfInfo.Valid = Distribution.Info(NewDistribNo - 2).InvCdfInfo.Valid
        Distribution.Info(NewDistribNo - 1).InvCdfInfo.Generate = Distribution.Info(NewDistribNo - 2).InvCdfInfo.Generate
        Distribution.Info(NewDistribNo - 1).InvCdfInfo.NumType = Distribution.Info(NewDistribNo - 2).InvCdfInfo.NumType
        Distribution.Info(NewDistribNo - 1).InvCdfInfo.Format = Distribution.Info(NewDistribNo - 2).InvCdfInfo.Format
        Distribution.Info(NewDistribNo - 1).InvCdfInfo.Alignment = Distribution.Info(NewDistribNo - 2).InvCdfInfo.Alignment
        Distribution.Info(NewDistribNo - 1).InvCdfInfo.ValueLabel = Distribution.Info(NewDistribNo - 2).InvCdfInfo.ValueLabel
        Distribution.Info(NewDistribNo - 1).InvCdfInfo.Units = Distribution.Info(NewDistribNo - 2).InvCdfInfo.Units
        Distribution.Info(NewDistribNo - 1).InvCdfInfo.LabelPrefix = Distribution.Info(NewDistribNo - 2).InvCdfInfo.LabelPrefix
        Distribution.Info(NewDistribNo - 1).InvCdfInfo.SeriesLabel = Distribution.Info(NewDistribNo - 2).InvCdfInfo.SeriesLabel
        Distribution.Info(NewDistribNo - 1).InvCdfInfo.Legend = Distribution.Info(NewDistribNo - 2).InvCdfInfo.Legend
        Distribution.Info(NewDistribNo - 1).InvCdfInfo.Description = Distribution.Info(NewDistribNo - 2).InvCdfInfo.Description

        Distribution.Info(NewDistribNo - 1).InvRevCdfInfo.Name = "InvRevCDF_" & NewDistribNo 'e.g. The first  distribution will have the InvRevCDF data named InvRevCDF_1
        Distribution.Info(NewDistribNo - 1).InvRevCdfInfo.Valid = Distribution.Info(NewDistribNo - 2).InvRevCdfInfo.Valid
        Distribution.Info(NewDistribNo - 1).InvRevCdfInfo.Generate = Distribution.Info(NewDistribNo - 2).InvRevCdfInfo.Generate
        Distribution.Info(NewDistribNo - 1).InvRevCdfInfo.NumType = Distribution.Info(NewDistribNo - 2).InvRevCdfInfo.NumType
        Distribution.Info(NewDistribNo - 1).InvRevCdfInfo.Format = Distribution.Info(NewDistribNo - 2).InvRevCdfInfo.Format
        Distribution.Info(NewDistribNo - 1).InvRevCdfInfo.Alignment = Distribution.Info(NewDistribNo - 2).InvRevCdfInfo.Alignment
        Distribution.Info(NewDistribNo - 1).InvRevCdfInfo.ValueLabel = Distribution.Info(NewDistribNo - 2).InvRevCdfInfo.ValueLabel
        Distribution.Info(NewDistribNo - 1).InvRevCdfInfo.Units = Distribution.Info(NewDistribNo - 2).InvRevCdfInfo.Units
        Distribution.Info(NewDistribNo - 1).InvRevCdfInfo.LabelPrefix = Distribution.Info(NewDistribNo - 2).InvRevCdfInfo.LabelPrefix
        Distribution.Info(NewDistribNo - 1).InvRevCdfInfo.SeriesLabel = Distribution.Info(NewDistribNo - 2).InvRevCdfInfo.SeriesLabel
        Distribution.Info(NewDistribNo - 1).InvRevCdfInfo.Legend = Distribution.Info(NewDistribNo - 2).InvRevCdfInfo.Legend
        Distribution.Info(NewDistribNo - 1).InvRevCdfInfo.Description = Distribution.Info(NewDistribNo - 2).InvRevCdfInfo.Description

        Distribution.Info(NewDistribNo - 1).Suffix = Distribution.Info(NewDistribNo - 2).Suffix

        Distribution.Info(NewDistribNo - 1).ParamEst.SampleSetLabel = Distribution.XValueInfo.ValueLabel
        Distribution.Info(NewDistribNo - 1).ParamEst.SampleSetUnits = Distribution.XValueInfo.Units



        'SelParamSet = NewDistribNo
        SelDistrib = NewDistribNo

        'Generate the distribution data:
        'If Distribution.Data.Tables.Contains("DataTable") Then
        If Distribution.Data.Tables.Contains("Continuous_Data_Table") Then
            Distribution.UpdateData(NewDistribNo) 'Just add the data from the new distribution.
            UpdateDataTableDisplay()
        Else
            Distribution.GenerateData() 'Generate all the data
            UpdateDataTableDisplay()
        End If

    End Sub

    Private Sub AddToChart_Click(sender As Object, e As EventArgs) Handles AddToChart.Click
        'Add the new distribution to any open charts.

        SaveAllOpenCharts() 'Save the changes made to all open charts.

        Dim ShowPdf As Boolean
        Dim ShowPdfLn As Boolean
        Dim ShowPmf As Boolean
        Dim ShowPmfLn As Boolean
        Dim ShowCdf As Boolean
        Dim ShowRevCdf As Boolean
        Dim ShowInvCdf As Boolean
        Dim ShowInvRevCdf As Boolean

        Dim Distrib As DistributionInfo = Distribution.Info(SelDistrib - 1)


        If Distrib.PdfInfo.Valid = True And Distrib.PdfInfo.Generate = True Then ShowPdf = True Else ShowPdf = False
        If Distrib.PdfLnInfo.Valid = True And Distrib.PdfLnInfo.Generate = True Then ShowPdfLn = True Else ShowPdfLn = False
        If Distrib.PmfInfo.Valid = True And Distrib.PmfInfo.Generate = True Then ShowPmf = True Else ShowPmf = False
        If Distrib.PmfLnInfo.Valid = True And Distrib.PmfLnInfo.Generate = True Then ShowPmfLn = True Else ShowPmfLn = False
        If Distrib.CdfInfo.Valid = True And Distrib.CdfInfo.Generate = True Then ShowCdf = True Else ShowCdf = False

        If Distrib.RevCdfInfo.Valid = True And Distrib.RevCdfInfo.Generate = True Then ShowRevCdf = True Else ShowRevCdf = False

        If Distrib.InvCdfInfo.Valid = True And Distrib.InvCdfInfo.Generate = True Then ShowInvCdf = True Else ShowInvCdf = False

        If Distrib.InvRevCdfInfo.Valid = True And Distrib.InvRevCdfInfo.Generate = True Then ShowInvRevCdf = True Else ShowInvRevCdf = False

        'Process all open charts
        Dim I As Integer
        Dim ChartName As String
        Dim ChartInfo As Xml.Linq.XDocument 'Stores the chart information.
        Dim SeriesInfo As IEnumerable(Of XElement)
        Dim AreaInfo As IEnumerable(Of XElement)
        'Dim AreaName() As String 'This will store the list of chart area names.
        Dim AreaName As New List(Of String) 'This will store the list of chart area names.

        For I = 0 To ChartList.Count - 1
            If IsNothing(ChartList(I)) Then
                'The chart form has been closed.
            Else
                ChartName = ChartList(I).ChartName
                ChartInfo = Distribution.ChartList(ChartName)
                SeriesInfo = From item In ChartInfo.<ChartSettings>.<SeriesCollection>.<Series>

                'Get a list of Area Names:
                AreaInfo = From item In ChartInfo.<ChartSettings>.<ChartAreasCollection>.<ChartArea>
                For Each item In AreaInfo
                    AreaName.Add(item.<Name>.Value)
                Next



                If AreaName.Contains("PdfArea") Then
                    'If ShowPdf Then SeriesInfo.Ancestors("SeriesCollection").Nodes.Last.AddAfterSelf(Series(ShowPdf, SelDistrib, "PDF", Distrib.PdfInfo.SeriesLabel, "PdfArea", "Value", Distrib.PdfInfo.Name, Color.Black, Distrib.Display.MarkerColor, Distrib.Display.LineColor, Distribution.Info(0).PdfInfo.Display.MarkerStyle))
                    'If ShowPdf Then SeriesInfo.Ancestors("SeriesCollection").Nodes.Last.AddAfterSelf(Series(ShowPdf, SelDistrib, "PDF", Distrib.Continuity, Distrib.PdfInfo.SeriesLabel, "PdfArea", "Value", Distrib.PdfInfo.Name, Color.Black, Distrib.Display.MarkerColor, Distrib.Display.LineColor, Distribution.Info(0).PdfInfo.Display.MarkerStyle))
                    If ShowPdf Then SeriesInfo.Ancestors("SeriesCollection").Nodes.Last.AddAfterSelf(Series(ShowPdf, SelDistrib, "PDF", Distrib.Continuity, Distrib.PdfInfo.Name, "PdfArea", Distrib.PdfInfo.SeriesLabel, "Value", Distrib.PdfInfo.Name, Color.Black, Distrib.Display.MarkerColor, Distrib.Display.LineColor, Distribution.Info(0).PdfInfo.Display.MarkerStyle))
                End If

                'If ShowPdfLn Then SeriesInfo.Ancestors("SeriesCollection").Nodes.Last.AddAfterSelf(Series(ShowPdfLn, SelParamSet, "PDFLn", Distrib.PdfLnInfo.SeriesLabel, "PdfLnArea", "Value", Distrib.PdfLnInfo.Name, Color.Black, Distrib.Display.MarkerColor, Distrib.Display.LineColor, Main.Distribution.Distrib.PdfLnInfo.Display.MarkerStyle))

                If AreaName.Contains("PdfLnArea") Then
                    'If ShowPdfLn Then SeriesInfo.Ancestors("SeriesCollection").Nodes.Last.AddAfterSelf(Series(ShowPdfLn, SelDistrib, "PDFLn", Distrib.PdfLnInfo.SeriesLabel, "PdfLnArea", "Value", Distrib.PdfLnInfo.Name, Color.Black, Distrib.Display.MarkerColor, Distrib.Display.LineColor, Distribution.Info(0).PdfLnInfo.Display.MarkerStyle))
                    'If ShowPdfLn Then SeriesInfo.Ancestors("SeriesCollection").Nodes.Last.AddAfterSelf(Series(ShowPdfLn, SelDistrib, "PDFLn", Distrib.Continuity, Distrib.PdfLnInfo.SeriesLabel, "PdfLnArea", "Value", Distrib.PdfLnInfo.Name, Color.Black, Distrib.Display.MarkerColor, Distrib.Display.LineColor, Distribution.Info(0).PdfLnInfo.Display.MarkerStyle))
                    If ShowPdfLn Then SeriesInfo.Ancestors("SeriesCollection").Nodes.Last.AddAfterSelf(Series(ShowPdfLn, SelDistrib, "PDFLn", Distrib.Continuity, Distrib.PdfLnInfo.Name, "PdfLnArea", Distrib.PdfLnInfo.SeriesLabel, "Value", Distrib.PdfLnInfo.Name, Color.Black, Distrib.Display.MarkerColor, Distrib.Display.LineColor, Distribution.Info(0).PdfLnInfo.Display.MarkerStyle))
                End If

                'If ShowPmf Then SeriesInfo.Ancestors("SeriesCollection").Nodes.Last.AddAfterSelf(Series(ShowPmf, SelParamSet, "PMF", Distrib.PdfInfo.SeriesLabel, "PmfArea", "Value", Distrib.PmfInfo.Name, Color.Black, Distrib.Display.MarkerColor, Distrib.Display.LineColor, Main.Distribution.Distrib.PmfInfo.Display.MarkerStyle))
                'If ShowPmf Then SeriesInfo.Ancestors("SeriesCollection").Nodes.Last.AddAfterSelf(Series(ShowPmf, SelDistrib, "PMF", Distrib.PdfInfo.SeriesLabel, "PmfArea", "Value", Distrib.PmfInfo.Name, Color.Black, Distrib.Display.MarkerColor, Distrib.Display.LineColor, Distribution.Info(0).PmfInfo.Display.MarkerStyle))

                If AreaName.Contains("PmfArea") Then
                    'If ShowPmf Then SeriesInfo.Ancestors("SeriesCollection").Nodes.Last.AddAfterSelf(Series(ShowPmf, SelDistrib, "PMF", Distrib.PmfInfo.SeriesLabel, "PmfArea", "Value", Distrib.PmfInfo.Name, Color.Black, Distrib.Display.MarkerColor, Distrib.Display.LineColor, Distribution.Info(0).PmfInfo.Display.MarkerStyle))
                    'If ShowPmf Then SeriesInfo.Ancestors("SeriesCollection").Nodes.Last.AddAfterSelf(Series(ShowPmf, SelDistrib, "PMF", Distrib.Continuity, Distrib.PmfInfo.SeriesLabel, "PmfArea", "Value", Distrib.PmfInfo.Name, Color.Black, Distrib.Display.MarkerColor, Distrib.Display.LineColor, Distribution.Info(0).PmfInfo.Display.MarkerStyle))
                    If ShowPmf Then SeriesInfo.Ancestors("SeriesCollection").Nodes.Last.AddAfterSelf(Series(ShowPmf, SelDistrib, "PMF", Distrib.Continuity, Distrib.PmfInfo.Name, "PmfArea", Distrib.PmfInfo.SeriesLabel, "Value", Distrib.PmfInfo.Name, Color.Black, Distrib.Display.MarkerColor, Distrib.Display.LineColor, Distribution.Info(0).PmfInfo.Display.MarkerStyle))
                End If

                If AreaName.Contains("PmfLnArea") Then
                    'If ShowPmfLn Then SeriesInfo.Ancestors("SeriesCollection").Nodes.Last.AddAfterSelf(Series(ShowPmfLn, SelDistrib, "PMFLn", Distrib.PmfLnInfo.SeriesLabel, "PmfLnArea", "Value", Distrib.PmfLnInfo.Name, Color.Black, Distrib.Display.MarkerColor, Distrib.Display.LineColor, Distribution.Info(0).PmfLnInfo.Display.MarkerStyle))
                    'If ShowPmfLn Then SeriesInfo.Ancestors("SeriesCollection").Nodes.Last.AddAfterSelf(Series(ShowPmfLn, SelDistrib, "PMFLn", Distrib.Continuity, Distrib.PmfLnInfo.SeriesLabel, "PmfLnArea", "Value", Distrib.PmfLnInfo.Name, Color.Black, Distrib.Display.MarkerColor, Distrib.Display.LineColor, Distribution.Info(0).PmfLnInfo.Display.MarkerStyle))
                    If ShowPmfLn Then SeriesInfo.Ancestors("SeriesCollection").Nodes.Last.AddAfterSelf(Series(ShowPmfLn, SelDistrib, "PMFLn", Distrib.Continuity, Distrib.PmfLnInfo.Name, "PmfLnArea", Distrib.PmfLnInfo.SeriesLabel, "Value", Distrib.PmfLnInfo.Name, Color.Black, Distrib.Display.MarkerColor, Distrib.Display.LineColor, Distribution.Info(0).PmfLnInfo.Display.MarkerStyle))
                End If

                'If ShowPmfLn Then SeriesInfo.Ancestors("SeriesCollection").Nodes.Last.AddAfterSelf(Series(ShowPmfLn, SelParamSet, "PMFLn", Distrib.PdfInfo.SeriesLabel, "PmfLnArea", "Value", Distrib.PmfLnInfo.Name, Color.Black, Distrib.Display.MarkerColor, Distrib.Display.LineColor, Main.Distribution.Distrib.PmfLnInfo.Display.MarkerStyle))
                'If ShowPmfLn Then SeriesInfo.Ancestors("SeriesCollection").Nodes.Last.AddAfterSelf(Series(ShowPmfLn, SelDistrib, "PMFLn", Distrib.PdfInfo.SeriesLabel, "PmfLnArea", "Value", Distrib.PmfLnInfo.Name, Color.Black, Distrib.Display.MarkerColor, Distrib.Display.LineColor, Distribution.Info(0).PmfLnInfo.Display.MarkerStyle))

                'If ShowCdf Then SeriesInfo.Ancestors("SeriesCollection").Nodes.Last.AddAfterSelf(Series(ShowCdf, SelParamSet, "CDF", Distrib.PdfInfo.SeriesLabel, "CdfArea", "Value", Distrib.CdfInfo.Name, Color.Black, Distrib.Display.MarkerColor, Distrib.Display.LineColor, Main.Distribution.Distrib.CdfInfo.Display.MarkerStyle))
                'If ShowCdf Then SeriesInfo.Ancestors("SeriesCollection").Nodes.Last.AddAfterSelf(Series(ShowCdf, SelDistrib, "CDF", Distrib.PdfInfo.SeriesLabel, "CdfArea", "Value", Distrib.CdfInfo.Name, Color.Black, Distrib.Display.MarkerColor, Distrib.Display.LineColor, Distribution.Info(0).CdfInfo.Display.MarkerStyle))

                If AreaName.Contains("CdfArea") Then
                    'If ShowCdf Then SeriesInfo.Ancestors("SeriesCollection").Nodes.Last.AddAfterSelf(Series(ShowCdf, SelDistrib, "CDF", Distrib.CdfInfo.SeriesLabel, "CdfArea", "Value", Distrib.CdfInfo.Name, Color.Black, Distrib.Display.MarkerColor, Distrib.Display.LineColor, Distribution.Info(0).CdfInfo.Display.MarkerStyle))
                    'If ShowCdf Then SeriesInfo.Ancestors("SeriesCollection").Nodes.Last.AddAfterSelf(Series(ShowCdf, SelDistrib, "CDF", Distrib.Continuity, Distrib.CdfInfo.SeriesLabel, "CdfArea", "Value", Distrib.CdfInfo.Name, Color.Black, Distrib.Display.MarkerColor, Distrib.Display.LineColor, Distribution.Info(0).CdfInfo.Display.MarkerStyle))
                    If ShowCdf Then SeriesInfo.Ancestors("SeriesCollection").Nodes.Last.AddAfterSelf(Series(ShowCdf, SelDistrib, "CDF", Distrib.Continuity, Distrib.CdfInfo.Name, "CdfArea", Distrib.CdfInfo.SeriesLabel, "Value", Distrib.CdfInfo.Name, Color.Black, Distrib.Display.MarkerColor, Distrib.Display.LineColor, Distribution.Info(0).CdfInfo.Display.MarkerStyle))
                End If

                If AreaName.Contains("RevCdfArea") Then
                    'If ShowRevCdf Then SeriesInfo.Ancestors("SeriesCollection").Nodes.Last.AddAfterSelf(Series(ShowRevCdf, SelDistrib, "RevCDF", Distrib.PdfInfo.SeriesLabel, "RevCdfArea", "Value", Distrib.RevCdfInfo.Name, Color.Black, Distrib.Display.MarkerColor, Distrib.Display.LineColor, Distribution.Info(0).RevCdfInfo.Display.MarkerStyle))
                    'If ShowRevCdf Then SeriesInfo.Ancestors("SeriesCollection").Nodes.Last.AddAfterSelf(Series(ShowRevCdf, SelDistrib, "RevCDF", Distrib.Continuity, Distrib.PdfInfo.SeriesLabel, "RevCdfArea", "Value", Distrib.RevCdfInfo.Name, Color.Black, Distrib.Display.MarkerColor, Distrib.Display.LineColor, Distribution.Info(0).RevCdfInfo.Display.MarkerStyle))
                    'If ShowRevCdf Then SeriesInfo.Ancestors("SeriesCollection").Nodes.Last.AddAfterSelf(Series(ShowRevCdf, SelDistrib, "RevCDF", Distrib.Continuity, Distrib.PdfInfo.SeriesLabel, "RevCdfArea", "Value", Distrib.RevCdfInfo.Name, Color.Black, Distrib.Display.MarkerColor, Distrib.Display.LineColor, Distribution.Info(0).RevCdfInfo.Display.MarkerStyle))
                    If ShowRevCdf Then SeriesInfo.Ancestors("SeriesCollection").Nodes.Last.AddAfterSelf(Series(ShowRevCdf, SelDistrib, "RevCDF", Distrib.Continuity, Distrib.PdfInfo.Name, "RevCdfArea", Distrib.PdfInfo.SeriesLabel, "Value", Distrib.RevCdfInfo.Name, Color.Black, Distrib.Display.MarkerColor, Distrib.Display.LineColor, Distribution.Info(0).RevCdfInfo.Display.MarkerStyle))
                End If

                'If ShowInvCdf Then SeriesInfo.Ancestors("SeriesCollection").Nodes.Last.AddAfterSelf(Series(ShowInvCdf, SelParamSet, "InvCDF", Distrib.PdfInfo.SeriesLabel, "InvCdfArea", "Value", Distrib.InvCdfInfo.Name, Color.Black, Distrib.Display.MarkerColor, Distrib.Display.LineColor, Main.Distribution.Distrib.InvCdfInfo.Display.MarkerStyle))
                'If ShowInvCdf Then SeriesInfo.Ancestors("SeriesCollection").Nodes.Last.AddAfterSelf(Series(ShowInvCdf, SelParamSet, "InvCDF", Distrib.PdfInfo.SeriesLabel, "InvCdfArea", "Probability", Distrib.InvCdfInfo.Name, Color.Black, Distrib.Display.MarkerColor, Distrib.Display.LineColor, Main.Distribution.Distrib.InvCdfInfo.Display.MarkerStyle))
                'If ShowInvCdf Then SeriesInfo.Ancestors("SeriesCollection").Nodes.Last.AddAfterSelf(Series(ShowInvCdf, SelDistrib, "InvCDF", Distrib.PdfInfo.SeriesLabel, "InvCdfArea", "Probability", Distrib.InvCdfInfo.Name, Color.Black, Distrib.Display.MarkerColor, Distrib.Display.LineColor, Distribution.Info(0).InvCdfInfo.Display.MarkerStyle))

                If AreaName.Contains("InvCdfArea") Then
                    'If ShowInvCdf Then SeriesInfo.Ancestors("SeriesCollection").Nodes.Last.AddAfterSelf(Series(ShowInvCdf, SelDistrib, "InvCDF", Distrib.InvCdfInfo.SeriesLabel, "InvCdfArea", "Probability", Distrib.InvCdfInfo.Name, Color.Black, Distrib.Display.MarkerColor, Distrib.Display.LineColor, Distribution.Info(0).InvCdfInfo.Display.MarkerStyle))
                    'If ShowInvCdf Then SeriesInfo.Ancestors("SeriesCollection").Nodes.Last.AddAfterSelf(Series(ShowInvCdf, SelDistrib, "InvCDF", Distrib.Continuity, Distrib.InvCdfInfo.SeriesLabel, "InvCdfArea", "Probability", Distrib.InvCdfInfo.Name, Color.Black, Distrib.Display.MarkerColor, Distrib.Display.LineColor, Distribution.Info(0).InvCdfInfo.Display.MarkerStyle))
                    If ShowInvCdf Then SeriesInfo.Ancestors("SeriesCollection").Nodes.Last.AddAfterSelf(Series(ShowInvCdf, SelDistrib, "InvCDF", Distrib.Continuity, Distrib.InvCdfInfo.Name, "InvCdfArea", Distrib.InvCdfInfo.SeriesLabel, "Probability", Distrib.InvCdfInfo.Name, Color.Black, Distrib.Display.MarkerColor, Distrib.Display.LineColor, Distribution.Info(0).InvCdfInfo.Display.MarkerStyle))
                End If

                If AreaName.Contains("InvRevCdfArea") Then
                    'If ShowInvRevCdf Then SeriesInfo.Ancestors("SeriesCollection").Nodes.Last.AddAfterSelf(Series(ShowInvRevCdf, SelDistrib, "InvRevCDF", Distrib.InvRevCdfInfo.SeriesLabel, "InvRevCdfArea", "Probability", Distrib.InvRevCdfInfo.Name, Color.Black, Distrib.Display.MarkerColor, Distrib.Display.LineColor, Distribution.Info(0).InvRevCdfInfo.Display.MarkerStyle))
                    'If ShowInvRevCdf Then SeriesInfo.Ancestors("SeriesCollection").Nodes.Last.AddAfterSelf(Series(ShowInvRevCdf, SelDistrib, "InvRevCDF", Distrib.Continuity, Distrib.InvRevCdfInfo.SeriesLabel, "InvRevCdfArea", "Probability", Distrib.InvRevCdfInfo.Name, Color.Black, Distrib.Display.MarkerColor, Distrib.Display.LineColor, Distribution.Info(0).InvRevCdfInfo.Display.MarkerStyle))
                    If ShowInvRevCdf Then SeriesInfo.Ancestors("SeriesCollection").Nodes.Last.AddAfterSelf(Series(ShowInvRevCdf, SelDistrib, "InvRevCDF", Distrib.Continuity, Distrib.InvRevCdfInfo.Name, "InvRevCdfArea", Distrib.InvRevCdfInfo.SeriesLabel, "Probability", Distrib.InvRevCdfInfo.Name, Color.Black, Distrib.Display.MarkerColor, Distrib.Display.LineColor, Distribution.Info(0).InvRevCdfInfo.Display.MarkerStyle))
                End If

                'Main.Distribution.ChartList(ChartName) = SeriesInfo 'Save the updated ChartInfo
                Distribution.ChartList(ChartName) = ChartInfo 'Save the updated ChartInfo
                    Distribution.Modified = True
                    ChartList(I).Plot() 'Replot the updated chart
                    ChartList(I).ReloadChartSettings()  'Reload the ChartInfo in any open ChartSettings form
                End If
        Next

    End Sub

    'Private Function Series(Exists As Boolean, DistribNo As Integer, FunctionType As String, SeriesName As String, ChartAreaName As String, XFieldName As String, YFieldName As String, BorderColor As Color, FillColor As Color, LineColor As Color, MarkerStyle As String) As IEnumerable(Of XElement)
    'Private Function Series(Exists As Boolean, DistribNo As Integer, FunctionType As String, Continuity As String, SeriesName As String, ChartAreaName As String, XFieldName As String, YFieldName As String, BorderColor As Color, FillColor As Color, LineColor As Color, MarkerStyle As String) As IEnumerable(Of XElement)
    Private Function Series(Exists As Boolean, DistribNo As Integer, FunctionType As String, Continuity As String, SeriesName As String, ChartAreaName As String, LegendText As String, XFieldName As String, YFieldName As String, BorderColor As Color, FillColor As Color, LineColor As Color, MarkerStyle As String) As IEnumerable(Of XElement)
        'Generate a ChartInfo Series from the specified XFieldName and YFieldName.
        'DistribNo is 0 for the primary distribution.
        'DistribNo is 1, 2, ... for the secondary distributions.
        'FunctionType is PDF, PDFLn, PMF, PMFLn, CDF or InvCDF.

        Dim ChartType As String
        If Continuity = "Discrete" Then
        ChartType = "Column"
        Else
        ChartType = "Line"
        End If

        If Exists Then
            Dim XDoc = <?xml version="1.0" encoding="utf-8"?>
                       <Series>
                           <DistribNo><%= DistribNo %></DistribNo>
                           <FunctionType><%= FunctionType %></FunctionType>
                           <Continuity><%= Continuity %></Continuity>
                           <Name><%= SeriesName %></Name>
                           <ChartType><%= ChartType %></ChartType>
                           <ChartArea><%= ChartAreaName %></ChartArea>
                           <Legend><%= ChartAreaName %></Legend>
                           <LegendText><%= LegendText %></LegendText>
                           <AxisLabel/>
                           <XFieldName><%= XFieldName %></XFieldName>
                           <XAxisType>Primary</XAxisType>
                           <XValueType>Auto</XValueType>
                           <YFieldName><%= YFieldName %></YFieldName>
                           <YAxisType>Primary</YAxisType>
                           <YValueType>Auto</YValueType>
                           <Marker>
                               <BorderColor><%= BorderColor.ToArgb.ToString %></BorderColor>
                               <BorderWidth>1</BorderWidth>
                               <Fill>Yes</Fill>
                               <Color><%= FillColor.ToArgb.ToString %></Color>
                               <Size>6</Size>
                               <Step>1</Step>
                               <Style><%= MarkerStyle %></Style>
                           </Marker>
                           <Color><%= LineColor.ToArgb.ToString %></Color>
                           <Width>1</Width>
                           <ToolTip/>
                       </Series>
            '<Legend>Legend1</Legend>
            '<ChartType>Line</ChartType>
            Return XDoc.<Series>
        Else
            'This series does not exist. Return nothing.
        End If
    End Function

    Private Sub cmbTableName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTableName.SelectedIndexChanged
        'A different table has been selected for display.
        If cmbTableName.Focused Then
            SelTableName = cmbTableName.SelectedItem.ToString
        Else
            'Dont update the view if the selection has been made programmatically.
        End If
    End Sub

    Private Sub btnGenerateSamples_Click(sender As Object, e As EventArgs) Handles btnGenerateSamples.Click
        'Generate Distribution samples.

        Try
            Dim NSamples As Integer = txtNDistribSamples.Text
            Dim TableName As String = txtSampleTableName.Text.Trim.Replace(" ", "_")
            If Distribution.Data.Tables.Contains(TableName) Then

            Else
                cmbTableName.Items.Add(TableName)
            End If
            Distribution.GenerateDistribSamples(Distribution.Info(SelDistrib - 1), NSamples, TableName)
        Catch ex As Exception
            Message.AddWarning(ex.Message & vbCrLf)
        End Try

    End Sub

    Private Sub btnCopySel_Click(sender As Object, e As EventArgs) Handles btnCopySel.Click
        'Copy the selected range to the clipboard.

        'MCTableName contains the name of the displayed table.
        If Distribution.Data.Tables.Contains(SelTableName) Then
            'Find the range of rows and columns to copy:
            Dim ThisRow As Integer
            Dim ThisCol As Integer
            Dim FirstRow As Integer = dgvDataTable.SelectedCells(0).RowIndex
            Dim FirstCol As Integer = dgvDataTable.SelectedCells(0).ColumnIndex
            Dim LastRow As Integer = FirstRow
            Dim LastCol As Integer = FirstCol

            For Each Item In dgvDataTable.SelectedCells
                ThisRow = Item.RowIndex
                ThisCol = Item.ColumnIndex
                If ThisRow < FirstRow Then FirstRow = ThisRow
                If ThisRow > LastRow Then LastRow = ThisRow
                If ThisCol < FirstCol Then FirstCol = ThisCol
                If ThisCol > LastCol Then LastCol = ThisCol
            Next

            Dim strCopy As String = ""
            Dim RowNo As Integer
            Dim ColNo As Integer
            Dim myTable As DataTable = Distribution.Data.Tables(SelTableName)
            For RowNo = FirstRow To LastRow
                For ColNo = FirstCol To LastCol
                    If myTable.Rows(RowNo).Item(ColNo) IsNot Nothing Then
                        strCopy &= myTable.Rows(RowNo).Item(ColNo).ToString
                    End If
                    strCopy &= Chr(Keys.Tab)
                Next
                strCopy = strCopy.Substring(0, strCopy.Length - 1)
                strCopy &= Environment.NewLine
            Next

            Dim DataObj As New DataObject
            DataObj.SetText(strCopy)
            Clipboard.SetDataObject(DataObj, True)
        Else
            Message.AddWarning("The data table was not found: " & SelTableName & vbCrLf)
        End If

    End Sub

    Private Sub btnDistribAnalysis_Click(sender As Object, e As EventArgs) Handles btnDistribAnalysis.Click
        'Open a DistribAnalysis  form.

        Dim FormNo As Integer = OpenNewDistribAnalysis()
    End Sub

    Private Sub btnGenSamples_Click(sender As Object, e As EventArgs) Handles btnGenSamples.Click
        'Open a SampleSingleValue  form.

        Dim FormNo As Integer = OpenNewGenerateSamples()
        'SampleSingleValueList(FormNo).RestoreFormSettings 'Restore the form settings after the FormNo is set.

    End Sub

    Public Sub DisableGridViews()
        Dim I As Integer
        For I = 0 To TableList.Count - 1
            If IsNothing(TableList(I)) Then
                'This chart form has been closed.
            Else
                'TableList(I).dgvResults.Enabled = False
                TableList(I).DisableGridView
            End If
        Next
    End Sub

    Public Sub EnableGridViews()
        Dim I As Integer
        For I = 0 To TableList.Count - 1
            If IsNothing(TableList(I)) Then
                'This chart form has been closed.
            Else
                'TableList(I).dgvResults.Enabled = True
                TableList(I).EnableGridView
            End If
        Next
    End Sub

    Private Sub btnUpdateSampling_Click(sender As Object, e As EventArgs) Handles btnUpdateSampling.Click
        'Update the distribution sampling based on the distribution parameters.
        UpdateSampling(99.9, 100) 'Update the sampling to include 99.9% of the distribution probability using 100 samples.
    End Sub

    Private Sub UpdateSampling(RangeProbPercent As Single, ApproxNSamples As Integer, DistribNo As Integer)
        'Update the distribution sampling using the selected Distribution Number.
        'DistribNo is the (one based) distribution number.
        'RangeProbPercent is the probability percentage of the distribution to include in the range.
        'ApproxNSamples the the approximate number of samples to use.

        Dim RangeProb As Double = RangeProbPercent / 100
        Dim RangeProbMin As Double = (1 - RangeProb) / 2
        Dim RangeProbMax As Double = RangeProbMin + RangeProb

        Dim RawMinimum As Double = Distribution.Info(DistribNo - 1).InvCDF(RangeProbMin) 'The minimum random variable value. The probability that the random variable lies between minimum and maximum is RangeProb.
        Dim RawMaximum As Double = Distribution.Info(DistribNo - 1).InvCDF(RangeProbMax) 'The maximum random variable value. The probability that the random variable lies between minimum and maximum is RangeProb.
        Dim RawInterval As Double = (RawMaximum - RawMinimum) / (ApproxNSamples - 1)

        Dim PrefInterval As Double = PreferredInterval(RawInterval)
        Dim PrefMinimum As Double = Math.Floor(RawMinimum / PrefInterval) * PrefInterval
        Dim PrefMaximum As Double = Math.Ceiling(RawMaximum / PrefInterval) * PrefInterval
        Dim NSamples As Integer = Math.Round((PrefMaximum - PrefMinimum) / PrefInterval) + 1

        Distribution.ContSampling.Minimum = PrefMinimum
        Distribution.ContSampling.Maximum = PrefMaximum
        Distribution.ContSampling.Interval = PrefInterval
        Distribution.ContSampling.NSamples = NSamples

        txtMinValue.Text = PrefMinimum
        txtMaxValue.Text = PrefMaximum
        txtSampleInt.Text = PrefInterval
        txtNSamples.Text = NSamples

    End Sub

    Private Sub UpdateSampling(RangeProbPercent As Single, ApproxNSamples As Integer)
        'Update the distribution sampling using all of the Distributions.
        'RangeProbPercent is the probability percentage of the distribution to include in the range.
        'ApproxNSamples the the approximate number of samples to use.

        If Distribution.Info.Count > 0 Then
            Dim RangeProb As Double = RangeProbPercent / 100
            Dim RangeProbMin As Double = (1 - RangeProb) / 2
            Dim RangeProbMax As Double = RangeProbMin + RangeProb

            Dim RawMinimum As Double = Distribution.Info(0).InvCDF(RangeProbMin) 'The minimum random variable value. The probability that the random variable lies between minimum and maximum is RangeProb.
            Dim RawMaximum As Double = Distribution.Info(0).InvCDF(RangeProbMax) 'The maximum random variable value. The probability that the random variable lies between minimum and maximum is RangeProb.
            Dim RawInterval As Double = (RawMaximum - RawMinimum) / (ApproxNSamples - 1)

            Dim NextMin As Double
            Dim NextMax As Double
            Dim I As Integer
            For I = 1 To Distribution.Info.Count - 1
                NextMin = Distribution.Info(I).InvCDF(RangeProbMin)
                NextMax = Distribution.Info(I).InvCDF(RangeProbMax)
                If NextMin < RawMinimum Then RawMinimum = NextMin
                If NextMax > RawMaximum Then RawMaximum = NextMax
            Next

            Dim PrefInterval As Double = PreferredInterval(RawInterval)
            Dim PrefMinimum As Double = Math.Floor(RawMinimum / PrefInterval) * PrefInterval
            Dim PrefMaximum As Double = Math.Ceiling(RawMaximum / PrefInterval) * PrefInterval
            Dim NSamples As Integer = Math.Round((PrefMaximum - PrefMinimum) / PrefInterval) + 1

            Distribution.ContSampling.Minimum = PrefMinimum
            Distribution.ContSampling.Maximum = PrefMaximum
            Distribution.ContSampling.Interval = PrefInterval
            Distribution.ContSampling.NSamples = NSamples

            txtMinValue.Text = PrefMinimum
            txtMaxValue.Text = PrefMaximum
            txtSampleInt.Text = PrefInterval
            txtNSamples.Text = NSamples

        Else
            Message.AddWarning("No distributions are defined." & vbCrLf)
        End If

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
            Message.AddWarning("There are no preferred coefficents in the list." & vbCrLf)
            Return RawCoeff
        End If
    End Function


#End Region 'Form Methods ---------------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Form Events - Events raised by this form." '=========================================================================================================================================

#End Region 'Form Events ----------------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Form Classes - Other classes used by this form." '===================================================================================================================================

    Public Class clsSendMessageParams
        'Parameters used when sending a message using the Message Service.
        Public ProjectNetworkName As String
        Public ConnectionName As String
        Public Message As String
    End Class

    Public Class clsInstructionParams
        'Parameters used when executing an instruction.
        Public Info As String 'The information in an instruction.
        Public Locn As String 'The location to send the information.
    End Class

    Public Class clsDistribInfo
        'Information about a distribution.

        ''Parameters chosen for the selected distribution. Available distribution have a maximum of five parameters.
        ''Separate variables are used so the code that uses them is a little briefer and clearer.
        'Public ParamA As Double
        'Public ParamB As Double
        'Public ParamC As Double
        'Public ParamD As Double
        'Public ParamE As Double

        'Parameter information:
        Public ParamAInfo As New ParamInfo
        Public ParamBInfo As New ParamInfo
        Public ParamCInfo As New ParamInfo
        Public ParamDInfo As New ParamInfo
        Public ParamEInfo As New ParamInfo


        Private _name As String = "" 'The name of the distribution.
        Property Name As String
            Get
                Return _name
            End Get
            Set(value As String)
                _name = value
            End Set
        End Property

        Private _value As Double 'The value of the parameter.
        Property Value As Double
            Get
                Return _value
            End Get
            Set(value As Double)
                _value = value
                'RaiseEvent ValueChanged()
            End Set
        End Property

        Private _description As String = "" 'A description of the distribution.
        Property Description As String
            Get
                Return _description
            End Get
            Set(value As String)
                _description = value
            End Set
        End Property

        Private _usage As String = "" 'Teh use of the distribution.
        Property Usage As String
            Get
                Return _usage
            End Get
            Set(value As String)
                _usage = value
            End Set
        End Property

        Private _continuity As String = "" 'The continuity of the distribution. (Continuous or Discrete)
        Property Continuity As String
            Get
                Return _continuity
            End Get
            Set(value As String)
                _continuity = value
            End Set
        End Property

        Private _nParams As Integer = 1 'The number of parameters used to specify the distribution. 
        Property NParams As Integer
            Get
                Return _nParams
            End Get
            Set(value As Integer)
                _nParams = value

            End Set
        End Property

        Private _rangeMin As String 'Descrbes the minimum range of valid distribution values.
        Property RangeMin As String
            Get
                Return _rangeMin
            End Get
            Set(value As String)
                _rangeMin = value
            End Set
        End Property

        Private _rangeMax As String 'Descrbes the maximum range of valid distribution values.
        Property RangeMax As String
            Get
                Return _rangeMax
            End Get
            Set(value As String)
                _rangeMax = value
            End Set
        End Property

        Private _pdfValid As Boolean = False 'If True, the PDF data type is valid for this distribution.
        Property PdfValid As Boolean
            Get
                Return _pdfValid
            End Get
            Set(value As Boolean)
                _pdfValid = value
            End Set
        End Property

        Private _pdfLnValid As Boolean = False 'If True, the PDFLn data type is valid for this distribution.
        Property PdfLnValid As Boolean
            Get
                Return _pdfLnValid
            End Get
            Set(value As Boolean)
                _pdfLnValid = value
            End Set
        End Property

        Private _pmfValid As Boolean = False 'If True, the PMF data type is valid for this distribution.
        Property PmfValid As Boolean
            Get
                Return _pmfValid
            End Get
            Set(value As Boolean)
                _pmfValid = value
            End Set
        End Property

        Private _pmfLnValid As Boolean = False 'If True, the PMFLn data type is valid for this distribution.
        Property PmfLnValid As Boolean
            Get
                Return _pmfLnValid
            End Get
            Set(value As Boolean)
                _pmfLnValid = value
            End Set
        End Property

        Private _cdfValid As Boolean = False 'If True, the CDF data type is valid for this distribution.
        Property CdfValid As Boolean
            Get
                Return _cdfValid
            End Get
            Set(value As Boolean)
                _cdfValid = value
            End Set
        End Property

        Private _invCdfValid As Boolean = False 'If True, the InvCDF data type is valid for this distribution.
        Property InvCdfValid As Boolean
            Get
                Return _invCdfValid
            End Get
            Set(value As Boolean)
                _invCdfValid = value
            End Set
        End Property

    End Class 'clsDistribInfo

    Private Sub txtSuffix_TextChanged(sender As Object, e As EventArgs) Handles txtSuffix.TextChanged

    End Sub



























































#End Region 'Form Classes ---------------------------------------------------------------------------------------------------------------------------------------------------------------------


End Class


