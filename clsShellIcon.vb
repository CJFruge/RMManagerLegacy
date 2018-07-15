Imports System.Runtime.InteropServices
Imports System.Drawing
Module clsShellIcon
    Public sIcon As New ShellIcon

    Public Class ShellIcon
        Public Const LVSIL_SMALL As Integer = 1
        Public Const LVSIL_NORMAL As Integer = 0
        Public Const LVM_SETIMAGELIST As Integer = &H1003
        Private Const cFILE_ATTRIBUTE_NORMAL As Integer = &H80
        Private Const cFILE_ATTRIBUTE_TEMPORARY As Integer = &H100
        Private Const cDisplayNameSize As Integer = 260
        Private Const cTypeNameSize As Integer = 80
        Private m_IconType As String
        Private m_imLarge As New ImageList
        Private m_imSmall As New ImageList
        Private m_htIconType As New Hashtable

        Public Enum IconSize As Integer
            Large = 0
            Small = 1
        End Enum

        Public Sub New()
            m_imSmall.Images.Add("RM_Manager.UPARROW", My.Resources.UPARROW)
            m_imSmall.Images.Add("RM_Manager.DNARROW", My.Resources.DNARROW)
            m_imSmall.Images.Add("RM_Manager.FolderClose", My.Resources.FolderClose)
            m_imLarge.Images.Add("RM_Manager.FolderClose", My.Resources.FolderClose)
            m_imLarge.ImageSize = New Size(32, 32)
        End Sub

        Protected Overloads Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                m_imLarge.Dispose()
                m_imSmall.Dispose()
            End If
            Dispose(disposing)
        End Sub

        Private Enum SHGFI As Integer
            '// <summary>get icon</summary>
            Icon = &H100
            '// <summary>get display name</summary>
            DisplayName = &H200
            '// <summary>get type name</summary>
            TypeName = &H400
            '// <summary>get attributes</summary>
            Attributes = &H800
            '// <summary>get icon location</summary>
            IconLocation = &H1000
            '// <summary>return exe type</summary>
            ExeType = &H2000
            '// <summary>get system icon index</summary>
            SysIconIndex = &H4000
            '// <summary>put a link overlay on icon</summary>
            LinkOverlay = &H8000
            '// <summary>show icon in selected state</summary>
            Selected = &H10000
            '// <summary>get only specified attributes</summary>
            Attr_Specified = &H20000
            '// <summary>get large icon</summary>
            LargeIcon = &H0
            '// <summary>get small icon</summary>
            SmallIcon = &H1
            '// <summary>get open icon</summary>
            OpenIcon = &H2
            '// <summary>get shell size icon</summary>
            ShellIconSize = &H4
            '// <summary>pszPath is a pidl</summary>
            PIDL = &H8
            '// <summary>use passed dwFileAttribute</summary>
            UseFileAttributes = &H10
            '// <summary>apply the appropriate overlays</summary>
            AddOverlays = &H20
            '// <summary>Get the index of the overlay in the upper 8 bits of the iIcon</summary>
            OverlayIndex = &H40
        End Enum

        <StructLayout(LayoutKind.Sequential)> _
        Private Structure SHFILEINFO
            Public hIcon As IntPtr ' : icon
            Public iIcon As Integer ' : icondex
            Public dwAttributes As Integer ' : SFGAO_ flags
            <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=cDisplayNameSize)> _
            Public szDisplayName As String
            <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=cTypeNameSize)> _
            Public szTypeName As String
        End Structure

        Private Function GetIconAPI(ByVal stExt As String, ByVal szIcon As IconSize) As Icon
            Dim hImg As IntPtr
            Dim shInfo As New SHFILEINFO()
            shInfo.szDisplayName = New String(Chr(0), cDisplayNameSize)
            shInfo.szTypeName = New String(Chr(0), cTypeNameSize)
            Dim sFlag As Integer = CInt(IIf(szIcon = IconSize.Large, SHGFI.LargeIcon, SHGFI.SmallIcon))
            Try
                hImg = SHGetFileInfo(stExt, cFILE_ATTRIBUTE_TEMPORARY, shInfo, Marshal.SizeOf(shInfo), SHGFI.Icon Or sFlag Or SHGFI.UseFileAttributes Or SHGFI.TypeName)
                Dim ico As Icon = CType(Icon.FromHandle(shInfo.hIcon).Clone(), Icon)
                DestroyIcon(shInfo.hIcon)
                m_IconType = shInfo.szTypeName
                Return ico
            Catch ex As Exception
                Throw ex
            End Try
            Return Nothing
        End Function

        Public Function GetIconIndex(ByRef stExt As String, ByVal szIcon As IconSize) As Integer
            If Not m_imLarge.Images.ContainsKey(stExt.ToUpper) Then
                m_imLarge.Images.Add(stExt.ToUpper, GetIconAPI(stExt, IconSize.Large))
            End If
            If Not m_imSmall.Images.ContainsKey(stExt.ToUpper) Then
                m_imSmall.Images.Add(stExt.ToUpper, GetIconAPI(stExt, IconSize.Small))
            End If
            If Not m_htIconType.ContainsKey(stExt.ToUpper) Then
                m_htIconType.Add(stExt.ToUpper, m_IconType)
            End If
            Return CInt(IIf(szIcon = IconSize.Large, m_imLarge.Images.IndexOfKey(stExt.ToUpper), m_imSmall.Images.IndexOfKey(stExt.ToUpper)))
        End Function

        Public Function GetIconType(ByRef stExt As String) As String
            Return m_htIconType(stExt.ToUpper).ToString
        End Function

        Public Function ImList(ByVal szIcon As IconSize) As ImageList
            Return CType(IIf(szIcon = IconSize.Large, m_imLarge, m_imSmall), ImageList)
        End Function
        '
        '
        'API
        Private Declare Ansi Function SHGetFileInfo Lib "shell32.dll" ( _
            ByVal pszPath As String, _
            ByVal dwFileAttributes As Integer, _
            ByRef psfi As SHFILEINFO, _
            ByVal cbFileInfo As Integer, _
            ByVal uFlags As Integer) As IntPtr

        Public Declare Function DestroyIcon Lib "user32.dll" (ByVal hIcon As IntPtr) As Integer
    End Class

End Module
