Imports System.Drawing

''' <summary>
''' 페이지네이션 UI 렌더링 헬퍼 클래스 (2026-01-18 09:10:00 코드 시작)
''' </summary>
Public Class PaginationHelper

    ''' <summary>
    ''' 페이지네이션 버튼을 패널에 렌더링합니다.
    ''' </summary>
    ''' <param name="panel">버튼을 추가할 패널</param>
    ''' <param name="totalRows">총 데이터 수</param>
    ''' <param name="pageSize">페이지 당 데이터 수</param>
    ''' <param name="currentPage">현재 페이지 (1부터 시작)</param>
    ''' <param name="clickHandler">버튼 클릭 이벤트 핸들러</param>
    Public Shared Sub RenderPagination(panel As Panel, totalRows As Integer, pageSize As Integer, currentPage As Integer, clickHandler As EventHandler)
        panel.Controls.Clear()

        Dim totalPages As Integer = Math.Ceiling(totalRows / pageSize)
        If totalPages = 0 Then totalPages = 1

        Dim startPage As Integer = ((currentPage - 1) \ 10) * 10 + 1
        Dim endPage As Integer = Math.Min(startPage + 9, totalPages)

        ' 2026-01-18 Tokyo Night Colors (Explicit)
        Dim accentColor As Color = Color.FromArgb(122, 162, 247) ' #7aa2f7 (Blue)
        Dim activeTxtColor As Color = Color.White
        Dim normalTxtColor As Color = Color.FromArgb(169, 177, 214) ' #a9b1d6 (Light Gray)
        Dim hoverColor As Color = Color.FromArgb(65, 72, 104) ' #414868


        ' [<] Prev Block Button
        If startPage > 1 Then
            Dim btn As New ModernButton()
            btn.Text = "<"
            btn.Size = New Size(30, 30)
            btn.Tag = startPage - 1
            btn.BorderRadius = 5
            btn.CustomBaseColor = accentColor
            AddHandler btn.Click, clickHandler
            panel.Controls.Add(btn)
        End If

        ' Numbered Buttons
        For i As Integer = startPage To endPage
            Dim btn As New ModernButton()
            btn.Text = i.ToString()
            btn.Size = New Size(40, 30)
            btn.Tag = i
            btn.BorderRadius = 5

            If i = currentPage Then
                btn.ForeColor = activeTxtColor
                btn.CustomBaseColor = accentColor
                btn.Font = New Font("Segoe UI", 10, FontStyle.Bold)
            Else
                btn.ForeColor = normalTxtColor
                btn.CustomBaseColor = Color.Transparent
                btn.CustomHoverColor = hoverColor
                btn.Font = New Font("Segoe UI", 10, FontStyle.Regular)
            End If

            AddHandler btn.Click, clickHandler
            panel.Controls.Add(btn)
        Next

        ' [>] Next Block Button
        If endPage < totalPages Then
            Dim btn As New ModernButton()
            btn.Text = ">"
            btn.Size = New Size(30, 30)
            btn.Tag = endPage + 1
            btn.BorderRadius = 5
            btn.CustomBaseColor = accentColor
            AddHandler btn.Click, clickHandler
            panel.Controls.Add(btn)
        End If
    End Sub

End Class
' (2026-01-18 09:10:00 코드 끝)
