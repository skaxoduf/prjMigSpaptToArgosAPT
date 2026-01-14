Imports System.Drawing.Drawing2D
Imports System.ComponentModel

Public Class CircularProgressBar
    Inherits Control

    Private _value As Integer = 0
    Private _maximum As Integer = 100
    Private _lineThickness As Integer = 15
    Private _progressColor As Color = Color.FromArgb(0, 122, 255) ' Blue
    Private _trackColor As Color = Color.FromArgb(30, 30, 30) ' Dark Gray/Black
    Private _textColor As Color = Color.White

    Public Sub New()
        SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        Me.DoubleBuffered = True
        Me.Size = New Size(200, 200)
        Me.BackColor = Color.Transparent
        Me.ForeColor = Color.White
        Me.Font = New Font("Segoe UI", 24, FontStyle.Bold)
    End Sub

    <Category("Behavior")>
    Public Property Value As Integer
        Get
            Return _value
        End Get
        Set(val As Integer)
            If val < 0 Then val = 0
            If val > _maximum Then val = _maximum
            _value = val
            Me.Invalidate()
        End Set
    End Property

    <Category("Behavior")>
    Public Property Maximum As Integer
        Get
            Return _maximum
        End Get
        Set(val As Integer)
            If val < 1 Then val = 1
            _maximum = val
            Me.Invalidate()
        End Set
    End Property

    <Category("Appearance")>
    Public Property LineThickness As Integer
        Get
            Return _lineThickness
        End Get
        Set(val As Integer)
            _lineThickness = val
            Me.Invalidate()
        End Set
    End Property

    <Category("Appearance")>
    Public Property ProgressColor As Color
        Get
            Return _progressColor
        End Get
        Set(val As Color)
            _progressColor = val
            Me.Invalidate()
        End Set
    End Property

    <Category("Appearance")>
    Public Property TrackColor As Color
        Get
            Return _trackColor
        End Get
        Set(val As Color)
            _trackColor = val
            Me.Invalidate()
        End Set
    End Property

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        Dim g As Graphics = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias
        g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias

        Dim rect As Rectangle = Me.ClientRectangle
        rect.Inflate(-_lineThickness, -_lineThickness)

        ' Draw Track
        Using pen As New Pen(_trackColor, _lineThickness)
            g.DrawEllipse(pen, rect)
        End Using

        ' Draw Progress
        If _value > 0 Then
            Dim sweepAngle As Single = (360.0F * _value) / _maximum
            Using pen As New Pen(_progressColor, _lineThickness)
                pen.StartCap = LineCap.Round
                pen.EndCap = LineCap.Round
                ' Start from top (-90 degrees)
                g.DrawArc(pen, rect, -90, sweepAngle)
            End Using
        End If

        ' Draw Text
        Dim percent As Integer = CInt((_value / _maximum) * 100)
        Dim text As String = String.Format("{0}%", percent)
        Dim textSize As SizeF = g.MeasureString(text, Me.Font)
        Dim textPoint As New PointF((Me.Width - textSize.Width) / 2, (Me.Height - textSize.Height) / 2)

        Using brush As New SolidBrush(Me.ForeColor)
            g.DrawString(text, Me.Font, brush, textPoint)
        End Using
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        Using path As New GraphicsPath()
            path.AddEllipse(0, 0, Me.Width, Me.Height)
            Me.Region = New Region(path)
        End Using
    End Sub
End Class
