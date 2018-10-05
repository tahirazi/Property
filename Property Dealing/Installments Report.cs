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
    public partial class Installments_Report : Form
    {
        public Installments_Report()
        {
            InitializeComponent();
        }
        private void Installments_Report_Load(object sender, EventArgs e)
        {

        }
        private void Installments_Report_FormClosed(object sender, FormClosedEventArgs e)
        {
            Global.Installments_Report = 0;
        }
    }
}
