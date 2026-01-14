Imports System.Windows.Forms
Imports System.Drawing.Drawing2D
Imports System.Runtime.InteropServices

Public Class FrmMessage
    ' 창 이동을 위한 API
    <DllImport("user32.dll")>
    Public Shared Function ReleaseCapture() As Boolean
    End Function
    <DllImport("user32.dll")>
    Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    End Function

    Private Sub FrmMessage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.None
        ' 테두리가 Panel에 가려지지 않도록 패딩 추가
        Me.Padding = New Padding(2)
        
        SetRoundedRegion()
        ' 2026-01-12 10:10:00 모던 메시지박스에 테마 적용
        ThemeManager.ApplyTheme(Me)
        
        ' 2026-01-13 Fix Color Mismatch (Make Panel transparent-like by matching Form BackColor)
        Panel1.BackColor = Me.BackColor
    End Sub

    Private Sub SetRoundedRegion()
        Dim radius As Integer = 12 ' 25 -> 12 좀 더 깔끔하게 축소
        Dim path As New GraphicsPath()
        ' Region은 폼 전체 크기 기준
        Dim rect As New Rectangle(0, 0, Me.Width, Me.Height)

        path.StartFigure()
        path.AddArc(rect.X, rect.Y, radius, radius, 180, 90)
        path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90)
        path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90)
        path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90)
        path.CloseFigure()

        Me.Region = New Region(path)
    End Sub

    ' 테두리 그리기
    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        Dim g As Graphics = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias

        Dim radius As Integer = 12 ' Radius 일치
        ' Pen 두께(2)를 고려하여 Rect 조정 (안쪽으로 1px 들어와야 짤리지 않음)
        Dim rect As New Rectangle(1, 1, Me.Width - 2, Me.Height - 2)
        Dim path As New GraphicsPath()

        path.StartFigure()
        path.AddArc(rect.X, rect.Y, radius, radius, 180, 90)
        path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90)
        path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90)
        path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90)
        path.CloseFigure()

        ' Accent Color Border (Theme Aware)
        Dim borderCol As Color = Color.FromArgb(122, 162, 247)
        If ThemeManager.CurrentPalette IsNot Nothing Then
            borderCol = ThemeManager.CurrentPalette.Accent
        End If

         Using pen As New Pen(borderCol, 2.0F)
            g.DrawPath(pen, path)
        End Using
    End Sub

    ' 창 이동 (제목 표시줄이 없으므로 내용 클릭해서 이동)
    Private Sub DragForm(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown, Panel1.MouseDown, lblMessage.MouseDown
        If e.Button = MouseButtons.Left Then
            ReleaseCapture()
            SendMessage(Me.Handle, &HA1, 2, 0)
        End If
    End Sub

    Private _mode As MessageBoxButtons = MessageBoxButtons.OK

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        If _mode = MessageBoxButtons.YesNo Then
            Me.DialogResult = DialogResult.Yes
        Else
            Me.DialogResult = DialogResult.OK
        End If
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If _mode = MessageBoxButtons.YesNo Then
            Me.DialogResult = DialogResult.No
        Else
            Me.DialogResult = DialogResult.Cancel
        End If
        Me.Close()
    End Sub

    ' 정적 헬퍼 함수
    Public Shared Function ShowMsg(text As String, title As String, Optional buttons As MessageBoxButtons = MessageBoxButtons.OK) As DialogResult
        Using frm As New FrmMessage()
            frm._mode = buttons
            
            ' 1. 텍스트 설정
            frm.lblMessage.Text = text
            
            ' 2. 텍스트 크기 측정 및 폼 크기 자동 조절 (2026-01-13 Fix Truncated Text)
            ' 여유 폭을 고려하여 측정 (Padding 등 고려)
            Dim maxSize As New Size(frm.lblMessage.Width, Integer.MaxValue)
            Dim textSize As Size = TextRenderer.MeasureText(text, frm.lblMessage.Font, maxSize, TextFormatFlags.WordBreak)
            
            ' 기본 높이(80)보다 텍스트가 길면 그만큼 폼 높이를 늘림
            Dim defaultLabelHeight As Integer = 80
            Dim extraHeight As Integer = 0
            
            ' 텍스트 높이가 기본 높이보다 크면 확장이 필요함 (+여유분 20px)
            If textSize.Height > defaultLabelHeight Then
                extraHeight = textSize.Height - defaultLabelHeight + 20
            End If
            
            If extraHeight > 0 Then
                frm.Height += extraHeight
                frm.Panel1.Height += extraHeight ' 패널도 같이 늘려야 함 (Label이 Dock.Fill이므로)
                ' 모양(Region) 재설정 (크기가 변했으므로)
                frm.SetRoundedRegion()
            End If
            
            ' 3. 버튼 설정
            If buttons = MessageBoxButtons.OK Then
                frm.btnCancel.Visible = False
                frm.btnOK.Location = New System.Drawing.Point((frm.Width - frm.btnOK.Width) / 2, frm.btnOK.Top) ' 중앙 정렬
                frm.btnOK.Text = "확인"
            ElseIf buttons = MessageBoxButtons.YesNo Then
                frm.btnOK.Text = "예"
                frm.btnCancel.Text = "아니오"
            End If
            
            Return frm.ShowDialog()
        End Using
    End Function
End Class
