using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMC_Test
{    
    class C_CMD_ACK
    {
        private System.ComponentModel.BackgroundWorker bg_Time_out = new System.ComponentModel.BackgroundWorker();
        private bool bTime_out_terminator = false;
        private int nBW_Sleep;

        public struct CMD_LIST
        {
            public DateTime CMD_TIME;
            public string CMD_MSG;
        };


        private List<CMD_LIST> CMD_Q = new List<CMD_LIST>();


        public C_CMD_ACK()
        {
            bTime_out_terminator = false;

            nBW_Sleep = 100;
            bg_Time_out.WorkerSupportsCancellation = true;
            bg_Time_out.DoWork += new System.ComponentModel.DoWorkEventHandler(bg_Time_out_DoWork);

        }

        public void Run_Time_out_check()
        {
            bTime_out_terminator = true;
            bg_Time_out.RunWorkerAsync();
        }

        public void Insert_Q(string msg)
        {
            CMD_LIST cmd;
            cmd.CMD_TIME = DateTime.Now;
            cmd.CMD_MSG = msg;

            CMD_Q.Add(cmd);
        }

        public void Check_cmd(string msg)
        {
            string[] str_buf = msg.Split(',');

            if (str_buf[0] == "ACK")
            {
                string new_msg="";

                for(int i = 1; i < str_buf.Length; i++)
                {
                    new_msg += str_buf[i] + ",";
                }

                new_msg = new_msg.Remove(new_msg.Length - 1, 1);

                for (int i = 0; i < CMD_Q.Count; i++)
                {
                    if (CMD_Q[i].CMD_MSG == new_msg)
                    {
                        CMD_Q.Remove(CMD_Q[i]);
                        break;
                    }
                }
            }
        }
                

        public void Set_Time_BW_time(int untime)
        {
            nBW_Sleep = untime;
        }

        private void bg_Time_out_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            DateTime now_time;
            TimeSpan timeDiff;
            while (bTime_out_terminator)
            {
                for(int i=0; i< CMD_Q.Count; i++)
                {
                    now_time = DateTime.Now;

                    timeDiff = CMD_Q[i].CMD_TIME - now_time;

                    if(timeDiff.Seconds < -3)
                    {
                        //AMC_Server.ERR_Insert(CMD_Q[i].CMD_TIME, CMD_Q[i].CMD_MSG);                                      
                    }

                }
                System.Threading.Thread.Sleep(nBW_Sleep);
            }
        }
    }    
}
