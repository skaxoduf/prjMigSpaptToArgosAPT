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
                        If reader.Read() Then
                            ' 프로시저에서 SELECT 'SUCCESS', Count, Msg 등을 리턴한다고 가정
                            Dim resultStatus As String = reader("Result").ToString()
                            Dim count As Integer = Convert.ToInt32(reader("MigratedCount"))
                            Dim msg As String = reader("Message").ToString()
                            
                            sbResult.AppendLine(String.Format("[{0}] 건수: {1}, 메시지: {2}", resultStatus, count, msg))
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
    ''' T_COMPANY 테이블에서 사용 중인 회사 목록과 회원 수를 조회합니다.
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
End Class
