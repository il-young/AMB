using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMC_Test
{
    public partial class frm_Log_Directory : Form
    {
        public frm_Log_Directory()
        {
            InitializeComponent();
        }

        private void btn_directory_search_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = Properties.Settings.Default.CMD_LOG_DIRECTORY;
            string folder_loc = "";
            DialogResult res = folderBrowserDialog1.ShowDialog();

            if(res == DialogResult.OK)
            {
                Properties.Settings.Default.CMD_LOG_DIRECTORY = folderBrowserDialog1.SelectedPath;
                Properties.Settings.Default.Save();

                tb_directory.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void frm_Log_Directory_Load(object sender, EventArgs e)
        {
            string[] dir = Properties.Settings.Default.LogSCRAP_DIR.Split(';');
            string[] AMB;

            for(int i = 0;  i < dir.Length; i++)
            {
                AMB = dir[i].Split('=');
                
                dg_logdirectory.Rows.Add(AMB);
            }

        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            int ccnt = dg_logdirectory.RowCount -1;
            string str = "";


            for (int i = 0; i < ccnt; i++)
            {
                if(dg_logdirectory.Rows[i].Cells[0].Value.ToString() != null &&  dg_logdirectory.Rows[i].Cells[1].Value.ToString() != null)
                    str += dg_logdirectory.Rows[i].Cells[0].Value.ToString() + "=" + dg_logdirectory.Rows[i].Cells[1].Value.ToString() + ";";
            }

            

            Properties.Settings.Default.LogSCRAP_DIR = str;
            Properties.Settings.Default.Save();

            Close();
        }

        private void btn_CANCEL_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] amb = new string[] {tb_NAME.Text, tb_directory.Text };

            DialogResult res = MessageBox.Show("Log 분석 폴더를 추가 하시겠습니까?","ADD", MessageBoxButtons.YesNo);

            if(res == DialogResult.Yes)
            {
                dg_logdirectory.Rows.Add(amb);
            }
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Log 분석 폴더를 삭제 하시겠습니까?", "Delete", MessageBoxButtons.YesNo);

            if(res == DialogResult.Yes)
            {
                DataGridViewSelectedRowCollection selected_row = dg_logdirectory.SelectedRows;

                dg_logdirectory.Rows.RemoveAt(selected_row[0].Index);
            }
        }
    }
}
