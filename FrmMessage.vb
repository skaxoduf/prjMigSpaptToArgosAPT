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
        SetRoundedRegion()
    End Sub

    Private Sub SetRoundedRegion()
        Dim radius As Integer = 25
        Dim path As New GraphicsPath()
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

        Dim radius As Integer = 25
        Dim rect As New Rectangle(0, 0, Me.Width - 1, Me.Height - 1)
        Dim path As New GraphicsPath()

        path.StartFigure()
        path.AddArc(rect.X, rect.Y, radius, radius, 180, 90)
        path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90)
        path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90)
        path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90)
        path.CloseFigure()

        ' Accent Color Border
        Using pen As New Pen(Color.FromArgb(122, 162, 247), 1.5F)
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

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    ' 정적 헬퍼 함수
    Public Shared Function ShowMsg(text As String, title As String, Optional buttons As MessageBoxButtons = MessageBoxButtons.OK) As DialogResult
        Using frm As New FrmMessage()
            ' 제목은 표시할 곳이 없으므로 생략하거나, 필요하면 상단에 별도 Label 추가 가능
            ' 현재 디자인에서는 본문만 깔끔하게 보여줌
            frm.lblMessage.Text = text
            
            ' 버튼 설정
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
