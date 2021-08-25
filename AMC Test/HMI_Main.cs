using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;
using System.IO;


namespace AMC_Test
{
    public partial class HMI_Main : Form
    {
        public struct LD_Dev
        {
            public string LD_NAME;
            public IPEndPoint LD_IP;
            public Socket LD_Client;
            public List<string> LD_GOAL;
            public bool LD_Is_connection;
            public LD_Status LD_ST;
            public double LD_DISTANCE;
        };

        public struct Call_Dev
        {
            public string Location;
            public int X;
            public int Y;
            public int force_Relese_Time;
            public string type;
        };

        public struct LD_Status
        {
            public string LD_ST;
            public string LD_CHARGE;
            public LD_Location LD_LOC;
            public string LD_TEMP;
            public string LD_LDS1;
            public string LD_LDS2;
            public string LD_LDS1_DISTANCE;
            public string LD_LDS2_DISTANCE;
            public double LD_LDS1_Angle;
            public double LD_LDS2_Angle;
            public double LD_LDS_Angle;
            public double LD_LDS_Angle2;
            public double LD_LDS_DIS_DIFF;

            public double LD_LDS_CORREC_CNT;
            public double LD_LDS_CORREC_ANG;

            public bool Is_Deltaheading_Comp;

        };

        public struct LD_Location
        {
            public int LD_X;
            public int LD_Y;
            public int LD_A;
        };

        public struct Command
        {
            public string SCR;
            public string DEST;
            public string TIME;
            public string TYPE;
            public LD_Status ST;
        };

        public enum CMD_ST
        {
            CMD_Wait,
            CMD_LD_Move,
            CMD_Arrive,
            CMD_Deltha_Heading,
            CMD_Read_QR,
            CMD_Conveyor_Move,
            CMD_Conveyor_Run,
            CMD_Comp
        };

        public Queue<Command> cmd_Q;


        protected const int List_Box_Line_Conut = 120;
        static public LD_Dev[] LD = new LD_Dev[10];
        static public Call_Dev Now_Dev;
        bool b_bg_terminator = false;
        bool b_bg_task_terminator = false;
        bool b_bg_LDS_treminator = false;
        bool b_bg_deltaHeading = false;
        protected int ndeltaHeading_Task_cnt;
        int n_move_1000_comp = 0;
        int n_deltaHeading_step = 0;
        
        public Socket LDS_Client;

        private const int LDS_Min_Distance = 50;    //mm
        private const int LDS_Max_Distance = 450;   //mm
        private const int LDS_Min_Val = 400;    //1V
        private const int LDS_Max_Val = 4000;   //10V
        private const int LDS_2_LDS_Distance = 440; //mm
        private double LDS_1mm_Val;    //mm


        
        byte recive_cnt = 0;
        int Serial_Timer_Cnt = 0;
        int Serial_Retry_Cnt = 0;
        bool is_serial_send = false;

        private bool[] Gate1_st = new bool[2];
        private bool[] Gate2_st = new bool[2];
        private bool[] Gate3_st = new bool[2];
        private bool[] Gate4_st = new bool[2];

        private int Cmd_step;

        
        

        static public Queue<string> Err_Q1 { get; set; } = new Queue<string>();
        public Queue<string> State_Q2 { get; set; } = new Queue<string>();

        public Save_Screen_frm Saver = new Save_Screen_frm();



        public HMI_Main()
        {
            InitializeComponent();
        }

        static public string[] Get_COM()
        {
            string[] str = SerialPort.GetPortNames();

            return str;
        }

