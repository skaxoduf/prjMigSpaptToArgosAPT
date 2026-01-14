Imports System.Data.SqlClient
Imports System.Text

Public Class DBHelper
    Private _connectionString As String

    Public Sub New(ByVal serverIp As String, ByVal dbName As String, ByVal userId As String, ByVal password As String)
        _connectionString = String.Format("Server={0};Database={1};User Id={2};Password={3};", serverIp, dbName, userId, password)
    End Sub

    ''' <summary>
    ''' 저장 프로시저를 실행하고 결과 메시지를 반환합니다.
    ''' </summary>
    Public Function ExecuteMigrationSP(spName As String, oldDbName As String, companyIdx As Integer, companyCode As String) As String
        Dim sbResult As New StringBuilder()
        
        Using conn As New SqlConnection(_connectionString)
            Try
                conn.Open()
                
                Using cmd As New SqlCommand(spName, conn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandTimeout = 600 ' 대량 이관 대비 타임아웃 10분 설정
                    
                    ' 파라미터 설정
                    cmd.Parameters.AddWithValue("@OldDbName", oldDbName)
                    cmd.Parameters.AddWithValue("@CompanyIdx", companyIdx)
                    cmd.Parameters.AddWithValue("@CompanyCode", companyCode)
                    
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        Dim resultFound As Boolean = False

                        ' 결과셋 순회 (중간에 빈 결과셋이나 영향받은 행 개수 메시지 등이 있을 수 있음)
                        Do
                            Dim hasResultCol As Boolean = False
                            If reader.FieldCount > 0 Then
                                For i As Integer = 0 To reader.FieldCount - 1
                                    If reader.GetName(i).Equals("Result", StringComparison.OrdinalIgnoreCase) Then
                                        hasResultCol = True
                                        Exit For
                                    End If
                                Next
                            End If

                            If hasResultCol Then
                                If reader.Read() Then
                                    ' 프로시저에서 SELECT 'SUCCESS', Count, Msg 등을 리턴한다고 가정
                                    Dim resultStatus As String = reader("Result").ToString()
                                    Dim count As Integer = Convert.ToInt32(reader("MigratedCount"))
                                    Dim msg As String = If(reader.FieldCount > 2, reader("Message").ToString(), "")

                                    sbResult.AppendLine(String.Format("[{0}] 건수: {1}, 메시지: {2}", resultStatus, count, msg))
                                    resultFound = True
                                End If
                                Exit Do ' 원하는 결과를 찾았으므로 루프 종료
                            End If
                        Loop While reader.NextResult()

                        If Not resultFound Then
                            sbResult.AppendLine("Alert: 예상된 결과셋(Result 컬럼)을 찾지 못했습니다.")
                        End If
                    End Using
                End Using
                
            Catch ex As Exception
                sbResult.AppendLine("Error: " & ex.Message)
            End Try
        End Using
        
        Return sbResult.ToString()
    End Function

    ''' <summary>
    ''' 쿼리를 실행하고 여러 결과셋 중 'Result' 컬럼이 있는 테이블을 찾아 반환합니다.
    ''' (트리거 등에 의해 원치 않는 결과셋이 먼저 반환되는 경우를 방지)
    ''' </summary>
    Public Function ExecuteSqlWithResultCheck(sql As String) As DataTable
        Dim resultDt As New DataTable()
        
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            Using cmd As New SqlCommand(sql, conn)
                 Using reader As SqlDataReader = cmd.ExecuteReader()
                    Do
                        ' 컬럼 확인
                        Dim hasResultCol As Boolean = False
                        If reader.FieldCount > 0 Then
                            ' 스키마 로드
                            Dim schemaTable As DataTable = reader.GetSchemaTable()
                            For Each row As DataRow In schemaTable.Rows
                                If row("ColumnName").ToString().Equals("Result", StringComparison.OrdinalIgnoreCase) Then
                                    hasResultCol = True
                                    Exit For
                                End If
                            Next
                        End If
                        
                        If hasResultCol Then
                            ' 'Result' 컬럼이 있는 결과셋을 찾았음. DataTable로 로드.
                            resultDt.Load(reader)
                            Return resultDt
                        End If
                        
                    Loop While reader.NextResult()
                 End Using
            End Using
        End Using
        
        Return Nothing ' 못 찾음
    End Function

    ''' <summary>
    ''' 단순 접속 테스트
    ''' </summary>
    Public Function TestConnection() As Boolean
        Using conn As New SqlConnection(_connectionString)
            Try
                conn.Open()
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Using
    End Function

    ''' <summary>
    ''' 임의의 쿼리를 실행하여 DataTable을 반환합니다. (Data Viewer용)
    ''' </summary>
    Public Function ExecuteQuery(query As String) As DataTable
        Dim dt As New DataTable()
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            Using cmd As New SqlCommand(query, conn)
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using
        Return dt
    End Function

    ''' <summary>
    ''' T_COMPANY 테이블에서 사용 중인 업체 목록과 회원 수를 조회합니다.
    ''' </summary>
    Public Function GetCompanyList() As DataTable
        Dim dt As New DataTable()
        Dim query As String = "SELECT C.F_IDX, C.F_COMPANY_CODE, C.F_COMPANY_NAME, " &
                              "(SELECT COUNT(*) FROM T_MEM M WHERE M.F_COMPANY_IDX = C.F_IDX) AS F_MEM_COUNT " &
                              "FROM T_COMPANY C WHERE C.F_USE_YN = '1' ORDER BY C.F_COMPANY_NAME"

        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            Using cmd As New SqlCommand(query, conn)
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using
        Return dt
    End Function
    ''' <summary>
    ''' SQL 파일을 읽어와서 저장 프로시저를 배포합니다. (GO 구문 분할 실행)
    ''' </summary>
    Public Function DeployStoredProcedure(filePath As String) As String
        Dim sbResult As New StringBuilder()

        If Not System.IO.File.Exists(filePath) Then
            Return "Error: 파일을 찾을 수 없습니다 - " & filePath
        End If

        Try
            Dim script As String = System.IO.File.ReadAllText(filePath, Encoding.Default) ' 한글 주석 등 고려

            ' GO 구문으로 분할 (대소문자 무시, 라인 단위)
            Dim commands As String() = System.Text.RegularExpressions.Regex.Split(script, "^\s*GO\s*$", System.Text.RegularExpressions.RegexOptions.Multiline Or System.Text.RegularExpressions.RegexOptions.IgnoreCase)

            Using conn As New SqlConnection(_connectionString)
                conn.Open()

                For Each cmdText As String In commands
                    If String.IsNullOrWhiteSpace(cmdText) Then Continue For

                    ' 2026-01-14 암호화 배포 (WITH ENCRYPTION)
                    ' CREATE/ALTER PROCEDURE 구문이 있고, 아직 암호화 옵션이 없다면 추가
                    If (cmdText.IndexOf("CREATE PROCEDURE", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
                        cmdText.IndexOf("ALTER PROCEDURE", StringComparison.OrdinalIgnoreCase) >= 0) AndAlso
                        cmdText.IndexOf("WITH ENCRYPTION", StringComparison.OrdinalIgnoreCase) < 0 Then

                        ' AS ~ BEGIN 패턴을 찾아서 WITH ENCRYPTION 삽입
                        ' 파라미터 등에 AS가 나올 수 있으므로 주의가 필요하나, 
                        ' 통상적인 SP 포맷(AS 줄바꿈 BEGIN)을 가정하여 처리
                        Dim pattern As String = "\bAS\s+BEGIN\b"
                        ' 정규식 옵션: 대소문자 무시
                        cmdText = System.Text.RegularExpressions.Regex.Replace(cmdText, pattern,
                                      "WITH ENCRYPTION AS" & Environment.NewLine & "BEGIN",
                                      System.Text.RegularExpressions.RegexOptions.IgnoreCase)
                    End If

                    Try
                        ' USE 구문은 무시하거나 에러가 날 수 있으므로, 현재 연결된 DB를 사용하도록 유도
                        ' 하지만 스크립트 내에 USE [DB]가 있으면 실행하려 할 것임.
                        ' 연결 스트링의 DB가 우선이므로 USE 구문이 있어도 권한만 있다면 넘어가거나, 
                        ' 만약 다른 DB를 지정하면 문제가 될 수 있음.
                        ' 여기서는 있는 그대로 실행하되, 에러 발생 시 로그 남김.

                        Using cmd As New SqlCommand(cmdText, conn)
                            cmd.ExecuteNonQuery()
                        End Using
                    Catch ex As SqlException
                        ' USE 구문 에러는 무시 (이미 접속된 DB 사용)
                        If ex.Message.Contains("USE") Then
                            sbResult.AppendLine("Alert: USE statements ignored or failed (Proceeding).")
                        Else
                            Throw ex
                        End If
                    End Try
                Next
            End Using

            sbResult.AppendLine("Success: " & System.IO.Path.GetFileName(filePath) & " 배포 완료.")

        Catch ex As Exception
            sbResult.AppendLine("Error (" & System.IO.Path.GetFileName(filePath) & "): " & ex.Message)
        End Try

        Return sbResult.ToString()
    End Function
End Class
