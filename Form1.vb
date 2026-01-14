Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Drawing.Drawing2D ' 추가: SmoothingMode 사용을 위해 필요
Imports System.Data.OleDb ' 엑셀 읽기를 위해 추가

Public Class Form1
    ' INI 파일 읽기를 위한 Win32 API
    <DllImport("kernel32")>
    Private Shared Function GetPrivateProfileString(ByVal section As String, ByVal key As String, ByVal def As String, ByVal retVal As StringBuilder, ByVal size As Integer, ByVal filePath As String) As Integer
    End Function

    Private _dbHelper As DBHelper
    Private _iniPath As String = Application.StartupPath & "\setting.ini"

    ' 설정값 변수 (업체 정보는 ComboBox에서 선택)
    Private _targetDbName As String = ""
    Private _sourceDbName As String = ""

    ' 2026-01-12 10:45:00 Source DB 조회를 위한 헬퍼 추가
    Private _sourceHelper As DBHelper

    ' (DataViewer 버튼 및 관련 변수 제거)
    ' Private WithEvents btnDataViewer As New Button() -> Layout 변경으로 제거됨

    ' 2026-01-13 초기화 완료 플래그
    Private _isLoaded As Boolean = False

    ' 2026-01-14 원형 진행바 (코드 생성)
    Private _loadingBar As CircularProgressBar

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 콤보박스 초기화 (LoadCompanyList에서 설정하므로 여기서는 제거)

        ' 2026-01-12 11:50:00 테마 강제 적용 (Modern Bright)
        ThemeManager.SetTheme(ThemeManager.AppTheme.ModernBright)
        ThemeManager.ApplyTheme(Me)

        Log("프로그램 시작... 설정 파일 로드 중")
        LoadSettings()

        ' 2026-01-12 11:00:00 동/호 리스트 초기화는 DB 연결 후 진행
        cboLimit.SelectedIndex = 1 ' Default 100
        cboLimitTarget.SelectedIndex = 1 ' Default 100 for Target

        ' 2026-01-13 Fix Z-Order to ensure Target Grid is visible
        ' Docking Layout Order: Last in Z-Order (SendToBack) -> First in Z-Order (BringToFront)
        ' We want pnlCenterAction (Top) to be processed FIRST (so it takes top space).
        ' We want grpTarget (Fill) to be processed LAST (so it takes remaining space).
        ' Therefore: pnlCenterAction -> SendToBack (High Index), grpTarget -> BringToFront (Index 0).
        pnlCenterAction.SendToBack()
        grpTarget.BringToFront()

        ' Ensure visibility
        grpTarget.Visible = True
        pnlCenterAction.Visible = True

        ' 2026-01-13 Customize Modern Buttons
        CustomizeButtons()

        ' 2026-01-14 Init CircularProgressBar
        _loadingBar = New CircularProgressBar()
        _loadingBar.Size = New Size(250, 250)
        _loadingBar.LineThickness = 20
        _loadingBar.ProgressColor = Color.FromArgb(0, 122, 255) ' Blue
        _loadingBar.TrackColor = Color.FromArgb(240, 240, 240) ' Light Gray
        _loadingBar.ForeColor = Color.White ' Text Color
        _loadingBar.Location = New Point((Me.Width - _loadingBar.Width) \ 2, (Me.Height - _loadingBar.Height) \ 2)
        _loadingBar.Anchor = AnchorStyles.None ' Resize 시 중앙 유지
        _loadingBar.Visible = False
        Me.Controls.Add(_loadingBar)
        _loadingBar.BringToFront()

        ' 초기화 완료
        _isLoaded = True
    End Sub

    ' 2026-01-13 버튼 스타일 초기화
    Private Sub CustomizeButtons()
        ' 이관하기 버튼 (Blue Accent) - Main Action Button
        btnMigrateMember.CustomBaseColor = Color.FromArgb(122, 162, 247)
        btnMigrateMember.CustomHoverColor = Color.FromArgb(150, 180, 250)
        btnMigrateMember.BorderRadius = 20
        btnMigrateMember.Font = New Font("Segoe UI", 12.0F, FontStyle.Bold)
        btnMigrateMember.ForeColor = Color.Black ' 밝은 배경엔 검은 글씨
        btnMigrateMember.Text = "회원 이관하기"


        ' 엑셀 불러오기 버튼 (Orange)
        btnLoadExcel.CustomBaseColor = Color.FromArgb(230, 126, 34)
        btnLoadExcel.CustomHoverColor = Color.FromArgb(243, 156, 18)
        btnLoadExcel.BorderRadius = 20
        btnLoadExcel.Font = New Font("Segoe UI", 12.0F, FontStyle.Bold)
        btnLoadExcel.ForeColor = Color.Black

        ' 초기화 버튼 (Red Danger)
        btnInitTargetMember.CustomBaseColor = Color.FromArgb(200, 60, 60)
        btnInitTargetMember.CustomHoverColor = Color.FromArgb(230, 80, 80)
        btnInitTargetMember.BorderRadius = 15
        btnInitTargetMember.ForeColor = Color.White

        ' 조회 및 설정 버튼 (Default - Dark Blue)
        btnSourceSearch.BorderRadius = 10
        btnSourceSearch.ForeColor = Color.White
        btnTargetSearch.BorderRadius = 10
        btnTargetSearch.ForeColor = Color.White
        btnSetting.BorderRadius = 10
        btnSetting.ForeColor = Color.White
    End Sub

    ' (DataViewer 관련 버튼 로직 제거)

    Private Sub LoadSettings()
        If Not File.Exists(_iniPath) Then
            Log("Error: setting.ini 파일을 찾을 수 없습니다.")
            Return
        End If

        Try
            ' [CONFIG] (업체 정보는 DB 조회로 변경되어 주석/제거)
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
            ' Source DB Helper 초기화
            _sourceHelper = New DBHelper(serverIp, _sourceDbName, userId, password)

            Log("설정 로드 완료.")
            Log(String.Format("Target DB: {0}, Source DB: {1}", _targetDbName, _sourceDbName))

            ' 접속 테스트 및 업체 목록 로드
            If _dbHelper.TestConnection() Then
                Log("DB 접속 성공!")
                LoadCompanyList()

                ' 2026-01-12 11:00:00 Source DB 기반 조회 조건 초기화 (동 리스트)
                LoadDongList()
            Else
                Log("Error: DB 접속 실패. 설정을 확인하세요.")
                btnMigrateMember.Enabled = False
            End If
            ' (2026-01-09 13:10:00 코드 끝)

        Catch ex As Exception
            Log("설정 로드 중 오류: " & ex.Message)
        End Try
    End Sub



    ' 업체 목록 로드 함수 (2026-01-09 13:35:00 코드 시작)
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
                Log("업체 목록 로드 완료: " & dt.Rows.Count & "개")
            Else
                Log("Error: 등록된 업체(T_COMPANY)가 없습니다.")
                btnMigrateMember.Enabled = False
            End If
        Catch ex As Exception
            Log("업체 목록 조회 실패: " & ex.Message)
        End Try
    End Sub
    ' (2026-01-09 13:35:00 코드 끝)

    ' 2026-01-12 18:00:00 업체 변경 시 뉴디비 그리드 초기화
    Private Sub cboCompany_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCompany.SelectedIndexChanged
        If Not _isLoaded Then Return ' 로딩 중 이벤트 무시

        If dgvTarget.DataSource IsNot Nothing Then
            dgvTarget.DataSource = Nothing
            grpTarget.Text = "New DB (Target)"
            pnlTargetPagination.Controls.Clear()
            _currentTargetPage = 1
        End If
    End Sub

    ' 2026-01-14 이관 버튼 통합 (DB -> DB / Excel -> DB)
    Private Sub btnMigrateMember_Click(sender As Object, e As EventArgs) Handles btnMigrateMember.Click
        If _isExcelMode Then
            MigrateFromExcel()
        Else
            MigrateFromDB()
        End If
    End Sub

    ' 2026-01-14 Old DB 이관 로직 (기존 로직 분리)
    Private Sub MigrateFromDB()
        ' 1. 기본 유효성 검사 (업체 선택)
        ' 공통 유효성 검사 (MigrationUtils 사용)
        If Not MigrationUtils.ValidateCompanySelection(cboCompany) Then Return
        If Not MigrationUtils.ValidateDbSettings(_targetDbName, _sourceDbName) Then Return

        ' 선택된 업체 정보 가져오기
        Dim drv = CType(cboCompany.SelectedItem, DataRowView)
        Dim selCompanyIdx = Convert.ToInt32(drv("F_IDX"))
        Dim selCompanyCode = drv("F_COMPANY_CODE").ToString
        Dim selCompanyName = drv("F_COMPANY_NAME").ToString

        ' 2026-01-13 14:00:00 Target DB 데이터 존재 여부 확인 (안전장치 상세 구현)
        Dim targetCount As Integer = 0
        Try
            Dim countQuery As String = String.Format("SELECT COUNT(*) FROM T_MEM WHERE F_COMPANY_CODE = '{0}'", selCompanyCode)
            Dim dtCount = _dbHelper.ExecuteQuery(countQuery)
            If dtCount.Rows.Count > 0 Then
                targetCount = Convert.ToInt32(dtCount.Rows(0)(0))
            End If
        Catch ex As Exception
            Log("데이터 확인 중 오류 발생: " & ex.Message)
            Return
        End Try

        ' 이관 진행 여부 변수
        Dim proceed As Boolean = False

        If targetCount > 0 Then
            ' [CASE 1] 데이터가 이미 존재하는 경우: 강력한 경고 및 안전장치 가동
            If FrmMessage.ShowMsg(String.Format("대상 업체({0})의 데이터가 이미 {1}건 존재합니다." & vbCrLf & "[경고] 이관 시 데이터가 덮어씌워지거나 초기화 될 수 있습니다." & vbCrLf & "계속 진행하시겠습니까?", selCompanyName, targetCount), "중요 경고", MessageBoxButtons.YesNo) = DialogResult.Yes Then

                ' 비밀번호 입력 요청
                Dim inputPass As String = Microsoft.VisualBasic.Interaction.InputBox("관리자 비밀번호를 입력해주세요.", "보안 확인", "")

                If inputPass = "dycis" Then
                    ' 최종 확인
                    If FrmMessage.ShowMsg("정말로 이관을 시작하시겠습니까? 이 작업은 되돌릴 수 없습니다.", "최종 확인", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                        proceed = True
                    End If
                Else
                    FrmMessage.ShowMsg("비밀번호가 일치하지 않습니다. 작업을 중단합니다.", "오류")
                End If
            End If
        Else
            ' [CASE 2] 데이터가 없는 경우: 일반 확인
            If FrmMessage.ShowMsg(String.Format("'{0}' 업체로 회원 이관을 시작하시겠습니까?", selCompanyName), "확인", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                proceed = True
            End If
        End If


        If proceed Then
            Log("=== 회원 이관 시작 (Old DB -> New DB) ===")

            Log(String.Format("검증 완료. 시작: Target={0}, Source={1}", _targetDbName, _sourceDbName))
            Log(String.Format("대상: {0} (IDX:{1})", selCompanyName, selCompanyIdx))

            ' 진행바 설정 (총 4단계: SP1, SP2, Dong, Member)
            _loadingBar.Maximum = 4
            _loadingBar.Value = 0
            _loadingBar.Visible = True
            _loadingBar.BringToFront()
            Application.DoEvents()

            ' 2026-01-13 저장 프로시저(SP) 자동 배포 기능 (공통 모듈 사용)

            ' SP 목록 정의
            Dim spList As String() = {"USP_MIG_DONG_HO.sql", "USP_MIG_MEMBER_TO_MEM.sql"}

            ' SP 배포 실행
            Dim deploySuccess As Boolean = MigrationUtils.DeploySpList(_dbHelper, spList, AddressOf Log, _loadingBar)

            If Not deploySuccess Then
                _loadingBar.Visible = False
                Return
            End If

            Try
                ' 1. 동/호 정보 이관 (2026-01-09 추가)
                Log(">>> [1/2] 동/호 정보(Master) 이관 시작...")
                Dim resultDong = _dbHelper.ExecuteMigrationSP("USP_MIG_DONG_HO", _sourceDbName, selCompanyIdx, selCompanyCode)
                Log("동/호 이관 결과: " & resultDong)
                _loadingBar.Value += 1 ' Step 3 Complete
                Application.DoEvents()

                ' 2. 회원 정보 이관
                Log(">>> [2/2] 회원 정보(Member) 이관 시작...")
                Dim resultMem = _dbHelper.ExecuteMigrationSP("USP_MIG_MEMBER_TO_MEM", _sourceDbName, selCompanyIdx, selCompanyCode)
                Log("회원 이관 결과: " & resultMem)
                _loadingBar.Value += 1 ' Step 4 Complete
                Application.DoEvents()

            Catch ex As Exception
                Log("실행 중 오류 발생: " & ex.Message)
            Finally
                _loadingBar.Visible = False
            End Try

            Log("=== 작업 종료 ===")

            ' 작업 완료 후 그리드 갱신
            _currentSourcePage = 1
            _currentTargetPage = 1
            ' 자동 갱신 하지 않음 (사용자가 직접 조회하도록)
            Log("이관이 완료되었습니다. 결과를 확인하려면 조회 버튼을 누르세요.")
        End If
    End Sub

    ' 2026-01-14 15:30:00 Excel Mode Flag
    Private _isExcelMode As Boolean = False

    ' 2026-01-14 15:30:00 엑셀 불러오기 버튼 클릭 핸들러
    Private Sub btnLoadExcel_Click(sender As Object, e As EventArgs) Handles btnLoadExcel.Click
        Dim ofd As New OpenFileDialog()
        ofd.Filter = "Excel Files|*.xlsx;*.xls"
        ofd.Title = "회원 엑셀 파일 선택"
        ofd.InitialDirectory = Application.StartupPath

        If ofd.ShowDialog() = DialogResult.OK Then
            Dim filePath As String = ofd.FileName
            LoadExcelToGrid(filePath)
        End If
    End Sub

    ' 2026-01-14 15:30:00 엑셀 파일 읽기 및 그리드 표시
    Private Sub LoadExcelToGrid(filePath As String)
        Log("=== 엑셀 파일 로딩 시작 ===")
        Dim connStr As String = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES"";", filePath)

        Try
            Using conn As New OleDbConnection(connStr)
                conn.Open()
                Dim dtSchema As DataTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
                Dim sheetName As String = ""
                If dtSchema.Rows.Count > 0 Then
                    sheetName = dtSchema.Rows(0)("TABLE_NAME").ToString()
                Else
                    Log("오류: 엑셀 시트를 찾을 수 없습니다.")
                    Return
                End If

                Dim query As String = String.Format("SELECT * FROM [{0}]", sheetName)
                Using cmd As New OleDbCommand(query, conn)
                    Using da As New OleDbDataAdapter(cmd)
                        Dim dtExcel As New DataTable()
                        da.Fill(dtExcel)

                        ' 2026-01-14 15:37:00 빈 행 제거 로직 (동/호 없는 데이터 숨김)
                        If dtExcel.Columns.Contains("동") AndAlso dtExcel.Columns.Contains("호") Then
                            For i As Integer = dtExcel.Rows.Count - 1 To 0 Step -1
                                Dim row As DataRow = dtExcel.Rows(i)
                                Dim dong As String = If(row("동") Is DBNull.Value, "", row("동").ToString().Trim())
                                Dim ho As String = If(row("호") Is DBNull.Value, "", row("호").ToString().Trim())

                                ' 동 또는 호가 비어있으면 해당 행 제거
                                If String.IsNullOrEmpty(dong) OrElse String.IsNullOrEmpty(ho) Then
                                    dtExcel.Rows.RemoveAt(i)
                                End If
                            Next
                        End If

                        ' 그리드 바인딩
                        dgvSource.DataSource = dtExcel
                        _isExcelMode = True

                        ' UI 업데이트
                        grpSource.Text = String.Format("Excel Preview : {0} Rows (File: {1})", dtExcel.Rows.Count, Path.GetFileName(filePath))
                        pnlSourcePagination.Controls.Clear() ' 엑셀 모드에서는 페이지네이션 숨김

                        Log(String.Format("엑셀 로드 완료: {0}건. 내용을 확인 후 [이관하기] 버튼을 누르세요.", dtExcel.Rows.Count))
                        btnMigrateMember.Text = "엑셀 이관하기" ' 버튼 텍스트 변경
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Log("엑셀 로드 중 오류: " & ex.Message)
            If ex.Message.Contains("registered") Then
                FrmMessage.ShowMsg("Microsoft.ACE.OLEDB Provider가 설치되어 있지 않습니다." & vbCrLf & "Microsoft Access Database Engine을 설치해주세요.", "오류")
            End If
            _isExcelMode = False
        End Try
    End Sub

    ' 2026-01-14 엑셀 이관 로직 (기존 로직 분리)
    Private Sub MigrateFromExcel()
        ' 0. Excel 모드 확인
        If Not _isExcelMode OrElse dgvSource.DataSource Is Nothing Then
            FrmMessage.ShowMsg("먼저 [엑셀 불러오기]를 통해 데이터를 로드해주세요.", "알림")
            Return
        End If

        ' 1. 업체 선택 확인 (공통 모듈 사용)
        If Not MigrationUtils.ValidateCompanySelection(cboCompany) Then Return

        Dim drv = CType(cboCompany.SelectedItem, DataRowView)
        ' Dim selCompanyIdx = Convert.ToInt32(drv("F_IDX")) ' Not used
        Dim selCompanyCode = drv("F_COMPANY_CODE").ToString
        Dim selCompanyName = drv("F_COMPANY_NAME").ToString
        Dim dtExcel As DataTable = CType(dgvSource.DataSource, DataTable)

        ' 2. 진행 확인
        If FrmMessage.ShowMsg(String.Format("현재 미리보기된 데이터({0}건)를" & vbCrLf & "대상 업체 '{1}'(으)로 이관하시겠습니까?", dtExcel.Rows.Count, selCompanyName), "이관 확인", MessageBoxButtons.YesNo) <> DialogResult.Yes Then
            Return
        End If

        ' 3. SP 배포 (공통 모듈 사용)
        Dim spList As String() = {"USP_MIG_EXCEL_MEMBER_TO_MEM.sql"}
        If Not MigrationUtils.DeploySpList(_dbHelper, spList, AddressOf Log) Then Return

        ' 4. 이관 실행 Loop
        Log("=== 엑셀 데이터 이관 시작 ===")
        Dim successCount As Integer = 0
        Dim failCount As Integer = 0

        ' 진행바 설정
        _loadingBar.Maximum = dtExcel.Rows.Count
        _loadingBar.Value = 0
        _loadingBar.Visible = True
        _loadingBar.BringToFront()
        Application.DoEvents()

        Try
            For Each row As DataRow In dtExcel.Rows
                _loadingBar.Value += 1 ' 진행률 업데이트

                ' 필수 값 체크 (동/호)
                Dim dong As String = If(row("동") Is DBNull.Value, "", row("동").ToString())
                Dim ho As String = If(row("호") Is DBNull.Value, "", row("호").ToString())

                If String.IsNullOrWhiteSpace(dong) OrElse String.IsNullOrWhiteSpace(ho) Then
                    Continue For ' 동/호 없으면 건너뜀
                End If

                ' 파라미터 매핑
                Dim cardNo As String = If(dtExcel.Columns.Contains("카드번호") AndAlso row("카드번호") IsNot DBNull.Value, row("카드번호").ToString(), "")
                Dim name As String = If(dtExcel.Columns.Contains("입주민 명") AndAlso row("입주민 명") IsNot DBNull.Value, row("입주민 명").ToString(), "")
                Dim regDate As String = If(dtExcel.Columns.Contains("등록일자") AndAlso row("등록일자") IsNot DBNull.Value, Convert.ToDateTime(row("등록일자")).ToString("yyyy-MM-dd"), "")
                Dim phone As String = If(dtExcel.Columns.Contains("핸드폰") AndAlso row("핸드폰") IsNot DBNull.Value, row("핸드폰").ToString(), "")
                Dim contact As String = If(dtExcel.Columns.Contains("연락처") AndAlso row("연락처") IsNot DBNull.Value, row("연락처").ToString(), "")
                Dim memo As String = If(dtExcel.Columns.Contains("메모") AndAlso row("메모") IsNot DBNull.Value, row("메모").ToString(), "")
                Dim birth As String = If(dtExcel.Columns.Contains("생일") AndAlso row("생일") IsNot DBNull.Value, Convert.ToDateTime(row("생일")).ToString("yyyy-MM-dd"), "")
                Dim gender As String = If(dtExcel.Columns.Contains("성별") AndAlso row("성별") IsNot DBNull.Value, row("성별").ToString(), "")

                Try
                    ' SP 호출 쿼리 생성
                    Dim sql As String = String.Format("EXEC USP_MIG_EXCEL_MEMBER_TO_MEM " &
                        "'{0}', '{1}', '{2}', '{3}', N'{4}', '{5}', '{6}', '{7}', N'{8}', '{9}', N'{10}'",
                        selCompanyCode,
                        dong.Replace("'", "''"),
                        ho.Replace("'", "''"),
                        cardNo.Replace("'", "''"),
                        name.Replace("'", "''"),
                        regDate,
                        phone.Replace("'", "''"),
                        contact.Replace("'", "''"),
                        memo.Replace("'", "''"),
                        birth,
                        gender.Replace("'", "''"))

                    Dim resultDt As DataTable = _dbHelper.ExecuteSqlWithResultCheck(sql)
                    If resultDt IsNot Nothing AndAlso resultDt.Rows.Count > 0 Then
                        Dim res As String = ""
                        Dim msg As String = ""

                        If resultDt.Columns.Contains("Result") Then
                            res = resultDt.Rows(0)("Result").ToString()
                            If resultDt.Columns.Contains("Message") Then
                                msg = resultDt.Rows(0)("Message").ToString()
                            End If
                        ElseIf resultDt.Columns.Contains("Response") Then
                            ' 2026-01-14 Fallback: Response 컬럼 지원
                            res = resultDt.Rows(0)("Response").ToString()
                            ' Response만 있고 Message가 없을 수 있으므로 처리
                            If resultDt.Columns.Contains("Message") Then
                                msg = resultDt.Rows(0)("Message").ToString()
                            Else
                                msg = "Message column not found (Response received)"
                            End If
                        End If

                        If res = "SUCCESS" OrElse res = "OK" OrElse res.Contains("Success") Then
                            successCount += 1
                        ElseIf String.IsNullOrEmpty(res) Then
                            ' 2026-01-14 디버깅: 컬럼이 없을 경우 확인
                            Dim cols As New List(Of String)
                            For Each col As DataColumn In resultDt.Columns
                                cols.Add(col.ColumnName)
                            Next
                            failCount += 1
                            Log(String.Format("오류 ({0}동 {1}호): 결과 컬럼 누락. 반환된 컬럼: [{2}]", dong, ho, String.Join(", ", cols)))
                        Else
                            failCount += 1
                            Log(String.Format("실패 ({0}동 {1}호): {2} (Msg: {3})", dong, ho, res, msg))
                        End If
                    Else
                        failCount += 1
                    End If
                Catch ex As Exception
                    failCount += 1
                    Log(String.Format("오류 ({0}동 {1}호): {2}", dong, ho, ex.Message))
                End Try

                Application.DoEvents()
            Next

            Log(String.Format("완료: 성공 {0}, 실패 {1}", successCount, failCount))

            ' 이관 후 Target DB 조회하여 결과 확인 유도
            btnTargetSearch.PerformClick()

        Catch ex As Exception
            Log("이관 중 치명적 오류: " & ex.Message)
        Finally
            ' 진행바 숨김
            _loadingBar.Visible = False
        End Try

        Log("=== 엑셀 데이터 이관 종료 ===")
    End Sub

    Private Sub btnSetting_Click(sender As Object, e As EventArgs) Handles btnSetting.Click
        ' 환경설정 버튼 클릭 이벤트 (2026-01-09 12:55:00 코드 시작)
        Dim frm As New FormSetting
        If frm.ShowDialog = DialogResult.OK Then
            Log("설정이 변경되었습니다. 설정을 다시 로드합니다.")
            LoadSettings()
            ' LoadAppTheme() ' 테마 변경 기능 제거 (Tokyo Night 고정)
        End If
        ' (2026-01-09 12:55:00 코드 끝)
    End Sub

    ' 2026-01-12 11:00:00 동 리스트 로드 (Source DB 기준)
    Private Sub LoadDongList()
        If _sourceHelper Is Nothing Then Return

        Try
            Dim query As String = "SELECT DISTINCT DongAddr FROM t_aptgb1 ORDER BY DongAddr"
            Dim dt As DataTable = _sourceHelper.ExecuteQuery(query)

            cboSourceDong.Items.Clear()
            cboSourceDong.Items.Add("전체")
            For Each row As DataRow In dt.Rows
                cboSourceDong.Items.Add(row("DongAddr").ToString())
            Next
            cboSourceDong.SelectedIndex = 0

        Catch ex As Exception
            Log("동 리스트 로드 실패: " & ex.Message)
        End Try
    End Sub

    Private Sub cboSourceDong_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSourceDong.SelectedIndexChanged
        If _sourceHelper Is Nothing OrElse cboSourceDong.SelectedItem Is Nothing Then Return

        Dim selectedDong As String = cboSourceDong.SelectedItem.ToString()
        If selectedDong = "전체" Then
            cboSourceHo.Items.Clear()
            cboSourceHo.Items.Add("전체")
            cboSourceHo.SelectedIndex = 0
            Return
        End If

        Try
            ' 호 리스트 로드 (선택된 동 기준)
            Dim query As String = String.Format("SELECT DISTINCT HoAddr FROM t_aptgb2 WHERE DongAddr = '{0}' ORDER BY HoAddr", selectedDong)
            Dim dt As DataTable = _sourceHelper.ExecuteQuery(query)

            cboSourceHo.Items.Clear()
            cboSourceHo.Items.Add("전체")
            For Each row As DataRow In dt.Rows
                cboSourceHo.Items.Add(row("HoAddr").ToString())
            Next
            cboSourceHo.SelectedIndex = 0

        Catch ex As Exception
            Log("호 리스트 로드 실패: " & ex.Message)
        End Try
    End Sub

    ' 2026-01-13 분리된 조회 버튼 핸들러
    Private Sub btnSourceSearch_Click(sender As Object, e As EventArgs) Handles btnSourceSearch.Click
        _isExcelMode = False ' 엑셀 모드 해제
        btnMigrateMember.Text = "회원 이관하기" ' 버튼 텍스트 복구
        _currentSourcePage = 1
        SearchSource()
    End Sub

    Private Sub btnTargetSearch_Click(sender As Object, e As EventArgs) Handles btnTargetSearch.Click
        _currentTargetPage = 1
        SearchTarget()
    End Sub

    ' 2026-01-13 검색창 엔터키 이벤트 추가
    Private Sub txtSourceSearchName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSourceSearchName.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True ' 비프음 방지
            btnSourceSearch.PerformClick() ' 버튼 클릭 효과 (페이지 초기화 포함)
        End If
    End Sub

    Private Sub txtTargetSearchName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtTargetSearchName.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            btnTargetSearch.PerformClick()
        End If
    End Sub

    ' 2026-01-12 13:20:00 그리드 이미지 오류 방지 (DataError 핸들러 추가)
    Private Sub dgvSource_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvSource.DataError
        ' 이미지 렌더링 등에서 발생하는 오류 무시
        e.ThrowException = False
        e.Cancel = False
    End Sub

    Private Sub dgvTarget_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvTarget.DataError
        ' 이미지 렌더링 등에서 발생하는 오류 무시
        e.ThrowException = False
        e.Cancel = False
    End Sub

    ' 2026-01-12 15:50:00 TabControl Dark Theme Draw Implementation
    Private Sub tabMain_DrawItem(sender As Object, e As DrawItemEventArgs) Handles tabMain.DrawItem
        Dim tabs As TabControl = DirectCast(sender, TabControl)
        Dim g As Graphics = e.Graphics
        Dim r As Rectangle = e.Bounds

        ' Tokyo Night Palette
        Dim backColor As Color = Color.FromArgb(26, 27, 38)
        Dim activeColor As Color = Color.FromArgb(65, 72, 104)
        Dim foreColor As Color = Color.White
        Dim inactiveFore As Color = Color.FromArgb(169, 177, 214)

        ' Background Fill
        g.FillRectangle(New SolidBrush(backColor), r)

        ' Selection Highlight
        If e.Index = tabs.SelectedIndex Then
            g.FillRectangle(New SolidBrush(activeColor), r)
            g.DrawString(tabs.TabPages(e.Index).Text, tabs.Font, New SolidBrush(foreColor), r.X + 10, r.Y + 6)
        Else
            g.DrawString(tabs.TabPages(e.Index).Text, tabs.Font, New SolidBrush(inactiveFore), r.X + 10, r.Y + 6)
        End If
    End Sub

    ' 2026-01-12 16:16:00 Image Hover Preview
    Private Sub dgvSource_CellMouseEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgvSource.CellMouseEnter
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Return

        Dim dgv As DataGridView = DirectCast(sender, DataGridView)
        Dim colName As String = dgv.Columns(e.ColumnIndex).Name

        If colName = "MbSajin" OrElse colName = "MbPhoto" Then
            Dim cellVal = dgv.Rows(e.RowIndex).Cells(e.ColumnIndex).Value

            If cellVal IsNot Nothing AndAlso TypeOf cellVal Is Byte() Then
                Try
                    Dim bytes As Byte() = DirectCast(cellVal, Byte())
                    If bytes.Length > 0 Then
                        Using ms As New MemoryStream(bytes)
                            Dim originalImage As Image = Image.FromStream(ms)
                            ' 그리기 위한 비트맵 복사 (Graphics 생성을 위해)
                            Dim bmp As New Bitmap(originalImage)

                            ' 회원 정보 가져오기 (컬럼 이름 확인 필요: MbNo, MbName)
                            Dim mbName As String = "Unknown"
                            Dim mbNo As String = "Unknown"

                            ' 안전하게 컬럼 값 가져오기
                            If dgv.Columns.Contains("MbName") Then mbName = dgv.Rows(e.RowIndex).Cells("MbName").Value.ToString()
                            If dgv.Columns.Contains("MbNo") Then mbNo = dgv.Rows(e.RowIndex).Cells("MbNo").Value.ToString()

                            Dim infoText As String = String.Format("{0} ({1})", mbName, mbNo)

                            Using g As Graphics = Graphics.FromImage(bmp)
                                g.SmoothingMode = SmoothingMode.AntiAlias
                                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit

                                ' 텍스트 배경 (반투명 검정)
                                ' 텍스트 폰트 설정
                                Dim nameFont As New Font("Segoe UI", 15, FontStyle.Bold)
                                Dim noFont As New Font("Segoe UI", 12.5F, FontStyle.Bold)

                                Dim nameSize As SizeF = g.MeasureString(mbName, nameFont)
                                Dim noSize As SizeF = g.MeasureString("No. " & mbNo, noFont)

                                ' 하단 여백 및 줄 간격
                                Dim padding As Integer = 8
                                Dim lineSpacing As Integer = 2
                                Dim totalTextHeight As Integer = CInt(nameSize.Height + lineSpacing + noSize.Height)
                                Dim boxHeight As Integer = totalTextHeight + (padding * 2)

                                ' 텍스트 배경 영역 (더 진한 반투명)
                                Dim rect As New Rectangle(0, bmp.Height - boxHeight, bmp.Width, boxHeight)

                                Using brushBg As New SolidBrush(Color.FromArgb(220, 20, 20, 25)) ' Deep Dark Blue-ish Black
                                    g.FillRectangle(brushBg, rect)
                                End Using

                                ' 텍스트 그리기 좌표
                                Dim namePos As New PointF(padding, rect.Y + padding)
                                Dim noPos As New PointF(padding, namePos.Y + nameSize.Height + lineSpacing)

                                ' 그림자 효과
                                g.DrawString(mbName, nameFont, Brushes.Black, namePos.X + 1, namePos.Y + 1)
                                g.DrawString("No. " & mbNo, noFont, Brushes.Black, noPos.X + 1, noPos.Y + 1)

                                ' 실제 텍스트 (밝은 흰색 & 회색)
                                g.DrawString(mbName, nameFont, Brushes.White, namePos)
                                g.DrawString("No. " & mbNo, noFont, Brushes.White, noPos)
                            End Using

                            picPreview.Image = bmp
                        End Using

                        ' Position Preview near mouse
                        Dim mousePos = Me.PointToClient(Cursor.Position)
                        picPreview.Location = New Point(mousePos.X + 20, mousePos.Y + 20)

                        ' Ensure it stays within form bounds
                        If picPreview.Right > Me.ClientSize.Width Then
                            picPreview.Left = mousePos.X - picPreview.Width - 20
                        End If
                        If picPreview.Bottom > Me.ClientSize.Height Then
                            picPreview.Top = mousePos.Y - picPreview.Height - 20
                        End If

                        picPreview.Visible = True
                        picPreview.BringToFront()
                    End If
                Catch ex As Exception
                    ' Ignore invalid image data
                End Try
            End If
        End If
    End Sub

    Private Sub dgvSource_CellMouseLeave(sender As Object, e As DataGridViewCellEventArgs) Handles dgvSource.CellMouseLeave
        picPreview.Visible = False
        picPreview.Image = Nothing
    End Sub

    ' 2026-01-12 16:35:00 Row Numbering Implementation
    Private Sub dgv_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles dgvSource.RowPostPaint, dgvTarget.RowPostPaint
        Dim dgv As DataGridView = DirectCast(sender, DataGridView)
        Dim rowIdx As String = (e.RowIndex + 1).ToString()

        ' Calculate the bounds for the row header
        Dim headerBounds As Rectangle = New Rectangle(e.RowBounds.Left, e.RowBounds.Top, dgv.RowHeadersWidth, e.RowBounds.Height)

        Using b As New SolidBrush(dgv.RowHeadersDefaultCellStyle.ForeColor)
            Dim format As New StringFormat()
            format.Alignment = StringAlignment.Center
            format.LineAlignment = StringAlignment.Center
            e.Graphics.DrawString(rowIdx, dgv.RowHeadersDefaultCellStyle.Font, b, headerBounds, format)
        End Using
    End Sub

    ' 2026-01-12 16:35:00 Pagination Variables
    Private _currentSourcePage As Integer = 1
    Private _currentTargetPage As Integer = 1
    Private _sourcePageSize As Integer = 100 ' 2026-01-13 Separate Source Page Size
    Private _targetPageSize As Integer = 100 ' 2026-01-13 Separate Target Page Size

    Private Sub cboLimit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboLimit.SelectedIndexChanged
        If Not _isLoaded Then Return ' 로딩 중 이벤트 무시
        ' 2026-01-13 Auto-Search Disabled
        ' 변수만 업데이트
        Dim val As Integer = 100
        If cboLimit.SelectedItem IsNot Nothing AndAlso Integer.TryParse(cboLimit.SelectedItem.ToString(), val) Then
            _sourcePageSize = val
        End If
    End Sub

    ' 2026-01-13 Target Limit Change
    Private Sub cboLimitTarget_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboLimitTarget.SelectedIndexChanged
        If Not _isLoaded Then Return
        ' 변수만 업데이트
        Dim val As Integer = 100
        If cboLimitTarget.SelectedItem IsNot Nothing AndAlso Integer.TryParse(cboLimitTarget.SelectedItem.ToString(), val) Then
            _targetPageSize = val
        End If
    End Sub

    ' 2026-01-12 16:40:00 Numbered Pagination Logic (Source)
    Private Sub RenderSourcePagination(totalRows As Integer)
        pnlSourcePagination.Controls.Clear()

        Dim totalPages As Integer = Math.Ceiling(totalRows / _sourcePageSize)
        If totalPages = 0 Then totalPages = 1

        Dim startPage As Integer = ((_currentSourcePage - 1) \ 10) * 10 + 1
        Dim endPage As Integer = Math.Min(startPage + 9, totalPages)

        ' [<] Prev Block Button
        If startPage > 1 Then
            Dim btn As New ModernButton()
            btn.Text = "<"
            btn.Size = New Size(30, 30)
            btn.Tag = startPage - 1
            btn.BorderRadius = 5
            btn.CustomBaseColor = ThemeManager.CurrentPalette.Accent
            AddHandler btn.Click, AddressOf SourcePageButton_Click
            pnlSourcePagination.Controls.Add(btn)
        End If

        ' Numbered Buttons
        For i As Integer = startPage To endPage
            Dim btn As New ModernButton()
            btn.Text = i.ToString()
            btn.Size = New Size(40, 30)
            btn.Tag = i
            btn.BorderRadius = 5

            If i = _currentSourcePage Then
                btn.ForeColor = Color.White
                btn.CustomBaseColor = ThemeManager.CurrentPalette.Accent
            Else
                btn.ForeColor = ThemeManager.CurrentPalette.ForeColor
                btn.CustomBaseColor = Color.Transparent
            End If

            AddHandler btn.Click, AddressOf SourcePageButton_Click
            pnlSourcePagination.Controls.Add(btn)
        Next

        ' [>] Next Block Button
        If endPage < totalPages Then
            Dim btn As New ModernButton()
            btn.Text = ">"
            btn.Size = New Size(30, 30)
            btn.Tag = endPage + 1
            btn.BorderRadius = 5
            btn.CustomBaseColor = ThemeManager.CurrentPalette.Accent
            AddHandler btn.Click, AddressOf SourcePageButton_Click
            pnlSourcePagination.Controls.Add(btn)
        End If
    End Sub

    ' 2026-01-12 16:40:00 Numbered Pagination Logic (Target)
    Private Sub RenderTargetPagination(totalRows As Integer)
        pnlTargetPagination.Controls.Clear()

        Dim totalPages As Integer = Math.Ceiling(totalRows / _targetPageSize)
        If totalPages = 0 Then totalPages = 1

        Dim startPage As Integer = ((_currentTargetPage - 1) \ 10) * 10 + 1
        Dim endPage As Integer = Math.Min(startPage + 9, totalPages)

        ' [<] Prev Block Button
        If startPage > 1 Then
            Dim btn As New ModernButton()
            btn.Text = "<"
            btn.Size = New Size(30, 30)
            btn.Tag = startPage - 1
            btn.BorderRadius = 5
            btn.CustomBaseColor = ThemeManager.CurrentPalette.Accent
            AddHandler btn.Click, AddressOf TargetPageButton_Click
            pnlTargetPagination.Controls.Add(btn)
        End If

        ' Numbered Buttons
        For i As Integer = startPage To endPage
            Dim btn As New ModernButton()
            btn.Text = i.ToString()
            btn.Size = New Size(40, 30)
            btn.Tag = i
            btn.BorderRadius = 5

            If i = _currentTargetPage Then
                btn.ForeColor = Color.White
                btn.CustomBaseColor = ThemeManager.CurrentPalette.Accent
            Else
                btn.ForeColor = ThemeManager.CurrentPalette.ForeColor
                btn.CustomBaseColor = Color.Transparent
            End If

            AddHandler btn.Click, AddressOf TargetPageButton_Click
            pnlTargetPagination.Controls.Add(btn)
        Next

        ' [>] Next Block Button
        If endPage < totalPages Then
            Dim btn As New ModernButton()
            btn.Text = ">"
            btn.Size = New Size(30, 30)
            btn.Tag = endPage + 1
            btn.BorderRadius = 5
            btn.CustomBaseColor = ThemeManager.CurrentPalette.Accent
            AddHandler btn.Click, AddressOf TargetPageButton_Click
            pnlTargetPagination.Controls.Add(btn)
        End If
    End Sub

    Private Sub SourcePageButton_Click(sender As Object, e As EventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        _currentSourcePage = Convert.ToInt32(btn.Tag)
        SearchSource()
    End Sub

    Private Sub TargetPageButton_Click(sender As Object, e As EventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        _currentTargetPage = Convert.ToInt32(btn.Tag)
        SearchTarget()
    End Sub

    ' 분리된 Source 조회 메서드
    Private Sub SearchSource()
        If _sourceHelper Is Nothing Then Return

        Dim nameKeyword As String = txtSourceSearchName.Text.Trim()
        Dim selectedDong As String = If(cboSourceDong.SelectedItem Is Nothing, "전체", cboSourceDong.SelectedItem.ToString())
        Dim selectedHo As String = If(cboSourceHo.SelectedItem Is Nothing, "전체", cboSourceHo.SelectedItem.ToString())

        Dim whereSource As New StringBuilder("WHERE 1=1 ")

        If Not String.IsNullOrEmpty(nameKeyword) Then
            whereSource.AppendFormat("AND MbName LIKE '%{0}%' ", nameKeyword)
        End If

        If selectedDong <> "전체" Then
            whereSource.AppendFormat("AND DongAddr = '{0}' ", selectedDong)
        End If

        If selectedHo <> "전체" Then
            whereSource.AppendFormat("AND HoAddr = '{0}' ", selectedHo)
        End If

        Try
            Dim sourceCountQuery As String = "SELECT COUNT(*) FROM T_Member " & whereSource.ToString()
            Dim totalSourceCount As Integer = 0
            Dim dtSourceCount = _sourceHelper.ExecuteQuery(sourceCountQuery)
            If dtSourceCount.Rows.Count > 0 Then
                totalSourceCount = Convert.ToInt32(dtSourceCount.Rows(0)(0))
            End If

            Dim offsetSource As Integer = (_currentSourcePage - 1) * _sourcePageSize
            ' OFFSET (SQL Server 2012+)
            Dim sqlSource As String = String.Format("SELECT * FROM T_Member {0} ORDER BY MbNo OFFSET {1} ROWS FETCH NEXT {2} ROWS ONLY", whereSource.ToString(), offsetSource, _sourcePageSize)

            Dim dtSource As DataTable = _sourceHelper.ExecuteQuery(sqlSource)
            dgvSource.DataSource = dtSource
            grpSource.Text = String.Format("Old DB (Source) : Top {0} / Total {1}", dtSource.Rows.Count, totalSourceCount)
            RenderSourcePagination(totalSourceCount)

        Catch ex As Exception
            ' Fallback for older SQL or errors
            Try
                Dim sqlSourceFB As String = "SELECT TOP 100 * FROM T_Member " & whereSource.ToString()
                Dim dtSourceFB As DataTable = _sourceHelper.ExecuteQuery(sqlSourceFB)
                dgvSource.DataSource = dtSourceFB
                grpSource.Text = "Old DB (Source) : Top 100 (Fallback)"
                RenderSourcePagination(0)
            Catch ex2 As Exception
                Log("Source Search Error: " & ex2.Message)
            End Try
        End Try
    End Sub

    ' 분리된 Target 조회 메서드
    Private Sub SearchTarget()
        If _dbHelper Is Nothing OrElse cboCompany.SelectedIndex < 0 Then
            Log("업체 선택 또는 Target DB 연결을 확인하세요.")
            Return
        End If

        Dim drv As DataRowView = CType(cboCompany.SelectedItem, DataRowView)
        Dim selCompanyCode As String = drv("F_COMPANY_CODE").ToString()
        Dim nameKeyword As String = txtTargetSearchName.Text.Trim()

        Dim whereTarget As New StringBuilder("WHERE 1=1 ")
        whereTarget.AppendFormat("AND F_COMPANY_CODE = '{0}' ", selCompanyCode)

        If Not String.IsNullOrEmpty(nameKeyword) Then
            whereTarget.AppendFormat("AND F_MEMNM LIKE '%{0}%' ", nameKeyword)
        End If

        Try
            Dim targetCountQuery As String = "SELECT COUNT(*) FROM T_MEM " & whereTarget.ToString()
            Dim totalTargetCount As Integer = 0
            Dim dtTargetCount = _dbHelper.ExecuteQuery(targetCountQuery)
            If dtTargetCount.Rows.Count > 0 Then
                totalTargetCount = Convert.ToInt32(dtTargetCount.Rows(0)(0))
            End If

            Dim offsetTarget As Integer = (_currentTargetPage - 1) * _targetPageSize ' Target also uses page size or fixed 100? Using same pagesize for consistency

            Dim sqlTarget As String = String.Format("SELECT * FROM T_MEM {0} ORDER BY F_IDX OFFSET {1} ROWS FETCH NEXT {2} ROWS ONLY", whereTarget.ToString(), offsetTarget, _targetPageSize)

            Dim dtTarget As DataTable = _dbHelper.ExecuteQuery(sqlTarget)
            dgvTarget.DataSource = dtTarget
            grpTarget.Text = String.Format("New DB (Target) : Top {0} / Total {1}", dtTarget.Rows.Count, totalTargetCount)
            RenderTargetPagination(totalTargetCount)

        Catch ex As Exception
            ' Fallback
            Try
                Dim sqlTargetFB As String = "SELECT TOP 100 * FROM T_MEM " & whereTarget.ToString()
                Dim dtTargetFB As DataTable = _dbHelper.ExecuteQuery(sqlTargetFB)
                dgvTarget.DataSource = dtTargetFB
                grpTarget.Text = "New DB (Target) : Top 100 (Fallback)"
                RenderTargetPagination(0)
            Catch ex2 As Exception
                Log("Target Search Error: " & ex2.Message)
            End Try
        End Try
    End Sub

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

    ' 2026-01-13 신규 DB 회원 초기화 버튼 구현
    Private Sub btnInitTargetMember_Click(sender As Object, e As EventArgs) Handles btnInitTargetMember.Click
        ' 1. 업체 선택 유효성 검사
        If cboCompany.SelectedValue Is Nothing Then
            FrmMessage.ShowMsg("초기화할 업체를 선택해주세요.", "알림")
            Return
        End If

        Dim drv As DataRowView = CType(cboCompany.SelectedItem, DataRowView)
        Dim selCompanyCode As String = drv("F_COMPANY_CODE").ToString()
        Dim selCompanyName As String = drv("F_COMPANY_NAME").ToString()

        ' 2. 1차 경고 메시지
        If FrmMessage.ShowMsg(String.Format("선택된 업체[{0}]의 모든 회원 및 동/호 정보가 삭제됩니다." & vbCrLf & "삭제된 데이터는 복구할 수 없습니다." & vbCrLf & "계속 진행하시겠습니까?", selCompanyName),
                              "데이터 삭제 경고", MessageBoxButtons.YesNo) <> DialogResult.Yes Then
            Return
        End If

        ' 3. 비밀번호 인증
        Dim inputPass As String = Microsoft.VisualBasic.Interaction.InputBox("데이터 삭제를 승인하려면 관리자 암호를 입력하세요.", "보안 인증", "")
        If inputPass <> "dycis" Then
            FrmMessage.ShowMsg("비밀번호가 일치하지 않습니다.", "인증 실패")
            Return
        End If

        ' 4. 최종 확인
        If FrmMessage.ShowMsg("정말로 삭제하시겠습니까? (최종 확인)", "삭제 확인", MessageBoxButtons.YesNo) <> DialogResult.Yes Then
            Return
        End If

        ' 5. 데이터 삭제 실행
        Try
            Log(String.Format(">>> [{0}] 데이터 초기화 시작...", selCompanyName))

            Dim selCompanyIdx As Integer = Convert.ToInt32(drv("F_IDX"))

            Dim sql As New StringBuilder()
            sql.AppendLine(String.Format("DELETE FROM T_MEM_PHOTO WHERE F_COMPANY_CODE = '{0}' AND F_COMPANY_IDX = {1};", selCompanyCode, selCompanyIdx))
            sql.AppendLine(String.Format("DELETE FROM T_MEM WHERE F_COMPANY_CODE = '{0}' AND F_COMPANY_IDX = {1};", selCompanyCode, selCompanyIdx))
            sql.AppendLine(String.Format("DELETE FROM T_DONG_HO WHERE F_COMPANY_CODE = '{0}' AND F_COMPANY_IDX = {1};", selCompanyCode, selCompanyIdx))
            sql.AppendLine(String.Format("DELETE FROM T_DONG WHERE F_COMPANY_CODE = '{0}' AND F_COMPANY_IDX = {1};", selCompanyCode, selCompanyIdx))

            _dbHelper.ExecuteQuery(sql.ToString()) ' ExecuteQuery returns DataTable.
            ' DBHelper only exposes ExecuteQuery which uses SqlDataAdapter.Fill. 
            ' For DELETE statements, it works fine (returns empty table) and commits immediately.

            Log("데이터 초기화 완료.")
            FrmMessage.ShowMsg("초기화가 완료되었습니다.", "완료")

            ' 그리드 갱신
            SearchTarget()

        Catch ex As Exception
            Log("초기화 중 오류 발생: " & ex.Message)
            FrmMessage.ShowMsg("오류가 발생했습니다: " & ex.Message, "오류")
        End Try
    End Sub

    Private Function ReadIni(section As String, key As String, def As String) As String
        Dim sb As New StringBuilder(255)
        GetPrivateProfileString(section, key, def, sb, 255, _iniPath)
        Return sb.ToString()
    End Function

    ' 2026-01-13 UI 개선: 이관 버튼 중앙 정렬 (pnlCenterAction Resize)
    Private Sub pnlCenterAction_Resize(sender As Object, e As EventArgs) Handles pnlCenterAction.Resize
        If btnMigrateMember IsNot Nothing Then
            btnMigrateMember.Left = (pnlCenterAction.Width - btnMigrateMember.Width) \ 2
            btnMigrateMember.Top = (pnlCenterAction.Height - btnMigrateMember.Height) \ 2
        End If
    End Sub

    Private Sub btnExcelMigrate_Click(sender As Object, e As EventArgs)

    End Sub
End Class
