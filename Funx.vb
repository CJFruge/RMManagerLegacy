Imports System.IO
Imports System.Text

Module Funx

    Public Function OpenFile(ByRef fName As String, ByRef stTitle As String, _
        ByRef fString As String, ByRef fIndex As Integer, Optional ByVal bFileExists As Boolean = True) As Boolean
        OpenFile = False
        Try
            Dim fd As New OpenFileDialog()
            With fd
                .ShowReadOnly = False
                .Multiselect = False
                .Title = stTitle
                .Filter = fString
                .FilterIndex = fIndex
                .InitialDirectory = Path.GetDirectoryName(fName)
                .FileName = Path.GetFileName(fName)
                .CheckFileExists = bFileExists
                If .ShowDialog() = DialogResult.OK Then
                    fName = .FileName
                    OpenFile = True
                End If
            End With
        Catch ex As Exception
            If ex.TargetSite.Name <> stFileError Then MessageBox.Show(ex.Message)
        End Try
    End Function

    Public Function CompactDB(ByRef stPath As String) As Boolean
        Dim stTemp As String
        Try
            Dim jro As JRO.JetEngine = New JRO.JetEngine()
            With My.Computer.FileSystem
                stTemp = Path.Combine(.SpecialDirectories.Temp, "temp.mdb")
                If .FileExists(stTemp) Then .DeleteFile(stTemp)

                jro.CompactDatabase("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & stPath, _
                "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & stTemp & ";Jet OLEDB:Engine Type=5")

                If .FileExists(stTemp) Then
                    .DeleteFile(stPath)
                    .RenameFile(stTemp, Path.GetFileName(stPath))
                    .MoveFile(Path.Combine(.SpecialDirectories.Temp, Path.GetFileName(stPath)), stPath)
                    CompactDB = True
                End If
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Public Function CreateDatabase(ByRef stPath As String) As Boolean
        With My.Computer.FileSystem
            Dim stTemplate As String = Path.Combine(My.Application.Info.DirectoryPath, c_stTemplateMDB)
            If .FileExists(stTemplate) And Not .FileExists(stPath) Then
                .CopyFile(stTemplate, stPath)
                Dim fi As FileInfo = .GetFileInfo(stPath)
                fi.Attributes = fi.Attributes And Not FileAttributes.ReadOnly
            End If
        End With
    End Function

    Public Function GetVolumeSerialNumber(ByVal dLetter As String) As String
        Dim fsFlags As UInt32
        Dim maxCompLen As UInt32
        Dim fsName As String = ""
        Dim volName As String = ""
        Dim volSerial As String = ""
        GetVolumeInfo(dLetter & ":\", volName, volSerial, maxCompLen, fsFlags, fsName)
        GetVolumeSerialNumber = volSerial
    End Function

    Private Sub GetVolumeInfo( _
  ByVal FolderPath As String, _
  ByRef VolumeName As String, _
  ByRef VolumeSerialNumber As String, _
  ByRef MaxComponentLength As UInt32, _
  ByRef FileSystemFlags As UInt32, _
  ByRef FileSystemName As String)
        ' see http://msdn.microsoft.com/en-us/library/aa364993(VS.85).aspx
        Dim sRootFolder As String = Path.GetPathRoot(FolderPath)
        Const MAX_PATH As Integer = &H104
        Dim volname As New System.Text.StringBuilder(MAX_PATH + 1)
        Dim fsname As New System.Text.StringBuilder(MAX_PATH + 1)
        Dim sernum As UInt32
        Dim RetVal As UInt32 = GetVolumeInformation(sRootFolder, volname, CUInt(volname.Capacity), _
                            sernum, MaxComponentLength, FileSystemFlags, _
                            fsname, CUInt(fsname.Capacity))
        If RetVal = 0 Then Throw New System.ComponentModel.Win32Exception(Err.LastDllError)
        VolumeName = volname.ToString
        FileSystemName = fsname.ToString

        Dim hpart, lpart As Integer
        hpart = CInt(sernum \ 65536)
        lpart = CInt(sernum - hpart * 65536L)
        VolumeSerialNumber = Hex(hpart).PadLeft(4, "0"c) & "-" & Hex(lpart).PadLeft(4, "0"c)
    End Sub

    Private Declare Auto Function GetVolumeInformation Lib "kernel32.dll" ( _
        ByVal RootPathName As String, _
        ByVal VolumeNameBuffer As StringBuilder, _
        ByVal VolumeNameSize As UInt32, _
        ByRef VolumeSerialNumber As UInt32, _
        ByRef MaximumComponentLength As UInt32, _
        ByRef FileSystemFlags As UInt32, _
        ByVal FileSystemNameBuffer As StringBuilder, _
        ByVal FileSystemNameSize As UInt32) As UInt32

    Declare Auto Function SendMessage Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr

    '
    '
    '
    '    Public Function GetSerialNumber(ByRef dLetter As String) As String
    ' Dim ValidDriveLetters As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
    '     If ValidDriveLetters.IndexOf(dLetter) <> -1 Then
    ' Dim moReturn As ManagementObjectCollection
    ' Dim moSearch As ManagementObjectSearcher
    ' Dim mo As ManagementObject
    '        GetSerialNumber = ""
    '        Try
    '            moSearch = New ManagementObjectSearcher("Select * from Win32_LogicalDisk where Name = '" & dLetter & ":'")
    '            moReturn = moSearch.Get()
    '            For Each mo In moReturn
    '                GetSerialNumber = mo("VolumeSerialNumber").ToString
    '            Next
    '        Catch ex As Exception
    '            MessageBox.Show(ex.Message)
    '        End Try
    '    End If
    '    Return Nothing
    'End Function
End Module
