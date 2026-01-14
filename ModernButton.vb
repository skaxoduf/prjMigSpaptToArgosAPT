Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Public Class ModernButton
    Inherits Button

    ' 2026-01-13 17:15:00 ModernButton 구현 시작
    ' GDI+를 사용하여 둥근 모서리와 부드러운 호버 효과를 구현합니다.

    Private _borderRadius As Integer = 15
    Private _borderColor As Color = Color.Transparent
    Private _borderSize As Integer = 0
    
    ' 상태별 색상 (Tokyo Night 테마 기반 기본값)
    Private _baseColor As Color = Color.FromArgb(65, 72, 104)
    Private _hoverColor As Color = Color.FromArgb(86, 95, 137)
    Private _pressedColor As Color = Color.FromArgb(41, 46, 66)
    
    Private _isHovered As Boolean = False
    Private _isPressed As Boolean = False

    Public Sub New()
        Me.FlatStyle = FlatStyle.Flat
        Me.FlatAppearance.BorderSize = 0
        Me.Size = New Size(150, 40)
        Me.BackColor = Color.Transparent
        Me.ForeColor = Color.White
        Me.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        Me.Cursor = Cursors.Hand
        Me.DoubleBuffered = True
    End Sub

    ' 속성 정의
    Public Property BorderRadius() As Integer
        Get
            Return _borderRadius
        End Get
        Set(ByVal value As Integer)
            _borderRadius = value
            Me.Invalidate()
        End Set
    End Property

    Public Property CustomBorderColor() As Color
        Get
            Return _borderColor
        End Get
        Set(ByVal value As Color)
            _borderColor = value
            Me.Invalidate()
        End Set
    End Property

    Public Property CustomBaseColor() As Color
        Get
            Return _baseColor
        End Get
        Set(ByVal value As Color)
            _baseColor = value
            Me.Invalidate()
        End Set
    End Property

    Public Property CustomHoverColor() As Color
        Get
            Return _hoverColor
        End Get
        Set(ByVal value As Color)
            _hoverColor = value
            Me.Invalidate()
        End Set
    End Property

    Protected Overrides Sub OnMouseEnter(e As EventArgs)
        MyBase.OnMouseEnter(e)
        _isHovered = True
        Me.Invalidate()
    End Sub

    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        MyBase.OnMouseLeave(e)
        _isHovered = False
        Me.Invalidate()
    End Sub

    Protected Overrides Sub OnMouseDown(mevent As MouseEventArgs)
        MyBase.OnMouseDown(mevent)
        _isPressed = True
        Me.Invalidate()
    End Sub

    Protected Overrides Sub OnMouseUp(mevent As MouseEventArgs)
        MyBase.OnMouseUp(mevent)
        _isPressed = False
        Me.Invalidate()
    End Sub

    Protected Overrides Sub OnPaint(pevent As PaintEventArgs)
        Dim g As Graphics = pevent.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias
        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit

        Dim rect As Rectangle = Me.ClientRectangle
        rect.Width -= 1
        rect.Height -= 1
        
        ' 1. 부모 색상으로 배경 채우기 (블랙 아티팩트 방지)
        ' 투명 배경을 시뮬레이션하기 위해 부모의 BackColor로 전체 영역을 먼저 칠함
        If Me.Parent IsNot Nothing Then
            Using bgBrush As New SolidBrush(Me.Parent.BackColor)
                g.FillRectangle(bgBrush, Me.ClientRectangle)
            End Using
        Else
            g.Clear(Color.FromArgb(26, 27, 38)) ' Default Theme Dark Background
        End If
        
        ' 2. 버튼 상태에 따른 색상 결정
        Dim bg As Color = _baseColor
        If _isPressed Then
            bg = _pressedColor
        ElseIf _isHovered Then
            bg = _hoverColor
        End If
        
        ' 3. 비활성화 상태 색상
        If Not Me.Enabled Then
            bg = Color.FromArgb(60, 60, 60)
        End If

        ' 4. 둥근 버튼 그리기
        Using path As GraphicsPath = GetFigurePath(rect, _borderRadius)
            Using brush As New SolidBrush(bg)
                g.FillPath(brush, path)
            End Using
            
            ' 테두리 (옵션)
            If _borderSize > 0 Then
                Using pen As New Pen(_borderColor, _borderSize)
                    g.DrawPath(pen, path)
                End Using
            End If
        End Using
        
        ' 5. 텍스트 그리기
        Dim textRect As Rectangle = Me.ClientRectangle
        If _isPressed Then
            textRect.Offset(1, 1)
        End If
        
        Dim textColor As Color = Me.ForeColor
        If Not Me.Enabled Then textColor = Color.Gray

        ' TextRenderer는 GDI 방식, DrawString은 GDI+ 방식. 
        ' 배경이 복잡하지 않으므로 AntiAlias + DrawString 조합이 더 깔끔할 수 있으나, 
        ' 레이아웃 정확도를 위해 TextRenderer 유지하되 플래그 조정
        TextRenderer.DrawText(g, Me.Text, Me.Font, textRect, textColor, TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter Or TextFormatFlags.WordBreak)
    End Sub

    Private Function GetFigurePath(rect As Rectangle, radius As Integer) As GraphicsPath
        Dim path As New GraphicsPath()
        Dim curveSize As Single = radius * 2F
        
        path.StartFigure()
        path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90)
        path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90)
        path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90)
        path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90)
        path.CloseFigure()
        
        Return path
    End Function
    ' 2026-01-13 17:15:00 ModernButton 구현 끝
End Class
