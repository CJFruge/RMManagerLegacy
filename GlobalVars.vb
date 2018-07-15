Module GlobalVars

    Public Structure UserOptions
        Dim RMMFilePath As String
        Dim SearchType As String
        Dim lastSearch As String
        Dim ListView As View
        Dim AutoScan As Boolean
        Dim infoHeight As Integer
        Dim treeWidth As Integer
        Dim bBalloons As Boolean
    End Structure

    Public uOpt As UserOptions
    Public lvmColWidth(10) As Integer
    Public lvsColWidth(10) As Integer
    Public xFH As New XmlFileHandler

End Module
