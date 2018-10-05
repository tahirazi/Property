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
    public partial class Print_Customer : Form
    {
        public int plot_no=0;
        public Print_Customer()
        {
            InitializeComponent();
        }

        private void Print_Customer_FormClosed(object sender, FormClosedEventArgs e)
        {
            Global.customer_report = 0;
        }

        private void Print_Customer_Load(object sender, EventArgs e)
        {
            if (plot_no == 0)
            {

            }
            else
            {
                DataSet1.EnforceConstraints = false;
                this.customerReportTableAdapter.Fill(this.DataSet1.customerReport, plot_no);
                this.reportViewer1.RefreshReport();
            }
            
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            DataSet1.EnforceConstraints = false;
            this.customerReportTableAdapter.Fill(this.DataSet1.customerReport, Convert.ToInt32(txtSearch.Text));
            this.reportViewer1.RefreshReport();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
