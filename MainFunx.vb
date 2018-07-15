Imports System.IO
Imports System.Data.Common

Partial Public Class frmMain

    Private Sub Lock(ByVal b As Boolean)
        bLocked = b
        tssb_File_Rescan.Enabled = Not b
        'tsmiFile_CreateNewDatabase.Enabled = Not b
        cmsTree_txtChangeVolSequenceNumber.Enabled = Not b
        cmsTree_ChangeSequenceNumber.Enabled = Not b
        cmsList_ChangeAlias.Enabled = Not b
        tssb_File_Open_Database.Enabled = Not b
        tssb_File_Add_Database.Enabled = Not b
        tssb_File_Import.Enabled = Not b
        cmbDrives.Enabled = Not b
    End Sub

    Private Sub BlankInfo()
        txtFileAlias.Text = ""
        txtDateModified.Text = ""
        txtSize.Text = ""
        txtFileName.Text = ""
        txtFolderName.Text = ""
        txtFileType.Text = ""
        txtVolumeName.Text = ""
        txtVolumeSequence.Text = ""
    End Sub

    Private Sub OpenDatabase()
        If uOpt.RMMFilePath = "" Then uOpt.RMMFilePath = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        If OpenFile(uOpt.RMMFilePath, "Open " & My.Application.Info.Description & " Database", c_fMDB, c_fIndexMDB) Then
            pDB.CloseMDB()
            ClearControls()
            If Not pDB.OpenMDB(uOpt.RMMFilePath, bRefresh) Then
                MessageBox.Show(c_stOpenError & My.Application.Info.Description)
                pDB.CloseMDB()
                Text = SetCaption(" * No Database Loaded * ")
                ClearControls()
            Else
                RefreshTree(False)
                Text = SetCaption(Path.GetFileNameWithoutExtension(uOpt.RMMFilePath.ToUpper))
            End If
        End If
    End Sub

    Private Sub SelectExplorer(ByRef stName As String, ByRef stVolumeName As String, ByVal itemID As Integer, ByVal bFolder As Boolean)

        Dim bFound As Boolean
        Dim dLetter As String = ""
        Dim stUneek As String
        Dim stSerial As String
        Dim stVolume As String = ""
        Dim stPath As String = ""

        stUneek = pDB.GetDiskUneekID(IIf(bFolder, "sp_GetDiskID_FolderID", "sp_GetDiskID_FileID").ToString, _
                                      IIf(bFolder, "inFolderID", "inFileID").ToString, itemID)
        For Each dInfo As DriveInfo In DriveInfo.GetDrives()
            If dInfo.DriveType = DriveType.CDRom Or dInfo.DriveType = DriveType.Removable Then
                If dInfo.IsReady Then
                    dLetter = dInfo.Name
                    stVolume = dInfo.VolumeLabel
                    stSerial = IIf(dInfo.VolumeLabel = "", "No Label", dInfo.VolumeLabel).ToString & "_" & GetVolumeSerialNumber(dLetter)
                    If stUneek = stSerial Then
                        bFound = True
                        Exit For
                    End If
                End If
            End If
        Next
        If bFound Then
            Dim id As Integer
            If bFolder Then
                id = itemID
            Else
                id = pDB.GetFolderID(itemID)
            End If
            Dim iE As DbEnumerator
            Do While id > 0
                iE = pDB.GetParentFolder(id)
                id = 0
                Do While iE.MoveNext
                    If iE.Current IsNot Nothing Then
                        Dim r As DbDataRecord = CType(iE.Current, DbDataRecord)
                        stPath = r("Name").ToString & "\" & stPath
                        id = CInt(r("Parent_ID"))
                    Else
                        id = 0
                        Exit Do
                    End If
                Loop
                pDB.CloseReaderAsync()
            Loop
            stPath = stPath.Replace(stVolume, dLetter.Substring(0, 2)) & IIf(bFolder, "", stName).ToString
            Process.Start("explorer", "/select," & stPath)
        Else
            MessageBox.Show("The disk '" & stVolumeName & "' is not available.")
        End If
    End Sub

    Private Sub RefreshTree(ByVal bForceClear As Boolean, Optional ByVal bSync As Boolean = False)

        If treeMain.Nodes.Count = 0 Then bForceClear = True
        If bForceClear Then
            treeMain.Nodes.Clear()
            drUneeks.Clear()
        End If
        Dim iE As DbEnumerator = pDB.GetRootFolders(bSync)
        Do While iE.MoveNext()
            If iE.Current IsNot Nothing Then
                Dim r As DbDataRecord = CType(iE.Current, DbDataRecord)
                Dim ix As String
                Select Case CInt(r("Type"))
                    Case DriveType.CDRom
                        ix = "CDorDVDwDrive"
                    Case DriveType.Removable
                        ix = "PenDrive"
                    Case (DriveType.Removable * 10)
                        ix = "FloppywDrive"
                    Case Else
                        ix = "PenDrive"
                End Select
                Dim stVol As String = r("Name").ToString
                Dim stUneek As String = r("UneekID").ToString
                Dim stSerial As String = stUneek.Replace(stVol & "_", "")
                If bForceClear Or Not treeMain.Nodes.ContainsKey(r("Folder_ID").ToString) Then
                    Dim nd As New TreeNode
                    With nd
                        .Name = r("Folder_ID").ToString
                        .Text = stVol & " [" & stSerial & "]"
                        .Tag = r("Disk_ID").ToString
                        .SelectedImageKey = ix
                        .ImageKey = ix
                    End With
                    treeMain.Nodes.Add(nd)
                    drUneeks.Add(stUneek)
                End If
            End If
        Loop
        pDB.CloseReaderAsync()
        If treeMain.Nodes.Count > 0 Then
            For Each nd As TreeNode In treeMain.Nodes
                iE = pDB.GetChildFolders(CInt(nd.Name))
                Do While iE.MoveNext()
                    If iE.Current IsNot Nothing Then
                        Dim r As DbDataRecord = CType(iE.Current, DbDataRecord)
                        If Not nd.Nodes.ContainsKey(r("Folder_ID").ToString) Then
                            nd.Nodes.Add(r("Folder_ID").ToString, r("Name").ToString, "folderclose", "folderopen")
                        End If
                    End If
                Loop
                pDB.CloseReaderAsync()
            Next
            If bForceClear Then treeMain.SelectedNode = treeMain.Nodes(0)
        End If
    End Sub

    Private Sub RefreshDrives()
        Dim bCatalogued As Boolean
        Dim dLetter As String
        Dim stSerial As String
        Try
            cmbDrives.Items.Clear()
            drTypes.Clear()
            For Each dInfo As DriveInfo In DriveInfo.GetDrives()
                If dInfo.DriveType = DriveType.CDRom Or dInfo.DriveType = DriveType.Removable Then
                    If dInfo.IsReady Then
                        dLetter = dInfo.Name.Substring(0, 1)
                        stSerial = IIf(dInfo.VolumeLabel = "", "No Label", dInfo.VolumeLabel).ToString & "_" & GetVolumeSerialNumber(dLetter)
                        bCatalogued = False
                        For Each drUneek As Object In drUneeks
                            If drUneek.ToString = stSerial Then
                                bCatalogued = True
                                Exit For
                            End If
                        Next
                        If Not bCatalogued Then
                            Select Case dInfo.DriveType
                                Case DriveType.CDRom
                                    drTypes.Add(dInfo.DriveType)
                                Case DriveType.Removable
                                    If dLetter = "A" Or dLetter = "B" Then
                                        drTypes.Add(dInfo.DriveType * 10)
                                    Else
                                        drTypes.Add(dInfo.DriveType)
                                    End If
                                Case Else
                                    drTypes.Add(dInfo.DriveType)
                            End Select
                            cmbDrives.Items.Add(dInfo.Name.Substring(0, 2) & " " & dInfo.VolumeLabel)
                        Else
                            'TODO refresh if its a pen drive
                        End If
                    End If
                End If
            Next
            If cmbDrives.Items.Count > 0 Then
                cmbDrives.Text = cmbDrives.Items(0).ToString
                tssb_File_Add_Database.Enabled = True
                tssb_File_Add_Database.Text = "&Add [" & cmbDrives.Text & "] to Database"
                tTip.ToolTipIcon = ToolTipIcon.Info
                tTip.ToolTipTitle = "Found New Media"
                tTip.ShowAlways = True
                tTip.UseAnimation = True
                tTip.Show("Select here, then press F12 to add.", cmbDrives, cmbDrives.Width - CInt((cmbDrives.Width / 8)), -(cmbDrives.Height * 3), 15000)
            Else
                tssb_File_Add_Database.Enabled = False
                tssb_File_Add_Database.Text = "&Add Media to Database"
            End If
        Catch ex As Exception
            If ex.TargetSite.Name <> "WinIOError" Then MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub RefreshList(ByVal parentID As Integer, ByRef pSearch As String, ByRef SearchType As String)
        listMain.Items.Clear()
        Dim bSearch As Boolean
        Dim lvi As New ArrayList
        Dim iE As DbEnumerator
        If parentID = 0 Then
            bSearch = True
            iE = pDB.GetSearchAll(pSearch, SearchType)
        Else
            iE = pDB.GetChildFolders(parentID)
        End If
        Dim szIcon As ShellIcon.IconSize = _
            CType(IIf(listMain.View = View.LargeIcon, ShellIcon.IconSize.Large, ShellIcon.IconSize.Small), ShellIcon.IconSize)
        If iE IsNot Nothing And Not bSearch Then
            Dim lsi As ListViewItem.ListViewSubItem
            Do While iE.MoveNext()
                If iE.Current IsNot Nothing Then
                    Dim r As DbDataRecord = CType(iE.Current, DbDataRecord)
                    Dim li As New ListViewItem
                    With li
                        .Name = r("Folder_ID").ToString
                        .Text = r("Name").ToString
                        .ImageIndex = sIcon.GetIconIndex("RM_Manager.FolderClose", szIcon)
                        lsi = New ListViewItem.ListViewSubItem
                        lsi.Name = "Alias"
                        lsi.Text = ""
                        .SubItems.Add(lsi)
                        lsi = New ListViewItem.ListViewSubItem
                        lsi.Name = "FileSize"
                        lsi.Text = ""
                        .SubItems.Add(lsi)
                        lsi = New ListViewItem.ListViewSubItem
                        lsi.Name = "Type"
                        lsi.Text = "File Folder"
                        .SubItems.Add(lsi)
                        lsi = New ListViewItem.ListViewSubItem
                        lsi.Name = "DateModified"
                        lsi.Text = [String].Format(c_ANSIDateFormat, r(lsi.Name))
                        .SubItems.Add(lsi)
                    End With
                    lvi.Add(li)
                End If
            Loop
        End If
        If Not bSearch Then
            pDB.CloseReaderAsync()
            iE = pDB.GetFolderContents(parentID)
        End If
        If iE IsNot Nothing Then
            Do While iE.MoveNext()
                If iE.Current IsNot Nothing Then
                    Dim r As DbDataRecord = CType(iE.Current, DbDataRecord)
                    Dim stExt As String = r("Extension").ToString
                    Dim li As New ListViewItem
                    Dim lsi As ListViewItem.ListViewSubItem
                    With li
                        .Name = r("File_ID").ToString
                        .Text = r("Name").ToString
                        .ImageIndex = sIcon.GetIconIndex(stExt, szIcon)
                        lsi = New ListViewItem.ListViewSubItem
                        lsi.Name = "Alias"
                        lsi.Text = r(lsi.Name).ToString
                        .SubItems.Add(lsi)
                        lsi = New ListViewItem.ListViewSubItem
                        lsi.Name = "FileSize"
                        lsi.Text = r(lsi.Name).ToString & " KB"
                        .SubItems.Add(lsi)
                        lsi = New ListViewItem.ListViewSubItem
                        lsi.Name = "Type"
                        lsi.Text = sIcon.GetIconType(stExt)
                        .SubItems.Add(lsi)
                        lsi = New ListViewItem.ListViewSubItem
                        lsi.Name = "DateModified"
                        lsi.Text = [String].Format(c_ANSIDateFormat, r(lsi.Name))
                        .SubItems.Add(lsi)
                    End With
                    lvi.Add(li)
                End If
            Loop
            pDB.CloseReaderAsync()
            listMain.Items.AddRange(DirectCast(lvi.ToArray(GetType(ListViewItem)), ListViewItem()))
            If listMain.Columns.Count = 0 Then
                With listMain.Columns
                    .Add("Name", lvmColWidth(0), HorizontalAlignment.Left)
                    .Add("Alias", lvmColWidth(1), HorizontalAlignment.Left)
                    .Add("Size", lvmColWidth(2), HorizontalAlignment.Right)
                    .Add("Type", lvmColWidth(3), HorizontalAlignment.Left)
                    .Add("Date Modified", lvmColWidth(4), HorizontalAlignment.Left)
                End With
                listMain.Columns(0).ImageKey = "RM_Manager.UPARROW"
            End If
            listMain.BackColor = CType(IIf(bSearch, Color.LightSteelBlue, SystemColors.Window), Color)
        End If
    End Sub

    Private Sub RefreshNode(ByVal node As TreeNode)
        Dim iE As DbEnumerator
        If node.Nodes.Count > 0 Then
            For Each nd As TreeNode In node.Nodes
                iE = pDB.GetChildFolders(CInt(nd.Name))
                Do While iE.MoveNext()
                    If iE.Current IsNot Nothing Then
                        Dim r As DbDataRecord = CType(iE.Current, DbDataRecord)
                        If Not nd.Nodes.ContainsKey(r("Folder_ID").ToString) Then
                            nd.Nodes.Add(r("Folder_ID").ToString, r("Name").ToString, "folderclose", "folderopen")
                        End If
                    End If
                Loop
                pDB.CloseReaderAsync()
            Next
        End If
        RefreshList(CInt(node.Name), "", "")
        BlankInfo()
    End Sub

    Private Sub ReSelectNode()
        For Each nd As TreeNode In treeMain.Nodes
            If CInt(nd.Tag) = m_cmTree_ID Then
                treeMain.SelectedNode = nd
            End If
        Next
    End Sub

    Private Sub RefreshSearch()
        Dim bAdd As Boolean = True
        Dim dt() As String
        Dim stQuery As String = ""
        For Each i As String In tscmbSearch.Items
            bAdd = i = tscmbSearch.Text
        Next
        If bAdd Then tscmbSearch.Items.Add(tscmbSearch.Text)
        Select Case tscmbSearchType.Text
            Case c_stSearchAll
                stQuery = "sp_ctl_GetFolderContents_SearchAll"
            Case c_stSearchFiles
                stQuery = "sp_ctl_GetFolderContents_SearchFiles"
            Case c_stSearchTypes
                stQuery = "sp_ctl_GetFolderContents_SearchTypes"
            Case c_stSearchAlias
                stQuery = "sp_ctl_GetFolderContents_SearchAlias"
            Case c_stSearchFolders
                stQuery = "sp_ctl_GetFolderContents_SearchFolders"
            Case c_stSearchDates
                stQuery = "sp_ctl_GetFolderContents_SearchDates"
                dt = tscmbSearch.Text.Split(CChar(" "))
                If dt.Length <> 2 Then Exit Sub
        End Select
        RefreshList(0, tscmbSearch.Text, stQuery)
    End Sub

    Private Sub SetViewMenu(ByVal v As View)
        Select Case v
            Case View.Details
                tssb_View_List.Checked = False
                tssb_View_Large.Checked = False
                tssb_View_Small.Checked = False
                tssb_View_Details.Checked = True
            Case View.LargeIcon
                tssb_View_List.Checked = False
                tssb_View_Large.Checked = True
                tssb_View_Small.Checked = False
                tssb_View_Details.Checked = False
            Case View.List
                tssb_View_List.Checked = True
                tssb_View_Large.Checked = False
                tssb_View_Small.Checked = False
                tssb_View_Details.Checked = False
            Case View.SmallIcon
                tssb_View_List.Checked = False
                tssb_View_Large.Checked = False
                tssb_View_Small.Checked = True
                tssb_View_Details.Checked = False
        End Select
        listMain.View = v
        uOpt.ListView = v
        If listMain.BackColor = Color.LightSteelBlue Then
            RefreshSearch()
        Else
            If treeMain.Nodes.Count > 0 Then RefreshList(CInt(treeMain.SelectedNode.Name), "", "")
        End If
    End Sub

    Private Function GetDriveType(ByVal ix As Integer) As Integer
        Return CInt(drTypes.Item(ix))
    End Function

    Private Sub ClearControls()
        treeMain.Nodes.Clear()
        listMain.Items.Clear()
    End Sub

    Private Function SetCaption(ByVal stCap As String) As String
        Return My.Application.Info.Description & " [" & stCap & "]"
    End Function
End Class
