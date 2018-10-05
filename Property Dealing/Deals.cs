using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;

namespace Property_Dealing
{
    public partial class Deals : Form
    {
        
        SqlConnection con = new SqlConnection(Connection_Properties.GetConnectionString());
        SqlCommand cmd = null;
        SqlDataAdapter da;
        SqlDataReader rdr;
        
        public Deals()
        {
            InitializeComponent();
        }

        private void Deals_Load(object sender, EventArgs e)
        {
            //dtpDate.Format = DateTimePickerFormat.Custom;
            //dtpDate.CustomFormat = "dd-MM-yyyy";
            fill_combo();
            GetData();
            
        }
        public void Update_Data()
        {
            try
            {
                con.Open();
                da = new SqlDataAdapter("SELECT [deal_no] as [Deal No.] ,[plot_no] as [Plot No] ,[customer_no] as [Customer No.] ,[customer_name] as [Customer Name] ,[plot_amount] as [Plot Amount] ,[advance] as [Advance] ,[installment_no] as [Installment No.] ,[installment_month] as [Installment Month] ,[installment_amount] as [Installment Amount] ,[date] as [Date] ,[agent_no] as [Agent No.] ,[agent_name] as [Agent Name] ,[comision_amount] as [Comission Amount], [receipt_no] as [Receipt No.] FROM [saiban].[dbo].[deals] WHERE customer_name like '" + txtSearch.Text + "%'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
                sum();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
        }
        public int A()
        {
            string stmt = "SELECT COUNT(*) FROM dbo.deals WHERE plot_no='"+Convert.ToInt32(cmbPlotNo.Text)+"'";
            int count = 0;

            using (SqlConnection con1 = new SqlConnection(Connection_Properties.GetConnectionString()))
            {
                using (SqlCommand cmdCount = new SqlCommand(stmt, con1))
                {
                    con1.Open();
                    count = (int)cmdCount.ExecuteScalar();
                    con1.Close();
                }
            }
            return count;
        }
        public void Customer_get()
        {
            try
            {
                con.Open();
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT distinct customer_no from deals WHERE plot_no='"+Convert.ToInt32(cmbPlotNo.Text)+"'";
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    cmbCustomerNo.Text = rdr[0].ToString();
                    //txtCustomerName.Text = rdr[1].ToString();
                    //txtPlotAmount.Text = rdr[1].ToString();
                    rdr.Close();
                    con.Close();
                    //try
                    //{
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
                    GetCom();
                }
                else
                {
                    rdr.Close();
                    con.Close();
                    cmbCustomerNo.Text = "";
                    txtCustomerName.Text = "";
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
                    GetCom();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        con.Close();
                    }
                }
                rdr.Close();
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
        }
        public void GetCom()
        {
            try
            {
                con.Open();
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT comission_amount from comission where plot_no='" + Convert.ToInt32(cmbPlotNo.Text) + "'";
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    txtComissionAm.Text = rdr[0].ToString();
                    con.Close();
                }
                rdr.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
            
            //da = new SqlDataAdapter("select comission_amount from comission",con);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //txtComissionAm.Text = dt.Rows[0].ToString();
        }
        public void GetData()
        {
            try
            {
                con.Open();
                da = new SqlDataAdapter("SELECT [installment_no] as [Installment No.] ,[installment_month] as [Installment Month] ,[installment_amount] as [Installment Amount] ,[date] as [Date], [comision_amount] as [Comission Amount], [receipt_no] as [Receipt No.] FROM [saiban].[dbo].[deals]", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
                sum();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
        }
        public void sum()
        {
            decimal Total = 0;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                Total += Convert.ToDecimal(dataGridView1.Rows[i].Cells["Comission Amount"].Value);
            }
            txtComissionPercentage.Text = Total.ToString();
            //txtComissionAm
            //  .Text = Total.ToString();
        }
        public void clear_boxes()
        {
            lblID.Text = "ID";
            txtAdvance.Clear();
            txtAgentName.Clear();
            txtComissionAmount.Clear();
            txtCustomerName.Clear();
            txtInstallmentAmount.Clear();
            txtInstallmentMonth.Clear();
            txtInstallmentNo.Clear();
            txtPlotAmount.Clear();
            cmbAgentNo.Text = "";
            cmbCustomerNo.Text = "";
            cmbPlotNo.Text = "";
            txtReceiptNo.Clear();

        }
        public void fill_combo()
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
                string query1 = "SELECT RTRIM (customer_no) from customers ORDER BY customer_no ASC";
                cmd.CommandText = query1;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    cmbCustomerNo.Items.Add(rdr[0]);
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
        public void minus()
        {
            decimal a, b, c;
            a = Convert.ToDecimal(txtComissionAm.Text);
            b = Convert.ToDecimal(txtComissionPercentage.Text);
            c = a - b;
            txtPayableComission.Text = c.ToString();
        }
        private void cmbPlotNo_SelectedIndexChanged(object sender, EventArgs e)
        {
             Customer_get();
            try
                    {
                        con.Open();
                        da = new SqlDataAdapter("SELECT [installment_no] as [Installment No.] ,[installment_month] as [Installment Month] ,[installment_amount] as [Installment Amount] ,[date] as [Date],[comision_amount] as [Comission Amount], [receipt_no] as [Receipt No.] FROM [saiban].[dbo].[deals] WHERE plot_no like '" + Convert.ToInt32(cmbPlotNo.Text) + "%'", con);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;
                        con.Close();
                        sum();
                        GetCom();
                        int b = A() + 1;
                        txtInstallmentNo.Text = b.ToString();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        con.Close();
                    }
            //try
            //{
            //    con.Open();
            //    cmd = new SqlCommand();
            //    cmd.Connection = con;
            //    string query = "SELECT sale_price from plots WHERE plot_no='"+Convert.ToInt32(cmbPlotNo.Text)+"'";
            //    cmd.CommandText = query;
            //    rdr = cmd.ExecuteReader();
            //    while (rdr.Read())
            //    {
            //        txtPlotAmount.Text = rdr[0].ToString();
            //    }
            //    con.Close();
            //    GetCom();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    con.Close();
            //}
            minus();
        }

        private void cmbCustomerNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con.Close();
                con.Open();
                cmd = new SqlCommand();
                cmd.Connection = con;
                string query = "SELECT customer_name from customers WHERE customer_no='" + Convert.ToInt32(cmbCustomerNo.Text) + "'";
                cmd.CommandText = query;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    txtCustomerName.Text = rdr[0].ToString();
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //con.Open();
                //String ct = "select plot_no from deals where plot_no=" + Convert.ToInt32(cmbPlotNo.Text)+ "";
                //cmd = new SqlCommand(ct);
                //cmd.Connection = con;
                //rdr = cmd.ExecuteReader();

                //if (rdr.Read() == true)
                //{
                //    MessageBox.Show("Record already exists" + "\n" + "please update the record of plot.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //    if ((rdr != null))
                //    {
                //        rdr.Close();
                //    }
                //    return;
                //}
                //rdr.Close();
                //con.Close();
                con.Open();
                string cb = "INSERT INTO deals VALUES ('"+Convert.ToInt32(cmbPlotNo.Text)+"','"+Convert.ToInt32(cmbCustomerNo.Text)+"','"+txtCustomerName.Text.Trim()+"','"+Convert.ToDecimal(txtPlotAmount.Text)+"','"+Convert.ToDecimal(txtAdvance.Text)+"','"+Convert.ToInt32(txtInstallmentNo.Text)+"','"+txtInstallmentMonth.Text.Trim()+"','"+Convert.ToDecimal(txtInstallmentAmount.Text)+"','"+dtpDate.Text+"','"+Convert.ToInt32(cmbAgentNo.Text)+"','"+txtAgentName.Text.Trim()+"','"+Convert.ToDecimal(txtComissionAmount.Text)+"','"+Convert.ToInt32(txtReceiptNo.Text)+"')";
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

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
                string cb = "UPDATE [saiban].[dbo].[deals] SET [plot_no] ='"+Convert.ToInt32(cmbPlotNo.Text)+"' ,[customer_no] ='"+Convert.ToInt32(cmbCustomerNo.Text)+"' ,[customer_name] ='"+txtCustomerName.Text+"' ,[plot_amount] ='"+Convert.ToDecimal(txtPlotAmount.Text)+"' ,[advance] ='"+Convert.ToDecimal(txtAdvance.Text)+"' ,[installment_no] ='"+Convert.ToInt32(txtInstallmentNo.Text)+"' ,[installment_month] ='"+txtInstallmentMonth.Text.Trim()+"',[installment_amount] ='"+Convert.ToDecimal(txtInstallmentAmount.Text)+"' ,[date] ='"+dtpDate.Text+"' ,[agent_no] ='"+Convert.ToInt32(cmbAgentNo.Text)+"' ,[agent_name] ='"+txtAgentName.Text+"' ,[comision_amount] ='"+Convert.ToDecimal(txtComissionAmount.Text)+"', [receipt_no]='"+Convert.ToInt32(txtReceiptNo.Text)+"' WHERE deal_no='"+Convert.ToInt32(lblID.Text)+"'";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.ExecuteReader();
                con.Close();
                MessageBox.Show("Successfully Updated", "Record Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            try
            {
                con.Open();
                string cb = "DELETE FROM deals WHERE deal_no='"+Convert.ToInt32(lblID.Text)+"'";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.ExecuteReader();
                con.Close();
                MessageBox.Show("Successfully Deleted", "Record Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clear_boxes();
                GetData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource == null)
            {
                MessageBox.Show("Sorry nothing to export into excel sheet..", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int rowsTotal = 0;
            int colsTotal = 0;
            int I = 0;
            int j = 0;
            int iC = 0;
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            Excel.Application xlApp = new Excel.Application();

            try
            {
                Excel.Workbook excelBook = xlApp.Workbooks.Add();
                Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelBook.Worksheets[1];
                xlApp.Visible = true;

                rowsTotal = dataGridView1.RowCount - 1;
                colsTotal = dataGridView1.Columns.Count - 1;
                var _with1 = excelWorksheet;
                _with1.Cells.Select();
                _with1.Cells.Delete();
                for (iC = 0; iC <= colsTotal; iC++)
                {
                    _with1.Cells[1, iC + 1].Value = dataGridView1.Columns[iC].HeaderText;
                }
                for (I = 0; I <= rowsTotal - 1; I++)
                {
                    for (j = 0; j <= colsTotal; j++)
                    {
                        _with1.Cells[I + 2, j + 1].value = dataGridView1.Rows[I].Cells[j].Value;
                    }
                }
                _with1.Rows["1:1"].Font.FontStyle = "Bold";
                _with1.Rows["1:1"].Font.Size = 12;

                _with1.Cells.Columns.AutoFit();
                _with1.Cells.Select();
                _with1.Cells.EntireColumn.AutoFit();
                _with1.Cells[1, 1].Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //RELEASE ALLOACTED RESOURCES
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                xlApp = null;
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridViewRow dr = dataGridView1.SelectedRows[0];
                lblID.Text = dr.Cells[0].Value.ToString();
                cmbPlotNo.Text = dr.Cells[1].Value.ToString();
                cmbCustomerNo.Text = dr.Cells[2].Value.ToString();
                txtCustomerName.Text = dr.Cells[3].Value.ToString();
                txtPlotAmount.Text = dr.Cells[4].Value.ToString();
                txtAdvance.Text = dr.Cells[5].Value.ToString();
                txtInstallmentNo.Text = dr.Cells[6].Value.ToString();
                txtInstallmentMonth.Text = dr.Cells[7].Value.ToString();
                txtInstallmentAmount.Text = dr.Cells[8].Value.ToString();
                dtpDate.Text = dr.Cells[9].Value.ToString();
                cmbAgentNo.Text = dr.Cells[10].Value.ToString();
                txtAgentName.Text = dr.Cells[11].Value.ToString();
                txtComissionAmount.Text = dr.Cells[12].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Select Plot No. and try again. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            clear_boxes();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if(cmbSearch.Text=="")
            {
                GetData();
            }
            else if (cmbSearch.Text == "Plot No.")
            {
                if (txtSearch.Text == "")
                {
                    GetData();
                }
                else
                {
                    try
                    {
                        con.Open();
                        da = new SqlDataAdapter("SELECT [installment_no] as [Installment No.] ,[installment_month] as [Installment Month] ,[installment_amount] as [Installment Amount] ,[date] as [Date],[comision_amount] as [Comission Amount], [receipt_no] as [Receipt No.] FROM [saiban].[dbo].[deals] WHERE plot_no like '" + Convert.ToInt32(txtSearch.Text) + "%'", con);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;
                        con.Close();
                        sum();
                        GetCom();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        con.Close();
                    }
                }
            }
            else if (cmbSearch.Text == "Customer No.")
            {
                if (txtSearch.Text == "")
                {
                    GetData();
                }
                else
                {
                    try
                    {
                        con.Open();
                        da = new SqlDataAdapter("SELECT [installment_no] as [Installment No.] ,[installment_month] as [Installment Month] ,[installment_amount] as [Installment Amount] ,[date] as [Date],[comision_amount] as [Comission Amount], [receipt_no] as [Receipt No.] FROM [saiban].[dbo].[deals] WHERE customer_no like '" + Convert.ToInt32(txtSearch.Text) + "%'", con);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;
                        con.Close();
                        sum();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        con.Close();
                    }
                }
            }
            else if (cmbSearch.Text == "Customer Name")
            {
                if (txtSearch.Text == "")
                {
                    GetData();
                }
                else
                {
                    try
                    {
                        con.Open();
                        da = new SqlDataAdapter("SELECT [installment_no] as [Installment No.] ,[installment_month] as [Installment Month] ,[installment_amount] as [Installment Amount] ,[date] as [Date],[comision_amount] as [Comission Amount], [receipt_no] as [Receipt No.] FROM [saiban].[dbo].[deals] WHERE customer_name like '" + txtSearch.Text + "%'", con);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;
                        con.Close();
                        sum();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        con.Close();
                    }
                }
            }
            else if (cmbSearch.Text == "Agent No.")
            {
                if (txtSearch.Text == "")
                {
                    GetData();
                }
                else
                {
                    try
                    {
                        con.Open();
                        da = new SqlDataAdapter("SELECT [installment_no] as [Installment No.] ,[installment_month] as [Installment Month] ,[installment_amount] as [Installment Amount] ,[date] as [Date],[comision_amount] as [Comission Amount], [receipt_no] as [Receipt No.] FROM [saiban].[dbo].[deals] WHERE agent_no like '" + Convert.ToInt32(txtSearch.Text) + "%'", con);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;
                        con.Close();
                        sum();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        con.Close();
                    }
                }
            }
            else if (cmbSearch.Text == "Agent Name")
            {
                if (txtSearch.Text == "")
                {
                    GetData();
                }
                else
                {
                    try
                    {
                        con.Open();
                        da = new SqlDataAdapter("SELECT [installment_no] as [Installment No.] ,[installment_month] as [Installment Month] ,[installment_amount] as [Installment Amount] ,[date] as [Date],[comision_amount] as [Comission Amount], [receipt_no] as [Receipt No.] FROM [saiban].[dbo].[deals] WHERE agent_name like '" + txtSearch.Text + "%'", con);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;
                        con.Close();
                        sum();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        con.Close();
                    }
                }
            }
            else
            {
                GetData();
            }
        }

        private void Deals_FormClosing(object sender, FormClosingEventArgs e)
        {
            Global.deals = 0;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void pbPrint_Click(object sender, EventArgs e)
        {
            Global.customer_report++;
            if (Global.customer_report == 1)
            {
                Print_Customer pcus = new Print_Customer();
                pcus.plot_no =Convert.ToInt32(cmbPlotNo.Text);
                pcus.Show();
            }
        }

        private void btnComPrint_Click(object sender, EventArgs e)
        {
            Global.com_rep++;
            if (Global.com_rep == 1)
            {
                Comission_Report crpt = new Comission_Report();
                crpt.abcd =Int32.Parse(cmbPlotNo.Text);
                crpt.Show();
            }
        }
    }
}
