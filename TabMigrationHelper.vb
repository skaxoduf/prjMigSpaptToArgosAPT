Imports System.Text

' 탭별 이관 관련 공통 로직 모듈
' 강좌/사물함/상품/일반시설/숙박시설/공간시설 탭에서 반복되던 코드를 통합
Public Module TabMigrationHelper

    ' 탭별 페이징 상태를 하나의 객체로 관리 (페이징 스칼라 변수 12개를 대체)
    Public Class TabPageState
        Public iCurrentSourcePage As Integer = 1
        Public iCurrentTargetPage As Integer = 1
        Public iSourcePageSize As Integer = 100
        Public iTargetPageSize As Integer = 100
    End Class

    ' T_TrsInout LEFT JOIN T_Member 패턴 소스 조회
    ' 강좌/상품/일반시설/숙박시설/공간시설 탭의 공통 소스 조회 로직
    ' sExtraCondition: 강좌 탭의 "AND A.rbcode = '0001'" 같이 탭별 추가 조건 (생략 가능)
    Public Sub SearchSourceWithTrsInout(
            sourceHelper As DBHelper,
            dgv As DataGridView,
            grp As GroupBox,
            state As TabPageState,
            pnlPagination As Panel,
            pageCallback As EventHandler,
            dtpStart As DateTimePicker,
            dtpEnd As DateTimePicker,
            txtName As TextBox,
            cboDong As ComboBox,
            cboHo As ComboBox,
            sGroupLabel As String,
            logAction As Action(Of String),
            Optional sExtraCondition As String = "")

        If sourceHelper Is Nothing Then Return
        Try
            Dim sStart As String = dtpStart.Value.ToString("yyyy-MM-dd")
            Dim sEnd As String = dtpEnd.Value.ToString("yyyy-MM-dd")
            Dim sName As String = txtName.Text.Trim()
            Dim sDong As String = If(cboDong.SelectedItem IsNot Nothing, cboDong.SelectedItem.ToString(), "전체")
            Dim sHo As String = If(cboHo.SelectedItem IsNot Nothing, cboHo.SelectedItem.ToString(), "전체")

            Dim sbWhere As New StringBuilder(String.Format("A.TrsDate BETWEEN '{0}' AND '{1}'", sStart, sEnd))
            If Not String.IsNullOrEmpty(sExtraCondition) Then sbWhere.Append(" " & sExtraCondition)
            If Not String.IsNullOrEmpty(sName) Then sbWhere.AppendFormat(" AND B.MbName LIKE '%{0}%'", sName)
            If sDong <> "전체" Then sbWhere.AppendFormat(" AND B.DongAddr = '{0}'", sDong)
            If sHo <> "전체" Then sbWhere.AppendFormat(" AND B.HoAddr = '{0}'", sHo)

            Dim sFrom As String = "T_TrsInout A LEFT JOIN T_Member B ON A.MbNo = B.MbNo"
            Dim iTotal As Integer = GetCount(sourceHelper, sFrom, sbWhere.ToString(), logAction)
            Dim iOffset As Integer = (state.iCurrentSourcePage - 1) * state.iSourcePageSize
            Dim sQuery As String = String.Format(
                "SELECT A.*, B.DongAddr, B.HoAddr, B.MbName FROM {0} WHERE {1} " &
                "ORDER BY A.TrsDate DESC, A.TrsNo DESC OFFSET {2} ROWS FETCH NEXT {3} ROWS ONLY",
                sFrom, sbWhere.ToString(), iOffset, state.iSourcePageSize)

            dgv.DataSource = sourceHelper.ExecuteQuery(sQuery)
            grp.Text = String.Format("{0} : 총 {1}건 (Page {2})", sGroupLabel, iTotal.ToString("N0"), state.iCurrentSourcePage)
            PaginationHelper.RenderPagination(pnlPagination, iTotal, state.iSourcePageSize, state.iCurrentSourcePage, pageCallback)
        Catch ex As Exception
            logAction("소스 조회 실패: " & ex.Message)
        End Try
    End Sub

    ' T_Member 직접 조회 패턴 (회원/사물함 탭 소스 공통)
    ' 날짜 범위 없이 이름/동/호만으로 T_Member를 조회
    Public Sub SearchSourceMember(
            sourceHelper As DBHelper,
            dgv As DataGridView,
            grp As GroupBox,
            state As TabPageState,
            pnlPagination As Panel,
            pageCallback As EventHandler,
            txtName As TextBox,
            cboDong As ComboBox,
            cboHo As ComboBox,
            sGroupLabel As String,
            logAction As Action(Of String))

        If sourceHelper Is Nothing Then Return
        Try
            Dim sName As String = txtName.Text.Trim()
            Dim sDong As String = If(cboDong.SelectedItem IsNot Nothing, cboDong.SelectedItem.ToString(), "전체")
            Dim sHo As String = If(cboHo.SelectedItem IsNot Nothing, cboHo.SelectedItem.ToString(), "전체")

            Dim sbWhere As New StringBuilder("1=1")
            If Not String.IsNullOrEmpty(sName) Then sbWhere.AppendFormat(" AND MbName LIKE '%{0}%'", sName)
            If sDong <> "전체" Then sbWhere.AppendFormat(" AND DongAddr = '{0}'", sDong)
            If sHo <> "전체" Then sbWhere.AppendFormat(" AND HoAddr = '{0}'", sHo)

            Dim iTotal As Integer = GetCount(sourceHelper, "T_Member", sbWhere.ToString(), logAction)
            Dim iOffset As Integer = (state.iCurrentSourcePage - 1) * state.iSourcePageSize
            Dim sQuery As String = String.Format(
                "SELECT * FROM T_Member WHERE {0} ORDER BY MbNo OFFSET {1} ROWS FETCH NEXT {2} ROWS ONLY",
                sbWhere.ToString(), iOffset, state.iSourcePageSize)

            Dim dt As DataTable = sourceHelper.ExecuteQuery(sQuery)
            dgv.DataSource = dt
            grp.Text = String.Format("{0} : Top {1} / Total {2}", sGroupLabel, dt.Rows.Count, iTotal)
            PaginationHelper.RenderPagination(pnlPagination, iTotal, state.iSourcePageSize, state.iCurrentSourcePage, pageCallback)
        Catch ex As Exception
            logAction("소스 조회 실패: " & ex.Message)
        End Try
    End Sub

    ' 타겟 DB 조회 공통 처리 (회사코드 + 이름 검색 + 페이징)
    ' 강좌/사물함/상품/일반시설/숙박시설/공간시설 탭의 Target 조회에 사용
    Public Sub SearchTargetCommon(
            dbHelper As DBHelper,
            dgv As DataGridView,
            grp As GroupBox,
            state As TabPageState,
            pnlPagination As Panel,
            pageCallback As EventHandler,
            sCompanyCode As String,
            txtName As TextBox,
            sTableName As String,
            sNameCol As String,
            sGroupLabel As String,
            logAction As Action(Of String))

        If dbHelper Is Nothing Then Return
        Try
            Dim sName As String = txtName.Text.Trim()
            Dim sWhere As String = String.Format("F_COMPANY_CODE = '{0}'", sCompanyCode)
            If Not String.IsNullOrEmpty(sName) Then sWhere &= String.Format(" AND {0} LIKE '%{1}%'", sNameCol, sName)

            Dim iTotal As Integer = GetCount(dbHelper, sTableName, sWhere, logAction)
            Dim iOffset As Integer = (state.iCurrentTargetPage - 1) * state.iTargetPageSize
            Dim sQuery As String = String.Format(
                "SELECT * FROM {0} WHERE {1} ORDER BY F_IDX DESC OFFSET {2} ROWS FETCH NEXT {3} ROWS ONLY",
                sTableName, sWhere, iOffset, state.iTargetPageSize)

            dgv.DataSource = dbHelper.ExecuteQuery(sQuery)
            grp.Text = String.Format("{0} : 총 {1}건 (Page {2})", sGroupLabel, iTotal.ToString("N0"), state.iCurrentTargetPage)
            PaginationHelper.RenderPagination(pnlPagination, iTotal, state.iTargetPageSize, state.iCurrentTargetPage, pageCallback)
        Catch ex As Exception
            logAction("타겟 조회 실패: " & ex.Message)
        End Try
    End Sub

    ' SP 이관 실행 공통 (DeploySpList → EXEC SP → 결과 로그 → 조회 콜백)
    Public Sub ExecuteSPMigration(
            dbHelper As DBHelper,
            sSpFileName As String,
            sSpName As String,
            sCompanyCode As String,
            sSourceDbName As String,
            sMigType As String,
            loadingBar As CircularProgressBar,
            refreshCallback As Action,
            logAction As Action(Of String))

        logAction(String.Format("=== {0} 이관 시작 ===", sMigType))
        If loadingBar IsNot Nothing Then loadingBar.Visible = True
        Try
            If Not MigrationUtils.DeploySpList(dbHelper, {sSpFileName}, logAction) Then Return

            logAction(String.Format(">>> {0} 이관 프로시저 실행 중 (시간이 소요될 수 있습니다)...", sMigType))
            Dim result As DataTable = dbHelper.ExecuteSqlWithResultCheck(
                String.Format("EXEC {0} @P_CompanyCode='{1}', @OldDbName='{2}'", sSpName, sCompanyCode, sSourceDbName))

            If result IsNot Nothing AndAlso result.Rows.Count > 0 Then
                logAction("실행 결과: " & result.Rows(0)(0).ToString())
            End If
            logAction(String.Format("=== {0} 이관 완료 ===", sMigType))
            refreshCallback()
        Catch ex As Exception
            logAction(String.Format("{0} 이관 중 오류: {1}", sMigType, ex.Message))
        Finally
            If loadingBar IsNot Nothing Then loadingBar.Visible = False
        End Try
    End Sub

    ' SP 초기화 실행 공통 (DeploySpList → EXEC 초기화SP → 결과 로그 → 조회 콜백)
    Public Sub ExecuteSPReset(
            dbHelper As DBHelper,
            sSpFileName As String,
            sSpName As String,
            sCompanyCode As String,
            sMigType As String,
            loadingBar As CircularProgressBar,
            refreshCallback As Action,
            logAction As Action(Of String))

        logAction(String.Format("=== {0} 데이터 초기화 시작 ===", sMigType))
        If loadingBar IsNot Nothing Then loadingBar.Visible = True
        Try
            If Not MigrationUtils.DeploySpList(dbHelper, {sSpFileName}, logAction) Then Return

            Dim result As DataTable = dbHelper.ExecuteSqlWithResultCheck(
                String.Format("EXEC {0} @P_CompanyCode='{1}'", sSpName, sCompanyCode))

            If result IsNot Nothing AndAlso result.Rows.Count > 0 Then
                Dim sRes As String = If(result.Columns.Contains("Result"), result.Rows(0)("Result").ToString(), result.Rows(0)(0).ToString())
                Dim sMsg As String = If(result.Columns.Contains("Msg") AndAlso result.Rows(0)("Msg") IsNot DBNull.Value, result.Rows(0)("Msg").ToString(), "")
                logAction(String.Format("결과: {0} ({1})", sRes, sMsg))
            End If
            logAction(String.Format("=== {0} 데이터 초기화 완료 ===", sMigType))
            refreshCallback()
        Catch ex As Exception
            logAction(String.Format("{0} 초기화 중 오류: {1}", sMigType, ex.Message))
        Finally
            If loadingBar IsNot Nothing Then loadingBar.Visible = False
        End Try
    End Sub

    ' FROM 절(테이블명 또는 JOIN 구문)과 WHERE 절로 카운트를 조회하는 공통 함수
    Public Function GetCount(dbHelper As DBHelper, sFromClause As String, sWhere As String, logAction As Action(Of String)) As Integer
        Try
            Dim dt As DataTable = dbHelper.ExecuteQuery(String.Format("SELECT COUNT(*) FROM {0} WHERE {1}", sFromClause, sWhere))
            If dt.Rows.Count > 0 Then Return Convert.ToInt32(dt.Rows(0)(0))
        Catch ex As Exception
            logAction("카운트 조회 오류: " & ex.Message)
        End Try
        Return 0
    End Function

End Module
