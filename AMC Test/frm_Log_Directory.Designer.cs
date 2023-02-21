namespace AMC_Test
{
    partial class frm_Log_Directory
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
            this.tb_directory = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_directory_search = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_NAME = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_CANCEL = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.dg_logdirectory = new System.Windows.Forms.DataGridView();
            this.NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DIR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_del = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dg_logdirectory)).BeginInit();
            this.SuspendLayout();
            // 
            // tb_directory
            // 
            this.tb_directory.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tb_directory.Location = new System.Drawing.Point(129, 12);
            this.tb_directory.Name = "tb_directory";
            this.tb_directory.ReadOnly = true;
            this.tb_directory.Size = new System.Drawing.Size(367, 32);
            this.tb_directory.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(13, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Directory :";
            // 
            // btn_directory_search
            // 
            this.btn_directory_search.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_directory_search.Location = new System.Drawing.Point(502, 12);
            this.btn_directory_search.Name = "btn_directory_search";
            this.btn_directory_search.Size = new System.Drawing.Size(71, 32);
            this.btn_directory_search.TabIndex = 2;
            this.btn_directory_search.Text = "...";
            this.btn_directory_search.UseVisualStyleBackColor = true;
            this.btn_directory_search.Click += new System.EventHandler(this.btn_directory_search_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(13, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "NAME :";
            // 
            // tb_NAME
            // 
            this.tb_NAME.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tb_NAME.Location = new System.Drawing.Point(129, 53);
            this.tb_NAME.Name = "tb_NAME";
            this.tb_NAME.Size = new System.Drawing.Size(367, 32);
            this.tb_NAME.TabIndex = 4;
            this.tb_NAME.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btn_OK
            // 
            this.btn_OK.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_OK.Location = new System.Drawing.Point(410, 297);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(46, 32);
            this.btn_OK.TabIndex = 5;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btn_CANCEL
            // 
            this.btn_CANCEL.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_CANCEL.Location = new System.Drawing.Point(462, 297);
            this.btn_CANCEL.Name = "btn_CANCEL";
            this.btn_CANCEL.Size = new System.Drawing.Size(111, 32);
            this.btn_CANCEL.TabIndex = 6;
            this.btn_CANCEL.Text = "CANCEL";
            this.btn_CANCEL.UseVisualStyleBackColor = true;
            this.btn_CANCEL.Click += new System.EventHandler(this.btn_CANCEL_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button1.Location = new System.Drawing.Point(502, 53);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 32);
            this.button1.TabIndex = 8;
            this.button1.Text = "ADD";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dg_logdirectory
            // 
            this.dg_logdirectory.AllowUserToAddRows = false;
            this.dg_logdirectory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_logdirectory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NAME,
            this.DIR});
            this.dg_logdirectory.Location = new System.Drawing.Point(17, 108);
            this.dg_logdirectory.Name = "dg_logdirectory";
            this.dg_logdirectory.RowHeadersWidth = 40;
            this.dg_logdirectory.RowTemplate.Height = 23;
            this.dg_logdirectory.Size = new System.Drawing.Size(556, 183);
            this.dg_logdirectory.TabIndex = 9;
            // 
            // NAME
            // 
            this.NAME.HeaderText = "NAME";
            this.NAME.Name = "NAME";
            this.NAME.ReadOnly = true;
            this.NAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // DIR
            // 
            this.DIR.HeaderText = "PATH";
            this.DIR.Name = "DIR";
            this.DIR.ReadOnly = true;
            this.DIR.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DIR.Width = 400;
            // 
            // btn_del
            // 
            this.btn_del.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_del.Location = new System.Drawing.Point(17, 300);
            this.btn_del.Name = "btn_del";
            this.btn_del.Size = new System.Drawing.Size(72, 32);
            this.btn_del.TabIndex = 10;
            this.btn_del.Text = "DEL";
            this.btn_del.UseVisualStyleBackColor = true;
            this.btn_del.Click += new System.EventHandler(this.btn_del_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(245, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "자기 자신 로그 폴더는 마지막에 설정 할것!!!";
            // 
            // frm_Log_Directory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 344);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_del);
            this.Controls.Add(this.dg_logdirectory);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_CANCEL);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.tb_NAME);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_directory_search);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_directory);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_Log_Directory";
            this.Text = "Log Directory Setting";
            this.Load += new System.EventHandler(this.frm_Log_Directory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dg_logdirectory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox tb_directory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_directory_search;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_NAME;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_CANCEL;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dg_logdirectory;
        private System.Windows.Forms.DataGridViewTextBoxColumn NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn DIR;
        private System.Windows.Forms.Button btn_del;
        private System.Windows.Forms.Label label3;
    }
}