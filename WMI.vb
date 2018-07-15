Imports System.Management
Module WMI
    Public Class clsWMI
        Private objOS As ManagementObjectSearcher
        Private objCS As ManagementObjectSearcher
        Private objMgmt As ManagementObject
        Private m_strComputerName As String
        Private m_strManufacturer As String
        Private m_StrModel As String
        Private m_strOSName As String
        Private m_strOSVersion As String
        Private m_strSystemType As String
        Private m_strTPM As String
        Private m_strWindowsDir As String


        Public Sub New()

            objOS = New ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem")
            objCS = New ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem")
            For Each Me.objMgmt In objOS.Get


                m_strOSName = objMgmt("name").ToString()
                m_strOSVersion = objMgmt("version").ToString()
                m_strComputerName = objMgmt("csname").ToString()
                m_strWindowsDir = objMgmt("windowsdirectory").ToString()
            Next

            For Each Me.objMgmt In objCS.Get
                m_strManufacturer = objMgmt("manufacturer").ToString()
                m_StrModel = objMgmt("model").ToString()
                m_strSystemType = objMgmt("systemtype").ToString
                m_strTPM = objMgmt("totalphysicalmemory").ToString()
            Next
        End Sub

        Public ReadOnly Property ComputerName() As String
            Get
                ComputerName = m_strComputerName
            End Get

        End Property
        Public ReadOnly Property Manufacturer() As String
            Get
                Manufacturer = m_strManufacturer
            End Get

        End Property
        Public ReadOnly Property Model() As String
            Get
                Model = m_StrModel
            End Get

        End Property
        Public ReadOnly Property OsName() As String
            Get
                OsName = m_strOSName
            End Get

        End Property

        Public ReadOnly Property OSVersion() As String
            Get
                OSVersion = m_strOSVersion
            End Get

        End Property
        Public ReadOnly Property SystemType() As String
            Get
                SystemType = m_strSystemType
            End Get

        End Property
        Public ReadOnly Property TotalPhysicalMemory() As String
            Get
                TotalPhysicalMemory = m_strTPM
            End Get

        End Property

        Public ReadOnly Property WindowsDirectory() As String
            Get
                WindowsDirectory = m_strWindowsDir
            End Get

        End Property

    End Class
End Module
