using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;

namespace AMC_Test
{
    public class DRS0201
    {
        SerialPort DRS_PORT = new SerialPort();
        
        private struct IJOG_TAG
        {
            int nJOG_DATA;
            uint unRESERVED;
            uint unSTOP;
            uint unMODE;
            uint unLED;
            uint unJOGINVALID;
            uint unID;

            byte byJOB_TIME;
        }

        private struct MOTOR_ST
        {
            public int ID;
            public int Position;
        }

        private const int MOTOR_NUM = 5;

        private const byte HEADER1 = 0xFF;
        private const byte HEADER2 = 0xFF;

        private const byte CMD_EEP_WRITE = 0x01;
        private const byte CMD_EEP_READ = 0x02;
        private const byte CMD_RAM_WRITE = 0X03;
        private const byte CMD_RAM_READ = 0x04;
        private const byte CMD_I_JOG = 0x05;
        private const byte CMD_S_JOG = 0x06;
        private const byte CMD_STAT = 0x07;
        private const byte CMD_ROLL_BACK = 0x08;
        private const byte CMD_REBOOT = 0x09;

        public bool bEStop = false;

        private System.ComponentModel.BackgroundWorker bg_St = new System.ComponentModel.BackgroundWorker();

        string Port_NAME;

        private MOTOR_ST[] MOTOR = new MOTOR_ST[MOTOR_NUM];

        private byte[] read_buffer = new byte[64];
        
        public DRS0201()
        {
            DRS_PORT.DataReceived += DRS_PORT_DataReceived;            
            bg_St.DoWork += Bg_St_DoWork;
            //bg_St.RunWorkerAsync();
            
        }

        private void Bg_St_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            byte[] data = new byte[9];

            data[0] = 0xFF;
            data[1] = 0xFF;
            data[2] = 0x09;
            data[3] = 0x03;
            data[4] = 0x04;
            data[5] = 0x2C;
            data[6] = 0xD2;
            data[7] = 0x34;
            data[8] = 0x16;

            System.Threading.Thread.Sleep(5000);
                       
            while(true)
            {
                try
                {
                    if (DRS_PORT.IsOpen == true)
                    {
                        if (bEStop == false)
                        {
                            int CRC = Make_CRC(data);

                            DRS_PORT.Write(data, 0, data.Length);

                            System.Threading.Thread.Sleep(10);

                            //DRS_PORT.Read(read_buffer, 0, DRS_PORT.BytesToRead);
                            int cnt = DRS_PORT.BytesToRead;
                            
                            Read_Serial_Data();
                        }
                        //for(int i = 0; i <  MOTOR_NUM; i++)
                        //{
                        //    Read_RAM_DATA((byte)MOTOR[i].ID, 0x44, 2);

                        //    System.Threading.Thread.Sleep(1000);

                        //    Read_Serial_Data();
                        //}

                    }
                    System.Threading.Thread.Sleep(500);
                }
                catch (Exception ex)
                {

                    throw;
                }                

                System.Threading.Thread.Sleep(500);
            }

            throw new NotImplementedException();
        }


        byte buffer_cnt = 0;

        private void Read_Serial_Data()
        {
            byte[] buff = new byte[64];
            int cnt = DRS_PORT.BytesToRead;
            int  i = 0;

            DRS_PORT.Read(buff, 0, cnt);

            for(i = 0; i < cnt ; i++)
            {
                if(read_buffer[0] == 0xff)
                {
                    if(read_buffer[1] == 0xff)
                    {
                        if (read_buffer[2] != 0xff)
                        {
                            read_buffer[buffer_cnt++] = buff[i];
                        }
                        else
                        {
                            read_buffer[0] = buff[i];
                        }
                    }
                    else
                    {
                        read_buffer[1] = buff[i];
                        buffer_cnt = 2;
                    }
                    
                }
                else
                {
                    read_buffer[0] = buff[i];
                }
                

                if (buffer_cnt == 34)
                {
                    Serial_data_paser();
                    buffer_cnt = 0;
                }                    
            }
        }


