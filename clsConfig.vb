Option Strict On
Option Explicit On
Imports System.Xml
Imports System.String
Imports System.Windows.Forms
Imports System.IO


Module clsConfig

    Class XmlFileHandler
        ' file path for xml config, and file constants
        Private Const c_stXmlFileName As String = "RMMConfig.xml"
        Private Const c_stXmlProcDefFileName As String = "RMMProcDefs.xml"
        Private Const c_stXmlVersion As String = "100"
        ' nodes
        Private Const c_stNodeRoot As String = "/root/"
        Private Const c_stNodeVersion As String = "Version"
        Private Const c_stNodeMainForm As String = c_stNodeRoot & "MainForm"
        Private Const c_stNodeUserOptions As String = c_stNodeRoot & "UserOptions"
        Private Const c_stNodeColWidth As String = c_stNodeRoot & "lvmColWidth"
        Private Const c_stNodeProcDef As String = c_stNodeRoot & "ProcDefs/"
        Private Const c_stElementProcDef As String = "_p"
        Private Const c_stElementProcName As String = "Name"
        ' main form
        Private Const c_stLeft As String = "Left"
        Private Const c_stTop As String = "Top"
        Private Const c_stWidth As String = "Width"
        Private Const c_stHeight As String = "Height"
        ' user options
        Private Const c_stRMMFilePath As String = "RMMFilePath"
        Private Const c_stAutoScan As String = "AutoScan"
        Private Const c_stListView As String = "ListView"
        Private Const c_stSearchType As String = "SearchType"
        Private Const c_stInfoHeight As String = "InfoHeight"
        Private Const c_stTreeWidth As String = "TreeWidth"
        Private Const c_stLastSearch As String = "LastSearch"
        Private Const c_stBalloons As String = "Balloons"
        ' column widths
        Private Const c_stColWidthPrefix As String = "_c"
        Private Const c_stValue As String = "Value"
        Private Const c_stFmt As String = "{0:D2}"
        Private m_procDoc As XmlDocument
        Private m_IX As Integer

        Public Function LoadConfiguration(ByRef mfrm As frmMain) As Boolean
            Dim ConfigData As New XmlDocument
            Dim cfgNode As XmlNode
            Dim xReader As XmlNodeReader
            Dim stPath As String
            Dim ix, d As Integer
            LoadConfiguration = True
            Try
                With My.Computer.FileSystem
                    stPath = Path.Combine(.SpecialDirectories.CurrentUserApplicationData, c_stXmlFileName)
                    If Not .GetFileInfo(stPath).Exists Or Not GetXmlVersion(stPath) Then
                        .CopyFile(Path.Combine(My.Application.Info.DirectoryPath, c_stXmlFileName), stPath, True)
                        Dim fInfo As FileInfo = .GetFileInfo(stPath)
                        fInfo.Attributes = fInfo.Attributes And Not FileAttributes.ReadOnly
                    End If
                End With
                ConfigData.Load(stPath)
                cfgNode = ConfigData.DocumentElement.SelectSingleNode(c_stNodeMainForm)
                If cfgNode IsNot Nothing Then
                    xReader = New XmlNodeReader(cfgNode)
                    With xReader
                        Do While (.Read())
                            If .NodeType = XmlNodeType.Element Then
                                Select Case .Name
                                    Case c_stLeft
                                        d = CInt(.ReadString())
                                        mfrm.Left = CInt(IIf(d <= 0, 100, d))
                                    Case c_stTop
                                        d = CInt(.ReadString())
                                        mfrm.Top = CInt(IIf(d <= 0, 100, d))
                                    Case c_stWidth
                                        d = CInt(.ReadString())
                                        mfrm.Width = CInt(IIf(d <= 0, CInt(SystemInformation.PrimaryMonitorSize.Width / 2), d))
                                    Case c_stHeight
                                        d = CInt(.ReadString())
                                        mfrm.Height = CInt(IIf(d <= 0, CInt(SystemInformation.PrimaryMonitorSize.Height / 2), d))
                                End Select
                            End If
                        Loop
                        .Close()
                    End With
                End If

                cfgNode = ConfigData.DocumentElement.SelectSingleNode(c_stNodeUserOptions)
                If cfgNode IsNot Nothing Then
                    xReader = New XmlNodeReader(cfgNode)
                    With xReader
                        Do While (.Read())
                            If .NodeType = XmlNodeType.Element Then
                                Select Case .Name
                                    Case c_stAutoScan
                                        uOpt.AutoScan = CBool(.ReadString())
                                    Case c_stListView
                                        uOpt.ListView = CType([Enum].Parse(GetType(View), .ReadString()), View)
                                    Case c_stSearchType
                                        uOpt.SearchType = .ReadString()
                                    Case c_stRMMFilePath
                                        uOpt.RMMFilePath = .ReadString()
                                    Case c_stInfoHeight
                                        uOpt.infoHeight = CInt(.ReadString())
                                    Case c_stTreeWidth
                                        uOpt.treeWidth = CInt(.ReadString())
                                    Case c_stLastSearch
                                        uOpt.lastSearch = .ReadString()
                                    Case c_stBalloons
                                        uOpt.bBalloons = CBool(.ReadString())
                                End Select
                            End If
                        Loop
                        .Close()
                    End With
                End If
                cfgNode = ConfigData.DocumentElement.SelectSingleNode(c_stNodeColWidth)
                If cfgNode IsNot Nothing Then
                    xReader = New XmlNodeReader(cfgNode)
                    With xReader
                        Do While (.Read())
                            If .NodeType = XmlNodeType.Element Then
                                If .Name.Substring(0, 2) = c_stColWidthPrefix Then
                                    ix = System.Math.Abs(CInt(.Name.Substring(2, 2)) - 1)
                                    .ReadToFollowing(c_stValue)
                                    If ix <= UBound(lvmColWidth) And .Name = c_stValue Then
                                        lvmColWidth(ix) = CInt(.ReadString())
                                    End If
                                End If
                            End If
                        Loop
                        .Close()
                    End With
                End If
            Catch ex As Exception
                LoadConfiguration = False
                MessageBox.Show(ex.Message)
            End Try
        End Function

        Public Function SaveConfiguration(ByRef mfrm As frmMain) As Boolean
            Dim ConfigData As New XmlDocument
            Dim cfgNode As XmlNode
            Dim xReader As XmlNodeReader
            Dim ix As Integer
            Dim stPath As String = Path.Combine(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData, c_stXmlFileName)
            Dim stNode As String = ""
            SaveConfiguration = True
            Try
                If GetXmlVersion(stPath) Then
                    ConfigData.Load(stPath)
                Else
                    SaveConfiguration = False
                    Exit Function
                End If
                stNode = c_stNodeMainForm
                cfgNode = ConfigData.DocumentElement.SelectSingleNode(stNode)
                If cfgNode IsNot Nothing Then
                    xReader = New XmlNodeReader(cfgNode)
                    With xReader
                        Do While (.Read())
                            If .NodeType = XmlNodeType.Element Then
                                cfgNode = ConfigData.SelectSingleNode(stNode & "/" & .Name)
                                Select Case .Name
                                    Case c_stLeft
                                        cfgNode.InnerText = CStr(mfrm.Left)
                                    Case c_stTop
                                        cfgNode.InnerText = CStr(mfrm.Top)
                                    Case c_stWidth
                                        cfgNode.InnerText = CStr(mfrm.Width)
                                    Case c_stHeight
                                        cfgNode.InnerText = CStr(mfrm.Height)
                                End Select
                            End If
                        Loop
                        .Close()
                    End With
                End If
                stNode = c_stNodeUserOptions
                cfgNode = ConfigData.DocumentElement.SelectSingleNode(stNode)
                If cfgNode IsNot Nothing Then
                    xReader = New XmlNodeReader(cfgNode)
                    With xReader
                        Do While (.Read())
                            If .NodeType = XmlNodeType.Element Then
                                cfgNode = ConfigData.SelectSingleNode(stNode & "/" & .Name)
                                Select Case .Name
                                    Case c_stRMMFilePath
                                        cfgNode.InnerText = uOpt.RMMFilePath
                                    Case c_stListView
                                        cfgNode.InnerText = uOpt.ListView.ToString
                                    Case c_stAutoScan
                                        cfgNode.InnerText = uOpt.AutoScan.ToString
                                    Case c_stSearchType
                                        cfgNode.InnerText = uOpt.SearchType
                                    Case c_stInfoHeight
                                        cfgNode.InnerText = uOpt.infoHeight.ToString
                                    Case c_stTreeWidth
                                        cfgNode.InnerText = uOpt.treeWidth.ToString
                                    Case c_stLastSearch
                                        cfgNode.InnerText = uOpt.lastSearch
                                    Case c_stBalloons
                                        cfgNode.InnerText = uOpt.bBalloons.ToString
                                End Select
                            End If
                        Loop
                        .Close()
                    End With
                End If
                stNode = c_stNodeColWidth
                cfgNode = ConfigData.DocumentElement.SelectSingleNode(stNode)
                If cfgNode IsNot Nothing Then
                    xReader = New XmlNodeReader(cfgNode)
                    With xReader
                        Do While (xReader.Read())
                            If .NodeType = XmlNodeType.Element Then
                                If .Name.Substring(0, 2) = c_stColWidthPrefix Then
                                    ix = System.Math.Abs(CInt(.Name.Substring(2, 2)) - 1)
                                    .ReadToFollowing(c_stValue)
                                    If ix <= UBound(lvmColWidth) And .Name = c_stValue Then
                                        cfgNode = ConfigData.SelectSingleNode(stNode & "/" & c_stColWidthPrefix & Format(c_stFmt, ix + 1) & "/" & .Name)
                                        If cfgNode IsNot Nothing Then cfgNode.InnerText = lvmColWidth(ix).ToString
                                    End If
                                End If
                            End If
                        Loop
                        .Close()
                    End With
                End If
                ConfigData.Save(stPath)
            Catch ex As Exception
                SaveConfiguration = False
                MessageBox.Show(ex.Message)
            End Try
        End Function

        Private Function GetXmlVersion(ByRef stFile As String) As Boolean
            Dim ConfigData As New XmlDocument
            Dim cfgNode As XmlNode
            Dim xReader As XmlNodeReader
            GetXmlVersion = False
            Try
                ConfigData.Load(stFile)
                cfgNode = ConfigData.DocumentElement.SelectSingleNode(c_stNodeRoot & c_stNodeVersion)
                If cfgNode IsNot Nothing Then
                    xReader = New XmlNodeReader(cfgNode)
                    With xReader
                        Do While (.Read())
                            If .NodeType = XmlNodeType.Element Then
                                If .Name = c_stNodeVersion Then
                                    If c_stXmlVersion = .ReadString() Then
                                        GetXmlVersion = True
                                    End If
                                End If
                            End If
                        Loop
                        .Close()
                    End With
                End If
            Catch ex As Exception
                If ex.TargetSite.Name <> stFileError Then MessageBox.Show(ex.Message)
            End Try
        End Function

        Public Function OpenProcDefs() As Boolean
            m_IX = 1
            m_procDoc = New XmlDocument
            Dim stPath As String = Path.Combine(My.Application.Info.DirectoryPath, c_stXmlProcDefFileName)
            m_procDoc.Load(stPath)
        End Function

        Public Function GetProcsVersion() As String
            GetProcsVersion = ""
            Dim procNode As XmlNode
            Dim xReader As XmlNodeReader
            procNode = m_procDoc.DocumentElement.SelectSingleNode(c_stNodeRoot & c_stNodeVersion)
            If procNode IsNot Nothing Then
                xReader = New XmlNodeReader(procNode)
                With xReader
                    Do While (.Read())
                        If .NodeType = XmlNodeType.Element Then
                            If .Name = c_stNodeVersion Then
                                GetProcsVersion = .ReadString()
                            End If
                        End If
                    Loop
                    .Close()
                End With
            End If
            Return GetProcsVersion
        End Function

        Public Sub CloseProcDefs()
            m_procDoc = Nothing
        End Sub

        Public Function GetProc(ByRef pName As String, ByRef pDefinition As String) As Boolean
            Dim procNode As XmlNode
            Dim xReader As XmlNodeReader
            procNode = m_procDoc.DocumentElement.SelectSingleNode(c_stNodeProcDef & c_stElementProcDef & Format(c_stFmt, m_IX))
            If procNode IsNot Nothing Then
                xReader = New XmlNodeReader(procNode)
                With xReader
                    Do While (.Read())
                        If .NodeType = XmlNodeType.Element Then
                            If .Name = c_stElementProcDef & Format(c_stFmt, m_IX) Then
                                .ReadToFollowing(c_stElementProcName)
                                If .Name = c_stElementProcName Then
                                    pName = .ReadString()
                                    .ReadToFollowing(c_stValue)
                                End If
                                If .Name = c_stValue Then
                                    pDefinition = .ReadString()
                                End If
                                m_IX += 1
                                Return True
                            End If
                        End If
                    Loop
                    .Close()
                End With
            End If
        End Function
    End Class
End Module