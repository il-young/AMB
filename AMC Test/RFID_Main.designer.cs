namespace AMC_Test
{
    partial class RFID_Main
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RFID_Main));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tslblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cmbSerial = new System.Windows.Forms.ComboBox();
            this.cmbConnectType = new System.Windows.Forms.ComboBox();
            this.label28 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtIpAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpConfiguration = new System.Windows.Forms.TabPage();
            this.btnConfigurationWrite = new System.Windows.Forms.Button();
            this.btnConfigurationRead = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label26 = new System.Windows.Forms.Label();
            this.nudPower = new System.Windows.Forms.NumericUpDown();
            this.chkAnt4 = new System.Windows.Forms.CheckBox();
            this.chkAnt3 = new System.Windows.Forms.CheckBox();
            this.chkAnt2 = new System.Windows.Forms.CheckBox();
            this.chkAnt1 = new System.Windows.Forms.CheckBox();
            this.grpVersion = new System.Windows.Forms.GroupBox();
            this.lblVersionRoot = new System.Windows.Forms.Label();
            this.lblVersionHardware = new System.Windows.Forms.Label();
            this.lblVersionReader = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.tpInventory = new System.Windows.Forms.TabPage();
            this.lvwInventory = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClearInventory = new System.Windows.Forms.Button();
            this.btnStopInventory = new System.Windows.Forms.Button();
            this.btnStartInventory = new System.Windows.Forms.Button();
            this.tpReadWrite = new System.Windows.Forms.TabPage();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.btnClearResponse = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.lstResponse = new System.Windows.Forms.ListBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnWrite = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.mtxtTagHex = new System.Windows.Forms.MaskedTextBox();
            this.numLength = new System.Windows.Forms.NumericUpDown();
            this.numLocation = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbBank = new System.Windows.Forms.ComboBox();
            this.tpLockKill = new System.Windows.Forms.TabPage();
            this.label23 = new System.Windows.Forms.Label();
            this.txtPasswordLockKill = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbUserPermissions = new System.Windows.Forms.ComboBox();
            this.cmbTidPermissions = new System.Windows.Forms.ComboBox();
            this.cmbEpcPermissions = new System.Windows.Forms.ComboBox();
            this.cmbAccessPermissions = new System.Windows.Forms.ComboBox();
            this.cmbKillPermissions = new System.Windows.Forms.ComboBox();
            this.btnClearResponseLockKill = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.lstResponseLockKill = new System.Windows.Forms.ListBox();
            this.btnKill = new System.Windows.Forms.Button();
            this.btnLock = new System.Windows.Forms.Button();
            this.bw_RFID_dealy = new System.ComponentModel.BackgroundWorker();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpConfiguration.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPower)).BeginInit();
            this.grpVersion.SuspendLayout();
            this.tpInventory.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tpReadWrite.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLocation)).BeginInit();
            this.tpLockKill.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 612);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(663, 25);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tslblStatus
            // 
            this.tslblStatus.AutoSize = false;
            this.tslblStatus.Name = "tslblStatus";
            this.tslblStatus.Size = new System.Drawing.Size(200, 20);
            this.tslblStatus.Text = "None";
            this.tslblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.splitContainer1.Panel1.Controls.Add(this.cmbSerial);
            this.splitContainer1.Panel1.Controls.Add(this.cmbConnectType);
            this.splitContainer1.Panel1.Controls.Add(this.label28);
            this.splitContainer1.Panel1.Controls.Add(this.btnClose);
            this.splitContainer1.Panel1.Controls.Add(this.btnConnect);
            this.splitContainer1.Panel1.Controls.Add(this.txtIpAddress);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel2.Enabled = false;
            this.splitContainer1.Size = new System.Drawing.Size(663, 612);
            this.splitContainer1.SplitterDistance = 56;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 1;
            // 
            // cmbSerial
            // 
            this.cmbSerial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSerial.Enabled = false;
            this.cmbSerial.FormattingEnabled = true;
            this.cmbSerial.Location = new System.Drawing.Point(266, 39);
            this.cmbSerial.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbSerial.Name = "cmbSerial";
            this.cmbSerial.Size = new System.Drawing.Size(117, 23);
            this.cmbSerial.TabIndex = 16;
            // 
            // cmbConnectType
            // 
            this.cmbConnectType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbConnectType.FormattingEnabled = true;
            this.cmbConnectType.Items.AddRange(new object[] {
            "TCP/IP",
            "SERIAL"});
            this.cmbConnectType.Location = new System.Drawing.Point(10, 24);
            this.cmbConnectType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbConnectType.Name = "cmbConnectType";
            this.cmbConnectType.Size = new System.Drawing.Size(127, 23);
            this.cmbConnectType.TabIndex = 15;
            this.cmbConnectType.SelectedIndexChanged += new System.EventHandler(this.cmbConnectType_SelectedIndexChanged);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label28.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label28.Location = new System.Drawing.Point(146, 44);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(75, 15);
            this.label28.TabIndex = 4;
            this.label28.Text = "* Serial :";
            // 
            // btnClose
            // 
            this.btnClose.Enabled = false;
            this.btnClose.Location = new System.Drawing.Point(533, 20);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(110, 29);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(413, 20);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(113, 29);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtIpAddress
            // 
            this.txtIpAddress.Enabled = false;
            this.txtIpAddress.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtIpAddress.Location = new System.Drawing.Point(266, 10);
            this.txtIpAddress.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtIpAddress.MaxLength = 15;
            this.txtIpAddress.Name = "txtIpAddress";
            this.txtIpAddress.ReadOnly = true;
            this.txtIpAddress.Size = new System.Drawing.Size(117, 25);
            this.txtIpAddress.TabIndex = 1;
            this.txtIpAddress.Text = "192.168.0.103";
            this.txtIpAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIpAddress_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(146, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "* IP Address :";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpConfiguration);
            this.tabControl1.Controls.Add(this.tpInventory);
            this.tabControl1.Controls.Add(this.tpReadWrite);
            this.tabControl1.Controls.Add(this.tpLockKill);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.HotTrack = true;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(663, 551);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            this.tabControl1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Selecting);
            // 
            // tpConfiguration
            // 
            this.tpConfiguration.Controls.Add(this.btnConfigurationWrite);
            this.tpConfiguration.Controls.Add(this.btnConfigurationRead);
            this.tpConfiguration.Controls.Add(this.groupBox1);
            this.tpConfiguration.Controls.Add(this.grpVersion);
            this.tpConfiguration.Location = new System.Drawing.Point(4, 25);
            this.tpConfiguration.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tpConfiguration.Name = "tpConfiguration";
            this.tpConfiguration.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tpConfiguration.Size = new System.Drawing.Size(655, 522);
            this.tpConfiguration.TabIndex = 3;
            this.tpConfiguration.Text = "Configuration";
            this.tpConfiguration.UseVisualStyleBackColor = true;
            // 
            // btnConfigurationWrite
            // 
            this.btnConfigurationWrite.Location = new System.Drawing.Point(331, 350);
            this.btnConfigurationWrite.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnConfigurationWrite.Name = "btnConfigurationWrite";
            this.btnConfigurationWrite.Size = new System.Drawing.Size(86, 29);
            this.btnConfigurationWrite.TabIndex = 3;
            this.btnConfigurationWrite.Text = "Write";
            this.btnConfigurationWrite.UseVisualStyleBackColor = true;
            this.btnConfigurationWrite.Click += new System.EventHandler(this.btnConfigurationWrite_Click);
            // 
            // btnConfigurationRead
            // 
            this.btnConfigurationRead.Location = new System.Drawing.Point(239, 350);
            this.btnConfigurationRead.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnConfigurationRead.Name = "btnConfigurationRead";
            this.btnConfigurationRead.Size = new System.Drawing.Size(86, 29);
            this.btnConfigurationRead.TabIndex = 2;
            this.btnConfigurationRead.Text = "Read";
            this.btnConfigurationRead.UseVisualStyleBackColor = true;
            this.btnConfigurationRead.Click += new System.EventHandler(this.btnConfigurationRead_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label26);
            this.groupBox1.Controls.Add(this.nudPower);
            this.groupBox1.Controls.Add(this.chkAnt4);
            this.groupBox1.Controls.Add(this.chkAnt3);
            this.groupBox1.Controls.Add(this.chkAnt2);
            this.groupBox1.Controls.Add(this.chkAnt1);
            this.groupBox1.Location = new System.Drawing.Point(21, 182);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(617, 125);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Antenna Info";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(502, 44);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(48, 15);
            this.label26.TabIndex = 5;
            this.label26.Text = "Power";
            // 
            // nudPower
            // 
            this.nudPower.Font = new System.Drawing.Font("굴림", 9F);
            this.nudPower.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudPower.Location = new System.Drawing.Point(485, 62);
            this.nudPower.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.nudPower.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.nudPower.Name = "nudPower";
            this.nudPower.ReadOnly = true;
            this.nudPower.Size = new System.Drawing.Size(82, 25);
            this.nudPower.TabIndex = 4;
            this.nudPower.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chkAnt4
            // 
            this.chkAnt4.AutoSize = true;
            this.chkAnt4.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.chkAnt4.Location = new System.Drawing.Point(345, 49);
            this.chkAnt4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkAnt4.Name = "chkAnt4";
            this.chkAnt4.Size = new System.Drawing.Size(77, 36);
            this.chkAnt4.TabIndex = 3;
            this.chkAnt4.Text = "Antenna 4";
            this.chkAnt4.UseVisualStyleBackColor = true;
            // 
            // chkAnt3
            // 
            this.chkAnt3.AutoSize = true;
            this.chkAnt3.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.chkAnt3.Location = new System.Drawing.Point(238, 49);
            this.chkAnt3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkAnt3.Name = "chkAnt3";
            this.chkAnt3.Size = new System.Drawing.Size(77, 36);
            this.chkAnt3.TabIndex = 2;
            this.chkAnt3.Text = "Antenna 3";
            this.chkAnt3.UseVisualStyleBackColor = true;
            // 
            // chkAnt2
            // 
            this.chkAnt2.AutoSize = true;
            this.chkAnt2.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.chkAnt2.Location = new System.Drawing.Point(130, 49);
            this.chkAnt2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkAnt2.Name = "chkAnt2";
            this.chkAnt2.Size = new System.Drawing.Size(77, 36);
            this.chkAnt2.TabIndex = 1;
            this.chkAnt2.Text = "Antenna 2";
            this.chkAnt2.UseVisualStyleBackColor = true;
            // 
            // chkAnt1
            // 
            this.chkAnt1.AutoSize = true;
            this.chkAnt1.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.chkAnt1.Location = new System.Drawing.Point(23, 49);
            this.chkAnt1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkAnt1.Name = "chkAnt1";
            this.chkAnt1.Size = new System.Drawing.Size(77, 36);
            this.chkAnt1.TabIndex = 0;
            this.chkAnt1.Text = "Antenna 1";
            this.chkAnt1.UseVisualStyleBackColor = true;
            // 
            // grpVersion
            // 
            this.grpVersion.Controls.Add(this.lblVersionRoot);
            this.grpVersion.Controls.Add(this.lblVersionHardware);
            this.grpVersion.Controls.Add(this.lblVersionReader);
            this.grpVersion.Controls.Add(this.label27);
            this.grpVersion.Controls.Add(this.label25);
            this.grpVersion.Controls.Add(this.label24);
            this.grpVersion.Location = new System.Drawing.Point(21, 36);
            this.grpVersion.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpVersion.Name = "grpVersion";
            this.grpVersion.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpVersion.Size = new System.Drawing.Size(617, 125);
            this.grpVersion.TabIndex = 0;
            this.grpVersion.TabStop = false;
            this.grpVersion.Text = "Version Info";
            // 
            // lblVersionRoot
            // 
            this.lblVersionRoot.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblVersionRoot.Location = new System.Drawing.Point(491, 60);
            this.lblVersionRoot.Name = "lblVersionRoot";
            this.lblVersionRoot.Size = new System.Drawing.Size(82, 15);
            this.lblVersionRoot.TabIndex = 5;
            // 
            // lblVersionHardware
            // 
            this.lblVersionHardware.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblVersionHardware.Location = new System.Drawing.Point(338, 60);
            this.lblVersionHardware.Name = "lblVersionHardware";
            this.lblVersionHardware.Size = new System.Drawing.Size(82, 15);
            this.lblVersionHardware.TabIndex = 4;
            // 
            // lblVersionReader
            // 
            this.lblVersionReader.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblVersionReader.Location = new System.Drawing.Point(99, 60);
            this.lblVersionReader.Name = "lblVersionReader";
            this.lblVersionReader.Size = new System.Drawing.Size(103, 15);
            this.lblVersionReader.TabIndex = 3;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(430, 60);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(61, 15);
            this.label27.TabIndex = 2;
            this.label27.Text = "* Root :";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(243, 60);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(89, 15);
            this.label25.TabIndex = 1;
            this.label25.Text = "* Hardware :";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(21, 60);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(75, 15);
            this.label24.TabIndex = 0;
            this.label24.Text = "* Reader :";
            // 
            // tpInventory
            // 
            this.tpInventory.Controls.Add(this.lvwInventory);
            this.tpInventory.Controls.Add(this.panel1);
            this.tpInventory.Location = new System.Drawing.Point(4, 25);
            this.tpInventory.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tpInventory.Name = "tpInventory";
            this.tpInventory.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tpInventory.Size = new System.Drawing.Size(655, 522);
            this.tpInventory.TabIndex = 0;
            this.tpInventory.Text = "Inventory";
            this.tpInventory.UseVisualStyleBackColor = true;
            // 
            // lvwInventory
            // 
            this.lvwInventory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader1,
            this.columnHeader2});
            this.lvwInventory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwInventory.FullRowSelect = true;
            this.lvwInventory.GridLines = true;
            this.lvwInventory.Location = new System.Drawing.Point(3, 63);
            this.lvwInventory.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lvwInventory.MultiSelect = false;
            this.lvwInventory.Name = "lvwInventory";
            this.lvwInventory.Size = new System.Drawing.Size(649, 455);
            this.lvwInventory.TabIndex = 1;
            this.lvwInventory.UseCompatibleStateImageBehavior = false;
            this.lvwInventory.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Date";
            this.columnHeader3.Width = 164;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Tag ID (HEX)";
            this.columnHeader1.Width = 313;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "TEXT";
            this.columnHeader2.Width = 143;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnClearInventory);
            this.panel1.Controls.Add(this.btnStopInventory);
            this.panel1.Controls.Add(this.btnStartInventory);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 4);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(649, 59);
            this.panel1.TabIndex = 0;
            // 
            // btnClearInventory
            // 
            this.btnClearInventory.Location = new System.Drawing.Point(208, 16);
            this.btnClearInventory.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClearInventory.Name = "btnClearInventory";
            this.btnClearInventory.Size = new System.Drawing.Size(86, 29);
            this.btnClearInventory.TabIndex = 2;
            this.btnClearInventory.Text = "Clear";
            this.btnClearInventory.UseVisualStyleBackColor = true;
            this.btnClearInventory.Click += new System.EventHandler(this.btnClearInventory_Click);
            // 
            // btnStopInventory
            // 
            this.btnStopInventory.Location = new System.Drawing.Point(114, 16);
            this.btnStopInventory.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnStopInventory.Name = "btnStopInventory";
            this.btnStopInventory.Size = new System.Drawing.Size(86, 29);
            this.btnStopInventory.TabIndex = 1;
            this.btnStopInventory.Text = "Stop";
            this.btnStopInventory.UseVisualStyleBackColor = true;
            this.btnStopInventory.Click += new System.EventHandler(this.btnStopInventory_Click);
            // 
            // btnStartInventory
            // 
            this.btnStartInventory.Location = new System.Drawing.Point(21, 16);
            this.btnStartInventory.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnStartInventory.Name = "btnStartInventory";
            this.btnStartInventory.Size = new System.Drawing.Size(86, 29);
            this.btnStartInventory.TabIndex = 0;
            this.btnStartInventory.Text = "Start";
            this.btnStartInventory.UseVisualStyleBackColor = true;
            this.btnStartInventory.Click += new System.EventHandler(this.btnStartInventory_Click);
            // 
            // tpReadWrite
            // 
            this.tpReadWrite.Controls.Add(this.label22);
            this.tpReadWrite.Controls.Add(this.label21);
            this.tpReadWrite.Controls.Add(this.label20);
            this.tpReadWrite.Controls.Add(this.btnClearResponse);
            this.tpReadWrite.Controls.Add(this.label7);
            this.tpReadWrite.Controls.Add(this.lstResponse);
            this.tpReadWrite.Controls.Add(this.txtPassword);
            this.tpReadWrite.Controls.Add(this.label6);
            this.tpReadWrite.Controls.Add(this.label5);
            this.tpReadWrite.Controls.Add(this.btnWrite);
            this.tpReadWrite.Controls.Add(this.btnRead);
            this.tpReadWrite.Controls.Add(this.mtxtTagHex);
            this.tpReadWrite.Controls.Add(this.numLength);
            this.tpReadWrite.Controls.Add(this.numLocation);
            this.tpReadWrite.Controls.Add(this.label4);
            this.tpReadWrite.Controls.Add(this.label3);
            this.tpReadWrite.Controls.Add(this.label2);
            this.tpReadWrite.Controls.Add(this.cmbBank);
            this.tpReadWrite.Location = new System.Drawing.Point(4, 25);
            this.tpReadWrite.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tpReadWrite.Name = "tpReadWrite";
            this.tpReadWrite.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tpReadWrite.Size = new System.Drawing.Size(655, 522);
            this.tpReadWrite.TabIndex = 1;
            this.tpReadWrite.Text = "Read / Write";
            this.tpReadWrite.UseVisualStyleBackColor = true;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(327, 206);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(115, 15);
            this.label22.TabIndex = 20;
            this.label22.Text = "(ex : 12345678)";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(235, 126);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(94, 15);
            this.label21.TabIndex = 19;
            this.label21.Text = "(Unit : Word)";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(235, 86);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(94, 15);
            this.label20.TabIndex = 18;
            this.label20.Text = "(Unit : Word)";
            // 
            // btnClearResponse
            // 
            this.btnClearResponse.Location = new System.Drawing.Point(24, 408);
            this.btnClearResponse.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClearResponse.Name = "btnClearResponse";
            this.btnClearResponse.Size = new System.Drawing.Size(86, 29);
            this.btnClearResponse.TabIndex = 17;
            this.btnClearResponse.Text = "Clear";
            this.btnClearResponse.UseVisualStyleBackColor = true;
            this.btnClearResponse.Click += new System.EventHandler(this.btnClearResponse_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(22, 326);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 15);
            this.label7.TabIndex = 16;
            this.label7.Text = "Response :";
            // 
            // lstResponse
            // 
            this.lstResponse.BackColor = System.Drawing.Color.PeachPuff;
            this.lstResponse.FormattingEnabled = true;
            this.lstResponse.ItemHeight = 15;
            this.lstResponse.Location = new System.Drawing.Point(133, 326);
            this.lstResponse.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstResponse.Name = "lstResponse";
            this.lstResponse.Size = new System.Drawing.Size(507, 109);
            this.lstResponse.TabIndex = 15;
            // 
            // txtPassword
            // 
            this.txtPassword.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtPassword.Location = new System.Drawing.Point(165, 201);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtPassword.MaxLength = 8;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(155, 25);
            this.txtPassword.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(54, 206);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 15);
            this.label6.TabIndex = 13;
            this.label6.Text = "Password :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(54, 166);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 15);
            this.label5.TabIndex = 11;
            this.label5.Text = "TAG ID (HEX) :";
            // 
            // btnWrite
            // 
            this.btnWrite.Location = new System.Drawing.Point(257, 258);
            this.btnWrite.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(86, 29);
            this.btnWrite.TabIndex = 8;
            this.btnWrite.Text = "Write";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(165, 258);
            this.btnRead.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(86, 29);
            this.btnRead.TabIndex = 7;
            this.btnRead.Text = "Read";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // mtxtTagHex
            // 
            this.mtxtTagHex.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.mtxtTagHex.Location = new System.Drawing.Point(165, 160);
            this.mtxtTagHex.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.mtxtTagHex.Name = "mtxtTagHex";
            this.mtxtTagHex.Size = new System.Drawing.Size(423, 25);
            this.mtxtTagHex.TabIndex = 6;
            this.mtxtTagHex.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // numLength
            // 
            this.numLength.Location = new System.Drawing.Point(165, 119);
            this.numLength.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numLength.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numLength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numLength.Name = "numLength";
            this.numLength.ReadOnly = true;
            this.numLength.Size = new System.Drawing.Size(64, 25);
            this.numLength.TabIndex = 5;
            this.numLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numLength.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numLength.ValueChanged += new System.EventHandler(this.OnLengthChange);
            // 
            // numLocation
            // 
            this.numLocation.Location = new System.Drawing.Point(165, 78);
            this.numLocation.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numLocation.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numLocation.Name = "numLocation";
            this.numLocation.ReadOnly = true;
            this.numLocation.Size = new System.Drawing.Size(64, 25);
            this.numLocation.TabIndex = 4;
            this.numLocation.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(54, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Length :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(54, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Location :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Bank :";
            // 
            // cmbBank
            // 
            this.cmbBank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBank.FormattingEnabled = true;
            this.cmbBank.Items.AddRange(new object[] {
            "RESERVED",
            "EPC",
            "TID",
            "USER"});
            this.cmbBank.Location = new System.Drawing.Point(165, 38);
            this.cmbBank.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbBank.Name = "cmbBank";
            this.cmbBank.Size = new System.Drawing.Size(177, 23);
            this.cmbBank.TabIndex = 0;
            // 
            // tpLockKill
            // 
            this.tpLockKill.Controls.Add(this.label23);
            this.tpLockKill.Controls.Add(this.txtPasswordLockKill);
            this.tpLockKill.Controls.Add(this.label19);
            this.tpLockKill.Controls.Add(this.label18);
            this.tpLockKill.Controls.Add(this.label17);
            this.tpLockKill.Controls.Add(this.label16);
            this.tpLockKill.Controls.Add(this.label15);
            this.tpLockKill.Controls.Add(this.label14);
            this.tpLockKill.Controls.Add(this.label13);
            this.tpLockKill.Controls.Add(this.label12);
            this.tpLockKill.Controls.Add(this.label11);
            this.tpLockKill.Controls.Add(this.label10);
            this.tpLockKill.Controls.Add(this.label9);
            this.tpLockKill.Controls.Add(this.cmbUserPermissions);
            this.tpLockKill.Controls.Add(this.cmbTidPermissions);
            this.tpLockKill.Controls.Add(this.cmbEpcPermissions);
            this.tpLockKill.Controls.Add(this.cmbAccessPermissions);
            this.tpLockKill.Controls.Add(this.cmbKillPermissions);
            this.tpLockKill.Controls.Add(this.btnClearResponseLockKill);
            this.tpLockKill.Controls.Add(this.label8);
            this.tpLockKill.Controls.Add(this.lstResponseLockKill);
            this.tpLockKill.Controls.Add(this.btnKill);
            this.tpLockKill.Controls.Add(this.btnLock);
            this.tpLockKill.Location = new System.Drawing.Point(4, 25);
            this.tpLockKill.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tpLockKill.Name = "tpLockKill";
            this.tpLockKill.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tpLockKill.Size = new System.Drawing.Size(655, 522);
            this.tpLockKill.TabIndex = 2;
            this.tpLockKill.Text = "Lock / Kill";
            this.tpLockKill.UseVisualStyleBackColor = true;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(337, 218);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(115, 15);
            this.label23.TabIndex = 41;
            this.label23.Text = "(ex : 12345678)";
            // 
            // txtPasswordLockKill
            // 
            this.txtPasswordLockKill.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtPasswordLockKill.Location = new System.Drawing.Point(165, 214);
            this.txtPasswordLockKill.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtPasswordLockKill.MaxLength = 8;
            this.txtPasswordLockKill.Name = "txtPasswordLockKill";
            this.txtPasswordLockKill.Size = new System.Drawing.Size(155, 25);
            this.txtPasswordLockKill.TabIndex = 40;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(73, 218);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(72, 15);
            this.label19.TabIndex = 39;
            this.label19.Text = "Password";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(451, 171);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(62, 15);
            this.label18.TabIndex = 38;
            this.label18.Text = "( Write )";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(451, 139);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(62, 15);
            this.label17.TabIndex = 37;
            this.label17.Text = "( Write )";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(451, 106);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(62, 15);
            this.label16.TabIndex = 36;
            this.label16.Text = "( Write )";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(451, 74);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(112, 15);
            this.label15.TabIndex = 35;
            this.label15.Text = "( Read / Write )";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(451, 46);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(112, 15);
            this.label14.TabIndex = 34;
            this.label14.Text = "( Read / Write )";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(72, 171);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(75, 15);
            this.label13.TabIndex = 33;
            this.label13.Text = "User Bank";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(80, 139);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(66, 15);
            this.label12.TabIndex = 32;
            this.label12.Text = "TID Bank";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(73, 106);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(74, 15);
            this.label11.TabIndex = 31;
            this.label11.Text = "EPC Bank";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(19, 74);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(126, 15);
            this.label10.TabIndex = 30;
            this.label10.Text = "Access Password";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(49, 41);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(94, 15);
            this.label9.TabIndex = 29;
            this.label9.Text = "Kill Password";
            // 
            // cmbUserPermissions
            // 
            this.cmbUserPermissions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUserPermissions.FormattingEnabled = true;
            this.cmbUserPermissions.Location = new System.Drawing.Point(165, 168);
            this.cmbUserPermissions.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbUserPermissions.Name = "cmbUserPermissions";
            this.cmbUserPermissions.Size = new System.Drawing.Size(252, 23);
            this.cmbUserPermissions.TabIndex = 28;
            // 
            // cmbTidPermissions
            // 
            this.cmbTidPermissions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTidPermissions.FormattingEnabled = true;
            this.cmbTidPermissions.Location = new System.Drawing.Point(165, 135);
            this.cmbTidPermissions.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbTidPermissions.Name = "cmbTidPermissions";
            this.cmbTidPermissions.Size = new System.Drawing.Size(252, 23);
            this.cmbTidPermissions.TabIndex = 27;
            // 
            // cmbEpcPermissions
            // 
            this.cmbEpcPermissions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEpcPermissions.FormattingEnabled = true;
            this.cmbEpcPermissions.Location = new System.Drawing.Point(165, 102);
            this.cmbEpcPermissions.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbEpcPermissions.Name = "cmbEpcPermissions";
            this.cmbEpcPermissions.Size = new System.Drawing.Size(252, 23);
            this.cmbEpcPermissions.TabIndex = 26;
            // 
            // cmbAccessPermissions
            // 
            this.cmbAccessPermissions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAccessPermissions.FormattingEnabled = true;
            this.cmbAccessPermissions.Location = new System.Drawing.Point(165, 70);
            this.cmbAccessPermissions.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbAccessPermissions.Name = "cmbAccessPermissions";
            this.cmbAccessPermissions.Size = new System.Drawing.Size(252, 23);
            this.cmbAccessPermissions.TabIndex = 25;
            // 
            // cmbKillPermissions
            // 
            this.cmbKillPermissions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKillPermissions.FormattingEnabled = true;
            this.cmbKillPermissions.Location = new System.Drawing.Point(165, 38);
            this.cmbKillPermissions.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbKillPermissions.Name = "cmbKillPermissions";
            this.cmbKillPermissions.Size = new System.Drawing.Size(252, 23);
            this.cmbKillPermissions.TabIndex = 24;
            // 
            // btnClearResponseLockKill
            // 
            this.btnClearResponseLockKill.Location = new System.Drawing.Point(24, 408);
            this.btnClearResponseLockKill.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClearResponseLockKill.Name = "btnClearResponseLockKill";
            this.btnClearResponseLockKill.Size = new System.Drawing.Size(86, 29);
            this.btnClearResponseLockKill.TabIndex = 23;
            this.btnClearResponseLockKill.Text = "Clear";
            this.btnClearResponseLockKill.UseVisualStyleBackColor = true;
            this.btnClearResponseLockKill.Click += new System.EventHandler(this.btnClearResponseLockKill_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(22, 326);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 15);
            this.label8.TabIndex = 22;
            this.label8.Text = "Response :";
            // 
            // lstResponseLockKill
            // 
            this.lstResponseLockKill.BackColor = System.Drawing.Color.PeachPuff;
            this.lstResponseLockKill.FormattingEnabled = true;
            this.lstResponseLockKill.ItemHeight = 15;
            this.lstResponseLockKill.Location = new System.Drawing.Point(133, 326);
            this.lstResponseLockKill.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstResponseLockKill.Name = "lstResponseLockKill";
            this.lstResponseLockKill.Size = new System.Drawing.Size(507, 109);
            this.lstResponseLockKill.TabIndex = 21;
            // 
            // btnKill
            // 
            this.btnKill.Location = new System.Drawing.Point(257, 260);
            this.btnKill.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnKill.Name = "btnKill";
            this.btnKill.Size = new System.Drawing.Size(86, 29);
            this.btnKill.TabIndex = 12;
            this.btnKill.Text = "Kill";
            this.btnKill.UseVisualStyleBackColor = true;
            this.btnKill.Click += new System.EventHandler(this.btnKill_Click);
            // 
            // btnLock
            // 
            this.btnLock.Location = new System.Drawing.Point(165, 260);
            this.btnLock.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnLock.Name = "btnLock";
            this.btnLock.Size = new System.Drawing.Size(86, 29);
            this.btnLock.TabIndex = 11;
            this.btnLock.Text = "Lock";
            this.btnLock.UseVisualStyleBackColor = true;
            this.btnLock.Click += new System.EventHandler(this.btnLock_Click);
            // 
            // bw_RFID_dealy
            // 
            this.bw_RFID_dealy.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_RFID_dealy_DoWork);
            // 
            // RFID_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 637);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RFID_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RFID Reader";            
            this.Load += new System.EventHandler(this.Main_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpConfiguration.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPower)).EndInit();
            this.grpVersion.ResumeLayout(false);
            this.grpVersion.PerformLayout();
            this.tpInventory.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tpReadWrite.ResumeLayout(false);
            this.tpReadWrite.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLocation)).EndInit();
            this.tpLockKill.ResumeLayout(false);
            this.tpLockKill.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpInventory;
        private System.Windows.Forms.TabPage tpReadWrite;
        private System.Windows.Forms.TabPage tpLockKill;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtIpAddress;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ListView lvwInventory;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button btnStartInventory;
        private System.Windows.Forms.Button btnStopInventory;
        private System.Windows.Forms.Button btnClearInventory;
        private System.Windows.Forms.ComboBox cmbBank;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numLength;
        private System.Windows.Forms.NumericUpDown numLocation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MaskedTextBox mtxtTagHex;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnKill;
        private System.Windows.Forms.Button btnLock;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.ToolStripStatusLabel tslblStatus;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListBox lstResponse;
        private System.Windows.Forms.Button btnClearResponse;
        private System.Windows.Forms.Button btnClearResponseLockKill;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListBox lstResponseLockKill;
        private System.Windows.Forms.ComboBox cmbKillPermissions;
        private System.Windows.Forms.ComboBox cmbUserPermissions;
        private System.Windows.Forms.ComboBox cmbTidPermissions;
        private System.Windows.Forms.ComboBox cmbEpcPermissions;
        private System.Windows.Forms.ComboBox cmbAccessPermissions;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtPasswordLockKill;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TabPage tpConfiguration;
        private System.Windows.Forms.GroupBox grpVersion;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label lblVersionRoot;
        private System.Windows.Forms.Label lblVersionHardware;
        private System.Windows.Forms.Label lblVersionReader;
        private System.Windows.Forms.CheckBox chkAnt4;
        private System.Windows.Forms.CheckBox chkAnt3;
        private System.Windows.Forms.CheckBox chkAnt2;
        private System.Windows.Forms.CheckBox chkAnt1;
        private System.Windows.Forms.Button btnConfigurationWrite;
        private System.Windows.Forms.Button btnConfigurationRead;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.NumericUpDown nudPower;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label label28;
        internal System.Windows.Forms.ComboBox cmbConnectType;
        private System.Windows.Forms.ComboBox cmbSerial;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.ComponentModel.BackgroundWorker bw_RFID_dealy;
    }
}

