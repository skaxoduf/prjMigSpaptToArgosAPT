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
        rtbLog = New RichTextBox()
        Label1 = New Label()
        cboCompany = New ComboBox()
        Label2 = New Label()
        pnlGlobalTop = New Panel()
        btnSetting = New ModernButton()
        btnMigrateMember = New ModernButton()
        btnLoadExcel = New ModernButton()
        tabMain = New TabControl()
        tabMember = New TabPage()
        splitContainerData = New SplitContainer()
        grpSource = New GroupBox()
        dgvSource = New DataGridView()
        pnlSourcePagination = New FlowLayoutPanel()
        pnlSourceSearch = New Panel()
        txtSourceSearchName = New TextBox()
        cboSourceDong = New ComboBox()
        cboSourceHo = New ComboBox()
        btnSourceSearch = New ModernButton()
        cboLimit = New ComboBox()
        pnlCenterAction = New Panel()
        grpTarget = New GroupBox()
        dgvTarget = New DataGridView()
        pnlTargetPagination = New FlowLayoutPanel()
        pnlTargetSearch = New Panel()
        txtTargetSearchName = New TextBox()
        btnTargetSearch = New ModernButton()
        cboLimitTarget = New ComboBox()
        btnInitTargetMember = New ModernButton()
        tabCourse = New TabPage()
        tabLocker = New TabPage()
        tabLockerPW = New TabPage()
        picPreview = New PictureBox()
        pnlGlobalTop.SuspendLayout()
        tabMain.SuspendLayout()
        tabMember.SuspendLayout()
        CType(splitContainerData, ComponentModel.ISupportInitialize).BeginInit()
        splitContainerData.Panel1.SuspendLayout()
        splitContainerData.Panel2.SuspendLayout()
        splitContainerData.SuspendLayout()
        grpSource.SuspendLayout()
        CType(dgvSource, ComponentModel.ISupportInitialize).BeginInit()
        pnlSourceSearch.SuspendLayout()
        pnlCenterAction.SuspendLayout()
        grpTarget.SuspendLayout()
        CType(dgvTarget, ComponentModel.ISupportInitialize).BeginInit()
        pnlTargetSearch.SuspendLayout()
        CType(picPreview, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' rtbLog
        ' 
        rtbLog.BackColor = Color.FromArgb(CByte(36), CByte(40), CByte(59))
        rtbLog.BorderStyle = BorderStyle.None
        rtbLog.Dock = DockStyle.Bottom
        rtbLog.Font = New Font("Consolas", 9F)
        rtbLog.ForeColor = Color.FromArgb(CByte(192), CByte(202), CByte(245))
        rtbLog.Location = New Point(0, 841)
        rtbLog.Name = "rtbLog"
        rtbLog.Size = New Size(1280, 80)
        rtbLog.TabIndex = 3
        rtbLog.Text = ""
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 9F)
        Label1.ForeColor = Color.FromArgb(CByte(169), CByte(177), CByte(214))
        Label1.Location = New Point(624, 25)
        Label1.Name = "Label1"
        Label1.Size = New Size(122, 15)
        Label1.TabIndex = 2
        Label1.Text = "* setting.ini ... SP 호출"
        ' 
        ' cboCompany
        ' 
        cboCompany.BackColor = Color.FromArgb(CByte(36), CByte(40), CByte(59))
        cboCompany.DropDownStyle = ComboBoxStyle.DropDownList
        cboCompany.FlatStyle = FlatStyle.Flat
        cboCompany.Font = New Font("Segoe UI", 10F)
        cboCompany.ForeColor = Color.White
        cboCompany.FormattingEnabled = True
        cboCompany.Location = New Point(110, 22)
        cboCompany.Name = "cboCompany"
        cboCompany.Size = New Size(492, 25)
        cboCompany.TabIndex = 1
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 11F, FontStyle.Bold)
        Label2.ForeColor = Color.FromArgb(CByte(169), CByte(177), CByte(214))
        Label2.Location = New Point(20, 25)
        Label2.Name = "Label2"
        Label2.Size = New Size(81, 20)
        Label2.TabIndex = 0
        Label2.Text = "이관 대상 :"
        ' 
        ' pnlGlobalTop
        ' 
        pnlGlobalTop.BackColor = Color.FromArgb(CByte(26), CByte(27), CByte(38))
        pnlGlobalTop.Controls.Add(Label2)
        pnlGlobalTop.Controls.Add(cboCompany)
        pnlGlobalTop.Controls.Add(Label1)
        pnlGlobalTop.Controls.Add(btnSetting)
        pnlGlobalTop.Dock = DockStyle.Top
        pnlGlobalTop.Location = New Point(0, 0)
        pnlGlobalTop.Name = "pnlGlobalTop"
        pnlGlobalTop.Size = New Size(1280, 70)
        pnlGlobalTop.TabIndex = 0
        ' 
        ' btnSetting
        ' 
        btnSetting.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnSetting.BackColor = Color.FromArgb(CByte(65), CByte(72), CByte(104))
        btnSetting.BorderRadius = 15
        btnSetting.CustomBaseColor = Color.FromArgb(CByte(65), CByte(72), CByte(104))
        btnSetting.CustomBorderColor = Color.Transparent
        btnSetting.CustomHoverColor = Color.FromArgb(CByte(86), CByte(95), CByte(137))
        btnSetting.FlatAppearance.BorderSize = 0
        btnSetting.FlatStyle = FlatStyle.Flat
        btnSetting.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        btnSetting.ForeColor = Color.White
        btnSetting.Location = New Point(1150, 22)
        btnSetting.Name = "btnSetting"
        btnSetting.Size = New Size(100, 35)
        btnSetting.TabIndex = 6
        btnSetting.Text = "환경설정"
        btnSetting.UseVisualStyleBackColor = False
        ' 
        ' btnMigrateMember
        ' 
        btnMigrateMember.Anchor = AnchorStyles.Top
        btnMigrateMember.BackColor = Color.FromArgb(CByte(122), CByte(162), CByte(247))
        btnMigrateMember.BorderRadius = 15
        btnMigrateMember.CustomBaseColor = Color.FromArgb(CByte(65), CByte(72), CByte(104))
        btnMigrateMember.CustomBorderColor = Color.Transparent
        btnMigrateMember.CustomHoverColor = Color.FromArgb(CByte(86), CByte(95), CByte(137))
        btnMigrateMember.FlatAppearance.BorderSize = 0
        btnMigrateMember.FlatStyle = FlatStyle.Flat
        btnMigrateMember.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold)
        btnMigrateMember.ForeColor = Color.White
        btnMigrateMember.Location = New Point(566, 12)
        btnMigrateMember.Name = "btnMigrateMember"
        btnMigrateMember.Size = New Size(160, 35)
        btnMigrateMember.TabIndex = 5
        btnMigrateMember.Text = "이관하기"
        btnMigrateMember.UseVisualStyleBackColor = False
        ' 
        ' btnLoadExcel
        ' 
        btnLoadExcel.Anchor = AnchorStyles.Top
        btnLoadExcel.BackColor = Color.FromArgb(CByte(230), CByte(126), CByte(34))
        btnLoadExcel.BorderRadius = 15
        btnLoadExcel.CustomBaseColor = Color.FromArgb(CByte(230), CByte(126), CByte(34))
        btnLoadExcel.CustomBorderColor = Color.Transparent
        btnLoadExcel.CustomHoverColor = Color.FromArgb(CByte(243), CByte(156), CByte(18))
        btnLoadExcel.FlatAppearance.BorderSize = 0
        btnLoadExcel.FlatStyle = FlatStyle.Flat
        btnLoadExcel.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold)
        btnLoadExcel.ForeColor = Color.White
        btnLoadExcel.Location = New Point(531, 6)
        btnLoadExcel.Name = "btnLoadExcel"
        btnLoadExcel.Size = New Size(124, 27)
        btnLoadExcel.TabIndex = 7
        btnLoadExcel.Text = "엑셀 불러오기"
        btnLoadExcel.UseVisualStyleBackColor = False
        ' 
        ' tabMain
        ' 
        tabMain.Controls.Add(tabMember)
        tabMain.Controls.Add(tabCourse)
        tabMain.Controls.Add(tabLocker)
        tabMain.Controls.Add(tabLockerPW)
        tabMain.Dock = DockStyle.Fill
        tabMain.DrawMode = TabDrawMode.OwnerDrawFixed
        tabMain.Location = New Point(0, 70)
        tabMain.Name = "tabMain"
        tabMain.Padding = New Point(20, 6)
        tabMain.SelectedIndex = 0
        tabMain.Size = New Size(1280, 771)
        tabMain.TabIndex = 1
        ' 
        ' tabMember
        ' 
        tabMember.BackColor = Color.FromArgb(CByte(26), CByte(27), CByte(38))
        tabMember.Controls.Add(splitContainerData)
        tabMember.Location = New Point(4, 30)
        tabMember.Name = "tabMember"
        tabMember.Padding = New Padding(3)
        tabMember.Size = New Size(1272, 737)
        tabMember.TabIndex = 0
        tabMember.Text = "회원정보 이관"
        ' 
        ' splitContainerData
        ' 
        splitContainerData.Dock = DockStyle.Fill
        splitContainerData.Location = New Point(3, 3)
        splitContainerData.Name = "splitContainerData"
        splitContainerData.Orientation = Orientation.Horizontal
        ' 
        ' splitContainerData.Panel1
        ' 
        splitContainerData.Panel1.Controls.Add(grpSource)
        splitContainerData.Panel1.Controls.Add(pnlCenterAction)
        ' 
        ' splitContainerData.Panel2
        ' 
        splitContainerData.Panel2.Controls.Add(grpTarget)
        splitContainerData.Size = New Size(1266, 731)
        splitContainerData.SplitterDistance = 420
        splitContainerData.TabIndex = 5
        ' 
        ' grpSource
        ' 
        grpSource.Controls.Add(dgvSource)
        grpSource.Controls.Add(pnlSourcePagination)
        grpSource.Controls.Add(pnlSourceSearch)
        grpSource.Dock = DockStyle.Fill
        grpSource.Location = New Point(0, 0)
        grpSource.Name = "grpSource"
        grpSource.Padding = New Padding(10)
        grpSource.Size = New Size(1266, 360)
        grpSource.TabIndex = 0
        grpSource.TabStop = False
        grpSource.Text = "Old DB (Source)"
        ' 
        ' dgvSource
        ' 
        dgvSource.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        dgvSource.BackgroundColor = Color.FromArgb(CByte(26), CByte(27), CByte(38))
        dgvSource.Dock = DockStyle.Fill
        dgvSource.Location = New Point(10, 66)
        dgvSource.Name = "dgvSource"
        dgvSource.Size = New Size(1246, 234)
        dgvSource.TabIndex = 2
        ' 
        ' pnlSourcePagination
        ' 
        pnlSourcePagination.BackColor = Color.Transparent
        pnlSourcePagination.Dock = DockStyle.Bottom
        pnlSourcePagination.Location = New Point(10, 300)
        pnlSourcePagination.Name = "pnlSourcePagination"
        pnlSourcePagination.Padding = New Padding(10)
        pnlSourcePagination.Size = New Size(1246, 50)
        pnlSourcePagination.TabIndex = 3
        pnlSourcePagination.WrapContents = False
        ' 
        ' pnlSourceSearch
        ' 
        pnlSourceSearch.Controls.Add(txtSourceSearchName)
        pnlSourceSearch.Controls.Add(btnLoadExcel)
        pnlSourceSearch.Controls.Add(cboSourceDong)
        pnlSourceSearch.Controls.Add(cboSourceHo)
        pnlSourceSearch.Controls.Add(btnSourceSearch)
        pnlSourceSearch.Controls.Add(cboLimit)
        pnlSourceSearch.Dock = DockStyle.Top
        pnlSourceSearch.Location = New Point(10, 26)
        pnlSourceSearch.Name = "pnlSourceSearch"
        pnlSourceSearch.Size = New Size(1246, 40)
        pnlSourceSearch.TabIndex = 1
        ' 
        ' txtSourceSearchName
        ' 
        txtSourceSearchName.Location = New Point(5, 8)
        txtSourceSearchName.Name = "txtSourceSearchName"
        txtSourceSearchName.PlaceholderText = "이름 검색"
        txtSourceSearchName.Size = New Size(100, 23)
        txtSourceSearchName.TabIndex = 0
        ' 
        ' cboSourceDong
        ' 
        cboSourceDong.DropDownStyle = ComboBoxStyle.DropDownList
        cboSourceDong.Location = New Point(115, 8)
        cboSourceDong.Name = "cboSourceDong"
        cboSourceDong.Size = New Size(100, 23)
        cboSourceDong.TabIndex = 1
        ' 
        ' cboSourceHo
        ' 
        cboSourceHo.DropDownStyle = ComboBoxStyle.DropDownList
        cboSourceHo.Location = New Point(225, 8)
        cboSourceHo.Name = "cboSourceHo"
        cboSourceHo.Size = New Size(100, 23)
        cboSourceHo.TabIndex = 2
        ' 
        ' btnSourceSearch
        ' 
        btnSourceSearch.BackColor = Color.FromArgb(CByte(65), CByte(72), CByte(104))
        btnSourceSearch.BorderRadius = 15
        btnSourceSearch.CustomBaseColor = Color.FromArgb(CByte(65), CByte(72), CByte(104))
        btnSourceSearch.CustomBorderColor = Color.Transparent
        btnSourceSearch.CustomHoverColor = Color.FromArgb(CByte(86), CByte(95), CByte(137))
        btnSourceSearch.FlatStyle = FlatStyle.Flat
        btnSourceSearch.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        btnSourceSearch.ForeColor = Color.White
        btnSourceSearch.Location = New Point(450, 6)
        btnSourceSearch.Name = "btnSourceSearch"
        btnSourceSearch.Size = New Size(75, 27)
        btnSourceSearch.TabIndex = 3
        btnSourceSearch.Text = "조회"
        btnSourceSearch.UseVisualStyleBackColor = False
        ' 
        ' cboLimit
        ' 
        cboLimit.DropDownStyle = ComboBoxStyle.DropDownList
        cboLimit.Items.AddRange(New Object() {"50", "100", "300", "500", "1000"})
        cboLimit.Location = New Point(340, 8)
        cboLimit.Name = "cboLimit"
        cboLimit.Size = New Size(100, 23)
        cboLimit.TabIndex = 4
        ' 
        ' pnlCenterAction
        ' 
        pnlCenterAction.Controls.Add(btnMigrateMember)
        pnlCenterAction.Dock = DockStyle.Bottom
        pnlCenterAction.Location = New Point(0, 360)
        pnlCenterAction.Name = "pnlCenterAction"
        pnlCenterAction.Size = New Size(1266, 60)
        pnlCenterAction.TabIndex = 1
        ' 
        ' grpTarget
        ' 
        grpTarget.Controls.Add(dgvTarget)
        grpTarget.Controls.Add(pnlTargetPagination)
        grpTarget.Controls.Add(pnlTargetSearch)
        grpTarget.Dock = DockStyle.Fill
        grpTarget.Location = New Point(0, 0)
        grpTarget.Name = "grpTarget"
        grpTarget.Padding = New Padding(10)
        grpTarget.Size = New Size(1266, 307)
        grpTarget.TabIndex = 0
        grpTarget.TabStop = False
        grpTarget.Text = "New DB (Target)"
        ' 
        ' dgvTarget
        ' 
        dgvTarget.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        dgvTarget.BackgroundColor = Color.FromArgb(CByte(26), CByte(27), CByte(38))
        dgvTarget.Dock = DockStyle.Fill
        dgvTarget.Location = New Point(10, 66)
        dgvTarget.Name = "dgvTarget"
        dgvTarget.Size = New Size(1246, 181)
        dgvTarget.TabIndex = 2
        ' 
        ' pnlTargetPagination
        ' 
        pnlTargetPagination.BackColor = Color.Transparent
        pnlTargetPagination.Dock = DockStyle.Bottom
        pnlTargetPagination.Location = New Point(10, 247)
        pnlTargetPagination.Name = "pnlTargetPagination"
        pnlTargetPagination.Padding = New Padding(10)
        pnlTargetPagination.Size = New Size(1246, 50)
        pnlTargetPagination.TabIndex = 3
        pnlTargetPagination.WrapContents = False
        ' 
        ' pnlTargetSearch
        ' 
        pnlTargetSearch.Controls.Add(txtTargetSearchName)
        pnlTargetSearch.Controls.Add(btnTargetSearch)
        pnlTargetSearch.Controls.Add(cboLimitTarget)
        pnlTargetSearch.Controls.Add(btnInitTargetMember)
        pnlTargetSearch.Dock = DockStyle.Top
        pnlTargetSearch.Location = New Point(10, 26)
        pnlTargetSearch.Name = "pnlTargetSearch"
        pnlTargetSearch.Size = New Size(1246, 40)
        pnlTargetSearch.TabIndex = 1
        ' 
        ' txtTargetSearchName
        ' 
        txtTargetSearchName.Location = New Point(5, 8)
        txtTargetSearchName.Name = "txtTargetSearchName"
        txtTargetSearchName.PlaceholderText = "이름 검색"
        txtTargetSearchName.Size = New Size(100, 23)
        txtTargetSearchName.TabIndex = 0
        ' 
        ' btnTargetSearch
        ' 
        btnTargetSearch.BackColor = Color.FromArgb(CByte(65), CByte(72), CByte(104))
        btnTargetSearch.BorderRadius = 15
        btnTargetSearch.CustomBaseColor = Color.FromArgb(CByte(65), CByte(72), CByte(104))
        btnTargetSearch.CustomBorderColor = Color.Transparent
        btnTargetSearch.CustomHoverColor = Color.FromArgb(CByte(86), CByte(95), CByte(137))
        btnTargetSearch.FlatStyle = FlatStyle.Flat
        btnTargetSearch.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        btnTargetSearch.ForeColor = Color.White
        btnTargetSearch.Location = New Point(115, 6)
        btnTargetSearch.Name = "btnTargetSearch"
        btnTargetSearch.Size = New Size(75, 27)
        btnTargetSearch.TabIndex = 1
        btnTargetSearch.Text = "조회"
        btnTargetSearch.UseVisualStyleBackColor = False
        ' 
        ' cboLimitTarget
        ' 
        cboLimitTarget.DropDownStyle = ComboBoxStyle.DropDownList
        cboLimitTarget.Items.AddRange(New Object() {"50", "100", "300", "500", "1000"})
        cboLimitTarget.Location = New Point(200, 8)
        cboLimitTarget.Name = "cboLimitTarget"
        cboLimitTarget.Size = New Size(100, 23)
        cboLimitTarget.TabIndex = 2
        ' 
        ' btnInitTargetMember
        ' 
        btnInitTargetMember.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnInitTargetMember.BackColor = Color.Transparent
        btnInitTargetMember.BorderRadius = 15
        btnInitTargetMember.CustomBaseColor = Color.FromArgb(CByte(65), CByte(72), CByte(104))
        btnInitTargetMember.CustomBorderColor = Color.Transparent
        btnInitTargetMember.CustomHoverColor = Color.FromArgb(CByte(86), CByte(95), CByte(137))
        btnInitTargetMember.FlatStyle = FlatStyle.Flat
        btnInitTargetMember.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        btnInitTargetMember.ForeColor = Color.White
        btnInitTargetMember.Location = New Point(1060, 6)
        btnInitTargetMember.Name = "btnInitTargetMember"
        btnInitTargetMember.Size = New Size(180, 27)
        btnInitTargetMember.TabIndex = 3
        btnInitTargetMember.Text = "회원 초기화(데이터 삭제)"
        btnInitTargetMember.UseVisualStyleBackColor = False
        ' 
        ' tabCourse
        ' 
        tabCourse.BackColor = Color.FromArgb(CByte(26), CByte(27), CByte(38))
        tabCourse.Location = New Point(4, 30)
        tabCourse.Name = "tabCourse"
        tabCourse.Size = New Size(1272, 737)
        tabCourse.TabIndex = 1
        tabCourse.Text = "강좌정보 이관"
        ' 
        ' tabLocker
        ' 
        tabLocker.BackColor = Color.FromArgb(CByte(26), CByte(27), CByte(38))
        tabLocker.Location = New Point(4, 30)
        tabLocker.Name = "tabLocker"
        tabLocker.Size = New Size(1272, 737)
        tabLocker.TabIndex = 2
        tabLocker.Text = "사물함 이관"
        ' 
        ' tabLockerPW
        ' 
        tabLockerPW.BackColor = Color.FromArgb(CByte(26), CByte(27), CByte(38))
        tabLockerPW.Location = New Point(4, 30)
        tabLockerPW.Name = "tabLockerPW"
        tabLockerPW.Size = New Size(1272, 737)
        tabLockerPW.TabIndex = 3
        tabLockerPW.Text = "사물함 비밀번호"
        ' 
        ' picPreview
        ' 
        picPreview.BackColor = Color.FromArgb(CByte(40), CByte(44), CByte(60))
        picPreview.BorderStyle = BorderStyle.FixedSingle
        picPreview.Location = New Point(0, 0)
        picPreview.Name = "picPreview"
        picPreview.Size = New Size(200, 200)
        picPreview.SizeMode = PictureBoxSizeMode.Zoom
        picPreview.TabIndex = 10
        picPreview.TabStop = False
        picPreview.Visible = False
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(26), CByte(27), CByte(38))
        ClientSize = New Size(1280, 921)
        Controls.Add(tabMain)
        Controls.Add(picPreview)
        Controls.Add(rtbLog)
        Controls.Add(pnlGlobalTop)
        Font = New Font("Segoe UI", 9F)
        ForeColor = Color.FromArgb(CByte(169), CByte(177), CByte(214))
        Name = "Form1"
        StartPosition = FormStartPosition.CenterScreen
        Text = "SPAPT to ArgosAPT Migration Tool"
        pnlGlobalTop.ResumeLayout(False)
        pnlGlobalTop.PerformLayout()
        tabMain.ResumeLayout(False)
        tabMember.ResumeLayout(False)
        splitContainerData.Panel1.ResumeLayout(False)
        splitContainerData.Panel2.ResumeLayout(False)
        CType(splitContainerData, ComponentModel.ISupportInitialize).EndInit()
        splitContainerData.ResumeLayout(False)
        grpSource.ResumeLayout(False)
        CType(dgvSource, ComponentModel.ISupportInitialize).EndInit()
        pnlSourceSearch.ResumeLayout(False)
        pnlSourceSearch.PerformLayout()
        pnlCenterAction.ResumeLayout(False)
        grpTarget.ResumeLayout(False)
        CType(dgvTarget, ComponentModel.ISupportInitialize).EndInit()
        pnlTargetSearch.ResumeLayout(False)
        pnlTargetSearch.PerformLayout()
        CType(picPreview, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)

    End Sub
    Friend WithEvents rtbLog As System.Windows.Forms.RichTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboCompany As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    
    ' Redesign Controls
    Friend WithEvents pnlGlobalTop As System.Windows.Forms.Panel
    Friend WithEvents tabMain As System.Windows.Forms.TabControl
    Friend WithEvents tabMember As System.Windows.Forms.TabPage
    Friend WithEvents tabCourse As System.Windows.Forms.TabPage
    Friend WithEvents tabLocker As System.Windows.Forms.TabPage
    Friend WithEvents tabLockerPW As System.Windows.Forms.TabPage
    
    ' Source Search Controls
    Friend WithEvents pnlSourceSearch As System.Windows.Forms.Panel
    Friend WithEvents txtSourceSearchName As System.Windows.Forms.TextBox
    Friend WithEvents cboSourceDong As System.Windows.Forms.ComboBox
    Friend WithEvents cboSourceHo As System.Windows.Forms.ComboBox
    Friend WithEvents btnSourceSearch As ModernButton
    Friend WithEvents cboLimit As ComboBox
    
    ' Target Search Controls
    Friend WithEvents pnlTargetSearch As System.Windows.Forms.Panel
    Friend WithEvents txtTargetSearchName As System.Windows.Forms.TextBox
    Friend WithEvents btnTargetSearch As ModernButton
    Friend WithEvents cboLimitTarget As ComboBox
    Friend WithEvents btnInitTargetMember As ModernButton
    
    Friend WithEvents pnlSourcePagination As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents pnlTargetPagination As System.Windows.Forms.FlowLayoutPanel
    
    Friend WithEvents splitContainerData As System.Windows.Forms.SplitContainer
    Friend WithEvents pnlCenterAction As System.Windows.Forms.Panel
    Friend WithEvents grpSource As System.Windows.Forms.GroupBox
    Friend WithEvents dgvSource As System.Windows.Forms.DataGridView
    Friend WithEvents grpTarget As System.Windows.Forms.GroupBox
    Friend WithEvents dgvTarget As System.Windows.Forms.DataGridView
    Friend WithEvents btnMigrateMember As ModernButton
    Friend WithEvents btnLoadExcel As ModernButton
    Friend WithEvents btnSetting As ModernButton
    Friend WithEvents picPreview As PictureBox
 End Class
