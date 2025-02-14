﻿namespace AMC_Test
{
    partial class HMI_Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Zigbee = new System.IO.Ports.SerialPort(this.components);
            this.btn_open = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.btn_connect = new System.Windows.Forms.Button();
            this.Serial_Timer = new System.Windows.Forms.Timer(this.components);
            this.Main_Task = new System.ComponentModel.BackgroundWorker();
            this.btn_setting = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.bg_reseve = new System.ComponentModel.BackgroundWorker();
            this.lb_log = new System.Windows.Forms.ListBox();
            this.t_saver = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dateTimeDisplay1 = new AdvancedHMIControls.DateTimeDisplay();
            this.label7 = new System.Windows.Forms.Label();
            this.tb_volt = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.l_charge = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.tb_lo_a = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tb_lo_y = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tb_lo_x = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tb_odo = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tb_wifi = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tb_temp = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.pb_i16 = new System.Windows.Forms.PictureBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.pb_i15 = new System.Windows.Forms.PictureBox();
            this.pb_i14 = new System.Windows.Forms.PictureBox();
            this.pb_i13 = new System.Windows.Forms.PictureBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.pb_i12 = new System.Windows.Forms.PictureBox();
            this.pb_i11 = new System.Windows.Forms.PictureBox();
            this.pb_i10 = new System.Windows.Forms.PictureBox();
            this.label23 = new System.Windows.Forms.Label();
            this.pb_i9 = new System.Windows.Forms.PictureBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.pb_i8 = new System.Windows.Forms.PictureBox();
            this.pb_i7 = new System.Windows.Forms.PictureBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.pb_i6 = new System.Windows.Forms.PictureBox();
            this.pb_i5 = new System.Windows.Forms.PictureBox();
            this.pb_i4 = new System.Windows.Forms.PictureBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.pb_i3 = new System.Windows.Forms.PictureBox();
            this.pb_i2 = new System.Windows.Forms.PictureBox();
            this.pb_i1 = new System.Windows.Forms.PictureBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label40 = new System.Windows.Forms.Label();
            this.pb_o16 = new System.Windows.Forms.PictureBox();
            this.label41 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.pb_o15 = new System.Windows.Forms.PictureBox();
            this.pb_o14 = new System.Windows.Forms.PictureBox();
            this.pb_o13 = new System.Windows.Forms.PictureBox();
            this.label44 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.pb_o12 = new System.Windows.Forms.PictureBox();
            this.pb_o11 = new System.Windows.Forms.PictureBox();
            this.pb_o10 = new System.Windows.Forms.PictureBox();
            this.label31 = new System.Windows.Forms.Label();
            this.pb_o9 = new System.Windows.Forms.PictureBox();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.pb_o8 = new System.Windows.Forms.PictureBox();
            this.pb_o7 = new System.Windows.Forms.PictureBox();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.pb_o6 = new System.Windows.Forms.PictureBox();
            this.pb_o5 = new System.Windows.Forms.PictureBox();
            this.pb_o4 = new System.Windows.Forms.PictureBox();
            this.label37 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.pb_o3 = new System.Windows.Forms.PictureBox();
            this.pb_o2 = new System.Windows.Forms.PictureBox();
            this.pb_o1 = new System.Windows.Forms.PictureBox();
            this.l_status = new System.Windows.Forms.Label();
            this.bg_LDS = new System.ComponentModel.BackgroundWorker();
            this.bg_deltaHeading = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_i16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_i15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_i14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_i13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_i12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_i11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_i10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_i9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_i8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_i7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_i6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_i5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_i4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_i3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_i2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_i1)).BeginInit();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_o16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_o15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_o14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_o13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_o12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_o11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_o10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_o9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_o8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_o7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_o6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_o5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_o4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_o3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_o2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_o1)).BeginInit();
            this.SuspendLayout();
            // 
            // Zigbee
            // 
            this.Zigbee.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.Zigbee_DataReceived);
            // 
            // btn_open
            // 
            this.btn_open.Location = new System.Drawing.Point(6, 145);
            this.btn_open.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_open.Name = "btn_open";
            this.btn_open.Size = new System.Drawing.Size(66, 18);
            this.btn_open.TabIndex = 0;
            this.btn_open.Text = "OPEN";
            this.btn_open.UseVisualStyleBackColor = true;
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(5, 168);
            this.btn_close.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(66, 18);
            this.btn_close.TabIndex = 1;
            this.btn_close.Text = "CLOSE";
            this.btn_close.UseVisualStyleBackColor = true;
            // 
            // btn_connect
            // 
            this.btn_connect.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_connect.Location = new System.Drawing.Point(4, 48);
            this.btn_connect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_connect.Name = "btn_connect";
            this.btn_connect.Size = new System.Drawing.Size(90, 36);
            this.btn_connect.TabIndex = 2;
            this.btn_connect.Text = "START";
            this.btn_connect.UseVisualStyleBackColor = true;
            this.btn_connect.Click += new System.EventHandler(this.btn_connect_Click);
            // 
            // Serial_Timer
            // 
            this.Serial_Timer.Tick += new System.EventHandler(this.Serial_Timer_Tick);
            // 
            // Main_Task
            // 
            this.Main_Task.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Main_Task_DoWork);
            // 
            // btn_setting
            // 
            this.btn_setting.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_setting.Location = new System.Drawing.Point(4, 5);
            this.btn_setting.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_setting.Name = "btn_setting";
            this.btn_setting.Size = new System.Drawing.Size(90, 38);
            this.btn_setting.TabIndex = 3;
            this.btn_setting.Text = "Setting";
            this.btn_setting.UseVisualStyleBackColor = true;
            this.btn_setting.Click += new System.EventHandler(this.button1_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(28, 214);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(66, 18);
            this.button1.TabIndex = 4;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // bg_reseve
            // 
            this.bg_reseve.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bg_reseve_DoWork);
            // 
            // lb_log
            // 
            this.lb_log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_log.Enabled = false;
            this.lb_log.FormattingEnabled = true;
            this.lb_log.ItemHeight = 12;
            this.lb_log.Items.AddRange(new object[] {
            "Log Box"});
            this.lb_log.Location = new System.Drawing.Point(0, 0);
            this.lb_log.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lb_log.Name = "lb_log";
            this.lb_log.Size = new System.Drawing.Size(880, 127);
            this.lb_log.TabIndex = 5;
            // 
            // t_saver
            // 
            this.t_saver.Interval = 500000;
            this.t_saver.Tick += new System.EventHandler(this.t_saver_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 29);
            this.label1.TabIndex = 6;
            this.label1.Text = "Volt";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 29);
            this.label2.TabIndex = 7;
            this.label2.Text = "Temp";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(14, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 29);
            this.label3.TabIndex = 8;
            this.label3.Text = "WIFI";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(293, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 29);
            this.label4.TabIndex = 11;
            this.label4.Text = "Location-Y";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(293, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(138, 29);
            this.label5.TabIndex = 10;
            this.label5.Text = "Location-X";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(14, 117);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 29);
            this.label6.TabIndex = 9;
            this.label6.Text = "Odometer";
            // 
            // dateTimeDisplay1
            // 
            this.dateTimeDisplay1.AutoSize = true;
            this.dateTimeDisplay1.DisplayFormat = "yyyy/MM/dd  hh:mm:ss";
            this.dateTimeDisplay1.Font = new System.Drawing.Font("Arial", 30F, System.Drawing.FontStyle.Bold);
            this.dateTimeDisplay1.ForeColor = System.Drawing.Color.Black;
            this.dateTimeDisplay1.Location = new System.Drawing.Point(10, 5);
            this.dateTimeDisplay1.Name = "dateTimeDisplay1";
            this.dateTimeDisplay1.Size = new System.Drawing.Size(404, 46);
            this.dateTimeDisplay1.TabIndex = 15;
            this.dateTimeDisplay1.Text = "2021-01-13  03:09:17";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(293, 79);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(189, 29);
            this.label7.TabIndex = 16;
            this.label7.Text = "Location-Angle";
            // 
            // tb_volt
            // 
            this.tb_volt.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_volt.Location = new System.Drawing.Point(121, 6);
            this.tb_volt.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tb_volt.Name = "tb_volt";
            this.tb_volt.ReadOnly = true;
            this.tb_volt.Size = new System.Drawing.Size(112, 26);
            this.tb_volt.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(238, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(20, 19);
            this.label8.TabIndex = 18;
            this.label8.Text = "V";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lb_log);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 315);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(880, 127);
            this.panel1.TabIndex = 19;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel7);
            this.panel2.Controls.Add(this.panel6);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(777, 56);
            this.panel2.TabIndex = 20;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.l_charge);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel7.Location = new System.Drawing.Point(588, 0);
            this.panel7.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(189, 56);
            this.panel7.TabIndex = 17;
            // 
            // l_charge
            // 
            this.l_charge.AutoSize = true;
            this.l_charge.Dock = System.Windows.Forms.DockStyle.Right;
            this.l_charge.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l_charge.Location = new System.Drawing.Point(110, 0);
            this.l_charge.Name = "l_charge";
            this.l_charge.Size = new System.Drawing.Size(79, 37);
            this.l_charge.TabIndex = 0;
            this.l_charge.Text = "00%";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.dateTimeDisplay1);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(457, 56);
            this.panel6.TabIndex = 16;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.button2);
            this.panel3.Controls.Add(this.button3);
            this.panel3.Controls.Add(this.textBox5);
            this.panel3.Controls.Add(this.button1);
            this.panel3.Controls.Add(this.btn_setting);
            this.panel3.Controls.Add(this.btn_connect);
            this.panel3.Controls.Add(this.btn_close);
            this.panel3.Controls.Add(this.btn_open);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(777, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(103, 315);
            this.panel3.TabIndex = 21;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(13, 271);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(66, 18);
            this.button2.TabIndex = 7;
            this.button2.Text = "CLOSE";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(14, 248);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(66, 18);
            this.button3.TabIndex = 6;
            this.button3.Text = "OPEN";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(13, 110);
            this.textBox5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(88, 21);
            this.textBox5.TabIndex = 5;
            this.textBox5.Text = "1";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label12);
            this.panel4.Controls.Add(this.tb_lo_a);
            this.panel4.Controls.Add(this.label13);
            this.panel4.Controls.Add(this.tb_lo_y);
            this.panel4.Controls.Add(this.label14);
            this.panel4.Controls.Add(this.tb_lo_x);
            this.panel4.Controls.Add(this.label11);
            this.panel4.Controls.Add(this.tb_odo);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Controls.Add(this.tb_wifi);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Controls.Add(this.tb_temp);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.tb_volt);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 56);
            this.panel4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(777, 259);
            this.panel4.TabIndex = 22;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(636, 89);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(15, 19);
            this.label12.TabIndex = 30;
            this.label12.Text = "º";
            // 
            // tb_lo_a
            // 
            this.tb_lo_a.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_lo_a.Location = new System.Drawing.Point(519, 84);
            this.tb_lo_a.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tb_lo_a.Name = "tb_lo_a";
            this.tb_lo_a.ReadOnly = true;
            this.tb_lo_a.Size = new System.Drawing.Size(112, 26);
            this.tb_lo_a.TabIndex = 29;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(636, 51);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(20, 19);
            this.label13.TabIndex = 28;
            this.label13.Text = "P";
            // 
            // tb_lo_y
            // 
            this.tb_lo_y.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_lo_y.Location = new System.Drawing.Point(519, 46);
            this.tb_lo_y.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tb_lo_y.Name = "tb_lo_y";
            this.tb_lo_y.ReadOnly = true;
            this.tb_lo_y.Size = new System.Drawing.Size(112, 26);
            this.tb_lo_y.TabIndex = 27;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(636, 10);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(20, 19);
            this.label14.TabIndex = 26;
            this.label14.Text = "P";
            // 
            // tb_lo_x
            // 
            this.tb_lo_x.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_lo_x.Location = new System.Drawing.Point(519, 6);
            this.tb_lo_x.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tb_lo_x.Name = "tb_lo_x";
            this.tb_lo_x.ReadOnly = true;
            this.tb_lo_x.Size = new System.Drawing.Size(112, 26);
            this.tb_lo_x.TabIndex = 25;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(273, 126);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 19);
            this.label11.TabIndex = 24;
            this.label11.Text = "Km";
            // 
            // tb_odo
            // 
            this.tb_odo.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_odo.Location = new System.Drawing.Point(156, 121);
            this.tb_odo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tb_odo.Name = "tb_odo";
            this.tb_odo.ReadOnly = true;
            this.tb_odo.Size = new System.Drawing.Size(112, 26);
            this.tb_odo.TabIndex = 23;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(238, 89);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(25, 19);
            this.label10.TabIndex = 22;
            this.label10.Text = "%";
            // 
            // tb_wifi
            // 
            this.tb_wifi.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_wifi.Location = new System.Drawing.Point(121, 84);
            this.tb_wifi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tb_wifi.Name = "tb_wifi";
            this.tb_wifi.ReadOnly = true;
            this.tb_wifi.Size = new System.Drawing.Size(112, 26);
            this.tb_wifi.TabIndex = 21;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(238, 51);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(22, 19);
            this.label9.TabIndex = 20;
            this.label9.Text = "℃";
            // 
            // tb_temp
            // 
            this.tb_temp.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_temp.Location = new System.Drawing.Point(121, 46);
            this.tb_temp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tb_temp.Name = "tb_temp";
            this.tb_temp.ReadOnly = true;
            this.tb_temp.Size = new System.Drawing.Size(112, 26);
            this.tb_temp.TabIndex = 19;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel9);
            this.panel5.Controls.Add(this.panel8);
            this.panel5.Controls.Add(this.l_status);
            this.panel5.Location = new System.Drawing.Point(0, 206);
            this.panel5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(777, 110);
            this.panel5.TabIndex = 23;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.textBox2);
            this.panel9.Controls.Add(this.textBox1);
            this.panel9.Controls.Add(this.label30);
            this.panel9.Controls.Add(this.pb_i16);
            this.panel9.Controls.Add(this.label27);
            this.panel9.Controls.Add(this.label28);
            this.panel9.Controls.Add(this.label29);
            this.panel9.Controls.Add(this.pb_i15);
            this.panel9.Controls.Add(this.pb_i14);
            this.panel9.Controls.Add(this.pb_i13);
            this.panel9.Controls.Add(this.label24);
            this.panel9.Controls.Add(this.label25);
            this.panel9.Controls.Add(this.label26);
            this.panel9.Controls.Add(this.pb_i12);
            this.panel9.Controls.Add(this.pb_i11);
            this.panel9.Controls.Add(this.pb_i10);
            this.panel9.Controls.Add(this.label23);
            this.panel9.Controls.Add(this.pb_i9);
            this.panel9.Controls.Add(this.label21);
            this.panel9.Controls.Add(this.label22);
            this.panel9.Controls.Add(this.pb_i8);
            this.panel9.Controls.Add(this.pb_i7);
            this.panel9.Controls.Add(this.label18);
            this.panel9.Controls.Add(this.label19);
            this.panel9.Controls.Add(this.label20);
            this.panel9.Controls.Add(this.pb_i6);
            this.panel9.Controls.Add(this.pb_i5);
            this.panel9.Controls.Add(this.pb_i4);
            this.panel9.Controls.Add(this.label15);
            this.panel9.Controls.Add(this.label16);
            this.panel9.Controls.Add(this.label17);
            this.panel9.Controls.Add(this.pb_i3);
            this.panel9.Controls.Add(this.pb_i2);
            this.panel9.Controls.Add(this.pb_i1);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel9.Location = new System.Drawing.Point(0, 0);
            this.panel9.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(388, 110);
            this.panel9.TabIndex = 2;
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(349, 39);
            this.textBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(25, 59);
            this.textBox2.TabIndex = 75;
            this.textBox2.Text = "P\r\nU\r\nT";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(330, 39);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(25, 44);
            this.textBox1.TabIndex = 74;
            this.textBox1.Text = "I\r\nN";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(318, 12);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(17, 12);
            this.label30.TabIndex = 73;
            this.label30.Text = "16";
            // 
            // pb_i16
            // 
            this.pb_i16.Image = global::AMC_Test.Properties.Resources.circle_grey;
            this.pb_i16.Location = new System.Drawing.Point(343, 2);
            this.pb_i16.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pb_i16.Name = "pb_i16";
            this.pb_i16.Size = new System.Drawing.Size(35, 32);
            this.pb_i16.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_i16.TabIndex = 72;
            this.pb_i16.TabStop = false;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(252, 86);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(17, 12);
            this.label27.TabIndex = 71;
            this.label27.Text = "15";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(252, 49);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(17, 12);
            this.label28.TabIndex = 70;
            this.label28.Text = "14";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(252, 12);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(17, 12);
            this.label29.TabIndex = 69;
            this.label29.Text = "13";
            // 
            // pb_i15
            // 
            this.pb_i15.Image = global::AMC_Test.Properties.Resources.circle_grey;
            this.pb_i15.Location = new System.Drawing.Point(277, 76);
            this.pb_i15.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pb_i15.Name = "pb_i15";
            this.pb_i15.Size = new System.Drawing.Size(35, 32);
            this.pb_i15.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_i15.TabIndex = 68;
            this.pb_i15.TabStop = false;
            // 
            // pb_i14
            // 
            this.pb_i14.Image = global::AMC_Test.Properties.Resources.circle_grey;
            this.pb_i14.Location = new System.Drawing.Point(277, 39);
            this.pb_i14.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pb_i14.Name = "pb_i14";
            this.pb_i14.Size = new System.Drawing.Size(35, 32);
            this.pb_i14.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_i14.TabIndex = 67;
            this.pb_i14.TabStop = false;
            // 
            // pb_i13
            // 
            this.pb_i13.Image = global::AMC_Test.Properties.Resources.circle_grey;
            this.pb_i13.Location = new System.Drawing.Point(277, 2);
            this.pb_i13.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pb_i13.Name = "pb_i13";
            this.pb_i13.Size = new System.Drawing.Size(35, 32);
            this.pb_i13.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_i13.TabIndex = 66;
            this.pb_i13.TabStop = false;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(186, 86);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(17, 12);
            this.label24.TabIndex = 65;
            this.label24.Text = "12";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(186, 49);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(17, 12);
            this.label25.TabIndex = 64;
            this.label25.Text = "11";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(186, 12);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(17, 12);
            this.label26.TabIndex = 63;
            this.label26.Text = "10";
            // 
            // pb_i12
            // 
            this.pb_i12.Image = global::AMC_Test.Properties.Resources.circle_grey;
            this.pb_i12.Location = new System.Drawing.Point(212, 75);
            this.pb_i12.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pb_i12.Name = "pb_i12";
            this.pb_i12.Size = new System.Drawing.Size(35, 32);
            this.pb_i12.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_i12.TabIndex = 62;
            this.pb_i12.TabStop = false;
            // 
            // pb_i11
            // 
            this.pb_i11.Image = global::AMC_Test.Properties.Resources.circle_grey;
            this.pb_i11.Location = new System.Drawing.Point(212, 38);
            this.pb_i11.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pb_i11.Name = "pb_i11";
            this.pb_i11.Size = new System.Drawing.Size(35, 32);
            this.pb_i11.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_i11.TabIndex = 61;
            this.pb_i11.TabStop = false;
            // 
            // pb_i10
            // 
            this.pb_i10.Image = global::AMC_Test.Properties.Resources.circle_grey;
            this.pb_i10.Location = new System.Drawing.Point(212, 2);
            this.pb_i10.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pb_i10.Name = "pb_i10";
            this.pb_i10.Size = new System.Drawing.Size(35, 32);
            this.pb_i10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_i10.TabIndex = 60;
            this.pb_i10.TabStop = false;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(128, 86);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(11, 12);
            this.label23.TabIndex = 59;
            this.label23.Text = "9";
            // 
            // pb_i9
            // 
            this.pb_i9.Image = global::AMC_Test.Properties.Resources.circle_grey;
            this.pb_i9.Location = new System.Drawing.Point(146, 74);
            this.pb_i9.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pb_i9.Name = "pb_i9";
            this.pb_i9.Size = new System.Drawing.Size(35, 32);
            this.pb_i9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_i9.TabIndex = 58;
            this.pb_i9.TabStop = false;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(128, 49);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(11, 12);
            this.label21.TabIndex = 57;
            this.label21.Text = "8";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(128, 12);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(11, 12);
            this.label22.TabIndex = 56;
            this.label22.Text = "7";
            // 
            // pb_i8
            // 
            this.pb_i8.Image = global::AMC_Test.Properties.Resources.circle_grey;
            this.pb_i8.Location = new System.Drawing.Point(146, 38);
            this.pb_i8.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pb_i8.Name = "pb_i8";
            this.pb_i8.Size = new System.Drawing.Size(35, 32);
            this.pb_i8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_i8.TabIndex = 55;
            this.pb_i8.TabStop = false;
            // 
            // pb_i7
            // 
            this.pb_i7.Image = global::AMC_Test.Properties.Resources.circle_grey;
            this.pb_i7.Location = new System.Drawing.Point(146, 1);
            this.pb_i7.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pb_i7.Name = "pb_i7";
            this.pb_i7.Size = new System.Drawing.Size(35, 32);
            this.pb_i7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_i7.TabIndex = 54;
            this.pb_i7.TabStop = false;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(69, 86);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(11, 12);
            this.label18.TabIndex = 53;
            this.label18.Text = "6";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(69, 49);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(11, 12);
            this.label19.TabIndex = 52;
            this.label19.Text = "5";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(69, 12);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(11, 12);
            this.label20.TabIndex = 51;
            this.label20.Text = "4";
            // 
            // pb_i6
            // 
            this.pb_i6.Image = global::AMC_Test.Properties.Resources.circle_grey;
            this.pb_i6.Location = new System.Drawing.Point(88, 75);
            this.pb_i6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pb_i6.Name = "pb_i6";
            this.pb_i6.Size = new System.Drawing.Size(35, 32);
            this.pb_i6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_i6.TabIndex = 50;
            this.pb_i6.TabStop = false;
            // 
            // pb_i5
            // 
            this.pb_i5.Image = global::AMC_Test.Properties.Resources.circle_grey;
            this.pb_i5.Location = new System.Drawing.Point(88, 38);
            this.pb_i5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pb_i5.Name = "pb_i5";
            this.pb_i5.Size = new System.Drawing.Size(35, 32);
            this.pb_i5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_i5.TabIndex = 49;
            this.pb_i5.TabStop = false;
            // 
            // pb_i4
            // 
            this.pb_i4.Image = global::AMC_Test.Properties.Resources.circle_grey;
            this.pb_i4.Location = new System.Drawing.Point(88, 2);
            this.pb_i4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pb_i4.Name = "pb_i4";
            this.pb_i4.Size = new System.Drawing.Size(35, 32);
            this.pb_i4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_i4.TabIndex = 48;
            this.pb_i4.TabStop = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(10, 86);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(11, 12);
            this.label15.TabIndex = 47;
            this.label15.Text = "3";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(10, 49);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(11, 12);
            this.label16.TabIndex = 46;
            this.label16.Text = "2";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(10, 12);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(11, 12);
            this.label17.TabIndex = 45;
            this.label17.Text = "1";
            // 
            // pb_i3
            // 
            this.pb_i3.Image = global::AMC_Test.Properties.Resources.circle_grey;
            this.pb_i3.Location = new System.Drawing.Point(29, 76);
            this.pb_i3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pb_i3.Name = "pb_i3";
            this.pb_i3.Size = new System.Drawing.Size(35, 32);
            this.pb_i3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_i3.TabIndex = 44;
            this.pb_i3.TabStop = false;
            // 
            // pb_i2
            // 
            this.pb_i2.Image = global::AMC_Test.Properties.Resources.circle_grey;
            this.pb_i2.Location = new System.Drawing.Point(29, 39);
            this.pb_i2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pb_i2.Name = "pb_i2";
            this.pb_i2.Size = new System.Drawing.Size(35, 32);
            this.pb_i2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_i2.TabIndex = 43;
            this.pb_i2.TabStop = false;
            this.pb_i2.Tag = "2";
            // 
            // pb_i1
            // 
            this.pb_i1.Image = global::AMC_Test.Properties.Resources.circle_grey;
            this.pb_i1.Location = new System.Drawing.Point(29, 2);
            this.pb_i1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pb_i1.Name = "pb_i1";
            this.pb_i1.Size = new System.Drawing.Size(35, 32);
            this.pb_i1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_i1.TabIndex = 42;
            this.pb_i1.TabStop = false;
            this.pb_i1.Tag = "1";
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.textBox3);
            this.panel8.Controls.Add(this.textBox4);
            this.panel8.Controls.Add(this.label40);
            this.panel8.Controls.Add(this.pb_o16);
            this.panel8.Controls.Add(this.label41);
            this.panel8.Controls.Add(this.label42);
            this.panel8.Controls.Add(this.label43);
            this.panel8.Controls.Add(this.pb_o15);
            this.panel8.Controls.Add(this.pb_o14);
            this.panel8.Controls.Add(this.pb_o13);
            this.panel8.Controls.Add(this.label44);
            this.panel8.Controls.Add(this.label45);
            this.panel8.Controls.Add(this.label46);
            this.panel8.Controls.Add(this.pb_o12);
            this.panel8.Controls.Add(this.pb_o11);
            this.panel8.Controls.Add(this.pb_o10);
            this.panel8.Controls.Add(this.label31);
            this.panel8.Controls.Add(this.pb_o9);
            this.panel8.Controls.Add(this.label32);
            this.panel8.Controls.Add(this.label33);
            this.panel8.Controls.Add(this.pb_o8);
            this.panel8.Controls.Add(this.pb_o7);
            this.panel8.Controls.Add(this.label34);
            this.panel8.Controls.Add(this.label35);
            this.panel8.Controls.Add(this.label36);
            this.panel8.Controls.Add(this.pb_o6);
            this.panel8.Controls.Add(this.pb_o5);
            this.panel8.Controls.Add(this.pb_o4);
            this.panel8.Controls.Add(this.label37);
            this.panel8.Controls.Add(this.label38);
            this.panel8.Controls.Add(this.label39);
            this.panel8.Controls.Add(this.pb_o3);
            this.panel8.Controls.Add(this.pb_o2);
            this.panel8.Controls.Add(this.pb_o1);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel8.Location = new System.Drawing.Point(389, 0);
            this.panel8.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(388, 110);
            this.panel8.TabIndex = 1;
            // 
            // textBox3
            // 
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.Location = new System.Drawing.Point(352, 39);
            this.textBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(25, 59);
            this.textBox3.TabIndex = 89;
            this.textBox3.Text = "P\r\nU\r\nT";
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox4
            // 
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox4.Location = new System.Drawing.Point(330, 39);
            this.textBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(25, 59);
            this.textBox4.TabIndex = 88;
            this.textBox4.Text = "O\r\nU\r\nT";
            this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(317, 12);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(17, 12);
            this.label40.TabIndex = 87;
            this.label40.Text = "16";
            // 
            // pb_o16
            // 
            this.pb_o16.Image = global::AMC_Test.Properties.Resources.circle_grey;
            this.pb_o16.Location = new System.Drawing.Point(342, 2);
            this.pb_o16.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pb_o16.Name = "pb_o16";
            this.pb_o16.Size = new System.Drawing.Size(35, 32);
            this.pb_o16.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_o16.TabIndex = 86;
            this.pb_o16.TabStop = false;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(251, 86);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(17, 12);
            this.label41.TabIndex = 85;
            this.label41.Text = "15";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(251, 49);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(17, 12);
            this.label42.TabIndex = 84;
            this.label42.Text = "14";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(251, 12);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(17, 12);
            this.label43.TabIndex = 83;
            this.label43.Text = "13";
            // 
            // pb_o15
            // 
            this.pb_o15.Image = global::AMC_Test.Properties.Resources.circle_grey;
            this.pb_o15.Location = new System.Drawing.Point(276, 76);
            this.pb_o15.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pb_o15.Name = "pb_o15";
            this.pb_o15.Size = new System.Drawing.Size(35, 32);
            this.pb_o15.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_o15.TabIndex = 82;
            this.pb_o15.TabStop = false;
            // 
            // pb_o14
            // 
            this.pb_o14.Image = global::AMC_Test.Properties.Resources.circle_grey;
            this.pb_o14.Location = new System.Drawing.Point(276, 39);
            this.pb_o14.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pb_o14.Name = "pb_o14";
            this.pb_o14.Size = new System.Drawing.Size(35, 32);
            this.pb_o14.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_o14.TabIndex = 81;
            this.pb_o14.TabStop = false;
            // 
            // pb_o13
            // 
            this.pb_o13.Image = global::AMC_Test.Properties.Resources.circle_grey;
            this.pb_o13.Location = new System.Drawing.Point(276, 2);
            this.pb_o13.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pb_o13.Name = "pb_o13";
            this.pb_o13.Size = new System.Drawing.Size(35, 32);
            this.pb_o13.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_o13.TabIndex = 80;
            this.pb_o13.TabStop = false;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(186, 86);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(17, 12);
            this.label44.TabIndex = 79;
            this.label44.Text = "12";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(186, 49);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(17, 12);
            this.label45.TabIndex = 78;
            this.label45.Text = "11";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(186, 12);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(17, 12);
            this.label46.TabIndex = 77;
            this.label46.Text = "10";
            // 
            // pb_o12
            // 
            this.pb_o12.Image = global::AMC_Test.Properties.Resources.circle_grey;
            this.pb_o12.Location = new System.Drawing.Point(211, 75);
            this.pb_o12.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pb_o12.Name = "pb_o12";
            this.pb_o12.Size = new System.Drawing.Size(35, 32);
            this.pb_o12.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_o12.TabIndex = 76;
            this.pb_o12.TabStop = false;
            // 
            // pb_o11
            // 
            this.pb_o11.Image = global::AMC_Test.Properties.Resources.circle_grey;
            this.pb_o11.Location = new System.Drawing.Point(211, 38);
            this.pb_o11.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pb_o11.Name = "pb_o11";
            this.pb_o11.Size = new System.Drawing.Size(35, 32);
            this.pb_o11.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_o11.TabIndex = 75;
            this.pb_o11.TabStop = false;
            // 
            // pb_o10
            // 
            this.pb_o10.Image = global::AMC_Test.Properties.Resources.circle_grey;
            this.pb_o10.Location = new System.Drawing.Point(211, 2);
            this.pb_o10.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pb_o10.Name = "pb_o10";
            this.pb_o10.Size = new System.Drawing.Size(35, 32);
            this.pb_o10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_o10.TabIndex = 74;
            this.pb_o10.TabStop = false;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(127, 86);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(11, 12);
            this.label31.TabIndex = 59;
            this.label31.Text = "9";
            // 
            // pb_o9
            // 
            this.pb_o9.Image = global::AMC_Test.Properties.Resources.circle_grey;
            this.pb_o9.Location = new System.Drawing.Point(145, 76);
            this.pb_o9.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pb_o9.Name = "pb_o9";
            this.pb_o9.Size = new System.Drawing.Size(35, 32);
            this.pb_o9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_o9.TabIndex = 58;
            this.pb_o9.TabStop = false;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(127, 49);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(11, 12);
            this.label32.TabIndex = 57;
            this.label32.Text = "8";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(127, 12);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(11, 12);
            this.label33.TabIndex = 56;
            this.label33.Text = "7";
            // 
            // pb_o8
            // 
            this.pb_o8.Image = global::AMC_Test.Properties.Resources.circle_grey;
            this.pb_o8.Location = new System.Drawing.Point(145, 39);
            this.pb_o8.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pb_o8.Name = "pb_o8";
            this.pb_o8.Size = new System.Drawing.Size(35, 32);
            this.pb_o8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_o8.TabIndex = 55;
            this.pb_o8.TabStop = false;
            // 
            // pb_o7
            // 
            this.pb_o7.Image = global::AMC_Test.Properties.Resources.circle_grey;
            this.pb_o7.Location = new System.Drawing.Point(145, 2);
            this.pb_o7.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pb_o7.Name = "pb_o7";
            this.pb_o7.Size = new System.Drawing.Size(35, 32);
            this.pb_o7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_o7.TabIndex = 54;
            this.pb_o7.TabStop = false;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(68, 86);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(11, 12);
            this.label34.TabIndex = 53;
            this.label34.Text = "6";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(68, 49);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(11, 12);
            this.label35.TabIndex = 52;
            this.label35.Text = "5";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(68, 12);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(11, 12);
            this.label36.TabIndex = 51;
            this.label36.Text = "4";
            // 
            // pb_o6
            // 
            this.pb_o6.Image = global::AMC_Test.Properties.Resources.circle_grey;
            this.pb_o6.Location = new System.Drawing.Point(87, 76);
            this.pb_o6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pb_o6.Name = "pb_o6";
            this.pb_o6.Size = new System.Drawing.Size(35, 32);
            this.pb_o6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_o6.TabIndex = 50;
            this.pb_o6.TabStop = false;
            // 
            // pb_o5
            // 
            this.pb_o5.Image = global::AMC_Test.Properties.Resources.circle_grey;
            this.pb_o5.Location = new System.Drawing.Point(87, 39);
            this.pb_o5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pb_o5.Name = "pb_o5";
            this.pb_o5.Size = new System.Drawing.Size(35, 32);
            this.pb_o5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_o5.TabIndex = 49;
            this.pb_o5.TabStop = false;
            // 
            // pb_o4
            // 
            this.pb_o4.Image = global::AMC_Test.Properties.Resources.circle_grey;
            this.pb_o4.Location = new System.Drawing.Point(87, 2);
            this.pb_o4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pb_o4.Name = "pb_o4";
            this.pb_o4.Size = new System.Drawing.Size(35, 32);
            this.pb_o4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_o4.TabIndex = 48;
            this.pb_o4.TabStop = false;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(10, 86);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(11, 12);
            this.label37.TabIndex = 47;
            this.label37.Text = "3";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(10, 49);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(11, 12);
            this.label38.TabIndex = 46;
            this.label38.Text = "2";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(10, 12);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(11, 12);
            this.label39.TabIndex = 45;
            this.label39.Text = "1";
            // 
            // pb_o3
            // 
            this.pb_o3.Image = global::AMC_Test.Properties.Resources.circle_grey;
            this.pb_o3.Location = new System.Drawing.Point(28, 76);
            this.pb_o3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pb_o3.Name = "pb_o3";
            this.pb_o3.Size = new System.Drawing.Size(35, 32);
            this.pb_o3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_o3.TabIndex = 44;
            this.pb_o3.TabStop = false;
            // 
            // pb_o2
            // 
            this.pb_o2.Image = global::AMC_Test.Properties.Resources.circle_grey;
            this.pb_o2.Location = new System.Drawing.Point(28, 39);
            this.pb_o2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pb_o2.Name = "pb_o2";
            this.pb_o2.Size = new System.Drawing.Size(35, 32);
            this.pb_o2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_o2.TabIndex = 43;
            this.pb_o2.TabStop = false;
            this.pb_o2.Tag = "2";
            // 
            // pb_o1
            // 
            this.pb_o1.Image = global::AMC_Test.Properties.Resources.circle_grey;
            this.pb_o1.Location = new System.Drawing.Point(28, 2);
            this.pb_o1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pb_o1.Name = "pb_o1";
            this.pb_o1.Size = new System.Drawing.Size(35, 32);
            this.pb_o1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_o1.TabIndex = 42;
            this.pb_o1.TabStop = false;
            this.pb_o1.Tag = "1";
            // 
            // l_status
            // 
            this.l_status.AutoSize = true;
            this.l_status.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l_status.Location = new System.Drawing.Point(10, 17);
            this.l_status.Name = "l_status";
            this.l_status.Size = new System.Drawing.Size(26, 37);
            this.l_status.TabIndex = 0;
            this.l_status.Text = " ";
            // 
            // bg_LDS
            // 
            this.bg_LDS.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bg_LDS_DoWork);
            // 
            // bg_deltaHeading
            // 
            this.bg_deltaHeading.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bg_deltaHeading_DoWork);
            // 
            // HMI_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 442);
            this.ControlBox = false;
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximumSize = new System.Drawing.Size(1682, 872);
            this.Name = "HMI_Main";
            this.Text = "LD_Monitor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HMI_Main_FormClosing);
            this.Load += new System.EventHandler(this.HMI_Main_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_i16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_i15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_i14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_i13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_i12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_i11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_i10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_i9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_i8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_i7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_i6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_i5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_i4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_i3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_i2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_i1)).EndInit();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_o16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_o15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_o14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_o13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_o12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_o11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_o10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_o9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_o8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_o7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_o6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_o5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_o4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_o3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_o2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_o1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.IO.Ports.SerialPort Zigbee;
        private System.Windows.Forms.Button btn_open;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Button btn_connect;
        private System.Windows.Forms.Timer Serial_Timer;
        private System.ComponentModel.BackgroundWorker Main_Task;
        private System.Windows.Forms.Button btn_setting;
        private System.Windows.Forms.Button button1;
        private System.ComponentModel.BackgroundWorker bg_reseve;
        private System.Windows.Forms.ListBox lb_log;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private AdvancedHMIControls.DateTimeDisplay dateTimeDisplay1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tb_volt;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tb_lo_a;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tb_lo_y;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tb_lo_x;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tb_odo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tb_wifi;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tb_temp;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label l_charge;
        private System.Windows.Forms.Panel panel6;
        public System.Windows.Forms.Timer t_saver;
        private System.Windows.Forms.Label l_status;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.PictureBox pb_i3;
        private System.Windows.Forms.PictureBox pb_i2;
        private System.Windows.Forms.PictureBox pb_i1;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.PictureBox pb_i6;
        private System.Windows.Forms.PictureBox pb_i5;
        private System.Windows.Forms.PictureBox pb_i4;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.PictureBox pb_i8;
        private System.Windows.Forms.PictureBox pb_i7;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.PictureBox pb_i9;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.PictureBox pb_i12;
        private System.Windows.Forms.PictureBox pb_i11;
        private System.Windows.Forms.PictureBox pb_i10;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.PictureBox pb_i15;
        private System.Windows.Forms.PictureBox pb_i14;
        private System.Windows.Forms.PictureBox pb_i13;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.PictureBox pb_i16;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label39;
        public System.Windows.Forms.PictureBox pb_o3;
        public System.Windows.Forms.PictureBox pb_o2;
        public System.Windows.Forms.PictureBox pb_o1;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
        public System.Windows.Forms.PictureBox pb_o6;
        public System.Windows.Forms.PictureBox pb_o5;
        public System.Windows.Forms.PictureBox pb_o4;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        public System.Windows.Forms.PictureBox pb_o8;
        public System.Windows.Forms.PictureBox pb_o7;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.PictureBox pb_o16;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.PictureBox pb_o15;
        private System.Windows.Forms.PictureBox pb_o14;
        private System.Windows.Forms.PictureBox pb_o13;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.PictureBox pb_o12;
        private System.Windows.Forms.PictureBox pb_o11;
        private System.Windows.Forms.PictureBox pb_o10;
        private System.Windows.Forms.Label label31;
        public System.Windows.Forms.PictureBox pb_o9;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.ComponentModel.BackgroundWorker bg_LDS;
        private System.Windows.Forms.TextBox textBox5;
        private System.ComponentModel.BackgroundWorker bg_deltaHeading;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}