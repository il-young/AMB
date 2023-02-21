using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using Modbus.Device;

using SocketGlobal;
using SocketGlobal.SendData;
using SuperSocket.ClientEngine;

using System.Media;
using System.Diagnostics;
using System.IO;

using Skynet;

namespace AMC_Test
{
    public partial class Form1 : Form
    {
        public struct LD_Dev
        {
            public string LD_NAME;
            public string LD_TYPE;
            public IPAddress LD_IP;
            public int LD_PORT;
            public Socket LD_Client;
            public List<string> LD_GOAL;
            public bool LD_Is_connection;
            public LD_Status LD_ST;
            public double LD_DISTANCE;
            public bool AUTO;
            public string LD_ID;
            public string LD_PW;
            public stRFID RFID;
            public string ZIGBEE_NUM;
            public string ZIGBEE_PORT;
            public int ALARM_DO_NUM;
            public int BATT_HIGH_LIMIT;
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

            public string LD_WIFI;
            public string LD_VOLT;
            public string LD_ODO;

            public double LD_LDS_CORREC_CNT;
            public double LD_LDS_CORREC_ANG;

            public bool Is_Deltaheading_Comp;

            public string LD_Localizetion_Score;

            public string LD_AREA;
        };

        public struct LD_Location
        {
            public int LD_X;
            public int LD_Y;
            public int LD_A;
        };

        public struct stDMR_POSITION
        {
            public int X;
            public int Y;
        }

        public struct stSERVER
        {
            public string SRV_NAME;
            public string SRV_IP;
            public string SRV_PORT;
            public Socket SRV_Client;
        }

        public struct stRFID
        {
            public IPAddress IP;
            public int PORT;
        }

        public struct stAREA
        {
            public string AREA_NAME;
            public Point P1, P2;
            public string Action;
        }

        public struct TAG_ID
        {
            public string TAG_PART;     // Goal name
            public string TAG_NAME;     // User name
            public string TAG_ID_NUM;   // User ID
            public bool TAG_TARGET;     // Login State true = login 
        }

        public struct stDOOR                                                                                    
        {
            public string NAME;
            public string ID;
            public string DO;
        }

        public enum nSKYNET
        {
            SM_RUN = 0,
            SM_IDLE,
            SM_ALARM
        };

        public struct stSKYNET
        {
            public string IP;
            public string LINE_CODE;
            public string PROCEESS_CODE;
            public string EQUIPMENT_ID;

            public nSKYNET B_STATUS_CODE;
            public nSKYNET STATUS_CODE;
            public string STATUS;
            public string SCR;
            public string DEST;

            public string ERR_CODE;
            public string ERR_TYPE;
            public string ERR_NAME;
            public string ERR_DESCRIPT;
            public string ERR_SOLUTION;
        }

        private List<stAREA> AREAs = new List<stAREA>();
        static public List<TAG_ID> CAPCODE_ARRAY = new List<TAG_ID>();
        static Skynet.Skynet skynet = new Skynet.Skynet();
        static public stSKYNET Skynet_Param;

        private CJOB AMC_JOB = new CJOB();

        static private ERR_Q ERR_QUEUE = new ERR_Q();

        protected const int List_Box_Line_Conut = 120;
        static public LD_Dev[] LD = new LD_Dev[10];
        public stSERVER AMC_SRV = new stSERVER();
        public stDOOR[] Doors = new stDOOR[10];

        bool b_bg_terminator = false;

        private bool bis_insert = false;
        protected int ndeltaHeading_Task_cnt;


        public Socket LDS_Client;
        private CALARM_SRV ALARM_SRV = new CALARM_SRV();

        private const int LDS_Min_Distance = 50;    //mm
        private const int LDS_Max_Distance = 450;   //mm
        private const int LDS_Min_Val = 6553;    //1V
        private const int LDS_Max_Val = 65535;   //10V
        private const int LDS_2_LDS_Distance = 370; //mm
        private double LDS_1mm_Val = 118.11222;    //mm


        private bool bCONECTED = false;
        protected bool bMODBUS_T = false;
        protected bool b_bg_LDS_terminator = false;
        protected bool bbg_CMD_terminator = false;
        protected bool bCmd_run = false;
        protected bool bst_terminator = false;
        protected bool bDISPLAY_T = false;
        static public bool bCONVEYOR_T = false;
        protected bool bRetry_T = false;
        protected bool bTimer_T = false;
        protected bool bEStop = false;
        protected bool bSkynet_connected = false;
        protected int nSkynet_Start = 0;
        static protected int nSkynet_Res = 0;

        private DateTime Err_chk_time;
        private int Err_chk_mil;
        private string Err_chk_msg;
        private int Err_chk_retry;

        static private AsyncTcpSession AMC_Client;
        static private string[] str_cmd_arr = new string[20];
        static public int nCmd_Step = 0;
        private int nroof_cnt = 0;
        private int nroof_max = 0;


        private ModbusSerialMaster IO_modbusMaster;
        private SerialPort IO_SerialPort;
        private bool[,] INPUT_DATA;
        static private bool[,] OUTPUT_DATA;
        private bool[] DIGITAL_DATA = new bool[32];
        private bool[,] LD_DI = new bool[2, 16];
        private bool[,] LD_DO = new bool[2, 16];
        private bool[] LD_DIGITAL_OUT = new bool[16];
        private bool[] LD_DIGITAL_OUT_B = new bool[16];


        private string Conveyor_MODE = "";

        private CPager Pager = new CPager();

        static public DRS0201 STOPPER = new DRS0201();

        private ushort LINEAR_AL;
        private short LINEAR_POS;

        static public HMI_Main HMI = new HMI_Main();
        static public MainForm DMR = new MainForm();
        static public RFID_Main frm_RFID = new RFID_Main();
        public AMC_Monitor Monitor = new AMC_Monitor();

        private bool Continue_JOB = false;
        private bool Conveyor_RUN_ST = false;
        private bool IN_JOB_END_ST = false;
        private bool OUT_JOB_END_ST = false;
        private bool in_job_finish = false;

        static string CMD_NAME;
        string STB_NAME;
        string GOAL_NAME;
        string STATUS;

        byte[] arr_byte = new byte[19];
        byte[] Recive_Byte = new byte[18];
        byte recive_cnt = 0;

        static private Color[] colors = new Color[8];

        static SoundPlayer low_batt_sound = new SoundPlayer(System.Environment.CurrentDirectory + "\\Setting\\low_batt.wav");




        /***************************************************************************************/
        /* AMC Setting                                                                         */

        private byte[] DI_BOARD_DEV_NUM = { 1 };
        private byte[] DO_BOARD_DEV_NUM = { 3 };
        private byte[] AI_BOARD_DEV_NUM = { 5 };

        private const int OUT_NUM_CONVEYOR_RUN = 0;
        private const int OUT_NUM_CONVEYOR_DIRECTION = 1;
        private const int OUT_NUM_GRIPER_RUN = 2;
        private const int OUT_NUM_GRIPER_DIRECTION = 3;
        private const int OUT_NUM_LAMP_RED = 4;
        private const int OUT_NUM_LAMP_YELLOW = 5;
        private const int OUT_NUM_LAMP_GREEN = 6;


        //private const int OUT_NUM_PIO1 = 8;
        //private const int OUT_NUM_PIO2 = 9;
        //private const int OUT_NUM_PIO3 = 10;
        //private const int OUT_NUM_PIO4 = 11;
        //private const int OUT_NUM_PIO5 = 12;
        //private const int OUT_NUM_PIO6 = 13;
        //private const int OUT_NUM_PIO7 = 14;
        //private const int OUT_NUM_PIO8 = 15;

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


        private const int IN_NUM_START = 11;


        private bool[] In_Port_AB = new bool[32];
        //private const int IN_NUM_PIO1 = 8;
        //private const int IN_NUM_PIO2 = 9;
        //private const int IN_NUM_PIO3 = 10;
        //private const int IN_NUM_PIO4 = 11;
        //private const int IN_NUM_PIO5 = 12;
        //private const int IN_NUM_PIO6 = 13;
        //private const int IN_NUM_PIO7 = 14;
        //private const int IN_NUM_PIO8 = 15;

        private const double DMR362_150_WFOV = 106;
        private const double DMR362_150_HFOV = 91;
        private const double DMR362_250_WFOV = 171;
        private const double DMR362_250_HFOV = 106;

        private const double DMR362_150_WPIXEL = 0.082;
        private const double DMR362_150_HPIXEL = 0.088;
        private const double DMR362_250_WPIXEL = 0.133;
        private const double DMR362_250_HPIXEL = 0.103;

        private const double DMR362_WPIXEL_INC = 0.00051;   // 1mm 당 Pixel 넓이 증가량.
        private const double DMR362_HPIXEL_INC = 0.00015;   // 1mm 당 Pixel 높이 증가량.

        private const ushort DI_ADDRESS = 0x0080;
        private const ushort DO_ADDRESS = 0x0000;
        private const int AI_ADDRESS = 0x82;

        private const int LINEAR_DEV_NUM = 7;
        private const ushort LINEAR_AL_ADD = 128;
        private const ushort LINEAR_POS_ADD = 204;
        private const ushort LINEAR_AL_CLEAR_ADD = 384;
        private const ushort LINEAR_CENTER_POSITION = 1338;


        private const byte STOPPER1_MOTOR_NUM = 5;
        private const byte STOPPER2_MOTOR_NUM = 4;
        private const byte STOPPER3_MOTOR_NUM = 3;
        private const byte STOPPER4_MOTOR_NUM = 6;  // Manual Right
        private const byte STOPPER5_MOTOR_NUM = 7;  // Manual Left  
        private const byte STOPPER6_MOTOR_NUM = 8;
        private const byte STOPPER7_MOTOR_NUM = 9;

        private const int STOPPER1_ON_POS = 650;
        private const int STOPPER2_ON_POS = 400;
        private const int STOPPER3_ON_POS = 650;
        private const int STOPPER4_ON_POS = 800;
        private const int STOPPER5_ON_POS = 215;

        private const int STOPPER1_OFF_POS = 800;
        private const int STOPPER2_OFF_POS = 220;
        private const int STOPPER3_OFF_POS = 800;
        private const int STOPPER4_OFF_POS = 512;
        private const int STOPPER5_OFF_POST = 512;

        private const byte STOPPER_ALL = 0xFE;


        private const byte DD_NUM_MAN_STOPPER_R = 0;
        private const byte DD_NUM_MAN_STOPPER_L = 1;


        private int Err_chk_nRetry = 0;

        private const string Err_chk_sMOUTH = "MOUTH";
        private const string Err_chk_sCONVEYOR_OUT = "CONVEYOR_OUT";



        private const int Err_chk_nMOUTH = 30;
        private const int Err_chk_nCONVEYOR_OUT = 30;


        private const int Err_chk_Retry_MOUTH = 3;
        private const int Err_chk_Retry_CONVEYOR_OUT = 3;


        /***************************************************************************************/


        /***************************************************************************************/
        /* AMB Setting                                                                         */

        private const int AMB_OUT_NUM_UNLATCH = 0;
        private const int AMB_OUT_NUM_LATCH = 1;
        private const int AMB_OUT_NUM_SIDE_LASER_OUT = 2;
        private const int AMB_OUT_NUM_SIDE_LASER_IN = 3;
        private const int AMB_OUT_NUM_LATCH_LAMP = 4;
        private const int AMB_OUT_NUM_UMLATCH_LAMP = 5;
        private const int AMB_OUT_NUM_SIGNAL_LAMP_RED = 8;
        private const int AMB_OUT_NUM_SIGNAL_LAMP_YELLOW = 7;
        private const int AMB_OUT_NUM_SIGNAL_LAMP_GREEN = 6;

        private const int AMB_IN_NUM_CART_LATCH_SENSOR = 0;
        private const int AMB_IN_NUM_CART_HOME_SENSOR = 4;
        private const int AMB_IN_NUM_LOCKER_HOME_SENSOR = 5;
        private const int AMB_IN_NUM_LOCKER_LINIT_SENSOR = 6;
        private const int AMB_IN_NUM_LATCH_SWITCH = 7;
        private const int AMB_IN_NUM_UNLATCH_SWITCH = 8;

        /***************************************************************************************/

        public Form1()
        {
            InitializeComponent();
            Form1.CheckForIllegalCrossThreadCalls = false;
        }


        private void Port_init()
        {
            In_Port_AB[0] = false;  //A접
            In_Port_AB[1] = false;  //A접
            In_Port_AB[2] = false;  //A접
            In_Port_AB[3] = false;  //A접
            In_Port_AB[4] = false;  //A접
            In_Port_AB[5] = false;  //A접
            In_Port_AB[6] = false;  //A접
            In_Port_AB[7] = false;  //A접

            In_Port_AB[8] = false;  //A접
            In_Port_AB[9] = false;  //A접
            In_Port_AB[10] = true;  //B접
            In_Port_AB[11] = false;  //A접
            In_Port_AB[12] = false;  //B접
            In_Port_AB[13] = false;  //A접
            In_Port_AB[14] = false;  //A접
            In_Port_AB[15] = true;  //B접
        }
        private void modbus_start()
        {
            while (true)
            {
                try
                {
                    IO_SerialPort = new SerialPort("COM1", 57600);
                    //IO_SerialPort = new SerialPort(SerialPort.GetPortNames()[0], 57600);

                    if (IO_SerialPort.IsOpen == false)
                    {
                        IO_SerialPort.Open();
                    }

                    IO_modbusMaster = ModbusSerialMaster.CreateRtu(IO_SerialPort);
                    IO_modbusMaster.Transport.ReadTimeout = 1000;
                    IO_modbusMaster.Transport.WriteTimeout = 2000;
                    IO_modbusMaster.Transport.Retries = 3;

                    if (IO_SerialPort.IsOpen == true)
                    {
                        for (int i = 0; i < 14; i++)
                        {
                            IO_modbusMaster.WriteSingleCoil(3, (ushort)(DO_ADDRESS + i), false);
                            System.Threading.Thread.Sleep(10);
                        }



                        bMODBUS_T = true;
                        if (bg_modbus.IsBusy == false)
                        {
                            bg_modbus.RunWorkerAsync();
                        }
                        else
                        {
                            return;
                        }
                    }

                    return;
                }
                catch (UnauthorizedAccessException)
                {
                    System.Threading.Thread.Sleep(250);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }


        static public void DO_OFF()
        {
            for (int i = 0; i < 14; i++)
            {
                OUTPUT_DATA[0, i] = false;
            }
        }


        double befor_batt = 0;
        bool IS_DISPLAY = false;
        bool b_skynet_ready = false;
        DateTime Ready_CHK_TIMER;


        private void Insert_Listbox(int index, string data, Socket LD_Socket)
        {
            try
            {
                this.Invoke(new MethodInvoker(
                    delegate ()
                    {

                        string[] str_buf = new string[100];

                        IS_DISPLAY = true;

                        data = data.Replace('\n', '\0');
                        data = data.Trim('\0');
                        str_buf = data.Split('\r');

                        for (int i = 0; i < (str_buf.Length == 1 ? str_buf.Length : str_buf.Length - 1); i++)
                        {
                            //str_buf[i] = str_buf[i].Replace('\n', ' ');
                            str_buf[i] = str_buf[i].Trim('\0');

                            if (str_buf[i].Contains("ExtendedStatusForHumans"))
                            {
                                string[] str_temp = str_buf[i].Split(' ');

                                LD[0].LD_ST.LD_ST = str_temp[1] + " ";

                                if (str_buf[i].Contains("EStop"))
                                {
                                    Insert_ERR((int)ERR_Q.ERR_CODE.LD_ESTOP);
                                }
                            }
                            else if (str_buf[i].Contains("Status:"))
                            {
                                LD[0].LD_ST.LD_ST += str_buf[i].Remove(0, 8);

                                if (LD[0].LD_ST.LD_ST.Contains("Arrived"))
                                {
                                    Monitor.Stop_Sound();
                                    Monitor.end_blink();

                                    if (str_cmd_arr[0] == "GOGOAL")
                                    {
                                        if (LD[0].LD_ST.LD_ST.Contains(str_cmd_arr[1]))
                                        {
                                            nCmd_Step++;
                                            bCmd_run = true;
                                        }
                                    }

                                    Monitor.reSet_Start_AREA();

                                    Ready_CHK_TIMER = DateTime.Now;
                                    Form1.Set_Skynet_Status(Form1.nSKYNET.SM_RUN, "COMPLETE");
                                    b_skynet_ready = true;
                                }

                                if(((DateTime.Now - Ready_CHK_TIMER).TotalSeconds > 30) && b_skynet_ready == true)
                                {
                                    Form1.Set_Skynet_Status(Form1.nSKYNET.SM_RUN, "READY");
                                    b_skynet_ready = false;
                                }
                            }
                            else if (str_buf[i].Contains("StateOfCharge:"))
                            {
                                string[] str_temp = str_buf[i].Split(' ');

                                LD[0].LD_ST.LD_CHARGE = Math.Truncate(float.Parse(str_temp[1])).ToString();
                                HMI.Set_BATT(LD[0].LD_ST.LD_CHARGE);
                                Monitor.Set_BATT(LD[0].LD_ST.LD_CHARGE);


                                if (double.Parse(LD[0].LD_ST.LD_CHARGE) <= 20.0)
                                    if (befor_batt > 20.0 || befor_batt == 0)
                                    {
                                        low_batt_sound.PlayLooping();
                                        Insert_ERR((int)ERR_Q.ERR_CODE.LD_LOW_BATTERY);
                                        befor_batt = double.Parse(LD[0].LD_ST.LD_CHARGE);
                                    }
                            }
                            else if (str_buf[i].Contains("Location:"))
                            {
                                string[] str_temp = str_buf[i].Split(' ');

                                LD[0].LD_ST.LD_LOC.LD_X = int.Parse(str_temp[1]);
                                LD[0].LD_ST.LD_LOC.LD_Y = int.Parse(str_temp[2]);
                                LD[0].LD_ST.LD_LOC.LD_A = int.Parse(str_temp[3]);

                                Set_AREA();
                            }
                            else if (str_buf[i].Contains("Temperature:"))
                            {
                                string[] str_temp = str_buf[i].Split(' ');

                                LD[0].LD_ST.LD_TEMP = str_temp[1];
                            }
                            else if (str_buf[i].Contains("Goal:"))
                            {
                                string[] str_aa = str_buf[i].Split(' ');

                                LD[0].LD_GOAL.Add(str_aa[1]);
                            }
                            else if (str_buf[i].Contains("End of goals"))//  str_temp[0] == "End" && str_temp[1] == "of" && str_temp[2] == "goals")
                            {
                                Monitor.Goal_ADD(LD[0].LD_GOAL);
                            }
                            else if (str_buf[i].Contains("EStop relieved but motors still disabled"))
                            {
                                Insert_ERR((int)ERR_Q.ERR_CODE.LD_ESTOP);
                            }
                            else if(str_buf[i].Contains("Latch Fail"))
                            {
                                Insert_ERR((int)ERR_Q.ERR_CODE.AMB_LATCH_FAIL);
                            }
                            else if(str_buf[i].Contains("DOOR"))
                            {//DOOR NAME CMD
                                string[] door_str = str_buf[i].Split(' ');

                                for(int j = 0; j < Doors.Length; j++)
                                {
                                    if (door_str[1] == Doors[j].NAME)
                                    {
                                        if (door_str[2] == "ON")
                                            DOOR_Open(Doors[j].ID);
                                        else if (door_str[2] == "OFF")
                                            DOOR_Close(Doors[j].ID);
                                    }
                                }
                            }


                            if(str_buf[i].Contains("LocalizationScore"))
                            {
                                string[] loc_temp = str_buf[i].Split(':');

                                Monitor.Set_Local(Math.Truncate(float.Parse(loc_temp[1]) * 100).ToString());
                                LD[0].LD_ST.LD_Localizetion_Score = Math.Truncate(float.Parse(loc_temp[1]) * 100).ToString();

                                if (0.10f > float.Parse(loc_temp[1]))
                                {
                                    Insert_ERR((int)ERR_Q.ERR_CODE.LD_ROBOT_LOST);
                                }
                            }

                            if (str_buf[i].Contains("Robot lost"))
                            {
                                Insert_ERR((int)ERR_Q.ERR_CODE.LD_ROBOT_LOST);
                            }

                            if (str_buf[i].Contains("Failed going to goal"))
                            {
                                Insert_ERR((int)ERR_Q.ERR_CODE.AMC_MOVE_FAIL);
                            }


                            if (str_buf[i].Contains("Docked"))
                            {
                                if (LD[0].LD_ST.LD_CHARGE != null)
                                {   
                                    if (bg_CMD.IsBusy == false)
                                        Monitor.Stop_Sound();

                                    //if(int.Parse(LD[0].LD_ST.LD_CHARGE) > LD[0].BATT_HIGH_LIMIT && bBATT_CMD1 == false)
                                    //{

                                    //}
                                }

                                Form1.Set_Skynet_Status(Form1.nSKYNET.SM_RUN, "RECHARGE");
                                Skynet_MSG_Send();
                            }

                            if (str_buf[i].ToUpper().Contains("DOOR"))
                            {
                                for (int j = 0; j < Doors.Length; j++)
                                {
                                    if (str_buf[i].ToUpper().Contains("DOOR" + j.ToString()))
                                    {
                                        string[] str_temp = str_buf[i].Split(' ');
                                        if (str_temp[1].ToUpper() == "OPEN")
                                            DOOR_Open(Doors[j].ID);
                                        else if (str_temp[1].ToUpper() == "CLOSE")
                                            DOOR_Close(Doors[j].ID);
                                    }
                                }
                            }

                            if(str_buf[i].ToUpper().Contains("ALARM"))
                            {
                                ALARM_SRV.Send_arrived();
                            }
                            
                            
                            //if (str_buf[i].Contains("StateOfCharge:"))
                            //{
                            //    string[] str_Temp = str_buf[i].Split(' ');

                            //    for(int j =  0; j <  str_Temp.Length; j++)
                            //    {
                            //        if (str_Temp[j] == "Status:")
                            //        {
                            //            string[] str_temp = str_buf[i].Split(' ');
                            //            string st_Temp = "";
                            //            for (int m = 1; m < 10; m++)
                            //            {
                            //                if (str_temp[m].Contains("StateOfCharge"))
                            //                {
                            //                    LD[0].LD_ST.LD_ST = st_Temp;
                            //                    break;
                            //                }
                            //                else
                            //                {
                            //                    st_Temp +=  " " + str_temp[m];                                                 
                            //                }

                            //                HMI.Set_BATT(LD[0].LD_ST.LD_CHARGE);
                            //            }

                            //        }
                            //        else if (str_Temp[j] == "StateOfCharge:")
                            //        {
                            //            LD[0].LD_ST.LD_CHARGE = str_Temp[j + 1] + "%";                                            
                            //        }                                            
                            //        else if(str_Temp[j] == "Location:")
                            //        {
                            //            LD[0].LD_ST.LD_LOC.LD_X = int.Parse(str_Temp[j + 1]);
                            //            LD[0].LD_ST.LD_LOC.LD_Y = int.Parse(str_Temp[j + 2]);
                            //            LD[0].LD_ST.LD_LOC.LD_A = int.Parse(str_Temp[j + 3]);

                            //            Set_AREA();
                            //        }
                            //        else if(str_Temp[j] == "Temperature:")
                            //        {
                            //            LD[0].LD_ST.LD_TEMP = str_Temp[j + 1];
                            //        }
                            //    }                                    
                            //}

                            


                            if (bg_CMD.IsBusy == true)
                            {
                                if (str_buf[i].Contains("Arrived at") && str_cmd_arr.Length > 1 ? str_buf[i].Contains(str_cmd_arr[1]) : false)
                                {
                                    nCmd_Step++;
                                    bCmd_run = true;
                                    LD[0].LD_ST.LD_ST = "ARRIVED_" + str_cmd_arr[1];
                                    Send_AMC_MSG("SEND", STB_NAME, CMD_NAME, GOAL_NAME, LD[0].LD_ST.LD_ST);
                                    Err_chk_nRetry = 0;

                                    break;
                                }
                                else if (str_buf[i].Contains("Completed doing task wait"))
                                {
                                    //String[] str_temp = str_buf[i].Split(' ');

                                    //if(int.Parse(str_temp[4]) >= 100)
                                    //{
                                    //    //aa
                                    //}
                                    //else
                                    //{
                                    //    nCmd_Step++;
                                    //    bCmd_run = true;
                                    //}
                                    nCmd_Step++;
                                    bCmd_run = true;

                                    break;
                                }
                                else if (str_buf[i].Contains("Completed doing task deltaHeading") && str_cmd_arr.Length > 1 ? str_buf[i].Contains(str_cmd_arr[1]) : false)
                                {
                                    bCmd_run = true;

                                    break;
                                }
                                else if (str_buf[i].Contains("Completed doing task move") && str_cmd_arr.Length > 1 ? str_buf[i].Contains(str_cmd_arr[1]) : false)
                                {
                                    nCmd_Step++;
                                    bCmd_run = true;

                                    break;
                                }
                                else if (str_buf[i].Contains("Completed doing task setHeading") && (str_cmd_arr.Length > 1 ? str_buf[i].Contains(str_cmd_arr[1]) : false))
                                {
                                    nCmd_Step++;
                                    bCmd_run = true;

                                    break;
                                }
                                else if (str_buf[i].Contains("EStop"))
                                {
                                    bEStop = true;
                                    STOPPER.bEStop = true;
                                    Insert_ERR((int)ERR_Q.ERR_CODE.LD_ESTOP);

                                    bbg_CMD_terminator = false;
                                    lb_cmd.Items.Clear();

                                    break;
                                }
                                else if (str_buf[i].Contains("Motor enabled"))
                                {
                                    bEStop = false;
                                    STOPPER.bEStop = false;

                                    break;
                                }
                                else if (str_buf[i].Contains("AREA"))
                                {
                                    string[] str_aa = str_buf[i].Split(' ');

                                    if (str_aa[0] == "IN")
                                    {
                                        LD[0].LD_ST.LD_AREA = str_aa[1];
                                    }
                                    else
                                    {
                                        LD[0].LD_ST.LD_AREA = "";
                                    }
                                }
                                else if (str_buf[i].Contains("Failed going to goal") && bCmd_run == false)
                                {
                                    string[] str_aa = str_buf[i].Split(' ');

                                    bCmd_run = true;
                                    Err_chk_nRetry++;

                                    if (Err_chk_nRetry >= 3)
                                    {
                                        Insert_ERR((int)ERR_Q.ERR_CODE.AMC_MOVE_FAIL);
                                        Err_chk_nRetry = 0;
                                    }
                                }
                                else if (str_buf[i].Contains("Failed"))
                                {
                                    bCmd_run = true;
                                }


                            }

                            if (lb_log.Items[lb_log.Items.Count - 1].ToString() != str_buf[i])
                            {
                                string[] str_temp = new string[20];

                                str_temp = str_buf[i].Split(' ');

                                if (str_temp[0] == "Input:" || str_temp[0] == "Output:")
                                {                                    
                                    HMI.Set_io_lamp(str_temp[0], str_temp[1], str_temp[2]);

                                    if (str_temp[0] == "Input:")
                                    {
                                        LD_DI[1, int.Parse(str_temp[1].Substring(1, str_temp[1].Length - 1)) - 1] = LD_DI[0, int.Parse(str_temp[1].Substring(1, str_temp[1].Length - 1)) - 1];
                                        LD_DI[0, int.Parse(str_temp[1].Substring(1, str_temp[1].Length - 1)) - 1] = (str_temp[2] == "off" ? false : true);
                                    }
                                    else if (str_temp[0] == "Output:")
                                    {
                                        LD_DO[1, int.Parse(str_temp[1].Substring(1, str_temp[1].Length - 1)) - 1] = LD_DO[0, int.Parse(str_temp[1].Substring(1, str_temp[1].Length - 1)) - 1];
                                        LD_DO[0, int.Parse(str_temp[1].Substring(1, str_temp[1].Length - 1)) - 1] = (str_temp[2] == "off" ? false : true);
                                    }

                                    
                                    CHK_DO();

                                    IS_DISPLAY = false;


                                }
                                else if (str_temp[0] == "Enter" && str_temp[1] == "password:")
                                {
                                    for (int j = 0; j < LD.Length; j++)
                                    {
                                        if (LD[i].LD_Client.Connected == true)
                                        {
                                            byte[] _buf = new byte[10];
                                            byte[] buf = new byte[10];

                                            _buf = Encoding.Default.GetBytes(LD[0].LD_PW);

                                            Array.Resize(ref _buf, LD[0].LD_PW.Length + 2);

                                            _buf[LD[0].LD_PW.Length] = 0x0D;
                                            _buf[LD[0].LD_PW.Length + 1] = 0x0A;

                                            LD[i].LD_Client.Send(_buf, 0, 6, SocketFlags.None);

                                            if (bg_LD_IO_ST.IsBusy == false)
                                            {
                                                bg_LD_IO_ST_T = true;
                                                bg_LD_IO_ST.RunWorkerAsync();
                                            }
                                        }
                                    }

                                    //lb_log.Items.Insert(lb_log.Items.Count, str_buf[i]);
                                    lb_log.SetSelected(lb_log.Items.Count - 1, true);

                                    b_bg_terminator = true;
                                    return;
                                }
                                else if (str_temp[0] == "Welcome")
                                {
                                    //EMPTY
                                    bst_terminator = true;
                                    if (bg_st.IsBusy == false)
                                    {
                                        bg_st.RunWorkerAsync();
                                    }

                                    bRetry_T = true;
                                if (bg_retry.IsBusy == false)
                                        bg_retry.RunWorkerAsync();  
                                }
                                else if (str_temp[0] == "Arrived" && str_temp[1] == "at")
                                {
                                    if (LD[0].LD_Client == LD_Socket && str_temp[2] == (str_cmd_arr.Length > 2 ? str_cmd_arr[1] : "fasle"))
                                    {
                                        nCmd_Step++;
                                        bCmd_run = true;
                                        LD[0].LD_ST.LD_ST = "ARRIVED_" + str_temp[2];
                                        Send_AMC_MSG("SEND", STB_NAME, CMD_NAME, GOAL_NAME, LD[0].LD_ST.LD_ST);
                                    }
                                }
                                else if (str_temp[0] == "Completed" && str_temp[1] == "doing" && str_temp[2] == "task")
                                {
                                    if (str_temp[3] == "wait")
                                    {
                                        nCmd_Step++;
                                        bCmd_run = true;
                                    }
                                    else if (str_temp[3] == "deltaHeading")
                                    {
                                        //Send_string(LD[0].LD_Client, "doTask wait " + Now_Dev.force_Relese_Time);
                                        //nCmd_Step++;
                                        bCmd_run = true;
                                    }
                                    else if (str_temp[3] == "move")
                                    {
                                        //if(str_temp[4] == "1000" && str_temp[5] == "50" && str_temp[6] == "150"  && str_temp[7] == "50" && str_temp[8] == "30" && str_temp[9] == "0" && str_temp[10] == "0" && str_temp[11] == "0" && str_temp[12] == "True" && str_temp[13] == "True" && str_temp[14] == "True")
                                        if (str_temp[4] == str_cmd_arr[1])
                                        {
                                            nCmd_Step++;
                                            bCmd_run = true;
                                        }
                                    }
                                }
                                else if (str_temp[0] == "Info:")
                                {
                                    if (str_temp[1] == "WirelessQuality")
                                    {
                                        //Saver.Set_lm_wifi(int.Parse(str_temp[2].Substring(0, str_temp[2].Length - 1)));
                                        //tb_wifi.Text = str_temp[2].Substring(0, str_temp[2].Length - 1);
                                        LD[0].LD_ST.LD_WIFI = str_temp[2].Substring(0, str_temp[2].Length - 1);
                                        HMI.Set_WIFI(LD[0].LD_ST.LD_WIFI);
                                    }
                                    else if (str_temp[1] == "PackVoltage")
                                    {
                                        //Saver.Set_gg_Value(str_temp[2]);
                                        //tb_volt.Text = str_temp[2];
                                        LD[0].LD_ST.LD_VOLT = str_temp[2];
                                        HMI.Set_VOLT(LD[0].LD_ST.LD_VOLT);
                                    }
                                    else if (str_temp[1] == "Odometer(KM)")
                                    {
                                        //Saver.Set_om_value(str_temp[2]);
                                        //tb_odo.Text = str_temp[2];
                                        LD[0].LD_ST.LD_ODO = str_temp[2];
                                        HMI.Set_ODO(LD[0].LD_ST.LD_ODO);

                                    }
                                    else if (str_temp[1] == "CurrentDraw")
                                    {

                                    }
                                    return;
                                }
                                else if (str_temp[0] == "LDS1")
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

                                            tb_LDS1.Text = LD[0].LD_ST.LD_LDS1;
                                            tb_LDS2.Text = LD[0].LD_ST.LD_LDS2;
                                        }

                                        //lb_log.Items.Insert(lb_log.Items.Count, LD[0].LD_ST.LD_LDS1_DISTANCE + ", " + LD[0].LD_ST.LD_LDS2_DISTANCE + ", " + LD[0].LD_ST.LD_LDS_Angle);
                                        //lb_log.SetSelected(lb_log.Items.Count - 1, true);
                                    }
                                }
                                else if (str_temp[0] == "EStop" && str_temp[1] == "pressed")
                                {
                                    bEStop = true;
                                    STOPPER.bEStop = true;
                                    Insert_ERR((int)ERR_Q.ERR_CODE.LD_ESTOP);
                                    Send_AMC_MSG("SEND", STB_NAME, CMD_NAME, GOAL_NAME, "ESTOP");
                                }
                                else if (str_temp[0] == "Error:")
                                {
                                    if (str_temp[1] == " Failed" && str_temp[2] == "going" && str_temp[3] == "to" && str_temp[4] == "goal")
                                    {
                                        Err_chk_nRetry += 1;

                                        if (Err_chk_nRetry < 3)
                                        {
                                            Send_string(LD[0].LD_Client, "goto " + str_cmd_arr[1]);
                                            LD[0].LD_ST.LD_ST = "MOVE_GOAL_" + str_cmd_arr[1] + " " + Err_chk_nRetry.ToString();
                                        }
                                        else
                                        {
                                            Insert_ERR((int)ERR_Q.ERR_CODE.AMC_MOVE_FAIL);
                                        }
                                    }
                                }
                            }
                            else
                            {

                            }


                            if (IS_DISPLAY == true)
                            {


                                if (str_buf[i].Length > List_Box_Line_Conut)
                                {
                                    string[] str_line = new string[10];

                                    for (int j = 0; j < (str_buf[i].Length / List_Box_Line_Conut) + 1; j++)
                                    {
                                        str_line[j] = str_buf[i].Substring(j * List_Box_Line_Conut, str_buf[i].Length > (j * List_Box_Line_Conut) + List_Box_Line_Conut ? List_Box_Line_Conut : str_buf[i].Length - (j * List_Box_Line_Conut));
                                        lb_log.Items.Add(str_line[j]);
                                        lb_log.SetSelected(lb_log.Items.Count - 1, true);
                                    }
                                }
                                else
                                {
                                    if (lb_log != null)
                                    {
                                        lb_log.Items.Add(str_buf[i]);
                                        //lb_log.Items.Insert(lb_log.Items.Count, str_buf[i]);
                                        //lb_log.Refresh();

                                        if (lb_log.Items.Count - 5 <= lb_log.SelectedIndex)
                                        {
                                            lb_log.SetSelected(lb_log.Items.Count - 1, true);
                                        }
                                    }
                                }
                                
                                    Insert_Log(str_buf[i]);
                            }
                            else
                            {
                                IS_DISPLAY = true;
                            }
                        }
                    }
                    )
                );
            }
            catch (Exception ex)
            {
                Insert_ERR_Log(ex);
                //Make_error(ex, "Insert_Listbox");
            }
        }


