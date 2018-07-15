Imports System.IO
Imports System.String
Imports System.Data
Imports System.Data.Common
Imports System.Data.OleDb

Module clsADONET
    Public Class ADONET
        Public Enum SQLCommand
            SelectCommandReader
            InsertCommand
            DeleteCommand
            UpdateCommand
            SelectStatementReader
            InsertStatement
            DeleteStatement
            UpdateStatement
            InsertProcedure
            DeleteProcedure
            SelectStatementDataSet
            SelectCommandDataSet
        End Enum

        Public dbCon As New OleDbConnection
        Public dbCmd As New OleDbCommand
        Public dbR As OleDbDataReader
        Public dbA As OleDbDataAdapter
        Public dbDS As DataSet
        Public parm As ArrayList

        Public Sub New()
            dbCon = New OleDbConnection
            dbCmd = New OleDbCommand
            dbDS = New DataSet
            dbA = New OleDbDataAdapter
            parm = New ArrayList
            dbCmd.Connection = dbCon '// a little stitch in time saves nine
        End Sub

        Protected Overloads Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                dbCon.Dispose()
                dbCmd.Dispose()
                dbA.Dispose()
                dbDS.Dispose()
            End If
        End Sub

        Public Sub SetParameter(ByRef pName As String, ByRef pVal As Object, ByVal pType As OleDbType, Optional ByVal pDir As ParameterDirection = ParameterDirection.Input)
            Dim p As New OleDbParameter
            Try
                p.Value = pVal
                p.Direction = pDir
                p.ParameterName = pName
                p.OleDbType = pType
                Select Case pType
                    Case OleDbType.VarChar
                        p.Size = pVal.ToString.Length
                    Case OleDbType.Integer
                        p.Size = 4
                    Case OleDbType.BigInt
                        p.Size = 8
                End Select
                parm.Add(p)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Sub

        Public Function ExecuteCommand(ByRef qName As String, ByVal oCmd As ADONET.SQLCommand, Optional ByVal stTableName As String = "") As Boolean
            Try
                With dbCmd
                    .CommandText = qName
                    .Parameters.Clear()
                    For Each p As OleDbParameter In parm
                        .Parameters.Add(p)
                    Next
                End With
                If dbCon.State = ConnectionState.Open Then dbCon.Close()
                If dbCon.State = ConnectionState.Closed Then dbCon.Open()
                Select Case oCmd
                    Case SQLCommand.InsertCommand
                        dbCmd.CommandType = CommandType.StoredProcedure
                        dbCmd.ExecuteNonQuery()

                    Case SQLCommand.UpdateCommand
                        dbCmd.CommandType = CommandType.StoredProcedure
                        dbCmd.ExecuteNonQuery()

                    Case SQLCommand.UpdateStatement
                        dbCmd.CommandType = CommandType.Text
                        dbCmd.ExecuteNonQuery()

                    Case SQLCommand.InsertStatement
                        dbCmd.CommandType = CommandType.Text
                        dbCmd.ExecuteNonQuery()

                    Case SQLCommand.DeleteCommand
                        dbCmd.CommandType = CommandType.StoredProcedure
                        dbCmd.ExecuteNonQuery()

                    Case SQLCommand.DeleteStatement
                        dbCmd.CommandType = CommandType.Text
                        dbCmd.ExecuteNonQuery()

                    Case SQLCommand.SelectCommandReader
                        dbCmd.CommandType = CommandType.StoredProcedure
                        dbR = dbCmd.ExecuteReader(CommandBehavior.SingleResult)

                    Case SQLCommand.SelectStatementReader
                        dbCmd.CommandType = CommandType.Text
                        dbR = dbCmd.ExecuteReader(CommandBehavior.SingleResult)

                    Case SQLCommand.InsertProcedure
                        dbCmd.CommandType = CommandType.Text
                        dbCmd.ExecuteNonQuery()

                    Case SQLCommand.DeleteProcedure
                        dbCmd.CommandType = CommandType.Text
                        dbCmd.ExecuteNonQuery()

                    Case SQLCommand.SelectCommandDataSet
                        dbDS.Clear()
                        dbCmd.CommandType = CommandType.StoredProcedure
                        dbA.SelectCommand = dbCmd
                        dbA.Fill(dbDS, stTableName)

                    Case SQLCommand.SelectStatementDataSet
                        dbDS.Clear()
                        dbCmd.CommandType = CommandType.Text
                        dbA.SelectCommand = dbCmd
                        dbA.Fill(dbDS, stTableName)

                    Case Else

                End Select
                Return True
            Catch ex As Exception
                If oCmd = SQLCommand.InsertProcedure Or oCmd = SQLCommand.DeleteProcedure Then
                    ' do nothing
                Else
                    MessageBox.Show(ex.Message)
                End If
            End Try
        End Function

        Public Sub CloseReader()
            Try
                If dbR IsNot Nothing Then
                    If Not dbR.IsClosed Then dbR.Close()
                    If dbCon.State = ConnectionState.Open Then dbCon.Close()
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Sub

        Public Function GetReaderValue(ByVal colName As String) As String
            GetReaderValue = ""
            Try
                If Not dbR.IsClosed Then
                    If dbR.HasRows And dbR.Read() Then
                        GetReaderValue = dbR(colName).ToString
                    End If
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Function

        Public Function GetReaderEnumerator() As DbEnumerator
            Try
                If Not dbR.IsClosed Then Return CType(dbR.GetEnumerator(), DbEnumerator)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return Nothing
        End Function

        Public Function GetDataReader() As OleDbDataReader
            Try
                If Not dbR.IsClosed Then Return dbR
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return Nothing
        End Function
    End Class
End Module
