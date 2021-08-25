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
    public partial class frmLocalize : Form
    {
        private List<string> Goals = new List<string>();

        public frmLocalize()
        {
            InitializeComponent();
        }

        public frmLocalize(List<string> ts)
        {
            InitializeComponent();

            for (int i = 0; i < ts.Count; i++)
            {
                Goals.Add(ts[i]);
            }                
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Vehicle Localize", "Localize", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                Form1.Send_LD_String("localizeAtGoal " + cb_Goal.Text);
                Close();
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Close();
            this.Dispose();
        }

        private void frmLocalize_Load(object sender, EventArgs e)
        {
            for(int i  = 0; i < Goals.Count; i++)
            {
                cb_Goal.Items.Add(Goals[i]);
            }
            
        }
    }
}
