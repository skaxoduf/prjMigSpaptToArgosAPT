Imports System.Drawing
Imports System.Windows.Forms

' 2026-01-12 10:00:00 테마 관리자 모듈 생성 (Light, Dark, Tokyo Night 지원)
Public Class ThemeManager

    Public Enum AppTheme
        Light
        Dark
        TokyoNight
        ModernBright ' 2026-01-12 Modern Bright 테마 추가
    End Enum

    Public Class MyPalette
        Public MainBack As Color
        Public SubBack As Color     ' Panel 등
        Public ForeColor As Color
        Public ControlBack As Color ' TextBox, ComboBox 등
        Public ControlFore As Color ' 입력 컨트롤 글자색
        Public Accent As Color      ' 포인트 컬러 (버튼, 테두리 등)
        ' Grid 관련 추가 색상
        Public GridHeaderBack As Color
        Public GridHeaderFore As Color
        Public GridLineColor As Color
        Public GridSelectionBack As Color
    End Class

    Public Shared CurrentTheme As AppTheme = AppTheme.Light
    Public Shared CurrentPalette As MyPalette

    ' 테마별 팔레트 정의
    Private Shared ReadOnly PaletteLight As New MyPalette With {
        .MainBack = Color.WhiteSmoke,
        .SubBack = Color.White,
        .ForeColor = Color.Black,
        .ControlBack = Color.White,
        .ControlFore = Color.Black,
        .Accent = Color.FromArgb(0, 120, 215), ' Windows Blue
        .GridHeaderBack = Color.Gainsboro,
        .GridHeaderFore = Color.Black,
        .GridLineColor = Color.LightGray,
        .GridSelectionBack = Color.FromArgb(0, 120, 215)
    }

    Private Shared ReadOnly PaletteDark As New MyPalette With {
        .MainBack = Color.FromArgb(45, 45, 48),
        .SubBack = Color.FromArgb(30, 30, 30),
        .ForeColor = Color.WhiteSmoke,
        .ControlBack = Color.FromArgb(60, 60, 60),
        .ControlFore = Color.White,
        .Accent = Color.FromArgb(0, 122, 204), ' VS Dark Blue
        .GridHeaderBack = Color.FromArgb(45, 45, 48),
        .GridHeaderFore = Color.WhiteSmoke,
        .GridLineColor = Color.FromArgb(60, 60, 60),
        .GridSelectionBack = Color.FromArgb(0, 122, 204)
    }

    Private Shared ReadOnly PaletteTokyoNight As New MyPalette With {
        .MainBack = Color.FromArgb(26, 27, 38),      ' #1a1b26 (Main Background)
        .SubBack = Color.FromArgb(22, 22, 30),       ' #16161e (Darker Background)
        .ForeColor = Color.White,                        ' #ffffff (Bright Text for Readability)
        .ControlBack = Color.FromArgb(65, 72, 104),  ' #414868 (Surface/Input)
        .ControlFore = Color.White,                      ' #ffffff
        .Accent = Color.FromArgb(122, 162, 247),     ' #7aa2f7 (Blue Accent)
        .GridHeaderBack = Color.FromArgb(22, 22, 30),
        .GridHeaderFore = Color.White,
        .GridLineColor = Color.FromArgb(65, 72, 104),
        .GridSelectionBack = Color.FromArgb(122, 162, 247)
    }

    ' 2026-01-12 Modern Bright 테마 정의 (Dark Mode로 재정의 - Comprehensive Dark)
    Private Shared ReadOnly PaletteModernBright As New MyPalette With {
        .MainBack = Color.FromArgb(26, 27, 38),      ' #1a1b26 (Tokyo Night Background)
        .SubBack = Color.FromArgb(22, 22, 30),       ' #16161e (Darker Panel)
        .ForeColor = Color.White,                        ' White Text
        .ControlBack = Color.FromArgb(65, 72, 104),  ' #414868 (Input Background)
        .ControlFore = Color.White,
        .Accent = Color.FromArgb(122, 162, 247),     ' #7aa2f7 (Blue Accent)
        .GridHeaderBack = Color.FromArgb(65, 72, 104), ' #414868 (Header Background)
        .GridHeaderFore = Color.White,
        .GridLineColor = Color.FromArgb(65, 72, 104),
        .GridSelectionBack = Color.FromArgb(80, 85, 120) ' Highlight
    }

    ' 테마 설정 및 팔레트 갱신
    Public Shared Sub SetTheme(theme As AppTheme)
        CurrentTheme = theme
        Select Case theme
            Case AppTheme.Light : CurrentPalette = PaletteLight
            Case AppTheme.Dark : CurrentPalette = PaletteDark
            Case AppTheme.TokyoNight : CurrentPalette = PaletteTokyoNight
            Case AppTheme.ModernBright : CurrentPalette = PaletteModernBright
        End Select
    End Sub

    ' 폼 전체에 테마 적용
    Public Shared Sub ApplyTheme(frm As Form)
        If CurrentPalette Is Nothing Then SetTheme(AppTheme.Light)

        frm.BackColor = CurrentPalette.MainBack
        frm.ForeColor = CurrentPalette.ForeColor

        ApplyToControls(frm.Controls)

        ' 폼 자체 갱신
        frm.Invalidate(True)
    End Sub

    Private Shared Sub ApplyToControls(controls As Control.ControlCollection)
        For Each ctrl As Control In controls
            ApplyStyle(ctrl)
            If ctrl.HasChildren Then
                ApplyToControls(ctrl.Controls)
            End If
        Next
    End Sub

    Private Shared Sub ApplyStyle(ctrl As Control)
        Dim p = CurrentPalette

        If TypeOf ctrl Is Button Then
            ' 2026-01-18 ModernButton은 자체 스타일을 가지므로 테마 적용에서 제외
            If TypeOf ctrl Is ModernButton Then Return

            Dim btn = DirectCast(ctrl, Button)
            btn.FlatStyle = FlatStyle.Flat
            btn.FlatAppearance.BorderSize = 1
            btn.BackColor = p.SubBack
            btn.ForeColor = p.Accent
            btn.FlatAppearance.BorderColor = p.Accent

        ElseIf TypeOf ctrl Is TextBox OrElse TypeOf ctrl Is ComboBox OrElse TypeOf ctrl Is ListBox Then
            ctrl.BackColor = p.ControlBack
            ctrl.ForeColor = p.ControlFore

        ElseIf TypeOf ctrl Is Label OrElse TypeOf ctrl Is CheckBox OrElse TypeOf ctrl Is RadioButton Then
            ctrl.ForeColor = p.ForeColor

        ElseIf TypeOf ctrl Is Panel OrElse TypeOf ctrl Is GroupBox OrElse TypeOf ctrl Is TabPage Then
            ctrl.BackColor = p.SubBack
            ctrl.ForeColor = p.ForeColor

        ElseIf TypeOf ctrl Is DataGridView Then
            Dim dgv = DirectCast(ctrl, DataGridView)

            ' 기본 설정
            dgv.EnableHeadersVisualStyles = False
            dgv.SelectionMode = DataGridViewSelectionMode.CellSelect
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            dgv.RowHeadersWidth = 30
            dgv.RowTemplate.Height = 30
            dgv.AllowUserToAddRows = False
            dgv.ReadOnly = True

            ' 스타일
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.Single
            dgv.BorderStyle = BorderStyle.None

            ' Colors from Palette (Hardcoding Removed)
            dgv.BackgroundColor = p.MainBack
            dgv.GridColor = p.GridLineColor

            ' Helper Function needed for some properties? No, direct assignment fine.

            ' Header Styling
            dgv.ColumnHeadersDefaultCellStyle.BackColor = p.GridHeaderBack
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = p.GridHeaderFore
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
            dgv.ColumnHeadersHeight = 35
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing

            ' Row Styling
            dgv.DefaultCellStyle.BackColor = p.MainBack ' Main Back for rows too? Or SubBack? keeping Dark logic implies MainBack usually.
            dgv.DefaultCellStyle.ForeColor = p.ForeColor
            dgv.DefaultCellStyle.SelectionBackColor = p.GridSelectionBack
            dgv.DefaultCellStyle.SelectionForeColor = p.ForeColor

            ' RowHeader Styling
            dgv.RowHeadersDefaultCellStyle.BackColor = p.GridHeaderBack
            dgv.RowHeadersDefaultCellStyle.ForeColor = p.GridHeaderFore
            dgv.RowHeadersDefaultCellStyle.SelectionBackColor = p.GridSelectionBack

            ' Typography (Keep Malgun Gothic)
            dgv.ColumnHeadersDefaultCellStyle.Font = New Font("맑은 고딕", 9.5F, FontStyle.Bold)
            dgv.DefaultCellStyle.Font = New Font("맑은 고딕", 9.0F)
            dgv.DefaultCellStyle.Padding = New Padding(0)
            dgv.RowHeadersDefaultCellStyle.Font = New Font("맑은 고딕", 9.0F)

            ' Alternating Rows
            If CurrentTheme = AppTheme.ModernBright OrElse CurrentTheme = AppTheme.TokyoNight Then
                dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(30, 32, 45) ' Slightly lighter than MainBack
            Else
                dgv.AlternatingRowsDefaultCellStyle.BackColor = p.SubBack
            End If

        End If
    End Sub

End Class
