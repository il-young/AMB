using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMC_Test
{
    public partial class Board : Form
    {
        string start_area = "";
        string target_area = "";

        public Board()
        {
            InitializeComponent();            
        }

        public void SetParent()
        {
            this.Parent = AMC_Test.AMC_Monitor.ActiveForm;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        public void Set_TEXT(string TEXT)
        {
            tb_text.Text = TEXT;
        }

        public void set_start(string st, string ta)
        {
            start_area = st;
            target_area = ta;
        }

        public void SetBtn(bool val)
        {
            button1.Visible = val;
        }

        private void tb_text_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