        private void Serial_data_paser()
        {
            byte[] buff = new byte[64];
            byte cnt = 0;

            if (read_buffer[0] == 0xff && read_buffer[1] == 0xff)
            {
                cnt = read_buffer[2];
                buff = new byte[cnt];
            }
            else
            {
                return;
            }

            for (int i = 0; i < cnt; i++)
            {
                buff[i] = read_buffer[i];                
            }

            read_buffer = new byte[40];

            int CRC = Make_CRC(buff);
            int buff_CRC = buff[5] << 8;
            buff_CRC = buff_CRC | buff[6];

            if (CRC == buff_CRC)
            {
                if(buff[3] != 0x00 && buff.Length >= 18)
                    MOTOR[Find_ID(buff[3])].Position = (buff[17] << 8) | buff[18];
            }

        }

        public void motot_init()
        {
            if (bEStop == false)
            {
                if (Form1.LD[0].LD_TYPE == "AMC")
                {
                    for (int i = 0; i < MOTOR_NUM; i++)
                    {
                        MOTOR[i].ID = 3 + i;

                        MOTOR_CLEAR1(MOTOR[i].ID);
                        System.Threading.Thread.Sleep(10);
                        MOTOR_TORQUE_ON(MOTOR[i].ID);
                        System.Threading.Thread.Sleep(10);
                        MOTOR_OFF(MOTOR[i].ID);
                        System.Threading.Thread.Sleep(10);
                    }
                }
            }
        }


        public void motor_clear()
        {
            if (bEStop == false)
            {
                if (Form1.LD[0].LD_TYPE == "AMC")
                {
                    for (int i = 0; i < MOTOR_NUM; i++)
                    {
                        MOTOR[i].ID = 3 + i;

                        MOTOR_CLEAR1(MOTOR[i].ID);
                        System.Threading.Thread.Sleep(10);
                        MOTOR_TORQUE_ON(MOTOR[i].ID);
                    }
                }
            }
        }

        public void motot_clear()
        {
            if (bEStop == false)
            {
                if (Form1.LD[0].LD_TYPE == "AMC")
                {
                    for (int i = 0; i < MOTOR_NUM; i++)
                    {
                        MOTOR[i].ID = 3 + i;

                        MOTOR_CLEAR1(MOTOR[i].ID);
                        System.Threading.Thread.Sleep(10);
                        MOTOR_TORQUE_ON(MOTOR[i].ID);
                        System.Threading.Thread.Sleep(10);
                    }
                }
            }
        }


