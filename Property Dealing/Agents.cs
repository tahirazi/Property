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
    public partial class Agents : Form
    {
        SqlConnection con = new SqlConnection(Connection_Properties.GetConnectionString());
        SqlDataAdapter da;
        SqlCommand cmd = null;
        public Agents()
        {
            InitializeComponent();
        }

        private void Agents_Load(object sender, EventArgs e)
        {
            GetData();
        }
        public void GetData()
        {
            da = new SqlDataAdapter("SELECT [agent_no] as [Agent No.] ,[agent_name] as [Agent Name],[phone] as Phone,[address] as [Address] FROM [saiban].[dbo].[agents]", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        public void clear_boxes()
        {
            txtAddress.Clear();
            txtAgentName.Clear();
            lblID.Text = "ID";
            txtPhone.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtAgentName.Text == "")
            {
                MessageBox.Show("Please enter Agent Name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "INSERT INTO agents VALUES ('" + txtAgentName.Text.Trim() + "','" + txtPhone.Text.Trim() + "','" + txtAddress.Text.Trim() + "')";
                    cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                    cmd.Clone();
                    con.Close();
                    MessageBox.Show("Successfully Added.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GetData();
                    clear_boxes();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                }
            }
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

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow dr = dataGridView1.SelectedRows[0];
            lblID.Text = dr.Cells[0].Value.ToString();
            txtAgentName.Text = dr.Cells[1].Value.ToString();
            txtPhone.Text = dr.Cells[2].Value.ToString();
            txtAddress.Text = dr.Cells[3].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string query = "UPDATE [saiban].[dbo].[agents] SET [agent_name] ='" + txtAgentName.Text.Trim() + "',[phone] ='" + txtPhone.Text.Trim() + "',[address] ='" + txtAddress.Text.Trim() + "' WHERE agent_no='"+Convert.ToInt32(lblID.Text)+"' ";
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
                cmd.Clone();
                con.Close();
                MessageBox.Show("Successfully Edited.", "Edited", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetData();
                clear_boxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string query = "DELETE from agents WHERE agent_no='"+Convert.ToInt32(lblID.Text)+"' ";
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
                cmd.Clone();
                con.Close();
                MessageBox.Show("Successfully Deleted.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetData();
                clear_boxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void txtSearchCustomerName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                da = new SqlDataAdapter("SELECT [agent_no] as [Agent No.] ,[agent_name] as [Agent Name],[phone] as Phone,[address] as [Address] FROM [saiban].[dbo].[agents] WHERE agent_name like '" + txtSearchCustomerName.Text + "%'", con);
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

        private void txtSearchCustomerNumber_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchCustomerNumber.Text == "")
            {
                GetData();
            }
            else
            {
                try
                {
                    da = new SqlDataAdapter("SELECT [agent_no] as [Agent No.] ,[agent_name] as [Agent Name],[phone] as Phone,[address] as [Address] FROM [saiban].[dbo].[agents] WHERE agent_no like '" + txtSearchCustomerNumber.Text + "%'", con);
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

        private void Agents_FormClosing(object sender, FormClosingEventArgs e)
        {
            Global.agents = 0;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
