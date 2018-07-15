Imports System.IO
Imports System.Data
Imports Microsoft.Reporting.WinForms

Public Class frmReport
    Private m_QueryArg As DataSet

    Property QueryArg() As DataSet
        Get
            Return m_QueryArg
        End Get
        Set(ByVal value As DataSet)
            m_QueryArg = value
        End Set
    End Property

    Private Sub frmReport_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        m_QueryArg.Clear()
        m_QueryArg.Dispose()
    End Sub

    Private Sub frmReport_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            Dim rd As New ReportDataSource("DataSet_v_rpt_DiskContents", m_QueryArg.Tables(0))
            rViewer.Reset()
            rViewer.ProcessingMode = ProcessingMode.Local
            With rViewer.LocalReport
                .ReportPath = Path.Combine(My.Application.Info.DirectoryPath, "DiskContents.rdlc")
                '.ReportEmbeddedResource = "My.Resources.DiskContents"
                .DataSources.Clear()
                .DataSources.Add(rd)
            End With
            rViewer.RefreshReport()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class