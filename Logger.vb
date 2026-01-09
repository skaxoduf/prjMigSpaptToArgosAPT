Imports System.IO
Imports System.Text

Public Class Logger
    Private Shared _logDir As String = Application.StartupPath & "\Logs"

    Public Shared Sub WriteLog(msg As String)
        Try
            If Not Directory.Exists(_logDir) Then
                Directory.CreateDirectory(_logDir)
            End If

            Dim fileName As String = String.Format("Log_{0}.txt", DateTime.Now.ToString("yyyyMMdd"))
            Dim filePath As String = Path.Combine(_logDir, fileName)
            Dim logContent As String = String.Format("[{0}] {1}{2}", DateTime.Now.ToString("HH:mm:ss"), msg, Environment.NewLine)

            File.AppendAllText(filePath, logContent, Encoding.Default)
        Catch ex As Exception
            ' 로깅 실패는 무시하거나 디버그 출력
            Debug.WriteLine("Log Error: " & ex.Message)
        End Try
    End Sub
End Class
