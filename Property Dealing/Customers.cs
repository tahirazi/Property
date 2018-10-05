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
    public partial class Customers : Form
    {
        SqlConnection con = new SqlConnection(Connection_Properties.GetConnectionString());
        SqlDataAdapter da;
        SqlCommand cmd = null;
        public Customers()
        {
            InitializeComponent();
        }

        private void Customers_Load(object sender, EventArgs e)
        {
            

            GetData();
        }

        #region Functions and methods
        public void GetData()
        {
            da = new SqlDataAdapter("SELECT [customer_no] as [Customer No.],[customer_name] as [Customer Name],[f_name] as [S/O, W/O, D/O],[id_card] as [ID Card No.],[phone] as Phone,[address] as [Address] FROM [saiban].[dbo].[customers]", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        public void clear_boxes()
        {
            txtAddress.Clear();
            txtCustomerName.Clear();
            txtFather.Clear();
            txtIDCard.Clear();
            txtPhone.Clear();
            lblID.Text = "Customer ID";
        }
        #endregion

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtCustomerName.Text == "")
            {
                MessageBox.Show("Please select customer Name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "INSERT INTO customers VALUES ('"+txtCustomerName.Text.Trim()+"','"+txtFather.Text.Trim()+"','"+txtIDCard.Text.Trim()+"','"+txtPhone.Text.Trim()+"','"+txtAddress.Text.Trim()+"')";
                    cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                    cmd.Clone();
                    con.Close();
                    MessageBox.Show("Successfully Added.","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
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

        private void btnGetData_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string query = "UPDATE [saiban].[dbo].[customers] SET [customer_name] ='"+txtCustomerName.Text+"',[f_name] ='"+txtFather.Text+"',[id_card] ='"+txtIDCard.Text+"',[phone] ='"+txtPhone.Text+"',[address] ='"+txtAddress.Text+"' WHERE customer_no='"+Convert.ToInt32(lblID.Text)+"' ";
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
                cmd.Clone();
                con.Close();
                MessageBox.Show("Successfully Edited.","Edited",MessageBoxButtons.OK,MessageBoxIcon.Information);
                GetData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                con.Close();
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
                DataGridViewRow dr = dataGridView1.SelectedRows[0];
                lblID.Text = dr.Cells[0].Value.ToString();
                txtCustomerName.Text = dr.Cells[1].Value.ToString();
                txtFather.Text = dr.Cells[2].Value.ToString();
                txtIDCard.Text = dr.Cells[3].Value.ToString();
                txtPhone.Text = dr.Cells[4].Value.ToString();
                txtAddress.Text = dr.Cells[5].Value.ToString();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clear_boxes();
        }
        private void txtSearchCustomerName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                da = new SqlDataAdapter("SELECT [customer_no] as [Customer No.],[customer_name] as [Customer Name],[f_name] as [S/O, W/O, D/O],[id_card] as [ID Card No.],[phone] as Phone,[address] as [Address] FROM [saiban].[dbo].[customers] WHERE customer_name like '"+txtSearchCustomerName.Text+"%'", con);
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
                    da = new SqlDataAdapter("SELECT [customer_no] as [Customer No.],[customer_name] as [Customer Name],[f_name] as [S/O, W/O, D/O],[id_card] as [ID Card No.],[phone] as Phone,[address] as [Address] FROM [saiban].[dbo].[customers] WHERE customer_no like '" + txtSearchCustomerNumber.Text + "%'", con);
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    con.Open();
                    string query = "DELETE FROM [saiban].[dbo].[customers] WHERE customer_no='" + Convert.ToInt32(lblID.Text) + "' ";
                    cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                    cmd.Clone();
                    con.Close();
                    MessageBox.Show("Successfully Deleted.", "Edited", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GetData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                }
            }
        }

        private void Customers_FormClosing(object sender, FormClosingEventArgs e)
        {
            Global.customers = 0;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}