namespace AMC_Test
{
    partial class frm_Individual_return
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btn_select_file = new System.Windows.Forms.Button();
            this.tb_file_name = new System.Windows.Forms.TextBox();
            this.tb_move_Cnt = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btn_select_file
            // 
            this.btn_select_file.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_select_file.Location = new System.Drawing.Point(436, 14);
            this.btn_select_file.Name = "btn_select_file";
            this.btn_select_file.Size = new System.Drawing.Size(142, 39);
            this.btn_select_file.TabIndex = 0;
            this.btn_select_file.Text = "파일 선택";
            this.btn_select_file.UseVisualStyleBackColor = true;
            this.btn_select_file.Click += new System.EventHandler(this.btn_select_file_Click);
            // 
            // tb_file_name
            // 
            this.tb_file_name.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tb_file_name.Location = new System.Drawing.Point(12, 14);
            this.tb_file_name.Name = "tb_file_name";
            this.tb_file_name.ReadOnly = true;
            this.tb_file_name.Size = new System.Drawing.Size(418, 39);
            this.tb_file_name.TabIndex = 1;
            this.tb_file_name.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tb_move_Cnt
            // 
            this.tb_move_Cnt.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tb_move_Cnt.Location = new System.Drawing.Point(12, 59);
            this.tb_move_Cnt.Name = "tb_move_Cnt";
            this.tb_move_Cnt.ReadOnly = true;
            this.tb_move_Cnt.Size = new System.Drawing.Size(418, 39);
            this.tb_move_Cnt.TabIndex = 2;
            this.tb_move_Cnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button1.Location = new System.Drawing.Point(436, 59);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(142, 39);
            this.button1.TabIndex = 3;
            this.button1.Text = "계  산";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frm_Individual_return
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 161);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tb_move_Cnt);
            this.Controls.Add(this.tb_file_name);
            this.Controls.Add(this.btn_select_file);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_Individual_return";
            this.Text = "개별 반송 횟수";
            this.Load += new System.EventHandler(this.frm_Individual_return_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btn_select_file;
        private System.Windows.Forms.TextBox tb_file_name;
        private System.Windows.Forms.TextBox tb_move_Cnt;
        private System.Windows.Forms.Button button1;
    }
}