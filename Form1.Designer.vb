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
        splitContainerCourse = New SplitContainer()
        grpCourseSource = New GroupBox()
        pnlCourseSourcePagination = New FlowLayoutPanel()
        dgvCourseSource = New DataGridView()
        pnlCourseSourceSearch = New Panel()
        btnCourseLoadExcel = New ModernButton()
        btnCourseSourceSearch = New ModernButton()
        dtpCourseEnd = New DateTimePicker()
        dtpCourseStart = New DateTimePicker()
        cboCourseLimit = New ComboBox()
        cboCourseSourceHo = New ComboBox()
        cboCourseSourceDong = New ComboBox()
        txtCourseSourceSearchName = New TextBox()
        pnlCourseCenterAction = New Panel()
        btnMigrateCourse = New ModernButton()
        grpCourseTarget = New GroupBox()
        pnlCourseTargetPagination = New FlowLayoutPanel()
        dgvCourseTarget = New DataGridView()
        pnlCourseTargetSearch = New Panel()
        btnCourseTargetSearch = New ModernButton()
        txtCourseTargetSearchName = New TextBox()
        cboCourseLimitTarget = New ComboBox()
        btnInitTargetCourse = New ModernButton()
        tabLocker = New TabPage()
        splitContainerLocker = New SplitContainer()
        grpLockerSource = New GroupBox()
        pnlLockerSourcePagination = New FlowLayoutPanel()
        dgvLockerSource = New DataGridView()
        pnlLockerSourceSearch = New Panel()
        btnLockerLoadExcel = New ModernButton()
        btnLockerSourceSearch = New ModernButton()
        cboLockerLimit = New ComboBox()
        cboLockerSourceHo = New ComboBox()
        cboLockerSourceDong = New ComboBox()
        txtLockerSourceSearchName = New TextBox()
        pnlLockerCenterAction = New Panel()
        btnMigrateLocker = New ModernButton()
        grpLockerTarget = New GroupBox()
        pnlLockerTargetPagination = New FlowLayoutPanel()
        dgvLockerTarget = New DataGridView()
        pnlLockerTargetSearch = New Panel()
        btnLockerTargetSearch = New ModernButton()
        txtLockerTargetSearchName = New TextBox()
        cboLockerLimitTarget = New ComboBox()
        btnInitTargetLocker = New ModernButton()
        tabProduct = New TabPage()
        splitContainerProduct = New SplitContainer()
        grpProductSource = New GroupBox()
        pnlProductSourcePagination = New FlowLayoutPanel()
        dgvProductSource = New DataGridView()
        pnlProductSourceSearch = New Panel()
        btnProductLoadExcel = New ModernButton()
        btnProductSourceSearch = New ModernButton()
        dtpProductEnd = New DateTimePicker()
        dtpProductStart = New DateTimePicker()
        cboProductLimit = New ComboBox()
        cboProductSourceHo = New ComboBox()
        cboProductSourceDong = New ComboBox()
        txtProductSourceSearchName = New TextBox()
        pnlProductCenterAction = New Panel()
        btnMigrateProduct = New ModernButton()
        grpProductTarget = New GroupBox()
        pnlProductTargetPagination = New FlowLayoutPanel()
        dgvProductTarget = New DataGridView()
        pnlProductTargetSearch = New Panel()
        btnProductTargetSearch = New ModernButton()
        txtProductTargetSearchName = New TextBox()
        cboProductLimitTarget = New ComboBox()
        btnInitTargetProduct = New ModernButton()
        tabGeneralFacility = New TabPage()
        splitContainerGeneral = New SplitContainer()
        grpGeneralSource = New GroupBox()
        pnlGeneralSourcePagination = New FlowLayoutPanel()
        dgvGeneralSource = New DataGridView()
        pnlGeneralSourceSearch = New Panel()
        btnGeneralLoadExcel = New ModernButton()
        btnGeneralSourceSearch = New ModernButton()
        dtpGeneralEnd = New DateTimePicker()
        dtpGeneralStart = New DateTimePicker()
        cboGeneralLimit = New ComboBox()
        cboGeneralSourceHo = New ComboBox()
        cboGeneralSourceDong = New ComboBox()
        txtGeneralSourceSearchName = New TextBox()
        pnlGeneralCenterAction = New Panel()
        btnMigrateGeneral = New ModernButton()
        grpGeneralTarget = New GroupBox()
        pnlGeneralTargetPagination = New FlowLayoutPanel()
        dgvGeneralTarget = New DataGridView()
        pnlGeneralTargetSearch = New Panel()
        btnGeneralTargetSearch = New ModernButton()
        txtGeneralTargetSearchName = New TextBox()
        cboGeneralLimitTarget = New ComboBox()
        btnInitTargetGeneral = New ModernButton()
        tabAccommodation = New TabPage()
        splitContainerAccommodation = New SplitContainer()
        grpAccommodationSource = New GroupBox()
        pnlAccommodationSourcePagination = New FlowLayoutPanel()
        dgvAccommodationSource = New DataGridView()
        pnlAccommodationSourceSearch = New Panel()
        btnAccommodationLoadExcel = New ModernButton()
        btnAccommodationSourceSearch = New ModernButton()
        dtpAccommodationEnd = New DateTimePicker()
        dtpAccommodationStart = New DateTimePicker()
        cboAccommodationLimit = New ComboBox()
        cboAccommodationSourceHo = New ComboBox()
        cboAccommodationSourceDong = New ComboBox()
        txtAccommodationSourceSearchName = New TextBox()
        pnlAccommodationCenterAction = New Panel()
        btnMigrateAccommodation = New ModernButton()
        grpAccommodationTarget = New GroupBox()
        pnlAccommodationTargetPagination = New FlowLayoutPanel()
        dgvAccommodationTarget = New DataGridView()
        pnlAccommodationTargetSearch = New Panel()
        btnAccommodationTargetSearch = New ModernButton()
        txtAccommodationTargetSearchName = New TextBox()
        cboAccommodationLimitTarget = New ComboBox()
        btnInitTargetAccommodation = New ModernButton()
        tabSpaceFacility = New TabPage()
        splitContainerSpace = New SplitContainer()
        grpSpaceSource = New GroupBox()
        pnlSpaceSourcePagination = New FlowLayoutPanel()
        dgvSpaceSource = New DataGridView()
        pnlSpaceSourceSearch = New Panel()
        btnSpaceLoadExcel = New ModernButton()
        btnSpaceSourceSearch = New ModernButton()
        dtpSpaceEnd = New DateTimePicker()
        dtpSpaceStart = New DateTimePicker()
        cboSpaceLimit = New ComboBox()
        cboSpaceSourceHo = New ComboBox()
        cboSpaceSourceDong = New ComboBox()
        txtSpaceSourceSearchName = New TextBox()
        pnlSpaceCenterAction = New Panel()
        btnMigrateSpace = New ModernButton()
        grpSpaceTarget = New GroupBox()
        pnlSpaceTargetPagination = New FlowLayoutPanel()
        dgvSpaceTarget = New DataGridView()
        pnlSpaceTargetSearch = New Panel()
        btnSpaceTargetSearch = New ModernButton()
        txtSpaceTargetSearchName = New TextBox()
        cboSpaceLimitTarget = New ComboBox()
        btnInitTargetSpace = New ModernButton()
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
        tabCourse.SuspendLayout()
        CType(splitContainerCourse, ComponentModel.ISupportInitialize).BeginInit()
        splitContainerCourse.Panel1.SuspendLayout()
        splitContainerCourse.Panel2.SuspendLayout()
        splitContainerCourse.SuspendLayout()
        grpCourseSource.SuspendLayout()
        CType(dgvCourseSource, ComponentModel.ISupportInitialize).BeginInit()
        pnlCourseSourceSearch.SuspendLayout()
        pnlCourseCenterAction.SuspendLayout()
        grpCourseTarget.SuspendLayout()
        CType(dgvCourseTarget, ComponentModel.ISupportInitialize).BeginInit()
        pnlCourseTargetSearch.SuspendLayout()
        tabLocker.SuspendLayout()
        CType(splitContainerLocker, ComponentModel.ISupportInitialize).BeginInit()
        splitContainerLocker.Panel1.SuspendLayout()
        splitContainerLocker.Panel2.SuspendLayout()
        splitContainerLocker.SuspendLayout()
        grpLockerSource.SuspendLayout()
        CType(dgvLockerSource, ComponentModel.ISupportInitialize).BeginInit()
        pnlLockerSourceSearch.SuspendLayout()
        pnlLockerCenterAction.SuspendLayout()
        grpLockerTarget.SuspendLayout()
        CType(dgvLockerTarget, ComponentModel.ISupportInitialize).BeginInit()
        pnlLockerTargetSearch.SuspendLayout()
        tabProduct.SuspendLayout()
        CType(splitContainerProduct, ComponentModel.ISupportInitialize).BeginInit()
        splitContainerProduct.Panel1.SuspendLayout()
        splitContainerProduct.Panel2.SuspendLayout()
        splitContainerProduct.SuspendLayout()
        grpProductSource.SuspendLayout()
        CType(dgvProductSource, ComponentModel.ISupportInitialize).BeginInit()
        pnlProductSourceSearch.SuspendLayout()
        pnlProductCenterAction.SuspendLayout()
        grpProductTarget.SuspendLayout()
        CType(dgvProductTarget, ComponentModel.ISupportInitialize).BeginInit()
        pnlProductTargetSearch.SuspendLayout()
        tabGeneralFacility.SuspendLayout()
        CType(splitContainerGeneral, ComponentModel.ISupportInitialize).BeginInit()
        splitContainerGeneral.Panel1.SuspendLayout()
        splitContainerGeneral.Panel2.SuspendLayout()
        splitContainerGeneral.SuspendLayout()
        grpGeneralSource.SuspendLayout()
        CType(dgvGeneralSource, ComponentModel.ISupportInitialize).BeginInit()
        pnlGeneralSourceSearch.SuspendLayout()
        pnlGeneralCenterAction.SuspendLayout()
        grpGeneralTarget.SuspendLayout()
        CType(dgvGeneralTarget, ComponentModel.ISupportInitialize).BeginInit()
        pnlGeneralTargetSearch.SuspendLayout()
        tabAccommodation.SuspendLayout()
        CType(splitContainerAccommodation, ComponentModel.ISupportInitialize).BeginInit()
        splitContainerAccommodation.Panel1.SuspendLayout()
        splitContainerAccommodation.Panel2.SuspendLayout()
        splitContainerAccommodation.SuspendLayout()
        grpAccommodationSource.SuspendLayout()
        CType(dgvAccommodationSource, ComponentModel.ISupportInitialize).BeginInit()
        pnlAccommodationSourceSearch.SuspendLayout()
        pnlAccommodationCenterAction.SuspendLayout()
        grpAccommodationTarget.SuspendLayout()
        CType(dgvAccommodationTarget, ComponentModel.ISupportInitialize).BeginInit()
        pnlAccommodationTargetSearch.SuspendLayout()
        tabSpaceFacility.SuspendLayout()
        CType(splitContainerSpace, ComponentModel.ISupportInitialize).BeginInit()
        splitContainerSpace.Panel1.SuspendLayout()
        splitContainerSpace.Panel2.SuspendLayout()
        splitContainerSpace.SuspendLayout()
        grpSpaceSource.SuspendLayout()
        CType(dgvSpaceSource, ComponentModel.ISupportInitialize).BeginInit()
        pnlSpaceSourceSearch.SuspendLayout()
        pnlSpaceCenterAction.SuspendLayout()
        grpSpaceTarget.SuspendLayout()
        CType(dgvSpaceTarget, ComponentModel.ISupportInitialize).BeginInit()
        pnlSpaceTargetSearch.SuspendLayout()
        CType(picPreview, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' rtbLog
        ' 
        rtbLog.BackColor = Color.FromArgb(CByte(36), CByte(40), CByte(59))
        rtbLog.BorderStyle = BorderStyle.None
        rtbLog.Dock = DockStyle.Bottom
        rtbLog.Font = New Font("Consolas", 9.0F)
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
        Label1.Font = New Font("Segoe UI", 9.0F)
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
        cboCompany.Font = New Font("Segoe UI", 10.0F)
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
        Label2.Font = New Font("Segoe UI", 11.0F, FontStyle.Bold)
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
        btnSetting.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
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
        tabMain.Controls.Add(tabProduct)
        tabMain.Controls.Add(tabGeneralFacility)
        tabMain.Controls.Add(tabAccommodation)
        tabMain.Controls.Add(tabSpaceFacility)
        tabMain.Dock = DockStyle.Fill
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
        btnSourceSearch.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
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
        btnTargetSearch.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
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
        btnInitTargetMember.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
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
        tabCourse.Controls.Add(splitContainerCourse)
        tabCourse.Location = New Point(4, 30)
        tabCourse.Name = "tabCourse"
        tabCourse.Padding = New Padding(3)
        tabCourse.Size = New Size(1272, 737)
        tabCourse.TabIndex = 1
        tabCourse.Text = "강좌정보 이관"
        ' 
        ' splitContainerCourse
        ' 
        splitContainerCourse.Dock = DockStyle.Fill
        splitContainerCourse.Location = New Point(3, 3)
        splitContainerCourse.Name = "splitContainerCourse"
        splitContainerCourse.Orientation = Orientation.Horizontal
        ' 
        ' splitContainerCourse.Panel1
        ' 
        splitContainerCourse.Panel1.Controls.Add(grpCourseSource)
        splitContainerCourse.Panel1.Controls.Add(pnlCourseCenterAction)
        ' 
        ' splitContainerCourse.Panel2
        ' 
        splitContainerCourse.Panel2.Controls.Add(grpCourseTarget)
        splitContainerCourse.Size = New Size(1266, 731)
        splitContainerCourse.SplitterDistance = 420
        splitContainerCourse.TabIndex = 0
        ' 
        ' grpCourseSource
        ' 
        grpCourseSource.Controls.Add(pnlCourseSourcePagination)
        grpCourseSource.Controls.Add(dgvCourseSource)
        grpCourseSource.Controls.Add(pnlCourseSourceSearch)
        grpCourseSource.Dock = DockStyle.Fill
        grpCourseSource.Location = New Point(0, 0)
        grpCourseSource.Name = "grpCourseSource"
        grpCourseSource.Padding = New Padding(10)
        grpCourseSource.Size = New Size(1266, 360)
        grpCourseSource.TabIndex = 0
        grpCourseSource.TabStop = False
        grpCourseSource.Text = "Old DB (Source) - 강좌/수강내역"
        ' 
        ' pnlCourseSourcePagination
        ' 
        pnlCourseSourcePagination.AutoSize = True
        pnlCourseSourcePagination.BackColor = Color.Transparent
        pnlCourseSourcePagination.Dock = DockStyle.Bottom
        pnlCourseSourcePagination.Location = New Point(10, 340)
        pnlCourseSourcePagination.Name = "pnlCourseSourcePagination"
        pnlCourseSourcePagination.Padding = New Padding(0, 10, 0, 0)
        pnlCourseSourcePagination.Size = New Size(1246, 10)
        pnlCourseSourcePagination.TabIndex = 2
        ' 
        ' dgvCourseSource
        ' 
        dgvCourseSource.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvCourseSource.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        dgvCourseSource.BackgroundColor = Color.FromArgb(CByte(26), CByte(27), CByte(38))
        dgvCourseSource.Location = New Point(10, 70)
        dgvCourseSource.Name = "dgvCourseSource"
        dgvCourseSource.Size = New Size(1246, 235)
        dgvCourseSource.TabIndex = 1
        ' 
        ' pnlCourseSourceSearch
        ' 
        pnlCourseSourceSearch.Controls.Add(btnCourseLoadExcel)
        pnlCourseSourceSearch.Controls.Add(btnCourseSourceSearch)
        pnlCourseSourceSearch.Controls.Add(dtpCourseEnd)
        pnlCourseSourceSearch.Controls.Add(dtpCourseStart)
        pnlCourseSourceSearch.Controls.Add(cboCourseLimit)
        pnlCourseSourceSearch.Controls.Add(cboCourseSourceHo)
        pnlCourseSourceSearch.Controls.Add(cboCourseSourceDong)
        pnlCourseSourceSearch.Controls.Add(txtCourseSourceSearchName)
        pnlCourseSourceSearch.Dock = DockStyle.Top
        pnlCourseSourceSearch.Location = New Point(10, 26)
        pnlCourseSourceSearch.Name = "pnlCourseSourceSearch"
        pnlCourseSourceSearch.Size = New Size(1246, 40)
        pnlCourseSourceSearch.TabIndex = 0
        ' 
        ' btnCourseLoadExcel
        ' 
        btnCourseLoadExcel.BackColor = Color.FromArgb(CByte(230), CByte(126), CByte(34))
        btnCourseLoadExcel.BorderRadius = 15
        btnCourseLoadExcel.CustomBaseColor = Color.FromArgb(CByte(230), CByte(126), CByte(34))
        btnCourseLoadExcel.CustomBorderColor = Color.Transparent
        btnCourseLoadExcel.CustomHoverColor = Color.FromArgb(CByte(243), CByte(156), CByte(18))
        btnCourseLoadExcel.FlatAppearance.BorderSize = 0
        btnCourseLoadExcel.FlatStyle = FlatStyle.Flat
        btnCourseLoadExcel.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold)
        btnCourseLoadExcel.ForeColor = Color.White
        btnCourseLoadExcel.Location = New Point(800, 6)
        btnCourseLoadExcel.Name = "btnCourseLoadExcel"
        btnCourseLoadExcel.Size = New Size(124, 27)
        btnCourseLoadExcel.TabIndex = 6
        btnCourseLoadExcel.Text = "엑셀 불러오기"
        btnCourseLoadExcel.UseVisualStyleBackColor = False
        btnCourseLoadExcel.Visible = False
        ' 
        ' btnCourseSourceSearch
        ' 
        btnCourseSourceSearch.BackColor = Color.FromArgb(CByte(65), CByte(72), CByte(104))
        btnCourseSourceSearch.BorderRadius = 15
        btnCourseSourceSearch.CustomBaseColor = Color.FromArgb(CByte(65), CByte(72), CByte(104))
        btnCourseSourceSearch.CustomBorderColor = Color.Transparent
        btnCourseSourceSearch.CustomHoverColor = Color.FromArgb(CByte(86), CByte(95), CByte(137))
        btnCourseSourceSearch.FlatAppearance.BorderSize = 0
        btnCourseSourceSearch.FlatStyle = FlatStyle.Flat
        btnCourseSourceSearch.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        btnCourseSourceSearch.ForeColor = Color.White
        btnCourseSourceSearch.Location = New Point(580, 6)
        btnCourseSourceSearch.Name = "btnCourseSourceSearch"
        btnCourseSourceSearch.Size = New Size(75, 27)
        btnCourseSourceSearch.TabIndex = 0
        btnCourseSourceSearch.Text = "조회"
        btnCourseSourceSearch.UseVisualStyleBackColor = False
        ' 
        ' dtpCourseEnd
        ' 
        dtpCourseEnd.Format = DateTimePickerFormat.Short
        dtpCourseEnd.Location = New Point(460, 8)
        dtpCourseEnd.Name = "dtpCourseEnd"
        dtpCourseEnd.Size = New Size(100, 23)
        dtpCourseEnd.TabIndex = 7
        ' 
        ' dtpCourseStart
        ' 
        dtpCourseStart.Format = DateTimePickerFormat.Short
        dtpCourseStart.Location = New Point(345, 8)
        dtpCourseStart.Name = "dtpCourseStart"
        dtpCourseStart.Size = New Size(100, 23)
        dtpCourseStart.TabIndex = 6
        ' 
        ' cboCourseLimit
        ' 
        cboCourseLimit.DropDownStyle = ComboBoxStyle.DropDownList
        cboCourseLimit.Items.AddRange(New Object() {"50", "100", "300", "500", "1000"})
        cboCourseLimit.Location = New Point(265, 8)
        cboCourseLimit.Name = "cboCourseLimit"
        cboCourseLimit.Size = New Size(70, 23)
        cboCourseLimit.TabIndex = 5
        ' 
        ' cboCourseSourceHo
        ' 
        cboCourseSourceHo.DropDownStyle = ComboBoxStyle.DropDownList
        cboCourseSourceHo.FormattingEnabled = True
        cboCourseSourceHo.Location = New Point(185, 8)
        cboCourseSourceHo.Name = "cboCourseSourceHo"
        cboCourseSourceHo.Size = New Size(70, 23)
        cboCourseSourceHo.TabIndex = 4
        ' 
        ' cboCourseSourceDong
        ' 
        cboCourseSourceDong.DropDownStyle = ComboBoxStyle.DropDownList
        cboCourseSourceDong.FormattingEnabled = True
        cboCourseSourceDong.Location = New Point(110, 8)
        cboCourseSourceDong.Name = "cboCourseSourceDong"
        cboCourseSourceDong.Size = New Size(70, 23)
        cboCourseSourceDong.TabIndex = 3
        ' 
        ' txtCourseSourceSearchName
        ' 
        txtCourseSourceSearchName.Location = New Point(5, 8)
        txtCourseSourceSearchName.Name = "txtCourseSourceSearchName"
        txtCourseSourceSearchName.PlaceholderText = "상품명 검색"
        txtCourseSourceSearchName.Size = New Size(100, 23)
        txtCourseSourceSearchName.TabIndex = 2
        ' 
        ' pnlCourseCenterAction
        ' 
        pnlCourseCenterAction.Controls.Add(btnMigrateCourse)
        pnlCourseCenterAction.Dock = DockStyle.Bottom
        pnlCourseCenterAction.Location = New Point(0, 360)
        pnlCourseCenterAction.Name = "pnlCourseCenterAction"
        pnlCourseCenterAction.Size = New Size(1266, 60)
        pnlCourseCenterAction.TabIndex = 1
        ' 
        ' btnMigrateCourse
        ' 
        btnMigrateCourse.Anchor = AnchorStyles.Top
        btnMigrateCourse.BackColor = Color.FromArgb(CByte(122), CByte(162), CByte(247))
        btnMigrateCourse.BorderRadius = 15
        btnMigrateCourse.CustomBaseColor = Color.FromArgb(CByte(65), CByte(72), CByte(104))
        btnMigrateCourse.CustomBorderColor = Color.Transparent
        btnMigrateCourse.CustomHoverColor = Color.FromArgb(CByte(86), CByte(95), CByte(137))
        btnMigrateCourse.FlatAppearance.BorderSize = 0
        btnMigrateCourse.FlatStyle = FlatStyle.Flat
        btnMigrateCourse.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold)
        btnMigrateCourse.ForeColor = Color.White
        btnMigrateCourse.Location = New Point(553, 12)
        btnMigrateCourse.Name = "btnMigrateCourse"
        btnMigrateCourse.Size = New Size(160, 35)
        btnMigrateCourse.TabIndex = 0
        btnMigrateCourse.Text = "강좌 이관하기"
        btnMigrateCourse.UseVisualStyleBackColor = False
        ' 
        ' grpCourseTarget
        ' 
        grpCourseTarget.Controls.Add(pnlCourseTargetPagination)
        grpCourseTarget.Controls.Add(dgvCourseTarget)
        grpCourseTarget.Controls.Add(pnlCourseTargetSearch)
        grpCourseTarget.Dock = DockStyle.Fill
        grpCourseTarget.Location = New Point(0, 0)
        grpCourseTarget.Name = "grpCourseTarget"
        grpCourseTarget.Padding = New Padding(10)
        grpCourseTarget.Size = New Size(1266, 307)
        grpCourseTarget.TabIndex = 0
        grpCourseTarget.TabStop = False
        grpCourseTarget.Text = "New DB (Target) - 강좌내역"
        ' 
        ' pnlCourseTargetPagination
        ' 
        pnlCourseTargetPagination.AutoSize = True
        pnlCourseTargetPagination.BackColor = Color.Transparent
        pnlCourseTargetPagination.Dock = DockStyle.Bottom
        pnlCourseTargetPagination.Location = New Point(10, 287)
        pnlCourseTargetPagination.Name = "pnlCourseTargetPagination"
        pnlCourseTargetPagination.Padding = New Padding(0, 10, 0, 0)
        pnlCourseTargetPagination.Size = New Size(1246, 10)
        pnlCourseTargetPagination.TabIndex = 2
        ' 
        ' dgvCourseTarget
        ' 
        dgvCourseTarget.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvCourseTarget.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        dgvCourseTarget.BackgroundColor = Color.FromArgb(CByte(26), CByte(27), CByte(38))
        dgvCourseTarget.Location = New Point(10, 70)
        dgvCourseTarget.Name = "dgvCourseTarget"
        dgvCourseTarget.Size = New Size(1246, 185)
        dgvCourseTarget.TabIndex = 1
        ' 
        ' pnlCourseTargetSearch
        ' 
        pnlCourseTargetSearch.Controls.Add(btnCourseTargetSearch)
        pnlCourseTargetSearch.Controls.Add(txtCourseTargetSearchName)
        pnlCourseTargetSearch.Controls.Add(cboCourseLimitTarget)
        pnlCourseTargetSearch.Controls.Add(btnInitTargetCourse)
        pnlCourseTargetSearch.Dock = DockStyle.Top
        pnlCourseTargetSearch.Location = New Point(10, 26)
        pnlCourseTargetSearch.Name = "pnlCourseTargetSearch"
        pnlCourseTargetSearch.Size = New Size(1246, 40)
        pnlCourseTargetSearch.TabIndex = 0
        ' 
        ' btnCourseTargetSearch
        ' 
        btnCourseTargetSearch.BackColor = Color.FromArgb(CByte(65), CByte(72), CByte(104))
        btnCourseTargetSearch.BorderRadius = 15
        btnCourseTargetSearch.CustomBaseColor = Color.FromArgb(CByte(65), CByte(72), CByte(104))
        btnCourseTargetSearch.CustomBorderColor = Color.Transparent
        btnCourseTargetSearch.CustomHoverColor = Color.FromArgb(CByte(86), CByte(95), CByte(137))
        btnCourseTargetSearch.FlatAppearance.BorderSize = 0
        btnCourseTargetSearch.FlatStyle = FlatStyle.Flat
        btnCourseTargetSearch.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        btnCourseTargetSearch.ForeColor = Color.White
        btnCourseTargetSearch.Location = New Point(110, 6)
        btnCourseTargetSearch.Name = "btnCourseTargetSearch"
        btnCourseTargetSearch.Size = New Size(75, 27)
        btnCourseTargetSearch.TabIndex = 0
        btnCourseTargetSearch.Text = "조회"
        btnCourseTargetSearch.UseVisualStyleBackColor = False
        ' 
        ' txtCourseTargetSearchName
        ' 
        txtCourseTargetSearchName.Location = New Point(5, 8)
        txtCourseTargetSearchName.Name = "txtCourseTargetSearchName"
        txtCourseTargetSearchName.PlaceholderText = "회원명 검색"
        txtCourseTargetSearchName.Size = New Size(100, 23)
        txtCourseTargetSearchName.TabIndex = 1
        ' 
        ' cboCourseLimitTarget
        ' 
        cboCourseLimitTarget.DropDownStyle = ComboBoxStyle.DropDownList
        cboCourseLimitTarget.Items.AddRange(New Object() {"50", "100", "300", "500", "1000"})
        cboCourseLimitTarget.Location = New Point(190, 8)
        cboCourseLimitTarget.Name = "cboCourseLimitTarget"
        cboCourseLimitTarget.Size = New Size(70, 23)
        cboCourseLimitTarget.TabIndex = 2
        ' 
        ' btnInitTargetCourse
        ' 
        btnInitTargetCourse.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnInitTargetCourse.BackColor = Color.Transparent
        btnInitTargetCourse.BorderRadius = 15
        btnInitTargetCourse.CustomBaseColor = Color.FromArgb(CByte(65), CByte(72), CByte(104))
        btnInitTargetCourse.CustomBorderColor = Color.Transparent
        btnInitTargetCourse.CustomHoverColor = Color.FromArgb(CByte(86), CByte(95), CByte(137))
        btnInitTargetCourse.FlatStyle = FlatStyle.Flat
        btnInitTargetCourse.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        btnInitTargetCourse.ForeColor = Color.White
        btnInitTargetCourse.Location = New Point(1060, 6)
        btnInitTargetCourse.Name = "btnInitTargetCourse"
        btnInitTargetCourse.Size = New Size(180, 27)
        btnInitTargetCourse.TabIndex = 3
        btnInitTargetCourse.Text = "강좌 초기화(데이터 삭제)"
        btnInitTargetCourse.UseVisualStyleBackColor = False
        ' 
        ' tabLocker
        ' 
        tabLocker.BackColor = Color.FromArgb(CByte(26), CByte(27), CByte(38))
        tabLocker.Controls.Add(splitContainerLocker)
        tabLocker.Location = New Point(4, 30)
        tabLocker.Name = "tabLocker"
        tabLocker.Padding = New Padding(3)
        tabLocker.Size = New Size(1272, 737)
        tabLocker.TabIndex = 2
        tabLocker.Text = "사물함 이관"
        ' 
        ' splitContainerLocker
        ' 
        splitContainerLocker.Dock = DockStyle.Fill
        splitContainerLocker.Location = New Point(3, 3)
        splitContainerLocker.Name = "splitContainerLocker"
        splitContainerLocker.Orientation = Orientation.Horizontal
        ' 
        ' splitContainerLocker.Panel1
        ' 
        splitContainerLocker.Panel1.Controls.Add(grpLockerSource)
        splitContainerLocker.Panel1.Controls.Add(pnlLockerCenterAction)
        ' 
        ' splitContainerLocker.Panel2
        ' 
        splitContainerLocker.Panel2.Controls.Add(grpLockerTarget)
        splitContainerLocker.Size = New Size(1266, 731)
        splitContainerLocker.SplitterDistance = 420
        splitContainerLocker.TabIndex = 0
        ' 
        ' grpLockerSource
        ' 
        grpLockerSource.Controls.Add(pnlLockerSourcePagination)
        grpLockerSource.Controls.Add(dgvLockerSource)
        grpLockerSource.Controls.Add(pnlLockerSourceSearch)
        grpLockerSource.Dock = DockStyle.Fill
        grpLockerSource.Location = New Point(0, 0)
        grpLockerSource.Name = "grpLockerSource"
        grpLockerSource.Padding = New Padding(10)
        grpLockerSource.Size = New Size(1266, 360)
        grpLockerSource.TabIndex = 0
        grpLockerSource.TabStop = False
        grpLockerSource.Text = "Old DB (Source) - 사물함내역"
        ' 
        ' pnlLockerSourcePagination
        ' 
        pnlLockerSourcePagination.AutoSize = True
        pnlLockerSourcePagination.BackColor = Color.Transparent
        pnlLockerSourcePagination.Dock = DockStyle.Bottom
        pnlLockerSourcePagination.Location = New Point(10, 340)
        pnlLockerSourcePagination.Name = "pnlLockerSourcePagination"
        pnlLockerSourcePagination.Padding = New Padding(0, 10, 0, 0)
        pnlLockerSourcePagination.Size = New Size(1246, 10)
        pnlLockerSourcePagination.TabIndex = 2
        ' 
        ' dgvLockerSource
        ' 
        dgvLockerSource.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvLockerSource.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        dgvLockerSource.BackgroundColor = Color.FromArgb(CByte(26), CByte(27), CByte(38))
        dgvLockerSource.Location = New Point(10, 70)
        dgvLockerSource.Name = "dgvLockerSource"
        dgvLockerSource.Size = New Size(1246, 235)
        dgvLockerSource.TabIndex = 1
        ' 
        ' pnlLockerSourceSearch
        ' 
        pnlLockerSourceSearch.Controls.Add(btnLockerLoadExcel)
        pnlLockerSourceSearch.Controls.Add(btnLockerSourceSearch)
        pnlLockerSourceSearch.Controls.Add(cboLockerLimit)
        pnlLockerSourceSearch.Controls.Add(cboLockerSourceHo)
        pnlLockerSourceSearch.Controls.Add(cboLockerSourceDong)
        pnlLockerSourceSearch.Controls.Add(txtLockerSourceSearchName)
        pnlLockerSourceSearch.Dock = DockStyle.Top
        pnlLockerSourceSearch.Location = New Point(10, 26)
        pnlLockerSourceSearch.Name = "pnlLockerSourceSearch"
        pnlLockerSourceSearch.Size = New Size(1246, 40)
        pnlLockerSourceSearch.TabIndex = 0
        ' 
        ' btnLockerLoadExcel
        ' 
        btnLockerLoadExcel.BackColor = Color.FromArgb(CByte(230), CByte(126), CByte(34))
        btnLockerLoadExcel.BorderRadius = 15
        btnLockerLoadExcel.CustomBaseColor = Color.FromArgb(CByte(230), CByte(126), CByte(34))
        btnLockerLoadExcel.CustomBorderColor = Color.Transparent
        btnLockerLoadExcel.CustomHoverColor = Color.FromArgb(CByte(243), CByte(156), CByte(18))
        btnLockerLoadExcel.FlatAppearance.BorderSize = 0
        btnLockerLoadExcel.FlatStyle = FlatStyle.Flat
        btnLockerLoadExcel.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold)
        btnLockerLoadExcel.ForeColor = Color.White
        btnLockerLoadExcel.Location = New Point(480, 6)
        btnLockerLoadExcel.Name = "btnLockerLoadExcel"
        btnLockerLoadExcel.Size = New Size(124, 27)
        btnLockerLoadExcel.TabIndex = 6
        btnLockerLoadExcel.Text = "엑셀 불러오기"
        btnLockerLoadExcel.UseVisualStyleBackColor = False
        btnLockerLoadExcel.Visible = False
        ' 
        ' btnLockerSourceSearch
        ' 
        btnLockerSourceSearch.BackColor = Color.FromArgb(CByte(65), CByte(72), CByte(104))
        btnLockerSourceSearch.BorderRadius = 15
        btnLockerSourceSearch.CustomBaseColor = Color.FromArgb(CByte(65), CByte(72), CByte(104))
        btnLockerSourceSearch.CustomBorderColor = Color.Transparent
        btnLockerSourceSearch.CustomHoverColor = Color.FromArgb(CByte(86), CByte(95), CByte(137))
        btnLockerSourceSearch.FlatAppearance.BorderSize = 0
        btnLockerSourceSearch.FlatStyle = FlatStyle.Flat
        btnLockerSourceSearch.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        btnLockerSourceSearch.ForeColor = Color.White
        btnLockerSourceSearch.Location = New Point(355, 6)
        btnLockerSourceSearch.Name = "btnLockerSourceSearch"
        btnLockerSourceSearch.Size = New Size(75, 27)
        btnLockerSourceSearch.TabIndex = 0
        btnLockerSourceSearch.Text = "조회"
        btnLockerSourceSearch.UseVisualStyleBackColor = False
        ' 
        ' cboLockerLimit
        ' 
        cboLockerLimit.DropDownStyle = ComboBoxStyle.DropDownList
        cboLockerLimit.Items.AddRange(New Object() {"50", "100", "300", "500", "1000"})
        cboLockerLimit.Location = New Point(265, 8)
        cboLockerLimit.Name = "cboLockerLimit"
        cboLockerLimit.Size = New Size(70, 23)
        cboLockerLimit.TabIndex = 5
        ' 
        ' cboLockerSourceHo
        ' 
        cboLockerSourceHo.DropDownStyle = ComboBoxStyle.DropDownList
        cboLockerSourceHo.FormattingEnabled = True
        cboLockerSourceHo.Location = New Point(185, 8)
        cboLockerSourceHo.Name = "cboLockerSourceHo"
        cboLockerSourceHo.Size = New Size(70, 23)
        cboLockerSourceHo.TabIndex = 4
        ' 
        ' cboLockerSourceDong
        ' 
        cboLockerSourceDong.DropDownStyle = ComboBoxStyle.DropDownList
        cboLockerSourceDong.FormattingEnabled = True
        cboLockerSourceDong.Location = New Point(110, 8)
        cboLockerSourceDong.Name = "cboLockerSourceDong"
        cboLockerSourceDong.Size = New Size(70, 23)
        cboLockerSourceDong.TabIndex = 3
        ' 
        ' txtLockerSourceSearchName
        ' 
        txtLockerSourceSearchName.Location = New Point(5, 8)
        txtLockerSourceSearchName.Name = "txtLockerSourceSearchName"
        txtLockerSourceSearchName.PlaceholderText = "회원명 검색"
        txtLockerSourceSearchName.Size = New Size(100, 23)
        txtLockerSourceSearchName.TabIndex = 2
        ' 
        ' pnlLockerCenterAction
        ' 
        pnlLockerCenterAction.Controls.Add(btnMigrateLocker)
        pnlLockerCenterAction.Dock = DockStyle.Bottom
        pnlLockerCenterAction.Location = New Point(0, 360)
        pnlLockerCenterAction.Name = "pnlLockerCenterAction"
        pnlLockerCenterAction.Size = New Size(1266, 60)
        pnlLockerCenterAction.TabIndex = 1
        ' 
        ' btnMigrateLocker
        ' 
        btnMigrateLocker.Anchor = AnchorStyles.Top
        btnMigrateLocker.BackColor = Color.FromArgb(CByte(122), CByte(162), CByte(247))
        btnMigrateLocker.BorderRadius = 15
        btnMigrateLocker.CustomBaseColor = Color.FromArgb(CByte(65), CByte(72), CByte(104))
        btnMigrateLocker.CustomBorderColor = Color.Transparent
        btnMigrateLocker.CustomHoverColor = Color.FromArgb(CByte(86), CByte(95), CByte(137))
        btnMigrateLocker.FlatAppearance.BorderSize = 0
        btnMigrateLocker.FlatStyle = FlatStyle.Flat
        btnMigrateLocker.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold)
        btnMigrateLocker.ForeColor = Color.White
        btnMigrateLocker.Location = New Point(553, 12)
        btnMigrateLocker.Name = "btnMigrateLocker"
        btnMigrateLocker.Size = New Size(160, 35)
        btnMigrateLocker.TabIndex = 0
        btnMigrateLocker.Text = "사물함 이관하기"
        btnMigrateLocker.UseVisualStyleBackColor = False
        ' 
        ' grpLockerTarget
        ' 
        grpLockerTarget.Controls.Add(pnlLockerTargetPagination)
        grpLockerTarget.Controls.Add(dgvLockerTarget)
        grpLockerTarget.Controls.Add(pnlLockerTargetSearch)
        grpLockerTarget.Dock = DockStyle.Fill
        grpLockerTarget.Location = New Point(0, 0)
        grpLockerTarget.Name = "grpLockerTarget"
        grpLockerTarget.Padding = New Padding(10)
        grpLockerTarget.Size = New Size(1266, 307)
        grpLockerTarget.TabIndex = 0
        grpLockerTarget.TabStop = False
        grpLockerTarget.Text = "New DB (Target) - 사물함내역"
        ' 
        ' pnlLockerTargetPagination
        ' 
        pnlLockerTargetPagination.AutoSize = True
        pnlLockerTargetPagination.BackColor = Color.Transparent
        pnlLockerTargetPagination.Dock = DockStyle.Bottom
        pnlLockerTargetPagination.Location = New Point(10, 287)
        pnlLockerTargetPagination.Name = "pnlLockerTargetPagination"
        pnlLockerTargetPagination.Padding = New Padding(0, 10, 0, 0)
        pnlLockerTargetPagination.Size = New Size(1246, 10)
        pnlLockerTargetPagination.TabIndex = 2
        ' 
        ' dgvLockerTarget
        ' 
        dgvLockerTarget.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvLockerTarget.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        dgvLockerTarget.BackgroundColor = Color.FromArgb(CByte(26), CByte(27), CByte(38))
        dgvLockerTarget.Location = New Point(10, 70)
        dgvLockerTarget.Name = "dgvLockerTarget"
        dgvLockerTarget.Size = New Size(1246, 185)
        dgvLockerTarget.TabIndex = 1
        ' 
        ' pnlLockerTargetSearch
        ' 
        pnlLockerTargetSearch.Controls.Add(btnLockerTargetSearch)
        pnlLockerTargetSearch.Controls.Add(txtLockerTargetSearchName)
        pnlLockerTargetSearch.Controls.Add(cboLockerLimitTarget)
        pnlLockerTargetSearch.Controls.Add(btnInitTargetLocker)
        pnlLockerTargetSearch.Dock = DockStyle.Top
        pnlLockerTargetSearch.Location = New Point(10, 26)
        pnlLockerTargetSearch.Name = "pnlLockerTargetSearch"
        pnlLockerTargetSearch.Size = New Size(1246, 40)
        pnlLockerTargetSearch.TabIndex = 0
        ' 
        ' btnLockerTargetSearch
        ' 
        btnLockerTargetSearch.BackColor = Color.FromArgb(CByte(65), CByte(72), CByte(104))
        btnLockerTargetSearch.BorderRadius = 15
        btnLockerTargetSearch.CustomBaseColor = Color.FromArgb(CByte(65), CByte(72), CByte(104))
        btnLockerTargetSearch.CustomBorderColor = Color.Transparent
        btnLockerTargetSearch.CustomHoverColor = Color.FromArgb(CByte(86), CByte(95), CByte(137))
        btnLockerTargetSearch.FlatAppearance.BorderSize = 0
        btnLockerTargetSearch.FlatStyle = FlatStyle.Flat
        btnLockerTargetSearch.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        btnLockerTargetSearch.ForeColor = Color.White
        btnLockerTargetSearch.Location = New Point(110, 6)
        btnLockerTargetSearch.Name = "btnLockerTargetSearch"
        btnLockerTargetSearch.Size = New Size(75, 27)
        btnLockerTargetSearch.TabIndex = 0
        btnLockerTargetSearch.Text = "조회"
        btnLockerTargetSearch.UseVisualStyleBackColor = False
        ' 
        ' txtLockerTargetSearchName
        ' 
        txtLockerTargetSearchName.Location = New Point(5, 8)
        txtLockerTargetSearchName.Name = "txtLockerTargetSearchName"
        txtLockerTargetSearchName.PlaceholderText = "회원명 검색"
        txtLockerTargetSearchName.Size = New Size(100, 23)
        txtLockerTargetSearchName.TabIndex = 1
        ' 
        ' cboLockerLimitTarget
        ' 
        cboLockerLimitTarget.DropDownStyle = ComboBoxStyle.DropDownList
        cboLockerLimitTarget.Items.AddRange(New Object() {"50", "100", "300", "500", "1000"})
        cboLockerLimitTarget.Location = New Point(190, 8)
        cboLockerLimitTarget.Name = "cboLockerLimitTarget"
        cboLockerLimitTarget.Size = New Size(70, 23)
        cboLockerLimitTarget.TabIndex = 2
        ' 
        ' btnInitTargetLocker
        ' 
        btnInitTargetLocker.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnInitTargetLocker.BackColor = Color.Transparent
        btnInitTargetLocker.BorderRadius = 15
        btnInitTargetLocker.CustomBaseColor = Color.FromArgb(CByte(65), CByte(72), CByte(104))
        btnInitTargetLocker.CustomBorderColor = Color.Transparent
        btnInitTargetLocker.CustomHoverColor = Color.FromArgb(CByte(86), CByte(95), CByte(137))
        btnInitTargetLocker.FlatStyle = FlatStyle.Flat
        btnInitTargetLocker.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        btnInitTargetLocker.ForeColor = Color.White
        btnInitTargetLocker.Location = New Point(1060, 6)
        btnInitTargetLocker.Name = "btnInitTargetLocker"
        btnInitTargetLocker.Size = New Size(180, 27)
        btnInitTargetLocker.TabIndex = 3
        btnInitTargetLocker.Text = "사물함 초기화(데이터 삭제)"
        btnInitTargetLocker.UseVisualStyleBackColor = False
        ' 
        ' tabProduct
        ' 
        tabProduct.BackColor = Color.FromArgb(CByte(26), CByte(27), CByte(38))
        tabProduct.Controls.Add(splitContainerProduct)
        tabProduct.Location = New Point(4, 30)
        tabProduct.Name = "tabProduct"
        tabProduct.Size = New Size(1272, 737)
        tabProduct.TabIndex = 3
        tabProduct.Text = "상품이관"
        ' 
        ' splitContainerProduct
        ' 
        splitContainerProduct.Dock = DockStyle.Fill
        splitContainerProduct.Location = New Point(0, 0)
        splitContainerProduct.Name = "splitContainerProduct"
        splitContainerProduct.Orientation = Orientation.Horizontal
        ' 
        ' splitContainerProduct.Panel1
        ' 
        splitContainerProduct.Panel1.Controls.Add(grpProductSource)
        splitContainerProduct.Panel1.Controls.Add(pnlProductCenterAction)
        ' 
        ' splitContainerProduct.Panel2
        ' 
        splitContainerProduct.Panel2.Controls.Add(grpProductTarget)
        splitContainerProduct.Size = New Size(1272, 737)
        splitContainerProduct.SplitterDistance = 423
        splitContainerProduct.TabIndex = 0
        ' 
        ' grpProductSource
        ' 
        grpProductSource.Controls.Add(pnlProductSourcePagination)
        grpProductSource.Controls.Add(dgvProductSource)
        grpProductSource.Controls.Add(pnlProductSourceSearch)
        grpProductSource.Dock = DockStyle.Fill
        grpProductSource.Location = New Point(0, 0)
        grpProductSource.Name = "grpProductSource"
        grpProductSource.Padding = New Padding(10)
        grpProductSource.Size = New Size(1272, 363)
        grpProductSource.TabIndex = 0
        grpProductSource.TabStop = False
        grpProductSource.Text = "Old DB (Source) - 상품내역"
        ' 
        ' pnlProductSourcePagination
        ' 
        pnlProductSourcePagination.AutoSize = True
        pnlProductSourcePagination.BackColor = Color.Transparent
        pnlProductSourcePagination.Dock = DockStyle.Bottom
        pnlProductSourcePagination.Location = New Point(10, 343)
        pnlProductSourcePagination.Name = "pnlProductSourcePagination"
        pnlProductSourcePagination.Padding = New Padding(0, 10, 0, 0)
        pnlProductSourcePagination.Size = New Size(1252, 10)
        pnlProductSourcePagination.TabIndex = 2
        ' 
        ' dgvProductSource
        ' 
        dgvProductSource.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvProductSource.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        dgvProductSource.BackgroundColor = Color.FromArgb(CByte(26), CByte(27), CByte(38))
        dgvProductSource.Location = New Point(17, 77)
        dgvProductSource.Name = "dgvProductSource"
        dgvProductSource.Size = New Size(1246, 235)
        dgvProductSource.TabIndex = 1
        ' 
        ' pnlProductSourceSearch
        ' 
        pnlProductSourceSearch.Controls.Add(btnProductLoadExcel)
        pnlProductSourceSearch.Controls.Add(btnProductSourceSearch)
        pnlProductSourceSearch.Controls.Add(dtpProductEnd)
        pnlProductSourceSearch.Controls.Add(dtpProductStart)
        pnlProductSourceSearch.Controls.Add(cboProductLimit)
        pnlProductSourceSearch.Controls.Add(cboProductSourceHo)
        pnlProductSourceSearch.Controls.Add(cboProductSourceDong)
        pnlProductSourceSearch.Controls.Add(txtProductSourceSearchName)
        pnlProductSourceSearch.Dock = DockStyle.Top
        pnlProductSourceSearch.Location = New Point(10, 26)
        pnlProductSourceSearch.Name = "pnlProductSourceSearch"
        pnlProductSourceSearch.Size = New Size(1252, 40)
        pnlProductSourceSearch.TabIndex = 0
        ' 
        ' btnProductLoadExcel
        ' 
        btnProductLoadExcel.BackColor = Color.FromArgb(CByte(230), CByte(126), CByte(34))
        btnProductLoadExcel.BorderRadius = 15
        btnProductLoadExcel.CustomBaseColor = Color.FromArgb(CByte(230), CByte(126), CByte(34))
        btnProductLoadExcel.CustomBorderColor = Color.Transparent
        btnProductLoadExcel.CustomHoverColor = Color.FromArgb(CByte(243), CByte(156), CByte(18))
        btnProductLoadExcel.FlatAppearance.BorderSize = 0
        btnProductLoadExcel.FlatStyle = FlatStyle.Flat
        btnProductLoadExcel.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold)
        btnProductLoadExcel.ForeColor = Color.White
        btnProductLoadExcel.Location = New Point(800, 6)
        btnProductLoadExcel.Name = "btnProductLoadExcel"
        btnProductLoadExcel.Size = New Size(124, 27)
        btnProductLoadExcel.TabIndex = 6
        btnProductLoadExcel.Text = "엑셀 불러오기"
        btnProductLoadExcel.UseVisualStyleBackColor = False
        btnProductLoadExcel.Visible = False
        ' 
        ' btnProductSourceSearch
        ' 
        btnProductSourceSearch.BackColor = Color.FromArgb(CByte(65), CByte(72), CByte(104))
        btnProductSourceSearch.BorderRadius = 15
        btnProductSourceSearch.CustomBaseColor = Color.FromArgb(CByte(65), CByte(72), CByte(104))
        btnProductSourceSearch.CustomBorderColor = Color.Transparent
        btnProductSourceSearch.CustomHoverColor = Color.FromArgb(CByte(86), CByte(95), CByte(137))
        btnProductSourceSearch.FlatAppearance.BorderSize = 0
        btnProductSourceSearch.FlatStyle = FlatStyle.Flat
        btnProductSourceSearch.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        btnProductSourceSearch.ForeColor = Color.White
        btnProductSourceSearch.Location = New Point(580, 6)
        btnProductSourceSearch.Name = "btnProductSourceSearch"
        btnProductSourceSearch.Size = New Size(75, 27)
        btnProductSourceSearch.TabIndex = 0
        btnProductSourceSearch.Text = "조회"
        btnProductSourceSearch.UseVisualStyleBackColor = False
        ' 
        ' dtpProductEnd
        ' 
        dtpProductEnd.Format = DateTimePickerFormat.Short
        dtpProductEnd.Location = New Point(460, 8)
        dtpProductEnd.Name = "dtpProductEnd"
        dtpProductEnd.Size = New Size(100, 23)
        dtpProductEnd.TabIndex = 7
        ' 
        ' dtpProductStart
        ' 
        dtpProductStart.Format = DateTimePickerFormat.Short
        dtpProductStart.Location = New Point(345, 8)
        dtpProductStart.Name = "dtpProductStart"
        dtpProductStart.Size = New Size(100, 23)
        dtpProductStart.TabIndex = 6
        ' 
        ' cboProductLimit
        ' 
        cboProductLimit.DropDownStyle = ComboBoxStyle.DropDownList
        cboProductLimit.Items.AddRange(New Object() {"50", "100", "300", "500", "1000"})
        cboProductLimit.Location = New Point(265, 8)
        cboProductLimit.Name = "cboProductLimit"
        cboProductLimit.Size = New Size(70, 23)
        cboProductLimit.TabIndex = 5
        ' 
        ' cboProductSourceHo
        ' 
        cboProductSourceHo.DropDownStyle = ComboBoxStyle.DropDownList
        cboProductSourceHo.FormattingEnabled = True
        cboProductSourceHo.Location = New Point(185, 8)
        cboProductSourceHo.Name = "cboProductSourceHo"
        cboProductSourceHo.Size = New Size(70, 23)
        cboProductSourceHo.TabIndex = 4
        ' 
        ' cboProductSourceDong
        ' 
        cboProductSourceDong.DropDownStyle = ComboBoxStyle.DropDownList
        cboProductSourceDong.FormattingEnabled = True
        cboProductSourceDong.Location = New Point(110, 8)
        cboProductSourceDong.Name = "cboProductSourceDong"
        cboProductSourceDong.Size = New Size(70, 23)
        cboProductSourceDong.TabIndex = 3
        ' 
        ' txtProductSourceSearchName
        ' 
        txtProductSourceSearchName.Location = New Point(5, 8)
        txtProductSourceSearchName.Name = "txtProductSourceSearchName"
        txtProductSourceSearchName.PlaceholderText = "상품명 검색"
        txtProductSourceSearchName.Size = New Size(100, 23)
        txtProductSourceSearchName.TabIndex = 2
        ' 
        ' pnlProductCenterAction
        ' 
        pnlProductCenterAction.Controls.Add(btnMigrateProduct)
        pnlProductCenterAction.Dock = DockStyle.Bottom
        pnlProductCenterAction.Location = New Point(0, 363)
        pnlProductCenterAction.Name = "pnlProductCenterAction"
        pnlProductCenterAction.Size = New Size(1272, 60)
        pnlProductCenterAction.TabIndex = 1
        ' 
        ' btnMigrateProduct
        ' 
        btnMigrateProduct.Anchor = AnchorStyles.Top
        btnMigrateProduct.BackColor = Color.FromArgb(CByte(122), CByte(162), CByte(247))
        btnMigrateProduct.BorderRadius = 15
        btnMigrateProduct.CustomBaseColor = Color.FromArgb(CByte(65), CByte(72), CByte(104))
        btnMigrateProduct.CustomBorderColor = Color.Transparent
        btnMigrateProduct.CustomHoverColor = Color.FromArgb(CByte(86), CByte(95), CByte(137))
        btnMigrateProduct.FlatAppearance.BorderSize = 0
        btnMigrateProduct.FlatStyle = FlatStyle.Flat
        btnMigrateProduct.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold)
        btnMigrateProduct.ForeColor = Color.White
        btnMigrateProduct.Location = New Point(553, 12)
        btnMigrateProduct.Name = "btnMigrateProduct"
        btnMigrateProduct.Size = New Size(160, 35)
        btnMigrateProduct.TabIndex = 0
        btnMigrateProduct.Text = "상품 이관하기"
        btnMigrateProduct.UseVisualStyleBackColor = False
        ' 
        ' grpProductTarget
        ' 
        grpProductTarget.Controls.Add(pnlProductTargetPagination)
        grpProductTarget.Controls.Add(dgvProductTarget)
        grpProductTarget.Controls.Add(pnlProductTargetSearch)
        grpProductTarget.Dock = DockStyle.Fill
        grpProductTarget.Location = New Point(0, 0)
        grpProductTarget.Name = "grpProductTarget"
        grpProductTarget.Padding = New Padding(10)
        grpProductTarget.Size = New Size(1272, 310)
        grpProductTarget.TabIndex = 0
        grpProductTarget.TabStop = False
        grpProductTarget.Text = "New DB (Target) - 상품내역"
        ' 
        ' pnlProductTargetPagination
        ' 
        pnlProductTargetPagination.AutoSize = True
        pnlProductTargetPagination.BackColor = Color.Transparent
        pnlProductTargetPagination.Dock = DockStyle.Bottom
        pnlProductTargetPagination.Location = New Point(10, 290)
        pnlProductTargetPagination.Name = "pnlProductTargetPagination"
        pnlProductTargetPagination.Padding = New Padding(0, 10, 0, 0)
        pnlProductTargetPagination.Size = New Size(1252, 10)
        pnlProductTargetPagination.TabIndex = 2
        ' 
        ' dgvProductTarget
        ' 
        dgvProductTarget.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvProductTarget.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        dgvProductTarget.BackgroundColor = Color.FromArgb(CByte(26), CByte(27), CByte(38))
        dgvProductTarget.Location = New Point(17, 77)
        dgvProductTarget.Name = "dgvProductTarget"
        dgvProductTarget.Size = New Size(1246, 185)
        dgvProductTarget.TabIndex = 1
        ' 
        ' pnlProductTargetSearch
        ' 
        pnlProductTargetSearch.Controls.Add(btnProductTargetSearch)
        pnlProductTargetSearch.Controls.Add(txtProductTargetSearchName)
        pnlProductTargetSearch.Controls.Add(cboProductLimitTarget)
        pnlProductTargetSearch.Controls.Add(btnInitTargetProduct)
        pnlProductTargetSearch.Dock = DockStyle.Top
        pnlProductTargetSearch.Location = New Point(10, 26)
        pnlProductTargetSearch.Name = "pnlProductTargetSearch"
        pnlProductTargetSearch.Size = New Size(1252, 40)
        pnlProductTargetSearch.TabIndex = 0
        ' 
        ' btnProductTargetSearch
        ' 
        btnProductTargetSearch.BackColor = Color.FromArgb(CByte(65), CByte(72), CByte(104))
        btnProductTargetSearch.BorderRadius = 15
        btnProductTargetSearch.CustomBaseColor = Color.FromArgb(CByte(65), CByte(72), CByte(104))
        btnProductTargetSearch.CustomBorderColor = Color.Transparent
        btnProductTargetSearch.CustomHoverColor = Color.FromArgb(CByte(86), CByte(95), CByte(137))
        btnProductTargetSearch.FlatAppearance.BorderSize = 0
        btnProductTargetSearch.FlatStyle = FlatStyle.Flat
        btnProductTargetSearch.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        btnProductTargetSearch.ForeColor = Color.White
        btnProductTargetSearch.Location = New Point(110, 6)
        btnProductTargetSearch.Name = "btnProductTargetSearch"
        btnProductTargetSearch.Size = New Size(75, 27)
        btnProductTargetSearch.TabIndex = 0
        btnProductTargetSearch.Text = "조회"
        btnProductTargetSearch.UseVisualStyleBackColor = False
        ' 
        ' txtProductTargetSearchName
        ' 
        txtProductTargetSearchName.Location = New Point(5, 8)
        txtProductTargetSearchName.Name = "txtProductTargetSearchName"
        txtProductTargetSearchName.PlaceholderText = "상품명 검색"
        txtProductTargetSearchName.Size = New Size(100, 23)
        txtProductTargetSearchName.TabIndex = 1
        ' 
        ' cboProductLimitTarget
        ' 
        cboProductLimitTarget.DropDownStyle = ComboBoxStyle.DropDownList
        cboProductLimitTarget.Items.AddRange(New Object() {"50", "100", "300", "500", "1000"})
        cboProductLimitTarget.Location = New Point(190, 8)
        cboProductLimitTarget.Name = "cboProductLimitTarget"
        cboProductLimitTarget.Size = New Size(70, 23)
        cboProductLimitTarget.TabIndex = 2
        ' 
        ' btnInitTargetProduct
        ' 
        btnInitTargetProduct.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnInitTargetProduct.BackColor = Color.Transparent
        btnInitTargetProduct.BorderRadius = 15
        btnInitTargetProduct.CustomBaseColor = Color.FromArgb(CByte(65), CByte(72), CByte(104))
        btnInitTargetProduct.CustomBorderColor = Color.Transparent
        btnInitTargetProduct.CustomHoverColor = Color.FromArgb(CByte(86), CByte(95), CByte(137))
        btnInitTargetProduct.FlatStyle = FlatStyle.Flat
        btnInitTargetProduct.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        btnInitTargetProduct.ForeColor = Color.White
        btnInitTargetProduct.Location = New Point(2112, 6)
        btnInitTargetProduct.Name = "btnInitTargetProduct"
        btnInitTargetProduct.Size = New Size(180, 27)
        btnInitTargetProduct.TabIndex = 3
        btnInitTargetProduct.Text = "상품 초기화(데이터 삭제)"
        btnInitTargetProduct.UseVisualStyleBackColor = False
        ' 
        ' tabGeneralFacility
        ' 
        tabGeneralFacility.BackColor = Color.FromArgb(CByte(26), CByte(27), CByte(38))
        tabGeneralFacility.Controls.Add(splitContainerGeneral)
        tabGeneralFacility.Location = New Point(4, 30)
        tabGeneralFacility.Name = "tabGeneralFacility"
        tabGeneralFacility.Size = New Size(1272, 737)
        tabGeneralFacility.TabIndex = 4
        tabGeneralFacility.Text = "일반시설이관"
        ' 
        ' splitContainerGeneral
        ' 
        splitContainerGeneral.Dock = DockStyle.Fill
        splitContainerGeneral.Location = New Point(0, 0)
        splitContainerGeneral.Name = "splitContainerGeneral"
        splitContainerGeneral.Orientation = Orientation.Horizontal
        ' 
        ' splitContainerGeneral.Panel1
        ' 
        splitContainerGeneral.Panel1.Controls.Add(grpGeneralSource)
        splitContainerGeneral.Panel1.Controls.Add(pnlGeneralCenterAction)
        ' 
        ' splitContainerGeneral.Panel2
        ' 
        splitContainerGeneral.Panel2.Controls.Add(grpGeneralTarget)
        splitContainerGeneral.Size = New Size(1272, 737)
        splitContainerGeneral.SplitterDistance = 423
        splitContainerGeneral.TabIndex = 0
        ' 
        ' grpGeneralSource
        ' 
        grpGeneralSource.Controls.Add(pnlGeneralSourcePagination)
        grpGeneralSource.Controls.Add(dgvGeneralSource)
        grpGeneralSource.Controls.Add(pnlGeneralSourceSearch)
        grpGeneralSource.Dock = DockStyle.Fill
        grpGeneralSource.Location = New Point(0, 0)
        grpGeneralSource.Name = "grpGeneralSource"
        grpGeneralSource.Padding = New Padding(10)
        grpGeneralSource.Size = New Size(1272, 363)
        grpGeneralSource.TabIndex = 0
        grpGeneralSource.TabStop = False
        grpGeneralSource.Text = "Old DB (Source) - 일반시설파악"
        ' 
        ' pnlGeneralSourcePagination
        ' 
        pnlGeneralSourcePagination.AutoSize = True
        pnlGeneralSourcePagination.BackColor = Color.Transparent
        pnlGeneralSourcePagination.Dock = DockStyle.Bottom
        pnlGeneralSourcePagination.Location = New Point(10, 343)
        pnlGeneralSourcePagination.Name = "pnlGeneralSourcePagination"
        pnlGeneralSourcePagination.Padding = New Padding(0, 10, 0, 0)
        pnlGeneralSourcePagination.Size = New Size(1252, 10)
        pnlGeneralSourcePagination.TabIndex = 2
        ' 
        ' dgvGeneralSource
        ' 
        dgvGeneralSource.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvGeneralSource.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        dgvGeneralSource.BackgroundColor = Color.FromArgb(CByte(26), CByte(27), CByte(38))
        dgvGeneralSource.Location = New Point(17, 77)
        dgvGeneralSource.Name = "dgvGeneralSource"
        dgvGeneralSource.Size = New Size(1246, 235)
        dgvGeneralSource.TabIndex = 1
        ' 
        ' pnlGeneralSourceSearch
        ' 
        pnlGeneralSourceSearch.Controls.Add(btnGeneralLoadExcel)
        pnlGeneralSourceSearch.Controls.Add(btnGeneralSourceSearch)
        pnlGeneralSourceSearch.Controls.Add(dtpGeneralEnd)
        pnlGeneralSourceSearch.Controls.Add(dtpGeneralStart)
        pnlGeneralSourceSearch.Controls.Add(cboGeneralLimit)
        pnlGeneralSourceSearch.Controls.Add(cboGeneralSourceHo)
        pnlGeneralSourceSearch.Controls.Add(cboGeneralSourceDong)
        pnlGeneralSourceSearch.Controls.Add(txtGeneralSourceSearchName)
        pnlGeneralSourceSearch.Dock = DockStyle.Top
        pnlGeneralSourceSearch.Location = New Point(10, 26)
        pnlGeneralSourceSearch.Name = "pnlGeneralSourceSearch"
        pnlGeneralSourceSearch.Size = New Size(1252, 40)
        pnlGeneralSourceSearch.TabIndex = 0
        ' 
        ' btnGeneralLoadExcel
        ' 
        btnGeneralLoadExcel.BackColor = Color.FromArgb(CByte(230), CByte(126), CByte(34))
        btnGeneralLoadExcel.BorderRadius = 15
        btnGeneralLoadExcel.CustomBaseColor = Color.FromArgb(CByte(230), CByte(126), CByte(34))
        btnGeneralLoadExcel.CustomBorderColor = Color.Transparent
        btnGeneralLoadExcel.CustomHoverColor = Color.FromArgb(CByte(243), CByte(156), CByte(18))
        btnGeneralLoadExcel.FlatAppearance.BorderSize = 0
        btnGeneralLoadExcel.FlatStyle = FlatStyle.Flat
        btnGeneralLoadExcel.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold)
        btnGeneralLoadExcel.ForeColor = Color.White
        btnGeneralLoadExcel.Location = New Point(800, 6)
        btnGeneralLoadExcel.Name = "btnGeneralLoadExcel"
        btnGeneralLoadExcel.Size = New Size(124, 27)
        btnGeneralLoadExcel.TabIndex = 6
        btnGeneralLoadExcel.Text = "엑셀 불러오기"
        btnGeneralLoadExcel.UseVisualStyleBackColor = False
        btnGeneralLoadExcel.Visible = False
        ' 
        ' btnGeneralSourceSearch
        ' 
        btnGeneralSourceSearch.BackColor = Color.FromArgb(CByte(65), CByte(72), CByte(104))
        btnGeneralSourceSearch.BorderRadius = 15
        btnGeneralSourceSearch.CustomBaseColor = Color.FromArgb(CByte(65), CByte(72), CByte(104))
        btnGeneralSourceSearch.CustomBorderColor = Color.Transparent
        btnGeneralSourceSearch.CustomHoverColor = Color.FromArgb(CByte(86), CByte(95), CByte(137))
        btnGeneralSourceSearch.FlatAppearance.BorderSize = 0
        btnGeneralSourceSearch.FlatStyle = FlatStyle.Flat
        btnGeneralSourceSearch.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        btnGeneralSourceSearch.ForeColor = Color.White
        btnGeneralSourceSearch.Location = New Point(580, 6)
        btnGeneralSourceSearch.Name = "btnGeneralSourceSearch"
        btnGeneralSourceSearch.Size = New Size(75, 27)
        btnGeneralSourceSearch.TabIndex = 0
        btnGeneralSourceSearch.Text = "조회"
        btnGeneralSourceSearch.UseVisualStyleBackColor = False
        ' 
        ' dtpGeneralEnd
        ' 
        dtpGeneralEnd.Format = DateTimePickerFormat.Short
        dtpGeneralEnd.Location = New Point(460, 8)
        dtpGeneralEnd.Name = "dtpGeneralEnd"
        dtpGeneralEnd.Size = New Size(100, 23)
        dtpGeneralEnd.TabIndex = 7
        ' 
        ' dtpGeneralStart
        ' 
        dtpGeneralStart.Format = DateTimePickerFormat.Short
        dtpGeneralStart.Location = New Point(345, 8)
        dtpGeneralStart.Name = "dtpGeneralStart"
        dtpGeneralStart.Size = New Size(100, 23)
        dtpGeneralStart.TabIndex = 6
        ' 
        ' cboGeneralLimit
        ' 
        cboGeneralLimit.DropDownStyle = ComboBoxStyle.DropDownList
        cboGeneralLimit.Items.AddRange(New Object() {"50", "100", "300", "500", "1000"})
        cboGeneralLimit.Location = New Point(265, 8)
        cboGeneralLimit.Name = "cboGeneralLimit"
        cboGeneralLimit.Size = New Size(70, 23)
        cboGeneralLimit.TabIndex = 5
        ' 
        ' cboGeneralSourceHo
        ' 
        cboGeneralSourceHo.DropDownStyle = ComboBoxStyle.DropDownList
        cboGeneralSourceHo.FormattingEnabled = True
        cboGeneralSourceHo.Location = New Point(185, 8)
        cboGeneralSourceHo.Name = "cboGeneralSourceHo"
        cboGeneralSourceHo.Size = New Size(70, 23)
        cboGeneralSourceHo.TabIndex = 4
        ' 
        ' cboGeneralSourceDong
        ' 
        cboGeneralSourceDong.DropDownStyle = ComboBoxStyle.DropDownList
        cboGeneralSourceDong.FormattingEnabled = True
        cboGeneralSourceDong.Location = New Point(110, 8)
        cboGeneralSourceDong.Name = "cboGeneralSourceDong"
        cboGeneralSourceDong.Size = New Size(70, 23)
        cboGeneralSourceDong.TabIndex = 3
        ' 
        ' txtGeneralSourceSearchName
        ' 
        txtGeneralSourceSearchName.Location = New Point(5, 8)
        txtGeneralSourceSearchName.Name = "txtGeneralSourceSearchName"
        txtGeneralSourceSearchName.PlaceholderText = "일반시설명 검색"
        txtGeneralSourceSearchName.Size = New Size(100, 23)
        txtGeneralSourceSearchName.TabIndex = 2
        ' 
        ' pnlGeneralCenterAction
        ' 
        pnlGeneralCenterAction.Controls.Add(btnMigrateGeneral)
        pnlGeneralCenterAction.Dock = DockStyle.Bottom
        pnlGeneralCenterAction.Location = New Point(0, 363)
        pnlGeneralCenterAction.Name = "pnlGeneralCenterAction"
        pnlGeneralCenterAction.Size = New Size(1272, 60)
        pnlGeneralCenterAction.TabIndex = 1
        ' 
        ' btnMigrateGeneral
        ' 
        btnMigrateGeneral.Anchor = AnchorStyles.Top
        btnMigrateGeneral.BackColor = Color.FromArgb(CByte(122), CByte(162), CByte(247))
        btnMigrateGeneral.BorderRadius = 15
        btnMigrateGeneral.CustomBaseColor = Color.FromArgb(CByte(65), CByte(72), CByte(104))
        btnMigrateGeneral.CustomBorderColor = Color.Transparent
        btnMigrateGeneral.CustomHoverColor = Color.FromArgb(CByte(86), CByte(95), CByte(137))
        btnMigrateGeneral.FlatAppearance.BorderSize = 0
        btnMigrateGeneral.FlatStyle = FlatStyle.Flat
        btnMigrateGeneral.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold)
        btnMigrateGeneral.ForeColor = Color.White
        btnMigrateGeneral.Location = New Point(553, 12)
        btnMigrateGeneral.Name = "btnMigrateGeneral"
        btnMigrateGeneral.Size = New Size(160, 35)
        btnMigrateGeneral.TabIndex = 0
        btnMigrateGeneral.Text = "일반시설 이관하기"
        btnMigrateGeneral.UseVisualStyleBackColor = False
        ' 
        ' grpGeneralTarget
        ' 
        grpGeneralTarget.Controls.Add(pnlGeneralTargetPagination)
        grpGeneralTarget.Controls.Add(dgvGeneralTarget)
        grpGeneralTarget.Controls.Add(pnlGeneralTargetSearch)
        grpGeneralTarget.Dock = DockStyle.Fill
        grpGeneralTarget.Location = New Point(0, 0)
        grpGeneralTarget.Name = "grpGeneralTarget"
        grpGeneralTarget.Padding = New Padding(10)
        grpGeneralTarget.Size = New Size(1272, 310)
        grpGeneralTarget.TabIndex = 0
        grpGeneralTarget.TabStop = False
        grpGeneralTarget.Text = "New DB (Target) - 일반시설내역"
        ' 
        ' pnlGeneralTargetPagination
        ' 
        pnlGeneralTargetPagination.AutoSize = True
        pnlGeneralTargetPagination.BackColor = Color.Transparent
        pnlGeneralTargetPagination.Dock = DockStyle.Bottom
        pnlGeneralTargetPagination.Location = New Point(10, 290)
        pnlGeneralTargetPagination.Name = "pnlGeneralTargetPagination"
        pnlGeneralTargetPagination.Padding = New Padding(0, 10, 0, 0)
        pnlGeneralTargetPagination.Size = New Size(1252, 10)
        pnlGeneralTargetPagination.TabIndex = 2
        ' 
        ' dgvGeneralTarget
        ' 
        dgvGeneralTarget.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvGeneralTarget.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        dgvGeneralTarget.BackgroundColor = Color.FromArgb(CByte(26), CByte(27), CByte(38))
        dgvGeneralTarget.Location = New Point(17, 77)
        dgvGeneralTarget.Name = "dgvGeneralTarget"
        dgvGeneralTarget.Size = New Size(1246, 185)
        dgvGeneralTarget.TabIndex = 1
        ' 
        ' pnlGeneralTargetSearch
        ' 
        pnlGeneralTargetSearch.Controls.Add(btnGeneralTargetSearch)
        pnlGeneralTargetSearch.Controls.Add(txtGeneralTargetSearchName)
        pnlGeneralTargetSearch.Controls.Add(cboGeneralLimitTarget)
        pnlGeneralTargetSearch.Controls.Add(btnInitTargetGeneral)
        pnlGeneralTargetSearch.Dock = DockStyle.Top
        pnlGeneralTargetSearch.Location = New Point(10, 26)
        pnlGeneralTargetSearch.Name = "pnlGeneralTargetSearch"
        pnlGeneralTargetSearch.Size = New Size(1252, 40)
        pnlGeneralTargetSearch.TabIndex = 0
        ' 
        ' btnGeneralTargetSearch
        ' 
        btnGeneralTargetSearch.BackColor = Color.FromArgb(CByte(65), CByte(72), CByte(104))
        btnGeneralTargetSearch.BorderRadius = 15
        btnGeneralTargetSearch.CustomBaseColor = Color.FromArgb(CByte(65), CByte(72), CByte(104))
        btnGeneralTargetSearch.CustomBorderColor = Color.Transparent
        btnGeneralTargetSearch.CustomHoverColor = Color.FromArgb(CByte(86), CByte(95), CByte(137))
        btnGeneralTargetSearch.FlatAppearance.BorderSize = 0
        btnGeneralTargetSearch.FlatStyle = FlatStyle.Flat
        btnGeneralTargetSearch.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        btnGeneralTargetSearch.ForeColor = Color.White
        btnGeneralTargetSearch.Location = New Point(110, 6)
        btnGeneralTargetSearch.Name = "btnGeneralTargetSearch"
        btnGeneralTargetSearch.Size = New Size(75, 27)
        btnGeneralTargetSearch.TabIndex = 0
        btnGeneralTargetSearch.Text = "조회"
        btnGeneralTargetSearch.UseVisualStyleBackColor = False
        ' 
        ' txtGeneralTargetSearchName
        ' 
        txtGeneralTargetSearchName.Location = New Point(5, 8)
        txtGeneralTargetSearchName.Name = "txtGeneralTargetSearchName"
        txtGeneralTargetSearchName.PlaceholderText = "일반시설명 검색"
        txtGeneralTargetSearchName.Size = New Size(100, 23)
        txtGeneralTargetSearchName.TabIndex = 1
        ' 
        ' cboGeneralLimitTarget
        ' 
        cboGeneralLimitTarget.DropDownStyle = ComboBoxStyle.DropDownList
        cboGeneralLimitTarget.Items.AddRange(New Object() {"50", "100", "300", "500", "1000"})
        cboGeneralLimitTarget.Location = New Point(190, 8)
        cboGeneralLimitTarget.Name = "cboGeneralLimitTarget"
        cboGeneralLimitTarget.Size = New Size(70, 23)
        cboGeneralLimitTarget.TabIndex = 2
        ' 
        ' btnInitTargetGeneral
        ' 
        btnInitTargetGeneral.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnInitTargetGeneral.BackColor = Color.Transparent
        btnInitTargetGeneral.BorderRadius = 15
        btnInitTargetGeneral.CustomBaseColor = Color.FromArgb(CByte(65), CByte(72), CByte(104))
        btnInitTargetGeneral.CustomBorderColor = Color.Transparent
        btnInitTargetGeneral.CustomHoverColor = Color.FromArgb(CByte(86), CByte(95), CByte(137))
        btnInitTargetGeneral.FlatStyle = FlatStyle.Flat
        btnInitTargetGeneral.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        btnInitTargetGeneral.ForeColor = Color.White
        btnInitTargetGeneral.Location = New Point(2112, 6)
        btnInitTargetGeneral.Name = "btnInitTargetGeneral"
        btnInitTargetGeneral.Size = New Size(180, 27)
        btnInitTargetGeneral.TabIndex = 3
        btnInitTargetGeneral.Text = "일반시설 초기화(데이터 삭제)"
        btnInitTargetGeneral.UseVisualStyleBackColor = False
        ' 
        ' tabAccommodation
        ' 
        tabAccommodation.BackColor = Color.FromArgb(CByte(26), CByte(27), CByte(38))
        tabAccommodation.Controls.Add(splitContainerAccommodation)
        tabAccommodation.Location = New Point(4, 30)
        tabAccommodation.Name = "tabAccommodation"
        tabAccommodation.Size = New Size(1272, 737)
        tabAccommodation.TabIndex = 5
        tabAccommodation.Text = "숙박시설이관"
        ' 
        ' splitContainerAccommodation
        ' 
        splitContainerAccommodation.Dock = DockStyle.Fill
        splitContainerAccommodation.Location = New Point(0, 0)
        splitContainerAccommodation.Name = "splitContainerAccommodation"
        splitContainerAccommodation.Orientation = Orientation.Horizontal
        ' 
        ' splitContainerAccommodation.Panel1
        ' 
        splitContainerAccommodation.Panel1.Controls.Add(grpAccommodationSource)
        splitContainerAccommodation.Panel1.Controls.Add(pnlAccommodationCenterAction)
        ' 
        ' splitContainerAccommodation.Panel2
        ' 
        splitContainerAccommodation.Panel2.Controls.Add(grpAccommodationTarget)
        splitContainerAccommodation.Size = New Size(1272, 737)
        splitContainerAccommodation.SplitterDistance = 423
        splitContainerAccommodation.TabIndex = 0
        ' 
        ' grpAccommodationSource
        ' 
        grpAccommodationSource.Controls.Add(pnlAccommodationSourcePagination)
        grpAccommodationSource.Controls.Add(dgvAccommodationSource)
        grpAccommodationSource.Controls.Add(pnlAccommodationSourceSearch)
        grpAccommodationSource.Dock = DockStyle.Fill
        grpAccommodationSource.Location = New Point(0, 0)
        grpAccommodationSource.Name = "grpAccommodationSource"
        grpAccommodationSource.Padding = New Padding(10)
        grpAccommodationSource.Size = New Size(1272, 363)
        grpAccommodationSource.TabIndex = 0
        grpAccommodationSource.TabStop = False
        grpAccommodationSource.Text = "Old DB (Source) - 숙박시설내역"
        ' 
        ' pnlAccommodationSourcePagination
        ' 
        pnlAccommodationSourcePagination.AutoSize = True
        pnlAccommodationSourcePagination.BackColor = Color.Transparent
        pnlAccommodationSourcePagination.Dock = DockStyle.Bottom
        pnlAccommodationSourcePagination.Location = New Point(10, 343)
        pnlAccommodationSourcePagination.Name = "pnlAccommodationSourcePagination"
        pnlAccommodationSourcePagination.Padding = New Padding(0, 10, 0, 0)
        pnlAccommodationSourcePagination.Size = New Size(1252, 10)
        pnlAccommodationSourcePagination.TabIndex = 2
        ' 
        ' dgvAccommodationSource
        ' 
        dgvAccommodationSource.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvAccommodationSource.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        dgvAccommodationSource.BackgroundColor = Color.FromArgb(CByte(26), CByte(27), CByte(38))
        dgvAccommodationSource.Location = New Point(17, 77)
        dgvAccommodationSource.Name = "dgvAccommodationSource"
        dgvAccommodationSource.Size = New Size(1246, 235)
        dgvAccommodationSource.TabIndex = 1
        ' 
        ' pnlAccommodationSourceSearch
        ' 
        pnlAccommodationSourceSearch.Controls.Add(btnAccommodationLoadExcel)
        pnlAccommodationSourceSearch.Controls.Add(btnAccommodationSourceSearch)
        pnlAccommodationSourceSearch.Controls.Add(dtpAccommodationEnd)
        pnlAccommodationSourceSearch.Controls.Add(dtpAccommodationStart)
        pnlAccommodationSourceSearch.Controls.Add(cboAccommodationLimit)
        pnlAccommodationSourceSearch.Controls.Add(cboAccommodationSourceHo)
        pnlAccommodationSourceSearch.Controls.Add(cboAccommodationSourceDong)
        pnlAccommodationSourceSearch.Controls.Add(txtAccommodationSourceSearchName)
        pnlAccommodationSourceSearch.Dock = DockStyle.Top
        pnlAccommodationSourceSearch.Location = New Point(10, 26)
        pnlAccommodationSourceSearch.Name = "pnlAccommodationSourceSearch"
        pnlAccommodationSourceSearch.Size = New Size(1252, 40)
        pnlAccommodationSourceSearch.TabIndex = 0
        ' 
        ' btnAccommodationLoadExcel
        ' 
        btnAccommodationLoadExcel.BackColor = Color.FromArgb(CByte(230), CByte(126), CByte(34))
        btnAccommodationLoadExcel.BorderRadius = 15
        btnAccommodationLoadExcel.CustomBaseColor = Color.FromArgb(CByte(230), CByte(126), CByte(34))
        btnAccommodationLoadExcel.CustomBorderColor = Color.Transparent
        btnAccommodationLoadExcel.CustomHoverColor = Color.FromArgb(CByte(243), CByte(156), CByte(18))
        btnAccommodationLoadExcel.FlatAppearance.BorderSize = 0
        btnAccommodationLoadExcel.FlatStyle = FlatStyle.Flat
        btnAccommodationLoadExcel.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold)
        btnAccommodationLoadExcel.ForeColor = Color.White
        btnAccommodationLoadExcel.Location = New Point(800, 6)
        btnAccommodationLoadExcel.Name = "btnAccommodationLoadExcel"
        btnAccommodationLoadExcel.Size = New Size(124, 27)
        btnAccommodationLoadExcel.TabIndex = 6
        btnAccommodationLoadExcel.Text = "엑셀 불러오기"
        btnAccommodationLoadExcel.UseVisualStyleBackColor = False
        btnAccommodationLoadExcel.Visible = False
        ' 
        ' btnAccommodationSourceSearch
        ' 
        btnAccommodationSourceSearch.BackColor = Color.FromArgb(CByte(65), CByte(72), CByte(104))
        btnAccommodationSourceSearch.BorderRadius = 15
        btnAccommodationSourceSearch.CustomBaseColor = Color.FromArgb(CByte(65), CByte(72), CByte(104))
        btnAccommodationSourceSearch.CustomBorderColor = Color.Transparent
        btnAccommodationSourceSearch.CustomHoverColor = Color.FromArgb(CByte(86), CByte(95), CByte(137))
        btnAccommodationSourceSearch.FlatAppearance.BorderSize = 0
        btnAccommodationSourceSearch.FlatStyle = FlatStyle.Flat
        btnAccommodationSourceSearch.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        btnAccommodationSourceSearch.ForeColor = Color.White
        btnAccommodationSourceSearch.Location = New Point(580, 6)
        btnAccommodationSourceSearch.Name = "btnAccommodationSourceSearch"
        btnAccommodationSourceSearch.Size = New Size(75, 27)
        btnAccommodationSourceSearch.TabIndex = 0
        btnAccommodationSourceSearch.Text = "조회"
        btnAccommodationSourceSearch.UseVisualStyleBackColor = False
        ' 
        ' dtpAccommodationEnd
        ' 
        dtpAccommodationEnd.Format = DateTimePickerFormat.Short
        dtpAccommodationEnd.Location = New Point(460, 8)
        dtpAccommodationEnd.Name = "dtpAccommodationEnd"
        dtpAccommodationEnd.Size = New Size(100, 23)
        dtpAccommodationEnd.TabIndex = 7
        ' 
        ' dtpAccommodationStart
        ' 
        dtpAccommodationStart.Format = DateTimePickerFormat.Short
        dtpAccommodationStart.Location = New Point(345, 8)
        dtpAccommodationStart.Name = "dtpAccommodationStart"
        dtpAccommodationStart.Size = New Size(100, 23)
        dtpAccommodationStart.TabIndex = 6
        ' 
        ' cboAccommodationLimit
        ' 
        cboAccommodationLimit.DropDownStyle = ComboBoxStyle.DropDownList
        cboAccommodationLimit.Items.AddRange(New Object() {"50", "100", "300", "500", "1000"})
        cboAccommodationLimit.Location = New Point(265, 8)
        cboAccommodationLimit.Name = "cboAccommodationLimit"
        cboAccommodationLimit.Size = New Size(70, 23)
        cboAccommodationLimit.TabIndex = 5
        ' 
        ' cboAccommodationSourceHo
        ' 
        cboAccommodationSourceHo.DropDownStyle = ComboBoxStyle.DropDownList
        cboAccommodationSourceHo.FormattingEnabled = True
        cboAccommodationSourceHo.Location = New Point(185, 8)
        cboAccommodationSourceHo.Name = "cboAccommodationSourceHo"
        cboAccommodationSourceHo.Size = New Size(70, 23)
        cboAccommodationSourceHo.TabIndex = 4
        ' 
        ' cboAccommodationSourceDong
        ' 
        cboAccommodationSourceDong.DropDownStyle = ComboBoxStyle.DropDownList
        cboAccommodationSourceDong.FormattingEnabled = True
        cboAccommodationSourceDong.Location = New Point(110, 8)
        cboAccommodationSourceDong.Name = "cboAccommodationSourceDong"
        cboAccommodationSourceDong.Size = New Size(70, 23)
        cboAccommodationSourceDong.TabIndex = 3
        ' 
        ' txtAccommodationSourceSearchName
        ' 
        txtAccommodationSourceSearchName.Location = New Point(5, 8)
        txtAccommodationSourceSearchName.Name = "txtAccommodationSourceSearchName"
        txtAccommodationSourceSearchName.PlaceholderText = "숙박시설명 검색"
        txtAccommodationSourceSearchName.Size = New Size(100, 23)
        txtAccommodationSourceSearchName.TabIndex = 2
        ' 
        ' pnlAccommodationCenterAction
        ' 
        pnlAccommodationCenterAction.Controls.Add(btnMigrateAccommodation)
        pnlAccommodationCenterAction.Dock = DockStyle.Bottom
        pnlAccommodationCenterAction.Location = New Point(0, 363)
        pnlAccommodationCenterAction.Name = "pnlAccommodationCenterAction"
        pnlAccommodationCenterAction.Size = New Size(1272, 60)
        pnlAccommodationCenterAction.TabIndex = 1
        ' 
        ' btnMigrateAccommodation
        ' 
        btnMigrateAccommodation.Anchor = AnchorStyles.Top
        btnMigrateAccommodation.BackColor = Color.FromArgb(CByte(122), CByte(162), CByte(247))
        btnMigrateAccommodation.BorderRadius = 15
        btnMigrateAccommodation.CustomBaseColor = Color.FromArgb(CByte(65), CByte(72), CByte(104))
        btnMigrateAccommodation.CustomBorderColor = Color.Transparent
        btnMigrateAccommodation.CustomHoverColor = Color.FromArgb(CByte(86), CByte(95), CByte(137))
        btnMigrateAccommodation.FlatAppearance.BorderSize = 0
        btnMigrateAccommodation.FlatStyle = FlatStyle.Flat
        btnMigrateAccommodation.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold)
        btnMigrateAccommodation.ForeColor = Color.White
        btnMigrateAccommodation.Location = New Point(553, 12)
        btnMigrateAccommodation.Name = "btnMigrateAccommodation"
        btnMigrateAccommodation.Size = New Size(160, 35)
        btnMigrateAccommodation.TabIndex = 0
        btnMigrateAccommodation.Text = "숙박시설 이관하기"
        btnMigrateAccommodation.UseVisualStyleBackColor = False
        ' 
        ' grpAccommodationTarget
        ' 
        grpAccommodationTarget.Controls.Add(pnlAccommodationTargetPagination)
        grpAccommodationTarget.Controls.Add(dgvAccommodationTarget)
        grpAccommodationTarget.Controls.Add(pnlAccommodationTargetSearch)
        grpAccommodationTarget.Dock = DockStyle.Fill
        grpAccommodationTarget.Location = New Point(0, 0)
        grpAccommodationTarget.Name = "grpAccommodationTarget"
        grpAccommodationTarget.Padding = New Padding(10)
        grpAccommodationTarget.Size = New Size(1272, 310)
        grpAccommodationTarget.TabIndex = 0
        grpAccommodationTarget.TabStop = False
        grpAccommodationTarget.Text = "New DB (Target) - 숙박시설내역"
        ' 
        ' pnlAccommodationTargetPagination
        ' 
        pnlAccommodationTargetPagination.AutoSize = True
        pnlAccommodationTargetPagination.BackColor = Color.Transparent
        pnlAccommodationTargetPagination.Dock = DockStyle.Bottom
        pnlAccommodationTargetPagination.Location = New Point(10, 290)
        pnlAccommodationTargetPagination.Name = "pnlAccommodationTargetPagination"
        pnlAccommodationTargetPagination.Padding = New Padding(0, 10, 0, 0)
        pnlAccommodationTargetPagination.Size = New Size(1252, 10)
        pnlAccommodationTargetPagination.TabIndex = 2
        ' 
        ' dgvAccommodationTarget
        ' 
        dgvAccommodationTarget.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvAccommodationTarget.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        dgvAccommodationTarget.BackgroundColor = Color.FromArgb(CByte(26), CByte(27), CByte(38))
        dgvAccommodationTarget.Location = New Point(17, 77)
        dgvAccommodationTarget.Name = "dgvAccommodationTarget"
        dgvAccommodationTarget.Size = New Size(1246, 185)
        dgvAccommodationTarget.TabIndex = 1
        ' 
        ' pnlAccommodationTargetSearch
        ' 
        pnlAccommodationTargetSearch.Controls.Add(btnAccommodationTargetSearch)
        pnlAccommodationTargetSearch.Controls.Add(txtAccommodationTargetSearchName)
        pnlAccommodationTargetSearch.Controls.Add(cboAccommodationLimitTarget)
        pnlAccommodationTargetSearch.Controls.Add(btnInitTargetAccommodation)
        pnlAccommodationTargetSearch.Dock = DockStyle.Top
        pnlAccommodationTargetSearch.Location = New Point(10, 26)
        pnlAccommodationTargetSearch.Name = "pnlAccommodationTargetSearch"
        pnlAccommodationTargetSearch.Size = New Size(1252, 40)
        pnlAccommodationTargetSearch.TabIndex = 0
        ' 
        ' btnAccommodationTargetSearch
        ' 
        btnAccommodationTargetSearch.BackColor = Color.FromArgb(CByte(65), CByte(72), CByte(104))
        btnAccommodationTargetSearch.BorderRadius = 15
        btnAccommodationTargetSearch.CustomBaseColor = Color.FromArgb(CByte(65), CByte(72), CByte(104))
        btnAccommodationTargetSearch.CustomBorderColor = Color.Transparent
        btnAccommodationTargetSearch.CustomHoverColor = Color.FromArgb(CByte(86), CByte(95), CByte(137))
        btnAccommodationTargetSearch.FlatAppearance.BorderSize = 0
        btnAccommodationTargetSearch.FlatStyle = FlatStyle.Flat
        btnAccommodationTargetSearch.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        btnAccommodationTargetSearch.ForeColor = Color.White
        btnAccommodationTargetSearch.Location = New Point(110, 6)
        btnAccommodationTargetSearch.Name = "btnAccommodationTargetSearch"
        btnAccommodationTargetSearch.Size = New Size(75, 27)
        btnAccommodationTargetSearch.TabIndex = 0
        btnAccommodationTargetSearch.Text = "조회"
        btnAccommodationTargetSearch.UseVisualStyleBackColor = False
        ' 
        ' txtAccommodationTargetSearchName
        ' 
        txtAccommodationTargetSearchName.Location = New Point(5, 8)
        txtAccommodationTargetSearchName.Name = "txtAccommodationTargetSearchName"
        txtAccommodationTargetSearchName.PlaceholderText = "숙박시설명 검색"
        txtAccommodationTargetSearchName.Size = New Size(100, 23)
        txtAccommodationTargetSearchName.TabIndex = 1
        ' 
        ' cboAccommodationLimitTarget
        ' 
        cboAccommodationLimitTarget.DropDownStyle = ComboBoxStyle.DropDownList
        cboAccommodationLimitTarget.Items.AddRange(New Object() {"50", "100", "300", "500", "1000"})
        cboAccommodationLimitTarget.Location = New Point(190, 8)
        cboAccommodationLimitTarget.Name = "cboAccommodationLimitTarget"
        cboAccommodationLimitTarget.Size = New Size(70, 23)
        cboAccommodationLimitTarget.TabIndex = 2
        ' 
        ' btnInitTargetAccommodation
        ' 
        btnInitTargetAccommodation.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnInitTargetAccommodation.BackColor = Color.Transparent
        btnInitTargetAccommodation.BorderRadius = 15
        btnInitTargetAccommodation.CustomBaseColor = Color.FromArgb(CByte(65), CByte(72), CByte(104))
        btnInitTargetAccommodation.CustomBorderColor = Color.Transparent
        btnInitTargetAccommodation.CustomHoverColor = Color.FromArgb(CByte(86), CByte(95), CByte(137))
        btnInitTargetAccommodation.FlatStyle = FlatStyle.Flat
        btnInitTargetAccommodation.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        btnInitTargetAccommodation.ForeColor = Color.White
        btnInitTargetAccommodation.Location = New Point(2112, 6)
        btnInitTargetAccommodation.Name = "btnInitTargetAccommodation"
        btnInitTargetAccommodation.Size = New Size(180, 27)
        btnInitTargetAccommodation.TabIndex = 3
        btnInitTargetAccommodation.Text = "숙박시설 초기화(데이터 삭제)"
        btnInitTargetAccommodation.UseVisualStyleBackColor = False
        ' 
        ' tabSpaceFacility
        ' 
        tabSpaceFacility.BackColor = Color.FromArgb(CByte(26), CByte(27), CByte(38))
        tabSpaceFacility.Controls.Add(splitContainerSpace)
        tabSpaceFacility.Location = New Point(4, 30)
        tabSpaceFacility.Name = "tabSpaceFacility"
        tabSpaceFacility.Size = New Size(1272, 737)
        tabSpaceFacility.TabIndex = 6
        tabSpaceFacility.Text = "공간시설이관"
        ' 
        ' splitContainerSpace
        ' 
        splitContainerSpace.Dock = DockStyle.Fill
        splitContainerSpace.Location = New Point(0, 0)
        splitContainerSpace.Name = "splitContainerSpace"
        splitContainerSpace.Orientation = Orientation.Horizontal
        ' 
        ' splitContainerSpace.Panel1
        ' 
        splitContainerSpace.Panel1.Controls.Add(grpSpaceSource)
        splitContainerSpace.Panel1.Controls.Add(pnlSpaceCenterAction)
        ' 
        ' splitContainerSpace.Panel2
        ' 
        splitContainerSpace.Panel2.Controls.Add(grpSpaceTarget)
        splitContainerSpace.Size = New Size(1272, 737)
        splitContainerSpace.SplitterDistance = 423
        splitContainerSpace.TabIndex = 0
        ' 
        ' grpSpaceSource
        ' 
        grpSpaceSource.Controls.Add(pnlSpaceSourcePagination)
        grpSpaceSource.Controls.Add(dgvSpaceSource)
        grpSpaceSource.Controls.Add(pnlSpaceSourceSearch)
        grpSpaceSource.Dock = DockStyle.Fill
        grpSpaceSource.Location = New Point(0, 0)
        grpSpaceSource.Name = "grpSpaceSource"
        grpSpaceSource.Padding = New Padding(10)
        grpSpaceSource.Size = New Size(1272, 363)
        grpSpaceSource.TabIndex = 0
        grpSpaceSource.TabStop = False
        grpSpaceSource.Text = "Old DB (Source) - 공간시설내역"
        ' 
        ' pnlSpaceSourcePagination
        ' 
        pnlSpaceSourcePagination.AutoSize = True
        pnlSpaceSourcePagination.BackColor = Color.Transparent
        pnlSpaceSourcePagination.Dock = DockStyle.Bottom
        pnlSpaceSourcePagination.Location = New Point(10, 343)
        pnlSpaceSourcePagination.Name = "pnlSpaceSourcePagination"
        pnlSpaceSourcePagination.Padding = New Padding(0, 10, 0, 0)
        pnlSpaceSourcePagination.Size = New Size(1252, 10)
        pnlSpaceSourcePagination.TabIndex = 2
        ' 
        ' dgvSpaceSource
        ' 
        dgvSpaceSource.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvSpaceSource.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        dgvSpaceSource.BackgroundColor = Color.FromArgb(CByte(26), CByte(27), CByte(38))
        dgvSpaceSource.Location = New Point(17, 77)
        dgvSpaceSource.Name = "dgvSpaceSource"
        dgvSpaceSource.Size = New Size(1246, 235)
        dgvSpaceSource.TabIndex = 1
        ' 
        ' pnlSpaceSourceSearch
        ' 
        pnlSpaceSourceSearch.Controls.Add(btnSpaceLoadExcel)
        pnlSpaceSourceSearch.Controls.Add(btnSpaceSourceSearch)
        pnlSpaceSourceSearch.Controls.Add(dtpSpaceEnd)
        pnlSpaceSourceSearch.Controls.Add(dtpSpaceStart)
        pnlSpaceSourceSearch.Controls.Add(cboSpaceLimit)
        pnlSpaceSourceSearch.Controls.Add(cboSpaceSourceHo)
        pnlSpaceSourceSearch.Controls.Add(cboSpaceSourceDong)
        pnlSpaceSourceSearch.Controls.Add(txtSpaceSourceSearchName)
        pnlSpaceSourceSearch.Dock = DockStyle.Top
        pnlSpaceSourceSearch.Location = New Point(10, 26)
        pnlSpaceSourceSearch.Name = "pnlSpaceSourceSearch"
        pnlSpaceSourceSearch.Size = New Size(1252, 40)
        pnlSpaceSourceSearch.TabIndex = 0
        ' 
        ' btnSpaceLoadExcel
        ' 
        btnSpaceLoadExcel.BackColor = Color.FromArgb(CByte(230), CByte(126), CByte(34))
        btnSpaceLoadExcel.BorderRadius = 15
        btnSpaceLoadExcel.CustomBaseColor = Color.FromArgb(CByte(230), CByte(126), CByte(34))
        btnSpaceLoadExcel.CustomBorderColor = Color.Transparent
        btnSpaceLoadExcel.CustomHoverColor = Color.FromArgb(CByte(243), CByte(156), CByte(18))
        btnSpaceLoadExcel.FlatAppearance.BorderSize = 0
        btnSpaceLoadExcel.FlatStyle = FlatStyle.Flat
        btnSpaceLoadExcel.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold)
        btnSpaceLoadExcel.ForeColor = Color.White
        btnSpaceLoadExcel.Location = New Point(800, 6)
        btnSpaceLoadExcel.Name = "btnSpaceLoadExcel"
        btnSpaceLoadExcel.Size = New Size(124, 27)
        btnSpaceLoadExcel.TabIndex = 6
        btnSpaceLoadExcel.Text = "엑셀 불러오기"
        btnSpaceLoadExcel.UseVisualStyleBackColor = False
        btnSpaceLoadExcel.Visible = False
        ' 
        ' btnSpaceSourceSearch
        ' 
        btnSpaceSourceSearch.BackColor = Color.FromArgb(CByte(65), CByte(72), CByte(104))
        btnSpaceSourceSearch.BorderRadius = 15
        btnSpaceSourceSearch.CustomBaseColor = Color.FromArgb(CByte(65), CByte(72), CByte(104))
        btnSpaceSourceSearch.CustomBorderColor = Color.Transparent
        btnSpaceSourceSearch.CustomHoverColor = Color.FromArgb(CByte(86), CByte(95), CByte(137))
        btnSpaceSourceSearch.FlatAppearance.BorderSize = 0
        btnSpaceSourceSearch.FlatStyle = FlatStyle.Flat
        btnSpaceSourceSearch.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        btnSpaceSourceSearch.ForeColor = Color.White
        btnSpaceSourceSearch.Location = New Point(580, 6)
        btnSpaceSourceSearch.Name = "btnSpaceSourceSearch"
        btnSpaceSourceSearch.Size = New Size(75, 27)
        btnSpaceSourceSearch.TabIndex = 0
        btnSpaceSourceSearch.Text = "조회"
        btnSpaceSourceSearch.UseVisualStyleBackColor = False
        ' 
        ' dtpSpaceEnd
        ' 
        dtpSpaceEnd.Format = DateTimePickerFormat.Short
        dtpSpaceEnd.Location = New Point(460, 8)
        dtpSpaceEnd.Name = "dtpSpaceEnd"
        dtpSpaceEnd.Size = New Size(100, 23)
        dtpSpaceEnd.TabIndex = 7
        ' 
        ' dtpSpaceStart
        ' 
        dtpSpaceStart.Format = DateTimePickerFormat.Short
        dtpSpaceStart.Location = New Point(345, 8)
        dtpSpaceStart.Name = "dtpSpaceStart"
        dtpSpaceStart.Size = New Size(100, 23)
        dtpSpaceStart.TabIndex = 6
        ' 
        ' cboSpaceLimit
        ' 
        cboSpaceLimit.DropDownStyle = ComboBoxStyle.DropDownList
        cboSpaceLimit.Items.AddRange(New Object() {"50", "100", "300", "500", "1000"})
        cboSpaceLimit.Location = New Point(265, 8)
        cboSpaceLimit.Name = "cboSpaceLimit"
        cboSpaceLimit.Size = New Size(70, 23)
        cboSpaceLimit.TabIndex = 5
        ' 
        ' cboSpaceSourceHo
        ' 
        cboSpaceSourceHo.DropDownStyle = ComboBoxStyle.DropDownList
        cboSpaceSourceHo.FormattingEnabled = True
        cboSpaceSourceHo.Location = New Point(185, 8)
        cboSpaceSourceHo.Name = "cboSpaceSourceHo"
        cboSpaceSourceHo.Size = New Size(70, 23)
        cboSpaceSourceHo.TabIndex = 4
        ' 
        ' cboSpaceSourceDong
        ' 
        cboSpaceSourceDong.DropDownStyle = ComboBoxStyle.DropDownList
        cboSpaceSourceDong.FormattingEnabled = True
        cboSpaceSourceDong.Location = New Point(110, 8)
        cboSpaceSourceDong.Name = "cboSpaceSourceDong"
        cboSpaceSourceDong.Size = New Size(70, 23)
        cboSpaceSourceDong.TabIndex = 3
        ' 
        ' txtSpaceSourceSearchName
        ' 
        txtSpaceSourceSearchName.Location = New Point(5, 8)
        txtSpaceSourceSearchName.Name = "txtSpaceSourceSearchName"
        txtSpaceSourceSearchName.PlaceholderText = "공간시설명 검색"
        txtSpaceSourceSearchName.Size = New Size(100, 23)
        txtSpaceSourceSearchName.TabIndex = 2
        ' 
        ' pnlSpaceCenterAction
        ' 
        pnlSpaceCenterAction.Controls.Add(btnMigrateSpace)
        pnlSpaceCenterAction.Dock = DockStyle.Bottom
        pnlSpaceCenterAction.Location = New Point(0, 363)
        pnlSpaceCenterAction.Name = "pnlSpaceCenterAction"
        pnlSpaceCenterAction.Size = New Size(1272, 60)
        pnlSpaceCenterAction.TabIndex = 1
        ' 
        ' btnMigrateSpace
        ' 
        btnMigrateSpace.Anchor = AnchorStyles.Top
        btnMigrateSpace.BackColor = Color.FromArgb(CByte(122), CByte(162), CByte(247))
        btnMigrateSpace.BorderRadius = 15
        btnMigrateSpace.CustomBaseColor = Color.FromArgb(CByte(65), CByte(72), CByte(104))
        btnMigrateSpace.CustomBorderColor = Color.Transparent
        btnMigrateSpace.CustomHoverColor = Color.FromArgb(CByte(86), CByte(95), CByte(137))
        btnMigrateSpace.FlatAppearance.BorderSize = 0
        btnMigrateSpace.FlatStyle = FlatStyle.Flat
        btnMigrateSpace.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold)
        btnMigrateSpace.ForeColor = Color.White
        btnMigrateSpace.Location = New Point(553, 12)
        btnMigrateSpace.Name = "btnMigrateSpace"
        btnMigrateSpace.Size = New Size(160, 35)
        btnMigrateSpace.TabIndex = 0
        btnMigrateSpace.Text = "공간시설 이관하기"
        btnMigrateSpace.UseVisualStyleBackColor = False
        ' 
        ' grpSpaceTarget
        ' 
        grpSpaceTarget.Controls.Add(pnlSpaceTargetPagination)
        grpSpaceTarget.Controls.Add(dgvSpaceTarget)
        grpSpaceTarget.Controls.Add(pnlSpaceTargetSearch)
        grpSpaceTarget.Dock = DockStyle.Fill
        grpSpaceTarget.Location = New Point(0, 0)
        grpSpaceTarget.Name = "grpSpaceTarget"
        grpSpaceTarget.Padding = New Padding(10)
        grpSpaceTarget.Size = New Size(1272, 310)
        grpSpaceTarget.TabIndex = 0
        grpSpaceTarget.TabStop = False
        grpSpaceTarget.Text = "New DB (Target) - 공간시설내역"
        ' 
        ' pnlSpaceTargetPagination
        ' 
        pnlSpaceTargetPagination.AutoSize = True
        pnlSpaceTargetPagination.BackColor = Color.Transparent
        pnlSpaceTargetPagination.Dock = DockStyle.Bottom
        pnlSpaceTargetPagination.Location = New Point(10, 290)
        pnlSpaceTargetPagination.Name = "pnlSpaceTargetPagination"
        pnlSpaceTargetPagination.Padding = New Padding(0, 10, 0, 0)
        pnlSpaceTargetPagination.Size = New Size(1252, 10)
        pnlSpaceTargetPagination.TabIndex = 2
        ' 
        ' dgvSpaceTarget
        ' 
        dgvSpaceTarget.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvSpaceTarget.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        dgvSpaceTarget.BackgroundColor = Color.FromArgb(CByte(26), CByte(27), CByte(38))
        dgvSpaceTarget.Location = New Point(17, 77)
        dgvSpaceTarget.Name = "dgvSpaceTarget"
        dgvSpaceTarget.Size = New Size(1246, 185)
        dgvSpaceTarget.TabIndex = 1
        ' 
        ' pnlSpaceTargetSearch
        ' 
        pnlSpaceTargetSearch.Controls.Add(btnSpaceTargetSearch)
        pnlSpaceTargetSearch.Controls.Add(txtSpaceTargetSearchName)
        pnlSpaceTargetSearch.Controls.Add(cboSpaceLimitTarget)
        pnlSpaceTargetSearch.Controls.Add(btnInitTargetSpace)
        pnlSpaceTargetSearch.Dock = DockStyle.Top
        pnlSpaceTargetSearch.Location = New Point(10, 26)
        pnlSpaceTargetSearch.Name = "pnlSpaceTargetSearch"
        pnlSpaceTargetSearch.Size = New Size(1252, 40)
        pnlSpaceTargetSearch.TabIndex = 0
        ' 
        ' btnSpaceTargetSearch
        ' 
        btnSpaceTargetSearch.BackColor = Color.FromArgb(CByte(65), CByte(72), CByte(104))
        btnSpaceTargetSearch.BorderRadius = 15
        btnSpaceTargetSearch.CustomBaseColor = Color.FromArgb(CByte(65), CByte(72), CByte(104))
        btnSpaceTargetSearch.CustomBorderColor = Color.Transparent
        btnSpaceTargetSearch.CustomHoverColor = Color.FromArgb(CByte(86), CByte(95), CByte(137))
        btnSpaceTargetSearch.FlatAppearance.BorderSize = 0
        btnSpaceTargetSearch.FlatStyle = FlatStyle.Flat
        btnSpaceTargetSearch.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        btnSpaceTargetSearch.ForeColor = Color.White
        btnSpaceTargetSearch.Location = New Point(110, 6)
        btnSpaceTargetSearch.Name = "btnSpaceTargetSearch"
        btnSpaceTargetSearch.Size = New Size(75, 27)
        btnSpaceTargetSearch.TabIndex = 0
        btnSpaceTargetSearch.Text = "조회"
        btnSpaceTargetSearch.UseVisualStyleBackColor = False
        ' 
        ' txtSpaceTargetSearchName
        ' 
        txtSpaceTargetSearchName.Location = New Point(5, 8)
        txtSpaceTargetSearchName.Name = "txtSpaceTargetSearchName"
        txtSpaceTargetSearchName.PlaceholderText = "공간시설명 검색"
        txtSpaceTargetSearchName.Size = New Size(100, 23)
        txtSpaceTargetSearchName.TabIndex = 1
        ' 
        ' cboSpaceLimitTarget
        ' 
        cboSpaceLimitTarget.DropDownStyle = ComboBoxStyle.DropDownList
        cboSpaceLimitTarget.Items.AddRange(New Object() {"50", "100", "300", "500", "1000"})
        cboSpaceLimitTarget.Location = New Point(190, 8)
        cboSpaceLimitTarget.Name = "cboSpaceLimitTarget"
        cboSpaceLimitTarget.Size = New Size(70, 23)
        cboSpaceLimitTarget.TabIndex = 2
        ' 
        ' btnInitTargetSpace
        ' 
        btnInitTargetSpace.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnInitTargetSpace.BackColor = Color.Transparent
        btnInitTargetSpace.BorderRadius = 15
        btnInitTargetSpace.CustomBaseColor = Color.FromArgb(CByte(65), CByte(72), CByte(104))
        btnInitTargetSpace.CustomBorderColor = Color.Transparent
        btnInitTargetSpace.CustomHoverColor = Color.FromArgb(CByte(86), CByte(95), CByte(137))
        btnInitTargetSpace.FlatStyle = FlatStyle.Flat
        btnInitTargetSpace.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        btnInitTargetSpace.ForeColor = Color.White
        btnInitTargetSpace.Location = New Point(2112, 6)
        btnInitTargetSpace.Name = "btnInitTargetSpace"
        btnInitTargetSpace.Size = New Size(180, 27)
        btnInitTargetSpace.TabIndex = 3
        btnInitTargetSpace.Text = "공간시설 초기화(데이터 삭제)"
        btnInitTargetSpace.UseVisualStyleBackColor = False
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
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(26), CByte(27), CByte(38))
        ClientSize = New Size(1280, 921)
        Controls.Add(tabMain)
        Controls.Add(picPreview)
        Controls.Add(rtbLog)
        Controls.Add(pnlGlobalTop)
        Font = New Font("Segoe UI", 9.0F)
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
        tabCourse.ResumeLayout(False)
        splitContainerCourse.Panel1.ResumeLayout(False)
        splitContainerCourse.Panel2.ResumeLayout(False)
        CType(splitContainerCourse, ComponentModel.ISupportInitialize).EndInit()
        splitContainerCourse.ResumeLayout(False)
        grpCourseSource.ResumeLayout(False)
        grpCourseSource.PerformLayout()
        CType(dgvCourseSource, ComponentModel.ISupportInitialize).EndInit()
        pnlCourseSourceSearch.ResumeLayout(False)
        pnlCourseSourceSearch.PerformLayout()
        pnlCourseCenterAction.ResumeLayout(False)
        grpCourseTarget.ResumeLayout(False)
        grpCourseTarget.PerformLayout()
        CType(dgvCourseTarget, ComponentModel.ISupportInitialize).EndInit()
        pnlCourseTargetSearch.ResumeLayout(False)
        pnlCourseTargetSearch.PerformLayout()
        tabLocker.ResumeLayout(False)
        splitContainerLocker.Panel1.ResumeLayout(False)
        splitContainerLocker.Panel2.ResumeLayout(False)
        CType(splitContainerLocker, ComponentModel.ISupportInitialize).EndInit()
        splitContainerLocker.ResumeLayout(False)
        grpLockerSource.ResumeLayout(False)
        grpLockerSource.PerformLayout()
        CType(dgvLockerSource, ComponentModel.ISupportInitialize).EndInit()
        pnlLockerSourceSearch.ResumeLayout(False)
        pnlLockerSourceSearch.PerformLayout()
        pnlLockerCenterAction.ResumeLayout(False)
        grpLockerTarget.ResumeLayout(False)
        grpLockerTarget.PerformLayout()
        CType(dgvLockerTarget, ComponentModel.ISupportInitialize).EndInit()
        pnlLockerTargetSearch.ResumeLayout(False)
        pnlLockerTargetSearch.PerformLayout()
        tabProduct.ResumeLayout(False)
        splitContainerProduct.Panel1.ResumeLayout(False)
        splitContainerProduct.Panel2.ResumeLayout(False)
        CType(splitContainerProduct, ComponentModel.ISupportInitialize).EndInit()
        splitContainerProduct.ResumeLayout(False)
        grpProductSource.ResumeLayout(False)
        grpProductSource.PerformLayout()
        CType(dgvProductSource, ComponentModel.ISupportInitialize).EndInit()
        pnlProductSourceSearch.ResumeLayout(False)
        pnlProductSourceSearch.PerformLayout()
        pnlProductCenterAction.ResumeLayout(False)
        grpProductTarget.ResumeLayout(False)
        grpProductTarget.PerformLayout()
        CType(dgvProductTarget, ComponentModel.ISupportInitialize).EndInit()
        pnlProductTargetSearch.ResumeLayout(False)
        pnlProductTargetSearch.PerformLayout()
        tabGeneralFacility.ResumeLayout(False)
        splitContainerGeneral.Panel1.ResumeLayout(False)
        splitContainerGeneral.Panel2.ResumeLayout(False)
        CType(splitContainerGeneral, ComponentModel.ISupportInitialize).EndInit()
        splitContainerGeneral.ResumeLayout(False)
        grpGeneralSource.ResumeLayout(False)
        grpGeneralSource.PerformLayout()
        CType(dgvGeneralSource, ComponentModel.ISupportInitialize).EndInit()
        pnlGeneralSourceSearch.ResumeLayout(False)
        pnlGeneralSourceSearch.PerformLayout()
        pnlGeneralCenterAction.ResumeLayout(False)
        grpGeneralTarget.ResumeLayout(False)
        grpGeneralTarget.PerformLayout()
        CType(dgvGeneralTarget, ComponentModel.ISupportInitialize).EndInit()
        pnlGeneralTargetSearch.ResumeLayout(False)
        pnlGeneralTargetSearch.PerformLayout()
        tabAccommodation.ResumeLayout(False)
        splitContainerAccommodation.Panel1.ResumeLayout(False)
        splitContainerAccommodation.Panel2.ResumeLayout(False)
        CType(splitContainerAccommodation, ComponentModel.ISupportInitialize).EndInit()
        splitContainerAccommodation.ResumeLayout(False)
        grpAccommodationSource.ResumeLayout(False)
        grpAccommodationSource.PerformLayout()
        CType(dgvAccommodationSource, ComponentModel.ISupportInitialize).EndInit()
        pnlAccommodationSourceSearch.ResumeLayout(False)
        pnlAccommodationSourceSearch.PerformLayout()
        pnlAccommodationCenterAction.ResumeLayout(False)
        grpAccommodationTarget.ResumeLayout(False)
        grpAccommodationTarget.PerformLayout()
        CType(dgvAccommodationTarget, ComponentModel.ISupportInitialize).EndInit()
        pnlAccommodationTargetSearch.ResumeLayout(False)
        pnlAccommodationTargetSearch.PerformLayout()
        tabSpaceFacility.ResumeLayout(False)
        splitContainerSpace.Panel1.ResumeLayout(False)
        splitContainerSpace.Panel2.ResumeLayout(False)
        CType(splitContainerSpace, ComponentModel.ISupportInitialize).EndInit()
        splitContainerSpace.ResumeLayout(False)
        grpSpaceSource.ResumeLayout(False)
        grpSpaceSource.PerformLayout()
        CType(dgvSpaceSource, ComponentModel.ISupportInitialize).EndInit()
        pnlSpaceSourceSearch.ResumeLayout(False)
        pnlSpaceSourceSearch.PerformLayout()
        pnlSpaceCenterAction.ResumeLayout(False)
        grpSpaceTarget.ResumeLayout(False)
        grpSpaceTarget.PerformLayout()
        CType(dgvSpaceTarget, ComponentModel.ISupportInitialize).EndInit()
        pnlSpaceTargetSearch.ResumeLayout(False)
        pnlSpaceTargetSearch.PerformLayout()
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
    Friend WithEvents tabProduct As System.Windows.Forms.TabPage
    Friend WithEvents tabGeneralFacility As System.Windows.Forms.TabPage
    Friend WithEvents tabAccommodation As System.Windows.Forms.TabPage
    Friend WithEvents tabSpaceFacility As System.Windows.Forms.TabPage

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

    ' Course Migration Controls
    Friend WithEvents splitContainerCourse As SplitContainer
    Friend WithEvents grpCourseSource As GroupBox
    Friend WithEvents dgvCourseSource As DataGridView
    Friend WithEvents pnlCourseSourceSearch As Panel
    Friend WithEvents btnCourseSourceSearch As ModernButton
    Friend WithEvents btnCourseLoadExcel As ModernButton
    Friend WithEvents txtCourseSourceSearchName As TextBox
    Friend WithEvents cboCourseSourceDong As ComboBox
    Friend WithEvents cboCourseSourceHo As ComboBox
    Friend WithEvents cboCourseLimit As ComboBox
    Friend WithEvents dtpCourseStart As DateTimePicker
    Friend WithEvents dtpCourseEnd As DateTimePicker
    Friend WithEvents pnlCourseSourcePagination As FlowLayoutPanel

    Friend WithEvents pnlCourseCenterAction As Panel
    Friend WithEvents btnMigrateCourse As ModernButton

    Friend WithEvents grpCourseTarget As GroupBox
    Friend WithEvents dgvCourseTarget As DataGridView
    Friend WithEvents pnlCourseTargetSearch As Panel
    Friend WithEvents btnCourseTargetSearch As ModernButton
    Friend WithEvents txtCourseTargetSearchName As TextBox
    Friend WithEvents cboCourseLimitTarget As ComboBox
    Friend WithEvents btnInitTargetCourse As ModernButton
    Friend WithEvents pnlCourseTargetPagination As FlowLayoutPanel

    ' Locker Migration Controls
    Friend WithEvents splitContainerLocker As SplitContainer
    Friend WithEvents grpLockerSource As GroupBox
    Friend WithEvents dgvLockerSource As DataGridView
    Friend WithEvents pnlLockerSourceSearch As Panel
    Friend WithEvents btnLockerSourceSearch As ModernButton
    Friend WithEvents btnLockerLoadExcel As ModernButton
    Friend WithEvents txtLockerSourceSearchName As TextBox
    Friend WithEvents cboLockerSourceDong As ComboBox
    Friend WithEvents cboLockerSourceHo As ComboBox
    Friend WithEvents cboLockerLimit As ComboBox
    Friend WithEvents pnlLockerSourcePagination As FlowLayoutPanel
    Friend WithEvents pnlLockerCenterAction As Panel
    Friend WithEvents btnMigrateLocker As ModernButton
    Friend WithEvents grpLockerTarget As GroupBox
    Friend WithEvents dgvLockerTarget As DataGridView
    Friend WithEvents pnlLockerTargetSearch As Panel
    Friend WithEvents btnLockerTargetSearch As ModernButton
    Friend WithEvents txtLockerTargetSearchName As TextBox
    Friend WithEvents cboLockerLimitTarget As ComboBox
    Friend WithEvents btnInitTargetLocker As ModernButton
    Friend WithEvents pnlLockerTargetPagination As FlowLayoutPanel

    ' Product Migration Controls
    Friend WithEvents splitContainerProduct As SplitContainer
    Friend WithEvents grpProductSource As GroupBox
    Friend WithEvents dgvProductSource As DataGridView
    Friend WithEvents pnlProductSourceSearch As Panel
    Friend WithEvents btnProductSourceSearch As ModernButton
    Friend WithEvents btnProductLoadExcel As ModernButton
    Friend WithEvents txtProductSourceSearchName As TextBox
    Friend WithEvents cboProductSourceDong As ComboBox
    Friend WithEvents cboProductSourceHo As ComboBox
    Friend WithEvents cboProductLimit As ComboBox
    Friend WithEvents dtpProductStart As DateTimePicker
    Friend WithEvents dtpProductEnd As DateTimePicker
    Friend WithEvents pnlProductSourcePagination As FlowLayoutPanel
    Friend WithEvents pnlProductCenterAction As Panel
    Friend WithEvents btnMigrateProduct As ModernButton
    Friend WithEvents grpProductTarget As GroupBox
    Friend WithEvents dgvProductTarget As DataGridView
    Friend WithEvents pnlProductTargetSearch As Panel
    Friend WithEvents btnProductTargetSearch As ModernButton
    Friend WithEvents txtProductTargetSearchName As TextBox
    Friend WithEvents cboProductLimitTarget As ComboBox
    Friend WithEvents btnInitTargetProduct As ModernButton
    Friend WithEvents pnlProductTargetPagination As FlowLayoutPanel

    ' General Facility Migration Controls
    Friend WithEvents splitContainerGeneral As SplitContainer
    Friend WithEvents grpGeneralSource As GroupBox
    Friend WithEvents dgvGeneralSource As DataGridView
    Friend WithEvents pnlGeneralSourceSearch As Panel
    Friend WithEvents btnGeneralSourceSearch As ModernButton
    Friend WithEvents btnGeneralLoadExcel As ModernButton
    Friend WithEvents txtGeneralSourceSearchName As TextBox
    Friend WithEvents cboGeneralSourceDong As ComboBox
    Friend WithEvents cboGeneralSourceHo As ComboBox
    Friend WithEvents cboGeneralLimit As ComboBox
    Friend WithEvents dtpGeneralStart As DateTimePicker
    Friend WithEvents dtpGeneralEnd As DateTimePicker
    Friend WithEvents pnlGeneralSourcePagination As FlowLayoutPanel
    Friend WithEvents pnlGeneralCenterAction As Panel
    Friend WithEvents btnMigrateGeneral As ModernButton
    Friend WithEvents grpGeneralTarget As GroupBox
    Friend WithEvents dgvGeneralTarget As DataGridView
    Friend WithEvents pnlGeneralTargetSearch As Panel
    Friend WithEvents btnGeneralTargetSearch As ModernButton
    Friend WithEvents txtGeneralTargetSearchName As TextBox
    Friend WithEvents cboGeneralLimitTarget As ComboBox
    Friend WithEvents btnInitTargetGeneral As ModernButton
    Friend WithEvents pnlGeneralTargetPagination As FlowLayoutPanel

    ' Accommodation Migration Controls
    Friend WithEvents splitContainerAccommodation As SplitContainer
    Friend WithEvents grpAccommodationSource As GroupBox
    Friend WithEvents dgvAccommodationSource As DataGridView
    Friend WithEvents pnlAccommodationSourceSearch As Panel
    Friend WithEvents btnAccommodationSourceSearch As ModernButton
    Friend WithEvents btnAccommodationLoadExcel As ModernButton
    Friend WithEvents txtAccommodationSourceSearchName As TextBox
    Friend WithEvents cboAccommodationSourceDong As ComboBox
    Friend WithEvents cboAccommodationSourceHo As ComboBox
    Friend WithEvents cboAccommodationLimit As ComboBox
    Friend WithEvents dtpAccommodationStart As DateTimePicker
    Friend WithEvents dtpAccommodationEnd As DateTimePicker
    Friend WithEvents pnlAccommodationSourcePagination As FlowLayoutPanel
    Friend WithEvents pnlAccommodationCenterAction As Panel
    Friend WithEvents btnMigrateAccommodation As ModernButton
    Friend WithEvents grpAccommodationTarget As GroupBox
    Friend WithEvents dgvAccommodationTarget As DataGridView
    Friend WithEvents pnlAccommodationTargetSearch As Panel
    Friend WithEvents btnAccommodationTargetSearch As ModernButton
    Friend WithEvents txtAccommodationTargetSearchName As TextBox
    Friend WithEvents cboAccommodationLimitTarget As ComboBox
    Friend WithEvents btnInitTargetAccommodation As ModernButton
    Friend WithEvents pnlAccommodationTargetPagination As FlowLayoutPanel

    ' Space Facility Migration Controls
    Friend WithEvents splitContainerSpace As SplitContainer
    Friend WithEvents grpSpaceSource As GroupBox
    Friend WithEvents dgvSpaceSource As DataGridView
    Friend WithEvents pnlSpaceSourceSearch As Panel
    Friend WithEvents btnSpaceSourceSearch As ModernButton
    Friend WithEvents btnSpaceLoadExcel As ModernButton
    Friend WithEvents txtSpaceSourceSearchName As TextBox
    Friend WithEvents cboSpaceSourceDong As ComboBox
    Friend WithEvents cboSpaceSourceHo As ComboBox
    Friend WithEvents cboSpaceLimit As ComboBox
    Friend WithEvents dtpSpaceStart As DateTimePicker
    Friend WithEvents dtpSpaceEnd As DateTimePicker
    Friend WithEvents pnlSpaceSourcePagination As FlowLayoutPanel
    Friend WithEvents pnlSpaceCenterAction As Panel
    Friend WithEvents btnMigrateSpace As ModernButton
    Friend WithEvents grpSpaceTarget As GroupBox
    Friend WithEvents dgvSpaceTarget As DataGridView
    Friend WithEvents pnlSpaceTargetSearch As Panel
    Friend WithEvents btnSpaceTargetSearch As ModernButton
    Friend WithEvents txtSpaceTargetSearchName As TextBox
    Friend WithEvents cboSpaceLimitTarget As ComboBox
    Friend WithEvents btnInitTargetSpace As ModernButton
    Friend WithEvents pnlSpaceTargetPagination As FlowLayoutPanel
End Class
