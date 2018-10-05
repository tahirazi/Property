using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Property_Dealing
{
    public partial class comissions : Form
    {
        SqlConnection con = new SqlConnection(Connection_Properties.GetConnectionString());
        SqlDataAdapter da;
        SqlCommand cmd = null;
        SqlDataReader rdr;

        private void Comissions_Load(object sender, EventArgs e)
        {
            GetData();
        }
        public void GetData()
        {
            try
            {
                da = new SqlDataAdapter("SELECT [plot_no] as [Plot no.],[agent_no] as [Agent No.],[agent_name] as [Agent Name],[plot_amount] as [Plot Amount],[comission_percentage] as [Comission Percetage] ,[comission_amount] as [Comissionn Amount] FROM [saiban].[dbo].[comission]", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
        }
    }
}
