Imports System.Runtime.InteropServices
Imports System.Text
Imports System.IO

Public Class FormSetting
    ' Win32 API for INI read/write
    <DllImport("kernel32")>
    Private Shared Function GetPrivateProfileString(ByVal section As String, ByVal key As String, ByVal def As String, ByVal retVal As StringBuilder, ByVal size As Integer, ByVal filePath As String) As Integer
    End Function

    <DllImport("kernel32")>
    Private Shared Function WritePrivateProfileString(ByVal section As String, ByVal key As String, ByVal val As String, ByVal filePath As String) As Long
    End Function

    Private _iniPath As String = Application.StartupPath & "\setting.ini"

    Private Sub FormSetting_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadSettings()
    End Sub

    Private Sub LoadSettings()
        If Not File.Exists(_iniPath) Then Return

        ' [CONFIG]
        txtCompanyIdx.Text = ReadIni("CONFIG", "CompanyIdx", "")
        txtCompanyCode.Text = ReadIni("CONFIG", "CompanyCode", "")

        ' [DB]
        txtServerIP.Text = ReadIni("DB", "ServerIP", "")
        txtUserID.Text = ReadIni("DB", "UserID", "")
        txtPassword.Text = ReadIni("DB", "Password", "")
        txtTargetDB.Text = ReadIni("DB", "TargetDB", "")
        txtSourceDB.Text = ReadIni("DB", "SourceDB", "")
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        ' Validation (Simple)
        If txtServerIP.Text.Trim() = "" OrElse txtUserID.Text.Trim() = "" Then
            FrmMessage.ShowMsg("DB 접속 정보를 입력해주세요.", "알림")
            Return
        End If

        Try
            ' [CONFIG]
            WriteIni("CONFIG", "CompanyIdx", txtCompanyIdx.Text.Trim())
            WriteIni("CONFIG", "CompanyCode", txtCompanyCode.Text.Trim())

            ' [DB]
            WriteIni("DB", "ServerIP", txtServerIP.Text.Trim())
            WriteIni("DB", "UserID", txtUserID.Text.Trim())
            WriteIni("DB", "Password", txtPassword.Text.Trim())
            WriteIni("DB", "TargetDB", txtTargetDB.Text.Trim())
            WriteIni("DB", "SourceDB", txtSourceDB.Text.Trim())

            FrmMessage.ShowMsg("저장되었습니다.", "알림")
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Catch ex As Exception
            FrmMessage.ShowMsg("저장 중 오류 발생: " & ex.Message, "오류")
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Function ReadIni(section As String, key As String, def As String) As String
        Dim sb As New StringBuilder(255)
        GetPrivateProfileString(section, key, def, sb, 255, _iniPath)
        Return sb.ToString()
    End Function

    Private Sub WriteIni(section As String, key As String, val As String)
        WritePrivateProfileString(section, key, val, _iniPath)
    End Sub
End Class
