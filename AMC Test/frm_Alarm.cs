using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;


namespace AMC_Test
{
    public partial class frm_Alarm : Form
    {

        private List<string> Goals = new List<string>();
        BackgroundWorker bg_local = new BackgroundWorker();
        bool b_terminator = true;

        public frm_Alarm()
        {
            InitializeComponent();
        }

        public frm_Alarm(List<string> ts)
        {
            InitializeComponent();

            bg_local.DoWork += Bg_local_DoWork;

            this.BringToFront();

            for (int i = 0; i < ts.Count; i++)
            {
                Goals.Add(ts[i]);
            }
        }


        private void Bg_local_DoWork(object sender, DoWorkEventArgs e)
        {
            while(b_terminator)
            {
                lb_Loc_Score.Text = Form1.LD[0].LD_ST.LD_Localizetion_Score;
                System.Threading.Thread.Sleep(1000);
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            b_terminator = false;
            Form1.Insert_System_Log("frm_Alarm OK 버튼 클릭");

            Form1.Err_clear();
            AMC_Test.Form1.DO_OFF();
            Form1.Send_LD_AMC_MSG("SEND", "NONE", "", "NONE", "BTN_RESET");
            Form1.Send_LD_String("stop");
            Form1.Conveyor_BW_stop();
            Form1.STOPPER.motot_init();
            Form1.Send_LD_String("enableMotors");
            Form1.Set_Skynet_Status(Form1.nSKYNET.SM_RUN, "READY");

            Close();
            //Dispose();
        }

        public void Start_local_score()
        {
            b_terminator = true;
            bg_local.RunWorkerAsync();            
        }


        public void Set_Code(string code)
        {
            lb_Code.Text = code;
        }

        public void Set_Name(string name)
        {
            lb_Name.Text = name;
        }

        public void Set_Solution(string sol)
        {
            tb_Solution.Text = sol;
        }

        private void label2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            frmLocalize frmLocalize = new frmLocalize(Goals);

            frmLocalize.ShowDialog();
        }

        public void Set_Localize(string Score)
        {
            lb_Loc_Score.Text = Score;
        }

        public void Set_Link(string Text)
        {
            ll_Manual.Text = Text;
        }

        private void ll_Manual_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Application.StartupPath + "\\Manual\\" + ll_Manual.Text);
        }

        private void label4_Click(object sender, EventArgs e)
        {
            frmLocalize frmLocalize = new frmLocalize(Goals);

            frmLocalize.ShowDialog();
        }
    }
}
