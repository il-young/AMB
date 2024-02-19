using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Media;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Data.SqlClient;

namespace AMC_Test
{
    public partial class AMC_Monitor : Form
    {
        public delegate void FormClose();
        public event FormClose FormCloseEvent;

        public struct sql_cmd
        {
            public string Query;
            public int retry_cnt;

            public void init()
            {
                Query = "";
                retry_cnt = 0;
            }

            public void retry()
            {
                retry_cnt++;
            }
        }

        Board brd = new Board();

        private bool bDisplay_T = false;

        private bool[] INPUT_DATA = new bool[16];
        private bool[] OUTPUT_DATA = new bool[16];
        private bool[] DIGITAL_DATA = new bool[32];

        private const int OUT_NUM_CONVEYOR_RUN = 0;
        private const int OUT_NUM_CONVEYOR_DIRECTION = 1;
        private const int OUT_NUM_GRIPER_RUN = 2;
        private const int OUT_NUM_GRIPER_DIRECTION = 3;

        private const int IN_NUM_TRAY_SENSOR1 = 6;
        private const int IN_NUM_TRAY_SENSOR1_1 = 5;
        private const int IN_NUM_TRAY_SENSOR2 = 4;
        private const int IN_NUM_TRAY_SENSOR2_1 = 3;
        private const int IN_NUM_TRAY_SENSOR3 = 2;
        private const int IN_NUM_TRAY_SENSOR3_1 = 1;
        private const int IN_NUM_TRAY_SENSOR4 = 0;
        private const int IN_NUM_TRAY_SENSOR4_1 = 0;

        //private const int IN_NUM_GRIPER_R = 8;
        //private const int IN_NUM_GRIPER_L = 9;
        private const int IN_NUM_GRIPER_ON_LIMIT = 8;
        private const int IN_NUM_GRIPER_OFF_LIMIT = 9;
        private const int IN_NUM_LIGHT_CURTAIN = 10;
        private const int IN_NUM_MOUTH_SENSOR = 12;
        private const int IN_NUM_MAN_OUT_SW = 14;
        private const int IN_NUM_TRANSFER_SENSOR = 11;
        private const int IN_NUM_MAN_STOP_SENSOR1 = 13;
        private const int IN_NUM_MAN_STOP_SENSOR2 = 13;

        public delegate void add_alarm(int alarm);
        public event add_alarm add_alarm_event;
        private bool barea_checker_T = false;

        private bool[] LD_INPUT = new bool[16];
        private bool[] LD_OUTPUT = new bool[16];

        private Color[] colors = new Color[8];

        static public List<string> Goals = new List<string>();

        private System.Threading.Thread DBThread;
        private static Queue<sql_cmd> SQL_Q = new Queue<sql_cmd>();

        public string Linecode = "";
        public string EquipmentID = "";

        bool bis_Alarm = false;
        bool is_Alarm = false;
        bool blink_t = false;
        string blink_btn_str = "";


        struct stBoat_param
        {
            string SIZE;
            string color;
        }

        private List<stBoat_param> boat = new List<stBoat_param>();


        SoundPlayer simpleSound = new SoundPlayer(System.Environment.CurrentDirectory + "\\Setting\\el.wav");

        public AMC_Monitor()
        {
            InitializeComponent();
        }

        public void SetSkynetData(string L, string E)
        {
            Linecode = L;
            EquipmentID = E;
        }

        public void Playing_Sound()
        {
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(System.Environment.CurrentDirectory + "\\Setting\\");
            List<string> file_name = new List<string>();            

            foreach (System.IO.FileInfo File in di.GetFiles())
            {
                if (File.Extension.ToLower().CompareTo(".wav") == 0)
                {
                    if (File.FullName.ToLower().Contains("low_batt") == false)
                        file_name.Add(File.FullName);
                }
            }

            simpleSound.SoundLocation = file_name[DateTime.Now.Second % file_name.Count];
            simpleSound.PlayLooping();
        }

        public void Play_sound()
        {
            simpleSound.Play();
        }

        public void Stop_Sound()
        {
            simpleSound.Stop();
            Where2go = "";
        }

        public void set_vehicle_location(Point loc)
        {
            vehicle_location = loc;
        }

        private void cameraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1.Insert_System_Log("CAMERA 메뉴 클릭");
            Form1.Show_Camera();
        }

