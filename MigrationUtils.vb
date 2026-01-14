Imports System.IO

Public Module MigrationUtils

    ''' <summary>
    ''' 업체 선택 유효성 검사
    ''' </summary>
    Public Function ValidateCompanySelection(cbo As ComboBox) As Boolean
        If cbo.SelectedValue Is Nothing Then
            FrmMessage.ShowMsg("이관할 업체를 선택해주세요.", "알림")
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' DB 설정 유효성 검사
    ''' </summary>
    Public Function ValidateDbSettings(targetDb As String, sourceDb As String) As Boolean
        If String.IsNullOrWhiteSpace(targetDb) Then
            FrmMessage.ShowMsg("Target DB가 설정되지 않았습니다. 환경설정을 확인해주세요.", "경고")
            Return False
        End If

        If String.IsNullOrWhiteSpace(sourceDb) Then
            FrmMessage.ShowMsg("Source DB가 설정되지 않았습니다. 환경설정을 확인해주세요.", "경고")
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' SP 목록 일괄 배포
    ''' </summary>
    Public Function DeploySpList(dbHelper As DBHelper, spNames As String(),
                                 Logger As Action(Of String),
                                 Optional ByRef progressBar As CircularProgressBar = Nothing) As Boolean
        Try
            Dim sqlDir As String = Path.Combine(Application.StartupPath, "SQL")
            ' Dev 환경 등에서 SQL 폴더가 상위에 있을 경우
            If Not Directory.Exists(sqlDir) Then
                Dim upDir As String = Path.Combine(Application.StartupPath, "..\..\SQL")
                If Directory.Exists(upDir) Then sqlDir = upDir
            End If

            Logger(String.Format(">>> [INFO] SP 배포 시작 ({0}건)...", spNames.Length))

            For Each spName In spNames
                Dim spPath As String = Path.Combine(sqlDir, spName)
                Dim result As String = dbHelper.DeployStoredProcedure(spPath)
                Logger(result.Trim())

                If result.Contains("Error") Then
                    Logger("!!! 오류: SP 배포 중 오류가 발생했습니다.")
                    Return False
                End If

                If progressBar IsNot Nothing Then
                    progressBar.Value += 1
                    Application.DoEvents()
                End If
            Next

            Return True

        Catch ex As Exception
            Logger("SP 배포 준비 중 오류: " & ex.Message)
            Return False
        End Try
    End Function

End Module
