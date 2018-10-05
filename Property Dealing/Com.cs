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
    public partial class Com : Form
    {
        public decimal percentage=0, amount=0, plot_amount=0;
        SqlConnection con = new SqlConnection(Connection_Properties.GetConnectionString());
        SqlDataAdapter da;
        SqlCommand cmd = null;
        SqlDataReader rdr;
        public Com()
        {
            InitializeComponent();
        }

        private void Com_Load(object sender, EventArgs e)
        {
            GetData();
            FillCombo();
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
        public void clear_boxes()
        {
            txtAgentName.Clear();
            txtAmount.Clear();
            txtPercentage.Clear();
            txtPlotAmount.Clear();
            cmbAgentNo.Text = "";
            cmbPlotNo.Text = "";
            lblPlotNo.Text = "Plot No.";
        }
        public void FillCombo()
        {
            try
            {
                con.Open();
                cmd = new SqlCommand();
                cmd.Connection = con;
                string query = "SELECT RTRIM (plot_no) from plots ORDER BY plot_no ASC";
                cmd.CommandText = query;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    cmbPlotNo.Items.Add(rdr[0]);
                }
                con.Close();

                con.Open();
                cmd = new SqlCommand();
                cmd.Connection = con;
                string query2 = "SELECT RTRIM (agent_no) from agents ORDER BY agent_no ASC";
                cmd.CommandText = query2;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    cmbAgentNo.Items.Add(rdr[0]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            clear_boxes();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string cb = "INSERT INTO comission VALUES ('"+Convert.ToInt32(cmbPlotNo.Text)+"','"+Convert.ToInt32(cmbAgentNo.Text)+"','"+txtAgentName.Text.Trim()+"','"+Convert.ToDecimal(txtPlotAmount.Text)+"','"+Convert.ToInt32(txtPercentage.Text)+"','"+Convert.ToDecimal(txtAmount.Text)+"')";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.ExecuteReader();
                con.Close();
                MessageBox.Show("Successfully saved", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clear_boxes();
                GetData();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string cb = "UPDATE [saiban].[dbo].[comission] SET [plot_no]='"+Convert.ToInt32(cmbPlotNo.Text)+"',[agent_no] ='"+Convert.ToInt32(cmbAgentNo.Text)+"',[agent_name] ='"+txtAgentName.Text.Trim()+"',[plot_amount] ='"+Convert.ToDecimal(txtPlotAmount.Text)+"',[comission_percentage] ='"+txtPercentage.Text+"',[comission_amount] ='"+Convert.ToDecimal(txtAmount.Text)+"' WHERE plot_no='"+Convert.ToInt32(lblPlotNo.Text)+"'";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.ExecuteReader();
                con.Close();
                MessageBox.Show("Successfully Updated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clear_boxes();
                GetData();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {

        }

        private void cmbPlotNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                cmd = new SqlCommand();
                cmd.Connection = con;
                string query = "SELECT sale_price from plots WHERE plot_no='" + Convert.ToInt32(cmbPlotNo.Text) + "'";
                cmd.CommandText = query;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    txtPlotAmount.Text = rdr[0].ToString();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
        }

        private void cmbAgentNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                cmd = new SqlCommand();
                cmd.Connection = con;
                string query = "SELECT agent_name from agents WHERE agent_no='" + Convert.ToInt32(cmbAgentNo.Text) + "'";
                cmd.CommandText = query;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    txtAgentName.Text = rdr[0].ToString();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
        }

        private void txtPercentage_TextChanged(object sender, EventArgs e)
        {
            if (txtPercentage.Text == "")
            {
                
            }
            else
            {
                try
                {
                    plot_amount = Convert.ToDecimal(txtPlotAmount.Text);
                    percentage = Convert.ToDecimal(txtPercentage.Text);
                    amount = (plot_amount / 100) * percentage;
                    txtAmount.Text = amount.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Com_FormClosed(object sender, FormClosedEventArgs e)
        {
            Global.comission = 0;
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow dr = dataGridView1.SelectedRows[0];
            cmbPlotNo.Text = dr.Cells[0].Value.ToString();
            cmbAgentNo.Text = dr.Cells[1].Value.ToString();
            txtAgentName.Text = dr.Cells[2].Value.ToString();
            txtPlotAmount.Text = dr.Cells[3].Value.ToString();
            txtPercentage.Text = dr.Cells[4].Value.ToString();
            txtAmount.Text = dr.Cells[5].Value.ToString();

        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            string strRowNumber = (e.RowIndex + 1).ToString();
            SizeF size = e.Graphics.MeasureString(strRowNumber, this.Font);
            if (dataGridView1.RowHeadersWidth < Convert.ToInt32((size.Width + 20)))
            {
                dataGridView1.RowHeadersWidth = Convert.ToInt32((size.Width + 20));
            }
            Brush b = SystemBrushes.ControlText;
            e.Graphics.DrawString(strRowNumber, this.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));
        }

        private void cmbSearch_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
