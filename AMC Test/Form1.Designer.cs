﻿namespace AMC_Test
{
    partial class Form1
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
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.bg_CMD = new System.ComponentModel.BackgroundWorker();
            this.bg_reseve = new System.ComponentModel.BackgroundWorker();
            this.bg_st = new System.ComponentModel.BackgroundWorker();
            this.bg_Display = new System.ComponentModel.BackgroundWorker();
            this.bg_Conveyor = new System.ComponentModel.BackgroundWorker();
            this.bg_SIM = new System.ComponentModel.BackgroundWorker();
            this.lb_cmd = new System.Windows.Forms.ListBox();
            this.btn_GoGoal = new System.Windows.Forms.Button();
            this.btn_move = new System.Windows.Forms.Button();
            this.btn_start = new System.Windows.Forms.Button();
            this.btn_heading = new System.Windows.Forms.Button();
            this.btn_log = new System.Windows.Forms.Button();
            this.btn_cmd_st = new System.Windows.Forms.Button();
            this.btn_end = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_load = new System.Windows.Forms.Button();
            this.nud_retry = new System.Windows.Forms.NumericUpDown();
            this.btn_del = new System.Windows.Forms.Button();
            this.tb_LDS2 = new System.Windows.Forms.TextBox();
            this.tb_LDS1 = new System.Windows.Forms.TextBox();
            this.btn_Pulse_Out = new System.Windows.Forms.Button();
            this.btn_CORRECTION = new System.Windows.Forms.Button();
            this.cb_com = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.cb_sim = new System.Windows.Forms.CheckBox();
            this.lb_log = new System.Windows.Forms.ListBox();
            this.bg_retry = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cameraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rFIDReaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aMCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bg_modbus = new System.ComponentModel.BackgroundWorker();
            this.bg_Timer = new System.ComponentModel.BackgroundWorker();
            this.Zigbee = new System.IO.Ports.SerialPort(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.btn_DOOR_OPEN = new System.Windows.Forms.Button();
            this.btn_DOOR_CLOSE = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.bg_ungrip = new System.ComponentModel.BackgroundWorker();
            this.bg_LD_IO_ST = new System.ComponentModel.BackgroundWorker();
            this.bg_latch = new System.ComponentModel.BackgroundWorker();
            this.bg_LD_OUT = new System.ComponentModel.BackgroundWorker();
            this.bg_zigbee = new System.ComponentModel.BackgroundWorker();
            this.bg_AGVLocation = new System.ComponentModel.BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.nud_retry)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bg_CMD
            // 
            this.bg_CMD.WorkerSupportsCancellation = true;
            this.bg_CMD.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bg_CMD_DoWork);
            // 
            // bg_reseve
            // 
            this.bg_reseve.WorkerSupportsCancellation = true;
            this.bg_reseve.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bg_reseve_DoWork);
            // 
            // bg_st
            // 
            this.bg_st.WorkerSupportsCancellation = true;
            this.bg_st.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bg_st_DoWork);
            // 
            // bg_Display
            // 
            this.bg_Display.WorkerSupportsCancellation = true;
            this.bg_Display.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bg_Display_DoWork);
            // 
            // bg_Conveyor
            // 
            this.bg_Conveyor.WorkerSupportsCancellation = true;
            this.bg_Conveyor.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bg_Conveyor_DoWork);
            // 
            // bg_SIM
            // 
            this.bg_SIM.WorkerSupportsCancellation = true;
            this.bg_SIM.DoWork += new System.ComponentModel.DoWorkEventHandler(this.SIM_TIME_DoWork);
            // 
            // lb_cmd
            // 
            this.lb_cmd.FormattingEnabled = true;
            this.lb_cmd.ItemHeight = 12;
            this.lb_cmd.Location = new System.Drawing.Point(10, 34);
            this.lb_cmd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lb_cmd.Name = "lb_cmd";
            this.lb_cmd.Size = new System.Drawing.Size(462, 232);
            this.lb_cmd.TabIndex = 0;
            this.lb_cmd.Click += new System.EventHandler(this.lb_cmd_Click);
            // 
            // btn_GoGoal
            // 
            this.btn_GoGoal.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_GoGoal.Location = new System.Drawing.Point(149, 270);
            this.btn_GoGoal.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_GoGoal.Name = "btn_GoGoal";
            this.btn_GoGoal.Size = new System.Drawing.Size(133, 31);
            this.btn_GoGoal.TabIndex = 1;
            this.btn_GoGoal.Text = "Go Goal";
            this.btn_GoGoal.UseVisualStyleBackColor = true;
            this.btn_GoGoal.Click += new System.EventHandler(this.btn_GoGoal_Click);
            // 
            // btn_move
            // 
            this.btn_move.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_move.Location = new System.Drawing.Point(149, 306);
            this.btn_move.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_move.Name = "btn_move";
            this.btn_move.Size = new System.Drawing.Size(133, 31);
            this.btn_move.TabIndex = 2;
            this.btn_move.Text = "Move";
            this.btn_move.UseVisualStyleBackColor = true;
            this.btn_move.Click += new System.EventHandler(this.btn_move_Click);
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(478, 33);
            this.btn_start.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(66, 18);
            this.btn_start.TabIndex = 3;
            this.btn_start.Text = "START";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // btn_heading
            // 
            this.btn_heading.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_heading.Location = new System.Drawing.Point(287, 270);
            this.btn_heading.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_heading.Name = "btn_heading";
            this.btn_heading.Size = new System.Drawing.Size(133, 31);
            this.btn_heading.TabIndex = 4;
            this.btn_heading.Text = "DeltaHeading";
            this.btn_heading.UseVisualStyleBackColor = true;
            this.btn_heading.Click += new System.EventHandler(this.btn_heading_Click);
            // 
            // btn_log
            // 
            this.btn_log.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_log.Location = new System.Drawing.Point(287, 306);
            this.btn_log.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_log.Name = "btn_log";
            this.btn_log.Size = new System.Drawing.Size(133, 31);
            this.btn_log.TabIndex = 5;
            this.btn_log.Text = "Log";
            this.btn_log.UseVisualStyleBackColor = true;
            this.btn_log.Click += new System.EventHandler(this.btn_log_Click);
            // 
            // btn_cmd_st
            // 
            this.btn_cmd_st.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_cmd_st.Location = new System.Drawing.Point(10, 270);
            this.btn_cmd_st.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_cmd_st.Name = "btn_cmd_st";
            this.btn_cmd_st.Size = new System.Drawing.Size(133, 31);
            this.btn_cmd_st.TabIndex = 6;
            this.btn_cmd_st.Text = "START";
            this.btn_cmd_st.UseVisualStyleBackColor = true;
            this.btn_cmd_st.Click += new System.EventHandler(this.btn_cmd_st_Click);
            // 
            // btn_end
            // 
            this.btn_end.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_end.Location = new System.Drawing.Point(10, 306);
            this.btn_end.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_end.Name = "btn_end";
            this.btn_end.Size = new System.Drawing.Size(133, 31);
            this.btn_end.TabIndex = 7;
            this.btn_end.Text = "END";
            this.btn_end.UseVisualStyleBackColor = true;
            this.btn_end.Click += new System.EventHandler(this.btn_end_Click);
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(478, 56);
            this.btn_save.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(66, 18);
            this.btn_save.TabIndex = 8;
            this.btn_save.Text = "SAVE";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_load
            // 
            this.btn_load.Location = new System.Drawing.Point(478, 79);
            this.btn_load.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_load.Name = "btn_load";
            this.btn_load.Size = new System.Drawing.Size(66, 18);
            this.btn_load.TabIndex = 9;
            this.btn_load.Text = "LOAD";
            this.btn_load.UseVisualStyleBackColor = true;
            this.btn_load.Click += new System.EventHandler(this.btn_load_Click);
            // 
            // nud_retry
            // 
            this.nud_retry.Location = new System.Drawing.Point(478, 198);
            this.nud_retry.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nud_retry.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_retry.Name = "nud_retry";
            this.nud_retry.ReadOnly = true;
            this.nud_retry.Size = new System.Drawing.Size(66, 21);
            this.nud_retry.TabIndex = 10;
            this.nud_retry.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btn_del
            // 
            this.btn_del.Location = new System.Drawing.Point(478, 102);
            this.btn_del.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_del.Name = "btn_del";
            this.btn_del.Size = new System.Drawing.Size(66, 18);
            this.btn_del.TabIndex = 12;
            this.btn_del.Text = "DELETE";
            this.btn_del.UseVisualStyleBackColor = true;
            this.btn_del.Click += new System.EventHandler(this.btn_del_Click);
            // 
            // tb_LDS2
            // 
            this.tb_LDS2.Location = new System.Drawing.Point(478, 248);
            this.tb_LDS2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tb_LDS2.Name = "tb_LDS2";
            this.tb_LDS2.ReadOnly = true;
            this.tb_LDS2.Size = new System.Drawing.Size(66, 21);
            this.tb_LDS2.TabIndex = 14;
            // 
            // tb_LDS1
            // 
            this.tb_LDS1.Location = new System.Drawing.Point(478, 223);
            this.tb_LDS1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tb_LDS1.Name = "tb_LDS1";
            this.tb_LDS1.ReadOnly = true;
            this.tb_LDS1.Size = new System.Drawing.Size(66, 21);
            this.tb_LDS1.TabIndex = 15;
            // 
            // btn_Pulse_Out
            // 
            this.btn_Pulse_Out.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_Pulse_Out.Location = new System.Drawing.Point(425, 270);
            this.btn_Pulse_Out.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Pulse_Out.Name = "btn_Pulse_Out";
            this.btn_Pulse_Out.Size = new System.Drawing.Size(118, 31);
            this.btn_Pulse_Out.TabIndex = 16;
            this.btn_Pulse_Out.Text = "On Pulse";
            this.btn_Pulse_Out.UseVisualStyleBackColor = true;
            this.btn_Pulse_Out.Click += new System.EventHandler(this.btn_Pulse_Out_Click);
            // 
            // btn_CORRECTION
            // 
            this.btn_CORRECTION.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.btn_CORRECTION.Location = new System.Drawing.Point(425, 306);
            this.btn_CORRECTION.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_CORRECTION.Name = "btn_CORRECTION";
            this.btn_CORRECTION.Size = new System.Drawing.Size(118, 31);
            this.btn_CORRECTION.TabIndex = 17;
            this.btn_CORRECTION.Text = "CORRECTION";
            this.btn_CORRECTION.UseVisualStyleBackColor = true;
            this.btn_CORRECTION.Click += new System.EventHandler(this.btn_check_io_Click);
            // 
            // cb_com
            // 
            this.cb_com.FormattingEnabled = true;
            this.cb_com.Location = new System.Drawing.Point(478, 175);
            this.cb_com.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cb_com.Name = "cb_com";
            this.cb_com.Size = new System.Drawing.Size(66, 20);
            this.cb_com.TabIndex = 18;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(478, 150);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(66, 21);
            this.textBox1.TabIndex = 19;
            this.textBox1.Text = "0";
            // 
            // cb_sim
            // 
            this.cb_sim.AutoSize = true;
            this.cb_sim.Location = new System.Drawing.Point(478, 130);
            this.cb_sim.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cb_sim.Name = "cb_sim";
            this.cb_sim.Size = new System.Drawing.Size(56, 16);
            this.cb_sim.TabIndex = 20;
            this.cb_sim.Text = "Simul";
            this.cb_sim.UseVisualStyleBackColor = true;
            // 
            // lb_log
            // 
            this.lb_log.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lb_log.FormattingEnabled = true;
            this.lb_log.ItemHeight = 12;
            this.lb_log.Items.AddRange(new object[] {
            "listbox"});
            this.lb_log.Location = new System.Drawing.Point(549, 34);
            this.lb_log.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lb_log.Name = "lb_log";
            this.lb_log.Size = new System.Drawing.Size(777, 292);
            this.lb_log.TabIndex = 21;
            // 
            // bg_retry
            // 
            this.bg_retry.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bg_retry_DoWork);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1335, 24);
            this.menuStrip1.TabIndex = 23;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(12, 20);
            // 
            // toolToolStripMenuItem
            // 
            this.toolToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cameraToolStripMenuItem,
            this.rFIDReaderToolStripMenuItem,
            this.lDToolStripMenuItem,
            this.aMCToolStripMenuItem});
            this.toolToolStripMenuItem.Name = "toolToolStripMenuItem";
            this.toolToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.toolToolStripMenuItem.Text = "&Tool";
            // 
            // cameraToolStripMenuItem
            // 
            this.cameraToolStripMenuItem.Name = "cameraToolStripMenuItem";
            this.cameraToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.cameraToolStripMenuItem.Text = "&Camera";
            this.cameraToolStripMenuItem.Click += new System.EventHandler(this.cameraToolStripMenuItem_Click);
            // 
            // rFIDReaderToolStripMenuItem
            // 
            this.rFIDReaderToolStripMenuItem.Name = "rFIDReaderToolStripMenuItem";
            this.rFIDReaderToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.rFIDReaderToolStripMenuItem.Text = "&RFID Reader";
            this.rFIDReaderToolStripMenuItem.Click += new System.EventHandler(this.rFIDReaderToolStripMenuItem_Click);
            // 
            // lDToolStripMenuItem
            // 
            this.lDToolStripMenuItem.Name = "lDToolStripMenuItem";
            this.lDToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.lDToolStripMenuItem.Text = "&LD";
            this.lDToolStripMenuItem.Click += new System.EventHandler(this.lDToolStripMenuItem_Click);
            // 
            // aMCToolStripMenuItem
            // 
            this.aMCToolStripMenuItem.Name = "aMCToolStripMenuItem";
            this.aMCToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.aMCToolStripMenuItem.Text = "&AMC";
            this.aMCToolStripMenuItem.Click += new System.EventHandler(this.aMCToolStripMenuItem_Click);
            // 
            // bg_modbus
            // 
            this.bg_modbus.WorkerSupportsCancellation = true;
            this.bg_modbus.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bg_modbus_DoWork);
            // 
            // bg_Timer
            // 
            this.bg_Timer.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bg_Timer_DoWork);
            // 
            // Zigbee
            // 
            this.Zigbee.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.Zigbee_DataReceived);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(587, 56);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 24;
            this.button1.Text = "IN_OPEN";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(587, 97);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 25;
            this.button2.Text = "IN_CLOSE";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(695, 56);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 26;
            this.button3.Text = "OUT_OPEN";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(695, 97);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 27;
            this.button4.Text = "OUT_CLOSE";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // btn_DOOR_OPEN
            // 
            this.btn_DOOR_OPEN.Location = new System.Drawing.Point(809, 56);
            this.btn_DOOR_OPEN.Name = "btn_DOOR_OPEN";
            this.btn_DOOR_OPEN.Size = new System.Drawing.Size(75, 23);
            this.btn_DOOR_OPEN.TabIndex = 26;
            this.btn_DOOR_OPEN.Text = "DOOR_OPEN";
            this.btn_DOOR_OPEN.UseVisualStyleBackColor = true;
            this.btn_DOOR_OPEN.Click += new System.EventHandler(this.Btn_DOOR_OPEN_Click);
            // 
            // btn_DOOR_CLOSE
            // 
            this.btn_DOOR_CLOSE.Location = new System.Drawing.Point(809, 97);
            this.btn_DOOR_CLOSE.Name = "btn_DOOR_CLOSE";
            this.btn_DOOR_CLOSE.Size = new System.Drawing.Size(75, 23);
            this.btn_DOOR_CLOSE.TabIndex = 27;
            this.btn_DOOR_CLOSE.Text = "DOOR_CLOSE";
            this.btn_DOOR_CLOSE.UseVisualStyleBackColor = true;
            this.btn_DOOR_CLOSE.Click += new System.EventHandler(this.Btn_DOOR_CLOSE_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(609, 187);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 21);
            this.textBox2.TabIndex = 28;
            // 
            // bg_ungrip
            // 
            this.bg_ungrip.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Bg_ungrip_DoWork);
            // 
            // bg_LD_IO_ST
            // 
            this.bg_LD_IO_ST.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bg_LD_IO_ST_DoWork);
            // 
            // bg_latch
            // 
            this.bg_latch.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bg_latch_DoWork);
            // 
            // bg_LD_OUT
            // 
            this.bg_LD_OUT.DoWork += new System.ComponentModel.DoWorkEventHandler(this.LD_OUT_DoWork);
            // 
            // bg_zigbee
            // 
            this.bg_zigbee.WorkerSupportsCancellation = true;
            this.bg_zigbee.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bg_zigbee_DoWork);
            // 
            // bg_AGVLocation
            // 
            this.bg_AGVLocation.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bg_AGVLocation_DoWork);
            // 
            // timer1
            // 
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1335, 342);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.btn_DOOR_CLOSE);
            this.Controls.Add(this.btn_DOOR_OPEN);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cb_sim);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.cb_com);
            this.Controls.Add(this.btn_CORRECTION);
            this.Controls.Add(this.btn_Pulse_Out);
            this.Controls.Add(this.tb_LDS1);
            this.Controls.Add(this.tb_LDS2);
            this.Controls.Add(this.btn_del);
            this.Controls.Add(this.nud_retry);
            this.Controls.Add(this.btn_load);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.btn_end);
            this.Controls.Add(this.btn_cmd_st);
            this.Controls.Add(this.btn_log);
            this.Controls.Add(this.btn_heading);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.btn_move);
            this.Controls.Add(this.btn_GoGoal);
            this.Controls.Add(this.lb_cmd);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.lb_log);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.ShowInTaskbar = false;
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nud_retry)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.ComponentModel.BackgroundWorker bg_CMD;
        private System.ComponentModel.BackgroundWorker bg_reseve;
        private System.ComponentModel.BackgroundWorker bg_st;
        private System.ComponentModel.BackgroundWorker bg_Display;
        private System.ComponentModel.BackgroundWorker bg_Conveyor;
        private System.ComponentModel.BackgroundWorker bg_SIM;
        private System.Windows.Forms.ListBox lb_cmd;
        private System.Windows.Forms.Button btn_GoGoal;
        private System.Windows.Forms.Button btn_move;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Button btn_heading;
        private System.Windows.Forms.Button btn_log;
        private System.Windows.Forms.Button btn_cmd_st;
        private System.Windows.Forms.Button btn_end;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button btn_load;
        private System.Windows.Forms.NumericUpDown nud_retry;
        private System.Windows.Forms.Button btn_del;
        private System.Windows.Forms.TextBox tb_LDS2;
        private System.Windows.Forms.TextBox tb_LDS1;
        private System.Windows.Forms.Button btn_Pulse_Out;
        private System.Windows.Forms.Button btn_CORRECTION;
        private System.Windows.Forms.ComboBox cb_com;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox cb_sim;
        private System.Windows.Forms.ListBox lb_log;
        private System.ComponentModel.BackgroundWorker bg_retry;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cameraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rFIDReaderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aMCToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker bg_modbus;
        private System.ComponentModel.BackgroundWorker bg_Timer;
        private System.IO.Ports.SerialPort Zigbee;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button btn_DOOR_OPEN;
        private System.Windows.Forms.Button btn_DOOR_CLOSE;
        private System.Windows.Forms.TextBox textBox2;
        private System.ComponentModel.BackgroundWorker bg_ungrip;
        private System.ComponentModel.BackgroundWorker bg_LD_IO_ST;
        private System.ComponentModel.BackgroundWorker bg_latch;
        private System.ComponentModel.BackgroundWorker bg_LD_OUT;
        private System.ComponentModel.BackgroundWorker bg_zigbee;
        private System.ComponentModel.BackgroundWorker bg_AGVLocation;
        private System.Windows.Forms.Timer timer1;
    }
}

