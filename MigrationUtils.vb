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

    ''' <summary>
    ''' 이관 확인 메시지 및 보안 절차 공통화
    ''' </summary>
    Public Function AskMigrationConfirmation(targetCount As Integer, companyName As String, migType As String) As Boolean
        If targetCount > 0 Then
            ' [CASE 1] 데이터가 이미 존재하는 경우: 강력한 경고
            Dim msg As String = String.Format("대상 업체({0})의 {2} 데이터가 이미 {1}건 존재합니다." & vbCrLf & "[경고] 이관 시 데이터가 덮어씌워지거나 중복될 수 있습니다." & vbCrLf & "계속 진행하시겠습니까?", companyName, targetCount, migType)
            If FrmMessage.ShowMsg(msg, "중요 경고", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                
                Dim inputPass As String = Microsoft.VisualBasic.Interaction.InputBox("관리자 비밀번호를 입력해주세요.", "보안 확인", "")
                If inputPass = "dycis" Then
                    Return FrmMessage.ShowMsg("정말로 이관을 시작하시겠습니까? 이 작업은 되돌릴 수 없습니다.", "최종 확인", MessageBoxButtons.YesNo) = DialogResult.Yes
                Else
                    FrmMessage.ShowMsg("비밀번호가 일치하지 않습니다. 작업을 중단합니다.", "오류")
                    Return False
                End If
            End If
        Else
            ' [CASE 2] 데이터가 없는 경우: 일반 확인
            Return FrmMessage.ShowMsg(String.Format("'{0}' 업체로 {1} 이관을 시작하시겠습니까?" & vbCrLf & "(기초코드 -> 마스터 -> 내역 순서로 진행)", companyName, migType), "확인", MessageBoxButtons.YesNo) = DialogResult.Yes
        End If
        Return False
    End Function

    ''' <summary>
    ''' 동 리스트 로드 (t_aptgb1)
    ''' </summary>
    Public Sub LoadDongList(dbHelper As DBHelper, cbo As ComboBox, logAction As Action(Of String))
        If dbHelper Is Nothing Then Return
        Try
            Dim query As String = "SELECT DISTINCT DongAddr FROM t_aptgb1 ORDER BY DongAddr"
            Dim dt As DataTable = dbHelper.ExecuteQuery(query)

            cbo.Items.Clear()
            cbo.Items.Add("전체")
            For Each row As DataRow In dt.Rows
                cbo.Items.Add(row("DongAddr").ToString())
            Next
            cbo.SelectedIndex = 0
        Catch ex As Exception
            logAction("동 리스트 로드 실패: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' 호 리스트 로드 (t_aptgb2)
    ''' </summary>
    Public Sub LoadHoList(dbHelper As DBHelper, dongName As String, cbo As ComboBox, logAction As Action(Of String))
        If dbHelper Is Nothing OrElse cbo Is Nothing Then Return

        If dongName = "전체" Then
            cbo.Items.Clear()
            cbo.Items.Add("전체")
            cbo.SelectedIndex = 0
            Return
        End If

        Try
            Dim query As String = String.Format("SELECT DISTINCT HoAddr FROM t_aptgb2 WHERE DongAddr = '{0}' ORDER BY HoAddr", dongName)
            Dim dt As DataTable = dbHelper.ExecuteQuery(query)

            cbo.Items.Clear()
            cbo.Items.Add("전체")
            For Each row As DataRow In dt.Rows
                cbo.Items.Add(row("HoAddr").ToString())
            Next
            cbo.SelectedIndex = 0
        Catch ex As Exception
            logAction("호 리스트 로드 실패: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' 삭제 확인 메시지 및 보안 절차 공통화
    ''' </summary>
    Public Function AskDeleteConfirmation(companyName As String, deleteTarget As String) As Boolean
        Dim msg As String = String.Format("선택된 업체[{0}]의 모든 {1} 정보가 삭제됩니다." & vbCrLf & "삭제된 데이터는 복구할 수 없습니다." & vbCrLf & "계속 진행하시겠습니까?", companyName, deleteTarget)
        
        If FrmMessage.ShowMsg(msg, deleteTarget & " 데이터 초기화", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            Dim inputPass As String = Microsoft.VisualBasic.Interaction.InputBox("관리자 비밀번호를 입력해주세요.", "보안 확인", "")
            If inputPass = "dycis" Then
                Return True
            Else
                FrmMessage.ShowMsg("비밀번호가 일치하지 않습니다. 작업을 중단합니다.", "오류")
                Return False
            End If
        End If
        Return False
    End Function

End Module
