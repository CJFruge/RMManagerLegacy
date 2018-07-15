Imports System.IO
Imports System.Threading
Imports System.ComponentModel
Imports System.Data.Common
Imports System.String

Public Class frmMain

    Private Const WM_DEVICECHANGE As Int32 = &H219
    Private Const c_stTurnOffAutoScan As String = "&Turn Off Auto Scan for Disks Inserted"
    Private Const c_stTurnOnAutoScan As String = "&Turn On Auto Scan for Disks Inserted"
    Private Const c_stTurnOffBalloons As String = "&Turn Off Balloon Tips"
    Private Const c_stTurnOnBalloons As String = "&Turn On Balloon Tips"
    Private Const c_stAutoScanOn As String = "AutoScan: ON"
    Private Const c_stAutoScanOff As String = "AutoScan: OFF"
    Private Const c_stOpenError As String = "This database is not compatible with "
    Private Const c_stEditAlias As String = "E&dit"
    Private Const c_stSaveAlias As String = "&Save"
    Private Const c_stSearchAll As String = "Search All (Files, Aliases, Folders)"
    Private Const c_stSearchFiles As String = "Search Files"
    Private Const c_stSearchFolders As String = "Search Folders"
    Private Const c_stSearchAlias As String = "Search Aliases"
    Private Const c_stSearchDates As String = "Search Date Range"

    Private bLocked As Boolean
    Private bRefresh As Boolean
    Private m_cmTree_ID As Integer
    Private WithEvents pDB As New DBHandler()
    Private sortCol As Integer
    Private lviSort As New ListItemComparer

    Protected Overrides Sub WndProc(ByRef m As Message)
        If Not bLocked And uOpt.AutoScan Then
            If m.Msg = WM_DEVICECHANGE Then RefreshDrives(cmbDrives, tTip, tssb_File_Add_Database)
        End If
        MyBase.WndProc(m)
    End Sub

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
                RefreshTree(pDB, treeMain)
                Text = SetCaption(Path.GetFileNameWithoutExtension(uOpt.RMMFilePath.ToUpper))
            End If
        End If
    End Sub

    Private Sub ClearControls()
        treeMain.Nodes.Clear()
        listMain.Items.Clear()
    End Sub

    Private Sub frmMain_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Dim fnt As Font = SystemFonts.MenuFont
        For Each i As ToolStripItem In tStrip.Items
            i.Font = fnt
        Next
        For Each i As ToolStripItem In cmsList.Items
            i.Font = fnt
        Next
        For Each i As ToolStripItem In cmsTree.Items
            i.Font = fnt
        Next
        sStrip.Font = SystemFonts.StatusFont
        fnt = SystemFonts.IconTitleFont
        listMain.Font = fnt
        treeMain.Font = fnt
        For Each c As Control In infoSplit.Panel2.Controls
            If c.GetType().ToString = "System.Windows.Forms.Label" Then
                c.Font = SystemFonts.SmallCaptionFont
            End If
        Next
        For Each c As Control In infoSplit.Panel2.Controls
            If c.GetType().ToString = "System.Windows.Forms.TextBox" Then
                c.Font = SystemFonts.IconTitleFont
            End If
        Next
    End Sub

    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        uOpt.SearchType = tscmbSearchType.Text
        uOpt.lastSearch = tscmbSearch.Text
        Dim j As Integer = 0
        For Each col As ColumnHeader In listMain.Columns
            If j <= UBound(lvmColWidth) Then lvmColWidth(j) = col.Width
            j += 1
        Next
        uOpt.infoHeight = infoSplit.SplitterDistance
        uOpt.treeWidth = formSplit.SplitterDistance
        xFH.SaveConfiguration(Me)
    End Sub

    Private Sub frmMain_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        Dim cmdArgs As String = ""
        listMain.SmallImageList = sIcon.ImList(ShellIcon.IconSize.Small)
        listMain.LargeImageList = sIcon.ImList(ShellIcon.IconSize.Large)
        listMain.ListViewItemSorter = lviSort

        xFH.LoadConfiguration(Me)
        If Height = 0 Then Height = CInt(SystemInformation.PrimaryMonitorSize.Height / 2)
        If Width = 0 Then Width = CInt(SystemInformation.PrimaryMonitorSize.Width / 2)
        If Left = 0 Then Left = 20
        If Top = 0 Then Top = 20
        tscmbSearch.Text = uOpt.lastSearch
        tssLabelScan.Text = IIf(uOpt.AutoScan, c_stAutoScanOn, c_stAutoScanOff).ToString
        tssb_Options_Toggle_AutoScan.Text = IIf(uOpt.AutoScan, c_stTurnOffAutoScan, c_stTurnOnAutoScan).ToString
        tssb_Options_Toggle_Balloon_Tips.Text = IIf(uOpt.bBalloons, c_stTurnOffBalloons, c_stTurnOnBalloons).ToString
        Text = SetCaption(Path.GetFileNameWithoutExtension(uOpt.RMMFilePath.ToUpper))
        With tscmbSearchType.Items
            .Add(c_stSearchAll)
            .Add(c_stSearchFiles)
            .Add(c_stSearchAlias)
            .Add(c_stSearchFolders)
            .Add(c_stSearchDates)
        End With
        tscmbSearchType.Text = uOpt.SearchType
        If My.Application.CommandLineArgs.Count > 0 Then
            cmdArgs = My.Application.CommandLineArgs.Item(0)
            bRefresh = CBool(IIf(cmdArgs = "/refresh", True, False))
        End If
        If uOpt.RMMFilePath <> "" Then
            If Not pDB.OpenMDB(uOpt.RMMFilePath, bRefresh) Then
                MessageBox.Show(c_stOpenError & My.Application.Info.Description)
                pDB.CloseMDB()
                OpenDatabase()
            Else
                RefreshTree(pDB, treeMain)
            End If
        Else
            uOpt.RMMFilePath = Path.Combine(Path.Combine(My.Computer.FileSystem.SpecialDirectories.MyDocuments, My.Resources.en_MyRemovableMedia), "New Database.mdb")
            OpenDatabase()
        End If
        infoSplit.SplitterDistance = uOpt.infoHeight
        formSplit.SplitterDistance = uOpt.treeWidth
        SetViewMenu(uOpt.ListView)
        If Not bLocked And uOpt.AutoScan Then
            RefreshDrives(cmbDrives, tTip, tssb_File_Add_Database)
        Else
            tssb_File_Add_Database.Enabled = cmbDrives.Items.Count > 0
        End If
    End Sub

    Private Sub cmbDrives_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles cmbDrives.SelectedIndexChanged
        tssLabelInfo.Text = pDB.GetDrive(cmbDrives.SelectedItem.ToString, False)
        tssb_File_Add_Database.Text = "&Add " & cmbDrives.Text & " to Database"
    End Sub

    Private Sub tssb_File_Add_Database_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles tssb_File_Add_Database.Click
        Dim stResults As String = pDB.GetDrive(cmbDrives.SelectedItem.ToString, False)
        tssLabelInfo.Text = cmbDrives.Text & " (" & stResults & "), adding to database..."
        pDB.mt_bAdd2DB = True
        pDB.mt_stDrive = cmbDrives.SelectedItem.ToString
        pDB.mt_DriveType = GetDriveType(cmbDrives.SelectedIndex)
        Lock(True)
        pDB.RunWorkerAsync()
    End Sub

    Private Sub tssb_File_Rescan_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles tssb_File_Rescan.Click
        If Not bLocked Then RefreshDrives(cmbDrives, tTip, tssb_File_Add_Database)
    End Sub

    Private Sub DBHandler_UpdateProgress(ByVal sender As Object, ByVal e As ProgressChangedEventArgs) Handles pDB.ProgressChanged
        tspBar.Value = e.ProgressPercentage
    End Sub

    Private Sub DBHandler_WorkCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles pDB.RunWorkerCompleted
        If e.Error IsNot Nothing Then MessageBox.Show(e.Error.Message)
        tssLabelInfo.Text = e.Result.ToString
        Lock(False)
        tspBar.Value = 0
        RefreshTree(pDB, treeMain)
        RefreshDrives(cmbDrives, tTip, tssb_File_Add_Database)
    End Sub

    Private Sub treeMain_NodeMouseClick(ByVal sender As Object, ByVal e As TreeNodeMouseClickEventArgs) Handles treeMain.NodeMouseClick
        Dim iE As IEnumerator
        txtFileAlias.ReadOnly = True
        If e.Node.Nodes.Count > 0 Then
            For Each nd As TreeNode In e.Node.Nodes
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
        RefreshList(pDB, listMain, CInt(e.Node.Name), "", "")
        txtFileAlias.Text = ""
        txtDateModified.Text = ""
        txtSize.Text = ""
        txtFileName.Text = ""
        txtFolderName.Text = ""
        txtFileType.Text = ""
        txtVolumeName.Text = ""
        txtVolumeSequence.Text = ""
    End Sub

    Private Sub tssb_Options_Toggle_AutoScan_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles tssb_Options_Toggle_AutoScan.Click
        If uOpt.AutoScan Then
            uOpt.AutoScan = False
            tssLabelScan.Text = "AutoScan: OFF"
            tssb_Options_Toggle_AutoScan.Text = c_stTurnOnAutoScan
        Else
            uOpt.AutoScan = True
            tssLabelScan.Text = "AutoScan: ON"
            tssb_Options_Toggle_AutoScan.Text = c_stTurnOffAutoScan
        End If
    End Sub

    Private Sub tscmbSearch_KeyUp(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles tscmbSearch.KeyUp
        If e.KeyCode = Keys.Enter Then
            RefreshSearch()
        End If
    End Sub

    Private Sub cmdSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdSearch.Click
        RefreshSearch()
    End Sub

    Private Sub RefreshSearch()
        Dim bAdd As Boolean = True
        Dim dt() As String
        Select Case tscmbSearchType.Text
            Case c_stSearchAll
                RefreshList(pDB, listMain, 0, tscmbSearch.Text, "sp_ctl_GetFolderContents_SearchAll")
            Case c_stSearchFiles
                RefreshList(pDB, listMain, 0, tscmbSearch.Text, "sp_ctl_GetFolderContents_SearchFiles")
            Case c_stSearchAlias
                RefreshList(pDB, listMain, 0, tscmbSearch.Text, "sp_ctl_GetFolderContents_SearchAlias")
            Case c_stSearchFolders
                RefreshList(pDB, listMain, 0, tscmbSearch.Text, "sp_ctl_GetFolderContents_SearchFolders")
            Case c_stSearchDates
                dt = tscmbSearch.Text.Split(CChar(" "))
                If dt.Length = 2 Then
                    RefreshList(pDB, listMain, 0, tscmbSearch.Text, "sp_ctl_GetFolderContents_SearchDates")
                End If
        End Select
        For Each i As String In tscmbSearch.Items
            If i = tscmbSearch.Text Then bAdd = False
        Next
        If bAdd Then tscmbSearch.Items.Add(tscmbSearch.Text)
    End Sub

    Private Sub tssb_View_Large_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tssb_View_Large.Click
        SetViewMenu(View.LargeIcon)
    End Sub

    Private Sub tssb_View_Small_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tssb_View_Small.Click
        SetViewMenu(View.SmallIcon)
    End Sub

    Private Sub tssb_View_List_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tssb_View_List.Click
        SetViewMenu(View.List)
    End Sub

    Private Sub tssb_View_Details_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tssb_View_Details.Click
        SetViewMenu(View.Details)
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
            If treeMain.Nodes.Count > 0 Then RefreshList(pDB, listMain, CInt(treeMain.SelectedNode.Name), "", "")
        End If
    End Sub

    Private Sub listMain_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles listMain.Click
        Dim iE As IEnumerator
        Dim bFolder As Boolean
        Dim folderID As Integer = 0
        Try
            If listMain.SelectedItems.Count = 1 Then
                txtFileAlias.ReadOnly = True
                Dim id As Integer = CInt(listMain.SelectedItems(0).Name)
                If listMain.SelectedItems(0).SubItems("Type").Text = "File Folder" Then
                    bFolder = True
                    iE = pDB.GetDiskOrFolderInfo("sp_ctl_GetDiskFolderInfo_FolderID", "inFolderID", id)
                Else
                    bFolder = False
                    iE = pDB.GetDiskOrFolderInfo("sp_ctl_GetDiskFolderInfo_FileID", "inFileID", id)
                End If

                Do While iE.MoveNext()
                    If iE.Current IsNot Nothing Then
                        Dim r As DbDataRecord = CType(iE.Current, DbDataRecord)
                        If bFolder Then
                            txtFileAlias.Text = "N/A"
                            txtDateModified.Text = [String].Format(c_ANSIDateFormat, r("DateModified"))
                            txtSize.Text = "N/A"
                            txtFileName.Text = "N/A"
                            txtFolderName.Text = r("Folder.Name").ToString
                        Else
                            txtFileAlias.Text = r("Alias").ToString
                            txtDateModified.Text = [String].Format(c_ANSIDateFormat, r("DateModified"))
                            txtSize.Text = r("FileSize").ToString & " KB"
                            txtFileName.Text = r("File.Name").ToString
                            txtFolderName.Text = r("Folder.Name").ToString
                        End If
                        folderID = CInt(r("Folder_ID"))
                        txtVolumeName.Text = r("Disk.Name").ToString
                        txtVolumeSequence.Text = r("Sequence").ToString
                    End If
                Loop
                pDB.CloseReaderAsync()
                txtFileType.Text = listMain.SelectedItems(0).SubItems("Type").Text
                If treeMain.Nodes.Count > 0 Then tssLabelInfo.Text = treeMain.SelectedNode.FullPath
                If listMain.BackColor = Color.LightSteelBlue Then
                    For Each nd As TreeNode In treeMain.Nodes
                        If CInt(nd.Name) = folderID Then
                            treeMain.SelectedNode = nd
                            nd.EnsureVisible()
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub listMain_ColumnClick(ByVal sender As Object, ByVal e As ColumnClickEventArgs) Handles listMain.ColumnClick
        If e.Column <> sortCol Then
            listMain.Columns(sortCol).ImageKey = ""
            listMain.Sorting = SortOrder.Ascending
            listMain.Columns(e.Column).ImageKey = "RM_Manager.UPARROW"
            sortCol = e.Column
        Else
            If listMain.Sorting = SortOrder.Ascending Then
                listMain.Sorting = SortOrder.Descending
                listMain.Columns(e.Column).ImageKey = "RM_Manager.DNARROW"
            Else
                listMain.Sorting = SortOrder.Ascending
                listMain.Columns(e.Column).ImageKey = "RM_Manager.UPARROW"
            End If
        End If
        lviSort.SetSort(e.Column, listMain.Sorting)
        listMain.Sort()
    End Sub

    Private Sub tssb_Help_About_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tssb_Help_About.Click
        vSplash.ShowDialog()
    End Sub

    Private Sub tssb_File_Open_Database_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tssb_File_Open_Database.Click
        OpenDatabase()
    End Sub

    Private Sub listMain_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles listMain.DoubleClick
        With listMain
            If .SelectedItems.Count > 0 Then
                Dim lsi As ListViewItem.ListViewSubItem = .SelectedItems(0).SubItems("Type")
                If lsi IsNot Nothing Then
                    If lsi.Text = "File Folder" Then
                        Dim id As Integer = CInt(.SelectedItems(0).Name)
                        RefreshList(pDB, listMain, id, "", "")
                        For Each nd As TreeNode In treeMain.SelectedNode.Nodes
                            If CInt(nd.Name) = id Then
                                treeMain.SelectedNode = nd
                                nd.EnsureVisible()
                                Exit For
                            End If
                        Next
                    End If
                End If
            End If
        End With
    End Sub

    Private Sub listMain_ItemMouseHover(ByVal sender As Object, ByVal e As ListViewItemMouseHoverEventArgs) Handles listMain.ItemMouseHover
        If uOpt.bBalloons Then
            Dim stInfo As String = ""
            Dim j As Integer = 0
            For Each lvsi As ListViewItem.ListViewSubItem In e.Item.SubItems
                stInfo &= listMain.Columns(j).Text & ": " & lvsi.Text & vbCrLf
                j += 1
            Next
            tTip.ToolTipTitle = "File Info"
            tTip.SetToolTip(CType(sender, Control), stInfo)
        End If
        tTip.Active = (listMain.View <> View.Details) And uOpt.bBalloons
    End Sub

    Private Sub tscmbSearchType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tscmbSearchType.SelectedIndexChanged
        uOpt.SearchType = tscmbSearchType.Text
    End Sub

    Private Sub tssb_Options_ToggleBalloonTips_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tssb_Options_Toggle_Balloon_Tips.Click
        If uOpt.bBalloons Then
            uOpt.bBalloons = False
            tssb_Options_Toggle_Balloon_Tips.Text = c_stTurnOnBalloons
        Else
            uOpt.bBalloons = True
            tssb_Options_Toggle_Balloon_Tips.Text = c_stTurnOffBalloons
        End If
    End Sub

    Private Sub tssb_File_Compact_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tssb_File_Compact.Click
        Dim stPath As String = uOpt.RMMFilePath
        If OpenFile(stPath, "Compact " & My.Application.Info.Description & " Database", c_fMDB, c_fIndexMDB) Then
            If stPath = uOpt.RMMFilePath Then
                pDB.CloseMDB()
                ClearControls()
            End If
            tssLabelInfo.Text = "Compacting..." & stPath
            If CompactDB(stPath) Then
                tssLabelInfo.Text = "Compaction successful"
                If stPath = uOpt.RMMFilePath Then
                    pDB.OpenMDB(stPath, False)
                    RefreshTree(pDB, treeMain)
                    tssLabelInfo.Text = "Ready"
                End If
            End If
        End If
    End Sub

    Private Sub tssb_File_CreateNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tssb_File_Create_New.Click
        Dim stPath As String = Path.Combine(Path.Combine(My.Computer.FileSystem.SpecialDirectories.MyDocuments, My.Resources.en_MyRemovableMedia), "New Database.mdb")
        If OpenFile(stPath, "Create New " & My.Application.Info.Description & " Database", c_fMDB, c_fIndexMDB, False) Then
            CreateDatabase(stPath)
        End If
    End Sub

    Private Sub cmsList_ChangeAlias_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmsList_ChangeAlias.Click
        If listMain.SelectedItems.Count = 1 Then
            Dim id As Integer = CInt(listMain.SelectedItems(0).Name)
            pDB.SetValue("sp_upd_FileAlias", "inDiskID", id, "inAlias", cmsList_txtFileAlias.Text.ToString)
            listMain.SelectedItems(0).SubItems("Alias").Text = cmsList_txtFileAlias.Text
            txtFileAlias.Text = cmsList_txtFileAlias.Text
        End If
    End Sub

    Private Sub cmsTree_PrintDiskContents_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsTree_PrintDiskContents.Click
        Dim nd As TreeNode = treeMain.SelectedNode
        If nd.Level = 0 Then
            With frmReport
                .Top = Top + 10
                .Left = Left + 10
                .QueryArg = pDB.GetIDataSet("sp_rpt_DiskContents", "inDiskID", nd.Tag)
                .Text = SetCaption("Print Disk Contents of " & nd.Text)
                .Show()
            End With
        End If
    End Sub

    Private Sub cmsTree_DeleteDisk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsTree_DeleteDisk.Click
        Dim nd As TreeNode = treeMain.SelectedNode
        If nd.Level = 0 Then
            If MessageBox.Show("Delete this disk from the database?", "Delete " & cmsTree_txtVolName.Text, _
                               MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = Windows.Forms.DialogResult.Yes Then
                pDB.DeleteDisk(CInt(nd.Tag))
                RefreshTree(pDB, treeMain)
                If uOpt.AutoScan Then RefreshDrives(cmbDrives, tTip, tssb_File_Add_Database)
            End If
        End If
    End Sub

    Private Sub cmsTree_Opened(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmsTree.Opened
        Dim nd As TreeNode = treeMain.SelectedNode
        Dim bRoot As Boolean = nd.Level = 0
        cmsTree_ChangeSequenceNumber.Enabled = bRoot
        cmsTree_DeleteDisk.Enabled = bRoot
        cmsTree_PrintDiskContents.Enabled = bRoot
        cmsTree_txtChangeVolSequenceNumber.Enabled = bRoot
        If bRoot Then
            m_cmTree_ID = CInt(nd.Tag)
            Dim iE As IEnumerator = pDB.GetDiskOrFolderInfo("sp_ctl_GetDiskInfo", "inDiskID", m_cmTree_ID)
            Do While (iE.MoveNext())
                If iE.Current IsNot Nothing Then
                    Dim r As DbDataRecord = CType(iE.Current, DbDataRecord)
                    cmsTree_txtVolName.Text = r("Name").ToString
                    cmsTree_txtChangeVolSequenceNumber.Text = r("Sequence").ToString
                End If
            Loop
            pDB.CloseReaderAsync()
        End If
    End Sub

    Private Sub cmsTree_cmdChangeSequenceNumber_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsTree_cmdChangeSequenceNumber.Click
        pDB.SetValue("sp_upd_DiskSequence", "inDiskID", m_cmTree_ID, "Sequence", cmsTree_txtChangeVolSequenceNumber.Text.ToString)
    End Sub

    Private Sub tssb_Help_Help_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tssb_Help_Help.Click
        ' freeware version will not have help available
        MessageBox.Show(My.Resources.en_HelpNotAvailable)
    End Sub

    Private Sub tssb_File_Import_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tssb_File_Import.Click
        With frmImport
            .Top = Top + 10
            .Left = Left + 10
            .Show()
        End With
    End Sub

    Private Sub cmsList_SelectInExplorer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsList_SelectInExplorer.Click
        If listMain.SelectedItems.Count = 1 Then
            Dim lvi As ListViewItem = listMain.SelectedItems(0)
            SelectExplorer(pDB, lvi.Text, txtVolumeName.Text, CInt(lvi.Name), lvi.SubItems("Type").Text = "File Folder")
        End If
    End Sub


    Private Sub cmsTree_SelectInExplorer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsTree_SelectInExplorer.Click
        Dim nd As TreeNode = treeMain.SelectedNode
        SelectExplorer(pDB, nd.Text, txtVolumeName.Text, CInt(nd.Name), True)
    End Sub

    Private Sub cmsList_Opening(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmsList.Opening
        If listMain.SelectedItems.Count = 1 Then
            If listMain.SelectedItems(0).SubItems("Type").Text <> "File Folder" Then
                cmsList_txtFileAlias.Text = txtFileAlias.Text
                cmsList_ChangeAlias.Enabled = True
            Else
                cmsList_txtFileAlias.Text = ""
                cmsList_ChangeAlias.Enabled = False
            End If
        End If
    End Sub

End Class

'
' try
'    {
'        // Parse the two objects passed as a parameter as a DateTime.
'        System.DateTime firstDate = 
'                DateTime.Parse(((ListViewItem)x).SubItems[col].Text);
'        System.DateTime secondDate = 
'                DateTime.Parse(((ListViewItem)y).SubItems[col].Text);
'        // Compare the two dates.
'        returnVal = DateTime.Compare(firstDate, secondDate);
'    }
'    // If neither compared object has a valid date format, compare
'    // as a string.
'    catch 
'    {
'        // Compare the two items as a string.
'        returnVal = String.Compare(((ListViewItem)x).SubItems[col].Text,
'                    ((ListViewItem)y).SubItems[col].Text);
'    }
'    // Determine whether the sort order is descending.
'    if (order == SortOrder.Descending)
'    // Invert the value returned by String.Compare.
'        returnVal *= -1;
'    return returnVal;
