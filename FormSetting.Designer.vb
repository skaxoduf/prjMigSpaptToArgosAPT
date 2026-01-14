<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormSetting
    Inherits System.Windows.Forms.Form

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

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        GroupBox2 = New GroupBox()
        Label7 = New Label()
        txtSourceDB = New TextBox()
        Label6 = New Label()
        txtTargetDB = New TextBox()
        Label5 = New Label()
        txtPassword = New TextBox()
        Label4 = New Label()
        txtUserID = New TextBox()
        Label3 = New Label()
        txtServerIP = New TextBox()
        btnSave = New Button()
        btnClose = New Button()
        GroupBox2.SuspendLayout()
        SuspendLayout()
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(Label7)
        GroupBox2.Controls.Add(txtSourceDB)
        GroupBox2.Controls.Add(Label6)
        GroupBox2.Controls.Add(txtTargetDB)
        GroupBox2.Controls.Add(Label5)
        GroupBox2.Controls.Add(txtPassword)
        GroupBox2.Controls.Add(Label4)
        GroupBox2.Controls.Add(txtUserID)
        GroupBox2.Controls.Add(Label3)
        GroupBox2.Controls.Add(txtServerIP)
        GroupBox2.ForeColor = Color.FromArgb(CByte(169), CByte(177), CByte(214))
        GroupBox2.Location = New Point(12, 12)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Size = New Size(360, 180)
        GroupBox2.TabIndex = 1
        GroupBox2.TabStop = False
        GroupBox2.Text = "DB 접속 설정"
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(15, 145)
        Label7.Name = "Label7"
        Label7.Size = New Size(61, 15)
        Label7.TabIndex = 9
        Label7.Text = "Source DB"
        ' 
        ' txtSourceDB
        ' 
        txtSourceDB.BackColor = Color.FromArgb(CByte(36), CByte(40), CByte(59))
        txtSourceDB.BorderStyle = BorderStyle.FixedSingle
        txtSourceDB.ForeColor = Color.White
        txtSourceDB.Location = New Point(110, 142)
        txtSourceDB.Name = "txtSourceDB"
        txtSourceDB.Size = New Size(200, 23)
        txtSourceDB.TabIndex = 8
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(15, 115)
        Label6.Name = "Label6"
        Label6.Size = New Size(58, 15)
        Label6.TabIndex = 7
        Label6.Text = "Target DB"
        ' 
        ' txtTargetDB
        ' 
        txtTargetDB.BackColor = Color.FromArgb(CByte(36), CByte(40), CByte(59))
        txtTargetDB.BorderStyle = BorderStyle.FixedSingle
        txtTargetDB.ForeColor = Color.White
        txtTargetDB.Location = New Point(110, 112)
        txtTargetDB.Name = "txtTargetDB"
        txtTargetDB.Size = New Size(200, 23)
        txtTargetDB.TabIndex = 6
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(15, 85)
        Label5.Name = "Label5"
        Label5.Size = New Size(57, 15)
        Label5.TabIndex = 5
        Label5.Text = "Password"
        ' 
        ' txtPassword
        ' 
        txtPassword.BackColor = Color.FromArgb(CByte(36), CByte(40), CByte(59))
        txtPassword.BorderStyle = BorderStyle.FixedSingle
        txtPassword.ForeColor = Color.White
        txtPassword.Location = New Point(110, 82)
        txtPassword.Name = "txtPassword"
        txtPassword.PasswordChar = "*"c
        txtPassword.Size = New Size(200, 23)
        txtPassword.TabIndex = 4
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(15, 55)
        Label4.Name = "Label4"
        Label4.Size = New Size(44, 15)
        Label4.TabIndex = 3
        Label4.Text = "User ID"
        ' 
        ' txtUserID
        ' 
        txtUserID.BackColor = Color.FromArgb(CByte(36), CByte(40), CByte(59))
        txtUserID.BorderStyle = BorderStyle.FixedSingle
        txtUserID.ForeColor = Color.White
        txtUserID.Location = New Point(110, 52)
        txtUserID.Name = "txtUserID"
        txtUserID.Size = New Size(200, 23)
        txtUserID.TabIndex = 2
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(15, 25)
        Label3.Name = "Label3"
        Label3.Size = New Size(52, 15)
        Label3.TabIndex = 1
        Label3.Text = "Server IP"
        ' 
        ' txtServerIP
        ' 
        txtServerIP.BackColor = Color.FromArgb(CByte(36), CByte(40), CByte(59))
        txtServerIP.BorderStyle = BorderStyle.FixedSingle
        txtServerIP.ForeColor = Color.White
        txtServerIP.Location = New Point(110, 22)
        txtServerIP.Name = "txtServerIP"
        txtServerIP.Size = New Size(200, 23)
        txtServerIP.TabIndex = 0
        ' 
        ' btnSave
        ' 
        btnSave.BackColor = Color.FromArgb(CByte(122), CByte(162), CByte(247))
        btnSave.FlatAppearance.BorderSize = 0
        btnSave.FlatStyle = FlatStyle.Flat
        btnSave.ForeColor = Color.White
        btnSave.Location = New Point(195, 212)
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(80, 30)
        btnSave.TabIndex = 2
        btnSave.Text = "저장"
        btnSave.UseVisualStyleBackColor = False
        ' 
        ' btnClose
        ' 
        btnClose.BackColor = Color.FromArgb(CByte(247), CByte(118), CByte(142))
        btnClose.FlatAppearance.BorderSize = 0
        btnClose.FlatStyle = FlatStyle.Flat
        btnClose.ForeColor = Color.White
        btnClose.Location = New Point(290, 212)
        btnClose.Name = "btnClose"
        btnClose.Size = New Size(80, 30)
        btnClose.TabIndex = 3
        btnClose.Text = "닫기"
        btnClose.UseVisualStyleBackColor = False
        ' 
        ' FormSetting
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(26), CByte(27), CByte(38))
        ClientSize = New Size(384, 267)
        Controls.Add(btnClose)
        Controls.Add(btnSave)
        Controls.Add(GroupBox2)
        Font = New Font("Segoe UI", 9F)
        ForeColor = Color.FromArgb(CByte(169), CByte(177), CByte(214))
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        MinimizeBox = False
        Name = "FormSetting"
        StartPosition = FormStartPosition.CenterParent
        Text = "환경 설정"
        GroupBox2.ResumeLayout(False)
        GroupBox2.PerformLayout()
        ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtSourceDB As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtTargetDB As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtUserID As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtServerIP As System.Windows.Forms.TextBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
End Class
