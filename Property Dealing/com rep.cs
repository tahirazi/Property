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
    public partial class com_rep : Form
    {
        public com_rep()
        {
            InitializeComponent();
        }

        private void com_rep_Load(object sender, EventArgs e)
        {
            DataSet1.EnforceConstraints = false;
            // TODO: This line of code loads data into the 'DataSet1.comission' table. You can move, or remove it, as needed.
            this.comissionTableAdapter.Fill(this.DataSet1.comission,1);
            // TODO: This line of code loads data into the 'DataSet1.comission' table. You can move, or remove it, as needed.
            this.reportViewer1.RefreshReport();
        }
    }
}
