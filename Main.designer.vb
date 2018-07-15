<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.tTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.sStrip = New System.Windows.Forms.StatusStrip
        Me.tssLabelInfo = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel
        Me.tssLabelScan = New System.Windows.Forms.ToolStripStatusLabel
        Me.tspBar = New System.Windows.Forms.ToolStripProgressBar
        Me.formSplit = New System.Windows.Forms.SplitContainer
        Me.cmbDrives = New System.Windows.Forms.ComboBox
        Me.treeMain = New System.Windows.Forms.TreeView
        Me.cmsTree = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cmsTree_txtVolName = New System.Windows.Forms.ToolStripMenuItem
        Me.cmsTree_SelectInExplorer = New System.Windows.Forms.ToolStripMenuItem
        Me.cmsTree_PrintDiskContents = New System.Windows.Forms.ToolStripMenuItem
        Me.cmsTree_ChangeSequenceNumber = New System.Windows.Forms.ToolStripMenuItem
        Me.cmsTree_txtChangeVolSequenceNumber = New System.Windows.Forms.ToolStripTextBox
        Me.cmsTree_cmdChangeSequenceNumber = New System.Windows.Forms.ToolStripMenuItem
        Me.cmsTree_DeleteDisk = New System.Windows.Forms.ToolStripMenuItem
        Me.imList = New System.Windows.Forms.ImageList(Me.components)
        Me.infoSplit = New System.Windows.Forms.SplitContainer
        Me.listMain = New System.Windows.Forms.ListView
        Me.cmsList = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cmsList_SelectInExplorer = New System.Windows.Forms.ToolStripMenuItem
        Me.cmsList_EditAlias = New System.Windows.Forms.ToolStripMenuItem
        Me.cmsList_txtFileAlias = New System.Windows.Forms.ToolStripTextBox
        Me.cmsList_ChangeAlias = New System.Windows.Forms.ToolStripMenuItem
        Me.cmsList_DeleteFile = New System.Windows.Forms.ToolStripMenuItem
        Me.lblVolumeNo = New System.Windows.Forms.Label
        Me.txtVolumeSequence = New System.Windows.Forms.TextBox
        Me.lblFolder = New System.Windows.Forms.Label
        Me.txtFolderName = New System.Windows.Forms.TextBox
        Me.lblVolume = New System.Windows.Forms.Label
        Me.txtVolumeName = New System.Windows.Forms.TextBox
        Me.lblFileType = New System.Windows.Forms.Label
        Me.txtFileType = New System.Windows.Forms.TextBox
        Me.lblFileDate = New System.Windows.Forms.Label
        Me.txtDateModified = New System.Windows.Forms.TextBox
        Me.lblSize = New System.Windows.Forms.Label
        Me.txtSize = New System.Windows.Forms.TextBox
        Me.lblFileAlias = New System.Windows.Forms.Label
        Me.txtFileAlias = New System.Windows.Forms.TextBox
        Me.lblFileName = New System.Windows.Forms.Label
        Me.txtFileName = New System.Windows.Forms.TextBox
        Me.ToolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator
        Me.tStrip = New System.Windows.Forms.ToolStrip
        Me.tssb_File = New System.Windows.Forms.ToolStripSplitButton
        Me.tssb_File_Rescan = New System.Windows.Forms.ToolStripMenuItem
        Me.tssb_File_Add_Database = New System.Windows.Forms.ToolStripMenuItem
        Me.tssb_File_Import = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.tssb_File_Open_Database = New System.Windows.Forms.ToolStripMenuItem
        Me.tssb_File_Create_New = New System.Windows.Forms.ToolStripMenuItem
        Me.tssb_File_Compact = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.tssb_File_Exit = New System.Windows.Forms.ToolStripMenuItem
        Me.tssb_Options = New System.Windows.Forms.ToolStripSplitButton
        Me.tssb_Options_Toggle_AutoScan = New System.Windows.Forms.ToolStripMenuItem
        Me.tssb_Options_Toggle_Balloon_Tips = New System.Windows.Forms.ToolStripMenuItem
        Me.tssb_View = New System.Windows.Forms.ToolStripSplitButton
        Me.tssb_View_Large = New System.Windows.Forms.ToolStripMenuItem
        Me.tssb_View_Small = New System.Windows.Forms.ToolStripMenuItem
        Me.tssb_View_List = New System.Windows.Forms.ToolStripMenuItem
        Me.tssb_View_Details = New System.Windows.Forms.ToolStripMenuItem
        Me.tssb_Help = New System.Windows.Forms.ToolStripSplitButton
        Me.tssb_Help_Help = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.tssb_Help_About = New System.Windows.Forms.ToolStripMenuItem
        Me.tssbSpacer = New System.Windows.Forms.ToolStripLabel
        Me.tscmbSearch = New System.Windows.Forms.ToolStripComboBox
        Me.cmdSearch = New System.Windows.Forms.ToolStripButton
        Me.tssbSpacer1 = New System.Windows.Forms.ToolStripLabel
        Me.tscmbSearchType = New System.Windows.Forms.ToolStripComboBox
        Me.sStrip.SuspendLayout()
        Me.formSplit.Panel1.SuspendLayout()
        Me.formSplit.Panel2.SuspendLayout()
        Me.formSplit.SuspendLayout()
        Me.cmsTree.SuspendLayout()
        Me.infoSplit.Panel1.SuspendLayout()
        Me.infoSplit.Panel2.SuspendLayout()
        Me.infoSplit.SuspendLayout()
        Me.cmsList.SuspendLayout()
        Me.tStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'tTip
        '
        Me.tTip.IsBalloon = True
        '
        'sStrip
        '
        Me.sStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tssLabelInfo, Me.ToolStripStatusLabel2, Me.tssLabelScan, Me.tspBar})
        Me.sStrip.Location = New System.Drawing.Point(0, 472)
        Me.sStrip.Name = "sStrip"
        Me.sStrip.Padding = New System.Windows.Forms.Padding(1, 0, 16, 0)
        Me.sStrip.Size = New System.Drawing.Size(1030, 22)
        Me.sStrip.TabIndex = 2
        Me.sStrip.Text = "StatusStrip1"
        '
        'tssLabelInfo
        '
        Me.tssLabelInfo.Name = "tssLabelInfo"
        Me.tssLabelInfo.Size = New System.Drawing.Size(911, 17)
        Me.tssLabelInfo.Spring = True
        Me.tssLabelInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ToolStripStatusLabel2
        '
        Me.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
        Me.ToolStripStatusLabel2.Size = New System.Drawing.Size(0, 17)
        Me.ToolStripStatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tssLabelScan
        '
        Me.tssLabelScan.Name = "tssLabelScan"
        Me.tssLabelScan.Size = New System.Drawing.Size(0, 17)
        Me.tssLabelScan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tspBar
        '
        Me.tspBar.Name = "tspBar"
        Me.tspBar.Size = New System.Drawing.Size(100, 16)
        '
        'formSplit
        '
        Me.formSplit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.formSplit.Location = New System.Drawing.Point(0, 0)
        Me.formSplit.Name = "formSplit"
        '
        'formSplit.Panel1
        '
        Me.formSplit.Panel1.Controls.Add(Me.cmbDrives)
        Me.formSplit.Panel1.Controls.Add(Me.treeMain)
        '
        'formSplit.Panel2
        '
        Me.formSplit.Panel2.Controls.Add(Me.infoSplit)
        Me.formSplit.Size = New System.Drawing.Size(1030, 494)
        Me.formSplit.SplitterDistance = 362
        Me.formSplit.SplitterWidth = 5
        Me.formSplit.TabIndex = 3
        '
        'cmbDrives
        '
        Me.cmbDrives.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbDrives.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDrives.FormattingEnabled = True
        Me.cmbDrives.Location = New System.Drawing.Point(3, 448)
        Me.cmbDrives.Name = "cmbDrives"
        Me.cmbDrives.Size = New System.Drawing.Size(356, 21)
        Me.cmbDrives.TabIndex = 2
        '
        'treeMain
        '
        Me.treeMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.treeMain.ContextMenuStrip = Me.cmsTree
        Me.treeMain.HideSelection = False
        Me.treeMain.ImageIndex = 0
        Me.treeMain.ImageList = Me.imList
        Me.treeMain.Location = New System.Drawing.Point(-1, 28)
        Me.treeMain.Name = "treeMain"
        Me.treeMain.SelectedImageIndex = 0
        Me.treeMain.Size = New System.Drawing.Size(362, 414)
        Me.treeMain.StateImageList = Me.imList
        Me.treeMain.TabIndex = 0
        '
        'cmsTree
        '
        Me.cmsTree.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmsTree_txtVolName, Me.cmsTree_SelectInExplorer, Me.cmsTree_PrintDiskContents, Me.cmsTree_ChangeSequenceNumber, Me.cmsTree_DeleteDisk})
        Me.cmsTree.Name = "cmsTree"
        Me.cmsTree.Size = New System.Drawing.Size(263, 114)
        '
        'cmsTree_txtVolName
        '
        Me.cmsTree_txtVolName.BackColor = System.Drawing.SystemColors.MenuBar
        Me.cmsTree_txtVolName.Enabled = False
        Me.cmsTree_txtVolName.Name = "cmsTree_txtVolName"
        Me.cmsTree_txtVolName.Size = New System.Drawing.Size(262, 22)
        Me.cmsTree_txtVolName.Text = "Vol Name"
        '
        'cmsTree_SelectInExplorer
        '
        Me.cmsTree_SelectInExplorer.Name = "cmsTree_SelectInExplorer"
        Me.cmsTree_SelectInExplorer.Size = New System.Drawing.Size(262, 22)
        Me.cmsTree_SelectInExplorer.Text = "&Select In Explorer"
        '
        'cmsTree_PrintDiskContents
        '
        Me.cmsTree_PrintDiskContents.Name = "cmsTree_PrintDiskContents"
        Me.cmsTree_PrintDiskContents.Size = New System.Drawing.Size(262, 22)
        Me.cmsTree_PrintDiskContents.Text = "&Print Disk Contents"
        '
        'cmsTree_ChangeSequenceNumber
        '
        Me.cmsTree_ChangeSequenceNumber.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmsTree_txtChangeVolSequenceNumber, Me.cmsTree_cmdChangeSequenceNumber})
        Me.cmsTree_ChangeSequenceNumber.Name = "cmsTree_ChangeSequenceNumber"
        Me.cmsTree_ChangeSequenceNumber.Size = New System.Drawing.Size(262, 22)
        Me.cmsTree_ChangeSequenceNumber.Text = "&Change Sequence Number"
        '
        'cmsTree_txtChangeVolSequenceNumber
        '
        Me.cmsTree_txtChangeVolSequenceNumber.Name = "cmsTree_txtChangeVolSequenceNumber"
        Me.cmsTree_txtChangeVolSequenceNumber.Size = New System.Drawing.Size(100, 23)
        '
        'cmsTree_cmdChangeSequenceNumber
        '
        Me.cmsTree_cmdChangeSequenceNumber.Name = "cmsTree_cmdChangeSequenceNumber"
        Me.cmsTree_cmdChangeSequenceNumber.Size = New System.Drawing.Size(160, 22)
        Me.cmsTree_cmdChangeSequenceNumber.Text = "C&hange"
        '
        'cmsTree_DeleteDisk
        '
        Me.cmsTree_DeleteDisk.Name = "cmsTree_DeleteDisk"
        Me.cmsTree_DeleteDisk.Size = New System.Drawing.Size(262, 22)
        Me.cmsTree_DeleteDisk.Text = "&Delete Disk"
        '
        'imList
        '
        Me.imList.ImageStream = CType(resources.GetObject("imList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imList.TransparentColor = System.Drawing.Color.Transparent
        Me.imList.Images.SetKeyName(0, "FolderClose")
        Me.imList.Images.SetKeyName(1, "FolderOpen")
        '
        'infoSplit
        '
        Me.infoSplit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.infoSplit.Location = New System.Drawing.Point(0, 0)
        Me.infoSplit.Name = "infoSplit"
        Me.infoSplit.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'infoSplit.Panel1
        '
        Me.infoSplit.Panel1.Controls.Add(Me.listMain)
        '
        'infoSplit.Panel2
        '
        Me.infoSplit.Panel2.Controls.Add(Me.lblVolumeNo)
        Me.infoSplit.Panel2.Controls.Add(Me.txtVolumeSequence)
        Me.infoSplit.Panel2.Controls.Add(Me.lblFolder)
        Me.infoSplit.Panel2.Controls.Add(Me.txtFolderName)
        Me.infoSplit.Panel2.Controls.Add(Me.lblVolume)
        Me.infoSplit.Panel2.Controls.Add(Me.txtVolumeName)
        Me.infoSplit.Panel2.Controls.Add(Me.lblFileType)
        Me.infoSplit.Panel2.Controls.Add(Me.txtFileType)
        Me.infoSplit.Panel2.Controls.Add(Me.lblFileDate)
        Me.infoSplit.Panel2.Controls.Add(Me.txtDateModified)
        Me.infoSplit.Panel2.Controls.Add(Me.lblSize)
        Me.infoSplit.Panel2.Controls.Add(Me.txtSize)
        Me.infoSplit.Panel2.Controls.Add(Me.lblFileAlias)
        Me.infoSplit.Panel2.Controls.Add(Me.txtFileAlias)
        Me.infoSplit.Panel2.Controls.Add(Me.lblFileName)
        Me.infoSplit.Panel2.Controls.Add(Me.txtFileName)
        Me.infoSplit.Panel2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.infoSplit.Size = New System.Drawing.Size(663, 494)
        Me.infoSplit.SplitterDistance = 220
        Me.infoSplit.TabIndex = 0
        '
        'listMain
        '
        Me.listMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.listMain.ContextMenuStrip = Me.cmsList
        Me.listMain.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.listMain.FullRowSelect = True
        Me.listMain.HideSelection = False
        Me.listMain.Location = New System.Drawing.Point(-1, 28)
        Me.listMain.Name = "listMain"
        Me.listMain.Size = New System.Drawing.Size(663, 192)
        Me.listMain.TabIndex = 1
        Me.listMain.UseCompatibleStateImageBehavior = False
        Me.listMain.View = System.Windows.Forms.View.Details
        '
        'cmsList
        '
        Me.cmsList.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmsList_SelectInExplorer, Me.cmsList_EditAlias, Me.cmsList_DeleteFile})
        Me.cmsList.Name = "cmsList"
        Me.cmsList.Size = New System.Drawing.Size(236, 70)
        '
        'cmsList_SelectInExplorer
        '
        Me.cmsList_SelectInExplorer.Name = "cmsList_SelectInExplorer"
        Me.cmsList_SelectInExplorer.Size = New System.Drawing.Size(235, 22)
        Me.cmsList_SelectInExplorer.Text = "&Select in Explorer"
        '
        'cmsList_EditAlias
        '
        Me.cmsList_EditAlias.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmsList_txtFileAlias, Me.cmsList_ChangeAlias})
        Me.cmsList_EditAlias.Name = "cmsList_EditAlias"
        Me.cmsList_EditAlias.Size = New System.Drawing.Size(235, 22)
        Me.cmsList_EditAlias.Text = "&Edit File Alias"
        '
        'cmsList_txtFileAlias
        '
        Me.cmsList_txtFileAlias.Name = "cmsList_txtFileAlias"
        Me.cmsList_txtFileAlias.Size = New System.Drawing.Size(100, 23)
        '
        'cmsList_ChangeAlias
        '
        Me.cmsList_ChangeAlias.Name = "cmsList_ChangeAlias"
        Me.cmsList_ChangeAlias.Size = New System.Drawing.Size(160, 22)
        Me.cmsList_ChangeAlias.Text = "C&hange"
        '
        'cmsList_DeleteFile
        '
        Me.cmsList_DeleteFile.Name = "cmsList_DeleteFile"
        Me.cmsList_DeleteFile.Size = New System.Drawing.Size(235, 22)
        Me.cmsList_DeleteFile.Text = "&Delete From Database"
        '
        'lblVolumeNo
        '
        Me.lblVolumeNo.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblVolumeNo.Location = New System.Drawing.Point(258, 122)
        Me.lblVolumeNo.Name = "lblVolumeNo"
        Me.lblVolumeNo.Size = New System.Drawing.Size(81, 18)
        Me.lblVolumeNo.TabIndex = 15
        Me.lblVolumeNo.Text = "Volume No."
        Me.lblVolumeNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtVolumeSequence
        '
        Me.txtVolumeSequence.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtVolumeSequence.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtVolumeSequence.Location = New System.Drawing.Point(345, 126)
        Me.txtVolumeSequence.Name = "txtVolumeSequence"
        Me.txtVolumeSequence.ReadOnly = True
        Me.txtVolumeSequence.Size = New System.Drawing.Size(133, 14)
        Me.txtVolumeSequence.TabIndex = 14
        Me.txtVolumeSequence.TabStop = False
        '
        'lblFolder
        '
        Me.lblFolder.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblFolder.Location = New System.Drawing.Point(258, 95)
        Me.lblFolder.Name = "lblFolder"
        Me.lblFolder.Size = New System.Drawing.Size(81, 18)
        Me.lblFolder.TabIndex = 13
        Me.lblFolder.Text = "In Folder"
        Me.lblFolder.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtFolderName
        '
        Me.txtFolderName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtFolderName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFolderName.Location = New System.Drawing.Point(345, 99)
        Me.txtFolderName.Name = "txtFolderName"
        Me.txtFolderName.ReadOnly = True
        Me.txtFolderName.Size = New System.Drawing.Size(133, 14)
        Me.txtFolderName.TabIndex = 12
        Me.txtFolderName.TabStop = False
        '
        'lblVolume
        '
        Me.lblVolume.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblVolume.Location = New System.Drawing.Point(258, 68)
        Me.lblVolume.Name = "lblVolume"
        Me.lblVolume.Size = New System.Drawing.Size(81, 18)
        Me.lblVolume.TabIndex = 11
        Me.lblVolume.Text = "In Volume"
        Me.lblVolume.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtVolumeName
        '
        Me.txtVolumeName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtVolumeName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtVolumeName.Location = New System.Drawing.Point(345, 72)
        Me.txtVolumeName.Name = "txtVolumeName"
        Me.txtVolumeName.ReadOnly = True
        Me.txtVolumeName.Size = New System.Drawing.Size(133, 14)
        Me.txtVolumeName.TabIndex = 10
        Me.txtVolumeName.TabStop = False
        '
        'lblFileType
        '
        Me.lblFileType.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblFileType.Location = New System.Drawing.Point(8, 122)
        Me.lblFileType.Name = "lblFileType"
        Me.lblFileType.Size = New System.Drawing.Size(81, 18)
        Me.lblFileType.TabIndex = 9
        Me.lblFileType.Text = "File Type"
        Me.lblFileType.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtFileType
        '
        Me.txtFileType.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtFileType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFileType.Location = New System.Drawing.Point(95, 123)
        Me.txtFileType.Name = "txtFileType"
        Me.txtFileType.ReadOnly = True
        Me.txtFileType.Size = New System.Drawing.Size(133, 14)
        Me.txtFileType.TabIndex = 8
        Me.txtFileType.TabStop = False
        '
        'lblFileDate
        '
        Me.lblFileDate.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblFileDate.Location = New System.Drawing.Point(8, 95)
        Me.lblFileDate.Name = "lblFileDate"
        Me.lblFileDate.Size = New System.Drawing.Size(81, 18)
        Me.lblFileDate.TabIndex = 7
        Me.lblFileDate.Text = "File Date"
        Me.lblFileDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDateModified
        '
        Me.txtDateModified.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtDateModified.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDateModified.Location = New System.Drawing.Point(95, 96)
        Me.txtDateModified.Name = "txtDateModified"
        Me.txtDateModified.ReadOnly = True
        Me.txtDateModified.Size = New System.Drawing.Size(133, 14)
        Me.txtDateModified.TabIndex = 6
        Me.txtDateModified.TabStop = False
        '
        'lblSize
        '
        Me.lblSize.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblSize.Location = New System.Drawing.Point(8, 68)
        Me.lblSize.Name = "lblSize"
        Me.lblSize.Size = New System.Drawing.Size(81, 18)
        Me.lblSize.TabIndex = 5
        Me.lblSize.Text = "Size"
        Me.lblSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtSize
        '
        Me.txtSize.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSize.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSize.Location = New System.Drawing.Point(95, 69)
        Me.txtSize.Name = "txtSize"
        Me.txtSize.ReadOnly = True
        Me.txtSize.Size = New System.Drawing.Size(133, 14)
        Me.txtSize.TabIndex = 4
        Me.txtSize.TabStop = False
        '
        'lblFileAlias
        '
        Me.lblFileAlias.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblFileAlias.Location = New System.Drawing.Point(8, 41)
        Me.lblFileAlias.Name = "lblFileAlias"
        Me.lblFileAlias.Size = New System.Drawing.Size(81, 18)
        Me.lblFileAlias.TabIndex = 3
        Me.lblFileAlias.Text = "File Alias"
        Me.lblFileAlias.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtFileAlias
        '
        Me.txtFileAlias.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtFileAlias.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFileAlias.Location = New System.Drawing.Point(95, 42)
        Me.txtFileAlias.MaxLength = 255
        Me.txtFileAlias.Name = "txtFileAlias"
        Me.txtFileAlias.ReadOnly = True
        Me.txtFileAlias.Size = New System.Drawing.Size(481, 14)
        Me.txtFileAlias.TabIndex = 2
        Me.txtFileAlias.TabStop = False
        '
        'lblFileName
        '
        Me.lblFileName.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblFileName.Location = New System.Drawing.Point(8, 14)
        Me.lblFileName.Name = "lblFileName"
        Me.lblFileName.Size = New System.Drawing.Size(81, 18)
        Me.lblFileName.TabIndex = 1
        Me.lblFileName.Text = "File Name"
        Me.lblFileName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtFileName
        '
        Me.txtFileName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtFileName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFileName.Location = New System.Drawing.Point(95, 15)
        Me.txtFileName.Name = "txtFileName"
        Me.txtFileName.ReadOnly = True
        Me.txtFileName.Size = New System.Drawing.Size(481, 14)
        Me.txtFileName.TabIndex = 0
        Me.txtFileName.TabStop = False
        '
        'ToolStripSeparator11
        '
        Me.ToolStripSeparator11.Name = "ToolStripSeparator11"
        Me.ToolStripSeparator11.Size = New System.Drawing.Size(6, 6)
        '
        'tStrip
        '
        Me.tStrip.Dock = System.Windows.Forms.DockStyle.None
        Me.tStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tssb_File, Me.tssb_Options, Me.tssb_View, Me.tssb_Help, Me.tssbSpacer, Me.tscmbSearch, Me.cmdSearch, Me.tssbSpacer1, Me.tscmbSearchType})
        Me.tStrip.Location = New System.Drawing.Point(0, 0)
        Me.tStrip.Name = "tStrip"
        Me.tStrip.Size = New System.Drawing.Size(973, 25)
        Me.tStrip.TabIndex = 2
        Me.tStrip.Text = "ToolStrip1"
        '
        'tssb_File
        '
        Me.tssb_File.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tssb_File.DropDownButtonWidth = 16
        Me.tssb_File.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tssb_File_Rescan, Me.tssb_File_Add_Database, Me.tssb_File_Import, Me.ToolStripSeparator2, Me.tssb_File_Open_Database, Me.tssb_File_Create_New, Me.tssb_File_Compact, Me.ToolStripSeparator3, Me.tssb_File_Exit})
        Me.tssb_File.Image = CType(resources.GetObject("tssb_File.Image"), System.Drawing.Image)
        Me.tssb_File.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tssb_File.Name = "tssb_File"
        Me.tssb_File.Size = New System.Drawing.Size(51, 22)
        Me.tssb_File.Text = "&File"
        '
        'tssb_File_Rescan
        '
        Me.tssb_File_Rescan.Name = "tssb_File_Rescan"
        Me.tssb_File_Rescan.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.tssb_File_Rescan.Size = New System.Drawing.Size(278, 22)
        Me.tssb_File_Rescan.Text = "&Rescan For New Media"
        '
        'tssb_File_Add_Database
        '
        Me.tssb_File_Add_Database.Name = "tssb_File_Add_Database"
        Me.tssb_File_Add_Database.ShortcutKeys = System.Windows.Forms.Keys.F12
        Me.tssb_File_Add_Database.Size = New System.Drawing.Size(278, 22)
        Me.tssb_File_Add_Database.Text = "&Add Media To Database"
        '
        'tssb_File_Import
        '
        Me.tssb_File_Import.Name = "tssb_File_Import"
        Me.tssb_File_Import.Size = New System.Drawing.Size(278, 22)
        Me.tssb_File_Import.Text = "&Import..."
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(275, 6)
        '
        'tssb_File_Open_Database
        '
        Me.tssb_File_Open_Database.Name = "tssb_File_Open_Database"
        Me.tssb_File_Open_Database.Size = New System.Drawing.Size(278, 22)
        Me.tssb_File_Open_Database.Text = "&Open Database..."
        '
        'tssb_File_Create_New
        '
        Me.tssb_File_Create_New.Name = "tssb_File_Create_New"
        Me.tssb_File_Create_New.Size = New System.Drawing.Size(278, 22)
        Me.tssb_File_Create_New.Text = "&Create Database"
        '
        'tssb_File_Compact
        '
        Me.tssb_File_Compact.Name = "tssb_File_Compact"
        Me.tssb_File_Compact.Size = New System.Drawing.Size(278, 22)
        Me.tssb_File_Compact.Text = "Compact &Database..."
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(275, 6)
        '
        'tssb_File_Exit
        '
        Me.tssb_File_Exit.Name = "tssb_File_Exit"
        Me.tssb_File_Exit.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.F4), System.Windows.Forms.Keys)
        Me.tssb_File_Exit.Size = New System.Drawing.Size(278, 22)
        Me.tssb_File_Exit.Text = "E&xit"
        '
        'tssb_Options
        '
        Me.tssb_Options.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tssb_Options.DropDownButtonWidth = 16
        Me.tssb_Options.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tssb_Options_Toggle_AutoScan, Me.tssb_Options_Toggle_Balloon_Tips})
        Me.tssb_Options.Image = CType(resources.GetObject("tssb_Options.Image"), System.Drawing.Image)
        Me.tssb_Options.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tssb_Options.Name = "tssb_Options"
        Me.tssb_Options.Size = New System.Drawing.Size(79, 22)
        Me.tssb_Options.Text = "&Options"
        '
        'tssb_Options_Toggle_AutoScan
        '
        Me.tssb_Options_Toggle_AutoScan.Name = "tssb_Options_Toggle_AutoScan"
        Me.tssb_Options_Toggle_AutoScan.Size = New System.Drawing.Size(227, 22)
        Me.tssb_Options_Toggle_AutoScan.Text = "Turn Off &Auto Scan"
        '
        'tssb_Options_Toggle_Balloon_Tips
        '
        Me.tssb_Options_Toggle_Balloon_Tips.Name = "tssb_Options_Toggle_Balloon_Tips"
        Me.tssb_Options_Toggle_Balloon_Tips.Size = New System.Drawing.Size(227, 22)
        Me.tssb_Options_Toggle_Balloon_Tips.Text = "Turn Off &Balloon Tips"
        '
        'tssb_View
        '
        Me.tssb_View.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tssb_View.DropDownButtonWidth = 16
        Me.tssb_View.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tssb_View_Large, Me.tssb_View_Small, Me.tssb_View_List, Me.tssb_View_Details})
        Me.tssb_View.Image = CType(resources.GetObject("tssb_View.Image"), System.Drawing.Image)
        Me.tssb_View.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tssb_View.Name = "tssb_View"
        Me.tssb_View.Size = New System.Drawing.Size(60, 22)
        Me.tssb_View.Text = "&View"
        '
        'tssb_View_Large
        '
        Me.tssb_View_Large.Name = "tssb_View_Large"
        Me.tssb_View_Large.Size = New System.Drawing.Size(166, 22)
        Me.tssb_View_Large.Text = "&Large Icons"
        '
        'tssb_View_Small
        '
        Me.tssb_View_Small.Name = "tssb_View_Small"
        Me.tssb_View_Small.Size = New System.Drawing.Size(166, 22)
        Me.tssb_View_Small.Text = "&Small Icons"
        '
        'tssb_View_List
        '
        Me.tssb_View_List.Name = "tssb_View_List"
        Me.tssb_View_List.Size = New System.Drawing.Size(166, 22)
        Me.tssb_View_List.Text = "Li&st"
        '
        'tssb_View_Details
        '
        Me.tssb_View_Details.Name = "tssb_View_Details"
        Me.tssb_View_Details.Size = New System.Drawing.Size(166, 22)
        Me.tssb_View_Details.Text = "&Details"
        '
        'tssb_Help
        '
        Me.tssb_Help.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tssb_Help.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tssb_Help_Help, Me.ToolStripSeparator1, Me.tssb_Help_About})
        Me.tssb_Help.Image = CType(resources.GetObject("tssb_Help.Image"), System.Drawing.Image)
        Me.tssb_Help.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tssb_Help.Name = "tssb_Help"
        Me.tssb_Help.Size = New System.Drawing.Size(52, 22)
        Me.tssb_Help.Text = "&Help"
        '
        'tssb_Help_Help
        '
        Me.tssb_Help_Help.Name = "tssb_Help_Help"
        Me.tssb_Help_Help.ShortcutKeys = System.Windows.Forms.Keys.F1
        Me.tssb_Help_Help.Size = New System.Drawing.Size(141, 22)
        Me.tssb_Help_Help.Text = "&Help"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(138, 6)
        '
        'tssb_Help_About
        '
        Me.tssb_Help_About.Name = "tssb_Help_About"
        Me.tssb_Help_About.Size = New System.Drawing.Size(141, 22)
        Me.tssb_Help_About.Text = "&About"
        '
        'tssbSpacer
        '
        Me.tssbSpacer.AutoSize = False
        Me.tssbSpacer.Name = "tssbSpacer"
        Me.tssbSpacer.Size = New System.Drawing.Size(100, 22)
        Me.tssbSpacer.Text = "&Search"
        Me.tssbSpacer.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tscmbSearch
        '
        Me.tscmbSearch.AutoSize = False
        Me.tscmbSearch.Name = "tscmbSearch"
        Me.tscmbSearch.Size = New System.Drawing.Size(300, 24)
        '
        'cmdSearch
        '
        Me.cmdSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdSearch.Image = Global.RM_Manager.My.Resources.Resources.Search_tiny
        Me.cmdSearch.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.Size = New System.Drawing.Size(23, 22)
        Me.cmdSearch.ToolTipText = "Search"
        '
        'tssbSpacer1
        '
        Me.tssbSpacer1.Name = "tssbSpacer1"
        Me.tssbSpacer1.Size = New System.Drawing.Size(41, 22)
        Me.tssbSpacer1.Text = "&Type"
        '
        'tscmbSearchType
        '
        Me.tscmbSearchType.AutoSize = False
        Me.tscmbSearchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.tscmbSearchType.Name = "tscmbSearchType"
        Me.tscmbSearchType.Size = New System.Drawing.Size(260, 24)
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1030, 494)
        Me.Controls.Add(Me.tStrip)
        Me.Controls.Add(Me.sStrip)
        Me.Controls.Add(Me.formSplit)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmMain"
        Me.Text = "Main"
        Me.sStrip.ResumeLayout(False)
        Me.sStrip.PerformLayout()
        Me.formSplit.Panel1.ResumeLayout(False)
        Me.formSplit.Panel2.ResumeLayout(False)
        Me.formSplit.ResumeLayout(False)
        Me.cmsTree.ResumeLayout(False)
        Me.infoSplit.Panel1.ResumeLayout(False)
        Me.infoSplit.Panel2.ResumeLayout(False)
        Me.infoSplit.Panel2.PerformLayout()
        Me.infoSplit.ResumeLayout(False)
        Me.cmsList.ResumeLayout(False)
        Me.tStrip.ResumeLayout(False)
        Me.tStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tTip As System.Windows.Forms.ToolTip
    Friend WithEvents sStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents formSplit As System.Windows.Forms.SplitContainer
    Friend WithEvents treeMain As System.Windows.Forms.TreeView
    Friend WithEvents imList As System.Windows.Forms.ImageList
    Friend WithEvents infoSplit As System.Windows.Forms.SplitContainer
    Friend WithEvents listMain As System.Windows.Forms.ListView
    Friend WithEvents lblFileAlias As System.Windows.Forms.Label
    Friend WithEvents txtFileAlias As System.Windows.Forms.TextBox
    Friend WithEvents lblFileName As System.Windows.Forms.Label
    Friend WithEvents txtFileName As System.Windows.Forms.TextBox
    Friend WithEvents lblSize As System.Windows.Forms.Label
    Friend WithEvents txtSize As System.Windows.Forms.TextBox
    Friend WithEvents lblFileType As System.Windows.Forms.Label
    Friend WithEvents txtFileType As System.Windows.Forms.TextBox
    Friend WithEvents lblFileDate As System.Windows.Forms.Label
    Friend WithEvents txtDateModified As System.Windows.Forms.TextBox
    Friend WithEvents lblFolder As System.Windows.Forms.Label
    Friend WithEvents txtFolderName As System.Windows.Forms.TextBox
    Friend WithEvents lblVolume As System.Windows.Forms.Label
    Friend WithEvents txtVolumeName As System.Windows.Forms.TextBox
    Friend WithEvents lblVolumeNo As System.Windows.Forms.Label
    Friend WithEvents txtVolumeSequence As System.Windows.Forms.TextBox
    Friend WithEvents cmbDrives As System.Windows.Forms.ComboBox
    Friend WithEvents cmsTree As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents cmsTree_PrintDiskContents As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmsTree_ChangeSequenceNumber As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmsTree_DeleteDisk As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmsTree_txtChangeVolSequenceNumber As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents cmsTree_cmdChangeSequenceNumber As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmsTree_txtVolName As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmsList As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents cmsList_SelectInExplorer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmsList_DeleteFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmsTree_SelectInExplorer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator11 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents tssb_File As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents tssb_File_Rescan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tssb_File_Add_Database As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tssb_File_Import As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tssb_File_Open_Database As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tssb_File_Create_New As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tssb_File_Compact As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tssb_File_Exit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tssb_Options As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents tssb_Options_Toggle_AutoScan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tssb_Options_Toggle_Balloon_Tips As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tssb_View As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tssb_View_Large As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tssb_View_Small As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tssb_View_List As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tssb_View_Details As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tssb_Help As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents tssb_Help_Help As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tssb_Help_About As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tssbSpacer As System.Windows.Forms.ToolStripLabel
    Friend WithEvents tscmbSearch As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents cmdSearch As System.Windows.Forms.ToolStripButton
    Friend WithEvents tscmbSearchType As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents tssLabelInfo As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tspBar As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents tssLabelScan As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents cmsList_EditAlias As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmsList_txtFileAlias As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents cmsList_ChangeAlias As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tssbSpacer1 As System.Windows.Forms.ToolStripLabel
End Class
