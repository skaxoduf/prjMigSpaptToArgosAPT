Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Drawing.Drawing2D ' 추가: SmoothingMode 사용을 위해 필요
Imports System.Data.OleDb ' 엑셀 읽기를 위해 추가

Public Class Form1
    ' (2026-01-18) Win32 API 선언 제거 -> SettingsHelper로 이동





    Private _dbHelper As DBHelper
    Private _iniPath As String = Application.StartupPath & "\setting.ini"

    ' 설정값 변수 (업체 정보는 ComboBox에서 선택)
    Private _targetDbName As String = ""
    Private _sourceDbName As String = ""

    ' 2026-01-12 10:45:00 Source DB 조회를 위한 헬퍼 추가
    Private _sourceHelper As DBHelper

    ' 2026-01-18 Setting Helper 추가
    Private _settingsHelper As SettingsHelper

    ' 2026-01-13 초기화 완료 플래그
    Private _isLoaded As Boolean = False

    ' 2026-01-14 원형 진행바 (코드 생성)
    Private _loadingBar As CircularProgressBar

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 콤보박스 초기화 (LoadCompanyList에서 설정하므로 여기서는 제거)

        ' 2026-01-12 (Removed) ThemeManager.SetTheme/ApplyTheme - Designer 제어를 위해 제거
        ThemeManager.SetTheme(ThemeManager.AppTheme.TokyoNight) ' Enable Dark Theme for MessageBox
        ' ThemeManager.ApplyTheme(Me) ' Form1 itself is styled by Designer, so skip applying to Me if needed, but SetTheme is crucial for other forms.

        Log("프로그램 시작... 설정 파일 로드 중")
        LoadSettings()

        ' 2026-01-12 11:00:00 동/호 리스트 초기화는 DB 연결 후 진행
        cboLimit.SelectedIndex = 1 ' Default 100
        cboLimitTarget.SelectedIndex = 1 ' Default 100 for Target

        cboCourseLimit.SelectedIndex = 1 ' Default 100
        cboCourseLimitTarget.SelectedIndex = 1 ' Default 100

        ' 2026-01-16 강좌 탭 날짜 검색 초기화 (이번 달 1일 ~ 오늘)
        dtpCourseStart.Value = New DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)
        dtpCourseEnd.Value = DateTime.Now
        txtCourseSourceSearchName.PlaceholderText = "회원명 검색"


        ' 2026-01-13 (Changes) Layout Order
        pnlCenterAction.SendToBack()
        grpTarget.BringToFront()

        ' Ensure visibility
        grpTarget.Visible = True
        pnlCenterAction.Visible = True

        ' 2026-01-18 Tokyo Night Style 적용
        UiHelper.ApplyGridTheme(dgvSource)
        UiHelper.ApplyGridTheme(dgvTarget)
        UiHelper.ApplyGridTheme(dgvCourseSource)
        UiHelper.ApplyGridTheme(dgvCourseTarget)

        ' 2026-01-18 GroupBox Title Color (Readability Fix)
        grpCourseSource.ForeColor = Color.FromArgb(192, 202, 245) ' Tokyo Night Text
        grpCourseTarget.ForeColor = Color.FromArgb(192, 202, 245)
        grpSource.ForeColor = Color.FromArgb(192, 202, 245)
        grpTarget.ForeColor = Color.FromArgb(192, 202, 245)


        ' 2026-01-13 (Removed) CustomizeButtons - Designer 제어를 위해 제거
        ' CustomizeButtons()

        ' 2026-01-18 (Restored) _loadingBar - 동적 생성 복구 (사용자 요청)
        ' Designer에 'loadingBar'가 없으면 코드로 생성
        Dim foundCtl = Me.Controls.Find("loadingBar", True).FirstOrDefault()
        If foundCtl Is Nothing Then foundCtl = Me.Controls.Find("CircularProgressBar1", True).FirstOrDefault()

        If foundCtl IsNot Nothing AndAlso TypeOf foundCtl Is CircularProgressBar Then
            _loadingBar = DirectCast(foundCtl, CircularProgressBar)
            _loadingBar.Visible = False
        Else
            ' 동적 생성 (Tokyo Night Style)
            _loadingBar = New CircularProgressBar()
            _loadingBar.Name = "loadingBar"
            _loadingBar.Size = New Size(150, 150)
            _loadingBar.Location = New Point((Me.Width - _loadingBar.Width) \ 2, (Me.Height - _loadingBar.Height) \ 2)
            _loadingBar.Anchor = AnchorStyles.None

            ' Style
            _loadingBar.ForeColor = Color.White
            _loadingBar.TrackColor = Color.FromArgb(26, 27, 38) ' Background/Track
            _loadingBar.ProgressColor = Color.FromArgb(122, 162, 247) ' Accent Blue
            _loadingBar.LineThickness = 15
            _loadingBar.Value = 0
            _loadingBar.Maximum = 100

            _loadingBar.Font = New Font("Segoe UI", 16, FontStyle.Bold)
            _loadingBar.Text = "Processing..."

            _loadingBar.Visible = False
            Me.Controls.Add(_loadingBar)
            _loadingBar.BringToFront()
        End If

        ' 초기화 완료
        _isLoaded = True
    End Sub

    ' 2026-01-13 버튼 스타일 초기화
    ' 2026-01-13 (Removed) CustomizeButtons - Designer에서 설정하세요.
    ' Private Sub CustomizeButtons() ... End Sub

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
            ' 2026-01-18 SettingsHelper 사용
            _settingsHelper = New SettingsHelper(_iniPath)

            Dim serverIp As String = _settingsHelper.ReadIni("DB", "ServerIP", "")
            Dim userId As String = _settingsHelper.ReadIni("DB", "UserID", "")
            Dim password As String = _settingsHelper.ReadIni("DB", "Password", "")
            _targetDbName = _settingsHelper.ReadIni("DB", "TargetDB", "")
            _sourceDbName = _settingsHelper.ReadIni("DB", "SourceDB", "")

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
                LoadCourseDongList() ' 강좌 탭 동 리스트 로드
                LoadCourseDongList() ' 강좌 탭 동 리스트 로드
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

        ' 2026-01-16 공통 이관 확인 로직 (Refactored)
        If Not MigrationUtils.AskMigrationConfirmation(targetCount, selCompanyName, "회원") Then
            Return
        End If
        'Dim proceed As Boolean = True


        If proceed Then
            Log("=== 회원 이관 시작 (Old DB -> New DB) ===")

            Log(String.Format("검증 완료. 시작: Target={0}, Source={1}", _targetDbName, _sourceDbName))
            Log(String.Format("대상: {0} (IDX:{1})", selCompanyName, selCompanyIdx))

            ' 진행바 설정 (총 4단계: SP1, SP2, Dong, Member)
            If _loadingBar IsNot Nothing Then
                _loadingBar.Maximum = 4
                _loadingBar.Value = 0
                _loadingBar.Visible = True
                _loadingBar.BringToFront()
            End If
            Application.DoEvents()

            ' 2026-01-13 저장 프로시저(SP) 자동 배포 기능 (공통 모듈 사용)

            ' SP 목록 정의
            Dim spList As String() = {"USP_MIG_DONG_HO.sql", "USP_MIG_MEMBER_TO_MEM.sql"}

            ' SP 배포 실행
            Dim deploySuccess As Boolean = MigrationUtils.DeploySpList(_dbHelper, spList, AddressOf Log, _loadingBar)

            If Not deploySuccess Then
                If _loadingBar IsNot Nothing Then _loadingBar.Visible = False
                Return
            End If

            Try
                ' 1. 동/호 정보 이관 (2026-01-09 추가)
                Dim resultDong = _dbHelper.ExecuteMigrationSP("USP_MIG_DONG_HO", _sourceDbName, selCompanyIdx, selCompanyCode)
                Log("동/호 이관 결과: " & resultDong)
                If _loadingBar IsNot Nothing Then _loadingBar.Value += 1 ' Step 3 Complete
                Application.DoEvents()

                ' 2. 회원 정보 이관
                Dim resultMem = _dbHelper.ExecuteMigrationSP("USP_MIG_MEMBER_TO_MEM", _sourceDbName, selCompanyIdx, selCompanyCode)
                Log("회원 이관 결과: " & resultMem)
                If _loadingBar IsNot Nothing Then _loadingBar.Value += 1 ' Step 4 Complete
                Application.DoEvents()

            Catch ex As Exception
                Log("실행 중 오류 발생: " & ex.Message)
            Finally
                If _loadingBar IsNot Nothing Then _loadingBar.Visible = False
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

        ' 2026-01-18 ExcelHelper 사용
        Dim dtExcel As DataTable = ExcelHelper.LoadExcelToDataTable(filePath, AddressOf Log)

        If dtExcel IsNot Nothing Then
            ' 그리드 바인딩
            dgvSource.DataSource = dtExcel
            _isExcelMode = True

            ' UI 업데이트
            grpSource.Text = String.Format("Excel Preview : {0} Rows (File: {1})", dtExcel.Rows.Count, Path.GetFileName(filePath))
            pnlSourcePagination.Controls.Clear() ' 엑셀 모드에서는 페이지네이션 숨김

            Log(String.Format("엑셀 로드 완료: {0}건. 내용을 확인 후 [이관하기] 버튼을 누르세요.", dtExcel.Rows.Count))
            btnMigrateMember.Text = "엑셀 이관하기" ' 버튼 텍스트 변경
        Else
            _isExcelMode = False
        End If
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
        If _loadingBar IsNot Nothing Then
            _loadingBar.Maximum = dtExcel.Rows.Count
            _loadingBar.Value = 0
            _loadingBar.Visible = True
            _loadingBar.BringToFront()
        End If
        Application.DoEvents()

        Try
            For Each row As DataRow In dtExcel.Rows
                If _loadingBar IsNot Nothing Then _loadingBar.Value += 1 ' 진행률 업데이트

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

                    Dim result As DataTable = _dbHelper.ExecuteSqlWithResultCheck(sql)
                    If result IsNot Nothing AndAlso result.Rows.Count > 0 Then
                        Dim res As String = result.Rows(0)("Result").ToString()
                        Dim msg As String = ""
                        If result.Columns.Contains("Msg") Then msg = result.Rows(0)("Msg").ToString()

                        If res = "SUCCESS" Then
                            successCount += 1
                        Else
                            failCount += 1
                            Log(String.Format("실패 ({0}동 {1}호): {2} (Msg: {3})", dong, ho, res, msg))
                        End If
                    Else
                        failCount += 1
                        Log(String.Format("실패 ({0}동 {1}호): 결과 반환 없음", dong, ho))
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
            If _loadingBar IsNot Nothing Then _loadingBar.Visible = False
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

    ' 2026-01-12 11:00:00 동 리스트 로드 (Source DB 기준) - Refactored
    Private Sub LoadDongList()
        MigrationUtils.LoadDongList(_sourceHelper, cboSourceDong, AddressOf Log)
    End Sub

    Private Sub cboSourceDong_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSourceDong.SelectedIndexChanged
        If _sourceHelper Is Nothing OrElse cboSourceDong.SelectedItem Is Nothing Then Return
        Dim selectedDong As String = cboSourceDong.SelectedItem.ToString()
        MigrationUtils.LoadHoList(_sourceHelper, selectedDong, cboSourceHo, AddressOf Log)
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

    ' 2026-01-12 (Removed) Tab Control OwnerDraw - Designer 제어를 위해 제거
    ' Private Sub tabMain_DrawItem(...) Handles tabMain.DrawItem
    ' ...
    ' End Sub

    ' 2026-01-12 16:16:00 Image Hover Preview
    Private Sub dgvSource_CellMouseEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgvSource.CellMouseEnter
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Return

        Dim dgv As DataGridView = DirectCast(sender, DataGridView)
        Dim colName As String = dgv.Columns(e.ColumnIndex).Name

        If colName = "MbSajin" OrElse colName = "MbPhoto" Then
            Dim cellVal = dgv.Rows(e.RowIndex).Cells(e.ColumnIndex).Value

            If cellVal IsNot Nothing AndAlso TypeOf cellVal Is Byte() Then
                ' 2026-01-18 UiHelper 사용
                Dim bytes As Byte() = DirectCast(cellVal, Byte())

                ' 회원 정보 가져오기
                Dim mbName As String = "Unknown"
                Dim mbNo As String = "Unknown"

                ' 안전하게 컬럼 값 가져오기
                If dgv.Columns.Contains("MbName") Then mbName = dgv.Rows(e.RowIndex).Cells("MbName").Value.ToString()
                If dgv.Columns.Contains("MbNo") Then mbNo = dgv.Rows(e.RowIndex).Cells("MbNo").Value.ToString()

                Dim bmp As Bitmap = UiHelper.CreatePreviewImage(bytes, mbName, mbNo)

                If bmp IsNot Nothing Then
                    picPreview.Image = bmp

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
            End If
        End If
    End Sub

    Private Sub dgvSource_CellMouseLeave(sender As Object, e As DataGridViewCellEventArgs) Handles dgvSource.CellMouseLeave
        picPreview.Visible = False
        picPreview.Image = Nothing
    End Sub

    ' 2026-01-12 16:35:00 Row Numbering Implementation
    Private Sub dgv_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles dgvSource.RowPostPaint, dgvTarget.RowPostPaint, dgvCourseSource.RowPostPaint, dgvCourseTarget.RowPostPaint, dgvCourseSource.RowPostPaint, dgvCourseTarget.RowPostPaint
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

    ' 2026-01-16 강좌 탭 페이징 변수
    Private _currentCourseSourcePage As Integer = 1
    Private _currentCourseTargetPage As Integer = 1
    Private _courseSourcePageSize As Integer = 50
    Private _courseTargetPageSize As Integer = 100

    Private Sub cboCourseLimit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCourseLimit.SelectedIndexChanged
        If Not _isLoaded Then Return
        Dim val As Integer = 50
        If cboCourseLimit.SelectedItem IsNot Nothing AndAlso Integer.TryParse(cboCourseLimit.SelectedItem.ToString(), val) Then
            _courseSourcePageSize = val
        End If
    End Sub

    Private Sub cboCourseLimitTarget_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCourseLimitTarget.SelectedIndexChanged
        If Not _isLoaded Then Return
        Dim val As Integer = 100
        If cboCourseLimitTarget.SelectedItem IsNot Nothing AndAlso Integer.TryParse(cboCourseLimitTarget.SelectedItem.ToString(), val) Then
            _courseTargetPageSize = val
        End If
    End Sub

    ' 2026-01-12 16:40:00 Numbered Pagination Logic (Source)
    Private Sub RenderSourcePagination(totalRows As Integer)
        ' 2026-01-18 PaginationHelper 사용
        PaginationHelper.RenderPagination(pnlSourcePagination, totalRows, _sourcePageSize, _currentSourcePage, AddressOf SourcePageButton_Click)
    End Sub


    ' 2026-01-12 16:40:00 Numbered Pagination Logic (Target)
    Private Sub RenderTargetPagination(totalRows As Integer)
        ' 2026-01-18 PaginationHelper 사용
        PaginationHelper.RenderPagination(pnlTargetPagination, totalRows, _targetPageSize, _currentTargetPage, AddressOf TargetPageButton_Click)
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

    ' (2026-01-18) ReadIni 함수 제거 (SettingsHelper로 대체)

    ' 2026-01-13 UI 개선: 이관 버튼 중앙 정렬 (pnlCenterAction Resize)
    Private Sub pnlCenterAction_Resize(sender As Object, e As EventArgs) Handles pnlCenterAction.Resize
        If btnMigrateMember IsNot Nothing Then
            btnMigrateMember.Left = (pnlCenterAction.Width - btnMigrateMember.Width) \ 2
            btnMigrateMember.Top = (pnlCenterAction.Height - btnMigrateMember.Height) \ 2
        End If
    End Sub

    ' ==========================================
    ' 강좌 이관 관련 코드 (2026-01-16 추가)
    ' ==========================================

    ' 2026-01-16 강좌 탭 페이징 렌더러 (Source)
    Private Sub RenderCourseSourcePagination(totalRows As Integer)
        ' 2026-01-18 PaginationHelper 사용
        PaginationHelper.RenderPagination(pnlCourseSourcePagination, totalRows, _courseSourcePageSize, _currentCourseSourcePage, AddressOf CourseSourcePageButton_Click)
    End Sub

    ' 2026-01-16 강좌 탭 페이징 렌더러 (Target)
    Private Sub RenderCourseTargetPagination(totalRows As Integer)
        ' 2026-01-18 PaginationHelper 사용
        PaginationHelper.RenderPagination(pnlCourseTargetPagination, totalRows, _courseTargetPageSize, _currentCourseTargetPage, AddressOf CourseTargetPageButton_Click)
    End Sub

    Private Sub CourseSourcePageButton_Click(sender As Object, e As EventArgs)
        Dim btn As ModernButton = DirectCast(sender, ModernButton)
        _currentCourseSourcePage = Convert.ToInt32(btn.Tag)
        btnCourseSourceSearch.PerformClick()
    End Sub

    Private Sub CourseTargetPageButton_Click(sender As Object, e As EventArgs)
        Dim btn As ModernButton = DirectCast(sender, ModernButton)
        _currentCourseTargetPage = Convert.ToInt32(btn.Tag)
        btnCourseTargetSearch.PerformClick()
    End Sub

    ' 1. 강좌 Old DB 조회 (동/호 검색 추가)
    Private Sub btnCourseSourceSearch_Click(sender As Object, e As EventArgs) Handles btnCourseSourceSearch.Click
        If _sourceHelper Is Nothing Then Return

        Try
            ' 2026-01-17 10:25:00 코드 시작: Source 조회 쿼리 수정 (JOIN 추가, Dong/Ho 조회)
            ' 조건: 날짜 범위, (회원명 OR 강좌명)
            Dim startDate As String = dtpCourseStart.Value.ToString("yyyy-MM-dd")
            Dim endDate As String = dtpCourseEnd.Value.ToString("yyyy-MM-dd")
            Dim searchName As String = txtCourseSourceSearchName.Text.Trim()
            Dim dong As String = If(cboCourseSourceDong.SelectedItem IsNot Nothing, cboCourseSourceDong.SelectedItem.ToString(), "전체")
            Dim ho As String = If(cboCourseSourceHo.SelectedItem IsNot Nothing, cboCourseSourceHo.SelectedItem.ToString(), "전체")

            ' rbcode = '0001' 조건 추가 (필수)
            ' 테이블 Alias 사용: A = T_TrsInout, B = T_Member
            Dim whereClause As String = String.Format("A.TrsDate BETWEEN '{0}' AND '{1}' AND A.rbcode = '0001'", startDate, endDate)

            ' 이름 검색 (회원이름만 검색, 강좌명 제외)
            If Not String.IsNullOrEmpty(searchName) Then
                whereClause &= String.Format(" AND (B.MbName LIKE '%{0}%')", searchName)
            End If

            ' 동/호 검색 (JOIN이 있으므로 가능)
            If dong <> "전체" Then
                whereClause &= String.Format(" AND B.DongAddr = '{0}'", dong)
            End If
            If ho <> "전체" Then
                whereClause &= String.Format(" AND B.HoAddr = '{0}'", ho)
            End If

            ' Count Query
            Dim countQuery As String = "SELECT COUNT(*) FROM T_TrsInout A LEFT JOIN T_Member B ON A.MbNo = B.MbNo WHERE " & whereClause
            Dim totalRows As Integer = 0
            Dim dtCount As DataTable = _sourceHelper.ExecuteQuery(countQuery)
            If dtCount.Rows.Count > 0 Then
                totalRows = Convert.ToInt32(dtCount.Rows(0)(0))
            End If

            ' Paging Query (OFFSET/FETCH 사용, RowNum 컬럼 제거)
            Dim offset As Integer = (_currentCourseSourcePage - 1) * _courseSourcePageSize

            ' T_TrsInout의 모든 컬럼(A.*)과 T_Member의 동,호,이름(B.DongAddr...)을 함께 조회
            Dim query As String = String.Format("SELECT A.*, B.DongAddr, B.HoAddr, B.MbName " &
                                                "FROM T_TrsInout A " &
                                                "LEFT JOIN T_Member B ON A.MbNo = B.MbNo " &
                                                "WHERE {0} " &
                                                "ORDER BY A.TrsDate DESC, A.TrsNo DESC " &
                                                "OFFSET {1} ROWS FETCH NEXT {2} ROWS ONLY",
                                                whereClause, offset, _courseSourcePageSize)

            Dim dt As DataTable = _sourceHelper.ExecuteQuery(query)
            dgvCourseSource.DataSource = dt
            grpCourseSource.Text = String.Format("Old DB (Source) - 매출내역 : 총 {0}건 (Page {1})", totalRows.ToString("N0"), _currentCourseSourcePage)

            ' Render Pagination
            RenderCourseSourcePagination(totalRows)
            ' 2026-01-17 10:25:00 코드 끝

        Catch ex As Exception
            Log("강좌 소스 조회 실패: " & ex.Message)
        End Try
    End Sub

    ' 강좌 탭용 동 리스트 로드 - Refactored
    Private Sub LoadCourseDongList()
        MigrationUtils.LoadDongList(_sourceHelper, cboCourseSourceDong, AddressOf Log)
    End Sub

    ' 강좌 탭용 동 선택 시 호 리스트 로드 - Refactored
    Private Sub cboCourseSourceDong_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCourseSourceDong.SelectedIndexChanged
        If _sourceHelper Is Nothing OrElse cboCourseSourceDong.SelectedItem Is Nothing Then Return
        Dim selectedDong As String = cboCourseSourceDong.SelectedItem.ToString()
        MigrationUtils.LoadHoList(_sourceHelper, selectedDong, cboCourseSourceHo, AddressOf Log)
    End Sub

    ' 2. 강좌 엑셀 불러오기 (기존 회원 엑셀 로직 재사용 가능여부 확인 -> 별도 로직이 안전)
    Private Sub btnCourseLoadExcel_Click(sender As Object, e As EventArgs) Handles btnCourseLoadExcel.Click
        Dim ofd As New OpenFileDialog()
        ofd.Filter = "Excel Files|*.xlsx;*.xls"
        ofd.Title = "강좌 엑셀 파일 선택"

        If ofd.ShowDialog() = DialogResult.OK Then
            LoadCourseExcelToGrid(ofd.FileName)
        End If
    End Sub

    Private Sub LoadCourseExcelToGrid(filePath As String)
        ' TODO: 엑셀 로드 구현 (회원과 유사하게)
        ' 현재는 Old DB 위주이므로 스텁만 작성
        FrmMessage.ShowMsg("아직 구현되지 않았습니다. DB 조회를 이용해주세요.", "알림")
    End Sub

    ' 3. 강좌 이관 실행
    Private Sub btnMigrateCourse_Click(sender As Object, e As EventArgs) Handles btnMigrateCourse.Click
        If Not MigrationUtils.ValidateCompanySelection(cboCompany) Then Return
        If Not MigrationUtils.ValidateDbSettings(_targetDbName, _sourceDbName) Then Return

        Dim drv = CType(cboCompany.SelectedItem, DataRowView)
        Dim selCompanyCode = drv("F_COMPANY_CODE").ToString()
        Dim selCompanyName = drv("F_COMPANY_NAME").ToString()

        ' 2026-01-16 데이터 존재 여부 확인 및 공통 확인 로직
        Dim targetCount As Integer = 0
        Try
            Dim countQuery As String = String.Format("SELECT COUNT(*) FROM T_ORDER_DETAIL_GANGJWA_INFO WHERE F_COMPANY_CODE = '{0}'", selCompanyCode)
            Dim dtCount = _dbHelper.ExecuteQuery(countQuery)
            If dtCount.Rows.Count > 0 Then
                targetCount = Convert.ToInt32(dtCount.Rows(0)(0))
            End If
        Catch ex As Exception
            Log("데이터 확인 중 오류 발생: " & ex.Message)
            Return
        End Try

        If Not MigrationUtils.AskMigrationConfirmation(targetCount, selCompanyName, "강좌") Then Return

        Log("=== 강좌 이관 시작 ===")
        If _loadingBar IsNot Nothing Then _loadingBar.Visible = True

        Try
            ' SP 배포
            If Not MigrationUtils.DeploySpList(_dbHelper, {"USP_MIG_OLD_TO_NEW_GANGJWA.sql"}, AddressOf Log) Then
                If _loadingBar IsNot Nothing Then _loadingBar.Visible = False
                Return
            End If

            ' SP 실행
            Log(">>> 강좌 통합 이관 프로시저 실행 중 (시간이 소요될 수 있습니다)...")
            Dim result As DataTable = _dbHelper.ExecuteSqlWithResultCheck(String.Format("EXEC USP_MIG_OLD_TO_NEW_GANGJWA @P_CompanyCode='{0}', @OldDbName='{1}'", selCompanyCode, _sourceDbName))

            If result IsNot Nothing AndAlso result.Rows.Count > 0 Then
                Dim res As String = result.Rows(0)(0).ToString()
                Dim errMsg As String = ""
                If result.Columns.Contains("ErrorMessage") AndAlso result.Rows(0)("ErrorMessage") IsNot DBNull.Value Then
                    errMsg = result.Rows(0)("ErrorMessage").ToString()
                End If

                Log("실행 결과: " & res & If(String.IsNullOrEmpty(errMsg), "", " (" & errMsg & ")"))
            End If

            Log("=== 강좌 이관 완료 ===")
            btnCourseTargetSearch.PerformClick() ' 결과 조회

        Catch ex As Exception
            Log("강좌 이관 중 오류: " & ex.Message)
        Finally
            If _loadingBar IsNot Nothing Then _loadingBar.Visible = False
        End Try
    End Sub

    ' 4. 강좌 New DB 조회 (Target) - T_ORDER_DETAIL_GANGJWA_INFO 조회
    Private Sub btnCourseTargetSearch_Click(sender As Object, e As EventArgs) Handles btnCourseTargetSearch.Click
        If _dbHelper Is Nothing OrElse cboCompany.SelectedIndex < 0 Then Return

        Dim drv = CType(cboCompany.SelectedItem, DataRowView)
        Dim selCompanyCode = drv("F_COMPANY_CODE").ToString()

        Try
            ' 2026-01-17 10:23:00 코드 시작: Target 조회 쿼리 수정 (통으로 조회)
            Dim searchName As String = txtCourseTargetSearchName.Text.Trim()
            Dim whereClause As String = String.Format("F_COMPANY_CODE = '{0}'", selCompanyCode)

            ' 이름 검색 (F_MEM_NAME 컬럼이 있다고 가정 - SP 확인 결과 있음)
            If Not String.IsNullOrEmpty(searchName) Then
                whereClause &= String.Format(" AND (F_MEM_NAME LIKE '%{0}%')", searchName)
            End If

            ' Count
            Dim countQuery As String = "SELECT COUNT(*) FROM T_ORDER_DETAIL_GANGJWA_INFO WHERE " & whereClause
            Dim totalRows As Integer = 0
            Dim dtCount = _dbHelper.ExecuteQuery(countQuery)
            If dtCount.Rows.Count > 0 Then
                totalRows = Convert.ToInt32(dtCount.Rows(0)(0))
            End If

            ' Paging
            Dim offset As Integer = (_currentCourseTargetPage - 1) * _courseTargetPageSize

            ' SELECT * FROM T_ORDER_DETAIL_GANGJWA_INFO
            Dim query As String = String.Format("SELECT * FROM T_ORDER_DETAIL_GANGJWA_INFO " &
                                                "WHERE {0} " &
                                                "ORDER BY F_IDX DESC " &
                                                "OFFSET {1} ROWS FETCH NEXT {2} ROWS ONLY",
                                                whereClause, offset, _courseTargetPageSize)

            Dim dt As DataTable = _dbHelper.ExecuteQuery(query)

            dgvCourseTarget.DataSource = dt
            grpCourseTarget.Text = String.Format("New DB (Target) - 강좌 이력 : 총 {0}건 (Page {1})", totalRows.ToString("N0"), _currentCourseTargetPage)

            RenderCourseTargetPagination(totalRows)
            ' 2026-01-17 10:23:00 코드 끝

        Catch ex As Exception
            Log("강좌 타겟 조회 실패: " & ex.Message)
        End Try
    End Sub

    ' 2026-01-17 10:32:00 코드 시작: 강좌 탭 검색창 엔터키 이벤트 추가
    Private Sub txtCourseSourceSearchName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCourseSourceSearchName.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            btnCourseSourceSearch.PerformClick()
        End If
    End Sub

    Private Sub txtCourseTargetSearchName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCourseTargetSearchName.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            btnCourseTargetSearch.PerformClick()
        End If
    End Sub
    ' 2026-01-17 10:32:00 코드 끝

    ' 5. 강좌 데이터 초기화
    Private Sub btnInitTargetCourse_Click(sender As Object, e As EventArgs) Handles btnInitTargetCourse.Click
        If _dbHelper Is Nothing OrElse cboCompany.SelectedIndex < 0 Then Return

        Dim drv = CType(cboCompany.SelectedItem, DataRowView)
        Dim selCompanyName = drv("F_COMPANY_NAME").ToString()
        Dim selCompanyCode = drv("F_COMPANY_CODE").ToString()

        If MigrationUtils.AskDeleteConfirmation(selCompanyName, "강좌 및 수강 이력") Then

            Log("=== 강좌 데이터 초기화 시작 ===")
            If _loadingBar IsNot Nothing Then _loadingBar.Visible = True

            Try
                ' SP 배포
                If Not MigrationUtils.DeploySpList(_dbHelper, {"USP_RESET_GANGJWA_MIGRATION.sql"}, AddressOf Log) Then
                    If _loadingBar IsNot Nothing Then _loadingBar.Visible = False
                    Return
                End If

                ' SP 실행
                Dim query As String = String.Format("EXEC USP_RESET_GANGJWA_MIGRATION @P_CompanyCode='{0}'", selCompanyCode)
                Dim result As DataTable = _dbHelper.ExecuteSqlWithResultCheck(query)

                If result IsNot Nothing AndAlso result.Rows.Count > 0 Then
                    Dim res As String = result.Rows(0)("Result").ToString()
                    Dim msg As String = result.Rows(0)("Msg").ToString()
                    Log(String.Format("결과: {0} ({1})", res, msg))
                End If

                Log("=== 강좌 데이터 초기화 완료 ===")
                btnCourseTargetSearch.PerformClick() ' 그리드 갱신

            Catch ex As Exception
                Log("강좌 초기화 중 오류: " & ex.Message)
            Finally
                If _loadingBar IsNot Nothing Then _loadingBar.Visible = False
            End Try
        End If
    End Sub

End Class
