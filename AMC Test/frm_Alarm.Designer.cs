namespace AMC_Test
{
    partial class frm_Alarm
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
            this.label1 = new System.Windows.Forms.Label();
            this.lb_Code = new System.Windows.Forms.Label();
            this.lb_Name = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_Solution = new System.Windows.Forms.TextBox();
            this.btn_OK = new System.Windows.Forms.Button();
            this.ll_Manual = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.lb_Loc_Score = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "CODE :";
            // 
            // lb_Code
            // 
            this.lb_Code.Font = new System.Drawing.Font("굴림", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lb_Code.Location = new System.Drawing.Point(152, 18);
            this.lb_Code.Name = "lb_Code";
            this.lb_Code.Size = new System.Drawing.Size(208, 32);
            this.lb_Code.TabIndex = 1;
            this.lb_Code.Text = "CODE_NUM";
            this.lb_Code.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb_Name
            // 
            this.lb_Name.Font = new System.Drawing.Font("굴림", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lb_Name.Location = new System.Drawing.Point(551, 18);
            this.lb_Name.Name = "lb_Name";
            this.lb_Name.Size = new System.Drawing.Size(237, 32);
            this.lb_Name.TabIndex = 3;
            this.lb_Name.Text = "CODE_NAME";
            this.lb_Name.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("굴림", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(413, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 32);
            this.label3.TabIndex = 2;
            this.label3.Text = "NAME :";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("굴림", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(12, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(213, 51);
            this.label2.TabIndex = 4;
            this.label2.Text = "SOLUTION :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.label2_MouseDoubleClick);
            // 
            // tb_Solution
            // 
            this.tb_Solution.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.tb_Solution.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.tb_Solution.Font = new System.Drawing.Font("굴림", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tb_Solution.Location = new System.Drawing.Point(12, 118);
            this.tb_Solution.Multiline = true;
            this.tb_Solution.Name = "tb_Solution";
            this.tb_Solution.ReadOnly = true;
            this.tb_Solution.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_Solution.Size = new System.Drawing.Size(776, 261);
            this.tb_Solution.TabIndex = 5;
            this.tb_Solution.Text = "Solution Text";
            // 
            // btn_OK
            // 
            this.btn_OK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_OK.Font = new System.Drawing.Font("굴림", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_OK.Location = new System.Drawing.Point(674, 385);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(114, 53);
            this.btn_OK.TabIndex = 6;
            this.btn_OK.Text = "O K";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // ll_Manual
            // 
            this.ll_Manual.AutoEllipsis = true;
            this.ll_Manual.AutoSize = true;
            this.ll_Manual.Font = new System.Drawing.Font("굴림", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ll_Manual.Location = new System.Drawing.Point(16, 385);
            this.ll_Manual.Name = "ll_Manual";
            this.ll_Manual.Size = new System.Drawing.Size(187, 35);
            this.ll_Manual.TabIndex = 7;
            this.ll_Manual.TabStop = true;
            this.ll_Manual.Text = "linkLabel1";
            this.ll_Manual.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ll_Manual_LinkClicked);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("굴림", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.Location = new System.Drawing.Point(407, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(271, 51);
            this.label4.TabIndex = 8;
            this.label4.Text = "Localize Score :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // lb_Loc_Score
            // 
            this.lb_Loc_Score.Font = new System.Drawing.Font("굴림", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lb_Loc_Score.Location = new System.Drawing.Point(684, 64);
            this.lb_Loc_Score.Name = "lb_Loc_Score";
            this.lb_Loc_Score.Size = new System.Drawing.Size(104, 51);
            this.lb_Loc_Score.TabIndex = 9;
            this.lb_Loc_Score.Text = "100%";
            this.lb_Loc_Score.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frm_Alarm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_OK;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ControlBox = false;
            this.Controls.Add(this.lb_Loc_Score);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ll_Manual);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.tb_Solution);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lb_Name);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lb_Code);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_Alarm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Alarm";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lb_Code;
        private System.Windows.Forms.Label lb_Name;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_Solution;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.LinkLabel ll_Manual;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lb_Loc_Score;
    }
}