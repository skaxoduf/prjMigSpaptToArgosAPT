Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.IO

''' <summary>
''' UI 관련 유틸리티 헬퍼 클래스 (2026-01-18 09:15:00 코드 시작)
''' </summary>
Public Class UiHelper

    ''' <summary>
    ''' 바이트 배열에서 이미지를 생성하고 텍스트 오버레이를 그립니다.
    ''' </summary>
    ''' <param name="originalBytes">이미지 바이트 배열</param>
    ''' <param name="name">회원명</param>
    ''' <param name="no">회원번호</param>
    ''' <returns>생성된 비트맵 (실패 시 Nothing)</returns>
    Public Shared Function CreatePreviewImage(originalBytes As Byte(), name As String, no As String) As Bitmap
        If originalBytes Is Nothing OrElse originalBytes.Length = 0 Then Return Nothing

        Try
            Using ms As New MemoryStream(originalBytes)
                Dim originalImage As Image = Image.FromStream(ms)
                Dim bmp As New Bitmap(originalImage)

                Using g As Graphics = Graphics.FromImage(bmp)
                    g.SmoothingMode = SmoothingMode.AntiAlias
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit

                    Dim nameFont As New Font("Segoe UI", 15, FontStyle.Bold)
                    Dim noFont As New Font("Segoe UI", 12.5F, FontStyle.Bold)

                    Dim nameSize As SizeF = g.MeasureString(name, nameFont)
                    Dim noSize As SizeF = g.MeasureString("No. " & no, noFont)

                    Dim padding As Integer = 8
                    Dim lineSpacing As Integer = 2
                    Dim totalTextHeight As Integer = CInt(nameSize.Height + lineSpacing + noSize.Height)
                    Dim boxHeight As Integer = totalTextHeight + (padding * 2)

                    ' 텍스트 배경 영역 (반투명 네이비)
                    Dim rect As New Rectangle(0, bmp.Height - boxHeight, bmp.Width, boxHeight)
                    Using brushBg As New SolidBrush(Color.FromArgb(220, 20, 20, 25))
                        g.FillRectangle(brushBg, rect)
                    End Using

                    ' 텍스트 그리기 좌표
                    Dim namePos As New PointF(padding, rect.Y + padding)
                    Dim noPos As New PointF(padding, namePos.Y + nameSize.Height + lineSpacing)

                    ' 그림자 효과
                    g.DrawString(name, nameFont, Brushes.Black, namePos.X + 1, namePos.Y + 1)
                    g.DrawString("No. " & no, noFont, Brushes.Black, noPos.X + 1, noPos.Y + 1)

                    ' 실제 텍스트
                    g.DrawString(name, nameFont, Brushes.White, namePos)
                    g.DrawString("No. " & no, noFont, Brushes.White, noPos)
                End Using

                Return bmp
            End Using
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' DataGridView에 Tokyo Night 테마를 적용합니다.
    ''' </summary>
    ''' <param name="dgv">대상 DataGridView</param>
    Public Shared Sub ApplyGridTheme(dgv As DataGridView)
        If dgv Is Nothing Then Return

        dgv.EnableHeadersVisualStyles = False
        dgv.BorderStyle = BorderStyle.None
        dgv.BackgroundColor = Color.FromArgb(26, 27, 38) ' #1a1b26 (Main Bg)
        dgv.GridColor = Color.FromArgb(41, 46, 66) ' #292e42 (Subtle Border)
        dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal ' Remove vertical lines

        ' Header Style
        dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(22, 22, 30) ' #16161e (Darker)
        dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(192, 202, 245) ' #c0caf5 (Text)
        dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(22, 22, 30)
        dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(192, 202, 245)
        dgv.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        dgv.ColumnHeadersHeight = 45 ' Increased Header Height
        dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ' Center Header

        ' Row Style
        dgv.DefaultCellStyle.BackColor = Color.FromArgb(26, 27, 38)
        dgv.DefaultCellStyle.ForeColor = Color.FromArgb(169, 177, 214) ' #a9b1d6
        dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(61, 89, 161) ' #3d59a1 (Muted Blue)
        dgv.DefaultCellStyle.SelectionForeColor = Color.White
        dgv.DefaultCellStyle.Font = New Font("Segoe UI", 10.0F) ' Increased Font Size
        dgv.DefaultCellStyle.Padding = New Padding(5, 0, 5, 0) ' Add Padding
        
        ' Alternating Row (Slightly lighter)
        dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(30, 32, 48) ' #1e2030
        
        dgv.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(26, 27, 38)
        dgv.RowHeadersDefaultCellStyle.ForeColor = Color.FromArgb(86, 95, 137)
        dgv.RowHeadersWidth = 35
        dgv.RowTemplate.Height = 40 ' Increased Row Height
    End Sub

End Class
' (2026-01-18 09:15:00 코드 끝)
