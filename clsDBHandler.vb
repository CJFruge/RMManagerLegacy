Imports System.IO
Imports System.Data
Imports System.Data.Common
Imports System.Text
Imports System.Data.OleDb
Imports System.ComponentModel
'Imports System.Runtime.InteropServices
Module clsDBHandler

    Class DBHandler
        Inherits BackgroundWorker
        Private db As Mdb2Ado
        Private m_diskID As Integer
        Private m_cntFolders As Integer
        Private m_cntFiles As Integer
        Private m_total As Integer
        Private m_FolderSeq As Integer
        Private m_DiskSeq As Integer
        Public mt_stDrive As String
        Public mt_bAdd2DB As Boolean
        Public mt_DriveType As Integer

        Public Sub New()
            db = New Mdb2Ado
            WorkerReportsProgress = True
        End Sub

        Public Function OpenMDB(ByVal stPath As String, ByVal bForceProcUpdate As Boolean) As Boolean
            db.OpenMDB(stPath)
            OpenMDB = GetToken("GUID") = c_stGUID
            AddProcedures(bForceProcUpdate)
        End Function

        Public Sub CloseMDB()
            db.CloseMDB()
        End Sub

        Private Function AddProcedures(ByVal bForce As Boolean) As Boolean
            xFH.OpenProcDefs()
            Dim stVersion = xFH.GetProcsVersion
            If GetToken("PROCSADDED") <> stVersion Or bForce Then
                Dim stProcName As String = ""
                Dim stProcDef As String = ""
                Do While xFH.GetProc(stProcName, stProcDef)
                    db.DropProcedure("DROP TABLE " & stProcName)
                    db.AddProcedure("CREATE PROC " & stProcName & " " & stProcDef)
                Loop
                xFH.CloseProcDefs()
                SetToken("PROCSADDED", stVersion)
            End If
        End Function

        Private Function SaveDiskInfo(ByRef stDisk As String) As Integer
            Dim stVolLabel As String = stDisk.Substring(3)
            If stVolLabel = "" Then stVolLabel = "No Label"
            Dim stDriveLetter As String = stDisk.Substring(0, 1)
            Dim stUneek As String = stVolLabel & "_" & GetVolumeSerialNumber(stDriveLetter)
            Try
                'PARAMETERS inName Text ( 255 ), inUneekID Text ( 255 ), inType Long, inSequence Long;
                db.ClearParameters()
                db.SetParameter("inName", stVolLabel.ToString, OleDb.OleDbType.VarChar, ParameterDirection.Input)
                db.SetParameter("inUneekID", stUneek.ToString, OleDb.OleDbType.VarChar, ParameterDirection.Input)
                db.SetParameter("inType", CInt(mt_DriveType), OleDb.OleDbType.Integer, ParameterDirection.Input)
                db.SetParameter("inSequence", CInt(m_DiskSeq), OleDb.OleDbType.Integer, ParameterDirection.Input)
                db.ExecuteCommand("sp_add_Disk", ADONET.SQLCommand.InsertCommand)
                '
                ' get the disk id because msaccess cant send back params
                ' PARAMETERS inUneekID Text ( 255 );
                db.ClearParameters()
                db.SetParameter("inUneekID", stUneek.ToString, OleDb.OleDbType.VarChar, ParameterDirection.Input)
                db.ExecuteCommand("sp_GetDiskID", ADONET.SQLCommand.SelectCommandReader)
                m_diskID = CInt(db.GetReaderValue("Disk_ID"))
                db.CloseReader()
                If m_diskID > 0 Then
                    m_FolderSeq = 0
                    SaveDiskInfo = SaveFolderInfo(stVolLabel, Now(), m_diskID * -1)
                End If
            Catch ex As Exception
                Throw ex
                'MessageBox.Show(ex.Message)
            End Try
        End Function

        Private Function SaveFolderInfo(ByRef stName As String, ByRef lastWriteTime As Date, ByRef ParentID As Integer) As Integer
            Try
                'PARAMETERS inName Text ( 255 ), inParentID Long, inDiskID Long, inSeqID Long, inDateModified DateTime;
                m_FolderSeq += 1
                db.ClearParameters()
                db.SetParameter("inName", stName.ToString, OleDb.OleDbType.VarChar, ParameterDirection.Input)
                db.SetParameter("inParentID", CInt(ParentID), OleDb.OleDbType.Integer, ParameterDirection.Input)
                db.SetParameter("inDiskID", CInt(m_diskID), OleDb.OleDbType.Integer, ParameterDirection.Input)
                db.SetParameter("inSeqID", CInt(m_FolderSeq), OleDb.OleDbType.Integer, ParameterDirection.Input)
                db.SetParameter("inDateModified", CDate(lastWriteTime), OleDb.OleDbType.Date, ParameterDirection.Input)
                db.ExecuteCommand("sp_add_Folder", ADONET.SQLCommand.InsertCommand)
                '
                ' sigh, get the folder id because msaccess cant send back params
                'PARAMETERS inDiskID Long, inParentID Long, inSeqID Long;
                db.ClearParameters()
                db.SetParameter("inDiskID", CInt(m_diskID), OleDb.OleDbType.Integer, ParameterDirection.Input)
                db.SetParameter("inParentID", CInt(ParentID), OleDb.OleDbType.Integer, ParameterDirection.Input)
                db.SetParameter("inSeqID", CInt(m_FolderSeq), OleDb.OleDbType.Integer, ParameterDirection.Input)
                db.ExecuteCommand("sp_GetFolderID", ADONET.SQLCommand.SelectCommandReader)
                SaveFolderInfo = CInt(db.GetReaderValue("Folder_ID"))
                db.CloseReader()
            Catch ex As Exception
                Throw ex
                'MessageBox.Show(ex.Message)
            End Try
        End Function

        Private Function SaveFileInfo(ByRef oFile As FileInfo, ByRef folderID As Integer) As Integer
            Try
                Dim stAlias As String = oFile.Name.Replace("_", " ")
                'PARAMETERS inName Text ( 255 ), inAlias Text ( 255 ), inDateCreated DateTime, inExtension Text ( 255 ), inSize Long, 
                'inCommand Text ( 255 ), inFolderID Long, inDiskID Long;
                db.ClearParameters()
                db.SetParameter("inName", oFile.Name.ToString, OleDb.OleDbType.VarChar, ParameterDirection.Input)
                db.SetParameter("inAlias", stAlias.ToString, OleDb.OleDbType.VarChar, ParameterDirection.Input)
                db.SetParameter("inDateModified", oFile.LastWriteTime.ToLocalTime, OleDb.OleDbType.Date, ParameterDirection.Input)
                db.SetParameter("inExtension", oFile.Extension.ToString, OleDb.OleDbType.VarChar, ParameterDirection.Input)
                db.SetParameter("inSize", CInt(oFile.Length / 1024), OleDb.OleDbType.Integer, ParameterDirection.Input)
                db.SetParameter("inCommand", "empty", OleDb.OleDbType.VarChar, ParameterDirection.Input)
                db.SetParameter("inFolderID", CInt(folderID), OleDb.OleDbType.Integer, ParameterDirection.Input)
                db.SetParameter("inDiskID", CInt(m_diskID), OleDb.OleDbType.Integer, ParameterDirection.Input)
                db.ExecuteCommand("sp_add_File", ADONET.SQLCommand.InsertCommand)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetAll(ByVal worker As BackgroundWorker, ByVal e As DoWorkEventArgs) As String
            Dim stMax As String
            If mt_bAdd2DB Then
                db.ExecuteCommand("sp_GetMaxDiskSequence", ADONET.SQLCommand.SelectCommandReader)
                stMax = db.GetReaderValue("MaxSequence")
                If stMax = "" Then stMax = "0"
                m_DiskSeq = CInt(stMax) + 1
                db.CloseReader()
            End If
            GetAll = GetDrive(mt_stDrive, mt_bAdd2DB)
        End Function

        Public Function GetDrive(ByRef stDrive As String, Optional ByVal bAdd2DB As Boolean = False) As String
            Try
                Dim oDir As New DirectoryInfo(stDrive.Substring(0, 2) & "\")
                Dim ID As Integer
                Dim stResults As String
                m_total = m_cntFiles + m_cntFolders
                m_cntFiles = 0
                m_cntFolders = 0
                If m_total = 0 Then m_total = 1
                If bAdd2DB Then ID = SaveDiskInfo(stDrive)
                Recurse(oDir, ID, bAdd2DB)
                If bAdd2DB Then ReportProgress(100)
                stResults = IIf(bAdd2DB, "Added ", "Found ").ToString
                stResults &= m_cntFiles & " files, and " & m_cntFolders & " folders"
                GetDrive = stResults
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Private Function Recurse(ByRef oDir As DirectoryInfo, ByRef parentID As Integer, ByRef bAdd As Boolean) As Boolean
            Dim oSubDirs() As DirectoryInfo
            Dim folderID As Integer
            Dim oFiles() As FileInfo

            oFiles = oDir.GetFiles()
            m_cntFiles += oFiles.Length
            For Each oFile In oFiles
                If bAdd Then SaveFileInfo(oFile, parentID)
            Next

            If bAdd Then ReportProgress(CInt((m_cntFiles + m_cntFolders) / m_total * 100) Mod 100)

            oSubDirs = oDir.GetDirectories()
            m_cntFolders += oSubDirs.Length
            For Each oSubDir In oSubDirs
                If bAdd Then folderID = SaveFolderInfo(oSubDir.Name, CDate(oDir.LastWriteTime.ToLocalTime), parentID)
                Recurse(oSubDir, folderID, bAdd)
            Next
        End Function

        Private Sub DBHandler_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles Me.DoWork
            Dim worker As BackgroundWorker = CType(sender, BackgroundWorker)
            e.Result = GetAll(worker, e)
        End Sub

        'Public Function GetConnectionString() As String
        '    Return db.GetConnString()
        'End Function

        Public Function GetRootFolders(ByVal bSync As Boolean) As DbEnumerator
            Dim stQuery As String = "sp_ctl_GetRootFolders"
            If bSync Then
                Return db.GetReaderRows(stQuery)
            Else
                Return db.GetReaderRowsAsync(stQuery)
            End If
        End Function

        Public Function GetChildFolders(ByRef inParentID As Integer) As DbEnumerator
            Return db.GetReaderRowsAsync("sp_ctl_GetChildFolders", , "inParentID", CInt(inParentID))
        End Function

        Public Function GetParentFolder(ByRef inFolderID As Integer) As DbEnumerator
            Return db.GetReaderRowsAsync("sp_GetParentFolder", , "inFolderID", CInt(inFolderID))
        End Function

        Public Function GetFolderContents(ByRef inFolderID As Integer) As DbEnumerator
            Return db.GetReaderRowsAsync("sp_ctl_GetFolderContents", , "inFolderID", CInt(inFolderID))
        End Function

        Public Function GetSearchAll(ByRef stSearch As String, ByVal stQueryName As String) As DbEnumerator
            Dim dt() As String = stSearch.Split(CChar(" "))
            If dt.Length = 1 Then
                Return db.GetReaderRowsAsync(stQueryName, OleDb.OleDbType.VarChar, "inSearchName", stSearch.ToString)
            Else
                If dt.Length = 2 Then
                    Dim pType(dt.Length - 1) As OleDbType
                    Dim pName(dt.Length - 1) As String
                    Dim pVal(dt.Length - 1) As Object
                    pName(0) = "inSearchFrom"
                    pType(0) = OleDbType.Date
                    pVal(0) = dt(0).ToString
                    pName(1) = "inSearchTo"
                    pType(1) = OleDbType.Date
                    pVal(1) = dt(1).ToString
                    Return db.GetReaderRowsAsync(stQueryName, pName, pType, pVal)
                End If
            End If
            Return Nothing
        End Function

        Public Function GetDiskOrFolderInfo(ByRef stQueryName As String, ByRef pName As String, ByRef inID As Integer) As DbEnumerator
            Return db.GetReaderRowsAsync(stQueryName, , pName, CInt(inID))
        End Function

        Public Function SetValue(ByRef stQueryName As String, ByRef idName As String, ByRef ID As Integer, ByRef valName As String, ByRef Value As Object) As Boolean
            db.ClearParameters()
            db.SetParameter(idName, CInt(ID), OleDb.OleDbType.Integer, ParameterDirection.Input)
            db.SetParameter(valName, Value, OleDb.OleDbType.VarChar, ParameterDirection.Input)
            SetValue = db.ExecuteCommand(stQueryName, ADONET.SQLCommand.UpdateCommand)
        End Function

        Public Function DeleteDisk(ByRef diskID As Integer) As Boolean
            db.ClearParameters()
            db.SetParameter("inDiskID", CInt(diskID), OleDb.OleDbType.Integer, ParameterDirection.Input)
            Return db.ExecuteCommand("sp_upd_DeleteDisk", ADONET.SQLCommand.DeleteCommand)
        End Function

        Public Function GetDiskUneekID(ByRef stQueryName As String, ByRef idName As String, ByRef ID As Integer) As String
            GetDiskUneekID = ""
            db.GetReaderRowsAsync(stQueryName, , idName, CInt(ID))
            GetDiskUneekID = db.GetReaderValueAsync("UneekID").ToString
            db.CloseReaderAsync()
        End Function

        Public Function GetFolderID(ByRef fileID As Integer) As Integer
            db.ClearParameters()
            db.SetParameter("inFileID", CInt(fileID), OleDb.OleDbType.Integer, ParameterDirection.Input)
            db.GetReaderRowsAsync("sp_GetFolderID_FileID", , "inFileID", CInt(fileID))
            GetFolderID = CInt(db.GetReaderValueAsync("Folder_ID"))
            db.CloseReaderAsync()
        End Function

        Public Function GetIDataSet(ByRef qName As String, ByRef pName As String, ByRef pValue As Object) As DataSet
            Return db.GetIDataSet(qName, , pName, pValue)
        End Function

        Public Function GetToken(ByRef stKeyName As String) As String
            GetToken = ""
            Dim stSql As String = c_dbGetToken & stKeyName & "'"
            db.ExecuteCommand(stSql, ADONET.SQLCommand.SelectStatementReader)
            GetToken = db.GetReaderValue("Value")
            db.CloseReader()
        End Function

        Public Function SetToken(ByRef stKeyName As String, ByVal stValue As String) As String
            SetToken = ""
            Dim stSql As String = c_dbSetToken1 & stValue & "'" & c_dbSetToken2 & stKeyName & "'"
            db.ExecuteCommand(stSql, ADONET.SQLCommand.UpdateStatement)
        End Function

        Public Sub CloseReaderAsync()
            db.CloseReaderAsync()
        End Sub
    End Class
End Module