        private void btn_connect_Click(object sender, EventArgs e)
        {
            if(btn_connect.Text == "START")
            {
                if (Zigbee.IsOpen == false)
                {
                    string[] str_com = Get_COM();
                    bool com_is_ok = false;

                    for (int i = 0; i < str_com.Length; i++)
                    {
                        if (str_com[i] == AMC_Test.Properties.Settings.Default.COM)
                        {
                            com_is_ok = true;
                        }
                    }

                    if (com_is_ok == true)
                    {
                        Zigbee.PortName = AMC_Test.Properties.Settings.Default.COM;
                        Zigbee.Open();
                                                
                        if (Main_Task.IsBusy == false)
                        {
                            //Main_Task.RunWorkerAsync();
                            b_bg_terminator = true;
                            bg_reseve.RunWorkerAsync();
                        }

                        btn_connect.Text = "STOP";
                        btn_setting.Enabled = false;
                    }
                    else
                    {
                        b_bg_terminator = true;
                        bg_reseve.RunWorkerAsync();
                        btn_connect.Text = "STOP";
                        btn_setting.Enabled = false;
                        //MessageBox.Show("시리얼 포트를 찾을 수 없습니다.", "시리얼 포트 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if(btn_connect.Text == "STOP")
            {
                b_bg_terminator = false;
                b_bg_task_terminator = false;

                btn_connect.Text = "START";
                
                btn_setting.Enabled = true;
            }         
        }


        private void Zigbee_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            
        }




        private void Serial_Timer_Tick(object sender, EventArgs e)
        {
                     
        }


        static public void Insert_Err_Q(string err_message)
        {
            Err_Q1.Enqueue(err_message);
        }

        private void Main_Task_DoWork(object sender, DoWorkEventArgs e)
        {
            System.Threading.Thread.Sleep(1000);

            while (b_bg_task_terminator == true)
            {
                if (Err_Q1.Count != 0)
                {
                    MessageBox.Show(Err_Q1.Dequeue());
                }
                else
                {
                    //Chk_Serial_Port();
                    //Get_DO_ST();
                    //Chk_DO();
                    chk_status();
                    
                    if(cmd_Q.Count != 0)
                    {
                        switch (Cmd_step)
                        {
                            case (int)CMD_ST.CMD_Wait :    // wait

                                break;
                            case (int)CMD_ST.CMD_LD_Move :     // LD_MOVE
                                break;
                            case (int)CMD_ST.CMD_Arrive :
                                break;
                            case (int)CMD_ST.CMD_Deltha_Heading :
                                break;
                            case (int)CMD_ST.CMD_Read_QR:
                                break;
                            case (int)CMD_ST.CMD_Conveyor_Move:
                                break;
                            case (int)CMD_ST.CMD_Conveyor_Run :
                                break;
                            case (int)CMD_ST.CMD_Comp:
                                break;
                        }
                    }
                }                
                System.Threading.Thread.Sleep(1000);
            }
        }
        

        private void chk_status()
        {
            byte[] B_buf = new byte["oneLineStatus".Length];

            if (LD[0].LD_Is_connection == true)
            {
                Send_string(LD[0].LD_Client, "oneLineStatus");
                Send_string(LD[0].LD_Client, "getInfo WirelessQuality");
                Send_string(LD[0].LD_Client, "getInfo CurrentDraw");
                Send_string(LD[0].LD_Client, "getInfo PackVoltage");
                Send_string(LD[0].LD_Client, "getInfo Odometer(KM)");
            }

            for (int i = 1; i <= 16; i++)
            {
                B_buf = Encoding.Default.GetBytes("inQ i" + i.ToString());

                Array.Resize(ref B_buf, B_buf.Length + 2);

                B_buf[B_buf.Length - 2] = 0x0D;
                B_buf[B_buf.Length - 1] = 0x0A;

                LD[0].LD_Client.Send(B_buf);


                B_buf = Encoding.Default.GetBytes("outQ o" + i.ToString());

                Array.Resize(ref B_buf, B_buf.Length + 2);

                B_buf[B_buf.Length - 2] = 0x0D;
                B_buf[B_buf.Length - 1] = 0x0A;

                LD[0].LD_Client.Send(B_buf);
            }
        }
        
        private bool Chk_DO()
        {
            
            
            return false;
        }

        private void Get_DO_ST()
        {
            Gate1_st[1] = Gate1_st[0];
            Gate1_st[0] = Get_IO_ST(AMC_Test.Properties.Settings.Default.DO01);

            Gate2_st[1] = Gate2_st[0];
            Gate2_st[0] = Get_IO_ST(AMC_Test.Properties.Settings.Default.DO02);

            Gate3_st[1] = Gate3_st[0];
            Gate3_st[0] = Get_IO_ST(AMC_Test.Properties.Settings.Default.DO03);

            Gate4_st[1] = Gate4_st[0];
            Gate4_st[0] = Get_IO_ST(AMC_Test.Properties.Settings.Default.DO04);
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
           // Setting.ShowDialog();
        }

        private void HMI_Main_Load(object sender, EventArgs e)
        {
            

            Set_1mm_Val();

            btn_connect_Click(sender, e);
            //t_saver.Enabled = true;

            //b_bg_LDS_treminator = true;
            //bg_LDS.RunWorkerAsync();

            //LDS_Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //LDS_Client.Connect(new IPEndPoint(IPAddress.Parse("192.168.0.2"), 1002));
            

            
            //Saver.Show();
            //Saver.Hide();
        }



       



        private void button1_Click_1(object sender, EventArgs e)
        {
            Form1.DMR.ShowDialog();


            
            //if (n_deltaHeading_step == 0)
            //{
            //    LD[0].LD_ST.LD_LDS_CORREC_ANG = 3;
            //    n_deltaHeading_step = 1;
            //}
            //else if (n_deltaHeading_step == 1)
            //{
            //    LD[0].LD_ST.LD_LDS_CORREC_ANG = 0;
            //    n_deltaHeading_step = 0;
            //}


            //int now_angle = Math.Abs(Send_Delta_Heading(LD[0].LD_ST.LD_LDS_CORREC_ANG));

            //if (now_angle == 0)
            //{
            //    ndeltaHeading_Task_cnt++;
            //}
        }

        private void bg_reseve_DoWork(object sender, DoWorkEventArgs e)
        {
            Byte[] _data = new Byte[200];
            String _buf;
            String[] SA_buf = new string[10];

            byte[] B_buf = new byte["oneLineStatus".Length];
            byte[] Bbuf = new byte["oneLineStatus".Length];

            while (b_bg_terminator)
            {
                for (int i = 0; i < LD.Length; i++)
                {
                    if (LD[i].LD_Is_connection == true)
                    {
                        LD[i].LD_Client.Receive(_data);
                        _buf = Encoding.Default.GetString(_data);
                        Insert_Listbox(0, _buf, LD[i].LD_Client);
                        _data = new byte[10240];
                        _buf = "";
                    }
                    System.Threading.Thread.Sleep(100);
                }
            }
        }


        private void Insert_Log(string msg)
        {

            string date = System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss:FFF"); ;
            string str_temp = "";
            string log_dir = System.Environment.CurrentDirectory + "\\Log\\" + System.DateTime.Now.ToString("yyyy/MM/dd") +" Log.txt";

            if(System.IO.File.Exists(log_dir) == false)
            {
                Directory.CreateDirectory(System.Environment.CurrentDirectory + "\\Log\\");
                string temp;

                temp = "========================================================" + Environment.NewLine;
                temp += "=                                                                                                            =" + Environment.NewLine;
                temp += "=                                      AMC Log File " + System.DateTime.Now.ToString("yyyy/MM/dd") + "                                    =" + Environment.NewLine;
                temp += "=                                                                                                            =" + Environment.NewLine;
                temp += "========================================================" + Environment.NewLine;

                System.IO.File.WriteAllText(log_dir, temp);                
            }
            else
            {
                //str_buf = System.IO.File.ReadAllText(log_dir);

                string[] arr_str = msg.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                System.IO.StreamWriter st = System.IO.File.AppendText(log_dir);

                for (int i = 0;  i < arr_str.Length; i++)
                {
                    if(arr_str[i].Trim('\0') != "")
                    {
                        str_temp = date + " " + arr_str[i];
                        st.WriteLine(str_temp);
                    }
                }

                st.Dispose();

                //if(arr_str.Length == 0)
                //{
                //    System.IO.File.AppendAllText(log_dir, str_temp);
                //}
                //else if (arr_str[arr_str.Length - 1] != str_temp)
                //{
                //    System.IO.File.AppendAllText(log_dir, str_temp);
                //}                
            }            
        }

        private void Insert_Listbox(int index, string data, Socket LD_Socket)
        {
            try
            {
                this.Invoke(new MethodInvoker(
                    delegate ()
                    {
                    string[] str_buf = new string[100];

                        Insert_Log(data);
                        data = data.Replace('\n', '\0');
                        data = data.Trim('\0');
                        str_buf = data.Split('\r');

                    for (int i = 0; i < (str_buf.Length == 1 ? str_buf.Length : str_buf.Length - 1); i++)
                    {
                        //str_buf[i] = str_buf[i].Replace('\n', ' ');
                        str_buf[i] = str_buf[i].Trim('\0');

                        if (lb_log.Items[lb_log.Items.Count - 1].ToString() != str_buf[i])
                        {
                            string[] str_temp = new string[20];

                            str_temp = str_buf[i].Split(' ');

                                if (str_temp[0] == "Status:")
                                {
                                    for (int n = 0; n < LD.Length; n++)
                                    {
                                        if (LD[n].LD_Client == LD_Socket)
                                        {
                                            for (int j = 0; j < str_temp.Length - 1; j++)
                                            {
                                                if (str_temp[j] == "Status:")
                                                {
                                                    string str = "";
                                                    for (int m = j + 1; m < str_temp.Length; m++)
                                                    {
                                                        if (str_temp[m] == "StateOfCharge:")
                                                        {
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            str += str_temp[m] + " ";
                                                        }
                                                    }
                                                    LD[n].LD_ST.LD_ST = str;
                                                    l_status.Text = str;
                                                    Saver.l_status.Text = str;

                                                }
                                                else if (str_temp[j] == "StateOfCharge:")
                                                {
                                                    LD[n].LD_ST.LD_CHARGE = str_temp[j + 1];
                                                    l_charge.Text = LD[n].LD_ST.LD_CHARGE + "%";
                                                }
                                                else if (str_temp[j] == "Location:")
                                                {
                                                    LD[n].LD_ST.LD_LOC.LD_X = int.Parse(str_temp[j + 1]);
                                                    LD[n].LD_ST.LD_LOC.LD_Y = int.Parse(str_temp[j + 2]);
                                                    LD[n].LD_ST.LD_LOC.LD_A = int.Parse(str_temp[j + 3]);

                                                    LD[n].LD_DISTANCE = Math.Sqrt(Math.Pow(Math.Abs((LD[n].LD_ST.LD_LOC.LD_X - Now_Dev.X)), 2) +
                                                                                Math.Pow(Math.Abs(LD[n].LD_ST.LD_LOC.LD_Y - Now_Dev.Y), 2));

                                                    tb_lo_x.Text = LD[n].LD_ST.LD_LOC.LD_X.ToString();
                                                    tb_lo_y.Text = LD[n].LD_ST.LD_LOC.LD_Y.ToString();
                                                    tb_lo_a.Text = LD[n].LD_ST.LD_LOC.LD_A.ToString();

                                                }
                                                else if (str_temp[j] == "Temperature:")
                                                {
                                                    LD[n].LD_ST.LD_TEMP = str_temp[j + 1];
                                                    Saver.Set_tm_Value(str_temp[j + 1]);
                                                    tb_temp.Text = LD[n].LD_ST.LD_TEMP;
                                                }
                                                else
                                                {
                                                    LD[n].LD_ST.LD_ST += " " + str_temp[j];

                                                    if (str_temp[1] == "Doing" && str_temp[2] == "task" && str_temp[3] == "wait")
                                                    {
                                                        //Set_Wait_Mode();
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else if (str_temp[0] == "Input:" || str_temp[0] == "Output:")
                                {
                                    Set_io_lamp(str_temp[0], str_temp[1], str_temp[2]);
                                }
                                else if (str_temp[0] == "Enter" && str_temp[1] == "password:")
                                {
                                    for (int j = 0; j < LD.Length; j++)
                                    {
                                        if (LD[i].LD_Is_connection == true)
                                        {
                                            byte[] _buf = new byte[10];
                                            byte[] buf = new byte[10];

                                            _buf = Encoding.Default.GetBytes("1111");

                                            Array.Resize(ref _buf, "1111".Length + 2);

                                            _buf["1111".Length] = 0x0D;
                                            _buf["1111".Length + 1] = 0x0A;

                                            LD[i].LD_Client.Send(_buf, 0, 6, SocketFlags.None);
                                        }
                                    }

                                    lb_log.Items.Insert(lb_log.Items.Count, str_buf[i]);
                                    lb_log.SetSelected(lb_log.Items.Count - 1, true);

                                    lb_log.Items.Insert(lb_log.Items.Count, "1111");
                                    lb_log.SetSelected(lb_log.Items.Count - 1, true);

                                    b_bg_terminator = true;
                                }
                                else if (str_temp[0] == "Welcome")
                                {
                                    b_bg_task_terminator = true;

                                    Main_Task.RunWorkerAsync();
                                }
                                else if (str_temp[0] == "Goal:")
                                {
                                    for (int j = 0; j < LD.Length; j++)
                                    {
                                        if (LD[j].LD_Client == LD_Socket)
                                        {                                            
                                            //Add_Goal(LD[j], str_temp[1]);
                                        }
                                    }
                                }
                                else if (str_temp[0] == "End" && str_temp[1] == "of" && str_temp[2] == "goals")
                                {
                                    for (int j = 0; j < LD.Length; j++)
                                    {
                                        if (LD[j].LD_Client == LD_Socket)
                                        {
                                            //Add_Goal(LD[j], goal);
                                        }
                                    }
                                }
                                else if (str_temp[0] == "Arrived" && str_temp[1] == "at")
                                {                                        
                                        if (LD[0].LD_Client == LD_Socket && str_temp[2] == "Goal5")
                                        {
                                            b_bg_deltaHeading = true;
                                            ndeltaHeading_Task_cnt = 1;
                                            bg_deltaHeading.RunWorkerAsync();
                                            //Set_Wait_Mode();
                                        }
                                }
                                else if (str_temp[0] == "Completed" && str_temp[1] == "doing" && str_temp[2] == "task")
                                {
                                    if (str_temp[3] == "wait")
                                    {
                                        if (str_temp[4] == Now_Dev.force_Relese_Time.ToString())
                                        {

                                        }
                                    }
                                    else if(str_temp[3] == "deltaHeading")                                    
                                    {
                                        //Send_string(LD[0].LD_Client, "doTask wait " + Now_Dev.force_Relese_Time);
                                    }
                                    else if(str_temp[3] == "move")
                                    {
                                        //if(str_temp[4] == "1000" && str_temp[5] == "50" && str_temp[6] == "150"  && str_temp[7] == "50" && str_temp[8] == "30" && str_temp[9] == "0" && str_temp[10] == "0" && str_temp[11] == "0" && str_temp[12] == "True" && str_temp[13] == "True" && str_temp[14] == "True")
                                        {
                                            n_move_1000_comp = 3;
                                        }
                                    }


                                    
                                }
                                else if(str_temp[0] == "Info:")
                                {
                                    if (str_temp[1] == "WirelessQuality")
                                    {                                        
                                        Saver.Set_lm_wifi(int.Parse(str_temp[2].Substring(0, str_temp[2].Length - 1)));
                                        tb_wifi.Text = str_temp[2].Substring(0, str_temp[2].Length - 1);
                                    }
                                    else if (str_temp[1] == "PackVoltage")
                                    {                                        
                                        Saver.Set_gg_Value(str_temp[2]);
                                        tb_volt.Text = str_temp[2];
                                    }
                                    else if(str_temp[1] == "Odometer(KM)")
                                    {
                                        Saver.Set_om_value(str_temp[2]);
                                        tb_odo.Text = str_temp[2];
                                    }
                                    else if(str_temp[1] == "CurrentDraw")
                                    {

                                    }
                                }
                            else if(str_temp[0] == "LDS1")
                                {
                                    if (LD_Socket == LDS_Client)
                                    {
                                        str_buf[i] = str_buf[i].Trim('\0');

                                        string[] str_temp1 = str_buf[i].Split(' ');
                                        if (str_temp1.Length == 4)
                                        {
                                            LD[0].LD_ST.LD_LDS1 = "";
                                            LD[0].LD_ST.LD_LDS2 = "";

                                            if (str_temp1[0] == "LDS1")
                                            {
                                                LD[0].LD_ST.LD_LDS1 = str_temp1[1];
                                            }

                                            if (str_temp1[2] == "LDS2")
                                            {
                                                LD[0].LD_ST.LD_LDS2 = str_temp1[3];
                                            }
                                            Get_LDS_Distance();
                                        }

                                        lb_log.Items.Insert(lb_log.Items.Count, LD[0].LD_ST.LD_LDS1_DISTANCE + ", " + LD[0].LD_ST.LD_LDS2_DISTANCE + ", " + LD[0].LD_ST.LD_LDS_Angle);
                                        lb_log.SetSelected(lb_log.Items.Count - 1, true);
                                    }
                                }
                                else
                                {
                                    if (str_buf[i].Length > List_Box_Line_Conut)
                                    {
                                        string[] str_line = new string[10];

                                        for (int j = 0; j < (str_buf[i].Length / List_Box_Line_Conut) + 1; j++)
                                        {
                                            str_line[j] = str_buf[i].Substring(j * List_Box_Line_Conut, str_buf[i].Length > (j * List_Box_Line_Conut) + List_Box_Line_Conut ? List_Box_Line_Conut : str_buf[i].Length - (j * List_Box_Line_Conut));
                                            lb_log.Items.Insert(lb_log.Items.Count, str_line[j]);
                                            lb_log.SetSelected(lb_log.Items.Count - 1, true);
                                        }
                                    }
                                    else
                                    {
                                        lb_log.Items.Insert(lb_log.Items.Count, str_buf[i]);
                                        lb_log.SetSelected(lb_log.Items.Count - 1, true);
                                    }
                                }
                            }
                            else
                            {
                               
                            }
                        
                            if (str_buf[i] == "Welcome to the server.")
                            {

                            }
                        }
                    }
                    )
                );
            }
            catch (Exception ex)
            {
                Make_error(ex, "Insert_Listbox");
            }
        }


        private void Send_string(Socket ld_socket, string data)
        {
            try
            {
                byte[] _buf = new byte[1024];
                byte[] buf = new byte[1024];

                Byte[] _data = new Byte[10240];

                _buf = Encoding.Default.GetBytes(data);

                Array.Resize(ref _buf, data.Length + 2);
                _buf[data.Length] = 0x0D;
                _buf[data.Length + 1] = 0x0A;

                if(ld_socket.Connected == true)                        
                    ld_socket.Send(_buf);
            }
            catch (Exception ex)
            {
                Make_error(ex, "Send_string");
            }
        }

        private void Make_error(Exception Err, string fuc_name)
        {
            MessageBox.Show(Err.Message, fuc_name, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void t_saver_Tick(object sender, EventArgs e)
        {
            //Saver.Show();

            t_saver.Enabled = false;
        }

        private void HMI_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            b_bg_task_terminator = false;
            b_bg_terminator = false;

            if (e.CloseReason != CloseReason.WindowsShutDown)
            {
                this.Hide();
                e.Cancel = true;
            }
        }

        public void Set_io_lamp(string IO, string num, string st)
        {
            string a = num.Substring(1, num.Length - 1);

            //st = st.Substring(0, st.Length - 1);

            if (IO == "Input:")
            {
                if (num.Substring(1, num.Length - 1) == "1")
                {
                    pb_i1.Image = (st == "off" ? AMC_Test.Properties.Resources.circle_grey.Clone() as Image : AMC_Test.Properties.Resources.circle_green.Clone() as Image);
                }
                else if (num.Substring(1, num.Length - 1) == "2")
                {
                    pb_i2.Image = (st == "off" ? AMC_Test.Properties.Resources.circle_grey.Clone() as Image : AMC_Test.Properties.Resources.circle_green.Clone() as Image);
                }
                else if (num.Substring(1, num.Length - 1) == "3")
                {
                    pb_i3.Image = (st == "off" ? AMC_Test.Properties.Resources.circle_grey.Clone() as Image : AMC_Test.Properties.Resources.circle_green.Clone() as Image);
                }
                else if (num.Substring(1, num.Length - 1) == "4")
                {
                    pb_i4.Image = (st == "off" ? AMC_Test.Properties.Resources.circle_grey.Clone() as Image : AMC_Test.Properties.Resources.circle_green.Clone() as Image);
                }
                else if (num.Substring(1, num.Length - 1) == "5")
                {
                    pb_i5.Image = (st == "off" ? AMC_Test.Properties.Resources.circle_grey.Clone() as Image : AMC_Test.Properties.Resources.circle_green.Clone() as Image);
                }
                else if (num.Substring(1, num.Length - 1) == "6")
                {
                    pb_i6.Image = (st == "off" ? AMC_Test.Properties.Resources.circle_grey.Clone() as Image : AMC_Test.Properties.Resources.circle_green.Clone() as Image);
                }
                else if (num.Substring(1, num.Length - 1) == "7")
                {
                    pb_i7.Image = (st == "off" ? AMC_Test.Properties.Resources.circle_grey.Clone() as Image : AMC_Test.Properties.Resources.circle_green.Clone() as Image);
                }
                else if (num.Substring(1, num.Length - 1) == "8")
                {
                    pb_i8.Image = (st == "off" ? AMC_Test.Properties.Resources.circle_grey.Clone() as Image : AMC_Test.Properties.Resources.circle_green.Clone() as Image);
                }
                else if (num.Substring(1, num.Length - 1) == "9")
                {
                    pb_i9.Image = (st == "off" ? AMC_Test.Properties.Resources.circle_grey.Clone() as Image : AMC_Test.Properties.Resources.circle_green.Clone() as Image);
                }
                else if (num.Substring(1, num.Length - 1) == "10")
                {
                    pb_i10.Image = (st == "off" ? AMC_Test.Properties.Resources.circle_grey.Clone() as Image : AMC_Test.Properties.Resources.circle_green.Clone() as Image);
                }
                else if (num.Substring(1, num.Length - 1) == "11")
                {
                    pb_i11.Image = (st == "off" ? AMC_Test.Properties.Resources.circle_grey.Clone() as Image : AMC_Test.Properties.Resources.circle_green.Clone() as Image);
                }
                else if (num.Substring(1, num.Length - 1) == "12")
                {
                    pb_i12.Image = (st == "off" ? AMC_Test.Properties.Resources.circle_grey.Clone() as Image : AMC_Test.Properties.Resources.circle_green.Clone() as Image);
                }
                else if (num.Substring(1, num.Length - 1) == "13")
                {
                    pb_i13.Image = (st == "off" ? AMC_Test.Properties.Resources.circle_grey.Clone() as Image : AMC_Test.Properties.Resources.circle_green.Clone() as Image);
                }
                else if (num.Substring(1, num.Length - 1) == "14")
                {
                    pb_i14.Image = (st == "off" ? AMC_Test.Properties.Resources.circle_grey.Clone() as Image : AMC_Test.Properties.Resources.circle_green.Clone() as Image);
                }
                else if (num.Substring(1, num.Length - 1) == "15")
                {
                    pb_i15.Image = (st == "off" ? AMC_Test.Properties.Resources.circle_grey.Clone() as Image : AMC_Test.Properties.Resources.circle_green.Clone() as Image);
                }
                else if (num.Substring(1, num.Length - 1) == "16")
                {
                    pb_i16.Image = (st == "off" ? AMC_Test.Properties.Resources.circle_grey.Clone() as Image : AMC_Test.Properties.Resources.circle_green.Clone() as Image);
                }
            }
            else
            {
                if (num.Substring(1, num.Length - 1) == "1")
                {
                    pb_o1.Image = (st == "off" ? AMC_Test.Properties.Resources.circle_grey.Clone() as Image : AMC_Test.Properties.Resources.circle_red.Clone() as Image);
                    pb_o1.Tag = (st == "off" ? "off" : "on");
                }
                else if (num.Substring(1, num.Length - 1) == "2")
                {
                    pb_o2.Image = (st == "off" ? AMC_Test.Properties.Resources.circle_grey.Clone() as Image : AMC_Test.Properties.Resources.circle_red.Clone() as Image);
                    pb_o2.Tag = (st == "off" ? "off" : "on");
                }
                else if (num.Substring(1, num.Length - 1) == "3")
                {
                    pb_o3.Image = (st == "off" ? AMC_Test.Properties.Resources.circle_grey.Clone() as Image : AMC_Test.Properties.Resources.circle_red.Clone() as Image);
                    pb_o3.Tag = (st == "off" ? "off" : "on");
                }
                else if (num.Substring(1, num.Length - 1) == "4")
                {
                    pb_o4.Image = (st == "off" ? AMC_Test.Properties.Resources.circle_grey.Clone() as Image : AMC_Test.Properties.Resources.circle_red.Clone() as Image);
                    pb_o4.Tag = (st == "off" ? "off" : "on");
                }
                else if (num.Substring(1, num.Length - 1) == "5")
                {
                    pb_o5.Image = (st == "off" ? AMC_Test.Properties.Resources.circle_grey.Clone() as Image : AMC_Test.Properties.Resources.circle_red.Clone() as Image);
                    pb_o5.Tag = (st == "off" ? "off" : "on");
                }
                else if (num.Substring(1, num.Length - 1) == "6")
                {
                    pb_o6.Image = (st == "off" ? AMC_Test.Properties.Resources.circle_grey.Clone() as Image : AMC_Test.Properties.Resources.circle_red.Clone() as Image);
                    pb_o6.Tag = (st == "off" ? "off" : "on");
                }
                else if (num.Substring(1, num.Length - 1) == "7")
                {
                    pb_o7.Image = (st == "off" ? AMC_Test.Properties.Resources.circle_grey.Clone() as Image : AMC_Test.Properties.Resources.circle_red.Clone() as Image);
                    pb_o7.Tag = (st == "off" ? "off" : "on");
                }
                else if (num.Substring(1, num.Length - 1) == "8")
                {
                    pb_o8.Image = (st == "off" ? AMC_Test.Properties.Resources.circle_grey.Clone() as Image : AMC_Test.Properties.Resources.circle_red.Clone() as Image);
                    pb_o8.Tag = (st == "off" ? "off" : "on");
                }
                else if (num.Substring(1, num.Length - 1) == "9")
                {
                    pb_o9.Image = (st == "off" ? AMC_Test.Properties.Resources.circle_grey.Clone() as Image : AMC_Test.Properties.Resources.circle_red.Clone() as Image);
                    pb_o9.Tag = (st == "off" ? "off" : "on");
                }
                else if (num.Substring(1, num.Length - 1) == "10")
                {
                    pb_o10.Image = (st == "off" ? AMC_Test.Properties.Resources.circle_grey.Clone() as Image : AMC_Test.Properties.Resources.circle_red.Clone() as Image);
                    pb_o10.Tag = (st == "off" ? "off" : "on");
                }
                else if (num.Substring(1, num.Length - 1) == "11")
                {
                    pb_o11.Image = (st == "off" ? AMC_Test.Properties.Resources.circle_grey.Clone() as Image : AMC_Test.Properties.Resources.circle_red.Clone() as Image);
                    pb_o11.Tag = (st == "off" ? "off" : "on");
                }
                else if (num.Substring(1, num.Length - 1) == "12")
                {
                    pb_o12.Image = (st == "off" ? AMC_Test.Properties.Resources.circle_grey.Clone() as Image : AMC_Test.Properties.Resources.circle_red.Clone() as Image);
                    pb_o12.Tag = (st == "off" ? "off" : "on");
                }
                else if (num.Substring(1, num.Length - 1) == "13")
                {
                    pb_o13.Image = (st == "off" ? AMC_Test.Properties.Resources.circle_grey.Clone() as Image : AMC_Test.Properties.Resources.circle_red.Clone() as Image);
                    pb_o13.Tag = (st == "off" ? "off" : "on");
                }
                else if (num.Substring(1, num.Length - 1) == "14")
                {
                    pb_o14.Image = (st == "off" ? AMC_Test.Properties.Resources.circle_grey.Clone() as Image : AMC_Test.Properties.Resources.circle_red.Clone() as Image);
                    pb_o14.Tag = (st == "off" ? "off" : "on");
                }
                else if (num.Substring(1, num.Length - 1) == "15")
                {
                    pb_o15.Image = (st == "off" ? AMC_Test.Properties.Resources.circle_grey.Clone() as Image : AMC_Test.Properties.Resources.circle_red.Clone() as Image);
                    pb_o15.Tag = (st == "off" ? "off" : "on");
                }
                else if (num.Substring(1, num.Length - 1) == "16")
                {
                    pb_o16.Image = (st == "off" ? AMC_Test.Properties.Resources.circle_grey.Clone() as Image : AMC_Test.Properties.Resources.circle_red.Clone() as Image);
                    pb_o16.Tag = (st == "off" ? "off" : "on");
                }
            }
        }


         public bool Get_IO_ST(string DIO_NUM)
        {
            DIO_NUM = DIO_NUM.ToUpper();

            if (DIO_NUM == "1")
            {
                return (string)pb_o1.Tag == "off" ? false : true;
            }
            else if (DIO_NUM == "2")
            {
                return (string)pb_o2.Tag == "off" ? false : true;
            }

            return false;
        }

        private void bg_LDS_DoWork(object sender, DoWorkEventArgs e)
        {
            /*
            Byte[] _data = new Byte[200];
            String _buf;
            String[] SA_buf = new string[10];

            NetworkStream stream = null;
            TcpListener tcpListener = null;
            StreamReader reader = null;

            //IP주소를 나타내는 객체를 생성,TcpListener를 생성시 인자로 사용할려고

            IPAddress ipAd = IPAddress.Parse("192.168.0.102");

            //TcpListener Class를 이용하여 클라이언트의 연결을 받아 들인다.
            tcpListener = new TcpListener(ipAd, 1002);
            tcpListener.Start();



            //Client의 접속이 올때 까지 Block 되는 부분, 대개 이부분을 Thread로 만들어 보내 버린다.
            //백그라운드 Thread에 처리를 맡긴다.

            LDS_Client = tcpListener.AcceptSocket();
            //클라이언트의 데이터를 읽고, 쓰기 위한 스트림을 만든다.

            stream = new NetworkStream(LDS_Client);

            Encoding encode = System.Text.Encoding.GetEncoding("ks_c_5601-1987");

            reader = new StreamReader(stream, encode);

            while (b_bg_LDS_treminator)
            {
                LDS_Client.Receive(_data);
                _buf = Encoding.Default.GetString(_data);
                
                //_buf = reader.ReadLine();                
                Insert_Listbox(0, _buf, LDS_Client);
                _data = new byte[10240];
                _buf = "";                
                System.Threading.Thread.Sleep(100);
            }
            */
        }

        private void Set_1mm_Val()
        {
            int val_diff = LDS_Max_Val - LDS_Min_Val;
            int dis_diff = LDS_Max_Distance - LDS_Min_Distance;

            LDS_1mm_Val =  val_diff/ dis_diff;
        }


        private void Get_LDS_Distance()
        {
            if(int.Parse(LD[0].LD_ST.LD_LDS1, System.Globalization.NumberStyles.HexNumber) > LDS_Min_Val && int.Parse(LD[0].LD_ST.LD_LDS1, System.Globalization.NumberStyles.HexNumber) < LDS_Max_Val )
            {
                LD[0].LD_ST.LD_LDS1_DISTANCE = (int.Parse(LD[0].LD_ST.LD_LDS1, System.Globalization.NumberStyles.HexNumber) / LDS_1mm_Val).ToString();
            }else
            {
                LD[0].LD_ST.LD_LDS1_DISTANCE = "0";
            }

            if (int.Parse(LD[0].LD_ST.LD_LDS2, System.Globalization.NumberStyles.HexNumber) > LDS_Min_Val && int.Parse(LD[0].LD_ST.LD_LDS2, System.Globalization.NumberStyles.HexNumber) < LDS_Max_Val)
            {
                LD[0].LD_ST.LD_LDS2_DISTANCE = (int.Parse(LD[0].LD_ST.LD_LDS2, System.Globalization.NumberStyles.HexNumber) / LDS_1mm_Val).ToString();
            }
            else
            {
                LD[0].LD_ST.LD_LDS2_DISTANCE = "0";
            }

            if (int.Parse(LD[0].LD_ST.LD_LDS1, System.Globalization.NumberStyles.HexNumber) > LDS_Min_Val && int.Parse(LD[0].LD_ST.LD_LDS1, System.Globalization.NumberStyles.HexNumber) < LDS_Max_Val && int.Parse(LD[0].LD_ST.LD_LDS2, System.Globalization.NumberStyles.HexNumber) > LDS_Min_Val && int.Parse(LD[0].LD_ST.LD_LDS2, System.Globalization.NumberStyles.HexNumber) < LDS_Max_Val)
                Get_LDS_Angle();
        }


        private void Get_LDS_Angle()
        {
            double d_diff;

            LD[0].LD_ST.LD_LDS_DIS_DIFF = double.Parse(LD[0].LD_ST.LD_LDS1_DISTANCE) - double.Parse(LD[0].LD_ST.LD_LDS2_DISTANCE);
            d_diff = LD[0].LD_ST.LD_LDS_DIS_DIFF < 0 ? LD[0].LD_ST.LD_LDS_DIS_DIFF * -1 : LD[0].LD_ST.LD_LDS_DIS_DIFF;

            if (d_diff <= 1)
            {
                LD[0].LD_ST.LD_LDS_Angle = 0;
                Insert_Listbox(0, LD[0].LD_ST.LD_LDS1_DISTANCE.ToString() + " - " + LD[0].LD_ST.LD_LDS2_DISTANCE.ToString() + " = " + LD[0].LD_ST.LD_LDS_DIS_DIFF.ToString(), LD[0].LD_Client);
            }
            else if (d_diff <= 2)
            {
                if ((int)LD[0].LD_ST.LD_LDS_DIS_DIFF > 0)
                {
                    LD[0].LD_ST.LD_LDS_Angle = -1;
                }
                else
                {
                    LD[0].LD_ST.LD_LDS_Angle = 1;
                }
            }
            else if (d_diff <= 5)
            {
                if ((int)LD[0].LD_ST.LD_LDS_DIS_DIFF > 0)
                {
                    LD[0].LD_ST.LD_LDS_Angle = -2;
                }
                else
                {
                    LD[0].LD_ST.LD_LDS_Angle = 2;
                }
            }
            else if (d_diff <= 10)
            {
                if ((int)LD[0].LD_ST.LD_LDS_DIS_DIFF > 0)
                {
                    LD[0].LD_ST.LD_LDS_Angle = -3;
                }
                else
                {
                    LD[0].LD_ST.LD_LDS_Angle = 3;
                }
            }
            else if (d_diff > 10)
            {
                if ((int)LD[0].LD_ST.LD_LDS_DIS_DIFF > 0)
                {
                    LD[0].LD_ST.LD_LDS_Angle = -5;
                }
                else
                {
                    LD[0].LD_ST.LD_LDS_Angle = 5;
                }
            }
        }

        private void Fnc_deltaHeading()
        {
            int now_angle = Math.Abs(Send_Delta_Heading(LD[0].LD_ST.LD_LDS_CORREC_ANG));

            //if(now_angle == 0)
            //{
            //    ndeltaHeading_Task_cnt++;
            //}
            //else if (now_angle == LD[0].LD_ST.LD_LDS_Angle2)
            //{
            //    LD[0].LD_ST.LD_LDS_CORREC_CNT++;

            //    if (LD[0].LD_ST.LD_LDS_CORREC_CNT++ >= 2)
            //    {
            //        LD[0].LD_ST.LD_LDS_CORREC_CNT = 0;
            //        LD[0].LD_ST.LD_LDS_CORREC_ANG++;  
            //    }
            //}
            //else
            //{
            //    LD[0].LD_ST.LD_LDS_Angle2 = now_angle;
            //    LD[0].LD_ST.LD_LDS_CORREC_ANG = 0;
            //}


        }


        private int Send_Delta_Heading(double Correc_Ang)
        {
            int a=0;

            if(LD[0].LD_ST.LD_LDS1_DISTANCE == "0"  && LD[0].LD_ST.LD_LDS2_DISTANCE == "0")
            {
                ndeltaHeading_Task_cnt++;
                return 0;
            }


            if(Math.Abs(LD[0].LD_ST.LD_LDS_Angle) == 0)
            {
                a = 0;
                ndeltaHeading_Task_cnt++;
            }
            else if(Math.Abs(LD[0].LD_ST.LD_LDS_Angle) >= 10)
            {
                a = 5;
            }
            else if (Math.Abs(LD[0].LD_ST.LD_LDS_Angle) > 3)
            {
                a = 3;
            }
            else if (Math.Abs(LD[0].LD_ST.LD_LDS_Angle) <= 3)
            {
                a = 1;
            }

                Send_string(LD[0].LD_Client, "doTask deltaHeading " + ((LD[0].LD_ST.LD_LDS_Angle > 0) ? a.ToString() : (a*-1).ToString()));

            return (int)LD[0].LD_ST.LD_LDS_Angle;
        }

        private void bg_deltaHeading_DoWork(object sender, DoWorkEventArgs e)
        {
            int Sleep_val = 1000;

            while(b_bg_deltaHeading)
            {
                switch (ndeltaHeading_Task_cnt)
                {
                    case 1:
                        if((LD[0].LD_ST.LD_LDS1_DISTANCE == "0" || LD[0].LD_ST.LD_LDS2_DISTANCE == "0") && n_move_1000_comp == 0)
                        {
                            Send_string(LD[0].LD_Client, "doTask move 1500 50 150 50 30 0 0 0 True True False");
                            n_move_1000_comp = 1;
                        }

                        if(double.Parse((LD[0].LD_ST.LD_LDS1_DISTANCE ?? "10000")) < 350 && double.Parse((LD[0].LD_ST.LD_LDS1_DISTANCE ?? "10000")) < 350 && n_move_1000_comp == 3)
                        {
                            Sleep_val = 3000;
                            n_move_1000_comp = 0;
                            ndeltaHeading_Task_cnt++;
                        }
                        break;
                    case 2:
                        Fnc_deltaHeading();

                        if(LD[0].LD_ST.LD_LDS_Angle == 0)
                        {
                            ndeltaHeading_Task_cnt++;
                        }
                        break;
                    case 3:
                        Sleep_val = 1000;
                        double LDS_EV = (double.Parse(LD[0].LD_ST.LD_LDS1_DISTANCE) + double.Parse(LD[0].LD_ST.LD_LDS2_DISTANCE)) / 2;

                        if (n_move_1000_comp == 0)
                        {                            
                            Send_string(LD[0].LD_Client, "doTask move 200 50 140 50 30 5 0 0 True True False");
                            n_move_1000_comp = 1;
                        }

                        if (double.Parse(LD[0].LD_ST.LD_LDS1_DISTANCE) < 160 && double.Parse(LD[0].LD_ST.LD_LDS2_DISTANCE) < 160 && n_move_1000_comp == 3)
                        {
                            Sleep_val = 3000;
                            n_move_1000_comp = 0;
                            ndeltaHeading_Task_cnt++;
                        }
                        break;
                    case 4:
                        Fnc_deltaHeading();
                        break;
                    case 5:
                        if (MessageBox.Show("Run Lift", "Warring", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                            ndeltaHeading_Task_cnt++;
                        break;
                    case 6:
                        Send_string(LD[0].LD_Client, "doTask move -1000 50 150 50 30 0 0 0 True True True");
                        ndeltaHeading_Task_cnt++;
                        break;
                    case 7:
                        ndeltaHeading_Task_cnt = 0;
                        b_bg_deltaHeading = false;                        
                        break;
                }
                
                System.Threading.Thread.Sleep(Sleep_val);
            }
        }


        public void Set_STATUS(string msg)
        {
            l_status.Text = msg;
            Saver.l_status.Text = msg;
        }

        public void Set_BATT(string batt)
        {
            l_charge.Text = batt;
        }


        public void Set_LOC(int x, int y, int a)
        {
            tb_lo_x.Text = x.ToString();
            tb_lo_y.Text = y.ToString();
            tb_lo_a.Text = a.ToString();
        }

        public void Set_Tempreture(string temp)
        {
            Saver.Set_tm_Value(temp);
            tb_temp.Text = temp;
        }

        public void Set_VOLT(string temp)
        {
            Saver.Set_gg_Value(temp);
            tb_volt.Text = temp;
        }

        public void Set_WIFI(string temp)
        {
            Saver.Set_lm_wifi(int.Parse(temp));
            tb_wifi.Text = temp;
        }

        public void Set_ODO(string temp)
        {
            Saver.Set_om_value(temp);
            tb_odo.Text = temp;
        }

    }
}
