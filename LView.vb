Imports System.Runtime.InteropServices
Imports System.ComponentModel
Imports System.Runtime.CompilerServices

Module LView

    '    <EditorBrowsable(EditorBrowsableState.Never)> 
    Public Class ListViewExtensions

        <StructLayout(LayoutKind.Sequential)> _
        Public Structure LVCOLUMN
            Public mask As Int32
            Public cx As Int32
            <MarshalAs(UnmanagedType.LPTStr)> _
            Public pszText As String
            Public hbm As IntPtr
            Public cchTextMax As Int32
            Public fmt As Int32
            Public iSubItem As Int32
            Public iImage As Int32
            Public iOrder As Int32
        End Structure

        Private Const HDI_FORMAT As Int32 = &H4
        Private Const HDF_SORTUP As Int32 = &H400
        Private Const HDF_SORTDOWN As Int32 = &H200
        Private Const LVM_GETHEADER As Int32 = &H101F
        Private Const HDM_GETITEM As Int32 = &H120B
        Private Const HDM_SETITEM As Int32 = &H120C

        '[System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "SendMessage")]
        'private static extern IntPtr SendMessageLVCOLUMN(IntPtr hWnd, Int32 Msg, IntPtr wParam, ref LVCOLUMN lPLVCOLUMN);
        Declare Auto Function SendMessage Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
        Declare Auto Function SendMessageLV Lib "user32.dll" Alias "SendMessage" (ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wParam As IntPtr, ByRef lParam As LVCOLUMN) As IntPtr

        'this System.Windows.Forms.ListView ListViewControl
        Public Sub SetSortIcon(ByVal lvCtl As ListView, ByVal ColumnIndex As Integer, ByVal Order As SortOrder)

            Dim ColumnHeader As IntPtr = SendMessage(lvCtl.Handle, LVM_GETHEADER, IntPtr.Zero, IntPtr.Zero)

            For ColumnNumber As Integer = 0 To lvCtl.Columns.Count - 1

                Dim ColumnPtr As IntPtr = New IntPtr(ColumnNumber)
                Dim lvColumn As LVCOLUMN = New LVCOLUMN()
                lvColumn.mask = HDI_FORMAT
                SendMessageLV(ColumnHeader, HDM_GETITEM, ColumnPtr, lvColumn)

                If Order = SortOrder.None And ColumnNumber = ColumnIndex Then
                    Select Case Order
                        Case SortOrder.Ascending
                            lvColumn.fmt = lvColumn.fmt And (Not HDF_SORTDOWN)
                            lvColumn.fmt = lvColumn.fmt Or HDF_SORTUP

                        Case SortOrder.Descending
                            lvColumn.fmt = lvColumn.fmt And (Not HDF_SORTUP)
                            lvColumn.fmt = lvColumn.fmt Or HDF_SORTDOWN
                    End Select
                Else
                    lvColumn.fmt = lvColumn.fmt And (Not HDF_SORTDOWN) And (Not HDF_SORTUP)
                End If
                SendMessageLV(ColumnHeader, HDM_SETITEM, ColumnPtr, lvColumn)
            Next
        End Sub
    End Class
    Public Function GetSystemList(Optional ByVal bLarge As Boolean = False) As IntPtr
        Dim hImg As IntPtr
        Dim shInfo As New SHFILEINFO()
        shInfo.szDisplayName = New String(Chr(0), cDisplayNameSize)
        shInfo.szTypeName = New String(Chr(0), cTypeNameSize)
        Dim sFlag As Integer = CInt(IIf(bLarge, SHGFI.LargeIcon, SHGFI.SmallIcon))
        Try
            hImg = SHGetFileInfo("C:\", 0, shInfo, Marshal.SizeOf(shInfo), SHGFI.Icon Or SHGFI.SysIconIndex Or sFlag)
            Return hImg
        Catch ex As Exception
            Throw ex
        End Try
        Return Nothing
    End Function

    Public Function GetSystemIconIndex(ByVal stExt As String, Optional ByVal bLarge As Boolean = False) As Integer
        Dim hImg As IntPtr
        Dim shInfo As New SHFILEINFO()
        shInfo.szDisplayName = New String(Chr(0), cDisplayNameSize)
        shInfo.szTypeName = New String(Chr(0), cTypeNameSize)
        Dim sFlag As Integer = CInt(IIf(bLarge, SHGFI.LargeIcon, SHGFI.SmallIcon))
        Try
            hImg = SHGetFileInfo(stExt, cFILE_ATTRIBUTE_TEMPORARY, shInfo, Marshal.SizeOf(shInfo), SHGFI.Icon Or sFlag Or SHGFI.UseFileAttributes)
            Return shInfo.iIcon
        Catch ex As Exception
            Throw ex
        End Try
        Return Nothing
    End Function

End Module
