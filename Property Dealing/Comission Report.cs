using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Property_Dealing
{
    public partial class Comission_Report : Form
    {
        public int abcd = 0;
        public Comission_Report()
        {
            InitializeComponent();
        }

        private void Comission_Report_Load(object sender, EventArgs e)
        {
            if (abcd == 0)
            {

            }
            else
            {
                DataSet1.EnforceConstraints = false;
                this.comissionTableAdapter.Fill(this.DataSet1.comission, abcd);
                // TODO: This line of code loads data into the 'DataSet1.comission' table. You can move, or remove it, as needed.
                this.reportViewer1.RefreshReport();
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            DataSet1.EnforceConstraints = false;
            this.comissionTableAdapter.Fill(this.DataSet1.comission, Convert.ToInt32(txtSearch.Text));
            // TODO: This line of code loads data into the 'DataSet1.comission' table. You can move, or remove it, as needed.
            this.reportViewer1.RefreshReport();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Comission_Report_FormClosing(object sender, FormClosingEventArgs e)
        {
            Global.com_rep = 0;
        }
    }
}
