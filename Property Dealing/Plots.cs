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
    public partial class Plots : Form
    {
        SqlConnection con = new SqlConnection(Connection_Properties.GetConnectionString());
        SqlDataAdapter da;
        SqlCommand cmd = null;
        public Plots()
        {
            InitializeComponent();
        }

        private void Plots_Load(object sender, EventArgs e)
        {
            GetData();
        }
        public void GetData()
        {
            da = new SqlDataAdapter("SELECT [reg_no] as [Reg. No.],[plot_no] as [Plot No.],[location] as Location,[size] as [Size],[type] as [Type],[purchase_price] as [Purchase Price],[sale_price] as [Sale Price] FROM [saiban].[dbo].[plots]", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }
        public void clear_boxes()
        {
            txtLocation.Clear();
            txtPlotNo.Clear();
            txtPurchasePrice.Clear();
            txtRegNo.Clear();
            txtSalePrice.Clear();
            txtSize.Clear();
            txtType.Clear();
            lblPlotNo.Text = "Plot No.";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtPlotNo.Text == "")
            {
                MessageBox.Show("Please Enter the Plot Number first.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            if (txtRegNo.Text == "")
            {
                MessageBox.Show("Please Enter the Plot Registration Number first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "INSERT INTO plots VALUES ('" + Convert.ToInt32(txtRegNo.Text.Trim()) + "','" + Convert.ToInt32(txtPlotNo.Text.Trim()) + "','" + txtLocation.Text.Trim() + "',LTRIM(RTRIM('" + txtSize.Text + "')),LTRIM(RTRIM('" + txtType.Text.Trim() + "')),'" + Convert.ToDecimal(txtPurchasePrice.Text.Trim()) + "','" + Convert.ToDecimal(txtSalePrice.Text.Trim()) + "')";
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string query = "UPDATE [saiban].[dbo].[plots] SET [reg_no] ='" + Convert.ToInt32(txtRegNo.Text) + "',[plot_no]='"+Convert.ToInt32(txtPlotNo.Text)+"',[location] =RTRIM('" + txtLocation.Text.Trim() + "'),[size] =RTRIM('" + txtSize.Text.Trim() + "'),[type] =RTRIM('" + txtType.Text.Trim() + "'),[purchase_price] ='" + Convert.ToDecimal(txtPurchasePrice.Text) + "',[sale_price] ='" + Convert.ToDecimal(txtSalePrice.Text) + "' WHERE plot_no='" + Convert.ToInt32(lblPlotNo.Text) + "' ";
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

        private void dataGridView1_RowsDefaultCellStyleChanged(object sender, EventArgs e)
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

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow dr = dataGridView1.SelectedRows[0];
            txtRegNo.Text = dr.Cells[0].Value.ToString();
            txtPlotNo.Text = dr.Cells[1].Value.ToString();
            lblPlotNo.Text = dr.Cells[1].Value.ToString();
            txtLocation.Text = dr.Cells[2].Value.ToString();
            txtSize.Text = dr.Cells[3].Value.ToString();
            txtType.Text = dr.Cells[4].Value.ToString();
            txtPurchasePrice.Text = dr.Cells[5].Value.ToString();
            txtSalePrice.Text = dr.Cells[6].Value.ToString();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string query = "delete from plots WHERE plot_no='" + Convert.ToInt32(txtPlotNo.Text) + "' ";
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
                cmd.Clone();
                con.Close();
                MessageBox.Show("Successfully Deleted.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetData();
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

        private void Plots_FormClosing(object sender, FormClosingEventArgs e)
        {
            Global.plots = 0;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            clear_boxes();
        }

        private void txtSearchPlotNo_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchPlotNo.Text == "")
            {
                GetData();
            }
            else
            {
                try
                {
                    da = new SqlDataAdapter("SELECT [reg_no] as [Reg. No.],[plot_no] as [Plot No.],[location] as Location,[size] as [Size],[type] as [Type],[purchase_price] as [Purchase Price],[sale_price] as [Sale Price] FROM [saiban].[dbo].[plots] WHERE plot_no like '"+Convert.ToInt32(txtSearchPlotNo.Text)+"%'", con);
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
    }
}
