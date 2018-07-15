Imports System.IO
Imports System.String
Imports System.Data
Imports System.Data.OleDb
Imports System.Runtime.InteropServices

Module clsMdb2Ado

    Class Mdb2Ado

        Private Const stConn As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="
        'Private dbCon As New OleDbConnection
        'Private dbConA As New OleDbConnection
        'Private dbConI As New OleDbConnection
        'Private dR As OleDbDataReader
        'Private dRA As OleDbDataReader
        'Private dRI As New DataSet
        'Private dCmd As New OleDbCommand
        'Private dCmdA As New OleDbCommand
        'Private dCmdI As New OleDbCommand
        'Private jpCollection As New ArrayList
        Dim dbD As New ADONET
        Dim dbA As New ADONET
        Dim dbI As New ADONET

        Public Sub OpenMDB(ByVal stFilePath As String)
            Try
                dbD.dbCon.ConnectionString = stConn & stFilePath
                dbA.dbCon.ConnectionString = dbD.dbCon.ConnectionString
                dbI.dbCon.ConnectionString = dbD.dbCon.ConnectionString
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Sub

        Public Sub CloseMDB()
            If dbD.dbCon.State = ConnectionState.Open Then dbD.dbCon.Close()
            If dbA.dbCon.State = ConnectionState.Open Then dbA.dbCon.Close()
            If dbI.dbCon.State = ConnectionState.Open Then dbI.dbCon.Close()
        End Sub

        Public Function ClearParameters() As Boolean
            dbD.parm.Clear()
        End Function

        Public Function AddProcedure(ByVal spDef As String) As Boolean
            AddProcedure = dbD.ExecuteCommand(spDef, ADONET.SQLCommand.InsertProcedure)
        End Function

        Public Function DropProcedure(ByVal spName As String) As Boolean
            DropProcedure = dbD.ExecuteCommand(spName, ADONET.SQLCommand.DeleteProcedure)
        End Function

        Public Function SetParameter(ByRef pName As String, ByRef pVal As Object, ByVal pType As OleDbType, ByVal pDir As ParameterDirection) As Boolean
            dbD.SetParameter(pName, pVal, pType, pDir)
        End Function

        Public Function ExecuteCommand(ByRef qName As String, ByVal oCmd As ADONET.SQLCommand) As Boolean
            Return dbD.ExecuteCommand(qName, oCmd)
        End Function

        Public Function GetReaderValue(ByVal colName As String) As String
            Return dbD.GetReaderValue(colName)
        End Function

        Public Function GetReaderValueAsync(ByVal colName As String) As String
            Return dbA.GetReaderValue(colName)
        End Function

        Public Function GetReaderRowsAsync(ByRef qName As String, Optional ByVal pType As OleDbType = OleDbType.Integer, _
                                           Optional ByRef pName As String = "", Optional ByRef pVal As Object = Nothing) As IEnumerator

            dbA.parm.Clear()
            dbA.SetParameter(pName, pVal, pType, ParameterDirection.Input)
            dbA.ExecuteCommand(qName, ADONET.SQLCommand.SelectCommand)
            Return dbA.GetReaderRows()
        End Function

        Public Function GetReaderRowsAsync(ByRef qName As String, ByRef pName() As String, ByVal pType() As OleDbType, ByRef pVal() As Object) As IEnumerator
            If UBound(pName) = UBound(pVal) And UBound(pName) = UBound(pType) Then
                dbA.parm.Clear()
                For j As Integer = 0 To UBound(pName)
                    dbA.SetParameter(pName(j), pVal(j), pType(j), ParameterDirection.Input)
                Next
                dbA.ExecuteCommand(qName, ADONET.SQLCommand.SelectCommand)
            End If
            Return dbA.GetReaderRows()
        End Function

        Public Function GetIDataSet(ByRef qName As String, Optional ByVal pType As OleDbType = OleDbType.Integer, _
                                           Optional ByRef pName As String = "", Optional ByRef pVal As Object = Nothing) As DataSet
            Dim parm As New OleDbParameter
            Dim oAdapter As New OleDbDataAdapter
            Try
                With dbI.dbCmd
                    .CommandText = qName
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.Clear()
                End With
                If pVal IsNot Nothing Then
                    With parm
                        .Direction = ParameterDirection.Input
                        .Value = pVal
                        .Size = CInt(IIf(pType = OleDbType.VarChar, pVal.ToString.Length, 4))
                        .ParameterName = pName
                    End With
                End If
                dbI.dbCmd.Parameters.Add(parm)
                If dbI.dbCon.State = ConnectionState.Open Then dbI.dbCon.Close()
                If dbI.dbCon.State = ConnectionState.Closed Then dbI.dbCon.Open()
                dbI.dbDS.Clear()
                oAdapter.SelectCommand = dbI.dbCmd
                oAdapter.Fill(dbI.dbDS, "DiskContents")
                Return dbI.dbDS
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return Nothing
        End Function

        Public Function GetReaderRows() As IEnumerator
            Return dbD.GetReaderRows()
        End Function

        Public Sub CloseReader()
            dbD.CloseReader()
        End Sub

        Public Sub CloseIReader()
            dbI.CloseReader()
        End Sub

        Public Sub CloseReaderAsync()
            dbA.CloseReader()
        End Sub
    End Class
End Module

