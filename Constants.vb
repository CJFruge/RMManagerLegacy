Module Constants

    Public Const c_stGUID As String = "500c9ffe-495e-4e86-b8cf-2cc2716c38a6"
    Public Const c_ANSIDateFormat As String = "{0:yyyy-MM-dd hh:mm tt}"
    Public Const c_stTemplateMDB As String = "TEMPLATE.dat"

    Public Const stFileError As String = "WinIOError"
    Public Const c_fMDB As String = "All Files (*.*)|*.*|RM Manager Database Files (*.MDB)|*.mdb"
    Public Const c_fIndexALL As Integer = 1
    Public Const c_fIndexMDB As Integer = 2

    '// sql proc defs
    '// these statements determine procedure drop/add status & must be hard-coded
    Public Const c_dbGetToken As String = "SELECT DBInfo.Value FROM DBInfo WHERE DBInfo.Key = '"
    Public Const c_dbSetToken1 As String = "UPDATE DBInfo SET DBInfo.Value = '"
    Public Const c_dbSetToken2 As String = " WHERE DBInfo.Key = '"

End Module
