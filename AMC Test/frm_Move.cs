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
    public partial class frm_Move : Form
    {
        public frm_Move()
        {
            InitializeComponent();

            //dateTimePicker1.CustomFormat = "yyyy/MM/dd";
            //dateTimePicker1.Format = DateTimePickerFormat.Custom;
        }

        private void btn_cal_Click(object sender, EventArgs e)
        {
            DateTime dt = dateTimePicker1.Value;

            Remove_controls_bytag("AMB");
            Count_CMD(dt.ToString("yyyy/MM/dd"));
        }

        public void Remove_controls_bytag(string tag)
        {
            foreach(Control ctr in Controls)
            {
                if (ctr != null)
                {
                    if (ctr.Tag != null)
                    {
                        if (ctr.Tag.ToString() == tag)
                            Controls.Remove(ctr);
                    }
                }
            }
        }

        public void Count_CMD(string date)
        {
            DateTime target_Date = Convert.ToDateTime(date);
            string todate_str = target_Date.ToString("yyyy-MM-dd");

            target_Date = target_Date.AddDays(-1);
            string target_str = target_Date.ToString("yyyy-MM-dd");

            string[] Path = Properties.Settings.Default.LogSCRAP_DIR.Split(';');
            string[] AMB;
            string[,] vehicle = new string[Path.Length, 2];

            int totcnt = 0;

            Label[] lb = new Label[Path.Length];
            TextBox[] tb = new TextBox[Path.Length];

            for(int i = 0; i < Path.Length; i++)
            {
                if (Path[i] != "")
                {
                    AMB = Path[i].Split('=');

                    DirectoryInfo dir = new DirectoryInfo(AMB[1]);

                    if (dir.Exists == true)
                    {
                        vehicle[i, 0] = AMB[0];
                        int cnt = CMD_count(AMB[1], vehicle[i, 0], date);

                        if (cnt != -1)
                        {
                            vehicle[i, 1] += cnt;
                            totcnt += cnt;
                        }                            
                        else
                        {

                        }

                        Point p = new Point();
                        Point tb_p = new Point();

                        p.X = 12 + (114 * (i % 3));
                        p.Y = 187 + (72 * (i / 3));

                        tb_p.X = 16 + (114 * (i % 3));
                        tb_p.Y = 211 + (72 * (i / 3));

                        lb[i] = new Label();
                        lb[i].Font = new Font("Arial", 16, FontStyle.Bold, GraphicsUnit.Point);
                        lb[i].Location = p;
                        lb[i].Text = vehicle[i, 0];
                        lb[i].Size = new Size(64, 21);
                        lb[i].AutoSize = true;
                        lb[i].Tag = "AMB";
                        Controls.Add(lb[i]);

                        tb[i] = new TextBox();
                        tb[i].Font = new Font("Arial", 16, FontStyle.Bold, GraphicsUnit.Point);
                        tb[i].Location = tb_p;
                        tb[i].Text = vehicle[i, 1];
                        tb[i].Size = new Size(60, 32);
                        tb[i].Tag = "AMB";
                        Controls.Add(tb[i]);                        
                    }
                    tb_res.Text = totcnt.ToString();
                    this.Size = new Size(this.Size.Width, 295 + (70 * (i / 3)));
                }
                else
                {

                }
            }
        }


        public int CMD_count(string path, string name, string date)
        {
            int tot = 0;

            tot = Cnt_CMD(path, name, false, date);
            tot += Cnt_CMD(path, name, true, date);

            return tot;
        }

        public int Cnt_CMD(string path, string name, bool today, string date)
        {
            string log_dir = "";
            string CMD_data = "";
            string[] path_data;
            string[] str_data;
            string[] cmd_data0;
            string[] cmd_data1;
            string[] cmd_split;                      

            DateTime t_Date = Convert.ToDateTime(date);
            DateTime b_Date = t_Date.AddDays(-1);

            int CMD = 0;
            DateTime dd;
            DateTime aa;

            if (today == false)
            {
                dd = Convert.ToDateTime(string.Format("{0} 06:00:00", b_Date.ToString("yyyy/MM/dd")));
                //DirectoryInfo dir = new DirectoryInfo(path + "\\" + b_Date.ToString("yyyy-MM-dd") + "\\");
                if (Directory.Exists(path + "\\" + b_Date.ToString("yyyy-MM-dd")) == true)
                {
                    path_data = Directory.GetFiles(path + "\\" + b_Date.ToString("yyyy-MM-dd"), "*CMD_" + name + "*");

                    if (path_data.Length != 0)
                        CMD_data = System.IO.File.ReadAllText(path_data[0]);
                    else
                    {
                        lb_msg.Items.Add(string.Format("NAME:{0}, PATH:{1}, 파일 없음 ", name, path + "\\" + b_Date.ToString("yyyy-MM-dd")));
                        return -1;
                    }
                }
                else
                {
                    lb_msg.Items.Add(string.Format("NAME:{0}, PATH:{1}, 폴더 없음 ", name, path + "\\" + b_Date.ToString("yyyy-MM-dd")));
                    return -1;
                }                
            }
            else
            {
                dd = Convert.ToDateTime(string.Format("{0} 05:59:59", t_Date.ToString("yyyy/MM/dd")));
                //DirectoryInfo dir = new DirectoryInfo(path + "\\" + t_Date.ToString("yyyy-MM-dd") + "\\");

                if (Directory.Exists(path + "\\" + t_Date.ToString("yyyy-MM-dd")) == true)
                {
                    path_data = Directory.GetFiles(path + "\\" + t_Date.ToString("yyyy-MM-dd"), "*CMD_" + name + "*");

                    if (path_data.Length != 0)
                        CMD_data = System.IO.File.ReadAllText(path_data[0]);
                    else
                    {
                        lb_msg.Items.Add(string.Format("NAME:{0}, PATH:{1}, 파일 없음 ", name, path + "\\" + b_Date.ToString("yyyy-MM-dd")));
                        return -1;
                    }
                }
                else
                {
                    lb_msg.Items.Add(string.Format("NAME:{0}, PATH:{1}, 폴더 없음 ", name, path + "\\" + b_Date.ToString("yyyy-MM-dd")));
                    return -1;
                }
            }

            str_data = CMD_data.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);


            for (int i = 6; i < str_data.Length; i++)
            {
                if (today == false)
                {                   

                    cmd_data0 = str_data[i - 1].Split(' ');
                    cmd_data1 = str_data[i].Split(' ');

                    cmd_split = cmd_data0[1].Split(':');

                    if (cmd_split.Length > 3)
                        cmd_data0[1] = cmd_split[0] + ":" + cmd_split[1] + ":" + cmd_split[2];

                    log_dir = string.Format("{0} {1}", cmd_data0[0], cmd_data0[1]);
                    log_dir.Replace('-', '/');
                    aa = Convert.ToDateTime(log_dir);

                    if(aa > dd)
                        if (cmd_data0[3] != cmd_data1[3])
                            CMD++;
                }
                else
                {
                    cmd_data0 = str_data[i - 1].Split(' ');
                    cmd_data1 = str_data[i].Split(' ');


                    cmd_split = cmd_data0[1].Split(':');

                    if (cmd_split.Length > 3)
                        cmd_data0[1] = cmd_split[0] + ":" + cmd_split[1] + ":" + cmd_split[2];

                    log_dir = string.Format("{0} {1}", cmd_data0[0], cmd_data0[1]);
                    log_dir.Replace('-', '/');
                    aa = Convert.ToDateTime(log_dir);

                    if (aa < dd)
                        if (cmd_data0[3] != cmd_data1[3])
                            CMD++;
                }
            }

            return CMD;
        }
    }
}