        private string Get_Area_Action(string area)
        {
            for(int i  = 0; i < AREAs.Count; i++)
            {
                if (AREAs[i].AREA_NAME == area)
                    return AREAs[i].Action;
            }

            return "";
        }

        string temp_area = "";

        private void Set_AREA()
        {

            temp_area = LD[0].LD_ST.LD_AREA;

            for (int i = 0; i < AREAs.Count; i++)
            {
                if ((AREAs[i].P1.X > AREAs[i].P2.X ? AREAs[i].P1.X : AREAs[i].P2.X) > LD[0].LD_ST.LD_LOC.LD_X && (AREAs[i].P1.X < AREAs[i].P2.X ? AREAs[i].P1.X : AREAs[i].P2.X) < LD[0].LD_ST.LD_LOC.LD_X)
                {
                    if ((AREAs[i].P1.Y > AREAs[i].P2.Y ? AREAs[i].P1.Y : AREAs[i].P2.Y) > LD[0].LD_ST.LD_LOC.LD_Y && (AREAs[i].P1.Y < AREAs[i].P2.Y ? AREAs[i].P1.Y : AREAs[i].P2.Y) < LD[0].LD_ST.LD_LOC.LD_Y)
                    {
                        LD[0].LD_ST.LD_AREA = AREAs[i].AREA_NAME;



                        if (temp_area != AREAs[i].AREA_NAME)
                        {
                            if (AREAs[i].Action != null)
                            {
                                string[] action_temp = AREAs[i].Action.Split('|');
                                if (action_temp[1] == "ON" || action_temp[1] == "OPEN")
                                {
                                    DOOR_Open(action_temp[0]);
                                }
                                else if (action_temp[1] == "OFF" || action_temp[1] == "CLOSE")
                                {
                                    DOOR_Close(action_temp[0]);
                                }
                            }

                            if (Get_Area_Action(temp_area) != null)
                            {
                                string[] baction_temp = Get_Area_Action(temp_area).Split('|');
                                if (baction_temp[2] == "ON" || baction_temp[2] == "OPEN")
                                {
                                    DOOR_Open(baction_temp[0]);
                                }
                                else if (baction_temp[2] == "OFF" || baction_temp[2] == "CLOSE")
                                {
                                    DOOR_Close(baction_temp[0]);
                                }
                            }
                        }

                        break;
                    }
                    else
                    {
                        LD[0].LD_ST.LD_AREA = "";
                    }
                }
                else
                {
                    LD[0].LD_ST.LD_AREA = "";
                }
            }

            Monitor.Set_AREA(LD[0].LD_ST.LD_AREA);

            

            //if (temp_area != "" && LD[0].LD_ST.LD_AREA == "")
            //{
            //    string capcode = Find_Capcode_By_Goalname(Monitor.Get_where2go());

                //    if (temp_area != LD[0].LD_ST.LD_AREA)
                //    {
                //        if (LD[0].LD_ST.LD_AREA.ToUpper().Contains("DOC") == false)
                //        {

                //        }
                //    }


                //    if (capcode != "")
                //    {
                //        //Send_AMC_MSG("SEND", "NONE", "PAGER", capcode, " [AMC] " + Monitor.Get_where2go() + "로 출발 합니다.");
                //    }
                //}
                //else if (temp_area == "" && LD[0].LD_ST.LD_AREA != "")
                //{
                //    string capcode = Find_Capcode_By_Goalname(LD[0].LD_ST.LD_AREA);
                //    if (capcode != "")
                //    {
                //        if (LD[0].LD_ST.LD_AREA != Monitor.Get_where2go())
                //        {
                //            //Send_AMC_MSG("SEND", "NONE", "PAGER", capcode, "[AMC] " + LD[0].LD_ST.LD_AREA + "에 도착 했습니다.");
                //        }
                //    }
                //}
        }

        static int Keepday = 30;

        private void Insert_Log(string msg)
        {
            string date = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:FFF"); ;
            string str_temp = "";
            string log_dir = System.Environment.CurrentDirectory + "\\Log\\" + System.DateTime.Now.ToString("yyyy/MM/dd") + " Log.txt";

            try
            {
                if (System.IO.File.Exists(log_dir) == false)
                {
                    Directory.CreateDirectory(System.Environment.CurrentDirectory + "\\Log\\");
                    string temp;

                    temp = "========================================================" + Environment.NewLine;
                    temp += "=                                                                                                            =" + Environment.NewLine;
                    temp += "=                                      AMC Log File " + System.DateTime.Now.ToString("yyyy/MM/dd") + "                                    =" + Environment.NewLine;
                    temp += "=                                                                                                            =" + Environment.NewLine;
                    temp += "========================================================" + Environment.NewLine;

                    System.IO.File.WriteAllText(log_dir, temp);
                    Check_Log_date(System.Environment.CurrentDirectory + "\\Log\\", Keepday);
                }

                //str_buf = System.IO.File.ReadAllText(log_dir);



                string[] arr_str = msg.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                System.IO.StreamWriter st = System.IO.File.AppendText(log_dir);

                for (int i = 0; i < arr_str.Length; i++)
                {
                    if (arr_str[i].Trim('\0') != "")
                    {
                        if (!(arr_str[i].Contains("Output:") || arr_str[i].Contains("Input:")))
                        {
                            str_temp = date + " " + arr_str[i];
                            st.WriteLine(str_temp);
                        }
                    }
                }

                st.Close();
                st.Dispose();

                
            }
            catch (Exception ex)
            {
                Insert_ERR_Log(ex);
                //Insert_Log(msg);
            }
        }

