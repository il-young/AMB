﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;

using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using SuperSocket.SocketBase.Logging;
using SuperSocket.SocketEngine;
using AMC_Test.ClientSession;

namespace AMC_Test
{
    class CALARM_SRV
    {
        private int ALARM_SRV_PORT;
        private string ALARM_SRV_ID;

        string ZIGBEE_ID = "";

        private claServer ALARM_SRV = new claServer();

        private char[] ALARM_CLIENT_Receve_data = new char[20];
        private int ALARM_CLIENT_Receve_data_cnt = 0;

        private System.ComponentModel.BackgroundWorker ACK_CHECK = new System.ComponentModel.BackgroundWorker();
        private System.ComponentModel.BackgroundWorker bg_Polling = new System.ComponentModel.BackgroundWorker();
        private bool bACK_CHECK_T = false;
        private bool bACK_OK = false;
        private bool bPolling_t = false;

        byte[] arr_byte = new byte[19];
        public CALARM_SRV()
        {
            
        }

        public CALARM_SRV(int port, string ID)
        {
            try
            {
                ALARM_SRV_PORT = port;
                ALARM_SRV_ID = ID;

                ALARM_SRV = new claServer();
                ALARM_SRV.NewSessionConnected += ALARM_SRV_Connected;
                ALARM_SRV.SessionClosed += ALARM_SRV_SessionClosed;
            
                ALARM_SRV.OnLog += ALARM_SRV_OnLog;
                ALARM_SRV.OnMessaged += ALARM_SRV_OnMessaged;
                ALARM_SRV.OnLoginUser += ALARM_SRV_OnLoginUser;
                ALARM_SRV.OnLogoutUser += ALARM_SRV_OnLogoutUser;

                ACK_CHECK.DoWork += new System.ComponentModel.DoWorkEventHandler(ACK_CHECK_DOWORK);
                bg_Polling.DoWork += new System.ComponentModel.DoWorkEventHandler(BG_POLLING_DOWORK);

                //ServerConfig serverConfig = new ServerConfig
                //{
                //    //Ip = "127.0.0.1",//테스트할때만 로컬 ip를 넣는다.
                //    Port = ALARM_SRV_PORT,
                //    MaxRequestLength = 0xffff
                //};

                //서버 설정 셋업
                ALARM_SRV.Setup(ALARM_SRV_PORT);

                if (false == ALARM_SRV.Start())
                {
                    Insert_Log("도착 알람 서버 시작 실패");
                    return;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        private void ALARM_SRV_Connected(claClientSession session)
        {
            Insert_Log(session.RemoteEndPoint.Address.ToString() + " Connected");

            //System.Windows.Forms.MessageBox.Show(session.RemoteEndPoint.Address.ToString());

            if (bg_Polling.IsBusy == false)
            {
                bPolling_t = true;
                bg_Polling.RunWorkerAsync();
            }
        }

        private void ALARM_SRV_SessionClosed(claClientSession session, SuperSocket.SocketBase.CloseReason value)
        {
            Insert_Log(session.RemoteEndPoint.Address.ToString() + " Disconnected");
        }


        private void ALARM_SRV_OnLog(claClientSession session, CustomEventArgs.LocalMessageEventArgs e)
        {
            Receve_data_paser(e.Message);
        }

        private void ALARM_SRV_OnMessaged(claClientSession session, CustomEventArgs.LocalMessageEventArgs e)
        {

        }


        private void ALARM_SRV_OnLoginUser(claClientSession session, CustomEventArgs.LocalMessageEventArgs e)
        {
            
        }

        private void ALARM_SRV_OnLogoutUser(claClientSession session, CustomEventArgs.LocalMessageEventArgs e)
        {

        }

        int Keepday = 30;

        private void Insert_Log(string msg)
        {
            string date = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:FFF"); ;
            string str_temp = "";
            string log_dir = System.Environment.CurrentDirectory + "\\Log\\ALARM_SRV\\ALARM_SRV_" + System.DateTime.Now.ToString("yyyy/MM/dd") + " Log.txt";

            try
            {
                if (System.IO.File.Exists(log_dir) == false)
                {
                    Directory.CreateDirectory(System.Environment.CurrentDirectory + "\\Log\\ALARM_SRV\\");
                    string temp;

                    temp = "========================================================" + Environment.NewLine;
                    temp += "=                                                                                                            =" + Environment.NewLine;
                    temp += "=                                      ALARM SRV Log File " + System.DateTime.Now.ToString("yyyy/MM/dd") + "                                    =" + Environment.NewLine;
                    temp += "=                                                                                                            =" + Environment.NewLine;
                    temp += "========================================================" + Environment.NewLine;

                    System.IO.File.WriteAllText(log_dir, temp);
                    Check_Log_date(System.Environment.CurrentDirectory + "\\Log\\ALARM_SRV\\", Keepday);
                }

                //str_buf = System.IO.File.ReadAllText(log_dir);
                               
                string[] arr_str = msg.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                System.IO.StreamWriter st = System.IO.File.AppendText(log_dir);

                for (int i = 0; i < arr_str.Length; i++)
                {                   
                    str_temp = "[" + date + "] " + arr_str[i];
                    st.WriteLine(str_temp);
                }

                st.Close();
                st.Dispose();
            }
            catch (Exception ex)
            {   
                //Insert_Log(msg);
            }
        }


        private void Insert_Log(string msg, byte[] data)
        {
            string date = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:FFF"); ;
            string str_temp = "";
            string log_dir = System.Environment.CurrentDirectory + "\\Log\\ALARM_SRV\\ALARM_SRV_" + System.DateTime.Now.ToString("yyyy/MM/dd") + " Log.txt";

            try
            {
                if (System.IO.File.Exists(log_dir) == false)
                {
                    Directory.CreateDirectory(System.Environment.CurrentDirectory + "\\Log\\ALARM_SRV\\");
                    string temp;

                    temp = "========================================================" + Environment.NewLine;
                    temp += "=                                                                                                            =" + Environment.NewLine;
                    temp += "=                                      ALARM SRV Log File " + System.DateTime.Now.ToString("yyyy/MM/dd") + "                                    =" + Environment.NewLine;
                    temp += "=                                                                                                            =" + Environment.NewLine;
                    temp += "========================================================" + Environment.NewLine;

                    System.IO.File.WriteAllText(log_dir, temp);
                    Check_Log_date(System.Environment.CurrentDirectory + "\\Log\\", Keepday);
                }

                //str_buf = System.IO.File.ReadAllText(log_dir);

                str_temp = "[" + date + "] " + msg;
                System.IO.StreamWriter st = System.IO.File.AppendText(log_dir);

                for (int i = 0; i < data.Length; i++)
                {
                    str_temp += " "+ data[i].ToString("X2");
                }

                st.WriteLine(str_temp);
                st.Close();
                st.Dispose();
            }
            catch (Exception ex)
            {
                //Insert_Log(msg);
            }
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

        private void Receve_data_paser(string data)
        {
            byte[] bdata = Encoding.UTF8.GetBytes(data);                
            string temp = Encoding.Default.GetString(bdata);

            Insert_Log("[R]",bdata);

            for (int i = 0; i < bdata.Length; i++)
            {
                if(bdata[i] != 0x02 && ALARM_CLIENT_Receve_data[0] == 0x02)
                    ALARM_CLIENT_Receve_data[ALARM_CLIENT_Receve_data_cnt++] = (char)bdata[i];
                else if(bdata[i] == 0x02)
                {
                    ALARM_CLIENT_Receve_data_cnt = 0;
                    ALARM_CLIENT_Receve_data[ALARM_CLIENT_Receve_data_cnt++] = (char)bdata[i];
                }

                if(ALARM_CLIENT_Receve_data_cnt >= 18)
                {
                    if (ALARM_CLIENT_Receve_data[0] == 0x02 && ALARM_CLIENT_Receve_data[ALARM_CLIENT_Receve_data_cnt - 1] == 0x03)
                    {
                        if (ALARM_CLIENT_Receve_data[1] == Encoding.GetEncoding("ksc_5601").GetBytes(ZIGBEE_ID)[0] &&
                            ALARM_CLIENT_Receve_data[2] == Encoding.GetEncoding("ksc_5601").GetBytes(ZIGBEE_ID)[1] &&
                            ALARM_CLIENT_Receve_data[3] == Encoding.GetEncoding("ksc_5601").GetBytes(ZIGBEE_ID)[2] &&
                            ALARM_CLIENT_Receve_data[4] == Encoding.GetEncoding("ksc_5601").GetBytes(ZIGBEE_ID)[3])
                        {
                            if (ALARM_CLIENT_Receve_data[5] == 'A' &&
                                ALARM_CLIENT_Receve_data[6] == 'C' &&
                                ALARM_CLIENT_Receve_data[7] == 'K')
                            {
                                if (ALARM_CLIENT_Receve_data[8] == ALARM_SRV_ID.ToCharArray()[0] &&
                                    ALARM_CLIENT_Receve_data[9] == ALARM_SRV_ID.ToCharArray()[1] &&
                                    ALARM_CLIENT_Receve_data[10] == ALARM_SRV_ID.ToCharArray()[2] &&
                                    ALARM_CLIENT_Receve_data[11] == ALARM_SRV_ID.ToCharArray()[3])
                                {
                                    if (ALARM_CLIENT_Receve_data[12] == 'A' &&
                                        ALARM_CLIENT_Receve_data[13] == 'B' &&
                                        ALARM_CLIENT_Receve_data[14] == 'Z')
                                    {
                                        //if(Compare_CS_VAL() == true)
                                        {
                                            ALARM_CLIENT_Receve_data.Initialize();
                                            bACK_OK = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    ALARM_CLIENT_Receve_data_cnt = 0;
                }
            }
        }


        private bool Compare_CS_VAL()
        {
            byte res = 0;

            for (int i = 1; i <= 14; i++)
            {
                res += (byte)ALARM_CLIENT_Receve_data[i];

                if (i == 14)
                {
                    string str = res.ToString("x").ToUpper();
                    char[] arr_str = new char[str.Length];

                    arr_str = str.Substring(str.Length - 2, 2).ToCharArray();

                    if((ALARM_CLIENT_Receve_data[15] == (char)arr_str[arr_str.Length - 2]) && (ALARM_CLIENT_Receve_data[16] == (char)arr_str[arr_str.Length - 1]))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return false;
        }


        public void Send_arrived()
        {
            byte res = 0;

            arr_byte[0] = 0x02;
            arr_byte[1] = (byte)ALARM_SRV_ID.Substring(0, 1).ToCharArray()[0];// 0x30;// (byte)arr_addr[0]; //0x30; // addr >>
            arr_byte[2] = (byte)ALARM_SRV_ID.Substring(1, 1).ToCharArray()[0];//0x34;// (byte)arr_addr[1]; //0x34;
            arr_byte[3] = (byte)ALARM_SRV_ID.Substring(2, 1).ToCharArray()[0];//0x30;// (byte)arr_addr[2]; //0x30;
            arr_byte[4] = (byte)ALARM_SRV_ID.Substring(3, 1).ToCharArray()[0];//0x31;// (byte)arr_addr[3]; //0x31; // << addr
            arr_byte[5] = 0x41;
            arr_byte[6] = 0x42;
            arr_byte[7] = 0x5A;
            arr_byte[8] = Encoding.GetEncoding("ksc_5601").GetBytes(ZIGBEE_ID)[0]; //0x31;// (byte)arr_AVG[0];//0x30;
            arr_byte[9] = Encoding.GetEncoding("ksc_5601").GetBytes(ZIGBEE_ID)[1]; //0x30;// (byte)arr_AVG[1];//0x30;
            arr_byte[10] = Encoding.GetEncoding("ksc_5601").GetBytes(ZIGBEE_ID)[2]; //0x30;//(byte)arr_AVG[2];//0x30;
            arr_byte[11] = Encoding.GetEncoding("ksc_5601").GetBytes(ZIGBEE_ID)[3]; //0x30;//(byte)arr_AVG[3];//0x38;
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


            foreach (claClientSession insUserTemp in ALARM_SRV.GetAllSessions())
            {
                insUserTemp.Send(Encoding.Default.GetString(arr_byte));
            }
            //ALARM_SRV.SendMsg_All(SocketGlobal.claCommand.Command.Msg, Encoding.Default.GetString(arr_byte) );
            Insert_Log("[S]", arr_byte);
            bACK_CHECK_T = true;
            if(ACK_CHECK.IsBusy == false)
                ACK_CHECK.RunWorkerAsync();
        }

        public void Send_Polling()
        {
            byte res = 0;

            arr_byte[0] = 0x02;
            arr_byte[1] = (byte)ALARM_SRV_ID.Substring(0, 1).ToCharArray()[0];// 0x30;// (byte)arr_addr[0]; //0x30; // addr >>
            arr_byte[2] = (byte)ALARM_SRV_ID.Substring(1, 1).ToCharArray()[0];//0x34;// (byte)arr_addr[1]; //0x34;
            arr_byte[3] = (byte)ALARM_SRV_ID.Substring(2, 1).ToCharArray()[0];//0x30;// (byte)arr_addr[2]; //0x30;
            arr_byte[4] = (byte)ALARM_SRV_ID.Substring(3, 1).ToCharArray()[0];//0x31;// (byte)arr_addr[3]; //0x31; // << addr
            arr_byte[5] = 0x41;
            arr_byte[6] = 0x42;
            arr_byte[7] = 0x5A;
            arr_byte[8] = Encoding.GetEncoding("ksc_5601").GetBytes(ZIGBEE_ID)[0]; //0x31;// (byte)arr_AVG[0];//0x30;
            arr_byte[9] = Encoding.GetEncoding("ksc_5601").GetBytes(ZIGBEE_ID)[1]; //0x30;// (byte)arr_AVG[1];//0x30;
            arr_byte[10] = Encoding.GetEncoding("ksc_5601").GetBytes(ZIGBEE_ID)[2]; //0x30;//(byte)arr_AVG[2];//0x30;
            arr_byte[11] = Encoding.GetEncoding("ksc_5601").GetBytes(ZIGBEE_ID)[3]; //0x30;//(byte)arr_AVG[3];//0x38;
            arr_byte[12] = 0x30;
            arr_byte[13] = 0x49;
            arr_byte[14] = 0x30;
            arr_byte[15] = 0x35;
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

            foreach (claClientSession insUserTemp in ALARM_SRV.GetAllSessions())
            {
                insUserTemp.Send(Encoding.Default.GetString(arr_byte));
            }
            //ALARM_SRV.SendMsg_All(SocketGlobal.claCommand.Command.Msg, Encoding.Default.GetString(arr_byte) );
            Insert_Log("[S]", arr_byte);
            bACK_CHECK_T = true;
            if (ACK_CHECK.IsBusy == false)
                ACK_CHECK.RunWorkerAsync();
        }

        private void BG_POLLING_DOWORK(object sender, DoWorkEventArgs e)
        {
            while(bPolling_t)
            {
                Send_Polling();
                System.Threading.Thread.Sleep(60*1000);
            }
        }

        private void ACK_CHECK_DOWORK(object sender, DoWorkEventArgs e)
        {
            while(bACK_CHECK_T)
            {
                if (bACK_OK == true)
                {
                    arr_byte.Initialize();
                    bACK_CHECK_T = false;
                    return;
                }
                else
                {
                    Insert_Log("No response");
                    ALARM_SRV.SendMsg_All(SocketGlobal.claCommand.Command.Msg, Encoding.Default.GetString(arr_byte));
                    Insert_Log("[S]", arr_byte);
                }

                System.Threading.Thread.Sleep(500);
            }
        }

        public void Set_Port(int port)
        {
            ALARM_SRV_PORT = port;
        }


        public void Set_ZIGBEE_ID(string ID)
        {
            ZIGBEE_ID = ID;
        }


    }
}
