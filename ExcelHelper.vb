Imports System.Data.OleDb
Imports System.IO

''' <summary>
''' 엑셀 파일 처리 헬퍼 클래스 (2026-01-18 09:05:00 코드 시작)
''' </summary>
Public Class ExcelHelper

    ''' <summary>
    ''' 엑셀 파일을 읽어 DataTable로 반환합니다. (빈 행 제거 로직 포함)
    ''' </summary>
    ''' <param name="filePath">엑셀 파일 경로</param>
    ''' <returns>DataTable (실패 시 Nothing)</returns>
    Public Shared Function LoadExcelToDataTable(filePath As String, logAction As Action(Of String)) As DataTable
        Dim dtResult As New DataTable()
        Dim connStr As String = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES"";", filePath)

        Try
            Using conn As New OleDbConnection(connStr)
                conn.Open()
                Dim dtSchema As DataTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
                Dim sheetName As String = ""
                If dtSchema.Rows.Count > 0 Then
                    sheetName = dtSchema.Rows(0)("TABLE_NAME").ToString()
                Else
                    logAction("오류: 엑셀 시트를 찾을 수 없습니다.")
                    Return Nothing
                End If

                Dim query As String = String.Format("SELECT * FROM [{0}]", sheetName)
                Using cmd As New OleDbCommand(query, conn)
                    Using da As New OleDbDataAdapter(cmd)
                        da.Fill(dtResult)

                        ' 빈 행 제거 로직 (동/호 없는 데이터 숨김)
                        If dtResult.Columns.Contains("동") AndAlso dtResult.Columns.Contains("호") Then
                            For i As Integer = dtResult.Rows.Count - 1 To 0 Step -1
                                Dim row As DataRow = dtResult.Rows(i)
                                Dim dong As String = If(row("동") Is DBNull.Value, "", row("동").ToString().Trim())
                                Dim ho As String = If(row("호") Is DBNull.Value, "", row("호").ToString().Trim())

                                ' 동 또는 호가 비어있으면 해당 행 제거
                                If String.IsNullOrEmpty(dong) OrElse String.IsNullOrEmpty(ho) Then
                                    dtResult.Rows.RemoveAt(i)
                                End If
                            Next
                        End If
                    End Using
                End Using
            End Using
            
            Return dtResult

        Catch ex As Exception
            logAction("엑셀 로드 중 오류: " & ex.Message)
            If ex.Message.Contains("registered") Then
                FrmMessage.ShowMsg("Microsoft.ACE.OLEDB Provider가 설치되어 있지 않습니다." & vbCrLf & "Microsoft Access Database Engine을 설치해주세요.", "오류")
            End If
            Return Nothing
        End Try
    End Function

End Class
' (2026-01-18 09:05:00 코드 끝)