        private void DRS_PORT_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] temp = new byte[512];
            DRS_PORT.Read(temp, 0, DRS_PORT.BytesToRead > 13 ? 13 : DRS_PORT.BytesToRead);

            int cnt = 0; ;
            byte[] buff = { };

            try
            {
                //if (temp[0] == 0xff && temp[1] == 0xff)
                //{
                //    cnt = temp[2];
                //    buff = new byte[cnt];
                //}

                //for (int i = 0; i < cnt; i++)
                //{
                //    buff[i] = temp[i];
                //}

                //int CRC = Make_CRC(buff);
                //int buff_CRC = buff[5] << 8;
                //buff_CRC = buff_CRC | buff[6];

                //if (CRC == buff_CRC)
                //{
                //    MOTOR[Find_ID(buff[3])].Position = (buff[10] << 8) | buff[9];
                //}
            }
            catch (Exception)
            {
                
            }
        }

        public int Read_Position(byte ID)
        {
            return MOTOR[ID].Position;
        }


        private int Find_ID(int ID)
        {
            for(int i = 0 ; i < MOTOR_NUM; i ++)
            {
                if (MOTOR[i].ID == ID)
                    return i;
            }
            return -1;
        }


        public void Set_Port_NAME(string COM)
        {
            if(DRS_PORT.IsOpen == false)
            {
                if (Form1.LD[0].LD_TYPE == "AMC")
                {
                    Port_NAME = COM;
                    DRS_PORT.PortName = COM;
                }
            }
            
        }

        public void Set_BAUDRATE(int baudrate)
        {
            DRS_PORT.BaudRate = baudrate;
        }

        public void MOTOR_P90(byte ID, byte PLAYT, int OPT)
        {
            if (bEStop == false)
            {
                if (Form1.LD[0].LD_TYPE == "AMC")
                {
                    MOTOR_CLEAR1(ID);
                    Send_DATA(ID, CMD_S_JOG, PLAYT, 780, OPT);
                }
            }
        }

        public void MOTOR_M90(byte ID, byte PLAYT, int OPT)
        {
            if (bEStop == false)
            {
                if (Form1.LD[0].LD_TYPE == "AMC")
                {
                    MOTOR_CLEAR1(ID);
                    Send_DATA(ID, CMD_S_JOG, PLAYT, 220, OPT);
                }
            }
        }

        public void MOTOR_ZERO(byte ID, byte PLAYT, int OPT)
        {
            if (bEStop == false)
            {
                if (Form1.LD[0].LD_TYPE == "AMC")
                {
                    MOTOR_CLEAR1(ID);
                    Send_DATA(ID, CMD_S_JOG, PLAYT, 512, OPT);
                }
            }
        }

        public void MOTOR_ON(int ID)
        {
            byte bID = (byte)ID;
            motot_clear();

            if (bID == 3)
            {                
                Send_DATA(bID, CMD_S_JOG, 1, 620, 0);
            }
            else if(bID == 4)
            {
                Send_DATA(bID, CMD_S_JOG, 1, 380, 0);
            }
            else if(bID == 5)
            {
                Send_DATA(bID, CMD_S_JOG, 1, 650, 0);
            }
            else if(bID == 6)
            {
                Send_DATA(bID, CMD_S_JOG, 1, 787, 0);
            }
            else if(bID == 7)
            {
                Send_DATA(bID, CMD_S_JOG, 1, 216, 0);
            }

            Form1.Insert_System_Log("Stopper" + ID.ToString() + " ON");
        }

        public void MOTOR_OFF(int ID)
        {
            byte bID = (Byte)ID;

            //motot_clear();

            if (bID == 3)
            {
                Send_DATA(bID, CMD_S_JOG, 1, 770, 0);
            }
            else if (bID == 4)
            {
                Send_DATA(bID, CMD_S_JOG, 1, 220, 0);
            }
            else if(bID == 5)
            {
                Send_DATA(bID, CMD_S_JOG, 1, 820, 0);
            }            
            else if (bID == 6)
            {
                Send_DATA(bID, CMD_S_JOG, 100, 510, 0);
            }
            else if (bID == 7)
            {
                Send_DATA(bID, CMD_S_JOG, 100, 500, 0);
            }

            Form1.Insert_System_Log("Stopper" + ID.ToString() + " OFF");
        }


        public void MOTOR_CLEAR2(int ID)
        {
            byte[] data = new byte[9];

            data[0] = 0xFF;
            data[1] = 0xFF;
            data[2] = 0x09;
            data[3] = (byte)ID;
            data[4] = 0x04;
            data[7] = 0x30;
            data[8] = 0x02;

            int CRC = Make_CRC(data);

            data[5] = (byte)((CRC >> 8) & 0x00FF);
            data[6] = (byte)(CRC & 0xFF);

            DRS_PORT.Write(data, 0, 9);
        }

        public void MOTOR_CLEAR1(int ID)
        {
            byte[] data = new byte[11];

            data[0] = 0xFF;
            data[1] = 0xFF;
            data[2] = 0x0B;
            data[3] = (byte)ID;
            data[4] = 0x03;
            data[7] = 0x30;
            data[8] = 0x02;
            data[9] = 0x00;
            data[10] = 0x00;

            int CRC = Make_CRC(data);

            data[5] = (byte)((CRC >> 8) & 0x00FF);
            data[6] = (byte)(CRC & 0xFF);

            DRS_PORT.Write(data, 0, 11);

            MOTOR_CLEAR2(ID);
        }


        public void Read_RAM_DATA(byte ID, byte ADD, byte len)
        {
            byte[] data = new byte[9];

            data[0] = 0xFF;
            data[1] = 0xFF;
            data[2] = 0x09;
            data[3] = ID;
            data[4] = 0x04;
            data[7] = ADD;
            data[8] = len;

            int CRC = Make_CRC(data);

            data[5] = (byte)((CRC >> 8) & 0x00FF);
            data[6] = (byte)(CRC & 0xFF);

            DRS_PORT.Write(data, 0, 9);
        }

        public void MOTOR_TORQUE_ON(int ID)
        {
            byte bID = (byte)ID;
            byte[] send_data = new byte[10];

            send_data[0] = 0xFF;
            send_data[1] = 0xFF;

            send_data[2] = 10;

            send_data[3] = bID;
            send_data[4] = 0x03;                       

            send_data[7] = 0x34;
            send_data[8] = 0x01;
            send_data[9] = 0x60;

            int CRC = Make_CRC(send_data);
            send_data[5] = (byte)(CRC >> 8);
            send_data[6] = (byte)(CRC & 0x00FF);

            DRS_PORT.Write(send_data, 0, send_data.Length);

            Form1.Insert_System_Log("Stopper" + ID.ToString() + " toque ON");
        }


        public void Send_DATA(byte pID, byte CMD, byte PlayT, int POS, int OPT)
        {
            byte[] send_data = new byte[12];

            if (bEStop == false)
            {
                if (Form1.LD[0].LD_TYPE == "AMC")
                {
                    send_data[0] = 0xFF;
                    send_data[1] = 0xFF;

                    send_data[2] = 12;

                    send_data[3] = pID;
                    send_data[4] = CMD;

                    send_data[7] = PlayT;

                    send_data[9] = (byte)(POS >> 8);
                    send_data[8] = (byte)(POS & 0x00FF);

                    send_data[10] = 0x00;
                    send_data[11] = pID;

                    int CRC = Make_CRC(send_data);
                    send_data[5] = (byte)(CRC >> 8);
                    send_data[6] = (byte)(CRC & 0x00FF);

                    DRS_PORT.Write(send_data, 0, send_data.Length);
                }
            }
        }

        public int Make_CRC(byte[] data)
        {
            byte CRC1 = 0x00;
            byte CRC2 = 0x00;
            int CRC = 0;
            try
            {
                if (data.Length == 0)
                    return CRC;

                CRC1 = data[2];

                for (int i = 3; i < data.Length; i++)
                {
                    if (i != 5 && i != 6)
                        CRC1 ^= data[i];
                }

                CRC1 = (byte)(CRC1 & 0xFE);
                CRC2 = (byte)(~CRC1 & 0xFE);

                CRC = CRC1;
                CRC = CRC << 8;
                CRC = CRC | CRC2;                
            }
            catch (Exception)
            {
                throw;
            }
            return CRC;
        }

        int OPEN_CNT = 0;

        public void OPEN_COM()
        {
            try
            {
                if (Form1.LD[0].LD_TYPE == "AMC")
                {
                    if (DRS_PORT.IsOpen == false)
                    {
                        DRS_PORT.Open();
                        OPEN_CNT = 0;
                    }
                }
            }
            catch (UnauthorizedAccessException unauthorizedAccessException)
            {
                {
                    OPEN_CNT++;
                    if(OPEN_CNT < 5)
                    {
                        System.Threading.Thread.Sleep(250);
                        OPEN_COM();
                    }
                    else
                    {
                        MessageBox.Show(string.Format("{0} IS NOT OPEN !!",DRS_PORT.PortName));
                    }                    
                }                
            }

        }

        public void CLOSE_COM()
        {
            DRS_PORT.Close();
        }
    }
}
