using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AMC_Test
{
    public partial class frm_Individual_return : Form
    {
        public frm_Individual_return()
        {
            InitializeComponent();
        }

        string file_name;

        private void frm_Individual_return_Load(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = System.Environment.CurrentDirectory + "\\Log\\";
            openFileDialog1.DefaultExt = "txt";
            file_name = Properties.Settings.Default.CMD_LOG_DIRECTORY + "\\" + System.DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd") + "\\" + System.DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd") + " CMD_" + Properties.Settings.Default.NAME + "_Log.txt";
            tb_file_name.Text = file_name;
        }

        private void btn_select_file_Click(object sender, EventArgs e)
        {
            DialogResult res = openFileDialog1.ShowDialog();

            if (res == DialogResult.OK)
            {   
                file_name = openFileDialog1.FileName;

                tb_file_name.Text = file_name;

                Properties.Settings.Default.INDIVIDUAL_FILE_NAME = file_name;
                Properties.Settings.Default.Save();
            }

        }

        public void Count_CMD()
        {
            

            string log_dir = file_name;
            string CMD_data = "";
            string[] str_data;
            string[] cmd_data0;
            string[] cmd_data1;
            int CMD = 0;

            
            CMD_data = System.IO.File.ReadAllText(log_dir);
            str_data = CMD_data.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            openFileDialog1.InitialDirectory = file_name;
            

            for (int i = 6; i < str_data.Length; i++)
            {
                cmd_data0 = str_data[i - 1].Split(' ');
                cmd_data1 = str_data[i].Split(' ');

                if (cmd_data0[2] != cmd_data1[3])
                    CMD++;
            }

            tb_move_Cnt.Text = CMD.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Count_CMD();
        }
    }
}
