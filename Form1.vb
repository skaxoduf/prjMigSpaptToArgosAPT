Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text

Public Class Form1
    ' INI 파일 읽기를 위한 Win32 API
    <DllImport("kernel32")>
    Private Shared Function GetPrivateProfileString(ByVal section As String, ByVal key As String, ByVal def As String, ByVal retVal As StringBuilder, ByVal size As Integer, ByVal filePath As String) As Integer
    End Function

    Private _dbHelper As DBHelper
    Private _iniPath As String = Application.StartupPath & "\setting.ini"

    ' 설정값 변수 (회사 정보는 ComboBox에서 선택)
    Private _targetDbName As String = ""
    Private _sourceDbName As String = ""

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 콤보박스 초기화 (LoadCompanyList에서 설정하므로 여기서는 제거)

        Log("프로그램 시작... 설정 파일 로드 중")
        LoadSettings()
    End Sub

    Private Sub LoadSettings()
        If Not File.Exists(_iniPath) Then
            Log("Error: setting.ini 파일을 찾을 수 없습니다.")
            Return
        End If

        Try
            ' [CONFIG] (회사 정보는 DB 조회로 변경되어 주석/제거)
            ' _companyIdx = Convert.ToInt32(ReadIni("CONFIG", "CompanyIdx", "0"))
            ' _companyCode = ReadIni("CONFIG", "CompanyCode", "")

            ' [DB]
            Dim serverIp As String = ReadIni("DB", "ServerIP", "")
            Dim userId As String = ReadIni("DB", "UserID", "")
            Dim password As String = ReadIni("DB", "Password", "")
            _targetDbName = ReadIni("DB", "TargetDB", "")
            _sourceDbName = ReadIni("DB", "SourceDB", "")

            ' DBHelper 초기화 (Target DB에 접속)
            _dbHelper = New DBHelper(serverIp, _targetDbName, userId, password)

            Log("설정 로드 완료.")
            Log(String.Format("Target DB: {0}, Source DB: {1}", _targetDbName, _sourceDbName))

            ' 접속 테스트 및 회사 목록 로드 (2026-01-09 13:10:00 코드 시작)
            If _dbHelper.TestConnection() Then
                Log("DB 접속 성공!")
                LoadCompanyList()
            Else
                Log("Error: DB 접속 실패. 설정을 확인하세요.")
                btnMigrateMember.Enabled = False
            End If
            ' (2026-01-09 13:10:00 코드 끝)

        Catch ex As Exception
            Log("설정 로드 중 오류: " & ex.Message)
        End Try
    End Sub

    ' 회사 목록 로드 함수 (2026-01-09 13:35:00 코드 시작)
    Private Sub LoadCompanyList()
        Try
            Dim dt As DataTable = _dbHelper.GetCompanyList()

            ' 표시용 컬럼 추가
            dt.Columns.Add("DisplayCol", GetType(String), "'[IDX: ' + F_IDX + '] [CODE: ' + F_COMPANY_CODE + '] ' + F_COMPANY_NAME + ' (회원수: ' + F_MEM_COUNT + '명)'")

            cboCompany.DataSource = dt
            cboCompany.DisplayMember = "DisplayCol"
            cboCompany.ValueMember = "F_IDX"

            If dt.Rows.Count > 0 Then
                cboCompany.SelectedIndex = 0
                btnMigrateMember.Enabled = True
                Log("회사 목록 로드 완료: " & dt.Rows.Count & "개")
            Else
                Log("Error: 등록된 회사(T_COMPANY)가 없습니다.")
                btnMigrateMember.Enabled = False
            End If
        Catch ex As Exception
            Log("회사 목록 조회 실패: " & ex.Message)
        End Try
    End Sub
    ' (2026-01-09 13:35:00 코드 끝)

    Private Sub btnMigrateMember_Click(sender As Object, e As EventArgs) Handles btnMigrateMember.Click
        ' 1. 기본 유효성 검사 (회사 선택)
        If cboCompany.SelectedValue Is Nothing Then
            FrmMessage.ShowMsg("이관할 회사를 선택해주세요.", "알림")
            Return
        End If

        ' 2. DB 설정 유효성 검사
        If String.IsNullOrWhiteSpace(_targetDbName) Then
            FrmMessage.ShowMsg("Target DB가 설정되지 않았습니다. 환경설정을 확인해주세요.", "경고")
            Return
        End If

        If String.IsNullOrWhiteSpace(_sourceDbName) Then
            FrmMessage.ShowMsg("Source DB가 설정되지 않았습니다. 환경설정을 확인해주세요.", "경고")
            Return
        End If

        If FrmMessage.ShowMsg("선택된 회사로 회원 이관을 시작하시겠습니까?", "확인", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            Log("=== 회원 이관 시작 ===")

            ' 선택된 회사 정보 가져오기
            Dim drv As DataRowView = CType(cboCompany.SelectedItem, DataRowView)
            Dim selCompanyIdx As Integer = Convert.ToInt32(drv("F_IDX"))
            Dim selCompanyCode As String = drv("F_COMPANY_CODE").ToString()

            Log(String.Format("검증 완료. 시작: Target={0}, Source={1}", _targetDbName, _sourceDbName))
            Log(String.Format("대상: {0} (IDX:{1})", drv("F_COMPANY_NAME"), selCompanyIdx))

            Try
                ' 1. 동/호 정보 이관 (2026-01-09 추가)
                Log(">>> [1/2] 동/호 정보(Master) 이관 시작...")
                Dim resultDong As String = _dbHelper.ExecuteMigrationSP("USP_MIG_DONG_HO", _sourceDbName, selCompanyIdx, selCompanyCode)
                Log("동/호 이관 결과: " & resultDong)

                ' 2. 회원 정보 이관
                Log(">>> [2/2] 회원 정보(Member) 이관 시작...")
                Dim resultMem As String = _dbHelper.ExecuteMigrationSP("USP_MIG_MEMBER_TO_MEM", _sourceDbName, selCompanyIdx, selCompanyCode)
                Log("회원 이관 결과: " & resultMem)

            Catch ex As Exception
                Log("실행 중 오류 발생: " & ex.Message)
            End Try

            Log("=== 작업 종료 ===")
        End If
    End Sub

    ''' <summary>
    ''' 로그 출력 함수
    ''' </summary>
    ''' <summary>
    ''' 로그 출력 함수 (화면 출력 + 파일 저장)
    ''' </summary>
    Private Sub Log(msg As String)
        ' 화면 출력
        rtbLog.AppendText(String.Format("[{0}] {1}{2}", DateTime.Now.ToString("HH:mm:ss"), msg, Environment.NewLine))
        rtbLog.ScrollToCaret()

        ' 파일 저장
        Logger.WriteLog(msg)
    End Sub

    Private Function ReadIni(section As String, key As String, def As String) As String
        Dim sb As New StringBuilder(255)
        GetPrivateProfileString(section, key, def, sb, 255, _iniPath)
        Return sb.ToString()
    End Function

    Private Sub btnSetting_Click(sender As Object, e As EventArgs) Handles btnSetting.Click
        ' 환경설정 버튼 클릭 이벤트 (2026-01-09 12:55:00 코드 시작)
        Dim frm As New FormSetting()
        If frm.ShowDialog() = DialogResult.OK Then
            Log("설정이 변경되었습니다. 설정을 다시 로드합니다.")
            LoadSettings()
        End If
        ' (2026-01-09 12:55:00 코드 끝)
    End Sub
End Class
