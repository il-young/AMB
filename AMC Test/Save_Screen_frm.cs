using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AMC_Test
{
    public partial class Save_Screen_frm : Form
    {
        public Save_Screen_frm()
        {
            InitializeComponent();
        }

        public void Set_lm_wifi(int nvalue)
        {
            lm_wifi.Value = nvalue;
        }

        public void Set_gg_Value(string value)
        {
            gg_volt.Value = double.Parse(value.Substring(0, value.Length - 2));
        }

        public void Set_om_value(string value)
        {
            om_distance.Value = double.Parse(value.Substring(0, value.Length));
        }

        public void Set_tm_Value(string value)
        {
            tm_temp.Value = int.Parse(value.Substring(0, value.Length));
        }

        private void Save_Screen_frm_Load(object sender, EventArgs e)
        {
            
        }


        private void Save_Screen_frm_Click(object sender, EventArgs e)
        {
            Saver_Hide();
        }

        private void gg_volt_Click(object sender, EventArgs e)
        {
            Saver_Hide();
        }

        private void tm_temp_Click(object sender, EventArgs e)
        {
            Saver_Hide();
        }

        private void om_distance_Click(object sender, EventArgs e)
        {
            Saver_Hide();
        }

        private void lm_wifi_Click(object sender, EventArgs e)
        {
            Saver_Hide();
        }

        private void Saver_Hide()
        {
            this.Hide();
            //Form1.HMI.t_saver.Enabled = true;
        }

        private void panel4_Click(object sender, EventArgs e)
        {
            Saver_Hide();
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            Saver_Hide();
        }

        private void dateTimeDisplay1_Click(object sender, EventArgs e)
        {
            Saver_Hide();
        }

        private void panel9_Click(object sender, EventArgs e)
        {
            Saver_Hide();
        }

        private void panel10_Click(object sender, EventArgs e)
        {
            Saver_Hide();
        }

        private void panel11_Click(object sender, EventArgs e)
        {
            Saver_Hide();
        }

        private void panel12_Click(object sender, EventArgs e)
        {
            Saver_Hide();
        }

        private void panel13_Click(object sender, EventArgs e)
        {
            Saver_Hide();
        }

        private void panel6_Click(object sender, EventArgs e)
        {
            Saver_Hide();
        }

      
    }
}