        private void rFIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1.Insert_System_Log("RFID 메뉴 클릭");
            Form1.Show_RFID();
        }

        private void lDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1.Insert_System_Log("LD 메뉴 클릭");
            Form1.Show_LD();
        }

        public void Set_IN_DATA(bool[,] val)
        {
            for (int i = 0; i < 16; i++)
            {
                INPUT_DATA[i] = val[0, i];
            }
        }

        public void Set_OUT_DATA(bool[,] val)
        {
            for (int i = 0; i < 16; i++)
            {
                OUTPUT_DATA[i] = val[0, i];
            }

        }

        public void Set_DIGITAL_DATA(bool[] val)
        {
            for (int i = 0; i < val.Length; i++)
            {
                DIGITAL_DATA[i] = val[i];
            }
        }

        private void AMC_Monitor_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormCloseEvent();
            brd.Close();
            if (e.CloseReason != CloseReason.WindowsShutDown)
            {
                this.Hide();
                e.Cancel = true;
            }
        }

        private void DBThread_Start()
        {
            int res = 0;

            while (true)
            {
                if (SQL_Q.Count > 0)
                {
                    if (SQL_Q.Peek().Query.Contains("TB_OPERATION_HISTORY") == true)
                    {
                        //MessageBox.Show(SQL_Q.Peek().Query);
                        res = WriteDB(SQL_Q.Peek().Query);
                        ////ReturnLogSave(string.Format("DBThread_Start Queue Count : {0}, Queue[0] : {1}, Return : {2}", SQL_Q.Count, SQL_Q.Peek(), res));

                        if (res != 0)
                            SQL_Q.Dequeue();
                        else
                        {
                            sql_cmd sql_temp = SQL_Q.Dequeue();

                            if (sql_temp.retry_cnt < 5)
                            {
                                sql_temp.retry();
                                SQL_Q.Enqueue(sql_temp);
                                ////ReturnLogSave(string.Format("DBThread_Start Retry:{0} Query:{1}", sql_temp.retry_cnt, sql_temp.Query));
                            }
                            else
                            {
                                ////ReturnLogSave(string.Format("DBThread_Start Retry Fail Query : {0}", sql_temp.Query));
                            }
                        }
                    }
                    else if(SQL_Q.Peek().Query.Contains("TBL_AGV_STATUS_LIST") == true)
                    {

                    }
                }
                System.Threading.Thread.Sleep(1000);
            }
        }


        private void bw_Display_DoWork(object sender, DoWorkEventArgs e)
        {
            int res = 0;

            while (bDisplay_T)
            {
                //btn_sensor1.BackColor = INPUT_DATA[IN_NUM_TRAY_SENSOR1] == true ? Color.OrangeRed : Color.Gray;
                //btn_sensor1_1.BackColor = INPUT_DATA[IN_NUM_TRAY_SENSOR1_1] == true ? Color.OrangeRed : Color.Gray;
                //btn_sensor2.BackColor = INPUT_DATA[IN_NUM_TRAY_SENSOR2] == true ? Color.OrangeRed : Color.Gray;
                //btn_sensor2_1.BackColor = INPUT_DATA[IN_NUM_TRAY_SENSOR2_1] == true ? Color.OrangeRed : Color.Gray;
                //btn_sensor3.BackColor = INPUT_DATA[IN_NUM_TRAY_SENSOR3] == true ? Color.OrangeRed : Color.Gray;
                //btn_sensor3_1.BackColor = INPUT_DATA[IN_NUM_TRAY_SENSOR3_1] == true ? Color.OrangeRed : Color.Gray;
                //btn_sensor4.BackColor = INPUT_DATA[IN_NUM_TRAY_SENSOR4] == true ? Color.OrangeRed : Color.Gray;
                //btn_transfer1.BackColor = INPUT_DATA[IN_NUM_TRANSFER_SENSOR] == true ? Color.OrangeRed : Color.Gray;
                //btn_transfer2.BackColor = INPUT_DATA[IN_NUM_TRANSFER_SENSOR] == true ? Color.OrangeRed : Color.Gray;
                //btn_mouth.BackColor = INPUT_DATA[IN_NUM_MOUTH_SENSOR] == true ? Color.OrangeRed : Color.Gray;

                //pb_griper_ccw.Visible = (OUTPUT_DATA[OUT_NUM_GRIPER_RUN] == true && OUTPUT_DATA[OUT_NUM_GRIPER_DIRECTION] == false) ? true : false;
                //pb_griper_cw.Visible = (OUTPUT_DATA[OUT_NUM_GRIPER_RUN] == true && OUTPUT_DATA[OUT_NUM_GRIPER_DIRECTION] == true) ? true : false;

                //btn_light_cutain1.Visible = INPUT_DATA[IN_NUM_LIGHT_CURTAIN] == true ? true : false;
                //btn_light_cutain2.Visible = INPUT_DATA[IN_NUM_LIGHT_CURTAIN] == true ? true : false;

                //Tray1.Visible = (INPUT_DATA[IN_NUM_TRAY_SENSOR1] == true) || (INPUT_DATA[IN_NUM_TRAY_SENSOR1_1] == true) ? true : false;
                //Tray2.Visible = (INPUT_DATA[IN_NUM_TRAY_SENSOR2] == true) || (INPUT_DATA[IN_NUM_TRAY_SENSOR2_1] == true) ? true : false;
                //Tray3.Visible = (INPUT_DATA[IN_NUM_TRAY_SENSOR3] == true) || (INPUT_DATA[IN_NUM_TRAY_SENSOR3_1] == true) ? true : false;
                //Tray4.Visible = (INPUT_DATA[IN_NUM_TRAY_SENSOR4] == true) ? true : false;
                //Tray5.Visible =  (INPUT_DATA[IN_NUM_TRAY_SENSOR4_1] == true) ? true : false;


                
                pb_i1.Image = LD_INPUT[0] == true ? Properties.Resources.circle_green.Clone() as Image : Properties.Resources.circle_grey.Clone() as Image;
                pb_i2.Image = LD_INPUT[1] == true ? Properties.Resources.circle_green.Clone() as Image : Properties.Resources.circle_grey.Clone() as Image;
                pb_i3.Image = LD_INPUT[2] == true ? Properties.Resources.circle_green.Clone() as Image : Properties.Resources.circle_grey.Clone() as Image;
                pb_i4.Image = LD_INPUT[3] == true ? Properties.Resources.circle_green.Clone() as Image : Properties.Resources.circle_grey.Clone() as Image;
                pb_i5.Image = LD_INPUT[4] == true ? Properties.Resources.circle_green.Clone() as Image : Properties.Resources.circle_grey.Clone() as Image;
                pb_i6.Image = LD_INPUT[5] == true ? Properties.Resources.circle_green.Clone() as Image : Properties.Resources.circle_grey.Clone() as Image;
                pb_i7.Image = LD_INPUT[6] == true ? Properties.Resources.circle_green.Clone() as Image : Properties.Resources.circle_grey.Clone() as Image;
                pb_i8.Image = LD_INPUT[7] == true ? Properties.Resources.circle_green.Clone() as Image : Properties.Resources.circle_grey.Clone() as Image;
                pb_i9.Image = LD_INPUT[8] == true ? Properties.Resources.circle_green.Clone() as Image : Properties.Resources.circle_grey.Clone() as Image;
                pb_i10.Image = LD_INPUT[9] == true ? Properties.Resources.circle_green.Clone() as Image : Properties.Resources.circle_grey.Clone() as Image;
                pb_i11.Image = LD_INPUT[10] == true ? Properties.Resources.circle_green.Clone() as Image : Properties.Resources.circle_grey.Clone() as Image;
                pb_i12.Image = LD_INPUT[11] == true ? Properties.Resources.circle_green.Clone() as Image : Properties.Resources.circle_grey.Clone() as Image;
                pb_i13.Image = LD_INPUT[12] == true ? Properties.Resources.circle_green.Clone() as Image : Properties.Resources.circle_grey.Clone() as Image;
                pb_i14.Image = LD_INPUT[13] == true ? Properties.Resources.circle_green.Clone() as Image : Properties.Resources.circle_grey.Clone() as Image;
                pb_i15.Image = LD_INPUT[14] == true ? Properties.Resources.circle_green.Clone() as Image : Properties.Resources.circle_grey.Clone() as Image;
                pb_i16.Image = LD_INPUT[15] == true ? Properties.Resources.circle_green.Clone() as Image : Properties.Resources.circle_grey.Clone() as Image;


                if (Properties.Settings.Default.ModeCall == true)
                {

                    if (Form1.Call == true)
                    {
                        pb_o1_ON.BringToFront();
                        pb_o1_OFF.SendToBack();
                    }
                    else
                    {
                        pb_o1_ON.SendToBack();
                        pb_o1_OFF.BringToFront();
                    }
                    

                    

                    if (Form1.MGZ == true)
                    {
                        pb_o2_ON.BringToFront();
                        pb_o2_OFF.SendToBack();
                    }
                    else
                    {
                        pb_o2_ON.SendToBack();
                        pb_o2_OFF.BringToFront();
                    }
                }
                else
                {
                    
                    
                }

                

                pb_o3.Image = LD_OUTPUT[2] == true ? Properties.Resources.circle_green.Clone() as Image : Properties.Resources.circle_grey.Clone() as Image;
                pb_o4.Image = LD_OUTPUT[3] == true ? Properties.Resources.circle_green.Clone() as Image : Properties.Resources.circle_grey.Clone() as Image;
                pb_o5.Image = LD_OUTPUT[4] == true ? Properties.Resources.circle_green.Clone() as Image : Properties.Resources.circle_grey.Clone() as Image;
                pb_o6.Image = LD_OUTPUT[5] == true ? Properties.Resources.circle_green.Clone() as Image : Properties.Resources.circle_grey.Clone() as Image;
                pb_o7.Image = LD_OUTPUT[6] == true ? Properties.Resources.circle_green.Clone() as Image : Properties.Resources.circle_grey.Clone() as Image;
                pb_o8.Image = LD_OUTPUT[7] == true ? Properties.Resources.circle_green.Clone() as Image : Properties.Resources.circle_grey.Clone() as Image;
                pb_o9.Image = LD_OUTPUT[8] == true ? Properties.Resources.circle_green.Clone() as Image : Properties.Resources.circle_grey.Clone() as Image;
                pb_o10.Image = LD_OUTPUT[9] == true ? Properties.Resources.circle_green.Clone() as Image : Properties.Resources.circle_grey.Clone() as Image;
                pb_o11.Image = LD_OUTPUT[10] == true ? Properties.Resources.circle_green.Clone() as Image : Properties.Resources.circle_grey.Clone() as Image;
                pb_o12.Image = LD_OUTPUT[11] == true ? Properties.Resources.circle_green.Clone() as Image : Properties.Resources.circle_grey.Clone() as Image;
                pb_o13.Image = LD_OUTPUT[12] == true ? Properties.Resources.circle_green.Clone() as Image : Properties.Resources.circle_grey.Clone() as Image;
                pb_o14.Image = LD_OUTPUT[13] == true ? Properties.Resources.circle_green.Clone() as Image : Properties.Resources.circle_grey.Clone() as Image;
                pb_o15.Image = LD_OUTPUT[14] == true ? Properties.Resources.circle_green.Clone() as Image : Properties.Resources.circle_grey.Clone() as Image;
                pb_o16.Image = LD_OUTPUT[15] == true ? Properties.Resources.circle_green.Clone() as Image : Properties.Resources.circle_grey.Clone() as Image;


                


                System.Threading.Thread.Sleep(1000);
            }
        }

        private void AMC_Monitor_Load(object sender, EventArgs e)
        {
            colors[0] = Color.Blue;
            colors[1] = Color.Red;
            colors[2] = Color.Yellow;
            colors[3] = Color.SkyBlue;
            colors[4] = Color.Green;
            colors[5] = Color.Orange;
            colors[6] = Color.LightGoldenrodYellow;
            colors[7] = Color.DarkSeaGreen;

            CheckForIllegalCrossThreadCalls = false;

            if(Properties.Settings.Default.ModeCall == true)
            {
                label8.Text = "MGZ";
                label9.Text = "CALL";
            }

            if (bgw_alarm.IsBusy == false)
                bgw_alarm.RunWorkerAsync();
        
            Load_boat_setting();
            bg_local.DoWork += Bg_local_DoWork;

            bDisplay_T = true;
            bw_Display.RunWorkerAsync();
            Read_BUTTON();
            DBThread = new System.Threading.Thread(DBThread_Start);

            if (DBThread.IsAlive == false)
                DBThread.Start();

            //tabControl1.Visible = false;
        }


        private void Load_boat_setting()
        {
            string boat_size = AMC_Test.Properties.Settings.Default.BOAT_SIZE;
            string boat_color = AMC_Test.Properties.Settings.Default.BOAT_COLOR;

            cb_SIZE.Items.Clear();

            for (int i = 0; i < (boat_size.Split(',').Length > boat_color.Split(',').Length ? boat_color.Split(',').Length : boat_size.Split(',').Length); i++)
            {
                cb_SIZE.Items.Add(boat_size.Split(',')[i] + "," + boat_color.Split(',')[i]);
            }

            cb_SIZE.SelectedItem = 1;
        }

        public void Goal_ADD(List<string> str)
        {
            for (int i = 0; i < str.Count; i++)
                Goals.Add(str[i]);
        }

        private void btn_move_Click(object sender, EventArgs e)
        {
            //Form1.Send_LD_AMC_MSG("SEND", "AMC1", "OUT_JOB_END", cb_goal.Text, "MTASK_END");
        }

        private void btn_light_cutain2_Click(object sender, EventArgs e)
        {

        }

        private void btn_MAN_Click(object sender, EventArgs e)
        {

        }

        private void btn_AUTO_Click(object sender, EventArgs e)
        {
            if (btn_AUTO.Text == "AUTO")
            {
                if(MessageBox.Show("Manual Mode로 전환 하시겠습니까?", "메뉴얼 모드 전환", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    Form1.LD[0].AUTO = false;
                    Form1.Send_LD_AMC_MSG("SEND", "NONE", "CMD", "NONE", "MANUAL");
                    btn_AUTO.Text = "MANUAL";
                    Form1.Insert_System_Log("Manual mode 전환");
                    btn_AUTO.BackColor = Color.Red; 
                }
                
            }
            else
            {
                if (MessageBox.Show("Auto Mode로 전환 하시겠습니까?", "오토 모드 전환", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    Form1.LD[0].AUTO = true;
                    Form1.Send_LD_AMC_MSG("SEND", "NONE", "CMD", "NONE", "AUTO");
                    btn_AUTO.Text = "AUTO";
                    Form1.Insert_System_Log("Auto mode 전환");
                    btn_AUTO.BackColor = Color.Lime;
                }
            }

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1.Insert_System_Log("RESET 버튼 클릭");

            Form1.Err_clear();
            AMC_Test.Form1.DO_OFF();
            Form1.Send_LD_AMC_MSG("SEND", "NONE", "", "NONE", "BTN_RESET");
            Form1.Send_LD_String("stop");            
            Form1.Conveyor_BW_stop();
            Form1.STOPPER.motot_init();
            Form1.Send_LD_String("enableMotors");
            Where2go = "";
            Stop_Sound();
            init_area();
        }

        public void Set_MSG(string msg)
        {
            tb_msg.Text = msg;
        }

        public void Set_Skynet_ST(int a )
        {
            if (a != 0)
                pb_skynet_on.BringToFront();
            else
                pb_skynet_off.BringToFront();
        }

        public string GetArea()
        {
            return tb_AREA.Text;
        }

        string AREA_TEMP = "";
        public void Set_AREA(string area)
        {
            AREA_TEMP = tb_AREA.Text;
            
            if (area == "OFF")
            {
                tb_AREA.Text = "OFFICE";
            }
            else if (area == "PAC")
            {
                tb_AREA.Text = "PACKING ROOM";
            }
            else if (area == "FVI")
            {
                tb_AREA.Text = "FVI";
            }
            else if (area == "PNP")
            {
                tb_AREA.Text = "PNP";
            }
            else if (area == "MAT")
            {
                tb_AREA.Text = "MATERIAL ROOM";
            }
            else if(area == "DOCK")
            {
                if(tb_AREA.Text != "DOCK")
                {
                    Form1.Set_Skynet_Status(Form1.nSKYNET.SM_RUN, "RECHARGE");
                    Stop_Sound();
                }
                tb_AREA.Text = "DOCK";
            }
            else if(area == "STANDBY")
            {
                if (tb_AREA.Text != "STANDBY")
                {
                    Form1.Set_Skynet_Status(Form1.nSKYNET.SM_RUN, "Standby");
                    Stop_Sound();
                }
                tb_AREA.Text = "STANDBY";
            }
            else
            {
                tb_AREA.Text = area;
            }

            if((AREA_TEMP != tb_AREA.Text) && (tb_AREA.Text != "") )
            {
                Form1.Set_Skynet_Status(Form1.nSKYNET.SM_RUN, "MOVE", tb_AREA.Text);
            }
        }

        public void Set_BATT(string batt)
        {
            tb_batt.Text = batt + "%";
        }

        public void Set_Local(string Score)
        {
            tb_Localize.Text = Score + "%";
        }

        string Where2go = "";
        private void Btn_goFVI_Click(object sender, EventArgs e)
        {
            //Form1.Insert_System_Log("GO FVI 버튼 클릭");

            //Form1.DO_OFF();

            //if (tb_AREA.Text != "FVI")
            //{
            //    Form1.Conveyor_BW_stop();
            //    Playing_Sound();
            //    Form1.Send_LD_String("goto FVI_STB");
            //    Where2go = "FVI";
            //    Form1.Insert_System_Log("FVI로 이동 합니다.");
            //    Run_area_checker();

            //    string capcode = Form1.Find_Capcode_By_Goalname(Get_where2go());

            //    if (capcode != "")
            //    {
            //        //Form1.Send_AMC_MSG("SEND", "NONE", "PAGER", capcode, " [AMC] " + Get_where2go() + "로 출발 합니다.");
            //    }
            //}

            if (tb_AREA.Text != btn_goFVI.Text.Split(' ')[1])
            {
                Form1.Insert_System_Log(btn_goFVI.Text + " 버튼 클릭");

                Form1.Conveyor_BW_stop();
                Playing_Sound();
                Form1.Send_LD_String("goto " + btn_goFVI.Text.Split(' ')[1] + "_STB");
                Where2go = btn_goFVI.Text.Split(' ')[1];
                Form1.Insert_System_Log(btn_goFVI.Text.Split(' ')[1] + "로 이동 합니다.");
                Run_area_checker();
            }

        }

        private void Btn_STOP_Click(object sender, EventArgs e)
        {
            try
            {            
                Form1.DO_OFF();

                Form1.Insert_System_Log("STOP 버튼 클릭");
            
                Form1.Send_LD_AMC_MSG("SEND", "NONE", "CMD", "NONE", "MANUAL");
                //Form1.LD[0].AUTO = false;
                Form1.Send_LD_String("stop");
                Stop_Sound();
                Where2go = "";
                init_area();
            }
            catch (Exception ex)
            {
                Form1.Insert_ERR_Log("]Btn_STOP_Click]" + ex.Message);
                throw;
            }
        }

        private void Btn_go_PNP_Click(object sender, EventArgs e)
        {
            Form1.Insert_System_Log(btn_go_PNP.Text +"버튼 클릭");

            if (tb_AREA.Text != btn_go_PNP.Text.Split(' ')[1])
            {
                Form1.Conveyor_BW_stop();

                Form1.Send_LD_String("goto " + btn_go_PNP.Text.Split(' ')[1] + "_STB");
                Form1.Insert_System_Log(btn_go_PNP.Text.Split(' ')[1] + "로 이동 합니다.");
                Playing_Sound();
                Where2go = btn_go_PNP.Text.Split(' ')[1];
                Run_area_checker();

                //string capcode = Form1.Find_Capcode_By_Goalname(Get_where2go());

                //if (capcode != "")
                //{
                    //Form1.Send_AMC_MSG("SEND", "NONE", "PAGER", capcode, " [AMC] " + Get_where2go() + "로 출발 합니다.");
                //}
            }

        }

        private void Btn_dock_Click(object sender, EventArgs e)
        {
            Form1.DO_OFF();

            Form1.Conveyor_BW_stop();

            Form1.Send_LD_String("DOCK AMC_DOCK");
            Form1.Insert_System_Log("충전 버튼 클릭");
            Playing_Sound();
            Where2go = "DOCK";
            Run_area_checker();
        }

        bool amc_monitor_close = false;
        private void EXITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1.Insert_System_Log("EXIT 메뉴 클릭");
            amc_monitor_close = true;
            this.Close();
        }

        public bool Get_AMC_close()
        {
            return amc_monitor_close;
        }

        private void ContextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void Btn_stop_music_Click(object sender, EventArgs e)
        {
            Form1.Insert_System_Log("음악 정지 버튼 클릭");
            Form1.Stop_batt_sound();
            Stop_Sound();

        }

        public string Get_where2go()
        {
            return Where2go;
        }

        public void Set_Where2go(string Area)
        {
            Where2go = Area;
        }

        private void ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            
        }

        public void Read_BUTTON()
        {
            string Setting_data = System.IO.File.ReadAllText(System.Environment.CurrentDirectory + "\\Setting\\BUTTON.txt", UTF8Encoding.Default);
            string[] str_data = Setting_data.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            Size form_size = panel3.Size;

            if (str_data.Length >= 7)
            {
                btn_Latch.Visible = false;
                btn_Unlatch.Visible = false;
                btn_Fold.Visible = false;
                btn_Unfold.Visible = false;
            }

            int h = (form_size.Height - 160 - (form_size.Height - 611)) / 4;
            int w = form_size.Width;// (str_data.Length > 6 ? (((int)str_data.Length -2) /4)+1 : 1);

            Button[] btn_CMD = new Button[str_data.Length];

            for (int i = 0; i < str_data.Length ; i++)
            {
                string[] str_temp = str_data[i].Split('\t');

                if (i == 0)
                {
                    btn_CMD[i] = new Button();
                    btn_CMD[i].Click += new EventHandler(btn_Click_Event);
                    btn_CMD[i].Text = str_temp[0];
                    btn_CMD[i].Tag = str_temp[1] + "," + str_temp[2] + "," + str_temp[3];
                    btn_CMD[i].Font = new Font("Arial", 24, FontStyle.Bold);
                    btn_CMD[i].Height = 158;
                    btn_CMD[i].Width = 238;
                    btn_CMD[i].BackColor = Color.Lime;
                    btn_CMD[i].Location = new System.Drawing.Point(0, 0);
                    panel3.Controls.Add(btn_CMD[i]);
                }
                else if(i == str_data.Length -1)
                {
                    btn_CMD[i] = new Button();
                    btn_CMD[i].Click += new EventHandler(btn_Click_Event);
                    btn_CMD[i].Text = str_temp[0];
                    btn_CMD[i].Tag = str_temp[1] + "," + str_temp[2] + "," + str_temp[3];
                    btn_CMD[i].Font = new Font("Arial", 24, FontStyle.Bold);
                    btn_CMD[i].Height = 153;
                    btn_CMD[i].Width = 235;
                    btn_CMD[i].BackColor = Color.Gold;
                    btn_CMD[i].Location = new System.Drawing.Point(4, 611);
                    panel3.Controls.Add(btn_CMD[i]);
                }
                else
                {
                    if (str_data[i] != "EMPTY")
                    {
                        if (i < 5)
                        {
                            btn_CMD[i] = new Button();
                            btn_CMD[i].Click += new EventHandler(btn_Click_Event);
                            btn_CMD[i].Text = str_temp[0];
                            btn_CMD[i].Tag = str_temp[1] + "," + str_temp[2] + "," + str_temp[3];
                            btn_CMD[i].Font = new Font("Arial", 24, FontStyle.Bold);
                            btn_CMD[i].Height = h;
                            btn_CMD[i].Width = w;
                            btn_CMD[i].BackColor = colors[i % colors.Length];
                            btn_CMD[i].Location = new System.Drawing.Point(((i - 1) / 4) * w, ((i - 1) % 4) * h + 160);
                            panel3.Controls.Add(btn_CMD[i]);
                        }
                        else
                        {
                            btn_CMD[i] = new Button();
                            btn_CMD[i].Click += new EventHandler(btn_Click_Event);
                            btn_CMD[i].Text = str_temp[0];
                            btn_CMD[i].Tag = str_temp[1] + "," + str_temp[2] + "," + str_temp[3];
                            btn_CMD[i].Font = new Font("Arial", 24, FontStyle.Bold);
                            btn_CMD[i].Height = h;
                            btn_CMD[i].Width = w;
                            btn_CMD[i].BackColor = colors[i % colors.Length];
                            btn_CMD[i].Location = new System.Drawing.Point(((i - 5) / 4) * w, ((i - 5) % 4) * h + 0);

                            if (panel6.Visible == false)
                            {
                                panel5.Controls.Add(btn_CMD[i]);
                            }
                            else
                            {
                                panel7.Controls.Add(btn_CMD[i]);
                            }
                        }
                    }
                }
            }
        }

        string start_Area = "";
        public Button clicked_btn = new Button();

        private void btn_Click_Event(object sender, EventArgs e)
        {   
            clicked_btn = (Button)sender;

            Form1.LD[0].LD_ST.LD_STANDBY = false;
            if (tabControl1.Visible == true)
                btn_OK_Click_1(sender, e);

            move_vehicle(clicked_btn);
        }

        public void MoveContinues()
        {
            move_vehicle(clicked_btn);
        }


        private void move_vehicle(Button  btn)
        {
            Insert_CMD_Log(btn.Text + " " + btn.Tag.ToString() + " 버튼 클릭");

            sql_cmd cmd = new sql_cmd();
            cmd.Query = string.Format("insert into TB_OPERATION_HISTORY ([DATETIME], [LINE_CODE], [EQUIP_ID], [TYPE], [DEPARTURE], [ARRIVAL]) values('{0}','{1}','{2}','{3}','{4}','{5}')", 
                DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff"),
                Linecode, EquipmentID, btn.Text + " Click", tb_AREA.Text, clicked_btn.Text);
            cmd.retry_cnt = 0;

            SQL_Q.Enqueue(cmd);

            end_blink();

            //if (s.Contains(tb_AREA.Text) == false || tb_AREA.Text == "")
            {
                Form1.Conveyor_BW_stop();

                

                if (btn.Tag.ToString().Split(',')[0].ToUpper().Contains("A2A") == true)
                {
                    Form1.Send_LD_String("executeMacro MOVE_" + tb_AREA.Text + "2" + btn.Text.Split(' ')[btn.Text.Split(' ').Length - 1].ToUpper());
                    Form1.Set_Skynet_Status((int)Form1.nSKYNET.SM_RUN, "Move", tb_AREA.Text, btn.Text.Split(' ')[btn.Text.Split(' ').Length - 1].ToUpper());
                }
                else
                {
                    Form1.Send_LD_String(btn.Tag.ToString().Split(',')[0]);
                    Form1.Set_Skynet_Status((int)Form1.nSKYNET.SM_RUN, "Move", tb_AREA.Text, btn.Text.Split(' ')[btn.Text.Split(' ').Length - 1].ToUpper());
                }

                //Form1.Insert_System_Log(s.Split(' ')[1] + "로 이동 합니다.");
                blink_btn_str = btn.Text;
                start_blink();

                Where2go = btn.Tag.ToString().Split(' ').Length >= 2 ? btn.Tag.ToString().Split(' ')[1].Split(',')[0] : btn.Tag.ToString().Split(',')[0];

                if (btn.Tag.ToString().Split(',')[1] == "Y")
                {
                    Playing_Sound();
                }
                else
                {
                    Stop_Sound();
                }

                if (btn.Tag.ToString().Split(',')[2] == "Y")
                {
                    
                    

                    if (tb_AREA.Text != "")
                        start_Area = tb_AREA.Text;


                    brd.Visible = false;
                    brd.Set_TEXT(start_Area + " => " + btn.Text.Split(' ')[btn.Text.Split(' ').Length - 1]);
                    brd.set_start(start_Area, btn.Text.Split(' ')[btn.Text.Split(' ').Length - 1]);
                    brd.ShowDialog();
                }

                Form1.Send_LD_String("sayInstant \"출발 합니다.\"");

                
                //Run_area_checker();
                
            }
        }


        public void AGVHide()
        {
            panel6.Hide();
        }
        public void BoardHide()
        {
            brd.Hide();
        }

        private void start_blink()
        {
            blink_t = true;
            is_1st = true;
            timer1.Enabled = true;
        }

        public void end_blink()
        {
            blink_t = false;
            timer1.Enabled = false;

            foreach (Button btn in panel3.Controls)
            {
                if (btn.Text == blink_btn_str)
                {                    
                    btn.BackColor = cl;
                    

                    return;                    
                }
            }
        }

        public void reSet_Start_AREA()
        {
            start_Area = "";
        }

        public Size Get_Panel3_size()
        {
            return panel3.Size;
        }

        

        private void AlingerONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if(btn_Aligner.Text.Contains("ON") == true)
            //{
            //    Properties.Settings.Default.GRIPPER_USE = false;
            //    btn_Aligner.Text = "Aligner : OFF";
            //    Properties.Settings.Default.Save();
            //}
            //else if(btn_Aligner.Text.Contains("OFF") == true)
            //{
            //    Properties.Settings.Default.GRIPPER_USE = true;
            //    btn_Aligner.Text = "Aligner : ON";
            //    Properties.Settings.Default.Save();
            //}
        }

        private void Btn_STOPPER_Click(object sender, EventArgs e)
        {
            //if (btn_STOPPER.Text.Contains("ON") == true)
            //{
            //    Properties.Settings.Default.STOPPER_USE = false;
            //    btn_Aligner.Text = "Stopper : OFF";
            //    Properties.Settings.Default.Save();
            //}
            //else if (btn_STOPPER.Text.Contains("OFF") == true)
            //{
            //    Properties.Settings.Default.STOPPER_USE = true;
            //    btn_Aligner.Text = "Stopper : ON";
            //    Properties.Settings.Default.Save();
            //}
        }

        private void Btn_CONVEYOR_Click(object sender, EventArgs e)
        {
            //if (btn_CONVEYOR.Text.Contains("ON") == true)
            //{
            //    Properties.Settings.Default.CONVEYOR_USE = false;
            //    btn_Aligner.Text = "Conveyor : OFF";
            //    Properties.Settings.Default.Save();
            //}
            //else if (btn_CONVEYOR.Text.Contains("OFF") == true)
            //{
            //    Properties.Settings.Default.CONVEYOR_USE = true;
            //    btn_Aligner.Text = "Conveyor : ON";
            //    Properties.Settings.Default.Save();
            //}
        }

        private void Tb_AREA_DoubleClick(object sender, EventArgs e)
        {
        }


        private void Run_area_checker()
        {
            barea_checker_T = true;
            if(bw_area_checker.IsBusy == false)
                bw_area_checker.RunWorkerAsync();
        }
                
        string saarea = "";
        Point vehicle_location = new Point();
        Point vehicle_blocation = new Point();
        int tot_retry_cnt;

        private void init_area()
        {            
            tot_retry_cnt = 0;
            barea_checker_T = false;

            if (bw_area_checker.IsBusy == true)
                bw_area_checker.CancelAsync();
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int area_checker_cnt = 0;

            while(barea_checker_T)
            {
                try
                {
                    saarea = tb_AREA.Text;

                    if(Get_where2go() == saarea)
                    {
                        barea_checker_T = false;
                        tot_retry_cnt = 0;
                    }
                    else
                    {
                        if(vehicle_blocation == vehicle_location)
                        {
                            if (area_checker_cnt >= 180)
                            {
                                if (tot_retry_cnt >= 5)
                                {
                                    add_alarm_event(5201);
                                }
                                else
                                {
                                    move_vehicle(clicked_btn);
                                    area_checker_cnt = 0;
                                    tot_retry_cnt++;
                                }
                            }
                            else
                            {
                                area_checker_cnt++;
                            }
                        }
                        else
                        {
                            area_checker_cnt = 0;
                        }
                    }

                    vehicle_blocation = vehicle_location;
                    
                    System.Threading.Thread.Sleep(1000);
                }
                catch (Exception ex)
                {
                    Form1.Insert_System_Log(ex.Message);
                }
            }
        }

        private void Btn_ungrip1_Click(object sender, EventArgs e)
        {
            Form1.Ungrip();
        }


        public void Set_LD_IO(bool[] INPUT,bool[] OUTPUT )
        {
            for(int i = 0; i < 16; i++)
            {
                LD_INPUT[i] = INPUT[i];
                LD_OUTPUT[i] = OUTPUT[i];
            }
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        public bool Get_Alarm_Panel_Visible()
        {
            return tabControl1.Visible;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AMC_Test.Form1.LATCH();


        }

        private void btn_Unlatch_Click(object sender, EventArgs e)
        {
            //AMC_Test.Form1.UNLATCH();
            reSet_Alarm();
        }

        private void btn_Unfold_Click(object sender, EventArgs e)
        {  
            AMC_Test.Form1.SIDE_LASER_UNFOLD();
        }

        private void btn_Fold_Click(object sender, EventArgs e)
        {
            AMC_Test.Form1.SIDE_LASER_FOLD();
        }

        private void btn_dock_Click_1(object sender, EventArgs e)
        {
            Btn_go_PNP_Click(sender, e);
        }


        private void btn_add_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Boat SIZE를 추가 합니까?", "Boat SIZE Insert",  MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string input_str = "";
                string size = AMC_Test.Properties.Settings.Default.BOAT_SIZE;
                string color = AMC_Test.Properties.Settings.Default.BOAT_COLOR;

                Form1.InputCOMBO("Boat SIZE add", "Boat 크기와 Color을 입력해 주세요(,로 구분)", ref input_str);

                if(input_str != "")
                {
                    size += "," + input_str.Split(',')[0];
                    color += ',' + input_str.Split(',')[1];

                    AMC_Test.Properties.Settings.Default.BOAT_SIZE = size;
                    AMC_Test.Properties.Settings.Default.BOAT_COLOR = color;
                    AMC_Test.Properties.Settings.Default.Save();

                    Load_boat_setting();
                }
            }
            
        }


        private void cb_SIZE_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = cb_SIZE.Items[cb_SIZE.SelectedIndex].ToString();

            pn_boat_size.BackColor = Color.FromName(str.Split(',')[1]);
            lb_size.Text = str.Split(',')[0];
        }

        private void btn_del_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("현제 항목을 삭제 하시겠습니까?", "Boat SIZE Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string[] size = AMC_Test.Properties.Settings.Default.BOAT_SIZE.Split(',');
                string[] color = AMC_Test.Properties.Settings.Default.BOAT_COLOR.Split(',');
                string size_temp = "";
                string color_temp = "";

                int cb_remove = cb_SIZE.SelectedIndex;

                for (int i = 0; i < size.Length; i++)
                {
                    if (i != cb_remove && size[i] != "")
                    {
                        size_temp += size[i];
                        color_temp += color[i];

                        if (i != size.Length && i < size.Length)
                        {
                            size_temp += ",";
                            color_temp += ",";
                        }
                    }
                }


                if (size_temp.Substring(size_temp.Length - 1, 1) == ",")
                    size_temp = size_temp.Remove(size_temp.Length - 1, 1);

                if (color_temp.Substring(color_temp.Length - 1, 1) == ",")
                    color_temp = color_temp.Remove(color_temp.Length - 1, 1);


                AMC_Test.Properties.Settings.Default.BOAT_SIZE = size_temp;
                AMC_Test.Properties.Settings.Default.BOAT_COLOR = color_temp;
                AMC_Test.Properties.Settings.Default.Save();

                Load_boat_setting();
            }
        }

        private void pn_boat_size_Click(object sender, EventArgs e)
        {
            cb_SIZE.SelectedIndex = cb_SIZE.SelectedIndex +1 == cb_SIZE.Items.Count ? 0 : cb_SIZE.SelectedIndex + 1;
        }

        private void lb_size_Click(object sender, EventArgs e)
        {
            cb_SIZE.SelectedIndex = cb_SIZE.SelectedIndex + 1 == cb_SIZE.Items.Count ? 0 : cb_SIZE.SelectedIndex + 1;
        }

        private void btn_cmd_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string s = btn.Tag.ToString();

            Form1.Insert_System_Log(s + "버튼 클릭");

            if (s.Contains(tb_AREA.Text) == false || tb_AREA.Text == "")
            {
                Form1.Conveyor_BW_stop();

                Form1.Send_LD_String(btn.Tag.ToString().Split(',')[0]);
                //Form1.Insert_System_Log(s.Split(' ')[1] + "로 이동 합니다.");

                if (btn.Tag.ToString().Split(',')[1] == "Y")
                {
                    Playing_Sound();
                }
                else
                {
                    Stop_Sound();
                }


                Where2go = s.Split(' ').Length >= 2 ? s.Split(' ')[1] : s;
                Run_area_checker();
            }
        }

        private void btn_stop_music_Click_1(object sender, EventArgs e)
        {
            Stop_Sound();
        }

        private void mangerModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string val = "";
            InputBox("Manager Mode", "Input Password", ref val);

            if(val == "mngr")
            {
                btn_Latch.Enabled = true;
                btn_Unlatch.Enabled = true;
                btn_Fold.Enabled = true;
                btn_Unfold.Enabled = true;
            }
        }

        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }

        private void tb_AREA_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            frmLocalize frmLocalize = new frmLocalize(Goals);

            frmLocalize.ShowDialog();
        }

        private void textBox3_DoubleClick(object sender, EventArgs e)
        {
            string val = "";
            InputBox("Turn On Setup Mode ", "Input Password", ref val);

            if(val == "mngr")
            {
                Form1.Set_Skynet_Setup_Mode();
            }
        }

        BackgroundWorker bg_local = new BackgroundWorker();
        bool b_terminator = true;

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

            reSet_Alarm();
        }

        private void label36_DoubleClick(object sender, EventArgs e)
        {
            frmLocalize frmLocalize = new frmLocalize(Goals);

            frmLocalize.ShowDialog();
        }

        public void Start_local_score()
        {
            b_terminator = true;

            if(bg_local.IsBusy == false)
                bg_local.RunWorkerAsync();
        }

        public void Set_AL_Panel()
        {
            pn_alarm.BringToFront();

            if(pn_alarm.Visible == false)
                pn_alarm.Visible = true;
        }



        public void Set_Alarm()
        {
            
            tabControl1.Visible = true;
        }

        public void  reSet_Alarm()
        {
            tabControl1.Visible = false;
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

        private void Bg_local_DoWork(object sender, DoWorkEventArgs e)
        {
            while (b_terminator)
            {
                lb_Loc_Score.Text = Form1.LD[0].LD_ST.LD_Localizetion_Score;
                System.Threading.Thread.Sleep(1000);
            }
        }

        private void bgw_alarm_DoWork(object sender, DoWorkEventArgs e)
        {
            while(true)
            {
                try
                {
                    //if (bis_Alarm == false && is_Alarm == true)
                    //{
                    //    //pn_alarm.Visible = pn_alarm.Visible == true ? false : true;
                    //    //panel4.Visible = panel4.Visible == true ? false : true;
                    //    //pn_alarm.BringToFront();

                    //    tabControl1.Visible = tabControl1.Visible == true ? false : true;
                    //}
                                       

                    //bis_Alarm = is_Alarm;
                    //is_Alarm = false;

                    System.Threading.Thread.Sleep(1000);
                }
                catch (Exception ex)
                {

                }
                
            }                
        }

        private void btn_OK_Click_1(object sender, EventArgs e)
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


            tabControl1.Visible = false;
            //reSet_Alarm();
        }

        private void Check_Log_date(string log_dir, int keep_day)
        {
            try
            {
                DirectoryInfo logdir = new DirectoryInfo(log_dir);

                foreach (FileInfo file in logdir.GetFiles())
                {
                    if (file.CreationTime < DateTime.Now.AddDays(-keep_day))
                    {
                        file.Delete();
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }




        public void Insert_CMD_Log(string msg)
        {
            string date = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"); ;
            string str_temp = "";
            string log_dir = Properties.Settings.Default.CMD_LOG_DIRECTORY + "\\" + System.DateTime.Now.ToString("yyyy-MM-dd") + "\\" + System.DateTime.Now.ToString("yyyy-MM-dd") + " CMD_" + Properties.Settings.Default.NAME + "_Log.txt";
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(Properties.Settings.Default.CMD_LOG_DIRECTORY + "\\" + System.DateTime.Now.ToString("yyyy-MM-dd") + "\\");

            try
            {
                if (di.Exists == false)
                    di.Create();

                Insert_CMD_Log_BackUp(msg);


                if (System.IO.File.Exists(log_dir) == false)
                {
                    string temp;
                    di.Create();

                    temp = "========================================================" + Environment.NewLine;
                    temp += "=                                                                                                            =" + Environment.NewLine;
                    temp += "=                                      AMB CMD Log File " + System.DateTime.Now.ToString("yyyy/MM/dd") + "                                    =" + Environment.NewLine;
                    temp += "=                                                                                                            =" + Environment.NewLine;
                    temp += "========================================================" + Environment.NewLine;

                    System.IO.File.WriteAllText(log_dir, temp);
                    Check_Log_date(Properties.Settings.Default.CMD_LOG_DIRECTORY + "\\" + System.DateTime.Now.ToString("yyyy-MM-dd") + "\\" , 30);
                }

                //str_buf = System.IO.File.ReadAllText(log_dir);

                string[] arr_str = msg.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                using (System.IO.StreamWriter st = System.IO.File.AppendText(log_dir))
                {

                    for (int i = 0; i < arr_str.Length; i++)
                    {
                        if (arr_str[i].Trim('\0') != "")
                        {
                            str_temp = date + " " + arr_str[i];
                            st.WriteLine(str_temp);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Insert_ERR_Log(ex.Message);
                //Insert_Log(msg);
            }
        }

        private int WriteDB(string msg)
        {
            int res = 0;
            string ConnectionString = "server=10.135.200.35;uid=skynet;pwd=amm;database=amm@123";

            try
            {
                using (SqlConnection ssconn = new SqlConnection(ConnectionString))
                {
                    ssconn.Open();
                    SqlCommand scmd = new SqlCommand(msg, ssconn);
                    scmd.CommandType = System.Data.CommandType.Text;

                    res = scmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {

            }

            return res;
        }


        public void Insert_CMD_Log_BackUp(string msg)
        {
            string date = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:FFF");
            string str_temp = "";
            string log_dir = System.Environment.CurrentDirectory + "\\Log\\" + System.DateTime.Now.ToString("yyyy-MM-dd") + "_BACKUP\\" + System.DateTime.Now.ToString("yyyy-MM-dd") + " CMD_" + Properties.Settings.Default.NAME + "_Log.txt";
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(System.Environment.CurrentDirectory + "\\Log\\" + System.DateTime.Now.ToString("yyyy-MM-dd") + "\\");

            try
            {
                if (di.Exists == false)
                    di.Create();

                if (System.IO.File.Exists(log_dir) == false)
                {
                    di.Create();
                    string temp;

                    temp = "========================================================" + Environment.NewLine;
                    temp += "=                                                                                                            =" + Environment.NewLine;
                    temp += "=                                      AMB CMD Log File " + System.DateTime.Now.ToString("yyyy/MM/dd") + "                                    =" + Environment.NewLine;
                    temp += "=                                                                                                            =" + Environment.NewLine;
                    temp += "========================================================" + Environment.NewLine;

                    System.IO.File.WriteAllText(log_dir, temp);
                    Check_Log_date(System.Environment.CurrentDirectory + "\\Log\\", 30);
                }

                //str_buf = System.IO.File.ReadAllText(log_dir);

                string[] arr_str = msg.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                System.IO.StreamWriter st = System.IO.File.AppendText(log_dir);

                for (int i = 0; i < arr_str.Length; i++)
                {
                    if (arr_str[i].Trim('\0') != "")
                    {
                        str_temp = date + " " + arr_str[i];
                        st.WriteLine(str_temp);
                    }
                }

                st.Close();
                st.Dispose();


            }
            catch (Exception ex)
            {
                //Insert_ERR_Log(ex.Message);
                //Insert_Log(msg);
            }
        }

        private void 반송횟수구하기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_Move move = new frm_Move();

            move.ShowDialog();
        }

        private void logDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_Log_Directory log_form = new frm_Log_Directory();
            log_form.ShowDialog();
        }

        private void 개별반송횟수구하기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_Individual_return log_form = new frm_Individual_return();

            log_form.ShowDialog();
        }

        private void bgw_btn_blink_DoWork(object sender, DoWorkEventArgs e)
        {
            
            }

        bool is_1st = true;
        Color cl = new Color();

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            

            try
            {
                foreach (Button btn in panel3.Controls)
                {
                    if (btn.Text == blink_btn_str)
                    {
                        if (is_1st == true)
                        {
                            cl = btn.BackColor;
                            is_1st = false;
                        }

                        if (btn.BackColor != Color.White)
                            btn.BackColor = Color.White;
                        else
                            btn.BackColor = cl;
                    }
                }
                

                System.Threading.Thread.Sleep(700);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void SetAGVLocation(string node)
        {
            tb_AGVNode.Text = node;
        }

        public void SetAGVLocationRed()
        {
            tb_AGVNode.BackColor = Color.Red;
        }

        public void SetAGVLocationControl()
        {
            tb_AGVNode.BackColor = Color.LightSlateGray;
        }

        public void SetMs(String ms)
        {
            tb_ms.Text = ms;
        }

        private void mobilePlannerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int nFileNameStartP = Properties.Settings.Default.MobilePlanner_Path.LastIndexOf("MobilePlanner.exe");

            string temp = @Properties.Settings.Default.MobilePlanner_Path.Substring(0, nFileNameStartP);
            openFileDialog1.InitialDirectory = temp;
            openFileDialog1.FileName = Properties.Settings.Default.MobilePlanner_Path.Substring(nFileNameStartP, 
                Properties.Settings.Default.MobilePlanner_Path.Length - nFileNameStartP);

            if (DialogResult.OK == openFileDialog1.ShowDialog())
            {
                Properties.Settings.Default.MobilePlanner_Path = openFileDialog1.FileName;
                Properties.Settings.Default.Save();
            }
        }

        bool isExecuting = false;
        // FindWindow 사용을 위한 코드
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(string strClassName, string StrWindowName);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern void SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int SW_SHOWNORMAL = 1;
        private const int SW_SHOWMINIMIZED = 2;
        private const int SW_MAXIMIZE = 3;

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Process[] p = Process.GetProcessesByName("MobilePlanner");

            if(p.Length == 0)
            {
                if (System.IO.File.Exists(Properties.Settings.Default.MobilePlanner_Path) == true)
                    Process.Start(Properties.Settings.Default.MobilePlanner_Path);
                else
                {
                    if(DialogResult.Yes == MessageBox.Show("Mobile Planner 경로가 일치하지 않습니다.\nMobile Planner 설치 경로를 확인 해 주세요\n경로를 설정 하시겠습니까?", "Mobile Planner 경로 확인 필요", MessageBoxButtons.YesNo, MessageBoxIcon.Error))
                    {
                        mobilePlannerToolStripMenuItem_Click(sender, e);
                    }
                }
            }
            else
            {
                foreach (Process proc in p)
                {
                    if (proc.ProcessName.Equals("MobilePlanner"))
                    //  Pgm_FileName 프로그램의 실행 파일[.exe]를 제외한 파일명
                    {
                        isExecuting = true;
                        break;
                    }
                    else
                        isExecuting = false;
                }

                if (isExecuting)
                {
                    this.WindowState = FormWindowState.Minimized;
                    //윈도우 핸들러
                    IntPtr procHandler = FindWindow(null, "MobilePlanner");
                    ShowWindow(procHandler, SW_MAXIMIZE);
                    SetForegroundWindow(procHandler);
                }
                else
                {
                    Process.Start(Properties.Settings.Default.MobilePlanner_Path);
                }
            }
        }

        private void menuStrip1_DoubleClick(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
                WindowState = FormWindowState.Maximized;
        }

        private void menuStrip1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void tb_ms_MouseDown(object sender, MouseEventArgs e)
        {
            frm_Alarm al = new frm_Alarm();
            al.Show();
        }

        private void ll_Manual_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Application.StartupPath + "\\Manual\\" + ll_Manual.Text);
        }

        private void callModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show($"Call Mode를 {!Properties.Settings.Default.ModeCall}로 변경하시겠습니까?", "Mode Change", MessageBoxButtons.YesNo))
            {
                Properties.Settings.Default.ModeCall = Properties.Settings.Default.ModeCall == false ? true : false;
                Properties.Settings.Default.Save();
            }
        }

        private void pb_o1_OFF_Click(object sender, EventArgs e)
        {

        }
    }
}
