using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AMC_Test
{
    class MS_SQL
    {
        public delegate void DataReceve(string query, System.Data.DataSet ds, double delay);
        public event DataReceve DataReceveEvent;

        

        int RetryMax = 5;

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

        private Queue<sql_cmd> Q = new Queue<sql_cmd>();

        public System.Threading.Thread DBThread;

        public MS_SQL()
        {
            DBThread = new System.Threading.Thread(DBStart);
            DBThread.Start();
        }

        public void DBStart()
        {
            while(true)
            {
                if(Q.Count > 0)
                {
                    try
                    {
                        sql_cmd temp = Q.Peek();

                        if (temp.retry_cnt < RetryMax)
                        {
                            temp.retry();

                            System.Data.DataSet ds = SearchData(temp.Query);

                            if (ds != null)
                            {
                                DataReceveEvent(temp.Query, ds, sw.Elapsed.TotalMilliseconds);
                                sw.Reset();
                                Q.Dequeue();
                            }
                            else
                            {
                                Q.Enqueue(temp);
                                Q.Dequeue();
                            }

                        }
                        else
                        {

                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    
                }

                System.Threading.Thread.Sleep(500);
            }
        }

        public void AddQuery(string query)
        {
            if(Q.Count == 0 )
            {
                sql_cmd temp = new sql_cmd();

                temp.init();
                temp.Query = query;                

                Q.Enqueue(temp);
            }
            else if (Q.Peek().Query != query)
            {
                sql_cmd temp = new sql_cmd();

                temp.init();
                temp.Query = query;               

                Q.Enqueue(temp);
            }
        }
        Stopwatch sw = new Stopwatch();

        private System.Data.DataSet SearchData(string sql)
        {
            System.Data.DataSet dt = new System.Data.DataSet();

            try
            {

                sw.Start();
                //strConnetionIP = "10.131.15.18";
                string strConnetion = string.Format("server=10.131.15.18;database=AUTOHW;user id=autohwadm;password=AUTOhw123!;Pooling=true;Min Pool Size=20;Max Pool Size=100;Connection Timeout=10");

                using (SqlConnection c = new SqlConnection(strConnetion))
                {
                    c.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, c))
                    {
                        using (SqlDataAdapter adt = new SqlDataAdapter(cmd))
                        {
                            adt.Fill(dt);
                        }
                    }
                }

                sw.Stop();


            }
            catch (Exception ex)
            {

            }
            return dt;
        }

        public void run_sql_command(string sql)
        {
            try
            {
                //lock (this)
                {
                    using (SqlConnection ssconn = new SqlConnection("server = 10.131.15.18; uid = autohwadm; pwd = Autohw123!; database = AUTOHW"))
                    {
                        ssconn.Open();
                        using (SqlCommand scom = new SqlCommand(sql, ssconn))
                        {
                            scom.CommandType = System.Data.CommandType.Text;
                            scom.CommandText = sql;
                            scom.ExecuteReader();
                        }
                    }
                    //ssconn.Close();
                    //ssconn.Dispose();
                    //scom.Dispose();
                }
                //frm_Main.save_log(string.Format("Call:{0} -> Function:{1}, Param:{2}", System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql));
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }

    }
}
