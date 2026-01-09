<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        btnMigrateMember = New Button()
        rtbLog = New RichTextBox()
        Label1 = New Label()
        btnSetting = New Button()
        cboCompany = New ComboBox()
        Label2 = New Label()
        SuspendLayout()
        ' 
        ' btnMigrateMember
        ' 
        btnMigrateMember.BackColor = Color.FromArgb(CByte(122), CByte(162), CByte(247))
        btnMigrateMember.FlatAppearance.BorderSize = 0
        btnMigrateMember.FlatStyle = FlatStyle.Flat
        btnMigrateMember.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold)
        btnMigrateMember.ForeColor = Color.White
        btnMigrateMember.Location = New Point(12, 50)
        btnMigrateMember.Name = "btnMigrateMember"
        btnMigrateMember.Size = New Size(150, 40)
        btnMigrateMember.TabIndex = 2
        btnMigrateMember.Text = "회원정보 이관 실행"
        btnMigrateMember.UseVisualStyleBackColor = False
        ' 
        ' rtbLog
        ' 
        rtbLog.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        rtbLog.BackColor = Color.FromArgb(CByte(36), CByte(40), CByte(59))
        rtbLog.BorderStyle = BorderStyle.None
        rtbLog.Font = New Font("Consolas", 9.75F)
        rtbLog.ForeColor = Color.FromArgb(CByte(192), CByte(202), CByte(245))
        rtbLog.Location = New Point(12, 100)
        rtbLog.Name = "rtbLog"
        rtbLog.Size = New Size(776, 450)
        rtbLog.TabIndex = 3
        rtbLog.Text = ""
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.ForeColor = Color.FromArgb(CByte(169), CByte(177), CByte(214))
        Label1.Location = New Point(220, 64)
        Label1.Name = "Label1"
        Label1.Size = New Size(281, 15)
        Label1.TabIndex = 2
        Label1.Text = "* setting.ini 및 선택된 회사 정보로 SP를 호출합니다."
        ' 
        ' btnSetting
        ' 
        btnSetting.BackColor = Color.FromArgb(CByte(65), CByte(72), CByte(104))
        btnSetting.FlatAppearance.BorderSize = 0
        btnSetting.FlatStyle = FlatStyle.Flat
        btnSetting.ForeColor = Color.White
        btnSetting.Location = New Point(688, 12)
        btnSetting.Name = "btnSetting"
        btnSetting.Size = New Size(100, 40)
        btnSetting.TabIndex = 4
        btnSetting.Text = "환경설정"
        btnSetting.UseVisualStyleBackColor = False
        ' 
        ' cboCompany
        ' 
        cboCompany.BackColor = Color.FromArgb(CByte(36), CByte(40), CByte(59))
        cboCompany.DropDownStyle = ComboBoxStyle.DropDownList
        cboCompany.FlatStyle = FlatStyle.Flat
        cboCompany.ForeColor = Color.White
        cboCompany.FormattingEnabled = True
        cboCompany.Location = New Point(100, 15)
        cboCompany.Name = "cboCompany"
        cboCompany.Size = New Size(401, 23)
        cboCompany.TabIndex = 1
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.ForeColor = Color.FromArgb(CByte(169), CByte(177), CByte(214))
        Label2.Location = New Point(15, 18)
        Label2.Name = "Label2"
        Label2.Size = New Size(64, 15)
        Label2.TabIndex = 0
        Label2.Text = "이관 대상 :"
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(26), CByte(27), CByte(38))
        ClientSize = New Size(800, 562)
        Controls.Add(Label2)
        Controls.Add(cboCompany)
        Controls.Add(Label1)
        Controls.Add(rtbLog)
        Controls.Add(btnMigrateMember)
        Controls.Add(btnSetting)
        Font = New Font("Segoe UI", 9F)
        ForeColor = Color.FromArgb(CByte(169), CByte(177), CByte(214))
        Name = "Form1"
        StartPosition = FormStartPosition.CenterScreen
        Text = "SPAPT to ArgosAPT Migration Tool"
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents btnMigrateMember As System.Windows.Forms.Button
    Friend WithEvents rtbLog As System.Windows.Forms.RichTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnSetting As System.Windows.Forms.Button
    Friend WithEvents cboCompany As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
