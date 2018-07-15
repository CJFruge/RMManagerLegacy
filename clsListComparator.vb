Module clsListComparator

    Public Class ListItemComparer

        Implements IComparer
        Private col As Integer
        Private sord As SortOrder

        Public Sub New()
            col = 0
        End Sub

        Public Sub SetSort(ByVal column As Integer, ByVal order As SortOrder)
            col = column
            sord = order
        End Sub

        Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
            Dim rtn As Integer
            If CType(x, ListViewItem).SubItems("Type").Text = "File Folder" Or CType(y, ListViewItem).SubItems("Type").Text = "File Folder" Then
                Return 0
            End If
            rtn = [String].Compare(CType(x, ListViewItem).SubItems(col).Text, CType(y, ListViewItem).SubItems(col).Text)
            If sord = SortOrder.Descending Then rtn *= -1
            Return rtn
        End Function
    End Class
End Module
