using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMC_Test
{
    public class CJOB
    {
        public struct stCommand
        {
            public string JOB_ID;
            public string JOB_SCR;
            public string JOB_DEST;
            public DateTime JOB_TIME;
            public string JOB_TYPE;
            public string AMC_NAME;
            public string JOB_ST;
            public int nSTEP;

            public bool IsRun;
        };

        stCommand JOB;

        public CJOB()
        {
            JOB.JOB_ID = "";
            JOB.JOB_SCR = "";
            JOB.JOB_DEST = "";
            JOB.JOB_TIME = DateTime.Now;
            JOB.JOB_TYPE = "";
            JOB.AMC_NAME = "";
            JOB.JOB_ST = "";
            JOB.nSTEP = 0;

            JOB.IsRun = false;
        }

        public void Insert_CMD(stCommand Cjob)
        {
            JOB = Cjob;
        }

        public void Set_IsRun(bool val)
        {
            JOB.IsRun = val;
        }

        public bool Get_IsRun()
        {
            return JOB.IsRun;
        }

        public void Set_AMC_NAME(string AMCNAME)
        {
            JOB.AMC_NAME = AMCNAME;
        }

        public void Set_JOB_ST(string ST)
        {
            JOB.JOB_ST = ST;
        }

        public bool IS_EMPTY()
        {
            if (JOB.JOB_TIME == null)
                return true;

            return false;
        }

        public string Get_JOB_ST()
        {
            return JOB.JOB_ST;
        }
        public stCommand Get_JOB()
        {
            return JOB;
        }

        public string Get_TYPE()
        {
            return JOB.JOB_TYPE;
        }

        public string Get_SCR()
        {
            return JOB.JOB_SCR;
        }

        public string Get_DEST()
        {
            return JOB.JOB_DEST;
        }



    }
}
