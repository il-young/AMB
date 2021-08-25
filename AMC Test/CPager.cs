using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace AMC_Test
{
    class CPager
    {
        SqlConnection conn = new SqlConnection(AMC_Test.Properties.Settings.Default.Pager_DB_ConnectionString);

        public void DB_Connect()
        {
            conn.Open();
        }

        public void DB_Write(string msg, int CAPCODE)
        {
            DB_Connect();
            AMC_Test.Pager_MessageTableAdapters.MESSAGE_READYTableAdapter adp = new AMC_Test.Pager_MessageTableAdapters.MESSAGE_READYTableAdapter();
            adp.Connection = conn;
            
            adp.Insert(DateTime.Now, msg, "P", 0, CAPCODE, "", "", "", "");

        }
    }
}
