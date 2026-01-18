Imports System.Runtime.InteropServices
Imports System.Text

''' <summary>
''' INI 파일 설정 관리 헬퍼 클래스 (2026-01-18 09:00:00 코드 시작)
''' </summary>
Public Class SettingsHelper

    ' INI 파일 읽기를 위한 Win32 API
    <DllImport("kernel32")>
    Private Shared Function GetPrivateProfileString(ByVal section As String, ByVal key As String, ByVal def As String, ByVal retVal As StringBuilder, ByVal size As Integer, ByVal filePath As String) As Integer
    End Function

    Private _iniPath As String

    Public Sub New(iniPath As String)
        _iniPath = iniPath
    End Sub

    ''' <summary>
    ''' INI 파일에서 설정값을 읽어옵니다.
    ''' </summary>
    ''' <param name="section">섹션 이름</param>
    ''' <param name="key">키 이름</param>
    ''' <param name="def">기본값</param>
    ''' <returns>설정값</returns>
    Public Function ReadIni(ByVal section As String, ByVal key As String, ByVal def As String) As String
        Dim sb As New StringBuilder(1024)
        GetPrivateProfileString(section, key, def, sb, 1024, _iniPath)
        Return sb.ToString()
    End Function

End Class
' (2026-01-18 09:00:00 코드 끝)
