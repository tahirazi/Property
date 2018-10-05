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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            menuStrip1.BackColor = Color.Transparent;
            menuStrip2.BackColor = Color.Transparent;
            //GoFullscreen(true);
        }
        private void GoFullscreen(bool fullscreen)
        {
            if (fullscreen)
            {
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                this.Bounds = Screen.PrimaryScreen.Bounds;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            }
        }
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void customersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Global.customers++;
            if (Global.customers == 1)
            {
                Customers custo = new Customers();
                custo.Show();
            }
        }

        private void plotsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Global.plots++;
            if (Global.plots == 1)
            {
                Plots plts = new Plots();
                plts.Show();
            }
        }

        private void agentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Global.agents++;
            if (Global.agents == 1)
            {
                Agents agnts = new Agents();
                agnts.Show();
            }
        }

        private void dealsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Global.deals++;
            if (Global.deals == 1)
            {
                Deals dls = new Deals();
                dls.Show();
            }
        }

        private void comissionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            Global.comission++;
            if (Global.comission == 1)
            {
                Com cm = new Com();
                cm.Show();
            }
        }

        private void backUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Global.backUp++;
            if (Global.backUp == 1)
            {
                Backup bu = new Backup();
                bu.Show();
            }

        }

        private void customerReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Global.customer_report++;
            if (Global.customer_report == 1)
            {
                Print_Customer pcus = new Print_Customer();
                pcus.Show();
            }
        }

        private void installmentsReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Global.Installments_Report++;
            if (Global.Installments_Report == 1)
            {
                Installments_Report ireports = new Installments_Report();
                ireports.Show();
            }
        }

        private void comissionReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Global.com_rep++;
            if (Global.com_rep == 1)
            {
                Comission_Report crpt = new Comission_Report();
                crpt.Show();
            }
        }
    }
}
