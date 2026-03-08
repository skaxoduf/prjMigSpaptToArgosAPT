Imports System.IO
Imports System.Text

' 메인 폼: DB 연결 설정 및 각 탭 이벤트 처리
' 탭별 공통 로직은 TabMigrationHelper 모듈을 사용하여 소스를 최소화
Public Class Form1

    ' DB 헬퍼
    Private dbHelper As DBHelper
    Private sourceHelper As DBHelper
    Private settingsHelper As SettingsHelper
    Private sIniPath As String = Application.StartupPath & "\setting.ini"
    Private sTargetDbName As String = ""
    Private sSourceDbName As String = ""
    Private bIsLoaded As Boolean = False
    Private bIsExcelMode As Boolean = False
    Private loadingBar As CircularProgressBar

    ' 탭별 페이징 상태 (탭별 스칼라 변수 12개를 TabPageState 7개로 대체)
    Private stateMember As New TabMigrationHelper.TabPageState()
    Private stateCourse As New TabMigrationHelper.TabPageState() With {.iSourcePageSize = 50}
    Private stateLocker As New TabMigrationHelper.TabPageState()
    Private stateProduct As New TabMigrationHelper.TabPageState() With {.iSourcePageSize = 50}
    Private stateGeneral As New TabMigrationHelper.TabPageState() With {.iSourcePageSize = 50}
    Private stateAccommodation As New TabMigrationHelper.TabPageState() With {.iSourcePageSize = 50}
    Private stateSpace As New TabMigrationHelper.TabPageState() With {.iSourcePageSize = 50}

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ThemeManager.SetTheme(ThemeManager.AppTheme.TokyoNight)
        Log("프로그램 시작... 설정 파일 로드 중")
        LoadSettings()

        ' 콤보박스 기본값
        cboLimit.SelectedIndex = 1 : cboLimitTarget.SelectedIndex = 1
        cboCourseLimit.SelectedIndex = 1 : cboCourseLimitTarget.SelectedIndex = 1
        cboLockerLimit.SelectedIndex = 1 : cboLockerLimitTarget.SelectedIndex = 1
        cboProductLimit.SelectedIndex = 0 : cboProductLimitTarget.SelectedIndex = 1
        cboGeneralLimit.SelectedIndex = 0 : cboGeneralLimitTarget.SelectedIndex = 1
        cboAccommodationLimit.SelectedIndex = 0 : cboAccommodationLimitTarget.SelectedIndex = 1
        cboSpaceLimit.SelectedIndex = 0 : cboSpaceLimitTarget.SelectedIndex = 1

        ' 날짜 검색 기본값 (이번 달 1일 ~ 오늘)
        Dim dtFirst As New DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)
        dtpCourseStart.Value = dtFirst : dtpCourseEnd.Value = DateTime.Now
        dtpProductStart.Value = dtFirst : dtpProductEnd.Value = DateTime.Now
        dtpGeneralStart.Value = dtFirst : dtpGeneralEnd.Value = DateTime.Now
        dtpAccommodationStart.Value = dtFirst : dtpAccommodationEnd.Value = DateTime.Now
        dtpSpaceStart.Value = dtFirst : dtpSpaceEnd.Value = DateTime.Now

        ' PlaceholderText
        txtCourseSourceSearchName.PlaceholderText = "회원명 검색"
        txtLockerSourceSearchName.PlaceholderText = "회원명 검색"
        txtProductSourceSearchName.PlaceholderText = "상품명 검색"
        txtGeneralSourceSearchName.PlaceholderText = "일반시설명 검색"
        txtAccommodationSourceSearchName.PlaceholderText = "숙박시설명 검색"
        txtSpaceSourceSearchName.PlaceholderText = "공간시설명 검색"

        ' 레이아웃 순서
        pnlCenterAction.SendToBack()
        grpTarget.BringToFront() : grpTarget.Visible = True : pnlCenterAction.Visible = True

        ' 그리드 테마 적용 (Tokyo Night)
        For Each dgv As DataGridView In {dgvSource, dgvTarget, dgvCourseSource, dgvCourseTarget,
                                          dgvLockerSource, dgvLockerTarget, dgvProductSource, dgvProductTarget,
                                          dgvGeneralSource, dgvGeneralTarget,
                                          dgvAccommodationSource, dgvAccommodationTarget,
                                          dgvSpaceSource, dgvSpaceTarget}
            UiHelper.ApplyGridTheme(dgv)
        Next

        ' GroupBox 제목 색상
        Dim titleColor As Color = Color.FromArgb(192, 202, 245)
        For Each grp As GroupBox In {grpSource, grpTarget, grpCourseSource, grpCourseTarget,
                                      grpLockerSource, grpLockerTarget, grpProductSource, grpProductTarget,
                                      grpGeneralSource, grpGeneralTarget,
                                      grpAccommodationSource, grpAccommodationTarget,
                                      grpSpaceSource, grpSpaceTarget}
            grp.ForeColor = titleColor
        Next

        ' 원형 진행바 초기화 (Designer에 없으면 동적 생성)
        Dim foundCtl = Me.Controls.Find("loadingBar", True).FirstOrDefault()
        If foundCtl Is Nothing Then foundCtl = Me.Controls.Find("CircularProgressBar1", True).FirstOrDefault()
        If foundCtl IsNot Nothing AndAlso TypeOf foundCtl Is CircularProgressBar Then
            loadingBar = DirectCast(foundCtl, CircularProgressBar)
            loadingBar.Visible = False
        Else
            loadingBar = New CircularProgressBar() With {
                .Name = "loadingBar", .Size = New Size(150, 150),
                .Location = New Point((Me.Width - 150) \ 2, (Me.Height - 150) \ 2),
                .Anchor = AnchorStyles.None, .ForeColor = Color.White,
                .TrackColor = Color.FromArgb(26, 27, 38),
                .ProgressColor = Color.FromArgb(122, 162, 247),
                .LineThickness = 15, .Value = 0, .Maximum = 100,
                .Font = New Font("Segoe UI", 16, FontStyle.Bold),
                .Text = "Processing...", .Visible = False}
            Me.Controls.Add(loadingBar)
            loadingBar.BringToFront()
        End If

        bIsLoaded = True
    End Sub

    ' 설정 파일(setting.ini) 로드 및 DB 연결 초기화
    Private Sub LoadSettings()
        If Not File.Exists(sIniPath) Then
            Log("Error: setting.ini 파일을 찾을 수 없습니다.") : Return
        End If
        Try
            settingsHelper = New SettingsHelper(sIniPath)
            Dim sServerIp As String = settingsHelper.ReadIni("DB", "ServerIP", "")
            Dim sUserId As String = settingsHelper.ReadIni("DB", "UserID", "")
            Dim sPassword As String = settingsHelper.ReadIni("DB", "Password", "")
            sTargetDbName = settingsHelper.ReadIni("DB", "TargetDB", "")
            sSourceDbName = settingsHelper.ReadIni("DB", "SourceDB", "")
            dbHelper = New DBHelper(sServerIp, sTargetDbName, sUserId, sPassword)
            sourceHelper = New DBHelper(sServerIp, sSourceDbName, sUserId, sPassword)
            Log(String.Format("설정 로드 완료. Target DB: {0}, Source DB: {1}", sTargetDbName, sSourceDbName))
            If dbHelper.TestConnection() Then
                Log("DB 접속 성공!")
                LoadCompanyList()
                ' 전체 탭 동 리스트를 한 번에 로드
                For Each cbo As ComboBox In {cboSourceDong, cboCourseSourceDong, cboLockerSourceDong,
                                              cboProductSourceDong, cboGeneralSourceDong,
                                              cboAccommodationSourceDong, cboSpaceSourceDong}
                    MigrationUtils.LoadDongList(sourceHelper, cbo, AddressOf Log)
                Next
            Else
                Log("Error: DB 접속 실패. 설정을 확인하세요.")
                btnMigrateMember.Enabled = False
            End If
        Catch ex As Exception
            Log("설정 로드 중 오류: " & ex.Message)
        End Try
    End Sub

    ' T_COMPANY 업체 목록 로드
    Private Sub LoadCompanyList()
        Try
            Dim dt As DataTable = dbHelper.GetCompanyList()
            dt.Columns.Add("DisplayCol", GetType(String),
                "'[IDX: ' + F_IDX + '] [CODE: ' + F_COMPANY_CODE + '] ' + F_COMPANY_NAME + ' (회원수: ' + F_MEM_COUNT + '명)'")
            cboCompany.DataSource = dt
            cboCompany.DisplayMember = "DisplayCol"
            cboCompany.ValueMember = "F_IDX"
            btnMigrateMember.Enabled = dt.Rows.Count > 0
            Log(If(dt.Rows.Count > 0, "업체 목록 로드 완료: " & dt.Rows.Count & "개",
                   "Error: 등록된 업체(T_COMPANY)가 없습니다."))
        Catch ex As Exception
            Log("업체 목록 조회 실패: " & ex.Message)
        End Try
    End Sub

    ' 로그 출력 (화면 + 파일 동시)
    Private Sub Log(msg As String)
        rtbLog.AppendText(String.Format("[{0}] {1}{2}", DateTime.Now.ToString("HH:mm:ss"), msg, Environment.NewLine))
        rtbLog.ScrollToCaret()
        Logger.WriteLog(msg)
    End Sub

    ' 선택된 업체 코드 반환 (반복 코드 제거)
    Private Function GetSelectedCompanyCode() As String
        If cboCompany.SelectedItem Is Nothing Then Return ""
        Return CType(cboCompany.SelectedItem, DataRowView)("F_COMPANY_CODE").ToString()
    End Function

    ' 선택된 업체명 반환
    Private Function GetSelectedCompanyName() As String
        If cboCompany.SelectedItem Is Nothing Then Return ""
        Return CType(cboCompany.SelectedItem, DataRowView)("F_COMPANY_NAME").ToString()
    End Function

    ' SQL 인젝션 방지용 작은따옴표 이스케이프
    Private Function Q(s As String) As String
        Return If(String.IsNullOrEmpty(s), "", s.Replace("'", "''"))
    End Function

    ' 업체 변경 시 회원 타겟 그리드 초기화
    Private Sub cboCompany_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCompany.SelectedIndexChanged
        If Not bIsLoaded Then Return
        If dgvTarget.DataSource IsNot Nothing Then
            dgvTarget.DataSource = Nothing
            grpTarget.Text = "New DB (Target)"
            pnlTargetPagination.Controls.Clear()
            stateMember.iCurrentTargetPage = 1
        End If
    End Sub

    ' 환경설정 버튼
    Private Sub btnSetting_Click(sender As Object, e As EventArgs) Handles btnSetting.Click
        Dim frm As New FormSetting
        If frm.ShowDialog = DialogResult.OK Then
            Log("설정이 변경되었습니다. 설정을 다시 로드합니다.")
            LoadSettings()
        End If
    End Sub

    ' 패널 크기 변경 시 이관 버튼 중앙 정렬
    Private Sub pnlCenterAction_Resize(sender As Object, e As EventArgs) Handles pnlCenterAction.Resize
        If btnMigrateMember IsNot Nothing Then
            btnMigrateMember.Left = (pnlCenterAction.Width - btnMigrateMember.Width) \ 2
            btnMigrateMember.Top = (pnlCenterAction.Height - btnMigrateMember.Height) \ 2
        End If
    End Sub

    ' DataGridView 데이터 오류 무시 (이미지 렌더링 등)
    Private Sub dgvSource_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvSource.DataError
        e.ThrowException = False : e.Cancel = False
    End Sub

    Private Sub dgvTarget_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvTarget.DataError
        e.ThrowException = False : e.Cancel = False
    End Sub

    ' 행 헤더에 순번 표시 (전체 탭 그리드 공통)
    Private Sub dgv_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) _
        Handles dgvSource.RowPostPaint, dgvTarget.RowPostPaint,
                dgvCourseSource.RowPostPaint, dgvCourseTarget.RowPostPaint,
                dgvLockerSource.RowPostPaint, dgvLockerTarget.RowPostPaint,
                dgvProductSource.RowPostPaint, dgvProductTarget.RowPostPaint,
                dgvGeneralSource.RowPostPaint, dgvGeneralTarget.RowPostPaint,
                dgvAccommodationSource.RowPostPaint, dgvAccommodationTarget.RowPostPaint,
                dgvSpaceSource.RowPostPaint, dgvSpaceTarget.RowPostPaint

        Dim dgv As DataGridView = DirectCast(sender, DataGridView)
        Dim bounds As New Rectangle(e.RowBounds.Left, e.RowBounds.Top, dgv.RowHeadersWidth, e.RowBounds.Height)
        Using b As New SolidBrush(dgv.RowHeadersDefaultCellStyle.ForeColor)
            Dim fmt As New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center}
            e.Graphics.DrawString((e.RowIndex + 1).ToString(), dgv.RowHeadersDefaultCellStyle.Font, b, bounds, fmt)
        End Using
    End Sub

    ' 소스 그리드 사진 컬럼 마우스오버 프리뷰
    Private Sub dgvSource_CellMouseEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgvSource.CellMouseEnter
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Return
        Dim dgv As DataGridView = DirectCast(sender, DataGridView)
        Dim sColName As String = dgv.Columns(e.ColumnIndex).Name
        If sColName <> "MbSajin" AndAlso sColName <> "MbPhoto" Then Return
        Dim cellVal = dgv.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
        If cellVal Is Nothing OrElse Not TypeOf cellVal Is Byte() Then Return
        Dim sMbName As String = If(dgv.Columns.Contains("MbName"), dgv.Rows(e.RowIndex).Cells("MbName").Value?.ToString(), "Unknown")
        Dim sMbNo As String = If(dgv.Columns.Contains("MbNo"), dgv.Rows(e.RowIndex).Cells("MbNo").Value?.ToString(), "Unknown")
        Dim bmp As Bitmap = UiHelper.CreatePreviewImage(DirectCast(cellVal, Byte()), sMbName, sMbNo)
        If bmp Is Nothing Then Return
        picPreview.Image = bmp
        Dim mousePos = Me.PointToClient(Cursor.Position)
        picPreview.Location = New Point(mousePos.X + 20, mousePos.Y + 20)
        If picPreview.Right > Me.ClientSize.Width Then picPreview.Left = mousePos.X - picPreview.Width - 20
        If picPreview.Bottom > Me.ClientSize.Height Then picPreview.Top = mousePos.Y - picPreview.Height - 20
        picPreview.Visible = True : picPreview.BringToFront()
    End Sub

    Private Sub dgvSource_CellMouseLeave(sender As Object, e As DataGridViewCellEventArgs) Handles dgvSource.CellMouseLeave
        picPreview.Visible = False : picPreview.Image = Nothing
    End Sub

    ' =============================================
    ' 회원 탭
    ' =============================================

    Private Sub btnMigrateMember_Click(sender As Object, e As EventArgs) Handles btnMigrateMember.Click
        If bIsExcelMode Then MigrateFromExcel() Else MigrateFromDB()
    End Sub

    ' Old DB → New DB 회원 이관 (proceed 버그 수정: 불필요한 변수 제거)
    Private Sub MigrateFromDB()
        If Not MigrationUtils.ValidateCompanySelection(cboCompany) Then Return
        If Not MigrationUtils.ValidateDbSettings(sTargetDbName, sSourceDbName) Then Return
        Dim drv = CType(cboCompany.SelectedItem, DataRowView)
        Dim iCompanyIdx As Integer = Convert.ToInt32(drv("F_IDX"))
        Dim sCode As String = drv("F_COMPANY_CODE").ToString()
        Dim sName As String = drv("F_COMPANY_NAME").ToString()
        Dim iTargetCount As Integer = 0
        Try
            Dim dtCount = dbHelper.ExecuteQuery(String.Format("SELECT COUNT(*) FROM T_MEM WHERE F_COMPANY_CODE = '{0}'", sCode))
            If dtCount.Rows.Count > 0 Then iTargetCount = Convert.ToInt32(dtCount.Rows(0)(0))
        Catch ex As Exception
            Log("데이터 확인 중 오류 발생: " & ex.Message) : Return
        End Try
        If Not MigrationUtils.AskMigrationConfirmation(iTargetCount, sName, "회원") Then Return
        Log(String.Format("=== 회원 이관 시작. 대상: {0} (IDX:{1}) ===", sName, iCompanyIdx))
        If loadingBar IsNot Nothing Then
            loadingBar.Maximum = 4 : loadingBar.Value = 0
            loadingBar.Visible = True : loadingBar.BringToFront()
        End If
        Application.DoEvents()
        If Not MigrationUtils.DeploySpList(dbHelper, {"USP_MIG_DONG_HO.sql", "USP_MIG_MEMBER_TO_MEM.sql"}, AddressOf Log, loadingBar) Then
            If loadingBar IsNot Nothing Then loadingBar.Visible = False : Return
        End If
        Try
            Log("동/호 이관 결과: " & dbHelper.ExecuteMigrationSP("USP_MIG_DONG_HO", sSourceDbName, iCompanyIdx, sCode))
            If loadingBar IsNot Nothing Then loadingBar.Value += 1 : Application.DoEvents()
            Log("회원 이관 결과: " & dbHelper.ExecuteMigrationSP("USP_MIG_MEMBER_TO_MEM", sSourceDbName, iCompanyIdx, sCode))
            If loadingBar IsNot Nothing Then loadingBar.Value += 1 : Application.DoEvents()
        Catch ex As Exception
            Log("실행 중 오류 발생: " & ex.Message)
        Finally
            If loadingBar IsNot Nothing Then loadingBar.Visible = False
        End Try
        Log("=== 작업 종료 === 결과 확인은 조회 버튼을 누르세요.")
    End Sub

    Private Sub btnLoadExcel_Click(sender As Object, e As EventArgs) Handles btnLoadExcel.Click
        Dim ofd As New OpenFileDialog() With {.Filter = "Excel Files|*.xlsx;*.xls", .Title = "회원 엑셀 파일 선택", .InitialDirectory = Application.StartupPath}
        If ofd.ShowDialog() = DialogResult.OK Then LoadExcelToGrid(ofd.FileName)
    End Sub

    Private Sub LoadExcelToGrid(sFilePath As String)
        Log("=== 엑셀 파일 로딩 시작 ===")
        Dim dtExcel As DataTable = ExcelHelper.LoadExcelToDataTable(sFilePath, AddressOf Log)
        If dtExcel IsNot Nothing Then
            dgvSource.DataSource = dtExcel : bIsExcelMode = True
            grpSource.Text = String.Format("Excel Preview : {0} Rows (File: {1})", dtExcel.Rows.Count, Path.GetFileName(sFilePath))
            pnlSourcePagination.Controls.Clear()
            Log(String.Format("엑셀 로드 완료: {0}건. [이관하기] 버튼을 누르세요.", dtExcel.Rows.Count))
            btnMigrateMember.Text = "엑셀 이관하기"
        Else
            bIsExcelMode = False
        End If
    End Sub

    Private Function SafeStr(dt As DataTable, row As DataRow, sColName As String) As String
        If Not dt.Columns.Contains(sColName) OrElse row(sColName) Is DBNull.Value Then Return ""
        Return Q(row(sColName).ToString())
    End Function

    Private Function SafeDate(dt As DataTable, row As DataRow, sColName As String) As String
        If Not dt.Columns.Contains(sColName) OrElse row(sColName) Is DBNull.Value Then Return ""
        Try : Return Convert.ToDateTime(row(sColName)).ToString("yyyy-MM-dd")
        Catch : Return ""
        End Try
    End Function

    Private Sub MigrateFromExcel()
        If Not bIsExcelMode OrElse dgvSource.DataSource Is Nothing Then
            FrmMessage.ShowMsg("먼저 [엑셀 불러오기]를 통해 데이터를 로드해주세요.", "알림") : Return
        End If
        If Not MigrationUtils.ValidateCompanySelection(cboCompany) Then Return
        Dim sCode As String = GetSelectedCompanyCode() : Dim sName As String = GetSelectedCompanyName()
        Dim dtExcel As DataTable = CType(dgvSource.DataSource, DataTable)
        If FrmMessage.ShowMsg(String.Format("현재 데이터({0}건)를{1}'{2}'(으)로 이관하시겠습니까?",
            dtExcel.Rows.Count, vbCrLf, sName), "이관 확인", MessageBoxButtons.YesNo) <> DialogResult.Yes Then Return
        If Not MigrationUtils.DeploySpList(dbHelper, {"USP_MIG_EXCEL_MEMBER_TO_MEM.sql"}, AddressOf Log) Then Return
        Log("=== 엑셀 데이터 이관 시작 ===")
        Dim iSuccess As Integer = 0, iFail As Integer = 0
        If loadingBar IsNot Nothing Then loadingBar.Maximum = dtExcel.Rows.Count : loadingBar.Value = 0 : loadingBar.Visible = True : loadingBar.BringToFront()
        Application.DoEvents()
        Try
            For Each row As DataRow In dtExcel.Rows
                If loadingBar IsNot Nothing Then loadingBar.Value += 1
                Dim sDong As String = If(row("동") Is DBNull.Value, "", row("동").ToString().Trim())
                Dim sHo As String = If(row("호") Is DBNull.Value, "", row("호").ToString().Trim())
                If String.IsNullOrWhiteSpace(sDong) OrElse String.IsNullOrWhiteSpace(sHo) Then Continue For
                Try
                    Dim sql As String = String.Format(
                        "EXEC USP_MIG_EXCEL_MEMBER_TO_MEM '{0}','{1}','{2}','{3}',N'{4}','{5}','{6}','{7}',N'{8}','{9}',N'{10}'",
                        sCode, Q(sDong), Q(sHo), SafeStr(dtExcel, row, "카드번호"),
                        SafeStr(dtExcel, row, "입주민 명"), SafeDate(dtExcel, row, "등록일자"),
                        SafeStr(dtExcel, row, "핸드폰"), SafeStr(dtExcel, row, "연락처"),
                        SafeStr(dtExcel, row, "메모"), SafeDate(dtExcel, row, "생일"), SafeStr(dtExcel, row, "성별"))
                    Dim result As DataTable = dbHelper.ExecuteSqlWithResultCheck(sql)
                    If result IsNot Nothing AndAlso result.Rows.Count > 0 AndAlso result.Rows(0)("Result").ToString() = "SUCCESS" Then
                        iSuccess += 1
                    Else
                        iFail += 1 : Log(String.Format("실패 ({0}동 {1}호)", sDong, sHo))
                    End If
                Catch exRow As Exception
                    iFail += 1 : Log(String.Format("오류 ({0}동 {1}호): {2}", sDong, sHo, exRow.Message))
                End Try
                Application.DoEvents()
            Next
            Log(String.Format("완료: 성공 {0}, 실패 {1}", iSuccess, iFail))
            btnTargetSearch.PerformClick()
        Catch ex As Exception
            Log("이관 중 치명적 오류: " & ex.Message)
        Finally
            If loadingBar IsNot Nothing Then loadingBar.Visible = False
        End Try
        Log("=== 엑셀 데이터 이관 종료 ===")
    End Sub

    Private Sub btnSourceSearch_Click(sender As Object, e As EventArgs) Handles btnSourceSearch.Click
        bIsExcelMode = False : btnMigrateMember.Text = "회원 이관하기"
        stateMember.iCurrentSourcePage = 1
        SearchMemberSource()
    End Sub

    Private Sub btnTargetSearch_Click(sender As Object, e As EventArgs) Handles btnTargetSearch.Click
        stateMember.iCurrentTargetPage = 1
        SearchMemberTarget()
    End Sub

    ' 회원 소스 그리드 조회 (검색 버튼 및 페이지 버튼 공용)
    Private Sub SearchMemberSource()
        TabMigrationHelper.SearchSourceMember(sourceHelper, dgvSource, grpSource, stateMember,
            pnlSourcePagination, AddressOf MemberSourcePageButton_Click,
            txtSourceSearchName, cboSourceDong, cboSourceHo, "Old DB (Source)", AddressOf Log)
    End Sub

    ' 회원 타겟 그리드 조회 (검색 버튼 및 페이지 버튼 공용)
    Private Sub SearchMemberTarget()
        TabMigrationHelper.SearchTargetCommon(dbHelper, dgvTarget, grpTarget, stateMember,
            pnlTargetPagination, AddressOf MemberTargetPageButton_Click,
            GetSelectedCompanyCode(), txtTargetSearchName, "T_MEM", "F_MEMNM", "New DB (Target)", AddressOf Log)
    End Sub

    Private Sub txtSourceSearchName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSourceSearchName.KeyDown
        If e.KeyCode = Keys.Enter Then e.SuppressKeyPress = True : btnSourceSearch.PerformClick()
    End Sub

    Private Sub txtTargetSearchName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtTargetSearchName.KeyDown
        If e.KeyCode = Keys.Enter Then e.SuppressKeyPress = True : btnTargetSearch.PerformClick()
    End Sub

    Private Sub cboLimit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboLimit.SelectedIndexChanged
        If Not bIsLoaded Then Return
        Dim iVal As Integer = 100
        If Integer.TryParse(cboLimit.SelectedItem?.ToString(), iVal) Then stateMember.iSourcePageSize = iVal
    End Sub

    Private Sub cboLimitTarget_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboLimitTarget.SelectedIndexChanged
        If Not bIsLoaded Then Return
        Dim iVal As Integer = 100
        If Integer.TryParse(cboLimitTarget.SelectedItem?.ToString(), iVal) Then stateMember.iTargetPageSize = iVal
    End Sub

    ' 페이지 버튼 클릭: 페이지 번호 설정 후 Search 메서드 직접 호출 (PerformClick 금지 - 1페이지 초기화 버그)
    Private Sub MemberSourcePageButton_Click(sender As Object, e As EventArgs)
        stateMember.iCurrentSourcePage = Convert.ToInt32(DirectCast(sender, ModernButton).Tag)
        SearchMemberSource()
    End Sub

    Private Sub MemberTargetPageButton_Click(sender As Object, e As EventArgs)
        stateMember.iCurrentTargetPage = Convert.ToInt32(DirectCast(sender, ModernButton).Tag)
        SearchMemberTarget()
    End Sub

    Private Sub cboSourceDong_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSourceDong.SelectedIndexChanged
        If sourceHelper Is Nothing OrElse cboSourceDong.SelectedItem Is Nothing Then Return
        MigrationUtils.LoadHoList(sourceHelper, cboSourceDong.SelectedItem.ToString(), cboSourceHo, AddressOf Log)
    End Sub

    Private Sub btnInitTargetMember_Click(sender As Object, e As EventArgs) Handles btnInitTargetMember.Click
        If cboCompany.SelectedValue Is Nothing Then
            FrmMessage.ShowMsg("초기화할 업체를 선택해주세요.", "알림") : Return
        End If
        Dim drv As DataRowView = CType(cboCompany.SelectedItem, DataRowView)
        Dim sCode As String = drv("F_COMPANY_CODE").ToString()
        Dim sName As String = drv("F_COMPANY_NAME").ToString()
        Dim iIdx As Integer = Convert.ToInt32(drv("F_IDX"))
        If Not MigrationUtils.AskDeleteConfirmation(sName, "회원 및 동/호") Then Return
        Try
            Log(String.Format(">>> [{0}] 데이터 초기화 시작...", sName))
            dbHelper.ExecuteNonQueryWithTransaction(New String() {
                String.Format("DELETE FROM T_MEM_PHOTO WHERE F_COMPANY_CODE = '{0}' AND F_COMPANY_IDX = {1}", sCode, iIdx),
                String.Format("DELETE FROM T_MEM WHERE F_COMPANY_CODE = '{0}' AND F_COMPANY_IDX = {1}", sCode, iIdx),
                String.Format("DELETE FROM T_DONG_HO WHERE F_COMPANY_CODE = '{0}' AND F_COMPANY_IDX = {1}", sCode, iIdx),
                String.Format("DELETE FROM T_DONG WHERE F_COMPANY_CODE = '{0}' AND F_COMPANY_IDX = {1}", sCode, iIdx)
            })
            Log("데이터 초기화 완료.")
            FrmMessage.ShowMsg("초기화가 완료되었습니다.", "완료")
            btnTargetSearch.PerformClick()
        Catch ex As Exception
            Log("초기화 중 오류 발생: " & ex.Message)
            FrmMessage.ShowMsg("오류가 발생했습니다: " & ex.Message, "오류")
        End Try
    End Sub

    ' =============================================
    ' 강좌 탭
    ' =============================================

    Private Sub cboCourseLimit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCourseLimit.SelectedIndexChanged
        If Not bIsLoaded Then Return
        Dim iVal As Integer = 50
        If Integer.TryParse(cboCourseLimit.SelectedItem?.ToString(), iVal) Then stateCourse.iSourcePageSize = iVal
    End Sub

    Private Sub cboCourseLimitTarget_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCourseLimitTarget.SelectedIndexChanged
        If Not bIsLoaded Then Return
        Dim iVal As Integer = 100
        If Integer.TryParse(cboCourseLimitTarget.SelectedItem?.ToString(), iVal) Then stateCourse.iTargetPageSize = iVal
    End Sub

    Private Sub cboCourseSourceDong_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCourseSourceDong.SelectedIndexChanged
        If sourceHelper Is Nothing OrElse cboCourseSourceDong.SelectedItem Is Nothing Then Return
        MigrationUtils.LoadHoList(sourceHelper, cboCourseSourceDong.SelectedItem.ToString(), cboCourseSourceHo, AddressOf Log)
    End Sub

    Private Sub txtCourseSourceSearchName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCourseSourceSearchName.KeyDown
        If e.KeyCode = Keys.Enter Then e.SuppressKeyPress = True : btnCourseSourceSearch.PerformClick()
    End Sub

    Private Sub txtCourseTargetSearchName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCourseTargetSearchName.KeyDown
        If e.KeyCode = Keys.Enter Then e.SuppressKeyPress = True : btnCourseTargetSearch.PerformClick()
    End Sub

    Private Sub btnCourseSourceSearch_Click(sender As Object, e As EventArgs) Handles btnCourseSourceSearch.Click
        stateCourse.iCurrentSourcePage = 1 : SearchCourseSource()
    End Sub

    Private Sub btnCourseTargetSearch_Click(sender As Object, e As EventArgs) Handles btnCourseTargetSearch.Click
        stateCourse.iCurrentTargetPage = 1 : SearchCourseTarget()
    End Sub

    Private Sub SearchCourseSource()
        TabMigrationHelper.SearchSourceWithTrsInout(sourceHelper, dgvCourseSource, grpCourseSource, stateCourse,
            pnlCourseSourcePagination, AddressOf CourseSourcePageButton_Click,
            dtpCourseStart, dtpCourseEnd, txtCourseSourceSearchName, cboCourseSourceDong, cboCourseSourceHo,
            "Old DB (Source) - 매출내역", AddressOf Log, "AND A.rbcode = '0001'")
    End Sub

    Private Sub SearchCourseTarget()
        TabMigrationHelper.SearchTargetCommon(dbHelper, dgvCourseTarget, grpCourseTarget, stateCourse,
            pnlCourseTargetPagination, AddressOf CourseTargetPageButton_Click,
            GetSelectedCompanyCode(), txtCourseTargetSearchName,
            "T_ORDER_DETAIL_GANGJWA_INFO", "F_MEM_NAME", "New DB (Target) - 강좌 이력", AddressOf Log)
    End Sub

    Private Sub CourseSourcePageButton_Click(sender As Object, e As EventArgs)
        stateCourse.iCurrentSourcePage = Convert.ToInt32(DirectCast(sender, ModernButton).Tag) : SearchCourseSource()
    End Sub

    Private Sub CourseTargetPageButton_Click(sender As Object, e As EventArgs)
        stateCourse.iCurrentTargetPage = Convert.ToInt32(DirectCast(sender, ModernButton).Tag) : SearchCourseTarget()
    End Sub

    Private Sub btnCourseLoadExcel_Click(sender As Object, e As EventArgs) Handles btnCourseLoadExcel.Click
        FrmMessage.ShowMsg("아직 구현되지 않았습니다. DB 조회를 이용해주세요.", "알림")
    End Sub

    Private Sub btnMigrateCourse_Click(sender As Object, e As EventArgs) Handles btnMigrateCourse.Click
        If Not MigrationUtils.ValidateCompanySelection(cboCompany) Then Return
        If Not MigrationUtils.ValidateDbSettings(sTargetDbName, sSourceDbName) Then Return
        Dim sCode As String = GetSelectedCompanyCode() : Dim sName As String = GetSelectedCompanyName()
        Dim iCount As Integer = TabMigrationHelper.GetCount(dbHelper, "T_ORDER_DETAIL_GANGJWA_INFO",
            String.Format("F_COMPANY_CODE = '{0}'", sCode), AddressOf Log)
        If Not MigrationUtils.AskMigrationConfirmation(iCount, sName, "강좌") Then Return
        TabMigrationHelper.ExecuteSPMigration(dbHelper, "USP_MIG_OLD_TO_NEW_GANGJWA.sql",
            "USP_MIG_OLD_TO_NEW_GANGJWA", sCode, sSourceDbName, "강좌",
            loadingBar, AddressOf SearchCourseTarget, AddressOf Log)
    End Sub

    Private Sub btnInitTargetCourse_Click(sender As Object, e As EventArgs) Handles btnInitTargetCourse.Click
        If dbHelper Is Nothing OrElse cboCompany.SelectedIndex < 0 Then Return
        If Not MigrationUtils.AskDeleteConfirmation(GetSelectedCompanyName(), "강좌 및 수강 이력") Then Return
        TabMigrationHelper.ExecuteSPReset(dbHelper, "USP_RESET_GANGJWA_MIGRATION.sql",
            "USP_RESET_GANGJWA_MIGRATION", GetSelectedCompanyCode(), "강좌",
            loadingBar, AddressOf SearchCourseTarget, AddressOf Log)
    End Sub

    ' =============================================
    ' 사물함 탭
    ' =============================================

    Private Sub cboLockerLimit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboLockerLimit.SelectedIndexChanged
        If Not bIsLoaded Then Return
        Dim iVal As Integer = 100
        If Integer.TryParse(cboLockerLimit.SelectedItem?.ToString(), iVal) Then stateLocker.iSourcePageSize = iVal
    End Sub

    Private Sub cboLockerLimitTarget_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboLockerLimitTarget.SelectedIndexChanged
        If Not bIsLoaded Then Return
        Dim iVal As Integer = 100
        If Integer.TryParse(cboLockerLimitTarget.SelectedItem?.ToString(), iVal) Then stateLocker.iTargetPageSize = iVal
    End Sub

    Private Sub cboLockerSourceDong_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboLockerSourceDong.SelectedIndexChanged
        If sourceHelper Is Nothing OrElse cboLockerSourceDong.SelectedItem Is Nothing Then Return
        MigrationUtils.LoadHoList(sourceHelper, cboLockerSourceDong.SelectedItem.ToString(), cboLockerSourceHo, AddressOf Log)
    End Sub

    Private Sub txtLockerSourceSearchName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtLockerSourceSearchName.KeyDown
        If e.KeyCode = Keys.Enter Then e.SuppressKeyPress = True : btnLockerSourceSearch.PerformClick()
    End Sub

    Private Sub txtLockerTargetSearchName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtLockerTargetSearchName.KeyDown
        If e.KeyCode = Keys.Enter Then e.SuppressKeyPress = True : btnLockerTargetSearch.PerformClick()
    End Sub

    Private Sub btnLockerSourceSearch_Click(sender As Object, e As EventArgs) Handles btnLockerSourceSearch.Click
        stateLocker.iCurrentSourcePage = 1 : SearchLockerSource()
    End Sub

    Private Sub btnLockerTargetSearch_Click(sender As Object, e As EventArgs) Handles btnLockerTargetSearch.Click
        stateLocker.iCurrentTargetPage = 1 : SearchLockerTarget()
    End Sub

    Private Sub SearchLockerSource()
        TabMigrationHelper.SearchSourceMember(sourceHelper, dgvLockerSource, grpLockerSource, stateLocker,
            pnlLockerSourcePagination, AddressOf LockerSourcePageButton_Click,
            txtLockerSourceSearchName, cboLockerSourceDong, cboLockerSourceHo, "Old DB (Source) - 사물함내역", AddressOf Log)
    End Sub

    Private Sub SearchLockerTarget()
        TabMigrationHelper.SearchTargetCommon(dbHelper, dgvLockerTarget, grpLockerTarget, stateLocker,
            pnlLockerTargetPagination, AddressOf LockerTargetPageButton_Click,
            GetSelectedCompanyCode(), txtLockerTargetSearchName, "T_MEM", "F_MEMNM", "New DB (Target) - 사물함 이력", AddressOf Log)
    End Sub

    Private Sub LockerSourcePageButton_Click(sender As Object, e As EventArgs)
        stateLocker.iCurrentSourcePage = Convert.ToInt32(DirectCast(sender, ModernButton).Tag) : SearchLockerSource()
    End Sub

    Private Sub LockerTargetPageButton_Click(sender As Object, e As EventArgs)
        stateLocker.iCurrentTargetPage = Convert.ToInt32(DirectCast(sender, ModernButton).Tag) : SearchLockerTarget()
    End Sub

    Private Sub btnLockerLoadExcel_Click(sender As Object, e As EventArgs) Handles btnLockerLoadExcel.Click
        FrmMessage.ShowMsg("아직 구현되지 않았습니다. DB 조회를 이용해주세요.", "알림")
    End Sub

    Private Sub btnMigrateLocker_Click(sender As Object, e As EventArgs) Handles btnMigrateLocker.Click
        If Not MigrationUtils.ValidateCompanySelection(cboCompany) Then Return
        If Not MigrationUtils.ValidateDbSettings(sTargetDbName, sSourceDbName) Then Return
        Dim sCode As String = GetSelectedCompanyCode() : Dim sName As String = GetSelectedCompanyName()
        Dim iCount As Integer = TabMigrationHelper.GetCount(dbHelper, "T_MEM", String.Format("F_COMPANY_CODE = '{0}'", sCode), AddressOf Log)
        If Not MigrationUtils.AskMigrationConfirmation(iCount, sName, "사물함") Then Return
        TabMigrationHelper.ExecuteSPMigration(dbHelper, "USP_MIG_OLD_TO_NEW_LOCKER.sql", "USP_MIG_OLD_TO_NEW_LOCKER", sCode, sSourceDbName, "사물함", loadingBar, AddressOf SearchLockerTarget, AddressOf Log)
    End Sub

    Private Sub btnInitTargetLocker_Click(sender As Object, e As EventArgs) Handles btnInitTargetLocker.Click
        If dbHelper Is Nothing OrElse cboCompany.SelectedIndex < 0 Then Return
        If Not MigrationUtils.AskDeleteConfirmation(GetSelectedCompanyName(), "사물함") Then Return
        TabMigrationHelper.ExecuteSPReset(dbHelper, "USP_RESET_LOCKER_MIGRATION.sql", "USP_RESET_LOCKER_MIGRATION", GetSelectedCompanyCode(), "사물함", loadingBar, AddressOf SearchLockerTarget, AddressOf Log)
    End Sub

    ' =============================================
    ' 상품 탭
    ' =============================================

    Private Sub cboProductLimit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboProductLimit.SelectedIndexChanged
        If Not bIsLoaded Then Return
        Dim iVal As Integer = 50
        If Integer.TryParse(cboProductLimit.SelectedItem?.ToString(), iVal) Then stateProduct.iSourcePageSize = iVal
    End Sub

    Private Sub cboProductLimitTarget_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboProductLimitTarget.SelectedIndexChanged
        If Not bIsLoaded Then Return
        Dim iVal As Integer = 100
        If Integer.TryParse(cboProductLimitTarget.SelectedItem?.ToString(), iVal) Then stateProduct.iTargetPageSize = iVal
    End Sub

    Private Sub cboProductSourceDong_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboProductSourceDong.SelectedIndexChanged
        If sourceHelper Is Nothing OrElse cboProductSourceDong.SelectedItem Is Nothing Then Return
        MigrationUtils.LoadHoList(sourceHelper, cboProductSourceDong.SelectedItem.ToString(), cboProductSourceHo, AddressOf Log)
    End Sub

    Private Sub txtProductSourceSearchName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtProductSourceSearchName.KeyDown
        If e.KeyCode = Keys.Enter Then e.SuppressKeyPress = True : btnProductSourceSearch.PerformClick()
    End Sub

    Private Sub txtProductTargetSearchName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtProductTargetSearchName.KeyDown
        If e.KeyCode = Keys.Enter Then e.SuppressKeyPress = True : btnProductTargetSearch.PerformClick()
    End Sub

    Private Sub btnProductSourceSearch_Click(sender As Object, e As EventArgs) Handles btnProductSourceSearch.Click
        stateProduct.iCurrentSourcePage = 1 : SearchProductSource()
    End Sub

    Private Sub btnProductTargetSearch_Click(sender As Object, e As EventArgs) Handles btnProductTargetSearch.Click
        stateProduct.iCurrentTargetPage = 1 : SearchProductTarget()
    End Sub

    Private Sub SearchProductSource()
        TabMigrationHelper.SearchSourceWithTrsInout(sourceHelper, dgvProductSource, grpProductSource, stateProduct,
            pnlProductSourcePagination, AddressOf ProductSourcePageButton_Click,
            dtpProductStart, dtpProductEnd, txtProductSourceSearchName, cboProductSourceDong, cboProductSourceHo,
            "Old DB (Source) - 상품내역", AddressOf Log)
    End Sub

    Private Sub SearchProductTarget()
        TabMigrationHelper.SearchTargetCommon(dbHelper, dgvProductTarget, grpProductTarget, stateProduct,
            pnlProductTargetPagination, AddressOf ProductTargetPageButton_Click,
            GetSelectedCompanyCode(), txtProductTargetSearchName, "T_ORDER_DETAIL_GANGJWA_INFO", "F_MEM_NAME", "New DB (Target) - 상품 이력", AddressOf Log)
    End Sub

    Private Sub ProductSourcePageButton_Click(sender As Object, e As EventArgs)
        stateProduct.iCurrentSourcePage = Convert.ToInt32(DirectCast(sender, ModernButton).Tag) : SearchProductSource()
    End Sub

    Private Sub ProductTargetPageButton_Click(sender As Object, e As EventArgs)
        stateProduct.iCurrentTargetPage = Convert.ToInt32(DirectCast(sender, ModernButton).Tag) : SearchProductTarget()
    End Sub

    Private Sub btnProductLoadExcel_Click(sender As Object, e As EventArgs) Handles btnProductLoadExcel.Click
        FrmMessage.ShowMsg("아직 구현되지 않았습니다. DB 조회를 이용해주세요.", "알림")
    End Sub

    Private Sub btnMigrateProduct_Click(sender As Object, e As EventArgs) Handles btnMigrateProduct.Click
        If Not MigrationUtils.ValidateCompanySelection(cboCompany) Then Return
        If Not MigrationUtils.ValidateDbSettings(sTargetDbName, sSourceDbName) Then Return
        Dim sCode As String = GetSelectedCompanyCode() : Dim sName As String = GetSelectedCompanyName()
        Dim iCount As Integer = TabMigrationHelper.GetCount(dbHelper, "T_ORDER_DETAIL_GANGJWA_INFO", String.Format("F_COMPANY_CODE = '{0}'", sCode), AddressOf Log)
        If Not MigrationUtils.AskMigrationConfirmation(iCount, sName, "상품") Then Return
        TabMigrationHelper.ExecuteSPMigration(dbHelper, "USP_MIG_OLD_TO_NEW_PRODUCT.sql", "USP_MIG_OLD_TO_NEW_PRODUCT", sCode, sSourceDbName, "상품", loadingBar, AddressOf SearchProductTarget, AddressOf Log)
    End Sub

    Private Sub btnInitTargetProduct_Click(sender As Object, e As EventArgs) Handles btnInitTargetProduct.Click
        If dbHelper Is Nothing OrElse cboCompany.SelectedIndex < 0 Then Return
        If Not MigrationUtils.AskDeleteConfirmation(GetSelectedCompanyName(), "상품") Then Return
        TabMigrationHelper.ExecuteSPReset(dbHelper, "USP_RESET_PRODUCT_MIGRATION.sql", "USP_RESET_PRODUCT_MIGRATION", GetSelectedCompanyCode(), "상품", loadingBar, AddressOf SearchProductTarget, AddressOf Log)
    End Sub

    ' =============================================
    ' 일반시설 탭
    ' =============================================

    Private Sub cboGeneralLimit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboGeneralLimit.SelectedIndexChanged
        If Not bIsLoaded Then Return
        Dim iVal As Integer = 50
        If Integer.TryParse(cboGeneralLimit.SelectedItem?.ToString(), iVal) Then stateGeneral.iSourcePageSize = iVal
    End Sub

    Private Sub cboGeneralLimitTarget_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboGeneralLimitTarget.SelectedIndexChanged
        If Not bIsLoaded Then Return
        Dim iVal As Integer = 100
        If Integer.TryParse(cboGeneralLimitTarget.SelectedItem?.ToString(), iVal) Then stateGeneral.iTargetPageSize = iVal
    End Sub

    Private Sub cboGeneralSourceDong_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboGeneralSourceDong.SelectedIndexChanged
        If sourceHelper Is Nothing OrElse cboGeneralSourceDong.SelectedItem Is Nothing Then Return
        MigrationUtils.LoadHoList(sourceHelper, cboGeneralSourceDong.SelectedItem.ToString(), cboGeneralSourceHo, AddressOf Log)
    End Sub

    Private Sub txtGeneralSourceSearchName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtGeneralSourceSearchName.KeyDown
        If e.KeyCode = Keys.Enter Then e.SuppressKeyPress = True : btnGeneralSourceSearch.PerformClick()
    End Sub

    Private Sub txtGeneralTargetSearchName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtGeneralTargetSearchName.KeyDown
        If e.KeyCode = Keys.Enter Then e.SuppressKeyPress = True : btnGeneralTargetSearch.PerformClick()
    End Sub

    Private Sub btnGeneralSourceSearch_Click(sender As Object, e As EventArgs) Handles btnGeneralSourceSearch.Click
        stateGeneral.iCurrentSourcePage = 1 : SearchGeneralSource()
    End Sub

    Private Sub btnGeneralTargetSearch_Click(sender As Object, e As EventArgs) Handles btnGeneralTargetSearch.Click
        stateGeneral.iCurrentTargetPage = 1 : SearchGeneralTarget()
    End Sub

    Private Sub SearchGeneralSource()
        TabMigrationHelper.SearchSourceWithTrsInout(sourceHelper, dgvGeneralSource, grpGeneralSource, stateGeneral,
            pnlGeneralSourcePagination, AddressOf GeneralSourcePageButton_Click,
            dtpGeneralStart, dtpGeneralEnd, txtGeneralSourceSearchName, cboGeneralSourceDong, cboGeneralSourceHo,
            "Old DB (Source) - 일반시설내역", AddressOf Log)
    End Sub

    Private Sub SearchGeneralTarget()
        TabMigrationHelper.SearchTargetCommon(dbHelper, dgvGeneralTarget, grpGeneralTarget, stateGeneral,
            pnlGeneralTargetPagination, AddressOf GeneralTargetPageButton_Click,
            GetSelectedCompanyCode(), txtGeneralTargetSearchName, "T_ORDER_DETAIL_GANGJWA_INFO", "F_MEM_NAME", "New DB (Target) - 일반시설 이력", AddressOf Log)
    End Sub

    Private Sub GeneralSourcePageButton_Click(sender As Object, e As EventArgs)
        stateGeneral.iCurrentSourcePage = Convert.ToInt32(DirectCast(sender, ModernButton).Tag) : SearchGeneralSource()
    End Sub

    Private Sub GeneralTargetPageButton_Click(sender As Object, e As EventArgs)
        stateGeneral.iCurrentTargetPage = Convert.ToInt32(DirectCast(sender, ModernButton).Tag) : SearchGeneralTarget()
    End Sub

    Private Sub btnGeneralLoadExcel_Click(sender As Object, e As EventArgs) Handles btnGeneralLoadExcel.Click
        FrmMessage.ShowMsg("아직 구현되지 않았습니다. DB 조회를 이용해주세요.", "알림")
    End Sub

    Private Sub btnMigrateGeneral_Click(sender As Object, e As EventArgs) Handles btnMigrateGeneral.Click
        If Not MigrationUtils.ValidateCompanySelection(cboCompany) Then Return
        If Not MigrationUtils.ValidateDbSettings(sTargetDbName, sSourceDbName) Then Return
        Dim sCode As String = GetSelectedCompanyCode() : Dim sName As String = GetSelectedCompanyName()
        Dim iCount As Integer = TabMigrationHelper.GetCount(dbHelper, "T_ORDER_DETAIL_GANGJWA_INFO", String.Format("F_COMPANY_CODE = '{0}'", sCode), AddressOf Log)
        If Not MigrationUtils.AskMigrationConfirmation(iCount, sName, "일반시설") Then Return
        TabMigrationHelper.ExecuteSPMigration(dbHelper, "USP_MIG_OLD_TO_NEW_GENERAL_FACILITY.sql", "USP_MIG_OLD_TO_NEW_GENERAL_FACILITY", sCode, sSourceDbName, "일반시설", loadingBar, AddressOf SearchGeneralTarget, AddressOf Log)
    End Sub

    Private Sub btnInitTargetGeneral_Click(sender As Object, e As EventArgs) Handles btnInitTargetGeneral.Click
        If dbHelper Is Nothing OrElse cboCompany.SelectedIndex < 0 Then Return
        If Not MigrationUtils.AskDeleteConfirmation(GetSelectedCompanyName(), "일반시설") Then Return
        TabMigrationHelper.ExecuteSPReset(dbHelper, "USP_RESET_GENERAL_FACILITY_MIGRATION.sql", "USP_RESET_GENERAL_FACILITY_MIGRATION", GetSelectedCompanyCode(), "일반시설", loadingBar, AddressOf SearchGeneralTarget, AddressOf Log)
    End Sub

    ' =============================================
    ' 숙박시설 탭
    ' =============================================

    Private Sub cboAccommodationLimit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboAccommodationLimit.SelectedIndexChanged
        If Not bIsLoaded Then Return
        Dim iVal As Integer = 50
        If Integer.TryParse(cboAccommodationLimit.SelectedItem?.ToString(), iVal) Then stateAccommodation.iSourcePageSize = iVal
    End Sub

    Private Sub cboAccommodationLimitTarget_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboAccommodationLimitTarget.SelectedIndexChanged
        If Not bIsLoaded Then Return
        Dim iVal As Integer = 100
        If Integer.TryParse(cboAccommodationLimitTarget.SelectedItem?.ToString(), iVal) Then stateAccommodation.iTargetPageSize = iVal
    End Sub

    Private Sub cboAccommodationSourceDong_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboAccommodationSourceDong.SelectedIndexChanged
        If sourceHelper Is Nothing OrElse cboAccommodationSourceDong.SelectedItem Is Nothing Then Return
        MigrationUtils.LoadHoList(sourceHelper, cboAccommodationSourceDong.SelectedItem.ToString(), cboAccommodationSourceHo, AddressOf Log)
    End Sub

    Private Sub txtAccommodationSourceSearchName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtAccommodationSourceSearchName.KeyDown
        If e.KeyCode = Keys.Enter Then e.SuppressKeyPress = True : btnAccommodationSourceSearch.PerformClick()
    End Sub

    Private Sub txtAccommodationTargetSearchName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtAccommodationTargetSearchName.KeyDown
        If e.KeyCode = Keys.Enter Then e.SuppressKeyPress = True : btnAccommodationTargetSearch.PerformClick()
    End Sub

    Private Sub btnAccommodationSourceSearch_Click(sender As Object, e As EventArgs) Handles btnAccommodationSourceSearch.Click
        stateAccommodation.iCurrentSourcePage = 1 : SearchAccommodationSource()
    End Sub

    Private Sub btnAccommodationTargetSearch_Click(sender As Object, e As EventArgs) Handles btnAccommodationTargetSearch.Click
        stateAccommodation.iCurrentTargetPage = 1 : SearchAccommodationTarget()
    End Sub

    Private Sub SearchAccommodationSource()
        TabMigrationHelper.SearchSourceWithTrsInout(sourceHelper, dgvAccommodationSource, grpAccommodationSource, stateAccommodation,
            pnlAccommodationSourcePagination, AddressOf AccommodationSourcePageButton_Click,
            dtpAccommodationStart, dtpAccommodationEnd, txtAccommodationSourceSearchName, cboAccommodationSourceDong, cboAccommodationSourceHo,
            "Old DB (Source) - 숙박시설내역", AddressOf Log)
    End Sub

    Private Sub SearchAccommodationTarget()
        TabMigrationHelper.SearchTargetCommon(dbHelper, dgvAccommodationTarget, grpAccommodationTarget, stateAccommodation,
            pnlAccommodationTargetPagination, AddressOf AccommodationTargetPageButton_Click,
            GetSelectedCompanyCode(), txtAccommodationTargetSearchName, "T_ORDER_DETAIL_GANGJWA_INFO", "F_MEM_NAME", "New DB (Target) - 숙박시설 이력", AddressOf Log)
    End Sub

    Private Sub AccommodationSourcePageButton_Click(sender As Object, e As EventArgs)
        stateAccommodation.iCurrentSourcePage = Convert.ToInt32(DirectCast(sender, ModernButton).Tag) : SearchAccommodationSource()
    End Sub

    Private Sub AccommodationTargetPageButton_Click(sender As Object, e As EventArgs)
        stateAccommodation.iCurrentTargetPage = Convert.ToInt32(DirectCast(sender, ModernButton).Tag) : SearchAccommodationTarget()
    End Sub

    Private Sub btnAccommodationLoadExcel_Click(sender As Object, e As EventArgs) Handles btnAccommodationLoadExcel.Click
        FrmMessage.ShowMsg("아직 구현되지 않았습니다. DB 조회를 이용해주세요.", "알림")
    End Sub

    Private Sub btnMigrateAccommodation_Click(sender As Object, e As EventArgs) Handles btnMigrateAccommodation.Click
        If Not MigrationUtils.ValidateCompanySelection(cboCompany) Then Return
        If Not MigrationUtils.ValidateDbSettings(sTargetDbName, sSourceDbName) Then Return
        Dim sCode As String = GetSelectedCompanyCode() : Dim sName As String = GetSelectedCompanyName()
        Dim iCount As Integer = TabMigrationHelper.GetCount(dbHelper, "T_ORDER_DETAIL_GANGJWA_INFO", String.Format("F_COMPANY_CODE = '{0}'", sCode), AddressOf Log)
        If Not MigrationUtils.AskMigrationConfirmation(iCount, sName, "숙박시설") Then Return
        TabMigrationHelper.ExecuteSPMigration(dbHelper, "USP_MIG_OLD_TO_NEW_ACCOMMODATION.sql", "USP_MIG_OLD_TO_NEW_ACCOMMODATION", sCode, sSourceDbName, "숙박시설", loadingBar, AddressOf SearchAccommodationTarget, AddressOf Log)
    End Sub

    Private Sub btnInitTargetAccommodation_Click(sender As Object, e As EventArgs) Handles btnInitTargetAccommodation.Click
        If dbHelper Is Nothing OrElse cboCompany.SelectedIndex < 0 Then Return
        If Not MigrationUtils.AskDeleteConfirmation(GetSelectedCompanyName(), "숙박시설") Then Return
        TabMigrationHelper.ExecuteSPReset(dbHelper, "USP_RESET_ACCOMMODATION_MIGRATION.sql", "USP_RESET_ACCOMMODATION_MIGRATION", GetSelectedCompanyCode(), "숙박시설", loadingBar, AddressOf SearchAccommodationTarget, AddressOf Log)
    End Sub

    ' =============================================
    ' 공간시설 탭
    ' =============================================

    Private Sub cboSpaceLimit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSpaceLimit.SelectedIndexChanged
        If Not bIsLoaded Then Return
        Dim iVal As Integer = 50
        If Integer.TryParse(cboSpaceLimit.SelectedItem?.ToString(), iVal) Then stateSpace.iSourcePageSize = iVal
    End Sub

    Private Sub cboSpaceLimitTarget_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSpaceLimitTarget.SelectedIndexChanged
        If Not bIsLoaded Then Return
        Dim iVal As Integer = 100
        If Integer.TryParse(cboSpaceLimitTarget.SelectedItem?.ToString(), iVal) Then stateSpace.iTargetPageSize = iVal
    End Sub

    Private Sub cboSpaceSourceDong_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSpaceSourceDong.SelectedIndexChanged
        If sourceHelper Is Nothing OrElse cboSpaceSourceDong.SelectedItem Is Nothing Then Return
        MigrationUtils.LoadHoList(sourceHelper, cboSpaceSourceDong.SelectedItem.ToString(), cboSpaceSourceHo, AddressOf Log)
    End Sub

    Private Sub txtSpaceSourceSearchName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSpaceSourceSearchName.KeyDown
        If e.KeyCode = Keys.Enter Then e.SuppressKeyPress = True : btnSpaceSourceSearch.PerformClick()
    End Sub

    Private Sub txtSpaceTargetSearchName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSpaceTargetSearchName.KeyDown
        If e.KeyCode = Keys.Enter Then e.SuppressKeyPress = True : btnSpaceTargetSearch.PerformClick()
    End Sub

    Private Sub btnSpaceSourceSearch_Click(sender As Object, e As EventArgs) Handles btnSpaceSourceSearch.Click
        stateSpace.iCurrentSourcePage = 1 : SearchSpaceSource()
    End Sub

    Private Sub btnSpaceTargetSearch_Click(sender As Object, e As EventArgs) Handles btnSpaceTargetSearch.Click
        stateSpace.iCurrentTargetPage = 1 : SearchSpaceTarget()
    End Sub

    Private Sub SearchSpaceSource()
        TabMigrationHelper.SearchSourceWithTrsInout(sourceHelper, dgvSpaceSource, grpSpaceSource, stateSpace,
            pnlSpaceSourcePagination, AddressOf SpaceSourcePageButton_Click,
            dtpSpaceStart, dtpSpaceEnd, txtSpaceSourceSearchName, cboSpaceSourceDong, cboSpaceSourceHo,
            "Old DB (Source) - 공간시설내역", AddressOf Log)
    End Sub

    Private Sub SearchSpaceTarget()
        TabMigrationHelper.SearchTargetCommon(dbHelper, dgvSpaceTarget, grpSpaceTarget, stateSpace,
            pnlSpaceTargetPagination, AddressOf SpaceTargetPageButton_Click,
            GetSelectedCompanyCode(), txtSpaceTargetSearchName, "T_ORDER_DETAIL_GANGJWA_INFO", "F_MEM_NAME", "New DB (Target) - 공간시설 이력", AddressOf Log)
    End Sub

    Private Sub SpaceSourcePageButton_Click(sender As Object, e As EventArgs)
        stateSpace.iCurrentSourcePage = Convert.ToInt32(DirectCast(sender, ModernButton).Tag) : SearchSpaceSource()
    End Sub

    Private Sub SpaceTargetPageButton_Click(sender As Object, e As EventArgs)
        stateSpace.iCurrentTargetPage = Convert.ToInt32(DirectCast(sender, ModernButton).Tag) : SearchSpaceTarget()
    End Sub

    Private Sub btnSpaceLoadExcel_Click(sender As Object, e As EventArgs) Handles btnSpaceLoadExcel.Click
        FrmMessage.ShowMsg("아직 구현되지 않았습니다. DB 조회를 이용해주세요.", "알림")
    End Sub

    Private Sub btnMigrateSpace_Click(sender As Object, e As EventArgs) Handles btnMigrateSpace.Click
        If Not MigrationUtils.ValidateCompanySelection(cboCompany) Then Return
        If Not MigrationUtils.ValidateDbSettings(sTargetDbName, sSourceDbName) Then Return
        Dim sCode As String = GetSelectedCompanyCode() : Dim sName As String = GetSelectedCompanyName()
        Dim iCount As Integer = TabMigrationHelper.GetCount(dbHelper, "T_ORDER_DETAIL_GANGJWA_INFO", String.Format("F_COMPANY_CODE = '{0}'", sCode), AddressOf Log)
        If Not MigrationUtils.AskMigrationConfirmation(iCount, sName, "공간시설") Then Return
        TabMigrationHelper.ExecuteSPMigration(dbHelper, "USP_MIG_OLD_TO_NEW_SPACE_FACILITY.sql", "USP_MIG_OLD_TO_NEW_SPACE_FACILITY", sCode, sSourceDbName, "공간시설", loadingBar, AddressOf SearchSpaceTarget, AddressOf Log)
    End Sub

    Private Sub btnInitTargetSpace_Click(sender As Object, e As EventArgs) Handles btnInitTargetSpace.Click
        If dbHelper Is Nothing OrElse cboCompany.SelectedIndex < 0 Then Return
        If Not MigrationUtils.AskDeleteConfirmation(GetSelectedCompanyName(), "공간시설") Then Return
        TabMigrationHelper.ExecuteSPReset(dbHelper, "USP_RESET_SPACE_FACILITY_MIGRATION.sql", "USP_RESET_SPACE_FACILITY_MIGRATION", GetSelectedCompanyCode(), "공간시설", loadingBar, AddressOf SearchSpaceTarget, AddressOf Log)
    End Sub

End Class
