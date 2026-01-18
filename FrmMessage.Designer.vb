<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMessage
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
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.btnOK = New ModernButton()
        Me.btnCancel = New ModernButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblMessage
        '
        Me.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMessage.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.lblMessage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(202, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.lblMessage.Location = New System.Drawing.Point(20, 20)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(460, 100)
        Me.lblMessage.TabIndex = 0
        Me.lblMessage.Text = "Message Content"
        Me.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.BackColor = System.Drawing.Color.FromArgb(CType(CType(122, Byte), Integer), CType(CType(162, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.btnOK.CustomBaseColor = System.Drawing.Color.FromArgb(CType(CType(122, Byte), Integer), CType(CType(162, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.btnOK.BorderRadius = 15
        Me.btnOK.FlatAppearance.BorderSize = 0
        Me.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOK.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.btnOK.ForeColor = System.Drawing.Color.White
        Me.btnOK.Location = New System.Drawing.Point(313, 145)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(80, 35)
        Me.btnOK.TabIndex = 1
        Me.btnOK.Text = "확인"
        Me.btnOK.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(104, Byte), Integer))
        Me.btnCancel.CustomBaseColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(104, Byte), Integer))
        Me.btnCancel.BorderRadius = 15
        Me.btnCancel.FlatAppearance.BorderSize = 0
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Location = New System.Drawing.Point(399, 145)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(80, 35)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "취소"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lblMessage)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(20)
        Me.Panel1.Size = New System.Drawing.Size(500, 130)
        Me.Panel1.TabIndex = 3
        '
        'FrmMessage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(27, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(500, 191)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmMessage"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "알림"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblMessage As System.Windows.Forms.Label
    Friend WithEvents btnOK As ModernButton
    Friend WithEvents btnCancel As ModernButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
End Class