        static private void Check_Log_date(string log_dir, int keep_day)
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
                Insert_ERR_Log(ex);
            }
        }

        private void btn_GoGoal_Click(object sender, EventArgs e)
        {
            string str_val = "Goal Name";

            DialogResult res = InputBox("Go Goal Input Box", "Input Goal Name", ref str_val);

            if (res == DialogResult.OK)
            {
                if (bis_insert == true)
                {
                    lb_cmd.Items.Insert(lb_cmd.SelectedIndex == -1 ? 0 : lb_cmd.SelectedIndex, "GoGoal " + str_val);
                    lb_cmd.SetSelected((lb_cmd.SelectedIndex == -1 ? 0 : lb_cmd.SelectedIndex) - 1, true);

                    bis_insert = false;
                }
                else
                {
                    lb_cmd.Items.Insert(lb_cmd.Items.Count, "GoGoal " + str_val);
                    lb_cmd.SetSelected(lb_cmd.Items.Count - 1, true);
                }
            }
        }

        private void btn_move_Click(object sender, EventArgs e)
        {
            string str_val = "Move Param";

            DialogResult res = InputBox("Move Input Box", "Input Move Param", ref str_val);

            if (res == DialogResult.OK)
            {
                if (bis_insert == true)
                {
                    lb_cmd.Items.Insert(lb_cmd.SelectedIndex == -1 ? 0 : lb_cmd.SelectedIndex, "Move " + str_val);
                    lb_cmd.SetSelected((lb_cmd.SelectedIndex == -1 ? 0 : lb_cmd.SelectedIndex), true);

                    bis_insert = false;
                }
                else
                {
                    lb_cmd.Items.Insert(lb_cmd.Items.Count, "Move " + str_val);
                    lb_cmd.SetSelected(lb_cmd.Items.Count - 1, true);
                }
            }
        }

        private void btn_heading_Click(object sender, EventArgs e)
        {
            if (bis_insert == true)
            {
                lb_cmd.Items.Insert(lb_cmd.SelectedIndex == -1 ? 0 : lb_cmd.SelectedIndex, "DeltaHeading");
                lb_cmd.SetSelected((lb_cmd.SelectedIndex == -1 ? 0 : lb_cmd.SelectedIndex), true);

                bis_insert = false;
            }
            else
            {
                lb_cmd.Items.Insert(lb_cmd.Items.Count, "DeltaHeading");
                lb_cmd.SetSelected(lb_cmd.Items.Count - 1, true);
            }
        }

        private void btn_cmd_st_Click(object sender, EventArgs e)
        {
            if (bis_insert == true)
            {
                lb_cmd.Items.Insert(lb_cmd.SelectedIndex == -1 ? 0 : lb_cmd.SelectedIndex, "START");
                lb_cmd.SetSelected((lb_cmd.SelectedIndex == -1 ? 0 : lb_cmd.SelectedIndex), true);
                bis_insert = false;
            }
            else
            {
                lb_cmd.Items.Insert(lb_cmd.Items.Count, "START");
                lb_cmd.SetSelected(lb_cmd.Items.Count - 1, true);
            }

        }

        private void btn_end_Click(object sender, EventArgs e)
        {
            if (bis_insert == true)
            {
                lb_cmd.Items.Insert(lb_cmd.SelectedIndex == -1 ? 0 : lb_cmd.SelectedIndex, "END");
                lb_cmd.SetSelected((lb_cmd.SelectedIndex == -1 ? 0 : lb_cmd.SelectedIndex), true);
            }
            else
            {
                lb_cmd.Items.Insert(lb_cmd.Items.Count, "END");
                lb_cmd.SetSelected(lb_cmd.Items.Count - 1, true);
            }
        }

        private void btn_log_Click(object sender, EventArgs e)
        {
            if (bis_insert == true)
            {
                lb_cmd.Items.Insert(lb_cmd.SelectedIndex == -1 ? 0 : lb_cmd.SelectedIndex, "Log");
                lb_cmd.SetSelected((lb_cmd.SelectedIndex == -1 ? 0 : lb_cmd.SelectedIndex), true);
            }
            else
            {
                lb_cmd.Items.Insert(lb_cmd.Items.Count, "Log");
                lb_cmd.SetSelected(lb_cmd.Items.Count - 1, true);
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


        public static DialogResult InputCOMBO(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            ComboBox combo = new ComboBox();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 150, 20);
            combo.SetBounds(180, 36, 150, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            combo.Items.AddRange(new object[] { colors[0].ToKnownColor() , colors[1].ToKnownColor(), colors[2].ToKnownColor(), colors[3].ToKnownColor(), colors[4].ToKnownColor(), colors[5].ToKnownColor(), colors[6].ToKnownColor(), colors[7].ToKnownColor() });


            combo.SelectedValueChanged += new EventHandler(COMBO_EVENT);
            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            combo.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel, combo });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text + "," + combo.Text;
            return dialogResult;
        }

        static private void COMBO_EVENT(object sender, EventArgs e)
        {
            ComboBox com = (ComboBox)sender;

            com.BackColor = Color.FromName( com.Items[com.SelectedIndex].ToString());            
        }


        int DSP_cnt = -1;
        
        private void bg_CMD_DoWork(object sender, DoWorkEventArgs e)
        {
            string str_cmd_temp = "";

            // System.Threading.Thread.Sleep(3000);


            while (bbg_CMD_terminator)
            {
                try
                {


                    if (nCmd_Step == lb_cmd.Items.Count)
                    {
                        bbg_CMD_terminator = false;
                        return;
                    }

                    if (ERR_QUEUE.MAX_ERR_LV() > 1)
                    {
                        bbg_CMD_terminator = false;
                        return;
                    }


                    lb_cmd.SelectedIndex = nCmd_Step;
                    nud_retry.Value = nroof_cnt + 1;

                    if (DSP_cnt != nCmd_Step)
                    {
                        LD[0].LD_ST.LD_ST = lb_cmd.Items[nCmd_Step].ToString();
                        DSP_cnt = nCmd_Step;
                    }


                    str_cmd_temp = lb_cmd.Items[nCmd_Step].ToString();
                    str_cmd_arr = str_cmd_temp.Split(' ');
                    str_cmd_arr[0] = str_cmd_arr[0].ToUpper();


                    if (bCmd_run == true || str_cmd_arr[0] == "DELTAHEADING")
                    {
                        if (str_cmd_arr[0] == "GOGOAL")
                        {
                            if (cb_sim.Checked == false)
                            {
                                if (LD[0].LD_DISTANCE < 300)
                                {
                                    Send_string(LD[0].LD_Client, "doTask move -100 50 0 0 30 0 0 0 True True False");
                                }
                                {
                                    Send_string(LD[0].LD_Client, "goto " + str_cmd_arr[1]);
                                    LD[0].LD_ST.LD_ST = "MOVE_GOAL_" + str_cmd_arr[1];
                                    //AMC_JOB.Set_JOB_ST("GOGOAL");

                                    string capcode = Form1.Find_Capcode_By_Goalname(Monitor.Get_where2go());

                                    if (capcode != "")
                                    {
                                        //Form1.Send_AMC_MSG("SEND", "NONE", "PAGER", capcode, " [AMC] " + Monitor.Get_where2go() + "로 출발 합니다.");
                                    }

                                    Send_AMC_MSG("SEND", STB_NAME, CMD_NAME, GOAL_NAME, LD[0].LD_ST.LD_ST);
                                    bCmd_run = false;
                                }

                            }
                            else
                            {
                                LD[0].LD_ST.LD_ST = "MOVE_GOAL_" + str_cmd_arr[1];
                                Send_AMC_MSG("SEND", STB_NAME, CMD_NAME, GOAL_NAME, LD[0].LD_ST.LD_ST);


                                System.Threading.Thread.Sleep(3000);
                                Insert_Listbox(0, "ARRIVED_" + str_cmd_arr[1], LD[0].LD_Client);
                                nCmd_Step++;
                            }

                        }
                        else if (str_cmd_arr[0] == "MOVE")
                        {
                            if (cb_sim.Checked == false)
                            {
                                if (int.Parse(str_cmd_arr[1]) < 0)
                                {
                                    Send_string(LD[0].LD_Client, "doTask move " + str_cmd_arr[1] + " 50 0 0 30 0 0 0 True True False");
                                }
                                else
                                {
                                    Send_string(LD[0].LD_Client, "doTask move " + str_cmd_arr[1] + " 50 50 50 30 0 0 0 True True False");
                                }


                                LD[0].LD_ST.LD_ST = "MOVE_" + str_cmd_arr[1];
                                //AMC_JOB.Set_JOB_ST("MOVE+" + str_cmd_arr[1]);
                                Send_AMC_MSG("SEND", STB_NAME, CMD_NAME, GOAL_NAME, LD[0].LD_ST.LD_ST);
                                bCmd_run = false;
                            }
                            else
                            {
                                LD[0].LD_ST.LD_ST = "MOVE_" + str_cmd_arr[1];
                                //AMC_JOB.Set_JOB_ST("MOVE+" + str_cmd_arr[1]);
                                bCmd_run = false;

                                System.Threading.Thread.Sleep(3000);
                                Insert_Listbox(0, "Completed doing task move" + str_cmd_arr[1], LD[0].LD_Client);
                                nCmd_Step++;
                            }
                        }
                        else if (str_cmd_arr[0] == "DELTAHEADING")
                        {
                            System.Threading.Thread.Sleep(900);
                            if (cb_sim.Checked == false)
                            {
                                if (Math.Abs(LD[0].LD_ST.LD_LDS_Angle) == 0)
                                {
                                    LD[0].LD_ST.LD_ST = "DELTAHEADING_END";
                                    Send_AMC_MSG("SEND", STB_NAME, CMD_NAME, GOAL_NAME, LD[0].LD_ST.LD_ST);
                                    bCmd_run = true;
                                    nCmd_Step++;
                                }
                                else
                                {
                                    if (int.Parse(LD[0].LD_ST.LD_LDS1) >= 58980 || int.Parse(LD[0].LD_ST.LD_LDS2) > 58980)
                                    {
                                        Insert_ERR((int)ERR_Q.ERR_CODE.AMC_DELTHAHEADING_FAIL);
                                    }
                                    else
                                    {
                                        LD[0].LD_ST.LD_ST = "DELTAHEADING";
                                        //AMC_JOB.Set_JOB_ST("DELTAHEADING");
                                        Send_AMC_MSG("SEND", STB_NAME, CMD_NAME, GOAL_NAME, LD[0].LD_ST.LD_ST);
                                        Send_Delta_Heading();
                                        bCmd_run = false;
                                    }

                                }
                            }
                            else
                            {
                                System.Threading.Thread.Sleep(3000);

                                LD[0].LD_ST.LD_ST = "DELTAHEADING_END";
                                Send_AMC_MSG("SEND", STB_NAME, CMD_NAME, GOAL_NAME, LD[0].LD_ST.LD_ST);
                                nCmd_Step++;
                            }
                        }
                        else if (str_cmd_arr[0] == "START")
                        {
                            Monitor.Playing_Sound();

                            if (Tray_Move_Back() == true)
                            {
                                nCmd_Step++;
                                AMC_JOB.Set_JOB_ST("JOB_START");
                                LD[0].LD_ST.LD_ST = "TASK_START";
                                bCmd_run = true;
                                Conveyor_step = 0;
                                Conveyor_STOP();
                                //Monitor.Playing_Sound();
                            }

                        }
                        else if (str_cmd_arr[0] == "END")
                        {
                            //if (++nroof_cnt >= nroof_max)
                            //{
                                LD[0].LD_ST.LD_ST = "TASK_END";
                                //AMC_JOB.Set_JOB_ST("JOB_TASK_END");
                                Send_AMC_MSG("SEND", STB_NAME, CMD_NAME, GOAL_NAME, "TASK_END");
                                //MOD_Direct_MOVE(LINEAR_CENTER_POSITION);

                                LD[0].LD_ST.LD_ST = "IDLE";

                                nCmd_Step = 0;
                                bCmd_run = false;
                                bbg_CMD_terminator = false;
                                b_bg_terminator = true;
                                b_bg_LDS_terminator = false;
                                lb_cmd.Items.Clear();
                                Conveyor_MODE = "";

                                string capcode = Form1.Find_Capcode_By_Goalname(Monitor.Get_where2go());

                                if (capcode != "")
                                {
                                    //Form1.Send_AMC_MSG("SEND", "NONE", "PAGER", capcode, " [AMC] " + Monitor.Get_where2go() + "에 도착 했습니다.");
                                }

                                //Monitor.Stop_Sound();

                                //btn_start_Click(sender, e);
                            //}
                            //else
                            //{
                            //    LD[0].LD_ST.LD_ST = "TASK_END";
                            //    //AMC_JOB.Set_JOB_ST("JOB_TASK_END");
                            //    Send_AMC_MSG("SEND", STB_NAME, CMD_NAME, GOAL_NAME, "TASK_END");
                            //    nCmd_Step = 0;
                            //    bCmd_run = true;
                            //}
                        }
                        else if (str_cmd_arr[0] == "LOG")
                        {
                            string msg = "";
                            double LDS1, LDS2, va; ;

                            double.TryParse(LD[0].LD_ST.LD_LDS1_DISTANCE, out LDS1);
                            double.TryParse(LD[0].LD_ST.LD_LDS2_DISTANCE, out LDS2);
                            va = LDS1 - LDS2;

                            msg = "LDS1" + '\t' + LD[0].LD_ST.LD_LDS1_DISTANCE + '\t' + "LDS2" + '\t' + LD[0].LD_ST.LD_LDS2_DISTANCE + '\t' + "LDS1-LDS2" + '\t' + va.ToString() + '\t' + "Angle" + '\t' + LD[0].LD_ST.LD_LDS_Angle + '\t' + "Location X" + '\t' + LD[0].LD_ST.LD_LOC.LD_X + '\t' + "Location Y" + '\t' + LD[0].LD_ST.LD_LOC.LD_Y + Environment.NewLine;

                            Insert_Log(msg);
                            nCmd_Step++;
                        }
                        else if (str_cmd_arr[0] == "CORRECTION")
                        {
                            if (cb_sim.Checked == false)
                            {
                                stDMR_POSITION LOC = new stDMR_POSITION();
                                LD[0].LD_ST.LD_ST = "CORRECTION";
                                //AMC_JOB.Set_JOB_ST("CORRECTION");
                                LOC = Read_QR();

                                double distance = (double.Parse(LD[0].LD_ST.LD_LDS1_DISTANCE) + double.Parse(LD[0].LD_ST.LD_LDS2_DISTANCE)) / 2;

                                if (LD[0].LD_ST.LD_LDS_Angle > 1)
                                {
                                    nCmd_Step = Find_DeltaHeading();
                                }
                                else if (distance > 255)
                                {
                                    Send_string(LD[0].LD_Client, "doTask move " + ((int)(distance - 255)).ToString() + " 50 30 50 30 0 0 0 True True False");
                                    System.Threading.Thread.Sleep(5000);
                                }
                                else
                                {
                                    if (LD[0].LD_ST.LD_LDS_Angle != 0)
                                    {
                                        nCmd_Step = Find_DeltaHeading();
                                    }
                                    else
                                    {
                                        int res = (int)(Loc2mmX(LOC) * 100);

                                        Insert_Listbox(0, "RES = " + res.ToString(), LD[0].LD_Client);

                                        if (LINEAR_AL != 0)
                                        {
                                            MOD_ERR_CLEAR();
                                        }

                                        //MOD_Direct_MOVE(res + LINEAR_CENTER_POSITION);

                                        if (LINEAR_AL == 0 && LINEAR_POS <= (res + 10) + LINEAR_CENTER_POSITION && LINEAR_POS >= (res - 10) + LINEAR_CENTER_POSITION)
                                        {
                                            LD[0].LD_ST.LD_ST = "CORRECTION_END";
                                            Send_AMC_MSG("RECV", STB_NAME, CMD_NAME, GOAL_NAME, "CORRECTION_END");
                                            bCmd_run = true;
                                            nCmd_Step++;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                System.Threading.Thread.Sleep(3000);
                                LD[0].LD_ST.LD_ST = "CORRECTION_END";
                                Send_AMC_MSG("RECV", STB_NAME, CMD_NAME, GOAL_NAME, "CORRECTION_END");
                                nCmd_Step++;
                            }
                        }
                        else if (str_cmd_arr[0] == "ON_PULSE_OUT")
                        {
                            DO_OUT(ushort.Parse((int.Parse(str_cmd_arr[1]) % 16).ToString()), true);

                            System.Threading.Thread.Sleep(int.Parse(str_cmd_arr[2]));

                            DO_OUT(ushort.Parse((int.Parse(str_cmd_arr[1]) % 16).ToString()), false);

                            nCmd_Step++;
                        }
                        else if (str_cmd_arr[0] == "CHECK_IO")
                        {
                            if (INPUT_DATA[0, int.Parse(str_cmd_arr[1])] == true)
                            {
                                nCmd_Step++;
                            }
                        }
                        else if (str_cmd_arr[0] == "CONVEYOR_IN")
                        {
                            if (Wait_Run_Signal())
                            {

                                Conveyor_MODE = "AUTO_IN";
                                LD[0].LD_ST.LD_ST = "CONVEYOR_IN";

                                Start_bg_Conveyor();
                            }
                            else
                            {
                                Send_AMC_MSG("SEND", STB_NAME, CMD_NAME, GOAL_NAME, "SIGNAL_WAIT");
                                System.Threading.Thread.Sleep(3000);
                            }
                        }
                        else if (str_cmd_arr[0] == "CONVEYOR_OUT")
                        {
                            if (Wait_Run_Signal())
                            {
                                Conveyor_MODE = "AUTO_OUT";
                                LD[0].LD_ST.LD_ST = "CONVEYOR_OUT";
                                Start_bg_Conveyor();
                            }
                        }
                        else if (str_cmd_arr[0] == "WAIT")
                        {
                            if (cb_sim.Checked == false)
                            {
                                Send_string(LD[0].LD_Client, "doTask wait " + str_cmd_arr[1]);
                                LD[0].LD_ST.LD_ST = "WAIT" + str_cmd_arr[1];
                                bCmd_run = false;
                            }
                            else
                            {
                                Insert_log("WAIT" + str_cmd_arr[1]);
                                nCmd_Step++;
                            }
                        }
                        else if ((str_cmd_arr[0] == "AWAIT"))
                        {
                            if (cb_sim.Checked == false)
                            {
                                Send_string(LD[0].LD_Client, "doTask wait " + str_cmd_arr[1]);
                                LD[0].LD_ST.LD_ST = "WAIT" + str_cmd_arr[1];
                                bCmd_run = true;
                                nCmd_Step++;
                            }
                            else
                            {
                                Insert_log("AWAIT" + str_cmd_arr[1]);
                                nCmd_Step++;
                            }
                        }
                        else if (str_cmd_arr[0] == "CHECK_MOUTH")
                        {
                            if (cb_sim.Checked == false)
                            {
                                if (INPUT_DATA[0, IN_NUM_MOUTH_SENSOR] && bCONECTED == true)
                                {
                                    bCONECTED = false;
                                    Send_AMC_MSG("RECV", STB_NAME, CMD_NAME, GOAL_NAME, "CONNECTED_OK");
                                    nCmd_Step++;
                                }
                                else
                                {
                                    Send_AMC_MSG("RECV", STB_NAME, CMD_NAME, GOAL_NAME, "NOT_CONNECTED");
                                }
                            }
                            else
                            {
                                if (INPUT_DATA[0, IN_NUM_MOUTH_SENSOR])
                                {
                                    Send_AMC_MSG("RECV", STB_NAME, CMD_NAME, GOAL_NAME, "CONNECTED_OK");
                                    nCmd_Step++;
                                }
                            }
                        }
                        else if (str_cmd_arr[0] == "PAGER")
                        {
                            string str = "";
                            for (int j = 2; j < str_cmd_arr.Length; j++)
                            {
                                str += str_cmd_arr[j];
                            }

                            //Send_AMC_MSG("SEND", "NONE", "PAGER", str_cmd_arr[1], str);
                            nCmd_Step++;
                        }
                        else if (str_cmd_arr[0] == "DOCK")
                        {
                            Send_string(LD[0].LD_Client, "dock " + str_cmd_arr[1]);
                        }
                        else if (str_cmd_arr[0] == "SETHEADING")
                        {
                            Send_string(LD[0].LD_Client, "doTask setHeading " + str_cmd_arr[1] + " 0 10 0 0 5 0");
                            bCmd_run = false;
                        }
                        else if (str_cmd_arr[0] == "AIR_IN_ON")
                        {
                            AIRSHOWER_IN_Open();
                            bCmd_run = false;
                        }
                        else if (str_cmd_arr[0] == "AIR_IN_OFF")
                        {
                            AIRSHOWER_IN_Close();
                            bCmd_run = false;
                        }
                        else if (str_cmd_arr[0] == "AIR_OUT_ON")
                        {
                            AIRSHOWER_OUT_Open();
                            bCmd_run = false;
                        }
                        else if (str_cmd_arr[0] == "AIR_OUT_OFF")
                        {
                            AIRSHOWER_OUT_Close();
                            bCmd_run = false;
                        }
                        else if (str_cmd_arr[0] == "DOOR_ON")
                        {
                            DOOR_Open(str_cmd_arr[1]);
                            bCmd_run = false;
                        }
                        else if (str_cmd_arr[0] == "DOOR_OFF")
                        {
                            DOOR_Close(str_cmd_arr[1]);
                            bCmd_run = false;
                        }
                        else if (str_cmd_arr[0] == "MAN_OUT")
                        {
                            bCmd_run = false;
                        }
                        else if (str_cmd_arr[0] == "MAN_IN")
                        {
                            bCmd_run = false;
                        }

                    }
                    else
                    {

                    }
                    System.Threading.Thread.Sleep(100);

                }
                catch (Exception)
                {
                }
            }
        }

        private int Find_DeltaHeading()
        {
            for (int i = 0; i < lb_cmd.Items.Count; i++)
            {
                if (lb_cmd.Items[i].ToString() == "DELTAHEADING")
                {
                    return i;
                }
            }
            return -1;
        }

        DateTime move_back_t;
        private bool Tray_Move_Back()
        {
            if (OUTPUT_DATA[0, OUT_NUM_CONVEYOR_RUN] == false)
            {
                move_back_t = DateTime.Now;
                Conveyor_CW_RUN();

            }

            if ((DateTime.Now - move_back_t).TotalMilliseconds >= 2000)
            {
                Conveyor_STOP();
                return true;
            }

            //if (INPUT_DATA[0, IN_NUM_TRAY_SENSOR1] == true && INPUT_DATA[0, IN_NUM_TRAY_SENSOR1_1] == false)
            //{
            //    STOPPER.MOTOR_ON(STOPPER1_MOTOR_NUM);
            //}

            //if (INPUT_DATA[0, IN_NUM_TRAY_SENSOR1] == true && INPUT_DATA[0, IN_NUM_TRAY_SENSOR2] == true && INPUT_DATA[0, IN_NUM_TRAY_SENSOR2_1] == false)
            //{
            //    STOPPER.MOTOR_ON(STOPPER2_MOTOR_NUM);
            //}

            //if (INPUT_DATA[0, IN_NUM_TRAY_SENSOR1] == true && INPUT_DATA[0, IN_NUM_TRAY_SENSOR2] == true && INPUT_DATA[0, IN_NUM_TRAY_SENSOR3] == true && INPUT_DATA[0, IN_NUM_TRAY_SENSOR3_1] == false)
            //{
            //    STOPPER.MOTOR_ON(STOPPER3_MOTOR_NUM);
            //    return true;
            //}

            //if (INPUT_DATA[0, IN_NUM_TRAY_SENSOR1_1] == false && INPUT_DATA[0, IN_NUM_TRAY_SENSOR2_1] == false && INPUT_DATA[0, IN_NUM_TRAY_SENSOR3_1] == false && INPUT_DATA[0, IN_NUM_TRAY_SENSOR4_1] == false)
            //{
            //    Conveyor_STOP();
            //    return true;
            //}
            //else if ((INPUT_DATA[0, IN_NUM_TRAY_SENSOR1] == false && INPUT_DATA[0, IN_NUM_TRAY_SENSOR1_1] == true) || (INPUT_DATA[0, IN_NUM_TRAY_SENSOR2] == false && INPUT_DATA[0, IN_NUM_TRAY_SENSOR2_1] == true) ||
            //    (INPUT_DATA[0, IN_NUM_TRAY_SENSOR1] == false && INPUT_DATA[0, IN_NUM_TRAY_SENSOR1] == true) || (INPUT_DATA[0, IN_NUM_TRAY_SENSOR1] == false && INPUT_DATA[0, IN_NUM_TRAY_SENSOR1] == true))
            //{
            //    Conveyor_CW_RUN();
            //}

            return false;
        }

        private void Set_Off_DO()
        {
            if (ERR_QUEUE.MAX_ERR_LV() > 0)
            {
                for (int i = 0; i < 8; i++)
                {
                    OUTPUT_DATA[0, i] = false;
                }
            }
        }


        bool err_st = false;

        private void Set_LAMP()
        {
            if (ERR_QUEUE.Is_EMPTY() == false)
            {
                Set_On_RED_LAMP();
                Set_Off_YELLOW_LAMP();
                Set_Off_GREEN_LAMP();

                err_st = false;
            }
            else if(ERR_QUEUE.Is_EMPTY() == true)
            {
                Set_On_GREEN_LAMP();
                Set_Off_RED_LAMP();
                Set_Off_YELLOW_LAMP();
                err_st = true;                
            }
        }


        private void Set_On_RED_LAMP()
        {
            if (LD[0].LD_TYPE == "AMC")
            {
                OUTPUT_DATA[0, OUT_NUM_LAMP_RED] = true;
            }
            else if (LD[0].LD_TYPE == "AMB" || LD[0].LD_TYPE == "MRBT")
            {
                LD_DIGITAL_OUT[AMB_OUT_NUM_SIGNAL_LAMP_RED] = true;
                LD_DIGITAL_OUT[AMB_OUT_NUM_SIGNAL_LAMP_YELLOW] = false;
                LD_DIGITAL_OUT[AMB_OUT_NUM_SIGNAL_LAMP_GREEN] = false;
            }
        }

        private void Set_Off_RED_LAMP()
        {
            if(LD[0].LD_TYPE == "AMC")
            {
                OUTPUT_DATA[0, OUT_NUM_LAMP_RED] = false;
            }
            else if(LD[0].LD_TYPE == "AMB" || LD[0].LD_TYPE == "MRBT")
            {
                LD_DIGITAL_OUT[AMB_OUT_NUM_SIGNAL_LAMP_RED] = false;
            }
        }

        private void Set_On_YELLOW_LAMP()
        {
            if(LD[0].LD_TYPE =="AMC")
            {
                OUTPUT_DATA[0, OUT_NUM_LAMP_YELLOW] = true;
            }
            else if(LD[0].LD_TYPE =="AMB" || LD[0].LD_TYPE == "MRBT")
            {
                LD_DIGITAL_OUT[AMB_OUT_NUM_SIGNAL_LAMP_RED] = false;
                LD_DIGITAL_OUT[AMB_OUT_NUM_SIGNAL_LAMP_YELLOW] = true;
                LD_DIGITAL_OUT[AMB_OUT_NUM_SIGNAL_LAMP_GREEN] = false;
            }
        }

        private void Set_Off_YELLOW_LAMP()
        {
            if (LD[0].LD_TYPE == "AMC")
            {
                OUTPUT_DATA[0, OUT_NUM_LAMP_YELLOW] = false;
            }
            else if (LD[0].LD_TYPE == "AMB" || LD[0].LD_TYPE == "MRBT")
            {
                LD_DIGITAL_OUT[AMB_OUT_NUM_SIGNAL_LAMP_YELLOW] = false;
            }
        }

        private void Set_On_GREEN_LAMP()
        {
            if (LD[0].LD_TYPE == "AMC")
            {
                OUTPUT_DATA[0, OUT_NUM_LAMP_GREEN] = true;
            }
            else if (LD[0].LD_TYPE == "AMB" || LD[0].LD_TYPE == "MRBT")
            {
                LD_DIGITAL_OUT[AMB_OUT_NUM_SIGNAL_LAMP_RED] = false;
                LD_DIGITAL_OUT[AMB_OUT_NUM_SIGNAL_LAMP_YELLOW] = false;
                LD_DIGITAL_OUT[AMB_OUT_NUM_SIGNAL_LAMP_GREEN] = true;
            }
        }

        private void Set_Off_GREEN_LAMP()
        {
            if(LD[0].LD_TYPE == "AMC")
            {
                OUTPUT_DATA[0, OUT_NUM_LAMP_GREEN] = false;
            }
            else if( LD[0].LD_TYPE == "AMB" || LD[0].LD_TYPE == "MRBT")
            {
                LD_DIGITAL_OUT[AMB_OUT_NUM_SIGNAL_LAMP_GREEN] = false;
            }            
        }

        private double Loc2mmX(stDMR_POSITION LOC)
        {
            double res = 0.0;

            int result = LOC.X - 640;


            //res = double.Parse(LD[0].LD_ST.LD_LDS1_DISTANCE) +
            ////    double.Parse(LD[0].LD_ST.LD_LDS2_DISTANCE);

            //res = res / 2;//
            //Insert_Listbox(0, "Distance : " + res.ToString(), LD[0].LD_Client);
            //res -= 212;
            //res = res * 0.55 + 150;//(DMR362_WPIXEL_INC) + DMR362_150_WPIXEL;
            //Insert_Listbox(0, "FOV : " + res.ToString(), LD[0].LD_Client);
            //res = res / 1280;
            //Insert_Listbox(0, "PIXEL WIDTH: " + res.ToString(), LD[0].LD_Client);
            //Insert_Listbox(0, "PIXEL VAL : " + LOC.X.ToString() + " - 640 = " + result.ToString(), LD[0].LD_Client);
            res = 0.13929688 * result;
            Insert_Listbox(0, "PIXEL2MM : " + res.ToString(), LD[0].LD_Client);

            return res;
        }

        private stDMR_POSITION Read_QR()
        {
            stDMR_POSITION LOC = new stDMR_POSITION();

            DMR.Trigger();

            System.Threading.Thread.Sleep(1000);

            string[] result = DMR.Read_Result().Split(',');

            if (result[0] != "")
            {
                LOC.X = int.Parse(result[1]);
                LOC.Y = int.Parse(result[2]);
            }

            return LOC;
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            if (btn_start.Text == "START")
            {
                if (cb_sim.Checked == false)
                {
                    //b_bg_LDS_terminator = true;
                    b_bg_terminator = true;
                    //bMODBUS_T = true;
                    //bst_terminator = true;

                    //STOPPER.Set_Port_NAME("COM2");
                    //STOPPER.Set_BAUDRATE(57600);
                    //STOPPER.OPEN_COM();
                    //STOPPER.motot_init();


                    //if (bg_LDS.IsBusy == false)
                    //    bg_LDS.RunWorkerAsync();
                    if (bg_reseve.IsBusy == false)
                    {
                        bg_reseve.RunWorkerAsync();
                    }

                    bSkynet_connected = skynet.Skynet_Connect(Skynet_Param.IP);


                    //if (bg_modbus.IsBusy == false)
                    //{
                    //    modbus_start();
                    //}

                    //if (bg_st.IsBusy == false)
                    //{
                    //    bg_st.RunWorkerAsync();
                    //}
                    if (bSkynet_connected == true)
                    {
                        nSkynet_Start = skynet.Skynet_PM_Start(Skynet_Param.LINE_CODE, Skynet_Param.PROCEESS_CODE, Skynet_Param.EQUIPMENT_ID);
                        Skynet_Param.STATUS_CODE = nSKYNET.SM_IDLE;
                        Skynet_MSG_Send();                        
                    }

                    Insert_System_Log("START button click");
                }
                else
                {
                    STOPPER.Set_Port_NAME(SerialPort.GetPortNames()[1]);
                    STOPPER.Set_BAUDRATE(57600);
                    STOPPER.OPEN_COM();

                    STOPPER.MOTOR_TORQUE_ON(STOPPER1_MOTOR_NUM);
                    STOPPER.MOTOR_TORQUE_ON(STOPPER2_MOTOR_NUM);
                    STOPPER.MOTOR_TORQUE_ON(STOPPER3_MOTOR_NUM);

                    Insert_System_Log("STOP button clink");

                }

                bbg_CMD_terminator = true;
                bCmd_run = true;
                bDISPLAY_T = true;

                nroof_max = (int)nud_retry.Value;

                if (bg_CMD.IsBusy == false)
                {
                    bg_CMD.RunWorkerAsync();
                }

                if (bg_Display.IsBusy == false)
                {
                    bg_Display.RunWorkerAsync();
                }

                if (LD[0].LD_Client.Connected == false)
                {
                    LD[0].LD_Client.Connect(new IPEndPoint(LD[0].LD_IP, LD[0].LD_PORT));

                    //LD[0].LD_Client.BeginConnect(new IPEndPoint(LD[0].LD_IP, 7171), new AsyncCallback(ConnectCallback), LD[0].LD_Client);
                }

                btn_start.Text = "STOP";
            }
            else if (btn_start.Text == "STOP")
            {
                bbg_CMD_terminator = false;
                b_bg_LDS_terminator = false;
                b_bg_terminator = false;
                bCmd_run = false;
                bst_terminator = false;
                nCmd_Step = 0;

                bg_CMD.CancelAsync();

                bg_reseve.CancelAsync();
                bg_st.CancelAsync();
                btn_start.Text = "START";
            }
        }

        private void AMC_Client_START()
        {
            retry_cnt = 0;
            try
            {
                if (AMC_Client != null)
                {
                    if (AMC_Client.IsConnected == false)
                    {

                        AMC_Client.Connect();
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "")
                {
                    bRetry_T = true;
                    bg_reseve.RunWorkerAsync();
                }
                else if ( ex.Message == "The socket is connecting, cannot connect again!")
                {
                    AMC_Client.Close();
                }

                Insert_ERR_Log(ex);

            }

        }

        private void AMC_Client_Connected(object sender, EventArgs e)
        {
            //Login();
            retry_cnt = 0;
            bRetry_T = false;
        }

        private void AMC_Client_Closed(object sender, EventArgs e)
        {
            if (AMC_Client != null)
            {
                if (bg_retry.IsBusy == false && AMC_Client.IsConnected == false)
                {
                    bRetry_T = true;
                    bg_retry.RunWorkerAsync();
                }
            }
        }

        DateTime AMC_ReciveTime = DateTime.Now;
        private void AMC_Client_DataReceived(object sender, DataEventArgs e)
        {
            try
            {
                if (AMC_Client != null)
                {
                    if (true == AMC_Client.IsConnected)
                    {
                        AMC_ReciveTime = DateTime.Now;
                        //정상데이터
                        //원본 데이터
                        dataOriginal insData = new dataOriginal();
                        insData.Length = e.Length;
                        //데이터를 오프셋을 기준으로 자른다.
                        Buffer.BlockCopy(e.Data, e.Offset, insData.Data, 0, insData.Length);



                        AMC_SRV_Parse(Encoding.UTF8.GetString(insData.Data));



                        //오리지널 데이터를 분석합니다.
                        //claSendData_Analysis insSD_A = new claSendData_Analysis(insData);
                        //MsgAnalysis(insSD_A);
                    }
                }
            }
            catch (Exception ex)
            {
                Insert_ERR_Log(ex);

            }
        }

        private void AMC_Client_Error(object sender, SuperSocket.ClientEngine.ErrorEventArgs e)
        {

        }


        /// <summary>
        /// AMC message Send
        /// </summary>
        /// <param name="MODE"></param>
        /// <param name="STB_NAME"></param>
        /// <param name="CMD_NAME"></param>
        /// <param name="GOAL_NAME"></param>
        /// <param name="ST"></param>
        static public void Send_AMC_MSG(string MODE, string STB_NAME, string CMD_NAME, string GOAL_NAME, string ST)
        {
            string _buf = string.Format("{0},AMC={1},STB={2},CMD={3},GOAL={4},STATUS={5};", MODE, LD[0].LD_NAME, STB_NAME, CMD_NAME, GOAL_NAME, ST);

            //lb_log.Items.Add("SEND: " + _buf);
            Send_string(_buf);

            Insert_System_Log(_buf);
        }


        static public void Send_LD_AMC_MSG(string MODE, string STB_NAME, string mCMD_NAME, string GOAL_NAME, string ST)
        {
            
            string _buf = string.Format("{0},AMC={1},STB={2},CMD={3},GOAL={4},STATUS={5};", MODE, LD[0].LD_NAME, STB_NAME, mCMD_NAME == "" ? CMD_NAME : mCMD_NAME, GOAL_NAME, ST);

            //lb_log.Items.Add("SEND: " + _buf);
            Send_string(_buf);
            Insert_System_Log(_buf);
        }

        static public void CMD_STEP_INC()
        {
            nCmd_Step++;
        }


        static private void Send_string(string msg)
        {
            byte[] byteSend = Encoding.UTF8.GetBytes(msg);

            try
            {
                if (AMC_Client != null)
                {
                    if (AMC_Client.IsConnected == true)
                    {
                        AMC_Client.Send(byteSend, 0, byteSend.Length);
                    }
                }
            }
            catch (Exception ex)
            {
                Insert_ERR_Log(ex);
                throw;
            }
            
        }

        static public void Send_LD_String(string msg)
        {
            try
            {
                byte[] _buf = new byte[1024];
                byte[] buf = new byte[1024];

                Byte[] _data = new Byte[10240];

                //_buf = Encoding.Default.GetBytes(msg);
                _buf = Encoding.GetEncoding("ksc_5601").GetBytes(msg);


                Array.Resize(ref _buf, msg.Length + 2);
                _buf[msg.Length] = 0x0D;
                _buf[msg.Length + 1] = 0x0A;

                if (LD[0].LD_Client.Connected == true)
                {
                    LD[0].LD_Client.Send(_buf);
                }
            }
            catch (Exception ex)
            {
                Insert_ERR_Log(ex);
                // Make_error(ex, "Send_string");
            }
        }

        public void Send_string(Socket ld_socket, string data)
        {
            try
            {
                byte[] _buf = new byte[1024];
                byte[] buf = new byte[1024];

                Byte[] _data = new Byte[10240];

                //_buf = Encoding.Default.GetBytes(data);
                _buf = Encoding.GetEncoding("ksc_5601").GetBytes(data);

                Array.Resize(ref _buf, data.Length + 2);
                _buf[data.Length] = 0x0D;
                _buf[data.Length + 1] = 0x0A;

                if (ld_socket.Connected == true)
                {
                    ld_socket.Send(_buf);
                }
            }
            catch (Exception)
            {
                // Make_error(ex, "Send_string");
            }
        }

        private void Get_LDS_Distance()
        {
            if (int.Parse(LD[0].LD_ST.LD_LDS1) > LDS_Min_Val && int.Parse(LD[0].LD_ST.LD_LDS1) < LDS_Max_Val)
            {
                LD[0].LD_ST.LD_LDS1_DISTANCE = (int.Parse(LD[0].LD_ST.LD_LDS1) / LDS_1mm_Val).ToString();
            }
            else
            {
                LD[0].LD_ST.LD_LDS1_DISTANCE = "0";
            }

            if (int.Parse(LD[0].LD_ST.LD_LDS2) > LDS_Min_Val && int.Parse(LD[0].LD_ST.LD_LDS2) < LDS_Max_Val)
            {
                LD[0].LD_ST.LD_LDS2_DISTANCE = (int.Parse(LD[0].LD_ST.LD_LDS2) / LDS_1mm_Val).ToString();
            }
            else
            {
                LD[0].LD_ST.LD_LDS2_DISTANCE = "0";
            }

            if (int.Parse(LD[0].LD_ST.LD_LDS1) > LDS_Min_Val && int.Parse(LD[0].LD_ST.LD_LDS1) < LDS_Max_Val && int.Parse(LD[0].LD_ST.LD_LDS2) > LDS_Min_Val && int.Parse(LD[0].LD_ST.LD_LDS2) < LDS_Max_Val)
            {
                Get_LDS_Angle();
            }
        }


        private void Get_LDS_Angle()
        {
            double d_diff;

            LD[0].LD_ST.LD_LDS_DIS_DIFF = double.Parse(LD[0].LD_ST.LD_LDS1_DISTANCE) - double.Parse(LD[0].LD_ST.LD_LDS2_DISTANCE);

            d_diff = LD[0].LD_ST.LD_LDS_DIS_DIFF < 0 ? LD[0].LD_ST.LD_LDS_DIS_DIFF * -1 : LD[0].LD_ST.LD_LDS_DIS_DIFF;

            if (d_diff <= 10)
            {
                LD[0].LD_ST.LD_LDS_Angle = 0;

                //Insert_Listbox(0, LD[0].LD_ST.LD_LDS1_DISTANCE.ToString() + " - " + LD[0].LD_ST.LD_LDS2_DISTANCE.ToString() + " = " + LD[0].LD_ST.LD_LDS_DIS_DIFF.ToString(), LD[0].LD_Client);
            }
            else if (d_diff <= 20)
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
            else if (d_diff <= 50)
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
            else if (d_diff <= 100)
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
            else if (d_diff > 100)
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

        private int Send_Delta_Heading()
        {
            int a = 0;

            if (LD[0].LD_ST.LD_LDS1_DISTANCE == "0" && LD[0].LD_ST.LD_LDS2_DISTANCE == "0")
            {
                ndeltaHeading_Task_cnt++;
                return 0;
            }

            if (Math.Abs(LD[0].LD_ST.LD_LDS_Angle) == 0)
            {
                a = 0;
                ndeltaHeading_Task_cnt++;
            }
            else if (Math.Abs(LD[0].LD_ST.LD_LDS_Angle) >= 10)
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

            Send_string(LD[0].LD_Client, "doTask deltaHeading " + ((LD[0].LD_ST.LD_LDS_Angle > 0) ? a.ToString() : (a * -1).ToString()));



            return (int)LD[0].LD_ST.LD_LDS_Angle;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            string log_dir = System.Environment.CurrentDirectory + "\\Setting\\CMD.txt";

            if (System.IO.File.Exists(log_dir) == false)
            {
                Directory.CreateDirectory(System.Environment.CurrentDirectory + "\\Setting\\");
                string[] arr_str = lb_cmd.Items.OfType<string>().ToArray();

                System.IO.File.WriteAllLines(log_dir, arr_str);
            }
            else
            {
                string[] arr_str = lb_cmd.Items.OfType<string>().ToArray();

                System.IO.File.WriteAllLines(log_dir, arr_str);
            }
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            string log_dir = System.Environment.CurrentDirectory + "\\Setting\\CMD.txt";
            string[] cmd_temp = System.IO.File.ReadAllLines(log_dir);

            lb_cmd.Items.Clear();
            lb_cmd.Items.AddRange(cmd_temp);
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {
            colors[0] = Color.Blue;
            colors[1] = Color.Red;
            colors[2] = Color.Yellow;
            colors[3] = Color.Green;
            colors[4] = Color.SkyBlue;
            colors[5] = Color.Orange;
            colors[6] = Color.LightGoldenrodYellow;
            colors[7] = Color.DarkSeaGreen;

            LD[0].LD_Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            LD[0].LD_GOAL = new List<string>();
            LD[0].LD_ST.LD_ST = "IDLE";
            //string[] ports = SerialPort.GetPortNames();
            INPUT_DATA = new bool[2, 16 * DI_BOARD_DEV_NUM.Length];
            OUTPUT_DATA = new bool[2, 16 * DO_BOARD_DEV_NUM.Length];

            Monitor.add_alarm_event += Monitor_add_alarm_event;   

            Read_Setting_Text();
            Read_AREA();
            Read_TAG_Setting();
            Read_Skynet();
            ERR_QUEUE.Read_Err_list();

            if(AMC_SRV.SRV_IP == "NONE")
            {
                AMC_Client = null;
            }
            else
            {
                IPEndPoint ipServer = new IPEndPoint(IPAddress.Parse(AMC_SRV.SRV_IP)
                                                , Convert.ToInt32(AMC_SRV.SRV_PORT));
                EndPoint epTemp = ipServer as EndPoint;

                AMC_Client = new AsyncTcpSession(epTemp);

                AMC_Client.Connected += AMC_Client_Connected;
                AMC_Client.Closed += AMC_Client_Closed;
                AMC_Client.DataReceived += AMC_Client_DataReceived;
                AMC_Client.Error += AMC_Client_Error;
                AMC_Client.SendingQueueSize = 0xffff;
            }

                Zigbee.PortName = LD[0].ZIGBEE_PORT;
                Zigbee.BaudRate = 9600;
                Zigbee.Parity = Parity.None;
                Chk_Serial_Port();

            Port_init();
            HMI.Show();
            HMI.Hide();
            DMR.Show();
            DMR.Hide();
            this.Hide();
            Monitor.Show();
            //this.Visible = false;            
            btn_start_Click(sender, e);
            //frm_RFID.Show();
            //frm_RFID.Hide();
            //cb_com.Items.AddRange(ports);
            //modbus_start();

            Insert_System_Log("Application Run");

            LD[0].AUTO = true;

            int a = 0;
            a = skynet.Skynet_SM_Send_Run(Skynet_Param.LINE_CODE, Skynet_Param.PROCEESS_CODE, Skynet_Param.EQUIPMENT_ID, "RECHARGE", LD[0].LD_ST.LD_CHARGE, LD[0].LD_ST.LD_AREA);
            Write_Skynet_Log("Status_code : " + "CHARGE" + ", Line_code : " + Skynet_Param.LINE_CODE + ", Process_code : " + Skynet_Param.PROCEESS_CODE + ", Equipment_ID : " + Skynet_Param.EQUIPMENT_ID + ", Status : " + Skynet_Param.STATUS + ", Res : " + a);
            Skynet_MSG_Send();
        }

        private void Monitor_add_alarm_event(int alarm_code)
        {
            ERR_QUEUE.Insert_ERR(alarm_code);
        }

        private void bg_reseve_DoWork(object sender, DoWorkEventArgs e)
        {
            //System.Threading.Thread.Sleep(1000);
            Byte[] _data = new Byte[200];
            String _buf;

            while (b_bg_terminator)
            {
                try
                {
                    for (int i = 0; i < LD.Length; i++)
                    {
                        if (LD[0].LD_Client.Connected == true)
                        {
                            LD[0].LD_Client.Receive(_data);
                            _buf = Encoding.Default.GetString(_data);
                            Insert_Listbox(0, _buf, LD[0].LD_Client);
                            Insert_Hex_Log(_buf);
                            _data = new byte[10240];
                            _buf = "";
                        }
                        else
                        {

                            //LD[0].LD_Client.BeginConnect(new IPEndPoint(IPAddress.Parse("192.168.0.10"), 7171));
                            
                        }

                        if(bg_st.IsBusy == false)
                        {
                            Insert_ERR_Log("bg_st Restart");                            
                            bst_terminator = true;
                            //bg_st.RunWorkerAsync();
                        }

                        System.Threading.Thread.Sleep(10);
                    }
                }
                catch (Exception ex)
                {
                    Insert_ERR_Log(ex);
                }
            }
        }



        private void Insert_ERR(int ERR_CODE)
        {
            Insert_System_Log("Error : " + ERR_CODE);
            Set_On_RED_LAMP();
            ERR_QUEUE.Insert_ERR(ERR_CODE);
        }


        public static string Last_MSG;
        public static void Insert_System_Log(string msg)
        {
            if (Last_MSG == msg)
            {
                return;
            }

            string date = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:FFF"); ;
            string str_temp = "";
            string log_dir = System.Environment.CurrentDirectory + "\\Log\\System\\" + System.DateTime.Now.ToString("yyyy/MM/dd") + "_Log.txt";
            



            try
            {
                if (System.IO.File.Exists(log_dir) == false)
                {
                    Directory.CreateDirectory(System.Environment.CurrentDirectory + "\\Log\\System\\");
                    string temp;

                    temp = "========================================================" + Environment.NewLine;
                    temp += "=                                                                                                            =" + Environment.NewLine;
                    temp += "=                                   AMC SYSTEM Log File " + System.DateTime.Now.ToString("yyyy/MM/dd") + "                                 =" + Environment.NewLine;
                    temp += "=                                                                                                            =" + Environment.NewLine;
                    temp += "========================================================" + Environment.NewLine;

                    System.IO.File.WriteAllText(log_dir, temp);

                    string[] arr_str = msg.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    System.IO.StreamWriter st = System.IO.File.AppendText(log_dir);

                    for (int i = 0; i < arr_str.Length; i++)
                    {
                        if (arr_str[i].Trim('\0') != "")
                        {
                            str_temp = date + " " + arr_str[i];
                            st.WriteLine(str_temp.ToCharArray());
                        }
                    }

                    Check_Log_date(System.Environment.CurrentDirectory + "\\Log\\System\\", 30);
                }
                else
                {
                    string[] arr_str = msg.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    System.IO.StreamWriter st = System.IO.File.AppendText(log_dir);

                    for (int i = 0; i < arr_str.Length; i++)
                    {
                        if (arr_str[i].Trim('\0') != "")
                        {
                            str_temp = date + " " + arr_str[i];
                            st.WriteLine(str_temp.ToCharArray());
                        }
                    }
                    st.Close();
                    st.Dispose();
                }

                Last_MSG = msg;
                

            }
            catch (Exception)
            {
                System.Threading.Thread.Sleep(10);
            }
        }

        public static void Insert_ERR_Log(Exception ex)
        {
            if (Last_MSG == ex.Message)
            {
                return;
            }

            string msg = ex.Message;

            string date = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:FFF"); ;
            string str_temp = "";
            string log_dir = System.Environment.CurrentDirectory + "\\Log\\Error\\Err" + System.DateTime.Now.ToString("yyyy/MM/dd") + "_Log.txt";



            try
            {
                if (System.IO.File.Exists(log_dir) == false)
                {
                    Directory.CreateDirectory(System.Environment.CurrentDirectory + "\\Log\\Error\\");
                    string temp;

                    temp = "========================================================" + Environment.NewLine;
                    temp += "=                                                                                                            =" + Environment.NewLine;
                    temp += "=                                   AMC SYSTEM Log File " + System.DateTime.Now.ToString("yyyy/MM/dd") + "                                 =" + Environment.NewLine;
                    temp += "=                                                                                                            =" + Environment.NewLine;
                    temp += "========================================================" + Environment.NewLine;

                    System.IO.File.WriteAllText(log_dir, temp);

                    string[] arr_str = msg.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    System.IO.StreamWriter st = System.IO.File.AppendText(log_dir);

                    for (int i = 0; i < arr_str.Length; i++)
                    {
                        if (arr_str[i].Trim('\0') != "")
                        {
                            str_temp = date + " " + arr_str[i];
                            st.WriteLine(str_temp.ToCharArray());
                        }
                    }

                    Check_Log_date(System.Environment.CurrentDirectory + "\\Log\\System\\Error\\", 30);
                }
                else
                {
                    string[] arr_str = msg.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    System.IO.StreamWriter st = System.IO.File.AppendText(log_dir);

                    for (int i = 0; i < arr_str.Length; i++)
                    {
                        if (arr_str[i].Trim('\0') != "")
                        {
                            str_temp = date + " " + arr_str[i] + Environment.NewLine;
                            str_temp += ex.Source;
                            st.WriteLine(str_temp.ToCharArray());
                        }
                    }
                    st.Close();
                    st.Dispose();
                }

                Last_MSG = msg;


            }
            catch (Exception)
            {
                System.Threading.Thread.Sleep(10);
            }
        }


        public static void Insert_ERR_Log(string msg)
        {
            if (Last_MSG == msg)
            {
                return;
            }

            string date = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:FFF"); ;
            string str_temp = "";
            string log_dir = System.Environment.CurrentDirectory + "\\Log\\Error\\Err" + System.DateTime.Now.ToString("yyyy/MM/dd") + "_Log.txt";



            try
            {
                if (System.IO.File.Exists(log_dir) == false)
                {
                    Directory.CreateDirectory(System.Environment.CurrentDirectory + "\\Log\\Error\\");
                    string temp;

                    temp = "========================================================" + Environment.NewLine;
                    temp += "=                                                                                                            =" + Environment.NewLine;
                    temp += "=                                   AMC SYSTEM Log File " + System.DateTime.Now.ToString("yyyy/MM/dd") + "                                 =" + Environment.NewLine;
                    temp += "=                                                                                                            =" + Environment.NewLine;
                    temp += "========================================================" + Environment.NewLine;

                    System.IO.File.WriteAllText(log_dir, temp);

                    string[] arr_str = msg.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    System.IO.StreamWriter st = System.IO.File.AppendText(log_dir);

                    for (int i = 0; i < arr_str.Length; i++)
                    {
                        if (arr_str[i].Trim('\0') != "")
                        {
                            str_temp = date + " " + arr_str[i];
                            st.WriteLine(str_temp.ToCharArray());
                        }
                    }

                    Check_Log_date(System.Environment.CurrentDirectory + "\\Log\\System\\Error\\", 30);
                }
                else
                {
                    string[] arr_str = msg.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    System.IO.StreamWriter st = System.IO.File.AppendText(log_dir);

                    for (int i = 0; i < arr_str.Length; i++)
                    {
                        if (arr_str[i].Trim('\0') != "")
                        {
                            str_temp = date + " " + arr_str[i] + Environment.NewLine;
                            st.WriteLine(str_temp.ToCharArray());
                        }
                    }
                    st.Close();
                    st.Dispose();
                }

                Last_MSG = msg;


            }
            catch (Exception)
            {
                System.Threading.Thread.Sleep(10);
            }
        }


        private void Insert_Hex_Log(string msg)
        {
            string date = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:FFF"); ;
            string str_temp = "";
            string log_dir = System.Environment.CurrentDirectory + "\\Log\\" + System.DateTime.Now.ToString("yyyy/MM/dd") + " Log_H.txt";

            try
            {
                if (System.IO.File.Exists(log_dir) == false)
                {
                    Directory.CreateDirectory(System.Environment.CurrentDirectory + "\\Log\\");
                    string temp;

                    temp = "========================================================" + Environment.NewLine;
                    temp += "=                                                                                                            =" + Environment.NewLine;
                    temp += "=                                      AMC Log File " + System.DateTime.Now.ToString("yyyy/MM/dd") + "                                    =" + Environment.NewLine;
                    temp += "=                                                                                                            =" + Environment.NewLine;
                    temp += "========================================================" + Environment.NewLine;

                    System.IO.File.WriteAllText(log_dir, temp);

                    string[] arr_str = msg.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    System.IO.StreamWriter st = System.IO.File.AppendText(log_dir);

                    for (int i = 0; i < arr_str.Length; i++)
                    {
                        if (arr_str[i].Trim('\0') != "")
                        {
                            str_temp = date + " " + arr_str[i];
                            st.WriteLine(str_temp.ToCharArray());
                        }
                    }
                }
                else
                {
                    string[] arr_str = msg.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    System.IO.StreamWriter st = System.IO.File.AppendText(log_dir);

                    for (int i = 0; i < arr_str.Length; i++)
                    {
                        if (arr_str[i].Trim('\0') != "")
                        {
                            str_temp = date + " " + arr_str[i];
                            st.WriteLine(str_temp.ToCharArray());
                        }
                    }
                    st.Close();
                    st.Dispose();
                }
            }
            catch (Exception)
            {
                System.Threading.Thread.Sleep(100);
            }
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            lb_cmd.Items.RemoveAt(lb_cmd.SelectedIndex);
        }

        private void lb_cmd_Click(object sender, EventArgs e)
        {
            bis_insert = true;
        }


        bool is_1st = false;
        bool heartbeat = false;

        private void bg_st_DoWork(object sender, DoWorkEventArgs e)
        {
            //System.Threading.Thread.Sleep(1000);

            Send_string(LD[0].LD_Client, "getGoals");
            System.Threading.Thread.Sleep(1000);
            bool Server_Disable = false;
            int Server_Disable_cnt = 0;


            while (bst_terminator)
            {
                try
                {
                    if (LD[0].LD_Client.Connected == true)
                    {
                        if (LD[0].LD_GOAL.Count != 0)
                        {
                            if (LD[0].LD_GOAL != null)
                            {
                                AMC_Client_START();
                            }
                        }

                        //if (LD[0].LD_GOAL != null && AMC_Client.IsConnected == false)
                        //{
                        //    AMC_Client_START();
                        //}

                        Send_string(LD[0].LD_Client, "status");
                        //Send_string(LD[0].LD_Client, "oneLineStatus");
                        Send_string(LD[0].LD_Client, "getInfo WirelessQuality");
                        Send_string(LD[0].LD_Client, "getInfo CurrentDraw");
                        Send_string(LD[0].LD_Client, "getInfo PackVoltage");
                        Send_string(LD[0].LD_Client, "getInfo Odometer(KM)");


                        if ((DateTime.Now.Second % 3) == 0)
                        {
                            Send_AMC_MSG("SEND", STB_NAME, "STATUS", GOAL_NAME, LD[0].LD_ST.LD_ST);

                            Process[] app = Process.GetProcessesByName("AMC_Server");
                            if (app.Length < 1)
                            {
                                //Process ps = new Process();
                                //ProcessStartInfo info = new ProcessStartInfo("D:\\Amkor\\AMC_Server\\bin\\Debug\\AMC_Server.exe");
                                //info.UseShellExecute = false;
                                //info.RedirectStandardOutput = true;
                                //info.WindowStyle = ProcessWindowStyle.Hidden;
                                //ps.StartInfo = info;
                                //ps.Start();
                            }
                            else
                            {
                                Insert_System_Log("AMC Server 실행 중.");
                            }

                            if((DateTime.Now - AMC_ReciveTime).TotalSeconds >= 30 && Server_Disable == false)
                            {
                                Insert_ERR_Log("AMC Server 응답 없습니다.");

                                if (++Server_Disable_cnt >= 100)
                                    Server_Disable = true;
                               


                                //Process[] process_list = Process.GetProcessesByName("AMC_Server");

                                //if(process_list.Length > 0)
                                //{
                                //    //process_list[0].Kill();

                                //}
                                //else
                                //{
                                //    Insert_ERR_Log("AMC Server가 없습니다.");
                                //}
                            }
                        }
                        else if ((DateTime.Now.Second % 3) == 1)
                        {
                            Send_AMC_MSG("SEND", STB_NAME, "AREA", GOAL_NAME, LD[0].LD_ST.LD_AREA);
                            Send_AMC_MSG("SEND", STB_NAME, "LOC", GOAL_NAME, LD[0].LD_ST.LD_LOC.LD_X.ToString() + "|" + LD[0].LD_ST.LD_LOC.LD_Y.ToString() + "|" + LD[0].LD_ST.LD_LOC.LD_A.ToString() + "|" + LD[0].LD_ST.LD_CHARGE);

                            if (bSkynet_connected == false)
                                bSkynet_connected = skynet.Skynet_Connect(Skynet_Param.IP);
                            else
                            {
                                Skynet_MSG_Send();
                            }
                        }
                        else if ((DateTime.Now.Second % 3) == 2)
                        {
                            if (LD[0].AUTO == true)
                            {
                                Form1.Send_LD_AMC_MSG("SEND", "NONE", "CMD", "NONE", "AUTO");
                            }
                            else
                            {
                                Form1.Send_LD_AMC_MSG("SEND", "NONE", "CMD", "NONE", "MANUAL");
                            }
                        }                        
                    }
                    else
                    {
                        //LD[0].LD_Client.Connect(new IPEndPoint(LD[0].LD_IP, 7171));
                        //LD[0].LD_Client.BeginConnect(new IPEndPoint(LD[0].LD_IP, 7171), new AsyncCallback(ConnectCallback), LD[0].LD_Client);
                    }

                    int a = 0;

                    if ((DateTime.Now.Second % 30) == 0)
                    {
                        a = skynet.Skynet_SM_Alive(Skynet_Param.LINE_CODE, Skynet_Param.PROCEESS_CODE, Skynet_Param.EQUIPMENT_ID, (heartbeat == true ? 1 : 0));
                        Write_Skynet_Log("Status_code : " + "ALIVE" + ", Line_code : " + Skynet_Param.LINE_CODE + ", Process_code : " + Skynet_Param.PROCEESS_CODE + ", Equipment_ID : " + Skynet_Param.EQUIPMENT_ID + ", Status : " + Skynet_Param.STATUS + ", Send : " + (heartbeat == true ? "1" : "0"));
                        Write_Skynet_Log("Status_code : " + "ALIVE" + ", Line_code : " + Skynet_Param.LINE_CODE + ", Process_code : " + Skynet_Param.PROCEESS_CODE + ", Equipment_ID : " + Skynet_Param.EQUIPMENT_ID + ", Status : " + Skynet_Param.STATUS + ", Res : " + a);

                        heartbeat = heartbeat == true ? false : true;
                        Monitor.Set_Skynet_ST(a);

                        if (is_1st == false && LD[0].LD_ST.LD_CHARGE != "")
                        {
                            a = skynet.Skynet_SM_Send_Run(Skynet_Param.LINE_CODE, Skynet_Param.PROCEESS_CODE, Skynet_Param.EQUIPMENT_ID, Skynet_Param.STATUS, LD[0].LD_ST.LD_CHARGE, LD[0].LD_ST.LD_AREA);
                            Write_Skynet_Log("Status_code : " + "CHARGE" + ", Line_code : " + Skynet_Param.LINE_CODE + ", Process_code : " + Skynet_Param.PROCEESS_CODE + ", Equipment_ID : " + Skynet_Param.EQUIPMENT_ID + ", Status : " + Skynet_Param.STATUS + ", Res : " + a);
                            Skynet_MSG_Send();

                            is_1st = true;
                        }
                        else
                        {
                            skynet.Skynet_Position(Skynet_Param.LINE_CODE, Skynet_Param.EQUIPMENT_ID, LD[0].LD_ST.LD_LOC.LD_X.ToString(), LD[0].LD_ST.LD_LOC.LD_Y.ToString(), Skynet_Param.STATUS);
                        }
                    }

                    if((DateTime.Now.Minute % 10) == 0)
                    {
                        if(DateTime.Now.Second == 0)
                        {                            
                            a = skynet.Skynet_SM_Send_Run(Skynet_Param.LINE_CODE, Skynet_Param.PROCEESS_CODE, Skynet_Param.EQUIPMENT_ID, Skynet_Param.STATUS, LD[0].LD_ST.LD_CHARGE, LD[0].LD_ST.LD_AREA);
                            Write_Skynet_Log("Status_code : " + "CHARGE" + ", Line_code : " + Skynet_Param.LINE_CODE + ", Process_code : " + Skynet_Param.PROCEESS_CODE + ", Equipment_ID : " + Skynet_Param.EQUIPMENT_ID + ", Status : " + Skynet_Param.STATUS + ", Res : " + a);
                            Skynet_MSG_Send();
                        }
                    }
                    

                    if (bg_reseve.IsBusy == false)
                    {
                        Insert_ERR_Log("bg_reserve Restart");
                        b_bg_terminator = true;
                        bg_reseve.RunWorkerAsync();
                    }

                    if(bg_Display.IsBusy == false)
                    {
                        Insert_ERR_Log("bg_Display Restart");
                        bDISPLAY_T = true;
                        bg_Display.RunWorkerAsync();
                    }

                    System.Threading.Thread.Sleep(1000);
                }
                catch (Exception ex)
                {
                    Insert_ERR_Log(ex);
                }
                
            }
        }


        static public void Skynet_MSG_Send()
        {
            if(Skynet_Param.B_STATUS_CODE != Skynet_Param.STATUS_CODE)
            {
                string status = "";

                if (Skynet_Param.STATUS_CODE == nSKYNET.SM_RUN)
                {
                    nSkynet_Res = skynet.Skynet_SM_Send_Run(Skynet_Param.LINE_CODE, Skynet_Param.PROCEESS_CODE, Skynet_Param.EQUIPMENT_ID, Skynet_Param.STATUS, Skynet_Param.SCR, Skynet_Param.DEST);                    
                    status = "RUN";                    
                }
                else if (Skynet_Param.STATUS_CODE == nSKYNET.SM_IDLE)
                {
                        nSkynet_Res = skynet.Skynet_SM_Send_Idle(Skynet_Param.LINE_CODE, Skynet_Param.PROCEESS_CODE, Skynet_Param.EQUIPMENT_ID, Skynet_Param.STATUS);
                        status = "IDLE";
                }
                else if (Skynet_Param.STATUS_CODE == nSKYNET.SM_ALARM)
                {
                    if (Skynet_Param.STATUS == "SETUP")
                    {
                        nSkynet_Res = skynet.Skynet_SM_Send_Setup(Skynet_Param.LINE_CODE, Skynet_Param.PROCEESS_CODE, Skynet_Param.EQUIPMENT_ID, "SETUP");
                    }
                    else
                    {
                        skynet.Skynet_SM_Send_Alarm(Skynet_Param.LINE_CODE, Skynet_Param.PROCEESS_CODE, Skynet_Param.EQUIPMENT_ID, Skynet_Param.STATUS);
                        skynet.Skynet_EM_DataSend(Skynet_Param.LINE_CODE, Skynet_Param.PROCEESS_CODE, Skynet_Param.EQUIPMENT_ID, Skynet_Param.ERR_CODE, Skynet_Param.ERR_TYPE, Skynet_Param.ERR_NAME, Skynet_Param.ERR_DESCRIPT, Skynet_Param.ERR_SOLUTION);
                        status = "ALARM";
                    }                    
                }

                Skynet_Param.B_STATUS_CODE = Skynet_Param.STATUS_CODE;
                Write_Skynet_Log("Status_code : " + status + ", Line_code : " + Skynet_Param.LINE_CODE + ", Process_code : " + Skynet_Param.PROCEESS_CODE + ", Equipment_ID : " + Skynet_Param.EQUIPMENT_ID + ", Status : " + Skynet_Param.STATUS + ", Res : " + nSkynet_Res);
                nSkynet_Res = skynet.Skynet_Position(Skynet_Param.LINE_CODE, Skynet_Param.EQUIPMENT_ID, LD[0].LD_ST.LD_LOC.LD_X.ToString(), LD[0].LD_ST.LD_LOC.LD_Y.ToString(), status);
                Write_Skynet_Log("Status_code : Position , Line_code : " + Skynet_Param.LINE_CODE + ", Process_code : " + Skynet_Param.PROCEESS_CODE + ", Equipment_ID : " + Skynet_Param.EQUIPMENT_ID + ", Status : " + Skynet_Param.STATUS +  "Position X:" + LD[0].LD_ST.LD_LOC.LD_X.ToString() + ", Y:" + LD[0].LD_ST.LD_LOC.LD_Y.ToString() + ", Res : " + nSkynet_Res);
            }
        }

        static private void Write_Skynet_Log(string MSG)
        {
            string date = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:FFF"); ;
            string str_temp = "";
            string log_dir = System.Environment.CurrentDirectory + "\\Log\\Skynet_" + System.DateTime.Now.ToString("yyyy/MM/dd") + "_Log.txt";

            try
            {
                if (System.IO.File.Exists(log_dir) == false)
                {
                    Directory.CreateDirectory(System.Environment.CurrentDirectory + "\\Log\\");
                    string temp;

                    temp = "========================================================" + Environment.NewLine;
                    temp += "=                                                                                                            =" + Environment.NewLine;
                    temp += "=                                      Skynet Log File " + System.DateTime.Now.ToString("yyyy/MM/dd") + "                                    =" + Environment.NewLine;
                    temp += "=                                                                                                            =" + Environment.NewLine;
                    temp += "========================================================" + Environment.NewLine;

                    System.IO.File.WriteAllText(log_dir, temp);
                    Check_Log_date(System.Environment.CurrentDirectory + "\\Log\\", Keepday);
                }

                //str_buf = System.IO.File.ReadAllText(log_dir);

                string[] arr_str = MSG.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                System.IO.StreamWriter st = System.IO.File.AppendText(log_dir);

                str_temp = date + " " + MSG;
                st.WriteLine(str_temp);

                st.Close();
                st.Dispose();
            }
            catch (Exception ex)
            {
                Insert_ERR_Log(ex);
                //Insert_Log(msg);
            }
        }

        static public void Set_Skynet_Status(nSKYNET ST_CODE, string ST)
        {
            Skynet_Param.STATUS_CODE = ST_CODE;
            Skynet_Param.STATUS = ST;

            Skynet_MSG_Send();
        }

        static public void Set_Skynet_Status(nSKYNET ST_CODE, string ST, string LOC)
        {
            Skynet_Param.STATUS_CODE = ST_CODE;
            Skynet_Param.STATUS = ST;
            Skynet_Param.SCR = LOC;

            Skynet_MSG_Send();
        }

        static public void Set_Skynet_Setup_Mode()
        {
            Skynet_Param.STATUS_CODE = nSKYNET.SM_ALARM;
            Skynet_Param.STATUS = "SETUP";

            Skynet_MSG_Send();
        }

        static public void Set_Skynet_Status(string ST)
        {
            Skynet_Param.STATUS = ST;

            Skynet_MSG_Send();
        }

        static public void Set_Skynet_Status(nSKYNET ST_CODE, string ST, string START, string TARGET)
        {
            Skynet_Param.STATUS_CODE = ST_CODE;
            Skynet_Param.STATUS = ST;
            Skynet_Param.SCR = START;
            Skynet_Param.DEST = TARGET;

            Skynet_MSG_Send();
        }

        private void Send_IO_ST()
        {
            if (LD[0].LD_TYPE == "MRBT")
            {
                for (int i = 1; i <= 16; i++)
                {
                    Send_string(LD[0].LD_Client, "inQ i" + i.ToString());
                    System.Threading.Thread.Sleep(50);
                }
            }
            
            for (int i = 1; i <= 16; i++)
            {
                Send_string(LD[0].LD_Client, "outQ o" + i.ToString());
                System.Threading.Thread.Sleep(50);
            }
        }

        private void CHK_DO()
        {
            for(int  i = 0; i< Doors.Length; i++)
            {
                if(Doors[i].ID == null ? false : true)
                {
                    if (LD_DO[1, int.Parse(Doors[i].DO)] == false && LD_DO[0, int.Parse(Doors[i].DO)] == true)
                    {
                        DOOR_Open(Doors[i].ID);
                    }

                    if (LD_DO[1, int.Parse(Doors[i].DO)] == true && LD_DO[0, int.Parse(Doors[i].DO)] == false)
                    {
                        DOOR_Close(Doors[i].ID);
                    }
                }
            }

            if(LD_DO[1,LD[0].ALARM_DO_NUM] == false && LD_DO[0, LD[0].ALARM_DO_NUM] == true)
            {
                ALARM_SRV.Send_arrived();
            }

            //if (LD_DO[1, 0] == false && LD_DO[0, 0] == true)
            //{
            //    DOOR_Open();
            //}
            //else if (LD_DO[1, 0] == true && LD_DO[0, 0] == false)
            //{
            //    DOOR_Close();
            //}

            //if (LD_DO[1, 1] == false && LD_DO[0, 1] == true)
            //{
            //    AIRSHOWER_IN_Open();
            //}
            //else if (LD_DO[1, 1] == true && LD_DO[0, 1] == false)
            //{
            //    AIRSHOWER_IN_Close();
            //}

            //if (LD_DO[1, 2] == false && LD_DO[0, 2] == true)
            //{
            //    AIRSHOWER_OUT_Open();
            //}
            //else if (LD_DO[1, 2] == true && LD_DO[0, 2] == false)
            //{
            //    AIRSHOWER_OUT_Close();
            //}
        }


        private void btn_Pulse_Out_Click(object sender, EventArgs e)
        {
            string str_val = "PULSE_OUT ";

            DialogResult res = InputBox("Pulse Out Input Box", "Input DO Port & Width", ref str_val);

            if (res == DialogResult.OK)
            {
                if (bis_insert == true)
                {
                    lb_cmd.Items.Insert(lb_cmd.SelectedIndex == -1 ? 0 : lb_cmd.SelectedIndex, "ON_PULSE_OUT " + str_val);
                    lb_cmd.SetSelected((lb_cmd.SelectedIndex == -1 ? 0 : lb_cmd.SelectedIndex) - 1, true);

                    bis_insert = false;
                }
                else
                {
                    lb_cmd.Items.Insert(lb_cmd.Items.Count, "GoGoal " + str_val);
                    lb_cmd.SetSelected(lb_cmd.Items.Count - 1, true);
                }
            }
        }


        bool[] befor_DO_ST = new bool[4];
        bool Griper_OFF_SENSOR;

        private void bg_modbus_DoWork(object sender, DoWorkEventArgs e)
        {
            bool[] bIN_res = new bool[16 * DI_BOARD_DEV_NUM.Length];
            bool[] bIN_temp = new bool[16];
            ushort[] DO = new ushort[15];
            bool[] bOUT_res = new bool[16 * DO_BOARD_DEV_NUM.Length];
            bool[] bOUT_temp = new bool[16];
            ushort[] usIN_res = new ushort[14];
            ushort[] usIN_temp = new ushort[14];
            ushort[] DI = new ushort[15];

            bool[] data = new bool[15];
            DateTime n;

            MOD_ERR_CLEAR();
            System.Threading.Thread.Sleep(10);
            //MOD_Direct_MOVE(LINEAR_CENTER_POSITION);

            System.Threading.Thread.Sleep(3000);

            while (bMODBUS_T)
            {
                try
                {
                    n = DateTime.Now;
                    DI = IO_modbusMaster.ReadInputRegisters(DI_BOARD_DEV_NUM[0], DI_ADDRESS, 16);

                    if (bDo_ungrip == true)
                    {
                        if(bg_ungrip.IsBusy == false)
                            bg_ungrip.RunWorkerAsync();

                        bDo_ungrip = false;
                    }



                    for (int j = 0; j < 16; j++)
                    {
                        bIN_temp[j] = ((DI[0] >> j) & 0x01) == 0x01 ? true : false;
                        //bIN_res[(i * 16) + j] = bIN_temp[j];
                        INPUT_DATA[1, j] = INPUT_DATA[0, j];

                        if (In_Port_AB[j] == true)
                        {
                            INPUT_DATA[0, j] = bIN_temp[j] == true ? false : true;
                        }
                        else
                        {
                            INPUT_DATA[0, j] = bIN_temp[j];
                        }
                    }


                    System.Threading.Thread.Sleep(5);

                    DO = IO_modbusMaster.ReadHoldingRegisters(3, DO_ADDRESS, 1);


                    for (int j = 0; j < 15; j++)
                    {
                        bOUT_temp[j] = ((DO[0] >> j) & 0x01) == 0x01 ? true : false;
                    }

                    System.Threading.Thread.Sleep(5);

                    //for (int j = 0; j < 15; j++)
                    //{
                    //    bOUT_temp[j] = ((DO[0] >> j) & 0x01) == 0x01 ? true : false;
                    //    bOUT_res[j] = bOUT_temp[j];
                    //    OUTPUT_DATA[1, j] = OUTPUT_DATA[0, j];
                    //    OUTPUT_DATA[0, j] = bOUT_temp[j];

                    //}


                    for (int i = 0; i < 8; i++)
                    {
                        if (OUTPUT_DATA[1, i] != OUTPUT_DATA[0, i] || bOUT_temp[i] != OUTPUT_DATA[0, i])
                        {
                            IO_modbusMaster.WriteSingleCoil(3, (ushort)(DO_ADDRESS + i), OUTPUT_DATA[0, i]);
                            Insert_System_Log("DO" + i.ToString() + (OUTPUT_DATA[0, i] == true ? " ON" : " OFF"));
                            System.Threading.Thread.Sleep(5);
                        }

                        OUTPUT_DATA[1, i] = OUTPUT_DATA[0, i];
                    }

                    System.Threading.Thread.Sleep(4);

                    usIN_temp = IO_modbusMaster.ReadInputRegisters(AI_BOARD_DEV_NUM[0], AI_ADDRESS, 2);
                    System.Threading.Thread.Sleep(5);

                    for (int j = 0; j < usIN_temp.Length; j++)
                    {
                        usIN_res[j] = usIN_temp[j];
                    }

                    LD[0].LD_ST.LD_LDS1 = (usIN_res[0] - 6553).ToString();
                    LD[0].LD_ST.LD_LDS2 = (usIN_res[1] - 6553).ToString();
                    Get_LDS_Distance();


                    if ((INPUT_DATA[0, IN_NUM_MAN_STOP_SENSOR2] == true || INPUT_DATA[0, IN_NUM_TRANSFER_SENSOR] == true) && (Conveyor_MODE == "") && IS_falling(IN_NUM_MAN_OUT_SW) == true)
                    {
                        Conveyor_step = 1;
                        Conveyor_MODE = "MAN_IN";
                        Start_bg_Conveyor();
                    }
                    else if (INPUT_DATA[0, IN_NUM_MAN_OUT_SW] == true)
                    {
                        if (IS_Rising(IN_NUM_MAN_OUT_SW) == true)
                        {
                            Set_Timer();
                        }

                        TimeSpan dateDiff = DateTime.Now - MAN_StartDate;

                        if (dateDiff.Seconds >= 2)
                        {
                            Conveyor_step = 1;
                            Conveyor_MODE = "MAN_OUT";
                            Start_bg_Conveyor();
                        }
                    }

                    if (Conveyor_MODE == "" || Conveyor_MODE == "MAN_IN" || Conveyor_MODE == "MAN_OUT")
                    {
                        if (IS_Rising(IN_NUM_LIGHT_CURTAIN) == true)
                        {
                            befor_DO_ST[0] = OUTPUT_DATA[0, OUT_NUM_CONVEYOR_DIRECTION];
                            befor_DO_ST[1] = OUTPUT_DATA[0, OUT_NUM_CONVEYOR_RUN];
                            befor_DO_ST[2] = OUTPUT_DATA[0, OUT_NUM_GRIPER_DIRECTION];
                            befor_DO_ST[3] = OUTPUT_DATA[0, OUT_NUM_GRIPER_RUN];

                            Conveyor_STOP();
                            Griper_Stop();
                        }
                        else if (IS_falling(IN_NUM_LIGHT_CURTAIN) == true)
                        {
                            OUTPUT_DATA[0, OUT_NUM_CONVEYOR_DIRECTION] = befor_DO_ST[0];
                            OUTPUT_DATA[0, OUT_NUM_CONVEYOR_RUN] = befor_DO_ST[1];
                            OUTPUT_DATA[0, OUT_NUM_GRIPER_DIRECTION] = befor_DO_ST[2];
                            OUTPUT_DATA[0, OUT_NUM_GRIPER_RUN] = befor_DO_ST[3];

                            if (befor_DO_ST[0] == true && befor_DO_ST[1] == true)
                            {
                                Conveyor_CCW_RUN();
                            }
                            else if (befor_DO_ST[0] == false && befor_DO_ST[1] == true)
                            {
                                Conveyor_CW_RUN();
                            }

                            if (befor_DO_ST[2] == true && befor_DO_ST[3] == true)
                            {
                                Griper_CCW_Run();
                            }
                            else if (befor_DO_ST[2] == false && befor_DO_ST[3] == true)
                            {
                                Griper_CW_Run();
                            }
                        }
                    }


                    if (bEStop == false)
                    {
                        //ushort[] AL = IO_modbusMaster.ReadHoldingRegisters(LINEAR_DEV_NUM, 128, 2);
                        //System.Threading.Thread.Sleep(5);
                        //ushort[] POS = IO_modbusMaster.ReadHoldingRegisters(LINEAR_DEV_NUM, 204, 2);


                        //LINEAR_POS = short.Parse(POS[1].ToString("X"), System.Globalization.NumberStyles.HexNumber);
                        //LINEAR_AL = AL[1];

                        //if (AL[0] != 0)
                        //{
                        //    ERR_QUEUE.Insert_ERR(AL[0]);
                        //}
                    }



                    System.Threading.Thread.Sleep(5);

                    tb_LDS1.Text = (DateTime.Now - n).TotalMilliseconds.ToString();

                }
                catch (Exception)
                {
                    bg_modbus_DoWork(sender, e);
                    throw;
                }
            }
        }

        private void Start_bg_Conveyor()
        {
            bCONVEYOR_T = true;


            if (lb_cmd.Items.Count != 0)
            {
                string str_cmd_temp = lb_cmd.Items[nCmd_Step].ToString().ToUpper();

                string[] str_cmd_arr = str_cmd_temp.Split(' ');

                if (str_cmd_arr[0] == "CONVEYOR_IN")
                {
                    Conveyor_step = 1;
                    if (bg_Conveyor.IsBusy == false)
                    {
                        bg_Conveyor.RunWorkerAsync();
                        //Send_AMC_MSG("RECV", STB_NAME, CMD_NAME, GOAL_NAME, "CONVEYOR_RUN_OK");
                    }
                }
                else if (str_cmd_arr[0] == "CONVEYOR_OUT")
                {
                    if (bg_Conveyor.IsBusy == false)
                    {
                        Conveyor_step = 1;
                        bg_Conveyor.RunWorkerAsync();
                        //Send_AMC_MSG("RECV", STB_NAME, CMD_NAME, GOAL_NAME, "CONVEYOR_RUN_OK");
                    }
                }
                else if (Conveyor_MODE == "MAN_OUT" && str_cmd_arr[0] == "MAN_OUT")
                {
                    if (bg_Conveyor.IsBusy == false)
                    {
                        Conveyor_step = 1;
                        bg_Conveyor.RunWorkerAsync();
                    }
                }
                else if (Conveyor_MODE == "MAN_IN" && str_cmd_arr[0] == "MAN_IN")
                {
                    if (bg_Conveyor.IsBusy == false)
                    {
                        Conveyor_step = 1;
                        bg_Conveyor.RunWorkerAsync();
                    }
                }
            }
            else
            {
                if (Conveyor_MODE == "MAN_OUT")
                {
                    if (bg_Conveyor.IsBusy == false)
                    {
                        Conveyor_step = 1;
                        bg_Conveyor.RunWorkerAsync();
                    }
                }
                else if (Conveyor_MODE == "MAN_IN")
                {
                    if (bg_Conveyor.IsBusy == false)
                    {
                        Conveyor_step = 1;
                        bg_Conveyor.RunWorkerAsync();
                    }
                }
            }




        }

        private void DO_OUT(ushort DO_NUM, bool val)
        {
            IO_modbusMaster.WriteSingleCoil(byte.Parse((int.Parse(str_cmd_arr[1]) / 16 + 1).ToString()), ushort.Parse((DO_ADDRESS + DO_NUM + (int.Parse(str_cmd_arr[1]) / 16)).ToString(), System.Globalization.NumberStyles.AllowCurrencySymbol), val);
        }

        private void bg_Display_DoWork(object sender, DoWorkEventArgs e)
        {
            System.Threading.Thread.Sleep(5000);

            while (bDISPLAY_T)
            {
                // insert_listbox에서 처리
                //for (int i = 1; i <= 16; i++)
                //{
                //    HMI.Set_io_lamp("Input:", "i"+ i.ToString(), INPUT_DATA[0, i-1] == true ? "on" : "off");
                //    HMI.Set_io_lamp("Onput:", "i" + i.ToString(), OUTPUT_DATA[0, i - 1] == true ? "on" : "off");
                //}

                try
                {
                    Monitor.Set_IN_DATA(INPUT_DATA);
                    Monitor.Set_OUT_DATA(OUTPUT_DATA);
                    Monitor.Set_DIGITAL_DATA(DIGITAL_DATA);
                    Monitor.set_vehicle_location(new Point(LD[0].LD_ST.LD_LOC.LD_X, LD[0].LD_ST.LD_LOC.LD_Y));

                    if (ERR_QUEUE.Is_EMPTY() == false && Monitor.Get_Alarm_Panel_Visible() == false)
                    {


                        Monitor.Set_MSG(ERR_QUEUE.Get_now_err_msg());

                        Monitor.Set_Code(ERR_QUEUE.Get_now_err_code().ToString());
                        Monitor.Set_Name(ERR_QUEUE.Get_now_err_name());
                        Monitor.Set_Solution(ERR_QUEUE.Get_now_err_solution());
                        Monitor.Set_Localize(LD[0].LD_ST.LD_Localizetion_Score);
                        Monitor.Set_Link(ERR_QUEUE.Get_now_err_manual());

                       

                        Skynet_Param.ERR_CODE = ERR_QUEUE.Get_now_err_code().ToString();
                        Skynet_Param.ERR_NAME = ERR_QUEUE.Get_now_err_name();
                        Skynet_Param.ERR_TYPE = "0";
                        Skynet_Param.ERR_DESCRIPT = ERR_QUEUE.Get_now_err_description();
                        Skynet_Param.ERR_SOLUTION = ERR_QUEUE.Get_now_err_solution();

                        Monitor.Start_local_score();

                        Monitor.Set_Alarm();
                        Set_Skynet_Status(nSKYNET.SM_ALARM, ERR_QUEUE.Get_now_err_msg());

                    }
                    else
                    {
                        Monitor.Set_MSG(LD[0].LD_ST.LD_ST);
                    }


                    Set_LAMP();
                    //Set_Off_DO();

                    if (ERR_QUEUE.Is_EMPTY() == false)
                    {
                        LD[0].LD_ST.LD_ST = ERR_QUEUE.Get_now_err_msg();
                        Conveyor_STOP();
                        Griper_Stop();
                    }

                    if (bg_modbus.IsBusy == false)
                    {
                        bg_modbus.RunWorkerAsync();
                    }

                    if (Monitor.Get_AMC_close() == true)
                    {
                        this.Close();
                    }

                    if(LD[0].LD_Client.Connected == false)
                    {
                        Insert_ERR_Log("LD Client reConnect");
                        BeginConnect2(LD[0].LD_IP.ToString(), 7171);
                        //LD[0].LD_Client.Connect(new IPEndPoint(LD[0].LD_IP, 7171));
                    }

                    System.Threading.Thread.Sleep(200);
                }
                catch (Exception ex)
                {
                    Insert_ERR_Log(ex);
                }
            }
        }

        

        public static void BeginConnect2(string host, int port)
        {
            IPAddress[] IPs = Dns.GetHostAddresses(host);

            LD[0].LD_Client = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp);

            
            LD[0].LD_Client.BeginConnect(IPs, port,
                new AsyncCallback(ConnectCallback1), LD[0].LD_Client);
                      
        }

        public static void ConnectCallback1(IAsyncResult ar)
        {
            try
            {
                Socket s = (Socket)ar.AsyncState;
                s.EndConnect(ar);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        //private static void ConnectCallback(IAsyncResult ar)
        //{
        //    try
        //    {
        //        // Retrieve the socket from the state object.  

        //        Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //        // Complete the connection.  
        //        client.EndConnect(ar);

        //        Console.WriteLine("Socket connected to {0}",
        //            client.RemoteEndPoint.ToString());

        //        // Signal that the connection has been made.  
        //        //connectDone.Set();
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.ToString());
        //    }
        //}

        private void MOD_ERR_CLEAR()
        {
            try
            {
                ushort[] temp = new ushort[2];

                temp[0] = 0x0000;
                temp[1] = 0x0001;

                IO_modbusMaster.WriteMultipleRegisters(LINEAR_DEV_NUM, LINEAR_AL_CLEAR_ADD, temp);

                temp[0] = 0x0000;
                temp[1] = 0x0000;

                IO_modbusMaster.WriteMultipleRegisters(LINEAR_DEV_NUM, LINEAR_AL_CLEAR_ADD, temp);
            }
            catch (Exception)
            {


            }
        }


        private void MOD_Direct_MOVE(int value)
        {
            const ushort addr = 0x58;
            ushort[] temp = new ushort[16];

            temp[0] = 0x0000;
            temp[1] = 0x0000; // 운전 data  default : 0
            temp[2] = 0x0000;
            temp[3] = 0x0001; // 절대위치 결정. default : 2(상대위치 결정)
            temp[4] = (ushort)((value >> 16) & 0xFFFF);
            temp[5] = (ushort)(value & 0xFFFF);    // 위치 값    default : 0
            temp[6] = 0x0000;
            temp[7] = 0x07D0;    // 속도 값 default : 1000
            temp[8] = 0x0000;
            temp[9] = 0x05DC;    //기동 Rate   default : 1000000
            temp[10] = 0x0000;
            temp[11] = 0x05DC;    // 정지 Rate  default : 1000000
            temp[12] = 0x0000;
            temp[13] = 0x03E8;    // 운전 전류    default : 1000
            temp[14] = 0x0000;
            temp[15] = 0x0001;    //  반영 Trigger  default : 0(무효)


            IO_modbusMaster.WriteMultipleRegisters(LINEAR_DEV_NUM, addr, temp);

        }

        private void btn_check_io_Click(object sender, EventArgs e)
        {
            lb_cmd.Items.Insert(lb_cmd.SelectedIndex == -1 ? 0 : lb_cmd.SelectedIndex, "CORRECTION");
            lb_cmd.SetSelected(lb_cmd.Items.Count - 1, true);


        }

        int Conveyor_step = 0;

        private void bg_Conveyor_DoWork(object sender, DoWorkEventArgs e)
        {
            DateTime n1;
            while (bCONVEYOR_T)
            {
                textBox2.Text = Conveyor_step.ToString();
                n1 = DateTime.Now;
                if ((Conveyor_MODE == "MAN_IN" || Conveyor_MODE == "MAN_OUT"))
                {
                    if (INPUT_DATA[0, IN_NUM_LIGHT_CURTAIN] == false && ERR_QUEUE.MAX_ERR_LV() <= 1)
                    {
                        if (Conveyor_MODE == "MAN_IN")
                        {
                            MAN_IN_Conveyor_Work();
                        }
                        else if (Conveyor_MODE == "AUTO_IN")
                        {
                            ///AUTO_IN_Conveyor_Work();
                        }
                        else if (Conveyor_MODE == "MAN_OUT")
                        {
                            MAN_OUT_Conveyor_Work();
                        }
                    }
                }
                else if (Conveyor_MODE == "AUTO_OUT")
                {
                    if (ERR_QUEUE.MAX_ERR_LV() == 0)
                    {
                        AUTO_OUT_Conveyor_Work();
                    }
                }

                System.Threading.Thread.Sleep(10);

                tb_LDS2.Text = (DateTime.Now - n1).TotalMilliseconds.ToString();
            }
        }

        private int Tray_Start_POS;

        private DateTime MAN_IN_bTime;
        private const int MAN_CONVEYOR_TIME_OUT = 10000;
        private const int MAN_GRIPER_TIME_OUT = 5000;
        private const int MAN_GRIPER_ON_TIME = 2500;

        private void MAN_IN_Conveyor_Work()
        {


            if (INPUT_DATA[0, IN_NUM_TRAY_SENSOR1] == true && INPUT_DATA[0, IN_NUM_TRAY_SENSOR1_1] == false)
            {
                STOPPER.MOTOR_ON(STOPPER1_MOTOR_NUM);
                Insert_System_Log("MAN_IN tray sensor1 detact " + Conveyor_step.ToString());
            }

            if (INPUT_DATA[0, IN_NUM_TRAY_SENSOR1] == true && INPUT_DATA[0, IN_NUM_TRAY_SENSOR2] == true && INPUT_DATA[0, IN_NUM_TRAY_SENSOR2_1] == false)
            {
                STOPPER.MOTOR_ON(STOPPER2_MOTOR_NUM);
                Insert_System_Log("MAN_IN tray sensor2 detact");
            }
            if (INPUT_DATA[0, IN_NUM_TRAY_SENSOR1] == true && INPUT_DATA[0, IN_NUM_TRAY_SENSOR2] == true && INPUT_DATA[0, IN_NUM_TRAY_SENSOR3] == true && INPUT_DATA[0, IN_NUM_TRAY_SENSOR3_1] == false)
            {
                STOPPER.MOTOR_ON(STOPPER3_MOTOR_NUM);
                Insert_System_Log("MAN_IN tray sensor3 detact");
            }


            switch (Conveyor_step)
            {
                case 0:
                    break;
                case 1:
                    STOPPER.motor_clear();

                    if (INPUT_DATA[0, IN_NUM_TRAY_SENSOR1] == true && INPUT_DATA[0, IN_NUM_TRAY_SENSOR2] == true && INPUT_DATA[0, IN_NUM_TRAY_SENSOR3] == true && INPUT_DATA[0, IN_NUM_TRAY_SENSOR4] == true)
                    {
                        Insert_ERR((int)ERR_Q.ERR_CODE.MAN_IN_TRAY_FULL);
                        return;
                    }
                    else
                    {
                        STOPPER.MOTOR_OFF(STOPPER1_MOTOR_NUM);
                        STOPPER.MOTOR_OFF(STOPPER2_MOTOR_NUM);
                        Conveyor_step++;
                    }

                    Insert_System_Log("Manual in run");
                    break;
                case 2:
                    //if (INPUT_DATA[0, IN_NUM_MAN_OUT_SW] == true)

                    STOPPER.MOTOR_ON(STOPPER3_MOTOR_NUM);
                    MAN_IN_bTime = DateTime.Now;


                    if (INPUT_DATA[0, IN_NUM_TRANSFER_SENSOR] == false)
                    {
                        Conveyor_step++;
                        Insert_System_Log("MAN_IN transfer sensor off");
                    }
                    else if ((DateTime.Now - MAN_IN_bTime).Milliseconds < MAN_CONVEYOR_TIME_OUT)
                    {
                        Conveyor_CW_RUN();
                    }

                    break;
                case 3:
                    if (INPUT_DATA[0, IN_NUM_TRAY_SENSOR4] == true)
                    {
                        System.Threading.Thread.Sleep(1500);
                        Conveyor_STOP();
                        Conveyor_step++;
                        Insert_System_Log("MAN_IN tray sensor4 detect");
                    }
                    else if ((DateTime.Now - MAN_IN_bTime).TotalMilliseconds >= MAN_CONVEYOR_TIME_OUT)
                    {
                        Conveyor_STOP();
                        Insert_ERR((int)ERR_Q.ERR_CODE.MAN_IN_CONVEYOR_TIME_OUT);
                    }
                    break;
                case 4:
                    MAN_IN_bTime = DateTime.Now;
                    Griper_CCW_Run();
                    Conveyor_step++;
                    break;
                case 5:
                    if (INPUT_DATA[0, IN_NUM_GRIPER_ON_LIMIT] == true || (DateTime.Now - MAN_IN_bTime).TotalMilliseconds >= MAN_GRIPER_ON_TIME)
                    {
                        Insert_System_Log("MAN_IN Griper on limit detact");
                        MAN_IN_bTime = DateTime.Now;
                        Griper_CW_Run();
                        Conveyor_step++;
                    }
                    else if ((DateTime.Now - MAN_IN_bTime).TotalMilliseconds >= MAN_GRIPER_TIME_OUT)
                    {
                        Griper_Stop();
                        Insert_ERR((int)ERR_Q.ERR_CODE.MAN_IN_GRIPER_TIME_OUT);
                    }
                    else
                    {
                        Griper_CCW_Run();
                    }
                    break;
                case 6:
                    Griper_CW_Run();
                    Conveyor_step++;
                    break;
                case 7:
                    if (INPUT_DATA[0, IN_NUM_GRIPER_OFF_LIMIT] == true || (DateTime.Now - MAN_IN_bTime).TotalMilliseconds >= 2000)
                    {
                        Griper_Stop();
                        Conveyor_step++;
                        Insert_System_Log("MAN_IN Griper off limit detact");
                    }
                    else if ((DateTime.Now - MAN_IN_bTime).TotalMilliseconds >= MAN_GRIPER_TIME_OUT)
                    {
                        Griper_Stop();
                        Insert_ERR((int)ERR_Q.ERR_CODE.MAN_IN_GRIPER_TIME_OUT);
                    }
                    break;
                case 8:
                    STOPPER.MOTOR_OFF(STOPPER3_MOTOR_NUM);
                    //STOPPER.MOTOR_OFF(STOPPER2_MOTOR_NUM);
                    Conveyor_step++;
                    break;
                case 9:
                    MAN_IN_bTime = DateTime.Now;
                    Conveyor_CW_RUN();
                    Conveyor_step++;
                    break;
                case 10:
                    if (INPUT_DATA[0, IN_NUM_TRAY_SENSOR3] == true)
                    {
                        Conveyor_STOP();
                        Conveyor_step++;
                        Insert_System_Log("MAN_IN tray sensor3 detact");

                    }
                    else if ((DateTime.Now - MAN_IN_bTime).TotalMilliseconds >= MAN_CONVEYOR_TIME_OUT)
                    {
                        Conveyor_STOP();
                        Insert_ERR((int)ERR_Q.ERR_CODE.MAN_IN_CONVEYOR_TIME_OUT);
                    }
                    break;
                case 11:
                    if ((INPUT_DATA[0, IN_NUM_MAN_STOP_SENSOR2] == true || INPUT_DATA[0, IN_NUM_MAN_STOP_SENSOR1] == true || INPUT_DATA[0, IN_NUM_TRANSFER_SENSOR] == true))
                    {
                        Conveyor_STOP();
                        if (INPUT_DATA[0, IN_NUM_MAN_OUT_SW] == true)
                        {
                            Insert_System_Log("MAN_IN manual switch push");
                            STOPPER.MOTOR_ON(STOPPER3_MOTOR_NUM);
                            //STOPPER.MOTOR_OFF(STOPPER2_MOTOR_NUM);
                            //STOPPER.MOTOR_ON(STOPPER1_MOTOR_NUM);

                            MAN_IN_bTime = DateTime.Now;
                            Conveyor_CW_RUN();
                            Conveyor_step++;
                        }
                    }
                    break;
                case 12:
                    if (INPUT_DATA[0, IN_NUM_TRAY_SENSOR4] == true)
                    {
                        System.Threading.Thread.Sleep(1000);
                        Conveyor_STOP();
                        Conveyor_step++;
                        Insert_System_Log("MAN_IN tray sensor4 detact");
                    }
                    else if ((DateTime.Now - MAN_IN_bTime).TotalMilliseconds >= MAN_CONVEYOR_TIME_OUT)
                    {
                        Insert_ERR((int)ERR_Q.ERR_CODE.MAN_IN_CONVEYOR_TIME_OUT);
                    }
                    break;
                case 13:
                    MAN_IN_bTime = DateTime.Now;
                    Griper_CCW_Run();
                    Conveyor_step++;
                    break;
                case 14:
                    if (INPUT_DATA[0, IN_NUM_GRIPER_ON_LIMIT] == true || (DateTime.Now - MAN_IN_bTime).TotalMilliseconds >= MAN_GRIPER_ON_TIME)
                    {
                        Insert_System_Log("MAN_IN griper on limit detact");
                        MAN_IN_bTime = DateTime.Now;
                        Griper_CW_Run();
                        Conveyor_step++;
                    }
                    else if ((DateTime.Now - MAN_IN_bTime).TotalMilliseconds >= MAN_GRIPER_TIME_OUT)
                    {
                        Griper_Stop();
                        Insert_ERR((int)ERR_Q.ERR_CODE.MAN_IN_GRIPER_TIME_OUT);
                    }
                    else
                    {
                        Griper_CCW_Run();
                    }
                    break;
                case 15:
                    Griper_CW_Run();
                    Conveyor_step++;
                    break;
                case 16:
                    if (INPUT_DATA[0, IN_NUM_GRIPER_OFF_LIMIT] == true || (DateTime.Now - MAN_IN_bTime).TotalMilliseconds >= 2000)
                    {
                        MAN_IN_bTime = DateTime.Now;
                        Griper_Stop();
                        STOPPER.MOTOR_OFF(STOPPER3_MOTOR_NUM);
                        Conveyor_step++;
                        Insert_System_Log("MAN_IN griper off limit detact");
                    }
                    else if ((DateTime.Now - MAN_IN_bTime).TotalMilliseconds >= MAN_GRIPER_TIME_OUT)
                    {
                        Griper_Stop();
                        Insert_ERR((int)ERR_Q.ERR_CODE.MAN_IN_GRIPER_TIME_OUT);
                    }
                    break;
                case 17:
                    MAN_IN_bTime = DateTime.Now;
                    Conveyor_CW_RUN();
                    Conveyor_step++;
                    break;
                case 18:
                    if (INPUT_DATA[0, IN_NUM_TRAY_SENSOR3] == true)
                    {
                        if (INPUT_DATA[0, IN_NUM_TRAY_SENSOR3_1] == false && INPUT_DATA[0, IN_NUM_TRAY_SENSOR4] == false)
                        {
                            Conveyor_STOP();
                            //STOPPER.MOTOR_ON(STOPPER3_MOTOR_NUM);
                            Conveyor_step++;
                            Insert_System_Log("MAN_IN tary sensor3 detact");
                        }
                    }
                    else if ((DateTime.Now - MAN_IN_bTime).TotalMilliseconds >= MAN_CONVEYOR_TIME_OUT)
                    {
                        Conveyor_STOP();
                        Insert_ERR((int)ERR_Q.ERR_CODE.MAN_IN_CONVEYOR_TIME_OUT);
                    }

                    break;
                case 19:
                    if ((INPUT_DATA[0, IN_NUM_MAN_STOP_SENSOR2] == true || INPUT_DATA[0, IN_NUM_MAN_STOP_SENSOR1] == true || INPUT_DATA[0, IN_NUM_TRANSFER_SENSOR] == true))
                    {
                        Conveyor_STOP();
                        if (INPUT_DATA[0, IN_NUM_MAN_OUT_SW] == true)
                        {
                            Insert_System_Log("MAN_IN manual switch push");
                            STOPPER.MOTOR_ON(STOPPER3_MOTOR_NUM);
                            MAN_IN_bTime = DateTime.Now;
                            Conveyor_CW_RUN();
                            Conveyor_step++;
                        }

                    }
                    break;
                case 20:
                    if (INPUT_DATA[0, IN_NUM_TRAY_SENSOR4] == true)
                    {
                        System.Threading.Thread.Sleep(1500);
                        Conveyor_STOP();
                        Conveyor_step++;
                        Insert_System_Log("MAN_IN tray sensor4 detact");
                    }
                    else if ((DateTime.Now - MAN_IN_bTime).TotalMilliseconds >= MAN_CONVEYOR_TIME_OUT)
                    {
                        Insert_ERR((int)ERR_Q.ERR_CODE.MAN_IN_CONVEYOR_TIME_OUT);
                    }
                    break;
                case 21:
                    MAN_IN_bTime = DateTime.Now;
                    Griper_CCW_Run();
                    Conveyor_step++;
                    break;
                case 22:
                    if (INPUT_DATA[0, IN_NUM_GRIPER_ON_LIMIT] == true || (DateTime.Now - MAN_IN_bTime).TotalMilliseconds >= MAN_GRIPER_ON_TIME)
                    {
                        MAN_IN_bTime = DateTime.Now;
                        Griper_CW_Run();
                        Conveyor_step++;
                        Insert_System_Log("MAN_IN griper on limit detact");
                    }
                    else if ((DateTime.Now - MAN_IN_bTime).TotalMilliseconds >= MAN_GRIPER_TIME_OUT)
                    {
                        Griper_Stop();
                        Insert_ERR((int)ERR_Q.ERR_CODE.MAN_IN_GRIPER_TIME_OUT);
                    }
                    else
                    {
                        Griper_CCW_Run();
                    }
                    break;
                case 23:

                    Conveyor_step++;
                    break;
                case 24:
                    if (INPUT_DATA[0, IN_NUM_GRIPER_OFF_LIMIT] == true || (DateTime.Now - MAN_IN_bTime).TotalMilliseconds >= 2000)
                    {
                        Griper_Stop();
                        STOPPER.MOTOR_OFF(STOPPER3_MOTOR_NUM);
                        Conveyor_step++;
                        Insert_System_Log("MAN_IN griper off limit detact");
                    }
                    else if ((DateTime.Now - MAN_IN_bTime).TotalMilliseconds >= MAN_GRIPER_TIME_OUT)
                    {
                        Griper_Stop();
                        Insert_ERR((int)ERR_Q.ERR_CODE.MAN_IN_GRIPER_TIME_OUT);
                    }
                    break;

                case 25:
                    MAN_IN_bTime = DateTime.Now;
                    Conveyor_CW_RUN();
                    Conveyor_step++;
                    break;
                case 26:

                    if (INPUT_DATA[0, IN_NUM_TRAY_SENSOR3] == true && INPUT_DATA[0, IN_NUM_TRAY_SENSOR3_1] == false)//INPUT_DATA[0, IN_NUM_TRAY_SENSOR1] == true || INPUT_DATA[0, IN_NUM_TRAY_SENSOR1_1] == false)
                    {
                        Conveyor_STOP();
                        STOPPER.MOTOR_ON(STOPPER1_MOTOR_NUM);
                        Conveyor_step++;
                        Insert_System_Log("MAN_IN tray sensor3 detact");
                    }
                    else if ((DateTime.Now - MAN_IN_bTime).TotalMilliseconds >= MAN_CONVEYOR_TIME_OUT)
                    {
                        Conveyor_STOP();
                        Insert_ERR((int)ERR_Q.ERR_CODE.MAN_IN_CONVEYOR_TIME_OUT);
                    }

                    break;
                case 27:
                    MAN_IN_bTime = DateTime.Now;
                    Conveyor_CW_RUN();
                    Conveyor_step++;
                    break;
                case 28:

                    if ((INPUT_DATA[0, IN_NUM_TRAY_SENSOR3] == true && INPUT_DATA[0, IN_NUM_TRAY_SENSOR3_1] == false) && INPUT_DATA[0, IN_NUM_TRAY_SENSOR4] == false)
                    {
                        Conveyor_STOP();
                        //STOPPER.MOTOR_ON(STOPPER3_MOTOR_NUM);
                        Conveyor_step++;
                        Insert_System_Log("MAN_IN tray sensor3 detact");
                    }
                    else if ((DateTime.Now - MAN_IN_bTime).TotalMilliseconds >= MAN_CONVEYOR_TIME_OUT)
                    {
                        Conveyor_STOP();
                        Insert_ERR((int)ERR_Q.ERR_CODE.MAN_IN_CONVEYOR_TIME_OUT);
                    }
                    break;
                case 29:
                    if ((INPUT_DATA[0, IN_NUM_MAN_STOP_SENSOR2] == true || INPUT_DATA[0, IN_NUM_MAN_STOP_SENSOR1] == true || INPUT_DATA[0, IN_NUM_TRANSFER_SENSOR] == true))
                    {
                        Conveyor_STOP();
                        if (INPUT_DATA[0, IN_NUM_MAN_OUT_SW] == true)
                        {
                            Insert_System_Log("MAN_IN manual switch push");
                            STOPPER.MOTOR_ON(STOPPER3_MOTOR_NUM);

                            MAN_IN_bTime = DateTime.Now;
                            Conveyor_CW_RUN();
                            Conveyor_step++;
                        }
                    }

                    break;
                case 30:
                    if (INPUT_DATA[0, IN_NUM_TRAY_SENSOR4] == true)
                    {
                        System.Threading.Thread.Sleep(1500);
                        Conveyor_STOP();
                        Conveyor_step++;
                        Insert_System_Log("MAN_IN tray sensor4 detact");
                    }
                    else if ((DateTime.Now - MAN_IN_bTime).TotalMilliseconds >= MAN_CONVEYOR_TIME_OUT)
                    {
                        Conveyor_STOP();
                        Insert_ERR((int)ERR_Q.ERR_CODE.MAN_IN_CONVEYOR_TIME_OUT);
                    }
                    break;
                case 31:
                    MAN_IN_bTime = DateTime.Now;
                    Griper_CCW_Run();
                    Conveyor_step++;
                    break;
                case 32:
                    if (INPUT_DATA[0, IN_NUM_GRIPER_ON_LIMIT] == true || (DateTime.Now - MAN_IN_bTime).TotalMilliseconds >= MAN_GRIPER_ON_TIME)
                    {
                        MAN_IN_bTime = DateTime.Now;
                        Griper_CW_Run();
                        Conveyor_step++;
                        Insert_System_Log("MAN_IN griper on limit detact");
                    }
                    else if ((DateTime.Now - MAN_IN_bTime).TotalMilliseconds >= MAN_GRIPER_TIME_OUT)
                    {
                        Griper_Stop();
                        Insert_ERR((int)ERR_Q.ERR_CODE.MAN_IN_GRIPER_TIME_OUT);
                    }
                    else
                    {
                        Griper_CCW_Run();
                    }
                    break;
                case 33:

                    Conveyor_step++;
                    break;
                case 34:

                    if (INPUT_DATA[0, IN_NUM_GRIPER_OFF_LIMIT] == true || (DateTime.Now - MAN_IN_bTime).TotalMilliseconds >= MAN_GRIPER_ON_TIME)
                    {
                        Griper_Stop();
                        Conveyor_step++;
                        Insert_System_Log("MAN_IN griper off limit detact");
                    }
                    else if ((DateTime.Now - MAN_IN_bTime).TotalMilliseconds >= MAN_GRIPER_TIME_OUT)
                    {
                        Griper_Stop();
                        Insert_ERR((int)ERR_Q.ERR_CODE.MAN_IN_GRIPER_TIME_OUT);
                    }
                    else
                    {
                        Griper_CW_Run();
                    }
                    break;
                case 35:
                    Conveyor_MODE = "";
                    bCONVEYOR_T = false;
                    Conveyor_step++;

                    if (str_cmd_arr[0] == "MAN_IN")
                    {
                        nCmd_Step++;
                        bCmd_run = true;

                        Send_AMC_MSG("SEND", "NONE", CMD_NAME, "NONE", "MAN_IN_END");
                    }

                    break;
                default:
                    break;
            }




        }

        private void NewMethod()
        {
            Conveyor_step++;
        }


        int MAN_OUT_TRAY_CNT = 0;
        DateTime MAN_OUT_bTIME;
        DateTime MAN_OUT_CONVEYOR_STOP_TIME;
        const int MAN_OUT_CONVEYOR_TIME_OUT = 10000;

        private void MAN_OUT_Conveyor_Work()
        {
            switch (Conveyor_step)
            {
                case 1:
                    STOPPER.motor_clear();

                    MAN_OUT_TRAY_CNT = 0;

                    if (INPUT_DATA[0, IN_NUM_TRAY_SENSOR1] == true)
                    {
                        MAN_OUT_TRAY_CNT++;
                    }

                    if (INPUT_DATA[0, IN_NUM_TRAY_SENSOR1_1] == true || INPUT_DATA[0, IN_NUM_TRAY_SENSOR2] == true)
                    {
                        MAN_OUT_TRAY_CNT++;
                    }

                    if (INPUT_DATA[0, IN_NUM_TRAY_SENSOR2_1] == true || INPUT_DATA[0, IN_NUM_TRAY_SENSOR3] == true)
                    {
                        MAN_OUT_TRAY_CNT++;
                    }

                    if (INPUT_DATA[0, IN_NUM_TRAY_SENSOR3_1] == true || INPUT_DATA[0, IN_NUM_TRAY_SENSOR4] == true)
                    {
                        MAN_OUT_TRAY_CNT++;
                    }

                    //MOD_Direct_MOVE(LINEAR_CENTER_POSITION);


                    if (MAN_OUT_TRAY_CNT == 0)
                    {
                        Conveyor_step = 7;
                    }
                    else
                    {
                        Insert_System_Log("MAN_OUT start");
                        Conveyor_step++;
                    }

                    break;
                case 2:
                    STOPPER.MOTOR_OFF(STOPPER1_MOTOR_NUM);
                    STOPPER.MOTOR_OFF(STOPPER2_MOTOR_NUM);
                    STOPPER.MOTOR_OFF(STOPPER3_MOTOR_NUM);

                    STOPPER.MOTOR_ON(STOPPER4_MOTOR_NUM);
                    STOPPER.MOTOR_ON(STOPPER5_MOTOR_NUM);

                    Conveyor_step++;
                    break;
                case 3:
                    if (INPUT_DATA[0, IN_NUM_MAN_OUT_SW] == true || INPUT_DATA[0, IN_NUM_MAN_STOP_SENSOR1] == true)
                    {
                        Insert_System_Log("MAN_OUT manual switch push");
                        MAN_OUT_bTIME = DateTime.Now;
                        Conveyor_CCW_RUN();
                        Conveyor_step++;
                    }

                    break;
                case 4:
                    if (INPUT_DATA[0, IN_NUM_TRANSFER_SENSOR] == true)
                    {
                        Insert_System_Log("MAN_OUT transfer sensor detact");
                        MAN_OUT_CONVEYOR_STOP_TIME = DateTime.Now;
                        Conveyor_step++;
                    }
                    break;
                case 5:
                    if (INPUT_DATA[0, IN_NUM_MAN_STOP_SENSOR1] == true || (DateTime.Now - MAN_OUT_CONVEYOR_STOP_TIME).TotalMilliseconds >= 1000)
                    {
                        Conveyor_STOP();
                        Conveyor_step++;
                        Insert_System_Log("MAN_OUT out stop sensor detact");
                    }
                    else if ((DateTime.Now - MAN_OUT_bTIME).TotalMilliseconds > MAN_OUT_CONVEYOR_TIME_OUT)
                    {
                        Conveyor_STOP();
                        Insert_System_Log("MAN_OUT out Conveyor Time Out");
                        Insert_ERR((int)ERR_Q.ERR_CODE.MAN_OUT_CONVEYOR_TIME_OUT);
                    }

                    break;
                case 6:
                    if (INPUT_DATA[0, IN_NUM_MAN_STOP_SENSOR2] == false && INPUT_DATA[0, IN_NUM_MAN_STOP_SENSOR1] == false && INPUT_DATA[0, IN_NUM_TRANSFER_SENSOR] == false)
                    {
                        Conveyor_step++;
                        Insert_System_Log("MAN_OUT tray out complete");
                    }

                    break;
                case 7:
                    if (INPUT_DATA[0, IN_NUM_MAN_STOP_SENSOR1] == true || INPUT_DATA[0, IN_NUM_TRANSFER_SENSOR] == true || INPUT_DATA[0, IN_NUM_TRAY_SENSOR1] == true || INPUT_DATA[0, IN_NUM_TRAY_SENSOR1_1] == true ||
                        INPUT_DATA[0, IN_NUM_TRAY_SENSOR2] == true || INPUT_DATA[0, IN_NUM_TRAY_SENSOR2_1] == true || INPUT_DATA[0, IN_NUM_TRAY_SENSOR3] == true || INPUT_DATA[0, IN_NUM_TRAY_SENSOR3_1] == true ||
                        INPUT_DATA[0, IN_NUM_TRAY_SENSOR4] == true)
                    {
                        Insert_System_Log("MAN_OUT tray detact retry manual out");
                        Conveyor_step = 2;
                        MAN_OUT_TRAY_CNT--;
                    }
                    else if (INPUT_DATA[0, IN_NUM_MAN_STOP_SENSOR1] == false && INPUT_DATA[0, IN_NUM_TRANSFER_SENSOR] == false && INPUT_DATA[0, IN_NUM_TRAY_SENSOR1] == false && INPUT_DATA[0, IN_NUM_TRAY_SENSOR1_1] == false &&
                        INPUT_DATA[0, IN_NUM_TRAY_SENSOR2] == false && INPUT_DATA[0, IN_NUM_TRAY_SENSOR2_1] == false && INPUT_DATA[0, IN_NUM_TRAY_SENSOR3] == false && INPUT_DATA[0, IN_NUM_TRAY_SENSOR3_1] == false &&
                        INPUT_DATA[0, IN_NUM_TRAY_SENSOR4] == false)
                    {
                        STOPPER.MOTOR_OFF(STOPPER4_MOTOR_NUM);
                        STOPPER.MOTOR_OFF(STOPPER5_MOTOR_NUM);

                        Conveyor_MODE = "";
                        bCONVEYOR_T = false;
                        Conveyor_step = 0;

                        Insert_System_Log("MAN_OUT end");

                        if (str_cmd_arr[0] == "MAN_OUT")
                        {
                            nCmd_Step++;
                            bCmd_run = true;

                            Send_AMC_MSG("SEND", "NONE", CMD_NAME, "NONE", "MAN_OUT_END");
                        }
                    }

                    break;
                default:
                    break;
            }
        }


        int Tray_count = 0;
        /// <summary>
        /// 자동으로 AMC에서 STB로 이동 할때
        /// </summary>
        /// 
        bool transfer_on;
        DateTime now_time, now_time1;
        int nloopcnt = 0;

        const int stop_time = 4000;
        const int stop_cnt = 200;

        private void AUTO_OUT_Conveyor_Work()
        {
            tb_LDS2.Text = nloopcnt.ToString();

            switch (Conveyor_step)
            {
                case 0:
                    STOPPER.motor_clear();
                    transfer_on = false;
                    Conveyor_step++;
                    Insert_System_Log("AUTO_OUT start");
                    break;
                case 1:
                    Tray_count = 0;
                    ALL_Stopper_down();

                    if (INPUT_DATA[0, IN_NUM_TRAY_SENSOR1] == true)
                    {
                        Tray_count++;
                    }

                    if (INPUT_DATA[0, IN_NUM_TRAY_SENSOR1_1] == true || INPUT_DATA[0, IN_NUM_TRAY_SENSOR2] == true)
                    {
                        Tray_count++;
                    }

                    if (INPUT_DATA[0, IN_NUM_TRAY_SENSOR2_1] == true || INPUT_DATA[0, IN_NUM_TRAY_SENSOR3] == true)
                    {
                        Tray_count++;
                    }

                    if (INPUT_DATA[0, IN_NUM_TRAY_SENSOR3_1] == true || INPUT_DATA[0, IN_NUM_TRAY_SENSOR4] == true)
                    {
                        Tray_count++;
                    }

                    Conveyor_step++;
                    AMC_JOB.Set_JOB_ST("AUTO_OUT START conveyor have " + Tray_count.ToString() + " trays");
                    break;
                case 2:
                    //AMC_JOB.Set_JOB_ST("AUTO_OUT_STOPER_DN");

                    //if ((Where_Is_Tray()/10) == 1)
                    //    Stopper4_DN();
                    //else

                    Conveyor_step++;
                    break;
                case 3:
                    if (Wait_Run_Signal())
                    {
                        in_job_finish = false;
                        Conveyor_RUN_ST = false;
                        Conveyor_CCW_RUN();
                        Send_AMC_MSG("SEND", STB_NAME, CMD_NAME, GOAL_NAME, "CONVEYOR_RUN_OK");
                        Conveyor_step++;
                    }

                    //AMC_JOB.Set_JOB_ST("AUTO_OUT_CONVEYOR_RUN");
                    break;
                case 4:
                    if (INPUT_DATA[0, IN_NUM_LIGHT_CURTAIN] == true)
                    {
                        Insert_System_Log("AUTO OUT light curtain detact");
                        Set_time_over_param(Err_chk_nCONVEYOR_OUT, Err_chk_sCONVEYOR_OUT);
                        now_time = DateTime.Now;
                        Conveyor_step++;
                    }
                    break;
                case 5:
                    now_time1 = DateTime.Now;
                    nloopcnt++;
                    if (nloopcnt >= stop_cnt)
                    {
                        transfer_on = false;
                        nloopcnt = 0;

                        Conveyor_STOP();

                        Tray_count -= 1;

                        if (Tray_count <= 0)
                        {
                            Conveyor_step = 15;
                            Insert_System_Log("AUTO OUT conveyor empty");
                        }
                        else
                        {
                            Conveyor_step++;
                            Insert_System_Log("AUTO OUT conveyor have " + Tray_count.ToString() + " trays");
                        }
                    }
                    break;

                case 6:
                    if (Wait_Run_Signal() && in_job_finish == true)
                    {
                        in_job_finish = false;
                        Conveyor_RUN_ST = false;
                        Conveyor_CCW_RUN();
                        Send_AMC_MSG("SEND", STB_NAME, CMD_NAME, GOAL_NAME, "CONVEYOR_RUN_OK");
                        Conveyor_step++;
                    }

                    //AMC_JOB.Set_JOB_ST("AUTO_OUT_CONVEYOR_RUN");
                    break;
                case 7:
                    if (INPUT_DATA[0, IN_NUM_LIGHT_CURTAIN] == true)
                    {
                        Insert_System_Log("AUTO OUT light curtain detact");
                        Set_time_over_param(Err_chk_nCONVEYOR_OUT, Err_chk_sCONVEYOR_OUT);
                        now_time = DateTime.Now;
                        Conveyor_step++;
                    }
                    break;
                case 8:
                    now_time1 = DateTime.Now;
                    nloopcnt++;

                    if (nloopcnt >= stop_cnt)
                    {
                        transfer_on = false;
                        nloopcnt = 0;

                        Conveyor_STOP();

                        Tray_count -= 1;

                        if (Tray_count <= 0)
                        {
                            Conveyor_step = 15;
                            Insert_System_Log("AUTO OUT conveyor empty");
                        }
                        else
                        {
                            Conveyor_step++;
                            Insert_System_Log("AUTO OUT conveyor have " + Tray_count.ToString() + " trays");
                        }
                    }
                    break;
                case 9:
                    if (Wait_Run_Signal() && in_job_finish == true)
                    {
                        in_job_finish = false;
                        Conveyor_RUN_ST = false;
                        Conveyor_CCW_RUN();
                        Send_AMC_MSG("SEND", STB_NAME, CMD_NAME, GOAL_NAME, "CONVEYOR_RUN_OK");
                        Conveyor_step++;
                    }

                    //AMC_JOB.Set_JOB_ST("AUTO_OUT_CONVEYOR_RUN");
                    break;
                case 10:
                    if (INPUT_DATA[0, IN_NUM_LIGHT_CURTAIN] == true)
                    {
                        Insert_System_Log("AUTO OUT light curtain detact");
                        Set_time_over_param(Err_chk_nCONVEYOR_OUT, Err_chk_sCONVEYOR_OUT);
                        now_time = DateTime.Now;
                        Conveyor_step++;
                    }
                    break;
                case 11:
                    now_time1 = DateTime.Now;
                    nloopcnt++;

                    if (nloopcnt >= stop_cnt)
                    {
                        transfer_on = false;
                        nloopcnt = 0;

                        Conveyor_STOP();

                        Tray_count -= 1;

                        if (Tray_count <= 0)
                        {
                            Conveyor_step = 15;
                            Insert_System_Log("AUTO OUT conveyor empty");
                        }
                        else
                        {
                            Conveyor_step++;
                            Insert_System_Log("AUTO OUT conveyor have " + Tray_count.ToString() + " trays");
                        }
                    }
                    break;
                case 12:
                    if (Wait_Run_Signal() && in_job_finish == true)
                    {
                        in_job_finish = false;
                        Conveyor_RUN_ST = false;
                        Conveyor_CCW_RUN();
                        Send_AMC_MSG("SEND", STB_NAME, CMD_NAME, GOAL_NAME, "CONVEYOR_RUN_OK");
                        Conveyor_step++;
                    }

                    //AMC_JOB.Set_JOB_ST("AUTO_OUT_CONVEYOR_RUN");
                    break;
                case 13:
                    if (INPUT_DATA[0, IN_NUM_LIGHT_CURTAIN] == true)
                    {
                        Set_time_over_param(Err_chk_nCONVEYOR_OUT, Err_chk_sCONVEYOR_OUT);
                        now_time = DateTime.Now;
                        Conveyor_step++;
                    }
                    break;
                case 14:
                    now_time1 = DateTime.Now;
                    nloopcnt++;

                    if (nloopcnt >= stop_cnt)
                    {
                        transfer_on = false;
                        nloopcnt = 0;

                        Conveyor_STOP();

                        Tray_count -= 1;

                        if (Tray_count <= 0)
                        {
                            Conveyor_step = 15;
                            Insert_System_Log("AUTO OUT conveyor empty");
                        }
                        else
                        {
                            Conveyor_step++;
                            Insert_System_Log("AUTO OUT conveyor have " + Tray_count.ToString() + " trays");
                        }
                    }
                    break;
                case 15:

                    if (in_job_finish == true)
                    {
                        bCONVEYOR_T = false;
                        in_job_finish = false;

                        if (bg_Conveyor.IsBusy == false)
                        {
                            bg_Conveyor.CancelAsync();
                        }
                    }
                    break;

                default:
                    break;
            }
        }

        private void ALL_Stopper_down()
        {
            STOPPER.MOTOR_OFF(STOPPER1_MOTOR_NUM);
            STOPPER.MOTOR_OFF(STOPPER2_MOTOR_NUM);
            STOPPER.MOTOR_OFF(STOPPER3_MOTOR_NUM);
            STOPPER.MOTOR_OFF(STOPPER4_MOTOR_NUM);
            STOPPER.MOTOR_OFF(STOPPER5_MOTOR_NUM);

        }

        /// <summary>
        /// Tray 있으면 true
        /// </summary>
        /// <returns></returns>
        private bool Is_Tray_Clear()
        {
            return NSensor1() || NSensor1_1() || NSensor2() || NSensor2_1() || NSensor3() || NSensor3_1() || NSensor4() || NSensor4_1();
        }


        /// <summary>
        /// 1234
        /// </summary>
        /// <returns></returns>
        private int Where_Is_Tray()
        {
            string tray = "";

            if (NSensor1())
            {
                tray = "1";
            }
            else
            {
                tray = "0";
            }

            if (NSensor2())
            {
                tray += "1";
            }
            else
            {
                tray += "0";
            }

            if (NSensor3())
            {
                tray += "1";
            }
            else
            {
                tray += "0";
            }

            if (NSensor4())
            {
                tray += "1";
            }
            else
            {
                tray += "0";
            }

            return int.Parse(tray);
        }

        private void Conveyor_CW_RUN()
        {
            //System.Threading.Thread.Sleep(10);
            //IO_modbusMaster.WriteSingleCoil(DO_BOARD_DEV_NUM[0], DO_ADDRESS + OUT_NUM_CONVEYOR_DIRECTION, false);
            //System.Threading.Thread.Sleep(10);
            //IO_modbusMaster.WriteSingleCoil(DO_BOARD_DEV_NUM[0], DO_ADDRESS + OUT_NUM_CONVEYOR_RUN, true);
            //System.Threading.Thread.Sleep(10);

            OUTPUT_DATA[0, OUT_NUM_CONVEYOR_DIRECTION] = false;
            OUTPUT_DATA[0, OUT_NUM_CONVEYOR_RUN] = true;

            Insert_System_Log("Conveyor CW run");

        }

        private void Conveyor_CCW_RUN()
        {
            //System.Threading.Thread.Sleep(10);
            //IO_modbusMaster.WriteSingleCoil(DO_BOARD_DEV_NUM[0], DO_ADDRESS + OUT_NUM_CONVEYOR_DIRECTION, true);
            //System.Threading.Thread.Sleep(10);
            //IO_modbusMaster.WriteSingleCoil(DO_BOARD_DEV_NUM[0], DO_ADDRESS + OUT_NUM_CONVEYOR_RUN, true);
            //System.Threading.Thread.Sleep(10);

            OUTPUT_DATA[0, OUT_NUM_CONVEYOR_DIRECTION] = true;
            OUTPUT_DATA[0, OUT_NUM_CONVEYOR_RUN] = true;

            Insert_System_Log("Conveyor CCW run");
        }

        private void Conveyor_STOP()
        {
            if (cb_sim.Checked == false)
            {
                //System.Threading.Thread.Sleep(10);
                //IO_modbusMaster.WriteSingleCoil(DO_BOARD_DEV_NUM[0], DO_ADDRESS + OUT_NUM_CONVEYOR_RUN, false);
                //System.Threading.Thread.Sleep(10);
                //IO_modbusMaster.WriteSingleCoil(DO_BOARD_DEV_NUM[0], DO_ADDRESS + OUT_NUM_CONVEYOR_DIRECTION, false);
                //System.Threading.Thread.Sleep(10);

                OUTPUT_DATA[0, OUT_NUM_CONVEYOR_RUN] = false;
                OUTPUT_DATA[0, OUT_NUM_CONVEYOR_DIRECTION] = false;
            }
            else
            {
                bSIM_T = false;

                bg_SIM.CancelAsync();

                OUTPUT_DATA[0, OUT_NUM_CONVEYOR_RUN] = false;
                OUTPUT_DATA[0, OUT_NUM_CONVEYOR_DIRECTION] = false;
            }

            Insert_System_Log("Conveyor stop");

            //LD[0].LD_ST.LD_ST = "CONVEYOR_STOP";
            //Send_AMC_MSG("SEND", STB_NAME, CMD_NAME, GOAL_NAME, "CONVEYOR_STOP");

        }


        private bool IS_Rising(int data_num)
        {
            return (INPUT_DATA[0, data_num] == true && INPUT_DATA[1, data_num] == false) == true ? true : false;
        }

        private bool IS_falling(int data_num)
        {
            return (INPUT_DATA[0, data_num] == false && INPUT_DATA[1, data_num] == true) == true ? true : false;
        }

        private int First_TRAY()
        {
            int a = 0;

            if (INPUT_DATA[0, IN_NUM_TRAY_SENSOR1] == true)
            {
                a = 1;
            }
            else if (INPUT_DATA[0, IN_NUM_TRAY_SENSOR2] == true)
            {
                a = 2;
            }
            else if (INPUT_DATA[0, IN_NUM_TRAY_SENSOR3] == true)
            {
                a = 3;
            }
            else if (INPUT_DATA[0, IN_NUM_TRAY_SENSOR4] == true)
            {
                a = 4;
            }
            else if (INPUT_DATA[0, IN_NUM_TRAY_SENSOR1_1] == true)
            {
                a = 11;
            }
            else if (INPUT_DATA[0, IN_NUM_TRAY_SENSOR2_1] == true)
            {
                a = 21;
            }
            else if (INPUT_DATA[0, IN_NUM_TRAY_SENSOR3_1] == true)
            {
                a = 31;
            }
            else if (INPUT_DATA[0, IN_NUM_TRAY_SENSOR4_1] == true)
            {
                a = 41;
            }

            return a;
        }


        //private void MAN_IN_MOV_4_1()
        //{
        //    //IO_modbusMaster.WriteSingleCoil(DO_BOARD_DEV_NUM[0], DO_ADDRESS + OUT_NUM_STOPER1, true);
        //    //IO_modbusMaster.WriteSingleCoil(DO_BOARD_DEV_NUM[0], DO_ADDRESS + OUT_NUM_STOPER2, true);
        //    //IO_modbusMaster.WriteSingleCoil(DO_BOARD_DEV_NUM[0], DO_ADDRESS + OUT_NUM_STOPER3, true);
        //    //IO_modbusMaster.WriteSingleCoil(DO_BOARD_DEV_NUM[0], DO_ADDRESS + OUT_NUM_STOPER4, true);

        //    STOPPER.MOTOR_ON(3);

        //    if (INPUT_DATA[0, IN_NUM_TRAY_SENSOR4] == true && INPUT_DATA[0, IN_NUM_TRAY_SENSOR4_1] == false)
        //    {
        //        STOP_Conveyor();
        //        Conveyor_step++;
        //    }
        //    else
        //    {
        //        RUN_Conveyor_CW();
        //    }
        //}




        private void Hold()
        {
            //if()
        }


        private void AUTO_IN_Stopper_UP_DOWN()
        {
            if (IS_Rising(IN_NUM_TRAY_SENSOR4) == true)
            {
                if (grip_step == 0)
                {
                    grip_step = 1;
                    Conveyor_STOP();

                    if (cb_sim.Checked == true)
                    {
                        bSIM_T = false;
                        if (bg_SIM.IsBusy == true)
                        {
                            bg_SIM.CancelAsync();
                        }

                        INPUT_DATA[1, IN_NUM_TRAY_SENSOR4] = INPUT_DATA[0, IN_NUM_TRAY_SENSOR4];
                    }
                }
                else if (grip_step == 4)
                {
                    if (NSensor4() == true)
                    {
                        LD[0].LD_ST.LD_ST = "CONVEYOR_STOP";
                        Send_AMC_MSG("SEND", STB_NAME, CMD_NAME, GOAL_NAME, "CONVEYOR_STOP");

                        grip_step = 0;
                        //Stopper3_UP();
                        Conveyor_STOP();

                        if (cb_sim.Checked == true)
                        {
                            bSIM_T = false;
                            if (bg_SIM.IsBusy == true)
                            {
                                bg_SIM.CancelAsync();
                            }
                        }
                    }
                }
            }


        }

        private void AUTO_OUT_STOPER_UPDN()
        {
            if (Tray_count == 0)
            {
                Conveyor_step++;
                Conveyor_STOP();
            }
            else
            {
                if (IS_falling(IN_NUM_TRAY_SENSOR4_1))
                {
                    //Tray_count--;
                    //Conveyor_step = 3;
                    //Conveyor_STOP();
                }
            }


            //if (NSensor3() == true && NSensor3_1() == false)
            //    Stopper3_UP();

            //if (NSensor2() == true && NSensor2_1() == false)
            //    Stopper2_UP();

            //if (NSensor1() == true && NSensor1_1() == false)
            //    Stopper1_UP();

            //if (INPUT_DATA[0, IN_NUM_TRAY_SENSOR3] == true)
            //{
            //    if (OUTPUT_DATA[0, OUT_NUM_STOPER3] == true)
            //        stop3 = true;
            //}
            //else stop3 = true;


            //if (INPUT_DATA[0, IN_NUM_TRAY_SENSOR2] == true)
            //{
            //    if (OUTPUT_DATA[0, OUT_NUM_STOPER2] == true)
            //        stop2 = true;
            //}
            //else stop2 = true;

            //if (INPUT_DATA[0, IN_NUM_TRAY_SENSOR1] == true)
            //{
            //    if (OUTPUT_DATA[0, OUT_NUM_STOPER1] == true)
            //        stop1 = true;
            //}
            //else stop1 = true;

            //if (stop1 && stop2 && stop3)
            //    Conveyor_step++;

        }
        int ng_cnt = 0;
        private void AUTO_IN_Grip_Tray()
        {
            switch (grip_step)
            {
                case 1:     // Grip Start
                    if (NGriper_LLimit() == false && NSensor4() == true)
                    {
                        Send_AMC_MSG("SEND", STB_NAME, CMD_NAME, GOAL_NAME, "GRIPPER_CW_RUN");
                        Griper_CW_Run();
                        grip_step++;
                    }
                    break;
                case 2:     // Grip OK
                    if (cb_sim.Checked == false)
                    {
                        if (NGriper_RLimit() == true)
                        {
                            Send_AMC_MSG("SEND", STB_NAME, CMD_NAME, GOAL_NAME, "GRIPPER_CCW_RUN");
                            Griper_CCW_Run();
                            grip_step++;
                        }
                    }
                    else
                    {
                        ng_cnt++;

                        if (ng_cnt >= 50)
                        {
                            ng_cnt = 0;
                            Send_AMC_MSG("SEND", STB_NAME, CMD_NAME, GOAL_NAME, "GRIPPER_CCW_RUN");
                            Griper_CCW_Run();
                            grip_step++;
                        }
                    }
                    break;
                case 3:     // Grip Complet
                    if (cb_sim.Checked == false)
                    {
                        if (NGriper_LLimit() == true)
                        {
                            Send_AMC_MSG("SEND", STB_NAME, CMD_NAME, GOAL_NAME, "GRIPPER_STOP");

                            Griper_Stop();
                            System.Threading.Thread.Sleep(1000);
                            //Conveyor_RUN();
                            grip_step++;
                        }
                    }
                    else
                    {
                        ng_cnt++;

                        if (ng_cnt >= 100)
                        {
                            Send_AMC_MSG("SEND", STB_NAME, CMD_NAME, GOAL_NAME, "GRIPPER_STOP");
                            LD[0].LD_ST.LD_ST = "FINISH";
                            Send_AMC_MSG("SEND", STB_NAME, CMD_NAME, GOAL_NAME, LD[0].LD_ST.LD_ST);
                            if (cb_sim.Checked == true)
                            {
                                if (bg_SIM.IsBusy == true)
                                {
                                    bg_SIM.CancelAsync();
                                }
                            }

                            Griper_Stop();
                            System.Threading.Thread.Sleep(1000);
                            //Conveyor_RUN();
                            grip_step++;
                            ng_cnt = 0;
                        }
                    }
                    break;
                case 4:

                    break;
                default:
                    break;
            }
        }


        int grip_step = 0;

        private void Grip_Tray()
        {
            switch (grip_step)
            {
                case 1:     // Grip Start
                    if (NGriper_RLimit() == false)
                    {

                        //IO_modbusMaster.WriteSingleCoil(DO_BOARD_DEV_NUM[0], DO_ADDRESS + OUT_NUM_GRIPER_DIRECTION, false);
                        //System.Threading.Thread.Sleep(10);
                        //IO_modbusMaster.WriteSingleCoil(DO_BOARD_DEV_NUM[0], DO_ADDRESS + OUT_NUM_GRIPER_RUN, true);
                        //System.Threading.Thread.Sleep(10);
                        OUTPUT_DATA[0, OUT_NUM_CONVEYOR_DIRECTION] = false;
                        OUTPUT_DATA[0, OUT_NUM_GRIPER_RUN] = true;
                        grip_step++;
                    }
                    break;
                case 2:     // Grip OK
                    if (NGriper_RLimit() == true)
                    {
                        //IO_modbusMaster.WriteSingleCoil(DO_BOARD_DEV_NUM[0], DO_ADDRESS + OUT_NUM_GRIPER_DIRECTION, true);
                        //System.Threading.Thread.Sleep(10);
                        //IO_modbusMaster.WriteSingleCoil(DO_BOARD_DEV_NUM[0], DO_ADDRESS + OUT_NUM_GRIPER_RUN, true);
                        //System.Threading.Thread.Sleep(10);

                        OUTPUT_DATA[0, OUT_NUM_CONVEYOR_DIRECTION] = true;
                        OUTPUT_DATA[0, OUT_NUM_GRIPER_RUN] = true;
                        grip_step++;
                    }
                    break;
                case 3:     // Grip Complet
                    if (NGriper_LLimit() == true)
                    {
                        //IO_modbusMaster.WriteSingleCoil(DO_BOARD_DEV_NUM[0], DO_ADDRESS + OUT_NUM_GRIPER_DIRECTION, false);
                        //System.Threading.Thread.Sleep(10);
                        //IO_modbusMaster.WriteSingleCoil(DO_BOARD_DEV_NUM[0], DO_ADDRESS + OUT_NUM_GRIPER_RUN, false);
                        //System.Threading.Thread.Sleep(10);


                        OUTPUT_DATA[0, OUT_NUM_CONVEYOR_DIRECTION] = false;
                        OUTPUT_DATA[0, OUT_NUM_GRIPER_RUN] = false;
                        grip_step++;
                    }
                    break;
                case 4:
                    Conveyor_step++;
                    grip_step = 0;
                    break;
                default:
                    break;
            }
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Set_Off_DO();
            bMODBUS_T = false;
            Properties.Settings.Default.Save();

            System.Threading.Thread.Sleep(200);
            if (IO_SerialPort != null)
            {
                if (IO_SerialPort.IsOpen == true)
                {
                    IO_SerialPort.Close();
                }

                IO_SerialPort.Dispose();
            }

            if (LD[0].LD_Client.Connected == true)
            {
                LD[0].LD_Client.Disconnect(false);
            }

            LD[0].LD_Client.Dispose();


        }

        //private void STOP_Conveyor()
        //{
        //    IO_modbusMaster.WriteSingleCoil(DO_BOARD_DEV_NUM[0], DO_ADDRESS + OUT_NUM_CONVEYOR_DIRECTION, false);
        //    System.Threading.Thread.Sleep(10);
        //    IO_modbusMaster.WriteSingleCoil(DO_BOARD_DEV_NUM[0], DO_ADDRESS + OUT_NUM_CONVEYOR_RUN, false);
        //    System.Threading.Thread.Sleep(10);
        //}

        //private void RUN_Conveyor_CW()
        //{
        //    IO_modbusMaster.WriteSingleCoil(DO_BOARD_DEV_NUM[0], DO_ADDRESS + OUT_NUM_CONVEYOR_DIRECTION, false);
        //    System.Threading.Thread.Sleep(10);
        //    IO_modbusMaster.WriteSingleCoil(DO_BOARD_DEV_NUM[0], DO_ADDRESS + OUT_NUM_CONVEYOR_RUN, true);
        //    System.Threading.Thread.Sleep(10);
        //}

        //private void RUN_Conveyor_CCW()
        //{
        //    IO_modbusMaster.WriteSingleCoil(DO_BOARD_DEV_NUM[0], DO_ADDRESS + OUT_NUM_CONVEYOR_DIRECTION, true);
        //    System.Threading.Thread.Sleep(10);
        //    IO_modbusMaster.WriteSingleCoil(DO_BOARD_DEV_NUM[0], DO_ADDRESS + OUT_NUM_CONVEYOR_RUN, true);
        //    System.Threading.Thread.Sleep(10);
        //}



        private void MAN_STOPPER_ON()
        {
            if (cb_sim.Checked == false)
            {
                STOPPER.MOTOR_ON(STOPPER4_MOTOR_NUM);
                STOPPER.MOTOR_ON(STOPPER5_MOTOR_NUM);

                DIGITAL_DATA[DD_NUM_MAN_STOPPER_L] = true;
                DIGITAL_DATA[DD_NUM_MAN_STOPPER_R] = true;
            }
            else
            {
                DIGITAL_DATA[DD_NUM_MAN_STOPPER_L] = true;
                DIGITAL_DATA[DD_NUM_MAN_STOPPER_R] = true;
            }
        }


        private void MAN_STOPPER_OFF()
        {
            if (cb_sim.Checked == false)
            {
                STOPPER.MOTOR_OFF(STOPPER4_MOTOR_NUM);
                STOPPER.MOTOR_OFF(STOPPER5_MOTOR_NUM);

                DIGITAL_DATA[DD_NUM_MAN_STOPPER_L] = false;
                DIGITAL_DATA[DD_NUM_MAN_STOPPER_R] = false;
            }
            else
            {
                DIGITAL_DATA[DD_NUM_MAN_STOPPER_L] = false;
                DIGITAL_DATA[DD_NUM_MAN_STOPPER_R] = false;
            }
        }


        private bool NSensor1()
        {
            return INPUT_DATA[0, IN_NUM_TRAY_SENSOR1];
        }

        private bool NSensor2()
        {
            return INPUT_DATA[0, IN_NUM_TRAY_SENSOR2];
        }

        private bool NSensor3()
        {
            return INPUT_DATA[0, IN_NUM_TRAY_SENSOR3];
        }

        private bool NSensor4()
        {
            return INPUT_DATA[0, IN_NUM_TRAY_SENSOR4];
        }

        private bool NSensor1_1()
        {
            return INPUT_DATA[0, IN_NUM_TRAY_SENSOR1_1];
        }

        private bool NSensor2_1()
        {
            return INPUT_DATA[0, IN_NUM_TRAY_SENSOR2_1];
        }

        private bool NSensor3_1()
        {
            return INPUT_DATA[0, IN_NUM_TRAY_SENSOR3_1];
        }

        private bool NSensor4_1()
        {
            return INPUT_DATA[0, IN_NUM_TRAY_SENSOR4_1];
        }

        private bool NSensor_MAN1()
        {
            return INPUT_DATA[0, IN_NUM_MAN_STOP_SENSOR1];
        }

        private bool NSensor_MAN2()
        {
            return INPUT_DATA[0, IN_NUM_MAN_STOP_SENSOR2];
        }


        private bool NLight_Cutain()
        {
            return INPUT_DATA[0, IN_NUM_LIGHT_CURTAIN];
        }

        private bool BSensor1()
        {
            return INPUT_DATA[1, IN_NUM_TRAY_SENSOR1];
        }

        private bool BSensor2()
        {
            return INPUT_DATA[1, IN_NUM_TRAY_SENSOR2];
        }

        private bool BSensor3()
        {
            return INPUT_DATA[1, IN_NUM_TRAY_SENSOR3];
        }

        private bool BSensor4()
        {
            return INPUT_DATA[1, IN_NUM_TRAY_SENSOR4];
        }

        private bool BSensor1_1()
        {
            return INPUT_DATA[1, IN_NUM_TRAY_SENSOR1_1];
        }

        private bool BSensor2_1()
        {
            return INPUT_DATA[1, IN_NUM_TRAY_SENSOR2_1];
        }

        private bool BSensor3_1()
        {
            return INPUT_DATA[1, IN_NUM_TRAY_SENSOR3_1];
        }

        private bool BSensor4_1()
        {
            return INPUT_DATA[1, IN_NUM_TRAY_SENSOR4_1];
        }

        private bool BLight_cutain()
        {
            return INPUT_DATA[1, IN_NUM_LIGHT_CURTAIN];
        }

        public void UnGrip()
        {
            if(INPUT_DATA[0,IN_NUM_GRIPER_OFF_LIMIT] == false)
            {
                Griper_CW_Run();                
            }
            else
            {
                Griper_Stop();
            }
            
        }

        private void Griper_CW_Run()
        {
            if (cb_sim.Checked == false)
            {
                //IO_modbusMaster.WriteSingleCoil(DO_BOARD_DEV_NUM[0], DO_ADDRESS + OUT_NUM_GRIPER_DIRECTION, false);
                //System.Threading.Thread.Sleep(10);
                //IO_modbusMaster.WriteSingleCoil(DO_BOARD_DEV_NUM[0], DO_ADDRESS + OUT_NUM_GRIPER_RUN, true);
                //System.Threading.Thread.Sleep(10);

                OUTPUT_DATA[0, OUT_NUM_GRIPER_DIRECTION] = false;
                OUTPUT_DATA[0, OUT_NUM_GRIPER_RUN] = true;


            }
            else
            {
                OUTPUT_DATA[0, OUT_NUM_GRIPER_DIRECTION] = false;
                OUTPUT_DATA[0, OUT_NUM_GRIPER_RUN] = true;
            }

            Insert_System_Log("Griper CW run");

        }

        private void Griper_CCW_Run()
        {
            if (cb_sim.Checked == false)
            {
                //IO_modbusMaster.WriteSingleCoil(DO_BOARD_DEV_NUM[0], DO_ADDRESS + OUT_NUM_GRIPER_DIRECTION, true);
                //System.Threading.Thread.Sleep(10);
                //IO_modbusMaster.WriteSingleCoil(DO_BOARD_DEV_NUM[0], DO_ADDRESS + OUT_NUM_GRIPER_RUN, true);
                //System.Threading.Thread.Sleep(10);

                OUTPUT_DATA[0, OUT_NUM_GRIPER_DIRECTION] = true;
                OUTPUT_DATA[0, OUT_NUM_GRIPER_RUN] = true;
            }
            else
            {
                OUTPUT_DATA[0, OUT_NUM_GRIPER_DIRECTION] = true;
                OUTPUT_DATA[0, OUT_NUM_GRIPER_RUN] = true;
            }

            Insert_System_Log("Griper CCW run");
        }

        private void Griper_Stop()
        {
            if (cb_sim.Checked == false)
            {
                //IO_modbusMaster.WriteSingleCoil(DO_BOARD_DEV_NUM[0], DO_ADDRESS + OUT_NUM_GRIPER_RUN, false);
                //System.Threading.Thread.Sleep(10);
                //IO_modbusMaster.WriteSingleCoil(DO_BOARD_DEV_NUM[0], DO_ADDRESS + OUT_NUM_GRIPER_DIRECTION, false);
                //System.Threading.Thread.Sleep(10);

                OUTPUT_DATA[0, OUT_NUM_GRIPER_DIRECTION] = false;
                OUTPUT_DATA[0, OUT_NUM_GRIPER_RUN] = false;
            }
            else
            {
                OUTPUT_DATA[0, OUT_NUM_GRIPER_DIRECTION] = false;
                OUTPUT_DATA[0, OUT_NUM_GRIPER_RUN] = false;
            }

            Insert_System_Log("Griper stop");
        }

        private bool NGriper_RLimit()
        {
            return INPUT_DATA[0, IN_NUM_GRIPER_ON_LIMIT];
        }

        private bool NGriper_LLimit()
        {
            return INPUT_DATA[0, IN_NUM_GRIPER_OFF_LIMIT];
        }

        private void Insert_log(string msg)
        {
            try
            {
                if (msg.Length > List_Box_Line_Conut)
                {
                    string[] str_line = new string[10];

                    for (int j = 0; j < (msg.Length / List_Box_Line_Conut) + 1; j++)
                    {
                        str_line[j] = msg.Substring(j * List_Box_Line_Conut, msg.Length > (j * List_Box_Line_Conut) + List_Box_Line_Conut ? List_Box_Line_Conut : msg.Length - (j * List_Box_Line_Conut));
                        lb_log.Items.Insert(lb_log.Items.Count, str_line[j]);
                        lb_log.SetSelected(lb_log.Items.Count - 1, true);
                    }
                }
                else
                {
                    lb_log.Items.Insert(lb_log.Items.Count, msg);
                    lb_log.SetSelected(lb_log.Items.Count - 1, true);
                }

            }
            catch (Exception)
            {
                Insert_log(msg);
            }
        }


        private void AMC_SRV_Parse(string msg)
        {
            string[] cmd_msg = msg.Split(';');
            try
            {            
                for (int j = 0; j < cmd_msg.Length; j++)
                {
                    Insert_log(cmd_msg[j]);

                    cmd_msg[j] = cmd_msg[j].Replace('\n', '\0');
                    cmd_msg[j] = cmd_msg[j].Replace('\r', '\0');
                    cmd_msg[j] = cmd_msg[j].Trim('\0');

                    if (Find_AMC(cmd_msg[j]) == LD[0].LD_NAME)
                    {
                        CMD_NAME = Find_CMD(cmd_msg[j]);
                        STB_NAME = Find_STB(cmd_msg[j]);
                        GOAL_NAME = Find_GOAL(cmd_msg[j]);
                        STATUS = Find_STATUS(cmd_msg[j]);

                        lb_log.Items.Add("RECV: " + cmd_msg[j]);

                        if (CMD_NAME == "CALL")
                        {
                            if (STATUS == "CONNECTED")
                            {
                                if (cb_sim.Checked == false)
                                {
                                    if (INPUT_DATA[0, IN_NUM_MOUTH_SENSOR] == true)
                                    {
                                        bCONECTED = true;
                                        DisEable_timer(Err_chk_sMOUTH);
                                        Send_AMC_MSG("RECV", STB_NAME, CMD_NAME, GOAL_NAME, "CONNECTED_OK");
                                    }
                                    else
                                    {
                                        Set_time_over_param(Err_chk_nMOUTH, Err_chk_sMOUTH);
                                        Send_AMC_MSG("RECV", STB_NAME, CMD_NAME, GOAL_NAME, "NOT_CONNECTED");
                                    }
                                }
                                else
                                {
                                    Send_AMC_MSG("RECV", STB_NAME, CMD_NAME, GOAL_NAME, "CONNECTED_OK");
                                }
                            }
                            else
                            {
                                string[] _buf = STATUS.Split(':');

                                if (_buf[0] == "RECIPE")
                                {
                                    lb_cmd.Items.Clear();

                                    for (int k = 0; k < _buf.Length; k++)
                                    {
                                        if (_buf[k] != "RECIPE")
                                        {
                                            _buf[k].Replace(';', '\0');
                                            lb_cmd.Items.Add(_buf[k]);

                                            if (_buf[k].Contains("GOGOAL") == true)
                                            {
                                                string[] temp = _buf[k].Split(' ');

                                                Monitor.Set_Where2go(temp[1].Substring(0, (temp[1].Length >= 3 ? 3 : temp[1].Length )));
                                            }
                                        }
                                    }

                                    bCmd_run = true;
                                    nCmd_Step = 0;
                                    bbg_CMD_terminator = true;
                                    if (bg_CMD.IsBusy == false)
                                    {
                                        bg_CMD.RunWorkerAsync();
                                    }

                                    Send_AMC_MSG("RECV", STB_NAME, CMD_NAME, GOAL_NAME, "RECIPE_OK");
                                }
                            }
                        }
                        else if (CMD_NAME == "REQUEST")
                        {
                            if (STATUS == "AMC_STATUS")
                            {
                                string temp = "LOC.X=" + LD[0].LD_ST.LD_LOC.LD_X + ",LOC.Y=" + LD[0].LD_ST.LD_LOC.LD_Y + ",LOC.A=" + LD[0].LD_ST.LD_LOC.LD_A +
                                ",TEMPER=" + LD[0].LD_ST.LD_TEMP + ",WIFI=" + LD[0].LD_ST.LD_WIFI + ",VOLT=" + LD[0].LD_ST.LD_VOLT + ",LDS1=" + LD[0].LD_ST.LD_LDS1_DISTANCE +
                                ",LDS2=" + LD[0].LD_ST.LD_LDS2_DISTANCE + ",LD_STATUS=" + LD[0].LD_ST.LD_ST + ",AMC_STATUS=" + "TEST RUN;";

                                Send_AMC_MSG("RECV", STB_NAME, CMD_NAME, GOAL_NAME, temp);
                            }
                            else if (STATUS == "GET_GOALS")
                            {
                                string temp = "";


                                if (LD[0].LD_GOAL.Count > 0)
                                {
                                    for (int m = 0; m < LD[0].LD_GOAL.Count; m++)
                                    {
                                        temp += LD[0].LD_GOAL[m] + "|";

                                        if (temp.Length > 800)
                                        {
                                            temp = temp.Substring(temp.Length - 1, 1) == "|" ? temp.Substring(0, temp.Length - 1) : temp;
                                            Send_AMC_MSG("RECV", STB_NAME, "GOALS", GOAL_NAME, temp);
                                            temp = "";
                                            System.Threading.Thread.Sleep(100);
                                        }
                                    }
                                    temp = temp.Substring(temp.Length - 1, 1) == "|" ? temp.Substring(0, temp.Length - 1) : temp;
                                    Send_AMC_MSG("RECV", STB_NAME, "GOALS", GOAL_NAME, temp.Substring(0, temp.Length - 1));//.Substring(0, temp.Length <=0 ? 0 : temp.Length));
                                }
                                else
                                {
                                    Send_string(LD[0].LD_Client, "getGoals");
                                }
                            }
                            else if (STATUS == "CONNECTED")
                            {
                                if (cb_sim.Checked == false)
                                {
                                    if (cb_sim.Checked == false)
                                    {
                                        if (cb_sim.Checked == false)
                                        {
                                            if (cb_sim.Checked == false)
                                            {
                                                if (INPUT_DATA[0, IN_NUM_MOUTH_SENSOR] == true)
                                                {
                                                    bCONECTED = true;
                                                    DisEable_timer(Err_chk_sMOUTH);
                                                    Send_AMC_MSG("RECV", STB_NAME, CMD_NAME, GOAL_NAME, "CONNECTED");
                                                }
                                                else
                                                {
                                                    Set_time_over_param(Err_chk_nMOUTH, Err_chk_sMOUTH);
                                                    Send_AMC_MSG("RECV", STB_NAME, CMD_NAME, GOAL_NAME, "NOT_CONNECTED");
                                                }
                                            }
                                            else
                                            {
                                                INPUT_DATA[0, IN_NUM_MOUTH_SENSOR] = true;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (STATUS == "DOCK")
                            {
                                Send_string(LD[0].LD_Client, "dock");
                            }
                            else
                            {
                                string[] _buf = STATUS.Split(':');

                                if (_buf[0] == "RECIPE")
                                {
                                    if (bg_CMD.IsBusy == true)
                                    {
                                        lb_cmd.Items.Clear();

                                        for (int k = 0; k < _buf.Length; k++)
                                        {
                                            if (_buf[k] != "RECIPE")
                                            {
                                                _buf[k].Replace(';', '\0');
                                                lb_cmd.Items.Add(_buf[k]);
                                            }
                                        }

                                        bCmd_run = true;
                                        nCmd_Step = 0;
                                        bbg_CMD_terminator = true;

                                        if (bg_CMD.IsBusy == false)
                                        {
                                            bg_CMD.RunWorkerAsync();
                                        }

                                        Send_AMC_MSG("RECV", STB_NAME, CMD_NAME, GOAL_NAME, "RECIPE_OK");
                                    }
                                    else
                                    {
                                        Send_AMC_MSG("RECV", STB_NAME, CMD_NAME, GOAL_NAME, "AMC_RUN");
                                    }
                                }
                            }


                        }
                        else if (CMD_NAME == "JOB_ASSIGN")
                        {
                            //CJOB.stCommand CMD_TEMP = new CJOB.stCommand();

                            //if (msg_buf[0] == "JOB_ID")
                            //    CMD_TEMP.JOB_ID = msg_buf[1];
                            //else if (msg_buf[0] == "JOB_TIME")
                            //    CMD_TEMP.JOB_TIME = DateTime.Parse(msg_buf[1]);
                            //else if (msg_buf[0] == "JOB_SCR")
                            //    CMD_TEMP.JOB_SCR = msg_buf[1];
                            //else if (msg_buf[0] == "JOB_DEST")
                            //    CMD_TEMP.JOB_DEST = msg_buf[1];
                            //else if (msg_buf[0] == "JOB_TYPE")
                            //    CMD_TEMP.JOB_TYPE = msg_buf[1];

                            //AMC_JOB.Insert_CMD(CMD_TEMP);
                        }
                        else if (CMD_NAME == "IN_JOB_START")
                        {
                            if (STATUS == "READY")
                            {
                                if (Is_Tray_Clear() == false)        //Tray가 없다.
                                {
                                    string[] _buf = LD[0].LD_ST.LD_ST.Split(' ');
                                    if (_buf[0] == "Doing" && _buf[1] == "task" && _buf[2] == "wait")    // Doing task wait
                                    {
                                        Send_AMC_MSG("RECV", STB_NAME, CMD_NAME, GOAL_NAME, "READY/NOT_READY");
                                    }
                                    else
                                    {
                                        Send_AMC_MSG("RECV", STB_NAME, CMD_NAME, GOAL_NAME, "NOT_READY/NOT_READY");
                                    }
                                }
                                else
                                {
                                    Send_AMC_MSG("RECV", STB_NAME, CMD_NAME, GOAL_NAME, "=NOT_READY/NOT_READY");
                                }
                            }
                            else if (STATUS == "CONVEYOR_RUN")
                            {
                                Conveyor_RUN_ST = true;
                                //Conveyor_CW_RUN();
                            }
                            else if (STATUS == "CONVEYOR_STOP")
                            {
                                DisEable_timer(Err_chk_sCONVEYOR_OUT);
                                Conveyor_RUN_ST = false;
                                Conveyor_STOP();
                                Send_AMC_MSG("SEND", STB_NAME, CMD_NAME, GOAL_NAME, "CONVEYOR_STOP");
                            }
                            else if (STATUS == "CONTINUE")
                            {
                                Continue_JOB = true;
                            }
                            else if (STATUS == "FINISH")
                            {
                                in_job_finish = true;
                                if (Tray_count <= 0)
                                {
                                    Send_AMC_MSG("SEND", STB_NAME, "IN_JOB_END", GOAL_NAME, "JOB_END");
                                }
                                else
                                {
                                    Send_AMC_MSG("SEND", STB_NAME, "REQUEST", GOAL_NAME, "CONNECTED_OK");
                                }
                            }
                        }
                        else if (CMD_NAME == "OUT_JOB_START")
                        {
                            if (STATUS == "CONVEYOR_RUN")
                            {
                                if (cb_sim.Checked == false)
                                {
                                    string[] _buf = LD[0].LD_ST.LD_ST.Split(' ');
                                    if (_buf[0] == "Doing" && _buf[1] == "task" && _buf[2] == "wait")    // Doing task wait
                                    {
                                        Conveyor_RUN_ST = true;
                                    }
                                    else
                                    {
                                        Send_AMC_MSG("RECV", STB_NAME, CMD_NAME, GOAL_NAME, "LD_IS_NOT_WAIT_MODE");
                                    }
                                }
                                else
                                {
                                    Conveyor_RUN_ST = true;
                                }
                            }
                            else if (STATUS == "CONVEYOR_STOP")
                            {
                                Conveyor_RUN_ST = false;
                                Conveyor_STOP();
                                Send_AMC_MSG("SEND", STB_NAME, CMD_NAME, GOAL_NAME, "CONVEYOR_STOP");
                            }
                            else if (STATUS == "TRAY_OUT_COMP")
                            {
                                Conveyor_RUN_ST = false;
                            }
                        }
                        else if (CMD_NAME == "OUT_JOB_END")
                        {
                            if (STATUS == "JOB_END")
                            {
                                OUT_JOB_END_ST = true;
                                bCONVEYOR_T = false;
                                Conveyor_step = 0;
                                Conveyor_STOP();
                            }
                        }
                        else if (CMD_NAME == "IN_JOB_END")
                        {
                            if (STATUS == "JOB_END")
                            {
                                nCmd_Step++;
                                OUT_JOB_END_ST = true;
                                bCONVEYOR_T = false;
                            }
                        }
                        else if (CMD_NAME == "RFID_FIND")
                        {
                            if (STATUS == "EMPTY")
                            {
                                Send_string(LD[0].LD_Client, "say \"Unregistered ID\"");
                            }
                            else
                            {
                                Send_string(LD[0].LD_Client, "say \"Welcome to " + STATUS + "\"");
                            }
                        }
                        else if (CMD_NAME == "MSG")
                        {
                            Send_string(LD[0].LD_Client, "say " + STATUS);
                        }
                        else if (CMD_NAME == "MAN_OUT")
                        {
                        }

                        string[] st_temp = STATUS.Split('_');
                        if (st_temp.Length > 2)
                        {
                            if (st_temp[1] == "ERROR")
                            {
                                Insert_ERR((int)ERR_Q.ERR_CODE.AMC_SERVER_SEND_STB_ERROR);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Insert_ERR_Log(ex);
            }
        }

        public void Read_Setting_Text()
        {
            try
            {
                string Setting_data = System.IO.File.ReadAllText(System.Environment.CurrentDirectory + "\\Setting\\Setting.txt");

                string[] str_data = Setting_data.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                string[] LD_ip = new string[10];
                int[] LD_port = new int[10];
                string[] LD_name = new string[10];


                for (int i = 0; i < str_data.Length - 1; i++)
                {
                    if (str_data[i] == "[SERVER]")
                    {
                        string[] str_temp;
                        for (int j = 0; j < 4; j++)
                        {
                            str_temp = str_data[i + j + 1].Split('=');

                            if (str_temp[0] == "NAME")
                            {
                                AMC_SRV.SRV_NAME = str_temp[1];
                            }
                            else if (str_temp[0] == "IP")
                            {
                                AMC_SRV.SRV_IP = str_temp[1];
                            }
                            else if (str_temp[0] == "PORT")
                            {
                                AMC_SRV.SRV_PORT = str_temp[1];
                            }
                        }
                        AMC_SRV.SRV_Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    }
                    else if (str_data[i] == "[AMC]")
                    {
                        String[] str_temp;
                        LD_Dev LD_Temp = new LD_Dev();

                        for (int j = 0; j < 9; j++)
                        {
                            str_temp = str_data[j + i + 1].Split('=');
                            if (str_temp[0] == "NAME")
                            {
                                LD_Temp.LD_NAME = str_temp[1];
                            }
                            else if(str_temp[0] == "TYPE")
                            {
                                LD_Temp.LD_TYPE = str_temp[1];
                            }
                            else if (str_temp[0] == "IP")
                            {
                                LD_Temp.LD_IP = IPAddress.Parse(str_temp[1]);
                            }
                            else if (str_temp[0] == "PORT")
                            {
                                LD_Temp.LD_PORT = int.Parse(str_temp[1]);
                            }
                            else if (str_temp[0] == "ID")
                            {
                                LD_Temp.LD_ID = str_temp[1];
                            }
                            else if (str_temp[0] == "PW")
                            {
                                LD_Temp.LD_PW = str_temp[1];
                            }
                            else if(str_temp[0] == "ZIGBEE_ID")
                            {
                                LD_Temp.ZIGBEE_NUM = str_temp[1];                                
                            }
                            else if(str_temp[0] == "ZIGBEE_PORT")
                            {
                                LD_Temp.ZIGBEE_PORT = str_temp[1];
                                
                            }
                            else if(str_temp[0] == "BATT_HIGH_LIMIT")
                            {
                                LD_Temp.BATT_HIGH_LIMIT = int.Parse(str_temp[1]);
                            }
                        }
                        LD[0] = LD_Temp;
                        LD[0].LD_Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        LD[0].LD_GOAL = new List<string>();

                    }
                    else if (str_data[i] == "[RFID]")
                    {
                        stRFID temp_rfid = new stRFID();
                        String[] str_temp;

                        for (int j = 0; j < 2; j++)
                        {
                            str_temp = str_data[j + i + 1].Split('=');

                            if (str_temp[0] == "IP")
                            {
                                temp_rfid.IP = IPAddress.Parse(str_temp[1]);
                            }
                            else if (str_temp[1] == "PORT")
                            {
                                temp_rfid.PORT = int.Parse(str_temp[1]);
                            }
                        }

                        LD[0].RFID = temp_rfid;
                    }
                    else if (str_data[i] == "[STB]")
                    {
                        for (int j = i + 1; j < str_data.Length; j++)
                        {
                            //LD[0].LD_GOAL.Add(str_data[j]);
                        }
                        Monitor.Goal_ADD(LD[0].LD_GOAL);
                    }
                    else if(str_data[i] == "[DOOR]")
                    {
                        String[] str_Temp;                        
                        str_Temp = str_data[i + 1].Split('=');

                        for (int j = 0; j < int.Parse(str_Temp[1]); j++)
                        {
                            stDOOR dOOR = new stDOOR();
                            string[] str_buf = str_data[j + i + 2].Split('=');
                            
                            dOOR.NAME = str_buf[0];
                            dOOR.ID = str_buf[1].Split(',')[0];
                            dOOR.DO = str_buf[1].Split(',')[1];
                            
                            Doors[j] = dOOR;
                        }
                    }
                    else if(str_data[i] == "[ALARM]")
                    {
                        
                        String[] str_temp;
                        int PORT = 0;
                        string ID = "";


                        for (int j = 0; j < 3; j++)
                        {
                            str_temp = str_data[j + i + 1].Split('=');

                            if (str_temp[0] == "PORT")
                            {
                                PORT = int.Parse(str_temp[1]);
                            }
                            else if (str_temp[0] == "ID")
                            {
                                ID = str_temp[1];
                            }
                            else if(str_temp[0]== "DO")
                            {
                                LD[0].ALARM_DO_NUM = int.Parse(str_temp[1]);
                            }
                        }

                        ALARM_SRV = new CALARM_SRV(PORT, ID);
                        ALARM_SRV.Set_ZIGBEE_ID(LD[0].ZIGBEE_NUM);

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void Read_AREA()
        {
            try
            {
                string Setting_data = System.IO.File.ReadAllText(System.Environment.CurrentDirectory + "\\Setting\\AREA.txt");
                string[] str_data = Setting_data.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < str_data.Length; i++)
                {
                    string[] str_temp = str_data[i].Split('\t');

                    stAREA aREA = new stAREA();
                    aREA.AREA_NAME = str_temp[0];
                    aREA.P1.X = int.Parse(str_temp[1]);
                    aREA.P1.Y = int.Parse(str_temp[2]);
                    aREA.P2.X = int.Parse(str_temp[3]);
                    aREA.P2.Y = int.Parse(str_temp[4]);

                    if(str_temp.Length >= 6)
                        aREA.Action = str_temp[5];

                    AREAs.Add(aREA);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Read_Skynet()
        {
            try
            {
                string Setting_data = System.IO.File.ReadAllText(System.Environment.CurrentDirectory + "\\Setting\\SKYNET.txt");
                string[] str_data = Setting_data.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < str_data.Length; i++)
                {
                    string[] str_temp = str_data[i].Split('\t');                   

                    if(str_temp[0] == "IP")
                    {
                        Skynet_Param.IP = str_temp[1];
                    }
                    else if (str_temp[0] == "LINE CODE")
                    {
                        Skynet_Param.LINE_CODE = str_temp[1];
                    }
                    else if (str_temp[0] == "PROCESS CODE")
                    {
                        Skynet_Param.PROCEESS_CODE = str_temp[1];
                    }
                    else if(str_temp[0] == "EQUIPMENT ID")
                    {
                        Skynet_Param.EQUIPMENT_ID = str_temp[1];
                    }
                }

                Monitor.SetSkynetData(Skynet_Param.LINE_CODE, Skynet_Param.EQUIPMENT_ID);
            }
            catch (Exception)
            {

                throw;
            }
        }




        public void Read_TAG_Setting()
        {
            string Setting_data = System.IO.File.ReadAllText(System.Environment.CurrentDirectory + "\\Setting\\CAPCODE.txt", UTF8Encoding.Default);

            string[] str_data = Setting_data.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            string[] str_temp = Setting_data.Split(Environment.NewLine.ToCharArray());
            TAG_ID tag_temp = new TAG_ID();

            for (int i = 0; i < str_temp.Length; i++)
            {
                if (str_temp[i] == "")
                {

                }
                else if (str_temp[i].Substring(0, 1) == "[" && str_temp[i].Substring(str_temp[i].Length - 1, 1) == "]")
                {
                    tag_temp.TAG_PART = str_temp[i].Substring(1, str_temp[i].Length - 2);
                }
                else
                {
                    string[] str_buf = str_temp[i].Split(',');

                    tag_temp.TAG_NAME = str_buf[0];
                    tag_temp.TAG_ID_NUM = str_buf[1];
                    tag_temp.TAG_TARGET = true;

                    CAPCODE_ARRAY.Add(tag_temp);
                }
            }
        }

        private void Login()
        {
            SendMsg(claCommand.Command.ID_Check, LD[0].LD_NAME);
        }

        private void SendMsg(claCommand.Command typeCommand, string sData)
        {
            //보낼 데이터 만들기
            dataSend insSend = new dataSend();
            insSend.Command = typeCommand;
            insSend.Data_String = sData;
            SendMsg(insSend);
        }

        /// <summary>
        /// 서버로 메시지를 전달 합니다.
        /// </summary>
        /// <param name="insDataSend"></param>
        private void SendMsg(dataSend insDataSend)
        {
            if (null == insDataSend)
            {   //데이터가 없으면 그냥 끝낸다.
                return;
            }

            //전달용 클래스를 바이트로 변환 한다.
            byte[] byteSend = insDataSend.CreateDataOriginal().Data;
            //서버로 전송~
            if(AMC_Client.IsConnected == true)
            {
                AMC_Client.Send(byteSend, 0, byteSend.Length);
            }
            else
            {
                AMC_Client.Connect();
            }
            
        }

        private string Find_AMC(string msg)
        {
            string[] str_buf = msg.Split(',');

            for (int i = 0; i < str_buf.Length - 1; i++)
            {
                string[] str_temp = str_buf[i].Split('=');

                if (str_temp[0] == "AMC")
                {
                    return str_temp[1];
                }
            }
            return "EMPTY";
        }

        private string Find_STB(string msg)
        {
            string val = "EMPTY";
            string[] _msg = msg.Split(',');

            for (int i = 0; i < _msg.Length; i++)
            {
                string[] _buf = _msg[i].Split('=');
                if (_buf[0] != "")
                {
                    if (_buf[0] == "STB")
                    {
                        val = _buf[1];
                    }
                }
            }

            return val;
        }

        private string Find_STATUS(string msg)
        {
            string val = "EMPTY";

            string[] _msg = msg.Split(',');

            for (int i = 0; i < _msg.Length; i++)
            {
                string[] _buf = _msg[i].Split('=');

                if (_buf[0] != "")
                {
                    if (_buf[0] == "STATUS")
                    {
                        val = _buf[1];
                    }
                }
            }

            return val;
        }


        private string Find_CMD(string msg)
        {
            string[] str_buf = msg.Split(',');

            try
            {            
                for (int i = 0; i < str_buf.Length; i++)
                {
                    string[] str_temp = str_buf[i].Split('=');

                    if (str_temp[0] == "CMD")
                    {
                        return str_temp.Length > 1 ? str_temp[1] : "EMPTY";
                    }
                }
                return "EMPTY";
            }
            catch (Exception ex)
            {
                Insert_ERR_Log("Find_CMD " + ex.Message);
            }
            return "EMPTY";
        }

        private static string Find_GOAL(string msg)
        {
            string[] str_buf = msg.Split(',');

            for (int i = 0; i < str_buf.Length; i++)
            {
                string[] str_temp = str_buf[i].Split('=');

                if (str_temp[0] == "GOAL")
                {
                    return str_temp[1];
                }
            }
            return "EMPTY";
        }

        static public string Find_Capcode_By_Goalname(string Goal_name)
        {
            string cap_code = "";

            for (int i = 0; i < CAPCODE_ARRAY.Count; i++)
            {
                if (CAPCODE_ARRAY[i].TAG_PART == Goal_name)
                {
                    if (CAPCODE_ARRAY[i].TAG_TARGET == true)
                    {
                        cap_code = CAPCODE_ARRAY[i].TAG_ID_NUM;
                        return cap_code;
                    }
                }
            }

            return cap_code;
        }

        int retry_cnt;

        private void Retry_timer_Tick(object sender, EventArgs e)
        {

            retry_cnt++;
            AMC_Client_START();

            if (retry_cnt > 5)
            {
                retry_cnt = 0;
            }
        }


        /// <summary>
        /// Conveyor Run Signal 기다림
        /// </summary>
        /// <returns></returns>
        private bool Wait_Run_Signal()
        {

            return Conveyor_RUN_ST;
        }


        DateTime MAN_StartDate;
        private void Set_Timer()
        {
            MAN_StartDate = DateTime.Now;
        }


        private void SIM_Insert_Tray()
        {
            //INPUT_DATA[0, IN_NUM_TRAY_SENSOR4_1] = true;
            for (int i = 0; i < 4; i++)
            {
                if (tray_pos[i] == -1)
                {
                    tray_pos[i] = 16;
                    return;
                }
            }
        }

        bool bSIM_T = false;
        bool[] sensor_temp = new bool[20];
        int[] tray_pos = new int[4] { -1, -1, -1, -1 };

        private void SIM_TIME_DoWork(object sender, DoWorkEventArgs e)
        {
            int cnt = 0;
            while (bSIM_T)
            {
                for (int i = 0; i < 16; i++)
                {
                    INPUT_DATA[1, i] = INPUT_DATA[0, i];
                }

                if (Conveyor_MODE == "AUTO_IN" && cnt++ > 30)
                {
                    cnt = 0;
                    for (int i = 0; i < tray_pos.Length; i++)
                    {


                        if (tray_pos[i] != -1)
                        {
                            sensor_temp[tray_pos[i]] = true;
                            sensor_temp[tray_pos[i] + 1] = false;
                            sensor_temp[tray_pos[i] + 2] = false;
                        }
                    }

                    INPUT_DATA[0, IN_NUM_TRAY_SENSOR1] = sensor_temp[0];
                    INPUT_DATA[0, IN_NUM_TRAY_SENSOR1_1] = sensor_temp[2];
                    INPUT_DATA[0, IN_NUM_TRAY_SENSOR2] = sensor_temp[4];
                    INPUT_DATA[0, IN_NUM_TRAY_SENSOR2_1] = sensor_temp[6];
                    INPUT_DATA[0, IN_NUM_TRAY_SENSOR3] = sensor_temp[8];
                    INPUT_DATA[0, IN_NUM_TRAY_SENSOR3_1] = sensor_temp[10];
                    INPUT_DATA[0, IN_NUM_TRAY_SENSOR4] = sensor_temp[12];
                    INPUT_DATA[0, IN_NUM_TRAY_SENSOR4_1] = sensor_temp[14];
                }
                else if (Conveyor_MODE == "AUTO_OUT" && cnt++ > 30)
                {
                    cnt = 0;
                    for (int i = 0; i < 16; i++)
                    {
                        sensor_temp[i] = false;
                    }

                    for (int i = 0; i < tray_pos.Length; i++)
                    {
                        //if (tray_pos[i] == 2 && OUTPUT_DATA[0, OUT_NUM_STOPER1])
                        //    break;
                        //if (tray_pos[i] == 6 && OUTPUT_DATA[0, OUT_NUM_STOPER2])
                        //    break;
                        //if (tray_pos[i] == 10 && OUTPUT_DATA[0, OUT_NUM_STOPER3])
                        //    break;

                        //if (OUTPUT_DATA[0, OUT_NUM_STOPER1] == false && tray_pos[i] != 2)
                        //{
                        //    tray_pos[i] = tray_pos[i] == -1 ? -1 : ++tray_pos[i];
                        //}
                        //else if (OUTPUT_DATA[0, OUT_NUM_STOPER2] == false && tray_pos[i] != 6)
                        //{
                        //    tray_pos[i] = tray_pos[i] == -1 ? -1 : ++tray_pos[i];
                        //}
                        //else if (OUTPUT_DATA[0, OUT_NUM_STOPER3] == false && tray_pos[i] != 10)
                        //{
                        //    tray_pos[i] = tray_pos[i] == -1 ? -1 : ++tray_pos[i];
                        //}

                        //if (tray_pos[i] != -1 && tray_pos[i] < 16)
                        //{
                        //    sensor_temp[tray_pos[i]] = true;
                        //}
                    }

                    INPUT_DATA[0, IN_NUM_TRAY_SENSOR1] = sensor_temp[0];
                    INPUT_DATA[0, IN_NUM_TRAY_SENSOR1_1] = sensor_temp[2];
                    INPUT_DATA[0, IN_NUM_TRAY_SENSOR2] = sensor_temp[4];
                    INPUT_DATA[0, IN_NUM_TRAY_SENSOR2_1] = sensor_temp[6];
                    INPUT_DATA[0, IN_NUM_TRAY_SENSOR3] = sensor_temp[8];
                    INPUT_DATA[0, IN_NUM_TRAY_SENSOR3_1] = sensor_temp[10];
                    INPUT_DATA[0, IN_NUM_TRAY_SENSOR4] = sensor_temp[12];
                    INPUT_DATA[0, IN_NUM_TRAY_SENSOR4_1] = sensor_temp[14];
                }

                System.Threading.Thread.Sleep(100);
            }
        }

        private void bg_retry_DoWork(object sender, DoWorkEventArgs e)
        {
            while (bRetry_T)
            {
                try
                {
                    if (AMC_Client != null)
                    {
                        if (AMC_Client.IsConnected == false)
                        {
                            AMC_Client_START();
                            retry_cnt++;
                        }
                    }
                    System.Threading.Thread.Sleep(3000);
                }
                catch (Exception)
                {
                    System.Threading.Thread.Sleep(3000);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Send_AMC_MSG("SEND", "NONE", "RFID_FIND", "NONE", "396664");
        }

        private void cameraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DMR.ShowDialog();
        }

        private void lDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HMI.Show();
        }

        private void rFIDReaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_RFID.ShowDialog();
        }

        private void aMCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Monitor.Show();
        }


        static public void Show_RFID()
        {
            frm_RFID.ShowDialog();
        }

        static public void Show_Camera()
        {
            DMR.ShowDialog();
        }


        static public void Show_LD()
        {
            HMI.ShowDialog();
        }

        private bool Is_IN(int scr, int dest, int per)
        {
            int MIN = (int)(dest * (100 - per) * 0.01);
            int MAX = (int)(dest * (100 + per) * 0.01);

            if (MIN < scr && scr > MAX)
            {
                return true;
            }

            return false;
        }


        static public void Err_clear()
        {
            ERR_QUEUE.Err_Remove();
        }

        static public void Conveyor_BW_stop()
        {
            bCONVEYOR_T = false;
        }

        static public void Reset()
        {

        }


        public void List_box_clear()
        {
            bCmd_run = false;
        }

        private void bg_Timer_DoWork(object sender, DoWorkEventArgs e)
        {
            while (bTimer_T)
            {
                if ((DateTime.Now - Err_chk_time).Milliseconds > Err_chk_mil)
                {
                    if (Err_chk_msg == Err_chk_sMOUTH)
                    {
                        Err_chk_nRetry += 1;

                        if (Err_chk_nRetry >= Err_chk_Retry_MOUTH)
                        {
                            Insert_ERR((int)ERR_Q.ERR_CODE.AUTO_OUT_MOUTH_ERR);
                            Err_chk_nRetry = 0;
                        }
                        else
                        {
                            Set_time_over_param(Err_chk_nMOUTH, Err_chk_sMOUTH);
                        }
                    }
                }

                System.Threading.Thread.Sleep(100);
            }
        }

        private void Set_time_over_param(int mil, string msg)
        {
            bTimer_T = true;
            Err_chk_time = DateTime.Now;
            Err_chk_mil = mil;
            Err_chk_msg = msg;

            bg_Timer.RunWorkerAsync();
        }


        private void DisEable_timer(string msg)
        {
            if (Err_chk_msg == msg)
            {
                bTimer_T = false;
                bg_Timer.CancelAsync();
            }
        }

        private void DOOR_Open(string id)
        {
            byte res = 0;

            arr_byte[0] = 0x02;
            arr_byte[1] = (byte)id.Substring(0, 1).ToCharArray()[0];// 0x30;// (byte)arr_addr[0]; //0x30; // addr >>
            arr_byte[2] = (byte)id.Substring(1, 1).ToCharArray()[0];//0x34;// (byte)arr_addr[1]; //0x34;
            arr_byte[3] = (byte)id.Substring(2, 1).ToCharArray()[0];//0x30;// (byte)arr_addr[2]; //0x30;
            arr_byte[4] = (byte)id.Substring(3, 1).ToCharArray()[0];//0x31;// (byte)arr_addr[3]; //0x31; // << addr
            arr_byte[5] = 0x41;
            arr_byte[6] = 0x42;
            arr_byte[7] = 0x5A;
            arr_byte[8] = (byte)LD[0].ZIGBEE_NUM.Substring(0, 1).ToCharArray()[0];//0x30;// (byte)arr_AVG[0];//0x30;
            arr_byte[9] = (byte)LD[0].ZIGBEE_NUM.Substring(1, 1).ToCharArray()[0];//0x30;// (byte)arr_AVG[1];//0x30;
            arr_byte[10] = (byte)LD[0].ZIGBEE_NUM.Substring(2, 1).ToCharArray()[0];//0x30;//(byte)arr_AVG[2];//0x30;
            arr_byte[11] = (byte)LD[0].ZIGBEE_NUM.Substring(3, 1).ToCharArray()[0];//0x31;//(byte)arr_AVG[3];//0x38;
            arr_byte[12] = 0x30;
            arr_byte[13] = 0x49;
            arr_byte[14] = 0x30;
            arr_byte[15] = 0x30;
            arr_byte[18] = 0x03;

            for (int i = 1; i <= 15; i++)
            {
                res += arr_byte[i];

                if (i == 15)
                {
                    string str = res.ToString("x").ToUpper();
                    char[] arr_str = new char[str.Length];

                    arr_str = str.Substring(str.Length - 2, 2).ToCharArray();

                    arr_byte[16] = (byte)arr_str[arr_str.Length - 2];
                    arr_byte[17] = (byte)arr_str[arr_str.Length - 1];
                }
            }

            Zigbee.Write(arr_byte, 0, 19);

            //bZIGBEE_T = true;
            //if(bg_zigbee.IsBusy == false)
            //{
            //    bg_zigbee.RunWorkerAsync();
            //}
        }

        private void DOOR_Close(string id)
        {
            byte res = 0;

            arr_byte[0] = 0x02;
            arr_byte[1] = (byte)id.Substring(0, 1).ToCharArray()[0];//0x30;// (byte)arr_addr[0]; //0x30; // addr >>
            arr_byte[2] = (byte)id.Substring(1, 1).ToCharArray()[0];//0x34;// (byte)arr_addr[1]; //0x34;
            arr_byte[3] = (byte)id.Substring(2, 1).ToCharArray()[0];//0x30;// (byte)arr_addr[2]; // 0x30;
            arr_byte[4] = (byte)id.Substring(3, 1).ToCharArray()[0];//0x31;// (byte)arr_addr[3]; //0x31; // << addr
            arr_byte[5] = 0x41;
            arr_byte[6] = 0x42;
            arr_byte[7] = 0x5A;
            arr_byte[8] = (byte)LD[0].ZIGBEE_NUM.Substring(0, 1).ToCharArray()[0];//0x30;// (byte)arr_AVG[0];//0x30;0x30;
            arr_byte[9] = (byte)LD[0].ZIGBEE_NUM.Substring(1, 1).ToCharArray()[0];//0x30;// (byte)arr_AVG[1];//0x30;0x30;
            arr_byte[10] = (byte)LD[0].ZIGBEE_NUM.Substring(2, 1).ToCharArray()[0];//0x30;// (byte)arr_AVG[2];//0x30; 0x30;
            arr_byte[11] = (byte)LD[0].ZIGBEE_NUM.Substring(3, 1).ToCharArray()[0];//0x31;// (byte)arr_AVG[3];//0x38; 0x38;
            arr_byte[12] = 0x30;
            arr_byte[13] = 0x4F;
            arr_byte[14] = 0x30;
            arr_byte[15] = 0x30;
            arr_byte[18] = 0x03;

            for (int i = 1; i <= 15; i++)
            {
                res += arr_byte[i];

                if (i == 15)
                {
                    string str = res.ToString("x").ToUpper();
                    char[] arr_str = new char[str.Length];

                    arr_str = str.Substring(str.Length - 2, 2).ToCharArray();

                    arr_byte[16] = (byte)arr_str[arr_str.Length - 2];
                    arr_byte[17] = (byte)arr_str[arr_str.Length - 1];
                }
            }

            Zigbee.Write(arr_byte, 0, 19);

            //bZIGBEE_T = true;
            //if (bg_zigbee.IsBusy == false)
            //{
            //    bg_zigbee.RunWorkerAsync();
            //}
        }

        private void AIRSHOWER_IN_Open()
        {
            byte res = 0;

            arr_byte[0] = 0x02;
            arr_byte[1] = 0x30;// (byte)arr_addr[0]; //0x30; // addr >>
            arr_byte[2] = 0x33;// (byte)arr_addr[1]; //0x34;
            arr_byte[3] = 0x30;// (byte)arr_addr[2]; // 0x30;
            arr_byte[4] = 0x31;// (byte)arr_addr[3]; //0x31; // << addr
            arr_byte[5] = 0x41;// 'A'
            arr_byte[6] = 0x47;// 'G'
            arr_byte[7] = 0x54;// 'T'
            arr_byte[8] = (byte)LD[0].ZIGBEE_NUM.Substring(0, 1).ToCharArray()[0];//0x30;// (byte)arr_AVG[0];//0x30;0x30;
            arr_byte[9] = (byte)LD[0].ZIGBEE_NUM.Substring(1, 1).ToCharArray()[0];//0x30;// (byte)arr_AVG[1];//0x30;0x30;
            arr_byte[10] = (byte)LD[0].ZIGBEE_NUM.Substring(2, 1).ToCharArray()[0];//0x30;// (byte)arr_AVG[2];//0x30; 0x30;
            arr_byte[11] = (byte)LD[0].ZIGBEE_NUM.Substring(3, 1).ToCharArray()[0];//0x31;// (byte)arr_AVG[3];//0x38; 0x38;
            arr_byte[12] = 0x30;
            arr_byte[13] = 0x49;// 'I'
            arr_byte[14] = 0x30;
            arr_byte[15] = 0x31;
            arr_byte[18] = 0x03;

            for (int i = 1; i <= 15; i++)
            {
                res += arr_byte[i];

                if (i == 15)
                {
                    string str = res.ToString("x").ToUpper();
                    char[] arr_str = new char[str.Length];

                    arr_str = str.Substring(str.Length - 2, 2).ToCharArray();

                    arr_byte[16] = (byte)arr_str[arr_str.Length - 2];
                    arr_byte[17] = (byte)arr_str[arr_str.Length - 1];
                }
            }

            Zigbee.Write(arr_byte, 0, 19);

            //bZIGBEE_T = true;
            //if (bg_zigbee.IsBusy == false)
            //{
            //    bg_zigbee.RunWorkerAsync();
            //}

        }

        private void AIRSHOWER_IN_Close()
        {
            byte res = 0;

            arr_byte[0] = 0x02;
            arr_byte[1] = 0x30;// (byte)arr_addr[0]; //0x30; // addr >>
            arr_byte[2] = 0x33;// (byte)arr_addr[1]; //0x34;
            arr_byte[3] = 0x30;// (byte)arr_addr[2]; // 0x30;
            arr_byte[4] = 0x31;// (byte)arr_addr[3]; //0x31; // << addr
            arr_byte[5] = 0x41;// 'A'
            arr_byte[6] = 0x47;// 'G'
            arr_byte[7] = 0x54;// 'T'
            arr_byte[8] = (byte)LD[0].ZIGBEE_NUM.Substring(0, 1).ToCharArray()[0];// 0x30;// (byte)arr_AVG[0];//0x30;0x30;
            arr_byte[9] = (byte)LD[0].ZIGBEE_NUM.Substring(1, 1).ToCharArray()[0];// 0x30;// (byte)arr_AVG[1];//0x30;0x30;
            arr_byte[10] = (byte)LD[0].ZIGBEE_NUM.Substring(2, 1).ToCharArray()[0];// 0x30;// (byte)arr_AVG[2];//0x30; 0x30;
            arr_byte[11] = (byte)LD[0].ZIGBEE_NUM.Substring(3, 1).ToCharArray()[0];// 0x31;// (byte)arr_AVG[3];//0x38; 0x38;
            arr_byte[12] = 0x30;
            arr_byte[13] = 0x49;// 'I'
            arr_byte[14] = 0x30;
            arr_byte[15] = 0x32;
            arr_byte[18] = 0x03;

            for (int i = 1; i <= 15; i++)
            {
                res += arr_byte[i];

                if (i == 15)
                {
                    string str = res.ToString("x").ToUpper();
                    char[] arr_str = new char[str.Length];

                    arr_str = str.Substring(str.Length - 2, 2).ToCharArray();

                    arr_byte[16] = (byte)arr_str[arr_str.Length - 2];
                    arr_byte[17] = (byte)arr_str[arr_str.Length - 1];
                }
            }

            Zigbee.Write(arr_byte, 0, 19);

            //bZIGBEE_T = true;
            //if (bg_zigbee.IsBusy == false)
            //{
            //    bg_zigbee.RunWorkerAsync();
            //}

        }


        private void AIRSHOWER_OUT_Open()
        {
            byte res = 0;

            arr_byte[0] = 0x02;
            arr_byte[1] = 0x30;// (byte)arr_addr[0]; //0x30; // addr >>
            arr_byte[2] = 0x33;// (byte)arr_addr[1]; //0x34;
            arr_byte[3] = 0x30;// (byte)arr_addr[2]; // 0x30;
            arr_byte[4] = 0x31;// (byte)arr_addr[3]; //0x31; // << addr
            arr_byte[5] = 0x41;// 'A'
            arr_byte[6] = 0x47;// 'G'
            arr_byte[7] = 0x54;// 'T'
            arr_byte[8] = (byte)LD[0].ZIGBEE_NUM.Substring(0, 1).ToCharArray()[0];// 0x30;// (byte)arr_AVG[0];//0x30;0x30;
            arr_byte[9] = (byte)LD[0].ZIGBEE_NUM.Substring(1, 1).ToCharArray()[0];// 0x30;// (byte)arr_AVG[1];//0x30;0x30;
            arr_byte[10] = (byte)LD[0].ZIGBEE_NUM.Substring(2, 1).ToCharArray()[0];// 0x30;// (byte)arr_AVG[2];//0x30; 0x30;
            arr_byte[11] = (byte)LD[0].ZIGBEE_NUM.Substring(3, 1).ToCharArray()[0];// 0x31;// (byte)arr_AVG[3];//0x38; 0x38;
            arr_byte[12] = 0x30;
            arr_byte[13] = 0x4F;// 'I'
            arr_byte[14] = 0x30;
            arr_byte[15] = 0x31;
            arr_byte[18] = 0x03;

            for (int i = 1; i <= 15; i++)
            {
                res += arr_byte[i];

                if (i == 15)
                {
                    string str = res.ToString("x").ToUpper();
                    char[] arr_str = new char[str.Length];

                    arr_str = str.Substring(str.Length - 2, 2).ToCharArray();

                    arr_byte[16] = (byte)arr_str[arr_str.Length - 2];
                    arr_byte[17] = (byte)arr_str[arr_str.Length - 1];
                }
            }

            Zigbee.Write(arr_byte, 0, 19);

        }

        private void AIRSHOWER_OUT_Close()
        {
            byte res = 0;

            arr_byte[0] = 0x02;
            arr_byte[1] = 0x30;// (byte)arr_addr[0]; //0x30; // addr >>
            arr_byte[2] = 0x33;// (byte)arr_addr[1]; //0x34;
            arr_byte[3] = 0x30;// (byte)arr_addr[2]; // 0x30;
            arr_byte[4] = 0x31;// (byte)arr_addr[3]; //0x31; // << addr
            arr_byte[5] = 0x41;// 'A'
            arr_byte[6] = 0x47;// 'G'
            arr_byte[7] = 0x54;// 'T'
            arr_byte[8] = (byte)LD[0].ZIGBEE_NUM.Substring(0, 1).ToCharArray()[0];// 0x30;// (byte)arr_AVG[0];//0x30;0x30;
            arr_byte[9] = (byte)LD[0].ZIGBEE_NUM.Substring(1, 1).ToCharArray()[0];// 0x30;// (byte)arr_AVG[1];//0x30;0x30;
            arr_byte[10] = (byte)LD[0].ZIGBEE_NUM.Substring(2, 1).ToCharArray()[0];// 0x30;// (byte)arr_AVG[2];//0x30; 0x30;
            arr_byte[11] = (byte)LD[0].ZIGBEE_NUM.Substring(3, 1).ToCharArray()[0];// 0x31;// (byte)arr_AVG[3];//0x38; 0x38;
            arr_byte[12] = 0x30;
            arr_byte[13] = 0x4F;// 'O'
            arr_byte[14] = 0x30;
            arr_byte[15] = 0x32;
            arr_byte[18] = 0x03;

            for (int i = 1; i <= 15; i++)
            {
                res += arr_byte[i];

                if (i == 15)
                {
                    string str = res.ToString("x").ToUpper();
                    char[] arr_str = new char[str.Length];

                    arr_str = str.Substring(str.Length - 2, 2).ToCharArray();

                    arr_byte[16] = (byte)arr_str[arr_str.Length - 2];
                    arr_byte[17] = (byte)arr_str[arr_str.Length - 1];
                }
            }

            Zigbee.Write(arr_byte, 0, 19);

        }

        int zigbee_read_cnt = 0;

        private void Zigbee_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int Read_cnt = Zigbee.BytesToRead;

            try
            {
                for (int i = 0; i < Read_cnt; i++)
                {
                    Recive_Byte[zigbee_read_cnt++] = (byte)Zigbee.ReadByte();

                    if (zigbee_read_cnt >= 18)
                    {
                        zigbee_read_cnt = 0;
                        Data_Paser();
                    }
                }
            }
            catch (Exception ex)
            {
                Insert_ERR_Log(ex);
            }
        }


        private bool Data_Paser()
        {
            byte res = 0;

            try
            {
                for (int i = 1; i <= Recive_Byte.Length - 1; i++)
                {
                    res += Recive_Byte[i];
                }

                string str = res.ToString("X");

                if (str.Length < 2)
                {
                    str += "0" + str;
                }

                char[] arr_str = new char[str.Length];

                arr_str = str.Substring(0, 2).ToCharArray();

                if (str_cmd_arr[0] == "AIR_IN_ON")
                {
                    if (Recive_Byte[5] == 'A' && Recive_Byte[6] == 'C' && Recive_Byte[7] == 'K')
                    {
                        if (Recive_Byte[8] == '0' && Recive_Byte[9] == '3' && Recive_Byte[10] == '0' && Recive_Byte[11] == '1')
                        {
                            if (Recive_Byte[12] == 'A' && Recive_Byte[13] == 'G' && Recive_Byte[14] == 'T')
                            {
                                nCmd_Step++;
                                bCmd_run = true;
                            }
                            else
                            {
                                bCmd_run = true;
                            }
                        }
                        else
                        {
                            bCmd_run = true;
                        }
                    }
                    else
                    {
                        bCmd_run = true;
                    }
                }
                else if (str_cmd_arr[0] == "AIR_IN_OFF")
                {
                    if (Recive_Byte[5] == 'A' && Recive_Byte[6] == 'C' && Recive_Byte[7] == 'K')
                    {
                        if (Recive_Byte[8] == '0' && Recive_Byte[9] == '3' && Recive_Byte[10] == '0' && Recive_Byte[11] == '1')
                        {
                            if (Recive_Byte[12] == 'A' && Recive_Byte[13] == 'G' && Recive_Byte[14] == 'T')
                            {
                                nCmd_Step++;
                                bCmd_run = true;
                            }
                            else
                            {
                                bCmd_run = true;
                            }
                        }
                        else
                        {
                            bCmd_run = true;
                        }
                    }
                    else
                    {
                        bCmd_run = true;
                    }
                }
                else if (str_cmd_arr[0] == "AIR_OUT_ON")
                {
                    if (Recive_Byte[5] == 'A' && Recive_Byte[6] == 'C' && Recive_Byte[7] == 'K')
                    {
                        if (Recive_Byte[8] == '0' && Recive_Byte[9] == '3' && Recive_Byte[10] == '0' && Recive_Byte[11] == '1')
                        {
                            if (Recive_Byte[12] == 'A' && Recive_Byte[13] == 'G' && Recive_Byte[14] == 'T')
                            {
                                nCmd_Step++;
                                bCmd_run = true;
                            }
                            else
                            {
                                bCmd_run = true;
                            }
                        }
                        else
                        {
                            bCmd_run = true;
                        }
                    }
                    else
                    {
                        bCmd_run = true;
                    }
                }
                else if (str_cmd_arr[0] == "AIR_OUT_OFF")
                {
                    if (Recive_Byte[5] == 'A' && Recive_Byte[6] == 'C' && Recive_Byte[7] == 'K')
                    {
                        if (Recive_Byte[8] == '0' && Recive_Byte[9] == '3' && Recive_Byte[10] == '0' && Recive_Byte[11] == '1')
                        {
                            if (Recive_Byte[12] == 'A' && Recive_Byte[13] == 'G' && Recive_Byte[14] == 'T')
                            {
                                nCmd_Step++;
                                bCmd_run = true;
                            }
                            else
                            {
                                bCmd_run = true;
                            }
                        }
                        else
                        {
                            bCmd_run = true;
                        }
                    }
                    else
                    {
                        bCmd_run = true;
                    }
                }
                else if (str_cmd_arr[0] == "DOOR_ON")
                {
                    if (Recive_Byte[5] == 'A' && Recive_Byte[6] == 'C' && Recive_Byte[7] == 'K')
                    {
                        if (Recive_Byte[8] == '0' && Recive_Byte[9] == '4' && Recive_Byte[10] == '0' && Recive_Byte[11] == '1')
                        {
                            if (Recive_Byte[12] == 'A' && Recive_Byte[13] == 'B' && Recive_Byte[14] == 'Z')
                            {
                                nCmd_Step++;
                                bCmd_run = true;
                            }
                            else
                            {
                                bCmd_run = true;
                            }
                        }
                        else
                        {
                            bCmd_run = true;
                        }
                    }
                    else
                    {
                        bCmd_run = true;
                    }
                }
                else if (str_cmd_arr[0] == "DOOR_OFF")
                {
                    if (Recive_Byte[5] == 'A' && Recive_Byte[6] == 'C' && Recive_Byte[7] == 'K')
                    {
                        if (Recive_Byte[8] == '0' && Recive_Byte[9] == '4' && Recive_Byte[10] == '0' && Recive_Byte[11] == '1')
                        {
                            if (Recive_Byte[12] == 'A' && Recive_Byte[13] == 'B' && Recive_Byte[14] == 'Z')
                            {
                                nCmd_Step++;
                                bCmd_run = true;
                            }
                            else
                            {
                                bCmd_run = true;
                            }
                        }
                        else
                        {
                            bCmd_run = true;
                        }
                    }
                    else
                    {
                        bCmd_run = true;
                    }
                }
                else
                {
                    if (Recive_Byte[0] == 0x02 && Recive_Byte[1] == arr_byte[8] && Recive_Byte[2] == arr_byte[9] && Recive_Byte[3] == arr_byte[10] && Recive_Byte[4] == arr_byte[11])
                    {
                        if (Recive_Byte[5] == 'A' && Recive_Byte[6] == 'C' && Recive_Byte[7] == 'K')
                        {
                            if (Recive_Byte[8] == arr_byte[1] && Recive_Byte[9] == arr_byte[2] && Recive_Byte[10] == arr_byte[3] && Recive_Byte[11] == arr_byte[4])
                            {
                                if (Recive_Byte[12] == 'A' && Recive_Byte[13] == 'B' && Recive_Byte[14] == 'Z')
                                {

                                    bZIGBEE_T = false;

                                    if (bg_zigbee.IsBusy == true)
                                        bg_zigbee.CancelAsync();
                                
                                }
                            }
                        }
                    }

                }


                Recive_Byte = new byte[Recive_Byte.Length];

                return false;
            }
            catch (Exception)
            {
                //MessageBox.Show(e.Message, "Data_Paser");
            }
            return false;
        }

        private void Chk_Serial_Port()
        {
            if (Zigbee.IsOpen == false)
            {
                string[] str_com = Get_COM();
                bool com_is_ok = false;

                for (int i = 0; i < str_com.Length; i++)
                {
                    if (str_com[i] == LD[0].ZIGBEE_PORT)
                    {
                        com_is_ok = true;
                    }
                }

                if (com_is_ok == true)
                {
                    Zigbee.PortName = LD[0].ZIGBEE_PORT;
                    Zigbee.Open();
                }
                else
                {
                    MessageBox.Show("시리얼 포트를 찾을 수 없습니다.", "시리얼 포트 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            //AIRSHOWER_IN_Open();
            
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            AIRSHOWER_IN_Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            AIRSHOWER_OUT_Open();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            AIRSHOWER_OUT_Close();
        }

        private void Btn_DOOR_OPEN_Click(object sender, EventArgs e)
        {
            //DOOR_Open();
        }

        private void Btn_DOOR_CLOSE_Click(object sender, EventArgs e)
        {
            //DOOR_Close();
        }

        static public string[] Get_COM()
        {
            string[] str = SerialPort.GetPortNames();

            return str;
        }

        static public string Get_CMD()
        {
            return str_cmd_arr[0];
        }


        DateTime Grip_Off_time;
        private void Bg_ungrip_DoWork(object sender, DoWorkEventArgs e)
        {
            Grip_Off_time = DateTime.Now;
                        
            while (INPUT_DATA[0, IN_NUM_GRIPER_OFF_LIMIT] == false && bg_Conveyor.IsBusy == false && (DateTime.Now - Grip_Off_time).TotalMilliseconds < 1500)
            {
                Griper_CW_Run();
                System.Threading.Thread.Sleep(50);
            }
            
            Griper_Stop();
        }

        static public bool bDo_ungrip = false;

        static public void Ungrip()
        {
            bDo_ungrip = true;
        }


        bool bg_LD_IO_ST_T = false;

        private void bg_LD_IO_ST_DoWork(object sender, DoWorkEventArgs e)
        {
            if (bg_LD_OUT.IsBusy == false)
            {
                bg_LD_OUT.RunWorkerAsync();
            }

            while(bg_LD_IO_ST_T)
            {
                    Send_IO_ST();

                    bool[] DI = new bool[16];
                    bool[] DO = new bool[16];

                    for (int i = 0; i < 16; i++)
                    {
                        DI[i] = LD_DI[0, i];
                        DO[i] = LD_DO[0, i];
                    }

                    Monitor.Set_LD_IO(DI, DO);

                if (LD[0].LD_TYPE != "AMB")
                {
                    if (Run_Latch_SS == true)
                    {
                        Run_Latch();
                    }

                    if (Side_laser_fold == true)
                    {
                        if (LD_DIGITAL_OUT[AMB_IN_NUM_LATCH_SWITCH] == false)
                        {
                            LD_DIGITAL_OUT[AMB_OUT_NUM_SIDE_LASER_OUT] = false;
                            LD_DIGITAL_OUT[AMB_OUT_NUM_SIDE_LASER_IN] = true;
                        }
                        else
                        {
                            MessageBox.Show("카트가 Lock 상태 입니다.", "Warring", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }
                    }
                    else
                    {
                        if (LD[0].LD_TYPE == "MRBT")
                        {
                            LD_DIGITAL_OUT[AMB_OUT_NUM_SIDE_LASER_IN] = false;
                            LD_DIGITAL_OUT[AMB_OUT_NUM_SIDE_LASER_OUT] = true;
                        }
                    }
                }
                System.Threading.Thread.Sleep(100);
            }            
        }


        static bool Latch_st = false;
        static bool BG_LATCH_T = false;
        static bool Run_Latch_SS = false;
        static bool Side_laser_fold;


        static public void LATCH()
        {
            Send_LD_String("executeMacro Latch");
        }

        static public void UNLATCH()
        {
            Send_LD_String("executeMacro Unlatch");
        }

        static public void SIDE_LASER_FOLD()
        {
            Send_LD_String("outputOff o3");
            Send_LD_String("outputOn o4");
        }


        static public void SIDE_LASER_UNFOLD()
        {
            Send_LD_String("outputOff o4");
            Send_LD_String("outputOn o3");
        }


        public void Run_Latch()
        {
            if (bg_latch.IsBusy == false)
            {
                bg_latch.RunWorkerAsync();
            }
        }


        private void bg_latch_DoWork(object sender, DoWorkEventArgs e)
        {
            while(BG_LATCH_T)
            {
                if (LD[0].LD_TYPE == "MRBT")
                {
                    if (Latch_st == true)
                    {
                        //if (LD_DI[0, AMB_IN_NUM_LOCKER_LINIT_SENSOR] == false || LD_DI[0,AMB_IN_NUM_CART_LATCH_SENSOR] == false)
                        //{
                        //    LD_DIGITAL_OUT[AMB_OUT_NUM_UNLATCH] = false;
                        //    LD_DIGITAL_OUT[AMB_OUT_NUM_LATCH] = true;                            
                        //}
                        //else
                        //{
                        //    LD_DIGITAL_OUT[AMB_OUT_NUM_LATCH] = false;
                        //    Send_string(LD[0].LD_Client, "outOff o" + (AMB_OUT_NUM_LATCH).ToString());
                        //}

                        Send_string(LD[0].LD_Client, "executeMacro Latch");
                    }
                    else if (Latch_st == false)
                    {
                        //if (LD_DI[0, AMB_IN_NUM_LOCKER_HOME_SENSOR] == false)
                        //{
                        //    LD_DIGITAL_OUT[AMB_OUT_NUM_LATCH] = false;
                        //    LD_DIGITAL_OUT[AMB_OUT_NUM_UNLATCH] = true;
                        //}
                        //else
                        //{
                        //    LD_DIGITAL_OUT[AMB_OUT_NUM_UNLATCH] = false;
                        //    Send_string(LD[0].LD_Client, "outOff o" + (AMB_OUT_NUM_UNLATCH).ToString());
                        //    BG_LATCH_T = false;
                        //    Run_Latch_SS = false;
                        //}

                        Send_string(LD[0].LD_Client, "executeMacro Unlatch");
                    }
                }
                System.Threading.Thread.Sleep(10);
            }
        }

        private void LD_OUT_DoWork(object sender, DoWorkEventArgs e)
        {
            while (bg_LD_IO_ST_T)
            {
                for (int i = 0; i < 16; i++)
                {
                    if (LD_DIGITAL_OUT[i] != LD_DIGITAL_OUT_B[i])
                    {
                        Send_string(LD[0].LD_Client, (LD_DIGITAL_OUT[i] == true ? "outOn o" + (i + 1).ToString() : "outOff o" + (i + 1).ToString()));
                        LD_DIGITAL_OUT_B[i] = LD_DIGITAL_OUT[i];
                        System.Threading.Thread.Sleep(5);
                    }
                }
                System.Threading.Thread.Sleep(10);
            }

        }

        bool bZIGBEE_T = false;

        private void bg_zigbee_DoWork(object sender, DoWorkEventArgs e)
        {
            System.Threading.Thread.Sleep(1000);
            while(bZIGBEE_T)
            {
                Zigbee.Write(arr_byte, 0, 19);

                System.Threading.Thread.Sleep(1000);
            }
        }

        static public void Stop_batt_sound()
        {
            low_batt_sound.Stop();
        }
    }
}
